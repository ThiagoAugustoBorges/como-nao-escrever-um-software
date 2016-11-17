namespace PesEmpre.cs
{
    partial class PesEmpre
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PesEmpre));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panPrinPesq = new System.Windows.Forms.Panel();
            this.panUpPesq = new System.Windows.Forms.Panel();
            this.grbFiltros = new System.Windows.Forms.GroupBox();
            this.btnAjuda = new System.Windows.Forms.Button();
            this.comModuloPES = new System.Windows.Forms.ComboBox();
            this.lblModuloPES = new System.Windows.Forms.Label();
            this.comAtividadePES = new System.Windows.Forms.ComboBox();
            this.lblAtividadePES = new System.Windows.Forms.Label();
            this.lblDescrição = new System.Windows.Forms.Label();
            this.txtDescri = new System.Windows.Forms.TextBox();
            this.grbQuantidade = new System.Windows.Forms.GroupBox();
            this.rabTOP = new System.Windows.Forms.RadioButton();
            this.rabTodos = new System.Windows.Forms.RadioButton();
            this.nupQtResultados = new System.Windows.Forms.NumericUpDown();
            this.grbOrganização = new System.Windows.Forms.GroupBox();
            this.rabAlfabetico = new System.Windows.Forms.RadioButton();
            this.rabNumerico = new System.Windows.Forms.RadioButton();
            this.Dgv_Pesquisa = new System.Windows.Forms.DataGridView();
            this.dgvcPesCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcPesDescricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcPesCpfCnpj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcPesFantasia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panDownPesq = new System.Windows.Forms.Panel();
            this.cheFiltrosPES = new System.Windows.Forms.CheckBox();
            this.txtQtSelectPES = new System.Windows.Forms.TextBox();
            this.lblSelectPES = new System.Windows.Forms.Label();
            this.panButtonsDownAb1 = new System.Windows.Forms.Panel();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.panPrinPesq.SuspendLayout();
            this.panUpPesq.SuspendLayout();
            this.grbFiltros.SuspendLayout();
            this.grbQuantidade.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupQtResultados)).BeginInit();
            this.grbOrganização.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Pesquisa)).BeginInit();
            this.panDownPesq.SuspendLayout();
            this.panButtonsDownAb1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panPrinPesq
            // 
            this.panPrinPesq.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panPrinPesq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPrinPesq.Controls.Add(this.panUpPesq);
            this.panPrinPesq.Controls.Add(this.panDownPesq);
            this.panPrinPesq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPrinPesq.Location = new System.Drawing.Point(0, 0);
            this.panPrinPesq.Name = "panPrinPesq";
            this.panPrinPesq.Size = new System.Drawing.Size(550, 398);
            this.panPrinPesq.TabIndex = 0;
            // 
            // panUpPesq
            // 
            this.panUpPesq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panUpPesq.Controls.Add(this.grbFiltros);
            this.panUpPesq.Controls.Add(this.grbQuantidade);
            this.panUpPesq.Controls.Add(this.grbOrganização);
            this.panUpPesq.Controls.Add(this.Dgv_Pesquisa);
            this.panUpPesq.Dock = System.Windows.Forms.DockStyle.Top;
            this.panUpPesq.Location = new System.Drawing.Point(0, 0);
            this.panUpPesq.Name = "panUpPesq";
            this.panUpPesq.Size = new System.Drawing.Size(548, 360);
            this.panUpPesq.TabIndex = 2;
            // 
            // grbFiltros
            // 
            this.grbFiltros.Controls.Add(this.btnAjuda);
            this.grbFiltros.Controls.Add(this.comModuloPES);
            this.grbFiltros.Controls.Add(this.lblModuloPES);
            this.grbFiltros.Controls.Add(this.comAtividadePES);
            this.grbFiltros.Controls.Add(this.lblAtividadePES);
            this.grbFiltros.Controls.Add(this.lblDescrição);
            this.grbFiltros.Controls.Add(this.txtDescri);
            this.grbFiltros.Location = new System.Drawing.Point(106, -3);
            this.grbFiltros.Name = "grbFiltros";
            this.grbFiltros.Size = new System.Drawing.Size(440, 124);
            this.grbFiltros.TabIndex = 89;
            this.grbFiltros.TabStop = false;
            // 
            // btnAjuda
            // 
            this.btnAjuda.Image = ((System.Drawing.Image)(resources.GetObject("btnAjuda.Image")));
            this.btnAjuda.Location = new System.Drawing.Point(386, 75);
            this.btnAjuda.Name = "btnAjuda";
            this.btnAjuda.Size = new System.Drawing.Size(45, 30);
            this.btnAjuda.TabIndex = 91;
            this.btnAjuda.TabStop = false;
            this.btnAjuda.UseVisualStyleBackColor = true;
            this.btnAjuda.Click += new System.EventHandler(this.btnAjuda_Click);
            // 
            // comModuloPES
            // 
            this.comModuloPES.BackColor = System.Drawing.Color.White;
            this.comModuloPES.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comModuloPES.ForeColor = System.Drawing.Color.Black;
            this.comModuloPES.FormattingEnabled = true;
            this.comModuloPES.Items.AddRange(new object[] {
            "TechSIS Free",
            "TechSIS Express",
            "TechSIS Business",
            "TechSIS Controle",
            "TechSIS Pró",
            "TODOS"});
            this.comModuloPES.Location = new System.Drawing.Point(172, 83);
            this.comModuloPES.Name = "comModuloPES";
            this.comModuloPES.Size = new System.Drawing.Size(158, 22);
            this.comModuloPES.TabIndex = 89;
            this.comModuloPES.TabStop = false;
            this.comModuloPES.SelectedIndexChanged += new System.EventHandler(this.comModuloPES_SelectedIndexChanged);
            // 
            // lblModuloPES
            // 
            this.lblModuloPES.AutoSize = true;
            this.lblModuloPES.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModuloPES.Location = new System.Drawing.Point(170, 70);
            this.lblModuloPES.Name = "lblModuloPES";
            this.lblModuloPES.Size = new System.Drawing.Size(126, 14);
            this.lblModuloPES.TabIndex = 90;
            this.lblModuloPES.Text = "MÓDULO DO SISTEMA";
            // 
            // comAtividadePES
            // 
            this.comAtividadePES.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comAtividadePES.FormattingEnabled = true;
            this.comAtividadePES.Items.AddRange(new object[] {
            "INDÚSTRIA",
            "COMÉRCIO",
            "AGRONEGÓCIO",
            "SERVIÇOS",
            "FARMÁCIA",
            "POSTO DE ABASTECIMENTO",
            "RESTAURANTE",
            "TODAS"});
            this.comAtividadePES.Location = new System.Drawing.Point(8, 83);
            this.comAtividadePES.Name = "comAtividadePES";
            this.comAtividadePES.Size = new System.Drawing.Size(158, 22);
            this.comAtividadePES.TabIndex = 87;
            this.comAtividadePES.SelectedIndexChanged += new System.EventHandler(this.comAtividadePES_SelectedIndexChanged);
            // 
            // lblAtividadePES
            // 
            this.lblAtividadePES.AutoSize = true;
            this.lblAtividadePES.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAtividadePES.Location = new System.Drawing.Point(6, 70);
            this.lblAtividadePES.Name = "lblAtividadePES";
            this.lblAtividadePES.Size = new System.Drawing.Size(70, 14);
            this.lblAtividadePES.TabIndex = 88;
            this.lblAtividadePES.Text = "ATIVIDADE";
            // 
            // lblDescrição
            // 
            this.lblDescrição.AutoSize = true;
            this.lblDescrição.ForeColor = System.Drawing.Color.Black;
            this.lblDescrição.Location = new System.Drawing.Point(6, 22);
            this.lblDescrição.Name = "lblDescrição";
            this.lblDescrição.Size = new System.Drawing.Size(168, 14);
            this.lblDescrição.TabIndex = 4;
            this.lblDescrição.Text = "DESCRIÇÃO DA EMPRESA..:";
            // 
            // txtDescri
            // 
            this.txtDescri.BackColor = System.Drawing.Color.White;
            this.txtDescri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescri.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescri.ForeColor = System.Drawing.Color.Black;
            this.txtDescri.Location = new System.Drawing.Point(9, 36);
            this.txtDescri.MaxLength = 55;
            this.txtDescri.Name = "txtDescri";
            this.txtDescri.Size = new System.Drawing.Size(423, 20);
            this.txtDescri.TabIndex = 3;
            this.txtDescri.TextChanged += new System.EventHandler(this.txtDescri_TextChanged);
            // 
            // grbQuantidade
            // 
            this.grbQuantidade.Controls.Add(this.rabTOP);
            this.grbQuantidade.Controls.Add(this.rabTodos);
            this.grbQuantidade.Controls.Add(this.nupQtResultados);
            this.grbQuantidade.Location = new System.Drawing.Point(1, 59);
            this.grbQuantidade.Name = "grbQuantidade";
            this.grbQuantidade.Size = new System.Drawing.Size(104, 62);
            this.grbQuantidade.TabIndex = 88;
            this.grbQuantidade.TabStop = false;
            this.grbQuantidade.Text = "Quantidade";
            // 
            // rabTOP
            // 
            this.rabTOP.AutoSize = true;
            this.rabTOP.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rabTOP.Location = new System.Drawing.Point(7, 38);
            this.rabTOP.Name = "rabTOP";
            this.rabTOP.Size = new System.Drawing.Size(46, 18);
            this.rabTOP.TabIndex = 2;
            this.rabTOP.Text = "TOP";
            this.rabTOP.UseVisualStyleBackColor = true;
            this.rabTOP.CheckedChanged += new System.EventHandler(this.rabTOP_CheckedChanged);
            // 
            // rabTodos
            // 
            this.rabTodos.AutoSize = true;
            this.rabTodos.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rabTodos.Location = new System.Drawing.Point(7, 17);
            this.rabTodos.Name = "rabTodos";
            this.rabTodos.Size = new System.Drawing.Size(60, 18);
            this.rabTodos.TabIndex = 1;
            this.rabTodos.Text = "Todos";
            this.rabTodos.UseVisualStyleBackColor = true;
            this.rabTodos.CheckedChanged += new System.EventHandler(this.rabTodos_CheckedChanged);
            // 
            // nupQtResultados
            // 
            this.nupQtResultados.Enabled = false;
            this.nupQtResultados.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nupQtResultados.Location = new System.Drawing.Point(52, 37);
            this.nupQtResultados.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nupQtResultados.Name = "nupQtResultados";
            this.nupQtResultados.ReadOnly = true;
            this.nupQtResultados.Size = new System.Drawing.Size(47, 20);
            this.nupQtResultados.TabIndex = 3;
            this.nupQtResultados.TabStop = false;
            this.nupQtResultados.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nupQtResultados.ValueChanged += new System.EventHandler(this.nupQtResultados_ValueChanged);
            // 
            // grbOrganização
            // 
            this.grbOrganização.Controls.Add(this.rabAlfabetico);
            this.grbOrganização.Controls.Add(this.rabNumerico);
            this.grbOrganização.ForeColor = System.Drawing.Color.Black;
            this.grbOrganização.Location = new System.Drawing.Point(1, -3);
            this.grbOrganização.Name = "grbOrganização";
            this.grbOrganização.Size = new System.Drawing.Size(104, 62);
            this.grbOrganização.TabIndex = 87;
            this.grbOrganização.TabStop = false;
            this.grbOrganização.Text = "Organização..:";
            // 
            // rabAlfabetico
            // 
            this.rabAlfabetico.AutoSize = true;
            this.rabAlfabetico.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rabAlfabetico.Location = new System.Drawing.Point(7, 38);
            this.rabAlfabetico.Name = "rabAlfabetico";
            this.rabAlfabetico.Size = new System.Drawing.Size(95, 18);
            this.rabAlfabetico.TabIndex = 1;
            this.rabAlfabetico.Text = "Alfabético";
            this.rabAlfabetico.UseVisualStyleBackColor = true;
            this.rabAlfabetico.CheckedChanged += new System.EventHandler(this.rabAlfabetico_CheckedChanged);
            // 
            // rabNumerico
            // 
            this.rabNumerico.AutoSize = true;
            this.rabNumerico.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rabNumerico.Location = new System.Drawing.Point(7, 17);
            this.rabNumerico.Name = "rabNumerico";
            this.rabNumerico.Size = new System.Drawing.Size(81, 18);
            this.rabNumerico.TabIndex = 0;
            this.rabNumerico.Text = "Númerico";
            this.rabNumerico.UseVisualStyleBackColor = true;
            this.rabNumerico.CheckedChanged += new System.EventHandler(this.rabNumerico_CheckedChanged);
            // 
            // Dgv_Pesquisa
            // 
            this.Dgv_Pesquisa.AllowUserToAddRows = false;
            this.Dgv_Pesquisa.AllowUserToDeleteRows = false;
            this.Dgv_Pesquisa.AllowUserToResizeRows = false;
            this.Dgv_Pesquisa.BackgroundColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Dgv_Pesquisa.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Dgv_Pesquisa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Pesquisa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcPesCodigo,
            this.dgvcPesDescricao,
            this.dgvcPesCpfCnpj,
            this.dgvcPesFantasia});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.ForestGreen;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Pesquisa.DefaultCellStyle = dataGridViewCellStyle2;
            this.Dgv_Pesquisa.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Dgv_Pesquisa.GridColor = System.Drawing.Color.Black;
            this.Dgv_Pesquisa.Location = new System.Drawing.Point(0, 121);
            this.Dgv_Pesquisa.Name = "Dgv_Pesquisa";
            this.Dgv_Pesquisa.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Dgv_Pesquisa.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Dgv_Pesquisa.RowHeadersVisible = false;
            this.Dgv_Pesquisa.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.Dgv_Pesquisa.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Dgv_Pesquisa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_Pesquisa.Size = new System.Drawing.Size(546, 237);
            this.Dgv_Pesquisa.TabIndex = 86;
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
            this.dgvcPesCodigo.Width = 80;
            // 
            // dgvcPesDescricao
            // 
            this.dgvcPesDescricao.HeaderText = "Descrição da Empresa";
            this.dgvcPesDescricao.Name = "dgvcPesDescricao";
            this.dgvcPesDescricao.ReadOnly = true;
            this.dgvcPesDescricao.Width = 300;
            // 
            // dgvcPesCpfCnpj
            // 
            this.dgvcPesCpfCnpj.HeaderText = "CPF.CNPJ";
            this.dgvcPesCpfCnpj.Name = "dgvcPesCpfCnpj";
            this.dgvcPesCpfCnpj.ReadOnly = true;
            this.dgvcPesCpfCnpj.Width = 150;
            // 
            // dgvcPesFantasia
            // 
            this.dgvcPesFantasia.HeaderText = "Nome Fantasia";
            this.dgvcPesFantasia.Name = "dgvcPesFantasia";
            this.dgvcPesFantasia.ReadOnly = true;
            this.dgvcPesFantasia.Width = 320;
            // 
            // panDownPesq
            // 
            this.panDownPesq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panDownPesq.Controls.Add(this.cheFiltrosPES);
            this.panDownPesq.Controls.Add(this.txtQtSelectPES);
            this.panDownPesq.Controls.Add(this.lblSelectPES);
            this.panDownPesq.Controls.Add(this.panButtonsDownAb1);
            this.panDownPesq.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panDownPesq.Location = new System.Drawing.Point(0, 359);
            this.panDownPesq.Name = "panDownPesq";
            this.panDownPesq.Size = new System.Drawing.Size(548, 37);
            this.panDownPesq.TabIndex = 1;
            // 
            // cheFiltrosPES
            // 
            this.cheFiltrosPES.AutoSize = true;
            this.cheFiltrosPES.Location = new System.Drawing.Point(178, 12);
            this.cheFiltrosPES.Name = "cheFiltrosPES";
            this.cheFiltrosPES.Size = new System.Drawing.Size(131, 18);
            this.cheFiltrosPES.TabIndex = 84;
            this.cheFiltrosPES.TabStop = false;
            this.cheFiltrosPES.Text = "LEMBRAR FILTROS";
            this.cheFiltrosPES.UseVisualStyleBackColor = true;
            // 
            // txtQtSelectPES
            // 
            this.txtQtSelectPES.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtQtSelectPES.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQtSelectPES.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtQtSelectPES.Location = new System.Drawing.Point(65, 10);
            this.txtQtSelectPES.Name = "txtQtSelectPES";
            this.txtQtSelectPES.Size = new System.Drawing.Size(71, 20);
            this.txtQtSelectPES.TabIndex = 83;
            this.txtQtSelectPES.TabStop = false;
            this.txtQtSelectPES.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSelectPES
            // 
            this.lblSelectPES.AutoSize = true;
            this.lblSelectPES.Location = new System.Drawing.Point(3, 16);
            this.lblSelectPES.Name = "lblSelectPES";
            this.lblSelectPES.Size = new System.Drawing.Size(63, 14);
            this.lblSelectPES.TabIndex = 82;
            this.lblSelectPES.Text = "SELECT.:";
            // 
            // panButtonsDownAb1
            // 
            this.panButtonsDownAb1.BackColor = System.Drawing.SystemColors.Control;
            this.panButtonsDownAb1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panButtonsDownAb1.Controls.Add(this.btnPesquisar);
            this.panButtonsDownAb1.Controls.Add(this.btnFechar);
            this.panButtonsDownAb1.Location = new System.Drawing.Point(324, -1);
            this.panButtonsDownAb1.Name = "panButtonsDownAb1";
            this.panButtonsDownAb1.Size = new System.Drawing.Size(223, 37);
            this.panButtonsDownAb1.TabIndex = 6;
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisar.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisar.Image")));
            this.btnPesquisar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisar.Location = new System.Drawing.Point(6, 3);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(110, 29);
            this.btnPesquisar.TabIndex = 1;
            this.btnPesquisar.Text = "Pesquisar - F1";
            this.btnPesquisar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisar.UseVisualStyleBackColor = true;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFechar.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar.Image = ((System.Drawing.Image)(resources.GetObject("btnFechar.Image")));
            this.btnFechar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFechar.Location = new System.Drawing.Point(115, 3);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(101, 29);
            this.btnFechar.TabIndex = 3;
            this.btnFechar.Text = "&Fechar - F7";
            this.btnFechar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // PesEmpre
            // 
            this.AcceptButton = this.btnPesquisar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnFechar;
            this.ClientSize = new System.Drawing.Size(550, 398);
            this.Controls.Add(this.panPrinPesq);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(566, 437);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(566, 437);
            this.Name = "PesEmpre";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechSIS INF - Pesquisa de Empresas                          PesEmpre.dll";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PesEmpre_FormClosing);
            this.Load += new System.EventHandler(this.PesEmpre_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PesEmpre_KeyDown);
            this.panPrinPesq.ResumeLayout(false);
            this.panUpPesq.ResumeLayout(false);
            this.grbFiltros.ResumeLayout(false);
            this.grbFiltros.PerformLayout();
            this.grbQuantidade.ResumeLayout(false);
            this.grbQuantidade.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupQtResultados)).EndInit();
            this.grbOrganização.ResumeLayout(false);
            this.grbOrganização.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Pesquisa)).EndInit();
            this.panDownPesq.ResumeLayout(false);
            this.panDownPesq.PerformLayout();
            this.panButtonsDownAb1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panPrinPesq;
        private System.Windows.Forms.Panel panUpPesq;
        private System.Windows.Forms.Panel panDownPesq;
        public System.Windows.Forms.Panel panButtonsDownAb1;
        public System.Windows.Forms.Button btnPesquisar;
        public System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.CheckBox cheFiltrosPES;
        private System.Windows.Forms.TextBox txtQtSelectPES;
        private System.Windows.Forms.Label lblSelectPES;
        private System.Windows.Forms.DataGridView Dgv_Pesquisa;
        private System.Windows.Forms.GroupBox grbQuantidade;
        private System.Windows.Forms.RadioButton rabTOP;
        private System.Windows.Forms.RadioButton rabTodos;
        private System.Windows.Forms.NumericUpDown nupQtResultados;
        private System.Windows.Forms.GroupBox grbOrganização;
        private System.Windows.Forms.RadioButton rabAlfabetico;
        private System.Windows.Forms.RadioButton rabNumerico;
        private System.Windows.Forms.GroupBox grbFiltros;
        private System.Windows.Forms.Label lblDescrição;
        private System.Windows.Forms.TextBox txtDescri;
        private System.Windows.Forms.ComboBox comAtividadePES;
        public System.Windows.Forms.Label lblAtividadePES;
        private System.Windows.Forms.ComboBox comModuloPES;
        public System.Windows.Forms.Label lblModuloPES;
        private System.Windows.Forms.Button btnAjuda;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcPesCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcPesDescricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcPesCpfCnpj;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcPesFantasia;
    }
}