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
using System.Net;

namespace AplReport
{
    internal class AplReport_MET
    {
        #region CRIPTOGRAFIA 
        public static string WenProtects(string Message)
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
        public static string WenDisprotects(string Message)
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

        public string NomeREPORT { get; set; }


        //Verifica os campos obrigatórios
        public bool CamposObrig(TextBox txtNome, TextBox txtEmail, TextBox txtCidade, RichTextBox rtbTexto)
        {
            if (String.IsNullOrEmpty(txtNome.Text))
            {
                MessageBox.Show("Campo (Nome) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNome.Select(); txtNome.SelectAll();
                return true;
            }
            if (txtNome.Text.Length <= 3)
            {
                MessageBox.Show("Campo (Nome) muito curto. Informe um nome maior", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNome.Select(); txtNome.SelectAll();
                return true;
            }
            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Campo (EMAIL) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Select(); txtEmail.SelectAll();
                return true;
            }
            if (String.IsNullOrEmpty(txtCidade.Text))
            {
                MessageBox.Show("Campo (Cidade) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCidade.Select(); txtCidade.SelectAll();
                return true;
            }
            if (txtCidade.Text.Length <= 3)
            {
                MessageBox.Show("Campo (Cidade) muito curto. Informe um nome maior", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCidade.Select(); txtCidade.SelectAll();
                return true;
            }
            if (String.IsNullOrEmpty(rtbTexto.Text))
            {
                MessageBox.Show("Informe o motivo do report.", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rtbTexto.Select(); rtbTexto.SelectAll();
                return true;
            }
            return false;
        }

        //Realiza a primeira conexão
        public bool ConecTESTE(Form Form)
        {
            string UsuarFTP_TESTE = "techsis";
            string SenhaFTP_TESTE = "3156350";
            string FPT_HOST_TESTE = @"ftp://ftp.xpg.com.br/Tech_STATUS/Status.txt";

            Form.Text = "REALIZANDO CONEXÃO DE TESTE.........";

            try
            {
                Application.DoEvents();
                FtpWebRequest RequisicaoTESTE = (FtpWebRequest)WebRequest.Create(FPT_HOST_TESTE);
                RequisicaoTESTE.Credentials = new NetworkCredential(UsuarFTP_TESTE, SenhaFTP_TESTE);
                RequisicaoTESTE.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                FtpWebResponse RespostaTESTE = (FtpWebResponse)RequisicaoTESTE.GetResponse();
                Form.Text = "TechSIS - FORMULÁRIO DE REPORTs";
                return false;
            }
            catch (Exception)
            {
                Form.Text = "TechSIS - FORMULÁRIO DE REPORTs";
                MessageBox.Show("Domínio FTP está em manutenção. Tente novamente mais tarde!\n\nNOTA.: Verifique sua conexão com a Internet.", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Form.Close();
                return true;
            }
        }

        //Esreve o report
        public void WrREPORT(RadioButton rabErro, RadioButton rabSugestao, RadioButton rabAdicionar, TextBox txtNome, TextBox txtEmail, TextBox txtCidade, RichTextBox rtbTexto)
        {
            if (!Directory.Exists("..\\Log\\Reports"))
            {
                Directory.CreateDirectory("..\\Log\\Reports");
            }



            NomeREPORT = "";
            string DateREPORT = DateTime.Now.ToString("dd.MM.yyyy");
            string HoraREPORT = DateTime.Now.ToString("HH.mm.ss");
            string SujeREPORT = txtNome.Text.Substring(0, 3);
            string TipoREPORT = "";

            if (rabErro.Checked == true)
            {
                NomeREPORT = "Report_Erro[" + DateREPORT + "].Hora[" + HoraREPORT + "].Nome[" + SujeREPORT + "].txt";
                TipoREPORT = "ERRO";
            }
            else if (rabSugestao.Checked == true)
            {
                NomeREPORT = "Report_Suge[" + DateREPORT + "].Hora[" + HoraREPORT + "].Nome[" + SujeREPORT + "].txt";
                TipoREPORT = "SUGESTÃO";
            }
            else if (rabAdicionar.Checked == true)
            {
                NomeREPORT = "Report_Adic[" + DateREPORT + "].Hora[" + HoraREPORT + "].Nome[" + SujeREPORT + "].txt";
                TipoREPORT = "ADICIONAR CAMPO";
            }

            try
            {
                StreamWriter Escrever_REPORT = new StreamWriter("..\\Log\\Reports\\" + NomeREPORT, true, Encoding.Default);

                Escrever_REPORT.WriteLine("TIPO DE REPORT.....: " + TipoREPORT);
                Escrever_REPORT.WriteLine("NOME DO CLIENTE....: " + txtNome.Text);
                Escrever_REPORT.WriteLine("EMAIL DO CLIENTE...: " + txtEmail.Text);
                Escrever_REPORT.WriteLine("CIDADE DO CLIENTE..: " + txtCidade.Text);
                Escrever_REPORT.WriteLine(" ");
                Escrever_REPORT.WriteLine(" ");
                Escrever_REPORT.WriteLine("TEXTO.: " + rtbTexto.Text);

                Escrever_REPORT.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("ERRO AO ESCREVER O REPORT.\n" + NomeREPORT + "\n\n" + Ex.Message, "TechSIS Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Faz o upload no FTP
        public void UpFTP(RadioButton rabErro, RadioButton rabSugestao, RadioButton rabAdicionar, TextBox txtNome, TextBox txtEmail, TextBox txtCidade, RichTextBox rtbTexto)
        {
            //Caminho do arquivo para upload
            FileInfo CaminhoArqui = new FileInfo("..\\Log\\Reports\\" + NomeREPORT);

            string FTP_Folder = "";

            if (rabErro.Checked == true)
            {
                FTP_Folder = "ftp://ftp.xpg.com.br/Tech_REPORT/Report_ERRO/";
            }
            else if (rabSugestao.Checked == true)
            {
                FTP_Folder = "ftp://ftp.xpg.com.br/Tech_REPORT/Report_SUGE/";
            }
            else if (rabAdicionar.Checked == true)
            {
                FTP_Folder = "ftp://ftp.xpg.com.br/Tech_REPORT/Report_ADIC/";
            }


            //Cria comunicação com o servidor
            FtpWebRequest Requis = (FtpWebRequest)FtpWebRequest.Create(FTP_Folder + NomeREPORT);
            //Define que a ação vai ser de upload
            Requis.Method = WebRequestMethods.Ftp.UploadFile;
            //Credenciais para o login (usuario, senha)
            Requis.Credentials = new NetworkCredential("techsis", "3156350");
            //modo passivo
            Requis.UsePassive = true;
            //dados binarios
            Requis.UseBinary = true;
            //setar o KeepAlive para false
            Requis.KeepAlive = false;

            Requis.ContentLength = CaminhoArqui.Length;
            //cria a stream que será usada para mandar o arquivo via FTP
            Stream ResStream = Requis.GetRequestStream();
            byte[] buffer = new byte[2048];

            //Lê o arquivo de origem
            FileStream FileStre = CaminhoArqui.OpenRead();
            try
            {
                //Enquanto vai lendo o arquivo de origem, vai escrevendo no FTP
                int readCount = FileStre.Read(buffer, 0, buffer.Length);
                while (readCount > 0)
                {
                    //Esceve o arquivo
                    ResStream.Write(buffer, 0, readCount);
                    readCount = FileStre.Read(buffer, 0, buffer.Length);
                }
            }
            finally
            {
                FileStre.Close();
                ResStream.Close();
            }
        }

        //Grava os dados do usuário em um XML
        public void GravaXMLUsur(RadioButton rabErro, RadioButton rabSugestao, RadioButton rabAdicionar, TextBox txtNome, TextBox txtEmail, TextBox txtCidade, RichTextBox rtbTexto, CheckBox cheLembrar)
        {
            if (!Directory.Exists(@"..\Log\"))
            {
                Directory.CreateDirectory(@"..\Log\");
            }

            try
            {
                string Patch = @"..\Log\DadosReport.xml";
                XmlTextWriter GerarXML = new XmlTextWriter(Patch, null);
                GerarXML.WriteStartDocument();
                GerarXML.WriteStartElement("Dados_Report");
                GerarXML.WriteElementString("LEMBRAR", WenProtects(cheLembrar.Checked.ToString()));
                GerarXML.WriteElementString("NOME", WenProtects(txtNome.Text));
                GerarXML.WriteElementString("EMAIL", WenProtects(txtEmail.Text));
                GerarXML.WriteElementString("CIDADE", WenProtects(txtCidade.Text));
                GerarXML.WriteEndElement();
                GerarXML.Close();
            }
            catch (Exception Ex)
            {
                DialogResult Erro = MessageBox.Show("ERRO Exception. Erro ao tentar gravar o XML (Exception Erro)\nAperte em CANCELAR para ver detalhes do erro.", "Verifique erro ocorrido!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (Erro == DialogResult.Cancel)
                {
                    MessageBox.Show("DETALHAMENTO DO ERRO Exception\n\n" + Ex.Message, "TechSIS Auto-Manutenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Le o conteudo dos xmls
        public void LerXMLUsuar(RadioButton rabErro, RadioButton rabSugestao, RadioButton rabAdicionar, TextBox txtNome, TextBox txtEmail, TextBox txtCidade, RichTextBox rtbTexto, CheckBox cheLembrar)
        {
            try
            {
                if (Directory.Exists(@"..\Log\"))
                {
                    if (File.Exists(@"..\Log\DadosReport.xml"))
                    {
                        string CaminhoDoArquivo = @"..\Log\DadosReport.xml";
                        XmlDocument LerXML = new XmlDocument();
                        LerXML.Load(CaminhoDoArquivo);
                        XmlNode LEMBRAR = LerXML.DocumentElement.SelectSingleNode("LEMBRAR");
                        string _LEMBRAR = WenDisprotects(LEMBRAR.InnerText.ToString());
                        if (_LEMBRAR == "True")
                        {
                            cheLembrar.Checked = true;

                            XmlNode NOME = LerXML.DocumentElement.SelectSingleNode("NOME");
                            string _NOME = WenDisprotects(NOME.InnerText.ToString());
                            txtNome.Text = _NOME;

                            XmlNode EMAIL = LerXML.DocumentElement.SelectSingleNode("EMAIL");
                            string _EMAIL = WenDisprotects(EMAIL.InnerText.ToString());
                            txtEmail.Text = _EMAIL;

                            XmlNode CIDADE = LerXML.DocumentElement.SelectSingleNode("CIDADE");
                            string _CIDADE = WenDisprotects(CIDADE.InnerText.ToString());
                            txtCidade.Text = _CIDADE;
                        }
                    }
                    else
                    {
                        cheLembrar.Checked = true;
                    }
                }
                else
                {
                    Directory.CreateDirectory(@"..\Log\");
                }
            }
            catch (XmlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método LerXMLUsuar()\n\nBLOCO.: 1 = AplReport_MET - POPULAR INFORMAÇÕES ÚLTIMO REPORT\n\n" + Ex.Message, "TechSIS XML Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método LerXMLUsuar()\n\nBLOCO.: 1 = AplReport_MET - POPULAR INFORMAÇÕES ÚLTIMO REPORT\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Seleciona a cor de fundo dos formularios
        public void SelecionaCorFundo(Control Control_1, Control Control_2, Control Control_3, Control Control_4, Control Control_5, Control Control_6, string CodigoLoja)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string SelecionaCOR = "SELECT CorFun_CON FROM TabConfi WHERE SeqLoj_CON = " + CodigoLoja;
            SqlCommand Comando = new SqlCommand(SelecionaCOR, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string Cor = Dr[0].ToString();

                    Control_1.BackColor = Color.FromName(Cor);
                    Control_2.BackColor = Color.FromName(Cor);
                    Control_3.BackColor = Color.FromName(Cor);
                    Control_4.BackColor = Color.FromName(Cor);
                    Control_5.BackColor = Color.FromName(Cor);
                    Control_6.BackColor = Color.FromName(Cor);
                }
                else
                {
                    Control_1.BackColor = Color.Silver;
                    Control_2.BackColor = Color.Silver;
                    Control_3.BackColor = Color.Silver;
                    Control_4.BackColor = Color.Silver;
                    Control_5.BackColor = Color.Silver;
                    Control_6.BackColor = Color.Silver;
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                Conexão.Close();
            }
        }
    }
}