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


namespace AplUsuaAt
{
    internal class AplUsuaAt_MET
    {
        //SELECIONA A COR DE FUNDO DOS FORMULÁRIOS
        public void MET_SelecionaCorFundo(Control Control_1, Control Control_2, Control Control_3, Control Control_4, Control Control_5, Control Control_6, string CodigoLoja)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string SelecionaCOR = "SELECT CorFun_CON FROM TabConfi WHERE SeqLoj_CON = " + CodigoLoja;
            SqlCommand Comando = new SqlCommand(SelecionaCOR, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string Cor = Dr["CorFun_CON"].ToString();

                    Control_1.BackColor = Color.FromName(Cor);
                    Control_2.BackColor = Color.FromName(Cor);
                    Control_3.BackColor = Color.FromName(Cor);
                    Control_4.BackColor = Color.FromName(Cor);
                    Control_5.BackColor = Color.FromName(Cor);
                    Control_6.BackColor = Color.FromName(Cor);
                }
                else
                {
                    Control_1.BackColor = Color.Silver;
                    Control_2.BackColor = Color.Silver;
                    Control_3.BackColor = Color.Silver;
                    Control_4.BackColor = Color.Silver;
                    Control_5.BackColor = Color.Silver;
                    Control_6.BackColor = Color.Silver;
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                Conexão.Close();
            }
        }

        //CAMPOS OBRIGATÓRIOS
        public bool MET_CamposObrig(ComboBox comPedidos, ComboBox comNotas, ComboBox comProduto)
        {
            if (comPedidos.SelectedIndex <= 0)
            {
                MessageBox.Show("Campo (Qt. a Pedido) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comPedidos.SelectAll(); comPedidos.Focus();
                return true;
            }
            if (comNotas.SelectedIndex <= 0)
            {
                MessageBox.Show("Campo (Qt. a Notas) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comNotas.SelectAll(); comNotas.Focus();
                return true;
            }
            if (comProduto.SelectedIndex <= 0)
            {
                MessageBox.Show("Campo (Qt. a Produto) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comProduto.SelectAll(); comProduto.Focus();
                return true;
            }
            return false;
        }

        //SELECIONA NO BANCO SO VALORES
        public void MET_SelecionaValores(string CodigoUsuar, TextBox txtAtalho1, TextBox txtAtalho2, ComboBox comPedidos, ComboBox comNotas, ComboBox comProduto, Button btnZerar)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string SelectUsu = "SELECT * FROM TabUsu02 WHERE SeqUsu_US2 = " + CodigoUsuar;
            SqlCommand ComandoSel = new SqlCommand(SelectUsu, Conexão);

            try
            {
                SqlDataReader Dr = ComandoSel.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    //VERIFICA SE EXISTE LINHA

                    string NomeAT1 = Dr["AtaNu1_US2"].ToString();
                    string NomeAT2 = Dr["AtaNu2_US2"].ToString();

                    txtAtalho1.Text = NomeAT1;
                    txtAtalho2.Text = NomeAT2;

                    string PEDIDO = Dr["AtaPed_US2"].ToString();
                    string NOTA = Dr["AtaNot_US2"].ToString();
                    string PRODUTO = Dr["AtaPro_US2"].ToString();

                    #region TRATAMENTO PEDIDO
                    if (PEDIDO == "AplPedVen")
                    {
                        comPedidos.SelectedIndex = 1;
                    }
                    else if (PEDIDO == "AplPedCon")
                    {
                        comPedidos.SelectedIndex = 2;
                    }
                    else if (PEDIDO == "AplPedTra")
                    {
                        comPedidos.SelectedIndex = 3;
                    }
                    else if (PEDIDO == "AplPedSer")
                    {
                        comPedidos.SelectedIndex = 4;
                    }
                    else if (PEDIDO == "AplPrePro")
                    {
                        comPedidos.SelectedIndex = 5;
                    }
                    else if (PEDIDO == "AplPreCom")
                    {
                        comPedidos.SelectedIndex = 6;
                    }
                    else if (PEDIDO == "AplOrcame")
                    {
                        comPedidos.SelectedIndex = 7;
                    }
                    else if (PEDIDO == "AplOrdSer")
                    {
                        comPedidos.SelectedIndex = 8;
                    }
                    else
                    {
                        comPedidos.SelectedIndex = 1;
                    }
                    #endregion

                    #region TRATAMENTO NOTA
                    if (NOTA == "AplNotVen")
                    {
                        comNotas.SelectedIndex = 1;
                    }
                    else if (NOTA == "AplNotCon")
                    {
                        comNotas.SelectedIndex = 2;
                    }
                    else if (NOTA == "AplNotTra")
                    {
                        comNotas.SelectedIndex = 3;
                    }
                    else if (NOTA == "AplNotDeF")
                    {
                        comNotas.SelectedIndex = 4;
                    }
                    else if (NOTA == "AplNotDeC")
                    {
                        comNotas.SelectedIndex = 5;
                    }
                    else if (NOTA == "AplNotLiS")
                    {
                        comNotas.SelectedIndex = 6;
                    }
                    else if (NOTA == "AplNotLiE")
                    {
                        comNotas.SelectedIndex = 7;
                    }
                    else if (NOTA == "AplPrCTRC")
                    {
                        comNotas.SelectedIndex = 8;
                    }
                    else
                    {
                        comNotas.SelectedIndex = 1;
                    }
                    #endregion

                    #region TRATAMENTO PRODUTOS
                    if (PRODUTO == "TabPro01")
                    {
                        comProduto.SelectedIndex = 1;
                    }
                    else if (PRODUTO == "TabPro02")
                    {
                        comProduto.SelectedIndex = 2;
                    }
                    else if (PRODUTO == "TabPro03")
                    {
                        comProduto.SelectedIndex = 3;
                    }
                    else
                    {
                        comProduto.SelectedIndex = 1;
                    }
                    #endregion

                    Dr.Close();
                }
                else
                {
                    #region INSERE VALORES GENERICOS
                    //SE NÃO EXISTE, INSERE UMA INFORMAÇÃO GENERICA
                    Dr.Close();

                    string InsertUsu = "INSERT INTO TabUsu02 (SeqUsu_US2,AtaNu1_US2,AtaNu2_US2,AtaPed_US2,AtaNot_US2,AtaPro_US2) VALUES (" + CodigoUsuar + ",'','','AplPedVen','AplNotVen','TabPro01')";
                    SqlCommand ComandoInse = new SqlCommand(InsertUsu, Conexão);

                    try
                    {
                        ComandoInse.ExecuteNonQuery();
                    }
                    catch (SqlException Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelecionaValores()\n\nBLOCO = AplUsuaAt_MET - INSERE VALORES GENERICOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelecionaValores()\n\nBLOCO = AplUsuaAt_MET - INSERE VALORES GENERICOS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        btnZerar.PerformClick();
                    }
                    #endregion
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelecionaValores()\n\nBLOCO = AplUsuaAt_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelecionaValores()\n\nBLOCO = AplUsuaAt_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //MODIFICA OS VALORES DOS ATALHOS
        public void MET_ModificaValores(string CodigoUsuar, TextBox txtAtalho1, TextBox txtAtalho2, ComboBox comPedidos, ComboBox comNotas, ComboBox comProduto, Form FORMU)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string strComandoUpdate = "UPDATE TabUsu02 SET AtaNu1_US2 = @AtaNu1_US2, AtaNu2_US2 = @AtaNu2_US2, AtaPed_US2 = @AtaPed_US2, AtaNot_US2 = @AtaNot_US2, AtaPro_US2 = @AtaPro_US2 WHERE SeqUsu_US2 = @SeqUsu_US2";
            SqlCommand Comando = new SqlCommand(strComandoUpdate, Conexão);

            string PEDIDO = "";
            string NOTA = "";
            string PRODUTO = "";

            #region TRATAMENTO PEDIDO
            if (comPedidos.SelectedIndex == 1)
            {
                PEDIDO = "AplPedVen";
            }
            else if (comPedidos.SelectedIndex == 2)
            {
                PEDIDO = "AplPedCon";
            }
            else if (comPedidos.SelectedIndex == 3)
            {
                PEDIDO = "AplPedTra";
            }
            else if (comPedidos.SelectedIndex == 4)
            {
                PEDIDO = "AplPedSer";
            }
            else if (comPedidos.SelectedIndex == 5)
            {
                PEDIDO = "AplPrePro";
            }
            else if (comPedidos.SelectedIndex == 6)
            {
                PEDIDO = "AplPreCom";
            }
            else if (comPedidos.SelectedIndex == 7)
            {
                PEDIDO = "AplOrcame";
            }
            else if (comPedidos.SelectedIndex == 8)
            {
                PEDIDO = "AplOrdSer";
            }
            else
            {
                PEDIDO = "AplPedVen";
            }
            #endregion

            #region TRATAMENTO NOTA
            if (comNotas.SelectedIndex == 1)
            {
                NOTA = "AplNotVen";
            }
            else if (comNotas.SelectedIndex == 2)
            {
                NOTA = "AplNotCon";
            }
            else if (comNotas.SelectedIndex == 3)
            {
                NOTA = "AplNotTra";
            }
            else if (comNotas.SelectedIndex == 4)
            {
                NOTA = "AplNotDeF";
            }
            else if (comNotas.SelectedIndex == 5)
            {
                NOTA = "AplNotDeC";
            }
            else if (comNotas.SelectedIndex == 6)
            {
                NOTA = "AplNotLiS";
            }
            else if (comNotas.SelectedIndex == 7)
            {
                NOTA = "AplNotLiE";
            }
            else if (comNotas.SelectedIndex == 8)
            {
                NOTA = "AplPrCTRC";
            }
            else
            {
                NOTA = "AplNotVen";
            }
            #endregion

            #region TRATAMENTO PRODUTOS
            if (comProduto.SelectedIndex == 1)
            {
                PRODUTO = "TabPro01";
            }
            else if (comProduto.SelectedIndex == 2)
            {
                PRODUTO = "TabPro02";
            }
            else if (comProduto.SelectedIndex == 3)
            {
                PRODUTO = "TabPro03";
            }
            else
            {
                PRODUTO = "TabPro01";
            }
            #endregion

            Comando.Parameters.Add("@SeqUsu_US2", SqlDbType.Int).Value = CodigoUsuar;
            Comando.Parameters.Add("@AtaNu1_US2", SqlDbType.VarChar).Value = txtAtalho1.Text;
            Comando.Parameters.Add("@AtaNu2_US2", SqlDbType.VarChar).Value = txtAtalho2.Text;
            Comando.Parameters.Add("@AtaPed_US2", SqlDbType.VarChar).Value = PEDIDO;
            Comando.Parameters.Add("@AtaNot_US2", SqlDbType.VarChar).Value = NOTA;
            Comando.Parameters.Add("@AtaPro_US2", SqlDbType.VarChar).Value = PRODUTO;

            try
            {
                Comando.ExecuteNonQuery();
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_ModificaValores()\n\nBLOCO = AplUsuaAt_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_ModificaValores()\n\nBLOCO = AplUsuaAt_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
                FORMU.Close();
            }
        }
    }
}