using System.Collections.Generic;
using System.Configuration;

namespace cApp.PositiveT.Rpg.Infrastruct
{
    static class Configuration
    {
        private static readonly List<string> ErrorList;

        private static int GetFromAppConfig(string appConfigName)
        {
            int val;
            if (!int.TryParse(ConfigurationManager.AppSettings[appConfigName], out val))
            {
                ErrorList.Add("error config initialize: hero.startHitPoints");
                return 0;
            }
            return val;
        }

        static Configuration()
        {
            ErrorList = new List<string>();
            HeroStartHitPoints = GetFromAppConfig("hero.startHitPoints");
            HeroMaxHitPoints = GetFromAppConfig("hero.maxHitPoints");
            HeroMight = GetFromAppConfig("hero.might");
            HeroMoney = GetFromAppConfig("hero.money");
            HeroMightFactor = GetFromAppConfig("hero.mightFactor");
            HeroMaxWinChance = GetFromAppConfig("hero.maxWinChance");
            HeroDefaultWinChance = GetFromAppConfig("hero.defaultWinChance");
            MonsterDamageAfterWin = GetFromAppConfig("monster.damageAfterWinInProcent");
            MonsterDamageAfterFail = GetFromAppConfig("monster.damageAfterFail");
            MonsterMoney = GetFromAppConfig("monster.money");
            WeaponCost = GetFromAppConfig("magazine.weaponCost");
            WeaponAttackMax = GetFromAppConfig("magazine.weaponAttackMax");
            ArmorCost = GetFromAppConfig("magazine.armorCost");
            ArmorDefenceMax = GetFromAppConfig("magazine.armorDefenceMax");
            HealHitPoints = GetFromAppConfig("magazine.healHitPoints");
            HealRestHitPoints = GetFromAppConfig("magazine.healRestHitPoints");
            HealCost = GetFromAppConfig("magazine.healCost");
        }

        public static List<string> GetErrorList()
        {
            return ErrorList;
        }

        public static int HeroStartHitPoints { get; private set; }
        public static int HeroMaxHitPoints { get; private set; }
        public static int HeroMight { get; private set; }
        public static int HeroMoney { get; private set; }
        public static int HeroMightFactor { get; private set; }
        public static int HeroMaxWinChance { get; private set; }
        public static int HeroDefaultWinChance { get; private set; }
        public static int MonsterDamageAfterWin { get; private set; }
        public static int MonsterDamageAfterFail { get; private set; }
        public static int MonsterMoney { get; private set; }
        public static int WeaponCost { get; private set; }
        public static int WeaponAttackMax { get; private set; }
        public static int ArmorCost { get; private set; }
        public static int ArmorDefenceMax { get; private set; }
        public static int HealHitPoints { get; private set; }
        public static int HealRestHitPoints { get; private set; }
        public static int HealCost { get; private set; }
    }
}
