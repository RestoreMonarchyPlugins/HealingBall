using Rocket.API;
namespace HealingBall
{
    public class Configuration : IRocketPluginConfiguration
    {
        public int MaximumRadius;
        public bool HealBleeding;
        public bool HealBroken;
        public bool FillThirst;
        public bool FillHunger;
        public bool HealToxic;
        public bool MessageHeal;
        public bool MessageSetHealth;
        public bool MessageForceBleeding;
        public bool MessageForceBroken;
        public void LoadDefaults()
        {
            MaximumRadius = 1000;
            HealBleeding = true;
            HealBroken = true;
            FillThirst = true;
            FillHunger = true;
            HealToxic = true;
            MessageHeal = true;
            MessageSetHealth = true;
            MessageForceBleeding = true;
            MessageForceBroken = true;
    }
    }
}
