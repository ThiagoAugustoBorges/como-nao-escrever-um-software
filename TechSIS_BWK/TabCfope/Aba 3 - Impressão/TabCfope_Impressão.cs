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

namespace TabCfope
{
    internal class TabCfope_Impressão
    {
        public bool CamposObrig(RadioButton rabRPV, RadioButton rabWORD, RadioButton rabEXCEL, RadioButton rabTXT, ComboBox comImpLocalizacao, RadioButton rabOrdemNumerica, RadioButton rabOrdemAlfabetica)
        {
            if (rabRPV.Checked == false && rabWORD.Checked == false && rabEXCEL.Checked == false && rabTXT.Checked == false)
            {
                rabRPV.Checked = true;
            }


            if (comImpLocalizacao.SelectedIndex < 0)
            {
                comImpLocalizacao.SelectedIndex = 2;
            }



            if (rabOrdemAlfabetica.Checked == false && rabOrdemNumerica.Checked == false)
            {
                rabOrdemNumerica.Checked = true;
            }

            return false;
        }
    }
}
