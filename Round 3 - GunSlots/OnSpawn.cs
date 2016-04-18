using System;
using System.Collections.Generic;
using System.Text;

namespace Procon_Plugins.Round_3___GunSlots {
    class OnSpawn : PluginBase {


        public static bool Code() {

            if ( limit.Activations(player.Name) < 1 ) {

                plugin.SendGlobalYell("Round 4 - GUNSLOTS ROUND. Type !gunslots for rules.", 30);

            }

            if ( !player.RoundData.issetBool("hasBeenKnifed") ) {
                player.RoundData.setBool("hasBeenKnifed", false);
            }

            if ( limit.Activations(player.Name) > 0 ) {

                if( player.RoundData.getBool( "hasBeenKnifed" ) ) {

                    plugin.SendPlayerYell(player.Name,
                        plugin.R("You have been killed and lost your gunslot! To get your weapons back, get a melee kill. Type !gunslots for rules."), 10);

                }

            }

            return false;

        }


    }
}
