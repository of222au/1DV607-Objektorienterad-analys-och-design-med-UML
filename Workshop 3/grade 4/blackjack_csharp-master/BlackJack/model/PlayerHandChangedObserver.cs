using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    interface PlayerHandChangedObserver
    {
        void PlayerHandChanged(Player a_player, Card a_card);
    }
}
