using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Card
    {

        public int suit { get;set; }

        /* HEARTS = 1,
           SPADES = 2,
           CLUBS = 3,
           DIAMOND = 4,
         */

        public int rank { get; set; }
  
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

        public int cardValue { get; set; }

        public Card(int s, int r)
        {
            suit = s;
            rank = r;
            if(r <= 10)
            {
                cardValue = r;
            }
            else
            {
                cardValue = 10;
            }
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
