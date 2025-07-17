using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32.SafeHandles;
using System.Management;

namespace Horizon.Library.Systems.FATX
{
    static class FatxDeviceControl
    {
        internal static string createLogicalPath(char logicalDriveLetter, int dataFile)
        {
            return String.Format(@"{0}:\Xbox360\Data{1:D4}", logicalDriveLetter, dataFile);
        }

        internal static string createLocalLogicalPath(string basePath, int dataFile)
        {
            return String.Format(@"{0}\Data{1:D4}", basePath, dataFile);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern SafeFileHandle CreateFile(string lpFileName, FileAccess dwDesiredAccess, FileShare dwShareMode,
        IntPtr lpSecurityAttributes, FileMode dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool DeviceIoControl(IntPtr hDevice, uint dwIoControlCode, IntPtr lpInBuffer, uint nInBufferSize, ref long lpOutBuffer, uint nOutBufferSize, out uint lpBytesReturned, IntPtr lpOverlapped);

        internal static DriveBase[] getFatxDevices(List<string> currentDevices)
        {
            List<DriveBase> deviceVolumes = new List<DriveBase>();
            foreach (DriveInfo logicDrive in DriveInfo.GetDrives())
                if (logicDrive.IsReady && !currentDevices.Contains(logicDrive.Name))
                {
                    try
                    {
                        DriveBase driveBase = new DriveBase(logicDrive);
                        if (driveBase.Valid)
                            deviceVolumes.Add(driveBase);
                    }
                    catch { }
                }
            foreach (ManagementObject physDrive in getPhysicalDrives())
                try
                {
                    if (!currentDevices.Contains((string)physDrive["Name"]))
                    {
                        DriveBase driveBase = new DriveBase((string)physDrive["Name"], (ulong)physDrive["Size"]);
                        if (driveBase.Valid)
                            deviceVolumes.Add(driveBase);
                    }
                }
                catch { }
            return deviceVolumes.ToArray();
        }

        private static ManagementObjectCollection getPhysicalDrives()
        {
            return new ManagementObjectSearcher(new WqlObjectQuery("SELECT Name, ConfigManagerErrorCode, Size FROM Win32_DiskDrive WHERE ConfigManagerErrorCode = 0 AND Size is NOT NULL")).Get();
        }

        internal enum FatxDeviceType
        {
            USB,
            HDD,
            MU
        }

        internal enum DiskType
        {
            PhysicalDrive,
            LogicalDrive,
            MemoryDump
        }

        internal class DriveBase
        {
            internal DriveBase(string volumeName, ulong diskSize)
            {
                Name = volumeName;
                BaseLength = diskSize;
                Type = DiskType.PhysicalDrive;
                PhysicalHandle = CreateFile(Name, FileAccess.ReadWrite, FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, 0xA0000040, IntPtr.Zero);
                if (Valid)
                {
                    uint returnedBytes;
                    if (DeviceIoControl(PhysicalHandle.DangerousGetHandle(), 0x0007405C, IntPtr.Zero, 0, ref Length, 8, out returnedBytes, IntPtr.Zero))
                    {
                        IO = new EndianIO(new FileStream(PhysicalHandle, FileAccess.ReadWrite), EndianType.BigEndian, true);
                        uint fatxMagic = 0x58544146;
                        if (Length > 3 && IO.In.ReadUInt32() == fatxMagic)
                            DeviceType = FatxDeviceType.MU;
                        else if (Length > (long)HDD.Storage + 3)
                        {
                            IO.Stream.Position = (long)HDD.Storage;
                            if (IO.In.ReadUInt32() == fatxMagic)
                                DeviceType = FatxDeviceType.HDD;
                        }
                    }
                }
                if (DeviceType == FatxDeviceType.USB)
                    CloseDisk();
            }

            #if INT2
            internal DriveBase(EndianIO driveIo, DiskType driveType, long length)
            {
                Type = DiskType.MemoryDump;
                Name = driveIo.FileName;
                IO = driveIo;
                IO.Open();
                if (driveType == DiskType.PhysicalDrive)
                {
                    Length = IO.Length;
                    uint fatxMagic = 0x58544146;
                    if (Length > 3 && IO.In.ReadUInt32() == fatxMagic)
                        DeviceType = FatxDeviceType.MU;
                    else if (Length > (long)HDD.Storage + 3)
                    {
                        IO.Stream.Position = (long)HDD.Storage;
                        if (IO.In.ReadUInt32() == fatxMagic)
                            DeviceType = FatxDeviceType.HDD;
                    }
                }
                else
                {
                    Length = length;
                    DeviceType = FatxDeviceType.USB;
                }
            }
            #endif

            internal DriveBase(DriveInfo logicalInfo)
            {
                Name = logicalInfo.Name;
                Type = DiskType.LogicalDrive;

                if (File.Exists(createLogicalPath(Name[0], 0)))
                {
                    List<string> filePaths = new List<string>();
                    for (int x = 0;; x++)
                    {
                        string currentPath = createLogicalPath(Name[0], x);
                        if (File.Exists(currentPath))
                            filePaths.Add(currentPath);
                        else
                            break;
                    }
                    if (filePaths.Count > 0)
                    {
                        IO = new MultiFileIO(filePaths, EndianType.BigEndian, true);
                        Length = IO.Length;
                    }
                }
                else
                {
                    var contentDir = new DirectoryInfo(logicalInfo.Name + "Content");

                    if (contentDir.Exists && contentDir.Name == "Content")
                    {
                        IsFat32 = true;
                    }
                }
                
            }

            internal void CloseDisk()
            {
                if (Type == DiskType.PhysicalDrive)
                {
                    PhysicalHandle.Close();
                    PhysicalHandle = null;
                }
                else
                    IO.Close();
            }

            private SafeFileHandle PhysicalHandle;
            internal EndianIO IO;
            internal DiskType Type;
            internal FatxDeviceType DeviceType;
            internal bool IsFat32;
            internal string Name;
            internal long Length;
            private ulong BaseLength;
            internal bool Valid
            {
                get
                {
                    if (IsFat32)
                        return true;
                    if (Type == DiskType.LogicalDrive || Type == DiskType.MemoryDump)
                        return Mounted && IO != null && IO.Opened;
                    return PhysicalHandle != null && !PhysicalHandle.IsInvalid && !PhysicalHandle.IsClosed;
                }
            }
            internal bool Mounted
            {
                get
                {
                    if (IsFat32)
                        return new DirectoryInfo(Name + "Content").Exists;
                    if (Type == DiskType.LogicalDrive)
                        return File.Exists(createLogicalPath(Name[0], 0));
                    if (Type == DiskType.MemoryDump)
                        return true;
                    foreach (ManagementObject physDrive in getPhysicalDrives())
                        if ((string)physDrive["Name"] == Name && physDrive["Size"] != null && (ulong)physDrive["Size"] == BaseLength)
                            return true;
                    return false;
                }
            }
        }

        public enum MU : long
        {
            Cache = 0,
            Storage = 0x7FF000
        }

        public enum USB : long
        {
            SystemAuxPartition = 0x400,
            StorageSystem = 0x8000400,
            //SystemURLCachePartition = 0xC000400,
            SystemExtPartition = 0x12000400,
            Storage = 0x20000000
        }

        public enum HDD : long
        {
            SecuritySector = 0x2000,
            SystemCache = 0x80000,
            TitleCache = 0x80080000,
            System1 = 0x10C080000,
            ExtendedSystem = 0x118EB0000,
            Compatibility = 0x120EB0000,
            Storage = 0x130EB0000
        }
    }
}
