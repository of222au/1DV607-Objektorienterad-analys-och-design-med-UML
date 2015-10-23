using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.controller
{
    class PlayGame : model.PlayerHandChangedObserver
    {
        model.Game m_game;
        view.IView m_view;

        public PlayGame(model.Game a_game, view.IView a_view)
        {
            m_game = a_game;
            m_view = a_view;

            //subscribe to game events
            m_game.AddSubscriber(this);

            UpdateView();
        }

        public bool Play()
        {
            if (m_game.IsGameOver())
            {
                //make sure view is cleared and updated before show game over
                UpdateView();

                m_view.DisplayGameOver(m_game.IsDealerWinner());
            }

            view.InputAction inputAction = m_view.GetInput();

            if (inputAction == view.InputAction.Play)
            {
                m_game.NewGame();
            }
            else if (inputAction == view.InputAction.Hit)
            {
                m_game.Hit();
            }
            else if (inputAction == view.InputAction.Stand)
            {
                m_game.Stand();
            }

            return (inputAction != view.InputAction.Quit);
        }

        private void UpdateView()
        {
            m_view.DisplayWelcomeMessage();

            m_view.DisplayDealerHand(m_game.GetDealerHand(), m_game.GetDealerScore());
            m_view.DisplayPlayerHand(m_game.GetPlayerHand(), m_game.GetPlayerScore());
        }

        public void PlayerHandChanged()
        {
            //pause the execution a moment
            System.Threading.Thread.Sleep(1000);

            //update view to show current card hands
            UpdateView();
        }
    }
}
