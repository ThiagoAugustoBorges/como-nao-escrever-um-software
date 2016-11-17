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

namespace TabNewSe
{
    internal class TabNewSe_MET
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

        //Define que no formulário pode conter apenas números
        public void MET_ApenasNúmeros(KeyPressEventArgs KeyPress)
        {
            if (!Char.IsDigit(KeyPress.KeyChar) && KeyPress.KeyChar != (char)8)
            {
                KeyPress.Handled = true;
                return;
            }
        }

        //SELECIONA O USUÁRIO NO TAB
        public void MET_SelecionaUsuarioTAB(TextBox txtCodigo, TextBox txtDescri, Control Retorno, Button btnGravar, MethodInvoker ZerarCampos)
        {
            #region TRATAMENTO txtCodigo em BRANCO & txtCodigo > 999999
            if (txtCodigo.Text == string.Empty)
            {
                txtCodigo.Text = "000000";
            }
            if (Convert.ToInt32(txtCodigo.Text) == 0)
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');
                Retorno.Focus();
                txtCodigo.SelectAll();
                ZerarCampos();
                btnGravar.Enabled = false;
                MessageBox.Show("Campo (Código) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (Convert.ToInt32(txtCodigo.Text) == 1)
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');
                Retorno.Focus();
                txtCodigo.SelectAll();
                ZerarCampos();
                btnGravar.Enabled = false;
                MessageBox.Show("Senha do usuário MASTER não pode ser alterada", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (Convert.ToInt32(txtCodigo.Text) >= 999999)
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');
                Retorno.Focus();
                txtCodigo.SelectAll();
                ZerarCampos();
                btnGravar.Enabled = false;
                MessageBox.Show("Limite de sequência atingido. Verifique!", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');
            }
            #endregion

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string strComando = "SELECT Sequen_USU,Apelid_USU,Status_USU FROM TabUsuar WHERE Sequen_USU = @Sequen_USU";
            SqlCommand Comando = new SqlCommand(strComando, Conexão);

            Comando.Parameters.Add("@Sequen_USU", SqlDbType.Int).Value = txtCodigo.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string STATUS = Dr["Status_USU"].ToString();

                    if (STATUS == "1")
                    {
                        ZerarCampos();
                        txtDescri.Text = Dr["Apelid_USU"].ToString();
                        btnGravar.Enabled = true;
                    }
                    else
                    {
                        Retorno.Focus();
                        txtCodigo.SelectAll();
                        ZerarCampos();
                        btnGravar.Enabled = false;

                        string NomeSTATUS = "";
                        #region Nomes
                        if (STATUS == "2")
                        {
                            NomeSTATUS = "INATIVO";
                        }
                        else if (STATUS == "3")
                        {
                            NomeSTATUS = "EXCLUIDO";
                        }
                        else
                        {
                            NomeSTATUS = "ERRO";
                        }
                        #endregion


                        MessageBox.Show("Usuário consta como (" + NomeSTATUS + "). Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    Retorno.Focus();
                    txtCodigo.SelectAll();
                    ZerarCampos();
                    btnGravar.Enabled = false;
                    MessageBox.Show("Usuário inexistente no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelecionaUsuarioTAB()\n\nBLOCO = TabNewSe_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelecionaUsuarioTAB()\n\nBLOCO = TabNewSe_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //Verifica se a senha digitada é a mesma do cadastrado no banco de dados
        public bool MET_VerificaSenha(TextBox txtCodigo, TextBox txtSenha, Control Retorno, Form FORMULARIO)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string SeleçãoSenha = "SELECT Senhas_USU FROM TabUsuar WHERE Sequen_USU = @Sequen";
            SqlCommand SenhaGO = new SqlCommand(SeleçãoSenha, Conexão);
            SenhaGO.Parameters.Add("@Sequen", SqlDbType.Int).Value = txtCodigo.Text;
            SqlDataReader Dr = SenhaGO.ExecuteReader(); Dr.Read();

            try
            {
                string _Senha = WenDisprotects(Dr["Senhas_USU"].ToString());

                if (txtSenha.Text != _Senha)
                {
                    MessageBox.Show("SENHA INFORMADA ESTÁ INCORRETA", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Retorno.Focus(); txtSenha.SelectAll();
                    return true;
                }

                return false;
            }
            catch (FormatException)
            {
                MessageBox.Show("Atenção.: Criptografia do banco de dados está incorreta", "TechSIS Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Usuário deverá ser EXCLUIDO e INCLUIDO manualmente no banco", "TechSIS Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FORMULARIO.Close();
                return true;
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerificaSenha()\n\nBLOCO = TabNewSe_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerificaSenha()\n\nBLOCO = TabNewSe_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            finally
            {
                Conexão.Close();
            }
        }

        //GRAVA A NOVA SENHA NO BANCO DE DADOS
        public void MET_GravaNovaSenha(TextBox txtCodigo, TextBox txtSenhaNova, Button btnGravar, MethodInvoker ZerarCampos, TextBox txtUsuario)
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

            string StringComandoINCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'090500','SENHA',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
            SqlCommand ComandoINCLUIR_HIST = new SqlCommand(StringComandoINCLUIR_HIST, Conexão);

            string SeleçãoSenha = "UPDATE TabUsuar SET Senhas_USU = @Senhas_USU WHERE Sequen_USU = @Sequen_USU";
            SqlCommand SenhaGO = new SqlCommand(SeleçãoSenha, Conexão);
            SenhaGO.Parameters.Add("@Sequen_USU", SqlDbType.Int).Value = txtCodigo.Text;
            SenhaGO.Parameters.Add("@Senhas_USU", SqlDbType.VarChar).Value = WenProtects(txtSenhaNova.Text);

            //Parametros do Insert no historico
            ComandoINCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
            ComandoINCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "ALTERAÇÃO DE SENHA DO USUÁRIO " + txtCodigo.Text;
            ComandoINCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = "EXECUTADA PELO USUÁRIO " + txtUsuario.Text;
            ComandoINCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
            ComandoINCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();

            try
            {
                DialogResult CONFIRMA = MessageBox.Show("CONFIRMA A TROCA DE SENHA PARA O USUÁRIO " + txtCodigo.Text + "?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (CONFIRMA == DialogResult.Yes)
                {
                    SenhaGO.ExecuteNonQuery();
                    ComandoINCLUIR_HIST.ExecuteNonQuery();
                    ZerarCampos();
                    btnGravar.Enabled = false;
                    txtCodigo.Select(); txtCodigo.SelectAll();
                    MessageBox.Show("SENHA MODIFICADA COM SUCESSO NO BANCO DE DADOS", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ZerarCampos();
                    btnGravar.Enabled = false;
                    txtCodigo.Select(); txtCodigo.SelectAll();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_GravaNovaSenha()\n\nBLOCO = TabNewSe_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_GravaNovaSenha()\n\nBLOCO = TabNewSe_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }
    }
}
