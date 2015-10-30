using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.view
{
    interface ModernView : IView
    {
        void AddInputSubscriber(InputActionObserver a_observer);
        void DisplayAddedCard(model.Player a_player, model.Card a_card, int playerScore);
    }
}
