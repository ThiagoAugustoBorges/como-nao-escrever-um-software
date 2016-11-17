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

namespace TabRotas
{
    internal class TabRotas_Relac
    {
        //POPULA O DGV DA RELAÇÃO
        public void Rel_PopulaDataGrid(DataGridView Dgv_Relacao, TextBox txtCodigoRel, TextBox txtDescriRel)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            Dgv_Relacao.Rows.Clear();

            string strComando = "SELECT SeqCli_RO1,Ordems_RO1,Descri_CLI FROM TabRot01 INNER JOIN TabClien ON TabRot01.SeqCli_RO1 = TabClien.Sequen_CLI WHERE Sequen_RO1 = @Sequen_RO1";
            SqlCommand Comando = new SqlCommand(strComando, Conexão);

            Comando.Parameters.Add("@Sequen_RO1", SqlDbType.Int).Value = txtCodigoRel.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader();
                while (Dr.Read())
                {
                    if (Dr.HasRows)
                    {
                        string SeqCli_RO1 = Dr["SeqCli_RO1"].ToString().PadLeft(6, '0');
                        string Ordems_RO1 = Dr["Ordems_RO1"].ToString().PadLeft(6, '0');
                        string Descri_CLI = Dr["Descri_CLI"].ToString();


                        Dgv_Relacao.Rows.Add(Ordems_RO1, Descri_CLI, SeqCli_RO1);

                    }
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Rel_PopulaDataGrid()\n\nBLOCO = MÉTODO DA TabRotas_Relac\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Rel_PopulaDataGrid()\n\nBLOCO = MÉTODO DA TabRotas_Relac\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //SELECIONA A ROTA NO TAB
        public void Rel_SelectRota(DataGridView Dgv_Relacao, TextBox txtCodigoRel, TextBox txtDescriRel, Control Retorno, MethodInvoker ButtonsEnable, MethodInvoker ButtonsDisable)
        {
            txtCodigoRel.Text = txtCodigoRel.Text.PadLeft(6, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            Dgv_Relacao.Rows.Clear();

            string strComando = "SELECT Descri_ROT,Status_ROT FROM TabRotas WHERE Sequen_ROT = @Sequen_ROT";
            SqlCommand Comando = new SqlCommand(strComando, Conexão);

            Comando.Parameters.Add("@Sequen_ROT", SqlDbType.Int).Value = txtCodigoRel.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    #region TRATAMENTO INATIVO EXCLUIDO
                    if (Convert.ToInt32(Dr["Status_ROT"]) == 2)
                    {
                        txtDescriRel.Text = string.Empty;
                        Retorno.Focus();
                        txtCodigoRel.SelectAll();
                        ButtonsDisable();
                        DialogResult Continua = MessageBox.Show("Rota consta como INATIVA. Deseja Continuar?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (Continua == DialogResult.Yes)
                        {
                            txtDescriRel.Text = Dr["Descri_ROT"].ToString();
                            Rel_PopulaDataGrid(Dgv_Relacao, txtCodigoRel, txtDescriRel);
                            ButtonsEnable();
                            if (Dgv_Relacao.Rows.Count <= 0)
                            {
                                ButtonsDisable();
                                MessageBox.Show("Nenhum cliente informado para a Rota selecionada", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            txtDescriRel.Text = string.Empty;
                            Retorno.Focus();
                            txtCodigoRel.SelectAll();
                            ButtonsDisable();
                        }
                         
                    }
                    else if (Convert.ToInt32(Dr["Status_ROT"]) == 3)
                    {
                        txtDescriRel.Text = string.Empty;
                        Retorno.Focus();
                        txtCodigoRel.SelectAll();
                        ButtonsDisable();
                        MessageBox.Show("Rota consta como EXCLUIDA. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    #endregion
                    else
                    {
                        txtDescriRel.Text = Dr["Descri_ROT"].ToString();
                        Rel_PopulaDataGrid(Dgv_Relacao, txtCodigoRel, txtDescriRel);
                        ButtonsEnable();
                        if (Dgv_Relacao.Rows.Count <= 0)
                        {
                            ButtonsDisable();
                            MessageBox.Show("Nenhum cliente informado para a Rota selecionada", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    txtDescriRel.Text = string.Empty;
                    Retorno.Focus();
                    txtCodigoRel.SelectAll();
                    ButtonsDisable();
                    MessageBox.Show("Rota não encontrada no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Rel_SelectRota()\n\nBLOCO = MÉTODO DA TabRotas_Relac\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Rel_SelectRota()\n\nBLOCO = MÉTODO DA TabRotas_Relac\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
                Retorno.Focus();
                txtCodigoRel.SelectAll();
            }
        }

        //REORGANIZA AS INFORMAÇÕES NO GRID
        public void Rel_ReorganizarUP(DataGridView Dgv_Relacao)
        {
            try
            {
                int TotalDeLinhas = Dgv_Relacao.Rows.Count;
                int IdSelecionada = Dgv_Relacao.SelectedCells[0].OwningRow.Index;
                if (IdSelecionada == 0)
                    return;
                int IdColuna = Dgv_Relacao.SelectedCells[0].OwningColumn.Index;
                DataGridViewRowCollection ColecaoLinhas = Dgv_Relacao.Rows;
                DataGridViewRow Linha = ColecaoLinhas[IdSelecionada];
                ColecaoLinhas.Remove(Linha);
                ColecaoLinhas.Insert(IdSelecionada - 1, Linha);
                Dgv_Relacao.ClearSelection();
                Dgv_Relacao.Rows[IdSelecionada - 1].Cells[IdColuna].Selected = true;
            }
            catch { }
        }
        public void Rel_ReorganizarDOWN(DataGridView Dgv_Relacao)
        {
            try
            {
                int TotalDeLinhas = Dgv_Relacao.Rows.Count;
                int IdSelecionada = Dgv_Relacao.SelectedCells[0].OwningRow.Index;
                if (IdSelecionada == TotalDeLinhas - 1)
                    return;
                int IdColuna = Dgv_Relacao.SelectedCells[0].OwningColumn.Index;
                DataGridViewRowCollection ColecaoLinhas = Dgv_Relacao.Rows;
                DataGridViewRow Linha = ColecaoLinhas[IdSelecionada];
                ColecaoLinhas.Remove(Linha);
                ColecaoLinhas.Insert(IdSelecionada + 1, Linha);
                Dgv_Relacao.ClearSelection();
                Dgv_Relacao.Rows[IdSelecionada + 1].Cells[IdColuna].Selected = true;
            }
            catch { }
        }

        //GRAVA AS INFORMAÇÕES NA TABELA
        public void Rel_GravaInformac(DataGridView Dgv_Relacao, TextBox txtCodigo, TextBox txtDescri)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            //DELETA TODO O GRID
            string strComandoEXC = "DELETE FROM TabRot01 WHERE Sequen_RO1 = " + txtCodigo.Text;
            SqlCommand ComandoEXC = new SqlCommand(strComandoEXC, Conexão);
            ComandoEXC.ExecuteNonQuery();

            try
            {

                int i = 0;
                while (i < Dgv_Relacao.RowCount)
                {
                    try
                    {
                        string strComando = "INSERT INTO TabRot01 VALUES (@Sequen_RO1, @SeqCli_RO1, @Ordems_RO1, '1')";
                        SqlCommand Comando = new SqlCommand(strComando, Conexão);

                        Comando.Parameters.Add("@Ordems_RO1", SqlDbType.Int).Value = i + 1;
                        Comando.Parameters.Add("@Sequen_RO1", SqlDbType.Int).Value = txtCodigo.Text;
                        Comando.Parameters.Add("@SeqCli_RO1", SqlDbType.Int).Value = Convert.ToInt32(Dgv_Relacao.Rows[i].Cells["dgvcRelCodigo"].Value);
                        Comando.ExecuteNonQuery();
                        i++;
                    }
                    catch (SqlException Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Rel_GravaInformac()\n\nBLOCO = 1 - INSERIR VALORES\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Rel_GravaInformac()\n\nBLOCO = 1 - INSERIR VALORES\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }






                MessageBox.Show("Reorganização de Rotas efetuada com sucesso!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Rel_PopulaDataGrid(Dgv_Relacao, txtCodigo, txtDescri);

                txtCodigo.Select();
                txtCodigo.SelectAll();


            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Rel_GravaInformac()\n\nBLOCO = 2 - TODO O MÉTODO\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Rel_GravaInformac()\n\nBLOCO = 2 - TODO O MÉTODO\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }
    }
}