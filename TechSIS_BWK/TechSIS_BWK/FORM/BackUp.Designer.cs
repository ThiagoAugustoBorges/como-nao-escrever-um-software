namespace TechSIS_BWK
{
    partial class BackUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackUp));
            this.Painel_First = new System.Windows.Forms.Panel();
            this.Painel_Down = new System.Windows.Forms.Panel();
            this.Painel_ed = new System.Windows.Forms.Panel();
            this.btnBackUp = new System.Windows.Forms.Button();
            this.lblServidorRES = new System.Windows.Forms.Label();
            this.btnBackAutoma = new System.Windows.Forms.Button();
            this.btnCancela = new System.Windows.Forms.Button();
            this.lblServidor = new System.Windows.Forms.Label();
            this.proBar = new System.Windows.Forms.ProgressBar();
            this.lblTipoBackup = new System.Windows.Forms.Label();
            this.comTipoBackup = new System.Windows.Forms.ComboBox();
            this.lblSobre = new System.Windows.Forms.Label();
            this.SaveFILE = new System.Windows.Forms.SaveFileDialog();
            this.Painel_First.SuspendLayout();
            this.Painel_Down.SuspendLayout();
            this.Painel_ed.SuspendLayout();
            this.SuspendLayout();
            // 
            // Painel_First
            // 
            this.Painel_First.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Painel_First.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Painel_First.Controls.Add(this.Painel_Down);
            this.Painel_First.Controls.Add(this.lblSobre);
            this.Painel_First.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Painel_First.Location = new System.Drawing.Point(0, 0);
            this.Painel_First.Name = "Painel_First";
            this.Painel_First.Size = new System.Drawing.Size(609, 254);
            this.Painel_First.TabIndex = 0;
            // 
            // Painel_Down
            // 
            this.Painel_Down.BackColor = System.Drawing.SystemColors.Control;
            this.Painel_Down.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Painel_Down.Controls.Add(this.Painel_ed);
            this.Painel_Down.Controls.Add(this.lblServidor);
            this.Painel_Down.Controls.Add(this.proBar);
            this.Painel_Down.Controls.Add(this.lblTipoBackup);
            this.Painel_Down.Controls.Add(this.comTipoBackup);
            this.Painel_Down.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Painel_Down.Location = new System.Drawing.Point(0, 45);
            this.Painel_Down.Name = "Painel_Down";
            this.Painel_Down.Size = new System.Drawing.Size(607, 207);
            this.Painel_Down.TabIndex = 2;
            // 
            // Painel_ed
            // 
            this.Painel_ed.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Painel_ed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Painel_ed.Controls.Add(this.btnBackUp);
            this.Painel_ed.Controls.Add(this.lblServidorRES);
            this.Painel_ed.Controls.Add(this.btnBackAutoma);
            this.Painel_ed.Controls.Add(this.btnCancela);
            this.Painel_ed.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Painel_ed.Location = new System.Drawing.Point(0, 168);
            this.Painel_ed.Name = "Painel_ed";
            this.Painel_ed.Size = new System.Drawing.Size(605, 37);
            this.Painel_ed.TabIndex = 9;
            // 
            // btnBackUp
            // 
            this.btnBackUp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackUp.Image = ((System.Drawing.Image)(resources.GetObject("btnBackUp.Image")));
            this.btnBackUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackUp.Location = new System.Drawing.Point(492, 1);
            this.btnBackUp.Name = "btnBackUp";
            this.btnBackUp.Size = new System.Drawing.Size(114, 35);
            this.btnBackUp.TabIndex = 4;
            this.btnBackUp.Text = "BackUP";
            this.btnBackUp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBackUp.UseVisualStyleBackColor = true;
            this.btnBackUp.Click += new System.EventHandler(this.btnBackUp_Click);
            // 
            // lblServidorRES
            // 
            this.lblServidorRES.AutoSize = true;
            this.lblServidorRES.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServidorRES.ForeColor = System.Drawing.Color.Red;
            this.lblServidorRES.Location = new System.Drawing.Point(315, 10);
            this.lblServidorRES.Name = "lblServidorRES";
            this.lblServidorRES.Size = new System.Drawing.Size(14, 15);
            this.lblServidorRES.TabIndex = 7;
            this.lblServidorRES.Text = " ";
            // 
            // btnBackAutoma
            // 
            this.btnBackAutoma.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackAutoma.Image = ((System.Drawing.Image)(resources.GetObject("btnBackAutoma.Image")));
            this.btnBackAutoma.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackAutoma.Location = new System.Drawing.Point(-3, 1);
            this.btnBackAutoma.Name = "btnBackAutoma";
            this.btnBackAutoma.Size = new System.Drawing.Size(312, 35);
            this.btnBackAutoma.TabIndex = 8;
            this.btnBackAutoma.Text = "CONFIG. PARA BACKUP AUTOMÁTICO";
            this.btnBackAutoma.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBackAutoma.UseVisualStyleBackColor = true;
            this.btnBackAutoma.Click += new System.EventHandler(this.btnBackAutoma_Click);
            // 
            // btnCancela
            // 
            this.btnCancela.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancela.Image = ((System.Drawing.Image)(resources.GetObject("btnCancela.Image")));
            this.btnCancela.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancela.Location = new System.Drawing.Point(391, 1);
            this.btnCancela.Name = "btnCancela";
            this.btnCancela.Size = new System.Drawing.Size(102, 35);
            this.btnCancela.TabIndex = 3;
            this.btnCancela.Text = "CANCELA";
            this.btnCancela.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancela.UseVisualStyleBackColor = true;
            this.btnCancela.Click += new System.EventHandler(this.btnCancela_Click);
            // 
            // lblServidor
            // 
            this.lblServidor.AutoSize = true;
            this.lblServidor.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServidor.Location = new System.Drawing.Point(57, 81);
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.Size = new System.Drawing.Size(198, 18);
            this.lblServidor.TabIndex = 6;
            this.lblServidor.Text = "STATUS DO BackUP.: ";
            // 
            // proBar
            // 
            this.proBar.Location = new System.Drawing.Point(60, 99);
            this.proBar.Name = "proBar";
            this.proBar.Size = new System.Drawing.Size(457, 32);
            this.proBar.TabIndex = 5;
            // 
            // lblTipoBackup
            // 
            this.lblTipoBackup.AutoSize = true;
            this.lblTipoBackup.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoBackup.Location = new System.Drawing.Point(130, 13);
            this.lblTipoBackup.Name = "lblTipoBackup";
            this.lblTipoBackup.Size = new System.Drawing.Size(322, 23);
            this.lblTipoBackup.TabIndex = 2;
            this.lblTipoBackup.Text = "TIPO DE BackUP DESEJADO!";
            // 
            // comTipoBackup
            // 
            this.comTipoBackup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comTipoBackup.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comTipoBackup.FormattingEnabled = true;
            this.comTipoBackup.Items.AddRange(new object[] {
            "SISTEMA",
            "BANCO DE DADOS",
            "FILTROS"});
            this.comTipoBackup.Location = new System.Drawing.Point(60, 39);
            this.comTipoBackup.Name = "comTipoBackup";
            this.comTipoBackup.Size = new System.Drawing.Size(457, 23);
            this.comTipoBackup.TabIndex = 1;
            this.comTipoBackup.SelectedIndexChanged += new System.EventHandler(this.comTipoBackup_SelectedIndexChanged);
            // 
            // lblSobre
            // 
            this.lblSobre.AutoSize = true;
            this.lblSobre.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSobre.ForeColor = System.Drawing.Color.Red;
            this.lblSobre.Location = new System.Drawing.Point(129, 8);
            this.lblSobre.Name = "lblSobre";
            this.lblSobre.Size = new System.Drawing.Size(321, 33);
            this.lblSobre.TabIndex = 1;
            this.lblSobre.Text = "TechSIS INF BackUp";
            // 
            // SaveFILE
            // 
            this.SaveFILE.Title = "Defina o caminho do arquivo a ser salvo";
            // 
            // BackUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 254);
            this.Controls.Add(this.Painel_First);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(625, 293);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(625, 293);
            this.Name = "BackUp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechSIS Auto-Manutenção.: BackUp";
            this.Load += new System.EventHandler(this.BackUp_Load);
            this.Painel_First.ResumeLayout(false);
            this.Painel_First.PerformLayout();
            this.Painel_Down.ResumeLayout(false);
            this.Painel_Down.PerformLayout();
            this.Painel_ed.ResumeLayout(false);
            this.Painel_ed.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Painel_First;
        private System.Windows.Forms.Label lblSobre;
        private System.Windows.Forms.SaveFileDialog SaveFILE;
        private System.Windows.Forms.Panel Painel_Down;
        private System.Windows.Forms.Button btnBackAutoma;
        private System.Windows.Forms.Label lblServidorRES;
        private System.Windows.Forms.Label lblServidor;
        private System.Windows.Forms.ProgressBar proBar;
        private System.Windows.Forms.Button btnBackUp;
        private System.Windows.Forms.Button btnCancela;
        private System.Windows.Forms.Label lblTipoBackup;
        private System.Windows.Forms.ComboBox comTipoBackup;
        private System.Windows.Forms.Panel Painel_ed;
    }
}