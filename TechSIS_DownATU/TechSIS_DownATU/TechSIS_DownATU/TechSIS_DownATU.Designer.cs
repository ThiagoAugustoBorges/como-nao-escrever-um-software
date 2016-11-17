namespace TechSIS_DownATU
{
    partial class TechSIS_DownATU
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TechSIS_DownATU));
            this.proBar = new System.Windows.Forms.ProgressBar();
            this.lblInf = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // proBar
            // 
            this.proBar.Location = new System.Drawing.Point(22, 66);
            this.proBar.Maximum = 4;
            this.proBar.Name = "proBar";
            this.proBar.Size = new System.Drawing.Size(402, 19);
            this.proBar.TabIndex = 1;
            // 
            // lblInf
            // 
            this.lblInf.AutoSize = true;
            this.lblInf.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInf.Location = new System.Drawing.Point(18, 90);
            this.lblInf.Name = "lblInf";
            this.lblInf.Size = new System.Drawing.Size(14, 14);
            this.lblInf.TabIndex = 2;
            this.lblInf.Text = " ";
            // 
            // picLogo
            // 
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(21, 2);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(401, 61);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 3;
            this.picLogo.TabStop = false;
            // 
            // TechSIS_DownATU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(445, 111);
            this.ControlBox = false;
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.lblInf);
            this.Controls.Add(this.proBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(461, 127);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(461, 127);
            this.Name = "TechSIS_DownATU";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TechSIS_DownATU_FormClosing);
            this.Load += new System.EventHandler(this.TechSIS_DownATU_Load);
            this.Shown += new System.EventHandler(this.TechSIS_DownATU_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar proBar;
        private System.Windows.Forms.Label lblInf;
        private System.Windows.Forms.PictureBox picLogo;
    }
}

