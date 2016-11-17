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
using System.Security;
using System.Security.Cryptography;
using System.Xml;

namespace TabEmpre
{
    public class TabEmpre_CamposObrig
    {
        public bool CamposObrig(TextBox TXT_MESTRE, Button btnAvancar, TextBox txtCodigo, TextBox txtDescri, TextBox txtFantasia, TextBox txtResponsavel, ComboBox comTipoFaturamento, TextBox txtEndLogradouro, TextBox txtEndNumero, TextBox txtEndCidade, TextBox txtEndCidDescri, TextBox txtEndCidUF, TextBox txtEndBairro, TextBox txtInscricaoEstadual, MaskedTextBox mtbVencEst)
        {
            if (TXT_MESTRE.Text == "ALTERAR" && btnAvancar.Enabled == true)
            {
                if (String.IsNullOrEmpty(txtCodigo.Text))
                {
                    MessageBox.Show("Campo (Código) deve ser preenchido", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigo.Select(); txtCodigo.SelectAll();
                    return true;
                }
                if (Convert.ToInt32(txtCodigo.Text) < 1)
                {
                    txtCodigo.Select();
                    SendKeys.Send("{TAB}");
                    return true;
                }
                if (String.IsNullOrEmpty(txtDescri.Text))
                {
                    MessageBox.Show("Campo (Descrição) deve ser preenchido", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescri.Select(); txtDescri.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtFantasia.Text))
                {
                    MessageBox.Show("Campo (Fantasia) deve ser preenchido", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFantasia.Select(); txtFantasia.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtResponsavel.Text))
                {
                    MessageBox.Show("Campo (Responsável) deve ser preenchido", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtResponsavel.Select(); txtResponsavel.SelectAll();
                    return true;
                }
                #region Obrigação em NFE
                if (String.IsNullOrEmpty(txtEndLogradouro.Text) && comTipoFaturamento.SelectedIndex == 1)
                {
                    MessageBox.Show("Campo (Logradouro) deve ser preenchido quando faturamento é NFe", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEndLogradouro.Select(); txtEndLogradouro.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtEndNumero.Text))
                {
                    txtEndNumero.Text = "S/N";
                    return false;
                }
                if (String.IsNullOrEmpty(txtEndCidade.Text) || String.IsNullOrEmpty(txtEndCidDescri.Text))
                {
                    MessageBox.Show("Campo (Cidade) deve ser preenchido", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEndCidade.Select(); txtEndCidade.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtEndCidUF.Text))
                {
                    MessageBox.Show("Atenção.: Cidade informada não contém UF. Verifique!", "TechSIS BWK Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
                if (String.IsNullOrEmpty(txtEndBairro.Text) && comTipoFaturamento.SelectedIndex == 1)
                {
                    MessageBox.Show("Campo (Bairro) deve ser preenchido quando faturamento é NFe", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEndBairro.Select(); txtEndBairro.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtInscricaoEstadual.Text))
                {
                    txtInscricaoEstadual.Text = "ISENTO";
                    mtbVencEst.Text = string.Empty;
                    return false;
                }
                #endregion
                return false;
            }
            return false;
        }
    }
}
