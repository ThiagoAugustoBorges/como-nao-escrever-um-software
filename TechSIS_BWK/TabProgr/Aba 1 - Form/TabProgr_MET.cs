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

namespace TabProgr
{
    internal class TabProgr_MET
    {
        //Seleciona o registro ao digitar TAB
        public void MET_SelecionaDigitarTAB(TextBox txtMESTRE, TextBox txtCodigo, TextBox txtDescricao, ComboBox comStatus, ComboBox comModulo, TextBox txtDataCadastro, Button btnGravar, MethodInvoker ZerarCampos, MethodInvoker CamposEnable, MethodInvoker CamposDisable, Control RetornoTXTCodigo)
        {
            #region INCLUIR
            if (txtMESTRE.Text == "INCLUIR")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                //Define a string e o comando
                string StringComandoSELEÇÃO = ("SELECT TOP 1 * FROM TabProgr WHERE Sequen_PGR = @Sequen");
                SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                ComandoSELEÇÃO.Parameters.Add("@Sequen", SqlDbType.VarChar).Value = txtCodigo.Text;


                try
                {
                    SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();

                    if (Dr.HasRows)
                    {
                        if (Dr["Status_PGR"].ToString() == "3")
                        {
                            btnGravar.Enabled = false;
                            ZerarCampos();
                            CamposDisable();
                            RetornoTXTCodigo.Focus();
                            MessageBox.Show("Registro consta como EXCLUIDO!", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            txtCodigo.Text = Dr["Sequen_PGR"].ToString();
                            txtDescricao.Text = Dr["Descri_PGR"].ToString();
                            DateTime DT = Convert.ToDateTime(txtDataCadastro.Text = Dr["DtCada_PGR"].ToString());
                            txtDataCadastro.Text = DT.ToString("dd/MM/yyyy");
                            comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_PGR"]);
                            comModulo.SelectedIndex = Convert.ToInt32(Dr["Modulo_PGR"]);

                            btnGravar.Enabled = false;
                            MessageBox.Show("Registro (" + txtCodigo.Text + ") já cadastrado", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CamposDisable();
                            RetornoTXTCodigo.Focus(); txtCodigo.SelectAll();
                        }
                    }
                    else
                    {
                        ZerarCampos();
                        CamposEnable();
                        txtDataCadastro.Text = DateTime.Today.ToString("dd/MM/yyyy");
                        comModulo.SelectedIndex = 1;
                        comStatus.SelectedIndex = 1;
                        btnGravar.Enabled = true;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaDigitarTAB()\n\nBLOCO.: 1 = INCLUIR\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaDigitarTAB()\n\nBLOCO.: 1 = INCLUIR\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
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
                string StringComandoSELEÇÃO = ("SELECT TOP 1 * FROM TabProgr WHERE Sequen_PGR = @Sequen");
                SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                ComandoSELEÇÃO.Parameters.Add("@Sequen", SqlDbType.VarChar).Value = txtCodigo.Text;


                try
                {
                    SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();

                    if (Dr.HasRows)
                    {
                        if (Dr["Status_PGR"].ToString() == "3")
                        {
                            btnGravar.Enabled = false;
                            ZerarCampos();
                            CamposDisable();
                            MessageBox.Show("Registro consta como EXCLUIDO!", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RetornoTXTCodigo.Focus(); txtCodigo.SelectAll();
                        }
                        else
                        {
                            txtCodigo.Text = Dr["Sequen_PGR"].ToString();
                            txtDescricao.Text = Dr["Descri_PGR"].ToString();
                            DateTime DT = Convert.ToDateTime(txtDataCadastro.Text = Dr["DtCada_PGR"].ToString());
                            txtDataCadastro.Text = DT.ToString("dd/MM/yyyy");
                            comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_PGR"]);
                            comModulo.SelectedIndex = Convert.ToInt32(Dr["Modulo_PGR"]);

                            if (txtMESTRE.Text == "SELECT" || txtMESTRE.Text == "CONSULTA")
                            {
                                CamposDisable();
                                btnGravar.Enabled = false;
                                RetornoTXTCodigo.Focus(); txtCodigo.SelectAll();
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
                        ZerarCampos();
                        btnGravar.Enabled = false;
                        CamposDisable();
                        MessageBox.Show("Registro (" + txtCodigo.Text + ") inexistente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RetornoTXTCodigo.Focus(); txtCodigo.SelectAll();
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaDigitarTAB()\n\nBLOCO.: 2 != INCLUIR\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaDigitarTAB()\n\nBLOCO.: 2 != INCLUIR\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
        }

        //Verifica se o código do programa é legal
        public bool MET_CodigoLoyou(TextBox txtCodigo, Control Retorno)
        {
            txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');
            if (Convert.ToInt32(txtCodigo.Text) < 10000)
            {
                MessageBox.Show("Código de programa informado é inválido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Retorno.Focus(); txtCodigo.SelectAll();
                return true;
            }

            return false;
        }

        //Seleciona o número de programas cadastrados em TabProgr
        public void MET_NúmeroDeProgramas(TextBox txtTotalProgramas)
        {
            //Abre a conexão
            StreamReader StringConexãos = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexãos.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComandoSELEÇÃO = ("SELECT COUNT (*) FROM TabProgr");
            SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);

            try
            {
                SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();
                txtTotalProgramas.Text = Dr[0].ToString();
                txtTotalProgramas.Text = txtTotalProgramas.Text.PadLeft(6, '0');

                Dr.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_NúmeroDeProgramas()\n\nException.: " + Ex.Message, "TechSIS Erro Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //Define que no formulário pode conter apenas números
        public void MET_ApenasNúmeros(KeyPressEventArgs KeyPress)
        {
            if (!Char.IsDigit(KeyPress.KeyChar) && KeyPress.KeyChar != (char)8)
            {
                KeyPress.Handled = true;
                return;
            }
        }

        //Captura o caminho de salvamento
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
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_CapCaminhoSALV()\n\nBLOCO = TabProgr_MET - CAPTURA CAMINHO DOS RELATÓRIOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_CapCaminhoSALV()\n\nBLOCO = TabProgr_MET - CAPTURA CAMINHO DOS RELATÓRIOS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
