using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AplUsuaAt
{
    internal partial class AplUsuaAt : Form
    {
        public AplUsuaAt()
        {
            InitializeComponent();
        }

        public string _Login_LojaID_AplUsuaAt { get; set; }
        public string _Login_UsuarioID_AplUsuaAt { get; set; }
        public string _Login_UsuarioDesc_AplUsuaAt { get; set; }

        //LOAD DO FORM
        private void AplUsuaAt_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = "USUÁRIO.: " + _Login_UsuarioID_AplUsuaAt.PadLeft(6, '0');

            AplUsuaAt_MET MET = new AplUsuaAt_MET();

            //SELECIONA A COR DE FUNDO
            MET.MET_SelecionaCorFundo(panMeio, panMeio, panMeio, panMeio, panMeio, panMeio, _Login_LojaID_AplUsuaAt);

            //SELECIONA OS VALORES DO BANCO
            MET.MET_SelecionaValores(_Login_UsuarioID_AplUsuaAt, txtAtalho1, txtAtalho2, comPedidos, comNotas, comProduto, btnZerar);

            
        }


        //Buttons do FORM
        #region Buttons Fechar, Zerar e Gravar
        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnZerar_Click(object sender, EventArgs e)
        {
            comPedidos.SelectedIndex = 1;
            comNotas.SelectedIndex = 1;
            comProduto.SelectedIndex = 1;

            btnGravar.Select();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            AplUsuaAt_MET MET = new AplUsuaAt_MET();

            //VERIFICA OS CAMPOS OBRIGATORIOS
            bool Preench = MET.MET_CamposObrig(comPedidos, comNotas, comProduto);
            if (!Preench) { } else { return; }

            //MODIFICA OS VALORES
            MET.MET_ModificaValores(_Login_UsuarioID_AplUsuaAt, txtAtalho1, txtAtalho2, comPedidos, comNotas, comProduto, this);
        }
        #endregion

        //TECLAS DE ATALHO
        private void AplUsuaAt_KeyDown(object sender, KeyEventArgs e)
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
    }
}
