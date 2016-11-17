namespace PesMsgNt.cs
{
    partial class PesMsgNt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PesMsgNt));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panPrinPesq = new System.Windows.Forms.Panel();
            this.panDownPesq = new System.Windows.Forms.Panel();
            this.btnAjuda = new System.Windows.Forms.Button();
            this.cheFiltrosPES = new System.Windows.Forms.CheckBox();
            this.txtQtSelectPES = new System.Windows.Forms.TextBox();
            this.lblSelectPES = new System.Windows.Forms.Label();
            this.panButtonsDownAb1 = new System.Windows.Forms.Panel();
            this.btnFechar = new System.Windows.Forms.Button();
            this.panUpPesq = new System.Windows.Forms.Panel();
            this.rabAlfabetico = new System.Windows.Forms.RadioButton();
            this.rabNumerico = new System.Windows.Forms.RadioButton();
            this.Dgv_Pesquisa = new System.Windows.Forms.DataGridView();
            this.dgvcPesCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcPesDescri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panPrinPesq.SuspendLayout();
            this.panDownPesq.SuspendLayout();
            this.panButtonsDownAb1.SuspendLayout();
            this.panUpPesq.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Pesquisa)).BeginInit();
            this.SuspendLayout();
            // 
            // panPrinPesq
            // 
            this.panPrinPesq.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panPrinPesq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPrinPesq.Controls.Add(this.panDownPesq);
            this.panPrinPesq.Controls.Add(this.panUpPesq);
            this.panPrinPesq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPrinPesq.Location = new System.Drawing.Point(0, 0);
            this.panPrinPesq.Name = "panPrinPesq";
            this.panPrinPesq.Size = new System.Drawing.Size(547, 446);
            this.panPrinPesq.TabIndex = 0;
            // 
            // panDownPesq
            // 
            this.panDownPesq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panDownPesq.Controls.Add(this.btnAjuda);
            this.panDownPesq.Controls.Add(this.cheFiltrosPES);
            this.panDownPesq.Controls.Add(this.txtQtSelectPES);
            this.panDownPesq.Controls.Add(this.lblSelectPES);
            this.panDownPesq.Controls.Add(this.panButtonsDownAb1);
            this.panDownPesq.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panDownPesq.Location = new System.Drawing.Point(0, 407);
            this.panDownPesq.Name = "panDownPesq";
            this.panDownPesq.Size = new System.Drawing.Size(545, 37);
            this.panDownPesq.TabIndex = 1;
            // 
            // btnAjuda
            // 
            this.btnAjuda.Image = ((System.Drawing.Image)(resources.GetObject("btnAjuda.Image")));
            this.btnAjuda.Location = new System.Drawing.Point(2, 3);
            this.btnAjuda.Name = "btnAjuda";
            this.btnAjuda.Size = new System.Drawing.Size(45, 30);
            this.btnAjuda.TabIndex = 93;
            this.btnAjuda.TabStop = false;
            this.btnAjuda.UseVisualStyleBackColor = true;
            this.btnAjuda.Click += new System.EventHandler(this.btnAjuda_Click);
            // 
            // cheFiltrosPES
            // 
            this.cheFiltrosPES.AutoSize = true;
            this.cheFiltrosPES.Location = new System.Drawing.Point(276, 11);
            this.cheFiltrosPES.Name = "cheFiltrosPES";
            this.cheFiltrosPES.Size = new System.Drawing.Size(131, 18);
            this.cheFiltrosPES.TabIndex = 92;
            this.cheFiltrosPES.TabStop = false;
            this.cheFiltrosPES.Text = "LEMBRAR FILTROS";
            this.cheFiltrosPES.UseVisualStyleBackColor = true;
            // 
            // txtQtSelectPES
            // 
            this.txtQtSelectPES.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtQtSelectPES.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQtSelectPES.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtQtSelectPES.Location = new System.Drawing.Point(163, 9);
            this.txtQtSelectPES.Name = "txtQtSelectPES";
            this.txtQtSelectPES.Size = new System.Drawing.Size(71, 20);
            this.txtQtSelectPES.TabIndex = 91;
            this.txtQtSelectPES.TabStop = false;
            this.txtQtSelectPES.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSelectPES
            // 
            this.lblSelectPES.AutoSize = true;
            this.lblSelectPES.Location = new System.Drawing.Point(101, 15);
            this.lblSelectPES.Name = "lblSelectPES";
            this.lblSelectPES.Size = new System.Drawing.Size(63, 14);
            this.lblSelectPES.TabIndex = 90;
            this.lblSelectPES.Text = "SELECT.:";
            // 
            // panButtonsDownAb1
            // 
            this.panButtonsDownAb1.BackColor = System.Drawing.SystemColors.Control;
            this.panButtonsDownAb1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panButtonsDownAb1.Controls.Add(this.btnFechar);
            this.panButtonsDownAb1.Location = new System.Drawing.Point(433, -1);
            this.panButtonsDownAb1.Name = "panButtonsDownAb1";
            this.panButtonsDownAb1.Size = new System.Drawing.Size(110, 37);
            this.panButtonsDownAb1.TabIndex = 89;
            // 
            // btnFechar
            // 
            this.btnFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFechar.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar.Image = ((System.Drawing.Image)(resources.GetObject("btnFechar.Image")));
            this.btnFechar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFechar.Location = new System.Drawing.Point(3, 3);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(101, 29);
            this.btnFechar.TabIndex = 3;
            this.btnFechar.Text = "&Fechar - F7";
            this.btnFechar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // panUpPesq
            // 
            this.panUpPesq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panUpPesq.Controls.Add(this.rabAlfabetico);
            this.panUpPesq.Controls.Add(this.rabNumerico);
            this.panUpPesq.Controls.Add(this.Dgv_Pesquisa);
            this.panUpPesq.Dock = System.Windows.Forms.DockStyle.Top;
            this.panUpPesq.Location = new System.Drawing.Point(0, 0);
            this.panUpPesq.Name = "panUpPesq";
            this.panUpPesq.Size = new System.Drawing.Size(545, 407);
            this.panUpPesq.TabIndex = 0;
            // 
            // rabAlfabetico
            // 
            this.rabAlfabetico.AutoSize = true;
            this.rabAlfabetico.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rabAlfabetico.Location = new System.Drawing.Point(281, 13);
            this.rabAlfabetico.Name = "rabAlfabetico";
            this.rabAlfabetico.Size = new System.Drawing.Size(259, 26);
            this.rabAlfabetico.TabIndex = 1;
            this.rabAlfabetico.Text = "ORDEM Alfabética - F4";
            this.rabAlfabetico.UseVisualStyleBackColor = true;
            this.rabAlfabetico.CheckedChanged += new System.EventHandler(this.rabAlfabetico_CheckedChanged);
            // 
            // rabNumerico
            // 
            this.rabNumerico.AutoSize = true;
            this.rabNumerico.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rabNumerico.Location = new System.Drawing.Point(6, 13);
            this.rabNumerico.Name = "rabNumerico";
            this.rabNumerico.Size = new System.Drawing.Size(237, 26);
            this.rabNumerico.TabIndex = 0;
            this.rabNumerico.Text = "ORDEM Númerica - F3";
            this.rabNumerico.UseVisualStyleBackColor = true;
            this.rabNumerico.CheckedChanged += new System.EventHandler(this.rabNumerico_CheckedChanged);
            // 
            // Dgv_Pesquisa
            // 
            this.Dgv_Pesquisa.AllowUserToAddRows = false;
            this.Dgv_Pesquisa.AllowUserToDeleteRows = false;
            this.Dgv_Pesquisa.AllowUserToResizeRows = false;
            this.Dgv_Pesquisa.BackgroundColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Dgv_Pesquisa.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.Dgv_Pesquisa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Pesquisa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcPesCodigo,
            this.dgvcPesDescri});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.ForestGreen;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Pesquisa.DefaultCellStyle = dataGridViewCellStyle10;
            this.Dgv_Pesquisa.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Dgv_Pesquisa.GridColor = System.Drawing.Color.Black;
            this.Dgv_Pesquisa.Location = new System.Drawing.Point(0, 45);
            this.Dgv_Pesquisa.Name = "Dgv_Pesquisa";
            this.Dgv_Pesquisa.ReadOnly = true;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Dgv_Pesquisa.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.Dgv_Pesquisa.RowHeadersVisible = false;
            this.Dgv_Pesquisa.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White;
            this.Dgv_Pesquisa.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.Dgv_Pesquisa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_Pesquisa.Size = new System.Drawing.Size(543, 360);
            this.Dgv_Pesquisa.TabIndex = 88;
            this.Dgv_Pesquisa.TabStop = false;
            this.Dgv_Pesquisa.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Pesquisa_CellClick);
            this.Dgv_Pesquisa.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Dgv_Pesquisa_RowsAdded);
            this.Dgv_Pesquisa.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Dgv_Pesquisa_RowsRemoved);
            // 
            // dgvcPesCodigo
            // 
            this.dgvcPesCodigo.HeaderText = "Código";
            this.dgvcPesCodigo.Name = "dgvcPesCodigo";
            this.dgvcPesCodigo.ReadOnly = true;
            this.dgvcPesCodigo.Width = 65;
            // 
            // dgvcPesDescri
            // 
            this.dgvcPesDescri.HeaderText = "Descrição da Mensagem";
            this.dgvcPesDescri.Name = "dgvcPesDescri";
            this.dgvcPesDescri.ReadOnly = true;
            this.dgvcPesDescri.Width = 474;
            // 
            // PesMsgNt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnFechar;
            this.ClientSize = new System.Drawing.Size(547, 446);
            this.Controls.Add(this.panPrinPesq);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(563, 485);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(563, 485);
            this.Name = "PesMsgNt";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechSIS INF - Pesquisa de Mensagens para Nt                 PesMsgNt.dll";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PesMsgNt_FormClosing);
            this.Load += new System.EventHandler(this.PesMsgNt_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PesMsgNt_KeyDown);
            this.panPrinPesq.ResumeLayout(false);
            this.panDownPesq.ResumeLayout(false);
            this.panDownPesq.PerformLayout();
            this.panButtonsDownAb1.ResumeLayout(false);
            this.panUpPesq.ResumeLayout(false);
            this.panUpPesq.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Pesquisa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panPrinPesq;
        private System.Windows.Forms.Panel panDownPesq;
        private System.Windows.Forms.Panel panUpPesq;
        private System.Windows.Forms.DataGridView Dgv_Pesquisa;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcPesCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcPesDescri;
        private System.Windows.Forms.RadioButton rabAlfabetico;
        private System.Windows.Forms.RadioButton rabNumerico;
        private System.Windows.Forms.CheckBox cheFiltrosPES;
        private System.Windows.Forms.TextBox txtQtSelectPES;
        private System.Windows.Forms.Label lblSelectPES;
        public System.Windows.Forms.Panel panButtonsDownAb1;
        public System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnAjuda;
    }
}