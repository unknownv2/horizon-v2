using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using XContent;
using System.CodeDom.Compiler;

namespace XboxDataBaseFile
{
    public enum XProfileIds
    {
        WEB_CONNECTION_SPEED = 0x1004200b,
        WEB_EMAIL_FORMAT = 0x10042000,
        WEB_FAVORITE_GAME = 0x10042004,
        WEB_FAVORITE_GAME1 = 0x10042005,
        WEB_FAVORITE_GAME2 = 0x10042006,
        WEB_FAVORITE_GAME3 = 0x10042007,
        WEB_FAVORITE_GAME4 = 0x10042008,
        WEB_FAVORITE_GAME5 = 0x10042009,
        WEB_FAVORITE_GENRE = 0x10042003,
        WEB_FLAGS = 0x10042001,
        WEB_FLASH = 0x1004200c,
        WEB_PLATFORMS_OWNED = 0x1004200a,
        WEB_SPAM = 0x10042002,
        WEB_VIDEO_PREFERENCE = 0x1004200d,
        XPROFILE_CRUX_BG_LARGE_PUBLIC = 0x406403fe,
        XPROFILE_CRUX_BG_SMALL_PUBLIC = 0x406403fd,
        XPROFILE_CRUX_BIO = 0x43e803fa,
        XPROFILE_CRUX_BKGD_IMAGE = 0x100403f3,
        XPROFILE_CRUX_LAST_CHANGE_TIME = 0x700803f4,
        XPROFILE_CRUX_MEDIA_MOTTO = 0x410003f6,
        XPROFILE_CRUX_MEDIA_PICTURE = 0x406403e8,
        XPROFILE_CRUX_MEDIA_STYLE1 = 0x100403ea,
        XPROFILE_CRUX_MEDIA_STYLE2 = 0x100403eb,
        XPROFILE_CRUX_MEDIA_STYLE3 = 0x100403ec,
        XPROFILE_CRUX_OFFLINE_ID = 0x603403f2,
        XPROFILE_CRUX_TOP_ALBUM1 = 0x100403ed,
        XPROFILE_CRUX_TOP_ALBUM2 = 0x100403ee,
        XPROFILE_CRUX_TOP_ALBUM3 = 0x100403ef,
        XPROFILE_CRUX_TOP_ALBUM4 = 0x100403f0,
        XPROFILE_CRUX_TOP_ALBUM5 = 0x100403f1,
        XPROFILE_CRUX_TOP_MEDIAID1 = 0x601003f7,
        XPROFILE_CRUX_TOP_MEDIAID2 = 0x601003f8,
        XPROFILE_CRUX_TOP_MEDIAID3 = 0x601003f9,
        XPROFILE_CRUX_TOP_MUSIC = 0x60a803f5,
        XPROFILE_FRIENDSAPP_SHOW_BUDDIES = 0x1004003e,
        XPROFILE_GAMER_ACTION_AUTO_AIM = 0x10040022,
        XPROFILE_GAMER_ACTION_AUTO_CENTER = 0x10040023,
        XPROFILE_GAMER_ACTION_MOVEMENT_CONTROL = 0x10040024,
        XPROFILE_GAMER_CONTROL_SENSITIVITY = 0x10040018,
        XPROFILE_GAMER_DIFFICULTY = 0x10040015,
        XPROFILE_GAMER_PREFERRED_COLOR_FIRST = 0x1004001d,
        XPROFILE_GAMER_PREFERRED_COLOR_SECOND = 0x1004001e,
        XPROFILE_GAMER_PRESENCE_USER_STATE = 0x10040007,
        XPROFILE_GAMER_RACE_ACCELERATOR_CONTROL = 0x10040029,
        XPROFILE_GAMER_RACE_BRAKE_CONTROL = 0x10040028,
        XPROFILE_GAMER_RACE_CAMERA_LOCATION = 0x10040027,
        XPROFILE_GAMER_RACE_TRANSMISSION = 0x10040026,
        XPROFILE_GAMER_TIER = 0x1004003a, // obsolete, any attemp to write to it will generate an exception
        XPROFILE_GAMER_TYPE = 0x10040001,
        XPROFILE_GAMER_LAST_SUBSCRIPTION_DATE = 0x70080049,
        XPROFILE_GAMER_YAXIS_INVERSION = 0x10040002,
        XPROFILE_GAMERCARD_ACHIEVEMENTS_EARNED = 0x10040013,
        XPROFILE_GAMERCARD_AVATAR_INFO_1 = 0x63e80044,
        XPROFILE_GAMERCARD_AVATAR_INFO_2 = 0x63e80045,
        XPROFILE_GAMERCARD_CRED = 0x10040006,
        XPROFILE_GAMERCARD_HAS_VISION = 0x10040008,
        XPROFILE_GAMERCARD_MOTTO = 0x402c0011,
        XPROFILE_GAMERCARD_PARTY_INFO = 0x60800046,
        XPROFILE_GAMERCARD_PERSONAL_PICTURE = 0x40640010,
        XPROFILE_GAMERCARD_PICTURE_KEY = 0x4064000f,
        XPROFILE_GAMERCARD_REGION = 0x10040005,
        XPROFILE_GAMERCARD_REP = 0x5004000b,
        XPROFILE_GAMERCARD_SERVICE_TYPE_FLAGS = 0x1004003f,
        XPROFILE_GAMERCARD_TITLE_ACHIEVEMENTS_EARNED = 0x10040039,
        XPROFILE_GAMERCARD_TITLE_CRED_EARNED = 0x10040038,
        XPROFILE_GAMERCARD_TITLES_PLAYED = 0x10040012,
        XPROFILE_GAMERCARD_USER_BIO = 0x43e80043,
        XPROFILE_GAMERCARD_USER_LOCATION = 0x40520041,
        XPROFILE_GAMERCARD_USER_NAME = 0x41040040,
        XPROFILE_GAMERCARD_USER_URL = 0x41900042,
        XPROFILE_GAMERCARD_ZONE = 0x10040004,
        XPROFILE_GAMERCARD_TENURE_LEVEL = 0x10040047,
        XPROFILE_GAMERCARD_TENURE_MILESTONE = 0x10040048,
        XPROFILE_MESSENGER_AUTO_SIGNIN = 0x1004003c,
        XPROFILE_MESSENGER_SIGNUP_STATE = 0x1004003b,
        XPROFILE_OPTION_CONTROLLER_VIBRATION = 0x10040003,
        XPROFILE_OPTION_VOICE_MUTED = 0x1004000c,
        XPROFILE_OPTION_VOICE_THRU_SPEAKERS = 0x1004000d,
        XPROFILE_OPTION_VOICE_VOLUME = 0x1004000e,
        XPROFILE_PERMISSIONS = 0x10040000,
        XPROFILE_SAVE_WINDOWS_LIVE_PASSWORD = 0x1004003d,
        XPROFILE_TITLE_SPECIFIC1 = 0x63e83fff,
        XPROFILE_TITLE_SPECIFIC2 = 0x63e83ffe,
        XPROFILE_TITLE_SPECIFIC3 = 0x63e83ffd,
    }
    public struct XAvatarAwardsCounter
    {
        public byte Earned;
        public byte Possible;
    }
    public class TitlePlayedRecord
    {
        public uint TitleId;
        public uint AchievementsPossible;
        public uint AchievementsEarned;
        public uint CredPossible;
        public uint CredEarned;
        public ushort ReservedAchievementCount;
        public XAvatarAwardsCounter AllAvatarAwards;
        public XAvatarAwardsCounter MaleAvatarAwards;
        public XAvatarAwardsCounter FemaleAvatarAwards;
        public uint ReservedFlags;
        public long LastLoadedAsLong;
        public DateTime LastLoaded
        {
            get
            {
                return DateTime.FromFileTime(LastLoadedAsLong);
            }
            set
            {
                LastLoadedAsLong = value.ToFileTime();
            }
        }
        public string TitleName;
        public TitlePlayedRecord()
        {

        }
        public TitlePlayedRecord(EndianReader reader)
        {
            this.TitleId = reader.ReadUInt32();
            this.AchievementsPossible = reader.ReadUInt32();
            this.AchievementsEarned = reader.ReadUInt32();
            this.CredPossible = reader.ReadUInt32();
            this.CredEarned = reader.ReadUInt32();
            this.ReservedAchievementCount = reader.ReadUInt16();
            this.AllAvatarAwards.Earned = reader.ReadByte();
            this.AllAvatarAwards.Possible = reader.ReadByte();
            this.MaleAvatarAwards.Earned = reader.ReadByte();
            this.MaleAvatarAwards.Possible = reader.ReadByte();
            this.FemaleAvatarAwards.Earned = reader.ReadByte();
            this.FemaleAvatarAwards.Possible = reader.ReadByte();
            this.ReservedFlags = reader.ReadUInt32();
            this.LastLoadedAsLong = reader.ReadInt64();
            this.TitleName = reader.ReadUnicodeNullTermString();
        }
        public void SetTitleListRecordInfo(uint Flag)
        {
            this.ReservedFlags |= Flag;
        }
        public byte[] ToArray()
        {
            MemoryStream ms = new MemoryStream();
            EndianWriter ew = new EndianWriter(ms, EndianType.BigEndian);

            ew.Write(this.TitleId);
            ew.Write(this.AchievementsPossible);
            ew.Write(this.AchievementsEarned);
            ew.Write(this.CredPossible);
            ew.Write(this.CredEarned);
            ew.Write(this.ReservedAchievementCount);
            ew.Write(this.AllAvatarAwards.Earned);
            ew.Write(this.AllAvatarAwards.Possible);
            ew.Write(this.MaleAvatarAwards.Earned);
            ew.Write(this.MaleAvatarAwards.Possible);
            ew.Write(this.FemaleAvatarAwards.Earned);
            ew.Write(this.FemaleAvatarAwards.Possible);
            ew.Write(this.ReservedFlags);
            ew.Write(this.LastLoadedAsLong);
            ew.WriteUnicodeNullTermString(TitleName);

            ew.Close();
            return ms.ToArray();
        }
    }
    public class ProfileFile : IDisposable
    {
        public static ulong SyncInfoRecordId = 0x0200000000;
        public static ulong IndexRecordId = 0x0100000000;
        private bool disposed = false;

        public EndianIO IO;
        public DataFile DataFile;
        public SettingsTracker SettingsTracker;
        public AchievementTracker AchievementTracker;
        private XContentPackage Package;

        public ProfileFile(XContentPackage Package, uint ProfileFileID)
        {
            this.Package = Package;

            this.IO = new EndianIO(Package.StfsContentPackage.GetFileStream(
                FormatTitleIDToFilename(ProfileFileID)), EndianType.BigEndian);

            if(!this.IO.Opened)
                this.IO.Open();
        }
        public void Read()
        {
            this.DataFile = new DataFile(this.IO);
            this.DataFile.Read();

            this.SettingsTracker = new SettingsTracker(this);
        }
        public SyncInfo ReadSyncInfo()
        {
            DataFileId id = new DataFileId()
            {
                Namespace = Namespace.TITLES,
                Id = ProfileFile.SyncInfoRecordId
            };

            SyncInfo record = new SyncInfo(new EndianReader(new MemoryStream(this.DataFile.ReadRecord(id)),
                EndianType.BigEndian));

            return record;
        }
        public void WriteSyncInfo(SyncInfo SyncInfo)
        {
            DataFileId id = new DataFileId()
            {
                Namespace = Namespace.TITLES,
                Id = ProfileFile.SyncInfoRecordId
            };

            this.DataFile.Upsert(id, SyncInfo.ToArray());
        }
        public static string FormatTitleIDToFilename(uint TitleId)
        {
            return string.Format("{0:X8}.gpd", TitleId);
        }
        public DataFileRecord FindEntryFromTitleId(List<DataFileRecord> Records, ulong TitleId)
        {
            return Records.Find(delegate(DataFileRecord record)
            {
                return record.Id.Id == TitleId;
            });
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DataFile.Dispose();
                }

                this.IO.Close();
            }
            disposed = true;
        }
        ~ProfileFile()
        {
            this.Dispose(false);
        }
    }
}