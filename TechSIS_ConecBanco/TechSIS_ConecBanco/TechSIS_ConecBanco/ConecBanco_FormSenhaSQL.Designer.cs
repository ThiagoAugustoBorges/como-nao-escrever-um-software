namespace TechSIS_ConecBanco
{
    partial class ConecBanco_FormSenhaSQL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConecBanco_FormSenhaSQL));
            this.label1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.btnConfirma = new System.Windows.Forms.Button();
            this.lblLimitado = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(383, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "  TechSIS - Senha SQL  ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.BackColor = System.Drawing.Color.White;
            this.lbl2.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2.ForeColor = System.Drawing.Color.Red;
            this.lbl2.Location = new System.Drawing.Point(69, 59);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(240, 16);
            this.lbl2.TabIndex = 9;
            this.lbl2.Text = "NÃO ESQUEÇA DE ANOTAR A SENHA";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(4, 41);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(368, 16);
            this.lbl1.TabIndex = 8;
            this.lbl1.Text = "DIGITE A SENHA DESEJADA PARA O USUÁRIO MASTER";
            // 
            // txtSenha
            // 
            this.txtSenha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSenha.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenha.Location = new System.Drawing.Point(133, 95);
            this.txtSenha.MaxLength = 7;
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(115, 20);
            this.txtSenha.TabIndex = 10;
            this.txtSenha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSenha.TextChanged += new System.EventHandler(this.txtSenha_TextChanged);
            this.txtSenha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSenha_KeyPress);
            // 
            // btnConfirma
            // 
            this.btnConfirma.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirma.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirma.Image = ((System.Drawing.Image)(resources.GetObject("btnConfirma.Image")));
            this.btnConfirma.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConfirma.Location = new System.Drawing.Point(276, 125);
            this.btnConfirma.Name = "btnConfirma";
            this.btnConfirma.Size = new System.Drawing.Size(104, 34);
            this.btnConfirma.TabIndex = 11;
            this.btnConfirma.Text = "CONFIRMAR";
            this.btnConfirma.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirma.UseVisualStyleBackColor = true;
            this.btnConfirma.Click += new System.EventHandler(this.btnConfirma_Click);
            this.btnConfirma.Enter += new System.EventHandler(this.btnConfirma_Enter);
            this.btnConfirma.Leave += new System.EventHandler(this.btnConfirma_Leave);
            this.btnConfirma.MouseEnter += new System.EventHandler(this.btnConfirma_MouseEnter);
            this.btnConfirma.MouseLeave += new System.EventHandler(this.btnConfirma_MouseLeave);
            // 
            // lblLimitado
            // 
            this.lblLimitado.AutoSize = true;
            this.lblLimitado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLimitado.Location = new System.Drawing.Point(4, 144);
            this.lblLimitado.Name = "lblLimitado";
            this.lblLimitado.Size = new System.Drawing.Size(166, 15);
            this.lblLimitado.TabIndex = 12;
            this.lblLimitado.Text = "Limitado a 07 caracteres";
            // 
            // ConecBanco_FormSenhaSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(383, 162);
            this.ControlBox = false;
            this.Controls.Add(this.lblLimitado);
            this.Controls.Add(this.btnConfirma);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(383, 162);
            this.MinimumSize = new System.Drawing.Size(383, 162);
            this.Name = "ConecBanco_FormSenhaSQL";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TechSIS-  Senha SQL";
            this.Load += new System.EventHandler(this.ConecBanco_FormSenhaSQL_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Button btnConfirma;
        private System.Windows.Forms.Label lblLimitado;
    }
}