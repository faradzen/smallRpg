using cApp.PositiveT.Rpg.Helpers;
using cApp.PositiveT.Rpg.Infrastruct;

namespace cApp.PositiveT.Rpg.Model
{
    class Weapon
    {
        public int Attack { get; private set; }
        public static int Cost { get; private set; }

        static Weapon()
        {
            Cost = Configuration.WeaponCost;
        }

        public Weapon()
        {
            Attack = Randomizer.GetSome(Configuration.WeaponAttackMax + 1);
        }
    }
}