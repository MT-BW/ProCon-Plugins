using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using PRoConEvents;

namespace Procon_Plugins.Round_3___GunSlots {
    class Enforcer : PluginBase {


        public static bool Code() {

            if( ! player.RoundData.issetBool( "hasBeenKnifed" ) ) {
                player.RoundData.setBool( "hasBeenKnifed", false );
            }


            if( player.RoundData.getBool( "hasBeenKnifed" ) ) {

                string[] allowedMeleWeapons = new[] {
                    "EODBot", "U_Repairtool", "U_BallisticShield", "U_Defib", "Melee",
                    "Suicide", "SoldierCollision", "DamageArea", "Death",
                };

                string pattern = string.Format( "({0})", string.Join( "|", allowedMeleWeapons ) );

                if( Regex.Match( kill.Weapon, pattern, RegexOptions.IgnoreCase ).Success ) {

                    // it's a Melee 

                    player.RoundData.setBool( "hasBeenKnifed", false );

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("Well done, you've got your weapons back!"));

                    plugin.SendPlayerYell(player.Name,
                        plugin.R("Well done, you've got your weapons back!"), 10);



                } else {

                    // it's not a Mele kill

                    int brokenRules = 0;

                    if ( player.RoundData.issetInt("BrokenRules") ) {
                        brokenRules = player.RoundData.getInt("BrokenRules");
                    }

                    brokenRules++;

                    KillReasonInterface formattedGun = plugin.FriendlyWeaponName(kill.Weapon);

                    player.RoundData.setInt("BrokenRules", brokenRules);

                    if ( brokenRules == 1 ) {

                        plugin.KillPlayer(player.Name);
                        plugin.SendPlayerMessage(player.Name,
                            plugin.R("You were killed by another player and lost your gunslot. Get your weapons back by getting a melee kill. Type !gunslots for rules. Warning 1/3"));

                        plugin.SendPlayerYell(player.Name,
                            plugin.R("You were killed by another player and lost your gunslot. Get your weapons back by getting a melee kill. Type !gunslots for rules. Warning 1/3"), 10);

                        plugin.PRoConChat(plugin.R("%p_n% has been killed for using " + formattedGun.Name + " without having a gunslot."));

                        plugin.SendGlobalMessage(plugin.R("%p_n% has been killed for using the " + formattedGun.Name + " without earning the gunslot."));


                    } else if ( brokenRules == 2 ) {

                        plugin.KillPlayer(player.Name);
                        plugin.SendPlayerMessage(player.Name,
                            plugin.R("You were killed by another player and lost your gunslot. Get your weapons back by getting a melee kill. Type !gunslots for rules. Warning 2/3"));

                        plugin.SendPlayerYell(player.Name,
                            plugin.R("You were killed by another player and lost your gunslot. Get your weapons back by getting a melee kill. Type !gunslots for rules. Warning 2/3"), 10);

                        plugin.PRoConChat(plugin.R("%p_n% has been killed for using " + formattedGun.Name + " without having a gunslot."));

                        plugin.SendGlobalMessage(plugin.R("%p_n% has been killed for using the " + formattedGun.Name + " without earning the gunslot."));

                    } else if ( brokenRules >= 3 ) {

                        plugin.KickPlayerWithMessage(player.Name,
                            plugin.R("%p_n%, kicked you for using the " + formattedGun.Name + " without having a gunslot. [Auto-Admin]"));
                        plugin.PRoConChat(plugin.R("%p_n% has been kicked for using " + formattedGun.Name + " without having a gunslot."));
                        plugin.SendGlobalMessage(plugin.R("%p_n% has been kicked for using the " + formattedGun.Name + " without earning the gunslot."));

                    }

                }



            }

            return false;

        }

    }
}
