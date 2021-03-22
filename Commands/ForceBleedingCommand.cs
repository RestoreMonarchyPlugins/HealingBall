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

        public string Help => $"{Instance.Translations.Instance.Translate("ForceBleedingHelp")}";

        public string Syntax => $"/forcebl (name/steamid) - {Help}";

        public List<string> Aliases => new List<string>() {"forcebl", "fbl"};

        public List<string> Permissions => new List<string>() {"forcebleeding"};

        public void Execute(IRP caller, string[] command)
        {
            if (caller is UP up)
            {
                switch (command.Length)
                {
                    case 1:
                    {
                        if (!TryFindPlayer(command[0], out var target))
                        {
                            SendChat(up, $"{Instance.Translations.Instance.Translate("ErrorIncorrectPlayer")}",
                                Color.white);
                            return;
                        }

                        target.Player.life.serverSetBleeding(true);
                        SendChat(up,
                            $"{Instance.Translations.Instance.Translate("SuccessfullyForceBleeding", target.CharacterName)}",
                            Color.white);
                        if (Config.MessageForceBleeding)
                            SendChat(target, $"{Instance.Translations.Instance.Translate("YouBleedingNow")}",
                                Color.white);
                        break;
                    }
                    case 0:
                        up.Player.life.serverSetBleeding(true);
                        SendChat(up, $"{Instance.Translations.Instance.Translate("SuccessfullyForceBleedingYourself")}",
                            Color.white);
                        break;
                    default:
                        SendChat(up, $"{Syntax}", Color.white);
                        break;
                }
            }
            else
            {
                if (command.Length != 1)
                {
                    SendConsole($"{Syntax}", ConsoleColor.White);
                    return;
                }

                if (!TryFindPlayer(command[0], out var target))
                {
                    SendConsole($"{Instance.Translations.Instance.Translate("ErrorIncorrectPlayer")}",
                        ConsoleColor.White);
                    return;
                }

                target.Player.life.serverSetBleeding(true);
                SendConsole(
                    $"{Instance.Translations.Instance.Translate("SuccessfullyForceBleeding", target.CharacterName)}",
                    ConsoleColor.White);
                if (Config.MessageForceBleeding)
                    SendChat(target, $"{Instance.Translations.Instance.Translate("YouBleedingNow")}", Color.white);
            }
        }
    }
}