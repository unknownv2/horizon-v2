using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace XboxDataBaseFile
{
    public class AchievementRecord
    {
        public enum XAchievementType
        {
            XACHIEVEMENT_TYPE_COMPLETION = 1,
            XACHIEVEMENT_TYPE_LEVELING,
            XACHIEVEMENT_TYPE_UNLOCK,
            XACHIEVEMENT_TYPE_EVENT,
            XACHIEVEMENT_TYPE_TOURNAMENT,
            XACHIEVEMENT_TYPE_CHECKPOINT,
            XACHIEVEMENT_TYPE_OTHER
        }
        internal static uint XACHIEVEMENT_DETAILS_MASK_TYPE = 0x00000007;
        internal static uint XACHIEVEMENT_DETAILS_SHOWUNACHIEVED = 0x00000008;
        internal static uint XACHIEVEMENT_DETAILS_ACHIEVED_ONLINE = 0x00010000;
        internal static uint XACHIEVEMENT_DETAILS_ACHIEVED = 0x00020000;


        public uint cb;
        public uint cred; // cred means gamerscore
        public DateTime dateTimeAchieved;
        public uint flags;
        public uint id;
        public uint imageId;
        public string Label;
        public string Description;
        public string Unachieved;

        public XAchievementType AchievementType
        {
            get
            {
                return (XAchievementType)(flags & XACHIEVEMENT_DETAILS_MASK_TYPE);
            }
            set
            {
                flags = ((uint)value & ~XACHIEVEMENT_DETAILS_MASK_TYPE) | (uint)value;
            }
        }
        public bool AchievementEarned
        {
            get
            {
                return (flags & XACHIEVEMENT_DETAILS_ACHIEVED) != 0 ? true : false;
            }
            set
            {
                if (value)
                {
                    flags = (flags & ~XACHIEVEMENT_DETAILS_ACHIEVED) | (XACHIEVEMENT_DETAILS_ACHIEVED | 0x100000);
                }
                else
                {
                    flags &= ~XACHIEVEMENT_DETAILS_ACHIEVED;
                }
            }
        }
        public bool AchievementEarnedOnline
        {
            get
            {
                return (flags & XACHIEVEMENT_DETAILS_ACHIEVED_ONLINE) != 0 ? true : false;
            }
            set
            {
                if (value)
                {
                    flags = (flags & ~XACHIEVEMENT_DETAILS_ACHIEVED_ONLINE) | (XACHIEVEMENT_DETAILS_ACHIEVED_ONLINE | 0x100000);
                }
                else
                {
                    flags &= ~XACHIEVEMENT_DETAILS_ACHIEVED_ONLINE;
                }
            }
        }
        public bool AchievementShowUnachieved
        {
            get
            {
                return (flags & XACHIEVEMENT_DETAILS_SHOWUNACHIEVED) != 0 ? true : false;
            }
            set
            {
                if (value)
                {
                    flags = (flags & ~XACHIEVEMENT_DETAILS_SHOWUNACHIEVED) | XACHIEVEMENT_DETAILS_SHOWUNACHIEVED;
                }
                else
                {
                    flags &= ~XACHIEVEMENT_DETAILS_SHOWUNACHIEVED;
                }
            }
        }
        public AchievementRecord() 
        {
            cb = 0x1c;
            dateTimeAchieved = DateTime.FromFileTime(0);
        }
        public AchievementRecord(EndianReader reader)
        {
            this.cb = reader.ReadUInt32(); // size of binary data
            this.id = reader.ReadUInt32();
            this.imageId = reader.ReadUInt32();
            this.cred = reader.ReadUInt32();
            this.flags = reader.ReadUInt32();
            this.dateTimeAchieved = DateTime.FromFileTime(reader.ReadInt64());
            this.Label = reader.ReadUnicodeNullTermString();
            this.Description = reader.ReadUnicodeNullTermString();
            this.Unachieved = reader.ReadUnicodeNullTermString();
        }
        public byte[] ToArray()
        {
            MemoryStream ms = new MemoryStream();
            EndianWriter ew = new EndianWriter(ms, EndianType.BigEndian);

            ew.Write(this.cb);
            ew.Write(this.id);
            ew.Write(this.imageId);
            ew.Write(this.cred);
            ew.Write(this.flags);
            ew.Write(this.dateTimeAchieved.ToFileTime());
            ew.WriteUnicodeNullTermString(this.Label);
            ew.WriteUnicodeNullTermString(this.Description);
            ew.WriteUnicodeNullTermString(this.Unachieved);

            ew.Close();
            return ms.ToArray();
        }
    }
    public class AchievementTracker
    {
        public DataFile DataFile;
        public ProfileFile ProfileFile;
        public TitlePlayedRecord TitleRecord;
        private uint TitleId;

        public List<AchievementRecord> Achievements;
        public AchievementTracker(ProfileFile profileFile, DataFile dataFile, TitlePlayedRecord titleRecord)
        {
            this.DataFile = dataFile;
            this.ProfileFile = profileFile;
            this.TitleRecord = titleRecord;
            this.TitleId = titleRecord.TitleId;
        }
        public void Read()
        {
            this.EnumFromDataFile();
        }
        private void EnumFromDataFile() // load all achievements into a nice list
        {
            this.Achievements = new List<AchievementRecord>();
            List<DataFileRecord> Records = this.DataFile.FindDataEntries(Namespace.ACHIEVEMENTS);
            if (Records != null && Records.Count != 0)
            {
                for (var x = 0; x < Records.Count; x++)
                {
                    this.Achievements.Add(new AchievementRecord(this.DataFile.SeekToRecord(Records[x].Entry)));
                }
            }
        }
        public Image GetAchievementTile(AchievementRecord Achiev) // retrieve the achievement's picture
        {
            if (Achiev.AchievementEarned)
            {
                DataFileEntry ent = this.DataFile.FindEntry(new DataFileId()
                {
                    Namespace = Namespace.IMAGES,
                    Id = Achiev.imageId
                });
                if (ent != null)
                {
                    return Image.FromStream(new MemoryStream(this.DataFile.Read(ent)));
                }
            }
            return null;            
        }

        public bool IsTitleWriteable()
        {
            if (this.Achievements == null)
            {
                throw new Exception("AchievementTracker: Attempted to read achievements before loading them.");
            }

            uint CredTotal = 0;
            for (int x = 0; x < this.Achievements.Count; x++)
            {
                AchievementRecord record = this.Achievements[x];
                if (record.AchievementEarned)
                {
                    CredTotal += record.cred;
                }
            }

            if (CredTotal != this.TitleRecord.CredEarned)
            {
                return false;
            }

            return true;            
        }
        public void AddAchievement(AchievementRecord Achievement, object Id)
        {
            this.DataFile.Upsert(new DataFileId() // update the single achievement record
            {
                Namespace = Namespace.ACHIEVEMENTS,
                Id = Convert.ToUInt64(Id)
            }, Achievement.ToArray());
        }
 
        public void UnlockAchievement(AchievementRecord Achievement, bool EarnedOnline) // properly unlock an achievement 
        {
            UnlockAchievement(Achievement, EarnedOnline, DateTime.Now);
        }
        public void UnlockAchievement(AchievementRecord Achievement, bool EarnedOnline, DateTime TimeAchieved) // properly unlock an achievement 
        {
            if (EarnedOnline)
            {
                Achievement.dateTimeAchieved = TimeAchieved; // set the date unlocked
                Achievement.AchievementEarnedOnline = true;
                if (TimeAchieved.Ticks > this.TitleRecord.LastLoaded.Ticks)
                    this.TitleRecord.LastLoaded = TimeAchieved;
            }

            Achievement.AchievementEarned = true;

            if (this.TitleRecord.CredEarned + Achievement.cred <= this.TitleRecord.CredPossible
            && this.TitleRecord.AchievementsEarned + 1 <= this.TitleRecord.AchievementsPossible)
            {
                this.TitleRecord.CredEarned += Achievement.cred;
                this.TitleRecord.AchievementsEarned++;
            }
            else
            {
                throw new Exception("Achievement count and/or cred count was out of bounds.");
            }
            // update the achievement record and the title record
            this.WriteAchievement(Achievement);

            // update the necessary profile settings
            this.ProfileFile.SettingsTracker.IncrementSetting(XProfileIds.XPROFILE_GAMERCARD_ACHIEVEMENTS_EARNED, 1);
            this.ProfileFile.SettingsTracker.IncrementSetting(XProfileIds.XPROFILE_GAMERCARD_CRED, Achievement.cred);

            this.Sync(Achievement.id, ReadAndIncrementSyncInfo());
        }
        private void WriteAchievement(AchievementRecord Achievement) // update an achievement record along with its title record
        {
            this.DataFile.Upsert(new DataFileId() // update the single achievement record
            {
                Namespace = Namespace.ACHIEVEMENTS,
                Id = Achievement.id
            }, Achievement.ToArray());

            this.ProfileFile.DataFile.Upsert(new DataFileId() // update the profile title record
            {
                Namespace = Namespace.TITLES,
                Id = this.TitleId
            }, this.TitleRecord.ToArray());
        }

        public ulong ReadAndIncrementSyncInfo()
        {
            DataFileId id = new DataFileId()
            {
                Namespace = Namespace.ACHIEVEMENTS,
                Id = ProfileFile.SyncInfoRecordId
            };

            SyncInfo record = new SyncInfo(new EndianReader(new MemoryStream(this.DataFile.ReadRecord(id)),
                EndianType.BigEndian));

            ulong SyncId = record.NextSync;

            record.NextSync++;

            this.DataFile.Upsert(id, record.ToArray());

            return SyncId;
        }
        private void Sync(uint Id, ulong SyncId)
        {
            DataFileId id = new DataFileId()
            {
                Namespace = Namespace.ACHIEVEMENTS,
                Id = ProfileFile.IndexRecordId
            };

            DataFileEntry Entry = this.DataFile.FindEntry(id);
            IndexRecord record = new IndexRecord(this.DataFile.SeekToRecord(Entry), Entry.Size);

            record.SetLastRecord(Id, SyncId);

            this.DataFile.Upsert(id, record.ToArray());

            id.Namespace = Namespace.TITLES;

            Entry = this.ProfileFile.DataFile.FindEntry(id);
            record = new IndexRecord(this.ProfileFile.DataFile.SeekToRecord(Entry), Entry.Size);

            SyncInfo ProfileSync = this.ProfileFile.ReadSyncInfo();

            record.SetLastRecord(this.TitleRecord.TitleId, ProfileSync);

            this.ProfileFile.WriteSyncInfo(ProfileSync);

            this.ProfileFile.DataFile.Upsert(id, record.ToArray());
        }
    }
}