using System;
using System.Collections.Generic;
using System.Text;

namespace Procon_Plugins.Round_3___GunSlots {

    class OnDeath : PluginBase {


        public static bool Code() {

            player.RoundData.setBool( "hasBeenKnifed", true );


            plugin.SendPlayerMessage(player.Name,
                plugin.R("You have been killed and lost your gunslot! To get your weapons back, get a melee kill. Type !gunslots for rules."));

            plugin.SendPlayerYell(player.Name,
                plugin.R("You have been killed and lost your gunslot! To get your weapons back, get a melee kill. Type !gunslots for rules."), 10);




            return false;
        }

    }

}