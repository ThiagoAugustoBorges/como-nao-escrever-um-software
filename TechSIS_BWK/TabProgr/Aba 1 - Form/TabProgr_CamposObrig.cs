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

namespace TabProgr
{
    internal class TabProgr_CamposObrig
    {
        public bool CamposObrig(TextBox txtMESTRE, TextBox txtCodigo, TextBox txtDescricao, ComboBox comStatus, ComboBox comModulo, TextBox txtUsuario)
        {
            if (txtMESTRE.Text == "INCLUIR" || txtMESTRE.Text == "ALTERAR")
            {
                if (String.IsNullOrEmpty(txtCodigo.Text) || Convert.ToInt32(txtCodigo.Text) < 10000)
                {
                    MessageBox.Show("Campo (Código) preeenchido incorretamente. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigo.Select(); txtCodigo.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtDescricao.Text))
                {
                    MessageBox.Show("Campo (Descrição) deve ser preenchido. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescricao.Select(); txtDescricao.SelectAll();
                    return true;
                }
                if (comStatus.SelectedIndex <= 0)
                {
                    MessageBox.Show("Campo (Status) deve ser preenchido. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comStatus.Select(); comStatus.SelectAll(); comStatus.Focus();
                    return true;
                }

                if (comModulo.SelectedIndex <= 0)
                {
                    MessageBox.Show("Campo (Modulo) deve ser preenchido. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comModulo.Select(); comModulo.SelectAll(); comModulo.Focus();
                    return true;
                }

                return false;
            }
            return false;
        }
    }
}
