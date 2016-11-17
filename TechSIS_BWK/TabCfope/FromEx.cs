using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TabCfope
{
    internal partial class FromEx : Form
    {
        public FromEx()
        {
            InitializeComponent();
        }

        public string _MotivoExclusão
        {
            get
            {
                return txtMotivo.Text;
            }

            set
            {
                txtMotivo.Text = value;
            }
        }

        private void btnConfirma_Click(object sender, EventArgs e)
        {
            if (txtMotivo.Text == string.Empty || txtMotivo.Text.Length < 15)
            {
                MessageBox.Show("Informe um motivo de pelo menos 15 caracteres!", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMotivo.Select();
                return;
            }


            this.Close();
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            txtMotivo.Text = string.Empty;
            this.Close();
        }

        private void FromEx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnConfirma.PerformClick();
            }
        }

        private void FromEx_Load(object sender, EventArgs e)
        {
            _MotivoExclusão = string.Empty;
        }
    }
}
