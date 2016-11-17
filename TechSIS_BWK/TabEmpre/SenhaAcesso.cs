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
using System.Security;
using System.Security.Cryptography;
using System.Xml;

namespace TabEmpre
{
    public partial class SenhaAcesso : Form
    {
        public SenhaAcesso()
        {
            InitializeComponent();
        }
        #region CRIPTOGRAFIA
        const string senha = "“3.!156350WeNeMy”";
        public static string Criptografar(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(senha));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }
        public static string Descriptografar(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(senha));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }
        #endregion


        public int IndexAntigo { get; set; }
        public int NovoIndex { get; set; }
        public string LojaLogada { get; set; }


        private void SenhaAcesso_Load(object sender, EventArgs e)
        {
            txtSenha.Select();
            comModuloSistema.SelectedIndex = IndexAntigo;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            string Confur = DateTime.Now.ToString("t");
            string Senha = Confur[0].ToString() + Confur[1].ToString();
            if (txtSenha.Text == "thiagowenemy1!" + Senha)
            {
                btnOK.Enabled = false;
                btnAcionar.Enabled = true;
                txtSenha.Enabled = false;
                comModuloSistema.Enabled = true;
                lblModuloSistema.Enabled = true;

                comModuloSistema.Select(); comModuloSistema.Focus(); comModuloSistema.SelectAll();
            }
            else
            {
                MessageBox.Show("A senha informada está incorreta", "TechSIS BWK Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha.Select(); txtSenha.SelectAll();
            }
        }
        private void btnAcionar_Click(object sender, EventArgs e)
        {
            NovoIndex = comModuloSistema.SelectedIndex;
            if (NovoIndex == IndexAntigo)
            {
                DialogResult Conf = MessageBox.Show("Atenção.: Mesmo módulo selecionado. Confirma?", "TechSIS Auto-Manutenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Conf == DialogResult.Yes)
                {
                    this.Close();
                }
                if (Conf == DialogResult.No)
                {
                    comModuloSistema.Select(); comModuloSistema.Focus(); comModuloSistema.SelectAll();
                }
            }
            else
            {
                if (NovoIndex > IndexAntigo)
                {
                    MessageBox.Show("Upgrade Realizado! Obrigado por escolher TechSIS.", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                if (NovoIndex < IndexAntigo)
                {
                    DialogResult Vers = MessageBox.Show("Atenção.: Versão escolhida é inferior a instalada. Confirma?", "TechSIS BWK Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (Vers == DialogResult.Yes)
                    {
                        this.Close();
                    }
                    if (Vers == DialogResult.No)
                    {
                        comModuloSistema.Select(); comModuloSistema.Focus(); comModuloSistema.SelectAll();
                    }
                }
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            NovoIndex = IndexAntigo;
        }




    }
}
