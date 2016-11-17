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
    internal class TabUsuar_MET
    {
        //Apresenta as informações do usuário ao digitar TAB no campo Código
        public void MET_SelecionaCodigoTAB(TextBox txtMESTRE, TextBox txtCodigo, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Panel panCodigoAb1, TextBox txtDescri, ComboBox comPermissao, TextBox txtEmailSuporte, TextBox txtMsn, TextBox txtEmailContato, TextBox txtSkype, TextBox txtEmpreCod, TextBox txtEmpreDesc, TextBox txtApelido, TextBox txtEmpresa, TextBox txtEmpreDescDown, TextBox txtCaixa, DataGridView Dgv_Empresas, ComboBox comStatus, Button btnGravar, MethodInvoker CamposEnable)
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
                MessageBox.Show("LIMITE DE CADASTROS DE USUÁRIOS ATINGIDO", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');
            }
            #endregion

            #region TRATAMENTO PARA USUÁRIO MASTER
            if (Convert.ToInt32(txtCodigo.Text) == 1)
            {
                panCodigoAb1.Focus();
                txtCodigo.SelectAll();
                ZerarCampos();
                CamposDisable();
                btnGravar.Enabled = false;
                Dgv_Empresas.Rows.Clear();
                MessageBox.Show("Número reservado para o usuário MASTER. Métodos bloqueados", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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
                string StringComandoSELEÇÃO = ("SELECT TOP 1 * FROM TabUsuar INNER JOIN TabEmpre ON TabUsuar.CodLoj_USU = TabEmpre.Sequen_EMP WHERE Sequen_USU = @Sequen");
                SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                ComandoSELEÇÃO.Parameters.Add("@Sequen", SqlDbType.VarChar).Value = txtCodigo.Text;

                try
                {
                    SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();

                    if (Dr.HasRows)
                    {
                        if (Dr["Status_USU"].ToString() == "3")
                        {
                            panCodigoAb1.Focus();
                            txtCodigo.SelectAll();
                            ZerarCampos();
                            CamposDisable();
                            btnGravar.Enabled = false;
                            Dgv_Empresas.Rows.Clear();
                            MessageBox.Show("Usuário consta como EXCLUIDO. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            txtDescri.Text = Dr["Descri_USU"].ToString();
                            comPermissao.SelectedIndex = Convert.ToInt32(Dr["Tipo01_USU"]);
                            txtEmailSuporte.Text = Dr["Emai01_USU"].ToString();
                            txtMsn.Text = Dr["Mesg01_USU"].ToString();
                            txtEmailContato.Text = Dr["Emai02_USU"].ToString();
                            txtSkype.Text = Dr["Skype1_USU"].ToString();
                            txtEmpreCod.Text = Dr["CodLoj_USU"].ToString().PadLeft(6, '0');
                            txtEmpreDesc.Text = Dr["Descri_EMP"].ToString();
                            txtApelido.Text = Dr["Apelid_USU"].ToString();
                            comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_USU"]);

                            //POPULA O DATAGRIDVIEW
                            MET_SelecionaCodigoTAB_DGV(txtCodigo, Dgv_Empresas, btnGravar);

                            panCodigoAb1.Focus();
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
                        comStatus.SelectedIndex = 1;
                        comPermissao.SelectedIndex = 1;
                        btnGravar.Enabled = true;
                        Dgv_Empresas.Rows.Clear();
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: CLASSE TabUsuar_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: CLASSE TabUsuar_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string StringComandoSELEÇÃO = ("SELECT TOP 1 * FROM TabUsuar INNER JOIN TabEmpre ON TabUsuar.CodLoj_USU = TabEmpre.Sequen_EMP WHERE Sequen_USU = @Sequen");
                SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                ComandoSELEÇÃO.Parameters.Add("@Sequen", SqlDbType.VarChar).Value = txtCodigo.Text;

                try
                {
                    SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();

                    if (Dr.HasRows)
                    {
                        if (Dr["Status_USU"].ToString() == "3")
                        {
                            panCodigoAb1.Focus();
                            txtCodigo.SelectAll();
                            ZerarCampos();
                            CamposDisable();
                            btnGravar.Enabled = false;
                            Dgv_Empresas.Rows.Clear();
                            MessageBox.Show("Usuário consta como EXCLUIDO. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            txtDescri.Text = Dr["Descri_USU"].ToString();
                            comPermissao.SelectedIndex = Convert.ToInt32(Dr["Tipo01_USU"]);
                            txtEmailSuporte.Text = Dr["Emai01_USU"].ToString();
                            txtMsn.Text = Dr["Mesg01_USU"].ToString();
                            txtEmailContato.Text = Dr["Emai02_USU"].ToString();
                            txtSkype.Text = Dr["Skype1_USU"].ToString();
                            txtEmpreCod.Text = Dr["CodLoj_USU"].ToString().PadLeft(6, '0');
                            txtEmpreDesc.Text = Dr["Descri_EMP"].ToString();
                            txtApelido.Text = Dr["Apelid_USU"].ToString();
                            comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_USU"]);

                            //POPULA O DATAGRIDVIEW
                            MET_SelecionaCodigoTAB_DGV(txtCodigo, Dgv_Empresas, btnGravar);

                            if (txtMESTRE.Text == "SELECT" || txtMESTRE.Text == "CONSULTA")
                            {
                                CamposDisable();
                                btnGravar.Enabled = false;
                                panCodigoAb1.Focus();
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
                        ZerarCampos();
                        CamposDisable();
                        btnGravar.Enabled = false;
                        Dgv_Empresas.Rows.Clear();
                        panCodigoAb1.Focus();
                        txtCodigo.SelectAll();
                        MessageBox.Show("Registro inexistente no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: CLASSE TabUsuar_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: CLASSE TabUsuar_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                }
                finally
                {
                    Conexão.Close();
                }
            #endregion
            }
        }

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

        //Popula o datagrid com informações dos textboxs
        private bool MET_VerificaNumero(int Caixa, int Loja, DataGridView Dgv_Empresas)
        {
            for (int i = 0; i < Dgv_Empresas.RowCount; i++)
            {
                if (Convert.ToInt32(Dgv_Empresas.Rows[i].Cells["dgvcCaixa"].Value) == Caixa && Convert.ToInt32(Dgv_Empresas.Rows[i].Cells["dgvcEmpresa"].Value) == Loja)
                    return false;
            }

            return true;
        }
        public void MET_PopulaDataGrid(DataGridView Dgv_Empresas, TextBox txtEmpresa, TextBox txtCaixa, TextBox txtCodigo, Button btnGravar, TextBox txtEmpreDescDown)
        {
            string LojaDesc = "";

            if (txtEmpresa.Text == string.Empty)
            {
                MessageBox.Show("Informe uma Empresa válida!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmpresa.Select();
                return;
            }
            if (txtEmpreDescDown.Text == string.Empty)
            {
                MessageBox.Show("Informe uma Empresa válida!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmpresa.Select();
                return;
            }



            if (Convert.ToInt32(txtCaixa.Text) <= 40)
            {

            }
            else
            {
                MessageBox.Show("O limite atual é de no máximo 40 Caixas", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCaixa.Text = string.Empty;
                SendKeys.Send("{TAB}");
                return;
            }



            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string SelectComando = "SELECT Descri_EMP FROM TabEmpre WHERE Sequen_EMP = " + txtEmpresa.Text;
            SqlCommand Comando = new SqlCommand(SelectComando, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    LojaDesc = Dr["Descri_EMP"].ToString();

                    bool Verifica = MET_VerificaNumero(Convert.ToInt32(txtCaixa.Text), Convert.ToInt32(txtEmpresa.Text), Dgv_Empresas);
                    if (!Verifica)
                    {
                        MessageBox.Show("Registro informado já encontrado abaixo. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCaixa.SelectAll();
                    }
                    else
                    {
                        string _Permi = "";
                        DialogResult Permi = MessageBox.Show("Usuário tem permissão de liberar crédito neste caixa?", "TechSIS Informação", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                        if (Permi == DialogResult.Yes)
                        { _Permi = "SIM"; }
                        else if (Permi == DialogResult.No)
                        { _Permi = "NÃO"; }
                        else
                        { _Permi = "CANCELAR"; }

                        if (_Permi != "CANCELAR")
                        {

                            Dgv_Empresas.Rows.Add(txtEmpresa.Text.PadLeft(6, '0'), LojaDesc, txtCaixa.Text.PadLeft(6, '0'), _Permi);
                            int Caixa;
                            Caixa = Convert.ToInt32(txtCaixa.Text);
                            Caixa = Int32.Parse(txtCaixa.Text);
                            txtCaixa.Text = (Caixa + 1).ToString().PadLeft(6, '0');
                            txtCaixa.SelectAll();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Empresa inexistente no banco de dados. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmpresa.Select(); txtEmpresa.SelectAll();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_PopulaDataGrid()\n\nBLOCO.: POPULAR DGV\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_PopulaDataGrid()\n\nBLOCO.: POPULAR DGV\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
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

        //Busca a loja no banco de dados
        public void MET_BuscaDadosLoja(TextBox CodigoEmpresa, Control DescriEmpresa, Control Retorno)
        {
            CodigoEmpresa.Text = CodigoEmpresa.Text.PadLeft(6, '0');
            if (Convert.ToInt32(CodigoEmpresa.Text) == 0)
            {
                DescriEmpresa.Text = string.Empty;
                MessageBox.Show("Campo (Empresa) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Retorno.Focus(); CodigoEmpresa.SelectAll();
                return;
            }


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

        //Seleciona o ultimo registro do banco e adiciona 1
        public void MET_SelecionaUltimoRegistroMaisUm(TextBox txtCodigo, Button btnGravar)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT MAX (Sequen_USU + 1) as Sequen_USU FROM TabUsuar";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    txtCodigo.Text = Dr["Sequen_USU"].ToString().PadLeft(6, '0');
                    txtCodigo.SelectAll();
                    Dr.Close();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUltimoRegistroMaisUm()\n\nBLOCO.: TabUsuar_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUltimoRegistroMaisUm()\n\nBLOCO.: TabUsuar_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
            }
            finally
            {
                Conexão.Close();
            }
        }

        //Remove a linha quando clicada e popula os textboxs
        public void MET_RemoveLinhaDGV_PopulaTXT(DataGridView Dgv_Empresas, Button btnGravar, DataGridViewCellEventArgs e, TextBox txtEmpresa, TextBox txtEmpreDescDown, TextBox txtCaixa)
        {
            if (btnGravar.Enabled == true && Dgv_Empresas.RowCount >= 0)
            {
                if (e.RowIndex != -1)
                {
                    txtEmpresa.Text = Dgv_Empresas.CurrentRow.Cells["dgvcEmpresa"].Value.ToString();
                    txtEmpreDescDown.Text = Dgv_Empresas.CurrentRow.Cells["dgvcDescricao"].Value.ToString();
                    txtCaixa.Text = Dgv_Empresas.CurrentRow.Cells["dgvcCaixa"].Value.ToString();
                    Dgv_Empresas.Rows.Remove(Dgv_Empresas.CurrentRow);
                    txtCaixa.Select(); txtCaixa.SelectAll();
                }
            }
        }


        //SELECIONA A COR DE FUNDO DOS FORMULÁRIOS
        public void SelectCorFundo(Control Control_1, Control Control_2, Control Control_3, Control Control_4, Control Control_5, Control Control_6, Control Control_7, string CodigoLoja)
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
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_CapCaminhoSALV()\n\nBLOCO = TabUsuar_MET - CAPTURA CAMINHO DOS RELATÓRIOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_CapCaminhoSALV()\n\nBLOCO = TabUsuar_MET - CAPTURA CAMINHO DOS RELATÓRIOS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
