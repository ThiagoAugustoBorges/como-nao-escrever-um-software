using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TabNewSe
{
    internal partial class TabNewSe : Form
    {
        public TabNewSe()
        {
            InitializeComponent();
        }

        public string _Login_LojaID_Senhas { get; set; }
        public string _Login_UsuarioID_Senhas { get; set; }
        public string _Login_UsuarioDesc_Senhas { get; set; }

        //ZERA OS CAMPOS
        public void ZerarCampos()
        {
            txtDescri.Text = string.Empty; txtSenhaAnt.Text = string.Empty; txtSenhaConfir.Text = string.Empty; txtSenhaNova.Text = string.Empty;
        }


        //LOAD DO FORM
        private void TabNewSe_Load(object sender, EventArgs e)
        {
            txtCodigo.Text = _Login_UsuarioID_Senhas.PadLeft(6, '0');
            txtUsuarioLogado.Text = _Login_UsuarioID_Senhas.PadLeft(6, '0'); ;


            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panPrin, panUp, panPrin, panPrin, panPrin, panPrin };
            //MÉTODO DA CFG
            CfgComun.CfgComun_CLASS CFG = new CfgComun.CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, _Login_LojaID_Senhas);


            txtCodigo.Select(); txtCodigo.SelectAll();
        }

        //TECLAS DE ATALHO
        private void TabNewSe_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F7:
                    btnFechar.PerformClick();
                    break;
                case Keys.F9:
                    btnZerar.PerformClick();
                    break;
                case Keys.F10:
                    btnGravar.PerformClick();
                    break;
            }
        }


        //EVENTO CLICK DOS BUTTONS
        #region Buttons Gravar, Fechar e Zerar
        private void btnZerar_Click(object sender, EventArgs e)
        {
            ZerarCampos();
            txtCodigo.Select(); txtCodigo.SelectAll();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            //CAMPOS OBRIGATÓRIOS
            TabNewSe_CamposObrig Obrig = new TabNewSe_CamposObrig();
            bool Obriga = Obrig.CamposObrig(txtCodigo, txtDescri, txtSenhaAnt, txtSenhaNova, txtSenhaConfir);
            if (!Obriga) { } else { return; }

            //VERIFICA SE A SENHA ANTIGA ESETÁ CORRETA
            TabNewSe_MET MET = new TabNewSe_MET();
            bool Senha = MET.MET_VerificaSenha(txtCodigo, txtSenhaAnt, txtSenhaAnt, this);
            if (!Senha) { } else { return; }

            //GRAVA A NOVA SENHA
            MET.MET_GravaNovaSenha(txtCodigo, txtSenhaNova, btnGravar, ZerarCampos, txtUsuarioLogado);

        }
        #endregion


        //APENAS NÚMEROS
        #region APENAS NÚMEROS
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabNewSe_MET MET = new TabNewSe_MET();
            MET.MET_ApenasNúmeros(e);
        }

        private void txtSenhaAnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabNewSe_MET MET = new TabNewSe_MET();
            MET.MET_ApenasNúmeros(e);
        }

        private void txtSenhaNova_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabNewSe_MET MET = new TabNewSe_MET();
            MET.MET_ApenasNúmeros(e);
        }

        private void txtSenhaConfir_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabNewSe_MET MET = new TabNewSe_MET();
            MET.MET_ApenasNúmeros(e);
        }
        #endregion



        //SELECIONA O USUÁRIO NO TAB
        private void txtCodigo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                TabNewSe_MET MET = new TabNewSe_MET();
                MET.MET_SelecionaUsuarioTAB(txtCodigo, txtDescri, grbUsuarioTrocar, btnGravar, ZerarCampos);
            }
        }

        //ZERA NO TEXTChange
        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            btnGravar.Enabled = false;
            ZerarCampos();
        }

        //ABRE A PESQUISA
        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                PesUsuar.cs.PesUsuar_CALL Call = new PesUsuar.cs.PesUsuar_CALL();
                Call._Login_CryptCode = _Login_LojaID_Senhas;
                Call._Login_CryptDesc = _Login_UsuarioID_Senhas;
                Call._WenCrypt = "PesUsuar21Wenemy3156!.350?°";
                Call.PesUsuar_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtCodigo.Text = Call._ResultPesquisaCALL;
                }

                txtCodigo.SelectAll();
            }
        }

        //VERIFICA A SENHA NO Tab
        private void txtSenhaAnt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                TabNewSe_MET MET = new TabNewSe_MET();
                MET.MET_VerificaSenha(txtCodigo, txtSenhaAnt, grbSenhas, this);
            }
        }


        //SELECT ALL NO CLICK
        #region SELECTALL NO METODO

        //SELECT ALL
        private void txtCodigo_MouseDown(object sender, MouseEventArgs e)
        {
            btnGravar.Enabled = false;
            ZerarCampos();
            txtCodigo.Select(); txtCodigo.SelectAll();
        }
        private void txtSenhaAnt_MouseDown(object sender, MouseEventArgs e)
        {
            txtSenhaAnt.SelectAll();
        }
        private void txtSenhaNova_MouseDown(object sender, MouseEventArgs e)
        {
            txtSenhaNova.SelectAll();
        }
        private void txtSenhaConfir_MouseDown(object sender, MouseEventArgs e)
        {
            txtSenhaConfir.SelectAll();
        }
        #endregion













    }
}
