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

namespace TabProgr
{
    internal class TabProgr_Pesquisa
    {
        //Preenche com os filtros caso esteja em branco
        public void Pesc_FILTROS(string LojaLogada, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, RadioButton rabAlfabetico, RadioButton rabNumerico, ComboBox comStatusPES)
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

            if (comStatusPES.SelectedIndex < 0)
            {
                comStatusPES.SelectedIndex = 0;
            }
        }

        //Faz a pesquisa a partir dos filtros
        public bool Pesc_EXECUTAR(DataGridView Dgv_Pesquisa, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, RadioButton rabAlfabetico, RadioButton rabNumerico, ComboBox comStatusPES, TextBox txtDescriPES, TextBox txtQtSelectPES, string LojaLogada)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            //CASO O USUÁRIO COMEÇE A PESQUISA E OS FILTROS ESTEJAM EM BRANCO
            //ESSE METODO MARCA ELES AUTOMATICAMENTE
            #region APLICA OS FILTROS CASO ESTEJAM EM BRANCO
            Pesc_FILTROS(LojaLogada, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comStatusPES);
            #endregion

            Dgv_Pesquisa.Rows.Clear();


            string NúmeroResults = "TOP " + nupQtResultados.Value.ToString();
            if (rabTodos.Checked == true)
            {
                NúmeroResults = "";
            }

            String Select_CMD = String.Format("SELECT " + NúmeroResults + " Sequen_PGR,Descri_PGR,Status_PGR,Modulo_PGR FROM TabProgr WHERE 1=1");

            if (!String.IsNullOrEmpty(txtDescriPES.Text))
                Select_CMD += " AND Descri_PGR LIKE '%' + @1 + '%'";


            if (comStatusPES.SelectedIndex > 0)
                Select_CMD += " AND Status_PGR = @Situação";


            if (rabAlfabetico.Checked == true)
                Select_CMD += " ORDER BY Descri_PGR";
            if (rabNumerico.Checked == true)
                Select_CMD += " ORDER BY Sequen_PGR";


            SqlCommand ComandoPESQ = new SqlCommand(Select_CMD, Conexão);
            ComandoPESQ.Parameters.Add("@Situação", SqlDbType.Int).Value = comStatusPES.SelectedIndex;
            ComandoPESQ.Parameters.Add("@1", SqlDbType.VarChar).Value = txtDescriPES.Text;

            try
            {
                SqlDataReader Dr = ComandoPESQ.ExecuteReader();
                while (Dr.Read())
                {
                    if (Dr.HasRows)
                    {
                        string Sequen_PGR = Dr["Sequen_PGR"].ToString().PadLeft(6, '0');
                        string Descri_PGR = Dr["Descri_PGR"].ToString();
                        string Status_PGR = Dr["Status_PGR"].ToString();
                        string Modulo_PGR = Dr["Modulo_PGR"].ToString();

                        #region TRATAMENTO PARA TEXTO STATUS
                        switch (Status_PGR)
                        {
                            case "1":
                                Status_PGR = "ATIVO";
                                break;
                            case "2":
                                Status_PGR = "INATIVO";
                                break;
                            case "3":
                                Status_PGR = "EXCLUIDO";
                                break;
                        }
                        #endregion
                        #region TRATAMENTO PARA TEXTO MODULO
                        switch (Modulo_PGR)
                        {
                            case "1":
                                Modulo_PGR = "FREE";
                                break;
                            case "2":
                                Modulo_PGR = "EXPRESS";
                                break;
                            case "3":
                                Modulo_PGR = "BUSINESS";
                                break;
                            case "4":
                                Modulo_PGR = "CONTROLE";
                                break;
                            case "5":
                                Modulo_PGR = "PRÓ";
                                break;
                        }
                        #endregion

                        Dgv_Pesquisa.Rows.Add(Sequen_PGR, Descri_PGR, Modulo_PGR, Status_PGR);
                    }
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Pesc_EXECUTAR()\n\nBLOCO = MÉTODO DA PESQUISA\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Pesc_EXECUTAR()\n\nBLOCO = MÉTODO DA PESQUISA\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            finally
            {
                Conexão.Close();
            }

            return false;
        }

    }
}
