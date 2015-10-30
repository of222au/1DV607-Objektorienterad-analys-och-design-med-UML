namespace BlackJack.view
{
    partial class FormView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanelDealerCards = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelPlayerCards = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxPlayerCards = new System.Windows.Forms.GroupBox();
            this.labelPlayerScore = new System.Windows.Forms.Label();
            this.groupBoxDealerCards = new System.Windows.Forms.GroupBox();
            this.labelDealerScore = new System.Windows.Forms.Label();
            this.buttonStand = new System.Windows.Forms.Button();
            this.buttonHit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonNewGame = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxPlayerCards.SuspendLayout();
            this.groupBoxDealerCards.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanelDealerCards
            // 
            this.flowLayoutPanelDealerCards.Location = new System.Drawing.Point(19, 23);
            this.flowLayoutPanelDealerCards.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelDealerCards.MaximumSize = new System.Drawing.Size(800, 0);
            this.flowLayoutPanelDealerCards.MinimumSize = new System.Drawing.Size(0, 123);
            this.flowLayoutPanelDealerCards.Name = "flowLayoutPanelDealerCards";
            this.flowLayoutPanelDealerCards.Size = new System.Drawing.Size(800, 123);
            this.flowLayoutPanelDealerCards.TabIndex = 6;
            // 
            // flowLayoutPanelPlayerCards
            // 
            this.flowLayoutPanelPlayerCards.Location = new System.Drawing.Point(19, 23);
            this.flowLayoutPanelPlayerCards.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelPlayerCards.MaximumSize = new System.Drawing.Size(800, 0);
            this.flowLayoutPanelPlayerCards.MinimumSize = new System.Drawing.Size(0, 123);
            this.flowLayoutPanelPlayerCards.Name = "flowLayoutPanelPlayerCards";
            this.flowLayoutPanelPlayerCards.Size = new System.Drawing.Size(800, 123);
            this.flowLayoutPanelPlayerCards.TabIndex = 8;
            // 
            // groupBoxPlayerCards
            // 
            this.groupBoxPlayerCards.AutoSize = true;
            this.groupBoxPlayerCards.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxPlayerCards.Controls.Add(this.labelPlayerScore);
            this.groupBoxPlayerCards.Controls.Add(this.flowLayoutPanelPlayerCards);
            this.groupBoxPlayerCards.Location = new System.Drawing.Point(16, 231);
            this.groupBoxPlayerCards.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxPlayerCards.MinimumSize = new System.Drawing.Size(0, 160);
            this.groupBoxPlayerCards.Name = "groupBoxPlayerCards";
            this.groupBoxPlayerCards.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxPlayerCards.Size = new System.Drawing.Size(827, 185);
            this.groupBoxPlayerCards.TabIndex = 9;
            this.groupBoxPlayerCards.TabStop = false;
            this.groupBoxPlayerCards.Text = "Your cards";
            // 
            // labelPlayerScore
            // 
            this.labelPlayerScore.AutoSize = true;
            this.labelPlayerScore.Location = new System.Drawing.Point(16, 150);
            this.labelPlayerScore.Name = "labelPlayerScore";
            this.labelPlayerScore.Size = new System.Drawing.Size(45, 16);
            this.labelPlayerScore.TabIndex = 9;
            this.labelPlayerScore.Text = "label2";
            // 
            // groupBoxDealerCards
            // 
            this.groupBoxDealerCards.AutoSize = true;
            this.groupBoxDealerCards.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxDealerCards.Controls.Add(this.labelDealerScore);
            this.groupBoxDealerCards.Controls.Add(this.flowLayoutPanelDealerCards);
            this.groupBoxDealerCards.Location = new System.Drawing.Point(16, 34);
            this.groupBoxDealerCards.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxDealerCards.MinimumSize = new System.Drawing.Size(0, 160);
            this.groupBoxDealerCards.Name = "groupBoxDealerCards";
            this.groupBoxDealerCards.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxDealerCards.Size = new System.Drawing.Size(827, 185);
            this.groupBoxDealerCards.TabIndex = 10;
            this.groupBoxDealerCards.TabStop = false;
            this.groupBoxDealerCards.Text = "Dealers cards";
            // 
            // labelDealerScore
            // 
            this.labelDealerScore.AutoSize = true;
            this.labelDealerScore.Location = new System.Drawing.Point(16, 150);
            this.labelDealerScore.Name = "labelDealerScore";
            this.labelDealerScore.Size = new System.Drawing.Size(45, 16);
            this.labelDealerScore.TabIndex = 7;
            this.labelDealerScore.Text = "label1";
            // 
            // buttonStand
            // 
            this.buttonStand.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStand.Location = new System.Drawing.Point(533, 14);
            this.buttonStand.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStand.Name = "buttonStand";
            this.buttonStand.Size = new System.Drawing.Size(181, 65);
            this.buttonStand.TabIndex = 11;
            this.buttonStand.Text = "Stand";
            this.buttonStand.UseVisualStyleBackColor = true;
            this.buttonStand.Click += new System.EventHandler(this.buttonStand_Click);
            // 
            // buttonHit
            // 
            this.buttonHit.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonHit.Location = new System.Drawing.Point(219, 14);
            this.buttonHit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonHit.Name = "buttonHit";
            this.buttonHit.Size = new System.Drawing.Size(275, 65);
            this.buttonHit.TabIndex = 12;
            this.buttonHit.Text = "Hit (one more card)";
            this.buttonHit.UseVisualStyleBackColor = true;
            this.buttonHit.Click += new System.EventHandler(this.buttonHit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonNewGame);
            this.panel1.Controls.Add(this.buttonHit);
            this.panel1.Controls.Add(this.buttonStand);
            this.panel1.Location = new System.Drawing.Point(16, 423);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(827, 92);
            this.panel1.TabIndex = 13;
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewGame.Location = new System.Drawing.Point(19, 14);
            this.buttonNewGame.Margin = new System.Windows.Forms.Padding(4);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.Size = new System.Drawing.Size(161, 65);
            this.buttonNewGame.TabIndex = 13;
            this.buttonNewGame.Text = "New game";
            this.buttonNewGame.UseVisualStyleBackColor = true;
            this.buttonNewGame.Click += new System.EventHandler(this.buttonNewGame_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(870, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.showRulesToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newGameToolStripMenuItem.Text = "New game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // showRulesToolStripMenuItem
            // 
            this.showRulesToolStripMenuItem.Name = "showRulesToolStripMenuItem";
            this.showRulesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showRulesToolStripMenuItem.Text = "Show rules";
            this.showRulesToolStripMenuItem.Click += new System.EventHandler(this.showRulesToolStripMenuItem_Click);
            // 
            // FormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 529);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxDealerCards);
            this.Controls.Add(this.groupBoxPlayerCards);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormView";
            this.Text = "BlackJack";
            this.groupBoxPlayerCards.ResumeLayout(false);
            this.groupBoxPlayerCards.PerformLayout();
            this.groupBoxDealerCards.ResumeLayout(false);
            this.groupBoxDealerCards.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDealerCards;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPlayerCards;
        private System.Windows.Forms.GroupBox groupBoxPlayerCards;
        private System.Windows.Forms.GroupBox groupBoxDealerCards;
        private System.Windows.Forms.Button buttonStand;
        private System.Windows.Forms.Button buttonHit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelPlayerScore;
        private System.Windows.Forms.Label labelDealerScore;
        private System.Windows.Forms.Button buttonNewGame;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showRulesToolStripMenuItem;
    }
}