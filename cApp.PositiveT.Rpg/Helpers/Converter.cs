
using cApp.PositiveT.Rpg.Infrastruct;

namespace cApp.PositiveT.Rpg.Helpers
{
    internal static class Factory
    {
        internal static HeroConfig GetHeroConfig()
        {
            var src = new HeroConfig();
            src.HealHitPoints = Configuration.HealHitPoints;
            src.HealRestHitPoints = Configuration.HealRestHitPoints;
            src.HealCost = Configuration.HealCost;
            src.MonsterDamageAfterWin = Configuration.MonsterDamageAfterWin;
            src.MonsterDamageAfterFail = Configuration.MonsterDamageAfterFail;
            src.HeroDefaultWinChance = Configuration.HeroDefaultWinChance;
            src.HeroMightFactor = Configuration.HeroMightFactor;
            src.HeroMaxWinChance = Configuration.HeroMaxWinChance;
            src.HeroMaxHitPoints = Configuration.HeroMaxHitPoints;
            src.HeroStartHitPoints = Configuration.HeroStartHitPoints;
            src.HeroMight = Configuration.HeroMight;
            src.HeroMoney = Configuration.HeroMoney;
            return src;
        }
    }
}
