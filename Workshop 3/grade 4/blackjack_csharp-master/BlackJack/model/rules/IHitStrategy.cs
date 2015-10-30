using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    interface IHitStrategy
    {
        void Accept(IHitStrategyVisitor visitor);
      
        bool DoHit(model.Player a_dealer);
    }
}
