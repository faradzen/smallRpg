using cApp.PositiveT.Rpg.Helpers;
using cApp.PositiveT.Rpg.Infrastruct;

namespace cApp.PositiveT.Rpg.Model
{
    class Armor
    {
        public int Defence { get; private set; }
        public static int Cost { get; private set; }

        static Armor()
        {
            Cost = Configuration.ArmorCost;
        }

        public Armor()
        {
            Defence = Randomizer.GetSome(Configuration.ArmorDefenceMax + 1);     
        }
    }
}