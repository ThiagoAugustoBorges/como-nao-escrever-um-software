using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AplReport
{
    internal partial class AplReport : Form
    {
        public AplReport()
        {
            InitializeComponent();
        }

        public string Seq_Loja { get; set; }
        public string Seq_Usua { get; set; }

        private void rabErro_CheckedChanged(object sender, EventArgs e)
        {
            if (rabErro.Checked == true)
            {
                grbDados.Enabled = true;
                btnEnviar.Enabled = true;
                cheLembrar.Enabled = true;
                lblVariavel.Text = "ONDE E QUANDO FOI O ERRO QUE VOCÊ ENCONTROU? (ENTER PARA MUDAR DE LINHA)";

                rabSugestao.Enabled = false;
                rabAdicionar.Enabled = false;

                AplReport_MET MET = new AplReport_MET();
                MET.LerXMLUsuar(rabErro, rabSugestao, rabAdicionar, txtNome, txtEmail, txtCidade, rtbTexto, cheLembrar);

                txtNome.Select();
            }
        }
        private void rabSugestao_CheckedChanged(object sender, EventArgs e)
        {
            if (rabSugestao.Checked == true)
            {
                grbDados.Enabled = true;
                btnEnviar.Enabled = true;
                cheLembrar.Enabled = true;
                lblVariavel.Text = "QUAL A SUGESTÃO QUE VOCÊ TEM PARA O SISTEMA? (ENTER PARA MUDAR DE LINHA)";

                rabErro.Enabled = false;
                rabAdicionar.Enabled = false;

                AplReport_MET MET = new AplReport_MET();
                MET.LerXMLUsuar(rabErro, rabSugestao, rabAdicionar, txtNome, txtEmail, txtCidade, rtbTexto, cheLembrar);

                txtNome.Select();
            }
        }
        private void rabAdicionar_CheckedChanged(object sender, EventArgs e)
        {
            if (rabAdicionar.Checked == true)
            {
                grbDados.Enabled = true;
                btnEnviar.Enabled = true;
                cheLembrar.Enabled = true;
                lblVariavel.Text = "ONDE E PORQUE VOCÊ QUER ADICIONAR UM CAMPO? (ENTER PARA MUDAR DE LINHA)";

                rabErro.Enabled = false;
                rabSugestao.Enabled = false;

                AplReport_MET MET = new AplReport_MET();
                MET.LerXMLUsuar(rabErro, rabSugestao, rabAdicionar, txtNome, txtEmail, txtCidade, rtbTexto, cheLembrar);

                txtNome.Select();
            }
        }



        private void AplReport_Load(object sender, EventArgs e)
        {
            grbTipo.Select();
            txtData.Text = DateTime.Now.ToString("dd/MM/yyyy");
            AplReport_MET MET = new AplReport_MET();
            MET.SelecionaCorFundo(Painel_First, Painel_Up, Painel_Down, Painel_Down, Painel_Down, Painel_Down, Seq_Loja);
        }



        private void btnEnviar_Click(object sender, EventArgs e)
        {
            AplReport_MET MET = new AplReport_MET();

            bool Preench = MET.CamposObrig(txtNome, txtEmail, txtCidade, rtbTexto);
            if (!Preench) { } else { return; }
            
            btnEnviar.Enabled = false;

            bool Conec = MET.ConecTESTE(this);
            if (!Conec) { } else { return; }



            MET.WrREPORT(rabErro, rabSugestao, rabAdicionar, txtNome, txtEmail, txtCidade, rtbTexto);
            MET.UpFTP(rabErro, rabSugestao, rabAdicionar, txtNome, txtEmail, txtCidade, rtbTexto);
            MET.GravaXMLUsur(rabErro, rabSugestao, rabAdicionar, txtNome, txtEmail, txtCidade, rtbTexto, cheLembrar);

            MessageBox.Show("Sr(a) " + txtNome.Text + "\nReport enviado com sucesso! OBRIGADO POR REPORTAR.\n\nSeu report será analisado, e caso a solicitação e/ou adição for aprovada ou o erro for comprovado pela area de suporte, será programado e disponibilizado para download via atualização automática.\nFique atento a caixa de email do contato " + txtEmail.Text + ", quando a solicitação for programada e/ou visualizada um email será enviado para o endereço informado.", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }
    }
}
