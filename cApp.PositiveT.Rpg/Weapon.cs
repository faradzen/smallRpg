using cApp.PositiveT.Rpg.Helpers;
using cApp.PositiveT.Rpg.Infrastruct;

namespace cApp.PositiveT.Rpg
{
    class Weapon
    {
        public int Attack { get; private set; }
        public int Cost { get; private set; }

        public Weapon()
        {
            Attack = Randomizer.GetSome(Configuration.WeaponAttackMax);
            Cost = Configuration.WeaponCost;
        }
    }
}