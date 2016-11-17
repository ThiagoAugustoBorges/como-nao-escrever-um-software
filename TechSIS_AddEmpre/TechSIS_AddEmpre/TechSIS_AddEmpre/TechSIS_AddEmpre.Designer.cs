namespace TechSIS_AddEmpre
{
    partial class TechSIS_AddEmpre
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TechSIS_AddEmpre));
            this.panPrin = new System.Windows.Forms.Panel();
            this.btnSair = new System.Windows.Forms.Button();
            this.grb1 = new System.Windows.Forms.GroupBox();
            this.btnInserir = new System.Windows.Forms.Button();
            this.lblSistema = new System.Windows.Forms.Label();
            this.lblModuloProvi = new System.Windows.Forms.Label();
            this.lblModulo = new System.Windows.Forms.Label();
            this.comModuloProvi = new System.Windows.Forms.ComboBox();
            this.comSistema = new System.Windows.Forms.ComboBox();
            this.comModulo = new System.Windows.Forms.ComboBox();
            this.lblFantasia = new System.Windows.Forms.Label();
            this.txtFantasia = new System.Windows.Forms.TextBox();
            this.lblCpfCnpj = new System.Windows.Forms.Label();
            this.mtbCpfCnpj = new System.Windows.Forms.MaskedTextBox();
            this.lblRazao = new System.Windows.Forms.Label();
            this.txtRazao = new System.Windows.Forms.TextBox();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.txtEmpresa = new System.Windows.Forms.TextBox();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panPrin.SuspendLayout();
            this.grb1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panPrin
            // 
            this.panPrin.BackColor = System.Drawing.Color.White;
            this.panPrin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPrin.Controls.Add(this.btnSair);
            this.panPrin.Controls.Add(this.grb1);
            this.panPrin.Controls.Add(this.btnSelecionar);
            this.panPrin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPrin.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panPrin.Location = new System.Drawing.Point(0, 0);
            this.panPrin.Name = "panPrin";
            this.panPrin.Size = new System.Drawing.Size(660, 243);
            this.panPrin.TabIndex = 0;
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(564, 11);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(83, 50);
            this.btnSair.TabIndex = 2;
            this.btnSair.Text = "SAIR";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolTip.SetToolTip(this.btnSair, "Sai do sistema");
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // grb1
            // 
            this.grb1.Controls.Add(this.btnInserir);
            this.grb1.Controls.Add(this.lblSistema);
            this.grb1.Controls.Add(this.lblModuloProvi);
            this.grb1.Controls.Add(this.lblModulo);
            this.grb1.Controls.Add(this.comModuloProvi);
            this.grb1.Controls.Add(this.comSistema);
            this.grb1.Controls.Add(this.comModulo);
            this.grb1.Controls.Add(this.lblFantasia);
            this.grb1.Controls.Add(this.txtFantasia);
            this.grb1.Controls.Add(this.lblCpfCnpj);
            this.grb1.Controls.Add(this.mtbCpfCnpj);
            this.grb1.Controls.Add(this.lblRazao);
            this.grb1.Controls.Add(this.txtRazao);
            this.grb1.Controls.Add(this.lblEmpresa);
            this.grb1.Controls.Add(this.txtEmpresa);
            this.grb1.Location = new System.Drawing.Point(3, 67);
            this.grb1.Name = "grb1";
            this.grb1.Size = new System.Drawing.Size(652, 172);
            this.grb1.TabIndex = 1;
            this.grb1.TabStop = false;
            // 
            // btnInserir
            // 
            this.btnInserir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInserir.Enabled = false;
            this.btnInserir.Image = ((System.Drawing.Image)(resources.GetObject("btnInserir.Image")));
            this.btnInserir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInserir.Location = new System.Drawing.Point(484, 132);
            this.btnInserir.Name = "btnInserir";
            this.btnInserir.Size = new System.Drawing.Size(160, 34);
            this.btnInserir.TabIndex = 15;
            this.btnInserir.Text = "INSERIR EMPRESA";
            this.btnInserir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolTip.SetToolTip(this.btnInserir, "Insere as informações capturadas do arquivo SecF para o banco de dados do sistema" +
        "");
            this.btnInserir.UseVisualStyleBackColor = true;
            this.btnInserir.Click += new System.EventHandler(this.btnInserir_Click);
            // 
            // lblSistema
            // 
            this.lblSistema.AutoSize = true;
            this.lblSistema.Location = new System.Drawing.Point(498, 89);
            this.lblSistema.Name = "lblSistema";
            this.lblSistema.Size = new System.Drawing.Size(56, 14);
            this.lblSistema.TabIndex = 14;
            this.lblSistema.Text = "SISTEMA";
            // 
            // lblModuloProvi
            // 
            this.lblModuloProvi.AutoSize = true;
            this.lblModuloProvi.Location = new System.Drawing.Point(235, 89);
            this.lblModuloProvi.Name = "lblModuloProvi";
            this.lblModuloProvi.Size = new System.Drawing.Size(126, 14);
            this.lblModuloProvi.TabIndex = 13;
            this.lblModuloProvi.Text = "MÓDULO PROVISÓRIO";
            // 
            // lblModulo
            // 
            this.lblModulo.AutoSize = true;
            this.lblModulo.Location = new System.Drawing.Point(76, 89);
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.Size = new System.Drawing.Size(49, 14);
            this.lblModulo.TabIndex = 12;
            this.lblModulo.Text = "MÓDULO";
            // 
            // comModuloProvi
            // 
            this.comModuloProvi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comModuloProvi.Enabled = false;
            this.comModuloProvi.FormattingEnabled = true;
            this.comModuloProvi.Items.AddRange(new object[] {
            "TechSIS FREE",
            "TechSIS EXPRESS",
            "TechSIS BUSINESS",
            "TechSIS CONTROLE",
            "TechSIS PRÓ"});
            this.comModuloProvi.Location = new System.Drawing.Point(203, 104);
            this.comModuloProvi.Name = "comModuloProvi";
            this.comModuloProvi.Size = new System.Drawing.Size(188, 22);
            this.comModuloProvi.TabIndex = 11;
            // 
            // comSistema
            // 
            this.comSistema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comSistema.Enabled = false;
            this.comSistema.FormattingEnabled = true;
            this.comSistema.Items.AddRange(new object[] {
            "TechSIS INF"});
            this.comSistema.Location = new System.Drawing.Point(398, 104);
            this.comSistema.Name = "comSistema";
            this.comSistema.Size = new System.Drawing.Size(246, 22);
            this.comSistema.TabIndex = 10;
            // 
            // comModulo
            // 
            this.comModulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comModulo.Enabled = false;
            this.comModulo.FormattingEnabled = true;
            this.comModulo.Items.AddRange(new object[] {
            "TechSIS FREE",
            "TechSIS EXPRESS",
            "TechSIS BUSINESS",
            "TechSIS CONTROLE",
            "TechSIS PRÓ"});
            this.comModulo.Location = new System.Drawing.Point(8, 104);
            this.comModulo.Name = "comModulo";
            this.comModulo.Size = new System.Drawing.Size(188, 22);
            this.comModulo.TabIndex = 8;
            // 
            // lblFantasia
            // 
            this.lblFantasia.AutoSize = true;
            this.lblFantasia.Location = new System.Drawing.Point(374, 52);
            this.lblFantasia.Name = "lblFantasia";
            this.lblFantasia.Size = new System.Drawing.Size(63, 14);
            this.lblFantasia.TabIndex = 7;
            this.lblFantasia.Text = "FANTASIA";
            // 
            // txtFantasia
            // 
            this.txtFantasia.BackColor = System.Drawing.Color.White;
            this.txtFantasia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFantasia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFantasia.Enabled = false;
            this.txtFantasia.Location = new System.Drawing.Point(155, 66);
            this.txtFantasia.Name = "txtFantasia";
            this.txtFantasia.Size = new System.Drawing.Size(489, 20);
            this.txtFantasia.TabIndex = 6;
            this.txtFantasia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCpfCnpj
            // 
            this.lblCpfCnpj.AutoSize = true;
            this.lblCpfCnpj.Location = new System.Drawing.Point(45, 52);
            this.lblCpfCnpj.Name = "lblCpfCnpj";
            this.lblCpfCnpj.Size = new System.Drawing.Size(63, 14);
            this.lblCpfCnpj.TabIndex = 5;
            this.lblCpfCnpj.Text = "CPF.CNPJ";
            // 
            // mtbCpfCnpj
            // 
            this.mtbCpfCnpj.BackColor = System.Drawing.Color.White;
            this.mtbCpfCnpj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mtbCpfCnpj.Enabled = false;
            this.mtbCpfCnpj.Location = new System.Drawing.Point(8, 66);
            this.mtbCpfCnpj.Name = "mtbCpfCnpj";
            this.mtbCpfCnpj.Size = new System.Drawing.Size(141, 20);
            this.mtbCpfCnpj.TabIndex = 4;
            this.mtbCpfCnpj.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblRazao
            // 
            this.lblRazao.AutoSize = true;
            this.lblRazao.Location = new System.Drawing.Point(310, 14);
            this.lblRazao.Name = "lblRazao";
            this.lblRazao.Size = new System.Drawing.Size(91, 14);
            this.lblRazao.TabIndex = 3;
            this.lblRazao.Text = "RAZÃO SOCIAL";
            // 
            // txtRazao
            // 
            this.txtRazao.BackColor = System.Drawing.Color.White;
            this.txtRazao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRazao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRazao.Enabled = false;
            this.txtRazao.Location = new System.Drawing.Point(88, 29);
            this.txtRazao.Name = "txtRazao";
            this.txtRazao.Size = new System.Drawing.Size(556, 20);
            this.txtRazao.TabIndex = 2;
            this.txtRazao.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.AutoSize = true;
            this.lblEmpresa.Location = new System.Drawing.Point(16, 14);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(56, 14);
            this.lblEmpresa.TabIndex = 1;
            this.lblEmpresa.Text = "EMPRESA";
            // 
            // txtEmpresa
            // 
            this.txtEmpresa.BackColor = System.Drawing.Color.White;
            this.txtEmpresa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmpresa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmpresa.Enabled = false;
            this.txtEmpresa.Location = new System.Drawing.Point(8, 29);
            this.txtEmpresa.Name = "txtEmpresa";
            this.txtEmpresa.Size = new System.Drawing.Size(74, 20);
            this.txtEmpresa.TabIndex = 0;
            this.txtEmpresa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelecionar.Image = ((System.Drawing.Image)(resources.GetObject("btnSelecionar.Image")));
            this.btnSelecionar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelecionar.Location = new System.Drawing.Point(231, 11);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(197, 50);
            this.btnSelecionar.TabIndex = 0;
            this.btnSelecionar.Text = "SELECIONAR ARQUIVO";
            this.btnSelecionar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolTip.SetToolTip(this.btnSelecionar, "Seleciona um arquivo SecF para ser adicionado ao banco de dados");
            this.btnSelecionar.UseVisualStyleBackColor = true;
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // TechSIS_AddEmpre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(660, 243);
            this.Controls.Add(this.panPrin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TechSIS_AddEmpre";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechSIS - Adicionar Empresa no Banco de Dados";
            this.Load += new System.EventHandler(this.TechSIS_AddEmpre_Load);
            this.panPrin.ResumeLayout(false);
            this.grb1.ResumeLayout(false);
            this.grb1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panPrin;
        private System.Windows.Forms.Button btnSelecionar;
        private System.Windows.Forms.GroupBox grb1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.TextBox txtEmpresa;
        private System.Windows.Forms.Label lblRazao;
        private System.Windows.Forms.TextBox txtRazao;
        private System.Windows.Forms.MaskedTextBox mtbCpfCnpj;
        private System.Windows.Forms.Label lblCpfCnpj;
        private System.Windows.Forms.Label lblFantasia;
        private System.Windows.Forms.TextBox txtFantasia;
        private System.Windows.Forms.ComboBox comSistema;
        private System.Windows.Forms.ComboBox comModulo;
        private System.Windows.Forms.Label lblSistema;
        private System.Windows.Forms.Label lblModuloProvi;
        private System.Windows.Forms.Label lblModulo;
        private System.Windows.Forms.ComboBox comModuloProvi;
        private System.Windows.Forms.Button btnInserir;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}

