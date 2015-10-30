namespace BlackJack.view
{
    partial class UserControlPlayerCard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelCard = new System.Windows.Forms.Panel();
            this.pictureBoxCard = new System.Windows.Forms.PictureBox();
            this.panelCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCard)).BeginInit();
            this.SuspendLayout();
            // 
            // panelCard
            // 
            this.panelCard.Controls.Add(this.pictureBoxCard);
            this.panelCard.Location = new System.Drawing.Point(0, 0);
            this.panelCard.Margin = new System.Windows.Forms.Padding(4);
            this.panelCard.MaximumSize = new System.Drawing.Size(71, 96);
            this.panelCard.Name = "panelCard";
            this.panelCard.Size = new System.Drawing.Size(71, 96);
            this.panelCard.TabIndex = 4;
            // 
            // pictureBoxCard
            // 
            this.pictureBoxCard.Image = global::BlackJack.Properties.Resources.StandardDeck;
            this.pictureBoxCard.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxCard.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxCard.Name = "pictureBoxCard";
            this.pictureBoxCard.Size = new System.Drawing.Size(935, 388);
            this.pictureBoxCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxCard.TabIndex = 3;
            this.pictureBoxCard.TabStop = false;
            // 
            // UserControlPlayerCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.panelCard);
            this.Name = "UserControlPlayerCard";
            this.Size = new System.Drawing.Size(75, 100);
            this.panelCard.ResumeLayout(false);
            this.panelCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.PictureBox pictureBoxCard;
    }
}
