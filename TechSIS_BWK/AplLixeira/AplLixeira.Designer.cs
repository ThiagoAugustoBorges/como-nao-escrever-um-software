namespace AplLixeira
{
    partial class AplLixeira
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AplLixeira));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panPrin = new System.Windows.Forms.Panel();
            this.panDown = new System.Windows.Forms.Panel();
            this.panUp = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.grbCampos = new System.Windows.Forms.GroupBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtDescri = new System.Windows.Forms.TextBox();
            this.lblPrograma = new System.Windows.Forms.Label();
            this.Dgv_Lixeira = new System.Windows.Forms.DataGridView();
            this.panButtons = new System.Windows.Forms.Panel();
            this.grbDown = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnEsvaziar = new System.Windows.Forms.Button();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.cheMensagem = new System.Windows.Forms.CheckBox();
            this.dgvcCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcOrigem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcDescri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panPrin.SuspendLayout();
            this.panDown.SuspendLayout();
            this.panUp.SuspendLayout();
            this.grbCampos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Lixeira)).BeginInit();
            this.panButtons.SuspendLayout();
            this.grbDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // panPrin
            // 
            this.panPrin.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panPrin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPrin.Controls.Add(this.panUp);
            this.panPrin.Controls.Add(this.panDown);
            this.panPrin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPrin.Location = new System.Drawing.Point(0, 0);
            this.panPrin.Name = "panPrin";
            this.panPrin.Size = new System.Drawing.Size(583, 476);
            this.panPrin.TabIndex = 0;
            // 
            // panDown
            // 
            this.panDown.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panDown.Controls.Add(this.grbDown);
            this.panDown.Controls.Add(this.panButtons);
            this.panDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panDown.Location = new System.Drawing.Point(0, 433);
            this.panDown.Name = "panDown";
            this.panDown.Size = new System.Drawing.Size(581, 41);
            this.panDown.TabIndex = 2;
            // 
            // panUp
            // 
            this.panUp.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panUp.Controls.Add(this.Dgv_Lixeira);
            this.panUp.Controls.Add(this.grbCampos);
            this.panUp.Controls.Add(this.lblTitulo);
            this.panUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panUp.Location = new System.Drawing.Point(0, 0);
            this.panUp.Name = "panUp";
            this.panUp.Size = new System.Drawing.Size(581, 433);
            this.panUp.TabIndex = 1;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblTitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitulo.Font = new System.Drawing.Font("Courier New", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(-20, -1);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(625, 38);
            this.lblTitulo.TabIndex = 6;
            this.lblTitulo.Text = "            LIXEIRA             ";
            // 
            // grbCampos
            // 
            this.grbCampos.Controls.Add(this.lblPrograma);
            this.grbCampos.Controls.Add(this.txtDescri);
            this.grbCampos.Controls.Add(this.txtCodigo);
            this.grbCampos.Location = new System.Drawing.Point(2, 37);
            this.grbCampos.Name = "grbCampos";
            this.grbCampos.Size = new System.Drawing.Size(575, 61);
            this.grbCampos.TabIndex = 1;
            this.grbCampos.TabStop = false;
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.PaleGreen;
            this.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigo.Location = new System.Drawing.Point(8, 28);
            this.txtCodigo.MaxLength = 6;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(60, 20);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigo.TextChanged += new System.EventHandler(this.txtCodigo_TextChanged);
            this.txtCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyDown);
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            this.txtCodigo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtCodigo_MouseDown);
            this.txtCodigo.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtCodigo_PreviewKeyDown);
            // 
            // txtDescri
            // 
            this.txtDescri.BackColor = System.Drawing.Color.White;
            this.txtDescri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescri.Enabled = false;
            this.txtDescri.Location = new System.Drawing.Point(68, 28);
            this.txtDescri.MaxLength = 6;
            this.txtDescri.Name = "txtDescri";
            this.txtDescri.Size = new System.Drawing.Size(499, 20);
            this.txtDescri.TabIndex = 2;
            // 
            // lblPrograma
            // 
            this.lblPrograma.AutoSize = true;
            this.lblPrograma.Location = new System.Drawing.Point(5, 14);
            this.lblPrograma.Name = "lblPrograma";
            this.lblPrograma.Size = new System.Drawing.Size(154, 14);
            this.lblPrograma.TabIndex = 10;
            this.lblPrograma.Text = "PROGRAMA DO SISTEMA.:";
            // 
            // Dgv_Lixeira
            // 
            this.Dgv_Lixeira.AllowUserToAddRows = false;
            this.Dgv_Lixeira.AllowUserToDeleteRows = false;
            this.Dgv_Lixeira.AllowUserToResizeColumns = false;
            this.Dgv_Lixeira.AllowUserToResizeRows = false;
            this.Dgv_Lixeira.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Dgv_Lixeira.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Dgv_Lixeira.ColumnHeadersHeight = 20;
            this.Dgv_Lixeira.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Dgv_Lixeira.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcCodigo,
            this.dgvcOrigem,
            this.dgvcDescri});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Lixeira.DefaultCellStyle = dataGridViewCellStyle3;
            this.Dgv_Lixeira.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Dgv_Lixeira.Location = new System.Drawing.Point(0, 102);
            this.Dgv_Lixeira.MultiSelect = false;
            this.Dgv_Lixeira.Name = "Dgv_Lixeira";
            this.Dgv_Lixeira.ReadOnly = true;
            this.Dgv_Lixeira.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Dgv_Lixeira.RowHeadersVisible = false;
            this.Dgv_Lixeira.RowHeadersWidth = 35;
            this.Dgv_Lixeira.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Dgv_Lixeira.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_Lixeira.Size = new System.Drawing.Size(579, 329);
            this.Dgv_Lixeira.TabIndex = 8;
            this.Dgv_Lixeira.TabStop = false;
            // 
            // panButtons
            // 
            this.panButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panButtons.Controls.Add(this.btnEsvaziar);
            this.panButtons.Controls.Add(this.btnCancelar);
            this.panButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panButtons.Location = new System.Drawing.Point(346, 0);
            this.panButtons.Name = "panButtons";
            this.panButtons.Size = new System.Drawing.Size(233, 39);
            this.panButtons.TabIndex = 0;
            // 
            // grbDown
            // 
            this.grbDown.Controls.Add(this.cheMensagem);
            this.grbDown.Controls.Add(this.lblUsuario);
            this.grbDown.Controls.Add(this.txtUsuario);
            this.grbDown.Location = new System.Drawing.Point(1, -5);
            this.grbDown.Name = "grbDown";
            this.grbDown.Size = new System.Drawing.Size(345, 43);
            this.grbDown.TabIndex = 1;
            this.grbDown.TabStop = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(0, 0);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(101, 37);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnEsvaziar
            // 
            this.btnEsvaziar.Enabled = false;
            this.btnEsvaziar.Image = ((System.Drawing.Image)(resources.GetObject("btnEsvaziar.Image")));
            this.btnEsvaziar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEsvaziar.Location = new System.Drawing.Point(99, 0);
            this.btnEsvaziar.Name = "btnEsvaziar";
            this.btnEsvaziar.Size = new System.Drawing.Size(132, 37);
            this.btnEsvaziar.TabIndex = 2;
            this.btnEsvaziar.Text = "ESVAZIAR TUDO";
            this.btnEsvaziar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEsvaziar.UseVisualStyleBackColor = true;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(3, 19);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(70, 14);
            this.lblUsuario.TabIndex = 73;
            this.lblUsuario.Text = "USUÁRIO.:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.BackColor = System.Drawing.Color.White;
            this.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsuario.Enabled = false;
            this.txtUsuario.Location = new System.Drawing.Point(73, 15);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(74, 20);
            this.txtUsuario.TabIndex = 72;
            this.txtUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cheMensagem
            // 
            this.cheMensagem.AutoSize = true;
            this.cheMensagem.Location = new System.Drawing.Point(168, 17);
            this.cheMensagem.Name = "cheMensagem";
            this.cheMensagem.Size = new System.Drawing.Size(159, 18);
            this.cheMensagem.TabIndex = 0;
            this.cheMensagem.TabStop = false;
            this.cheMensagem.Text = "NÃO EXIBIR MENSAGEM";
            this.cheMensagem.UseVisualStyleBackColor = true;
            // 
            // dgvcCodigo
            // 
            this.dgvcCodigo.HeaderText = "CÓDIGO";
            this.dgvcCodigo.Name = "dgvcCodigo";
            this.dgvcCodigo.ReadOnly = true;
            this.dgvcCodigo.Width = 80;
            // 
            // dgvcOrigem
            // 
            this.dgvcOrigem.HeaderText = "ORIGEM";
            this.dgvcOrigem.Name = "dgvcOrigem";
            this.dgvcOrigem.ReadOnly = true;
            this.dgvcOrigem.Width = 140;
            // 
            // dgvcDescri
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvcDescri.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvcDescri.HeaderText = "DESCRIÇÃO DO ITEM";
            this.dgvcDescri.Name = "dgvcDescri";
            this.dgvcDescri.ReadOnly = true;
            this.dgvcDescri.Width = 500;
            // 
            // AplLixeira
            // 
            this.AcceptButton = this.btnEsvaziar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(583, 476);
            this.Controls.Add(this.panPrin);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(599, 515);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(599, 515);
            this.Name = "AplLixeira";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LIXEIRA DO SISTEMA                     AplLixeira.dll";
            this.panPrin.ResumeLayout(false);
            this.panDown.ResumeLayout(false);
            this.panUp.ResumeLayout(false);
            this.panUp.PerformLayout();
            this.grbCampos.ResumeLayout(false);
            this.grbCampos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Lixeira)).EndInit();
            this.panButtons.ResumeLayout(false);
            this.grbDown.ResumeLayout(false);
            this.grbDown.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panPrin;
        private System.Windows.Forms.Panel panUp;
        private System.Windows.Forms.Panel panDown;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grbCampos;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtDescri;
        private System.Windows.Forms.Label lblPrograma;
        private System.Windows.Forms.DataGridView Dgv_Lixeira;
        private System.Windows.Forms.GroupBox grbDown;
        private System.Windows.Forms.Panel panButtons;
        private System.Windows.Forms.Button btnEsvaziar;
        private System.Windows.Forms.Button btnCancelar;
        public System.Windows.Forms.Label lblUsuario;
        public System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.CheckBox cheMensagem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcOrigem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcDescri;

    }
}