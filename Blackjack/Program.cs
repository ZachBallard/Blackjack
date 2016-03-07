using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            var player = new Player();
            var dealer = new Dealer();

            welcomeScreen();

            player.name = whatIsName();

            bool exit = false;
            bool isdealerGraphicCall = false;

            int playerWins = 0;
            int dealerWins = 0;
            int numOfGames = 0;

            //game setup
            while (!exit)
            {
                var deck = new Deck(getDeckNumber());

                player.money = howMuchMoney();
                int startMoney = player.money;

                //actual dealing
                while (true)
                {
                    numOfGames++;

                    player.bet = askForBet(player.money);

                    if (deck.mainDeck.Count >= 2)
                    {
                        //take 2 card from deck and put in  player hand
                        Card a = deck.mainDeck.First();
                        deck.mainDeck.RemoveAt(0);
                        Card b = deck.mainDeck.First();
                        deck.mainDeck.RemoveAt(0);
                        player.hand.Add(a);
                        player.hand.Add(b);
                    }
                    else
                    {
                        //put discard in maindeck and reshuffle
                        deck.mainDeck.AddRange(deck.discardDeck);
                        deck.discardDeck.Clear();

                        deck.Shuffle();

                        //take 2 card from deck and put in  player hand
                        Card a = deck.mainDeck.First();
                        deck.mainDeck.RemoveAt(0);
                        Card b = deck.mainDeck.First();
                        deck.mainDeck.RemoveAt(0);
                        player.hand.Add(a);
                        player.hand.Add(b);
                    }

                    if (deck.mainDeck.Count >= 2)
                    {
                        //take  2 card from deck and put in  dealer hand
                        Card a = deck.mainDeck.First();
                        deck.mainDeck.RemoveAt(0);
                        Card b = deck.mainDeck.First();
                        deck.mainDeck.RemoveAt(0);
                        dealer.hand.Add(a);
                        dealer.hand.Add(b);
                    }
                    else
                    {
                        //put discard in maindeck and reshuffle
                        deck.mainDeck.AddRange(deck.discardDeck);
                        deck.discardDeck.Clear();

                        deck.Shuffle();

                        //take 2 card from deck and put in dealer hand
                        Card a = deck.mainDeck.First();
                        deck.mainDeck.RemoveAt(0);
                        Card b = deck.mainDeck.First();
                        deck.mainDeck.RemoveAt(0);
                        dealer.hand.Add(a);
                        dealer.hand.Add(b);
                    }

                    //display graphics
                    Console.Clear();
                    int currentPoints = 0;

                    if (currentPoints > 21)
                    {
                        Console.WriteLine($"{player.name} has BUSTED");
                    }
                    else
                    {
                        Console.WriteLine($"{player.name} has {currentPoints}");
                    }

                    displayGraphics(dealer.isShowing, player.hand, isdealerGraphicCall);

                    isdealerGraphicCall = true;
                    currentPoints = CheckPoints(dealer.hand);

                    if (currentPoints > 21)
                    {
                        Console.WriteLine($"Dealer has BUSTED");
                    }
                    else
                    {
                        Console.WriteLine($"Dealer has ???");
                    }

                    displayGraphics(dealer.isShowing, dealer.hand, isdealerGraphicCall);

                    isdealerGraphicCall = false;

                    //check for blackjack
                    bool blackjack = false;

                    if (CheckPoints(player.hand) == 21)
                    {
                        player.money += player.bet;
                        blackjack = true;
                    }

                    //check for split
                    player.isSplit = willSplit(player.hand);

                    if (player.isSplit)
                    {
                        player.bet += player.bet;

                        Card c = player.hand.First();
                        player.hand2.Add(c);
                        player.hand.Clear();
                        player.hand.Add(c);

                        if (deck.mainDeck.Count >= 1)
                        {
                            //take card from deck and put in  player hand
                            Card a = deck.mainDeck.First();
                            player.hand.Add(a);
                            deck.mainDeck.RemoveAt(0);
                        }
                        else
                        {
                            //put discard in maindeck and 
                            deck.mainDeck.AddRange(deck.discardDeck);
                            deck.discardDeck.Clear();

                            deck.Shuffle();

                            //take card from deck and put in  player hand
                            Card a = deck.mainDeck.First();
                            player.hand.Add(a);
                            deck.mainDeck.RemoveAt(0);
                        }

                        if (deck.mainDeck.Count >= 1)
                        {
                            //take card from deck and put in  player hand
                            Card a = deck.mainDeck.First();
                            player.hand2.Add(a);
                            deck.mainDeck.RemoveAt(0);
                        }
                        else
                        {
                            //put discard in maindeck and 
                            deck.mainDeck.AddRange(deck.discardDeck);
                            deck.discardDeck.Clear();

                            deck.Shuffle();

                            //take card from deck and put in  player hand
                            Card a = deck.mainDeck.First();
                            player.hand2.Add(a);
                            deck.mainDeck.RemoveAt(0);
                        }
                    }

                    //handle six card charlie rule (currently only six. could change to any.)
                    bool hasSix = false;

                    //player begins hit chain
                    if (!blackjack)
                    {
                        while (true)
                        {
                            //display graphics
                            Console.Clear();
                            currentPoints = CheckPoints(player.hand);

                            if (currentPoints > 21)
                            {
                                Console.WriteLine($"{player.name} has BUSTED");
                            }
                            else
                            {
                                Console.WriteLine($"{player.name} has {currentPoints}");
                            }

                            displayGraphics(dealer.isShowing, player.hand, isdealerGraphicCall);

                            isdealerGraphicCall = true;
                            currentPoints = CheckPoints(dealer.hand);

                            if (currentPoints > 21)
                            {
                                Console.WriteLine($"Dealer has BUSTED");
                            }
                            else
                            {
                                Console.WriteLine($"Dealer has ???");
                            }

                            displayGraphics(dealer.isShowing, dealer.hand, isdealerGraphicCall);

                            isdealerGraphicCall = false;

                            if (CheckPoints(player.hand) > 21)
                            {
                                break;
                            }

                            if (player.hand.Count == 6 && CheckPoints(player.hand) <= 21)
                            {
                                hasSix = true;
                                break;
                            }

                            if (doHit())
                            {
                                if (deck.mainDeck.Count >= 1)
                                {
                                    //take card from deck and put in  player hand
                                    Card a = deck.mainDeck.First();
                                    player.hand.Add(a);
                                    deck.mainDeck.RemoveAt(0);
                                }
                                else
                                {
                                    //put discard in maindeck and 
                                    deck.mainDeck.AddRange(deck.discardDeck);
                                    deck.discardDeck.Clear();

                                    deck.Shuffle();

                                    //take card from deck and put in  player hand
                                    Card a = deck.mainDeck.First();
                                    player.hand.Add(a);
                                    deck.mainDeck.RemoveAt(0);
                                }
                            }
                            else
                            {
                                break;
                            }
                        }

                        //handle split situation
                        if (player.isSplit)
                        {
                            Console.WriteLine("You are about to play your second hand...");
                            Console.WriteLine("> Please type anything <");
                            Console.ReadLine();

                            while (player.isSplit)
                            {
                                //display graphics
                                Console.Clear();
                                currentPoints = CheckPoints(player.hand);

                                if (currentPoints > 21)
                                {
                                    Console.WriteLine($"Hand 1: {player.name} has BUSTED");
                                }
                                else
                                {
                                    Console.WriteLine($"Hand 1:{player.name} has {currentPoints}");
                                }
                                currentPoints = CheckPoints(player.hand2);

                                if (currentPoints > 21)
                                {
                                    Console.WriteLine($"Hand 2: {player.name} has BUSTED");
                                }
                                else
                                {
                                    Console.WriteLine($"Hand 2: {player.name} has {currentPoints}");
                                }

                                displayGraphics(dealer.isShowing, player.hand2, isdealerGraphicCall);

                                isdealerGraphicCall = true;
                                currentPoints = CheckPoints(dealer.hand);

                                if (currentPoints > 21)
                                {
                                    Console.WriteLine($"Dealer has BUSTED");
                                }
                                else
                                {
                                    Console.WriteLine($"Dealer has ???");
                                }

                                displayGraphics(dealer.isShowing, dealer.hand, isdealerGraphicCall);

                                isdealerGraphicCall = false;

                                if (CheckPoints(player.hand) > 21)
                                {
                                    break;
                                }

                                if (player.hand2.Count == 6 && CheckPoints(player.hand2) <= 21)
                                {
                                    hasSix = true;
                                    break;
                                }

                                if (doHit())
                                {
                                    if (deck.mainDeck.Count >= 1)
                                    {
                                        //take card from deck and put in  player hand
                                        Card a = deck.mainDeck.First();
                                        player.hand2.Add(a);
                                        deck.mainDeck.RemoveAt(0);
                                    }
                                    else
                                    {
                                        //put discard in maindeck and 
                                        deck.mainDeck.AddRange(deck.discardDeck);
                                        deck.discardDeck.Clear();

                                        deck.Shuffle();

                                        //take card from deck and put in  player hand
                                        Card a = deck.mainDeck.First();
                                        player.hand2.Add(a);
                                        deck.mainDeck.RemoveAt(0);
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            //check to see which hand to use

                            if (CheckPoints(player.hand2) > CheckPoints(player.hand) && CheckPoints(player.hand2) <= 21)
                            {
                                player.hand.Clear();
                                player.hand.AddRange(player.hand2);
                            }
                            else
                            {
                                Console.WriteLine("The first score will be used...");
                                Console.WriteLine("> Please type anything <");
                                Console.ReadLine();
                            }

                        }

                        if (!hasSix)
                        {
                            dealer.isShowing = true;

                            //dealer begins hit chain
                            while (CheckPoints(dealer.hand) <= 16 && CheckPoints(player.hand) <= 21)
                            {
                                //display graphics
                                Console.Clear();
                                currentPoints = CheckPoints(player.hand);

                                if (currentPoints > 21)
                                {
                                    Console.WriteLine($"{player.name} has BUSTED");
                                }
                                else
                                {
                                    Console.WriteLine($"player.name has {currentPoints}");
                                }

                                displayGraphics(dealer.isShowing, player.hand, isdealerGraphicCall);

                                isdealerGraphicCall = true;
                                currentPoints = CheckPoints(dealer.hand);

                                if (currentPoints > 21)
                                {
                                    Console.WriteLine($"Dealer has BUSTED");
                                }
                                else
                                {
                                    Console.WriteLine($"Dealer has {currentPoints}");
                                }

                                displayGraphics(dealer.isShowing, dealer.hand, isdealerGraphicCall);

                                isdealerGraphicCall = false;

                                if (deck.mainDeck.Count >= 1)
                                {
                                    //take card from deck and put in dealer hand
                                    Card a = deck.mainDeck.First();
                                    dealer.hand.Add(a);
                                    deck.mainDeck.RemoveAt(0);
                                }
                                else
                                {
                                    //put discard in maindeck and reshuffle

                                    deck.mainDeck.AddRange(deck.discardDeck);
                                    deck.discardDeck.Clear();

                                    deck.Shuffle();

                                    //take card from deck and put in dealer hand
                                    Card a = deck.mainDeck.First();
                                    dealer.hand.Add(a);
                                    deck.mainDeck.RemoveAt(0);
                                }

                                if (CheckPoints(dealer.hand) >= 21)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    //display graphics
                    Console.Clear();
                    currentPoints = CheckPoints(player.hand);

                    if (currentPoints > 21)
                    {
                        Console.WriteLine($"{player.name} has BUSTED");
                    }
                    else
                    {
                        Console.WriteLine($"{player.name} has {currentPoints}");
                    }

                    displayGraphics(dealer.isShowing, player.hand, isdealerGraphicCall);

                    isdealerGraphicCall = true;
                    currentPoints = CheckPoints(dealer.hand);

                    if (currentPoints > 21)
                    {
                        Console.WriteLine($"Dealer has BUSTED");
                    }
                    else
                    {
                        Console.WriteLine($"Dealer has {currentPoints}");
                    }

                    displayGraphics(dealer.isShowing, dealer.hand, isdealerGraphicCall);

                    isdealerGraphicCall = false;

                    //check for results
                    if(blackjack)
                    {
                        Console.WriteLine();
                        Console.WriteLine("BLACKJACk!");
                        Console.WriteLine();
                    }

                    if ((blackjack || hasSix || CheckPoints(player.hand) > CheckPoints(dealer.hand) || CheckPoints(dealer.hand) > 21) && CheckPoints(player.hand) <= 21)
                    {
                        //show money gained
                        Console.WriteLine($"\nYou had ${player.money}");
                        player.money += player.bet;
                        Console.WriteLine($"You now have ${player.money}");
                        playerWins++;
                    }
                    else
                    {
                        //show money lost
                        Console.WriteLine($"\nYou had ${player.money}");
                        player.money -= player.bet;
                        Console.WriteLine($"You now have ${player.money}");
                        dealerWins++;
                    }

                    //put both hands in discard
                    deck.discardDeck.AddRange(player.hand);
                    player.hand.Clear();

                    deck.discardDeck.AddRange(dealer.hand);
                    dealer.hand.Clear();

                    //how to stop dealing
                    if (player.money <= 0)
                    {
                        Console.WriteLine("> Please type anything <");
                        Console.ReadLine();
                        break;
                    }

                    if (!dealAgain())
                    {
                        exit = true;
                        break;
                    }
                }//actual dealing over

                showResults(startMoney, player.money, numOfGames, playerWins, dealerWins);
                exit = playAgain(player.name);
            }//app close
        }

        private static void displayGraphics(bool isShowing, List<Card> hand, bool isdealerGraphicCall)
        {
            int cardNumCounter = 1;

            foreach (var card in hand)
            {
                //set suit and rank nodes
                string suitGraphic;
                string rankGraphic;

                switch (card.suit)
                {
                    case 1:
                        suitGraphic = "H";
                        break;
                    case 2:
                        suitGraphic = "S";
                        break;
                    case 3:
                        suitGraphic = "C";
                        break;
                    default:
                        suitGraphic = "D";
                        break;
                }

                if (card.rank > 1 && card.rank < 10)
                {
                    rankGraphic = card.rank.ToString();
                }
                else if (card.rank == 10)
                {
                    rankGraphic = "T";
                }
                else if (card.rank == 11)
                {
                    rankGraphic = "J";
                }
                else if (card.rank == 12)
                {
                    rankGraphic = "Q";
                }
                else if (card.rank == 13)
                {
                    rankGraphic = "K";
                }
                else
                {
                    rankGraphic = "A";
                };


                //print cards
                // cardNumCounter == 2 && isShowing == false && isdealerGraphicCall == false
                if (isShowing == false && isdealerGraphicCall == true && cardNumCounter == 2)
                {
                    Console.WriteLine(" _____");
                    Console.WriteLine($"|?   ?|");
                    Console.WriteLine($"|  ?  |");
                    Console.WriteLine($"|?   ?|");
                    Console.WriteLine("|_____|");
                }
                else
                {
                    Console.WriteLine(" _____");
                    Console.WriteLine($"|{rankGraphic}   {rankGraphic}|");
                    Console.WriteLine($"|  {suitGraphic}  |");
                    Console.WriteLine($"|{rankGraphic}   {rankGraphic}|");
                    Console.WriteLine("|_____|");
                }

                cardNumCounter++;
            }
        }

        private static int CheckPoints(List<Card> hand)
        {
            //remember to check for ace making bust

            bool hasAce = false;
            int total = 0;
            int aceCounter = 0;

            foreach (var card in hand)
            {
                if (card.cardValue == 11)
                {
                    hasAce = true;
                    aceCounter++;
                }

                total += card.cardValue;
            }
            if (total > 21 && hasAce)
            {
                total -= 10 * aceCounter;
            }

            return total;
        }

        private static bool playAgain(string playerName)
        {
            string userInput = "";

            while (true)
            {
                Console.WriteLine($"\nWould you like to try again {playerName}? (y)es or (n)o");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "y":
                        return false;
                    case "n":
                        return true;
                    default:
                        Console.WriteLine("\nThat wasn't a valid selection. Try again.");
                        break;
                }
            }
        }

        private static bool dealAgain()
        {
            string userInput = "";

            while (true)
            {
                Console.WriteLine("\nWould you like to deal again? (y)es or (n)o");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "y":
                        return true;
                    case "n":
                        return false;
                    default:
                        Console.WriteLine("\nThat wasn't a valid selection. Try again.");
                        break;
                }
            }
        }

        private static bool doHit()
        {
            string userInput = "";

            while (true)
            {
                Console.WriteLine($"\nWould you like to (h)it or (s)tay?");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "h":
                        return true;
                    case "s":
                        return false;
                    default:
                        Console.WriteLine("\nThat wasn't a valid selection. Try again.");
                        break;
                }
            }
        }

        private static bool willSplit(List<Card> playerHand)
        {
            string userInput = "";

            var c = playerHand.First();
            var d = playerHand.Last();

            if (c.rank == d.rank)
            {
                while (true)
                {
                    Console.WriteLine("\nWould you like to split and double down? (y)es or (n)o");
                    userInput = Console.ReadLine();

                    switch (userInput)
                    {
                        case "y":
                            return true;
                        case "n":
                            return false;
                        default:
                            Console.WriteLine("\nThat wasn't a valid selection. Try again.");
                            break;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public static int askForBet(int playerMoney)
        {
            string userInput = "";
            int result;

            while (true)
            {
                Console.WriteLine($"\nHow much would you like to bet? (You have ${playerMoney} to bet)");
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out result))
                {
                    if (result <= playerMoney)
                    {
                        return int.Parse(userInput);
                    }
                    else if (result > playerMoney)
                    {
                        Console.WriteLine($"\nYou only have ${playerMoney} to bet!");
                    }
                }
                else
                {
                    Console.WriteLine("\nThat wasn't a valid selection. Try again.");
                }
            }
        }

        private static int howMuchMoney()
        {
            string userInput = "";

            while (true)
            {
                Console.WriteLine($"\nHow much money do you have to play with today?");
                userInput = Console.ReadLine();

                int i = 0;
                bool isNumber = int.TryParse(userInput, out i);

                if (isNumber)
                {
                    return i;
                }
                else
                {
                    Console.WriteLine("\nThat wasn't a valid selection. Try again.");
                }
            }
        }

        private static int getDeckNumber()
        {
            string userInput = "";

            while (true)
            {
                Console.WriteLine($"\nWould you like to use (1) or (7) decks?");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        return 1;
                    case "7":
                        return 7;
                    default:
                        Console.WriteLine("\nThat wasn't a valid selection. Try again.");
                        break;
                }
            }
        }

        private static string whatIsName()
        {
            string userInput = "";

            Console.WriteLine($"\nWhat should we call you?");
            userInput = Console.ReadLine();
            return userInput;
        }

        private static void welcomeScreen()
        {
            Console.WriteLine(@"  ___        __     ___            ____     __     ___          ");
            Console.WriteLine(@" |   \ ||   //\\   //  \\ ||  //  \___ |   //\\   //  \\ ||  // ");
            Console.WriteLine(@" |=--< ||  //__\\  ||     ||=||   _   ||  //__\\  ||     ||=||  ");
            Console.WriteLine(@" |___/ ||=//    \\ \\__// ||  \\  \\__|/ //    \\ \\__// ||  \\ ");
            Console.WriteLine(@" __ |_    ===== _   _ |_   ___   _       _   _ _| The Iron Yard ");
            Console.WriteLine(@" __ |_| \/  // (_| (_ | |  ||_) (_| | | (_| | (_|  (3/3/2016)   ");
            Console.WriteLine(@"       _/  //==            ||_)     | |             ^^^^^^^^    ");
            Console.WriteLine(@"(21)(21)(21)(21)(21)(21)(21)(21)(21)(21)(21)(21)(21)(21)(21)(21)");
        }

        private static void showResults(int startMoney, int money, int numOfGames, int playerWins, int dealerWins)
        {
            if (money <= 0)
            {
                Console.WriteLine(@".------.------.------.------.------.------.     .------.------.------.------.------.------.");
                Console.WriteLine(@"|Y.--. |O.--. |U.--. |'.--. |R.--. |E.--. |.-.  |B.--. |R.--. |O.--. |K.--. |E.--. |!.--. |");
                Console.WriteLine(@"| (\/) | :/\: | (\/) | :/\: | :(): | (\/) ((0)) | :(): | :(): | :/\: | :/\: | (\/) | (\/) |");
                Console.WriteLine(@"| :\/: | :\/: | :\/: | :\/: | ()() | :\/: |'-.-.| ()() | ()() | :\/: | :\/: | :\/: | :\/: |");
                Console.WriteLine(@"| '--'Y| '--'O| '--'U| '--''| '--'R| '--'E| ((0)| '--'B| '--'R| '--'O| '--'K| '--'E| '--'!|");
                Console.WriteLine(@"`------`------`------`------`------`------'  '-'`------`------`------`------`------`------'");
                Console.WriteLine(@"(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)0");
            }
            else if (money > startMoney)
            {
                Console.WriteLine(@"  ______              __            _______            __        __  __ ");
                Console.WriteLine(@" /      \            |  \          |       \          |  \      |  \|  \");
                Console.WriteLine(@"|  $$$$$$\  ______  _| $$_         | $$$$$$$\ ______   \$$  ____| $$| $$");
                Console.WriteLine(@"| $$ __\$$ /      \|   $$ \        | $$__/ $$|      \ |  \ /      $$| $$");
                Console.WriteLine(@"| $$|    \|  $$$$$$\\$$$$$$        | $$    $$ \$$$$$$\| $$|  $$$$$$$| $$");
                Console.WriteLine(@"| $$ \$$$$| $$    $$ | $$ __       | $$$$$$$ /      $$| $$| $$  | $$ \$$");
                Console.WriteLine(@"| $$__| $$| $$$$$$$$ | $$|  \      | $$     |  $$$$$$$| $$| $$__| $$ __ ");
                Console.WriteLine(@"\$$    $$ \$$     \  \$$  $$      | $$      \$$    $$| $$ \$$    $$| $$\");
                Console.WriteLine(@" \$$$$$$   \$$$$$$$   \$$$$        \$$       \$$$$$$$ \$$  \$$$$$$$ \$$/");
                Console.WriteLine(@"$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            }
            else
            {
                Console.WriteLine(@" __    __   ______  ________        _______   _______    ______   __    __  ________  __ ");
                Console.WriteLine(@"|  \  |  \ /      \|        \      |       \ |       \  /      \ |  \  /  \|        \|  \");
                Console.WriteLine(@"| $$\ | $$|  $$$$$$\\$$$$$$$$      | $$$$$$$\| $$$$$$$\|  $$$$$$\| $$ /  $$| $$$$$$$$| $$");
                Console.WriteLine(@"| $$$\| $$| $$  | $$  | $$         | $$__/ $$| $$__| $$| $$  | $$| $$/  $$ | $$__    | $$");
                Console.WriteLine(@"| $$$$\ $$| $$  | $$  | $$         | $$    $$| $$    $$| $$  | $$| $$  $$  | $$  \   | $$");
                Console.WriteLine(@"| $$\$$ $$| $$  | $$  | $$         | $$$$$$$\| $$$$$$$\| $$  | $$| $$$$$\  | $$$$$    \$$");
                Console.WriteLine(@"| $$ \$$$$| $$__/ $$  | $$         | $$__/ $$| $$  | $$| $$__/ $$| $$ \$$\ | $$_____  __ ");
                Console.WriteLine(@"| $$  \$$$ \$$    $$  | $$         | $$    $$| $$  | $$ \$$    $$| $$  \$$\| $$     \|  \");
                Console.WriteLine(@" \$$   \$$  \$$$$$$    \$$          \$$$$$$$  \$$   \$$  \$$$$$$  \$$   \$$ \$$$$$$$$ \$$");
                Console.WriteLine(@"(!Winning)(!Winning)(!Winning)(!Winning)(!Winning)(!Winning)(!Winning)(!Winning)(!Winning)");
            }

            Console.WriteLine();
            Console.WriteLine($"\nYou started with {startMoney} and ended with {money}");
            Console.WriteLine($"\nOut of {numOfGames} games:");
            Console.WriteLine($"Player wins: {playerWins} Player Loses: {dealerWins}");
            //add value based heckling?
        }
    }
}
