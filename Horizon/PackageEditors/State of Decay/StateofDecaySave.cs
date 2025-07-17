using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using Crysis;

namespace StateofDecay
{
    public enum StateofDecayGameMode
    {
        Default,
        Breakdown,
        Lifeline
    }
    public class StateofDecayLifelineSave
    {
        [DllImport("class3.dll")]
        private static extern int RtsSaveDecrypt(byte[] pDestination, ref uint pDestSize,
            byte[] pSource, uint pSrcSize, byte[] pxuid);

        [DllImport("class3.dll")]
        private static extern int RtsSaveEncrypt(byte[] pDestination, byte[] pSource, uint pSrcSize, byte[] pxuid);

        private readonly EndianIO IO;
        private readonly EndianIO _saveFileIO;
        private readonly CrysisCryptek _cryptek;
        public RTSSaveDataBuffer RtsSave;
        public StateofDecaySave GameSave;
        private readonly byte[] Creator;

        internal StateofDecayLifelineSave(EndianIO io, CrysisCryptek cryptek, ulong xuid)
        {
            Creator = Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(xuid));

            if (!io.Opened)
                io.Open();

            IO = io;

            var ms = new MemoryStream();

            io.SeekTo(0);
            cryptek.Crypt(io.In.ReadBytes(io.Length), ms, false);

            byte[] dest = new byte[io.Length];
            uint destSize = 0;

            if (RtsSaveDecrypt(dest, ref destSize, ms.ToArray(), (uint)ms.Length, Creator) != io.Length)
                throw new Exception("SoD: error while reading Lifeline save");

            ms = new MemoryStream();
            ms.Write(dest, 0, dest.Length);

            _saveFileIO = new EndianIO(ms, EndianType.BigEndian, true);
            //_saveFileIO.ToArray().Save(@"F:\Projects\State of Decay\Saves\Lifeline\Freezaqw\Highway.ulsave");
            _cryptek = cryptek;

            _saveFileIO.SeekTo(0x3C);

            GameSave = new StateofDecaySave(_saveFileIO, _saveFileIO.In.ReadBytes(IO.Length - 0x3C));
            //_saveFileIO.ToArray().Save(@"F:\Projects\State of Decay\Saves\Lifeline\Save 1\Highway.ulsave");
        }
        internal void Save()
        {
            GameSave.Save();

            byte[] dest = new byte[_saveFileIO.Length];
            var src = _saveFileIO.ToArray();

            if (RtsSaveEncrypt(dest, src, (uint)_saveFileIO.Length, Creator) != _saveFileIO.Length)
                throw new Exception("SoD: error while writing Lifeline save");

            var ms = new MemoryStream();
            // encrypt the data
            _cryptek.Crypt(dest, ms, true);

            IO.Stream.SetLength(ms.Length);
            IO.SeekTo(0);
            IO.Out.Write(ms.ToArray());
        }
        public void ExtractDataBufferToFile(string filename)
        {
            _saveFileIO.Stream.Save(filename);
        }
        internal void InjectData(byte[] data)
        {
            byte[] dest = new byte[data.Length];
            var src = data;

            int saveEncLen = RtsSaveEncrypt(dest, src, (uint) data.Length, Creator);
            if (saveEncLen != data.Length)
                throw new Exception("SoD: error while writing Lifeline save");

            var ms = new MemoryStream();
            // encrypt the data
            _cryptek.Crypt(dest, ms, true);

            IO.Stream.SetLength(ms.Length);
            IO.SeekTo(0);
            IO.Out.Write(ms.ToArray());
        }
    }
    public class StateofDecayBreakdownSave
    {
        [DllImport("class3.dll")]
        private static extern int RtsSaveDecrypt(byte[] pDestination, ref uint pDestSize,
            byte[] pSource, uint pSrcSize, byte[] pxuid);

        [DllImport("class3.dll")]
        private static extern int RtsSaveEncrypt(byte[] pDestination, byte[] pSource, uint pSrcSize, byte[] pxuid);

        private readonly EndianIO IO;
        private readonly EndianIO _saveFileIO;
        private readonly CrysisCryptek _cryptek;

        public RTSSaveDataBuffer RtsSave;
        public StateofDecaySave GameSave;
        private readonly byte[] Creator;

        internal StateofDecayBreakdownSave(EndianIO io, CrysisCryptek cryptek, ulong xuid)
        {
            Creator = Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(xuid));

            if (!io.Opened)
                io.Open();

            IO = io;

            var ms = new MemoryStream();

            io.SeekTo(0);
            cryptek.Crypt(io.In.ReadBytes(io.Length), ms, false);

            byte[] dest = new byte[io.Length];
            uint destSize = 0;

            if(RtsSaveDecrypt(dest, ref destSize, ms.ToArray(), (uint)ms.Length, Creator) != io.Length)
                throw new Exception("SoD: error while reading Breakdown save");
  
            ms = new MemoryStream();
            ms.Write(dest, 0, dest.Length);

            _saveFileIO = new EndianIO(ms, EndianType.BigEndian, true);

            //_saveFileIO.ToArray().Save(@"F:\Projects\State of Decay\Saves\Lifeline\Freezaqw\Sandbox.ulsave");
            
            _cryptek = cryptek;

            _saveFileIO.SeekTo(0x3C);

            GameSave = new StateofDecaySave(_saveFileIO, _saveFileIO.In.ReadBytes(IO.Length - 0x3C));
        }

        internal StateofDecayBreakdownSave(string filename)
        {
            IO = new EndianIO(filename, EndianType.BigEndian, true);

            IO.SeekTo(0x3C);
            GameSave = new StateofDecaySave(IO, IO.In.ReadBytes(IO.Length - 0x3C));
            IO.Close();
        }

        internal void Save()
        {
            GameSave.Save();

            byte[] dest = new byte[_saveFileIO.Length];
            var src = _saveFileIO.ToArray();

            if (RtsSaveEncrypt(dest, src, (uint)_saveFileIO.Length, Creator) != _saveFileIO.Length)
                throw new Exception("SoD: error while writing Breakdown save");

            var ms = new MemoryStream();
            // encrypt the data
            _cryptek.Crypt(dest, ms, true);

            IO.Stream.SetLength(ms.Length);
            IO.SeekTo(0);
            IO.Out.Write(ms.ToArray());
        }
        public void ExtractDataBufferToFile(string filename)
        {
            _saveFileIO.Stream.Save(filename);
        }
        internal void InjectData(byte[] data)
        {
            byte[] dest = new byte[data.Length];
            var src = data;

            if (RtsSaveEncrypt(dest, src, (uint)data.Length, Creator) != data.Length)
                throw new Exception("SoD: error while writing Breakdown save");

            var ms = new MemoryStream();
            // encrypt the data
            _cryptek.Crypt(dest, ms, true);

            IO.Stream.SetLength(ms.Length);
            IO.SeekTo(0);
            IO.Out.Write(ms.ToArray());
        }
    }
    public class StateofDecaySave
    {
        private readonly EndianIO IO;
        private EndianIO _saveFileIO;
        private readonly CrysisCryptek _cryptek;
        public RTSSaveDataBuffer RtsSave;
        public List<NSCommunity> Enclaves;

        public DateTime TimeStamp;
        public float Fame;
        public float Influence;
        public NSGlobals Globals;

        // pointers
        private long TimeStampLocation;
        private long CharactersStartLocation = -1;
        private long CharactersEndLocation = -1;
        private long FameLocation = -1;
        private long GlobalsLocation = -1;
        private long EnclavesStartLocation = -1;
        private long EnclavesEndLocation = -1;

        internal uint GameVersion;
        private bool IsDLCSave;

        internal StateofDecaySave(EndianIO io, CrysisCryptek cryptek)
        {
            IO = io;

            var ms = new MemoryStream();

            io.SeekTo(0);
            cryptek.Crypt(io.In.ReadBytes(io.Length), ms, false);

            _saveFileIO = new EndianIO(ms, EndianType.BigEndian, true);

            //_saveFileIO.ToArray().Save(@"F:\Projects\State of Decay\Saves\Lifeline\Freezaqw\Sandbox.ulsave");
            _cryptek = cryptek;

            _saveFileIO.SeekTo(0);
            GameVersion = _saveFileIO.In.ReadUInt32(); // version for determining save format

            if (GameVersion > 0x5356000A)
                throw new Exception("SoD: Invalid save game version detected. This is most likely an incompability with the editor as the result of a new Title Update. Report to the Developer!");
            if (_saveFileIO.In.ReadInt32() != IO.Length)
                throw new Exception("SoD: invalid save length found in header");

            Read();
        }

        internal StateofDecaySave(EndianIO io, byte[] saveData)
        {
            // we are loading DLC(Breakdown or Lifeline) save files
            IsDLCSave = true;

            IO = io;

            var ms = new MemoryStream();
            ms.Write(saveData, 0, saveData.Length);

            _saveFileIO = new EndianIO(ms, EndianType.BigEndian, true);

            _saveFileIO.SeekTo(0);
            GameVersion = RTSSaveDataBuffer.StaticConvert32(_saveFileIO.In.ReadUInt32());

            if (_saveFileIO.In.ReadInt32() != IO.Length)
                throw new Exception("SoD: invalid file length found in Breakdown save header");

            //if (RTSSaveDataBuffer.StaticConvert32(RTSSaveDataBuffer.RtsCipherObfuscate(_saveFileIO.In.ReadUInt32(), 0x1F)) != IO.Length)
                //throw new Exception("SoD: invalid file length found in Breakdown save header");

            Read();
        }

        private void Read()
        {
            // addresses are for v1 of the game executable
            int v, count, vl;//, crt = 0;

            RtsSave = new RTSSaveDataBuffer(_saveFileIO, GameVersion);

            if (GameVersion >= 0x5356000A)
            {
                RtsSave.IO.In.ReadUInt32();  //hash ?
            }

            var version = RtsSave.ReadInt32();
            
            //rtsSave.SetVersion(version);
            // 833449A0
            if (RTSSaveDataBuffer.IsValidSaveVersion(version, 0x52540000, 0xFFFF0000))
            {
                if (version >= 0x52540023)
                {
                    RtsSave.ReadInt32();
                    RtsSave.ReadInt32();
                }
                if (version >= 0x52540003)
                    RtsSave.ReadInt32();
                if (version >= 0x52540005)
                    RtsSave.ReadInt32();
                if (version >= 0x5254001C)
                    RtsSave.ReadInt32();
                if (version >= 0x52540006)
                {
                    count = RtsSave.ReadInt32();
                    for (var i = 0; i < count; i++)
                    {
                        RtsSave.ReadInt32();
                        // 83338930
                        {
                            v = RtsSave.ReadInt32();
                            if (RTSSaveDataBuffer.IsValidSaveVersion(v, 0x42550000, 0xFFFF0000))
                            {
                                if (v >= 0x4255000A)
                                    RtsSave.ReadInt32();
                                else
                                    RtsSave.ReadInt16();
                                // 8332F860 - RTSHandle_Load
                                {
                                    RtsSave.RTSHandleLoad();
                                }
                                if (v < 0x42550009)
                                {
                                    RtsSave.ReadInt16();
                                    RtsSave.ReadByte();
                                }
                            }
                        }
                        // 83333D70
                        if (version < 0x5254001F) // -tu4
                        {
                            v = RtsSave.ReadInt32();
                            if (RTSSaveDataBuffer.IsValidSaveVersion(v, 0x42550000, 0xFFFF0000))
                            {
                                var ct = RtsSave.ReadByte();
                                for (int j = 0; j < ct; j++)
                                {
                                    RtsSave.ReadByte();
                                    RtsSave.ReadByte();
                                }
                            }
                        }
                    }
                }
                // 83335BE8 - tu4
                if (version >= 0x5254001F)
                {
                    v = RtsSave.ReadInt32();
                    if (RTSSaveDataBuffer.IsValidSaveVersion(v, 0x50520000, 0xFFFF0000))
                    {
                        v = RtsSave.PeekInt32();
                        while(v != 0x50524E44)
                        {
                            RtsSave.ReadInt16();
                            RtsSave.ReadByte();
                            v = RtsSave.PeekInt32();
                        }
                        RtsSave.ReadInt32();
                    }
                }
                // 83344C10
                if (version >= 0x52540010)
                {
                    if (version < 0x52540020) // 833470D4
                    {
                        count = RtsSave.ReadInt32();
                        for (int i = 0; i < count; i++)
                        {
                            RtsSave.ReadInt32();
                            if (version >= 0x52540011)
                                RtsSave.ReadInt16();

                            RtsSave.ReadInt16();
                            RtsSave.ReadByte();
                        }
                    }
                    else if (version >= 0x52540020) // 83347280
                    {
                        count = RtsSave.ReadInt32();
                        for (int i = 0; i < count; i++)
                        {
                            RtsSave.ReadInt32();
                            RtsSave.ReadByte();
                            if (version >= 0x52540022)
                            {
                                RtsSave.ReadInt16();
                            }
                            else
                            {
                                RtsSave.ReadByte();
                            }
                        }
                    }
                }
                else if ((version < 0x52540010) && (version >= 0x52540020)) // 83347280
                {
                    count = RtsSave.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        RtsSave.ReadInt32();
                        RtsSave.ReadByte();
                        if (version >= 0x52540022)
                        {
                            RtsSave.ReadInt16();
                        }
                        else
                        {
                            RtsSave.ReadByte();
                        }
                    }
                }
                // 83347370 - tu4
                if (version >= 0x52540021)
                {
                    count = RtsSave.ReadInt16();
                    int counter = 0;
                    if(count != 0)
                    {
                        do
                        {
                            RtsSave.ReadInt32();
                        } while ((counter += 0x20) < count);
                    }
                }
                // 83344D40
                if (version >= 0x5254000E)
                {
                    // 833335B0
                    v = RtsSave.ReadInt32();
                    if (RTSSaveDataBuffer.IsValidSaveVersion(v, 0x53550000, 0xFFFF0000))
                    {
                        count = RtsSave.ReadInt32();
                        for (int i = 0; i < count; i++)
                        {
                            RtsSave.ReadInt32();
                            RtsSave.ReadByte();
                        }
                    }
                }
                // 83344D5C
                if (version >= 0x52540015)
                {
                    // 8333E6B0
                    v = RtsSave.ReadInt32();
                    if (RTSSaveDataBuffer.IsValidSaveVersion(v, 0x44500000, 0xFFFF0000))
                    {
                        count = RtsSave.ReadInt32();
                        for (int i = 0; i < count; i++)
                        {
                            RtsSave.ReadInt32();
                            RtsSave.ReadByte();
                        }
                    }
                }
            }
            // 832F3228 - Read Globals
            v = RtsSave.ReadInt32();
            if (RTSSaveDataBuffer.IsValidSaveVersion(v, 0x52440000, 0xFFFF0000))
            {
                GlobalsLocation = RtsSave.Position;
                Globals = new NSGlobals();
                var id = RtsSave.ReadUInt32();
                while (id != 0x52444544)
                {
                    Globals.Add(new NSGlobal { Id = id, Value = RtsSave.ReadFloat() });
                    id = RtsSave.ReadUInt32();
                }
            }
            // 833476F8
            if (version >= 0x5254000C)
            {
                // 833C19A8
                var vt = RtsSave.ReadInt32();
                if (RTSSaveDataBuffer.IsValidSaveVersion(vt, 0x52410000, 0xFFFF0000))
                {
                    count = RtsSave.ReadInt32();
                    for (var i = 0; i < count; i++)
                    {
                        v = RtsSave.ReadInt32();
                        // 833BB330
                        if (RTSSaveDataBuffer.IsValidSaveVersion(v, 0x544F0000, 0xFFFF0000))
                        {
                            if(v >= 0x544F0003)
                                RtsSave.RTSHandleLoad();

                            RtsSave.ReadInt32();
                            if (v >= 0x544F0005)
                                RtsSave.ReadInt32();
                            if (v < 0x544F0006)
                                RtsSave.ReadInt32();

                            if(v < 0x544F0002)
                                throw new Exception("version >= TIMED_OUTPUT_VERSION_HASHES");
                            RtsSave.ReadInt32();
                            if (v >= 0x544F0004)
                                RtsSave.ReadInt32();

                            RtsSave.ReadInt32();
                        }
                    }
                }
                // 833C1A74
                if (vt >= 0x52410001)
                {
                    count = RtsSave.ReadInt32();
                    for (var i = 0; i < count; i++)
                    {
                        RtsSave.RTSHandleLoad();
                        RtsSave.ReadInt32();
                        RtsSave.ReadInt32();
                    }
                }
                // 833C1B6C
                if (vt >= 0x52410002)
                {
                    count = RtsSave.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        RtsSave.RTSHandleLoad();
                        RtsSave.ReadInt32();
                        RtsSave.ReadInt32();
                        for (int j = 0; j < 0x14; j++)
                        {
                            RtsSave.RTSHandleLoad();
                        }
                    }
                }
                // 833C1C90
                if (vt >= 0x52410003)
                {
                    count = RtsSave.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        RtsSave.ReadInt32();
                        if(vt >= 0x52410004)
                            RtsSave.RTSHandleLoad();
                    }
                }
            }
            // 83347708 
            if (version >= 0x52540004)
            {
                v = RtsSave.ReadInt32(); //- lifeline crash here
                if (RTSSaveDataBuffer.IsValidSaveVersion(v, 0x434F4D00, 0xFFFFFF00))
                {
                    RtsSave.Characters = new List<NSCharacter>();
                    if (v >= 0x434F4D06)
                    {
                        // 83415CE0 - Load Character Model list
                        {
                            var vt = RtsSave.ReadInt32();
                            if (RTSSaveDataBuffer.IsValidSaveVersion(vt, 0x43484700, 0xFFFFFF00))
                            {
                                RtsSave.ReadInt32();
                                if (vt >= 0x43484702)
                                {
                                    count = RtsSave.ReadInt32();
                                    for (var i = 0; i < count; i++)
                                    {
                                        RtsSave.ReadInt32();
                                    }
                                }
                            }
                        }
                        RtsSave.ReadInt32();
                        CharactersStartLocation = RtsSave.Position;
                        for (var i = 0; i < 0x64; i++)
                        {
                            var character = new NSCharacter(RtsSave);
                            //if (character.IsLoaded)
                            RtsSave.Characters.Add(character);
                        }
                        CharactersEndLocation = RtsSave.Position;
                    }
                    else
                    {
                        count = RtsSave.ReadInt32();
                        if (v >= 0x434F4D02)
                            count = RtsSave.ReadInt32();

                        CharactersStartLocation = RtsSave.Position;
                        for (var i = 0; i < count; i++)
                        {
                            var character = new NSCharacter(RtsSave);
                            //if (character.IsLoaded)
                            RtsSave.Characters.Add(character);
                        }
                        CharactersEndLocation = RtsSave.Position;
                    }
                    // 8339FDDC
                    if (v >= 0x434F4D08)
                    {
                        RtsSave.ReadInt16();
                    }
                    if (v >= 0x434F4D04)
                    {
                        count = 0x1E;
                        if (v >= 0x434F4D05)
                        {
                            count = RtsSave.ReadInt32();
                        }
                        EnclavesStartLocation = RtsSave.Position;
                        Enclaves = new List<NSCommunity>();
                        for (var i = 0; i < count; i++)
                        {
                            // 8339DE00 - NSCommunity::Enclave::Load
                            var comunity = new NSCommunity(RtsSave);
                            comunity.LoadEnclave();
                            Enclaves.Add(comunity);
                        }
                        EnclavesEndLocation = RtsSave.Position;
                    }
                    if (v >= 0x434F4D07)
                    {
                        count = RtsSave.ReadInt32();
                        for (var i = 0; i < count; i++)
                        {
                            // 8339A9E0
                            NSCommunity.LoadCSearchParty(RtsSave);
                        }
                    }
                    if (v >= 0x434F4D03)
                    {
                        if (v < 0x434F4D09)
                        {
                            count = RtsSave.ReadInt32();
                            for (var i = 0; i < count; i++)
                            {
                                RtsSave.ReadInt32();
                            }
                            count = RtsSave.ReadInt32();
                            for (var i = 0; i < count; i++)
                            {
                                RtsSave.ReadInt32();
                            }
                            count = RtsSave.ReadInt32();
                            for (var i = 0; i < count; i++)
                            {
                                RtsSave.ReadInt32();
                            }
                            count = RtsSave.ReadInt32();
                            for (var i = 0; i < count; i++)
                            {
                                RtsSave.ReadInt32();
                            }
                        }
                        else
                        {
                            if (v < 0x434F4D0A)
                            {

                            }
                        }
                    }
                    // 83538C50
                    {
                        var vt = RtsSave.ReadInt32();
                        if (RTSSaveDataBuffer.IsValidSaveVersion(vt, 0x464D0000, 0xFFFF0000))
                        {
                            FameLocation = RtsSave.Position;
                            if (vt >= 0x464D0004)
                            {
                                Influence = RtsSave.ReadFloat();
                                Fame = RtsSave.ReadFloat();
                            }
                            else
                            {
                                Influence = RtsSave.ReadFloat();
                                Fame = RtsSave.ReadFloat();
                            }

                            if (vt < 0x464D0005)
                            {
                                count = RtsSave.ReadInt32();
                                for (var i = 0; i < count; i++)
                                {
                                    RtsSave.ReadInt32();
                                }
                            }
                        }
                    }
                    if (v >= 0x434F4D0B)
                        RtsSave.ReadInt32();
                    if(v >= 0x434F4D0C)
                        RtsSave.RTSHandleLoad();
                }
                // 833168B0
                v = RtsSave.ReadInt32();
                if (RTSSaveDataBuffer.IsValidSaveVersion(v, 0x484F0000, 0xFFFF0000))
                {
                    count = RtsSave.ReadInt32();
                    for (var i = 0; i < count; i++)
                    {
                        RtsSave.ReadInt64();
                        RtsSave.ReadByte();
                    }
                    if (v >= 0x484F0003)
                    {
                        RtsSave.ReadInt64();
                    }
                    else
                    {
                        RtsSave.ReadInt32();
                    }
                    int a = RtsSave.ReadInt32(), j = 0;
                    if (a > 0)
                    {
                        // 834FE828 - TU5
                        var resume = true;
                        while (resume && (j) < a)
                        {
                            //resume = false;
                            var vt = RtsSave.ReadInt32();
                            if (RTSSaveDataBuffer.IsValidSaveVersion(vt, 0x46414300, 0xFFFFFF00))
                            {
                                if (vt >= 0x4641430A)
                                {
                                    RtsSave.ReadByte();
                                    RtsSave.ReadInt16();
                                }
                                uint k;
                                if (vt >= 0x46414308)
                                {
                                    k  = RtsSave.ReadUInt32();
                                    RtsSave.ReadInt32();
                                    RtsSave.ReadInt32();
                                }
                                else
                                {
                                    k = RtsSave.ReadUInt32();
                                    RtsSave.ReadInt32();
                                    RtsSave.ReadInt32();
                                }
                                RtsSave.ReadInt32();
                                if (vt < 0x4641430F)
                                    RtsSave.ReadInt32();
                                if (vt >= 0x46414304)
                                {
                                    if (vt >= 0x46414309)
                                    {
                                        //8338AE30
                                        var vf = RtsSave.ReadInt32();
                                        if (RTSSaveDataBuffer.IsValidSaveVersion(vf, 0x43490000, 0xFFFF0000))
                                        {
                                            RtsSave.ReadInt16();
                                            RtsSave.ReadInt16();
                                        }
                                        else
                                        {
                                            resume = false;
                                        }
                                    }
                                    else
                                    {
                                        RtsSave.ReadInt32();
                                    }
                                }
                                if (vt >= 0x46414302)
                                {
                                    RtsSave.ReadInt64();
                                }
                                if (vt >= 0x46414306 && vt < 0x4641430D)
                                {
                                    RtsSave.ReadInt64();
                                }

                                if (k == 0x4d1f1272) // Outpost
                                {
                                    if (vt >= 0x46414305)
                                    {
                                        RtsSave.ReadInt32();
                                        RtsSave.ReadInt32();
                                        RtsSave.ReadInt32();
                                    }
                                    if (vt >= 0x4641430B)
                                    {
                                        RtsSave.ReadInt32();
                                        RtsSave.ReadInt32();
                                    }
                                }
                                else if (k == 0xc1b08860)
                                {
                                    for (int i = 0; i < 2; i++)
                                    {
                                        RtsSave.ReadInt32();
                                        RtsSave.ReadInt32();
                                        RtsSave.ReadInt32();
                                        RtsSave.ReadInt32();
                                        RtsSave.ReadInt32();
                                    }
                                }
                                if (vt >= 0x46414307)
                                {
                                    if (vt >= 0x46414308)
                                    {
                                        RtsSave.ReadInt32();
                                    }
                                    else
                                    {
                                        RtsSave.ReadInt32();
                                    }
                                }
                                if (vt >= 0x4641430C)
                                    RtsSave.ReadInt32();
                                if (vt >= 0x4641430E)
                                {
                                    RtsSave.RTSHandleLoad();
                                }
                            }
                            else
                            {
                                resume = false;
                            }
                            j++;
                        }
                    }
                    if (v >= 0x484F0004)
                    {
                        // 834C57F8
                        var vt = RtsSave.ReadInt32();
                        if (RTSSaveDataBuffer.IsValidSaveVersion(vt, 0x544F0000, 0xFFFF0000))
                        {
                            if (vt < 0x544F0003)
                            {
                                count = RtsSave.ReadInt32();
                                for (var i = 0; i < count; i++)
                                {
                                    vt = RtsSave.ReadInt32();
                                    if (RTSSaveDataBuffer.IsValidSaveVersion(vt, 0x544F0000, 0xFFFF0000))
                                    {
                                        RtsSave.ReadInt32();
                                        RtsSave.ReadInt32();
                                        RtsSave.ReadInt32();
                                        RtsSave.ReadInt32();
                                    }
                                }
                            }
                        }
                    }
                    if (v >= 0x484F0008)
                    {
                        a = RtsSave.ReadInt32();
                        if (a > 0)
                        {
                            // 834CE1D8
                            for (int i = 0; i < a; i++)
                            {
                                var vt = RtsSave.ReadInt32();
                                if (RTSSaveDataBuffer.IsValidSaveVersion(vt, 0x53464100, 0xFFFFFF00))
                                {
                                    RtsSave.ReadInt32();
                                    RtsSave.ReadInt32();
                                    RtsSave.ReadInt32();
                                    RtsSave.ReadInt32();
                                    RtsSave.ReadInt32();
                                    RtsSave.ReadInt32();
                                    RtsSave.ReadInt32();
                                    RtsSave.ReadInt32();
                                    RtsSave.ReadInt32();
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    if (v >= 0x484F000B)
                    {
                        RtsSave.ReadInt32();
                        RtsSave.ReadByte();
                    }
                    if (v >= 0x484F0002 && v < 0x484F0009)
                    {
                        for (var i = 0; i < 3; i++)
                        {
                            RtsSave.ReadInt32();
                        }
                    }
                    if (v >= 0x484F0005 && v < 0x484F0006)
                    {
                        count = RtsSave.ReadInt32();
                        for (int i = 0; i < count; i++)
                        {
                            // 83312E58
                            vl = RtsSave.ReadInt32();
                            if (RTSSaveDataBuffer.IsValidSaveVersion(vl, 0x4F550000, 0xFFFF0000))
                            {
                                RtsSave.ReadInt32();
                                RtsSave.ReadInt32();
                                RtsSave.ReadInt32();
                                RtsSave.ReadByte();
                                RtsSave.ReadByte();
                            }
                        }
                    }
                }
            }
            // 83504E98 (tu4=835194C8)
            {
                vl = RtsSave.ReadInt32();
                if (RTSSaveDataBuffer.IsValidSaveVersion(vl, 0x46410000, 0xFFFF0000))
                {
                    RtsSave.ReadInt32();
                    if (vl >= 0x46410008)
                    {
                        count = RtsSave.ReadInt32();
                        for (int i = 0; i < count; i++)
                        {
                            RtsSave.ReadInt32();
                            RtsSave.ReadByte();
                        }
                    }
                }
            }
            // 83371518 (tu4=8337A180)
            {
                vl = RtsSave.ReadInt32();
                if (RTSSaveDataBuffer.IsValidSaveVersion(vl, 0x43470000, 0xFFFF0000))
                {
                    if (vl < 0x43470004)
                    {
                        count = vl >= 0x43470002 ? RtsSave.ReadInt32() : 0x0C;
                        for (var i = 0; i < count; i++)
                        {
                            RtsSave.ReadInt32();
                            var ct = RtsSave.ReadInt32();
                            for (var j = 0; j < ct; j++)
                            {
                                RtsSave.ReadByte();
                                RtsSave.ReadByte();
                            }
                        }
                    }
                    if (vl >= 0x43470003)
                    {
                        RtsSave.ReadInt32();
                        count = RtsSave.ReadInt32();
                        for (var i = 0; i < count; i++)
                        {
                            if (vl >= 0x43470005)
                                RtsSave.ReadInt16();
                            else
                                RtsSave.ReadByte();
                        }
                    }
                }
            }
            // read vehicles
            if (version >= 0x5254000B)
            {
                // 833AC940 (tu4=833B5758)
                vl = RtsSave.ReadInt32();
                if (RTSSaveDataBuffer.IsValidSaveVersion(vl, 0x564D0000, 0xFFFF0000))
                {
                    RtsSave.ReadInt32();
                    count = RtsSave.ReadInt32();
                    for (var i = 0; i < count; i++)
                    {
                        RtsSave.ReadInt16();
                        var f = RtsSave.ReadUInt32(); // vehicle id
                        byte vflags = 0;
                        if (vl >= 0x564D0004)
                            vflags = RtsSave.ReadByte();
                        if (f != 0 || vl < 0x564D0003)
                        {
                            if (vl >= 0x564D0008)
                            {
                                for (var x = 0; x < 7; x++)
                                {
                                    RtsSave.ReadBytesInternal(4);
                                }
                            }
                            else
                            {
                                RtsSave.ReadBytesInternal(0x1C);
                            }
                            if (vl >= 0x564D0002)
                            {
                                var ct = RtsSave.ReadInt32();
                                for (var j = 0; j < ct; j++)
                                {
                                    RtsSave.ReadFloat();
                                    if (vl >= 0x564D0005)
                                    {
                                        RtsSave.ReadByte();
                                    }
                                }
                                if (vl >= 0x564D0007 && (vflags & 0x8) != 0)
                                {
                                    NSCharacter.LoadInventory(RtsSave);
                                }
                            }
                        }
                    }
                }
            }
            if (version >= 0x5254000D) 
            {
                // 832912A8
                v = RtsSave.ReadInt32();
                if (RTSSaveDataBuffer.IsValidSaveVersion(v, 0x5A4D0000, 0xFFFF0000))
                {
                    if (v >= 0x5A4D0002)
                        RtsSave.ReadInt16();
                    count = RtsSave.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        RtsSave.ReadInt32();
                        RtsSave.ReadInt32();
                        if (v >= 0x5A4D0002)
                        {
                            RtsSave.RTSHandleLoad();
                            RtsSave.RTSHandleLoad();
                        }
                        if (v >= 0x5A4D0003)
                            RtsSave.ReadByte();
                    }
                    RtsSave.ReadInt32();
                }
            }
            if (version >= 0x5254000F)
            {
                // 83517270
                v = RtsSave.ReadInt32();
                if (RTSSaveDataBuffer.IsValidSaveVersion(v, 0x524F0000, 0xFFFF0000))
                {
                    count = RtsSave.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        RtsSave.ReadInt32();
                        RtsSave.ReadInt32();
                        RtsSave.ReadInt32();
                        RtsSave.ReadInt32();
                    }
                }
            }
            if (version >= 0x52540012)
            {
                // 833CE2A8
                v = RtsSave.ReadInt32();
                if (RTSSaveDataBuffer.IsValidSaveVersion(v, 0x524F0000, 0xFFFF0000) ||
                    RTSSaveDataBuffer.IsValidSaveVersion(v, 0x454C0000, 0xFFFF0000))
                {
                    var f = (v & 0xFFFF);
                    var ct = RtsSave.ReadInt32();
                    var ck = RtsSave.ReadInt32();
                    var ch = 0;
                    if (f < 8)
                        ch = RtsSave.ReadInt32();
                    var cf = 0;
                    if (f >= 5)
                        cf = RtsSave.ReadInt32();
                    var cl = 0;
                    if (f >= 8)
                        cl = RtsSave.ReadInt32();
                    if (f >= 2)
                        RtsSave.ReadInt32();
                    count = RtsSave.ReadInt32();
                    for (var i = 0; i < count; i++)
                    {
                        RtsSave.ReadInt32();
                        RtsSave.ReadInt32();
                        RtsSave.ReadInt32();
                        RtsSave.ReadInt32();
                        RtsSave.ReadByte();
                        if (f >= 3)
                            RtsSave.ReadInt32();
                        for (int j = 0; j < ct; j++)
                        {
                            RtsSave.ReadInt32();
                            RtsSave.ReadByte();
                        }
                        for (int j = 0; j < ck; j++)
                        {
                            if (f >= 8)
                            {
                                RtsSave.ReadByte();
                                RtsSave.ReadByte();
                                if (f >= 9)
                                {
                                    RtsSave.ReadInt32();
                                    RtsSave.ReadInt32();
                                    RtsSave.ReadInt32();
                                }
                                else
                                {
                                    RtsSave.ReadInt32();
                                }
                                for (int k = 0; k < cl; k++)
                                {
                                    RtsSave.ReadInt32();
                                    RtsSave.ReadInt32();
                                }
                            }
                            else
                            {
                                RtsSave.ReadInt32();
                                if (f >= 7)
                                {
                                    RtsSave.ReadInt32();
                                }
                                else
                                {
                                    RtsSave.RTSHandleLoad();
                                }
                                for (int k = 0; k < ch; k++)
                                {
                                    RtsSave.ReadInt32();
                                }
                            }
                        }
                        for (int j = 0; j < cf; j++)
                        {
                            RtsSave.ReadByte();
                            RtsSave.ReadByte();
                            if (f >= 6)
                                RtsSave.ReadInt32();
                        }
                    }
                }
            }
            if (version >= 0x52540018)
            {
                // 832E8720
                v = RtsSave.ReadInt32();
                if (RTSSaveDataBuffer.IsValidSaveVersion(v, 0x41435400, 0xFFFFFF00))
                {
                    count = RtsSave.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        // 832E44E8 - NSActivity_CActivityDeliverRucksack_Load
                        vl = RtsSave.ReadInt32();
                        if (RTSSaveDataBuffer.IsValidSaveVersion(vl, 0x41445200, 0xFFFFFF00))
                        {
                            RtsSave.ReadInt32();
                            RtsSave.ReadInt32();
                            RtsSave.ReadInt32();
                            var f = RtsSave.ReadByte();
                            if (vl >= 0x41445202)
                                RtsSave.ReadByte();
                            if (f < 1)
                            {
                                RtsSave.ReadInt32();
                                RtsSave.ReadInt32();
                                RtsSave.ReadByte();
                                NSCharacter.LoadInventory(RtsSave);
                            }
                            else if(f==1)
                            {
                                RtsSave.ReadByte();
                                RtsSave.ReadInt32();
                                RtsSave.RTSHandleLoad();
                            }
                            if (vl >= 0x41445203)
                            {
                                RtsSave.ReadInt32();
                                RtsSave.ReadInt32();
                            }
                        }
                    }
                }
            }
            if (version >= 0x52540019)
            {
                // 83514080
                v = RtsSave.ReadInt32();
                if (RTSSaveDataBuffer.IsValidSaveVersion(v, 0x48490000, 0xFFFF0000))
                {
                    count = RtsSave.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        RtsSave.ReadInt32();
                        RtsSave.ReadInt32();
                        RtsSave.ReadInt32();
                    }

                }
            }
            if (version >= 0x5254001D)
            {
                // 8329A718
                v = RtsSave.ReadInt32();
                if (RTSSaveDataBuffer.IsValidSaveVersion(v, 0x434D0000, 0xFFFF0000))
                {
                    RtsSave.RTSHandleLoad();
                    RtsSave.ReadInt32();
                    RtsSave.ReadInt32();
                    RtsSave.ReadInt32();
                    RtsSave.ReadInt32();
                }
            }
            if (version >= 0x52540007)
            {
                // read datetime parts
                TimeStampLocation = RtsSave.Position;

                long d2 = RtsSave.ReadUInt32();
                long d1 = RtsSave.ReadUInt32();
                long datetime = (d1 << 32);
                datetime |= d2;
                TimeStamp = DateTime.FromFileTimeUtc(datetime);
            }
            if (version >= 0x5254001A)
            {
                RtsSave.ReadInt32();
            }
            if (version >= 0x5254001B)
            {
                RtsSave.ReadInt32();
            }
            // EOF
        }

        public void Save()
        {
            // write the timestamp
            RtsSave.Position = TimeStampLocation;
            var dt = TimeStamp.ToFileTimeUtc();
            var dt1 = (int)((dt >> 32) & 0xFFFFFFFF);
            var dt2 = (int)(dt & 0xFFFFFFFF);
            RtsSave.WriteInt32(dt2);
            RtsSave.WriteInt32(dt1);

            // write fame and influence
            RtsSave.Position = FameLocation;
            RtsSave.WriteSingle(Influence);
            RtsSave.WriteSingle(Fame);

            // write the globals
            RtsSave.Position = GlobalsLocation;
            foreach (var nsGlobal in Globals)
            {
                RtsSave.WriteUInt32(nsGlobal.Id);
                RtsSave.WriteSingle(nsGlobal.Value);
            }
            RtsSave.WriteUInt32(0x52444544);

            // reload a new RTS class for writing the characters
            var output = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            
            output.Out.Write(_saveFileIO.ToArray());

            output.Position = CharactersStartLocation;
            // write skill
            foreach (var character in RtsSave.Characters)
            {
                output.Out.Write(character.Write());
            }

            RtsSave.Position = CharactersEndLocation;
            output.Out.Write(RtsSave.IO.In.ReadBytes(EnclavesStartLocation - RtsSave.Position));

            // write communities (enclaves)
            foreach (var enclave in Enclaves)
            {
                output.Out.Write(enclave.Write());
            }

            RtsSave.Position = EnclavesEndLocation;
            output.Out.Write(RtsSave.IO.In.ReadBytes(RtsSave.IO.Length - RtsSave.Position));
  
            // align for encryption
            while ((output.Length % 8) != 0)
                output.Out.WriteByte(0x00);

            output.Stream.Flush();
            output.Out.SeekNWrite(0x04, (int)output.Length);

            _saveFileIO.Close();

            // reload save
            _saveFileIO = output;

            Read();

            // Flush save data
            // we write to package only after verifying the file data can be parsed correctly above
            FinishSave();
        }
        private void FinishSave()
        {
            if (IsDLCSave)
            {
                _saveFileIO.Out.SeekNWrite(0x04, (int)_saveFileIO.Length + 0x3C);

                IO.SeekTo(0x3C);
                IO.Out.Write(_saveFileIO.ToArray());

            }
            else
            {
                var ms = new MemoryStream();

                // encrypt the data
                _cryptek.Crypt(_saveFileIO.ToArray(), ms, true);

                // write data back to the package IO
                IO.Stream.SetLength(ms.Length);
                IO.SeekTo(0);
                IO.Out.Write(ms.ToArray());
            }
        }
        public void InjectDataBuffer(byte[] data)
        {
            var ms = new MemoryStream();
            _cryptek.Crypt(data, ms, true);

            if (!IO.Opened)
                IO.Open();

            IO.Stream.SetLength(ms.Length);
            IO.SeekTo(0);
            IO.Out.Write(ms.ToArray());
        }

        public MemoryStream ExtractDataBuffer()
        {
            return new MemoryStream(_saveFileIO.ToArray());
        }
        public void ExtractDataBufferToFile(string filename)
        {
            _saveFileIO.Stream.Save(filename);
        }
    }
    public class NSSkill
    {
        public uint Id;
        public ushort Experience;
    }
    public class NSSkills : List<NSSkill>
    {
        public bool NoIds;

        public ushort this[uint id]
        {
            get { return Find(skill => skill.Id == id).Experience; }
            set { Find(skill => skill.Id == id).Experience = value; }
        }

    }
    public class NSGlobal
    {
        public uint Id;
        public float Value;
    }
    public class NSGlobals : List<NSGlobal>
    {
        public float this[uint id]
        {
            get { return Find(global => global.Id == id).Value; }
            set { Find(global => global.Id == id).Value = value; }
        }
    }

    public class NSCharacter : NSObject
    {
        public bool IsModified;
        public byte ProgressAsFriend;
        public float Health;
        public float HealthDamage;
        public float Stamina;

        public NSInventory Inventory;
        public NSSkills Skills;
        public List<uint> Traits = new List<uint>();
 
        private readonly RTSSaveDataBuffer Save;

        public bool IsLoaded;
        public bool IsCommunityMember;

        public uint Id;
        public uint FirstName;
        public uint LastName;

        // Addresses
        public long StartAddress = -1;
        public long EndAddress = -1;
        public long SkillStartLocation = -1;
        public long SkillEndLocation = -1;
        public long InventoryStartLocation = -1;
        public long InventoryEndLocation = -1;
        public long ProgressAsFriendLocation = -1;
        public long TraitsStartLocation = -1;
        public long TraitsEndLocation = -1;
        public long HealthLocation = -1;
        public long StaminaEndLocation = -1;
        public long NameLocation = -1;
        public long IdentLocation = -1;

        public NSCharacter(RTSSaveDataBuffer save)
        {
            Save = save;
            ObjectType = NSObjectType.Character;
            IsLoaded = Load();
        }

        // 83425978
        public bool Load()
        {
            int v, f = 0;
            StartAddress = Save.Position;
            v = Save.ReadInt32();
            if (RTSSaveDataBuffer.IsValidSaveVersion(v, 0x43480000, 0xFFFF0000))
            {
                if (v >= 0x4348000B)
                {
                    f = v >= 0x4348000F ? Save.ReadInt32() : Save.ReadByte();
                    IsCommunityMember = ((f >> 3) & 1) != 0;
                    if ((f & 1) == 0)
                    {
                        EndAddress = Save.Position;
                        return false;
                    }
                }
                if (v >= 0x4348000A)
                {
                    IdentLocation = Save.Position;
                    Id = Save.ReadUInt32();
                }
                else
                {
                    Save.ReadInt16();
                }

                if (v >= 0x43480014)
                {
                    NameLocation = Save.Position;
                    FirstName = Save.ReadUInt32();
                    LastName = Save.ReadUInt32();
                }
                if (v >= 0x43480015)
                    Save.ReadByte();
                if (v < 0x4348000B)
                {
                    f = v >= 0x4348000F ? Save.ReadInt32() : Save.ReadByte();
                }
                if (v < 0x4348001D)
                {
                    Save.ReadByte();
                    Save.ReadByte();
                }

                ProgressAsFriendLocation = Save.Position;
                ProgressAsFriend = Save.ReadByte();

                if (v >= 0x43480010)
                    Save.RTSHandleLoad();
                if (v >= 0x43480004)
                    Save.ReadInt32();
                if (v >= 0x43480005)
                {
                    HealthLocation = Save.Position;
                    Health = Save.ReadFloat();
                    HealthDamage = Save.ReadFloat(); // health damage
                    Stamina = Save.ReadFloat();
                    StaminaEndLocation = Save.Position;
                    if (v < 0x4348001B)
                        Save.ReadInt32();
                }
                if (v >= 0x4348001A)
                    Save.ReadInt32();
                if (v >= 0x4348000C && v < 0x4348001C)
                    Save.ReadInt32();
                if (v >= 0x43480019)
                    Save.ReadByte();

                // load chracter traits
                TraitsStartLocation = Save.Position;
                var count = Save.ReadInt32(); // 83425E34
                for (var i = 0; i < count; i++)
                {
                    Traits.Add(Save.ReadUInt32());
                }
                TraitsEndLocation = Save.Position;
                if (v >= 0x43480012) // 83425F64
                {
                    // read tasks
                    Save.ReadInt32();
                    Save.ReadInt16();
                    var a = Save.ReadInt32();
                    if (a == 3)
                    {
                        Save.ReadInt32();
                        Save.ReadInt32();
                        Save.ReadInt32();
                    }
                    else
                    {
                        var ids = new List<uint>();
                        for (var i = 0; i < a; i++)
                        {
                            uint id = Save.ReadUInt32();
                            ids.Add(id);
                        }
                    }
                }
                if (v >= 0x43480016) // 8342604C
                {
                    Skills = new NSSkills();
                    SkillStartLocation = Save.Position;
                    if (v >= 0x43480017)
                    {
                        count = Save.ReadInt32();
                        for (var i = 0; i < count; i++)
                        {
                            Skills.Add(new NSSkill{Id = Save.ReadUInt32(), Experience = Save.ReadUInt16()});
                        }
                    }
                    else
                    {
                        Skills.NoIds = true; // do not select by ids
                        count = Save.ReadInt32();
                        for (var i = 0; i < count; i++)
                        {
                            Skills.Add(new NSSkill { Id = 0, Experience = Save.ReadUInt16() });
                        }
                    }
                    SkillEndLocation = Save.Position;
                }
                if (v >= 0x43480007)
                {
                    // read inventory
                    if (v < 0x43480013)
                    {
                        if ((f & 0x20) != 0)
                            Save.ReadByte();
                        if ((f & 0x40) != 0)
                        {
                            Save.ReadByte();
                            Save.ReadInt16();
                            Save.ReadInt16();
                        }
                        if ((f & 0x80) != 0)
                        {
                            Save.ReadByte();
                        }
                        if (v >= 0x4348000E)
                        {

                        }
                    }
                    else
                    {
                        var vt = Save.ReadInt32();
                        if (RTSSaveDataBuffer.IsValidSaveVersion(vt, 0x43484900, 0xFFFFFF00))
                        {
                            // fill in jump @ 83422390
                            // jumps to 834C1BF0
                            InventoryStartLocation = Save.Position;
                            Inventory = LoadInventory(Save);
                            InventoryEndLocation = Save.Position;
                            Save.ReadInt32(); // equipped backpack
                            Save.ReadByte();
                            Save.ReadByte();
                        }
                    }
                }
                if (v >= 0x43480009)
                {
                    if (v >= 0x4348000D)
                    {
                        Save.RTSHandleLoad();
                    }
                    else
                    {
                        Save.ReadByte();
                    }
                }
                if (v >= 0x43480011)
                {
                    // 8338AE30
                    {
                        var vt = Save.ReadInt32();
                        if (RTSSaveDataBuffer.IsValidSaveVersion(vt, 0x43490000, 0xFFFF0000))
                        {
                            Save.ReadInt16();
                            Save.ReadInt16();
                        }
                    }
                }
                else
                {
                    if (v >= 0x4348000B)
                    {
                        Save.ReadInt16();
                    }
                }
                if (v >= 0x43480018)
                {
                    Save.ReadInt32();
                    Save.ReadInt32();
                }
            }
            EndAddress = Save.Position;
            return true;
        }

        // 834C1BF0
        public static NSInventory LoadInventory(RTSSaveDataBuffer save)
        {
            int vt, count;
            // save.Read Inventory
            vt = save.ReadInt32();
            if (RTSSaveDataBuffer.IsValidSaveVersion(vt, 0x49560000, 0xFFFF0000))
            {
                // save.Read item IDs
                count = save.ReadInt32();
                var inventory = new NSInventory { MaxCount = count};

                var invItemIds = new List<uint>();
                for (var i = 0; i < count; i++)
                {
                    invItemIds.Add((uint)save.ReadInt32());
                }
                // save.Read item structures
                for (var i = 0; i < count; i++)
                {
                    var type = (NSInventoryItemType)save.ReadByte();
                    switch (type)
                    {
                        case NSInventoryItemType.Empty:
                            inventory.Add(new NSInventoryItem());
                            break;
                        // same basic types, no need for two different structures
                        case NSInventoryItemType.Projectile:
                        case NSInventoryItemType.Consumable:
                            {
                                inventory.Add(new NSConsumable { Id = invItemIds[i], Value = save.ReadByte(), Type = type});
                            }
                            break;
                        case NSInventoryItemType.FireArm: // fire arms
                            {
                                var fireArm = new NSFireArm { Id = invItemIds[i], Type = type};
                                if (vt >= 0x49560003)
                                {
                                    if (vt >= 0x49560005)
                                    {
                                        fireArm.DurabilityCritical = (ushort)save.ReadBytesInternal(2).ReadInt16(0);
                                        fireArm.Durability = (ushort)save.ReadBytesInternal(2).ReadInt16(0);
                                    }
                                    else
                                    {
                                        var fA = save.ReadBytesInternal(4);
                                        fireArm.DurabilityCritical = (ushort) fA.ReadInt16(0);
                                            // critical shots before damage
                                        fireArm.Durability = (ushort) fA.ReadInt16(2); // shots before damage
                                    }
                                }
                                fireArm.Clip = save.ReadByte(); // current clip
                                fireArm.SilencerShots = save.ReadByte();
                                inventory.Add(fireArm);
                            }
                            break;
                        case NSInventoryItemType.MeleeWeapon: // melee weapons
                            if (vt >= 0x49560002)
                            {
                                var meleeWeapon = new NSMeeleWeapon { Id = invItemIds[i], Type = type};
                                if (vt >= 0x49560003)
                                {
                                    if (vt >= 0x49560005)
                                    {
                                        meleeWeapon.Durability = (ushort) save.ReadBytesInternal(2).ReadInt16(0);
                                        meleeWeapon.DurabilityMax = (ushort) save.ReadBytesInternal(2).ReadInt16(0);
                                    }
                                    else
                                    {
                                        var mW = save.ReadBytesInternal(4);
                                        meleeWeapon.Durability = (ushort) mW.ReadInt16(0);
                                        meleeWeapon.DurabilityMax = (ushort) mW.ReadInt16(2);
                                    }
                                }
                                else
                                {
                                    meleeWeapon.Durability = save.ReadByte();
                                    meleeWeapon.DurabilityMax = save.ReadByte();
                                }
                                inventory.Add(meleeWeapon);
                            }
                        break;
                        // no values to read
                        case NSInventoryItemType.Backpack:
                        {
                            inventory.Add(new NSInventoryItem { Id = invItemIds[i], Type = type});
                        }
                        break;
                    case NSInventoryItemType.Vehicle:
                            {
                                save.ReadByte();
                            }
                        break;
                    }
                }
                return inventory;
            }
            return null;
        }

        public static void WriteInventory(RTSSaveDataBuffer save, int version, NSInventory inventory)
        {
            if (RTSSaveDataBuffer.IsValidSaveVersion(version, 0x49560000, 0xFFFF0000))
            {
                save.WriteInt32(inventory.Count);
                foreach (var item in inventory)
                {
                    save.WriteUInt32(item.Id);
                }
                foreach (var item in inventory)
                {
                    save.WriteByte((byte)item.Type);
                    switch (item.Type)
                    {
                        case NSInventoryItemType.Consumable:
                            var consumable = item as NSConsumable;
                            save.WriteByte(consumable.Value);
                            break;
                        case NSInventoryItemType.Projectile:
                            var projectile = item as NSConsumable;
                            save.WriteByte(projectile.Value);
                            break;
                        case NSInventoryItemType.FireArm:
                            var gun = item as NSFireArm;
                            var gunData = new byte[4];
                            if (version >= 0x49560003)
                            {
                                if (version >= 0x49560005)
                                {
                                    save.WriteUInt16(gun.Durability);
                                    save.WriteUInt16(gun.DurabilityCritical);
                                }
                                else
                                {
                                    gunData.WriteInt16(0, gun.DurabilityCritical);
                                    gunData.WriteInt16(2, gun.Durability);
                                    save.WriteBytesInternal(gunData);
                                }
                            }
                            save.WriteByte(gun.Clip);
                            save.WriteByte(gun.SilencerShots);
                            break;
                        case NSInventoryItemType.MeleeWeapon:
                            var weapon = item as NSMeeleWeapon;
                            var weaponData = new byte[4];
                            if (version >= 0x49560003)
                            {
                                if (version >= 0x49560005)
                                {
                                    save.WriteUInt16(weapon.Durability);
                                    save.WriteUInt16(weapon.DurabilityMax);
                                }
                                else
                                {
                                    weaponData.WriteInt16(0, weapon.Durability);
                                    weaponData.WriteInt16(2, weapon.DurabilityMax);
                                    save.WriteBytesInternal(weaponData);
                                }
                            }
                            else
                            {
                                save.WriteByte((byte)weapon.Durability);
                                save.WriteByte((byte)weapon.DurabilityMax);
                            }
                            break;
                    }
                }
            }
        }

        public byte[] Write()
        {
            var io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            var rts = new RTSSaveDataBuffer(io, Save.Version);

            Save.Position = StartAddress;
            if(IsLoaded && IsCommunityMember)
            {
                io.Out.Write(Save.IO.In.ReadBytes(IdentLocation - Save.Position));
                // write character model id
                rts.WriteUInt32(Id);
                Save.Position = IdentLocation + 5;
                io.Out.Write(Save.IO.In.ReadBytes(NameLocation - Save.Position));
                // save first and last name
                rts.WriteUInt32(FirstName);
                rts.WriteUInt32(LastName);
                Save.Position = NameLocation += 0x0A;
                io.Out.Write(Save.IO.In.ReadBytes(ProgressAsFriendLocation - Save.Position));
                // write friend progress
                rts.WriteByte(ProgressAsFriend);
                Save.Position = ProgressAsFriendLocation + 2;
                io.Out.Write(Save.IO.In.ReadBytes(HealthLocation - Save.Position));
                rts.WriteSingle(Health);
                rts.WriteSingle(HealthDamage);
                rts.WriteSingle(Stamina);
                Save.Position = StaminaEndLocation;
                io.Out.Write(Save.IO.In.ReadBytes(TraitsStartLocation - Save.Position));
                // write traits list
                rts.WriteInt32(Traits.Count);
                foreach (uint trait in Traits)
                {
                    rts.WriteUInt32(trait);
                }
                Save.Position = TraitsEndLocation;
                io.Out.Write(Save.IO.In.ReadBytes(SkillStartLocation - Save.Position));
                // write the skills
                rts.WriteInt32(Skills.Count);
                foreach (var skill in Skills)
                {
                    rts.WriteUInt32(skill.Id);
                    rts.WriteUInt16(skill.Experience);
                }
                Save.Position = SkillEndLocation;
                io.Out.Write(Save.IO.In.ReadBytes(InventoryStartLocation - Save.Position));
                // write the inventory
                Save.Position = InventoryStartLocation;
                var version = Save.ReadInt32();
                rts.WriteInt32(version);
                WriteInventory(rts, version, Inventory);
                Save.Position = InventoryEndLocation;
            }
            io.Out.Write(Save.IO.In.ReadBytes(EndAddress - Save.Position));
            return io.ToArray();
        }
    }

    public class NSFireArm : NSInventoryItem
    {
        public ushort DurabilityCritical; // remaining number of critical shots to be fired before weapon is damaged
        public ushort Durability;  // remaining number of shots to be fired before weapon is damaged
        public byte Clip; // ammo in the magazine
        public byte SilencerShots; // remaining silenced shots

        public NSFireArm()
        {
            Type = NSInventoryItemType.FireArm;
        }
    }

    public class NSMeeleWeapon : NSInventoryItem
    {
        public ushort DurabilityMax; // total number of hits before weapon is damaged
        public ushort Durability; // remaining number of hits before weapon is damaged

        public NSMeeleWeapon()
        {
            Type = NSInventoryItemType.MeleeWeapon;
        }
    }

    public class NSConsumable : NSInventoryItem
    {
        public byte Value;
        public NSConsumable()
        {
            Type = NSInventoryItemType.Consumable;
        }
    }

    public class NSProjectile : NSInventoryItem
    {
        public byte Value;
        public NSProjectile()
        {
            Type = NSInventoryItemType.Projectile;
        }
    }

    public class NSInventoryItem : NSObject
    {
        public NSInventoryItemType Type
        {
            get { return (NSInventoryItemType) ObjectType; }
            set { ObjectType = (NSObjectType)value; }
        }
        public uint Id;
    }

    public class NSInventory : List<NSInventoryItem>
    {
        public bool Modified;
        public int MaxCount;
        public int AddNewItem(NSInventoryItem item)
        {
            if (FindAll(t => t.Type == NSInventoryItemType.Empty).Count <= 0)
                return -2;

            var index = FindIndex(t => t.Type == NSInventoryItemType.Empty);
            if (index != -1)
            {
                this[index] = item;
            }
            else
            {
                Add(item);
            }
            Modified = true;
            return index;
        }
    }
    public class NSObject
    {
        public NSObjectType ObjectType;
    }

    public enum NSObjectType
    {
        Empty,
        Projectile,
        Backpack,
        Consumable,
        FireArm,
        MeleeWeapon,
        Character,
        Enclave
    }
    public enum NSInventoryItemType
    {
        Empty,
        Projectile,
        Backpack,
        Consumable,
        FireArm,
        MeleeWeapon,
        Vehicle
    }

    public class NSCommunity : NSObject
    {
        private readonly RTSSaveDataBuffer Save;

        public uint Id;

        public NSInventory Inventory;

        public long StartAddress = -1;
        public long EndAddress = -1;
        public long InventoryStartAddress = -1;
        public long InventoryEndAddress = -1;

        public NSCommunity(RTSSaveDataBuffer save)
        {
            Save = save;
            ObjectType = NSObjectType.Enclave;
        }

        public void LoadEnclave()
        {
            int f, count;
            StartAddress = Save.Position;
            var v = Save.ReadInt32();
            if (!RTSSaveDataBuffer.IsValidSaveVersion(v, 0x454E0000, 0xFFFF0000)) return;
            if (v < 0x454E000C)
            {
                f = Save.ReadInt32();
                if (f > -1)
                    f = 8;
            }
            else
            {
                f = Save.ReadByte();
            }
            if ((f & 0x08) == 0)
            {
                EndAddress = Save.Position;
                return;
            }
            Save.ReadInt32();
            Save.ReadInt32();
            Save.ReadInt32();
            Save.ReadInt32();
            Save.ReadInt32();
            if (v >= 0x454E0002)
            {
                Save.ReadInt32();
                if (v < 0x454E0004)
                    Save.ReadByte();
            }
            if (v >= 0x454E0003 && v < 0x454E000C)
                Save.ReadByte();
            if (v >= 0x454E0005)
            {
                Id = Save.ReadUInt32();
                if (v >= 0x454E000C)
                {
                    Save.ReadInt32();
                    if (v >= 0x454E000D)
                    {
                        Save.ReadInt32();
                    }
                }
                else
                {
                    Save.ReadInt16();
                }
                if (v < 0x454E000C)
                    Save.ReadInt32();
            }
            if (v >= 0x454E0006 && v < 0x454E000C)
            {
                for (int i = 0; i < 8; i++)
                {
                    Save.ReadFloat();
                }
                Save.ReadInt16();
                Save.ReadInt16();
            }
            if (v >= 0x454E0014)
            {
                count = Save.ReadInt32();
                for (var i = 0; i < count; i++)
                {
                    var a = Save.ReadByte();
                    var b = Save.ReadByte();
                    if (a == 0 && b == 0)
                        continue;
                    Save.ReadByte();
                    Save.ReadInt32();
                    Save.ReadInt32();
                    Save.ReadInt32();
                    Save.ReadInt32();
                    // jump to 8329DC70
                    if(a != 0)
                    {
                        var vt = Save.ReadInt32();
                        if (RTSSaveDataBuffer.IsValidSaveVersion(vt, 0x4D490000, 0xFFFF0000))
                        {
                            if (Save.ReadInt32() != 0)
                            {
                                Save.ReadInt32();
                                Save.RTSHandleLoad();
                                Save.ReadInt32();
                                Save.ReadInt32();
                                Save.ReadInt32();
                                Save.ReadInt32();
                                Save.ReadInt32();
                                Save.ReadInt32();
                                a = Save.ReadByte();
                                for (var j = 0; j < a; j++)
                                {
                                    Save.RTSHandleLoad();
                                }
                                Save.ReadByte();
                                if (vt >= 0x4D490002)
                                    Save.ReadByte();
                                continue;
                            }
                        }
                        else
                        {
                            Save.ReadInt32();
                        }
                        // 83298990
                        {
                            Save.ReadInt32();
                            Save.RTSHandleLoad();
                            Save.ReadInt32();
                            Save.ReadInt32();
                            Save.ReadInt32();
                            Save.ReadInt32();
                            Save.ReadInt32();
                            Save.ReadInt32();
                            a = Save.ReadByte();
                            for (var j = 0; j < a; j++)
                            {
                                Save.RTSHandleLoad();
                            }
                            Save.ReadByte();
                            Save.ReadByte();
                        }
                    }
                }
            }
            else
            {
                if (v >= 0x454E0007)
                {
                    count = Save.ReadInt32();
                    /*
                    for (var i = 0; i < count; i++)
                    {

                    }
                    */
                }
            }
            if (v >= 0x454E0009)
            {
                Save.ReadInt16();
            }
            if (v >= 0x454E000F)
            {
                // Save.Read house stockpile inventory
                InventoryStartAddress = Save.Position;
                Inventory = NSCharacter.LoadInventory(Save);
                InventoryEndAddress = Save.Position;
            }
            if (v >= 0x454E0011)
                Save.ReadByte();
            if (v >= 0x454E0013)
            {
                Save.ReadByte();
                Save.ReadByte();
            }
            EndAddress = Save.Position;
        }

        public static void LoadCSearchParty(RTSSaveDataBuffer Save)
        {
            var v = Save.ReadInt32();
            if (RTSSaveDataBuffer.IsValidSaveVersion(v, 0x53500000, 0xFFFF0000))
            {
                if (Save.ReadByte() != 0x00)
                {
                    // 8338EC40
                    {
                        var vt = Save.ReadInt32();
                        if (RTSSaveDataBuffer.IsValidSaveVersion(vt, 0x4E470000, 0xFFFF0000))
                        {
                            var count = Save.ReadByte();
                            if (vt >= 0x4E470002)
                            {
                                Save.ReadByte();
                            }
                            for (var i = 0; i < count; i++)
                            {
                                // 8338AE30
                                {
                                    vt = Save.ReadInt32();
                                    if (RTSSaveDataBuffer.IsValidSaveVersion(vt, 0x43490000, 0xFFFF0000))
                                    {
                                        Save.ReadInt16();
                                        Save.ReadInt16();
                                    }
                                }
                            }
                            Save.ReadByte();
                        }
                    }
                    if (v >= 0x53500003)
                    {
                        Save.RTSHandleLoad();
                    }
                    var a = Save.ReadByte();
                    Save.ReadByte();
                    if (a == 2)
                    {
                        Save.ReadFloat();
                        Save.ReadByte();
                        Save.ReadInt32();
                        if (v >= 0x53500002)
                            Save.ReadInt32();

                        Save.ReadByte();
                        if (v >= 0x53500002 || v >= 0x53500005)
                        {
                            Save.ReadInt32();
                            Save.ReadInt32();
                            Save.ReadInt32();
                        }
                        if (v >= 0x53500004)
                        {
                            Save.ReadInt32();
                            Save.ReadByte();
                        }
                    }
                }
            }
        }

        public byte[] Write()
        {
            var io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            var rts = new RTSSaveDataBuffer(io, Save.Version);
            Save.Position = StartAddress;
            if(Inventory != null)
            {
                io.Out.Write(Save.IO.In.ReadBytes(InventoryStartAddress - Save.Position));

                Save.Position = InventoryStartAddress;
                var version = Save.ReadInt32();
                rts.WriteInt32(version);

                NSCharacter.WriteInventory(rts, version, Inventory);

                Save.Position = InventoryEndAddress;
            }
            io.Out.Write(Save.IO.In.ReadBytes(EndAddress - Save.Position));
            return io.ToArray();
        }
    }

    public class RTSSaveDataBuffer
    {
        private const string ERROR_DF_SIZE = "Data being loaded is a different size from what was saved, {0:D8} != {1:D8}";

        public readonly EndianIO IO;
        internal readonly uint Version;
        public List<NSCharacter> Characters;
        public long Position
        {
            get { return IO.Position; }
            set { IO.Position = value; }
        }

        public RTSSaveDataBuffer(EndianIO io, uint version)
        {
            IO = io;

            Version = version; // determines how we handle save data
        }

        public void RTSHandleLoad()
        {
            var v = ReadInt32();
            if (IsValidSaveVersion(v, 0x52480000, 0xFFFF0000))
            {
                var type = ReadByte();
                if (v >= 0x52480003)
                {
                    if(type != 0x0D && type != 8)
                    {
                        ReadInt16();
                        ReadInt16();
                    }
                    else
                    {
                        ReadInt32();
                    }
                }
            }
        }

        public void NSCommunityEnclaveLoad()
        {
            int f, count;
            var v = ReadInt32();
            if (!IsValidSaveVersion(v, 0x454E0000, 0xFFFF0000)) return;
            if (v < 0x454E000C)
            {
                f = ReadInt32();
                if (f > -1)
                    f = 8;
            }
            else
            {
                f = ReadByte();
            }
            if((f & 0x08) == 0) return;
            ReadInt32();
            ReadInt32();
            ReadInt32();
            ReadInt32();
            ReadInt32();
            if (v >= 0x454E0002)
            {
                ReadInt32();
                if (v < 0x454E0004)
                    ReadByte();
            }
            if (v >= 0x454E0003 && v < 0x454E000C)
                ReadByte();
            if (v >= 0x454E0005)
            {
                if (v >= 0x454E000A)
                {
                    ReadInt32();
                }
                else
                {
                    ReadInt32();
                }
                if (v >= 0x454E000C)
                {
                    ReadInt32();
                    if (v >= 0x454E000D)
                    {
                        ReadInt32();
                    }
                }
                else
                {
                    ReadInt16();
                }
                if (v < 0x454E000C)
                ReadInt32();
            }
            if (v >= 0x454E0006 && v < 0x454E000C)
            {
                for (int i = 0; i < 8; i++)
                {
                    ReadFloat();
                }
                ReadInt16();
                ReadInt16();
            }
            if (v >= 0x454E0014)
            {
                count = ReadInt32();
                for (var i = 0; i < count; i++)
                {
                    var a = ReadByte();
                    var b = ReadByte();
                    if (a == 0 && b == 0)
                        continue;
                    ReadByte();
                    ReadInt32();
                    ReadInt32();
                    ReadInt32();
                    ReadInt32();
                    // jump to 8329DC70
                    {
                        var vt = ReadInt32();
                        if (IsValidSaveVersion(vt, 0x4D490000, 0xFFFF0000))
                        {
                            if (ReadInt32() != 0)
                            {
                                ReadInt32();
                                RTSHandleLoad();
                                ReadInt32();
                                ReadInt32();
                                ReadInt32();
                                ReadInt32();
                                ReadInt32();
                                ReadInt32();
                                a = ReadByte();
                                for (var j = 0; j < a; j++)
                                {
                                    RTSHandleLoad();
                                }
                                ReadByte();
                                if (vt >= 0x4D490002)
                                    ReadByte();
                                continue;
                            }
                        }
                        else
                        {
                            ReadInt32();
                        }
                        // 83298990
                        {
                            ReadInt32();
                            RTSHandleLoad();
                            ReadInt32();
                            ReadInt32();
                            ReadInt32();
                            ReadInt32();
                            ReadInt32();
                            ReadInt32();
                            a = ReadByte();
                            for (var j = 0; j < a; j++)
                            {
                                RTSHandleLoad();
                            }
                            ReadByte();
                            ReadByte();
                        }
                    }
                }
            }
            else
            {
                if (v >= 0x454E0007)
                {
                    count = ReadInt32();
                    for (var i = 0; i < count; i++)
                    {
                        
                    }
                }
            }
            if (v >= 0x454E0009)
            {
                ReadInt16();
            }
            if (v >= 0x454E000F)
            {
                // read house stockpile inventory
                NSCharacter.LoadInventory(this);
            };
            if (v >= 0x454E0011)
                ReadByte();
            if (v >= 0x454E0013)
            {
                ReadByte();
                ReadByte();
            }
        }

        public void WriteByte(byte value)
        {
            IO.Out.Write(WConvert8(value));
            IO.Out.WriteByte(WConvert8(1));
        }

        public void WriteInt32(int value)
        {
            WriteUInt32((uint) value);
        }
        public void WriteUInt32(uint value)
        {
            IO.Out.Write(WConvert32(value));
            IO.Out.WriteByte(WConvert8(4));
        }
        public void WriteUInt16(ushort value)
        {
            WriteInt16((short) value);
        }
        public void WriteInt16(short value)
        {
            IO.Out.Write(WConvert16(value));
            IO.Out.WriteByte(WConvert8(2));
        }
        public void WriteSingle(float value)
        {
            WriteInt32(BitConverter.ToInt32(BitConverter.GetBytes(value), 0));
            //IO.Out.Write(value);
            //IO.Out.WriteByte(WConvert8(4));
        }

        public byte[] ConvertArray(byte[] value)
        {
            if (Version < 0x53560005)
                return value;

            int count = value.Length, i = 0;
            if (count >= 4)
            {
                for (i = 0; i < count; i+=4)
                {
                    var val32 = Convert32(value.ReadUInt32(i));
                    value.WriteInt32(i, (int)val32);
                }
            }
            count -= i;
            if (count >= 2)
            {
                for (i = 0; i < count; i += 2)
                {
                    var val16 = Convert16(value.ReadInt16(i));
                    value.WriteInt16(i, val16);
                } 
            }
            count -= i;
            if (count >= 1)
            {
                for (i = 0; i < count; i++)
                {
                    var val8 = Convert8(value[i]);
                    value[i*4] = val8;
                } 
            }
            return value;
        }

        internal byte[] WConvertArray(byte[] value)
        {
            if (Version < 0x53560005)
                return value;

            int count = value.Length, i = 0;
            if (count >= 4)
            {
                for (i = 0; i < count; i += 4)
                {
                    var val32 = WConvert32(value.ReadUInt32(i));
                    value.WriteInt32(i, (int)val32);
                }
            }
            count -= i;
            if (count >= 2)
            {
                for (i = 0; i < count; i += 2)
                {
                    var val16 = WConvert16(value.ReadInt16(i));
                    value.WriteInt16(i, val16);
                }
            }
            count -= i;
            if (count >= 1)
            {
                for (i = 0; i < count; i++)
                {
                    var val8 = WConvert8(value[i]);
                    value[i * 4] = val8;
                }
            }
            return value;
        }

        public uint Convert32(uint value)
        {
            if (Version < 0x53560005)
                return value;

            const uint c1 = 0xE2222223, c2 = 0xC4444447, c3 = 0x99999999, c4 = 0xFC0C0C0F, c5 = 0xF030303F, c6 = 0xC3C3C3C3;
            uint r10, r9, r11 = value;

            r10 = (r11 >> 1) & 0x3FFFFFFE;
            r9 = (r11 << 1) & 0x7FFFFFFC;
            r10 &= c1;
            r9 &= c2;
            r10 |= r9;
            r11 &= c3;
            r11 |= r10;
            r10 = (r11 >> 2) & 0xFFFFFFC;
            r9 = (r11 << 2) & 0x3FFFFFF0;
            r10 &= c4;
            r9 &= c5;
            r10 |= r9;
            r11 &= c6;
            r11 |= r10;
            r10 = (r11 >> 4) & 0xFFFFF0;
            r9 = (r11 << 4) & 0xFFFFF00;
            r11 = r11 & 0xFFFFF00F;
            r10 = r10 & 0xFFF000FF;
            r9 = r9 & 0xFF000FFF;
            r11 = r11 & 0xF00FFFFF;
            r10 |= r9;
            r11 |= r10;
            r10 = (r11 >> 8) & 0xFF00;
            r9 = (r11 << 8) & 0xFF0000;
            r11 = r11 & 0xFF0000FF;
            r10 |= r9;
            r11 |= r10;

            uint T0, T1, T2 = value;
            T0 = (T2 >> 1) & 0x3FFFFFFE;
            T1 = (T2 << 1) & 0x7FFFFFFC;
            T0 &= c1;
            T1 &= c2;
            T0 |= T1;
            T2 &= c3;
            T2 |= T0;
            T0 = (T2 >> 2) & 0xFFFFFFC;
            T1 = (T2 << 2) & 0x3FFFFFF0;
            T0 &= c4;
            T1 &= c5;
            T0 |= T1;
            T2 &= c6;
            T2 |= T0;
            T0 = (T2 >> 4) & 0xFFFFF0;
            T1 = (T2 << 4) & 0xFFFFF00;
            T2 = T2 & 0xFFFFF00F;
            T0 = T0 & 0xFFF000FF;
            T1 = T1 & 0xFF000FFF;
            T2 = T2 & 0xF00FFFFF;
            T0 |= T1;
            T2 |= T0;

            T2 = (T2 & 0xFF0000FF) | (((T2 >> 8) & 0xFF00) | ((T2 << 8) & 0xFF0000));

            if (T2 != r11)
                throw new Exception();

            return (r11);
        }
        public static uint StaticConvert32(uint value)
        {
            const uint c1 = 0xE2222223, c2 = 0xC4444447, c3 = 0x99999999, c4 = 0xFC0C0C0F, c5 = 0xF030303F, c6 = 0xC3C3C3C3;
            uint r10, r9, r11 = value;

            r10 = (r11 >> 1) & 0x3FFFFFFE;
            r9 = (r11 << 1) & 0x7FFFFFFC;
            r10 &= c1;
            r9 &= c2;
            r10 |= r9;
            r11 &= c3;
            r11 |= r10;
            r10 = (r11 >> 2) & 0xFFFFFFC;
            r9 = (r11 << 2) & 0x3FFFFFF0;
            r10 &= c4;
            r9 &= c5;
            r10 |= r9;
            r11 &= c6;
            r11 |= r10;
            r10 = (r11 >> 4) & 0xFFFFF0;
            r9 = (r11 << 4) & 0xFFFFF00;
            r11 = r11 & 0xFFFFF00F;
            r10 = r10 & 0xFFF000FF;
            r9 = r9 & 0xFF000FFF;
            r11 = r11 & 0xF00FFFFF;
            r10 |= r9;
            r11 |= r10;
            r10 = (r11 >> 8) & 0xFF00;
            r9 = (r11 << 8) & 0xFF0000;
            r11 = r11 & 0xFF0000FF;
            r10 |= r9;

            return (r11 | r10);
        }

        public static uint RtsCipherObfuscate(uint input, uint flags)
        {
            uint T0, T1;

            if ((flags & 0x01) != 0)
            {
                T0 = ((input & 0xD5555555) << 1);
                T1 = (input >> 1) & 0xD5555555;
                input = T0 | T1;
            }
            if ((flags & 0x02) != 0)
            {
                T0 = ((input & 0xF3333333) << 2);
                T1 = (input >> 2) & 0xF3333333;
                input = T0 | T1;
            }
            if ((flags & 0x04) != 0)
            {
                T0 = (input & 0xFF0F0F0F) << 4;
                T1 = (input >> 4) & 0xFF0F0F0F;
                input = (T0 | T1);
            }
            if ((flags & 0x08) != 0)
            {
                T0 = (input & 0xFFFF00FF) << 8;
                T1 = (input >> 8) & 0xFFFF00FF;
                input = T0 | T1;
            }
            if ((flags & 0x10) != 0)
            {
                input = input.RotateRight(16);
            }

            return input;
        }

        internal uint WConvert32(uint value)
        {
            if (Version < 0x53560005)
                return value;

            const uint c1 = 0xE2222223, c2 = 0xC4444447, c3 = 0x99999999, c4 = 0xFC0C0C0F, c5 = 0xF030303F, c6 = 0xC3C3C3C3;
            uint r10, r9, r11 = value;

            r9 = (r11 << 8) & 0xFF0000;
            r10 = (r11 >> 8) & 0xFF00;
            r11 = r11 & 0xFF0000FF;
            r10 |= r9;
            r11 |= r10;
            r10 = (r11 >> 4) & 0xFFFFF0;
            r9 = (r11 << 4) & 0xFFFFF00;
            r10 = r10 & 0xFFF000FF;
            r11 = r11 & 0xFFFFF00F;
            r9 = r9 & 0xFF000FFF;
            r11 = r11 & 0xF00FFFFF;
            r10 |= r9;
            r11 |= r10;
            r10 = (r11 >> 2) & 0xFFFFFFC;
            r9 = (r11 << 2) & 0x3FFFFFF0;
            r10 &= c4;
            r9 &= c5;
            r10 |= r9;
            r11 &= c6;
            r11 |= r10;
            r10 = (r11 >> 1) & 0x3FFFFFFE;
            r9 = (r11 << 1) & 0x7FFFFFFC;

            r10 &= c1;
            r9 &= c2;
            r10 |= r9;
            r11 &= c3;
            r11 |= r10;

            uint T0, T1, T2 = value;

            T1 = (T2 << 8) & 0xFF0000;
            T0 = (T2 >> 8) & 0xFF00;
            T2 = T2 & 0xFF0000FF;
            T0 |= T1;
            T2 |= T0;
            T0 = (T2 >> 4) & 0xFFFFF0;
            T1 = (T2 << 4) & 0xFFFFF00;
            T0 = T0 & 0xFFF000FF;
            T2 = T2 & 0xFFFFF00F;
            T1 = T1 & 0xFF000FFF;
            T2 = T2 & 0xF00FFFFF;
            T0 |= T1;
            T2 |= T0;
            T0 = (T2 >> 2) & 0xFFFFFFC;
            T1 = (T2 << 2) & 0x3FFFFFF0;
            T0 &= c4;
            T1 &= c5;
            T0 |= T1;
            T2 &= c6;
            T2 |= T0;
            T0 = (T2 >> 1) & 0x3FFFFFFE;
            T1 = (T2 << 1) & 0x7FFFFFFC;

            T2 = (T2 & c3) | ((T0 & c1) | (T1 & c2));

            if(T2 != r11)
                throw new Exception();

            return (r11);
        }

        public short Convert16(int value)
        {
            if (Version < 0x53560005)
                return (short) value;

            int T4 = value;

            T4 = (T4 & 0x9999) | ((((T4 >> 1) & 0x3FFE) & -0x1DDD) | (((T4 << 1) & 0x7FFC) & -0x3BB9));
            T4 = (T4 & 0xC3C3) | ((((T4 >> 2) & 0xFFC) & -1009) | (((T4 << 2) & 0x3FF0) & -4033));
            T4 = (T4 & 0xF00F) | (((T4 >> 4) & 0xF0) | ((T4 << 4) & 0xF00));

            return (short)T4;
        }

        internal short WConvert16(int value)
        {
            if (Version < 0x53560005)
                return (short)value;

            int T2 = value;

            T2 = (T2 & 0xF00F) | (((T2 >> 4) & 0xF0) | ((T2 << 4) & 0xF00));
            T2 = (T2 & 0xC3C3) | ((((T2 >> 2) & 0xFFC) & -0x3f1) | (((T2 << 2) & 0x3FF0) & -0xFC1));
            T2 = (T2 & 0x9999) | ((((T2 >> 1) & 0x3FFE) & -0x1DDD) | (((T2 << 1) & 0x7FFC) & -0x3BB9));

            return (short)T2;
        }

        public byte Convert8(byte value)
        {
            if (Version < 0x53560005)
                return value;

            uint T2 = value;

            T2 = (T2 & 0x99) | ((((T2 >> 1) & 0x3E) & 0xFFFFFFE3) | (((T2 << 1) & 0x7C) & 0xFFFFFFC7));
            T2 = ((T2 & 0xC3) | (((T2 >> 2) & 0xC) | ((T2 << 2) & 0x30)));

            return (byte)T2;
        }

        internal byte WConvert8(byte value)
        {
            if (Version < 0x53560005)
                return value;

            uint T2 = value;

            T2 = (T2 & 0xC3) | (((T2 >> 2) & 0xC) | ((T2 << 2) & 0x30));
            T2 = (T2 & 0x99) | ((((T2 >> 1) & 0x3E) & 0xFFFFFFE3) | (((T2 << 1) & 0x7C) & 0xFFFFFFC7));

            return (byte)T2;
        }

        public byte ReadByte()
        {
            var val = Convert8(IO.In.ReadByte());
            VerifyLength(1);
            return val;
        }

        public short ReadInt16()
        {
            var val = Convert16(IO.In.ReadInt16());
            VerifyLength(2);
            return val;
        }

        public ushort ReadUInt16()
        {
            var val = Convert16(IO.In.ReadUInt16());
            VerifyLength(2);
            return (ushort)val;
        }

        public int ReadInt32(bool verifyLength)
        {
            var val = Convert32(IO.In.ReadUInt32());
            if(verifyLength)
                VerifyLength(4);
            return (int)val;
        }

        public int PeekInt32()
        {
            var val = Convert32(IO.In.ReadUInt32());
            IO.Position -= 4;
            return (int)val;
        }

        public int ReadInt32()
        {
            return ReadInt32(true);
        }

        public long ReadInt64()
        {
            var val = ConvertArray(IO.In.ReadBytes(8)).ReadInt64(0);
            VerifyLength(8);
            return val;
        }

        public uint ReadUInt32()
        {
            var val = Convert32(IO.In.ReadUInt32());
            VerifyLength(4);
            return val;
        }

        public float ReadFloat()
        {
            var val = BitConverter.ToSingle(BitConverter.GetBytes(Convert32(IO.In.ReadUInt32())), 0);
            VerifyLength(4);
            return val;
        }

        private void VerifyLength(int count)
        {
            var len = Convert8(IO.In.ReadByte());
            if(count != len)
                throw new Exception(string.Format(ERROR_DF_SIZE, count, len));
        }
        public void WriteBytesInternal(byte[] value)
        {
            IO.Out.Write(WConvertArray(value));
            IO.Out.WriteByte(WConvert8((byte)value.Length));
        }
        public byte[] ReadBytesInternal(int count)
        {
            var data = ConvertArray(IO.In.ReadBytes(count));
            var len = Convert8(IO.In.ReadByte());
            if (len != count)
                throw new Exception(string.Format("Data being loaded is a different size from what was saved, {0:D8} != {1:D8}", count, len));

            return data;
        }

        public static bool IsValidSaveVersion(int version, int cmpVersion, uint mask)
        {
            return cmpVersion == (version & mask);
        }
    }
}