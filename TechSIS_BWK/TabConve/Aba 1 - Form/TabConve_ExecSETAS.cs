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


namespace TabConve
{
    internal class TabConve_ExecSETAS
    {
        public void ExecSETAS(string StringComandoSELEÇÃO, TextBox txtMESTRE, TextBox txtCodigo, Panel panUpAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Button btnGravar, MethodInvoker CamposEnable, TextBox txtDescri, string LojaLogada, ComboBox comStatus, ComboBox comTipo, TextBox txtTaxa)
        {
            string Mensagem_De_Erro = "";

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string NomeDaOpção = "Convênios e Cartões";

            #region Comandos
            if (StringComandoSELEÇÃO == "1")
            {
                StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabConve WHERE Status_COV <> 3 ORDER BY Sequen_COV";
                Mensagem_De_Erro = "Sem dados de " + NomeDaOpção + " para exibir";
            }
            if (StringComandoSELEÇÃO == "2")
            {
                string Num = txtCodigo.Text;
                if (txtCodigo.Text == string.Empty)
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabConve WHERE Status_COV <> 3 ORDER BY Sequen_COV";
                    Mensagem_De_Erro = "Não existe registro anterior no banco";
                }
                else
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabConve WHERE Sequen_COV < " + Num + " AND Status_COV <> 3 ORDER BY Sequen_COV DESC";
                    Mensagem_De_Erro = "Não existe registro anterior no banco";
                }
            }
            if (StringComandoSELEÇÃO == "3")
            {
                string Num = txtCodigo.Text;
                if (txtCodigo.Text == string.Empty)
                {
                    Num = "0";
                }
                StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabConve WHERE Sequen_COV > " + Num + " AND Status_COV <> 3";
                Mensagem_De_Erro = "Não existe próximo registro no banco";
            }
            if (StringComandoSELEÇÃO == "4")
            {
                StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabConve WHERE Sequen_COV >= 1  AND Status_COV <> 3 ORDER BY Sequen_COV DESC";
                Mensagem_De_Erro = "Sem dados de " + NomeDaOpção + " para exibir";
            }
            #endregion

            SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);

            try
            {
                SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    txtCodigo.Text = Dr["Sequen_COV"].ToString().PadLeft(6, '0');
                    txtDescri.Text = Dr["Descri_COV"].ToString();
                    #region comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_ROT"]);
                    try
                    {
                        comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_COV"]) - 1;
                    }
                    catch (Exception)
                    {
                        comStatus.SelectedIndex = 1;
                    }
                    #endregion
                    #region comTipo.SelectedIndex = Convert.ToInt32(Dr["Tipo01_COV"]);
                    try
                    {
                        comTipo.SelectedIndex = Convert.ToInt32(Dr["Tipo01_COV"]) - 1;
                    }
                    catch (Exception)
                    {
                        comTipo.SelectedIndex = -1;
                    }
                    #endregion
                    #region txtTaxa.Text = Convert.ToDecimal(Dr["Taxa01_COV"]).ToString("00.00");
                    if (Convert.ToDecimal(Dr["Taxa01_COV"]) <= 0)
                    {
                        txtTaxa.Text = string.Empty;
                    }
                    else
                    {
                        txtTaxa.Text = Convert.ToDecimal(Dr["Taxa01_COV"]).ToString("00.00");
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