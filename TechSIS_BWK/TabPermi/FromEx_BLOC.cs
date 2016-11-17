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
    internal partial class FromEx_BLOC : Form
    {
        public FromEx_BLOC()
        {
            InitializeComponent();
        }


        public string MOTIVO
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
        public int SELECTEDIndex
        {
            get
            {
                return comBlocoBl1.SelectedIndex;
            }

            set
            {
                comBlocoBl1.SelectedIndex = value;
            }
        }

        private void FromEx_BLOC_Load(object sender, EventArgs e)
        {
            MOTIVO = string.Empty;
            SELECTEDIndex = -1;

            comBlocoBl1.SelectedIndex = 0;
        }

        private void btnConfirma_Click(object sender, EventArgs e)
        {
            if (txtMotivo.Text == string.Empty || txtMotivo.Text.Length < 15)
            {
                MessageBox.Show("Informe um motivo de pelo menos 15 caracteres!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMotivo.Select();
                return;
            }
            if (comBlocoBl1.SelectedIndex < 0)
            {
                MessageBox.Show("Informe o bloco a ser excluido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comBlocoBl1.Select(); comBlocoBl1.SelectAll();
                return;
            }
            this.Close();
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            MOTIVO = string.Empty;
            SELECTEDIndex = -1;
            this.Close();
        }

        private void FromEx_BLOC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnConfirma.PerformClick();
            }
        }

        private void comBlocoBl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMotivo.Select();
            txtMotivo.SelectAll();
        }
    }
}
