using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using ODFX;

namespace XContent
{
    public class SvodDevice : GdfDevice
    {
        public MultiFileIO IO;

        private List<SvodCacheElement> BlockCache;
        private byte[][] CacheData, CacheHashData;

        private byte[] FirstFragmentHash;
        private long DataStartOffset, Eof, DataBlockLength;
        private byte FragmentCount;
        private byte TopLevelHashCount = 0x00;
        private bool HasEnhancedGDFLayout;

        private byte CacheHeadIndex;
        public readonly uint[] SvodDataBlocksPerHashTreeLevel = new uint[] { 0xCC, 0xA290, 0x818AC0 };

        public SvodDevice()
        {

        }
        ~SvodDevice()
        {
            if (this.IO != null && this.IO.Opened)
                this.IO.Close();
        }

        public void SvodCreateDevice(SvodCreatePacket CreatePacket)
        {
            this.IO = new MultiFileIO(Directory.GetFiles(CreatePacket.DataFileDirectory).ToList(), EndianType.BigEndian, true, true);

            if (CreatePacket.Descriptor == null)
            {
                throw new SvodException("invalid volume descriptor.");
            }
            else if (CreatePacket.FSCache == null)
            {
                throw new SvodException("invalid file system cache.");
            }

            var Descriptor = CreatePacket.Descriptor;

            if (Descriptor.DescriptorLength != 0x24)
            {
                throw new SvodException(string.Format("invalid volume descriptor length 0x{0:X8}.", CreatePacket.Descriptor.DescriptorLength));
            }
            else if (Descriptor.BlockCacheElementCount == 0x00)
            {
                throw new SvodException(string.Format("invalid cache element count."));
            }
            else if (Descriptor.Features.MustBeZeroForFutureUsage != 0x00)
            {
                throw new SvodException(string.Format("unsupported device features 0x{0:X2} [0xC0000001].", Descriptor.FeaturesFlags & 0x3F));
            }

            uint StartingDataBlock = 0x00;
            if (((Descriptor.FeaturesFlags >> 6) & 0x1) == 0x01)
            {
                StartingDataBlock = Descriptor.StartingDataBlock;
                if (StartingDataBlock <= 0x10)
                {
                    throw new SvodException(string.Format("unexpected starting data block: 0x{0:X8} [0xC0000001].", StartingDataBlock));
                }
            }
            else
            {
                StartingDataBlock = BitConverter.GetBytes(Descriptor.StartingDataBlock)[0];
            }

            uint NumberOfDataBlocks = Descriptor.NumberOfDataBlocks;
            if (((Descriptor.FeaturesFlags >> 26) & 0x1) == 0x01)
            {
                NumberOfDataBlocks++;
            }

            ushort FragmentCount = (ushort)((NumberOfDataBlocks + 0xA1C3) / 0xA1C4);
            if (FragmentCount > 0x38)
            {
                throw new SvodException(string.Format("fragment count too high: 0x{0:X4}", FragmentCount));
            }

            if (Descriptor.BlockCacheElementCount != 0x00)
            {
                // init block cache
                this.BlockCache = new List<SvodCacheElement>();
                this.CacheData = new byte[Descriptor.BlockCacheElementCount][];
                this.CacheHashData = new byte[0x38][];

                for (var x = 0; x < Descriptor.BlockCacheElementCount; x++)
                {
                    this.BlockCache.Add(new SvodCacheElement()
                    {
                        BlockNumber = 0x00,
                        Type = (byte)0xFF,
                        NextIndex = (byte)((x == Descriptor.BlockCacheElementCount - 1) ? 0x00 : (x + 1)),
                        PreviousIndex = (byte)(x == 0 ? (Descriptor.BlockCacheElementCount + 0xFF) : (x + 1) + 0xFE)
                    });
                    this.CacheData[x] = new byte[0x1000];
                }
                for(var x = 0; x < 0x38; x++)
                    this.CacheHashData[x] = new byte[0x14];

                this.CacheHashData[0] = Descriptor.FirstFragmentHashEntry.Hash;
            }

            Array.Copy(Descriptor.FirstFragmentHashEntry.Hash, this.FirstFragmentHash = new byte[0x14], 0x14);

            this.DataBlockLength = (long)NumberOfDataBlocks << 0x0C;
            this.DataStartOffset = Horizon.Functions.Global.ROTL64(StartingDataBlock, 0x0C) & 0xFFFFFFFF000;
            this.Eof = this.DataStartOffset + this.DataBlockLength;
            this.HasEnhancedGDFLayout = Convert.ToBoolean(Descriptor.Features.HasEnhancedGDFLayout);
            this.FragmentCount = (byte)FragmentCount;
        }

        public override void DeviceControl(ulong ControlCode, ref byte[] OutputBuffer)
        {
            switch (ControlCode)
            {
                case 0x2404C:
                    {
                        OutputBuffer.WriteInt32(0x04, 0x800);
                        OutputBuffer.WriteInt32(0x00, (int)(this.DataBlockLength >> 0x0B));
                    }
                    break;
            }
        }

        public override void Read(EndianIO ReadIO, long Length, long FileByteOffset)
        {
            if (Length != 0x00)
            {
                int err = 0;
                long numberOfBytesToRead = Length, FileOffset = 0, l_len = numberOfBytesToRead;
                do
                {
                    if (l_len != 0x00)
                    {
                        if (FileByteOffset < this.DataStartOffset)
                        {
                            if (this.HasEnhancedGDFLayout && (((((FileByteOffset >> 0x0B) >> 52) + FileByteOffset) >> 0x0C) == 0x10))
                            {
                                long bytesToRead = 0x1000 - (FileByteOffset & 0xFFF);
                                numberOfBytesToRead = l_len > bytesToRead ? bytesToRead : numberOfBytesToRead;
                            }
                            else
                            {
                                ReadIO.Out.Write(new byte[0x800]);
                                FileOffset += 0x800;
                                l_len -= 0x800;

                                continue;
                            }
                        }
                        else
                        {
                            FileOffset = this.HasEnhancedGDFLayout ? (FileByteOffset - DataStartOffset) + 0x1000 : FileByteOffset - DataStartOffset;
                        }

                        if ((numberOfBytesToRead < 0x1000) && (((ulong)(((numberOfBytesToRead & 0xFFFFFFFF) + FileByteOffset) ^ (FileByteOffset + 0xFFF)) & 0xFFFFFFFFFFFFF000) == 0x00))
                        {
                            err = this.SvodFullyCachedRead(ReadIO, FileOffset, numberOfBytesToRead);
                        }
                        else
                        {
                            err = this.SvodPartiallyCachedRead(ReadIO, FileOffset, numberOfBytesToRead);
                        }
                        l_len -= numberOfBytesToRead;
                        FileOffset += numberOfBytesToRead;
                    }
                    else break;
                } while (err >= 0x00);
            }
        }

        private int SvodPartiallyCachedRead(EndianIO ReadIO, long Position, long Length)
        {
            int err = 0x00;
            long l_pos = Position & 0xFFF, t_pos = Position, l_len = Length;
            if (l_pos != 0x00)
            {
                l_len = 0x1000 - l_pos;
                if(l_len >= Length)
                    throw new SvodException(string.Format("invalid length supplied for partial cache reading: 0x{0:X8}.", Length));

                this.SvodFullyCachedRead(ReadIO, t_pos, l_len);
                t_pos += l_len & 0xFFFFFFFF;
                l_len = Length - l_len;
            }
            
            while (l_len >= 0x1000)
            {
                uint blockNum = (uint)(((t_pos >> 0x0B) >> 52) + t_pos) >> 0x0C, numberOfBytesToRead = (uint)(l_len & 0xFFFFF000), nBlockNum = (blockNum % SvodDataBlocksPerHashTreeLevel[0]),
                    remainderBytes = (0xCC - nBlockNum) << 0x0C, fragmentIndex = 0x00;
                numberOfBytesToRead = numberOfBytesToRead > remainderBytes ? remainderBytes : numberOfBytesToRead;

                byte[] hashBlock = null;

                if (this.SvodMapBlock(blockNum, 0x01, out hashBlock) >= 0x00)
                {
                    byte[] dataBlock = this.SvodReadBackingBlocks(blockNum, 0x00, numberOfBytesToRead >> 0x0C, out fragmentIndex);
                    if (dataBlock != null)
                    {
                        t_pos += (numberOfBytesToRead & 0xFFFFFFFF);
                        l_len -= numberOfBytesToRead;
                        ReadIO.Out.Write(dataBlock);
                        uint pHashIdx = nBlockNum * 0x14, pDataBlockIdx = 0x00;
                        do
                        {
                            byte[] rgbHash = hashBlock.ReadBytes(pHashIdx, 0x14);
                            if (!HorizonCrypt.ArrayEquals(rgbHash, HorizonCrypt.XeCryptSha(dataBlock.ReadBytes(pDataBlockIdx, 0x1000), 0x1000, null, 0x00, null, 0x00)))
                            {
                                throw new SvodException(string.Format("hash mismatch for block 0x{0:X8} [0xC0000032].", blockNum));
                            }
                            pHashIdx += 0x14;
                            pDataBlockIdx += 0x1000;
                        } while ((numberOfBytesToRead -= 0x1000) > 0x00);
                    }
                }
            }
               
            if (l_len != 0x00)
            {
                return this.SvodFullyCachedRead(ReadIO, t_pos, l_len);
            }
        
            return err;
        }

        private int SvodFullyCachedRead(EndianIO ReadIO, long Position, long Length) 
        {
            int err = 0x00;
            if (Length != 0x00)
            {
                long l_pos = Position & 0xFFF, l_len = (0x1000 - l_pos) > Length ? Length : (0x1000 - l_pos);
                byte[] dataBlock = null;

                do
                {
                    if (Length > 0x00 && (err = this.SvodMapBlock((uint)((((Position >> 0x0B) >> 52) + Position) >> 0x0C) & 0xFFFFFFFF, 0x00, out dataBlock)) >= 0x00)
                    {
                        ReadIO.Out.Write(dataBlock.ReadBytes(l_pos, l_len));
                        Length -= l_len;
                        Position += l_len & 0xFFFFFFFF;
                    }
                } while (Length > 0x00);
            }
            return err;
        }

        private int SvodMapBlock(uint BlockNumber, int ElementType, out byte[] DataBlock) 
        {
            DataBlock = null;
            int hashBlockNum = -1, element_t = ElementType, err = -1;
            byte[] dbHashEntry = null, pbDataBlock = null;

            if (this.SvodMapExistingBlock(BlockNumber, ElementType, out DataBlock) == 0x00)
            {
                while (ElementType != 0x02)
                {
                    ElementType++;
                    if((err = this.SvodMapExistingBlock(BlockNumber, ElementType, out DataBlock)) != 0x00)
                        goto Map_New_Block;
                }

                hashBlockNum = (int)(BlockNumber / 0xA1C4);
                do
                {
                    if(this.TopLevelHashCount >= hashBlockNum)
                        break;

                }while (this.SvodMapNewBlock((uint)(this.TopLevelHashCount * 0xA1C4), 0x02, this.CacheHashData[this.TopLevelHashCount], out pbDataBlock) >= 0x00) ;

                dbHashEntry = this.CacheHashData[hashBlockNum];                    
                
                err = this.SvodMapNewBlock(BlockNumber, ElementType, dbHashEntry, out DataBlock);
            Map_New_Block:
                if (err >= 0x00)
                {
                    do
                    {
                        if (ElementType == element_t)
                            break;

                        int hashIndex = -1;
                        if (ElementType == 0x02)
                        {
                            hashIndex = (int)((BlockNumber / SvodDataBlocksPerHashTreeLevel[0]) % 0xCB);
                        }
                        else
                        {
                            hashIndex = (int)(BlockNumber % SvodDataBlocksPerHashTreeLevel[0]);
                        }
                        dbHashEntry = DataBlock.ReadBytes(hashIndex * 0x14, 0x14);
                        ElementType--;
                    } while (this.SvodMapNewBlock(BlockNumber, ElementType, dbHashEntry, out DataBlock) >= 0x00);
                }
            }

            return 0x00;
        }

        private int SvodMapNewBlock(uint BlockNumber, int ElementType, byte[] BlockHash, out byte[] DataBlock) 
        {
            DataBlock = null;
            uint blockNum = BlockNumber, FragmentIndex;

            switch (ElementType)
            {
                case 0x00:
                    break;
                case 0x01:
                    blockNum = (BlockNumber / SvodDataBlocksPerHashTreeLevel[0]) * SvodDataBlocksPerHashTreeLevel[0];
                    break;
                case 0x02:
                    blockNum = (BlockNumber / 0xA1C4) * 0xA1C4;
                    break;
            }
            if(ElementType > 0x00)
                blockNum = BlockNumber - (BlockNumber - blockNum);

            int cacheIndex = this.BlockCache[this.CacheHeadIndex].PreviousIndex;
            var cacheEntry = this.BlockCache[cacheIndex];
            DataBlock = this.CacheData[cacheEntry.NextIndex] = this.SvodReadBackingBlocks(blockNum, ElementType, 0x01, out FragmentIndex);

            if (!HorizonCrypt.ArrayEquals(HorizonCrypt.XeCryptSha(DataBlock, null, null), BlockHash))
                throw new SvodException(string.Format("hash mismatch for block number 0x{0:X8} [0xC0000032].", blockNum));

            this.SvodMoveBlockCacheEntry(cacheIndex);
            cacheEntry.BlockNumber = blockNum;
            cacheEntry.Type = (byte)ElementType;

            if (ElementType == 0x02)
            {
                if (TopLevelHashCount == FragmentIndex && FragmentIndex < 0x38)
                {
                    this.CacheHashData[(1 + FragmentIndex)] = DataBlock.ReadBytes(0xFDC, 0x14);
                    TopLevelHashCount++;
                }
            }

            return 0x00;
        }

        private int SvodMapExistingBlock(uint BlockNumber, int ElementType, out byte[] BlockData)
        {
            uint blockNum = 0xFFFFFFFF;
            BlockData = null;

            switch (ElementType)
            {
                case 0x00:
                    blockNum = (BlockNumber / SvodDataBlocksPerHashTreeLevel[0]) * SvodDataBlocksPerHashTreeLevel[0];
                    break;
                case 0x01:
                case 0x02:
                    blockNum = (BlockNumber / 0xA1C4) * 0xA1C4;
                    break;
            }
            blockNum = BlockNumber - (BlockNumber - blockNum);
            int cacheIndex = this.CacheHeadIndex;
            SvodCacheElement cacheEntry;
            do
            {
                cacheEntry = this.BlockCache[cacheIndex];
                if (cacheEntry.BlockNumber == blockNum && cacheEntry.Type == ElementType)
                {
                    this.SvodMoveBlockCacheEntry(cacheEntry.NextIndex);
                    BlockData = this.CacheData[cacheEntry.NextIndex];

                    return 0x01;
                }
                cacheIndex = cacheEntry.NextIndex;
            } while (this.CacheHeadIndex != cacheIndex);
            return 0x00;
        }

        private byte[] SvodReadBackingBlocks(uint BlockNumber, int BlockLevel, uint BlockCount, out uint FragmentIndex)
        {
            if(BlockLevel != 0x00 && BlockCount != 0x01)
                throw new SvodException(string.Format("invalid block count {0} detected while reading hash block 0x{1:X8}.", BlockCount, BlockNumber));
            uint blockNum = 0xFFFFFFFF, blockCountPerLevel = 0xA290;

            switch (BlockLevel)
            {
                case 0x00:
                    blockNum = (((BlockNumber / 0xCC) + (BlockNumber / 0xA1C4)) + BlockNumber) + 2;
                    break;
                case 0x01:
                    blockNum = ((BlockNumber / 0xCC) * 0xCD) + (BlockNumber / 0xA1C4) + 1;
                    break;
                case 0x02:
                    blockNum = (BlockNumber / 0xA1C4) * blockCountPerLevel;
                    break;
                default:
                    throw new SvodException(string.Format("invalid block level {0} detected while reading block 0x{1:X8}.", BlockLevel, BlockNumber));
            }

            FragmentIndex = blockNum / blockCountPerLevel;
            long Position = ((blockNum - (FragmentIndex * blockCountPerLevel)) << 0x0C) & 0xFFFFFFFF;
            this.SvodFetchFragmentHandle(FragmentIndex);

            // read block data
            this.IO.In.SeekTo(Position);
            byte[] blockData = this.IO.In.ReadBytes(BlockCount << 0x0C);

            if (blockData.Length != (BlockCount << 0x0C))
                throw new SvodException("invalid block length read 0x{0:X8}:0x{1:X8} [0xC0000185].");

            return blockData;
        }

        private void SvodMoveBlockCacheEntry(int blockIndex)
        {
            if (blockIndex != CacheHeadIndex)
            {
                var cacheEntry = this.BlockCache[this.CacheHeadIndex];

                if (cacheEntry.PreviousIndex != blockIndex)
                {
                    byte bIndex = (byte)(blockIndex & 0xFF);

                    var curEntry = this.BlockCache[blockIndex];
                    var updEntry = this.BlockCache[curEntry.PreviousIndex];

                    updEntry.NextIndex = curEntry.NextIndex;
                    updEntry = this.BlockCache[curEntry.NextIndex];
                    updEntry.PreviousIndex = curEntry.PreviousIndex;
                    curEntry.NextIndex = this.CacheHeadIndex;
                    curEntry.PreviousIndex = cacheEntry.PreviousIndex;
                    updEntry = this.BlockCache[cacheEntry.PreviousIndex];

                    updEntry.NextIndex = bIndex;
                    cacheEntry.PreviousIndex = bIndex;
                }

                this.CacheHeadIndex = (byte)blockIndex;
            }
        }

        private void SvodFetchFragmentHandle(uint FragmentIndex)
        {
            this.IO.openFile((int)FragmentIndex);
        }
    }
}