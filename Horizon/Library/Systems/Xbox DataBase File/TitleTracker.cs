using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Horizon.Server;
using XContent;

namespace XboxDataBaseFile
{
    public class TitlePlayedAdder
    {
        private ProfileFile ProfileFile;
        private DataFile DataFile;
        private List<uint> TitleList;
        private DataFile NewDataFile;
        private SettingsTracker SettingsTracker;
        public TitlePlayedAdder(ProfileFile profileFile)
        {
            this.ProfileFile = profileFile;
            this.DataFile = profileFile.DataFile;

            DataFileConstructor dfc = new DataFileConstructor(this.DataFile, this.DataFile.MaxEntries);
            this.NewDataFile = dfc.Initiate();
            this.SettingsTracker = new SettingsTracker(NewDataFile);

            this.TitleList = new List<uint>();
        }
        public void AddTitleToListCache(GameAdder.TitleMetaInfo title)
        {
            TitlePlayedRecord rec = new TitlePlayedRecord();
            rec.TitleId = uint.Parse(title.TID, System.Globalization.NumberStyles.HexNumber);
            rec.TitleName = title.TitleName;
            rec.CredPossible = Convert.ToUInt32(title.TotalCredit);
            rec.AchievementsPossible = Convert.ToUInt32(title.TotalAchievements);
            rec.ReservedAchievementCount = Convert.ToUInt16(title.TotalAchievements);
            rec.AllAvatarAwards.Possible = title.TotalAwards;
            rec.AllAvatarAwards.Earned = 0;
            rec.FemaleAvatarAwards.Possible = title.FemaleAwards;
            rec.FemaleAvatarAwards.Earned = 0;
            rec.MaleAvatarAwards.Possible = title.MaleAwards;
            rec.MaleAvatarAwards.Earned = 0;
            rec.CredEarned = 0;
            rec.AchievementsEarned = 0;
            rec.LastLoadedAsLong = 0;
            DataFileEntry tagEntry = new DataFileEntry(0, 0);

            DataFileId tagId = new DataFileId(Namespace.TITLES, rec.TitleId);

            this.NewDataFile.Upsert(tagId, rec.ToArray());

            this.TitleList.Add(rec.TitleId);
        }
        public void FlushTitleList()
        {
            DataFileId id = new DataFileId()
            {
                Namespace = Namespace.TITLES,
                Id = ProfileFile.IndexRecordId
            };

            DataFileEntry entry = this.DataFile.FindEntry(id);
            IndexRecord IndexRecord = null;
            if (entry == null)
            {
                IndexRecord = new IndexRecord();
            }
            else
            {
                // add the title to the index record
                IndexRecord = new IndexRecord((this.DataFile.SeekToRecord(entry)), entry.Size);
            }
            DataFileId syncId = new DataFileId()
            {
                Namespace = Namespace.TITLES,
                Id = ProfileFile.SyncInfoRecordId
            };

            SyncInfo SyncRecord = new SyncInfo(new EndianReader(new MemoryStream(this.DataFile.ReadRecord(syncId)),
                EndianType.BigEndian));

            for (var x = 0; x < this.TitleList.Count; x++)
            {
                IndexRecord.SetLastRecord(TitleList[x], SyncRecord.NextSync++);
            }

            this.SettingsTracker.IncrementSetting(XProfileIds.XPROFILE_GAMERCARD_TITLES_PLAYED, this.TitleList.Count);

            this.NewDataFile.Upsert(id, IndexRecord.ToArray());
            this.NewDataFile.Upsert(syncId, SyncRecord.ToArray());

            this.NewDataFile.IO.Stream.Flush();

            byte[] data = this.NewDataFile.ToArray();

            this.DataFile.IO.Stream.SetLength(data.Length);
            this.DataFile.IO.Out.SeekTo(0);
            this.DataFile.IO.Out.Write(data);

            this.DataFile.Read();
        }
    }
    public class TitleAdder
    {
        private StfsDevice XContentDevice;
        private StfsDevice PecDevice;
        private XContent.PEC PEC;
        public TitleAdder(StfsDevice xcontentDevice, XContent.PEC Pec)
        {
            this.XContentDevice = xcontentDevice;
            if (Pec != null)
            {
                this.PEC = Pec;
                this.PecDevice = Pec.StfsPec;
            }
        }
        public void AddTitle(uint TitleId, GameAdder.TitleTemplate Title)
        {
            if (Title != null)
            {
                string FileName = ProfileFile.FormatTitleIDToFilename(TitleId);
                // create the files
                if (Title.Tile != null)
                {
                    var io = new EndianIO(new StfsFileStream(XContentDevice, FileName, FileMode.CreateNew), EndianType.BigEndian);
   
                    this.BuildAchievementList(Title.Achievements, io, Title.Meta.TitleName, Title.Tile);
                }
                if (Title.Awards.Count > 0 && this.PecDevice != null)
                {
                    var io = new EndianIO(new StfsFileStream(PecDevice, FileName, FileMode.CreateNew), EndianType.BigEndian);

                    this.BuildAwardList(Title.Awards, io);
                }
            }
        }
        private void BuildAchievementList(List<GameAdder.TitleAchievement> Achievements, EndianIO IO, string Titlename, byte[] Tile)
        {
            var df = DataFileConstructor.Create(0x200);

            var indexRecord = new IndexRecord();
            var syncRecord = new SyncInfo();

            df.Upsert(new DataFileId()
            {
                Id = ProfileFile.SyncInfoRecordId,
                Namespace = Namespace.SETTINGS
            }, syncRecord.ToArray());

            if (Achievements.Count > 0)
            {
                df.Upsert(new DataFileId()
                {
                    Id = ProfileFile.IndexRecordId,
                    Namespace = Namespace.SETTINGS
                }, indexRecord.ToArray());

                uint LastSync = (uint)Achievements.Count;
                syncRecord.NextSync = LastSync + 1;
                for (var x = 0; x < Achievements.Count; x++)
                {
                    var record = new AchievementRecord();
                    var achiev = Achievements[x];

                    record.cred = (uint)achiev.Credit;
                    record.id = achiev.ID;
                    record.flags = achiev.Flags;
                    record.Label = achiev.AchievementName;
                    record.id = achiev.ID;

                    record.Description = achiev.UnlockedDescription;
                    record.Unachieved = record.AchievementShowUnachieved == true ? achiev.UnlockedDescription : achiev.LockedDescription;

                    df.Upsert(new DataFileId()
                    {
                        Namespace = Namespace.ACHIEVEMENTS,
                        Id = record.id
                    }, record.ToArray());

                    indexRecord.SetLastRecord(record.id, LastSync--);
                }
            }

            df.Upsert(new DataFileId()
            {
                Id = ProfileFile.SyncInfoRecordId,
                Namespace = Namespace.ACHIEVEMENTS
            }, syncRecord.ToArray());

            df.Upsert(new DataFileId()
            {
                Id = ProfileFile.IndexRecordId,
                Namespace = Namespace.ACHIEVEMENTS
            }, indexRecord.ToArray());

            if (Tile != null && Tile.Length > 0)
            {
                df.Upsert(new DataFileId()
                {
                    Namespace = Namespace.IMAGES,
                    Id = 0x0000000000008000
                }, Tile);
            }

            df.Upsert(new DataFileId()
            {
                Namespace = Namespace.STRINGS,
                Id = 0x0000000000008000
            }, UnicodeEncoding.BigEndianUnicode.GetBytes(Titlename + "\0"));


            IO.Open();
            IO.Out.SeekTo(0);
            IO.Out.Write(df.ToArray());
            IO.Close();

            df.Dispose();
        }
        private void BuildAwardList(List<GameAdder.TitleAward> Awards, EndianIO IO)
        {
            var df = DataFileConstructor.Create(0x24); // create a DataFile "skeleton"

            var indexRecord = new IndexRecord();
            var syncRecord = new SyncInfo();

            if (Awards.Count > 0)
            {
                uint LastSync = (uint)Awards.Count;
                syncRecord.NextSync = LastSync + 1;
                for (var x = 0; x < Awards.Count; x++)
                {
                    var asset = new AvatarAssetRecord();
                    asset.Id = Awards[x].Id;
                    asset.cb = Awards[x].cb;
                    asset.id = Awards[x].id;

                    asset.imageId = Awards[x].imageId;
                    asset.flags = Awards[x].flags;
                    asset.reserved = Awards[x].reserved;

                    asset.Name = Awards[x].Name;
                    asset.Description = Awards[x].Description;
                    asset.UnawardedText = Awards[x].UnawardedText;

                    df.Upsert(new DataFileId()
                    {
                        Namespace = Namespace.AVATAR,
                        Id = asset.Id
                    }, asset.Serialize());

                    indexRecord.SetLastRecord(asset.Id, LastSync--);
                }
            }
            df.Upsert(new DataFileId()
            {
                Id = XContent.PEC.SyncInfoRecordId,
                Namespace = Namespace.AVATAR
            }, syncRecord.ToArray());

            df.Upsert(new DataFileId()
            {
                Id = XContent.PEC.IndexRecordId,
                Namespace = Namespace.AVATAR
            }, indexRecord.ToArray());

            // inject datafile from memory and flush
            IO.Open();
            IO.Out.SeekTo(0);
            IO.Out.Write(df.ToArray());
            IO.Close();

            df.Dispose();
        }
    }
}