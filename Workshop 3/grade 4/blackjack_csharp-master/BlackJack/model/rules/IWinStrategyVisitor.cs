using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    interface IWinStrategyVisitor
    {
        void Visit(DealerWinsOnEqualScoreWinStrategy rule);
        void Visit(PlayerWinsOnEqualScoreWinStrategy rule);
    }
}
