using System;
using System.Collections.Generic;
using System.Text;

namespace Procon_Plugins.Dynamic_Tickets {
    class OnRoundEnd : PluginBase {


        public static bool Code() {

            plugin.ServerCommand("mapList.getMapIndices");
            plugin.ServerCommand("mapList.getRounds");
            plugin.ServerCommand("serverInfo");

            int nextRound = server.CurrentRound + 2;
            if ( nextRound > server.TotalRounds ) nextRound = 1;

            if ( ( server.CurrentRound + 1 ) < server.TotalRounds ) {
                plugin.ConsoleWrite("^2Next round is ^b" + plugin.FriendlyMapName(server.MapFileName) + "^n on ^b" + plugin.FriendlyModeName(server.Gamemode) + "^n, round " + ( server.CurrentRound + 1 ) + " of " + server.TotalRounds + ", with " + server.PlayerCount + " players remaining");
                return false;
            }

            plugin.ConsoleWrite("^2Next map is ^b" + plugin.FriendlyMapName(server.NextMapFileName) + "^n on ^b" + plugin.FriendlyModeName(server.NextGamemode) + "^n, round " + nextRound + ", with " + server.PlayerCount + " players remaining");

            int gameModeCounter = 100;
            int roundTimeLimit = 100;

            if ( server.NextGamemode == "ConquestLarge0" ) { // CQ LARGE
                gameModeCounter = 125; 

                plugin.ConsoleWrite("^2Setting vars.gameModeCounter to " + gameModeCounter + " for CQ LArge.");
                plugin.ServerCommand("vars.gameModeCounter", gameModeCounter.ToString());

            } else if ( server.NextGamemode == "ConquestSmall0" ) { // CQ SMALL
                gameModeCounter = 200;

                plugin.ConsoleWrite("^2Setting vars.gameModeCounter to " + gameModeCounter + " for CQ small.");
                plugin.ServerCommand("vars.gameModeCounter", gameModeCounter.ToString());

            } else if ( server.NextGamemode == "RushLarge0" ) {  // RUSH LARGE
                gameModeCounter = 300;

                plugin.ConsoleWrite("^2Setting vars.gameModeCounter to " + gameModeCounter + " for Rush.");
                plugin.ServerCommand("vars.gameModeCounter", gameModeCounter.ToString());

            } else if ( server.NextGamemode == "Obliteration" ) { // OBLITERATION
                gameModeCounter = 100;

                plugin.ConsoleWrite("^2Setting vars.gameModeCounter to " + gameModeCounter + " for Oblitteration.");
                plugin.ServerCommand("vars.gameModeCounter", gameModeCounter.ToString());

                roundTimeLimit = 85;

                plugin.ConsoleWrite("^2Setting vars.roundTimeLimit to " + roundTimeLimit + " for Obliteration.");
                plugin.ServerCommand("vars.roundTimeLimit", roundTimeLimit.ToString());

            } else if ( server.NextGamemode == "Domination0" ) {  // DOMINATION

                gameModeCounter = 200;

                plugin.ConsoleWrite("^2Setting vars.gameModeCounter to " + gameModeCounter + " for Domination.");
                plugin.ServerCommand("vars.gameModeCounter", gameModeCounter.ToString());

            } else if ( server.NextGamemode == "TeamDeathMatch0" ) {  // TDM

                gameModeCounter = 400;

                plugin.ConsoleWrite("^2Setting vars.gameModeCounter to " + gameModeCounter + " for TDM.");
                plugin.ServerCommand("vars.gameModeCounter", gameModeCounter.ToString());

            } else if ( server.NextGamemode == "Chainlink0" ) {  // Chainlink

                gameModeCounter = 300;

                plugin.ConsoleWrite("^2Setting vars.gameModeCounter to " + gameModeCounter + " for Chainlink.");
                plugin.ServerCommand("vars.gameModeCounter", gameModeCounter.ToString());

            } else if ( server.NextGamemode == "CaptureTheFlag0" ) { // CTF

                gameModeCounter = 120; 

                plugin.ConsoleWrite("^2Setting vars.roundTimeLimit to " + roundTimeLimit + ". for CTF");
                plugin.ServerCommand("vars.roundTimeLimit", roundTimeLimit.ToString());
            }

            plugin.Data.setInt("gameModeCounter", gameModeCounter);
            plugin.Data.setInt("roundTimeLimit", roundTimeLimit);
            plugin.Data.setBool("roundEnded", true);

            return false;

        }

    }
}
