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

namespace TabClien
{
    internal class TabClien_CamposObrig
    {
        public bool CamposObrig(TextBox txtMESTRE, TextBox txtCodigo, TextBox txtDescri, TextBox txtCodigoPrin, ComboBox comTipoPFPJ, MaskedTextBox mtbCpfCnpj, TextBox txtFantasia, TextBox txtInscricaoEstadual, MaskedTextBox mtbVencEst, TextBox txtEmpresaCod, TextBox txtEmpresaDesc, TextBox txtConceito, TextBox txtEndCidadeFATU, TextBox txtEndCidDescriFATU, TextBox txtEndCidUFFATU, ComboBox comContratoEmpresa, ComboBox comCategoria, TextBox txtVendedorDesc, TextBox txtVendedorCod, TextBox txtEndCidadePERSO, TextBox txtEndCidDescriPERSO, TextBox txtTransportadoraCod, TextBox txtTransportadoraDesc, TextBox txtConvenioCod, TextBox txtConvenioDesc, TextBox txtCfop, TextBox txtCfopDesc, MaskedTextBox mtbVencMun, TextBox txtInscricaoMunicipal, TextBox txtValorLimiteCre, MaskedTextBox mtbDataVenciLimite, MaskedTextBox mtbContraInicio, MaskedTextBox mtbContraFim, MaskedTextBox mtbPfCpfConjuge, TextBox txtPfConjuge, ComboBox comStatus, TextBox txtRotaSequen, TextBox txtRotaCod)
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
                if (String.IsNullOrEmpty(txtCodigoPrin.Text))
                {
                    txtCodigoPrin.Text = txtCodigo.Text;
                }
                if (comTipoPFPJ.SelectedIndex < 0)
                {
                    MessageBox.Show("Campo (Tipo) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comTipoPFPJ.Select(); comTipoPFPJ.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(mtbCpfCnpj.Text))
                {
                    MessageBox.Show("Campo (CPF.CNPJ) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtbCpfCnpj.Select(); mtbCpfCnpj.SelectAll();
                    return true;
                }
                if (comCategoria.SelectedIndex < 0)
                {
                    MessageBox.Show("Campo (Categoria) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comCategoria.Select(); comCategoria.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtFantasia.Text))
                {
                    MessageBox.Show("Campo (Fantasia/Apelido) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFantasia.Select(); txtFantasia.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtInscricaoEstadual.Text))
                {
                    txtInscricaoEstadual.Text = "ISENTO";
                    mtbVencEst.Text = string.Empty;
                }
                if (String.IsNullOrEmpty(txtEmpresaCod.Text))
                {
                    MessageBox.Show("Campo (Empresa) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmpresaCod.Select(); txtEmpresaCod.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtEmpresaDesc.Text))
                {
                    MessageBox.Show("Campo (Empresa) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmpresaCod.Select(); txtEmpresaCod.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtConceito.Text))
                {
                    MessageBox.Show("Campo (Conceito) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtConceito.Select(); txtConceito.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtRotaSequen.Text) && txtRotaCod.Text != string.Empty)
                {
                    MessageBox.Show("Campo (Rota) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRotaCod.Select(); txtRotaCod.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtEndCidadeFATU.Text))
                {
                    MessageBox.Show("Campo (Cidade) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEndCidadeFATU.Select(); txtEndCidadeFATU.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtEndCidDescriFATU.Text))
                {
                    MessageBox.Show("Campo (Cidade) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEndCidadeFATU.Select(); txtEndCidadeFATU.SelectAll();
                    return true;
                }
                if (String.IsNullOrEmpty(txtEndCidUFFATU.Text))
                {
                    MessageBox.Show("Atenção.: Cidade informada não contem UF. Verifique!", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
                if (comContratoEmpresa.SelectedIndex < 0)
                {
                    comContratoEmpresa.SelectedIndex = 0;
                }
                if (txtVendedorDesc.Text == string.Empty)
                {
                    txtVendedorCod.Text = string.Empty;
                }
                if (txtEndCidDescriPERSO.Text == string.Empty)
                {
                    txtEndCidadePERSO.Text = string.Empty;
                }
                if (txtTransportadoraDesc.Text == string.Empty)
                {
                    txtTransportadoraCod.Text = string.Empty;
                }
                if (txtConvenioDesc.Text == string.Empty)
                {
                    txtConvenioCod.Text = string.Empty;
                }
                if (txtCfopDesc.Text == string.Empty)
                {
                    txtCfop.Text = string.Empty;
                }
                if (txtCfopDesc.Text == string.Empty)
                {
                    txtCfop.Text = string.Empty;
                }
                if (txtInscricaoEstadual.Text == string.Empty)
                {
                    mtbVencEst.Text = string.Empty;
                }
                if (txtInscricaoMunicipal.Text == string.Empty)
                {
                    mtbVencMun.Text = string.Empty;
                }
                if (txtValorLimiteCre.Text == string.Empty)
                {
                    mtbDataVenciLimite.Text = string.Empty;
                }
                if (txtPfConjuge.Text == string.Empty)
                {
                    mtbPfCpfConjuge.Text = string.Empty;
                }
                if (comContratoEmpresa.SelectedIndex == 2)
                {
                    mtbContraInicio.Text = string.Empty;
                    mtbContraFim.Text = string.Empty;
                }
                if (comContratoEmpresa.SelectedIndex == 1)
                {
                    mtbContraFim.Text = string.Empty;
                }
                if (comStatus.SelectedIndex <= 0)
                {
                    comStatus.SelectedIndex = 1;
                }
                return false;
            }
            return false;
        }
    }
}
