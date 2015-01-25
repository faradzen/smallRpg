using System;
using System.Linq;
using cApp.PositiveT.Rpg.Helpers;
using cApp.PositiveT.Rpg.Infrastruct;
using cApp.PositiveT.Rpg.Model;

namespace cApp.PositiveT.Rpg
{
    class Program
    {
        public enum ResponseType
        {
            Yes = 'y',
            No = 'n'
        }

        public enum ActionType
        {
            Fight = 'w',
            BuyWeapon = 'a',
            BuyArmor = 'd',
            Heal = 's',
            Help = 'h',
            Rest = 'q',
            GenerateAction = 'e'
        }

        static class Keys
        {
            public static readonly char[] Actions =
            {
                (char) ActionType.Fight,
                (char) ActionType.BuyArmor,
                (char) ActionType.BuyWeapon,
                (char) ActionType.Heal,
                (char) ActionType.Help,
                (char) ActionType.Rest,
                (char) ActionType.GenerateAction
            };

            public static char[] Response =
            {
                (char) ResponseType.No, (char) ResponseType.Yes
            };
        }

        static void Main()
        {
            Console.WriteLine("игра: Темный Лес.");
            Console.WriteLine("боевая задача: выйти из темного леса и спасать мир, истребляя светлых во имя темных сил.");
            Console.WriteLine("Для справки по управлению героем нажмите <H>");
            //check correct configuration            
            if (Configuration.GetErrorList().Any())
            {
                foreach (var errorMsg in Configuration.GetErrorList())
                {
                    Console.WriteLine(errorMsg);
                }
                Console.WriteLine("Ошибка в конфиге.");
                Console.ReadKey();
                return;
            }

            //initialize units
            var messenger = new Messenger();
            var heroConfig = Factory.GetHeroConfig();
            var hero = new Hero(messenger, heroConfig);
            var isEnd = false;

            //run game
            Console.WriteLine("...еще один дождливый день...не дай клинкам заржаветь!");
            while (!isEnd)
            {
                Console.WriteLine("стоим на распутье и думаем что делать:");
                var key = messenger.ReadSpecificKeys(Keys.Actions);

                switch ((ActionType)key)
                {
                    case ActionType.Help:
                        Console.WriteLine("w - пойти в лес и порубить со светлыми");
                        Console.WriteLine("a - покупка оружия. Только у нас вы можете приобрести клинки темного власетлина за полцены...покупаешь два, третий в подарок!");
                        Console.WriteLine("d - покупка снаряжения.");
                        Console.WriteLine("s - лечиться. В нашей клинике, имени камрада Сидорова, верного сподвижника темного властелина и грозы светлых стай, вы можете излечить даже застарелый склероз!");
                        Console.WriteLine("q - просто и бесплатно отдохнуть на свежем воздухе");
                        Console.WriteLine("e - сделать что ни будь...бот.");
                        break;
                    case ActionType.BuyArmor:
                        hero.BuyArmor();
                        break;
                    case ActionType.BuyWeapon:
                        hero.BuyWeapon();
                        break;
                    case ActionType.Fight:
                        hero.Fight();
                        break;
                    case ActionType.Heal:
                        hero.Healing();
                        break;
                    case ActionType.Rest:
                        hero.Rest();
                        break;
                    case ActionType.GenerateAction:
                        hero.DoSomething();
                        break;
                    default:
                        Console.WriteLine("и не надо нажмать на все подряд");
                        break;
                }
                if (hero.IsDead)
                {
                    Console.WriteLine(
                        "Дней прошло: {0}. Герой погиб! Темный властелин в печали, силы Света захватят мир и все потонет в вечном празднике...хотите еще раз помешать торжеству Света? (y/n)", hero.GetDays());
                    var response = messenger.ReadSpecificKeys(Keys.Response);
                    if (response.Equals((char)ResponseType.No))
                    {
                        isEnd = true;
                    }
                    else
                    {
                        hero.InitHero(heroConfig);
                    }
                }
            }
            Console.WriteLine("Игра закончена. Лето приближается...");
            Console.ReadKey();
        }
    }
}
