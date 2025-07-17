using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rockstar
{
    public struct SPStat
    {
        public string Name;
        public SPStatType Type;
        public string Comment;
    }

    public enum SPStatType
    {
        Float,
        Int,
        Bool,
        Label,
        String,
        U8,
        U16,
        U32,
        U64,
        Date,
        Position,
        ProfileSetting,
        Packed
    }

    internal abstract class GrandTheftAutoVData
    {
        public static Dictionary<uint, SPStat> SPStats = new Dictionary<uint, SPStat>
            {
                {
                    0xB2CD9437,
                    new SPStat
                        {
                            Name = "_Version",
                            Type = SPStatType.Int,
                            Comment = "If player registered to SC from in-game; set to 1. Otherwise; set to 0. "
                        }
                },
                {
                    0x63B6DDE6,
                    new SPStat
                        {
                            Name = "_AcctSubscriptionLevel",
                            Type = SPStatType.Int,
                            Comment = "Subscription level for player"
                        }
                },
                {
                    0xCF204454,
                    new SPStat
                        {
                            Name = "_AutoLoginEnabled",
                            Type = SPStatType.Int,
                            Comment =
                                "If the player is online when the title screen is reached; this should be set to 1. Otherwise; should be 0."
                        }
                },
                {
                    0xE660F221,
                    new SPStat
                        {
                            Name = "_DisplayMode",
                            Type = SPStatType.Int,
                            Comment = "The vertical resolution last played in (e.g. 480; 720; 900; 1080; etc)."
                        }
                },
                {
                    0x1F4C4210,
                    new SPStat
                        {
                            Name = "_Gamma",
                            Type = SPStatType.Float,
                            Comment =
                                "Most recent gamma (in-game brightness) setting used; in whatever units the game uses."
                        }
                },
                {
                    0xA30B0B85,
                    new SPStat
                        {
                            Name = "_InvertedAim",
                            Type = SPStatType.Int,
                            Comment = "If player is using inverted aiming; set to 1. Otherwise; set to 0."
                        }
                },
                {
                    0x5B7EEB62,
                    new SPStat
                        {
                            Name = "_MpAvgDownBps",
                            Type = SPStatType.Float,
                            Comment = "Avg downstream Bytes per second in multiplayer. (0 if N/A)"
                        }
                },
                {
                    0xAFE62C0C,
                    new SPStat
                        {
                            Name = "_MpAvgPeerPing",
                            Type = SPStatType.Float,
                            Comment = "Avg ping to other players in milliseconds. (0 if N/A)"
                        }
                },
                {
                    0xB79F27DD,
                    new SPStat
                        {
                            Name = "_MpAvgUpBps",
                            Type = SPStatType.Float,
                            Comment = "Avg upstream Bytes per second in multiplayer. (0 if N/A)"
                        }
                },
                {
                    0xCF9ED858,
                    new SPStat
                        {
                            Name = "_MpNumLanSessions",
                            Type = SPStatType.Int,
                            Comment = "Total number of LAN (system link) sessions hosted or joined."
                        }
                },
                {
                    0xA1C09C1A,
                    new SPStat
                        {
                            Name = "_NatType",
                            Type = SPStatType.Int,
                            Comment = "The type of NAT the player is behind. The following mappings are used:"
                        }
                },
                {
                    0xA851E67C,
                    new SPStat
                        {
                            Name = "_NumSaveGames",
                            Type = SPStatType.Int,
                            Comment = "Number of save game slots the player is using."
                        }
                },
                {
                    0xA7F6D4A3,
                    new SPStat
                        {
                            Name = "_PublicIp",
                            Type = SPStatType.Int,
                            Comment = "This will be the public IP of the console; used for geolocation."
                        }
                },
                {
                    0x0BE3001D,
                    new SPStat
                        {
                            Name = "_ScIsLinked",
                            Type = SPStatType.Int,
                            Comment = "If account is linked to SC; set to 1. Otherwise; set to 0."
                        }
                },
                {
                    0x20FFC655,
                    new SPStat
                        {
                            Name = "_ScRegisteredInGame",
                            Type = SPStatType.Int,
                            Comment = "If player registered to SC from in-game; set to 1. Otherwise; set to 0. "
                        }
                },
                {
                    0x4E531AD8,
                    new SPStat
                        {
                            Name = "_SaveMpTimestamp_0",
                            Type = SPStatType.U64,
                            Comment = "Multiplayer Savegame Timestamp "
                        }
                },
                {
                    0x5C913754,
                    new SPStat
                        {
                            Name = "_SaveMpTimestamp_1",
                            Type = SPStatType.U64,
                            Comment = "Multiplayer Savegame Timestamp "
                        }
                },
                {
                    0x49CA91CB,
                    new SPStat
                        {
                            Name = "_SaveMpTimestamp_2",
                            Type = SPStatType.U64,
                            Comment = "Multiplayer Savegame Timestamp "
                        }
                },
                {
                    0x5807AE45,
                    new SPStat
                        {
                            Name = "_SaveMpTimestamp_3",
                            Type = SPStatType.U64,
                            Comment = "Multiplayer Savegame Timestamp "
                        }
                },
                {
                    0x656448FE,
                    new SPStat
                        {
                            Name = "_SaveMpTimestamp_4",
                            Type = SPStatType.U64,
                            Comment = "Multiplayer Savegame Timestamp "
                        }
                },
                {
                    0x73966562,
                    new SPStat
                        {
                            Name = "_SaveMpTimestamp_5",
                            Type = SPStatType.U64,
                            Comment = "Multiplayer Savegame Timestamp "
                        }
                },
                {
                    0xF9166A0D,
                    new SPStat
                        {
                            Name = "_SaveSpTimestamp",
                            Type = SPStatType.U64,
                            Comment = "Singleplayer Savegame Timestamp "
                        }
                },
                {0x6506561B, new SPStat {Name = "_ActiveCrew", Type = SPStatType.U64, Comment = "Active Crew Id "}},
                {
                    0xD8911956,
                    new SPStat
                        {
                            Name = "_MpProfileStatVersion_0",
                            Type = SPStatType.U64,
                            Comment = "Multiplayer ProfileStat Version "
                        }
                },
                {
                    0xAC67C0E4,
                    new SPStat
                        {
                            Name = "_MpProfileStatVersion_1",
                            Type = SPStatType.U64,
                            Comment = "Multiplayer ProfileStat Version "
                        }
                },
                {
                    0x9A9F9D54,
                    new SPStat
                        {
                            Name = "_MpProfileStatVersion_2",
                            Type = SPStatType.U64,
                            Comment = "Multiplayer ProfileStat Version "
                        }
                },
                {
                    0x517C8B0F,
                    new SPStat
                        {
                            Name = "_MpProfileStatVersion_3",
                            Type = SPStatType.U64,
                            Comment = "Multiplayer ProfileStat Version "
                        }
                },
                {
                    0xBFBC678D,
                    new SPStat
                        {
                            Name = "_MpProfileStatVersion_4",
                            Type = SPStatType.U64,
                            Comment = "Multiplayer ProfileStat Version "
                        }
                },
                {
                    0x751E5252,
                    new SPStat
                        {
                            Name = "_MpProfileStatVersion_5",
                            Type = SPStatType.U64,
                            Comment = "Multiplayer ProfileStat Version "
                        }
                },
                {
                    0x16194AAA,
                    new SPStat
                        {
                            Name = "_SpProfileStatVersion",
                            Type = SPStatType.U64,
                            Comment = "Singelplayer ProfileStat Version "
                        }
                },
                {
                    0xF38138AA,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_0",
                            Type = SPStatType.ProfileSetting,
                            Comment = "TARGETING_MODE - 0"
                        }
                },
                {
                    0x0543DC2F,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_1",
                            Type = SPStatType.ProfileSetting,
                            Comment = "AXIS_INVERSION - 1"
                        }
                },
                {
                    0xA4759A8C,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_2",
                            Type = SPStatType.ProfileSetting,
                            Comment = "CONTROLLER_VIBRATION - 2"
                        }
                },
                {
                    0xB6303E01,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_3",
                            Type = SPStatType.ProfileSetting,
                            Comment = "CONTROLLER_SENSITIVITY - 3"
                        }
                },
                {
                    0xC0CD533B,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_4",
                            Type = SPStatType.ProfileSetting,
                            Comment = "CONTROLLER_SNIPER_INVERT_UNUSED - 4"
                        }
                },
                {
                    0xD302F7A6,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_5",
                            Type = SPStatType.ProfileSetting,
                            Comment = "CONTROLLER_SIXAXIS_HELI - 5"
                        }
                },
                {
                    0x4D73EC8E,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_6",
                            Type = SPStatType.ProfileSetting,
                            Comment = "CONTROLLER_SIXAXIS_BIKE - 6"
                        }
                },
                {
                    0x58B4030A,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_7",
                            Type = SPStatType.ProfileSetting,
                            Comment = "CONTROLLER_SIXAXIS_BOAT - 7"
                        }
                },
                {
                    0x69EFA581,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_8",
                            Type = SPStatType.ProfileSetting,
                            Comment = "CONTROLLER_SIXAXIS_RELOAD - 8"
                        }
                },
                {
                    0x9338F813,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_9",
                            Type = SPStatType.ProfileSetting,
                            Comment = "CONTROLLER_SIXAXIS_MISSION - 9"
                        }
                },
                {
                    0x530D1EB8,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_10",
                            Type = SPStatType.ProfileSetting,
                            Comment = "CONTROLLER_SIXAXIS_ACTIVITY - 10"
                        }
                },
                {
                    0xB648E52E,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_11",
                            Type = SPStatType.ProfileSetting,
                            Comment = "CONTROLLER_SIXAXIS_AFTERTOUCH - 11"
                        }
                },
                {
                    0x6F82D7A3,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_12",
                            Type = SPStatType.ProfileSetting,
                            Comment = "CONTROLLER_CONTROL_CONFIG - 12"
                        }
                },
                {
                    0x5CC43226,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_13",
                            Type = SPStatType.ProfileSetting,
                            Comment = "CONTROLLER_AIM_SENSITIVITY - 13"
                        }
                },
                {
                    0x418E1369,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_100",
                            Type = SPStatType.ProfileSetting,
                            Comment = "VOICE_THRU_SPEAKERS - 100"
                        }
                },
                {
                    0x77E00010,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_101",
                            Type = SPStatType.ProfileSetting,
                            Comment = "VOICE_MUTED - 101"
                        }
                },
                {
                    0x6A4464D5,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_102",
                            Type = SPStatType.ProfileSetting,
                            Comment = "VOICE_VOLUME - 102"
                        }
                },
                {
                    0xD9CC876E,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_203",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_SUBTITLES - 203"
                        }
                },
                {
                    0x2221181A,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_204",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_RADAR_MODE - 204"
                        }
                },
                {
                    0x348FBCF7,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_205",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_HUD_MODE - 205"
                        }
                },
                {
                    0xCCD86D86,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_206",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_LANGUAGE - 206"
                        }
                },
                {
                    0x9E251020,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_207",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_GPS - 207"
                        }
                },
                {
                    0xEB39AA48,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_208",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_AUTOSAVE_MODE - 208"
                        }
                },
                {
                    0xBD93CEFD,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_209",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_HANDBRAKE_CAM - 209"
                        }
                },
                {
                    0x155578B7,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_210",
                            Type = SPStatType.ProfileSetting,
                            Comment = "LEGACY_DO_NOT_USE_DISPLAY_GAMMA - 210"
                        }
                },
                {
                    0x0717DC3C,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_211",
                            Type = SPStatType.ProfileSetting,
                            Comment = "CONTROLLER_CINEMATIC_SHOOTING - 211"
                        }
                },
                {
                    0x0F1D6C43,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_212",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_SAFEZONE_SIZE - 212"
                        }
                },
                {
                    0x01E351CF,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_213",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_GAMMA - 213"
                        }
                },
                {
                    0x2BAE2568,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_214",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_LEGACY_UNUSED1 - 214"
                        }
                },
                {
                    0x1C6B06E2,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_215",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_LEGACY_UNUSED2 - 215"
                        }
                },
                {
                    0x68769EF8,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_216",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_LEGACY_UNUSED3 - 216"
                        }
                },
                {
                    0x39A64158,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_217",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_LEGACY_UNUSED4 - 217"
                        }
                },
                {
                    0x83DC55C3,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_218",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_LEGACY_UNUSED5 - 218"
                        }
                },
                {
                    0x75433891,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_219",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_LEGACY_UNUSED6 - 219"
                        }
                },
                {
                    0xD46213F9,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_220",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_CAMERA_HEIGHT - 220"
                        }
                },
                {
                    0xF9E00F29,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_300",
                            Type = SPStatType.ProfileSetting,
                            Comment = "AUDIO_SFX_LEVEL - 300"
                        }
                },
                {
                    0x4A63B037,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_301",
                            Type = SPStatType.ProfileSetting,
                            Comment = "AUDIO_MUSIC_LEVEL - 301"
                        }
                },
                {
                    0x9C82D474,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_302",
                            Type = SPStatType.ProfileSetting,
                            Comment = "AUDIO_VOICE_OUTPUT - 302"
                        }
                },
                {
                    0x6EAC78C8,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_303",
                            Type = SPStatType.ProfileSetting,
                            Comment = "AUDIO_GPS_SPEECH - 303"
                        }
                },
                {
                    0x41279DBF,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_304",
                            Type = SPStatType.ProfileSetting,
                            Comment = "AUDIO_HIGH_DYNAMIC_RANGE - 304"
                        }
                },
                {
                    0xDA68D03F,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_305",
                            Type = SPStatType.ProfileSetting,
                            Comment = "AUDIO_SPEAKER_OUTPUT - 305"
                        }
                },
                {
                    0xECD7751C,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_306",
                            Type = SPStatType.ProfileSetting,
                            Comment = "AUDIO_MUSIC_LEVEL_IN_MP - 306"
                        }
                },
                {
                    0x7F2499B8,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_307",
                            Type = SPStatType.ProfileSetting,
                            Comment = "AUDIO_INTERACTIVE_MUSIC - 307"
                        }
                },
                {
                    0x8F543A17,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_308",
                            Type = SPStatType.ProfileSetting,
                            Comment = "AUDIO_DIALOGUE_BOOST - 308"
                        }
                },
                {
                    0x29ABEEC4,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_309",
                            Type = SPStatType.ProfileSetting,
                            Comment = "AUDIO_VOICE_SPEAKERS - 309"
                        }
                },
                {
                    0xF58547EC,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_412",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_RETICULE - 412"
                        }
                },
                {
                    0x2693AA08,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_413",
                            Type = SPStatType.ProfileSetting,
                            Comment = "CONTROLLER_CONFIG - 413"
                        }
                },
                {
                    0x4A03F0E8,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_414",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DISPLAY_FLICKER_FILTER - 414"
                        }
                },
                {
                    0xE86EA923,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_450",
                            Type = SPStatType.ProfileSetting,
                            Comment = "NUM_SINGLEPLAYER_GAMES_STARTED - 450"
                        }
                },
                {
                    0xEACBD27E,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_500",
                            Type = SPStatType.ProfileSetting,
                            Comment = "DLC_LOADED - 500"
                        }
                },
                {
                    0x75E3CC7A,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_600",
                            Type = SPStatType.ProfileSetting,
                            Comment = "AMBISONIC_DECODER - 600"
                        }
                },
                {
                    0xC3A5F362,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_677",
                            Type = SPStatType.ProfileSetting,
                            Comment = "AMBISONIC_DECODER_END - 677"
                        }
                },
                {
                    0xB092CD3C,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_678",
                            Type = SPStatType.ProfileSetting,
                            Comment = "AMBISONIC_DECODER_TYPE - 678"
                        }
                },
                {
                    0x286E3C75,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_700",
                            Type = SPStatType.ProfileSetting,
                            Comment = "PC_GRAPHICS_LEVEL - 700"
                        }
                },
                {
                    0x6468B46D,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_701",
                            Type = SPStatType.ProfileSetting,
                            Comment = "PC_SYSTEM_LEVEL - 701"
                        }
                },
                {
                    0x4E3A8811,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_702",
                            Type = SPStatType.ProfileSetting,
                            Comment = "PC_AUDIO_LEVEL - 702"
                        }
                },
                {
                    0x2368D5F3,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_800",
                            Type = SPStatType.ProfileSetting,
                            Comment = "FEED_PHONE - 800"
                        }
                },
                {
                    0x312E717E,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_801",
                            Type = SPStatType.ProfileSetting,
                            Comment = "FEED_STATS - 801"
                        }
                },
                {
                    0x06D11CC4,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_802",
                            Type = SPStatType.ProfileSetting,
                            Comment = "FEED_CREW - 802"
                        }
                },
                {
                    0x148BB839,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_803",
                            Type = SPStatType.ProfileSetting,
                            Comment = "FEED_FRIENDS - 803"
                        }
                },
                {
                    0xDC4747ED,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_804",
                            Type = SPStatType.ProfileSetting,
                            Comment = "FEED_SOCIAL - 804"
                        }
                },
                {
                    0xEA88E470,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_805",
                            Type = SPStatType.ProfileSetting,
                            Comment = "FEED_STORE - 805"
                        }
                },
                {
                    0xD85554A6,
                    new SPStat
                        {
                            Name = "_PROFILE_SETTING_900",
                            Type = SPStatType.ProfileSetting,
                            Comment = "FACEBOOK_UPDATES - 900"
                        }
                },
                {
                    0x0E3E6A20,
                    new SPStat
                        {
                            Name = "SP_PSTAT_BOOL0",
                            Type = SPStatType.Packed,
                            Comment = "Stat with Packed booleans index 0. Takes up to 64 booleans."
                        }
                },
                {
                    0x04F4578C,
                    new SPStat
                        {
                            Name = "SP_PSTAT_BOOL1",
                            Type = SPStatType.Packed,
                            Comment = "Stat with Packed booleans index 1. Takes up to 64 booleans."
                        }
                },
                {
                    0xEABBA31B,
                    new SPStat
                        {
                            Name = "SP_PSTAT_BOOL2",
                            Type = SPStatType.Packed,
                            Comment = "Stat with Packed booleans index 2. Takes up to 64 booleans."
                        }
                },
                {
                    0xDFA68CF1,
                    new SPStat
                        {
                            Name = "SP_PSTAT_BOOL3",
                            Type = SPStatType.Packed,
                            Comment = "Stat with Packed booleans index 2. Takes up to 64 booleans."
                        }
                },
                {
                    0x5559F856,
                    new SPStat
                        {
                            Name = "SP_PSTAT_BOOL4",
                            Type = SPStatType.Packed,
                            Comment = "Stat with Packed booleans index 2. Takes up to 64 booleans."
                        }
                },
                {
                    0x6BD02546,
                    new SPStat
                        {
                            Name = "SP_PSTAT_BOOL5",
                            Type = SPStatType.Packed,
                            Comment = "Stat with Packed booleans index 2. Takes up to 64 booleans."
                        }
                },
                {
                    0x3884BEAC,
                    new SPStat
                        {
                            Name = "SP_PSTAT_BOOL6",
                            Type = SPStatType.Packed,
                            Comment = "Stat with Packed booleans index 2. Takes up to 64 booleans."
                        }
                },
                {
                    0x26361A0F,
                    new SPStat
                        {
                            Name = "SP_PSTAT_BOOL7",
                            Type = SPStatType.Packed,
                            Comment = "Stat with Packed booleans index 2. Takes up to 64 booleans."
                        }
                },
                {
                    0xA4471647,
                    new SPStat
                        {
                            Name = "SP_PSTAT_BOOL8",
                            Type = SPStatType.Packed,
                            Comment = "Stat with Packed booleans index 2. Takes up to 64 booleans."
                        }
                },
                {
                    0xB1C38393,
                    new SPStat
                        {
                            Name = "PSTAT_BOOL0",
                            Type = SPStatType.Packed,
                            Comment = "Character Stat with Packed booleans index 0. Takes up to 64 booleans."
                        }
                },
                {
                    0xE7EEEFE9,
                    new SPStat
                        {
                            Name = "PSTAT_BOOL1",
                            Type = SPStatType.Packed,
                            Comment = "Character Stat with Packed booleans index 1. Takes up to 64 booleans."
                        }
                },
                {
                    0xD5A44B54,
                    new SPStat
                        {
                            Name = "PSTAT_BOOL2",
                            Type = SPStatType.Packed,
                            Comment = "Character Stat with Packed booleans index 2. Takes up to 64 booleans."
                        }
                },
                {
                    0x460B68BB,
                    new SPStat
                        {
                            Name = "SP_PSTAT_INT0",
                            Type = SPStatType.Packed,
                            Comment = "Stat with Packed integers index 0. Takes up to 8 integers."
                        }
                },
                {
                    0x18348D0E,
                    new SPStat
                        {
                            Name = "SP_PSTAT_INT1",
                            Type = SPStatType.Packed,
                            Comment = "Stat with Packed integers index 1. Takes up to 8 integers"
                        }
                },
                {
                    0x2243A12C,
                    new SPStat
                        {
                            Name = "SP_PSTAT_INT2",
                            Type = SPStatType.Packed,
                            Comment = "Stat with Packed integers index 2. Takes up to 8 integers"
                        }
                },
                {
                    0xC9D5ED86,
                    new SPStat
                        {
                            Name = "SP_PSTAT_INT3",
                            Type = SPStatType.Packed,
                            Comment = "Stat with Packed integers index 3. Takes up to 8 integers"
                        }
                },
                {
                    0xBC8FD2FA,
                    new SPStat
                        {
                            Name = "SP_PSTAT_INT4",
                            Type = SPStatType.Packed,
                            Comment = "Stat with Packed integers index 4. Takes up to 8 integers"
                        }
                },
                {
                    0xAD7234BF,
                    new SPStat
                        {
                            Name = "SP_PSTAT_INT5",
                            Type = SPStatType.Packed,
                            Comment = "Stat with Packed integers index 5. Takes up to 8 integers"
                        }
                },
                {
                    0xA00519E5,
                    new SPStat
                        {
                            Name = "SP_PSTAT_INT6",
                            Type = SPStatType.Packed,
                            Comment = "Stat with Packed integers index 6. Takes up to 8 integers"
                        }
                },
                {
                    0x910542A1,
                    new SPStat
                        {
                            Name = "PSTAT_INT0",
                            Type = SPStatType.Packed,
                            Comment = "Character Stat with Packed integers index 0. Takes up to 8 integers."
                        }
                },
                {
                    0xA34AE72C,
                    new SPStat
                        {
                            Name = "PSTAT_INT1",
                            Type = SPStatType.Packed,
                            Comment = "Character Stat with Packed integers index 1. Takes up to 8 integers"
                        }
                },
                {
                    0x75918BBA,
                    new SPStat
                        {
                            Name = "PSTAT_INT2",
                            Type = SPStatType.Packed,
                            Comment = "Character Stat with Packed integers index 2. Takes up to 8 integers"
                        }
                },
                {
                    0x85D02C37,
                    new SPStat
                        {
                            Name = "PSTAT_INT3",
                            Type = SPStatType.Packed,
                            Comment = "Character Stat with Packed integers index 2. Takes up to 8 integers"
                        }
                },
                {
                    0xC98C337A,
                    new SPStat
                        {
                            Name = "PSTAT_INT4",
                            Type = SPStatType.Packed,
                            Comment = "Character Stat with Packed integers index 2. Takes up to 8 integers"
                        }
                },
                {
                    0xBBCA97F7,
                    new SPStat
                        {
                            Name = "PSTAT_INT5",
                            Type = SPStatType.Packed,
                            Comment = "Character Stat with Packed integers index 2. Takes up to 8 integers"
                        }
                },
                {
                    0xECFFFA61,
                    new SPStat
                        {
                            Name = "PSTAT_INT6",
                            Type = SPStatType.Packed,
                            Comment = "Character Stat with Packed integers index 2. Takes up to 8 integers"
                        }
                },
                {
                    0xF82F73AD,
                    new SPStat {Name = "CITIES_PASSED", Type = SPStatType.Int, Comment = "Number of cities unlocked."}
                },
                {
                    0x486CDCFF,
                    new SPStat
                        {
                            Name = "TOTAL_PROGRESS_MADE",
                            Type = SPStatType.Float,
                            Comment = "Overall of progress made by the player"
                        }
                },
                {
                    0x1952FA15,
                    new SPStat
                        {
                            Name = "MSG_BEING_DISPLAYED",
                            Type = SPStatType.Bool,
                            Comment = "Message being dysplayed"
                        }
                },
                {
                    0x7AC407E8,
                    new SPStat
                        {
                            Name = "MOCAP_CUTSCENE_SKIPPED",
                            Type = SPStatType.Int,
                            Comment = "MOCAP CUTSCENE SKIPPED"
                        }
                },
                {
                    0xE7DAC013,
                    new SPStat
                        {
                            Name = "MOCAP_CUTSCENE_WATCHED",
                            Type = SPStatType.Int,
                            Comment = "MOCAP CUTSCENE WATCHED"
                        }
                },
                {
                    0x89E05558,
                    new SPStat {Name = "CUTSCENES_SKIPPED", Type = SPStatType.Int, Comment = "CUTSCENE SKIPPED"}
                },
                {
                    0xBED23222,
                    new SPStat {Name = "CUTSCENES_WATCHED", Type = SPStatType.Int, Comment = "CUTSCENE WATCHED"}
                },
                {
                    0xDDF209FC,
                    new SPStat
                        {
                            Name = "TIMES_MISSION_SKIPPED",
                            Type = SPStatType.Int,
                            Comment =
                                "The number of SP missions that have been skipped by the player. Character independent."
                        }
                },
                {
                    0x71308002,
                    new SPStat
                        {
                            Name = "PLAYING_TIME",
                            Type = SPStatType.U32,
                            Comment = "Total Playing time in Single Palyer."
                        }
                },
                {
                    0xD90C36BA,
                    new SPStat
                        {
                            Name = "STEALTH_TIME",
                            Type = SPStatType.U32,
                            Comment = "Total Playing time in Single Palyer."
                        }
                },
                {
                    0xD2F435EA,
                    new SPStat
                        {
                            Name = "LAST_PLAYING_TIME",
                            Type = SPStatType.U32,
                            Comment = "Previous Playing time - Total time until death."
                        }
                },
                {
                    0xE9EC4DD1,
                    new SPStat
                        {
                            Name = "SP_VEHICLE_MODELS_DRIVEN",
                            Type = SPStatType.Int,
                            Comment = "Maximun number of friends found in one match."
                        }
                },
                {
                    0x1AC36D8F,
                    new SPStat
                        {
                            Name = "SP_LAST_MISSION_NAME",
                            Type = SPStatType.Label,
                            Comment = "SP last mission name. Character independent."
                        }
                },
                {
                    0x3FE25CFA,
                    new SPStat
                        {
                            Name = "MP_LAST_MISSION_NAME",
                            Type = SPStatType.Label,
                            Comment = "MP last mission name. Character independent."
                        }
                },
                {
                    0x5D94A268,
                    new SPStat
                        {
                            Name = "SP_TOTAL_SWITCHES",
                            Type = SPStatType.Int,
                            Comment = "Total Number of times the player switched character."
                        }
                },
                {
                    0xC8B7EB88,
                    new SPStat
                        {
                            Name = "TOTAL_SWITCHES",
                            Type = SPStatType.Int,
                            Comment = "SP Number of times the player switched character."
                        }
                },
                {
                    0x9D2F68E1,
                    new SPStat
                        {
                            Name = "TIME_SPENT_ON_STOCKMARKET",
                            Type = SPStatType.U32,
                            Comment = "Stock market time"
                        }
                },
                {
                    0xFA1BA689,
                    new SPStat
                        {
                            Name = "MP_SESSIONS_STARTED",
                            Type = SPStatType.Int,
                            Comment = "Number of multiplayer sessions started."
                        }
                },
                {
                    0x2BDA870E,
                    new SPStat
                        {
                            Name = "MP_SESSIONS_ENDED",
                            Type = SPStatType.Int,
                            Comment = "Number of multiplayer sessions ended properlly."
                        }
                },
                {
                    0xFCAD8600,
                    new SPStat
                        {
                            Name = "MP_MOST_FRIENDS_IN_ONE_MATCH",
                            Type = SPStatType.Int,
                            Comment = "Maximun number of friends found in one match."
                        }
                },
                {
                    0x2C0CE8B1,
                    new SPStat
                        {
                            Name = "MP_VEHICLE_MODELS_DRIVEN",
                            Type = SPStatType.Int,
                            Comment = "Maximun number of friends found in one match."
                        }
                },
                {
                    0xA9E55789,
                    new SPStat
                        {
                            Name = "MP_PLAYING_TIME",
                            Type = SPStatType.U32,
                            Comment = "Total Playing time in multiplayer."
                        }
                },
                {
                    0x55C0DF48,
                    new SPStat
                        {
                            Name = "SP_CHOP_WALK_DONE",
                            Type = SPStatType.Int,
                            Comment = "An int that defines how many times Franklin has taken chop for a walk."
                        }
                },
                {
                    0x2CF30F28,
                    new SPStat
                        {
                            Name = "SP_HEIST_CHOSE_JEWEL_STEALTH",
                            Type = SPStatType.Bool,
                            Comment =
                                "True when the player chose to do the stealth approach to the Jewel Store Job. False when they chose the high impact approach."
                        }
                },
                {
                    0x6D221218,
                    new SPStat
                        {
                            Name = "SP_HEIST_CHOSE_DOCKS_SINK_SHIP",
                            Type = SPStatType.Bool,
                            Comment =
                                "True when the player chose to do the sink tanker approach to the Port of LS heist. False when they chose the deep sea diving approach."
                        }
                },
                {
                    0xDE9349DB,
                    new SPStat
                        {
                            Name = "SP_HEIST_CHOSE_BUREAU_FIRECREW",
                            Type = SPStatType.Bool,
                            Comment =
                                "True when the player chose to do the firecrew approach to the Bureau Raid. False when they chose the helicopter drop approach."
                        }
                },
                {
                    0x5052A748,
                    new SPStat
                        {
                            Name = "SP_HEIST_CHOSE_BIGS_TRAFFIC",
                            Type = SPStatType.Bool,
                            Comment =
                                "True when the player chose to do the traffic control approach to the Big Score. False when they chose the helicopter lift approach."
                        }
                },
                {
                    0xD7C92E23,
                    new SPStat
                        {
                            Name = "SP_FINAL_DECISION",
                            Type = SPStatType.Int,
                            Comment =
                                "An int that defines which choice the player made at the end of the game: 1 - Killed Michael, 2 - Killed Trevor, 3 - Saved Michael and Trevor."
                        }
                },
                {
                    0x534F1767,
                    new SPStat
                        {
                            Name = "SP_CELLPHONE_SLEEP_TIME",
                            Type = SPStatType.Int,
                            Comment = "The number of seconds that the player has had their phone in sleep mode."
                        }
                },
                {
                    0xEBE4CC5E,
                    new SPStat
                        {
                            Name = "SP_AMBIENT_SWITCH_COUNT",
                            Type = SPStatType.Int,
                            Comment = "The number of switches that have taken place off mission."
                        }
                },
                {
                    0x34E0D087,
                    new SPStat
                        {
                            Name = "SP_CAR_APP_ORDER_COUNT",
                            Type = SPStatType.Int,
                            Comment = "The number of times the player has processed a car app order."
                        }
                },
                {
                    0xAF548136,
                    new SPStat
                        {
                            Name = "SP_KILLED_ORTEGA",
                            Type = SPStatType.Bool,
                            Comment = "True when the player made the choice to kill Ortega in Trevor1."
                        }
                },
                {
                    0x34E4189C,
                    new SPStat
                        {
                            Name = "SP_KILLED_DR_FRIEDLANDER",
                            Type = SPStatType.Bool,
                            Comment = "True when the player made the choice to kill Dr. Friedlander in Shrink5."
                        }
                },
                {
                    0xF0F12DBA,
                    new SPStat
                        {
                            Name = "SP_KILLED_AL",
                            Type = SPStatType.Bool,
                            Comment = "True when the player made the choice to kill Al in Nigel3."
                        }
                },
                {
                    0xF7F50817,
                    new SPStat
                        {
                            Name = "SP_UNDER_THE_BRIDGE_COUNT",
                            Type = SPStatType.Int,
                            Comment = "The number of times the player has flew under a bridge."
                        }
                },
                {
                    0xD0C8638C,
                    new SPStat
                        {
                            Name = "SP_KNIFE_FLIGHTS_COUNT",
                            Type = SPStatType.Int,
                            Comment = "The number of times the player has been in a knife flight."
                        }
                },
                {
                    0xC25F07F9,
                    new SPStat
                        {
                            Name = "SP_MONEY_SPENT_ON_WEAPONS",
                            Type = SPStatType.Int,
                            Comment = "The amount of cash spent on weapons, mods, and ammo."
                        }
                },
                {
                    0xA8383B84,
                    new SPStat
                        {
                            Name = "SP_MONEY_SPENT_ON_CLOTHES",
                            Type = SPStatType.Int,
                            Comment = "The amount of cash spent on clothes."
                        }
                },
                {
                    0x91A61D0E,
                    new SPStat
                        {
                            Name = "SP_NUMBER_OF_TAXIS_USED",
                            Type = SPStatType.Int,
                            Comment = "The amount of times the player has used a taxi."
                        }
                },
                {
                    0x96244C42,
                    new SPStat
                        {
                            Name = "RE_WALLETS_RECOVERED",
                            Type = SPStatType.Int,
                            Comment = "Wallets recovered during REs."
                        }
                },
                {
                    0x3D6C7070,
                    new SPStat
                        {
                            Name = "RE_WALLETS_RETURNED",
                            Type = SPStatType.Int,
                            Comment = "Wallets returned during REs."
                        }
                },
                {
                    0x814395A9,
                    new SPStat
                        {
                            Name = "SP0_SUCCESSFUL_COUNTERS",
                            Type = SPStatType.Int,
                            Comment = "Number of melee counter counter"
                        }
                },
                {
                    0x4C1B21B7,
                    new SPStat
                        {
                            Name = "SP1_SUCCESSFUL_COUNTERS",
                            Type = SPStatType.Int,
                            Comment = "Number of melee counter counter"
                        }
                },
                {
                    0x5802F5EC,
                    new SPStat
                        {
                            Name = "SP2_SUCCESSFUL_COUNTERS",
                            Type = SPStatType.Int,
                            Comment = "Number of melee counter counter"
                        }
                },
                {
                    0x4ED35AEB,
                    new SPStat
                        {
                            Name = "SP0_FIRES_EXTINGUISHED",
                            Type = SPStatType.Int,
                            Comment = "Number of fires extinguished"
                        }
                },
                {
                    0x2BFC5476,
                    new SPStat
                        {
                            Name = "SP1_FIRES_EXTINGUISHED",
                            Type = SPStatType.Int,
                            Comment = "Number of fires extinguished"
                        }
                },
                {
                    0x30BC38F0,
                    new SPStat
                        {
                            Name = "SP2_FIRES_EXTINGUISHED",
                            Type = SPStatType.Int,
                            Comment = "Number of fires extinguished"
                        }
                },
                {
                    0x379AD859,
                    new SPStat {Name = "SP0_FIRES_STARTED", Type = SPStatType.Int, Comment = "Number of fires started"}
                },
                {
                    0x28CFC60A,
                    new SPStat {Name = "SP1_FIRES_STARTED", Type = SPStatType.Int, Comment = "Number of fires started"}
                },
                {
                    0xE6B48687,
                    new SPStat {Name = "SP2_FIRES_STARTED", Type = SPStatType.Int, Comment = "Number of fires started"}
                },
                {
                    0xEF942377,
                    new SPStat {Name = "SP0_TIMES_CHEATED", Type = SPStatType.Int, Comment = "Number of times cheated"}
                },
                {
                    0x658A6A42,
                    new SPStat {Name = "SP1_TIMES_CHEATED", Type = SPStatType.Int, Comment = "Number of times cheated"}
                },
                {
                    0xD23723F4,
                    new SPStat {Name = "SP2_TIMES_CHEATED", Type = SPStatType.Int, Comment = "Number of times cheated"}
                },
                {
                    0x85B741F9,
                    new SPStat {Name = "SP0_MISSION_NAME", Type = SPStatType.Label, Comment = "Current Mission name"}
                },
                {
                    0xA8D0ACBC,
                    new SPStat {Name = "SP1_MISSION_NAME", Type = SPStatType.Label, Comment = "Current Mission name"}
                },
                {
                    0x8EDCA2D4,
                    new SPStat {Name = "SP2_MISSION_NAME", Type = SPStatType.Label, Comment = "Current Mission name"}
                },
                {
                    0x7D6DD22B,
                    new SPStat
                        {
                            Name = "SP0_LAST_MISSION_NAME",
                            Type = SPStatType.Label,
                            Comment = "The last mission this character completed"
                        }
                },
                {
                    0x92A901E5,
                    new SPStat
                        {
                            Name = "SP1_LAST_MISSION_NAME",
                            Type = SPStatType.Label,
                            Comment = "The last mission this character completed"
                        }
                },
                {
                    0x65D3445A,
                    new SPStat
                        {
                            Name = "SP2_LAST_MISSION_NAME",
                            Type = SPStatType.Label,
                            Comment = "The last mission this character completed"
                        }
                },
                {
                    0xB4A8ED2F,
                    new SPStat
                        {
                            Name = "SP0_TOTAL_PLAYING_TIME",
                            Type = SPStatType.U64,
                            Comment = "Total Playing time in milliseconds - per character."
                        }
                },
                {
                    0x44F6F4CE,
                    new SPStat
                        {
                            Name = "SP1_TOTAL_PLAYING_TIME",
                            Type = SPStatType.U64,
                            Comment = "Total Playing time in milliseconds - per character."
                        }
                },
                {
                    0x8D571D8F,
                    new SPStat
                        {
                            Name = "SP2_TOTAL_PLAYING_TIME",
                            Type = SPStatType.U64,
                            Comment = "Total Playing time in milliseconds - per character."
                        }
                },
                {
                    0xC9E829C1,
                    new SPStat
                        {
                            Name = "SP0_LONGEST_PLAYING_TIME",
                            Type = SPStatType.U32,
                            Comment = "Longest Playing time since last death in milliseconds."
                        }
                },
                {
                    0xFC02F35D,
                    new SPStat
                        {
                            Name = "SP1_LONGEST_PLAYING_TIME",
                            Type = SPStatType.U32,
                            Comment = "Longest Playing time since last death in milliseconds."
                        }
                },
                {
                    0x221A3F87,
                    new SPStat
                        {
                            Name = "SP2_LONGEST_PLAYING_TIME",
                            Type = SPStatType.U32,
                            Comment = "Longest Playing time since last death in milliseconds."
                        }
                },
                {
                    0xE3BD66FC,
                    new SPStat
                        {
                            Name = "SP0_LONGEST_CAM_TIME_DRIVING",
                            Type = SPStatType.U32,
                            Comment = "Longest time spent driving in cinematic camera in milliseconds."
                        }
                },
                {
                    0x45656675,
                    new SPStat
                        {
                            Name = "SP1_LONGEST_CAM_TIME_DRIVING",
                            Type = SPStatType.U32,
                            Comment = "Longest time spent driving in cinematic camera in milliseconds."
                        }
                },
                {
                    0xC74DF4CA,
                    new SPStat
                        {
                            Name = "SP2_LONGEST_CAM_TIME_DRIVING",
                            Type = SPStatType.U32,
                            Comment = "Longest time spent driving in cinematic camera in milliseconds."
                        }
                },
                {
                    0x5BEBB365,
                    new SPStat
                        {
                            Name = "SP0_MISSIONS_PASSED",
                            Type = SPStatType.Int,
                            Comment = "Number of missions passed"
                        }
                },
                {
                    0x68E3051A,
                    new SPStat
                        {
                            Name = "SP1_MISSIONS_PASSED",
                            Type = SPStatType.Int,
                            Comment = "Number of missions passed"
                        }
                },
                {
                    0x0F5CCE25,
                    new SPStat
                        {
                            Name = "SP2_MISSIONS_PASSED",
                            Type = SPStatType.Int,
                            Comment = "Number of missions passed"
                        }
                },
                {
                    0x6D943FCC,
                    new SPStat
                        {
                            Name = "SP0_MISSIONS_FAILED",
                            Type = SPStatType.Int,
                            Comment = "Number of missions failed"
                        }
                },
                {
                    0x27A3FCA7,
                    new SPStat
                        {
                            Name = "SP1_MISSIONS_FAILED",
                            Type = SPStatType.Int,
                            Comment = "Number of missions failed"
                        }
                },
                {
                    0x6FD7C463,
                    new SPStat
                        {
                            Name = "SP2_MISSIONS_FAILED",
                            Type = SPStatType.Int,
                            Comment = "Number of missions failed"
                        }
                },
                {
                    0x4FC9A5DC,
                    new SPStat
                        {
                            Name = "SP0_SP_LEAST_FAVORITE_STATION",
                            Type = SPStatType.Int,
                            Comment = "Least favorite radio station"
                        }
                },
                {
                    0x73958049,
                    new SPStat
                        {
                            Name = "SP1_SP_LEAST_FAVORITE_STATION",
                            Type = SPStatType.Int,
                            Comment = "Least favorite radio station"
                        }
                },
                {
                    0x6D523110,
                    new SPStat
                        {
                            Name = "SP2_SP_LEAST_FAVORITE_STATION",
                            Type = SPStatType.Int,
                            Comment = "Least favorite radio station"
                        }
                },
                {
                    0x0E7EF5A2,
                    new SPStat
                        {
                            Name = "SP0_SP_MOST_FAVORITE_STATION",
                            Type = SPStatType.Int,
                            Comment = "Most favorite radio station"
                        }
                },
                {
                    0xEAC317FF,
                    new SPStat
                        {
                            Name = "SP1_SP_MOST_FAVORITE_STATION",
                            Type = SPStatType.Int,
                            Comment = "Most favorite radio station"
                        }
                },
                {
                    0x951D8509,
                    new SPStat
                        {
                            Name = "SP2_SP_MOST_FAVORITE_STATION",
                            Type = SPStatType.Int,
                            Comment = "Most favorite radio station"
                        }
                },
                {
                    0x0E6327A2,
                    new SPStat
                        {
                            Name = "SP0_DIED_IN_DROWNING",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died by drowning"
                        }
                },
                {
                    0x0D58A018,
                    new SPStat
                        {
                            Name = "SP1_DIED_IN_DROWNING",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died by drowning"
                        }
                },
                {
                    0x7C204DF1,
                    new SPStat
                        {
                            Name = "SP2_DIED_IN_DROWNING",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died by drowning"
                        }
                },
                {
                    0xCC3E43D8,
                    new SPStat
                        {
                            Name = "SP0_DIED_IN_DROWNINGINVEHICLE",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died by drowning in vehicle"
                        }
                },
                {
                    0x89627C8B,
                    new SPStat
                        {
                            Name = "SP1_DIED_IN_DROWNINGINVEHICLE",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died by drowning in vehicle"
                        }
                },
                {
                    0x95F47875,
                    new SPStat
                        {
                            Name = "SP2_DIED_IN_DROWNINGINVEHICLE",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died by drowning in vehicle"
                        }
                },
                {
                    0xEF39FF1E,
                    new SPStat
                        {
                            Name = "SP0_DIED_IN_EXPLOSION",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died by explosion"
                        }
                },
                {
                    0x93EB58EC,
                    new SPStat
                        {
                            Name = "SP1_DIED_IN_EXPLOSION",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died by explosion"
                        }
                },
                {
                    0xADCAD7DD,
                    new SPStat
                        {
                            Name = "SP2_DIED_IN_EXPLOSION",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died by explosion"
                        }
                },
                {
                    0x4CC0638B,
                    new SPStat
                        {
                            Name = "SP0_DIED_IN_FALL",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died by fall "
                        }
                },
                {
                    0x12775C2C,
                    new SPStat
                        {
                            Name = "SP1_DIED_IN_FALL",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died by fall "
                        }
                },
                {
                    0x0FEF6027,
                    new SPStat
                        {
                            Name = "SP2_DIED_IN_FALL",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died by fall "
                        }
                },
                {
                    0x1183803D,
                    new SPStat
                        {
                            Name = "SP0_DIED_IN_FIRE",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died by fire"
                        }
                },
                {
                    0xE7DD2D72,
                    new SPStat
                        {
                            Name = "SP1_DIED_IN_FIRE",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died by fire"
                        }
                },
                {
                    0x05989779,
                    new SPStat
                        {
                            Name = "SP2_DIED_IN_FIRE",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died by fire"
                        }
                },
                {
                    0x24576060,
                    new SPStat
                        {
                            Name = "SP0_DIED_IN_ROAD",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died in a road accident"
                        }
                },
                {
                    0x6B95364D,
                    new SPStat
                        {
                            Name = "SP1_DIED_IN_ROAD",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died in a road accident"
                        }
                },
                {
                    0x450F028A,
                    new SPStat
                        {
                            Name = "SP2_DIED_IN_ROAD",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died in a road accident"
                        }
                },
                {
                    0x27092988,
                    new SPStat
                        {
                            Name = "SP0_DIED_IN_MISSION",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died in a mission"
                        }
                },
                {
                    0x339212EE,
                    new SPStat
                        {
                            Name = "SP1_DIED_IN_MISSION",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died in a mission"
                        }
                },
                {
                    0xCEC3144E,
                    new SPStat
                        {
                            Name = "SP2_DIED_IN_MISSION",
                            Type = SPStatType.Int,
                            Comment = "Number of times player died in a mission"
                        }
                },
                {
                    0x5006898A,
                    new SPStat
                        {
                            Name = "SP0_BUSTED",
                            Type = SPStatType.Int,
                            Comment = "Number of times the player was arrested"
                        }
                },
                {
                    0xF0742DB2,
                    new SPStat
                        {
                            Name = "SP1_BUSTED",
                            Type = SPStatType.Int,
                            Comment = "Number of times the player was arrested"
                        }
                },
                {
                    0x6F23A8C6,
                    new SPStat
                        {
                            Name = "SP2_BUSTED",
                            Type = SPStatType.Int,
                            Comment = "Number of times the player was arrested"
                        }
                },
                {
                    0x1FA0D512,
                    new SPStat
                        {
                            Name = "SP0_MANUAL_SAVED",
                            Type = SPStatType.Int,
                            Comment = "Number of times saved the game manually"
                        }
                },
                {
                    0x89434C62,
                    new SPStat
                        {
                            Name = "SP1_MANUAL_SAVED",
                            Type = SPStatType.Int,
                            Comment = "Number of times saved the game manually"
                        }
                },
                {
                    0x62255ACD,
                    new SPStat
                        {
                            Name = "SP2_MANUAL_SAVED",
                            Type = SPStatType.Int,
                            Comment = "Number of times saved the game manually"
                        }
                },
                {
                    0x03722CBD,
                    new SPStat
                        {
                            Name = "SP0_AUTO_SAVED",
                            Type = SPStatType.Int,
                            Comment = "Number of times saved the game automatically"
                        }
                },
                {
                    0x14E45F66,
                    new SPStat
                        {
                            Name = "SP1_AUTO_SAVED",
                            Type = SPStatType.Int,
                            Comment = "Number of times saved the game automatically"
                        }
                },
                {
                    0x92628902,
                    new SPStat
                        {
                            Name = "SP2_AUTO_SAVED",
                            Type = SPStatType.Int,
                            Comment = "Number of times saved the game automatically"
                        }
                },
                {
                    0x31C1D075,
                    new SPStat
                        {
                            Name = "SP0_COPS_KILLS_ON_SPREE",
                            Type = SPStatType.Int,
                            Comment = "Number of Cops killed in spree."
                        }
                },
                {
                    0xB1D9F3C1,
                    new SPStat
                        {
                            Name = "SP1_COPS_KILLS_ON_SPREE",
                            Type = SPStatType.Int,
                            Comment = "Number of Cops killed in spree."
                        }
                },
                {
                    0x09A8459B,
                    new SPStat
                        {
                            Name = "SP2_COPS_KILLS_ON_SPREE",
                            Type = SPStatType.Int,
                            Comment = "Number of Cops killed in spree."
                        }
                },
                {
                    0xF5FBEBC9,
                    new SPStat
                        {
                            Name = "SP0_PEDS_KILLS_ON_SPREE",
                            Type = SPStatType.Int,
                            Comment = "Number of Peds killed in spree."
                        }
                },
                {
                    0xAFBCBB2A,
                    new SPStat
                        {
                            Name = "SP1_PEDS_KILLS_ON_SPREE",
                            Type = SPStatType.Int,
                            Comment = "Number of Peds killed in spree."
                        }
                },
                {
                    0x772DD779,
                    new SPStat
                        {
                            Name = "SP2_PEDS_KILLS_ON_SPREE",
                            Type = SPStatType.Int,
                            Comment = "Number of Peds killed in spree."
                        }
                },
                {
                    0xE7F7D5EF,
                    new SPStat
                        {
                            Name = "SP0_LONGEST_KILLING_SPREE",
                            Type = SPStatType.U32,
                            Comment = "Longest killing count in spree."
                        }
                },
                {
                    0x826F5683,
                    new SPStat
                        {
                            Name = "SP1_LONGEST_KILLING_SPREE",
                            Type = SPStatType.U32,
                            Comment = "Longest killing count in spree."
                        }
                },
                {
                    0x4BC48058,
                    new SPStat
                        {
                            Name = "SP2_LONGEST_KILLING_SPREE",
                            Type = SPStatType.U32,
                            Comment = "Longest killing count in spree."
                        }
                },
                {
                    0x2666E4ED,
                    new SPStat
                        {
                            Name = "SP0_LONGEST_KILLING_SPREE_TIME",
                            Type = SPStatType.U32,
                            Comment = "Longest killing time count in spree."
                        }
                },
                {
                    0xE392455F,
                    new SPStat
                        {
                            Name = "SP1_LONGEST_KILLING_SPREE_TIME",
                            Type = SPStatType.U32,
                            Comment = "Longest killing time count in spree."
                        }
                },
                {
                    0xEC3E844B,
                    new SPStat
                        {
                            Name = "SP2_LONGEST_KILLING_SPREE_TIME",
                            Type = SPStatType.U32,
                            Comment = "Longest killing time count in spree."
                        }
                },
                {
                    0x7822DF05,
                    new SPStat {Name = "SP0_KILLS_STEALTH", Type = SPStatType.Int, Comment = "Number of stealth kills"}
                },
                {
                    0x5024F460,
                    new SPStat {Name = "SP1_KILLS_STEALTH", Type = SPStatType.Int, Comment = "Number of stealth kills"}
                },
                {
                    0x2C9E9FC1,
                    new SPStat {Name = "SP2_KILLS_STEALTH", Type = SPStatType.Int, Comment = "Number of stealth kills"}
                },
                {
                    0x6CC02238,
                    new SPStat {Name = "SP0_KILLS_INNOCENTS", Type = SPStatType.Int, Comment = "Number of peds killed"}
                },
                {
                    0x6EEDA9DB,
                    new SPStat {Name = "SP1_KILLS_INNOCENTS", Type = SPStatType.Int, Comment = "Number of peds killed"}
                },
                {
                    0xA21AE0C7,
                    new SPStat {Name = "SP2_KILLS_INNOCENTS", Type = SPStatType.Int, Comment = "Number of peds killed"}
                },
                {
                    0x0B89D4A4,
                    new SPStat {Name = "SP0_KILLS_COP", Type = SPStatType.Int, Comment = "Number of Cops killed"}
                },
                {
                    0xEDCBDF44,
                    new SPStat {Name = "SP1_KILLS_COP", Type = SPStatType.Int, Comment = "Number of Cops killed"}
                },
                {
                    0xED88DC67,
                    new SPStat {Name = "SP2_KILLS_COP", Type = SPStatType.Int, Comment = "Number of Cops killed"}
                },
                {
                    0x097B47C5,
                    new SPStat {Name = "SP0_KILLS_SWAT", Type = SPStatType.Int, Comment = "Number of Swat killed"}
                },
                {
                    0x34C841C7,
                    new SPStat {Name = "SP1_KILLS_SWAT", Type = SPStatType.Int, Comment = "Number of Swat killed"}
                },
                {
                    0x2F0FF8F1,
                    new SPStat {Name = "SP2_KILLS_SWAT", Type = SPStatType.Int, Comment = "Number of Swat killed"}
                },
                {
                    0x8ED3CD34,
                    new SPStat
                        {
                            Name = "SP0_KILLS_SINCE_LAST_CHECKPOINT",
                            Type = SPStatType.Int,
                            Comment = "All kills since last checkpoint"
                        }
                },
                {
                    0xD8B1083C,
                    new SPStat
                        {
                            Name = "SP1_KILLS_SINCE_LAST_CHECKPOINT",
                            Type = SPStatType.Int,
                            Comment = "All kills since last checkpoint"
                        }
                },
                {
                    0xFBB45584,
                    new SPStat
                        {
                            Name = "SP2_KILLS_SINCE_LAST_CHECKPOINT",
                            Type = SPStatType.Int,
                            Comment = "All kills since last checkpoint"
                        }
                },
                {
                    0xFC394200,
                    new SPStat
                        {
                            Name = "SP0_KILLS_SINCE_SAFEHOUSE_VISIT",
                            Type = SPStatType.Int,
                            Comment = "All Kills since last safe house visit."
                        }
                },
                {
                    0x6B15DBEA,
                    new SPStat
                        {
                            Name = "SP1_KILLS_SINCE_SAFEHOUSE_VISIT",
                            Type = SPStatType.Int,
                            Comment = "All Kills since last safe house visit."
                        }
                },
                {
                    0x1F782AEA,
                    new SPStat
                        {
                            Name = "SP2_KILLS_SINCE_SAFEHOUSE_VISIT",
                            Type = SPStatType.Int,
                            Comment = "All Kills since last safe house visit."
                        }
                },
                {
                    0x71A7CCC5,
                    new SPStat
                        {
                            Name = "SP0_TOTAL_LEGITIMATE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Total number of kills from checkpoints."
                        }
                },
                {
                    0x1287A641,
                    new SPStat
                        {
                            Name = "SP1_TOTAL_LEGITIMATE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Total number of kills from checkpoints."
                        }
                },
                {
                    0x8FDCFD93,
                    new SPStat
                        {
                            Name = "SP2_TOTAL_LEGITIMATE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Total number of kills from checkpoints."
                        }
                },
                {
                    0x3FB4EC15,
                    new SPStat
                        {
                            Name = "SP0_KILLS_BY_OTHERS",
                            Type = SPStatType.Int,
                            Comment = "Peds killed by other players"
                        }
                },
                {
                    0x51920ACC,
                    new SPStat
                        {
                            Name = "SP1_KILLS_BY_OTHERS",
                            Type = SPStatType.Int,
                            Comment = "Peds killed by other players"
                        }
                },
                {
                    0xD07F1C68,
                    new SPStat
                        {
                            Name = "SP2_KILLS_BY_OTHERS",
                            Type = SPStatType.Int,
                            Comment = "Peds killed by other players"
                        }
                },
                {
                    0x292FC5F4,
                    new SPStat
                        {
                            Name = "SP0_LARGE_ACCIDENTS",
                            Type = SPStatType.Int,
                            Comment = "Number of large accidents."
                        }
                },
                {
                    0x3B2640EF,
                    new SPStat
                        {
                            Name = "SP1_LARGE_ACCIDENTS",
                            Type = SPStatType.Int,
                            Comment = "Number of large accidents."
                        }
                },
                {
                    0x41BC05A3,
                    new SPStat
                        {
                            Name = "SP2_LARGE_ACCIDENTS",
                            Type = SPStatType.Int,
                            Comment = "Number of large accidents."
                        }
                },
                {
                    0x6F83C4D6,
                    new SPStat
                        {
                            Name = "SP0_LONGEST_DRIVE_NOCRASH",
                            Type = SPStatType.Float,
                            Comment = "Longest Drive Without a crash"
                        }
                },
                {
                    0x67F26B2C,
                    new SPStat
                        {
                            Name = "SP1_LONGEST_DRIVE_NOCRASH",
                            Type = SPStatType.Float,
                            Comment = "Longest Drive Without a crash"
                        }
                },
                {
                    0x955C15EA,
                    new SPStat
                        {
                            Name = "SP2_LONGEST_DRIVE_NOCRASH",
                            Type = SPStatType.Float,
                            Comment = "Longest Drive Without a crash"
                        }
                },
                {
                    0x66858CFB,
                    new SPStat {Name = "SP0_DIST_CAR", Type = SPStatType.Float, Comment = "Total Distance in a car"}
                },
                {
                    0x22361D14,
                    new SPStat {Name = "SP1_DIST_CAR", Type = SPStatType.Float, Comment = "Total Distance in a car"}
                },
                {
                    0x5793500B,
                    new SPStat {Name = "SP2_DIST_CAR", Type = SPStatType.Float, Comment = "Total Distance in a car"}
                },
                {
                    0xACC0B8F3,
                    new SPStat
                        {
                            Name = "SP0_DIST_DRIVING_CAR",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a car"
                        }
                },
                {
                    0xE2FB0D71,
                    new SPStat
                        {
                            Name = "SP1_DIST_DRIVING_CAR",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a car"
                        }
                },
                {
                    0x813F6F8E,
                    new SPStat
                        {
                            Name = "SP2_DIST_DRIVING_CAR",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a car"
                        }
                },
                {
                    0x703AD554,
                    new SPStat
                        {
                            Name = "SP0_TIME_DRIVING_CAR",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a car"
                        }
                },
                {
                    0xE5E7BE54,
                    new SPStat
                        {
                            Name = "SP1_TIME_DRIVING_CAR",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a car"
                        }
                },
                {
                    0xCF7B2160,
                    new SPStat
                        {
                            Name = "SP2_TIME_DRIVING_CAR",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a car"
                        }
                },
                {
                    0x30E41DE9,
                    new SPStat
                        {
                            Name = "SP0_DIST_PLANE",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a plane"
                        }
                },
                {
                    0x484FF3B4,
                    new SPStat
                        {
                            Name = "SP1_DIST_PLANE",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a plane"
                        }
                },
                {
                    0x55C5736B,
                    new SPStat
                        {
                            Name = "SP2_DIST_PLANE",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a plane"
                        }
                },
                {
                    0xC0D06C21,
                    new SPStat
                        {
                            Name = "SP0_DIST_DRIVING_PLANE",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a plane"
                        }
                },
                {
                    0x44D57F52,
                    new SPStat
                        {
                            Name = "SP1_DIST_DRIVING_PLANE",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a plane"
                        }
                },
                {
                    0x96F92324,
                    new SPStat
                        {
                            Name = "SP2_DIST_DRIVING_PLANE",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a plane"
                        }
                },
                {
                    0x900983E9,
                    new SPStat
                        {
                            Name = "SP0_TIME_DRIVING_PLANE",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a plane"
                        }
                },
                {
                    0xE301A690,
                    new SPStat
                        {
                            Name = "SP1_TIME_DRIVING_PLANE",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a plane"
                        }
                },
                {
                    0x614DB6DF,
                    new SPStat
                        {
                            Name = "SP2_TIME_DRIVING_PLANE",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a plane"
                        }
                },
                {
                    0xDB3AC4C4,
                    new SPStat
                        {
                            Name = "SP0_DIST_QUADBIKE",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a quadbike"
                        }
                },
                {
                    0x7CE364EF,
                    new SPStat
                        {
                            Name = "SP1_DIST_QUADBIKE",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a quadbike"
                        }
                },
                {
                    0xAC9F19B0,
                    new SPStat
                        {
                            Name = "SP2_DIST_QUADBIKE",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a quadbike"
                        }
                },
                {
                    0xB2C8E1D7,
                    new SPStat
                        {
                            Name = "SP0_DIST_DRIVING_QUADBIKE",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a quadbike"
                        }
                },
                {
                    0x751A9CDA,
                    new SPStat
                        {
                            Name = "SP1_DIST_DRIVING_QUADBIKE",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a quadbike"
                        }
                },
                {
                    0x76411A6F,
                    new SPStat
                        {
                            Name = "SP2_DIST_DRIVING_QUADBIKE",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a quadbike"
                        }
                },
                {
                    0xACFA5AF8,
                    new SPStat
                        {
                            Name = "SP0_TIME_DRIVING_QUADBIKE",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a quadbike"
                        }
                },
                {
                    0xF34371B2,
                    new SPStat
                        {
                            Name = "SP1_TIME_DRIVING_QUADBIKE",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a quadbike"
                        }
                },
                {
                    0xFCB385B9,
                    new SPStat
                        {
                            Name = "SP2_TIME_DRIVING_QUADBIKE",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a quadbike"
                        }
                },
                {
                    0x51285156,
                    new SPStat
                        {
                            Name = "SP0_DIST_HELI",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a heli"
                        }
                },
                {
                    0xAF516749,
                    new SPStat
                        {
                            Name = "SP1_DIST_HELI",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a heli"
                        }
                },
                {
                    0x143012A6,
                    new SPStat
                        {
                            Name = "SP2_DIST_HELI",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a heli"
                        }
                },
                {
                    0xD6C43962,
                    new SPStat
                        {
                            Name = "SP0_DIST_DRIVING_HELI",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a heli"
                        }
                },
                {
                    0x971DF8A4,
                    new SPStat
                        {
                            Name = "SP1_DIST_DRIVING_HELI",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a heli"
                        }
                },
                {
                    0x0DD73216,
                    new SPStat
                        {
                            Name = "SP2_DIST_DRIVING_HELI",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a heli"
                        }
                },
                {
                    0x69039180,
                    new SPStat
                        {
                            Name = "SP0_TIME_DRIVING_HELI",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a heli"
                        }
                },
                {
                    0xE275A1BF,
                    new SPStat
                        {
                            Name = "SP1_TIME_DRIVING_HELI",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a heli"
                        }
                },
                {
                    0xA27342A6,
                    new SPStat
                        {
                            Name = "SP2_TIME_DRIVING_HELI",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a heli"
                        }
                },
                {
                    0xFB22C036,
                    new SPStat
                        {
                            Name = "SP0_DIST_BIKE",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a bike"
                        }
                },
                {
                    0xBD87354D,
                    new SPStat
                        {
                            Name = "SP1_DIST_BIKE",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a bike"
                        }
                },
                {
                    0xFCD36626,
                    new SPStat
                        {
                            Name = "SP2_DIST_BIKE",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a bike"
                        }
                },
                {
                    0x1CFEE5C3,
                    new SPStat
                        {
                            Name = "SP0_DIST_DRIVING_BIKE",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a bike"
                        }
                },
                {
                    0x7FC7D3F8,
                    new SPStat
                        {
                            Name = "SP1_DIST_DRIVING_BIKE",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a bike"
                        }
                },
                {
                    0x3C915448,
                    new SPStat
                        {
                            Name = "SP2_DIST_DRIVING_BIKE",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a bike"
                        }
                },
                {
                    0x8BCEF8AA,
                    new SPStat
                        {
                            Name = "SP0_TIME_DRIVING_BIKE",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a bike"
                        }
                },
                {
                    0x36F8C66E,
                    new SPStat
                        {
                            Name = "SP1_TIME_DRIVING_BIKE",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a bike"
                        }
                },
                {
                    0x933395BD,
                    new SPStat
                        {
                            Name = "SP2_TIME_DRIVING_BIKE",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a bike"
                        }
                },
                {
                    0xB77EB9A5,
                    new SPStat
                        {
                            Name = "SP0_DIST_BICYCLE",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a bicycle"
                        }
                },
                {
                    0x1CC64CAF,
                    new SPStat
                        {
                            Name = "SP1_DIST_BICYCLE",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a bicycle"
                        }
                },
                {
                    0xE03A6157,
                    new SPStat
                        {
                            Name = "SP2_DIST_BICYCLE",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a bicycle"
                        }
                },
                {
                    0x462448B6,
                    new SPStat
                        {
                            Name = "SP0_DIST_DRIVING_BICYCLE",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a bicycle"
                        }
                },
                {
                    0x5A54B545,
                    new SPStat
                        {
                            Name = "SP1_DIST_DRIVING_BICYCLE",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a bicycle"
                        }
                },
                {
                    0x2510A582,
                    new SPStat
                        {
                            Name = "SP2_DIST_DRIVING_BICYCLE",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a bicycle"
                        }
                },
                {
                    0xC421EB13,
                    new SPStat
                        {
                            Name = "SP0_TIME_DRIVING_BICYCLE",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a bicycle"
                        }
                },
                {
                    0x4BEAE1EA,
                    new SPStat
                        {
                            Name = "SP1_TIME_DRIVING_BICYCLE",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a bicycle"
                        }
                },
                {
                    0x4D2185C6,
                    new SPStat
                        {
                            Name = "SP2_TIME_DRIVING_BICYCLE",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a bicycle"
                        }
                },
                {
                    0x3D537384,
                    new SPStat
                        {
                            Name = "SP0_DIST_BOAT",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a boat"
                        }
                },
                {
                    0x8A403342,
                    new SPStat
                        {
                            Name = "SP1_DIST_BOAT",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a boat"
                        }
                },
                {
                    0x17BDC976,
                    new SPStat
                        {
                            Name = "SP2_DIST_BOAT",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a boat"
                        }
                },
                {
                    0xEEE5F002,
                    new SPStat
                        {
                            Name = "SP0_DIST_DRIVING_BOAT",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a boat"
                        }
                },
                {
                    0x5A2A7CCF,
                    new SPStat
                        {
                            Name = "SP1_DIST_DRIVING_BOAT",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a boat"
                        }
                },
                {
                    0x53F4B827,
                    new SPStat
                        {
                            Name = "SP2_DIST_DRIVING_BOAT",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a boat"
                        }
                },
                {
                    0xA1DFC3E0,
                    new SPStat
                        {
                            Name = "SP0_TIME_DRIVING_BOAT",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a boat"
                        }
                },
                {
                    0x40180ACC,
                    new SPStat
                        {
                            Name = "SP1_TIME_DRIVING_BOAT",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a boat"
                        }
                },
                {
                    0x125A8499,
                    new SPStat
                        {
                            Name = "SP2_TIME_DRIVING_BOAT",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a boat"
                        }
                },
                {
                    0x997D6BE0,
                    new SPStat
                        {
                            Name = "SP0_DIST_SUBMARINE",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a submarine"
                        }
                },
                {
                    0x7D5CBA0B,
                    new SPStat
                        {
                            Name = "SP1_DIST_SUBMARINE",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a submarine"
                        }
                },
                {
                    0xAC49ED1A,
                    new SPStat
                        {
                            Name = "SP2_DIST_SUBMARINE",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a submarine"
                        }
                },
                {
                    0xC71DB08E,
                    new SPStat
                        {
                            Name = "SP0_DIST_DRIVING_SUBMARINE",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a submarine"
                        }
                },
                {
                    0x429826EC,
                    new SPStat
                        {
                            Name = "SP1_DIST_DRIVING_SUBMARINE",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a submarine"
                        }
                },
                {
                    0x2D2D1D51,
                    new SPStat
                        {
                            Name = "SP2_DIST_DRIVING_SUBMARINE",
                            Type = SPStatType.Float,
                            Comment = "Total time driving a submarine"
                        }
                },
                {
                    0x7316265D,
                    new SPStat
                        {
                            Name = "SP0_TIME_DRIVING_SUBMARINE",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a submarine"
                        }
                },
                {
                    0x28C67737,
                    new SPStat
                        {
                            Name = "SP1_TIME_DRIVING_SUBMARINE",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a submarine"
                        }
                },
                {
                    0x99C22D56,
                    new SPStat
                        {
                            Name = "SP2_TIME_DRIVING_SUBMARINE",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a submarine"
                        }
                },
                {
                    0x451BE82B,
                    new SPStat
                        {
                            Name = "SP0_DIST_SWIMMING",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a swimming"
                        }
                },
                {
                    0xD89276BF,
                    new SPStat
                        {
                            Name = "SP1_DIST_SWIMMING",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a swimming"
                        }
                },
                {
                    0x0A3D86DB,
                    new SPStat
                        {
                            Name = "SP2_DIST_SWIMMING",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a swimming"
                        }
                },
                {
                    0x85B20F46,
                    new SPStat
                        {
                            Name = "SP0_TIME_SWIMMING",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a swimming"
                        }
                },
                {
                    0x00FC0047,
                    new SPStat
                        {
                            Name = "SP1_TIME_SWIMMING",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a swimming"
                        }
                },
                {
                    0xE3A51E94,
                    new SPStat
                        {
                            Name = "SP2_TIME_SWIMMING",
                            Type = SPStatType.U64,
                            Comment = "Total time driving a swimming"
                        }
                },
                {
                    0xE1945801,
                    new SPStat
                        {
                            Name = "SP0_DIST_WALKING",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a walking"
                        }
                },
                {
                    0x13BAF711,
                    new SPStat
                        {
                            Name = "SP1_DIST_WALKING",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a walking"
                        }
                },
                {
                    0x572EFB3E,
                    new SPStat
                        {
                            Name = "SP2_DIST_WALKING",
                            Type = SPStatType.Float,
                            Comment = "Total Distance driving a walking"
                        }
                },
                {
                    0x89A91E32,
                    new SPStat {Name = "SP0_TIME_WALKING", Type = SPStatType.U64, Comment = "Total time spent walking"}
                },
                {
                    0x4B167E9F,
                    new SPStat {Name = "SP1_TIME_WALKING", Type = SPStatType.U64, Comment = "Total time spent walking"}
                },
                {
                    0x01D749CB,
                    new SPStat {Name = "SP2_TIME_WALKING", Type = SPStatType.U64, Comment = "Total time spent walking"}
                },
                {
                    0x269652AD,
                    new SPStat
                        {
                            Name = "SP0_DIST_WALK_ST",
                            Type = SPStatType.Float,
                            Comment = "Total Distance walking in Stealth mode"
                        }
                },
                {
                    0x2CFEBE25,
                    new SPStat
                        {
                            Name = "SP1_DIST_WALK_ST",
                            Type = SPStatType.Float,
                            Comment = "Total Distance walking in Stealth mode"
                        }
                },
                {
                    0x01D40EF6,
                    new SPStat
                        {
                            Name = "SP2_DIST_WALK_ST",
                            Type = SPStatType.Float,
                            Comment = "Total Distance walking in Stealth mode"
                        }
                },
                {
                    0xA245390A,
                    new SPStat {Name = "SP0_DIST_RUNNING", Type = SPStatType.Float, Comment = "Total Distance running"}
                },
                {
                    0x9B0D6F2B,
                    new SPStat {Name = "SP1_DIST_RUNNING", Type = SPStatType.Float, Comment = "Total Distance running"}
                },
                {
                    0x17B89DAB,
                    new SPStat {Name = "SP2_DIST_RUNNING", Type = SPStatType.Float, Comment = "Total Distance running"}
                },
                {
                    0x6B56E540,
                    new SPStat {Name = "SP0_TIME_UNDERWATER", Type = SPStatType.U64, Comment = "Total time underwater."}
                },
                {
                    0x62137830,
                    new SPStat {Name = "SP1_TIME_UNDERWATER", Type = SPStatType.U64, Comment = "Total time underwater."}
                },
                {
                    0x703993D3,
                    new SPStat {Name = "SP2_TIME_UNDERWATER", Type = SPStatType.U64, Comment = "Total time underwater."}
                },
                {
                    0x7CBE2E91,
                    new SPStat {Name = "SP0_TIME_IN_WATER", Type = SPStatType.U64, Comment = "Total time in water."}
                },
                {
                    0x02B9667A,
                    new SPStat {Name = "SP1_TIME_IN_WATER", Type = SPStatType.U64, Comment = "Total time in water."}
                },
                {
                    0x8A7BC844,
                    new SPStat {Name = "SP2_TIME_IN_WATER", Type = SPStatType.U64, Comment = "Total time in water."}
                },
                {
                    0x5004C7A5,
                    new SPStat
                        {
                            Name = "SP0_TIME_IN_COVER",
                            Type = SPStatType.U64,
                            Comment = "Total time spent in cover"
                        }
                },
                {
                    0xC1DC2326,
                    new SPStat
                        {
                            Name = "SP1_TIME_IN_COVER",
                            Type = SPStatType.U64,
                            Comment = "Total time spent in cover"
                        }
                },
                {
                    0x2DC901C1,
                    new SPStat
                        {
                            Name = "SP2_TIME_IN_COVER",
                            Type = SPStatType.U64,
                            Comment = "Total time spent in cover"
                        }
                },
                {
                    0xA6A91747,
                    new SPStat
                        {
                            Name = "SP0_ENTERED_COVER",
                            Type = SPStatType.Int,
                            Comment = "Number of times the player has entered cover"
                        }
                },
                {
                    0x8A2A16C5,
                    new SPStat
                        {
                            Name = "SP1_ENTERED_COVER",
                            Type = SPStatType.Int,
                            Comment = "Number of times the player has entered cover"
                        }
                },
                {
                    0x0BF9C30A,
                    new SPStat
                        {
                            Name = "SP2_ENTERED_COVER",
                            Type = SPStatType.Int,
                            Comment = "Number of times the player has entered cover"
                        }
                },
                {
                    0x0214A2DF,
                    new SPStat
                        {
                            Name = "SP0_ENTERED_COVER_AND_SHOT",
                            Type = SPStatType.Int,
                            Comment = "Number of times the player has entered cover and fired a shot or shots"
                        }
                },
                {
                    0xCFCA5719,
                    new SPStat
                        {
                            Name = "SP1_ENTERED_COVER_AND_SHOT",
                            Type = SPStatType.Int,
                            Comment = "Number of times the player has entered cover and fired a shot or shots"
                        }
                },
                {
                    0x19A85630,
                    new SPStat
                        {
                            Name = "SP2_ENTERED_COVER_AND_SHOT",
                            Type = SPStatType.Int,
                            Comment = "Number of times the player has entered cover and fired a shot or shots"
                        }
                },
                {
                    0xD33D26E4,
                    new SPStat
                        {
                            Name = "SP0_CROUCHED",
                            Type = SPStatType.Int,
                            Comment = "Number of times the player has crouched"
                        }
                },
                {
                    0x37BEDDBE,
                    new SPStat
                        {
                            Name = "SP1_CROUCHED",
                            Type = SPStatType.Int,
                            Comment = "Number of times the player has crouched"
                        }
                },
                {
                    0xFFEE9DFC,
                    new SPStat
                        {
                            Name = "SP2_CROUCHED",
                            Type = SPStatType.Int,
                            Comment = "Number of times the player has crouched"
                        }
                },
                {
                    0x8E9DD911,
                    new SPStat
                        {
                            Name = "SP0_CROUCHED_AND_SHOT",
                            Type = SPStatType.Int,
                            Comment = "Number of times the player has crouched and fired a shot or shots"
                        }
                },
                {
                    0xF805C350,
                    new SPStat
                        {
                            Name = "SP1_CROUCHED_AND_SHOT",
                            Type = SPStatType.Int,
                            Comment = "Number of times the player has crouched and fired a shot or shots"
                        }
                },
                {
                    0xE02E71AF,
                    new SPStat
                        {
                            Name = "SP2_CROUCHED_AND_SHOT",
                            Type = SPStatType.Int,
                            Comment = "Number of times the player has crouched and fired a shot or shots"
                        }
                },
                {
                    0xB0F82E33,
                    new SPStat
                        {
                            Name = "SP0_DIST_AS_PASSENGER_TAXI",
                            Type = SPStatType.Float,
                            Comment = "Total distance as a taxi passenger"
                        }
                },
                {
                    0xE4D9CF3D,
                    new SPStat
                        {
                            Name = "SP1_DIST_AS_PASSENGER_TAXI",
                            Type = SPStatType.Float,
                            Comment = "Total distance as a taxi passenger"
                        }
                },
                {
                    0x9F14B4C6,
                    new SPStat
                        {
                            Name = "SP2_DIST_AS_PASSENGER_TAXI",
                            Type = SPStatType.Float,
                            Comment = "Total distance as a taxi passenger"
                        }
                },
                {
                    0x7791342E,
                    new SPStat
                        {
                            Name = "SP0_SKIPDIST_AS_PASSENGER_TAXI",
                            Type = SPStatType.Float,
                            Comment = "Total distance as a taxi passenger skipped"
                        }
                },
                {
                    0xB2F4BD87,
                    new SPStat
                        {
                            Name = "SP1_SKIPDIST_AS_PASSENGER_TAXI",
                            Type = SPStatType.Float,
                            Comment = "Total distance as a taxi passenger skipped"
                        }
                },
                {
                    0x3150E4C3,
                    new SPStat
                        {
                            Name = "SP2_SKIPDIST_AS_PASSENGER_TAXI",
                            Type = SPStatType.Float,
                            Comment = "Total distance as a taxi passenger skipped"
                        }
                },
                {
                    0xC4125C81,
                    new SPStat
                        {
                            Name = "SP0_DIST_AS_PASSENGER_TRAIN",
                            Type = SPStatType.Float,
                            Comment = "Total distance as a train passenger"
                        }
                },
                {
                    0xC10A1A10,
                    new SPStat
                        {
                            Name = "SP1_DIST_AS_PASSENGER_TRAIN",
                            Type = SPStatType.Float,
                            Comment = "Total distance as a train passenger"
                        }
                },
                {
                    0x877B1BFE,
                    new SPStat
                        {
                            Name = "SP2_DIST_AS_PASSENGER_TRAIN",
                            Type = SPStatType.Float,
                            Comment = "Total distance as a train passenger"
                        }
                },
                {
                    0x0C57EEDD,
                    new SPStat
                        {
                            Name = "SP0_AVERAGE_SPEED",
                            Type = SPStatType.Float,
                            Comment = "Average speed when driving a car or a bike"
                        }
                },
                {
                    0x9E0B8921,
                    new SPStat
                        {
                            Name = "SP1_AVERAGE_SPEED",
                            Type = SPStatType.Float,
                            Comment = "Average speed when driving a car or a bike"
                        }
                },
                {
                    0x5CFD8910,
                    new SPStat
                        {
                            Name = "SP2_AVERAGE_SPEED",
                            Type = SPStatType.Float,
                            Comment = "Average speed when driving a car or a bike"
                        }
                },
                {
                    0x626A8B64,
                    new SPStat
                        {
                            Name = "SP0_FLIGHT_TIME",
                            Type = SPStatType.Float,
                            Comment = "Average speed when driving a car or a bike"
                        }
                },
                {
                    0xF336EA6B,
                    new SPStat
                        {
                            Name = "SP1_FLIGHT_TIME",
                            Type = SPStatType.Float,
                            Comment = "Average speed when driving a car or a bike"
                        }
                },
                {
                    0x66CF673A,
                    new SPStat
                        {
                            Name = "SP2_FLIGHT_TIME",
                            Type = SPStatType.Float,
                            Comment = "Average speed when driving a car or a bike"
                        }
                },
                {
                    0xEF705AB5,
                    new SPStat
                        {
                            Name = "SP0_LONGEST_CHASE_TIME",
                            Type = SPStatType.Float,
                            Comment = "Duration of the longest cop chase."
                        }
                },
                {
                    0x9E0BF9FC,
                    new SPStat
                        {
                            Name = "SP1_LONGEST_CHASE_TIME",
                            Type = SPStatType.Float,
                            Comment = "Duration of the longest cop chase."
                        }
                },
                {
                    0x160E1402,
                    new SPStat
                        {
                            Name = "SP2_LONGEST_CHASE_TIME",
                            Type = SPStatType.Float,
                            Comment = "Duration of the longest cop chase."
                        }
                },
                {
                    0xA892DB2D,
                    new SPStat
                        {
                            Name = "SP0_TOTAL_CHASE_TIME",
                            Type = SPStatType.U32,
                            Comment = "Total time spent losing cops."
                        }
                },
                {
                    0xF008382E,
                    new SPStat
                        {
                            Name = "SP1_TOTAL_CHASE_TIME",
                            Type = SPStatType.U32,
                            Comment = "Total time spent losing cops."
                        }
                },
                {
                    0x90881D81,
                    new SPStat
                        {
                            Name = "SP2_TOTAL_CHASE_TIME",
                            Type = SPStatType.U32,
                            Comment = "Total time spent losing cops."
                        }
                },
                {
                    0x9D253076,
                    new SPStat
                        {
                            Name = "SP0_LAST_CHASE_TIME",
                            Type = SPStatType.Float,
                            Comment = "Duration of the last cop chase."
                        }
                },
                {
                    0xF59478D6,
                    new SPStat
                        {
                            Name = "SP1_LAST_CHASE_TIME",
                            Type = SPStatType.Float,
                            Comment = "Duration of the last cop chase."
                        }
                },
                {
                    0x16428C29,
                    new SPStat
                        {
                            Name = "SP2_LAST_CHASE_TIME",
                            Type = SPStatType.Float,
                            Comment = "Duration of the last cop chase."
                        }
                },
                {
                    0x027A435A,
                    new SPStat
                        {
                            Name = "SP0_TOTAL_TIME_MAX_STARS",
                            Type = SPStatType.U32,
                            Comment = "Total time under maximun wanted stars"
                        }
                },
                {
                    0xCF4F3496,
                    new SPStat
                        {
                            Name = "SP1_TOTAL_TIME_MAX_STARS",
                            Type = SPStatType.U32,
                            Comment = "Total time under maximun wanted stars"
                        }
                },
                {
                    0x5671799E,
                    new SPStat
                        {
                            Name = "SP2_TOTAL_TIME_MAX_STARS",
                            Type = SPStatType.U32,
                            Comment = "Total time under maximun wanted stars"
                        }
                },
                {
                    0x180042BF,
                    new SPStat
                        {
                            Name = "SP0_STARS_ATTAINED",
                            Type = SPStatType.Int,
                            Comment = "Total Number of wanted stars the player has been awarded"
                        }
                },
                {
                    0xB736B2F7,
                    new SPStat
                        {
                            Name = "SP1_STARS_ATTAINED",
                            Type = SPStatType.Int,
                            Comment = "Total Number of wanted stars the player has been awarded"
                        }
                },
                {
                    0xEF242E3E,
                    new SPStat
                        {
                            Name = "SP2_STARS_ATTAINED",
                            Type = SPStatType.Int,
                            Comment = "Total Number of wanted stars the player has been awarded"
                        }
                },
                {
                    0xA2EE38FC,
                    new SPStat
                        {
                            Name = "SP0_STARS_EVADED",
                            Type = SPStatType.Int,
                            Comment = "Total Number of wanted stars the player has evaded"
                        }
                },
                {
                    0xDF8F0A7F,
                    new SPStat
                        {
                            Name = "SP1_STARS_EVADED",
                            Type = SPStatType.Int,
                            Comment = "Total Number of wanted stars the player has evaded"
                        }
                },
                {
                    0x2B26FC23,
                    new SPStat
                        {
                            Name = "SP2_STARS_EVADED",
                            Type = SPStatType.Int,
                            Comment = "Total Number of wanted stars the player has evaded"
                        }
                },
                {
                    0xCD2D8FF2,
                    new SPStat
                        {
                            Name = "SP0_NO_TIMES_WANTED_LEVEL",
                            Type = SPStatType.Int,
                            Comment = "The number of times character obtains a wanted level"
                        }
                },
                {
                    0x62586DB1,
                    new SPStat
                        {
                            Name = "SP1_NO_TIMES_WANTED_LEVEL",
                            Type = SPStatType.Int,
                            Comment = "The number of times character obtains a wanted level"
                        }
                },
                {
                    0xAAAC55DB,
                    new SPStat
                        {
                            Name = "SP2_NO_TIMES_WANTED_LEVEL",
                            Type = SPStatType.Int,
                            Comment = "The number of times character obtains a wanted level"
                        }
                },
                {
                    0xF09994E5,
                    new SPStat
                        {
                            Name = "SP0_NUM_WEAPON_PICKUPS",
                            Type = SPStatType.Int,
                            Comment = "Number of pickups collected"
                        }
                },
                {
                    0x41202ABE,
                    new SPStat
                        {
                            Name = "SP1_NUM_WEAPON_PICKUPS",
                            Type = SPStatType.Int,
                            Comment = "Number of pickups collected"
                        }
                },
                {
                    0x1F8F134E,
                    new SPStat
                        {
                            Name = "SP2_NUM_WEAPON_PICKUPS",
                            Type = SPStatType.Int,
                            Comment = "Number of pickups collected"
                        }
                },
                {
                    0xDE978781,
                    new SPStat
                        {
                            Name = "SP0_NUM_HEALTH_PICKUPS",
                            Type = SPStatType.Int,
                            Comment = "Number of pickups collected"
                        }
                },
                {
                    0xEF0E04E7,
                    new SPStat
                        {
                            Name = "SP1_NUM_HEALTH_PICKUPS",
                            Type = SPStatType.Int,
                            Comment = "Number of pickups collected"
                        }
                },
                {
                    0x9445F7E1,
                    new SPStat
                        {
                            Name = "SP2_NUM_HEALTH_PICKUPS",
                            Type = SPStatType.Int,
                            Comment = "Number of pickups collected"
                        }
                },
                {
                    0xDB770A3F,
                    new SPStat
                        {
                            Name = "SP0_NUM_ARMOUR_PICKUPS",
                            Type = SPStatType.Int,
                            Comment = "Number of pickups collected"
                        }
                },
                {
                    0xD44C2C7B,
                    new SPStat
                        {
                            Name = "SP1_NUM_ARMOUR_PICKUPS",
                            Type = SPStatType.Int,
                            Comment = "Number of pickups collected"
                        }
                },
                {
                    0x6F58CC0C,
                    new SPStat
                        {
                            Name = "SP2_NUM_ARMOUR_PICKUPS",
                            Type = SPStatType.Int,
                            Comment = "Number of pickups collected"
                        }
                },
                {
                    0x6C993B86,
                    new SPStat
                        {
                            Name = "SP0_NUM_AMMO_PICKUPS",
                            Type = SPStatType.Int,
                            Comment = "Number of pickups collected"
                        }
                },
                {
                    0x58FC6D76,
                    new SPStat
                        {
                            Name = "SP1_NUM_AMMO_PICKUPS",
                            Type = SPStatType.Int,
                            Comment = "Number of pickups collected"
                        }
                },
                {
                    0x48903D70,
                    new SPStat
                        {
                            Name = "SP2_NUM_AMMO_PICKUPS",
                            Type = SPStatType.Int,
                            Comment = "Number of pickups collected"
                        }
                },
                {
                    0x1D52F118,
                    new SPStat
                        {
                            Name = "SP0_TIMES_M_INIT_F_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Michael called Franklin as a battle buddy"
                        }
                },
                {
                    0xAC965628,
                    new SPStat
                        {
                            Name = "SP1_TIMES_M_INIT_F_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Michael called Franklin as a battle buddy"
                        }
                },
                {
                    0x6367B3F5,
                    new SPStat
                        {
                            Name = "SP2_TIMES_M_INIT_F_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Michael called Franklin as a battle buddy"
                        }
                },
                {
                    0xE5E8394C,
                    new SPStat
                        {
                            Name = "SP0_TIMES_M_INIT_T_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Michael called Trevor as a battle buddy"
                        }
                },
                {
                    0xEBABCE6A,
                    new SPStat
                        {
                            Name = "SP1_TIMES_M_INIT_T_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Michael called Trevor as a battle buddy"
                        }
                },
                {
                    0x27F00F0A,
                    new SPStat
                        {
                            Name = "SP2_TIMES_M_INIT_T_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Michael called Trevor as a battle buddy"
                        }
                },
                {
                    0x9CA35252,
                    new SPStat
                        {
                            Name = "SP0_TIMES_F_INIT_M_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Franklin called Michael as a battle buddy"
                        }
                },
                {
                    0x29CCB8F2,
                    new SPStat
                        {
                            Name = "SP1_TIMES_F_INIT_M_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Franklin called Michael as a battle buddy"
                        }
                },
                {
                    0xB1CB69F2,
                    new SPStat
                        {
                            Name = "SP2_TIMES_F_INIT_M_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Franklin called Michael as a battle buddy"
                        }
                },
                {
                    0x2F2F453F,
                    new SPStat
                        {
                            Name = "SP0_TIMES_F_INIT_T_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Franklin called Trevor as a battle buddy"
                        }
                },
                {
                    0xB0214095,
                    new SPStat
                        {
                            Name = "SP1_TIMES_F_INIT_T_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Franklin called Trevor as a battle buddy"
                        }
                },
                {
                    0x650DE028,
                    new SPStat
                        {
                            Name = "SP2_TIMES_F_INIT_T_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Franklin called Trevor as a battle buddy"
                        }
                },
                {
                    0x284127A2,
                    new SPStat
                        {
                            Name = "SP0_TIMES_T_INIT_M_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Trevor called Michael as a battle buddy"
                        }
                },
                {
                    0xFDEC465C,
                    new SPStat
                        {
                            Name = "SP1_TIMES_T_INIT_M_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Trevor called Michael as a battle buddy"
                        }
                },
                {
                    0xB9B3F306,
                    new SPStat
                        {
                            Name = "SP2_TIMES_T_INIT_M_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Trevor called Michael as a battle buddy"
                        }
                },
                {
                    0xA622A4BB,
                    new SPStat
                        {
                            Name = "SP0_TIMES_T_INIT_F_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Trevor called Franklin as a battle buddy"
                        }
                },
                {
                    0x603FF8C4,
                    new SPStat
                        {
                            Name = "SP1_TIMES_T_INIT_F_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Trevor called Franklin as a battle buddy"
                        }
                },
                {
                    0x4E15985C,
                    new SPStat
                        {
                            Name = "SP2_TIMES_T_INIT_F_BBUDDY",
                            Type = SPStatType.Int,
                            Comment = "The number of times Trevor called Franklin as a battle buddy"
                        }
                },
                {
                    0xDCDD4CA8,
                    new SPStat
                        {
                            Name = "SP0_SPECIAL_ABILITY",
                            Type = SPStatType.Int,
                            Comment = "Remaining Special Ability to Unlock"
                        }
                },
                {
                    0x15151676,
                    new SPStat
                        {
                            Name = "SP1_SPECIAL_ABILITY",
                            Type = SPStatType.Int,
                            Comment = "Remaining Special Ability to Unlock"
                        }
                },
                {
                    0x078FE7A3,
                    new SPStat
                        {
                            Name = "SP2_SPECIAL_ABILITY",
                            Type = SPStatType.Int,
                            Comment = "Remaining Special Ability to Unlock"
                        }
                },
                {
                    0x4ECD9F81,
                    new SPStat
                        {
                            Name = "SP0_SPECIAL_ABILITY_UNLOCKED",
                            Type = SPStatType.Int,
                            Comment = "Current Special Ability percentage (0-100)"
                        }
                },
                {
                    0x51CCFBA3,
                    new SPStat
                        {
                            Name = "SP1_SPECIAL_ABILITY_UNLOCKED",
                            Type = SPStatType.Int,
                            Comment = "Current Special Ability percentage (0-100)"
                        }
                },
                {
                    0x05B06442,
                    new SPStat
                        {
                            Name = "SP2_SPECIAL_ABILITY_UNLOCKED",
                            Type = SPStatType.Int,
                            Comment = "Current Special Ability percentage (0-100)"
                        }
                },
                {
                    0xC8A3A687,
                    new SPStat
                        {
                            Name = "SP0_PLANE_LANDINGS",
                            Type = SPStatType.Int,
                            Comment = "Number of good plane landings"
                        }
                },
                {
                    0xC4DF932B,
                    new SPStat
                        {
                            Name = "SP1_PLANE_LANDINGS",
                            Type = SPStatType.Int,
                            Comment = "Number of good plane landings"
                        }
                },
                {
                    0xA6FA09E0,
                    new SPStat
                        {
                            Name = "SP2_PLANE_LANDINGS",
                            Type = SPStatType.Int,
                            Comment = "Number of good plane landings"
                        }
                },
                {
                    0x2EB0D704,
                    new SPStat
                        {
                            Name = "SP0_SPECIAL_ABILITY_ACTIVE_NUM",
                            Type = SPStatType.Int,
                            Comment = "Number times character has used special ability."
                        }
                },
                {
                    0xB4212752,
                    new SPStat
                        {
                            Name = "SP1_SPECIAL_ABILITY_ACTIVE_NUM",
                            Type = SPStatType.Int,
                            Comment = "Number times character has used special ability."
                        }
                },
                {
                    0x60BA937F,
                    new SPStat
                        {
                            Name = "SP2_SPECIAL_ABILITY_ACTIVE_NUM",
                            Type = SPStatType.Int,
                            Comment = "Number times character has used special ability."
                        }
                },
                {
                    0x43203ABB,
                    new SPStat
                        {
                            Name = "SP0_SPECIAL_ABILITY_ACTIVE_TIME",
                            Type = SPStatType.U32,
                            Comment = "Total time special ability has been active."
                        }
                },
                {
                    0x1A709BFE,
                    new SPStat
                        {
                            Name = "SP1_SPECIAL_ABILITY_ACTIVE_TIME",
                            Type = SPStatType.U32,
                            Comment = "Total time special ability has been active."
                        }
                },
                {
                    0x7F801422,
                    new SPStat
                        {
                            Name = "SP2_SPECIAL_ABILITY_ACTIVE_TIME",
                            Type = SPStatType.U32,
                            Comment = "Total time special ability has been active."
                        }
                },
                {0x22C8AAA2, new SPStat {Name = "SP0_STAMINA", Type = SPStatType.Int, Comment = "Stamina"}},
                {0x255EFFB5, new SPStat {Name = "SP1_STAMINA", Type = SPStatType.Int, Comment = "Stamina"}},
                {0x7D8246AE, new SPStat {Name = "SP2_STAMINA", Type = SPStatType.Int, Comment = "Stamina"}},
                {0x906B2799, new SPStat {Name = "SP0_STRENGTH", Type = SPStatType.Int, Comment = "Strength"}},
                {0xB82874E3, new SPStat {Name = "SP1_STRENGTH", Type = SPStatType.Int, Comment = "Strength"}},
                {0x4F19E159, new SPStat {Name = "SP2_STRENGTH", Type = SPStatType.Int, Comment = "Strength"}},
                {
                    0x73968EBD,
                    new SPStat {Name = "SP0_LUNG_CAPACITY", Type = SPStatType.Int, Comment = "Lung capacity modifier"}
                },
                {
                    0x6C3BBB1A,
                    new SPStat {Name = "SP1_LUNG_CAPACITY", Type = SPStatType.Int, Comment = "Lung capacity modifier"}
                },
                {
                    0x7E9487B3,
                    new SPStat {Name = "SP2_LUNG_CAPACITY", Type = SPStatType.Int, Comment = "Lung capacity modifier"}
                },
                {0x11B47270, new SPStat {Name = "SP0_WHEELIE_ABILITY", Type = SPStatType.Int, Comment = "Bike skill"}},
                {0x7DD80AC8, new SPStat {Name = "SP1_WHEELIE_ABILITY", Type = SPStatType.Int, Comment = "Bike skill"}},
                {0x6BEF592F, new SPStat {Name = "SP2_WHEELIE_ABILITY", Type = SPStatType.Int, Comment = "Bike skill"}},
                {
                    0x78ABE4E6,
                    new SPStat {Name = "SP0_FLYING_ABILITY", Type = SPStatType.Int, Comment = "Flying modifier"}
                },
                {
                    0xE98BEE3D,
                    new SPStat {Name = "SP1_FLYING_ABILITY", Type = SPStatType.Int, Comment = "Flying modifier"}
                },
                {
                    0x77CF9710,
                    new SPStat {Name = "SP2_FLYING_ABILITY", Type = SPStatType.Int, Comment = "Flying modifier"}
                },
                {
                    0xB4892709,
                    new SPStat {Name = "SP0_SHOOTING_ABILITY", Type = SPStatType.Int, Comment = "Shooting modifier"}
                },
                {
                    0xCB261497,
                    new SPStat {Name = "SP1_SHOOTING_ABILITY", Type = SPStatType.Int, Comment = "Shooting modifier"}
                },
                {
                    0x2A3A74EA,
                    new SPStat {Name = "SP2_SHOOTING_ABILITY", Type = SPStatType.Int, Comment = "Shooting modifier"}
                },
                {
                    0x2268B791,
                    new SPStat {Name = "SP0_STEALTH_ABILITY", Type = SPStatType.Int, Comment = "Stealth modifier"}
                },
                {
                    0xE76D0C23,
                    new SPStat {Name = "SP1_STEALTH_ABILITY", Type = SPStatType.Int, Comment = "Stealth modifier"}
                },
                {
                    0xD03B7EEB,
                    new SPStat {Name = "SP2_STEALTH_ABILITY", Type = SPStatType.Int, Comment = "Stealth modifier"}
                },
                {
                    0x191F2FFD,
                    new SPStat {Name = "SP0_STAMINA_MAXED", Type = SPStatType.Int, Comment = "Time to max out stat"}
                },
                {
                    0x2AF7EAB1,
                    new SPStat {Name = "SP1_STAMINA_MAXED", Type = SPStatType.Int, Comment = "Time to max out stat"}
                },
                {
                    0x80CFD59F,
                    new SPStat {Name = "SP2_STAMINA_MAXED", Type = SPStatType.Int, Comment = "Time to max out stat"}
                },
                {
                    0x82B97F9F,
                    new SPStat {Name = "SP0_STRENGTH_MAXED", Type = SPStatType.Int, Comment = "Time to max out stat"}
                },
                {
                    0x4CEB919A,
                    new SPStat {Name = "SP1_STRENGTH_MAXED", Type = SPStatType.Int, Comment = "Time to max out stat"}
                },
                {
                    0xCDA0445F,
                    new SPStat {Name = "SP2_STRENGTH_MAXED", Type = SPStatType.Int, Comment = "Time to max out stat"}
                },
                {
                    0xC6741E98,
                    new SPStat
                        {
                            Name = "SP0_LUNG_CAPACITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0x15D7B73C,
                    new SPStat
                        {
                            Name = "SP1_LUNG_CAPACITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0xED0F848F,
                    new SPStat
                        {
                            Name = "SP2_LUNG_CAPACITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0xE4754628,
                    new SPStat
                        {
                            Name = "SP0_WHEELIE_ABILITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0x6191A406,
                    new SPStat
                        {
                            Name = "SP1_WHEELIE_ABILITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0x2B36A001,
                    new SPStat
                        {
                            Name = "SP2_WHEELIE_ABILITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0x3DE41CED,
                    new SPStat
                        {
                            Name = "SP0_FLYING_ABILITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0xF1ED0502,
                    new SPStat
                        {
                            Name = "SP1_FLYING_ABILITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0x72EB9B6D,
                    new SPStat
                        {
                            Name = "SP2_FLYING_ABILITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0x7440D437,
                    new SPStat
                        {
                            Name = "SP0_SHOOTING_ABILITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0x0173160D,
                    new SPStat
                        {
                            Name = "SP1_SHOOTING_ABILITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0x89886D83,
                    new SPStat
                        {
                            Name = "SP2_SHOOTING_ABILITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0x9E46DE07,
                    new SPStat
                        {
                            Name = "SP0_STEALTH_ABILITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0x62A8A3C2,
                    new SPStat
                        {
                            Name = "SP1_STEALTH_ABILITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0x0976A873,
                    new SPStat
                        {
                            Name = "SP2_STEALTH_ABILITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0xB2B4CDCF,
                    new SPStat
                        {
                            Name = "SP0_SPECIAL_ABILITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0x282DDB00,
                    new SPStat
                        {
                            Name = "SP1_SPECIAL_ABILITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0xBB50B3ED,
                    new SPStat
                        {
                            Name = "SP2_SPECIAL_ABILITY_MAXED",
                            Type = SPStatType.Int,
                            Comment = "Time to max out stat"
                        }
                },
                {
                    0x22641988,
                    new SPStat {Name = "SP0_WEAP_UNLOCK_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xCE8B34FD,
                    new SPStat {Name = "SP1_WEAP_UNLOCK_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x7AFABEF9,
                    new SPStat {Name = "SP2_WEAP_UNLOCK_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x3451BD63,
                    new SPStat {Name = "SP0_WEAP_UNLOCK_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xBC8C9100,
                    new SPStat {Name = "SP1_WEAP_UNLOCK_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x7E0EC521,
                    new SPStat {Name = "SP2_WEAP_UNLOCK_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x03B5F973,
                    new SPStat {Name = "SP0_WEAP_ADDON_UNLOCK_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x4622CB9A,
                    new SPStat {Name = "SP1_WEAP_ADDON_UNLOCK_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x392224C0,
                    new SPStat {Name = "SP2_WEAP_ADDON_UNLOCK_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x5F3FB081,
                    new SPStat {Name = "SP0_WEAP_ADDON_UNLOCK_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x738C266C,
                    new SPStat {Name = "SP1_WEAP_ADDON_UNLOCK_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x2700807D,
                    new SPStat {Name = "SP2_WEAP_ADDON_UNLOCK_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x54859B0D,
                    new SPStat {Name = "SP0_WEAP_ADDON_UNLOCK_2", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x61CE82F1,
                    new SPStat {Name = "SP1_WEAP_ADDON_UNLOCK_2", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x5AFFE87B,
                    new SPStat {Name = "SP2_WEAP_ADDON_UNLOCK_2", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x83DA79B6,
                    new SPStat {Name = "SP0_WEAP_ADDON_UNLOCK_3", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x6F001D94,
                    new SPStat {Name = "SP1_WEAP_ADDON_UNLOCK_3", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x51B4D5E5,
                    new SPStat {Name = "SP2_WEAP_ADDON_UNLOCK_3", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {0xDAF98259, new SPStat {Name = "SP0_WEAP_VIEW_0", Type = SPStatType.Int, Comment = "Shop item bitset"}},
                {0x9B7F30DA, new SPStat {Name = "SP1_WEAP_VIEW_0", Type = SPStatType.Int, Comment = "Shop item bitset"}},
                {0xB53AE772, new SPStat {Name = "SP2_WEAP_VIEW_0", Type = SPStatType.Int, Comment = "Shop item bitset"}},
                {0x0DB267CA, new SPStat {Name = "SP0_WEAP_VIEW_1", Type = SPStatType.Int, Comment = "Shop item bitset"}},
                {0x8D7694C9, new SPStat {Name = "SP1_WEAP_VIEW_1", Type = SPStatType.Int, Comment = "Shop item bitset"}},
                {0x836D03D7, new SPStat {Name = "SP2_WEAP_VIEW_1", Type = SPStatType.Int, Comment = "Shop item bitset"}},
                {
                    0x0E35E955,
                    new SPStat {Name = "SP0_WEAP_ADDON_VIEW_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xF4B3E048,
                    new SPStat {Name = "SP1_WEAP_ADDON_VIEW_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xF206E229,
                    new SPStat {Name = "SP2_WEAP_ADDON_VIEW_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x00464D76,
                    new SPStat {Name = "SP0_WEAP_ADDON_VIEW_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x870E84FF,
                    new SPStat {Name = "SP1_WEAP_ADDON_VIEW_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xE44C46B4,
                    new SPStat {Name = "SP2_WEAP_ADDON_VIEW_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x30552D93,
                    new SPStat {Name = "SP0_WEAP_ADDON_VIEW_2", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x6EAED43C,
                    new SPStat {Name = "SP1_WEAP_ADDON_VIEW_2", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xD38BA533,
                    new SPStat {Name = "SP2_WEAP_ADDON_VIEW_2", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x52A57233,
                    new SPStat {Name = "SP0_WEAP_ADDON_VIEW_3", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x8287FBEE,
                    new SPStat {Name = "SP1_WEAP_ADDON_VIEW_3", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xC519884F,
                    new SPStat {Name = "SP2_WEAP_ADDON_VIEW_3", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xB6EFF39A,
                    new SPStat {Name = "SP0_WEAP_TINT_VIEW_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x74148C1B,
                    new SPStat {Name = "SP1_WEAP_TINT_VIEW_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x6636CCB6,
                    new SPStat {Name = "SP2_WEAP_TINT_VIEW_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x87BA152F,
                    new SPStat {Name = "SP0_WEAP_TINT_VIEW_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x878F3310,
                    new SPStat {Name = "SP1_WEAP_TINT_VIEW_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x516CA322,
                    new SPStat {Name = "SP2_WEAP_TINT_VIEW_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x92962AE7,
                    new SPStat {Name = "SP0_WEAP_TINT_VIEW_2", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x59C95785,
                    new SPStat {Name = "SP1_WEAP_TINT_VIEW_2", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x8A6B951F,
                    new SPStat {Name = "SP2_WEAP_TINT_VIEW_2", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x1210A9DE,
                    new SPStat {Name = "SP0_WEAP_TINT_VIEW_3", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x6B247A3B,
                    new SPStat {Name = "SP1_WEAP_TINT_VIEW_3", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x74086859,
                    new SPStat {Name = "SP2_WEAP_TINT_VIEW_3", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x63B6CD29,
                    new SPStat {Name = "SP0_WEAP_TINT_VIEW_4", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xE9E177B3,
                    new SPStat {Name = "SP1_WEAP_TINT_VIEW_4", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xA930D2A9,
                    new SPStat {Name = "SP2_WEAP_TINT_VIEW_4", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xBBE7BEA0, new SPStat {Name = "SP0_WEAP_PURCH_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xF875DEDC, new SPStat {Name = "SP1_WEAP_PURCH_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x0A967697, new SPStat {Name = "SP2_WEAP_PURCH_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xBD0AC0E6, new SPStat {Name = "SP0_WEAP_PURCH_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x0B5B048E, new SPStat {Name = "SP1_WEAP_PURCH_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xE4562A17, new SPStat {Name = "SP2_WEAP_PURCH_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x407407AA,
                    new SPStat {Name = "SP0_WEAP_ADDON_PURCH_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xD3EEBB4E,
                    new SPStat {Name = "SP1_WEAP_ADDON_PURCH_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x06AAEF0A,
                    new SPStat {Name = "SP2_WEAP_ADDON_PURCH_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xAE46634D,
                    new SPStat {Name = "SP0_WEAP_ADDON_PURCH_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xB9588622,
                    new SPStat {Name = "SP1_WEAP_ADDON_PURCH_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xF4614A77,
                    new SPStat {Name = "SP2_WEAP_ADDON_PURCH_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xD25EAB9D,
                    new SPStat {Name = "SP0_WEAP_ADDON_PURCH_2", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x3D718E5A,
                    new SPStat {Name = "SP1_WEAP_ADDON_PURCH_2", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xE96F3493,
                    new SPStat {Name = "SP2_WEAP_ADDON_PURCH_2", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xBF938607,
                    new SPStat {Name = "SP0_WEAP_ADDON_PURCH_3", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x6B5D6A31,
                    new SPStat {Name = "SP1_WEAP_ADDON_PURCH_3", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xDAAC970E,
                    new SPStat {Name = "SP2_WEAP_ADDON_PURCH_3", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x87AF642A,
                    new SPStat {Name = "SP0_WEAP_TINT_PURCH_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xB19A20D6,
                    new SPStat {Name = "SP1_WEAP_TINT_PURCH_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x0D87A875,
                    new SPStat {Name = "SP2_WEAP_TINT_PURCH_0", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x4EE77297,
                    new SPStat {Name = "SP0_WEAP_TINT_PURCH_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x84614665,
                    new SPStat {Name = "SP1_WEAP_TINT_PURCH_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x8D73284E,
                    new SPStat {Name = "SP2_WEAP_TINT_PURCH_1", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x2D4E2F65,
                    new SPStat {Name = "SP0_WEAP_TINT_PURCH_2", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x6F2A1BF7,
                    new SPStat {Name = "SP1_WEAP_TINT_PURCH_2", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x734BF3FC,
                    new SPStat {Name = "SP2_WEAP_TINT_PURCH_2", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xF079B5BD,
                    new SPStat {Name = "SP0_WEAP_TINT_PURCH_3", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xE6FE8B9E,
                    new SPStat {Name = "SP1_WEAP_TINT_PURCH_3", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x41EC913E,
                    new SPStat {Name = "SP2_WEAP_TINT_PURCH_3", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x40B7D638,
                    new SPStat {Name = "SP0_WEAP_TINT_PURCH_4", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0xF8C12F23,
                    new SPStat {Name = "SP1_WEAP_TINT_PURCH_4", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {
                    0x581EBDA2,
                    new SPStat {Name = "SP2_WEAP_TINT_PURCH_4", Type = SPStatType.Int, Comment = "Shop item bitset"}
                },
                {0x0324C31D, new SPStat {Name = "SP0_TOTAL_CASH", Type = SPStatType.Int, Comment = "Cash - Michael"}},
                {0x44BD6982, new SPStat {Name = "SP1_TOTAL_CASH", Type = SPStatType.Int, Comment = "Cash - Michael"}},
                {0x8D75047D, new SPStat {Name = "SP2_TOTAL_CASH", Type = SPStatType.Int, Comment = "Cash - Michael"}},
                {
                    0x185B8F46,
                    new SPStat
                        {
                            Name = "SP0_MONEY_SPENT_IN_CLOTHES",
                            Type = SPStatType.Int,
                            Comment = "Money spent in buying clothes."
                        }
                },
                {
                    0xE6AE13D3,
                    new SPStat
                        {
                            Name = "SP1_MONEY_SPENT_IN_CLOTHES",
                            Type = SPStatType.Int,
                            Comment = "Money spent in buying clothes."
                        }
                },
                {
                    0xDEBB7D4A,
                    new SPStat
                        {
                            Name = "SP2_MONEY_SPENT_IN_CLOTHES",
                            Type = SPStatType.Int,
                            Comment = "Money spent in buying clothes."
                        }
                },
                {
                    0x01A5C0A6,
                    new SPStat
                        {
                            Name = "SP0_MONEY_SPENT_IN_STRIP_CLUBS",
                            Type = SPStatType.Int,
                            Comment = "Money spent in strip clubs."
                        }
                },
                {
                    0x192B26BC,
                    new SPStat
                        {
                            Name = "SP1_MONEY_SPENT_IN_STRIP_CLUBS",
                            Type = SPStatType.Int,
                            Comment = "Money spent in strip clubs."
                        }
                },
                {
                    0x12D8DFD0,
                    new SPStat
                        {
                            Name = "SP2_MONEY_SPENT_IN_STRIP_CLUBS",
                            Type = SPStatType.Int,
                            Comment = "Money spent in strip clubs."
                        }
                },
                {
                    0x0D156803,
                    new SPStat
                        {
                            Name = "SP0_MONEY_SPENT_ON_TAXIS",
                            Type = SPStatType.Int,
                            Comment = "Money spent in taxis."
                        }
                },
                {
                    0x6F8CE97A,
                    new SPStat
                        {
                            Name = "SP1_MONEY_SPENT_ON_TAXIS",
                            Type = SPStatType.Int,
                            Comment = "Money spent in taxis."
                        }
                },
                {
                    0x87561C19,
                    new SPStat
                        {
                            Name = "SP2_MONEY_SPENT_ON_TAXIS",
                            Type = SPStatType.Int,
                            Comment = "Money spent in taxis."
                        }
                },
                {
                    0x80AA2712,
                    new SPStat
                        {
                            Name = "SP0_MONEY_SPENT_IN_COP_BRIBES",
                            Type = SPStatType.Int,
                            Comment = "Money spent in COP bribes."
                        }
                },
                {
                    0x6E7EDEE4,
                    new SPStat
                        {
                            Name = "SP1_MONEY_SPENT_IN_COP_BRIBES",
                            Type = SPStatType.Int,
                            Comment = "Money spent in COP bribes."
                        }
                },
                {
                    0x44407004,
                    new SPStat
                        {
                            Name = "SP2_MONEY_SPENT_IN_COP_BRIBES",
                            Type = SPStatType.Int,
                            Comment = "Money spent in COP bribes."
                        }
                },
                {
                    0x9B137288,
                    new SPStat
                        {
                            Name = "SP0_MONEY_SPENT_ON_HEALTHCARE",
                            Type = SPStatType.Int,
                            Comment = "Money spent in Healthcare."
                        }
                },
                {
                    0xA42D2A3D,
                    new SPStat
                        {
                            Name = "SP1_MONEY_SPENT_ON_HEALTHCARE",
                            Type = SPStatType.Int,
                            Comment = "Money spent in Healthcare."
                        }
                },
                {
                    0x260D4F2E,
                    new SPStat
                        {
                            Name = "SP2_MONEY_SPENT_ON_HEALTHCARE",
                            Type = SPStatType.Int,
                            Comment = "Money spent in Healthcare."
                        }
                },
                {
                    0xE56D46F6,
                    new SPStat
                        {
                            Name = "SP0_MONEY_SPENT_IN_BUYING_GUNS",
                            Type = SPStatType.Int,
                            Comment = "Money spent in Buying Guns."
                        }
                },
                {
                    0xAD57C3A1,
                    new SPStat
                        {
                            Name = "SP1_MONEY_SPENT_IN_BUYING_GUNS",
                            Type = SPStatType.Int,
                            Comment = "Money spent in Buying Guns."
                        }
                },
                {
                    0xD49E38CD,
                    new SPStat
                        {
                            Name = "SP2_MONEY_SPENT_IN_BUYING_GUNS",
                            Type = SPStatType.Int,
                            Comment = "Money spent in Buying Guns."
                        }
                },
                {
                    0x0841E7EF,
                    new SPStat
                        {
                            Name = "SP0_MONEY_MADE_FROM_MISSIONS",
                            Type = SPStatType.Int,
                            Comment = "Money made from missions."
                        }
                },
                {
                    0x29337BB6,
                    new SPStat
                        {
                            Name = "SP1_MONEY_MADE_FROM_MISSIONS",
                            Type = SPStatType.Int,
                            Comment = "Money made from missions."
                        }
                },
                {
                    0xBC68CF93,
                    new SPStat
                        {
                            Name = "SP2_MONEY_MADE_FROM_MISSIONS",
                            Type = SPStatType.Int,
                            Comment = "Money made from missions."
                        }
                },
                {
                    0x690BE17A,
                    new SPStat
                        {
                            Name = "SP0_MONEY_MADE_FROM_RANDOM_PEDS",
                            Type = SPStatType.Int,
                            Comment = "Money made from random peds."
                        }
                },
                {
                    0xDF8F768A,
                    new SPStat
                        {
                            Name = "SP1_MONEY_MADE_FROM_RANDOM_PEDS",
                            Type = SPStatType.Int,
                            Comment = "Money made from random peds."
                        }
                },
                {
                    0xBB95B3E0,
                    new SPStat
                        {
                            Name = "SP2_MONEY_MADE_FROM_RANDOM_PEDS",
                            Type = SPStatType.Int,
                            Comment = "Money made from random peds."
                        }
                },
                {
                    0x401E72D4,
                    new SPStat
                        {
                            Name = "SP0_MONEY_PICKED_UP_ON_STREET",
                            Type = SPStatType.Int,
                            Comment = "Money picked up in the street."
                        }
                },
                {
                    0xF6989010,
                    new SPStat
                        {
                            Name = "SP1_MONEY_PICKED_UP_ON_STREET",
                            Type = SPStatType.Int,
                            Comment = "Money picked up in the street."
                        }
                },
                {
                    0x9E251FB6,
                    new SPStat
                        {
                            Name = "SP2_MONEY_PICKED_UP_ON_STREET",
                            Type = SPStatType.Int,
                            Comment = "Money picked up in the street."
                        }
                },
                {
                    0x0334D043,
                    new SPStat
                        {
                            Name = "SP0_NUMBER_NEAR_MISS",
                            Type = SPStatType.Int,
                            Comment = "Number of vehicles near misses."
                        }
                },
                {
                    0xE883A61F,
                    new SPStat
                        {
                            Name = "SP1_NUMBER_NEAR_MISS",
                            Type = SPStatType.Int,
                            Comment = "Number of vehicles near misses."
                        }
                },
                {
                    0x3B9AE2C6,
                    new SPStat
                        {
                            Name = "SP2_NUMBER_NEAR_MISS",
                            Type = SPStatType.Int,
                            Comment = "Number of vehicles near misses."
                        }
                },
                {
                    0x30457844,
                    new SPStat
                        {
                            Name = "SP0_BAILED_FROM_VEHICLE",
                            Type = SPStatType.Int,
                            Comment = "Number of times jumped out of a moving vehicle."
                        }
                },
                {
                    0x03A85368,
                    new SPStat
                        {
                            Name = "SP1_BAILED_FROM_VEHICLE",
                            Type = SPStatType.Int,
                            Comment = "Number of times jumped out of a moving vehicle."
                        }
                },
                {
                    0x217BB815,
                    new SPStat
                        {
                            Name = "SP2_BAILED_FROM_VEHICLE",
                            Type = SPStatType.Int,
                            Comment = "Number of times jumped out of a moving vehicle."
                        }
                },
                {
                    0x9B37D2FF,
                    new SPStat
                        {
                            Name = "SP0_THROWNTHROUGH_WINDSCREEN",
                            Type = SPStatType.Int,
                            Comment = "Number of times thrown through a windscreen"
                        }
                },
                {
                    0x4612FDFA,
                    new SPStat
                        {
                            Name = "SP1_THROWNTHROUGH_WINDSCREEN",
                            Type = SPStatType.Int,
                            Comment = "Number of times thrown through a windscreen"
                        }
                },
                {
                    0x6D865BCE,
                    new SPStat
                        {
                            Name = "SP2_THROWNTHROUGH_WINDSCREEN",
                            Type = SPStatType.Int,
                            Comment = "Number of times thrown through a windscreen"
                        }
                },
                {
                    0xB209F9B2,
                    new SPStat
                        {
                            Name = "SP0_TOTAL_DAMAGE_CARS",
                            Type = SPStatType.Float,
                            Comment = "Total damage done in Cars"
                        }
                },
                {
                    0x4A79F9B2,
                    new SPStat
                        {
                            Name = "SP1_TOTAL_DAMAGE_CARS",
                            Type = SPStatType.Float,
                            Comment = "Total damage done in Cars"
                        }
                },
                {
                    0xE9A23CFB,
                    new SPStat
                        {
                            Name = "SP2_TOTAL_DAMAGE_CARS",
                            Type = SPStatType.Float,
                            Comment = "Total damage done in Cars"
                        }
                },
                {
                    0xC2C80F72,
                    new SPStat
                        {
                            Name = "SP0_TOTAL_DAMAGE_BIKES",
                            Type = SPStatType.Float,
                            Comment = "Total damage done in Bikes"
                        }
                },
                {
                    0x5436A593,
                    new SPStat
                        {
                            Name = "SP1_TOTAL_DAMAGE_BIKES",
                            Type = SPStatType.Float,
                            Comment = "Total damage done in Bikes"
                        }
                },
                {
                    0xA4AB7F0F,
                    new SPStat
                        {
                            Name = "SP2_TOTAL_DAMAGE_BIKES",
                            Type = SPStatType.Float,
                            Comment = "Total damage done in Bikes"
                        }
                },
                {
                    0xBD538575,
                    new SPStat
                        {
                            Name = "SP0_TOTAL_DAMAGE_QUADBIKES",
                            Type = SPStatType.Float,
                            Comment = "Total damage done in Quadbikes"
                        }
                },
                {
                    0x1ABD4E80,
                    new SPStat
                        {
                            Name = "SP1_TOTAL_DAMAGE_QUADBIKES",
                            Type = SPStatType.Float,
                            Comment = "Total damage done in Quadbikes"
                        }
                },
                {
                    0xAEB8EA46,
                    new SPStat
                        {
                            Name = "SP2_TOTAL_DAMAGE_QUADBIKES",
                            Type = SPStatType.Float,
                            Comment = "Total damage done in Quadbikes"
                        }
                },
                {
                    0x31C6B070,
                    new SPStat
                        {
                            Name = "SP0_NUMBER_CRASHES_CARS",
                            Type = SPStatType.Int,
                            Comment = "Number of crashes done in Cars"
                        }
                },
                {
                    0x89534864,
                    new SPStat
                        {
                            Name = "SP1_NUMBER_CRASHES_CARS",
                            Type = SPStatType.Int,
                            Comment = "Number of crashes done in Cars"
                        }
                },
                {
                    0xDE7005C3,
                    new SPStat
                        {
                            Name = "SP2_NUMBER_CRASHES_CARS",
                            Type = SPStatType.Int,
                            Comment = "Number of crashes done in Cars"
                        }
                },
                {
                    0xC6767633,
                    new SPStat
                        {
                            Name = "SP0_NUMBER_CRASHES_BIKES",
                            Type = SPStatType.Int,
                            Comment = "Number of crashes done in Bikes"
                        }
                },
                {
                    0xDF050A65,
                    new SPStat
                        {
                            Name = "SP1_NUMBER_CRASHES_BIKES",
                            Type = SPStatType.Int,
                            Comment = "Number of crashes done in Bikes"
                        }
                },
                {
                    0x24CD3FCC,
                    new SPStat
                        {
                            Name = "SP2_NUMBER_CRASHES_BIKES",
                            Type = SPStatType.Int,
                            Comment = "Number of crashes done in Bikes"
                        }
                },
                {
                    0x5187345A,
                    new SPStat
                        {
                            Name = "SP0_NUMBER_CRASHES_QUADBIKES",
                            Type = SPStatType.Int,
                            Comment = "Number of crashes done in Quadbikes"
                        }
                },
                {
                    0xF453AC86,
                    new SPStat
                        {
                            Name = "SP1_NUMBER_CRASHES_QUADBIKES",
                            Type = SPStatType.Int,
                            Comment = "Number of crashes done in Quadbikes"
                        }
                },
                {
                    0xCF1922C3,
                    new SPStat
                        {
                            Name = "SP2_NUMBER_CRASHES_QUADBIKES",
                            Type = SPStatType.Int,
                            Comment = "Number of crashes done in Quadbikes"
                        }
                },
                {
                    0xFEA539C6,
                    new SPStat
                        {
                            Name = "SP0_NUMBER_STOLEN_COP_VEHICLE",
                            Type = SPStatType.Int,
                            Comment = "Number of cars stolen"
                        }
                },
                {
                    0x6EEE1362,
                    new SPStat
                        {
                            Name = "SP1_NUMBER_STOLEN_COP_VEHICLE",
                            Type = SPStatType.Int,
                            Comment = "Number of cars stolen"
                        }
                },
                {
                    0x62CE19E3,
                    new SPStat
                        {
                            Name = "SP2_NUMBER_STOLEN_COP_VEHICLE",
                            Type = SPStatType.Int,
                            Comment = "Number of cars stolen"
                        }
                },
                {
                    0x5B7CEFDF,
                    new SPStat
                        {
                            Name = "SP0_NUMBER_STOLEN_CARS",
                            Type = SPStatType.Int,
                            Comment = "Number of cars stolen"
                        }
                },
                {
                    0x2A805C9C,
                    new SPStat
                        {
                            Name = "SP1_NUMBER_STOLEN_CARS",
                            Type = SPStatType.Int,
                            Comment = "Number of cars stolen"
                        }
                },
                {
                    0x2BA86310,
                    new SPStat
                        {
                            Name = "SP2_NUMBER_STOLEN_CARS",
                            Type = SPStatType.Int,
                            Comment = "Number of cars stolen"
                        }
                },
                {
                    0xC5ABAC68,
                    new SPStat
                        {
                            Name = "SP0_NUMBER_STOLEN_BIKES",
                            Type = SPStatType.Int,
                            Comment = "Number of bikes stolen"
                        }
                },
                {
                    0x4433EF71,
                    new SPStat
                        {
                            Name = "SP1_NUMBER_STOLEN_BIKES",
                            Type = SPStatType.Int,
                            Comment = "Number of bikes stolen"
                        }
                },
                {
                    0xED4A32CF,
                    new SPStat
                        {
                            Name = "SP2_NUMBER_STOLEN_BIKES",
                            Type = SPStatType.Int,
                            Comment = "Number of bikes stolen"
                        }
                },
                {
                    0x9A6795A2,
                    new SPStat
                        {
                            Name = "SP0_NUMBER_STOLEN_BOATS",
                            Type = SPStatType.Int,
                            Comment = "Number of boats stolen"
                        }
                },
                {
                    0xDA461788,
                    new SPStat
                        {
                            Name = "SP1_NUMBER_STOLEN_BOATS",
                            Type = SPStatType.Int,
                            Comment = "Number of boats stolen"
                        }
                },
                {
                    0xAA5420DA,
                    new SPStat
                        {
                            Name = "SP2_NUMBER_STOLEN_BOATS",
                            Type = SPStatType.Int,
                            Comment = "Number of boats stolen"
                        }
                },
                {
                    0xAE7B9C29,
                    new SPStat
                        {
                            Name = "SP0_NUMBER_STOLEN_HELIS",
                            Type = SPStatType.Int,
                            Comment = "Number of helis stolen"
                        }
                },
                {
                    0xEAB1ED9A,
                    new SPStat
                        {
                            Name = "SP1_NUMBER_STOLEN_HELIS",
                            Type = SPStatType.Int,
                            Comment = "Number of helis stolen"
                        }
                },
                {
                    0x28EEEB16,
                    new SPStat
                        {
                            Name = "SP2_NUMBER_STOLEN_HELIS",
                            Type = SPStatType.Int,
                            Comment = "Number of helis stolen"
                        }
                },
                {
                    0xDC5FA439,
                    new SPStat
                        {
                            Name = "SP0_NUMBER_STOLEN_PLANES",
                            Type = SPStatType.Int,
                            Comment = "Number of planes stolen"
                        }
                },
                {
                    0x3EBDFA41,
                    new SPStat
                        {
                            Name = "SP1_NUMBER_STOLEN_PLANES",
                            Type = SPStatType.Int,
                            Comment = "Number of planes stolen"
                        }
                },
                {
                    0x5034FD9F,
                    new SPStat
                        {
                            Name = "SP2_NUMBER_STOLEN_PLANES",
                            Type = SPStatType.Int,
                            Comment = "Number of planes stolen"
                        }
                },
                {
                    0xC1FF3F07,
                    new SPStat
                        {
                            Name = "SP0_NUMBER_STOLEN_QUADBIKES",
                            Type = SPStatType.Int,
                            Comment = "Number of quad bikes stolen"
                        }
                },
                {
                    0x2F87B97A,
                    new SPStat
                        {
                            Name = "SP1_NUMBER_STOLEN_QUADBIKES",
                            Type = SPStatType.Int,
                            Comment = "Number of quad bikes stolen"
                        }
                },
                {
                    0xDD678017,
                    new SPStat
                        {
                            Name = "SP2_NUMBER_STOLEN_QUADBIKES",
                            Type = SPStatType.Int,
                            Comment = "Number of quad bikes stolen"
                        }
                },
                {
                    0x4457629E,
                    new SPStat
                        {
                            Name = "SP0_NUMBER_STOLEN_BICYCLES",
                            Type = SPStatType.Int,
                            Comment = "Number of bicycles stolen"
                        }
                },
                {
                    0x26AB2928,
                    new SPStat
                        {
                            Name = "SP1_NUMBER_STOLEN_BICYCLES",
                            Type = SPStatType.Int,
                            Comment = "Number of bicycles stolen"
                        }
                },
                {
                    0xEF848E22,
                    new SPStat
                        {
                            Name = "SP2_NUMBER_STOLEN_BICYCLES",
                            Type = SPStatType.Int,
                            Comment = "Number of bicycles stolen"
                        }
                },
                {
                    0x5887C998,
                    new SPStat
                        {
                            Name = "SP0_TIRES_POPPED_BY_GUNSHOT",
                            Type = SPStatType.Int,
                            Comment = "Number of tires popped by gunshot"
                        }
                },
                {
                    0x24D340BB,
                    new SPStat
                        {
                            Name = "SP1_TIRES_POPPED_BY_GUNSHOT",
                            Type = SPStatType.Int,
                            Comment = "Number of tires popped by gunshot"
                        }
                },
                {
                    0xCA5216B6,
                    new SPStat
                        {
                            Name = "SP2_TIRES_POPPED_BY_GUNSHOT",
                            Type = SPStatType.Int,
                            Comment = "Number of tires popped by gunshot"
                        }
                },
                {
                    0x85F53D9F,
                    new SPStat
                        {
                            Name = "SP0_VEHICLES_DESTROYED_ON_SPREE",
                            Type = SPStatType.Int,
                            Comment = "Number of vehicles destroyed on spree"
                        }
                },
                {
                    0x6A4B4B6F,
                    new SPStat
                        {
                            Name = "SP1_VEHICLES_DESTROYED_ON_SPREE",
                            Type = SPStatType.Int,
                            Comment = "Number of vehicles destroyed on spree"
                        }
                },
                {
                    0x5F31D123,
                    new SPStat
                        {
                            Name = "SP2_VEHICLES_DESTROYED_ON_SPREE",
                            Type = SPStatType.Int,
                            Comment = "Number of vehicles destroyed on spree"
                        }
                },
                {
                    0xD5148501,
                    new SPStat
                        {
                            Name = "SP0_COP_VEHI_DESTROYED_ON_SPREE",
                            Type = SPStatType.Int,
                            Comment = "Number of COP vehicles destroyed on spree"
                        }
                },
                {
                    0x7C372707,
                    new SPStat
                        {
                            Name = "SP1_COP_VEHI_DESTROYED_ON_SPREE",
                            Type = SPStatType.Int,
                            Comment = "Number of COP vehicles destroyed on spree"
                        }
                },
                {
                    0x499723A4,
                    new SPStat
                        {
                            Name = "SP2_COP_VEHI_DESTROYED_ON_SPREE",
                            Type = SPStatType.Int,
                            Comment = "Number of COP vehicles destroyed on spree"
                        }
                },
                {
                    0xCF8F9239,
                    new SPStat
                        {
                            Name = "SP0_TANKS_DESTROYED_ON_SPREE",
                            Type = SPStatType.Int,
                            Comment = "Number of TANK vehicles destroyed on spree"
                        }
                },
                {
                    0xD40B2DC4,
                    new SPStat
                        {
                            Name = "SP1_TANKS_DESTROYED_ON_SPREE",
                            Type = SPStatType.Int,
                            Comment = "Number of TANK vehicles destroyed on spree"
                        }
                },
                {
                    0xBEE54649,
                    new SPStat
                        {
                            Name = "SP2_TANKS_DESTROYED_ON_SPREE",
                            Type = SPStatType.Int,
                            Comment = "Number of TANK vehicles destroyed on spree"
                        }
                },
                {
                    0x70E3B4EF,
                    new SPStat {Name = "SP0_CARS_WRECKED", Type = SPStatType.Int, Comment = "Number of cars trashed"}
                },
                {
                    0xF6D4638E,
                    new SPStat {Name = "SP1_CARS_WRECKED", Type = SPStatType.Int, Comment = "Number of cars trashed"}
                },
                {
                    0xF7C75FDD,
                    new SPStat {Name = "SP2_CARS_WRECKED", Type = SPStatType.Int, Comment = "Number of cars trashed"}
                },
                {
                    0xD18DA00D,
                    new SPStat
                        {
                            Name = "SP0_CARS_COPS_WRECKED",
                            Type = SPStatType.Int,
                            Comment = "Number of cars trashed"
                        }
                },
                {
                    0xAD6B854D,
                    new SPStat
                        {
                            Name = "SP1_CARS_COPS_WRECKED",
                            Type = SPStatType.Int,
                            Comment = "Number of cars trashed"
                        }
                },
                {
                    0x7E48F752,
                    new SPStat
                        {
                            Name = "SP2_CARS_COPS_WRECKED",
                            Type = SPStatType.Int,
                            Comment = "Number of cars trashed"
                        }
                },
                {
                    0x38664C78,
                    new SPStat {Name = "SP0_CARS_EXPLODED", Type = SPStatType.Int, Comment = "Number of cars exploded"}
                },
                {
                    0x7815DE6A,
                    new SPStat {Name = "SP1_CARS_EXPLODED", Type = SPStatType.Int, Comment = "Number of cars exploded"}
                },
                {
                    0x7B137FED,
                    new SPStat {Name = "SP2_CARS_EXPLODED", Type = SPStatType.Int, Comment = "Number of cars exploded"}
                },
                {
                    0xC57F7011,
                    new SPStat
                        {
                            Name = "SP0_CARS_COPS_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of cop cars exploded"
                        }
                },
                {
                    0x13CB11D3,
                    new SPStat
                        {
                            Name = "SP1_CARS_COPS_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of cop cars exploded"
                        }
                },
                {
                    0x785F4702,
                    new SPStat
                        {
                            Name = "SP2_CARS_COPS_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of cop cars exploded"
                        }
                },
                {
                    0xAF9C9E40,
                    new SPStat
                        {
                            Name = "SP0_BIKES_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of bikes exploded"
                        }
                },
                {
                    0x85468DA0,
                    new SPStat
                        {
                            Name = "SP1_BIKES_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of bikes exploded"
                        }
                },
                {
                    0x09DB7862,
                    new SPStat
                        {
                            Name = "SP2_BIKES_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of bikes exploded"
                        }
                },
                {
                    0xDB221DBC,
                    new SPStat
                        {
                            Name = "SP0_BOATS_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of boats exploded"
                        }
                },
                {
                    0x366564B8,
                    new SPStat
                        {
                            Name = "SP1_BOATS_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of boats exploded"
                        }
                },
                {
                    0x69DB2C38,
                    new SPStat
                        {
                            Name = "SP2_BOATS_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of boats exploded"
                        }
                },
                {
                    0x08692A1F,
                    new SPStat
                        {
                            Name = "SP0_HELIS_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of helis exploded"
                        }
                },
                {
                    0x00085058,
                    new SPStat
                        {
                            Name = "SP1_HELIS_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of helis exploded"
                        }
                },
                {
                    0xB2DDD1A3,
                    new SPStat
                        {
                            Name = "SP2_HELIS_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of helis exploded"
                        }
                },
                {
                    0x5899F8D7,
                    new SPStat
                        {
                            Name = "SP0_PLANES_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of planes exploded"
                        }
                },
                {
                    0x95E68DD3,
                    new SPStat
                        {
                            Name = "SP1_PLANES_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of planes exploded"
                        }
                },
                {
                    0x7B8A015C,
                    new SPStat
                        {
                            Name = "SP2_PLANES_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of planes exploded"
                        }
                },
                {
                    0x73C66799,
                    new SPStat
                        {
                            Name = "SP0_QUADBIKE_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of quadbike exploded"
                        }
                },
                {
                    0x6F23C84B,
                    new SPStat
                        {
                            Name = "SP1_QUADBIKE_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of quadbike exploded"
                        }
                },
                {
                    0x1D07796B,
                    new SPStat
                        {
                            Name = "SP2_QUADBIKE_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of quadbike exploded"
                        }
                },
                {
                    0x405D25F6,
                    new SPStat
                        {
                            Name = "SP0_BICYCLE_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of bicycle exploded"
                        }
                },
                {
                    0x28D52704,
                    new SPStat
                        {
                            Name = "SP1_BICYCLE_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of bicycle exploded"
                        }
                },
                {
                    0x9AF8AD43,
                    new SPStat
                        {
                            Name = "SP2_BICYCLE_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of bicycle exploded"
                        }
                },
                {
                    0x08FBB87B,
                    new SPStat
                        {
                            Name = "SP0_SUBMARINE_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of submarine exploded"
                        }
                },
                {
                    0xE954DE98,
                    new SPStat
                        {
                            Name = "SP1_SUBMARINE_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of submarine exploded"
                        }
                },
                {
                    0xCF3AFEE2,
                    new SPStat
                        {
                            Name = "SP2_SUBMARINE_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of submarine exploded"
                        }
                },
                {
                    0xE7D7E9CB,
                    new SPStat
                        {
                            Name = "SP0_TRAIN_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of train exploded"
                        }
                },
                {
                    0x233A4E54,
                    new SPStat
                        {
                            Name = "SP1_TRAIN_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of train exploded"
                        }
                },
                {
                    0xFCFEDA0A,
                    new SPStat
                        {
                            Name = "SP2_TRAIN_EXPLODED",
                            Type = SPStatType.Int,
                            Comment = "Number of train exploded"
                        }
                },
                {
                    0x8863C21F,
                    new SPStat
                        {
                            Name = "SP0_FASTEST_SPEED",
                            Type = SPStatType.Float,
                            Comment = "Fastest speed recorded in a vehicle in meters/h"
                        }
                },
                {
                    0xDBD48449,
                    new SPStat
                        {
                            Name = "SP1_FASTEST_SPEED",
                            Type = SPStatType.Float,
                            Comment = "Fastest speed recorded in a vehicle in meters/h"
                        }
                },
                {
                    0x0B16318C,
                    new SPStat
                        {
                            Name = "SP2_FASTEST_SPEED",
                            Type = SPStatType.Float,
                            Comment = "Fastest speed recorded in a vehicle in meters/h"
                        }
                },
                {
                    0x3756ED23,
                    new SPStat
                        {
                            Name = "SP0_TOP_SPEED_CAR",
                            Type = SPStatType.U32,
                            Comment = "Model Index of the fastest speed car"
                        }
                },
                {
                    0x1E5D27C6,
                    new SPStat
                        {
                            Name = "SP1_TOP_SPEED_CAR",
                            Type = SPStatType.U32,
                            Comment = "Model Index of the fastest speed car"
                        }
                },
                {
                    0x874A328F,
                    new SPStat
                        {
                            Name = "SP2_TOP_SPEED_CAR",
                            Type = SPStatType.U32,
                            Comment = "Model Index of the fastest speed car"
                        }
                },
                {
                    0x9E961691,
                    new SPStat
                        {
                            Name = "SP0_LONGEST_2WHEEL_DIST",
                            Type = SPStatType.Float,
                            Comment = "Longest 2 wheel distance in a vehicle"
                        }
                },
                {
                    0x4691C247,
                    new SPStat
                        {
                            Name = "SP1_LONGEST_2WHEEL_DIST",
                            Type = SPStatType.Float,
                            Comment = "Longest 2 wheel distance in a vehicle"
                        }
                },
                {
                    0xC033C6F2,
                    new SPStat
                        {
                            Name = "SP2_LONGEST_2WHEEL_DIST",
                            Type = SPStatType.Float,
                            Comment = "Longest 2 wheel distance in a vehicle"
                        }
                },
                {
                    0x6880D28A,
                    new SPStat
                        {
                            Name = "SP0_LONGEST_2WHEEL_TIME",
                            Type = SPStatType.U32,
                            Comment = "Longest 2 wheel time in a vehicle"
                        }
                },
                {
                    0x6B06D9C8,
                    new SPStat
                        {
                            Name = "SP1_LONGEST_2WHEEL_TIME",
                            Type = SPStatType.U32,
                            Comment = "Longest 2 wheel time in a vehicle"
                        }
                },
                {
                    0x4196A527,
                    new SPStat
                        {
                            Name = "SP2_LONGEST_2WHEEL_TIME",
                            Type = SPStatType.U32,
                            Comment = "Longest 2 wheel time in a vehicle"
                        }
                },
                {
                    0xBA8B089A,
                    new SPStat
                        {
                            Name = "SP0_LONGEST_STOPPIE_DIST",
                            Type = SPStatType.Float,
                            Comment = "Longest stoppie distance in a bike"
                        }
                },
                {
                    0xACFA5095,
                    new SPStat
                        {
                            Name = "SP1_LONGEST_STOPPIE_DIST",
                            Type = SPStatType.Float,
                            Comment = "Longest stoppie distance in a bike"
                        }
                },
                {
                    0x22013B50,
                    new SPStat
                        {
                            Name = "SP2_LONGEST_STOPPIE_DIST",
                            Type = SPStatType.Float,
                            Comment = "Longest stoppie distance in a bike"
                        }
                },
                {
                    0xD20A5FCB,
                    new SPStat
                        {
                            Name = "SP0_LONGEST_STOPPIE_TIME",
                            Type = SPStatType.U32,
                            Comment = "Longest stoppie time in a bike"
                        }
                },
                {
                    0x017D624B,
                    new SPStat
                        {
                            Name = "SP1_LONGEST_STOPPIE_TIME",
                            Type = SPStatType.U32,
                            Comment = "Longest stoppie time in a bike"
                        }
                },
                {
                    0xF3F27FE8,
                    new SPStat
                        {
                            Name = "SP2_LONGEST_STOPPIE_TIME",
                            Type = SPStatType.U32,
                            Comment = "Longest stoppie time in a bike"
                        }
                },
                {
                    0xD43735C4,
                    new SPStat
                        {
                            Name = "SP0_LONGEST_WHEELIE_DIST",
                            Type = SPStatType.Float,
                            Comment = "Longest wheelie time in a bike"
                        }
                },
                {
                    0x56B52DBF,
                    new SPStat
                        {
                            Name = "SP1_LONGEST_WHEELIE_DIST",
                            Type = SPStatType.Float,
                            Comment = "Longest wheelie time in a bike"
                        }
                },
                {
                    0x42D28D43,
                    new SPStat
                        {
                            Name = "SP2_LONGEST_WHEELIE_DIST",
                            Type = SPStatType.Float,
                            Comment = "Longest wheelie time in a bike"
                        }
                },
                {
                    0x10A8338F,
                    new SPStat
                        {
                            Name = "SP0_LONGEST_WHEELIE_TIME",
                            Type = SPStatType.U32,
                            Comment = "Longest wheelie time in a bike"
                        }
                },
                {
                    0x450AF991,
                    new SPStat
                        {
                            Name = "SP1_LONGEST_WHEELIE_TIME",
                            Type = SPStatType.U32,
                            Comment = "Longest wheelie time in a bike"
                        }
                },
                {
                    0x729F34B7,
                    new SPStat
                        {
                            Name = "SP2_LONGEST_WHEELIE_TIME",
                            Type = SPStatType.U32,
                            Comment = "Longest wheelie time in a bike"
                        }
                },
                {
                    0x385D094F,
                    new SPStat
                        {
                            Name = "SP0_TOTAL_WHEELIE_TIME",
                            Type = SPStatType.U64,
                            Comment = "Total wheelie time in a bike"
                        }
                },
                {
                    0x93111CE3,
                    new SPStat
                        {
                            Name = "SP1_TOTAL_WHEELIE_TIME",
                            Type = SPStatType.U64,
                            Comment = "Total wheelie time in a bike"
                        }
                },
                {
                    0xEB36C273,
                    new SPStat
                        {
                            Name = "SP2_TOTAL_WHEELIE_TIME",
                            Type = SPStatType.U64,
                            Comment = "Total wheelie time in a bike"
                        }
                },
                {
                    0x37893538,
                    new SPStat
                        {
                            Name = "SP0_LONGEST_SURVIVED_FREEFALL",
                            Type = SPStatType.Float,
                            Comment = "Longest Survived free fall in meters."
                        }
                },
                {
                    0xFB059467,
                    new SPStat
                        {
                            Name = "SP1_LONGEST_SURVIVED_FREEFALL",
                            Type = SPStatType.Float,
                            Comment = "Longest Survived free fall in meters."
                        }
                },
                {
                    0x547884B2,
                    new SPStat
                        {
                            Name = "SP2_LONGEST_SURVIVED_FREEFALL",
                            Type = SPStatType.Float,
                            Comment = "Longest Survived free fall in meters."
                        }
                },
                {
                    0x541A7DB5,
                    new SPStat
                        {
                            Name = "SP0_HIGHEST_SKITTLES",
                            Type = SPStatType.Int,
                            Comment = "Number of Peds Run down"
                        }
                },
                {
                    0xD17C6205,
                    new SPStat
                        {
                            Name = "SP1_HIGHEST_SKITTLES",
                            Type = SPStatType.Int,
                            Comment = "Number of Peds Run down"
                        }
                },
                {
                    0x2B47F62A,
                    new SPStat
                        {
                            Name = "SP2_HIGHEST_SKITTLES",
                            Type = SPStatType.Int,
                            Comment = "Number of Peds Run down"
                        }
                },
                {
                    0x2442F65D,
                    new SPStat
                        {
                            Name = "SP0_MOST_FLIPS_IN_ONE_JUMP",
                            Type = SPStatType.Int,
                            Comment = "Number of flips in one vehicle jump"
                        }
                },
                {
                    0x7A4B5BC6,
                    new SPStat
                        {
                            Name = "SP1_MOST_FLIPS_IN_ONE_JUMP",
                            Type = SPStatType.Int,
                            Comment = "Number of flips in one vehicle jump"
                        }
                },
                {
                    0x00C2F44C,
                    new SPStat
                        {
                            Name = "SP2_MOST_FLIPS_IN_ONE_JUMP",
                            Type = SPStatType.Int,
                            Comment = "Number of flips in one vehicle jump"
                        }
                },
                {
                    0x3023A419,
                    new SPStat
                        {
                            Name = "SP0_MOST_SPINS_IN_ONE_JUMP",
                            Type = SPStatType.Int,
                            Comment = "Number of spins in one vehicle jump"
                        }
                },
                {
                    0x61CB76AE,
                    new SPStat
                        {
                            Name = "SP1_MOST_SPINS_IN_ONE_JUMP",
                            Type = SPStatType.Int,
                            Comment = "Number of spins in one vehicle jump"
                        }
                },
                {
                    0x1EF45852,
                    new SPStat
                        {
                            Name = "SP2_MOST_SPINS_IN_ONE_JUMP",
                            Type = SPStatType.Int,
                            Comment = "Number of spins in one vehicle jump"
                        }
                },
                {
                    0x7D324E5C,
                    new SPStat
                        {
                            Name = "SP0_FARTHEST_JUMP_DIST",
                            Type = SPStatType.Float,
                            Comment = "Longest jump distance"
                        }
                },
                {
                    0x52BE93DF,
                    new SPStat
                        {
                            Name = "SP1_FARTHEST_JUMP_DIST",
                            Type = SPStatType.Float,
                            Comment = "Longest jump distance"
                        }
                },
                {
                    0xCE37C24D,
                    new SPStat
                        {
                            Name = "SP2_FARTHEST_JUMP_DIST",
                            Type = SPStatType.Float,
                            Comment = "Longest jump distance"
                        }
                },
                {
                    0x5E305B77,
                    new SPStat
                        {
                            Name = "SP0_HIGHEST_JUMP_REACHED",
                            Type = SPStatType.Float,
                            Comment = "Highest jump distance"
                        }
                },
                {
                    0x183CE878,
                    new SPStat
                        {
                            Name = "SP1_HIGHEST_JUMP_REACHED",
                            Type = SPStatType.Float,
                            Comment = "Highest jump distance"
                        }
                },
                {
                    0x7F73D36C,
                    new SPStat
                        {
                            Name = "SP2_HIGHEST_JUMP_REACHED",
                            Type = SPStatType.Float,
                            Comment = "Highest jump distance"
                        }
                },
                {
                    0xDD533882,
                    new SPStat
                        {
                            Name = "SP0_AIR_LAUNCHES_OVER_5S",
                            Type = SPStatType.Int,
                            Comment = "Number of air launches over 5 seconds"
                        }
                },
                {
                    0xB8502CEC,
                    new SPStat
                        {
                            Name = "SP1_AIR_LAUNCHES_OVER_5S",
                            Type = SPStatType.Int,
                            Comment = "Number of air launches over 5 seconds"
                        }
                },
                {
                    0x500FE2D9,
                    new SPStat
                        {
                            Name = "SP2_AIR_LAUNCHES_OVER_5S",
                            Type = SPStatType.Int,
                            Comment = "Number of air launches over 5 seconds"
                        }
                },
                {
                    0x266DCAB6,
                    new SPStat
                        {
                            Name = "SP0_AIR_LAUNCHES_OVER_5M",
                            Type = SPStatType.Int,
                            Comment = "Number of air launches over 5 meters"
                        }
                },
                {
                    0xE6818952,
                    new SPStat
                        {
                            Name = "SP1_AIR_LAUNCHES_OVER_5M",
                            Type = SPStatType.Int,
                            Comment = "Number of air launches over 5 meters"
                        }
                },
                {
                    0x71D9266B,
                    new SPStat
                        {
                            Name = "SP2_AIR_LAUNCHES_OVER_5M",
                            Type = SPStatType.Int,
                            Comment = "Number of air launches over 5 meters"
                        }
                },
                {
                    0xF82A5FB1,
                    new SPStat
                        {
                            Name = "SP0_AIR_LAUNCHES_OVER_40M",
                            Type = SPStatType.Int,
                            Comment = "Number of air launches over 40 meters"
                        }
                },
                {
                    0x991A098E,
                    new SPStat
                        {
                            Name = "SP1_AIR_LAUNCHES_OVER_40M",
                            Type = SPStatType.Int,
                            Comment = "Number of air launches over 40 meters"
                        }
                },
                {
                    0x9AD589CC,
                    new SPStat
                        {
                            Name = "SP2_AIR_LAUNCHES_OVER_40M",
                            Type = SPStatType.Int,
                            Comment = "Number of air launches over 40 meters"
                        }
                },
                {
                    0xC136EA82,
                    new SPStat
                        {
                            Name = "SP0_NUMBER_OF_AIR_LAUNCHES",
                            Type = SPStatType.Int,
                            Comment = "Number of air launches"
                        }
                },
                {
                    0xC803C007,
                    new SPStat
                        {
                            Name = "SP1_NUMBER_OF_AIR_LAUNCHES",
                            Type = SPStatType.Int,
                            Comment = "Number of air launches"
                        }
                },
                {
                    0xA6E32287,
                    new SPStat
                        {
                            Name = "SP2_NUMBER_OF_AIR_LAUNCHES",
                            Type = SPStatType.Int,
                            Comment = "Number of air launches"
                        }
                },
                {
                    0x64D4FE27,
                    new SPStat {Name = "SP0_KILLS_ARMED", Type = SPStatType.Int, Comment = "Number of Armed kills"}
                },
                {
                    0x645D90CD,
                    new SPStat {Name = "SP1_KILLS_ARMED", Type = SPStatType.Int, Comment = "Number of Armed kills"}
                },
                {
                    0x3C500974,
                    new SPStat {Name = "SP2_KILLS_ARMED", Type = SPStatType.Int, Comment = "Number of Armed kills"}
                },
                {
                    0x1255DF37,
                    new SPStat
                        {
                            Name = "SP0_KILLS_IN_FREE_AIM",
                            Type = SPStatType.Float,
                            Comment = "number of kills in free aim"
                        }
                },
                {
                    0x246C6CD7,
                    new SPStat
                        {
                            Name = "SP1_KILLS_IN_FREE_AIM",
                            Type = SPStatType.Float,
                            Comment = "number of kills in free aim"
                        }
                },
                {
                    0x1441A8CB,
                    new SPStat
                        {
                            Name = "SP2_KILLS_IN_FREE_AIM",
                            Type = SPStatType.Float,
                            Comment = "number of kills in free aim"
                        }
                },
                {0x8887FA85, new SPStat {Name = "SP0_KILLS", Type = SPStatType.Int, Comment = "Number of times killed"}},
                {0xF9892BB2, new SPStat {Name = "SP1_KILLS", Type = SPStatType.Int, Comment = "Number of times killed"}},
                {0xDE864F6F, new SPStat {Name = "SP2_KILLS", Type = SPStatType.Int, Comment = "Number of times killed"}},
                {0xAA007BC5, new SPStat {Name = "SP0_DEATHS", Type = SPStatType.Int, Comment = "Number of times died"}},
                {0x6F3B9ADF, new SPStat {Name = "SP1_DEATHS", Type = SPStatType.Int, Comment = "Number of times died"}},
                {0x6B94F9C1, new SPStat {Name = "SP2_DEATHS", Type = SPStatType.Int, Comment = "Number of times died"}},
                {0xD8146AD1, new SPStat {Name = "SP0_SHOTS", Type = SPStatType.Int, Comment = "Number of times fired"}},
                {0x8EAF083B, new SPStat {Name = "SP1_SHOTS", Type = SPStatType.Int, Comment = "Number of times fired"}},
                {0x9C1A8CA0, new SPStat {Name = "SP2_SHOTS", Type = SPStatType.Int, Comment = "Number of times fired"}},
                {0x82FEB19D, new SPStat {Name = "SP0_HITS", Type = SPStatType.Int, Comment = "Number of times hit"}},
                {0x786BBD76, new SPStat {Name = "SP1_HITS", Type = SPStatType.Int, Comment = "Number of times hit"}},
                {0x09D82DED, new SPStat {Name = "SP2_HITS", Type = SPStatType.Int, Comment = "Number of times hit"}},
                {
                    0x7A728DFF,
                    new SPStat
                        {
                            Name = "SP0_HITS_MISSION",
                            Type = SPStatType.Int,
                            Comment = "Number of times hit during a mission"
                        }
                },
                {
                    0xE3F06A75,
                    new SPStat
                        {
                            Name = "SP1_HITS_MISSION",
                            Type = SPStatType.Int,
                            Comment = "Number of times hit during a mission"
                        }
                },
                {
                    0x41DBDDD5,
                    new SPStat
                        {
                            Name = "SP2_HITS_MISSION",
                            Type = SPStatType.Int,
                            Comment = "Number of times hit during a mission"
                        }
                },
                {
                    0xC45479D2,
                    new SPStat
                        {
                            Name = "SP0_HITS_PEDS_VEHICLES",
                            Type = SPStatType.Int,
                            Comment = "Number of times mp char 0 hit"
                        }
                },
                {
                    0xA08ED8BC,
                    new SPStat
                        {
                            Name = "SP1_HITS_PEDS_VEHICLES",
                            Type = SPStatType.Int,
                            Comment = "Number of times mp char 0 hit"
                        }
                },
                {
                    0xBEFC845D,
                    new SPStat
                        {
                            Name = "SP2_HITS_PEDS_VEHICLES",
                            Type = SPStatType.Int,
                            Comment = "Number of times mp char 0 hit"
                        }
                },
                {
                    0x8948A8BF,
                    new SPStat
                        {
                            Name = "SP0_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times killed by headshot"
                        }
                },
                {
                    0xEC733A8E,
                    new SPStat
                        {
                            Name = "SP1_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times killed by headshot"
                        }
                },
                {
                    0x740BDDC6,
                    new SPStat
                        {
                            Name = "SP2_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times killed by headshot"
                        }
                },
                {
                    0xD7E020F8,
                    new SPStat {Name = "SP0_DB_KILLS", Type = SPStatType.Int, Comment = "Number of times killed"}
                },
                {
                    0xC092EE17,
                    new SPStat {Name = "SP1_DB_KILLS", Type = SPStatType.Int, Comment = "Number of times killed"}
                },
                {
                    0xB25E29D1,
                    new SPStat {Name = "SP2_DB_KILLS", Type = SPStatType.Int, Comment = "Number of times killed"}
                },
                {
                    0x8B906F49,
                    new SPStat
                        {
                            Name = "SP0_DB_SHOTTIME",
                            Type = SPStatType.U32,
                            Comment = "Number of times time in drive by"
                        }
                },
                {
                    0xCA181893,
                    new SPStat
                        {
                            Name = "SP1_DB_SHOTTIME",
                            Type = SPStatType.U32,
                            Comment = "Number of times time in drive by"
                        }
                },
                {
                    0xCDA29937,
                    new SPStat
                        {
                            Name = "SP2_DB_SHOTTIME",
                            Type = SPStatType.U32,
                            Comment = "Number of times time in drive by"
                        }
                },
                {
                    0x458C5F8D,
                    new SPStat {Name = "SP0_DB_SHOTS", Type = SPStatType.Int, Comment = "Number of times fired"}
                },
                {
                    0xA948C41C,
                    new SPStat {Name = "SP1_DB_SHOTS", Type = SPStatType.Int, Comment = "Number of times fired"}
                },
                {
                    0xA2196BBA,
                    new SPStat {Name = "SP2_DB_SHOTS", Type = SPStatType.Int, Comment = "Number of times fired"}
                },
                {0x02F85E34, new SPStat {Name = "SP0_DB_HITS", Type = SPStatType.Int, Comment = "Number of times hit"}},
                {0xB27C5EEA, new SPStat {Name = "SP1_DB_HITS", Type = SPStatType.Int, Comment = "Number of times hit"}},
                {0xDAB488DB, new SPStat {Name = "SP2_DB_HITS", Type = SPStatType.Int, Comment = "Number of times hit"}},
                {
                    0x7E9B2159,
                    new SPStat
                        {
                            Name = "SP0_DB_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times killed by headshot"
                        }
                },
                {
                    0x5CC40B48,
                    new SPStat
                        {
                            Name = "SP1_DB_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times killed by headshot"
                        }
                },
                {
                    0xDCE21065,
                    new SPStat
                        {
                            Name = "SP2_DB_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times killed by headshot"
                        }
                },
                {
                    0x538941ED,
                    new SPStat
                        {
                            Name = "SP0_PASS_DB_SHOTTIME",
                            Type = SPStatType.U32,
                            Comment = "Number of times time in drive by"
                        }
                },
                {
                    0xCFA95B9A,
                    new SPStat
                        {
                            Name = "SP1_PASS_DB_SHOTTIME",
                            Type = SPStatType.U32,
                            Comment = "Number of times time in drive by"
                        }
                },
                {
                    0x53722223,
                    new SPStat
                        {
                            Name = "SP2_PASS_DB_SHOTTIME",
                            Type = SPStatType.U32,
                            Comment = "Number of times time in drive by"
                        }
                },
                {
                    0x847BA4B9,
                    new SPStat {Name = "SP0_PASS_DB_KILLS", Type = SPStatType.Int, Comment = "Number of times killed"}
                },
                {
                    0xA8DB15EE,
                    new SPStat {Name = "SP1_PASS_DB_KILLS", Type = SPStatType.Int, Comment = "Number of times killed"}
                },
                {
                    0xEFB8CC52,
                    new SPStat {Name = "SP2_PASS_DB_KILLS", Type = SPStatType.Int, Comment = "Number of times killed"}
                },
                {
                    0xD5D2C602,
                    new SPStat {Name = "SP0_PASS_DB_SHOTS", Type = SPStatType.Int, Comment = "Number of times fired"}
                },
                {
                    0x3F7C9A7F,
                    new SPStat {Name = "SP1_PASS_DB_SHOTS", Type = SPStatType.Int, Comment = "Number of times fired"}
                },
                {
                    0x966098DD,
                    new SPStat {Name = "SP2_PASS_DB_SHOTS", Type = SPStatType.Int, Comment = "Number of times fired"}
                },
                {
                    0x9A9E2140,
                    new SPStat {Name = "SP0_PASS_DB_HITS", Type = SPStatType.Int, Comment = "Number of times hit"}
                },
                {
                    0x05267390,
                    new SPStat {Name = "SP1_PASS_DB_HITS", Type = SPStatType.Int, Comment = "Number of times hit"}
                },
                {
                    0xD825C0C1,
                    new SPStat {Name = "SP2_PASS_DB_HITS", Type = SPStatType.Int, Comment = "Number of times hit"}
                },
                {
                    0x9C1A9115,
                    new SPStat
                        {
                            Name = "SP0_PASS_DB_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times killed by headshot"
                        }
                },
                {
                    0x9D1E7296,
                    new SPStat
                        {
                            Name = "SP1_PASS_DB_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times killed by headshot"
                        }
                },
                {
                    0xC73B75EF,
                    new SPStat
                        {
                            Name = "SP2_PASS_DB_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times killed by headshot"
                        }
                },
                {
                    0xD36B4073,
                    new SPStat
                        {
                            Name = "SP0_EXPLOSIVES_USED",
                            Type = SPStatType.Int,
                            Comment = "Number of Explosives used"
                        }
                },
                {
                    0x03B47688,
                    new SPStat
                        {
                            Name = "SP1_EXPLOSIVES_USED",
                            Type = SPStatType.Int,
                            Comment = "Number of Explosives used"
                        }
                },
                {
                    0x74D80482,
                    new SPStat
                        {
                            Name = "SP2_EXPLOSIVES_USED",
                            Type = SPStatType.Int,
                            Comment = "Number of Explosives used"
                        }
                },
                {
                    0x5F8C6723,
                    new SPStat
                        {
                            Name = "SP0_EXPLOSIVE_DAMAGE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of explosive damage weapons hits."
                        }
                },
                {
                    0x0982B3DD,
                    new SPStat
                        {
                            Name = "SP1_EXPLOSIVE_DAMAGE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of explosive damage weapons hits."
                        }
                },
                {
                    0xA34B5CEE,
                    new SPStat
                        {
                            Name = "SP2_EXPLOSIVE_DAMAGE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of explosive damage weapons hits."
                        }
                },
                {
                    0xDCE98BDF,
                    new SPStat
                        {
                            Name = "SP0_EXPLOSIVE_DAMAGE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of explosive damage weapons shots."
                        }
                },
                {
                    0xB2290636,
                    new SPStat
                        {
                            Name = "SP1_EXPLOSIVE_DAMAGE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of explosive damage weapons shots."
                        }
                },
                {
                    0x96FC7D4E,
                    new SPStat
                        {
                            Name = "SP2_EXPLOSIVE_DAMAGE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of explosive damage weapons shots."
                        }
                },
                {
                    0xB6818979,
                    new SPStat {Name = "SP0_WEAPON_ACCURACY", Type = SPStatType.Float, Comment = "Weapon accuracy"}
                },
                {
                    0x2784A49E,
                    new SPStat {Name = "SP1_WEAPON_ACCURACY", Type = SPStatType.Float, Comment = "Weapon accuracy"}
                },
                {
                    0x0C3A9FC6,
                    new SPStat {Name = "SP2_WEAPON_ACCURACY", Type = SPStatType.Float, Comment = "Weapon accuracy"}
                },
                {
                    0x7166D363,
                    new SPStat
                        {
                            Name = "SP0_FAVORITE_WEAPON",
                            Type = SPStatType.U32,
                            Comment = "The favorite weapon hash."
                        }
                },
                {
                    0x5886D69D,
                    new SPStat
                        {
                            Name = "SP1_FAVORITE_WEAPON",
                            Type = SPStatType.U32,
                            Comment = "The favorite weapon hash."
                        }
                },
                {
                    0x4E44B70E,
                    new SPStat
                        {
                            Name = "SP2_FAVORITE_WEAPON",
                            Type = SPStatType.U32,
                            Comment = "The favorite weapon hash."
                        }
                },
                {
                    0xDEA353CF,
                    new SPStat
                        {
                            Name = "SP0_FAVORITE_WEAPON_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time held the favotire weapon."
                        }
                },
                {
                    0xF05D2C95,
                    new SPStat
                        {
                            Name = "SP1_FAVORITE_WEAPON_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time held the favotire weapon."
                        }
                },
                {
                    0x66E1396F,
                    new SPStat
                        {
                            Name = "SP2_FAVORITE_WEAPON_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time held the favotire weapon."
                        }
                },
                {
                    0x4FE1E706,
                    new SPStat
                        {
                            Name = "SP0_UNARMED_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xD4D6F88F,
                    new SPStat
                        {
                            Name = "SP1_UNARMED_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xDB08CFA7,
                    new SPStat
                        {
                            Name = "SP2_UNARMED_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x4733C53E,
                    new SPStat
                        {
                            Name = "SP0_UNARMED_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xD6A0A4A8,
                    new SPStat
                        {
                            Name = "SP1_UNARMED_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x855BE8B6,
                    new SPStat
                        {
                            Name = "SP2_UNARMED_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x546DBFC0,
                    new SPStat
                        {
                            Name = "SP0_UNARMED_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xB39C10A1,
                    new SPStat
                        {
                            Name = "SP1_UNARMED_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x5CA0182D,
                    new SPStat
                        {
                            Name = "SP2_UNARMED_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x1B48AE06,
                    new SPStat
                        {
                            Name = "SP0_KNIFE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x728CF782,
                    new SPStat
                        {
                            Name = "SP1_KNIFE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xB656823D,
                    new SPStat
                        {
                            Name = "SP2_KNIFE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xF97DDEB8,
                    new SPStat
                        {
                            Name = "SP0_KNIFE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x4849ED32,
                    new SPStat
                        {
                            Name = "SP1_KNIFE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x798384E4,
                    new SPStat
                        {
                            Name = "SP2_KNIFE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x173AD2F7,
                    new SPStat
                        {
                            Name = "SP0_KNIFE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x59AD5414,
                    new SPStat
                        {
                            Name = "SP1_KNIFE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x8B417E64,
                    new SPStat
                        {
                            Name = "SP2_KNIFE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xBAE2C6FA,
                    new SPStat
                        {
                            Name = "SP0_KNIFE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x0BF59E03,
                    new SPStat
                        {
                            Name = "SP1_KNIFE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x3CADD40F,
                    new SPStat
                        {
                            Name = "SP2_KNIFE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x76FAE668,
                    new SPStat
                        {
                            Name = "SP0_NIGHTSTICK_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x4E659A74,
                    new SPStat
                        {
                            Name = "SP1_NIGHTSTICK_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xCE793318,
                    new SPStat
                        {
                            Name = "SP2_NIGHTSTICK_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x11440813,
                    new SPStat
                        {
                            Name = "SP0_NIGHTSTICK_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xEC45A8B2,
                    new SPStat
                        {
                            Name = "SP1_NIGHTSTICK_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xEF1A90B5,
                    new SPStat
                        {
                            Name = "SP2_NIGHTSTICK_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xFF014470,
                    new SPStat
                        {
                            Name = "SP0_NIGHTSTICK_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xCF5BD99E,
                    new SPStat
                        {
                            Name = "SP1_NIGHTSTICK_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x9FC61082,
                    new SPStat
                        {
                            Name = "SP2_NIGHTSTICK_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xE090161E,
                    new SPStat
                        {
                            Name = "SP0_NIGHTSTICK_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x52A25D5B,
                    new SPStat
                        {
                            Name = "SP1_NIGHTSTICK_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x0FEEE170,
                    new SPStat
                        {
                            Name = "SP2_NIGHTSTICK_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x59FB94C7,
                    new SPStat
                        {
                            Name = "SP0_CROWBAR_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xFD81C4EB,
                    new SPStat
                        {
                            Name = "SP1_CROWBAR_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xDEBBB2B0,
                    new SPStat
                        {
                            Name = "SP2_CROWBAR_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x8C0A16CB,
                    new SPStat
                        {
                            Name = "SP0_CROWBAR_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xC29109EE,
                    new SPStat
                        {
                            Name = "SP1_CROWBAR_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x54264A78,
                    new SPStat
                        {
                            Name = "SP2_CROWBAR_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x87045B7D,
                    new SPStat
                        {
                            Name = "SP0_CROWBAR_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x46D887E0,
                    new SPStat
                        {
                            Name = "SP1_CROWBAR_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xE3E2932D,
                    new SPStat
                        {
                            Name = "SP2_CROWBAR_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xEE76136C,
                    new SPStat
                        {
                            Name = "SP0_CROWBAR_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xE3B32405,
                    new SPStat
                        {
                            Name = "SP1_CROWBAR_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xC3E42A6E,
                    new SPStat
                        {
                            Name = "SP2_CROWBAR_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x12FE1C34,
                    new SPStat
                        {
                            Name = "SP0_SHOVEL_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xEDC81D3A,
                    new SPStat
                        {
                            Name = "SP1_SHOVEL_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x5DB11498,
                    new SPStat
                        {
                            Name = "SP2_SHOVEL_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x1A4B2700,
                    new SPStat
                        {
                            Name = "SP0_SHOVEL_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xDD18CE24,
                    new SPStat
                        {
                            Name = "SP1_SHOVEL_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x41A0CFCB,
                    new SPStat
                        {
                            Name = "SP2_SHOVEL_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x3C1F1FF8,
                    new SPStat
                        {
                            Name = "SP0_SHOVEL_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x67ED4D7E,
                    new SPStat
                        {
                            Name = "SP1_SHOVEL_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xB27FC28B,
                    new SPStat
                        {
                            Name = "SP2_SHOVEL_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xB12C8DA5,
                    new SPStat
                        {
                            Name = "SP0_SHOVEL_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x4E0C50E4,
                    new SPStat
                        {
                            Name = "SP1_SHOVEL_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x46BE7D90,
                    new SPStat
                        {
                            Name = "SP2_SHOVEL_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x21672869,
                    new SPStat
                        {
                            Name = "SP0_WRENCH_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xD7DCDD69,
                    new SPStat
                        {
                            Name = "SP1_WRENCH_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x01FE5F9D,
                    new SPStat
                        {
                            Name = "SP2_WRENCH_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xEDA3EA7D,
                    new SPStat
                        {
                            Name = "SP0_WRENCH_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xF373E673,
                    new SPStat
                        {
                            Name = "SP1_WRENCH_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xB22F1217,
                    new SPStat
                        {
                            Name = "SP2_WRENCH_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x44BBACA2,
                    new SPStat
                        {
                            Name = "SP0_WRENCH_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xC41BBF1D,
                    new SPStat
                        {
                            Name = "SP1_WRENCH_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x41E1D7B7,
                    new SPStat
                        {
                            Name = "SP2_WRENCH_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x97856A28,
                    new SPStat
                        {
                            Name = "SP0_WRENCH_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x23A110C7,
                    new SPStat
                        {
                            Name = "SP1_WRENCH_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x92E8DACB,
                    new SPStat
                        {
                            Name = "SP2_WRENCH_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x704F217F,
                    new SPStat
                        {
                            Name = "SP0_HAMMER_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xF7431774,
                    new SPStat
                        {
                            Name = "SP1_HAMMER_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x2FA67BF1,
                    new SPStat
                        {
                            Name = "SP2_HAMMER_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xB93FF3EE,
                    new SPStat
                        {
                            Name = "SP0_HAMMER_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x55B2EC38,
                    new SPStat
                        {
                            Name = "SP1_HAMMER_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x6DCE7552,
                    new SPStat
                        {
                            Name = "SP2_HAMMER_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x405122F6,
                    new SPStat
                        {
                            Name = "SP0_HAMMER_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xDF2F54ED,
                    new SPStat
                        {
                            Name = "SP1_HAMMER_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x12F1A8BC,
                    new SPStat
                        {
                            Name = "SP2_HAMMER_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xDC8EAB1D,
                    new SPStat
                        {
                            Name = "SP0_HAMMER_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xD5553E76,
                    new SPStat
                        {
                            Name = "SP1_HAMMER_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x459BD64F,
                    new SPStat
                        {
                            Name = "SP2_HAMMER_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x042BA22B,
                    new SPStat
                        {
                            Name = "SP0_BAT_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xA6D91A2B,
                    new SPStat
                        {
                            Name = "SP1_BAT_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x99FC79B1,
                    new SPStat
                        {
                            Name = "SP2_BAT_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xD592E18D,
                    new SPStat
                        {
                            Name = "SP0_BAT_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x0CB9C024,
                    new SPStat
                        {
                            Name = "SP1_BAT_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x5A16D8EC,
                    new SPStat
                        {
                            Name = "SP2_BAT_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xC2B9867B,
                    new SPStat
                        {
                            Name = "SP0_BAT_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x64ABFBCD,
                    new SPStat
                        {
                            Name = "SP1_BAT_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x4E30900F,
                    new SPStat
                        {
                            Name = "SP2_BAT_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x45B62C1E,
                    new SPStat
                        {
                            Name = "SP0_BAT_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x63B8F067,
                    new SPStat
                        {
                            Name = "SP1_BAT_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x3A8BC6E9,
                    new SPStat
                        {
                            Name = "SP2_BAT_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xAAAE49E4,
                    new SPStat
                        {
                            Name = "SP0_GCLUB_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xD6A39457,
                    new SPStat
                        {
                            Name = "SP1_GCLUB_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xC0CDA725,
                    new SPStat
                        {
                            Name = "SP2_GCLUB_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x68A4BADA,
                    new SPStat
                        {
                            Name = "SP0_GCLUB_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x6316F860,
                    new SPStat
                        {
                            Name = "SP1_GCLUB_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x2BE9B6C6,
                    new SPStat
                        {
                            Name = "SP2_GCLUB_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x977FCCB4,
                    new SPStat
                        {
                            Name = "SP0_GCLUB_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x8F672114,
                    new SPStat
                        {
                            Name = "SP1_GCLUB_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xF54C1401,
                    new SPStat
                        {
                            Name = "SP2_GCLUB_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xC08CB278,
                    new SPStat
                        {
                            Name = "SP0_GCLUB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x239B0FE2,
                    new SPStat
                        {
                            Name = "SP1_GCLUB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xF55FFCBD,
                    new SPStat
                        {
                            Name = "SP2_GCLUB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x927EC550,
                    new SPStat
                        {
                            Name = "SP0_PISTOL_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xECACE50E,
                    new SPStat
                        {
                            Name = "SP1_PISTOL_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xBE34102B,
                    new SPStat
                        {
                            Name = "SP2_PISTOL_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x9DFE850C,
                    new SPStat
                        {
                            Name = "SP0_PISTOL_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x00F23A38,
                    new SPStat
                        {
                            Name = "SP1_PISTOL_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x7D43303F,
                    new SPStat
                        {
                            Name = "SP2_PISTOL_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xB04F4D3A,
                    new SPStat
                        {
                            Name = "SP0_PISTOL_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xAB8DC726,
                    new SPStat
                        {
                            Name = "SP1_PISTOL_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x3A578749,
                    new SPStat
                        {
                            Name = "SP2_PISTOL_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x2B905FF3,
                    new SPStat
                        {
                            Name = "SP0_PISTOL_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x5B23FAC1,
                    new SPStat
                        {
                            Name = "SP1_PISTOL_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x3783ACAE,
                    new SPStat
                        {
                            Name = "SP2_PISTOL_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xA773C0D8,
                    new SPStat
                        {
                            Name = "SP0_PISTOL_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xE67976F4,
                    new SPStat
                        {
                            Name = "SP1_PISTOL_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xCB0E4204,
                    new SPStat
                        {
                            Name = "SP2_PISTOL_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xCA9314D6,
                    new SPStat
                        {
                            Name = "SP0_PISTOL_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x786C69DE,
                    new SPStat
                        {
                            Name = "SP1_PISTOL_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x4B407D10,
                    new SPStat
                        {
                            Name = "SP2_PISTOL_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x47B7B34B,
                    new SPStat
                        {
                            Name = "SP0_PISTOL_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0x443D0E68,
                    new SPStat
                        {
                            Name = "SP1_PISTOL_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0xF9D73205,
                    new SPStat
                        {
                            Name = "SP2_PISTOL_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0x58213D58,
                    new SPStat
                        {
                            Name = "SP0_PISTOL_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xAD2E1132,
                    new SPStat
                        {
                            Name = "SP1_PISTOL_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x626E5F3E,
                    new SPStat
                        {
                            Name = "SP2_PISTOL_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x7F823C7D,
                    new SPStat
                        {
                            Name = "SP0_CMBTPISTOL_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x8FEE5111,
                    new SPStat
                        {
                            Name = "SP1_CMBTPISTOL_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xB9BC8936,
                    new SPStat
                        {
                            Name = "SP2_CMBTPISTOL_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xBD4D5165,
                    new SPStat
                        {
                            Name = "SP0_CMBTPISTOL_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x0F6F87F5,
                    new SPStat
                        {
                            Name = "SP1_CMBTPISTOL_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x5749D441,
                    new SPStat
                        {
                            Name = "SP2_CMBTPISTOL_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x584A6E15,
                    new SPStat
                        {
                            Name = "SP0_CMBTPISTOL_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x7F4AB517,
                    new SPStat
                        {
                            Name = "SP1_CMBTPISTOL_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xBC7B4914,
                    new SPStat
                        {
                            Name = "SP2_CMBTPISTOL_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x2CF10787,
                    new SPStat
                        {
                            Name = "SP0_CMBTPISTOL_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x2F040608,
                    new SPStat
                        {
                            Name = "SP1_CMBTPISTOL_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x7DF152A6,
                    new SPStat
                        {
                            Name = "SP2_CMBTPISTOL_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xAFE297F7,
                    new SPStat
                        {
                            Name = "SP0_CMBTPISTOL_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xC4530444,
                    new SPStat
                        {
                            Name = "SP1_CMBTPISTOL_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xF9CB4CFD,
                    new SPStat
                        {
                            Name = "SP2_CMBTPISTOL_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x4E92EF0A,
                    new SPStat
                        {
                            Name = "SP0_CMBTPISTOL_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x1BF93126,
                    new SPStat
                        {
                            Name = "SP1_CMBTPISTOL_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xB2B162A6,
                    new SPStat
                        {
                            Name = "SP2_CMBTPISTOL_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xF335DA43,
                    new SPStat
                        {
                            Name = "SP0_CMBTPISTOL_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0xDCF37E45,
                    new SPStat
                        {
                            Name = "SP1_CMBTPISTOL_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0xF58C37AB,
                    new SPStat
                        {
                            Name = "SP2_CMBTPISTOL_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0xC753EA35,
                    new SPStat
                        {
                            Name = "SP0_CMBTPISTOL_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xCE98C687,
                    new SPStat
                        {
                            Name = "SP1_CMBTPISTOL_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xC234C41C,
                    new SPStat
                        {
                            Name = "SP2_CMBTPISTOL_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xEEDD64DB,
                    new SPStat
                        {
                            Name = "SP0_PISTOL50_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x9F82D0DC,
                    new SPStat
                        {
                            Name = "SP1_PISTOL50_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xF40BAAA4,
                    new SPStat
                        {
                            Name = "SP2_PISTOL50_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xECD898A7,
                    new SPStat
                        {
                            Name = "SP0_PISTOL50_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x01D908F7,
                    new SPStat
                        {
                            Name = "SP1_PISTOL50_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xFB41CB4A,
                    new SPStat
                        {
                            Name = "SP2_PISTOL50_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xFE84C7E4,
                    new SPStat
                        {
                            Name = "SP0_PISTOL50_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xA1991D09,
                    new SPStat
                        {
                            Name = "SP1_PISTOL50_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x161EF26E,
                    new SPStat
                        {
                            Name = "SP2_PISTOL50_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x2A9CB6BD,
                    new SPStat
                        {
                            Name = "SP0_PISTOL50_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xBB4BF6A7,
                    new SPStat
                        {
                            Name = "SP1_PISTOL50_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x87906E1E,
                    new SPStat
                        {
                            Name = "SP2_PISTOL50_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xBCD54417,
                    new SPStat
                        {
                            Name = "SP0_PISTOL50_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x51273F22,
                    new SPStat
                        {
                            Name = "SP1_PISTOL50_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x9053B84B,
                    new SPStat
                        {
                            Name = "SP2_PISTOL50_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xD00B92B4,
                    new SPStat
                        {
                            Name = "SP0_PISTOL50_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x28DC6D47,
                    new SPStat
                        {
                            Name = "SP1_PISTOL50_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x0A6CE8EF,
                    new SPStat
                        {
                            Name = "SP2_PISTOL50_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x557DC879,
                    new SPStat
                        {
                            Name = "SP0_PISTOL50_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0xE3E34312,
                    new SPStat
                        {
                            Name = "SP1_PISTOL50_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0x974B4B48,
                    new SPStat
                        {
                            Name = "SP2_PISTOL50_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0x1EE938F3,
                    new SPStat
                        {
                            Name = "SP0_PISTOL50_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xBC037770,
                    new SPStat
                        {
                            Name = "SP1_PISTOL50_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x07873013,
                    new SPStat
                        {
                            Name = "SP2_PISTOL50_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x3525C7D2,
                    new SPStat
                        {
                            Name = "SP0_APPISTOL_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x10A173AB,
                    new SPStat
                        {
                            Name = "SP1_APPISTOL_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x5FCC4D2C,
                    new SPStat
                        {
                            Name = "SP2_APPISTOL_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x4AC511C2,
                    new SPStat
                        {
                            Name = "SP0_APPISTOL_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xD8F7652D,
                    new SPStat
                        {
                            Name = "SP1_APPISTOL_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xB27AE540,
                    new SPStat
                        {
                            Name = "SP2_APPISTOL_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xECE7F37B,
                    new SPStat
                        {
                            Name = "SP0_APPISTOL_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xFED8FB57,
                    new SPStat
                        {
                            Name = "SP1_APPISTOL_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x69F5DB4D,
                    new SPStat
                        {
                            Name = "SP2_APPISTOL_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x2B29EEAA,
                    new SPStat
                        {
                            Name = "SP0_APPISTOL_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xEA05C29C,
                    new SPStat
                        {
                            Name = "SP1_APPISTOL_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x91D78E29,
                    new SPStat
                        {
                            Name = "SP2_APPISTOL_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xC5B6DCE1,
                    new SPStat
                        {
                            Name = "SP0_APPISTOL_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x202BA224,
                    new SPStat
                        {
                            Name = "SP1_APPISTOL_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xC9FE8FA8,
                    new SPStat
                        {
                            Name = "SP2_APPISTOL_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xDD1D2E93,
                    new SPStat
                        {
                            Name = "SP0_APPISTOL_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x604634E1,
                    new SPStat
                        {
                            Name = "SP1_APPISTOL_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x2E28DFF8,
                    new SPStat
                        {
                            Name = "SP2_APPISTOL_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x1C0EBB26,
                    new SPStat
                        {
                            Name = "SP0_APPISTOL_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0x96787082,
                    new SPStat
                        {
                            Name = "SP1_APPISTOL_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0xD6F23996,
                    new SPStat
                        {
                            Name = "SP2_APPISTOL_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0x3DA7ADB5,
                    new SPStat
                        {
                            Name = "SP0_APPISTOL_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xF81F9E2F,
                    new SPStat
                        {
                            Name = "SP1_APPISTOL_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x8E7BB98B,
                    new SPStat
                        {
                            Name = "SP2_APPISTOL_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xE9F4CF77,
                    new SPStat
                        {
                            Name = "SP0_MICROSMG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x9B398F32,
                    new SPStat
                        {
                            Name = "SP1_MICROSMG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x0899E092,
                    new SPStat
                        {
                            Name = "SP2_MICROSMG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x93665EDB,
                    new SPStat
                        {
                            Name = "SP0_MICROSMG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xB0559894,
                    new SPStat
                        {
                            Name = "SP1_MICROSMG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x217ECA01,
                    new SPStat
                        {
                            Name = "SP2_MICROSMG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x2493BDCD,
                    new SPStat
                        {
                            Name = "SP0_MICROSMG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xFE16654D,
                    new SPStat
                        {
                            Name = "SP1_MICROSMG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x6D6D03F0,
                    new SPStat
                        {
                            Name = "SP2_MICROSMG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xB7A62A4F,
                    new SPStat
                        {
                            Name = "SP0_MICROSMG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xED0C70B8,
                    new SPStat
                        {
                            Name = "SP1_MICROSMG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x03EAD219,
                    new SPStat
                        {
                            Name = "SP2_MICROSMG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x53098BF2,
                    new SPStat
                        {
                            Name = "SP0_MICROSMG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x58B0CD6F,
                    new SPStat
                        {
                            Name = "SP1_MICROSMG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xAB315FB7,
                    new SPStat
                        {
                            Name = "SP2_MICROSMG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x5067802A,
                    new SPStat
                        {
                            Name = "SP0_MICROSMG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xAF31E63D,
                    new SPStat
                        {
                            Name = "SP1_MICROSMG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xDCF8E594,
                    new SPStat
                        {
                            Name = "SP2_MICROSMG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xB4ED303D,
                    new SPStat
                        {
                            Name = "SP0_MICROSMG_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0x8729A854,
                    new SPStat
                        {
                            Name = "SP1_MICROSMG_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0xA72C1D3A,
                    new SPStat
                        {
                            Name = "SP2_MICROSMG_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0x1470D3C4,
                    new SPStat
                        {
                            Name = "SP0_MICROSMG_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x4011D3D7,
                    new SPStat
                        {
                            Name = "SP1_MICROSMG_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x2F29A4D6,
                    new SPStat
                        {
                            Name = "SP2_MICROSMG_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x24A7C4A6,
                    new SPStat
                        {
                            Name = "SP0_SMG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xF12094C7,
                    new SPStat
                        {
                            Name = "SP1_SMG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xF15AC81B,
                    new SPStat
                        {
                            Name = "SP2_SMG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x21F0A1B2,
                    new SPStat
                        {
                            Name = "SP0_SMG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xC4536784,
                    new SPStat
                        {
                            Name = "SP1_SMG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x4D397C1C,
                    new SPStat
                        {
                            Name = "SP2_SMG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x28F3AA0A,
                    new SPStat
                        {
                            Name = "SP0_SMG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x286AC24B,
                    new SPStat
                        {
                            Name = "SP1_SMG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x12DB7FE4,
                    new SPStat
                        {
                            Name = "SP2_SMG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x82EF11CF,
                    new SPStat
                        {
                            Name = "SP0_SMG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xA94FE9F3,
                    new SPStat
                        {
                            Name = "SP1_SMG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xC7FAFB43,
                    new SPStat
                        {
                            Name = "SP2_SMG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x5663C55E,
                    new SPStat
                        {
                            Name = "SP0_SMG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x7F79C6EA,
                    new SPStat
                        {
                            Name = "SP1_SMG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x3667D739,
                    new SPStat
                        {
                            Name = "SP2_SMG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x579CB17C,
                    new SPStat
                        {
                            Name = "SP0_SMG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x2889B62F,
                    new SPStat
                        {
                            Name = "SP1_SMG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x1461E647,
                    new SPStat
                        {
                            Name = "SP2_SMG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x8F96DACB,
                    new SPStat
                        {
                            Name = "SP0_SMG_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0x9431DF33,
                    new SPStat
                        {
                            Name = "SP1_SMG_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0x4C11AA82,
                    new SPStat
                        {
                            Name = "SP2_SMG_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0x6FB2AC72,
                    new SPStat {Name = "SP0_SMG_AMMO_BOUGHT", Type = SPStatType.Int, Comment = "Ammo bought for weapon"}
                },
                {
                    0xC82D8E12,
                    new SPStat {Name = "SP1_SMG_AMMO_BOUGHT", Type = SPStatType.Int, Comment = "Ammo bought for weapon"}
                },
                {
                    0xDCA839B5,
                    new SPStat {Name = "SP2_SMG_AMMO_BOUGHT", Type = SPStatType.Int, Comment = "Ammo bought for weapon"}
                },
                {
                    0xE44DDE4D,
                    new SPStat
                        {
                            Name = "SP0_ASLTSMG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xC8292B57,
                    new SPStat
                        {
                            Name = "SP1_ASLTSMG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x942CA440,
                    new SPStat
                        {
                            Name = "SP2_ASLTSMG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x26A90359,
                    new SPStat
                        {
                            Name = "SP0_ASLTSMG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x6F59BDB2,
                    new SPStat
                        {
                            Name = "SP1_ASLTSMG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x2BD904E8,
                    new SPStat
                        {
                            Name = "SP2_ASLTSMG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xD24E6069,
                    new SPStat
                        {
                            Name = "SP0_ASLTSMG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x2CEE801C,
                    new SPStat
                        {
                            Name = "SP1_ASLTSMG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xD60FDC89,
                    new SPStat
                        {
                            Name = "SP2_ASLTSMG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x0022F5CD,
                    new SPStat
                        {
                            Name = "SP0_ASLTSMG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xC3858541,
                    new SPStat
                        {
                            Name = "SP1_ASLTSMG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xBA4BA8DD,
                    new SPStat
                        {
                            Name = "SP2_ASLTSMG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xA4779827,
                    new SPStat
                        {
                            Name = "SP0_ASLTSMG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x4ECECFD5,
                    new SPStat
                        {
                            Name = "SP1_ASLTSMG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xF6611F6A,
                    new SPStat
                        {
                            Name = "SP2_ASLTSMG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xF786635F,
                    new SPStat
                        {
                            Name = "SP0_ASLTSMG_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0xB0D44AAB,
                    new SPStat
                        {
                            Name = "SP1_ASLTSMG_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0x063E9CD5,
                    new SPStat
                        {
                            Name = "SP2_ASLTSMG_DB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held drive by weapon (MS)"
                        }
                },
                {
                    0xEAE5C98F,
                    new SPStat
                        {
                            Name = "SP0_ASLTSMG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x75150101,
                    new SPStat
                        {
                            Name = "SP1_ASLTSMG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xAB6B2964,
                    new SPStat
                        {
                            Name = "SP2_ASLTSMG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xCD799235,
                    new SPStat
                        {
                            Name = "SP0_ASLTSMG_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x59A265C4,
                    new SPStat
                        {
                            Name = "SP1_ASLTSMG_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xB95A796F,
                    new SPStat
                        {
                            Name = "SP2_ASLTSMG_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x11B8A56B,
                    new SPStat
                        {
                            Name = "SP0_ASLTRIFLE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x0E10BBDE,
                    new SPStat
                        {
                            Name = "SP1_ASLTRIFLE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x2D29EBA5,
                    new SPStat
                        {
                            Name = "SP2_ASLTRIFLE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x0C768D9C,
                    new SPStat
                        {
                            Name = "SP0_ASLTRIFLE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xD289F0DF,
                    new SPStat
                        {
                            Name = "SP1_ASLTRIFLE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x55307D94,
                    new SPStat
                        {
                            Name = "SP2_ASLTRIFLE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x3A2A45CC,
                    new SPStat
                        {
                            Name = "SP0_ASLTRIFLE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xE5137662,
                    new SPStat
                        {
                            Name = "SP1_ASLTRIFLE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x198468B9,
                    new SPStat
                        {
                            Name = "SP2_ASLTRIFLE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x25214BE5,
                    new SPStat
                        {
                            Name = "SP0_ASLTRIFLE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xACDE073F,
                    new SPStat
                        {
                            Name = "SP1_ASLTRIFLE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xFC71B3C3,
                    new SPStat
                        {
                            Name = "SP2_ASLTRIFLE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x0887FEF6,
                    new SPStat
                        {
                            Name = "SP0_ASLTRIFLE_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x1B5FB754,
                    new SPStat
                        {
                            Name = "SP1_ASLTRIFLE_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xC749A2C4,
                    new SPStat
                        {
                            Name = "SP2_ASLTRIFLE_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xEDEA6730,
                    new SPStat
                        {
                            Name = "SP0_ASLTRIFLE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x784AC6F4,
                    new SPStat
                        {
                            Name = "SP1_ASLTRIFLE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xA7D0C0D4,
                    new SPStat
                        {
                            Name = "SP2_ASLTRIFLE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x78F51914,
                    new SPStat
                        {
                            Name = "SP0_ASLTRIFLE_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x96E77323,
                    new SPStat
                        {
                            Name = "SP1_ASLTRIFLE_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x37CD04E7,
                    new SPStat
                        {
                            Name = "SP2_ASLTRIFLE_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x2E1ACB9B,
                    new SPStat
                        {
                            Name = "SP0_CRBNRIFLE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x21094EE7,
                    new SPStat
                        {
                            Name = "SP1_CRBNRIFLE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x81B49126,
                    new SPStat
                        {
                            Name = "SP2_CRBNRIFLE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xDBABC073,
                    new SPStat
                        {
                            Name = "SP0_CRBNRIFLE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x8D181EE7,
                    new SPStat
                        {
                            Name = "SP1_CRBNRIFLE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x3ED00260,
                    new SPStat
                        {
                            Name = "SP2_CRBNRIFLE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xB5FD84C1,
                    new SPStat
                        {
                            Name = "SP0_CRBNRIFLE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x2E05243F,
                    new SPStat
                        {
                            Name = "SP1_CRBNRIFLE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xEE4BAE25,
                    new SPStat
                        {
                            Name = "SP2_CRBNRIFLE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x9F9BF062,
                    new SPStat
                        {
                            Name = "SP0_CRBNRIFLE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x74BA7075,
                    new SPStat
                        {
                            Name = "SP1_CRBNRIFLE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xD5421EA1,
                    new SPStat
                        {
                            Name = "SP2_CRBNRIFLE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xE3A73C78,
                    new SPStat
                        {
                            Name = "SP0_CRBNRIFLE_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x10191DE8,
                    new SPStat
                        {
                            Name = "SP1_CRBNRIFLE_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xC6C44E80,
                    new SPStat
                        {
                            Name = "SP2_CRBNRIFLE_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x762AE622,
                    new SPStat
                        {
                            Name = "SP0_CRBNRIFLE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x2AB2DF5A,
                    new SPStat
                        {
                            Name = "SP1_CRBNRIFLE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xB9E0DDA2,
                    new SPStat
                        {
                            Name = "SP2_CRBNRIFLE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x9399726E,
                    new SPStat
                        {
                            Name = "SP0_CRBNRIFLE_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x2009F3F7,
                    new SPStat
                        {
                            Name = "SP1_CRBNRIFLE_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x7F499878,
                    new SPStat
                        {
                            Name = "SP2_CRBNRIFLE_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xA319F06A,
                    new SPStat
                        {
                            Name = "SP0_ADVRIFLE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x27426333,
                    new SPStat
                        {
                            Name = "SP1_ADVRIFLE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x29F28370,
                    new SPStat
                        {
                            Name = "SP2_ADVRIFLE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x7E234325,
                    new SPStat
                        {
                            Name = "SP0_ADVRIFLE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xDA35C7D1,
                    new SPStat
                        {
                            Name = "SP1_ADVRIFLE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xF91A30F9,
                    new SPStat
                        {
                            Name = "SP2_ADVRIFLE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xB38FDC1C,
                    new SPStat
                        {
                            Name = "SP0_ADVRIFLE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xA355C68C,
                    new SPStat
                        {
                            Name = "SP1_ADVRIFLE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x6BA25703,
                    new SPStat
                        {
                            Name = "SP2_ADVRIFLE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x8EC05449,
                    new SPStat
                        {
                            Name = "SP0_ADVRIFLE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x130DD4D6,
                    new SPStat
                        {
                            Name = "SP1_ADVRIFLE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x08D9676B,
                    new SPStat
                        {
                            Name = "SP2_ADVRIFLE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xEBAA6516,
                    new SPStat
                        {
                            Name = "SP0_ADVRIFLE_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x4BF874AA,
                    new SPStat
                        {
                            Name = "SP1_ADVRIFLE_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xD24D2463,
                    new SPStat
                        {
                            Name = "SP2_ADVRIFLE_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x39F30167,
                    new SPStat
                        {
                            Name = "SP0_ADVRIFLE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x7F4CE53F,
                    new SPStat
                        {
                            Name = "SP1_ADVRIFLE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x42450B1B,
                    new SPStat
                        {
                            Name = "SP2_ADVRIFLE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x0A62B2DC,
                    new SPStat
                        {
                            Name = "SP0_ADVRIFLE_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x95D78C20,
                    new SPStat
                        {
                            Name = "SP1_ADVRIFLE_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xCC287FF9,
                    new SPStat
                        {
                            Name = "SP2_ADVRIFLE_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xAAFBD990,
                    new SPStat
                        {
                            Name = "SP0_MG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x88869DB8,
                    new SPStat
                        {
                            Name = "SP1_MG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x800936AF,
                    new SPStat
                        {
                            Name = "SP2_MG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x006BD4D1,
                    new SPStat
                        {
                            Name = "SP0_MG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x892909F9,
                    new SPStat
                        {
                            Name = "SP1_MG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x76C029B6,
                    new SPStat
                        {
                            Name = "SP2_MG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x579B9496,
                    new SPStat
                        {
                            Name = "SP0_MG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xA1076A3B,
                    new SPStat
                        {
                            Name = "SP1_MG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xE1EEF7BE,
                    new SPStat
                        {
                            Name = "SP2_MG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x63E2063D,
                    new SPStat
                        {
                            Name = "SP0_MG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xB0EB5B48,
                    new SPStat
                        {
                            Name = "SP1_MG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x863BDFF3,
                    new SPStat
                        {
                            Name = "SP2_MG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x49145FB7,
                    new SPStat
                        {
                            Name = "SP0_MG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x1AA9CD85,
                    new SPStat
                        {
                            Name = "SP1_MG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x561EF318,
                    new SPStat
                        {
                            Name = "SP2_MG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x50805A76,
                    new SPStat
                        {
                            Name = "SP0_MG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xE9709D76,
                    new SPStat
                        {
                            Name = "SP1_MG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x80C5FF03,
                    new SPStat
                        {
                            Name = "SP2_MG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xF272E1AF,
                    new SPStat {Name = "SP0_MG_AMMO_BOUGHT", Type = SPStatType.Int, Comment = "Ammo bought for weapon"}
                },
                {
                    0xDB48D77E,
                    new SPStat {Name = "SP1_MG_AMMO_BOUGHT", Type = SPStatType.Int, Comment = "Ammo bought for weapon"}
                },
                {
                    0xF0FFFACB,
                    new SPStat {Name = "SP2_MG_AMMO_BOUGHT", Type = SPStatType.Int, Comment = "Ammo bought for weapon"}
                },
                {
                    0xA4D9F3C0,
                    new SPStat
                        {
                            Name = "SP0_CMBTMG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xE231C9BF,
                    new SPStat
                        {
                            Name = "SP1_CMBTMG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x391AD98E,
                    new SPStat
                        {
                            Name = "SP2_CMBTMG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x798139AE,
                    new SPStat
                        {
                            Name = "SP0_CMBTMG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xCD6B895E,
                    new SPStat
                        {
                            Name = "SP1_CMBTMG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x5EED3B1F,
                    new SPStat
                        {
                            Name = "SP2_CMBTMG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x3CC8DBEC,
                    new SPStat
                        {
                            Name = "SP0_CMBTMG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x44E3E8F8,
                    new SPStat
                        {
                            Name = "SP1_CMBTMG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x99209C78,
                    new SPStat
                        {
                            Name = "SP2_CMBTMG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xB0D35BC7,
                    new SPStat
                        {
                            Name = "SP0_CMBTMG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x69258AA0,
                    new SPStat
                        {
                            Name = "SP1_CMBTMG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x9E298614,
                    new SPStat
                        {
                            Name = "SP2_CMBTMG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x8BCB4AFF,
                    new SPStat
                        {
                            Name = "SP0_CMBTMG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x8B12C8F4,
                    new SPStat
                        {
                            Name = "SP1_CMBTMG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xF6B4932A,
                    new SPStat
                        {
                            Name = "SP2_CMBTMG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xF2CB9455,
                    new SPStat
                        {
                            Name = "SP0_CMBTMG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x0D2FC365,
                    new SPStat
                        {
                            Name = "SP1_CMBTMG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x6E8EE2A6,
                    new SPStat
                        {
                            Name = "SP2_CMBTMG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x3CA6AC8C,
                    new SPStat
                        {
                            Name = "SP0_CMBTMG_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x2BC2F280,
                    new SPStat
                        {
                            Name = "SP1_CMBTMG_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xDB0E79D9,
                    new SPStat
                        {
                            Name = "SP2_CMBTMG_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xAA805306,
                    new SPStat
                        {
                            Name = "SP0_ASLTMG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x4A34711C,
                    new SPStat
                        {
                            Name = "SP1_ASLTMG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x76C2C6AD,
                    new SPStat
                        {
                            Name = "SP2_ASLTMG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xBBE65EF0,
                    new SPStat
                        {
                            Name = "SP0_ASLTMG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x3A3DD8A5,
                    new SPStat
                        {
                            Name = "SP1_ASLTMG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x5C11E40C,
                    new SPStat
                        {
                            Name = "SP2_ASLTMG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xDFFA596B,
                    new SPStat
                        {
                            Name = "SP0_ASLTMG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xCC7BFD31,
                    new SPStat
                        {
                            Name = "SP1_ASLTMG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xCF9DDCC5,
                    new SPStat
                        {
                            Name = "SP2_ASLTMG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x6858C6B6,
                    new SPStat
                        {
                            Name = "SP0_ASLTMG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x1A104229,
                    new SPStat
                        {
                            Name = "SP1_ASLTMG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x99F5861F,
                    new SPStat
                        {
                            Name = "SP2_ASLTMG_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x53AF4FB5,
                    new SPStat
                        {
                            Name = "SP0_ASLTMG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xAAFDD875,
                    new SPStat
                        {
                            Name = "SP1_ASLTMG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x21C4EB44,
                    new SPStat
                        {
                            Name = "SP2_ASLTMG_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x3DD130BC,
                    new SPStat
                        {
                            Name = "SP0_ASLTMG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xF55294BA,
                    new SPStat
                        {
                            Name = "SP1_ASLTMG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xCDDF0C03,
                    new SPStat
                        {
                            Name = "SP2_ASLTMG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x6D428BE3,
                    new SPStat
                        {
                            Name = "SP0_ASLTMG_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xD1658647,
                    new SPStat
                        {
                            Name = "SP1_ASLTMG_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x46F87EFD,
                    new SPStat
                        {
                            Name = "SP2_ASLTMG_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xD5F7453F,
                    new SPStat
                        {
                            Name = "SP0_PUMP_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xED8F0367,
                    new SPStat
                        {
                            Name = "SP1_PUMP_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xD641E29A,
                    new SPStat
                        {
                            Name = "SP2_PUMP_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x4876E175,
                    new SPStat
                        {
                            Name = "SP0_PUMP_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x37EC0D7C,
                    new SPStat
                        {
                            Name = "SP1_PUMP_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xB70ED1AB,
                    new SPStat
                        {
                            Name = "SP2_PUMP_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x30F94B9B,
                    new SPStat
                        {
                            Name = "SP0_PUMP_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x079EC013,
                    new SPStat
                        {
                            Name = "SP1_PUMP_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x7E931DE7,
                    new SPStat
                        {
                            Name = "SP2_PUMP_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x182C67ED,
                    new SPStat
                        {
                            Name = "SP0_PUMP_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x37544A8B,
                    new SPStat
                        {
                            Name = "SP1_PUMP_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x1CC0E29A,
                    new SPStat
                        {
                            Name = "SP2_PUMP_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xD8B14C42,
                    new SPStat
                        {
                            Name = "SP0_PUMP_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x00C9243C,
                    new SPStat
                        {
                            Name = "SP1_PUMP_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x52B03DE8,
                    new SPStat
                        {
                            Name = "SP2_PUMP_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x1511A224,
                    new SPStat
                        {
                            Name = "SP0_PUMP_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x407C6E29,
                    new SPStat
                        {
                            Name = "SP1_PUMP_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x6E6F0669,
                    new SPStat
                        {
                            Name = "SP2_PUMP_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x1D76089A,
                    new SPStat
                        {
                            Name = "SP0_PUMP_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x0FE6B187,
                    new SPStat
                        {
                            Name = "SP1_PUMP_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x185FE2C6,
                    new SPStat
                        {
                            Name = "SP2_PUMP_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xA1ED1981,
                    new SPStat
                        {
                            Name = "SP0_SAWNOFF_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x23A25048,
                    new SPStat
                        {
                            Name = "SP1_SAWNOFF_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x5707093B,
                    new SPStat
                        {
                            Name = "SP2_SAWNOFF_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xC60DECA2,
                    new SPStat
                        {
                            Name = "SP0_SAWNOFF_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x1EFE1FF9,
                    new SPStat
                        {
                            Name = "SP1_SAWNOFF_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x6A28EECE,
                    new SPStat
                        {
                            Name = "SP2_SAWNOFF_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x1B0B50E8,
                    new SPStat
                        {
                            Name = "SP0_SAWNOFF_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xB26B68C5,
                    new SPStat
                        {
                            Name = "SP1_SAWNOFF_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xE0404CC2,
                    new SPStat
                        {
                            Name = "SP2_SAWNOFF_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xD166D323,
                    new SPStat
                        {
                            Name = "SP0_SAWNOFF_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x5093DFCD,
                    new SPStat
                        {
                            Name = "SP1_SAWNOFF_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x1C21ED25,
                    new SPStat
                        {
                            Name = "SP2_SAWNOFF_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xAF786CF9,
                    new SPStat
                        {
                            Name = "SP0_SAWNOFF_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xAE73F281,
                    new SPStat
                        {
                            Name = "SP1_SAWNOFF_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x7DA4FD9B,
                    new SPStat
                        {
                            Name = "SP2_SAWNOFF_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x26D1C6F2,
                    new SPStat
                        {
                            Name = "SP0_SAWNOFF_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x53D3F835,
                    new SPStat
                        {
                            Name = "SP1_SAWNOFF_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x73D83FDD,
                    new SPStat
                        {
                            Name = "SP2_SAWNOFF_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x89E362B3,
                    new SPStat
                        {
                            Name = "SP0_SAWNOFF_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xA605B3D8,
                    new SPStat
                        {
                            Name = "SP1_SAWNOFF_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xDF5CA036,
                    new SPStat
                        {
                            Name = "SP2_SAWNOFF_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x73668062,
                    new SPStat
                        {
                            Name = "SP0_BULLPUP_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x81ED48FE,
                    new SPStat
                        {
                            Name = "SP1_BULLPUP_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xEDA0C0C5,
                    new SPStat
                        {
                            Name = "SP2_BULLPUP_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x76585C54,
                    new SPStat
                        {
                            Name = "SP0_BULLPUP_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x643DA37F,
                    new SPStat
                        {
                            Name = "SP1_BULLPUP_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xB73A64E4,
                    new SPStat
                        {
                            Name = "SP2_BULLPUP_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xE6C669FA,
                    new SPStat
                        {
                            Name = "SP0_BULLPUP_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x4B9F3656,
                    new SPStat
                        {
                            Name = "SP1_BULLPUP_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x1B01ADD1,
                    new SPStat
                        {
                            Name = "SP2_BULLPUP_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x59272071,
                    new SPStat
                        {
                            Name = "SP0_BULLPUP_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xF75F6472,
                    new SPStat
                        {
                            Name = "SP1_BULLPUP_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x6BABFAC4,
                    new SPStat
                        {
                            Name = "SP2_BULLPUP_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x8D3F0F11,
                    new SPStat
                        {
                            Name = "SP0_BULLPUP_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xA16C8811,
                    new SPStat
                        {
                            Name = "SP1_BULLPUP_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x4B8055A4,
                    new SPStat
                        {
                            Name = "SP2_BULLPUP_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xB1540D85,
                    new SPStat
                        {
                            Name = "SP0_BULLPUP_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x33E04A89,
                    new SPStat
                        {
                            Name = "SP1_BULLPUP_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x990CAA54,
                    new SPStat
                        {
                            Name = "SP2_BULLPUP_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x740484FB,
                    new SPStat
                        {
                            Name = "SP0_BULLPUP_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x4404C57E,
                    new SPStat
                        {
                            Name = "SP1_BULLPUP_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x9AEA3DD4,
                    new SPStat
                        {
                            Name = "SP2_BULLPUP_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xCF4DAED4,
                    new SPStat
                        {
                            Name = "SP0_ASLTSHTGN_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x717CE802,
                    new SPStat
                        {
                            Name = "SP1_ASLTSHTGN_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xA2C2EAC1,
                    new SPStat
                        {
                            Name = "SP2_ASLTSHTGN_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x1BAA1630,
                    new SPStat
                        {
                            Name = "SP0_ASLTSHTGN_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x9AD75482,
                    new SPStat
                        {
                            Name = "SP1_ASLTSHTGN_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x02D4021B,
                    new SPStat
                        {
                            Name = "SP2_ASLTSHTGN_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x900D2126,
                    new SPStat
                        {
                            Name = "SP0_ASLTSHTGN_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x63CCFE4A,
                    new SPStat
                        {
                            Name = "SP1_ASLTSHTGN_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xA38F4778,
                    new SPStat
                        {
                            Name = "SP2_ASLTSHTGN_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xB124630E,
                    new SPStat
                        {
                            Name = "SP0_ASLTSHTGN_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xB32B7ACC,
                    new SPStat
                        {
                            Name = "SP1_ASLTSHTGN_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x48FD9E6E,
                    new SPStat
                        {
                            Name = "SP2_ASLTSHTGN_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x163F9B26,
                    new SPStat
                        {
                            Name = "SP0_ASLTSHTGN_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x2C244A28,
                    new SPStat
                        {
                            Name = "SP1_ASLTSHTGN_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x2F113D12,
                    new SPStat
                        {
                            Name = "SP2_ASLTSHTGN_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xACCF9900,
                    new SPStat
                        {
                            Name = "SP0_ASLTSHTGN_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xFF0C55EB,
                    new SPStat
                        {
                            Name = "SP1_ASLTSHTGN_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x1264F78C,
                    new SPStat
                        {
                            Name = "SP2_ASLTSHTGN_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x2DC74880,
                    new SPStat
                        {
                            Name = "SP0_ASLTSHTGN_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x452C8ADC,
                    new SPStat
                        {
                            Name = "SP1_ASLTSHTGN_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x6118442E,
                    new SPStat
                        {
                            Name = "SP2_ASLTSHTGN_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x8A11B38E,
                    new SPStat
                        {
                            Name = "SP0_STUNGUN_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x96EBC4D7,
                    new SPStat
                        {
                            Name = "SP1_STUNGUN_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x9809E142,
                    new SPStat
                        {
                            Name = "SP2_STUNGUN_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x1E1B0A75,
                    new SPStat
                        {
                            Name = "SP0_STUNGUN_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x608DC88E,
                    new SPStat
                        {
                            Name = "SP1_STUNGUN_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x74E76C2A,
                    new SPStat
                        {
                            Name = "SP2_STUNGUN_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xD0C31AD6,
                    new SPStat
                        {
                            Name = "SP0_STUNGUN_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x99832876,
                    new SPStat
                        {
                            Name = "SP1_STUNGUN_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x6844B551,
                    new SPStat
                        {
                            Name = "SP2_STUNGUN_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x2CE6B309,
                    new SPStat
                        {
                            Name = "SP0_STUNGUN_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x4A61A118,
                    new SPStat
                        {
                            Name = "SP1_STUNGUN_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xB0629099,
                    new SPStat
                        {
                            Name = "SP2_STUNGUN_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x89C2337B,
                    new SPStat
                        {
                            Name = "SP0_STUNGUN_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x9B3858AF,
                    new SPStat
                        {
                            Name = "SP1_STUNGUN_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x3095FE45,
                    new SPStat
                        {
                            Name = "SP2_STUNGUN_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x4674715E,
                    new SPStat
                        {
                            Name = "SP0_STUNGUN_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x581C5C2A,
                    new SPStat
                        {
                            Name = "SP1_STUNGUN_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x395AC1C5,
                    new SPStat
                        {
                            Name = "SP2_STUNGUN_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x88B9EA96,
                    new SPStat
                        {
                            Name = "SP0_STUNGUN_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xDE9CC104,
                    new SPStat
                        {
                            Name = "SP1_STUNGUN_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xD735ECF5,
                    new SPStat
                        {
                            Name = "SP2_STUNGUN_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xAF25C5B3,
                    new SPStat
                        {
                            Name = "SP0_SNIPERRFL_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x3F6C2508,
                    new SPStat
                        {
                            Name = "SP1_SNIPERRFL_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xF03462FB,
                    new SPStat
                        {
                            Name = "SP2_SNIPERRFL_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xD60F9F4A,
                    new SPStat
                        {
                            Name = "SP0_SNIPERRFL_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xC28F2AC3,
                    new SPStat
                        {
                            Name = "SP1_SNIPERRFL_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x79D72818,
                    new SPStat
                        {
                            Name = "SP2_SNIPERRFL_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x4A3B3F6E,
                    new SPStat
                        {
                            Name = "SP0_SNIPERRFL_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xEE273BA0,
                    new SPStat
                        {
                            Name = "SP1_SNIPERRFL_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xDF18C7E7,
                    new SPStat
                        {
                            Name = "SP2_SNIPERRFL_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x83AFEB1F,
                    new SPStat
                        {
                            Name = "SP0_SNIPERRFL_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x738EDFEE,
                    new SPStat
                        {
                            Name = "SP1_SNIPERRFL_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xB52AF0F4,
                    new SPStat
                        {
                            Name = "SP2_SNIPERRFL_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xF59302F6,
                    new SPStat
                        {
                            Name = "SP0_SNIPERRFL_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x86A340B1,
                    new SPStat
                        {
                            Name = "SP1_SNIPERRFL_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xE9C69995,
                    new SPStat
                        {
                            Name = "SP2_SNIPERRFL_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x5F8A7D8B,
                    new SPStat
                        {
                            Name = "SP0_SNIPERRFL_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x8A6AAB98,
                    new SPStat
                        {
                            Name = "SP1_SNIPERRFL_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xF4BD4B30,
                    new SPStat
                        {
                            Name = "SP2_SNIPERRFL_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x3ED3098B,
                    new SPStat
                        {
                            Name = "SP0_SNIPERRFL_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x84660997,
                    new SPStat
                        {
                            Name = "SP1_SNIPERRFL_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x92F79B2C,
                    new SPStat
                        {
                            Name = "SP2_SNIPERRFL_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x8ADC817D,
                    new SPStat
                        {
                            Name = "SP0_HVYSNIPER_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x032A17AE,
                    new SPStat
                        {
                            Name = "SP1_HVYSNIPER_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x95E7C54B,
                    new SPStat
                        {
                            Name = "SP2_HVYSNIPER_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xD6D6F043,
                    new SPStat
                        {
                            Name = "SP0_HVYSNIPER_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x65EEF313,
                    new SPStat
                        {
                            Name = "SP1_HVYSNIPER_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x83A97F47,
                    new SPStat
                        {
                            Name = "SP2_HVYSNIPER_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x51B95A64,
                    new SPStat
                        {
                            Name = "SP0_HVYSNIPER_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x1974B322,
                    new SPStat
                        {
                            Name = "SP1_HVYSNIPER_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x88D0A862,
                    new SPStat
                        {
                            Name = "SP2_HVYSNIPER_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x5E0826E6,
                    new SPStat
                        {
                            Name = "SP0_HVYSNIPER_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x2AA6CD70,
                    new SPStat
                        {
                            Name = "SP1_HVYSNIPER_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xDC9A0153,
                    new SPStat
                        {
                            Name = "SP2_HVYSNIPER_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x1FD71980,
                    new SPStat
                        {
                            Name = "SP0_HVYSNIPER_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x0CD729F7,
                    new SPStat
                        {
                            Name = "SP1_HVYSNIPER_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xC478BED6,
                    new SPStat
                        {
                            Name = "SP2_HVYSNIPER_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x4BCE75B0,
                    new SPStat
                        {
                            Name = "SP0_HVYSNIPER_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xFD5795C3,
                    new SPStat
                        {
                            Name = "SP1_HVYSNIPER_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x1BDE6D37,
                    new SPStat
                        {
                            Name = "SP2_HVYSNIPER_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xF36ACFF3,
                    new SPStat
                        {
                            Name = "SP0_HVYSNIPER_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xE4E1044A,
                    new SPStat
                        {
                            Name = "SP1_HVYSNIPER_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xBAF7B850,
                    new SPStat
                        {
                            Name = "SP2_HVYSNIPER_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x4C848F6B,
                    new SPStat
                        {
                            Name = "SP0_GRNLAUNCH_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x0EBD1027,
                    new SPStat
                        {
                            Name = "SP1_GRNLAUNCH_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x01EC6982,
                    new SPStat
                        {
                            Name = "SP2_GRNLAUNCH_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x3880740A,
                    new SPStat
                        {
                            Name = "SP0_GRNLAUNCH_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x29BA4072,
                    new SPStat
                        {
                            Name = "SP1_GRNLAUNCH_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xC8D90E8B,
                    new SPStat
                        {
                            Name = "SP2_GRNLAUNCH_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x624B2AED,
                    new SPStat
                        {
                            Name = "SP0_GRNLAUNCH_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x99116A0B,
                    new SPStat
                        {
                            Name = "SP1_GRNLAUNCH_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x9DEF7FB5,
                    new SPStat
                        {
                            Name = "SP2_GRNLAUNCH_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x4AEA56F2,
                    new SPStat
                        {
                            Name = "SP0_GRNLAUNCH_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xD5427F00,
                    new SPStat
                        {
                            Name = "SP1_GRNLAUNCH_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x76195EF4,
                    new SPStat
                        {
                            Name = "SP2_GRNLAUNCH_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x5B887571,
                    new SPStat
                        {
                            Name = "SP0_GRNLAUNCH_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x9B4343B9,
                    new SPStat
                        {
                            Name = "SP1_GRNLAUNCH_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x9790821F,
                    new SPStat
                        {
                            Name = "SP2_GRNLAUNCH_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x8011D4B7,
                    new SPStat
                        {
                            Name = "SP0_GRNLAUNCH_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xC105DEBE,
                    new SPStat
                        {
                            Name = "SP1_GRNLAUNCH_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x09E60AC0,
                    new SPStat
                        {
                            Name = "SP2_GRNLAUNCH_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xD38D9731,
                    new SPStat
                        {
                            Name = "SP0_RPG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x13E04915,
                    new SPStat
                        {
                            Name = "SP1_RPG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x10255C69,
                    new SPStat
                        {
                            Name = "SP2_RPG_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x74CAF12F,
                    new SPStat
                        {
                            Name = "SP0_RPG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x66304920,
                    new SPStat
                        {
                            Name = "SP1_RPG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x30619508,
                    new SPStat
                        {
                            Name = "SP2_RPG_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xBA9C1D15,
                    new SPStat
                        {
                            Name = "SP0_RPG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xF9F58B19,
                    new SPStat
                        {
                            Name = "SP1_RPG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x7713F940,
                    new SPStat
                        {
                            Name = "SP2_RPG_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xD18E1E81,
                    new SPStat
                        {
                            Name = "SP0_RPG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x012E245F,
                    new SPStat
                        {
                            Name = "SP1_RPG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xB076A5F1,
                    new SPStat
                        {
                            Name = "SP2_RPG_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xD02FB897,
                    new SPStat {Name = "SP0_RPG_AMMO_BOUGHT", Type = SPStatType.Int, Comment = "Ammo bought for weapon"}
                },
                {
                    0xEFF569BF,
                    new SPStat {Name = "SP1_RPG_AMMO_BOUGHT", Type = SPStatType.Int, Comment = "Ammo bought for weapon"}
                },
                {
                    0x7E15933C,
                    new SPStat {Name = "SP2_RPG_AMMO_BOUGHT", Type = SPStatType.Int, Comment = "Ammo bought for weapon"}
                },
                {
                    0xFA4906A8,
                    new SPStat
                        {
                            Name = "SP0_MINIGUNS_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x237248F8,
                    new SPStat
                        {
                            Name = "SP1_MINIGUNS_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xB0AF63A6,
                    new SPStat
                        {
                            Name = "SP2_MINIGUNS_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x9B98B608,
                    new SPStat
                        {
                            Name = "SP0_MINIGUNS_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x8A7A0413,
                    new SPStat
                        {
                            Name = "SP1_MINIGUNS_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x55741FBC,
                    new SPStat
                        {
                            Name = "SP2_MINIGUNS_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x7B459F25,
                    new SPStat
                        {
                            Name = "SP0_MINIGUNS_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x1FFFA0C0,
                    new SPStat
                        {
                            Name = "SP1_MINIGUNS_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xEB2751DB,
                    new SPStat
                        {
                            Name = "SP2_MINIGUNS_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x5C12B127,
                    new SPStat
                        {
                            Name = "SP0_MINIGUNS_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xAD5E33BB,
                    new SPStat
                        {
                            Name = "SP1_MINIGUNS_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x5D610E64,
                    new SPStat
                        {
                            Name = "SP2_MINIGUNS_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x03FE5EF3,
                    new SPStat
                        {
                            Name = "SP0_MINIGUNS_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x6842BD1E,
                    new SPStat
                        {
                            Name = "SP1_MINIGUNS_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x45934081,
                    new SPStat
                        {
                            Name = "SP2_MINIGUNS_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x31A78526,
                    new SPStat
                        {
                            Name = "SP0_MINIGUNS_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xB4C0F3F6,
                    new SPStat
                        {
                            Name = "SP1_MINIGUNS_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xA1CA2909,
                    new SPStat
                        {
                            Name = "SP2_MINIGUNS_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x94DF518F,
                    new SPStat
                        {
                            Name = "SP0_MINIGUNS_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x3E233260,
                    new SPStat
                        {
                            Name = "SP1_MINIGUNS_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xA7A72FC9,
                    new SPStat
                        {
                            Name = "SP2_MINIGUNS_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x52B7A7D6,
                    new SPStat
                        {
                            Name = "SP0_GRENADE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x06CF2676,
                    new SPStat
                        {
                            Name = "SP1_GRENADE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x2424737F,
                    new SPStat
                        {
                            Name = "SP2_GRENADE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xD608424C,
                    new SPStat
                        {
                            Name = "SP0_GRENADE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x3FEBCC46,
                    new SPStat
                        {
                            Name = "SP1_GRENADE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x8923B0FB,
                    new SPStat
                        {
                            Name = "SP2_GRENADE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xE3A8E67E,
                    new SPStat
                        {
                            Name = "SP0_GRENADE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x9B99A65B,
                    new SPStat
                        {
                            Name = "SP1_GRENADE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x4965DD9A,
                    new SPStat
                        {
                            Name = "SP2_GRENADE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x17EE028A,
                    new SPStat
                        {
                            Name = "SP0_GRENADE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xC3D05F66,
                    new SPStat
                        {
                            Name = "SP1_GRENADE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x09CB322E,
                    new SPStat
                        {
                            Name = "SP2_GRENADE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x3DFEC5E6,
                    new SPStat
                        {
                            Name = "SP0_GRENADE_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x354577AE,
                    new SPStat
                        {
                            Name = "SP1_GRENADE_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x0BF1CA0A,
                    new SPStat
                        {
                            Name = "SP2_GRENADE_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x0ADAA963,
                    new SPStat
                        {
                            Name = "SP0_SMKGRENADE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x7641DCE5,
                    new SPStat
                        {
                            Name = "SP1_SMKGRENADE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xC5DC3ADB,
                    new SPStat
                        {
                            Name = "SP2_SMKGRENADE_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xE78F9963,
                    new SPStat
                        {
                            Name = "SP0_SMKGRENADE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xDC6C85A1,
                    new SPStat
                        {
                            Name = "SP1_SMKGRENADE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x25BB27CE,
                    new SPStat
                        {
                            Name = "SP2_SMKGRENADE_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xEB01C20B,
                    new SPStat
                        {
                            Name = "SP0_SMKGRENADE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xBA2C7E49,
                    new SPStat
                        {
                            Name = "SP1_SMKGRENADE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x02AC42FB,
                    new SPStat
                        {
                            Name = "SP2_SMKGRENADE_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xDB851344,
                    new SPStat
                        {
                            Name = "SP0_SMKGRENADE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x0EF3A8B5,
                    new SPStat
                        {
                            Name = "SP1_SMKGRENADE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xD29143BF,
                    new SPStat
                        {
                            Name = "SP2_SMKGRENADE_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x41AE7797,
                    new SPStat
                        {
                            Name = "SP0_SMKGRENADE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x109045B8,
                    new SPStat
                        {
                            Name = "SP1_SMKGRENADE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xE90AADDA,
                    new SPStat
                        {
                            Name = "SP2_SMKGRENADE_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x6AF4D74A,
                    new SPStat
                        {
                            Name = "SP0_SMKGRENADE_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x40E7461B,
                    new SPStat
                        {
                            Name = "SP1_SMKGRENADE_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xBEB76CEF,
                    new SPStat
                        {
                            Name = "SP2_SMKGRENADE_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x6B5E11A2,
                    new SPStat
                        {
                            Name = "SP0_STKYBMB_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x29568E07,
                    new SPStat
                        {
                            Name = "SP1_STKYBMB_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xAC37759C,
                    new SPStat
                        {
                            Name = "SP2_STKYBMB_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x9045F1A6,
                    new SPStat
                        {
                            Name = "SP0_STKYBMB_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xDA42BB3E,
                    new SPStat
                        {
                            Name = "SP1_STKYBMB_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x79F8646F,
                    new SPStat
                        {
                            Name = "SP2_STKYBMB_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x3A138C8E,
                    new SPStat
                        {
                            Name = "SP0_STKYBMB_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xF72C8905,
                    new SPStat
                        {
                            Name = "SP1_STKYBMB_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x09BA6D14,
                    new SPStat
                        {
                            Name = "SP2_STKYBMB_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x7986AFF9,
                    new SPStat
                        {
                            Name = "SP0_STKYBMB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xA320A047,
                    new SPStat
                        {
                            Name = "SP1_STKYBMB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xAC4D3B69,
                    new SPStat
                        {
                            Name = "SP2_STKYBMB_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x469C6802,
                    new SPStat
                        {
                            Name = "SP0_STKYBMB_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x7F32E5D8,
                    new SPStat
                        {
                            Name = "SP1_STKYBMB_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xFF1EA0D3,
                    new SPStat
                        {
                            Name = "SP2_STKYBMB_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0x4E391D7B,
                    new SPStat
                        {
                            Name = "SP0_MOLOTOV_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xC8B004E9,
                    new SPStat
                        {
                            Name = "SP1_MOLOTOV_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x32FAE991,
                    new SPStat
                        {
                            Name = "SP2_MOLOTOV_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x208C95AC,
                    new SPStat
                        {
                            Name = "SP0_MOLOTOV_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xFC0786F2,
                    new SPStat
                        {
                            Name = "SP1_MOLOTOV_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x4D2428B3,
                    new SPStat
                        {
                            Name = "SP2_MOLOTOV_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xF59FFC34,
                    new SPStat
                        {
                            Name = "SP0_MOLOTOV_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x30C7C806,
                    new SPStat
                        {
                            Name = "SP1_MOLOTOV_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xBBB2C0BB,
                    new SPStat
                        {
                            Name = "SP2_MOLOTOV_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x0DA76DD8,
                    new SPStat
                        {
                            Name = "SP0_MOLOTOV_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x41BD7E0A,
                    new SPStat
                        {
                            Name = "SP1_MOLOTOV_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xF2A17C6A,
                    new SPStat
                        {
                            Name = "SP2_MOLOTOV_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x43763C8B,
                    new SPStat
                        {
                            Name = "SP0_MOLOTOV_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xA38E7BDB,
                    new SPStat
                        {
                            Name = "SP1_MOLOTOV_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xC91DF269,
                    new SPStat
                        {
                            Name = "SP2_MOLOTOV_AMMO_BOUGHT",
                            Type = SPStatType.Int,
                            Comment = "Ammo bought for weapon"
                        }
                },
                {
                    0xF6A456B7,
                    new SPStat
                        {
                            Name = "SP0_EXTINGUISHER_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x8073ADAD,
                    new SPStat
                        {
                            Name = "SP1_EXTINGUISHER_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xDE133C98,
                    new SPStat
                        {
                            Name = "SP2_EXTINGUISHER_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x7644BCB8,
                    new SPStat
                        {
                            Name = "SP0_RHINO_ROCKET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x27F58F77,
                    new SPStat
                        {
                            Name = "SP1_RHINO_ROCKET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x8BE2DC68,
                    new SPStat
                        {
                            Name = "SP2_RHINO_ROCKET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x494EA716,
                    new SPStat
                        {
                            Name = "SP0_RHINO_ROCKET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x77C1D1E8,
                    new SPStat
                        {
                            Name = "SP1_RHINO_ROCKET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x716675FE,
                    new SPStat
                        {
                            Name = "SP2_RHINO_ROCKET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x76CF7CB6,
                    new SPStat
                        {
                            Name = "SP0_RHINO_ROCKET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xA5A32591,
                    new SPStat
                        {
                            Name = "SP1_RHINO_ROCKET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x14F7E461,
                    new SPStat
                        {
                            Name = "SP2_RHINO_ROCKET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xDC958982,
                    new SPStat
                        {
                            Name = "SP0_RHINO_ROCKET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xCF40C5B3,
                    new SPStat
                        {
                            Name = "SP1_RHINO_ROCKET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x909297FF,
                    new SPStat
                        {
                            Name = "SP2_RHINO_ROCKET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x0074F7EC,
                    new SPStat
                        {
                            Name = "SP0_RHINO_ROCKET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x9C11CC6C,
                    new SPStat
                        {
                            Name = "SP1_RHINO_ROCKET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x57BC6AFE,
                    new SPStat
                        {
                            Name = "SP2_RHINO_ROCKET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xD6780D19,
                    new SPStat
                        {
                            Name = "SP0_BUZZARD_BULLET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xCA0C8476,
                    new SPStat
                        {
                            Name = "SP1_BUZZARD_BULLET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x0F9B86FA,
                    new SPStat
                        {
                            Name = "SP2_BUZZARD_BULLET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x7EC43618,
                    new SPStat
                        {
                            Name = "SP0_BUZZARD_BULLET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xB5E0310F,
                    new SPStat
                        {
                            Name = "SP1_BUZZARD_BULLET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x47A70E12,
                    new SPStat
                        {
                            Name = "SP2_BUZZARD_BULLET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xBDB73136,
                    new SPStat
                        {
                            Name = "SP0_BUZZARD_BULLET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x0ED9121F,
                    new SPStat
                        {
                            Name = "SP1_BUZZARD_BULLET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xE6D95B3A,
                    new SPStat
                        {
                            Name = "SP2_BUZZARD_BULLET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xC9DE1FAA,
                    new SPStat
                        {
                            Name = "SP0_BUZZARD_BULLET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xBACB6597,
                    new SPStat
                        {
                            Name = "SP1_BUZZARD_BULLET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x2A8BEF20,
                    new SPStat
                        {
                            Name = "SP2_BUZZARD_BULLET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x1A4DBC89,
                    new SPStat
                        {
                            Name = "SP0_BUZZARD_BULLET_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xFDF31DD2,
                    new SPStat
                        {
                            Name = "SP1_BUZZARD_BULLET_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x948D3335,
                    new SPStat
                        {
                            Name = "SP2_BUZZARD_BULLET_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x9A13B454,
                    new SPStat
                        {
                            Name = "SP0_BUZZARD_BULLET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xA86B15F2,
                    new SPStat
                        {
                            Name = "SP1_BUZZARD_BULLET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xEC82929A,
                    new SPStat
                        {
                            Name = "SP2_BUZZARD_BULLET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xEDC9EC2F,
                    new SPStat
                        {
                            Name = "SP0_BUZZARD_ROCKET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x439B5756,
                    new SPStat
                        {
                            Name = "SP1_BUZZARD_ROCKET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x7FB0A311,
                    new SPStat
                        {
                            Name = "SP2_BUZZARD_ROCKET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x512364D1,
                    new SPStat
                        {
                            Name = "SP0_BUZZARD_ROCKET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xC5C09BBD,
                    new SPStat
                        {
                            Name = "SP1_BUZZARD_ROCKET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x3162A85C,
                    new SPStat
                        {
                            Name = "SP2_BUZZARD_ROCKET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x0B5E6A45,
                    new SPStat
                        {
                            Name = "SP0_BUZZARD_ROCKET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xA0FDC2F3,
                    new SPStat
                        {
                            Name = "SP1_BUZZARD_ROCKET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xCB510416,
                    new SPStat
                        {
                            Name = "SP2_BUZZARD_ROCKET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xE64F50CF,
                    new SPStat
                        {
                            Name = "SP0_BUZZARD_ROCKET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x6BA43AE4,
                    new SPStat
                        {
                            Name = "SP1_BUZZARD_ROCKET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x76369BCA,
                    new SPStat
                        {
                            Name = "SP2_BUZZARD_ROCKET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x22BB420A,
                    new SPStat
                        {
                            Name = "SP0_BUZZARD_ROCKET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x411DC220,
                    new SPStat
                        {
                            Name = "SP1_BUZZARD_ROCKET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x2C8260A8,
                    new SPStat
                        {
                            Name = "SP2_BUZZARD_ROCKET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x3E0A4027,
                    new SPStat
                        {
                            Name = "SP0_HUNTER_BULLET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x94B4063C,
                    new SPStat
                        {
                            Name = "SP1_HUNTER_BULLET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x7407F3A3,
                    new SPStat
                        {
                            Name = "SP2_HUNTER_BULLET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x6BEE6D88,
                    new SPStat
                        {
                            Name = "SP0_HUNTER_BULLET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xAC2501F2,
                    new SPStat
                        {
                            Name = "SP1_HUNTER_BULLET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x76CA9DC4,
                    new SPStat
                        {
                            Name = "SP2_HUNTER_BULLET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x9ED5CED5,
                    new SPStat
                        {
                            Name = "SP0_HUNTER_BULLET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x9C28216F,
                    new SPStat
                        {
                            Name = "SP1_HUNTER_BULLET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xB3A1C2DC,
                    new SPStat
                        {
                            Name = "SP2_HUNTER_BULLET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xF938B3F5,
                    new SPStat
                        {
                            Name = "SP0_HUNTER_BULLET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x9BC85215,
                    new SPStat
                        {
                            Name = "SP1_HUNTER_BULLET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x31D800DE,
                    new SPStat
                        {
                            Name = "SP2_HUNTER_BULLET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x9D948B08,
                    new SPStat
                        {
                            Name = "SP0_HUNTER_BULLET_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xBFDA3E12,
                    new SPStat
                        {
                            Name = "SP1_HUNTER_BULLET_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x17588D03,
                    new SPStat
                        {
                            Name = "SP2_HUNTER_BULLET_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x8B66CA2E,
                    new SPStat
                        {
                            Name = "SP0_HUNTER_BULLET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xD355CA4E,
                    new SPStat
                        {
                            Name = "SP1_HUNTER_BULLET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x376148DB,
                    new SPStat
                        {
                            Name = "SP2_HUNTER_BULLET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xB9DE8916,
                    new SPStat
                        {
                            Name = "SP0_HUNTER_ROCKET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xD851EAB7,
                    new SPStat
                        {
                            Name = "SP1_HUNTER_ROCKET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xD757F864,
                    new SPStat
                        {
                            Name = "SP2_HUNTER_ROCKET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x6483B75E,
                    new SPStat
                        {
                            Name = "SP0_HUNTER_ROCKET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x4E250C9A,
                    new SPStat
                        {
                            Name = "SP1_HUNTER_ROCKET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xE1D68195,
                    new SPStat
                        {
                            Name = "SP2_HUNTER_ROCKET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xB6158A8A,
                    new SPStat
                        {
                            Name = "SP0_HUNTER_ROCKET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xE9039743,
                    new SPStat
                        {
                            Name = "SP1_HUNTER_ROCKET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xE4B16EE8,
                    new SPStat
                        {
                            Name = "SP2_HUNTER_ROCKET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xE0D1203E,
                    new SPStat
                        {
                            Name = "SP0_HUNTER_ROCKET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xA781F1D9,
                    new SPStat
                        {
                            Name = "SP1_HUNTER_ROCKET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x84AB080A,
                    new SPStat
                        {
                            Name = "SP2_HUNTER_ROCKET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x2605DFF3,
                    new SPStat
                        {
                            Name = "SP0_HUNTER_ROCKET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x12ACC07E,
                    new SPStat
                        {
                            Name = "SP1_HUNTER_ROCKET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xD820D556,
                    new SPStat
                        {
                            Name = "SP2_HUNTER_ROCKET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xF2BF077D,
                    new SPStat
                        {
                            Name = "SP0_LAZER_BULLET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x46870A0D,
                    new SPStat
                        {
                            Name = "SP1_LAZER_BULLET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xC5665361,
                    new SPStat
                        {
                            Name = "SP2_LAZER_BULLET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xBFC28A96,
                    new SPStat
                        {
                            Name = "SP0_LAZER_BULLET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x93B9AE25,
                    new SPStat
                        {
                            Name = "SP1_LAZER_BULLET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x663E3A65,
                    new SPStat
                        {
                            Name = "SP2_LAZER_BULLET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x3A45087E,
                    new SPStat
                        {
                            Name = "SP0_LAZER_BULLET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xFA2B52EC,
                    new SPStat
                        {
                            Name = "SP1_LAZER_BULLET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x3DF3639C,
                    new SPStat
                        {
                            Name = "SP2_LAZER_BULLET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x53FAFE10,
                    new SPStat
                        {
                            Name = "SP0_LAZER_BULLET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x53D631CA,
                    new SPStat
                        {
                            Name = "SP1_LAZER_BULLET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x04BC51B3,
                    new SPStat
                        {
                            Name = "SP2_LAZER_BULLET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xEAF0B1E9,
                    new SPStat
                        {
                            Name = "SP0_LAZER_BULLET_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x10C4869E,
                    new SPStat
                        {
                            Name = "SP1_LAZER_BULLET_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0x785F62DA,
                    new SPStat
                        {
                            Name = "SP2_LAZER_BULLET_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by headshot with weapon"
                        }
                },
                {
                    0xD14F581E,
                    new SPStat
                        {
                            Name = "SP0_LAZER_BULLET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x84D60989,
                    new SPStat
                        {
                            Name = "SP1_LAZER_BULLET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xBA86DF3C,
                    new SPStat
                        {
                            Name = "SP2_LAZER_BULLET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x379027AC,
                    new SPStat
                        {
                            Name = "SP0_LAZER_ROCKET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x46A5880D,
                    new SPStat
                        {
                            Name = "SP1_LAZER_ROCKET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xD076D496,
                    new SPStat
                        {
                            Name = "SP2_LAZER_ROCKET_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xD433EAAB,
                    new SPStat
                        {
                            Name = "SP0_LAZER_ROCKET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x3EE45324,
                    new SPStat
                        {
                            Name = "SP1_LAZER_ROCKET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x99BAD2F8,
                    new SPStat
                        {
                            Name = "SP2_LAZER_ROCKET_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xD4E51CEC,
                    new SPStat
                        {
                            Name = "SP0_LAZER_ROCKET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xB0321DD5,
                    new SPStat
                        {
                            Name = "SP1_LAZER_ROCKET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xC19E5E7E,
                    new SPStat
                        {
                            Name = "SP2_LAZER_ROCKET_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0xE7038BE7,
                    new SPStat
                        {
                            Name = "SP0_LAZER_ROCKET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x198DE993,
                    new SPStat
                        {
                            Name = "SP1_LAZER_ROCKET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x5F7654D0,
                    new SPStat
                        {
                            Name = "SP2_LAZER_ROCKET_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x2ED47370,
                    new SPStat
                        {
                            Name = "SP0_LAZER_ROCKET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xC9F67084,
                    new SPStat
                        {
                            Name = "SP1_LAZER_ROCKET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x56376543,
                    new SPStat
                        {
                            Name = "SP2_LAZER_ROCKET_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x3617B23C,
                    new SPStat
                        {
                            Name = "SP0_SPPLAYER_LASER_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x3E498E27,
                    new SPStat
                        {
                            Name = "SP1_SPPLAYER_LASER_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xF9D91B44,
                    new SPStat
                        {
                            Name = "SP2_SPPLAYER_LASER_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x3FD809ED,
                    new SPStat
                        {
                            Name = "SP0_SPPLAYER_LASER_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x7F01B35E,
                    new SPStat
                        {
                            Name = "SP1_SPPLAYER_LASER_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x46D7D9B9,
                    new SPStat
                        {
                            Name = "SP2_SPPLAYER_LASER_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xFE19BD18,
                    new SPStat
                        {
                            Name = "SP0_SPPLAYER_LASER_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x82B2A550,
                    new SPStat
                        {
                            Name = "SP1_SPPLAYER_LASER_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x558F884D,
                    new SPStat
                        {
                            Name = "SP2_SPPLAYER_LASER_SHOTS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael fired weapon"
                        }
                },
                {
                    0x0DA888BD,
                    new SPStat
                        {
                            Name = "SP0_SPPLAYER_LASER_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xC9CD66E5,
                    new SPStat
                        {
                            Name = "SP1_SPPLAYER_LASER_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0xF0624DC6,
                    new SPStat
                        {
                            Name = "SP2_SPPLAYER_LASER_HITS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael hit with weapon"
                        }
                },
                {
                    0x3314551F,
                    new SPStat
                        {
                            Name = "SP0_SPPLAYER_LASER_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x8E38772B,
                    new SPStat
                        {
                            Name = "SP1_SPPLAYER_LASER_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0x7CB41D9A,
                    new SPStat
                        {
                            Name = "SP2_SPPLAYER_LASER_HELDTIME",
                            Type = SPStatType.U32,
                            Comment = "Time Michael held weapon (MS)"
                        }
                },
                {
                    0xE93EC9B0,
                    new SPStat
                        {
                            Name = "SP0_WATER_CANNON_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x155C17AD,
                    new SPStat
                        {
                            Name = "SP1_WATER_CANNON_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0xDEB9718F,
                    new SPStat
                        {
                            Name = "SP2_WATER_CANNON_KILLS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed with weapon"
                        }
                },
                {
                    0x58B5EAF8,
                    new SPStat
                        {
                            Name = "SP0_WATER_CANNON_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0xF94A3290,
                    new SPStat
                        {
                            Name = "SP1_WATER_CANNON_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x8FCDF315,
                    new SPStat
                        {
                            Name = "SP2_WATER_CANNON_DEATHS",
                            Type = SPStatType.Int,
                            Comment = "Number of times Michael killed by weapon"
                        }
                },
                {
                    0x537025B6,
                    new SPStat
                        {
                            Name = "CAR_MOD_APP_USED",
                            Type = SPStatType.Bool,
                            Comment = "If the player has used the car app."
                        }
                },
                {
                    0xD6D6EBDA,
                    new SPStat
                        {
                            Name = "CHOP_APP_USED",
                            Type = SPStatType.Bool,
                            Comment = "If the player has used the dog app."
                        }
                },
                {
                    0x86267624,
                    new SPStat
                        {
                            Name = "NUM_HIDDEN_PACKAGES_0",
                            Type = SPStatType.Int,
                            Comment = "Hidden packages collected of Letter Scraps."
                        }
                },
                {
                    0x052AF42B,
                    new SPStat
                        {
                            Name = "NUM_HIDDEN_PACKAGES_1",
                            Type = SPStatType.Int,
                            Comment = "Hidden packages collected of Spaceship Parts."
                        }
                },
                {
                    0xFAF0DFB7,
                    new SPStat
                        {
                            Name = "NUM_HIDDEN_PACKAGES_2",
                            Type = SPStatType.Int,
                            Comment = "Hidden packages collected of Epsilon Tract ."
                        }
                },
                {
                    0xE99D3D10,
                    new SPStat
                        {
                            Name = "NUM_HIDDEN_PACKAGES_3",
                            Type = SPStatType.Int,
                            Comment = "Hidden packages collected of Sonar Collections Marina Property ."
                        }
                },
                {
                    0x3B6EE0B2,
                    new SPStat
                        {
                            Name = "NUM_HIDDEN_PACKAGES_4",
                            Type = SPStatType.Int,
                            Comment = "Hidden packages collected of Submarine Wreckage."
                        }
                },
                {
                    0x1C0F010C,
                    new SPStat
                        {
                            Name = "NUM_DESTROYED_SIGNS_0",
                            Type = SPStatType.Int,
                            Comment = "Number of signs taken out."
                        }
                },
                {
                    0xE6FB1EBC,
                    new SPStat
                        {
                            Name = "NUM_MISSIONS_AVAILABLE",
                            Type = SPStatType.Int,
                            Comment = "Number of missions that are available to play in entire game."
                        }
                },
                {
                    0xC115F6B0,
                    new SPStat
                        {
                            Name = "NUM_MISSIONS_COMPLETED",
                            Type = SPStatType.Int,
                            Comment = "Number of missions that have been completed so far."
                        }
                },
                {
                    0x2053B0A0,
                    new SPStat
                        {
                            Name = "NUM_MINIGAMES_AVAILABLE",
                            Type = SPStatType.Int,
                            Comment = "Number of minigames that are available to play in entire game."
                        }
                },
                {
                    0x4F5B53BA,
                    new SPStat
                        {
                            Name = "NUM_MINIGAMES_COMPLETED",
                            Type = SPStatType.Int,
                            Comment = "Number of minigames that have been completed so far."
                        }
                },
                {
                    0x214A068C,
                    new SPStat
                        {
                            Name = "NUM_ODDJOBS_AVAILABLE",
                            Type = SPStatType.Int,
                            Comment = "Number of oddjobs that are available to play in entire game."
                        }
                },
                {
                    0x5D0E88A9,
                    new SPStat
                        {
                            Name = "NUM_ODDJOBS_COMPLETED",
                            Type = SPStatType.Int,
                            Comment = "Number of oddjobs that have been completed so far."
                        }
                },
                {
                    0xFEC8B0D8,
                    new SPStat
                        {
                            Name = "NUM_RNDPEOPLE_AVAILABLE",
                            Type = SPStatType.Int,
                            Comment = "Number of random people elements that are available to play in entire game."
                        }
                },
                {
                    0xCD2D71F9,
                    new SPStat
                        {
                            Name = "NUM_RNDPEOPLE_COMPLETED",
                            Type = SPStatType.Int,
                            Comment = "Number of random people elements that have been completed so far."
                        }
                },
                {
                    0xF32498EA,
                    new SPStat
                        {
                            Name = "NUM_RNDEVENTS_AVAILABLE",
                            Type = SPStatType.Int,
                            Comment = "Number of random events that are available to play in entire game."
                        }
                },
                {
                    0x817B5488,
                    new SPStat
                        {
                            Name = "NUM_RNDEVENTS_COMPLETED",
                            Type = SPStatType.Int,
                            Comment = "Number of random events that have been completed so far."
                        }
                },
                {
                    0x83A486E8,
                    new SPStat
                        {
                            Name = "NUM_MISC_AVAILABLE",
                            Type = SPStatType.Int,
                            Comment = "Number of misc elements that are available to play in entire game."
                        }
                },
                {
                    0xF82362EF,
                    new SPStat
                        {
                            Name = "NUM_MISC_COMPLETED",
                            Type = SPStatType.Int,
                            Comment = "Number of misc elements that have been completed so far."
                        }
                },
                {
                    0xC1EF6530,
                    new SPStat {Name = "USJS_FOUND", Type = SPStatType.Int, Comment = "Number of stunt jumps tried"}
                },
                {
                    0xDCFE8B7F,
                    new SPStat
                        {
                            Name = "USJS_FOUND_MASK",
                            Type = SPStatType.U64,
                            Comment = "Mask of all stunt jumps tried"
                        }
                },
                {
                    0x861ADACB,
                    new SPStat
                        {
                            Name = "USJS_COMPLETED",
                            Type = SPStatType.Int,
                            Comment = "Number of stunt jumps completed"
                        }
                },
                {
                    0xAAE2A953,
                    new SPStat
                        {
                            Name = "USJS_COMPLETED_MASK",
                            Type = SPStatType.U64,
                            Comment = "Mask of all stunt jumps completed"
                        }
                },
                {
                    0x1B9AC6D6,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_agency_heist1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x2A8D64BB,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_agency_heist2",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xDA97C734,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_agency_prep1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xD52DFE10,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_agency_heist3A",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xA3791AA7,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_agency_heist3B",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x772C9FBC,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_armenian1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x84AF3AC1,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_armenian2",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x1E5DEE20,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_armenian3",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xCF53D283,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_Assassin_Valet",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xA196D2AB,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_Assassin_Multi",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xED8EA282,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_Assassin_Hooker",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x3B70202D,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_Assassin_Bus",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x665F415A,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_carsteal1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xE5E9C071,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_carsteal2",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x9CAD2DF5,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_carsteal3",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xC16B7771,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_carsteal4",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x2B475E38,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_chinese1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x4D72A28E,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_chinese2",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xDBABC1D1,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_docks_setup",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x0D16E814,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_docks_prep1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x31CA9F65,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_docks_prep2B",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xB7D08BD0,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_docks_heistA",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xC93F2EAD,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_docks_heistB",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x9A0D4389,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_exile1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x8E002B6F,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_exile2",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x73AEF6CD,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_exile3",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xB83F2CB3,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_family1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x8AC7D1C5,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_family2",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xD489E548,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_family3",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xA75B8AEC,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_family4",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x9C6D750C,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_family5",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x6F3B1AA8,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_family6",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xEF6F95E6,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_finale_intro",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x1251B212,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_finaleA",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x04A816BF,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_finaleB",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xE9F7B8AD,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_finaleC1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x3BBDDC38,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_finaleC2",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x97BCE4EC,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_finale_credits",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xAD87A46A,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_finale_heist1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x49B47F95,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_finale_heist2_intro",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x4B551B0E,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_finale_heist_prepA",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x7DCAFFF9,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_finale_heist_prepB",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x6DC85FF4,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_finale_heist_prepC",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x16D4B20E,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_finale_heist_prepD",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x06A92C01,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_finale_heist2A",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xF86F8F8E,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_finale_heist2B",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x24FA438B,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_fbi1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x36B6E704,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_fbi2",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x94DE2355,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_fbi3",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x7895A333,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_fbi4_intro",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x479C3B08,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_fbi4_prep1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xC8F2BDB3,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_fbi4_prep2",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x1B2A6221,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_fbi4_prep3",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xED7E86CA,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_fbi4_prep4",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xFF54AA76,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_fbi4_prep5",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xA702C79E,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_fbi4",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x4EC854CD,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_fbi5A",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xB46CF5E9,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_franklin0",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x85C6989D,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_franklin1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xB457F5BB,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_franklin2",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x8C556773,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_jewelry_setup1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x70A6696A,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_jewelry_prep1A",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xAE8CE67E,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_jewelry_prep2A",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x5F6546E8,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_jewelry_prep1B",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xB5A3C9F5,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_jewelry_heist",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x7A4A0E68,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_lamar1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x3E1E3F01,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_lester1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x3B7D496A,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_martin1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x9FC983F9,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_michael1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xFD513F07,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_michael2",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x4B0E5A80,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_michael3",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x18DBF61C,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_michael4",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x761BC772,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_me_amanda1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xAFD5F154,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_me_jimmy1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xEF4AFF85,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_me_tracey1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x38532C31,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_prologue1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x1C8ECEFF,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_rural_bank_setup",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xFF714154,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_rural_bank_prep1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x0ED2DC1E,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_rural_bank_heist",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x8E7A66E7,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_drf1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x7F8C490B,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_drf2",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x71E2ADB8,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_drf3",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x637190D6,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_drf4",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xD49DF331,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_drf5",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xB6E708DA,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_solomon1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0xC4AD2466,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_solomon2",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x8C0D331F,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_solomon3",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x50A52507,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_trevor1",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x7B1C79F5,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_trevor2",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x2F55E269,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_trevor3",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {
                    0x219B46F4,
                    new SPStat
                        {
                            Name = "MISS_SWITCH_trevor4",
                            Type = SPStatType.Int,
                            Comment = "Mission stat - switch count"
                        }
                },
                {0x03F87738, new SPStat {Name = "APP_ENGINE", Type = SPStatType.Bool, Comment = "Engine Power"}},
                {
                    0x0DAB94D4,
                    new SPStat {Name = "APP_TURBO", Type = SPStatType.Bool, Comment = "Turbo and air-intake sfx "}
                },
                {
                    0x9426471B,
                    new SPStat
                        {
                            Name = "APP_EXAUST",
                            Type = SPStatType.Bool,
                            Comment = "Make that sound gutsier and noticeably different from an engine upgrade"
                        }
                },
                {
                    0x3C4084BB,
                    new SPStat
                        {
                            Name = "APP_BRAKES",
                            Type = SPStatType.Bool,
                            Comment = "Can do in 5% or 10% increment without any issues"
                        }
                },
                {
                    0xF599E066,
                    new SPStat
                        {
                            Name = "APP_BODY_KITS",
                            Type = SPStatType.Int,
                            Comment = "bumper additions, spoilers, side skirts"
                        }
                },
                {
                    0x16922D28,
                    new SPStat
                        {
                            Name = "APP_WHEELS_STYLE",
                            Type = SPStatType.Int,
                            Comment = "Different style wheels to fit with style of car"
                        }
                },
                {
                    0xDEC12ED5,
                    new SPStat
                        {
                            Name = "APP_WINDOW_TINTS_AMOUNT",
                            Type = SPStatType.Int,
                            Comment = "window tint amount changes"
                        }
                },
                {
                    0xCA125E40,
                    new SPStat
                        {
                            Name = "APP_WINDOW_TINTS_COLOUR",
                            Type = SPStatType.Int,
                            Comment = "window tint colour changes"
                        }
                },
                {
                    0xAB0CD36B,
                    new SPStat
                        {
                            Name = "APP_BODY_COLOUR_1",
                            Type = SPStatType.Int,
                            Comment = "Some cars may have two colours"
                        }
                },
                {
                    0xBFD2FCF7,
                    new SPStat
                        {
                            Name = "APP_BODY_COLOUR_2",
                            Type = SPStatType.Int,
                            Comment = "Some cars may have two colours"
                        }
                },
                {
                    0xE12A2A0A,
                    new SPStat {Name = "APP_ARMOUR", Type = SPStatType.Bool, Comment = "higher vehicle strength"}
                },
                {
                    0x67ACEF8E,
                    new SPStat
                        {
                            Name = "APP_HEADLIGHTS",
                            Type = SPStatType.Bool,
                            Comment = "upgrade his headlights to be blue modern style projector lights"
                        }
                },
                {0x4D7F9852, new SPStat {Name = "APP_HORN", Type = SPStatType.Int, Comment = "in-game horn sfx"}},
                {
                    0xDE290EB3,
                    new SPStat {Name = "APP_RADIO_SUBWOOFER", Type = SPStatType.Bool, Comment = "subwoofer on/off"}
                },
                {
                    0x9B36434B,
                    new SPStat {Name = "APP_HYDRAULICA", Type = SPStatType.Bool, Comment = "Not for all cars on/off"}
                },
                {0xC9EF0B97, new SPStat {Name = "APP_NITROUS", Type = SPStatType.Bool, Comment = "Car Nitrous"}},
                {
                    0x657ECBB3,
                    new SPStat
                        {
                            Name = "APP_TYRE_SMOKE",
                            Type = SPStatType.Bool,
                            Comment = "Coloured smoke from tyres on/off"
                        }
                },
                {
                    0x2E08348D,
                    new SPStat
                        {
                            Name = "APP_TYRE_SMOKE_COLOUR",
                            Type = SPStatType.Int,
                            Comment = "Coloured smoke from tyres"
                        }
                },
                {
                    0x2ADC4F3E,
                    new SPStat
                        {
                            Name = "MG_RANGE_PIST1_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in Pistol Challenge 1"
                        }
                },
                {
                    0x43E3E4D5,
                    new SPStat
                        {
                            Name = "MG_RANGE_PIST2_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in Pistol Challenge 2"
                        }
                },
                {
                    0x0203ACA9,
                    new SPStat
                        {
                            Name = "MG_RANGE_PIST3_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in Pistol Challenge 3"
                        }
                },
                {
                    0xEAC83B40,
                    new SPStat
                        {
                            Name = "MG_RANGE_SMG1_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in SMG Challenge 1"
                        }
                },
                {
                    0xF92DDD6B,
                    new SPStat
                        {
                            Name = "MG_RANGE_SMG2_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in SMG Challenge 2"
                        }
                },
                {
                    0x080BA759,
                    new SPStat
                        {
                            Name = "MG_RANGE_SMG3_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in SMG Challenge 3"
                        }
                },
                {
                    0xEFDD62B7,
                    new SPStat
                        {
                            Name = "MG_RANGE_AR1_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in Assault Rifle Challenge 1"
                        }
                },
                {
                    0xAD46872B,
                    new SPStat
                        {
                            Name = "MG_RANGE_AR2_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in Assault Rifle Challenge 2"
                        }
                },
                {
                    0x245672FB,
                    new SPStat
                        {
                            Name = "MG_RANGE_AR3_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in Assault Rifle Challenge 3"
                        }
                },
                {
                    0x09CFB5FC,
                    new SPStat
                        {
                            Name = "MG_RANGE_SG1_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in Shotgun Challenge 1"
                        }
                },
                {
                    0x5D9EF586,
                    new SPStat
                        {
                            Name = "MG_RANGE_SG2_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in Shotgun Challenge 2"
                        }
                },
                {
                    0x14ADCB6B,
                    new SPStat
                        {
                            Name = "MG_RANGE_SG3_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in Shotgun Challenge 3"
                        }
                },
                {
                    0x96F1BC3B,
                    new SPStat
                        {
                            Name = "MG_RANGE_LMG1_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in LMG Challenge 1"
                        }
                },
                {
                    0x8EF64925,
                    new SPStat
                        {
                            Name = "MG_RANGE_LMG2_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in LMG Challenge 2"
                        }
                },
                {
                    0x11347D7E,
                    new SPStat
                        {
                            Name = "MG_RANGE_LMG3_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in LMG Challenge 3"
                        }
                },
                {
                    0x97F52EDD,
                    new SPStat
                        {
                            Name = "MG_RANGE_HVY1_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in Hvy Challenge 1"
                        }
                },
                {
                    0x810BFB36,
                    new SPStat
                        {
                            Name = "MG_RANGE_HVY2_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in Hvy Challenge 2"
                        }
                },
                {
                    0x02C78EC9,
                    new SPStat
                        {
                            Name = "MG_RANGE_HVY3_HISCORE",
                            Type = SPStatType.Int,
                            Comment = "Best score achieved in Hvy Challenge 3"
                        }
                },
                {
                    0x0C801B0A,
                    new SPStat
                        {
                            Name = "FL_CO_ARM1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission ARMENIAN_1"
                        }
                },
                {
                    0x195634B6,
                    new SPStat
                        {
                            Name = "FL_CO_ARM2",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission ARMENIAN_2"
                        }
                },
                {
                    0x96B92F7A,
                    new SPStat
                        {
                            Name = "FL_CO_ARM3",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission ARMENIAN_3"
                        }
                },
                {
                    0xCFA68795,
                    new SPStat
                        {
                            Name = "FL_CO_ASS1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission ASSASSIN_1"
                        }
                },
                {
                    0x0140EAD1,
                    new SPStat
                        {
                            Name = "FL_CO_ASS2",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission ASSASSIN_2"
                        }
                },
                {
                    0xF2D94E02,
                    new SPStat
                        {
                            Name = "FL_CO_ASS3",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission ASSASSIN_3"
                        }
                },
                {
                    0xDDD323F6,
                    new SPStat
                        {
                            Name = "FL_CO_ASS4",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission ASSASSIN_4"
                        }
                },
                {
                    0xCF948779,
                    new SPStat
                        {
                            Name = "FL_CO_ASS5",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission ASSASSIN_5"
                        }
                },
                {
                    0xE2046CCE,
                    new SPStat
                        {
                            Name = "FL_CO_CAR1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission CARSTEAL_1"
                        }
                },
                {
                    0xC74B375C,
                    new SPStat
                        {
                            Name = "FL_CO_CAR2",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission CARSTEAL_2"
                        }
                },
                {
                    0x7DB52445,
                    new SPStat
                        {
                            Name = "FL_CO_CAR3",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission CARSTEAL_3"
                        }
                },
                {
                    0x706F09B9,
                    new SPStat
                        {
                            Name = "FL_CO_CAR4",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission CARSTEAL_4"
                        }
                },
                {
                    0x71FB374E,
                    new SPStat
                        {
                            Name = "FL_CO_CHN1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission CHINESE_1"
                        }
                },
                {
                    0x5F2E91B5,
                    new SPStat
                        {
                            Name = "FL_CO_CHN2",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission CHINESE_2"
                        }
                },
                {
                    0x98749F31,
                    new SPStat
                        {
                            Name = "FL_CO_EXL1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission EXILE_1"
                        }
                },
                {
                    0x861FFA84,
                    new SPStat
                        {
                            Name = "FL_CO_EXL2",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission EXILE_2"
                        }
                },
                {
                    0xB403564E,
                    new SPStat
                        {
                            Name = "FL_CO_EXL3",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission EXILE_3"
                        }
                },
                {
                    0xDD40F0EE,
                    new SPStat
                        {
                            Name = "FL_CO_FAM1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FAMILY_1"
                        }
                },
                {
                    0x6F2394B5,
                    new SPStat
                        {
                            Name = "FL_CO_FAM2",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FAMILY_2"
                        }
                },
                {
                    0xEC1B8EAF,
                    new SPStat
                        {
                            Name = "FL_CO_FAM3",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FAMILY_3"
                        }
                },
                {
                    0x59E0EA38,
                    new SPStat
                        {
                            Name = "FL_CO_FAM4",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FAMILY_4"
                        }
                },
                {
                    0x4FB7D5E6,
                    new SPStat
                        {
                            Name = "FL_CO_FAM5",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FAMILY_5"
                        }
                },
                {
                    0x3D573125,
                    new SPStat
                        {
                            Name = "FL_CO_FAM6",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FAMILY_6"
                        }
                },
                {
                    0x28EF8E32,
                    new SPStat
                        {
                            Name = "FL_CO_FINA",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FINALE_A"
                        }
                },
                {
                    0x372E2AAF,
                    new SPStat
                        {
                            Name = "FL_CO_FINB",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FINALE_B"
                        }
                },
                {
                    0xE0F723B7,
                    new SPStat
                        {
                            Name = "FL_CO_FINC2",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FINALE_C2"
                        }
                },
                {
                    0xB177B544,
                    new SPStat
                        {
                            Name = "FL_CO_FIB1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FBI_1"
                        }
                },
                {
                    0x9F4210D9,
                    new SPStat
                        {
                            Name = "FL_CO_FIB2",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FBI_2"
                        }
                },
                {
                    0xFADEC815,
                    new SPStat
                        {
                            Name = "FL_CO_FIB3",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FBI_3"
                        }
                },
                {
                    0x42678835,
                    new SPStat
                        {
                            Name = "FL_CO_FB4P1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FBI_4_PREP_1"
                        }
                },
                {
                    0x54A12CA8,
                    new SPStat
                        {
                            Name = "FL_CO_FB4P2",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FBI_4_PREP_2"
                        }
                },
                {
                    0x9A35B7D0,
                    new SPStat
                        {
                            Name = "FL_CO_FB4P3",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FBI_4_PREP_3"
                        }
                },
                {
                    0x73866A72,
                    new SPStat
                        {
                            Name = "FL_CO_FB4P4",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FBI_4_PREP_4"
                        }
                },
                {
                    0xBB757A4F,
                    new SPStat
                        {
                            Name = "FL_CO_FB4P5",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FBI_4_PREP_5"
                        }
                },
                {
                    0x32963783,
                    new SPStat
                        {
                            Name = "FL_CO_FIB4",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FBI_4"
                        }
                },
                {
                    0xD85B030E,
                    new SPStat
                        {
                            Name = "FL_CO_FIB5",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FBI_5"
                        }
                },
                {
                    0x52FBFCCA,
                    new SPStat
                        {
                            Name = "FL_CO_FRA0",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FRANKLIN_0"
                        }
                },
                {
                    0x40B15835,
                    new SPStat
                        {
                            Name = "FL_CO_FRA1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FRANKLIN_1"
                        }
                },
                {
                    0x0BF56ECA,
                    new SPStat
                        {
                            Name = "FL_CO_FRA2",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission FRANKLIN_2"
                        }
                },
                {
                    0x9D6944BC,
                    new SPStat
                        {
                            Name = "FL_CO_LM1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission LAMAR"
                        }
                },
                {
                    0xF897EFC4,
                    new SPStat
                        {
                            Name = "FL_CO_LS1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission LESTER_1"
                        }
                },
                {
                    0x560583E1,
                    new SPStat
                        {
                            Name = "FL_CO_MAR1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission MARTIN_1"
                        }
                },
                {
                    0xB3D569AB,
                    new SPStat
                        {
                            Name = "FL_CO_MIC1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission MICHAEL_1"
                        }
                },
                {
                    0x81300461,
                    new SPStat
                        {
                            Name = "FL_CO_MIC2",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission MICHAEL_2"
                        }
                },
                {
                    0xD54EACBD,
                    new SPStat
                        {
                            Name = "FL_CO_MIC3",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission MICHAEL_3"
                        }
                },
                {
                    0xA3DCC9BA,
                    new SPStat
                        {
                            Name = "FL_CO_MIC4",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission MICHAEL_4"
                        }
                },
                {
                    0x6E73EDBC,
                    new SPStat
                        {
                            Name = "FL_CO_MEA1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission ME_AMANDA"
                        }
                },
                {
                    0x1EDC280A,
                    new SPStat
                        {
                            Name = "FL_CO_MEJ1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission ME_JIMMY"
                        }
                },
                {
                    0x14F8ABFB,
                    new SPStat
                        {
                            Name = "FL_CO_MET1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission ME_TRACEY"
                        }
                },
                {
                    0x21AEFD38,
                    new SPStat
                        {
                            Name = "FL_CO_PRO1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission PROLOGUE"
                        }
                },
                {
                    0x530A1A76,
                    new SPStat
                        {
                            Name = "FL_CO_DRF1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission SHRINK_1"
                        }
                },
                {
                    0xA5E9C034,
                    new SPStat
                        {
                            Name = "FL_CO_DRF2",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission SHRINK_2"
                        }
                },
                {
                    0xBC9EED9E,
                    new SPStat
                        {
                            Name = "FL_CO_DRF3",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission SHRINK_3"
                        }
                },
                {
                    0x1C162C8F,
                    new SPStat
                        {
                            Name = "FL_CO_DRF4",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission SHRINK_4"
                        }
                },
                {
                    0x69F94858,
                    new SPStat
                        {
                            Name = "FL_CO_DRF5",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission SHRINK_5"
                        }
                },
                {
                    0x3C6720DF,
                    new SPStat
                        {
                            Name = "FL_CO_SOL1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission SOLOMON_1"
                        }
                },
                {
                    0x4D79C304,
                    new SPStat
                        {
                            Name = "FL_CO_SOL2",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission SOLOMON_2"
                        }
                },
                {
                    0x29847B1A,
                    new SPStat
                        {
                            Name = "FL_CO_SOL3",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission SOLOMON_3"
                        }
                },
                {
                    0x8C39F0F5,
                    new SPStat
                        {
                            Name = "FL_CO_TRV1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission TREVOR_1"
                        }
                },
                {
                    0x1DF79472,
                    new SPStat
                        {
                            Name = "FL_CO_TRV2",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission TREVOR_2"
                        }
                },
                {
                    0x3BA6CFD0,
                    new SPStat
                        {
                            Name = "FL_CO_TRV3",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission TREVOR_3"
                        }
                },
                {
                    0x4543E30A,
                    new SPStat
                        {
                            Name = "FL_CO_TRV4",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission TREVOR_4"
                        }
                },
                {
                    0xDA36DFD8,
                    new SPStat
                        {
                            Name = "FL_CO_AH1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_AGENCY_1"
                        }
                },
                {
                    0x9B146194,
                    new SPStat
                        {
                            Name = "FL_CO_AH2",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_AGENCY_2"
                        }
                },
                {
                    0x1DB8D9DB,
                    new SPStat
                        {
                            Name = "FL_CO_AHP1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_AGENCY_PREP1"
                        }
                },
                {
                    0xB6C140E2,
                    new SPStat
                        {
                            Name = "FL_CO_AH3a",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_AGENCY_3A"
                        }
                },
                {
                    0xC87EE45D,
                    new SPStat
                        {
                            Name = "FL_CO_AH3b",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_AGENCY_3B"
                        }
                },
                {
                    0x7A67D643,
                    new SPStat
                        {
                            Name = "FL_CO_DH1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_DOCKS_1"
                        }
                },
                {
                    0x62155F5A,
                    new SPStat
                        {
                            Name = "FL_CO_DHP1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_DOCKS_PREP_1"
                        }
                },
                {
                    0xC9934963,
                    new SPStat
                        {
                            Name = "FL_CO_DHP2B",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_DOCKS_PREP_2B"
                        }
                },
                {
                    0x053F6A81,
                    new SPStat
                        {
                            Name = "FL_CO_DH2A",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_DOCKS_2A"
                        }
                },
                {
                    0x17190E34,
                    new SPStat
                        {
                            Name = "FL_CO_DH2B",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_DOCKS_2B"
                        }
                },
                {
                    0xCA69CB5F,
                    new SPStat
                        {
                            Name = "FL_CO_FH1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_FINALE_1"
                        }
                },
                {
                    0xC4E2B744,
                    new SPStat
                        {
                            Name = "FL_CO_FH2I",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_FINALE_2_INTRO"
                        }
                },
                {
                    0x8C043035,
                    new SPStat
                        {
                            Name = "FL_CO_FHPRA",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_FINALE_PREP_A"
                        }
                },
                {
                    0x9DB1D390,
                    new SPStat
                        {
                            Name = "FL_CO_FHPRB",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_FINALE_PREP_B"
                        }
                },
                {
                    0x9402C30E,
                    new SPStat
                        {
                            Name = "FL_CO_FHPC1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_FINALE_PREP_C1"
                        }
                },
                {
                    0xAE267751,
                    new SPStat
                        {
                            Name = "FL_CO_FHPC2",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_FINALE_PREP_C2"
                        }
                },
                {
                    0x9EFD58FF,
                    new SPStat
                        {
                            Name = "FL_CO_FHPC3",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_FINALE_PREP_C3"
                        }
                },
                {
                    0x7DA6D11E,
                    new SPStat
                        {
                            Name = "FL_CO_FHPD",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_FINALE_PREP_D"
                        }
                },
                {
                    0x1D62E837,
                    new SPStat
                        {
                            Name = "FL_CO_FH2A",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_FINALE_2A"
                        }
                },
                {
                    0x107C4E6A,
                    new SPStat
                        {
                            Name = "FL_CO_FH2B",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_FINALE_2B"
                        }
                },
                {
                    0xAB072864,
                    new SPStat
                        {
                            Name = "FL_CO_JH1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_JEWELRY_1"
                        }
                },
                {
                    0x9BAE01D3,
                    new SPStat
                        {
                            Name = "FL_CO_JHP1A",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_JEWELRY_P1A"
                        }
                },
                {
                    0x68C42D4C,
                    new SPStat
                        {
                            Name = "FL_CO_JHP2A",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_JEWELRY_P2A"
                        }
                },
                {
                    0xE7B899BB,
                    new SPStat
                        {
                            Name = "FL_CO_JHP1B",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_JEWELRY_P1B"
                        }
                },
                {
                    0x43149876,
                    new SPStat
                        {
                            Name = "FL_CO_JH2A",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_JEWELRY_2"
                        }
                },
                {
                    0x5953C4F4,
                    new SPStat
                        {
                            Name = "FL_CO_JH2B",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_JEWELRY_2"
                        }
                },
                {
                    0x656CFF63,
                    new SPStat
                        {
                            Name = "FL_CO_RH1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_RURAL_1"
                        }
                },
                {
                    0x05998C98,
                    new SPStat
                        {
                            Name = "FL_CO_RHP1",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_RURAL_PREP_1"
                        }
                },
                {
                    0x53225ACE,
                    new SPStat
                        {
                            Name = "FL_CO_RH2",
                            Type = SPStatType.Int,
                            Comment = "Flow completion order stat for mission H_RURAL_2"
                        }
                },
                {0x24258C94, new SPStat {Name = "SM_BRVECDESBFA", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x065D5E88, new SPStat {Name = "SM_BRVECDESBRU", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xC43912C0, new SPStat {Name = "SM_BRVECDESLSC", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xD6A1B78D, new SPStat {Name = "SM_BRVECDESLST", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x18813C57, new SPStat {Name = "SM_BRVECDESLTD", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x48B37AB8, new SPStat {Name = "SM_BRVECDESMAI", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x9CF1B2B2, new SPStat {Name = "SM_BRVECDESRON", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xCBFFB96E, new SPStat {Name = "SM_BRVECDESSHT", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x7999A1D5, new SPStat {Name = "SM_BRVECDESUMA", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x2C5C7E15, new SPStat {Name = "SM_BRVECDESVAP", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x31D4C964, new SPStat {Name = "SM_BRVECDESHVY", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xD31EB709, new SPStat {Name = "SM_VECBUYBFA", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xD859BDB6, new SPStat {Name = "SM_VECBUYBRU", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x4BD97B2C, new SPStat {Name = "SM_VECBUYMAI", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x5B0CB064, new SPStat {Name = "SM_VECBUYSHT", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x2E23F3BE, new SPStat {Name = "SM_VECBUYUMA", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xAEF44107, new SPStat {Name = "SM_VECBUYVAP", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xFBBFB7A7, new SPStat {Name = "SM_VECBUYHVY", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x4065FE8C, new SPStat {Name = "SM_DISDRIVBFA", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xB83D89B1, new SPStat {Name = "SM_DISDRIVBRU", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x2DD05B42, new SPStat {Name = "SM_DISDRIVLST", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x36AFC3AD, new SPStat {Name = "SM_DISDRIVMAI", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x6DF82EFC, new SPStat {Name = "SM_DISDRIVSHT", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xF4BD5AB6, new SPStat {Name = "SM_DISDRIVUMA", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x588422E8, new SPStat {Name = "SM_DISDRIVVAP", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x74900BB9, new SPStat {Name = "SM_DISDRIVHVY", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x76F8AD05, new SPStat {Name = "SM_VECMODLSC", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x81DA7DA9, new SPStat {Name = "SM_VECSTOLBFA", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x1E959849, new SPStat {Name = "SM_VECSTOLBRU", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x853C3FD8, new SPStat {Name = "SM_VECSTOLLST", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x51D1C281, new SPStat {Name = "SM_VECSTOLMAI", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x3C0D22FD, new SPStat {Name = "SM_VECSTOLSHT", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x3D9F8364, new SPStat {Name = "SM_VECSTOLUMA", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x19798BB3, new SPStat {Name = "SM_VECSTOLVAP", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xC2F8ECF4, new SPStat {Name = "SM_VECSTOLHVY", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x239F45D1, new SPStat {Name = "SM_VECDMGBFA", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xCA4AC9F3, new SPStat {Name = "SM_VECDMGBRU", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x0331EFB9, new SPStat {Name = "SM_VECDMGMAI", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x2114F8A1, new SPStat {Name = "SM_VECDMGSHT", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x61B029B8, new SPStat {Name = "SM_VECDMGUMA", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x4B0D2E2F, new SPStat {Name = "SM_VECDMGVAP", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x5189F36D, new SPStat {Name = "SM_VECDMGHVY", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x613EF1FB, new SPStat {Name = "SM_VECPEDKIL", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x89F2230E, new SPStat {Name = "SM_WEPBUYSHR", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xCA37CB6E, new SPStat {Name = "SM_WEPBUYHAL", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x6C4DBFBB, new SPStat {Name = "SM_WEPTAKEVOM", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x33C2C8B3, new SPStat {Name = "SM_WEPTAKESHR", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x605D9F19, new SPStat {Name = "SM_WEPTAKEHAL", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x820DE289, new SPStat {Name = "SM_KILCOPVOM", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x1306045C, new SPStat {Name = "SM_KILCOPSHR", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xB1744661, new SPStat {Name = "SM_KILCOPHAL", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x5CB123E2, new SPStat {Name = "SM_KILCRIMVOM", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x6CD0919E, new SPStat {Name = "SM_KILCRIMSHR", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x12268EB7, new SPStat {Name = "SM_KILCRIMHAL", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xA7BB8B93, new SPStat {Name = "SM_KILCIVMAI", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x9674FEDF, new SPStat {Name = "SM_KILCIVSHT", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x7DD4FC1C, new SPStat {Name = "SM_KILCIVUMA", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x24CBF4DC, new SPStat {Name = "SM_KILCIVVOM", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x59D605A2, new SPStat {Name = "SM_KILCIVSHR", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xAECD255F, new SPStat {Name = "SM_KILCIVHAL", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xEEB6E2EC, new SPStat {Name = "SM_VENUSESPU", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xEE9B4890, new SPStat {Name = "SM_NEWDAM", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xE0C6DCC0, new SPStat {Name = "SM_HPKIL", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xA8D8BAC3, new SPStat {Name = "SM_PUBCLUB", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x84A7FA83, new SPStat {Name = "SM_TDRNK", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x26E7185F, new SPStat {Name = "SM_FRNPUB", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x5E226D55, new SPStat {Name = "SM_DRNKCRM", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xFE0CAE77, new SPStat {Name = "SM_RAMCOM", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x36AB79FD, new SPStat {Name = "SM_RADCNT", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x7B6F5CE9, new SPStat {Name = "SM_RADWZL", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xCE56F1B9, new SPStat {Name = "SM_RADZIT", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x3CBC48B3, new SPStat {Name = "SM_ZITITCNT", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xC4316A0D, new SPStat {Name = "SM_ZITITWZL", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x3A4BD9D9, new SPStat {Name = "SM_ZITITZIT", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x71FEF5FE, new SPStat {Name = "SM_RADCHACNT", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xCD0473BC, new SPStat {Name = "SM_RADCHAWZL", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xF4060711, new SPStat {Name = "SM_PARA", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x3F5657ED, new SPStat {Name = "SM_TKFIRE", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x661C930C, new SPStat {Name = "SM_FIBAI", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x1450A592, new SPStat {Name = "SM_TANDES", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xB12E1298, new SPStat {Name = "SM_GAREP", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xF9D87ADB, new SPStat {Name = "SM_GAMONSP", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xB37BDD7C, new SPStat {Name = "SM_MONB", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x91237BA0, new SPStat {Name = "SM_MONUPSHK", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xC080BC5B, new SPStat {Name = "SM_TAXDEST", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x9B95F9EF, new SPStat {Name = "SM_KILWBFA", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x9339F118, new SPStat {Name = "SM_KILWBRU", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xF204D8C1, new SPStat {Name = "SM_KILWMAI", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x84A8FFEA, new SPStat {Name = "SM_KILWSHT", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x78CF874F, new SPStat {Name = "SM_KILWUMA", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x2D836790, new SPStat {Name = "SM_KILWVAP", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x4A7A845E, new SPStat {Name = "SM_KILWVOM", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x8351DFB9, new SPStat {Name = "SM_KILWHVY", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x183B0BBA, new SPStat {Name = "SM_CLOBOFBIN", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x15F9780C, new SPStat {Name = "SM_CLOBOFPKW", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x9551691E, new SPStat {Name = "SM_CLOBOFPON", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xAD42F919, new SPStat {Name = "SM_YOGA", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xB739474C, new SPStat {Name = "SM_TRI", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x88F1A7A3, new SPStat {Name = "SM_GYM", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xF5E8314E, new SPStat {Name = "SM_STRIP", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x67A7813D, new SPStat {Name = "SM_UGHOK", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x0FC63DEB, new SPStat {Name = "SM_STRTRO", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x443B4A47, new SPStat {Name = "SM_PISCO", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xAB96B5EB, new SPStat {Name = "SM_TOTINJ", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x85A8B0CB, new SPStat {Name = "SM_DRUGKIL", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xA8700C53, new SPStat {Name = "SM_HANGOVR", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x1D5B3F9C, new SPStat {Name = "SM_KILLSPR", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xEA87A7A4, new SPStat {Name = "SM_PEDFIREKILL", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x8D94688D, new SPStat {Name = "SM_PEDFIRETICK", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x6DC5D004, new SPStat {Name = "SM_TVTICKWAP", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x9719E2EC, new SPStat {Name = "SM_TVTICKWIW", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x8E1519FF, new SPStat {Name = "SM_ZITPOPZIT", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x714E33E2, new SPStat {Name = "SM_CARAPP", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x48A6EB87, new SPStat {Name = "SM_STOROB", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xC825E22E, new SPStat {Name = "SM_PHONCALBDG", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x99ECDEA2, new SPStat {Name = "SM_PHONCALTNK", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x39D1798F, new SPStat {Name = "SM_PHONCALWIZ", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xFBCCE5C6, new SPStat {Name = "SM_PHONTXTBDG", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xEF2DD7CF, new SPStat {Name = "SM_PHONTXTTNK", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x14C541D8, new SPStat {Name = "SM_PHONTXTWIZ", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xF7710B58, new SPStat {Name = "SM_CHTICKBDG", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xFBBE0666, new SPStat {Name = "SM_CHTICKTNK", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xB564575D, new SPStat {Name = "SM_CHTICKWIZ", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x0F5BFC56, new SPStat {Name = "SM_CALCANBDG", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xCB14F438, new SPStat {Name = "SM_CALCANTNK", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0x834E0489, new SPStat {Name = "SM_CALCANWIZ", Type = SPStatType.Int, Comment = "Stock Modifier"}},
                {0xAF1B8CBD, new SPStat {Name = "SM_PRICE_AMU", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0xBE957455, new SPStat {Name = "SM_PRICE_BDG", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x16A5A694, new SPStat {Name = "SM_PRICE_BFA", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x9B68BE74, new SPStat {Name = "SM_PRICE_BIN", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x3426CC6E, new SPStat {Name = "SM_PRICE_BTR", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0xC2318499, new SPStat {Name = "SM_PRICE_BLE", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0xC1AA72DF, new SPStat {Name = "SM_PRICE_BRU", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x684A08E3, new SPStat {Name = "SM_PRICE_CNT", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x3DD02F84, new SPStat {Name = "SM_PRICE_CRE", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x4A85DB65, new SPStat {Name = "SM_PRICE_DGP", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x3185496A, new SPStat {Name = "SM_PRICE_WAP", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x9FDF3EEB, new SPStat {Name = "SM_PRICE_FAC", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0xBFA67171, new SPStat {Name = "SM_PRICE_FRT", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0xED0D1957, new SPStat {Name = "SM_PRICE_LSC", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0xD24AE3C7, new SPStat {Name = "SM_PRICE_LST", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x4FD5E45B, new SPStat {Name = "SM_PRICE_LTD", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x65D5E80E, new SPStat {Name = "SM_PRICE_MAI", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x2FD3ED4D, new SPStat {Name = "SM_PRICE_PKW", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x81399293, new SPStat {Name = "SM_PRICE_PIS", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0xA3A49054, new SPStat {Name = "SM_PRICE_PON", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x6FAD4AFA, new SPStat {Name = "SM_PRICE_RON", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x0841D365, new SPStat {Name = "SM_PRICE_SHT", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0xD66F9BFD, new SPStat {Name = "SM_PRICE_SPU", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x630CF477, new SPStat {Name = "SM_PRICE_TNK", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x21F02220, new SPStat {Name = "SM_PRICE_WIW", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0xC5FB5491, new SPStat {Name = "SM_PRICE_UMA", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x52ABB392, new SPStat {Name = "SM_PRICE_VAP", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x9E458E5D, new SPStat {Name = "SM_PRICE_VOM", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0xDCC34382, new SPStat {Name = "SM_PRICE_WZL", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x6E4ABAD4, new SPStat {Name = "SM_PRICE_WIZ", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0xC3A44802, new SPStat {Name = "SM_PRICE_ZIT", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x68C4146C, new SPStat {Name = "SM_PRICE_SHK", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0xE57E6045, new SPStat {Name = "SM_PRICE_MOL", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0xE44793D5, new SPStat {Name = "SM_PRICE_PMP", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x67C918C4, new SPStat {Name = "SM_PRICE_GOT", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0xDD778E01, new SPStat {Name = "SM_PRICE_EYE", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x3E9309AB, new SPStat {Name = "SM_PRICE_HVY", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0xED5C1D9A, new SPStat {Name = "SM_PRICE_SHR", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x9EF8D085, new SPStat {Name = "SM_PRICE_HAL", Type = SPStatType.Float, Comment = "Comunity stat"}},
                {0x82484E3B, new SPStat {Name = "AMU_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x7109ABBE, new SPStat {Name = "AMU_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x1E9406D4, new SPStat {Name = "AMU_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x032C931C, new SPStat {Name = "BDG_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x359477EB, new SPStat {Name = "BDG_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x2591D7E6, new SPStat {Name = "BDG_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x3CFCD2B9, new SPStat {Name = "BFA_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x4EA97612, new SPStat {Name = "BFA_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x15C28445, new SPStat {Name = "BFA_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xBC803EF7, new SPStat {Name = "BIN_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xCEB76365, new SPStat {Name = "BIN_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xFD6040B6, new SPStat {Name = "BIN_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x45E583CD, new SPStat {Name = "BTR_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x1ED6B5B0, new SPStat {Name = "BTR_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x3090D924, new SPStat {Name = "BTR_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x931217F4, new SPStat {Name = "BLE_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xA0BDB34B, new SPStat {Name = "BLE_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x76895EDF, new SPStat {Name = "BLE_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x37DCF232, new SPStat {Name = "BRU_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x2D53DD20, new SPStat {Name = "BRU_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x138F2997, new SPStat {Name = "BRU_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xDC3992B9, new SPStat {Name = "CNT_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x11ECFE1F, new SPStat {Name = "CNT_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x008E5B62, new SPStat {Name = "CNT_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x2EB51BB3, new SPStat {Name = "CRE_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x5C207689, new SPStat {Name = "CRE_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x4F665D15, new SPStat {Name = "CRE_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xBCEB8AD2, new SPStat {Name = "DGP_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xA021513E, new SPStat {Name = "DGP_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x925FB5BB, new SPStat {Name = "DGP_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xA983F1D8, new SPStat {Name = "WAP_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xBBB2963D, new SPStat {Name = "WAP_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x7E4B1B67, new SPStat {Name = "WAP_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xD16A8B17, new SPStat {Name = "FAC_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xE7BEB7BF, new SPStat {Name = "FAC_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x2CDBC1F8, new SPStat {Name = "FAC_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x6D6A4ADC, new SPStat {Name = "FRT_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x0F420E8D, new SPStat {Name = "FRT_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x21863315, new SPStat {Name = "FRT_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x02756105, new SPStat {Name = "LSC_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xD4CB85B2, new SPStat {Name = "LSC_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x20B71D88, new SPStat {Name = "LSC_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x25DF05CB, new SPStat {Name = "LST_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x571D683F, new SPStat {Name = "LST_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x4945CC90, new SPStat {Name = "LST_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x966594AD, new SPStat {Name = "LTD_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xA9F7BBD1, new SPStat {Name = "LTD_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xF353CE88, new SPStat {Name = "LTD_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xBB10714F, new SPStat {Name = "MAI_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xAD59D5E2, new SPStat {Name = "MAI_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x5FAA3A84, new SPStat {Name = "MAI_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x873D6F09, new SPStat {Name = "PKW_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x1F131EBE, new SPStat {Name = "PKW_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x0D147AB9, new SPStat {Name = "PKW_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xAE5F5253, new SPStat {Name = "PIS_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xC9C3891F, new SPStat {Name = "PIS_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xDBFD2D92, new SPStat {Name = "PIS_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x41100978, new SPStat {Name = "PON_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x508EA875, new SPStat {Name = "PON_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x63EB4F2E, new SPStat {Name = "PON_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xF518C91E, new SPStat {Name = "RON_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xE1EFA2CC, new SPStat {Name = "RON_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xBFC05E5A, new SPStat {Name = "RON_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x61978846, new SPStat {Name = "SHT_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x905DE5D2, new SPStat {Name = "SHT_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x7E114139, new SPStat {Name = "SHT_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x8081B053, new SPStat {Name = "SPU_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x76EF9D2F, new SPStat {Name = "SPU_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x67B5FEB8, new SPStat {Name = "SPU_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x542B419B, new SPStat {Name = "TNK_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x45ECA51E, new SPStat {Name = "TNK_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x91B53CEA, new SPStat {Name = "TNK_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xB90F380E, new SPStat {Name = "WIW_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x07CDD58A, new SPStat {Name = "WIW_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x160571F9, new SPStat {Name = "WIW_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xDD52704A, new SPStat {Name = "UMA_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xB01395CD, new SPStat {Name = "UMA_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x2CE98F77, new SPStat {Name = "UMA_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x849CD588, new SPStat {Name = "VAP_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x95C477D7, new SPStat {Name = "VAP_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x63819352, new SPStat {Name = "VAP_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x31F8E6A6, new SPStat {Name = "VOM_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x008603C1, new SPStat {Name = "VOM_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x587D33AE, new SPStat {Name = "VOM_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xDAED8DB1, new SPStat {Name = "WZL_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x4872E8BA, new SPStat {Name = "WZL_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xFF475664, new SPStat {Name = "WZL_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x5566944F, new SPStat {Name = "WIZ_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xC6DCF73A, new SPStat {Name = "WIZ_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xB42C51D9, new SPStat {Name = "WIZ_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xAB9893DF, new SPStat {Name = "ZIT_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xE0FEFEAB, new SPStat {Name = "ZIT_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xD0FB5EA4, new SPStat {Name = "ZIT_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x21A781B5, new SPStat {Name = "SHK_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xFB59B51A, new SPStat {Name = "SHK_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x05DA4A1B, new SPStat {Name = "SHK_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x63525353, new SPStat {Name = "MOL_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x921930E4, new SPStat {Name = "MOL_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x87631B78, new SPStat {Name = "MOL_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x8FC1BD84, new SPStat {Name = "PMP_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x61B76170, new SPStat {Name = "PMP_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xAE7CFAFA, new SPStat {Name = "PMP_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x41EC850A, new SPStat {Name = "GOT_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x301DE16D, new SPStat {Name = "GOT_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x2E88DE43, new SPStat {Name = "GOT_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xAE6D1F7C, new SPStat {Name = "EYE_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x8EFAE098, new SPStat {Name = "EYE_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x80E94475, new SPStat {Name = "EYE_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x05E7DCCF, new SPStat {Name = "HVY_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xEA84A609, new SPStat {Name = "HVY_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xDD498B93, new SPStat {Name = "HVY_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x014F140E, new SPStat {Name = "SHR_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xDC1AC9A2, new SPStat {Name = "SHR_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x25CCDD09, new SPStat {Name = "SHR_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0xF6B90447, new SPStat {Name = "HAL_OW0", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x099B2A0B, new SPStat {Name = "HAL_OW1", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {0x1BE1CE98, new SPStat {Name = "HAL_OW2", Type = SPStatType.Int, Comment = "Stock market volume data"}},
                {
                    0x7AAE022C,
                    new SPStat
                        {
                            Name = "SM_DEBUG_PROF_MODE",
                            Type = SPStatType.Bool,
                            Comment = "Stock market debug flag"
                        }
                },
                {0xAB3E01DB, new SPStat {Name = "TOTAL_CASH_EARNED", Type = SPStatType.Int, Comment = "Cash - Michael"}},
                {
                    0xE4458240,
                    new SPStat
                        {
                            Name = "JEWEL_HEIST_RAW_TAKE",
                            Type = SPStatType.Int,
                            Comment =
                                "Raw total of money taken for the last run of jewel heist, this is before crew take or any other deduction."
                        }
                },
                {0x54C030FD, new SPStat {Name = "NO_PHOTOS_TAKEN", Type = SPStatType.Int, Comment = "Photos taken	"}},
                {0xADA59892, new SPStat {Name = "LAP_DANCED_BOUGHT", Type = SPStatType.Int, Comment = "Lap dances	"}},
                {0x37AFFB52, new SPStat {Name = "NO_CARS_REPAIR", Type = SPStatType.Int, Comment = "Vehicles repaired"}},
                {
                    0x3F9E6330,
                    new SPStat {Name = "VEHICLES_SPRAYED", Type = SPStatType.Int, Comment = "Vehicles resprayed"}
                },
                {
                    0xAEA4A078,
                    new SPStat {Name = "MONEY_TOTAL_SPENT", Type = SPStatType.Int, Comment = "Total cash spent  "}
                },
                {
                    0xC0DF9BE1,
                    new SPStat
                        {
                            Name = "MONEY_SPENT_CAR_MODS",
                            Type = SPStatType.Int,
                            Comment = "Cash spent on car mods"
                        }
                },
                {
                    0x92C87BFF,
                    new SPStat
                        {
                            Name = "MONEY_SPENT_ON_TATTOOS",
                            Type = SPStatType.Int,
                            Comment = "Cash spent on tattoos"
                        }
                },
                {
                    0xC2ECAFF8,
                    new SPStat
                        {
                            Name = "MONEY_SPENT_ON_HAIRDOS",
                            Type = SPStatType.Int,
                            Comment = "Cash spent on hairstyles"
                        }
                },
                {
                    0x9D6E8663,
                    new SPStat
                        {
                            Name = "MONEY_SPENT_PROPERTY",
                            Type = SPStatType.Int,
                            Comment = "Cash spent on property"
                        }
                },
                {
                    0xBAB14648,
                    new SPStat
                        {
                            Name = "RC_WALLETS_RECOVERED",
                            Type = SPStatType.Int,
                            Comment = "Wallets recovered during theft RCs, set to profile for 1435601"
                        }
                },
                {
                    0x317D5F22,
                    new SPStat
                        {
                            Name = "RC_WALLETS_RETURNED",
                            Type = SPStatType.Int,
                            Comment = "Wallets returned during theft RCs, set to profile for 1435601"
                        }
                },
                {
                    0x618E8650,
                    new SPStat
                        {
                            Name = "MS_ARM1_NOSCRATCH",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ARM1_NOSCRATCH : Not a scratch   (Player repo car takes less than 10% damage during mission)' Target value is: '100' lessThan status is : 'True'"
                        }
                },
                {
                    0x9308C44C,
                    new SPStat
                        {
                            Name = "MS_ARM1_SPECIAL_ABILITY_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ARM1_SPECIAL_ABILITY_TIME : The time in milliseconds which the player used their characters special ability on this mission.' Target value is: '7000' lessThan status is : 'False'"
                        }
                },
                {
                    0x05CFBB1D,
                    new SPStat
                        {
                            Name = "MS_ARM1_WINNER",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ARM1_WINNER : Did the player win the race between Franklin and Lamar? Set this to true if Franklin was in first place when the car park cutscene started.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x5120CCDD,
                    new SPStat
                        {
                            Name = "MS_ARM1_HIT_ALIENS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ARM1_HIT_ALIENS : Did the player drive through the movie studio without hitting any aliens? Call when/if the player hits one to fail them, otherwise pass.' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x8511E119,
                    new SPStat
                        {
                            Name = "MS_ARM2_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ARM2_TIME : Mission Time (Player completes the mission within 6 minutes 30 seconds)' Target value is: '390000' lessThan status is : 'True'"
                        }
                },
                {
                    0x7B24B461,
                    new SPStat
                        {
                            Name = "MS_ARM2_PETROL_FIRE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ARM2_PETROL_FIRE : Trail blazer (The player shoots the petrol trail which blows up the car)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x8201A006,
                    new SPStat
                        {
                            Name = "MS_ARM2_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ARM2_HEADSHOTS : The number of headshot kills performed during this mission.' Target value is: '6' lessThan status is : 'False'"
                        }
                },
                {
                    0x6DBC04CB,
                    new SPStat
                        {
                            Name = "MS_ARM2_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ARM2_ACCURACY : Tracking the players overall accuracy' Target value is: '70' lessThan status is : 'False'"
                        }
                },
                {
                    0x64BE4CA3,
                    new SPStat
                        {
                            Name = "MS_ARM3_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ARM3_TIME : Mission Time (Player completes the mission within 5 minutes)' Target value is: '300000' lessThan status is : 'True'"
                        }
                },
                {
                    0x1BDB5ABC,
                    new SPStat
                        {
                            Name = "MS_ARM3_DAMAGE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ARM3_DAMAGE : Damage taken (Player takes no damage during the brawl with Michael)' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0xAC05ADCA,
                    new SPStat
                        {
                            Name = "MS_ARM3_GARDEN_KO",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ARM3_GARDEN_KO : Call if they knock out the gardener with a stealth attack' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x1BA26304,
                    new SPStat
                        {
                            Name = "MS_ASS1_MIRROR_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS1_MIRROR_TIME : Mirrored stat, special case' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xFE8C6FFC,
                    new SPStat
                        {
                            Name = "MS_ASS1_MIRROR_SNIPER_USED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS1_MIRROR_SNIPER_USED : Mirrored stat' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x1A29EEDD,
                    new SPStat
                        {
                            Name = "MS_ASS1_MIRROR_CASH",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS1_MIRROR_CASH : Money earned. Mirror.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x77CB3E8C,
                    new SPStat
                        {
                            Name = "MS_ASS1_MIRROR_PERCENT",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS1_MIRROR_PERCENT : Completion percentage. Mirrored.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xF717B363,
                    new SPStat
                        {
                            Name = "MS_ASS1_MIRROR_MEDAL",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS1_MIRROR_MEDAL : Medal achieved. Mirror.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x1F45698E,
                    new SPStat
                        {
                            Name = "MS_ASS2_MIRROR_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS2_MIRROR_TIME : Mirrored stat, special case' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xD83D16A8,
                    new SPStat
                        {
                            Name = "MS_ASS2_MIRROR_QUICKBOOL",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS2_MIRROR_QUICKBOOL : Mirrored stat' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x14B5B059,
                    new SPStat
                        {
                            Name = "MS_ASS2_MIRROR_PERCENT",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS2_MIRROR_PERCENT : Completion percentage.Mirrored.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xAFFEC41E,
                    new SPStat
                        {
                            Name = "MS_ASS2_MIRROR_MEDAL",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS2_MIRROR_MEDAL : Medal achieved. Mirror.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x267B84FE,
                    new SPStat
                        {
                            Name = "MS_ASS2_MIRROR_CASH",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS2_MIRROR_CASH : Money earned. Mirror.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x8DA21E6C,
                    new SPStat
                        {
                            Name = "MS_ASS3_MIRROR_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS3_MIRROR_TIME : Mirrored stat, special case' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x74B65CB6,
                    new SPStat
                        {
                            Name = "MS_ASS3_MIRROR_CLEAN_ESCAPE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS3_MIRROR_CLEAN_ESCAPE : Mirrored stat' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xF4622AEB,
                    new SPStat
                        {
                            Name = "MS_ASS3_MIRROR_CASH",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS3_MIRROR_CASH : Money earned. Mirror.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x43A34AAA,
                    new SPStat
                        {
                            Name = "MS_ASS3_MIRROR_PERCENTAGE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS3_MIRROR_PERCENTAGE : Completion percentage. Mirrored.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xECF24EC8,
                    new SPStat
                        {
                            Name = "MS_ASS3_MIRROR_MEDAL",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS3_MIRROR_MEDAL : Medal achieved. Mirror.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x63FF5A78,
                    new SPStat
                        {
                            Name = "MS_ASS4_MIRROR_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS4_MIRROR_TIME : Mirrored stat, special case' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xDE654A69,
                    new SPStat
                        {
                            Name = "MS_ASS4_MIRROR_HIT_AND_RUN",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS4_MIRROR_HIT_AND_RUN : Mirrored stat' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x4C1D8F24,
                    new SPStat
                        {
                            Name = "MS_ASS4_MIRROR_CASH",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS4_MIRROR_CASH : Money earned. Mirror.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x269F18C6,
                    new SPStat
                        {
                            Name = "MS_ASS4_MIRROR_PERCENTAGE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS4_MIRROR_PERCENTAGE : Completion percentage. Mirrored.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xAB7C228B,
                    new SPStat
                        {
                            Name = "MS_ASS4_MIRROR_MEDAL",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS4_MIRROR_MEDAL : Medal achieved. Mirror.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xD281DD03,
                    new SPStat
                        {
                            Name = "MS_ASS5_MIRROR_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS5_MIRROR_TIME : Mirrored stat, special case' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x0E6EBD8F,
                    new SPStat
                        {
                            Name = "MS_ASS5_MIRROR_NO_FLY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS5_MIRROR_NO_FLY : Mirrored stat' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x98F36DF7,
                    new SPStat
                        {
                            Name = "MS_ASS5_MIRROR_CASH",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS5_MIRROR_CASH : Money earned. Mirror.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x909B1AE1,
                    new SPStat
                        {
                            Name = "MS_ASS5_MIRROR_PERCENT",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS5_MIRROR_PERCENT : Completion percentage. Mirrored.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x7F9B9032,
                    new SPStat
                        {
                            Name = "MS_ASS5_MIRROR_MEDAL",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'ASS5_MIRROR_MEDAL : Medal achieved. Mirror.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x1AE9C9CA,
                    new SPStat
                        {
                            Name = "MS_CS2_NO_SCRATCH",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CS2_NO_SCRATCH : No damage to stolen car (Player causes zero damage to the stolen car)' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x9F57F3ED,
                    new SPStat
                        {
                            Name = "MS_CS2_EAVESDROPPER",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CS2_EAVESDROPPER : Update for each of the 3 conversations listened to' Target value is: '3' lessThan status is : 'False'"
                        }
                },
                {
                    0x8021BC85,
                    new SPStat
                        {
                            Name = "MS_CS2_SCANMAN",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CS2_SCANMAN : Chad scanned  right away  in the car park.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xBFE4FB42,
                    new SPStat
                        {
                            Name = "MS_CS1_DROVE_BETWEEN_TRUCKS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CS1_DROVE_BETWEEN_TRUCKS : Drove between trucks' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x07E1D4EB,
                    new SPStat
                        {
                            Name = "MS_CS1_DROVE_BETWEEN_BUSES",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CS1_DROVE_BETWEEN_BUSES : Drove between buses' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xFE4C46EE,
                    new SPStat
                        {
                            Name = "MS_CS1_DROVE_THROUGH_TUNNEL",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CS1_DROVE_THROUGH_TUNNEL : Drove through tunnel' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x712C3CE4,
                    new SPStat
                        {
                            Name = "MS_CS1_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CS1_TIME : Mission time (Player completes the mission in a set amount of time) - 12 mins' Target value is: '720000' lessThan status is : 'True'"
                        }
                },
                {
                    0x0111A418,
                    new SPStat
                        {
                            Name = "MS_CS1_SPECIAL_USED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CS1_SPECIAL_USED : Franklin special used (Player uses the special ability in the race while playing as Franklin)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xABD0FFF7,
                    new SPStat
                        {
                            Name = "MS_CS3_NO_SCRATCH",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CS3_NO_SCRATCH : Not a Scratch (Player doesnt cause any damage to the stolen car)' Target value is: '100' lessThan status is : 'True'"
                        }
                },
                {
                    0x06C36B81,
                    new SPStat
                        {
                            Name = "MS_CS3_FASTEST_SPEED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CS3_FASTEST_SPEED : Fastest Speed (Player reaches maximum speed in the stolen car)' Target value is: '52' lessThan status is : 'False'"
                        }
                },
                {
                    0x59B1E915,
                    new SPStat
                        {
                            Name = "MS_CS3_ACTOR_KNOCKOUT",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CS3_ACTOR_KNOCKOUT : Stealthy Recasting (Player kills the actor with a knock out)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xBBBDD762,
                    new SPStat
                        {
                            Name = "MS_CS3_EJECTOR_SEAT_USED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CS3_EJECTOR_SEAT_USED : Premature Ejector (Player uses the ejector seat within 10 seconds)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xA0746827,
                    new SPStat
                        {
                            Name = "MS_CS3_RAN_OVER_ACTOR_AGAIN",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CS3_RAN_OVER_ACTOR_AGAIN : Second Strike (Runs over the actor a second time)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xB01B39FD,
                    new SPStat
                        {
                            Name = "MS_CS4_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CS4_TIME : Mission Time (complete the mission in 12 mins) The timer should start when the player enters the vehicle.' Target value is: '720000' lessThan status is : 'True'"
                        }
                },
                {
                    0xA941D881,
                    new SPStat
                        {
                            Name = "MS_CS4_NO_SCRATCH",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CS4_NO_SCRATCH : None of the stolen cars take any damage. Update: Now JB700 as only one you drive' Target value is: '100' lessThan status is : 'True'"
                        }
                },
                {
                    0xBDFFCE5F,
                    new SPStat
                        {
                            Name = "MS_CS4_SHREDS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CS4_SHREDS : Increment for each car hit with the stringer strip' Target value is: '3' lessThan status is : 'False'"
                        }
                },
                {
                    0xFF784345,
                    new SPStat
                        {
                            Name = "MS_CHI1_BODY_COUNT",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CHI1_BODY_COUNT : Body count (Player kills at least 32 enemies)' Target value is: '32' lessThan status is : 'False'"
                        }
                },
                {
                    0xCC35FB8A,
                    new SPStat
                        {
                            Name = "MS_CHI1_UNMARKED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CHI1_UNMARKED : Unmarked (Player takes less than 50 damage). ' Target value is: '50' lessThan status is : 'True'"
                        }
                },
                {
                    0xA7817449,
                    new SPStat
                        {
                            Name = "MS_CHI1_VEHICLES_DESTROYED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CHI1_VEHICLES_DESTROYED : Number of vehicles blown up' Target value is: '6' lessThan status is : 'False'"
                        }
                },
                {
                    0x3C1ED174,
                    new SPStat
                        {
                            Name = "MS_CHI1_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CHI1_TIME : Total time taken to complete the mission' Target value is: '270000' lessThan status is : 'True'"
                        }
                },
                {
                    0x16C02E44,
                    new SPStat
                        {
                            Name = "MS_CHI2_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CHI2_HEADSHOTS : Headshots (Player gets 10 headshots)' Target value is: '10' lessThan status is : 'False'"
                        }
                },
                {
                    0x7E360AB8,
                    new SPStat
                        {
                            Name = "MS_CHI2_UNMARKED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CHI2_UNMARKED : Unmarked (Player takes less than 50 damage)' Target value is: '50' lessThan status is : 'True'"
                        }
                },
                {
                    0x653905E4,
                    new SPStat
                        {
                            Name = "MS_CHI2_ONE_SHOT_TWO_KILLS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CHI2_ONE_SHOT_TWO_KILLS : 2 Birds 1 Stone (Player kills at least 2 enemies with one bullet)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x9FE5ADA0,
                    new SPStat
                        {
                            Name = "MS_CHI2_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'CHI2_ACCURACY : Weapon accuracy' Target value is: '80' lessThan status is : 'False'"
                        }
                },
                {
                    0xF06B8D6C,
                    new SPStat
                        {
                            Name = "MS_EXL1_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EXL1_ACCURACY : Accuracy (Player has 80% shooting accuracy in the mission)' Target value is: '80' lessThan status is : 'False'"
                        }
                },
                {
                    0x697E13B4,
                    new SPStat
                        {
                            Name = "MS_EXL1_BAIL_IN_CAR",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EXL1_BAIL_IN_CAR : Did the player exit the plane in the car? Set to true if they do' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x841CBFFC,
                    new SPStat
                        {
                            Name = "MS_EXL2_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EXL2_ACCURACY : Accuracy (Player has 70% shooting accuracy in the mission)' Target value is: '70' lessThan status is : 'False'"
                        }
                },
                {
                    0x06FEA738,
                    new SPStat
                        {
                            Name = "MS_EXL2_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EXL2_TIME : Mission Time (Player completes the mission in 9 minutes)' Target value is: '540000' lessThan status is : 'True'"
                        }
                },
                {
                    0x407E5EA8,
                    new SPStat
                        {
                            Name = "MS_EXL2_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EXL2_HEADSHOTS : Headshots (Player gets 3  headshots in the mission)' Target value is: '3' lessThan status is : 'False'"
                        }
                },
                {
                    0x10A74AFD,
                    new SPStat
                        {
                            Name = "MS_EXL2_ANIMAL_KILLED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EXL2_ANIMAL_KILLED : Thin the herd (Player doesnt kill any animals during the sniping section) call this to fail when the player kills an animal' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0xC6A300B9,
                    new SPStat
                        {
                            Name = "MS_EXL3_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EXL3_TIME : Mission Time (Player completes mission in a set amount of time) - 11 mins 30 secs' Target value is: '690000' lessThan status is : 'True'"
                        }
                },
                {
                    0x67BB465A,
                    new SPStat
                        {
                            Name = "MS_EXL3_FASTEST_SPEED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EXL3_FASTEST_SPEED : -	Fastest speed (Player reaches maximum speed on the bike)' Target value is: '46' lessThan status is : 'False'"
                        }
                },
                {
                    0x9C9BC150,
                    new SPStat
                        {
                            Name = "MS_EXL3_JUMPED_AT_FIRST_OPPERTU",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EXL3_JUMPED_AT_FIRST_OPPERTUNITY : First jump succeeded (Player jumps from the track to the train at the first opportunity)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xBBE29251,
                    new SPStat
                        {
                            Name = "MS_FAM1_QUICK_CATCH",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FAM1_QUICK_CATCH : Quick Catch (Player catches Jimmy within 10 seconds of him being on the mast)' Target value is: '10000' lessThan status is : 'True'"
                        }
                },
                {
                    0x12C039B9,
                    new SPStat
                        {
                            Name = "MS_FAM1_NOSCRATCH",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FAM1_NOSCRATCH :  Traffic dodger  no damage taken from other traffic while player is controlling the vehicle. Set watch entity to the vehicle the player is driving.' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0xCB9E6F75,
                    new SPStat
                        {
                            Name = "MS_FAM2_FASTEST_SPEED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FAM2_FASTEST_SPEED : Fastest speed (Players jet-ski reaches maximum speed)' Target value is: '27' lessThan status is : 'False'"
                        }
                },
                {
                    0x075721E5,
                    new SPStat
                        {
                            Name = "MS_FAM2_FELL_OFF_BIKE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FAM2_FELL_OFF_BIKE : Stay on the bike (Player doesnt fall off of the bike) Updated to track number of bails from bike. Add 1 each time the player bails.' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x2CE1CCDF,
                    new SPStat
                        {
                            Name = "MS_FAM2_FAST_SWIM",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FAM2_FAST_SWIM : Faster than fish (Player swims to the boat in 55 seconds). This one probably needs a better name' Target value is: '60000' lessThan status is : 'True'"
                        }
                },
                {
                    0x3A7EB494,
                    new SPStat
                        {
                            Name = "MS_FAM3_NOSCRATCH",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FAM3_NOSCRATCH : Not a scratch (Players car takes less than 10% damage)' Target value is: '100' lessThan status is : 'True'"
                        }
                },
                {
                    0x0F2BF67F,
                    new SPStat
                        {
                            Name = "MS_FAM3_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FAM3_TIME : Mission Time (Player completes the mission in 5 minutes)' Target value is: '330000' lessThan status is : 'True'"
                        }
                },
                {
                    0x7365E22F,
                    new SPStat
                        {
                            Name = "MS_FAM3_VEHICLE_KILLS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FAM3_VEHICLE_KILLS : Gain three or more kills in vehicle, for M or F.' Target value is: '3' lessThan status is : 'False'"
                        }
                },
                {
                    0xAB922C7C,
                    new SPStat
                        {
                            Name = "MS_FAM4_LORRY_SPEED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FAM4_LORRY_SPEED : Fastest Speed (Player reaches maximum speed in lorry)' Target value is: '34' lessThan status is : 'False'"
                        }
                },
                {
                    0xFFDC066B,
                    new SPStat
                        {
                            Name = "MS_FAM4_GOT_TOO_FAR_FROM_LAZ",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FAM4_GOT_TOO_FAR_FROM_LAZ : Stay close to Lazlow (Player stays within a certain distance on Lazlow for the entire chase) Call bool update if the player fails to stay close enough.' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x415F64C2,
                    new SPStat
                        {
                            Name = "MS_FAM4_COORD_KO",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FAM4_COORD_KO : Set if the player knocks out the event coordinator' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x28FC7101,
                    new SPStat
                        {
                            Name = "MS_FAM4_TRUCK_UNHOOKED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FAM4_TRUCK_UNHOOKED : Call inform on this if the player unhooks the trailer' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0xAF66E028,
                    new SPStat
                        {
                            Name = "MS_FAM5_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FAM5_TIME : 15 mins' Target value is: '900000' lessThan status is : 'True'"
                        }
                },
                {
                    0x8DBB806A,
                    new SPStat
                        {
                            Name = "MS_FAM5_WARRIOR_POSE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FAM5_WARRIOR_POSE : Warrior (Yoga pose complete)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x384235B8,
                    new SPStat
                        {
                            Name = "MS_FAM5_TRIANGLE_POSE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FAM5_TRIANGLE_POSE : Triangle (Yoga pose complete)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x7618EE9E,
                    new SPStat
                        {
                            Name = "MS_FAM5_SUN_SALUTATION_POSE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FAM5_SUN_SALUTATION_POSE : The sun salutation  (Yoga pose complete)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x5DBA01B2,
                    new SPStat
                        {
                            Name = "MS_FAM6_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FAM6_TIME : Mission time (Player completes mission in10 mins 30 secs)' Target value is: '630000' lessThan status is : 'True'"
                        }
                },
                {
                    0xEDECE255,
                    new SPStat
                        {
                            Name = "MS_FIN_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FIN_TIME : Mission Time (Player completes mission in 18 mins 30 secs)' Target value is: '1080000' lessThan status is : 'True'"
                        }
                },
                {
                    0xB9F5ED56,
                    new SPStat
                        {
                            Name = "MS_FIN_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FIN_HEADSHOTS : Total enemies killed with headshot by player (across all characters)' Target value is: '20' lessThan status is : 'False'"
                        }
                },
                {
                    0xC6525942,
                    new SPStat
                        {
                            Name = "MS_FIN_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FIN_ACCURACY : Overall accuracy across all characters' Target value is: '80' lessThan status is : 'False'"
                        }
                },
                {
                    0x010E848F,
                    new SPStat
                        {
                            Name = "MS_FIN_CHENG_BOMBER",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FIN_CHENG_BOMBER : Player killed Cheng with a sticky bomb' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x70A2D148,
                    new SPStat
                        {
                            Name = "MS_FIN_HAINES_HEADSHOT",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FIN_HAINES_HEADSHOT : Player killed Hains with a headshot' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x6EF92452,
                    new SPStat
                        {
                            Name = "MS_FIN_STRETCH_STRONGARM",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FIN_STRETCH_STRONGARM : Player killed Stretch with a melee attack' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x6D2DB400,
                    new SPStat
                        {
                            Name = "MS_FBI1_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI1_ACCURACY : Accuracy (Player has 70% shooting accuracy in the mission)' Target value is: '70' lessThan status is : 'False'"
                        }
                },
                {
                    0xD035575C,
                    new SPStat
                        {
                            Name = "MS_FBI1_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI1_HEADSHOTS : Headshots (Player gets 14 headshots in the mission)' Target value is: '14' lessThan status is : 'False'"
                        }
                },
                {
                    0xA6E785FF,
                    new SPStat
                        {
                            Name = "MS_FBI1_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI1_TIME : Mission Time (Player completes the mission in 6 minutes)' Target value is: '570000' lessThan status is : 'True'"
                        }
                },
                {
                    0xE737A348,
                    new SPStat
                        {
                            Name = "MS_FBI1_SPECKILLS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI1_SPECKILLS : Special Ability Kills (Player kills X number of enemies while using special ability)' Target value is: '4' lessThan status is : 'False'"
                        }
                },
                {
                    0x20C913F9,
                    new SPStat
                        {
                            Name = "MS_FBI1_UNMARKED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI1_UNMARKED : Unmarked (Player takes less than 50 damage)' Target value is: '50' lessThan status is : 'True'"
                        }
                },
                {
                    0x6262CD45,
                    new SPStat
                        {
                            Name = "MS_FBI2_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI2_TIME : Mission Time (Player completes the mission in 7.5 minutes)' Target value is: '450000' lessThan status is : 'True'"
                        }
                },
                {
                    0x4C9A4298,
                    new SPStat
                        {
                            Name = "MS_FBI2_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI2_ACCURACY : Accuracy (Player has 33% or greater shooting accuracy in the mission, machine guns against the helis make this tricky, see 759431) - old issue' Target value is: '60' lessThan status is : 'False'"
                        }
                },
                {
                    0xCDBEBFE6,
                    new SPStat
                        {
                            Name = "MS_FBI2_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI2_HEADSHOTS : Headshots (Player gets 10 headshots in the mission)' Target value is: '10' lessThan status is : 'False'"
                        }
                },
                {
                    0x0E62F571,
                    new SPStat
                        {
                            Name = "MS_FBI3_HEART_STOPPED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI3_HEART_STOPPED : Dont stop me now (Player passes the mission without the victims heart stopping) Default pass. Fail if adrenaline is used.' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0xCC5FA930,
                    new SPStat
                        {
                            Name = "MS_FBI3_ELECTROCUTION",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI3_ELECTROCUTION : Electrocution (Player electrocutes the victim)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x333849DC,
                    new SPStat
                        {
                            Name = "MS_FBI3_TOOTH",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI3_TOOTH : The Tooth Hurts (Player pulls out the victims tooth)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x752C73BE,
                    new SPStat
                        {
                            Name = "MS_FBI3_WRENCH",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI3_WRENCH : Wrench (Player hits victim with the wrench)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xE60EAFA5,
                    new SPStat
                        {
                            Name = "MS_FBI3_WATERBOARD",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI3_WATERBOARD : Its Legal! (Player tortures victim with waterboarding)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x062EB31D,
                    new SPStat
                        {
                            Name = "MS_FBI4_HELISHOT",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI4_HELISHOT : Helicopter shot down (The player shoots down the helicopter with the RPG while playing as Trevor)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x990A4438,
                    new SPStat
                        {
                            Name = "MS_FBI4_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI4_HEADSHOTS : Total enemy headshot kills' Target value is: '12' lessThan status is : 'False'"
                        }
                },
                {
                    0xEFF4CD47,
                    new SPStat
                        {
                            Name = "MS_FBI4_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI4_ACCURACY : Overall accuracy across all characters' Target value is: '60' lessThan status is : 'False'"
                        }
                },
                {
                    0x4CF55B89,
                    new SPStat
                        {
                            Name = "MS_FBI4_SWITCHES",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI4_SWITCHES : Number of character switches' Target value is: '10' lessThan status is : 'False'"
                        }
                },
                {
                    0xCAD41258,
                    new SPStat
                        {
                            Name = "MS_FBI4P1_TIME_TAKEN",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI4P1_TIME_TAKEN : Player completes the mission in 5 minutes' Target value is: '300000' lessThan status is : 'True'"
                        }
                },
                {
                    0x5B49C0D7,
                    new SPStat
                        {
                            Name = "MS_FBI4P1_VEHICLE_DAMAGE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI4P1_VEHICLE_DAMAGE : Damage caused to the garbage truck' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x75B8E540,
                    new SPStat
                        {
                            Name = "MS_FBI4P1_MAX_SPEED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI4P1_MAX_SPEED : Max speed reached in the garbage truck' Target value is: '33' lessThan status is : 'False'"
                        }
                },
                {
                    0x211744A0,
                    new SPStat
                        {
                            Name = "MS_FBI4P2_TIME_TAKEN",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI4P2_TIME_TAKEN : Overall mission time - 3 minutes' Target value is: '180000' lessThan status is : 'True'"
                        }
                },
                {
                    0x20D05B0F,
                    new SPStat
                        {
                            Name = "MS_FBI4P2_VEHICLE_DAMAGE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI4P2_VEHICLE_DAMAGE : Damage taken by the tow truck' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0xA8DA7553,
                    new SPStat
                        {
                            Name = "MS_FBI4P2_MAX_SPEED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI4P2_MAX_SPEED : Max speed reached in the tow truck' Target value is: '33' lessThan status is : 'False'"
                        }
                },
                {
                    0x50352A47,
                    new SPStat
                        {
                            Name = "MS_FBI5_STUNS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI5_STUNS : Enemies stunned (Player uses the stungun on at least 8  enemies)' Target value is: '8' lessThan status is : 'False'"
                        }
                },
                {
                    0x8894CB21,
                    new SPStat
                        {
                            Name = "MS_FBI5_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI5_HEADSHOTS : Total number of enemies killed by headshots by the player (across all characters)' Target value is: '15' lessThan status is : 'False'"
                        }
                },
                {
                    0x06CCE7FD,
                    new SPStat
                        {
                            Name = "MS_FBI5_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI5_ACCURACY : Overall accuracy across all characters used by the player.' Target value is: '70' lessThan status is : 'False'"
                        }
                },
                {
                    0x419D89BB,
                    new SPStat
                        {
                            Name = "MS_FBI5_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI5_TIME : Total time taken to complete mission' Target value is: '780000' lessThan status is : 'True'"
                        }
                },
                {
                    0xC1845B8F,
                    new SPStat
                        {
                            Name = "MS_FRA0_NOSCRATCH",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FRA0_NOSCRATCH : Not a scratch (Players vehicle takes less than 10%)' Target value is: '100' lessThan status is : 'True'"
                        }
                },
                {
                    0x666EAFEE,
                    new SPStat
                        {
                            Name = "MS_FRA0_DOGCAM",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FRA0_DOGCAM : Dog cam use(Player uses Dog Cam for 10 seconds)' Target value is: '10000' lessThan status is : 'False'"
                        }
                },
                {
                    0x85DA9F9C,
                    new SPStat
                        {
                            Name = "MS_FRA0_SPECIAL_ABILITY_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FRA0_SPECIAL_ABILITY_TIME : The time in milliseconds which the player used their characters special ability on this mission.' Target value is: '7000' lessThan status is : 'False'"
                        }
                },
                {
                    0x8E57604C,
                    new SPStat
                        {
                            Name = "MS_FRA1_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FRA1_TIME : Target time 7 mins' Target value is: '420000' lessThan status is : 'True'"
                        }
                },
                {
                    0x7D9422DA,
                    new SPStat
                        {
                            Name = "MS_FRA1_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FRA1_ACCURACY : Overall accuracy throughout the mission (including all switches)' Target value is: '70' lessThan status is : 'False'"
                        }
                },
                {
                    0x8ABF3374,
                    new SPStat
                        {
                            Name = "MS_FRA1_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FRA1_HEADSHOTS : Total number of headshots.' Target value is: '12' lessThan status is : 'False'"
                        }
                },
                {
                    0x81054EB3,
                    new SPStat
                        {
                            Name = "MS_FRA2_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FRA2_HEADSHOTS : Total enemies killed by headshots (across all characters)' Target value is: '18' lessThan status is : 'False'"
                        }
                },
                {
                    0x71DF671D,
                    new SPStat
                        {
                            Name = "MS_FRA2_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FRA2_ACCURACY : Overall weapon accuracy on mission (across all characters)' Target value is: '80' lessThan status is : 'False'"
                        }
                },
                {
                    0x822552AF,
                    new SPStat
                        {
                            Name = "MS_FRA2_SWITCHES",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FRA2_SWITCHES : Number of character switches' Target value is: '3' lessThan status is : 'False'"
                        }
                },
                {
                    0xA41E0854,
                    new SPStat
                        {
                            Name = "MS_FRA2_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FRA2_TIME : Total time taken to complete mission' Target value is: '810000' lessThan status is : 'True'"
                        }
                },
                {
                    0x95B04A45,
                    new SPStat
                        {
                            Name = "MS_LAM1_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'LAM1_ACCURACY : Accuracy (Player has 60% shooting accuracy in the mission)' Target value is: '60' lessThan status is : 'False'"
                        }
                },
                {
                    0x88E2C44E,
                    new SPStat
                        {
                            Name = "MS_LAM1_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'LAM1_HEADSHOTS : Headshots (Player gets 10 headshots in the mission)' Target value is: '10' lessThan status is : 'False'"
                        }
                },
                {
                    0xE6345A36,
                    new SPStat
                        {
                            Name = "MS_LAM1_UNMARKED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'LAM1_UNMARKED : Unmarked (Player takes less than 50 damage)' Target value is: '50' lessThan status is : 'True'"
                        }
                },
                {
                    0x9664CB65,
                    new SPStat
                        {
                            Name = "MS_LAM1_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'LAM1_TIME : Mission Time (Player has to complete the mission in 10 minutes 30 seconds)' Target value is: '630000' lessThan status is : 'True'"
                        }
                },
                {
                    0x4A257BA7,
                    new SPStat
                        {
                            Name = "MS_LES1A_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'LES1A_TIME : Mission Time (Player completes the mission in 8.5 minutes)' Target value is: '510000' lessThan status is : 'True'"
                        }
                },
                {
                    0x2B6B7F94,
                    new SPStat
                        {
                            Name = "MS_LES1A_CLEAR_POPUPS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'LES1A_CLEAR_POPUPS : Clearing pop ups (Player clears pop-ups off of the screen in 32 seconds)' Target value is: '32000' lessThan status is : 'True'"
                        }
                },
                {
                    0x2A09AA30,
                    new SPStat
                        {
                            Name = "MS_MAR1_FASTEST_SPEED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MAR1_FASTEST_SPEED : Fastest Speed (Player reaches maximum speed on the bike)' Target value is: '42' lessThan status is : 'False'"
                        }
                },
                {
                    0x0AD578E0,
                    new SPStat
                        {
                            Name = "MS_MAR1_FELL_OFF_THE_BIKE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MAR1_FELL_OFF_THE_BIKE : Dont fall off the bike (Player never falls off of the bike) This stat should track the total number of times the player bails from the bike' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x0DEBAE0E,
                    new SPStat
                        {
                            Name = "MS_MAR1_ONE_SHOT",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MAR1_ONE_SHOT : (It will be passed if the player manages to shoot the plane down with 3 shots)This stat should track the number of bullets fired during the plane shootdown section.' Target value is: '4' lessThan status is : 'True'"
                        }
                },
                {
                    0x921E4DDA,
                    new SPStat
                        {
                            Name = "MS_MAR1_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MAR1_TIME : Mission time (Player completes the mission in 9 minutes)' Target value is: '585000' lessThan status is : 'True'"
                        }
                },
                {
                    0x59379093,
                    new SPStat
                        {
                            Name = "MS_MIC1_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIC1_TIME : Mission time (Player completes mission in a set number of time) - 11 mins' Target value is: '660000' lessThan status is : 'True'"
                        }
                },
                {
                    0xA83B96D0,
                    new SPStat
                        {
                            Name = "MS_MIC1_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIC1_HEADSHOTS : Headshots (Player gets 20 headshots in the mission)' Target value is: '20' lessThan status is : 'False'"
                        }
                },
                {
                    0x3F761B2C,
                    new SPStat
                        {
                            Name = "MS_MIC1_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIC1_ACCURACY : Overall accuracy on the mission' Target value is: '80' lessThan status is : 'False'"
                        }
                },
                {
                    0x2983A142,
                    new SPStat
                        {
                            Name = "MS_MIC2_TIMES_SWITCHED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIC2_TIMES_SWITCHED : Character Switches (Player completes the mission using only 3 switches)' Target value is: '4' lessThan status is : 'True'"
                        }
                },
                {
                    0xEE1BAFFA,
                    new SPStat
                        {
                            Name = "MS_MIC2_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIC2_ACCURACY : Accuracy (Player has 70% shooting accuracy in the mission)' Target value is: '70' lessThan status is : 'False'"
                        }
                },
                {
                    0x7E373E6A,
                    new SPStat
                        {
                            Name = "MS_MIC2_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIC2_HEADSHOTS : Headshots (Player gets 10 headshots in the mission)' Target value is: '10' lessThan status is : 'False'"
                        }
                },
                {
                    0xD6AB0D7C,
                    new SPStat
                        {
                            Name = "MS_MIC2_MIKE_RESCUE_TIMER",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIC2_MIKE_RESCUE_TIMER : Reaching Michael quickly (Player rescues Michael in 3 minutes 30 seconds)' Target value is: '210000' lessThan status is : 'True'"
                        }
                },
                {
                    0xDBD33726,
                    new SPStat
                        {
                            Name = "MS_MIC2_WAYPOINT_USED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIC2_WAYPOINT_USED : No map waypoint used (Player completes the mission without setting a waypoint on the map) call this when they set a waypoint' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x4369C721,
                    new SPStat
                        {
                            Name = "MS_MIC3_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIC3_TIME : Target time: 7 minutes' Target value is: '420000' lessThan status is : 'True'"
                        }
                },
                {
                    0x4F4D0914,
                    new SPStat
                        {
                            Name = "MS_MIC3_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIC3_HEADSHOTS : Total enemies killed by the player with headshots.' Target value is: '18' lessThan status is : 'False'"
                        }
                },
                {
                    0x5B58BF54,
                    new SPStat
                        {
                            Name = "MS_MIC3_HELIKILL",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIC3_HELIKILL : Pass if the player takes out the pursuing helicopter.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x1B55181F,
                    new SPStat
                        {
                            Name = "MS_MIC4_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIC4_TIME : Target time 6:30 (increased due to limo section)' Target value is: '390000' lessThan status is : 'True'"
                        }
                },
                {
                    0x6B66344B,
                    new SPStat
                        {
                            Name = "MS_MIC4_MAX_SPEED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIC4_MAX_SPEED : Fastest speed reached in any vehicle on the mission. Watch target should always be the players current vehicle.' Target value is: '48' lessThan status is : 'False'"
                        }
                },
                {
                    0xA645FE95,
                    new SPStat
                        {
                            Name = "MS_MIC4_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIC4_HEADSHOTS : Enemies killed by headshots' Target value is: '12' lessThan status is : 'False'"
                        }
                },
                {
                    0x8A408F1F,
                    new SPStat
                        {
                            Name = "MS_MIC4_HEADSHOT_RESCUE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIC4_HEADSHOT_RESCUE : Player rescues Amanda and Tracey with a headshot.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x0B20E331,
                    new SPStat
                        {
                            Name = "MS_SOL2_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'SOL2_TIME : Mission Time (Player completes mission in a set number of time) - 3 mins' Target value is: '180000' lessThan status is : 'True'"
                        }
                },
                {
                    0xB5CF8E2E,
                    new SPStat
                        {
                            Name = "MS_SOL3_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'SOL3_TIME : Mission Time (Player completes mission in 5 minutes)' Target value is: '315000' lessThan status is : 'True'"
                        }
                },
                {
                    0x1D6F1893,
                    new SPStat
                        {
                            Name = "MS_SOL3_MAX_SPEED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'SOL3_MAX_SPEED : Fastest speed reached in any car. This should track across ANY car the player enters. Watch target should always be the players current vehicle.' Target value is: '52' lessThan status is : 'False'"
                        }
                },
                {
                    0xA17B082D,
                    new SPStat
                        {
                            Name = "MS_SOL3_COP_LOSS_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'SOL3_COP_LOSS_TIME : Time taken to lose the wanted level. Start the timer when the player gets a wanted level and end it when the level has been lost. - 2 mins' Target value is: '120000' lessThan status is : 'True'"
                        }
                },
                {
                    0x543A07D6,
                    new SPStat
                        {
                            Name = "MS_SOL3_NEWS_HELI_CAM_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'SOL3_NEWS_HELI_CAM_TIME : Total time in ms which the player views the news heli cam. This should only track action cam use when the news heli cam is available. Target 15 seconds' Target value is: '15000' lessThan status is : 'False'"
                        }
                },
                {
                    0xDF2415D9,
                    new SPStat
                        {
                            Name = "MS_TRV1_NO_SURVIVORS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TRV1_NO_SURVIVORS : Set to true when/if the player kills all the fleeing bikers after the shootout.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x17985E24,
                    new SPStat
                        {
                            Name = "MS_TRV1_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TRV1_HEADSHOTS : Headshots (Player gets 12 headshots)' Target value is: '12' lessThan status is : 'False'"
                        }
                },
                {
                    0xEEA37301,
                    new SPStat
                        {
                            Name = "MS_TRV1_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TRV1_TIME : Mission Time (Player completes mission in 12 minutes)' Target value is: '720000' lessThan status is : 'True'"
                        }
                },
                {
                    0xB71CFB88,
                    new SPStat
                        {
                            Name = "MS_TRV1_TRAILER_DAMAGE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TRV1_TRAILER_DAMAGE : Trailer Damage (Player causes $X amount of damage to the trailer)' Target value is: '5000' lessThan status is : 'False'"
                        }
                },
                {
                    0x6B21B85F,
                    new SPStat
                        {
                            Name = "MS_TRV1_BIKERS_KILLED_BEFORE_LO",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TRV1_BIKERS_KILLED_BEFORE_LOCATION : Lost and damned (Player kills the Lost and Damned bikers during the chase before they reach the location)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x454E1FF2,
                    new SPStat
                        {
                            Name = "MS_TRV2_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TRV2_HEADSHOTS : Headshots (Player gets 5 headshots)' Target value is: '5' lessThan status is : 'False'"
                        }
                },
                {
                    0x9A11E7B0,
                    new SPStat
                        {
                            Name = "MS_TRV2_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TRV2_TIME : Mission Time (Player completes the mission in 12 minutes)' Target value is: '720000' lessThan status is : 'True'"
                        }
                },
                {
                    0x3465D9F5,
                    new SPStat
                        {
                            Name = "MS_TRV2_RACEBACK_WON",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TRV2_RACEBACK_WON : Nervous Twitch (Player beats the buddy back to the airstrip)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x6B310C33,
                    new SPStat
                        {
                            Name = "MS_TRV2_UNDERBRIDGES",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TRV2_UNDERBRIDGES : 6 bridges, 1 plane (Player flies under any 6 bridges over the river)' Target value is: '6' lessThan status is : 'False'"
                        }
                },
                {
                    0x6AA4213A,
                    new SPStat
                        {
                            Name = "MS_TRV2_ALL_BIKERS_KILLED_ON_WI",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TRV2_ALL_BIKERS_KILLED_ON_WING : Call when/if trevor kills all the bikers.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x607B651B,
                    new SPStat
                        {
                            Name = "MS_TRV3_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TRV3_HEADSHOTS : Headshots (Player gets 5 headshots)' Target value is: '5' lessThan status is : 'False'"
                        }
                },
                {
                    0x18545D5B,
                    new SPStat
                        {
                            Name = "MS_TRV3_NOT_DETECTED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TRV3_NOT_DETECTED : Mystery gift (Undetected while setting up the explosives)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xBBCCE274,
                    new SPStat
                        {
                            Name = "MS_TRV3_UNMARKED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TRV3_UNMARKED : Unmarked (Player takes less than 50 damage)' Target value is: '50' lessThan status is : 'True'"
                        }
                },
                {
                    0x7CFD3AE9,
                    new SPStat
                        {
                            Name = "MS_TRV3_ALL_TRAILERS_AT_ONCE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TRV3_ALL_TRAILERS_AT_ONCE : Perfect gift (Detonated all trailers in one go)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x4A7C177D,
                    new SPStat
                        {
                            Name = "MS_AH1_EAGLE_EYE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'AH1_EAGLE_EYE : Eagle eye - Check all the number plates with zooming' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xFFB34B2C,
                    new SPStat
                        {
                            Name = "MS_AH1_MISSED_A_SPOT",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'AH1_MISSED_A_SPOT : Cleanly tailed - Get through to the janitors place without getting too close to be spotted' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xB67C0C95,
                    new SPStat
                        {
                            Name = "MS_AH1_CLEANED_OUT",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'AH1_CLEANED_OUT : Cleaned out  Get through the mission in quick time (9 minutes real time)' Target value is: '540000' lessThan status is : 'True'"
                        }
                },
                {
                    0x097691B5,
                    new SPStat
                        {
                            Name = "MS_AH2_QUICK_GETAWAY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'AH2_QUICK_GETAWAY : Quick getaway - Leave the building site within a set amount of time (45 seconds real time)' Target value is: '45000' lessThan status is : 'True'"
                        }
                },
                {
                    0x0A298B9B,
                    new SPStat
                        {
                            Name = "MS_AHP1_TRUCKCALLED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'AHP1_TRUCKCALLED : Set passed if they call a fire truck' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x6E9DA1EE,
                    new SPStat
                        {
                            Name = "MS_AHP1_NOSCRATCH",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'AHP1_NOSCRATCH : Set the watch to any fire truck the player gets in' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0xDF439506,
                    new SPStat
                        {
                            Name = "MS_AH3A_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'AH3A_TIME : Total time taken to complete mission - 18 mins' Target value is: '1080000' lessThan status is : 'True'"
                        }
                },
                {
                    0x679CBC8C,
                    new SPStat
                        {
                            Name = "MS_AH3A_OXYGEN_REMAINING",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'AH3A_OXYGEN_REMAINING : A pure percentage count of how much oxygen remains at the end of the mission. - 40%' Target value is: '40' lessThan status is : 'False'"
                        }
                },
                {
                    0xEF55C6B3,
                    new SPStat
                        {
                            Name = "MS_AH3A_FLOOR_MOP_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'AH3A_FLOOR_MOP_TIME : A timer of how long the player mops the floor. Start when they begin mopping, end when they finish planting the last bomb. - 2 mins 50 secs' Target value is: '170000' lessThan status is : 'True'"
                        }
                },
                {
                    0x1D350E81,
                    new SPStat
                        {
                            Name = "MS_AH3A_ABSEIL_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'AH3A_ABSEIL_TIME : Start the timer when they begin abseiling, end when done. - 30 seconds' Target value is: '30000' lessThan status is : 'True'"
                        }
                },
                {
                    0x65F7C554,
                    new SPStat
                        {
                            Name = "MS_AH3B_INNOCENTS_KILLED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'AH3B_INNOCENTS_KILLED : No innocent pedestrians/NPC's killed' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x4DB5303A,
                    new SPStat
                        {
                            Name = "MS_AH3B_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'AH3B_HEADSHOTS : Total enemies the player has killed by headshots, across all characters on mission.' Target value is: '20' lessThan status is : 'False'"
                        }
                },
                {
                    0x1CAF4533,
                    new SPStat
                        {
                            Name = "MS_AH3B_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'AH3B_ACCURACY : Overall accuracy while on mission' Target value is: '70' lessThan status is : 'False'"
                        }
                },
                {
                    0x734B3B64,
                    new SPStat
                        {
                            Name = "MS_AH3B_LANDING_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'AH3B_LANDING_ACCURACY : Accuracy of parachute landing, bool' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xB1AD2D8D,
                    new SPStat
                        {
                            Name = "MS_AH3B_HACKING_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'AH3B_HACKING_TIME : Time spent playing the hacking mini game. - 45 secs' Target value is: '45000' lessThan status is : 'True'"
                        }
                },
                {
                    0x3C654325,
                    new SPStat
                        {
                            Name = "MS_AH3B_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'AH3B_TIME : Total time taken to complete mission - 19 mins' Target value is: '1140000' lessThan status is : 'True'"
                        }
                },
                {
                    0xB1753E95,
                    new SPStat
                        {
                            Name = "MS_DH1_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'DH1_TIME : Mission Time - time (total play time of the mission). Target 20 mins' Target value is: '1200000' lessThan status is : 'True'"
                        }
                },
                {
                    0xFD2E9F2B,
                    new SPStat
                        {
                            Name = "MS_DH1_EMPLOYEE_OF_THE_MONTH",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'DH1_EMPLOYEE_OF_THE_MONTH : Employee of the month - bool (award for not causing any damage to any of the containers during the tasks at the docks)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x11E68101,
                    new SPStat
                        {
                            Name = "MS_DH1_PERFECT_SURVEILLANCE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'DH1_PERFECT_SURVEILLANCE : Perfect surveillance - awarded for taking all three photographs.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xFC1A09B0,
                    new SPStat
                        {
                            Name = "MS_DH1_HONEST_DAYS_WORK",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'DH1_HONEST_DAYS_WORK : An honest days work - bool (awarded for not causing any disturbance at the docks and remaining undetected)' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xB057C6AE,
                    new SPStat
                        {
                            Name = "MS_DHP1_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'DHP1_TIME : Complete within 08:30' Target value is: '510000' lessThan status is : 'True'"
                        }
                },
                {
                    0xFE618B4E,
                    new SPStat
                        {
                            Name = "MS_DHP1_NO_BOARDING",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'DHP1_NO_BOARDING : Steal the Sub without boarding the boat. Fail If the player gets on the boat' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x3E969FA9,
                    new SPStat
                        {
                            Name = "MS_DHP2_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'DHP2_TIME : Complete within 05:30' Target value is: '330000' lessThan status is : 'True'"
                        }
                },
                {
                    0x6971D8EC,
                    new SPStat
                        {
                            Name = "MS_DH2A_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'DH2A_HEADSHOTS : Enemies killed by headshots' Target value is: '12' lessThan status is : 'False'"
                        }
                },
                {
                    0x2C504EB9,
                    new SPStat
                        {
                            Name = "MS_DH2A_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'DH2A_ACCURACY : Overall accuracy across all switches' Target value is: '80' lessThan status is : 'False'"
                        }
                },
                {
                    0x9263AFAB,
                    new SPStat
                        {
                            Name = "MS_DH2A_STEALTH_KILLS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'DH2A_STEALTH_KILLS : Number of stealth kills (melee or silenced weapons) without being detected.' Target value is: '12' lessThan status is : 'False'"
                        }
                },
                {
                    0x623EED56,
                    new SPStat
                        {
                            Name = "MS_DH2A_TIME_TO_FIND_CONTAINER",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'DH2A_TIME_TO_FIND_CONTAINER : Time taken to find the container during the scuba diving section 60 secs' Target value is: '60000' lessThan status is : 'True'"
                        }
                },
                {
                    0x0D362A11,
                    new SPStat
                        {
                            Name = "MS_DH2A_NO_ALARMS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'DH2A_NO_ALARMS : No alarms triggered, call inform if they trigger an alarm to fail them. Otherwise defaults to pass' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x684D6D4A,
                    new SPStat
                        {
                            Name = "MS_DH2B_TIME_TO_FIND_CONTAINER",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'DH2B_TIME_TO_FIND_CONTAINER : Time taken to find the container during the submarine section.' Target value is: '120000' lessThan status is : 'True'"
                        }
                },
                {
                    0x1ED7AE3E,
                    new SPStat
                        {
                            Name = "MS_DH2B_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'DH2B_TIME : Total time taken to complete mission - 14:30' Target value is: '870000' lessThan status is : 'True'"
                        }
                },
                {
                    0x511824C1,
                    new SPStat
                        {
                            Name = "MS_DH2B_KILLED_ALL_MERRYWEATHER",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'DH2B_KILLED_ALL_MERRYWEATHER : The player kills all the merryweather peds that pursue them.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x1694EA10,
                    new SPStat
                        {
                            Name = "MS_DH2B_ESCAPE_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'DH2B_ESCAPE_TIME : Time taken for the player to escape/kill merryweather.' Target value is: '240000' lessThan status is : 'True'"
                        }
                },
                {
                    0xEBA3B886,
                    new SPStat
                        {
                            Name = "MS_FH1_PERFECT_DISTANCE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FH1_PERFECT_DISTANCE : Player was never warned for flying too close or too far away from the security van. Set to true if they were warned.' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x087BFA72,
                    new SPStat
                        {
                            Name = "MS_FH1_FIND_HOLE_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FH1_FIND_HOLE_TIME : Time it takes the player to find the hole after being instructed by the on screen message. 15 secs' Target value is: '15000' lessThan status is : 'True'"
                        }
                },
                {
                    0xE0885BA5,
                    new SPStat
                        {
                            Name = "MS_FH1_UNDER_BRIDGE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FH1_UNDER_BRIDGE : Player flew under the bridge. Set to true if the player flies under the bridge.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xB5E9DBB8,
                    new SPStat
                        {
                            Name = "MS_FH1_THROUGH_TUNNEL",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FH1_THROUGH_TUNNEL : Player flew through the tunnel. Set to true if the player flies through the tunnel.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xB3DD253A,
                    new SPStat
                        {
                            Name = "MS_FH1_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FH1_TIME : Total time taken to complete mission - 11 mins' Target value is: '660000' lessThan status is : 'True'"
                        }
                },
                {
                    0x34AF4678,
                    new SPStat
                        {
                            Name = "MS_FHPA_ESCAPE_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FHPA_ESCAPE_TIME : How long to escape with wagon, open time window when entering wagon, close when escape complete - 2 mins' Target value is: '120000' lessThan status is : 'True'"
                        }
                },
                {
                    0xD59CBD39,
                    new SPStat
                        {
                            Name = "MS_FHPA_DAMAGE_TO_WAGON",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FHPA_DAMAGE_TO_WAGON : Damage to the Police wagon' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x6B4A2325,
                    new SPStat
                        {
                            Name = "MS_FHPB_SNEAK_THIEF",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FHPB_SNEAK_THIEF : Steal the Cutter without being detected' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xDB4A1ABB,
                    new SPStat
                        {
                            Name = "MS_FHPB_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FHPB_TIME : Complete within 05:00' Target value is: '300000' lessThan status is : 'True'"
                        }
                },
                {
                    0x54BCEB0A,
                    new SPStat
                        {
                            Name = "MS_FH2A_MAX_SPEED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FH2A_MAX_SPEED : Fastest speed reached in any vehicle the player uses on mission. Watch target should always be the players current vehicle.' Target value is: '47' lessThan status is : 'False'"
                        }
                },
                {
                    0xFCCAED28,
                    new SPStat
                        {
                            Name = "MS_FH2A_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FH2A_HEADSHOTS : Total number of enemies killed by headshot - 20' Target value is: '20' lessThan status is : 'False'"
                        }
                },
                {
                    0xCA62CCE0,
                    new SPStat
                        {
                            Name = "MS_FH2A_TRAFFIC_LIGHT_CHANGES",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FH2A_TRAFFIC_LIGHT_CHANGES : Number of traffic light changes - 8' Target value is: '8' lessThan status is : 'True'"
                        }
                },
                {
                    0xA68E2609,
                    new SPStat
                        {
                            Name = "MS_FH2A_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FH2A_ACCURACY : General accuracy' Target value is: '60' lessThan status is : 'False'"
                        }
                },
                {
                    0xB87C6FEE,
                    new SPStat
                        {
                            Name = "MS_FH2B_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FH2B_HEADSHOTS : Total number of enemies killed by headshot' Target value is: '20' lessThan status is : 'False'"
                        }
                },
                {
                    0xC433D22B,
                    new SPStat
                        {
                            Name = "MS_FH2B_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FH2B_TIME : Total time taken to complete mission - target 16 mins' Target value is: '960000' lessThan status is : 'True'"
                        }
                },
                {
                    0x5910DE8B,
                    new SPStat
                        {
                            Name = "MS_FH2B_GOLD_DROP_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FH2B_GOLD_DROP_TIME : How long does it take to drop off the gold' Target value is: '25000' lessThan status is : 'True'"
                        }
                },
                {
                    0x4CE09B1D,
                    new SPStat
                        {
                            Name = "MS_FH2B_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FH2B_ACCURACY : Finish with a shooting accuracy of at least 60%' Target value is: '60' lessThan status is : 'False'"
                        }
                },
                {
                    0x345AD178,
                    new SPStat
                        {
                            Name = "MS_JH1_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'JH1_TIME : Total time taken to complete mission' Target value is: '480000' lessThan status is : 'True'"
                        }
                },
                {
                    0x46DA684D,
                    new SPStat
                        {
                            Name = "MS_JH1_PERFECT_PIC",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'JH1_PERFECT_PIC : Capture all 3 security features in one picture' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xF77BF1A5,
                    new SPStat
                        {
                            Name = "MS_JHP1A_SNEAKY_PEST",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'JHP1A_SNEAKY_PEST : Sneaky Pest ? ask imran' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xD853AB58,
                    new SPStat
                        {
                            Name = "MS_JHP2A_LOOSE_CARGO",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'JHP2A_LOOSE_CARGO : Shoot open the back doors to release the cargo' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xDD4D698E,
                    new SPStat
                        {
                            Name = "MS_RH1_LEISURELY_DRIVE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'RH1_LEISURELY_DRIVE : Leisurely drive - Player drives to the bank in 3 mins 30 secs' Target value is: '210000' lessThan status is : 'True'"
                        }
                },
                {
                    0xFE3F3670,
                    new SPStat
                        {
                            Name = "MS_RH1_WINNER",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'RH1_WINNER : Player wins the race back to the meth lab.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x3CD25E1C,
                    new SPStat
                        {
                            Name = "MS_RH1P_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'RH1P_HEADSHOTS : Enemies killed by headshots' Target value is: '5' lessThan status is : 'False'"
                        }
                },
                {
                    0x1FB32A9C,
                    new SPStat
                        {
                            Name = "MS_RH1P_CONVOY_STOPPED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'RH1P_CONVOY_STOPPED : player uses sticky bombs to stop the convoy  blow lead car or rear car.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xA7EE626B,
                    new SPStat
                        {
                            Name = "MS_RH2_BULLETS_FIRED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'RH2_BULLETS_FIRED : Total bullets fired on the mission.' Target value is: '4000' lessThan status is : 'False'"
                        }
                },
                {
                    0x7DF0569C,
                    new SPStat
                        {
                            Name = "MS_RH2_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'RH2_ACCURACY : Overall accuracy while on mission.' Target value is: '50' lessThan status is : 'False'"
                        }
                },
                {
                    0xCF639597,
                    new SPStat
                        {
                            Name = "MS_RH2_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'RH2_TIME : Time taken to complete mission' Target value is: '1140000' lessThan status is : 'True'"
                        }
                },
                {
                    0xBDB42E17,
                    new SPStat
                        {
                            Name = "MS_RH2_COLLATERAL_DAMAGE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'RH2_COLLATERAL_DAMAGE : An arbitrary dollar total of the damage done, increment to raise. Call when/if the player hits gas station to pass.' Target value is: '1000000' lessThan status is : 'False'"
                        }
                },
                {
                    0xC1D99CF6,
                    new SPStat
                        {
                            Name = "MS_BA1_DAMAGE_TAKEN",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'BA1_DAMAGE_TAKEN : Unmarked (player takes less than 50 damage)' Target value is: '50' lessThan status is : 'True'"
                        }
                },
                {
                    0x37FEA3C6,
                    new SPStat
                        {
                            Name = "MS_BA1_KILLCHAIN",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'BA1_KILLCHAIN : Kill Chain  Player kills X number of aliens in under X amount of seconds (more likely with Minigun). Barry 1 ' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xDF72667D,
                    new SPStat
                        {
                            Name = "MS_BA2_VANS_DESTROYED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'BA2_VANS_DESTROYED : Pre-Emptive Strike  Player destroys X clown vans quickly before they have a chance to spawn clowns. Barry 2.' Target value is: '4' lessThan status is : 'False'"
                        }
                },
                {
                    0x7380C561,
                    new SPStat
                        {
                            Name = "MS_BA2_DANCING_CLOWNS_KILLED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'BA2_DANCING_CLOWNS_KILLED : Greatest Dancer - Kill 6 Clowns while they are dancing. Barry 2' Target value is: '6' lessThan status is : 'False'"
                        }
                },
                {
                    0x93C9DD4F,
                    new SPStat
                        {
                            Name = "MS_BA3A_DELIVERY_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'BA3A_DELIVERY_TIME : Mission Time  Player delivers vehicle under a set amount of time (more generous due to losing cops). Barry 3A' Target value is: '165000' lessThan status is : 'True'"
                        }
                },
                {
                    0x9B15455A,
                    new SPStat
                        {
                            Name = "MS_BA3A_AVOID_STAKEOUT",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'BA3A_AVOID_STAKEOUT : Player completes the mission without becoming wanted. Starts TRUE, call this enum if the player is detected to fail them. Otherwise passes.' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x1A316200,
                    new SPStat
                        {
                            Name = "MS_BA3C_STASH_UNHOOKED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'BA3C_STASH_UNHOOKED : Hooked  Stash car never unhooks from tow truck. Barry 3C . Call this to fail it. Otherwise defaults to pass.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xEC2A7155,
                    new SPStat
                        {
                            Name = "MS_BA3C_DELIVERY_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'BA3C_DELIVERY_TIME : Mission Time  Player delivers vehicle within a set amount of time.  Barry 3C. ' Target value is: '90000' lessThan status is : 'True'"
                        }
                },
                {
                    0x0CB5C541,
                    new SPStat
                        {
                            Name = "MS_DR1_DREYFUSS_KILLED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'DR1_DREYFUSS_KILLED : Cut!  Player kills Dreyfuss' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x5D341B03,
                    new SPStat
                        {
                            Name = "MS_EP4_ARTIFACT_DETECTOR_USED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EP4_ARTIFACT_DETECTOR_USED : Use the Force  Player completes the mission without using the artefact detector. Epsilon 4. Call if they use it. Otherwise this defaults to pass.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xBFE71E33,
                    new SPStat
                        {
                            Name = "MS_EP6_PERFECT_LANDING",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EP6_PERFECT_LANDING : Touchdown  Player lands the epsilon plane perfectly (no damage). Epsilon 6. ' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x9DED3909,
                    new SPStat
                        {
                            Name = "MS_EP6_UNDER_BRIDGE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EP6_UNDER_BRIDGE : Canyon Crusader - Fly under one of the bridges down Raton Canyon.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xCD76F951,
                    new SPStat
                        {
                            Name = "MS_EP8_SECURITY_WIPED_OUT",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EP8_SECURITY_WIPED_OUT : Extremist Exit/Cult Intervention  Player wipes out all epsilon security. Epsilon 8' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x6B7CA9E9,
                    new SPStat
                        {
                            Name = "MS_EP8_MONEY_STOLEN",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EP8_MONEY_STOLEN : Judas/Show me the Money  Player steals the epsilon money and escapes. Epsilon 8' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x2159815C,
                    new SPStat
                        {
                            Name = "MS_EXT1_FALL_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EXT1_FALL_TIME : Free Faller  Player falls for 7 seconds before opening shoot. Extreme 1' Target value is: '7000' lessThan status is : 'False'"
                        }
                },
                {
                    0x6873637A,
                    new SPStat
                        {
                            Name = "MS_EXT1_BIG_AIR",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EXT1_BIG_AIR : Big Air  Player gets 2.5 seconds of air time during the bike race (can be done). Extreme 1' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xAA378CF3,
                    new SPStat
                        {
                            Name = "MS_EXT1_RACE_WON",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EXT1_RACE_WON : Downhill King  Player wins the bike race against Dom. Extreme 1' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x502FC01D,
                    new SPStat
                        {
                            Name = "MS_EXT2_LEAP_FROM_ATV_TO_WATER",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EXT2_LEAP_FROM_ATV_TO_WATER : Dive Bomber  Player jumps off ATV and free falls into water (doesnt use parachute).  Extreme 2' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x24BB2421,
                    new SPStat
                        {
                            Name = "MS_EXT2_NUMBER_OF_SPINS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EXT2_NUMBER_OF_SPINS : Sky Blazer  Player performs X number of spins on the quad bike. Extreme 2. ' Target value is: '8' lessThan status is : 'False'"
                        }
                },
                {
                    0xC825A59D,
                    new SPStat
                        {
                            Name = "MS_EXT3_DARE_DEVIL",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EXT3_DARE_DEVIL : Dare Devil  Player opens their parachute after Dom. Extreme 3' Target value is: '8000' lessThan status is : 'False'"
                        }
                },
                {
                    0x05A23A99,
                    new SPStat
                        {
                            Name = "MS_EXT3_PERFECT_LANDING",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EXT3_PERFECT_LANDING : Bullseye  Player lands perfectly on the back of the truck. Extreme 3' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x9C407B82,
                    new SPStat
                        {
                            Name = "MS_EXT4_FALL_SURVIVED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'EXT4_FALL_SURVIVED : Leap of Faith  Player jumps and survives after following Dom; chute auto opens. Extreme 4' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x57D7C671,
                    new SPStat
                        {
                            Name = "MS_FA1_SHORTCUT_USED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FA1_SHORTCUT_USED : Contender  Win without using shortcuts.  Fanatic 1. Call if the player does take a shortcut. Otherwise defaults to pass.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xF7921BAA,
                    new SPStat
                        {
                            Name = "MS_FA2_BUMPED_INTO_MARY_ANN",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FA2_BUMPED_INTO_MARY_ANN : Good cyclist - didn't bump into Mary Ann during the race. Call this bool stat if the bump happens to fail them. Otherwise they pass.' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x7FC84B00,
                    new SPStat
                        {
                            Name = "MS_FA2_QUICK_WIN",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FA2_QUICK_WIN : Won race under a set par time. Call when/if the player beats the par time.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x90D3FE58,
                    new SPStat
                        {
                            Name = "MS_FA3_SHORTCUT_USED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FA3_SHORTCUT_USED : Champion  Win without using shortcuts. Fanatic 3. Call if the player does take a shortcut. Otherwise defaults to pass.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x7D8DDA73,
                    new SPStat
                        {
                            Name = "MS_HU1_TWO_COYOTES_KILLED_IN_ON",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'HU1_TWO_COYOTES_KILLED_IN_ONE : 2 for 1  Player kills 2 coyotes with one shot.  Hunting 1. ' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x098237FB,
                    new SPStat
                        {
                            Name = "MS_HU1_TYRE_SHOOTING_ACCURACY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'HU1_TYRE_SHOOTING_ACCURACY : Pop! Pop!  Player has X% shooting accuracy popping all three car tires. Hunting 1. Update once with percentage value.' Target value is: '75' lessThan status is : 'False'"
                        }
                },
                {
                    0x30AF28D6,
                    new SPStat
                        {
                            Name = "MS_HU1_HIT_EACH_SATELLITE_FIRST",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'HU1_HIT_EACH_SATELLITE_FIRST_GO : Bad Signal  Player hits all three satellite dishes without missing. Hunting 1' Target value is: '3' lessThan status is : 'False'"
                        }
                },
                {
                    0xDAD587AC,
                    new SPStat
                        {
                            Name = "MS_HU2_DETECTED_BY_ELK",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'HU2_DETECTED_BY_ELK : Downwind  Player does not get detected by any elk. Hunting 2. Call this to fail them. Otherwise passes by default.' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x9AD002A1,
                    new SPStat
                        {
                            Name = "MS_HU2_ELK_HEADSHOTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'HU2_ELK_HEADSHOTS : Heart Hunter  Increment this stat when an elk is shot in the heart, this was changed due to 1202655' Target value is: '3' lessThan status is : 'False'"
                        }
                },
                {
                    0x9F1D9348,
                    new SPStat
                        {
                            Name = "MS_JO2_JOSH_MELEED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'JO2_JOSH_MELEED : Pulverizer  Player uses a melee weapon to beat up Avery. Josh 2' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xF364C526,
                    new SPStat
                        {
                            Name = "MS_JO2_STOPPED_IN_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'JO2_STOPPED_IN_TIME : Quick Catch  Player stops Averys car within X seconds of him leaving his house. Josh 2' Target value is: '40000' lessThan status is : 'True'"
                        }
                },
                {
                    0x10939166,
                    new SPStat
                        {
                            Name = "MS_JO3_ESCAPE_WITHOUT_ALERTING_",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'JO3_ESCAPE_WITHOUT_ALERTING_COPS : Out of the Frying Pan/No Time for Bacon  Player escapes without alerting the cops. Josh 3' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xC2BD6972,
                    new SPStat
                        {
                            Name = "MS_JO3_POUR_FUEL_IN_ONE_GO",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'JO3_POUR_FUEL_IN_ONE_GO : Pyromaniac  Player pours fuel trail in one go. Josh 3' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x56B52330,
                    new SPStat
                        {
                            Name = "MS_JO4_JOSH_KILLED_BEFORE_ESCAP",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'JO4_JOSH_KILLED_BEFORE_ESCAPE : Dirty Rat  Player kills Josh before escaping. Josh 4' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xE3B1151A,
                    new SPStat
                        {
                            Name = "MS_JO4_ESCAPE_IN_PARKED_COP_CAR",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'JO4_ESCAPE_IN_PARKED_COP_CAR : Hot Pursuit  Player escapes in the parked cop car. Josh 4' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x3370CFB6,
                    new SPStat
                        {
                            Name = "MS_MIN1_VEHICLE_TAKEN_AFTER_STU",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIN1_VEHICLE_TAKEN_AFTER_STUNS : Mariachi My Ride  Player takes the Mariachi vehicle after stunning both band members   Minute 1' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x69048240,
                    new SPStat
                        {
                            Name = "MS_MIN1_FAST_STOP",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIN1_FAST_STOP : Stop the Music  Player stops the Mariachi within X time of the leaving bar.  Minute 1' Target value is: '40000' lessThan status is : 'True'"
                        }
                },
                {
                    0xF5355023,
                    new SPStat
                        {
                            Name = "MS_MIN2_FIRST_CAPTURE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIN2_FIRST_CAPTURE : Downed  Capture the first immigrant within X seconds. Minute 2' Target value is: '30000' lessThan status is : 'True'"
                        }
                },
                {
                    0x3934E2A8,
                    new SPStat
                        {
                            Name = "MS_MIN2_SECOND_CAPTURE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIN2_SECOND_CAPTURE : Double Downed  Capture the second group of immigrants within X time. Minute 2' Target value is: '55000' lessThan status is : 'True'"
                        }
                },
                {
                    0xF80005FE,
                    new SPStat
                        {
                            Name = "MS_MIN2_STUNNED_ALL",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIN2_STUNNED_ALL : Shock and Awe  Player/AI uses stun gun to stop all immigrants (not ramming the off road) Minute 2' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x77353328,
                    new SPStat
                        {
                            Name = "MS_MIN3_STUNNED_AND_KILLED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIN3_STUNNED_AND_KILLED : What goes around  Player uses the stun gun on both Joe and Josef before killing them. Minute 3' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xC893DE5A,
                    new SPStat
                        {
                            Name = "MS_MIN3_KILL_BEFORE_ESCAPE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'MIN3_KILL_BEFORE_ESCAPE : No Migration  Player kills Joe and Josef before they can leave the farm. Minute 3' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xEA130ABE,
                    new SPStat
                        {
                            Name = "MS_NI1A_PLAYER_DAMAGE_DURING_BR",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'NI1A_PLAYER_DAMAGE_DURING_BRAWL : Fist Fury  Player takes no damage during the fight with Willy from Love Fist.  Nigel 1A' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0xF8B6882C,
                    new SPStat
                        {
                            Name = "MS_NI1A_ENTOURAGE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'NI1A_ENTOURAGE : player will get awarded it for talking to the entourage' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xD738180C,
                    new SPStat
                        {
                            Name = "MS_NI1B_GARDENER_TAKEN_OUT",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'NI1B_GARDENER_TAKEN_OUT : Weed Killer  Player take outs the gardener. Nigel 1B' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xAA918FF9,
                    new SPStat
                        {
                            Name = "MS_NI1B_CLOTHES_TAKEN_NO_ALERTS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'NI1B_CLOTHES_TAKEN_NO_ALERTS : Sneak Thief  Player steals the clothes without detection. Nigel 1B' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x9079E9A4,
                    new SPStat
                        {
                            Name = "MS_NI1C_PLAYER_GOT_TOO_FAR_FROM",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'NI1C_PLAYER_GOT_TOO_FAR_FROM_MUFFY : Hot on the Paws  Player keeps close to Mr Muffy Cakes throughout chase. Nigel 1C. Call when the player gets too far away. Otherwise passes by default. ' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x236B0112,
                    new SPStat
                        {
                            Name = "MS_NI1D_GOLF_CLUB_STOLEN_IN_TIM",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'NI1D_GOLF_CLUB_STOLEN_IN_TIME : Under Par  Player steals the golf club off Glen Stanky in under X seconds after Starky is alerted or killed. Nigel 1D. Open the window when they can steal the club. Close it when they do.' Target value is: '30000' lessThan status is : 'True'"
                        }
                },
                {
                    0x55608456,
                    new SPStat
                        {
                            Name = "MS_NI1D_HEADSHOTTED_STANKY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'NI1D_HEADSHOTTED_STANKY : Hole in One  Headshot Glen Stanky. Nigel 1D. Put only him on the watch list.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x0BCD25C9,
                    new SPStat
                        {
                            Name = "MS_NI1D_KILLED_STANKY_AND_GUARD",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'NI1D_KILLED_STANKY_AND_GUARDS : FOUR!  Kill Glen Stanky and his 3 security guards. Nigel 1D' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xD98A9303,
                    new SPStat
                        {
                            Name = "MS_NI2_GOT_TOO_FAR_AWAY_FROM_NA",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'NI2_GOT_TOO_FAR_AWAY_FROM_NAPOLI : Stalker  Player keeps close to Al Di Napoli during the chase. Nigel 2. Call this when the player is too far away. Otherwise they pass by default.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xE0C90C2C,
                    new SPStat
                        {
                            Name = "MS_NI2_HARM_IN_HOSPITAL_DRIVE_T",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'NI2_HARM_IN_HOSPITAL_DRIVE_THROUGH : Accident and Emergency  Player doesnt harm anyone whilst driving through the hospital. Nigel 2 Call this if the player causes any harm. Otherwise they by default pass it.' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x3FD7D7BA,
                    new SPStat
                        {
                            Name = "MS_NI2_VEHICLE_DAMAGE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'NI2_VEHICLE_DAMAGE : Not a Scratch  Player takes less than X% vehicle damage during chase. Nigel 2' Target value is: '40' lessThan status is : 'True'"
                        }
                },
                {
                    0xD80B0B6B,
                    new SPStat
                        {
                            Name = "MS_NI3_BAILED_AT_LAST_MOMENT",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'NI3_BAILED_AT_LAST_MOMENT : Skin of your Teeth  Player exits the car at the last possible moment on train tracks. Nigel 3' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xC656437A,
                    new SPStat
                        {
                            Name = "MS_NI3_REVERSED_TO_KILL",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'NI3_REVERSED_TO_KILL : Locomotivation  Player kill Al Di Napoli with the train. Nigel 3' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x69AD6157,
                    new SPStat
                        {
                            Name = "MS_PAP1_TAKEN_OUT_IN_ONE_SWING",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'PAP1_TAKEN_OUT_IN_ONE_SWING : Smack Down  Player ensures Beverly takes out the rival on his first swing. Paparazzo 1' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x48E863C1,
                    new SPStat
                        {
                            Name = "MS_PAP1_PICTURES_SNAPPED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'PAP1_PICTURES_SNAPPED : Picture Perfect  Player helps Beverly snap X number of pictures. Paparazzo 1' Target value is: '3' lessThan status is : 'False'"
                        }
                },
                {
                    0x65E364F6,
                    new SPStat
                        {
                            Name = "MS_PAP2_POOL_JUMP",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'PAP2_POOL_JUMP : Quick Dip  Player jumps into the swimming pool whilst following Beverly. Paparazzo 2' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x74AB0166,
                    new SPStat
                        {
                            Name = "MS_PAP2_FACE_RECOG_PERCENT",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'PAP2_FACE_RECOG_PERCENT : Money shot  Player gets face recognition software up to X% during chase. Paparazzo 2. Call once with the percentage achieved as the optional argument.' Target value is: '90' lessThan status is : 'False'"
                        }
                },
                {
                    0x46C3F997,
                    new SPStat
                        {
                            Name = "MS_PAP3A_FAR_FROM_POPPY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'PAP3A_FAR_FROM_POPPY : Thick of it  Player stays close to Poppy throughout the chase. Paparazzo 3A Call this if the player gets too far away. Otherwise default pass.' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0xBD366761,
                    new SPStat
                        {
                            Name = "MS_PAP3A_PHOTO_IN_CUFFS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'PAP3A_PHOTO_IN_CUFFS : DUI Diva  Player takes the photo of Poppy once shes been cuffed by the cop. Paparazzo 3A' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xDB85122C,
                    new SPStat
                        {
                            Name = "MS_PAP3B_PHOTO_SPOTTED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'PAP3B_PHOTO_SPOTTED : Silent Snapper  Player takes the photograph of the Princess without being spotted. Paparazzo 3B Call this when if the player is spotted. It will default to pass otherwise' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x7EAB3A96,
                    new SPStat
                        {
                            Name = "MS_PAP3B_PHOTO_OF_USE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'PAP3B_PHOTO_OF_USE : Royal Drag  Player takes the photo of the Princess as shes taking a drag. Paparazzo 3B' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x3177F8F5,
                    new SPStat
                        {
                            Name = "MS_PAP4_ENTIRE_CREW_KILLED_IN_O",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'PAP4_ENTIRE_CREW_KILLED_IN_ONE : Action!  Player kills the entire crew with one shot (explosives). Paparazzo 4' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xDE238F7A,
                    new SPStat
                        {
                            Name = "MS_TLO_WOUNDS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TLO_WOUNDS : Wounded  Player wounds the Last One X times before catching him. RCM The Last One' Target value is: '3' lessThan status is : 'False'"
                        }
                },
                {
                    0x567399B4,
                    new SPStat
                        {
                            Name = "MS_TLO_AMBIENT_ANIMAL_KILLS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TLO_AMBIENT_ANIMAL_KILLS : Hunter  Player kills X ambient animals whilst chasing the Last One. RCM The Last One' Target value is: '4' lessThan status is : 'False'"
                        }
                },
                {
                    0xE62689BB,
                    new SPStat
                        {
                            Name = "MS_TLO_TO_SITE_ON_FOOT",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TLO_TO_SITE_ON_FOOT : Mr Green  Player heads to the scat site on foot (doesnt use Dune buggy). RCM The Last One' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x9BDD1B37,
                    new SPStat
                        {
                            Name = "MS_JH2A_CASE_GRAB_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'JH2A_CASE_GRAB_TIME : open window when player can smash cases, close when they're done. - 50 secs' Target value is: '50000' lessThan status is : 'True'"
                        }
                },
                {
                    0xC50CC213,
                    new SPStat
                        {
                            Name = "MS_JH2A_FRANKLIN_BIKE_DAMAGE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'JH2A_FRANKLIN_BIKE_DAMAGE : Damage taken by franklin's bike during the chase while you are in the van. Set entity watch then.' Target value is: '500' lessThan status is : 'True'"
                        }
                },
                {
                    0x25A22429,
                    new SPStat
                        {
                            Name = "MS_JH2A_CASES_SMASHED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'JH2A_CASES_SMASHED : Increment for each case smashed' Target value is: '20' lessThan status is : 'False'"
                        }
                },
                {
                    0xB5080F2B,
                    new SPStat
                        {
                            Name = "MS_FBI4P4_FACE_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI4P4_FACE_TIME : How quickly did the player buy masks? target 20 seconds' Target value is: '20000' lessThan status is : 'True'"
                        }
                },
                {
                    0xC5553802,
                    new SPStat
                        {
                            Name = "MS_FBI4P4_CLICHE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI4P4_CLICHE : Player buys all hockey masks' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x7195CC46,
                    new SPStat
                        {
                            Name = "MS_FBI4P5_QUICK_SHOPPER",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI4P5_QUICK_SHOPPER : Time Taken to purchase suits on entering store - 15 secs' Target value is: '30000' lessThan status is : 'True'"
                        }
                },
                {
                    0xF34AEB34,
                    new SPStat
                        {
                            Name = "MS_FBI4P5_UNITED_COLOURS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FBI4P5_UNITED_COLOURS : Player bought a different colour for each heist member' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x6181EAC1,
                    new SPStat
                        {
                            Name = "MS_TON1_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TON1_TIME : Complete within 05:00' Target value is: '300000' lessThan status is : 'True'"
                        }
                },
                {
                    0xB7C582FF,
                    new SPStat
                        {
                            Name = "MS_TON1_UNHOOK",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TON1_UNHOOK : Keep the vehicle hooked until delivery. Call this to fail it. Otherwise defaults to pass' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0xA4FDF974,
                    new SPStat
                        {
                            Name = "MS_TON2_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TON2_TIME : Complete within 05:30' Target value is: '330000' lessThan status is : 'True'"
                        }
                },
                {
                    0xBE7DC039,
                    new SPStat
                        {
                            Name = "MS_TON2_UNHOOK",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TON2_UNHOOK : Keep the vehicle hooked until delivery. Call this to fail it. Otherwise defaults to pass' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0xC5DD072D,
                    new SPStat
                        {
                            Name = "MS_TON3_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TON3_TIME : Complete within 07:00' Target value is: '420000' lessThan status is : 'True'"
                        }
                },
                {
                    0x1A6B33DB,
                    new SPStat
                        {
                            Name = "MS_TON3_UNHOOK",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TON3_UNHOOK : Keep the vehicle hooked until delivery. Call this to fail it. Otherwise defaults to pass' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x9F7D4E9A,
                    new SPStat
                        {
                            Name = "MS_TON4_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TON4_TIME : Complete within 06:00' Target value is: '360000' lessThan status is : 'True'"
                        }
                },
                {
                    0x94074407,
                    new SPStat
                        {
                            Name = "MS_TON4_UNHOOK",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TON4_UNHOOK : Keep the vehicle hooked until delivery. Call this to fail it. Otherwise defaults to pass' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0xA49F5687,
                    new SPStat
                        {
                            Name = "MS_TON5_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TON5_TIME : Complete within 05:00' Target value is: '300000' lessThan status is : 'True'"
                        }
                },
                {
                    0x0ED63B8B,
                    new SPStat
                        {
                            Name = "MS_TON5_UNHOOK",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TON5_UNHOOK : Keep the vehicle hooked until delivery. Call this to fail it. Otherwise defaults to pass' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x22EE8E37,
                    new SPStat
                        {
                            Name = "MS_HAO1_FASTEST_LAP",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'HAO1_FASTEST_LAP : Fastest lap time. This may need a new command to override it directly rather than time it.' Target value is: '80000' lessThan status is : 'True'"
                        }
                },
                {
                    0x508FB4F1,
                    new SPStat
                        {
                            Name = "MS_HAO1_RACE_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'HAO1_RACE_TIME : Overall race time' Target value is: '170000' lessThan status is : 'True'"
                        }
                },
                {
                    0xE1172BF4,
                    new SPStat
                        {
                            Name = "MS_HAO1_COLLISIONS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'HAO1_COLLISIONS : Fewer than 10 collisions' Target value is: '5' lessThan status is : 'True'"
                        }
                },
                {
                    0xF2A8EE8D,
                    new SPStat
                        {
                            Name = "MS_FINA_KILLTREV",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FINA_KILLTREV : Award stat for killing Trevor. Pass if player killsTrevor and not Michael' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x899A30D6,
                    new SPStat
                        {
                            Name = "MS_FINB_KILLMIC",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FINB_KILLMIC : Auto awarded stat for killing Michael. No action required. No action required.' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xBF9D0FD3,
                    new SPStat
                        {
                            Name = "MS_SOL1_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'SOL1_TIME : Complete the mission in 10 minutes' Target value is: '600000' lessThan status is : 'True'"
                        }
                },
                {
                    0x5EE692D7,
                    new SPStat
                        {
                            Name = "MS_SOL1_SILENT_TAKEDOWNS",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'SOL1_SILENT_TAKEDOWNS : Silent takedowns, either melee or silenced weapons' Target value is: '3' lessThan status is : 'False'"
                        }
                },
                {
                    0x99784AB8,
                    new SPStat
                        {
                            Name = "MS_SOL1_BRAWL_DAMAGE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'SOL1_BRAWL_DAMAGE : Take no damage during the fight with Rocco' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0xE0ECA3BA,
                    new SPStat
                        {
                            Name = "MS_SOL1_PERFECT_LANDING",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'SOL1_PERFECT_LANDING : Landed the helicopter without damaging it' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x766D321B,
                    new SPStat
                        {
                            Name = "MS_TRV4_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'TRV4_TIME : Overall mission time - 4 mins' Target value is: '240000' lessThan status is : 'True'"
                        }
                },
                {
                    0xA5635A9E,
                    new SPStat
                        {
                            Name = "MS_FINPC1_MAPPED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FINPC1_MAPPED : Player took the gauntlet that was in the email picture' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xE366C621,
                    new SPStat
                        {
                            Name = "MS_FINPC1_CAR_DAMAGE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FINPC1_CAR_DAMAGE : Total car damage taken on the mission in the Gauntlet' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x0C335648,
                    new SPStat
                        {
                            Name = "MS_FINPC1_MOD_GAUNTLET",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FINPC1_MOD_GAUNTLET : Total amount spent modding Gauntlet' Target value is: '1000' lessThan status is : 'False'"
                        }
                },
                {
                    0x575AFE76,
                    new SPStat
                        {
                            Name = "MS_FINPC2_MAPPED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FINPC2_MAPPED : Player took the gauntlet that was in the email picture' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x6A1CE800,
                    new SPStat
                        {
                            Name = "MS_FINPC2_CAR_DAMAGE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FINPC2_CAR_DAMAGE : Total car damage taken on the mission in the Gauntlet' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0x3C7930F5,
                    new SPStat
                        {
                            Name = "MS_FINPC2_MOD_GAUNTLET",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FINPC2_MOD_GAUNTLET : Total amount spent modding Gauntlet' Target value is: '1000' lessThan status is : 'False'"
                        }
                },
                {
                    0xC38EE6A7,
                    new SPStat
                        {
                            Name = "MS_FINPC3_MAPPED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FINPC3_MAPPED : Player took the gauntlet that was in the email picture' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0x925CA43D,
                    new SPStat
                        {
                            Name = "MS_FINPC3_CAR_DAMAGE",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FINPC3_CAR_DAMAGE : Total car damage taken on the mission in the Gauntlet' Target value is: '1' lessThan status is : 'True'"
                        }
                },
                {
                    0xF2EB8744,
                    new SPStat
                        {
                            Name = "MS_FINPC3_MOD_GAUNTLET",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FINPC3_MOD_GAUNTLET : Total amount spent modding Gauntlet' Target value is: '1000' lessThan status is : 'False'"
                        }
                },
                {
                    0x971435E2,
                    new SPStat
                        {
                            Name = "MS_FINPD_TIME",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FINPD_TIME : Complete within 04:30  ' Target value is: '270000' lessThan status is : 'True'"
                        }
                },
                {
                    0x145E22D3,
                    new SPStat
                        {
                            Name = "MS_FINPD_UNDETECTED",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'FINPD_UNDETECTED : Steal the train without being detected' Target value is: '1' lessThan status is : 'False'"
                        }
                },
                {
                    0xF543BCBA,
                    new SPStat
                        {
                            Name = "MS_JHP1B_SWIFT_GETAWAY",
                            Type = SPStatType.Int,
                            Comment =
                                "Mission stat : 'JHP1B_SWIFT_GETAWAY : Lose the wanted level within 02:00' Target value is: '120000' lessThan status is : 'True'"
                        }
                },
                {
                    0x9651E79B,
                    new SPStat
                        {
                            Name = "HCS_JEWEL_GAMEPLAY_CHOICE",
                            Type = SPStatType.Int,
                            Comment = "Heist choice stat. Gameplay choice made for The Jewel Store Job. "
                        }
                },
                {
                    0x4E659379,
                    new SPStat
                        {
                            Name = "HCS_JEWEL_CREW1_CHOICE",
                            Type = SPStatType.Int,
                            Comment = "Heist choice stat. Crew member chosen in slot 1 for The Jewel Store Job."
                        }
                },
                {
                    0xF531A11C,
                    new SPStat
                        {
                            Name = "HCS_JEWEL_CREW2_CHOICE",
                            Type = SPStatType.Int,
                            Comment = "Heist choice stat. Crew member chosen in slot 2 for The Jewel Store Job."
                        }
                },
                {
                    0x259D4EC4,
                    new SPStat
                        {
                            Name = "HCS_JEWEL_CREW3_CHOICE",
                            Type = SPStatType.Int,
                            Comment = "Heist choice stat. Crew member chosen in slot 3 for The Jewel Store Job."
                        }
                },
                {
                    0x9E957292,
                    new SPStat
                        {
                            Name = "HCS_PORT_GAMEPLAY_CHOICE",
                            Type = SPStatType.Int,
                            Comment = "Heist choice stat. Gameplay choice made for The Port of LS Heist. "
                        }
                },
                {
                    0xC3FB2C32,
                    new SPStat
                        {
                            Name = "HCS_PALETO_CREW1_CHOICE",
                            Type = SPStatType.Int,
                            Comment = "Heist choice stat. Crew member chosen in slot 1 for The Paleto Score."
                        }
                },
                {
                    0x0D1E461E,
                    new SPStat
                        {
                            Name = "HCS_BUREAU_GAMEPLAY_CHOICE",
                            Type = SPStatType.Int,
                            Comment = "Heist choice stat. Gameplay choice made for The Bureau Heist. "
                        }
                },
                {
                    0x9850CEB4,
                    new SPStat
                        {
                            Name = "HCS_BUREAU_CREW1_CHOICE",
                            Type = SPStatType.Int,
                            Comment = "Heist choice stat. Crew member chosen in slot 1 for The Bureau Heist."
                        }
                },
                {
                    0x74943159,
                    new SPStat
                        {
                            Name = "HCS_BUREAU_CREW2_CHOICE",
                            Type = SPStatType.Int,
                            Comment = "Heist choice stat. Crew member chosen in slot 2 for The Bureau Heist."
                        }
                },
                {
                    0xDD06B178,
                    new SPStat
                        {
                            Name = "HCS_BUREAU_CREW3_CHOICE",
                            Type = SPStatType.Int,
                            Comment = "Heist choice stat. Crew member chosen in slot 3 for The Bureau Heist."
                        }
                },
                {
                    0x26344E96,
                    new SPStat
                        {
                            Name = "HCS_BIGS_GAMEPLAY_CHOICE",
                            Type = SPStatType.Int,
                            Comment = "Heist choice stat. Gameplay choice made for The Big Score. "
                        }
                },
                {
                    0xB7E6BEE8,
                    new SPStat
                        {
                            Name = "HCS_BIGS_CREW1_CHOICE",
                            Type = SPStatType.Int,
                            Comment = "Heist choice stat. Crew member chosen in slot 1 for The Big Score."
                        }
                },
                {
                    0xB81527DB,
                    new SPStat
                        {
                            Name = "HCS_BIGS_CREW2_CHOICE",
                            Type = SPStatType.Int,
                            Comment = "Heist choice stat. Crew member chosen in slot 2 for The Big Score."
                        }
                },
                {
                    0x9FF0746A,
                    new SPStat
                        {
                            Name = "HCS_BIGS_CREW3_CHOICE",
                            Type = SPStatType.Int,
                            Comment = "Heist choice stat. Crew member chosen in slot 3 for The Big Score."
                        }
                },
                {
                    0xEABFB375,
                    new SPStat
                        {
                            Name = "HCS_BIGS_CREW4_CHOICE",
                            Type = SPStatType.Int,
                            Comment = "Heist choice stat. Crew member chosen in slot 4 for The Big Score."
                        }
                },
                {
                    0x33C516A7,
                    new SPStat
                        {
                            Name = "HCS_BIGS_CREW5_CHOICE",
                            Type = SPStatType.Int,
                            Comment = "Heist choice stat. Crew member chosen in slot 5 for The Big Score."
                        }
                },
                {
                    0x57620010,
                    new SPStat
                        {
                            Name = "HCS_PENALTY_JEWEL_FUNERAL",
                            Type = SPStatType.Int,
                            Comment =
                                "Heist penalty stat. Money lost paying for crew member's death in the Jewel Store Job."
                        }
                },
                {
                    0x26D589E3,
                    new SPStat
                        {
                            Name = "HCS_PENALTY_JEWEL_DROP_MONEY",
                            Type = SPStatType.Int,
                            Comment =
                                "Heist penalty stat. Money lost due to crew member dropping it during the Jewel Store Job."
                        }
                },
                {
                    0xFF64C09C,
                    new SPStat
                        {
                            Name = "HCS_PENALTY_JEWEL_VAN_DAMAGE",
                            Type = SPStatType.Int,
                            Comment =
                                "Heist penalty stat. Money lost due to repairs made to the Bugstar Van before the Jewel Store Job. (Stealth branch only)"
                        }
                },
                {
                    0x7C5CE5DF,
                    new SPStat
                        {
                            Name = "HCS_PENALTY_JEWEL_MADR_HOUSE",
                            Type = SPStatType.Int,
                            Comment =
                                "Heist penalty stat. Money lost paying Madrazzo for the cost of the house Michael destroyed."
                        }
                },
                {
                    0x30B508E0,
                    new SPStat
                        {
                            Name = "HCS_PENALTY_DOCKS_UNSELL_WPN",
                            Type = SPStatType.Int,
                            Comment =
                                "Heist penalty stat. Money lost due to not being able to sell the Chinese weapon stolen in the heist."
                        }
                },
                {
                    0x523D24FA,
                    new SPStat
                        {
                            Name = "HCS_PENALTY_PALETO_FUNERAL",
                            Type = SPStatType.Int,
                            Comment = "Heist penalty stat. Money lost paying for crew member's death in the Paleto Score."
                        }
                },
                {
                    0x05A9B1C9,
                    new SPStat
                        {
                            Name = "HCS_PENALTY_PALETO_DROP_MONEY",
                            Type = SPStatType.Int,
                            Comment =
                                "Heist penalty stat. Money lost due to crew member dropping it during the Jewel Store Job."
                        }
                },
                {
                    0x4E28074D,
                    new SPStat
                        {
                            Name = "HCS_PENALTY_PALETO_AGENT_CUT",
                            Type = SPStatType.Int,
                            Comment = "Heist penalty stat. Money lost due to FIB agents taking a cut for the Paleto Score."
                        }
                },
                {
                    0xB701A892,
                    new SPStat
                        {
                            Name = "HCS_PENALTY_BUREAU_FUNERAL",
                            Type = SPStatType.Int,
                            Comment = "Heist penalty stat. Money lost paying for crew member's death in the Bureau Raid."
                        }
                },
                {
                    0x358939F4,
                    new SPStat
                        {
                            Name = "HCS_PENALTY_BIGS_FUNERAL",
                            Type = SPStatType.Int,
                            Comment = "Heist penalty stat. Money lost paying for crew member's death in the Big Score."
                        }
                },
                {
                    0xB86F623F,
                    new SPStat
                        {
                            Name = "HCS_PENALTY_BIGS_DROP_MONEY",
                            Type = SPStatType.Int,
                            Comment = "Heist penalty stat. Money lost due to crew member dropping it during the Big Score."
                        }
                },
                {
                    0xE8FDDE81,
                    new SPStat
                        {
                            Name = "HCS_PENALTY_BIGS_SLOW_LOADING",
                            Type = SPStatType.Int,
                            Comment =
                                "Heist penalty stat. Money lost due to crew members being slow at loading the gold. (Traffick Control branch only)"
                        }
                },
                {
                    0x850EAC46,
                    new SPStat
                        {
                            Name = "HCS_PENALTY_BIGS_HOSTAGE_GIFT",
                            Type = SPStatType.Int,
                            Comment =
                                "Heist penalty stat. Money lost due to gifting a gold bar to the hostage. (Traffick Control branch only)"
                        }
                },
                {
                    0x6C11A0FD,
                    new SPStat
                        {
                            Name = "PERCENT_STORY_MISSIONS",
                            Type = SPStatType.Int,
                            Comment = "Total Story Missions Done"
                        }
                },
                {
                    0xB82247AC,
                    new SPStat
                        {
                            Name = "PERCENT_AMBIENT_MISSIONS",
                            Type = SPStatType.Int,
                            Comment = "Total Ambient Missions Done"
                        }
                },
                {0x97262FC8, new SPStat {Name = "PERCENT_HEISTS", Type = SPStatType.Int, Comment = "Total Heists Done"}},
                {
                    0x1CA274FA,
                    new SPStat {Name = "PERCENT_ODDJOBS", Type = SPStatType.Int, Comment = "Total Oddjobs Done"}
                },
                {
                    0x6D318975,
                    new SPStat
                        {
                            Name = "PERCENT_HIDDEN_PACKAGES",
                            Type = SPStatType.Int,
                            Comment = "Total Hidden Packages Done"
                        }
                },
                {
                    0xCFEC68BF,
                    new SPStat {Name = "PERCENT_USJS", Type = SPStatType.Int, Comment = "Total Stunt Jumps Done"}
                },
                {
                    0x3DBE8EB3,
                    new SPStat
                        {
                            Name = "NUM_SH_BEER_DRUNK",
                            Type = SPStatType.Int,
                            Comment = "How many beers drunk in Safehouse."
                        }
                },
                {
                    0x70AAD431,
                    new SPStat
                        {
                            Name = "NUM_SH_BONG_SMOKED",
                            Type = SPStatType.Int,
                            Comment = "How many bong hits taken in Safehouse."
                        }
                },
                {
                    0xB516C889,
                    new SPStat
                        {
                            Name = "NUM_SH_SOFA_USED",
                            Type = SPStatType.Int,
                            Comment = "How many times sofa used in Safehouse."
                        }
                },
                {
                    0xC338EED8,
                    new SPStat
                        {
                            Name = "NUM_SH_TV_WATCHED",
                            Type = SPStatType.Int,
                            Comment = "How many times TV watched from sofa."
                        }
                },
                {
                    0xE4BA03C8,
                    new SPStat
                        {
                            Name = "NUM_SH_SOFA_SMOKED",
                            Type = SPStatType.Int,
                            Comment = "How many cigars or spliffs smoked on sofa."
                        }
                },
                {
                    0x32188F05,
                    new SPStat
                        {
                            Name = "NUM_SH_WINE_DRANK",
                            Type = SPStatType.Int,
                            Comment = "How many glasses of wine drank in safehouse."
                        }
                },
                {
                    0x74FF78D2,
                    new SPStat
                        {
                            Name = "NUM_SH_WHEATGRASS",
                            Type = SPStatType.Int,
                            Comment = "How many times of wheatgrass drank in safehouse."
                        }
                },
                {
                    0x6A32E752,
                    new SPStat
                        {
                            Name = "NUM_SH_WHISKEY",
                            Type = SPStatType.Int,
                            Comment = "How many shots of whiskey drank in safehouse."
                        }
                },
                {
                    0xFF3CB62B,
                    new SPStat
                        {
                            Name = "NUM_SH_GAS_HUFFED",
                            Type = SPStatType.Int,
                            Comment = "How many times Trevor huffed gas."
                        }
                },
                {
                    0xF2025908,
                    new SPStat
                        {
                            Name = "NUM_SH_MR_JAM_USED",
                            Type = SPStatType.Int,
                            Comment = "How many times Trevor used Mr Raspberry Jam."
                        }
                },
                {
                    0x39A82E27,
                    new SPStat
                        {
                            Name = "PROP_BOUGHT_TRAF",
                            Type = SPStatType.Bool,
                            Comment = "Has player bought arms trafficking property"
                        }
                },
                {
                    0xE7DD5D61,
                    new SPStat
                        {
                            Name = "PROP_BOUGHT_CSCR",
                            Type = SPStatType.Bool,
                            Comment = "Has player bought car scrap yard property"
                        }
                },
                {
                    0x4AF04453,
                    new SPStat
                        {
                            Name = "PROP_BOUGHT_WEED",
                            Type = SPStatType.Bool,
                            Comment = "Has player bought weed shop property"
                        }
                },
                {
                    0x167201F1,
                    new SPStat
                        {
                            Name = "PROP_BOUGHT_TAXI",
                            Type = SPStatType.Bool,
                            Comment = "Has player bought taxi lot property"
                        }
                },
                {
                    0x5F0017EB,
                    new SPStat
                        {
                            Name = "PROP_BOUGHT_CMSH",
                            Type = SPStatType.Bool,
                            Comment = "Has player bought car mod shop property"
                        }
                },
                {
                    0x6A917D37,
                    new SPStat
                        {
                            Name = "PROP_BOUGHT_SOCO",
                            Type = SPStatType.Bool,
                            Comment = "Has player bought sonar collections property"
                        }
                },
                {
                    0x48655009,
                    new SPStat
                        {
                            Name = "PROP_BOUGHT_TOWI",
                            Type = SPStatType.Bool,
                            Comment = "Has player bought towing impound property"
                        }
                },
                {
                    0x5FC23AC9,
                    new SPStat
                        {
                            Name = "PROP_BOUGHT_GOLF",
                            Type = SPStatType.Bool,
                            Comment = "Has player bought golf club property"
                        }
                },
                {
                    0xF10BB8B8,
                    new SPStat
                        {
                            Name = "PROP_BOUGHT_CINV",
                            Type = SPStatType.Bool,
                            Comment = "Has player bought vinewood cinema property"
                        }
                },
                {
                    0xD13078E2,
                    new SPStat
                        {
                            Name = "PROP_BOUGHT_CIND",
                            Type = SPStatType.Bool,
                            Comment = "Has player bought downtown cinema property"
                        }
                },
                {
                    0x654BA13A,
                    new SPStat
                        {
                            Name = "PROP_BOUGHT_CINM",
                            Type = SPStatType.Bool,
                            Comment = "Has player bought morningwood cinema property"
                        }
                },
                {
                    0x831DE5CD,
                    new SPStat
                        {
                            Name = "PROP_BOUGHT_BARTE",
                            Type = SPStatType.Bool,
                            Comment = "Has player bought bar tequilala property"
                        }
                },
                {
                    0xA52623C5,
                    new SPStat
                        {
                            Name = "PROP_BOUGHT_BARPI",
                            Type = SPStatType.Bool,
                            Comment = "Has player bought bar pitchers property"
                        }
                },
                {
                    0x8CD2ED0F,
                    new SPStat
                        {
                            Name = "PROP_BOUGHT_BARHE",
                            Type = SPStatType.Bool,
                            Comment = "Has player bought bar hen house property"
                        }
                },
                {
                    0xC68FE074,
                    new SPStat
                        {
                            Name = "PROP_BOUGHT_BARHO",
                            Type = SPStatType.Bool,
                            Comment = "Has player bought bar hookies property"
                        }
                },
                {
                    0x0D003043,
                    new SPStat
                        {
                            Name = "PROP_BOUGHT_STRIP",
                            Type = SPStatType.Bool,
                            Comment = "Has player bought strip club property"
                        }
                },
                {
                    0x154CE878,
                    new SPStat
                        {
                            Name = "PROP_EARNED_TRAF",
                            Type = SPStatType.Int,
                            Comment = "Money earned from arms trafficking property"
                        }
                },
                {
                    0xA89A682B,
                    new SPStat
                        {
                            Name = "PROP_EARNED_CSCR",
                            Type = SPStatType.Int,
                            Comment = "Money earned from car scrap yard property"
                        }
                },
                {
                    0x4E83EBA2,
                    new SPStat
                        {
                            Name = "PROP_EARNED_WEED",
                            Type = SPStatType.Int,
                            Comment = "Money earned from weed shop property"
                        }
                },
                {
                    0xD99E128F,
                    new SPStat
                        {
                            Name = "PROP_EARNED_TAXI",
                            Type = SPStatType.Int,
                            Comment = "Money earned from taxi lot property"
                        }
                },
                {
                    0xDEA91853,
                    new SPStat
                        {
                            Name = "PROP_EARNED_CMSH",
                            Type = SPStatType.Int,
                            Comment = "Money earned from car mod shop property"
                        }
                },
                {
                    0xE283B3E9,
                    new SPStat
                        {
                            Name = "PROP_EARNED_SOCO",
                            Type = SPStatType.Int,
                            Comment = "Money earned from sonar collections property"
                        }
                },
                {
                    0x44DEAA85,
                    new SPStat
                        {
                            Name = "PROP_EARNED_TOWI",
                            Type = SPStatType.Int,
                            Comment = "Money earned from towing impound property"
                        }
                },
                {
                    0xBAA081B8,
                    new SPStat
                        {
                            Name = "PROP_EARNED_GOLF",
                            Type = SPStatType.Int,
                            Comment = "Money earned from golf club property"
                        }
                },
                {
                    0xD79EF4C2,
                    new SPStat
                        {
                            Name = "PROP_EARNED_CINV",
                            Type = SPStatType.Int,
                            Comment = "Money earned from vinewood cinema property"
                        }
                },
                {
                    0xAD6EA08A,
                    new SPStat
                        {
                            Name = "PROP_EARNED_CIND",
                            Type = SPStatType.Int,
                            Comment = "Money earned from downtown cinema property"
                        }
                },
                {
                    0x6670928F,
                    new SPStat
                        {
                            Name = "PROP_EARNED_CINM",
                            Type = SPStatType.Int,
                            Comment = "Money earned from morningwood cinema property"
                        }
                },
                {
                    0x0D04D01E,
                    new SPStat
                        {
                            Name = "PROP_EARNED_BARTE",
                            Type = SPStatType.Int,
                            Comment = "Money earned from bar tequilala property"
                        }
                },
                {
                    0x888A0002,
                    new SPStat
                        {
                            Name = "PROP_EARNED_BARPI",
                            Type = SPStatType.Int,
                            Comment = "Money earned from bar pitchers property"
                        }
                },
                {
                    0x07A852AD,
                    new SPStat
                        {
                            Name = "PROP_EARNED_BARHE",
                            Type = SPStatType.Int,
                            Comment = "Money earned from bar hen house property"
                        }
                },
                {
                    0xAFDDA319,
                    new SPStat
                        {
                            Name = "PROP_EARNED_BARHO",
                            Type = SPStatType.Int,
                            Comment = "Money earned from bar hookies property"
                        }
                },
                {
                    0x0835AEDA,
                    new SPStat
                        {
                            Name = "PROP_EARNED_STRIP",
                            Type = SPStatType.Int,
                            Comment = "Money earned from strip club property"
                        }
                },
                {
                    0xFA6B471F,
                    new SPStat
                        {
                            Name = "PROP_MISSIONS_CSCR",
                            Type = SPStatType.Int,
                            Comment = "Property missions triggered for car scrap yard property"
                        }
                },
                {
                    0xB8A7D2D8,
                    new SPStat
                        {
                            Name = "PROP_MISSIONS_WEED",
                            Type = SPStatType.Int,
                            Comment = "Property missions triggered for weed shop property"
                        }
                },
                {
                    0x08028FDC,
                    new SPStat
                        {
                            Name = "PROP_MISSIONS_TAXI",
                            Type = SPStatType.Int,
                            Comment = "Property missions triggered for taxi lot property"
                        }
                },
                {
                    0x15B7CCC0,
                    new SPStat
                        {
                            Name = "PROP_MISSIONS_CINV",
                            Type = SPStatType.Int,
                            Comment = "Property missions triggered for vinewood cinema property"
                        }
                },
                {
                    0xDDA55CBC,
                    new SPStat
                        {
                            Name = "PROP_MISSIONS_CIND",
                            Type = SPStatType.Int,
                            Comment = "Property missions triggered for downtown cinema property"
                        }
                },
                {
                    0x73118796,
                    new SPStat
                        {
                            Name = "PROP_MISSIONS_CINM",
                            Type = SPStatType.Int,
                            Comment = "Property missions triggered for morningwood cinema property"
                        }
                },
                {
                    0x1EB8E63E,
                    new SPStat
                        {
                            Name = "PROP_MISSIONS_BARTE",
                            Type = SPStatType.Int,
                            Comment = "Property missions triggered for bar tequilala property"
                        }
                },
                {
                    0x44B22E25,
                    new SPStat
                        {
                            Name = "PROP_MISSIONS_BARPI",
                            Type = SPStatType.Int,
                            Comment = "Property missions triggered for bar pitchers property"
                        }
                },
                {
                    0x0695BB09,
                    new SPStat
                        {
                            Name = "PROP_MISSIONS_BARHE",
                            Type = SPStatType.Int,
                            Comment = "Property missions triggered for bar hen house property"
                        }
                },
                {
                    0x75DB9993,
                    new SPStat
                        {
                            Name = "PROP_MISSIONS_BARHO",
                            Type = SPStatType.Int,
                            Comment = "Property missions triggered for bar hookies property"
                        }
                },

            };
    }
}
