using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Horizon.Library.Systems.FATX
{
    /* FATX Partitions
     * X = index
     * Preface with \Device
     * 
     *  0: HardDisk
     *  - \HarddiskX\                    - Hard Drive
     *     Device  - Path: \Device\Harddisk0\SystemAuxPartition
           Device  - Path: \Device\Harddisk0\WindowsPartition
           Device  - Path: \Device\Harddisk0\PixDump
           Device  - Path: \Device\Harddisk0\Partition0
           Device  - Path: \Device\Harddisk0\Partition1
           Device  - Path: \Device\Harddisk0\SystemExtPartition
           Device  - Path: \Device\Harddisk0\TitleURLCachePartition
           Device  - Path: \Device\Harddisk0\SystemPartition
           Device  - Path: \Device\Harddisk0\DumpPartition
           Device  - Path: \Device\Harddisk0\MemDump
           Device  - Path: \Device\Harddisk0\PixStream
           Device  - Path: \Device\Harddisk0\SystemURLCachePartition
           Device  - Path: \Device\Harddisk0\PhysicalDisk
           Device  - Path: \Device\Harddisk0\Cache0
           Device  - Path: \Device\Harddisk0\Cache1
           Device  - Path: \Device\Harddisk0\AltFlash
     *  1: MU
     *  - \BuiltInMuSfc\
     *  - \BuiltInMuSfcSystem\
     *  - System Cache                           O:0x00000000    S:0x7FF000
     *  - Content                                O:0x7FF000      S:EOF - 0x7FF000
     *  2: Mass:
     *  - \MassX\                         - USB Storage
     *  - \MassXPartitionFile\StorageSystem\     O:0x8000400     S:0x0000000004000000
     *  - \MassXPartitionFile\Storage\           O:0x20000000    S:Growable
    */

    public enum XCONTENTDEVICETYPE
    {
        XCONTENTDEVICETYPE_HDD = 1,
        XCONTENTDEVICETYPE_MU
    }
    public enum FATXPartitionType
    {
        NonGrowable = 0,
        Growable
    }
    public class FatxBootSectorHeader
    {
        public uint Magic;
        public uint Id;
        public uint SectorsPerCluster;
        public uint FirstCluster;

        public FatxBootSectorHeader(EndianReader reader)
        {
            this.Magic = reader.ReadUInt32();
            this.Id = reader.ReadUInt32();
            this.SectorsPerCluster = reader.ReadUInt32();
            this.FirstCluster = reader.ReadUInt32();
        }
    }
    public class FatxDirectoryEntry
    {
        [Flags]
        public enum Attribute : byte
        {
            /// <summary>
            /// Dirent is a normal file.
            /// </summary>
            Normal = 0x00,

            /// <summary>
            /// Dirent is readonly and cannot be written to.
            /// </summary>
            ReadOnly = 0x01,

            /// <summary>
            /// The dirent is hidden and cannot be seen by normal means.
            /// </summary>
            Hidden = 0x02,

            /// <summary>
            /// The dirent is an essential system file.
            /// </summary>
            System = 0x04,

            /// <summary>
            /// The dirent is a volume.
            /// </summary>
            Volume = 0x08,

            /// <summary>
            /// The dirent is a directory/folder.
            /// </summary>
            Directory = 0x10,

            /// <summary>
            /// The dirent is awaiting deletion.
            /// </summary>
            Archive = 0x20,

            /// <summary>
            /// The dirent is a device file.
            /// </summary>
            Device = 0x40,
        }

        public byte FileNameLength;
        public byte Characteristics;

        public string Filename;

        public uint FirstClusterNumber;
        public uint FileSize;

        public int CreationTimeStamp;
        public int LastAccessTimeStamp;
        public int LastWriteTimeStamp;

        public bool IsDirectory
        {
            get { return ((this.Characteristics >> 4) & 0x01) == 1; }
        }
        public bool IsValid = true;

        public string FilePath;

        public FatxDirectoryEntry() { }
        public FatxDirectoryEntry(EndianReader reader)
        {
            this.FileNameLength = reader.ReadByte();
            this.Characteristics = reader.ReadByte();

            if (FileNameLength > 0x2A || FileNameLength == 0 || (((this.Characteristics & 0xFFFFFFF8) & 0xFFFFFFCF) != 0x00))
            {
                this.IsValid = false;
                return;
            }

            byte[] fileNameBytes = reader.ReadBytes(this.FileNameLength);

            char[] fileName = new char[this.FileNameLength];

            for (int x = 0; x < 2; x++)
                if (this.FileNameLength == (x + 1) && fileNameBytes[x] == '.')
                {
                    this.IsValid = false;
                    return;
                }

            for (int x = 0; x < this.FileNameLength; x++)
            {
                var fchar = fileNameBytes[x];
                if (fchar < 0x20 || fchar > 0x7e || ((1 << (fchar & 0x1f)) & FatxDevice.FatxFatIllegalTable[((fchar >> 3) & 0x1FFFFFFC) / 4]) != 0)
                {
                    this.IsValid = false;
                    return;
                }
                fileName[x] = (char)fileNameBytes[x];
            }

            this.Filename = new string(fileName);

            reader.BaseStream.Position += 0x2a - this.FileNameLength;
            this.FirstClusterNumber = reader.ReadUInt32();
            this.FileSize = reader.ReadUInt32();
            this.CreationTimeStamp = reader.ReadInt32();
            this.LastAccessTimeStamp = reader.ReadInt32();
            this.LastWriteTimeStamp = reader.ReadInt32();
        }
        public FatxDirectoryEntry(EndianReader reader, string FilePath)
        {
            this.FileNameLength = reader.ReadByte();
            this.Characteristics = reader.ReadByte();

            if (FileNameLength > 0x2A || FileNameLength == 0 || (((this.Characteristics & 0xFFFFFFF8) & 0xFFFFFFCF) != 0x00))
            {
                this.IsValid = false;
                return;
            }

            byte[] fileNameBytes = reader.ReadBytes(this.FileNameLength);

            char[] fileName = new char[this.FileNameLength];

            for (int x = 0; x < 2; x++)
                if (this.FileNameLength == (x + 1) && fileNameBytes[x] == '.')
                {
                    this.IsValid = false;
                    return;
                }

            for (int x = 0; x < this.FileNameLength; x++)
            {
                var fchar = fileNameBytes[x];
                if (fchar < 0x20 || fchar > 0x7e || ((1 << (fchar & 0x1f)) & FatxDevice.FatxFatIllegalTable[((fchar >> 3) & 0x1FFFFFFC) / 4]) != 0)
                {
                    this.IsValid = false;
                    return;
                }
                fileName[x] = (char)fileNameBytes[x];
            }

            this.Filename = new string(fileName);

            this.FilePath = FilePath;

            reader.BaseStream.Position += 0x2a - this.FileNameLength;
            this.FirstClusterNumber = reader.ReadUInt32();
            this.FileSize = reader.ReadUInt32();
            this.CreationTimeStamp = reader.ReadInt32();
            this.LastAccessTimeStamp = reader.ReadInt32();
            this.LastWriteTimeStamp = reader.ReadInt32();
        }
        public byte[] ToArray()
        {
            MemoryStream ms = new MemoryStream();
            EndianWriter ew = new EndianWriter(ms, EndianType.BigEndian);

            ew.Write(FileNameLength);
            ew.Write((byte)Characteristics);
            ew.WriteAsciiString(Filename, Filename.Length);

            byte[] Filler = Enumerable.Repeat((byte)0xff, (0x2a - Filename.Length)).ToArray();
            if(Filler.Length > 0x00)
                ew.Write(Filler);

            ew.Write(this.FirstClusterNumber);
            ew.Write(this.FileSize);
            ew.Write(this.CreationTimeStamp);
            ew.Write(FatxDevice.FatxTimeToFatTimestamp(DateTime.Now));
            ew.Write(this.LastWriteTimeStamp);
            ew.Close();

            return ms.ToArray();
        }
    }
    public class FatxCacheEntry
    {
        public uint State;
        public uint Flags;
        public uint StartingCluster
        {
            get
            {
                return (State & 0xFFFFFFF);
            }
            set
            {
                State = (State & 0xF0000000) | (value & 0xFFFFFFF);
            }
        }
        public uint ClusterIndex
        {
            get
            {
                return (Flags & 0xFFFFFFF);
            }
            set
            {
                Flags = (Flags & 0xF0000000) | (value & 0xFFFFFFF);
            }
        }
        public int NextIndex
        {
            get
            {
                return (int)(State >> 28);
            }
            set
            {
                State = Convert.ToUInt32(((value << 28) & 0xF0000000)) | (State & 0xFFFFFFF);
            }
        }
        public int PreviousIndex
        {
            get
            {
                return (int)(Flags >> 28);
            }
            set
            {
                Flags = Convert.ToUInt32(((value << 28) & 0xF0000000)) | (Flags & 0xFFFFFFF);
            }
        }
        public uint ContiguousClusters;

        public FatxCacheEntry() { }
    }
    public class FatxFcb
    {
        public string FileName;
        public byte Flags;
        public byte CacheHeadIndex;

        public int CreationTimeStamp;
        public DateTime ActiveTimeStamp;
        public int LastWriteTimeStamp;

        public uint FileSize;
        public uint EndOfFile;
        public uint DirectoryEntryByteOffset;
        public uint FirstCluster;
        public uint LastCluster;

        public byte State;
        public byte Characteristics;
        public uint ByteOffset;

        public int ReferenceCount;

        public List<FatxCacheEntry> Cache;

        public FatxFcb ParentFCB;
        public FatxFcb CloneFCB;

        public string FullFileName;

        public bool IsDirectory
        {
            get { return (State & 0x02) != 0; }
        }
        public bool IsRootDir
        {
            get { return (State & 0x04) != 0; }
        }
        public bool IsMarkedForDeletion
        {
            get { return (State & 0x10) != 0; }
        }
        public bool IsModified
        {
            get { return (State & 0x20) != 0; }
        }

        /* States
         * 0x01 - Is Title-Owned
         * 0x02 - Is Folder/Directory
         * 0x04 - Is Root Directory FCB
         * 0x10 - Marked For Deletion
         * 0x20 - Is Modified
        */

        public FatxFcb() { }
        public FatxFcb(FatxDirectoryEntry DirectoryLookup, FatxFcb ParentFcb, uint DirectoryEntryByteOffset)
        {
            if (DirectoryLookup.IsDirectory)
            {
                this.State |= 0x02;
            }

            this.DirectoryEntryByteOffset = DirectoryEntryByteOffset;

            this.Flags = DirectoryLookup.Characteristics;
            this.CreationTimeStamp = DirectoryLookup.CreationTimeStamp;
            this.LastWriteTimeStamp = DirectoryLookup.LastAccessTimeStamp;

            if (DirectoryLookup.FileNameLength > 0x2A || DirectoryLookup.FileNameLength == 0)
            {
                throw new FatxException(string.Format("detected an invalid filename length on directory entry {0}.", DirectoryLookup.Filename));
            }

            this.FileSize = DirectoryLookup.FileSize;

            try
            {
                this.ActiveTimeStamp = FatxDevice.FatxFatTimestampToTime(DirectoryLookup.LastAccessTimeStamp);
            }
            catch
            {
                this.ActiveTimeStamp = DateTime.Now;
            }

            this.FileName = DirectoryLookup.Filename;

            this.FirstCluster = DirectoryLookup.FirstClusterNumber;

            if (this.FirstCluster != 0)
            {
                this.EndOfFile = 0xffffffff;
            }
            else
            {
                if (this.EndOfFile != 0x00)
                {
                    throw new FatxException("Attempted to create an FCB with an invalid directory entry.");
                }
            }

            this.ParentFCB = ParentFcb;
            this.Characteristics = DirectoryLookup.Characteristics;
        }
    }
    public class FatxAllocationState
    {
        public class AllocationState
        {
            public uint FirstAllocatedCluster;
            public uint ContiguousClusters;

            public AllocationState() { }
        }
        public List<AllocationState> AllocationStates;

        public FatxAllocationState()
        {
            this.AllocationStates = new List<AllocationState>(0xA);
        }
    }
    public class FatxVolumeExtension
    {
        public readonly byte SectorShift = 0x09;
        public byte ClusterShift;
        public byte ChainShift;
        public long BackingClusterOffset;
        public long BootSectorSize;
        public uint ClusterSize;
        public uint ClusterCount;

        public uint LastFreeClusterIndex;
        public uint FreedClusterCount;

        public byte AllocationShift;
        public byte[] AllocationSupport;

        public static uint SectorSize = 0x200;

        public FatxVolumeExtension()
        {

        }
    }
    public enum FatxIO
    {
        Read = 2,
        Write
    }
}