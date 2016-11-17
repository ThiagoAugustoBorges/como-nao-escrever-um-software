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

namespace TabUsuar
{
    internal class TabUsuar_CamposObrig
    {
        public bool CamposObrig(TextBox txtCodigo, TextBox txtDescri, ComboBox comPermissao, TextBox txtEmpreCod, TextBox txtEmpreDesc, TextBox txtApelido, ComboBox comStatus, DataGridView Dgv_Empresas, TextBox txtCaixa, TextBox txtMESTRE)
        {
            if (txtMESTRE.Text == "ALTERAR" || txtMESTRE.Text == "INCLUIR")
            {

                if (String.IsNullOrEmpty(txtCodigo.Text))
                {
                    MessageBox.Show("Campo (Código) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigo.Select(); txtCodigo.SelectAll();
                    return true;
                }
                if (Convert.ToInt32(txtCodigo.Text) == 0)
                {
                    MessageBox.Show("Campo (Código) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigo.Select(); txtCodigo.SelectAll();
                    return true;
                }




                if (String.IsNullOrEmpty(txtDescri.Text))
                {
                    MessageBox.Show("Campo (Descrição) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescri.Select(); txtDescri.SelectAll();
                    return true;
                }





                if (comPermissao.SelectedIndex <= 0)
                {
                    MessageBox.Show("Campo (Permissão) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comPermissao.Select(); comPermissao.SelectAll();
                    return true;
                }




                if (String.IsNullOrEmpty(txtEmpreCod.Text))
                {
                    MessageBox.Show("Campo (Empresa Padrão) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmpreCod.Select(); txtEmpreCod.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtEmpreDesc.Text))
                {
                    MessageBox.Show("Campo (Empresa Padrão) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmpreCod.Select(); txtEmpreCod.SelectAll();
                    return true;
                }




                if (String.IsNullOrEmpty(txtApelido.Text))
                {
                    MessageBox.Show("Campo (Apelido) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtApelido.Select(); txtApelido.SelectAll();
                    return true;
                }



                if (comStatus.SelectedIndex <= 0)
                {
                    MessageBox.Show("Campo (Status) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comStatus.Select(); comStatus.SelectAll();
                    return true;
                }



                if (Dgv_Empresas.Rows.Count == 0)
                {
                    MessageBox.Show("Adicione pelo menos um caixa para este usuário", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCaixa.Select();
                    return true;
                }
            }
            return false;
        }
    }
}
