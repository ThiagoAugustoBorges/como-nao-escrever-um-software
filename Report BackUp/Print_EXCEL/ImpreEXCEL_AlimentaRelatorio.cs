using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace Print_EXCEL
{
    internal class ImpreEXCEL_AlimentaRelatorio
    {
        public string EncontrouInformação { get; set; }

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

        //ALIMENTA PARA TabProgr
        public void TabProgr_ImpreEXCEL_Ali(string NomeDoArquivo, ComboBox comSituacaoIMP, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, Excel.Worksheet Worksheet_EXCEL)
        {
            if (NomeDoArquivo == "EXC_TabProgr.xlsx")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                String Select_CMD = String.Format("SELECT * FROM TabProgr WHERE 1=1");
                if (comSituacaoIMP.SelectedIndex > 0)
                    Select_CMD += " AND Status_PGR = " + comSituacaoIMP.SelectedIndex;


                if (rabOrdemAlfabetica.Checked == true)
                    Select_CMD += " ORDER BY Descri_PGR";

                SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader();
                    int LinhaRecebeEscrita = 3;
                    int QuantidadeDeRegistros = 1;
                    while (Dr.Read())
                    {
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";


                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1] = Dr["Sequen_PGR"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1].NumberFormat = "000000";
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 2] = Dr["Descri_PGR"].ToString();

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

                            //TRANSFORMA O NÚMERO DO MODULO EM NOME
                            #region TRATAMENTO String MODULO
                            string Modulo_PGR = Dr["Modulo_PGR"].ToString();

                            if (Convert.ToInt32(Modulo_PGR) == 1)
                            {
                                Modulo_PGR = "FREE";
                            }
                            else if (Convert.ToInt32(Modulo_PGR) == 2)
                            {
                                Modulo_PGR = "EXPRESS";
                            }
                            else if (Convert.ToInt32(Modulo_PGR) == 3)
                            {
                                Modulo_PGR = "BUSINESS";
                            }
                            else if (Convert.ToInt32(Modulo_PGR) == 4)
                            {
                                Modulo_PGR = "CONTROLE";
                            }
                            else if (Convert.ToInt32(Modulo_PGR) == 5)
                            {
                                Modulo_PGR = "PRÓ";
                            }
                            else
                            {
                                Modulo_PGR = "ERRO..!";
                            }
                            #endregion


                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 3] = Modulo_PGR;
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 4] = Status_PGR;
                            LinhaRecebeEscrita++;
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                        QuantidadeDeRegistros++;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabUsuar
        public void TabUsuar_ImpreEXCEL_Ali(string NomeDoArquivo, ComboBox comSituacaoIMP, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, CheckBox cheImpApelid, ComboBox comPermissaoIMP, RadioButton rabOrdemAlfabeticaApelid, TextBox txtEmpreCodImp, Excel.Worksheet Worksheet_EXCEL)
        {
            if (NomeDoArquivo == "EXC_TabUsuar.xlsx")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();


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
                    int LinhaRecebeEscrita = 3;
                    int QuantidadeDeRegistros = 1;
                    while (Dr.Read())
                    {
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";


                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1] = Dr["Sequen_USU"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1].NumberFormat = "000000";


                            if (cheImpApelid.Checked == true)
                            {
                                Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 2] = Dr["Apelid_USU"].ToString();
                            }
                            else
                            {
                                Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 2] = Dr["Descri_USU"].ToString();
                            }



                            string Tipo01 = Dr["Tipo01_USU"].ToString();
                            //TRATAMENTO PARA Tipo01
                            #region TRATAMENTO String STATUS
                            if (Convert.ToInt32(Tipo01) == 1)
                            {
                                Tipo01 = "TOTAL";
                            }
                            else if (Convert.ToInt32(Tipo01) == 2)
                            {
                                Tipo01 = "LIMITADO";
                            }
                            else if (Convert.ToInt32(Tipo01) == 3)
                            {
                                Tipo01 = "RESTRITO";
                            }
                            else
                            {
                                Tipo01 = "ERRO..!";
                            }

                            #endregion



                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 3] = Tipo01;

                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 4] = Dr["CodLoj_USU"].ToString().PadLeft(6, '0');
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 4].NumberFormat = "000000";
                            LinhaRecebeEscrita++;
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                        QuantidadeDeRegistros++;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabPermi
        public void TabPermi_ImpreEXCEL_Ali(string NomeDoArquivo, ComboBox comSituacaoIMP, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, CheckBox cheImpApelid, ComboBox comPermissaoIMP, RadioButton rabOrdemAlfabeticaApelid, TextBox txtImpProgrCod, Excel.Worksheet Worksheet_EXCEL, TextBox txtImpUsuarCod)
        {
            if (NomeDoArquivo == "EXC_TabPermi.xlsx")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();


                String Select_CMD = String.Format("SELECT SeqUsu_PER,Apelid_USU,Descri_USU,SeqPgr_PER,LEFT (Descri_PGR,19) AS Descri_PGR,PerINC_PER,PerALT_PER,PerEXC_PER,PerCON_PER,PerABA_PER,PerAb1_PER,PerAb2_PER,PerAb3_PER,PerAb4_PER FROM TabPermi INNER JOIN TabProgr ON TabPermi.SeqPgr_PER = TabProgr.Sequen_PGR INNER JOIN TabUsuar ON TabPermi.SeqUsu_PER = TabUsuar.Sequen_USU WHERE 1=1");


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
                    int LinhaRecebeEscrita = 3;
                    int QuantidadeDeRegistros = 1;
                    while (Dr.Read())
                    {
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";


                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1] = Dr["SeqUsu_PER"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1].NumberFormat = "000000";

                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 2] = Dr["SeqPgr_PER"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 2].NumberFormat = "000000";


                            #region COLOR EM INCLUIR
                            string INC = Dr["PerINC_PER"].ToString();
                            if (INC == "False")
                            {
                                INC = "NÃO";
                            }
                            else
                            {
                                INC = "SIM";
                            }
                            #endregion

                            #region COLOR EM ALTERAR
                            string ALT = Dr["PerALT_PER"].ToString();
                            if (ALT == "False")
                            {
                                ALT = "NÃO";
                            }
                            else
                            {
                                ALT = "SIM";
                            }
                            #endregion

                            #region COLOR EM EXCLUIR
                            string EXC = Dr["PerEXC_PER"].ToString();
                            if (EXC == "False")
                            {
                                EXC = "NÃO";
                            }
                            else
                            {
                                EXC = "SIM";
                            }
                            #endregion

                            #region COLOR EM CONSULTAR
                            string CON = Dr["PerCON_PER"].ToString();
                            if (CON == "False")
                            {
                                CON = "NÃO";
                            }
                            else
                            {
                                CON = "SIM";
                            }
                            #endregion

                            #region COLOR EM ABAS
                            string ABA = Dr["PerABA_PER"].ToString();
                            if (ABA == "False")
                            {
                                ABA = "NÃO";
                            }
                            else
                            {
                                ABA = "SIM";
                            }
                            #endregion



                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 3] = INC;
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 4] = ALT;
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 5] = EXC;
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 6] = ABA;
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 7] = CON;


                            LinhaRecebeEscrita++;
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                        QuantidadeDeRegistros++;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabCidad
        public void TabCidad_ImpreEXCEL_Ali(string NomeDoArquivo, ComboBox comImpStatus, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, Excel.Worksheet Worksheet_EXCEL)
        {
            if (NomeDoArquivo == "EXC_TabCidad.xlsx")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                String Select_CMD = String.Format("SELECT * FROM TabCidad WHERE 1=1");
                if (comImpStatus.SelectedIndex > 0)
                    Select_CMD += " AND Status_CID = " + comImpStatus.SelectedIndex;


                if (rabOrdemAlfabetica.Checked == true)
                    Select_CMD += " ORDER BY Descri_CID";

                SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader();
                    int LinhaRecebeEscrita = 3;
                    int QuantidadeDeRegistros = 1;
                    while (Dr.Read())
                    {
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";


                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1] = Dr["Sequen_CID"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1].NumberFormat = "000000";
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 2] = Dr["Descri_CID"].ToString();

                            //TRANSFORMA 1 2 E 3 EM ATIVO, INATIVO E EXCLUIDO
                            #region TRATAMENTO String STATUS
                            string Status_PGR = Dr["Status_CID"].ToString();

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


                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 3] = Dr["IbgeMu_CID"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 4] = Status_PGR;
                            LinhaRecebeEscrita++;
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                        QuantidadeDeRegistros++;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabClien
        public void TabClien_ImpreEXCEL_Ali(string NomeDoArquivo, ComboBox comImpSituacao, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, CheckBox cheImpApelid, ComboBox comImpCategoria, RadioButton rabOrdemAlfabeticaApelid, TextBox txtImpEmpreCod, TextBox txtImpCidadeCod, Excel.Worksheet Worksheet_EXCEL)
        {
            if (NomeDoArquivo == "EXC_TabClien.xlsx")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                String Select_CMD = String.Format("SELECT Sequen_CLI,LEFT (Descri_CLI, 38) AS Descri_CLI,LEFT (Fantas_CLI, 38) AS Fantas_CLI,LEFT (CpfCnp_CLI, 14) AS CpfCnp_CLI,LEFT (InsEst_CLI, 12) AS InsEst_CLI,TelPab_CLI FROM TabClien WHERE 1=1");

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
                    int LinhaRecebeEscrita = 3;
                    int QuantidadeDeRegistros = 1;
                    while (Dr.Read())
                    {
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";


                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1] = Dr["Sequen_CLI"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1].NumberFormat = "000000";


                            if (cheImpApelid.Checked == true)
                            {
                                Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 2] = Dr["Fantas_CLI"].ToString();
                            }
                            else
                            {
                                Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 2] = Dr["Descri_CLI"].ToString();
                            }


                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 3] = FormatarCpfCnpj(Dr["CpfCnp_CLI"].ToString());
                            LinhaRecebeEscrita++;
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                        QuantidadeDeRegistros++;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabProgr
        public void TabMsgNt_ImpreEXCEL_Ali(string NomeDoArquivo, ComboBox comSituacaoIMP, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, Excel.Worksheet Worksheet_EXCEL, string LojaLogada)
        {
            if (NomeDoArquivo == "EXC_TabMsgNt.xlsx")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                String Select_CMD = String.Format("SELECT Sequen_MSG,LEFT (Descri_MSG, 39) AS Descri_MSG,SeqEmp_MSG,LEFT (Descri_EMP, 33) AS Descri_EMP FROM TabMsgNt INNER JOIN TabEmpre ON TabMsgNt.SeqEmp_MSG = TabEmpre.Sequen_EMP WHERE 1=1");

                if (comSituacaoIMP.SelectedIndex > 0)
                    Select_CMD += " AND SeqEmp_MSG = " + LojaLogada;

                if (rabOrdemAlfabetica.Checked == true)
                    Select_CMD += " ORDER BY Descri_MSG";

                SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader();
                    int LinhaRecebeEscrita = 3;
                    int QuantidadeDeRegistros = 1;
                    while (Dr.Read())
                    {
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";


                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1] = Dr["Sequen_MSG"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1].NumberFormat = "000";
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 2] = Dr["Descri_MSG"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 3] = Dr["SeqEmp_MSG"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 3].NumberFormat = "000000";

                            LinhaRecebeEscrita++;
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                        QuantidadeDeRegistros++;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabCfope
        public void TabCfope_ImpreEXCEL_Ali(string NomeDoArquivo, ComboBox comSituacaoIMP, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, Excel.Worksheet Worksheet_EXCEL, string LojaLogada)
        {
            if (NomeDoArquivo == "EXC_TabCfope.xlsx")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                String Select_CMD = String.Format("SELECT Sequen_CFO,Descri_CFO,EntCom_CFO,EntInd_CFO FROM TabCfope WHERE 1=1");


                if (comSituacaoIMP.SelectedIndex < 2)
                    Select_CMD += " AND DenFor_CFO = " + comSituacaoIMP.SelectedIndex;

                if (rabOrdemAlfabetica.Checked == true)
                    Select_CMD += " ORDER BY Descri_CFO";

                SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader();
                    int LinhaRecebeEscrita = 3;
                    int QuantidadeDeRegistros = 1;
                    while (Dr.Read())
                    {
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";


                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1] = Dr["Sequen_CFO"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1].NumberFormat = "0000";

                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 2] = Dr["Descri_CFO"].ToString();

                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 3] = Dr["EntCom_CFO"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 3].NumberFormat = "0000";

                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 4] = Dr["EntInd_CFO"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 4].NumberFormat = "0000";

                            LinhaRecebeEscrita++;
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                        QuantidadeDeRegistros++;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabRotas
        public void TabRotas_ImpreEXCEL_Ali(string NomeDoArquivo, ComboBox comSituacaoIMP, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, Excel.Worksheet Worksheet_EXCEL, string LojaLogada)
        {
            if (NomeDoArquivo == "EXC_TabRotas.xlsx")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                String Select_CMD = String.Format("SELECT * FROM TabRotas WHERE 1=1");

                if (comSituacaoIMP.SelectedIndex > 0)
                    Select_CMD += " AND Status_ROT = " + comSituacaoIMP.SelectedIndex;

                if (rabOrdemAlfabetica.Checked == true)
                    Select_CMD += " ORDER BY Descri_ROT";

                SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader();
                    int LinhaRecebeEscrita = 3;
                    int QuantidadeDeRegistros = 1;
                    while (Dr.Read())
                    {
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";


                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1] = Dr["Sequen_ROT"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1].NumberFormat = "000000";

                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 2] = Dr["Descri_ROT"].ToString();

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

                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 3] = Status_ROT;


                            LinhaRecebeEscrita++;
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                        QuantidadeDeRegistros++;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabConve
        public void TabConve_ImpreEXCEL_Ali(string NomeDoArquivo, ComboBox comImpTipo, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, Excel.Worksheet Worksheet_EXCEL, string LojaLogada)
        {
            if (NomeDoArquivo == "EXC_TabConve.xlsx")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                String Select_CMD = String.Format("SELECT Sequen_COV, LEFT (Descri_COV, 38) AS Descri_COV, Tipo01_COV, Taxa01_COV, Status_COV FROM TabConve WHERE 1=1");

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
                    int LinhaRecebeEscrita = 3;
                    int QuantidadeDeRegistros = 1;
                    while (Dr.Read())
                    {
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";


                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1] = Dr["Sequen_COV"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1].NumberFormat = "000000";

                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 2] = Dr["Descri_COV"].ToString();

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

                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 3] = Status_COV;

                            #region TRATAMENTO TIPO
                            string Tipo01_COV = Dr["Tipo01_COV"].ToString();
                            if (Convert.ToInt32(Tipo01_COV) == 1)
                            {
                                Tipo01_COV = "CONVÊNIO";
                            }
                            else if (Convert.ToInt32(Tipo01_COV) == 2)
                            {
                                Tipo01_COV = "CARTÃO C";
                            }
                            else if (Convert.ToInt32(Tipo01_COV) == 3)
                            {
                                Tipo01_COV = "CARTÃO D";
                            }
                            else
                            {
                                Tipo01_COV = "ERRO.";
                            }
                            #endregion

                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 4] = Tipo01_COV;


                            LinhaRecebeEscrita++;
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                        QuantidadeDeRegistros++;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabSetor
        public void TabSetor_ImpreEXCEL_Ali(string NomeDoArquivo, ComboBox comImpStatus, ComboBox comImpTipo, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, Excel.Worksheet Worksheet_EXCEL, string LojaLogada)
        {
            if (NomeDoArquivo == "EXC_TabSetor.xlsx")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                String Select_CMD = String.Format("SELECT Sequen_SET, LEFT (Descri_SET, 36) AS Descri_SET, LEFT (Locali_SET, 13) AS Locali_SET FROM TabSetor WHERE 1=1");

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
                    int LinhaRecebeEscrita = 3;
                    int QuantidadeDeRegistros = 1;
                    while (Dr.Read())
                    {
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";

                            #region TRATAMENTO TIPO
                            string TIPO = "";
                            if (Convert.ToInt32(Dr["Sequen_SET"].ToString().PadLeft(7, '0').Substring(3, 4)) == 0)
                            {
                                TIPO = "SET";
                            }
                            else
                            {
                                TIPO = "SUB";
                            }
                            #endregion

                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1] = Dr["Sequen_SET"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 1].NumberFormat = "0000000";
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 2] = Dr["Descri_SET"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 3] = Dr["Locali_SET"].ToString();
                            Worksheet_EXCEL.Cells[LinhaRecebeEscrita, 4] = TIPO;


                            LinhaRecebeEscrita++;
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                        QuantidadeDeRegistros++;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreEXCEL_Ali()\n\nBLOCO = CLASSE ImpreEXCEL_AlimentaRelatorio\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }
    }
}
