using System;
using System.Collections.Generic;
using System.Text;

namespace Procon_Plugins.Round_3___GunSlots {

    class OnDeath : PluginBase {


        public static bool Code() {

            if( killer.RoundData.getBool( "hasBeenKnifed" ) == true ) {

                if( !player.RoundData.getBool( "hasBeenKnifed" ) ) {

                    plugin.SendPlayerMessage( player.Name,
                        killer.Name + " killed you illegally. You -HAVE NOT- lost your gunslot." );

                    plugin.SendPlayerYell( player.Name,
                        killer.Name + " killed you illegally. You -HAVE NOT- lost your gunslot.", 10 );

                }


            } else {


                if ( !player.RoundData.getBool("hasBeenKnifed") ) {

                    player.RoundData.setBool("hasBeenKnifed", true);


                    plugin.SendPlayerMessage(player.Name,
                        plugin.R(
                                 "You have been killed and lost your gunslot! To get your weapons back, get a melee kill. Type !gunslots for rules."));

                    plugin.SendPlayerYell(player.Name,
                        plugin.R(
                                 "You have been killed and lost your gunslot! To get your weapons back, get a melee kill. Type !gunslots for rules."),
                        10);
                }

            }

            return false;
        }

    }

}