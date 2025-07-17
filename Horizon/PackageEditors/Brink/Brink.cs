using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Brink;

namespace Horizon.PackageEditors.Brink
{
    public partial class Brink : EditorControl
    {
        //public static readonly string FID = "425307D9";
        private BrinkSave BrinkSave;

        public Brink()
        {
            InitializeComponent();
            TitleID = FormID.Brink;
        }

        public override void enablePanels(bool enable)
        {
            panelMain.Enabled = enable;
        }

        public override bool Entry()
        {
            this.comboBoxEx1.Items.Clear();
            this.richTextBox1.Text = string.Empty;

            if (!OpenStfsFile("save.dat"))
                return false;

            this.BrinkSave = new BrinkSave(this.IO);

            for (var x = 0; x < this.BrinkSave.SettingsEntries.Count; x++)
            {
                this.comboBoxEx1.Items.Add(string.Format("{0:D4}", x));
            }

            if (this.BrinkSave.SettingsEntries.Count > 0x04)
                this.comboBoxEx1.SelectedIndex = 0x04;
            else
                this.comboBoxEx1.SelectedIndex = 0x00;

            return true;
        }

        public override void Save()
        {
            int Index = this.comboBoxEx1.SelectedIndex;
            int dwReturn = this.BrinkSave.WriteEntryBuffer(this.BrinkSave.SettingsEntries[Index], Index, this.richTextBox1.Text);
            switch (dwReturn)
            {
                case 0x01:
                    Functions.UI.errorBox("BRINK: Attempted to write setting data longer than the allocated space for that setting.\n\n"
                        + "Shorten your configuration file before saving again!");
                    break;
            }
        }

        private void SettingsIndexChanged(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
            this.richTextBox1.Text = this.BrinkSave.ReadEntryBuffer(this.BrinkSave.SettingsEntries[this.comboBoxEx1.SelectedIndex]);
            this.richTextBox1.MaxLength = this.BrinkSave.MaxSettingsEntrySize[this.comboBoxEx1.SelectedIndex];
            tabMain.Select();
        }
    }
}
