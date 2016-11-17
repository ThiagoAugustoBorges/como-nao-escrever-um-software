using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TabProgr
{
    internal class TabProgr_ImpreMET
    {
        public bool CamposObrig(RadioButton rabRPV, RadioButton rabWORD, RadioButton rabEXCEL, RadioButton rabTXT, ComboBox comSituacaoIMP, RadioButton rabOrdemNumerica, RadioButton rabOrdemAlfabetica)
        {
            if (rabRPV.Checked == false && rabWORD.Checked == false && rabEXCEL.Checked == false && rabTXT.Checked == false)
            {
                rabRPV.Checked = true;
            }
            if (comSituacaoIMP.SelectedIndex < 0)
            {
                comSituacaoIMP.SelectedIndex = 0;
            }
            if (rabOrdemAlfabetica.Checked == false && rabOrdemNumerica.Checked == false)
            {
                rabOrdemNumerica.Checked = true;
            }
            return false;
        }
    }
}
