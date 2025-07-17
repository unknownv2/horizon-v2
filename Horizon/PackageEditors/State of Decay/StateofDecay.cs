using System;
using System.Drawing;
using System.Windows.Forms;
using Crysis;
using DevComponents.AdvTree;
using DevComponents.Editors;
using Horizon.Functions;
using Horizon.PackageEditors.State_of_Decay.Controls;
using StateofDecay;
using System.IO;

namespace Horizon.PackageEditors.State_of_Decay
{
    public partial class StateofDecay : EditorControl
    {
        private StateofDecaySave GameSave;
        private StateofDecaySave DefaultSave;
        private StateofDecayBreakdownSave BreakdownSave;
        private StateofDecayLifelineSave LifelineSave;

        private const string DefaultName = "Class3.ulsave";
        private const string SandboxName = "Sandbox.ulsave";
        private const string LifelineName = "Highway.ulsave";

        private bool injected;
        private bool ReloadingInventory;
        private StateofDecayGameMode SelectedGameMode;

        /*
        private bool IsBreakdownSave
        {
            get { return BreakdownSave != null && GameSave == BreakdownSave.GameSave; }
        }
        */

        public StateofDecay()
        {
            TitleID = FormID.StateofDecay; // 584111E8
            InitializeComponent();
            Size = new Size(617, 397);

#if INT2
            btnExtractSave.Visible = true;
            btnInjectSave.Visible = true;
            btnInjectBreakdownSave.Visible = true;
#endif
            
        }

        public override bool Entry()
        {
            if (DoesFileExist(DefaultName))
            {
                OpenStfsFile(DefaultName);
                var crypto = new CrysisCryptek(SettingAsByteArray(56), 0xCB);

                DefaultSave = new StateofDecaySave(IO, crypto);

                cmbGameType.Items.Add("Default");
            }
            if (DoesFileExist(SandboxName) && Package.Header.Metadata.Creator != 0)
            {
                var breakdownCrypto = new CrysisCryptek(SettingAsByteArray(56), 0xCB);
                BreakdownSave =
                    new StateofDecayBreakdownSave(Package.StfsContentPackage.GetEndianIO(SandboxName, true),
                                              breakdownCrypto, Package.Header.Metadata.Creator);

                cmbGameType.Items.Add("Breakdown");

            }
            if (DoesFileExist(LifelineName) && Package.Header.Metadata.Creator != 0)
            {
                var lifelineCrypto = new CrysisCryptek(SettingAsByteArray(56), 0xCB);
                LifelineSave = new StateofDecayLifelineSave(Package.StfsContentPackage.GetEndianIO(LifelineName, true),
                                                            lifelineCrypto, Package.Header.Metadata.Creator);

                cmbGameType.Items.Add("Lifeline");
            }
            if (DefaultSave == null && BreakdownSave == null && LifelineSave == null)
                return false;

            // load the default save file
            cmbGameType.SelectedIndex = 0;

            return true;
        }

        public override void Save()
        {
            if (injected) return;

            GameSave.TimeStamp = dtTimeStamp.Value;
            GameSave.Influence = (float)fInfluence.Value;
            GameSave.Fame = (float)fFame.Value;

            // save all items
            SaveInventoryListing(stcInventory);
            SaveInventoryListing(stcSupplyLocker);

            if(BreakdownSave != null)
                BreakdownSave.Save();

            if (LifelineSave != null)
                LifelineSave.Save();

            if(DefaultSave != null)
                DefaultSave.Save();

            DisplaySave();
        }

        private void CmbGameTypeIndexChanged(object sender, EventArgs e)
        {
            GameSave = null;
            SelectedGameMode = (StateofDecayGameMode)Enum.Parse(typeof(StateofDecayGameMode), cmbGameType.Items[cmbGameType.SelectedIndex].ToString());// cmbGameType.SelectedIndex;
            switch (SelectedGameMode)
            {
                case StateofDecayGameMode.Default:
                    if(DefaultSave != null)
                        GameSave = DefaultSave;
                    break;
                case StateofDecayGameMode.Breakdown:
                    if(BreakdownSave != null)
                        GameSave = BreakdownSave.GameSave;
                    break;
                case StateofDecayGameMode.Lifeline:
                    if(LifelineSave != null)
                        GameSave = LifelineSave.GameSave;
                    break;
            }
            /*
            if (BreakdownSave == null)
                GameSave = DefaultSave;
            else if (DefaultSave == null)
                GameSave = BreakdownSave.GameSave;
            else
                GameSave = cmbGameType.SelectedIndex == 0 ? DefaultSave : BreakdownSave.GameSave;
            */
            if(GameSave == null)
                throw new Exception("SoD: failed to load any game save");

            DisplaySave();
        }

        private void SaveInventoryListing(DevComponents.DotNetBar.SuperTabControl superTabControl)
        {
            foreach (DevComponents.DotNetBar.SuperTabItem sti in superTabControl.Tabs)
            {
                SaveInventoryItem(sti);
            }
        }
        private void SaveInventoryItem(DevComponents.DotNetBar.SuperTabItem sti)
        {
            var obj = sti.AttachedControl.Controls[0] as NSEditorContainer;
            if(obj != null)
                obj.Write();
        }
        private void DisplaySave()
        {
            // load characters in your community
            LoadCharacterTree();

            // load supply locker inventory
            LoadSupplyLockerTree();
            
            // load game globals
            treePlayerStats.Nodes.Clear();

            foreach (var nsGlobal in GameSave.Globals)
            {
                if (StateofDecayData.Globals.ContainsKey(nsGlobal.Id))
                    treePlayerStats.Nodes.Add(CreateFloatNode(nsGlobal, StateofDecayData.Globals[nsGlobal.Id],
                                                              float.MaxValue));
            }

            // save timestamp
            dtTimeStamp.Value = GameSave.TimeStamp;

            // fame and influence
            fInfluence.Value = GameSave.Influence;
            fFame.Value = GameSave.Fame;
        }

        private void LoadCharacterTree()
        {
            treeInventory.Nodes.Clear();
            foreach (DevComponents.DotNetBar.SuperTabItem sti in stcInventory.Tabs)
            {
                sti.Tag = null;
                sti.AttachedControl.Controls.Clear();
            }
            stcInventory.Tabs.Clear();

            foreach (var character in GameSave.RtsSave.Characters)
            {
                if (!character.IsLoaded || !character.IsCommunityMember) continue;

                string firstName, lastName;
                switch (SelectedGameMode)
                {
                    case StateofDecayGameMode.Default:

                        firstName = StateofDecayData.CharacterNames[character.FirstName];
                        lastName = StateofDecayData.CharacterNames[character.LastName];
                        break;
                    case StateofDecayGameMode.Breakdown:
                        firstName = StateofDecayData.BreakdownCharacterNames[character.FirstName];
                        lastName = StateofDecayData.BreakdownCharacterNames[character.LastName];
                        
                        break;
                    case StateofDecayGameMode.Lifeline:
                        firstName = StateofDecayData.LifelineCharacterNames[character.FirstName];
                        lastName = StateofDecayData.LifelineCharacterNames[character.LastName];
                        break;
                    default:
                        throw new Exception("SoD: Could not find character name from database.");
                }

                var characterNode = new Node(string.Format("{0} {1}",
                    firstName, lastName)){ Tag = character };

                foreach (var inventoryItem in character.Inventory)
                {
                    if (inventoryItem.Type != NSInventoryItemType.Empty)
                        characterNode.Nodes.Add(CreateInventoryItemNode(inventoryItem));
                }
                treeInventory.Nodes.Add(characterNode);
            }
        }

        private void LoadSupplyLockerTree()
        {
            treeSupplyLocker.Nodes.Clear();
            foreach (DevComponents.DotNetBar.SuperTabItem sti in stcSupplyLocker.Tabs)
            {
                sti.Tag = null;
                sti.AttachedControl.Controls.Clear();
            }
            stcSupplyLocker.Tabs.Clear();
            foreach (var enclave in GameSave.Enclaves)
            {
                if (enclave.Id == 0) continue;

                var sLocker = new Node(StateofDecayData.Enclaves[enclave.Id]) {Tag = enclave};
                foreach (var item in enclave.Inventory)
                {
                    if (item.Type != NSInventoryItemType.Empty)
                        sLocker.Nodes.Add(CreateInventoryItemNode(item));
                }
                treeSupplyLocker.Nodes.Add(sLocker);
            }
        }

        private Node CreateInventoryItemNode(NSInventoryItem inventoryItem)
        {
            var node = new Node {Tag = inventoryItem};

            switch (inventoryItem.Type)
            {
                case NSInventoryItemType.FireArm:
                    node.Text = StateofDecayDataManager.GetData("FireArms", SelectedGameMode)[inventoryItem.Id];
                    break;
                case NSInventoryItemType.MeleeWeapon:
                    node.Text = StateofDecayDataManager.GetData("MeleeWeapons", SelectedGameMode)[inventoryItem.Id];
                    break;
                case NSInventoryItemType.Consumable:
                    node.Text = StateofDecayData.Consumables[inventoryItem.Id];
                    break;
                case NSInventoryItemType.Projectile:
                    node.Text = StateofDecayData.Projectiles[inventoryItem.Id];
                    break;
                case NSInventoryItemType.Backpack:
                    node.Text = StateofDecayData.Backpacks[inventoryItem.Id];
                    break;
            }
            node.Tag = inventoryItem;
            return node;
        }

        private void TreeInventory_AfterNodeDeselect(object sender, AdvTreeNodeEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                if (!ReloadingInventory)
                {
                    ReloadingInventory = true;
                    if (sender == treeInventory) // when we are editing the character tree
                    {
                        var character = (NSCharacter)(e.Node.Tag);
                        foreach (DevComponents.DotNetBar.SuperTabItem sti in (sender == treeInventory ? stcInventory : stcSupplyLocker).Tabs)
                        {
                            var obj = sti.AttachedControl.Controls[0] as NSEditorContainer;
                            if (obj != null && obj.ItemLinker.ObjectType == NSObjectType.Character)
                            {
                                obj.Write();

                                string firstName, lastName;
                                switch (obj.GameMode)
                                {
                                    case StateofDecayGameMode.Default:
                                        firstName = StateofDecayData.CharacterNames[character.FirstName];
                                        lastName = StateofDecayData.CharacterNames[character.LastName];
                                        break;
                                    case StateofDecayGameMode.Breakdown:
                                        firstName = StateofDecayData.BreakdownCharacterNames[character.FirstName];
                                        lastName = StateofDecayData.BreakdownCharacterNames[character.LastName];

                                        break;
                                    case StateofDecayGameMode.Lifeline:
                                        firstName = StateofDecayData.LifelineCharacterNames[character.FirstName];
                                        lastName = StateofDecayData.LifelineCharacterNames[character.LastName];
                                        break;
                                    default:
                                        throw new Exception("SoD: Could not find character name from database.");
                                }
                                e.Node.Text = string.Format("{0} {1}", firstName, lastName);
                            }
                        }

                        if (character.Inventory.Modified)
                        {
                            var characterNode = e.Node;
                            characterNode.Nodes.Clear();

                            foreach (var inventoryItem in character.Inventory)
                            {
                                if (inventoryItem.Type != NSInventoryItemType.Empty)
                                    characterNode.Nodes.Add(CreateInventoryItemNode(inventoryItem));
                            }
                            // we finished reloading the inventory
                            character.Inventory.Modified = false;
                            // close all opened tabs
                            foreach (DevComponents.DotNetBar.SuperTabItem sti in stcInventory.Tabs)
                            {
                                sti.Tag = null;
                                sti.AttachedControl.Controls.Clear();
                            }
                            stcInventory.Tabs.Clear();
                        }
                    }
                    else if (sender == treeSupplyLocker) // we are editing the enclaves
                    {
                        var enclave = (NSCommunity) (e.Node.Tag);
                        if (enclave.Inventory.Modified)
                        {
                            var enclaveNode = e.Node;
                            enclaveNode.Nodes.Clear();
                            foreach (var inventoryItem in enclave.Inventory)
                            {
                                if (inventoryItem.Type != NSInventoryItemType.Empty)
                                    enclaveNode.Nodes.Add(CreateInventoryItemNode(inventoryItem));
                            }
                            // we finished reloading the inventory
                            enclave.Inventory.Modified = false;
                            // close all opened tabs
                            foreach (DevComponents.DotNetBar.SuperTabItem sti in stcSupplyLocker.Tabs)
                            {
                                sti.Tag = null;
                                sti.AttachedControl.Controls.Clear();
                            }
                            stcSupplyLocker.Tabs.Clear();
                            stcSupplyLocker.Update();
                        }
                    }
                    ReloadingInventory = false;
                }
            }
            else
            {
                SaveInventoryListing(sender == treeInventory ? stcInventory : stcSupplyLocker);
            }
        }

        private void TreeInventory_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
        {
            var tree = sender as AdvTree;
            if (ReloadingInventory)
                return;

            if(tree == null || tree.SelectedNode == null)
                return;

            if (tree.SelectedNode.Tag == null)
            {
                UI.messageBox("Invalid tag selected!");
                return;
            }

            var objLink = (NSObject) (tree.SelectedNode.Tag);
            if (tree.SelectedNode.Parent != null)
            {
                var iLink = (NSInventoryItem) (tree.SelectedNode.Tag);
                if (iLink.Type == NSInventoryItemType.Backpack) // no values/options for these
                    return;
            }

            var superTabControl = tree == treeInventory ? stcInventory : stcSupplyLocker;
            DevComponents.DotNetBar.SuperTabItem sti = null;
            foreach (DevComponents.DotNetBar.SuperTabItem sti2 in superTabControl.Tabs)
            {
                if (sti2.Tag == tree.SelectedNode.Tag)
                {
                    sti = sti2;
                    sti.AttachedControl.Controls.Clear();
                    break;
                }
            }

            if (sti == null)
                sti = superTabControl.CreateTab(tree.SelectedNode.Text);

            sti.Tag = tree.SelectedNode.Tag;

            var editor = new NSEditorContainer(objLink, GameSave.GameVersion, SelectedGameMode) { Dock = DockStyle.Fill };
            editor.Dock = DockStyle.Fill;
            sti.AttachedControl.Controls.Add(editor);
            stcInventory.SelectedTab = sti;
        }

        private void BtnTab_Click(object sender, EventArgs e)
        {
            Size = new Size((((DevComponents.DotNetBar.RibbonTabItem) sender).Name != "tabMain") ? 772 : 617,
                            Size.Height);
        }

        private void BtnExtractClick(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog {FileName = cmbGameType.Text + ".ulsave"};
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            switch (cmbGameType.Text)
            {
                case "Default":
                    GameSave.ExtractDataBufferToFile(sfd.FileName);
                    break;
                case "Breakdown":
                    BreakdownSave.ExtractDataBufferToFile(sfd.FileName);
                    break;
                case "Lifeline":
                    LifelineSave.ExtractDataBufferToFile(sfd.FileName);
                    break;
            }
        }

        private void BtnInjectClick(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            switch (cmbGameType.Text)
            {
                case "Default":
                    GameSave.InjectDataBuffer(File.ReadAllBytes(ofd.FileName));
                    break;
                case "Breakdown":
                    BreakdownSave.InjectData(File.ReadAllBytes(ofd.FileName));
                    break;
                case "Lifeline":
                    LifelineSave.InjectData(File.ReadAllBytes(ofd.FileName));
                    break;
            }
            
            injected = true;
        }

        
        private void BtnInjectBreakdownSaveClick(object sender, EventArgs e)
        {
            
            var ofd = new OpenFileDialog();

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            var breakdownCrypto = new CrysisCryptek(SettingAsByteArray(56), 0xCB);
            var ms = new MemoryStream();
            breakdownCrypto.Crypt(File.ReadAllBytes(ofd.FileName), ms, true);

            // overwrite data
            Package.StfsContentPackage.InjectFileFromArray(SandboxName, ms.ToArray());
            injected = true; 
            
        }
        
        private Node CreateFloatNode(NSGlobal global, string title, double maxValue)
        {
            var dbInput = new DoubleInput
                {
                    Tag = global.Id,
                    Value = global.Value,
                    MinValue = 0,
                    MaxValue = maxValue,
                    ShowUpDown = true
                };
            dbInput.ValueChanged += Float_ValueChange;

            var node = new Node(title);
            node.Cells.Add(new Cell {HostedControl = dbInput});

            return node;
        }

        private void Float_ValueChange(object sender, EventArgs e)
        {
            var i = sender as DoubleInput;
            if (i != null)
                GameSave.Globals[(uint) i.Tag] = Convert.ToSingle(i.Value);
        }

        private void SuperTabControl_TabItemClose(object sender, DevComponents.DotNetBar.SuperTabStripTabItemCloseEventArgs e)
        {
            if (e.Tab == null) return;

            SaveInventoryItem(e.Tab as DevComponents.DotNetBar.SuperTabItem);
        }
    }
}
