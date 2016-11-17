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

namespace TabCidad
{
    internal class TabCidad_btnGravar
    {
        public void GravarINC(TextBox txtMESTRE, TextBox txtCodigo, Panel panCodigoAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Button btnGravar, MethodInvoker CamposEnable, TextBox txtDescri, TextBox txtPaisCod, TextBox txtPaisDesc, TextBox txtIbgeUF, ComboBox comUF, TextBox txtUFDesc, TextBox txtIbgeMuCod, TextBox txtIbgeMuDesc, TextBox txtIbgeEstadual, MaskedTextBox mtbCep1, MaskedTextBox mtbCep2, ComboBox comStatus, TextBox txtUsuario)
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

                //Define a string e o comando2
                string StringComandoINCLUIR_TABE = "INSERT INTO TabCidad VALUES (@Sequen_CID,@Descri_CID,@UfFede_CID,@UfInde_CID,@UfDesc_CID,@PaisCi_CID,@IbgeCi_CID,@IbgeMu_CID,@IbgeEs_CID,@CepCi1_CID,@CepCi2_CID,@Status_CID)";
                SqlCommand ComandoINCLUIR_TABE = new SqlCommand(StringComandoINCLUIR_TABE, Conexão);
                string StringComandoINCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'010200','INCLUSÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                SqlCommand ComandoINCLUIR_HIST = new SqlCommand(StringComandoINCLUIR_HIST, Conexão);


                //Parametros do Insert na tabela
                ComandoINCLUIR_TABE.Parameters.Add("@Sequen_CID", SqlDbType.Int).Value = txtCodigo.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@Descri_CID", SqlDbType.VarChar).Value = txtDescri.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@UfFede_CID", SqlDbType.VarChar).Value = comUF.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@UfInde_CID", SqlDbType.Int).Value = comUF.SelectedIndex;
                ComandoINCLUIR_TABE.Parameters.Add("@UfDesc_CID", SqlDbType.VarChar).Value = txtUFDesc.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@PaisCi_CID", SqlDbType.Int).Value = txtPaisCod.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@IbgeCi_CID", SqlDbType.Int).Value = txtIbgeUF.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@IbgeMu_CID", SqlDbType.Int).Value = txtIbgeMuCod.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@Status_CID", SqlDbType.Int).Value = comStatus.SelectedIndex;
                #region ComandoINCLUIR_TABE.Parameters.Add("@IbgeEs_CID", SqlDbType.Int)
                if (txtIbgeEstadual.Text != string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@IbgeEs_CID", SqlDbType.Int).Value = txtIbgeEstadual.Text;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@IbgeEs_CID", SqlDbType.Int).Value = 0;
                }
                #endregion
                #region ComandoINCLUIR_TABE.Parameters.Add("@CepCi1_CID", SqlDbType.Int)
                if (mtbCep1.Text != string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@CepCi1_CID", SqlDbType.Int).Value = mtbCep1.Text;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@CepCi1_CID", SqlDbType.Int).Value = 0;
                }
                #endregion
                #region ComandoINCLUIR_TABE.Parameters.Add("@CepCi2_CID", SqlDbType.Int)
                if (mtbCep2.Text != string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@CepCi2_CID", SqlDbType.Int).Value = mtbCep2.Text;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@CepCi2_CID", SqlDbType.Int).Value = 0;
                }
                #endregion


                //Parametros do Insert no historico
                ComandoINCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "INCLUSÃO DA CIDADE.....: " + txtCodigo.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = txtDescri.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();

                try
                {
                   
                    ComandoINCLUIR_TABE.ExecuteNonQuery();
                    ComandoINCLUIR_HIST.ExecuteNonQuery();
                    MessageBox.Show("Dados inseridos com sucesso!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ZerarCampos();
                    CamposDisable();

                    TabCidad_MET MET = new TabCidad_MET();
                    MET.MET_SelecionaUltimoRegistroMaisUm(txtCodigo, btnGravar);

                    txtCodigo.Select(); txtCodigo.SelectAll();
                    btnGravar.Enabled = false;
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
        public void GravarALT(TextBox txtMESTRE, TextBox txtCodigo, Panel panCodigoAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Button btnGravar, MethodInvoker CamposEnable, TextBox txtDescri, TextBox txtPaisCod, TextBox txtPaisDesc, TextBox txtIbgeUF, ComboBox comUF, TextBox txtUFDesc, TextBox txtIbgeMuCod, TextBox txtIbgeMuDesc, TextBox txtIbgeEstadual, MaskedTextBox mtbCep1, MaskedTextBox mtbCep2, ComboBox comStatus, TextBox txtUsuario)
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

                //Define a string e o comando2
                string StringComandoALTERAR_TABE = "UPDATE TabCidad SET Descri_CID = @Descri_CID, UfFede_CID = @UfFede_CID, UfInde_CID = @UfInde_CID, UfDesc_CID = @UfDesc_CID, PaisCi_CID = @PaisCi_CID, IbgeCi_CID = @IbgeCi_CID, IbgeMu_CID = @IbgeMu_CID, IbgeEs_CID = @IbgeEs_CID, CepCi1_CID = @CepCi1_CID, CepCi2_CID = @CepCi2_CID, Status_CID = @Status_CID WHERE Sequen_CID = @Sequen_CID";
                SqlCommand ComandoALTERAR_TABE = new SqlCommand(StringComandoALTERAR_TABE, Conexão);
                string StringComandoALTERAR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'010200','ALTERAÇÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                SqlCommand ComandoALTERAR_HIST = new SqlCommand(StringComandoALTERAR_HIST, Conexão);


                //Parametros do Insert na tabela
                ComandoALTERAR_TABE.Parameters.Add("@Sequen_CID", SqlDbType.Int).Value = txtCodigo.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Descri_CID", SqlDbType.VarChar).Value = txtDescri.Text;
                ComandoALTERAR_TABE.Parameters.Add("@UfFede_CID", SqlDbType.VarChar).Value = comUF.Text;
                ComandoALTERAR_TABE.Parameters.Add("@UfInde_CID", SqlDbType.Int).Value = comUF.SelectedIndex;
                ComandoALTERAR_TABE.Parameters.Add("@UfDesc_CID", SqlDbType.VarChar).Value = txtUFDesc.Text;
                ComandoALTERAR_TABE.Parameters.Add("@PaisCi_CID", SqlDbType.Int).Value = txtPaisCod.Text;
                ComandoALTERAR_TABE.Parameters.Add("@IbgeCi_CID", SqlDbType.Int).Value = txtIbgeUF.Text;
                ComandoALTERAR_TABE.Parameters.Add("@IbgeMu_CID", SqlDbType.Int).Value = txtIbgeMuCod.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Status_CID", SqlDbType.Int).Value = comStatus.SelectedIndex;
                #region ComandoALTERAR_TABE.Parameters.Add("@IbgeEs_CID", SqlDbType.Int)
                if (txtIbgeEstadual.Text != string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@IbgeEs_CID", SqlDbType.Int).Value = txtIbgeEstadual.Text;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@IbgeEs_CID", SqlDbType.Int).Value = 0;
                }
                #endregion
                #region ComandoALTERAR_TABE.Parameters.Add("@CepCi1_CID", SqlDbType.Int)
                if (mtbCep1.Text != string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@CepCi1_CID", SqlDbType.Int).Value = mtbCep1.Text;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@CepCi1_CID", SqlDbType.Int).Value = 0;
                }
                #endregion
                #region ComandoALTERAR_TABE.Parameters.Add("@CepCi2_CID", SqlDbType.Int)
                if (mtbCep2.Text != string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@CepCi2_CID", SqlDbType.Int).Value = mtbCep2.Text;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@CepCi2_CID", SqlDbType.Int).Value = 0;
                }
                #endregion


                //Parametros do Insert no historico
                ComandoALTERAR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoALTERAR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "ALTERAÇÃO DA CIDADE....: " + txtCodigo.Text;
                ComandoALTERAR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = txtDescri.Text;
                ComandoALTERAR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoALTERAR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();

                try
                {
                    ComandoALTERAR_TABE.ExecuteNonQuery();
                    ComandoALTERAR_HIST.ExecuteNonQuery();
                    MessageBox.Show("Dados alterados com sucesso!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ZerarCampos();
                    CamposDisable();


                    txtCodigo.Select(); txtCodigo.SelectAll();
                    btnGravar.Enabled = false;
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
        public void GravarEXC(TextBox txtMESTRE, TextBox txtCodigo, string Motivo, TextBox txtUsuario)
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
                string StringComandoEXCLUI_TABE = "UPDATE TabCidad SET Status_CID = 3 WHERE Sequen_CID = @Sequen_CID";
                string StringComandoEXCLUI_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'010200','EXCLUSÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                SqlCommand ComandoEXCLUI_HIST = new SqlCommand(StringComandoEXCLUI_HIST, Conexão);
                SqlCommand ComandoEXCLUI_TABE = new SqlCommand(StringComandoEXCLUI_TABE, Conexão);


                //Parametros do Insert na tabela
                ComandoEXCLUI_TABE.Parameters.Add("@Sequen_CID", SqlDbType.Int).Value = txtCodigo.Text;


                //Parametros do Exc no banco
                ComandoEXCLUI_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoEXCLUI_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "EXCLUSÃO DA CIDADE.....: " + txtCodigo.Text;
                ComandoEXCLUI_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = "Motivo.: " + Motivo;
                ComandoEXCLUI_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoEXCLUI_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now;

                DialogResult Exc = MessageBox.Show("Deseja mover o registro para a LIXEIRA?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Exc == DialogResult.Yes)
                {
                    try
                    {
                        ComandoEXCLUI_TABE.ExecuteNonQuery();
                        ComandoEXCLUI_HIST.ExecuteNonQuery();
                        MessageBox.Show("Dados excluidos com sucesso!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            }
        }
    }
}
