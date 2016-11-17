using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;
using System.Xml;

namespace TechSIS_SecFGenerator
{
    internal class SecFGenerator_MET
    {
        #region CRIPTOGRAFIA
        internal static string WenProtects(string Message)
        {
            string senha = "“3.!156350WeNeMy”";
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
        internal static string WenDisprotect(string Message)
        {
            string senha = "“3.!156350WeNeMy”";
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

        #region VALIDA CPF\CNPJ
        //VALIDA O CPF CNPJ
        public bool MET_ValidaCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
        public bool MET_ValidaCNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
        #endregion

        #region CONTROLES
        public NumericUpDown nupArquivo { get; set; }
        public TextBox txtRazao { get; set; }
        public ComboBox comTipo { get; set; }
        public MaskedTextBox mtbCpfCnpj { get; set; }
        public TextBox txtFantasia { get; set; }
        public ComboBox comLicOriginal { get; set; }
        public ComboBox comLicProvisoria { get; set; }
        public ComboBox comLicSistema { get; set; }
        public TextBox txtChave { get; set; }
        #endregion


        //VERIFICA OS CAMPOS OBRIGATORIOS ANTES DE GERAR A CHAVE
        public bool CamposObrig()
        {
            if (txtRazao.Text == string.Empty || txtRazao.Text.Length < 10)
            {
                MessageBox.Show("Campo (Razão) em branco ou com menos de 10 digitos", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRazao.Select(); txtRazao.SelectAll();
                return true;
            }
            if (comTipo.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione CPF ou CNPJ para a empresa", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comTipo.Select(); comTipo.SelectAll();
                return true;
            }
            if (mtbCpfCnpj.Text == string.Empty)
            {
                MessageBox.Show("Campo (CPF.CNPJ) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbCpfCnpj.Select(); mtbCpfCnpj.SelectAll();
                return true;
            }
            if (txtFantasia.Text == string.Empty || txtFantasia.Text.Length < 05)
            {
                MessageBox.Show("Campo (Fantasia) em branco ou com menos de 05 digitos", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFantasia.Select(); txtFantasia.SelectAll();
                return true;
            }
            if (comLicOriginal.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione a licença original do arquivo", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comLicOriginal.Select(); comLicOriginal.SelectAll();
                return true;
            }
            if (comLicProvisoria.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione a licença provisória do arquivo", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comLicProvisoria.Select(); comLicProvisoria.SelectAll();
                return true;
            }
            if (comLicSistema.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione o sistema de iniciação do arquivo", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comLicSistema.Select(); comLicSistema.SelectAll();
                return true;
            }
            return false;
        }

        //VALIDA CPF OU CNPJ
        public bool ValidarCPFCNPJ()
        {
            if (comTipo.SelectedIndex == 0)
            {
                if (MET_ValidaCPF(mtbCpfCnpj.Text) == true)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Campo (CPF) é inválido. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtbCpfCnpj.Select(); mtbCpfCnpj.SelectAll();
                }
            }
            else
            {
                if (MET_ValidaCNPJ(mtbCpfCnpj.Text) == true)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Campo (CNPJ) é inválido. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtbCpfCnpj.Select(); mtbCpfCnpj.SelectAll();
                }
            }

            return false;
        }

        //GERO A CHAVE DO SISTEMA
        public string GerarChave(MethodInvoker ZerarCampos, MethodInvoker CamposEnable)
        {
            try
            {
                string _RAZ = txtRazao.Text.PadRight(50, ' ');
                string _FAN = txtFantasia.Text.PadRight(50, ' '); ;
                string _CPF = mtbCpfCnpj.Text.PadRight(15, ' ');

                string CHAVE = "TECHSIS" +
                    _RAZ[3].ToString() + _CPF[3].ToString() + _CPF[1].ToString() + _FAN[3].ToString()
                    + _FAN[1].ToString() + _CPF[5].ToString() + _RAZ[5].ToString() + _FAN[1].ToString()
                    + _RAZ[1].ToString() + _RAZ[3].ToString() + _FAN[2].ToString() + _CPF[8].ToString()
                    + _CPF[1].ToString() + _RAZ[5].ToString() + _RAZ[15].ToString() + _RAZ[12].ToString()
                    + _FAN[10].ToString() + _FAN[13].ToString() + _RAZ[14].ToString() + _RAZ[23].ToString()
                    + _CPF[4].ToString() + _CPF[13].ToString() + _CPF[14].ToString() + _RAZ[10].ToString()
                    + _FAN[5].ToString() + _CPF[2].ToString() + _CPF[13].ToString() + _RAZ[19].ToString()
                    + _RAZ[12].ToString() + _CPF[6].ToString() + _FAN[0].ToString() + _RAZ[0].ToString() + "END";

                CHAVE = CHAVE.Replace(" ", string.Empty);


                return CHAVE;
            }
            catch (Exception)
            {
                MessageBox.Show("ERRO AO GERAR CHAVE DE ACESSO. TENTE NOVAMENTE!", "TechSIS ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ZerarCampos();
                CamposEnable();
                return string.Empty;
            }
        }

        //GERO NOVO ARQUIVO DE LICENÇA
        public void GerarArquivo()
        {
            string _Arquivo = (nupArquivo.Value + 14).ToString("00");
            string _LICENCA = (comLicOriginal.SelectedIndex + 7).ToString("00");
            string _PROVISO = (comLicProvisoria.SelectedIndex + 15).ToString("00");
            string _SISTEMA = (comLicSistema.SelectedIndex + 3).ToString("00");

            try
            {
                FolderBrowserDialog Browser = new FolderBrowserDialog();
                Browser.Description = "Selecione o local onde o arquivo SecF_" + nupArquivo.Value.ToString("00") + " será salvo";
                DialogResult SalvarEm = Browser.ShowDialog();
                if (SalvarEm != DialogResult.Cancel)
                {
                    XmlTextWriter GerarXML = new XmlTextWriter(Browser.SelectedPath + "SecF_" + nupArquivo.Value.ToString("00") + ".xml", Encoding.UTF8);
                    GerarXML.WriteStartDocument();
                    GerarXML.WriteStartElement("TechSIS_SecurityFILE");
                    //RAZÃO SOCIAL
                    GerarXML.WriteElementString("Tech_SEC_01", WenProtects(txtRazao.Text));
                    //FANTASIA
                    GerarXML.WriteElementString("Tech_SEC_02", WenProtects(txtFantasia.Text));
                    //CPF OU CNPJ
                    GerarXML.WriteElementString("Tech_SEC_03", WenProtects(mtbCpfCnpj.Text));
                    //CHAVE
                    GerarXML.WriteElementString("Tech_SEC_04", WenProtects(txtChave.Text));
                    //DATA DO ARQUIVO + ARQUIVO + MODULO + MODULO PROVISORIO + SISTEMA
                    GerarXML.WriteElementString("Tech_SEC_05", WenProtects(DateTime.Now.ToString() + _Arquivo + _LICENCA + _PROVISO + _SISTEMA));


                    GerarXML.WriteEndElement();
                    GerarXML.Close();


                    MessageBox.Show("Arquivo de licença gerado com sucesso!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("ERRO AO GERAR ARQUIVO. Verifique o local de salvamento!", "TechSIS ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
            }
        }
    }
}
