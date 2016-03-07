using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            for (int i = 0; i <= numOfDecks; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    for (int k = 1; k <= 13; k++)
                    {
                        mainDeck.Add(new Card(j, k));
                    }
                }
            }

            return mainDeck;
        }
        public static Random rand = new Random();
        public List<Card> Shuffle()
        {
            int i = mainDeck.Count;
            while (i > 1)
            {
                i--;
                var k = rand.Next(i + 1);
                var swap = mainDeck[k];
                mainDeck[k] = mainDeck[i];
                mainDeck[i] = swap;
            }

            Console.Clear();
            Console.WriteLine("The deck is out of cards...");
            Console.WriteLine("> Please type anything <");
            Console.ReadLine();
            Console.WriteLine("...");
            Console.WriteLine("...");
            Console.WriteLine("...");
            Console.WriteLine("...");
            Console.WriteLine("...The deck has been shuffled!");
            Console.WriteLine("> Please type anything <");
            Console.ReadLine();

            return mainDeck;
        }
    }
}
