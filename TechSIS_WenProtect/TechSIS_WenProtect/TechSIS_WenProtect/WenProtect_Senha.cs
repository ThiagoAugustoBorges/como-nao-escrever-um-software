using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TechSIS_WenProtect
{
    internal partial class WenProtect_Senha : Form
    {
        public WenProtect_Senha()
        {
            InitializeComponent();
        }


        //FECHA A APLICAÇÃO
        private void btnCancela_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
        //VERIFICA A SENHA
        private void btnConfirma_Click(object sender, EventArgs e)
        {
            if (txtSenha.Text == "!!!3156wenemy350...")
            {
                WenProtect Protect = new WenProtect();
                this.Opacity = 0;
                Protect.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Senha de acesso ao sistema Protect está incorreta", "TechSIS Protect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Text = string.Empty;
                txtSenha.Select();
            }
        }
    }
}
