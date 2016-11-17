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
namespace TabCfope
{
    internal class TabCfope_CamposObrig
    {
        public bool CamposObrig(TextBox txtMESTRE, TextBox txtCodigo, TextBox txtDescri, TextBox txtIndCod, TextBox txtIndDesc, TextBox txtComeCod, TextBox txtComeDesc, ComboBox comLocalizacao)
        {
            if (txtMESTRE.Text == "ALTERAR" || txtMESTRE.Text == "INCLUIR")
            {
                if (String.IsNullOrEmpty(txtCodigo.Text))
                {
                    MessageBox.Show("Campo (Código) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigo.Select(); txtCodigo.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtDescri.Text))
                {
                    MessageBox.Show("Campo (Descrição) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescri.Select(); txtDescri.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtIndDesc.Text))
                {
                    txtIndCod.Text = string.Empty;
                }
                if (String.IsNullOrEmpty(txtComeDesc.Text))
                {
                    txtComeCod.Text = string.Empty;
                }
                if (comLocalizacao.SelectedIndex < 0)
                {
                    comLocalizacao.SelectedIndex = 0;
                }
            }
            return false;
        }
    }
}
