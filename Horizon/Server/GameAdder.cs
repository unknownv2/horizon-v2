using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using Horizon.Properties;
using System.IO;
using Horizon.Functions;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;

namespace Horizon.Server
{
    public static class GameAdder
    {
        public struct TitleMetaInfo
        {
            public string TID;
            public string TitleName;
            public byte TotalAwards;
            public byte MaleAwards;
            public byte FemaleAwards;
            public int TotalAchievements;
            public int TotalCredit;
            public bool Flagged;
        }

        public class TitleTemplate
        {
            public byte[] Tile;
            public TitleMetaInfo Meta = new TitleMetaInfo();
            public List<TitleAchievement> Achievements = new List<TitleAchievement>();
            public List<TitleAward> Awards = new List<TitleAward>();
        }

        public struct TitleAchievement
        {
            public string AchievementName;
            public int Credit;
            public uint ID;
            public uint ImageID;
            public uint Flags;
            public string LockedDescription;
            public string UnlockedDescription;
        }

        public struct TitleAward
        {
            public uint cb;
            public byte[] id;
            public ulong Id;
            public uint imageId;
            public uint flags;
            public ulong reserved;
            public string Name;
            public string Description;
            public string UnawardedText;
        }

        internal enum AwardType : byte
        {
            All = 0,
            Male = 1,
            Female = 2
        }

        internal static Dictionary<string, TitleTemplate> Titles = new Dictionary<string, TitleTemplate>();
        internal static void updateMetaStorage(XPathNavigator nav)
        {
            Settings.Default.GameAdder = Ionic.Zlib.ZlibStream.UncompressString(Convert.FromBase64String(nav.Value));
            Settings.Default.Save();
        }

        internal static bool populateTitleMeta()
        {
            if (Titles.Count > 0)
                return true;
            if (Settings.Default.GameAdder.Length == 0)
                return false;
            XPathNavigator nav = new XPathDocument(new MemoryStream(Encoding.ASCII.GetBytes(Settings.Default.GameAdder))).CreateNavigator();
            nav.MoveToRoot();
            nav.MoveToFirstChild();
            if (nav.HasChildren)
            {
                nav.MoveToFirstChild();
                do
                {
                    TitleTemplate title = new TitleTemplate();
                    title.Meta.TitleName = Encoding.BigEndianUnicode.GetString(Global.hexStringToArray(nav.Value));
                    nav.MoveToFirstAttribute();
                    title.Meta.TID = nav.Value;
                    if (!Titles.ContainsKey(title.Meta.TID))
                    {
                        nav.MoveToNextAttribute();
                        title.Meta.Flagged = nav.ValueAsInt == 1;
                        nav.MoveToNextAttribute();
                        title.Meta.TotalAchievements = nav.ValueAsInt;
                        nav.MoveToNextAttribute();
                        title.Meta.TotalCredit = nav.ValueAsInt;
                        nav.MoveToNextAttribute();
                        title.Meta.TotalAwards = (byte)nav.ValueAsInt;
                        nav.MoveToNextAttribute();
                        title.Meta.MaleAwards = (byte)nav.ValueAsInt;
                        nav.MoveToNextAttribute();
                        title.Meta.FemaleAwards = (byte)nav.ValueAsInt;
                        Titles.Add(title.Meta.TID, title);
                    }
                }
                while (nav.MoveToFollowing(XPathNodeType.Element));
            }
            return true;
        }

        internal static DataGridViewRow getGridRow(TitleTemplate title, ref DataGridViewX listTitles)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(listTitles);
            row.Tag = title.Meta.TID;
            row.Cells[0].Value = title.Meta.TitleName;
            row.Cells[1].Value = title.Meta.TID;
            row.Cells[2].Value = title.Meta.TotalCredit;
            row.Cells[3].Value = title.Meta.TotalAchievements;
            return row;
        }

        internal static DataGridViewRow getPimpedOutGridRow(TitleTemplate title, ref DataGridViewX listTitles)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(listTitles);
            row.Height = 66;
            row.Tag = title.Meta.TID;
            row.Cells[0].Value = System.Drawing.Image.FromStream(new MemoryStream(title.Tile));
            row.Cells[1].Value = "<b>" + title.Meta.TitleName + "</b><br></br>Title ID: " + title.Meta.TID + "<br></br>Awards: " + title.Meta.TotalAwards.ToString();
            row.Cells[2].Value = title.Meta.TotalCredit;
            row.Cells[3].Value = title.Meta.TotalAchievements;
            return row;
        }

        internal static bool titleExists(string TID)
        {
            return Titles.ContainsKey(TID);
        }

        internal static TitleTemplate getTitle(string TID)
        {
            if (titleExists(TID))
                return Titles[TID];
            return null;
        }

        internal static void addTitleData(XPathNavigator nav)
        {
            nav.MoveToFirstAttribute();
            string tid = nav.Value;
            nav.MoveToNextAttribute();
            string encodedTile = nav.Value;
            nav.MoveToParent();
            if (titleExists(tid))
            {
                Titles[tid].Tile = Convert.FromBase64String(encodedTile);
                if (nav.HasChildren)
                {
                    nav.MoveToFirstChild();
                    do
                    {
                        if (nav.Name == "ach")
                        {
                            TitleAchievement newAch = new TitleAchievement();
                            nav.MoveToFirstAttribute();
                            newAch.Credit = nav.ValueAsInt;
                            nav.MoveToNextAttribute();
                            newAch.ID = (uint)nav.ValueAsInt;
                            nav.MoveToNextAttribute();
                            newAch.ImageID = (uint)nav.ValueAsInt;
                            nav.MoveToNextAttribute();
                            newAch.Flags = (uint)nav.ValueAsInt;
                            nav.MoveToParent();
                            nav.MoveToFirstChild();
                            newAch.AchievementName = Encoding.BigEndianUnicode.GetString(Global.hexStringToArray(nav.Value));
                            nav.MoveToNext();
                            newAch.LockedDescription = Encoding.BigEndianUnicode.GetString(Global.hexStringToArray(nav.Value));
                            nav.MoveToNext();
                            newAch.UnlockedDescription = Encoding.BigEndianUnicode.GetString(Global.hexStringToArray(nav.Value));
                            Titles[tid].Achievements.Add(newAch);
                        }
                        else if (nav.Name == "aw")
                        {
                            TitleAward newAward = new TitleAward();
                            nav.MoveToFirstAttribute();
                            newAward.id = Global.hexStringToArray(nav.Value);
                            nav.MoveToNextAttribute();
                            newAward.Id = (ulong)nav.ValueAsLong;
                            nav.MoveToNextAttribute();
                            newAward.imageId = (uint)nav.ValueAsInt;
                            nav.MoveToNextAttribute();
                            newAward.flags = (uint)nav.ValueAsInt;
                            nav.MoveToNextAttribute();
                            newAward.reserved = ulong.Parse(nav.Value);
                            nav.MoveToNextAttribute();
                            newAward.cb = (uint)nav.ValueAsInt;
                            nav.MoveToParent();
                            nav.MoveToFirstChild();
                            newAward.Name = Encoding.BigEndianUnicode.GetString(Global.hexStringToArray(nav.Value));
                            nav.MoveToNext();
                            newAward.UnawardedText = Encoding.BigEndianUnicode.GetString(Global.hexStringToArray(nav.Value));
                            nav.MoveToNext();
                            newAward.Description = Encoding.BigEndianUnicode.GetString(Global.hexStringToArray(nav.Value));
                            Titles[tid].Awards.Add(newAward);
                        }
                        nav.MoveToParent();
                    }
                    while (nav.MoveToNext());
                    nav.MoveToParent();
                }
            }
        }

        private static string makeGuid(byte[] id)
        {
            int[] insertLocations = new int[] { 8, 13, 18, 23 };
            string Guid = id.ToHexString().ToUpper();
            foreach (int pos in insertLocations)
                Guid = Guid.Insert(pos, "-");
            return Guid;
        }

        internal static string getAssetURL(byte[] id, bool v2)
        {
            string Guid = makeGuid(id);
            return String.Format("http://download.xboxlive.com/content/{0}/avataritems/{1}.bin", Guid.Substring(28), (v2 ? "v2/" : String.Empty) + Guid);
        }

        internal static string getAssetImageURL(byte[] id, int size)
        {
            string Guid = makeGuid(id);
            return String.Format("http://avatar.xboxlive.com/global/t.{0}/avataritem/{1}/{2}", Guid.Substring(28), Guid, size);
        }

        internal static bool reqAchievementInfo(string[] tids)
        {
            string reqTIDs = String.Empty;
            foreach (string tid in tids)
                if (titleExists(tid))
                    reqTIDs += "|" + tid;
            if (reqTIDs.Length == 0)
                return false;
            Config.enableCompression = true;
            Request req = new Request("ach");
            req.addParam("tid", reqTIDs);
            return req.doRequest();
        }
    }
}
