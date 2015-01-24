using System;
using System.Globalization;
using System.Linq;

namespace cApp.PositiveT.Rpg.Infrastruct
{
    class Messenger : IMessenger
    {
        public void Write(string s)
        {
            Console.WriteLine(s);
        }

        public string ReadKey()
        {
            return Console.ReadKey().ToString();
        }

        public string ReadSpecificKey(char keyValue)
        {
            while (!Console.ReadKey().KeyChar.Equals(keyValue))
            {
                return keyValue.ToString(CultureInfo.InvariantCulture);
            }
            return String.Empty;
        }

        public char ReadSpecificKeys(char[] keyValues)
        {
            var check = false;
            char pushed = ' ';
            while (!check)
            {
                pushed = Console.ReadKey().KeyChar;
                if (keyValues.Any(f => f.Equals(pushed)))
                {
                    check = true;
                }
            }
            return pushed;
        }
    }
}
