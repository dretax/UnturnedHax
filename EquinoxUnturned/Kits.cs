using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace EquinoxUnturned
{
    public class Kits : IRocketCommand

    {
        public int Cooldown = 2100000;
        public void Execute(IRocketPlayer caller, string[] command)
        {
            bool yukon = false;
            if (Provider.map.ToLower().Contains("yukon"))
            {
                yukon = true;
                Cooldown = 200000;
            }
            UnturnedPlayer player = caller as UnturnedPlayer;
            if (player == null)
            {
                return;
            }
            if (EquinoxUnturned.Storage.ContainsKey(player.CSteamID.ToString()))
            {
                var playercd = EquinoxUnturned.Storage[player.CSteamID.ToString()];
                var systick = System.Environment.TickCount;
                bool giveit = double.IsNaN(systick - playercd) || (systick - playercd) < 0;
                var calc = systick - playercd;
                if (calc >= Cooldown || giveit)
                {
                    if (!yukon)
                    {
                        player.GiveItem(95, 2);
                        player.GiveItem(391, 1);
                        player.GiveItem(276, 1);
                    }
                    else
                    {
                        player.GiveItem(221, 1);
                        player.GiveItem(214, 1);
                        player.GiveItem(95, 2);
                        player.GiveItem(276, 1);
                        player.GiveItem(362, 1);
                    }
                    UnturnedChat.Say(player.CSteamID, "You got the kit!", Color.cyan);
                    EquinoxUnturned.Storage[player.CSteamID.ToString()] = System.Environment.TickCount;
                }
                else
                {
                    var done = (calc/1000)/60;
                    var done2 = (Cooldown/1000)/60;
                    UnturnedChat.Say(player.CSteamID,
                        "Time Remaining: " + Math.Round((double) done, 2, MidpointRounding.ToEven) + "/" +
                        Math.Round((double) done2, 2, MidpointRounding.ToEven) + " minutes");
                }
            }
            else
            {
                if (!yukon)
                {
                    player.GiveItem(95, 2);
                    player.GiveItem(391, 1);
                    player.GiveItem(276, 1);
                }
                else
                {
                    player.GiveItem(221, 1);
                    player.GiveItem(214, 1);
                    player.GiveItem(95, 2);
                    player.GiveItem(276, 1);
                    player.GiveItem(362, 1);
                }
                UnturnedChat.Say(player.CSteamID, "You got the kit!", Color.cyan);
                EquinoxUnturned.Storage[player.CSteamID.ToString()] = System.Environment.TickCount;
            }
        }

        // Properties
        public List<string> Aliases
        {
            get
            {
                return new List<string>();
            }
        }

        public AllowedCaller AllowedCaller
        {
            get
            {
                return AllowedCaller.Player;
            }
        }

        public string Help
        {
            get
            {
                return "Gives you a kit";
            }
        }

        public string Name
        {
            get
            {
                return "kit";
            }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string> { "equinox.kit" };
            }
        }

        public bool RunFromConsole
        {
            get
            {
                return false;
            }
        }

        public string Syntax
        {
            get
            {
                return "<kit>";
            }
        }


    }
}
