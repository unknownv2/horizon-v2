using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ModernWarfare3
{
    public class CampaignSave
    {
        private EndianIO IO, SaveIO;
        private int Dataoffset = 0x480, SizeOffset = 0x44C;

        public CampaignSave(EndianIO io)
        {
            Read(io);
        }
        public CampaignSave(EndianIO io, int dataOffset, int sizeOffset)
        {
            Dataoffset = dataOffset;
            SizeOffset = sizeOffset;
            Read(io);
        }

        private void Read(EndianIO io)
        {
            if (io != null)
                IO = io;

            uint size = this.IO.In.SeekNReadUInt32(SizeOffset);
            this.IO.In.SeekTo(Dataoffset);
            byte[] SaveData = this.IO.In.ReadBytes(size);
            uint StoredSum = this.IO.In.SeekNReadUInt32(0x08);
            uint CalcSum = CalculateChecksum(SaveData);

            if (CalcSum != StoredSum)
            {
                throw new Exception("save data has been tampered with");
            }

            this.SaveIO = new EndianIO(SaveData, EndianType.BigEndian, true);
        }
        private EndianIO MoveToSegment(int Segment)
        {
            this.SaveIO.In.SeekTo(0);
            while (Segment-- != 0)
            {
                uint segmentlen = this.SaveIO.In.ReadUInt32();
                this.SaveIO.In.BaseStream.Position += (segmentlen - 4);
            }
            return new EndianIO(DecompressSegment(new EndianReader(this.SaveIO.In.ReadBytes(this.SaveIO.In.ReadInt32() - 4), EndianType.BigEndian)), EndianType.BigEndian, true);
        }
        public byte[] DecompressSegment(EndianReader SegmentReader)
        {
            MemoryStream stream = new MemoryStream();
            int compression_ctr = 0, szSegment = (int)SegmentReader.BaseStream.Length, g_zeroCount = 0, copy_ctr = 0;
            bool Exit_Decompression = false;
            do
            {
                if (compression_ctr != 0)
                {
                    do
                    {
                        compression_ctr--;
                        if (SegmentReader.BaseStream.Position >= szSegment)
                        {
                            goto B_Exit_Decompression;
                        }
                        stream.WriteByte(SegmentReader.ReadByte());

                    } while (compression_ctr != 0);
                    copy_ctr = g_zeroCount;
                }

                while (copy_ctr != 0)
                {
                    copy_ctr--;
                    stream.WriteByte(0);
                    g_zeroCount = copy_ctr;
                }
                if (SegmentReader.BaseStream.Position >= szSegment)
                {
                    goto B_Exit_Decompression;
                }
                byte val = SegmentReader.ReadByte();
                if ((val & 0xC0) == 0)
                {
                    compression_ctr = (val & 0x3F) + 1;
                    copy_ctr = 0;
                }
                else
                {
                    copy_ctr = (val & 0x3F) + 1;
                    compression_ctr = (val >> 6);
                }
                g_zeroCount = copy_ctr;
            } while (!Exit_Decompression);
 B_Exit_Decompression:
            return stream.ToArray();
        }
        private byte[] CompressSegment(EndianReader SegmentReader)
        {
            var io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            int g_zeroCount = 0, g_compCount = 0, g_cacheBufferLen = -1, g_cacheSize = 1, g_cacheWriteLen = (int)SegmentReader.BaseStream.Length,
                g_readIdx = 0, g_writeIdx = 0;
            byte tval = 0;
            do
            {
                if (g_compCount == 0)
                {
                    do
                    {
                        tval = SegmentReader.ReadByte();
                        if (g_cacheBufferLen == 0x3F)
                        {
                            io.Out.SeekTo(g_writeIdx);
                            io.Out.WriteByte(0x3F);
                            g_writeIdx += g_cacheSize;

                            if ((g_writeIdx + 0x41) > 0x1C0000)
                            {
                                throw new Exception("invalid cache size detected.");
                            }

                            io.Out.SeekTo(g_writeIdx + 1);
                            io.Out.WriteByte(tval);

                            g_cacheBufferLen = 0;
                            g_compCount = 0;
                            g_cacheSize = 2;

                            g_zeroCount = (tval == 0) ? 1 : 0;
                        }
                        else
                        {
                            if (tval != 0)
                            {
                                g_zeroCount = 0;
                            }
                            else
                            {
                                g_zeroCount++;
                                if (g_cacheBufferLen <= 2 && g_cacheBufferLen >= 0)
                                {
                                    g_compCount = (g_cacheBufferLen + 1);
                                    goto restart_comp_loop;
                                }
                                else if (g_cacheBufferLen > 2 && g_zeroCount >= 3)
                                {
                                    int cidx = g_cacheBufferLen + 0xFD;
                                    g_cacheSize -= 3;
                                    io.In.SeekTo(g_writeIdx + g_cacheSize);
                                    tval = io.In.ReadByte();

                                    io.Out.SeekTo(g_writeIdx);
                                    io.Out.WriteByte(cidx);

                                    g_writeIdx += (g_cacheSize);

                                    if ((g_writeIdx + 0x41) > 0x1C0000)
                                    {
                                        throw new Exception("invalid cache size detected.");
                                    }

                                    g_compCount = 1;
                                    g_zeroCount = 0;

                                    io.Out.SeekTo(g_writeIdx + 1);
                                    io.Out.WriteByte(tval);

                                    g_cacheBufferLen = 2;
                                    g_cacheSize = 2;

                                    goto loop_0;
                                }
                            }
                            io.Out.SeekTo(g_writeIdx + g_cacheSize);
                            io.Out.WriteByte(tval);
                            g_cacheBufferLen++;
                            g_cacheSize++;
                        }
                    } while (++g_readIdx < g_cacheWriteLen);
                    if (g_readIdx >= g_cacheWriteLen)
                        break;
                }
                else
                {
                    do
                    {
                        tval = SegmentReader.ReadByte();
                        if (tval != 0)
                        {
                            g_zeroCount = 0;
                            break;
                        }
                        else
                        {
                            g_zeroCount++;
                            if (g_cacheBufferLen == 0x3F)
                            {
                                g_zeroCount = 1;
                                break;
                            }
                            else
                            {
                                g_cacheBufferLen++;
                            }
                        }
                    } while (++g_readIdx < g_cacheWriteLen);
                    if (g_readIdx >= g_cacheWriteLen)
                        break;

                    g_cacheBufferLen += (int)((g_compCount << 6) & 0xFFFFFFFF);
                    io.Out.SeekTo(g_writeIdx);
                    io.Out.WriteByte(g_cacheBufferLen);
                    g_writeIdx += (g_cacheSize);

                    if ((g_writeIdx + 0x41) > 0x1C0000)
                    {
                        throw new Exception("invalid cache size detected.");
                    }
                    io.Out.SeekTo(g_writeIdx + 1);
                    io.Out.WriteByte(tval);

                    g_compCount = 0;
                    g_cacheSize = 2;
                }
            restart_comp_loop:
                g_cacheBufferLen = 0;
            loop_0:
                g_readIdx++;
            } while (g_readIdx < g_cacheWriteLen);

            int final_idx = (int)io.Stream.Position;
            if (g_cacheSize > 1)
            {
                g_cacheBufferLen += (g_compCount << 6);
                io.Out.SeekTo(g_writeIdx);
                io.Out.WriteByte(g_cacheBufferLen);
                g_writeIdx += (g_cacheSize);
            }

            if ((g_writeIdx + 0x41) > 0x1C0000)
            {
                throw new Exception("invalid cache size detected.");
            }

            io.Stream.Flush();
            if (final_idx != g_writeIdx)
            {
                throw new Exception("invalid cache size detected.");
            }

            io.SeekTo(0x00);
            return io.In.ReadBytes(g_writeIdx);
        }
        public byte[] Save(byte[] SaveData)
        {
            var io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            this.IO.In.SeekTo(0);
            byte[] header = this.IO.In.ReadBytes(Dataoffset);
            io.Stream.SetLength(SaveData.Length + Dataoffset);
            io.Out.Write(header);
            io.Out.SeekNWrite(SizeOffset, SaveData.Length);
            io.Out.SeekNWrite(0x08, CalculateChecksum(SaveData));
            io.Out.SeekTo(Dataoffset);
            io.Out.Write(SaveData);
            io.Out.Flush();
            this.IO.Close();
            return io.ToArray();
        }
        public byte[] ExtractSegment(int Index)
        {
            return this.MoveToSegment(Index).ToArray();
        }
        public byte[] InjectSegment(byte[] SegmentData)
        {
            return CompressSegment(new EndianReader(SegmentData, EndianType.BigEndian));
        }
        public byte[] CopySegment(int Segment)
        {
            this.SaveIO.In.SeekTo(0);
            while (Segment-- != 0)
            {
                uint segmentlen = this.SaveIO.In.ReadUInt32();
                this.SaveIO.In.BaseStream.Position += (segmentlen - 4);
            }
            return this.SaveIO.In.ReadBytes(this.SaveIO.In.ReadInt32() - 4);
        }
        /*
        private void ReadSegment2()
        {
            var r = this.MoveToSegment(2).In;
            for(int x = 0; x < 5; x++)
                r.ReadByte();

            int a = r.ReadInt32();
            a = r.ReadInt32();
            r.ReadInt32();
            r.ReadBytes(0x34);
            r.ReadInt16();
            r.ReadInt16();
            r.ReadInt32();
            r.ReadInt32();
            r.ReadInt32();
            r.ReadInt32();
            r.ReadInt32();
            r.ReadInt32();
            r.ReadBytes(0x40);
            r.ReadInt64();
            r.ReadInt64();
            r.ReadInt64();
            r.ReadInt32();
            r.ReadInt32();
            r.ReadInt16();
            r.ReadBytes(0x18);

            read_struct1(0x400, r);
            read_struct1(0x20, r);
            read_struct1(0x200, r);
            read_struct1(0x100, r);
            read_struct1(0x200, r);
            read_struct1(0x80, r);
            read_struct1(0x4B0, r);
            read_struct1(0x02, r);
            read_struct1(0x0D, r);
            int len;
            if ((len = r.ReadInt32()) >= 0)
            {
                do
                {
                    //read dvars
                    if (len < 0x400) 
                        r.ReadString(len);
                    len = r.ReadInt32();
                    if (len < 0x400) 
                        r.ReadString(len);
                } while ((len = r.ReadInt32()) != -1);
            }
            r.ReadBytes(0xAC00);
            r.ReadInt32();
            r.ReadInt32();
            r.ReadInt32();
            r.ReadInt32();
            int g_player_max_health = r.ReadInt32();
        }
        private void read_struct1(int count, EndianReader r)
        {
            for (int x = 0; x < count; x++)
            {
                int val;
                if ((val = r.ReadInt16()) != 0)
                {
                    if (val < 0x400)
                        r.ReadBytes(val);
                }
            }
        }
        */
        private uint CalculateChecksum(byte[] Data)
        {
            Adler32 adler = new Adler32();
            adler.Update(Data);
            return (uint)adler.Value;
        }
    }
    public class MemFile
    {
        private EndianIO IO;
        private int SegmentIndex, g_zeroCount;
        
        public MemFile()
        {
            this.IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
        }
        public void StartSegment(int idx)
        {
            this.SegmentIndex = idx;
        }
        public void WriteSegment(byte[] SegmentData)
        {
            this.IO.Out.Write(SegmentData.Length + 4);
            this.IO.Out.Write(SegmentData);
        }
        public byte[] ToArray()
        {
            if (IO != null && IO.Length > 0)
                return this.IO.ToArray();
            else
                throw new Exception("invalid I/O detected.");
        }
        public int ReadData(byte[] buffer, int offset, int count)
        {
            MemoryStream stream = new MemoryStream(buffer);
            stream.Position = offset;
            int compression_ctr = 0, copy_count = count, copy_ctr = 0, ncount = 0;
            do
            {
                if (compression_ctr != 0)
                {
                    do
                    {
                        compression_ctr--;
                        copy_count--;
                        stream.WriteByte(IO.In.ReadByte());
                        ncount++;
                        if (copy_count == 0)
                        {
                            return ncount;
                        }
                    } while (compression_ctr != 0);
                    copy_ctr = g_zeroCount;
                }

                while (copy_ctr != 0)
                {
                    if (copy_count != 0)
                    {
                        copy_ctr--;
                        stream.WriteByte(0);
                        ncount++;
                        copy_count--;
                        g_zeroCount = copy_ctr;
                        if (copy_count == 0)
                        {
                            return ncount;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                byte val = IO.In.ReadByte();
                if ((val & 0xC0) == 0)
                {
                    compression_ctr = (val & 0x3F) + 1;
                    copy_ctr = 0;
                }
                else
                {
                    copy_ctr = (val & 0x3F) + 1;
                    compression_ctr = (val >> 6);
                }
                g_zeroCount = copy_ctr;
            } while (true);
        }
    }

	
	/// <summary>
	/// Computes Adler32 checksum for a stream of data. An Adler32
	/// checksum is not as reliable as a CRC32 checksum, but a lot faster to
	/// compute.
	/// 
	/// The specification for Adler32 may be found in RFC 1950.
	/// ZLIB Compressed Data Format Specification version 3.3)
	/// 
	/// 
	/// From that document:
	/// 
	///      "ADLER32 (Adler-32 checksum)
	///       This contains a checksum value of the uncompressed data
	///       (excluding any dictionary data) computed according to Adler-32
	///       algorithm. This algorithm is a 32-bit extension and improvement
	///       of the Fletcher algorithm, used in the ITU-T X.224 / ISO 8073
	///       standard.
	/// 
	///       Adler-32 is composed of two sums accumulated per byte: s1 is
	///       the sum of all bytes, s2 is the sum of all s1 values. Both sums
	///       are done modulo 65521. s1 is initialized to 1, s2 to zero.  The
	///       Adler-32 checksum is stored as s2*65536 + s1 in most-
	///       significant-byte first (network) order."
	/// 
	///  "8.2. The Adler-32 algorithm
	/// 
	///    The Adler-32 algorithm is much faster than the CRC32 algorithm yet
	///    still provides an extremely low probability of undetected errors.
	/// 
	///    The modulo on unsigned long accumulators can be delayed for 5552
	///    bytes, so the modulo operation time is negligible.  If the bytes
	///    are a, b, c, the second sum is 3a + 2b + c + 3, and so is position
	///    and order sensitive, unlike the first sum, which is just a
	///    checksum.  That 65521 is prime is important to avoid a possible
	///    large class of two-byte errors that leave the check unchanged.
	///    (The Fletcher checksum uses 255, which is not prime and which also
	///    makes the Fletcher check insensitive to single byte changes 0 -
	///    255.)
	/// 
	///    The sum s1 is initialized to 1 instead of zero to make the length
	///    of the sequence part of s2, so that the length does not have to be
	///    checked separately. (Any sequence of zeroes has a Fletcher
	///    checksum of zero.)"
	/// </summary>
	/// <see cref="ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream"/>
	/// <see cref="ICSharpCode.SharpZipLib.Zip.Compression.Streams.DeflaterOutputStream"/>
	internal sealed class Adler32
	{
		/// <summary>
		/// largest prime smaller than 65536
		/// </summary>
		readonly static uint BASE = 65521;
		
		uint checksum;
		
		/// <summary>
		/// Returns the Adler32 data checksum computed so far.
		/// </summary>
		public long Value 
		{
			get 
			{
				return checksum;
			}
		}
		
		/// <summary>
		/// Creates a new instance of the <code>Adler32</code> class.
		/// The checksum starts off with a value of 1.
		/// </summary>
		public Adler32()
		{
			Reset();
		}
		
		/// <summary>
		/// Resets the Adler32 checksum to the initial value.
		/// </summary>
		public void Reset()
		{
			checksum = 1; //Initialize to 1
		}
		
		/// <summary>
		/// Updates the checksum with the byte b.
		/// </summary>
		/// <param name="bval">
		/// the data value to add. The high byte of the int is ignored.
		/// </param>
		public void Update(int bval)
		{
			//We could make a length 1 byte array and call update again, but I
			//would rather not have that overhead
			uint s1 = checksum & 0xFFFF;
			uint s2 = checksum >> 16;
			
			s1 = (s1 + ((uint)bval & 0xFF)) % BASE;
			s2 = (s1 + s2) % BASE;
			
			checksum = (s2 << 16) + s1;
		}
		
		/// <summary>
		/// Updates the checksum with the bytes taken from the array.
		/// </summary>
		/// <param name="buffer">
		/// buffer an array of bytes
		/// </param>
		public void Update(byte[] buffer)
		{
			Update(buffer, 0, buffer.Length);
		}
		
		/// <summary>
		/// Updates the checksum with the bytes taken from the array.
		/// </summary>
		/// <param name="buf">
		/// an array of bytes
		/// </param>
		/// <param name="off">
		/// the start of the data used for this update
		/// </param>
		/// <param name="len">
		/// the number of bytes to use for this update
		/// </param>
		public void Update(byte[] buf, int off, int len)
		{
			if (buf == null) 
			{
				throw new ArgumentNullException("buf");
			}
			
			if (off < 0 || len < 0 || off + len > buf.Length) 
			{
				throw new ArgumentOutOfRangeException();
			}
			
			//(By Per Bothner)
			uint s1 = checksum & 0xFFFF;
			uint s2 = checksum >> 16;
			
			while (len > 0) 
			{
				// We can defer the modulo operation:
				// s1 maximally grows from 65521 to 65521 + 255 * 3800
				// s2 maximally grows by 3800 * median(s1) = 2090079800 < 2^31
				int n = 3800;
				if (n > len) 
				{
					n = len;
				}
				len -= n;
				while (--n >= 0) 
				{
					s1 = s1 + (uint)(buf[off++] & 0xFF);
					s2 = s2 + s1;
				}
				s1 %= BASE;
				s2 %= BASE;
			}
			
			checksum = (s2 << 16) | s1;
		}
	}
}
