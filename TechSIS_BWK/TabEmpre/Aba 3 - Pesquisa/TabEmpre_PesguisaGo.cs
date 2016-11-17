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

namespace TabEmpre
{
    public class TabEmpre_PesguisaGo
    {
        public void PesquisaGo(DataGridView Dgv_Pesquisa, RadioButton rabAlfabetico, RadioButton rabNumerico, RadioButton rabTodos, RadioButton rabTOP, ComboBox comSituacaoPES, NumericUpDown nupQtResultados, TextBox txtRazaoPES, TextBox txtFantasiaPES, ComboBox comAtividadePES, ComboBox comModuloPES)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            #region TRATAMENTOS
            if (rabAlfabetico.Checked == false && rabNumerico.Checked == false)
            {
                rabAlfabetico.Checked = true;
            }
            if (rabTodos.Checked == false && rabTOP.Checked == false)
            {
                rabTodos.Checked = true;
            }
            if (comSituacaoPES.SelectedIndex < 0)
            {
                comSituacaoPES.SelectedIndex = 3;
            }
            if (comAtividadePES.SelectedIndex < 0)
            {
                comAtividadePES.SelectedIndex = 7;
            }
            if (comModuloPES.SelectedIndex < 0)
            {
                comModuloPES.SelectedIndex = 5;
            }
            if (nupQtResultados.Value == 0 && nupQtResultados.Enabled == true)
            {
                nupQtResultados.Value = 20;
            }
            #endregion


            Dgv_Pesquisa.Rows.Clear();

            string NúmeroResults = "TOP " + nupQtResultados.Value.ToString();
            if (rabTodos.Checked == true)
            {
                NúmeroResults = "";
            }

            String Select_CMD = String.Format("SELECT " + NúmeroResults + " Sequen_EMP,Descri_EMP,Fantas_EMP,CpfCnp_EMP FROM TabEmpre WHERE 1=1");
            if (!String.IsNullOrEmpty(txtRazaoPES.Text))
                Select_CMD += " AND Descri_EMP LIKE '%" + txtRazaoPES.Text + "%'";
            if (!String.IsNullOrEmpty(txtFantasiaPES.Text))
                Select_CMD += " AND Fantas_EMP LIKE '%" + txtFantasiaPES.Text + "%'";

            if (comSituacaoPES.SelectedIndex < 3)
                Select_CMD += " AND SitEmp_EMP = " + comSituacaoPES.SelectedIndex;
            if (comAtividadePES.SelectedIndex < 7)
                Select_CMD += " AND Ativid_EMP = " + comAtividadePES.SelectedIndex;
            if (comModuloPES.SelectedIndex < 5)
                Select_CMD += " AND ModSof_EMP = " + comModuloPES.SelectedIndex;


            if (rabAlfabetico.Checked == true)
                Select_CMD += " ORDER BY Descri_EMP";
            if (rabNumerico.Checked == true)
                Select_CMD += " ORDER BY Sequen_EMP";

            SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    if (Dr.HasRows)
                    {
                        string Sequen_EMP = Dr["Sequen_EMP"].ToString().PadLeft(6, '0');
                        string Descri_EMP = Dr["Descri_EMP"].ToString();
                        string Fantas_EMP = Dr["Fantas_EMP"].ToString();
                        string CpfCnp_EMP = Dr["CpfCnp_EMP"].ToString();

                        Dgv_Pesquisa.Rows.Add(Sequen_EMP, Descri_EMP, Fantas_EMP, CpfCnp_EMP);
                    }
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("Erro ao tentar realizar a pesquisa. (SQLErro.: " + Ex.Number + ")\n\n" + Ex.Message, "Verifique erro ocorrido!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Erro ao tentar realizar a pesquisa. (SQLErro.: " + Ex.Message + ")", "Verifique erro ocorrido!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
