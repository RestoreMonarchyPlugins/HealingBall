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
    public class ForceBleedingCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public Plugin Instance => Plugin.Instance;

        public Configuration Config => Plugin.Instance.Configuration.Instance;

        public string Name => "forcebleeding";

        public string Help => $"{Instance.DefaultTranslations.Translate("ForceBleedingHelp")}";

        public string Syntax => $"/forcebl (name/steamid) - {Help}";

        public List<string> Aliases => new List<string>() { "forcebl", "fbl" };

        public List<string> Permissions => new List<string>() { "forcebleeding" };

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
                    target.Player.life.serverSetBleeding(true);
                    SendChat(up, $"{Instance.DefaultTranslations.Translate("SuccessfullyForceBleeding", target.CharacterName)}", Color.white);
                    if (Config.MessageForceBleeding)
                        SendChat(target, $"{Instance.DefaultTranslations.Translate("YouBleedingNow")}", Color.white);
                }
                else if (command.Length == 0)
                {
                    up.Player.life.serverSetBleeding(true);
                    SendChat(up, $"{Instance.DefaultTranslations.Translate("SuccessfullyForceBleedingYourself")}", Color.white);
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
                    target.Player.life.serverSetBleeding(true);
                    SendConsole($"{Instance.DefaultTranslations.Translate("SuccessfullyForceBleeding", target.CharacterName)}", ConsoleColor.White);
                    if (Config.MessageForceBleeding)
                        SendChat(target, $"{Instance.DefaultTranslations.Translate("YouBleedingNow")}", Color.white);
                }
                else SendConsole($"{Syntax}", ConsoleColor.White);
            }
        }
    }
}
