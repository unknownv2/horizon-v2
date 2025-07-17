using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Horizon.PackageClasses;
using ModernWarfare3;
using Horizon.Functions;
using DevComponents.DotNetBar;
using Horizon.Controls;
using CallofDuty;

namespace Horizon.PackageEditors.Call_of_Duty_Advanced_Warfare
{
    public partial class CoDAdvancedWarfare : EditorControl
    {
        private CampaignSave saveGame;

        private string[] SegmentInfo =
        {
            "Nothing worth modding",
            "Effects and scripts",
            "Effects, audio, scripts, and DVARs",
            "Model properties",
            "Mission progress, models",
            null,
            "Effects, physics, and scripts",
            null,
        };

        public CoDAdvancedWarfare()
        {
            InitializeComponent();
            TitleID = FormID.CallofDutyAdvancedWarfare;


            modPanels = new []
            {
                panelMain
            };

            cmdMaxHealth.SetDvars(new object[] {
                "g_player_maxHealth", 10000
            });

            cmdUnlimitedSprint.SetDvars(new object[] {
                "player_sprintUnlimited", 1
            });

            cmdGiveAll.SetDvars(new object[] {
                "g_giveAll", 1
            });

            cmdHideHud.SetDvars(new object[] {
                "cg_drawHUD", 0
            });

            cmdJumpSlowdown.SetDvars(new object[] {
                "jump_slowdownEnable", 0
            });

            cmdDebugUI.SetDvars(new object[] {
                "debug", 1,
                "ui_debugMode", 1
            });

            cmdDisableAI.SetDvars(new object[] {
                "g_ai", 0,
                "g_spawnai", 0
            });
        }
        private RibbonPanel[] modPanels;
        private Segment2 seg2;
        private byte[][] Segments;
        public override bool Entry()
        {
            if (!OpenStfsFile("savegame.svg"))
                return false;

            //0x500, 0x4CC
            saveGame = new CampaignSave(IO, SettingAsInt(30), SettingAsInt(156));

            populateSegments();

            readSegments();

            return true;
        }

        private bool isBusy;
        private void RefreshPanels()
        {
            this.isBusy = true;

            foreach (RibbonPanel panel in modPanels)
            {
                foreach (Control c in panel.Controls)
                {
                    if (c is DvarButton)
                    {
                        bool isGood = true;
                        DvarButton but = (DvarButton)c;
                        foreach (Dvar dvar in but.Dvars)
                        {
                            string val = seg2.Dvars[dvar.NameLc];
                            if (val == null || val.ToLower() != dvar.ValueLc)
                            {
                                isGood = false;
                                break;
                            }
                        }
                        but.Checked = isGood;
                    }
                    else if (c is DvarSlider)
                    {
                        DvarSlider slider = (DvarSlider)c;
                        string val = seg2.Dvars[slider.DvarLc];
                        if (val == null)
                            slider.Checked = false;
                        else
                        {
                            float fval;
                            if (float.TryParse(val, out fval))
                            {
                                if (fval >= slider.Minimum && fval <= slider.Maximum)
                                {
                                    slider.Value = fval;
                                    slider.Checked = true;
                                }
                            }
                            else
                                slider.Checked = false;
                        }
                    }
                }
            }

            this.isBusy = false;
        }

        private void SetSliderValues()
        {
            foreach (RibbonPanel panel in modPanels)
            {
                foreach (Control c in panel.Controls)
                {
                    if (c is DvarSlider)
                    {
                        DvarSlider slider = (DvarSlider)c;
                        ListViewItem li = getListViewItemFromDvar(slider.DvarLc);
                        if (slider.Checked)
                        {
                            string val = slider.Value.ToString();
                            seg2.Dvars[slider.DvarLc] = val;
                            if (li == null)
                                listDvars.Items.Add(slider.ListViewItem);
                            else
                                li.SubItems[1].Text = val;
                        }
                        else
                        {
                            seg2.Dvars.Remove(slider.DvarLc);
                            if (li != null)
                                li.Remove();
                        }
                    }
                }
            }
        }

        public override void Save()
        {
            SetSliderValues();
            Segments[2] = seg2.ToArray();
            MemFile memFile = new MemFile();
            for (int x = 0; x < 8; x++)
                memFile.WriteSegment(this.saveGame.InjectSegment(Segments[x]));

            Package.StfsContentPackage.InjectFileFromArray("savegame.svg", this.saveGame.Save(memFile.ToArray()));

            OpenStfsFile("savegame.svg");
            saveGame = new CampaignSave(IO, 0x500, 0x4CC);
        }

        private void readSegments()
        {
            readSegment2();
        }

        private void populateSegments()
        {
            listSegments.BeginUpdate();
            listSegments.Items.Clear();

            this.Segments = new byte[8][];
            for (int x = 0; x < 8; x++)
            {
                this.Segments[x] = this.saveGame.ExtractSegment(x);

                string segmentText = "Segment " + x;
                if (SegmentInfo[x] != null)
                    segmentText += " - " + SegmentInfo[x];
                listSegments.Items.Add(new ListViewItem(new [] { segmentText, Global.getFormatFromBytes(Segments[x].Length) }));
            }

            listSegments.EndUpdate();
        }

        private void readSegment2()
        {
            seg2 = new Segment2();
            seg2.Read(Segments[2]);

            listDvars.BeginUpdate();
            listDvars.Items.Clear();
            foreach (KeyValuePair<string, string> dvar in seg2.Dvars)
                listDvars.Items.Add(new ListViewItem(new string[] { dvar.Key, dvar.Value }));
            listDvars.EndUpdate();

            RefreshPanels();
        }

        private void listDvars_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listDvars.SelectedItems.Count != 0)
            {
                txtDvar.Text = listDvars.SelectedItems[0].Text;
                txtValue.Text = listDvars.SelectedItems[0].SubItems[1].Text;
            }
        }

        private void txtDvar_TextChanged(object sender, EventArgs e)
        {
            cmdAddEntry.Text = this.seg2.Dvars.ContainsKey(txtDvar.Text) ? "Update Entry" : "Add Entry";
        }

        private void cmdRemoveEntry_Click(object sender, EventArgs e)
        {
            if (listDvars.SelectedItems.Count == 0)
                return;

            while (listDvars.SelectedIndices.Count != 0)
            {
                this.seg2.Dvars.Remove(listDvars.SelectedItems[0].Text);
                listDvars.Items.RemoveAt(listDvars.SelectedIndices[0]);
            }
            txtDvar.Clear();
            txtValue.Clear();
        }

        private void cmdAddEntry_Click(object sender, EventArgs e)
        {
            if (txtDvar.TextLength == 0 || txtValue.TextLength == 0)
            {
                UI.errorBox("Enter a DVAR and value!");
                return;
            }

            if (txtDvar.Text.ToLower() == "g_player_maxhealth")
            {
                int maxHealth;
                if (!int.TryParse(txtValue.Text, out maxHealth))
                {
                    UI.errorBox("This DVAR must have a numeric value!");
                    return;
                }
            }

            if (this.seg2.Dvars.ContainsKey(txtDvar.Text))
            {
                this.seg2.Dvars[txtDvar.Text] = txtValue.Text;
                ListViewItem li = getListViewItemFromDvar(txtDvar.Text);
                if (li != null)
                    li.SubItems[1].Text = txtValue.Text;
            }
            else
            {
                this.seg2.Dvars[txtDvar.Text] = txtValue.Text;
                listDvars.Items.Add(new ListViewItem(new string[] { txtDvar.Text, txtValue.Text }));
                txtDvar.Clear();
                txtValue.Clear();
            }
        }

        public ListViewItem getListViewItemFromDvar(string dvar)
        {
            dvar = dvar.ToLower();
            for (int x = 0; x < listDvars.Items.Count; x++)
                if (listDvars.Items[x].Text.ToLower() == dvar)
                    return listDvars.Items[x];
            return null;
        }

        private void cmdExtractSegment_Click(object sender, EventArgs e)
        {
            if (listSegments.SelectedIndices.Count == 0)
            {
                UI.errorBox("Select a segment from the list!");
                return;
            }

            int segment = listSegments.SelectedItems[0].Index;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Segment " + segment + ".bin";
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            File.WriteAllBytes(sfd.FileName, Segments[segment]);

            UI.messageBox("Segment extracted!", "Segment Saved", MessageBoxIcon.Information);
        }

        private static bool segmentMessageShown = false;
        private void cmdReplaceSegment_Click(object sender, EventArgs e)
        {
            if (listSegments.SelectedIndices.Count == 0)
            {
                UI.errorBox("Select a segment from the list!");
                return;
            }

            int segment = listSegments.SelectedItems[0].Index;

            if (!segmentMessageShown)
            {
                if (UI.messageBox(this, "This segment may contain information shown in the other tabs.\nAny changes made to them may be lost if you replace this segment.\n\nContinue?",
                    "Replace Segment", MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3) != DialogResult.Yes)
                    return;
                segmentMessageShown = true;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "Segment " + segment + ".bin";
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            Segments[segment] = File.ReadAllBytes(ofd.FileName);

            listSegments.SelectedItems[0].SubItems[1].Text = Global.getFormatFromBytes(Segments[segment].Length);

            this.readSegments();

            UI.messageBox("Segment replaced successfully!", "Segment Replaced", MessageBoxIcon.Information);
        }

        private void cmdExtractSegments_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK)
                return;

            string filePath = fbd.SelectedPath + "\\Segment ";
            for (int x = 0; x < 8; x++)
                File.WriteAllBytes(filePath + x + ".bin", this.Segments[x]);

            UI.messageBox("All segments extracted!", "Segments Saved", MessageBoxIcon.Information);
        }

        private int searchIndexTop;
        private List<int> searchIndex;
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchIndex = new List<int>();
            if (txtSearch.TextLength != 0)
            {
                string searchText = txtSearch.Text.ToLower();
                for (int x = 0; x < listDvars.Items.Count; x++)
                    if (listDvars.Items[x].Text.ToLower().Contains(searchText))
                        searchIndex.Add(x);
            }
            if (searchIndex.Count != 0)
            {
                searchIndexTop = 0;
                listDvars.TopItem = listDvars.Items[searchIndex[0]];
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmdSearch_Click(null, null);
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.TextLength != 0 && searchIndex.Count != 0)
                listDvars.TopItem = listDvars.Items[searchIndex[searchIndexTop++ % searchIndex.Count]];
        }

        private void rbPackageEditor_SelectedRibbonTabChanged(object sender, EventArgs e)
        {
            if (saveGame == null)
                return;

            if (rbPackageEditor.SelectedRibbonTabItem == tabAdvanced)
                this.SetSliderValues();
            else
                this.RefreshPanels();
        }

        private void cmdDvar_CheckedChanged(object sender, EventArgs e)
        {
            if (this.isBusy)
                return;

            DvarButton but = (DvarButton)sender;
            if (but.Checked)
            {
                foreach (Dvar dvar in but.Dvars)
                {
                    seg2.Dvars[dvar.Name] = dvar.Value;
                    ListViewItem li = getListViewItemFromDvar(dvar.NameLc);
                    if (li == null)
                        listDvars.Items.Add(dvar.ListViewItem);
                    else
                        li.SubItems[1].Text = dvar.Value;
                }
            }
            else
            {
                foreach (Dvar dvar in but.Dvars)
                {
                    seg2.Dvars.Remove(dvar.NameLc);
                    ListViewItem li = getListViewItemFromDvar(dvar.NameLc);
                    if (li != null)
                        li.Remove();
                }
            }
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmdAddEntry_Click(null, null);
        }
    }
    class Segment2 : Modern_Warfare_3.Segment
    {
        private byte[] header, postDvars1, footer;
        private int u1, u2, u3, u4;

        public List<string> models1, models2,
            effects1, effects2,
            audio1, audio2,
            text1, uList1, uList2;

        public DvarCollection Dvars;

        public override void Read(EndianIO io)
        {
            header = io.In.ReadBytes(0xCE);

            models1 = readStringBlock(0x400, io);
            models2 = readStringBlock(0x40, io);
            effects1 = readStringBlock(0x200, io);
            effects2 = readStringBlock(0x200, io);
            audio1 = readStringBlock(0x200, io);
            audio2 = readStringBlock(0x80, io);
            text1 = readStringBlock(0x866, io);
            uList1 = readStringBlock(0x02, io);
            uList2 = readStringBlock(0x0D, io);

            Dvars = new DvarCollection(io);

            postDvars1 = io.In.ReadBytes(0xC3E4);
            u1 = io.In.ReadInt32();
            u2 = io.In.ReadInt32();
            u3 = io.In.ReadInt32();
            u4 = io.In.ReadInt32();

            io.In.ReadInt32();

            footer = io.In.ReadBytes(io.Length - io.Position);
        }

        public override byte[] ToArray()
        {
            EndianIO IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            IO.Out.Write(header);

            writeStringBlock(0x400, IO, models1);
            writeStringBlock(0x40, IO, models2);
            writeStringBlock(0x200, IO, effects1);
            writeStringBlock(0x200, IO, effects2);
            writeStringBlock(0x200, IO, audio1);
            writeStringBlock(0x80, IO, audio2);
            writeStringBlock(0x866, IO, text1);
            writeStringBlock(0x02, IO, uList1);
            writeStringBlock(0x0D, IO, uList2);

            Dvars.Write(IO);

            IO.Out.Write(postDvars1);
            IO.Out.Write(u1);
            IO.Out.Write(u2);
            IO.Out.Write(u3);
            IO.Out.Write(u4);

            if (Dvars.ContainsKey("g_player_maxHealth"))
                IO.Out.Write(int.Parse(Dvars["g_player_maxHealth"]));
            else
                IO.Out.Write(100);

            IO.Out.Write(footer);

            IO.Close();
            return IO.ToArray();
        }
    }
}
