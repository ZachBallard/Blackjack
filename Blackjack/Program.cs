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

            welcomeScreen();
            whatIsName();

            bool exit = false;

            while (!exit)
            {
                getDeckNumber();
                howMuchMoney();

                var player = new Player();
                var dealer = new Dealer();
                var deck = new Deck();

                while (true)
                {
                    bool isPlayerTurn = true;

                    askForBet();

                    deck.Build();
                    deck.Shuffle();

                    dealer.dealCard();

                    displayGraphics(dealer.isShowing);

                    if (player.checkPoints() == 21)
                    {

                    }

                    if (player.isSplit)
                    {

                    }

                    player.isSplit = willSplit();

                    while (true)
                    {
                        if (player.checkPoints() > 21)
                        {
                            break;
                        }
                        if (hitStay())
                        {
                            dealer.dealCard(isPlayerTurn);
                        }
                        else
                        {
                            break;
                        }
                    }

                    while (player.isSplit)
                    {
                        if (player.checkPoints() > 21)
                        {
                            break;
                        }
                        if (hitStay())
                        {
                            dealer.dealCard(isPlayerTurn);
                        }
                        else
                        {
                            break;
                        }
                    }

                    dealer.isShowing = true;

                    displayGraphics(dealer.isShowing);

                    if (dealer.CheckPoints() == 21)
                    {
                        break;
                    }

                    while (dealer.CheckPoints() < 18)
                    {
                        dealer.dealCard(isPlayerTurn);
                        if(dealer.CheckPoints >= 21)
                        {
                            break;
                        }
                    }


                    //check win
                }

            }
        }


        private static bool hitStay()
        {
            throw new NotImplementedException();
        }

        private static void displayGraphics(bool dealerShowing)
        {
            throw new NotImplementedException();
        }

        private static bool willSplit()
        {
            return true;
        }

        public static void askForBet()
        {
            throw new NotImplementedException();
        }

        private static void howMuchMoney()
        {
            throw new NotImplementedException();
        }

        private static void getDeckNumber()
        {
            throw new NotImplementedException();
        }

        private static void whatIsName()
        {
            throw new NotImplementedException();
        }

        private static void welcomeScreen()
        {
            throw new NotImplementedException();
        }

    }
}
