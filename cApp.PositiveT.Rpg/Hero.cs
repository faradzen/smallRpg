using System;
using System.Collections.Generic;
using cApp.PositiveT.Rpg.Helpers;
using cApp.PositiveT.Rpg.Infrastruct;

namespace cApp.PositiveT.Rpg
{
    class Hero
    {
        private readonly IMessenger _msg;
        
        public int HitPointsMax { get; private set; }
        public int HitPoints { get; private set; }
        public int Might { get; private set; }
        public int Money { get; private set; }
        public List<Armor> Armors { get; private set; }
        public List<Weapon> Weapons { get; private set; }
        public bool IsDead { get; private set; }
        private int _summaryItemsAttack;
        private int _summaryItemsDefence;
        private int _healCost;
        private int _healMight;
        private int _healRestMight;
        private int _days;

        public Hero(IMessenger messenger)
        {
            _msg = messenger;
            InitHero();
        }

        /// <summary>
        /// создать или оживить героя.
        /// </summary>
        public void InitHero()
        {
            Armors = new List<Armor>();
            Weapons = new List<Weapon>();
            HitPointsMax = Configuration.HeroMaxHitPoints;
            HitPoints = Configuration.HeroStartHitPoints;
            Might = Configuration.HeroMight;
            Money = Configuration.HeroMoney;
            _healMight = Configuration.HealHitPoints;
            _healRestMight = Configuration.HealRestHitPoints;
            _healCost = Configuration.HealCost;
            _summaryItemsAttack = 0;
            _summaryItemsDefence = 0;
            _days = 0;
            IsDead = false;
            _msg.Write("================================================================================");
            _msg.Write("Из глубоких пучин, из дремучих лесов вышел на тропу войны простой солдат сил тьмы...");
        }

        public void PrintHeroInfo()
        {
            _msg.Write(String.Format("сводка: жизнь={0}/{1}, мощь = {2}, броня = {3}, золото = {4} ", HitPoints,
                HitPointsMax, (Might + _summaryItemsAttack), _summaryItemsDefence, Money));
            if (IsDead)
            {
                _msg.Write("Статус : ЗОМБИ ");
            }
        }

        private bool IsMonsterKilled()
        {
            var maxChance = Configuration.HeroDefaultWinChance +
                            (Might + _summaryItemsAttack) * Configuration.HeroMightFactor;
            var minChance = Math.Min(maxChance, Configuration.HeroMaxWinChance);
            return Randomizer.GetSome(100) <= minChance;
        }

        public void Fight()
        {
            if (IsDead)
            {
                _msg.Write("Ты умер, родной, хватит драться. Сходи водички попей...живой.");
                return;
            }
            int dmg;
            if (IsMonsterKilled())
            {
                var dmg1 = Math.Round((double)HitPoints / Configuration.MonsterDamageAfterWin, MidpointRounding.AwayFromZero);
                dmg = (int)dmg1;
                Damage(dmg);
                Money += 5;
                _msg.Write("Ура! Пляшем ритуальный танец и собираем добычу...");
            }
            else
            {
                _msg.Write("ааа!....проклятые светлоухие жулики...опять получил по щам.");
                dmg = Configuration.MonsterDamageAfterFail;
                Damage(dmg);
            }
            _msg.Write(String.Format("получено повреждений:{0}", dmg));
            PrintHeroInfo();
            _days++;
        }

        public void BuyArmor()
        {
            if (IsDead)
            {
                _msg.Write("Кыш отседова, нежить мелкая!");
            }
            var newArmor = new Armor();
            Armors.Add(newArmor);
            _summaryItemsDefence += newArmor.Defence;
            if (newArmor.Defence > 1)
            {
                _msg.Write(String.Format("эх хороша кольчужка...добавил {0} к броне", newArmor.Defence));
            }
            else
            {
                _msg.Write("перековываем, дорабатываем, допиливаем железки");
            }
            PrintHeroInfo();
            _days++;
        }

        public void BuyWeapon()
        {
            if (IsDead)
            {
                _msg.Write("Уходи, уходи давай...досвидания!");
            }
            var newWeapon = new Weapon();
            Weapons.Add(newWeapon);
            _summaryItemsAttack += newWeapon.Attack;
            if (newWeapon.Attack > 1)
            {
                _msg.Write(String.Format("хо-хо...новая фенечка на ножичек..аж {0} к мощи", newWeapon.Attack));
            }
            else
            {
                _msg.Write("точим-точим ножик ночью темной...");
            }
            PrintHeroInfo();
            _days++;
        }

        public void Rest()
        {
            if (IsDead)
            {
                _msg.Write("нежить тоже хочет отдыхать...");
                return;
            }
            if (HitPoints > HitPointsMax * 0.7)
            {
                _msg.Write("хмм...да я отдохнул вроде как...может в бой?");
            }
            _msg.Write("ромашки, одуваны...лежу в кустах я пьяный...");
            Heal(_healRestMight);
            PrintHeroInfo();
            _days++;
        }

        public void Healing()
        {
            if (HitPoints < 0)
            {
                _msg.Write("Извини, любезный, нежить не лечим.");
                return;
            }
            if ((Money - _healCost) < 0)
            {
                _msg.Write("Извини, любезный, денег в долг не даем, без денег не лечим.");
            }
            Heal(_healMight);
            Money -= _healCost;
            _msg.Write("пластыри, мази, припарки, скальпели...фуу..страшно лечиться.");
            if (HitPoints == HitPointsMax)
            {
                _msg.Write("хмм...под завязочку..а не зря ли я деньги трачу?");
            }
            PrintHeroInfo();
            _days++;
        }

        private void Heal(int amount)
        {
            var newHitPointsValue = HitPoints + amount;
            var maxHits = HitPointsMax + _summaryItemsDefence;
            HitPoints = newHitPointsValue > maxHits
                ? maxHits
                : newHitPointsValue;
        }

        private void Damage(int dmg)
        {
            if ((HitPoints - dmg) < 0)
            {
                //dead
                HitPoints = 0;
                IsDead = true;
            }
            else
            {
                HitPoints -= dmg;
            }
        }
    }
}