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
    internal partial class TechSIS_SecFGenerator : Form
    {
        public TechSIS_SecFGenerator()
        {
            InitializeComponent();
        }

        public void ZerarCampos()
        {
            txtRazao.Text = string.Empty;
            comTipo.SelectedIndex = -1;
            mtbCpfCnpj.Text = string.Empty;
            mtbCpfCnpj.Mask = string.Empty;
            txtFantasia.Text = string.Empty;
            comLicOriginal.SelectedIndex = -1;
            comLicProvisoria.SelectedIndex = -1;
            comLicSistema.SelectedIndex = -1;
            txtChave.Text = string.Empty;
            btnGerarArquivo.Enabled = false;
        }
        public void CamposDisable()
        {
            nupArquivo.Enabled = false;
            txtRazao.Enabled = false;
            comTipo.Enabled = false;
            mtbCpfCnpj.Enabled = false;
            mtbCpfCnpj.Enabled = false;
            txtFantasia.Enabled = false;
            comLicOriginal.Enabled = false;
            comLicProvisoria.Enabled = false;
            comLicSistema.Enabled = false;
            txtChave.Enabled = false;
        }
        public void CamposEnable()
        {
            nupArquivo.Enabled = true;
            txtRazao.Enabled = true;
            comTipo.Enabled = true;
            mtbCpfCnpj.Enabled = true;
            mtbCpfCnpj.Enabled = true;
            txtFantasia.Enabled = true;
            comLicOriginal.Enabled = true;
            comLicProvisoria.Enabled = true;
            comLicSistema.Enabled = true;
            btnGerarChave.Enabled = true;
        }

        //INSTANCIO
        SecFGenerator_MET MET = new SecFGenerator_MET();


        //LOAD DO FORMULÁRIO
        private void TechSIS_SecFGenerator_Load(object sender, EventArgs e)
        {
            txtRazao.Select();

            MET.nupArquivo = nupArquivo;
            MET.txtRazao = txtRazao;
            MET.comTipo = comTipo;
            MET.mtbCpfCnpj = mtbCpfCnpj;
            MET.txtFantasia = txtFantasia;
            MET.comLicOriginal = comLicOriginal;
            MET.comLicProvisoria = comLicProvisoria;
            MET.comLicSistema = comLicSistema;
            MET.txtChave = txtChave;
        }


        #region Buttons do Formulário
        //GERA O ARQUIVO
        private void btnGerarArquivo_Click(object sender, EventArgs e)
        {
            MET.GerarArquivo();
            ZerarCampos();
            CamposEnable();
            nupArquivo.Value += 1;
        }
        //ZERA OS CAMPOS
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ZerarCampos();
            CamposEnable();
            txtRazao.Select();
        }
        //SAI DO FORMULÁRIO
        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
        //GERA A CHAVE
        private void btnGerarChave_Click(object sender, EventArgs e)
        {
            //CAMPOS OBRIGATÓRIOS
            bool CamposObrig = MET.CamposObrig();
            if (!CamposObrig) { } else { return; }

            //VALIDA CNPJ CPF
            bool Validar = MET.ValidarCPFCNPJ();
            if (Validar) { } else { return; }

           
            //GERO A CHAVE
            txtChave.Text = MET.GerarChave(ZerarCampos, CamposEnable);


            CamposDisable();
            btnGerarChave.Enabled = false;
            btnGerarArquivo.Enabled = true;
            btnGerarArquivo.Select();
        }
        #endregion


        //NÃO DEIXA MUDAR O VALOR DO ARQUIVO
        private void nupArquivo_ValueChanged(object sender, EventArgs e)
        {
            ZerarCampos();
            txtRazao.Select();
        }

        //MUDA O SELECTINDEX
        private void comLicOriginal_SelectedIndexChanged(object sender, EventArgs e)
        {
            comLicProvisoria.SelectedIndex = comLicOriginal.SelectedIndex;
            comLicSistema.SelectedIndex = 0;
        }

        //MASCA CPF CNPJ
        private void comTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comTipo.SelectedIndex == 0)
            {
                mtbCpfCnpj.Text = string.Empty;
                mtbCpfCnpj.Mask = "000,000,000-00";
                mtbCpfCnpj.Select();
                mtbCpfCnpj.SelectAll();
            }
            else if (comTipo.SelectedIndex == 1)
            {
                mtbCpfCnpj.Text = string.Empty;
                mtbCpfCnpj.Mask = "00,000,000/0000-00";
                mtbCpfCnpj.Select();
                mtbCpfCnpj.SelectAll();
            }
            else
            {
                mtbCpfCnpj.Mask = string.Empty;
            }
        }


        //SELECT ALL
        private void txtRazao_MouseDown(object sender, MouseEventArgs e)
        {
            txtRazao.SelectAll();
        }
        private void mtbCpfCnpj_MouseDown(object sender, MouseEventArgs e)
        {
            mtbCpfCnpj.SelectAll();
        }
        private void txtFantasia_MouseDown(object sender, MouseEventArgs e)
        {
            txtFantasia.SelectAll();
        }

       
    }
}
