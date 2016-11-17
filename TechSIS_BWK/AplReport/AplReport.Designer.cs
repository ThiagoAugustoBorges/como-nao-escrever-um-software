namespace AplReport
{
    partial class AplReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AplReport));
            this.Painel_First = new System.Windows.Forms.Panel();
            this.Painel_Up = new System.Windows.Forms.Panel();
            this.grbDados = new System.Windows.Forms.GroupBox();
            this.rtbTexto = new System.Windows.Forms.RichTextBox();
            this.lblVariavel = new System.Windows.Forms.Label();
            this.txtCidade = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblCidade = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.grbTipo = new System.Windows.Forms.GroupBox();
            this.rabAdicionar = new System.Windows.Forms.RadioButton();
            this.rabSugestao = new System.Windows.Forms.RadioButton();
            this.rabErro = new System.Windows.Forms.RadioButton();
            this.Painel_Down = new System.Windows.Forms.Panel();
            this.txtData = new System.Windows.Forms.TextBox();
            this.cheLembrar = new System.Windows.Forms.CheckBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.Painel_First.SuspendLayout();
            this.Painel_Up.SuspendLayout();
            this.grbDados.SuspendLayout();
            this.grbTipo.SuspendLayout();
            this.Painel_Down.SuspendLayout();
            this.SuspendLayout();
            // 
            // Painel_First
            // 
            this.Painel_First.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Painel_First.Controls.Add(this.Painel_Up);
            this.Painel_First.Controls.Add(this.Painel_Down);
            this.Painel_First.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Painel_First.Location = new System.Drawing.Point(0, 0);
            this.Painel_First.Name = "Painel_First";
            this.Painel_First.Size = new System.Drawing.Size(619, 388);
            this.Painel_First.TabIndex = 0;
            // 
            // Painel_Up
            // 
            this.Painel_Up.BackColor = System.Drawing.Color.MistyRose;
            this.Painel_Up.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Painel_Up.Controls.Add(this.grbDados);
            this.Painel_Up.Controls.Add(this.grbTipo);
            this.Painel_Up.Dock = System.Windows.Forms.DockStyle.Top;
            this.Painel_Up.Location = new System.Drawing.Point(0, 0);
            this.Painel_Up.Name = "Painel_Up";
            this.Painel_Up.Size = new System.Drawing.Size(617, 340);
            this.Painel_Up.TabIndex = 2;
            // 
            // grbDados
            // 
            this.grbDados.Controls.Add(this.rtbTexto);
            this.grbDados.Controls.Add(this.lblVariavel);
            this.grbDados.Controls.Add(this.txtCidade);
            this.grbDados.Controls.Add(this.txtEmail);
            this.grbDados.Controls.Add(this.txtNome);
            this.grbDados.Controls.Add(this.lblCidade);
            this.grbDados.Controls.Add(this.lblEmail);
            this.grbDados.Controls.Add(this.lblNome);
            this.grbDados.Enabled = false;
            this.grbDados.Location = new System.Drawing.Point(3, 56);
            this.grbDados.Name = "grbDados";
            this.grbDados.Size = new System.Drawing.Size(609, 277);
            this.grbDados.TabIndex = 1;
            this.grbDados.TabStop = false;
            this.grbDados.Text = "SEUS DADOS";
            // 
            // rtbTexto
            // 
            this.rtbTexto.Location = new System.Drawing.Point(7, 124);
            this.rtbTexto.Name = "rtbTexto";
            this.rtbTexto.Size = new System.Drawing.Size(595, 147);
            this.rtbTexto.TabIndex = 7;
            this.rtbTexto.Text = "";
            // 
            // lblVariavel
            // 
            this.lblVariavel.AutoSize = true;
            this.lblVariavel.Location = new System.Drawing.Point(7, 107);
            this.lblVariavel.Name = "lblVariavel";
            this.lblVariavel.Size = new System.Drawing.Size(14, 14);
            this.lblVariavel.TabIndex = 6;
            this.lblVariavel.Text = " ";
            // 
            // txtCidade
            // 
            this.txtCidade.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCidade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCidade.Location = new System.Drawing.Point(111, 71);
            this.txtCidade.MaxLength = 65;
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.Size = new System.Drawing.Size(491, 20);
            this.txtCidade.TabIndex = 5;
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtEmail.ForeColor = System.Drawing.Color.Blue;
            this.txtEmail.Location = new System.Drawing.Point(111, 47);
            this.txtEmail.MaxLength = 65;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(491, 20);
            this.txtEmail.TabIndex = 4;
            // 
            // txtNome
            // 
            this.txtNome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNome.Location = new System.Drawing.Point(111, 23);
            this.txtNome.MaxLength = 65;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(491, 20);
            this.txtNome.TabIndex = 3;
            // 
            // lblCidade
            // 
            this.lblCidade.AutoSize = true;
            this.lblCidade.Location = new System.Drawing.Point(7, 75);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Size = new System.Drawing.Size(98, 14);
            this.lblCidade.TabIndex = 2;
            this.lblCidade.Text = "SUA CIDADE..:";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(7, 51);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(98, 14);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "SEU EMAIL...:";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(7, 28);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(98, 14);
            this.lblNome.TabIndex = 0;
            this.lblNome.Text = "SEU NOME....:";
            // 
            // grbTipo
            // 
            this.grbTipo.Controls.Add(this.rabAdicionar);
            this.grbTipo.Controls.Add(this.rabSugestao);
            this.grbTipo.Controls.Add(this.rabErro);
            this.grbTipo.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbTipo.Location = new System.Drawing.Point(3, 3);
            this.grbTipo.Name = "grbTipo";
            this.grbTipo.Size = new System.Drawing.Size(609, 52);
            this.grbTipo.TabIndex = 0;
            this.grbTipo.TabStop = false;
            this.grbTipo.Text = "INFORME O TIPO DE REPORT";
            // 
            // rabAdicionar
            // 
            this.rabAdicionar.AutoSize = true;
            this.rabAdicionar.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rabAdicionar.ForeColor = System.Drawing.Color.Black;
            this.rabAdicionar.Location = new System.Drawing.Point(344, 25);
            this.rabAdicionar.Name = "rabAdicionar";
            this.rabAdicionar.Size = new System.Drawing.Size(263, 19);
            this.rabAdicionar.TabIndex = 2;
            this.rabAdicionar.Text = "PRECISO QUE ADICIONE UM NOVO CAMPO";
            this.rabAdicionar.UseVisualStyleBackColor = true;
            this.rabAdicionar.CheckedChanged += new System.EventHandler(this.rabAdicionar_CheckedChanged);
            // 
            // rabSugestao
            // 
            this.rabSugestao.AutoSize = true;
            this.rabSugestao.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rabSugestao.ForeColor = System.Drawing.Color.Black;
            this.rabSugestao.Location = new System.Drawing.Point(159, 25);
            this.rabSugestao.Name = "rabSugestao";
            this.rabSugestao.Size = new System.Drawing.Size(179, 19);
            this.rabSugestao.TabIndex = 1;
            this.rabSugestao.Text = "QUERO DAR UMA SUGESTÃO";
            this.rabSugestao.UseVisualStyleBackColor = true;
            this.rabSugestao.CheckedChanged += new System.EventHandler(this.rabSugestao_CheckedChanged);
            // 
            // rabErro
            // 
            this.rabErro.AutoSize = true;
            this.rabErro.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rabErro.ForeColor = System.Drawing.Color.Black;
            this.rabErro.Location = new System.Drawing.Point(7, 25);
            this.rabErro.Name = "rabErro";
            this.rabErro.Size = new System.Drawing.Size(144, 19);
            this.rabErro.TabIndex = 0;
            this.rabErro.Text = "ENCONTREI UM ERRO";
            this.rabErro.UseVisualStyleBackColor = true;
            this.rabErro.CheckedChanged += new System.EventHandler(this.rabErro_CheckedChanged);
            // 
            // Painel_Down
            // 
            this.Painel_Down.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Painel_Down.Controls.Add(this.txtData);
            this.Painel_Down.Controls.Add(this.cheLembrar);
            this.Painel_Down.Controls.Add(this.btnEnviar);
            this.Painel_Down.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Painel_Down.Location = new System.Drawing.Point(0, 339);
            this.Painel_Down.MaximumSize = new System.Drawing.Size(617, 47);
            this.Painel_Down.MinimumSize = new System.Drawing.Size(617, 47);
            this.Painel_Down.Name = "Painel_Down";
            this.Painel_Down.Size = new System.Drawing.Size(617, 47);
            this.Painel_Down.TabIndex = 1;
            // 
            // txtData
            // 
            this.txtData.BackColor = System.Drawing.Color.White;
            this.txtData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtData.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtData.Enabled = false;
            this.txtData.Location = new System.Drawing.Point(336, 14);
            this.txtData.MaxLength = 100;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(98, 20);
            this.txtData.TabIndex = 8;
            this.txtData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cheLembrar
            // 
            this.cheLembrar.AutoSize = true;
            this.cheLembrar.Enabled = false;
            this.cheLembrar.Location = new System.Drawing.Point(10, 17);
            this.cheLembrar.Name = "cheLembrar";
            this.cheLembrar.Size = new System.Drawing.Size(306, 18);
            this.cheLembrar.TabIndex = 1;
            this.cheLembrar.Text = "LEMBRAR MEUS DADOS PARA UMA PRÓXIMA VEZ.";
            this.cheLembrar.UseVisualStyleBackColor = true;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Enabled = false;
            this.btnEnviar.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviar.Image = ((System.Drawing.Image)(resources.GetObject("btnEnviar.Image")));
            this.btnEnviar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnviar.Location = new System.Drawing.Point(453, 5);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(157, 36);
            this.btnEnviar.TabIndex = 0;
            this.btnEnviar.Text = "ENVIAR REPORT!";
            this.btnEnviar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // AplReport
            // 
            this.AcceptButton = this.btnEnviar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 388);
            this.Controls.Add(this.Painel_First);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(635, 427);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(635, 427);
            this.Name = "AplReport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechSIS - FORMULÁRIO DE REPORTs ";
            this.Load += new System.EventHandler(this.AplReport_Load);
            this.Painel_First.ResumeLayout(false);
            this.Painel_Up.ResumeLayout(false);
            this.grbDados.ResumeLayout(false);
            this.grbDados.PerformLayout();
            this.grbTipo.ResumeLayout(false);
            this.grbTipo.PerformLayout();
            this.Painel_Down.ResumeLayout(false);
            this.Painel_Down.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Painel_First;
        private System.Windows.Forms.Panel Painel_Up;
        private System.Windows.Forms.Panel Painel_Down;
        private System.Windows.Forms.GroupBox grbDados;
        private System.Windows.Forms.RichTextBox rtbTexto;
        private System.Windows.Forms.Label lblVariavel;
        private System.Windows.Forms.TextBox txtCidade;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblCidade;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.GroupBox grbTipo;
        private System.Windows.Forms.RadioButton rabAdicionar;
        private System.Windows.Forms.RadioButton rabSugestao;
        private System.Windows.Forms.RadioButton rabErro;
        private System.Windows.Forms.CheckBox cheLembrar;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.TextBox txtData;
    }
}