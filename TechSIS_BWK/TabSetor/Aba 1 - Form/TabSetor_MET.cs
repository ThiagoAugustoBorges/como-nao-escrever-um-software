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

namespace TabSetor
{
    internal class TabSetor_MET
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

        //SELECIONA A COR DE FUNDO DOS FORMULÁRIOS
        public void MET_SelectCorFundo(Control Control_1, Control Control_2, Control Control_3, Control Control_4, Control Control_5, Control Control_6, Control Control_7, string CodigoLoja)
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
                    string Cor = Dr[0].ToString();

                    Control_1.BackColor = Color.FromName(Cor);
                    Control_2.BackColor = Color.FromName(Cor);
                    Control_3.BackColor = Color.FromName(Cor);
                    Control_4.BackColor = Color.FromName(Cor);
                    Control_5.BackColor = Color.FromName(Cor);
                    Control_6.BackColor = Color.FromName(Cor);
                    Control_7.BackColor = Color.FromName(Cor);
                }
                else
                {
                    Control_1.BackColor = Color.Silver;
                    Control_2.BackColor = Color.Silver;
                    Control_3.BackColor = Color.Silver;
                    Control_4.BackColor = Color.Silver;
                    Control_5.BackColor = Color.Silver;
                    Control_6.BackColor = Color.Silver;
                    Control_7.BackColor = Color.Silver;
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
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_CapCaminhoSALV()\n\nBLOCO = TabSetor_MET - CAPTURA CAMINHO DOS RELATÓRIOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_CapCaminhoSALV()\n\nBLOCO = TabSetor_MET - CAPTURA CAMINHO DOS RELATÓRIOS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
            return txtCaminhoRel.Text;
        }



        //BUSCA AS INFORMAÇÕES AO DIGITAR TAB NO CAMPO CÓDIGO
        public void MET_SelecionaCodigoTAB_SET(TextBox txtMESTRE, TextBox txtSetCod, TextBox txtSubCod, Control Retorno, MethodInvoker ZerarCampos, MethodInvoker CamposEnable, MethodInvoker CamposDisable, Button btnGravar, string LojaLogada, Label lblDescricao, ComboBox comStatus)
        {
            #region TRATAMENTO txtSetCod em BRANCO e > QUE 999
            #region TRATAMENTO EM BRANCO
            if (txtSetCod.Text == string.Empty)
            {
                txtSetCod.Text = "000";
            }
            #endregion
            #region TRATAMENTO = 0
            if (Convert.ToInt32(txtSetCod.Text) == 0)
            {
                txtSetCod.Text = txtSetCod.Text.PadLeft(3, '0');
                Retorno.Focus();
                txtSetCod.SelectAll();
                ZerarCampos();
                CamposDisable();
                btnGravar.Enabled = false;
                MessageBox.Show("Campo (Setor) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #endregion
            #region TRATAMENTO LIMITE
            if (Convert.ToInt32(txtSetCod.Text) == 999)
            {
                txtSetCod.Text = txtSetCod.Text.PadLeft(3, '0');
                Retorno.Focus();
                txtSetCod.SelectAll();
                ZerarCampos();
                CamposDisable();
                btnGravar.Enabled = false;
                MessageBox.Show("LIMITE DE CADASTROS DE SETORES ATINGIDO", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            else
            {
                txtSetCod.Text = txtSetCod.Text.PadLeft(3, '0');
            }
            #endregion

            #region TRATAMENTO INCLUIR
            if (txtMESTRE.Text == "INCLUIR")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                //Define a string e o comando
                string StringComandoSELEÇÃO = ("SELECT TOP 1 Descri_SET, Status_SET FROM TabSetor WHERE Sequen_SET LIKE '%0000%' AND LEFT(RIGHT(REPLICATE('0',07) + CAST(CAST(Sequen_SET AS INT) AS VARCHAR),07),3) = @Sequen_SET ORDER BY Sequen_SET");
                SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                ComandoSELEÇÃO.Parameters.Add("@Sequen_SET", SqlDbType.VarChar).Value = txtSetCod.Text;

                try
                {
                    SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();

                    if (Dr.HasRows)
                    {
                        ZerarCampos();


                        #region TRATAMENTO EXCLUIDO
                        if (Dr["Status_SET"].ToString() == "3")
                        {
                            btnGravar.Enabled = false;
                            ZerarCampos();
                            CamposDisable();
                            Retorno.Focus();
                            lblDescricao.Text = "DESCRIÇÃO DO SETOR OU SUBSETOR";
                            txtSubCod.Text = string.Empty;
                            MessageBox.Show("Registro consta como EXCLUIDO!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        #endregion
                        else
                        {
                            string Descri_SET = Dr["Descri_SET"].ToString();

                            txtSubCod.Enabled = true;
                            lblDescricao.Text = "DESCRIÇÃO DO SETOR OU SUBSETOR (" + Descri_SET + ")";
                            //TRATAMENTO PARA SETOR JÁ EXISTENTE
                            //SELECIONA O ULTIMO +1
                            MET_SelecionaUltimoRegistroMaisUmSUB(txtSubCod, txtSetCod, btnGravar, LojaLogada);     
                        }
                    }
                    else
                    {
                        txtSubCod.Text = "0000";
                        ZerarCampos();
                        CamposEnable();
                        lblDescricao.Text = "DESCRIÇÃO DO SETOR OU SUBSETOR";
                        btnGravar.Enabled = true;
                        txtSubCod.Enabled = false;
                        comStatus.SelectedIndex = 0;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB_SET()\n\nBLOCO.: TabSetor_MET = INCLUIR\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB_SET()\n\nBLOCO.: TabSetor_MET = INCLUIR\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
            #region TRATAMENTO != INCLUIR
            if (txtMESTRE.Text != "INCLUIR")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                //Define a string e o comando
                string StringComandoSELEÇÃO = ("SELECT TOP 1 Descri_SET, Status_SET FROM TabSetor WHERE Sequen_SET LIKE '%0000%' AND LEFT(RIGHT(REPLICATE('0',07) + CAST(CAST(Sequen_SET AS INT) AS VARCHAR),07),3) = @Sequen_SET ORDER BY Sequen_SET");
                SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                ComandoSELEÇÃO.Parameters.Add("@Sequen_SET", SqlDbType.VarChar).Value = txtSetCod.Text;

                try
                {
                    SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();

                    if (Dr.HasRows)
                    {
                        ZerarCampos();

                        #region TRATAMENTO EXCLUIDO
                        if (Dr["Status_SET"].ToString() == "3")
                        {
                            btnGravar.Enabled = false;
                            ZerarCampos();
                            CamposDisable();
                            Retorno.Focus();
                            lblDescricao.Text = "DESCRIÇÃO DO SETOR OU SUBSETOR";
                            txtSubCod.Text = string.Empty;
                            MessageBox.Show("Registro consta como EXCLUIDO!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        #endregion
                        else
                        {
                            ZerarCampos();
                            CamposDisable();
                            txtSubCod.Enabled = true;
                            btnGravar.Enabled = false;
                            string Descri_SET = Dr["Descri_SET"].ToString();


                            lblDescricao.Text = "DESCRIÇÃO DO SETOR OU SUBSETOR (" + Descri_SET + ")";
                        }
                    }
                    else
                    {
                        btnGravar.Enabled = false;
                        ZerarCampos();
                        CamposDisable();
                        Retorno.Focus();
                        lblDescricao.Text = "DESCRIÇÃO DO SETOR OU SUBSETOR";
                        //DESABILITA O TXT SUB
                        txtSubCod.Enabled = false;
                        txtSubCod.Text = string.Empty;
                        MessageBox.Show("Setor inexistente no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB_SET()\n\nBLOCO.: TabSetor_MET != INCLUIR\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB_SET()\n\nBLOCO.: TabSetor_MET != INCLUIR\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
        }
        //BUSCA AS INFORMAÇÕES AO DIGITAR TAB NO CAMPO CÓDIGO
        public void MET_SelecionaCodigoTAB_SUB(TextBox txtMESTRE, TextBox txtSetCod, TextBox txtSubCod, Control Retorno, MethodInvoker ZerarCampos, MethodInvoker CamposEnable, MethodInvoker CamposDisable, Button btnGravar, string LojaLogada, Label lblDescricao, TextBox txtDescri, TextBox txtRespon, TextBox txtLocali, ComboBox comStatus)
        {
            #region TRATAMENTO txtSetCod em BRANCO e > QUE 9999
            #region TRATAMENTO EM BRANCO
            if (txtSubCod.Text == string.Empty)
            {
                txtSubCod.Text = "0000";
            }
            #endregion
            #region TRATAMENTO LIMITE
            if (Convert.ToInt32(txtSubCod.Text) >= 9999)
            {
                txtSubCod.Text = txtSubCod.Text.PadLeft(4, '0');
                Retorno.Focus();
                txtSubCod.SelectAll();
                ZerarCampos();
                CamposDisable();
                btnGravar.Enabled = false;
                MessageBox.Show("LIMITE DE CADASTROS DE SUBSETORES ATINGIDO", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            else
            {
                txtSubCod.Text = txtSubCod.Text.PadLeft(4, '0');
            }
            #endregion

            #region TRATAMENTO INCLUIR
            if (txtMESTRE.Text == "INCLUIR")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string StringComandoSELEÇÃO = "";
                SqlCommand ComandoSELEÇÃO;

                //Define a string e o comando
                if (Convert.ToInt32(txtSubCod.Text) == 0)
                {
                    StringComandoSELEÇÃO = ("SELECT TOP 1 * FROM TabSetor WHERE Sequen_SET LIKE '%0000%' AND LEFT(RIGHT(REPLICATE('0',07) + CAST(CAST(Sequen_SET AS INT) AS VARCHAR),07),3) = @Sequen_SET ORDER BY Sequen_SET");
                    ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                    ComandoSELEÇÃO.Parameters.Add("@Sequen_SET", SqlDbType.VarChar).Value = txtSetCod.Text;
                }
                else
                {
                    StringComandoSELEÇÃO = ("SELECT TOP 1 * FROM TabSetor WHERE Sequen_SET NOT LIKE '%0000%' AND Sequen_SET = @Sequen_SET");
                    ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                    ComandoSELEÇÃO.Parameters.Add("@Sequen_SET", SqlDbType.VarChar).Value = txtSetCod.Text + txtSubCod.Text;
                }

                try
                {
                    SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();

                    if (Dr.HasRows)
                    {
                        ZerarCampos();


                        #region TRATAMENTO EXCLUIDO
                        if (Dr["Status_SET"].ToString() == "3")
                        {
                            btnGravar.Enabled = false;
                            ZerarCampos();
                            CamposDisable();
                            Retorno.Focus();
                            MessageBox.Show("Registro consta como EXCLUIDO!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        #endregion
                        else
                        {
                            txtDescri.Text = Dr["Descri_SET"].ToString();
                            txtRespon.Text = Dr["Respon_SET"].ToString();
                            txtLocali.Text = Dr["Locali_SET"].ToString();
                            #region comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_SET"]) - 1;
                            try
                            {
                                comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_SET"]) - 1;
                            }
                            catch (Exception)
                            {
                                comStatus.SelectedIndex = 0;
                            }
                            #endregion


                            CamposDisable();
                            btnGravar.Enabled = false;
                            Retorno.Focus();
                            txtSubCod.SelectAll();
                            MessageBox.Show("Setor/Subsetor já cadastrado no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);   
                        }
                    }
                    else
                    {
                        ZerarCampos();
                        CamposEnable();
                        btnGravar.Enabled = true;
                        comStatus.SelectedIndex = 0;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB_SUB()\n\nBLOCO.: TabSetor_MET = INCLUIR\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB_SUB()\n\nBLOCO.: TabSetor_MET = INCLUIR\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
            #region TRATAMENTO != INCLUIR
            if (txtMESTRE.Text != "INCLUIR")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string StringComandoSELEÇÃO = "";
                SqlCommand ComandoSELEÇÃO;

                //Define a string e o comando
                if (Convert.ToInt32(txtSubCod.Text) == 0)
                {
                    StringComandoSELEÇÃO = ("SELECT TOP 1 * FROM TabSetor WHERE Sequen_SET LIKE '%0000%' AND LEFT(RIGHT(REPLICATE('0',07) + CAST(CAST(Sequen_SET AS INT) AS VARCHAR),07),3) = @Sequen_SET ORDER BY Sequen_SET");
                    ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                    ComandoSELEÇÃO.Parameters.Add("@Sequen_SET", SqlDbType.VarChar).Value = txtSetCod.Text;
                }
                else
                {
                    StringComandoSELEÇÃO = ("SELECT TOP 1 * FROM TabSetor WHERE Sequen_SET NOT LIKE '%0000%' AND Sequen_SET = @Sequen_SET");
                    ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                    ComandoSELEÇÃO.Parameters.Add("@Sequen_SET", SqlDbType.VarChar).Value = txtSetCod.Text + txtSubCod.Text;
                }



                try
                {
                    SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();

                    if (Dr.HasRows)
                    {
                        ZerarCampos();


                        #region TRATAMENTO EXCLUIDO
                        if (Dr["Status_SET"].ToString() == "3")
                        {
                            btnGravar.Enabled = false;
                            ZerarCampos();
                            CamposDisable();
                            Retorno.Focus();
                            MessageBox.Show("Registro consta como EXCLUIDO!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        #endregion
                        else
                        {
                            txtDescri.Text = Dr["Descri_SET"].ToString();
                            txtRespon.Text = Dr["Respon_SET"].ToString();
                            txtLocali.Text = Dr["Locali_SET"].ToString();
                            #region comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_SET"]) - 1;
                            try
                            {
                                comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_SET"]) - 1;
                            }
                            catch (Exception)
                            {
                                comStatus.SelectedIndex = 0;
                            }
                            #endregion



                            if (txtMESTRE.Text == "SELECT" || txtMESTRE.Text == "CONSULTA")
                            {
                                CamposDisable();
                                btnGravar.Enabled = false;
                                Retorno.Focus();
                                txtSubCod.SelectAll();
                            }
                            else
                            {
                                CamposEnable();
                                btnGravar.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        CamposDisable();
                        btnGravar.Enabled = false;
                        Retorno.Focus();
                        txtSubCod.SelectAll();
                        MessageBox.Show("Subsetor inexistente no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB_SUB()\n\nBLOCO.: TabSetor_MET != INCLUIR\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB_SUB()\n\nBLOCO.: TabSetor_MET != INCLUIR\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
        }


        //SELECIONA O ULTIMO REGISTRO NO BANCO E ADICIONA MAIS 1
        public void MET_SelecionaUltimoRegistroMaisUmSETOR(TextBox txtSetCod, Button btnGravar, string LojaLogada)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT MAX (LEFT(RIGHT(REPLICATE('0',07) + CAST(CAST(Sequen_SET AS INT) AS VARCHAR),07),3) + 1) AS Sequen_SET FROM TabSetor WHERE Sequen_SET LIKE '%0000%'";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string Codigo = Dr["Sequen_SET"].ToString();
                    if (Codigo == string.Empty)
                    {
                        txtSetCod.Text = "001";
                    }
                    else
                    {
                        txtSetCod.Text = Codigo.PadLeft(3, '0');
                    }

                    
                    txtSetCod.SelectAll();
                    Dr.Close();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUltimoRegistroMaisUmSETOR()\n\nBLOCO.: TabSetor_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUltimoRegistroMaisUmSETOR()\n\nBLOCO.: TabSetor_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
            }
            finally
            {
                Conexão.Close();
            }
        }
        //SELECIONA O ULTIMO REGISTRO NO BANCO E ADICIONA MAIS 1
        public void MET_SelecionaUltimoRegistroMaisUmSUB(TextBox txtSubCod, TextBox txtSetCod, Button btnGravar, string LojaLogada)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            string StringComando = "SELECT TOP 1 MAX (RIGHT(Sequen_SET,3) + 1) AS Sequen_SET FROM TabSetor WHERE Sequen_SET NOT LIKE '%0000%' AND LEFT(RIGHT(REPLICATE('0',07) + CAST(CAST(Sequen_SET AS INT) AS VARCHAR),07),3) = @Sequen_SET ORDER BY Sequen_SET";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen_SET", SqlDbType.Int).Value = txtSetCod.Text;


            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string Codigo = Dr["Sequen_SET"].ToString();
                    if (Codigo == string.Empty)
                    {
                        txtSubCod.Text = "0001";
                    }
                    else
                    {
                        txtSubCod.Text = Codigo.PadLeft(4, '0');
                    }

                    txtSubCod.SelectAll();
                    Dr.Close();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUltimoRegistroMaisUmSUB()\n\nBLOCO.: TabSetor_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUltimoRegistroMaisUmSUB()\n\nBLOCO.: TabSetor_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
            }
            finally
            {
                Conexão.Close();
            }
        }


        //VERIFICA SE O SETOR/SUBSETOR JÁ EXISTE
        public void MET_VerificaSetorExiste(TextBox txtMESTRE, TextBox txtDescri, Control Retorno, TextBox txtSetCod, TextBox txtSubCod, Button btnGravar, MethodInvoker ZerarCampos, MethodInvoker CamposDisable)
        {
            if (txtMESTRE.Text == "INCLUIR")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string StringComando = "SELECT Sequen_SET FROM TabSetor WHERE Descri_SET = @Descri_SET";
                SqlCommand Comando = new SqlCommand(StringComando, Conexão);

                Comando.Parameters.Add("@Descri_SET", SqlDbType.VarChar).Value = txtDescri.Text;

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        MessageBox.Show("REGISTRO EXISTENTE NO BANCO DE DADOS. CÓDIGO (" + Dr["Sequen_SET"].ToString().PadLeft(7, '0') + ")", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Retorno.Focus();
                        txtSetCod.SelectAll();
                        btnGravar.Enabled = false;
                        ZerarCampos();
                        CamposDisable();
                        txtSubCod.Text = string.Empty;
                        txtSubCod.Enabled = false;
                        
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_VerificaSetorExiste()\n\nBLOCO.: TabSetor_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_VerificaSetorExiste()\n\nBLOCO.: TabSetor_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }
    }
}
