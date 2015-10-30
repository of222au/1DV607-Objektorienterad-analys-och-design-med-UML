using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    interface INewGameStrategyVisitor
    {
        void Visit(AmericanNewGameStrategy rule);
        void Visit(InternationalNewGameStrategy rule);
    }
}
