using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using PRoConEvents;

namespace Procon_Plugins.PistolsV2 {
    class Commands : PluginBase {

        public static bool Code() {



            /****************************************************
            ** !Pistols command
            ****************************************************/
            if ( Regex.Match(player.LastChat, @"^\!pistols", RegexOptions.IgnoreCase).Success ) {
                plugin.SendPlayerMessage(player.Name,
                    plugin.R("I=========== PISTOL ROUND RULES ===========I"));

                plugin.SendPlayerMessage(player.Name,
                    plugin.R("Maximum of 30 kills with each pistol."));

                plugin.SendPlayerMessage(player.Name,
                    plugin.R("Complete the !limits to unlock unlimited kills"));

                plugin.SendPlayerMessage(player.Name,
                    plugin.R("To unlock PDW's, complete the !unlocks challenge."));

                plugin.SendPlayerMessage(player.Name,
                    plugin.R("Grenades will be unlocked after 10 pistol kills"));

                plugin.SendPlayerMessage(player.Name,
                    plugin.R("The bow will be unlocked after 50 kills."));

                plugin.SendPlayerMessage(player.Name,
                    plugin.R("Commands: !nades, !limits, !unlocks, !bow, !unlockers"));
            }

            /****************************************************
            ** !Nades command
            ****************************************************/
            if ( Regex.Match(player.LastChat, @"^\!nades", RegexOptions.IgnoreCase).Success ) {
                if ( player.KillsRound >= 10 ) {
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I=========== GRENADES ===========I"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You have unlocked grenades"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I==========================================I"));
                } else {
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I=========== GRENADES INFO ===========I"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You have not yet unlocked grenades!"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R(player.KillsRound + "/10 kills done, keep going."));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I==========================================I"));
                }
            }

            /****************************************************
            ** !Limits command
            ****************************************************/
            if ( Regex.Match(player.LastChat, @"^\!limits", RegexOptions.IgnoreCase).Success ) {

                int reptool = (int) player[ "U_Repairtool" ].KillsRound;
                int defib = (int) player[ "U_Defib" ].KillsRound;

                if ( reptool >= 2 && defib >= 2 ) {
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I============ LIMITS  ============I"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You have completed the challenge. All allowed guns are now unlimited."));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I==========================================I"));
                } else {
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I============ LIMITS  ============I"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You have not yet completed the challenge!"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You have " + reptool + "/5 kills with reptool," + defib + "/5 kills with defibs. "));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("Keep going!"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I==========================================I"));
                }
            }

            /****************************************************
            ** !Unlocks command
            ****************************************************/
            if ( Regex.Match(player.LastChat, @"^\!unlocks", RegexOptions.IgnoreCase).Success ) {


                if ( !player.RoundData.issetBool("Unlocked") || !player.RoundData.getBool("Unlocked") ) {

                    string[] gunsToKillWith = new string[] {"U_SaddlegunSnp", "U_DesertEagle" , "U_HK45C", "U_CZ75", "U_FN57", "U_M1911", "U_M9", "U_MP443", "U_P226",
             "U_QSZ92","U_Glock18", "U_M93R","U_Unica6", "U_SW40", "U_Taurus44", "U_MP412Rex" };

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I========= PDW UNLOCKS ==========I"));

                    plugin.SendPlayerMessage(player.Name, plugin.R("Remaining guns to get 3 headshots with:"));

                    string guns = "";

                    foreach ( string gun in gunsToKillWith ) {
                        if ( player[ gun ].HeadshotsRound < 3 ) {

                            KillReasonInterface formattedGun = plugin.FriendlyWeaponName(gun);

                            guns += formattedGun.Name + "(" + player[ gun ].HeadshotsRound + "/3), ";

                        }
                    }

                    int chunkSize = 50;
                    int stringLength = guns.Length;
                    for ( int i = 0; i < stringLength; i += chunkSize ) {
                        if ( i + chunkSize > stringLength ) chunkSize = stringLength - i;
                        plugin.SendPlayerMessage(player.Name, plugin.R(guns.Substring(i, chunkSize)));

                    }

                    

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I==========================================I"));

                } else {

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I========= PDW UNLOCKS ==========I"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You have completed the challenge and unlocked PDW's!"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I==========================================I"));

                }

            }

            /****************************************************
            ** !Bow command
            ****************************************************/
            if ( Regex.Match(player.LastChat, @"^\!bow", RegexOptions.IgnoreCase).Success ) {
                if ( player.KillsRound >= 50 ) {
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I=========== BOW  ===========I"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You have unlocked the bow."));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I==========================================I"));
                } else {
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I=========== BOW  ===========I"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You haven't unlocked the bow yet!"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R(player.KillsRound + "/50 kills done"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("Keep going!"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I==========================================I"));
                }
            }

            /****************************************************
            ** !Unlockers command
            ****************************************************/
            if ( Regex.Match(player.LastChat, @"^\!unlockers", RegexOptions.IgnoreCase).Success ) {

                if ( !plugin.RoundData.issetString("UnlockPlayers") ) {
                    plugin.RoundData.setString("UnlockPlayers", "");
                }
                string unlockPlayers = plugin.RoundData.getString("UnlockPlayers");


                if ( !plugin.RoundData.issetString("LimitPlayers") ) {
                    plugin.RoundData.setString("LimitPlayers", "");
                }
                string limitPlayers = plugin.RoundData.getString("LimitPlayers");


                plugin.SendPlayerMessage(player.Name,
                        plugin.R("I=========== Unlockers ===========I"));

                if ( limitPlayers.Length <= 0 ) {
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("Weapon Limits: No players have completed this yet!"));
                } else {

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("Weapon Limits: " + limitPlayers.Replace("|", ", ")));

                }

                if ( unlockPlayers.Length <= 0 ) {
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("PDW Unlock: No players have completed this yet!"));
                } else {

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("PDW Unlock: " + unlockPlayers.Replace("|", ", ")));

                }

                plugin.SendPlayerMessage(player.Name,
                        plugin.R("I==========================================I"));

            }

            return false;

        }

    }
}
