using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XboxDataBaseFile
{
    internal enum XUserDataTypes // for reference when handling data types
    {
        XUSER_DATA_TYPE_CONTEXT = 0,
        XUSER_DATA_TYPE_INT32 = 1,
        XUSER_DATA_TYPE_INT64 = 2,
        XUSER_DATA_TYPE_DOUBLE = 3,
        XUSER_DATA_TYPE_UNICODE = 4,
        XUSER_DATA_TYPE_FLOAT = 5,
        XUSER_DATA_TYPE_BINARY = 6,
        XUSER_DATA_TYPE_DATETIME = 7,
        XUSER_DATA_TYPE_NULL = 0xff,
    }
    internal enum XProfileGamercardZoneOptions
    {
        XPROFILE_GAMERCARD_ZONE_XBOX_1 = 0x0,
        XPROFILE_GAMERCARD_ZONE_RR = 0x1, // Recreation
        XPROFILE_GAMERCARD_ZONE_PRO = 0x2,
        XPROFILE_GAMERCARD_ZONE_FAMILY = 0x3,
        XPROFILE_GAMERCARD_ZONE_UNDERGROUND = 0x4,
    }

    public class SettingRecord
    {
        public uint cbData;
        public double dblData;
        public float fData;
        public byte[] fixedData;
        private DateTime ftData;
        public long i64Data;
        public int nData;
        public uint pbData;
        public uint settingId;
        public byte settingType;
        public byte[] varData;

        public uint Unk1;
        public byte[] Unk2;
        public byte[] Reserved;
        public SettingRecord()
        {
            this.settingId = 0;
            this.settingType = 0;
            this.fixedData = null;
            this.varData = null;
        }
        public SettingRecord(uint SettingId, byte SettingType)
        {
            this.settingId = SettingId;
            this.settingType = SettingType;
            this.Unk2 = new byte[7];
            this.varData = null;
            this.Reserved = new byte[4];
        }
        public bool Read(byte[] SettingData)
        {
            if (SettingData == null) return false;

            EndianReader reader = new EndianReader(new MemoryStream(SettingData, false), EndianType.BigEndian);
            this.settingId = reader.ReadUInt32();
            this.Unk1 = reader.ReadUInt32();
            this.settingType = reader.ReadByte();
            this.Unk2 = reader.ReadBytes(7);
            switch (this.settingType)
            {
                case 0:
                    this.nData = reader.ReadInt32();
                    this.Reserved = reader.ReadBytes(4);
                    break;

                case 1:
                    this.nData = reader.ReadInt32();
                    this.Reserved = reader.ReadBytes(4);
                    break;

                case 2:
                    this.i64Data = reader.ReadInt64();
                    break;

                case 3:
                    this.dblData = reader.ReadDouble();
                    break;

                case 4:
                    this.cbData = reader.ReadUInt32();
                    this.pbData = reader.ReadUInt32();
                    break;

                case 5:
                    this.fData = reader.ReadSingle();
                    this.Reserved = reader.ReadBytes(4);
                    break;

                case 6:
                    this.cbData = reader.ReadUInt32();
                    this.pbData = reader.ReadUInt32();
                    break;

                case 7:
                    this.i64Data = reader.ReadInt64();
                    this.ftData = new DateTime(this.i64Data);
                    break;

                default:
                    this.fixedData = reader.ReadBytes(8);
                    break;
            }
            this.varData = reader.ReadBytes((int)(reader.BaseStream.Length - reader.BaseStream.Position));
            reader.Close();
            return true;
        }
        public byte[] ToArray()
        {
            MemoryStream MS = new MemoryStream();
            EndianWriter ew = new EndianWriter(MS, EndianType.BigEndian);

            ew.Write(this.settingId);
            ew.Write(this.Unk1);
            ew.Write(this.settingType);
            ew.Write(this.Unk2);
            switch (this.settingType)
            {
                case 0:
                    ew.Write(this.nData);
                    ew.Write(this.Reserved);
                    break;

                case 1:
                    ew.Write(this.nData);
                    ew.Write(this.Reserved);
                    break;

                case 2:
                    ew.Write(this.i64Data);
                    break;

                case 3:
                    ew.Write(this.dblData);
                    break;

                case 4:
                    ew.Write(this.cbData);
                    ew.Write(this.pbData);
                    break;

                case 5:
                    ew.Write(this.fData);
                    ew.Write(this.Reserved);
                    break;

                case 6:
                    ew.Write(this.cbData);
                    ew.Write(this.pbData);
                    break;

                case 7:
                    ew.Write(i64Data);
                    break;

                default:
                    ew.Write(this.fixedData);
                    break;
            }
            if (varData != null && varData.Length > 0)
                ew.Write(varData);
            return MS.ToArray();
        }
    }
    public class SettingsTracker
    {
        public ProfileFile ProfileFile;
        public DataFile DataFile;

        public SettingsTracker(ProfileFile profileFile)
        {
            this.ProfileFile = profileFile;
            this.DataFile = profileFile.DataFile;
        }
        public SettingsTracker(DataFile dataFile)
        {
            this.DataFile = dataFile;
        }
        public void Read()
        {

        }
        public void CreateSetting(ulong SettingID, byte[] Setting)
        {
            this.DataFile.Upsert(new DataFileId()
            {
                Id = SettingID,
                Namespace = Namespace.SETTINGS
            }, Setting);
        }
        public byte[] ReadSetting(XProfileIds ID)
        {
            DataFileId id = new DataFileId()
            {
                Namespace = Namespace.SETTINGS,
                Id = (ulong)ID
            };
            DataFileEntry entry = this.DataFile.FindEntry(id);
            if (entry != null)
            {
                return this.DataFile.Read(entry);
            }
            return null;
        }
        public void WriteSetting(XProfileIds ID, byte[] Setting)
        {
            DataFileId id = new DataFileId()
            {
                Namespace = Namespace.SETTINGS,
                Id = (ulong)ID
            };
            DataFileEntry entry = this.DataFile.FindEntry(id);
            if (entry != null)
            {
                this.DataFile.Upsert(id, Setting);
            }
            else if (entry != null)
            {
                this.CreateSetting((ulong)ID, Setting);
            }
            this.UpdateSyncRecord(ID, this.ReadAndIncrementSyncInfo());
        }
        public void IncrementSetting(XProfileIds SettingID, object objValue)
        {
            int Value = Convert.ToInt32(objValue);
            SettingRecord rec = new SettingRecord();
            byte[] Data = this.ReadSetting(SettingID);
            if (Data != null)
                rec.Read(Data);
            else
            {
                rec.nData = 0;
                rec.settingType = 1;
                rec.settingId = (uint)SettingID;
                rec.Unk1 = new uint();
                rec.Unk2 = new byte[7];
                rec.Reserved = new byte[4];
            }
            rec.nData += (int)Value;
            this.DataFile.Upsert(new DataFileId()
            {
                Namespace = Namespace.SETTINGS,
                Id = (ulong)SettingID

            }, rec.ToArray());
        }
        public ulong ReadAndIncrementSyncInfo()
        {
            DataFileId id = new DataFileId()
            {
                Namespace = Namespace.SETTINGS,
                Id = ProfileFile.SyncInfoRecordId
            };

            SyncInfo record = new SyncInfo(new EndianReader(new MemoryStream(this.DataFile.ReadRecord(id)),
                EndianType.BigEndian));

            ulong SyncId = record.NextSync;

            record.NextSync++;

            this.DataFile.Upsert(id, record.ToArray());

            return SyncId;
        }
        public void UpdateSyncRecord(XProfileIds Id, ulong SyncId)
        {
            DataFileId id = new DataFileId()
            {
                Namespace = Namespace.SETTINGS,
                Id = ProfileFile.IndexRecordId
            };

            IndexRecord record = new IndexRecord((this.DataFile.SeekToRecord(this.DataFile.FindEntry(id))),
                this.DataFile.FindEntry(id).Size);

            record.SetLastRecord((ulong)Id, SyncId);

            this.DataFile.Upsert(id, record.ToArray());
        }
    }
}