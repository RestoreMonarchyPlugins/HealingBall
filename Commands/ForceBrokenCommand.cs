using Rocket.API;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using UnityEngine;
using static HealingBall.Extensions;
using Color = UnityEngine.Color;
using IRP = Rocket.API.IRocketPlayer;
using UP = Rocket.Unturned.Player.UnturnedPlayer;

namespace HealingBall
{
    public class ForceBrokenCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public Plugin Instance => Plugin.Instance;

        public Configuration Config => Plugin.Instance.Configuration.Instance;

        public string Name => "forcebroken";

        public string Help => $"{Instance.DefaultTranslations.Translate("ForceBrokenHelp")}";

        public string Syntax => $"/forcebr (name/steamid) - {Help}";

        public List<string> Aliases => new List<string>() { "forcebr", "fbr" };

        public List<string> Permissions => new List<string>() { "forcebroken" };

        public void Execute(IRP caller, string[] command)
        {
            if (caller is UP)
            {
                UP up = (UP)caller;
                if (command.Length == 1)
                {
                    if (!TryFindPlayer(command[0], out UP target))
                    {
                        SendChat(up, $"{Instance.DefaultTranslations.Translate("ErrorIncorrectPlayer")}", Color.white);
                        return;
                    }
                    target.Player.life.serverSetLegsBroken(true);
                    SendChat(up, $"{Instance.DefaultTranslations.Translate("SuccessfullyForceBroken", target.CharacterName)}", Color.white);
                    if (Config.MessageForceBroken)
                        SendChat(target, $"{Instance.DefaultTranslations.Translate("YourLegWasBroken")}", Color.white);
                }
                else if (command.Length == 0)
                {
                    up.Player.life.serverSetLegsBroken(true);
                    SendChat(up, $"{Instance.DefaultTranslations.Translate("SuccessfullyForceBrokenYourself")}", Color.white);
                }
                else SendChat(up, $"{Syntax}", Color.white);
            }
            else
            {
                if (command.Length == 1)
                {
                    if (!TryFindPlayer(command[0], out UP target))
                    {
                        SendConsole($"{Instance.DefaultTranslations.Translate("ErrorIncorrectPlayer")}", ConsoleColor.White);
                        return;
                    }
                    target.Player.life.serverSetLegsBroken(true);
                    SendConsole($"{Instance.DefaultTranslations.Translate("SuccessfullyForceBroken", target.CharacterName)}", ConsoleColor.White);
                    if (Config.MessageForceBroken)
                        SendChat(target, $"{Instance.DefaultTranslations.Translate("YourLegWasBroken")}", Color.white);
                }
                else SendConsole($"{Syntax}", ConsoleColor.White);
            }
        }
    }
}
