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

namespace TechSIS_BWK
{
    internal partial class InforMaquina : Form
    {
        public InforMaquina()
        {
            InitializeComponent();
        }

        public string LoginLoja { get; set; }
        public string UserCod { get; set; }
        public string UserDes { get; set; }

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

        private void InforMaquina_Load(object sender, EventArgs e)
        {
            lblNomeDaMaquina.Text = Environment.MachineName;
            lblSistemaOperacional.Text = Environment.OSVersion.ToString();
            lblDiretorioSistema.Text = "DIRETÓRIO DO SISTEMA..: " + Directory.GetCurrentDirectory();

            lblNumProc.Text = "NUM. DE PROCESSADORES.: " + Environment.ProcessorCount.ToString("0000");
            lblUsuarioLogado.Text = "USUÁRIO LOGADO........: " + UserCod.PadLeft(6,'0') + " " + UserDes;


            string CaminhoDoArquivo = ("SecF_" + LoginLoja + ".xml");
            XmlDocument LerXML = new XmlDocument();
            LerXML.Load(CaminhoDoArquivo);
            XmlNode Chave_UTF8 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_04");
            string _Chave_UTF8 = Descriptografar(Chave_UTF8.InnerText.ToString());
            string _Modulo = _Chave_UTF8.Substring(12, 1);
            string ModuloDesc = "";
            if (Convert.ToInt32(_Modulo) == 0)
            {
                ModuloDesc = "TechSIS Free EDITION";
            }
            if (Convert.ToInt32(_Modulo) == 1)
            {
                ModuloDesc = "TechSIS Express EDITION";
            }
            if (Convert.ToInt32(_Modulo) == 2)
            {
                ModuloDesc = "TechSIS Business EDITION";
            }
            if (Convert.ToInt32(_Modulo) == 3)
            {
                ModuloDesc = "TechSIS Controle EDITION";
            }
            if (Convert.ToInt32(_Modulo) == 4)
            {
                ModuloDesc = "TechSIS Pró EDITION";
            }

            lblModuloSistema.Text = "MODULO DO SISTEMA.....: " + ModuloDesc;

            lblUsuarWin.Text = "USUÁRIO DO WINDOWS....: " + Environment.UserName;


            if (Directory.Exists(Environment.GetEnvironmentVariable("ProgramFiles(x86)")))
            {
                lblArquitetura.Text = "ARQUITETURA...........: 64 BITS";
            }
            else
            {
                lblArquitetura.Text = "ARQUITETURA...........: 32 BITS";
            }

            lblVersSistema.Text = "VERSÃO DO SISTEMA.....: " + Environment.Version;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
