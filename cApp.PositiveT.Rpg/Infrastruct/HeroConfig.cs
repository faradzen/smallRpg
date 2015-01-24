namespace cApp.PositiveT.Rpg.Infrastruct
{
    public class HeroConfig : IHeroConfig
    {
        public int HealHitPoints { get; set; }
        public int HealRestHitPoints { get; set; }
        public int HealCost { get; set; }
        public int MonsterDamageAfterWin { get; set; }
        public int MonsterDamageAfterFail { get; set; }
        public int HeroDefaultWinChance { get; set; }
        public int HeroMightFactor { get; set; }
        public int HeroMaxWinChance { get; set; }
        public int HeroMaxHitPoints { get; set; }
        public int HeroStartHitPoints { get; set; }
        public int HeroMight { get; set; }
        public int HeroMoney { get; set; }

        public HeroConfig()
        {

        }
    }
}