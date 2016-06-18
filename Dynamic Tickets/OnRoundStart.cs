using System;
using System.Collections.Generic;
using System.Text;

namespace Procon_Plugins.Dynamic_Tickets {
    class OnRoundStart : PluginBase {


        public static bool Code() {

            plugin.ServerCommand("mapList.list", "0");
            plugin.ServerCommand("mapList.getMapIndices");
            plugin.ServerCommand("serverInfo");
            plugin.Data.unsetBool("roundEnded");
            plugin.ConsoleWrite("Default Settings");

            plugin.ConsoleWrite("Round started - type:" +server.Gamemode + ", tickets: " + plugin.Data.getInt("gameModeCounter").ToString() + ", time: " + plugin.Data.getInt("roundTimeLimit").ToString());

            return false;

        }

    }
}
