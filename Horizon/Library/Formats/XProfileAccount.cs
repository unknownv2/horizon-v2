using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace XProfile
{
    public class StringValueAttribute : Attribute
    {

        #region Properties

        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringValue { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }
        #endregion

    }
    public class XProfileAccount
    {
        public enum XOnlineLanguage : byte
        {
            [StringValue("")]
            Null = 0,
            [StringValue("en")]
            English = 1,
            [StringValue("ja")]
            Japanese,
            [StringValue("de")]
            German,
            [StringValue("fr")]
            French,
            [StringValue("es")]
            Spanish,
            [StringValue("it")]
            Italian,
            [StringValue("ko")]
            Korean,
            [StringValue("zh")]
            Chinese,
            [StringValue("pt")]
            Portuguese,
            [StringValue("zh")]
            Chinese2,
            [StringValue("pl")]
            Polish,
            [StringValue("ru")]
            Russian,
            [StringValue("da")]
            Danish,
            [StringValue("fi")]
            Finnish,
            [StringValue("nb")]
            Norwegian,
            [StringValue("nl")]
            Dutch,
            [StringValue("sv")]
            Swedish,
            [StringValue("cs")]
            Czech,
            [StringValue("el")]
            Greek,
            [StringValue("hu")]
            Hungarian
        }
        public enum XOnlineCountry : byte
        {
            [StringValue("")]
            Null = 0,
            [StringValue("AT")]
            Austria = 5,
            [StringValue("AU")]
            Australia = 6,
            [StringValue("BE")]
            Belgium = 8,
            [StringValue("BR")]
            Brazil = 0xd,
            [StringValue("CA")]
            Canada = 0x10,
            [StringValue("CH")]
            Switzerland = 0x12,
            [StringValue("CL")]
            Chile = 0x13,
            [StringValue("CN")]
            China = 0x14,
            [StringValue("CO")]
            Colombia = 0x15,
            [StringValue("CZ")]
            CzechRepublic = 0x17,
            [StringValue("DE")]
            Germany = 0x18,
            [StringValue("DK")]
            Denmark = 0x19,
            [StringValue("ES")]
            Spain = 0x1f,
            [StringValue("FI")]
            Finland = 0x20,
            [StringValue("FR")]
            France = 0x22,
            [StringValue("GB")]
            GreatBritain = 0x23,
            [StringValue("GR")]
            Greece = 0x25,
            [StringValue("HK")]
            HongKong = 0x27,
            [StringValue("HU")]
            Hungary = 0x2a,
            [StringValue("IE")]
            Ireland = 0x2c,
            [StringValue("IN")]
            India = 0x2e,
            [StringValue("IT")]
            Italy = 0x32,
            [StringValue("JP")]
            Japan = 0x35,
            [StringValue("KR")]
            Korea = 0x38,
            [StringValue("MX")]
            Mexico = 0x47,
            [StringValue("NL")]
            Netherlands = 0x4a,
            [StringValue("NO")]
            Norway = 0x4b,
            [StringValue("NZ")]
            NewZealand = 0x4c,
            [StringValue("PL")]
            Poland = 0x52,
            [StringValue("PT")]
            PuertoRico = 0x54,
            [StringValue("RU")]
            Russia = 0x58,
            [StringValue("SE")]
            Sweeden = 0x5a,
            [StringValue("SG")]
            Singapore = 0x5b,
            [StringValue("TW")]
            Taiwan = 0x65,
            [StringValue("U")]
            UnitedStates = 0x67,
            [StringValue("ZA")]
            SouthAfrica = 0x6d
        }
        public enum XOnlinePassCodeTypes
        {
            XOnlinePassCodeNone = 0x00,
            XOnlinePassCodeDpadUp = 0x1,
            XOnlinePassCodeDpadDown = 0x2,
            XOnlinePassCodeDpadLeft = 0x3,
            XOnlinePassCodeDpadRight = 0x4,
            XOnlinePassCodeGamepadX = 0x5,
            XOnlinePassCodeGamepadY = 0x6,
            XOnlinePassCodeGamepadLeftTrigger = 0x9,
            XOnlinePassCodeGamepadRightTrigger = 0xA,
            XOnlinePassCodeGamepadLeftShoulder = 0xB,
            XOnlinePassCodeGamepadRightShoulder = 0xC
        };
        public enum XOnlineTierType
        {
            XOnlineTierInvalid = 0x0,
            XOnlineTierSilver = 0x3,
            XOnlineTierGold = 0x6,
            XOnlineTierFamilyGold = 0x09
        };
        public class XamAccountInfo
        {
            public uint Reserved;
            public uint LiveFlags;
            public string GamerTag;
            public ulong XuidOnline;
            public uint CachedUserFlags;
            public uint OnlineServiceNetworkId;
            public byte[] Passcode;
            public string OnlineDomain;
            public string OnlineKerberosRealm;
            public byte[] OnlineKey;
            public string UserPassportMembername;
            public string UserPassportPassword;
            public string OwnerPassportMembername;

            public bool Recovering
            {
                get { return ((this.Reserved >> (0x18 + 0x06)) & 1) == 0x1; }
                set { this.Reserved = (uint)(this.Reserved & ~0x40000000) | ((Convert.ToUInt32(value) & 1) << (0x18 + 6)); }
            }
            public bool XboxLiveEnabled
            {
                get { return ((this.Reserved >> (0x18 + 0x05)) & 1) == 0x1; }
                set { this.Reserved = (uint)(this.Reserved & ~0x20000000) | ((Convert.ToUInt32(value) & 1) << (0x18 + 5)); }
            }
            public bool PasswordProtected
            {
                get { return ((this.Reserved >> (0x18 + 0x04)) & 1) == 0x1; }
                set { this.Reserved = (uint)(this.Reserved & ~0x10000000) | ((Convert.ToUInt32(value) & 1) << (0x18 + 4)); }
            }
            public XamAccountInfo(EndianReader reader)
            {
                this.Reserved = reader.ReadUInt32();
                this.LiveFlags = reader.ReadUInt32();
                this.GamerTag = reader.ReadUnicodeString(16);
                this.XuidOnline = reader.ReadUInt64();
                this.CachedUserFlags = reader.ReadUInt32();
                this.OnlineServiceNetworkId = reader.ReadUInt32();
                this.Passcode = reader.ReadBytes(4);
                this.OnlineDomain = reader.ReadAsciiString(20);
                this.OnlineKerberosRealm = reader.ReadAsciiString(24);
                this.OnlineKey = reader.ReadBytes(16);
                this.UserPassportMembername = reader.ReadAsciiString(114);
                this.UserPassportPassword = reader.ReadAsciiString(32);
                this.OwnerPassportMembername = reader.ReadAsciiString(114);
            }
            public void SetPassKey(List<XOnlinePassCodeTypes> Passcodes)
            {
                for (var x = 0; x < 4; x++)
                    this.Passcode[x] = (byte)Passcodes[x];
            }
            public byte[] ToArray()
            {
                MemoryStream ms = new MemoryStream();
                EndianWriter writer = new EndianWriter(ms, EndianType.BigEndian);

                writer.Write(this.Reserved);
                writer.Write(this.LiveFlags);
                writer.WriteUnicodeString(this.GamerTag, 16);
                writer.Write(XuidOnline);
                writer.Write(CachedUserFlags);
                writer.Write(OnlineServiceNetworkId);
                writer.Write(Passcode);
                writer.WriteAsciiString(OnlineDomain, 20);
                writer.WriteAsciiString(OnlineKerberosRealm, 24);
                writer.Write(OnlineKey);
                writer.WriteAsciiString(UserPassportMembername, 114);
                writer.WriteAsciiString(UserPassportPassword, 32);
                writer.WriteAsciiString(OwnerPassportMembername, 114);

                writer.Close();
                return ms.ToArray();
            }
        }
        public XamAccountInfo Info;
        public List<XOnlinePassCodeTypes> PassCode;
        public XOnlineTierType MembershipType
        {
            get { return (XOnlineTierType)(this.Info.CachedUserFlags >> 20 & 0x1F);}
            set { this.Info.CachedUserFlags = (uint)(this.Info.CachedUserFlags & ~(0x1f << 20)) | (Convert.ToUInt32(value) & 0x1f) << 20; }
        }
        public string OnlineLanguage
        {
            get { return GetStringValue(XLanguage); }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                this.Info.CachedUserFlags = (uint)(this.Info.CachedUserFlags & ~(0x1f << 25)) |
                (Convert.ToUInt32((GetOnlineLanguageFromString(value))) & 0x1f) << 25;
            }
        }
        public string OnlineCountry
        {
            get 
            { 
                return GetStringValue(XCountry); 
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                this.Info.CachedUserFlags = (uint)(this.Info.CachedUserFlags & ~(0xff << 8)) | 
                (Convert.ToUInt32(GetOnlineCountryFromString(value)) & 0xff) << 8;
            }
        }
        public XOnlineCountry XCountry
        {
            get { return (XOnlineCountry) (this.Info.CachedUserFlags >> 0x08 & 0xFF); }
            set { OnlineCountry = Enum.GetName(typeof(XOnlineCountry), value); }
        }
        public XOnlineLanguage XLanguage
        {
            get { return (XOnlineLanguage) (this.Info.CachedUserFlags >> 25 & 0x1F); }
            set { OnlineLanguage = Enum.GetName(typeof(XOnlineLanguage), value); }
        }

        private EndianIO IO;
        public bool DeveloperAccount = false;
        public XProfileAccount(byte[] AccountBuffer)
        {
            byte[] Data = new byte[0x17c];
            Data = HorizonCrypt.XeKeysUnObfuscate(1, AccountBuffer, false);
            if (Data == null)
            {
                Data = HorizonCrypt.XeKeysUnObfuscate(1, AccountBuffer, true);
                if (Data == null)
                {
                    throw new Exception("Invalid Account info buffer found.");
                }
                this.DeveloperAccount = true;
            }
            this.IO = new EndianIO(Data, EndianType.BigEndian);

            this.IO.Open();

            this.Read();

        }
        public void SetPasscode(int Index, XOnlinePassCodeTypes Passcode)
        {
            this.PassCode[Index] = Passcode;
        }
        private void Read()
        {
            this.Info = new XamAccountInfo(IO.In);

            this.PassCode = new List<XOnlinePassCodeTypes>();

            for (var x = 0; x < 4; x++)
                this.PassCode.Add((XOnlinePassCodeTypes)this.Info.Passcode[x]);

        }
        public byte[] Save()
        {
            this.Info.SetPassKey(this.PassCode);
            return HorizonCrypt.XeKeysObfuscate(1, this.Info.ToArray(), this.DeveloperAccount);
        }
        private static string GetStringValue(Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }
        private static XOnlineLanguage GetOnlineLanguageFromString(string Language)
        {
            return (XOnlineLanguage)Enum.Parse(typeof(XOnlineLanguage), Language, true);
        }
        private static XOnlineCountry GetOnlineCountryFromString(string Language)
        {
            return (XOnlineCountry)Enum.Parse(typeof(XOnlineCountry), Language, true);
        }
    }
}