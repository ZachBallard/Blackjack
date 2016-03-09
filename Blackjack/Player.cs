using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Player
    {
        public string Name { get; set; }
        public int Money { get; set; }
        public List<Card> Hand { get; set; }
        public List<Card> Hand2 { get; set; }
        public bool IsSplit { get; set; }
        public int Bet { get; set; }


        public Player()
        {
            Hand = new List<Card>();
            Hand2 = new List<Card>();
        }
    }
}
