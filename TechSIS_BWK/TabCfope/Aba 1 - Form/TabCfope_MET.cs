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

namespace TabCfope
{
    internal class TabCfope_MET
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

        //BUSCA OS CFOPS CORRESPONDENTES
        public void MET_SelectionaCFOPCorres(TextBox txtIndCod, TextBox txtIndDesc, TextBox txtComeCod, TextBox txtComeDesc)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão1 = new SqlConnection(LerString);
            Conexão1.Open();
            SqlConnection Conexão2 = new SqlConnection(LerString);
            Conexão2.Open();


            string StringComando1 = "SELECT Descri_CFO FROM TabCfope WHERE Sequen_CFO = @Seq1";
            SqlCommand Comando1 = new SqlCommand(StringComando1, Conexão1);
            Comando1.Parameters.Add("@Seq1", SqlDbType.Int).Value = txtIndCod.Text;

            string StringComando2 = "SELECT Descri_CFO FROM TabCfope WHERE Sequen_CFO = @Seq1";
            SqlCommand Comando2 = new SqlCommand(StringComando2, Conexão2);
            Comando2.Parameters.Add("@Seq1", SqlDbType.Int).Value = txtComeCod.Text;


            try
            {
                if (txtIndCod.Text != string.Empty)
                {
                    SqlDataReader Dr1 = Comando1.ExecuteReader(); Dr1.Read();
                    if (Dr1.HasRows)
                    {
                        txtIndDesc.Text = Dr1["Descri_CFO"].ToString();
                        Dr1.Close();
                    }
                }
                if (txtComeCod.Text != string.Empty)
                {
                    SqlDataReader Dr2 = Comando2.ExecuteReader(); Dr2.Read();
                    if (Dr2.HasRows)
                    {
                        txtComeDesc.Text = Dr2["Descri_CFO"].ToString();
                        Dr2.Close();
                    }
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelectionaCFOPCorres()\n\nBLOCO.: TabCfope_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelectionaCFOPCorres()\n\nBLOCO.: TabCfope_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão1.Close();
                Conexão2.Close();
            }
        }

        //SELECIONA CFOP NO TAB
        public void MET_SelectionaCFOPTab(TextBox Codigo, TextBox Descricao, Control Retorno)
        {
            Codigo.Text = Codigo.Text.PadLeft(4, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Descri_CFO FROM TabCfope WHERE Sequen_CFO = " + Codigo.Text;
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    if (Convert.ToInt32(Codigo.Text.Substring(0, 1)) > 3)
                    {
                        Descricao.Text = string.Empty;
                        MessageBox.Show("Selecione um CFOP de ENTRADA. O informado é CFOP de SAIDA", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Retorno.Focus(); Codigo.SelectAll();
                    }
                    else
                    {
                        Descricao.Text = Dr["Descri_CFO"].ToString();
                    }
                }
                else
                {
                    Descricao.Text = string.Empty;
                    MessageBox.Show("CFOP não encontrado no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Retorno.Focus(); Codigo.SelectAll();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelectionaCFOPTab()\n\nBLOCO.: TabCfope_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelectionaCFOPTab()\n\nBLOCO.: TabCfope_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //BUSCA AS INFORMAÇÕES AO DIGITAR TAB NO CAMPO CÓDIGO
        public void MET_SelecionaCodigoTAB(TextBox txtMESTRE, TextBox txtCodigo, Panel panUpAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Button btnGravar, MethodInvoker CamposEnable, TextBox txtDescri, string LojaLogada, TextBox txtIndCod, TextBox txtIndDesc, TextBox txtComeCod, TextBox txtComeDesc, ComboBox comLocalizacao)
        {
            #region TRATAMENTO txtCodigo em BRANCO e > QUE 9999
            #region TRATAMENTO EM BRANCO
            if (txtCodigo.Text == string.Empty)
            {
                txtCodigo.Text = "000";
            }
            #endregion
            #region TRATAMENTO = 0
            if (Convert.ToInt32(txtCodigo.Text) == 0)
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(3, '0');
                panUpAb1.Focus();
                txtCodigo.SelectAll();
                ZerarCampos();
                CamposDisable();
                btnGravar.Enabled = false;
                MessageBox.Show("Campo (Código) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #endregion
            #region TRATAMENTO LIMITE
            if (Convert.ToInt32(txtCodigo.Text) == 9999)
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(4, '0');
                panUpAb1.Focus();
                txtCodigo.SelectAll();
                ZerarCampos();
                CamposDisable();
                btnGravar.Enabled = false;
                MessageBox.Show("LIMITE DE CADASTROS DE CFOPs ATINGIDO", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            else
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(4, '0');
            }


            #endregion

            #region INCLUIR
            if (txtMESTRE.Text == "INCLUIR")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                //Define a string e o comando
                string StringComandoSELEÇÃO = ("SELECT * FROM TabCfope WHERE Sequen_CFO = @Sequen_CFO");
                SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                ComandoSELEÇÃO.Parameters.Add("@Sequen_CFO", SqlDbType.VarChar).Value = txtCodigo.Text;

                try
                {
                    SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        ZerarCampos();

                        txtDescri.Text = Dr["Descri_CFO"].ToString();
                        #region txtIndCod.Text = Dr["EntInd_CFO"].ToString().PadLeft(4, '0');
                        if (Convert.ToInt32(Dr["EntInd_CFO"]) <= 0)
                        {
                            txtIndCod.Text = string.Empty;
                        }
                        else
                        {
                            txtIndCod.Text = Dr["EntInd_CFO"].ToString().PadLeft(4, '0');
                        }
                        #endregion
                        #region txtIndCod.Text = Dr["EntInd_CFO"].ToString().PadLeft(4, '0');
                        if (Convert.ToInt32(Dr["EntCom_CFO"]) <= 0)
                        {
                            txtComeCod.Text = string.Empty;
                        }
                        else
                        {
                            txtComeCod.Text = Dr["EntCom_CFO"].ToString().PadLeft(4, '0');
                        }
                        #endregion
                        #region comLocalizacao.SelectedIndex = Convert.ToInt32(Dr["DenFor_CFO"]);
                        try
                        {
                            comLocalizacao.SelectedIndex = Convert.ToInt32(Dr["DenFor_CFO"]);
                        }
                        catch (Exception)
                        {
                            comLocalizacao.SelectedIndex = -1;
                        }
                        #endregion

                        //SELECIONA OS CFOPs
                        MET_SelectionaCFOPCorres(txtIndCod, txtIndDesc, txtComeCod, txtComeDesc);


                        //TRATAMENTO PARA CLIENTE JÁ EXISTENTE
                        panUpAb1.Focus();
                        txtCodigo.SelectAll();
                        CamposDisable();
                        btnGravar.Enabled = false;
                        MessageBox.Show("Registro já cadastrado no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        ZerarCampos();
                        CamposEnable();
                        btnGravar.Enabled = true;

                        #region TRATAMENTO PARA DENTRO\FORA DO ESTADO NA INCLUSÃO
                        if (txtCodigo.Text.Substring(0,1) == "1")
                        {
                            comLocalizacao.SelectedIndex = 0;
                        }
                        else if (txtCodigo.Text.Substring(0, 1) == "2")
                        {
                            comLocalizacao.SelectedIndex = 1;
                        }
                        else if (txtCodigo.Text.Substring(0, 1) == "5")
                        {
                            comLocalizacao.SelectedIndex = 0;
                        }
                        else if (txtCodigo.Text.Substring(0, 1) == "6")
                        {
                            comLocalizacao.SelectedIndex = 1;
                        }
                        else
                        {
                            comLocalizacao.SelectedIndex = 1;
                        }
                        #endregion
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: TabCfope_MET = INCLUIR\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: TabCfope_MET = INCLUIR\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
            #region OUTROS
            if (txtMESTRE.Text != "INCLUIR")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                //Define a string e o comando
                string StringComandoSELEÇÃO = ("SELECT * FROM TabCfope WHERE Sequen_CFO = @Sequen_CFO");
                SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                ComandoSELEÇÃO.Parameters.Add("@Sequen_CFO", SqlDbType.VarChar).Value = txtCodigo.Text;

                try
                {
                    SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        ZerarCampos();

                        txtDescri.Text = Dr["Descri_CFO"].ToString();
                        #region txtIndCod.Text = Dr["EntInd_CFO"].ToString().PadLeft(4, '0');
                        if (Convert.ToInt32(Dr["EntInd_CFO"]) <= 0)
                        {
                            txtIndCod.Text = string.Empty;
                        }
                        else
                        {
                            txtIndCod.Text = Dr["EntInd_CFO"].ToString().PadLeft(4, '0');
                        }
                        #endregion
                        #region txtIndCod.Text = Dr["EntInd_CFO"].ToString().PadLeft(4, '0');
                        if (Convert.ToInt32(Dr["EntCom_CFO"]) <= 0)
                        {
                            txtComeCod.Text = string.Empty;
                        }
                        else
                        {
                            txtComeCod.Text = Dr["EntCom_CFO"].ToString().PadLeft(4, '0');
                        }
                        #endregion
                        #region comLocalizacao.SelectedIndex = Convert.ToInt32(Dr["DenFor_CFO"]);
                        try
                        {
                            comLocalizacao.SelectedIndex = Convert.ToInt32(Dr["DenFor_CFO"]);
                        }
                        catch (Exception)
                        {
                            comLocalizacao.SelectedIndex = -1;
                        }
                        #endregion

                        //SELECIONA OS CFOPs
                        MET_SelectionaCFOPCorres(txtIndCod, txtIndDesc, txtComeCod, txtComeDesc);


                        if (txtMESTRE.Text == "SELECT" || txtMESTRE.Text == "CONSULTA")
                        {
                            CamposDisable();
                            btnGravar.Enabled = false;
                            panUpAb1.Focus();
                            txtCodigo.SelectAll();
                        }
                        else
                        {
                            CamposEnable();
                            btnGravar.Enabled = true;
                        }
                    }
                    else
                    {
                        ZerarCampos();
                        CamposDisable();
                        btnGravar.Enabled = false;
                        panUpAb1.Focus();
                        txtCodigo.SelectAll();
                        MessageBox.Show("Registro inexistente no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: TabCfope_MET != INCLUIR\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: TabCfope_MET != INCLUIR\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
        }

        //SELECIONA O ULTIMO REGISTRO NO BANCO E ADICIONA MAIS 1
        public void MET_SelecionaUltimoRegistroMaisUm(TextBox txtCodigo, Button btnGravar, string LojaLogada)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT MAX (Sequen_CFO + 1) as Sequen_CFO FROM TabCfope";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string Codigo = Dr["Sequen_CFO"].ToString();
                    if (Codigo == string.Empty)
                    {
                        txtCodigo.Text = "1000";
                    }
                    else
                    {
                        txtCodigo.Text = Codigo.PadLeft(4, '0');
                    }

                    txtCodigo.SelectAll();
                    Dr.Close();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUltimoRegistroMaisUm()\n\nBLOCO.: TabCfope_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUltimoRegistroMaisUm()\n\nBLOCO.: TabCfope_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
            }
            finally
            {
                Conexão.Close();
            }
        }

        //CAPTURA O CAMINHO DO SALVAMENTO
        public string MET_CapCaminhoSALV(TextBox txtCaminhoRel, string LojaLogada)
        {
            if (txtCaminhoRel.Text == string.Empty)
            {

                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();


                string StringCapCaminhoSALV = "SELECT CamRel_CON FROM TabConfi WHERE SeqLoj_CON = @Sequen";
                SqlCommand Comando = new SqlCommand(StringCapCaminhoSALV, Conexão);

                Comando.Parameters.Add("@Sequen", SqlDbType.Int).Value = LojaLogada;

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        string Caminho = Dr["CamRel_CON"].ToString();

                        return Caminho;
                    }
                    else
                    {
                        MessageBox.Show("Caminho padrão para salvar arquivos de relatório não foi localizado", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_CapCaminhoSALV()\n\nBLOCO = TabClien_MET - CAPTURA CAMINHO DOS RELATÓRIOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_CapCaminhoSALV()\n\nBLOCO = TabClien_MET - CAPTURA CAMINHO DOS RELATÓRIOS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
            return txtCaminhoRel.Text;
        }
    }
}
