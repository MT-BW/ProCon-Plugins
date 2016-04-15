using System;
using System.Collections.Generic;
using System.Text;
using PRoConEvents;


namespace Procon_Plugins {
    public abstract class PluginBase {

        protected static PlayerInfoInterface player;
        protected static LimitInfoInterface limit;
        protected static TeamInfoInterface teamX;
        protected static ServerInfoInterface server;
        protected static KillInfoInterface kill;
        protected static PlayerInfoInterface killer;
        protected static PlayerInfoInterface victim;
        protected static PluginInterface plugin;

    }
}
