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

        public Deck(int deckNumber)
        {
            mainDeck = new List<Card>();
            discardDeck = new List<Card>();
            numOfDecks = deckNumber;
            mainDeck = Build();
            mainDeck = Shuffle();
        }

        public List<Card> Build()
        {
            for(int i = 0; i <= numOfDecks; i++)
            {
                for(int j = 1; j <= 4; j++)
                {
                    for (int k = 1; k <= 13; k++)
                    {
                        mainDeck.Add(new Card(j, k));
                    }
                }
            }

            return mainDeck;
        }
        public List<Card> Shuffle()
        {
            mainDeck.OrderBy(x => Guid.NewGuid()).ToList();
            return mainDeck;
        }
    }
}
