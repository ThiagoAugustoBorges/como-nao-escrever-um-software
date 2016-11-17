namespace TabEmpre
{
    partial class SenhaAcesso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SenhaAcesso));
            this.Painel_First = new System.Windows.Forms.Panel();
            this.Painel_Up = new System.Windows.Forms.Panel();
            this.comModuloSistema = new System.Windows.Forms.ComboBox();
            this.lblModuloSistema = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.picCadeado = new System.Windows.Forms.PictureBox();
            this.lblInfDo = new System.Windows.Forms.Label();
            this.lblAtencao = new System.Windows.Forms.Label();
            this.lblInfUm = new System.Windows.Forms.Label();
            this.lblSenha = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.lblHome = new System.Windows.Forms.Label();
            this.Painel_Down = new System.Windows.Forms.Panel();
            this.lblTechSIS = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAcionar = new System.Windows.Forms.Button();
            this.Painel_First.SuspendLayout();
            this.Painel_Up.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCadeado)).BeginInit();
            this.Painel_Down.SuspendLayout();
            this.SuspendLayout();
            // 
            // Painel_First
            // 
            this.Painel_First.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Painel_First.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Painel_First.Controls.Add(this.Painel_Up);
            this.Painel_First.Controls.Add(this.Painel_Down);
            this.Painel_First.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Painel_First.Location = new System.Drawing.Point(0, 0);
            this.Painel_First.Name = "Painel_First";
            this.Painel_First.Size = new System.Drawing.Size(385, 195);
            this.Painel_First.TabIndex = 0;
            // 
            // Painel_Up
            // 
            this.Painel_Up.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Painel_Up.Controls.Add(this.comModuloSistema);
            this.Painel_Up.Controls.Add(this.lblModuloSistema);
            this.Painel_Up.Controls.Add(this.btnOK);
            this.Painel_Up.Controls.Add(this.picCadeado);
            this.Painel_Up.Controls.Add(this.lblInfDo);
            this.Painel_Up.Controls.Add(this.lblAtencao);
            this.Painel_Up.Controls.Add(this.lblInfUm);
            this.Painel_Up.Controls.Add(this.lblSenha);
            this.Painel_Up.Controls.Add(this.txtSenha);
            this.Painel_Up.Controls.Add(this.lblHome);
            this.Painel_Up.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Painel_Up.Location = new System.Drawing.Point(0, 0);
            this.Painel_Up.Name = "Painel_Up";
            this.Painel_Up.Size = new System.Drawing.Size(383, 154);
            this.Painel_Up.TabIndex = 1;
            // 
            // comModuloSistema
            // 
            this.comModuloSistema.BackColor = System.Drawing.Color.White;
            this.comModuloSistema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comModuloSistema.Enabled = false;
            this.comModuloSistema.ForeColor = System.Drawing.Color.Black;
            this.comModuloSistema.FormattingEnabled = true;
            this.comModuloSistema.Items.AddRange(new object[] {
            "TechSIS Free",
            "TechSIS Express",
            "TechSIS Business",
            "TechSIS Controle",
            "TechSIS Pró"});
            this.comModuloSistema.Location = new System.Drawing.Point(66, 96);
            this.comModuloSistema.Name = "comModuloSistema";
            this.comModuloSistema.Size = new System.Drawing.Size(238, 22);
            this.comModuloSistema.TabIndex = 80;
            this.comModuloSistema.TabStop = false;
            // 
            // lblModuloSistema
            // 
            this.lblModuloSistema.AutoSize = true;
            this.lblModuloSistema.Enabled = false;
            this.lblModuloSistema.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModuloSistema.Location = new System.Drawing.Point(100, 81);
            this.lblModuloSistema.Name = "lblModuloSistema";
            this.lblModuloSistema.Size = new System.Drawing.Size(161, 14);
            this.lblModuloSistema.TabIndex = 81;
            this.lblModuloSistema.Text = "NOVO MÓDULO DO SISTEMA";
            // 
            // btnOK
            // 
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(256, 55);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(48, 26);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // picCadeado
            // 
            this.picCadeado.Image = ((System.Drawing.Image)(resources.GetObject("picCadeado.Image")));
            this.picCadeado.Location = new System.Drawing.Point(66, 44);
            this.picCadeado.Name = "picCadeado";
            this.picCadeado.Size = new System.Drawing.Size(37, 34);
            this.picCadeado.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCadeado.TabIndex = 1;
            this.picCadeado.TabStop = false;
            // 
            // lblInfDo
            // 
            this.lblInfDo.AutoSize = true;
            this.lblInfDo.Location = new System.Drawing.Point(74, 136);
            this.lblInfDo.Name = "lblInfDo";
            this.lblInfDo.Size = new System.Drawing.Size(294, 14);
            this.lblInfDo.TabIndex = 6;
            this.lblInfDo.Text = "TROCA DE MÓDULO NECESSITA DE UM NOVO SecF";
            // 
            // lblAtencao
            // 
            this.lblAtencao.AutoSize = true;
            this.lblAtencao.ForeColor = System.Drawing.Color.Red;
            this.lblAtencao.Location = new System.Drawing.Point(8, 136);
            this.lblAtencao.Name = "lblAtencao";
            this.lblAtencao.Size = new System.Drawing.Size(70, 14);
            this.lblAtencao.TabIndex = 5;
            this.lblAtencao.Text = "ATENÇÃO.:";
            // 
            // lblInfUm
            // 
            this.lblInfUm.AutoSize = true;
            this.lblInfUm.Location = new System.Drawing.Point(11, 121);
            this.lblInfUm.Name = "lblInfUm";
            this.lblInfUm.Size = new System.Drawing.Size(357, 14);
            this.lblInfUm.TabIndex = 4;
            this.lblInfUm.Text = "PARA A TROCA DE MÓDULO A SENHA MESTRA É NECESSÁRIA";
            // 
            // lblSenha
            // 
            this.lblSenha.AutoSize = true;
            this.lblSenha.Location = new System.Drawing.Point(135, 44);
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.Size = new System.Drawing.Size(91, 14);
            this.lblSenha.TabIndex = 3;
            this.lblSenha.Text = "Senha MESTRA";
            // 
            // txtSenha
            // 
            this.txtSenha.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtSenha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSenha.Location = new System.Drawing.Point(109, 58);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(139, 20);
            this.txtSenha.TabIndex = 2;
            this.txtSenha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSenha.UseSystemPasswordChar = true;
            // 
            // lblHome
            // 
            this.lblHome.AutoSize = true;
            this.lblHome.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHome.Location = new System.Drawing.Point(10, 7);
            this.lblHome.Name = "lblHome";
            this.lblHome.Size = new System.Drawing.Size(361, 23);
            this.lblHome.TabIndex = 1;
            this.lblHome.Text = "DIGITE A SENHA DE SEGURANÇA";
            // 
            // Painel_Down
            // 
            this.Painel_Down.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Painel_Down.Controls.Add(this.lblTechSIS);
            this.Painel_Down.Controls.Add(this.btnCancelar);
            this.Painel_Down.Controls.Add(this.btnAcionar);
            this.Painel_Down.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Painel_Down.Location = new System.Drawing.Point(0, 154);
            this.Painel_Down.Name = "Painel_Down";
            this.Painel_Down.Size = new System.Drawing.Size(383, 39);
            this.Painel_Down.TabIndex = 0;
            // 
            // lblTechSIS
            // 
            this.lblTechSIS.AutoSize = true;
            this.lblTechSIS.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblTechSIS.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTechSIS.ForeColor = System.Drawing.Color.Crimson;
            this.lblTechSIS.Location = new System.Drawing.Point(20, 11);
            this.lblTechSIS.Name = "lblTechSIS";
            this.lblTechSIS.Size = new System.Drawing.Size(153, 23);
            this.lblTechSIS.TabIndex = 7;
            this.lblTechSIS.Text = "TechSIS BWK";
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(207, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(86, 30);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAcionar
            // 
            this.btnAcionar.Enabled = false;
            this.btnAcionar.Image = ((System.Drawing.Image)(resources.GetObject("btnAcionar.Image")));
            this.btnAcionar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAcionar.Location = new System.Drawing.Point(292, 4);
            this.btnAcionar.Name = "btnAcionar";
            this.btnAcionar.Size = new System.Drawing.Size(86, 30);
            this.btnAcionar.TabIndex = 2;
            this.btnAcionar.Text = "ACIONAR";
            this.btnAcionar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAcionar.UseVisualStyleBackColor = true;
            this.btnAcionar.Click += new System.EventHandler(this.btnAcionar_Click);
            // 
            // SenhaAcesso
            // 
            this.AcceptButton = this.btnAcionar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(385, 195);
            this.ControlBox = false;
            this.Controls.Add(this.Painel_First);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(401, 209);
            this.Name = "SenhaAcesso";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechSIS BWK - Segurança";
            this.Load += new System.EventHandler(this.SenhaAcesso_Load);
            this.Painel_First.ResumeLayout(false);
            this.Painel_Up.ResumeLayout(false);
            this.Painel_Up.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCadeado)).EndInit();
            this.Painel_Down.ResumeLayout(false);
            this.Painel_Down.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Painel_First;
        private System.Windows.Forms.Panel Painel_Up;
        private System.Windows.Forms.PictureBox picCadeado;
        private System.Windows.Forms.Label lblInfDo;
        private System.Windows.Forms.Label lblAtencao;
        private System.Windows.Forms.Label lblInfUm;
        private System.Windows.Forms.Label lblSenha;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Label lblHome;
        private System.Windows.Forms.Panel Painel_Down;
        private System.Windows.Forms.Label lblTechSIS;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAcionar;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox comModuloSistema;
        public System.Windows.Forms.Label lblModuloSistema;
    }
}