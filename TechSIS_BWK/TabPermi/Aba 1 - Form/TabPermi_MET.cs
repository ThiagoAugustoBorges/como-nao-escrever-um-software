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

namespace TabPermi
{
    internal class TabPermi_MET
    {
        //SELECIONA o USUÁRIO NO TAB
        public void MET_SelecionaUsuario(TextBox txtUsuarCodigo, Button btnGravar, Panel panUsuarioAb1, MethodInvoker CamposEnable_grb1, MethodInvoker CamposDisable_grb1, MethodInvoker CamposEnable_grb2, MethodInvoker CamposDisable_grb2, MethodInvoker ZerarCampos_grb1, MethodInvoker ZerarCampos_grb2, TextBox txtUsuarDescri, RadioButton rabAb1Blocos, RadioButton rabAb1Unitario, CheckBox chePerIncBl2, CheckBox chePerAltBl2, CheckBox chePerExcBl2, CheckBox cheConAltBl2, CheckBox cheBloqAbasBl2, NumericUpDown nupDown1Bl2, NumericUpDown nupDown2Bl2, NumericUpDown nupDown3Bl2, NumericUpDown nupDown4Bl2, Button btnMarcarTodasBl2, Button btnPassAntBl2, Button btnPassProxBl2, TextBox txtDescicaoBl2)
        {
            #region rabAb1Blocos
            if (rabAb1Blocos.Checked == true)
            {
                #region TRATAMENTO txtCodigo em BRANCO
                if (txtUsuarCodigo.Text == string.Empty)
                {
                    txtUsuarCodigo.Text = "000000";
                }
                if (Convert.ToInt32(txtUsuarCodigo.Text) == 0)
                {
                    txtUsuarCodigo.Text = txtUsuarCodigo.Text.PadLeft(6, '0');
                    panUsuarioAb1.Focus();
                    txtUsuarCodigo.SelectAll();

                    ZerarCampos_grb1();
                    ZerarCampos_grb2();

                    CamposDisable_grb1();
                    CamposDisable_grb2();

                    txtUsuarDescri.Text = string.Empty;

                    btnGravar.Enabled = false;
                    MessageBox.Show("Campo (Código) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    txtUsuarCodigo.Text = txtUsuarCodigo.Text.PadLeft(6, '0');
                }
                #endregion

                #region TRATAMENTO PARA USUÁRIO MASTER
                if (Convert.ToInt32(txtUsuarCodigo.Text) == 1)
                {
                    panUsuarioAb1.Focus();
                    txtUsuarCodigo.SelectAll();

                    ZerarCampos_grb1();
                    ZerarCampos_grb2();

                    CamposDisable_grb1();
                    CamposDisable_grb2();

                    txtUsuarDescri.Text = string.Empty;

                    btnGravar.Enabled = false;
                    MessageBox.Show("Usuário MASTER já possui permissão total", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();


                //Define a string e o comando
                string StringComandoSELEÇÃO = ("SELECT Apelid_USU,Status_USU FROM TabUsuar WHERE Sequen_USU = @Sequen");
                SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                ComandoSELEÇÃO.Parameters.Add("@Sequen", SqlDbType.VarChar).Value = txtUsuarCodigo.Text;

                try
                {
                    SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();

                    if (Dr.HasRows)
                    {
                        #region TRATAMENTO STATUS
                        if (Convert.ToInt32(Dr["Status_USU"]) == 3)
                        {
                            panUsuarioAb1.Focus();
                            txtUsuarCodigo.SelectAll();
                            ZerarCampos_grb1();
                            ZerarCampos_grb2();
                            CamposDisable_grb1();
                            CamposDisable_grb2();
                            txtUsuarDescri.Text = string.Empty;
                            btnGravar.Enabled = false;
                            MessageBox.Show("Usuário consta como EXCLUIDO. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (Convert.ToInt32(Dr["Status_USU"]) == 2)
                        {
                            DialogResult CONTINUA = MessageBox.Show("Usuário consta como INATIVO. Deseja Continuar?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (CONTINUA == DialogResult.Yes)
                            {
                                txtUsuarDescri.Text = Dr["Apelid_USU"].ToString();
                                btnGravar.Enabled = true;
                                ZerarCampos_grb1();
                                CamposEnable_grb1();
                            }
                            else
                            {
                                panUsuarioAb1.Focus();
                                txtUsuarCodigo.SelectAll();
                                ZerarCampos_grb1();
                                ZerarCampos_grb2();
                                CamposDisable_grb1();
                                CamposDisable_grb2();
                                txtUsuarDescri.Text = string.Empty;
                                btnGravar.Enabled = false;
                            }
                        }
                        #endregion
                        else
                        {
                            txtUsuarDescri.Text = Dr["Apelid_USU"].ToString();
                            btnGravar.Enabled = true;
                            ZerarCampos_grb1();
                            CamposEnable_grb1();
                        }
                    }
                    else
                    {
                        panUsuarioAb1.Focus();
                        txtUsuarCodigo.SelectAll();
                        ZerarCampos_grb1();
                        ZerarCampos_grb2();
                        CamposDisable_grb1();
                        CamposDisable_grb2();
                        txtUsuarDescri.Text = string.Empty;
                        btnGravar.Enabled = false;
                        MessageBox.Show("Usuário inexistente no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUsuario()\n\nBLOCO.: CLASSE TabPermi_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                    txtUsuarDescri.Text = string.Empty;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUsuario()\n\nBLOCO.: CLASSE TabPermi_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                    txtUsuarDescri.Text = string.Empty;
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
            #region rabAb1Unitario
            else if (rabAb1Unitario.Checked == true)
            {
                #region TRATAMENTO txtCodigo em BRANCO
                if (txtUsuarCodigo.Text == string.Empty)
                {
                    txtUsuarCodigo.Text = "000000";
                }
                if (Convert.ToInt32(txtUsuarCodigo.Text) == 0)
                {
                    txtUsuarCodigo.Text = txtUsuarCodigo.Text.PadLeft(6, '0');
                    panUsuarioAb1.Focus();
                    txtUsuarCodigo.SelectAll();

                    ZerarCampos_grb1();
                    ZerarCampos_grb2();

                    CamposDisable_grb1();
                    CamposDisable_grb2();

                    txtUsuarDescri.Text = string.Empty;

                    btnGravar.Enabled = false;
                    MessageBox.Show("Campo (Código) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    txtUsuarCodigo.Text = txtUsuarCodigo.Text.PadLeft(6, '0');
                }
                #endregion

                #region TRATAMENTO PARA USUÁRIO MASTER
                if (Convert.ToInt32(txtUsuarCodigo.Text) == 1)
                {
                    panUsuarioAb1.Focus();
                    txtUsuarCodigo.SelectAll();

                    ZerarCampos_grb1();
                    ZerarCampos_grb2();

                    CamposDisable_grb1();
                    CamposDisable_grb2();

                    txtUsuarDescri.Text = string.Empty;

                    btnGravar.Enabled = false;
                    MessageBox.Show("Usuário MASTER já possui permissão total", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();


                //Define a string e o comando
                string StringComandoSELEÇÃO = ("SELECT Apelid_USU,Status_USU FROM TabUsuar WHERE Sequen_USU = @Sequen");
                SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                ComandoSELEÇÃO.Parameters.Add("@Sequen", SqlDbType.VarChar).Value = txtUsuarCodigo.Text;

                try
                {
                    SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();

                    if (Dr.HasRows)
                    {
                        #region TRATAMENTO STATUS
                        if (Convert.ToInt32(Dr["Status_USU"]) == 3)
                        {
                            panUsuarioAb1.Focus();
                            txtUsuarCodigo.SelectAll();
                            ZerarCampos_grb1();
                            ZerarCampos_grb2();
                            CamposDisable_grb1();
                            CamposDisable_grb2();
                            txtUsuarDescri.Text = string.Empty;
                            btnGravar.Enabled = false;
                            MessageBox.Show("Usuário consta como EXCLUIDO. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (Convert.ToInt32(Dr["Status_USU"]) == 2)
                        {
                            DialogResult CONTINUA = MessageBox.Show("Usuário consta como INATIVO. Deseja Continuar?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (CONTINUA == DialogResult.Yes)
                            {
                                txtUsuarDescri.Text = Dr["Apelid_USU"].ToString();
                                btnGravar.Enabled = true;
                                ZerarCampos_grb2();
                                CamposEnable_grb2();
                            }
                            else
                            {
                                panUsuarioAb1.Focus();
                                txtUsuarCodigo.SelectAll();
                                ZerarCampos_grb1();
                                ZerarCampos_grb2();
                                CamposDisable_grb1();
                                CamposDisable_grb2();
                                txtUsuarDescri.Text = string.Empty;
                                btnGravar.Enabled = false;
                            }
                        }
                        #endregion
                        else
                        {
                            txtUsuarDescri.Text = Dr["Apelid_USU"].ToString();
                            btnGravar.Enabled = false;
                            ZerarCampos_grb2();
                            CamposEnable_grb2();
                            DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);
                        }
                    }
                    else
                    {
                        panUsuarioAb1.Focus();
                        txtUsuarCodigo.SelectAll();
                        ZerarCampos_grb1();
                        ZerarCampos_grb2();
                        CamposDisable_grb1();
                        CamposDisable_grb2();
                        txtUsuarDescri.Text = string.Empty;
                        btnGravar.Enabled = false;
                        MessageBox.Show("Usuário inexistente no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUsuario()\n\nBLOCO.: CLASSE TabPermi_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                    txtUsuarDescri.Text = string.Empty;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUsuario()\n\nBLOCO.: CLASSE TabPermi_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                    txtUsuarDescri.Text = string.Empty;
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion


            #region SE NENHUM ESTIVER MARCADO
            else
            {
                panUsuarioAb1.Focus();
                txtUsuarCodigo.SelectAll();

                ZerarCampos_grb1();
                ZerarCampos_grb2();

                CamposDisable_grb1();
                CamposDisable_grb2();

                txtUsuarDescri.Text = string.Empty;

                btnGravar.Enabled = false;

                MessageBox.Show("Selecione primeiramente o tipo de permissão (BLOCO ou UNITÁRIA)", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            #endregion
        }


        public void ZeraCamposChecked(CheckBox chePerIncBl2, CheckBox chePerAltBl2, CheckBox chePerExcBl2, CheckBox cheConAltBl2, CheckBox cheBloqAbasBl2, NumericUpDown nupDown1Bl2, NumericUpDown nupDown2Bl2, NumericUpDown nupDown3Bl2, NumericUpDown nupDown4Bl2, TextBox txtDescicaoBl2)
        {
            chePerIncBl2.Checked = false;
            chePerAltBl2.Checked = false;
            chePerExcBl2.Checked = false;
            cheConAltBl2.Checked = false;
            cheBloqAbasBl2.Checked = false;

            nupDown1Bl2.Value = 0;
            nupDown2Bl2.Value = 0;
            nupDown3Bl2.Value = 0;
            nupDown4Bl2.Value = 0;
        }
        public void DesabilitarCamposChecked(CheckBox chePerIncBl2, CheckBox chePerAltBl2, CheckBox chePerExcBl2, CheckBox cheConAltBl2, CheckBox cheBloqAbasBl2, NumericUpDown nupDown1Bl2, NumericUpDown nupDown2Bl2, NumericUpDown nupDown3Bl2, NumericUpDown nupDown4Bl2, Button btnMarcarTodasBl2, Button btnPassAntBl2, Button btnPassProxBl2)
        {
            chePerIncBl2.Enabled = false;
            chePerAltBl2.Enabled = false;
            chePerExcBl2.Enabled = false;
            cheConAltBl2.Enabled = false;
            cheBloqAbasBl2.Enabled = false;

            nupDown1Bl2.Enabled = false;
            nupDown2Bl2.Enabled = false;
            nupDown3Bl2.Enabled = false;
            nupDown4Bl2.Enabled = false;

            btnMarcarTodasBl2.Enabled = false;
            btnPassAntBl2.Enabled = false;
            btnPassProxBl2.Enabled = false;
        }
        public void HabilitarCamposChecked(CheckBox chePerIncBl2, CheckBox chePerAltBl2, CheckBox chePerExcBl2, CheckBox cheConAltBl2, CheckBox cheBloqAbasBl2, NumericUpDown nupDown1Bl2, NumericUpDown nupDown2Bl2, NumericUpDown nupDown3Bl2, NumericUpDown nupDown4Bl2, Button btnMarcarTodasBl2, Button btnPassAntBl2, Button btnPassProxBl2)
        {
            chePerIncBl2.Enabled = true;
            chePerAltBl2.Enabled = true;
            chePerExcBl2.Enabled = true;
            cheConAltBl2.Enabled = true;
            cheBloqAbasBl2.Enabled = true;

           
            btnMarcarTodasBl2.Enabled = true;
            btnPassAntBl2.Enabled = true;
            btnPassProxBl2.Enabled = true;
        }
        //SELECIONA O PROGRAMA
        public void MET_SelecionaPrograma(TextBox txtUsuarCodigo, Button btnGravar, Panel panUsuarioAb1, MethodInvoker CamposEnable_grb1, MethodInvoker CamposDisable_grb1, MethodInvoker CamposEnable_grb2, MethodInvoker CamposDisable_grb2, MethodInvoker ZerarCampos_grb1, MethodInvoker ZerarCampos_grb2, TextBox txtUsuarDescri, RadioButton rabAb1Blocos, RadioButton rabAb1Unitario, TextBox txtCodigoBl2, TextBox txtDescicaoBl2, GroupBox grb2, TextBox txtMESTRE, CheckBox chePerIncBl2, CheckBox chePerAltBl2, CheckBox chePerExcBl2, CheckBox cheConAltBl2, CheckBox cheBloqAbasBl2, NumericUpDown nupDown1Bl2, NumericUpDown nupDown2Bl2, NumericUpDown nupDown3Bl2, NumericUpDown nupDown4Bl2, Button btnMarcarTodasBl2, Button btnPassAntBl2, Button btnPassProxBl2)
        {
            #region TRATAMENTO txtCodigoBl2 em BRANCO
            if (txtCodigoBl2.Text == string.Empty)
            {
                txtCodigoBl2.Text = "000000";
            }
           
            if (Convert.ToInt32(txtCodigoBl2.Text) < 10000)
            {
                txtCodigoBl2.Text = txtCodigoBl2.Text.PadLeft(6, '0');
                grb2.Focus();
                txtCodigoBl2.SelectAll();
                
                btnGravar.Enabled = false;
                ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
                DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);

                txtDescicaoBl2.Text = string.Empty;
                MessageBox.Show("Código de programa informado é inválido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                txtCodigoBl2.Text = txtCodigoBl2.Text.PadLeft(6, '0');
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

                string StringComandoPROGR = "SELECT Descri_PGR,Status_PGR FROM TabProgr WHERE Sequen_PGR = @Sequen";
                SqlCommand ComandoPROGR = new SqlCommand(StringComandoPROGR, Conexão);
                ComandoPROGR.Parameters.Add("@Sequen", SqlDbType.VarChar).Value = txtCodigoBl2.Text;

                string StringComandoPERMI = "SELECT * FROM TabPermi WHERE SeqPgr_PER = @Programa AND SeqUsu_PER = @Usuario";
                SqlCommand ComandoPERMI = new SqlCommand(StringComandoPERMI, Conexão);
                ComandoPERMI.Parameters.Add("@Programa", SqlDbType.VarChar).Value = txtCodigoBl2.Text;
                ComandoPERMI.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = txtUsuarCodigo.Text;


                #region AQUI TA MUITO BAGUNÇADO
                try
                {
                    SqlDataReader Dr = ComandoPROGR.ExecuteReader(); Dr.Read();
                    //SE O PROGRAMA EXISTE
                    if (Dr.HasRows)
                    {

                        txtDescicaoBl2.Text = Dr["Descri_PGR"].ToString();

                        Dr.Close();

                        //BUSCA INFORMAÇÕES DA PERMISSÃO
                        try
                        {
                            SqlDataReader Permi = ComandoPERMI.ExecuteReader(); Permi.Read();

                            //SE A PERMISSÃO EXISTE
                            if (Permi.HasRows)
                            {

                                ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
                                DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);

                                string chePerIncBl2_String = Permi["PerINC_PER"].ToString();
                                string chePerAltBl2_String = Permi["PerALT_PER"].ToString();
                                string chePerExcBl2_String = Permi["PerEXC_PER"].ToString();
                                string cheConAltBl2_String = Permi["PerCON_PER"].ToString();
                                string cheBloqAbasBl2_String = Permi["PerABA_PER"].ToString();

                                #region TRATAMENTOS String Boolean
                                #region chePerIncBl2
                                if (chePerIncBl2_String == "True")
                                {
                                    chePerIncBl2.Checked = true;
                                }
                                else
                                {
                                    chePerIncBl2.Checked = false;
                                }
                                #endregion
                                #region chePerAltBl2
                                if (chePerAltBl2_String == "True")
                                {
                                    chePerAltBl2.Checked = true;
                                }
                                else
                                {
                                    chePerAltBl2.Checked = false;
                                }
                                #endregion
                                #region chePerExcBl2
                                if (chePerExcBl2_String == "True")
                                {
                                    chePerExcBl2.Checked = true;
                                }
                                else
                                {
                                    chePerExcBl2.Checked = false;
                                }
                                #endregion
                                #region cheConAltBl2
                                if (cheConAltBl2_String == "True")
                                {
                                    cheConAltBl2.Checked = true;
                                }
                                else
                                {
                                    cheConAltBl2.Checked = false;
                                }
                                #endregion
                                #region cheBloqAbasBl2
                                if (cheBloqAbasBl2_String == "True")
                                {
                                    cheBloqAbasBl2.Checked = true;

                                    nupDown1Bl2.Value = Convert.ToDecimal(Permi["PerAb1_PER"]);
                                    nupDown2Bl2.Value = Convert.ToDecimal(Permi["PerAb2_PER"]);
                                    nupDown3Bl2.Value = Convert.ToDecimal(Permi["PerAb3_PER"]);
                                    nupDown4Bl2.Value = Convert.ToDecimal(Permi["PerAb4_PER"]);
                                }
                                else
                                {
                                    cheBloqAbasBl2.Checked = false;

                                    nupDown1Bl2.Value = 0;
                                    nupDown2Bl2.Value = 0;
                                    nupDown3Bl2.Value = 0;
                                    nupDown4Bl2.Value = 0;
                                }
                                #endregion
                                #endregion


                                btnGravar.Enabled = false;
                                grb2.Focus();
                                txtCodigoBl2.SelectAll();
                                MessageBox.Show("Permissão (" + txtCodigoBl2.Text + ") já cadastrada para este usuário.\nPARA MODIFICAR A PERMISSÃO USE A (ALTERAÇÃO)", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            //SE A PERMISSÃO NÃO EXISTE
                            else
                            {
                                HabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);
                                ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);

                                btnGravar.Enabled = true;
                            }

                            Permi.Close();
                        }
                            //ERRO DA PERMISSÃO
                        catch (SqlException Ex)
                        {
                            MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaPrograma()\n\nBLOCO.: ERRO DA PERMISSÃO\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            btnGravar.Enabled = false;
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaPrograma()\n\nBLOCO.: ERRO DA PERMISSÃO\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            btnGravar.Enabled = false;
                        }
                    }







                        //SE O PROGRAMA NÃO EXISTE
                    else
                    {
                        txtDescicaoBl2.Text = string.Empty; 
                        btnGravar.Enabled = false;
                        DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);
                        ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
                        MessageBox.Show("Caminho de programa inexistente no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        grb2.Focus();
                        txtCodigoBl2.SelectAll();
                    }

                    Dr.Close();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaPrograma()\n\nBLOCO.: ERRO DO PROGRAMA\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaPrograma()\n\nBLOCO.: ERRO DO PROGRAMA\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                }
                #endregion


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

                string StringComandoPROGR = "SELECT Descri_PGR FROM TabProgr WHERE Sequen_PGR = @Sequen";
                SqlCommand ComandoPROGR = new SqlCommand(StringComandoPROGR, Conexão);
                ComandoPROGR.Parameters.Add("@Sequen", SqlDbType.VarChar).Value = txtCodigoBl2.Text;

                string StringComandoPERMI = "SELECT * FROM TabPermi WHERE SeqPgr_PER = @Programa AND SeqUsu_PER = @Usuario";
                SqlCommand ComandoPERMI = new SqlCommand(StringComandoPERMI, Conexão);
                ComandoPERMI.Parameters.Add("@Programa", SqlDbType.VarChar).Value = txtCodigoBl2.Text;
                ComandoPERMI.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = txtUsuarCodigo.Text;


                #region AQUI TA MUITO BAGUNÇADO
                try
                {
                    SqlDataReader Dr = ComandoPROGR.ExecuteReader(); Dr.Read();
                    //SE O PROGRAMA EXISTE
                    if (Dr.HasRows)
                    {

                        txtDescicaoBl2.Text = Dr["Descri_PGR"].ToString();

                        Dr.Close();

                        //BUSCA INFORMAÇÕES DA PERMISSÃO
                        try
                        {
                            SqlDataReader Permi = ComandoPERMI.ExecuteReader(); Permi.Read();

                            //SE A PERMISSÃO EXISTE
                            if (Permi.HasRows)
                            {

                                ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
                                HabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);

                                string chePerIncBl2_String = Permi["PerINC_PER"].ToString();
                                string chePerAltBl2_String = Permi["PerALT_PER"].ToString();
                                string chePerExcBl2_String = Permi["PerEXC_PER"].ToString();
                                string cheConAltBl2_String = Permi["PerCON_PER"].ToString();
                                string cheBloqAbasBl2_String = Permi["PerABA_PER"].ToString();

                                #region TRATAMENTOS String Boolean
                                #region chePerIncBl2
                                if (chePerIncBl2_String == "True")
                                {
                                    chePerIncBl2.Checked = true;
                                }
                                else
                                {
                                    chePerIncBl2.Checked = false;
                                }
                                #endregion
                                #region chePerAltBl2
                                if (chePerAltBl2_String == "True")
                                {
                                    chePerAltBl2.Checked = true;
                                }
                                else
                                {
                                    chePerAltBl2.Checked = false;
                                }
                                #endregion
                                #region chePerExcBl2
                                if (chePerExcBl2_String == "True")
                                {
                                    chePerExcBl2.Checked = true;
                                }
                                else
                                {
                                    chePerExcBl2.Checked = false;
                                }
                                #endregion
                                #region cheConAltBl2
                                if (cheConAltBl2_String == "True")
                                {
                                    cheConAltBl2.Checked = true;
                                }
                                else
                                {
                                    cheConAltBl2.Checked = false;
                                }
                                #endregion
                                #region cheBloqAbasBl2
                                if (cheBloqAbasBl2_String == "True")
                                {
                                    cheBloqAbasBl2.Checked = true;

                                    nupDown1Bl2.Value = Convert.ToDecimal(Permi["PerAb1_PER"]);
                                    nupDown2Bl2.Value = Convert.ToDecimal(Permi["PerAb2_PER"]);
                                    nupDown3Bl2.Value = Convert.ToDecimal(Permi["PerAb3_PER"]);
                                    nupDown4Bl2.Value = Convert.ToDecimal(Permi["PerAb4_PER"]);
                                }
                                else
                                {
                                    cheBloqAbasBl2.Checked = false;

                                    nupDown1Bl2.Value = 0;
                                    nupDown2Bl2.Value = 0;
                                    nupDown3Bl2.Value = 0;
                                    nupDown4Bl2.Value = 0;
                                }
                                #endregion
                                #endregion


                                btnGravar.Enabled = true;
                                
                            }
                            //SE A PERMISSÃO NÃO EXISTE
                            else
                            {
                                DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);
                                ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
                                btnGravar.Enabled = false;
                                grb2.Focus();
                                txtCodigoBl2.SelectAll();
                                txtDescicaoBl2.Text = string.Empty;
                                MessageBox.Show("Permissão (" + txtCodigoBl2.Text + ") inexistente para este usuário", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            Permi.Close();
                        }
                        //ERRO DA PERMISSÃO
                        catch (SqlException Ex)
                        {
                            MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaPrograma()\n\nBLOCO.: ERRO DA PERMISSÃO\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            btnGravar.Enabled = false;
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaPrograma()\n\nBLOCO.: ERRO DA PERMISSÃO\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            btnGravar.Enabled = false;
                        }
                    }







                        //SE O PROGRAMA NÃO EXISTE
                    else
                    {
                        txtDescicaoBl2.Text = string.Empty;
                        btnGravar.Enabled = false;
                        DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);
                        ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
                        MessageBox.Show("Caminho de programa inexistente no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        grb2.Focus();
                        txtCodigoBl2.SelectAll();
                    }

                    Dr.Close();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaPrograma()\n\nBLOCO.: ERRO DO PROGRAMA\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaPrograma()\n\nBLOCO.: ERRO DO PROGRAMA\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnGravar.Enabled = false;
                }
                #endregion


            }
            #endregion
        }


        public void MET_SelecionaPrograma_SETAS(string Sinal_Maior_Menor,TextBox txtCodigoBl2, TextBox txtDescicaoBl2, string Complemento)
        {
            if (txtCodigoBl2.Text == string.Empty)
            {
                txtCodigoBl2.Text = "000000";
            }

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComandoPROGR = "SELECT TOP 1 Descri_PGR,Sequen_PGR FROM TabProgr WHERE Sequen_PGR " + Sinal_Maior_Menor + " @Sequen " + Complemento;
            SqlCommand ComandoPROGR = new SqlCommand(StringComandoPROGR, Conexão);
            ComandoPROGR.Parameters.Add("@Sequen", SqlDbType.VarChar).Value = txtCodigoBl2.Text;


            try
            {
                SqlDataReader Dr = ComandoPROGR.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    txtCodigoBl2.Text = Dr["Sequen_PGR"].ToString();
                    txtDescicaoBl2.Text = Dr["Descri_PGR"].ToString();

                    txtCodigoBl2.Select(); txtCodigoBl2.SelectAll();
                }
                else
                {
                    MessageBox.Show("Não existem mais dados para exibir", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigoBl2.Select(); txtCodigoBl2.SelectAll();
                }

            }
            catch (Exception)
            {
 
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
    }
}