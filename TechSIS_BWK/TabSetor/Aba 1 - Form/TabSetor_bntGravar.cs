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

namespace TabSetor
{
    internal class TabSetor_bntGravar
    {
        public void GravarINC(TextBox txtMESTRE, TextBox txtSetCod, TextBox txtSubCod, Control Retorno, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Button btnGravar, MethodInvoker CamposEnable, TextBox txtDescri, string LojaLogada, ComboBox comStatus, TextBox txtUsuario, TextBox txtRespon, TextBox txtLocali)
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
                string StringComandoINCLUIR_TABE = "INSERT INTO TabSetor VALUES(@Sequen_SET,@Descri_SET,@Respon_SET,@Locali_SET,@Status_SET)";
                SqlCommand ComandoINCLUIR_TABE = new SqlCommand(StringComandoINCLUIR_TABE, Conexão);
                string StringComandoINCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'010700','INCLUSÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                SqlCommand ComandoINCLUIR_HIST = new SqlCommand(StringComandoINCLUIR_HIST, Conexão);

                ComandoINCLUIR_TABE.Parameters.Add("@Sequen_SET", SqlDbType.Int).Value = txtSetCod.Text + txtSubCod.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@Descri_SET", SqlDbType.VarChar).Value = txtDescri.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@Respon_SET", SqlDbType.VarChar).Value = txtRespon.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@Locali_SET", SqlDbType.VarChar).Value = txtLocali.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@Status_SET", SqlDbType.Int).Value = comStatus.SelectedIndex + 1;



                //Parametros do Insert no Historico
                ComandoINCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "INCLUSÃO DO SETOR......: " + txtSetCod.Text + txtSubCod.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = txtDescri.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();

                try
                {
                    ComandoINCLUIR_TABE.ExecuteNonQuery();
                    ComandoINCLUIR_HIST.ExecuteNonQuery();

                    MessageBox.Show("Dados inseridos no banco de dados com sucesso!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Seleciona ultimo registro +1
                    TabSetor_MET MET = new TabSetor_MET();
                    MET.MET_SelecionaUltimoRegistroMaisUmSUB(txtSubCod, txtSetCod, btnGravar, LojaLogada);


                    ZerarCampos();
                    CamposDisable();
                    txtSubCod.Enabled = true;
                    txtSubCod.Select(); txtSubCod.SelectAll();
                    btnGravar.Enabled = false;
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
        public void GravarALT(TextBox txtMESTRE, TextBox txtSetCod, TextBox txtSubCod, Control Retorno, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Button btnGravar, MethodInvoker CamposEnable, TextBox txtDescri, string LojaLogada, ComboBox comStatus, TextBox txtUsuario, TextBox txtRespon, TextBox txtLocali)
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
                string StringComandoALTERAR_TABE = "UPDATE TabSetor SET Descri_SET = @Descri_SET, Respon_SET = @Respon_SET, Locali_SET = @Locali_SET, Status_SET = @Status_SET WHERE Sequen_SET = @Sequen_SET";
                SqlCommand ComandoALTERAR_TABE = new SqlCommand(StringComandoALTERAR_TABE, Conexão);
                string StringComandoINCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'010700','ALTERAÇÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                SqlCommand ComandoINCLUIR_HIST = new SqlCommand(StringComandoINCLUIR_HIST, Conexão);


                ComandoALTERAR_TABE.Parameters.Add("@Sequen_SET", SqlDbType.Int).Value = txtSetCod.Text + txtSubCod.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Descri_SET", SqlDbType.VarChar).Value = txtDescri.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Locali_SET", SqlDbType.VarChar).Value = txtLocali.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Respon_SET", SqlDbType.VarChar).Value = txtRespon.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Status_SET", SqlDbType.Int).Value = comStatus.SelectedIndex + 1;


                //Parametros do Insert no Historico
                ComandoINCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "ALTERAÇÃO DO SETOR.....: " + txtSetCod.Text + txtSubCod.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = txtDescri.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();

                try
                {
                    ComandoALTERAR_TABE.ExecuteNonQuery();
                    ComandoINCLUIR_HIST.ExecuteNonQuery();


                    MessageBox.Show("Dados alterados no banco de dados com sucesso!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ZerarCampos();
                    CamposDisable();
                    txtSubCod.Enabled = true;
                    txtSubCod.Select(); txtSubCod.SelectAll();
                    btnGravar.Enabled = false;
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
        public void GravarEXC(TextBox txtMESTRE, TextBox txtSetCod, TextBox txtSubCod, Control Retorno, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Button btnGravar, MethodInvoker CamposEnable, TextBox txtDescri, string LojaLogada, ComboBox comStatus, TextBox txtUsuario, TextBox txtRespon, TextBox txtLocali, string MOTIVO_EXC)
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
                string StringComandoEXCLUI_TABE = "UPDATE TabSetor SET Status_SET = 3 WHERE Sequen_SET = @Sequen_SET";
                SqlCommand ComandoEXCLUI_TABE = new SqlCommand(StringComandoEXCLUI_TABE, Conexão);
                string StringComandoINCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'010700','EXCLUSÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                SqlCommand ComandoINCLUIR_HIST = new SqlCommand(StringComandoINCLUIR_HIST, Conexão);

                ComandoEXCLUI_TABE.Parameters.Add("@Sequen_SET", SqlDbType.Int).Value = txtSetCod.Text + txtSubCod.Text;


                //Parametros do Exc no banco
                ComandoINCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "EXCLUSÃO DO SETOR......: " + txtSetCod.Text + txtSubCod.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = "Motivo.: " + MOTIVO_EXC;
                ComandoINCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now;

                DialogResult Exc = MessageBox.Show("Deseja mover o registro para a LIXEIRA?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Exc == DialogResult.Yes)
                {
                    try
                    {
                        ComandoEXCLUI_TABE.ExecuteNonQuery();
                        ComandoINCLUIR_HIST.ExecuteNonQuery();

                        ZerarCampos();
                        CamposDisable();
                        btnGravar.Enabled = false;
                        MessageBox.Show("Registro enviado para a LIXEIRA!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    Retorno.Focus();
                    btnGravar.Enabled = false;
                }
            }
        }
    }
}
