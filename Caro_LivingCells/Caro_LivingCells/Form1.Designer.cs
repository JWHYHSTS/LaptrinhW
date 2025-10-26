namespace BTL
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnRedo = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnCom = new System.Windows.Forms.Button();
            this.pnlBanco = new System.Windows.Forms.Panel();
            this.btnPvP = new System.Windows.Forms.Button();
            this.btnAlphaBeta = new System.Windows.Forms.Button();
            this.timerTurn = new System.Windows.Forms.Timer(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.btnreset = new System.Windows.Forms.Button();
            this.guna2CustomGradientPanel2 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.toggleMusic = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2CustomGradientPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRedo
            // 
            this.btnRedo.Font = new System.Drawing.Font("Times New Roman", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedo.Location = new System.Drawing.Point(48, 773);
            this.btnRedo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(298, 80);
            this.btnRedo.TabIndex = 4;
            this.btnRedo.Text = "Tiến 1 bước";
            this.btnRedo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Red;
            this.button3.Font = new System.Drawing.Font("Times New Roman", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(48, 973);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(298, 80);
            this.button3.TabIndex = 3;
            this.button3.Text = "Thoát";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Font = new System.Drawing.Font("Times New Roman", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUndo.Location = new System.Drawing.Point(48, 673);
            this.btnUndo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(298, 80);
            this.btnUndo.TabIndex = 2;
            this.btnUndo.Text = "Lùi 1 bước";
            this.btnUndo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnCom
            // 
            this.btnCom.Font = new System.Drawing.Font("Times New Roman", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCom.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCom.Location = new System.Drawing.Point(48, 473);
            this.btnCom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCom.Name = "btnCom";
            this.btnCom.Size = new System.Drawing.Size(298, 80);
            this.btnCom.TabIndex = 1;
            this.btnCom.Text = "Lv1: dễ";
            this.btnCom.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCom.UseVisualStyleBackColor = true;
            this.btnCom.Click += new System.EventHandler(this.btnCom_Click);
            // 
            // pnlBanco
            // 
            this.pnlBanco.BackColor = System.Drawing.Color.Lime;
            this.pnlBanco.Location = new System.Drawing.Point(411, 103);
            this.pnlBanco.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlBanco.Name = "pnlBanco";
            this.pnlBanco.Size = new System.Drawing.Size(1009, 955);
            this.pnlBanco.TabIndex = 5;
            this.pnlBanco.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBanco_Paint);
            this.pnlBanco.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlBanco_MouseClick);
            // 
            // btnPvP
            // 
            this.btnPvP.Font = new System.Drawing.Font("Times New Roman", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPvP.Location = new System.Drawing.Point(48, 873);
            this.btnPvP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPvP.Name = "btnPvP";
            this.btnPvP.Size = new System.Drawing.Size(298, 80);
            this.btnPvP.TabIndex = 6;
            this.btnPvP.Text = "P vs P";
            this.btnPvP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPvP.UseVisualStyleBackColor = true;
            this.btnPvP.Click += new System.EventHandler(this.btnPvP_Click);
            // 
            // btnAlphaBeta
            // 
            this.btnAlphaBeta.Font = new System.Drawing.Font("Times New Roman", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlphaBeta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAlphaBeta.Location = new System.Drawing.Point(48, 573);
            this.btnAlphaBeta.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAlphaBeta.Name = "btnAlphaBeta";
            this.btnAlphaBeta.Size = new System.Drawing.Size(298, 80);
            this.btnAlphaBeta.TabIndex = 7;
            this.btnAlphaBeta.Text = "Lv2: khó";
            this.btnAlphaBeta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAlphaBeta.UseVisualStyleBackColor = true;
            this.btnAlphaBeta.Click += new System.EventHandler(this.btnAlphaBeta_Click);
            // 
            // timerTurn
            // 
            this.timerTurn.Interval = 1000;
            this.timerTurn.Tick += new System.EventHandler(this.timerTurn_Tick);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Arial", 25.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(1295, 9);
            this.lblTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(110, 79);
            this.lblTime.TabIndex = 8;
            this.lblTime.Text = "00";
            // 
            // btnreset
            // 
            this.btnreset.BackColor = System.Drawing.Color.White;
            this.btnreset.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnreset.ForeColor = System.Drawing.Color.Black;
            this.btnreset.Location = new System.Drawing.Point(48, 373);
            this.btnreset.Name = "btnreset";
            this.btnreset.Size = new System.Drawing.Size(298, 80);
            this.btnreset.TabIndex = 9;
            this.btnreset.Text = "Reset";
            this.btnreset.UseVisualStyleBackColor = false;
            this.btnreset.Click += new System.EventHandler(this.btnreset_Click);
            // 
            // guna2CustomGradientPanel2
            // 
            this.guna2CustomGradientPanel2.Controls.Add(this.pictureBox1);
            this.guna2CustomGradientPanel2.Controls.Add(this.guna2Button1);
            this.guna2CustomGradientPanel2.Controls.Add(this.toggleMusic);
            this.guna2CustomGradientPanel2.Controls.Add(this.btnreset);
            this.guna2CustomGradientPanel2.Controls.Add(this.guna2PictureBox1);
            this.guna2CustomGradientPanel2.Controls.Add(this.lblTime);
            this.guna2CustomGradientPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2CustomGradientPanel2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2CustomGradientPanel2.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.guna2CustomGradientPanel2.FillColor3 = System.Drawing.Color.WhiteSmoke;
            this.guna2CustomGradientPanel2.Location = new System.Drawing.Point(0, 0);
            this.guna2CustomGradientPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2CustomGradientPanel2.Name = "guna2CustomGradientPanel2";
            this.guna2CustomGradientPanel2.Size = new System.Drawing.Size(1490, 1061);
            this.guna2CustomGradientPanel2.TabIndex = 17;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::BTL.Properties.Resources.icon_am_thanh;
            this.pictureBox1.Location = new System.Drawing.Point(361, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(110, 94);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // guna2Button1
            // 
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(686, 11);
            this.guna2Button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(270, 70);
            this.guna2Button1.TabIndex = 28;
            this.guna2Button1.Text = "BẢNG XẾP HẠNG";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // toggleMusic
            // 
            this.toggleMusic.BackColor = System.Drawing.Color.Transparent;
            this.toggleMusic.Checked = true;
            this.toggleMusic.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.toggleMusic.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.toggleMusic.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleMusic.CheckedState.InnerColor = System.Drawing.Color.White;
            this.toggleMusic.Location = new System.Drawing.Point(478, 33);
            this.toggleMusic.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.toggleMusic.Name = "toggleMusic";
            this.toggleMusic.Size = new System.Drawing.Size(90, 34);
            this.toggleMusic.TabIndex = 27;
            this.toggleMusic.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.toggleMusic.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.toggleMusic.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleMusic.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.toggleMusic.CheckedChanged += new System.EventHandler(this.toggleMusic_CheckedChanged_1);
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.BorderRadius = 10;
            this.guna2PictureBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.Image")));
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(39, 38);
            this.guna2PictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(300, 312);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 18;
            this.guna2PictureBox1.TabStop = false;
            this.guna2PictureBox1.UseTransparentBackground = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1490, 1061);
            this.Controls.Add(this.btnAlphaBeta);
            this.Controls.Add(this.btnPvP);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnRedo);
            this.Controls.Add(this.pnlBanco);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnCom);
            this.Controls.Add(this.guna2CustomGradientPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cờ caro";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.guna2CustomGradientPanel2.ResumeLayout(false);
            this.guna2CustomGradientPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCom;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.Panel pnlBanco;
        private System.Windows.Forms.Button btnPvP;
        private System.Windows.Forms.Button btnAlphaBeta;
        private System.Windows.Forms.Timer timerTurn;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnreset;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel2;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2ToggleSwitch toggleMusic;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

