namespace TechSIS_BWK
{
    partial class Sobre_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sobre_Form));
            this.Painel = new System.Windows.Forms.Panel();
            this.btnFechar = new System.Windows.Forms.Button();
            this.picName = new System.Windows.Forms.PictureBox();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.txtDescri = new System.Windows.Forms.TextBox();
            this.Painel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // Painel
            // 
            this.Painel.BackColor = System.Drawing.Color.White;
            this.Painel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Painel.Controls.Add(this.txtDescri);
            this.Painel.Controls.Add(this.btnFechar);
            this.Painel.Controls.Add(this.picName);
            this.Painel.Controls.Add(this.picLogo);
            this.Painel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Painel.Location = new System.Drawing.Point(0, 0);
            this.Painel.Name = "Painel";
            this.Painel.Size = new System.Drawing.Size(566, 315);
            this.Painel.TabIndex = 0;
            // 
            // btnFechar
            // 
            this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar.Image = ((System.Drawing.Image)(resources.GetObject("btnFechar.Image")));
            this.btnFechar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFechar.Location = new System.Drawing.Point(440, 276);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(121, 34);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.Text = "Sair";
            this.btnFechar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // picName
            // 
            this.picName.Image = ((System.Drawing.Image)(resources.GetObject("picName.Image")));
            this.picName.Location = new System.Drawing.Point(118, 3);
            this.picName.Name = "picName";
            this.picName.Size = new System.Drawing.Size(423, 85);
            this.picName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picName.TabIndex = 1;
            this.picName.TabStop = false;
            // 
            // picLogo
            // 
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(18, 3);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(94, 85);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // txtDescri
            // 
            this.txtDescri.BackColor = System.Drawing.Color.White;
            this.txtDescri.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescri.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtDescri.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescri.Location = new System.Drawing.Point(18, 94);
            this.txtDescri.Multiline = true;
            this.txtDescri.Name = "txtDescri";
            this.txtDescri.ReadOnly = true;
            this.txtDescri.Size = new System.Drawing.Size(523, 176);
            this.txtDescri.TabIndex = 3;
            this.txtDescri.Text = resources.GetString("txtDescri.Text");
            // 
            // Sobre_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 315);
            this.ControlBox = false;
            this.Controls.Add(this.Painel);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(582, 331);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(582, 331);
            this.Name = "Sobre_Form";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Painel.ResumeLayout(false);
            this.Painel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Painel;
        private System.Windows.Forms.PictureBox picName;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.TextBox txtDescri;
    }
}