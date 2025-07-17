using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace XboxDataBaseFile
{
    public enum BodyType : byte
    {
        Unknown = 0,
        Male = 1,
        Female = 2,
        Both = 3
    }
    public class AvatarAssetRecord
    {
        internal static uint XAVATAR_DETAILS_SHOWUNACHIEVED = 0x00000008;
        internal static uint XAVATAR_DETAILS_ACHIEVED_ONLINE = 0x00010000;
        internal static uint XAVATAR_DETAILS_ACHIEVED = 0x00020000;
        internal static uint XAVATAR_DETAILS_SHOWUNAWARDED = 0x00000008;

        public uint cb;
        public byte[] id;
        public ulong Id;
        public uint imageId;
        public uint flags;
        public DateTime dtAwarded;
        public ulong reserved;
        public BodyType BodyType
        {
            get
            {
                return (BodyType)(this.id[7] & 15);
            }
        }
        public string Name;
        public string Description;
        public string UnawardedText;
        public int TitleId;

        public bool AssetCollected
        {
            get
            {
                return (flags & XAVATAR_DETAILS_ACHIEVED) != 0 ? true : false;
            }
            set
            {
                if (value)
                {
                    flags = (flags & ~XAVATAR_DETAILS_ACHIEVED) | (XAVATAR_DETAILS_ACHIEVED | 0x100000);
                }
                else
                {
                    flags &= ~XAVATAR_DETAILS_ACHIEVED;
                }
            }
        }
        public bool AssetCollectedOnline
        {
            get
            {
                return (flags & XAVATAR_DETAILS_ACHIEVED_ONLINE) != 0 ? true : false;
            }
            set
            {
                if (value)
                {
                    flags = (flags & ~XAVATAR_DETAILS_ACHIEVED_ONLINE) | (XAVATAR_DETAILS_ACHIEVED_ONLINE | 0x100000);
                }
                else
                {
                    flags &= ~XAVATAR_DETAILS_ACHIEVED_ONLINE;
                }
            }
        }
        public bool AssetShowUnawarded
        {
            get
            {
                return (flags & XAVATAR_DETAILS_SHOWUNAWARDED) != 0 ? true : false;
            }
            set
            {
                if (value)
                {
                    flags = (flags & ~XAVATAR_DETAILS_SHOWUNAWARDED) | XAVATAR_DETAILS_SHOWUNAWARDED;
                }
                else
                {
                    flags &= ~XAVATAR_DETAILS_SHOWUNAWARDED;
                }
            }
        }
        public AvatarAssetRecord()
        {
            dtAwarded = DateTime.FromFileTime(0);
        }
        public AvatarAssetRecord(EndianReader reader, ulong Id)
        {
            this.Id = Id;
            this.cb = reader.ReadUInt32();
            this.id = reader.ReadBytes(0x10);

            this.imageId = reader.ReadUInt32();
            this.flags = reader.ReadUInt32();
            this.dtAwarded = DateTime.FromFileTime(reader.ReadInt64());
            this.reserved = reader.ReadUInt64();

            this.Name = reader.ReadUnicodeNullTermString();
            this.Description = reader.ReadUnicodeNullTermString();
            this.UnawardedText = reader.ReadUnicodeNullTermString();

            this.TitleId += id[15];
            this.TitleId += id[14] << 8;
            this.TitleId += id[13] << 0x10;
            this.TitleId += id[12] << 0x18;
        }
        public byte[] Serialize()
        {
            MemoryStream ms = new MemoryStream();
            EndianWriter ew = new EndianWriter(ms, EndianType.BigEndian);

            ew.Write(cb);
            ew.Write(id);
            ew.Write(imageId);
            ew.Write(flags);
            ew.Write(dtAwarded.ToFileTime());
            ew.Write(reserved);
            ew.WriteUnicodeNullTermString(Name);
            ew.WriteUnicodeNullTermString(Description);
            ew.WriteUnicodeNullTermString(UnawardedText);

            ew.Close();
            return ms.ToArray();
        }
    }
    public class AvatarAssetTracker
    {
        public List<AvatarAssetRecord> Awards;

        private EndianIO IO;
        private ProfileFile ProfileFile;
        private DataFile DataFile;
        private TitlePlayedRecord TitleRecord;
        public AvatarAssetTracker(ProfileFile profileFile, DataFile dataFile, TitlePlayedRecord titleRecord)
        {
            this.IO = dataFile.IO;
            this.ProfileFile = profileFile;
            this.DataFile = dataFile;
            this.TitleRecord = titleRecord;

            this.Awards = null;

            this.Read();
        }
        private void Read()
        {
            this.EnumFromDataFile();
        }
        private void EnumFromDataFile()
        {
            this.Awards = new List<AvatarAssetRecord>();
            List<DataFileRecord> Records = this.DataFile.FindDataEntries(Namespace.AVATAR);
            if (Records != null && Records.Count != 0)
            {
                for (var x = 0; x < Records.Count; x++)
                {
                    this.Awards.Add(new AvatarAssetRecord(this.DataFile.SeekToRecord(Records[x].Entry), Records[x].Id.Id));
                }
            }
        }
        public Image GetAssetTile(AvatarAssetRecord Asset) // retrieve the avatar assets's picture
        {
            DataFileEntry ent = this.DataFile.FindEntry(new DataFileId()
            {
                Namespace = Namespace.IMAGES,
                Id = Asset.imageId
            });
            if (ent != null)
            {
                return Image.FromStream(new MemoryStream(this.DataFile.Read(ent)));
            }
            return null;
        }

        public void UnlockAsset(AvatarAssetRecord Asset, bool CollectedOnline)
        {
            this.UnlockAsset(Asset, CollectedOnline, DateTime.Now);
        }
        public void UnlockAsset(AvatarAssetRecord Asset, bool CollectedOnline, DateTime dtCollected)
        {
            if (!Asset.AssetCollected)
            {
                if (CollectedOnline)
                {
                    Asset.dtAwarded = dtCollected; // set the date collected
                    Asset.AssetCollectedOnline = true;
                }

                Asset.AssetCollected = true;

                switch (Asset.BodyType)
                {
                    case BodyType.Female:
                        this.TitleRecord.FemaleAvatarAwards.Earned++;
                        break;
                    case BodyType.Male:
                        this.TitleRecord.MaleAvatarAwards.Earned++;
                        break;
                }

                this.TitleRecord.SetTitleListRecordInfo(0x10);

                this.TitleRecord.AllAvatarAwards.Earned++;

                this.WriteAsset(Asset);

                this.Sync(Asset.Id, ReadAndIncrementSyncInfo());
            }
        }
        private void WriteAsset(AvatarAssetRecord Asset)
        {
            this.DataFile.Upsert(new DataFileId() 
            {
                Namespace = Namespace.AVATAR,
                Id = Asset.Id
            }, Asset.Serialize());

            this.ProfileFile.DataFile.Upsert(new DataFileId() // update the profile title record
            {
                Namespace = Namespace.TITLES,
                Id = this.TitleRecord.TitleId
            }, this.TitleRecord.ToArray());
        }

        private ulong ReadAndIncrementSyncInfo()
        {
            DataFileId id = new DataFileId()
            {
                Namespace = Namespace.AVATAR,
                Id = XContent.PEC.SyncInfoRecordId
            };

            SyncInfo record = new SyncInfo(new EndianReader(new MemoryStream(this.DataFile.ReadRecord(id)),
                EndianType.BigEndian));

            ulong SyncId = record.NextSync;

            record.NextSync++;

            this.DataFile.Upsert(id, record.ToArray());

            return SyncId;
        }
        private void Sync(object Id, ulong SyncId)
        {
            DataFileId id = new DataFileId()
            {
                Namespace = Namespace.AVATAR,
                Id = XContent.PEC.IndexRecordId
            };

            DataFileEntry ent = this.DataFile.FindEntry(id);
            IndexRecord record = new IndexRecord((this.DataFile.SeekToRecord(ent)), ent.Size);

            record.SetLastRecord(Convert.ToUInt64(Id), SyncId);

            this.DataFile.Upsert(id, record.ToArray());

            id.Namespace = Namespace.TITLES;
            id.Id = ProfileFile.IndexRecordId;

            ent = this.ProfileFile.DataFile.FindEntry(id);
            record = new IndexRecord((this.ProfileFile.DataFile.SeekToRecord(ent)), ent.Size);

            SyncInfo ProfileSync = this.ProfileFile.ReadSyncInfo();

            record.SetLastRecord(this.TitleRecord.TitleId, ProfileSync);

            this.ProfileFile.WriteSyncInfo(ProfileSync);

            this.ProfileFile.DataFile.Upsert(id, record.ToArray());
        }

        public string FormatAssetFilename(AvatarAssetRecord Asset)
        {
            return string.Format("Content\\0000000000000000\\{0}\\00009000\\{1}", this.TitleRecord.TitleId.ToString("X"),
                Asset.id.ToHexString().ToUpper());
        }
    }
}