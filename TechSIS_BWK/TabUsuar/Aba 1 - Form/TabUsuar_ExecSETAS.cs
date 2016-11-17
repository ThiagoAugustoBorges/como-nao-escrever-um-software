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

namespace TabUsuar
{
    internal class TabUsuar_ExecSETAS
    {
        //Popula o datagridview
        public void MET_SelecionaCodigoTAB_DGV(TextBox txtCodigo, DataGridView Dgv_Empresas, Button btnGravar)
        {
            Dgv_Empresas.Rows.Clear();

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT SeqUsu_US1,SeqLoj_US1,Descri_EMP,SeqCai_US1,PerCre_US1 FROM TabUsu01 INNER JOIN TabUsuar ON TabUsu01.SeqUsu_US1 = TabUsuar.Sequen_USU INNER JOIN TabEmpre ON TabUsu01.SeqLoj_US1 = TabEmpre.Sequen_EMP WHERE SeqUsu_US1 = @Sequen";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen", SqlDbType.VarChar).Value = txtCodigo.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    if (Dr.HasRows)
                    {
                        string SeqLoj_US1 = Dr["SeqLoj_US1"].ToString().PadLeft(6, '0');
                        string Descri_EMP = Dr["Descri_EMP"].ToString();
                        string SeqCai_US1 = Dr["SeqCai_US1"].ToString().PadLeft(6, '0');
                        string PerCre_US1 = Dr["PerCre_US1"].ToString();

                        Dgv_Empresas.Rows.Add(SeqLoj_US1, Descri_EMP, SeqCai_US1, PerCre_US1);
                    }
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB_DGV()\n\nBLOCO.: 1 = TabUsuar_MET - POPULAR DataGridView\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB_DGV()\n\nBLOCO.: 1 = TabUsuar_MET - POPULAR DataGridView\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
            }
            finally
            {
                Conexão.Close();
            }
        }

        //Seleciona a empresa
        public void MET_SelecionaEmpresa(TextBox CodigoEmpresa, TextBox DescriEmpresa, Control Retorno)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Descri_EMP FROM TabEmpre WHERE Sequen_EMP = @Sequen";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen", SqlDbType.VarChar).Value = CodigoEmpresa.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    DescriEmpresa.Text = Dr["Descri_EMP"].ToString();
                }
                else
                {
                    DescriEmpresa.Text = string.Empty;
                    MessageBox.Show("Empresa inexistente no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Retorno.Focus(); CodigoEmpresa.SelectAll();
                }
            }
            catch (SqlException Ex)
            {
                DescriEmpresa.Text = string.Empty;
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_BuscaDadosLoja()\n\nBLOCO.: BUSCA LOJA NO BANCO\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Retorno.Focus(); CodigoEmpresa.SelectAll();
            }
            catch (Exception Ex)
            {
                DescriEmpresa.Text = string.Empty;
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_BuscaDadosLoja()\n\nBLOCO.: BUSCA LOJA NO BANCO\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Retorno.Focus(); CodigoEmpresa.SelectAll();
            }
            finally
            {
                Conexão.Close();
            }
        }

        public void ExexSETAS(string StringComandoSELEÇÃO, TextBox txtMESTRE, TextBox txtCodigo, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Panel panCodigoAb1, TextBox txtDescri, ComboBox comPermissao, TextBox txtEmailSuporte, TextBox txtMsn, TextBox txtEmailContato, TextBox txtSkype, TextBox txtEmpreCod, TextBox txtEmpreDesc, TextBox txtApelido, TextBox txtEmpresa, TextBox txtEmpreDescDown, TextBox txtCaixa, DataGridView Dgv_Empresas, ComboBox comStatus, Button btnGravar, MethodInvoker CamposEnable)
        {
            string Mensagem_De_Erro = "";

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string NomeDaOpção = "Usuários";

            #region Comandos
            if (StringComandoSELEÇÃO == "1")
            {
                StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabUsuar WHERE Status_USU <> 3 AND Sequen_USU > 1 ORDER BY Sequen_USU";
                Mensagem_De_Erro = "Sem dados de " + NomeDaOpção + " para exibir";
            }
            if (StringComandoSELEÇÃO == "2")
            {
                string Num = txtCodigo.Text;
                if (txtCodigo.Text == string.Empty)
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabUsuar WHERE Status_USU <> 3 AND Sequen_USU > 1 ORDER BY Sequen_USU";
                    Mensagem_De_Erro = "Não existe registro anterior no banco";
                }
                else
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabUsuar WHERE Sequen_USU < " + Num + " AND Status_USU <> 3 AND Sequen_USU > 1 ORDER BY Sequen_USU DESC";
                    Mensagem_De_Erro = "Não existe registro anterior no banco";
                }
            }
            if (StringComandoSELEÇÃO == "3")
            {
                string Num = txtCodigo.Text;
                if (txtCodigo.Text == string.Empty)
                {
                    Num = "1";
                }
                StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabUsuar WHERE Sequen_USU > " + Num + " AND Status_USU <> 3";
                Mensagem_De_Erro = "Não existe próximo registro no banco";
            }
            if (StringComandoSELEÇÃO == "4")
            {
                StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabUsuar WHERE Status_USU <> 3 AND Sequen_USU > 1 ORDER BY Sequen_USU DESC";
                Mensagem_De_Erro = "Sem dados de " + NomeDaOpção + " para exibir";
            }
            #endregion


            SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);

            try
            {
                SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    txtCodigo.Text = Dr["Sequen_USU"].ToString().PadLeft(6, '0');
                    txtDescri.Text = Dr["Descri_USU"].ToString();
                    comPermissao.SelectedIndex = Convert.ToInt32(Dr["Tipo01_USU"]);
                    txtEmailSuporte.Text = Dr["Emai01_USU"].ToString();
                    txtMsn.Text = Dr["Mesg01_USU"].ToString();
                    txtEmailContato.Text = Dr["Emai02_USU"].ToString();
                    txtSkype.Text = Dr["Skype1_USU"].ToString();
                    txtEmpreCod.Text = Dr["CodLoj_USU"].ToString().PadLeft(6, '0');
                    txtApelido.Text = Dr["Apelid_USU"].ToString();
                    comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_USU"]);

                    //Seleciona a empresa
                    MET_SelecionaEmpresa(txtEmpreCod, txtEmpreDesc, null);

                    //Popula o grid
                    MET_SelecionaCodigoTAB_DGV(txtCodigo, Dgv_Empresas, btnGravar);

                    btnGravar.Enabled = false;
                    CamposDisable();
                }
                else
                {
                    btnGravar.Enabled = false;
                    CamposDisable();

                    MessageBox.Show(Mensagem_De_Erro, "Consulta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método ExecSETAS()\n\nBLOCO = CONSULTA DE DADOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método ExecSETAS()\n\nBLOCO = CONSULTA DE DADOS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }
    }
}
