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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ServiceProcess;

namespace TechSIS_BWK
{
    internal class TechSIS_MET
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

        #region CAPTURA DE STRINGs DO SISTEMA

        //SELECIONA O MODULO DO SOFTWARE, E ESCREVE NO LABEL
        public string MET_SelecionaModuloSoftware(string LojaLogada)
        {
            string Modulo = "";

            try
            {
                string CaminhoDoArquivo = ("SecF_" + LojaLogada + ".xml");
                XmlDocument LerXML = new XmlDocument();
                LerXML.Load(CaminhoDoArquivo);
                XmlNode Tech_SEC_05 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_05");

                string _Tech_SEC_05 = WenDisprotects(Tech_SEC_05.InnerText.ToString());
                int _Modulo = Convert.ToInt32(_Tech_SEC_05.Substring(21, 2)) - 7;


                if (File.Exists(CaminhoDoArquivo))
                {
                    if (_Modulo == 0)
                    {
                        Modulo = "FREE";
                    }
                    else if (_Modulo == 1)
                    {
                        Modulo = "EXPRESS";
                    }
                    else if (_Modulo == 2)
                    {
                        Modulo = "BUSINESS";
                    }
                    else if (_Modulo == 3)
                    {
                        Modulo = "CONTROLE";
                    }
                    else if (_Modulo == 4)
                    {
                        Modulo = "PRÓ";
                    }
                    else
                    {
                        Modulo = "ERRO.";
                    }

                    return Modulo;
                }
                else
                {
                    return "ERRO.";
                }
            }
            catch (XmlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelecionaModuloSoftware()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS XML Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                return "ERRO.";
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelecionaModuloSoftware()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                return "ERRO.";
            }
        }

        //PEGA A VERSÃO DO BANCO NA TABELA E JOGA NO LBL
        public string MET_SelecionaVersaoBanco(string LojaLogada)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string SelecionaCOR = "SELECT VerBco_CON FROM TabConfi WHERE SeqLoj_CON = " + LojaLogada;
            SqlCommand Comando = new SqlCommand(SelecionaCOR, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    if (Dr["VerBco_CON"].ToString() == string.Empty)
                    {
                        return "0.0.0";
                    }
                    else
                    {
                        return Dr["VerBco_CON"].ToString();
                    }
                }
                else
                {
                    return "0.0.0";
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelecionaVersaoBanco()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "0.0.0";
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelecionaVersaoBanco()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "0.0.0";
            }
            finally
            {
                Conexão.Close();
            }
        }

        //PERGUNTA ANTES DE MUDAR DE USUÁRIO
        public string MET_PerguntaTrocarUsuCMD(string LojaLogada)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string SelecionaCOR = "SELECT PerUsu_CON FROM TabConfi WHERE SeqLoj_CON = " + LojaLogada;
            SqlCommand Comando = new SqlCommand(SelecionaCOR, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string SimNao = Dr[0].ToString();
                    if (SimNao == "True")
                    {
                        return "SIM";
                    }
                    else
                    {
                        return "NÃO";
                    }
                }
                else
                {
                    return "NÃO";
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_PerguntaTrocarUsuCMD()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "NÃO";
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_PerguntaTrocarUsuCMD()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "NÃO";
            }
            finally
            {
                Conexão.Close();
            }
        }

        //CAPTURA OS ATALHOS DO BUTTONS
        public string MET_AtalhosButtons(string IDButton, string Codigo_Loja, string CodigoUsu)
        {
            #region BUTTON 1
            if (IDButton == "1")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string strATALHOS = "SELECT NomDl1_CON FROM TabConfi WHERE SeqLoj_CON = " + Codigo_Loja;
                SqlCommand Comando = new SqlCommand(strATALHOS, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        string ATALHO = Dr["NomDl1_CON"].ToString();
                        return ATALHO;
                    }
                    else
                    {
                        return "ERRO";
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_AtalhosButtons()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "ERRO";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_AtalhosButtons()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "ERRO";
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
            #region BUTTON 2
            if (IDButton == "2")
            {

                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string strATALHOS = "SELECT NomDl2_CON FROM TabConfi WHERE SeqLoj_CON = " + Codigo_Loja;
                SqlCommand Comando = new SqlCommand(strATALHOS, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        string ATALHO = Dr["NomDl2_CON"].ToString();
                        return ATALHO;
                    }
                    else
                    {
                        return "ERRO";
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_AtalhosButtons()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "ERRO";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_AtalhosButtons()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "ERRO";
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
            #region BUTTON 1 = ATALHO USUÁRIO
            if (IDButton == "3")
            {

                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string strATALHOS = "SELECT AtaNu1_US2 FROM TabUsu02 WHERE SeqUsu_US2 = " + CodigoUsu;
                SqlCommand Comando = new SqlCommand(strATALHOS, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        string ATALHO = Dr["AtaNu1_US2"].ToString();
                        return ATALHO;
                    }
                    else
                    {
                        return "ERRO";
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_AtalhosButtons()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "ERRO";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_AtalhosButtons()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "ERRO";
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
            #region BUTTON 2 = ATALHO USUÁRIO
            if (IDButton == "4")
            {

                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string strATALHOS = "SELECT AtaNu2_US2 FROM TabUsu02 WHERE SeqUsu_US2 = " + CodigoUsu;
                SqlCommand Comando = new SqlCommand(strATALHOS, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        string ATALHO = Dr["AtaNu2_US2"].ToString();
                        return ATALHO;
                    }
                    else
                    {
                        return "ERRO";
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_AtalhosButtons()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "ERRO";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_AtalhosButtons()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "ERRO";
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
            return "ERRO";
        }

        #endregion

        //DEFINE O CONTROLE Q PODE RECEBER APENAS NÚMEROS
        public void MET_ApenasNúmeros(KeyPressEventArgs KeyPress)
        {
            if (!Char.IsDigit(KeyPress.KeyChar) && KeyPress.KeyChar != (char)8)
            {
                KeyPress.Handled = true;
                return;
            }
        }

        //SELECIONA A COR DE FUNDO DOS FORMULÁRIOS
        public void MET_SelecionaCorFundo(Control Control_1, Control Control_2, Control Control_3, Control Control_4, Control Control_5, Control Control_6, string CodigoLoja)
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
                    string Cor = Dr["CorFun_CON"].ToString();

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

        //DETERMINA O TEMPO DE INATIVIDADE
        public int str_TimeINTERVAL { get; set; }
        public void MET_CapturarTempoOcioso(string LojaLogada)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringOcioso = "SELECT TimeOc_CON FROM TabConfi WHERE SeqLoj_CON = " + LojaLogada;
            SqlCommand Comando = new SqlCommand(StringOcioso, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string TempoOcioso = Dr["TimeOc_CON"].ToString();
                    if (TempoOcioso == string.Empty)
                    {
                        TempoOcioso = "5";
                    }
                    if (Convert.ToInt32(TempoOcioso) < 1)
                    {
                        TempoOcioso = "1";
                    }
                    if (Convert.ToInt32(TempoOcioso) > 10)
                    {
                        TempoOcioso = "10";
                    }

                    string Time = TimeSpan.FromMinutes(Convert.ToDouble(TempoOcioso)).TotalMilliseconds.ToString();
                    str_TimeINTERVAL = Convert.ToInt32(Time);

                }
                else
                {
                    str_TimeINTERVAL = 600000;
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_CapturarTempoOcioso()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_CapturarTempoOcioso()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //VERIFICA SE O PROGRAMA É ENABLE OU DISABLE
        public void MET_ProgramaDisable(ToolStripMenuItem Tool, string Codigo_Caminho)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string SelecionaPrograma = "SELECT Status_PGR FROM TabProgr WHERE Sequen_PGR = " + Codigo_Caminho;

            SqlCommand _Go = new SqlCommand(SelecionaPrograma, Conexão);
            string Pgr = Convert.ToString(_Go.ExecuteScalar());

            try
            {
                if (Pgr != "1")
                {
                    Tool.Enabled = false;
                }
                else
                {
                    Tool.Enabled = true;
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_ProgramaDisable()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_ProgramaDisable()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }


        ///SEC_F
        #region MÉTODOS DO ARQUIVO Sec_F

        //VERIFICA SE A LOJA LOGADA, É DO MESMO NÚMERO DO ARQUIVO SEC_F
        public bool SEC_F_VerificaSeNúmeroLegal(string NúmeroLoja)
        {
            try
            {
                string CaminhoDoArquivo = ("SecF_" + NúmeroLoja + ".xml");
                XmlDocument LerXML = new XmlDocument();
                LerXML.Load(CaminhoDoArquivo);
                XmlNode Tech_SEC_05 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_05");
                string _Tech_SEC_05 = WenDisprotects(Tech_SEC_05.InnerText.ToString());

                if (Convert.ToInt32(NúmeroLoja) != (Convert.ToInt32(_Tech_SEC_05.Substring(19, 2)) - 14))
                {
                    MessageBox.Show("CÓDIGO DA EMPRESA LOGADA É DIFERENTE DA LICENÇA DO SISTEMA", "TechSIS ERRO FATAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.ExitThread();
                    return true;
                }
            }
            catch (XmlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método SEC_F_VerificaSeNúmeroLegal()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS XML Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                return true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método SEC_F_VerificaSeNúmeroLegal()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                return true;
            }

            return false;
        }

        //DA O UPDATE NO BANCO CASO O ARQUIVO SECF FOR ALTERADO
        public void SEC_F_UpdateSEC(string LojaLogada)
        {
            try
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string CaminhoDoArquivo = ("SecF_" + LojaLogada + ".xml");
                XmlDocument LerXML = new XmlDocument();
                LerXML.Load(CaminhoDoArquivo);
                XmlNode RAZAO_XML = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_01");
                XmlNode FANTA_XML = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_02");
                XmlNode CPFCNPJ_XML = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_03");
                XmlNode CHAVE_XML = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_04");

                string _RAZAO_STR = WenDisprotects(RAZAO_XML.InnerText);
                string _FANTA_STR = WenDisprotects(FANTA_XML.InnerText);
                string _CPFCNPJ_STR = WenDisprotects(CPFCNPJ_XML.InnerText);
                string _CHAVE_STR = WenDisprotects(CHAVE_XML.InnerText);


                string StringComando = "UPDATE TabEmpre SET Descri_EMP = @Descri_EMP,Fantas_EMP = @Fantas_EMP,Tipo01_EMP = @Tipo01_EMP,CpfCnp_EMP = @CpfCnp_EMP WHERE Sequen_EMP = " + LojaLogada;
                SqlCommand Comando = new SqlCommand(StringComando, Conexão);
                Comando.Parameters.Add("@Descri_EMP", SqlDbType.VarChar).Value = _RAZAO_STR;
                Comando.Parameters.Add("@Fantas_EMP", SqlDbType.VarChar).Value = _FANTA_STR;
                #region Comando.Parameters.Add("@Tipo01_EMP", SqlDbType.Int).Value = "0";
                if (_CPFCNPJ_STR.Length == 14)
                {
                    //CNPJ
                    Comando.Parameters.Add("@Tipo01_EMP", SqlDbType.Int).Value = "1";
                }
                else
                {
                    //CPF
                    Comando.Parameters.Add("@Tipo01_EMP", SqlDbType.Int).Value = "0";
                }
                #endregion
                Comando.Parameters.Add("@CpfCnp_EMP", SqlDbType.VarChar).Value = _CPFCNPJ_STR;

                try
                {
                    Comando.ExecuteNonQuery();
                    MessageBox.Show("CONFIGURAÇÕES INICIAIS RESTAURADAS COM SUCESSO!", "TechSIS Auto-Manutenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Restart();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Erro ao executar um rollback nas informações do arq. SecF\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.ExitThread();
                }
                finally
                {
                    Conexão.Close();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método SEC_F_UpdateSEC()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método SEC_F_UpdateSEC()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //VERIFICA NO ARQUIVO SECF SE A CHAVE UTF8 É LEGAL
        public bool SEC_F_VerificarUTF8(string LojaLogada)
        {
            try
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();


                string LojaTA = "'" + LojaLogada + "'";


                string CaminhoDoArquivo = ("SecF_" + LojaLogada + ".xml");
                XmlDocument LerXML = new XmlDocument();
                LerXML.Load(CaminhoDoArquivo);
                XmlNode Tech_SEC_04 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_04");
                string _Tech_SEC_04 = WenDisprotects(Tech_SEC_04.InnerText.ToString());

                string SELECT_No_Banco = "SELECT Descri_EMP,Fantas_EMP,CpfCnp_EMP FROM TabEmpre WHERE Sequen_EMP = " + LojaTA;
                SqlCommand Comando = new SqlCommand(SELECT_No_Banco, Conexão);

                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                string _RAZ = Dr["Descri_EMP"].ToString().PadRight(50, ' ');
                string _FAN = Dr["Fantas_EMP"].ToString().PadRight(50, ' ');
                string _CPF = Dr["CpfCnp_EMP"].ToString().PadRight(15, ' ');


                string CHAVE_DO_BANCO = "TECHSIS" +
                    _RAZ[3].ToString() + _CPF[3].ToString() + _CPF[1].ToString() + _FAN[3].ToString()
                    + _FAN[1].ToString() + _CPF[5].ToString() + _RAZ[5].ToString() + _FAN[1].ToString()
                    + _RAZ[1].ToString() + _RAZ[3].ToString() + _FAN[2].ToString() + _CPF[8].ToString()
                    + _CPF[1].ToString() + _RAZ[5].ToString() + _RAZ[15].ToString() + _RAZ[12].ToString()
                    + _FAN[10].ToString() + _FAN[13].ToString() + _RAZ[14].ToString() + _RAZ[23].ToString()
                    + _CPF[4].ToString() + _CPF[13].ToString() + _CPF[14].ToString() + _RAZ[10].ToString()
                    + _FAN[5].ToString() + _CPF[2].ToString() + _CPF[13].ToString() + _RAZ[19].ToString()
                    + _RAZ[12].ToString() + _CPF[6].ToString() + _FAN[0].ToString() + _RAZ[0].ToString() + "END";


                CHAVE_DO_BANCO = CHAVE_DO_BANCO.Replace(" ", string.Empty);


                if (CHAVE_DO_BANCO != _Tech_SEC_04)
                {
                    MessageBox.Show("CHAVE DE SEGURANÇA DIFERENTE DO ARQUIVO CRIPTOGRAFADO", "TechSIS Erro FATAL - CHAVE UTF8", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult Mudar = MessageBox.Show("DESEJA VOLTAR O SISTEMA PARA AS CONFIGURAÇÕES INICIAIS?", "TechSIS Auto-Manutenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (Mudar == DialogResult.Yes)
                    {
                        SEC_F_UpdateSEC(LojaLogada);
                    }
                    if (Mudar == DialogResult.No)
                    {
                        MessageBox.Show("O Software conta com um arquivo de criptografia de segurança para maior comodidade do Cliente e também da Software-House. Este arquivo é dotado com informações que não devem ser alteradas. Caso a alteração seja inevitável, a Software-House deverá ser comunicada para que libere um novo arquivo de segurança criptografado.\n\nO que é 'voltar o sistema para as configurações iniciais'?\nConsiste em voltar um backup de informações para que as chaves criptografadas sejam restauradas.Essa operação não acarreta em perca de dados nem de performace.\n\n" + DateTime.Now.ToString("f"), "TechSIS - Segurança da Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.ExitThread();
                    }

                    return true;
                }


                return false;
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método SEC_F_VerificarUTF8()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                return true;
            }
            catch (XmlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método SEC_F_VerificarUTF8()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS XML Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                return true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método SEC_F_VerificarUTF8()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                return true;
            }
        }

        //SELECIONA SE O ARQUIVO SECF EXISTE
        public bool SEC_F_Verifica_SecF_EXISTE(string LojaCodigo, TextBox txtSenha)
        {
            //Seleciona a Loja logada
            string CaminhoDoArquivo = ("SecF_" + LojaCodigo + ".xml");

            if (!File.Exists(CaminhoDoArquivo))
            {
                MessageBox.Show("ARQUIVO (" + CaminhoDoArquivo + ") INEXISTENTE. LOGIN NÃO PERMITIDO", "TechSIS Erro FATAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Select(); txtSenha.SelectAll();
                return true;
            }

            return false;
        }

        //VERIFICA SE O ARQUIVO SECF NÃO FOI EDITADO E CORROMPIDO
        public bool SEC_F_Verifica_SecF_LEGAL(string LojaCodigo, TextBox txtSenha)
        {
            try
            {
                string CaminhoDoArquivo = ("SecF_" + LojaCodigo + ".xml");
                XmlDocument LerXML = new XmlDocument();
                LerXML.Load(CaminhoDoArquivo);
                XmlNode Tech_SEC_01 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_01");
                string _Tech_SEC_01 = WenDisprotects(Tech_SEC_01.InnerText.ToString());
                XmlNode Tech_SEC_02 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_02");
                string _Tech_SEC_02 = WenDisprotects(Tech_SEC_02.InnerText.ToString());
                XmlNode Tech_SEC_03 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_03");
                string _Tech_SEC_03 = WenDisprotects(Tech_SEC_03.InnerText.ToString());
                XmlNode Tech_SEC_04 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_04");
                string _Tech_SEC_04 = WenDisprotects(Tech_SEC_04.InnerText.ToString());
                XmlNode Tech_SEC_05 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_05");
                string _Tech_SEC_05 = WenDisprotects(Tech_SEC_05.InnerText.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("    ARQUIVO SecF_" + LojaCodigo + " INVÁLIDO OU CORROMPIDO\nSISTEMA NÃO PODERÁ PROSEGUIR E SERÁ FECHADO", "TechSIS Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                return true;
            }

            return false;
        }

        #endregion


        //VERIFICA AS INFORMAÇÕES NO ARQUIVO XML DadosServidor
        public void MET_VerificaServidorETC(ToolStripStatusLabel NomeServidorStriplbl, ToolStripStatusLabel NomeBancoStriplbl, ToolStripStatusLabel EServidorResStriplbl)
        {
            string CaminhoDoArquivo = (@"..\Conexão\DadosServidor.xml");
            if (File.Exists(CaminhoDoArquivo))
            {
                try
                {
                    XmlDocument LerXML = new XmlDocument();
                    LerXML.Load(CaminhoDoArquivo);

                    XmlNode Tipo_Instancia = LerXML.DocumentElement.SelectSingleNode("Tipo_Instancia");
                    XmlNode Desc_Servidor = LerXML.DocumentElement.SelectSingleNode("Desc_Servidor");
                    XmlNode Inst_Servidor = LerXML.DocumentElement.SelectSingleNode("Inst_Servidor");
                    XmlNode Banco_De_Dados = LerXML.DocumentElement.SelectSingleNode("Banco_De_Dados");
                    XmlNode MaquinaServidor = LerXML.DocumentElement.SelectSingleNode("MaquinaServidor");

                    string _Tipo_Instancia = Tipo_Instancia.InnerText.ToString();
                    string _Desc_Servidor = Desc_Servidor.InnerText.ToString();
                    string _Inst_Servidor = Inst_Servidor.InnerText.ToString();
                    string _Banco_De_Dados = Banco_De_Dados.InnerText.ToString();
                    string _MaquinaServidor = MaquinaServidor.InnerText.ToString();

                    if (_MaquinaServidor == "S")
                    {
                        EServidorResStriplbl.Text = "SIM";
                    }
                    else
                    {
                        EServidorResStriplbl.Text = "NÃO";
                    }


                    if (_Tipo_Instancia == "N")
                    {
                        NomeServidorStriplbl.Text = _Desc_Servidor + @"\" + _Inst_Servidor;
                        NomeBancoStriplbl.Text = _Banco_De_Dados;
                    }
                    else
                    {
                        NomeServidorStriplbl.Text = _Desc_Servidor;
                        NomeBancoStriplbl.Text = _Banco_De_Dados;
                    }
                }
                catch (XmlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerificaServidorETC()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS XML Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerificaServidorETC()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Arquivo (DadosServidor) não foi encontrado!", "TechSIS Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
            }
        }

        //VERIFICA A PERMISSÃO AO CLICAR NO TOOL DA JANELA
        public bool MET_VerificaPermissão(string NúmeroDoPrograma, string UsuarioLogado)
        {
            if (Convert.ToInt32(UsuarioLogado) != 1)
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();


                string strSelect = "SELECT SeqPgr_PER FROM TabPermi WHERE SeqUsu_PER = @SeqUsu_PER AND SeqPgr_PER = @SeqPgr_PER";
                SqlCommand cmdComand = new SqlCommand(strSelect, Conexão);
                cmdComand.Parameters.Add("@SeqUsu_PER", SqlDbType.Int).Value = UsuarioLogado;
                cmdComand.Parameters.Add("@SeqPgr_PER", SqlDbType.Int).Value = NúmeroDoPrograma;


                try
                {
                    SqlDataReader Dr = cmdComand.ExecuteReader(); Dr.Read();

                    if (!Dr.HasRows)
                    {
                        MessageBox.Show("VOCÊ NÃO TEM PERMISSÃO DE ACESSO PARA (" + NúmeroDoPrograma + ")", "TechSIS Permissões", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerificaPermissão()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.ExitThread();
                    return true;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerificaPermissão()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.ExitThread();
                    return true;
                }
                finally
                {
                    Conexão.Close();
                }
            }


            return false;
        }

        //METODOS PARA O METODO MAIN
        #region MÉTODO Main

        //VERIFICA SE A PASTA CONEXÃO EXISTE
        //VERIFICA SE O ARQUIVO STRINGCONEXÃO EXISTE
        //VERIFICA SE O ARQUIVO DADOS SERVIDOR
        public bool MET_VerificaArquivosDiretorios()
        {
            string DIRETORIO = ("..\\Conexão");

            System.Diagnostics.Process Proc = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo ProcINF = new System.Diagnostics.ProcessStartInfo("..\\Debug\\TechSIS_ConecBanco.exe");
            Proc.StartInfo = ProcINF;

            //VERIFICA SE O DIRETORIO CONEXÃO EXISTE
            #region DIRETÓRIO Conexão EXISTE?
            if (!Directory.Exists(DIRETORIO))
            {
                try
                {
                    Proc.Start();
                }
                catch (Exception)
                {
                    MessageBox.Show("Arquivo (TechSIS_ConecBanco.exe) inexistente!\nEXECUTE O INSTALADOR DO SISTEMA NOVAMENTE", "TechSIS ERRO DE INICIALIZAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return true;
            }
            #endregion

            //VERIFICA SE O ARQUIVO StringConexão.XML EXISTE
            #region ARQUIVO StringConexão EXISTE?
            if (!File.Exists(DIRETORIO + "\\StringConexão.xml"))
            {
                DialogResult Abrir = MessageBox.Show(@"ARQUIVO (" + DIRETORIO + "\\StringConexão.xml" + ") NÃO FOI LOCALIZADO!\n\nDESEJA ABRIR O INSTALADOR DO SISTEMA?", "TechSIS ERRO DE INICIALIZAÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (Abrir == DialogResult.Yes)
                {
                    try
                    {
                        Proc.Start();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Arquivo (TechSIS_ConecBanco.exe) inexistente!\nEXECUTE O INSTALADOR DO SISTEMA NOVAMENTE", "TechSIS ERRO DE INICIALIZAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {

                }

                return true;
            }
            #endregion

            //VERIFICA SE O ARQUIVO DadosServidor.XML EXISTE
            #region ARQUIVO DadosServidor EXISTE?
            if (!File.Exists(DIRETORIO + "\\DadosServidor.xml"))
            {
                DialogResult Abrir = MessageBox.Show(@"ARQUIVO (" + DIRETORIO + "\\DadosServidor.xml" + ") NÃO FOI LOCALIZADO!\n\nDESEJA ABRIR O INSTALADOR DO SISTEMA?", "TechSIS ERRO DE INICIALIZAÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (Abrir == DialogResult.Yes)
                {
                    try
                    {
                        Proc.Start();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Arquivo (TechSIS_ConecBanco.exe) inexistente!\nEXECUTE O INSTALADOR DO SISTEMA NOVAMENTE", "TechSIS ERRO DE INICIALIZAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {

                }

                return true;
            }
            #endregion

            //VERIFICA SE O ARQUIVO CfgComun.dll EXISTE
            #region DLL CfgComun EXISTE?
            if (!File.Exists("..\\Debug\\CfgComun.dll"))
            {
                MessageBox.Show(@"ARQUIVO (CfgComun.dll) NÃO FOI LOCALIZADO!", "TechSIS ERRO DE INICIALIZAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            #endregion


            return false;
        }

        //SUBSTITUI O ARQUIVO DE ATUALIZAÇÃO AUTOMÁTICA
        public void MET_SubsTechATU()
        {
            //Verifica se já está em execução
            Process[] Proc = Process.GetProcessesByName("TechSIS_UpdSIS");
            if (Proc.Length > 0)
            {

            }
            else
            {

                try
                {
                    //NOME DO EXE NA PASTA RAIZ DEBUG
                    string CaminhoDoArquivoDEBUG = "TechSIS_UpdSIS.exe";
                    string CaminhoDoArquivoTEMP = "..\\Temp\\TechSIS_UpdSIS.exe";

                    if (File.Exists(CaminhoDoArquivoTEMP))
                    {
                        //PEGA A DATA DENTRO DA PASTA TEMP
                        DateTime DATA_Renc = File.GetLastWriteTime(CaminhoDoArquivoTEMP);


                        if (File.Exists(CaminhoDoArquivoDEBUG))
                        {
                            DateTime DATA_Debug = File.GetLastWriteTime(CaminhoDoArquivoDEBUG);

                            if (DATA_Renc >= DATA_Debug)
                            {
                                //SE A DATA FOR MAIOR, COPIA O NOVO ATUALIZADOR
                                File.Copy(CaminhoDoArquivoTEMP, CaminhoDoArquivoDEBUG, true);
                            }
                        }
                        else
                        {
                            //SE O ARQUIVO NÃO EXISTE, COPIA O NOVO ATUALIZADOR
                            File.Copy(CaminhoDoArquivoTEMP, CaminhoDoArquivoDEBUG, true);
                        }
                    }


                    //INICIA O ATUALIZADOR
                    Process.Start(CaminhoDoArquivoDEBUG);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("NÃO FOI POSSÍVEL COPIAR O NOVO ATUALIZADOR!\n" + Ex.GetType().ToString() + "\n" + Ex.Message, "TechSIS ERRO MAIN()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //REALIZA A CONEXÃO DE TESTE
        public bool MET_ConecTESTEBanco()
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);

            System.Diagnostics.Process Proc = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo ProcINF = new System.Diagnostics.ProcessStartInfo("..\\Debug\\TechSIS_ConecBanco.exe");
            Proc.StartInfo = ProcINF;

            try
            {
                Conexão.Open();
                return false;
            }

            #region SQL Exception
            catch (SqlException Ex)
            {
                //INSTANCIA PARADA
                #region INSTANCIA PARADA
                if (Ex.Number == -1)
                {
                    if (File.Exists("..\\Conexão\\DadosServidor.xml"))
                    {
                        try
                        {
                            XmlDocument LerXML = new XmlDocument();
                            LerXML.Load("..\\Conexão\\DadosServidor.xml");
                            XmlNode DadosServidor_xml = LerXML.DocumentElement.SelectSingleNode("Inst_Servidor");
                            string INST = DadosServidor_xml.InnerText.ToString();

                            ServiceController Service = new ServiceController("MSSQL$" + INST);
                            //Se o status da instância for STOPPED, ela ficará STARTED 
                            if (Service.Status.Equals(ServiceControllerStatus.Stopped) || Service.Status.Equals(ServiceControllerStatus.StopPending) ||
                              Service.Status.Equals(ServiceControllerStatus.Paused) || Service.Status.Equals(ServiceControllerStatus.PausePending))
                            {
                                Service.Start(); //Starta o processo
                                Application.DoEvents();
                                Service.WaitForStatus(ServiceControllerStatus.Running); //Aguarda até que o processo fique startado definitivamente
                                MessageBox.Show("SERVIÇO INICIADO COM SUCESSO!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Application.Restart();
                            }
                        }
                        catch (Exception Exz)
                        {
                            MessageBox.Show("ERRO NA TENTATIVA DE INICIALIZAÇÃO DE SERVIÇO DO SQL\n\n" + Exz.Message, "TechSIS ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                #endregion
                else
                {
                    DialogResult Abrir = MessageBox.Show("CONEXÃO COM O BANCO DE DADOS NÃO FOI EFETUADA!\n\n" + Ex.Message + "\n\nDESEJA ABRIR O INSTALADOR DO SISTEMA?", "TechSIS ERRO DE INICIALIZAÇÃO.: " + Ex.Number, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (Abrir == DialogResult.Yes)
                    {
                        try
                        {
                            Proc.Start();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Arquivo (TechSIS_ConecBanco.exe) inexistente!\nEXECUTE O INSTALADOR DO SISTEMA NOVAMENTE", "TechSIS ERRO DE INICIALIZAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                return true;
            }
            #endregion
            #region Exception
            catch (Exception Ex)
            {
                DialogResult Abrir = MessageBox.Show("CONEXÃO COM O BANCO DE DADOS NÃO FOI EFETUADA!\n\n" + Ex.Message + "\n\nDESEJA ABRIR O INSTALADOR DO SISTEMA?", "TechSIS ERRO DE INICIALIZAÇÃO Exception", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (Abrir == DialogResult.Yes)
                {
                    try
                    {
                        Proc.Start();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Arquivo (TechSIS_ConecBanco.exe) inexistente!\nEXECUTE O INSTALADOR DO SISTEMA NOVAMENTE", "TechSIS ERRO DE INICIALIZAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return true;
            }
            #endregion
        }

        //VERIFICA SE O ARQUIVO DADOS SERVIDOR É VALIDO
        public bool MET_VerificaArquivosValidos()
        {
            System.Diagnostics.Process Proc = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo ProcINF = new System.Diagnostics.ProcessStartInfo("..\\Debug\\TechSIS_ConecBanco.exe");
            Proc.StartInfo = ProcINF;

            string CaminhoDoArquivo = (@"..\Conexão\DadosServidor.xml");
            if (File.Exists(CaminhoDoArquivo))
            {
                try
                {
                    XmlDocument LerXML = new XmlDocument();
                    LerXML.Load(CaminhoDoArquivo);

                    XmlNode Tipo_Instancia = LerXML.DocumentElement.SelectSingleNode("Tipo_Instancia");
                    XmlNode Desc_Servidor = LerXML.DocumentElement.SelectSingleNode("Desc_Servidor");
                    XmlNode Inst_Servidor = LerXML.DocumentElement.SelectSingleNode("Inst_Servidor");
                    XmlNode Banco_De_Dados = LerXML.DocumentElement.SelectSingleNode("Banco_De_Dados");
                    XmlNode UsuarioSQL = LerXML.DocumentElement.SelectSingleNode("UsuarioSQL");
                    XmlNode SenhaSQL = LerXML.DocumentElement.SelectSingleNode("SenhaSQL");

                    string _Tipo_Instancia = Tipo_Instancia.InnerText.ToString();
                    string _Desc_Servidor = Desc_Servidor.InnerText.ToString();
                    string _Inst_Servidor = Inst_Servidor.InnerText.ToString();
                    string _Banco_De_Dados = Banco_De_Dados.InnerText.ToString();
                    string _UsuarioSQL = WenDisprotects(UsuarioSQL.InnerText.ToString());
                    string _SenhaSQL = WenDisprotects(SenhaSQL.InnerText.ToString());

                    string StringConexão = "";



                    if (_Tipo_Instancia == "N")
                    {
                        StringConexão = "Server=" + _Desc_Servidor + "\\" + _Inst_Servidor + ";Database=" + _Banco_De_Dados + ";Uid=" + _UsuarioSQL + ";Pwd=" + _SenhaSQL;
                    }
                    else
                    {
                        StringConexão = "Server=" + _Desc_Servidor + ";Database=" + _Banco_De_Dados + ";Uid=" + _UsuarioSQL + ";Pwd=" + _SenhaSQL;
                    }


                    SqlConnection Conexão = new SqlConnection(StringConexão);

                    try
                    {
                        Conexão.Open();
                    }
                    catch (Exception)
                    {
                        DialogResult Abrir = MessageBox.Show("CONEXÃO COM O ARQUIVO DE DADOS NÃO FOI EFETUADA\n\nDESEJA ABRIR O INSTALADOR DO SISTEMA?", "TechSIS ERRO DE INICIALIZAÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (Abrir == DialogResult.Yes)
                        {
                            try
                            {
                                Proc.Start();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Arquivo (TechSIS_ConecBanco.exe) inexistente!\nEXECUTE O INSTALADOR DO SISTEMA NOVAMENTE", "TechSIS ERRO DE INICIALIZAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return true;
                            }
                        }
                        return true;
                    }
                }
                catch (XmlException Ex)
                {
                    DialogResult Abrir = MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerificaArquivosValidos()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message + "\n\nDESEJA ABRIR O INSTALADOR DO SISTEMA?", "TechSIS ERRO DE INICIALIZAÇÃO - XML", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (Abrir == DialogResult.Yes)
                    {
                        try
                        {
                            Proc.Start();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Arquivo (TechSIS_ConecBanco.exe) inexistente!\nEXECUTE O INSTALADOR DO SISTEMA NOVAMENTE", "TechSIS ERRO DE INICIALIZAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return true;
                        }
                    }
                    return true;
                }
                catch (Exception Ex)
                {
                    DialogResult Abrir = MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerificaArquivosValidos()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message + "\n\nDESEJA ABRIR O INSTALADOR DO SISTEMA?", "TechSIS ERRO DE INICIALIZAÇÃO - EXC", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (Abrir == DialogResult.Yes)
                    {
                        try
                        {
                            Proc.Start();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Arquivo (TechSIS_ConecBanco.exe) inexistente!\nEXECUTE O INSTALADOR DO SISTEMA NOVAMENTE", "TechSIS ERRO DE INICIALIZAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return true;
                        }
                    }
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Arquivo (DadosServidor) não foi encontrado!", "TechSIS Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }


            return false;
        }

        #endregion

        //VERIFICA SE A IMAGEM DE FUNDO DO FORMULÁRIO EXISTE
        public void MET_VerificaImagemFundo(Form FORMU)
        {
            try
            {
                if (!Directory.Exists("..\\Imagens"))
                {
                    Directory.CreateDirectory("..\\Imagens");
                }

                string CaminhoImagem = @"..\Imagens\TechSIS Unic Soft.png";

                if (File.Exists(CaminhoImagem))
                {
                    FORMU.BackgroundImage = Image.FromFile(CaminhoImagem);
                }
                else
                {
                    System.IO.DirectoryInfo Info = new DirectoryInfo("..\\Imagens");
                    string DIRETO = Info.FullName;
                    MessageBox.Show("IMAGEM DE FUNDO DO SISTEMA NÃO FOI ECONTRADA NA PASTA\n\n\t    IMAGEM DEVE ESTAR NO DIRETÓRIO\n\n" + DIRETO, "TechSIS Unic Soft.png NÃO ENCONTRADA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerificaImagemFundo()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //SELECIONA OPÇÕES DE ATALHO NO PAINEL
        public void MET_AtalhosPanelOpcoes(string Codigo_Loja, Button btnAtalhoPan1, Button btnAtalhoPan2)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string strATALHOS = "SELECT NomAt1_CON, NomAt2_CON, NomDl1_CON, NomDl2_CON FROM TabConfi WHERE SeqLoj_CON = " + Codigo_Loja;
            SqlCommand Comando = new SqlCommand(strATALHOS, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    btnAtalhoPan1.Text = Dr["NomAt1_CON"].ToString();
                    btnAtalhoPan2.Text = Dr["NomAt2_CON"].ToString();
                }
                else
                {

                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_AtalhosPanelOpcoes()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_AtalhosPanelOpcoes()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //VERIFICAR SE PAINEL É VISIVEL AO LOGAR
        public void MET_VerPanVISIVEL(Panel Painel_Opcoes, string Codigo_Loja, Label lblOpcoes)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string strATALHOS = "SELECT OcuPan_CON FROM TabConfi WHERE SeqLoj_CON = " + Codigo_Loja;
            SqlCommand Comando = new SqlCommand(strATALHOS, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string VISIVEL = Dr["OcuPan_CON"].ToString();
                    if (VISIVEL.ToUpper() == "TRUE")
                    {
                        Painel_Opcoes.Visible = true;
                        lblOpcoes.Text = "OCULTAR";
                    }
                    else
                    {
                        Painel_Opcoes.Visible = false;
                        lblOpcoes.Text = "OPÇÕES";
                    }
                }
                else
                {

                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerPanVISIVEL()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerPanVISIVEL()\n\nBLOCO = TechSIS_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }


        #region VERIFICA SE EXISTE LINHA DE CONFIGURAÇÃO


        //INSERE A CONFIGURAÇÃO DA LOJA 1
        public bool Conec_InsertConfig(string LojaLogada)
        {
            if (!File.Exists(@"..\\Debug\SecF_" + LojaLogada + ".xml"))
            {
                MessageBox.Show("Segurança.: Arquivo SecF_" + LojaLogada + ".xml não foi localizado.", "TechSIS Erro FATAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                return true;
            }



            #region BLOCO 1 = INSERE AS INFORMAÇÕES DA CONFIGURAÇÃO
            else
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                try
                {

                    string Comando_INSERIR = "INSERT INTO TabConfi (SeqLoj_CON,CorFun_CON,QtPesq_CON,CamRel_CON,NomAt1_CON,NomAt2_CON,PlayMu_CON,VerBco_CON,GraXML_CON) VALUES (" + LojaLogada + ",'White','30','C:\\TechSIS INF\\Relatórios\\','ATALHO 01','ATALHO 02','False','1.0.0','True')";
                    SqlCommand _Comando_INSERIR = new SqlCommand(Comando_INSERIR, Conexão);


                    _Comando_INSERIR.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_InsertConfig\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.ExitThread();
                    return true;
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion

            return false;
        }
        //REMOVO NULOS DA TABLEA
        public bool Conec_RemoverNuloConfig()
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string strComand = "USE TechSIS_INF SELECT COLUNAS.NAME AS NOME, COLUNAS.type AS TIPO FROM SYSOBJECTS AS TABELAS, SYSCOLUMNS AS COLUNAS WHERE TABELAS.ID = COLUNAS.ID AND TABELAS.NAME = 'TabConfi' AND COLUNAS.isnullable = '1'";
            SqlCommand Comando = new SqlCommand(strComand, Conexão);
            List<string> COLUNA = new List<string>();
            List<string> TIPOS = new List<string>();
            using (SqlDataReader Dr = Comando.ExecuteReader())
            {
                while (Dr.Read())
                {
                    COLUNA.Add(Dr[0].ToString());
                    TIPOS.Add(Dr[1].ToString());
                }
            }


            try
            {
                //108 = INT
                //39 = VARCHAR
                //106 = DECIMAL
                for (int i = 0; i < COLUNA.Count; i++)
                {
                    string NomeColuna = COLUNA[i];
                    string strUpdate = "UPDATE TabConfi SET " + NomeColuna + " = @Parametro";
                    SqlCommand UpdateCmd = new SqlCommand(strUpdate, Conexão);

                    #region PARAMETROS
                    if (TIPOS[i] == "39")
                    {
                        //VARCHAR
                        UpdateCmd.Parameters.Add("@Parametro", SqlDbType.VarChar).Value = string.Empty;
                    }
                    else if (TIPOS[i] == "108")
                    {
                        //INT
                        UpdateCmd.Parameters.Add("@Parametro", SqlDbType.Int).Value = 0;
                    }
                    else if (TIPOS[i] == "106")
                    {
                        //DECIMAL
                        UpdateCmd.Parameters.Add("@Parametro", SqlDbType.Decimal).Value = 0;
                    }
                    else
                    {
                        //DATA
                        UpdateCmd.Parameters.Add("@Parametro", SqlDbType.DateTime).Value = DateTime.MinValue;
                    }
                    #endregion

                    UpdateCmd.ExecuteNonQuery();
                }
            }

            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_RemoverNuloConfig\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.ExitThread();
                return true;
            }
            finally
            {
                Conexão.Close();
            }


            return false;

        }


        #endregion

        //VERIFICA SE EXISTE 1 LINHA DE HISTÓRICO E 1 LINHA DE CONFIGURAÇÃO GERAL
        public void MET_VerHISTVerCONF(int IDMet, string LojaLogada)
        {
            #region HISTÓRICO
            if (IDMet == 1)
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string strComando = "SELECT * FROM TabHisto WHERE Sequen_HIS = 1";
                SqlCommand Comando = new SqlCommand(strComando, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        Dr.Close();
                    }
                    else
                    {
                        try
                        {
                            Dr.Close();

                            string strComandoINSERT = "INSERT INTO TabHisto VALUES (1,000000,'SYSTEM','TechSIS INF','Configuração Inicial',1,GETDATE())";
                            SqlCommand ComandoINSERT = new SqlCommand(strComandoINSERT, Conexão);

                            ComandoINSERT.ExecuteNonQuery();
                        }
                        catch (SqlException Ex)
                        {
                            MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerHISTVerCONF()\n\nBLOCO = INSERT GENÉRICO NO HISTÓRICO\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerHISTVerCONF()\n\nBLOCO = INSERT GENÉRICO NO HISTÓRICO\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerHISTVerCONF()\n\nBLOCO = HISTÓRICO\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerHISTVerCONF()\n\nBLOCO = HISTÓRICO\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
            #region CONFIGURAÇÃO
            if (IDMet == 2)
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string strComando = "SELECT * FROM TabConfi WHERE SeqLoj_CON = " + LojaLogada;
                SqlCommand Comando = new SqlCommand(strComando, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        Dr.Close();
                    }
                    else
                    {
                        try
                        {
                            Dr.Close();

                            bool InsertConf = Conec_InsertConfig(LojaLogada);
                            bool ConfNull = Conec_RemoverNuloConfig();
                            if (!InsertConf) { } else { return; }
                            if (!ConfNull) { } else { return; }
                        }
                        catch (SqlException Ex)
                        {
                            MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerHISTVerCONF()\n\nBLOCO = INSERT GENÉRICO CONFIGURAÇÃO\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerHISTVerCONF()\n\nBLOCO = INSERT GENÉRICO CONFIGURAÇÃO\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerHISTVerCONF()\n\nBLOCO = CONFIGURAÇÃO\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerHISTVerCONF()\n\nBLOCO = CONFIGURAÇÃO\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
        }
    }
}