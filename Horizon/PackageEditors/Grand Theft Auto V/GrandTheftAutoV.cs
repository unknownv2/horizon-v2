using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rockstar;
using DevComponents.DotNetBar.Controls;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;

namespace Horizon.PackageEditors.Grand_Theft_Auto_V
{
    public partial class GrandTheftAutoV : EditorControl
    {
        private GrandTheftAutoVClass saveGame;
        private bool loading = false;
        private bool changedMainTabValues = false;
        public GrandTheftAutoV()
        {
            TitleID = FormID.GrandTheftAutoV;
            InitializeComponent();
        }

        public override bool Entry()
        {
            loading = true;

            if (!OpenStfsFile(0))
                return false;

            saveGame = new GrandTheftAutoVClass(IO);
            refreshMainPageUIComponents();
            refreshList();

            loading = false;
            return true;
        }

        public override void Save()
        {
            // MAX AMMO / ALL ATTACHMENTS
            saveGame.ModifyWeapons(chkMaxAmmo.Checked, chkAllAttachments.Checked);

            // Save
            IO.Out.BaseStream.Position = 0;
            IO.Out.Write(saveGame.ToArray());
            IO.Stream.SetLength(IO.Out.BaseStream.Position);
        }

        private void cmdMaxAll_Click(object sender, EventArgs e)
        {
            intMichaelMoney.Value = intMichaelMoney.MaxValue;
            intFranklinMoney.Value = intFranklinMoney.MaxValue;
            intTrevorMoney.Value = intTrevorMoney.MaxValue;
        }
        private void MaxSlidersInContainer(Control c)
        {
            foreach (Control c2 in c.Controls)
                if (c2.GetType() == typeof(Slider))
                    (c2 as Slider).Value = (c2 as Slider).Maximum;
        }
        private void btnMaxMichaelSkills_Click(object sender, EventArgs e)
        {
            MaxSlidersInContainer(groupMichaelSkills);
        }
        private void btnMaxFranklinSkills_Click(object sender, EventArgs e)
        {
            MaxSlidersInContainer(groupFranklinSkills);
        }
        private void btnMaxTrevorSkills_Click(object sender, EventArgs e)
        {
            MaxSlidersInContainer(groupTrevorSkills);
        }
        private void intFranklinMoney_ValueChanged(object sender, EventArgs e)
        {
            if (loading)
                return;

            // This is a handler for all money changes.
            saveGame.progressInt32Values.FranklinMoney = (int)intFranklinMoney.Value;
            saveGame.progressInt32Values.MichaelMoney = (int)intMichaelMoney.Value;
            saveGame.progressInt32Values.TrevorMoney = (int)intTrevorMoney.Value;
            changedMainTabValues = true;
        }
        private void intMichaelSpecial_ValueChanged(object sender, EventArgs e)
        {
            // This is the handler for all character's special values.

            // No characters special can be below 56 without reverting back to 56 when you change characters.
            if ((sender as Slider).Value < 56)
                (sender as Slider).Value = 56;
            setSkillValues();
        }
        private void intTrevorDriving_ValueChanged(object sender, EventArgs e)
        {
            // This is the handler for all characters values that aren't special or lung capacity.
            setSkillValues();
        }
        private void intMichaelLungCapacity_ValueChanged(object sender, EventArgs e)
        {
            // This value can't go negative to go below the base value, so we must restrict it.
            int minLevel = (22 + saveGame.progressInt32Values.StatModifiers[((int)Rockstar.GrandTheftAutoVClass.SP_Stats.LungCapacity * 4) + 2]);
            if (minLevel > 100) minLevel = 100;

            if (intMichaelLungCapacity.Value < minLevel)
                intMichaelLungCapacity.Value = minLevel;
            setSkillValues();
        }
        private void intFranklinLungCapacity_ValueChanged(object sender, EventArgs e)
        {
            // This value can't go negative to go below the base value, so we must restrict it.
            int minLevel = (46 + saveGame.progressInt32Values.StatModifiers[((int)Rockstar.GrandTheftAutoVClass.SP_Stats.LungCapacity * 4) + 3]);
            if (minLevel > 100) minLevel = 100;

            if (intFranklinLungCapacity.Value < minLevel)
                intFranklinLungCapacity.Value = minLevel;
            setSkillValues();
        }
        private void intTrevorLungCapacity_ValueChanged(object sender, EventArgs e)
        {
            // This value can't go negative to go below the base value, so we must restrict it.
            int minLevel = (28 + saveGame.progressInt32Values.StatModifiers[((int)Rockstar.GrandTheftAutoVClass.SP_Stats.LungCapacity * 4) + 4]);
            if (minLevel > 100) minLevel = 100;

            if (intTrevorLungCapacity.Value < minLevel)
                intTrevorLungCapacity.Value = minLevel;
            setSkillValues();
        }

        private void refreshMainPageUIComponents()
        {
            if (saveGame == null)
                return;
            loading = true;

            // note: this is a seperate function since the value editor might make changes to these, so we'll need to call this to refresh it.
            intFranklinMoney.Value = (int)saveGame.progressInt32Values.FranklinMoney;
            intMichaelMoney.Value = (int)saveGame.progressInt32Values.MichaelMoney;
            intTrevorMoney.Value = (int)saveGame.progressInt32Values.TrevorMoney;

            // MICHAEL SKILLS
            this.intMichaelSpecial.Value = (int)saveGame.progressInt32Values.MichaelSpecial;
            this.intMichaelStamina.Value = (int)saveGame.progressInt32Values.MichaelStamina;
            this.intMichaelShooting.Value = (int)saveGame.progressInt32Values.MichaelShooting;
            this.intMichaelStrength.Value = (int)saveGame.progressInt32Values.MichaelStrength;
            this.intMichaelStealth.Value = (int)saveGame.progressInt32Values.MichaelStealth;
            this.intMichaelFlying.Value = (int)saveGame.progressInt32Values.MichaelFlying;
            this.intMichaelDriving.Value = (int)saveGame.progressInt32Values.MichaelDriving;
            this.intMichaelLungCapacity.Value = (int)saveGame.progressInt32Values.MichaelLungCapacity;


            // FRANKLIN SKILLS
            this.intFranklinSpecial.Value = (int)saveGame.progressInt32Values.FranklinSpecial;
            this.intFranklinStamina.Value = (int)saveGame.progressInt32Values.FranklinStamina;
            this.intFranklinShooting.Value = (int)saveGame.progressInt32Values.FranklinShooting;
            this.intFranklinStrength.Value = (int)saveGame.progressInt32Values.FranklinStrength;
            this.intFranklinStealth.Value = (int)saveGame.progressInt32Values.FranklinStealth;
            this.intFranklinFlying.Value = (int)saveGame.progressInt32Values.FranklinFlying;
            this.intFranklinDriving.Value = (int)saveGame.progressInt32Values.FranklinDriving;
            this.intFranklinLungCapacity.Value = (int)saveGame.progressInt32Values.FranklinLungCapacity;

            // TREVOR SKILLS
            this.intTrevorSpecial.Value = (int)saveGame.progressInt32Values.TrevorSpecial;
            this.intTrevorStamina.Value = (int)saveGame.progressInt32Values.TrevorStamina;
            this.intTrevorShooting.Value = (int)saveGame.progressInt32Values.TrevorShooting;
            this.intTrevorStrength.Value = (int)saveGame.progressInt32Values.TrevorStrength;
            this.intTrevorStealth.Value = (int)saveGame.progressInt32Values.TrevorStealth;
            this.intTrevorFlying.Value = (int)saveGame.progressInt32Values.TrevorFlying;
            this.intTrevorDriving.Value = (int)saveGame.progressInt32Values.TrevorDriving;
            this.intTrevorLungCapacity.Value = (int)saveGame.progressInt32Values.TrevorLungCapacity;

            loading = false;
        }
        private void setSkillValues()
        {
            // If we're loading the save, this might be triggered by our numeric ui components being altered, ignore these changes.
            if (loading)
                return;

            changedMainTabValues = true; // indicate we changed some values so when we switch tabs it reloads.

            // MICHAEL SKILLS
            if (saveGame.progressInt32Values.MichaelSpecial != this.intMichaelSpecial.Value)
                saveGame.progressInt32Values.MichaelSpecial = this.intMichaelSpecial.Value;
            if (saveGame.progressInt32Values.MichaelStamina != this.intMichaelStamina.Value)
                saveGame.progressInt32Values.MichaelStamina = this.intMichaelStamina.Value;
            if (saveGame.progressInt32Values.MichaelShooting != this.intMichaelShooting.Value)
                saveGame.progressInt32Values.MichaelShooting = this.intMichaelShooting.Value;
            if (saveGame.progressInt32Values.MichaelStrength != this.intMichaelStrength.Value)
                saveGame.progressInt32Values.MichaelStrength = this.intMichaelStrength.Value;
            if (saveGame.progressInt32Values.MichaelStealth != this.intMichaelStealth.Value)
                saveGame.progressInt32Values.MichaelStealth = this.intMichaelStealth.Value;
            if (saveGame.progressInt32Values.MichaelFlying != this.intMichaelFlying.Value)
                saveGame.progressInt32Values.MichaelFlying = this.intMichaelFlying.Value;
            if (saveGame.progressInt32Values.MichaelDriving != this.intMichaelDriving.Value)
                saveGame.progressInt32Values.MichaelDriving = this.intMichaelDriving.Value;
            if (saveGame.progressInt32Values.MichaelLungCapacity != this.intMichaelLungCapacity.Value)
                saveGame.progressInt32Values.MichaelLungCapacity = this.intMichaelLungCapacity.Value;

            // FRANKLIN SKILLS
            if (saveGame.progressInt32Values.FranklinSpecial != this.intFranklinSpecial.Value)
                saveGame.progressInt32Values.FranklinSpecial = this.intFranklinSpecial.Value;
            if (saveGame.progressInt32Values.FranklinStamina != this.intFranklinStamina.Value)
                saveGame.progressInt32Values.FranklinStamina = this.intFranklinStamina.Value;
            if (saveGame.progressInt32Values.FranklinShooting != this.intFranklinShooting.Value)
                saveGame.progressInt32Values.FranklinShooting = this.intFranklinShooting.Value;
            if (saveGame.progressInt32Values.FranklinStrength != this.intFranklinStrength.Value)
                saveGame.progressInt32Values.FranklinStrength = this.intFranklinStrength.Value;
            if (saveGame.progressInt32Values.FranklinStealth != this.intFranklinStealth.Value)
                saveGame.progressInt32Values.FranklinStealth = this.intFranklinStealth.Value;
            if (saveGame.progressInt32Values.FranklinFlying != this.intFranklinFlying.Value)
                saveGame.progressInt32Values.FranklinFlying = this.intFranklinFlying.Value;
            if (saveGame.progressInt32Values.FranklinDriving != this.intFranklinDriving.Value)
                saveGame.progressInt32Values.FranklinDriving = this.intFranklinDriving.Value;
            if (saveGame.progressInt32Values.FranklinLungCapacity != this.intFranklinLungCapacity.Value)
                saveGame.progressInt32Values.FranklinLungCapacity = this.intFranklinLungCapacity.Value;

            // TREVOR SKILLS
            if (saveGame.progressInt32Values.TrevorSpecial != this.intTrevorSpecial.Value)
                saveGame.progressInt32Values.TrevorSpecial = this.intTrevorSpecial.Value;
            if (saveGame.progressInt32Values.TrevorStamina != this.intTrevorStamina.Value)
                saveGame.progressInt32Values.TrevorStamina = this.intTrevorStamina.Value;
            if (saveGame.progressInt32Values.TrevorShooting != this.intTrevorShooting.Value)
                saveGame.progressInt32Values.TrevorShooting = this.intTrevorShooting.Value;
            if (saveGame.progressInt32Values.TrevorStrength != this.intTrevorStrength.Value)
                saveGame.progressInt32Values.TrevorStrength = this.intTrevorStrength.Value;
            if (saveGame.progressInt32Values.TrevorStealth != this.intTrevorStealth.Value)
                saveGame.progressInt32Values.TrevorStealth = this.intTrevorStealth.Value;
            if (saveGame.progressInt32Values.TrevorFlying != this.intTrevorFlying.Value)
                saveGame.progressInt32Values.TrevorFlying = this.intTrevorFlying.Value;
            if (saveGame.progressInt32Values.TrevorDriving != this.intTrevorDriving.Value)
                saveGame.progressInt32Values.TrevorDriving = this.intTrevorDriving.Value;
            if (saveGame.progressInt32Values.TrevorLungCapacity != this.intTrevorLungCapacity.Value)
                saveGame.progressInt32Values.TrevorLungCapacity = this.intTrevorLungCapacity.Value;


            #region Value Checks

            // MICHAEL SKILLS
            if (saveGame.progressInt32Values.MichaelSpecial != this.intMichaelSpecial.Value)
                throw new Exception("Michael Special Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.MichaelStamina != this.intMichaelStamina.Value)
                throw new Exception("Michael Stamina Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.MichaelShooting != this.intMichaelShooting.Value)
                throw new Exception("Michael Shooting Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.MichaelStrength != this.intMichaelStrength.Value)
                throw new Exception("Michael Strength Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.MichaelStealth != this.intMichaelStealth.Value)
                throw new Exception("Michael Stealth Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.MichaelFlying != this.intMichaelFlying.Value)
                throw new Exception("Michael Flying Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.MichaelDriving != this.intMichaelDriving.Value)
                throw new Exception("Michael Driving Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.MichaelLungCapacity != this.intMichaelLungCapacity.Value)
                throw new Exception("Michael Lung Capacity Calculation Error. Please report this error on the forums and submit your save!");

            // FRANKLIN SKILLS
            if (saveGame.progressInt32Values.FranklinSpecial != this.intFranklinSpecial.Value)
                throw new Exception("Franklin Special Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.FranklinStamina != this.intFranklinStamina.Value)
                throw new Exception("Franklin Stamina Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.FranklinShooting != this.intFranklinShooting.Value)
                throw new Exception("Franklin Shooting Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.FranklinStrength != this.intFranklinStrength.Value)
                throw new Exception("Franklin Strength Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.FranklinStealth != this.intFranklinStealth.Value)
                throw new Exception("Franklin Stealth Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.FranklinFlying != this.intFranklinFlying.Value)
                throw new Exception("Franklin Flying Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.FranklinDriving != this.intFranklinDriving.Value)
                throw new Exception("Franklin Driving Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.FranklinLungCapacity != this.intFranklinLungCapacity.Value)
                throw new Exception("Franklin Lung Capacity Calculation Error. Please report this error on the forums and submit your save!");

            // TREVOR SKILLS
            if (saveGame.progressInt32Values.TrevorSpecial != this.intTrevorSpecial.Value)
                throw new Exception("Trevor Special Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.TrevorStamina != this.intTrevorStamina.Value)
                throw new Exception("Trevor Stamina Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.TrevorShooting != this.intTrevorShooting.Value)
                throw new Exception("Trevor Shooting Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.TrevorStrength != this.intTrevorStrength.Value)
                throw new Exception("Trevor Strength Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.TrevorStealth != this.intTrevorStealth.Value)
                throw new Exception("Trevor Stealth Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.TrevorFlying != this.intTrevorFlying.Value)
                throw new Exception("Trevor Flying Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.TrevorDriving != this.intTrevorDriving.Value)
                throw new Exception("Trevor Driving Calculation Error. Please report this error on the forums and submit your save!");
            if (saveGame.progressInt32Values.TrevorLungCapacity != this.intTrevorLungCapacity.Value)
                throw new Exception("Trevor Lung Capacity Calculation Error. Please report this error on the forums and submit your save!");
            #endregion
        }

        private void refreshList()
        {
            if (saveGame == null)
                return;
            listValues.Nodes.Clear();
            foreach (uint ID in GrandTheftAutoVData.SPStats.Keys)
            {
                Rockstar.SPStat stat = GrandTheftAutoVData.SPStats[ID];

                //Create our node
                Node node = new Node(stat.Name);
                //Add our description
                node.Cells.Add(new Cell(stat.Comment));

                // Add our value
                try
                {
                    switch (stat.Type)
                    {
                        case SPStatType.ProfileSetting:
                            continue;// none in the gametype
                        case SPStatType.Packed:
                            continue;// none in the gametype
                        case SPStatType.Date:
                            continue;// none of these type
                        case SPStatType.Position:
                            continue;// none of these type
                        case SPStatType.String:
                            continue;// none of these type
                        case SPStatType.U8:
                            continue;// none of these type
                        case SPStatType.U16:
                            continue;// none of these type
                        case SPStatType.Int:
                            node.Cells.Add(new Cell(saveGame.progressInt32Values.Values[ID].ToString()));
                            break;
                        case SPStatType.U32:
                            node.Cells.Add(new Cell(saveGame.progressUInt32Values.Values[ID].ToString()));
                            break;
                        case SPStatType.U64:
                            node.Cells.Add(new Cell(saveGame.progressInt64Values.Values[ID].ToString()));
                            break;
                        case SPStatType.Label:
                            node.Cells.Add(new Cell(saveGame.progressInt32Values.Values[ID].ToString()));
                            break;
                        case SPStatType.Float:
                            node.Cells.Add(new Cell(saveGame.progressFloatValues.Values[ID].ToString()));
                            break;
                        case SPStatType.Bool:
                            node.Cells.Add(new Cell(((saveGame.progressBoolValues.Values[ID] >> 24) != 0).ToString())); // check first byte
                            break;
                        default:
                            continue;
                    }
                }
                catch
                {
                    continue;
                    //node.Cells.Add(new Cell("N/A"));
                    // we'll support adding values later, it's super simple, our systems almost complete for it.
                }

                //Set it's tag
                node.Tag = ID;
                //Add our node to our values list
                listValues.Nodes.Add(node);
            }
            // we won't need to reload this list until more values are changed again.
            changedMainTabValues = false;
        }

        private void listValues_SelectionChanged(object sender, EventArgs e)
        {
            if (listValues.SelectedNode == null)
                return;

            // Get important cells
            Cell valueCell = listValues.SelectedNode.Cells[2];

            // Get the type
            SPStatType valType = GrandTheftAutoVData.SPStats[(uint)listValues.SelectedNode.Tag].Type;

            // hide all value editor controls
            valEditorFloat.Visible = false;
            valEditorInt.Visible = false;
            valEditorBool.Visible = false;

            try
            {
                // determine which to show.
                switch (valType)
                {
                    case SPStatType.Date:
                        break;
                    case SPStatType.Position:
                        break;
                    case SPStatType.String:
                        break;
                    case SPStatType.U8:
                        break;
                    case SPStatType.U16:
                        break;
                    case SPStatType.Int:
                        valEditorInt.Visible = true;
                        valEditorInt.Maximum = int.MaxValue;
                        valEditorInt.Minimum = int.MinValue;
                        valEditorInt.Value = int.Parse(valueCell.Text);
                        break;
                    case SPStatType.U32:
                        valEditorInt.Visible = true;
                        valEditorInt.Maximum = uint.MaxValue;
                        valEditorInt.Minimum = uint.MinValue;
                        valEditorInt.Value = uint.Parse(valueCell.Text);
                        break;
                    case SPStatType.U64:
                        valEditorInt.Visible = true;
                        valEditorInt.Maximum = ulong.MaxValue;
                        valEditorInt.Minimum = ulong.MinValue;
                        valEditorInt.Value = ulong.Parse(valueCell.Text);
                        break;
                    case SPStatType.Label:
                        valEditorInt.Visible = true;
                        valEditorInt.Maximum = int.MaxValue;
                        valEditorInt.Minimum = int.MinValue;
                        valEditorInt.Value = int.Parse(valueCell.Text);
                        break;
                    case SPStatType.Float:
                        valEditorFloat.Visible = true;
                        valEditorFloat.Value = float.Parse(valueCell.Text);
                        break;
                    case SPStatType.Bool:
                        valEditorBool.Visible = true;
                        if (bool.Parse(valueCell.Text))
                            valEditorBool.SelectedIndex = 1;
                        else
                            valEditorBool.SelectedIndex = 0;
                        break;
                    default:
                        return;
                }
            }
            catch
            {
                // Some value couldn't be parsed, it was N/A, so just set it all to 0
                valEditorFloat.Value = 0;
                valEditorInt.Value = 0;
                valEditorBool.SelectedIndex = 0;
            }
           
        }

        private void listValues_Click(object sender, EventArgs e)
        {

        }

        private void cmdSetValue_Click(object sender, EventArgs e)
        {
            if (listValues.SelectedNode == null)
                return;

            // Get important cells
            Cell valueCell = listValues.SelectedNode.Cells[2];
            uint ID = (uint)listValues.SelectedNode.Tag;
            // Get the type
            SPStatType valType = GrandTheftAutoVData.SPStats[ID].Type;

            // determine which to show.
            switch (valType)
            {
                case SPStatType.Date:
                    break;
                case SPStatType.Position:
                    break;
                case SPStatType.String:
                    break;
                case SPStatType.U8:
                    break;
                case SPStatType.U16:
                    break;
                case SPStatType.Int:
                    valueCell.Text = valEditorInt.Value.ToString();
                    saveGame.progressInt32Values.Values[ID] = (int)valEditorInt.Value;
                    break;
                case SPStatType.U32:
                    valueCell.Text = valEditorInt.Value.ToString();
                    saveGame.progressUInt32Values.Values[ID] = (uint)valEditorInt.Value;
                    break;
                case SPStatType.U64:
                    valueCell.Text = valEditorInt.Value.ToString();
                    saveGame.progressInt64Values.Values[ID] = (long)valEditorInt.Value;
                    break;
                case SPStatType.Label:
                    valueCell.Text = valEditorInt.Value.ToString();
                    saveGame.progressInt32Values.Values[ID] = (int)valEditorInt.Value;
                    break;
                case SPStatType.Float:
                    valueCell.Text = valEditorFloat.Value.ToString();
                    saveGame.progressFloatValues.Values[ID] = (float)valEditorFloat.Value;
                    break;
                case SPStatType.Bool:
                    valueCell.Text = valEditorBool.SelectedItem.ToString();
                    saveGame.progressBoolValues.Values[ID] = ((saveGame.progressBoolValues.Values[ID] & 0x00FFFFFF) | (uint)(valEditorBool.SelectedIndex << 24));
                    break;
                default:
                    return;
            }
        }

        private void rbPackageEditor_SelectedRibbonTabChanged(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(RibbonControl))
                return;
            RibbonTabItem selectedItem = (sender as RibbonControl).SelectedRibbonTabItem;
            if (selectedItem == tabMain)
                refreshMainPageUIComponents();
            else if(selectedItem == tabValueEditor)
                if(changedMainTabValues)
                    refreshList();
        }

    }
}
