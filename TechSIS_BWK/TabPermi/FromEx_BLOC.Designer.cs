namespace TabPermi
{
    partial class FromEx_BLOC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FromEx_BLOC));
            this.PainelUp = new System.Windows.Forms.Panel();
            this.comBlocoBl1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancela = new System.Windows.Forms.Button();
            this.btnConfirma = new System.Windows.Forms.Button();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.PainelUp.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // PainelUp
            // 
            this.PainelUp.BackColor = System.Drawing.SystemColors.ControlDark;
            this.PainelUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PainelUp.Controls.Add(this.comBlocoBl1);
            this.PainelUp.Controls.Add(this.label3);
            this.PainelUp.Controls.Add(this.label2);
            this.PainelUp.Controls.Add(this.label1);
            this.PainelUp.Controls.Add(this.panel2);
            this.PainelUp.Controls.Add(this.txtMotivo);
            this.PainelUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PainelUp.Location = new System.Drawing.Point(0, 0);
            this.PainelUp.Name = "PainelUp";
            this.PainelUp.Size = new System.Drawing.Size(379, 138);
            this.PainelUp.TabIndex = 7;
            // 
            // comBlocoBl1
            // 
            this.comBlocoBl1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comBlocoBl1.FormattingEnabled = true;
            this.comBlocoBl1.Items.AddRange(new object[] {
            "BLOCO 1 = (TABELAS GERAIS + RELATÓRIOS + CADASTROS)",
            "BLOCO 2 = (BLOCO 1 + ESTOQUE + MOVIMENTAÇÕES)",
            "BLOCO 3 = (BLOCO 2 + FINANCEIRO)",
            "BLOCO 4 = (BLOCO 3 + CONTÁBIL)",
            "BLOCO 5 = (LIBERAÇÃO TOTAL - GERENCIAMENTO)",
            "BLOCO 6 = (LIBERAÇÃO TOTAL)"});
            this.comBlocoBl1.Location = new System.Drawing.Point(5, 53);
            this.comBlocoBl1.Name = "comBlocoBl1";
            this.comBlocoBl1.Size = new System.Drawing.Size(369, 22);
            this.comBlocoBl1.TabIndex = 6;
            this.comBlocoBl1.SelectedIndexChanged += new System.EventHandler(this.comBlocoBl1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Mínimo.: 15 Caracteres";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(212, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "EXCLUSÃO!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Digite o motivo da";
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnCancela);
            this.panel2.Controls.Add(this.btnConfirma);
            this.panel2.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.panel2.Location = new System.Drawing.Point(209, 105);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(168, 31);
            this.panel2.TabIndex = 2;
            // 
            // btnCancela
            // 
            this.btnCancela.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancela.Image = ((System.Drawing.Image)(resources.GetObject("btnCancela.Image")));
            this.btnCancela.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancela.Location = new System.Drawing.Point(1, 1);
            this.btnCancela.Name = "btnCancela";
            this.btnCancela.Size = new System.Drawing.Size(81, 25);
            this.btnCancela.TabIndex = 2;
            this.btnCancela.Text = "&CANCELA";
            this.btnCancela.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancela.UseVisualStyleBackColor = true;
            this.btnCancela.Click += new System.EventHandler(this.btnCancela_Click);
            // 
            // btnConfirma
            // 
            this.btnConfirma.Image = ((System.Drawing.Image)(resources.GetObject("btnConfirma.Image")));
            this.btnConfirma.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirma.Location = new System.Drawing.Point(81, 1);
            this.btnConfirma.Name = "btnConfirma";
            this.btnConfirma.Size = new System.Drawing.Size(81, 25);
            this.btnConfirma.TabIndex = 1;
            this.btnConfirma.Text = "&OK - F1";
            this.btnConfirma.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConfirma.UseVisualStyleBackColor = true;
            this.btnConfirma.Click += new System.EventHandler(this.btnConfirma_Click);
            // 
            // txtMotivo
            // 
            this.txtMotivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMotivo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMotivo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMotivo.Location = new System.Drawing.Point(5, 81);
            this.txtMotivo.MaxLength = 50;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(369, 20);
            this.txtMotivo.TabIndex = 0;
            // 
            // FromEx_BLOC
            // 
            this.AcceptButton = this.btnConfirma;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancela;
            this.ClientSize = new System.Drawing.Size(379, 138);
            this.Controls.Add(this.PainelUp);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(395, 176);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(395, 176);
            this.Name = "FromEx_BLOC";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechSIS - MOTIVO PARA EXCLUSÃO";
            this.Load += new System.EventHandler(this.FromEx_BLOC_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FromEx_BLOC_KeyDown);
            this.PainelUp.ResumeLayout(false);
            this.PainelUp.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PainelUp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancela;
        private System.Windows.Forms.Button btnConfirma;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.ComboBox comBlocoBl1;
    }
}