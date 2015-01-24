using System;

namespace cApp.PositiveT.Rpg.Helpers
{
    static class Randomizer
    {
        private static readonly Random Rnd;

        static Randomizer()
        {
            Rnd = new Random();
        }

        public static int GetSome(int max, int min = 1)
        {
            return Rnd.Next(min, max);
        }
    }
}
