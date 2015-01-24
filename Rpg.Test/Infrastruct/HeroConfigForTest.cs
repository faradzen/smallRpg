
using cApp.PositiveT.Rpg.Infrastruct;

namespace Rpg.Test.Infrastruct
{
    class HeroConfigForTest : IHeroConfig
    {
        public int HealHitPoints { get; set; }
        public int HealRestHitPoints { get; set; }
        public int HealCost { get; set; }
        public int MonsterDamageAfterWinInProcent { get; set; }
        public int MonsterDamageAfterFail { get; set; }
        public int MonsterMoney { get; set; }
        public int HeroDefaultWinChance { get; set; }
        public int HeroMightFactor { get; set; }
        public int HeroMaxWinChance { get; set; }
        public int HeroMaxHitPoints { get; set; }
        public int HeroStartHitPoints { get; set; }
        public int HeroMight { get; set; }
        public int HeroMoney { get; set; }
    }
}
