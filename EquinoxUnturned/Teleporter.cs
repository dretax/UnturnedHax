using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using UnityEngine;

namespace EquinoxUnturned
{
    public class Teleporter : IRocketCommand
    {
        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = caller as UnturnedPlayer;
            if (player == null)
            {
                return;
            }
            if (command.Length == 3)
            {
                float i, i2, i3;
                if (float.TryParse(command[0], out i) && float.TryParse(command[1], out i2) &&
                    float.TryParse(command[2], out i3))
                {
                    var v = new Vector3(i, i2, i3);
                    player.Teleport(v, 0);
                }
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
                return "Teleporter";
            }
        }

        public string Name
        {
            get
            {
                return "tpc";
            }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string> { "equinox.tpc" };
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
                return "<tpc>";
            }
        }


    }
}
