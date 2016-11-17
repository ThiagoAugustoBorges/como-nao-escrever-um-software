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

namespace TabClien
{
    internal class TabClien_Pesquisa
    {
        #region FORMATAR CPF.CNPJ
        public static string FormatarCpfCnpj(string strCpfCnpj)
        {
            if (strCpfCnpj.Length <= 11)
            {
                MaskedTextProvider mtpCpf = new MaskedTextProvider(@"000\.000\.000-00");
                mtpCpf.Set(ZerosEsquerda(strCpfCnpj, 11));
                return mtpCpf.ToString();
            }
            else
            {
                MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");
                mtpCnpj.Set(ZerosEsquerda(strCpfCnpj, 11));
                return mtpCnpj.ToString();
            }
        }
        public static string ZerosEsquerda(string strString, int intTamanho)
        {
            string strResult = "";
            for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
            {
                strResult += "0";
            }
            return strResult + strString;
        }
        #endregion

        //PREENCHE COM OS FILTROS EM BRANCO
        public void Pesc_FILTROS(string LojaLogada, ComboBox comPesSituacao, ComboBox comPesCategoria, ComboBox comPesQtAoCredito, TextBox txtPesEmpCod, TextBox txtPesEmpDesc, TextBox txtPesVendCod, TextBox txtPesVendDesc, TextBox txtPesCidadeCod, TextBox txtPesCidadeDesc, RadioButton rabNumerico, RadioButton rabAlfabetico, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados)
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


            if (txtPesEmpDesc.Text == string.Empty)
            {
                txtPesEmpCod.Text = string.Empty;
            }

            if (txtPesVendDesc.Text == string.Empty)
            {
                txtPesVendCod.Text = string.Empty;
            }

            if (txtPesCidadeDesc.Text == string.Empty)
            {
                txtPesCidadeCod.Text = string.Empty;
            }


            if (comPesSituacao.SelectedIndex < 0)
            {
                comPesSituacao.SelectedIndex = 0;
            }

            if (comPesCategoria.SelectedIndex < 0)
            {
                comPesCategoria.SelectedIndex = 6;
            }

            if (comPesQtAoCredito.SelectedIndex < 0)
            {
                comPesQtAoCredito.SelectedIndex = 2;
            }
        }

        //FAZ A PESQUISA A PARTIR DOS FILTROS
        public void Pesc_EXECUTAR(string LojaLogada, RadioButton rabNumerico, RadioButton rabAlfabetico, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, TextBox txtPesDescri, TextBox txtPesApelido, TextBox txtPesEmpCod, TextBox txtPesEmpDesc, TextBox txtPesVendCod, TextBox txtPesVendDesc, MaskedTextBox mtbPesCpfCnpj, TextBox txtPesPrincipal, TextBox txtPesRota, TextBox txtPesConvenio, TextBox txtPesCidadeCod, TextBox txtPesCidadeDesc, ComboBox comPesSituacao, ComboBox comPesCategoria, ComboBox comPesQtAoCredito, CheckBox cheOrdemAlfDown, DataGridView Dgv_Pesquisa)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            //CASO O USUÁRIO COMEÇE A PESQUISA E OS FILTROS ESTEJAM EM BRANCO
            //ESSE METODO MARCA ELES AUTOMATICAMENTE
            #region APLICA OS FILTROS CASO ESTEJAM EM BRANCO
            Pesc_FILTROS(LojaLogada, comPesSituacao, comPesCategoria, comPesQtAoCredito, txtPesEmpCod, txtPesEmpDesc, txtPesVendCod, txtPesVendDesc, txtPesCidadeCod, txtPesCidadeDesc, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
            #endregion


            Dgv_Pesquisa.Rows.Clear();

            string NúmeroResults = "TOP " + nupQtResultados.Value.ToString();
            if (rabTodos.Checked == true)
            {
                NúmeroResults = "";
            }


            String Select_CMD = String.Format("SELECT " + NúmeroResults + " Sequen_CLI,Descri_CLI,CpfCnp_CLI,Fantas_CLI,EmpSeq_CLI,Descri_EMP FROM TabClien INNER JOIN TabEmpre ON TabClien.EmpSeq_CLI = TabEmpre.Sequen_EMP WHERE 1=1");

            if (!String.IsNullOrEmpty(txtPesDescri.Text))
                Select_CMD += " AND Descri_CLI LIKE '%' + @1 + '%'";

            if (!String.IsNullOrEmpty(txtPesApelido.Text))
                Select_CMD += " AND Fantas_CLI LIKE '%' + @2 + '%'";

            if (!String.IsNullOrEmpty(txtPesEmpCod.Text))
                Select_CMD += " AND EmpSeq_CLI = " + txtPesEmpCod.Text;

            if (!String.IsNullOrEmpty(txtPesVendCod.Text))
                Select_CMD += " AND VenSeq_CLI = " + txtPesVendCod.Text;

            if (!String.IsNullOrEmpty(txtPesCidadeCod.Text))
                Select_CMD += " AND EndCi1_CLI = " + txtPesCidadeCod.Text;

            if (!String.IsNullOrEmpty(txtPesPrincipal.Text))
                Select_CMD += " AND SeqPri_CLI = " + txtPesPrincipal.Text;

            if (!String.IsNullOrEmpty(txtPesRota.Text))
                Select_CMD += " AND RotSeq_CLI = " + txtPesRota.Text;

            if (!String.IsNullOrEmpty(txtPesConvenio.Text))
                Select_CMD += " AND ConSeq_CLI = " + txtPesConvenio.Text;

            if (!String.IsNullOrEmpty(mtbPesCpfCnpj.Text))
                Select_CMD += " AND CpfCnp_CLI = '" + mtbPesCpfCnpj.Text + "'";



            if (comPesSituacao.SelectedIndex > 0)
                Select_CMD += " AND Status_CLI = " + comPesSituacao.SelectedIndex;
            if (comPesCategoria.SelectedIndex < 6)
                Select_CMD += " AND Catego_CLI = " + comPesCategoria.SelectedIndex;
            if (comPesQtAoCredito.SelectedIndex < 2)
                Select_CMD += " AND SitCre_CLI = " + comPesQtAoCredito.SelectedIndex;





            if (cheOrdemAlfDown.Checked == true)
            {
                if (rabAlfabetico.Checked == true)
                    Select_CMD += " ORDER BY Fantas_CLI";
            }
            else
            {
                if (rabAlfabetico.Checked == true)
                    Select_CMD += " ORDER BY Descri_CLI";
            }
            if (rabNumerico.Checked == true)
                Select_CMD += " ORDER BY Sequen_CLI";




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
                        string Sequen_CLI = Dr["Sequen_CLI"].ToString().PadLeft(6, '0');
                        string Descri_CLI = Dr["Descri_CLI"].ToString();
                        string CpfCnp_CLI = Dr["CpfCnp_CLI"].ToString();
                        string Fantas_CLI = Dr["Fantas_CLI"].ToString();
                        string EmpSeq_CLI = Dr["EmpSeq_CLI"].ToString().PadLeft(6, '0');
                        string Descri_EMP = Dr["Descri_EMP"].ToString();



                        Dgv_Pesquisa.Rows.Add(Sequen_CLI, Descri_CLI, FormatarCpfCnpj(CpfCnp_CLI), Fantas_CLI, EmpSeq_CLI + " " + Descri_EMP);
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


        //SELECIONA AS FKs
        #region SELECIONA AS FKs PESQUISA

        //SELECIONA O CLIENTE
        public void PESQ_SelecionaClientLEAVE(TextBox txtPesPrincipal)
        {
            txtPesPrincipal.Text = txtPesPrincipal.Text.PadLeft(6, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Descri_CLI,Status_CLI FROM TabClien WHERE Sequen_CLI = @Sequen_CLI";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen_CLI", SqlDbType.Int).Value = txtPesPrincipal.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {

                }
                else
                {
                    txtPesPrincipal.Text = string.Empty;
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método PESQ_SelecionaClientTAB()\n\nBLOCO.: CLASSE TabClien_Pesquisa\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método PESQ_SelecionaClientTAB()\n\nBLOCO.: CLASSE TabClien_Pesquisa\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //SELECIONA A ROTA
        public void PESQ_SelecionaRotasLEAVE(TextBox txtPesRota)
        {
            txtPesRota.Text = txtPesRota.Text.PadLeft(6, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Sequen_ROT, Status_ROT FROM TabRotas WHERE Sequen_ROT = @Sequen_ROT";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen_ROT", SqlDbType.Int).Value = txtPesRota.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {

                }
                else
                {
                    txtPesRota.Text = string.Empty;
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método PESQ_SelecionaRotasLEAVE()\n\nBLOCO.: CLASSE TabClien_Pesquisa\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método PESQ_SelecionaRotasLEAVE()\n\nBLOCO.: CLASSE TabClien_Pesquisa\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //SELECIONA O CONVENIO
        public void PESQ_SelecionaConvenLEAVE(TextBox txtPesConvenio)
        {
            txtPesConvenio.Text = txtPesConvenio.Text.PadLeft(6, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Descri_COV, Status_COV FROM TabConve WHERE Sequen_COV = @Sequen_COV";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen_COV", SqlDbType.Int).Value = txtPesConvenio.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {

                }
                else
                {
                    txtPesConvenio.Text = string.Empty;
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método PESQ_SelecionaConvenLEAVE()\n\nBLOCO.: CLASSE TabClien_Pesquisa\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método PESQ_SelecionaConvenLEAVE()\n\nBLOCO.: CLASSE TabClien_Pesquisa\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }
        #endregion
    }
}
