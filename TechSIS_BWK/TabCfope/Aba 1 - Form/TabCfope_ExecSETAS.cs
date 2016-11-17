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
    internal class TabCfope_ExecSETAS
    {
        public void ExecSETAS(string StringComandoSELEÇÃO, TextBox txtMESTRE, TextBox txtCodigo, Panel panUpAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Button btnGravar, MethodInvoker CamposEnable, TextBox txtDescri, string LojaLogada, TextBox txtIndCod, TextBox txtIndDesc, TextBox txtComeCod, TextBox txtComeDesc, ComboBox comLocalizacao)
        {
            string Mensagem_De_Erro = "";

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string NomeDaOpção = "CFOPs";

            #region Comandos
            if (StringComandoSELEÇÃO == "1")
            {
                StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabCfope WHERE Sequen_CFO >= 1 ORDER BY Sequen_CFO";
                Mensagem_De_Erro = "Sem dados de " + NomeDaOpção + " para exibir";
            }
            if (StringComandoSELEÇÃO == "2")
            {
                string Num = txtCodigo.Text;
                if (txtCodigo.Text == string.Empty)
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabCfope WHERE Sequen_CFO >= 1 ORDER BY Sequen_CFO";
                    Mensagem_De_Erro = "Não existe registro anterior no banco";
                }
                else
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabCfope WHERE Sequen_CFO < " + Num + " ORDER BY Sequen_CFO DESC";
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
                StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabCfope WHERE Sequen_CFO > " + Num;
                Mensagem_De_Erro = "Não existe próximo registro no banco";
            }
            if (StringComandoSELEÇÃO == "4")
            {
                StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabCfope WHERE Sequen_CFO >= 1 ORDER BY Sequen_CFO DESC";
                Mensagem_De_Erro = "Sem dados de " + NomeDaOpção + " para exibir";
            }
            #endregion

            SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);

            try
            {
                SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    txtCodigo.Text = Dr["Sequen_CFO"].ToString().PadLeft(4, '0');
                    txtDescri.Text = Dr["Descri_CFO"].ToString();
                    #region txtIndCod.Text = Dr["EntInd_CFO"].ToString().PadLeft(4, '0');
                    if (Convert.ToInt32(Dr["EntInd_CFO"]) <= 0)
                    {
                        txtIndCod.Text = string.Empty;
                    }
                    else
                    {
                        txtIndCod.Text = Dr["EntInd_CFO"].ToString().PadLeft(4, '0');
                    }
                    #endregion
                    #region txtIndCod.Text = Dr["EntInd_CFO"].ToString().PadLeft(4, '0');
                    if (Convert.ToInt32(Dr["EntCom_CFO"]) <= 0)
                    {
                        txtComeCod.Text = string.Empty;
                    }
                    else
                    {
                        txtComeCod.Text = Dr["EntCom_CFO"].ToString().PadLeft(4, '0');
                    }
                    #endregion
                    #region comLocalizacao.SelectedIndex = Convert.ToInt32(Dr["DenFor_CFO"]);
                    try
                    {
                        comLocalizacao.SelectedIndex = Convert.ToInt32(Dr["DenFor_CFO"]);
                    }
                    catch (Exception)
                    {
                        comLocalizacao.SelectedIndex = -1;
                    }
                    #endregion

                    TabCfope_MET MET = new TabCfope_MET();
                    MET.MET_SelectionaCFOPCorres(txtIndCod, txtIndDesc, txtComeCod, txtComeDesc);
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
