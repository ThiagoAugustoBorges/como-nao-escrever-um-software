using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace ReportRPV
{
    internal class ReportRPV_FeedsReport : ReportRPV_TechSIS
    {
        static string FeedsFileName = string.Empty;
        static string FeedsCompanyCode = string.Empty;

        public ReportRPV_FeedsReport(string FileName, string CompanyCode)
            : base (FileName,CompanyCode)
        {
            FeedsFileName = FileName;
            FeedsCompanyCode = CompanyCode;
        }


        //Conexão
        private SqlConnection Conexão { get; set; }
        private void SqlConexão()
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            Conexão = new SqlConnection(LerString);
            Conexão.Open();
        }


        


        //ALIMENTA PARA O FORMULÁRIO TabProgr
        public void TabProgr_Report(StreamWriter Writer_Arq, params Control[] ThisControles)
        {
            if (FeedsFileName == "TabProgr")
            {
                SqlConexão();

                String Select_CMD = String.Format("SELECT * FROM TabProgr WHERE 1=1");


                int SelectedIndex = (ThisControles[1] as ComboBox).SelectedIndex;
                bool Checked = (ThisControles[2] as RadioButton).Checked;


                if (SelectedIndex > 0)
                    Select_CMD += " AND Status_PGR = @Status_PGR";
                if (Checked == true)
                    Select_CMD += " ORDER BY Descri_PGR";



                SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);
                Comando.Parameters.Add("@Status_PGR", SqlDbType.Int).Value = SelectedIndex;

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader();

                    int FeedsDefineNovaPagina = 0;
                    ReportRPV_Statics.FeedsQtResultados = 0;

                    while (Dr.Read())
                    {
                        ThisControles[0].Text = ReportRPV_Statics.FeedsQtResultados.ToString("000000");

                        if (Dr.HasRows)
                        {
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{720}" + Dr["Sequen_PGR"]);
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{1860}" + Dr["Descri_PGR"]);

                            //TRANSFORMA 1 2 E 3 EM ATIVO, INATIVO E EXCLUIDO
                            #region TRATAMENTO String STATUS
                            string Status_PGR = Dr["Status_PGR"].ToString();

                            if (Convert.ToInt32(Status_PGR) == 1)
                            {
                                Status_PGR = "ATIVO";
                            }
                            else if (Convert.ToInt32(Status_PGR) == 2)
                            {
                                Status_PGR = "INATIVO";
                            }
                            else if (Convert.ToInt32(Status_PGR) == 3)
                            {
                                Status_PGR = "EXCLUIDO";
                            }
                            else
                            {
                                Status_PGR = "ERRO..!";
                            }

                            #endregion

                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{10080}" + Status_PGR + @"{\n}");


                            FeedsDefineNovaPagina++;
                            ReportRPV_Statics.FeedsQtResultados++;

                            if (FeedsDefineNovaPagina == 65)
                            {
                                Writer_Arq.WriteLine("{\\np}");
                                Writer_Arq.WriteLine(@"[data]");

                                ReportRPV_AboutCompany Company = new ReportRPV_AboutCompany(FeedsFileName, FeedsCompanyCode);
                                ReportRPV_ColunsDesign Coluns = new ReportRPV_ColunsDesign(FeedsFileName, FeedsCompanyCode);
                                //Escreve a empresa novamente
                                Company.CompanyFields(Writer_Arq);
                                //Escreve as colunas novamente
                                Coluns.DefiningColumns(Writer_Arq);


                                FeedsDefineNovaPagina = 0;
                            }

                            ReportRPV_Statics.FeedsEncontrouInformação = "SIM";
                        }
                        else
                        {
                            ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                        }
                    }
                }
                catch (SqlException Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_Report()\n\nBLOCO = CLASSE ReportRPV_FeedsReport\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_Report()\n\nBLOCO = CLASSE ReportRPV_FeedsReport\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }




        //ALIMENTA PARA O FORMULÁRIO TabUsuar
        public void TabUsuar_ImpreRPV_Ali(RadioButton rabOrdemAlfabetica, RadioButton rabOrdemAlfabeticaApelid, ComboBox comSituacaoIMP, ComboBox comPermissaoIMP, TextBox txtEmpreCodImp, CheckBox cheImpApelid, StreamWriter Writer_Arq, string NomeDoRelatorio, string LojaLogada)
        {
            if (FeedsFileName == "TabUsuar")
            {
               
                String Select_CMD = String.Format("SELECT * FROM TabUsuar WHERE 1=1");

                if (comSituacaoIMP.SelectedIndex > 0)
                    Select_CMD += " AND Status_USU = " + comSituacaoIMP.SelectedIndex;
                if (comPermissaoIMP.SelectedIndex > 0)
                    Select_CMD += " AND Tipo01_USU = " + comPermissaoIMP.SelectedIndex;
                if (txtEmpreCodImp.Text != string.Empty)
                    Select_CMD += " AND CodLoj_USU = " + txtEmpreCodImp.Text;


                if (rabOrdemAlfabetica.Checked == true)
                    Select_CMD += " ORDER BY Descri_USU";
                if (rabOrdemAlfabeticaApelid.Checked == true)
                    Select_CMD += " ORDER BY Apelid_USU";



                SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader();
                    int DefineNovaPagina = 0;
                    ReportRPV_Statics.FeedsQtResultados = 0;
                    while (Dr.Read())
                    {
                        if (Dr.HasRows)
                        {
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{720}" + Dr["Sequen_USU"].ToString().PadLeft(6, '0'));

                            if (cheImpApelid.Checked == true)
                            {
                                Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{1860}" + Dr["Apelid_USU"]);
                            }
                            else
                            {
                                Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{1860}" + Dr["Descri_USU"]);
                            }

                            //TRANSFORMA 1 2 E 3 EM ATIVO, INATIVO E EXCLUIDO
                            #region TRATAMENTO String PERMISSÃO
                            string Tipo01_USU = Dr["Tipo01_USU"].ToString();

                            if (Convert.ToInt32(Tipo01_USU) == 1)
                            {
                                Tipo01_USU = "TOTAL";
                            }
                            else if (Convert.ToInt32(Tipo01_USU) == 2)
                            {
                                Tipo01_USU = "LIMITADO";
                            }
                            else if (Convert.ToInt32(Tipo01_USU) == 3)
                            {
                                Tipo01_USU = "RESTRITO";
                            }
                            else
                            {
                                Tipo01_USU = "ERRO..!";
                            }

                            #endregion

                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{8400}" + Tipo01_USU);

                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{10260}" + Dr["CodLoj_USU"].ToString().PadLeft(6, '0') + @"{\n}");


                            DefineNovaPagina++;
                            ReportRPV_Statics.FeedsQtResultados++;

                            if (DefineNovaPagina == 65)
                            {
                                Writer_Arq.WriteLine("{\\np}");
                                Writer_Arq.WriteLine(@"[data]");


                                ReportRPV_AboutCompany Company = new ReportRPV_AboutCompany(FeedsFileName, FeedsCompanyCode);
                                ReportRPV_ColunsDesign Coluns = new ReportRPV_ColunsDesign(FeedsFileName, FeedsCompanyCode);
                                Company.CompanyFields(Writer_Arq);
                                Coluns.DefiningColumns(Writer_Arq);


                                DefineNovaPagina = 0;
                            }

                            ReportRPV_Statics.FeedsEncontrouInformação = "SIM";
                        }
                        else
                        {
                            ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                        }
                    }
                }
                catch (SqlException Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA O FOMULÁRIO TabPermi
        public void TabPermi_ImpreRPV_Ali(RadioButton rabOrdemAlfabetica,TextBox txtImpProgrCod, TextBox txtImpUsuarCod, CheckBox cheImpApelid, StreamWriter Writer_Arq, string NomeDoRelatorio, string LojaLogada)
        {
            if (FeedsFileName == "TabPermi")
            {
               
                String Select_CMD = String.Format("SELECT SeqUsu_PER,LEFT (Apelid_USU, 37) AS Apelid_USU, LEFT (Descri_USU, 37) AS Descri_USU,SeqPgr_PER,LEFT (Descri_PGR,19) AS Descri_PGR,PerINC_PER,PerALT_PER,PerEXC_PER,PerCON_PER,PerABA_PER,PerAb1_PER,PerAb2_PER,PerAb3_PER,PerAb4_PER FROM TabPermi INNER JOIN TabProgr ON TabPermi.SeqPgr_PER = TabProgr.Sequen_PGR INNER JOIN TabUsuar ON TabPermi.SeqUsu_PER = TabUsuar.Sequen_USU WHERE 1=1");


                if (txtImpProgrCod.Text != string.Empty)
                    Select_CMD += " AND SeqPgr_PER = " + txtImpProgrCod.Text;
                if (txtImpUsuarCod.Text != string.Empty)
                    Select_CMD += " AND SeqUsu_PER = " + txtImpUsuarCod.Text;


                if (rabOrdemAlfabetica.Checked == true)
                    Select_CMD += " ORDER BY Descri_PGR";
                else
                    Select_CMD += " ORDER BY SeqPgr_PER";




                SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader();
                    int DefineNovaPagina = 0;
                    ReportRPV_Statics.FeedsQtResultados = 0;
                    while (Dr.Read())
                    {
                        if (Dr.HasRows)
                        {
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{720}" + Dr["SeqUsu_PER"].ToString().PadLeft(6, '0'));

                            if (cheImpApelid.Checked == true)
                            {
                                Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{1650}" + Dr["Apelid_USU"]);
                            }
                            else
                            {
                                Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{1650}" + Dr["Descri_USU"]);
                            }

                            #region COLOR EM INCLUIR
                            string PerINC = Dr["PerINC_PER"].ToString();
                            if (PerINC == "False")
                            {
                                PerINC = "NÃO";
                            }
                            else
                            {
                                PerINC = "SIM";
                            }
                            #endregion

                            #region COLOR EM ALTERAR
                            string PerALT = Dr["PerALT_PER"].ToString();
                            if (PerALT == "False")
                            {
                                PerALT = "NÃO";
                            }
                            else
                            {
                                PerALT = "SIM";
                            }
                            #endregion

                            #region COLOR EM EXCLUIR
                            string PerEXC = Dr["PerEXC_PER"].ToString();
                            if (PerEXC == "False")
                            {
                                PerEXC = "NÃO";
                            }
                            else
                            {
                                PerEXC = "SIM";
                            }
                            #endregion

                            #region COLOR EM CONSULTAR
                            string PerCON = Dr["PerCON_PER"].ToString();
                            if (PerCON == "False")
                            {
                                PerCON = "NÃO";
                            }
                            else
                            {
                                PerCON = "SIM";
                            }
                            #endregion

                            #region COLOR EM ABAS
                            string PerABA = Dr["PerABA_PER"].ToString();
                            if (PerABA == "False")
                            {
                                PerABA = "NÃO";
                            }
                            else
                            {
                                PerABA = "SIM";
                            }
                            #endregion


                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{5790}" + Dr["SeqPgr_PER"].ToString() + " " + Dr["Descri_PGR"].ToString());

                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{8800}" + PerINC);
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{9350}" + PerALT);
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{9870}" + PerEXC);
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{10410}" + PerCON);
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{10970}" + PerABA + @"{\n}");


                            DefineNovaPagina++;
                            ReportRPV_Statics.FeedsQtResultados++;
                           
                            if (DefineNovaPagina == 65)
                            {
                                Writer_Arq.WriteLine("{\\np}");
                                Writer_Arq.WriteLine(@"[data]");


                                ReportRPV_AboutCompany Company = new ReportRPV_AboutCompany(FeedsFileName, FeedsCompanyCode);
                                ReportRPV_ColunsDesign Coluns = new ReportRPV_ColunsDesign(FeedsFileName, FeedsCompanyCode);
                                Company.CompanyFields(Writer_Arq);
                                Coluns.DefiningColumns(Writer_Arq);

                                DefineNovaPagina = 0;
                            }

                            ReportRPV_Statics.FeedsEncontrouInformação = "SIM";
                        }
                        else
                        {
                            ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                        }
                    }
                }
                catch (SqlException Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA O FOMULÁRIO TabPermi
        public void TabCidad_ImpreRPV_Ali(RadioButton rabOrdemAlfabetica, StreamWriter Writer_Arq, string NomeDoRelatorio, string LojaLogada, ComboBox comImpStatus)
        {
            if (FeedsFileName == "TabCidad")
            {
                
                String Select_CMD = String.Format("SELECT Sequen_CID,Descri_CID,UfFede_CID,Descri_PAI,IbgeMu_CID,Status_CID FROM TabCidad INNER JOIN TabPaise ON TabCidad.PaisCi_CID = TabPaise.Codigo_PAI WHERE 1=1");


                if (comImpStatus.SelectedIndex > 0)
                    Select_CMD += " AND Status_CID = " + comImpStatus.SelectedIndex;


                if (rabOrdemAlfabetica.Checked == true)
                    Select_CMD += " ORDER BY Descri_CID";
                else
                    Select_CMD += " ORDER BY Sequen_CID";




                SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader();
                    int DefineNovaPagina = 0;
                    ReportRPV_Statics.FeedsQtResultados = 0;
                    while (Dr.Read())
                    {
                        if (Dr.HasRows)
                        {
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{720}" + Dr["Sequen_CID"].ToString().PadLeft(6, '0'));
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{1650}" + Dr["Descri_CID"].ToString());
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{6550}" + Dr["UfFede_CID"].ToString());
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{7140}" + Dr["Descri_PAI"].ToString());
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{9080}" + Dr["IbgeMu_CID"].ToString());



                            #region STATUS
                            int STATUS = Convert.ToInt32(Dr["Status_CID"]);
                            string strSTATUS = "";
                            if (STATUS == 1)
                            {
                                strSTATUS = "ATIVO";
                            }
                            else if (STATUS == 2)
                            {
                                strSTATUS = "INATIVO";
                            }
                            else if (STATUS == 3)
                            {
                                strSTATUS = "EXCLUIDO";
                            }
                            else
                            { 
                                strSTATUS = "ERRO";
                            }
                        
                            #endregion

                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{10180}" + strSTATUS + @"{\n}");



                            DefineNovaPagina++;
                            ReportRPV_Statics.FeedsQtResultados++;

                            if (DefineNovaPagina == 65)
                            {
                                Writer_Arq.WriteLine("{\\np}");
                                Writer_Arq.WriteLine(@"[data]");


                                ReportRPV_AboutCompany Company = new ReportRPV_AboutCompany(FeedsFileName, FeedsCompanyCode);
                                ReportRPV_ColunsDesign Coluns = new ReportRPV_ColunsDesign(FeedsFileName, FeedsCompanyCode);
                                Company.CompanyFields(Writer_Arq);
                                Coluns.DefiningColumns(Writer_Arq);


                                DefineNovaPagina = 0;
                            }

                            ReportRPV_Statics.FeedsEncontrouInformação = "SIM";
                        }
                        else
                        {
                            ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                        }
                    }
                }
                catch (SqlException Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA O FORMULÁRIO TabClien
        public void TabClien_ImpreRPV_Ali(RadioButton rabOrdemAlfabetica, RadioButton rabOrdemAlfabeticaApelid, ComboBox comImpSituacao, ComboBox comImpCategoria, TextBox txtImpEmpreCod, TextBox txtImpCidadeCod, CheckBox cheImpApelid, StreamWriter Writer_Arq, string NomeDoRelatorio, string LojaLogada)
        {
            if (FeedsFileName == "TabClien")
            {
              
                String Select_CMD = String.Format("SELECT Sequen_CLI,LEFT (Descri_CLI, 46) AS Descri_CLI,LEFT (Fantas_CLI, 46) AS Fantas_CLI,LEFT (CpfCnp_CLI, 14) AS CpfCnp_CLI,LEFT (InsEst_CLI, 12) AS InsEst_CLI,TelPab_CLI FROM TabClien WHERE 1=1");

                if (comImpSituacao.SelectedIndex > 0)
                    Select_CMD += " AND Status_CLI = " + comImpSituacao.SelectedIndex;
                if (comImpCategoria.SelectedIndex < 6)
                    Select_CMD += " AND Catego_CLI = " + comImpCategoria.SelectedIndex;

                if (txtImpEmpreCod.Text != string.Empty)
                    Select_CMD += " AND EmpSeq_CLI = " + txtImpEmpreCod.Text;
                if (txtImpCidadeCod.Text != string.Empty)
                    Select_CMD += " AND EndCi1_CLI = " + txtImpCidadeCod.Text;


                if (rabOrdemAlfabetica.Checked == true)
                    Select_CMD += " ORDER BY Descri_CLI";
                if (rabOrdemAlfabeticaApelid.Checked == true)
                    Select_CMD += " ORDER BY Fantas_CLI";



                SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader();
                    int DefineNovaPagina = 0;
                    ReportRPV_Statics.FeedsQtResultados = 0;
                    while (Dr.Read())
                    {
                        if (Dr.HasRows)
                        {                           
                            if (cheImpApelid.Checked == true)
                            {
                                Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{540}" + Dr["Sequen_CLI"].ToString().PadLeft(6, '0') + " " + Dr["Fantas_CLI"].ToString());
                            }
                            else
                            {
                                Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{540}" + Dr["Sequen_CLI"].ToString().PadLeft(6, '0') + " " + Dr["Descri_CLI"].ToString());
                            }

                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{6290}" + FormatarCpfCnpj(Dr["CpfCnp_CLI"].ToString()));
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{8230}" + Dr["InsEst_CLI"].ToString());
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{9960}" + Dr["TelPab_CLI"].ToString() + @"{\n}");


                            DefineNovaPagina++;
                            ReportRPV_Statics.FeedsQtResultados++;

                            if (DefineNovaPagina == 65)
                            {
                                Writer_Arq.WriteLine("{\\np}");
                                Writer_Arq.WriteLine(@"[data]");

                                ReportRPV_AboutCompany Company = new ReportRPV_AboutCompany(FeedsFileName, FeedsCompanyCode);
                                ReportRPV_ColunsDesign Coluns = new ReportRPV_ColunsDesign(FeedsFileName, FeedsCompanyCode);
                                Company.CompanyFields(Writer_Arq);
                                Coluns.DefiningColumns(Writer_Arq);


                                DefineNovaPagina = 0;
                            }

                            ReportRPV_Statics.FeedsEncontrouInformação = "SIM";
                        }
                        else
                        {
                            ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                        }
                    }
                }
                catch (SqlException Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA O FORMULÁRIO TabMsgNt
        public void TabMsgNt_ImpreRPV_Ali(RadioButton rabOrdemAlfabetica, ComboBox comSituacaoIMP, StreamWriter Writer_Arq, string NomeDoRelatorio, string LojaLogada)
        {
            if (FeedsFileName == "TabMsgNt")
            {
              
                String Select_CMD = String.Format("SELECT Sequen_MSG,LEFT (Descri_MSG, 39) AS Descri_MSG,SeqEmp_MSG,LEFT (Descri_EMP, 33) AS Descri_EMP FROM TabMsgNt INNER JOIN TabEmpre ON TabMsgNt.SeqEmp_MSG = TabEmpre.Sequen_EMP WHERE 1=1");

                if (comSituacaoIMP.SelectedIndex > 0)
                    Select_CMD += " AND SeqEmp_MSG = " + LojaLogada;

                if (rabOrdemAlfabetica.Checked == true)
                    Select_CMD += " ORDER BY Descri_MSG";



                SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader();
                    int DefineNovaPagina = 0;
                    ReportRPV_Statics.FeedsQtResultados = 0;
                    while (Dr.Read())
                    {
                        if (Dr.HasRows)
                        {
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{850}" + Dr["Sequen_MSG"].ToString().PadLeft(3, '0'));
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{1560}" + Dr["Descri_MSG"]);
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{6400}" + Dr["SeqEmp_MSG"].ToString().PadLeft(6, '0'));
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{7400}" + Dr["Descri_EMP"].ToString() + " {\\n}");


                            DefineNovaPagina++;
                            ReportRPV_Statics.FeedsQtResultados++;

                            if (DefineNovaPagina == 65)
                            {
                                Writer_Arq.WriteLine("{\\np}");
                                Writer_Arq.WriteLine(@"[data]");


                                ReportRPV_AboutCompany Company = new ReportRPV_AboutCompany(FeedsFileName, FeedsCompanyCode);
                                ReportRPV_ColunsDesign Coluns = new ReportRPV_ColunsDesign(FeedsFileName, FeedsCompanyCode);
                                Company.CompanyFields(Writer_Arq);
                                Coluns.DefiningColumns(Writer_Arq);


                                DefineNovaPagina = 0;
                            }

                            ReportRPV_Statics.FeedsEncontrouInformação = "SIM";
                        }
                        else
                        {
                            ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                        }
                    }
                }
                catch (SqlException Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA O FORMULÁRIO TabCfope
        public void TabCfope_ImpreRPV_Ali(RadioButton rabOrdemAlfabetica, ComboBox comSituacaoIMP, StreamWriter Writer_Arq, string NomeDoRelatorio, string LojaLogada)
        {
            if (FeedsFileName == "TabCfope")
            {
              
                String Select_CMD = String.Format("SELECT Sequen_CFO,Descri_CFO,EntCom_CFO,EntInd_CFO FROM TabCfope WHERE 1=1");

                if (comSituacaoIMP.SelectedIndex < 2)
                    Select_CMD += " AND DenFor_CFO = " + comSituacaoIMP.SelectedIndex;

                if (rabOrdemAlfabetica.Checked == true)
                    Select_CMD += " ORDER BY Descri_CFO";



                SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader();
                    int DefineNovaPagina = 0;
                    ReportRPV_Statics.FeedsQtResultados = 0;
                    while (Dr.Read())
                    {
                        if (Dr.HasRows)
                        {
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{810}" + Dr["Sequen_CFO"].ToString().PadLeft(4, '0'));
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{1600}" + Dr["Descri_CFO"]);
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{9650}" + Dr["EntInd_CFO"].ToString().PadLeft(4, '0'));
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{10610}" + Dr["EntCom_CFO"].ToString().PadLeft(4, '0') + "{\\n}");


                            DefineNovaPagina++;
                            ReportRPV_Statics.FeedsQtResultados++;

                            if (DefineNovaPagina == 65)
                            {
                                Writer_Arq.WriteLine("{\\np}");
                                Writer_Arq.WriteLine(@"[data]");

                                ReportRPV_AboutCompany Company = new ReportRPV_AboutCompany(FeedsFileName, FeedsCompanyCode);
                                ReportRPV_ColunsDesign Coluns = new ReportRPV_ColunsDesign(FeedsFileName, FeedsCompanyCode);
                                Company.CompanyFields(Writer_Arq);
                                Coluns.DefiningColumns(Writer_Arq);

                                DefineNovaPagina = 0;
                            }

                            ReportRPV_Statics.FeedsEncontrouInformação = "SIM";
                        }
                        else
                        {
                            ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                        }
                    }
                }
                catch (SqlException Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA O FORMULÁRIO TabCfope
        public void TabRotas_ImpreRPV_Ali(RadioButton rabOrdemAlfabetica, ComboBox comSituacaoIMP, StreamWriter Writer_Arq, string NomeDoRelatorio, string LojaLogada)
        {
            if (FeedsFileName == "TabRotas")
            {
               
                String Select_CMD = String.Format("SELECT * FROM TabRotas WHERE 1=1");

                if (comSituacaoIMP.SelectedIndex > 0)
                    Select_CMD += " AND Status_ROT = " + comSituacaoIMP.SelectedIndex;

                if (rabOrdemAlfabetica.Checked == true)
                    Select_CMD += " ORDER BY Descri_ROT";



                SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader();
                    int DefineNovaPagina = 0;
                    ReportRPV_Statics.FeedsQtResultados = 0;
                    while (Dr.Read())
                    {
                        if (Dr.HasRows)
                        {
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{750}" + Dr["Sequen_ROT"].ToString().PadLeft(6, '0'));
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{1800}" + Dr["Descri_ROT"]);

                            #region TRATAMENTO STATUS
                            string Status_ROT = Dr["Status_ROT"].ToString();
                            if (Convert.ToInt32(Status_ROT) == 1)
                            {
                                Status_ROT = "ATIVA";
                            }
                            else if (Convert.ToInt32(Status_ROT) == 2)
                            {
                                Status_ROT = "INATIVA";
                            }
                            else if (Convert.ToInt32(Status_ROT) == 3)
                            {
                                Status_ROT = "EXCLUIDA";
                            }
                            else
                            {
                                Status_ROT = "ERRO.";
                            }
                            #endregion

                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{9700}" + Status_ROT + "{\\n}");


                            DefineNovaPagina++;
                            ReportRPV_Statics.FeedsQtResultados++;

                            if (DefineNovaPagina == 65)
                            {
                                Writer_Arq.WriteLine("{\\np}");
                                Writer_Arq.WriteLine(@"[data]");

                                ReportRPV_AboutCompany Company = new ReportRPV_AboutCompany(FeedsFileName, FeedsCompanyCode);
                                ReportRPV_ColunsDesign Coluns = new ReportRPV_ColunsDesign(FeedsFileName, FeedsCompanyCode);
                                Company.CompanyFields(Writer_Arq);
                                Coluns.DefiningColumns(Writer_Arq);


                                DefineNovaPagina = 0;
                            }

                            ReportRPV_Statics.FeedsEncontrouInformação = "SIM";
                        }
                        else
                        {
                            ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                        }
                    }
                }
                catch (SqlException Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA O FORMULÁRIO TabConve
        public void TabConve_ImpreRPV_Ali(RadioButton rabOrdemAlfabetica, ComboBox comImpTipo, StreamWriter Writer_Arq, string NomeDoRelatorio, string LojaLogada)
        {
            if (FeedsFileName == "TabConve")
            {
                
                String Select_CMD = String.Format("SELECT Sequen_COV, LEFT (Descri_COV, 50) AS Descri_COV, Tipo01_COV, Taxa01_COV, Status_COV FROM TabConve WHERE 1=1");

                if (comImpTipo.SelectedIndex > 0 && comImpTipo.SelectedIndex < 4)
                    Select_CMD += " AND Tipo01_COV = " + comImpTipo.SelectedIndex;
                if (comImpTipo.SelectedIndex == 4)
                    Select_CMD += " AND Tipo01_COV <> 1";

                if (rabOrdemAlfabetica.Checked == true)
                    Select_CMD += " ORDER BY Descri_COV";



                SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader();
                    int DefineNovaPagina = 0;
                    ReportRPV_Statics.FeedsQtResultados = 0;
                    while (Dr.Read())
                    {
                        if (Dr.HasRows)
                        {
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{750}" + Dr["Sequen_COV"].ToString().PadLeft(6, '0'));
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{1800}" + Dr["Descri_COV"]);

                            #region TRATAMENTO STATUS
                            string Status_COV = Dr["Status_COV"].ToString();
                            if (Convert.ToInt32(Status_COV) == 1)
                            {
                                Status_COV = "ATIVA";
                            }
                            else if (Convert.ToInt32(Status_COV) == 2)
                            {
                                Status_COV = "INATIVA";
                            }
                            else if (Convert.ToInt32(Status_COV) == 3)
                            {
                                Status_COV = "EXCLUIDA";
                            }
                            else
                            {
                                Status_COV = "ERRO.";
                            }
                            #endregion

                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{8400}" + Status_COV);
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=10;c=0}{10100}" + Dr["Taxa01_COV"] + "{\\n}");


                            DefineNovaPagina++;
                            ReportRPV_Statics.FeedsQtResultados++;

                            if (DefineNovaPagina == 65)
                            {
                                Writer_Arq.WriteLine("{\\np}");
                                Writer_Arq.WriteLine(@"[data]");

                                ReportRPV_AboutCompany Company = new ReportRPV_AboutCompany(FeedsFileName, FeedsCompanyCode);
                                ReportRPV_ColunsDesign Coluns = new ReportRPV_ColunsDesign(FeedsFileName, FeedsCompanyCode);
                                Company.CompanyFields(Writer_Arq);
                                Coluns.DefiningColumns(Writer_Arq);


                                DefineNovaPagina = 0;
                            }

                            ReportRPV_Statics.FeedsEncontrouInformação = "SIM";
                        }
                        else
                        {
                            ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                        }
                    }
                }
                catch (SqlException Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA O FORMULÁRIO TabSetor
        public void TabSetor_ImpreRPV_Ali(RadioButton rabOrdemAlfabetica, ComboBox comImpStatus, StreamWriter Writer_Arq, string NomeDoRelatorio, string LojaLogada, ComboBox comImpTipo)
        {
            if (FeedsFileName == "TabSetor")
            {
              
                String Select_CMD = String.Format("SELECT Sequen_SET, LEFT (Descri_SET, 50) AS Descri_SET, LEFT (Locali_SET, 31) AS Locali_SET FROM TabSetor WHERE 1=1");

                if (comImpStatus.SelectedIndex > 0)
                    Select_CMD += " AND Status_SET = " + comImpStatus.SelectedIndex;

                if (rabOrdemAlfabetica.Checked == true)
                    Select_CMD += " ORDER BY Descri_SET";

                if (comImpTipo.SelectedIndex > 0)
                {
                    if (comImpTipo.SelectedIndex == 1)
                    {
                        Select_CMD += " AND Sequen_SET LIKE '%0000%'";
                    }
                    if (comImpTipo.SelectedIndex == 2)
                    {
                        Select_CMD += " AND Sequen_SET NOT LIKE '%0000%'";
                    }
                }



                SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader();
                    int DefineNovaPagina = 0;
                    ReportRPV_Statics.FeedsQtResultados = 0;
                    while (Dr.Read())
                    {
                        if (Dr.HasRows)
                        {
                            #region TRATAMENTO TIPO
                            string TIPO = "";
                            if (Convert.ToInt32(Dr["Sequen_SET"].ToString().PadLeft(7, '0').Substring(3, 4)) == 0)
                            {
                                TIPO = "SETOR";
                            }
                            else
                            {
                                TIPO = "SUB";
                            }
                            #endregion

                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{660}" + Dr["Sequen_SET"].ToString().PadLeft(7, '0'));
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{1600}" + Dr["Descri_SET"]);
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{7080}" + Dr["Locali_SET"]);
                            Writer_Arq.WriteLine("{F=Courier New;b=N;S=08;c=0}{10700}" + TIPO + "{\\n}");


                            DefineNovaPagina++;
                            ReportRPV_Statics.FeedsQtResultados++;

                            if (DefineNovaPagina == 65)
                            {
                                Writer_Arq.WriteLine("{\\np}");
                                Writer_Arq.WriteLine(@"[data]");

                                ReportRPV_AboutCompany Company = new ReportRPV_AboutCompany(FeedsFileName, FeedsCompanyCode);
                                ReportRPV_ColunsDesign Coluns = new ReportRPV_ColunsDesign(FeedsFileName, FeedsCompanyCode);
                                Company.CompanyFields(Writer_Arq);
                                Coluns.DefiningColumns(Writer_Arq);

                                DefineNovaPagina = 0;
                            }

                            ReportRPV_Statics.FeedsEncontrouInformação = "SIM";
                        }
                        else
                        {
                            ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                        }
                    }
                }
                catch (SqlException Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    ReportRPV_Statics.FeedsEncontrouInformação = "NÃO";
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + FeedsFileName + "_ImpreRPV_Ali()\n\nBLOCO = CLASSE ImpreRPV_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }




        #region FORMATAR CPF.CNPJ
        public static string FormatarCpfCnpj(string strCpfCnpj)
        {
            if (strCpfCnpj.Length <= 11)
            {
                MaskedTextProvider mtpCpf = new MaskedTextProvider(@"000\.000\.000-00");
                mtpCpf.Set(ZerosEsquerda(strCpfCnpj, 11));
                return mtpCpf.ToString();
            }
            else
            {
                MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");
                mtpCnpj.Set(ZerosEsquerda(strCpfCnpj, 11));
                return mtpCnpj.ToString();
            }
        }
        public static string ZerosEsquerda(string strString, int intTamanho)
        {
            string strResult = "";
            for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
            {
                strResult += "0";
            }
            return strResult + strString;
        }
        #endregion

    }
}
