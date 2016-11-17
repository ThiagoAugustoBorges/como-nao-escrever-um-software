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

namespace TabSetor
{
    internal class TabSetor_CamposObrig
    {
        public bool CamposObrig(TextBox txtMESTRE, TextBox txtSetCod, TextBox txtSubCod, TextBox txtDescri, ComboBox comStatus)
        {
            if (txtMESTRE.Text == "ALTERAR" || txtMESTRE.Text == "INCLUIR")
            {
                if (String.IsNullOrEmpty(txtSetCod.Text))
                {
                    MessageBox.Show("Campo (Setor) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSetCod.Select(); txtSetCod.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtSubCod.Text))
                {
                    MessageBox.Show("Campo (Subsetor) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSubCod.Select(); txtSubCod.SelectAll();
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
            }


            return false;
        }
    }
}
