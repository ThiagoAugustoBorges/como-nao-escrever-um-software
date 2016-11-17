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

namespace TabConve
{
    internal class TabConve_CamposObrig
    {
        public bool CamposObrig(TextBox txtMESTRE, TextBox txtCodigo, TextBox txtDescri, ComboBox comStatus, ComboBox comTipo)
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
                if (comStatus.SelectedIndex < 0)
                {
                    MessageBox.Show("Campo (Status) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comStatus.Select(); comStatus.SelectAll();
                    return true;
                }
                if (comTipo.SelectedIndex < 0)
                {
                    MessageBox.Show("Campo (Tipo) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comTipo.Select(); comTipo.SelectAll();
                    return true;
                }
            }
            return false;
        }
    }
}
