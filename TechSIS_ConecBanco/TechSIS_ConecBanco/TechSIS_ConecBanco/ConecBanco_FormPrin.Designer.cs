namespace TechSIS_ConecBanco
{
    partial class ConecBanco_FormPrin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConecBanco_FormPrin));
            this.btnNova = new System.Windows.Forms.Button();
            this.btnRest = new System.Windows.Forms.Button();
            this.panDown = new System.Windows.Forms.Panel();
            this.btnAvancar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAjuda = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.rabServidor = new System.Windows.Forms.RadioButton();
            this.rabEstacao = new System.Windows.Forms.RadioButton();
            this.panDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNova
            // 
            this.btnNova.BackColor = System.Drawing.SystemColors.Control;
            this.btnNova.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNova.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNova.Location = new System.Drawing.Point(113, 191);
            this.btnNova.Name = "btnNova";
            this.btnNova.Size = new System.Drawing.Size(361, 67);
            this.btnNova.TabIndex = 4;
            this.btnNova.Text = "NOVA INSTALAÇÃO";
            this.btnNova.UseVisualStyleBackColor = false;
            this.btnNova.Click += new System.EventHandler(this.btnNova_Click);
            this.btnNova.MouseEnter += new System.EventHandler(this.btnNova_MouseEnter);
            this.btnNova.MouseLeave += new System.EventHandler(this.btnNova_MouseLeave);
            // 
            // btnRest
            // 
            this.btnRest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRest.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRest.Location = new System.Drawing.Point(113, 281);
            this.btnRest.Name = "btnRest";
            this.btnRest.Size = new System.Drawing.Size(361, 67);
            this.btnRest.TabIndex = 5;
            this.btnRest.Text = "RESTAURAR BACKUP";
            this.btnRest.UseVisualStyleBackColor = true;
            this.btnRest.MouseEnter += new System.EventHandler(this.btnRest_MouseEnter);
            this.btnRest.MouseLeave += new System.EventHandler(this.btnRest_MouseLeave);
            // 
            // panDown
            // 
            this.panDown.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panDown.Controls.Add(this.btnAvancar);
            this.panDown.Controls.Add(this.btnCancelar);
            this.panDown.Controls.Add(this.btnAjuda);
            this.panDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panDown.Location = new System.Drawing.Point(4, 386);
            this.panDown.Name = "panDown";
            this.panDown.Size = new System.Drawing.Size(566, 43);
            this.panDown.TabIndex = 0;
            // 
            // btnAvancar
            // 
            this.btnAvancar.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAvancar.Image = ((System.Drawing.Image)(resources.GetObject("btnAvancar.Image")));
            this.btnAvancar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAvancar.Location = new System.Drawing.Point(411, 3);
            this.btnAvancar.Name = "btnAvancar";
            this.btnAvancar.Size = new System.Drawing.Size(144, 34);
            this.btnAvancar.TabIndex = 2;
            this.btnAvancar.Text = "AVANÇAR";
            this.btnAvancar.UseVisualStyleBackColor = true;
            this.btnAvancar.Click += new System.EventHandler(this.btnAvancar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.Location = new System.Drawing.Point(294, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(117, 34);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAjuda
            // 
            this.btnAjuda.Image = ((System.Drawing.Image)(resources.GetObject("btnAjuda.Image")));
            this.btnAjuda.Location = new System.Drawing.Point(2, 1);
            this.btnAjuda.Name = "btnAjuda";
            this.btnAjuda.Size = new System.Drawing.Size(47, 38);
            this.btnAjuda.TabIndex = 3;
            this.btnAjuda.TabStop = false;
            this.btnAjuda.UseVisualStyleBackColor = true;
            // 
            // picLogo
            // 
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(4, 3);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(566, 150);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // rabServidor
            // 
            this.rabServidor.AutoSize = true;
            this.rabServidor.Location = new System.Drawing.Point(4, 207);
            this.rabServidor.Name = "rabServidor";
            this.rabServidor.Size = new System.Drawing.Size(109, 18);
            this.rabServidor.TabIndex = 13;
            this.rabServidor.TabStop = true;
            this.rabServidor.Text = "radioButton1";
            this.rabServidor.UseVisualStyleBackColor = true;
            this.rabServidor.Visible = false;
            // 
            // rabEstacao
            // 
            this.rabEstacao.AutoSize = true;
            this.rabEstacao.Location = new System.Drawing.Point(4, 231);
            this.rabEstacao.Name = "rabEstacao";
            this.rabEstacao.Size = new System.Drawing.Size(109, 18);
            this.rabEstacao.TabIndex = 14;
            this.rabEstacao.TabStop = true;
            this.rabEstacao.Text = "radioButton2";
            this.rabEstacao.UseVisualStyleBackColor = true;
            this.rabEstacao.Visible = false;
            // 
            // ConecBanco_FormPrin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(573, 431);
            this.ControlBox = false;
            this.Controls.Add(this.rabEstacao);
            this.Controls.Add(this.rabServidor);
            this.Controls.Add(this.panDown);
            this.Controls.Add(this.btnRest);
            this.Controls.Add(this.btnNova);
            this.Controls.Add(this.picLogo);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(579, 460);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(579, 460);
            this.Name = "ConecBanco_FormPrin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechSIS INF - INSTALAÇÃO E CONFIGURAÇÃO                 Versão 2.0.54";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConecBanco_FormPrin_FormClosing);
            this.Load += new System.EventHandler(this.ConecBanco_FormPrin_Load);
            this.panDown.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Button btnAjuda;
        private System.Windows.Forms.Button btnNova;
        private System.Windows.Forms.Button btnRest;
        private System.Windows.Forms.Panel panDown;
        private System.Windows.Forms.Button btnAvancar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.RadioButton rabServidor;
        private System.Windows.Forms.RadioButton rabEstacao;

    }
}