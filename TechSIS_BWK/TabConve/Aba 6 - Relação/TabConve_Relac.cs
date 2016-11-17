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

namespace TabConve
{
    internal class TabConve_Relac
    {
        //POPULA O DGV DA RELAÇÃO
        public void Rel_PopulaDataGrid(DataGridView Dgv_Relacao, TextBox txtCodigoAb6)
        {
            txtCodigoAb6.Text = txtCodigoAb6.Text.PadLeft(6, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            Dgv_Relacao.Rows.Clear();

            string strComando = "";

            if (Convert.ToInt32(txtCodigoAb6.Text) == 0)
            {
                strComando = "SELECT Sequen_CLI,Descri_CLI,ConSeq_CLI,Descri_COV FROM TabClien INNER JOIN TabConve ON TabClien.ConSeq_CLI = TabConve.Sequen_COV";
            }
            else
            {
                strComando = "SELECT Sequen_CLI,Descri_CLI,ConSeq_CLI,Descri_COV FROM TabClien INNER JOIN TabConve ON TabClien.ConSeq_CLI = TabConve.Sequen_COV WHERE ConSeq_CLI = @ConSeq_CLI";
            }

            SqlCommand Comando = new SqlCommand(strComando, Conexão);

            if (txtCodigoAb6.Text != string.Empty)
            {
                Comando.Parameters.Add("@ConSeq_CLI", SqlDbType.Int).Value = txtCodigoAb6.Text;
            }

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    if (Dr.HasRows)
                    {
                        string Sequen_CLI = Dr["Sequen_CLI"].ToString().PadLeft(6, '0');
                        string Descri_CLI = Dr["Descri_CLI"].ToString();
                        string ConSeq_CLI = Dr["ConSeq_CLI"].ToString().PadLeft(6, '0');
                        string Descri_COV = Dr["Descri_COV"].ToString();


                        Dgv_Relacao.Rows.Add(Sequen_CLI, Descri_CLI, ConSeq_CLI, Descri_COV);

                    }
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Rel_PopulaDataGrid()\n\nBLOCO = MÉTODO DA TabConve_Relac\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Rel_PopulaDataGrid()\n\nBLOCO = MÉTODO DA TabConve_Relac\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
                txtCodigoAb6.Select();
                txtCodigoAb6.SelectAll();
            }
        }
    }
}
