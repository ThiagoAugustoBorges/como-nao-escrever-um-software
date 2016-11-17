namespace TabUsuar
{
    partial class TabUsuar_SenhaINC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabUsuar_SenhaINC));
            this.panPrin = new System.Windows.Forms.Panel();
            this.panUP = new System.Windows.Forms.Panel();
            this.grbSenha = new System.Windows.Forms.GroupBox();
            this.txtConfirmacao = new System.Windows.Forms.TextBox();
            this.lblConfirmacao = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.lblSenha = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.panDown = new System.Windows.Forms.Panel();
            this.picTechSIS = new System.Windows.Forms.PictureBox();
            this.lblTechSIS = new System.Windows.Forms.Label();
            this.panButtonsDownAb1 = new System.Windows.Forms.Panel();
            this.btnConfirma = new System.Windows.Forms.Button();
            this.panPrin.SuspendLayout();
            this.panUP.SuspendLayout();
            this.grbSenha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.panDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTechSIS)).BeginInit();
            this.panButtonsDownAb1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panPrin
            // 
            this.panPrin.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panPrin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPrin.Controls.Add(this.panUP);
            this.panPrin.Controls.Add(this.panDown);
            this.panPrin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPrin.Location = new System.Drawing.Point(0, 0);
            this.panPrin.Name = "panPrin";
            this.panPrin.Size = new System.Drawing.Size(443, 199);
            this.panPrin.TabIndex = 0;
            // 
            // panUP
            // 
            this.panUP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panUP.Controls.Add(this.grbSenha);
            this.panUP.Controls.Add(this.lblTitulo);
            this.panUP.Controls.Add(this.picImage);
            this.panUP.Dock = System.Windows.Forms.DockStyle.Top;
            this.panUP.Location = new System.Drawing.Point(0, 0);
            this.panUP.Name = "panUP";
            this.panUP.Size = new System.Drawing.Size(441, 161);
            this.panUP.TabIndex = 2;
            // 
            // grbSenha
            // 
            this.grbSenha.BackColor = System.Drawing.SystemColors.ControlDark;
            this.grbSenha.Controls.Add(this.txtConfirmacao);
            this.grbSenha.Controls.Add(this.lblConfirmacao);
            this.grbSenha.Controls.Add(this.txtSenha);
            this.grbSenha.Controls.Add(this.lblSenha);
            this.grbSenha.ForeColor = System.Drawing.Color.Blue;
            this.grbSenha.Location = new System.Drawing.Point(123, 40);
            this.grbSenha.Name = "grbSenha";
            this.grbSenha.Size = new System.Drawing.Size(194, 100);
            this.grbSenha.TabIndex = 2;
            this.grbSenha.TabStop = false;
            this.grbSenha.Text = "SENHAS";
            // 
            // txtConfirmacao
            // 
            this.txtConfirmacao.BackColor = System.Drawing.Color.White;
            this.txtConfirmacao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirmacao.Location = new System.Drawing.Point(47, 74);
            this.txtConfirmacao.MaxLength = 7;
            this.txtConfirmacao.Name = "txtConfirmacao";
            this.txtConfirmacao.Size = new System.Drawing.Size(100, 20);
            this.txtConfirmacao.TabIndex = 3;
            this.txtConfirmacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtConfirmacao.UseSystemPasswordChar = true;
            this.txtConfirmacao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConfirmacao_KeyPress);
            // 
            // lblConfirmacao
            // 
            this.lblConfirmacao.AutoSize = true;
            this.lblConfirmacao.Location = new System.Drawing.Point(54, 57);
            this.lblConfirmacao.Name = "lblConfirmacao";
            this.lblConfirmacao.Size = new System.Drawing.Size(84, 14);
            this.lblConfirmacao.TabIndex = 2;
            this.lblConfirmacao.Text = "CONFIRMAÇÃO";
            // 
            // txtSenha
            // 
            this.txtSenha.BackColor = System.Drawing.Color.White;
            this.txtSenha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSenha.Location = new System.Drawing.Point(47, 33);
            this.txtSenha.MaxLength = 7;
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(100, 20);
            this.txtSenha.TabIndex = 1;
            this.txtSenha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSenha.UseSystemPasswordChar = true;
            this.txtSenha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSenha_KeyPress);
            // 
            // lblSenha
            // 
            this.lblSenha.AutoSize = true;
            this.lblSenha.Location = new System.Drawing.Point(72, 16);
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.Size = new System.Drawing.Size(42, 14);
            this.lblSenha.TabIndex = 0;
            this.lblSenha.Text = "SENHA";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(49, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(384, 22);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "DIGITE A SENHA DESEJADA DO USUÁRIO";
            // 
            // picImage
            // 
            this.picImage.Image = ((System.Drawing.Image)(resources.GetObject("picImage.Image")));
            this.picImage.Location = new System.Drawing.Point(3, 3);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(46, 41);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImage.TabIndex = 0;
            this.picImage.TabStop = false;
            // 
            // panDown
            // 
            this.panDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panDown.Controls.Add(this.picTechSIS);
            this.panDown.Controls.Add(this.lblTechSIS);
            this.panDown.Controls.Add(this.panButtonsDownAb1);
            this.panDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panDown.Location = new System.Drawing.Point(0, 160);
            this.panDown.Name = "panDown";
            this.panDown.Size = new System.Drawing.Size(441, 37);
            this.panDown.TabIndex = 1;
            // 
            // picTechSIS
            // 
            this.picTechSIS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picTechSIS.Image = ((System.Drawing.Image)(resources.GetObject("picTechSIS.Image")));
            this.picTechSIS.Location = new System.Drawing.Point(95, 0);
            this.picTechSIS.Name = "picTechSIS";
            this.picTechSIS.Size = new System.Drawing.Size(47, 35);
            this.picTechSIS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTechSIS.TabIndex = 3;
            this.picTechSIS.TabStop = false;
            // 
            // lblTechSIS
            // 
            this.lblTechSIS.AutoSize = true;
            this.lblTechSIS.Font = new System.Drawing.Font("Segoe Print", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTechSIS.Location = new System.Drawing.Point(-2, -1);
            this.lblTechSIS.Name = "lblTechSIS";
            this.lblTechSIS.Size = new System.Drawing.Size(99, 37);
            this.lblTechSIS.TabIndex = 4;
            this.lblTechSIS.Text = "TechSIS";
            // 
            // panButtonsDownAb1
            // 
            this.panButtonsDownAb1.BackColor = System.Drawing.SystemColors.Control;
            this.panButtonsDownAb1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panButtonsDownAb1.Controls.Add(this.btnConfirma);
            this.panButtonsDownAb1.Location = new System.Drawing.Point(313, -1);
            this.panButtonsDownAb1.Name = "panButtonsDownAb1";
            this.panButtonsDownAb1.Size = new System.Drawing.Size(127, 37);
            this.panButtonsDownAb1.TabIndex = 6;
            // 
            // btnConfirma
            // 
            this.btnConfirma.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirma.Image = ((System.Drawing.Image)(resources.GetObject("btnConfirma.Image")));
            this.btnConfirma.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirma.Location = new System.Drawing.Point(3, 3);
            this.btnConfirma.Name = "btnConfirma";
            this.btnConfirma.Size = new System.Drawing.Size(119, 29);
            this.btnConfirma.TabIndex = 1;
            this.btnConfirma.Text = "&Confirma - F1";
            this.btnConfirma.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConfirma.UseVisualStyleBackColor = true;
            this.btnConfirma.Click += new System.EventHandler(this.btnConfirma_Click);
            // 
            // TabUsuar_SenhaINC
            // 
            this.AcceptButton = this.btnConfirma;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 199);
            this.ControlBox = false;
            this.Controls.Add(this.panPrin);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(459, 238);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(459, 238);
            this.Name = "TabUsuar_SenhaINC";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechSIS - Senha Usuário";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TabUsuar_SenhaINC_FormClosing);
            this.Load += new System.EventHandler(this.TabUsuar_SenhaINC_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TabUsuar_SenhaINC_KeyDown);
            this.panPrin.ResumeLayout(false);
            this.panUP.ResumeLayout(false);
            this.panUP.PerformLayout();
            this.grbSenha.ResumeLayout(false);
            this.grbSenha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.panDown.ResumeLayout(false);
            this.panDown.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTechSIS)).EndInit();
            this.panButtonsDownAb1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panPrin;
        private System.Windows.Forms.Panel panUP;
        private System.Windows.Forms.Panel panDown;
        public System.Windows.Forms.Panel panButtonsDownAb1;
        public System.Windows.Forms.Button btnConfirma;
        private System.Windows.Forms.GroupBox grbSenha;
        private System.Windows.Forms.TextBox txtConfirmacao;
        private System.Windows.Forms.Label lblConfirmacao;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Label lblSenha;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Label lblTechSIS;
        private System.Windows.Forms.PictureBox picTechSIS;
    }
}