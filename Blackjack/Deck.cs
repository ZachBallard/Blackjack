using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Deck
    {
        public int numOfDecks { get; set; }
        public List<Card> mainDeck { get; set; }
        public List<Card> discardDeck { get; set; }

        public void Build()
        {
            foreach (Card.Rank r in Enum.GetValues(typeof(Card.Rank)))
            {
                foreach (Card.Suit s in Enum.GetValues(typeof(Card.Suit)))
                {
                    mainDeck.Add(new Card(s, r));
                }
            }
        }
        public void Shuffle()
        {
            mainDeck.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
 }
