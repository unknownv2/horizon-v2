using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO.Compression;
using System.IO;
using System.Data.SQLite;
using System.Data;

namespace ForzaMotorsport
{
    public enum ForzaTypes
    {
        PropertyBag = 0x0F,
        Float32 = 0x09,
        CarId = 0x07,
        UInt64 = 0x04,
        UInt32 = 0x03,
        Uint16 = 0x02,
        UInt8 = 0x01,
        Bool = 0x00,
    }

    public enum ForzaVersion
    {
        Forza3,
        Forza4,
        ForzaHorizon,
        ForzaHorizon2
    }

    public enum ForzaFileTypes
    {
        ForzaProfile,
        PlayerDatabase,
        Screenshots,
        Vinyls,
        CarSetups,
        Liveries,
        Designs,
        SFPurchaseHistory
    }

    public struct ForzaProfileEntry
    {
        public long Address; // not part of the collected data

        public int NameLenth;
        public string Name;
        public Int64 Flags;
        public ForzaTypes Type;
        public byte[] Value;


        private int _childCount;

        public int PropertyChildCount
        {
            get
            {
                if (Type == ForzaTypes.PropertyBag)
                    return _childCount;
                else
                    throw new ForzaException("cannot retrieve child count for a non-property bag entry.");
            }
            set
            {
                if (Type == ForzaTypes.PropertyBag)
                    _childCount = value;
                else
                    throw new ForzaException("cannot set child count for a non-property bag entry.");
            }
        }
    }

    public class ForzaProfile
    {
        public List<ForzaProfileEntry> ProfileSchemaEntries;
        public uint TotalPropertyCount;

        private EndianIO IO;

        public ForzaProfile(EndianIO IO)
        {
            if (!IO.Opened)
                IO.Open();

            this.IO = IO;

            //IO.Stream.Save(@"C:\Users\Thierry\Desktop\Game Projects\Forza Motorsport 4\Saves\Cars\Mine\1\ForzaProfile.DEC.BIN");

            this.ReadFileEntries();
        }

        private void ReadFileEntries()
        {
            TotalPropertyCount = this.IO.In.ReadUInt32();

            this.ProfileSchemaEntries = new List<ForzaProfileEntry>();

            for (int x = 0; x < TotalPropertyCount; x++)
            {
                var ent = ReadProfileEntry();

                if (ent.Type == ForzaTypes.PropertyBag)
                    // for PropertyBags, the value specifies how many properties the bag holds 
                {
                    ent.PropertyChildCount = BitConverter.ToInt32(
                        Horizon.Functions.Global.convertToBigEndian(ent.Value), 0);

                    this.ProfileSchemaEntries.Add(ent);

                    for (int j = 0; j < ent.PropertyChildCount; j++)
                    {
                        this.ProfileSchemaEntries.Add(ReadProfileEntry());
                    }
                }
                else
                {
                    this.ProfileSchemaEntries.Add(ent);
                }
            }
            if (this.ProfileSchemaEntries.Count == 0)
            {
                throw new ForzaException("did not read any settings from the profile.");
            }
        }

        private ForzaProfileEntry ReadProfileEntry()
        {
            var Entry = new ForzaProfileEntry();

            Entry.NameLenth = this.IO.In.ReadInt32();
            Entry.Name = new string(this.IO.In.ReadChars(Entry.NameLenth));
            Entry.Flags = this.IO.In.ReadInt64();
            Entry.Type = (ForzaTypes) this.IO.In.ReadByte();
            Entry.Address = this.IO.In.BaseStream.Position;

            if ((this.IO.In.BaseStream.Length - GetTypeLength(Entry.Type)) > 0)
            {
                Entry.Value = this.IO.In.ReadBytes(GetTypeLength(Entry.Type));
            }
            else
            {
                throw new ForzaException("invalid Profile property data size");
            }
            return Entry;
        }

        public static int GetTypeLength(ForzaTypes type)
        {
            switch (type)
            {
                case ForzaTypes.CarId:
                    return 4;
                case ForzaTypes.PropertyBag:
                    return 4;
                case ForzaTypes.UInt64:
                    return 8;
                case ForzaTypes.Float32:
                    return 4;
                case ForzaTypes.UInt32:
                    return 4;
                case ForzaTypes.Uint16:
                    return 2;
                case ForzaTypes.UInt8:
                    return 1;
                case ForzaTypes.Bool:
                    return 1;
                default:
                    throw new Exception("could not determine the length of a data type.");
            }
        }

        public long GetEntryAddress(string EntryName)
        {
            //Retrieves the address of the entry.
            if (EntryName != null && EntryName != string.Empty)
            {
                return this.ProfileSchemaEntries.Find(delegate(ForzaProfileEntry entry)
                                                          {
                                                              return entry.Name == EntryName;
                                                          }).Address; // Address of the entry in the stream.
            }
            else
            {
                throw new ForzaException(string.Format("invalid Profile property name specified: {0}.", EntryName));
            }
        }

        public ForzaProfileEntry GetEntry(string EntryName)
        {
            //Retrieves the entry with the specified name.
            if (EntryName != null && EntryName != string.Empty)
            {
                return this.ProfileSchemaEntries.Find(delegate(ForzaProfileEntry entry)
                                                          {
                                                              return entry.Name == EntryName;
                                                          });
            }
            else
            {
                throw new ForzaException(string.Format("invalid Profile property name specified: {0}.", EntryName));
            }
        }
    }

    public class ForzaScreenshot
    {
        private readonly EndianIO _io;
        private static byte[] _aesKey, _shaKey;
        private readonly byte[] _creator;
        private readonly GlobalForzaSecurity _forzaSecurity;

        public ForzaScreenshot(EndianIO io, ulong creatorId, byte[] aesKey, byte[] hmacKey, ForzaVersion forzaVersion)
        {
            if (!io.Opened)
                io.Open();

            _creator = Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(creatorId));

            if (_aesKey == null || _shaKey == null)
            {
                switch (forzaVersion)
                {
                    case ForzaVersion.ForzaHorizon:
                        _aesKey = GlobalForzaSecurity.TransformHorizonSessionKey(aesKey, _creator, 5);
                        _shaKey = GlobalForzaSecurity.TransformHorizonSessionKey(hmacKey, _creator, 4);
                        break;
                }
            }
            _io = io;
            _forzaSecurity = new GlobalForzaSecurity(ForzaVersion.ForzaHorizon, _aesKey, _shaKey);
            _forzaSecurity.ReadHeader(_io.In.ReadBytes(0x34));
        }

        public byte[] Read()
        {
            return _forzaSecurity.DecryptData(_io.ToArray()).ToArray();
        }

        public byte[] Write(byte[] imageData)
        {
            _io.Close();
            return _forzaSecurity.EncryptData(imageData);
        }

        public static byte[] EncryptScreenshot(byte[] ImageData, ref byte[] EncryptionHeader, int[] FSEncHeader,
                                               byte[] AesKey, byte[] AesIv, byte[] ShaKey, ref int FileSize,
                                               ForzaVersion Version)
        {
            int RoundedImageSize = (int) ((ImageData.Length + 0x0F) & 0xFFFFFFF0);

            var imageMS = new MemoryStream();
            var imageWriter = new EndianWriter(imageMS, EndianType.BigEndian);

            if (FileSize != ImageData.Length)
            {
                int ObfuscatedFileSize = GlobalForzaSecurity.ObfuscateLength(ImageData.Length, FSEncHeader[0], FSEncHeader[1],
                                                                       FSEncHeader[2], FSEncHeader[3]);

                int UnObfuscatedFileSize = GlobalForzaSecurity.UnObfuscateLength(ObfuscatedFileSize, FSEncHeader[0],
                                                                           FSEncHeader[1], FSEncHeader[2],
                                                                           FSEncHeader[3]);

                if (UnObfuscatedFileSize != ImageData.Length)
                {
                    throw new Exception("ForzaScreenshot: file size error occured while writing an image.");
                }

                // copy the obfuscated length into our DataEx that will be hashed
                Array.Copy(Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(ObfuscatedFileSize)), 0,
                           EncryptionHeader, 0, 0x04);

                FileSize = ImageData.Length;
            }

            int remainder = RoundedImageSize - ImageData.Length;

            var memorystream = new MemoryStream();
            memorystream.SetLength(RoundedImageSize);

            memorystream.Write(ImageData, 0, ImageData.Length);

            // create a 16-byte aligned array
            if (remainder != 0x00)
            {
                byte[] confounder = new byte[remainder];
                new Random().NextBytes(confounder);
                memorystream.Write(confounder, 0x00, confounder.Length);
            }

            memorystream.Close();
            ImageData = memorystream.ToArray();

            // encrypt image data
            GlobalForzaSecurity.AesCbc(AesKey, ref ImageData, AesIv, true);

            imageWriter.SeekTo(0x00);

            if (Version == ForzaVersion.Forza4)
            {
                imageWriter.Write(Forza4.Forza4KeyMarshal.HashData(ShaKey, ImageData, EncryptionHeader));
            }
            else if (Version == ForzaVersion.Forza3)
            {
                imageWriter.Write(Forza3.ForzaSecurity.HmacSha(ShaKey, EncryptionHeader, ImageData));
            }
            else
            {
                throw new Exception("ForzaScreenshot: invalid Forza Motorsport version specified.");
            }

            imageWriter.Write(EncryptionHeader);
            imageWriter.Write(ImageData);

            imageWriter.Close();
            return imageMS.ToArray();
        }
    }

    public class ForzaDatabase
    {
        protected readonly EndianIO Io;
        private readonly byte[] _aesKey, _shaKey;
        private readonly GlobalForzaSecurity _securityProcessor;
        protected ForzaSQLite Database;

        public ForzaDatabase(EndianIO io, byte[] aesKey, byte[] shaKey, ulong creator, ForzaSQLite dataBase,
                             ForzaVersion gameVersion)
        {
            if (!io.Opened) // Player Database
                io.Open();
            Database = dataBase;
            Io = io;
            var creatorId = Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(creator));
            switch (gameVersion)
            {
                case ForzaVersion.Forza3:
                case ForzaVersion.Forza4:
                    _aesKey = GlobalForzaSecurity.TransformSessionKey(aesKey, creatorId, 3);
                    _shaKey = GlobalForzaSecurity.TransformSessionKey(shaKey, creatorId, 2);
                    break;
                case ForzaVersion.ForzaHorizon:
                    _aesKey = GlobalForzaSecurity.TransformHorizonSessionKey(aesKey, creatorId, 3);
                    _shaKey = GlobalForzaSecurity.TransformHorizonSessionKey(shaKey, creatorId, -1);
                    break;
            }
            _securityProcessor = new GlobalForzaSecurity(gameVersion, _aesKey, _shaKey);
            byte[] fileData = Io.ToArray();
            bool profileData = fileData.ReadInt32(0x00) == 0x636D7373;
            Read(_securityProcessor.DecryptData(fileData,  profileData).ToArray());
        }

        public DataTable GetDataTable(string tableName)
        {
            return Database.RetrieveTableData(tableName);
        }

        private void Read(byte[] input)
        {
            var databaseData = Decompress(input);
            var fileName = Path.GetTempFileName();
            var dbFile = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            dbFile.Write(databaseData, 0, databaseData.Length);
            dbFile.Flush();
            if (Database != null)
                Database.SetDataSource(fileName, dbFile);
            else
                Database = new ForzaSQLite(fileName, dbFile);
        }

        public byte[] Extract()
        {
            return Database.Extract();
        }

        public void Save(bool isLiveryDatabase)
        {
            byte[] dataFile = Extract();
            int dataLength = dataFile.Length, pos = 0;

            var ms = new MemoryStream();
            var dbwriter = new EndianWriter(ms, EndianType.BigEndian);
            dbwriter.Write(dataLength);

            while (dataLength > 0)
            {
                var readlen = dataLength;
                if (readlen >= 0x200000)
                    readlen = 0x200000;

                byte[] sdata = new byte[readlen];
                Array.Copy(dataFile, pos, sdata, 0, readlen);

                var compressedData = Compress(sdata);

                dbwriter.Write(compressedData.Length);
                dbwriter.Write(sdata.Length);
                dbwriter.Write(compressedData);

                pos += readlen;
                dataLength -= readlen;
            }

            var outData = _securityProcessor.EncryptData(ms.ToArray());
            dbwriter.Close();
            if (isLiveryDatabase)
            {
                Io.Out.SeekTo(0x00);
                Io.Out.Write(outData);
            }
            else
            {
                Io.Out.SeekTo(8);
                Io.Out.Write(outData);
                Io.Out.SeekTo(4);
                Io.Out.Write(outData.Length);
            }
        }

        public static byte[] Decompress(byte[] input)
        {
            MemoryStream ms = new MemoryStream();

            EndianReader reader = new EndianReader(input, EndianType.BigEndian);

            int TotalSize = reader.ReadInt32();

            while (TotalSize > 0)
            {
                int CompressedSize = reader.ReadInt32();
                int DecompressedSize = reader.ReadInt32();

                byte[] DecompressedData = new byte[DecompressedSize];

                var DeflateStream = new DeflateStream(new MemoryStream(reader.ReadBytes(CompressedSize)),
                                                      System.IO.Compression.CompressionMode.Decompress);
                DeflateStream.Read(DecompressedData, 0, DecompressedSize);
                DeflateStream.Close();

                TotalSize -= DecompressedSize;

                ms.Write(DecompressedData, 0, DecompressedSize);
            }

            return ms.ToArray();
        }

        public static byte[] Compress(byte[] input)
        {
            MemoryStream MS = new MemoryStream();
            var SqlStream = new MemoryStream(input);
            var DeflateStream = new DeflateStream(MS, CompressionMode.Compress);

            var data = new byte[4096];
            int length;

            while ((length = SqlStream.Read(data, 0, 4096)) > 0)
            {
                DeflateStream.Write(data, 0, length);
            }

            SqlStream.Close();
            DeflateStream.Close();

            return MS.ToArray();
        }
    }

    public class ForzaLiveryDatabase : ForzaDatabase
    {
        public ForzaLiveryDatabase(EndianIO io, byte[] aesKey, byte[] shaKey, ulong creator, ForzaSQLite dataBase,
                                   ForzaVersion gameVersion) : base(io, aesKey, shaKey, creator, dataBase, gameVersion)
        {
            
        }
        public List<string> GetFilenames(ForzaFileTypes fileType)
        {
            List<string> filenames = new List<string>();
            foreach (DataRow r in Database.RetrieveTableData("MetaData").Rows)
            {
                if (Convert.ToBoolean(r["IsNewFile"])) continue;
                switch (fileType)
                {
                    case ForzaFileTypes.Vinyls:
                        if (r["Folder"].ToString().Contains("LivGroups"))
                            filenames.Add(string.Format("{0}\\{1:D4}_layers.lvg", r["Title"], Convert.ToUInt32(r["Layers"]) & 0xFFFF));
                        break;
                    case ForzaFileTypes.CarSetups:
                        if (r["Folder"].ToString().Contains("CarSetup"))
                            filenames.Add(string.Format("{0}{1}", r["Folder"].ToString().Substring(0xA), r["DataFileName"]));
                        break;
                    case ForzaFileTypes.Liveries:
                        if (r["Folder"].ToString().Contains("LivCatalog"))
                            filenames.Add(string.Format("{0}\\{1}\\{2:D4}_layers_{3:D4}.{4:D4}", Convert.ToUInt32(r["Ordinal"]), r["Title"], Convert.ToUInt32(r["NumLayers"]) & 0xFFFF, Convert.ToUInt32(r["Ordinal"]), "liv"));
                        break;
                }
            }
            return filenames;
        }
        public List<string> GetOwners(ForzaFileTypes Type)
        {
            List<string> owners = new List<string>();
            foreach (DataRow r in Database.RetrieveTableData("MetaData").Rows)
            {
                if (Convert.ToBoolean(r["IsNewFile"])) continue;
                switch (Type)
                {
                    case ForzaFileTypes.Vinyls:
                    case ForzaFileTypes.Liveries:
                        owners.Add(r["Owner"].ToString());
                        break;
                    case ForzaFileTypes.CarSetups:
                        owners.Add(r["Creator"].ToString());
                        break;
                }
            }
            return owners;
        }
        public List<string> GetSaveIds()
        {
            return (from DataRow r in Database.RetrieveTableData("MetaData").Rows where !Convert.ToBoolean(r["IsNewFile"]) select r["SaveID"].ToString()).ToList();
        }
        public List<string> GetLocks()
        {
            return (from DataRow r in Database.RetrieveTableData("MetaData").Rows where !Convert.ToBoolean(r["IsNewFile"]) select Convert.ToBoolean(r["IsLocked"]).ToString()).ToList();
        }
        public void UpdateSetting(string record, string recordId, string value)
        {
            Database.UpdateTableData("MetaData", record, value, "SaveID", recordId);
        }
        public object SearchTableForSetting(string SearchString, string Setting, DataTable Table)
        {
            foreach (DataRow r in Table.Rows)
            {
                if (r["SaveID"].ToString() == SearchString)
                {
                    return r[Setting];
                }
            }
            return null;
        }
    }

    public class ForzaLivery
    {
        protected EndianIO _io, _liveryIO;
        private readonly GlobalForzaSecurity _securityProcessor;
        public bool IsUnlocked;
        protected int FileSize;
        public ForzaLivery(EndianIO io, ForzaFileTypes fileType, ForzaKeyMarshal keyMarshal, ForzaVersion gameVersion)
        {
            if(!io.Opened)
                io.Open();

            _io = io;

            _securityProcessor = new GlobalForzaSecurity(gameVersion, keyMarshal.ForzaAesKeyMarshal(fileType), keyMarshal.ForzaShaKeyMarshal(fileType));
            _liveryIO = _securityProcessor.DecryptData(_io.ToArray());
            FileSize = _securityProcessor.FileSize;
        }

        public void Save()
        {
            _liveryIO.Stream.Flush();

            _io.SeekTo(0x00);
            _io.Out.Write(_securityProcessor.EncryptData(Extract()));
            _io.Stream.Flush();
        }

        internal void Close()
        {
            if (_io.Opened)
                _io.Close();
            if (_liveryIO.Opened)
                _liveryIO.Close();
        }

        public byte[] Extract()
        {
            if (_liveryIO == null)
                return null;

            _liveryIO.SeekTo(0x00);
            return _liveryIO.In.ReadBytes(FileSize);
        }

        public void Inject(byte[] liveryData)
        {
            _liveryIO = new EndianIO(liveryData, EndianType.BigEndian);
            FileSize = liveryData.Length;

            _io.Stream.SetLength(FileSize);
            _io.SeekTo(0x00);
            _io.Out.Write(_securityProcessor.EncryptData(liveryData));
            _io.Stream.Flush();
        }

        public virtual void LockUnlock(bool unlock)
        {}
    }

    public class ForzaVinylGroup : ForzaLivery
    {
        public ForzaVinylGroup(EndianIO io, ForzaKeyMarshal keyMarshal, ForzaVersion gameVersion) :
            base(io, ForzaFileTypes.Vinyls, keyMarshal, gameVersion)
        {
            if (_liveryIO.In.ReadInt32() != 0x6C767967)
            {
                throw new ForzaException("invalid layer group magic.");
            }
            _liveryIO.In.SeekTo(0x1C);
            IsUnlocked = !(_liveryIO.In.ReadByte() >= 1);
        }

        public override void LockUnlock(bool unlock)
        {
            byte authorCount = _liveryIO.In.SeekNPeekByte(0x1C);
            var memoryStream = new MemoryStream();
            if ((IsUnlocked = unlock))
            {
                // file is already unlocked
                if(authorCount == 0x00)
                    return;

                var livWriter = new EndianWriter(memoryStream, EndianType.BigEndian);
                _liveryIO.In.SeekTo(0x00);
                livWriter.Write(_liveryIO.In.ReadBytes(0x1C));
                livWriter.WriteByte(0x00);
                _liveryIO.In.BaseStream.Position += (1 + (authorCount * 8));
                livWriter.Write(_liveryIO.In.ReadBytes(FileSize - _liveryIO.In.BaseStream.Position));
                FileSize = (int)memoryStream.Length;
                livWriter.Close();
                _liveryIO = new EndianIO(memoryStream.ToArray(), EndianType.BigEndian, true);
            }
            else
            {
                // means its already locked
                if (authorCount != 0)
                    return;

                var livWriter = new EndianWriter(memoryStream, EndianType.BigEndian);
                _liveryIO.In.SeekTo(0x00);
                livWriter.Write(_liveryIO.In.ReadBytes(0x1c));
                livWriter.WriteByte(0x01);
                livWriter.Write(new byte[8]);
                _liveryIO.In.BaseStream.Position += 1;
                livWriter.Write(_liveryIO.In.ReadBytes(FileSize - _liveryIO.In.BaseStream.Position));
                FileSize = (int)memoryStream.Length;
                livWriter.Close();
                _liveryIO = new EndianIO(memoryStream.ToArray(), EndianType.BigEndian, true);
            }
        }
    }

    public class ForzaLiveryCatalog : ForzaLivery
    {
        public ForzaLiveryCatalog(EndianIO io, ForzaKeyMarshal keyMarshal, ForzaVersion gameVersion) :
            base(io, ForzaFileTypes.Liveries, keyMarshal, gameVersion)
        {
            if (_liveryIO.In.ReadInt32() != 0x63726C76)
            {
                throw new ForzaException("invalid livery magic.");
            }

            _liveryIO.In.SeekTo(0xB);
            IsUnlocked = !Convert.ToBoolean(_liveryIO.In.ReadByte() & 1); 
        }

        public override void LockUnlock(bool unlock)
        {
            _liveryIO.In.SeekTo(0xB);
            byte flag = _liveryIO.In.ReadByte();
            flag = (IsUnlocked = unlock)? (byte)(flag & 0xFE) : (byte)(flag | 1);
            _liveryIO.Out.SeekTo(0xb);
            _liveryIO.Out.Write(flag);
        }
    }

    public class ForzaCarSetup : ForzaLivery
    {
        public ForzaCarSetup(EndianIO io, ForzaKeyMarshal keyMarshal, ForzaVersion gameVersion) :
            base(io, ForzaFileTypes.CarSetups, keyMarshal, gameVersion)
        {
            _liveryIO.In.SeekTo(0x01);
            IsUnlocked = !Convert.ToBoolean(_liveryIO.In.ReadByte() & 1);
        }

        public override void LockUnlock(bool unlock)
        {
            _liveryIO.In.SeekTo(0x01);
            byte flag = _liveryIO.In.ReadByte();
            flag = (IsUnlocked = unlock) ? (byte)(flag & 0xFE) : (byte)(flag | 1);
            _liveryIO.Out.SeekTo(0x01);
            _liveryIO.Out.Write(flag);
        } 
    }


    public class ForzaKeyMarshal
    {
        private readonly Dictionary<string, int> _keyTable;
        private readonly List<byte[]> _keyStack;

        public ForzaKeyMarshal() 
        {
            _keyTable = new Dictionary<string, int>();
            _keyStack = new List<byte[]>();
        }

        public ForzaKeyMarshal(string fileTypeName, byte[] aesKey, byte[] shaKey)
        {
            _keyTable = new Dictionary<string, int>();
            _keyStack = new List<byte[]>();

            AddKey(fileTypeName + "AES", aesKey);
            AddKey(fileTypeName + "SHA", shaKey);
        }

        public void AddKey(string keyIdent, byte[] key)
        {
            _keyTable.Add(keyIdent, _keyTable.Count);

            _keyStack.Add(key);
        }
        public byte[] ForzaAesKeyMarshal(ForzaFileTypes type)
        {
            switch (type)
            {
                case ForzaFileTypes.Screenshots:
                    return _keyStack[_keyTable["ScreenshotAES"]];
                case ForzaFileTypes.CarSetups:
                    return _keyStack[_keyTable["CarSetupAES"]];
                case ForzaFileTypes.Liveries:
                    return _keyStack[_keyTable["LiveryCatalogAES"]];
                case ForzaFileTypes.Vinyls:
                    return _keyStack[_keyTable["LiveryGroupAES"]];
                case ForzaFileTypes.ForzaProfile:
                    return _keyStack[_keyTable["ProfileAES"]];
                case ForzaFileTypes.PlayerDatabase:
                    return _keyStack[_keyTable["DatabaseAES"]];
                default:
                    throw new Exception("ForzaSecurity: Attempted to retrieve a key from an invalid index.");
            }
        }
        public byte[] ForzaShaKeyMarshal(ForzaFileTypes type)
        {
            switch (type)
            {
                case ForzaFileTypes.Screenshots:
                    return _keyStack[_keyTable["ScreenshotSHA"]];
                case ForzaFileTypes.CarSetups:
                    return _keyStack[_keyTable["CarSetupSHA"]];
                case ForzaFileTypes.Liveries:
                    return _keyStack[_keyTable["LiveryCatalogSHA"]];
                case ForzaFileTypes.Vinyls:
                    return _keyStack[_keyTable["LiveryGroupSHA"]];
                case ForzaFileTypes.ForzaProfile:
                    return _keyStack[_keyTable["ProfileSHA"]];
                case ForzaFileTypes.PlayerDatabase:
                    return _keyStack[_keyTable["DatabaseSHA"]];
                default:
                    throw new Exception("ForzaSecurity: Attempted to retrieve a key from an invalid index.");
            }
        }
    }

    public class ForzaGarage : ForzaDatabase
    {
        public class ForzaGarageCar
        {
            public int CarId;
            public int Key;
            public string OriginalOwner;
            public int NumberOfOwners;
            public string ThumbnailPath;
            public ForzaCarbinReader CarReader;

            public bool IsModified;
        }

        public struct ForzaCar
        {
            public string ThumbnailPath;
            public int CarId;
        }

        public class ForzaGaragerSorter : IComparer<ForzaGarageCar>
        {
            public int Compare(ForzaGarageCar a, ForzaGarageCar b)
            {
                if (a.Key > b.Key) return 1;
                if (a.Key < b.Key) return -1;
                return 0;
            }
        }

        public ulong ProfileId;

        private List<ForzaGarageCar> _storedCars;

        public ForzaGarage(EndianIO io, byte[] aesKey, byte[] shaKey, ulong creatorId, ForzaSQLite dataBase, ForzaVersion gameVersion)
            : base(io, aesKey, shaKey, creatorId, dataBase, gameVersion)
        {
            ProfileId = creatorId;
        }

        public List<ForzaGarageCar> GetGarageCars()
        {
            _storedCars = new List<ForzaGarageCar>();
            var table = Database.RetrieveTableData("Career_Garage");
            foreach (DataRow car in table.Rows)
            {
                _storedCars.Add(new ForzaGarageCar()
                {
                    CarId = Convert.ToInt32(car["CarId"]),
                    OriginalOwner = Convert.ToString(car["OriginalOwner"]),
                    NumberOfOwners = Convert.ToInt32(car["NumOwners"]),
                    Key = Convert.ToInt32(car["Id"]),
                    ThumbnailPath = Convert.ToString(car["Thumbnail"])
                });
            }
            if (_storedCars.Count > 0x00)
            {
                _storedCars.Sort(new ForzaGaragerSorter());
            }
            return _storedCars;
        }

        public List<ForzaCar> SetGarageList(List<ForzaGarageCar> garageList, out List<string> removableThumbPaths)
        {
            var carIndex = _storedCars[_storedCars.Count - 1].Key;
            var newCarThumbPaths = new List<ForzaCar>();
            removableThumbPaths = new List<string>();
            foreach (var car in garageList)
            {
                if (!car.IsModified || car.CarReader == null) continue;
                var rCar = car.CarReader;
                if (car.Key != 0x00)
                {
                    if (Exists(car.Key, -1))
                    {
                        if(Database.Remove("Career_Garage", "Id", car.Key))
                            removableThumbPaths.Add(car.ThumbnailPath.Remove(0, 18));
                    }
                }
                var newCar = new Dictionary<string, string>();
                var thumbnailPath = string.Format(@"{0:X16}:\Thumbnails\Thumbnail_{1}.xdc", ProfileId, ++carIndex);
                while (rCar.Read())
                {
                    switch (rCar.GetValueName())
                    {
                        case "Id":
                            newCar.Add("Id", carIndex.ToString());
                            break;
                        case "CarId":
                            newCar.Add("CarId", car.CarId.ToString());
                            break;
                        case "CarGroup":
                            newCar.Add("CarGroup", rCar.GetNextValue().Replace("-1", "NULL"));
                            break;
                        case "TireBrand":
                            newCar.Add("TireBrand", rCar.GetNextValue().Replace("-1", "NULL"));
                            break;
                        case "Thumbnail":
                            newCar.Add("Thumbnail", thumbnailPath);
                            break;
                        case "OriginalOwner":
                            newCar.Add("OriginalOwner", car.OriginalOwner);
                            break;
                        case "NumOwners":
                            newCar.Add("NumOwners", car.NumberOfOwners.ToString());
                            break;
                        default:
                            newCar.Add(rCar.GetValueName(), rCar.GetNextValue());
                            break;
                    }
                }
                rCar.Close();
                Database.Insert("Career_Garage", newCar);
                newCarThumbPaths.Add(new ForzaCar
                                         {
                                             CarId = car.CarId,
                                             ThumbnailPath = thumbnailPath.Remove(0, 18)
                                         });
                car.Key = carIndex;
            }
            return newCarThumbPaths;
        }

        public bool Exists(int carKey, int carId)
        {
            if (carKey != -1 && carId != -1)
                return _storedCars.Exists(entry => ((entry.Key == carKey) && (entry.CarId == carId)));
            if (carKey != -1)
                return _storedCars.Exists(entry => entry.Key == carKey);
            if (carId != -1)
                return _storedCars.Exists(entry => entry.CarId == carId);

            throw new ForzaException("invalid values supplied for garage searching.");
        }

        public void ExportGarageSchema(string outputFilename)
        {
            var schema = Database.GetShema("Career_Garage");
            var writer = new StreamWriter(outputFilename);
            writer.Write(schema);
            writer.Close();
        }
    }
    public class ForzaCarbinReader
    {
        private readonly Crysis2.StreamCipher _streamCipher = new Crysis2.StreamCipher();
        private readonly byte[] _key;

        private EndianReader _keyInfo;
        private string _dataType;
        private string _dataName;
        private string _info;
        private readonly StringReader _schemaInfo;
        private readonly bool _isEncrypted;

        public ForzaCarbinReader(string schema, EndianReader reader, byte[] key)
        {
            if (key != null)
            {
                Array.Copy(key, 0, _key = new byte[key.Length], 0x00, key.Length);
                _isEncrypted = true;
            }

            SetCarReader(reader);

            if (!string.IsNullOrEmpty(schema))
                _schemaInfo = new StringReader(schema);
        }

        public void SetCarReader(EndianReader reader)
        {
            if (reader == null) return;
            var carBin = reader.BaseStream.ToArray();
            if(_isEncrypted)
            {
                _streamCipher.Init(_key, 0x10);
                _streamCipher.ProcessBuffer(carBin, 0x1E8, carBin);
            }
            _keyInfo = new EndianReader(carBin, EndianType.BigEndian);
        }

        public string GetNextValue()
        {
            var value = GetValue();
            if (value != null)
            {
                switch (_dataType)
                {
                    case "INT":
                    case "INTEGER":
                        return Convert.ToInt32(value).ToString();
                    case "REAL":
                        return Convert.ToSingle(value).ToString();
                    case "TEXT":
                        return value.ToString();
                }
            }
            return null;
        }

        public string GetValueName()
        {
            return _dataName;
        }

        public object GetValue()
        {
            if (_info != null)
            {
                switch (_dataType)
                {
                    case "INT":
                    case "INTEGER":
                        return _keyInfo.ReadInt32();
                    case "REAL":
                        return _keyInfo.ReadSingle();
                    case "TEXT":
                        return _keyInfo.ReadStringNullTerminated();
                    case "NULL":
                        return string.Empty;
                }
            }
            return null;
        }

        public void SkipNextValue()
        {
            GetValue();
        }

        public bool Read()
        {
            string line;
            if ((line = _schemaInfo.ReadLine()) != null)
            {
                _info = line;
                //Schema = [{ValueName}]={ValueType}
                _dataName = _info.Substring(_info.IndexOf('[') + 1, _info.IndexOf(']') - 1);
                _dataType = _info.Substring(_info.IndexOf('=') + 1, (_info.Length - _info.IndexOf('=')) - 2);
                return true;
            }
            return false;
        }

        public void Close()
        {
            _keyInfo.Close();
            _schemaInfo.Close();

            _dataName = null;
            _dataType = null;
            _info = null;
        }

        public void SaveTo(string fileName)
        {
            _keyInfo.BaseStream.Save(fileName);
        }
    }
    public class ForzaCarbinWriter
    {
        private EndianIO _io;
        private readonly Crysis2.StreamCipher _streamCipher = new Crysis2.StreamCipher();
        private readonly  byte[] _key;
        //private readonly string _garageSchema;
        private readonly DataTable _garageTable;

        public ForzaCarbinWriter(DataTable garageTable, byte[] key)
        {
            _garageTable = garageTable;
            _key = key;
        }

        public void ExportToDirectory(string directoryPath)
        {
            var reader = _garageTable.CreateDataReader();
            while (reader.Read())
            {
                _io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
                for(var i = 0; i < reader.FieldCount; i++)
                {
                    switch (reader.GetName(i))
                    {
                        case "Id":
                        case "CarId":
                        case "Thumbnail":
                        case "OriginalOwner":
                        case "NumOwners":
                            break; 
                        case "CarGroup":
                        case "TireBrand":
                            _io.Out.Write(-1);
                            break;
                        default:
                            switch (reader.GetDataTypeName(i))
                            {
                                case "Int32":
                                case "INTEGER":
                                case "INT":
                                    _io.Out.Write(reader.GetInt32(i));
                                    break;
                                case "Single":
                                case "REAL":
                                    _io.Out.Write(reader.GetFloat(i));
                                    break;
                                case "String":
                                case "TEXT":
                                    var stringValue = reader.GetString(i);
                                    if (stringValue.Length > 0x00)
                                        _io.Out.WriteAsciiString(stringValue, stringValue.Length + 1);
                                    break;
                            }
                            break;
                    }
                }
                byte[] carBin = _io.ToArray();
                if(_key != null)
                {
                    _streamCipher.Init(_key, 0x10);
                    _streamCipher.ProcessBuffer(carBin, carBin.Length, carBin);
                }
                carBin.Save(string.Format("{0}\\Car_{1}.bin", directoryPath, Convert.ToInt32(reader["CarId"])));
                _io.Close();
            }
        }
    }
    public class GlobalForzaSecurity
    {
        private ForzaVersion GameVersion;

        private byte[] AesKey, ShaKey, EncHeader, AesIv;
        internal int FileSize, _dataLocation;
        private int[] FSEncryptionTable;

        public delegate byte[] Hash(byte[] key, byte[] input1, byte[] input2);

        public GlobalForzaSecurity(ForzaVersion gameVersion)
        {
            this.GameVersion = gameVersion;
        }
        public GlobalForzaSecurity(ForzaVersion gameVersion, byte[] aesKey, byte[] shaKey)
        {
            GameVersion = gameVersion;

            AesKey = aesKey;
            ShaKey = shaKey;
        }

        public void ReadHeader(byte[] header)
        {
            _dataLocation = 0x20;
            var reader = new EndianReader(header, EndianType.BigEndian);
            InitializeEncryptionTable(reader);
        }

        public byte[] EncryptData(byte[] fileData)
        {
            int roundedFileSize = (int)((fileData.Length + 0x0F) & 0xFFFFFFF0);
            var fileMs = new MemoryStream();
            var fileWriter = new EndianWriter(fileMs, EndianType.BigEndian);

            if (FileSize != fileData.Length)
            {
                int obfuscatedFileSize = ObfuscateLength(fileData.Length, FSEncryptionTable[0], FSEncryptionTable[1], FSEncryptionTable[2], FSEncryptionTable[3]);
                int unObfuscatedFileSize = UnObfuscateLength(obfuscatedFileSize, FSEncryptionTable[0], FSEncryptionTable[1], FSEncryptionTable[2], FSEncryptionTable[3]);
                if (unObfuscatedFileSize != fileData.Length)
                {
                    throw new ForzaException("file size error occured while writing save data.");
                }

                // copy the obfuscated length into our DataEx that will be hashed
                Array.Copy(Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(obfuscatedFileSize)), 0, EncHeader, 0, 0x04);

                FileSize = fileData.Length;
            }

            int remainder = roundedFileSize - fileData.Length;

            var memorystream = new MemoryStream();
            memorystream.SetLength(roundedFileSize);

            memorystream.Write(fileData, 0, fileData.Length);

            // create a 16-byte aligned array
            if (remainder != 0x00)
            {
                byte[] confounder = new byte[remainder];
                new Random().NextBytes(confounder);
                memorystream.Write(confounder, 0x00, confounder.Length);
            }

            memorystream.Close();
            fileData = memorystream.ToArray();

            // encrypt save data
            AesCbc(AesKey, ref fileData, AesIv, true);

            fileWriter.SeekTo(0x00);
            fileWriter.Write(HashData(ShaKey, fileData, EncHeader));

            fileWriter.Write(EncHeader);
            fileWriter.Write(fileData);

            fileWriter.Close();
            return fileMs.ToArray();
        }

        public void EncryptProfileData(EndianIO io, byte[] profileData)
        {
            AesCbc(AesKey, ref profileData, AesIv, true);

            io.Out.SeekTo(0x08);
            io.Out.Write(HashData(ShaKey, profileData, EncHeader));
            // we do not need to write back the encryption header because we are not modifying the file size
            io.SeekTo(0x3C);
            io.Out.Write(profileData);
        }

        public EndianIO DecryptData(byte[] fileData, bool profileData)
        {
            _dataLocation = profileData ? 0x28 : 0x20;
            var reader = new EndianReader(fileData, EndianType.BigEndian);
            if(FSEncryptionTable == null || EncHeader == null)
            {
                FSEncryptionTable = new int[4];
                reader.SeekTo(_dataLocation);
                FileSize = UnObfuscateLength(reader.ReadInt32(), FSEncryptionTable[0] = reader.ReadInt32(), FSEncryptionTable[1] = reader.ReadInt32(),
                    FSEncryptionTable[2] = reader.ReadInt32(), FSEncryptionTable[3] = reader.ReadInt32());
                reader.SeekTo(_dataLocation); // skip the SHA256 Hash
                EncHeader = reader.ReadBytes(0x14);  // the first 4 bytes of 'EncHeader' contain an obfuscated file size, remainder is AesIV
                Array.Copy(EncHeader, 4, AesIv = new byte[0x10], 0, 0x10);
            }
            reader.SeekTo(_dataLocation + 0x14);
            byte[] saveData = reader.ReadBytes((FileSize + 0x0F) & 0xFFFFFFF0);
            //var SaveData = reader.ReadBytes(fileData.ReadInt32(0x04) - 0x20);
            reader.SeekTo(_dataLocation - 0x20);
            byte[] storedHash = reader.ReadBytes(0x20);
            if (!Horizon.Functions.Global.compareArray(storedHash, HashData(ShaKey, saveData, EncHeader)))
            {
                throw new ForzaException("Forza userdata has been tampered with.");
            }
            AesCbc(AesKey, ref saveData, AesIv, false);
            return new EndianIO(saveData, EndianType.BigEndian, true);
        }

        public EndianIO DecryptData(byte[] fileData)
        {
            return DecryptData(fileData, false);
        }

        private void InitializeEncryptionTable(EndianReader reader)
        {
            FSEncryptionTable = new int[4];
            reader.SeekTo(_dataLocation);
            int encSize = reader.ReadInt32();
            for (int i = 0; i < 0x04; i++)
                FSEncryptionTable[i] = reader.ReadInt32();
            FileSize = UnObfuscateLength(encSize, FSEncryptionTable[0], FSEncryptionTable[1], FSEncryptionTable[2], FSEncryptionTable[3]);
            reader.SeekTo(_dataLocation); // skip the SHA256 Hash
            EncHeader = reader.ReadBytes(0x14);  // the first 4 bytes of 'EncHeader' contain an obfuscated file size, remainder is AesIV
            Array.Copy(EncHeader, 4, AesIv = new byte[0x10], 0, 0x10);
        }

        public int GetFilesize()
        {
            return FileSize;
        }

        public byte[] HashData(byte[] Key, byte[] InputA, byte[] InputB)
        {
            Hash h = GameVersion == ForzaVersion.Forza3 ? new Hash(Forza3HashData) : new Hash(Forza4HashData);
            return h(Key, InputA, InputB);
        }
        public static byte[] Forza3HashData(byte[] HmacKey, byte[] Input1, byte[] Input2)
        {
            HMACSHA1 ShaHmac = new HMACSHA1();
            ShaHmac.Key = HmacKey;
            ShaHmac.TransformBlock(Input1, 0, Input1.Length, null, 0);
            ShaHmac.TransformFinalBlock(Input2, 0, Input2.Length);
            return ShaHmac.Hash;
        }
        public static byte[] Forza4HashData(byte[] ShaKey, byte[] Data, byte[] EncryptionHeader)
        {
            SHA256Managed FinalHasher = new SHA256Managed(), TempHasher = new SHA256Managed();

            int len = ShaKey.Length;
            byte[] tempbuffer = new byte[len * 2];
            for (var x = 0; x < len; x++)
            {
                tempbuffer[x] = (byte)(ShaKey[x] ^ 0x5C);
                tempbuffer[x + 0x20] = 0x5C;
            }
            FinalHasher.TransformBlock(tempbuffer, 0, 0x40, null, 0);

            for (var x = 0; x < len; x++)
            {
                tempbuffer[x] = (byte)(ShaKey[x] ^ 0x36);
                tempbuffer[x + 0x20] = 0x36;
            }
            TempHasher.TransformBlock(tempbuffer, 0, 0x40, null, 0);
            TempHasher.TransformBlock(EncryptionHeader, 0, 0x14, null, 0);
            TempHasher.TransformFinalBlock(Data, 0, Data.Length);

            FinalHasher.TransformFinalBlock(TempHasher.Hash, 0, 0x20);

            return FinalHasher.Hash;
        }

        // Old functions - left for compability
        public static void AesCbc(byte[] AESKey, ref byte[] Data, byte[] IV, bool Encrypt)
        {
            AesCryptoServiceProvider AesCrypto = new AesCryptoServiceProvider();
            AesCrypto.IV = IV;
            AesCrypto.Key = AESKey;
            AesCrypto.Mode = CipherMode.CBC;
            AesCrypto.Padding = PaddingMode.None;
            ICryptoTransform cTransform;

            if (Encrypt)
            {
                cTransform = AesCrypto.CreateEncryptor();
            }
            else
            {
                cTransform = AesCrypto.CreateDecryptor();
            }
            Data = cTransform.TransformFinalBlock(Data, 0, Data.Length);
        }
        public static byte[] TransformSessionKey(byte[] BaseKey, byte[] CreatorId, int ObfStartIndex)
        {
            int KeySize = BaseKey.Length, Mask = KeySize - 1;
            byte[] key = new byte[KeySize];
            Int64 num = 0, num2 = 0, num3 = 0;
            for (int x = new int(); x < KeySize; x++)
            {
                num3 = x & Mask;
                num2 = (x + ObfStartIndex) & 7;
                num3 = BaseKey[num3];
                num = CreatorId[num2];
                num3 = num ^ num3;
                key[x] = (byte)num3;
            }
            return key;
        }
        public static byte[] TransformHorizonSessionKey(byte[] baseKey, byte[] creatorId, int obfStartIndex)
        {
            int keySize = baseKey.Length, mask = keySize - 1;
            byte[] key = new byte[keySize];
            Int64 num4 = 0;
            for (int x = new int(); x < keySize; x++)
            {
                Int64 num3 = x & mask;
                Int64 num2 = (x + obfStartIndex) & 7;
                num3 = baseKey[num3];
                Int64 num = creatorId[num2];
                num3 = num ^ num3;
                num3 += num4;
                num4 += 2;
                key[x] = (byte)(num3 & 0xFF);
            }
            return key;
        }
        public static int UnObfuscateLength(int dwInput1, int dwInput2, int dwInput3, int dwInput4, int dwInput5)
        {
            dwInput3 ^= dwInput2;
            dwInput3 ^= dwInput4;
            dwInput3 ^= dwInput5;
            dwInput5 = dwInput3 ^ dwInput1;
            return dwInput5;
        }
        public static int UnObfuscateLength2(int dwInput1, int dwInput2, int dwInput3, int dwInput4, int dwInput5)
        {
            return (int)((UnObfuscateLength(dwInput1, dwInput2, dwInput3, dwInput4, dwInput5) + 0x0F) & 0xFFFFFFF0);
        }
        public static int ObfuscateLength(int Length, int dwInput1, int dwInput2, int dwInput3, int dwInput4)
        {
            int dwInput5 = dwInput1 ^ dwInput2;
            dwInput5 ^= dwInput3;
            dwInput5 ^= dwInput4;
            dwInput5 ^= Length;
            return dwInput5;
        }
    }
    public class ForzaSQLite
    {
        private SQLiteDatabase Database;
        private string DatabaseSource;
        private FileStream DbFile;

        public ForzaSQLite(string DataSource, FileStream DatabaseFile, string[] TableListing)
        {
            if ((DataSource != null && DataSource != string.Empty))
            {
                if (DatabaseFile != null)
                {
                    DbFile = DatabaseFile;
                }

                this.Database = new SQLiteDatabase();
                this.Database.Open();
                this.Database.Attach(DataSource, TableListing);

                DatabaseSource = DataSource;
            }
        }
        public ForzaSQLite(string DataSource, FileStream DatabaseFile)
        {
            SetDataSource(DataSource, DatabaseFile);
        }

        public void SetDataSource(string DataSource, FileStream DatabaseFile)
        {
            if ((DataSource != null && DataSource != string.Empty))
            {
                if (DatabaseFile != null)
                {
                    DbFile = DatabaseFile;
                }

                this.Database = new SQLiteDatabase(DataSource);
                this.Database.Open();

                DatabaseSource = DataSource;
            }
        }

        public List<string> RetrieveTables()
        {
            List<string> Tables = new List<string>();

            var MainTables = Database.GetDataTable("SELECT name FROM sqlite_master WHERE type = \"table\";");
            foreach (DataRow table in MainTables.Rows)
            {
                Tables.Add(table[0].ToString());
            }

            return Tables;
        }

        public List<string> RetrieveTableEntries(string TableName)
        {
            var TableEntries = new List<string>();

            var DataTable = Database.GetDataTable(string.Format("SELECT * FROM {0}", TableName));

            var DataColumn = DataTable.Columns;
            foreach (var Column in DataColumn)
            {
                TableEntries.Add(Column.ToString());
            }
            return TableEntries;
        }

        public DataTable RetrieveTableData(string TableName)
        {
            return Database.GetDataTable(string.Format("SELECT * FROM {0}", TableName));
        }

        public int GetCount(string TableName)
        {
            return int.Parse(Database.ExecuteScalar(string.Format("SELECT COUNT() FROM {0}", TableName)));
        }

        public bool Insert(string TableName, Dictionary<string, string> Data)
        {
            return this.Database.Insert(TableName, Data);
        }

        public string GetShema(string TableName)
        {
            return this.Database.GetSchema(TableName);
        }

        public bool UpdateTableData(string TableName, string RecordName, string Value, string RecordIdName, string RecordId)
        {
            var NewValues = new Dictionary<String, String>();
            NewValues.Add(RecordName, Value);
            return this.Database.Update(TableName, NewValues, string.Format("{0}={1}", RecordIdName, RecordId));
        }

        public byte[] Extract()
        {
            this.DbFile.Flush();
            return this.DbFile.ToArray();
        }

        public bool Remove(string TableName, string RecordIdName, string RecordId)
        {
            return this.Database.Delete(TableName, string.Format("{0}={1}", RecordIdName, RecordId));
        }

        public bool Remove(string TableName, string RecordIdName, int RecordId)
        {
            return this.Database.Delete(TableName, string.Format("{0}={1}", RecordIdName, RecordId));
        }
        /*
        public void ExtractCarBins()
        {
            int carId;
            byte[] _key = new byte[0x10] {0x7A,0xB4,0x10,0x55,0x4E,0x83,0xA0,0x7B,0x44,0x47,0x2C,0xAA,0x84,0xD2,0xFE,0xEB};
            for (int x = 0; x < 018; x++)
            {
                byte[] CarData = this.Database.ExtractCarBins("Career_Garage", x, out carId);
                Crysis2.StreamCipher s = new Crysis2.StreamCipher();
                s.Init(_key, 0x10);
                s.ProcessBuffer(CarData, 0x1E8, CarData);
                CarData.Save(string.Format(@"C:\Users\Thierry\Desktop\Game Projects\Forza Motorsport 4\Saves\Cars\Unicorns\Carbins\Car_{0}.bin", carId));
            }
        }
        */
    }
    public class SQLiteDatabase
    {
        String dbConnection;
        SQLiteConnection cnn;

        /// <summary>
        ///     Default Constructor for SQLiteDatabase Class.
        /// </summary>
        public SQLiteDatabase()
        {
            dbConnection = "Data Source=:memory:;Version=3;";
        }

        /// <summary>
        ///     Single Param Constructor for specifying the DB file.
        /// </summary>
        /// <param name="inputFile">The File containing the DB</param>
        public SQLiteDatabase(String inputFile)
        {
            dbConnection = String.Format("Data Source={0};Version=3;", inputFile);
        }

        public void Open()
        {
            cnn = new SQLiteConnection(dbConnection);
            cnn.Open();
        }

        public void Attach(string OriginalDBFileName, string[] TableListing)
        {
            if (TableListing != null && TableListing.Length > 0)
            {
                SQLiteCommand mycommand = new SQLiteCommand(string.Format("ATTACH \"{0}\" AS disk", OriginalDBFileName), cnn);
                mycommand.ExecuteNonQuery();
                foreach (string table in TableListing)
                {
                    mycommand = new SQLiteCommand(string.Format("CREATE TABLE {0} AS SELECT * FROM disk.{1};", table, table), cnn);
                    mycommand.ExecuteNonQuery();
                }
                mycommand = new SQLiteCommand("DETACH disk", cnn);
                mycommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Single Param Constructor for specifying advanced connection options.
        /// </summary>
        /// <param name="connectionOpts">A dictionary containing all desired options and their values</param>
        public SQLiteDatabase(Dictionary<String, String> connectionOpts)
        {
            String str = "";
            foreach (KeyValuePair<String, String> row in connectionOpts)
            {
                str += String.Format("{0}={1}; ", row.Key, row.Value);
            }
            str = str.Trim().Substring(0, str.Length - 1);
            dbConnection = str;
        }

        /// <summary>
        ///     Allows the programmer to run a query against the Database.
        /// </summary>
        /// <param name="sql">The SQL to run</param>
        /// <returns>A DataTable containing the result set.</returns>
        public DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SQLiteCommand mycommand = new SQLiteCommand(cnn);
                mycommand.CommandText = sql;
                SQLiteDataReader reader = mycommand.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return dt;
        }

        /// <summary>
        ///     Allows the programmer to interact with the database for purposes other than a query.
        /// </summary>
        /// <param name="sql">The SQL to be run.</param>
        /// <returns>An Integer containing the number of rows updated.</returns>
        public int ExecuteNonQuery(string sql)
        {

            SQLiteCommand mycommand = new SQLiteCommand(cnn);
            mycommand.CommandText = sql;
            int rowsUpdated = mycommand.ExecuteNonQuery();
            return rowsUpdated;
        }

        /// <summary>
        ///     Allows the programmer to retrieve single items from the DB.
        /// </summary>
        /// <param name="sql">The query to run.</param>
        /// <returns>A string.</returns>
        public string ExecuteScalar(string sql)
        {
            SQLiteCommand mycommand = new SQLiteCommand(cnn);
            mycommand.CommandText = sql;
            object value = mycommand.ExecuteScalar();
            if (value != null)
            {
                return value.ToString();
            }
            return "";
        }

        /// <summary>
        ///     Allows the programmer to easily update rows in the DB.
        /// </summary>
        /// <param name="tableName">The table to update.</param>
        /// <param name="data">A dictionary containing Column names and their new values.</param>
        /// <param name="where">The where clause for the update statement.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool Update(String tableName, Dictionary<String, String> data, String where)
        {
            String vals = "";
            Boolean returnCode = true;
            if (data.Count >= 1)
            {
                foreach (KeyValuePair<String, String> val in data)
                {
                    vals += String.Format(" {0} = '{1}',", val.Key.ToString(), val.Value.ToString());
                }
                vals = vals.Substring(0, vals.Length - 1);
            }
            try
            {
                returnCode = this.ExecuteNonQuery(String.Format("update {0} set {1} where {2};", tableName, vals, where)) > 0 ;
            }
            catch
            {
                returnCode = false;
            }
            return returnCode;
        }

        /// <summary>
        ///     Allows the programmer to easily delete rows from the DB.
        /// </summary>
        /// <param name="tableName">The table from which to delete.</param>
        /// <param name="where">The where clause for the delete.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool Delete(String tableName, String where)
        {
            Boolean returnCode = true;
            try
            {
                this.ExecuteNonQuery(String.Format("delete from {0} where {1};", tableName, where));
            }
            catch (Exception fail)
            {
                System.Diagnostics.Debug.WriteLine(fail.ToString());
                returnCode = false;
            }
            return returnCode;
        }

        /// <summary>
        ///     Allows the programmer to easily insert into the DB
        /// </summary>
        /// <param name="tableName">The table into which we insert the data.</param>
        /// <param name="data">A dictionary containing the column names and data for the insert.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool Insert(String tableName, Dictionary<String, String> data)
        {
            String columns = "";
            String values = "";
            Boolean returnCode = true;
            foreach (KeyValuePair<String, String> val in data)
            {
                columns += String.Format(" {0},", val.Key.ToString());
                values += String.Format(" '{0}',", val.Value);
            }
            columns = columns.Substring(0, columns.Length - 1);
            values = values.Substring(0, values.Length - 1);
            try
            {
                this.ExecuteNonQuery(String.Format("insert into {0}({1}) values({2});", tableName, columns, values));
            }
            catch (Exception fail)
            {
                System.Diagnostics.Debug.WriteLine(fail.ToString());
                returnCode = false;
            }
            return returnCode;
        }

        /// <summary>
        ///     Allows the programmer to easily delete all data from the DB.
        /// </summary>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool ClearDB()
        {
            DataTable tables;
            try
            {
                tables = this.GetDataTable("select NAME from SQLITE_MASTER where type='table' order by NAME;");
                foreach (DataRow table in tables.Rows)
                {
                    this.ClearTable(table["NAME"].ToString());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Allows the user to easily clear all data from a specific table.
        /// </summary>
        /// <param name="table">The name of the table to clear.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool ClearTable(String table)
        {
            try
            {

                this.ExecuteNonQuery(String.Format("delete from {0};", table));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetSchema(String table)
        {
            StringBuilder Schema = new StringBuilder();
            // These is how you list the schema of an SQLite database
            SQLiteCommand comm = new SQLiteCommand(string.Format("SELECT * FROM {0}", table), cnn);
            // Populate the reader
            SQLiteDataReader reader = comm.ExecuteReader();

            reader.Read();
            {
                for (int a = 0; a < reader.FieldCount; a++)
                {
                    // This will give you the name of the current row's column
                    string columnName = reader.GetName(a);
                    // This will give you the value of the current row's column
                    string columnValue = reader[a].ToString();
                    Schema.AppendLine(string.Format("[{0}]={1},", reader.GetName(a), reader.GetDataTypeName(a)));
                }
            }
            reader.Close();

            return Schema.ToString();
        }

        /*
        public byte[] ExtractCarBins(String table, int CarIndex, out int CarId)
        {
            StringBuilder Schema = new StringBuilder();
            // These is how you list the schema of an SQLite database
            SQLiteCommand comm = new SQLiteCommand(string.Format("SELECT * FROM {0}", table), cnn);
            // Populate the reader
            SQLiteDataReader reader = comm.ExecuteReader();

            var ms = new MemoryStream();
            var writer = new EndianWriter(ms, EndianType.BigEndian);
            int i = 0;
            CarId = 0;
            while(reader.Read())
            {
                if ((i++) == CarIndex)
                {
                    CarId = Convert.ToInt32(reader["CarId"]);
                    for (int a = 2; a < reader.FieldCount; a++)
                    {
                        // This will give you the name of the current row's column
                        string columnName = reader.GetName(a);
                        // This will give you the value of the current row's column
                        string columnValue = reader[a].ToString();
                        string columnType = reader.GetDataTypeName(a);
                        if (columnType.Contains("REAL"))
                            writer.Write(float.Parse(columnValue));
                        else if (columnType.Contains("INT"))
                        {
                            if (columnValue != string.Empty)
                                writer.Write(int.Parse(columnValue));
                            else
                                writer.Write(-1);
                        }
                        else if (columnType.Contains("TEXT") && columnName == "Guid")
                            writer.WriteAsciiString(columnValue, columnValue.Length + 1);
                    }
                }
            }
            reader.Close();
            return ms.ToArray();
        }
        */
    }
    public class ForzaStringTable
    {
        private EndianReader Reader;

        public ForzaStringTable(EndianReader reader)
        {
            this.Reader = reader;
            this.ReadHeader();
        }
        public void ReadHeader()
        {
            if (Reader.ReadAsciiString(0x4) != "LSB2")
                throw new ForzaException("invalid StringTable header magic.");

            if (Reader.ReadInt32(EndianType.LittleEndian) != 0x01 || (Reader.ReadInt16() >> 8) != 0x02)
            {
                throw new ForzaException("invalid StringTable header values.");
            }

            Reader.BaseStream.Position += 0x2;
            int HeaderLen = Reader.ReadInt32();
            int TableEntryCount = Reader.ReadInt32();

            int StringTablePtr = Reader.ReadInt32();
            int StringHashTablePtr = Reader.ReadInt32();
            int ChangeLogTablePtr = Reader.ReadInt32();
            int FooterDataPtr = Reader.ReadInt32();

            int StringDataLen = Reader.ReadInt32();
            int StringDataEntryCount;
            int StringDataHeaderLen = ((StringDataEntryCount = Reader.ReadInt32()) + 1) * 6;

            Reader.BaseStream.Position = ((StringTablePtr + 8) + StringDataHeaderLen);

            TextWriter writer = new StreamWriter(@"C:\Users\Thierry\Desktop\Game Projects\Forza Motorsport 4\Saves\Cars\car_names.txt");
            for (int x = 0; x < StringDataEntryCount; x++)
            {
                writer.WriteLine(Reader.ReadUnicodeNullTermString());
            }
            writer.Close();
        }
    }
    internal class ForzaException : Exception
    {
        internal ForzaException(string message)
            : base("Forza: " + message)
        {

        }
    }
}