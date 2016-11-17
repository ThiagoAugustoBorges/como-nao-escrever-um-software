namespace TechSIS_WenProtect
{
    partial class WenProtect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WenProtect));
            this.panPrin = new System.Windows.Forms.Panel();
            this.grbCrypt = new System.Windows.Forms.GroupBox();
            this.lblOutput = new System.Windows.Forms.Label();
            this.lblInput = new System.Windows.Forms.Label();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.rtbInput = new System.Windows.Forms.RichTextBox();
            this.grbForm = new System.Windows.Forms.GroupBox();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnDisprotect = new System.Windows.Forms.Button();
            this.btnProtect = new System.Windows.Forms.Button();
            this.lblSenha = new System.Windows.Forms.Label();
            this.txtSenhaCript = new System.Windows.Forms.TextBox();
            this.panPrin.SuspendLayout();
            this.grbCrypt.SuspendLayout();
            this.grbForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // panPrin
            // 
            this.panPrin.BackColor = System.Drawing.Color.White;
            this.panPrin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPrin.Controls.Add(this.grbCrypt);
            this.panPrin.Controls.Add(this.grbForm);
            this.panPrin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPrin.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panPrin.Location = new System.Drawing.Point(0, 0);
            this.panPrin.Name = "panPrin";
            this.panPrin.Size = new System.Drawing.Size(784, 441);
            this.panPrin.TabIndex = 0;
            // 
            // grbCrypt
            // 
            this.grbCrypt.Controls.Add(this.lblOutput);
            this.grbCrypt.Controls.Add(this.lblInput);
            this.grbCrypt.Controls.Add(this.rtbOutput);
            this.grbCrypt.Controls.Add(this.rtbInput);
            this.grbCrypt.Location = new System.Drawing.Point(3, 78);
            this.grbCrypt.Name = "grbCrypt";
            this.grbCrypt.Size = new System.Drawing.Size(776, 358);
            this.grbCrypt.TabIndex = 1;
            this.grbCrypt.TabStop = false;
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(11, 183);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(49, 14);
            this.lblOutput.TabIndex = 3;
            this.lblOutput.Text = "OUTPUT";
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point(11, 14);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(42, 14);
            this.lblInput.TabIndex = 2;
            this.lblInput.Text = "INPUT";
            // 
            // rtbOutput
            // 
            this.rtbOutput.BackColor = System.Drawing.Color.LightGray;
            this.rtbOutput.Location = new System.Drawing.Point(6, 200);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(762, 141);
            this.rtbOutput.TabIndex = 1;
            this.rtbOutput.Text = "";
            // 
            // rtbInput
            // 
            this.rtbInput.BackColor = System.Drawing.Color.DarkGray;
            this.rtbInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbInput.Location = new System.Drawing.Point(6, 33);
            this.rtbInput.Name = "rtbInput";
            this.rtbInput.Size = new System.Drawing.Size(762, 141);
            this.rtbInput.TabIndex = 0;
            this.rtbInput.Text = "";
            // 
            // grbForm
            // 
            this.grbForm.Controls.Add(this.btnSair);
            this.grbForm.Controls.Add(this.btnDisprotect);
            this.grbForm.Controls.Add(this.btnProtect);
            this.grbForm.Controls.Add(this.lblSenha);
            this.grbForm.Controls.Add(this.txtSenhaCript);
            this.grbForm.Location = new System.Drawing.Point(3, 3);
            this.grbForm.Name = "grbForm";
            this.grbForm.Size = new System.Drawing.Size(776, 69);
            this.grbForm.TabIndex = 0;
            this.grbForm.TabStop = false;
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(702, 17);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(68, 43);
            this.btnSair.TabIndex = 4;
            this.btnSair.Text = "SAIR";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnDisprotect
            // 
            this.btnDisprotect.Image = ((System.Drawing.Image)(resources.GetObject("btnDisprotect.Image")));
            this.btnDisprotect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisprotect.Location = new System.Drawing.Point(561, 17);
            this.btnDisprotect.Name = "btnDisprotect";
            this.btnDisprotect.Size = new System.Drawing.Size(142, 43);
            this.btnDisprotect.TabIndex = 3;
            this.btnDisprotect.Text = "WenDisprotect";
            this.btnDisprotect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDisprotect.UseVisualStyleBackColor = true;
            this.btnDisprotect.Click += new System.EventHandler(this.btnDisprotect_Click);
            // 
            // btnProtect
            // 
            this.btnProtect.Image = ((System.Drawing.Image)(resources.GetObject("btnProtect.Image")));
            this.btnProtect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProtect.Location = new System.Drawing.Point(420, 17);
            this.btnProtect.Name = "btnProtect";
            this.btnProtect.Size = new System.Drawing.Size(142, 43);
            this.btnProtect.TabIndex = 2;
            this.btnProtect.Text = "WenProtect";
            this.btnProtect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProtect.UseVisualStyleBackColor = true;
            this.btnProtect.Click += new System.EventHandler(this.btnProtect_Click);
            // 
            // lblSenha
            // 
            this.lblSenha.AutoSize = true;
            this.lblSenha.Location = new System.Drawing.Point(8, 35);
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.Size = new System.Drawing.Size(98, 14);
            this.lblSenha.TabIndex = 1;
            this.lblSenha.Text = "Senha Crypt.:";
            // 
            // txtSenhaCript
            // 
            this.txtSenhaCript.BackColor = System.Drawing.Color.Linen;
            this.txtSenhaCript.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSenhaCript.Location = new System.Drawing.Point(112, 29);
            this.txtSenhaCript.Name = "txtSenhaCript";
            this.txtSenhaCript.Size = new System.Drawing.Size(190, 20);
            this.txtSenhaCript.TabIndex = 0;
            this.txtSenhaCript.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSenhaCript.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSenhaCript_KeyDown);
            // 
            // WenProtect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 441);
            this.Controls.Add(this.panPrin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WenProtect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechSIS - WenProtect";
            this.panPrin.ResumeLayout(false);
            this.grbCrypt.ResumeLayout(false);
            this.grbCrypt.PerformLayout();
            this.grbForm.ResumeLayout(false);
            this.grbForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panPrin;
        private System.Windows.Forms.GroupBox grbCrypt;
        private System.Windows.Forms.GroupBox grbForm;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.RichTextBox rtbInput;
        private System.Windows.Forms.Label lblSenha;
        private System.Windows.Forms.TextBox txtSenhaCript;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnDisprotect;
        private System.Windows.Forms.Button btnProtect;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Label lblInput;
    }
}

