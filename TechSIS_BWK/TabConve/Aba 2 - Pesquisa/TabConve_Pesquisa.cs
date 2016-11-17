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
    internal class TabConve_Pesquisa
    {
        //PREENCHE COM OS FILTROS EM BRANCO
        public void Pesc_FILTROS(string LojaLogada, ComboBox comPesTipo, TextBox txtPesDescri, RadioButton rabNumerico, RadioButton rabAlfabetico, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados)
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

            if (comPesTipo.SelectedIndex < 0)
            {
                comPesTipo.SelectedIndex = 0;
            }
        }

        //FAZ A PESQUISA A PARTIR DOS FILTROS
        public void Pesc_EXECUTAR(DataGridView Dgv_Pesquisa, string LojaLogada, ComboBox comPesTipo, TextBox txtPesDescri, RadioButton rabNumerico, RadioButton rabAlfabetico, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            //CASO O USUÁRIO COMEÇE A PESQUISA E OS FILTROS ESTEJAM EM BRANCO
            //ESSE METODO MARCA ELES AUTOMATICAMENTE
            #region APLICA OS FILTROS CASO ESTEJAM EM BRANCO
            Pesc_FILTROS(LojaLogada, comPesTipo, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
            #endregion


            Dgv_Pesquisa.Rows.Clear();


            string NúmeroResults = "TOP " + nupQtResultados.Value.ToString();
            if (rabTodos.Checked == true)
            {
                NúmeroResults = "";
            }

            String Select_CMD = String.Format("SELECT " + NúmeroResults + " Sequen_COV,Descri_COV,Tipo01_COV FROM TabConve WHERE 1=1");

            if (!String.IsNullOrEmpty(txtPesDescri.Text))
                Select_CMD += " AND Descri_COV LIKE '%' + @1 + '%'";


            if (comPesTipo.SelectedIndex > 0 && comPesTipo.SelectedIndex < 4)
                Select_CMD += " AND Tipo01_COV = " + comPesTipo.SelectedIndex;
            if (comPesTipo.SelectedIndex == 4)
                Select_CMD += " AND Tipo01_COV <> 1";



            if (rabAlfabetico.Checked == true)
                Select_CMD += " ORDER BY Descri_COV";
            if (rabNumerico.Checked == true)
                Select_CMD += " ORDER BY Sequen_COV";

            SqlCommand ComandoPESQ = new SqlCommand(Select_CMD, Conexão);
            ComandoPESQ.Parameters.Add("@1", SqlDbType.VarChar).Value = txtPesDescri.Text;

            try
            {
                SqlDataReader Dr = ComandoPESQ.ExecuteReader();
                while (Dr.Read())
                {
                    if (Dr.HasRows)
                    {
                        string Sequen_COV = Dr["Sequen_COV"].ToString().PadLeft(6, '0');
                        string Descri_COV = Dr["Descri_COV"].ToString();

                        string Tipo01_COV = Dr["Tipo01_COV"].ToString();

                        #region TRATAMENTO TIPOS
                        if (Convert.ToInt32(Tipo01_COV) == 1)
                        {
                            Tipo01_COV = "CONVÊNIO";
                        }
                        else if (Convert.ToInt32(Tipo01_COV) == 2)
                        {
                            Tipo01_COV = "CARTÃO CRÉ";
                        }
                        else if (Convert.ToInt32(Tipo01_COV) == 3)
                        {
                            Tipo01_COV = "CARTÃO DÉB";
                        }
                        else
                        {
                            Tipo01_COV = "ERRO.";
                        }
                        #endregion

                        Dgv_Pesquisa.Rows.Add(Sequen_COV, Descri_COV, Tipo01_COV);

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
