namespace TechSIS_SecFGenerator
{
    partial class TechSIS_SecFGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TechSIS_SecFGenerator));
            this.panPrin = new System.Windows.Forms.Panel();
            this.grb4 = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnGerarArquivo = new System.Windows.Forms.Button();
            this.grb3 = new System.Windows.Forms.GroupBox();
            this.picCadeado = new System.Windows.Forms.PictureBox();
            this.btnGerarChave = new System.Windows.Forms.Button();
            this.txtChave = new System.Windows.Forms.TextBox();
            this.grb2 = new System.Windows.Forms.GroupBox();
            this.lblLicencaParaSistema = new System.Windows.Forms.Label();
            this.comLicSistema = new System.Windows.Forms.ComboBox();
            this.lblLicencaProvisoria = new System.Windows.Forms.Label();
            this.lblLicencaOriginal = new System.Windows.Forms.Label();
            this.comLicProvisoria = new System.Windows.Forms.ComboBox();
            this.comLicOriginal = new System.Windows.Forms.ComboBox();
            this.grb1 = new System.Windows.Forms.GroupBox();
            this.lblFantasia = new System.Windows.Forms.Label();
            this.txtFantasia = new System.Windows.Forms.TextBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.lblCpfCnpj = new System.Windows.Forms.Label();
            this.mtbCpfCnpj = new System.Windows.Forms.MaskedTextBox();
            this.comTipo = new System.Windows.Forms.ComboBox();
            this.lblRazao = new System.Windows.Forms.Label();
            this.lblArquivo = new System.Windows.Forms.Label();
            this.txtRazao = new System.Windows.Forms.TextBox();
            this.nupArquivo = new System.Windows.Forms.NumericUpDown();
            this.panPrin.SuspendLayout();
            this.grb4.SuspendLayout();
            this.grb3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCadeado)).BeginInit();
            this.grb2.SuspendLayout();
            this.grb1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupArquivo)).BeginInit();
            this.SuspendLayout();
            // 
            // panPrin
            // 
            this.panPrin.BackColor = System.Drawing.Color.White;
            this.panPrin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPrin.Controls.Add(this.grb4);
            this.panPrin.Controls.Add(this.grb3);
            this.panPrin.Controls.Add(this.grb2);
            this.panPrin.Controls.Add(this.grb1);
            this.panPrin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPrin.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panPrin.Location = new System.Drawing.Point(0, 0);
            this.panPrin.Name = "panPrin";
            this.panPrin.Size = new System.Drawing.Size(690, 308);
            this.panPrin.TabIndex = 5;
            // 
            // grb4
            // 
            this.grb4.Controls.Add(this.btnCancelar);
            this.grb4.Controls.Add(this.btnSair);
            this.grb4.Controls.Add(this.btnGerarArquivo);
            this.grb4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grb4.Location = new System.Drawing.Point(0, 252);
            this.grb4.Name = "grb4";
            this.grb4.Size = new System.Drawing.Size(688, 54);
            this.grb4.TabIndex = 4;
            this.grb4.TabStop = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(495, 11);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(117, 40);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(611, 11);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(72, 40);
            this.btnSair.TabIndex = 3;
            this.btnSair.Text = "SAIR";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnGerarArquivo
            // 
            this.btnGerarArquivo.Enabled = false;
            this.btnGerarArquivo.Image = ((System.Drawing.Image)(resources.GetObject("btnGerarArquivo.Image")));
            this.btnGerarArquivo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGerarArquivo.Location = new System.Drawing.Point(367, 11);
            this.btnGerarArquivo.Name = "btnGerarArquivo";
            this.btnGerarArquivo.Size = new System.Drawing.Size(129, 40);
            this.btnGerarArquivo.TabIndex = 1;
            this.btnGerarArquivo.Text = "GERAR ARQUIVO";
            this.btnGerarArquivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGerarArquivo.UseVisualStyleBackColor = true;
            this.btnGerarArquivo.Click += new System.EventHandler(this.btnGerarArquivo_Click);
            // 
            // grb3
            // 
            this.grb3.Controls.Add(this.picCadeado);
            this.grb3.Controls.Add(this.btnGerarChave);
            this.grb3.Controls.Add(this.txtChave);
            this.grb3.Location = new System.Drawing.Point(3, 175);
            this.grb3.Name = "grb3";
            this.grb3.Size = new System.Drawing.Size(682, 77);
            this.grb3.TabIndex = 3;
            this.grb3.TabStop = false;
            this.grb3.Text = "CHAVE DO SISTEMA";
            // 
            // picCadeado
            // 
            this.picCadeado.Image = ((System.Drawing.Image)(resources.GetObject("picCadeado.Image")));
            this.picCadeado.Location = new System.Drawing.Point(600, 13);
            this.picCadeado.Name = "picCadeado";
            this.picCadeado.Size = new System.Drawing.Size(76, 58);
            this.picCadeado.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCadeado.TabIndex = 14;
            this.picCadeado.TabStop = false;
            // 
            // btnGerarChave
            // 
            this.btnGerarChave.Location = new System.Drawing.Point(280, 16);
            this.btnGerarChave.Name = "btnGerarChave";
            this.btnGerarChave.Size = new System.Drawing.Size(137, 32);
            this.btnGerarChave.TabIndex = 1;
            this.btnGerarChave.Text = "GERAR CHAVE";
            this.btnGerarChave.UseVisualStyleBackColor = true;
            this.btnGerarChave.Click += new System.EventHandler(this.btnGerarChave_Click);
            // 
            // txtChave
            // 
            this.txtChave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChave.Enabled = false;
            this.txtChave.Location = new System.Drawing.Point(129, 51);
            this.txtChave.Name = "txtChave";
            this.txtChave.Size = new System.Drawing.Size(443, 20);
            this.txtChave.TabIndex = 12;
            this.txtChave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtChave.UseSystemPasswordChar = true;
            // 
            // grb2
            // 
            this.grb2.Controls.Add(this.lblLicencaParaSistema);
            this.grb2.Controls.Add(this.comLicSistema);
            this.grb2.Controls.Add(this.lblLicencaProvisoria);
            this.grb2.Controls.Add(this.lblLicencaOriginal);
            this.grb2.Controls.Add(this.comLicProvisoria);
            this.grb2.Controls.Add(this.comLicOriginal);
            this.grb2.Location = new System.Drawing.Point(3, 110);
            this.grb2.Name = "grb2";
            this.grb2.Size = new System.Drawing.Size(682, 65);
            this.grb2.TabIndex = 2;
            this.grb2.TabStop = false;
            this.grb2.Text = "QUANTO A LICENÇA";
            // 
            // lblLicencaParaSistema
            // 
            this.lblLicencaParaSistema.AutoSize = true;
            this.lblLicencaParaSistema.Location = new System.Drawing.Point(450, 23);
            this.lblLicencaParaSistema.Name = "lblLicencaParaSistema";
            this.lblLicencaParaSistema.Size = new System.Drawing.Size(161, 14);
            this.lblLicencaParaSistema.TabIndex = 9;
            this.lblLicencaParaSistema.Text = "LICENÇA PARA O SISTEMA";
            // 
            // comLicSistema
            // 
            this.comLicSistema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comLicSistema.FormattingEnabled = true;
            this.comLicSistema.Items.AddRange(new object[] {
            "TechSIS INF"});
            this.comLicSistema.Location = new System.Drawing.Point(452, 37);
            this.comLicSistema.Name = "comLicSistema";
            this.comLicSistema.Size = new System.Drawing.Size(222, 22);
            this.comLicSistema.TabIndex = 3;
            // 
            // lblLicencaProvisoria
            // 
            this.lblLicencaProvisoria.AutoSize = true;
            this.lblLicencaProvisoria.Location = new System.Drawing.Point(230, 23);
            this.lblLicencaProvisoria.Name = "lblLicencaProvisoria";
            this.lblLicencaProvisoria.Size = new System.Drawing.Size(133, 14);
            this.lblLicencaProvisoria.TabIndex = 7;
            this.lblLicencaProvisoria.Text = "LICENÇA PROVISÓRIA";
            // 
            // lblLicencaOriginal
            // 
            this.lblLicencaOriginal.AutoSize = true;
            this.lblLicencaOriginal.Location = new System.Drawing.Point(9, 23);
            this.lblLicencaOriginal.Name = "lblLicencaOriginal";
            this.lblLicencaOriginal.Size = new System.Drawing.Size(119, 14);
            this.lblLicencaOriginal.TabIndex = 6;
            this.lblLicencaOriginal.Text = "LICENÇA ORIGINAL";
            // 
            // comLicProvisoria
            // 
            this.comLicProvisoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comLicProvisoria.FormattingEnabled = true;
            this.comLicProvisoria.Items.AddRange(new object[] {
            "TechSIS FREE",
            "TechSIS EXPRESS",
            "TechSIS BUSINESS",
            "TechSIS CONTROLE",
            "TechSIS PRÓ"});
            this.comLicProvisoria.Location = new System.Drawing.Point(232, 37);
            this.comLicProvisoria.Name = "comLicProvisoria";
            this.comLicProvisoria.Size = new System.Drawing.Size(214, 22);
            this.comLicProvisoria.TabIndex = 2;
            // 
            // comLicOriginal
            // 
            this.comLicOriginal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comLicOriginal.FormattingEnabled = true;
            this.comLicOriginal.Items.AddRange(new object[] {
            "TechSIS FREE",
            "TechSIS EXPRESS",
            "TechSIS BUSINESS",
            "TechSIS CONTROLE",
            "TechSIS PRÓ"});
            this.comLicOriginal.Location = new System.Drawing.Point(12, 37);
            this.comLicOriginal.Name = "comLicOriginal";
            this.comLicOriginal.Size = new System.Drawing.Size(214, 22);
            this.comLicOriginal.TabIndex = 1;
            this.comLicOriginal.SelectedIndexChanged += new System.EventHandler(this.comLicOriginal_SelectedIndexChanged);
            // 
            // grb1
            // 
            this.grb1.Controls.Add(this.lblFantasia);
            this.grb1.Controls.Add(this.txtFantasia);
            this.grb1.Controls.Add(this.lblTipo);
            this.grb1.Controls.Add(this.lblCpfCnpj);
            this.grb1.Controls.Add(this.mtbCpfCnpj);
            this.grb1.Controls.Add(this.comTipo);
            this.grb1.Controls.Add(this.lblRazao);
            this.grb1.Controls.Add(this.lblArquivo);
            this.grb1.Controls.Add(this.txtRazao);
            this.grb1.Controls.Add(this.nupArquivo);
            this.grb1.Location = new System.Drawing.Point(3, 3);
            this.grb1.Name = "grb1";
            this.grb1.Size = new System.Drawing.Size(682, 105);
            this.grb1.TabIndex = 1;
            this.grb1.TabStop = false;
            this.grb1.Text = "INFORMAÇÕES DA EMPRESA";
            // 
            // lblFantasia
            // 
            this.lblFantasia.AutoSize = true;
            this.lblFantasia.Location = new System.Drawing.Point(443, 56);
            this.lblFantasia.Name = "lblFantasia";
            this.lblFantasia.Size = new System.Drawing.Size(63, 14);
            this.lblFantasia.TabIndex = 11;
            this.lblFantasia.Text = "FATANSIA";
            // 
            // txtFantasia
            // 
            this.txtFantasia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFantasia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFantasia.Location = new System.Drawing.Point(261, 71);
            this.txtFantasia.Name = "txtFantasia";
            this.txtFantasia.Size = new System.Drawing.Size(413, 20);
            this.txtFantasia.TabIndex = 4;
            this.txtFantasia.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtFantasia_MouseDown);
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(15, 56);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(35, 14);
            this.lblTipo.TabIndex = 9;
            this.lblTipo.Text = "TIPO";
            // 
            // lblCpfCnpj
            // 
            this.lblCpfCnpj.AutoSize = true;
            this.lblCpfCnpj.Location = new System.Drawing.Point(126, 56);
            this.lblCpfCnpj.Name = "lblCpfCnpj";
            this.lblCpfCnpj.Size = new System.Drawing.Size(63, 14);
            this.lblCpfCnpj.TabIndex = 8;
            this.lblCpfCnpj.Text = "CPF.CNPJ";
            // 
            // mtbCpfCnpj
            // 
            this.mtbCpfCnpj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mtbCpfCnpj.Location = new System.Drawing.Point(62, 71);
            this.mtbCpfCnpj.Name = "mtbCpfCnpj";
            this.mtbCpfCnpj.Size = new System.Drawing.Size(193, 20);
            this.mtbCpfCnpj.TabIndex = 3;
            this.mtbCpfCnpj.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtbCpfCnpj.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.mtbCpfCnpj.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mtbCpfCnpj_MouseDown);
            // 
            // comTipo
            // 
            this.comTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comTipo.FormattingEnabled = true;
            this.comTipo.Items.AddRange(new object[] {
            "PF",
            "PJ"});
            this.comTipo.Location = new System.Drawing.Point(9, 70);
            this.comTipo.Name = "comTipo";
            this.comTipo.Size = new System.Drawing.Size(47, 22);
            this.comTipo.TabIndex = 2;
            this.comTipo.SelectedIndexChanged += new System.EventHandler(this.comTipo_SelectedIndexChanged);
            // 
            // lblRazao
            // 
            this.lblRazao.AutoSize = true;
            this.lblRazao.Location = new System.Drawing.Point(326, 19);
            this.lblRazao.Name = "lblRazao";
            this.lblRazao.Size = new System.Drawing.Size(91, 14);
            this.lblRazao.TabIndex = 3;
            this.lblRazao.Text = "RAZÃO SOCIAL";
            // 
            // lblArquivo
            // 
            this.lblArquivo.AutoSize = true;
            this.lblArquivo.Location = new System.Drawing.Point(9, 19);
            this.lblArquivo.Name = "lblArquivo";
            this.lblArquivo.Size = new System.Drawing.Size(49, 14);
            this.lblArquivo.TabIndex = 2;
            this.lblArquivo.Text = "NÚMERO";
            // 
            // txtRazao
            // 
            this.txtRazao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRazao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRazao.Location = new System.Drawing.Point(62, 33);
            this.txtRazao.Name = "txtRazao";
            this.txtRazao.Size = new System.Drawing.Size(612, 20);
            this.txtRazao.TabIndex = 1;
            this.txtRazao.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtRazao_MouseDown);
            // 
            // nupArquivo
            // 
            this.nupArquivo.Location = new System.Drawing.Point(9, 33);
            this.nupArquivo.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nupArquivo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupArquivo.Name = "nupArquivo";
            this.nupArquivo.ReadOnly = true;
            this.nupArquivo.Size = new System.Drawing.Size(47, 20);
            this.nupArquivo.TabIndex = 0;
            this.nupArquivo.TabStop = false;
            this.nupArquivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nupArquivo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupArquivo.ValueChanged += new System.EventHandler(this.nupArquivo_ValueChanged);
            // 
            // TechSIS_SecFGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 308);
            this.Controls.Add(this.panPrin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TechSIS_SecFGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechSIS - Gerador de Arquivo SecF";
            this.Load += new System.EventHandler(this.TechSIS_SecFGenerator_Load);
            this.panPrin.ResumeLayout(false);
            this.grb4.ResumeLayout(false);
            this.grb3.ResumeLayout(false);
            this.grb3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCadeado)).EndInit();
            this.grb2.ResumeLayout(false);
            this.grb2.PerformLayout();
            this.grb1.ResumeLayout(false);
            this.grb1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupArquivo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panPrin;
        private System.Windows.Forms.Button btnGerarArquivo;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.GroupBox grb1;
        private System.Windows.Forms.ComboBox comLicOriginal;
        private System.Windows.Forms.NumericUpDown nupArquivo;
        private System.Windows.Forms.Label lblRazao;
        private System.Windows.Forms.Label lblArquivo;
        private System.Windows.Forms.TextBox txtRazao;
        private System.Windows.Forms.MaskedTextBox mtbCpfCnpj;
        private System.Windows.Forms.ComboBox comTipo;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Label lblCpfCnpj;
        private System.Windows.Forms.Label lblFantasia;
        private System.Windows.Forms.TextBox txtFantasia;
        private System.Windows.Forms.GroupBox grb2;
        private System.Windows.Forms.Label lblLicencaProvisoria;
        private System.Windows.Forms.Label lblLicencaOriginal;
        private System.Windows.Forms.ComboBox comLicProvisoria;
        private System.Windows.Forms.GroupBox grb3;
        private System.Windows.Forms.Button btnGerarChave;
        private System.Windows.Forms.TextBox txtChave;
        private System.Windows.Forms.Label lblLicencaParaSistema;
        private System.Windows.Forms.ComboBox comLicSistema;
        private System.Windows.Forms.PictureBox picCadeado;
        private System.Windows.Forms.GroupBox grb4;
    }
}

