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

namespace TabSetor
{
    internal class TabSetor_Pesquisa
    {
        //PREENCHE COM OS FILTROS EM BRANCO
        public void Pesc_FILTROS(string LojaLogada, ComboBox comPesStatus, TextBox txtPesDescri, RadioButton rabNumerico, RadioButton rabAlfabetico, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados)
        {
            string QuantidadeResultadosPadrao = "";

            //Captura a quantidade de resultado padrão na configuração geral do sistema
            #region CAPTURA A QUANTIDADE DE RESULTADOS NA CONFIGURAÇÃO DO SISTEMA
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringCaptura = "SELECT QtPesq_CON FROM TabConfi WHERE SeqLoj_CON = @Sequen";
            SqlCommand ComandoCaptura = new SqlCommand(StringCaptura, Conexão);

            ComandoCaptura.Parameters.Add("@Sequen", SqlDbType.Int).Value = LojaLogada;

            try
            {
                SqlDataReader Dr = ComandoCaptura.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    QuantidadeResultadosPadrao = Dr["QtPesq_CON"].ToString();
                }
                else
                {
                    QuantidadeResultadosPadrao = "20";
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Pesc_FILTROS()\n\nBLOCO = CAPTURA A QUANTIDADE DE RESULTADOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Pesc_FILTROS()\n\nBLOCO = CAPTURA A QUANTIDADE DE RESULTADOS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
            #endregion

            if (rabAlfabetico.Checked == false && rabNumerico.Checked == false)
            {
                rabNumerico.Checked = true;
            }

            if (rabTodos.Checked == false && rabTOP.Checked == false)
            {
                rabTOP.Checked = true;
                nupQtResultados.Value = Convert.ToDecimal(QuantidadeResultadosPadrao);
            }

            if (rabTOP.Checked == true && nupQtResultados.Value == 0)
            {
                nupQtResultados.Value = Convert.ToDecimal(QuantidadeResultadosPadrao);
            }

            if (comPesStatus.SelectedIndex < 0)
            {
                comPesStatus.SelectedIndex = 0;
            }
        }

        //FAZ A PESQUISA A PARTIR DOS FILTROS
        public void Pesc_EXECUTAR(DataGridView Dgv_Pesquisa, string LojaLogada, ComboBox comPesStatus, TextBox txtPesDescri, RadioButton rabNumerico, RadioButton rabAlfabetico, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            //CASO O USUÁRIO COMEÇE A PESQUISA E OS FILTROS ESTEJAM EM BRANCO
            //ESSE METODO MARCA ELES AUTOMATICAMENTE
            #region APLICA OS FILTROS CASO ESTEJAM EM BRANCO
            Pesc_FILTROS(LojaLogada, comPesStatus, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
            #endregion


            Dgv_Pesquisa.Rows.Clear();


            string NúmeroResults = "TOP " + nupQtResultados.Value.ToString();
            if (rabTodos.Checked == true)
            {
                NúmeroResults = "";
            }

            String Select_CMD = String.Format("SELECT " + NúmeroResults + " Sequen_SET,Descri_SET,Status_SET FROM TabSetor WHERE 1=1");

            if (!String.IsNullOrEmpty(txtPesDescri.Text))
                Select_CMD += " AND Descri_SET LIKE '%' + @1 + '%'";


            if (comPesStatus.SelectedIndex > 0)
                Select_CMD += " AND Status_SET = " + comPesStatus.SelectedIndex;


            if (rabAlfabetico.Checked == true)
                Select_CMD += " ORDER BY Descri_SET";
            if (rabNumerico.Checked == true)
                Select_CMD += " ORDER BY Sequen_SET";

            SqlCommand ComandoPESQ = new SqlCommand(Select_CMD, Conexão);
            ComandoPESQ.Parameters.Add("@1", SqlDbType.VarChar).Value = txtPesDescri.Text;

            try
            {
                SqlDataReader Dr = ComandoPESQ.ExecuteReader();
                while (Dr.Read())
                {
                    if (Dr.HasRows)
                    {
                        string Sequen_ROT = Dr["Sequen_SET"].ToString().PadLeft(7, '0');
                        string Descri_ROT = Dr["Descri_SET"].ToString();

                        string Status_ROT = Dr["Status_SET"].ToString();

                        #region TRATAMENTO ROTAS
                        if (Convert.ToInt32(Status_ROT) == 1)
                        {
                            Status_ROT = "ATIVA";
                        }
                        else if (Convert.ToInt32(Status_ROT) == 2)
                        {
                            Status_ROT = "INATIVA";
                        }
                        else if (Convert.ToInt32(Status_ROT) == 3)
                        {
                            Status_ROT = "EXCLUIDA";
                        }
                        else
                        {
                            Status_ROT = "ERRO.";
                        }
                        #endregion

                        Dgv_Pesquisa.Rows.Add(Sequen_ROT, Descri_ROT, Status_ROT);

                    }
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Pesc_EXECUTAR()\n\nBLOCO = MÉTODO DA PESQUISA\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Pesc_EXECUTAR()\n\nBLOCO = MÉTODO DA PESQUISA\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }
    }
}
