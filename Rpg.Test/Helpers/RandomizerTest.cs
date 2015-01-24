using System.Collections.Generic;
using System.Linq;
using cApp.PositiveT.Rpg.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rpg.Test.Helpers
{
    [TestClass]
    public class RandomizerTest
    {
        [TestMethod]
        public void TestWork()
        {
            // var rnd = new Randomizer();
            var i = 0;
            const int maxVal = 10;
            var values = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            while (i < 1000 && values.Any())
            {
                i++;
                var next = Randomizer.GetSome(maxVal);
                if (values.Any(f => f.Equals(next)))
                {
                    values.Remove(next);
                }
            }
            Assert.AreEqual(false, values.Any());
        }
    }
}
