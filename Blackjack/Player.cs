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

        public int checkPoints()
        {
            return;
        }
    }
}
