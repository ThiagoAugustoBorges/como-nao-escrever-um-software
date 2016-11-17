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
    internal class TabPermi_btnGRAVAR
    {

        public void _ButtonINC_BLOC(TextBox txtUsuarCodigo, Button btnGravar, Panel panUsuarioAb1, MethodInvoker CamposEnable_grb1, MethodInvoker CamposDisable_grb1, MethodInvoker CamposEnable_grb2, MethodInvoker CamposDisable_grb2, MethodInvoker ZerarCampos_grb1, MethodInvoker ZerarCampos_grb2, TextBox txtUsuarDescri, RadioButton rabAb1Blocos, RadioButton rabAb1Unitario, TextBox txtCodigoBl2, TextBox txtDescicaoBl2, GroupBox grb2, TextBox txtMESTRE, CheckBox chePerIncBl2, CheckBox chePerAltBl2, CheckBox chePerExcBl2, CheckBox cheConAltBl2, CheckBox cheBloqAbasBl2, NumericUpDown nupDown1Bl2, NumericUpDown nupDown2Bl2, NumericUpDown nupDown3Bl2, NumericUpDown nupDown4Bl2, Button btnMarcarTodasBl2, Button btnPassAntBl2, Button btnPassProxBl2, CheckBox chePerIncBl1, CheckBox chePerAltBl1, CheckBox chePerExcBl1, CheckBox cheConAltBl1, CheckBox cheBloqAbasBl1, NumericUpDown nupDown1Bl1, NumericUpDown nupDown2Bl1, NumericUpDown nupDown3Bl1, NumericUpDown nupDown4Bl1, Button btnMarcarTodasBl1, Button btnPassAntBl1, Button btnPassProxBl1, TextBox txtUsuario, ComboBox comBlocoBl1)
        {
            if (txtMESTRE.Text == "INCLUIR")
            {
                if (rabAb1Blocos.Checked == true)
                {
                    #region BLOCO
                    try
                    {
                        //Cria a conexão com o Banco de Dados e Abre!
                        StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                        string LerString = StringConexão.ReadLine();
                        SqlConnection Conexão = new SqlConnection(LerString);
                        Conexão.Open();

                        SqlConnection Conexão_READER = new SqlConnection(LerString);
                        Conexão_READER.Open();

                        SqlConnection Conexão_DELETE = new SqlConnection(LerString);
                        Conexão_DELETE.Open();

                        String CapProgramasCada = "";


                        #region SELECTs
                        if (comBlocoBl1.SelectedIndex == 0)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr WHERE Sequen_PGR > 10000 AND Sequen_PGR < 30000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 60000 AND Sequen_PGR < 70000";
                        }
                        if (comBlocoBl1.SelectedIndex == 1)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr WHERE Sequen_PGR > 10000 AND Sequen_PGR < 30000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 60000 AND Sequen_PGR < 70000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 50000 AND Sequen_PGR < 60000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 30000 AND Sequen_PGR < 40000";
                        }
                        if (comBlocoBl1.SelectedIndex == 2)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr WHERE Sequen_PGR > 10000 AND Sequen_PGR < 30000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 60000 AND Sequen_PGR < 70000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 50000 AND Sequen_PGR < 60000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 30000 AND Sequen_PGR < 40000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 40000 AND Sequen_PGR < 50000";
                        }
                        if (comBlocoBl1.SelectedIndex == 3)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr WHERE Sequen_PGR > 10000 AND Sequen_PGR < 30000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 60000 AND Sequen_PGR < 70000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 50000 AND Sequen_PGR < 60000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 30000 AND Sequen_PGR < 40000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 40000 AND Sequen_PGR < 50000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 70000 AND Sequen_PGR < 80000";
                        }
                        if (comBlocoBl1.SelectedIndex == 4)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr WHERE Sequen_PGR < 90000";
                        }
                        if (comBlocoBl1.SelectedIndex == 5)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr";
                        }
                        #endregion


                        SqlCommand cmd_CapProgramasCada = new SqlCommand(CapProgramasCada, Conexão_READER);
                        SqlDataReader Dr_Progr = cmd_CapProgramasCada.ExecuteReader(CommandBehavior.CloseConnection);


                        //EXCLUIR
                        string ComandoEXC = "DELETE FROM TabPermi WHERE SeqUsu_PER = " + txtUsuarCodigo.Text;
                        SqlCommand cmdEXC = new SqlCommand(ComandoEXC, Conexão_DELETE);
                        cmdEXC.ExecuteNonQuery();


                        while (Dr_Progr.Read())
                        {
                            //Pega o Ultimo registro da Tab de Historico!
                            string PegarULTIMORegHIST = "SELECT MAX (Sequen_HIS + 1) FROM TabHisto";
                            SqlCommand PegarREG = new SqlCommand(PegarULTIMORegHIST, Conexão);
                            SqlDataReader Dr = PegarREG.ExecuteReader(); Dr.Read();
                            int _SequenHIS = Convert.ToInt32(Dr[0].ToString());
                            Dr.Close();



                            string NumeroPrograma = Dr_Progr[0].ToString();

                            //String e cria o SQLComand
                            string StringComandoINCLUIR_TABE = "INSERT INTO TabPermi VALUES (@SeqUsu,@SeqPgr,@PerINC,@PerALT,@PerEXC,@PerCON,@PerABA,@PerAb1,@PerAb2,@PerAb3,@PerAb4)";
                            string StringComandoINCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'090300','INCLUSÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                            SqlCommand ComandoINCLUIR_HIST = new SqlCommand(StringComandoINCLUIR_HIST, Conexão);
                            SqlCommand ComandoINCLUIR_TABE = new SqlCommand(StringComandoINCLUIR_TABE, Conexão);


                            //Parametros do Insert na tabela
                            ComandoINCLUIR_TABE.Parameters.Add("@SeqUsu", SqlDbType.Int).Value = txtUsuarCodigo.Text;
                            ComandoINCLUIR_TABE.Parameters.Add("@SeqPgr", SqlDbType.VarChar).Value = NumeroPrograma;
                            ComandoINCLUIR_TABE.Parameters.Add("@PerINC", SqlDbType.VarChar).Value = chePerIncBl1.Checked;
                            ComandoINCLUIR_TABE.Parameters.Add("@PerALT", SqlDbType.VarChar).Value = chePerAltBl1.Checked;
                            ComandoINCLUIR_TABE.Parameters.Add("@PerEXC", SqlDbType.VarChar).Value = chePerExcBl1.Checked;
                            ComandoINCLUIR_TABE.Parameters.Add("@PerCON", SqlDbType.VarChar).Value = cheConAltBl1.Checked;
                            ComandoINCLUIR_TABE.Parameters.Add("@PerABA", SqlDbType.VarChar).Value = cheBloqAbasBl1.Checked;
                            ComandoINCLUIR_TABE.Parameters.Add("@PerAb1", SqlDbType.Int).Value = Convert.ToInt32(nupDown1Bl1.Value);
                            ComandoINCLUIR_TABE.Parameters.Add("@PerAb2", SqlDbType.Int).Value = Convert.ToInt32(nupDown2Bl1.Value);
                            ComandoINCLUIR_TABE.Parameters.Add("@PerAb3", SqlDbType.Int).Value = Convert.ToInt32(nupDown3Bl1.Value);
                            ComandoINCLUIR_TABE.Parameters.Add("@PerAb4", SqlDbType.Int).Value = Convert.ToInt32(nupDown4Bl1.Value);

                            //Parametros do Insert no banco
                            ComandoINCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                            ComandoINCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "INCLUSÃO DA PERMISSÃO..: " + NumeroPrograma + " PARA O USUÁRIO.: " + txtUsuarCodigo.Text;
                            ComandoINCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = "INC.: " + chePerIncBl1.Checked + " | ALT.: " + chePerAltBl1.Checked + " | EXC.: " + chePerExcBl1.Checked;
                            ComandoINCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                            ComandoINCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();

                            try
                            {
                                ComandoINCLUIR_HIST.ExecuteNonQuery();
                                ComandoINCLUIR_TABE.ExecuteNonQuery();
                            }
                            catch (SqlException Ex)
                            {
                                if (Ex.Number == 2627)
                                {

                                }
                                else
                                {
                                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método _ButtonINC_BLOC()\n\nBLOCO = INSERIR PERMISSÃO NO BLOCO\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }



                        MessageBox.Show("Permissão inserida com sucesso", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        TabPermi_MET MET = new TabPermi_MET();

                        comBlocoBl1.Enabled = false;
                        comBlocoBl1.SelectedIndex = -1;
                        MET.ZeraCamposChecked(chePerIncBl1, chePerAltBl1, chePerExcBl1, cheConAltBl1, cheBloqAbasBl1, nupDown1Bl1, nupDown2Bl1, nupDown3Bl1, nupDown4Bl1, null);
                        MET.DesabilitarCamposChecked(chePerIncBl1, chePerAltBl1, chePerExcBl1, cheConAltBl1, cheBloqAbasBl1, nupDown1Bl1, nupDown2Bl1, nupDown3Bl1, nupDown4Bl1, btnMarcarTodasBl1, btnPassAntBl1, btnPassProxBl1);

                        txtUsuarCodigo.Select();
                        txtUsuarCodigo.SelectAll();
                        txtUsuarDescri.Text = string.Empty;

                        btnGravar.Enabled = false;
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }





                    #endregion
                }
                else
                {
                    #region UNITÁRIO
                    //Cria a conexão com o Banco de Dados e Abre!
                    StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                    string LerString = StringConexão.ReadLine();
                    SqlConnection Conexão = new SqlConnection(LerString);
                    Conexão.Open();

                    //Pega o Ultimo registro da Tab de Historico!
                    string PegarULTIMORegHIST = "SELECT MAX (Sequen_HIS + 1) FROM TabHisto";
                    SqlCommand PegarREG = new SqlCommand(PegarULTIMORegHIST, Conexão);
                    SqlDataReader Dr = PegarREG.ExecuteReader(); Dr.Read();
                    int _SequenHIS = Convert.ToInt32(Dr[0].ToString());
                    Dr.Close();

                    //String e cria o SQLComand
                    string StringComandoINCLUIR_TABE = "INSERT INTO TabPermi VALUES (@SeqUsu,@SeqPgr,@PerINC,@PerALT,@PerEXC,@PerCON,@PerABA,@PerAb1,@PerAb2,@PerAb3,@PerAb4)";
                    string StringComandoINCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'090300','INCLUSÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                    SqlCommand ComandoINCLUIR_HIST = new SqlCommand(StringComandoINCLUIR_HIST, Conexão);
                    SqlCommand ComandoINCLUIR_TABE = new SqlCommand(StringComandoINCLUIR_TABE, Conexão);


                    //Parametros do Insert na tabela
                    ComandoINCLUIR_TABE.Parameters.Add("@SeqUsu", SqlDbType.Int).Value = txtUsuarCodigo.Text;
                    ComandoINCLUIR_TABE.Parameters.Add("@SeqPgr", SqlDbType.VarChar).Value = txtCodigoBl2.Text;
                    ComandoINCLUIR_TABE.Parameters.Add("@PerINC", SqlDbType.VarChar).Value = chePerIncBl2.Checked;
                    ComandoINCLUIR_TABE.Parameters.Add("@PerALT", SqlDbType.VarChar).Value = chePerAltBl2.Checked;
                    ComandoINCLUIR_TABE.Parameters.Add("@PerEXC", SqlDbType.VarChar).Value = chePerExcBl2.Checked;
                    ComandoINCLUIR_TABE.Parameters.Add("@PerCON", SqlDbType.VarChar).Value = cheConAltBl2.Checked;
                    ComandoINCLUIR_TABE.Parameters.Add("@PerABA", SqlDbType.VarChar).Value = cheBloqAbasBl2.Checked;
                    ComandoINCLUIR_TABE.Parameters.Add("@PerAb1", SqlDbType.Int).Value = Convert.ToInt32(nupDown1Bl2.Value);
                    ComandoINCLUIR_TABE.Parameters.Add("@PerAb2", SqlDbType.Int).Value = Convert.ToInt32(nupDown2Bl2.Value);
                    ComandoINCLUIR_TABE.Parameters.Add("@PerAb3", SqlDbType.Int).Value = Convert.ToInt32(nupDown3Bl2.Value);
                    ComandoINCLUIR_TABE.Parameters.Add("@PerAb4", SqlDbType.Int).Value = Convert.ToInt32(nupDown4Bl2.Value);

                    //Parametros do Insert no banco
                    ComandoINCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                    ComandoINCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "INCLUSÃO DA PERMISSÃO..: " + txtCodigoBl2.Text + " PARA O USUÁRIO.: " + txtUsuarCodigo.Text;
                    ComandoINCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = "INC.: " + chePerIncBl2.Checked + " | ALT.: " + chePerAltBl2.Checked + " | EXC.: " + chePerExcBl2.Checked;
                    ComandoINCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                    ComandoINCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();


                    try
                    {
                        ComandoINCLUIR_HIST.ExecuteNonQuery();
                        ComandoINCLUIR_TABE.ExecuteNonQuery();
                        MessageBox.Show("Permissão inserida com sucesso", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        TabPermi_MET MET = new TabPermi_MET();
                        MET.ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
                        MET.DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);

                        txtCodigoBl2.Select();
                        txtCodigoBl2.SelectAll();
                        txtDescicaoBl2.Text = string.Empty;

                        btnGravar.Enabled = false;
                    }
                    catch (SqlException Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método _ButtonINC_BLOC()\n\nBLOCO = INSERIR PERMISSÃO UNITÁRIA\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método _ButtonINC_BLOC()\n\nBLOCO = INSERIR PERMISSÃO UNITÁRIA\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Conexão.Close();
                    }

                    #endregion

                }
            }
        }




        public void _ButtonALT_BLOC(TextBox txtUsuarCodigo, Button btnGravar, Panel panUsuarioAb1, MethodInvoker CamposEnable_grb1, MethodInvoker CamposDisable_grb1, MethodInvoker CamposEnable_grb2, MethodInvoker CamposDisable_grb2, MethodInvoker ZerarCampos_grb1, MethodInvoker ZerarCampos_grb2, TextBox txtUsuarDescri, RadioButton rabAb1Blocos, RadioButton rabAb1Unitario, TextBox txtCodigoBl2, TextBox txtDescicaoBl2, GroupBox grb2, TextBox txtMESTRE, CheckBox chePerIncBl2, CheckBox chePerAltBl2, CheckBox chePerExcBl2, CheckBox cheConAltBl2, CheckBox cheBloqAbasBl2, NumericUpDown nupDown1Bl2, NumericUpDown nupDown2Bl2, NumericUpDown nupDown3Bl2, NumericUpDown nupDown4Bl2, Button btnMarcarTodasBl2, Button btnPassAntBl2, Button btnPassProxBl2, CheckBox chePerIncBl1, CheckBox chePerAltBl1, CheckBox chePerExcBl1, CheckBox cheConAltBl1, CheckBox cheBloqAbasBl1, NumericUpDown nupDown1Bl1, NumericUpDown nupDown2Bl1, NumericUpDown nupDown3Bl1, NumericUpDown nupDown4Bl1, Button btnMarcarTodasBl1, Button btnPassAntBl1, Button btnPassProxBl1, TextBox txtUsuario, ComboBox comBlocoBl1)
        {
            if (txtMESTRE.Text == "ALTERAR")
            {
                if (rabAb1Blocos.Checked == true)
                {
                    #region BLOCO
                    try
                    {
                        //Cria a conexão com o Banco de Dados e Abre!
                        StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                        string LerString = StringConexão.ReadLine();
                        SqlConnection Conexão = new SqlConnection(LerString);
                        Conexão.Open();

                        SqlConnection Conexão_READER = new SqlConnection(LerString);
                        Conexão_READER.Open();


                        String CapProgramasCada = "";


                        #region SELECTs
                        if (comBlocoBl1.SelectedIndex == 0)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr WHERE Sequen_PGR > 10000 AND Sequen_PGR < 30000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 60000 AND Sequen_PGR < 70000";
                        }
                        if (comBlocoBl1.SelectedIndex == 1)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr WHERE Sequen_PGR > 10000 AND Sequen_PGR < 30000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 60000 AND Sequen_PGR < 70000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 50000 AND Sequen_PGR < 60000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 30000 AND Sequen_PGR < 40000";
                        }
                        if (comBlocoBl1.SelectedIndex == 2)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr WHERE Sequen_PGR > 10000 AND Sequen_PGR < 30000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 60000 AND Sequen_PGR < 70000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 50000 AND Sequen_PGR < 60000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 30000 AND Sequen_PGR < 40000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 40000 AND Sequen_PGR < 50000";
                        }
                        if (comBlocoBl1.SelectedIndex == 3)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr WHERE Sequen_PGR > 10000 AND Sequen_PGR < 30000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 60000 AND Sequen_PGR < 70000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 50000 AND Sequen_PGR < 60000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 30000 AND Sequen_PGR < 40000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 40000 AND Sequen_PGR < 50000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 70000 AND Sequen_PGR < 80000";
                        }
                        if (comBlocoBl1.SelectedIndex == 4)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr WHERE Sequen_PGR < 90000";
                        }
                        if (comBlocoBl1.SelectedIndex == 5)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr";
                        }
                        #endregion


                        SqlCommand cmd_CapProgramasCada = new SqlCommand(CapProgramasCada, Conexão_READER);
                        SqlDataReader Dr_Progr = cmd_CapProgramasCada.ExecuteReader(CommandBehavior.CloseConnection);


                        while (Dr_Progr.Read())
                        {
                            //Pega o Ultimo registro da Tab de Historico!
                            string PegarULTIMORegHIST = "SELECT MAX (Sequen_HIS + 1) FROM TabHisto";
                            SqlCommand PegarREG = new SqlCommand(PegarULTIMORegHIST, Conexão);
                            SqlDataReader Dr = PegarREG.ExecuteReader(); Dr.Read();
                            int _SequenHIS = Convert.ToInt32(Dr[0].ToString());
                            Dr.Close();



                            string NumeroPrograma = Dr_Progr[0].ToString();

                            //String e cria o SQLComand
                            string StringComandoALTERAR_TABE = "UPDATE TabPermi SET PerINC_PER = @PerINC,PerALT_PER = @PerALT,PerEXC_PER = @PerEXC,PerCON_PER = @PerCON,PerABA_PER = @PerABA,PerAb1_PER = @PerAb1,PerAb2_PER = @PerAb2,PerAb3_PER = @PerAb3,PerAb4_PER = @PerAb4 WHERE SeqUsu_PER = @SeqUsu AND SeqPgr_PER = @SeqPgr";
                            string StringComandoALTERAR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'090300','ALTERAÇÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                            SqlCommand ComandoALTERAR_HIST = new SqlCommand(StringComandoALTERAR_HIST, Conexão);
                            SqlCommand ComandoALTERAR_TABE = new SqlCommand(StringComandoALTERAR_TABE, Conexão);


                            //Parametros do Insert na tabela
                            ComandoALTERAR_TABE.Parameters.Add("@SeqUsu", SqlDbType.Int).Value = txtUsuarCodigo.Text;
                            ComandoALTERAR_TABE.Parameters.Add("@SeqPgr", SqlDbType.VarChar).Value = NumeroPrograma;
                            ComandoALTERAR_TABE.Parameters.Add("@PerINC", SqlDbType.VarChar).Value = chePerIncBl1.Checked;
                            ComandoALTERAR_TABE.Parameters.Add("@PerALT", SqlDbType.VarChar).Value = chePerAltBl1.Checked;
                            ComandoALTERAR_TABE.Parameters.Add("@PerEXC", SqlDbType.VarChar).Value = chePerExcBl1.Checked;
                            ComandoALTERAR_TABE.Parameters.Add("@PerCON", SqlDbType.VarChar).Value = cheConAltBl1.Checked;
                            ComandoALTERAR_TABE.Parameters.Add("@PerABA", SqlDbType.VarChar).Value = cheBloqAbasBl1.Checked;
                            ComandoALTERAR_TABE.Parameters.Add("@PerAb1", SqlDbType.Int).Value = Convert.ToInt32(nupDown1Bl1.Value);
                            ComandoALTERAR_TABE.Parameters.Add("@PerAb2", SqlDbType.Int).Value = Convert.ToInt32(nupDown2Bl1.Value);
                            ComandoALTERAR_TABE.Parameters.Add("@PerAb3", SqlDbType.Int).Value = Convert.ToInt32(nupDown3Bl1.Value);
                            ComandoALTERAR_TABE.Parameters.Add("@PerAb4", SqlDbType.Int).Value = Convert.ToInt32(nupDown4Bl1.Value);

                            //Parametros do Insert no banco
                            ComandoALTERAR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                            ComandoALTERAR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "ALTERAÇÃO DA PERMISSÃO.: " + NumeroPrograma + " PARA O USUÁRIO.: " + txtUsuarCodigo.Text;
                            ComandoALTERAR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = "INC.: " + chePerIncBl1.Checked + " | ALT.: " + chePerAltBl1.Checked + " | EXC.: " + chePerExcBl1.Checked;
                            ComandoALTERAR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                            ComandoALTERAR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();

                            try
                            {
                                ComandoALTERAR_TABE.ExecuteNonQuery();
                                ComandoALTERAR_HIST.ExecuteNonQuery();
                            }
                            catch (SqlException Ex)
                            {
                                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método _ButtonALT_BLOC()\n\nBLOCO = ALTERAR PERMISSÃO NO BLOCO\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }



                        MessageBox.Show("Permissão alterada com sucesso", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        TabPermi_MET MET = new TabPermi_MET();

                        comBlocoBl1.Enabled = false;
                        comBlocoBl1.SelectedIndex = -1;
                        MET.ZeraCamposChecked(chePerIncBl1, chePerAltBl1, chePerExcBl1, cheConAltBl1, cheBloqAbasBl1, nupDown1Bl1, nupDown2Bl1, nupDown3Bl1, nupDown4Bl1, null);
                        MET.DesabilitarCamposChecked(chePerIncBl1, chePerAltBl1, chePerExcBl1, cheConAltBl1, cheBloqAbasBl1, nupDown1Bl1, nupDown2Bl1, nupDown3Bl1, nupDown4Bl1, btnMarcarTodasBl1, btnPassAntBl1, btnPassProxBl1);

                        txtUsuarCodigo.Select();
                        txtUsuarCodigo.SelectAll();
                        txtUsuarDescri.Text = string.Empty;

                        btnGravar.Enabled = false;
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }





                    #endregion
                }
                else
                {
                    #region UNITÁRIO
                    //Cria a conexão com o Banco de Dados e Abre!
                    StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                    string LerString = StringConexão.ReadLine();
                    SqlConnection Conexão = new SqlConnection(LerString);
                    Conexão.Open();

                    //Pega o Ultimo registro da Tab de Historico!
                    string PegarULTIMORegHIST = "SELECT MAX (Sequen_HIS + 1) FROM TabHisto";
                    SqlCommand PegarREG = new SqlCommand(PegarULTIMORegHIST, Conexão);
                    SqlDataReader Dr = PegarREG.ExecuteReader(); Dr.Read();
                    int _Sequen = Convert.ToInt32(Dr[0].ToString());
                    Dr.Close();

                    //String e cria o SQLComand
                    string StringComandoALTERAR_TABE = "UPDATE TabPermi SET PerINC_PER = @PerINC,PerALT_PER = @PerALT,PerEXC_PER = @PerEXC,PerCON_PER = @PerCON,PerABA_PER = @PerABA,PerAb1_PER = @PerAb1,PerAb2_PER = @PerAb2,PerAb3_PER = @PerAb3,PerAb4_PER = @PerAb4 WHERE SeqUsu_PER = @SeqUsu AND SeqPgr_PER = @SeqPgr";
                    string StringComandoALTERAR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'090300','ALTERAÇÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                    SqlCommand ComandoALTERAR_HIST = new SqlCommand(StringComandoALTERAR_HIST, Conexão);
                    SqlCommand ComandoALTERAR_TABE = new SqlCommand(StringComandoALTERAR_TABE, Conexão);

                    //Parametros do Insert na tabela
                    ComandoALTERAR_TABE.Parameters.Add("@SeqUsu", SqlDbType.Int).Value = txtUsuarCodigo.Text;
                    ComandoALTERAR_TABE.Parameters.Add("@SeqPgr", SqlDbType.VarChar).Value = txtCodigoBl2.Text;
                    ComandoALTERAR_TABE.Parameters.Add("@PerINC", SqlDbType.VarChar).Value = chePerIncBl2.Checked;
                    ComandoALTERAR_TABE.Parameters.Add("@PerALT", SqlDbType.VarChar).Value = chePerAltBl2.Checked;
                    ComandoALTERAR_TABE.Parameters.Add("@PerEXC", SqlDbType.VarChar).Value = chePerExcBl2.Checked;
                    ComandoALTERAR_TABE.Parameters.Add("@PerCON", SqlDbType.VarChar).Value = cheConAltBl2.Checked;
                    ComandoALTERAR_TABE.Parameters.Add("@PerABA", SqlDbType.VarChar).Value = cheBloqAbasBl2.Checked;
                    ComandoALTERAR_TABE.Parameters.Add("@PerAb1", SqlDbType.Int).Value = Convert.ToInt32(nupDown1Bl2.Value);
                    ComandoALTERAR_TABE.Parameters.Add("@PerAb2", SqlDbType.Int).Value = Convert.ToInt32(nupDown2Bl2.Value);
                    ComandoALTERAR_TABE.Parameters.Add("@PerAb3", SqlDbType.Int).Value = Convert.ToInt32(nupDown3Bl2.Value);
                    ComandoALTERAR_TABE.Parameters.Add("@PerAb4", SqlDbType.Int).Value = Convert.ToInt32(nupDown4Bl2.Value);

                    //Parametros do Insert no banco
                    ComandoALTERAR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _Sequen;
                    ComandoALTERAR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "ALTERAÇÃO DA PERMISSÃO.: " + txtCodigoBl2.Text + " PARA O USUÁRIO.: " + txtUsuarCodigo.Text;
                    ComandoALTERAR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = "INC.: " + chePerIncBl2.Checked + " | ALT.: " + chePerAltBl2.Checked + " | EXC.: " + chePerExcBl2.Checked;
                    ComandoALTERAR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                    ComandoALTERAR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();

                    try
                    {
                        ComandoALTERAR_HIST.ExecuteNonQuery();
                        ComandoALTERAR_TABE.ExecuteNonQuery();
                        MessageBox.Show("Dados alterados com sucesso", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TabPermi_MET MET = new TabPermi_MET();
                        MET.ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
                        MET.DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);

                        txtCodigoBl2.Select();
                        txtCodigoBl2.SelectAll();
                        txtDescicaoBl2.Text = string.Empty;

                        btnGravar.Enabled = false;
                    }
                    catch (SqlException Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método _ButtonALT_BLOC()\n\nBLOCO = ALTERAR PERMISSÃO UNITÁRIA\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método _ButtonALT_BLOC()\n\nBLOCO = ALTERAR PERMISSÃO UNITÁRIA\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Conexão.Close();
                    }
                }
                    #endregion
            }
        }




        public void _ButtonEXC_BLOC(TextBox txtUsuarCodigo, Button btnGravar, Panel panUsuarioAb1, MethodInvoker CamposEnable_grb1, MethodInvoker CamposDisable_grb1, MethodInvoker CamposEnable_grb2, MethodInvoker CamposDisable_grb2, MethodInvoker ZerarCampos_grb1, MethodInvoker ZerarCampos_grb2, TextBox txtUsuarDescri, RadioButton rabAb1Blocos, RadioButton rabAb1Unitario, TextBox txtCodigoBl2, TextBox txtDescicaoBl2, GroupBox grb2, TextBox txtMESTRE, CheckBox chePerIncBl2, CheckBox chePerAltBl2, CheckBox chePerExcBl2, CheckBox cheConAltBl2, CheckBox cheBloqAbasBl2, NumericUpDown nupDown1Bl2, NumericUpDown nupDown2Bl2, NumericUpDown nupDown3Bl2, NumericUpDown nupDown4Bl2, Button btnMarcarTodasBl2, Button btnPassAntBl2, Button btnPassProxBl2, CheckBox chePerIncBl1, CheckBox chePerAltBl1, CheckBox chePerExcBl1, CheckBox cheConAltBl1, CheckBox cheBloqAbasBl1, NumericUpDown nupDown1Bl1, NumericUpDown nupDown2Bl1, NumericUpDown nupDown3Bl1, NumericUpDown nupDown4Bl1, Button btnMarcarTodasBl1, Button btnPassAntBl1, Button btnPassProxBl1, TextBox txtUsuario, ComboBox comBlocoBl1, string MotivoBLOC, string MotivoUNIT, int Index)
        {
            if (txtMESTRE.Text == "EXCLUIR")
            {
                if (rabAb1Blocos.Checked == true)
                {
                    #region BLOCO
                    try
                    {
                        //Cria a conexão com o Banco de Dados e Abre!
                        StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                        string LerString = StringConexão.ReadLine();
                        SqlConnection Conexão = new SqlConnection(LerString);
                        Conexão.Open();

                        SqlConnection Conexão_READER = new SqlConnection(LerString);
                        Conexão_READER.Open();


                        String CapProgramasCada = "";


                        #region SELECTs
                        if (Index == 0)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr WHERE Sequen_PGR > 10000 AND Sequen_PGR < 30000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 60000 AND Sequen_PGR < 70000";
                        }
                        if (Index == 1)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr WHERE Sequen_PGR > 10000 AND Sequen_PGR < 30000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 60000 AND Sequen_PGR < 70000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 50000 AND Sequen_PGR < 60000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 30000 AND Sequen_PGR < 40000";
                        }
                        if (Index == 2)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr WHERE Sequen_PGR > 10000 AND Sequen_PGR < 30000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 60000 AND Sequen_PGR < 70000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 50000 AND Sequen_PGR < 60000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 30000 AND Sequen_PGR < 40000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 40000 AND Sequen_PGR < 50000";
                        }
                        if (Index == 3)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr WHERE Sequen_PGR > 10000 AND Sequen_PGR < 30000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 60000 AND Sequen_PGR < 70000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 50000 AND Sequen_PGR < 60000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 30000 AND Sequen_PGR < 40000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 40000 AND Sequen_PGR < 50000 UNION ALL SELECT * FROM TabProgr WHERE Sequen_PGR > 70000 AND Sequen_PGR < 80000";
                        }
                        if (Index == 4)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr WHERE Sequen_PGR < 90000";
                        }
                        if (Index == 5)
                        {
                            CapProgramasCada = "SELECT * FROM TabProgr";
                        }
                        #endregion


                        SqlCommand cmd_CapProgramasCada = new SqlCommand(CapProgramasCada, Conexão_READER);
                        SqlDataReader Dr_Progr = cmd_CapProgramasCada.ExecuteReader(CommandBehavior.CloseConnection);

                        comBlocoBl1.SelectedIndex = Index;

                        string IndexMsg = Convert.ToString(Index + 1);

                        DialogResult APAGAR = MessageBox.Show("Deseja remover a permissão do usuário " + txtUsuarCodigo.Text + " de todo o BLOCO " + IndexMsg + "?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (APAGAR == DialogResult.Yes)
                        {


                            while (Dr_Progr.Read())
                            {
                                //Pega o Ultimo registro da Tab de Historico!
                                string PegarULTIMORegHIST = "SELECT MAX (Sequen_HIS + 1) FROM TabHisto";
                                SqlCommand PegarREG = new SqlCommand(PegarULTIMORegHIST, Conexão);
                                SqlDataReader Dr = PegarREG.ExecuteReader(); Dr.Read();
                                int _SequenHIS = Convert.ToInt32(Dr[0].ToString());
                                Dr.Close();



                                string NumeroPrograma = Dr_Progr[0].ToString();

                                //String e cria o SQLComand
                                string StringComandoEXCLUIR_TABE = "DELETE FROM TabPermi WHERE SeqUsu_PER = @SeqUsu AND SeqPgr_PER = @SeqPgr";
                                string StringComandoEXCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'090300','EXCLUSÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                                SqlCommand ComandoEXCLUIR_HIST = new SqlCommand(StringComandoEXCLUIR_HIST, Conexão);
                                SqlCommand ComandoEXCLUIR_TABE = new SqlCommand(StringComandoEXCLUIR_TABE, Conexão);


                                //Parametros do Insert na tabela
                                ComandoEXCLUIR_TABE.Parameters.Add("@SeqUsu", SqlDbType.Int).Value = txtUsuarCodigo.Text;
                                ComandoEXCLUIR_TABE.Parameters.Add("@SeqPgr", SqlDbType.VarChar).Value = NumeroPrograma;


                                //Parametros do Exc no banco
                                ComandoEXCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                                ComandoEXCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "EXCLUSÃO DA PERMISSÃO..: " + NumeroPrograma + " DO USUÁRIO..: " + txtUsuarCodigo.Text;
                                ComandoEXCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = "Motivo.: " + MotivoBLOC;
                                ComandoEXCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                                ComandoEXCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now;

                                try
                                {
                                    ComandoEXCLUIR_HIST.ExecuteNonQuery();
                                    ComandoEXCLUIR_TABE.ExecuteNonQuery();
                                }
                                catch (SqlException Ex)
                                {
                                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método _ButtonALT_BLOC()\n\nBLOCO = ALTERAR PERMISSÃO NO BLOCO\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }



                            MessageBox.Show("Permissão excluida com sucesso", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            TabPermi_MET MET = new TabPermi_MET();

                            comBlocoBl1.Enabled = false;
                            comBlocoBl1.SelectedIndex = -1;
                            MET.ZeraCamposChecked(chePerIncBl1, chePerAltBl1, chePerExcBl1, cheConAltBl1, cheBloqAbasBl1, nupDown1Bl1, nupDown2Bl1, nupDown3Bl1, nupDown4Bl1, null);
                            MET.DesabilitarCamposChecked(chePerIncBl1, chePerAltBl1, chePerExcBl1, cheConAltBl1, cheBloqAbasBl1, nupDown1Bl1, nupDown2Bl1, nupDown3Bl1, nupDown4Bl1, btnMarcarTodasBl1, btnPassAntBl1, btnPassProxBl1);

                            txtUsuarCodigo.Select();
                            txtUsuarCodigo.SelectAll();
                            txtUsuarDescri.Text = string.Empty;

                            btnGravar.Enabled = false;
                        }
                        else
                        {
                            TabPermi_MET MET = new TabPermi_MET();

                            comBlocoBl1.Enabled = false;
                            comBlocoBl1.SelectedIndex = -1;
                            MET.ZeraCamposChecked(chePerIncBl1, chePerAltBl1, chePerExcBl1, cheConAltBl1, cheBloqAbasBl1, nupDown1Bl1, nupDown2Bl1, nupDown3Bl1, nupDown4Bl1, null);
                            MET.DesabilitarCamposChecked(chePerIncBl1, chePerAltBl1, chePerExcBl1, cheConAltBl1, cheBloqAbasBl1, nupDown1Bl1, nupDown2Bl1, nupDown3Bl1, nupDown4Bl1, btnMarcarTodasBl1, btnPassAntBl1, btnPassProxBl1);

                            txtUsuarCodigo.Select();
                            txtUsuarCodigo.SelectAll();
                            txtUsuarDescri.Text = string.Empty;

                            btnGravar.Enabled = false;
                        }
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }





                    #endregion
                }
                else
                {
                    #region UNITÁRIO
                    //Cria a conexão com o Banco de Dados e Abre!
                    StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                    string LerString = StringConexão.ReadLine();
                    SqlConnection Conexão = new SqlConnection(LerString);
                    Conexão.Open();

                    //Pega o Ultimo registro da Tab de Historico!
                    string PegarULTIMORegHIST = "SELECT MAX (Sequen_HIS + 1) FROM TabHisto";
                    SqlCommand PegarREG = new SqlCommand(PegarULTIMORegHIST, Conexão);
                    SqlDataReader Dr = PegarREG.ExecuteReader(); Dr.Read();
                    int _Sequen = Convert.ToInt32(Dr[0].ToString());
                    Dr.Close();

                    //String e cria o SQLComand
                    string StringComandoEXCLUIR_TABE = "DELETE FROM TabPermi WHERE SeqUsu_PER = @SeqUsu AND SeqPgr_PER = @SeqPgr";
                    string StringComandoEXCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'090300','EXCLUSÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                    SqlCommand ComandoEXCLUIR_HIST = new SqlCommand(StringComandoEXCLUIR_HIST, Conexão);
                    SqlCommand ComandoEXCLUIR_TABE = new SqlCommand(StringComandoEXCLUIR_TABE, Conexão);

                    //Parametros do Exc na tabela
                    ComandoEXCLUIR_TABE.Parameters.Add("@SeqUsu", SqlDbType.Int).Value = txtUsuarCodigo.Text;
                    ComandoEXCLUIR_TABE.Parameters.Add("@SeqPgr", SqlDbType.VarChar).Value = txtCodigoBl2.Text;


                    //Parametros do Exc no banco
                    ComandoEXCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _Sequen;
                    ComandoEXCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "EXCLUSÃO DA PERMISSÃO..: " + txtCodigoBl2.Text + " DO USUÁRIO..: " + txtUsuarCodigo.Text;
                    ComandoEXCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = "Motivo.: " + MotivoUNIT;
                    ComandoEXCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                    ComandoEXCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now;


                    DialogResult Exc = MessageBox.Show("Deseja DELETAR a permissão?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (Exc == DialogResult.Yes)
                    {
                        try
                        {
                            ComandoEXCLUIR_HIST.ExecuteNonQuery();
                            ComandoEXCLUIR_TABE.ExecuteNonQuery();
                            MessageBox.Show("Atenção.: Registro DELETADO. Esta opção não contem LIXEIRA", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            TabPermi_MET MET = new TabPermi_MET();
                            MET.ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
                            MET.DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);

                            txtCodigoBl2.Select();
                            txtCodigoBl2.SelectAll();
                            txtDescicaoBl2.Text = string.Empty;

                            btnGravar.Enabled = false;
                        }
                        catch (SqlException Ex)
                        {
                            MessageBox.Show("Erro na Exclusão de Dados (TabPermi-EXC)", "TechSIS BWK Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show("Erro na Exclusão de Dados (TabPermi-EXC)", Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        { 
                            Conexão.Close();
                        }



                        if (Exc == DialogResult.No)
                        {
                            txtDescicaoBl2.Text = string.Empty;
                        }
                    }
                    #endregion
                }
            }
        }




    }
}
