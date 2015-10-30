using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlackJack.view
{
    partial class UserControlPlayerCard : UserControl
    {
        private enum StandardDeckImageCardColorRows
        {
            Clubs = 0,
            Diamonds = 1,
            Hearts = 2,
            Spades = 3
        }

        private const int c_standardDeckImageWidth = 935;
        private const int c_standardDeckImageHeight = 388;

        private const int c_randomizeCardTimes = 6;
        private const int c_randomizeCardSleep = 100;




        public UserControlPlayerCard()
        {
            InitializeComponent();

            UnShowCard();
        }

        public void RandomizeCard()
        {
            Random rnd = new Random();

            for (int i = 0; i < c_randomizeCardTimes; i++)
            {
                int rndValue = rnd.Next(0, 13); // 0-12
                int rndColor = rnd.Next(0, 4); // 0-3

                model.Card card = new model.Card((model.Card.Color)rndColor, (model.Card.Value)rndValue);
                DoShowCard(card);

                System.Threading.Thread.Sleep(c_randomizeCardSleep);
                Application.DoEvents();
                //TODO : the view doesn't see this
            }
        }

        public void ShowCard(model.Card a_card)
        {
            //first do some randomize
            RandomizeCard();

            DoShowCard(a_card);
        }
        private void DoShowCard(model.Card a_card)
        {
            PositionCardPictureBoxToShowCorrectCardImage(a_card);
        }
        private void PositionCardPictureBoxToShowCorrectCardImage(model.Card a_card)
        {
            if (a_card.GetColor() != model.Card.Color.Hidden)
            {
                int[] cardPositions = new int[(int)model.Card.Value.Count] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                int cardPosition = cardPositions[(int)a_card.GetValue()];

                if (cardPosition == 12) { cardPosition = 0; } // since ace is first in the image
                else { cardPosition += 1; } //+ one step since ace is first in the image

                int left = cardPosition * GetStandardDeckCardImageWidth() + cardPosition; // (int)Math.Round(cardPosition);
                int imageRow = -1;
                switch (a_card.GetColor())
                {
                    case model.Card.Color.Clubs:
                        imageRow = (int)StandardDeckImageCardColorRows.Clubs;
                        break;
                    case model.Card.Color.Diamonds:
                        imageRow = (int)StandardDeckImageCardColorRows.Diamonds;
                        break;
                    case model.Card.Color.Hearts:
                        imageRow = (int)StandardDeckImageCardColorRows.Hearts;
                        break;
                    case model.Card.Color.Spades:
                        imageRow = (int)StandardDeckImageCardColorRows.Spades;
                        break;
                }
                int top = imageRow * GetStandardDeckCardImageHeight();

                pictureBoxCard.Left = -left;
                pictureBoxCard.Top = -top;

                panelCard.Width = GetStandardDeckCardImageWidth();
                panelCard.Height = GetStandardDeckCardImageHeight();
            }
            else
            {
                //hidden card, making sure not shown
                UnShowCard();
            }
        }
        
        private int GetStandardDeckCardImageWidth() 
        {
            return (c_standardDeckImageWidth / 13);
        }
        private int GetStandardDeckCardImageHeight() 
        {
            return (c_standardDeckImageHeight / 4);
        }
        private void UnShowCard()
        {
            pictureBoxCard.Left = 1000;
            pictureBoxCard.Top = 1000;
        }
    }
}
