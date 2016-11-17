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

namespace TabCidad
{
    internal class TabCidad_CamposObrig
    {
        public bool CamposObrig(TextBox txtMESTRE, TextBox txtCodigo, Panel panCodigoAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Button btnGravar, MethodInvoker CamposEnable, TextBox txtDescri, TextBox txtPaisCod, TextBox txtPaisDesc, TextBox txtIbgeUF, ComboBox comUF, TextBox txtUFDesc, TextBox txtIbgeMuCod, TextBox txtIbgeMuDesc, TextBox txtIbgeEstadual, MaskedTextBox mtbCep1, MaskedTextBox mtbCep2, ComboBox comStatus)
        {
            if (txtMESTRE.Text == "ALTERAR" || txtMESTRE.Text == "INCLUIR")
            {
                if (String.IsNullOrEmpty(txtCodigo.Text) || Convert.ToInt32(txtCodigo.Text) < 1 || Convert.ToInt32(txtCodigo.Text) >= 999999)
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
                if (String.IsNullOrEmpty(txtPaisCod.Text) || String.IsNullOrEmpty(txtPaisDesc.Text))
                {
                    MessageBox.Show("Campo (País) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPaisCod.Select(); txtPaisCod.SelectAll();
                    return true;
                }
                if (comUF.SelectedIndex < 0)
                {
                    MessageBox.Show("Campo (UF) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comUF.Select(); comUF.SelectAll(); comUF.Focus();
                    return true;
                }
                if (String.IsNullOrEmpty(txtIbgeMuCod.Text) || String.IsNullOrEmpty(txtIbgeMuDesc.Text))
                {
                    MessageBox.Show("Campo (Código Município) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIbgeMuCod.Select(); txtIbgeMuCod.SelectAll();
                    return true;
                }
                if (mtbCep1.Text != string.Empty && mtbCep1.Text.Length < 5)
                {
                    MessageBox.Show("Campo (CEP) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtbCep1.Select(); mtbCep1.SelectAll();
                    SendKeys.Send("{HOME}");
                    SendKeys.Send("+{END}");
                    return true;
                }
                if (mtbCep2.Text != string.Empty && mtbCep2.Text.Length < 5)
                {
                    MessageBox.Show("Campo (CEP) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtbCep2.Select(); mtbCep2.SelectAll();
                    SendKeys.Send("{HOME}");
                    SendKeys.Send("+{END}");
                    return true;
                }
                if (comStatus.SelectedIndex < 1)
                {
                    MessageBox.Show("Campos (Situação) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comStatus.Select(); comStatus.SelectAll();
                    return true;
                }
                if (txtDescri.Text != txtIbgeMuDesc.Text && Convert.ToInt32(txtIbgeMuCod.Text) != 0)
                {
                    DialogResult Continua = MessageBox.Show("Atenção: Cidade informada não bate com a descrição do Município\n\nDESEJA CONTINUAR?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (Continua == DialogResult.Yes)
                    {

                    }
                    else
                    {
                        txtIbgeMuCod.Select(); txtIbgeMuCod.SelectAll();
                        return true;
                    }
                }
                if (comUF.Text == "EXT" && Convert.ToInt32(txtPaisCod.Text) == 1058)
                {
                    DialogResult Continua = MessageBox.Show("Atenção: UF informada (EXT) não bate com o País (BRASIL)\n\nDESEJA CONTINUAR?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (Continua == DialogResult.Yes)
                    {

                    }
                    else
                    {
                        comUF.Select(); comUF.SelectAll();
                        return true;
                    }
                }

                return false;
            }
            return false;
        }
    }
}
