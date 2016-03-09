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
        public int NumOfDecks { get; set; }
        public List<Card> MainDeck { get; set; }
        public List<Card> DiscardDeck { get; set; }

        public Deck(int deckNumber)
        {
            MainDeck = new List<Card>();
            DiscardDeck = new List<Card>();
            NumOfDecks = deckNumber;
            MainDeck = Build();
            MainDeck = Shuffle();
        }

        public List<Card> Build()
        {
            for (int i = 0; i <= NumOfDecks; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    for (int k = 1; k <= 13; k++)
                    {
                        MainDeck.Add(new Card(j, k));
                    }
                }
            }

            return MainDeck;
        }
        public static Random Rand = new Random();
        public List<Card> Shuffle()
        {
            int i = MainDeck.Count;
            while (i > 1)
            {
                i--;
                var k = Rand.Next(i + 1);
                var swap = MainDeck[k];
                MainDeck[k] = MainDeck[i];
                MainDeck[i] = swap;
            }

            Console.Clear();
            Console.WriteLine("\nThe deck is out of cards...");
            Console.WriteLine("> Please type anything <");
            Console.ReadLine();
            Console.WriteLine("...");
            Console.WriteLine("...");
            Console.WriteLine("...");
            Console.WriteLine("...");
            Console.WriteLine("...The deck has been shuffled!");
            Console.WriteLine("> Please type anything <");
            Console.ReadLine();

            return MainDeck;
        }
    }
}
