using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
//using HaloReach3d.IO;

namespace GrandTheftAutoIV
{
    public class GrandTheftAutoIVClass
    {
        //REFERENCES: 
        //http://www.gtamodding.com/index.php?title=List_of_Weapons_%28GTA4%29
        //http://gta.wikia.com/Weapons_in_GTA_IV
        //http://gta.wikia.com/Weapons_in_The_Lost_and_Damned
        //http://gta.wikia.com/Weapons_in_The_Ballad_of_Gay_Tony

        /// <summary>
        /// Our IO to handle this save.
        /// </summary>
        public EndianIO IO { get; set; }

        #region Game Save Values

        /// <summary>
        /// Our version of the episode for this gamesave.
        /// </summary>
        public EpisodeVersion Episode_Version { get; set; }
        /// <summary>
        /// Our money variable indicating how much money we have.
        /// </summary>
        public int Money { get; set; }
        /// <summary>
        /// Our health value indicating our health.
        /// </summary>
        public float Health { get; set; }
        /// <summary>
        /// Our armor value indicating how much armor we currently have.
        /// </summary>
        public float Armor { get; set; }
        /// <summary>
        /// Our weapon slots indicating our weapon 
        /// </summary>
        public WeaponSlot[] Weapon_Slots { get; set; }

        /// <summary>
        /// A dictionary using weapon slot types as keys, where returned values are weapons corresponding to those slot types for this game.
        /// </summary>
        public Dictionary<WeaponSlotType, List<WeaponIdentifier>> Weapon_Types_For_Slot
        {
            get
            {
                //Create our dictionary
                Dictionary<WeaponSlotType, List<WeaponIdentifier>> dict = new Dictionary<WeaponSlotType, List<WeaponIdentifier>>();

                //INITIALIZATION
                dict[WeaponSlotType.UNARMED] = new List<WeaponIdentifier>();
                dict[WeaponSlotType.MELEE] = new List<WeaponIdentifier>();
                dict[WeaponSlotType.HANDGUN] = new List<WeaponIdentifier>();
                dict[WeaponSlotType.SHOTGUN] = new List<WeaponIdentifier>();
                dict[WeaponSlotType.SMG] = new List<WeaponIdentifier>();
                dict[WeaponSlotType.RIFLE] = new List<WeaponIdentifier>();
                dict[WeaponSlotType.SNIPER] = new List<WeaponIdentifier>();
                dict[WeaponSlotType.HEAVY] = new List<WeaponIdentifier>();
                dict[WeaponSlotType.THROWN] = new List<WeaponIdentifier>();

                //UNARMED LIST
                dict[WeaponSlotType.UNARMED].Add(WeaponIdentifier.Unarmed);
                // while we're here, add unarmed to every other list
                dict[WeaponSlotType.MELEE].Add(WeaponIdentifier.Unarmed);
                dict[WeaponSlotType.HANDGUN].Add(WeaponIdentifier.Unarmed);
                dict[WeaponSlotType.HEAVY].Add(WeaponIdentifier.Unarmed);
                dict[WeaponSlotType.RIFLE].Add(WeaponIdentifier.Unarmed);
                dict[WeaponSlotType.SHOTGUN].Add(WeaponIdentifier.Unarmed);
                dict[WeaponSlotType.SMG].Add(WeaponIdentifier.Unarmed);
                dict[WeaponSlotType.SNIPER].Add(WeaponIdentifier.Unarmed);
                dict[WeaponSlotType.THROWN].Add(WeaponIdentifier.Unarmed);

                //MELEE LIST.
                dict[WeaponSlotType.MELEE].Add(WeaponIdentifier.Baseball_Bat);
                dict[WeaponSlotType.MELEE].Add(WeaponIdentifier.Knife);

                
                //HANDGUN LIST
                dict[WeaponSlotType.HANDGUN].Add(WeaponIdentifier.Glock_17);
                dict[WeaponSlotType.HANDGUN].Add(WeaponIdentifier.Desert_Eagle);

                //SHOTGUN LIST
                dict[WeaponSlotType.SHOTGUN].Add(WeaponIdentifier.Pump_Shotgun);
                dict[WeaponSlotType.SHOTGUN].Add(WeaponIdentifier.Combat_Shotgun);

                //SMG LIST
                dict[WeaponSlotType.SMG].Add(WeaponIdentifier.Micro_UZI);
                dict[WeaponSlotType.SMG].Add(WeaponIdentifier.MP5);

                //RIFLE LIST
                dict[WeaponSlotType.RIFLE].Add(WeaponIdentifier.Assault_Rifle);
                dict[WeaponSlotType.RIFLE].Add(WeaponIdentifier.Carbine_Rifle);

                //SNIPER LIST
                dict[WeaponSlotType.SNIPER].Add(WeaponIdentifier.Sniper_Rifle);
                dict[WeaponSlotType.SNIPER].Add(WeaponIdentifier.Combat_Sniper);

                //HEAVY LIST
                dict[WeaponSlotType.HEAVY].Add(WeaponIdentifier.Rocket_Launcher);

                //THROWN LIST
                dict[WeaponSlotType.THROWN].Add(WeaponIdentifier.Grenade);
                dict[WeaponSlotType.THROWN].Add(WeaponIdentifier.Molotov_Cocktail);

                //Add our Lost and Damned Weapons.
                if (Episode_Version == EpisodeVersion.GTA_IV_Lost_And_The_Damned)
                {
                    dict[WeaponSlotType.MELEE].Add(WeaponIdentifier.Pool_Cue_2);
                    dict[WeaponSlotType.HANDGUN].Add(WeaponIdentifier.Automatic_9mm);
                    dict[WeaponSlotType.SHOTGUN].Add(WeaponIdentifier.Assault_Shotgun);
                    dict[WeaponSlotType.SHOTGUN].Add(WeaponIdentifier.SawnOff_Shotgun);
                    //In Original and LATD: dict[WeaponSlotType.HEAVY].Add(WeaponIdentifier.Minigun);
                    dict[WeaponSlotType.HEAVY].Add(WeaponIdentifier.Grenade_Launcher);
                    dict[WeaponSlotType.THROWN].Add(WeaponIdentifier.Pipe_Bomb);
                }

                //Add our Ballad of Gay Tony Weapons
                if (Episode_Version == EpisodeVersion.GTA_IV_Ballad_Of_Gay_Tony)
                {
                    dict[WeaponSlotType.HANDGUN].Add(WeaponIdentifier.Pistol_44);
                    dict[WeaponSlotType.SMG].Add(WeaponIdentifier.Golden_UZI);
                    dict[WeaponSlotType.SMG].Add(WeaponIdentifier.P90);
                    dict[WeaponSlotType.RIFLE].Add(WeaponIdentifier.M249_SAW);
                    dict[WeaponSlotType.SHOTGUN].Add(WeaponIdentifier.AA_12);
                    dict[WeaponSlotType.SHOTGUN].Add(WeaponIdentifier.Non_Explosive_AA_12);
                    dict[WeaponSlotType.SNIPER].Add(WeaponIdentifier.Advanced_Sniper);
                    dict[WeaponSlotType.HEAVY].Add(WeaponIdentifier.Grenade_Launcher);
                    dict[WeaponSlotType.THROWN].Add(WeaponIdentifier.Satchel_Charge);
                }
                //Return our dictionary
                return dict;
            }
        }

        #endregion

        #region Constructor

        public GrandTheftAutoIVClass(EndianIO io)
        {
            //Set our IO
            IO = io;
            //Read our gamesave
            Read();
        }

        #endregion

        #region Functions

        public void Read()
        {
            //Go to our version offset
            IO.In.ReadInt32();
            //Read our game version
            Episode_Version = (EpisodeVersion)IO.In.ReadUInt32();

            //Go to our money
            IO.In.BaseStream.Position = 0xEA;
            //Read our money
            Money = IO.In.ReadInt32();

            //Go to our health and armor
            IO.In.BaseStream.Position = 0x132;
            //Read our health
            Health = IO.In.ReadSingle();
            //Read our armor
            Armor = IO.In.ReadSingle();

            //Create our weapon slots
            Weapon_Slots = new WeaponSlot[0x09];
            //Loop for each weapon slot
            for (int i = 0; i < Weapon_Slots.Length; i++)
                //Load our weapon slot
                Weapon_Slots[i] = new WeaponSlot(IO, i);
        }

        public void Write()
        {
            //Go to our money offset
            IO.Out.BaseStream.Position = 0xEA;
            //Write our money
            IO.Out.Write(Money);

            //Go to our health and armor offset
            IO.Out.BaseStream.Position = 0x132;
            //Write our health
            IO.Out.Write(Health);
            //Write our armor
            IO.Out.Write(Armor);

            //Loop for each weapon slot
            for (int i = 0; i < Weapon_Slots.Length; i++)
                //Save our weapon slot.
                Weapon_Slots[i].Write(IO, i);
            //Recalculate our signature.
            Recalculate_Signature();
        }

        public void Recalculate_Signature()
        {
            IO.Stream.Flush();
            //Create our signature
            int siggy = 0;
            //Go to offset 0
            IO.In.BaseStream.Position = 0;
            //Loop for our length - 8. (Signature is 8 bytes before end, footer is 4 bytes before end).
            while(IO.In.BaseStream.Position < IO.In.BaseStream.Length - 8)
                //Add our byte to our checksum.
                siggy += (int)IO.In.ReadByte();
            IO.Out.BaseStream.Position = IO.Out.BaseStream.Length - 8;
            //Write our signature
            IO.Out.Write(siggy);
        }

        /// <summary>
        /// Takes a given identifier for a weapon and returns it's string.
        /// </summary>
        /// <param name="id">The weapon identifier.</param>
        /// <returns>The string for the weapon identifier.</returns>
        public static string WeaponIDToString(WeaponIdentifier id) { return WeaponIdentifierStrings[(uint)id]; }
        /// <summary>
        /// Takes a given weapon string and returns the identifier.
        /// </summary>
        /// <param name="weaponStr">The weapon string.</param>
        /// <returns>The identifier to return for the string.</returns>
        public static WeaponIdentifier WeaponStringToID(string weaponStr)
        {
            for (int i = 0; i < WeaponIdentifierStrings.Length; i++)
                if (WeaponIdentifierStrings[i] == weaponStr)
                    return (WeaponIdentifier)i;
            return WeaponIdentifier.Unarmed;
        }

        /// <summary>
        /// Returns a list of usable weapon names for this save, for the slot type.
        /// </summary>
        /// <param name="type">The slot type of weapons to return.</param>
        /// <returns>The weapons that fall into the slot type category.</returns>
        public string[] GetWeaponNamesForType(WeaponSlotType type)
        {
            //Get our list of weapons
            WeaponIdentifier[] weaponIDs = Weapon_Types_For_Slot[type].ToArray();
            //Create our string array
            string[] weaponStrs = new string[weaponIDs.Length];
            //Loop for each weaponID
            for (int i = 0; i < weaponIDs.Length; i++)
                //Get our weapon string.
                weaponStrs[i] = WeaponIDToString(weaponIDs[i]);

            //Return our string array
            return weaponStrs;
        }

        public Image WeaponIDImage(WeaponIdentifier id)
        {
            //Create our bitmap file
            Bitmap bitm = null;
            //Get our bitmap
            switch (id)
            {
                case WeaponIdentifier.AA_12:
                case WeaponIdentifier.Non_Explosive_AA_12:
                    bitm = Horizon.Properties.Resources.GTA4_AA12;
                    break;
                case WeaponIdentifier.Advanced_Sniper:
                    bitm = Horizon.Properties.Resources.GTA4_AdvancedSniper;
                    break;
                case WeaponIdentifier.Assault_Rifle:
                    bitm = Horizon.Properties.Resources.GTA4_AK47;
                    break;
                case WeaponIdentifier.Assault_Shotgun:
                    bitm = Horizon.Properties.Resources.GTA4_Assault_Shotgun;
                    break;
                case WeaponIdentifier.Automatic_9mm:
                    bitm = Horizon.Properties.Resources.GTA4_Automatic_9mm;
                    break;
                case WeaponIdentifier.Baseball_Bat:
                    bitm = Horizon.Properties.Resources.GTA4_BaseballBat;
                    break;
                case WeaponIdentifier.Carbine_Rifle:
                    bitm = Horizon.Properties.Resources.GTA4_M4Carbine;
                    break;
                case WeaponIdentifier.Combat_Shotgun:
                    bitm = Horizon.Properties.Resources.GTA4_CombatShotgun;
                    break;
                case WeaponIdentifier.Combat_Sniper:
                    bitm = Horizon.Properties.Resources.GTA4_CombatSniper;
                    break;
                case WeaponIdentifier.Desert_Eagle:
                    bitm = Horizon.Properties.Resources.GTA4_DesertEagle;
                    break;
                case WeaponIdentifier.Glock_17:
                    bitm = Horizon.Properties.Resources.GTA4_Glock;
                    break;
                case WeaponIdentifier.Golden_UZI:
                    bitm = Horizon.Properties.Resources.GTA4_GoldUzi;
                    break;
                case WeaponIdentifier.Grenade:
                    bitm = Horizon.Properties.Resources.GTA4_Grenade;
                    break;
                case WeaponIdentifier.Grenade_Launcher:
                    bitm = Horizon.Properties.Resources.GTA4_Grenade_Launcher;
                    break;
                case WeaponIdentifier.Knife:
                    bitm = Horizon.Properties.Resources.GTA4_Knife;
                    break;
                case WeaponIdentifier.M249_SAW:
                    bitm = Horizon.Properties.Resources.GTA4_Advanced_MG;
                    break;
                case WeaponIdentifier.Micro_UZI:
                    bitm = Horizon.Properties.Resources.GTA4_MicroUZI;
                    break;
                case WeaponIdentifier.Molotov_Cocktail:
                    bitm = Horizon.Properties.Resources.GTA4_Molotov;
                    break;
                case WeaponIdentifier.MP5:
                    bitm = Horizon.Properties.Resources.GTA4_MP5;
                    break;
                case WeaponIdentifier.P90:
                    bitm = Horizon.Properties.Resources.GTA4_P90;
                    break;
                case WeaponIdentifier.Pipe_Bomb:
                    bitm = Horizon.Properties.Resources.GTA4_Pipe_Bomb;
                    break;
                case WeaponIdentifier.Pistol_44:
                    bitm = Horizon.Properties.Resources.GTA4_Pistol44;
                    break;
                case WeaponIdentifier.Pool_Cue_1:
                case WeaponIdentifier.Pool_Cue_2:
                    bitm = Horizon.Properties.Resources.GTA4_Pool_Cue;
                    break;
                case WeaponIdentifier.Pump_Shotgun:
                    bitm = Horizon.Properties.Resources.GTA4_PumpShotgun;
                    break;
                case WeaponIdentifier.Rocket_Launcher:
                    bitm = Horizon.Properties.Resources.GTA4_RPG;
                    break;
                case WeaponIdentifier.Satchel_Charge:
                    bitm = Horizon.Properties.Resources.GTA4_StickyBomb;
                    break;
                case WeaponIdentifier.SawnOff_Shotgun:
                    bitm = Horizon.Properties.Resources.GTA4_Sawed_Off_Shotgun;
                    break;
                case WeaponIdentifier.Sniper_Rifle:
                    bitm = Horizon.Properties.Resources.GTA4_SniperRifle;
                    break;
                case WeaponIdentifier.Unarmed:
                    bitm = Horizon.Properties.Resources.GTA4_Fist;
                    break;
                default:
                    bitm = new Bitmap(0, 0);
                    break;

            }
            //Return our bitmap.
            return (Image)bitm;
        }
        #endregion

        #region Structures and Classes

        /// <summary>
        /// This class handles a weapon slot at a given index and provides information on it.
        /// </summary>
        public class WeaponSlot
        {

            #region Values
            /// <summary>
            /// Our type of slot, weapons of other types may not be added here.
            /// </summary>
            public WeaponSlotType Weapon_Slot_Type { get; set; }
            /// <summary>
            /// Our type of weapon for this slot.
            /// </summary>
            public WeaponIdentifier Weapon_Identifier { get; set; }

            /// <summary>
            /// A string indicating our weapon identifier/name.
            /// </summary>
            public string Weapon_Identifier_Str
            {
                get { return GrandTheftAutoIVClass.WeaponIDToString(Weapon_Identifier); }
            }

            /// <summary>
            /// Our ammunition count for this weapon.
            /// </summary>
            public ushort Ammunition { get; set; }
            #endregion

            #region Constructor

            /// <summary>
            /// Our default constructor for this class that reads our info for this weapon item slot.
            /// </summary>
            /// <param name="IO">Our IO</param>
            /// <param name="index"></param>
            public WeaponSlot(EndianIO IO, int index) { Read(IO, index); }

            #endregion

            #region Functions

            /// <summary>
            /// This function reads our weapon slot for the given index(there are 8 slots total, so the last index is 7).
            /// </summary>
            /// <param name="IO">Our IO to read with.</param>
            /// <param name="index">Our index to read for the weapon slot.</param>
            public void Read(EndianIO IO, int index)
            {
                //Set our slot type
                Weapon_Slot_Type = (WeaponSlotType)index;

                //Go to our weapon type offset.
                IO.In.BaseStream.Position = 0x13E + (index * 4);
                //Read our weapon type
                Weapon_Identifier = (WeaponIdentifier)IO.In.ReadUInt32();
                //Go to our ammunition offset.
                IO.In.BaseStream.Position = 0x166 + (index * 2);
                //Read our ammunition
                Ammunition = IO.In.ReadUInt16();
            }

            /// <summary>
            /// This function reads our weapon slot for the given index(there are 8 slots total, so the last index is 7).
            /// </summary>
            /// <param name="IO">Our IO to read with.</param>
            /// <param name="index">Our index to read for the weapon slot.</param>
            public void Write(EndianIO IO, int index)
            {
                //Go to our weapon type offset.
                IO.Out.BaseStream.Position = 0x13E + (index * 4);
                //Write our weapon type
                IO.Out.Write((uint)Weapon_Identifier);
                //Go to our ammunition offset.
                IO.Out.BaseStream.Position = 0x166 + (index * 2);
                //Write our ammunition
                IO.Out.Write(Ammunition);
            }

            #endregion
        }

        /// <summary>
        /// Our type of weapon slot.
        /// </summary>
        public enum WeaponSlotType : uint
        {
            UNARMED = 0x00,
            MELEE = 0x01,
            HANDGUN = 0x02,
            SHOTGUN = 0x03,
            SMG = 0x04,
            RIFLE = 0x05,
            SNIPER = 0x06,
            HEAVY = 0x07,
            THROWN = 0x08
        }

        /// <summary>
        /// Our weapon identifiers for the weapon slots. (TODO: Lost and Damned, Ballad of Gay Tony).
        /// </summary>
        public enum WeaponIdentifier : uint
        {
            Unarmed = 0x00,
            Baseball_Bat = 0x01,
            Pool_Cue_1 = 0x02,
            Knife = 0x03,
            Grenade = 0x04,
            Molotov_Cocktail = 0x05,
            Rocket_Launcher_Projectile = 0x06, //unusable
            Glock_17 = 0x07,
            Unused_08 = 0x08, 
            Desert_Eagle = 0x09,
            Pump_Shotgun = 0x0A,
            Combat_Shotgun = 0x0B,
            Micro_UZI = 0x0C,
            MP5 = 0x0D,
            Assault_Rifle = 0x0E,
            Carbine_Rifle = 0x0F,
            Combat_Sniper = 0x10,
            Sniper_Rifle = 0x11,
            Rocket_Launcher = 0x12,
            Flamethrower = 0x13,
            Minigun = 0x14,
            Grenade_Launcher = 0x15,
            Assault_Shotgun = 0x16,
            Unused_0x17 = 0x17, //unused
            Pool_Cue_2 = 0x18,
            Grenade_Launcher_Projectile = 0x19, //unusable
            SawnOff_Shotgun = 0x1A,
            Automatic_9mm = 0x1B,
            Pipe_Bomb = 0x1C,
            Pistol_44 = 0x1D,
            AA_12 = 0x1E,
            Non_Explosive_AA_12 = 0x1F,
            P90 = 0x20,
            Golden_UZI = 0x21,
            M249_SAW = 0x22,
            Advanced_Sniper = 0x23,
            Satchel_Charge = 0x24,
        }

        public static string[] WeaponIdentifierStrings = {
            "Unarmed",
            "Baseball Bat",
            "Pool Cue 1",
            "Knife",
            "Grenades (M26A1)",
            "Molotov Cocktail",
            "RPG Projectile",
            "Pistol (Glock 22)",
            "Unused 0x08",
            "Combat Pistol (Desert Eagle .357)",
            "Pump Shotgun (Ithaca Model 37 Stakeout)",
            "Combat Shotgun (Remington 11-87)",
            "Micro-SMG (Micro Uzi)",
            "SMG (SW MP-10)",
            "Assault Rifle (AK-47)",
            "Carbine Rifle (M4A1)",
            "Combat Sniper (H&K PSG-1)",
            "Sniper Rifle (Remington 700)",
            "RPG (RPG-7)",
            "Flamethrower",
            "Minigun",
            "Grenade Launcher (Heckler and Koch HK69A1)",
            "Assault Shotgun (DAO-12)",
            "Unused 0x17",
            "Pool Cue 2",
            "Grenade Launcher Projectile",
            "Sawn-off Shotgun",
            "Automatic 9mm (CZ-75 Automatic)",
            "Pipe Bomb",
            "Pistol .44 (AMP Automag Model 180)",
            "Automatic/Explosive Shotgun (AA-12)",
            "Automatic/Non-Explosive Shotgun (AA-12)",
            "Assault SMG (FN P90)",
            "Gold SMG (Uzi)",
            "Advanced MG (M249 SAW)",
            "Advanced Sniper (DSR-1)",
            "Sticky Bombs (Satchel charges)"
                                                  };

        /// <summary>
        /// The version of our save, determines which episode we're handling.
        /// </summary>
        public enum EpisodeVersion : uint
        {
            GTA_IV_Original = 0x000ACCEE,
            GTA_IV_Lost_And_The_Damned = 0x00093A3A,
            GTA_IV_Ballad_Of_Gay_Tony = 0x00098072
        }

        #endregion
    }
}
