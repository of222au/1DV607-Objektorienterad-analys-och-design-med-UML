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

        private List<PlayerHandChangedObserver> m_observers;

        public Game()
        {
            m_observers = new List<PlayerHandChangedObserver>();

            m_dealer = new Dealer(new rules.RulesFactory());
            m_player = new Player();

            //subscribe to dealer events (when cards are dealed)
            m_dealer.AddSubscriber(this);
        }

        public bool IsGameOver()
        {
            return m_dealer.IsGameOver();
        }

        public bool IsDealerWinner()
        {
            return m_dealer.IsDealerWinner(m_player);
        }

        public bool NewGame()
        {
            return m_dealer.NewGame(m_player);
        }

        public bool Hit()
        {
            return m_dealer.Hit(m_player);
        }

        public bool Stand()
        {
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

        public void PlayerHandChanged()
        {
            //send the observer call along to observers for this object
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
