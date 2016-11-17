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

namespace TabProgr
{
    internal class TabProgr_btnGRAVAR
    {
        public void GravarINC(TextBox txtMESTRE, TextBox txtCodigo, TextBox txtDescricao, ComboBox comStatus, ComboBox comModulo, TextBox txtUsuario, TextBox txtTotalProgramas, MethodInvoker ZerarCampos, Button btnGravar, MethodInvoker CamposDisable)
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
                string StringComandoINCLUIR_TABE = "INSERT INTO [dbo].[TabProgr]([Sequen_PGR],[Descri_PGR],[DtCada_PGR],[Status_PGR],[Modulo_PGR]) VALUES (@Sequen_PGR,@Descri_PGR,@DtCada_PGR,@Status_PGR,@Modulo_PGR)";
                SqlCommand ComandoINCLUIR_TABE = new SqlCommand(StringComandoINCLUIR_TABE, Conexão);
                string StringComandoINCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'090200','INCLUSÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                SqlCommand ComandoINCLUIR_HIST = new SqlCommand(StringComandoINCLUIR_HIST, Conexão);


                //Parametros do Insert na tabela
                ComandoINCLUIR_TABE.Parameters.Add("@Sequen_PGR", SqlDbType.VarChar).Value = txtCodigo.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@Descri_PGR", SqlDbType.VarChar).Value = txtDescricao.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@DtCada_PGR", SqlDbType.DateTime).Value = DateTime.Now.ToString();
                ComandoINCLUIR_TABE.Parameters.Add("@Status_PGR", SqlDbType.Int).Value = comStatus.SelectedIndex;
                ComandoINCLUIR_TABE.Parameters.Add("@Modulo_PGR", SqlDbType.Int).Value = comModulo.SelectedIndex;


                //Parametros do Insert no Historico
                ComandoINCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "INCLUSÃO DO PROGRAMA...: " + txtCodigo.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = txtDescricao.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();

                try
                {
                    ComandoINCLUIR_HIST.ExecuteNonQuery();
                    ComandoINCLUIR_TABE.ExecuteNonQuery();
                    MessageBox.Show("Dados inseridos no banco de dados com sucesso!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Seleciona o número de programas cadastrados
                    TabProgr_MET MET = new TabProgr_MET();
                    MET.MET_NúmeroDeProgramas(txtTotalProgramas);


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
        public void GravarALT(TextBox txtMESTRE, TextBox txtCodigo, TextBox txtDescricao, ComboBox comStatus, ComboBox comModulo, TextBox txtUsuario, TextBox txtTotalProgramas, MethodInvoker ZerarCampos, Button btnGravar, MethodInvoker CamposDisable)
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
                string StringComandoALTERAR_TABE = "UPDATE TabProgr SET Descri_PGR = @Descri_PGR,Status_PGR = @Status_PGR,Modulo_PGR = @Modulo_PGR WHERE Sequen_PGR = @Sequen_PGR";
                SqlCommand ComandoALTERAR_TABE = new SqlCommand(StringComandoALTERAR_TABE, Conexão);
                string StringComandoALTERAR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'090200','ALTERAÇÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                SqlCommand ComandoALTERAR_HIST = new SqlCommand(StringComandoALTERAR_HIST, Conexão);

                //Parametros do Insert na tabela
                ComandoALTERAR_TABE.Parameters.Add("@Sequen_PGR", SqlDbType.VarChar).Value = txtCodigo.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Descri_PGR", SqlDbType.VarChar).Value = txtDescricao.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Status_PGR", SqlDbType.Int).Value = comStatus.SelectedIndex;
                ComandoALTERAR_TABE.Parameters.Add("@Modulo_PGR", SqlDbType.Int).Value = comModulo.SelectedIndex;

                //Parametros do Insert no banco
                ComandoALTERAR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoALTERAR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "ALTERAÇÃO DO PROGRAMA..: " + txtCodigo.Text;
                ComandoALTERAR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = txtDescricao.Text;
                ComandoALTERAR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoALTERAR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now;

                try
                {
                    ComandoALTERAR_HIST.ExecuteNonQuery();
                    ComandoALTERAR_TABE.ExecuteNonQuery();

                    MessageBox.Show("Dados alterados com sucesso no banco de dados!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ZerarCampos();
                    btnGravar.Enabled = false;
                    txtCodigo.Select(); txtCodigo.SelectAll();
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
        public void GravarEXC(TextBox txtMESTRE, TextBox txtCodigo, TextBox txtDescricao, ComboBox comStatus, ComboBox comModulo, TextBox txtUsuario, TextBox txtTotalProgramas, MethodInvoker ZerarCampos, Button btnGravar, MethodInvoker CamposDisable, string Motivo, GroupBox grbControls)
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
                string StringComandoEXCLUIR_TABE = "UPDATE TabProgr SET Status_PGR = @Status_PGR WHERE Sequen_PGR = @Sequen_PGR";
                SqlCommand ComandoEXCLUIR_TABE = new SqlCommand(StringComandoEXCLUIR_TABE, Conexão);
                string StringComandoEXCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'090200','EXCLUSÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                SqlCommand ComandoEXCLUIR_HIST = new SqlCommand(StringComandoEXCLUIR_HIST, Conexão);

                //Parametros do Exc na tabela
                ComandoEXCLUIR_TABE.Parameters.Add("@Sequen_PGR", SqlDbType.VarChar).Value = txtCodigo.Text;
                ComandoEXCLUIR_TABE.Parameters.Add("@Status_PGR", SqlDbType.Int).Value = "3";

                //Parametros do Exc no banco
                ComandoEXCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoEXCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "EXCLUSÃO DO PROGRAMA...: " + txtCodigo.Text;
                ComandoEXCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = "Motivo.: " + Motivo;
                ComandoEXCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoEXCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now;

                DialogResult Exc = MessageBox.Show("Deseja mover o registro para a LIXEIRA?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Exc == DialogResult.Yes)
                {
                    try
                    {
                        ComandoEXCLUIR_TABE.ExecuteNonQuery();
                        ComandoEXCLUIR_HIST.ExecuteNonQuery();
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
                    grbControls.Focus();
                    btnGravar.Enabled = false;
                }
            }
        }
    }
}
