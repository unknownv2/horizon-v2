using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using XContent;

namespace ODFX
{   
    //public class 
    public class OdfxDevice
    {
        private OdfxType VolumeType;
        private GdfDevice GdfDevice;
        public OdfxFcb DirectoryFcb;
        public List<OdfxFcb> Fcbs;

        public uint LastBlockNumber;
        public uint PageSize;
        public byte PageShift;
        public int Flags;
        public uint DataStartBlockModifier;

        private static string OdfxGdfVolumeDescriptorSignature = "MICROSOFT*XBOX*MEDIA";

        public OdfxDevice(GdfDevice gdfDevice, OdfxType volumeType) 
        {
            if (gdfDevice != null)
            {
                this.GdfDevice = gdfDevice;
                this.VolumeType = volumeType;
            }
        }

        private void ValidateGdfxSignature(string Signature)
        {
            if (Signature != OdfxGdfVolumeDescriptorSignature)
            {
                throw new SvodException("invalid GDFX volume descriptor signature [0xC000014F].");
            }
        }

        public void OdfxMountVolume()
        {
            byte[] VolumeInfo = new byte[0x08];
            this.IoSynchronousDeviceIoControlRequest(0x2404C, ref VolumeInfo);
            this.OdfxCreateVolumeDevice(VolumeInfo);
            this.OdfxGdfMountVolume();

            this.DataStartBlockModifier = 0x00;
        }

        private void OdfxCreateVolumeDevice(byte[] VolumeInfo)
        {
            this.PageSize = VolumeInfo.ReadUInt32(0x04);
            this.PageShift = (byte)(0x1F - Horizon.Library.Systems.FATX.FatxDevice.CountLeadingZerosWord(PageSize));

            long mask = ~(long)(this.PageSize - 1);
            this.LastBlockNumber = (uint)(((((long)this.PageSize * (long)VolumeInfo.ReadInt32(0x00) & mask)) >> this.PageShift) & 0xFFFFFFFF);
        }

        private void OdfxGdfMountVolume()
        {
            if (this.PageSize != 0x800)
                throw new OdfxException("invalid page size detected while mounting volume [0xC000014F].");

            var reader = new EndianReader(this.FscMapBuffer(0x10000, 0x1000), EndianType.LittleEndian);
            this.ValidateGdfxSignature(reader.ReadAsciiString(0x00, 0x14));
            this.ValidateGdfxSignature(reader.ReadAsciiString(0x7EC, 0x14));

            reader.SeekTo(0x14);
            this.DirectoryFcb = new OdfxFcb(0x05);
            this.DirectoryFcb.FirstBlockNumber = reader.ReadUInt32();
            this.DirectoryFcb.FileSize = reader.ReadInt32();

            this.Fcbs = new List<OdfxFcb>();
            this.Flags = 0x11;
        }

        private void OdfxValidateFileExtent(uint BlockNumber, uint FileSize)
        {
            uint remainderBlocks = this.LastBlockNumber - BlockNumber;
            uint fileExt = (((this.PageSize + FileSize) - 1) & ~(this.PageSize - 1)) >> this.PageShift;
            if ((BlockNumber >= this.LastBlockNumber) || (remainderBlocks < fileExt))
                throw new OdfxException("invalid file extent [0xC0000032].");
        }

        private OdfxFcb OdfxGdfCreateFcb(OdfxGdfDirectoryEntry DirectoryEntry, OdfxFcb ParentFcb)
        {
            OdfxFcb Fcb = this.Fcbs.Find(delegate(OdfxFcb fcb)
            {
                return fcb.Filename == DirectoryEntry.FileName && fcb.FirstBlockNumber == DirectoryEntry.FirstBlockNumber && fcb.FileSize == DirectoryEntry.FileSize;
            });
            if (Fcb != null)
            {
                return Fcb;
            }
            else
            {
                Fcb = new OdfxFcb(DirectoryEntry, ParentFcb);
                this.OdfxConnectChildFcb(Fcb, ParentFcb);
                Fcbs.Add(Fcb);
                return Fcb;
            };
        }

        private OdfxGdfDirectoryEntry OdfxGdfLookupElementNameInDirectory(OdfxFcb ParentFcb, string FileName)
        {
            if (!ParentFcb.IsDirectory)
                throw new OdfxException(string.Format("could not open the file control block while searching for in the directory '{0' because it is not a valid directory file", FileName));
            if (ParentFcb.BlockLength == 0x00)
                throw new OdfxException("invalid directory block length detected [0xC0000034].");

            byte[] buffer = null;
            int blockIdx = 0x00, blockMod = 0x00;
            EndianReader reader = null;
            do
            {
                if (buffer == null || (blockMod != (blockIdx & 0xFFFFF800)))
                {
                    buffer = this.FscMapBuffer(Horizon.Functions.Global.ROTL64(ParentFcb.FirstBlockNumber, 11) + (blockMod = (int)(blockIdx & 0xFFFFF800)), 0x1000);
                    reader = new EndianReader(buffer, EndianType.LittleEndian);
                }
                int directoryBlockOffset = blockIdx & 0x7FF;

                if (directoryBlockOffset > 0x7F2 || (directoryBlockOffset + 0x0D) > 0x7F2)
                    throw new OdfxException("found invalid directory byte offset [0xC0000032].");
                reader.SeekTo(directoryBlockOffset + 0x0D);
                byte fileNameLength = reader.ReadByte();
                int strRet = string.Compare(reader.ReadAsciiString(fileNameLength), FileName, true);
                if (strRet == 0x00)
                {
                    OdfxGdfDirectoryEntry directoryEntry;
                    reader.SeekTo(directoryBlockOffset + 0x4);
                    directoryEntry.FirstBlockNumber = reader.ReadUInt32();
                    directoryEntry.FileSize = reader.ReadUInt32();
                    directoryEntry.Flags = reader.ReadByte();
                    directoryEntry.FileName = reader.ReadAsciiString(reader.ReadByte());

                    this.OdfxValidateFileExtent(directoryEntry.FirstBlockNumber, directoryEntry.FileSize);

                    return directoryEntry;
                }

                int nextEntryOffset = strRet >= 0x00 ? reader.ReadInt16(directoryBlockOffset) : reader.ReadInt16(directoryBlockOffset + 0x02);
                blockIdx = Horizon.Functions.Global.ROTL32(nextEntryOffset, 2);
                if (blockIdx == 0x00 || (blockIdx < (directoryBlockOffset + blockMod)))
                    throw new OdfxException("found invalid directory byte offset while searching for the next entry [0xC0000034].");
                if(blockIdx >= ParentFcb.BlockLength)
                    throw new OdfxException("found invalid directory byte offset while searching a folder [0xC0000032].");
            } while (true);
        }

        public OdfxFcb OdfxGdfFindUnopenedChildFcb(OdfxFcb ParentFcb, string FileName)
        {
            if (!ParentFcb.IsDirectory)
                throw new OdfxException(string.Format("could not open the file control block while opening '{0}' because it is not a valid directory file", FileName));

            var Entry = this.OdfxGdfLookupElementNameInDirectory(ParentFcb, FileName);

            OdfxFcb fcb = this.OdfxGdfCreateFcb(Entry, ParentFcb);
            if (fcb == null)
            {
                throw new OdfxException(string.Format("could not create a new file control block for file {0} [0xC000009A].", FileName));
            }
            return fcb;
        }

        private void OdfxConnectChildFcb(OdfxFcb Child, OdfxFcb Parent)
        {
            Parent.Referenced++;
        }

        public void OdfxReferenceFcb(OdfxFcb Fcb)
        {
            if (Fcb != null)
            {
                Fcb.Referenced++;
            }
        }

        public void OdfxDereferenceFcb(OdfxFcb Fcb)
        {
            Fcb.Referenced--;
        }

        private byte[] FscMapBuffer(long PhysicalOffset, long DataSize)
        {
            var IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            if (VolumeType == OdfxType.SVOD)
            {
                long LowOffset = PhysicalOffset & 0xFFF;
                PhysicalOffset = Horizon.Functions.Global.ROTL64((PhysicalOffset >> 0x0C) & 0xFFFFFFFF, 0x0C) & 0xFFFFFFFF000;
                this.GdfDevice.Read(IO, DataSize + LowOffset, PhysicalOffset);
                if (LowOffset != 0x00)
                {
                    IO.In.SeekTo(LowOffset);
                    return IO.In.ReadBytes(DataSize);
                }
            }
            else if (VolumeType == OdfxType.ODD)
            {
                
            }

            return IO.ToArray();
        }

        private void IoSynchronousDeviceIoControlRequest(ulong ControlCode, ref byte[] OutputBuffer)
        {
            this.GdfDevice.DeviceControl(ControlCode, ref OutputBuffer);            
        }

        public byte[] IoCallDriver(long FileOffset, long NumberOfBytesToRead)
        {
            return this.FscMapBuffer(FileOffset, NumberOfBytesToRead);;
        }

        // Public File Functions
        public EndianIO CreateFile(string FileName)
        {
            return new EndianIO(new OdfxFileStream(this, FileName), EndianType.BigEndian, true);
        }
        public byte[] ExtractFileToArray(string FileName)
        {
            return new OdfxFileStream(this, FileName).ToArray();
        }
    }

    public class OdfxFileStream : Stream
    {
        private OdfxDevice Device;
        private OdfxFcb Fcb;

        public override bool CanSeek { get { return true; } }
        public override bool CanRead { get { return true; } }
        public override bool CanWrite { get { return false; } }
        public override long Length { get { return Fcb.FileSize; } }

        private long _position = 0;

        public override long Position
        {
            get { return _position; }
            set
            {
                _position = Seek(value, SeekOrigin.Begin);
            }
        }

        public OdfxFileStream(OdfxDevice OdfxDevice, string Filename)
        {
            if (OdfxDevice != null && Filename != string.Empty)
            {
                this.Device = OdfxDevice;

                OdfxFcb parentFcb = this.Device.DirectoryFcb;
                var Name = StfsDevice.BreakStringByPath(Filename);
                this.Device.OdfxReferenceFcb(parentFcb);

                for (int x = 0; x < Name.Length; x++)
                {
                    this.Fcb = this.Device.OdfxGdfFindUnopenedChildFcb(parentFcb, Name[x]);

                    this.Device.OdfxDereferenceFcb(parentFcb);

                    parentFcb = (x != (Name.Length - 1)) ? this.Fcb : parentFcb;
                }
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    _position = offset;
                    break;
                case SeekOrigin.Current:
                    _position += offset;
                    break;
                case SeekOrigin.End:
                    _position = this.Fcb.FileSize - offset;
                    break;
            }

            return _position;
        }
        public override int Read(byte[] buffer, int offset, int count)
        {
            int len = count;
            if (len != 0x00)
            {
                uint numberOfBytesToRead = (uint)count;
                if (this.Fcb != null)
                {
                    if (this.Fcb.IsDirectory)
                        throw new OdfxException("attempted to read a folder as a file [0xC0000010].");
                    else if (_position >= this.Fcb.FileSize)
                        throw new OdfxException("reached end-of-file [0xC0000011].");

                    numberOfBytesToRead = ((len & 0xFFFFFFFF) >= (Fcb.FileSize - _position)) ? (uint)(Fcb.FileSize - _position) & 0xFFFFFFFF : numberOfBytesToRead;

                    if ((Fcb.Flags & 0x10) == 0x00)
                    {
                        if (numberOfBytesToRead == 0x00)
                            throw new OdfxException("invalid page size detected while reading.");
                        if(((Device.Flags & 0x01) != 0x00 || (Fcb == null)))
                        {
                            if (Fcb != null)
                            {
                                long FileOffset = ((((Fcb.FirstBlockNumber + Device.DataStartBlockModifier) & 0xFFFFFFFF) << Device.PageShift) + _position);
                                uint bytesToRead = ((Device.PageSize + numberOfBytesToRead) - 1) & ~(Device.PageSize - 1);

                                byte[] data = this.Device.IoCallDriver(FileOffset, bytesToRead);
                                Array.Copy(data, 0x00, buffer, offset, len);
                                _position += len;
                            }
                        }
                    }
                }
            }
            return len;
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new Exception("Not implemented.");
        }
        public override void Flush()
        {
            throw new Exception("Not implemented.");
        }
        public override void SetLength(long value)
        {
            throw new Exception("Not implemented.");
        }

        public override void Close()
        {
            if (this.Fcb.Referenced != 0x00)
            {
                this.Device.OdfxDereferenceFcb(this.Fcb);
            }
        }

        public byte[] ToArray()
        {
            // create a byte array to hold the file
            byte[] _buffer = new byte[Fcb.FileSize];
            // read the file's data into a buffer
            Read(_buffer, 0, _buffer.Length);
            // return a buffer containing the file
            return _buffer;
        }
    }
}