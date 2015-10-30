using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlackJack.view
{
    partial class FormView : Form, ModernView
    {
        private string m_rules = "";

        private List<UserControlPlayerCard> m_dealerCardUserControls;
        private List<UserControlPlayerCard> m_playerCardUserControls;

        private List<InputActionObserver> m_observers;

        public FormView()
        {
            InitializeComponent();

            m_dealerCardUserControls = new List<UserControlPlayerCard>();
            m_playerCardUserControls = new List<UserControlPlayerCard>();
            m_observers = new List<InputActionObserver>();

            ClearView();
        }

        private void ClearView()
        {
            //labelGameMessage.Text = "";
            labelDealerScore.Text = "";
            labelPlayerScore.Text = "";

            //disable input action buttons
            EnableDisableInputActionButtons(false);
        }

        public void DisplayWelcomeMessage(model.rules.IRulesFactory rules)
        {
            EnableDisableInputActionButtons(true);

            //generate rules
            m_rules = "";
            rules.GetNewGameRule().Accept(this);
            rules.GetWinRule().Accept(this);
            rules.GetHitRule().Accept(this);

            //clear player and dealer hand card controls
            ClearHands();
        }
        //Game rules visitor pattern:
        public void Visit(model.rules.DealerWinsOnEqualScoreWinStrategy rule) { m_rules += "Win rule: Dealer wins on equal score" + Environment.NewLine; }
        public void Visit(model.rules.PlayerWinsOnEqualScoreWinStrategy rule) { m_rules += "Win rule: Player wins on equal score" + Environment.NewLine; }
        public void Visit(model.rules.BasicHitStrategy rule) { m_rules += "Hit rule: Basic" + Environment.NewLine; }
        public void Visit(model.rules.Soft17HitStrategy rule) { m_rules += "Hit rule: Soft 17" + Environment.NewLine; }
        public void Visit(model.rules.AmericanNewGameStrategy rule) { m_rules += "New game rule: American" + Environment.NewLine; }
        public void Visit(model.rules.InternationalNewGameStrategy rule) { m_rules += "New game rule: International" + Environment.NewLine; }


        public InputAction GetInput()
        {
            return InputAction.Unknown;
        }

        public void DisplayGameOver(bool a_dealerIsWinner)
        {
            if (a_dealerIsWinner)
            {
                MessageBox.Show("Sorry, you lost..", "Dealer won",  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Hurray, you won!", "Player won", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //disable input action buttons
            EnableDisableInputActionButtons(false);
        }

        public void DisplayPlayerHand(IEnumerable<model.Card> a_hand, int a_score)
        {
            ClearHand(false);
            DisplayHand(false, a_hand, a_score);
        }

        public void DisplayDealerHand(IEnumerable<model.Card> a_hand, int a_score)
        {
            ClearHand(true);
            DisplayHand(true, a_hand, a_score);
        }

        private void ClearHands()
        {
            ClearHand(true);
            ClearHand(false);
        }
        private void ClearHand(bool dealerNotPlayer)
        {
            if (dealerNotPlayer) //dealer
            {
                foreach (UserControlPlayerCard userControlCard in m_dealerCardUserControls)
                {
                    flowLayoutPanelDealerCards.Controls.Remove(userControlCard);
                }
                m_dealerCardUserControls.Clear();
            }
            else //player
            {
                foreach (UserControlPlayerCard userControlCard in m_playerCardUserControls)
                {
                    flowLayoutPanelPlayerCards.Controls.Remove(userControlCard);
                }
                m_playerCardUserControls.Clear();
            }
        }
        private void DisplayHand(bool dealerNotPlayer, IEnumerable<model.Card> a_hand, int a_score)
        {
            foreach (model.Card c in a_hand)
            {
                CreateCard(dealerNotPlayer, c);
            }
            UpdatePlayerScore(dealerNotPlayer, a_score);
        }

        private void UpdatePlayerScore(bool dealerNotPlayer, int score)
        {
            if (dealerNotPlayer)
            {
                labelDealerScore.Text = "Score: " + score;
            }
            else
            {
                labelPlayerScore.Text = "Score: " + score;
            }
        }

        public void DisplayAddedCard(model.Player a_player, model.Card a_card, int playerScore)
        {
            bool isDealer = (a_player is model.Dealer);
            CreateCard(isDealer, a_card);
            UpdatePlayerScore(isDealer, playerScore);

            /*
            System.Threading.Thread.Sleep(200);
            Application.DoEvents();
            System.Threading.Thread.Sleep(200);
            Application.DoEvents();
             * */
        }



        private void CreateCard(bool dealerNotPlayer, model.Card a_card)
        {
            UserControlPlayerCard cardControl = new UserControlPlayerCard();
            if (dealerNotPlayer)
            {
                m_dealerCardUserControls.Add(cardControl);
                flowLayoutPanelDealerCards.Controls.Add(cardControl);
            }
            else
            {
                m_playerCardUserControls.Add(cardControl);
                flowLayoutPanelPlayerCards.Controls.Add(cardControl);
            }

            Application.DoEvents();
            Application.DoEvents();

            cardControl.ShowCard(a_card);
        }


        private void EnableDisableInputActionButtons(bool inGame)
        {
            buttonNewGame.Enabled = (!inGame);
            buttonHit.Enabled = (inGame);
            buttonStand.Enabled = (inGame);
        }

        private void buttonHit_Click(object sender, EventArgs e)
        {
            NotifyObserversOfInput(InputAction.Hit);
        }

        private void buttonStand_Click(object sender, EventArgs e)
        {
            NotifyObserversOfInput(InputAction.Stand);
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            NotifyObserversOfInput(InputAction.Play);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotifyObserversOfInput(InputAction.Play);
        }

        private void showRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(m_rules, "The game rules");
        }

        //observer methods
        public void AddInputSubscriber(InputActionObserver a_observer)
        {
            if (!m_observers.Contains(a_observer))
            {
                m_observers.Add(a_observer);
            }
        }
        private void NotifyObserversOfInput(InputAction inputAction)
        {
            if (m_observers != null)
            {
                foreach (InputActionObserver o in m_observers)
                {
                    o.Input(inputAction);
                }
            }
        }
    }
}
