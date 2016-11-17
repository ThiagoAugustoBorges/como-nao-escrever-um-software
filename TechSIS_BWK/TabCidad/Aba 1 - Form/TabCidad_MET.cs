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

namespace TabCidad
{
    internal class TabCidad_MET
    {
        //Define que no formulário pode conter apenas números
        public void MET_ApenasNúmeros(KeyPressEventArgs KeyPress)
        {
            if (!Char.IsDigit(KeyPress.KeyChar) && KeyPress.KeyChar != (char)8)
            {
                KeyPress.Handled = true;
                return;
            }
        }

        //Seleciona o ultimo registro do banco e adiciona 1
        public void MET_SelecionaUltimoRegistroMaisUm(TextBox txtCodigo, Button btnGravar)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT MAX (Sequen_CID + 1) AS Sequen_CID FROM TabCidad";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    txtCodigo.Text = Dr["Sequen_CID"].ToString().PadLeft(6, '0');
                    txtCodigo.SelectAll();
                    Dr.Close();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUltimoRegistroMaisUm()\n\nBLOCO.: TabCidad_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUltimoRegistroMaisUm()\n\nBLOCO.: TabCidad_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
            }
            finally
            {
                Conexão.Close();
            }
        }

        //Seleciona os registros no TAB
        public void MET_SelecionarCodigoDigitarTAB(TextBox txtMESTRE, TextBox txtCodigo, Panel panCodigoAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Button btnGravar, MethodInvoker CamposEnable, TextBox txtDescri, TextBox txtPaisCod, TextBox txtPaisDesc, TextBox txtIbgeUF, ComboBox comUF, TextBox txtUFDesc, TextBox txtIbgeMuCod, TextBox txtIbgeMuDesc, TextBox txtIbgeEstadual, MaskedTextBox mtbCep1, MaskedTextBox mtbCep2, ComboBox comStatus)
        {
            #region TRATAMENTO txtCodigo em BRANCO & txtCodigo > 999999
            if (txtCodigo.Text == string.Empty)
            {
                txtCodigo.Text = "000000";
            }
            if (Convert.ToInt32(txtCodigo.Text) == 0)
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');
                panCodigoAb1.Focus();
                txtCodigo.SelectAll();
                ZerarCampos();
                CamposDisable();
                btnGravar.Enabled = false;
                MessageBox.Show("Campo (Código) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (Convert.ToInt32(txtCodigo.Text) >= 999999)
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');
                panCodigoAb1.Focus();
                txtCodigo.SelectAll();
                ZerarCampos();
                CamposDisable();
                btnGravar.Enabled = false;
                MessageBox.Show("LIMITE DE CADASTROS DE CIDADES ATINGIDO", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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

                string StringComando = "SELECT Sequen_CID,Descri_CID,UfFede_CID,UfInde_CID,UfDesc_CID,PaisCi_CID,Descri_PAI,IbgeCi_CID,IbgeMu_CID,Descri_MUN,IbgeEs_CID,CepCi1_CID,CepCi2_CID,Status_CID FROM TabCidad INNER JOIN TabPaise ON TabCidad.PaisCi_CID = TabPaise.Codigo_PAI INNER JOIN TabCidMu ON TabCidad.IbgeMu_CID = TabCidMu.Codigo_MUN WHERE Sequen_CID = @Sequen";
                SqlCommand Comando = new SqlCommand(StringComando, Conexão);
                Comando.Parameters.Add("@Sequen", SqlDbType.VarChar).Value = txtCodigo.Text;

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {

                        int STATUS = Convert.ToInt32(Dr["Status_CID"]);


                        if (STATUS == 3)
                        {
                            btnGravar.Enabled = false;
                            panCodigoAb1.Focus();
                            CamposDisable();
                            ZerarCampos();
                            MessageBox.Show("Cidade consta como EXCLUIDA. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                        else
                        {
                            btnGravar.Enabled = false;
                            panCodigoAb1.Focus();
                            CamposDisable();

                            txtDescri.Text = Dr["Descri_CID"].ToString();
                            txtPaisCod.Text = Dr["PaisCi_CID"].ToString();
                            txtPaisDesc.Text = Dr["Descri_PAI"].ToString();
                            txtIbgeMuCod.Text = Dr["IbgeMu_CID"].ToString();
                            txtIbgeMuDesc.Text = Dr["Descri_MUN"].ToString();
                            txtIbgeEstadual.Text = Dr["IbgeEs_CID"].ToString();
                            mtbCep1.Text = Dr["CepCi1_CID"].ToString();
                            mtbCep2.Text = Dr["CepCi2_CID"].ToString();

                            comUF.SelectedIndex = Convert.ToInt32(Dr["UfInde_CID"]);


                            if (STATUS > 3 || STATUS <= 0)
                            {
                                STATUS = 2;
                            }
                            comStatus.SelectedIndex = STATUS;

                            #region TRATAMENTOS PARA CAMPOS INT = 0
                            if (Convert.ToInt32(Dr["CepCi1_CID"]) == 0)
                            {
                                mtbCep1.Text = string.Empty;
                            }
                            if (Convert.ToInt32(Dr["CepCi2_CID"]) == 0)
                            {
                                mtbCep2.Text = string.Empty;
                            }
                            if (Convert.ToInt32(Dr["IbgeEs_CID"]) == 0)
                            {
                                txtIbgeEstadual.Text = string.Empty;
                            }
                            #endregion

                            MessageBox.Show("Registro já cadastrado no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        btnGravar.Enabled = true;
                        ZerarCampos();
                        CamposEnable();
                        comStatus.SelectedIndex = 1;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: CLASSE TabCidad_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: CLASSE TabCidad_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
            #region OUTROS
            else
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string StringComando = "SELECT Sequen_CID,Descri_CID,UfFede_CID,UfInde_CID,UfDesc_CID,PaisCi_CID,Descri_PAI,IbgeCi_CID,IbgeMu_CID,Descri_MUN,IbgeEs_CID,CepCi1_CID,CepCi2_CID,Status_CID FROM TabCidad INNER JOIN TabPaise ON TabCidad.PaisCi_CID = TabPaise.Codigo_PAI INNER JOIN TabCidMu ON TabCidad.IbgeMu_CID = TabCidMu.Codigo_MUN WHERE Sequen_CID = @Sequen";
                SqlCommand Comando = new SqlCommand(StringComando, Conexão);
                Comando.Parameters.Add("@Sequen", SqlDbType.VarChar).Value = txtCodigo.Text;

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {

                        int STATUS = Convert.ToInt32(Dr["Status_CID"]);


                        if (STATUS == 3)
                        {
                            btnGravar.Enabled = false;
                            panCodigoAb1.Focus();
                            CamposDisable();
                            ZerarCampos();
                            MessageBox.Show("Cidade consta como EXCLUIDA. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                        else
                        {
                            btnGravar.Enabled = true;
                            CamposEnable();

                            txtDescri.Text = Dr["Descri_CID"].ToString();
                            txtPaisCod.Text = Dr["PaisCi_CID"].ToString();
                            txtPaisDesc.Text = Dr["Descri_PAI"].ToString();
                            txtIbgeMuCod.Text = Dr["IbgeMu_CID"].ToString();
                            txtIbgeMuDesc.Text = Dr["Descri_MUN"].ToString();
                            txtIbgeEstadual.Text = Dr["IbgeEs_CID"].ToString();
                            mtbCep1.Text = Dr["CepCi1_CID"].ToString();
                            mtbCep2.Text = Dr["CepCi2_CID"].ToString();

                            comUF.SelectedIndex = Convert.ToInt32(Dr["UfInde_CID"]);


                            if (STATUS > 3 || STATUS <= 0)
                            {
                                STATUS = 2;
                            }
                            comStatus.SelectedIndex = STATUS;

                            #region TRATAMENTOS PARA CAMPOS INT = 0
                            if (Convert.ToInt32(Dr["CepCi1_CID"]) == 0)
                            {
                                mtbCep1.Text = string.Empty;
                            }
                            if (Convert.ToInt32(Dr["CepCi2_CID"]) == 0)
                            {
                                mtbCep2.Text = string.Empty;
                            }
                            if (Convert.ToInt32(Dr["IbgeEs_CID"]) == 0)
                            {
                                txtIbgeEstadual.Text = string.Empty;
                            }
                            #endregion


                            if (txtMESTRE.Text == "SELECT" || txtMESTRE.Text == "CONSULTA")
                            {
                                btnGravar.Enabled = false;
                                CamposDisable();
                                panCodigoAb1.Focus();
                            }
                        }
                    }
                    else
                    {
                        btnGravar.Enabled = false;
                        ZerarCampos();
                        CamposDisable();
                        panCodigoAb1.Focus();
                        MessageBox.Show("Registro inexistente no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: CLASSE TabCidad_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: CLASSE TabCidad_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
        }

        //Seleciona o PAIS no TAB
        public void MET_SelecionaPAIS(TextBox txtPaisCod, TextBox txtPaisDesc, Control Retorno, ComboBox comUF, ComboBox comStatus, TextBox txtIbgeMuCod, TextBox txtIbgeMuDesc, TextBox txtIbgeEstadual)
        {
            txtPaisCod.Text = txtPaisCod.Text.PadLeft(6, '0');

            if (Convert.ToInt32(txtPaisCod.Text) < 1)
            {
                Retorno.Focus();
                txtPaisCod.SelectAll();
                MessageBox.Show("Campo (Código País) preenchido incorretamente.", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Descri_PAI FROM TabPaise WHERE Codigo_PAI = @CodigoPAI";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@CodigoPAI", SqlDbType.Int).Value = txtPaisCod.Text;


            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();

                if (Dr.HasRows)
                {
                    txtPaisDesc.Text = Dr["Descri_PAI"].ToString();

                    if (Convert.ToInt32(txtPaisCod.Text) != 1058)
                    {
                        comUF.SelectedIndex = 27;
                        txtIbgeMuCod.Text = "0";
                        txtIbgeMuDesc.Text = "EXTERIOR";
                        txtIbgeEstadual.Select();
                        SendKeys.Send("{HOME}");
                        SendKeys.Send("+{END}");
                    }
                }
                else
                {
                    MessageBox.Show("Nenhuma informação encontrada no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPaisDesc.Text = string.Empty;
                    Retorno.Focus(); txtPaisCod.SelectAll();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelecionaPAIS()\n\nBLOCO = TabCidad_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelecionaPAIS()\n\nBLOCO = TabCidad_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //Popula o campo Código Municipal e avisa se codigo municipal for exterior e o pais for Brasil
        public void MET_SelecionaMUNI(TextBox txtIbgeMuCod, TextBox txtIbgeMuDesc, TextBox txtPaisCod, Control Retorno)
        {
            txtIbgeMuCod.Text = txtIbgeMuCod.Text.PadLeft(6, '0');

            if (Convert.ToInt32(txtIbgeMuCod.Text) < 0)
            {
                Retorno.Focus();
                txtIbgeMuCod.SelectAll();
                MessageBox.Show("Campo (Código País) preenchido incorretamente.", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Descri_MUN FROM TabCidMu WHERE Codigo_MUN = @CodigoMUN";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@CodigoMUN", SqlDbType.Int).Value = txtIbgeMuCod.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();

                if (Dr.HasRows)
                {
                    txtIbgeMuDesc.Text = Dr["Descri_MUN"].ToString();

                    if (Dr["Descri_MUN"].ToString() == "EXTERIOR" && Convert.ToInt32(txtPaisCod.Text) == 1058)
                    {
                        MessageBox.Show("Você está selecionando um código EXTERIOR com o País sendo BRASIL.\nEsta condição está errada, porém o sistema irá deixar você continuar.\n\nSe seu tipo de emissão de nota for NFe, ela não será emitida se houver um Cliente/Fornecedor cadastrado com esta cidade.", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Nenhuma informação encontrada no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Retorno.Select(); txtIbgeMuCod.SelectAll();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelecionaMUNI()\n\nBLOCO = TabCidad_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelecionaMUNI()\n\nBLOCO = TabCidad_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //Verifica se CIDADE já existe no banco de dados
        public bool MET_VerificaCidadeEXISTE(TextBox txtDescri, TextBox txtCodigo, TextBox txtMESTRE)
        {
            if (txtMESTRE.Text == "INCLUIR")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string StringComando = "SELECT Sequen_CID FROM TabCidad WHERE Descri_CID = @Descri";
                SqlCommand Comando = new SqlCommand(StringComando, Conexão);
                Comando.Parameters.Add("@Descri", SqlDbType.VarChar).Value = txtDescri.Text;

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        string CodigoCidade = Dr["Sequen_CID"].ToString();
                        DialogResult JaCadastrado = MessageBox.Show("Atenção: Cidade já cadastrada no banco de dados do sistema.\nDESEJA CONTINUAR?                         Código da Cidade.: " + CodigoCidade.PadLeft(6, '0'), "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (JaCadastrado == DialogResult.Yes)
                        {
                            return false;
                        }
                        if (JaCadastrado == DialogResult.No)
                        {
                            txtCodigo.Select(); txtDescri.SelectAll();
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerificaCidadeEXISTE()\n\nBLOCO = TabCidad_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_VerificaCidadeEXISTE()\n\nBLOCO = TabCidad_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }

                return false;
            }
            return false;
        }

        //Popula os campos a partir da descrição da Cidade
        public void MET_PopulaCamposCidade(TextBox txtDescri, TextBox txtIbgeMuCod, ComboBox comUF, TextBox txtPaisCod, TextBox txtPaisDesc, TextBox txtIbgeMuDesc, TextBox txtIbgeEstadual, MaskedTextBox mtbCep1, TextBox txtMESTRE)
        {
            if (txtMESTRE.Text == "INCLUIR")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string StringComando = "SELECT * FROM TabCidMu WHERE Descri_MUN = @Descri";
                SqlCommand Comando = new SqlCommand(StringComando, Conexão);
                Comando.Parameters.Add("@Descri", SqlDbType.VarChar).Value = txtDescri.Text;

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        txtIbgeMuCod.Text = Dr["Codigo_MUN"].ToString();
                        txtIbgeMuDesc.Text = Dr["Descri_MUN"].ToString();
                        string UF = Dr["UfFede_MUN"].ToString();

                        #region TRATAMENTO UF
                        switch (UF)
                        {
                            case "AC":
                                comUF.SelectedIndex = 0;
                                break;
                            case "AL":
                                comUF.SelectedIndex = 1;
                                break;
                            case "AM":
                                comUF.SelectedIndex = 2;
                                break;
                            case "AP":
                                comUF.SelectedIndex = 3;
                                break;
                            case "BA":
                                comUF.SelectedIndex = 4;
                                break;
                            case "CE":
                                comUF.SelectedIndex = 5;
                                break;
                            case "DF":
                                comUF.SelectedIndex = 6;
                                break;
                            case "ES":
                                comUF.SelectedIndex = 7;
                                break;
                            case "GO":
                                comUF.SelectedIndex = 8;
                                break;
                            case "MA":
                                comUF.SelectedIndex = 9;
                                break;
                            case "MG":
                                comUF.SelectedIndex = 10;
                                break;
                            case "MS":
                                comUF.SelectedIndex = 11;
                                break;
                            case "MT":
                                comUF.SelectedIndex = 12;
                                break;
                            case "PA":
                                comUF.SelectedIndex = 13;
                                break;
                            case "PB":
                                comUF.SelectedIndex = 14;
                                break;
                            case "PE":
                                comUF.SelectedIndex = 15;
                                break;
                            case "PI":
                                comUF.SelectedIndex = 16;
                                break;
                            case "PR":
                                comUF.SelectedIndex = 17;
                                break;
                            case "RJ":
                                comUF.SelectedIndex = 18;
                                break;
                            case "RN":
                                comUF.SelectedIndex = 19;
                                break;
                            case "RO":
                                comUF.SelectedIndex = 20;
                                break;
                            case "RR":
                                comUF.SelectedIndex = 21;
                                break;
                            case "RS":
                                comUF.SelectedIndex = 22;
                                break;
                            case "SC":
                                comUF.SelectedIndex = 23;
                                break;
                            case "SE":
                                comUF.SelectedIndex = 24;
                                break;
                            case "SP":
                                comUF.SelectedIndex = 25;
                                break;
                            case "TO":
                                comUF.SelectedIndex = 26;
                                break;

                        }
                        #endregion

                        txtPaisCod.Text = "001058";
                        txtPaisDesc.Text = "BRASIL";

                        txtIbgeEstadual.Select();
                        SendKeys.Send("{HOME}");
                        SendKeys.Send("+{END}");
                    }
                    else
                    {
                        MessageBox.Show("Informações de UF e IBGE do Município não foram encontradas.\n\nVerifique se você digitou o nome da Cidade corretamente e sem acentuação alguma. Se estiver digitado corretamente pode ocorrer da cidade ser na verdade um distrito, se este for o caso, coloque na descrição o nome do distrito, e no Código IBGE coloque o código referente a cidade sede.\nEsta mensagem também irá aparecer se a cidade atual que está sendo cadastrada for de outro País, neste caso ignore e continue.", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        comUF.SelectedIndex = -1;
                        txtIbgeMuCod.Text = string.Empty;
                        txtIbgeMuDesc.Text = string.Empty;

                        txtPaisCod.Text = "001058";
                        txtPaisDesc.Text = "BRASIL";

                        txtPaisCod.Select();
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_PopulaCamposCidade()\n\nBLOCO = TabCidad_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_PopulaCamposCidade()\n\nBLOCO = TabCidad_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
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
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_CapCaminhoSALV()\n\nBLOCO = TabCidad_MET - CAPTURA CAMINHO DOS RELATÓRIOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_CapCaminhoSALV()\n\nBLOCO = TabCidad_MET - CAPTURA CAMINHO DOS RELATÓRIOS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
            return txtCaminhoRel.Text;
        }


        //Define que no campo pode haver apenas números
        #region Apenas Números
        public void ApenasNúmeros(KeyPressEventArgs KeyPress)
        {
            if (!Char.IsDigit(KeyPress.KeyChar) && KeyPress.KeyChar != (char)8)
            {
                KeyPress.Handled = true;
                return;
            }
        }
        #endregion
    }
}
