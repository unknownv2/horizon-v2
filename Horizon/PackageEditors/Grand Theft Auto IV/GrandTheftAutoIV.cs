using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GrandTheftAutoIV;

namespace Horizon.PackageEditors.Grand_Theft_Auto_IV
{
    public partial class GrandTheftAutoIV : EditorControl
    {
        /// <summary>
        /// Our field title ID
        /// </summary>
        //public static readonly string FID = "545407F2";

        /// <summary>
        /// Our class to handle our GTA IV save.
        /// </summary>
        public GrandTheftAutoIVClass GTA_IV_Class { get; set; }

        /// <summary>
        /// Our default constructor for this class.
        /// </summary>
        public GrandTheftAutoIV()
        {
            InitializeComponent();
            TitleID = FormID.GrandTheftAutoIV;
            
            uintMoney.MaxValue = int.MaxValue;
            uintMoney.MinValue = 0;
            floatArmor.Maximum = 100000000000;
            floatHealth.Maximum = 100000000000;
        }

        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            //Open our first file.
            if (!this.OpenStfsFile(0))
                return false;
            
            //Read our save
            GTA_IV_Class = new GrandTheftAutoIVClass(IO);

            //Set our form's text
            this.Text = GTA_IV_Class.Episode_Version.ToString().Replace('_', ' ').Replace("GTA IV", "GTA IV:");

            //Set our money
            uintMoney.Value = GTA_IV_Class.Money;
            //Set our health and armor
            if (GTA_IV_Class.Health > (float)floatHealth.Maximum)
                GTA_IV_Class.Health = (float)floatHealth.Maximum;
            floatHealth.Value = (decimal)GTA_IV_Class.Health;

            if (GTA_IV_Class.Armor > (float)floatArmor.Maximum)
                GTA_IV_Class.Armor = (float)floatArmor.Maximum;
            floatArmor.Value = (decimal)GTA_IV_Class.Armor;

            //Clear our combobox items
            comboHandgun.Items.Clear();
            comboHeavy.Items.Clear();
            comboMelee.Items.Clear();
            comboRifle.Items.Clear();
            comboShotgun.Items.Clear();
            comboSMG.Items.Clear();
            comboSniper.Items.Clear();
            comboThrown.Items.Clear();
            comboUnarmed.Items.Clear();

            //Load our combobox items
            comboHandgun.Items.AddRange(GTA_IV_Class.GetWeaponNamesForType(GrandTheftAutoIVClass.WeaponSlotType.HANDGUN));
            comboHeavy.Items.AddRange(GTA_IV_Class.GetWeaponNamesForType(GrandTheftAutoIVClass.WeaponSlotType.HEAVY));
            comboMelee.Items.AddRange(GTA_IV_Class.GetWeaponNamesForType(GrandTheftAutoIVClass.WeaponSlotType.MELEE));
            comboRifle.Items.AddRange(GTA_IV_Class.GetWeaponNamesForType(GrandTheftAutoIVClass.WeaponSlotType.RIFLE));
            comboShotgun.Items.AddRange(GTA_IV_Class.GetWeaponNamesForType(GrandTheftAutoIVClass.WeaponSlotType.SHOTGUN));
            comboSMG.Items.AddRange(GTA_IV_Class.GetWeaponNamesForType(GrandTheftAutoIVClass.WeaponSlotType.SMG));
            comboSniper.Items.AddRange(GTA_IV_Class.GetWeaponNamesForType(GrandTheftAutoIVClass.WeaponSlotType.SNIPER));
            comboThrown.Items.AddRange(GTA_IV_Class.GetWeaponNamesForType(GrandTheftAutoIVClass.WeaponSlotType.THROWN));
            comboUnarmed.Items.AddRange(GTA_IV_Class.GetWeaponNamesForType(GrandTheftAutoIVClass.WeaponSlotType.UNARMED));
            
            //Loop for each slot
            foreach(GrandTheftAutoIVClass.WeaponSlot slot in GTA_IV_Class.Weapon_Slots)
                //Determine our type and set data accordingly.
                switch (slot.Weapon_Slot_Type)
                {
                    case GrandTheftAutoIVClass.WeaponSlotType.UNARMED:
                        comboUnarmed.SelectedItem = slot.Weapon_Identifier_Str;
                        intAmmoUnarmed.Value = slot.Ammunition;
                        break;
                    case GrandTheftAutoIVClass.WeaponSlotType.THROWN:
                        comboThrown.SelectedItem = slot.Weapon_Identifier_Str;
                        intAmmoThrown.Value = slot.Ammunition;
                        break;
                    case GrandTheftAutoIVClass.WeaponSlotType.SNIPER:
                        comboSniper.SelectedItem = slot.Weapon_Identifier_Str;
                        intAmmoSniper.Value = slot.Ammunition;
                        break;
                    case GrandTheftAutoIVClass.WeaponSlotType.SMG:
                        comboSMG.SelectedItem = slot.Weapon_Identifier_Str;
                        intAmmoSMG.Value = slot.Ammunition;
                        break;
                    case GrandTheftAutoIVClass.WeaponSlotType.SHOTGUN:
                        comboShotgun.SelectedItem = slot.Weapon_Identifier_Str;
                        intAmmoShotgun.Value = slot.Ammunition;
                        break;
                    case GrandTheftAutoIVClass.WeaponSlotType.RIFLE:
                        comboRifle.SelectedItem = slot.Weapon_Identifier_Str;
                        intAmmoRifle.Value = slot.Ammunition;
                        break;
                    case GrandTheftAutoIVClass.WeaponSlotType.MELEE:
                        comboMelee.SelectedItem = slot.Weapon_Identifier_Str;
                        intAmmoMelee.Value = slot.Ammunition;
                        break;
                    case GrandTheftAutoIVClass.WeaponSlotType.HEAVY:
                        comboHeavy.SelectedItem = slot.Weapon_Identifier_Str;
                        intAmmoHeavy.Value = slot.Ammunition;
                        break;
                    case GrandTheftAutoIVClass.WeaponSlotType.HANDGUN:
                        comboHandgun.SelectedItem = slot.Weapon_Identifier_Str;
                        intAmmoHandgun.Value = slot.Ammunition;
                        break;
                }

            //Set our picture as our first combobox item
            SetPreviewImage(comboUnarmed.SelectedItem.ToString());

            //Our file is read correctly.
            return true;
        }
        public override void Save()
        {
            //Set our values
            GTA_IV_Class.Money = uintMoney.Value;
            GTA_IV_Class.Health = (float)floatHealth.Value;
            GTA_IV_Class.Armor = (float)floatArmor.Value;

            //Loop for each weapon slot.
            foreach(GrandTheftAutoIVClass.WeaponSlot slot in GTA_IV_Class.Weapon_Slots)
                //Determine our type and handle it correctly.
                switch (slot.Weapon_Slot_Type)
                {
                    case GrandTheftAutoIVClass.WeaponSlotType.UNARMED:
                        slot.Weapon_Identifier = GrandTheftAutoIVClass.WeaponStringToID(comboUnarmed.SelectedItem.ToString());
                        slot.Ammunition = (ushort)intAmmoUnarmed.Value;
                        break;
                    case GrandTheftAutoIVClass.WeaponSlotType.THROWN:
                        slot.Weapon_Identifier = GrandTheftAutoIVClass.WeaponStringToID(comboThrown.SelectedItem.ToString());
                        slot.Ammunition = (ushort)intAmmoThrown.Value;
                        break;
                    case GrandTheftAutoIVClass.WeaponSlotType.SNIPER:
                        slot.Weapon_Identifier = GrandTheftAutoIVClass.WeaponStringToID(comboSniper.SelectedItem.ToString());
                        slot.Ammunition = (ushort)intAmmoSniper.Value;
                        break;
                    case GrandTheftAutoIVClass.WeaponSlotType.SMG:
                        slot.Weapon_Identifier = GrandTheftAutoIVClass.WeaponStringToID(comboSMG.SelectedItem.ToString());
                        slot.Ammunition = (ushort)intAmmoSMG.Value;
                        break;
                    case GrandTheftAutoIVClass.WeaponSlotType.SHOTGUN:
                        slot.Weapon_Identifier = GrandTheftAutoIVClass.WeaponStringToID(comboShotgun.SelectedItem.ToString());
                        slot.Ammunition = (ushort)intAmmoShotgun.Value;
                        break;
                    case GrandTheftAutoIVClass.WeaponSlotType.RIFLE:
                        slot.Weapon_Identifier = GrandTheftAutoIVClass.WeaponStringToID(comboRifle.SelectedItem.ToString());
                        slot.Ammunition = (ushort)intAmmoRifle.Value;
                        break;
                    case GrandTheftAutoIVClass.WeaponSlotType.MELEE:
                        slot.Weapon_Identifier = GrandTheftAutoIVClass.WeaponStringToID(comboMelee.SelectedItem.ToString());
                        slot.Ammunition = (ushort)intAmmoMelee.Value;
                        break;
                    case GrandTheftAutoIVClass.WeaponSlotType.HEAVY:
                        slot.Weapon_Identifier = GrandTheftAutoIVClass.WeaponStringToID(comboHeavy.SelectedItem.ToString());
                        slot.Ammunition = (ushort)intAmmoHeavy.Value;
                        break;
                    case GrandTheftAutoIVClass.WeaponSlotType.HANDGUN:
                        slot.Weapon_Identifier = GrandTheftAutoIVClass.WeaponStringToID(comboHandgun.SelectedItem.ToString());
                        slot.Ammunition = (ushort)intAmmoHandgun.Value;
                        break;
                }

            //Write our save
            GTA_IV_Class.Write();            
        }

        private void cmbWeaponSlot1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmdMaxAll_Click(object sender, EventArgs e)
        {
            //Set our maximums
            uintMoney.Value = uintMoney.MaxValue;
            floatHealth.Value = floatHealth.Maximum;
            floatArmor.Value = floatArmor.Maximum;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            intAmmoThrown.Value = (int)ushort.MaxValue;
            intAmmoSniper.Value = (int)ushort.MaxValue;
            intAmmoSMG.Value = (int)ushort.MaxValue;
            intAmmoShotgun.Value = (int)ushort.MaxValue;
            intAmmoRifle.Value = (int)ushort.MaxValue;
            intAmmoHeavy.Value = (int)ushort.MaxValue;
            intAmmoHandgun.Value = (int)ushort.MaxValue;
        }

        /// <summary>
        /// This function sets our preview image for the given weapon ID.
        /// </summary>
        /// <param name="weaponID">The weapon ID string to show the image for.</param>
        private void SetPreviewImage(string weaponID)
        {
            //Set our image.
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = GTA_IV_Class.WeaponIDImage(GrandTheftAutoIVClass.WeaponStringToID(weaponID));
        }

        private void comboUnarmed_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPreviewImage(((DevComponents.DotNetBar.Controls.ComboBoxEx)sender).SelectedItem.ToString());
        }

        private void comboUnarmed_Click(object sender, EventArgs e)
        {
            if (((DevComponents.DotNetBar.Controls.ComboBoxEx)sender).SelectedItem != null)
                SetPreviewImage(((DevComponents.DotNetBar.Controls.ComboBoxEx)sender).SelectedItem.ToString());
        }
    }
}
