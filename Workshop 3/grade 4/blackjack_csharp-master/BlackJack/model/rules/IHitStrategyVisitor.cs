using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    interface IHitStrategyVisitor
    {
        void Visit(BasicHitStrategy rule);
        void Visit(Soft17HitStrategy rule);
    }
}
