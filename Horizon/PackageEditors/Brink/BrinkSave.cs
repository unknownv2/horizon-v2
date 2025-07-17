using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Brink
{
    public class BrinkSave
    {
        public struct BrinkSettingsEntry
        {
            public int Length;
            public uint Checksum;
            public int Unknown1;
            public int Unknownv2;
            public long DataLocation;

            public BrinkSettingsEntry(EndianReader reader)
            {
                this.Length = reader.ReadInt32();
                this.Checksum = reader.ReadUInt32();
                this.Unknown1 = reader.ReadInt32();
                this.Unknownv2 = reader.ReadInt32();
                this.DataLocation = reader.BaseStream.Position;
            }
        }
        private EndianIO IO;
        public List<BrinkSettingsEntry> SettingsEntries;

        private uint[] SettingsEntryLocations = new uint[]
        { 
            0x10, 0xA0, 0x4B0, 0x5C0, 0xF5C0, 0x111C0
        };
        public int[] MaxSettingsEntrySize = new int[]
        {
            0x80, 0x400, 0x100, 0xEFF0, 0x1BF0, 0x13BF0
        };
        public BrinkSave(EndianIO IO)
        {
            this.IO = IO;

            this.SettingsEntries = new List<BrinkSettingsEntry>();

            this.Read();
        }
        private void Read()
        {
            for (var x = 0; x < this.SettingsEntryLocations.Length; x++)
            {
                this.IO.In.SeekTo(this.SettingsEntryLocations[x]);
                this.SettingsEntries.Add(new BrinkSettingsEntry(this.IO.In));
            }
        }
        public string ReadEntryBuffer(BrinkSettingsEntry Entry)
        {
            this.IO.In.SeekTo(Entry.DataLocation);
            return this.IO.In.ReadNullTerminatedString();
        }
        public int WriteEntryBuffer(BrinkSettingsEntry Entry, int EntryIndex, string Value)
        {
            int dwReturn = 0x00;

            if (Value.Length + 1 <= MaxSettingsEntrySize[EntryIndex])
            {
                byte[] data = Encoding.ASCII.GetBytes(Value);

                this.IO.Out.SeekTo(Entry.DataLocation - 0x10);
                this.IO.Out.Write(Value.Length);
                this.IO.Out.Write(CalculateChecksum(data));
                this.IO.Out.SeekTo(Entry.DataLocation);
                this.IO.Out.WriteAsciiString(Value, MaxSettingsEntrySize[EntryIndex]);
            }
            else
            {
                dwReturn = 0x01;
            }

            return dwReturn;
        }

        private uint CalculateChecksum(byte[] Data)
        {
            return MurmurHash.Hash(Data);
        }
    }
    public class MurmurHash
    {
        public static UInt32 Hash(Byte[] data)
        {
            return Hash(data, 0x17293876);
        }
        const UInt32 m = 0x5bd1e995;
        const Int32 r = 24;

        public static UInt32 Hash(Byte[] data, UInt32 seed)
        {
            Int32 length = data.Length;
            if (length == 0)
                return 0;
            UInt32 h = seed ^ (UInt32)length;
            Int32 currentIndex = 0;
            while (length >= 4)
            {
                UInt32 k = BitConverter.ToUInt32(data, currentIndex);
                k *= m;
                k ^= k >> r;
                k *= m;

                h *= m;
                h ^= k;
                currentIndex += 4;
                length -= 4;
            }
            switch (length)
            {
                case 3:
                    h ^= BitConverter.ToUInt16(data, currentIndex);
                    h ^= (UInt32)data[currentIndex + 2] << 16;
                    h *= m;
                    break;
                case 2:
                    h ^= BitConverter.ToUInt16(data, currentIndex);
                    h *= m;
                    break;
                case 1:
                    h ^= data[currentIndex];
                    h *= m;
                    break;
                default:
                    break;
            }

            h ^= h >> 13;
            h *= m;
            h ^= h >> 15;

            return h;
        }
    }
}
