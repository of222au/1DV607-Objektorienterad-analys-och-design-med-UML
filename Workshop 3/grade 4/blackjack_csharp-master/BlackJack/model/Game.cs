using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    class Game : PlayerHandChangedObserver
    {
        private model.Dealer m_dealer;
        private model.Player m_player;
        private model.rules.IRulesFactory m_rulesFactory;
        private bool playerStands;

        private List<PlayerHandChangedObserver> m_observers;

        public Game(rules.IRulesFactory a_rulesFactory)
        {
            m_observers = new List<PlayerHandChangedObserver>();

            m_rulesFactory = a_rulesFactory;
            m_dealer = new Dealer(m_rulesFactory);
            m_player = new Player();

            //subscribe to dealer events (when cards are dealed)
            m_dealer.AddSubscriber(this);
        }

        public rules.IRulesFactory GetRules()
        {
            return m_rulesFactory;
        }

        public bool IsGameOver()
        {
            return (playerStands && m_dealer.IsGameOver());
        }

        public bool IsDealerWinner()
        {
            return m_dealer.IsDealerWinner(m_player);
        }

        public bool NewGame()
        {
            playerStands = false;
            return m_dealer.NewGame(m_player);
        }

        public bool Hit()
        {
            return m_dealer.Hit(m_player);
        }

        public bool Stand()
        {
            playerStands = true;
            return m_dealer.Stand();
        }

        public IEnumerable<Card> GetDealerHand()
        {
            return m_dealer.GetHand();
        }

        public IEnumerable<Card> GetPlayerHand()
        {
            return m_player.GetHand();
        }

        public int GetDealerScore()
        {
            return m_dealer.CalcScore();
        }

        public int GetPlayerScore()
        {
            return m_player.CalcScore();
        }

        //observer methods
        public void AddSubscriber(PlayerHandChangedObserver a_observer)
        {
            if (!m_observers.Contains(a_observer))
            {
                m_observers.Add(a_observer);
            }
        }

        public void PlayerHandChanged(Player a_player, Card a_card)
        {
            //send the observer call along to observers for this object
            if (m_observers != null)
            {
                foreach (PlayerHandChangedObserver o in m_observers)
                {
                    o.PlayerHandChanged(a_player, a_card);
                }
            }
        }
    }
}
