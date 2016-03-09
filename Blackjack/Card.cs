using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Card
    {

        public int Suit { get;set; }

        /* HEARTS = 1,
           SPADES = 2,
           CLUBS = 3,
           DIAMOND = 4,
         */

        public int Rank { get; set; }
  
         /* Ace= 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Jack = 11,
            Queen = 12,
            King = 13
         */

        public int CardValue { get; set; }

        public Card(int s, int r)
        {
            Suit = s;
            Rank = r;
            if(r == 1)
            {
                CardValue = 11;
            }
            else if (r <= 10 && r >1)
            {
                CardValue = r;
            }
            else
            {
                CardValue = 10;
            }
        }
    }
}
