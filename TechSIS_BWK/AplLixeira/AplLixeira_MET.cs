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

namespace AplLixeira
{
    internal class AplLixeira_MET
    {
        //DEFINE QUAL CAMPO PODE RECEBER APENAS NÚMEROS
        public void MET_ApenasNúmeros(KeyPressEventArgs KeyPress)
        {
            if (!Char.IsDigit(KeyPress.KeyChar) && KeyPress.KeyChar != (char)8)
            {
                KeyPress.Handled = true;
                return;
            }
        }

        //SELECIONA O PROGRAMA
        public void MET_SelecionaPrograma(TextBox txtCodigo, TextBox txtDescri, Control Retorno, DataGridView Dgv_Lixeira, Button btnEsvaziar)
        {
            txtCodigo.Text = txtCodigo.Text.PadRight(6, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string SelectCMD = "SELECT Descri_PGR,Tabela_PGR,Prefix_PGR FROM TabProgr WHERE Sequen_PGR = @Sequen_PGR";
            SqlCommand Comando = new SqlCommand(SelectCMD, Conexão);
            Comando.Parameters.Add("@Sequen_PGR", SqlDbType.VarChar).Value = txtCodigo.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    txtDescri.Text = Dr["Descri_PGR"].ToString();
                    string TABELA = Dr["Tabela_PGR"].ToString();
                    string PREFIX = Dr["Prefix_PGR"].ToString();
                    MET_SelecionaItemsTab(PREFIX, TABELA, Dgv_Lixeira, btnEsvaziar);
                    if (Dgv_Lixeira.Rows.Count == 0)
                    {
                        MessageBox.Show("Nenhum item para exclusão foi encontrado!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    txtDescri.Text = string.Empty;
                    MessageBox.Show("Programa inexistente no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException Ex)
            {
                txtDescri.Text = string.Empty;
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelecionaPrograma()\n\nBLOCO = AplLixeira_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                txtDescri.Text = string.Empty;
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelecionaPrograma()\n\nBLOCO = AplLixeira_MET\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Retorno.Focus(); txtCodigo.SelectAll();
                Conexão.Close();
            }
        }

        //SELECIONA OS ITEMS EXCLUIDOS
        public void MET_SelecionaItemsTab(string PREFIXO, string TABELA, DataGridView Dgv_Lixeira, Button btnEsvaziar)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string SelectCMD = "SELECT Sequen_" + PREFIXO + ", Descri_" + PREFIXO + " FROM " + TABELA + " WHERE Status_" + PREFIXO + " = 3";
            SqlCommand Comando = new SqlCommand(SelectCMD, Conexão);


            try
            {
                SqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    string CODIGO = Dr["Sequen_" + PREFIXO].ToString();
                    string DESCRI = Dr["Descri_" + PREFIXO].ToString();


                    Dgv_Lixeira.Rows.Add(CODIGO.PadLeft(10, '0'), TABELA, DESCRI);

                    if (Dgv_Lixeira.Rows.Count > 0)
                    {
                        btnEsvaziar.Enabled = true;
                    }
                }
            }
            catch (Exception)
            {
 
            }

        }
    }
}
