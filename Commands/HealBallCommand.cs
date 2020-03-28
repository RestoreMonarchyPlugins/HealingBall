using Rocket.API;
using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;
using static HealingBall.Extensions;
using Color = UnityEngine.Color;
using IRP = Rocket.API.IRocketPlayer;
using UP = Rocket.Unturned.Player.UnturnedPlayer;

namespace HealingBall
{
    public class HealBallCommand : IRocketCommand
    {
        public Plugin Instance => Plugin.Instance;

        public Configuration Config => Plugin.Instance.Configuration.Instance;

        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "healball";

        public string Help => $"{Instance.DefaultTranslations.Translate("HealBallHelp")}";

        public string Syntax => $"/healball [radius] - {Help}";

        public List<string> Aliases => new List<string>() { "healb", "hball", "hb" };

        public List<string> Permissions => new List<string>() { "healball" };

        public void Execute(IRP caller, string[] command)
        {
            UP up = (UP)caller;
            if (command.Length == 1)
            {
                if (command[0] == "*")
                {
                    foreach (var pl in Provider.clients)
                    {
                        pl.player.Heal();
                        if (Config.MessageHeal)
                            SendChat(UP.FromSteamPlayer(pl), $"{Instance.DefaultTranslations.Translate("YouWasHealed")}", Color.white);
                    }
                    SendChat(up, $"{Instance.DefaultTranslations.Translate("SuccessfullyHealed")}", Color.white);
                }
                else
                {
                    if (int.Parse(command[0]) > 0 && int.Parse(command[0]) <= Config.MaximumRadius)
                    {
                        var pls = Provider.clients.FindAll(x => Vector3.Distance(up.Position, UP.FromSteamPlayer(x).Position) <= int.Parse(command[0]) && x.playerID.steamID.m_SteamID != up.CSteamID.m_SteamID);
                        foreach (var pl in pls)
                        {
                            pl.player.Heal();
                            if (Config.MessageHeal)
                                SendChat(UP.FromSteamPlayer(pl), $"{Instance.DefaultTranslations.Translate("YouWasHealed")}", Color.white);
                        }
                        SendChat(up, $"{Instance.DefaultTranslations.Translate("SuccessfullyHealedRadius", pls.Count, command[0])}", Color.white);
                    }
                    else
                        SendChat(up, $"{Instance.DefaultTranslations.Translate("IncorrectRadius", Config.MaximumRadius)}", Color.white);
                }
            }
            else SendChat(up, $"{Syntax}", Color.white);
        }
    }
}
