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

namespace TabNewSe
{
    internal class TabNewSe_CamposObrig
    {
        //Define os campos obrigatorios antes de dar o OK
        public bool CamposObrig(TextBox txtCodigo, TextBox txtDescri, TextBox txtSenha1, TextBox txtSenha2, TextBox txtSenha3)
        {
            if (String.IsNullOrEmpty(txtCodigo.Text))
            {
                MessageBox.Show("Campo (CÓDIGO) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodigo.Select(); txtCodigo.SelectAll();
                return true;
            }
            if (String.IsNullOrEmpty(txtSenha1.Text))
            {
                MessageBox.Show("Campo (SENHA ATUAL) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSenha1.Select(); txtSenha1.SelectAll();
                return true;
            }
            if (String.IsNullOrEmpty(txtSenha2.Text))
            {
                MessageBox.Show("Campo (NOVA SENHA) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSenha2.Select(); txtSenha2.SelectAll();
                return true;
            }
            if (String.IsNullOrEmpty(txtSenha3.Text))
            {
                MessageBox.Show("Campo (CONFIRMAÇÃO) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSenha3.Select(); txtSenha3.SelectAll();
                return true;
            }
            if (txtSenha1.Text == txtSenha2.Text)
            {
                MessageBox.Show("NOVA SENHA DEVE SER DIFERENTE DA ANTIGA", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSenha2.Select(); txtSenha2.SelectAll();
                txtSenha3.Text = string.Empty;
                return true;
            }
            if (txtSenha2.Text != txtSenha3.Text)
            {
                MessageBox.Show("NOVA SENHA E CONFIRMAÇÃO NÃO BATEM", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSenha3.Select(); txtSenha3.SelectAll();
                txtSenha3.Text = string.Empty;
                return true;
            }
            return false;
        }
    }
}
