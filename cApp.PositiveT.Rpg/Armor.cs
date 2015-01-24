using cApp.PositiveT.Rpg.Helpers;
using cApp.PositiveT.Rpg.Infrastruct;

namespace cApp.PositiveT.Rpg
{
    class Armor
    {
        public int Defence { get; private set; }
        public int Cost { get; private set; }

        public Armor()
        {
            Defence = Randomizer.GetSome(Configuration.ArmorDefenceMax);
            Cost = Configuration.ArmorCost;
        }
    }
}