using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class DealerWinsOnEqualScoreWinStrategy : IWinStrategy
    {
        public bool IsDealerWinner(int dealerScore, int playerScore, int g_maxScore)
        {
            if (playerScore > g_maxScore)
            {
                return true;
            }
            else if (dealerScore > g_maxScore)
            {
                return false;
            }
            return dealerScore >= playerScore;
        }
    }
}
