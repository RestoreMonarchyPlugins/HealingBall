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

        public string Help => $"{Instance.DefaultTranslations.Translate("HealHelp")}";

        public string Syntax => $"/heal (name/steamid) - {Help}";

        public List<string> Aliases => new List<string>() { };

        public List<string> Permissions => new List<string>() { "heal" };

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
                    target.Player.Heal();
                    SendChat(up, $"{Instance.DefaultTranslations.Translate("SuccessfullyHealedPlayer", target.CharacterName)}", Color.white);
                    if (Config.MessageHeal)
                        SendChat(target, $"{Instance.DefaultTranslations.Translate("YouWasHealed")}", Color.white);
                }
                else if (command.Length == 0)
                {
                    up.Player.Heal();
                    SendChat(up, $"{Instance.DefaultTranslations.Translate("SuccessfullyHealedYourself")}", Color.white);
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
                    target.Player.Heal();
                    SendConsole($"{Instance.DefaultTranslations.Translate("SuccessfullyHealedPlayer", target.CharacterName)}", ConsoleColor.White);
                    if (Config.MessageHeal)
                        SendChat(target, $"{Instance.DefaultTranslations.Translate("YouWasHealed")}", Color.white);
                }
                else SendConsole($"{Syntax}", ConsoleColor.White);
            }
        }
    }
}
