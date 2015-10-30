using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    interface INewGameStrategy
    {
        void Accept(INewGameStrategyVisitor visitor);

        bool NewGame(Dealer a_dealer, Player a_player);
    }
}
