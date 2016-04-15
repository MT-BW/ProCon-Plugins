using System;
using System.Collections.Generic;
using System.Text;

namespace Procon_Plugins.Round_3___GunSlots {
    class OnRoundEnd : PluginBase {

        public static bool Code() {

            plugin.SendGlobalYell("Next Round is Round 5 - Standard Conquest 3200", 30);

            plugin.ServerCommand("vars.serverDescription", "ROUND 5. General rules: No Mortar/UCAV/SUAV, No M320 (LVG, 3GL, HE), No airburst & No rockets. // Get a 1-month VIP slot for 2.5 euros! Info on http://monkeygaming.net");

            plugin.ServerCommand("vars.serverMessage ", "Welcome to RedMonkey Operation Locker high tickets! play fair, be nice and have fun. Please type !rules");

            return false;

        }


    }
}
