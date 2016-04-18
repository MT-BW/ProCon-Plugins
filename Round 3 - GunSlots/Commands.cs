using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Procon_Plugins.Round_3___GunSlots {

    class Commands : PluginBase {

        public static bool Code() {



            /****************************************************
            ** !Gunslots command
            ****************************************************/
            if ( Regex.Match( player.LastChat, @"^\!gunslots$", RegexOptions.IgnoreCase ).Success ) {
                plugin.SendPlayerMessage( player.Name,
                    plugin.R( "I========= GUNSLOT ROUND RULES =========I" ) );

                plugin.SendPlayerMessage( player.Name,
                    plugin.R( "You start with all weapons as normal." ) );

                plugin.SendPlayerMessage( player.Name,
                    plugin.R( "When you die, you spawn with no weapons." ) );

                plugin.SendPlayerMessage( player.Name,
                    plugin.R( "To get your weapons back, you must get a Melee kill." ) );

                plugin.SendPlayerMessage( player.Name,
                    plugin.R( "Type !slot to see whether your weapons are enabled." ) );

                plugin.SendPlayerMessage( player.Name,
                    plugin.R( "Melee kills: Knife, Defibs, Repair tool, shield." ) );

                //plugin.SendPlayerMessage(player.Name,
                //    plugin.R("I======================================I"));
            }

            /****************************************************
            ** !Gunslot command
            ****************************************************/
            if ( Regex.Match(player.LastChat, @"^\!slot$", RegexOptions.IgnoreCase).Success ) {

                if ( !player.RoundData.issetBool("hasBeenKnifed") ) {
                    player.RoundData.setBool("hasBeenKnifed", false);
                }

                plugin.SendPlayerMessage(player.Name,
                plugin.R("I========= YOUR GUNSLOT =========I"));

                if ( player.RoundData.getBool( "hasBeenKnifed" ) ) {

                    plugin.SendPlayerMessage(player.Name,
                    plugin.R("You don't have a gunslot. Get a Melee kill to continue."));

                    plugin.SendPlayerMessage(player.Name,
                    plugin.R("Type !gunslots for rules."));

                } else {

                    plugin.SendPlayerMessage(player.Name,
                    plugin.R("You now have a gunslot. You may use any weapon."));

                }

                //plugin.SendPlayerMessage(player.Name,
                //    plugin.R("I======================================I"));

            }

            return false;

        }



    }

}