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

namespace TabPermi
{
    internal class TabPermi_Pesquisa
    {
        //Preenche com os filtros caso esteja em branco
        public void Pesc_FILTROS(string LojaLogada, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, RadioButton rabAlfabetico, RadioButton rabNumerico, TextBox txtPesUsuarCod, TextBox txtPesUsuarDesc, TextBox txtPesProgrCod, TextBox txtPesProgrDesc)
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

            

            if (txtPesUsuarDesc.Text == string.Empty)
            {
                txtPesUsuarCod.Text = string.Empty;
            }
            if (txtPesProgrDesc.Text == string.Empty)
            {
                txtPesProgrCod.Text = string.Empty;
            }
        }

        //Faz a pesquisa a partir dos filtros
        public void Pesc_EXECUTAR(DataGridView Dgv_Pesquisa, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, RadioButton rabAlfabetico, RadioButton rabNumerico, TextBox txtQtSelectPES, TextBox txtPesUsuarCod, TextBox txtPesUsuarDesc, TextBox txtPesProgrCod, TextBox txtPesProgrDesc, string LojaLogada, Button btnPesquisar)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();





            //CASO O USUÁRIO COMEÇE A PESQUISA E OS FILTROS ESTEJAM EM BRANCO
            //ESSE METODO MARCA ELES AUTOMATICAMENTE
            #region APLICA OS FILTROS CASO ESTEJAM EM BRANCO
            Pesc_FILTROS(LojaLogada, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, txtPesUsuarCod, txtPesUsuarDesc, txtPesProgrCod, txtPesProgrDesc);
            #endregion


            Dgv_Pesquisa.Rows.Clear();


            string NúmeroResults = "TOP " + nupQtResultados.Value.ToString();
            if (rabTodos.Checked == true)
            {
                NúmeroResults = "";
            }



            String Select_CMD = String.Format("SELECT " + NúmeroResults + " SeqUsu_PER,Apelid_USU,SeqPgr_PER,Descri_PGR,PerINC_PER,PerALT_PER,PerEXC_PER,PerCON_PER,PerABA_PER,PerAb1_PER,PerAb2_PER,PerAb3_PER,PerAb4_PER FROM TabPermi INNER JOIN TabProgr ON TabPermi.SeqPgr_PER = TabProgr.Sequen_PGR INNER JOIN TabUsuar ON TabPermi.SeqUsu_PER = TabUsuar.Sequen_USU WHERE 1=1");
            if (!String.IsNullOrEmpty(txtPesProgrCod.Text))
                Select_CMD += " AND SeqPgr_PER = " + txtPesProgrCod.Text;
            if (!String.IsNullOrEmpty(txtPesUsuarCod.Text))
                Select_CMD += " AND SeqUsu_PER = " + txtPesUsuarCod.Text;

            if (rabAlfabetico.Checked == true)
                Select_CMD += " ORDER BY Descri_PGR";
            if (rabNumerico.Checked == true)
                Select_CMD += " ORDER BY Sequen_PGR";


            SqlCommand ComandoPESQ = new SqlCommand(Select_CMD, Conexão);

            try
            {
                SqlDataReader Dr = ComandoPESQ.ExecuteReader();
                int i = 0;
                while (Dr.Read())
                {
                    if (Dr.HasRows)
                    {
                        string SeqUsu_PER = Dr["SeqUsu_PER"].ToString().PadLeft(6, '0');
                        string Apelid_USU = Dr["Apelid_USU"].ToString();
                        string SeqPgr_PER = Dr["SeqPgr_PER"].ToString().PadLeft(6, '0');
                        string Descri_PGR = Dr["Descri_PGR"].ToString();

                        string PerINC_PER = Dr["PerINC_PER"].ToString();
                        string PerALT_PER = Dr["PerALT_PER"].ToString();
                        string PerEXC_PER = Dr["PerEXC_PER"].ToString();
                        string PerCON_PER = Dr["PerCON_PER"].ToString();
                        string PerABA_PER = Dr["PerABA_PER"].ToString();

                        #region TRATAMENTO SIM OU NÃO
                        switch (PerINC_PER)
                        {
                            case "True":
                                PerINC_PER = "SIM";
                                break;
                            case "False":
                                PerINC_PER = "NÃO";
                                break;
                        }
                        switch (PerALT_PER)
                        {
                            case "True":
                                PerALT_PER = "SIM";
                                break;
                            case "False":
                                PerALT_PER = "NÃO";
                                break;
                        }
                        switch (PerEXC_PER)
                        {
                            case "True":
                                PerEXC_PER = "SIM";
                                break;
                            case "False":
                                PerEXC_PER = "NÃO";
                                break;
                        }
                        switch (PerCON_PER)
                        {
                            case "True":
                                PerCON_PER = "SIM";
                                break;
                            case "False":
                                PerCON_PER = "NÃO";
                                break;
                        }
                        switch (PerABA_PER)
                        {
                            case "True":
                                PerABA_PER = "SIM";
                                break;
                            case "False":
                                PerABA_PER = "NÃO";
                                break;
                        }
                        #endregion

                        Dgv_Pesquisa.Rows.Add(SeqUsu_PER, Apelid_USU, SeqPgr_PER, Descri_PGR, PerINC_PER, PerALT_PER, PerEXC_PER, PerCON_PER, PerABA_PER);

                        #region COLOR EM BLOQUEIA ABAS
                        if (Dgv_Pesquisa.Rows[i].Cells["dgvcPesAbas"].Value.ToString() == "NÃO")
                        {
                            Dgv_Pesquisa.Rows[i].Cells["dgvcPesAbas"].Style.BackColor = Color.White;
                        }
                        else
                        {
                            Dgv_Pesquisa.Rows[i].Cells["dgvcPesAbas"].Style.BackColor = Color.Gray;
                        }
                        #endregion

                        #region COLOR EM INCLUIR
                        if (Dgv_Pesquisa.Rows[i].Cells["dgvcPesINC"].Value.ToString() == "SIM")
                        {
                            Dgv_Pesquisa.Rows[i].Cells["dgvcPesINC"].Style.BackColor = Color.White;
                        }
                        else
                        {
                            Dgv_Pesquisa.Rows[i].Cells["dgvcPesINC"].Style.BackColor = Color.Red;
                        }
                        #endregion

                        #region COLOR EM ALTERAR
                        if (Dgv_Pesquisa.Rows[i].Cells["dgvcPesALT"].Value.ToString() == "SIM")
                        {
                            Dgv_Pesquisa.Rows[i].Cells["dgvcPesALT"].Style.BackColor = Color.White;
                        }
                        else
                        {
                            Dgv_Pesquisa.Rows[i].Cells["dgvcPesALT"].Style.BackColor = Color.Red;
                        }
                        #endregion

                        #region COLOR EM EXCLUIR
                        if (Dgv_Pesquisa.Rows[i].Cells["dgvcPesEXC"].Value.ToString() == "SIM")
                        {
                            Dgv_Pesquisa.Rows[i].Cells["dgvcPesEXC"].Style.BackColor = Color.White;
                        }
                        else
                        {
                            Dgv_Pesquisa.Rows[i].Cells["dgvcPesEXC"].Style.BackColor = Color.Red;
                        }
                        #endregion

                        #region COLOR EM CONSULTA
                        if (Dgv_Pesquisa.Rows[i].Cells["dgvcPesCON"].Value.ToString() == "SIM")
                        {
                            Dgv_Pesquisa.Rows[i].Cells["dgvcPesCON"].Style.BackColor = Color.White;
                        }
                        else
                        {
                            Dgv_Pesquisa.Rows[i].Cells["dgvcPesCON"].Style.BackColor = Color.Red;
                        }
                        #endregion

                        i++;
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

                btnPesquisar.Select();
            }
        }

        //Procura o usuário no Tab
        public void Pesc_SearchUSUAR(TextBox txtPesUsuarCod, TextBox txtPesUsuarDesc, GroupBox grbFiltros)
        {
            if (txtPesUsuarCod.Text != string.Empty)
            {
                txtPesUsuarCod.Text = txtPesUsuarCod.Text.PadLeft(6, '0');

                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string Search = "SELECT Apelid_USU FROM TabUsuar WHERE Sequen_USU = @Sequen";
                SqlCommand SearchComando = new SqlCommand(Search, Conexão);
                SearchComando.Parameters.Add("@Sequen", SqlDbType.Int).Value = txtPesUsuarCod.Text;

                try
                {
                    SqlDataReader Dr = SearchComando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        txtPesUsuarDesc.Text = Dr["Apelid_USU"].ToString();
                    }
                    else
                    {
                        txtPesUsuarDesc.Text = string.Empty;
                        grbFiltros.Focus();
                        txtPesUsuarCod.SelectAll();
                        MessageBox.Show("Usuário não foi encontrado no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Pesc_SearchUSUAR()\n\nBLOCO = MÉTODO DA PESQUISA\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Pesc_SearchUSUAR()\n\nBLOCO = MÉTODO DA PESQUISA\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //Procura o usuário no Tab
        public void Pesc_SearchPROGR(TextBox txtPesUsuarCod, TextBox txtPesUsuarDesc, GroupBox grbFiltros, TextBox txtPesProgrCod, TextBox txtPesProgrDesc)
        {
            if (txtPesProgrCod.Text != string.Empty)
            {
                txtPesProgrCod.Text = txtPesProgrCod.Text.PadLeft(6, '0');

                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string Search = "SELECT Descri_PGR FROM TabProgr WHERE Sequen_PGR = @Sequen";
                SqlCommand SearchComando = new SqlCommand(Search, Conexão);
                SearchComando.Parameters.Add("@Sequen", SqlDbType.Int).Value = txtPesProgrCod.Text;

                try
                {
                    SqlDataReader Dr = SearchComando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        txtPesProgrDesc.Text = Dr["Descri_PGR"].ToString();
                    }
                    else
                    {
                        txtPesProgrDesc.Text = string.Empty;
                        txtPesUsuarCod.Select();
                        txtPesProgrCod.SelectAll();
                        MessageBox.Show("Programa não foi encontrado no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Pesc_SearchPROGR()\n\nBLOCO = MÉTODO DA PESQUISA\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Pesc_SearchPROGR()\n\nBLOCO = MÉTODO DA PESQUISA\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //Obriga um usuário ou um programa
        public bool Pesc_ObrigUsuarOuProgr(TextBox txtPesUsuarCod, TextBox txtPesUsuarDesc, TextBox txtPesProgrCod, TextBox txtPesProgrDesc, TabControl TabControl)
        {
            if (txtPesProgrDesc.Text == string.Empty)
            {
                txtPesProgrCod.Text = string.Empty;
            }
            if (txtPesUsuarDesc.Text == string.Empty)
            {
                txtPesUsuarCod.Text = string.Empty;
            }


            if (txtPesProgrCod.Text == string.Empty && txtPesUsuarCod.Text == string.Empty && TabControl.SelectedIndex == 1)
            {
                MessageBox.Show("Informe um usuário ou um programa para a pesquisa", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPesUsuarCod.Select();
                return true;
            }
            return false;
        }
    }
}
