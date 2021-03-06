﻿namespace PesVende.cs
{
    partial class PesVende
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PesVende));
            this.panPrinPesq = new System.Windows.Forms.Panel();
            this.panUpPesq = new System.Windows.Forms.Panel();
            this.grbFiltros = new System.Windows.Forms.GroupBox();
            this.comContrato = new System.Windows.Forms.ComboBox();
            this.comComiss = new System.Windows.Forms.ComboBox();
            this.lblComissao = new System.Windows.Forms.Label();
            this.lblContrato = new System.Windows.Forms.Label();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.txtEmpresa = new System.Windows.Forms.TextBox();
            this.lblCpfCnpj = new System.Windows.Forms.Label();
            this.txtCpfCnpj = new System.Windows.Forms.TextBox();
            this.txtCidadeCod = new System.Windows.Forms.TextBox();
            this.lblFantasia = new System.Windows.Forms.Label();
            this.txtFantasia = new System.Windows.Forms.TextBox();
            this.comStatus = new System.Windows.Forms.ComboBox();
            this.lblSituação = new System.Windows.Forms.Label();
            this.lblCidade = new System.Windows.Forms.Label();
            this.lblDescri = new System.Windows.Forms.Label();
            this.txtCidadeDesc = new System.Windows.Forms.TextBox();
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
            this.dgvcPesDescri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcPesCpfCnpj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcPesFantasia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcPesStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panDownPesq = new System.Windows.Forms.Panel();
            this.cheFiltrosPES = new System.Windows.Forms.CheckBox();
            this.txtQtSelectPES = new System.Windows.Forms.TextBox();
            this.lblSelectPES = new System.Windows.Forms.Label();
            this.btnAjuda = new System.Windows.Forms.Button();
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
            this.panPrinPesq.Size = new System.Drawing.Size(589, 428);
            this.panPrinPesq.TabIndex = 3;
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
            this.panUpPesq.Size = new System.Drawing.Size(587, 389);
            this.panUpPesq.TabIndex = 2;
            // 
            // grbFiltros
            // 
            this.grbFiltros.Controls.Add(this.comContrato);
            this.grbFiltros.Controls.Add(this.comComiss);
            this.grbFiltros.Controls.Add(this.lblComissao);
            this.grbFiltros.Controls.Add(this.lblContrato);
            this.grbFiltros.Controls.Add(this.lblEmpresa);
            this.grbFiltros.Controls.Add(this.txtEmpresa);
            this.grbFiltros.Controls.Add(this.lblCpfCnpj);
            this.grbFiltros.Controls.Add(this.txtCpfCnpj);
            this.grbFiltros.Controls.Add(this.txtCidadeCod);
            this.grbFiltros.Controls.Add(this.lblFantasia);
            this.grbFiltros.Controls.Add(this.txtFantasia);
            this.grbFiltros.Controls.Add(this.comStatus);
            this.grbFiltros.Controls.Add(this.lblSituação);
            this.grbFiltros.Controls.Add(this.lblCidade);
            this.grbFiltros.Controls.Add(this.lblDescri);
            this.grbFiltros.Controls.Add(this.txtCidadeDesc);
            this.grbFiltros.Controls.Add(this.txtDescri);
            this.grbFiltros.Location = new System.Drawing.Point(106, -3);
            this.grbFiltros.Name = "grbFiltros";
            this.grbFiltros.Size = new System.Drawing.Size(478, 124);
            this.grbFiltros.TabIndex = 91;
            this.grbFiltros.TabStop = false;
            // 
            // comContrato
            // 
            this.comContrato.AccessibleName = "5";
            this.comContrato.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comContrato.FormattingEnabled = true;
            this.comContrato.Items.AddRange(new object[] {
            "Todos",
            "COM CONTRATO",
            "SEM CONTRATO"});
            this.comContrato.Location = new System.Drawing.Point(353, 97);
            this.comContrato.Name = "comContrato";
            this.comContrato.Size = new System.Drawing.Size(121, 22);
            this.comContrato.TabIndex = 8;
            this.comContrato.SelectedIndexChanged += new System.EventHandler(this.comContrato_SelectedIndexChanged);
            // 
            // comComiss
            // 
            this.comComiss.AccessibleName = "5";
            this.comComiss.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comComiss.FormattingEnabled = true;
            this.comComiss.Items.AddRange(new object[] {
            "Todas",
            "FATURAMENTO",
            "LIQUIDAÇÃO"});
            this.comComiss.Location = new System.Drawing.Point(144, 97);
            this.comComiss.Name = "comComiss";
            this.comComiss.Size = new System.Drawing.Size(203, 22);
            this.comComiss.TabIndex = 7;
            this.comComiss.SelectedIndexChanged += new System.EventHandler(this.comComiss_SelectedIndexChanged);
            // 
            // lblComissao
            // 
            this.lblComissao.AutoSize = true;
            this.lblComissao.ForeColor = System.Drawing.Color.Black;
            this.lblComissao.Location = new System.Drawing.Point(143, 84);
            this.lblComissao.Name = "lblComissao";
            this.lblComissao.Size = new System.Drawing.Size(133, 14);
            this.lblComissao.TabIndex = 30;
            this.lblComissao.Text = "TIPO DE COMISSÃO.:";
            // 
            // lblContrato
            // 
            this.lblContrato.AutoSize = true;
            this.lblContrato.ForeColor = System.Drawing.Color.Black;
            this.lblContrato.Location = new System.Drawing.Point(355, 84);
            this.lblContrato.Name = "lblContrato";
            this.lblContrato.Size = new System.Drawing.Size(77, 14);
            this.lblContrato.TabIndex = 28;
            this.lblContrato.Text = "CONTRATO.:";
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.AutoSize = true;
            this.lblEmpresa.ForeColor = System.Drawing.Color.Black;
            this.lblEmpresa.Location = new System.Drawing.Point(357, 47);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(56, 14);
            this.lblEmpresa.TabIndex = 26;
            this.lblEmpresa.Text = "EMPRESA";
            // 
            // txtEmpresa
            // 
            this.txtEmpresa.AccessibleName = "5";
            this.txtEmpresa.BackColor = System.Drawing.Color.PaleGreen;
            this.txtEmpresa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmpresa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmpresa.ForeColor = System.Drawing.Color.Black;
            this.txtEmpresa.Location = new System.Drawing.Point(353, 61);
            this.txtEmpresa.MaxLength = 55;
            this.txtEmpresa.Name = "txtEmpresa";
            this.txtEmpresa.Size = new System.Drawing.Size(65, 20);
            this.txtEmpresa.TabIndex = 5;
            this.txtEmpresa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEmpresa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmpresa_KeyDown);
            this.txtEmpresa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpresa_KeyPress);
            this.txtEmpresa.Leave += new System.EventHandler(this.txtEmpresa_Leave);
            this.txtEmpresa.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtEmpresa_MouseDown);
            this.txtEmpresa.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtEmpresa_PreviewKeyDown);
            // 
            // lblCpfCnpj
            // 
            this.lblCpfCnpj.AutoSize = true;
            this.lblCpfCnpj.ForeColor = System.Drawing.Color.Black;
            this.lblCpfCnpj.Location = new System.Drawing.Point(238, 47);
            this.lblCpfCnpj.Name = "lblCpfCnpj";
            this.lblCpfCnpj.Size = new System.Drawing.Size(63, 14);
            this.lblCpfCnpj.TabIndex = 23;
            this.lblCpfCnpj.Text = "CPF.CNPJ";
            // 
            // txtCpfCnpj
            // 
            this.txtCpfCnpj.BackColor = System.Drawing.Color.White;
            this.txtCpfCnpj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCpfCnpj.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCpfCnpj.ForeColor = System.Drawing.Color.Black;
            this.txtCpfCnpj.Location = new System.Drawing.Point(241, 61);
            this.txtCpfCnpj.MaxLength = 55;
            this.txtCpfCnpj.Name = "txtCpfCnpj";
            this.txtCpfCnpj.Size = new System.Drawing.Size(106, 20);
            this.txtCpfCnpj.TabIndex = 4;
            this.txtCpfCnpj.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCpfCnpj.TextChanged += new System.EventHandler(this.txtCpfCnpj_TextChanged);
            this.txtCpfCnpj.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCpfCnpj_KeyPress);
            this.txtCpfCnpj.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtCpfCnpj_MouseDown);
            // 
            // txtCidadeCod
            // 
            this.txtCidadeCod.BackColor = System.Drawing.Color.PaleGreen;
            this.txtCidadeCod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCidadeCod.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCidadeCod.ForeColor = System.Drawing.Color.Black;
            this.txtCidadeCod.Location = new System.Drawing.Point(4, 61);
            this.txtCidadeCod.MaxLength = 55;
            this.txtCidadeCod.Name = "txtCidadeCod";
            this.txtCidadeCod.Size = new System.Drawing.Size(60, 20);
            this.txtCidadeCod.TabIndex = 3;
            this.txtCidadeCod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCidadeCod.TextChanged += new System.EventHandler(this.txtCidadeCod_TextChanged);
            this.txtCidadeCod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCidadeCod_KeyDown);
            this.txtCidadeCod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCidadeCod_KeyPress);
            this.txtCidadeCod.Leave += new System.EventHandler(this.txtCidadeCod_Leave);
            this.txtCidadeCod.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtCidadeCod_MouseDown);
            this.txtCidadeCod.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtCidadeCod_PreviewKeyDown);
            // 
            // lblFantasia
            // 
            this.lblFantasia.AutoSize = true;
            this.lblFantasia.ForeColor = System.Drawing.Color.Black;
            this.lblFantasia.Location = new System.Drawing.Point(238, 12);
            this.lblFantasia.Name = "lblFantasia";
            this.lblFantasia.Size = new System.Drawing.Size(217, 14);
            this.lblFantasia.TabIndex = 20;
            this.lblFantasia.Text = "FANTASIA\\APELIDO DO VENDEDOR.:";
            // 
            // txtFantasia
            // 
            this.txtFantasia.BackColor = System.Drawing.Color.White;
            this.txtFantasia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFantasia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFantasia.ForeColor = System.Drawing.Color.Black;
            this.txtFantasia.Location = new System.Drawing.Point(241, 26);
            this.txtFantasia.MaxLength = 55;
            this.txtFantasia.Name = "txtFantasia";
            this.txtFantasia.Size = new System.Drawing.Size(233, 20);
            this.txtFantasia.TabIndex = 2;
            this.txtFantasia.TextChanged += new System.EventHandler(this.txtFantasia_TextChanged);
            this.txtFantasia.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtFantasia_MouseDown);
            // 
            // comStatus
            // 
            this.comStatus.AccessibleName = "5";
            this.comStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comStatus.FormattingEnabled = true;
            this.comStatus.Items.AddRange(new object[] {
            "Todas",
            "ATIVADAS",
            "INATIVADAS",
            "EXCLUIDAS"});
            this.comStatus.Location = new System.Drawing.Point(4, 97);
            this.comStatus.Name = "comStatus";
            this.comStatus.Size = new System.Drawing.Size(134, 22);
            this.comStatus.TabIndex = 6;
            this.comStatus.SelectedIndexChanged += new System.EventHandler(this.comStatus_SelectedIndexChanged);
            // 
            // lblSituação
            // 
            this.lblSituação.AutoSize = true;
            this.lblSituação.ForeColor = System.Drawing.Color.Black;
            this.lblSituação.Location = new System.Drawing.Point(3, 84);
            this.lblSituação.Name = "lblSituação";
            this.lblSituação.Size = new System.Drawing.Size(84, 14);
            this.lblSituação.TabIndex = 18;
            this.lblSituação.Text = "SITUAÇÃO..:";
            // 
            // lblCidade
            // 
            this.lblCidade.AutoSize = true;
            this.lblCidade.ForeColor = System.Drawing.Color.Black;
            this.lblCidade.Location = new System.Drawing.Point(1, 47);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Size = new System.Drawing.Size(182, 14);
            this.lblCidade.TabIndex = 15;
            this.lblCidade.Text = "CIDADE DO VENDEDOR......:";
            // 
            // lblDescri
            // 
            this.lblDescri.AutoSize = true;
            this.lblDescri.ForeColor = System.Drawing.Color.Black;
            this.lblDescri.Location = new System.Drawing.Point(1, 12);
            this.lblDescri.Name = "lblDescri";
            this.lblDescri.Size = new System.Drawing.Size(182, 14);
            this.lblDescri.TabIndex = 12;
            this.lblDescri.Text = "DESCRIÇÃO DO VENDEDOR...:";
            // 
            // txtCidadeDesc
            // 
            this.txtCidadeDesc.BackColor = System.Drawing.Color.White;
            this.txtCidadeDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCidadeDesc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCidadeDesc.Enabled = false;
            this.txtCidadeDesc.ForeColor = System.Drawing.Color.Black;
            this.txtCidadeDesc.Location = new System.Drawing.Point(64, 61);
            this.txtCidadeDesc.MaxLength = 55;
            this.txtCidadeDesc.Name = "txtCidadeDesc";
            this.txtCidadeDesc.Size = new System.Drawing.Size(173, 20);
            this.txtCidadeDesc.TabIndex = 13;
            // 
            // txtDescri
            // 
            this.txtDescri.BackColor = System.Drawing.Color.White;
            this.txtDescri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescri.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescri.ForeColor = System.Drawing.Color.Black;
            this.txtDescri.Location = new System.Drawing.Point(4, 26);
            this.txtDescri.MaxLength = 55;
            this.txtDescri.Name = "txtDescri";
            this.txtDescri.Size = new System.Drawing.Size(233, 20);
            this.txtDescri.TabIndex = 1;
            this.txtDescri.TextChanged += new System.EventHandler(this.txtDescri_TextChanged);
            this.txtDescri.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtDescri_MouseDown);
            // 
            // grbQuantidade
            // 
            this.grbQuantidade.Controls.Add(this.rabTOP);
            this.grbQuantidade.Controls.Add(this.rabTodos);
            this.grbQuantidade.Controls.Add(this.nupQtResultados);
            this.grbQuantidade.Location = new System.Drawing.Point(1, 59);
            this.grbQuantidade.Name = "grbQuantidade";
            this.grbQuantidade.Size = new System.Drawing.Size(104, 62);
            this.grbQuantidade.TabIndex = 90;
            this.grbQuantidade.TabStop = false;
            this.grbQuantidade.Text = "Quantidade";
            // 
            // rabTOP
            // 
            this.rabTOP.AccessibleName = "5";
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
            this.rabTodos.AccessibleName = "5";
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
            this.nupQtResultados.AccessibleName = "5";
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
            this.grbOrganização.TabIndex = 89;
            this.grbOrganização.TabStop = false;
            this.grbOrganização.Text = "Organização..:";
            // 
            // rabAlfabetico
            // 
            this.rabAlfabetico.AccessibleName = "5";
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
            this.rabNumerico.AccessibleName = "5";
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
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Dgv_Pesquisa.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.Dgv_Pesquisa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Pesquisa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcPesCodigo,
            this.dgvcPesDescri,
            this.dgvcPesCpfCnpj,
            this.dgvcPesFantasia,
            this.dgvcPesStatus});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.ForestGreen;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Pesquisa.DefaultCellStyle = dataGridViewCellStyle6;
            this.Dgv_Pesquisa.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Dgv_Pesquisa.GridColor = System.Drawing.Color.Black;
            this.Dgv_Pesquisa.Location = new System.Drawing.Point(0, 122);
            this.Dgv_Pesquisa.Name = "Dgv_Pesquisa";
            this.Dgv_Pesquisa.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Dgv_Pesquisa.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.Dgv_Pesquisa.RowHeadersVisible = false;
            this.Dgv_Pesquisa.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            this.Dgv_Pesquisa.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.Dgv_Pesquisa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_Pesquisa.Size = new System.Drawing.Size(585, 265);
            this.Dgv_Pesquisa.TabIndex = 87;
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
            this.dgvcPesCodigo.Width = 70;
            // 
            // dgvcPesDescri
            // 
            this.dgvcPesDescri.HeaderText = "Descrição da Transportadora";
            this.dgvcPesDescri.Name = "dgvcPesDescri";
            this.dgvcPesDescri.ReadOnly = true;
            this.dgvcPesDescri.Width = 400;
            // 
            // dgvcPesCpfCnpj
            // 
            this.dgvcPesCpfCnpj.HeaderText = "CPF.CNPJ";
            this.dgvcPesCpfCnpj.Name = "dgvcPesCpfCnpj";
            this.dgvcPesCpfCnpj.ReadOnly = true;
            this.dgvcPesCpfCnpj.Width = 140;
            // 
            // dgvcPesFantasia
            // 
            this.dgvcPesFantasia.HeaderText = "Fantasia";
            this.dgvcPesFantasia.Name = "dgvcPesFantasia";
            this.dgvcPesFantasia.ReadOnly = true;
            this.dgvcPesFantasia.Width = 450;
            // 
            // dgvcPesStatus
            // 
            this.dgvcPesStatus.HeaderText = "Situação";
            this.dgvcPesStatus.Name = "dgvcPesStatus";
            this.dgvcPesStatus.ReadOnly = true;
            this.dgvcPesStatus.Width = 120;
            // 
            // panDownPesq
            // 
            this.panDownPesq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panDownPesq.Controls.Add(this.cheFiltrosPES);
            this.panDownPesq.Controls.Add(this.txtQtSelectPES);
            this.panDownPesq.Controls.Add(this.lblSelectPES);
            this.panDownPesq.Controls.Add(this.btnAjuda);
            this.panDownPesq.Controls.Add(this.panButtonsDownAb1);
            this.panDownPesq.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panDownPesq.Location = new System.Drawing.Point(0, 389);
            this.panDownPesq.Name = "panDownPesq";
            this.panDownPesq.Size = new System.Drawing.Size(587, 37);
            this.panDownPesq.TabIndex = 0;
            // 
            // cheFiltrosPES
            // 
            this.cheFiltrosPES.AccessibleName = "5";
            this.cheFiltrosPES.AutoSize = true;
            this.cheFiltrosPES.Location = new System.Drawing.Point(225, 10);
            this.cheFiltrosPES.Name = "cheFiltrosPES";
            this.cheFiltrosPES.Size = new System.Drawing.Size(131, 18);
            this.cheFiltrosPES.TabIndex = 88;
            this.cheFiltrosPES.TabStop = false;
            this.cheFiltrosPES.Text = "LEMBRAR FILTROS";
            this.cheFiltrosPES.UseVisualStyleBackColor = true;
            // 
            // txtQtSelectPES
            // 
            this.txtQtSelectPES.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtQtSelectPES.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQtSelectPES.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtQtSelectPES.Location = new System.Drawing.Point(132, 8);
            this.txtQtSelectPES.Name = "txtQtSelectPES";
            this.txtQtSelectPES.Size = new System.Drawing.Size(71, 20);
            this.txtQtSelectPES.TabIndex = 87;
            this.txtQtSelectPES.TabStop = false;
            this.txtQtSelectPES.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSelectPES
            // 
            this.lblSelectPES.AutoSize = true;
            this.lblSelectPES.Location = new System.Drawing.Point(70, 14);
            this.lblSelectPES.Name = "lblSelectPES";
            this.lblSelectPES.Size = new System.Drawing.Size(63, 14);
            this.lblSelectPES.TabIndex = 86;
            this.lblSelectPES.Text = "SELECT.:";
            // 
            // btnAjuda
            // 
            this.btnAjuda.Image = ((System.Drawing.Image)(resources.GetObject("btnAjuda.Image")));
            this.btnAjuda.Location = new System.Drawing.Point(3, 2);
            this.btnAjuda.Name = "btnAjuda";
            this.btnAjuda.Size = new System.Drawing.Size(45, 31);
            this.btnAjuda.TabIndex = 17;
            this.btnAjuda.TabStop = false;
            this.btnAjuda.UseVisualStyleBackColor = true;
            this.btnAjuda.Click += new System.EventHandler(this.btnAjuda_Click);
            // 
            // panButtonsDownAb1
            // 
            this.panButtonsDownAb1.BackColor = System.Drawing.SystemColors.Control;
            this.panButtonsDownAb1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panButtonsDownAb1.Controls.Add(this.btnPesquisar);
            this.panButtonsDownAb1.Controls.Add(this.btnFechar);
            this.panButtonsDownAb1.Location = new System.Drawing.Point(363, -1);
            this.panButtonsDownAb1.Name = "panButtonsDownAb1";
            this.panButtonsDownAb1.Size = new System.Drawing.Size(223, 37);
            this.panButtonsDownAb1.TabIndex = 85;
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
            // PesVende
            // 
            this.AcceptButton = this.btnPesquisar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnFechar;
            this.ClientSize = new System.Drawing.Size(589, 428);
            this.Controls.Add(this.panPrinPesq);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(605, 467);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(605, 467);
            this.Name = "PesVende";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechSIS INF - Pesquisa de Vendedores                        PesVende.dll";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PesVende_FormClosing);
            this.Load += new System.EventHandler(this.PesVende_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PesVende_KeyDown);
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
        private System.Windows.Forms.GroupBox grbFiltros;
        private System.Windows.Forms.ComboBox comComiss;
        private System.Windows.Forms.Label lblComissao;
        private System.Windows.Forms.Button btnAjuda;
        private System.Windows.Forms.Label lblContrato;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.TextBox txtEmpresa;
        private System.Windows.Forms.Label lblCpfCnpj;
        private System.Windows.Forms.TextBox txtCpfCnpj;
        private System.Windows.Forms.TextBox txtCidadeCod;
        private System.Windows.Forms.Label lblFantasia;
        private System.Windows.Forms.TextBox txtFantasia;
        private System.Windows.Forms.ComboBox comStatus;
        private System.Windows.Forms.Label lblSituação;
        private System.Windows.Forms.Label lblCidade;
        private System.Windows.Forms.Label lblDescri;
        private System.Windows.Forms.TextBox txtCidadeDesc;
        private System.Windows.Forms.TextBox txtDescri;
        private System.Windows.Forms.GroupBox grbQuantidade;
        private System.Windows.Forms.RadioButton rabTOP;
        private System.Windows.Forms.RadioButton rabTodos;
        private System.Windows.Forms.NumericUpDown nupQtResultados;
        private System.Windows.Forms.GroupBox grbOrganização;
        private System.Windows.Forms.RadioButton rabAlfabetico;
        private System.Windows.Forms.RadioButton rabNumerico;
        private System.Windows.Forms.DataGridView Dgv_Pesquisa;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcPesCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcPesDescri;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcPesCpfCnpj;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcPesFantasia;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcPesStatus;
        private System.Windows.Forms.Panel panDownPesq;
        private System.Windows.Forms.CheckBox cheFiltrosPES;
        private System.Windows.Forms.TextBox txtQtSelectPES;
        private System.Windows.Forms.Label lblSelectPES;
        public System.Windows.Forms.Panel panButtonsDownAb1;
        public System.Windows.Forms.Button btnPesquisar;
        public System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.ComboBox comContrato;
    }
}