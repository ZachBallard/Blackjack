using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Card
    {
        private Suit s;
        private Rank r;

        public enum Suit
        {
            HEARTS = 0,
            SPADES = 1,
            CLUBS = 2,
            DIAMOND = 3,
        };

        public enum Rank
        {
            Ace= 1,
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
        };

        public Card(Suit s, Rank r)
        {
            Random random = new Random();
            s = random.Next(0, 3);
            r = random.Next(0, 13);

            this.s = s;
            this.r = r;
        }

        /* __________
          |          |
          | {r}  {r} |
          |          |
          |  {suit}  |
          |          |
          | {r}  {r} |
          |__________|        
        */
    }
}
