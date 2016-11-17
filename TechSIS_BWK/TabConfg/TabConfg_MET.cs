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

namespace TabConfg
{
    internal class TabConfg_MET
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

        //SELECIONA A SENHA PARA VER SE ESTÁ CORRETA
        public bool MET_SelectSenha(TextBox txtSenha, Button btnSenha, MethodInvoker CamposEnable, Button btnGravar, CheckBox cheGravaFiltros)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string strSelectSenha = "SELECT Senhas_USU FROM TabUsuar WHERE Sequen_USU = 1";
            SqlCommand Comando = new SqlCommand(strSelectSenha, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string _Senha = WenDisprotects(Dr["Senhas_USU"].ToString());

                    if (_Senha != txtSenha.Text)
                    {
                        MessageBox.Show("TechSIS - SENHA INFORMADA ESTÁ INCORRETA!", "SENHA DO USUÁRIO MASTER", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSenha.Select(); txtSenha.SelectAll();
                        return true;
                    }
                    else
                    {
                        btnSenha.Enabled = false;
                        txtSenha.Enabled = false;
                        CamposEnable();
                        btnGravar.Enabled = true;
                        cheGravaFiltros.Select();
                    }
                }
                else
                {
                    MessageBox.Show("NÃO FOI ENCONTRADO O USUÁRIO MASTER NO BANCO", "TechSIS ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSenha.Select(); txtSenha.SelectAll();
                    return true;
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelectSenha()\n\nBLOCO = TabConfg_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelectSenha()\n\nBLOCO = TabConfg_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            finally
            {
                Conexão.Close();
            }


            return false;
        }

        //SELECIONA DO BANCO
        public void MET_SelectResult(string Sequen_Loja, CheckBox cheGravaFiltros, NumericUpDown nupQtResultados, TextBox txtCaminhoREL, NumericUpDown nupOcioso, CheckBox cheTocarBoasVindas, CheckBox cheMasterLixeira, CheckBox cheMasterLoja, CheckBox cheAlertaReceber, CheckBox cheAlertaPagar, CheckBox cheDatasComemorativas, CheckBox cheAlertaOffice, CheckBox cheTrocarUsuario, CheckBox cheOcultarPainel, CheckBox cheMsgEmergencia, TextBox txtAtalho1, TextBox txtAtalho2, TextBox txtAtalhoDll1, TextBox txtAtalhoDll2, Panel panMeio, CheckBox cheLancCaixa)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string BuscaBD = "SELECT * FROM TabConfi WHERE SeqLoj_CON = " + Sequen_Loja;
            SqlCommand Comando = new SqlCommand(BuscaBD, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();

                if (Dr.HasRows)
                {
                    nupQtResultados.Value = Convert.ToDecimal(Dr["QtPesq_CON"]);
                    txtCaminhoREL.Text = Dr["CamRel_CON"].ToString();
                    nupOcioso.Value = Convert.ToDecimal(Dr["TimeOc_CON"]);

                    #region cheGravaFiltros
                    string strcheGravaFiltros = Dr["GraXML_CON"].ToString();
                    if (strcheGravaFiltros == "True")
                    {
                        cheGravaFiltros.Checked = true;
                    }
                    else
                    {
                        cheGravaFiltros.Checked = false;
                    }
                    #endregion
                    #region cheTocarBoasVindas
                    string strcheTocarBoasVindas = Dr["PlayMu_CON"].ToString();
                    if (strcheTocarBoasVindas == "True")
                    {
                        cheTocarBoasVindas.Checked = true;
                    }
                    else
                    {
                        cheTocarBoasVindas.Checked = false;
                    }
                    #endregion
                    #region cheMasterLixeira
                    string strcheMasterLixeira = Dr["LixMas_CON"].ToString();
                    if (strcheMasterLixeira == "True")
                    {
                        cheMasterLixeira.Checked = true;
                    }
                    else
                    {
                        cheMasterLixeira.Checked = false;
                    }
                    #endregion
                    #region cheMasterLoja
                    string strcheMasterLoja = Dr["LojMas_CON"].ToString();
                    if (strcheMasterLoja == "True")
                    {
                        cheMasterLoja.Checked = true;
                    }
                    else
                    {
                        cheMasterLoja.Checked = false;
                    }
                    #endregion
                    #region cheAlertaReceber
                    string strcheAlertaReceber = Dr["AleRec_CON"].ToString();
                    if (strcheAlertaReceber == "True")
                    {
                        cheAlertaReceber.Checked = true;
                    }
                    else
                    {
                        cheAlertaReceber.Checked = false;
                    }
                    #endregion
                    #region cheAlertaPagar
                    string strcheAlertaPagar = Dr["AlePag_CON"].ToString();
                    if (strcheAlertaPagar == "True")
                    {
                        cheAlertaPagar.Checked = true;
                    }
                    else
                    {
                        cheAlertaPagar.Checked = false;
                    }
                    #endregion
                    #region cheDatasComemorativas
                    string strcheDatasComemorativas = Dr["DatCom_CON"].ToString();
                    if (strcheDatasComemorativas == "True")
                    {
                        cheDatasComemorativas.Checked = true;
                    }
                    else
                    {
                        cheDatasComemorativas.Checked = false;
                    }
                    #endregion
                    #region cheAlertaOffice
                    string strcheAlertaOffice = Dr["AleOff_CON"].ToString();
                    if (strcheAlertaOffice == "True")
                    {
                        cheAlertaOffice.Checked = true;
                    }
                    else
                    {
                        cheAlertaOffice.Checked = false;
                    }
                    #endregion
                    #region cheTrocarUsuario
                    string strcheTrocarUsuario = Dr["PerUsu_CON"].ToString();
                    if (strcheTrocarUsuario == "True")
                    {
                        cheTrocarUsuario.Checked = true;
                    }
                    else
                    {
                        cheTrocarUsuario.Checked = false;
                    }
                    #endregion
                    #region cheOcultarPainel
                    string strcheOcultarPainel = Dr["OcuPan_CON"].ToString();
                    if (strcheOcultarPainel == "True")
                    {
                        cheOcultarPainel.Checked = true;
                    }
                    else
                    {
                        cheOcultarPainel.Checked = false;
                    }
                    #endregion
                    #region cheMsgEmergencia
                    string strcheMsgEmergencia = Dr["ExiEme_CON"].ToString();
                    if (strcheMsgEmergencia == "True")
                    {
                        cheMsgEmergencia.Checked = true;
                    }
                    else
                    {
                        cheMsgEmergencia.Checked = false;
                    }
                    #endregion
                    #region cheLancCaixa
                    string strcheLancCaixa = Dr["LacCai_CON"].ToString();
                    if (strcheLancCaixa == "True")
                    {
                        cheLancCaixa.Checked = true;
                    }
                    else
                    {
                        cheLancCaixa.Checked = false;
                    }
                    #endregion

                    txtAtalho1.Text = Dr["NomAt1_CON"].ToString();
                    txtAtalho2.Text = Dr["NomAt2_CON"].ToString();
                    txtAtalhoDll1.Text = Dr["NomDl1_CON"].ToString();
                    txtAtalhoDll2.Text = Dr["NomDl2_CON"].ToString();
                }
                else
                {

                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelectResult()\n\nBLOCO = TabConfg_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("      TechSIS ERRO.: EXISTEM VALORES NULOS NA TABELA\nREFAÇA A CONFIGURAÇÃO E TECLE (CONFIRMAR) NOVAMENTE", "TechSIS InvalidCastException Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            { 
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelectResult()\n\nBLOCO = TabConfg_MET\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //GRAVA NO BANCO
        public void MET_GravarResult(string Sequen_Loja, string Codigo_Uso, string Descri_Uso, CheckBox cheGravaFiltros, NumericUpDown nupQtResultados, TextBox txtCaminhoREL, NumericUpDown nupOcioso, CheckBox cheTocarBoasVindas, CheckBox cheMasterLixeira, CheckBox cheMasterLoja, CheckBox cheAlertaReceber, CheckBox cheAlertaPagar, CheckBox cheDatasComemorativas, CheckBox cheAlertaOffice, CheckBox cheTrocarUsuario, CheckBox cheOcultarPainel, CheckBox cheMsgEmergencia, TextBox txtAtalho1, TextBox txtAtalho2, TextBox txtAtalhoDll1, TextBox txtAtalhoDll2, Panel panMeio, CheckBox cheLancCaixa)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            //Pega o Ultimo registro da Tab de Historico!
            string PegarULTIMORegHIST = "SELECT MAX (Sequen_HIS + 1) FROM TabHisto";
            SqlCommand PegarREG = new SqlCommand(PegarULTIMORegHIST, Conexão);
            SqlDataReader Dr = PegarREG.ExecuteReader(); Dr.Read();
            int _SequenHIS = Convert.ToInt32(Dr[0].ToString());
            Dr.Close();


            string StringComandoINCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'090400','CONFIGURAÇÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
            SqlCommand ComandoINCLUIR_HIST = new SqlCommand(StringComandoINCLUIR_HIST, Conexão);

            string UpDATE = "UPDATE TabConfi SET QtPesq_CON = @QtPesq_CON, GraXML_CON = @GraXML_CON, CorFun_CON = @CorFun_CON, CamRel_CON = @CamRel_CON, PlayMu_CON = @PlayMu_CON, LixMas_CON = @LixMas_CON, TimeOc_CON = @TimeOc_CON, LojMas_CON = @LojMas_CON, AleRec_CON = @AleRec_CON, AlePag_CON = @AlePag_CON, DatCom_CON = @DatCom_CON, AleOff_CON = @AleOff_CON, OcuPan_CON = @OcuPan_CON, ExiEme_CON = @ExiEme_CON, PerUsu_CON = @PerUsu_CON, NomAt1_CON = @NomAt1_CON, NomAt2_CON = @NomAt2_CON, NomDl1_CON = @NomDl1_CON, NomDl2_CON = @NomDl2_CON, LacCai_CON = @LacCai_CON WHERE SeqLoj_CON = " + Sequen_Loja;
            SqlCommand Comando = new SqlCommand(UpDATE, Conexão);

            Comando.Parameters.Add("@QtPesq_CON", SqlDbType.Int).Value = nupQtResultados.Value;
            Comando.Parameters.Add("@GraXML_CON", SqlDbType.VarChar).Value = cheGravaFiltros.Checked;
            Comando.Parameters.Add("@CorFun_CON", SqlDbType.VarChar).Value = panMeio.BackColor.Name;
            Comando.Parameters.Add("@CamRel_CON", SqlDbType.VarChar).Value = txtCaminhoREL.Text;
            Comando.Parameters.Add("@PlayMu_CON", SqlDbType.VarChar).Value = cheTocarBoasVindas.Checked;
            Comando.Parameters.Add("@LixMas_CON", SqlDbType.VarChar).Value = cheMasterLixeira.Checked;
            Comando.Parameters.Add("@TimeOc_CON", SqlDbType.VarChar).Value = nupOcioso.Value;
            Comando.Parameters.Add("@LojMas_CON", SqlDbType.VarChar).Value = cheMasterLoja.Checked;
            Comando.Parameters.Add("@AleRec_CON", SqlDbType.VarChar).Value = cheAlertaReceber.Checked;
            Comando.Parameters.Add("@AlePag_CON", SqlDbType.VarChar).Value = cheAlertaPagar.Checked;
            Comando.Parameters.Add("@DatCom_CON", SqlDbType.VarChar).Value = cheDatasComemorativas.Checked;
            Comando.Parameters.Add("@AleOff_CON", SqlDbType.VarChar).Value = cheAlertaOffice.Checked;
            Comando.Parameters.Add("@OcuPan_CON", SqlDbType.VarChar).Value = cheOcultarPainel.Checked;
            Comando.Parameters.Add("@ExiEme_CON", SqlDbType.VarChar).Value = cheMsgEmergencia.Checked;
            Comando.Parameters.Add("@PerUsu_CON", SqlDbType.VarChar).Value = cheTrocarUsuario.Checked;
            Comando.Parameters.Add("@LacCai_CON", SqlDbType.VarChar).Value = cheLancCaixa.Checked;
            Comando.Parameters.Add("@NomAt1_CON", SqlDbType.VarChar).Value = txtAtalho1.Text;
            Comando.Parameters.Add("@NomAt2_CON", SqlDbType.VarChar).Value = txtAtalho2.Text;
            Comando.Parameters.Add("@NomDl1_CON", SqlDbType.VarChar).Value = txtAtalhoDll1.Text;
            Comando.Parameters.Add("@NomDl2_CON", SqlDbType.VarChar).Value = txtAtalhoDll2.Text;

            

            //Parametros do Insert no historico
            ComandoINCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
            ComandoINCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "ALTERAÇÃO DA CONFIGURAÇÃO GERAL";
            ComandoINCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = "EXECUTADA PELO USUÁRIO " + Descri_Uso;
            ComandoINCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = Codigo_Uso;
            ComandoINCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();

            try
            {
                Comando.ExecuteNonQuery();
                ComandoINCLUIR_HIST.ExecuteNonQuery();
                MessageBox.Show("DADOS CONFIRMADOS COM SUCESSO", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_GravarResult()\n\nBLOCO = TabConfg_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_GravarResult()\n\nBLOCO = TabConfg_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
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
    }
}
