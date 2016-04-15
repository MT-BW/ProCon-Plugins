using System;
using System.Collections.Generic;
using System.Text;

namespace Procon_Plugins.Round_1___PistolsV2 {
    class OnSpawn : PluginBase {

        public static bool Code() {


            if( limit.Activations( player.Name ) < 2 ) {

                plugin.SendGlobalYell("Round 2 - PISTOL ONLY ROUND. Type !pistols for rules. !nades, !limits, !unlocks, !bow, !unlockers", 10);

            }


            

            return false;

        }


    }
}
