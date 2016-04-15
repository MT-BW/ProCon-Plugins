using System;
using System.Collections.Generic;
using System.Text;

namespace Procon_Plugins.Round_0___Normal {
    class OnRoundStart : PluginBase {

        public static bool Code() {

            plugin.SendGlobalYell("Round 1 - Standard Conquest 3200", 30);

            return false;

        }


    }
}
