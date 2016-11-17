using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TabUsuar
{
    internal partial class TabUsuar_SenhaINC : Form
    {
        public TabUsuar_SenhaINC()
        {
            InitializeComponent();
        }


        public string Senha { get; set; }


        //Define que pode usar apenas números
        #region Apenas Números
        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabUsuar_MET MET = new TabUsuar_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtConfirmacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabUsuar_MET MET = new TabUsuar_MET();
            MET.MET_ApenasNúmeros(e);
        }
        #endregion

        private void btnConfirma_Click(object sender, EventArgs e)
        {
            if (txtSenha.Text == string.Empty)
            {
                MessageBox.Show("Digite uma senha para o usuário", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSenha.Select();
                return;
            }
            if (txtConfirmacao.Text == string.Empty)
            {
                MessageBox.Show("Digite a confirmação da senha", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConfirmacao.Select();
                return;
            }

            if (txtConfirmacao.Text != txtSenha.Text)
            {
                MessageBox.Show("Confirmação não bate com senha informada", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConfirmacao.Select(); txtConfirmacao.SelectAll();
                return;
            }


            DialogResult Confirma = MessageBox.Show("Confirma a senha informada?", "TechSIS Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (Confirma == DialogResult.Yes)
            {
                Senha = txtSenha.Text;
                Close();
            }
            else
            {
                txtSenha.Text = string.Empty;
                txtConfirmacao.Text = string.Empty;
                txtSenha.Select();
            }
        }

      

        private void TabUsuar_SenhaINC_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Senha == string.Empty)
            {
                e.Cancel = true;
                MessageBox.Show("Informe uma senha para que a inclusão seja feita", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha.Select();
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void TabUsuar_SenhaINC_Load(object sender, EventArgs e)
        {
            txtSenha.Select();
        }

        private void TabUsuar_SenhaINC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnConfirma.PerformClick();
            }
        }
    }
}
