using System;
using System.Collections.Generic;
using cApp.PositiveT.Rpg.Helpers;
using cApp.PositiveT.Rpg.Infrastruct;

namespace cApp.PositiveT.Rpg.Model
{
    internal class Hero
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
        private readonly int _healCost;
        private readonly int _healMight;
        private readonly int _healRestMight;
        private readonly int _monsterDamageAfterWinInProcent;
        private readonly int _heroDefaultWinChance;
        private readonly int _heroMightFactor;
        private readonly int _heroMaxWinChance;
        private readonly int _monsterDamageAfterFail;
        private readonly int _monsterMoney;
        private int _days;

        public Hero(IMessenger messenger, IHeroConfig config)
        {
            _msg = messenger;
            _healMight = config.HealHitPoints;
            _healRestMight = config.HealRestHitPoints;
            _healCost = config.HealCost;
            _monsterDamageAfterWinInProcent = config.MonsterDamageAfterWinInProcent;
            _heroDefaultWinChance = config.HeroDefaultWinChance;
            _heroMightFactor = config.HeroMightFactor;
            _heroMaxWinChance = config.HeroMaxWinChance;
            _monsterDamageAfterFail = config.MonsterDamageAfterFail;
            _monsterMoney = config.MonsterMoney;
            InitHero(config);
        }

        /// <summary>
        /// создать или оживить героя.
        /// </summary>
        public void InitHero(IHeroConfig config)
        {
            Armors = new List<Armor>();
            Weapons = new List<Weapon>();
            HitPointsMax = config.HeroMaxHitPoints;
            HitPoints = config.HeroStartHitPoints;
            Might = config.HeroMight;
            Money = config.HeroMoney;
            _summaryItemsAttack = 0;
            _summaryItemsDefence = 0;
            _days = 0;
            IsDead = false;
            _msg.Write("================================================================================");
            _msg.Write("Из глубоких пучин, из дремучих лесов вышел на тропу войны простой солдат сил тьмы...");
        }

        public void PrintHeroInfo()
        {
            _msg.Write(String.Format("сводка за день {5}: жизнь={0}/{1}, мощь = {2}, броня = {3}, золото = {4} ", HitPoints,
                HitPointsMax, (Might + _summaryItemsAttack), _summaryItemsDefence, Money, _days));
            if (IsDead)
            {
                _msg.Write("Статус : помер ");
            }
        }

        private bool IsMonsterKilled()
        {
            var maxChance = _heroDefaultWinChance +
                            (Might + _summaryItemsAttack) * _heroMightFactor;
            var minChance = Math.Min(maxChance, _heroMaxWinChance);
            return Randomizer.GetSome(100) <= minChance;
        }

        public bool? Fight()
        {
            if (IsDead)
            {
                _msg.Write("Ты умер, родной, хватит драться. Сходи водички попей...живой.");
                return null;
            }
            int dmg;
            bool isKill;
            if (IsMonsterKilled())
            {
                var dmg1 = Math.Round((((double)HitPoints / 100) * _monsterDamageAfterWinInProcent), MidpointRounding.AwayFromZero);
                dmg = (int)dmg1;
                Damage(dmg);
                Money += _monsterMoney;
                _msg.Write("Ура! Пляшем ритуальный танец и собираем добычу...");
                isKill = true;
            }
            else
            {
                _msg.Write("ааа!....проклятые светлоухие жулики...опять получил по щам.");
                dmg = _monsterDamageAfterFail;
                Damage(dmg);
                isKill = false;
            }
            _msg.Write(String.Format("получено повреждений:{0}", dmg));
            PrintHeroInfo();
            _days++;
            return isKill;
        }

        public void BuyArmor()
        {
            if (IsDead)
            {
                _msg.Write("Кыш отседова, нежить мелкая!");
                return;
            }
            if (Money < Armor.Cost || (Money - Armor.Cost) < 0)
            {
                _msg.Write("эх, денег не хватает..");
                return;
            }
            var newArmor = new Armor();
            Armors.Add(newArmor);
            _summaryItemsDefence += newArmor.Defence;
            HitPointsMax += newArmor.Defence;
            Money -= Armor.Cost;
            if (newArmor.Defence > 1)
            {
                _msg.Write(String.Format("эх хороша кольчужка...добавил {0} к броне", newArmor.Defence));
            }
            else
            {
                _msg.Write("еще одна обвесочка!");
            }
            PrintHeroInfo();
            _days++;
        }

        public void BuyWeapon()
        {
            if (IsDead)
            {
                _msg.Write("Уходи, уходи давай...досвидания!");
                return;
            }
            if (Money < Weapon.Cost || (Money - Weapon.Cost) < 0)
            {
                _msg.Write("Нет денег - нет мечей!");
                return;
            }
            var newWeapon = new Weapon();
            Weapons.Add(newWeapon);
            _summaryItemsAttack += newWeapon.Attack;
            Money -= Weapon.Cost;
            if (newWeapon.Attack > 1)
            {
                _msg.Write(String.Format("хо-хо...новая фенечка на ножичек..аж {0} к мощи", newWeapon.Attack));
            }
            else
            {
                _msg.Write("ножичек засиял ярче");
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
            if (HitPoints.Equals(HitPointsMax))
            {
                _msg.Write("Зачем отдыхать? Пора в бой!");
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
            if (HitPoints.Equals(HitPointsMax))
            {
                _msg.Write("Пациент, вы полностью здоровы!");
                return;
            }
            if (Money < _healCost || (Money - _healCost) < 0)
            {
                _msg.Write("Извини, любезный, денег в долг не даем, без денег не лечим.");
                return;
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
            HitPoints = newHitPointsValue > HitPointsMax
                ? HitPointsMax
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