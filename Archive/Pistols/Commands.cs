using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Procon_Plugins.Pistols {
    class Commands : PluginBase {

        public static bool Code() {

            if ( Regex.Match(player.LastChat, @"^\!unlocks", RegexOptions.IgnoreCase).Success ) {
                // string[] gunsToBreakChains = new string[] { "Melee", "U_Repairtool", "U_BallisticShield", "U_Defib" };

                var knife = (int) player[ "Melee" ].KillsRound;
                var reptool = (int) player[ "U_Repairtool" ].KillsRound;
                var shield = (int) player[ "U_BallisticShield" ].KillsRound;
                var defib = (int) player[ "U_Defib" ].KillsRound;

                if ( knife >= 2 && reptool >= 2 && shield >= 2 && defib >= 2 ) {
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I=========== UNLOCKS  ===========I"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You have completed the challenge. You may use all pistols unlimited now."));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I==========================================I"));
                } else {
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I=========== UNLOCKS  ===========I"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You have not yet completed the challenge!"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You have " + knife + "/2 kills with knife, " + reptool + "/2 kills with repair tool, " +
                                  shield + "/2 kills with shield, " + defib + "/2 kills with defibs. "));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("Keep going!"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I==========================================I"));
                }
            }

            if ( Regex.Match(player.LastChat, @"^\!shorty", RegexOptions.IgnoreCase).Success ) {
                var gunsToKillWith = new[] {
                "U_Glock18", "U_SW40", "U_DesertEagle", "U_Unica6", "U_M9", "U_MP412Rex", "U_MP443", "U_P226", "U_QSZ92",
                "U_Taurus44", "U_HK45C", "U_CZ75", "U_FN57", "U_M1911"
            };

                var hasCompletedChallenge = true;

                foreach ( var gun in gunsToKillWith ) {
                    if ( player[ gun ].KillsRound <= 0 ) {
                        hasCompletedChallenge = false;
                        break;
                    }
                }

                if ( hasCompletedChallenge ) {
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I=========== Shorty 12G  ===========I"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You have completed the challenge and unlocked the shorty!"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I==========================================I"));
                } else {
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I=========== Shorty 12G  ===========I"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You haven't unlocked the shorty yet. Remaining guns:"));

                    var guns = "";

                    foreach ( var gun in gunsToKillWith ) {
                        if ( player[ gun ].KillsRound <= 0 ) {
                            var formattedGun = gun.Replace("U_", "");

                            if ( formattedGun == "HK45C" ) {
                                formattedGun = "COMPACT 45";
                            } else if ( formattedGun == "Taurus44" ) {
                                formattedGun = "44 MAGNUM";
                            }

                            guns += formattedGun + ", ";
                        }
                    }

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R(guns));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I==========================================I"));
                }
            }

            if ( Regex.Match(player.LastChat, @"^\!bow", RegexOptions.IgnoreCase).Success ) {
                if ( player.KillsRound >= 50 ) {
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I=========== BOW  ===========I"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You have unlocked the bow."));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I==========================================I"));
                } else {
                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I=========== BOW  ===========I"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("You haven't unlocked the bow yet!"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R(player.KillsRound + "/50 kills done"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("Keep going!"));

                    plugin.SendPlayerMessage(player.Name,
                        plugin.R("I==========================================I"));
                }
            }

            return false;
        }

    }
}
