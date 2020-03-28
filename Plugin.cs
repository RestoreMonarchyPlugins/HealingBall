using Rocket.API.Collections;
using Rocket.Core.Plugins;
using System;
using System.Reflection;
using Logger = Rocket.Core.Logging.Logger;

namespace HealingBall
{
    public class Plugin : RocketPlugin<Configuration>
    {
        public static Plugin Instance = null;

        protected override void Load()
        {
            try
            {
                Instance = this;
                Logger.Log($"[RestoreMonarchy] {Name} {Assembly.GetName().Version} has been loaded!", ConsoleColor.Yellow);
                Logger.Log($"[RestoreMonarchy] Plugin made by Greenorine", ConsoleColor.Yellow);
            }
            catch (Exception ex)
            {
                Logger.Log($"[ERROR] Unknown Error", ConsoleColor.Red);
                Logger.Log(ex.ToString(), ConsoleColor.White);
                base.UnloadPlugin();
            }
        }

        protected override void Unload()
        {
            Logger.Log($"[RestoreMonarchy] {Name} has been unloaded!", ConsoleColor.Yellow);
            Instance = null;
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "HealBallHelp", "Heals players in ball with radius x." },
            { "SuccessfullyHealedRadius", "Successfully healed {0} players in radius {1}." },
            { "YouWasHealed", "You was healed by the Administrator." },
            { "IncorrectRadius", "The specified radius is greater than the maximum allowed radius (Max: {0})" },
            { "SuccessfullyHealed", "Successfully healed all players." },
            { "HealHelp", "Heals player." },
            { "SuccessfullyHealedYourself", "You was healed." },
            { "SuccessfullyHealedPlayer", "Successfully healed player {0}." },
            { "SetHealthHelp", "Sets player's health to specified count." },
            { "SuccessfullySetHealth", "Successfully set health for player {0} to {1}." },
            { "HealthWasChanged", "Your health was changed to {0} by the Administrator." },
            { "ErrorIncorrectPlayer", "This player doesn't exist!" },
            { "ErrorIncorrectCount", "You entered an invalid value!" },
            { "ForceBleedingHelp", "Makes player bleeding." },
            { "SuccessfullyForceBroken", "Successfully set bleeding true for player {0}." },
            { "SuccessfullyForceBleedingYourself", "Now you bleeding." },
            { "YouBleedingNow", "You was made bleeding by the Administrator." },
            { "ForceBrokenHelp", "Breaks a player's leg." },
            { "SuccessfullyForceBroken", "Successfully broken leg for player {0}." },
            { "SuccessfullyForceBrokenYourself", "Successfully broken leg." },
            { "YourLegWasBroken", "Your leg was broken by the Administator." }
        };
    }
}
