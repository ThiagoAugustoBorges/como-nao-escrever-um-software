using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TechSIS_BWK
{
    internal partial class TechSIS_LoginSIS : Form
    {
        public TechSIS_LoginSIS()
        {
            InitializeComponent();
        }



        public string Login_LojaID { get; set; }
        public string Login_LojaDesc { get; set; }

        public string Login_UsuarioID { get; set; }
        public string Login_UsuarioDescri { get; set; }


        public string WenFILE_PONTO_DE_VERIFICACAO { get; set; }


        //ARMAZENA A EMPRESA LOGADA PARA A VERIFICAÇÃO
        //DOS ARQUIVOS DE LIBERAÇÃO
        //DEPOIS NÃO É MAIS UTILIZADA
        private string EmpresaLogadaTemporaria { get; set; }




        //POPULA O COMBOBOX
        private void TechSIS_LoginSIS_Load(object sender, EventArgs e)
        {
            TechSIS_LoginMET LogMET = new TechSIS_LoginMET();
            LogMET.LOG_PreencheComboBoxComUsuarios(comUsuario);
            LogMET.LOG_XML_CapturaUsuario(comUsuario, comEmpresa, cheLembrar, nupCaixa);
        }
        //ATALHO NO TECLADO
        private void TechSIS_LoginSIS_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    btnLogar.PerformClick();
                    break;
                case Keys.F7:
                    btnSair.PerformClick();
                    break;
            }
        }


        //SELECIONA AS EMPRESAS E O ARQUIVO DE LIBERAÇÃO DO USUÁRIO SELECIONADO
        private void comUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            TechSIS_LoginMET LogMET = new TechSIS_LoginMET();
            LogMET.LOG_PreencheComboBoxComEmpresas(comUsuario, comEmpresa);
            txtSenha.Select(); txtSenha.SelectAll();
        }
        //VERIFICAÇÃO DO ARQUIVO DE LIBERAÇÃO
        private void comEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comEmpresa.SelectedIndex > -1)
            {
                #region CAPTURA A EMPRESA LOGADA TEMPORARIA
                try
                {
                    int UsuarioLogado = Convert.ToInt32(comUsuario.Text.Substring(0, 6));

                    if (UsuarioLogado != 1)
                    {
                        int EmpresaLogada = Convert.ToInt32(comEmpresa.Text.Substring(0, 6));
                        EmpresaLogadaTemporaria = EmpresaLogada.ToString("00");
                    }
                    else
                    {
                        EmpresaLogadaTemporaria = "01";
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("ERRO AO CAPTURAR (EMPRESA LOGADA TEMPORARIA)", "TechSIS ERRO FATAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.ExitThread();
                    return;
                }
                #endregion

                TechSIS_LoginMET.EmpresaLogada = EmpresaLogadaTemporaria;
                TechSIS_LoginMET LogMET = new TechSIS_LoginMET();
                #region REPASSO OS CONTROLES
                LogMET.btnLiberarUso = btnLiberarUso;
                LogMET.btnLogar = btnLogar;
                LogMET.FRM = this;
                LogMET.txtSenha = txtSenha;
                LogMET.btnMotivo = btnMotivo;
                LogMET.panSenha = panSenha;
                LogMET.txtLiberado = txtLiberado;
                LogMET.txtQtDias = txtQtDias;
                LogMET.lblLiberação = lblLiberação;
                #endregion

                //VERIFICO SE O ARQUIVO EXISTE
                WenFILE_PONTO_DE_VERIFICACAO = "VERIFICAÇÃO SE O ARQUIVO EXISTE";
                bool ArquivoExiste = LogMET.WenFILE_ArquivoExiste();
                if (!ArquivoExiste) { } else { return; }

                //VERIFICO SE O ARQUIVO É VALIDO
                WenFILE_PONTO_DE_VERIFICACAO = "VERIFICAÇÃO SE O ARQUIVO É VÁLIDO";
                bool ArquivoValido = LogMET.WenFILE_ArquivoValido();
                if (!ArquivoValido) { } else { return; }

                //VERIFICO SE A EMPRESA DO ARQUIVO É VALIDA
                WenFILE_PONTO_DE_VERIFICACAO = "VERIFICAÇÃO SE A EMPRESA DO ARQUIVO É VÁLIDA";
                bool EmpresaValida = LogMET.WenFILE_EmpresaValida();
                if (!EmpresaValida) { } else { return; }

                //VERIFICO SE O NOME DA MAQUINA E DO DOMINIO SÃO OS MESMOS DO ARQUIVO
                WenFILE_PONTO_DE_VERIFICACAO = "VERIFICAÇÃO DA INTEGRIDADE DO ARQUIVO DE LIBERAÇÃO";
                bool ArquivoComput = LogMET.WenFILE_ArquivoComput();
                if (!ArquivoComput) { } else { return; }

                //VERIFICO OS DIAS DE USO DO ARQUIVO
                WenFILE_PONTO_DE_VERIFICACAO = "VERIFICAÇÃO DA QT. DE DIAS DE USO DO ARQUIVO";
                bool DiasUsoRest = LogMET.WenFILE_DiasDeUsoRest();
                if (!DiasUsoRest) { } else { return; }

                //VERIFICO SE O ARQUIVO ESTÁ VENCIDO OU NÃO
                WenFILE_PONTO_DE_VERIFICACAO = "VERIFICAÇÃO FINAL DE VENCIMENTO";
                bool LibertVencida = LogMET.WenFILE_LibertVencida();
                if (!LibertVencida) { } else { return; }

                //VOLTO O FOCO PARA O TXT SENHA
                txtSenha.Select(); txtSenha.SelectAll();
            }
        }


        //SELECIONA O TXT DA SENHA NOVAMENTE
        private void cheLembrar_CheckedChanged(object sender, EventArgs e)
        {
            txtSenha.Select(); txtSenha.SelectAll();
        }
        private void nupCaixa_ValueChanged(object sender, EventArgs e)
        {
            txtSenha.Select(); txtSenha.SelectAll();
        }
        private void txtSenha_MouseDown(object sender, MouseEventArgs e)
        {
            txtSenha.Select(); txtSenha.SelectAll();
        }
        private void linkTeclado_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("osk");
            txtSenha.Select(); txtSenha.SelectAll();
        }




        //EXECUTA AS VERIFICAÇÕES ANTES DE LOGAR
        private void btnLogar_Click(object sender, EventArgs e)
        {
            if (comUsuario.SelectedIndex < 0)
            {
                comUsuario.DroppedDown = true;
                return;
            }

            #region DEFINE AS STRINGS - Empresa e Usuário CODIGO
            if (comEmpresa.Text != "ACESSO A EMPRESA MASTER PADRÃO (00001)")
            {
                Login_LojaID = Convert.ToInt32(comEmpresa.Text.Substring(0, 6)).ToString("00");
                Login_LojaDesc = comEmpresa.Text.Substring(9);
            }
            else
            {
                Login_LojaID = "01";
                Login_LojaDesc = "ACESSO A EMPRESA MASTER PADRÃO (00001)";
            }


            Login_UsuarioID = Convert.ToInt32(comUsuario.Text.Substring(0, 6)).ToString();
            Login_UsuarioDescri = comUsuario.Text.Substring(9);
            #endregion


            //VERIFICA SE O ARQUIVO SecF REFERENTE AO CÓDIGO DA LOJA EXISTE
            TechSIS_MET MET = new TechSIS_MET();
            TechSIS_LoginMET LogMET = new TechSIS_LoginMET();

            //VERIFICA SE O ARQUIVO SecF EXISTE
            bool EXISTELoj = MET.SEC_F_Verifica_SecF_EXISTE(Login_LojaID, txtSenha);
            if (!EXISTELoj)
            { }
            else
            { return; }

            //VERIFICA A AUTENTICIDADE DO ARQUIVO SecF
            bool Corromp = MET.SEC_F_Verifica_SecF_LEGAL(Login_LojaID, txtSenha);
            if (!Corromp)
            { }
            else
            { return; }

            //VERIFICA SE A SENHA DO USUÁRIO ESTÁ CORRETA
            bool Verif = LogMET.LOG_VerificaSenhaUsuario(Login_UsuarioID, txtSenha);
            if (!Verif)
            { }
            else
            { return; }

            //VERIFICA SE O USUÁRIO TEM PERMISSÃO NO CAIXA
            bool Return = LogMET.LOG_VerificaCaixaUsuario(Login_UsuarioID, nupCaixa, Login_LojaID, txtSenha);
            if (!Return)
            { }
            else
            { return; }

            //GRAVA O XML DE LEMBRAR
            LogMET.LOG_XML_GravaCapturaUsuario(Login_UsuarioID, Login_UsuarioDescri, Login_LojaID, Login_LojaDesc, comUsuario, comEmpresa, cheLembrar, nupCaixa);

            this.Opacity = 0;

            TechSIS_LoginSIS_Logando Log = new TechSIS_LoginSIS_Logando();
            Log.Login_LojaID = Login_LojaID;
            Log.ShowDialog();

            this.Close();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        //SAI DO SISTEMA
        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
        //ABRE O FORMULÁRIO DE LIBERAÇÃO
        private void btnLiberarUso_Click(object sender, EventArgs e)
        {
            int Empresa = comEmpresa.SelectedIndex;
            TechSIS_LoginMET LogMET = new TechSIS_LoginMET();

            //VERIFICO SE O ARQUIVO SecF EXISTE
            bool ArquivoExiste = LogMET.SecF_ArquivoExiste();
            if (!ArquivoExiste) { } else { return; }

            //VERIFICO SE O ARQUIVO SecF É VALIDO
            bool ArquivoValido = LogMET.SecF_ArquivoValido();
            if (!ArquivoValido) { } else { return; }

            //VERIFICO A EMPRESA INFORMADA NO ARQUIVO SecF
            bool ArquivoEmpresa = LogMET.SecF_ArquivoEmpresa();
            if (!ArquivoEmpresa) { } else { return; }


            this.Opacity = 0;
            TechSIS_LoginSIS_Liberacao LIBERT = new TechSIS_LoginSIS_Liberacao();
            LIBERT.EmpresaLogada = EmpresaLogadaTemporaria;
            LIBERT.ShowDialog();
            comEmpresa.SelectedIndex = -1;
            comEmpresa.SelectedIndex = Empresa;
            this.Opacity = 100;
        }
        //EXPLICA O MOTIVO DO BLOQUEIO
        private void btnMotivo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ocorreu uma exceção nas verificações de segurança do sistema.\nAlgumas dicas para evitar este tipo de problema são. \n\n\b Mantenha a data do computador atualizada. \n\b Não troque a pasta do sistema de diretório. \n\b Não tente editar arquivos criptografados. \n\b Não exclua arquivos no diretório raiz do executável principal. \n\b Não faça procedimentos de configuração por sua conta.\n\nO erro de verificação retornado pelo sistema foi\n(" + WenFILE_PONTO_DE_VERIFICACAO + ")", "TechSIS AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtSenha.Select(); txtSenha.SelectAll();
        }







    }
}