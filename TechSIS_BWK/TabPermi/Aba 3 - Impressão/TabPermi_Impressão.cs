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
    internal class TabPermi_Impressão
    {
        public bool CamposObrig(RadioButton rabRPV, RadioButton rabWORD, RadioButton rabEXCEL, RadioButton rabTXT, RadioButton rabOrdemNumerica, RadioButton rabOrdemAlfabetica, TextBox txtImpUsuarCod, TextBox txtImpUsuarDesc, TextBox txtImpProgrCod, TextBox txtImpProgrDesc)
        {
            if (rabRPV.Checked == false && rabWORD.Checked == false && rabEXCEL.Checked == false && rabTXT.Checked == false)
            {
                rabRPV.Checked = true;
            }

            
            if (rabOrdemAlfabetica.Checked == false && rabOrdemNumerica.Checked == false)
            {
                rabOrdemNumerica.Checked = true;
            }



            if (txtImpUsuarDesc.Text == string.Empty)
            {
                txtImpUsuarCod.Text = string.Empty;
            }
            if (txtImpProgrDesc.Text == string.Empty)
            {
                txtImpProgrCod.Text = string.Empty;
            }

            if (txtImpUsuarCod.Text == string.Empty && txtImpProgrCod.Text == string.Empty)
            {
                MessageBox.Show("Informe um usuário ou um programa para a impressão", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtImpUsuarCod.Select();
                return true;
            }
            return false;
        }



        //Procura o usuário no Tab
        public void Imp_SearchUSUAR(TextBox txtImpUsuarCod, TextBox txtImpUsuarDesc, GroupBox grbInformacoesImpre)
        {
            if (txtImpUsuarCod.Text != string.Empty)
            {
                txtImpUsuarCod.Text = txtImpUsuarCod.Text.PadLeft(6, '0');

                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string Search = "SELECT Apelid_USU FROM TabUsuar WHERE Sequen_USU = @Sequen";
                SqlCommand SearchComando = new SqlCommand(Search, Conexão);
                SearchComando.Parameters.Add("@Sequen", SqlDbType.Int).Value = txtImpUsuarCod.Text;

                try
                {
                    SqlDataReader Dr = SearchComando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        txtImpUsuarDesc.Text = Dr["Apelid_USU"].ToString();
                    }
                    else
                    {
                        txtImpUsuarDesc.Text = string.Empty;
                        grbInformacoesImpre.Focus();
                        txtImpUsuarCod.SelectAll();
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
        public void Imp_SearchPROGR(TextBox txtImpUsuarCod, TextBox txtImpUsuarDesc, GroupBox grbInformacoesImpre, TextBox txtImpProgrCod, TextBox txtImpProgrDesc)
        {
            if (txtImpProgrCod.Text != string.Empty)
            {
                txtImpProgrCod.Text = txtImpProgrCod.Text.PadLeft(6, '0');

                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string Search = "SELECT Descri_PGR FROM TabProgr WHERE Sequen_PGR = @Sequen";
                SqlCommand SearchComando = new SqlCommand(Search, Conexão);
                SearchComando.Parameters.Add("@Sequen", SqlDbType.Int).Value = txtImpProgrCod.Text;

                try
                {
                    SqlDataReader Dr = SearchComando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        txtImpProgrDesc.Text = Dr["Descri_PGR"].ToString();
                    }
                    else
                    {
                        txtImpProgrDesc.Text = string.Empty;
                        txtImpUsuarCod.Select();
                        txtImpProgrCod.SelectAll();
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
    }
}
