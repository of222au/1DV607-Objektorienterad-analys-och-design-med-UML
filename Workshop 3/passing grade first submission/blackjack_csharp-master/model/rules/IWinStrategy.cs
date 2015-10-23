using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    interface IWinStrategy
    {
        bool IsDealerWinner(int dealerScore, int playerScore, int g_maxScore);
    }
}
