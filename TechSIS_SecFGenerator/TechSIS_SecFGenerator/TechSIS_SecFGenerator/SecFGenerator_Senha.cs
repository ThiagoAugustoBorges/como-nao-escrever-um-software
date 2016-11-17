using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TechSIS_SecFGenerator
{
    internal partial class SecFGenerator_Senha : Form
    {
        public SecFGenerator_Senha()
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
            if (txtSenha.Text == "3156350")
            {
                TechSIS_SecFGenerator Generator = new TechSIS_SecFGenerator();
                this.Opacity = 0;
                this.ShowInTaskbar = false;
                Generator.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Senha de acesso ao sistema Generator está incorreta", "TechSIS Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Text = string.Empty;
                txtSenha.Select();
            }
        }
    }
}
