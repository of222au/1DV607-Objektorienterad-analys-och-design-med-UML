using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    class Dealer : Player
    {
        private Deck m_deck = null;
        private const int g_maxScore = 21;

        private rules.INewGameStrategy m_newGameRule;
        private rules.IHitStrategy m_hitRule;
        private rules.IWinStrategy m_winRule;

        private List<PlayerHandChangedObserver> m_observers;

        public Dealer(rules.RulesFactory a_rulesFactory)
        {
            m_observers = new List<PlayerHandChangedObserver>();

            m_newGameRule = a_rulesFactory.GetNewGameRule();
            m_hitRule = a_rulesFactory.GetHitRule();
            m_winRule = a_rulesFactory.GetWinRule();
        }

        public bool NewGame(Player a_player)
        {
            if (m_deck == null || IsGameOver())
            {
                m_deck = new Deck();
                ClearHand();
                a_player.ClearHand();
                return m_newGameRule.NewGame(this, a_player);   
            }
            return false;
        }

        public bool Hit(Player a_player)
        {
            if (m_deck != null && a_player.CalcScore() < g_maxScore && !IsGameOver())
            {
                DealCardToPlayer(a_player);
                return true;
            }
            return false;
        }

        public bool Stand()
        {
            if (m_deck != null)
            {
                ShowHand();
                foreach (Card c in GetHand())
                {
                    c.Show(true);
                }

                while (m_hitRule.DoHit(this))
                {
                    DealCardToDealer();
                }
            }

            return true;
        }

        public void DealCardToDealer()
        {
            DealCard(this);
        }
        public void DealCardToPlayer(Player a_player)
        {
            DealCard(a_player);
        }
        private void DealCard(Player toPlayer)
        {
            Card c = m_deck.GetCard();
            c.Show(true);
            toPlayer.DealCard(c);

            NotifyObserversPlayerHandChanged();
        }

        public bool IsDealerWinner(Player a_player)
        {
            return m_winRule.IsDealerWinner(CalcScore(), a_player.CalcScore(), g_maxScore);
        }

        public bool IsGameOver()
        {
            if (m_deck != null && /*CalcScore() >= g_hitLimit*/ m_hitRule.DoHit(this) != true)
            {
                return true;
            }
            return false;
        }


        //observer methods
        public void AddSubscriber(PlayerHandChangedObserver a_observer)
        {
            if (!m_observers.Contains(a_observer))
            {
                m_observers.Add(a_observer);
            }
        }
        private void NotifyObserversPlayerHandChanged()
        {
            if (m_observers != null)
            {
                foreach (PlayerHandChangedObserver o in m_observers)
                {
                    o.PlayerHandChanged();
                }
            }
        }
    }
}
