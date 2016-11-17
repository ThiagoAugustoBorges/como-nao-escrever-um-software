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
    internal class TabSetor_ExecSETAS
    {
        public void ExecSETAS_SET(string StringComandoSELEÇÃO, TextBox txtMESTRE, TextBox txtSetCod, TextBox txtSubCod, MethodInvoker CamposDisable, Button btnGravar, TextBox txtDescri, string LojaLogada, ComboBox comStatus, TextBox txtRespon, TextBox txtLocali)
        {
            string Mensagem_De_Erro = "";

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            #region Comandos
            if (StringComandoSELEÇÃO == "1")
            {
                #region SETA 1
                string Num = txtSetCod.Text;
                if (txtSetCod.Text == string.Empty)
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabSetor WHERE Sequen_SET LIKE '%0000%' AND Status_SET <> 3";
                    Mensagem_De_Erro = "Não existe setor anterior para exibir!";
                }
                else
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabSetor WHERE Sequen_SET LIKE '%0000%' AND LEFT(RIGHT(REPLICATE('0',07) + CAST(CAST(Sequen_SET AS INT) AS VARCHAR),07),3) < " + Num + " AND Status_SET <> 3 ORDER BY Sequen_SET  DESC";
                    Mensagem_De_Erro = "Não existe setor anterior para exibir!";
                }
                #endregion
            }
            if (StringComandoSELEÇÃO == "4")
            {
                #region SETA 4
                string Num = txtSetCod.Text;
                if (txtSetCod.Text == string.Empty)
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabSetor WHERE Sequen_SET LIKE '%0000%' AND Status_SET <> 3 ORDER BY Sequen_SET";
                    Mensagem_De_Erro = "Não existe próximo setor para exibir!";
                }
                else
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabSetor WHERE Sequen_SET LIKE '%0000%' AND LEFT(RIGHT(REPLICATE('0',07) + CAST(CAST(Sequen_SET AS INT) AS VARCHAR),07),3) > " + Num + " AND Status_SET <> 3 ORDER BY Sequen_SET";
                    Mensagem_De_Erro = "Não existe próximo setor para exibir!";
                }
                #endregion
            }
            #endregion


            SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);

            try
            {
                SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    txtSetCod.Text = Dr["Sequen_SET"].ToString().PadLeft(7, '0').Substring(0, 3);
                    txtSubCod.Text = "0000";
                    txtDescri.Text = Dr["Descri_SET"].ToString();
                    txtLocali.Text = Dr["Locali_SET"].ToString();
                    txtRespon.Text = Dr["Respon_SET"].ToString();
                    #region comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_SET"]) - 1;
                    try
                    {
                        comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_SET"]) - 1;
                    }
                    catch (Exception)
                    {
                        comStatus.SelectedIndex = -1;
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show(Mensagem_De_Erro, "Consulta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método ExecSETAS()\n\nBLOCO = CONSULTA DE DADOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método ExecSETAS()\n\nBLOCO = CONSULTA DE DADOS\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        public void ExecSETAS_SUB(string StringComandoSELEÇÃO, TextBox txtMESTRE, TextBox txtSetCod, TextBox txtSubCod, MethodInvoker CamposDisable, Button btnGravar, TextBox txtDescri, string LojaLogada, ComboBox comStatus, TextBox txtRespon, TextBox txtLocali, Button btnSeta1, Button btnSeta4)
        {
            string Mensagem_De_Erro = "";


            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            #region Comandos
            if (StringComandoSELEÇÃO == "2")
            {
                #region SETA 2
                if (txtSubCod.Text == "0000" || txtSubCod.Text == string.Empty)
                {
                    if (txtSetCod.Text != string.Empty)
                    {
                        StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabSetor WHERE Sequen_SET NOT LIKE '%0000%' AND Status_SET <> 3 AND LEFT(RIGHT(REPLICATE('0',07) + CAST(CAST(Sequen_SET AS INT) AS VARCHAR),07),3) = " + txtSetCod.Text;
                    }
                    else
                    {
                        StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabSetor WHERE Sequen_SET NOT LIKE '%0000%' AND Status_SET <> 3";
                    }

                    Mensagem_De_Erro = "Não existe registro anterior!";
                }
                else
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 LEFT(RIGHT(REPLICATE('0',07) + CAST(CAST(Sequen_SET AS INT) AS VARCHAR),07),7) as Sequen_SET, Descri_SET,Respon_SET,Locali_SET,Status_SET FROM TabSetor WHERE Sequen_SET NOT LIKE '%0000%' AND LEFT(RIGHT(REPLICATE('0',07) + CAST(CAST(Sequen_SET AS INT) AS VARCHAR),07),3) = " + txtSetCod.Text + " AND LEFT(RIGHT(REPLICATE('0',07) + CAST(CAST(Sequen_SET AS INT) AS VARCHAR),07),7) < " + txtSetCod.Text + txtSubCod.Text + " AND Status_SET <> 3 ORDER BY Sequen_SET DESC";
                    Mensagem_De_Erro = "Não existe registro anterior!";
                }
                #endregion
            }
            if (StringComandoSELEÇÃO == "3")
            {
                #region SETA 3
                if (txtSubCod.Text == "0000" || txtSubCod.Text == string.Empty)
                {
                    if (txtSetCod.Text != string.Empty)
                    {
                        StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabSetor WHERE Sequen_SET NOT LIKE '%0000%' AND Status_SET <> 3 AND LEFT(RIGHT(REPLICATE('0',07) + CAST(CAST(Sequen_SET AS INT) AS VARCHAR),07),3) = " + txtSetCod.Text;
                    }
                    else
                    {
                        StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabSetor WHERE Sequen_SET NOT LIKE '%0000%' AND Status_SET <> 3";
                    }
                    
                    Mensagem_De_Erro = "Não existe próximo registro!";
                }
                else
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 LEFT(RIGHT(REPLICATE('0',07) + CAST(CAST(Sequen_SET AS INT) AS VARCHAR),07),7) as Sequen_SET, Descri_SET,Respon_SET,Locali_SET,Status_SET FROM TabSetor WHERE Sequen_SET NOT LIKE '%0000%' AND LEFT(RIGHT(REPLICATE('0',07) + CAST(CAST(Sequen_SET AS INT) AS VARCHAR),07),3) = " + txtSetCod.Text + " AND LEFT(RIGHT(REPLICATE('0',07) + CAST(CAST(Sequen_SET AS INT) AS VARCHAR),07),7) > " + txtSetCod.Text + txtSubCod.Text + " AND Status_SET <> 3 ORDER BY Sequen_SET";
                    Mensagem_De_Erro = "Não existe próximo registro!";
                }
                #endregion
            }
            #endregion


            SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);

            try
            {
                SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    txtSetCod.Text = Dr["Sequen_SET"].ToString().PadLeft(7, '0').Substring(0, 3);
                    txtSubCod.Text = Dr["Sequen_SET"].ToString().PadLeft(7, '0').Substring(3, 4);
                    txtDescri.Text = Dr["Descri_SET"].ToString();
                    txtLocali.Text = Dr["Locali_SET"].ToString();
                    txtRespon.Text = Dr["Respon_SET"].ToString();
                    #region comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_SET"]) - 1;
                    try
                    {
                        comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_SET"]) - 1;
                    }
                    catch (Exception)
                    {
                        comStatus.SelectedIndex = -1;
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show(Mensagem_De_Erro, "Consulta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método ExecSETAS()\n\nBLOCO = CONSULTA DE DADOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método ExecSETAS()\n\nBLOCO = CONSULTA DE DADOS\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }
    }
}
