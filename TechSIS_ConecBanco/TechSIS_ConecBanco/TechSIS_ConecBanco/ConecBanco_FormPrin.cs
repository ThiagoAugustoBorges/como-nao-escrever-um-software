using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace TechSIS_ConecBanco
{
    internal partial class ConecBanco_FormPrin : Form
    {
        public ConecBanco_FormPrin()
        {
            InitializeComponent();
        }

        public ComboBox comInstancia { get; set; }
        public TextBox txtServidor { get; set; }
        public GroupBox grbDadosCapturados { get; set; }
        public ProgressBar proBar { get; set; }
        public Label lblInstalando { get; set; }
        public Label lblInformacao { get; set; }
        public Label lblTechCodigo { get; set; }
        public Label lblTechDescri { get; set; }

        public SqlConnection Conexão { get; set; }
        public string SenhaUsuarioSA { get; set; }



        //MODIFICA A COR DOS BUTTONS QUANDO O USUÁRIO PASSA O MOUSE
        //EM CIMA DOS CONTROLES
        private void btnNova_MouseEnter(object sender, EventArgs e)
        {
            btnNova.BackColor = Color.GreenYellow;
        }
        private void btnNova_MouseLeave(object sender, EventArgs e)
        {
            btnNova.BackColor = SystemColors.Control;
        }
        private void btnRest_MouseEnter(object sender, EventArgs e)
        {
            btnRest.BackColor = Color.GreenYellow;
        }
        private void btnRest_MouseLeave(object sender, EventArgs e)
        {
            btnRest.BackColor = SystemColors.Control;
        }


        //CRIO OS RADIO BUTTONS PARA A NOVA INSTALAÇÃO
        private void btnNova_Click(object sender, EventArgs e)
        {
            panDown.Visible = true;

            this.Controls.Remove(btnNova);
            this.Controls.Remove(btnRest);



            #region CRIA O rabServidor
            rabServidor.AutoSize = true;
            rabServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            rabServidor.Location = new System.Drawing.Point(101, 205);
            rabServidor.Name = "rabServidor";
            rabServidor.Size = new System.Drawing.Size(378, 29);
            rabServidor.TabIndex = 7;
            rabServidor.Text = "INSTALAÇÃO EM UM SERVIDOR";
            rabServidor.UseVisualStyleBackColor = true;
            rabServidor.Checked = true;
            #endregion
            #region CRIA O rabEstação
            rabEstacao.AutoSize = true;
            rabEstacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            rabEstacao.Location = new System.Drawing.Point(101, 267);
            rabEstacao.Name = "rabEstacao";
            rabEstacao.Size = new System.Drawing.Size(384, 29);
            rabEstacao.TabIndex = 8;
            rabEstacao.Text = "INSTALAÇÃO EM UMA ESTAÇÃO";
            rabEstacao.UseVisualStyleBackColor = true;
            #endregion

            rabServidor.Visible = true;
            rabEstacao.Visible = true;


            this.Controls.Add(rabServidor);
            this.Controls.Add(rabEstacao);

            btnAvancar.Select();
        }


        //DEIXO O PAINEL DO BUTÃO AVANÇAR INVISIVEL
        //INSTANCIO OS CONTROLES
        private void ConecBanco_FormPrin_Load(object sender, EventArgs e)
        {
            this.Focus();

            proBar = new ProgressBar();
            lblInstalando = new Label();
            lblInformacao = new Label();
            Conexão = new SqlConnection();
            lblTechCodigo = new Label();
            lblTechDescri = new Label();
            panDown.Visible = false;
        }


        ConecBanco_MET MET = new ConecBanco_MET();
        //REMOVO OS RADION BUTTONS
        //CRIO AS PROPRIEDADES DO txtServidor e do comInstancia
        //CRIO O LBL SERVIDOR E LBL INSTANCIA
        //CRIO O GROUP BOX PARA ARMAZENAR OS CONTROLES
        //DEFINO SE A INSTALAÇÃO É DE UM SERVIDOR OU DE UMA ESTAÇÃO
        private void btnAvancar_Click(object sender, EventArgs e)
        {
            #region INSTALAR
            if (btnAvancar.Text == "INSTALAR")
            {
                MET.txtServidor = txtServidor.Text;
                MET.comInstancia = comInstancia.Text;


                #region DEFINE CAMPO OS CAMPOS OBRIGATÓRIOS
                if (String.IsNullOrEmpty(txtServidor.Text))
                {
                    MessageBox.Show("Campo (Servidor) deve ser preenchido.", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtServidor.Select(); txtServidor.SelectAll();
                    return;
                }
                if (String.IsNullOrEmpty(comInstancia.Text))
                {
                    DialogResult Confirma = MessageBox.Show("Confirma a instalação para uma instância padrão?", "TechSIS Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (Confirma == System.Windows.Forms.DialogResult.No)
                    {
                        comInstancia.Select(); comInstancia.SelectAll();
                        return;
                    }
                }
                #endregion

                #region DEFINE OS ARQUIVOS NECESSÁRIOS PARA A INSTALAÇÃO
                bool Arqu = MET.Conec_VerificaArquNece();
                if (!Arqu) { } else { return; }
                #endregion

                this.Controls.Remove(grbDadosCapturados);


                #region PROPRIEDADES DO Label lblInstalando
                lblInstalando = new Label();
                lblInstalando.AutoSize = true;
                lblInstalando.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblInstalando.Location = new System.Drawing.Point(143, 196);
                lblInstalando.Name = "lblInstalando";
                lblInstalando.Size = new System.Drawing.Size(289, 33);
                Application.DoEvents();
                #endregion
                #region PROPRIEDADES DA PrograssBar
                proBar = new ProgressBar();
                proBar.Location = new System.Drawing.Point(23, 232);
                proBar.Name = "proBar";
                proBar.Size = new System.Drawing.Size(524, 35);
                #endregion
                #region PROPRIEDADES DO Label lblInformacao
                lblInformacao.AutoSize = true;
                lblInformacao.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblInformacao.Location = new System.Drawing.Point(20, 292);
                lblInformacao.Name = "lblInformacao";
                lblInformacao.Size = new System.Drawing.Size(14, 14);
                #endregion
                #region PROPRIEDADES DO Label TechCodigo
                lblTechCodigo.AutoSize = true;
                lblTechCodigo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblTechCodigo.ForeColor = System.Drawing.Color.Black;
                lblTechCodigo.Location = new System.Drawing.Point(20, 333);
                lblTechCodigo.Name = "lblTechCodigo";
                lblTechCodigo.Size = new System.Drawing.Size(91, 14);
                lblTechCodigo.TabIndex = 17;
                lblTechCodigo.Text = "CÓDIGO....: ";
                #endregion
                #region PROPRIEDADES DO Label TechDescri
                lblTechDescri.AutoSize = true;
                lblTechDescri.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblTechDescri.ForeColor = System.Drawing.Color.Black;
                lblTechDescri.Location = new System.Drawing.Point(20, 356);
                lblTechDescri.Name = "lblTechDescri";
                lblTechDescri.Size = new System.Drawing.Size(91, 14);
                lblTechDescri.TabIndex = 18;
                lblTechDescri.Text = "DESCRIÇÃO.: ";
                #endregion



                btnAvancar.Enabled = false;
                btnCancelar.Enabled = false;
                btnAjuda.Enabled = false;

                this.Controls.Add(lblInstalando);
                this.Controls.Add(proBar);
                this.Controls.Add(lblInformacao);
                Application.DoEvents();
                lblInstalando.Text = "INICIANDO........";
                lblInformacao.Text = " ";

                int TOTAL_DE_METODOS = 21;

                int NumeroDeMetodos = 1;
                proBar.Maximum = NumeroDeMetodos;
                proBar.Value++;

                this.UseWaitCursor = true;

                #region INSTALAÇÃO EM SERVIDOR
                if (rabServidor.Checked == true)
                {
                    #region MÉTODO 1 - FAZ A CONEXÃO COM O SQL
                    NumeroDeMetodos++;
                    Application.DoEvents();
                    lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                    lblInformacao.Text = "REALIZANDO A PRIMEIRA CONEXÃO COM O SQL";
                    proBar.Maximum = NumeroDeMetodos;
                    proBar.Value++;

                    bool ConecSQL = MET.Conec_ConexãoSQLServer(Conexão);
                    if (!ConecSQL)
                    {

                    }
                    else
                    {
                        Dispose();
                        return; 
                    }

                    #endregion

                    #region MÉTODO 2 - MUDA O SQL PARA MODO MISTO
                    NumeroDeMetodos++;
                    Application.DoEvents();
                    lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                    lblInformacao.Text = "REALIZANDO A TROCA DO SQL PARA O MODO MISTO";
                    proBar.Maximum = NumeroDeMetodos;
                    proBar.Value++;

                    bool ChangeKey = MET.Conec_ChangeModoSQL(comInstancia, this, txtServidor);
                    if (!ChangeKey)
                    {

                    }
                    else
                    { 
                        Dispose(); return;
                    }

                    #endregion

                    #region MÉTODO 3 - MUDO A SENHA DO SQL SERVER
                    NumeroDeMetodos++;
                    Application.DoEvents();
                    lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                    lblInformacao.Text = "DEFININDO A SENHA DO USUÁRIO MASTER DO SISTEMA";
                    proBar.Maximum = NumeroDeMetodos;
                    proBar.Value++;

                    ConecBanco_FormSenhaSQL SenhaSQL = new ConecBanco_FormSenhaSQL();
                    SenhaSQL.TipoChamada = "1";
                    SenhaSQL.Owner = this;
                    SenhaSQL.ShowDialog();
                    SenhaUsuarioSA = SenhaSQL.Senha;

                    bool HabilUsuarSa = MET.Conec_HabilUsuarSaDefineSenha(Conexão, SenhaUsuarioSA);
                    if (!HabilUsuarSa)
                    {

                    }
                    else
                    {
                        Dispose(); 
                        return;
                    }

                    #endregion

                    #region MÉTODO 4 - DEFINE A PERMISSÃO sysadmin PARA OS USUÁRIOS DO SQL
                    NumeroDeMetodos++;
                    Application.DoEvents();
                    lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                    lblInformacao.Text = "INSERINDO PERMISSÃO sysadmin PARA USUÁRIOS DO WINDOWS";
                    proBar.Maximum = NumeroDeMetodos;
                    proBar.Value++;

                    bool PermiSQL = MET.Conec_PermissaoSQL(Conexão, SenhaUsuarioSA);
                    if (!PermiSQL)
                    {

                    }
                    else
                    {
                        Dispose(); return;
                    }
                    #endregion

                    #region MÉTODO 5 - CRIA UM BANCO DE DADOS NOVO
                    NumeroDeMetodos++;
                    Application.DoEvents();
                    lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                    lblInformacao.Text = "CRIANDO O BANCO DE DADOS TechSIS_INF";
                    proBar.Maximum = NumeroDeMetodos;
                    proBar.Value++;

                    bool CreateDatabase = MET.Conec_ConfigCreateDatabase(Conexão);
                    if (!CreateDatabase)
                    {

                    }
                    else
                    {
                        Dispose(); return;
                    }
                    #endregion

                    #region MÉTODO 6 - CRIO OS SCRIPTS 1 E 2 PARA .BAT
                    NumeroDeMetodos++;
                    Application.DoEvents();
                    lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                    lblInformacao.Text = "CRIANDO OS PRIMEIROS SCRIPTS DO BANCO DE DADOS";
                    proBar.Maximum = NumeroDeMetodos;
                    proBar.Value++;

                    bool BAT_1 = MET.Conec_ConfigWriteBAT_1(SenhaUsuarioSA, txtServidor.Text, comInstancia.Text);
                    if (!BAT_1)
                    {

                    }
                    else
                    {
                        Dispose(); return;
                    }
                    #endregion

                    #region MÉTODO 7 - RODO OS .BAT 1 E 2
                    NumeroDeMetodos++;
                    Application.DoEvents();
                    lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                    lblInformacao.Text = "RODANDO OS PRIMEIROS SCRIPTS DO BANCO DE DADOS";
                    proBar.Maximum = NumeroDeMetodos;
                    proBar.Value++;

                    bool RODO_1 = MET.Conec_ConfigRodaScript_1();
                    if (!RODO_1)
                    {

                    }
                    else
                    {
                        Dispose(); return;
                    }
                    #endregion

                    #region MÉTODO 8 - INSERT DO USUÁRIO MASTER
                    NumeroDeMetodos++;
                    Application.DoEvents();
                    lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                    lblInformacao.Text = "INSERINDO USUÁRIO MASTER NO BANCO DE DADOS";
                    proBar.Maximum = NumeroDeMetodos;
                    proBar.Value++;

                    bool Insert_MASTER = MET.Conec_InsertMASTER(Conexão, SenhaUsuarioSA);
                    if (!Insert_MASTER)
                    {

                    }
                    else
                    {
                        Dispose(); return;
                    }
                    #endregion

                    #region MÉTODO 9 - INSERT A EMPRESA NO BANCO DE DADOS
                    NumeroDeMetodos++;
                    Application.DoEvents();
                    lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                    lblInformacao.Text = "INSERINDO EMPRESA PADRÃO NO BANCO DE DADOS";
                    proBar.Maximum = NumeroDeMetodos;
                    proBar.Value++;

                    bool Insert_EMPRESA = MET.Conec_InsertLOJA(Conexão);
                    if (!Insert_EMPRESA)
                    {

                    }
                    else
                    {
                        Dispose(); return;
                    }
                    #endregion

                    #region MÉTODO 9.1 - REMOVENDO NULOS
                    NumeroDeMetodos++;
                    Application.DoEvents();
                    lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                    lblInformacao.Text = "REMOVENDO VALORES NULOS DAS TABELAS";
                    proBar.Maximum = NumeroDeMetodos;
                    proBar.Value++;

                    bool RemoverNulo = MET.Conec_RemoverNuloEmpresa(Conexão);
                    if (!RemoverNulo)
                    {

                    }
                    else
                    {
                        Dispose(); return;
                    }
                    #endregion

                    #region MÉTODO 10 - INSERT A CONF. GERAL DA EMPRESA 1
                    NumeroDeMetodos++;
                    Application.DoEvents();
                    lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                    lblInformacao.Text = "INSERINDO CONFIGURAÇÃO GERAL NO BANCO DE DADOS";
                    proBar.Maximum = NumeroDeMetodos;
                    proBar.Value++;

                    bool Insert_CONFIG = MET.Conec_InsertConfig(Conexão);
                    if (!Insert_CONFIG)
                    {

                    }
                    else
                    {
                        Dispose(); return;
                    }
                    #endregion

                    #region MÉTODO 10.1 - REMOVENDO NULOS
                    NumeroDeMetodos++;
                    Application.DoEvents();
                    lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                    lblInformacao.Text = "REMOVENDO VALORES NULOS DAS TABELAS";
                    proBar.Maximum = NumeroDeMetodos;
                    proBar.Value++;

                    bool RemoverNuloConfig = MET.Conec_RemoverNuloConfig(Conexão);
                    if (!RemoverNuloConfig)
                    {

                    }
                    else
                    {
                        Dispose(); return;
                    }
                    #endregion

                    this.Controls.Add(lblTechCodigo);
                    this.Controls.Add(lblTechDescri);

                    #region MÉTODO 11 - INSERE AS INFORMAÇÕES DO ARQUIVO TECH 01 DE 03
                    NumeroDeMetodos++;
                    Application.DoEvents();
                    lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                    lblInformacao.Text = "INSERINDO MUNICÍPIOS - (01 de 03).tech";

                    //INSERE OS MUNICIPIOS A PARTIR DO ARQUIVO .TECH
                    bool Munic = MET.InsertTECH_MUNIC(Conexão, proBar, lblInformacao, lblTechCodigo, lblTechDescri);
                    if (!Munic)
                    { }
                    else
                    { Dispose(); return; }
                    #endregion

                    #region MÉTODO 12 - INSERE AS INFORMAÇÕES DO ARQUIVO TECH 02 DE 03
                    NumeroDeMetodos++;
                    Application.DoEvents();
                    lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                    lblInformacao.Text = "INSERINDO PAÍSES - (02 de 03).tech";

                    //INSERE OS PAISES A PARTIR DO ARQUIVO .TECH
                    bool Paise = MET.InsertTECH_PAISE(Conexão, proBar, lblInformacao, lblTechCodigo, lblTechDescri);
                    if (!Paise)
                    { }
                    else
                    { Dispose(); return; }
                    #endregion

                    #region MÉTODO 13 - INSERE AS INFORMAÇÕES DO ARQUIVO TECH 03 DE 03
                    NumeroDeMetodos++;
                    Application.DoEvents();
                    lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                    lblInformacao.Text = "INSERINDO PAÍSES - (03 de 03).tech";

                    //INSERE OS PAISES A PARTIR DO ARQUIVO .TECH
                    bool NCM = MET.InsertTECH_NCM(Conexão, proBar, lblInformacao, lblTechCodigo, lblTechDescri);
                    if (!NCM)
                    { }
                    else
                    { Dispose(); return; }
                    #endregion

                    this.Controls.Remove(lblTechCodigo);
                    this.Controls.Remove(lblTechDescri);

                    #region MÉTODO 14 - CRIO OS SCRIPTS 3 E 4 PARA .BAT
                    NumeroDeMetodos++;
                    Application.DoEvents();
                    lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                    lblInformacao.Text = "CRIANDO SCRIPTS SECUNDÁRIOS DO BANCO DE DADOS";
                    proBar.Maximum = NumeroDeMetodos;
                    proBar.Value = NumeroDeMetodos;

                    bool BAT_2 = MET.Conec_ConfigWriteBAT_2(SenhaUsuarioSA, txtServidor.Text, comInstancia.Text);
                    if (!BAT_2)
                    {

                    }
                    else
                    {
                        Dispose(); return;
                    }
                    #endregion

                    #region MÉTODO 15 - RODO OS .BAT 3 E 4
                    NumeroDeMetodos++;
                    Application.DoEvents();
                    lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                    lblInformacao.Text = "RODANDO OS SCRIPTS SECUNDÁRIOS DO BANCO DE DADOS";
                    proBar.Maximum = NumeroDeMetodos;
                    proBar.Value++;

                    bool RODO_2 = MET.Conec_ConfigRodaScript_2();
                    if (!RODO_2)
                    {

                    }
                    else
                    {
                        Dispose(); return;
                    }
                    #endregion

                }
                #endregion
                #region INSTALAÇÃO EM ESTAÇÃO
                else //rabServidor.Checked = true
                {
                    string Conec = "N";
                    while (Conec == "N")
                    {
                        ConecBanco_FormSenhaSQL SenhaSQL = new ConecBanco_FormSenhaSQL();
                        SenhaSQL.TipoChamada = "2";
                        SenhaSQL.Owner = this;
                        SenhaSQL.ShowDialog();
                        SenhaUsuarioSA = SenhaSQL.Senha;

                        #region MÉTODO 1 - FAZ A CONEXÃO COM O SQL
                        Application.DoEvents();
                        lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                        lblInformacao.Text = "REALIZANDO A PRIMEIRA CONEXÃO COM O SQL";
                        proBar.Maximum = 2;
                        proBar.Value = 1;

                        #region TENTO EXECUTAR A CONEXÃO COM O SQL
                        Conexão = new SqlConnection();
                        Conexão.ConnectionString = MET.Conec_StringDeConexãoUsuarioSA(SenhaUsuarioSA);

                        try
                        {
                            Conexão.Open();
                            Conec = "S";
                        }
                        catch (SqlException Ex)
                        {
                            DialogResult Ten = MessageBox.Show("NÃO FOI POSSÍVEL REALIZAR A CONEXÃO COM O BANCO!\nDESEJA TENTAR NOVAMENTE?", "TechSIS SQLErro.: " + Ex.Number, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (Ten == System.Windows.Forms.DialogResult.No)
                            {
                                Application.ExitThread();
                                return;
                            }
                        }
                        catch (Exception)
                        {
                            DialogResult Ten = MessageBox.Show("NÃO FOI POSSÍVEL REALIZAR A CONEXÃO COM O BANCO!\nDESEJA TENTAR NOVAMENTE?", "TechSIS Exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (Ten == System.Windows.Forms.DialogResult.No)
                            {
                                Application.ExitThread();
                                return;
                            }
                        }
                        finally
                        {
                            Conexão.Close();
                        }
                    }
                    #endregion

                    #endregion
                }
                #endregion

                this.UseWaitCursor = false;

                //COMUM PARA ESTAÇÃO E SERVIDOR
                #region MÉTODO 16 - CRIA A STRING DE CONEXÃO
                NumeroDeMetodos++;
                Application.DoEvents();
                lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                lblInformacao.Text = "CRIANDO A STRING DE CONEXÃO COM O SERVIDOR";
                proBar.Maximum = NumeroDeMetodos;
                proBar.Value++;

                bool StringConec = MET.Config_GravaXMLConexao(rabServidor, rabEstacao, txtServidor, comInstancia, SenhaUsuarioSA);
                if (!StringConec)
                {

                }
                else
                {
                    Dispose(); return;
                }
                #endregion
                #region MÉTODO 17 - CRIA ARQUIVO DE CONFIGURAÇÃO DO SERVIDOR - DadosServidor.xml
                NumeroDeMetodos++;
                Application.DoEvents();
                lblInstalando.Text = "Instalando (" + NumeroDeMetodos + " de " + TOTAL_DE_METODOS + ")";
                lblInformacao.Text = "CRIANDO ARQUIVO DE CONFIGURAÇÃO DO SERVIDOR";
                proBar.Maximum = NumeroDeMetodos;
                proBar.Value++;

                bool ArqConfig = MET.Config_GravaXMLInforma(rabServidor, rabEstacao, txtServidor, comInstancia, SenhaUsuarioSA);
                if (!ArqConfig)
                {

                }
                else
                {
                    Dispose(); return;
                }
                #endregion


                try
                {
                    MET.DeleteDirectory("..\\Scripts");

                    System.Diagnostics.ProcessStartInfo Processo = new System.Diagnostics.ProcessStartInfo();
                    Processo.UseShellExecute = true;
                    Processo.WorkingDirectory = "..\\Debug";
                    Processo.FileName = "TechSIS_BWK.exe";
                    Processo.Verb = "runas";

                    System.Diagnostics.Process.Start(Processo);
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("Falha na execução do último método do instalador.\nA falha pode afetar o funcionamento do sistema.\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Conec Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                Application.ExitThread();
            }
            #endregion

            #region AVANÇAR
            if (btnAvancar.Text == "AVANÇAR")
            {
                #region PROPRIEDADES comInstancia
                comInstancia = new ComboBox();
                comInstancia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                comInstancia.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                comInstancia.FormattingEnabled = true;
                comInstancia.Location = new System.Drawing.Point(124, 62);
                comInstancia.Name = "comInstancia";
                comInstancia.Size = new System.Drawing.Size(418, 25);
                
                #endregion
                #region PROPRIEDADES txtServidor
                txtServidor = new TextBox();
                txtServidor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                txtServidor.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                txtServidor.Location = new System.Drawing.Point(124, 37);
                txtServidor.Name = "txtServidor";
                txtServidor.Size = new System.Drawing.Size(418, 24);
                txtServidor.CharacterCasing = CharacterCasing.Upper;
                #endregion


                //VERIFICO TAMBÉM SE EXISTE INSTANCIA INSTALADA
                //NO CASO DA INSTALAÇÃO SER EM UM SERVIDOR
                if (rabServidor.Checked == true)
                {
                    //POPULO O COMBOBOX COM AS INSTANCIAS INSTALADAS
                    bool ComboBox = MET.Conec_PopuloComboBoxInstancias(comInstancia);
                    if (!ComboBox)
                    {
                        comInstancia.SelectedIndex = 0;
                    }
                    else
                    {
                        return;
                    }
                }


                #region CRIA LBL INSTANCIA E LBL SERVIDOR
                // 
                // lblInstancia
                // 
                Label lblInstancia = new Label();
                lblInstancia.AutoSize = true;
                lblInstancia.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblInstancia.Location = new System.Drawing.Point(2, 71);
                lblInstancia.Name = "lblInstancia";
                lblInstancia.Size = new System.Drawing.Size(125, 17);
                lblInstancia.TabIndex = 1;
                lblInstancia.Text = "INSTÂNCIA...:";
                // 
                // lblServidor
                // 
                Label lblServidor = new Label();
                lblServidor.AutoSize = true;
                lblServidor.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblServidor.Location = new System.Drawing.Point(2, 44);
                lblServidor.Name = "lblServidor";
                lblServidor.Size = new System.Drawing.Size(125, 17);
                lblServidor.TabIndex = 0;
                lblServidor.Text = "SERVIDOR....:";
                #endregion
                #region CRIA O GROUP BOX
                // 
                // grbDadosCapturados
                // 
                grbDadosCapturados = new GroupBox();
                grbDadosCapturados.Controls.Add(comInstancia);
                grbDadosCapturados.Controls.Add(txtServidor);
                grbDadosCapturados.Controls.Add(lblInstancia);
                grbDadosCapturados.Controls.Add(lblServidor);
                grbDadosCapturados.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                grbDadosCapturados.Location = new System.Drawing.Point(12, 181);
                grbDadosCapturados.Name = "grbDadosCapturados";
                grbDadosCapturados.Size = new System.Drawing.Size(552, 101);
                grbDadosCapturados.TabIndex = 16;
                grbDadosCapturados.TabStop = false;
                grbDadosCapturados.Text = "DADOS CAPTURADOS";
                grbDadosCapturados.Visible = true;
                #endregion


                this.Controls.Remove(rabServidor);
                this.Controls.Remove(rabEstacao);
                this.Controls.Add(grbDadosCapturados);


                if (rabServidor.Checked == true)
                {
                    txtServidor.Text = Environment.MachineName;
                    txtServidor.Enabled = false;
                }
                if (rabEstacao.Checked == true)
                {
                    txtServidor.Select();
                    txtServidor.Text = Environment.MachineName;
                    txtServidor.Enabled = true;
                    comInstancia.DropDownStyle = ComboBoxStyle.Simple;
                    comInstancia.Text = "SQLEXPRESS";
                    txtServidor.SelectAll();
                }

               
                btnAvancar.Text = "INSTALAR";
            }
            #endregion
        }


        //FECHA A INSTALAÇÃO
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult Fechar = MessageBox.Show("Deseja fechar a instalação e configuração do sistema?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Fechar == System.Windows.Forms.DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else
            {
 
            }
        }
        private void ConecBanco_FormPrin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (proBar.Value < 1)
            {
                DialogResult Fechar = MessageBox.Show("Deseja fechar a instalação e configuração do sistema?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Fechar == System.Windows.Forms.DialogResult.Yes)
                {
                    Application.ExitThread();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}