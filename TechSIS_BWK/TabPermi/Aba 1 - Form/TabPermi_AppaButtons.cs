using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TabPermi
{
    internal class TabPermi_AppaButtons
    {
        public void _ButtonINC(TextBox txtMESTRE, Button btnGravar, TextBox txtUsuarCodigo, TabControl TabControl, TabPage Tp1, MethodInvoker CamposEnable_grb1, MethodInvoker CamposDisable_grb1, MethodInvoker CamposEnable_grb2, MethodInvoker CamposDisable_grb2, MethodInvoker ZerarCampos_grb1, MethodInvoker ZerarCampos_grb2, TextBox txtUsuarDescri)
        {
            TabControl.SelectedTab = Tp1;
            txtMESTRE.Text = "INCLUIR";
            txtMESTRE.BackColor = Color.Green;
            txtMESTRE.ForeColor = Color.Black;
            txtUsuarCodigo.Select(); txtUsuarCodigo.SelectAll();
            btnGravar.Enabled = false;

            txtUsuarDescri.Text = string.Empty;

            CamposDisable_grb1();
            CamposDisable_grb2();
            ZerarCampos_grb1();
            ZerarCampos_grb2();
        }
        public void _ButtonALT(TextBox txtMESTRE, Button btnGravar, TextBox txtUsuarCodigo, TabControl TabControl, TabPage Tp1, MethodInvoker CamposEnable_grb1, MethodInvoker CamposDisable_grb1, MethodInvoker CamposEnable_grb2, MethodInvoker CamposDisable_grb2, MethodInvoker ZerarCampos_grb1, MethodInvoker ZerarCampos_grb2, TextBox txtUsuarDescri)
        {
            TabControl.SelectedTab = Tp1;
            txtMESTRE.Text = "ALTERAR";
            txtMESTRE.BackColor = Color.Yellow;
            txtMESTRE.ForeColor = Color.Black;
            txtUsuarCodigo.Select(); txtUsuarCodigo.SelectAll();
            btnGravar.Enabled = false;

            txtUsuarDescri.Text = string.Empty;

            CamposDisable_grb1();
            CamposDisable_grb2();
            ZerarCampos_grb1();
            ZerarCampos_grb2();
        }
        public void _ButtonEXC(TextBox txtMESTRE, Button btnGravar, TextBox txtUsuarCodigo, TabControl TabControl, TabPage Tp1, MethodInvoker CamposEnable_grb1, MethodInvoker CamposDisable_grb1, MethodInvoker CamposEnable_grb2, MethodInvoker CamposDisable_grb2, MethodInvoker ZerarCampos_grb1, MethodInvoker ZerarCampos_grb2, TextBox txtUsuarDescri)
        {
            TabControl.SelectedTab = Tp1;
            txtMESTRE.Text = "EXCLUIR";
            txtMESTRE.BackColor = Color.Red;
            txtMESTRE.ForeColor = Color.Black;
            txtUsuarCodigo.Select(); txtUsuarCodigo.SelectAll();
            btnGravar.Enabled = false;

            txtUsuarDescri.Text = string.Empty;

            CamposDisable_grb1();
            CamposDisable_grb2();
            ZerarCampos_grb1();
            ZerarCampos_grb2();
        }


        public void _ButtonSETAS(TextBox txtMESTRE, Button btnGravar, TextBox txtUsuarCodigo, TabControl TabControl, TabPage Tp1, MethodInvoker CamposEnable_grb1, MethodInvoker CamposDisable_grb1, MethodInvoker CamposEnable_grb2, MethodInvoker CamposDisable_grb2, MethodInvoker ZerarCampos_grb1, MethodInvoker ZerarCampos_grb2)
        {
            TabControl.SelectedTab = Tp1;
            txtMESTRE.Text = "CONSULTA";
            txtMESTRE.BackColor = Color.Blue;
            txtMESTRE.ForeColor = Color.White;
            btnGravar.Enabled = false;

            CamposDisable_grb1();
            CamposDisable_grb2();
        }


        public void _ButtonZER(TextBox txtMESTRE, Button btnGravar, TextBox txtUsuarCodigo, TabControl TabControl, TabPage Tp1, MethodInvoker CamposEnable_grb1, MethodInvoker CamposDisable_grb1, MethodInvoker CamposEnable_grb2, MethodInvoker CamposDisable_grb2, MethodInvoker ZerarCampos_grb1, MethodInvoker ZerarCampos_grb2, Button btnIncluir, TextBox txtUsuarDescri)
        {
            TabControl.SelectedTab = Tp1;
            txtMESTRE.Text = "SELECT";
            txtMESTRE.BackColor = Color.Silver;
            txtMESTRE.ForeColor = Color.Black;

            txtUsuarDescri.Text = string.Empty;
            txtUsuarCodigo.Text = string.Empty;

            txtUsuarCodigo.Text = string.Empty;
            btnIncluir.Select();
            btnGravar.Enabled = false;
            CamposDisable_grb1();
            CamposDisable_grb2();
        }
    }
}
