using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    interface IRulesFactory
    {
        IHitStrategy GetHitRule();

        INewGameStrategy GetNewGameRule();

        IWinStrategy GetWinRule();
    }
}
