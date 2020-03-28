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
    public class SetHealthCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public Plugin Instance => Plugin.Instance;

        public Configuration Config => Plugin.Instance.Configuration.Instance;

        public string Name => "sethealth";

        public string Help => $"{Instance.DefaultTranslations.Translate("SetHealthHelp")}";

        public string Syntax => $"/sethealth [name/steamid] [amount] - {Help}";

        public List<string> Aliases => new List<string>() { "sethp" };

        public List<string> Permissions => new List<string>() { "sethealth" };

        public void Execute(IRP caller, string[] command)
        {
            if (caller is UP)
            {
                UP up = (UP)caller;
                if (command.Length == 2)
                {
                    var health = int.Parse(command[1]);
                    if (health == 0)
                    {
                        SendChat(up, $"{Instance.DefaultTranslations.Translate("ErrorIncorrectCount")}", Color.white);
                        return;
                    }
                    if (!TryFindPlayer(command[0], out UP target))
                    {
                        SendChat(up, $"{Instance.DefaultTranslations.Translate("ErrorIncorrectPlayer")}", Color.white);
                        return;
                    }
                    if (target.Health < health)
                        target.Heal((byte)(health - int.Parse(target.Health.ToString())));
                    else
                        target.Damage((byte)(int.Parse(target.Health.ToString()) - health), Vector3.zero, EDeathCause.PUNCH, ELimb.SKULL, CSteamID.Nil);
                    SendChat(up, $"{Instance.DefaultTranslations.Translate("SuccessfullySetHealth", target.CharacterName, health)}", Color.white);
                    if (Config.MessageSetHealth)
                        SendChat(target, $"{Instance.DefaultTranslations.Translate("HealthWasChanged", health)}", Color.white);
                }
                else SendChat(up, $"{Syntax}", Color.white);
            }
            else
            {
                if (command.Length == 2)
                {
                    var health = int.Parse(command[1]);
                    if (health == 0)
                    {
                        SendConsole($"{Instance.DefaultTranslations.Translate("ErrorIncorrectCount")}", ConsoleColor.White);
                        return;
                    }
                    if (!TryFindPlayer(command[0], out UP target))
                    {
                        SendConsole($"{Instance.DefaultTranslations.Translate("ErrorIncorrectPlayer")}", ConsoleColor.White);
                        return;
                    }
                    if (target.Health < health)
                        target.Heal((byte)(health - int.Parse(target.Health.ToString())));
                    else
                    {
                        target.Damage((byte)(int.Parse(target.Health.ToString()) - health), Vector3.zero, EDeathCause.KILL, ELimb.LEFT_ARM, CSteamID.Nil);
                        target.Player.life.serverSetBleeding(false);
                    }
                    SendConsole($"{Instance.DefaultTranslations.Translate("SuccessfullySetHealth", target.CharacterName, health)}", ConsoleColor.White);
                    if (Config.MessageSetHealth)
                        SendChat(target, $"{Instance.DefaultTranslations.Translate("HealthWasChanged", health)}", Color.white);
                }
                else SendConsole($"{Syntax}", ConsoleColor.White);
            }
        }
    }
}
