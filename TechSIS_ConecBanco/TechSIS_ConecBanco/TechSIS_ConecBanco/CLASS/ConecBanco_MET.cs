using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;
using System.Xml;
using System.ServiceProcess;



namespace TechSIS_ConecBanco
{
    internal class ConecBanco_MET
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

        public string StringConexão { get; set; }
        public string txtServidor
        {
            get;
            set;
        }
        public string comInstancia
        {
            get;
            set;
        }
        public string VersãoSQL { get; set; }

        //DEFINE QUAL CAMPO PODE RECEBER APENAS NÚMEROS
        public void MET_ApenasNúmeros(KeyPressEventArgs KeyPress)
        {
            if (!Char.IsDigit(KeyPress.KeyChar) && KeyPress.KeyChar != (char)8)
            {
                KeyPress.Handled = true;
                return;
            }
        }

        //VERIFICA OS ARQUIVOS NECESSARIOS PARA A INSTALAÇÃO
        public bool Conec_VerificaArquNece()
        {
            String ArquivosFALT = "";

            #region Arquivos
            if (!Directory.Exists("..\\Tech FILEs"))
            {
                ArquivosFALT += "\b   Diretório (Tech FILEs)\n";
            }
            if (!Directory.Exists("..\\Scripts"))
            {
                ArquivosFALT += "\b   Diretório (Scripts)\n";
            }

            if (!File.Exists("..\\Tech FILEs\\0 - Municipios.tech"))
            {
                ArquivosFALT += "\b   Arquivo (0 - Municipios)\n";
            }
            if (!File.Exists("..\\Tech FILEs\\1 - Paises.tech"))
            {
                ArquivosFALT += "\b   Arquivo (1 - Paises)\n";
            }
            if (!File.Exists("..\\Tech FILEs\\2 - Ncm TechSIS.tech"))
            {
                ArquivosFALT += "\b   Arquivo (2 - Ncm TechSIS)\n";
            }

            if (!File.Exists("..\\Scripts\\Script 1 - Exec das Tabelas.sql"))
            {
                ArquivosFALT += "\b   Arquivo (Script 1 - Exec das Tabelas)\n";
            }
            if (!File.Exists("..\\Scripts\\Script 2 - Exec dos INSERTs.sql"))
            {
                ArquivosFALT += "\b   Arquivo (Script 2 - Exec dos INSERTs)\n";
            }
            if (!File.Exists("..\\Scripts\\Script 3 - Exec das FKs.sql"))
            {
                ArquivosFALT += "\b   Arquivo (Script 3 - Exec das FKs)\n";
            }
            if (!File.Exists("..\\Scripts\\Script 4 - Tabela de Programas.sql"))
            {
                ArquivosFALT += "\b   Arquivo (Script 4 - Tabela de Programas)\n";
            }

            if (!File.Exists("..\\Debug\\SecF_01.xml"))
            {
                ArquivosFALT += "\b   Arquivo (SecF_01)\n";
            }

            if (!File.Exists("..\\Debug\\TechSIS_BWK.exe"))
            {
                ArquivosFALT += "\b   Executável (TechSIS_BWK)\n";
            }
            #endregion


            if (ArquivosFALT != string.Empty)
            {
                MessageBox.Show("Atenção.: foi detectado a falta de arquivos necessários para a instalação\n\n" + ArquivosFALT + "\n\nPara resolver o problema, execute o instalador novamente.", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
           

            return false;
        }

        //POPULO O COMBOBOX COM AS INSTANCIAS
        public bool Conec_PopuloComboBoxInstancias(ComboBox comInstancia)
        {
            string Mensagem_Erro_SQL = "INSTÂNCIA DO MS SQL Server NÃO FOI ENCONTRADA.\n\nNenhuma instância foi encontrada atravéz da busca automática do software, isso significa que o Microsoft® SQL Server® não foi instalado, ou foi instalado de forma incorreta. No momento da instalação, verifique se o item 'Mecanismo de Banco de Dados' foi marcado. A reinstalação do SQL pode resolver o problema.";

            // LISTA AS INSTANCIAS DEPENDENDO DA ARQUITETURA DO WINDOWS
            try
            {
                #region x64
                if (Directory.Exists(Environment.GetEnvironmentVariable("ProgramFiles(x86)")))
                {
                    var Machine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                    var SubK = Machine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
                    String[] InstInsta = (String[])SubK.GetValue("InstalledInstances");

                    int NumInst = InstInsta.Length;
                    if (NumInst < 1)
                    {
                        //DEFINO SE EXISTE INTANCIA INSTALADA
                        MessageBox.Show(Mensagem_Erro_SQL, "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                    for (int i = 0; i < NumInst; i++)
                    {
                        comInstancia.Items.Insert(i, InstInsta[i]);
                    }
                }
                #endregion
                #region x32
                else
                {
                    Microsoft.Win32.RegistryKey REGEDIT;
                    REGEDIT = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server", false);
                    String[] InstInsta;
                    InstInsta = ((String[])REGEDIT.GetValue("InstalledInstances"));

                    int NumInst = InstInsta.Length;
                    if (NumInst < 1)
                    {
                        //DEFINO SE EXISTE INTANCIA INSTALADA
                        MessageBox.Show(Mensagem_Erro_SQL, "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                    for (int i = 0; i < NumInst; i++)
                    {
                        comInstancia.Items.Insert(i, InstInsta[i]);
                    }
                }
                #endregion
            }
            catch (Exception)
            {
                DialogResult FazerDown = MessageBox.Show(Mensagem_Erro_SQL + "\n\nDESEJA FAZER O DOWNLOAD DE UMA VERSÃO COMPATÍVEL?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (FazerDown == DialogResult.Yes)
                {
                    if (Directory.Exists(Environment.GetEnvironmentVariable("ProgramFiles(x86)")))
                    {
                        System.Diagnostics.Process.Start("http://download.microsoft.com/download/8/D/D/8DD7BDBA-CEF7-4D8E-8C16-D9F69527F909/ENU/x64/SQLEXPR_x64_ENU.exe");
                    }
                    else
                    {
                        System.Diagnostics.Process.Start("http://download.microsoft.com/download/8/D/D/8DD7BDBA-CEF7-4D8E-8C16-D9F69527F909/ENU/x86/SQLEXPR_x86_ENU.exe");
                    }
                }
                else
                {
                    MessageBox.Show("A instalação será encerrada devido a falta de pré-requisitos", "TechSIS AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.ExitThread();
                }
                return true;
            }

            return false;
        }

        //DEFINE A STRING DE CONEXÃO
        public string Conec_StringDeConexão()
        {
            if (comInstancia == "MSSQLSERVER" || comInstancia == string.Empty || comInstancia == "LOCAL")
            {
                StringConexão = @"Server=" + txtServidor + ";Database=master;Trusted_Connection=Yes";
            }
            else
            {
                StringConexão = @"Server=" + txtServidor + @"\" + comInstancia + ";Database=master;Trusted_Connection=Yes";
            }

            return StringConexão;
        }

        //CRIO A CONEXÃO COM O BANCO DE DADOS
        public bool Conec_ConexãoSQLServer(SqlConnection Conexão)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            Conexão.ConnectionString = Conec_StringDeConexão();

            try
            {
                Conexão.Open();
            }
            catch (SqlException Ex)
            {
                DialogResult Detalhes = MessageBox.Show("TechSIS.: Um falha foi detectada ao fazer a conexão com o SQL\n\n Deseja ver detalhes do erro?","TechSIS ERRO FATAL", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (Detalhes == DialogResult.Yes)
                {
                    MessageBox.Show("TechSIS.: SQL Erro " + Ex.Number + "\n" + Ex.Message + "\n" + Ex.GetType().ToString() + "\n\nFIM DO ERRO", "TechSIS DETALHAMENTO DE ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("A instalação não poderá prosseguir e será fechada", "TechSIS ERRO FATAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return true;
            }
            catch (Exception Ex)
            {
                DialogResult Detalhes = MessageBox.Show("TechSIS.: Um falha foi detectada ao fazer a conexão com o SQL\n\n Deseja ver detalhes do erro?", "TechSIS ERRO FATAL", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (Detalhes == DialogResult.Yes)
                {
                    MessageBox.Show("TechSIS.: Exception\n" + Ex.Message + "\n" + Ex.GetType().ToString() + "\n\nFIM DO ERRO", "TechSIS DETALHAMENTO DE ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("A instalação não poderá prosseguir e será fechada", "TechSIS ERRO FATAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return true;
            }
            finally
            {
                Conexão.Close();
            }


            return false;
        }

        //HABILITO O MODO MISTO DO SQL E PEGO A VERSÃO INSTALADO DO SQL
        public bool Conec_ChangeModoSQL(ComboBox comInstancia, Form Form, TextBox txtServidor)
        {
            VersãoSQL = "";

            #region BLOCO 1 = PEGA A VERSÃO DO SQL INSTALADA

            //Cria a conexão com o Banco de Dados e Abre!
            StringConexão = @"Server=" + txtServidor.Text + @"\" + comInstancia.Text + ";Database=master;Trusted_Connection=Yes";
            if (comInstancia.Text == "MSSQLSERVER")
            {
                StringConexão = @"Server=" + txtServidor.Text + ";Database=master;Trusted_Connection=Yes";
            }


            //Cria a conexão com o Banco de Dados e Abre!
            SqlConnection Conexão = new SqlConnection(StringConexão);
            Conexão.Open();

            string StringComando = "SELECT  SERVERPROPERTY('productversion') AS Versão, SERVERPROPERTY ('productlevel') AS Pack, SERVERPROPERTY ('edition') AS Edição";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    //tratamento R2
                    if (Dr["Versão"].ToString().Substring(0, 5) == "10.50")
                    {
                        VersãoSQL = "10_50";
                    }
                    else
                    {
                        VersãoSQL = Dr["Versão"].ToString().Substring(0, 2);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_ChangeModoSQL\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            finally
            {
                Conexão.Close();
            }
            #endregion


            #region BLOCO 2 = ALTERA A CHAVE "LoginMode" PARA 2 - Modo Misto
            //BLOCO CATCH 2
            try
            {
                if (Directory.Exists(Environment.GetEnvironmentVariable("ProgramFiles(x86)")))
                {
                    var Machine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                    var SubK = Machine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server\\MSSQL" + VersãoSQL + "." + comInstancia.Text + "\\MSSQLServer\\", true);
                    SubK.SetValue("LoginMode", "2", RegistryValueKind.DWord);
                }
                else
                {
                    Microsoft.Win32.RegistryKey REGEDIT;
                    REGEDIT = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server\\MSSQL" + VersãoSQL + "." + comInstancia.Text + "\\MSSQLServer\\", true);
                    REGEDIT.SetValue("LoginMode", "2", RegistryValueKind.DWord);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_ChangeModoSQL\n\n\b BLOCO DO TRATAMENTO = 02\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            #endregion


            //Pega a instância do SQL Server Express do Cliente
            Application.DoEvents();
            string NomeINST = "MSSQL$" + comInstancia.Text;
            if (comInstancia.Text == "MSSQLSERVER")
            {
                NomeINST = "MSSQLSERVER";
            }

            ServiceController Service = new ServiceController(NomeINST);

            #region BLOCO 3 = PARA E INICIA A INSTÂNCIA DO SQL SERVER
            //BLOCO CATCH 2
            try
            {
                #region PARA O SERVIÇO
                //Se o status da instância for STOPPED, ela ficará STARTED 
                if (Service.Status.Equals(ServiceControllerStatus.Running) || Service.Status.Equals(ServiceControllerStatus.Paused))
                {
                    Service.Stop(); //Para o processo
                    Application.DoEvents();
                    Service.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                #endregion
                #region INICIA O SERVIÇO
                //Se o status da instância for STOPPED, ela ficará STARTED 
                if (Service.Status.Equals(ServiceControllerStatus.Stopped) || Service.Status.Equals(ServiceControllerStatus.StopPending) ||
                  Service.Status.Equals(ServiceControllerStatus.Paused) || Service.Status.Equals(ServiceControllerStatus.PausePending))
                {
                    Service.Start(); //Starta o processo
                    Application.DoEvents();
                    Service.WaitForStatus(ServiceControllerStatus.Running); //Aguarda até que o processo fique startado definitivamente
                }
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_ChangeModoSQL\n\n\b BLOCO DO TRATAMENTO = 03\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            #endregion

            return false;
        }

        //HABILITO O USUÁRIO SA E DEFINO SUA SENHA
        public bool Conec_HabilUsuarSaDefineSenha(SqlConnection Conexão, string Senha)
        {
            try
            {
                Conexão.ConnectionString = Conec_StringDeConexão();
                Conexão.Open();
                string StringComando = "ALTER LOGIN sa ENABLE ALTER LOGIN sa WITH PASSWORD = " + "'" + Senha + "'";
                SqlCommand Comando = new SqlCommand(StringComando, Conexão);

                Comando.ExecuteNonQuery();
            }

            catch (SqlException Ex)
            {
                if (Ex.Number == 15151)
                {
                    MessageBox.Show("Atenção.: O usuário do WINDOWS em que você está logado, não tem permissão de acesso ao Mecanismo de Banco de Dados do SQL Server.\n\nA instalação irá ser interrompida. Para resolver este problema, faça a instalação do sistema em um usuário do Windows que tenha permissão de acesso ao banco.\n\nPossível Causa.: Você está instalando o sistema por um usuário do Windows diferente do usuário que efetuou a instalação do SQL Server", "TechSIS Permissão    -  (Conec_HabilUsuarSaDefineSenha) BLOCO UNICO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("TechSIS SQL.: Um falha foi detectada ao executar o método Conec_HabilUsuarSaDefineSenha\n\n\b BLOCO DO TRATAMENTO = UNICO\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_HabilUsuarSaDefineSenha\n\n\b BLOCO DO TRATAMENTO = UNICO\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            finally
            {
                Conexão.Close();
            }

            return false;
        }

        //DEFINE A STRING DE CONEXÃO COM USUÁRIO SA
        public string Conec_StringDeConexãoUsuarioSA(string Senha)
        {
            if (comInstancia == "MSSQLSERVER" || comInstancia == string.Empty || comInstancia == "LOCAL")
            {
                StringConexão = @"Server=" + txtServidor + ";Database=master;Uid=sa;Pwd=" + Senha;
            }
            else
            {
                StringConexão = @"Server=" + txtServidor + @"\" + comInstancia + ";Database=master;Uid=sa;Pwd=" + Senha;
            }

            return StringConexão;
        }

        //SETA A PERMISSÃO DE ADM PARA TODOS OS USUÁRIO DO WINDOWS
        public bool Conec_PermissaoSQL(SqlConnection Conexão, string Senha)
        {
            Conexão.ConnectionString = Conec_StringDeConexãoUsuarioSA(Senha);

            
            //FAÇO O TRATAMENTO PARA R2
            if (VersãoSQL == "10_50")
            {
                VersãoSQL = "10";
            }


            #region BLOCO 1 = APENAS TESTA A CONEXÃO COM O SQL (Conexão.Open())
            try
            {
                Conexão.Open();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_PermissaoSQL\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            finally
            {
                Conexão.Close();
            }
            #endregion


            #region BLOCO 2 = CRIA O LOGIN WINDOWS NO SQL E DA PERMISSÃO sysadmin PARA O MESMO
            try
            {
                Conexão.Open();

                string StringComando_1 = @"CREATE LOGIN [\Todos] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[Português (Brasil)]";
                string StringComando_2 = "";
                if (Convert.ToInt32(VersãoSQL) <= 10)
                {
                    StringComando_2 = @"EXEC sp_addsrvrolemember '\Todos', 'sysadmin'";
                }
                else
                {
                    StringComando_2 = @"ALTER SERVER ROLE sysadmin ADD MEMBER [\Todos]";
                }
                SqlCommand Comando_1 = new SqlCommand(StringComando_1, Conexão);
                SqlCommand Comando_2 = new SqlCommand(StringComando_2, Conexão);

                Comando_1.ExecuteNonQuery();
                Comando_2.ExecuteNonQuery();
            }
            catch (SqlException Ex)
            {
                if (Ex.Number == 15025)
                {
                    //SE O USUÁRIO JÁ FOR CADASTRADO NA PERMISSÃO DO SQL, NÃO FAZER NADA
                }
                else
                {
                    MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_PermissaoSQL\n\n\b BLOCO DO TRATAMENTO = 02         CÓDIGO ERRO.: " + Ex.Number + "\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }
            finally
            {
                Conexão.Close();
            }
            #endregion


           

            return false;
        }

        //CREA O NOVO BANCO DE DADOS TechSIS_INF
        public bool Conec_ConfigCreateDatabase(SqlConnection Conexão)
        {
            #region BLOCO 1 = APENAS ABRE A CONEXÃO COM O BANCO
            try
            {
                Conexão.Open();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_ConfigCreateDatabase\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            finally
            {
                Conexão.Close();
            }
            #endregion


            #region BLOCO 2 = VERIFICA SE O BANCO EXISTE E FAZ O BackUp = TAMBÉM DROPA O BANCO SE EXISTIR
            try
            {
                Conexão.Open();

                string StringComandoSQL = "SELECT * FROM sys.databases WHERE name = 'TechSIS_INF'";
                SqlCommand ComandoSQL = new SqlCommand(StringComandoSQL, Conexão);

                SqlDataReader Dr = ComandoSQL.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    Dr.Close();
                    DialogResult Result = MessageBox.Show("BANCO DE DADOS COM O NOME TechSIS_INF JÁ EXISTENTE.\nDESEJA CRIAR UM BackUp DE SEGURANÇA?", "TechSIS Auto-Manutenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (Result == DialogResult.Yes)
                    {
                        CriaBackUpSeguranca(Conexão);
                    }

                    if (Conexão.State == ConnectionState.Closed)
                    {
                        Conexão.Open();
                    }
                    string StringComandoDROP = "USE MASTER DROP DATABASE TechSIS_INF";
                    SqlCommand ComandoDROP = new SqlCommand(StringComandoDROP, Conexão);

                    ComandoDROP.ExecuteNonQuery();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_ConfigCreateDatabase\n\n\b BLOCO DO TRATAMENTO = 02\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            finally
            {
                Conexão.Close();
            }
            #endregion


            #region BLOCO 3 = CRIA UM NOVO BANCO DE DADOS EM BRANCO
            try
            {
                Conexão.Open();

                string StringComandoSQL = "CREATE DATABASE TechSIS_INF";
                SqlCommand ComandoSQL = new SqlCommand(StringComandoSQL, Conexão);

                ComandoSQL.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_ConfigCreateDatabase\n\n\b BLOCO DO TRATAMENTO = 03\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            finally
            {
                Conexão.Close();
            }

            #endregion

            return false;
        }

        //CRIA O BACKUP DO BANCO CASO ELE JA EXISTE
        public void CriaBackUpSeguranca(SqlConnection Conexão)
        {
            string Continua = "S";
            while (Continua == "S")
            {
                if (Conexão.State == ConnectionState.Closed)
                {
                    Conexão.Open();
                }


                FolderBrowserDialog Browser = new FolderBrowserDialog();
                Browser.Description = "Selecione o diretório do BackUp";
                DialogResult BrowserDialog = Browser.ShowDialog();


                if (BrowserDialog != DialogResult.OK)
                {
                    DialogResult CancBackUp = MessageBox.Show("DESEJA CANCELAR O BackUp DE SEGURANÇA?", "TechSIS Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (CancBackUp == DialogResult.Yes)
                    {
                        Continua = "N";
                    }
                    else
                    {
                        Continua = "S";
                    }
                }
                else
                {
                    string StrComando = "BACKUP DATABASE [TechSIS_INF] TO DISK = '" + Browser.SelectedPath + "\\BackUpTechSIS_INF.bak'";
                    SqlCommand Comando = new SqlCommand(StrComando, Conexão);

                    try
                    {
                        Comando.ExecuteNonQuery();
                        MessageBox.Show("BackUp REALIZADO EM (" + Browser.SelectedPath + "\\BackUpTechSIS_INF.bak)", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Continua = "N";
                    }
                    catch (SqlException Ex)
                    {
                        if (Ex.Number == 3201)
                        {
                            MessageBox.Show("Você não tem permissão de Administrador para realizar um BackUp no diretório informado. Informe outro diretório, preferêncialmente uma unidade de disco em que o sistema operacional não esteja instalado.", "TechSIS BackUp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("ERRO NÃO TRATADO AO REALIZAR BackUp! TENTE NOVAMENTE", "TechSIS SQLErro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("ERRO NÃO TRATADO AO REALIZAR BackUp! TENTE NOVAMENTE", "TechSIS Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Conexão.Close();
                    }
                }
            }
        }

        //EXECUTA OS BATs REFERENTES AOS SCRIPTS 1 E 2
        public bool Conec_ConfigWriteBAT_1(string Senha, string Servidor, string Instancia)
        {
            if (!Directory.Exists(@"..\\Scripts"))
            {
                Directory.CreateDirectory(@"..\\Scripts");
            }


            #region BLOCO 1 = CRIA O .BAT DO Script 1 - Exec das Tabelas
            try
            {
                DirectoryInfo Info_1 = new DirectoryInfo("..\\Scripts");
                string MeuDirectory_1 = Info_1.FullName;
                StreamWriter Writ_1 = new StreamWriter(@"..\\Scripts\TechSIS_ATU_1.bat", false, Encoding.Default);
                if (Instancia == "MSSQLSERVER" || Instancia.ToUpper() == "LOCAL" || Instancia == "")
                {
                    Writ_1.WriteLine("sqlcmd -U sa -P " + Senha + " -S " + Servidor + " -d TechSIS_INF -i \"" + MeuDirectory_1 + "\\Script 1 - Exec das Tabelas.sql\"");
                }
                else
                {
                    Writ_1.WriteLine("sqlcmd -U sa -P " + Senha + " -S " + Servidor + "\\" + Instancia + " -d TechSIS_INF -i \"" + MeuDirectory_1 + "\\Script 1 - Exec das Tabelas.sql\"");
                }

                Writ_1.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_ConfigWriteBAT_1\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            #endregion
            #region BLOCO 2 = CRIA O .BAT DO Script 2 - Exec dos INSERTs
            try
            {
                DirectoryInfo Info_2 = new DirectoryInfo("..\\Scripts");
                string MeuDirectory_2 = Info_2.FullName;
                StreamWriter Writ_2 = new StreamWriter(@"..\\Scripts\TechSIS_ATU_2.bat", false, Encoding.Default);
                if (Instancia == "MSSQLSERVER" || Instancia.ToUpper() == "LOCAL" || Instancia == "")
                {
                    Writ_2.WriteLine("sqlcmd -U sa -P " + Senha + " -S " + Servidor + " -d TechSIS_INF -i \"" + MeuDirectory_2 + "\\Script 2 - Exec dos INSERTs.sql\"");
                }
                else
                {
                    Writ_2.WriteLine("sqlcmd -U sa -P " + Senha + " -S " + Servidor + "\\" + Instancia + " -d TechSIS_INF -i \"" + MeuDirectory_2 + "\\Script 2 - Exec dos INSERTs.sql\"");
                }
                Writ_2.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_ConfigWriteBAT_1\n\n\b BLOCO DO TRATAMENTO = 02\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            #endregion

            return false;
        }

        //RODA OS SCRIPTS 1 E 2
        public bool Conec_ConfigRodaScript_1()
        {
            #region BLOCO 1 = RODA O BAT TechSIS_ATU_1
            try
            {
                DirectoryInfo Info_1 = new DirectoryInfo("..\\Scripts");
                string MeuDirectory_1 = Info_1.FullName;
                string ArquBAT_1 = MeuDirectory_1 + @"\TechSIS_ATU_1.bat";
                string SQLArqu_1 = MeuDirectory_1 + @"\Script 1 - Exec das Tabelas.sql"; ;
                Process Proc_1 = new Process();
                Proc_1.StartInfo.FileName = ArquBAT_1;
                Proc_1.StartInfo.Arguments = SQLArqu_1;
                Proc_1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Proc_1.StartInfo.ErrorDialog = false;
                Proc_1.StartInfo.WorkingDirectory = Path.GetDirectoryName(ArquBAT_1);
                Proc_1.Start();
                Proc_1.WaitForExit();
                if (Proc_1.ExitCode != 0)
                { }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método ConfigRodaScript_1\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            #endregion
            #region BLOCO 2 = RODA O BAT TechSIS_ATU_2
            try
            {
                DirectoryInfo Info_2 = new DirectoryInfo("..\\Scripts");
                string MeuDirectory_2 = Info_2.FullName;
                string ArquBAT_2 = MeuDirectory_2 + @"\TechSIS_ATU_2.bat";
                string SQLArqu_2 = MeuDirectory_2 + @"\Script 2 - Exec dos INSERTs.sql"; ;
                Process Proc_2 = new Process();
                Proc_2.StartInfo.FileName = ArquBAT_2;
                Proc_2.StartInfo.Arguments = SQLArqu_2;
                Proc_2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Proc_2.StartInfo.ErrorDialog = false;
                Proc_2.StartInfo.WorkingDirectory = Path.GetDirectoryName(ArquBAT_2);
                Proc_2.Start();
                Proc_2.WaitForExit();
                if (Proc_2.ExitCode != 0)
                { }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método ConfigRodaScript_1\n\n\b BLOCO DO TRATAMENTO = 02\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            #endregion

            return false;
        }

        //INSERE O USUÁRIO MASTER NO BANCO DE DADOS
        public bool Conec_InsertMASTER(SqlConnection Conexão, string Senha)
        {

            #region BLOCO 1 = ABRE A CONEXÃO E FAZ O INSERT DO USUÁRIO MASTER NO BANCO
            try
            {
                Conexão.Open();

                string SenhaCRIPT = "'" + WenProtects(Senha) + "'";

                string StringComando = "USE TechSIS_INF INSERT INTO TabUsuar VALUES (1,'TechSIS INF - LTDA','TechSIS MASTER',1,1,'thiago@prodados.com','thiagoaborges@live.com'," + SenhaCRIPT + ",1,'thiago@prodados.com','thiago-programador',GETDATE()) USE MASTER";
                SqlCommand Comando = new SqlCommand(StringComando, Conexão);

                Comando.ExecuteNonQuery();

            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_InsertMASTER\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            finally
            {
                Conexão.Close();
            }
            #endregion


            return false;
        }

        //INSERE A EMPRESA 1 NO BANCO DE DADOS
        public bool Conec_InsertLOJA(SqlConnection Conexão)
        {
            if (!File.Exists(@"..\\Debug\SecF_01.xml"))
            {
                MessageBox.Show("Segurança.: Arquivo SecF_01.xml não foi localizado.\n\nA instalação será interrompida.", "TechSIS Erro FATAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            #region BLOCO 1 = INSERE AS INFORMAÇÕES DA EMPRESA NO BANCO DE DADOS
            else
            {
                try
                {
                    Conexão.Open();

                    string CaminhoDoArquivo = ("SecF_01.xml");
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
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_InsertLOJA\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        //REMOVO NULOS DA TABELA
        public bool Conec_RemoverNuloEmpresa(SqlConnection Conexão)
        {
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
        public bool Conec_InsertConfig(SqlConnection Conexão)
        {
            if (!File.Exists(@"..\\Debug\SecF_01.xml"))
            {
                MessageBox.Show("Segurança.: Arquivo SecF_01.xml não foi localizado.\n\nA instalação será interrompida.", "TechSIS Erro FATAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            #region BLOCO 1 = INSERE AS INFORMAÇÕES DA CONFIGURAÇÃO
            else
            {
                try
                {
                    Conexão.Open();


                    string Comando_INSERIR = "USE TechSIS_INF INSERT INTO TabConfi (SeqLoj_CON,CorFun_CON,QtPesq_CON,CamRel_CON,NomAt1_CON,NomAt2_CON,PlayMu_CON,VerBco_CON,GraXML_CON) VALUES (1,'White','30','C:\\TechSIS INF\\Relatórios\\','ATALHO 01','ATALHO 02','False','1.0.0','True') USE MASTER";
                    SqlCommand _Comando_INSERIR = new SqlCommand(Comando_INSERIR, Conexão);


                    _Comando_INSERIR.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Conec_InsertConfig\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        public bool Conec_RemoverNuloConfig(SqlConnection Conexão)
        {
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

        #region INSERE ARQUIVOS .tech
        //Insere Arquivos .tech
        public bool InsertTECH_MUNIC(SqlConnection Conexão, ProgressBar proBar, Label lblStatusMsg, Label lblCodigo, Label lblDescricao)
        {
            try
            {
                Conexão.Open();

                string StringComando = "";
                SqlCommand Comando = new SqlCommand();

                lblStatusMsg.Text = "ALIMENTANDO O BANCO DE DADOS 1 de 3.. (AGUARDE)";
                #region BLOCO 1 = Municipios

                // Lendo arquivo texto
                string MUNIC = @"..\Tech FILEs\0 - Municipios.tech";
                string[] QT_Linhas_MUNIC = null;


                if (File.Exists(MUNIC))
                {
                    QT_Linhas_MUNIC = File.ReadAllLines(MUNIC);
                }
                else
                {
                    throw new Exception(String.Format("Arquivo [{0}] nao encontrado", MUNIC));
                }



                string NumeMUN = String.Empty;
                string DescMUN = String.Empty;
                string NuFedeMUN = String.Empty;
                string SiFedeMUN = String.Empty;

                StreamReader Ler = new StreamReader(MUNIC);
                int NumLinhas = System.IO.File.ReadAllLines(MUNIC).Length;
                proBar.Maximum = NumLinhas;

                StringComando = "USE TechSIS_INF";
                Comando = new SqlCommand(StringComando, Conexão);
                Comando.ExecuteNonQuery();

                // Processo as linhas do arquivo
                int i = 1;
                while (i < QT_Linhas_MUNIC.Length)
                {
                    StringComando = "INSERT INTO TabCidMu VALUES (@Sequenc,@NumeMUN,@DescMUN,@NuFedeMUN,@SiFedeMUN)";
                    Comando = new SqlCommand(StringComando, Conexão);

                    string Linha = QT_Linhas_MUNIC[i].ToString();

                    NuFedeMUN = Linha.Substring(0, 2);
                    SiFedeMUN = Linha.Substring(3, 2);
                    NumeMUN = Linha.Substring(24, 7);
                    DescMUN = Linha.Substring(32).ToUpper();


                    Application.DoEvents();
                    lblCodigo.Text = "CÓDIGO.: " + NumeMUN.ToString();
                    Application.DoEvents();
                    lblDescricao.Text = "DESCRIÇÃO.: " + DescMUN.ToString();

                    Comando.Parameters.Add("@Sequenc", SqlDbType.Int).Value = i;
                    Comando.Parameters.Add("@NumeMUN", SqlDbType.Int).Value = NumeMUN;
                    Comando.Parameters.Add("@DescMUN", SqlDbType.VarChar).Value = DescMUN;
                    Comando.Parameters.Add("@NuFedeMUN", SqlDbType.VarChar).Value = NuFedeMUN;
                    Comando.Parameters.Add("@SiFedeMUN", SqlDbType.VarChar).Value = SiFedeMUN;
                    Comando.ExecuteNonQuery();

                    proBar.Value = i;

                    i++;
                }

                StringComando = "USE TechSIS_INF";
                Comando = new SqlCommand(StringComando, Conexão);
                Comando.ExecuteNonQuery();
                #endregion
                Conexão.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método InsertTECH_MUNIC\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            finally
            {
                Conexão.Close();
            }


            return false;

        }

        //Insere Arquivos .tech
        public bool InsertTECH_PAISE(SqlConnection Conexão, ProgressBar proBar, Label lblStatusMsg, Label lblCodigo, Label lblDescricao)
        {
            try
            {
                Conexão.Open();

                string StringComando = "";
                SqlCommand Comando = new SqlCommand();


                lblStatusMsg.Text = "ALIMENTANDO O BANCO DE DADOS 2 de 3.. (AGUARDE)";
                #region BLOCO 1 = Paises
                // Lendo arquivo texto

                string PAISE = @"..\Tech FILEs\1 - Paises.tech";
                string[] Linhas = null;
                if (File.Exists(PAISE))
                {
                    Linhas = File.ReadAllLines(PAISE);
                }
                else
                {
                    throw new Exception(String.Format("Arquivo [{0}] nao encontrado", PAISE));
                }

                //Escrevendo na tabela Teste1 no banco SQLExpress\Teste
                //Cria a conexão com o Banco de Dados e Abre!
                string NumePAI = String.Empty;
                string DescPAI = String.Empty;


                StreamReader Ler = new StreamReader(PAISE);
                int NumLinhas = System.IO.File.ReadAllLines(PAISE).Length;
                proBar.Maximum = NumLinhas;

                StringComando = "USE TechSIS_INF";
                Comando = new SqlCommand(StringComando, Conexão);
                Comando.ExecuteNonQuery();


                // Processo as linhas do arquivo
                int z = 1;
                while (z < Linhas.Length)
                {
                    StringComando = "INSERT INTO TabPaise VALUES (@Sequenc,@NumePAI,@DescPAI)";
                    Comando = new SqlCommand(StringComando, Conexão);

                    string Linha = Linhas[z].ToString();

                    NumePAI = Linha.Substring(0, 8).TrimStart();
                    DescPAI = Linha.Substring(58);

                    Application.DoEvents();
                    lblCodigo.Text = "CÓDIGO.: " + NumePAI.ToString();
                    Application.DoEvents();
                    lblDescricao.Text = "DESCRIÇÃO.: " + DescPAI.ToString();

                    Application.DoEvents();
                    if (z == 1)
                    {
                        z = 2;
                    }

                    Comando.Parameters.Add("@Sequenc", SqlDbType.Int).Value = z;
                    Comando.Parameters.Add("@NumePAI", SqlDbType.Int).Value = NumePAI;
                    Comando.Parameters.Add("@DescPAI", SqlDbType.VarChar).Value = DescPAI;
                    Application.DoEvents();
                    Comando.ExecuteNonQuery();

                    proBar.Value = z;

                    z++;
                }

                StringComando = "USE TechSIS_INF";
                Comando = new SqlCommand(StringComando, Conexão);
                Comando.ExecuteNonQuery();
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método InsertTECH_PAISE\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            finally
            {
                Conexão.Close();
            }

            return false;
        }

        //Insere Arquivos .tech
        public bool InsertTECH_NCM(SqlConnection Conexão, ProgressBar proBar, Label lblStatusMsg, Label lblCodigo, Label lblDescricao)
        {
            Conexão.Open();

            string StringComando = "";
            SqlCommand Comando = new SqlCommand();


            lblStatusMsg.Text = "ALIMENTANDO O BANCO DE DADOS 3 de 3.. (AGUARDE)";
            #region Ncm
            try
            {
                // Lendo arquivo texto
                string NCM = @"..\Tech FILEs\2 - Ncm TechSIS.tech";
                string[] QT_Linhas_NCM = null;
                if (File.Exists(NCM))
                {
                    QT_Linhas_NCM = File.ReadAllLines(NCM);
                }
                else
                {
                    throw new Exception(String.Format("Arquivo [{0}] nao encontrado", NCM));
                }

                string NumeNCM = String.Empty;
                string DescNCM = String.Empty;

                StreamReader Ler = new StreamReader(NCM);
                int NumLinhas = System.IO.File.ReadAllLines(NCM).Length;
                proBar.Maximum = NumLinhas;


                StringComando = "USE TechSIS_INF";
                Comando = new SqlCommand(StringComando, Conexão);
                Comando.ExecuteNonQuery();


                // Processo as linhas do arquivo
                int p = 1;
                while (p < QT_Linhas_NCM.Length)
                {
                    StringComando = "INSERT INTO TabNcmSo VALUES (@Sequenc,@NumeNCM,@DescNCM)";
                    Comando = new SqlCommand(StringComando, Conexão);

                    string Linha = QT_Linhas_NCM[p].ToString();


                    Linha.PadRight(75, ' ');
                    NumeNCM = Linha.Substring(0, 9);
                    DescNCM = Linha.Substring(19);

                    Application.DoEvents();
                    lblCodigo.Text = "CÓDIGO.: " + NumeNCM.ToString();
                    Application.DoEvents();
                    lblDescricao.Text = "DESCRIÇÃO.: " + DescNCM.ToString();


                    if (p == 1)
                    {
                        p = 2;
                    }

                    Comando.Parameters.Add("@Sequenc", SqlDbType.Int).Value = p;
                    Comando.Parameters.Add("@NumeNCM", SqlDbType.Int).Value = NumeNCM;
                    Comando.Parameters.Add("@DescNCM", SqlDbType.VarChar).Value = DescNCM;
                    Comando.ExecuteNonQuery();


                    proBar.Value = p;
                    p++;
                }

                StringComando = "USE MASTER";
                Comando = new SqlCommand(StringComando, Conexão);
                Comando.ExecuteNonQuery();
            #endregion
                Conexão.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método InsertTECH_NCM\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }

            return false;
        }
        #endregion

        //EXECUTA OS BATs REFERENTES AOS SCRIPTS 3 E 4
        public bool Conec_ConfigWriteBAT_2(string Senha, string Servidor, string Instancia)
        {
            if (!Directory.Exists(@"..\\Scripts"))
            {
                Directory.CreateDirectory(@"..\\Scripts");
            }

            #region BLOCO 1 = CRIA O .BAT DO Script 3 - Exec das FKs
            try
            {
                DirectoryInfo Info_1 = new DirectoryInfo("..\\Scripts");
                string MeuDirectory_1 = Info_1.FullName;
                StreamWriter Writ_1 = new StreamWriter(@"..\\Scripts\TechSIS_ATU_1.bat", false, Encoding.Default);
                if (Instancia == "MSSQLSERVER" || Instancia.ToUpper() == "LOCAL" || Instancia == "")
                {
                    Writ_1.WriteLine("sqlcmd -U sa -P " + Senha + " -S " + Servidor + " -d TechSIS_INF -i \"" + MeuDirectory_1 + "\\Script 3 - Exec das FKs.sql\"");
                }
                else
                {
                    Writ_1.WriteLine("sqlcmd -U sa -P " + Senha + " -S " + Servidor + "\\" + Instancia + " -d TechSIS_INF -i \"" + MeuDirectory_1 + "\\Script 3 - Exec das FKs.sql\"");
                }
                Writ_1.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método ConfigWriteBAT_2\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            #endregion
            #region BLOCO 2 = CRIA O .BAT DO Script 4 - Tabela de Programas
            try
            {
                DirectoryInfo Info_2 = new DirectoryInfo("..\\Scripts");
                string MeuDirectory_2 = Info_2.FullName;
                StreamWriter Writ_2 = new StreamWriter(@"..\\Scripts\TechSIS_ATU_2.bat", false, Encoding.Default);
                if (Instancia == "MSSQLSERVER" || Instancia.ToUpper() == "LOCAL" || Instancia == "")
                {
                    Writ_2.WriteLine("sqlcmd -U sa -P " + Senha + " -S " + Servidor + " -d TechSIS_INF -i \"" + MeuDirectory_2 + "\\Script 4 - Tabela de Programas.sql\"");
                }
                else
                {
                    Writ_2.WriteLine("sqlcmd -U sa -P " + Senha + " -S " + Servidor + "\\" + Instancia + " -d TechSIS_INF -i \"" + MeuDirectory_2 + "\\Script 4 - Tabela de Programas.sql\"");
                }
                Writ_2.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método ConfigWriteBAT_2\n\n\b BLOCO DO TRATAMENTO = 02\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            #endregion

            return false;
        }

        //RODA OS SCRIPTS 3 E 4
        public bool Conec_ConfigRodaScript_2()
        {

            #region BLOCO 1 = RODA O BAT TechSIS_ATU_1
            try
            {
                DirectoryInfo Info_1 = new DirectoryInfo("..\\Scripts");
                string MeuDirectory_1 = Info_1.FullName;
                string ArquBAT_1 = MeuDirectory_1 + @"\TechSIS_ATU_1.bat";
                string SQLArqu_1 = MeuDirectory_1 + @"\Script 3 - Exec das FKs.sql"; ;
                Process Proc_1 = new Process();
                Proc_1.StartInfo.FileName = ArquBAT_1;
                Proc_1.StartInfo.Arguments = SQLArqu_1;
                Proc_1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Proc_1.StartInfo.ErrorDialog = false;
                Proc_1.StartInfo.WorkingDirectory = Path.GetDirectoryName(ArquBAT_1);
                Proc_1.Start();
                Proc_1.WaitForExit();
                if (Proc_1.ExitCode != 0)
                { }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método ConfigRodaScript_2\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            #endregion
            #region BLOCO 1 = RODA O BAT TechSIS_ATU_2
            try
            {
                DirectoryInfo Info_2 = new DirectoryInfo("..\\Scripts");
                string MeuDirectory_2 = Info_2.FullName;
                string ArquBAT_2 = MeuDirectory_2 + @"\TechSIS_ATU_2.bat";
                string SQLArqu_2 = MeuDirectory_2 + @"\Script 4 - Tabela de Programas.sql"; ;
                Process Proc_2 = new Process();
                Proc_2.StartInfo.FileName = ArquBAT_2;
                Proc_2.StartInfo.Arguments = SQLArqu_2;
                Proc_2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Proc_2.StartInfo.ErrorDialog = false;
                Proc_2.StartInfo.WorkingDirectory = Path.GetDirectoryName(ArquBAT_2);
                Proc_2.Start();
                Proc_2.WaitForExit();
                if (Proc_2.ExitCode != 0)
                { }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método ConfigRodaScript_2\n\n\b BLOCO DO TRATAMENTO = 02\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            #endregion


            return false;
        }




        //GRAVA O XML COM A STRING DE CONEXÃO
        public bool Config_GravaXMLConexao(RadioButton rabServidor, RadioButton rabEstacao, TextBox txtServidor, ComboBox comInstancia, string Senha)
        {
            try
            {

                if (!Directory.Exists(@"..\Conexão"))
                {
                    Directory.CreateDirectory(@"..\Conexão");
                }
                if (File.Exists(@"..\Conexão\StringConexão.xml"))
                {
                    File.Delete(@"..\Conexão\StringConexão.xml");
                }

                StreamWriter GravarEmTXT = new StreamWriter(@"..\Conexão\StringConexão.xml");

                if (rabServidor.Checked == true)
                {
                    if (comInstancia.Text == "MSSQLSERVER")
                    {
                        GravarEmTXT.WriteLine("Server=" + txtServidor.Text + ";Database=TechSIS_INF;Uid=sa;Pwd=" + Senha);
                    }
                    else
                    {
                        GravarEmTXT.WriteLine("Server=" + txtServidor.Text + "\\" + comInstancia.Text + ";Database=TechSIS_INF;Uid=sa;Pwd=" + Senha);
                    }
                }
                if (rabEstacao.Checked == true)
                {
                    if (comInstancia.Text == "MSSQLSERVER" || comInstancia.Text.ToUpper() == "LOCAL" || comInstancia.Text == "")
                    {
                        GravarEmTXT.WriteLine("Server=" + txtServidor.Text + ";Database=TechSIS_INF;Uid=sa;Pwd=" + Senha);
                    }
                    else
                    {
                        GravarEmTXT.WriteLine("Server=" + txtServidor.Text + "\\" + comInstancia.Text + ";Database=TechSIS_INF;Uid=sa;Pwd=" + Senha);
                    }
                }


                GravarEmTXT.Close();


                System.IO.File.SetAttributes(@"..\Conexão\StringConexão.xml", System.IO.FileAttributes.Hidden);
            }

            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Config_GravaXMLConexao\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }

            return false;
        }

        //GRAVA O XML DE INFORMAÇÕES DO SERVIDOR
        public bool Config_GravaXMLInforma(RadioButton rabServidor, RadioButton rabEstacao, TextBox txtServidor, ComboBox comInstancia, string Senha)
        {

            if (!Directory.Exists(@"..\Conexão"))
            {
                Directory.CreateDirectory(@"..\Conexão");
            }

            try
            {

                string TipoINSTA = "N";
                string MaquSERVI = "S";

                if (rabServidor.Checked == true)
                {
                    if (comInstancia.Text == "MSSQLSERVER")
                    {
                        TipoINSTA = "P";
                    }
                }
                if (rabEstacao.Checked == true)
                {
                    MaquSERVI = "N";

                    if (comInstancia.Text == "MSSQLSERVER" || comInstancia.Text.ToUpper() == "LOCAL" || comInstancia.Text == "")
                    {
                        TipoINSTA = "P";
                    }
                }


                XmlTextWriter GerarXML = new XmlTextWriter(@"..\Conexão\DadosServidor.xml", null);
                GerarXML.WriteStartDocument();
                GerarXML.WriteStartElement("Dados_Loggin");
                GerarXML.WriteElementString("Tipo_Instancia", TipoINSTA);
                GerarXML.WriteElementString("Desc_Servidor", txtServidor.Text);
                GerarXML.WriteElementString("Inst_Servidor", comInstancia.Text);
                GerarXML.WriteElementString("Banco_De_Dados", "TechSIS_INF");
                GerarXML.WriteElementString("UsuarioSQL", WenProtects("sa"));
                GerarXML.WriteElementString("SenhaSQL", WenProtects(Senha));
                GerarXML.WriteElementString("MaquinaServidor", WenProtects(MaquSERVI));
                GerarXML.WriteElementString("Data_Criacao", DateTime.Today.ToString("dd/MM/yyyy"));
                GerarXML.WriteEndElement();
                GerarXML.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS.: Um falha foi detectada ao executar o método Config_GravaXMLInforma\n\n\b BLOCO DO TRATAMENTO = 01\n\n" + Ex.Message, "TechSIS Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("A configuração do sistema será fechada. Execute novamente", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }

            return false;
        }


        //DELETO O DIRETORIO
        public void DeleteDirectory(string dirLocation)
        {
            string[] files = Directory.GetFiles(dirLocation);
            string[] dirs = Directory.GetDirectories(dirLocation);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(dirLocation, false);
        }










        











        






















        




    }
}