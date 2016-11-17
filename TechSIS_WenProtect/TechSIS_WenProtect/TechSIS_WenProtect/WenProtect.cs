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

namespace TechSIS_WenProtect
{
    internal partial class WenProtect : Form
    {
        public WenProtect()
        {
            InitializeComponent();
        }


        #region CRIPTOGRAFIA     
        internal static string WenProtects(string Message, string Senha)
        {
            string senha = "“" + Senha + "”";
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
        internal static string WenDisprotect(string Message, string Senha)
        {
            string senha = "“" + Senha + "”";
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



        //SAI DO SISTEMA
        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        //CRIPTOGRAFA
        private void btnProtect_Click(object sender, EventArgs e)
        {
            try
            {
                rtbOutput.Text = WenProtects(rtbInput.Text, txtSenhaCript.Text);
            }
            catch (Exception)
            {
                rtbOutput.Text = "ERRO AO GERAR CRIPTOGRAFIA";
            }
           
        }
        //DESCRIPTOGRAFA
        private void btnDisprotect_Click(object sender, EventArgs e)
        {
            try
            {
                rtbOutput.Text = WenDisprotect(rtbInput.Text, txtSenhaCript.Text);
            }
            catch (Exception)
            {
                rtbOutput.Text = "ERRO AO GERAR DESCRIPTOGRAFIA";
            }
           
        }


        //APARECE A SENHA
        private void txtSenhaCript_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P && e.Modifiers == Keys.Control)
            {
                txtSenhaCript.Text = "3.!156350WeNeMy";
                btnProtect.Select();
            }
        }
    }
}
