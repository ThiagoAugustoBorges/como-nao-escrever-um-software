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
    public class TabEmpre_ApareButtons
    {
        public void _ButtonINC(TextBox TXT_MESTRE, Button btnGravar, TextBox txtCodigo, MethodInvoker CamposINATIV, TabControl TabControl, TabPage Tp_1)
        {
            TabControl.SelectedTab = Tp_1;
            TXT_MESTRE.Text = "INCLUIR";
            TXT_MESTRE.BackColor = Color.Green;
            TXT_MESTRE.ForeColor = Color.Black;
            txtCodigo.Select(); txtCodigo.SelectAll();
            btnGravar.Enabled = false;
            CamposINATIV();
        }
        public void _ButtonALT(TextBox TXT_MESTRE, Button btnGravar, TextBox txtCodigo, MethodInvoker CamposINATIV, TabControl TabControl, TabPage Tp_1)
        {
            TabControl.SelectedTab = Tp_1;
            TXT_MESTRE.Text = "ALTERAR";
            TXT_MESTRE.BackColor = Color.Yellow;
            TXT_MESTRE.ForeColor = Color.Black;
            txtCodigo.Select(); txtCodigo.SelectAll();
            btnGravar.Enabled = false;
            CamposINATIV();
        }
        public void _ButtonEXC(TextBox TXT_MESTRE, Button btnGravar, TextBox txtCodigo, MethodInvoker CamposINATIV, TabControl TabControl, TabPage Tp_1)
        {
            TabControl.SelectedTab = Tp_1;
            TXT_MESTRE.Text = "EXCLUIR";
            TXT_MESTRE.BackColor = Color.Red;
            TXT_MESTRE.ForeColor = Color.Black;
            txtCodigo.Select(); txtCodigo.SelectAll();
            btnGravar.Enabled = false;
            CamposINATIV();
        }

        public void _ButtonSETAS(TextBox TXT_MESTRE, Button btnGravar, MethodInvoker CamposINATIV, TabControl TabControl, TabPage Tp_1)
        {
            TabControl.SelectedTab = Tp_1;
            TXT_MESTRE.Text = "CONSULTA";
            TXT_MESTRE.BackColor = Color.Blue;
            TXT_MESTRE.ForeColor = Color.White;
            btnGravar.Enabled = false;
            CamposINATIV();
        }

        public void _ButtonZER(TextBox TXT_MESTRE, TextBox txtCodigo, Button bntINC, Button btnGravar, MethodInvoker CamposINATIV, TabControl TabControl, TabPage Tp_1)
        {
            TabControl.SelectedTab = Tp_1;
            TXT_MESTRE.Text = "SELECT";
            TXT_MESTRE.BackColor = Color.Silver;
            TXT_MESTRE.ForeColor = Color.Black;
            txtCodigo.Text = string.Empty;
            bntINC.Select();
            btnGravar.Enabled = false;
            CamposINATIV();
        }
    }
}
