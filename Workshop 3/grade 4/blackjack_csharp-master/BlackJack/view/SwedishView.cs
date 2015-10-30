using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.view
{
    class SwedishView : IView
    {
        public void DisplayWelcomeMessage(model.rules.IRulesFactory rules)
        {
            System.Console.Clear();
            System.Console.WriteLine("Hej Black Jack Världen");
            System.Console.WriteLine("----------------------");

            System.Console.WriteLine("Följande regler används:");
            rules.GetNewGameRule().Accept(this);
            rules.GetWinRule().Accept(this);
            rules.GetHitRule().Accept(this);
            
            System.Console.WriteLine("----------------------");
            System.Console.WriteLine("Skriv 'p' för att Spela, 'h' för nytt kort, 's' för att stanna 'q' för att avsluta\n");
        }

        //Game rules visitor pattern:
        public void Visit(model.rules.DealerWinsOnEqualScoreWinStrategy rule) { System.Console.WriteLine("Vinn-regel: Croupiern vinner vid samma poäng"); }
        public void Visit(model.rules.PlayerWinsOnEqualScoreWinStrategy rule) { System.Console.WriteLine("Vinn-regel: Spelaren vinner vid samma poäng"); }
        public void Visit(model.rules.BasicHitStrategy rule) { System.Console.WriteLine("Hit-regel: Standard"); }
        public void Visit(model.rules.Soft17HitStrategy rule) { System.Console.WriteLine("Hit-regel: Mjuk 17"); }
        public void Visit(model.rules.AmericanNewGameStrategy rule) { System.Console.WriteLine("Nytt spel-regel: Amerikansk"); }
        public void Visit(model.rules.InternationalNewGameStrategy rule) { System.Console.WriteLine("Nytt spel-regel: Internationell"); }

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
            if (a_card.GetColor() == model.Card.Color.Hidden)
            {
                System.Console.WriteLine("Dolt Kort");
            }
            else
            {
                String[] colors = new String[(int)model.Card.Color.Count]
                    { "Hjärter", "Spader", "Ruter", "Klöver" };
                String[] values = new String[(int)model.Card.Value.Count] 
                    { "två", "tre", "fyra", "fem", "sex", "sju", "åtta", "nio", "tio", "knekt", "dam", "kung", "ess" };
                System.Console.WriteLine("{0} {1}", colors[(int)a_card.GetColor()], values[(int)a_card.GetValue()]);
            }
        }
        public void DisplayPlayerHand(IEnumerable<model.Card> a_hand, int a_score)
        {
            DisplayHand("Spelare", a_hand, a_score);
        }
        public void DisplayDealerHand(IEnumerable<model.Card> a_hand, int a_score)
        {
            DisplayHand("Croupier", a_hand, a_score);
        }
        public void DisplayGameOver(bool a_dealerIsWinner)
        {
            System.Console.Write("Slut: ");
            if (a_dealerIsWinner)
            {
                System.Console.WriteLine("Croupiern Vann!");
            }
            else
            {
                System.Console.WriteLine("Du vann!");
            }
        }

        private void DisplayHand(String a_name, IEnumerable<model.Card> a_hand, int a_score)
        {
            System.Console.WriteLine("{0} Har: ", a_name);
            foreach (model.Card c in a_hand)
            {
                DisplayCard(c);
            }
            System.Console.WriteLine("Poäng: {0}", a_score);
            System.Console.WriteLine("");
        }
    }
}
