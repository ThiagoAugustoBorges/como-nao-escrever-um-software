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

namespace TabRotas
{
    internal class TabRotas_MET
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

        //SELECIONA O ULTIMO REGISTRO NO BANCO E ADICIONA MAIS 1
        public void MET_SelecionaUltimoRegistroMaisUm(TextBox txtCodigo, Button btnGravar, string LojaLogada)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT MAX (Sequen_ROT + 1) as Sequen_ROT FROM TabRotas";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string Codigo = Dr["Sequen_ROT"].ToString();
                    if (Codigo == string.Empty)
                    {
                        txtCodigo.Text = "000001";
                    }
                    else
                    {
                        txtCodigo.Text = Codigo.PadLeft(6, '0');
                    }

                    txtCodigo.SelectAll();
                    Dr.Close();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUltimoRegistroMaisUm()\n\nBLOCO.: TabRotas_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUltimoRegistroMaisUm()\n\nBLOCO.: TabRotas_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
            }
            finally
            {
                Conexão.Close();
            }
        }

        //BUSCA AS INFORMAÇÕES AO DIGITAR TAB NO CAMPO CÓDIGO
        public void MET_SelecionaCodigoTAB(TextBox txtMESTRE, TextBox txtCodigo, Panel panUpAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Button btnGravar, MethodInvoker CamposEnable, TextBox txtDescri, string LojaLogada, ComboBox comStatus)
        {
            #region TRATAMENTO txtCodigo em BRANCO e > QUE 999999
            #region TRATAMENTO EM BRANCO
            if (txtCodigo.Text == string.Empty)
            {
                txtCodigo.Text = "000000";
            }
            #endregion
            #region TRATAMENTO = 0
            if (Convert.ToInt32(txtCodigo.Text) == 0)
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');
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
            if (Convert.ToInt32(txtCodigo.Text) > 999998)
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');
                panUpAb1.Focus();
                txtCodigo.SelectAll();
                ZerarCampos();
                CamposDisable();
                btnGravar.Enabled = false;
                MessageBox.Show("LIMITE DE CADASTROS DE ROTAS ATINGIDO", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            else
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');
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
                string StringComandoSELEÇÃO = ("SELECT * FROM TabRotas WHERE Sequen_ROT = @Sequen_ROT");
                SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                ComandoSELEÇÃO.Parameters.Add("@Sequen_ROT", SqlDbType.VarChar).Value = txtCodigo.Text;

                try
                {
                    SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        ZerarCampos();

                        if (Dr["Status_ROT"].ToString() == "3")
                        {
                            btnGravar.Enabled = false;
                            ZerarCampos();
                            CamposDisable();
                            panUpAb1.Focus();
                            MessageBox.Show("Registro consta como EXCLUIDO!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            txtDescri.Text = Dr["Descri_ROT"].ToString();
                            #region comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_ROT"]);
                            try
                            {
                                comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_ROT"]);
                            }
                            catch (Exception)
                            {
                                comStatus.SelectedIndex = 2;
                            }
                            #endregion

                            //TRATAMENTO PARA ROTA JÁ EXISTENTE
                            panUpAb1.Focus();
                            txtCodigo.SelectAll();
                            CamposDisable();
                            btnGravar.Enabled = false;
                            MessageBox.Show("Registro já cadastrado no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        ZerarCampos();
                        CamposEnable();
                        btnGravar.Enabled = true;

                        comStatus.SelectedIndex = 1;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: TabRotas_MET = INCLUIR\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: TabRotas_MET = INCLUIR\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string StringComandoSELEÇÃO = ("SELECT * FROM TabRotas WHERE Sequen_ROT = @Sequen_ROT");
                SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                ComandoSELEÇÃO.Parameters.Add("@Sequen_ROT", SqlDbType.VarChar).Value = txtCodigo.Text;

                try
                {
                    SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        ZerarCampos();

                        if (Dr["Status_ROT"].ToString() == "3")
                        {
                            btnGravar.Enabled = false;
                            ZerarCampos();
                            CamposDisable();
                            panUpAb1.Focus();
                            MessageBox.Show("Registro consta como EXCLUIDO!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {

                            txtDescri.Text = Dr["Descri_ROT"].ToString();
                            #region comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_ROT"]);
                            try
                            {
                                comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_ROT"]);
                            }
                            catch (Exception)
                            {
                                comStatus.SelectedIndex = 2;
                            }
                            #endregion


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
                    }
                    else
                    {
                        CamposDisable();
                        btnGravar.Enabled = false;
                        panUpAb1.Focus();
                        txtCodigo.SelectAll();
                        MessageBox.Show("Registro inexistente no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: TabRotas_MET != INCLUIR\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: TabRotas_MET != INCLUIR\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
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
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_CapCaminhoSALV()\n\nBLOCO = TabRotas_MET - CAPTURA CAMINHO DOS RELATÓRIOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_CapCaminhoSALV()\n\nBLOCO = TabRotas_MET - CAPTURA CAMINHO DOS RELATÓRIOS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
            return txtCaminhoRel.Text;
        }

        //VERIFICA SE MENSAGEM JA EXISTE
        public void MET_VerificaRotaExiste(TextBox txtMESTRE, TextBox txtCodigo, Panel panUpAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Button btnGravar, MethodInvoker CamposEnable, TextBox txtDescri, string LojaLogada)
        {
            if (txtMESTRE.Text == "INCLUIR")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string StringComando = "SELECT Sequen_ROT FROM TabRotas WHERE Descri_ROT = @Descri_ROT";
                SqlCommand Comando = new SqlCommand(StringComando, Conexão);

                Comando.Parameters.Add("@Descri_ROT", SqlDbType.VarChar).Value = txtDescri.Text;

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        panUpAb1.Focus();
                        txtCodigo.SelectAll();
                        btnGravar.Enabled = false;
                        txtDescri.Text = string.Empty;
                        MessageBox.Show("REGISTRO EXISTENTE NO BANCO DE DADOS. CÓDIGO (" + Dr["Sequen_ROT"].ToString().PadLeft(6, '0') + ")", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_VerificaRotaExiste()\n\nBLOCO.: TabRotas_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_VerificaRotaExiste()\n\nBLOCO.: TabRotas_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
