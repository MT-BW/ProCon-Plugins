using System;
using System.Collections.Generic;
using System.Text;

namespace Procon_Plugins.Dynamic_Tickets
{

    using System.Text.RegularExpressions;

    class OnAnyChat : PluginBase
    {

        public static bool Code() {

            plugin.ServerCommand( "mapList.getMapIndices" );
            plugin.ServerCommand( "mapList.getRounds" );
            plugin.ServerCommand( "serverInfo" );

            if( Regex.Match( player.LastChat, @"^\!nextround", RegexOptions.IgnoreCase ).Success ) {
                plugin.SendGlobalYell(
                                      "Next round is ^b" + plugin.FriendlyMapName( server.MapFileName ) + "^n on ^b"
                                      + plugin.FriendlyModeName( server.Gamemode ) + "^n, round "
                                      + ( server.CurrentRound + 1 ) + " of " + server.TotalRounds + ", with "
                                      + server.PlayerCount + " players remaining",
                    20 );

            }

        }

    }
}