using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.Editors;
using StateofDecay;

namespace Horizon.PackageEditors.State_of_Decay.Controls
{
    public partial class NSCharacterObject : UserControl
    {
        public NSCharacter Character { get; set; }
        //private readonly bool IsBreakdownSave;
        private readonly StateofDecayGameMode GameMode;
        public NSCharacterObject(NSCharacter linker, StateofDecayGameMode gameMode)
        {
            Character = linker;
            GameMode = gameMode;
            //IsBreakdownSave = isBreakdown;

            InitializeComponent();

            cmbTraitChoice.Items.Clear();
            foreach (KeyValuePair<uint, string> trait in StateofDecayData.Traits)
            {
                cmbTraitChoice.Items.Add(trait.Value);
            }

            cmbTraitChoice.SelectedIndexChanged += TraitCmb_SelectedIndexChanged;

            foreach (KeyValuePair<uint, string> name in StateofDecayDataManager.GetData("CharacterNames", gameMode))
            {
                var nameItem = new ComboItem(name.Value) {Tag = name.Key};

                cmbFirstNames.Items.Add(nameItem);
                cmbLastNames.Items.Add(nameItem);
            }

            cmbFirstNames.Items.Add(new ComboItem("None"));
            cmbLastNames.Items.Add(new ComboItem("None"));

            Dictionary<uint, string> modeList = StateofDecayDataManager.GetData("CharacterModels", gameMode);
  
            foreach (KeyValuePair<uint, string> model in modeList)
            {
                cmbCharacterModels.Items.Add(new ComboItem(model.Value) {Tag = model.Key});
            }
            
            Read();

            // load max value setters
            btnMaxStamina.Tag = fStamina;
            btnMaxHealth.Tag = fHealth;

            btnMaxStamina.Click += BtnClick_MaxFloat;
            btnMaxHealth.Click += BtnClick_MaxFloat;
        }

        public void Read()
        {
            // display name
            cmbFirstNames.Text = StateofDecayDataManager.GetData("CharacterNames", GameMode)[Character.FirstName];

            cmbLastNames.Text = StateofDecayDataManager.GetData("CharacterNames", GameMode)[Character.LastName];

            cmbCharacterModels.Text = StateofDecayDataManager.GetData("CharacterModels", GameMode)[Character.Id];

            // load skill set
            foreach (var skill in Character.Skills)
            {
                if (StateofDecayData.Skills.ContainsKey(skill.Id))
                {
                    var skillEntry = StateofDecayData.Skills[skill.Id];
                    treeSkills.Nodes.Add(CreateSkillNode(skill, skillEntry.Name, skillEntry.Experience[skillEntry.Experience.Length - 1], skillEntry.Experience));
                }
            }
            // check for the first four basic skills
            foreach (var basicSkill in StateofDecayData.BasicSkills)
            {
                if ((!Character.Skills.Exists(sk => sk.Id == basicSkill)))
                {
                    var skillEntry = StateofDecayData.Skills[basicSkill];
                    var newSkill = new NSSkill { Id = basicSkill, Experience = 0 };
                    Character.Skills.Add(newSkill);

                    treeSkills.Nodes.Add(CreateSkillNode(newSkill,
                                                         skillEntry.Name,
                                                         skillEntry.Experience[skillEntry.Experience.Length - 1],
                                                         skillEntry.Experience));
                }
            }
            // add hidden skills acquired from traits
            foreach (KeyValuePair<uint, StateofDecayData.NSExpertise> skill in StateofDecayData.Skills)
            {
                if ((!Character.Skills.Exists(sk => sk.Id == skill.Key)) && skill.Value.Restrictions != null)
                {
                    foreach (uint restriction in skill.Value.Restrictions)
                    {
                        if (Character.Traits.Contains(restriction))
                        {
                            var skillEntry = StateofDecayData.Skills[skill.Key];
                            var newSkill = new NSSkill {Id = skill.Key, Experience = 0};
                            Character.Skills.Add(newSkill);

                            treeSkills.Nodes.Add(CreateSkillNode(newSkill, 
                                                                 skillEntry.Name,
                                                                 skillEntry.Experience[skillEntry.Experience.Length - 1],
                                                                 skillEntry.Experience));
                        }
                    }
                } 
            }

            // load trait list
            foreach (var trait in Character.Traits)
            {
                if (StateofDecayData.Traits.ContainsKey(trait))
                    treeTraits.Nodes.Add(CreateTraitNode(trait));
            }

            chkUnlockAsFriend.Checked = Character.ProgressAsFriend == 100;
            intFriendProg.Value = Character.ProgressAsFriend;
            fHealth.Value = Character.Health;
            fStamina.Value = Character.Stamina;
        }

        public void Write()
        {
            Character.ProgressAsFriend = (byte)(chkUnlockAsFriend.Checked ? 100 : intFriendProg.Value);
            Character.Health = (float) fHealth.Value;
            Character.Stamina = (float) fStamina.Value;

            //if (((uint)(cmbFirstNames.SelectedItem as ComboItem).Tag) != Character.FirstName || ((uint)(cmbLastNames.SelectedItem as ComboItem).Tag) != Character.LastName)
                //throw  new Exception("s");

            Character.FirstName = (uint) (cmbFirstNames.SelectedItem as ComboItem).Tag;
            Character.LastName = (uint)(cmbLastNames.SelectedItem as ComboItem).Tag;
            Character.Id = (uint)(cmbCharacterModels.SelectedItem as ComboItem).Tag;
        }


        private Node CreateSkillNode(NSSkill skill, string title, int maxValue, short[] levels)
        {
            var intInput = new IntegerInput
            {
                Tag = skill,
                Value = skill.Experience,
                MinValue = 0,
                MaxValue = maxValue,
                ShowUpDown = true
            };
            intInput.ValueChanged += SkillInt32_ValueChanged;

            var cmbInput = new ComboBox();
            for (var i = 0; i < levels.Count(); i++)
            {
                cmbInput.Items.Add((i + 1).ToString(CultureInfo.InvariantCulture));
            }
            cmbInput.Tag = levels;
            var levs = new List<short>(levels);
            var idx = 0;
            for (var i = 0; i < levs.Count; i++)
            {
                if (i == levs.Count - 1)
                {
                    idx = levs.Count - 1;
                    break;
                }
                if ((skill.Experience > levs[i] && skill.Experience < levs[i + 1]) || skill.Experience == levs[i])
                {
                    idx = i;
                    break;
                }
            }
            cmbInput.SelectedIndex = idx;
            cmbInput.SelectedIndexChanged += SkillCmb_SelectedIndexChanged;

            var node = new Node(title);
            node.Cells.Add(new Cell { HostedControl = cmbInput });
            node.Cells.Add(new Cell { HostedControl = intInput });

            return node;
        }
        private void SkillInt32_ValueChanged(object sender, EventArgs e)
        {
            var i = sender as IntegerInput;
            i.MaxValue = ushort.MaxValue;
            if (i != null)
                (i.Tag as NSSkill).Experience = (ushort)i.Value;
        }
        private void SkillCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            var input = sender as ComboBox;
            if (input != null)
            {
                var node = treeSkills.SelectedNode;
                var levels = (short[])input.Tag;
                (node.Cells[2].HostedControl as IntegerInput).Value = levels[input.SelectedIndex];
            }
        }

        private Node CreateTraitNode(uint key)
        {
            var node = new Node {Text = StateofDecayData.Traits[key], Tag = key};
            return node;
        }

        private void TraitTree_NodeSelected(object sender, AdvTreeNodeEventArgs e)
        {
            var input = e.Node.Cells[0];
            if (input != null)
            {
                uint newKey = StateofDecayData.Traits.First(t => t.Value == input.Text).Key;
                rTxtTraitDesc.Text = StateofDecayData.TraitsDescription[newKey];
                cmbTraitChoice.Text = input.Text;
            }
        }
        private void TraitCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            var input = sender as ComboBox;
            if (input != null)
            {
                var node = treeTraits.SelectedNode;
                var sKey = StateofDecayData.Traits.First(t => t.Value == input.Text);
                var oldKey = (uint)node.Tag;
                var newKey = sKey.Key;
                if (newKey == oldKey)
                    return;

                var newName = sKey.Value;
                var index = Character.Traits.FindIndex(0, t => t == oldKey);
                if (index == -1)
                {
                    Functions.UI.errorBox("Failed to find the index for the selected trait.");
                    return;
                }
                Character.Traits.Remove(oldKey);
                Character.Traits.Insert(index, newKey);

                node.Tag = newKey;
                node.Text = newName;
            }
        }
        private void BtnClick_ClearAllTraits(object sender, EventArgs e)
        {
            /*
            for (var i = 0; i < Character.Traits.Count; i++)
            {
                Character.Traits[i] = 0x00;
            }
            */
            foreach (Node node in treeTraits.Nodes)
            {
                var cmb = node.HostedControl as ComboBox;
                cmb.SelectedIndex = 0;
            }
        }

        private void BtnClick_MaxFloat(object sender, EventArgs e)
        {
            var fInput = ((sender as DevComponents.DotNetBar.ButtonX).Tag as DoubleInput);
            if (fInput != null)
                fInput.Value = fInput.MaxValue;
        }
    }
}
