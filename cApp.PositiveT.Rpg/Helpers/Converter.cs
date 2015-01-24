
using cApp.PositiveT.Rpg.Infrastruct;

namespace cApp.PositiveT.Rpg.Helpers
{
    internal static class Factory
    {
        internal static HeroConfig GetHeroConfig()
        {
            var src = new HeroConfig
                      {
                          HealHitPoints = Configuration.HealHitPoints,
                          HealRestHitPoints = Configuration.HealRestHitPoints,
                          HealCost = Configuration.HealCost,
                          MonsterDamageAfterWinInProcent = Configuration.MonsterDamageAfterWin,
                          MonsterDamageAfterFail = Configuration.MonsterDamageAfterFail,
                          MonsterMoney = Configuration.MonsterMoney,
                          HeroDefaultWinChance = Configuration.HeroDefaultWinChance,
                          HeroMightFactor = Configuration.HeroMightFactor,
                          HeroMaxWinChance = Configuration.HeroMaxWinChance,
                          HeroMaxHitPoints = Configuration.HeroMaxHitPoints,
                          HeroStartHitPoints = Configuration.HeroStartHitPoints,
                          HeroMight = Configuration.HeroMight,
                          HeroMoney = Configuration.HeroMoney
                      };
            return src;
        }
    }
}
