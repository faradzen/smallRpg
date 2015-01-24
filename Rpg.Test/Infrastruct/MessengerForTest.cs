using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using cApp.PositiveT.Rpg.Infrastruct;

namespace Rpg.Test.Infrastruct
{
    class MessengerForTest : IMessenger
    {
        private string _key;

        public void Write(string s)
        {
            
        }

        public void SetReadingKey(string key)
        {
            _key = key;
        }

        public string ReadKey()
        {
            return _key;
        }
    }
}
