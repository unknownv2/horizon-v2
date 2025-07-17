using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Rockstar
{
    //70707070 is used as padding.
    // 073B49A6 block ID = Coordinates for objects?
    // 00000102 ID = Some kind of tables. 64-bit per entry. 32bits = ID?, 32bits = 0xFF + 3 bytes of that are valid..?
    internal class GrandTheftAutoVClass
    {
        private EndianIO IO;
        private ProgressionHeader progressionHeader;
        private ProgressMap progressMap;
        private ProgressChapters progressChapters;
        private PSIG psig;
        private ChecksumHeader checksumHeader;

        public ProgressBoolValues progressBoolValues { get; set; }
        public ProgressInt32Values progressInt32Values { get; set; }
        public ProgressUInt32Values progressUInt32Values { get; set; }
        public ProgressInt64Values progressInt64Values { get; set; }
        public ProgressFloatValues progressFloatValues { get; set; }
        internal GrandTheftAutoVClass(EndianIO io)
        {
            IO = new EndianIO(Decrypt(io.ToArray()), EndianType.BigEndian, true);
            progressionHeader = new ProgressionHeader(IO);

            // Read our save file
            IO.In.BaseStream.Position = progressionHeader.ProgressMapOffset;
            progressMap = new ProgressMap(IO);
            progressChapters = new ProgressChapters(IO);
            psig = new PSIG(IO);
            checksumHeader = new ChecksumHeader(IO);

            // Initialize our embedded values table
            progressFloatValues = new ProgressFloatValues(progressMap);
            progressInt64Values = new ProgressInt64Values(progressMap);
            progressInt32Values = new ProgressInt32Values(this, progressMap);
            progressUInt32Values = new ProgressUInt32Values(progressMap);
            progressBoolValues = new ProgressBoolValues(progressMap);
        }

        internal byte[] ToArray()
        {
            // Save our values
            progressInt32Values.Write();
            progressFloatValues.Write();
            progressInt64Values.Write();
            progressUInt32Values.Write();
            progressBoolValues.Write();


            // First we need to calculate where our pmap will be.
            // Since it is below our actual pmap entry data, we'll need to loop to calculate.
            uint newPMAPLocation = 0x10; // (progression header)
            for (int i = 0; i < progressMap.Entries.Length; i++)
            {
                progressMap.Entries[i].SetEntryOffset(newPMAPLocation); // set our map entry's offset for it while we're at it.
                int entryLength = progressMap.Entries[i].Data.Length;
                // Only pad every item but the last.
                if (i != progressMap.Entries.Length - 1)
                    newPMAPLocation += (uint)(entryLength + ((0x10 - (entryLength % 0x10))) % 0x10); // make room for our entry padded to 0x10.
                else
                    newPMAPLocation += (uint)entryLength;
            }
            progressionHeader.ProgressMapOffset = newPMAPLocation;

            // Now we can begin writing our data.

            // Write our progression header
            progressionHeader.Write(IO);
            // Write our progression entries.
            for (int i = 0; i < progressMap.Entries.Length; i++)
            {
                // Write our data
                IO.Out.Write(progressMap.Entries[i].Data);
                // Pad to 0x10 if not last entry
                if (i != progressMap.Entries.Length - 1)
                    while (IO.Out.BaseStream.Position % 0x10 > 0)
                        IO.Out.Write((byte)0x70);
            }
            progressMap.Write(IO);
            progressChapters.Write(IO);
            psig.Write(IO);
            checksumHeader.Write(IO);

            return Encrypt(IO.ToArray());
        }

        public static byte[] Decrypt(byte[] dataIn)
        {
            byte[] data = new byte[dataIn.Length];
            dataIn.CopyTo(data, 0);

            // Create our Rijndael class
            Rijndael rj = Rijndael.Create();
            rj.BlockSize = 128;
            rj.KeySize = 256;
            rj.Mode = CipherMode.ECB;
            rj.Key = KeyStore.AESKey;
            rj.IV = new byte[16];
            rj.Padding = PaddingMode.None;

            ICryptoTransform transform = rj.CreateDecryptor();

            int dataLen = data.Length & ~0x0F; // byte align 16

            if (dataLen > 0)
            {
                transform.TransformBlock(dataIn, 0, dataLen, data, 0);
            }

            return data;
        }
        public static byte[] Encrypt(byte[] dataIn)
        {
            byte[] data = new byte[dataIn.Length];
            dataIn.CopyTo(data, 0);

            // Create our Rijndael class
            Rijndael rj = Rijndael.Create();
            rj.BlockSize = 128;
            rj.KeySize = 256;
            rj.Mode = CipherMode.ECB;
            rj.Key = KeyStore.AESKey;
            rj.IV = new byte[16];
            rj.Padding = PaddingMode.None;

            ICryptoTransform transform = rj.CreateEncryptor();

            int dataLen = data.Length & ~0x0F; // byte align 16

            if (dataLen > 0)
            {
                transform.TransformBlock(dataIn, 0, dataLen, data, 0);
            }

            return data;
        }

        public void ModifyWeapons(bool maxAmmo, bool allAttachments)
        {
            // This function is temporary until we map out the rest of this block. It just writes max ammo count and attachment flags.
            ProgressMap.ProgressMapEntry mEntry = progressMap.GetEntry(0x06ED53BB);
            byte[] tempData = mEntry.Data;

            // Loop for each character and write our ammo and attachments.
            EndianIO IO2 = new EndianIO(tempData, EndianType.BigEndian, true);
            for (int i = 0; i < 0x210; i += 0xC)
            {
                IO2.In.BaseStream.Position = 0x2820 + i;
                if (IO2.In.ReadUInt32() != 0)
                {
                    if (maxAmmo)
                        IO2.Out.Write((uint)int.MaxValue);
                    else
                        IO2.Out.BaseStream.Position += 4;
                    if (allAttachments)
                        IO2.Out.Write((uint)0xFFFFFFFF);
                }
            }
            for (int i = 0; i < 0x210; i += 0xC)
            {
                IO2.In.BaseStream.Position = 0x2AF8 + i;
                if (IO2.In.ReadUInt32() != 0)
                {
                    if (maxAmmo)
                        IO2.Out.Write((uint)int.MaxValue);
                    else
                        IO2.Out.BaseStream.Position += 4;
                    if (allAttachments)
                        IO2.Out.Write((uint)0xFFFFFFFF);
                }
            }
            for (int i = 0; i < 0x210; i += 0xC)
            {
                IO2.In.BaseStream.Position = 0x2DD0 + i;
                if (IO2.In.ReadUInt32() != 0)
                {
                    if (maxAmmo)
                        IO2.Out.Write((uint)int.MaxValue);
                    else
                        IO2.Out.BaseStream.Position += 4;
                    if (allAttachments)
                        IO2.Out.Write((uint)0xFFFFFFFF);
                }
            }

            mEntry.Data = IO2.ToArray();
        }

        /// <summary>
        /// The absolute header of the gamesave, points to the progress map for the file.
        /// </summary>
        public class ProgressionHeader
        {
            public const uint MAGIC = 0x5053494E;
            public uint ProgressMapOffset { get; set; }
            public ulong Padding { get; set; }


            public ProgressionHeader(EndianIO IO)
            {
                IO.In.BaseStream.Position = 0x0;

                // Validate and read our table header.
                if (IO.In.ReadUInt32() != MAGIC)
                    throw new Exception("PSIN header magic invalid.");

                ProgressMapOffset = IO.In.ReadUInt32();
                Padding = IO.In.ReadUInt64();
            }

            public void Write(EndianIO IO)
            {
                IO.Out.BaseStream.Position = 0x0;

                // Write our data
                IO.Out.Write(MAGIC);
                IO.Out.Write(ProgressMapOffset);
                IO.Out.Write(Padding);
            }
        }
        /// <summary>
        /// The ProgressMap for a gamesave indicates where all parts of the save file are located.
        /// </summary>
        public class ProgressMap
        {
            private const uint MAGIC = 0x504D4150;
            private uint Size { get; set; }
            private uint Unknown0 { get; set; }
            private ushort Count { get; set; }
            private ushort Padding { get; set; }

            public ProgressMapEntry[] Entries { get; set; }

            public ProgressMap(EndianIO IO)
            {
                // Validate and read our table header.
                if (IO.In.ReadUInt32() != MAGIC)
                    throw new Exception("PMAP header magic invalid.");
                Size = IO.In.ReadUInt32();
                Unknown0 = IO.In.ReadUInt32();
                Count = IO.In.ReadUInt16();
                Padding = IO.In.ReadUInt16();

                // Read our table entries.
                Entries = new ProgressMapEntry[Count];
                for (int i = 0; i < Entries.Length; i++)
                {
                    Entries[i] = new ProgressMapEntry(IO);
                }
            }
            public void Write(EndianIO IO)
            {
                // Fix up variables quickly.
                Count = (ushort)Entries.Length;
                Size = (uint)(0x10 + (0x10 * Count));

                IO.Out.Write(MAGIC);
                IO.Out.Write(Size);
                IO.Out.Write(Unknown0);
                IO.Out.Write(Count);
                IO.Out.Write(Padding);

                // Write our table entries.
                for (int i = 0; i < Entries.Length; i++)
                    Entries[i].Write(IO);
            }

            /// <summary>
            /// Obtains the entry with the given ID if it exists.
            /// </summary>
            /// <param name="ID">The ID of the entry to obtain.</param>
            /// <returns>Returns the entry if found, otherwise null.</returns>
            public ProgressMapEntry GetEntry(uint ID)
            {
                foreach (ProgressMapEntry entry in Entries)
                    if (entry.ID == ID)
                        return entry;
                return null;
            }

            /// <summary>
            /// A progress map entry is indicated by an identifier and describes partitions of the save file.
            /// </summary>
            public class ProgressMapEntry
            {
                public uint ID { get; set; }
                private uint Unknown { get; set; } // always zero when i've seen it, likely padding, but we'll play it safe..
                public byte[] Data { get; set; }

                private uint entryOffset;

                public ProgressMapEntry(EndianIO IO)
                {
                    // Read our entry information.
                    ID = IO.In.ReadUInt32();
                    entryOffset = IO.In.ReadUInt32();
                    Unknown = IO.In.ReadUInt32();
                    uint entryLength = IO.In.ReadUInt32();
                    // Save our position
                    int offset = (int)IO.In.BaseStream.Position;

                    // Obtain our entry data.
                    IO.In.BaseStream.Position = entryOffset;
                    Data = IO.In.ReadBytes(entryLength);

                    // Restore our position.
                    IO.In.BaseStream.Position = offset;
                }
                public void SetEntryOffset(uint offset)
                {
                    entryOffset = offset;
                }
                public void Write(EndianIO IO)
                {
                    IO.Out.Write(ID);
                    IO.Out.Write(entryOffset);
                    IO.Out.Write(Unknown);
                    IO.Out.Write((uint)Data.Length);
                }
            }
        }
        /// <summary>
        /// TODO.
        /// </summary>
        public class ProgressChapters
        {
            public const uint MAGIC = 0x50534348;
            public uint Size { get; set; }
            public byte[] Data { get; set; }
            public ProgressChapters(EndianIO IO)
            {
                // Validate and read our table header.
                if (IO.In.ReadUInt32() != MAGIC)
                    throw new Exception("PSCH header magic invalid.");
                Size = IO.In.ReadUInt32();
                Data = IO.In.ReadBytes(Size - 8);
            }
            public void Write(EndianIO IO)
            {
                IO.Out.Write(MAGIC);
                IO.Out.Write(Size);
                IO.Out.Write(Data);
            }
        }
        /// <summary>
        /// TODO.
        /// </summary>
        public class PSIG
        {
            public const uint MAGIC = 0x50534947;
            public uint Size { get; set; }
            public byte[] Data { get; set; }
            public PSIG(EndianIO IO)
            {
                // Validate and read our table header.
                if (IO.In.ReadUInt32() != MAGIC)
                    throw new Exception("PSIG header magic invalid.");
                Size = IO.In.ReadUInt32();
                Data = IO.In.ReadBytes(Size - 8);
            }
            public void Write(EndianIO IO)
            {
                IO.Out.Write(MAGIC);
                IO.Out.Write(Size);
                IO.Out.Write(Data);
            }
        }
        /// <summary>
        /// The checksum header is used to validate the entire save file as a whole.
        /// </summary>
        public class ChecksumHeader
        {
            private const uint MAGIC = 0x43484B53;
            private uint Size { get; set; }
            private uint ChecksumLength { get; set; }
            private uint Checksum { get; set; }
            private byte Unknown { get; set; }

            public ChecksumHeader(EndianIO IO)
            {

                // Validate and read our table header.
                if (IO.In.ReadUInt32() != MAGIC)
                    throw new Exception("CHKS header magic invalid.");
                Size = IO.In.ReadUInt32();
                ChecksumLength = IO.In.ReadUInt32();
                Checksum = IO.In.ReadUInt32();
                Unknown = IO.In.ReadByte();
                IO.In.ReadByte();
                IO.In.ReadByte();
                IO.In.ReadByte(); // padding
            }
            public void Write(EndianIO IO)
            {
                // Write our checksum header without a checksum, then calculate and fill it in.
                uint endOffset = (uint)IO.Out.BaseStream.Position;
                ChecksumLength = endOffset + 0x14;
                IO.Out.Write(MAGIC);
                IO.Out.Write(Size);
                IO.Out.Write((int)0);
                IO.Out.Write((int)0);
                IO.Out.Write(Unknown);
                IO.Out.Write((byte)0x70);
                IO.Out.Write((byte)0x70);
                IO.Out.Write((byte)0x70); // padding
                IO.Stream.SetLength(IO.Out.BaseStream.Position); // set our stream position to end here, the end of file is after checksum.

                // Calculate our actual checksum.
                uint sum = 0x3fac7125;

                IO.In.BaseStream.Position = 0;
                for (int i = 0; i < IO.In.BaseStream.Length; i++)
                {
                    uint a = (uint)((sbyte)IO.In.ReadByte()) + sum;
                    uint b = (a << 10) + a;
                    sum = ((b >> 6) ^ b);
                }

                sum = (sum << 3) + sum;
                sum ^= (sum >> 11);
                Checksum = (sum << 15) + sum;

                // Write our checksum length and checksum
                IO.Out.BaseStream.Position = endOffset + 8;
                IO.Out.Write(ChecksumLength); // temporary as well, should use actual filesize.
                IO.Out.Write(Checksum);
            }
        }

        internal enum SP_Stats
        {
            Special = 0,
            Stamina = 1,
            Strength = 2,
            LungCapacity = 3,
            Driving = 4,
            Flying = 5,
            Shooting = 6,
            Stealth = 7,
        }
        internal enum SP_Characters
        {
            Michael,
            Franklin,
            Trevor
        }

        internal class Character
        {
            internal int[] Stats = new int[8];

            internal int Special;
            internal int Stamina;
            internal int Strength;
            internal int LungCapacity;
            internal int Driving;
            internal int Flying;
            internal int Shooting;
            internal int Stealth;
        }

        // A table found with our pmap.
        public class ProgressFloatValues
        {
            // required
            private ProgressMap.ProgressMapEntry mapEntry;
            private const uint ID = 0x21d25675;
            private EndianIO IO;
            public Dictionary<uint, float> Values;

            //ids
            private const uint ID_MICHAEL_STAMINA2 = 0xa245390a;
            private const uint ID_FRANKLIN_STAMINA2 = 0x9b0d6f2b;
            private const uint ID_TREVOR_STAMINA2 = 0x17b89dab;
            private const uint ID_MICHAEL_STEALTH3 = 0x269652ad;
            private const uint ID_FRANKLIN_STEALTH3 = 0x2cfebe25;
            private const uint ID_TREVOR_STEALTH3 = 0x01d40ef6;

            // exposed values
            public float MichaelStamina2
            {
                get { return Values[ID_MICHAEL_STAMINA2]; }
                set { Values[ID_MICHAEL_STAMINA2] = value; }
            }
            public float FranklinStamina2
            {
                get { return Values[ID_FRANKLIN_STAMINA2]; }
                set { Values[ID_FRANKLIN_STAMINA2] = value; }
            }
            public float TrevorStamina2
            {
                get { return Values[ID_TREVOR_STAMINA2]; }
                set { Values[ID_TREVOR_STAMINA2] = value; }
            }
            public float MichaelStealth3
            {
                get { return Values[ID_MICHAEL_STEALTH3]; }
                set { Values[ID_MICHAEL_STEALTH3] = value; }
            }
            public float FranklinStealth3
            {
                get { return Values[ID_FRANKLIN_STEALTH3]; }
                set { Values[ID_FRANKLIN_STEALTH3] = value; }
            }
            public float TrevorStealth3
            {
                get { return Values[ID_TREVOR_STEALTH3]; }
                set { Values[ID_TREVOR_STEALTH3] = value; }
            }

            public ProgressFloatValues(ProgressMap progressMap)
            {
                mapEntry = progressMap.GetEntry(ID);
                IO = new EndianIO(mapEntry.Data, EndianType.BigEndian, true);
                Values = new Dictionary<uint, float>();
                for (int i = 0; i < IO.In.BaseStream.Length / 8; i++)
                    Values[IO.In.ReadUInt32()] = IO.In.ReadSingle();
            }
            public void Write()
            {
                // Write out our table
                IO.Out.BaseStream.Position = 0;
                foreach (uint key in Values.Keys)
                {
                    IO.Out.Write(key);
                    IO.Out.Write(Values[key]);
                }
                IO.Stream.SetLength(IO.Out.BaseStream.Position);

                // Set our data
                mapEntry.Data = IO.ToArray();
            }
        }
        public class ProgressBoolValues
        {
            // required
            private ProgressMap.ProgressMapEntry mapEntry;

            private const uint ID = 0x5AC4DD62;
            private EndianIO IO;
            public Dictionary<uint, uint> Values;


            public ProgressBoolValues(ProgressMap progressMap)
            {
                mapEntry = progressMap.GetEntry(ID);
                IO = new EndianIO(mapEntry.Data, EndianType.BigEndian, true);
                Values = new Dictionary<uint, uint>();
                for (int i = 0; i < IO.In.BaseStream.Length / 8; i++)
                    Values[IO.In.ReadUInt32()] = IO.In.ReadUInt32();
            }

            public void Write()
            {
                // Write out our table
                IO.Out.BaseStream.Position = 0;
                foreach (uint key in Values.Keys)
                {
                    IO.Out.Write(key);
                    IO.Out.Write(Values[key]);
                }
                IO.Stream.SetLength(IO.Out.BaseStream.Position);

                // Set our data
                mapEntry.Data = IO.ToArray();
            }
        }
        public class ProgressUInt32Values
        {
            // required
            private ProgressMap.ProgressMapEntry mapEntry;

            private const uint ID = 0x7DBDF141;
            private EndianIO IO;
            public Dictionary<uint, uint> Values;


            public ProgressUInt32Values(ProgressMap progressMap)
            {
                mapEntry = progressMap.GetEntry(ID);
                IO = new EndianIO(mapEntry.Data, EndianType.BigEndian, true);
                Values = new Dictionary<uint, uint>();
                for (int i = 0; i < IO.In.BaseStream.Length / 8; i++)
                    Values[IO.In.ReadUInt32()] = IO.In.ReadUInt32();
            }

            public void Write()
            {
                // Write out our table
                IO.Out.BaseStream.Position = 0;
                foreach (uint key in Values.Keys)
                {
                    IO.Out.Write(key);
                    IO.Out.Write(Values[key]);
                }
                IO.Stream.SetLength(IO.Out.BaseStream.Position);

                // Set our data
                mapEntry.Data = IO.ToArray();
            }
        }
        public class ProgressInt32Values
        {
            // required
            private GrandTheftAutoVClass parent;
            private ProgressMap.ProgressMapEntry mapEntry;
            private ProgressFloatValues progressFloatValues;

            private const uint ID = 0x83125968;
            private EndianIO IO;
            public Dictionary<uint, int> Values;
            public int[] StatModifiers = new int[8 * 0x10];
            private float[] StaminaModifiers = new float[3];
            private int[] ShootingModifiers = new int[3];
            private int[] ShootingModifiers2 = new int[3];
            //private double StaminaMax = 16093.44;

            // ids
            private const uint ID_FRANKLIN_MONEY = 0x44BD6982;
            private const uint ID_MICHAEL_MONEY = 0x0324C31D;
            private const uint ID_TREVOR_MONEY = 0x8D75047D;

            private const uint ID_MICHAEL_SPECIAL = 0x4ecd9f81;
            private const uint ID_MICHAEL_STAMINA = 0x22c8aaa2;
            private const uint ID_MICHAEL_STAMINA3 = 0x85B20F46;
            private const uint ID_MICHAEL_STAMINA4 = 0xC421EB13;
            private const uint ID_MICHAEL_SHOOTING = 0xb4892709;
            private const uint ID_MICHAEL_SHOOTING2 = 0xc45479d2;
            private const uint ID_MICHAEL_SHOOTING3 = 0x7A728DFF;
            private const uint ID_MICHAEL_STRENGTH = 0x906b2799;
            private const uint ID_MICHAEL_STRENGTH2 = 0x546dbfc0;
            private const uint ID_MICHAEL_STEALTH = 0x2268b791;
            private const uint ID_MICHAEL_STEALTH2 = 0x7822df05;
            private const uint ID_MICHAEL_FLYING = 0x78abe4e6;
            private const uint ID_MICHAEL_FLYING2 = 0xc8a3a687;
            private const uint ID_MICHAEL_FLYING3 = 0x900983E9;
            private const uint ID_MICHAEL_FLYING4 = 0x69039180;
            private const uint ID_MICHAEL_DRIVING = 0x11b47270;
            private const uint ID_MICHAEL_DRIVING2 = 0x0334d043;
            private const uint ID_MICHAEL_LUNGCAPACITY = 0x73968ebd;

            private const uint ID_FRANKLIN_SPECIAL = 0x51ccfba3;
            private const uint ID_FRANKLIN_STAMINA = 0x255effb5;
            private const uint ID_FRANKLIN_STAMINA3 = 0x00FC0047;
            private const uint ID_FRANKLIN_STAMINA4 = 0x4BEAE1EA;
            private const uint ID_FRANKLIN_SHOOTING = 0xcb261497;
            private const uint ID_FRANKLIN_SHOOTING2 = 0xA08ED8BC;
            private const uint ID_FRANKLIN_SHOOTING3 = 0xe3f06a75;
            private const uint ID_FRANKLIN_STRENGTH = 0xb82874e3;
            private const uint ID_FRANKLIN_STRENGTH2 = 0xb39c10a1;
            private const uint ID_FRANKLIN_STEALTH = 0xe76d0c23;
            private const uint ID_FRANKLIN_STEALTH2 = 0x5024f460;
            private const uint ID_FRANKLIN_FLYING = 0xe98bee3d;
            private const uint ID_FRANKLIN_FLYING2 = 0xc4df932b;
            private const uint ID_FRANKLIN_FLYING3 = 0xE301A690;
            private const uint ID_FRANKLIN_FLYING4 = 0xE275A1BF;
            private const uint ID_FRANKLIN_DRIVING = 0x7dd80ac8;
            private const uint ID_FRANKLIN_DRIVING2 = 0xe883a61f;
            private const uint ID_FRANKLIN_LUNGCAPACITY = 0x6c3bbb1a;

            private const uint ID_TREVOR_SPECIAL = 0x05b06442;
            private const uint ID_TREVOR_STAMINA = 0x7d8246ae;
            private const uint ID_TREVOR_STAMINA3 = 0xE3A51E94;
            private const uint ID_TREVOR_STAMINA4 = 0x4D2185C6;
            private const uint ID_TREVOR_SHOOTING = 0x2a3a74ea;
            private const uint ID_TREVOR_SHOOTING2 = 0xbefc845d;
            private const uint ID_TREVOR_SHOOTING3 = 0x41DBDDD5;
            private const uint ID_TREVOR_STRENGTH = 0x4f19e159;
            private const uint ID_TREVOR_STRENGTH2 = 0x5ca0182d;
            private const uint ID_TREVOR_STEALTH = 0xd03b7eeb;
            private const uint ID_TREVOR_STEALTH2 = 0x2c9e9fc1;
            private const uint ID_TREVOR_FLYING = 0x77cf9710;
            private const uint ID_TREVOR_FLYING2 = 0xa6fa09e0;
            private const uint ID_TREVOR_FLYING3 = 0xA27342A6;
            private const uint ID_TREVOR_FLYING4 = 0x614DB6DF;
            private const uint ID_TREVOR_DRIVING = 0x6bef592f;
            private const uint ID_TREVOR_DRIVING2 = 0x3b9ae2c6;
            private const uint ID_TREVOR_LUNGCAPACITY = 0x7e9487b3;

            // exposed values
            public int FranklinMoney
            {
                get { return Values[ID_FRANKLIN_MONEY]; }
                set { Values[ID_FRANKLIN_MONEY] = value; }
            }
            public int MichaelMoney
            {
                get { return Values[ID_MICHAEL_MONEY]; }
                set { Values[ID_MICHAEL_MONEY] = value; }
            }
            public int TrevorMoney
            {
                get { return Values[ID_TREVOR_MONEY]; }
                set { Values[ID_TREVOR_MONEY] = value; }
            }

            public int MichaelSpecial
            {
                get { return Values[ID_MICHAEL_SPECIAL]; }
                set { Values[ID_MICHAEL_SPECIAL] = value; }
            }
            public int FranklinSpecial
            {
                get { return Values[ID_FRANKLIN_SPECIAL]; }
                set { Values[ID_FRANKLIN_SPECIAL] = value; }
            }
            public int TrevorSpecial
            {
                get { return Values[ID_TREVOR_SPECIAL]; }
                set { Values[ID_TREVOR_SPECIAL] = value; }
            }

            public int MichaelStamina
            {
                get
                {
                    decimal a = ((decimal)progressFloatValues.MichaelStamina2 - (decimal)StaminaModifiers[0]);
                    decimal b = a / 175;
                    decimal c = (47 + StatModifiers[((int)SP_Stats.Stamina * 4) + 2]);
                    decimal d = ModifierCalculator(parent.progressInt64Values.Values[ID_MICHAEL_STAMINA3]);
                    decimal e = ModifierCalculator(parent.progressInt64Values.Values[ID_MICHAEL_STAMINA4]);
                    decimal f = b + c + d + e;

                    if (f > 100)
                        f = 100;
                    if (f < 0)
                        f = 0;
                    return (int)f;
                }
                set
                {
                    if (Math.Abs(StaminaModifiers[0]) > 10000)
                        StaminaModifiers[0] = 0;

                    decimal a = (47 + StatModifiers[((int)SP_Stats.Stamina * 4) + 2]);
                    decimal b = (ModifierCalculator(parent.progressInt64Values.Values[ID_MICHAEL_STAMINA3]) + ModifierCalculator(parent.progressInt64Values.Values[ID_MICHAEL_STAMINA4]));
                    decimal c = (value - (a + b));
                    decimal d = (c * 175);
                    decimal e = d + (decimal)StaminaModifiers[0];
                    Values[ID_MICHAEL_STAMINA] = value;
                    progressFloatValues.MichaelStamina2 = (float)Math.Ceiling(e);
                }
            }
            public int FranklinStamina
            {
                get
                {
                    decimal a = ((decimal)progressFloatValues.FranklinStamina2 - (decimal)StaminaModifiers[1]);
                    decimal b = a / 175;
                    decimal c = (51 + StatModifiers[((int)SP_Stats.Stamina * 4) + 3]);
                    decimal d = ModifierCalculator(parent.progressInt64Values.Values[ID_FRANKLIN_STAMINA3]);
                    decimal e = ModifierCalculator(parent.progressInt64Values.Values[ID_FRANKLIN_STAMINA4]);
                    decimal f = b + c + d + e;
                    if (f > 100)
                        f = 100;
                    if (f < 0)
                        f = 0;
                    return (int)f;
                }
                set
                {
                    if (Math.Abs(StaminaModifiers[1]) > 10000)
                        StaminaModifiers[1] = 0;

                    decimal a = (51 + StatModifiers[((int)SP_Stats.Stamina * 4) + 3]);
                    decimal b = (ModifierCalculator(parent.progressInt64Values.Values[ID_FRANKLIN_STAMINA3]) + ModifierCalculator(parent.progressInt64Values.Values[ID_FRANKLIN_STAMINA4]));
                    decimal c = (value - (a + b));
                    decimal d = (c * 175);
                    decimal e = d + (decimal)StaminaModifiers[1];
                    Values[ID_FRANKLIN_STAMINA] = value;
                    progressFloatValues.FranklinStamina2 = (float)Math.Ceiling(e);
                }
            }
            public int TrevorStamina
            {
                get
                {
                    decimal a = ((decimal)progressFloatValues.TrevorStamina2 - (decimal)StaminaModifiers[2]);
                    decimal b = a / 175;
                    decimal c = (23 + StatModifiers[((int)SP_Stats.Stamina * 4) + 4]);
                    decimal d = ModifierCalculator(parent.progressInt64Values.Values[ID_TREVOR_STAMINA3]);
                    decimal e = ModifierCalculator(parent.progressInt64Values.Values[ID_TREVOR_STAMINA4]);
                    decimal f = b + c + d + e;
                    if (f > 100)
                        f = 100;
                    if (f < 0)
                        f = 0;
                    return (int)f;
                }
                set
                {
                    if (Math.Abs(StaminaModifiers[2]) > 10000)
                        StaminaModifiers[2] = 0;

                    decimal a = (23 + StatModifiers[((int)SP_Stats.Stamina * 4) + 4]);
                    decimal b = (ModifierCalculator(parent.progressInt64Values.Values[ID_TREVOR_STAMINA3]) + ModifierCalculator(parent.progressInt64Values.Values[ID_TREVOR_STAMINA4]));
                    decimal c = (value - (a + b));
                    decimal d = (c * 175);
                    decimal e = d + (decimal)StaminaModifiers[2];
                    Values[ID_TREVOR_STAMINA] = value;
                    progressFloatValues.TrevorStamina2 = (float)Math.Ceiling(e);
                }
            }

            public int MichaelShooting
            {
                get
                {
                    decimal s2 = Values[ID_MICHAEL_SHOOTING2];
                    decimal s3 = Values[ID_MICHAEL_SHOOTING3];
                    decimal a = (s3 - ShootingModifiers[(int)SP_Characters.Michael]);
                    decimal b = a / 40;
                    decimal c = (s2 - s3);
                    decimal d = (c - ShootingModifiers2[(int)SP_Characters.Michael]);
                    decimal e = d / 80;
                    decimal f = (79 + StatModifiers[((int)SP_Stats.Shooting * 4) + 2]);
                    decimal g = b + e;
                    g += f;

                    if (g > 100)
                        g = 100;
                    if (g < 0)
                        g = 0;
                    return (int)g;
                }
                set
                {
                    // We need to revert this because old/other editors could've set it too high and we lose precision in calculation.
                    if (Math.Abs(Values[ID_MICHAEL_SHOOTING3]) > 10000)
                        Values[ID_MICHAEL_SHOOTING3] = 0;
                    if (Math.Abs(ShootingModifiers[(int)SP_Characters.Michael]) > 10000)
                        ShootingModifiers[(int)SP_Characters.Michael] = 0;
                    if (Math.Abs(ShootingModifiers2[(int)SP_Characters.Michael]) > 10000)
                        ShootingModifiers2[(int)SP_Characters.Michael] = 0;

                    decimal a = (Values[ID_MICHAEL_SHOOTING3] - ShootingModifiers[(int)SP_Characters.Michael]);
                    decimal b = a / 40;
                    decimal c = (79 + StatModifiers[((int)SP_Stats.Shooting * 4) + 2]);
                    decimal d = (value - (b + c));
                    decimal e = (d * 80);
                    decimal f = (Values[ID_MICHAEL_SHOOTING3] + ShootingModifiers2[(int)SP_Characters.Michael]);
                    decimal g = e + f;
                    Values[ID_MICHAEL_SHOOTING] = value;
                    Values[ID_MICHAEL_SHOOTING2] = (int)Math.Ceiling(g);
                }
            }
            public int FranklinShooting
            {
                get
                {
                    decimal s2 = Values[ID_FRANKLIN_SHOOTING2];
                    decimal s3 = Values[ID_FRANKLIN_SHOOTING3];
                    decimal a = (s3 - ShootingModifiers[(int)SP_Characters.Franklin]);
                    decimal b = a / 40;
                    decimal c = (s2 - s3);
                    decimal d = (c - ShootingModifiers2[(int)SP_Characters.Franklin]);
                    decimal e = d / 80;
                    decimal f = (24 + StatModifiers[((int)SP_Stats.Shooting * 4) + 3]);
                    decimal g = b + e;
                    g += f;
                    if (g > 100)
                        g = 100;
                    if (g < 0)
                        g = 0;
                    return (int)g;
                }
                set
                {
                    // We need to revert this because old/other editors could've set it too high and we lose precision in calculation.
                    if (Math.Abs(Values[ID_FRANKLIN_SHOOTING3]) > 10000)
                        Values[ID_FRANKLIN_SHOOTING3] = 0;
                    if (Math.Abs(ShootingModifiers[(int)SP_Characters.Franklin]) > 10000)
                        ShootingModifiers[(int)SP_Characters.Franklin] = 0;
                    if (Math.Abs(ShootingModifiers2[(int)SP_Characters.Franklin]) > 10000)
                        ShootingModifiers2[(int)SP_Characters.Franklin] = 0;

                    decimal a = (Values[ID_FRANKLIN_SHOOTING3] - ShootingModifiers[(int)SP_Characters.Franklin]);
                    decimal b = a / 40;
                    decimal c = (24 + StatModifiers[((int)SP_Stats.Shooting * 4) + 3]);
                    decimal d = (value - (b + c));
                    decimal e = (d * 80);
                    decimal f = (Values[ID_FRANKLIN_SHOOTING3] + ShootingModifiers2[(int)SP_Characters.Franklin]);
                    decimal g = e + f;
                    Values[ID_FRANKLIN_SHOOTING] = value;
                    Values[ID_FRANKLIN_SHOOTING2] = (int)Math.Ceiling(g);

                }
            }
            public int TrevorShooting
            {
                get
                {
                    decimal s2 = Values[ID_TREVOR_SHOOTING2];
                    decimal s3 = Values[ID_TREVOR_SHOOTING3];
                    decimal a = (s3 - ShootingModifiers[(int)SP_Characters.Trevor]);
                    decimal b = a / 40;
                    decimal c = (s2 - s3);
                    decimal d = (c - ShootingModifiers2[(int)SP_Characters.Trevor]);
                    decimal e = d / 80;
                    decimal f = (69 + StatModifiers[((int)SP_Stats.Shooting * 4) + 4]);
                    decimal g = b + e;
                    g += f;
                    if (g > 100)
                        g = 100;
                    if (g < 0)
                        g = 0;
                    return (int)g;
                }
                set
                {
                    // We need to revert this because old/other editors could've set it too high and we lose precision in calculation.
                    if (Math.Abs(Values[ID_TREVOR_SHOOTING3]) > 10000)
                        Values[ID_TREVOR_SHOOTING3] = 0;
                    if (Math.Abs(ShootingModifiers[(int)SP_Characters.Trevor]) > 10000)
                        ShootingModifiers[(int)SP_Characters.Trevor] = 0;
                    if (Math.Abs(ShootingModifiers2[(int)SP_Characters.Trevor]) > 10000)
                        ShootingModifiers2[(int)SP_Characters.Trevor] = 0;

                    decimal a = (Values[ID_TREVOR_SHOOTING3] - ShootingModifiers[(int)SP_Characters.Trevor]);
                    decimal b = a / 40;
                    decimal c = (69 + StatModifiers[((int)SP_Stats.Shooting * 4) + 4]);
                    decimal d = (value - (b + c));
                    decimal e = (d * 80);
                    decimal f = (Values[ID_TREVOR_SHOOTING3] + ShootingModifiers2[(int)SP_Characters.Trevor]);
                    decimal g = e + f;
                    Values[ID_TREVOR_SHOOTING] = value;
                    Values[ID_TREVOR_SHOOTING2] = (int)Math.Ceiling(g);
                }
            }

            public int MichaelStrength
            {
                get { return (int)((Values[ID_MICHAEL_STRENGTH2] / 20.0f) + (21 + StatModifiers[((int)SP_Stats.Strength * 4) + 2])); }
                set { Values[ID_MICHAEL_STRENGTH] = value; Values[ID_MICHAEL_STRENGTH2] = (int)((value - (21 + StatModifiers[((int)SP_Stats.Strength * 4) + 2])) * 20.0f); }
            }
            public int FranklinStrength
            {
                get { return (int)((Values[ID_FRANKLIN_STRENGTH2] / 20.0f) + (49 + StatModifiers[((int)SP_Stats.Strength * 4) + 3])); }
                set { Values[ID_FRANKLIN_STRENGTH] = value; Values[ID_FRANKLIN_STRENGTH2] = (int)((value - (49 + StatModifiers[((int)SP_Stats.Strength * 4) + 3])) * 20.0f); }
            }
            public int TrevorStrength
            {
                get { return (int)((Values[ID_TREVOR_STRENGTH2] / 20.0f) + (79 + StatModifiers[((int)SP_Stats.Strength * 4) + 4])); }
                set { Values[ID_TREVOR_STRENGTH] = value; Values[ID_TREVOR_STRENGTH2] = (int)((value - (79 + StatModifiers[((int)SP_Stats.Strength * 4) + 4])) * 20.0f); }
            }

            public int MichaelStealth
            {
                get
                {
                    float a = (Values[ID_MICHAEL_STEALTH2] * 0.75f);
                    float b = (progressFloatValues.MichaelStealth3 / 45f);
                    float c = (81 + StatModifiers[((int)SP_Stats.Stealth * 4) + 2]);
                    float d = a + b + c;
                    if (d > 100)
                        d = 100;
                    if (d < 0)
                        d = 0;
                    return (int)d;
                }
                set
                {
                    // We need to revert this because old/other editors could've set it too high and we lose precision in calculation.
                    if (Math.Abs(progressFloatValues.MichaelStealth3) > 10000f)
                        progressFloatValues.MichaelStealth3 = 0;

                    Values[ID_MICHAEL_STEALTH] = value;
                    float a = (progressFloatValues.MichaelStealth3 / 45f);
                    float b = (81 + StatModifiers[((int)SP_Stats.Stealth * 4) + 2]);
                    float c = (value - (a + b));
                    float d = c / 0.75f;
                    Values[ID_MICHAEL_STEALTH2] = (int)Math.Ceiling(d);
                }

            }
            public int FranklinStealth
            {
                get
                {
                    float a = (Values[ID_FRANKLIN_STEALTH2] * 0.75f);
                    float b = (progressFloatValues.FranklinStealth3 / 45f);
                    float c = (21 + StatModifiers[((int)SP_Stats.Stealth * 4) + 3]);
                    float d = a + b + c;
                    if (d > 100)
                        d = 100;
                    if (d < 0)
                        d = 0;
                    return (int)d;
                }
                set
                {
                    // We need to revert this because old/other editors could've set it too high and we lose precision in calculation.
                    if (Math.Abs(progressFloatValues.FranklinStealth3) > 10000f)
                        progressFloatValues.FranklinStealth3 = 0;

                    Values[ID_FRANKLIN_STEALTH] = value;
                    float a = (progressFloatValues.FranklinStealth3 / 45f);
                    float b = (21 + StatModifiers[((int)SP_Stats.Stealth * 4) + 3]);
                    float c = (value - (a + b));
                    float d = c / 0.75f;
                    Values[ID_FRANKLIN_STEALTH2] = (int)Math.Ceiling(d);
                }
            }
            public int TrevorStealth
            {
                get
                {

                    float a = (Values[ID_TREVOR_STEALTH2] * 0.75f);
                    float b = (progressFloatValues.TrevorStealth3 / 45f);
                    float c = (49 + StatModifiers[((int)SP_Stats.Stealth * 4) + 4]);
                    float d = a + b + c;
                    if (d > 100)
                        d = 100;
                    if (d < 0)
                        d = 0;
                    return (int)d;
                }
                set
                {
                    // We need to revert this because old/other editors could've set it too high and we lose precision in calculation.
                    if (Math.Abs(progressFloatValues.TrevorStealth3) > 10000f)
                        progressFloatValues.TrevorStealth3 = 0;

                    Values[ID_TREVOR_STEALTH] = value;
                    float a = (progressFloatValues.TrevorStealth3 / 45f);
                    float b = (49 + StatModifiers[((int)SP_Stats.Stealth * 4) + 4]);
                    float c = (value - (a + b));
                    float d = c / 0.75f;
                    Values[ID_TREVOR_STEALTH2] = (int)Math.Ceiling(d);
                }
            }

            public int MichaelFlying
            {
                get
                {
                    return (int)(((Values[ID_MICHAEL_FLYING2]) + (31 + StatModifiers[((int)SP_Stats.Flying * 4) + 2])
                    + (ModifierCalculator(parent.progressInt64Values.Values[ID_MICHAEL_FLYING3]) / 10f) + (ModifierCalculator(parent.progressInt64Values.Values[ID_MICHAEL_FLYING4]) / 10f)));
                }
                set
                {
                    Values[ID_MICHAEL_FLYING] = value; Values[ID_MICHAEL_FLYING2] = (int)Math.Ceiling(value - ((31 + StatModifiers[((int)SP_Stats.Flying * 4) + 2])
                    + (ModifierCalculator(parent.progressInt64Values.Values[ID_MICHAEL_FLYING3]) / 10f) + (ModifierCalculator(parent.progressInt64Values.Values[ID_MICHAEL_FLYING4]) / 10f)));
                }
            }
            public int FranklinFlying
            {
                get
                {
                    return (int)((Values[ID_FRANKLIN_FLYING2]) + (19 + StatModifiers[((int)SP_Stats.Flying * 4) + 3])
                    + (ModifierCalculator(parent.progressInt64Values.Values[ID_FRANKLIN_FLYING3]) / 10f) + (ModifierCalculator(parent.progressInt64Values.Values[ID_FRANKLIN_FLYING4]) / 10f));
                }
                set
                {
                    Values[ID_FRANKLIN_FLYING] = value; Values[ID_FRANKLIN_FLYING2] = (int)Math.Ceiling(value - ((19 + StatModifiers[((int)SP_Stats.Flying * 4) + 3])
                    + (ModifierCalculator(parent.progressInt64Values.Values[ID_FRANKLIN_FLYING3]) / 10f) + (ModifierCalculator(parent.progressInt64Values.Values[ID_FRANKLIN_FLYING4]) / 10f)));
                }
            }
            public int TrevorFlying
            {
                get
                {
                    return (int)((Values[ID_TREVOR_FLYING2]) + (82 + StatModifiers[((int)SP_Stats.Flying * 4) + 4])
                    + (ModifierCalculator(parent.progressInt64Values.Values[ID_TREVOR_FLYING3]) / 10f) + (ModifierCalculator(parent.progressInt64Values.Values[ID_TREVOR_FLYING4]) / 10f));
                }

                set
                {
                    Values[ID_TREVOR_FLYING] = value; Values[ID_TREVOR_FLYING2] = (int)Math.Ceiling(value - ((82 + StatModifiers[((int)SP_Stats.Flying * 4) + 4])
                  + (ModifierCalculator(parent.progressInt64Values.Values[ID_TREVOR_FLYING3]) / 10f) + (ModifierCalculator(parent.progressInt64Values.Values[ID_TREVOR_FLYING4]) / 10f)));
                }
            }

            public int MichaelDriving
            {
                get { return (int)(((Values[ID_MICHAEL_DRIVING2] / 50.0f) + (54 + StatModifiers[((int)SP_Stats.Driving * 4) + 2]))); }
                set { Values[ID_MICHAEL_DRIVING] = value; Values[ID_MICHAEL_DRIVING2] = (int)((value - (54 + StatModifiers[((int)SP_Stats.Driving * 4) + 2])) * 50.0f); }
            }
            public int FranklinDriving
            {
                get { return (int)(((Values[ID_FRANKLIN_DRIVING2] / 50.0f) + (71 + StatModifiers[((int)SP_Stats.Driving * 4) + 3]))); }
                set { Values[ID_FRANKLIN_DRIVING] = value; Values[ID_FRANKLIN_DRIVING2] = (int)((value - (71 + StatModifiers[((int)SP_Stats.Driving * 4) + 3])) * 50.0f); }
            }
            public int TrevorDriving
            {
                get { return (int)(((Values[ID_TREVOR_DRIVING2] / 50.0f) + (31 + StatModifiers[((int)SP_Stats.Driving * 4) + 4]))); }
                set { Values[ID_TREVOR_DRIVING] = value; Values[ID_TREVOR_DRIVING2] = (int)((value - (31 + StatModifiers[((int)SP_Stats.Driving * 4) + 4])) * 50.0f); }
            }

            public int MichaelLungCapacity
            {
                get { return (int)((((parent.progressInt64Values.MichaelLungCapacity2 / 1000.0f) / 30.0f) + (22 + StatModifiers[((int)SP_Stats.LungCapacity * 4) + 2]))); }
                set { Values[ID_MICHAEL_LUNGCAPACITY] = value; parent.progressInt64Values.MichaelLungCapacity2 = (int)(((value - (22 + StatModifiers[((int)SP_Stats.LungCapacity * 4) + 2])) * 30.0f) * 1000.0f); }
            }
            public int FranklinLungCapacity
            {
                get { return (int)((((parent.progressInt64Values.FranklinLungCapacity2 / 1000.0f) / 30.0f) + (46 + StatModifiers[((int)SP_Stats.LungCapacity * 4) + 3]))); }
                set { Values[ID_FRANKLIN_LUNGCAPACITY] = value; parent.progressInt64Values.FranklinLungCapacity2 = (int)(((value - (46 + StatModifiers[((int)SP_Stats.LungCapacity * 4) + 3])) * 30.0f) * 1000.0f); }
            }
            public int TrevorLungCapacity
            {
                get { return (int)((((parent.progressInt64Values.TrevorLungCapacity2 / 1000.0f) / 30.0f) + (28 + StatModifiers[((int)SP_Stats.LungCapacity * 4) + 4]))); }
                set { Values[ID_TREVOR_LUNGCAPACITY] = value; parent.progressInt64Values.TrevorLungCapacity2 = (int)(((value - (28 + StatModifiers[((int)SP_Stats.LungCapacity * 4) + 4])) * 30.0f) * 1000.0f); }
            }

            public ProgressInt32Values(GrandTheftAutoVClass parent, ProgressMap progressMap)
            {
                this.parent = parent;
                progressFloatValues = parent.progressFloatValues;

                //var entry = progressMap.GetEntry(0x06ED53BB);
                EndianIO pIO = new EndianIO(progressMap.GetEntry(0x06ED53BB).Data, EndianType.BigEndian, true);
                pIO.SeekTo(0x3394);
                for (int i = 0; i < StatModifiers.Length; i++)
                {
                    StatModifiers[i] = pIO.In.ReadInt32();
                }
                pIO.SeekTo(0x3548);
                for (int i = 0; i < StaminaModifiers.Length; i++)
                {
                    StaminaModifiers[i] = pIO.In.ReadSingle();
                }
                pIO.SeekTo(0x3598);
                for (int i = 0; i < ShootingModifiers.Length; i++)
                {
                    ShootingModifiers[i] = pIO.In.ReadInt32();
                }
                pIO.SeekTo(0x35A8);
                for (int i = 0; i < ShootingModifiers2.Length; i++)
                {
                    ShootingModifiers2[i] = pIO.In.ReadInt32();
                }


                mapEntry = progressMap.GetEntry(ID);
                IO = new EndianIO(mapEntry.Data, EndianType.BigEndian, true);
                Values = new Dictionary<uint, int>();
                for (int i = 0; i < IO.In.BaseStream.Length / 8; i++)
                    Values[IO.In.ReadUInt32()] = IO.In.ReadInt32();

                /*
                CompareValues(ID_MICHAEL_STAMINA, MichaelStamina);
                CompareValues(ID_MICHAEL_STRENGTH, MichaelStrength);
                CompareValues(ID_MICHAEL_LUNGCAPACITY, MichaelLungCapacity);
                CompareValues(ID_MICHAEL_DRIVING, MichaelDriving);
                CompareValues(ID_MICHAEL_FLYING, MichaelFlying);
                CompareValues(ID_MICHAEL_SHOOTING, MichaelShooting);
                CompareValues(ID_MICHAEL_STEALTH, MichaelStealth);

                CompareValues(ID_FRANKLIN_STAMINA, FranklinStamina);
                CompareValues(ID_FRANKLIN_STRENGTH, FranklinStrength);
                CompareValues(ID_FRANKLIN_LUNGCAPACITY, FranklinLungCapacity);
                CompareValues(ID_FRANKLIN_DRIVING, FranklinDriving);
                CompareValues(ID_FRANKLIN_FLYING, FranklinFlying);
                CompareValues(ID_FRANKLIN_SHOOTING, FranklinShooting);
                CompareValues(ID_FRANKLIN_STEALTH, FranklinStealth);

                CompareValues(ID_TREVOR_STAMINA, TrevorStamina);
                CompareValues(ID_TREVOR_STRENGTH, TrevorStrength);
                CompareValues(ID_TREVOR_LUNGCAPACITY, TrevorLungCapacity);
                CompareValues(ID_TREVOR_DRIVING, TrevorDriving);
                CompareValues(ID_TREVOR_FLYING, TrevorFlying);
                CompareValues(ID_TREVOR_SHOOTING, TrevorShooting);
                CompareValues(ID_TREVOR_STEALTH, TrevorStealth);*/
            }

            private void CompareValues(uint valuedId, float value2)
            {
                float value1 = Values[valuedId];
                if (Math.Abs(value1 - value2) > Single.Epsilon)
                {
                    throw new Exception(string.Format("invalid value: 0x{0:X8}", valuedId));
                }
            }
            public void Write()
            {
                //var entry = progressMap.GetEntry(0x06ED53BB);
                ProgressMap.ProgressMapEntry pEntry = parent.progressMap.GetEntry(0x06ED53BB);
                EndianIO pIO = new EndianIO(pEntry.Data, EndianType.BigEndian, true);
                pIO.SeekTo(0x3394);
                for (int i = 0; i < StatModifiers.Length; i++)
                {
                    pIO.Out.Write(StatModifiers[i]);
                }
                pIO.SeekTo(0x3548);
                for (int i = 0; i < StaminaModifiers.Length; i++)
                {
                    pIO.Out.Write(StaminaModifiers[i]);
                }
                pIO.SeekTo(0x3598);
                for (int i = 0; i < ShootingModifiers.Length; i++)
                {
                    pIO.Out.Write(ShootingModifiers[i]);
                }
                pIO.SeekTo(0x35A8);
                for (int i = 0; i < ShootingModifiers2.Length; i++)
                {
                    pIO.Out.Write(ShootingModifiers2[i]);
                }
                pEntry.Data = pIO.ToArray();
                pIO.Close();

                // Write out our table
                IO.Out.BaseStream.Position = 0;
                foreach (uint key in Values.Keys)
                {
                    IO.Out.Write(key);
                    IO.Out.Write(Values[key]);
                }
                IO.Stream.SetLength(IO.Out.BaseStream.Position);

                // Set our data
                mapEntry.Data = IO.ToArray();
            }

            private static int ModifierCalculator(long value)
            {
                long val1 = ((((value / 0x3E8) & 0xFFFFFFFF) / 0x3C) / 0x3C) / 0x18;
                long tval2 = ((((value / 0x3E8) & 0xFFFFFFFF) / 0x3C) / 0x3C);
                long val2 = (((tval2 * 0x2AAAAAAB) >> 0x20) >> 2);
                val2 += (val2 >> 31);
                val2 += (val2 << 1);
                val2 = tval2 - (val2 << 3);

                long tval3 = (((value / 0x3E8) & 0xFFFFFFFF) / 0x3C);
                long val3 = ((tval3 * -2004318071) >> 0x20) + tval3;
                val3 = (val3 >> 5);
                val3 += (val3 >> 31);
                val3 *= 0x3C;
                val3 = tval3 - val3;

                return (int)((val1 * 0x05A0) + (val2 * 0x3C) + val3);
            }
        }
        public class ProgressInt64Values
        {
            // required
            private ProgressMap.ProgressMapEntry mapEntry;
            private const uint ID = 0xb8db3f29;
            private EndianIO IO;
            public Dictionary<uint, long> Values;

            // ids
            private const uint ID_MICHAEL_LUNGCAPACITY2 = 0x6B56E540;
            private const uint ID_FRANKLIN_LUNGCAPACITY2 = 0x62137830;
            private const uint ID_TREVOR_LUNGCAPACITY2 = 0x703993D3;

            // exposed values
            public long MichaelLungCapacity2
            {
                get { return Values[ID_MICHAEL_LUNGCAPACITY2]; }
                set { Values[ID_MICHAEL_LUNGCAPACITY2] = value; }
            }

            public long FranklinLungCapacity2
            {
                get { return Values[ID_FRANKLIN_LUNGCAPACITY2]; }
                set { Values[ID_FRANKLIN_LUNGCAPACITY2] = value; }
            }
            public long TrevorLungCapacity2
            {
                get { return Values[ID_TREVOR_LUNGCAPACITY2]; }
                set { Values[ID_TREVOR_LUNGCAPACITY2] = value; }
            }

            public ProgressInt64Values(ProgressMap progressMap)
            {
                mapEntry = progressMap.GetEntry(ID);
                IO = new EndianIO(mapEntry.Data, EndianType.BigEndian, true);
                Values = new Dictionary<uint, long>();
                for (int i = 0; i < IO.In.BaseStream.Length / 12; i++)
                    Values[IO.In.ReadUInt32()] = IO.In.ReadInt64();
            }
            public void Write()
            {
                // Write out our table
                IO.Out.BaseStream.Position = 0;
                foreach (uint key in Values.Keys)
                {
                    IO.Out.Write(key);
                    IO.Out.Write(Values[key]);
                }
                IO.Stream.SetLength(IO.Out.BaseStream.Position);

                // Set our data
                mapEntry.Data = IO.ToArray();
            }
        }

    }
    public static class KeyStore
    {
        public delegate byte[] KeyLoader();

        private static byte[] _aesKey;

        private static KeyLoader _keyLoader;


        static KeyStore()
        {
            // Default Key Loader
            SetKeyLoader(LoadKey);
        }

        public static void SetKeyLoader(KeyLoader loader)
        {
            _keyLoader = loader;
        }
        private static byte[] LoadKey()
        {
            return new byte[]
                {
                    0x66, 0xC0, 0xD6, 0x9E, 0xCE, 0x49, 0xCA, 0x45, 0x76, 0x22, 0xB5, 0x85, 0x8F, 0x29, 0xAC, 0xB0,
                    0x3C, 0xBF, 0xFB, 0x0B, 0x76, 0x14, 0x37, 0x23, 0xA1, 0xC2, 0x63, 0xA6, 0x2A, 0xE9, 0x68, 0xEC
                };

        }
        public static byte[] AESKey
        {
            get
            {
                if (_aesKey == null)
                {
                    _aesKey = _keyLoader();
                }
                return _aesKey;
            }
        }
    }
}
