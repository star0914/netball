namespace Netball
{
    partial class Netball
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Netball));
            this.img_logo = new System.Windows.Forms.PictureBox();
            this.lb_logoTxt = new System.Windows.Forms.Label();
            this.li_season = new System.Windows.Forms.ComboBox();
            this.lb_season = new System.Windows.Forms.Label();
            this.li_competition = new System.Windows.Forms.ComboBox();
            this.lb_competition = new System.Windows.Forms.Label();
            this.lb_round = new System.Windows.Forms.Label();
            this.li_round = new System.Windows.Forms.ComboBox();
            this.la_team = new System.Windows.Forms.Label();
            this.la_cu_state = new System.Windows.Forms.Label();
            this.la_slash = new System.Windows.Forms.Label();
            this.la_total = new System.Windows.Forms.Label();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.btnFetchResult = new System.Windows.Forms.Button();
            this.li_tournament = new System.Windows.Forms.ComboBox();
            this.lb_tournament = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.img_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // img_logo
            // 
            this.img_logo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.img_logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.img_logo.Image = ((System.Drawing.Image)(resources.GetObject("img_logo.Image")));
            this.img_logo.Location = new System.Drawing.Point(56, 43);
            this.img_logo.Name = "img_logo";
            this.img_logo.Size = new System.Drawing.Size(50, 52);
            this.img_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.img_logo.TabIndex = 8;
            this.img_logo.TabStop = false;
            // 
            // lb_logoTxt
            // 
            this.lb_logoTxt.AutoSize = true;
            this.lb_logoTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_logoTxt.Location = new System.Drawing.Point(112, 57);
            this.lb_logoTxt.Name = "lb_logoTxt";
            this.lb_logoTxt.Size = new System.Drawing.Size(189, 24);
            this.lb_logoTxt.TabIndex = 9;
            this.lb_logoTxt.Text = "MATCH SELECTION";
            // 
            // li_season
            // 
            this.li_season.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.li_season.FormattingEnabled = true;
            this.li_season.Location = new System.Drawing.Point(56, 184);
            this.li_season.Name = "li_season";
            this.li_season.Size = new System.Drawing.Size(267, 28);
            this.li_season.TabIndex = 10;
            // 
            // lb_season
            // 
            this.lb_season.AutoSize = true;
            this.lb_season.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_season.Location = new System.Drawing.Point(62, 161);
            this.lb_season.Name = "lb_season";
            this.lb_season.Size = new System.Drawing.Size(76, 20);
            this.lb_season.TabIndex = 11;
            this.lb_season.Text = "Season : ";
            // 
            // li_competition
            // 
            this.li_competition.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.li_competition.FormattingEnabled = true;
            this.li_competition.Location = new System.Drawing.Point(393, 184);
            this.li_competition.Name = "li_competition";
            this.li_competition.Size = new System.Drawing.Size(131, 28);
            this.li_competition.TabIndex = 12;
            // 
            // lb_competition
            // 
            this.lb_competition.AutoSize = true;
            this.lb_competition.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_competition.Location = new System.Drawing.Point(402, 161);
            this.lb_competition.Name = "lb_competition";
            this.lb_competition.Size = new System.Drawing.Size(106, 20);
            this.lb_competition.TabIndex = 13;
            this.lb_competition.Text = "Competition : ";
            // 
            // lb_round
            // 
            this.lb_round.AutoSize = true;
            this.lb_round.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_round.Location = new System.Drawing.Point(583, 161);
            this.lb_round.Name = "lb_round";
            this.lb_round.Size = new System.Drawing.Size(69, 20);
            this.lb_round.TabIndex = 14;
            this.lb_round.Text = "Round : ";
            // 
            // li_round
            // 
            this.li_round.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.li_round.FormattingEnabled = true;
            this.li_round.Location = new System.Drawing.Point(578, 184);
            this.li_round.Name = "li_round";
            this.li_round.Size = new System.Drawing.Size(152, 28);
            this.li_round.TabIndex = 15;
            // 
            // la_team
            // 
            this.la_team.AutoSize = true;
            this.la_team.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.la_team.Location = new System.Drawing.Point(124, 367);
            this.la_team.Name = "la_team";
            this.la_team.Size = new System.Drawing.Size(41, 15);
            this.la_team.TabIndex = 16;
            this.la_team.Text = "teams";
            // 
            // la_cu_state
            // 
            this.la_cu_state.AutoSize = true;
            this.la_cu_state.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.la_cu_state.Location = new System.Drawing.Point(56, 367);
            this.la_cu_state.Name = "la_cu_state";
            this.la_cu_state.Size = new System.Drawing.Size(14, 15);
            this.la_cu_state.TabIndex = 17;
            this.la_cu_state.Text = "0";
            // 
            // la_slash
            // 
            this.la_slash.AutoSize = true;
            this.la_slash.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.la_slash.Location = new System.Drawing.Point(82, 367);
            this.la_slash.Name = "la_slash";
            this.la_slash.Size = new System.Drawing.Size(10, 15);
            this.la_slash.TabIndex = 18;
            this.la_slash.Text = "/";
            // 
            // la_total
            // 
            this.la_total.AutoSize = true;
            this.la_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.la_total.Location = new System.Drawing.Point(90, 367);
            this.la_total.Name = "la_total";
            this.la_total.Size = new System.Drawing.Size(14, 15);
            this.la_total.TabIndex = 19;
            this.la_total.Text = "0";
            // 
            // btn_refresh
            // 
            this.btn_refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_refresh.BackColor = System.Drawing.Color.Transparent;
            this.btn_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_refresh.Image = ((System.Drawing.Image)(resources.GetObject("btn_refresh.Image")));
            this.btn_refresh.Location = new System.Drawing.Point(695, 60);
            this.btn_refresh.MaximumSize = new System.Drawing.Size(35, 35);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_refresh.Size = new System.Drawing.Size(35, 35);
            this.btn_refresh.TabIndex = 20;
            this.btn_refresh.UseVisualStyleBackColor = false;
            // 
            // btnFetchResult
            // 
            this.btnFetchResult.BackColor = System.Drawing.Color.Transparent;
            this.btnFetchResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFetchResult.ForeColor = System.Drawing.Color.Black;
            this.btnFetchResult.Location = new System.Drawing.Point(568, 354);
            this.btnFetchResult.Name = "btnFetchResult";
            this.btnFetchResult.Size = new System.Drawing.Size(162, 40);
            this.btnFetchResult.TabIndex = 21;
            this.btnFetchResult.Text = "Fetch Results";
            this.btnFetchResult.UseVisualStyleBackColor = false;
            // 
            // li_tournament
            // 
            this.li_tournament.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.li_tournament.FormattingEnabled = true;
            this.li_tournament.Location = new System.Drawing.Point(546, 63);
            this.li_tournament.Name = "li_tournament";
            this.li_tournament.Size = new System.Drawing.Size(131, 28);
            this.li_tournament.TabIndex = 22;
            // 
            // lb_tournament
            // 
            this.lb_tournament.AutoSize = true;
            this.lb_tournament.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_tournament.Location = new System.Drawing.Point(441, 66);
            this.lb_tournament.Name = "lb_tournament";
            this.lb_tournament.Size = new System.Drawing.Size(103, 20);
            this.lb_tournament.TabIndex = 23;
            this.lb_tournament.Text = "Tournament :";
            // 
            // Netball
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lb_tournament);
            this.Controls.Add(this.li_tournament);
            this.Controls.Add(this.btnFetchResult);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.la_total);
            this.Controls.Add(this.la_slash);
            this.Controls.Add(this.la_cu_state);
            this.Controls.Add(this.la_team);
            this.Controls.Add(this.li_round);
            this.Controls.Add(this.lb_round);
            this.Controls.Add(this.lb_competition);
            this.Controls.Add(this.li_competition);
            this.Controls.Add(this.lb_season);
            this.Controls.Add(this.li_season);
            this.Controls.Add(this.lb_logoTxt);
            this.Controls.Add(this.img_logo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Netball";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Netball";
            ((System.ComponentModel.ISupportInitialize)(this.img_logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox img_logo;
        private System.Windows.Forms.Label lb_logoTxt;
        private System.Windows.Forms.ComboBox li_season;
        private System.Windows.Forms.Label lb_season;
        private System.Windows.Forms.ComboBox li_competition;
        private System.Windows.Forms.Label lb_competition;
        private System.Windows.Forms.Label lb_round;
        private System.Windows.Forms.ComboBox li_round;
        private System.Windows.Forms.Label la_team;
        private System.Windows.Forms.Label la_cu_state;
        private System.Windows.Forms.Label la_slash;
        private System.Windows.Forms.Label la_total;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Button btnFetchResult;
        private System.Windows.Forms.ComboBox li_tournament;
        private System.Windows.Forms.Label lb_tournament;
    }
}

