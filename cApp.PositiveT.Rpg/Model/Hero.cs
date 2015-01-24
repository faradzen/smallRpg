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
        /// ������� ��� ������� �����.
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
            _msg.Write("�� �������� �����, �� �������� ����� ����� �� ����� ����� ������� ������ ��� ����...");
        }

        public void PrintHeroInfo()
        {
            _msg.Write(String.Format("������ �� ���� {5}: �����={0}/{1}, ���� = {2}, ����� = {3}, ������ = {4} ", HitPoints,
                HitPointsMax, (Might + _summaryItemsAttack), _summaryItemsDefence, Money, _days));
            if (IsDead)
            {
                _msg.Write("������ : ����� ");
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
                _msg.Write("�� ����, ������, ������ �������. ����� ������� �����...�����.");
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
                _msg.Write("���! ������ ���������� ����� � �������� ������...");
                isKill = true;
            }
            else
            {
                _msg.Write("���!....��������� ���������� ������...����� ������� �� ���.");
                dmg = _monsterDamageAfterFail;
                Damage(dmg);
                isKill = false;
            }
            _msg.Write(String.Format("�������� �����������:{0}", dmg));
            PrintHeroInfo();
            _days++;
            return isKill;
        }

        public void BuyArmor()
        {
            if (IsDead)
            {
                _msg.Write("��� ��������, ������ ������!");
                return;
            }
            if (Money < Armor.Cost || (Money - Armor.Cost) < 0)
            {
                _msg.Write("��, ����� �� �������..");
                return;
            }
            var newArmor = new Armor();
            Armors.Add(newArmor);
            _summaryItemsDefence += newArmor.Defence;
            HitPointsMax += newArmor.Defence;
            Money -= Armor.Cost;
            if (newArmor.Defence > 1)
            {
                _msg.Write(String.Format("�� ������ ���������...������� {0} � �����", newArmor.Defence));
            }
            else
            {
                _msg.Write("��� ���� ���������!");
            }
            PrintHeroInfo();
            _days++;
        }

        public void BuyWeapon()
        {
            if (IsDead)
            {
                _msg.Write("�����, ����� �����...����������!");
                return;
            }
            if (Money < Weapon.Cost || (Money - Weapon.Cost) < 0)
            {
                _msg.Write("��� ����� - ��� �����!");
                return;
            }
            var newWeapon = new Weapon();
            Weapons.Add(newWeapon);
            _summaryItemsAttack += newWeapon.Attack;
            Money -= Weapon.Cost;
            if (newWeapon.Attack > 1)
            {
                _msg.Write(String.Format("��-��...����� ������� �� �������..�� {0} � ����", newWeapon.Attack));
            }
            else
            {
                _msg.Write("������� ������ ����");
            }
            PrintHeroInfo();
            _days++;
        }

        public void Rest()
        {
            if (IsDead)
            {
                _msg.Write("������ ���� ����� ��������...");
                return;
            }
            if (HitPoints.Equals(HitPointsMax))
            {
                _msg.Write("����� ��������? ���� � ���!");
                return;
            }
            if (HitPoints > HitPointsMax * 0.7)
            {
                _msg.Write("���...�� � �������� ����� ���...����� � ���?");
            }
            _msg.Write("�������, �������...���� � ������ � ������...");
            Heal(_healRestMight);
            PrintHeroInfo();
            _days++;
        }

        public void Healing()
        {
            if (HitPoints < 0)
            {
                _msg.Write("������, ��������, ������ �� �����.");
                return;
            }
            if (HitPoints.Equals(HitPointsMax))
            {
                _msg.Write("�������, �� ��������� �������!");
                return;
            }
            if (Money < _healCost || (Money - _healCost) < 0)
            {
                _msg.Write("������, ��������, ����� � ���� �� ����, ��� ����� �� �����.");
                return;
            }
            Heal(_healMight);
            Money -= _healCost;
            _msg.Write("��������, ����, ��������, ���������...���..������� ��������.");
            if (HitPoints == HitPointsMax)
            {
                _msg.Write("���...��� ���������..� �� ��� �� � ������ �����?");
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