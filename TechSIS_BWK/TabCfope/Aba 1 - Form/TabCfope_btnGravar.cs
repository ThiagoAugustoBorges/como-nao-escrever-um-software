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
using System.Xml;

namespace TabCfope
{
    internal class TabCfope_btnGravar
    {
        public void GravarINC(TextBox txtMESTRE, TextBox txtCodigo, Panel panUpAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Button btnGravar, MethodInvoker CamposEnable, TextBox txtDescri, string LojaLogada, TextBox txtIndCod, TextBox txtIndDesc, TextBox txtComeCod, TextBox txtComeDesc, ComboBox comLocalizacao, TextBox txtUsuario)
        {
            if (txtMESTRE.Text == "INCLUIR")
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



                //Define a string e o comando
                string StringComandoINCLUIR_TABE = "INSERT INTO TabCfope VALUES(@Sequen_CFO,@Descri_CFO,@EntCom_CFO,@EntInd_CFO,@DenFor_CFO)";
                SqlCommand ComandoINCLUIR_TABE = new SqlCommand(StringComandoINCLUIR_TABE, Conexão);
                string StringComandoINCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'010300','INCLUSÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                SqlCommand ComandoINCLUIR_HIST = new SqlCommand(StringComandoINCLUIR_HIST, Conexão);

                ComandoINCLUIR_TABE.Parameters.Add("@Sequen_CFO", SqlDbType.Int).Value = txtCodigo.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@Descri_CFO", SqlDbType.VarChar).Value = txtDescri.Text;
                #region ComandoINCLUIR_TABE.Parameters.Add("@EntCom_CFO", SqlDbType.Int).Value = txtComeCod.Text;
                if (txtComeCod.Text == string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@EntCom_CFO", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@EntCom_CFO", SqlDbType.Int).Value = txtComeCod.Text;
                }
                #endregion
                #region ComandoINCLUIR_TABE.Parameters.Add("@EntInd_CFO", SqlDbType.Int).Value = txtIndCod.Text;
                if (txtIndCod.Text == string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@EntInd_CFO", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@EntInd_CFO", SqlDbType.Int).Value = txtIndCod.Text;
                }
                #endregion
                ComandoINCLUIR_TABE.Parameters.Add("@DenFor_CFO", SqlDbType.Int).Value = comLocalizacao.SelectedIndex;



                //Parametros do Insert no Historico
                ComandoINCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "INCLUSÃO DO CFOP.......: " + txtCodigo.Text.PadLeft(4, '0');
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = txtDescri.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();

                try
                {
                    ComandoINCLUIR_TABE.ExecuteNonQuery();
                    ComandoINCLUIR_HIST.ExecuteNonQuery();

                    MessageBox.Show("Dados inseridos no banco de dados com sucesso!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Seleciona ultimo registro +1
                    TabCfope_MET MET = new TabCfope_MET();
                    MET.MET_SelecionaUltimoRegistroMaisUm(txtCodigo, btnGravar, LojaLogada);


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
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarINC()\n\nBLOCO = INCLUSÃO DE DADOS\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }
        public void GravarALT(TextBox txtMESTRE, TextBox txtCodigo, Panel panUpAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Button btnGravar, MethodInvoker CamposEnable, TextBox txtDescri, string LojaLogada, TextBox txtIndCod, TextBox txtIndDesc, TextBox txtComeCod, TextBox txtComeDesc, ComboBox comLocalizacao, TextBox txtUsuario)
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



                //Define a string e o comando
                string StringComandoALTERAR_TABE = "UPDATE TabCfope SET Descri_CFO = @Descri_CFO, EntInd_CFO = @EntInd_CFO, EntCom_CFO = @EntCom_CFO, DenFor_CFO = @DenFor_CFO WHERE Sequen_CFO = @Sequen_CFO";
                SqlCommand ComandoALTERAR_TABE = new SqlCommand(StringComandoALTERAR_TABE, Conexão);
                string StringComandoINCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'010300','ALTERAÇÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                SqlCommand ComandoINCLUIR_HIST = new SqlCommand(StringComandoINCLUIR_HIST, Conexão);

                ComandoALTERAR_TABE.Parameters.Add("@Sequen_CFO", SqlDbType.Int).Value = txtCodigo.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Descri_CFO", SqlDbType.VarChar).Value = txtDescri.Text;
                #region ComandoINCLUIR_TABE.Parameters.Add("@EntCom_CFO", SqlDbType.Int).Value = txtComeCod.Text;
                if (txtComeCod.Text == string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@EntCom_CFO", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@EntCom_CFO", SqlDbType.Int).Value = txtComeCod.Text;
                }
                #endregion
                #region ComandoINCLUIR_TABE.Parameters.Add("@EntInd_CFO", SqlDbType.Int).Value = txtIndCod.Text;
                if (txtIndCod.Text == string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@EntInd_CFO", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@EntInd_CFO", SqlDbType.Int).Value = txtIndCod.Text;
                }
                #endregion
                ComandoALTERAR_TABE.Parameters.Add("@DenFor_CFO", SqlDbType.Int).Value = comLocalizacao.SelectedIndex;


                //Parametros do Insert no Historico
                ComandoINCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "ALTERAÇÃO DO CFOP......: " + txtCodigo.Text.PadLeft(4, '0');
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = txtDescri.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();

                try
                {
                    ComandoALTERAR_TABE.ExecuteNonQuery();
                    ComandoINCLUIR_HIST.ExecuteNonQuery();


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
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarALT()\n\nBLOCO = ALTERAÇÃO DE DADOS\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }
        public void GravarEXC(TextBox txtMESTRE, TextBox txtCodigo, Panel panUpAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Button btnGravar, MethodInvoker CamposEnable, TextBox txtDescri, TextBox txtUsuario, string MOTIVO_EXC, string LojaLogada)
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



                //Define a string e o comando
                string StringComandoEXCLUI_TABE = "DELETE FROM TabCfope WHERE Sequen_CFO = @Sequen_CFO";
                SqlCommand ComandoEXCLUI_TABE = new SqlCommand(StringComandoEXCLUI_TABE, Conexão);
                string StringComandoINCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'010300','EXCLUSÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                SqlCommand ComandoINCLUIR_HIST = new SqlCommand(StringComandoINCLUIR_HIST, Conexão);

                ComandoEXCLUI_TABE.Parameters.Add("@Sequen_CFO", SqlDbType.Int).Value = txtCodigo.Text;


                //Parametros do Exc no banco
                ComandoINCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "EXCLUSÃO DO CFOP.......: " + txtCodigo.Text.PadLeft(4, '0');
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = "Motivo.: " + MOTIVO_EXC;
                ComandoINCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now;

                DialogResult Exc = MessageBox.Show("Deseja DELETAR o registro do banco de dados?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Exc == DialogResult.Yes)
                {
                    try
                    {
                        ComandoEXCLUI_TABE.ExecuteNonQuery();
                        ComandoINCLUIR_HIST.ExecuteNonQuery();

                        ZerarCampos();
                        CamposDisable();
                        btnGravar.Enabled = false;
                        MessageBox.Show("REGISTRO EXCLUIDO. OPÇÃO (01.03.00) NÃO POSSUI LIXEIRA!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (SqlException Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarEXC()\n\nBLOCO = EXCLUSÃO DE DADOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarEXC()\n\nBLOCO = EXCLUSÃO DE DADOS\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    panUpAb1.Focus();
                    btnGravar.Enabled = false;
                }
            }
        }
    }
}
