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

namespace TechSIS_AddEmpre
{
    internal class AddEmpre_MET
    {
        #region CRIPTOGRAFIA
        private static string WenProtects(string Message)
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
        private static string WenDisprotects(string Message)
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
        #region RESULTADOS
        internal string _SecF_EmpresaCod { get; set; }
        internal string _SecF_EmpresaRaz { get; set; }
        internal string _SecF_EmpresaFan { get; set; }
        internal string _SecF_EmpresaCpf { get; set; }
        internal string _SecF_ModuloPrin { get; set; }
        internal string _SecF_ModuloProv { get; set; }
        internal string _SecF_SISTEMA { get; set; }
        #endregion
        #region CONTROLES
        public TextBox txtEmpresa { get; set; }
        public TextBox txtRazao { get; set; }
        public MaskedTextBox mtbCpfCnpj { get; set; }
        public TextBox txtFantasia { get; set; }
        public ComboBox comModulo { get; set; }
        public ComboBox comModuloProvi { get; set; }
        public ComboBox comSistema { get; set; }
        public Button btnIncluir { get; set; }
        #endregion


        //VERIFICA ARQUIVOS NECESSARIOS
        public bool MET_ArquivoNecessarios()
        {
            string DIRETORIO = "..\\Conexão";

            //VERIFICA SE O DIRETORIO CONEXÃO EXISTE
            #region DIRETÓRIO Conexão EXISTE?
            if (!Directory.Exists(DIRETORIO))
            {
                MessageBox.Show("ARQUIVO(S) DE INICIALIZAÇÃO DE ASSEMBLY NÃO ENCONTRATO(S)", "TechSIS ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            #endregion

            //VERIFICA SE O ARQUIVO StringConexão.XML EXISTE
            #region ARQUIVO StringConexão EXISTE?
            if (!File.Exists(DIRETORIO + "\\StringConexão.xml"))
            {
                MessageBox.Show("ARQUIVO(S) DE INICIALIZAÇÃO DE ASSEMBLY NÃO ENCONTRATO(S)", "TechSIS ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            #endregion

            //VERIFICA SE O ARQUIVO DadosServidor.XML EXISTE
            #region ARQUIVO DadosServidor EXISTE?
            if (!File.Exists(DIRETORIO + "\\DadosServidor.xml"))
            {
                MessageBox.Show("ARQUIVO(S) DE INICIALIZAÇÃO DE ASSEMBLY NÃO ENCONTRATO(S)", "TechSIS ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            #endregion

            //VERIFICA SE O ARQUIVO CfgComun.dll EXISTE
            #region DLL CfgComun EXISTE?
            if (!File.Exists("..\\Debug\\CfgComun.dll"))
            {
                MessageBox.Show(@"ARQUIVO (CfgComun.dll) NÃO FOI LOCALIZADO!", "TechSIS ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            #endregion
            return false;  
        }



        //VERIFICA SE O ARQUIVO É VÁLIDO
        public bool MET_ArquivoValido(string CaminhoArquivo)
        {
            try
            {
                XmlDocument LerXML = new XmlDocument();
                LerXML.Load(CaminhoArquivo);
                //RAZÃO
                XmlNode Tech_SEC_01 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_01");
                //FANTASIA
                XmlNode Tech_SEC_02 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_02");
                //CPF OU CNPJ
                XmlNode Tech_SEC_03 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_03");
                //DATA E ARQUIVO
                XmlNode Tech_SEC_05 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_05");


                string _Tech_SEC_01 = WenDisprotects(Tech_SEC_01.InnerText.ToString());
                string _Tech_SEC_02 = WenDisprotects(Tech_SEC_02.InnerText.ToString());
                string _Tech_SEC_03 = WenDisprotects(Tech_SEC_03.InnerText.ToString());
                string _Tech_SEC_05 = WenDisprotects(Tech_SEC_05.InnerText.ToString());

                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Arquivo selecionado não é um XML SecF válido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
        }

        //POPULO AS STRINGS
        public void MET_PopularStrings(string CaminhoArquivo)
        {
            try
            {
                XmlDocument LerXML = new XmlDocument();
                LerXML.Load(CaminhoArquivo);
                //RAZÃO
                XmlNode Tech_SEC_01 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_01");
                //FANTASIA
                XmlNode Tech_SEC_02 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_02");
                //CPF OU CNPJ
                XmlNode Tech_SEC_03 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_03");
                //DATA E ARQUIVO
                XmlNode Tech_SEC_05 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_05");


                string _Tech_SEC_01 = WenDisprotects(Tech_SEC_01.InnerText.ToString());
                string _Tech_SEC_02 = WenDisprotects(Tech_SEC_02.InnerText.ToString());
                string _Tech_SEC_03 = WenDisprotects(Tech_SEC_03.InnerText.ToString());
                string _Tech_SEC_05 = WenDisprotects(Tech_SEC_05.InnerText.ToString());

                _SecF_EmpresaCod = (Convert.ToInt32(_Tech_SEC_05.Substring(19, 2)) - 14).ToString("000000");
                _SecF_EmpresaRaz = _Tech_SEC_01;
                _SecF_EmpresaFan = _Tech_SEC_02;
                _SecF_EmpresaCpf = _Tech_SEC_03;
                _SecF_ModuloPrin = (Convert.ToInt32(_Tech_SEC_05.Substring(21, 2)) - 7).ToString("00");
                _SecF_ModuloProv = (Convert.ToInt32(_Tech_SEC_05.Substring(23, 2)) - 15).ToString("00");
                _SecF_SISTEMA = (Convert.ToInt32(_Tech_SEC_05.Substring(25, 2)) - 3).ToString("00");

            }
            catch (Exception)
            {
                MessageBox.Show("ERRO NÃO TRATADO AO SELECIONAR VALORES (1)", "TechSIS ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //POPULOS OS CONTROLES
        public void MET_PopularControles()
        {
            try
            {
                txtEmpresa.Text = _SecF_EmpresaCod;
                txtRazao.Text = _SecF_EmpresaRaz;
                txtFantasia.Text = _SecF_EmpresaFan;
                mtbCpfCnpj.Text = _SecF_EmpresaCpf;
                #region MASCARA CPF CNPJ
                if (_SecF_EmpresaCpf.Length == 14)
                {
                    mtbCpfCnpj.Mask = "00,000,000/0000-00";
                }
                else
                {
                    mtbCpfCnpj.Mask = "000,000,000-00";
                }
                #endregion

                comModulo.SelectedIndex = Convert.ToInt32(_SecF_ModuloPrin);
                comModuloProvi.SelectedIndex = Convert.ToInt32(_SecF_ModuloProv);
                comSistema.SelectedIndex = Convert.ToInt32(_SecF_SISTEMA);

                btnIncluir.Enabled = true;
                btnIncluir.Select();
            }
            catch (Exception)
            {
                MessageBox.Show("ERRO NÃO TRATADO AO SELECIONAR VALORES (2)", "TechSIS ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        //INSERE A EMPRESA 1 NO BANCO DE DADOS
        public bool Conec_InsertEmpresa(string CaminhoDoArquivo)
        {
            if (!File.Exists(CaminhoDoArquivo))
            {
                MessageBox.Show("Segurança.: Arquivo SecF não foi localizado.", "TechSIS Erro FATAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            #region BLOCO 1 = INSERE AS INFORMAÇÕES DA EMPRESA NO BANCO DE DADOS
            else
            {
                try
                {
                    //Cria a conexão com o Banco de Dados e Abre!
                    StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                    string LerString = StringConexão.ReadLine();
                    SqlConnection Conexão = new SqlConnection(LerString);
                    Conexão.Open();

                    XmlDocument LerXML = new XmlDocument();
                    LerXML.Load(CaminhoDoArquivo);

                    //RAZÃO
                    XmlNode Tech_SEC_01 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_01");
                    //FANTASIA
                    XmlNode Tech_SEC_02 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_02");
                    //CPF CNPJ
                    XmlNode Tech_SEC_03 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_03");
                    //EMPRESA
                    XmlNode Tech_SEC_05 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_05");


                    string _Tech_SEC_01 = WenDisprotects(Tech_SEC_01.InnerText.ToString());
                    string _Tech_SEC_02 = WenDisprotects(Tech_SEC_02.InnerText.ToString());
                    string _Tech_SEC_03 = WenDisprotects(Tech_SEC_03.InnerText.ToString());
                    string _Tech_SEC_05 = WenDisprotects(Tech_SEC_05.InnerText.ToString());

                    int _CodigoEmp = Convert.ToInt32(_Tech_SEC_05.Substring(19, 2)) - 14;


                    string Comando_INSERIR = @"USE TechSIS_INF INSERT INTO TabEmpre (Sequen_EMP,Descri_EMP,Fantas_EMP,Tipo01_EMP,EndCid_EMP,CpfCnp_EMP,RegTri_EMP,NumCai_EMP,DtVeEs_EMP,DtVeMu_EMP,DatCad_EMP) VALUES (@Sequen_EMP,@Descri_EMP,@Fantas_EMP,@Tipo01_EMP,@EndCid_EMP,@CpfCnp_EMP,1,3,@DATA1,@DATA2,GETDATE()) USE MASTER";

                    SqlCommand _Comando_INSERIR = new SqlCommand(Comando_INSERIR, Conexão);

                    _Comando_INSERIR.Parameters.Add("@Sequen_EMP", SqlDbType.Int).Value = _CodigoEmp;
                    _Comando_INSERIR.Parameters.Add("@Descri_EMP", SqlDbType.VarChar).Value = _Tech_SEC_01;
                    _Comando_INSERIR.Parameters.Add("@Fantas_EMP", SqlDbType.VarChar).Value = _Tech_SEC_02;
                    _Comando_INSERIR.Parameters.Add("@CpfCnp_EMP", SqlDbType.VarChar).Value = _Tech_SEC_03;
                    #region _Comando_INSERIR.Parameters.Add("@Tipo01_EMP", SqlDbType.Int).Value = "1";
                    if (_Tech_SEC_03.Length == 14)
                    {
                        _Comando_INSERIR.Parameters.Add("@Tipo01_EMP", SqlDbType.Int).Value = "1";
                    }
                    else
                    {
                        _Comando_INSERIR.Parameters.Add("@Tipo01_EMP", SqlDbType.Int).Value = "0";
                    }
                    _Comando_INSERIR.Parameters.Add("@CpfCnp", SqlDbType.VarChar).Value = _Tech_SEC_03;
                    #endregion
                    _Comando_INSERIR.Parameters.Add("@EndCid_EMP", SqlDbType.Int).Value = 1;

                    _Comando_INSERIR.Parameters.Add("@DATA1", SqlDbType.VarChar).Value = DateTime.MinValue;
                    _Comando_INSERIR.Parameters.Add("@DATA2", SqlDbType.VarChar).Value = DateTime.MinValue;

                    _Comando_INSERIR.ExecuteNonQuery();
                }
                catch (SqlException Ex)
                {
                    if (Ex.Number == 2627)
                    {
                        MessageBox.Show("Empresa já cadastrada no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_InsertEmpresa\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }
            #endregion

            return false;
        }

        //REMOVO NULOS DA TABELA
        public bool Conec_RemoverNuloEmpresa()
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string strComand = "USE TechSIS_INF SELECT COLUNAS.NAME AS NOME, COLUNAS.type AS TIPO FROM SYSOBJECTS AS TABELAS, SYSCOLUMNS AS COLUNAS WHERE TABELAS.ID = COLUNAS.ID AND TABELAS.NAME = 'TabEmpre' AND COLUNAS.isnullable = '1'";
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
                    string strUpdate = "UPDATE TabEmpre SET " + NomeColuna + " = @Parametro";
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
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_RemoverNuloEmpresa\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            finally
            {
                Conexão.Close();
            }


            return false;
        }

        //INSERE A CONFIGURAÇÃO DA LOJA 1
        public bool Conec_InsertConfig(string CaminhoDoArquivo)
        {
            if (!File.Exists(CaminhoDoArquivo))
            {
                MessageBox.Show("Segurança.: Arquivo SecF não foi localizado.", "TechSIS Erro FATAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            #region BLOCO 1 = INSERE AS INFORMAÇÕES DA CONFIGURAÇÃO
            else
            {
                try
                {
                    //Cria a conexão com o Banco de Dados e Abre!
                    StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                    string LerString = StringConexão.ReadLine();
                    SqlConnection Conexão = new SqlConnection(LerString);
                    Conexão.Open();


                    string Comando_INSERIR = "USE TechSIS_INF INSERT INTO TabConfi (SeqLoj_CON,CorFun_CON,QtPesq_CON,CamRel_CON,NomAt1_CON,NomAt2_CON,PlayMu_CON,VerBco_CON,GraXML_CON) VALUES (" + _SecF_EmpresaCod + ",'White','30','C:\\TechSIS INF\\Relatórios','ATALHO 01','ATALHO 02','False','1.0.0','True') USE MASTER";
                    SqlCommand _Comando_INSERIR = new SqlCommand(Comando_INSERIR, Conexão);


                    _Comando_INSERIR.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_InsertConfig\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
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
                return true;
            }
            finally
            {
                Conexão.Close();
            }


            return false;

        }
    }
}
