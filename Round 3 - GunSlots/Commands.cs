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
            if ( Regex.Match( player.LastChat, @"^\!gunslots", RegexOptions.IgnoreCase ).Success ) {
                plugin.SendPlayerMessage( player.Name,
                    plugin.R( "I========= GUNSLOT ROUND RULES =========I" ) );

                plugin.SendPlayerMessage( player.Name,
                    plugin.R( "You start with all weapons as normal." ) );

                plugin.SendPlayerMessage( player.Name,
                    plugin.R( "When somebody kills you, you spawn with no weapons." ) );

                plugin.SendPlayerMessage( player.Name,
                    plugin.R( "To get your weapons back, you must get a Melee kill." ) );

                plugin.SendPlayerMessage( player.Name,
                    plugin.R( "After you get a Melee kill, you can continue as normal." ) );

                plugin.SendPlayerMessage( player.Name,
                    plugin.R( "Melee kills: Knife, Defibs, Repair tool, EOD Bot, shield." ) );

                plugin.SendPlayerMessage(player.Name,
                    plugin.R("I======================================I"));
            }

            return false;

        }



    }

}