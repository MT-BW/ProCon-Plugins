using System;
using System.Collections.Generic;
using System.Text;

namespace Procon_Plugins.Round_3___GunSlots {
    class OnSpawn : PluginBase {

        public static bool Code() {

            if ( limit.Activations(player.Name) < 1 ) {
                
                plugin.SendGlobalYell("Round 2 - PISTOL ONLY ROUND. Type !pistols for rules. !nades, !limits, !unlocks, !bow, !unlockers", 10);

            }

            if ( !player.RoundData.issetBool("hasBeenKnifed") ) {
                player.RoundData.setBool("hasBeenKnifed", false);
            }

            if ( limit.Activations(player.Name) > 0 && limit.Activations(player.Name) < 4 ) {

                if( player.RoundData.getBool( "hasBeenKnifed" ) ) {

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You have been killed and lost your gunslot! To get your weapons back, get a melee kill. Type !gunslots for rules."));

                    plugin.SendPlayerYell(player.Name,
                        plugin.R("You have been killed and lost your gunslot! To get your weapons back, get a melee kill. Type !gunslots for rules."), 10);

                }

            }

            return false;

        }


    }
}
