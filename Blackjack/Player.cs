using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Player
    {
        public string name { get; set; }
        public int money { get; set; }
        public List<Card> hand { get; set; }
        public int splitValue { get;set; }
        public bool isSplit { get; set; }
        public int bet { get; set; }

        public int CheckPoints()
        {
            //remember is ace 1 or 11
            return 0;
        }
    }
}
