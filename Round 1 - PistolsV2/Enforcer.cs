using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using PRoConEvents;

namespace Procon_Plugins.PistolsV2 {
    class Enforcer : PluginBase {

        public static bool Code() {

            string[] allowPistolsSemi = new string[] {
            "U_SaddlegunSnp", "U_DesertEagle", "U_HK45C", "U_CZ75", "U_FN57", "U_M1911", "U_M9", "U_MP443", "U_P226",
            "U_QSZ92"};
            string[] allowPistolsAuto = new string[] { "U_Glock18", "U_M93R" };
            string[] allowPistolsRevolver = new string[] { "U_Unica6", "U_SW40", "U_Taurus44", "U_MP412Rex" };

            string[] allowGrenades = new string[] { "U_Handflare", "U_Flashbang", "U_M18", "U_M34", "U_Grenade_RGO", "U_V40", "U_M67" };

            string[] allowOther = new string[] {
            "EODBot", "U_Repairtool", "U_BallisticShield", "U_Defib", "Melee", "Suicide", "SoldierCollision",
            "DamageArea", "dlSHTR"
        };

            string[] allowPdws = new string[] { "U_MX4", "U_PP2000", "U_UMP45", "U_UMP9", "U_CBJ-MS", "	U_MagpulPDR",
            "U_Scorpion","U_JS2","U_Groza-4","U_ASVal","U_P90","U_MPX","U_MP7","U_SR2","U_MagpulPDR" };

            string[] allowSnipers = new string[] { "U_CS-LR4", "U_M40A5", "U_Scout", "U_SV98", "U_JNG90", "U_SRS", "U_M98B", "U_M200", "U_FY-JS",
                                                   "U_GOL", "U_L96A1", "U_SR338", "U_CS5" };

            string[] pistolUnlock = new string[] {
                "U_Glock18", "U_M93R","U_HK45C", "U_CZ75", "U_FN57", "U_M1911", "U_M9", "U_MP443", "U_P226",
                "U_QSZ92", "U_SaddlegunSnp", "U_Taurus44", "U_MP412Rex"
            };

            string pattern1 = string.Format("({0})", string.Join("|", allowPistolsSemi));
            string pattern2 = string.Format("({0})", string.Join("|", allowPistolsAuto));
            string pattern3 = string.Format("({0})", string.Join("|", allowPistolsRevolver));
            string pattern4 = string.Format("({0})", string.Join("|", allowGrenades));
            string pattern5 = string.Format("({0})", string.Join("|", allowOther));
            string pattern6 = string.Format("({0})", string.Join("|", allowPdws));
            string pattern7 = string.Format("({0})", string.Join("|", allowSnipers));

            if (Regex.Match(kill.Weapon, pattern1, RegexOptions.IgnoreCase).Success ||
                Regex.Match(kill.Weapon, pattern2, RegexOptions.IgnoreCase).Success ||
                Regex.Match(kill.Weapon, pattern3, RegexOptions.IgnoreCase).Success ||
                Regex.Match(kill.Weapon, pattern4, RegexOptions.IgnoreCase).Success ||
                Regex.Match(kill.Weapon, pattern5, RegexOptions.IgnoreCase).Success ||
                Regex.Match(kill.Weapon, pattern6, RegexOptions.IgnoreCase).Success ||
                Regex.Match(kill.Weapon, pattern7, RegexOptions.IgnoreCase).Success
                ) {
                //This is a pistol kill.

                string pdwPattern = string.Format("({0})", string.Join("|", allowPdws));
                string sniperPattern = string.Format("({0})", string.Join("|", allowSnipers));

                if( Regex.Match(kill.Weapon, pdwPattern, RegexOptions.IgnoreCase).Success ) {

                    /****************************************************
                    ** Check for PDW kill
                    ****************************************************/
                    if ( player.RoundData.issetBool("Unlocked") && player.RoundData.getBool("Unlocked") ) {

                        // They've unlocked it.. all is good

                    } else {
                        plugin.KillPlayer(player.Name);
                        plugin.SendPlayerMessage(player.Name,
                            plugin.R("You must unlock PDW's before you can use them. To do this type !unlocks."));

                        plugin.SendPlayerYell(player.Name, plugin.R("You must unlock PDW's before you can use them. To do this type !unlocks."), 10);

                        plugin.SendGlobalMessage(plugin.R("%p_n% has been killed for using a PDW without unlocking it."));

                        plugin.PRoConChat(plugin.R("%p_n% killed for using a PDW without unlocking it."));

                    }

                } else if( Regex.Match( kill.Weapon, sniperPattern, RegexOptions.IgnoreCase ).Success ) {

                    /****************************************************
                    ** Check for Sniper kill
                    ****************************************************/
                    if( player.RoundData.issetBool( "UnlockedSniper" ) && player.RoundData.getBool( "UnlockedSniper" ) ) {

                        // They've unlocked it.. all is good

                    } else {
                        plugin.KillPlayer( player.Name );
                        plugin.SendPlayerMessage(
                                                 player.Name,
                            plugin.R(
                                     "You must unlock sniper rifles before you can use them. To do this type !sniper." ) );

                        plugin.SendPlayerYell(
                                              player.Name,
                            plugin.R( "You must unlock snipe rifles before you can use them. To do this type !sniper." ),
                            10 );

                        plugin.SendGlobalMessage(
                                                 plugin.R(
                                                          "%p_n% has been killed for using a sniper rifle without unlocking it." ) );

                        plugin.PRoConChat( plugin.R( "%p_n% killed for using a sniper rifle without unlocking it." ) );

                    }

                } else {

                    if( player.RoundData.issetBool( "BrokenLimit" ) && player.RoundData.getBool( "BrokenLimit" ) ) {
                        // They've broken their chains, let them go mad
                    } else {

                        /****************************************************
                        ** Individual Weapon Limits
                        ****************************************************/
                        int weaponLimit = 30;

                        int[] warnAt = new int[] { 20, 25 };

                        int currentWeaponCount = (int)player[ kill.Weapon ].KillsRound;

                        KillReasonInterface formattedGun = plugin.FriendlyWeaponName( kill.Weapon );

                        if( Array.IndexOf( warnAt, currentWeaponCount ) > -1 ) {
                            plugin.SendPlayerMessage(
                                                     player.Name,
                                plugin.R(
                                         "You have " + currentWeaponCount + "/" + weaponLimit + " kills with the "
                                         + formattedGun.Name + ". To unlock this weapon beyond " + weaponLimit
                                         + " kills, complete the round challenge with !limits." ) );

                            plugin.SendPlayerYell(
                                                  player.Name,
                                plugin.R(
                                         "You have " + currentWeaponCount + "/" + weaponLimit + " kills with the "
                                         + formattedGun.Name + ". To unlock this weapon beyond " + weaponLimit
                                         + " kills, complete the round challenge with !limits." ),
                                5 );
                        }

                        if( currentWeaponCount == weaponLimit ) {
                            plugin.SendPlayerMessage(
                                                     player.Name,
                                plugin.R(
                                         "You have " + currentWeaponCount + "/" + weaponLimit + " kills with the "
                                         + formattedGun.Name
                                         + ". If you use this weapon again you will be auto-killed. To unlock this weapon beyond "
                                         + weaponLimit + " kills, complete the round challenge with !limits." ) );

                            plugin.SendPlayerYell(
                                                  player.Name,
                                plugin.R(
                                         "You have " + currentWeaponCount + "/" + weaponLimit + " kills with the "
                                         + formattedGun.Name
                                         + ". If you use this weapon again you will be auto-killed. To unlock this weapon beyond "
                                         + weaponLimit + " kills, complete the round challenge with !limits." ),
                                5 );
                        }

                        if( currentWeaponCount > weaponLimit ) {
                            plugin.KillPlayer( player.Name );
                            plugin.SendPlayerMessage(
                                                     player.Name,
                                plugin.R(
                                         "You have reached the kill limit on the " + formattedGun.Name
                                         + ". To unlock this weapon beyond " + weaponLimit
                                         + " kills, complete the round challenge with !limits." ) );

                            plugin.SendPlayerYell(
                                                  player.Name,
                                plugin.R(
                                         "You have reached the kill limit on the " + formattedGun.Name
                                         + ". To unlock this weapon beyond " + weaponLimit
                                         + " kills, complete the round challenge with !limits." ),
                                10 );

                            plugin.PRoConChat(
                                              plugin.R(
                                                       "%p_n% has reached the weapon limit with the "
                                                       + formattedGun.Name + "." ) );

                        }

                        /****************************************************
                    ** Check for !limits unlock
                    ****************************************************/

                        string[] gunsToBreakLimit = new string[] { "U_Repairtool", "U_Defib" };

                        bool hasBrokenLimit = true;

                        foreach( string gun in gunsToBreakLimit ) {
                            if( player[ gun ].KillsRound < 3 ) {
                                hasBrokenLimit = false;
                                break;
                            }
                        }

                        if( hasBrokenLimit ) {

                            if( !plugin.RoundData.issetString( "LimitPlayers" ) ) {
                                plugin.RoundData.setString( "LimitPlayers", "" );
                            }

                            string limitPlayers = plugin.RoundData.getString( "LimitPlayers" );

                            limitPlayers += player.Name + "|";

                            plugin.RoundData.setString( "LimitPlayers", limitPlayers );

                            player.RoundData.setBool( "BrokenLimit", true );

                            plugin.SendPlayerMessage(
                                                     player.Name,
                                plugin.R(
                                         "You have completed the limit unlock for this round, you may now use any allowed gun without limits." ) );

                            plugin.SendPlayerYell(
                                                  player.Name,
                                plugin.R(
                                         "You have completed the limit unlock for this round, you may now use any allowed gun without limits." ),
                                5 );

                            plugin.SendGlobalMessage( plugin.R( "!!!! %p_n% has completed the !limits challenge." ) );
                            plugin.SendGlobalYell( plugin.R( "!!!! %p_n% has completed the !limits challenge." ), 10 );

                            plugin.PRoConChat( plugin.R( "%p_n% completed the !limits challenge." ) );

                        }
                    }

                }

                /****************************************************
                ** Check Grenade Kills
                ****************************************************/
                int grenadeKillsUnlock = 20;

                string grenadePattern = string.Format("({0})", string.Join("|", allowGrenades));

                if ( Regex.Match(kill.Weapon, grenadePattern, RegexOptions.IgnoreCase).Success ) {
                    /****************************************************
                    ** Punish if use grenades before 20 kills - This is lazy.. come back to this and check they're actually pistol kills.
                    ****************************************************/
                    if ( player.KillsRound < grenadeKillsUnlock ) {
                        plugin.KillPlayer(player.Name);
                        plugin.SendPlayerMessage(player.Name,
                            plugin.R("You cannot use Grenades until you reach " + grenadeKillsUnlock + " kills."));

                        plugin.SendPlayerYell(player.Name,
                            plugin.R("You cannot use Grenades until you reach " + grenadeKillsUnlock + " kills."), 10);
                    }
                }

                /****************************************************
                ** Unlock grenades after 20 kills - This is lazy.. come back to this and check they're actually pistol kills.
                ****************************************************/
                if ( player.KillsRound == grenadeKillsUnlock ) {
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You have unlocked grenades for the rest of this round."));

                    plugin.SendPlayerYell(player.Name,
                        plugin.R("You have unlocked grenades for the rest of this round."), 10);

                }



                /****************************************************
                ** Check for PDW !unlocks
                ****************************************************/

                int headshotsPerPistol = 4;

                //if (player.EAGuid == "EA_8FDC8F1A15FD5C54570E31127EB33BF8")
                //{
                //    headshotsPerPistol = 5;
                //}

                if ( !player.RoundData.issetBool("Unlocked") || !player.RoundData.getBool("Unlocked") ) {

                    bool hasCompletedChallenge = true;

                    foreach ( string gun in pistolUnlock) {
                        if ( player[ gun ].HeadshotsRound < headshotsPerPistol ) {
                            hasCompletedChallenge = false;
                            break;
                        }
                    }

                    //foreach ( string gun in allowPistolsAuto ) {
                    //    if ( player[ gun ].HeadshotsRound < headshotsPerPistol ) {
                    //        hasCompletedChallenge = false;
                    //        break;
                    //    }
                    //}

                    //foreach ( string gun in allowPistolsRevolver ) {
                    //    if ( player[ gun ].HeadshotsRound < headshotsPerPistol ) {
                    //        hasCompletedChallenge = false;
                    //        break;
                    //    }
                    //}

                    if ( hasCompletedChallenge ) {

                        if ( !plugin.RoundData.issetString("UnlockPlayers") ) {
                            plugin.RoundData.setString("UnlockPlayers", "");
                        }

                        string unlockPlayers = plugin.RoundData.getString("UnlockPlayers");

                        unlockPlayers += player.Name + "|";

                        plugin.RoundData.setString("UnlockPlayers", unlockPlayers);

                        player.RoundData.setBool("Unlocked", true);
                        plugin.SendPlayerMessage(player.Name,
                            plugin.R("Congrats, you have just unlocked PDW's."));

                        plugin.SendPlayerYell(player.Name,
                            plugin.R("Congrats, you have just unlocked PDW's."), 5);

                        plugin.SendGlobalMessage(plugin.R("!!!! %p_n% has completed the !pdw challenge."));
                        plugin.SendGlobalYell(plugin.R("!!!! %p_n% has completed the !pdw challenge."),10);

                        plugin.PRoConChat(plugin.R("%p_n% completed the !pdw challenge."));

                    }
                }

                /****************************************************
                ** Check Phantom Bow Kills
                ****************************************************/
                int bowKillsUnlock = 30;

                if ( Regex.Match(kill.Weapon, @"(dlSHTR)", RegexOptions.IgnoreCase).Success ) {

                    /****************************************************
                    ** Punish if use Phantom before 50 kills - This is lazy.. come back to this and check they're actually pistol kills.
                    ****************************************************/
                    if ( player.KillsRound < bowKillsUnlock ) {
                        plugin.KillPlayer(player.Name);
                        plugin.SendPlayerMessage(player.Name,
                            plugin.R("You cannot use the Phantom Bow until you reach " + bowKillsUnlock +
                                      " kills. Type !bow for your progress."));

                        plugin.SendPlayerYell(player.Name,
                            plugin.R("You cannot use the Phantom Bow until you reach " + bowKillsUnlock +
                                      " kills. Type !bow for your progress."), 5);
                    }
                }

                /****************************************************
                ** Unlock Phantom after 50 kills - This is lazy.. come back to this and check they're actually pistol kills.
                ****************************************************/
                if ( player.KillsRound == bowKillsUnlock ) {
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You have unlocked the phantom bow."));

                    plugin.SendPlayerYell(player.Name,
                        plugin.R("You have unlocked the phantom bow."), 5);

                    plugin.PRoConChat(plugin.R("%p_n% has unlocked the Phantom Bow."));
                }


                /****************************************************
                ** Check for Sniper !sniper
                ****************************************************/

                int bowHeadshots = 10;

                if ( !player.RoundData.issetBool("UnlockedSniper") || !player.RoundData.getBool("UnlockedSniper") ) {

                    if ( player[ "dlSHTR" ].HeadshotsRound >= bowHeadshots ) {

                        if ( !plugin.RoundData.issetString("UnlockSnipers") ) {
                            plugin.RoundData.setString("UnlockSnipers", "");
                        }

                        string unlockPlayers = plugin.RoundData.getString("UnlockSnipers");

                        unlockPlayers += player.Name + "|";

                        plugin.RoundData.setString("UnlockSnipers", unlockPlayers);

                        player.RoundData.setBool("UnlockedSniper", true);
                        plugin.SendPlayerMessage(player.Name,
                            plugin.R("Congrats, you have just unlocked sniper rifles."));

                        plugin.SendPlayerYell(player.Name,
                            plugin.R("Congrats, you have just unlocked sniper rifles."), 5);

                        plugin.SendGlobalMessage(plugin.R("!!!! %p_n% has completed the !sniper challenge."));
                        plugin.SendGlobalYell(plugin.R("!!!! %p_n% has completed the !sniper challenge."), 10);

                        plugin.PRoConChat(plugin.R("%p_n% completed the !sniper challenge."));

                    }
                }

            } else {
                // This is not a pistol kill.

                int brokenRules = 0;

                if ( player.RoundData.issetInt("BrokenRules") ) {
                    brokenRules = player.RoundData.getInt("BrokenRules");
                }

                brokenRules++;

                player.RoundData.setInt("BrokenRules", brokenRules);

                KillReasonInterface formattedGun = plugin.FriendlyWeaponName(kill.Weapon);

                if ( brokenRules == 1 ) {

                    plugin.KillPlayer(player.Name);
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("%p_n%, this is the pistol round. Please don't use the " + formattedGun.Name + " again! Warning 1/3"));

                    plugin.SendPlayerYell(player.Name,
                        plugin.R("%p_n%, this is the pistol round. Please don't use the " + formattedGun.Name + " again! Warning 1/3"), 10);

                    plugin.PRoConChat(plugin.R("%p_n% has been killed for using the " + formattedGun.Name + "."));

                    plugin.SendGlobalMessage(plugin.R("%p_n% has been killed for using the " + formattedGun.Name + "."));


                } else if ( brokenRules == 2 ) {

                    plugin.KillPlayer(player.Name);
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("%p_n%, this is the pistol round. Please don't use the " + formattedGun.Name + " again! Warning 2/3"));

                    plugin.SendPlayerYell(player.Name,
                        plugin.R("%p_n%, this is the pistol round. Please don't use the " + formattedGun.Name + " again! Warning 2/3"), 10);

                    plugin.PRoConChat(plugin.R("%p_n% has been killed for using the " + formattedGun.Name + "."));

                    plugin.SendGlobalMessage(plugin.R("%p_n% has been killed for using the " + formattedGun.Name + "."));

                } else if ( brokenRules >= 3 ) {

                    plugin.KickPlayerWithMessage(player.Name,
                      plugin.R("%p_n%, kicked you for using the " + formattedGun.Name + " on the pistol only round. [Auto-Admin]"));
                    plugin.PRoConChat(plugin.R("%p_n% has been kicked for using the " + formattedGun.Name + "."));
                    plugin.SendGlobalMessage(plugin.R("%p_n% has been kicked for using the " + formattedGun.Name + "."));

                }
            }

            return false;

        }

    }
}
