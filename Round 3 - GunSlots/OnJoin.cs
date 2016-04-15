using System;
using System.Collections.Generic;
using System.Text;

namespace Procon_Plugins.Round_3___GunSlots {
    class OnJoin : PluginBase {

        public static bool Code() {

            plugin.SendGlobalYell("Round 4 - GUNSLOTS ROUND. Type !gunslots for rules.", 30);

            return false;

        }


    }
}
