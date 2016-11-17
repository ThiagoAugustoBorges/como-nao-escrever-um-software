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
    internal class TabCidad_AppaButtons
    {
        public void _ButtonINC(TextBox txtMESTRE, Button btnGravar, TextBox txtCodigo, MethodInvoker CamposDisable, TabControl TabControl, TabPage Tp1, MethodInvoker ZerarCampos)
        {
            TabControl.SelectedTab = Tp1;
            txtMESTRE.Text = "INCLUIR";
            txtMESTRE.BackColor = Color.Green;
            txtMESTRE.ForeColor = Color.Black;
            txtCodigo.Select(); txtCodigo.SelectAll();
            btnGravar.Enabled = false;
            ZerarCampos();
            CamposDisable();
        }
        public void _ButtonALT(TextBox txtMESTRE, Button btnGravar, TextBox txtCodigo, MethodInvoker CamposDisable, TabControl TabControl, TabPage Tp1, MethodInvoker ZerarCampos)
        {
            TabControl.SelectedTab = Tp1;
            txtMESTRE.Text = "ALTERAR";
            txtMESTRE.BackColor = Color.Yellow;
            txtMESTRE.ForeColor = Color.Black;
            txtCodigo.Select(); txtCodigo.SelectAll();
            btnGravar.Enabled = false;
            ZerarCampos();
            CamposDisable();
        }
        public void _ButtonEXC(TextBox txtMESTRE, Button btnGravar, TextBox txtCodigo, MethodInvoker CamposDisable, TabControl TabControl, TabPage Tp1, MethodInvoker ZerarCampos)
        {
            TabControl.SelectedTab = Tp1;
            txtMESTRE.Text = "EXCLUIR";
            txtMESTRE.BackColor = Color.Red;
            txtMESTRE.ForeColor = Color.Black;
            txtCodigo.Select(); txtCodigo.SelectAll();
            btnGravar.Enabled = false;
            ZerarCampos();
            CamposDisable();
        }

        public void _ButtonSETAS(TextBox txtMESTRE, Button btnGravar, MethodInvoker CamposDisable, TabControl TabControl, TabPage Tp1)
        {
            TabControl.SelectedTab = Tp1;
            txtMESTRE.Text = "CONSULTA";
            txtMESTRE.BackColor = Color.Blue;
            txtMESTRE.ForeColor = Color.White;
            btnGravar.Enabled = false;
            CamposDisable();
        }

        public void _ButtonZER(TextBox txtMESTRE, TextBox txtCodigo, Button btnIncluir, Button btnGravar, MethodInvoker CamposDisable, TabControl TabControl, TabPage Tp1)
        {
            TabControl.SelectedTab = Tp1;
            txtMESTRE.Text = "SELECT";
            txtMESTRE.BackColor = Color.Silver;
            txtMESTRE.ForeColor = Color.Black;
            txtCodigo.Text = string.Empty;
            btnIncluir.Select();
            btnGravar.Enabled = false;
            CamposDisable();
        }
    }
}
