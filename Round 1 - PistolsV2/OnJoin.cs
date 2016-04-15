using System;
using System.Collections.Generic;
using System.Text;

namespace Procon_Plugins.Round_1___PistolsV2 {
    class OnJoin : PluginBase {

        public static bool Code() {


            plugin.SendGlobalYell("Round 2 - PISTOL ONLY ROUND. Type !pistols for rules. !nades, !limits, !unlocks, !bow, !unlockers", 30);

            return false;

        }


    }
}
