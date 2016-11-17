namespace TabNewSe
{
    partial class TabNewSe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabNewSe));
            this.panPrin = new System.Windows.Forms.Panel();
            this.panUp = new System.Windows.Forms.Panel();
            this.grbSenhas = new System.Windows.Forms.GroupBox();
            this.lblConSe = new System.Windows.Forms.Label();
            this.lblNoSe = new System.Windows.Forms.Label();
            this.lblSeAtu = new System.Windows.Forms.Label();
            this.txtSenhaConfir = new System.Windows.Forms.TextBox();
            this.txtSenhaNova = new System.Windows.Forms.TextBox();
            this.txtSenhaAnt = new System.Windows.Forms.TextBox();
            this.grbUsuarioTrocar = new System.Windows.Forms.GroupBox();
            this.txtDescri = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblSepa = new System.Windows.Forms.Label();
            this.lblTech = new System.Windows.Forms.Label();
            this.panDown = new System.Windows.Forms.Panel();
            this.grbUsuarLogado = new System.Windows.Forms.GroupBox();
            this.lblUsuarioLogado = new System.Windows.Forms.Label();
            this.txtUsuarioLogado = new System.Windows.Forms.TextBox();
            this.panButtonsDownAb1 = new System.Windows.Forms.Panel();
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnZerar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.panPrin.SuspendLayout();
            this.panUp.SuspendLayout();
            this.grbSenhas.SuspendLayout();
            this.grbUsuarioTrocar.SuspendLayout();
            this.panDown.SuspendLayout();
            this.grbUsuarLogado.SuspendLayout();
            this.panButtonsDownAb1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panPrin
            // 
            this.panPrin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPrin.Controls.Add(this.panUp);
            this.panPrin.Controls.Add(this.panDown);
            this.panPrin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPrin.Location = new System.Drawing.Point(0, 0);
            this.panPrin.Name = "panPrin";
            this.panPrin.Size = new System.Drawing.Size(498, 250);
            this.panPrin.TabIndex = 0;
            // 
            // panUp
            // 
            this.panUp.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panUp.Controls.Add(this.grbSenhas);
            this.panUp.Controls.Add(this.grbUsuarioTrocar);
            this.panUp.Controls.Add(this.lblSepa);
            this.panUp.Controls.Add(this.lblTech);
            this.panUp.Dock = System.Windows.Forms.DockStyle.Top;
            this.panUp.Location = new System.Drawing.Point(0, 0);
            this.panUp.Name = "panUp";
            this.panUp.Size = new System.Drawing.Size(496, 211);
            this.panUp.TabIndex = 2;
            // 
            // grbSenhas
            // 
            this.grbSenhas.Controls.Add(this.lblConSe);
            this.grbSenhas.Controls.Add(this.lblNoSe);
            this.grbSenhas.Controls.Add(this.lblSeAtu);
            this.grbSenhas.Controls.Add(this.txtSenhaConfir);
            this.grbSenhas.Controls.Add(this.txtSenhaNova);
            this.grbSenhas.Controls.Add(this.txtSenhaAnt);
            this.grbSenhas.Location = new System.Drawing.Point(23, 106);
            this.grbSenhas.Name = "grbSenhas";
            this.grbSenhas.Size = new System.Drawing.Size(451, 80);
            this.grbSenhas.TabIndex = 3;
            this.grbSenhas.TabStop = false;
            // 
            // lblConSe
            // 
            this.lblConSe.AutoSize = true;
            this.lblConSe.Location = new System.Drawing.Point(317, 25);
            this.lblConSe.Name = "lblConSe";
            this.lblConSe.Size = new System.Drawing.Size(84, 14);
            this.lblConSe.TabIndex = 5;
            this.lblConSe.Text = "CONFIRMAÇÃO";
            // 
            // lblNoSe
            // 
            this.lblNoSe.AutoSize = true;
            this.lblNoSe.Location = new System.Drawing.Point(184, 25);
            this.lblNoSe.Name = "lblNoSe";
            this.lblNoSe.Size = new System.Drawing.Size(77, 14);
            this.lblNoSe.TabIndex = 4;
            this.lblNoSe.Text = "NOVA SENHA";
            // 
            // lblSeAtu
            // 
            this.lblSeAtu.AutoSize = true;
            this.lblSeAtu.Location = new System.Drawing.Point(42, 25);
            this.lblSeAtu.Name = "lblSeAtu";
            this.lblSeAtu.Size = new System.Drawing.Size(84, 14);
            this.lblSeAtu.TabIndex = 3;
            this.lblSeAtu.Text = "SENHA ATUAL";
            // 
            // txtSenhaConfir
            // 
            this.txtSenhaConfir.BackColor = System.Drawing.Color.White;
            this.txtSenhaConfir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSenhaConfir.Location = new System.Drawing.Point(322, 39);
            this.txtSenhaConfir.MaxLength = 7;
            this.txtSenhaConfir.Name = "txtSenhaConfir";
            this.txtSenhaConfir.Size = new System.Drawing.Size(75, 20);
            this.txtSenhaConfir.TabIndex = 2;
            this.txtSenhaConfir.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSenhaConfir.UseSystemPasswordChar = true;
            this.txtSenhaConfir.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSenhaConfir_KeyPress);
            this.txtSenhaConfir.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtSenhaConfir_MouseDown);
            // 
            // txtSenhaNova
            // 
            this.txtSenhaNova.BackColor = System.Drawing.Color.White;
            this.txtSenhaNova.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSenhaNova.Location = new System.Drawing.Point(186, 39);
            this.txtSenhaNova.MaxLength = 7;
            this.txtSenhaNova.Name = "txtSenhaNova";
            this.txtSenhaNova.Size = new System.Drawing.Size(75, 20);
            this.txtSenhaNova.TabIndex = 1;
            this.txtSenhaNova.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSenhaNova.UseSystemPasswordChar = true;
            this.txtSenhaNova.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSenhaNova_KeyPress);
            this.txtSenhaNova.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtSenhaNova_MouseDown);
            // 
            // txtSenhaAnt
            // 
            this.txtSenhaAnt.BackColor = System.Drawing.Color.White;
            this.txtSenhaAnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSenhaAnt.Location = new System.Drawing.Point(47, 39);
            this.txtSenhaAnt.MaxLength = 7;
            this.txtSenhaAnt.Name = "txtSenhaAnt";
            this.txtSenhaAnt.Size = new System.Drawing.Size(75, 20);
            this.txtSenhaAnt.TabIndex = 0;
            this.txtSenhaAnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSenhaAnt.UseSystemPasswordChar = true;
            this.txtSenhaAnt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSenhaAnt_KeyPress);
            this.txtSenhaAnt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtSenhaAnt_MouseDown);
            this.txtSenhaAnt.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtSenhaAnt_PreviewKeyDown);
            // 
            // grbUsuarioTrocar
            // 
            this.grbUsuarioTrocar.Controls.Add(this.txtDescri);
            this.grbUsuarioTrocar.Controls.Add(this.txtCodigo);
            this.grbUsuarioTrocar.Location = new System.Drawing.Point(23, 60);
            this.grbUsuarioTrocar.Name = "grbUsuarioTrocar";
            this.grbUsuarioTrocar.Size = new System.Drawing.Size(451, 40);
            this.grbUsuarioTrocar.TabIndex = 2;
            this.grbUsuarioTrocar.TabStop = false;
            // 
            // txtDescri
            // 
            this.txtDescri.BackColor = System.Drawing.Color.White;
            this.txtDescri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescri.Enabled = false;
            this.txtDescri.Location = new System.Drawing.Point(62, 13);
            this.txtDescri.Name = "txtDescri";
            this.txtDescri.Size = new System.Drawing.Size(383, 20);
            this.txtDescri.TabIndex = 6;
            this.txtDescri.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.PaleGreen;
            this.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigo.Location = new System.Drawing.Point(5, 13);
            this.txtCodigo.MaxLength = 6;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(57, 20);
            this.txtCodigo.TabIndex = 5;
            this.txtCodigo.Tag = "";
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigo.TextChanged += new System.EventHandler(this.txtCodigo_TextChanged);
            this.txtCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyDown);
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            this.txtCodigo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtCodigo_MouseDown);
            this.txtCodigo.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtCodigo_PreviewKeyDown);
            // 
            // lblSepa
            // 
            this.lblSepa.AutoSize = true;
            this.lblSepa.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSepa.Location = new System.Drawing.Point(-5, 49);
            this.lblSepa.Name = "lblSepa";
            this.lblSepa.Size = new System.Drawing.Size(508, 7);
            this.lblSepa.TabIndex = 1;
            this.lblSepa.Text = "---------------------------------------------------------------------------------" +
    "--------------------------------------------------------------------------------" +
    "-------";
            // 
            // lblTech
            // 
            this.lblTech.AutoSize = true;
            this.lblTech.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTech.Location = new System.Drawing.Point(47, 0);
            this.lblTech.Name = "lblTech";
            this.lblTech.Size = new System.Drawing.Size(399, 55);
            this.lblTech.TabIndex = 0;
            this.lblTech.Text = "TechSIS Senhas";
            // 
            // panDown
            // 
            this.panDown.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panDown.Controls.Add(this.grbUsuarLogado);
            this.panDown.Controls.Add(this.panButtonsDownAb1);
            this.panDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panDown.Location = new System.Drawing.Point(0, 211);
            this.panDown.Name = "panDown";
            this.panDown.Size = new System.Drawing.Size(496, 37);
            this.panDown.TabIndex = 1;
            // 
            // grbUsuarLogado
            // 
            this.grbUsuarLogado.Controls.Add(this.lblUsuarioLogado);
            this.grbUsuarLogado.Controls.Add(this.txtUsuarioLogado);
            this.grbUsuarLogado.Location = new System.Drawing.Point(0, -5);
            this.grbUsuarLogado.Name = "grbUsuarLogado";
            this.grbUsuarLogado.Size = new System.Drawing.Size(179, 39);
            this.grbUsuarLogado.TabIndex = 6;
            this.grbUsuarLogado.TabStop = false;
            // 
            // lblUsuarioLogado
            // 
            this.lblUsuarioLogado.AutoSize = true;
            this.lblUsuarioLogado.Location = new System.Drawing.Point(6, 19);
            this.lblUsuarioLogado.Name = "lblUsuarioLogado";
            this.lblUsuarioLogado.Size = new System.Drawing.Size(70, 14);
            this.lblUsuarioLogado.TabIndex = 6;
            this.lblUsuarioLogado.Text = "USUÁRIO.:";
            // 
            // txtUsuarioLogado
            // 
            this.txtUsuarioLogado.BackColor = System.Drawing.Color.White;
            this.txtUsuarioLogado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsuarioLogado.Enabled = false;
            this.txtUsuarioLogado.Location = new System.Drawing.Point(76, 13);
            this.txtUsuarioLogado.MaxLength = 7;
            this.txtUsuarioLogado.Name = "txtUsuarioLogado";
            this.txtUsuarioLogado.Size = new System.Drawing.Size(75, 20);
            this.txtUsuarioLogado.TabIndex = 6;
            this.txtUsuarioLogado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panButtonsDownAb1
            // 
            this.panButtonsDownAb1.BackColor = System.Drawing.SystemColors.Control;
            this.panButtonsDownAb1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panButtonsDownAb1.Controls.Add(this.btnGravar);
            this.panButtonsDownAb1.Controls.Add(this.btnZerar);
            this.panButtonsDownAb1.Controls.Add(this.btnFechar);
            this.panButtonsDownAb1.Location = new System.Drawing.Point(179, -1);
            this.panButtonsDownAb1.Name = "panButtonsDownAb1";
            this.panButtonsDownAb1.Size = new System.Drawing.Size(315, 37);
            this.panButtonsDownAb1.TabIndex = 5;
            // 
            // btnGravar
            // 
            this.btnGravar.Enabled = false;
            this.btnGravar.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGravar.Image = ((System.Drawing.Image)(resources.GetObject("btnGravar.Image")));
            this.btnGravar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGravar.Location = new System.Drawing.Point(6, 3);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(101, 29);
            this.btnGravar.TabIndex = 1;
            this.btnGravar.Text = "&Gravar - F10";
            this.btnGravar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // btnZerar
            // 
            this.btnZerar.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZerar.Image = ((System.Drawing.Image)(resources.GetObject("btnZerar.Image")));
            this.btnZerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnZerar.Location = new System.Drawing.Point(106, 3);
            this.btnZerar.Name = "btnZerar";
            this.btnZerar.Size = new System.Drawing.Size(101, 29);
            this.btnZerar.TabIndex = 2;
            this.btnZerar.Text = "&Zerar - F9";
            this.btnZerar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnZerar.UseVisualStyleBackColor = true;
            this.btnZerar.Click += new System.EventHandler(this.btnZerar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFechar.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar.Image = ((System.Drawing.Image)(resources.GetObject("btnFechar.Image")));
            this.btnFechar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFechar.Location = new System.Drawing.Point(206, 3);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(101, 29);
            this.btnFechar.TabIndex = 3;
            this.btnFechar.Text = "&Fechar - F7";
            this.btnFechar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // TabNewSe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 250);
            this.Controls.Add(this.panPrin);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(514, 289);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(514, 289);
            this.Name = "TabNewSe";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manutenção de Senhas.: 09-05-00        TabNewSe.dll";
            this.Load += new System.EventHandler(this.TabNewSe_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TabNewSe_KeyDown);
            this.panPrin.ResumeLayout(false);
            this.panUp.ResumeLayout(false);
            this.panUp.PerformLayout();
            this.grbSenhas.ResumeLayout(false);
            this.grbSenhas.PerformLayout();
            this.grbUsuarioTrocar.ResumeLayout(false);
            this.grbUsuarioTrocar.PerformLayout();
            this.panDown.ResumeLayout(false);
            this.grbUsuarLogado.ResumeLayout(false);
            this.grbUsuarLogado.PerformLayout();
            this.panButtonsDownAb1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panPrin;
        private System.Windows.Forms.Panel panUp;
        private System.Windows.Forms.Panel panDown;
        private System.Windows.Forms.Label lblSepa;
        private System.Windows.Forms.Label lblTech;
        private System.Windows.Forms.GroupBox grbUsuarioTrocar;
        private System.Windows.Forms.TextBox txtDescri;
        public System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.GroupBox grbSenhas;
        private System.Windows.Forms.TextBox txtSenhaConfir;
        private System.Windows.Forms.TextBox txtSenhaNova;
        private System.Windows.Forms.TextBox txtSenhaAnt;
        private System.Windows.Forms.Label lblConSe;
        private System.Windows.Forms.Label lblNoSe;
        private System.Windows.Forms.Label lblSeAtu;
        public System.Windows.Forms.Panel panButtonsDownAb1;
        public System.Windows.Forms.Button btnGravar;
        public System.Windows.Forms.Button btnZerar;
        public System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.GroupBox grbUsuarLogado;
        private System.Windows.Forms.Label lblUsuarioLogado;
        private System.Windows.Forms.TextBox txtUsuarioLogado;
    }
}