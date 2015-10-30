using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class BasicHitStrategy : IHitStrategy
    {
        public void Accept(IHitStrategyVisitor visitor) { visitor.Visit(this); }

        private const int g_hitLimit = 17;

        public bool DoHit(model.Player a_dealer)
        {
            return a_dealer.CalcScore() < g_hitLimit;
        }
    }
}
