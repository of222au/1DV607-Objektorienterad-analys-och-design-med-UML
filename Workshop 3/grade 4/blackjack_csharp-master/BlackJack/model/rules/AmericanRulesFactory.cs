using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class AmericanRulesFactory : IRulesFactory
    {
        public IHitStrategy GetHitRule()
        {
            return new BasicHitStrategy();
        }

        public INewGameStrategy GetNewGameRule()
        {
            return new AmericanNewGameStrategy();
        }

        public IWinStrategy GetWinRule()
        {
            return new DealerWinsOnEqualScoreWinStrategy();
        }
    }
}
