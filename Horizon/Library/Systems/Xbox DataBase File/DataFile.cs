using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace XboxDataBaseFile
{
    public enum Namespace : ushort
    {
        ACHIEVEMENTS = 1,
        IMAGES = 2,
        SETTINGS = 3,
        TITLES = 4,
        STRINGS = 5,
        AVATAR = 6
    }

    public class DataFileId 
    {
        public Namespace Namespace;
        public ulong Id;
        public DataFileId()
        {

        }
        public DataFileId(Namespace nameSpace, object id)
        {
            Namespace = nameSpace;
            Id = Convert.ToUInt64(id);
        }
        public byte[] ToArray()
        {
            MemoryStream ms = new MemoryStream();
            EndianWriter ew = new EndianWriter(ms, EndianType.BigEndian);

            ew.Write((ushort)Namespace);
            ew.Write(Id);

            ew.Close();
            return ms.ToArray();
        }
        public bool Equals(DataFileId p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.Id == p.Id) && (this.Namespace == p.Namespace);
        }
    }
    public class DataFileEntry
    {
        public uint Offset;
        public uint Size;
        public DataFileEntry() { }
        public DataFileEntry(object Offset, object Size)
        {
            this.Offset = Convert.ToUInt32(Offset);
            this.Size = Convert.ToUInt32(Size);
        }
        public bool Equals(DataFileEntry obj)
        {
            return (obj.Size == Size && obj.Offset == Offset);
        }
        public byte[] ToArray()
        {
            MemoryStream ms = new MemoryStream();
            EndianWriter ew = new EndianWriter(ms, EndianType.BigEndian);

            ew.Write(Offset);
            ew.Write(Size);

            ew.Close();
            return ms.ToArray();
        }
    }
    public class DataFileRecord
    {
        public DataFileId Id = new DataFileId();
        public DataFileEntry Entry = new DataFileEntry();
        public DataFileRecord() { }
        public DataFileRecord(DataFileId Id, DataFileEntry Entry)
        {
            this.Id = Id;
            this.Entry = Entry;
        }
        public DataFileRecord(EndianReader reader)
        {
            this.Id = new DataFileId(); this.Entry = new DataFileEntry();
            this.Id.Namespace = (Namespace)reader.ReadInt16();
            this.Id.Id = reader.ReadUInt64();
            this.Entry.Offset = (reader.ReadUInt32());
            this.Entry.Size = reader.ReadUInt32();
        }
        public byte[] ToArray()
        {
            MemoryStream ms = new MemoryStream();
            EndianWriter ew = new EndianWriter(ms, EndianType.BigEndian);
            ew.Write(this.Id.ToArray());
            ew.Write(this.Entry.ToArray());

            ew.Close();
            return ms.ToArray();
        }

    }
    public class SyncInfo
    {
        public ulong LastSync;
        public ulong NextSync;
        public DateTime ServerSyncTime;

        public SyncInfo()
        {
            this.NextSync = 1;
            ServerSyncTime = DateTime.FromFileTime(0);
        }
        public SyncInfo(EndianReader reader)
        {
            this.NextSync = reader.ReadUInt64();
            this.LastSync = reader.ReadUInt64();
            this.ServerSyncTime = DateTime.FromFileTime(reader.ReadInt64());
        }
        public byte[] ToArray()
        {
            MemoryStream ms = new MemoryStream();
            EndianWriter ew = new EndianWriter(ms, EndianType.BigEndian);

            ew.Write(this.NextSync);
            ew.Write(this.LastSync);
            ew.Write(this.ServerSyncTime.ToFileTime());

            ew.Close();
            return ms.ToArray();
        }
    }
    public class IndexRecord : IDisposable
    {
        public class SyncIndexRecord
        {
            public ulong Id;
            public ulong SyncId;
            public SyncIndexRecord(ulong Id, ulong SyncId)
            {
                this.Id = Id;
                this.SyncId = SyncId;
            }
            public SyncIndexRecord(EndianReader reader)
            {
                this.Id = reader.ReadUInt64();
                this.SyncId = reader.ReadUInt64();
            }
        }
        public List<SyncIndexRecord> Records;
        public IndexRecord()
        {
            this.Records = new List<SyncIndexRecord>();
        }
        public IndexRecord(EndianReader reader, uint DataSize)
        {
            this.Records = new List<SyncIndexRecord>();
            if (DataSize > 0)
            {
                do
                {
                    this.Records.Add(new SyncIndexRecord(reader));
                } while ((DataSize -= 0x10) > 0);
            }
        }
        private SyncIndexRecord FindRecord(ulong Id)
        {
            return Records.Find(delegate(SyncIndexRecord record)
            {
                return record.Id == Id;
            });
        }
        private int FindFreeRecord()
        {
            return Records.FindIndex(record => record.Id == 0);
        }
        public void SetLastRecord(object id, ulong SyncId)
        {
            ulong Id = Convert.ToUInt64(id);
            SyncIndexRecord rec = FindRecord(Id);
            if (rec != null)
            {
                this.Records.Remove(rec);
                rec.SyncId = SyncId;
                this.Records.Add(rec);
            }
            else if (FindFreeRecord() != -1)
            {
                int Index = FindFreeRecord();
                this.Records[Index].Id = Id;
                this.Records[Index].SyncId = SyncId;
            }
            else
            {
                this.Records.Add(new SyncIndexRecord(Id, SyncId));
            }
        }
        public void SetLastRecord(object id, SyncInfo SyncInfo)
        {
            ulong Id = Convert.ToUInt64(id);
            SyncIndexRecord rec = FindRecord(Id);
            if (rec != null)
            {
                this.Records.Remove(rec);
                if (rec.SyncId != (SyncInfo.NextSync - 1))
                {
                    rec.SyncId = SyncInfo.NextSync++;
                }
                this.Records.Add(rec);
            }
            else if (FindFreeRecord() != -1)
            {
                int Index = FindFreeRecord();
                this.Records[Index].Id = Id;
                this.Records[Index].SyncId = SyncInfo.NextSync++;
            }
            else
            {
                this.Records.Add(new SyncIndexRecord(Id, SyncInfo.NextSync++));
            }
        }
        public byte[] ToArray()
        {
            MemoryStream ms = new MemoryStream();
            EndianWriter ew = new EndianWriter(ms, EndianType.BigEndian);

            for (int x = 0; x < Records.Count; x++)
            {
                ew.Write(Records[x].Id);
                ew.Write(Records[x].SyncId);
            }

            ew.Close();
            return ms.ToArray();
        }
        public void Dispose()
        {
            this.Records.Clear();
        }
    }
    public class FreeRecord
    {
        public uint Offset;
        public int Size;
        public FreeRecord()
        {
            this.Offset = 0;
            this.Size = 0;
        }
        public FreeRecord(EndianReader reader)
        {
            this.Offset = reader.ReadUInt32();
            this.Size = reader.ReadInt32();
        }
        public byte[] ToArray()
        {
            MemoryStream ms = new MemoryStream();
            EndianWriter ew = new EndianWriter(ms, EndianType.BigEndian);

            ew.Write((this.Offset));
            ew.Write(this.Size);

            ew.Close();
            return ms.ToArray();
        }
    }
    public class TitleSetting : IDisposable
    {
        private bool disposed;

        public long Size
        {
            get { return this.Stream.Length;}
        }

        private MemoryStream Stream = new MemoryStream();
        private SettingRecord Record;

        public TitleSetting(uint SettingId, object DataLength ) 
        {
            Record = new SettingRecord(SettingId, 0x06);
            Record.cbData = Convert.ToUInt32(DataLength);
        }
        public TitleSetting(EndianReader reader)
        {
            Record = new SettingRecord();
            Record.Read(reader.ReadBytes(0x18));

            Stream.Write(reader.ReadBytes((int)Record.cbData), 0, (int)Record.cbData);
        }
        public void Inject(byte[] Data)
        {
            Stream.SetLength(Data.Length);
            Stream.Position = 0x00;
            Stream.Write(Data, 0, Data.Length);
            Record.cbData = (uint)Data.Length;
        }
        public byte[] ToArray()
        {
            var MS = new MemoryStream();
            var writer = new EndianWriter(MS, EndianType.BigEndian);
            writer.BaseStream.SetLength(Stream.Length + 0x18);

            writer.Write(Record.ToArray());
            writer.Write(Stream.ToArray());

            writer.Close();
            return MS.ToArray();
        }
        public byte[] GetBuffer()
        {
            return Stream.ToArray();
        }
        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.Stream.Close();
                }
            }
            disposed = true;
        }

        ~TitleSetting()
        {
            this.Dispose(false);
        }
    }
    public class FreeRecordComparer : IComparer<FreeRecord>
    {
        public int Compare(FreeRecord x, FreeRecord y)
        {
            return x.Offset.CompareTo(y.Offset);
        }
    }
    public class DataFileRecordComparer : IComparer<DataFileRecord>
    {
        public int Compare(DataFileRecord x, DataFileRecord y)
        {
            if (x.Id.Namespace == y.Id.Namespace)
            {
                return x.Id.Id.CompareTo(y.Id.Id);
            }
            else
            {
                return x.Id.Namespace.CompareTo(y.Id.Namespace);
            }
        }
    }
    public class DataFile : IDisposable
    {
        private bool disposed;

        public EndianIO IO;
        public uint DataStartOffset, FreeStartOffset;
        public int EntryCount
        {
            get
            {
                return this.DataFileRecords.Count;
            }
        }
        public UInt32 signature, version, reserved, MaxEntries, MaxFreeEntries, CurrentFreeEntries;
        public List<DataFileRecord> DataFileRecords;
        public List<FreeRecord> FreeRecords;

        private Namespace[] Namespaces = new Namespace[] {Namespace.ACHIEVEMENTS, 
            Namespace.IMAGES, Namespace.SETTINGS, Namespace.TITLES, Namespace.STRINGS, Namespace.AVATAR};
        public DataFile()
        {
            this.signature = 0x58444246;
            this.version = 0x0001;
            this.reserved = 0x0000;
            this.DataFileRecords = new List<DataFileRecord>();
            this.FreeRecords = new List<FreeRecord>();
        }
        public DataFile(EndianIO IO)
        {
            this.IO = IO;
            if (!this.IO.Opened)
                this.IO.Open();

            this.FreeRecords = null;
            this.DataFileRecords = null;

            //this.Read();
        }
        public void Read()
        {
            /*
             * Each entry is 18 bytes ( 0x12 ) and the entry table starts at 0x18 in a DataFile
            */

            // these values are static across the entire DataFile format
            const Int32 sign = 0x58444246, TableLocation = 0x18;

            this.IO.In.SeekTo(0);

            signature = this.IO.In.ReadUInt32();
            if (signature != sign)
                throw new DataFileException("Invalid Data File signature found.");

            version = this.IO.In.ReadUInt16();
            if (version != 0x01)
                throw new DataFileException("Invalid Data File version found.");

            reserved = this.IO.In.ReadUInt16();

            MaxEntries = this.IO.In.ReadUInt32();
            int _entryCount = this.IO.In.ReadInt32();

            MaxFreeEntries = this.IO.In.ReadUInt32();
            CurrentFreeEntries = this.IO.In.ReadUInt32();

            if (_entryCount > MaxEntries)
                throw new DataFileException("Invalid entry count found in the Data File max.");

            FreeStartOffset = 0x18 + (MaxEntries * 0x12);

            DataStartOffset = ((MaxFreeEntries + 3) << 3) + (MaxEntries * 0x12);

            this.IO.In.SeekTo(0);

            EndianReader reader = new EndianReader(new MemoryStream(this.IO.In.ReadBytes(this.DataStartOffset)), EndianType.BigEndian);

            this.DataFileRecords = new List<DataFileRecord>();

            reader.SeekTo(TableLocation);

            for (uint x = 0; x < _entryCount; x++)
            {
                this.DataFileRecords.Add(new DataFileRecord(reader));
            }

            this.FreeRecords = new List<FreeRecord>();

            reader.SeekTo(FreeStartOffset);

            for (var x = 0; x < CurrentFreeEntries; x++)
            {
                this.FreeRecords.Add(new FreeRecord(reader));
            }

            reader.Close();
        }
        private void WriteHeader()
        {
            this.FreeRecords.Sort(new FreeRecordComparer().Compare);
            uint FreeOffset = (uint)(this.IO.Stream.Length - this.DataStartOffset);
            this.FreeRecords.Last().Offset = FreeOffset;
            this.FreeRecords.Last().Size = ~(int)FreeOffset;

            this.IO.Out.SeekTo(0x08);
            this.IO.Out.Write(this.MaxEntries);
            this.IO.Out.Write(this.EntryCount);
            this.IO.Out.Write(this.MaxFreeEntries);
            this.IO.Out.Write(this.FreeRecords.Count);

            this.IO.Out.SeekTo(this.FreeStartOffset);
            this.IO.Out.Write(new byte[this.MaxFreeEntries * 8]);

            this.IO.Out.SeekTo(this.FreeStartOffset);

            for (var x = 0; x < this.FreeRecords.Count; x++)
            {
                this.IO.Out.Write(this.FreeRecords[x].ToArray());
            }

            this.IO.Out.SeekTo(0x18);
            this.IO.Out.Write(new byte[this.MaxEntries * 0x12]);

            this.IO.Out.SeekTo(0x18);

            List<DataFileRecord> Records = this.DataFileRecords;
            Records.Sort(new DataFileRecordComparer().Compare);

            for (var x = 0; x < this.EntryCount; x++)
            {
                this.IO.Out.Write(Records[x].ToArray());
            }
        }

        /* Base class functions */
        private DataFileRecord FindDFREntry(DataFileId tagId)
        {
            return this.DataFileRecords.Find(delegate(DataFileRecord dfr)
            {
                return dfr.Id.Equals(tagId);
            });
        }
        private int FindDFRIndex(DataFileId tagId)
        {
            return this.DataFileRecords.FindIndex(delegate(DataFileRecord dfr)
            {
                return dfr.Id.Equals(tagId);
            });
        }
        private DataFileRecord FindRecordEntry(DataFileId tagId)
        {
            try
            {
                return this.FindDFREntry(tagId);
            }
            catch
            {
                //System.Diagnostics.Debug.WriteLine("Warning : Could not find entry in datafile record dictionary.");
                //System.Diagnostics.Debug.WriteLine(string.Format("Exception caught is : {0}.", ex.ToString()));
            }
            return null;
        }
        public DataFileEntry FindEntry(DataFileId tagId)
        {
            try
            {
                DataFileRecord record = this.FindDFREntry(tagId);

                if (record != null)
                {
                    return record.Entry;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                //System.Diagnostics.Debug.WriteLine("Warning : Could not find entry in datafile record dictionary.");
                //System.Diagnostics.Debug.WriteLine(string.Format("Exception caught is : {0}.", ex.ToString()));
            }
            return null;
        }
        private void UpdateEntry(DataFileId tagId, object Offset, object Size)
        {
            DataFileRecord ent = this.FindDFREntry(tagId);
            if (ent == null)
            {
                ent = new DataFileRecord();
                ent.Id = tagId;
                this.DataFileRecords.Add(ent);
            }
            ent.Entry.Offset = Convert.ToUInt32(Offset);
            ent.Entry.Size = Convert.ToUInt32(Size);

            this.DataFileRecords[FindDFRIndex(tagId)] = ent;

            this.WriteHeader();
        }
        private int FreeEntryMatch(int DataSize)
        {
            return this.FreeRecords.FindIndex(delegate(FreeRecord rec)
            {
                return rec.Size >= DataSize;
            });
        }

        public void AddRecord(DataFileRecord record)
        {
            this.DataFileRecords.Add(record);

            this.WriteHeader();
        }
        public byte[] CopyToArray()
        {
            MemoryStream Ms = new MemoryStream();
            EndianIO IO = new EndianIO(Ms, EndianType.BigEndian, true);

            IO.Out.SeekTo(0);
            IO.Out.Write(this.signature);
            IO.Out.Write((ushort)this.version);
            IO.Out.Write((ushort)this.reserved);
            this.IO.In.SeekTo(0x08);
            IO.Out.Write(this.IO.In.ReadBytes(this.IO.Stream.Length - 0x08));

            IO.Close();

            return Ms.ToArray();
        }
        public byte[] ToArray()
        {
            this.WriteHeader();

            this.IO.Out.SeekTo(0);
            this.IO.Out.Write(this.signature);
            this.IO.Out.Write((ushort)this.version);
            this.IO.Out.Write((ushort)this.reserved);
            this.IO.In.SeekTo(0);
            return this.IO.In.ReadBytes(this.IO.Stream.Length);
        }
        /* Seeking(?) Section */
        public EndianReader SeekToRecord(DataFileEntry Record)
        {
            this.IO.SeekTo(Record.Offset + this.DataStartOffset);

            EndianReader reader = new EndianReader(new MemoryStream(this.IO.In.ReadBytes(Record.Size)), EndianType.BigEndian);

            return reader;
        }

        /* Read section */
        public byte[] ReadRecord(DataFileId tagId)
        {
            DataFileEntry ent = this.FindEntry(tagId);
            if (ent != null)
            {
                return this.Read(ent);
            }
            return null;
        }
        public byte[] Read(DataFileEntry tagEnt)
        {
            this.IO.In.SeekTo(tagEnt.Offset + this.DataStartOffset);
            return this.IO.In.ReadBytes(tagEnt.Size);
        }
        public byte[] ReadTitleSetting(DataFileId tagId)
        {
            var DataFileEntry = this.FindEntry(tagId);
            if (DataFileEntry != null)
            {
                this.IO.In.SeekTo(DataFileEntry.Offset + this.DataStartOffset);

                var Setting = new TitleSetting(new EndianReader(this.IO.In.ReadBytes(DataFileEntry.Size), EndianType.BigEndian));

                return Setting.GetBuffer();
            }
            else
            {
                throw new DataFileException(string.Format("attempted to read a non-existent title setting [0x{0:X8}] .", tagId.Id));
            }
        }
        public List<DataFileRecord> FindDataEntries(Namespace Namespace)
        {
            List<DataFileRecord> Records = new List<DataFileRecord>();
            DataFileId id = new DataFileId()
            {
                Namespace = Namespace,
                Id = Namespace == Namespace.AVATAR ? XContent.PEC.IndexRecordId : ProfileFile.IndexRecordId
            };
            DataFileEntry entry = this.FindEntry(id);
            if (entry != null)
            {
                IndexRecord record = new IndexRecord((this.SeekToRecord(entry)), entry.Size);

                for (var x = 0; x < record.Records.Count; x++)
                {
                    Records.Add(this.FindRecordEntry(new DataFileId() { Id = record.Records[x].Id, Namespace = Namespace }));
                }
            }
            return Records;        
        }

        /* Write section */
        public void Upsert(DataFileId tagId, byte[] tagBuffer)
        {
            this.UpdateRecord(tagId, tagBuffer);
        }
        public void WriteTitleSetting(DataFileId tagId, byte[] Data)
        {
            if (Data == null || Data.Length == 0x00)
            {
                this.DeleteTitleSetting(tagId);
            }
            else
            {
                var DataFileEntry = this.FindEntry(tagId);
                if (DataFileEntry != null)
                {
                    this.IO.In.SeekTo(DataFileEntry.Offset + this.DataStartOffset);

                    var Setting = new TitleSetting(new EndianReader(this.IO.In.ReadBytes(DataFileEntry.Size), EndianType.BigEndian));

                    Setting.Inject(Data);

                    this.UpdateRecord(tagId, Setting.ToArray());

                    Setting.Dispose();
                }
                else
                {
                    this.CreateTitleSetting(tagId.Id, Data);
                }
            }
        }
        public void CreateTitleSetting(ulong SettingId, byte[] SettingData)
        {
            if (SettingData == null || SettingData.Length == 0x00) throw new DataFileException("detected an invalid buffer while creating a new setting.");

            var tagId = new DataFileId() { Id = SettingId, Namespace = Namespace.SETTINGS };

            var titleSetting = new TitleSetting((uint)(SettingId & 0xFFFFFFFF), SettingData.Length);

            titleSetting.Inject(SettingData);

            this.UpdateRecord(tagId, titleSetting.ToArray());

            titleSetting.Dispose();
        }
        public void DeleteTitleSetting(DataFileId tagId)
        {
            var DataFileRecordEntry = FindRecordEntry(tagId);
            if (DataFileRecordEntry != null)
            {
                this.FreeRecords.Add(new FreeRecord()
                {
                    Offset = DataFileRecordEntry.Entry.Offset,
                    Size = (int)DataFileRecordEntry.Entry.Size
                });

                this.DataFileRecords.Remove(DataFileRecordEntry);

                this.WriteHeader();
            }
            else
            {
                //throw new DataFileException("attempted to delete an non-existant entry.");
            }
        }

        private void Insert(DataFileId tagId, byte[] tagData)
        {
            if (this.EntryCount + 1 > MaxEntries)
            {
                var dfc = new DataFileConstructor(this, MaxEntries + 0x200);
                var df = dfc.Initiate();

                this.IO.Out.SeekTo(0);
                this.IO.Out.Write(df.ToArray());

                this.DataFileRecords = df.DataFileRecords;
                this.FreeRecords = df.FreeRecords;

                dfc.Dispose();

                MaxEntries += 0x200;

                FreeStartOffset = 0x18 + (MaxEntries * 0x12);

                DataStartOffset = ((MaxFreeEntries + 3) << 3) + (MaxEntries * 0x12);
            }

            int Index = FreeEntryMatch(tagData.Length); // see if there is a freed entry with a matching data size

            if (Index != -1)
            {
                uint Offset = this.FreeRecords[Index].Offset;

                this.IO.Out.SeekTo(Offset + this.DataStartOffset);
                this.IO.Out.Write(tagData);
                int Size = this.FreeRecords[Index].Size;

                if (Size < tagData.Length)
                    throw new DataFileException("Invalid free entry was to be used.");

                if (Size > tagData.Length)
                {
                    this.FreeRecords[Index].Offset += (uint)tagData.Length;
                    this.FreeRecords[Index].Size = (int)(Size - tagData.Length);
                }
                else
                {
                    this.FreeRecords.RemoveAt(Index);
                }
                this.UpdateEntry(tagId, Offset, tagData.Length);
            }
            else
            {
                DataFileRecord dfr = this.FindDFREntry(tagId);
                if (dfr != null)
                {
                    this.DataFileRecords.Remove(dfr);
                }
                DataFileEntry ent = new DataFileEntry(this.IO.Stream.Length - this.DataStartOffset, tagData.Length);

                this.Write(ent, tagData);

                this.AddRecord(new DataFileRecord(tagId, ent));
            }
        }
        private void UpdateRecord(DataFileId tagId, byte[] tagData)
        {
            DataFileEntry ent = FindEntry(tagId);
            if (ent != null)
            {
                if (ent.Size >= tagData.Length)
                {
                    this.Write(ent, tagData);

                    if (ent.Size > tagData.Length)
                    {
                        this.FreeRecords.Add(new FreeRecord()
                        {
                            Offset = Convert.ToUInt32(ent.Offset + tagData.Length),
                            Size = Convert.ToInt32(ent.Size - tagData.Length)
                        });

                        this.UpdateEntry(tagId, ent.Offset, tagData.Length);

                        this.WriteHeader();
                    }
                }
                else
                {
                    if (ent.Size > 0)
                    {
                        this.FreeRecords.Add(new FreeRecord()
                        {
                            Offset = ent.Offset,
                            Size = (int)ent.Size
                        });
                    }
                    this.Insert(tagId, tagData);
                }
            }
            else
            {
                this.Insert(tagId, tagData);
            }
        }
        private void Write(DataFileEntry entry, byte[] Data)
        {
            this.IO.Out.SeekTo(entry.Offset + this.DataStartOffset);
            this.IO.Out.Write(Data);
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.DataFileRecords.Clear();
                    this.FreeRecords.Clear();
                    Array.Clear(Namespaces, 0, Namespaces.Length);
                }
                this.IO.Close();
            }
            disposed = true;
        }

        ~DataFile()
        {
            this.Dispose(false);
        }
    }
}