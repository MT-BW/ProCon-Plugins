using System;
using System.Collections.Generic;
using System.Text;

namespace Procon_Plugins.Dynamic_Tickets {
    class OnIntervalServer : PluginBase {


        // Expression: plugin.Data.issetBool("roundEnded")
        public static bool Code() {

            plugin.ServerCommand("vars.gameModeCounter", plugin.Data.getInt("gameModeCounter").ToString());
            plugin.ServerCommand("vars.roundTimeLimit", plugin.Data.getInt("roundTimeLimit").ToString());
            plugin.ConsoleWrite("^2Command SPAM!");
            return false;

        }

    }
}
