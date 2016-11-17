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
    internal class TabClien_AppaButtons
    {
        public void _ButtonINC(TextBox txtMESTRE, Button btnGravar, TextBox txtCodigo, MethodInvoker CamposDisable, TabControl TabControl, TabPage Tp1, MethodInvoker ZerarCampos, Button btnInfFinaShow, Button btnInfComerShow, Button btnAvancar, Button btnGravarAb2, Button btnVoltar)
        {
            TabControl.SelectedTab = Tp1;
            txtMESTRE.Text = "INCLUIR";
            txtMESTRE.BackColor = Color.Green;
            txtMESTRE.ForeColor = Color.Black;
            txtCodigo.Select(); txtCodigo.SelectAll();


            btnGravar.Enabled = false;
            btnInfFinaShow.Enabled = false;
            btnInfComerShow.Enabled = false;
            btnAvancar.Enabled = false;
            btnGravarAb2.Enabled = false;
            btnVoltar.Enabled = false;


            ZerarCampos();
            CamposDisable();
        }
        public void _ButtonALT(TextBox txtMESTRE, Button btnGravar, TextBox txtCodigo, MethodInvoker CamposDisable, TabControl TabControl, TabPage Tp1, MethodInvoker ZerarCampos, Button btnInfFinaShow, Button btnInfComerShow, Button btnAvancar, Button btnGravarAb2, Button btnVoltar)
        {
            TabControl.SelectedTab = Tp1;
            txtMESTRE.Text = "ALTERAR";
            txtMESTRE.BackColor = Color.Yellow;
            txtMESTRE.ForeColor = Color.Black;
            txtCodigo.Select(); txtCodigo.SelectAll();


            btnGravar.Enabled = false;
            btnInfFinaShow.Enabled = false;
            btnInfComerShow.Enabled = false;
            btnAvancar.Enabled = false;
            btnGravarAb2.Enabled = false;
            btnVoltar.Enabled = false;


            ZerarCampos();
            CamposDisable();
        }
        public void _ButtonEXC(TextBox txtMESTRE, Button btnGravar, TextBox txtCodigo, MethodInvoker CamposDisable, TabControl TabControl, TabPage Tp1, MethodInvoker ZerarCampos, Button btnInfFinaShow, Button btnInfComerShow, Button btnAvancar, Button btnGravarAb2, Button btnVoltar)
        {
            TabControl.SelectedTab = Tp1;
            txtMESTRE.Text = "EXCLUIR";
            txtMESTRE.BackColor = Color.Red;
            txtMESTRE.ForeColor = Color.Black;
            txtCodigo.Select(); txtCodigo.SelectAll();


            btnGravar.Enabled = false;
            btnInfFinaShow.Enabled = false;
            btnInfComerShow.Enabled = false;
            btnAvancar.Enabled = false;
            btnGravarAb2.Enabled = false;
            btnVoltar.Enabled = false;


            ZerarCampos();
            CamposDisable();
        }

        public void _ButtonSETAS(TextBox txtMESTRE, Button btnGravar, MethodInvoker CamposDisable, TabControl TabControl, TabPage Tp1, Button btnInfFinaShow, Button btnInfComerShow, Button btnAvancar, Button btnGravarAb2, Button btnVoltar)
        {
            if (TabControl.SelectedIndex > 1)
            {
                TabControl.SelectedTab = Tp1;
            }

            txtMESTRE.Text = "CONSULTA";
            txtMESTRE.BackColor = Color.Blue;
            txtMESTRE.ForeColor = Color.White;


            btnGravar.Enabled = false;
            btnInfFinaShow.Enabled = true;
            btnInfComerShow.Enabled = true;
            btnAvancar.Enabled = true;
            btnGravarAb2.Enabled = false;
            btnVoltar.Enabled = true;


            CamposDisable();
        }

        public void _ButtonZER(TextBox txtMESTRE, TextBox txtCodigo, Button btnIncluir, Button btnGravar, MethodInvoker CamposDisable, TabControl TabControl, TabPage Tp1, Button btnInfFinaShow, Button btnInfComerShow, Button btnAvancar, Button btnGravarAb2, Button btnVoltar)
        {
            TabControl.SelectedTab = Tp1;
            txtMESTRE.Text = "SELECT";
            txtMESTRE.BackColor = Color.Silver;
            txtMESTRE.ForeColor = Color.Black;
            txtCodigo.Text = string.Empty;
            
            btnGravar.Enabled = false;
            btnInfFinaShow.Enabled = false;
            btnInfComerShow.Enabled = false;
            btnAvancar.Enabled = false;
            btnGravarAb2.Enabled = false;
            btnVoltar.Enabled = false;


            CamposDisable();

            btnIncluir.Select();
        }
    }
}
