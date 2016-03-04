﻿using System;
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

            int playerWins = 0;
            int dealerWins = 0;
            int numOfGames = 0;

            //game setup
            while (!exit)
            {
                var deck = new Deck();

                deck.numOfDecks = getDeckNumber();

                player.money = howMuchMoney();
                int startMoney = player.money;

                deck.Build();
                deck.Shuffle();

                //actual dealing
                while (true)
                {
                    bool isPlayerTurn = true;

                    numOfGames++;

                    

                    player.bet = askForBet(player.money);

                    dealer.DealCard(isPlayerTurn);

                    displayGraphics(dealer.isShowing);

                    //check for blackjack

                    bool blackjack = false;

                    if (player.CheckPoints() == 21)
                    {
                        player.money += player.bet;
                        blackjack = true;
                    }

                    //not going to impliment at this time
                    //player.isSplit = willSplit();

                    //handle hit or stay
                    bool hasSix = false;

                    if (!blackjack)
                    {
                        while (true)
                        {
                            if (player.CheckPoints() > 21)
                            {
                                player.money -= player.bet;
                                break;
                            }
                            if (player.hand.Count >= 6)
                            {
                                hasSix = true;
                                break;
                            }
                            if (doHit())
                            {
                                dealer.DealCard(isPlayerTurn);
                            }
                            else
                            {
                                break;
                            }
                        }

                        /*while (player.isSplit)
                        {
                            if (player.CheckPoints() > 21)
                            {
                                break;
                            }
                            if (hitStay())
                            {
                                dealer.DealCard(isPlayerTurn);
                            }
                            else
                            {
                                break;
                            }
                        }*/

                        if (!hasSix)
                        {
                            dealer.isShowing = true;

                            displayGraphics(dealer.isShowing);


                            if (dealer.CheckPoints() == 21)
                            {
                                player.money -= player.bet;
                            }

                            while (dealer.CheckPoints() < 18)
                            {
                                dealer.DealCard(isPlayerTurn);
                                if (dealer.CheckPoints() >= 21)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    //check for results
                    if (blackjack || hasSix || player.CheckPoints() > dealer.CheckPoints())
                    {
                        //show money gained
                        player.money += player.bet;
                        playerWins++;
                    }
                    else
                    {
                        //show money lost
                        player.money -= player.bet;
                        dealerWins++;
                    }

                    //how to stop dealing
                    if (player.money < 0)
                    {
                        //show lost all money
                        break;
                    }

                    if (!dealAgain())
                    {
                        exit = true;
                        break;
                    }
                }//actual dealing over

                showResults(startMoney, player.money, numOfGames, playerWins, dealerWins);
            }//app close
        }

        private static void displayGraphics(bool dealerShowing)
        {
            throw new NotImplementedException();
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
                        return false;
                    case "n":
                        return true;
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
      
        /* private static bool willSplit()
         {
             return true;
         }
        */

        public static int askForBet(int playerMoney)
        {
            string userInput = "";

            while (true)
            {
                Console.WriteLine($"\nHow much would you like to bet? (You have ${playerMoney} to bet)");
                userInput = Console.ReadLine();

                if (int.Parse(userInput) <= playerMoney)
                {
                    return int.Parse(userInput);
                }
                else if (int.Parse(userInput) > playerMoney)
                {
                    Console.WriteLine($"\nYou only have ${playerMoney} to bet!");
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
            if(money <= 0)
            {
                Console.WriteLine(@".------.------.------.------.------.------.     .------.------.------.------.------.------.");
                Console.WriteLine(@"|Y.--. |O.--. |U.--. |'.--. |R.--. |E.--. |.-.  |B.--. |R.--. |O.--. |K.--. |E.--. |!.--. |");
                Console.WriteLine(@"| (\/) | :/\: | (\/) | :/\: | :(): | (\/) ((0)) | :(): | :(): | :/\: | :/\: | (\/) | (\/) |");
                Console.WriteLine(@"| :\/: | :\/: | :\/: | :\/: | ()() | :\/: |'-.-.| ()() | ()() | :\/: | :\/: | :\/: | :\/: |");
                Console.WriteLine(@"| '--'Y| '--'O| '--'U| '--''| '--'R| '--'E| ((0)| '--'B| '--'R| '--'O| '--'K| '--'E| '--'!|");
                Console.WriteLine(@"`------`------`------`------`------`------'  '-'`------`------`------`------`------`------'");
                Console.WriteLine(@"(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)(damn)0");
            }
            else if(money > startMoney)
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
