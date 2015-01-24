
namespace cApp.PositiveT.Rpg.Infrastruct
{
    public interface IHeroConfig
    {
        int HealHitPoints { get; }
        int HealRestHitPoints { get; }
        int HealCost { get; }
        int MonsterDamageAfterWinInProcent { get; }
        int MonsterDamageAfterFail { get; }
        int MonsterMoney { get; }
        int HeroDefaultWinChance { get; }
        int HeroMightFactor { get; }
        int HeroMaxWinChance { get; }
        int HeroMaxHitPoints { get; }
        int HeroStartHitPoints { get; }
        int HeroMight { get; }
        int HeroMoney { get; }
    }
}
