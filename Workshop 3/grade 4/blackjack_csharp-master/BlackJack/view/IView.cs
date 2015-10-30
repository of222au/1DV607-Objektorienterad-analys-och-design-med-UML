using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.view
{
    enum InputAction
    {
        Play,
        Hit,
        Stand,
        Quit,
        Unknown
    }

    interface IView : model.rules.IHitStrategyVisitor, model.rules.IWinStrategyVisitor, model.rules.INewGameStrategyVisitor
    {
        void DisplayWelcomeMessage(model.rules.IRulesFactory rules);
        InputAction GetInput();
        void DisplayPlayerHand(IEnumerable<model.Card> a_hand, int a_score);
        void DisplayDealerHand(IEnumerable<model.Card> a_hand, int a_score);
        void DisplayGameOver(bool a_dealerIsWinner);
    }
}
