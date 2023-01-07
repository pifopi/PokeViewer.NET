﻿using PKHeX.Core;
using SysBot.Base;
using SysBot.Pokemon;
using System.Net;

namespace PokeViewer.NET
{
    public class RoutineExecutor
    {
        private readonly static SwitchConnectionConfig Config = new() { Protocol = SwitchProtocol.WiFi, IP = Properties.Settings.Default.SwitchIP, Port = 6000 };
        public SwitchSocketAsync SwitchConnection = new(Config);
        public int GameType;

        public static byte[] DownloadRemoteImageFile(string uri)
        {
            byte[] content;
#pragma warning disable SYSLIB0014 // Type or member is obsolete
            var request = (HttpWebRequest)WebRequest.Create(uri);
#pragma warning restore SYSLIB0014 // Type or member is obsolete

            using (var response = request.GetResponse())
            using (var reader = new BinaryReader(response.GetResponseStream()))
            {
                content = reader.ReadBytes(100000);
            }

            return content;
        }

        public static string FormOutput(ushort species, byte form, out string[] formString)
        {
            var strings = GameInfo.GetStrings("en");
            formString = FormConverter.GetFormList(species, strings.Types, strings.forms, GameInfo.GenderSymbolASCII, typeof(EntityContext) == typeof(PK8) ? EntityContext.Gen8 : EntityContext.Gen4);
            if (formString.Length is 0)
                return string.Empty;

            formString[0] = "";
            if (form >= formString.Length)
                form = (byte)(formString.Length - 1);

            return formString[form].Contains("-") ? formString[form] : formString[form] == "" ? "" : $"-{formString[form]}";
        }

        public static string PokeImg(PKM pkm, bool canGmax, int VersionType)
        {
            bool md = false;
            bool fd = false;
            string[] baseLink;
            string newbase = string.Empty;
            string dimensions = "128x128";
            baseLink = "https://raw.githubusercontent.com/zyro670/HomeImages/master/128x128/poke_capture_0001_000_mf_n_00000000_f_n.png".Split('_');

            if ((Species)pkm.Species == Species.Koraidon)
            {
                newbase = "https://raw.githubusercontent.com/zyro670/PokeTextures/main/Placeholder_Sprites/scaled_up_sprites/998.png";
                return newbase;
            }
            if ((Species)pkm.Species == Species.Miraidon)
            {
                newbase = "https://raw.githubusercontent.com/zyro670/PokeTextures/main/Placeholder_Sprites/scaled_up_sprites/999.png";
                return newbase;
            }
            if (VersionType == (int)GameSelected.Scarlet && (Species)pkm.Species is Species.Tauros || VersionType == (int)GameSelected.Violet && (Species)pkm.Species is Species.Tauros)
            {
                if (pkm.Form > 0)
                    newbase = "https://raw.githubusercontent.com/zyro670/PokeTextures/main/Placeholder_Sprites/scaled_up_sprites/128-" + (ushort)pkm.Form + ".png";
                else
                    newbase = "https://raw.githubusercontent.com/zyro670/PokeTextures/main/Placeholder_Sprites/scaled_up_sprites/128.png";
                return newbase;
            }
            if (VersionType == (int)GameSelected.Scarlet && (Species)pkm.Species is Species.Tatsugiri || VersionType == (int)GameSelected.Violet && (Species)pkm.Species is Species.Tatsugiri)
            {
                if (pkm.Form > 0)
                    newbase = "https://raw.githubusercontent.com/zyro670/PokeTextures/main/Placeholder_Sprites/scaled_up_sprites/952-" + (ushort)pkm.Form + ".png";
                else
                    newbase = "https://raw.githubusercontent.com/zyro670/PokeTextures/main/Placeholder_Sprites/scaled_up_sprites/952.png";
                return newbase;
            }
            if (VersionType == (int)GameSelected.Scarlet && (Species)pkm.Species is Species.Squawkabilly || VersionType == (int)GameSelected.Violet && (Species)pkm.Species is Species.Squawkabilly)
            {
                if (pkm.Form > 0)
                    newbase = "https://raw.githubusercontent.com/zyro670/PokeTextures/main/Placeholder_Sprites/scaled_up_sprites/960-" + (ushort)pkm.Form + ".png";
                else
                    newbase = "https://raw.githubusercontent.com/zyro670/PokeTextures/main/Placeholder_Sprites/scaled_up_sprites/960.png";
                return newbase;
            }
            if (VersionType == (int)GameSelected.Scarlet && (Species)pkm.Species is Species.Maushold || VersionType == (int)GameSelected.Violet && (Species)pkm.Species is Species.Maushold)
            {
                if (pkm.Form > 0)
                    newbase = "https://raw.githubusercontent.com/zyro670/PokeTextures/main/Placeholder_Sprites/scaled_up_sprites/946-" + (ushort)pkm.Form + ".png";
                else
                    newbase = "https://raw.githubusercontent.com/zyro670/PokeTextures/main/Placeholder_Sprites/scaled_up_sprites/946.png";
                return newbase;
            }
            if (VersionType == (int)GameSelected.Scarlet && (Species)pkm.Species is Species.Wooper || VersionType == (int)GameSelected.Violet && (Species)pkm.Species is Species.Wooper)
            {
                if (pkm.Form > 0)
                    newbase = "https://raw.githubusercontent.com/zyro670/PokeTextures/main/Placeholder_Sprites/scaled_up_sprites/194-1.png";
                if (pkm.IsShiny && pkm.Form > 0)
                    newbase = "https://raw.githubusercontent.com/zyro670/PokeTextures/main/Placeholder_Sprites/scaled_up_sprites/Shiny/194-1.png";
                return newbase;
            }
            if ((Species)pkm.Species == Species.Gimmighoul)
            {
                newbase = "https://raw.githubusercontent.com/zyro670/PokeTextures/main/Placeholder_Sprites/scaled_up_sprites/976.png";
                return newbase;
            }
            if ((Species)pkm.Species == Species.SlitherWing)
            {
                if (pkm.IsShiny)
                    newbase = "https://raw.githubusercontent.com/zyro670/PokeTextures/main/Placeholder_Sprites/scaled_up_sprites/Shiny/984.png";
                else
                    newbase = "https://raw.githubusercontent.com/zyro670/PokeTextures/main/Placeholder_Sprites/scaled_up_sprites/984.png";
                return newbase;
            }
            if ((Species)pkm.Species > Species.Enamorus)
            {
                // Leave out for now as shiny sprites are den only mons
                string formz = string.Empty;
                string genderz = string.Empty;
                if (pkm.Form > 0)
                    formz = $"-{pkm.Form}";
                ushort[] currShinies = { 906, 915, 916, 917, 918, 919, 920, 921, 922, 923, 924, 925, 926, 927, 928, 929, 930, 931, 932, 933, 934, 935, 936, 938, 939, 940, 941, 942, 943, 944, 945, 945, 947, 948, 949, 951, 952, 953, 954, 955, 956, 957, 957, 959, 960, 961, 962, 963, 964, 965, 966, 967, 968, 969, 970, 971, 972, 973, 974, 975, 984, 1000, 1001, 1002, 1003, 1004, 1005, 1006, 1007, 1008, 1009, 1010 };
                if (pkm.IsShiny && currShinies.Contains(pkm.Species))
                    newbase = $"https://raw.githubusercontent.com/zyro670/PokeTextures/main/Placeholder_Sprites/scaled_up_sprites/Shiny/" + $"{(int)pkm.Species}{formz}" + ".png";
                else
                    newbase = "https://raw.githubusercontent.com/zyro670/PokeTextures/main/Placeholder_Sprites/scaled_up_sprites/" + (int)pkm.Species + ".png";
                return newbase;
            }

            if (pkm.Species == (ushort)Species.Sneasel)
            {
                if (pkm.Form is 0)
                {
                    if (pkm.Gender is (int)Gender.Male)
                        newbase = $"https://raw.githubusercontent.com/zyro670/HomeImages/master/" + dimensions + "/poke_capture_0215_000_md_n_00000000_f_n.png";
                    if (pkm.Gender is (int)Gender.Male && pkm.IsShiny)
                        newbase = $"https://raw.githubusercontent.com/zyro670/HomeImages/master/" + dimensions + "/poke_capture_0215_000_md_n_00000000_f_r.png";
                    if (pkm.Gender is (int)Gender.Female)
                        newbase = $"https://raw.githubusercontent.com/zyro670/HomeImages/master/" + dimensions + "/poke_capture_0215_000_fd_n_00000000_f_n.png";
                    if (pkm.Gender is (int)Gender.Female && pkm.IsShiny)
                        newbase = $"https://raw.githubusercontent.com/zyro670/HomeImages/master/" + dimensions + "/poke_capture_0215_000_fd_n_00000000_f_r.png";
                }
                if (pkm.Form is 1)
                {
                    if (pkm.Gender is (int)Gender.Male)
                        newbase = $"https://raw.githubusercontent.com/zyro670/HomeImages/master/" + dimensions + "/poke_capture_0215_001_md_n_00000000_f_n.png";
                    if (pkm.Gender is (int)Gender.Male && pkm.IsShiny)
                        newbase = $"https://raw.githubusercontent.com/zyro670/HomeImages/master/" + dimensions + "/poke_capture_0215_001_md_n_00000000_f_r.png";
                    if (pkm.Gender is (int)Gender.Female)
                        newbase = $"https://raw.githubusercontent.com/zyro670/HomeImages/master/" + dimensions + "/poke_capture_0215_001_fd_n_00000000_f_n.png";
                    if (pkm.Gender is (int)Gender.Female && pkm.IsShiny)
                        newbase = $"https://raw.githubusercontent.com/zyro670/HomeImages/master/" + dimensions + "/poke_capture_0215_001_fd_n_00000000_f_r.png";
                }
                return newbase;
            }

            if (Enum.IsDefined(typeof(GenderDependent), pkm.Species) && !canGmax && pkm.Form is 0)
            {
                if (pkm.Gender is 0 && pkm.Species is not (ushort)Species.Torchic)
                    md = true;
                else fd = true;
            }

            int form = pkm.Species switch
            {
                (ushort)Species.Sinistea or (ushort)Species.Polteageist or (ushort)Species.Rockruff or (ushort)Species.Mothim => 0,
                (ushort)Species.Alcremie when pkm.IsShiny || canGmax => 0,
                _ => pkm.Form,

            };

            baseLink[2] = pkm.Species < 10 ? $"000{pkm.Species}" : pkm.Species < 100 && pkm.Species > 9 ? $"00{pkm.Species}" : $"0{pkm.Species}";
            baseLink[3] = pkm.Form < 10 ? $"00{form}" : $"0{form}";
            baseLink[4] = pkm.PersonalInfo.OnlyFemale ? "fo" : pkm.PersonalInfo.OnlyMale ? "mo" : pkm.PersonalInfo.Genderless ? "uk" : fd ? "fd" : md ? "md" : "mf";
            baseLink[5] = canGmax ? "g" : "n";
            baseLink[6] = "0000000" + (pkm.Species is (ushort)Species.Alcremie && !canGmax ? pkm.Data[0xE4] : 0);
            baseLink[8] = pkm.IsShiny ? "r.png" : "n.png";
            return string.Join("_", baseLink);
        }

    }
    enum GameSelected
    {
        LGP = 0,
        LGE = 1,
        SW = 2,
        SH = 3,
        BD = 4,
        SP = 5,
        LA = 6,
        Scarlet = 7,
        Violet = 8,
    }
}
