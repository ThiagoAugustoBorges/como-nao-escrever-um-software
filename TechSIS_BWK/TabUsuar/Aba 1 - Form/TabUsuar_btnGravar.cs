using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Security;
using System.Security.Cryptography;

namespace TabUsuar
{
    internal class TabUsuar_btnGravar
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


        public string SenhaFORM { get; set; }


        public void GravarGRID(DataGridView Dgv_Empresas, string Comando, SqlConnection Conexão, TextBox txtCodigo)
        {
            int i = 0;
            while (i < Dgv_Empresas.RowCount)
            {
                try
                {
                    SqlCommand ComandoINCLUIR_GRID = new SqlCommand(Comando, Conexão);
                    ComandoINCLUIR_GRID.Parameters.Add("@SeqUsu_US1", SqlDbType.Int).Value = txtCodigo.Text;
                    ComandoINCLUIR_GRID.Parameters.Add("@SeqLoj_US1", SqlDbType.Int).Value = Convert.ToInt32(Dgv_Empresas.Rows[i].Cells["dgvcEmpresa"].Value);
                    ComandoINCLUIR_GRID.Parameters.Add("@SeqCai_US1", SqlDbType.Int).Value = Convert.ToInt32(Dgv_Empresas.Rows[i].Cells["dgvcCaixa"].Value);
                    ComandoINCLUIR_GRID.Parameters.Add("@PerCre_US1", SqlDbType.VarChar).Value = Convert.ToString(Dgv_Empresas.Rows[i].Cells["dgvcLiberCred"].Value);
                    ComandoINCLUIR_GRID.ExecuteNonQuery();
                    i++;
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarGRID()\n\nBLOCO = MÉTODO ÚNICO INCLUSÃO DO GRID\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarGRID()\n\nBLOCO = MÉTODO ÚNICO INCLUSÃO DO GRID\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public void GravarINC(TextBox txtMESTRE, TextBox txtCodigo, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Panel panCodigoAb1, TextBox txtDescri, ComboBox comPermissao, TextBox txtEmailSuporte, TextBox txtMsn, TextBox txtEmailContato, TextBox txtSkype, TextBox txtEmpreCod, TextBox txtEmpreDesc, TextBox txtApelido, TextBox txtEmpresa, TextBox txtEmpreDescDown, TextBox txtCaixa, DataGridView Dgv_Empresas, ComboBox comStatus, Button btnGravar, MethodInvoker CamposEnable, TextBox txtUsuario)
        {
            if (txtMESTRE.Text == "INCLUIR")
            {
                TabUsuar_SenhaINC SenhaForm = new TabUsuar_SenhaINC();
                SenhaForm.ShowDialog();
                SenhaFORM = SenhaForm.Senha;


                if (SenhaFORM != string.Empty)
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

                    //Define a string e o comando2
                    string StringComandoINCLUIR_TABE = "INSERT INTO TabUsuar VALUES (@Sequen_USU,@Descri_USU,@Apelid_USU,@CodLoj_USU,@Tipo01_USU,@Emai01_USU,@Mesg01_USU,@Senhas_USU,@Status_USU,@Emai02_USU,@Skype1_USU,@DtCada_USU)";
                    SqlCommand ComandoINCLUIR_TABE = new SqlCommand(StringComandoINCLUIR_TABE, Conexão);
                    string StringComandoINCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'090100','INCLUSÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                    SqlCommand ComandoINCLUIR_HIST = new SqlCommand(StringComandoINCLUIR_HIST, Conexão);

                    //INCLUI O GRID
                    string StringComandoINCLUIR_GRID = "INSERT INTO TabUsu01 VALUES (@SeqUsu_US1,@SeqLoj_US1,@SeqCai_US1,@PerCre_US1)";


                    //Parametros do Insert na tabela
                    ComandoINCLUIR_TABE.Parameters.Add("@Sequen_USU", SqlDbType.VarChar).Value = txtCodigo.Text;
                    ComandoINCLUIR_TABE.Parameters.Add("@Descri_USU", SqlDbType.VarChar).Value = txtDescri.Text;
                    ComandoINCLUIR_TABE.Parameters.Add("@Apelid_USU", SqlDbType.VarChar).Value = txtApelido.Text;
                    ComandoINCLUIR_TABE.Parameters.Add("@CodLoj_USU", SqlDbType.Int).Value = txtEmpreCod.Text;
                    ComandoINCLUIR_TABE.Parameters.Add("@Tipo01_USU", SqlDbType.Int).Value = comPermissao.SelectedIndex;
                    ComandoINCLUIR_TABE.Parameters.Add("@Emai01_USU", SqlDbType.VarChar).Value = txtEmailSuporte.Text;
                    ComandoINCLUIR_TABE.Parameters.Add("@Mesg01_USU", SqlDbType.VarChar).Value = txtMsn.Text;
                    ComandoINCLUIR_TABE.Parameters.Add("@Senhas_USU", SqlDbType.VarChar).Value = WenProtects(SenhaFORM);
                    ComandoINCLUIR_TABE.Parameters.Add("@Status_USU", SqlDbType.VarChar).Value = comStatus.SelectedIndex;
                    ComandoINCLUIR_TABE.Parameters.Add("@Emai02_USU", SqlDbType.VarChar).Value = txtEmailContato.Text;
                    ComandoINCLUIR_TABE.Parameters.Add("@Skype1_USU", SqlDbType.VarChar).Value = txtSkype.Text;
                    ComandoINCLUIR_TABE.Parameters.Add("@DtCada_USU", SqlDbType.DateTime).Value = DateTime.Now.ToString();



                    //Parametros do Insert no Historico
                    ComandoINCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                    ComandoINCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "INCLUSÃO DO USUÁRIO....: " + txtCodigo.Text;
                    ComandoINCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = txtDescri.Text;
                    ComandoINCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                    ComandoINCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();



                    try
                    {
                        ComandoINCLUIR_TABE.ExecuteNonQuery();
                        ComandoINCLUIR_HIST.ExecuteNonQuery();


                        //INCLUI O GRID NO BANCO
                        GravarGRID(Dgv_Empresas, StringComandoINCLUIR_GRID, Conexão, txtCodigo);


                        MessageBox.Show("Dados inseridos no banco de dados com sucesso!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Seleciona ultimo registro +1
                        TabUsuar_MET MET = new TabUsuar_MET();
                        MET.MET_SelecionaUltimoRegistroMaisUm(txtCodigo, btnGravar);


                        ZerarCampos();
                        txtCodigo.Select(); txtCodigo.SelectAll();
                        btnGravar.Enabled = false;
                        CamposDisable();
                    }
                    catch (SqlException Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarINC()\n\nBLOCO = INCLUSÃO DE DADOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarINC()\n\nBLOCO = INCLUSÃO DE DADOS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Conexão.Close();
                    }
                }
            }
        }
        public void GravarALT(TextBox txtMESTRE, TextBox txtCodigo, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Panel panCodigoAb1, TextBox txtDescri, ComboBox comPermissao, TextBox txtEmailSuporte, TextBox txtMsn, TextBox txtEmailContato, TextBox txtSkype, TextBox txtEmpreCod, TextBox txtEmpreDesc, TextBox txtApelido, TextBox txtEmpresa, TextBox txtEmpreDescDown, TextBox txtCaixa, DataGridView Dgv_Empresas, ComboBox comStatus, Button btnGravar, MethodInvoker CamposEnable, TextBox txtUsuario)
        {
            if (txtMESTRE.Text == "ALTERAR")
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


                //String e cria o SQLComand
                string StringComandoALTERAR_TABE = "UPDATE TabUsuar SET Descri_USU = @Descri_USU, Apelid_USU = @Apelid_USU, CodLoj_USU = @CodLoj_USU, Tipo01_USU = @Tipo01_USU, Emai01_USU = @Emai01_USU, Mesg01_USU = @Mesg01_USU, Status_USU = @Status_USU, Emai02_USU = @Emai02_USU, Skype1_USU = @Skype1_USU WHERE Sequen_USU = @Sequen";
                string StringComandoALTERAR_GRID_EX = "DELETE FROM TabUsu01 WHERE SeqUsu_US1 = " + txtCodigo.Text;
                string StringComandoALTERAR_GRID_IN = "INSERT INTO TabUsu01 VALUES (@SeqUsu_US1,@SeqLoj_US1,@SeqCai_US1,@PerCre_US1)";
                string StringComandoINCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'090100','ALTERAÇÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";


                SqlCommand ComandoINCLUIR_HIST = new SqlCommand(StringComandoINCLUIR_HIST, Conexão);
                SqlCommand ComandoALTERAR_TABE = new SqlCommand(StringComandoALTERAR_TABE, Conexão);
                SqlCommand EXCLUI_GRID = new SqlCommand(StringComandoALTERAR_GRID_EX, Conexão);


                //Parametros do Insert na tabela
                ComandoALTERAR_TABE.Parameters.Add("@Sequen", SqlDbType.VarChar).Value = txtCodigo.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Descri_USU", SqlDbType.VarChar).Value = txtDescri.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Apelid_USU", SqlDbType.VarChar).Value = txtApelido.Text;
                ComandoALTERAR_TABE.Parameters.Add("@CodLoj_USU", SqlDbType.Int).Value = txtEmpreCod.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Tipo01_USU", SqlDbType.Int).Value = comPermissao.SelectedIndex;
                ComandoALTERAR_TABE.Parameters.Add("@Emai01_USU", SqlDbType.VarChar).Value = txtEmailSuporte.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Mesg01_USU", SqlDbType.VarChar).Value = txtMsn.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Status_USU", SqlDbType.VarChar).Value = comStatus.SelectedIndex;
                ComandoALTERAR_TABE.Parameters.Add("@Emai02_USU", SqlDbType.VarChar).Value = txtEmailContato.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Skype1_USU", SqlDbType.VarChar).Value = txtSkype.Text;



                //Parametros do Insert no banco
                ComandoINCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "ALTERAÇÃO DO USUÁRIO..: " + txtCodigo.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = txtDescri.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();

                try
                {
                    ComandoINCLUIR_HIST.ExecuteNonQuery();
                    ComandoALTERAR_TABE.ExecuteNonQuery();
                    EXCLUI_GRID.ExecuteNonQuery();

                    //INCLUI O GRID NO BANCO
                    GravarGRID(Dgv_Empresas, StringComandoALTERAR_GRID_IN, Conexão, txtCodigo);


                    MessageBox.Show("Dados alterados no banco de dados com sucesso!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    ZerarCampos();
                    txtCodigo.Select(); txtCodigo.SelectAll();
                    btnGravar.Enabled = false;
                    CamposDisable();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarALT()\n\nBLOCO = ALTERAÇÃO DE DADOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarALT()\n\nBLOCO = ALTERAÇÃO DE DADOS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }
        public void GravarEXC(TextBox txtMESTRE, TextBox txtCodigo, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Panel panCodigoAb1, TextBox txtDescri, ComboBox comPermissao, TextBox txtEmailSuporte, TextBox txtMsn, TextBox txtEmailContato, TextBox txtSkype, TextBox txtEmpreCod, TextBox txtEmpreDesc, TextBox txtApelido, TextBox txtEmpresa, TextBox txtEmpreDescDown, TextBox txtCaixa, DataGridView Dgv_Empresas, ComboBox comStatus, Button btnGravar, MethodInvoker CamposEnable, TextBox txtUsuario, string Motivo)
        {
            if (txtMESTRE.Text == "EXCLUIR")
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

                //String e cria o SQLComand
                string StringComandoEXCLUI_TABE = "UPDATE TabUsuar SET Status_USU = 3 WHERE Sequen_USU = @Sequen";
                string StringComandoEXCLUI_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'090100','EXCLUSÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                SqlCommand ComandoEXCLUI_HIST = new SqlCommand(StringComandoEXCLUI_HIST, Conexão);
                SqlCommand ComandoEXCLUI_TABE = new SqlCommand(StringComandoEXCLUI_TABE, Conexão);



                ComandoEXCLUI_TABE.Parameters.Add("@Sequen", SqlDbType.VarChar).Value = txtCodigo.Text;



                //Parametros do Exc no banco
                ComandoEXCLUI_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoEXCLUI_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "EXCLUSÃO DO USUÁRIO...: " + txtCodigo.Text;
                ComandoEXCLUI_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = "Motivo.: " + Motivo;
                ComandoEXCLUI_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoEXCLUI_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now;


                DialogResult Exc = MessageBox.Show("Deseja mover o registro para a LIXEIRA?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Exc == DialogResult.Yes)
                {
                    try
                    {
                        ComandoEXCLUI_HIST.ExecuteNonQuery();
                        ComandoEXCLUI_TABE.ExecuteNonQuery();
                        MessageBox.Show("Registro enviado para a LIXEIRA!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ZerarCampos();
                        CamposDisable();
                        btnGravar.Enabled = false;
                    }
                    catch (SqlException Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarEXC()\n\nBLOCO = EXCLUSÃO DE DADOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarEXC()\n\nBLOCO = EXCLUSÃO DE DADOS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Conexão.Close();
                    }
                }
                if (Exc == DialogResult.No)
                {
                    ZerarCampos();
                    CamposDisable();
                    panCodigoAb1.Focus();
                    btnGravar.Enabled = false;
                }
            }
        }
    }
}