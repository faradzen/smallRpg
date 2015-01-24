
using cApp.PositiveT.Rpg.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rpg.Test.Infrastruct;

namespace Rpg.Test.Model
{
    [TestClass]
    public class HeroTest
    {
        private static HeroConfigForTest GetConfig(int hitPoints)
        {
            return new HeroConfigForTest
            {
                HealCost = 10,
                HealHitPoints = 10,
                HealRestHitPoints = 3,
                HeroDefaultWinChance = 50,
                HeroMaxHitPoints = hitPoints,
                HeroMaxWinChance = 70,
                HeroMight = 0,
                HeroMightFactor = 5,
                HeroMoney = 100,
                HeroStartHitPoints = hitPoints,
                MonsterDamageAfterFail = 40,
                MonsterDamageAfterWinInProcent = 10,
                MonsterMoney = 10
            };
        }

        private static Hero GetHero(HeroConfigForTest config)
        {
            var messenger = new MessengerForTest();
            var hero = new Hero(messenger, config);
            return hero;
        }

        [TestMethod]
        public void TestFight()
        {
            var config = GetConfig(983);
            var hero = GetHero(config);
            for (int i = 0; i < 100; i++)
            {
                var moneyBefore = hero.Money;
                var hitPointsBefore = hero.HitPoints;
                var res = hero.Fight();
                //check
                //if (config.HeroStartHitPoints - hero.HitPoints == config.MonsterDamageAfterFail)
                if (!res.HasValue)
                {
                    Assert.AreEqual(true, hero.IsDead);
                    break;
                }
                if (res.Value)
                {
                    //hero win
                    Assert.AreEqual(moneyBefore + config.MonsterMoney, hero.Money);
                    var calcNewHits = ((double)hitPointsBefore / 100) * (100 - config.MonsterDamageAfterWinInProcent);
                    Assert.AreEqual(true, calcNewHits + 1 >= hero.HitPoints);
                    Assert.AreEqual(true, calcNewHits - 1 <= hero.HitPoints);
                }
                else
                {
                    //hero fail
                    Assert.AreEqual(moneyBefore, hero.Money);
                    if (hero.IsDead)
                    {
                        Assert.AreEqual(true, hitPointsBefore < config.MonsterDamageAfterFail);
                    }
                    else
                    {
                        Assert.AreEqual(config.MonsterDamageAfterFail, hitPointsBefore - hero.HitPoints);
                    }
                }
            }
        }
    }
}
