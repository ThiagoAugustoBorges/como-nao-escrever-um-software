namespace TechSIS_BWK
{
    partial class ImagemFundo_REL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImagemFundo_REL));
            this.panPrin = new System.Windows.Forms.Panel();
            this.panUp = new System.Windows.Forms.Panel();
            this.lblTechSIS = new System.Windows.Forms.Label();
            this.lblProcurar = new System.Windows.Forms.Label();
            this.btnProcurarImagem = new System.Windows.Forms.Button();
            this.lblNova = new System.Windows.Forms.Label();
            this.lblAnterior = new System.Windows.Forms.Label();
            this.picNova = new System.Windows.Forms.PictureBox();
            this.picAnt = new System.Windows.Forms.PictureBox();
            this.panDown = new System.Windows.Forms.Panel();
            this.lblExtencao = new System.Windows.Forms.Label();
            this.picBrasil = new System.Windows.Forms.PictureBox();
            this.btnCancela = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.OpenFILE = new System.Windows.Forms.OpenFileDialog();
            this.panPrin.SuspendLayout();
            this.panUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNova)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAnt)).BeginInit();
            this.panDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBrasil)).BeginInit();
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
            this.panPrin.Size = new System.Drawing.Size(702, 220);
            this.panPrin.TabIndex = 0;
            // 
            // panUp
            // 
            this.panUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panUp.Controls.Add(this.lblTechSIS);
            this.panUp.Controls.Add(this.lblProcurar);
            this.panUp.Controls.Add(this.btnProcurarImagem);
            this.panUp.Controls.Add(this.lblNova);
            this.panUp.Controls.Add(this.lblAnterior);
            this.panUp.Controls.Add(this.picNova);
            this.panUp.Controls.Add(this.picAnt);
            this.panUp.Dock = System.Windows.Forms.DockStyle.Top;
            this.panUp.Location = new System.Drawing.Point(0, 0);
            this.panUp.Name = "panUp";
            this.panUp.Size = new System.Drawing.Size(700, 173);
            this.panUp.TabIndex = 1;
            // 
            // lblTechSIS
            // 
            this.lblTechSIS.AutoSize = true;
            this.lblTechSIS.Font = new System.Drawing.Font("Monotype Corsiva", 36F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTechSIS.Location = new System.Drawing.Point(382, 17);
            this.lblTechSIS.Name = "lblTechSIS";
            this.lblTechSIS.Size = new System.Drawing.Size(255, 57);
            this.lblTechSIS.TabIndex = 6;
            this.lblTechSIS.Text = "TechSIS INF";
            // 
            // lblProcurar
            // 
            this.lblProcurar.AutoSize = true;
            this.lblProcurar.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcurar.ForeColor = System.Drawing.Color.Red;
            this.lblProcurar.Location = new System.Drawing.Point(343, 122);
            this.lblProcurar.Name = "lblProcurar";
            this.lblProcurar.Size = new System.Drawing.Size(252, 22);
            this.lblProcurar.TabIndex = 5;
            this.lblProcurar.Text = "PROCURAR NOVA IMAGEM.:";
            // 
            // btnProcurarImagem
            // 
            this.btnProcurarImagem.Image = ((System.Drawing.Image)(resources.GetObject("btnProcurarImagem.Image")));
            this.btnProcurarImagem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProcurarImagem.Location = new System.Drawing.Point(601, 108);
            this.btnProcurarImagem.Name = "btnProcurarImagem";
            this.btnProcurarImagem.Size = new System.Drawing.Size(87, 36);
            this.btnProcurarImagem.TabIndex = 4;
            this.btnProcurarImagem.Text = "...";
            this.btnProcurarImagem.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnProcurarImagem.UseVisualStyleBackColor = true;
            this.btnProcurarImagem.Click += new System.EventHandler(this.btnProcurarImagem_Click);
            // 
            // lblNova
            // 
            this.lblNova.AutoSize = true;
            this.lblNova.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNova.Location = new System.Drawing.Point(223, 7);
            this.lblNova.Name = "lblNova";
            this.lblNova.Size = new System.Drawing.Size(54, 22);
            this.lblNova.TabIndex = 3;
            this.lblNova.Text = "NOVA";
            // 
            // lblAnterior
            // 
            this.lblAnterior.AutoSize = true;
            this.lblAnterior.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnterior.Location = new System.Drawing.Point(41, 7);
            this.lblAnterior.Name = "lblAnterior";
            this.lblAnterior.Size = new System.Drawing.Size(98, 22);
            this.lblAnterior.TabIndex = 2;
            this.lblAnterior.Text = "ANTERIOR";
            // 
            // picNova
            // 
            this.picNova.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picNova.Location = new System.Drawing.Point(176, 29);
            this.picNova.Name = "picNova";
            this.picNova.Size = new System.Drawing.Size(155, 115);
            this.picNova.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picNova.TabIndex = 1;
            this.picNova.TabStop = false;
            // 
            // picAnt
            // 
            this.picAnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAnt.Location = new System.Drawing.Point(15, 29);
            this.picAnt.Name = "picAnt";
            this.picAnt.Size = new System.Drawing.Size(155, 115);
            this.picAnt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAnt.TabIndex = 0;
            this.picAnt.TabStop = false;
            // 
            // panDown
            // 
            this.panDown.BackColor = System.Drawing.Color.White;
            this.panDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panDown.Controls.Add(this.lblExtencao);
            this.panDown.Controls.Add(this.picBrasil);
            this.panDown.Controls.Add(this.btnCancela);
            this.panDown.Controls.Add(this.btnOK);
            this.panDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panDown.Location = new System.Drawing.Point(0, 173);
            this.panDown.Name = "panDown";
            this.panDown.Size = new System.Drawing.Size(700, 45);
            this.panDown.TabIndex = 0;
            // 
            // lblExtencao
            // 
            this.lblExtencao.AutoSize = true;
            this.lblExtencao.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExtencao.Location = new System.Drawing.Point(124, 7);
            this.lblExtencao.Name = "lblExtencao";
            this.lblExtencao.Size = new System.Drawing.Size(304, 32);
            this.lblExtencao.TabIndex = 6;
            this.lblExtencao.Text = "Extensão.:   .JPG";
            // 
            // picBrasil
            // 
            this.picBrasil.Image = ((System.Drawing.Image)(resources.GetObject("picBrasil.Image")));
            this.picBrasil.Location = new System.Drawing.Point(3, 1);
            this.picBrasil.Name = "picBrasil";
            this.picBrasil.Size = new System.Drawing.Size(62, 40);
            this.picBrasil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBrasil.TabIndex = 5;
            this.picBrasil.TabStop = false;
            // 
            // btnCancela
            // 
            this.btnCancela.Image = ((System.Drawing.Image)(resources.GetObject("btnCancela.Image")));
            this.btnCancela.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancela.Location = new System.Drawing.Point(481, 3);
            this.btnCancela.Name = "btnCancela";
            this.btnCancela.Size = new System.Drawing.Size(107, 36);
            this.btnCancela.TabIndex = 3;
            this.btnCancela.Text = "CANCELA";
            this.btnCancela.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancela.UseVisualStyleBackColor = true;
            this.btnCancela.Click += new System.EventHandler(this.btnCancela_Click);
            // 
            // btnOK
            // 
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(588, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(107, 36);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK - F1";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // OpenFILE
            // 
            this.OpenFILE.Filter = "Imagens (*.JPG)|*.JPG";
            this.OpenFILE.Title = "TechSIS INF - LOCALIZE A IMAGEM";
            // 
            // ImagemFundo_REL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 220);
            this.Controls.Add(this.panPrin);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(718, 259);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(718, 259);
            this.Name = "ImagemFundo_REL";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechSIS INF - Troca Imagem de Fundo dos Relatórios RPV";
            this.Load += new System.EventHandler(this.ImagemFundo_REL_Load);
            this.panPrin.ResumeLayout(false);
            this.panUp.ResumeLayout(false);
            this.panUp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNova)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAnt)).EndInit();
            this.panDown.ResumeLayout(false);
            this.panDown.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBrasil)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panPrin;
        private System.Windows.Forms.Panel panUp;
        private System.Windows.Forms.Panel panDown;
        private System.Windows.Forms.PictureBox picNova;
        private System.Windows.Forms.PictureBox picAnt;
        private System.Windows.Forms.Label lblNova;
        private System.Windows.Forms.Label lblAnterior;
        private System.Windows.Forms.Label lblProcurar;
        private System.Windows.Forms.Button btnProcurarImagem;
        private System.Windows.Forms.Label lblTechSIS;
        private System.Windows.Forms.Button btnCancela;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox picBrasil;
        private System.Windows.Forms.Label lblExtencao;
        private System.Windows.Forms.OpenFileDialog OpenFILE;
    }
}