using SDG.Unturned;
using Steamworks;
using System;
using Color = UnityEngine.Color;
using Logger = Rocket.Core.Logging.Logger;
using UP = Rocket.Unturned.Player.UnturnedPlayer;

namespace HealingBall
{
    public static class Extensions
    {
        public static Configuration Config => Plugin.Instance.Configuration.Instance;

        public static void SendChat(UP up, string text, Color color)
        {
            ChatManager.serverSendMessage($"<color=green>[HealingBall]</color> {text}", color, null,
                up.SteamPlayer(), EChatMode.SAY, "", true);
        }

        public static void SendConsole(string text, ConsoleColor color)
        {
            Logger.Log(text, color);
        }

        public static bool TryFindPlayer(string parameter, out UP target)
        {
            target = ulong.TryParse(parameter, out var sid)
                ? UP.FromCSteamID(new CSteamID(sid))
                : UP.FromName(parameter);
            return target != null;
        }

        public static void Heal(this Player player)
        {
            player.life.askHeal(100, Config.HealBleeding, Config.HealBroken);
            if (Config.FillHunger)
                player.life.askEat(100);
            if (Config.FillThirst)
                player.life.askDrink(100);
            if (Config.HealToxic)
                player.life.askDisinfect(100);
        }
    }
}