using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Horizon.Functions;


namespace Horizon.Library.Systems.FATX
{
    public class FatxDevice
    {
        public volatile EndianIO IO;

        public FatxFcb DirectoryFcb;
        public FatxVolumeExtension VolumeExtension;

        protected FATXPartitionType PartitionType;
        protected FatxBootSectorHeader BootSectorHeader;

        protected List<FatxFcb> Fcbs;

        public long DeviceSize;
        public long DeviceOffset;

        public static uint[] FatxFatIllegalTable = new uint[4] { 0xFFFFFFFF, 0xFC009C04, 0x10000000, 0x10000000 };

        public FatxDevice(EndianIO IO, FATXPartitionType PartitionType, long DeviceOffset, long DeviceSize)
        {
            this.IO = IO;

            this.PartitionType = PartitionType;

            this.DeviceOffset = DeviceOffset;
            this.DeviceSize = DeviceSize;
        }

        public void FatxMountVolume()
        {
            if (!this.IO.Opened)
            {
                this.IO.Open();
            }

            this.Fcbs = new List<FatxFcb>();

            this.VolumeExtension = new FatxVolumeExtension();

            this.FatxCreateVolumeDevice();

            this.FatxProcessBootSector(this.PartitionType);

            this.FatxInitializeAllocationSupport();
        }

        private void FatxCreateVolumeDevice()
        {
            this.DirectoryFcb = new FatxFcb();
            this.DirectoryFcb.State = 0x06;
            this.DirectoryFcb.Flags = 0x03;
            this.DirectoryFcb.EndOfFile = 0xFFFFFFFF;

            this.FatxResetClusterCache(this.DirectoryFcb);
        }

        private void FatxProcessBootSector(FATXPartitionType PartitionType)
        {
            this.IO.SeekTo(this.DeviceOffset);

            this.BootSectorHeader = new FatxBootSectorHeader(this.IO.In);

            uint NewSectors = RoundToBlock(BootSectorHeader.SectorsPerCluster), BootSectorSize = 0x1000;

            if (NewSectors > 0x1000)
            {
                BootSectorSize = NewSectors;
            }

            if (BootSectorSize > DeviceSize)
            {
                throw new FatxException("boot sector size was greater than device size [0xC000014F].");
            }

            this.VolumeExtension.ClusterSize = BootSectorHeader.SectorsPerCluster << this.VolumeExtension.SectorShift;

            this.VolumeExtension.ClusterShift = this.VolumeExtension.ClusterSize != 0 ? (byte)(0x1F - CountLeadingZerosWord(this.VolumeExtension.ClusterSize)) : this.VolumeExtension.ClusterShift;

            if (PartitionType == FATXPartitionType.Growable)
            {
                if (this.VolumeExtension.ClusterSize != 0x4000)
                {
                    throw new FatxException("invalid bytes per cluster for growable file partition.");
                }
            }

            if (this.VolumeExtension.ClusterSize < BootSectorSize)
            {
                throw new FatxException("found too small of cluster size.");
            }

            long ClusterCount = (DeviceSize >> this.VolumeExtension.ClusterShift) + 0x1;

            if (ClusterCount >= 0x0FFF0 || PartitionType == FATXPartitionType.Growable)
            {
                this.VolumeExtension.ChainShift = 0x02;
                ClusterCount <<= 0x02;
            }
            else
            {
                this.VolumeExtension.ChainShift = 0x01;
                ClusterCount <<= 0x01;
            }

            ClusterCount = ((ClusterCount + BootSectorSize) - 1) & ~(BootSectorSize - 1) & 0xFFFFFFFF;

            this.VolumeExtension.ClusterCount = (uint)(((DeviceSize - BootSectorSize) - ClusterCount) >> this.VolumeExtension.ClusterShift);
            this.VolumeExtension.BackingClusterOffset = ClusterCount + (BootSectorSize & 0xFFFFFFFF);
            this.VolumeExtension.BootSectorSize = BootSectorSize;

            if ((BootSectorSize & 0xFFF) != 0)
            {
                throw new FatxException(string.Format("invalid boot sector length detected 0x{0:X8}.", BootSectorSize));
            }
            else if ((this.VolumeExtension.BackingClusterOffset & 0xFFF) != 0)
            {
                throw new FatxException(string.Format("invalid backing cluster offset length detected 0x{0:X16}.", this.VolumeExtension.BackingClusterOffset));
            }
            else if ((this.VolumeExtension.ClusterSize & 0xFFF) != 0)
            {
                throw new FatxException(string.Format("invalid cluster size detected 0x{0:X8}.", this.VolumeExtension.ClusterSize));
            }

            this.DirectoryFcb.FirstCluster = BootSectorHeader.FirstCluster;
        }

        private void FatxInitializeAllocationSupport()
        {
            if ((this.VolumeExtension.BootSectorSize & 0xFFF) != 0x00)
            {
                throw new FatxException("detected an invalid boot sector size.");
            }

            uint RealClusterCount = this.VolumeExtension.ClusterCount + 1;

            if (RealClusterCount > 0x40000)
            {
                uint AllocationShift = (uint)((1 << (0x1F - CountLeadingZerosWord((RealClusterCount << 1) - 1))) >> 11);
                this.VolumeExtension.AllocationShift = (byte)(0x1F - CountLeadingZerosWord(AllocationShift < 0x400 ? 0x400 : AllocationShift));
                this.VolumeExtension.AllocationSupport = new byte[0x100];
            }

            uint ChainMapOffset = 0, LastFreeClusterIndex = 0xFFFFFFFF, FreedClusterCount = 0x00, RemainderOfChainMap = RealClusterCount << this.VolumeExtension.ChainShift,
                MappedBufferSize = 0x0000000000001000;

            do
            {
                if ((ChainMapOffset & 0xFFF) != 0x00)
                {
                    throw new FatxException("detected an invalid cluster offset.");
                }

                MappedBufferSize = MappedBufferSize > RemainderOfChainMap ? RemainderOfChainMap : MappedBufferSize;

                var reader = new EndianReader(this.FscMapBuffer(this.VolumeExtension.BootSectorSize + ChainMapOffset, MappedBufferSize, false), EndianType.BigEndian);

                uint CurrentChainMapLocation = 0x00;

                if (MappedBufferSize > RemainderOfChainMap)
                {
                    throw new FatxException("Buffer was larger than the maximum size.");
                }

                uint ChainMapBlockSize = MappedBufferSize, StaticRemainderChainMapBlockSize = 0x1000;
                do
                {
                    uint ReadClusterCount = FreedClusterCount;

                    StaticRemainderChainMapBlockSize = ChainMapBlockSize < 0x1000 ? ChainMapBlockSize : StaticRemainderChainMapBlockSize;

                    uint RealRemainderCMBS = StaticRemainderChainMapBlockSize;

                    if (this.VolumeExtension.ChainShift == 1)
                    {
                        CurrentChainMapLocation = ChainMapOffset == 0 ? 2 : CurrentChainMapLocation;

                        if (CurrentChainMapLocation < RealRemainderCMBS)
                        {
                            reader.BaseStream.Position = CurrentChainMapLocation;
                            do
                            {
                                uint ClusterNum = reader.ReadUInt16();
                                if (ClusterNum == 0)
                                {
                                    LastFreeClusterIndex = LastFreeClusterIndex == 0xFFFFFFFF ? ((ChainMapOffset >> 1) + (CurrentChainMapLocation >> 1)) : LastFreeClusterIndex;
                                    FreedClusterCount++;
                                }

                            } while ((CurrentChainMapLocation += 2) < RealRemainderCMBS);
                        }
                    }
                    else
                    {
                        CurrentChainMapLocation = ChainMapOffset == 0 ? (uint)4 : 0;

                        if (CurrentChainMapLocation < RealRemainderCMBS)
                        {
                            reader.BaseStream.Position = CurrentChainMapLocation;
                            do
                            {
                                uint ClusterNum = reader.ReadUInt32();
                                if (ClusterNum == 0)
                                {
                                    LastFreeClusterIndex = LastFreeClusterIndex == 0xFFFFFFFF ? ((ChainMapOffset >> 2) + (CurrentChainMapLocation >> 2)) : LastFreeClusterIndex;
                                    FreedClusterCount++;
                                }

                            } while ((CurrentChainMapLocation += 4) < RealRemainderCMBS);
                        }
                    }
                    if (ReadClusterCount != FreedClusterCount)
                    {
                        this.FatxMarkFreeClusterRange(ChainMapOffset);
                    }

                    ChainMapOffset += StaticRemainderChainMapBlockSize;

                } while ((ChainMapBlockSize -= StaticRemainderChainMapBlockSize) != 0);

            } while ((RemainderOfChainMap -= MappedBufferSize) != 0);

            this.VolumeExtension.FreedClusterCount = FreedClusterCount;
            this.VolumeExtension.LastFreeClusterIndex = LastFreeClusterIndex;
        }

        public FatxFcb FatxCreateFcb(FatxDirectoryEntry Entry, FatxFcb ParentFcb, uint DirectoryEntryByteOffset, string FilePath)
        {
            FatxFcb fcb = this.Fcbs.Find(delegate(FatxFcb FCB)
            {
                return FCB.FullFileName == FilePath && (Entry.IsDirectory == FCB.IsDirectory);
            });
            if (fcb != null)
            {
                return fcb;
            }
            else
            {
                fcb = new FatxFcb(Entry, ParentFcb, DirectoryEntryByteOffset);

                this.FatxReferenceFcb(fcb);

                this.FatxResetClusterCache(fcb);

                fcb.FullFileName = FilePath;

                this.Fcbs.Add(fcb);

                return fcb;
            }
        }

        public void FatxReferenceFcb(FatxFcb Fcb)
        {
            Fcb.ReferenceCount++;
        }

        public void FatxDereferenceFcb(FatxFcb Fcb)
        {
            if (Fcb.ReferenceCount <= 0)
            {
                throw new FatxException("attempted to de-reference an FCB with zero references.");
            }

            FatxFcb parentFcb = null;

            do
            {
                Fcb.ReferenceCount--;

                if (Fcb.ReferenceCount == 0)
                {
                    if (Fcb.ParentFCB != null)
                    {

                    }

                    if (Fcb.CloneFCB != null)
                    {
                        Fcb.CloneFCB = null;
                    }

                    parentFcb = Fcb.ParentFCB;

                    this.Fcbs.Remove(Fcb);

                    Fcb = parentFcb;
                }
                else
                {
                    break;
                }

            } while (Fcb != null);
        }

        public FatxFcb FatxCreateNewFile(FatxFcb ParentFcb, string FileName, string FullName, FatxDirectoryEntry.Attribute Characteristics, uint FileSize, uint FreeDirEntryOffset)
        {
            if (!ParentFcb.IsDirectory)
            {
                throw new FatxException("parent FCB for a new file is not a directory.");
            }

            if (FreeDirEntryOffset == 0xFFFFFFFF)
            {
                if (ParentFcb.EndOfFile == 0xFFFFFFFF)
                {
                    throw new FatxException("invalid end-of-file found.");
                }

                FreeDirEntryOffset = ParentFcb.EndOfFile;
                this.FatxExtendDirectoryAllocation(ParentFcb);
            }

            uint LastAllocated = 0, TotalAllocated = 0, FirstAllocated = 0x00, EndOfFile = 0x00;
            FatxAllocationState AllocationState = null;

            if (Characteristics == FatxDirectoryEntry.Attribute.Directory)
            {
                this.FatxAllocateClusters(0x00, 0x01, out AllocationState, out TotalAllocated, out LastAllocated);

                if (TotalAllocated != 0x01)
                {
                    throw new FatxException("the cluster allocation function returned an invalid allocated cluster count.");
                }

                FirstAllocated = AllocationState.AllocationStates[0].FirstAllocatedCluster;
                EndOfFile = this.VolumeExtension.ClusterSize;
                this.FatxInitializeDirectoryCluster(FirstAllocated);
            }
            else
            {
                if (FileSize != 0x00)
                {
                    EndOfFile = (((this.VolumeExtension.ClusterSize + FileSize) - 1) & ~(this.VolumeExtension.ClusterSize - 1));
                    this.FatxAllocateClusters(0x00, (EndOfFile >> this.VolumeExtension.ClusterShift), out AllocationState, out TotalAllocated, out LastAllocated);
                    FirstAllocated = AllocationState.AllocationStates[0].FirstAllocatedCluster;
                    if (EndOfFile > 0xFFFFFFFF)
                    {
                        EndOfFile = 0xFFFFFFFF;
                    }
                }
                else
                {
                    LastAllocated = 0x00;
                    TotalAllocated = 0x00;
                }
            }

            var DirectoryEntry = new FatxDirectoryEntry();

            DirectoryEntry.FileNameLength = Convert.ToByte(FileName.Length);
            DirectoryEntry.Filename = FileName;
            DirectoryEntry.Characteristics = Convert.ToByte(Characteristics);

            DirectoryEntry.CreationTimeStamp = FatxTimeToFatTimestamp(DateTime.Now);
            DirectoryEntry.LastAccessTimeStamp = DirectoryEntry.CreationTimeStamp;
            DirectoryEntry.LastWriteTimeStamp = DirectoryEntry.CreationTimeStamp;

            DirectoryEntry.FirstClusterNumber = FirstAllocated;

            FatxFcb Fcb = this.FatxCreateFcb(DirectoryEntry, ParentFcb, FreeDirEntryOffset, FullName);

            this.FatxUpdateDirectoryEntry(Fcb);

            if (EndOfFile != 0x00)
            {
                if (Fcb.EndOfFile != 0xffffffff)
                {
                    throw new FatxException(string.Format("invalid end-of-file for file size {0:X8}.", FileSize));
                }

                Fcb.EndOfFile = EndOfFile;
                Fcb.LastCluster = LastAllocated;

                this.FatxAppendClusterRunsToClusterCache(Fcb, 0x00, AllocationState, TotalAllocated);
            }
            else
            {
                if (Fcb.EndOfFile != 0x00)
                {
                    throw new FatxException("invalid end-of-file for file size 0x00000000.");
                }
                else if (Fcb.LastCluster != 0x00)
                {
                    throw new FatxException("invalid cluster for the end-of-file found.");
                }
            }

            return Fcb;
        }

        private void FatxOverwriteExistingFile(FatxFcb Fcb, object FileSize)
        {
            if (Fcb.IsDirectory)
            {
                throw new FatxException("attempted to overwrite a directory.");
            }
            else if (Fcb.ReferenceCount >= 0x02)
            {
                throw new FatxException("invalid reference count found.");
            }

            Fcb.State = (byte)((Fcb.State & 0xD7) | 0x20);
            Fcb.ActiveTimeStamp = DateTime.Now;
            Fcb.CreationTimeStamp = FatxTimeToFatTimestamp(Fcb.ActiveTimeStamp);
            Fcb.LastWriteTimeStamp = Fcb.CreationTimeStamp;
            Fcb.FileSize = 0x00;

            this.FatxSetAllocationSize(Fcb, Convert.ToUInt32(FileSize), true, false);

            if ((Fcb.State & 0x20) != 0x00)
            {
                this.FatxUpdateDirectoryEntry(Fcb);
            }
        }

        public void FatxSetAllocationSize(FatxFcb Fcb, uint FileSize, bool DeleteFile, bool DisableTruncation)
        {
            uint RoundedSize = ((this.VolumeExtension.ClusterSize + FileSize) - 1) & ~(this.VolumeExtension.ClusterSize - 1);

            if (RoundedSize != 0x00)
            {
                if (Fcb.EndOfFile == 0xFFFFFFFF)
                {
                    if (Fcb.LastCluster != 0x00)
                    {
                        throw new FatxException(string.Format("invalid last cluster detected while setting a new allocation size for {0}.", Fcb.FileName));
                    }

                    uint Cluster, ContinuousClusterCount;

                    uint dwReturn = this.FatxFileByteOffsetToCluster(Fcb, 0xFFFFFFFF, out Cluster, out ContinuousClusterCount);

                    if (dwReturn == 0xC0000102)
                    {
                        if (DeleteFile)
                        {
                            this.FatxDeleteFileAllocation(Fcb);
                        }
                    }
                }
                if (Fcb.EndOfFile < RoundedSize)
                {
                    this.FatxExtendFileAllocation(Fcb, RoundedSize);
                }
                else
                {
                    if (!DisableTruncation && (Fcb.EndOfFile > RoundedSize))
                    {
                        this.FatxTruncateFileAllocation(Fcb, RoundedSize);
                    }
                }
            }
            else
            {
                this.FatxDeleteFileAllocation(Fcb);
            }
        }

        public void FatxExtendFileAllocation(FatxFcb Fcb, object FileSize)
        {
            uint Filesize = (uint)(FileSize);

            if (Filesize == 0x00 || ((this.VolumeExtension.ClusterSize - 1) & Filesize) != 0x0)
            {
                throw new FatxException(string.Format("invalid extension length detected for {0}.", Fcb.FileName));
            }
            else if (Fcb.EndOfFile == 0xFFFFFFFF || Fcb.EndOfFile >= Filesize)
            {
                throw new FatxException("found an invalid FCB while extending the file's allocation.");
            }
            else if (Fcb.IsDirectory)
            {
                throw new FatxException("attempted to extended a directory instead of a file.");
            }
            else if (Fcb.EndOfFile != 0x00)
            {
                if (Fcb.FirstCluster == 0x00 || Fcb.LastCluster == 0x00)
                {
                    throw new FatxException("found an invalid FCB while extending the file's allocation.");
                }
            }

            uint LastAllocated = 0, TotalAllocated = 0;
            FatxAllocationState AllocationState = null;

            this.FatxAllocateClusters(Fcb.LastCluster, (Filesize - Fcb.EndOfFile) >> this.VolumeExtension.ClusterShift, out AllocationState, out TotalAllocated, out LastAllocated);

            if (Fcb.FirstCluster == 0x00)
            {
                Fcb.FirstCluster = AllocationState.AllocationStates[0].FirstAllocatedCluster;

                this.FatxUpdateDirectoryEntry(Fcb);
            }

            Fcb.LastCluster = LastAllocated;

            this.FatxAppendClusterRunsToClusterCache(Fcb, (Fcb.EndOfFile >> this.VolumeExtension.ClusterShift), AllocationState, TotalAllocated);

            Fcb.EndOfFile = Filesize;
        }

        private void FatxTruncateFileAllocation(FatxFcb Fcb, uint FileSize)
        {
            if (FileSize == 0x00 || ((this.VolumeExtension.ClusterSize - 1) & FileSize) != 0x00)
            {
                throw new FatxException("detected an invalid truncation size.");
            }
            else if (Fcb.EndOfFile == 0xFFFFFFFF || Fcb.EndOfFile <= FileSize || Fcb.IsDirectory)
            {
                throw new FatxException("invalid FCB detected.");
            }

            if (Fcb.EndOfFile != 0x00)
            {
                if (Fcb.FirstCluster == 0x00 || Fcb.LastCluster == 0x00)
                {
                    throw new FatxException("detected an invalid starting or final cluster.");
                }
            }

            uint ReturnedCluster, ContinuousClusterCount;

            uint dwReturn = this.FatxFileByteOffsetToCluster(Fcb, FileSize - 1, out ReturnedCluster, out ContinuousClusterCount);

            dwReturn = dwReturn == 0xC0000011 ? 0xC0000102 : dwReturn;

            if (dwReturn == 0x00)
            {
                if (Fcb.FileSize > FileSize)
                {
                    Fcb.FileSize = FileSize;

                    this.FatxUpdateDirectoryEntry(Fcb);
                }

                this.FatxFreeClusters(ReturnedCluster, true);

                Fcb.EndOfFile = FileSize;
                Fcb.LastCluster = ReturnedCluster;

                this.FatxInvalidateClusterCache(Fcb, ReturnedCluster);
            }
        }

        private void FatxDeleteFileAllocation(FatxFcb Fcb)
        {
            if (Fcb.IsDirectory)
            {
                throw new FatxException("attempted to delete allocation for a directory.");
            }

            if (Fcb.FirstCluster != 0x00 || Fcb.FileSize != 0x00)
            {
                uint FirstCluster = Fcb.FirstCluster;

                Fcb.FileSize = 0x00;
                Fcb.FirstCluster = 0x00;

                this.FatxUpdateDirectoryEntry(Fcb);

                this.FatxFreeClusters(FirstCluster, false);

                Fcb.EndOfFile = 0x00;
                Fcb.LastCluster = 0x00;
            }
            else
            {
                if (Fcb.EndOfFile != 0x00 || Fcb.LastCluster != 0x00)
                {
                    throw new FatxException("detected an invalid FCB while deleting a file's allocation.");
                }
            }
        }

        public void FatxExtendDirectoryAllocation(FatxFcb Fcb)
        {
            if (!Fcb.IsDirectory)
            {
                throw new FatxException("attempted to extend a file as a directory.");
            }
            else if (Fcb.EndOfFile == 0x00 || Fcb.EndOfFile == 0xffffffff || Fcb.LastCluster == 0x00)
            {
                throw new FatxException("invalid FCB detected while extending directory allocation.");
            }
            else if (Fcb.EndOfFile >= 0x40000)
            {
                throw new FatxException("directory cannot be extended. [0xC00002EA]");
            }

            FatxAllocationState AllocationState = null;
            uint TotalAllocated = 0, LastAllocatedCluster = 0;

            this.FatxAllocateClusters(0x00, 0x01, out AllocationState, out TotalAllocated, out LastAllocatedCluster);

            uint AllocatedCluster = AllocationState.AllocationStates[0].FirstAllocatedCluster;

            if (TotalAllocated != 0x01)
            {
                throw new FatxException(string.Format("allocated an invalid amount of clusters: {0}.", TotalAllocated));
            }
            else if (LastAllocatedCluster != AllocatedCluster)
            {
                throw new FatxException("directory cluster allocation failed.");
            }

            this.FatxInitializeDirectoryCluster(AllocatedCluster);
            this.FatxLinkClusterChains(Fcb.LastCluster, AllocatedCluster);

            Fcb.LastCluster = AllocatedCluster;

            this.FatxAppendClusterRunsToClusterCache(Fcb, (Fcb.EndOfFile >> this.VolumeExtension.ClusterShift), AllocationState, TotalAllocated);

            Fcb.EndOfFile += this.VolumeExtension.ClusterSize;

            if (Fcb.CloneFCB != null)
            {
                Fcb.CloneFCB.FirstCluster = Fcb.FirstCluster;
                Fcb.CloneFCB.LastCluster = Fcb.LastCluster;
                Fcb.CloneFCB.EndOfFile = Fcb.EndOfFile;
            }
        }

        public uint FatxSetDispositionInformation(FatxFcb Fcb, WinFile.FileDispositionInformation DispositionInfo)
        {
            uint dwReturn = 0x00;

            if (DispositionInfo.DeleteFile)
            {
                if (Fcb.IsDirectory)
                {
                    dwReturn = this.FatxIsDirectoryEmpty(Fcb);

                    if (dwReturn != 0x00 && dwReturn != 0xC0000102)
                    {
                        return dwReturn;
                    }
                }

                Fcb.State |= 0x10;
            }
            else
            {
                Fcb.State &= 0xEF;
            }

            return dwReturn;
        }

        public uint FatxSetEndOfFileInformation(FatxFcb Fcb, WinFile.FileEndOfFileInfo EndOfFileInfo)
        {
            uint dwReturn = 0x00;

            if (Fcb.IsDirectory)
            {
                throw new FatxException("attempted to set the EOF of a directory.");
            }

            if (EndOfFileInfo.EndOfFile.HighPart == 0x00)
            {
                uint NewSize = EndOfFileInfo.EndOfFile.LowPart;
                if (NewSize != Fcb.FileSize)
                {
                    if (NewSize > Fcb.FileSize)
                    {
                        this.FatxSetAllocationSize(Fcb, NewSize, false, true);
                    }

                    Fcb.FileSize = NewSize;

                    dwReturn = this.FatxUpdateDirectoryEntry(Fcb);
                }
            }
            else
            {
                throw new FatxException(string.Format("invalid value detected for the new EOF of {0}. [0xC000007F]", Fcb.FileName));
            }

            return dwReturn;
        }

        private uint FatxIsDirectoryEmpty(FatxFcb Fcb)
        {
            if (!Fcb.IsDirectory)
            {
                throw new FatxException("attempted to determine if a non-directory was empty.");
            }

            uint DirectoryOffset;
            FatxDirectoryEntry dirEnt;

            uint dwReturn = this.FatxFindNextDirectoryEntry(Fcb, 0x00, null, out dirEnt, out DirectoryOffset);

            if (dwReturn != 0x00)
            {
                return (dwReturn == 0xC0000011) ? 0x00 : dwReturn;
            }

            return 0xC0000101;
        }

        public uint FatxFindNextDirectoryEntry(FatxFcb Fcb, uint SearchStartOffset, string SearchString, out FatxDirectoryEntry DirectoryEntry, out uint DirectoryOffset)
        {
            uint dwReturn = 0x00, ReturnedCluster, ContinuousClusterCount;

            DirectoryEntry = null;
            DirectoryOffset = 0xFFFFFFFF;

            if (SearchStartOffset >= this.VolumeExtension.ClusterSize)
            {
                dwReturn = this.FatxFileByteOffsetToCluster(Fcb, SearchStartOffset, out ReturnedCluster, out ContinuousClusterCount);
            }
            else
            {
                ReturnedCluster = Fcb.FirstCluster;

                if ((ReturnedCluster - 1) >= this.VolumeExtension.ClusterCount)
                {
                    throw new FatxException("invalid starting cluster for directory.");
                }
            }

            int MaxFileNameLength = (0xF9 - Fcb.Characteristics) > 0x2A ? 0x2A : (0xF9 - Fcb.Characteristics);

            if (MaxFileNameLength < -1)
            {
                throw new FatxException("invalid file name length for directory. [0xC0000011]");
            }

            do
            {
                uint SearchRemainder = ((this.VolumeExtension.ClusterSize - 1) & SearchStartOffset), Remainder = this.VolumeExtension.ClusterSize - SearchRemainder;
                long PhysicalOffset = this.VolumeExtension.BackingClusterOffset + ((Convert.ToInt64(ReturnedCluster - 1) << this.VolumeExtension.ClusterShift) + (SearchRemainder & 0xFFFFFFFF));

                do
                {
                    var reader = new EndianReader(this.FscMapBuffer(PhysicalOffset, false), EndianType.BigEndian);

                    uint lowOffset = Convert.ToUInt32(0x1000 - (PhysicalOffset & 0xFFF));

                    if (lowOffset < Remainder)
                    {
                        Remainder -= lowOffset;
                        PhysicalOffset += (lowOffset & 0xFFFFFFFF);
                    }
                    else
                    {
                        lowOffset = Remainder;
                        Remainder = 0x00;
                    }

                    uint ClusterEnd = this.VolumeExtension.ClusterSize, Boundary = lowOffset, EntryPosition = 0x00;

                    do
                    {
                        var dirEnt = new FatxDirectoryEntry(reader, Fcb.FullFileName == null ? string.Empty : Fcb.FullFileName + "\\");
                        uint Flags = Convert.ToUInt32(dirEnt.FileNameLength);

                        if (Flags == 0x00 || Flags == 0xFF)
                        {
                            return 0xC0000011; // FATX: invalid data buffer detected.
                        }

                        if (dirEnt.FileNameLength <= MaxFileNameLength)
                        {
                            Flags = (dirEnt.Characteristics & 0xFFFFFFF8) & 0xFFFFFFCF;

                            if (Flags == 0x00 && FatxIsValidFatFileName(dirEnt.Filename))
                            {
                                if (SearchString == null || dirEnt.Filename.ToLower().Contains(SearchString.ToLower()))
                                {
                                    DirectoryEntry = dirEnt;
                                    DirectoryOffset = EntryPosition;

                                    return 0x00;
                                }
                            }
                        }                        

                        SearchStartOffset += 0x40;
                        reader.BaseStream.Position = (EntryPosition += 0x40);

                    } while (EntryPosition < Boundary);

                } while (Remainder != 0x00);

                dwReturn = this.FatxFileByteOffsetToCluster(Fcb, SearchStartOffset, out ReturnedCluster, out ContinuousClusterCount);

            } while (SearchStartOffset < 0x40000 && dwReturn == 0x00);

            return dwReturn;
        }

        public static bool FatxIsValidFatFileName(string FileName)
        {
            if (FileName.Length == 0 && FileName.Length > 0x2A)
            {
                return false;
            }

            for (int x = 0; x < 2; x++)
            {
                if (FileName.Length == (x + 1) && FileName[x] == '.')
                    return false;
            }

            for (int x = 0; x < FileName.Length; x++)
            {
                var fchar = FileName[x];
                if (fchar < 0x20 || fchar > 0x7e || ((1 << (fchar & 0x1f)) & FatxFatIllegalTable[((fchar >> 3) & 0x1FFFFFFC) / 4]) != 0)
                    return false;
            }

            return true;
        }

        public uint FatxLookupElementNameInDirectory(FatxFcb DirectoryFcb, string FileName, out FatxDirectoryEntry DirectoryLookup, out uint DirectoryEntryByteOffset, out uint FreeDirEntryOffset)
        {
            if (!DirectoryFcb.IsDirectory)
            {
                throw new FatxException(string.Format("Attempted to look-up an entry [{0}] in a non-directory.", FileName));
            }

            DirectoryLookup = null;
            DirectoryEntryByteOffset = 0x00; FreeDirEntryOffset = 0x00;

            uint FileByteOffset = DirectoryFcb.ByteOffset, didNotFind = 0xC0000034, dwErrorCode = 0x00, CurrentCluster = DirectoryFcb.FirstCluster, FreeEntryOffset = 0xffffffff;

            if (FileByteOffset != 0x00)
            {
                long PhysByteOffset;
                uint Range;

                this.FatxFileByteOffsetToPhysicalByteOffset(DirectoryFcb, FileByteOffset, out PhysByteOffset, out Range);

                var reader = new EndianReader(new MemoryStream(this.FscMapBuffer(PhysByteOffset, false)), EndianType.BigEndian);
                var dirEnt = new FatxDirectoryEntry(reader, DirectoryFcb.FullFileName == null ? string.Empty : DirectoryFcb.FullFileName + "\\");

                if ((dirEnt.FileNameLength == FileName.Length) || (dirEnt.FileNameLength <= 0x2A))
                {
                    if (dirEnt.Filename == FileName)
                    {
                        DirectoryLookup = dirEnt;
                        dwErrorCode = 0x00;
                        DirectoryEntryByteOffset = FileByteOffset;

                        goto FATX_EXIT_DIR_LOOKUP;
                    }
                }
                DirectoryFcb.ByteOffset = 0x00;
                FileByteOffset = 0x00;
            }

            if ((CurrentCluster - 1) >= this.VolumeExtension.ClusterCount)
            {
                throw new FatxException("invalid starting cluster for directory.");
            }

            do
            {
                long PhysicalOffset = (Convert.ToInt64((CurrentCluster - 1) & 0xFFFFFFFF) << this.VolumeExtension.ClusterShift) + this.VolumeExtension.BackingClusterOffset;

                uint ClusterSize = this.VolumeExtension.ClusterSize, ContinuousClusterCount;

                if ((PhysicalOffset & 0xFFF) != 0)
                {
                    throw new FatxException("invalid physical offset was caught while looking up an element in the directory.");
                }
                else if ((ClusterSize & 0xFFF) != 0)
                {
                    throw new FatxException("volume has an invalid cluster size.");
                }

                uint clusterIndex = 1, ReaderByteOffset = 0;

                do
                {
                    int readerPosition = 0;
                    var directoryReader = new EndianReader(new MemoryStream(this.FscMapBuffer(PhysicalOffset, false)), EndianType.BigEndian);
                    do
                    {
                        directoryReader.BaseStream.Position = readerPosition;

                        var dirEnt = new FatxDirectoryEntry(directoryReader, DirectoryFcb.FullFileName == null ? string.Empty : DirectoryFcb.FullFileName + "\\");
                        byte FileNameLength = dirEnt.FileNameLength;
                        if ((FileNameLength == 0x00) || (FileNameLength == 0xFF) || (FileNameLength == 0xE5))
                        {
                            if (FreeEntryOffset == 0xffffffff)
                            {
                                FreeEntryOffset = ReaderByteOffset;
                            }
                        }

                        if ((FileNameLength == 0x00) || (FileNameLength == 0xff))
                        {
                            dwErrorCode = didNotFind;

                            goto FATX_EXIT_DIR_LOOKUP;
                        }
                        else if ((FileNameLength == FileName.Length && FileNameLength <= 0x2A))
                        {
                            if (dirEnt.Filename == FileName)
                            {
                                DirectoryLookup = dirEnt;
                                DirectoryEntryByteOffset = ReaderByteOffset;
                                dwErrorCode = 0x00;

                                goto FATX_EXIT_DIR_LOOKUP;
                            }
                        }

                        readerPosition += 0x40;
                        ReaderByteOffset += 0x40;

                    } while (ReaderByteOffset < (0x1000 * clusterIndex)); // for every 0x1000 bytes in a cluster

                    clusterIndex++;
                    PhysicalOffset += 0x1000;

                } while ((ClusterSize -= 0x1000) != 0);

                FileByteOffset += this.VolumeExtension.ClusterSize;

                dwErrorCode = this.FatxFileByteOffsetToCluster(DirectoryFcb, FileByteOffset, out CurrentCluster, out ContinuousClusterCount);

                if (dwErrorCode == 0xC0000011)
                {
                    dwErrorCode = didNotFind;
                }

                if (dwErrorCode != 0x00)
                {
                    break;
                }
                else if (FileByteOffset >= 0x40000)
                {
                    dwErrorCode = 0xC0000102;
                    break;
                }
            } while (true); // for every new cluster

        FATX_EXIT_DIR_LOOKUP:

            if (dwErrorCode != didNotFind)
            {
                FreeEntryOffset = 0xFFFFFFFE;
            }

            FreeDirEntryOffset = FreeEntryOffset;

            return dwErrorCode;
        }

        public uint FatxUpdateDirectoryEntry(FatxFcb Fcb)
        {
            if (Fcb.IsRootDir)
            {
                throw new FatxException("Attempted to update the root directory, which is not an entry.");
            }
            else if (Fcb.ParentFCB == null)
            {
                return 0xC0000102;
            }

            uint Range;
            long PhysicalOffset;

            this.FatxFileByteOffsetToPhysicalByteOffset(Fcb.ParentFCB, Fcb.DirectoryEntryByteOffset, out PhysicalOffset, out Range);

            byte[] DataBuffer = this.FscMapBuffer(PhysicalOffset, true);

            EndianWriter writer = new EndianWriter(new MemoryStream(DataBuffer), EndianType.BigEndian);

            writer.Write(Convert.ToByte(Fcb.FileName.Length));
            writer.Write(Fcb.Flags);
            writer.Write(Fcb.FileName);
            byte[] Padding = new byte[0x2A - Fcb.FileName.Length];
            HorizonCrypt.memset(Padding, 0, 0xFF, Padding.Length);
            writer.Write(Padding);
            writer.BaseStream.Position = 0x2C;
            writer.Write(Fcb.FirstCluster);
            writer.Write(Fcb.FileSize);
            writer.Write(Fcb.CreationTimeStamp);
            writer.Write(FatxTimeToFatTimestamp(Fcb.ActiveTimeStamp)); // last write
            writer.Write(Fcb.LastWriteTimeStamp);

            writer.Close();

            this.FscWriteBuffer(PhysicalOffset, 0x40, DataBuffer);

            Fcb.State &= 0xDF;

            return 0x00;
        }

        public uint FatxFileByteOffsetToPhysicalByteOffset(FatxFcb Fcb, uint FileByteOffset, out long PhysicalByteOffset, out uint ContinuousClusterRange)
        {
            uint Cluster, ContinuousClusterCount;

            PhysicalByteOffset = 0x00; ContinuousClusterRange = 0x00;

            uint dwErrorCode = this.FatxFileByteOffsetToCluster(Fcb, FileByteOffset, out Cluster, out ContinuousClusterCount);

            if (dwErrorCode != 0)
            {
                if (dwErrorCode == 0xC0000011)
                {
                    dwErrorCode = 0xC0000102;
                }
            }
            else
            {
                PhysicalByteOffset = this.VolumeExtension.BackingClusterOffset + (Convert.ToInt64((Cluster - 1) & 0xFFFFFFFF) << this.VolumeExtension.ClusterShift);
                PhysicalByteOffset += (((this.VolumeExtension.ClusterSize - 1) & FileByteOffset) & 0xFFFFFFFF);

                ContinuousClusterRange = Convert.ToUInt32((Convert.ToInt64(ContinuousClusterCount) << this.VolumeExtension.ClusterShift) - ((this.VolumeExtension.ClusterSize - 1) & FileByteOffset));
            }
            return dwErrorCode;
        }

        // Returns the cluster that corresponds to the offset of the file on the device
        public uint FatxFileByteOffsetToCluster(FatxFcb Fcb, uint FileByteOffset, out uint ReturnedCluster, out uint ContinuousClusterCount)
        {
            if ((this.VolumeExtension.BootSectorSize & 0xFFF) != 0)
            {
                throw new FatxException("Detected an invalid boot sector size.");
            }

            ReturnedCluster = 0x00; ContinuousClusterCount = 0x00;

            if ((Fcb.EndOfFile != 0xffffffff) && (FileByteOffset >= Fcb.EndOfFile))
            {
                return 0xC0000011;
            }

            int ActiveCacheEntryIndex = Fcb.CacheHeadIndex, FinalCacheEntryIndex = ActiveCacheEntryIndex, CurrentCacheEntryIndex = -1, NextCluster = 0;
            int LastClusterIndex = (int)(FileByteOffset >> this.VolumeExtension.ClusterShift), FcbCacheHeadIndex = ActiveCacheEntryIndex & 0xff;

            bool FillEmptyClusterCacheEntry = false;

            FatxCacheEntry CacheEntry = null, FinalCacheEntry = null;

            do
            {
                var Entry = Fcb.Cache[ActiveCacheEntryIndex];
                if (Entry.ContiguousClusters != 0)
                {
                    if (LastClusterIndex >= Entry.ClusterIndex)
                    {
                        if (LastClusterIndex < (Entry.ClusterIndex + Entry.ContiguousClusters))
                        {
                            FinalCacheEntry = Entry;

                            goto update_cache_and_return;
                        }

                        else if (CacheEntry == null || (Entry.ClusterIndex > CacheEntry.ClusterIndex))
                        {
                            CacheEntry = Entry;
                            CurrentCacheEntryIndex = ActiveCacheEntryIndex;
                        }
                    }
                }
                else
                {
                    FillEmptyClusterCacheEntry = true;
                    break;
                }

                ActiveCacheEntryIndex = Entry.NextIndex;
                FinalCacheEntryIndex = ActiveCacheEntryIndex;

            } while (FcbCacheHeadIndex != ActiveCacheEntryIndex);

            int CurrentClusterIndex = 0, CurrentCluster = 0;

            if (CacheEntry != null)
            {
                CurrentCluster = (int)(CacheEntry.StartingCluster + CacheEntry.ContiguousClusters) - 1;
                CurrentClusterIndex = (int)(CacheEntry.ContiguousClusters + CacheEntry.ClusterIndex) - 1;

                if ((CurrentCluster - 1) >= this.VolumeExtension.ClusterCount)
                {
                    throw new FatxException(string.Format("Invalid CurrentCluster detected [0x{0:X8}].", CurrentCluster - 1));
                }

            }
            else
            {
                CurrentCluster = (int)Fcb.FirstCluster;

                if ((uint)(CurrentCluster - 1) >= this.VolumeExtension.ClusterCount)
                {
                    if ((CurrentCluster != 0 && CurrentCluster != -1) || ((CurrentCluster == 0) && (Fcb.IsDirectory)))
                    {
                        return 0xC0000102;
                    }
                    else
                    {
                        Fcb.LastCluster = (uint)ActiveCacheEntryIndex;
                        Fcb.EndOfFile = (uint)(CurrentClusterIndex << this.VolumeExtension.ClusterShift);
                    }

                    return 0x00;
                }
                CurrentClusterIndex = 0;
            }

            int StreamOffset = 0, ContiguousClusterCount = 1, FirstCluster = CurrentCluster, FirstContinuousClusterIndex = CurrentClusterIndex, chainBoundOffset = 0;

            NextCluster = CurrentCluster;

            EndianReader reader = null;

            if (CurrentClusterIndex < LastClusterIndex)
            {
                do
                {
                    CurrentClusterIndex++;

                    int ChainMapOffset = (NextCluster << this.VolumeExtension.ChainShift);

                    if (reader == null || chainBoundOffset != (ChainMapOffset & 0xFFFFF000))
                    {
                        if (reader != null)
                        {
                            reader.Close();
                        }

                        chainBoundOffset = (int)(ChainMapOffset & 0xFFFFF000);

                        reader = new EndianReader(this.FscMapBuffer(this.VolumeExtension.BootSectorSize + chainBoundOffset, false), EndianType.BigEndian);
                    }

                    int PreviousCluster = NextCluster;

                    StreamOffset = ChainMapOffset & 0xFFF;
                    reader.SeekTo(StreamOffset);

                    if (this.VolumeExtension.ChainShift != 1)
                    {
                        NextCluster = reader.ReadInt32();
                    }
                    else
                    {
                        NextCluster = reader.ReadInt16();
                        if ((uint)NextCluster >= 0xFFF0)
                        {
                            NextCluster = Convert.ToInt32(ExtendSign(NextCluster, 16));
                        }
                    }

                    if ((uint)(NextCluster - 1) >= this.VolumeExtension.ClusterCount)
                    {
                        if (NextCluster != -1)
                        {
                            return 0xC0000102;
                        }
                        else
                        {
                            Fcb.LastCluster = (uint)PreviousCluster;
                            Fcb.EndOfFile = (uint)(CurrentClusterIndex << this.VolumeExtension.ClusterShift);

                            return 0xC0000011;
                        }
                    }

                    if ((PreviousCluster + 1) != NextCluster)
                    {
                        if (CacheEntry != null)
                        {
                            if (FirstCluster != ((CacheEntry.StartingCluster + CacheEntry.ContiguousClusters) - 1) || (FirstContinuousClusterIndex != ((CacheEntry.ClusterIndex + CacheEntry.ContiguousClusters) - 1)))
                            {
                                throw new FatxException("Detected invalid CurrentCluster.");
                            }

                            CacheEntry.ContiguousClusters = (uint)(CacheEntry.ContiguousClusters + ContiguousClusterCount) - 1;
                            CacheEntry = null;
                        }
                        else
                        {
                            if (FillEmptyClusterCacheEntry)
                            {
                                FillEmptyClusterCacheEntry = this.FatxFillEmptyClusterCacheEntry(Fcb, (uint)FirstCluster, (uint)FirstContinuousClusterIndex, (uint)ContiguousClusterCount);
                            }
                        }

                        ContiguousClusterCount = 1;
                        FirstCluster = NextCluster;
                        FirstContinuousClusterIndex = CurrentClusterIndex;
                    }
                    else
                    {
                        ContiguousClusterCount++;
                    }

                } while (CurrentClusterIndex < LastClusterIndex);


                if (reader != null)
                {
                    do
                    {
                        int ChainMapOffset = NextCluster << this.VolumeExtension.ChainShift;

                        if (chainBoundOffset != (ChainMapOffset & 0xFFFFF000))
                        {
                            break;
                        }

                        StreamOffset = ChainMapOffset & 0xFFF;
                        int PreviousCluster = NextCluster;

                        reader.SeekTo(StreamOffset);

                        if (this.VolumeExtension.ChainShift == 0x1)
                        {
                            NextCluster = reader.ReadInt16();
                            if ((uint)NextCluster >= 0xFFF0)
                            {
                                NextCluster = Convert.ToInt32(ExtendSign(NextCluster, 16));
                            }
                        }
                        else
                        {
                            NextCluster = reader.ReadInt32();
                        }

                        if ((PreviousCluster + 1) != NextCluster)
                        {
                            if (NextCluster != -1)
                            {
                                if ((NextCluster - 1) < this.VolumeExtension.ClusterCount)
                                {
                                    if (FillEmptyClusterCacheEntry)
                                    {
                                        this.FatxFillEmptyClusterCacheEntry(Fcb, (uint)NextCluster, (uint)(FirstContinuousClusterIndex + ContiguousClusterCount), 1);
                                    }
                                }
                            }
                            else
                            {
                                Fcb.EndOfFile = (uint)((FirstContinuousClusterIndex + ContiguousClusterCount) << this.VolumeExtension.ClusterShift);
                                Fcb.LastCluster = (uint)PreviousCluster;
                            }

                            break;
                        }

                        ContiguousClusterCount++;

                    } while (true);

                    reader.Close();
                }
            }

            if (CacheEntry != null)
            {
                if ((((CacheEntry.StartingCluster + CacheEntry.ContiguousClusters) - 1) != FirstCluster) || (((CacheEntry.ClusterIndex + CacheEntry.ContiguousClusters) - 1) != FirstContinuousClusterIndex))
                {
                    throw new FatxException("invalid cache entry detected.");
                }

                CacheEntry.ContiguousClusters = (uint)(CacheEntry.ContiguousClusters + ContiguousClusterCount) - 1;
                FinalCacheEntryIndex = CurrentCacheEntryIndex & 0xFF;

                FinalCacheEntry = CacheEntry;
            }
            else
            {
                int PreviousEntryIndex = Fcb.Cache[Fcb.CacheHeadIndex].PreviousIndex;
                FinalCacheEntryIndex = PreviousEntryIndex;

                FinalCacheEntry = Fcb.Cache[PreviousEntryIndex];
                FinalCacheEntry.ContiguousClusters = (uint)ContiguousClusterCount;
                FinalCacheEntry.StartingCluster = (uint)FirstCluster;
                FinalCacheEntry.ClusterIndex = (uint)FirstContinuousClusterIndex;
            }

        update_cache_and_return:

            int HeadCacheIndex = (FinalCacheEntryIndex & 0xFF);

            if (HeadCacheIndex != Fcb.CacheHeadIndex)
            {
                if (HeadCacheIndex != Fcb.Cache[Fcb.CacheHeadIndex].PreviousIndex)
                {
                    this.FatxMoveClusterCacheEntryToTail(Fcb, HeadCacheIndex);
                }

                Fcb.CacheHeadIndex = (byte)FinalCacheEntryIndex;
            }

            ReturnedCluster = (uint)((FinalCacheEntry.StartingCluster - FinalCacheEntry.ClusterIndex) + LastClusterIndex);
            ContinuousClusterCount = (FinalCacheEntry.StartingCluster + FinalCacheEntry.ContiguousClusters) - ReturnedCluster;

            return 0x00;
        }

        private bool FatxFillEmptyClusterCacheEntry(FatxFcb Fcb, uint StartingCluster, uint ClusterIndex, uint ContiguousClusters)
        {
            int index = Fcb.CacheHeadIndex;
            FatxCacheEntry entry = null;
            do
            {
                entry = Fcb.Cache[index];
                if (entry.ContiguousClusters == 0)
                {
                    entry.ContiguousClusters = ContiguousClusters;
                    entry.StartingCluster = StartingCluster;
                    entry.ClusterIndex = ClusterIndex;

                    var sEntry = Fcb.Cache[entry.NextIndex];
                    return (sEntry.ContiguousClusters == 0);
                }
                index = entry.NextIndex;
            } while (Fcb.CacheHeadIndex != index);

            return false;
        }
        private void FatxMoveClusterCacheEntryToTail(FatxFcb Fcb, int Index)
        {
            if (Fcb.CacheHeadIndex == Index)
            {
                throw new FatxException("attempted to move a cache entry to the same index.");
            }
            var CacheEntry = Fcb.Cache[Index];
            var HeadCacheEntry = Fcb.Cache[Fcb.CacheHeadIndex];

            Fcb.Cache[CacheEntry.PreviousIndex].NextIndex = CacheEntry.NextIndex;

            Fcb.Cache[CacheEntry.NextIndex].PreviousIndex = CacheEntry.PreviousIndex;

            CacheEntry.NextIndex = Fcb.CacheHeadIndex;
            CacheEntry.PreviousIndex = HeadCacheEntry.PreviousIndex; 

            Fcb.Cache[HeadCacheEntry.PreviousIndex].NextIndex = Index;
            HeadCacheEntry.PreviousIndex = Index;
        }
        private void FatxResetClusterCache(FatxFcb Fcb)
        {
            Fcb.Cache = new List<FatxCacheEntry>();

            uint ctr = 0x10000000, maxCtr = 0, LastIndex = 0, index = 0;

            if (!Fcb.IsDirectory)
            {
                LastIndex = 0x90000000;
                maxCtr = 0xB0000000;
            }
            else
            {
                LastIndex = 0x10000000;
                maxCtr = 0x30000000;
            }
            do
            {
                var entry = new FatxCacheEntry();
                entry.State = (entry.State & ~0xF0000000) | (ctr & 0xF0000000);
                entry.Flags = (entry.Flags & ~0xF0000000) | ((((index++) - 1) << 28) & 0xF0000000);

                Fcb.Cache.Add(entry);

            } while ((ctr += 0x10000000) < maxCtr);

            FatxCacheEntry firstEntry = Fcb.Cache[0];
            firstEntry.Flags = (firstEntry.Flags & ~0xF0000000) | LastIndex;

            Fcb.Cache[Fcb.Cache.Count - 1].State &= 0xFFFFFFF;

            Fcb.CacheHeadIndex = 0x00;
        }
        private void FatxInvalidateClusterCache(FatxFcb Fcb, uint Cluster)
        {
            int Index = Fcb.CacheHeadIndex;
            do
            {
                var Entry = Fcb.Cache[Index];
                int TempIndex = Index;
                Index = Entry.NextIndex;

                if (Entry.ContiguousClusters == 0x00)
                {
                    break;
                }

                if (Cluster > Entry.PreviousIndex)
                {
                    if (Cluster < (Entry.PreviousIndex + Entry.ContiguousClusters))
                    {
                        Entry.ContiguousClusters = Cluster - Entry.ContiguousClusters;
                    }
                }
                else
                {
                    Entry.ContiguousClusters = 0x00;
                    if (Fcb.CacheHeadIndex != TempIndex)
                    {
                        this.FatxMoveClusterCacheEntryToTail(Fcb, TempIndex);
                    }
                    else
                    {
                        Fcb.CacheHeadIndex = Convert.ToByte(Index);
                    }
                }

            } while (Index != Fcb.CacheHeadIndex);
        }
        private void FatxAppendClusterRunsToClusterCache(FatxFcb Fcb, uint LastCluster, FatxAllocationState AllocationState, long Count)
        {
            if (Fcb.EndOfFile == 0xFFFFFFFF)
            {
                throw new FatxException("could not append the cluster runs for an invalid FCB.");
            }
            else if (Count < 1)
            {
                throw new FatxException("cannot append zero clusters to the cache.");
            }

            FatxCacheEntry Entry = null;

            if (LastCluster != 0x00)
            {
                int Index = Fcb.CacheHeadIndex;
                do
                {
                    Entry = Fcb.Cache[Index];

                    if (Entry.ContiguousClusters == 0x00)
                    {
                        goto append_to_cache;
                    }
                    else if (Entry.ClusterIndex >= LastCluster)
                    {
                        throw new FatxException("invalid cluster index detected while appending to the cache.");
                    }
                    else if ((Entry.ClusterIndex + Entry.ContiguousClusters) != LastCluster)
                    {
                        var AllocState = AllocationState.AllocationStates[0];
                        if ((Entry.StartingCluster + Entry.ContiguousClusters) == AllocState.FirstAllocatedCluster)
                        {
                            AllocState.FirstAllocatedCluster = Entry.StartingCluster;
                            AllocState.ContiguousClusters += Entry.ContiguousClusters;

                            LastCluster = Entry.ClusterIndex;

                            if (Index != Fcb.CacheHeadIndex)
                            {
                                this.FatxMoveClusterCacheEntryToTail(Fcb, Fcb.CacheHeadIndex);
                            }
                            else
                            {
                                Fcb.CacheHeadIndex = Convert.ToByte(Entry.NextIndex);
                            }
                        }
                        break;
                    }
                    Index = Entry.NextIndex;
                } while (Index != Fcb.CacheHeadIndex);
            }

        append_to_cache:
            int LastIndex = Convert.ToInt32(Count - 1);

            do
            {
                LastCluster += AllocationState.AllocationStates[LastIndex].ContiguousClusters;
            } while (LastIndex-- > 0);

            LastIndex = Convert.ToInt32(Count - 1);
            Entry = Fcb.Cache[Fcb.CacheHeadIndex];

            int index = -1;
            do
            {
                index = Entry.PreviousIndex;
                var LastAllocationState = AllocationState.AllocationStates[LastIndex];
                LastCluster -= LastAllocationState.ContiguousClusters;

                var Entry2 = Fcb.Cache[index];
                Entry2.StartingCluster = LastAllocationState.FirstAllocatedCluster;
                Entry2.ClusterIndex = LastCluster;
                Entry2.ContiguousClusters = LastAllocationState.ContiguousClusters;

                Entry = Entry2;

            } while (LastIndex-- > 0);

            Fcb.CacheHeadIndex = Convert.ToByte(index);
        }

        /* Fatx I/O Functions */
        public void FatxPartiallyCachedSynchronousIo(FatxFcb Fcb, FatxIO Operation, uint FileByteOffset, EndianIO _io, uint BytesToReadWrite, bool Unknown)
        {
            if ((this.VolumeExtension.BackingClusterOffset & 0xFFF) != 0)
            {
                throw new FatxException("Detected an invalid backing cluster offset for the device.");
            }

            uint LowOffset = (FileByteOffset & 0xFFF), nonCachedIOLength = 0, dwReturn = 0, _FileByteOffset = FileByteOffset, _BytesToReadWrite = BytesToReadWrite;

            if (LowOffset != 0)
            {
                uint FullyCachedIOLength = 0x1000 - LowOffset;

                if (FullyCachedIOLength >= BytesToReadWrite)
                {
                    throw new FatxException("Fully cached IO byte-length was greater than the total requested IO byte-length.");
                }

                this.FatxFullyCachedSynchronousIo(Fcb, Operation, FileByteOffset, _io, FullyCachedIOLength, true);

                FileByteOffset += FullyCachedIOLength;
                BytesToReadWrite -= FullyCachedIOLength;
            }

            if (Unknown)
            {
                if (Operation != FatxIO.Read)
                {
                    throw new FatxException("Invalid IO operation request detected.");
                }

                uint SectorSize = (FatxVolumeExtension.SectorSize) - 1;
                uint SectorOffset = (FatxVolumeExtension.SectorSize + BytesToReadWrite) - 1;

                BytesToReadWrite = SectorOffset & (~SectorSize);
                nonCachedIOLength = BytesToReadWrite;
            }
            else
            {
                if (BytesToReadWrite < 0x1000)
                {
                    throw new FatxException("Detected an invalid amount of bytes left to read/write.");
                }
                nonCachedIOLength = (BytesToReadWrite & 0xFFFFF000);
            }

            this.FatxNonCachedSynchronousIo(Fcb, Operation, FileByteOffset, _io, nonCachedIOLength, true);

            uint FullyCachedRemainder = BytesToReadWrite - nonCachedIOLength;

            if (FullyCachedRemainder != 0x00)
            {
                dwReturn = this.FatxFullyCachedSynchronousIo(Fcb, Operation, FileByteOffset + nonCachedIOLength, _io, FullyCachedRemainder, true);
            }

            if (dwReturn > int.MaxValue)
            {
                this.FatxSynchronousIoTail(Fcb, Operation, _FileByteOffset, _BytesToReadWrite);
            }
        }

        public uint FatxFullyCachedSynchronousIo(FatxFcb Fcb, FatxIO Operation, uint FileByteOffset, EndianIO _io, uint BytesToReadWrite, bool DoNotUpdateAccess)
        {
            if (BytesToReadWrite == 0)
            {
                throw new FatxException("Attempted to read/write zero bytes.");
            }

            EndianWriter writer = null;

            long PhysicalOffset = 0;
            uint ContiguousClusterRange = 0, _BytesToReadWrite = BytesToReadWrite, _FileByteOffset = FileByteOffset;

            uint dwErrorCode = this.FatxFileByteOffsetToPhysicalByteOffset(Fcb, _FileByteOffset, out PhysicalOffset, out ContiguousClusterRange);

            if (dwErrorCode != 0) throw new FatxException(string.Format("FatxFileByteOffsetToPhysicalByteOffset failed with 0x{0:X8}.", dwErrorCode));

            do
            {
                do
                {
                    uint lowOffset = (uint)PhysicalOffset & 0xFFF, remainder = 0x1000 - lowOffset;

                    if (remainder >= ContiguousClusterRange)
                    {
                        remainder = ContiguousClusterRange;
                        ContiguousClusterRange = 0;
                    }
                    else
                    {
                        ContiguousClusterRange -= remainder;
                    }

                    if (remainder > _BytesToReadWrite)
                    {
                        remainder = _BytesToReadWrite;
                    }

                    if (Operation == FatxIO.Read)
                    {
                        _io.Out.Write(this.FscMapBuffer(PhysicalOffset, 0x1000, false), remainder);
                    }
                    else if (Operation == FatxIO.Write)
                    {
                        byte[] Data = null;

                        if (remainder == 0x1000 || (((FileByteOffset & 0xFFF) == 0) && (_BytesToReadWrite + _FileByteOffset) >= Fcb.FileSize))
                        {
                            Data = this.FscMapEmptyBuffer();
                        }
                        else
                        {
                            Data = this.FscMapBuffer(PhysicalOffset, true);
                        }

                        writer = new EndianWriter(Data, EndianType.BigEndian);
                        writer.Write(_io.In.ReadBytes(remainder));
                        writer.Close();

                        this.FscWriteBuffer(PhysicalOffset, remainder, Data);
                    }

                    _BytesToReadWrite -= remainder;
                    PhysicalOffset += (remainder & 0xFFFFFFFF);

                    if (_BytesToReadWrite != 0)
                    {
                        _FileByteOffset += remainder;
                    }
                    else
                    {
                        if (!DoNotUpdateAccess)
                        {
                            this.FatxSynchronousIoTail(Fcb, Operation, FileByteOffset, BytesToReadWrite);
                        }
                        return 0x00;
                    }
                } while (ContiguousClusterRange != 0);

            } while (this.FatxFileByteOffsetToPhysicalByteOffset(Fcb, _FileByteOffset, out PhysicalOffset, out ContiguousClusterRange) == 0);

            return 0x00;
        }
        private void FatxNonCachedSynchronousIo(FatxFcb Fcb, FatxIO Operation, uint FileByteOffset, EndianIO _io, uint BytesToReadWrite, bool DoNotUpdateAccess)
        {
            long PhysicalOffset = 0, NextPhysicalOffset = 0, CurrentPhysicalOffset = 0;
            uint ClusterRange = 0, IoCount = ((BytesToReadWrite + 0x200) - 1) & ~(uint)(0x1FF);

            if (IoCount == 0)
            {
                throw new FatxException("Detected an invalid amount of bytes to read/write for a non-cached request.");
            }
            else if (((this.BootSectorHeader.SectorsPerCluster - 1) & FileByteOffset) != 0)
            {
                throw new FatxException("Detected an invalid start position to read/write for a non-cached request.");
            }

            this.FatxFileByteOffsetToPhysicalByteOffset(Fcb, FileByteOffset, out PhysicalOffset, out ClusterRange);

            do
            {
                uint TemporaryBufferLength = 0;
                CurrentPhysicalOffset = PhysicalOffset;
                do
                {
                    if (ClusterRange > IoCount)
                    {
                        ClusterRange = IoCount;
                        IoCount = 0;
                    }
                    else
                    {
                        IoCount -= ClusterRange;
                    }

                    TemporaryBufferLength += ClusterRange;
                    FileByteOffset += ClusterRange;

                    if (IoCount != 0)
                    {
                        ClusterRange &= 0xFFFFFFFF;
                        NextPhysicalOffset = PhysicalOffset + ClusterRange;

                        uint dwReturn = this.FatxFileByteOffsetToPhysicalByteOffset(Fcb, FileByteOffset, out PhysicalOffset, out ClusterRange);
                        if (dwReturn != 0)
                        {
                            throw new FatxException(string.Format("File byte offset to physical byte offset failed during non-cached synchronous I/O. [0x{0:X8}]", dwReturn));
                        }
                    }
                    else
                    {
                        break;
                    }

                } while (NextPhysicalOffset == PhysicalOffset);

                if (Operation == FatxIO.Read)
                {
                    _io.Out.Write(this.FscMapBuffer(CurrentPhysicalOffset, TemporaryBufferLength, false));
                }
                else if (Operation == FatxIO.Write)
                {
                    this.FscWriteBuffer(CurrentPhysicalOffset, TemporaryBufferLength, _io.In.ReadBytes(TemporaryBufferLength));
                }

            } while (IoCount != 0);

            if (!DoNotUpdateAccess)
            {
                this.FatxSynchronousIoTail(Fcb, Operation, FileByteOffset, BytesToReadWrite);
            }
        }

        private void FatxSynchronousIoTail(FatxFcb Fcb, FatxIO IOType, uint FileByteOffset, uint BytesToReadWrite)
        {
            if (IOType == FatxIO.Write)
            {
                Fcb.ActiveTimeStamp = DateTime.Now;

                Fcb.State |= 0x20;

                uint endOfFile = FileByteOffset + BytesToReadWrite;

                if (endOfFile > Fcb.FileSize)
                {
                    Fcb.FileSize = endOfFile;

                    this.FatxUpdateDirectoryEntry(Fcb);
                }
            }
        }

        private uint FatxDeleteFile(FatxFcb Fcb, uint FileOffset, bool FreeClusters)
        {
            if (!Fcb.IsDirectory)
            {
                throw new FatxException("invalid FCB detected while attempting to delete a file.");
            }

            uint ClusterRange;
            long PhysicalOffset;

            uint dwErrorCode = this.FatxFileByteOffsetToPhysicalByteOffset(Fcb, FileOffset, out PhysicalOffset, out ClusterRange);

            byte[] DataBuffer = this.FscMapBuffer(PhysicalOffset, true);

            var IO = new EndianIO(DataBuffer, EndianType.BigEndian, true);

            IO.Out.Write(Convert.ToByte(0xE5));

            this.FscWriteBuffer(PhysicalOffset, 0x40, DataBuffer);

            IO.In.SeekTo(0x2C);

            if (FreeClusters)
            {
                dwErrorCode = this.FatxFreeClusters(IO.In.ReadUInt32(), false);
            }

            return dwErrorCode;
        }

        public void FatxFinalizeDeleteOnClose(FatxFcb Fcb)
        {
            this.FatxMarkDirectoryEntryDeleted(Fcb);

            this.FatxFreeClusters(Fcb.FirstCluster, false);

            this.FatxDereferenceFcb(Fcb.ParentFCB);

            Fcb.ParentFCB = null;
        }

        private uint FatxMarkDirectoryEntryDeleted(FatxFcb Fcb)
        {
            if (Fcb.IsRootDir)
            {
                throw new FatxException("attempted to mark the root directory listing as deleted.");
            }
            else if (Fcb.ParentFCB == null)
            {
                throw new FatxException("detected an invalid parent FCB. [0xC0000102]");
            }

            long PhysicalOffset;
            uint ClusterRange;

            uint dwReturn = this.FatxFileByteOffsetToPhysicalByteOffset(Fcb.ParentFCB, Fcb.DirectoryEntryByteOffset, out PhysicalOffset, out ClusterRange);

            var fatxIo = new EndianIO(this.FscMapBuffer(PhysicalOffset, true), EndianType.BigEndian, true);

            fatxIo.Out.SeekTo(0x00);
            fatxIo.Out.Write(Convert.ToByte(0xE5));

            this.FscWriteBuffer(PhysicalOffset, 0x40, fatxIo.ToArray());

            return dwReturn;
        }

        private void FatxInitializeDirectoryCluster(uint Cluster)
        {
            if ((--Cluster) >= this.VolumeExtension.ClusterCount)
            {
                throw new FatxException("cluster was outside of volume cluster range.");
            }

            long PhysicalOffset = this.VolumeExtension.BackingClusterOffset + (Convert.ToInt64(Cluster & 0xFFFFFFFF) << this.VolumeExtension.ClusterShift);

            if ((PhysicalOffset & 0xFFF) != 0x00)
            {
                throw new FatxException("detected an invalid physical offset.");
            }
            else if ((this.VolumeExtension.ClusterSize & 0xFFF) != 0x00)
            {
                throw new FatxException("detected an invalid cluster size for this volume.");
            }

            uint ClusterSize = this.VolumeExtension.ClusterSize;

            byte[] Data = this.FscMapEmptyBuffer();
            HorizonCrypt.memset(Data, 0, 0xFF, 0x1000);

            do
            {
                this.FscWriteBuffer(PhysicalOffset, 0x1000, Data);
                PhysicalOffset += 0x1000;
            } while ((ClusterSize -= 0x1000) > 0x00);
        }

        private uint FatxFreeClusters(uint FirstClusterNumber, bool UpdateEndofClusterLink)
        {
            if ((this.VolumeExtension.BootSectorSize & 0xFFF) != 0x00)
            {
                throw new FatxException(string.Format("Invalid boot sector length detected.", this.VolumeExtension.BootSectorSize));
            }
            else if ((FirstClusterNumber - 1) >= this.VolumeExtension.ClusterCount)
            {
                return 0x00;
            }

            uint LastFreeClusterIndex = this.VolumeExtension.LastFreeClusterIndex, PreviousChainMapPosition = 0,
                FreedCluserCount = 0, ClosingLink = UpdateEndofClusterLink ? 0xFFFFFFFF : 0x00, CurrentCluster = FirstClusterNumber;

            EndianIO fatxIO = null;
            MemoryStream Ms = null;

            do
            {
                uint ChainMapPosition = (uint)CurrentCluster << this.VolumeExtension.ChainShift;

                if (Ms != null)
                {
                    if ((ChainMapPosition & 0xFFFFF000) != PreviousChainMapPosition)
                    {
                        this.FscWriteBuffer(this.VolumeExtension.BootSectorSize + (PreviousChainMapPosition & 0xFFFFF000), 0x1000, Ms.ToArray());

                        this.FatxMarkFreeClusterRange(PreviousChainMapPosition);

                        this.VolumeExtension.FreedClusterCount += FreedCluserCount;
                        this.VolumeExtension.LastFreeClusterIndex = LastFreeClusterIndex;

                        if (this.VolumeExtension.FreedClusterCount != 0 && ((LastFreeClusterIndex - 1) >= this.VolumeExtension.ClusterCount))
                        {
                            throw new FatxException("invalid cluster index detected while freeing clusters.");
                        }

                        FreedCluserCount = 0x00;
                    }
                    else
                    {
                        goto chainMap_continue;
                    }
                }

                PreviousChainMapPosition = ChainMapPosition & 0xFFFFF000;

                fatxIO = new EndianIO(Ms = new MemoryStream(this.FscMapBuffer(this.VolumeExtension.BootSectorSize + PreviousChainMapPosition, false)),
                    EndianType.BigEndian, true);

            chainMap_continue:

                if (CurrentCluster < LastFreeClusterIndex)
                {
                    if (ClosingLink == 0x00)
                    {
                        LastFreeClusterIndex = FirstClusterNumber;
                    }
                }

                uint LocalChainMapPosition = ChainMapPosition & 0xFFF;

                fatxIO.SeekTo(LocalChainMapPosition);

                if (this.VolumeExtension.ChainShift == 0x01)
                {
                    CurrentCluster = fatxIO.In.ReadUInt16();

                    if (CurrentCluster >= 0xFFF0)
                    {
                        CurrentCluster = (uint)(ExtendSign(CurrentCluster, 16));
                    }

                    fatxIO.SeekTo(LocalChainMapPosition);
                    fatxIO.Out.Write((ushort)(ClosingLink));
                }
                else
                {
                    CurrentCluster = fatxIO.In.ReadUInt32();
                    fatxIO.SeekTo(LocalChainMapPosition);
                    fatxIO.Out.Write(ClosingLink);
                }

                if (ClosingLink == 0x00)
                {
                    FreedCluserCount++;
                }

                ClosingLink = 0x00;

            } while ((CurrentCluster - 1) < this.VolumeExtension.ClusterCount);

            if (CurrentCluster != 0xFFFFFFFF)
            {
                throw new FatxException("corrupt FAT chain found while freeing clusters.");
            }
            else if (Ms == null)
            {
                throw new FatxException("invalid file allocation table buffer found.");
            }

            this.FscWriteBuffer(this.VolumeExtension.BootSectorSize + (PreviousChainMapPosition & 0xFFFFFFFF), 0x1000, Ms.ToArray());
            this.FatxMarkFreeClusterRange(PreviousChainMapPosition);

            this.VolumeExtension.FreedClusterCount += FreedCluserCount;
            this.VolumeExtension.LastFreeClusterIndex = LastFreeClusterIndex;

            if (this.VolumeExtension.FreedClusterCount != 0 && ((LastFreeClusterIndex - 1) >= this.VolumeExtension.ClusterCount))
            {
                throw new FatxException("invalid cluster index detected while freeing clusters.");
            }
            else if (this.VolumeExtension.FreedClusterCount >= this.VolumeExtension.ClusterCount)
            {
                throw new FatxException("freed cluster count was outside of total cluster range.");
            }

            return 0x01;
        }

        private void FatxMarkFreeClusterRange(uint ChainMapOffset)
        {
            if (this.VolumeExtension.AllocationSupport == null)
            {
                return;
            }

            uint ClusterIndex = (ChainMapOffset >> this.VolumeExtension.ChainShift) >> this.VolumeExtension.AllocationShift, AllocationIndex = ClusterIndex >> 6;

            if (AllocationIndex >= 0x20)
            {
                throw new FatxException("Allocation support index was out of bounds.");
            }

            var io = new EndianIO(this.VolumeExtension.AllocationSupport, EndianType.BigEndian, true);

            io.In.SeekTo(AllocationIndex << 3);

            long Marker = Convert.ToInt64(Convert.ToInt64(Convert.ToInt64(1) << Convert.ToInt32((0x3F - (ClusterIndex & 0x3F)) & 0xFFFFFFFF)) | io.In.ReadInt64());

            io.Out.SeekTo(AllocationIndex << 3);

            io.Out.Write(Marker);
        }

        private void FatxClearFreeClusterRange(uint ChainMapOffset)
        {
            if (this.VolumeExtension.AllocationSupport == null)
            {
                return;
            }

            uint AllocationIndex = ((ChainMapOffset + 0x1000) >> this.VolumeExtension.ChainShift) >> this.VolumeExtension.AllocationShift;

            if (AllocationIndex == 0x00)
            {
                return;
            }

            uint ClusterIndex = AllocationIndex - 1;
            AllocationIndex = ClusterIndex >> 6;

            if (AllocationIndex >= 0x20)
            {
                throw new FatxException("allocation support index was out of bounds.");
            }

            var io = new EndianIO(this.VolumeExtension.AllocationSupport, EndianType.BigEndian, true);

            io.SeekTo(AllocationIndex << 3);

            long Marker = (Convert.ToInt64(1) << Convert.ToInt32((0x3F - (ClusterIndex & 0x3F)) & 0xFFFFFFFF)) | io.In.ReadInt64();

            io.SeekTo(AllocationIndex << 3);

            io.Out.Write(Marker);
        }

        private uint FatxSkipAheadToFreeClusterRange(uint Cluster)
        {
            if ((Cluster - 1) >= this.VolumeExtension.ClusterCount)
            {
                throw new FatxException("cluster was outside of volume cluster range.");
            }
            else if (this.VolumeExtension.AllocationSupport == null)
            {
                return Cluster;
            }

            uint ClusterIndex = Cluster >> this.VolumeExtension.AllocationShift, AllocationIndex = ClusterIndex >> 6;
            ulong Marker = Convert.ToUInt64(0xffffffffffffffff) >> Convert.ToInt32(ClusterIndex & 0x3F);

            if (AllocationIndex >= 0x20)
            {
                throw new FatxException("allocation support index was out of bounds.");
            }

            var reader = new EndianReader(this.VolumeExtension.AllocationSupport, EndianType.BigEndian);

            reader.SeekTo(AllocationIndex << 3);

            do
            {
                Marker &= reader.ReadUInt64();

                if (Marker != 0x00)
                {
                    long NewAllocationIndex = ((AllocationIndex << 6) + CountLeadingZerosDouble((long)Marker));
                    return NewAllocationIndex == ClusterIndex ? Cluster : Convert.ToUInt32(NewAllocationIndex << this.VolumeExtension.AllocationShift);
                }

                Marker = 0xffffffffffffffff;

            } while ((++AllocationIndex) < 0x20);

            return Cluster;
        }

        private uint FatxAllocateClusters(uint CurrentCluster, uint RequestedAllocationCount, out FatxAllocationState AllocationState, out uint TotalAllocated, out uint LastAllocatedCluster)
        {
            uint dwReturn = 0x00;

            if ((this.VolumeExtension.BootSectorSize & 0xFFF) != 0x00)
            {
                throw new FatxException("invalid volume boot sector size detected while allocating clusters.");
            }
            else if (RequestedAllocationCount > this.VolumeExtension.FreedClusterCount)
            {
                throw new FatxException("requested amount of clusters to be allocated was outside of total free cluster range. [0xC000007F]");
            }

            AllocationState = new FatxAllocationState();
            TotalAllocated = 0x00; LastAllocatedCluster = 0x00;

            uint NewCurrentCluster = 0x00, FreeCurrentCluster = 0, ChainMapLocation = 0x00;

            if (CurrentCluster != 0x00)
            {
                NewCurrentCluster = CurrentCluster + 1;
                ChainMapLocation = (CurrentCluster << this.VolumeExtension.ChainShift);
            }
            else
            {
                NewCurrentCluster = this.VolumeExtension.LastFreeClusterIndex;
                ChainMapLocation = 0xFFFFFFFF;
            }

            uint StartingCluster = CurrentCluster, LastCluster = 0x00, ClusterOffset = 0, ChainMapOffset = 0x00,
                FirstAllocatedCluster = 0x00, nCurrentCluster = 0x00, tCurrentCluster = 0x00, AllocatedContiguousClusters = 0x00,
                AllocatedCount = 0x00, OldPosition = 0x00;

            int ClusterNum = 0x00;

            bool ClearFreeClusterRange = Convert.ToBoolean((CountLeadingZerosWord(this.VolumeExtension.LastFreeClusterIndex - NewCurrentCluster) >> 5) & 0x01),
                IsCurrentClusterInUse = false, IsAllocationContiguous = false, FlushBuffers = false, NewTable = false;

            MemoryStream ms = null;
            EndianIO io = null;

            do
            {
                do
                {
                    if ((this.VolumeExtension.ClusterCount + 1) == NewCurrentCluster)
                    {
                        if (ClearFreeClusterRange)
                        {
                            throw new FatxException("FAT table is corrupt. [0xC0000032]");
                        }
                        else
                        {
                            ClearFreeClusterRange = true;
                            NewCurrentCluster = this.VolumeExtension.LastFreeClusterIndex;
                        }
                    }

                    if ((NewCurrentCluster - 1) >= this.VolumeExtension.ClusterCount)
                    {
                        throw new FatxException("cluster index outside of volume cluster range.");
                    }

                    ClusterOffset = NewCurrentCluster << this.VolumeExtension.ChainShift;

                    if (ms != null)
                    {
                        if ((ClusterOffset & 0xFFFFF000) == ChainMapOffset)
                        {
                            goto UseSameFATXTableBuffer;
                        }

                        if (FlushBuffers)
                        {
                            this.FscWriteBuffer(this.VolumeExtension.BootSectorSize + (ChainMapOffset & 0xFFFFFFFF), 0x1000, io.ToArray());

                            this.VolumeExtension.FreedClusterCount -= AllocatedCount;

                            if (StartingCluster != 0x00 && !IsAllocationContiguous)
                            {
                                this.FatxLinkClusterChains(StartingCluster, LastCluster);
                            }

                            StartingCluster = tCurrentCluster;
                        }

                        if (ClearFreeClusterRange)
                        {
                            this.FatxClearFreeClusterRange(ChainMapOffset);
                        }

                        io.Close();
                        ms = null;

                        AllocatedCount = 0x00;
                        FlushBuffers = false;
                    }

                    FreeCurrentCluster = this.FatxSkipAheadToFreeClusterRange(NewCurrentCluster);
                    IsCurrentClusterInUse = (FreeCurrentCluster != NewCurrentCluster) ? true : false;
                    NewCurrentCluster = IsCurrentClusterInUse ? FreeCurrentCluster : NewCurrentCluster;

                } while (IsCurrentClusterInUse);

                ChainMapOffset = (ClusterOffset & 0xFFFFF000);
                io = new EndianIO(ms = new MemoryStream(this.FscMapBuffer(this.VolumeExtension.BootSectorSize + ChainMapOffset, true)), EndianType.BigEndian, true);

                if ((StartingCluster != 0x00) && (StartingCluster == CurrentCluster) && ((ChainMapLocation & 0xFFFFF000) == ChainMapOffset))
                {
                    OldPosition = (ChainMapLocation & 0xFFF);
                    io.In.SeekTo(OldPosition);

                    IsAllocationContiguous = true;
                    NewTable = true;

                    if (this.VolumeExtension.ChainShift == 0x01)
                    {
                        ClusterNum = io.In.ReadInt16();
                        if (ClusterNum >= -16)
                        {
                            ClusterNum = Convert.ToInt32(ExtendSign(ClusterNum, 16));
                        }
                    }
                    else
                    {
                        ClusterNum = io.In.ReadInt32();
                    }

                    if (ClusterNum != -1)
                    {
                        throw new FatxException("found an invalid cluster number while allocating clusters.");
                    }
                }
                else
                {
                    NewTable = false;
                    IsAllocationContiguous = false;
                }

            UseSameFATXTableBuffer:

                io.In.SeekTo(ClusterOffset & 0xFFF);

                if (this.VolumeExtension.ChainShift == 0x01)
                {
                    ClusterNum = io.In.ReadUInt16();
                    if (ClusterNum >= -16)
                    {
                        ClusterNum = Convert.ToInt32(ExtendSign(ClusterNum, 16));
                    }
                }
                else
                {
                    ClusterNum = io.In.ReadInt32();
                }

                if (ClusterNum == 0x00)
                {
                    io.Out.SeekTo(ClusterOffset & 0xFFF);

                    if (this.VolumeExtension.ChainShift == 0x01)
                    {
                        io.Out.Write(Convert.ToUInt16(0xFFFF));
                    }
                    else
                    {
                        io.Out.Write(0xFFFFFFFF);
                    }

                    FlushBuffers = true;
                    AllocatedCount++;

                    if (NewTable)
                    {
                        io.Out.SeekTo(OldPosition);

                        if (this.VolumeExtension.ChainShift == 0x01)
                        {
                            io.Out.Write(Convert.ToUInt16(NewCurrentCluster));
                        }
                        else
                        {
                            io.Out.Write(NewCurrentCluster);
                        }
                    }
                    else
                    {
                        NewTable = true;
                        LastCluster = NewCurrentCluster;
                    }

                    if (FirstAllocatedCluster != 0x00)
                    {
                        if ((nCurrentCluster + AllocatedContiguousClusters) != NewCurrentCluster)
                        {
                            if (AllocationState.AllocationStates.Count < 0x0A)
                            {
                                AllocationState.AllocationStates.Add(new FatxAllocationState.AllocationState()
                                {
                                    FirstAllocatedCluster = nCurrentCluster,
                                    ContiguousClusters = AllocatedContiguousClusters
                                });
                            }

                            nCurrentCluster = NewCurrentCluster;
                            AllocatedContiguousClusters = 0x00;
                        }
                    }
                    else
                    {
                        FirstAllocatedCluster = NewCurrentCluster;
                        nCurrentCluster = NewCurrentCluster;

                        if (AllocatedContiguousClusters != 0x00)
                        {
                            throw new FatxException("invalid allocation index detected while allocating clusters.");
                        }
                    }

                    AllocatedContiguousClusters++;
                    tCurrentCluster = NewCurrentCluster;
                    OldPosition = (ClusterOffset & 0xFFF);

                    if ((--RequestedAllocationCount) == 0x00)
                    {
                        break;
                    }
                }

                NewCurrentCluster++;

            } while (true);

            this.FscWriteBuffer(this.VolumeExtension.BootSectorSize + ChainMapOffset, 0x1000, io.ToArray());

            if (ClearFreeClusterRange)
            {
                this.VolumeExtension.LastFreeClusterIndex = NewCurrentCluster + 1;
            }

            this.VolumeExtension.FreedClusterCount -= AllocatedCount;

            if (this.VolumeExtension.LastFreeClusterIndex == 0x00)
            {
                if ((this.VolumeExtension.FreedClusterCount - 1) >= this.VolumeExtension.ClusterCount)
                {
                    throw new FatxException("free cluster count was outside of volume cluster range.");
                }
            }

            if (StartingCluster != 0x00)
            {
                if (!IsAllocationContiguous)
                {
                    this.FatxLinkClusterChains(StartingCluster, LastCluster);
                }
            }

            if (AllocationState.AllocationStates.Count < 0x0A)
            {
                AllocationState.AllocationStates.Add(new FatxAllocationState.AllocationState()
                {
                    FirstAllocatedCluster = nCurrentCluster,
                    ContiguousClusters = AllocatedContiguousClusters
                });
            }

            TotalAllocated = Convert.ToUInt32(AllocationState.AllocationStates.Count);
            LastAllocatedCluster = NewCurrentCluster;

            return dwReturn;
        }

        private uint FatxLinkClusterChains(uint StartingCluster, uint LastCluster)
        {
            uint dwReturn = 0x00;

            if ((StartingCluster - 1) >= this.VolumeExtension.ClusterCount)
            {
                throw new FatxException("starting cluster was outside of total cluster range.");
            }
            else if ((LastCluster - 1) >= this.VolumeExtension.ClusterCount)
            {
                throw new FatxException("ending cluster was outside of total cluster range.");
            }

            long PhysicalOffset = this.VolumeExtension.BootSectorSize + ((StartingCluster << this.VolumeExtension.ChainShift) & 0xFFFFFFFF);

            var fatxIO = new EndianIO(this.FscMapBuffer(PhysicalOffset, true), EndianType.BigEndian, true);

            int CurrentCluster = 0x00;
            if (this.VolumeExtension.ChainShift == 0x01)
            {
                CurrentCluster = fatxIO.In.ReadInt16();
                if (CurrentCluster >= -16)
                {
                    CurrentCluster = Convert.ToInt32(ExtendSign(CurrentCluster, 16));
                }
            }
            else
            {
                CurrentCluster = fatxIO.In.ReadInt32();
            }

            if (CurrentCluster != -1)
            {
                throw new FatxException("detected an invalid cluster chain while linking.");
            }

            fatxIO.Out.SeekTo(0x00);

            if (this.VolumeExtension.ChainShift == 0x01)
            {
                fatxIO.Out.Write(Convert.ToInt16(LastCluster));
            }
            else
            {
                fatxIO.Out.Write(LastCluster);
            }

            this.FscWriteBuffer(PhysicalOffset, 0x1000, fatxIO.ToArray());

            return dwReturn;
        }

        public bool FscTestForFullyCachedIo(uint FileByteOffset, uint BytesToReadWrite)
        {
            if (BytesToReadWrite == 0)
            {
                throw new FatxException("Requested an I/O operation of zero bytes.");
            }

            if (BytesToReadWrite < 0x1000)
            {
                return true;
            }
            else if ((((FileByteOffset + BytesToReadWrite) ^ (FileByteOffset + 0xFFF)) & 0xFFFFF000) != 0)
            {
                return false;
            }

            return true;
        }

        private byte[] FscMapBuffer(long PhysicalOffset, bool InvalidateBlocks)
        {
            return FscMapBuffer(PhysicalOffset, 0x1000, InvalidateBlocks);
        }
        private byte[] FscMapBuffer(long PhysicalOffset, long DataSize, bool InvalidateBlocks)
        {
            ulong Position = (Convert.ToUInt64(this.DeviceOffset + PhysicalOffset) & 0xFFFFFFFFFFFFF200);

            if (Position >= Convert.ToUInt64(this.DeviceSize + this.DeviceOffset))
            {
                throw new FatxException(string.Format("invalid read position detected : 0x{0:X16}.", Position));
            }

            this.IO.SeekTo(Position);

            long remainder = ((this.DeviceOffset + PhysicalOffset) & 0xFFF) & ~0x200;

            if (remainder != 0)
            {
                MemoryStream ms = new MemoryStream();

                ms.Write(this.IO.In.ReadBytes(RoundToBlock(remainder + DataSize)), Convert.ToInt32(remainder), Convert.ToInt32(DataSize));

                return ms.ToArray();
            }

            return this.IO.In.ReadBytes(DataSize);
        }
        private byte[] FscMapEmptyBuffer()
        {
            return new byte[0x1000];
        }

        private void FscWriteBuffer(long PhysicalOffset, long SizeToWrite, byte[] Data)
        {
            ulong Position = (Convert.ToUInt64(this.DeviceOffset + PhysicalOffset) & 0xFFFFFFFFFFFFF200);

            if (Position >= Convert.ToUInt64(this.DeviceOffset + this.DeviceSize))
            {
                throw new FatxException(string.Format("invalid write position detected : 0x{0:X16}.", Position));
            }

            this.IO.SeekTo(Position);

            long remainder = (((this.DeviceOffset + PhysicalOffset) & 0xFFF) & ~0x200);

            if (remainder != 0 || SizeToWrite % 0x200 != 0)
            {
                uint sizeToWriteSpan = RoundToBlock(SizeToWrite + remainder);
                byte[] tempBuffer = this.IO.In.ReadBytes(sizeToWriteSpan);
                this.IO.Position -= sizeToWriteSpan;
                Array.Copy(Data, 0, tempBuffer, remainder, SizeToWrite);
                SizeToWrite = sizeToWriteSpan;
                Data = tempBuffer;
            }

            this.IO.Out.Write(Data, SizeToWrite);

            this.IO.Out.Flush();
        }

        public static DateTime FatxFatTimestampToTime(int FatTimeStamp)
        {
            return new DateTime((
                (FatTimeStamp >> 25) & 0x7F) + 1980,
                (FatTimeStamp >> 21) & 0x0F,
                (FatTimeStamp >> 16) & 0x1F,
                (FatTimeStamp >> 11) & 0x1F,
                (FatTimeStamp >> 5) & 0x3F,
                (FatTimeStamp & 0x1F) << 1).ToLocalTime();
        }
        public static int FatxTimeToFatTimestamp(DateTime DateTime)
        {
            DateTime = DateTime.ToUniversalTime();
            var second = DateTime.Second;
            var year = DateTime.Year;
            year -= 1980;
            second >>= 1;
            return (year << 25) | (DateTime.Month << 21) | (DateTime.Day << 16) | (DateTime.Hour << 11) | (DateTime.Minute << 5) | second;
        }

        /* Generic file/stream functions */
        public byte[] ExtractFileToArray(string FileName)
        {
            return new FatxFileStream(this, FileName, FileMode.Open, 0x00).ToArray();
        }

        public EndianIO LoadFileToIO(string Filename)
        {
            return new EndianIO(new FatxFileStream(this, Filename, FileMode.Open, null), EndianType.BigEndian, true);
        }
        public FatxFileStream LoadFileStream(string Filename)
        {
            return new FatxFileStream(this, Filename, FileMode.Open, 0x00);
        }

        public void InjectFileFromArray(string Filename, byte[] FileBuffer)
        {
            var FileStream = new FatxFileStream(this, Filename, FileMode.Open, null);

            this.FatxOverwriteExistingFile(FileStream.Fcb, FileBuffer.Length);

            var writer = new EndianWriter(FileStream, EndianType.BigEndian);
            writer.Write(FileBuffer);
            writer.Close();

        }

        public Stream OverwriteFileFromStream(string Filename, long FileSize)
        {
            var FileStream = new FatxFileStream(this, Filename, FileMode.Open, null);

            if (FileStream.Fcb.IsDirectory)
            {
                throw new FatxException("attempted to create a stream for a directory.");
            }

            this.FatxOverwriteExistingFile(FileStream.Fcb, FileSize);
            FileStream.SetLength(FileSize);

            return FileStream;
        }

        public Stream CreateFileStream(string FileName, long FileSize)
        {
            var FileStream = new FatxFileStream(this, FileName, FileMode.CreateNew, FileSize);
            var writer = new EndianWriter(FileStream, EndianType.BigEndian);
            writer.BaseStream.SetLength(FileSize);

            return FileStream;
        }

        public void CreateFileFromArray(string FileName, byte[] FileBuffer)
        {
            var writer = new EndianWriter(new FatxFileStream(this, FileName, FileMode.CreateNew, FileBuffer.Length), EndianType.BigEndian);
            //writer.BaseStream.SetLength(FileBuffer.Length);
            writer.Write(FileBuffer);
            writer.Close();
        }

        public void DeleteFile(string FileName)
        {
            FatxFcb Fcb = null, FileFcb = null;
            var Filename = BreakStringByPath(FileName);
            int LastName = Filename.Length - 1, FilenameLength = LastName + 1;
            do
            {
                int idx = 0x00;
                var File = new StringBuilder();

                while (idx < FilenameLength)
                {
                    File.Append(Filename[idx++] + (idx < FilenameLength ? "\\" : string.Empty));
                }

                var FileStream = new FatxFileStream(this, File.ToString(), FileMode.Open, null);

                if(LastName == (Filename.Length - 1))
                {
                    FileFcb = FileStream.Fcb;
                }

                this.FatxDeleteFile(FileStream.Fcb.ParentFCB, FileStream.DirectoryEntryByteOffset, true);

                Fcb = FileStream.Fcb.ParentFCB;

                FilenameLength--;

            } while (LastName-- > 1 && this.FatxIsDirectoryEmpty(Fcb) == 0x00);

            this.FatxDereferenceFcb(FileFcb);
        }

        public void MoveFile(string ExistingFileName, string NewFileName)
        {
            var OldPath = BreakStringByPath(ExistingFileName);
            var NewPath = BreakStringByPath(NewFileName);
            int NewFileNameIndex = NewPath.Length - 1;

            if (OldPath[OldPath.Length - 1] == NewPath[NewFileNameIndex]) // check to make sure the file name is the same
            {
                uint dwReturn, DirectoryEntryByteOffset, FreeDirEntryOffset = 0x00;
                FatxFcb Fcb = null, ParentFcb = this.DirectoryFcb, FileFcb = null;
                var FullName = new StringBuilder();
                FatxDirectoryEntry DirectoryEntry;

                int LastName = OldPath.Length - 1, FilenameLength = LastName + 1;
                do
                {
                    int idx = 0x00;
                    var File = new StringBuilder();

                    while (idx < FilenameLength)
                    {
                        File.Append(OldPath[idx++] + (idx < FilenameLength ? "\\" : string.Empty));
                    }

                    var FileStream = new FatxFileStream(this, File.ToString(), FileMode.Open, null);

                    if (LastName == (OldPath.Length - 1))
                    {
                        FileFcb = FileStream.Fcb;
                    }

                    this.FatxDeleteFile(FileStream.Fcb.ParentFCB, FileStream.DirectoryEntryByteOffset, LastName == (OldPath.Length - 1) ? false : true);

                    Fcb = FileStream.Fcb.ParentFCB;

                    FilenameLength--;

                } while (LastName-- > 1 && this.FatxIsDirectoryEmpty(Fcb) == 0x00);

                // Create new directories if necessary
                for (var x = 0; x < NewFileNameIndex; x++)
                {
                    if (FatxDevice.FatxIsValidFatFileName(NewPath[x]))
                    {
                        dwReturn = this.FatxLookupElementNameInDirectory(ParentFcb, NewPath[x], out DirectoryEntry, out DirectoryEntryByteOffset, out FreeDirEntryOffset);

                        FullName.Append(NewPath[x]);

                        if (dwReturn == 0x00)
                        {
                            Fcb = this.FatxCreateFcb(DirectoryEntry, ParentFcb, DirectoryEntryByteOffset, FullName.ToString());
                        }
                        else
                        {
                            Fcb = this.FatxCreateNewFile(ParentFcb, NewPath[x], FullName.ToString(), FatxDirectoryEntry.Attribute.Directory, 0, FreeDirEntryOffset);
                        }

                        if (!Fcb.IsDirectory)
                        {
                            throw new FatxException("created a file instead of a directory while moving a file. [0xC0000071]");
                        }

                        ParentFcb = Fcb;
                        FullName.Append("\\");                        
                    }
                    else
                    {
                        throw new FatxException(string.Format("invalid filename detected while creating {0}. [0xC0000033]", NewFileName));
                    }
                }

                dwReturn = this.FatxLookupElementNameInDirectory(ParentFcb, NewPath[NewFileNameIndex], out DirectoryEntry, out DirectoryEntryByteOffset, out FreeDirEntryOffset);

                Fcb = FileFcb;
                Fcb.ParentFCB = ParentFcb;
                Fcb.DirectoryEntryByteOffset = FreeDirEntryOffset;

                this.FatxUpdateDirectoryEntry(Fcb);

                this.FatxDereferenceFcb(Fcb);
            }
        }

        public bool FileExists(string FileName)
        {
            return FileExists(FileName, false);
        }
        public bool FolderExists(string FolderName)
        {
            return FileExists(FolderName, true);
        }
        private bool FileExists(string Filename, bool SearchForDirectory)
        {
            if (Filename == null || Filename == string.Empty)
                return false;

            var Name = FatxDevice.BreakStringByPath(Filename);
            FatxFcb parentFcb = this.DirectoryFcb, Fcb = null;
            var DirectoryEntry = new FatxDirectoryEntry();
            StringBuilder FullName = new StringBuilder();

            this.FatxReferenceFcb(parentFcb);

            uint DirEntryOffset, FreeDirEntOffset;
            for (int x = 0; x < Name.Length; x++)
            {
                if (FatxDevice.FatxIsValidFatFileName(Name[x]))
                {
                    uint dwReturn = this.FatxLookupElementNameInDirectory(parentFcb, Name[x], out DirectoryEntry, out DirEntryOffset, out FreeDirEntOffset);

                    if (dwReturn != 0 || ((x == (Name.Length - 1)) && (SearchForDirectory ? !DirectoryEntry.IsDirectory : DirectoryEntry.IsDirectory)))
                    {
                        this.FatxDereferenceFcb(parentFcb);
                        return false;
                    }

                    Fcb = this.FatxCreateFcb(DirectoryEntry, parentFcb, DirEntryOffset, FullName.ToString());

                    FullName.Append(Name[x]);

                    if (x != (Name.Length - 1))
                    {
                        parentFcb = Fcb;
                        FullName.Append("\\");
                    }
                }
                else
                {
                    throw new StfsException(string.Format("Invalid filename detected while opening {0}. [0xC0000033].", Filename));
                }
            }

            if (Fcb != null)
            {
                this.FatxDereferenceFcb(Fcb);
            }
            else
            {
                return false;
            }

            return true;
        }

        public List<FatxDirectoryEntry> GetNestedDirectoryEntries(string FilePath)
        {
            FatxDirectoryEntry DirectoryEntry;
            FatxFcb parentFcb = this.DirectoryFcb, Fcb = null;

            string[] Filename = FatxDevice.BreakStringByPath(FilePath);
            StringBuilder FullName = new StringBuilder();

            for (var x = 0; x < Filename.Length; x++)
            {
                if (FatxDevice.FatxIsValidFatFileName(Filename[x]))
                {
                    uint DirectoryEntryByteOffset, FreeDirEntryOffset;

                    uint dwReturn = this.FatxLookupElementNameInDirectory(parentFcb, Filename[x], out DirectoryEntry, out DirectoryEntryByteOffset, out FreeDirEntryOffset);

                    if (dwReturn != 0)
                    {
                        return null;
                    }

                    FullName.Append(Filename[x]);

                    Fcb = this.FatxCreateFcb(DirectoryEntry, parentFcb, DirectoryEntryByteOffset, FullName.ToString());

                    if (x != (Filename.Length - 1))
                    {
                        parentFcb = Fcb;
                        FullName.Append("\\");
                    }
                }
                else
                {
                    throw new FatxException(string.Format("{0} was found to be an invalid FAT name.", Filename[x]));
                }
            }
            return GetNestedDirectoryEntries(Fcb);
        }
        public List<FatxDirectoryEntry> GetNestedDirectoryEntries(FatxDirectoryEntry DirectoryEntry)
        {
            return GetNestedDirectoryEntries(DirectoryEntry.FilePath + DirectoryEntry.Filename);
        }
        private List<FatxDirectoryEntry> GetNestedDirectoryEntries(FatxFcb Fcb)
        {
            var Directories = new List<FatxDirectoryEntry>();
            if (Fcb.IsDirectory)
            {
                uint CurrentCluster = Fcb.FirstCluster, FileByteOffset = 0, ContinuousClusterCount = 0x00;
                do
                {
                    long PhysicalOffset = (Convert.ToInt64((CurrentCluster - 1) & 0xFFFFFFFF) << this.VolumeExtension.ClusterShift) + this.VolumeExtension.BackingClusterOffset;
                    uint ClusterSize = this.VolumeExtension.ClusterSize, ClusterIndex = 1, ReaderByteOffset = 0x00;

                    do
                    {
                        int readerPosition = 0;
                        var directoryReader = new EndianReader(new MemoryStream(this.FscMapBuffer(PhysicalOffset, false)), EndianType.BigEndian);
                        do
                        {
                            directoryReader.BaseStream.Position = readerPosition;
                            var dirEnt = new FatxDirectoryEntry(directoryReader, Fcb.FullFileName == null ? string.Empty : Fcb.FullFileName + "\\");

                            ReaderByteOffset += 0x40;
                            readerPosition += 0x40;

                            if (dirEnt.IsValid)
                            {
                                int index = Directories.FindIndex(dirEntry => dirEntry.FirstClusterNumber == dirEnt.FirstClusterNumber);
                                if (index == -1)
                                    Directories.Add(dirEnt);
                                else if (Directories[index].LastWriteTimeStamp < dirEnt.LastWriteTimeStamp)
                                    Directories[index].LastWriteTimeStamp = dirEnt.LastWriteTimeStamp;
                            }

                        } while (ReaderByteOffset < (0x1000 * ClusterIndex));

                        directoryReader.Close();
                        ClusterIndex++;
                        PhysicalOffset += 0x1000;

                    } while ((ClusterSize -= 0x1000) != 0);

                    FileByteOffset += this.VolumeExtension.ClusterSize;

                } while (this.FatxFileByteOffsetToCluster(Fcb, FileByteOffset, out CurrentCluster, out ContinuousClusterCount) == 0);
            }
            else
            {
                throw new FatxException("Attempted to retrieve nested directories for a non-directory.");
            }

            return Directories;
        }

        public List<FatxDirectoryEntry> GetRootDirectoryEntries()
        {
            return GetNestedDirectoryEntries(this.DirectoryFcb);
        }

        /* Static system functions */
        public static string[] BreakStringByPath(string String)
        {
            return String.Split('\\');
        }
        public static uint RoundToBlock(object value)
        {
            return (Convert.ToUInt32(value) + 0xFFF) & 0xFFFFF000;
        }

        public static int CountLeadingZerosWord(long Value)
        {
            return (int)(CountLeadingZeros(Value, 32));
        }
        private static long CountLeadingZerosDouble(long Value)
        {
            return CountLeadingZeros(Value, 64);
        }

        private static long CountLeadingZeros(long Value, int BitLength)
        {
            long leadingZeros = 0;
            for (var x = 0; x < BitLength; x++)
            {
                if ((Value & ((long)(1) << x)) == 0)
                    leadingZeros++;
                else
                    leadingZeros = 0;
            }
            return leadingZeros;
        }
        private static long ExtendSign(object Value, int BitLength)
        {
            long value = Convert.ToInt64(Value), extendIndex = 64 - BitLength;

            if (((value & (1 << (BitLength - 1))) != 0x00 ? 1 : 0) != 0x00)
            {
                for (var x = BitLength; x < 64; x++)
                {
                    value |= Convert.ToInt64(1) << x;
                }
            }
            return value;
        }
    }
    public class FatxFileStream : Stream
    {

        public FatxDevice Device;
        public FatxFcb Fcb;
        public FatxDirectoryEntry DirectoryEntry;
        public uint DirectoryEntryByteOffset;

        public override bool CanSeek { get { return true; } }
        public override bool CanRead { get { return true; } }
        public override bool CanWrite { get { return true; } }

        public override long Length { get { return this.Fcb.FileSize; } }

        public long _position;

        public override long Position
        {
            get { return _position; }
            set
            {
                Seek(value, SeekOrigin.Begin);
            }
        }

        public FatxFileStream(FatxDevice Device, string FileName, FileMode FileMode, object FileSize)
        {
            this.Device = Device;

            FatxFcb parentFcb = this.Device.DirectoryFcb;
            string[] Filename = FatxDevice.BreakStringByPath(FileName);
            StringBuilder FullName = new StringBuilder();
            uint FreeDirEntryOffset, dwReturn;

            this.Device.FatxReferenceFcb(parentFcb);

            switch (FileMode)
            {
                case FileMode.Open:

                    for (var x = 0; x < Filename.Length; x++)
                    {
                        if (FatxDevice.FatxIsValidFatFileName(Filename[x]))
                        {
                            dwReturn = this.Device.FatxLookupElementNameInDirectory(parentFcb, Filename[x], out this.DirectoryEntry, out DirectoryEntryByteOffset, out FreeDirEntryOffset);
                            if (dwReturn != 0)
                            {
                                throw new FatxException(string.Format("could not find {0} in directory listing.", Filename[x]));
                            }

                            FullName.Append(Filename[x]);

                            this.Fcb = this.Device.FatxCreateFcb(this.DirectoryEntry, parentFcb, DirectoryEntryByteOffset, FullName.ToString());

                            if (x != (Filename.Length - 1))
                            {
                                parentFcb = Fcb;
                                FullName.Append("\\");
                            }
                        }
                        else
                        {
                            throw new FatxException(string.Format("{0} was found to be an invalid FAT name.", Filename[x]));
                        }
                    }
                    break;
                case FileMode.CreateNew:

                    for (var i = 0; i < Filename.Length; i++)
                    {
                        if (FatxDevice.FatxIsValidFatFileName(Filename[i]))
                        {
                            dwReturn = this.Device.FatxLookupElementNameInDirectory(parentFcb, Filename[i], out this.DirectoryEntry, out DirectoryEntryByteOffset, out FreeDirEntryOffset);

                            FullName.Append(Filename[i]);
                            bool IsFile = i != (Filename.Length - 1);

                            if (dwReturn == 0)
                            {
                                this.Fcb = this.Device.FatxCreateFcb(this.DirectoryEntry, parentFcb, DirectoryEntryByteOffset, FullName.ToString());
                            }
                            else
                            {
                                this.Fcb = this.Device.FatxCreateNewFile(parentFcb, Filename[i], FullName.ToString(), IsFile ? FatxDirectoryEntry.Attribute.Directory : FatxDirectoryEntry.Attribute.Normal, IsFile ? 0 : Convert.ToUInt32(FileSize), FreeDirEntryOffset);
                            }

                            if (IsFile)
                            {
                                parentFcb = Fcb;
                                FullName.Append("\\");
                            }
                        }
                        else
                        {
                            throw new StfsException(string.Format("Invalid filename detected while creating {0}. [0xC0000033].", Filename));
                        }
                    }
                    break;
                default:
                    throw new FatxException(string.Format("attempted to access {0} in an unsupported mode.", FileName));
            }
        }
        public FatxFileStream(FatxDevice fatxDevice, FatxDirectoryEntry DirectoryEntry) : this(fatxDevice, DirectoryEntry.FilePath + DirectoryEntry.Filename, FileMode.Open, null)
        {            
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
                    _position = this.Length - offset;
                    break;
            }

            return _position;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (count > 0)
            {
                EndianIO io = new EndianIO(buffer, EndianType.BigEndian, true);

                uint ReadPosition = Convert.ToUInt32(_position), ReadLength = Convert.ToUInt32(count);

                if (this.Device.FscTestForFullyCachedIo(ReadPosition, ReadLength))
                {
                    this.Device.FatxFullyCachedSynchronousIo(this.Fcb, FatxIO.Read, ReadPosition, io, ReadLength, false);
                }
                else
                {
                    this.Device.FatxPartiallyCachedSynchronousIo(this.Fcb, FatxIO.Read, ReadPosition, io, ReadLength, false);
                }

                _position += count;
            }
            return count;
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (count > 0)
            {
                long ExtendedLength = (this.Device.VolumeExtension.ClusterSize + ((Position + count)) - 1) & ~(this.Device.VolumeExtension.ClusterSize - 1);

                if (Position > ExtendedLength)
                {
                    throw new FatxException("invalid stream position found. [0xC000007F]");
                }
                else if (Fcb.EndOfFile == 0xFFFFFFFF)
                {
                    uint ReturnedCluster, ContinuousClusterCount;

                    uint dwReturn = this.Device.FatxFileByteOffsetToCluster(Fcb, 0xFFFFFFFF, out ReturnedCluster, out ContinuousClusterCount);

                    if (dwReturn != 0x00 && dwReturn != 0xC0000011)
                    {
                        throw new FatxException(string.Format("FatxFileByteOffsetToPhysicalByteOffset failed with [0x{0:X8}].", dwReturn));
                    }
                }

                if (Fcb.FileSize > Fcb.EndOfFile)
                {
                    throw new FatxException(string.Format("the file size for file {0} was greater than the allocated file size [0xC0000102]", Fcb.FileName));
                }
                else if ((Fcb.FileSize <= Fcb.EndOfFile) && ((Position + count) > Fcb.EndOfFile))
                {
                    this.Device.FatxExtendFileAllocation(Fcb, ExtendedLength);
                }

                var io = new EndianIO(buffer, EndianType.BigEndian, true);

                uint WritePosition = Convert.ToUInt32(_position), WriteLength = Convert.ToUInt32(count);

                if (this.Device.FscTestForFullyCachedIo(WritePosition, WriteLength))
                {
                    this.Device.FatxFullyCachedSynchronousIo(this.Fcb, FatxIO.Write, WritePosition, io, WriteLength, false);
                }
                else
                {
                    this.Device.FatxPartiallyCachedSynchronousIo(this.Fcb, FatxIO.Write, WritePosition, io, WriteLength, false);
                }

                _position += count;
            }
        }
        public override void SetLength(long Length)
        {
            this.Device.FatxSetAllocationSize(this.Fcb, Convert.ToUInt32(Length), false, false);

            this.Device.FatxSetEndOfFileInformation(this.Fcb, new WinFile.FileEndOfFileInfo()
            {
                EndOfFile = new WinFile.LargeInteger()
                {
                    QuadPart = Length
                }
            });
        }
        public override void Flush()
        {

        }
        public override void Close()
        {
            if (Fcb.ReferenceCount != 0x00)
            {
                this.Device.FatxDereferenceFcb(this.Fcb);
            }

            if (this.Fcb.IsMarkedForDeletion)
            {
                this.Device.FatxFinalizeDeleteOnClose(this.Fcb);
            }
            else if (this.Fcb.IsModified)
            {
                this.Device.FatxUpdateDirectoryEntry(this.Fcb);
            }
        }

        public uint SetInformation(WinFile.FileInformationClass _FileInformationClass, object FileInformation)
        {
            uint dwReturn = 0x00000000;

            return dwReturn;

        }
        public byte[] ToArray()
        {
            byte[] Data = new byte[this.Fcb.FileSize];

            Read(Data, 0, Data.Length);

            return Data;
        }
    }
}