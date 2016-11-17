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

namespace TabUsuar
{
    internal class TabUsuar_Pesquisa
    {
        //Preenche com os filtros caso esteja em branco
        public void Pesc_FILTROS(string LojaLogada, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, RadioButton rabAlfabetico, RadioButton rabNumerico, ComboBox comPesSituacao, ComboBox comPesPermissao, TextBox txtPesEmpDesc, TextBox txtPesEmpCod)
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

            if (comPesSituacao.SelectedIndex < 0)
            {
                comPesSituacao.SelectedIndex = 0;
            }

            if (comPesPermissao.SelectedIndex < 0)
            {
                comPesPermissao.SelectedIndex = 0;
            }


            if (txtPesEmpDesc.Text == string.Empty)
            {
                txtPesEmpCod.Text = string.Empty;
            }
        }

        //Faz a pesquisa a partir dos filtros
        public void Pesc_EXECUTAR(DataGridView Dgv_Pesquisa, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, RadioButton rabAlfabetico, RadioButton rabNumerico, ComboBox comPesSituacao, ComboBox comPesPermissao, TextBox txtPesDescri, TextBox txtPesApelido, TextBox txtPesEmpCod, TextBox txtQtSelectPES, TextBox txtPesEmpDesc, string LojaLogada)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            //CASO O USUÁRIO COMEÇE A PESQUISA E OS FILTROS ESTEJAM EM BRANCO
            //ESSE METODO MARCA ELES AUTOMATICAMENTE
            #region APLICA OS FILTROS CASO ESTEJAM EM BRANCO
            Pesc_FILTROS(LojaLogada, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comPesSituacao, comPesPermissao, txtPesEmpDesc, txtPesEmpCod);
            #endregion

            Dgv_Pesquisa.Rows.Clear();


            string NúmeroResults = "TOP " + nupQtResultados.Value.ToString();
            if (rabTodos.Checked == true)
            {
                NúmeroResults = "";
            }



            String Select_CMD = String.Format("SELECT " + NúmeroResults + " Sequen_USU,Descri_USU,Apelid_USU,Tipo01_USU FROM TabUsuar WHERE 1=1");

            if (!String.IsNullOrEmpty(txtPesDescri.Text))
                Select_CMD += " AND Descri_USU LIKE '%' + @1 + '%'";

            if (!String.IsNullOrEmpty(txtPesApelido.Text))
                Select_CMD += " AND Apelid_USU LIKE '%' + @2 + '%'";

            if (!String.IsNullOrEmpty(txtPesEmpCod.Text))
                Select_CMD += " AND CodLoj_USU = " + txtPesEmpCod.Text;


            if (comPesSituacao.SelectedIndex > 0)
                Select_CMD += " AND Status_USU = " + comPesSituacao.SelectedIndex;
            if (comPesPermissao.SelectedIndex > 0)
                Select_CMD += " AND Tipo01_USU = " + comPesPermissao.SelectedIndex;


            if (rabAlfabetico.Checked == true)
                Select_CMD += " ORDER BY Descri_USU";
            if (rabNumerico.Checked == true)
                Select_CMD += " ORDER BY Sequen_USU";


            SqlCommand ComandoPESQ = new SqlCommand(Select_CMD, Conexão);
            ComandoPESQ.Parameters.Add("@1", SqlDbType.VarChar).Value = txtPesDescri.Text;
            ComandoPESQ.Parameters.Add("@2", SqlDbType.VarChar).Value = txtPesApelido.Text;

            try
            {
                SqlDataReader Dr = ComandoPESQ.ExecuteReader();
                while (Dr.Read())
                {
                    if (Dr.HasRows)
                    {
                        string Sequen_USU = Dr["Sequen_USU"].ToString().PadLeft(6, '0');
                        string Descri_USU = Dr["Descri_USU"].ToString();
                        string Apelid_USU = Dr["Apelid_USU"].ToString();
                        string Tipo01_USU = Dr["Tipo01_USU"].ToString();

                        #region TRATAMENTO PARA TEXTO STATUS
                        switch (Tipo01_USU)
                        {
                            case "1":
                                Tipo01_USU = "ACESSO TOTAL";
                                break;
                            case "2":
                                Tipo01_USU = "ACESSO LIMITADO";
                                break;
                            case "3":
                                Tipo01_USU = "ACESSO RESTRITO";
                                break;
                        }
                        #endregion

                        Dgv_Pesquisa.Rows.Add(Sequen_USU, Descri_USU, Apelid_USU, Tipo01_USU);
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
