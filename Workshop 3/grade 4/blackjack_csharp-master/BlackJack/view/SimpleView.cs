using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.view
{
    class SimpleView : IView
    {
        public void DisplayWelcomeMessage(model.rules.IRulesFactory rules)
        {
            System.Console.Clear();
            System.Console.WriteLine("Hello Black Jack World");
            System.Console.WriteLine("----------------------");

            System.Console.WriteLine("The following rules are used:");
            rules.GetNewGameRule().Accept(this);
            rules.GetWinRule().Accept(this);
            rules.GetHitRule().Accept(this);

            System.Console.WriteLine("----------------------");
            System.Console.WriteLine("Type 'p' to Play, 'h' to Hit, 's' to Stand or 'q' to Quit\n");
        }

        //Game rules visitor pattern:
        public void Visit(model.rules.DealerWinsOnEqualScoreWinStrategy rule) { System.Console.WriteLine("Win rule: Dealer wins on equal score"); }
        public void Visit(model.rules.PlayerWinsOnEqualScoreWinStrategy rule) { System.Console.WriteLine("Win rule: Player wins on equal score"); }
        public void Visit(model.rules.BasicHitStrategy rule) { System.Console.WriteLine("Hit rule: Basic"); }
        public void Visit(model.rules.Soft17HitStrategy rule) { System.Console.WriteLine("Hit rule: Soft 17"); }
        public void Visit(model.rules.AmericanNewGameStrategy rule) { System.Console.WriteLine("New game rule: American"); }
        public void Visit(model.rules.InternationalNewGameStrategy rule) { System.Console.WriteLine("New game rule: International"); }


        public InputAction GetInput()
        {
            string input = System.Console.ReadLine();

            if (input == "p")
            {
                return InputAction.Play;
            }
            else if (input == "h")
            {
                return InputAction.Hit;
            }
            else if (input == "s")
            {
                return InputAction.Stand;
            }
            else if (input == "q")
            {
                return InputAction.Quit;
            }
            else
            {
                return InputAction.Unknown;
            }
        }

        private void DisplayCard(model.Card a_card)
        {
            System.Console.WriteLine("{0} of {1}", a_card.GetValue(), a_card.GetColor());
        }

        public void DisplayPlayerHand(IEnumerable<model.Card> a_hand, int a_score)
        {
            DisplayHand("Player", a_hand, a_score);
        }

        public void DisplayDealerHand(IEnumerable<model.Card> a_hand, int a_score)
        {
            DisplayHand("Dealer", a_hand, a_score);
        }

        private void DisplayHand(String a_name, IEnumerable<model.Card> a_hand, int a_score)
        {
            System.Console.WriteLine("{0} Has: ", a_name);
            foreach (model.Card c in a_hand)
            {
                DisplayCard(c);
            }
            System.Console.WriteLine("Score: {0}", a_score);
            System.Console.WriteLine("");
        }

        public void DisplayGameOver(bool a_dealerIsWinner)
        {
            System.Console.Write("GameOver: ");
            if (a_dealerIsWinner)
            {
                System.Console.WriteLine("Dealer Won!");
            }
            else
            {
                System.Console.WriteLine("You Won!");
            }
        }

    }
}
