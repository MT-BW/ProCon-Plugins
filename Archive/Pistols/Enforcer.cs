using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using PRoConEvents;

namespace Procon_Plugins.Pistols {
    class Enforcer : PluginBase {

        public static bool Code() {
            

            var allowWeapons = new[] {
                    "U_SaddlegunSnp", "U_Glock18", "U_Handflare", "U_Flashbang", "U_M18", "U_M34", "U_Grenade_RGO", "U_V40",
                    "U_M67", "EODBot", "U_Unica6", "U_SW40", "U_DesertEagle", "U_Repairtool", "U_BallisticShield",
                    "U_Taurus44",
                    "U_HK45C", "U_CZ75", "U_FN57", "U_M1911", "U_M9", "U_MP412Rex", "U_MP443", "U_P226", "U_QSZ92",
                    "U_Defib",
                    "Melee", "Suicide", "SoldierCollision", "U_SerbuShorty", "DamageArea", "Death", "dlSHTR"
            };

            var pattern = string.Format( "({0})", string.Join( "|", allowWeapons ) );

            if( kill.Weapon != "U_M98B" && Regex.Match( kill.Weapon, pattern, RegexOptions.IgnoreCase ).Success ) {
                //This is a pistol kill.

                if( player.RoundData.issetBool( "BrokenChains" ) && player.RoundData.getBool( "BrokenChains" ) ) {
                    // They've broken their chains, let them go mad
                } else {
                    /****************************************************
                    ** Individual Weapon Limits
                    ****************************************************/
                    var weaponLimit = 30;

                    var warnAt = new[] {20, 25};

                    var currentWeaponCount = (int) player[ kill.Weapon ].KillsRound;

                    if( Array.IndexOf( warnAt, currentWeaponCount ) > -1 ) {
                        plugin.SendPlayerMessage( player.Name,
                            plugin.R( "You have " + currentWeaponCount + "/" + weaponLimit +
                                        " kills with the %w_n%. To unlock this weapon beyond " + weaponLimit +
                                        " kills, complete the round challenge with !unlocks." ) );

                        plugin.SendPlayerYell( player.Name,
                            plugin.R( "You have " + currentWeaponCount + "/" + weaponLimit +
                                        " kills with the %w_n%. To unlock this weapon beyond " + weaponLimit +
                                        " kills, complete the round challenge with !unlocks." ), 5 );
                    }

                    if( currentWeaponCount == weaponLimit ) {
                        plugin.SendPlayerMessage( player.Name,
                            plugin.R( "You have " + currentWeaponCount + "/" + weaponLimit +
                                        " kills with the %w_n%. If you use this weapon again you will be auto-killed. To unlock this weapon beyond " +
                                        weaponLimit + " kills, complete the round challenge with !unlocks." ) );

                        plugin.SendPlayerYell( player.Name,
                            plugin.R( "You have " + currentWeaponCount + "/" + weaponLimit +
                                        " kills with the %w_n%. If you use this weapon again you will be auto-killed. To unlock this weapon beyond " +
                                        weaponLimit + " kills, complete the round challenge with !unlocks." ), 5 );
                    }

                    if( currentWeaponCount > weaponLimit ) {
                        plugin.KillPlayer( player.Name );
                        plugin.SendPlayerMessage( player.Name,
                            plugin.R( "You have reached the kill limit on the %w_n%. To unlock this weapon beyond " +
                                        weaponLimit + " kills, complete the round challenge with !unlocks." ) );

                        plugin.SendPlayerYell( player.Name,
                            plugin.R( "You have reached the kill limit on the %w_n%. To unlock this weapon beyond " +
                                        weaponLimit + " kills, complete the round challenge with !unlocks." ), 10 );
                    }

                    /****************************************************
                    ** Check Grenade Kills
                    ****************************************************/
                    var grenadeKillsUnlock = 10;

                    var grenadeWeaponCodes = new[]
                    {"U_Handflare", "U_Flashbang", "U_M18", "U_M34", "U_Grenade_RGO", "U_V40", "U_M67"};
                    var grenadePattern = string.Format( "({0})", string.Join( "|", grenadeWeaponCodes ) );

                    if( Regex.Match( kill.Weapon, grenadePattern, RegexOptions.IgnoreCase ).Success ) {
                        /****************************************************
                        ** Punish if use grenades before 10 kills - This is lazy.. come back to this and check they're actually pistol kills.
                        ****************************************************/
                        if( player.KillsRound < grenadeKillsUnlock ) {
                            plugin.KillPlayer( player.Name );
                            plugin.SendPlayerMessage( player.Name,
                                plugin.R( "You cannot use Grenades until you reach " + grenadeKillsUnlock + " kills." ) );

                            plugin.SendPlayerYell( player.Name,
                                plugin.R( "You cannot use Grenades until you reach " + grenadeKillsUnlock + " kills." ),
                                10 );
                        }
                    }

                    /****************************************************
                    ** Unlock grenades after 10 kills - This is lazy.. come back to this and check they're actually pistol kills.
                    ****************************************************/
                    if( player.KillsRound == grenadeKillsUnlock ) {
                        plugin.SendPlayerMessage( player.Name,
                            plugin.R( "You have unlocked grenades for the rest of this round." ) );

                        plugin.SendPlayerYell( player.Name,
                            plugin.R( "You have unlocked grenades for the rest of this round." ), 5 );
                    }

                    /****************************************************
                    ** Check for Shorty 12G unlock
                    ****************************************************/
                    if( !player.RoundData.issetBool( "UnlockedShorty" ) ||
                        !player.RoundData.getBool( "UnlockedShorty" ) ) {
                        var gunsToKillWith = new[] {
                            "U_Glock18", "U_SW40", "U_DesertEagle", "U_Unica6", "U_M9", "U_MP412Rex", "U_MP443",
                            "U_P226",
                            "U_QSZ92", "U_Taurus44", "U_HK45C", "U_CZ75", "U_FN57", "U_M1911"
                        };

                        var hasCompletedChallenge = true;

                        foreach( var gun in gunsToKillWith ) {
                            if( player[ gun ].KillsRound <= 0 ) {
                                hasCompletedChallenge = false;
                                break;
                            }
                        }

                        if( hasCompletedChallenge ) {
                            player.RoundData.setBool( "UnlockedShorty", true );
                            plugin.SendPlayerMessage( player.Name,
                                plugin.R( "You have now unlocked the Shorty 12G." ) );

                            plugin.SendPlayerYell( player.Name,
                                plugin.R( "You have now unlocked the Shorty 12G." ), 5 );
                        }
                    }

                    /****************************************************
                    ** Check for Short 12G kill
                    ****************************************************/
                    if( Regex.Match( kill.Weapon, @"(U_SerbuShorty)", RegexOptions.IgnoreCase ).Success ) {
                        if( player.RoundData.issetBool( "UnlockedShorty" ) &&
                            player.RoundData.getBool( "UnlockedShorty" ) ) {
                            // They've unlocked it.. all is good
                        } else {
                            plugin.KillPlayer( player.Name );
                            plugin.SendPlayerMessage( player.Name,
                                plugin.R(
                                            "You must unlock the Shorty 12G before you can us it. To do this, get 1 kill with every pistol. To see remaining pistols type !shorty." ) );

                            plugin.SendPlayerYell( player.Name,
                                plugin.R(
                                            "You must unlock the Shorty 12G before you can us it. To see remaining pistols type !shorty." ),
                                10 );

                            plugin.SendGlobalMessage(
                                                        plugin.R(
                                                                "%p_n% has been killed for using the Shorty 12G without unlocking it." ) );
                        }
                    }

                    /****************************************************
                    ** Check Phantom Bow Kills
                    ****************************************************/
                    var bowKillsUnlock = 50;

                    if( Regex.Match( kill.Weapon, @"(dlSHTR)", RegexOptions.IgnoreCase ).Success ) {
                        /****************************************************
                        ** Punish if use Phantom before 50 kills - This is lazy.. come back to this and check they're actually pistol kills.
                        ****************************************************/
                        if( player.KillsRound < bowKillsUnlock ) {
                            plugin.KillPlayer( player.Name );
                            plugin.SendPlayerMessage( player.Name,
                                plugin.R( "You cannot use the Phantom Bow until you reach " + bowKillsUnlock +
                                            " kills. Type !bow for your progress." ) );

                            plugin.SendPlayerYell( player.Name,
                                plugin.R( "You cannot use the Phantom Bow until you reach " + bowKillsUnlock +
                                            " kills. Type !bow for your progress." ), 5 );
                        }
                    }

                    /****************************************************
                    ** Unlock Phantom after 50 kills - This is lazy.. come back to this and check they're actually pistol kills.
                    ****************************************************/
                    if( player.KillsRound == bowKillsUnlock ) {
                        plugin.SendPlayerMessage( player.Name,
                            plugin.R( "You have unlocked the phantom bow." ) );

                        plugin.SendPlayerYell( player.Name,
                            plugin.R( "You have unlocked the phantom bow." ), 5 );
                    }

                    /****************************************************
                    ** Check for chain breaking stats
                    ****************************************************/

                    var gunsToBreakChains = new[] {"Melee", "U_Repairtool", "U_BallisticShield", "U_Defib"};

                    var hasBrokenChains = true;

                    foreach( var gun in gunsToBreakChains ) {
                        if( player[ gun ].KillsRound <= 1 ) {
                            hasBrokenChains = false;
                            break;
                        }
                    }

                    if( hasBrokenChains ) {
                        player.RoundData.setBool( "BrokenChains", true );
                        plugin.SendPlayerMessage( player.Name,
                            plugin.R(
                                        "You have completed the unlock for this round, you may now use any pistol without limits. You have also unlocked the Short 12G and Phantom Bow." ) );

                        plugin.SendPlayerYell( player.Name,
                            plugin.R(
                                        "You have completed the unlock for this round, you may now use any pistol without limits." ),
                            5 );
                    }
                }
            } else {
                // This is not a pistol kill.

                var brokenRules = 0;

                if( player.RoundData.issetInt( "BrokenRules" ) ) {
                    brokenRules = player.RoundData.getInt( "BrokenRules" );
                }

                brokenRules++;

                player.RoundData.setInt( "BrokenRules", brokenRules );

                if( brokenRules == 1 ) {
                    plugin.KillPlayer( player.Name );
                    plugin.SendPlayerMessage( player.Name,
                        plugin.R( "%p_n%, this is the pistol round. Please don't use the %w_n% again! Warning 1/3" ) );

                    plugin.SendPlayerYell( player.Name,
                        plugin.R( "%p_n%, this is the pistol round. Please don't use the %w_n% again! Warning 1/3" ), 10 );

                    plugin.PRoConChat( plugin.R( "%p_n% has been killed for using %w_n%" ) );
                    plugin.SendGlobalMessage( plugin.R( "%p_n% has been killed for using %w_n%" ) );
                } else if( brokenRules == 2 ) {
                    plugin.KillPlayer( player.Name );
                    plugin.SendPlayerMessage( player.Name,
                        plugin.R( "%p_n%, this is the pistol round. Please don't use the %w_n% again! Warning 2/3" ) );

                    plugin.SendPlayerYell( player.Name,
                        plugin.R( "%p_n%, this is the pistol round. Please don't use the %w_n% again! Warning 2/3" ), 10 );

                    plugin.PRoConChat( plugin.R( "%p_n% has been killed for using %w_n%" ) );
                    plugin.SendGlobalMessage( plugin.R( "%p_n% has been killed for using %w_n%" ) );
                } else if( brokenRules >= 3 ) {
                    plugin.KickPlayerWithMessage( player.Name,
                        plugin.R( "%p_n%, kicked you for using %w_n% on the pistol only round" ) );
                    plugin.PRoConChat( plugin.R( "%p_n% has been kicked for using %w_n%" ) );
                    plugin.SendGlobalMessage( plugin.R( "%p_n% has been kicked for using %w_n%" ) );
                }
            }

            return false;

        }

    }
}
