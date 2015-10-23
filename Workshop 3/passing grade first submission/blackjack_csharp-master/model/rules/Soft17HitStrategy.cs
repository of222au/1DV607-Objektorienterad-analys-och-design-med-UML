using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class Soft17HitStrategy : IHitStrategy
    {
        private const int g_hitLimit = 17;

        public bool DoHit(model.Player a_dealer)
        {
            bool hasAce = false;
            foreach (Card c in a_dealer.GetHand())
            {
                if (c.GetValue() == Card.Value.Ace)
                {
                    hasAce = true;
                    break;
                }
            }

            int score = a_dealer.CalcScore();
            return (score < g_hitLimit ||
                    (score == g_hitLimit && hasAce));
        }
    }
}
