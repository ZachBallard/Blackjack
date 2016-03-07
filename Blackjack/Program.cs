using System;
using System.Collections.Generic;
using System.Data;
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
            bool isStory = false;
            bool willBuyTime = false;

            int playerWins = 0;
            int dealerWins = 0;
            int numOfGames = 0;
            int gamesLeft = 40;
            int startMoney = 0;
            int deckNumber = 0;
            int totalDebt = 10000;

            //game setup
            while (!exit)
            {

                isStory = isPlayingStory();

                if (!isStory)
                {
                    Console.Clear();
                    player.money = howMuchMoney();
                    startMoney = player.money;
                    deckNumber = getDeckNumber();

                }
                else
                {
                    player.money = 217;
                    startMoney = player.money;
                    deckNumber = 2;

                    //Lay down the setup
                    Console.Clear();
                    Console.WriteLine("\nYou are $10,000 in debt to the wrong sort of people. Dangerous people, of course.");
                    Console.WriteLine("\n...And you haven't been paying.");
                    Console.WriteLine("\nYou managed to disappear for a while, but then they invited you're Mom over. By van and violence.");
                    Console.WriteLine("Money or her life starts paying off the debt.");
                    Console.WriteLine("\nThe wrong sort of people with your Mom have one possible weakness now. Tradition. You walk, head high, into");
                    Console.WriteLine("their trap and invoke an old right of conflicting parties. The Right of The Gambler.");
                    Console.WriteLine("\nYou've been fortunate in your ploy. The right was invoked and only two decks were available for your observed");
                    Console.WriteLine("game choice: Blackjack. Still, it won't be easy...");
                    Console.WriteLine("> Please type anything <");
                    Console.ReadLine();

                    Console.Clear();
                    Console.WriteLine("\nYou have $217 dollars and enough time to play around 40 games.");
                    Console.WriteLine("\nAs per The Right of the Gambler, more time can be bought with you're winnings. ");
                    Console.WriteLine("\n$500 towards you're debt will get you enough for about 25 more games. ");
                    Console.WriteLine("\nYou better get the money in time or your Mother will be a very sad topic. \n\nAnd you probably will be too...");
                    Console.WriteLine("> Please type anything <");
                    Console.ReadLine();
                    Console.Clear();
                }

                var deck = new Deck(deckNumber);

                //actual dealing
                while (true)
                {
                    numOfGames++;

                    if (player.money > 500 && isStory)
                    {
                        willBuyTime = askForBuyTime();

                        if (willBuyTime)
                        {
                            player.money -= 500;
                            gamesLeft += 25;
                            totalDebt -= 500;
                        }
                    }

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

                    if (isStory)
                    {
                        Console.WriteLine($"{player.name} has {gamesLeft} games left");
                    }

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

                            if (isStory)
                            {
                                Console.WriteLine($"{player.name} has {gamesLeft} games left");
                            }

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

                                if (isStory)
                                {
                                    Console.WriteLine($"{player.name} has {gamesLeft} games left");
                                }

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

                                if (isStory)
                                {
                                    Console.WriteLine($"{player.name} has {gamesLeft} games left");
                                }

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

                    if (isStory)
                    {
                        Console.WriteLine($"{player.name} has {gamesLeft} games left");
                    }

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
                    if (blackjack)
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
                        gamesLeft--;
                    }
                    else
                    {
                        //show money lost
                        Console.WriteLine($"\nYou had ${player.money}");
                        player.money -= player.bet;
                        Console.WriteLine($"You now have ${player.money}");
                        dealerWins++;
                        gamesLeft--;
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

                    if (isStory && player.money >= totalDebt)
                    {
                        break;
                    }

                    if (!dealAgain() && !isStory)
                    {
                        break;
                    }

                }//actual dealing over

                showResults(startMoney, player.money, numOfGames, playerWins, dealerWins, isStory, totalDebt);
                exit = playAgain(player.name);
            }//app close
        }

        private static bool askForBuyTime()
        {
            string userInput = "";

            while (true)
            {
                Console.WriteLine($"\nWill you buy more time for $500? (y)es or (n)o");
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

        private static bool isPlayingStory()
        {
            string userInput = "";

            while (true)
            {
                Console.WriteLine($"\nWould you like to start Story Mode? (y)es or (n)o");
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

            int deckNum = 0;

            while (true)
            {
                Console.WriteLine($"\nHow many decks do you want to use?");
                userInput = Console.ReadLine();

                int.TryParse(userInput, out deckNum);

                if(deckNum > 0)
                {
                    return deckNum;
                }
                else
                {
                    Console.WriteLine("\nThat wasn't a valid selection. Try again.");
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

        private static void showResults(int startMoney, int money, int numOfGames, int playerWins, int dealerWins, bool isStory, int totalDebt)
        {
            Console.Clear();

            if (money <= 0)
            {
                Console.WriteLine(@".------.------.------.------.------.------.     .------.------.------.------.------.------.");
                Console.WriteLine(@"|Y.--. |O.--. |U.--. |'.--. |R.--. |E.--. |.-.  |B.--. |R.--. |O.--. |K.--. |E.--. |!.--. |");
                Console.WriteLine(@"| (\/) | :/\: | (\/) | :/\: | :(): | (\/) ((0)) | :(): | :(): | :/\: | :/\: | (\/) | (\/) |");
                Console.WriteLine(@"| :\/: | :\/: | :\/: | :\/: | ()() | :\/: |'-.-.| ()() | ()() | :\/: | :\/: | :\/: | :\/: |");
                Console.WriteLine(@"| '--'Y| '--'O| '--'U| '--''| '--'R| '--'E| ((0)| '--'B| '--'R| '--'O| '--'K| '--'E| '--'!|");
                Console.WriteLine(@"`------`------`------`------`------`------'  '-'`------`------`------`------`------`------'");
                Console.WriteLine(@"(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)0");
                if (isStory)
                {
                    Console.WriteLine("\nAnd you're Mother is too. The worse sort of way.\nYou're sure to follow...");
                }
            }
            else if (money > startMoney && !isStory)
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
                Console.WriteLine("\nI'm not made in a way that I can judge how much you earned, but...");
                Console.WriteLine("\nit is still probably better this was a game. (A fantastic one! Am I right? No?)");
            }
            else if (isStory)
            {
                Console.WriteLine(@"          _______           _  _______  _______    _______  _______  _______          ");
                Console.WriteLine(@"|\     /|(  ___  )|\     /|( )(  ____ )(  ____ \  (       )(  ___  )(       )         ");
                Console.WriteLine(@"( \   / )| (   ) || )   ( ||/ | (    )|| (    \/  | () () || (   ) || () () |         ");
                Console.WriteLine(@" \ (_) / | |   | || |   | |   | (____)|| (__      | || || || |   | || || || |         ");
                Console.WriteLine(@"  \   /  | |   | || |   | |   |     __)|  __)     | |(_)| || |   | || |(_)| |         ");
                Console.WriteLine(@"   ) (   | |   | || |   | |   | (\ (   | (        | |   | || |   | || |   | |         ");
                Console.WriteLine(@"   | |   | (___) || (___) |   | ) \ \__| (____/\  | )   ( || (___) || )   ( | _  _  _ ");
                Console.WriteLine(@"   \_/   (_______)(_______)   |/   \__/(_______/  |/     \|(_______)|/     \|(_)(_)(_)");
                Console.WriteLine(@"?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|?|");
                if (money >= totalDebt)
                {
                    Console.WriteLine("\n...didn't die and you aren't in debt anymore! Congrats!");
                    Console.WriteLine("\nYou might owe her a meal or something...after the hospital visit...");
                }
                else
                {
                    Console.WriteLine("\n...probably isn't angry anymore. But she probably isn't anything anymore. You're sure to follow.");
                }
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
                Console.WriteLine("\nBut you certainly aren't richer. All is right in the world.");
            }

            Console.WriteLine();
            Console.WriteLine($"\nYou started with ${startMoney} and ended with ${money}");
            Console.WriteLine($"\nOut of {numOfGames} games:");
            Console.WriteLine($"Player wins: {playerWins} Player Loses: {dealerWins}");
            //add value based heckling?
        }
    }
}
