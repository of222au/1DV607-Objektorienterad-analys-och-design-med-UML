using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.controller
{
    class PlayGame : model.PlayerHandChangedObserver, view.InputActionObserver
    {
        model.Game m_game;
        view.IView m_view;

        public PlayGame(model.Game a_game, view.IView a_view)
        {
            m_game = a_game;
            m_view = a_view;

            //subscribe to game events
            m_game.AddSubscriber(this);

            if (!(m_view is view.ModernView))
            {
                ClearView();
            }
        }

        public bool Play()
        {
            if (m_view is view.ModernView)
            {
                view.FormView view = (view.FormView)m_view;
                view.AddInputSubscriber(this);
                return true;
            }
            else
            {
                if (m_game.IsGameOver())
                {
                    //make sure view is cleared and updated before show game over
                    ClearView();
                    UpdateViewHands();

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
        }

        //InputActionObserver method
        public void Input(view.InputAction inputAction)
        {
            if (inputAction == view.InputAction.Play)
            {
                ClearView();
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

            if (m_game.IsGameOver())
            {
                m_view.DisplayGameOver(m_game.IsDealerWinner());
            }
        }

        private void ClearView()
        {
            m_view.DisplayWelcomeMessage(m_game.GetRules());
        }

        private void UpdateViewHands(model.Player playerWithAddedCard = null, model.Card cardAdded = null)
        {
            if (m_view is view.ModernView)
            {
                if (playerWithAddedCard != null && cardAdded != null)
                {
                    view.ModernView modernView = (view.ModernView)m_view;
                    int playerScore = 0;
                    if (playerWithAddedCard is model.Dealer)
                    {
                        playerScore = m_game.GetDealerScore();
                    }
                    else
                    {
                        playerScore = m_game.GetPlayerScore();
                    }
                    modernView.DisplayAddedCard(playerWithAddedCard, cardAdded, playerScore);
                }
            }
            else 
            {
                m_view.DisplayDealerHand(m_game.GetDealerHand(), m_game.GetDealerScore());
                m_view.DisplayPlayerHand(m_game.GetPlayerHand(), m_game.GetPlayerScore());
            }
        }

        public void PlayerHandChanged(model.Player a_player, model.Card a_card)
        {
            if (!(m_view is view.ModernView))
            {
                //pause the execution a moment
                System.Threading.Thread.Sleep(500);

                //update view to show current card hands
                ClearView();
            }

            UpdateViewHands(a_player, a_card);
        }
    }
}
