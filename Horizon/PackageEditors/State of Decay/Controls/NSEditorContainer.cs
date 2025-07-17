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
    public partial class NSEditorContainer : UserControl
    {
        public NSObject ItemLinker;
        public readonly StateofDecayGameMode GameMode;
        public NSEditorContainer(NSObject itemLinker, uint gameVersion, StateofDecayGameMode gameMode)
        {
            ItemLinker = itemLinker;
            InitializeComponent();
            GameMode = gameMode;
            Read(gameVersion, gameMode);
        }

        private void Add(Control c)
        {
            c.Dock = DockStyle.Top;
            pnlControls.Controls.Add(c);
            c.BringToFront();
        }

        public void Read(uint gameVersion, StateofDecayGameMode gameMode)
        {
            pnlControls.Controls.Clear();

            switch (ItemLinker.ObjectType)
            {
                case NSObjectType.FireArm:
                    var gunObject = new NSInventoryGunObject((NSFireArm)ItemLinker);
                    Add(gunObject);
                    break;
                case NSObjectType.MeleeWeapon:
                    var weaponObject = new NSInventoryWeaponObject((NSMeeleWeapon)ItemLinker);
                    Add(weaponObject);
                    break;
                case NSObjectType.Consumable:
                case NSObjectType.Projectile:
                    var consumObject = new NSInventoryConsumableObject((NSConsumable)ItemLinker);
                    Add(consumObject);
                    break;
                case NSObjectType.Character:
                    var charObject = new NSCharacterObject((NSCharacter)ItemLinker, gameMode);
                    Add(charObject);
                    // character inventory object
                    Add(new NSInventoryListObject(((NSCharacter)ItemLinker).Inventory, gameVersion, gameMode, true));
                    break;
                case NSObjectType.Enclave:
                    // community inventory object
                    var invListObj = new NSInventoryListObject(((NSCommunity)ItemLinker).Inventory, gameVersion, gameMode, true);
                    Add(invListObj);
                    Add(new NSEnclaveObject(((NSCommunity)ItemLinker).Inventory, invListObj, gameMode));
                    break;
            }
        }

        public void Write()
        {
            foreach (Control c in pnlControls.Controls)
            {
                if (c.GetType() == typeof(NSInventoryGunObject))
                    ((NSInventoryGunObject)c).Write();
                else if (c.GetType() == typeof(NSInventoryWeaponObject))
                    ((NSInventoryWeaponObject)c).Write();
                else if (c.GetType() == typeof(NSInventoryConsumableObject))
                    ((NSInventoryConsumableObject)c).Write();
                else if (c.GetType() == typeof(NSCharacterObject))
                    ((NSCharacterObject)c).Write();
            }
        }
    }
}
