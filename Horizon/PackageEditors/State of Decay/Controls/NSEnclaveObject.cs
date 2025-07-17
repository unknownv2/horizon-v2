using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StateofDecay;

namespace Horizon.PackageEditors.State_of_Decay.Controls
{
    public partial class NSEnclaveObject : UserControl
    {
        private readonly NSInventory Inventory;
        private readonly NSInventoryListObject listLinker;
        private bool _hasHitItemLimit;
        private readonly StateofDecayGameMode GameMode;

        public NSEnclaveObject(NSInventory linker, NSInventoryListObject inventoryListLinker, StateofDecayGameMode gameMode)
        {
            Inventory = linker;
            listLinker = inventoryListLinker;
            GameMode = gameMode;

            InitializeComponent();
        }
        private void AddAllFromList(Dictionary<uint, string> itemList)
        {
            if(_hasHitItemLimit)
            {
                Functions.UI.messageBox("Reached locker item limit");
                return;
            }

            foreach (KeyValuePair<uint, string> item in itemList)
            {
                if (Inventory.Exists(nsinv => nsinv.Id == item.Key)) continue;
                if (Inventory.AddNewItem(new NSMeeleWeapon {Id = item.Key}) == -2)
                {
                    _hasHitItemLimit = true;
                }
            }
        }
        private bool HasReachedItemLimit(int errorCode)
        {
            if (errorCode == -2)
            {
                Functions.UI.messageBox("Reached locker item limit");
                listLinker.Read();
                return true;
            }
            return false;
        }
        private void BtnClick_AddAllItems(object sender, EventArgs e)
        {
            /*
            AddAllFromList(StateofDecayData.MeleeWeapons);
            AddAllFromList(StateofDecayData.Projectiles);
            AddAllFromList(StateofDecayData.Consumables);
            AddAllFromList(StateofDecayData.FireArms);
            */
            
            foreach (KeyValuePair<uint, string> meleeWeapon in StateofDecayDataManager.GetData("MeleeWeapons" , GameMode))
            {
                if (!Inventory.Exists(nsinv => nsinv.Id == meleeWeapon.Key))
                {
                    if(HasReachedItemLimit(Inventory.AddNewItem(new NSMeeleWeapon { Id = meleeWeapon.Key })))
                        return;
                }
            }
            foreach (KeyValuePair<uint, string> projectile in StateofDecayData.Projectiles)
            {
                if (!Inventory.Exists(nsinv => nsinv.Id == projectile.Key))
                {
                    if(HasReachedItemLimit(Inventory.AddNewItem(new NSConsumable { Id = projectile.Key, Type = NSInventoryItemType.Projectile })))
                        return;
                }
            }
            foreach (KeyValuePair<uint, string> consumable in StateofDecayData.Consumables)
            {
                if (!Inventory.Exists(nsinv => nsinv.Id == consumable.Key))
                {
                    if(HasReachedItemLimit(Inventory.AddNewItem(new NSConsumable { Id = consumable.Key })))
                        return;
                }
            }
            foreach (KeyValuePair<uint, string> fireArm in StateofDecayDataManager.GetData("FireArms", GameMode))
            {
                if (!Inventory.Exists(nsinv => nsinv.Id == fireArm.Key))
                {
                    if(HasReachedItemLimit(Inventory.AddNewItem(new NSFireArm { Id = fireArm.Key })))
                        return;
                }
            }
            
            //Inventory.Modified = true;

            listLinker.Read();
        }
    }
}
