using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ODFX
{
    public enum OdfxType
    {
        SVOD,
        ODD
    }

    public class OdfxFcb
    {
        public byte Flags = 0x00;
        public bool IsDirectory
        {
            get
            {
                return (Flags & 0x1) != 0x00;
            }
            set
            {
                Flags = (byte)((Flags & ~0x01) | Convert.ToByte(value));
            }
        }
        public int Referenced;
        public uint FirstBlockNumber;
        public long BlockLength
        {
            get
            {
                return (FileSize & 0xFFFFFFFF);
            }
        }
        public string Filename;
        public long FileSize;

        public byte FilenameLength
        {
            get
            {
                if (Filename != null)
                    return (byte)(Filename.Length & 0xFF);
                else
                    return 0x00;
            }
        }

        public OdfxFcb(byte bFlags)
        {
            Flags = bFlags;
        }

        public OdfxFcb(OdfxGdfDirectoryEntry DirectoryEntry, OdfxFcb ParentFcb)
        {
            this.Referenced = 0x01;
            this.Filename = DirectoryEntry.FileName;
            this.FirstBlockNumber = DirectoryEntry.FirstBlockNumber;
            this.FileSize = DirectoryEntry.FileSize;
            int flags = ParentFcb.Flags & 0x04;

            if (DirectoryEntry.IsDirectory)
            {
                flags |= 0x01;
            }
            else
            {
                if((DirectoryEntry.Flags & 0x40) != 0x00)
                    flags |= 0x20;
            }

            this.Flags = (byte)(flags & 0xFF);
        }
    }

    public struct OdfxGdfDirectoryEntry
    {
        public string FileName;
        public uint FirstBlockNumber;
        public uint FileSize;
        public byte Flags;

        public bool IsDirectory
        {
            get
            {
                return (Flags & 0x10) != 0x00;
            }
        }
    }

    public abstract class GdfDevice
    {
        public abstract void DeviceControl(ulong ControlCode, ref byte[] OutputBuffer);
        public abstract void Read(EndianIO ReadIO, long Length, long FileByteOffset);
    }
}