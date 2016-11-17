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

namespace TabClien
{
    internal class TabClien_Impressao
    {
        public bool CamposObrig(RadioButton rabRPV, RadioButton rabWORD, RadioButton rabEXCEL, RadioButton rabTXT, ComboBox comImpSituacao, ComboBox comImpCategoria, RadioButton rabOrdemNumerica, RadioButton rabOrdemAlfabetica, RadioButton rabOrdemAlfabeticaApelid, TextBox txtImpEmpreCod, TextBox txtImpCidadeCod, TextBox txtImpEmpreDesc, TextBox txtImpCidadeDesc)
        {
            if (rabRPV.Checked == false && rabWORD.Checked == false && rabEXCEL.Checked == false && rabTXT.Checked == false)
            {
                rabRPV.Checked = true;
            }

            if (comImpSituacao.SelectedIndex < 0)
            {
                comImpSituacao.SelectedIndex = 0;
            }

            if (comImpCategoria.SelectedIndex < 0)
            {
                comImpCategoria.SelectedIndex = 6;
            }

            if (rabOrdemAlfabetica.Checked == false && rabOrdemNumerica.Checked == false && rabOrdemAlfabeticaApelid.Checked == false)
            {
                rabOrdemNumerica.Checked = true;
            }

            if (txtImpEmpreDesc.Text == string.Empty)
            {
                txtImpEmpreCod.Text = string.Empty;
            }

            if (txtImpCidadeDesc.Text == string.Empty)
            {
                txtImpCidadeCod.Text = string.Empty;
            }
            return false;
        }
    }
}
