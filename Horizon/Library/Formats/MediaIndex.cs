using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Horizon.Library.Formats
{
    public enum XMIOBJECTTYPE
    {
        XMIOBJECTTYPE_INVALID = 0x0,
        XMIOBJECTTYPE_FREE = 0x1,
        XMIOBJECTTYPE_MUSIC_TRACK = 0x2,
        XMIOBJECTTYPE_MUSIC_ALBUM = 0x3,
        XMIOBJECTTYPE_MUSIC_ARTIST = 0x4,
        XMIOBJECTTYPE_MUSIC_GENRE = 0x5,
        XMIOBJECTTYPE_MUSIC_PLAYLIST = 0x6,
        XMIOBJECTTYPE_HEADER = 0x7,
        XMIOBJECTTYPE_MASTERLIST = 0x8,
        XMIOBJECTTYPE_MUSIC_PLAYLIST_ENTRY = 0x9,
        XMIOBJECTTYPE_COUNT = 0xA
    }

    public struct XMIENTRY_HEADER
    {
        public static uint FieldSize = 24;

        public XMIOBJECTTYPE nObjectType;
        public uint dwSignature;
        public uint dwVersion;
        public XMILISTHEAD MasterListList;

        public XMIENTRY_HEADER(EndianReader reader)
        {
            this.nObjectType = (XMIOBJECTTYPE)reader.ReadUInt32();

            this.dwSignature = reader.ReadUInt32();

            if (this.dwSignature != 0x20494D58)
            {
                throw new XmiException("Invalid signature.");
            }

            this.dwVersion = reader.ReadUInt32();

            if (this.dwVersion != 0x02)
            {
                throw new XmiException("Invalid version.");
            }

            this.MasterListList = new XMILISTHEAD(reader);
        }
        public byte[] ToArray()
        {
            MemoryStream MS = new MemoryStream();
            EndianWriter writer = new EndianWriter(MS, EndianType.BigEndian);

            writer.Write((uint)this.nObjectType);
            writer.Write(this.dwSignature);
            writer.Write(this.dwVersion);
            writer.Write(this.MasterListList.ToArray());

            writer.Close();

            return MS.ToArray();
        }
    }
    public struct XMILISTHEAD
    {
        public uint nPrevEntry;
        public uint nNextEntry;
        public uint nEntryCount;

        public XMILISTHEAD(EndianReader reader)
        {
            this.nPrevEntry = reader.ReadUInt32();
            this.nNextEntry = reader.ReadUInt32();
            this.nEntryCount = reader.ReadUInt32();
        }
        public byte[] ToArray()
        {
            MemoryStream MS = new MemoryStream();
            EndianWriter writer = new EndianWriter(MS, EndianType.BigEndian);

            writer.Write(this.nPrevEntry);
            writer.Write(this.nNextEntry);
            writer.Write(this.nEntryCount);

            writer.Close();

            return MS.ToArray();
        }
    }
    public struct XMILISTENTRY
    {
        public uint nPrevEntry;
        public uint nNextEntry;
        public uint nParentEntry;

        public XMILISTENTRY(EndianReader reader)
        {
            this.nPrevEntry = reader.ReadUInt32();
            this.nNextEntry = reader.ReadUInt32();
            this.nParentEntry = reader.ReadUInt32();
        }
        public byte[] ToArray()
        {
            MemoryStream MS = new MemoryStream();
            EndianWriter writer = new EndianWriter(MS, EndianType.BigEndian);

            writer.Write(this.nPrevEntry);
            writer.Write(this.nNextEntry);
            writer.Write(this.nParentEntry);

            writer.Close();

            return MS.ToArray();
        }
    }
    public struct XMIENTRY_MASTERLIST
    {
        public static uint FieldSize = 32;

        public XMIOBJECTTYPE nObjectType;
        public XMILISTENTRY MasterListEntry;
        public XMILISTHEAD EntryList;
        public XMIOBJECTTYPE nChildObjectType;

        public XMIENTRY_MASTERLIST(EndianReader reader)
        {
            this.nObjectType = (XMIOBJECTTYPE)reader.ReadUInt32();
            this.MasterListEntry = new XMILISTENTRY(reader);
            this.EntryList = new XMILISTHEAD(reader);
            this.nChildObjectType = (XMIOBJECTTYPE)reader.ReadUInt32();
        }
        public byte[] ToArray()
        {
            MemoryStream MS = new MemoryStream();
            EndianWriter writer = new EndianWriter(MS, EndianType.BigEndian);

            writer.Write((uint)this.nObjectType);
            writer.Write(this.MasterListEntry.ToArray());
            writer.Write(this.EntryList.ToArray());
            writer.Write((uint)nChildObjectType);

            writer.Close();

            return MS.ToArray();
        }

    }
    public struct XMIENTRY_MUSIC_TRACK
    {
        public static uint FieldSize = 148;

        public XMIOBJECTTYPE nObjectType;
        public XMILISTENTRY MasterListEntry;
        public string szName;
        public XMILISTENTRY Album;
        public XMILISTENTRY Artist;
        public XMILISTENTRY Genre;
        public XMILISTHEAD PlaylistEntryList;
        public uint dwDuration
        {
            get { return (dwDurationAndTrackNumber >> 9) & 0x007FFFFF; }
            set { dwDurationAndTrackNumber = (dwDurationAndTrackNumber & 0x1FF) | (value & 0x007FFFFF) << 9; }
        }
        public uint dwTrackNumber
        {
            get { return (dwDurationAndTrackNumber >> 2) & 0x7F; }
            set { dwDurationAndTrackNumber = (dwDurationAndTrackNumber & 0xFFFFFF03) | (value & 0x7F) << 2; }
        }
        public uint dwFormat
        {
            get { return dwDurationAndTrackNumber & 0x03; }
            set { dwDurationAndTrackNumber = (dwDurationAndTrackNumber & 0xFFFFFFFC) | (value & 0x03); }
        }
        public uint dwDurationAndTrackNumber;
        public uint EntryIndex;

        public XMIENTRY_MUSIC_TRACK(EndianReader reader)
        {
            this.nObjectType = (XMIOBJECTTYPE)reader.ReadUInt32();
            this.MasterListEntry = new XMILISTENTRY(reader);
            this.szName = reader.ReadUnicodeString(0x28);
            this.Album = new XMILISTENTRY(reader);
            this.Artist = new XMILISTENTRY(reader);
            this.Genre = new XMILISTENTRY(reader);
            this.PlaylistEntryList = new XMILISTHEAD(reader);
            this.dwDurationAndTrackNumber = reader.ReadUInt32();
            this.EntryIndex = 0xffffffff;
        }
    }
    public struct XMIENTRY_MUSIC_ALBUM
    {
        public static uint FieldSize = 376;

        public XMIOBJECTTYPE nObjectType;
        public XMILISTENTRY MasterListEntry;
        public string szName;
        public XMILISTHEAD TrackList;
        public XMILISTENTRY Artist;
        public XMILISTENTRY Genre;
        public uint dwReleaseYear;
        public byte[] AlbumId;
        public uint EntryIndex;

        public XMIENTRY_MUSIC_ALBUM(EndianReader reader)
        {
            this.nObjectType = (XMIOBJECTTYPE)reader.ReadUInt32();
            this.MasterListEntry = new XMILISTENTRY(reader);
            this.szName = reader.ReadUnicodeString(0x28);
            this.TrackList = new XMILISTHEAD(reader);
            this.Artist = new XMILISTENTRY(reader);
            this.Genre = new XMILISTENTRY(reader);
            this.dwReleaseYear = reader.ReadUInt32();
            this.AlbumId = reader.ReadBytes(20);
            this.EntryIndex = 0xffffffff;
        }
    }
    public struct XMIENTRY_MUSIC_ARTIST
    {
        public XMIOBJECTTYPE nObjectType;
        public XMILISTENTRY MasterListEntry;
        public string szName;
        public XMILISTHEAD TrackList;
        public XMILISTHEAD AlbumList;
        public uint EntryIndex;

        public XMIENTRY_MUSIC_ARTIST(EndianReader reader)
        {
            this.nObjectType = (XMIOBJECTTYPE)reader.ReadUInt32();
            this.MasterListEntry = new XMILISTENTRY(reader);
            this.szName = reader.ReadUnicodeString(0x28);
            this.TrackList = new XMILISTHEAD(reader);
            this.AlbumList = new XMILISTHEAD(reader);
            this.EntryIndex = 0xffffffff;
        }
    }
    public struct XMIENTRY_MUSIC_GENRE
    {
        public XMIOBJECTTYPE nObjectType;
        public XMILISTENTRY MasterListEntry;
        public string szName;
        public XMILISTHEAD TrackList;
        public XMILISTHEAD AlbumList;
        public uint EntryIndex;

        public XMIENTRY_MUSIC_GENRE(EndianReader reader)
        {
            this.nObjectType = (XMIOBJECTTYPE)reader.ReadUInt32();
            this.MasterListEntry = new XMILISTENTRY(reader);
            this.szName = reader.ReadUnicodeString(0x28);
            this.TrackList = new XMILISTHEAD(reader);
            this.AlbumList = new XMILISTHEAD(reader);
            this.EntryIndex = 0xffffffff;
        }
    }
    public struct XMIENTRY_MUSIC_PLAYLIST
    {
        public XMIOBJECTTYPE nObjectType;
        public XMILISTENTRY MasterListEntry;
        public string szName;
        public XMILISTHEAD PlaylistEntryList;
        public uint EntryIndex;

        public XMIENTRY_MUSIC_PLAYLIST(EndianReader reader)
        {
            this.nObjectType = (XMIOBJECTTYPE)reader.ReadUInt32();
            this.MasterListEntry = new XMILISTENTRY(reader);
            this.szName = reader.ReadUnicodeString(0x28);
            this.PlaylistEntryList = new XMILISTHEAD(reader);
            this.EntryIndex = 0xffffffff;
        }
    }

    public class MediaIndex
    {
        private static uint XMIENTRYSIZE = 600;
        private EndianIO IO;

        public XMIENTRY_HEADER Header;
        public XMIENTRY_MASTERLIST[] MasterList;

        public List<XMIENTRY_MUSIC_ALBUM> Albums;
        public List<XMIENTRY_MUSIC_TRACK> Tracks;
        public List<XMIENTRY_MUSIC_ARTIST> Artists;
        public List<XMIENTRY_MUSIC_GENRE> Genres;
        public List<XMIENTRY_MUSIC_PLAYLIST> Playlists;

        public MediaIndex(string FileName) : this(new EndianIO(FileName, EndianType.BigEndian, true))
        {

        }
        public MediaIndex(EndianIO IO)
        {
            this.IO = IO;

            if (!this.IO.Opened)
                this.IO.Open();

            this.Validate();
            this.CreateMasterListEntries();
            this.ReadEntries();
        }

        private void Validate()
        {
            this.Header = new XMIENTRY_HEADER(this.IO.In);
            this.MasterList = new XMIENTRY_MASTERLIST[this.Header.MasterListList.nEntryCount];
        }

        private void CreateMasterListEntries()
        {
            for (var x = 0; x < this.Header.MasterListList.nEntryCount; x++)
            {
                this.IO.In.SeekTo( (x + 1) * XMIENTRYSIZE);
                this.MasterList[x] = new XMIENTRY_MASTERLIST(this.IO.In);
            }
        }

        private void ReadEntries()
        {
            for (var x = 0; x < this.Header.MasterListList.nEntryCount; x++)
            {
                XMIENTRY_MASTERLIST Entry = this.MasterList[x];
                uint NextEntry = Entry.EntryList.nNextEntry;

                switch (Entry.nChildObjectType)
                {
                    case XMIOBJECTTYPE.XMIOBJECTTYPE_MUSIC_TRACK:

                        this.Tracks = new List<XMIENTRY_MUSIC_TRACK>();
                        for (var i = 0; i < Entry.EntryList.nEntryCount; i++)
                        {
                            this.IO.In.SeekTo(NextEntry * XMIENTRYSIZE);

                            XMIENTRY_MUSIC_TRACK Track = new XMIENTRY_MUSIC_TRACK(this.IO.In);
                            Track.EntryIndex = NextEntry;
                            NextEntry = Track.MasterListEntry.nNextEntry;

                            this.Tracks.Add(Track);
                        }
                        break;
                    case XMIOBJECTTYPE.XMIOBJECTTYPE_MUSIC_ALBUM:

                        this.Albums = new List<XMIENTRY_MUSIC_ALBUM>();

                        for (var i = 0; i < Entry.EntryList.nEntryCount; i++)
                        {
                            this.IO.In.SeekTo(NextEntry * XMIENTRYSIZE);

                            XMIENTRY_MUSIC_ALBUM Album = new XMIENTRY_MUSIC_ALBUM(this.IO.In);
                            Album.EntryIndex = NextEntry;
                            NextEntry = Album.MasterListEntry.nNextEntry;

                            this.Albums.Add(Album);
                        }
                        break;
                    case XMIOBJECTTYPE.XMIOBJECTTYPE_MUSIC_ARTIST:

                        this.Artists = new List<XMIENTRY_MUSIC_ARTIST>();
                        for (var i = 0; i < Entry.EntryList.nEntryCount; i++)
                        {
                            this.IO.In.SeekTo(NextEntry * XMIENTRYSIZE);

                            XMIENTRY_MUSIC_ARTIST Artist = new XMIENTRY_MUSIC_ARTIST(this.IO.In);
                            Artist.EntryIndex = NextEntry;
                            NextEntry = Artist.MasterListEntry.nNextEntry;

                            this.Artists.Add(Artist);
                        }
                        break;
                    case XMIOBJECTTYPE.XMIOBJECTTYPE_MUSIC_GENRE:

                        this.Genres = new List<XMIENTRY_MUSIC_GENRE>();
                        for (var i = 0; i < Entry.EntryList.nEntryCount; i++)
                        {
                            this.IO.In.SeekTo(NextEntry * XMIENTRYSIZE);

                            XMIENTRY_MUSIC_GENRE Genre = new XMIENTRY_MUSIC_GENRE(this.IO.In);
                            Genre.EntryIndex = NextEntry;
                            NextEntry = Genre.MasterListEntry.nNextEntry;

                            this.Genres.Add(Genre);
                        }
                        break;
                    case XMIOBJECTTYPE.XMIOBJECTTYPE_MUSIC_PLAYLIST:

                        this.Playlists = new List<XMIENTRY_MUSIC_PLAYLIST>();
                        for (var i = 0; i < Entry.EntryList.nEntryCount; i++)
                        {
                            this.IO.In.SeekTo(NextEntry * XMIENTRYSIZE);

                            XMIENTRY_MUSIC_PLAYLIST Playlist = new XMIENTRY_MUSIC_PLAYLIST(this.IO.In);
                            Playlist.EntryIndex = NextEntry;
                            NextEntry = Playlist.MasterListEntry.nNextEntry;

                            this.Playlists.Add(Playlist);
                        }
                        break;
                }
            }
        }

        private string GetMediaPath(uint EntryIndex)
        {
            return string.Format("media\\{0:X4}\\{1:X4}", EntryIndex >> 12, EntryIndex & 0xFFF);
        }
    }

    public struct XMIMEDIAFILEAUDIO
    {
        public uint dwSignature;
        public uint dwVersion;
        public uint dwMediaType;
        public string szTrackName;
        public string szAlbumName;
        public string szAlbumArtistName;
        public string szTrackArtistName;
        public string szAlbumGenreName;
        public string szTrackGenreName;
        public uint dwDuration;
        public uint dwTrackNumber;
        public uint dwAlbumReleaseYear;
        public byte[] AlbumId;

        public EndianIO IO;

        public XMIMEDIAFILEAUDIO(EndianIO IO)
        {
            this.IO = IO;
            EndianReader reader = IO.In;

            this.dwSignature = reader.ReadUInt32();
            this.dwVersion = reader.ReadUInt32();
            this.dwMediaType = reader.ReadUInt32();
            this.szTrackName = reader.ReadUnicodeString(0x100);
            this.szAlbumName = reader.ReadUnicodeString(0x100);
            this.szAlbumArtistName = reader.ReadUnicodeString(0x100);
            this.szTrackArtistName = reader.ReadUnicodeString(0x100);
            this.szAlbumGenreName = reader.ReadUnicodeString(0x100);
            this.szTrackGenreName = reader.ReadUnicodeString(0x100);
            this.dwDuration = reader.ReadUInt32();
            this.dwTrackNumber = reader.ReadUInt32();
            this.dwAlbumReleaseYear = reader.ReadUInt32();
            this.AlbumId = reader.ReadBytes(240);
        }
        public byte[] ReadAudio()
        {
            this.IO.In.SeekTo(0xD08);
            return this.IO.In.ReadBytes(this.IO.Stream.Length - 0xD08);
        }
    }
}
