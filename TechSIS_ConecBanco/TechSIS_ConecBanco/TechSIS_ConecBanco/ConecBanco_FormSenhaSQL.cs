using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TechSIS_ConecBanco
{
    internal partial class ConecBanco_FormSenhaSQL : Form
    {
        public ConecBanco_FormSenhaSQL()
        {
            InitializeComponent();
        }

        public string Senha { get; set; }
        public string TipoChamada { get; set; }

        //TROCA A COR DO BUTTON
        private void btnConfirma_Enter(object sender, EventArgs e)
        {
            btnConfirma.BackColor = Color.YellowGreen;
        }
        private void btnConfirma_Leave(object sender, EventArgs e)
        {
            btnConfirma.BackColor = SystemColors.Control;
        }
        private void btnConfirma_MouseEnter(object sender, EventArgs e)
        {
            btnConfirma.Select();
            btnConfirma.BackColor = Color.YellowGreen;
        }
        private void btnConfirma_MouseLeave(object sender, EventArgs e)
        {
            txtSenha.Select();
            txtSenha.SelectAll();
            btnConfirma.BackColor = SystemColors.Control;
        }




        //SELECIONA O BUTTON QUANDO CARACTERES = 7
        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            if (txtSenha.Text.Length == 7 && TipoChamada != "2")
            {
                btnConfirma.Select();
            }
        }

        //CONFIRMA A SENHA
        private void btnConfirma_Click(object sender, EventArgs e)
        {
            if (txtSenha.Text == string.Empty)
            {
                MessageBox.Show("Campo (Senha) deve ser informado", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSenha.Select();
                return;
            }
            else
            {
                Senha = txtSenha.Text;
                Close();
            }
        }


        //DEFINE O TIPO DE CHAMADA
        private void ConecBanco_FormSenhaSQL_Load(object sender, EventArgs e)
        {
            if (TipoChamada == "2")
            {
                lblLimitado.Visible = false;
                txtSenha.MaxLength = 30;
                lbl1.Text = " DIGITE A SENHA SQL DO SGDB SERVIDOR DO BANCO";
                lbl2.Text = "CONF. SERÁ FEITA AUTOMATICAMENTE";
            }
        }


        //RECEBE APENAS NÚMEROS
        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            ConecBanco_MET MET = new ConecBanco_MET();
            MET.MET_ApenasNúmeros(e);
        }


    }
}
