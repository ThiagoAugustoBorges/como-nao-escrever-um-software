using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Xml;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TechSIS_BWK
{
    internal partial class TechSIS_BWK : Form
    {
        public TechSIS_BWK()
        {
            InitializeComponent();
        }


        


        public string _Login_UsuarioID         
        { get; set; }
        public string _Login_LojaID            
        { get; set; }
        public string _Login_UsuarioDescri     
        { get; set; }
        public string _Login_LojaDescri        
        { get; set; }
        public string _Login_MODULO
        { get; set; }
        public StreamReader SEC_F_EMUso        
        {
            get;
            set;
        }
        public StreamReader DadosServidor_EMUso
        {
            get;
            set;
        }


        //LOAD DO FORMULÁRIO
        private void TechSIS_BWK_Load(object sender, EventArgs e)
        {
            TechSIS_MET MET = new TechSIS_MET();

            //POPULA A IMAGEM DE FUNDO DO FORMULÁRIO
            MET.MET_VerificaImagemFundo(this);
            //POPULA AS INFORMAÇÕES DO SERVIDOR
            MET.MET_VerificaServidorETC(NomeServidorStriplbl, NomeBancoStriplbl, EServidorResStriplbl);
            //DEIXA O ARQUIVO EM USO
            DadosServidor_EMUso = new StreamReader(@"..\Conexão\DadosServidor.xml");
        }

        //SHOW DO FORMULÁRIO
        public void TechSIS_BWK_Shown(object sender, EventArgs e)
        {
            //ABRE A TELA DE LOGIN
            TechSIS_MET MET = new TechSIS_MET();
            TechSIS_LoginMET LogMET = new TechSIS_LoginMET();

            TechSIS_LoginSIS Login = new TechSIS_LoginSIS();
            Login.Owner = this;
            DialogResult TIPO = Login.ShowDialog();


            if (TIPO != System.Windows.Forms.DialogResult.Cancel)
            {
                _Login_LojaID = Login.Login_LojaID;
                _Login_UsuarioID = Login.Login_UsuarioID;
                _Login_UsuarioDescri = Login.Login_UsuarioDescri;
                _Login_LojaDescri = Login.Login_LojaDesc;
                _Login_MODULO = MET.MET_SelecionaModuloSoftware(_Login_LojaID);


                OciosoTrueFalse = "False";


                //CAPTURA O TEMPO OCIOSO
                MET.MET_CapturarTempoOcioso(_Login_LojaID);
                Timer_Ocioso.AutoReset = true;
                Timer_Ocioso.Interval = MET.str_TimeINTERVAL;
                Timer_Ocioso.Enabled = true;
                Timer_Ocioso.Elapsed += new System.Timers.ElapsedEventHandler(MoveSprite);

                //VERIFICA SE O PAINEL É VISIVEL
                MET.MET_VerPanVISIVEL(Painel_Opcoes, _Login_LojaID, lblOpcoes);

                //SELECIONA A COR DE FUNDO DO FORMULÁRIO
                MET.MET_SelecionaCorFundo(Painel_Buttons, Painel_Buttons, Painel_Informações, Painel_Opcoes, Painel_Erro, MenuStrip_FORM, _Login_LojaID);
                //SELECIONA O MODULO E JOGA NO LABEL
                lblModulo.Text = _Login_MODULO;



                //POPULA AS INFORMAÇÕES NOS TEXTBOX EM BAIXO DO FORMULÁRIO
                lblNomUsuario.Text = _Login_UsuarioDescri;
                lblLoja.Text = _Login_LojaDescri;
                lblCodigoUsuarioDENT.Text = _Login_UsuarioID.PadLeft(6, '0');


                //COLOCA O SEC_F EM USO
                SEC_F_EMUso = new StreamReader(@"SecF_" + _Login_LojaID + ".XML");


                //VERIFICA SE A LOJA DO SEC_F É REALMENTE A LOJA LOGADA
                bool LojaLoga = MET.SEC_F_VerificaSeNúmeroLegal(_Login_LojaID);
                if (!LojaLoga) { } else { return; }


                //COMPARA A UTF8 DO BANCO DE DADOS COM A DO ARQUIVO CRIPTOGRAFADO
                bool UTF8 = MET.SEC_F_VerificarUTF8(_Login_LojaID);
                if (!UTF8) { } else { return; }

                //VERIFICA OS BUTTONS DE ATALHO NO PANEL OPÇOES
                MET.MET_AtalhosPanelOpcoes(_Login_LojaID, btnAtalhoPan1, btnAtalhoPan2);

                //PEGA A VERSÃO DO BANCO NO SQL E JOGA NO LABEL
                VersaoBancoStriplblRES.Text = MET.MET_SelecionaVersaoBanco(_Login_LojaID);

                //VERIFICA SE ESTÁ NO MODO DE EMERGENCIA
                //MET.MET_VerHISTEmerge(_Login_LojaID);

                this.Text = "TechSIS INF - " + lblLoja.Text;

            }
        }


        //EVENTO DO TIMER DE OCIOSO
        #region TIMER, STRING DO TIMER, E O EVENTO DO TIMER
        System.Timers.Timer Timer_Ocioso = new System.Timers.Timer();
        string OciosoTrueFalse { get; set; }
        public void MoveSprite(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (OciosoTrueFalse == "False")
                {
                    Timer_Ocioso.Enabled = false;
                    UsuárioInativo Ina = new UsuárioInativo();
                    Ina.ShowDialog();
                    Application.Restart();
                }
            }
            finally
            {
                OciosoTrueFalse = "False";
            }
        }
        #endregion



        //DEFINE A PERMISSÃO DE ACESSO AO FORMULÁRIO
        //PASSA OS PARAMETROS DE INICIALIZAÇÃO (INCLUINDO A SENHA DA DLL)
        #region ABRE FORMULÁRIOS NO TOOL STRIP
        #region 01 - TABELAS DO SOFTWARE
        private void Tool_Chamar_Empresa_Click(object sender, EventArgs e)
        {
            bool Boolean = false;
            foreach (Form _AbrirModal in Application.OpenForms)
            {
                if (_AbrirModal is TabEmpre.TabEmpre)
                {
                    _AbrirModal.WindowState = FormWindowState.Normal;
                    _AbrirModal.Focus();
                    Boolean = true;
                }
            }
            if (!Boolean)
            {
                TabEmpre.TabEmpre Call = new TabEmpre.TabEmpre();
                TechSIS_MET MET = new TechSIS_MET();
                bool PermiAcess = MET.MET_VerificaPermissão("010100", _Login_UsuarioID);
                if (!PermiAcess) { } else { return; }
                Call._Login_UsuarioID_TabEmpre = _Login_UsuarioID;
                Call._Login_LojaID_TabEmpre = _Login_LojaID;
                Call.Owner = this;
                Call.Show();
            }
        }
        private void Tool_Chamar_Cidades_Click(object sender, EventArgs e)
        {
            bool Boolean = false;
            foreach (Form _AbrirModal in Application.OpenForms)
            {
                if (_AbrirModal.Name == "TabCidad" && _AbrirModal.WindowState == FormWindowState.Minimized)
                {
                    _AbrirModal.WindowState = FormWindowState.Normal;
                    _AbrirModal.Focus();
                    Boolean = true;
                }
            }
            if (!Boolean)
            {

                TabCidad.TabCidad_CALL Call = new TabCidad.TabCidad_CALL();
                TechSIS_MET MET = new TechSIS_MET();
                bool PermiAcess = MET.MET_VerificaPermissão("010200", _Login_UsuarioID);
                if (!PermiAcess) { } else { return; }
                Call._Login_CryptCode = _Login_LojaID;
                Call._Login_CryptDesc = _Login_UsuarioID;
                Call._FORM_PAI = this;
                Call._WenCrypt = "TabCidad3Wenemy3156!.350?°";
                Call.TabCidad_AUTORIZADO();
            }
        }
        private void Tool_Chamar_TabCfope_Click(object sender, EventArgs e)
        {
            bool Boolean = false;
            foreach (Form _AbrirModal in Application.OpenForms)
            {
                if (_AbrirModal.Name == "TabCfope" && _AbrirModal.WindowState == FormWindowState.Minimized)
                {
                    _AbrirModal.WindowState = FormWindowState.Normal;
                    _AbrirModal.Focus();
                    Boolean = true;
                }
            }
            if (!Boolean)
            {

                TabCfope.TabCfope_CALL Call = new TabCfope.TabCfope_CALL();
                TechSIS_MET MET = new TechSIS_MET();
                bool PermiAcess = MET.MET_VerificaPermissão("010300", _Login_UsuarioID);
                if (!PermiAcess) { } else { return; }
                Call._Login_CryptCode = _Login_LojaID;
                Call._Login_CryptDesc = _Login_UsuarioID;
                Call._FORM_PAI = this;
                Call._WenCrypt = "TabCfope3Wenemy3156!.350?°";
                Call.TabCfope_AUTORIZADO();
            }
        }
        private void Tool_Chamar_TabRotas_Click(object sender, EventArgs e)
        {
            bool Boolean = false;
            foreach (Form _AbrirModal in Application.OpenForms)
            {
                if (_AbrirModal.Name == "TabRotas" && _AbrirModal.WindowState == FormWindowState.Minimized)
                {
                    _AbrirModal.WindowState = FormWindowState.Normal;
                    _AbrirModal.Focus();
                    Boolean = true;
                }
            }
            if (!Boolean)
            {
                TabRotas.TabRotas_CALL Call = new TabRotas.TabRotas_CALL();
                TechSIS_MET MET = new TechSIS_MET();
                bool PermiAcess = MET.MET_VerificaPermissão("010400", _Login_UsuarioID);
                if (!PermiAcess) { } else { return; }
                Call._Login_CryptCode = _Login_LojaID;
                Call._Login_CryptDesc = _Login_UsuarioID;
                Call._FORM_PAI = this;
                Call._Login_Crypt01 = "2";
                Call._WenCrypt = "TabRotas18Wenemy3156!.350?°";
                Call.TabRotas_AUTORIZADO();
            }
        }
        private void Tool_Chamar_TabMsgNf_Click(object sender, EventArgs e)
        {
            bool Boolean = false;
            foreach (Form _AbrirModal in Application.OpenForms)
            {
                if (_AbrirModal.Name == "TabMsgNt" && _AbrirModal.WindowState == FormWindowState.Minimized)
                {
                    _AbrirModal.WindowState = FormWindowState.Normal;
                    _AbrirModal.Focus();
                    Boolean = true;
                }
            }
            if (!Boolean)
            {
                TabMsgNt.TabMsgNt_CALL Call = new TabMsgNt.TabMsgNt_CALL();
                TechSIS_MET MET = new TechSIS_MET();
                bool PermiAcess = MET.MET_VerificaPermissão("010500", _Login_UsuarioID);
                if (!PermiAcess) { } else { return; }
                Call._Login_CryptCode = _Login_LojaID;
                Call._Login_CryptDesc = _Login_UsuarioID;
                Call._FORM_PAI = this;
                Call._WenCrypt = "TabMsgNt13Wenemy3156!.350?°";
                Call.TabMsgNt_AUTORIZADO();
            }
        }
        private void Tool_Chamar_TabConve_Click(object sender, EventArgs e)
        {
            bool Boolean = false;
            foreach (Form _AbrirModal in Application.OpenForms)
            {
                if (_AbrirModal.Name == "TabConve" && _AbrirModal.WindowState == FormWindowState.Minimized)
                {
                    _AbrirModal.WindowState = FormWindowState.Normal;
                    _AbrirModal.Focus();
                    Boolean = true;
                }
            }
            if (!Boolean)
            {
                TabConve.TabConve_CALL Call = new TabConve.TabConve_CALL();
                TechSIS_MET MET = new TechSIS_MET();
                bool PermiAcess = MET.MET_VerificaPermissão("010600", _Login_UsuarioID);
                if (!PermiAcess) { } else { return; }
                Call._Login_CryptCode = _Login_LojaID;
                Call._Login_CryptDesc = _Login_UsuarioID;
                Call._FORM_PAI = this;
                Call._WenCrypt = "TabConve3Wenemy3156!.350?°";
                Call.TabConve_AUTORIZADO();
            }
        }
        private void Tool_Chamar_TabSetor_Click(object sender, EventArgs e)
        {
            bool Boolean = false;
            foreach (Form _AbrirModal in Application.OpenForms)
            {
                if (_AbrirModal.Name == "TabSetor" && _AbrirModal.WindowState == FormWindowState.Minimized)
                {
                    _AbrirModal.WindowState = FormWindowState.Normal;
                    _AbrirModal.Focus();
                    Boolean = true;
                }
            }
            if (!Boolean)
            {
                TabSetor.TabSetor_CALL Call = new TabSetor.TabSetor_CALL();
                TechSIS_MET MET = new TechSIS_MET();
                bool PermiAcess = MET.MET_VerificaPermissão("010700", _Login_UsuarioID);
                if (!PermiAcess) { } else { return; }
                Call._Login_CryptCode = _Login_LojaID;
                Call._Login_CryptDesc = _Login_UsuarioID;
                Call._FORM_PAI = this;
                Call._WenCrypt = "TabSetor19Wenemy3156!.350?°";
                Call.TabSetor_AUTORIZADO();
            }
        }
        #endregion
        #region 02 - CADASTROS
        private void Tool_Chamar_Clientes_Click(object sender, EventArgs e)
        {
            bool Boolean = false;
            foreach (Form _AbrirModal in Application.OpenForms)
            {
                if (_AbrirModal.Name == "TabClien" && _AbrirModal.WindowState == FormWindowState.Minimized)
                {
                    _AbrirModal.WindowState = FormWindowState.Normal;
                    _AbrirModal.Focus();
                    Boolean = true;
                }
            }
            if (!Boolean)
            {
                TabClien.TabClien_CALL Call = new TabClien.TabClien_CALL();
                TechSIS_MET MET = new TechSIS_MET();
                bool PermiAcess = MET.MET_VerificaPermissão("020100", _Login_UsuarioID);
                if (!PermiAcess) { } else { return; }
                Call._Login_CryptCode = _Login_LojaID;
                Call._Login_CryptDesc = _Login_UsuarioID;
                Call._Login_Crypt01 = _Login_LojaDescri;
                Call._Login_Crypt02 = _Login_UsuarioDescri;
                Call._FORM_PAI = this;
                Call._WenCrypt = "TabClien3Wenemy3156!.350?°";
                Call.TabClien_AUTORIZADO();
            }
        }
        #endregion
        #region 08 - OPÇÕES ADICIONAIS
        private void Tool_Chamar_AplUsuaAt_Click(object sender, EventArgs e)
        {
            AplUsuaAt.AplUsuaAt_CALL Call = new AplUsuaAt.AplUsuaAt_CALL();
            Call._Login_CryptCode = _Login_LojaID;
            Call._Login_CryptDesc = _Login_UsuarioID;
            Call._Login_Crypt02 = _Login_UsuarioDescri;
            Call._FORM_PAI = this;
            Call._WenCrypt = "AplUsuaAt21Wenemy3156!.350?°";
            Call.AplUsuaAt_AUTORIZADO();
        }
        private void Tool_Chamar_OrdemRo_Click(object sender, EventArgs e)
        {
            bool Boolean = false;
            foreach (Form _AbrirModal in Application.OpenForms)
            {
                if (_AbrirModal.Name == "TabRotas" && _AbrirModal.WindowState == FormWindowState.Minimized)
                {
                    _AbrirModal.WindowState = FormWindowState.Normal;
                    _AbrirModal.Focus();
                    Boolean = true;
                }
            }
            if (!Boolean)
            {
                TabRotas.TabRotas_CALL Call = new TabRotas.TabRotas_CALL();
                TechSIS_MET MET = new TechSIS_MET();
                bool PermiAcess = MET.MET_VerificaPermissão("010400", _Login_UsuarioID);
                if (!PermiAcess) { } else { return; }
                Call._Login_CryptCode = _Login_LojaID;
                Call._Login_CryptDesc = _Login_UsuarioID;
                Call._FORM_PAI = this;
                Call._Login_Crypt01 = "1";
                Call._WenCrypt = "TabRotas18Wenemy3156!.350?°";
                Call.TabRotas_AUTORIZADO();
            }
        }
        #endregion
        #region 09 - GERENCIAMENTO
        private void Tool_Chamar_Usuarios_Click(object sender, EventArgs e)
        {
            bool Boolean = false;
            foreach (Form _AbrirModal in Application.OpenForms)
            {
                if (_AbrirModal.Name == "TabUsuar" && _AbrirModal.WindowState == FormWindowState.Minimized)
                {
                    _AbrirModal.WindowState = FormWindowState.Normal;
                    _AbrirModal.Focus();
                    Boolean = true;
                }
            }
            if (!Boolean)
            {
                TabUsuar.TabUsuar_CALL Call = new TabUsuar.TabUsuar_CALL();
                TechSIS_MET MET = new TechSIS_MET();
                bool PermiAcess = MET.MET_VerificaPermissão("090100", _Login_UsuarioID);
                if (!PermiAcess) { } else { return; }
                Call._Login_CryptCode = _Login_LojaID;
                Call._Login_CryptDesc = _Login_UsuarioID;
                Call._FORM_PAI = this;
                Call._WenCrypt = "TabUsuar21Wenemy3156!.350?°";
                Call.TabUsuar_AUTORIZADO();
            }
        }
        private void Tool_Chamar_Programas_Click(object sender, EventArgs e)
        {
            bool Boolean = false;
            foreach (Form _AbrirModal in Application.OpenForms)
            {
                if (_AbrirModal.Name == "TabProgr" && _AbrirModal.WindowState == FormWindowState.Minimized)
                {
                    _AbrirModal.WindowState = FormWindowState.Normal;
                    _AbrirModal.Focus();
                    Boolean = true;
                }
            }
            if (!Boolean)
            {
                TabProgr.TabProgr_CALL Call = new TabProgr.TabProgr_CALL();
                TechSIS_MET MET = new TechSIS_MET();
                bool PermiAcess = MET.MET_VerificaPermissão("090200", _Login_UsuarioID);
                if (!PermiAcess) { } else { return; }
                Call._Login_CryptCode = _Login_LojaID;
                Call._Login_CryptDesc = _Login_UsuarioID;
                Call._FORM_PAI = this;
                Call._WenCrypt = "TabProgr16Wenemy3156!.350?°";
                Call.TabProgr_AUTORIZADO();
            }
        }
        private void Tool_Chamar_Permissão_Click(object sender, EventArgs e)
        {
            bool Boolean = false;
            foreach (Form _AbrirModal in Application.OpenForms)
            {
                if (_AbrirModal.Name == "TabPermi" && _AbrirModal.WindowState == FormWindowState.Minimized)
                {
                    _AbrirModal.WindowState = FormWindowState.Normal;
                    _AbrirModal.Focus();
                    Boolean = true;
                }
            }
            if (!Boolean)
            {
                TabPermi.TabPermi_CALL Call = new TabPermi.TabPermi_CALL();
                TechSIS_MET MET = new TechSIS_MET();
                bool PermiAcess = MET.MET_VerificaPermissão("090300", _Login_UsuarioID);
                if (!PermiAcess) { } else { return; }
                Call._Login_CryptCode = _Login_LojaID;
                Call._Login_CryptDesc = _Login_UsuarioID;
                Call._FORM_PAI = this;
                Call._WenCrypt = "TabPermi16Wenemy3156!.350?°";
                Call.TabPermi_AUTORIZADO();
            }
        }
        private void Tool_Chamar_Lixeira_Click(object sender, EventArgs e)
        {
            //bool Boolean = false;
            //foreach (Form _AbrirModal in Application.OpenForms)
            //{
            //    if (_AbrirModal is TabProgr.TabProgr)
            //    {
            //        _AbrirModal.WindowState = FormWindowState.Normal;
            //        _AbrirModal.Focus();
            //        Boolean = true;
            //    }
            //}
            //if (!Boolean)
            //{
            //    AplLixeira.AplLixeira Call = new AplLixeira.AplLixeira();
            //    TechSIS_MET MET = new TechSIS_MET();
            //    bool PermiAcess = MET.MET_VerificaPermissão("090400", _Login_UsuarioID);
            //    if (!PermiAcess) { } else { return; }
            //    Call._Login_UsuarioID_AplLixeira = _Login_UsuarioID;
            //    Call._Login_LojaID_AplLixeira = _Login_LojaID;
            //    Call.Owner = this;
            //    Call.Show();
            //}
        }
        private void Tool_Chamar_GerenciamentoDeSenhas_Click(object sender, EventArgs e)
        {
            TabNewSe.TabNewSe_CALL Call = new TabNewSe.TabNewSe_CALL();
            Call._Login_CryptCode = _Login_LojaID;
            Call._Login_CryptDesc = _Login_UsuarioID;
            Call._Login_Crypt02 = _Login_UsuarioDescri;
            Call._FORM_PAI = this;
            Call._WenCrypt = "TabNewSe14Wenemy3156!.350?°";
            Call.TabNewSe_AUTORIZADO();
        }
        private void Tool_Chamar_ConfiSoftware_Click(object sender, EventArgs e)
        {
            TabConfg.TabConfg_CALL Call = new TabConfg.TabConfg_CALL();
            TechSIS_MET MET = new TechSIS_MET();
            MET.MET_VerificaPermissão("090400", _Login_UsuarioID);
            Call._Login_CryptCode = _Login_LojaID;
            Call._Login_CryptDesc = _Login_UsuarioID;
            Call._Login_Crypt02 = _Login_UsuarioDescri;
            Call.MenuStrip_FORM = MenuStrip_FORM;
            Call.Panel_Opcoes = Painel_Opcoes;
            Call.Panel_Buttons = Painel_Buttons;
            Call.Painel_Informações = Painel_Informações;
            Call.Painel_Erro = Painel_Erro;
            Call.Cor_BackUp = Painel_Erro.BackColor;
            Call.ATALHO1 = btnAtalhoPan1;
            Call.ATALHO2 = btnAtalhoPan2;
            Call._FORM_PAI = this;
            Call._WenCrypt = "TabConfg3Wenemy3156!.350?°";
            Call.TabConfg_AUTORIZADO();
        }
        private void Tool_Chamar_AddLojas_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("TechSIS_AddEmpre.exe"))
                {
                    string Argumentos = "1" + MenuStrip_FORM.BackColor.Name;


                    Process Proc = new Process();
                    Proc.StartInfo.FileName = "TechSIS_AddEmpre.exe";
                    Proc.StartInfo.CreateNoWindow = true;
                    Proc.StartInfo.Arguments = Argumentos;
                    Proc.Start();

                }
                else
                {
                    MessageBox.Show("Executável (TechSIS_AddEmpre.exe) não foi encontrado na pasta Debug", "TechSIS Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void Chamar_SOBRE_Click(object sender, EventArgs e)
        {
            Sobre_Form Sg = new Sobre_Form();
            Sg.ShowDialog();
        }
        #endregion
        #endregion


        //CHAMA O PerformClick DOS TOOL STRIP PARA OS ATALHOS NO MENU
        #region ATALHOS MENU PRINCIPAL
        private void Chamar_ATALHO_Clientes_Click(object sender, EventArgs e)
        {
            Tool_Chamar_TabClien.PerformClick();
        }
        #endregion

       



        //Desativa no MouseEnter as opções de programas do sistema
        #region Desatiba a opção pela Tabela de Programas
        //Desativa as Tabelas do Software
        private void TabelaDeSoftwares_MouseEnter(object sender, EventArgs e)
        {
            TechSIS_MET MET = new TechSIS_MET();
            try
            {
                MET.MET_ProgramaDisable(Tool_Chamar_TabEmpre, "010100");
                MET.MET_ProgramaDisable(Tool_Chamar_TabCidad, "010200");
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Erro.. Contate Suporte", Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Desativa as opções de Cadastros
        private void Cadastros_MouseEnter(object sender, EventArgs e)
        {
            TechSIS_MET MET = new TechSIS_MET();
            try
            {
                MET.MET_ProgramaDisable(Tool_Chamar_TabClien, "020100");
                MET.MET_ProgramaDisable(Tool_Chamar_TabForne, "020200");
                MET.MET_ProgramaDisable(Tool_Chamar_TabTrans, "020300");

                MET.MET_ProgramaDisable(ToolProdutos, "020400");
                MET.MET_ProgramaDisable(Tool_Chamar_TabPro01, "020401");
                MET.MET_ProgramaDisable(Tool_Chamar_TabPro02, "020402");
                MET.MET_ProgramaDisable(Tool_Chamar_TabPro03, "020403");

                MET.MET_ProgramaDisable(ToolServicos, "020500");
                MET.MET_ProgramaDisable(Tool_Chamar_TabSer01, "020501");
                MET.MET_ProgramaDisable(Tool_Chamar_TabSer02, "020502");

                MET.MET_ProgramaDisable(ToolServicos, "020500");
                MET.MET_ProgramaDisable(Tool_Chamar_TabSer01, "020501");
                MET.MET_ProgramaDisable(Tool_Chamar_TabSer02, "020502");

                MET.MET_ProgramaDisable(ToolMaquinario, "020600");
                MET.MET_ProgramaDisable(Tool_Chamar_TabMotor, "020601");
                MET.MET_ProgramaDisable(Tool_Chamar_TabOpera, "020602");
                MET.MET_ProgramaDisable(Tool_Chamar_TabMaqui, "020603");
                MET.MET_ProgramaDisable(Tool_Chamar_TabVeicu, "020604");
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Erro.. Contate Suporte", Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

       

        //Define a propriedades para configurar o modulo (TROCA A COR DO MODULO QUANDO PASSA O MOUSE)
        #region Propriedades MODULO
        private void Painel_Modulo_MouseEnter(object sender, EventArgs e)
        {
            Painel_Modulo.BackColor = Color.Red;
            lblModulo.LinkColor = Color.Yellow;
        }
        private void Painel_Modulo_MouseLeave(object sender, EventArgs e)
        {
            Painel_Modulo.BackColor = Color.Silver;
            lblModulo.LinkColor = Color.Blue;

        }
        private void lblModulo_MouseEnter(object sender, EventArgs e)
        {
            Painel_Modulo.BackColor = Color.Red;
            lblModulo.LinkColor = Color.Yellow;
        }
        private void lblModulo_MouseLeave(object sender, EventArgs e)
        {
            lblModulo.LinkColor = Color.Blue;
            Painel_Modulo.BackColor = Color.Silver;
        }

        private void lblModulo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MsgMódulo Mod = new MsgMódulo();
            Mod.ShowDialog();
        }

        private void Painel_Modulo_Click(object sender, EventArgs e)
        {
            MsgMódulo Mod = new MsgMódulo();
            Mod.ShowDialog();
        }
        #endregion




        //OPÇÕES DE PAINEL
        #region PAINEL - OCULTA PAINEL E MOVE O PAINEL
        //Oculta os paineis do form
        private void cheOcultar_CheckedChanged(object sender, EventArgs e)
        {
            if (cheOcultar.Checked == true)
            {
                cheOcultar.Text = "MOSTRAR PAINEL";
                Painel_Buttons.Visible = false;
                Painel_Opcoes.Visible = false;
                Painel_Erro.Visible = false;
                lblOpcoes.Text = "OPÇÕES";
            }
            if (cheOcultar.Checked == false)
            {
                cheOcultar.Text = "OCULTAR PAINEL";
                Painel_Buttons.Visible = true;
                Painel_Erro.Visible = true; ;
            }
        }
        #endregion


        //METODOS E EVENTOS REFERENTES AO BOTÃO DIREITO
        #region BOTÃO DIREITO METODO

        //ABRE O FORM DE TROCA DE IMAGEM DE FUNDO
        private void BotaoDireito_LocalizaImagemFundo_Click(object sender, EventArgs e)
        {
            ImagemFundo Fundo = new ImagemFundo();
            Fundo.LojaLogada = _Login_LojaID;
            Fundo.FORMULARIO = this;
            Fundo.Owner = this;
            Fundo.ShowDialog();
        }
        //ABRE O FORM DE TROCA DE IMAGEM DE FUNDO REL RPB
        private void BotaoDireito_ImagemFundoRPV_Click(object sender, EventArgs e)
        {
            ImagemFundo_REL Fundo = new ImagemFundo_REL();
            Fundo.Login_LojaID_Image = _Login_LojaID;
            Fundo.Owner = this;
            Fundo.ShowDialog();
        }
        //ABRE O FORM DE INFORMAÇÕES DA MAQUINA
        private void BotaoDireito_InfoMaquina_Click(object sender, EventArgs e)
        {
            InforMaquina Info = new InforMaquina();
            Info.LoginLoja = _Login_LojaID;
            Info.UserCod = _Login_UsuarioID;
            Info.UserDes = _Login_UsuarioDescri;
            Info.Owner = this;
            Info.ShowDialog();
        }
        //ABRE A CALCULADORA
        private void BotaoDireito_Calculadora_Click(object sender, EventArgs e)
        {
            Tool_Chamar_Calc.PerformClick();
        }

        //REFERENTE A PARTE DE EXIBIR
        #region BOTÃO DIREITO (EXIBIR)
        private void BotaoDireito_Exibir_MouseEnter(object sender, EventArgs e)
        {
            if (Painel_Buttons.Visible == true)
            {
                BotaoDireito_Exibir_Ferramentas.Checked = true;
            }
            else
            {
                BotaoDireito_Exibir_Ferramentas.Checked = false;
            }

            if (Painel_Opcoes.Visible == true)
            {
                BotaoDireito_Exibir_Opcoes.Checked = true;
            }
            else
            {
                BotaoDireito_Exibir_Opcoes.Checked = false;
            }
        }
        private void BotaoDireito_Exibir_Ferramentas_Click(object sender, EventArgs e)
        {
            if (BotaoDireito_Exibir_Ferramentas.Checked == true)
            {
                cheOcultar.Checked = true;
                BotaoDireito_Exibir_Ferramentas.Checked = false;
            }
            else
            {
                cheOcultar.Checked = false;
                BotaoDireito_Exibir_Ferramentas.Checked = true;
            }
        }
        private void BotaoDireito_Exibir_Opcoes_Click(object sender, EventArgs e)
        {
            if (BotaoDireito_Exibir_Opcoes.Checked == true)
            {
                Painel_Opcoes.Visible = false;
                BotaoDireito_Exibir_Opcoes.Checked = false;
                lblOpcoes.Text = "OPÇÕES";
            }
            else
            {
                Painel_Opcoes.Visible = true;
                BotaoDireito_Exibir_Opcoes.Checked = true;
                if (cheOcultar.Checked == true)
                {
                    cheOcultar.Checked = false;
                }
                lblOpcoes.Text = "OCULTAR";
            }
        }
        #endregion
        #endregion



        //BUTTONS SEM SER DE ATALHO
        #region BUTTONS AVULSOS
        //ABRE O FORMULÁRIO DE REPORTE
        private void btnReportar_Click(object sender, EventArgs e)
        {
            AplReport.AplReport_CALL Call = new AplReport.AplReport_CALL();
            Call._Login_CryptCode = _Login_LojaID;
            Call._Login_CryptDesc = _Login_UsuarioID;
            Call._FORM_PAI = this;
            Call._WenCrypt = "AplReport18Wenemy3156!.350?°";
            Call.AplReport_AUTORIZADO();
        }

        //ABRE O FORMULÁRIO DE BACKUP
        private void btnBackUp_Click(object sender, EventArgs e)
        {
            BackUp Back = new BackUp();
            Back.Servidor = EServidorResStriplbl.Text;
            Back.BancoDeDados = NomeBancoStriplbl.Text;
            Back.CodigoLoja = _Login_LojaID;
            Back.ShowDialog();
        }

        //ABRE O EXECUTÁVEL DE CONEXÃO REMOTA
        private void btnConexao_Click(object sender, EventArgs e)
        {
            Tool_Chamar_Conec.PerformClick();
        }


        //MUDA O USUÁRIO LOGADO
        void btnMudarUsuarioEXECUTA(object sender, EventArgs e)
        {
            try
            {
                lblLoja.Text = string.Empty; lblNomUsuario.Text = string.Empty; lblCodigoUsuarioDENT.Text = string.Empty;

                MenuStrip_FORM.BackColor = SystemColors.Control;
                Painel_Erro.BackColor = SystemColors.Control;
                Painel_Buttons.BackColor = SystemColors.Control;
                Painel_Informações.BackColor = SystemColors.Control;
                Painel_Opcoes.BackColor = SystemColors.Control;

                foreach (Form x in this.OwnedForms)
                {
                    x.Close();
                }

                SEC_F_EMUso.Close();
                TechSIS_BWK_Shown(sender, e);
            }
            catch (Exception)
            {
                MessageBox.Show("ERRO AO TROCAR DE USUÁRIO!", "TechSIS. ERRO NÃO TRATADO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnMudarUsuario_Click(object sender, EventArgs e)
        {
            TechSIS_MET MET = new TechSIS_MET();
            string PERGUNTA = MET.MET_PerguntaTrocarUsuCMD(_Login_LojaID);


            if (PERGUNTA == "SIM")
            {
                DialogResult Dialog = MessageBox.Show("Você deseja fazer a troca do usário " + _Login_UsuarioDescri + "?", "TechSIS Informação", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (Dialog == DialogResult.Yes)
                {
                    btnMudarUsuarioEXECUTA(sender, e);
                }
                if (Dialog == DialogResult.Cancel)
                {
                    MessageBox.Show("TechSIS Informação.: Para desativar esta pergunta de confirmação de troca de usuário, basta ir na opção 08.04.00 (Configuração do Software) e desmarcar a caixa de checagem 'PERGUNTAR SE DESEJA TROCAR DE USUÁRIO'. Dessa forma, toda vez em que o usuário logado clicar no botão de troca de usuário, o Software não irá mais perguntar se ele deseja fazer a ação desejada.\n\n TechSIS BWK Auto-Manutenção", "TechSIS BWK Auto-Manutenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                btnMudarUsuarioEXECUTA(sender, e);
            }
        }

        //MOSTRA O PAINEL DE OPÇÕES
        private void btnOpcoes_Click(object sender, EventArgs e)
        {
            if (lblOpcoes.Text == "OPÇÕES")
            {
                Painel_Opcoes.Visible = true;
                lblOpcoes.Text = "OCULTAR";
                return;
            }
            if (lblOpcoes.Text == "OCULTAR")
            {
                Painel_Opcoes.Visible = false;
                lblOpcoes.Text = "OPÇÕES";
                return;
            }
        }

        //ABRE AS CONFIGURAÇÕES PELO BOTÃO DE ATALHO NO PAINEL
        private void btnConfig_Click(object sender, EventArgs e)
        {
            Tool_Chamar_TabConfg.PerformClick();
        }
        #endregion



        //ABRE OS ATALHOS
        #region ATALHOS - Conexão Remota\Notepad\Calculadora\Google\Bing\Reportar\Curtir Facebook
        private void Tool_Chamar_ConexãoRemota_Click(object sender, EventArgs e)
        {
            DialogResult Deseja = MessageBox.Show("DESEJA INICIAR UMA CONEXÃO REMOTA?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Deseja == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process.Start("..\\Debug\\TeamViewer.exe");
                }
                catch (Exception)
                {
                    MessageBox.Show("Executável TeamViewer.exe não encontrado", "TechSIS Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
 
            }
        }
        private void Tool_Chamar_Notepad_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("Notepad");
            }
            catch (Exception)
            {
                MessageBox.Show("Executável Notepad.exe não encontrado", "TechSIS Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Tool_Chamar_Calc_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("Calc");
            }
            catch (Exception)
            {
                MessageBox.Show("Executável Calc.exe não encontrado", "TechSIS Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Tool_Chamar_Google_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.com.br/");
        }
        private void Tool_Chamar_Bing_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://br.bing.com/");
        }
        private void Tool_Chamar_AplReport_Click(object sender, EventArgs e)
        {
            btnReportar.PerformClick();
        }
        private void Tool_Chamar_Curtir_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.facebook.com/softwarestechsis");
        }
        #endregion


        //VERIFICA USUÁRIO ATIVO\INATIVO
        #region Ocioso Ativo
        private void ToolStrip_MouseMove(object sender, MouseEventArgs e)
        {
            OciosoTrueFalse = "True";
        }

        private void MenuStrip_MouseMove(object sender, MouseEventArgs e)
        {
            OciosoTrueFalse = "True";
        }

        private void Painel_Buttons_MouseMove(object sender, MouseEventArgs e)
        {
            OciosoTrueFalse = "True";
        }
        #endregion


        //DEFINIÇÃO DOS BUTTONS DE ATALHO NO FORMULÁRIO
        #region ATALHOS
        //ATALHO USUARIO 1
        private void Chamar_ATALHO_ATALHO1_Click(object sender, EventArgs e)
        {
            TechSIS_MET MET = new TechSIS_MET();
            string ATA1 = MET.MET_AtalhosButtons("3", _Login_LojaID, _Login_UsuarioID);

            try
            {
                foreach (ToolStripMenuItem CABEÇALHO in MenuStrip_FORM.Items)
                {
                    foreach (ToolStripItem OPÇÕES in CABEÇALHO.DropDown.Items)
                    {
                        if (OPÇÕES.Name == "Tool_Chamar_" + ATA1)
                        {
                            OPÇÕES.PerformClick();
                            break;
                        }
                        if (OPÇÕES.GetType() == typeof(ToolStripMenuItem))
                        {
                            ToolStripMenuItem OP = (OPÇÕES as ToolStripMenuItem);
                            foreach (ToolStripItem NOVO in OP.DropDown.Items)
                            {
                                if (NOVO.Name == "Tool_Chamar_" + ATA1)
                                {
                                    NOVO.PerformClick();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }



        }
        //ATALHO USUARIO 2
        private void Chamar_ATALHO_ATALHO2_Click(object sender, EventArgs e)
        {
            TechSIS_MET MET = new TechSIS_MET();
            string ATA1 = MET.MET_AtalhosButtons("4", _Login_LojaID, _Login_UsuarioID);

            try
            {
                foreach (ToolStripMenuItem CABEÇALHO in MenuStrip_FORM.Items)
                {
                    foreach (ToolStripItem OPÇÕES in CABEÇALHO.DropDown.Items)
                    {
                        if (OPÇÕES.Name == "Tool_Chamar_" + ATA1)
                        {
                            OPÇÕES.PerformClick();
                            break;
                        }
                        if (OPÇÕES.GetType() == typeof(ToolStripMenuItem))
                        {
                            ToolStripMenuItem OP = (OPÇÕES as ToolStripMenuItem);
                            foreach (ToolStripItem NOVO in OP.DropDown.Items)
                            {
                                if (NOVO.Name == "Tool_Chamar_" + ATA1)
                                {
                                    NOVO.PerformClick();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        //BUTTONS DE ATALHO
        private void btnAtalhoPan1_Click(object sender, EventArgs e)
        {
            TechSIS_MET MET = new TechSIS_MET();
            string ATA1 = MET.MET_AtalhosButtons("1", _Login_LojaID, _Login_UsuarioID);

            try
            {
                foreach (ToolStripMenuItem CABEÇALHO in MenuStrip_FORM.Items)
                {
                    foreach (ToolStripItem OPÇÕES in CABEÇALHO.DropDown.Items)
                    {
                        if (OPÇÕES.Name == "Tool_Chamar_" + ATA1)
                        {
                            OPÇÕES.PerformClick();
                            break;
                        }
                        if (OPÇÕES.GetType() == typeof(ToolStripMenuItem))
                        {
                            ToolStripMenuItem OP = (OPÇÕES as ToolStripMenuItem);
                            foreach (ToolStripItem NOVO in OP.DropDown.Items)
                            {
                                if (NOVO.Name == "Tool_Chamar_" + ATA1)
                                {
                                    NOVO.PerformClick();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }


            
        }
        //BUTTONS DE ATALHO
        private void btnAtalhoPan2_Click(object sender, EventArgs e)
        {
            TechSIS_MET MET = new TechSIS_MET();
            string ATA2 = MET.MET_AtalhosButtons("2", _Login_LojaID, _Login_UsuarioID);

            try
            {
                foreach (ToolStripMenuItem CABEÇALHO in MenuStrip_FORM.Items)
                {
                    foreach (ToolStripItem OPÇÕES in CABEÇALHO.DropDown.Items)
                    {
                        if (OPÇÕES.Name == "Tool_Chamar_" + ATA2)
                        {
                            OPÇÕES.PerformClick();
                            break;
                        }
                        if (OPÇÕES.GetType() == typeof(ToolStripMenuItem))
                        {
                            ToolStripMenuItem OP = (OPÇÕES as ToolStripMenuItem);
                            foreach (ToolStripItem NOVO in OP.DropDown.Items)
                            {
                                if (NOVO.Name == "Tool_Chamar_" + ATA2)
                                {
                                    NOVO.PerformClick();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        #endregion



        private void Tool_Chamar_TabPro01_Click(object sender, EventArgs e)
        {
            MessageBox.Show("apertei");
        }

        private void Tool_Chamar_TabPro03_Click(object sender, EventArgs e)
        {
            MessageBox.Show("apertei");
        }

        private void Chamar_ATALHO_Produtos_Click(object sender, EventArgs e)
        {

        }
































        





































    }
}
