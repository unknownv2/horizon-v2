using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XboxDataBaseFile;
using Horizon.Functions;
using System.IO;
using Horizon.Properties;
using XContent;
using System.Threading;
using Horizon.Forms;

namespace Horizon.PackageEditors.Avatar_Award_Unlocker
{
    public partial class AvatarAwardUnlocker : EditorControl
    {
        public AvatarAwardUnlocker()
        {
            InitializeComponent();
            backupOnOpen = true;
            #if PNET
                listGames.ContextMenuStrip = menuExtract;
            #endif
            listAwards.Columns[2].DefaultCellStyle.WrapMode
                = listAwards.Columns[1].DefaultCellStyle.WrapMode
                = DataGridViewTriState.True;
        }

        private int currentGame;
        public override void revertForm()
        {
            isBusy = true;
            tabMain.Select();
            tabAward.Visible = false;
            listAwards.Rows.Clear();
            listGames.Items.Clear();
            currentGame = SettingAsInt(16);
            avTracker = null;
            totalPossible = 0;
            TitlePlayedRecords = new List<TitlePlayedRecord>();
            isBusy = false;
        }

        private ProfileFile Profile;
        private AvatarAssetTracker avTracker;
        public override bool Entry()
        {
            tileTotal = SettingAsString(248);
            totalAll = SettingAsString(219);
            totalTitle = SettingAsString(63);
            unlockAllDisplayed = SettingAsString(91);
            secretAward = SettingAsString(144);
            bool pecFound = DoesFileExist(SettingAsString(159));
            if (pecFound)
            {
                Profile = new ProfileFile(Package, (uint)SettingAsLong(90));
                PEC = new PEC(Package.StfsContentPackage.GetEndianIO(SettingAsString(159)));
                if (PEC == null)
                {
                    UI.messageBox(SettingAsString(81), SettingAsString(126), MessageBoxIcon.Error);
                    cmdUnlockAll.Enabled = cmdUnlockAllAwards.Enabled = false;
                    return false;
                }
                Profile.Read();
                Text = SettingAsString(59) + (Account == null ? "Unknown" : Account.Info.GamerTag);
                populateTitleRecords();
                updateAwardProgressText();
            }
            if (TitlePlayedRecords.Count == 0 || !pecFound)
            {
                UI.messageBox(SettingAsString(81), SettingAsString(126), MessageBoxIcon.Information);
                cmdUnlockAll.Enabled = cmdUnlockAllAwards.Enabled = false;
            }
            else
                listGames.Items[0].Selected = true;
            return true;
        }

        private uint totalPossible;
        private List<TitlePlayedRecord> TitlePlayedRecords;
        private PEC PEC;
        private void populateTitleRecords()
        {
            prePopulate();
            List<DataFileRecord> TitleRecords = Profile.DataFile.FindDataEntries(Namespace.TITLES);
            for (int x = 0; x < TitleRecords.Count; x++)
            {
                if (TitleRecords[x].Id.Id != 0)
                {
                    TitlePlayedRecord newRec = new TitlePlayedRecord(Profile.DataFile.SeekToRecord(TitleRecords[x].Entry));
                    if (newRec.AllAvatarAwards.Possible != 0)
                    {
                        TitlePlayedRecords.Add(newRec);
                        totalPossible += TitlePlayedRecords[TitlePlayedRecords.Count + SettingAsInt(16)].AllAvatarAwards.Possible;
                        listGames.Items.Add(getGameRow(TitlePlayedRecords.Count + SettingAsInt(16)));
                    }
                }
            }
            postPopulate();
        }

        private bool isBusy = false;
        private void prePopulate()
        {
            isBusy = true;
            listGames.Items.Clear();
            listGames.BeginUpdate();
            listGames.TileSize = new Size(SettingAsInt(134), SettingAsInt(246));
            listGames.LargeImageList = new ImageList();
            listGames.LargeImageList.ImageSize = new Size(SettingAsInt(211), SettingAsInt(211));
            listGames.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
        }

        private void postPopulate()
        {
            listGames.EndUpdate();
            if (listGames.Items.Count > 0)
                listGames.Items[0].Selected = true;
            else
                tabMain.Select();
            isBusy = false;
        }

        private static string tileTotal;
        private ListViewItem getGameRow(int x)
        {
            DataFile dataFile = new DataFile(Package.StfsContentPackage.GetEndianIO(ProfileFile.FormatTitleIDToFilename(TitlePlayedRecords[x].TitleId)));
            dataFile.Read();
            listGames.LargeImageList.Images.Add(getGameTile(dataFile));
            ListViewItem Game = new ListViewItem(TitlePlayedRecords[x].TitleName, listGames.Items.Count);
            Game.SubItems[0].Tag = x;
            Game.SubItems.Add(TitlePlayedRecords[x].TitleId.ToString("X"));
            Game.SubItems.Add(String.Format(tileTotal,
                TitlePlayedRecords[x].AllAvatarAwards.Earned.ToString(),
                TitlePlayedRecords[x].AllAvatarAwards.Possible.ToString()));
            return Game;
        }

        private Image getGameTile(DataFile dataFile)
        {
            try
            {
                return Image.FromStream(new MemoryStream(
                        dataFile.ReadRecord(new DataFileId()
                        {
                            Namespace = Namespace.IMAGES,
                            Id = (ulong)SettingAsLong(82)
                        })));
            }
            catch { return Resources.QuestionMark; }
        }

        private static string totalAll;
        private static string totalTitle;
        private void updateAwardProgressText()
        {
            pTotal.Maximum = (int)totalPossible;
            SettingRecord rec = new SettingRecord();
            pTotal.Value = getTotalUnlocked();
            pTotal.Text = String.Format(totalAll, pTotal.Value, pTotal.Maximum);
        }

        private int getTotalUnlocked()
        {
            int totalUnlocked = 0;
            foreach (TitlePlayedRecord title in TitlePlayedRecords)
                totalUnlocked += title.AllAvatarAwards.Earned;
            return totalUnlocked;
        }

        public override void Save()
        {
            if (PEC != null)
            {
                PEC.Flush();
                PEC.Save();
            }
        }

        private static string unlockAllDisplayed;
        private void listGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listGames.SelectedIndices.Count != 0)
            {
                isBusy = true;
                currentGame = (int)listGames.SelectedItems[0].SubItems[0].Tag;
                DataFile dataFile = new DataFile(PEC.StfsPec.GetEndianIO(ProfileFile.FormatTitleIDToFilename(TitlePlayedRecords[currentGame].TitleId)));
                dataFile.Read();
                pbGame.Image = listGames.LargeImageList.Images[listGames.SelectedItems[0].ImageIndex];
                avTracker = new AvatarAssetTracker(Profile, dataFile, TitlePlayedRecords[currentGame]);
                cmdUnlockAll.Text = unlockAllDisplayed;
                tabMain.Text = TitlePlayedRecords[currentGame].TitleName;
                updateGameProgress();
                tabMain.Select();
                rbPackageEditor.Refresh();
                populateAwardList();
                if (listAwards.Rows.Count > SettingAsInt(74))
                    loadAwardRow(0);
                isBusy = false;
                tabMain.Select();
                rbPackageEditor.Refresh();
            }
        }

        private void populateAwardList()
        {
            listAwards.Rows.Clear();
            if (currentGame != -1)
            {
                for (int x = new int(); x < avTracker.Awards.Count; x++)
                    listAwards.Rows.Add(getAwardRow(x));
                if (!(tabAward.Visible = listAwards.Rows.Count != 0))
                    tabMain.Select();
                rbPackageEditor.Refresh();
            }
        }

        private void ckUnlocked_CheckedChanged(object sender, EventArgs e)
        {
            if (dateUnlocked.Enabled = ckUnlockedOnline.Checked)
                dateUnlocked.Value = DateTime.Now;
        }

        private static string secretAward;
        private DataGridViewRow getAwardRow(int x)
        {
            DataGridViewRow Av = new DataGridViewRow();
            Av.CreateCells(listAwards);
            Av.Height = 66;
            Av.Tag = x;
            Av.Cells[0].Value = avTracker.Awards[x].AssetCollected ? Resources.EarnedAvatarAward : Resources.Unearned;
            Av.Cells[1].Value = avTracker.Awards[x].Name.Replace(Environment.NewLine, String.Empty)
                + Environment.NewLine
                + "Gender: " + avTracker.Awards[x].BodyType.ToString()
                + Environment.NewLine
                + (avTracker.Awards[x].AssetCollected ? (SettingAsString(57)
                + (avTracker.Awards[x].AssetCollectedOnline ? SettingAsString(197) : SettingAsString(70))) : SettingAsString(188));
            if (avTracker.Awards[x].AssetCollected)
                Av.Cells[2].Value = avTracker.Awards[x].Description;
            else
                if (avTracker.Awards[x].AssetShowUnawarded)
                    Av.Cells[2].Value = avTracker.Awards[x].UnawardedText;
                else
                    Av.Cells[2].Value = secretAward;
            Av.Cells[2].Value = ((string)Av.Cells[2].Value).Replace(Environment.NewLine, " ");
            return Av;
        }

        private int currentAward;
        private void loadAwardRow(int row)
        {
            currentAward = (int)listAwards.Rows[row].Tag;
            DataGridViewRow Row = getAwardRow(currentAward);
            pbAward.Image = (Image)(listAwards.Rows[row].Cells[0].Value = Row.Cells[0].Value);
            pbMarketplace.ImageLocation = Server.GameAdder.getAssetImageURL(avTracker.Awards[currentAward].id, SettingAsInt(27));
            listAwards.Rows[row].Cells[1].Value = Row.Cells[1].Value;
            listAwards.Rows[row].Cells[2].Value = Row.Cells[2].Value;
            lblLockedDescription.Text = "<b>" + SettingAsString(142) + ":</b> "
                + (avTracker.Awards[currentAward].AssetShowUnawarded ? avTracker.Awards[currentAward].UnawardedText : secretAward);
            lblUnlockedDescription.Text = "<b>" + SettingAsString(133) + ":</b> " + avTracker.Awards[currentAward].Description;
            dateUnlocked.Enabled = ckUnlockedOffline.Checked = ckUnlockedOnline.Checked = SettingAsBool(218);
            if (avTracker.Awards[currentAward].AssetCollectedOnline)
                ckUnlockedOnline.Checked = true;
            else if (avTracker.Awards[currentAward].AssetCollected)
                ckUnlockedOffline.Checked = true;
            else
                dateUnlocked.Value = DateTime.Now;
            dateUnlocked.Tag = avTracker.Awards[currentAward].dtAwarded.ToFileTimeUtc();
            panelAward.Enabled = !avTracker.Awards[currentAward].AssetCollected;
            tabAward.Text = avTracker.Awards[currentAward].Name;
            rbPackageEditor.Refresh();
        }

        private void updateGameProgress()
        {
            updateGameRow(currentGame);
            pTotalTitle.Maximum = TitlePlayedRecords[currentGame].AllAvatarAwards.Possible;
            pTotalTitle.Value = TitlePlayedRecords[currentGame].AllAvatarAwards.Earned;
            pTotalTitle.Text = String.Format(totalTitle, pTotalTitle.Value, pTotalTitle.Maximum, TitlePlayedRecords[currentGame].TitleName);
        }

        private void updateGameRow(int x)
        {
            for (int i = 0; i < listGames.Items.Count; i++)
                if ((int)listGames.Items[i].SubItems[0].Tag == x)
                    listGames.Items[i].SubItems[2].Text = String.Format(tileTotal,
                        TitlePlayedRecords[x].AllAvatarAwards.Earned.ToString(),
                        TitlePlayedRecords[x].AllAvatarAwards.Possible.ToString());
        }

        private void cmdUnlockAward_Click(object sender, EventArgs e)
        {
            try
            {
                if (ckUnlockedOffline.Checked || ckUnlockedOnline.Checked)
                    unlockAward(listAwards.SelectedRows[0].Index, ckUnlockedOnline.Checked);
            }
            catch (Exception ex)
            {
                saveErrorRebuild(ex);
            }
        }

        private void unlockAward(int row, bool earnedOnline)
        {
            int x = (int)listAwards.Rows[row].Tag;
            if (!avTracker.Awards[x].AssetCollected)
            {
                var dt = dateUnlocked.Value.AddMilliseconds(Global.random.Next(0, 60000));
                avTracker.UnlockAsset(avTracker.Awards[x], earnedOnline, dt);
                if (WorkerRunning)
                {
                    Main.mainForm.Invoke((MethodInvoker)delegate
                    {
                        updateAwardProgressText();
                        updateGameProgress();
                    });
                }
                else
                {
                    loadAwardRow(row);
                    updateAwardProgressText();
                    updateGameProgress();
                }
            }
        }

        private void cmdUnlockAll_Click(object sender, EventArgs e)
        {
            if (listAwards.SelectedRows.Count > 1)
                for (int x = 0; x < listAwards.SelectedRows.Count; x++)
                    unlockAward(listAwards.SelectedRows[x].Index, SettingAsBool(218));
            else
                for (int x = 0; x < listAwards.Rows.Count; x++)
                {
                    if (cancelUnlock)
                        break;
                    unlockAward(x, SettingAsBool(218));
                }
        }

        private void listAwards_SelectionChanged(object sender, EventArgs e)
        {
            if (!isBusy)
            {
                if (listAwards.SelectedRows.Count > 1)
                {
                    cmdUnlockAll.Text = SettingAsString(216);
                    tabMain.Select();
                }
                else
                {
                    cmdUnlockAll.Text = unlockAllDisplayed;
                    tabAward.Select();
                }
                if (listAwards.Rows.Count > 0)
                    if (listAwards.SelectedRows.Count == 0)
                        listAwards.Rows[0].Selected = true;
                    else
                        loadAwardRow(listAwards.SelectedRows[0].Index);
            }
        }

        private void enableControls(bool enable)
        {
            listAwards.Enabled
                = listGames.Enabled
                = cmdUnlockAll.Enabled
                = cmdUnlockAward.Enabled
                = enable;
        }

        private bool cancelUnlock = false;
        private static readonly string cancel = "Cancel";
        private void cmdUnlockAllAwards_Click(object sender, EventArgs e)
        {
            if (cmdUnlockAllAwards.Text.Length == cancel.Length)
                cancelUnlock = true;
            else if (TitlePlayedRecords.Count > 0 && UI.messageBox(SettingAsString(84), SettingAsString(178),
                MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
            {
                if (Meta.IsFatx && FormHandle.isDeviceWorkerThreadRunning(Meta.DeviceIndex))
                {
                    FatxHandle.showOperationRunningMessage();
                    return;
                }
                enableControls(false);
                cmdUnlockAllAwards.Text = cancel;
                WorkerThread = new Thread((ThreadStart)delegate
                {
                    bool errorThrown = true;
                    try
                    {
                        for (int x = 0; x < listGames.Items.Count; x++)
                        {
                            Main.mainForm.Invoke((MethodInvoker)delegate { listGames.Items[x].Selected = true; });
                            cmdUnlockAll_Click(cmdUnlockAll, null);
                            if (cancelUnlock)
                                break;
                        }
                        errorThrown = false;
                    }
                    catch (FileNotFoundException)
                    {
                        return;
                    }
                    catch (Exception ex)
                    {
                        UI.errorBox("An error has occured while unlocking your avatar awards!\n\nDetails:\n" + ex.Message);
                    }
                    Main.mainForm.Invoke((MethodInvoker)delegate
                    {
                        for (int x = 0; x < listAwards.Rows.Count; x++)
                            loadAwardRow(x);
                        if (listAwards.Rows.Count != 0)
                            loadAwardRow(0);
                        if (!errorThrown && !cancelUnlock)
                            UI.messageBox(SettingAsString(235), SettingAsString(163), MessageBoxIcon.Information);
                        cancelUnlock = false;
                        cmdUnlockAllAwards.Text = "Unlock All Awards";
                        enableControls(true);
                    });
                });
                WorkerThread.Start();
            }
        }

        private void extractAwardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listGames.SelectedIndices.Count == 1)
            {
                int cG = (int)listGames.SelectedItems[0].SubItems[0].Tag;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "GPD|*.gpd";
                if (sfd.ShowDialog() == DialogResult.OK)
                    File.WriteAllBytes(sfd.FileName, PEC.StfsPec.ExtractFileToArray(ProfileFile.FormatTitleIDToFilename(TitlePlayedRecords[cG].TitleId)));
            }
        }
    }
}
