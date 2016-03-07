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
        public List<Card> hand2 { get; set; }
        public bool isSplit { get; set; }
        public int bet { get; set; }


        public Player()
        {
            hand = new List<Card>();
            hand2 = new List<Card>();
        }
    }
}
