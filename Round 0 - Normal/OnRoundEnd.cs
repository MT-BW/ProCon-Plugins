using System;
using System.Collections.Generic;
using System.Text;

namespace Procon_Plugins.Round_0___Normal {
    class OnRoundEnd : PluginBase {

        public static bool Code() {

            plugin.SendGlobalYell( "Next Round is Round 2 - Pistols Only w/ Unlocks", 30 );

            plugin.ServerCommand("vars.serverDescription", "ROUND 2: PISTOLS ONLY. TYPE !PISTOLS WHEN IN-GAME. General rules: No Mortar/UCAV/SUAV, No M320 (LVG, 3GL, HE), No airburst & No rockets. // Get a 1-month VIP slot for 2.5 euros! Info on http://monkeygaming.net");

            plugin.ServerCommand("vars.serverMessage ", "Welcome to RedMonkey Lockers only 3200! Current round is pistols only. Please type !pistols & !rules.");

            return false;


        }


    }
}
