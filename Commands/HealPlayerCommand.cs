using Rocket.API;
using Steamworks;
using System;
using System.Collections.Generic;
using static HealingBall.Extensions;
using Color = UnityEngine.Color;
using IRP = Rocket.API.IRocketPlayer;
using UP = Rocket.Unturned.Player.UnturnedPlayer;

namespace HealingBall
{
    public class HealPlayer : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public Plugin Instance => Plugin.Instance;

        public Configuration Config => Plugin.Instance.Configuration.Instance;

        public string Name => "heal";

        public string Help => $"{Instance.Translations.Instance.Translate("HealHelp")}";

        public string Syntax => $"/heal (name/steamid) - {Help}";

        public List<string> Aliases => new List<string>() { };

        public List<string> Permissions => new List<string>() {"heal"};

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

                        target.Player.Heal();
                        SendChat(up,
                            $"{Instance.Translations.Instance.Translate("SuccessfullyHealedPlayer", target.CharacterName)}",
                            Color.white);
                        if (Config.MessageHeal)
                            SendChat(target, $"{Instance.Translations.Instance.Translate("YouWasHealed")}", Color.white);
                        break;
                    }
                    case 0:
                        up.Player.Heal();
                        SendChat(up, $"{Instance.Translations.Instance.Translate("SuccessfullyHealedYourself")}",
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

                target.Player.Heal();
                SendConsole(
                    $"{Instance.Translations.Instance.Translate("SuccessfullyHealedPlayer", target.CharacterName)}",
                    ConsoleColor.White);
                if (Config.MessageHeal)
                    SendChat(target, $"{Instance.Translations.Instance.Translate("YouWasHealed")}", Color.white);
            }
        }
    }
}