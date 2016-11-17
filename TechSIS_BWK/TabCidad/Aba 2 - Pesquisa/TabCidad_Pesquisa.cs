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

namespace TabCidad
{
    internal class TabCidad_Pesquisa
    {
        //Preenche com os filtros caso esteja em branco
        public void Pesc_FILTROS(string LojaLogada, DataGridView Dgv_Pesquisa, RadioButton rabTodos, NumericUpDown nupQtResultados, TextBox txtPesDescri, ComboBox comPesStatus, RadioButton rabAlfabetico, RadioButton rabNumerico, RadioButton rabTOP)
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

        public void Pesc_EXECUTAR(DataGridView Dgv_Pesquisa, RadioButton rabTodos, NumericUpDown nupQtResultados, TextBox txtPesDescri, ComboBox comPesStatus, RadioButton rabAlfabetico, RadioButton rabNumerico, RadioButton rabTOP, string LojaLogada)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            //CASO O USUÁRIO COMEÇE A PESQUISA E OS FILTROS ESTEJAM EM BRANCO
            //ESSE METODO MARCA ELES AUTOMATICAMENTE
            #region Preenche os FILTROS
            Pesc_FILTROS(LojaLogada, Dgv_Pesquisa, rabTodos, nupQtResultados, txtPesDescri, comPesStatus, rabAlfabetico, rabNumerico, rabTOP);
            #endregion


            Dgv_Pesquisa.Rows.Clear();

            string NúmeroResults = "TOP " + nupQtResultados.Value.ToString();
            if (rabTodos.Checked == true)
            {
                NúmeroResults = "";
            }

            String Select_CMD = String.Format("SELECT " + NúmeroResults + " Sequen_CID,Descri_CID,UfFede_CID,IbgeMu_CID FROM TabCidad WHERE 1=1");
            if (!String.IsNullOrEmpty(txtPesDescri.Text))
                Select_CMD += " AND Descri_CID LIKE '%' + @1 + '%'";
            if (comPesStatus.SelectedIndex > 0)
                Select_CMD += " AND Status_CID = " + comPesStatus.SelectedIndex;


            if (rabAlfabetico.Checked == true)
                Select_CMD += " ORDER BY Descri_CID";
            if (rabNumerico.Checked == true)
                Select_CMD += " ORDER BY Sequen_CID";

            SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);
            Comando.Parameters.Add("@1", SqlDbType.VarChar).Value = txtPesDescri.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    if (Dr.HasRows)
                    {
                        string Sequen_CID = Dr["Sequen_CID"].ToString().PadLeft(6, '0');
                        string Descri_CID = Dr["Descri_CID"].ToString();
                        string UfDesc_CID = Dr["UfFede_CID"].ToString();
                        string IbgeMu_CID = Dr["IbgeMu_CID"].ToString();

                        Dgv_Pesquisa.Rows.Add(Sequen_CID, Descri_CID, UfDesc_CID, IbgeMu_CID);

                        txtPesDescri.Select();
                    }
                    else
                    {
                        txtPesDescri.Select();
                    }
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Pesc_EXECUTAR()\n\nBLOCO = TabCidad_Pesquisa\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Pesc_EXECUTAR()\n\nBLOCO = TabCidad_Pesquisa\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }
    }
}
