using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Net;


namespace TechSIS_BWK
{
    internal class TechSIS_LoginMET
    {
        #region FORMATAR CPF E CNPJ
        private static string FormatarCpfCnpj(string strCpfCnpj)
        {
            if (strCpfCnpj.Length <= 11)
            {
                MaskedTextProvider mtpCpf = new MaskedTextProvider(@"000\.000\.000-00");
                mtpCpf.Set(ZerosEsquerda(strCpfCnpj, 11));
                return mtpCpf.ToString();
            }
            else
            {
                MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");
                mtpCnpj.Set(ZerosEsquerda(strCpfCnpj, 11));
                return mtpCnpj.ToString();
            }
        }
        private static string ZerosEsquerda(string strString, int intTamanho)
        {
            string strResult = "";
            for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
            {
                strResult += "0";
            }
            return strResult + strString;
        }
        #endregion
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

        #region MÉTODOS DA TELA DE LOGIN


        //POPULA O COMBOBOX COM OS USUÁRIO CADASTRADOS
        public void LOG_PreencheComboBoxComUsuarios(ComboBox comUsuario)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string strComando = "SELECT Sequen_USU, Apelid_USU FROM TabUsuar WHERE Status_USU <> 3 AND Status_USU <> 2";
            SqlCommand Comando = new SqlCommand(strComando, Conexão);
            SqlDataReader Dr = Comando.ExecuteReader(); ;


            while (Dr.Read())
            {
                string COD = Dr["Sequen_USU"].ToString().PadLeft(6, '0');
                string DES = Dr["Apelid_USU"].ToString();

                comUsuario.Items.Add(COD + " - " + DES);
            }
        }

        //PREENCHE A EMPRESA DO USUÁRIO SELECIONADO
        public void LOG_PreencheComboBoxComEmpresas(ComboBox comUsuario, ComboBox comEmpresa)
        {
            //LIMPA AS VARIAVEIS
            comEmpresa.Items.Clear();

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string strComando = "SELECT DISTINCT Sequen_EMP, Descri_EMP FROM TabUsu01 INNER JOIN TabEmpre ON TabUsu01.SeqLoj_US1 = TabEmpre.Sequen_EMP WHERE SeqUsu_US1 = @SeqUsu_US1";
            SqlCommand Comando = new SqlCommand(strComando, Conexão);
            Comando.Parameters.Add("@SeqUsu_US1", SqlDbType.VarChar).Value = comUsuario.Text.Substring(0, 6);
            SqlDataReader Dr = Comando.ExecuteReader(); ;

            try
            {
                //SE EXISTE REGISTRO NA TabUsu01
                if (Dr.HasRows)
                {
                    if (Convert.ToInt32(comUsuario.Text.Substring(0, 6)) != 1)
                    {
                        while (Dr.Read())
                        {
                            string COD = Dr["Sequen_EMP"].ToString().PadLeft(6, '0');
                            string DES = Dr["Descri_EMP"].ToString();

                            comEmpresa.Items.Add(COD + " - " + DES);
                        }
                    }
                }
                    //SE NÃO EXISTE NENHUM REGISTRO NA TabUsu01
                else
                {
                    //SE O USUÁRIO É MASTER
                    //ADICIONA A EMPRESA MASTER PADRÃO
                    if (Convert.ToInt32(comUsuario.Text.Substring(0, 6)) == 1)
                    {
                        comEmpresa.Items.Add("ACESSO A EMPRESA MASTER PADRÃO (00001)");
                    }
                        //SE NÃO FOR O USUÁRIO MASTER
                        //DOU A MENSAGEM FALANDO QUE NENHUM EMPRESA FOI ENCONTRADA
                        //NA TABELA PARA O USUÁRIO INFORMADO
                    else
                    {
                        MessageBox.Show("Nenhuma empresa encontrada para o usuário informado", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método LOG_PreencheComboBoxComEmpresas()\n\nBLOCO = TechSIS_LoginMET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
                comEmpresa.SelectedIndex = 0;
            }
        }

        //CAPTURA O USUÁRIO SALVO
        public void LOG_XML_CapturaUsuario(ComboBox comUsuario, ComboBox comEmpresa, CheckBox cheLembrar, NumericUpDown nupCaixa)
        {
            try
            {
                string CaminhoDoArquivo = (@"..\Log\DadosLoggin.xml");
                if (File.Exists(CaminhoDoArquivo))
                {
                    XmlDocument LerXML = new XmlDocument();
                    LerXML.Load(CaminhoDoArquivo);

                    XmlNode LEMBRAR_USUAR = LerXML.DocumentElement.SelectSingleNode("LOGIN_ID_10");
                    XmlNode INDEX_USUARIO = LerXML.DocumentElement.SelectSingleNode("LOGIN_ID_03");
                    XmlNode INDEX_EMPRESA = LerXML.DocumentElement.SelectSingleNode("LOGIN_ID_04");
                    XmlNode NUMERO_CAIXAS = LerXML.DocumentElement.SelectSingleNode("LOGIN_ID_08");


                    string _LEMBRAR_USUAR = WenDisprotects(LEMBRAR_USUAR.InnerText.ToString());

                    int _INDEX_USU = Convert.ToInt32(WenDisprotects(INDEX_USUARIO.InnerText.ToString()));
                    int _INDEX_LOJ = Convert.ToInt32(WenDisprotects(INDEX_EMPRESA.InnerText.ToString()));
                    decimal _CAIXA = Convert.ToDecimal(WenDisprotects(NUMERO_CAIXAS.InnerText));


                    if (_LEMBRAR_USUAR == "True")
                    {
                        comUsuario.SelectedIndex = _INDEX_USU;
                        comEmpresa.SelectedIndex = _INDEX_LOJ;
                        nupCaixa.Value = _CAIXA;
                        cheLembrar.Checked = true;
                    }
                    else
                    {
                        comUsuario.SelectedIndex = 0;
                    }
                }
                else
                {
                    comUsuario.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
                comUsuario.SelectedIndex = 0;
            }
        }

        //GRAVA UM NOVO XML DE USUÁRIO SALVO
        public void LOG_XML_GravaCapturaUsuario(string CodigoUsuario, string DescriUsuario, string LojaCodigo, string LojaDescri, ComboBox comUsuario, ComboBox comEmpresa, CheckBox cheLembrar, NumericUpDown nupCaixa)
        {
            try
            {
                if (!Directory.Exists(@"..\Log\"))
                {
                    Directory.CreateDirectory(@"..\Log\");
                }

                XmlTextWriter GerarXML = new XmlTextWriter(@"..\Log\DadosLoggin.xml", Encoding.Default);
                GerarXML.WriteStartDocument();


                GerarXML.WriteStartElement("TechSIS_Loggon");
                //ID DO USUÁRIO
                GerarXML.WriteElementString("LOGIN_ID_01", WenProtects(CodigoUsuario));
                //DESCRIÇÃO DO USUÁRIO
                GerarXML.WriteElementString("LOGIN_ID_02", WenProtects(DescriUsuario));
                //INDEX DO COMBOBOX USUÁRIO
                GerarXML.WriteElementString("LOGIN_ID_03", WenProtects(comUsuario.SelectedIndex.ToString()));
                //INDEX DA LOJA
                GerarXML.WriteElementString("LOGIN_ID_04", WenProtects(comEmpresa.SelectedIndex.ToString()));
                //CÓDIGO DA LOJA
                GerarXML.WriteElementString("LOGIN_ID_05", WenProtects(LojaCodigo));
                //DESCRIÇÃO DA LOJA
                GerarXML.WriteElementString("LOGIN_ID_06", WenProtects(LojaDescri));
                //NOME DO COMPUTADOR
                GerarXML.WriteElementString("LOGIN_ID_07", WenProtects(Environment.MachineName));
                //NUMERO DO CAIXA
                GerarXML.WriteElementString("LOGIN_ID_08", WenProtects(nupCaixa.Value.ToString()));
                //DATA
                GerarXML.WriteElementString("LOGIN_ID_09", WenProtects(DateTime.Today.ToString("dd/MM/yyyy")));
                //LEMBRAR USUÁRIO?
                GerarXML.WriteElementString("LOGIN_ID_10", WenProtects(cheLembrar.Checked.ToString()));

                GerarXML.WriteEndElement();
                GerarXML.Close();
            }
            catch (XmlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método LOG_XML_GravaCapturaUsuario()\n\nBLOCO = TechSIS_LoginMET\n\n" + Ex.Message, "TechSIS XML Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método LOG_XML_GravaCapturaUsuario()\n\nBLOCO = TechSIS_LoginMET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //VERIFICA SE SENHA DO USUÁRIO ESTÁ CORRETA
        public bool LOG_VerificaSenhaUsuario(string UsuarioID, TextBox txtSenha)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            string strComando = "SELECT Senhas_USU FROM TabUsuar WHERE Sequen_USU = @Sequen_USU";
            SqlCommand SenhaGO = new SqlCommand(strComando, Conexão);
            SenhaGO.Parameters.Add("@Sequen_USU", SqlDbType.VarChar).Value = UsuarioID;


            SqlDataReader Dr = SenhaGO.ExecuteReader(); Dr.Read();

            try
            {
                string _Senha = WenDisprotects(Dr["Senhas_USU"].ToString());

                if (txtSenha.Text != _Senha)
                {
                    MessageBox.Show("SENHA INFORMADA ESTÁ INCORRETA. TENTE NOVAMENTE!", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSenha.Select(); txtSenha.SelectAll();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Atenção.: Criptografia no banco de dados está incorreta", "TechSIS Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Usuário deverá ser EXCLUIDO e INCLUIDO manualmente no banco", "TechSIS Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método LOG_VerificaSenhaUsuario()\n\nBLOCO = TechSIS_LoginMET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método LOG_VerificaSenhaUsuario()\n\nBLOCO = TechSIS_LoginMET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            finally
            {
                Conexão.Close();
            }
        }

        //VERIFICA SE O USUÁRIO TEM PERMISSÃO AO CAIXA INFORMADO
        public bool LOG_VerificaCaixaUsuario(string CodigoUsuar, NumericUpDown nupCaixa, string LojaCodigo, TextBox txtSenha)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string VerificaCaixa = "SELECT SeqCai_US1 FROM TabUsu01 WHERE SeqUsu_US1 = @Usuario AND SeqLoj_US1 = @Loja AND SeqCai_US1 = @Caixa";
            SqlCommand Comando = new SqlCommand(VerificaCaixa, Conexão);
            Comando.Parameters.Add("@Usuario", SqlDbType.Int).Value = CodigoUsuar;
            Comando.Parameters.Add("@Loja", SqlDbType.Int).Value = LojaCodigo;
            Comando.Parameters.Add("@Caixa", SqlDbType.Int).Value = nupCaixa.Value;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();


                if (Dr.HasRows)
                {
                    string Caixa = Dr["SeqCai_US1"].ToString();
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(CodigoUsuar) == 1)
                    {
                        return false;
                    }
                    else
                    {
                        MessageBox.Show("NÚMERO DE CAIXA NÃO LIBERADO PARA O USUÁRIO", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSenha.Select(); txtSenha.SelectAll();
                        return true;
                    }
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método LOG_VerificaCaixaUsuario()\n\nBLOCO = TechSIS_LoginMET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método LOG_VerificaCaixaUsuario()\n\nBLOCO = TechSIS_LoginMET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            finally
            {
                Conexão.Close();
            }
        }

        #endregion


        



        //DEFINE A EMPRESA LOGADA
        public static string EmpresaLogada { get; set; }

       

        #region TODOS OS MÉTODOS REFERENTE AO ARQUIVO DE LIBERAÇÃO

        #region CONTROLES
        public Button btnLogar { get; set; }
        public Button btnLiberarUso { get; set; }
        public Form FRM { get; set; }
        public TextBox txtSenha { get; set; }
        public Button btnMotivo { get; set; }
        public Panel panSenha { get; set; }
        public TextBox txtLiberado { get; set; }
        public TextBox txtQtDias { get; set; }
        public Label lblLiberação { get; set; }
        #endregion



        //DIAS DE LIBERAÇÃO DO ARQUIVO (1° LINHA)
        public static string Arq_DiasLiberado { get; set; }
        //DATA DO ÚLTIMO LOG (2° LINHA)
        public static string Arq_DataUltimoLog { get; set; }
        //TIPO DE LIBERAÇÃO (EMERGÊNCIA ou NORMAL) (1° LINHA)
        public static string Arq_TipoLibert { get; set; }
        //IDENTIFICAÇÃO DO ARQUIVO (NÚMERO + MACHINE + DOMINIO)
        public static string Arq_IdentArqui { get; set; }
        //TIPO DE LOG - FIRSTUSE OU SECONDUSE
        public static string Arq_TipoLog { get; set; }



        //DEFINE O CAMINHO DO ARQUIVO
        public string CaminhoArqLibert = "..\\Debug\\TechSIS_" + EmpresaLogada + "_WenFILE.ini";

      
        //EXECUTO ESTE MÉTODO SE ALGUM DOS MÉTODOS ABAIXO, RETORNAR TRUE
        void WenFILE_ErroDeVerificao()
        {
            //VOLTO OS CONTROLES AO NORMAL
            panSenha.Visible = false;
            txtQtDias.Visible = false;
            txtLiberado.Visible = false;
            lblLiberação.Visible = false;
            btnMotivo.Visible = true;
            btnLogar.Visible = false;
            btnLiberarUso.Visible = true;
            FRM.AcceptButton = btnLiberarUso;
        }

        //VERIFICO SE O ARQUIVO EXISTE
        public bool WenFILE_ArquivoExiste()
        {  
            if (!File.Exists(CaminhoArqLibert))
            {
                WenFILE_ErroDeVerificao();
                return true;
            }
            else
            {
                //VOLTO OS CONTROLES AO NORMAL
                panSenha.Visible = true;
                txtQtDias.Visible = true;
                txtLiberado.Visible = true;
                lblLiberação.Visible = true;
                btnMotivo.Visible = false;
                btnLogar.Visible = true;
                btnLiberarUso.Visible = false;
                FRM.AcceptButton = btnLogar;
                txtSenha.Select(); txtSenha.SelectAll();
                return false;
            }
        }

        //VERIFICO SE O ARQUIVO É VALIDO E POPULO AS STRING COM AS INFORMAÇÕES
        public bool WenFILE_ArquivoValido()
        {
            try
            {
                using (StreamReader LerArq = new StreamReader(CaminhoArqLibert))
                {
                    string ArqDecrypt = WenDisprotects(LerArq.ReadToEnd());
                    string[] Linhas = ArqDecrypt.Split('\n');

                    Arq_DiasLiberado = Convert.ToInt32(Linhas[0]).ToString("00");
                    Arq_DataUltimoLog = Convert.ToDateTime(Linhas[1]).ToString("dd/MM/yyyy");
                    Arq_TipoLibert = Linhas[2];
                    Arq_IdentArqui = Linhas[3];
                    Arq_TipoLog = Linhas[4];

                    //ARQUIVOS SÃO VALIDOS
                    return false;
                }
            }
            catch (Exception)
            {
                //ARQUIVOS SÃO INVALIDOS
                WenFILE_ErroDeVerificao();

                return true;
            }
        }

        //VERIFICO SE A EMPRESA DO ARQUIVO É VÁLIDA
        public bool WenFILE_EmpresaValida()
        {
            try
            {
                if (Convert.ToInt32(Arq_IdentArqui.Substring(0, 2)) == Convert.ToInt32(EmpresaLogada))
                {
                    //ARQUIVOS É VALIDO
                    return false;
                }
                //SE A EMPRESA É DIFERENTE NÃO É VALIDO
                else
                {
                    //ARQUIVOS SÃO INVALIDOS
                    WenFILE_ErroDeVerificao();

                    return true;
                }
            }
            //SE DER ERRO NÃO É VALIDO
            catch (Exception)
            {
                //ARQUIVOS SÃO INVALIDOS
                WenFILE_ErroDeVerificao();

                return true;
            }
        }

        //VERIFICO SE O ARQUIVO PERTENCE A ESTE COMPUTADOR
        public bool WenFILE_ArquivoComput()
        {
            try
            {
                if (Arq_IdentArqui.Substring(2) == Environment.MachineName.ToUpper() + Environment.UserDomainName.ToLower())
                {
                    //ARQUIVOS É VALIDO
                    return false;
                }
                //SE O NOME DA MAQUINA E DO USUÁRIO FOR DIFERENTE, O ARQUIVO NÃO PERTENCE
                else
                {
                    //ARQUIVOS SÃO INVALIDOS
                    WenFILE_ErroDeVerificao();

                    return true;
                }

            }


            //SE DER ERRO NÃO É VALIDO
            catch (Exception)
            {
                //ARQUIVOS SÃO INVALIDOS
                WenFILE_ErroDeVerificao();

                return true;
            }
        }

        //VERIFICA A QUANTIDADE DE DIAS DE USO
        public bool WenFILE_DiasDeUsoRest()
        {
            try
            {
                string DATA_SERVIDOR = "";

                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string strComando = "SELECT {fn NOW()} AS DATA_SERVIDOR";
                SqlCommand Comando = new SqlCommand(strComando, Conexão);
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    DATA_SERVIDOR = Dr["DATA_SERVIDOR"].ToString();
                }


              
                //PEGO OS DIAS DE LIBERAÇÃO RESTANTES
                //PEGO A DATA DO ARQUIVO REFERENTE A ULTIMA VEZ QUE FOI USADO
                int _DIAS_LIBERT = Convert.ToInt32(Arq_DiasLiberado);
                DateTime _DATA_ONTE = Convert.ToDateTime(Arq_DataUltimoLog);

                //PEGO A NOVA DATA DANDO UM SELECT NO SERVIDOR
                DateTime _DATA_SERVIDOR = Convert.ToDateTime(DATA_SERVIDOR);


                if (_DATA_SERVIDOR.ToString("dd/MM/yyyy") != _DATA_ONTE.ToString("dd/MM/yyyy"))
                {
                    _DIAS_LIBERT -= 1;

                    //DEFINO 0 PARA O NÚMERO NÃO FICAR NEGATIVO
                    //E TER MAIS UMA STRING DE CARACTER NA HORA DE FAZER UM SUBSTRING
                    if (_DIAS_LIBERT < 0)
                    {
                        _DIAS_LIBERT = 0;
                    }

                    //VOLTO O ARQUIVO PARA VISIBLE PARA PODER GRAVAR UMA NOVA DATA
                    File.SetAttributes(CaminhoArqLibert, FileAttributes.Normal);


                    //ESCREVO A NOVA DATA DE LIBERAÇÃO
                    string[] Linhas = new string[] { _DIAS_LIBERT.ToString("00"), _DATA_SERVIDOR.ToString("dd/MM/yyyy"), Arq_TipoLibert, Arq_IdentArqui, Arq_TipoLog };

                    string Crypt = "";
                    int i = 0;
                    while (i < 5)
                    {
                        Crypt += Linhas[i] + "\n";
                        i++;
                    }

                    using (StreamWriter EscNovaDATA = new StreamWriter(CaminhoArqLibert))
                    {
                        EscNovaDATA.Write(WenProtects(Crypt));
                    }
                }


                //EXIBO AS INFORMAÇÕES NOS TEXTBOX DO FORMULARIO
                txtLiberado.Text = (Convert.ToDateTime(DATA_SERVIDOR).AddDays(_DIAS_LIBERT)).ToString("dd/MM/yyyy");
                txtQtDias.Text = _DIAS_LIBERT.ToString("00") + " DIAS";


                //MUDO A COR DO TEXTBOX DEPENDENDO DA QUANTIDADE DE DIAS RESTANTES
                if (_DIAS_LIBERT <= 5 && _DIAS_LIBERT > 2)
                {
                    txtLiberado.BackColor = Color.Yellow;
                }
                else if (_DIAS_LIBERT <= 2)
                {
                    txtLiberado.BackColor = Color.Red;
                }
                else
                {
                    txtLiberado.BackColor = Color.Green;
                }

                File.SetAttributes(CaminhoArqLibert, FileAttributes.Hidden);

                return false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                //ARQUIVOS SÃO INVALIDOS
                WenFILE_ErroDeVerificao();
                return true;
            }
        }

        //VERIFICA SE A LIBERAÇÃO ESTÁ VENCIDA
        public bool WenFILE_LibertVencida()
        {
            if (Convert.ToInt32(txtQtDias.Text.Substring(0, 2)) == 0)
            {
                WenFILE_ErroDeVerificao();
                lblLiberação.Text = "    VALIDADE ATINGIDA    ";
                lblLiberação.Visible = true;
                lblLiberação.BorderStyle = BorderStyle.FixedSingle;
                lblLiberação.BackColor = Color.Red;
                return true;
            }
            else if (Convert.ToInt32(txtQtDias.Text.Substring(0, 2)) == 1)
            {
                lblLiberação.Text = "LIMITE ATÉ";
                lblLiberação.BorderStyle = BorderStyle.None;
                lblLiberação.BackColor = Color.White;
                txtQtDias.Text = "01 DIA";
                return false;
            }
            else
            {
                lblLiberação.Text = "LIMITE ATÉ";
                lblLiberação.BorderStyle = BorderStyle.None;
                lblLiberação.BackColor = Color.White;
                return false;
            }
        }
        #endregion

        #region TODOS OS MÉTODOS REFERENTE A LIBERAÇÃO DE UM NOVO ARQUIVO

        public string SecF_RAZAO { get; set; }
        public string SecF_CNPJ { get; set; }
        public string SecF_MODULO { get; set; }
        public string SecF_DATA { get; set; }

        public Label lblRazao { get; set; }
        public Label lblCpfCnpj { get; set; }
        public Label lblModulo { get; set; }
        public Label lblDataLiberacao { get; set; }


        //DEFINE O CAMINHO DO ARQUIVO
        public string CaminhoSecF = "..\\Debug\\SecF_" + EmpresaLogada + ".xml";

        //VERIFICA SE O ARQUIVO SecF EXISTE
        public bool SecF_ArquivoExiste()
        {
            string CaminhoDoArquivo = (CaminhoSecF);

            if (!File.Exists(CaminhoDoArquivo))
            {
                MessageBox.Show("Arquivo de licença SecF não encontrado no diretório", "TechSIS LIBERAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            return false;
        }

        //VERIFICA SE O ARQUIVO SecF É VALIDO
        public bool SecF_ArquivoValido()
        {
            try
            {
                XmlDocument LerXML = new XmlDocument();
                LerXML.Load(CaminhoSecF);


                XmlNode Tech_SEC_01 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_01");
                XmlNode Tech_SEC_02 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_02");
                XmlNode Tech_SEC_03 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_03");
                XmlNode Tech_SEC_04 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_04");
                XmlNode Tech_SEC_05 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_05");

                string _Tech_SEC_01 = WenDisprotects(Tech_SEC_01.InnerText.ToString());
                string _Tech_SEC_02 = WenDisprotects(Tech_SEC_02.InnerText.ToString());
                string _Tech_SEC_03 = WenDisprotects(Tech_SEC_03.InnerText.ToString());
                string _Tech_SEC_04 = WenDisprotects(Tech_SEC_04.InnerText.ToString());
                string _Tech_SEC_05 = WenDisprotects(Tech_SEC_05.InnerText.ToString());

                //ARQUIVOS SÃO VALIDOS
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Arquivo de licença SecF é inválido ou está corrompido", "TechSIS LIBERAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
        }

        //VERIFICA SE EMPRESA DO ARQUIVO SecF
        public bool SecF_ArquivoEmpresa()
        {
            string Mensagem = "Arquivo de liberação não pertence a empresa informada";

            try
            {
                XmlDocument LerXML = new XmlDocument();
                LerXML.Load(CaminhoSecF);

                XmlNode Tech_SEC_05 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_05");
                string _Tech_SEC_05 = WenDisprotects(Tech_SEC_05.InnerText.ToString());

                //SE FOR DIFERENTE
                if (Convert.ToInt32(EmpresaLogada) != (Convert.ToInt32(_Tech_SEC_05.Substring(19, 2)) - 14))
                {
                    MessageBox.Show(Mensagem, "TechSIS LIBERAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return true;
                }

                return false;
            }
                //SE DER ERRO
            catch (Exception)
            {
                MessageBox.Show(Mensagem, "TechSIS LIBERAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
        }

        //SELECIONA OS VALORES E COLOCO NAS STRING PUBLICAS
        public void SecF_SelecionaValores()
        {
            try
            {
                XmlDocument LerXML = new XmlDocument();
                LerXML.Load(CaminhoSecF);

                //RAZÃO
                XmlNode Tech_SEC_01 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_01");
                //CPF CNPJ
                XmlNode Tech_SEC_03 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_03");
                //MODULO
                XmlNode Tech_SEC_05 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_05");

                string _Tech_SEC_01 = WenDisprotects(Tech_SEC_01.InnerText.ToString());
                string _Tech_SEC_03 = WenDisprotects(Tech_SEC_03.InnerText.ToString());
                string _Tech_SEC_05 = WenDisprotects(Tech_SEC_05.InnerText.ToString());

                SecF_RAZAO = _Tech_SEC_01;
                SecF_CNPJ = _Tech_SEC_03;
                SecF_MODULO = (Convert.ToInt32(_Tech_SEC_05.Substring(21, 2)) - 7).ToString("00");
                SecF_DATA = _Tech_SEC_05.Substring(0, 19);

                #region TROCA O NÚMERO DO MÓDULO PELO NOME
                string MODULO = "";
                if (Convert.ToInt32(SecF_MODULO) == 0)
                {
                    MODULO = "TechSIS FREE EDITION";
                }
                else if (Convert.ToInt32(SecF_MODULO) == 1)
                {
                    MODULO = "TechSIS EXPRESS EDITION";
                }
                else if (Convert.ToInt32(SecF_MODULO) == 2)
                {
                    MODULO = "TechSIS BUSINESS EDITION";
                }
                else if (Convert.ToInt32(SecF_MODULO) == 3)
                {
                    MODULO = "TechSIS CONTROLE EDITION";
                }
                else if (Convert.ToInt32(SecF_MODULO) == 4)
                {
                    MODULO = "TechSIS PRÓ EDITION";
                }
                else
                {
                    MODULO = "ERRO AO CAPTURAR MÓDULO";
                }
                #endregion

                lblRazao.Text += SecF_RAZAO;
                lblModulo.Text += MODULO;
                lblCpfCnpj.Text += FormatarCpfCnpj(SecF_CNPJ);
                lblDataLiberacao.Text += SecF_DATA;
            }
            catch (Exception)
            {
                MessageBox.Show("ERRO AO SELECIONAR VALORES DO ARQUIVO DE SEGURANÇA", "TechSIS LIBERAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
            }
        }

        //DEFINO SE ESTÁ LIBERADO O CPF OU CNPJ DO ARQUIVO
        public bool Libert_VerificaStatusPagamento(string UsuarFTP, string SenhaFTP, string FTP_HOST, string CFP_CNPJ)
        {
            #region STRING COM MOTIVOS
            string MOTIVO_ERRO_LIBERAÇÃO = "Não foi possível adicionar os dias de liberação de emergência.\n\nPOSSÍVEIS MOTIVOS DA NÃO LIBERAÇÃO DE ARQUIVO\n\b A empresa já fez o uso dos dias de emergência.\n\b O arquivo de liberação foi editado ou deletado.\n\b A criptografia do arquivo está incorreta.\n\b A data do computador está incorreta.";
            #endregion

            //DEFINO QUE O CAMINHO COMPLETO É
            string FTP_CaminhoFULL = FTP_HOST + CFP_CNPJ + ".txt";
            FtpWebRequest Requisicao = (FtpWebRequest)WebRequest.Create(FTP_CaminhoFULL);
            Requisicao.Credentials = new NetworkCredential(UsuarFTP, SenhaFTP);
            Requisicao.Method = WebRequestMethods.Ftp.GetDateTimestamp;

            try
            {
                //ARQUIVO EXISTE
                FtpWebResponse Resposta = (FtpWebResponse)Requisicao.GetResponse();
                DateTime ModifidoEm = Resposta.LastModified;
                return false;
            }

            #region ERRO SE O ARQUIVO COM O NOME DO CNPJ NÃO EXISTIR
            catch (Exception)
            {
                DialogResult AddDays = MessageBox.Show("Foi detectada pendência(s) de pagamento(s) para o CPF/CNPJ.\nEntre em contato com financeiro TechSIS para mais detalhes.\n\nDeseja tentar adicionar 05 dias de liberação de emergência?", "TechSIS LIBERAÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (AddDays == System.Windows.Forms.DialogResult.Yes)
                {
                    if ((Arq_TipoLibert == "NORMAL") && (File.Exists(CaminhoArqLibert)) && (Convert.ToInt32(Arq_IdentArqui.Substring(0,2)) == Convert.ToInt32(EmpresaLogada)))
                    {
                        Libert_NovoArquivo(5, "EMERGENCIA");
                    }
                    else
                    {
                        MessageBox.Show(MOTIVO_ERRO_LIBERAÇÃO, "TechSIS LIBERAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                return true;
            }
            #endregion
        }

        //LIBERA DIAS DE LICENÇA
        public void Libert_NovoArquivo(int QtDias, string Tipo)
        {
            try
            {
                File.Delete(CaminhoArqLibert);

                string DATA_SERVIDOR = "";

                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string strComando = "SELECT {fn NOW()} AS DATA_SERVIDOR";
                SqlCommand Comando = new SqlCommand(strComando, Conexão);
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    DATA_SERVIDOR = Dr["DATA_SERVIDOR"].ToString();
                }


                //ESCREVO A NOVA DATA DE LIBERAÇÃO
                string[] Linhas = new string[] { QtDias.ToString("00"), DateTime.Now.ToString("dd/MM/yyyy"), Tipo, EmpresaLogada + Environment.MachineName.ToUpper() + Environment.UserDomainName.ToLower(), "SECONDUSE" };

                string Crypt = "";
                int i = 0;
                while (i < 5)
                {
                    Crypt += Linhas[i] + "\n";
                    i++;
                }

                using (StreamWriter EscNovaDATA = new StreamWriter(CaminhoArqLibert))
                {
                    EscNovaDATA.Write(WenProtects(Crypt));
                }


                if (Tipo == "EMERGENCIA")
                {

                    MessageBox.Show("Foi adicionado " + QtDias.ToString("00") + " dias de liberação de EMERGÊNCIA!", "TechSIS LIBERAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Foi adicionado " + QtDias.ToString("00") + " dias de liberação NORMAL", "TechSIS LIBERAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            catch (Exception)
            {
                MessageBox.Show("ERRO AO ADICIONAR DIAS DE LICENÇA!", "TechSIS ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //SELECIONA SE É EMERGENCIA OU NORMAL
        public string Libert_TipoLibert()
        {
            if (Arq_TipoLibert == "NORMAL")
            {
                return "NORMAL";
            }
            else
            {
                return "EMERGENCIA";
            }
        }

        #endregion

        //VERIFICA SE A MÚSICA É TOCADA OU NÃO
        public void LOG_VerificaMusica(string LojaSequencia)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string ComandoMusica = "SELECT PlayMu_CON FROM TabConfi WHERE SeqLoj_CON = " + LojaSequencia;
            SqlCommand Comando = new SqlCommand(ComandoMusica, Conexão);
            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string Play = Dr["PlayMu_CON"].ToString();
                    if (Play == "True")
                    {
                        System.Media.SoundPlayer Som = new System.Media.SoundPlayer("Loggon.wav");
                        try
                        {
                            Som.Play();
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método LOG_VerificaMusica()\n\nBLOCO = TechSIS_LoginMET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método LOG_VerificaMusica()\n\nBLOCO = TechSIS_LoginMET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }



    }
}
