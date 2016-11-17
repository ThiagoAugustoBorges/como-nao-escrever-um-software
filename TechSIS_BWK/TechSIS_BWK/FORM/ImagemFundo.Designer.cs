namespace TechSIS_BWK
{
    partial class ImagemFundo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImagemFundo));
            this.OpenFILE = new System.Windows.Forms.OpenFileDialog();
            this.Painel_First = new System.Windows.Forms.Panel();
            this.Painel_Up = new System.Windows.Forms.Panel();
            this.lbl2 = new System.Windows.Forms.Label();
            this.btnProcurar = new System.Windows.Forms.Button();
            this.txtCaminho = new System.Windows.Forms.TextBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.Painel_Down = new System.Windows.Forms.Panel();
            this.picBrasil = new System.Windows.Forms.PictureBox();
            this.lbl3 = new System.Windows.Forms.Label();
            this.btnCancela = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.Painel_First.SuspendLayout();
            this.Painel_Up.SuspendLayout();
            this.Painel_Down.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBrasil)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenFILE
            // 
            this.OpenFILE.Filter = "Imagens (*.PNG)|*.PNG";
            this.OpenFILE.Title = "TechSIS INF - LOCALIZE A IMAGEM";
            // 
            // Painel_First
            // 
            this.Painel_First.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Painel_First.Controls.Add(this.Painel_Up);
            this.Painel_First.Controls.Add(this.Painel_Down);
            this.Painel_First.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Painel_First.Location = new System.Drawing.Point(0, 0);
            this.Painel_First.Name = "Painel_First";
            this.Painel_First.Size = new System.Drawing.Size(553, 177);
            this.Painel_First.TabIndex = 0;
            // 
            // Painel_Up
            // 
            this.Painel_Up.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Painel_Up.Controls.Add(this.lbl2);
            this.Painel_Up.Controls.Add(this.btnProcurar);
            this.Painel_Up.Controls.Add(this.txtCaminho);
            this.Painel_Up.Controls.Add(this.lbl1);
            this.Painel_Up.Dock = System.Windows.Forms.DockStyle.Top;
            this.Painel_Up.Location = new System.Drawing.Point(0, 0);
            this.Painel_Up.Name = "Painel_Up";
            this.Painel_Up.Size = new System.Drawing.Size(551, 129);
            this.Painel_Up.TabIndex = 2;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Font = new System.Drawing.Font("Segoe Print", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2.Location = new System.Drawing.Point(158, 34);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(231, 51);
            this.lbl2.TabIndex = 3;
            this.lbl2.Text = "E TECLE (OK)";
            // 
            // btnProcurar
            // 
            this.btnProcurar.Location = new System.Drawing.Point(498, 84);
            this.btnProcurar.Name = "btnProcurar";
            this.btnProcurar.Size = new System.Drawing.Size(42, 31);
            this.btnProcurar.TabIndex = 2;
            this.btnProcurar.Text = "...";
            this.btnProcurar.UseVisualStyleBackColor = true;
            this.btnProcurar.Click += new System.EventHandler(this.btnProcurar_Click);
            // 
            // txtCaminho
            // 
            this.txtCaminho.BackColor = System.Drawing.Color.White;
            this.txtCaminho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCaminho.Enabled = false;
            this.txtCaminho.Location = new System.Drawing.Point(13, 91);
            this.txtCaminho.Name = "txtCaminho";
            this.txtCaminho.Size = new System.Drawing.Size(479, 20);
            this.txtCaminho.TabIndex = 1;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Segoe Print", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(59, -4);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(433, 51);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "SELECIONE NOVA IMAGEM";
            // 
            // Painel_Down
            // 
            this.Painel_Down.BackColor = System.Drawing.Color.White;
            this.Painel_Down.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Painel_Down.Controls.Add(this.picBrasil);
            this.Painel_Down.Controls.Add(this.lbl3);
            this.Painel_Down.Controls.Add(this.btnCancela);
            this.Painel_Down.Controls.Add(this.btnOK);
            this.Painel_Down.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Painel_Down.Location = new System.Drawing.Point(0, 129);
            this.Painel_Down.Name = "Painel_Down";
            this.Painel_Down.Size = new System.Drawing.Size(551, 46);
            this.Painel_Down.TabIndex = 1;
            // 
            // picBrasil
            // 
            this.picBrasil.Image = ((System.Drawing.Image)(resources.GetObject("picBrasil.Image")));
            this.picBrasil.Location = new System.Drawing.Point(258, 2);
            this.picBrasil.Name = "picBrasil";
            this.picBrasil.Size = new System.Drawing.Size(62, 40);
            this.picBrasil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBrasil.TabIndex = 4;
            this.picBrasil.TabStop = false;
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl3.Location = new System.Drawing.Point(2, 7);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(253, 32);
            this.lbl3.TabIndex = 4;
            this.lbl3.Text = "Extensão: .PNG";
            // 
            // btnCancela
            // 
            this.btnCancela.Image = ((System.Drawing.Image)(resources.GetObject("btnCancela.Image")));
            this.btnCancela.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancela.Location = new System.Drawing.Point(326, 5);
            this.btnCancela.Name = "btnCancela";
            this.btnCancela.Size = new System.Drawing.Size(107, 36);
            this.btnCancela.TabIndex = 1;
            this.btnCancela.Text = "CANCELA";
            this.btnCancela.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancela.UseVisualStyleBackColor = true;
            this.btnCancela.Click += new System.EventHandler(this.btnCancela_Click);
            // 
            // btnOK
            // 
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(433, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(107, 36);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK - F1";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ImagemFundo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 177);
            this.Controls.Add(this.Painel_First);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(569, 216);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(569, 216);
            this.Name = "ImagemFundo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechSIS INF - Troca Imagem de Fundo";
            this.Load += new System.EventHandler(this.ImagemFundo_Load);
            this.Painel_First.ResumeLayout(false);
            this.Painel_Up.ResumeLayout(false);
            this.Painel_Up.PerformLayout();
            this.Painel_Down.ResumeLayout(false);
            this.Painel_Down.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBrasil)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog OpenFILE;
        private System.Windows.Forms.Panel Painel_First;
        private System.Windows.Forms.Panel Painel_Up;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Button btnProcurar;
        private System.Windows.Forms.TextBox txtCaminho;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Panel Painel_Down;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Button btnCancela;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox picBrasil;
    }
}