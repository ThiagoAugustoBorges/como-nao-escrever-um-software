using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AplLixeira
{
    public partial class AplLixeira : Form
    {
        public AplLixeira()
        {
            InitializeComponent();
        }

        public string _Login_UsuarioID_AplLixeira
        { get; set; }
        public string _Login_LojaID_AplLixeira
        { get; set; }



        //SELECIONA OS VALORES NO TAB
        private void txtCodigo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtCodigo.Text == string.Empty)
                {
                    MessageBox.Show("Campo (Código) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    grbCampos.Focus(); txtCodigo.SelectAll();
                }
                else
                {
                    if (Convert.ToInt32(txtCodigo.Text) <= 0)
                    {
                        txtCodigo.Text = string.Empty;
                        MessageBox.Show("Campo (Código) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        grbCampos.Focus(); txtCodigo.SelectAll();
                    }
                    else
                    {
                        AplLixeira_MET MET = new AplLixeira_MET();
                        MET.MET_SelecionaPrograma(txtCodigo, txtDescri, grbCampos, Dgv_Lixeira, btnEsvaziar);
                        
                    }
                }
            }
        }



        
        //CHAMA A PESQUISA
        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                Dgv_Lixeira.Rows.Clear();
                btnEsvaziar.Enabled = false;
                txtDescri.Text = string.Empty;

                PesProgr.cs.PesProgr_CALL Call = new PesProgr.cs.PesProgr_CALL();
                Call._Login_CryptCode = _Login_LojaID_AplLixeira;
                Call._Login_CryptDesc = _Login_UsuarioID_AplLixeira;
                Call._WenCrypt = "PesProgr13Wenemy3156!.350?°";
                Call.PesProgr_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtCodigo.Text = Call._ResultPesquisaCALL;
                }

                txtCodigo.SelectAll();
            }
        }

        //APENAS NÚMEROS
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            AplLixeira_MET MET = new AplLixeira_MET();
            MET.MET_ApenasNúmeros(e);
        }

        //APAGA NO TEXTCHANGE
        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            Dgv_Lixeira.Rows.Clear();
            btnEsvaziar.Enabled = false;
            txtDescri.Text = string.Empty;
        }

        //APAGA NO MOUSEDOWN
        private void txtCodigo_MouseDown(object sender, MouseEventArgs e)
        {
            Dgv_Lixeira.Rows.Clear();
            btnEsvaziar.Enabled = false;
            txtDescri.Text = string.Empty;
            txtCodigo.SelectAll();
        }

    }
}
