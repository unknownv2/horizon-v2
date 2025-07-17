using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;
using StateofDecay;

namespace Horizon.PackageEditors.State_of_Decay.Controls
{
    public partial class NSInventoryListObject : UserControl
    {
        public NSInventory Inventory;
        public static string[] ItemCategories;
        public static List<Dictionary<uint, string>> ItemLists;
        private bool SelectingItem;
        private readonly uint GameVersion;
        private readonly bool IsEnclave;
        public Dictionary<uint, StateofDecayData.NSMeleeWeaponProperties> MeleeWeaponProperties;
        public Dictionary<uint, StateofDecayData.NSFireArmData> FireArmProperties;

        public NSInventoryListObject(NSInventory linker, uint gameVersion, StateofDecayGameMode gameMode, bool isEnclave)
        {
            Inventory = linker;
            GameVersion = gameVersion;
            IsEnclave = isEnclave;
            switch (gameMode)
            {
                    case StateofDecayGameMode.Default:
                    FireArmProperties = StateofDecayData.FireArmProperties;
                    MeleeWeaponProperties = StateofDecayData.MeleeWeaponProperties;
                    break;
                    case StateofDecayGameMode.Breakdown:
                    FireArmProperties = StateofDecayData.BreakdownFireArmProperties;
                    MeleeWeaponProperties = StateofDecayData.BreakdownMeleeWeaponProperties;
                    break;
                    case StateofDecayGameMode.Lifeline:
                    MeleeWeaponProperties = StateofDecayData.LifelineMeleeWeaponProperties;
                    FireArmProperties = StateofDecayData.LifelineFireArmProperties;
                    break;
            }

            InitializeComponent();

            if (ItemCategories == null)
            {
                ItemCategories = new[] { "Empty", "Ammunition", "Backpacks", "Consumables", "Guns", "Weapons" };
            }

            ItemLists = new List<Dictionary<uint, string>>
                {
                    StateofDecayData.Empty,
                    StateofDecayData.Projectiles,
                    StateofDecayData.Backpacks,
                    StateofDecayData.Consumables,
                    StateofDecayDataManager.GetData("FireArms", gameMode),
                    StateofDecayDataManager.GetData("MeleeWeapons", gameMode)
                };

            cmbCategory.Items.AddRange(ItemCategories);

            Read();
        }

        public void Read()
        {
            treeInventoryList.Nodes.Clear();
            foreach (var inventoryItem in Inventory)
            {
                treeInventoryList.Nodes.Add(CreateFItemNode(inventoryItem));
            }
        }
        private Node CreateFItemNode(NSInventoryItem item)
        {
            /*
            var cmbCategoryInput = new ComboBox();
            cmbCategoryInput.Items.AddRange(ItemCategories);
            var cmbItemInput = new ComboBox();
            var items = ItemLists[(int)item.Type];
            foreach (KeyValuePair<uint, string> info in items)
            {
                cmbItemInput.Items.Add(info.Value);
            }
            cmbItemInput.Text = items[item.Id];
            cmbItemInput.Tag = item;

            cmbCategoryInput.SelectedIndex = (int)item.Type;

            cmbCategoryInput.SelectedIndexChanged += InvCmbCategory_SelectedIndexChanged;
            cmbItemInput.SelectedIndexChanged += InvCmbItem_SelectedIndexChanged;
            */
            //var node = new Node { HostedControl = cmbCategoryInput };
            //node.Cells.Add(new Cell { HostedControl = cmbItemInput });
            var node = new Node(ItemCategories[(int)item.Type]);
            node.Cells.Add(new Cell(ItemLists[(int)item.Type].ContainsKey(item.Id) ? (ItemLists[(int)item.Type][item.Id]) : item.Id.ToString("X")));
            node.Tag = item;
            return node;
        }
        /*
        private Node CreateItemNode(NSInventoryItem item)
        {
            var cmbCategoryInput = new ComboBox();
            cmbCategoryInput.Items.AddRange(ItemCategories);
            var cmbItemInput = new ComboBox();
            var items = ItemLists[(int)item.Type];
            foreach (KeyValuePair<uint, string> info in items)
            {
                cmbItemInput.Items.Add(info.Value);
            }
            cmbItemInput.Text = items[item.Id];
            cmbItemInput.Tag = item;

            cmbCategoryInput.SelectedIndex = (int)item.Type;

            cmbCategoryInput.SelectedIndexChanged += InvCmbCategory_SelectedIndexChanged;
            cmbItemInput.SelectedIndexChanged += InvCmbItem_SelectedIndexChanged;

            var node = new Node { HostedControl = cmbCategoryInput };
            node.Cells.Add(new Cell { HostedControl = cmbItemInput });
            return node;
        }
        */
        private void InvCmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            var node = treeInventoryList.SelectedNode;
            if (node == null || node.Tag == null)
                return;

            // reset inventory entry
            var inventoryItem = (node.Tag as NSInventoryItem);

            cmbItem.Items.Clear();
            var items = ItemLists[(sender as DevComponents.DotNetBar.Controls.ComboBoxEx).SelectedIndex];
            foreach (KeyValuePair<uint, string> info in items)
            {
                cmbItem.Items.Add(info.Value);
            }

            if (SelectingItem) return;


            // empty object
            inventoryItem.Id = 0;
            inventoryItem.Type = NSInventoryItemType.Empty;

            // set the flag for resetting the tree list
            Inventory.Modified = true;
        }

        private void InvCmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectingItem) return;

            var node = treeInventoryList.SelectedNode;
            if(node == null || node.Tag == null)
                return;

            var itemList = ItemLists[cmbCategory.SelectedIndex];

            var itemIndex = Inventory.FindIndex(t => t == (NSInventoryItem)node.Tag);
            var itemType = (NSInventoryItemType)cmbCategory.SelectedIndex;

            switch (itemType)
            {
                case NSInventoryItemType.Consumable:
                case NSInventoryItemType.Projectile:
                    Inventory[itemIndex] = new NSConsumable();
                    break;
                case NSInventoryItemType.FireArm:
                    Inventory[itemIndex] = new NSFireArm();
                    break;
                case NSInventoryItemType.MeleeWeapon:
                    Inventory[itemIndex] = new NSMeeleWeapon();
                    break;
            }

            Inventory[itemIndex].Id = itemList.FirstOrDefault(i => i.Value == cmbItem.Text).Key;
            Inventory[itemIndex].Type = itemType;

            node.Cells[0].Text = cmbCategory.Text;
            node.Cells[1].Text = cmbItem.Text;

            node.Tag = Inventory[itemIndex];

            Inventory.Modified = true;
        }

        private void BtnClick_MaxAllWeaponAttributes(object sender, EventArgs e)
        {
            foreach (NSInventoryItem inventoryItem in Inventory)
            {
                if(inventoryItem != null)
                {
                    MaxInventoryItemProperties(inventoryItem);
                }
            }

            //Inventory.Modified = true;
        }

        private void BtnClick_MaxAllSelected(object sender, EventArgs e)
        {
            foreach (Node node in treeInventoryList.SelectedNodes)
            {
                var item = node.Tag as NSInventoryItem;
                if (item != null)
                {
                    MaxInventoryItemProperties(item);
                }
            }
        }

        private void MaxInventoryItemProperties(NSInventoryItem inventoryItem)
        {
            if (inventoryItem == null)
            {
                throw new Exception("SoD: invalid inventory item found  [Error: 0x10]");
            }

            if (GameVersion < 0x53560005)
            {
                switch (inventoryItem.ObjectType)
                {
                    case NSObjectType.Consumable:
                    case NSObjectType.Projectile:
                        (inventoryItem as NSConsumable).Value = byte.MaxValue;
                        break;
                    case NSObjectType.FireArm:
                        var fireArm = (inventoryItem as NSFireArm);
                        fireArm.Clip = fireArm.SilencerShots = byte.MaxValue;
                        fireArm.Durability = fireArm.DurabilityCritical = ushort.MaxValue;
                        break;
                    case NSObjectType.MeleeWeapon:
                        var meleeWeapon = (inventoryItem as NSMeeleWeapon);
                        meleeWeapon.Durability = meleeWeapon.DurabilityMax = ushort.MaxValue;
                        break;
                }
            }
            else
            {
                switch (inventoryItem.ObjectType)
                {
                    case NSObjectType.Projectile:
                        if ((inventoryItem as NSConsumable) == null)
                            throw new Exception("SoD: could not find the requested ammunition type [Error: 0x04]");

                        (inventoryItem as NSConsumable).Value = (byte)(IsEnclave ? 255 : 30);
                        break;

                    case NSObjectType.Consumable:
                        if ((inventoryItem as NSConsumable) == null)
                            throw new Exception("SoD: could not find the requested consumable [Error: 0x03]");

                        (inventoryItem as NSConsumable).Value = (byte)(IsEnclave ? 255 : 5);
                        break;

                    case NSObjectType.FireArm:
                        var fireArm = (inventoryItem as NSFireArm);
                        if (fireArm == null)
                            throw new Exception("SoD: could not find the requested firearm [Error: 0x01]");

                        var fireArmProp = FireArmProperties[fireArm.Id];

                        fireArm.Clip = fireArmProp.ClipMax;
                        fireArm.SilencerShots = byte.MaxValue;
                        fireArm.Durability = fireArmProp.DurabilityMax;
                        fireArm.DurabilityCritical = fireArmProp.DurabilityCriticalMax;
                        break;

                    case NSObjectType.MeleeWeapon:
                        var meleeWeapon = (inventoryItem as NSMeeleWeapon);
                        if (meleeWeapon == null)
                            throw new Exception("SoD: could not find the requested melee weapon [Error: 0x02]");

                        var meleeWeaponProp = MeleeWeaponProperties[meleeWeapon.Id];

                        meleeWeapon.Durability = meleeWeaponProp.RemainingDurabilityMax;
                        meleeWeapon.DurabilityMax = meleeWeaponProp.DurabilityMax;
                        break;
                }
            }
            Inventory.Modified = true;
        }
        private void AdvTree_AfterInvItemSelect(object sender, AdvTreeNodeEventArgs e)
        {
            // more than one selected means we shouldn't allow a change (yet)
            if (treeInventoryList.SelectedNodes.Count > 1)
            {
                cmbCategory.Text = cmbItem.Text = string.Empty;
                return;
            }
            // continue and allow switching
            if(e.Node == null)
                return;

            var item = e.Node.Tag as NSInventoryItem;
            if(item == null)
                return;

            SelectingItem = true;

            cmbCategory.Text = ItemCategories[(int)item.Type];
            cmbItem.Text = ItemLists[(int) item.Type][item.Id];

            SelectingItem = false;
        }
    }
}
