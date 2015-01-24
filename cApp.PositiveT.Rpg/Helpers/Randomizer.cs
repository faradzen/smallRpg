using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Rpg.Test")]
namespace cApp.PositiveT.Rpg.Helpers
{
   
    internal static class Randomizer
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
