namespace TechSIS_BWK
{
    partial class InforMaquina
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InforMaquina));
            this.panPrin = new System.Windows.Forms.Panel();
            this.btnSair = new System.Windows.Forms.Button();
            this.pan3 = new System.Windows.Forms.Panel();
            this.lblVersSistema = new System.Windows.Forms.Label();
            this.lblArquitetura = new System.Windows.Forms.Label();
            this.lblUsuarWin = new System.Windows.Forms.Label();
            this.lblNumProc = new System.Windows.Forms.Label();
            this.lblUsuarioLogado = new System.Windows.Forms.Label();
            this.lblModuloSistema = new System.Windows.Forms.Label();
            this.lblDiretorioSistema = new System.Windows.Forms.Label();
            this.pan2 = new System.Windows.Forms.Panel();
            this.lblSistemaOperacional = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pan1 = new System.Windows.Forms.Panel();
            this.lblNomeDaMaquina = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panPrin.SuspendLayout();
            this.pan3.SuspendLayout();
            this.pan2.SuspendLayout();
            this.pan1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panPrin
            // 
            this.panPrin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPrin.Controls.Add(this.btnSair);
            this.panPrin.Controls.Add(this.pan3);
            this.panPrin.Controls.Add(this.pan2);
            this.panPrin.Controls.Add(this.pan1);
            this.panPrin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPrin.Location = new System.Drawing.Point(0, 0);
            this.panPrin.Name = "panPrin";
            this.panPrin.Size = new System.Drawing.Size(970, 332);
            this.panPrin.TabIndex = 0;
            // 
            // btnSair
            // 
            this.btnSair.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSair.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(0, 293);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(968, 37);
            this.btnSair.TabIndex = 4;
            this.btnSair.Text = "SAIR";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // pan3
            // 
            this.pan3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pan3.Controls.Add(this.lblVersSistema);
            this.pan3.Controls.Add(this.lblArquitetura);
            this.pan3.Controls.Add(this.lblUsuarWin);
            this.pan3.Controls.Add(this.lblNumProc);
            this.pan3.Controls.Add(this.lblUsuarioLogado);
            this.pan3.Controls.Add(this.lblModuloSistema);
            this.pan3.Controls.Add(this.lblDiretorioSistema);
            this.pan3.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan3.Location = new System.Drawing.Point(0, 82);
            this.pan3.Name = "pan3";
            this.pan3.Size = new System.Drawing.Size(968, 210);
            this.pan3.TabIndex = 3;
            // 
            // lblVersSistema
            // 
            this.lblVersSistema.AutoSize = true;
            this.lblVersSistema.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersSistema.Location = new System.Drawing.Point(11, 180);
            this.lblVersSistema.Name = "lblVersSistema";
            this.lblVersSistema.Size = new System.Drawing.Size(238, 18);
            this.lblVersSistema.TabIndex = 10;
            this.lblVersSistema.Text = "VERSÃO DO SISTEMA.....:";
            // 
            // lblArquitetura
            // 
            this.lblArquitetura.AutoSize = true;
            this.lblArquitetura.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArquitetura.Location = new System.Drawing.Point(10, 153);
            this.lblArquitetura.Name = "lblArquitetura";
            this.lblArquitetura.Size = new System.Drawing.Size(238, 18);
            this.lblArquitetura.TabIndex = 9;
            this.lblArquitetura.Text = "ARQUITETURA...........:";
            // 
            // lblUsuarWin
            // 
            this.lblUsuarWin.AutoSize = true;
            this.lblUsuarWin.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuarWin.Location = new System.Drawing.Point(11, 126);
            this.lblUsuarWin.Name = "lblUsuarWin";
            this.lblUsuarWin.Size = new System.Drawing.Size(238, 18);
            this.lblUsuarWin.TabIndex = 8;
            this.lblUsuarWin.Text = "USUÁRIO DO WINDOWS....:";
            // 
            // lblNumProc
            // 
            this.lblNumProc.AutoSize = true;
            this.lblNumProc.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumProc.Location = new System.Drawing.Point(11, 99);
            this.lblNumProc.Name = "lblNumProc";
            this.lblNumProc.Size = new System.Drawing.Size(238, 18);
            this.lblNumProc.TabIndex = 7;
            this.lblNumProc.Text = "NUM. DE PROCESSADORES.:";
            // 
            // lblUsuarioLogado
            // 
            this.lblUsuarioLogado.AutoSize = true;
            this.lblUsuarioLogado.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuarioLogado.Location = new System.Drawing.Point(11, 70);
            this.lblUsuarioLogado.Name = "lblUsuarioLogado";
            this.lblUsuarioLogado.Size = new System.Drawing.Size(238, 18);
            this.lblUsuarioLogado.TabIndex = 6;
            this.lblUsuarioLogado.Text = "USUÁRIO LOGADO........:";
            // 
            // lblModuloSistema
            // 
            this.lblModuloSistema.AutoSize = true;
            this.lblModuloSistema.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModuloSistema.Location = new System.Drawing.Point(11, 41);
            this.lblModuloSistema.Name = "lblModuloSistema";
            this.lblModuloSistema.Size = new System.Drawing.Size(238, 18);
            this.lblModuloSistema.TabIndex = 5;
            this.lblModuloSistema.Text = "MÓDULO DO SISTEMA.....:";
            // 
            // lblDiretorioSistema
            // 
            this.lblDiretorioSistema.AutoSize = true;
            this.lblDiretorioSistema.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiretorioSistema.Location = new System.Drawing.Point(10, 13);
            this.lblDiretorioSistema.Name = "lblDiretorioSistema";
            this.lblDiretorioSistema.Size = new System.Drawing.Size(238, 18);
            this.lblDiretorioSistema.TabIndex = 4;
            this.lblDiretorioSistema.Text = "DIRETÓRIO DO SISTEMA..:";
            // 
            // pan2
            // 
            this.pan2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pan2.Controls.Add(this.lblSistemaOperacional);
            this.pan2.Controls.Add(this.label4);
            this.pan2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan2.Location = new System.Drawing.Point(0, 41);
            this.pan2.Name = "pan2";
            this.pan2.Size = new System.Drawing.Size(968, 41);
            this.pan2.TabIndex = 2;
            // 
            // lblSistemaOperacional
            // 
            this.lblSistemaOperacional.AutoSize = true;
            this.lblSistemaOperacional.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSistemaOperacional.Location = new System.Drawing.Point(299, 10);
            this.lblSistemaOperacional.Name = "lblSistemaOperacional";
            this.lblSistemaOperacional.Size = new System.Drawing.Size(23, 23);
            this.lblSistemaOperacional.TabIndex = 3;
            this.lblSistemaOperacional.Text = " ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(283, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "Sistema Operacional.:";
            // 
            // pan1
            // 
            this.pan1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pan1.Controls.Add(this.lblNomeDaMaquina);
            this.pan1.Controls.Add(this.label1);
            this.pan1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan1.Location = new System.Drawing.Point(0, 0);
            this.pan1.Name = "pan1";
            this.pan1.Size = new System.Drawing.Size(968, 41);
            this.pan1.TabIndex = 1;
            // 
            // lblNomeDaMaquina
            // 
            this.lblNomeDaMaquina.AutoSize = true;
            this.lblNomeDaMaquina.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeDaMaquina.Location = new System.Drawing.Point(299, 10);
            this.lblNomeDaMaquina.Name = "lblNomeDaMaquina";
            this.lblNomeDaMaquina.Size = new System.Drawing.Size(23, 23);
            this.lblNomeDaMaquina.TabIndex = 3;
            this.lblNomeDaMaquina.Text = " ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nome Da Maquina.....:";
            // 
            // InforMaquina
            // 
            this.AcceptButton = this.btnSair;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSair;
            this.ClientSize = new System.Drawing.Size(970, 332);
            this.Controls.Add(this.panPrin);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(986, 371);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(986, 371);
            this.Name = "InforMaquina";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechSIS Informações da Maquina para Suporte";
            this.Load += new System.EventHandler(this.InforMaquina_Load);
            this.panPrin.ResumeLayout(false);
            this.pan3.ResumeLayout(false);
            this.pan3.PerformLayout();
            this.pan2.ResumeLayout(false);
            this.pan2.PerformLayout();
            this.pan1.ResumeLayout(false);
            this.pan1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panPrin;
        private System.Windows.Forms.Panel pan1;
        private System.Windows.Forms.Label lblNomeDaMaquina;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pan2;
        private System.Windows.Forms.Label lblSistemaOperacional;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pan3;
        private System.Windows.Forms.Label lblModuloSistema;
        private System.Windows.Forms.Label lblDiretorioSistema;
        private System.Windows.Forms.Label lblUsuarioLogado;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Label lblNumProc;
        private System.Windows.Forms.Label lblUsuarWin;
        private System.Windows.Forms.Label lblArquitetura;
        private System.Windows.Forms.Label lblVersSistema;
    }
}