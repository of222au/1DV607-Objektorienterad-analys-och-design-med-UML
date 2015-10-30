using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class InternationalNewGameStrategy : INewGameStrategy
    {
        public void Accept(INewGameStrategyVisitor visitor) { visitor.Visit(this); }

        public bool NewGame(Dealer a_dealer, Player a_player)
        {
            a_dealer.DealCardToPlayer(a_player);
            a_dealer.DealCardToDealer();
            a_dealer.DealCardToPlayer(a_player);

            return true;
        }
    }
}
