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

namespace TabUsuar
{
    internal class TabUsuar_Impressao
    {
        public bool CamposObrig(RadioButton rabRPV, RadioButton rabWORD, RadioButton rabEXCEL, RadioButton rabTXT, ComboBox comSituacaoImp, ComboBox comPermissaoImp, RadioButton rabOrdemNumerica, RadioButton rabOrdemAlfabetica, RadioButton rabOrdemAlfabeticaApelid, TextBox txtEmpreCodImp, TextBox txtEmpreDescImp)
        {
            if (rabRPV.Checked == false && rabWORD.Checked == false && rabEXCEL.Checked == false && rabTXT.Checked == false)
            {
                rabRPV.Checked = true;
            }

            if (comSituacaoImp.SelectedIndex < 0)
            {
                comSituacaoImp.SelectedIndex = 0;
            }

            if (comPermissaoImp.SelectedIndex < 0)
            {
                comPermissaoImp.SelectedIndex = 0;
            }

            if (rabOrdemAlfabetica.Checked == false && rabOrdemNumerica.Checked == false && rabOrdemAlfabeticaApelid.Checked == false)
            {
                rabOrdemNumerica.Checked = true;
            }

            if (txtEmpreDescImp.Text == string.Empty)
            {
                txtEmpreCodImp.Text = string.Empty;
            }
            return false;
        }

    }
}