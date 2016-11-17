using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TabProgr
{
    internal class TabProgr_ExecSETA
    {
        public void ExexSETAS(string StringComandoSELEÇÃO, TextBox txtCodigo, TextBox txtDescricao, TextBox txtDataCadastro, ComboBox comStatus, ComboBox comModulo, Button btnGravar, MethodInvoker CamposDisable)
        {
            string Mensagem_De_Erro = "";


            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string NomeDaOpção = "Programas";

            #region Comandos
            if (StringComandoSELEÇÃO == "1")
            {
                StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabProgr WHERE Status_PGR <> 3 ORDER BY Sequen_PGR";
                Mensagem_De_Erro = "Sem dados de " + NomeDaOpção + " para exibir";
            }
            if (StringComandoSELEÇÃO == "2")
            {
                string Num = txtCodigo.Text;
                if (txtCodigo.Text == string.Empty)
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabProgr WHERE Status_PGR <> 3 ORDER BY Sequen_PGR";
                    Mensagem_De_Erro = "Não existe registro anterior no banco";
                }
                else
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabProgr WHERE Sequen_PGR < " + Num + " AND Status_PGR <> 3 ORDER BY Sequen_PGR DESC";
                    Mensagem_De_Erro = "Não existe registro anterior no banco";
                }
            }
            if (StringComandoSELEÇÃO == "3")
            {
                string Num = txtCodigo.Text;
                if (txtCodigo.Text == string.Empty)
                {
                    Num = "1";
                }
                StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabProgr WHERE Sequen_PGR > " + Num + " AND Status_PGR <> 3";
                Mensagem_De_Erro = "Não existe próximo registro no banco";
            }
            if (StringComandoSELEÇÃO == "4")
            {
                StringComandoSELEÇÃO = "SELECT TOP 1 MAX(Sequen_PGR) as Sequencia, * FROM TabProgr WHERE Status_PGR <> 3 GROUP BY Sequen_PGR, Descri_PGR,DtCada_PGR,Status_PGR,Modulo_PGR,Tabela_PGR,Prefix_PGR ORDER BY Sequen_PGR DESC";
                Mensagem_De_Erro = "Sem dados de " + NomeDaOpção + " para exibir";
            }
            #endregion

            SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);

            try
            {
                SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    txtCodigo.Text = Dr["Sequen_PGR"].ToString();
                    txtDescricao.Text = Dr["Descri_PGR"].ToString();
                    DateTime DT = Convert.ToDateTime(txtDataCadastro.Text = Dr["DtCada_PGR"].ToString());
                    txtDataCadastro.Text = DT.ToString("dd/MM/yyyy");
                    comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_PGR"]);
                    comModulo.SelectedIndex = Convert.ToInt32(Dr["Modulo_PGR"]);
                    btnGravar.Enabled = false;
                    CamposDisable();
                }
                else
                {
                    btnGravar.Enabled = false;
                    CamposDisable();

                    MessageBox.Show(Mensagem_De_Erro, "Consulta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método ExexSETAS()\n\nBLOCO = CONSULTA DE DADOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método ExexSETAS()\n\nBLOCO = CONSULTA DE DADOS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }
    }
}
