using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Dealer
    {
        public List<Card> Hand { get; set; }
        public bool IsShowing { get; set; }


        public Dealer()
        {
            Hand = new List<Card>();
        }
    }


}
