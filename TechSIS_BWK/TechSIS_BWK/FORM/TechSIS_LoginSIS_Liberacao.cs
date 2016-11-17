using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace TechSIS_BWK
{
    internal partial class TechSIS_LoginSIS_Liberacao : Form
    {
        public TechSIS_LoginSIS_Liberacao()
        {
            InitializeComponent();
        }

        TechSIS_LoginMET LogMET = new TechSIS_LoginMET();



        string UsuarioFTP = "techsis";
        string SenhaFTP = "3156350";
        //LIBERT É A PASTA ONDE FICA OS TXTs COM OS CPFS E CNPJS
        string FTP_HOST = @"ftp://ftp.xpg.com.br/Tech_LIBERT/";



        //Método da API
        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);
        //VERIFICA SE EXISTE CONEXÃO COM A INTERNET
        public static Boolean IsConnected()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }



        public string EmpresaLogada { get; set; }

        //SELECIONA OS DADOS
        private void TechSIS_LoginSIS_Liberacao_Load(object sender, EventArgs e)
        {  
            LogMET.lblCpfCnpj = lblCpfCnpj;
            LogMET.lblDataLiberacao = lblDataLiberacao;
            LogMET.lblModulo = lblModulo;
            LogMET.lblRazao = lblRazao;



            //SELECIONO OS VALORES DO ARQUIVO SecF E POPULO OS LABELS
            LogMET.SecF_SelecionaValores();

            //SE FOR FREE, EU NEM FAÇO VERIFICAÇÃO DE NADA
            if (lblModulo.Text == "MÓDULO.......: TechSIS FREE EDITION")
            {
                btnConexão.Enabled = false;
                picBtn1.Visible = false;
                btnStatus.Enabled = false;
                picBtn2.Visible = false;
                btnLiberar.Enabled = true;
                picBtn3.Visible = true;
                lblAvisoMsg.Text = "GERA MAIS 30 DIAS DE LIBERAÇÃO PARA O ARQUIVO";
            }


            this.Text += " (" + EmpresaLogada.PadLeft(6, '0') + ")";
        }




        //VERIFICA A CONEXÃO COM A INTERNET
        private void btnConexão_Click(object sender, EventArgs e)
        {
            if (IsConnected() == true)
            {
                btnConexão.Enabled = false;
                picBtn1.Visible = false;
                btnStatus.Enabled = true;
                picBtn2.Visible = true;

                lblAvisoMsg.Text = "VERIFICAÇÃO DO STATUS DE PAGAMENTO DA MANUTENÇÃO";
            }
            else
            {
                btnConexão.Enabled = false;
                picBtn1.Visible = false;

                DialogResult Manual = MessageBox.Show("Não foi encontrada uma conexão com a internet na maquina atual\nDeseja efetuar a liberação manual do sistema? (Exige senha)","TechSIS LIBERAÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (Manual == DialogResult.Yes)
                {
                    TechSIS_LoginSIS_LiberacaoManu Manu = new TechSIS_LoginSIS_LiberacaoManu();
                    Manu.Owner = this;
                    Manu.ShowDialog();
                }               

                btnConexão.Enabled = true;
                picBtn1.Visible = true;
                btnConexão.Select();
            }
        }
        //VERIFICA O STATUS DE PAGAMENTO DO SISTEMA
        //ADICIONA OS DIAS DE EMERGENCIA
        private void btnStatus_Click(object sender, EventArgs e)
        {
            bool VerificaStatusPagamento = LogMET.Libert_VerificaStatusPagamento(UsuarioFTP, SenhaFTP, FTP_HOST, LogMET.SecF_CNPJ);
            if (!VerificaStatusPagamento)
            {
                btnStatus.Enabled = false;
                picBtn2.Visible = false;
                btnLiberar.Enabled = true;
                picBtn3.Visible = true;
                lblAvisoMsg.Text = "GERA MAIS 30 DIAS DE LIBERAÇÃO PARA O ARQUIVO";
            }
            else
            {
                btnStatus.Enabled = true;
                picBtn2.Visible = true;
                btnLiberar.Enabled = false;
                picBtn3.Visible = false;
                btnVoltar.PerformClick();
            }
        }
        //LIBERA UMA NOVA DATA
        private void btnLiberar_Click(object sender, EventArgs e)
        {
            LogMET.Libert_NovoArquivo(30, "NORMAL");

            //VOLTO O ARQUIVO PARA OCULTO
            System.IO.File.SetAttributes("..\\Debug\\TechSIS_" + EmpresaLogada + "_WenFILE.ini", System.IO.FileAttributes.Hidden);

            btnVoltar.PerformClick();
        }
    }
}
