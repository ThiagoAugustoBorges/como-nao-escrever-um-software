using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using System.Data.SqlClient;

namespace Print_WORD
{
    internal class ImpreWORD_AlimentaRelatorio
    {
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

        public string EncontrouInformação { get; set; }

        //ALIMENTA PARA TabProgr
        public void TabProgr_ImpreWORD_Ali(string NomeDoArquivo, string LojaLogada, ComboBox comSituacaoIMP, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, Word.Application Word_Appli)
        {
            ImpreWORD_Colunas Coluns = new ImpreWORD_Colunas();

            if (NomeDoArquivo == "WORD_TabProgr.docx")
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
                    int QuebraPagina = 0;
                    int QuantidadeDeRegistros = 1;
                    int NumeroDaPagina = 1;


                    while (Dr.Read())
                    {
                        Application.DoEvents();
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        Application.DoEvents();
                        QuantidadeDeRegistros++;


                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";

                            string RESULT_1 = Dr["Sequen_PGR"].ToString().PadLeft(6, '0');
                            string RESULT_2 = Dr["Descri_PGR"].ToString();
                            string RESULT_3 = Dr["Status_PGR"].ToString();
                            string RESULT_4 = Dr["Modulo_PGR"].ToString();

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

                            Word_Appli.Selection.TypeText(RESULT_1.PadRight(12, ' '));
                            Word_Appli.Selection.TypeText(RESULT_2.PadRight(60, ' '));
                            Word_Appli.Selection.TypeText(Status_PGR.PadRight(10, ' '));
                            Word_Appli.Selection.TypeText(Modulo_PGR.PadRight(13, ' '));
                            QuebraPagina++;

                            if (QuebraPagina == 60)
                            {
                                NumeroDaPagina++;
                                Coluns.WORD_EscreveCabecalho(NomeDoArquivo, Word_Appli);
                                Coluns.WORD_DefineCabeçalho(LojaLogada, Word_Appli, NumeroDaPagina);
                                Coluns.WORD_DefineColunas(NomeDoArquivo);
                                //PEGA NA CLASSE Colunas E JOGA NESSE MÉTODO
                                Word_Appli.Selection.TypeText(Coluns.ColunasDocumento);
                                QuebraPagina = 0;
                            }
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                    }
                    Dr.Close();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabUsuar
        public void TabUsuar_ImpreWORD_Ali(string NomeDoArquivo, string LojaLogada, ComboBox comSituacaoIMP, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, CheckBox cheImpApelid, ComboBox comPermissaoIMP, RadioButton rabOrdemAlfabeticaApelid, TextBox txtEmpreCodImp, Word.Application Word_Appli)
        {
            ImpreWORD_Colunas Coluns = new ImpreWORD_Colunas();

            if (NomeDoArquivo == "WORD_TabUsuar.docx")
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
                    int QuebraPagina = 0;
                    int QuantidadeDeRegistros = 1;
                    int NumeroDaPagina = 1;


                    while (Dr.Read())
                    {
                        Application.DoEvents();
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        Application.DoEvents();
                        QuantidadeDeRegistros++;


                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";

                            string RESULT_1 = Dr["Sequen_USU"].ToString().PadLeft(6, '0');

                            string RESULT_2 = "";
                            if (cheImpApelid.Checked == true)
                            {
                                RESULT_2 = Dr["Apelid_USU"].ToString();
                            }
                            else
                            {
                                RESULT_2 = Dr["Descri_USU"].ToString();
                            }


                            string RESULT_3 = Dr["Tipo01_USU"].ToString();
                            string RESULT_4 = Dr["CodLoj_USU"].ToString().PadLeft(6, '0');

                            //TRATAMENTO PARA Tipo01
                            #region TRATAMENTO String STATUS
                            if (Convert.ToInt32(RESULT_3) == 1)
                            {
                                RESULT_3 = "TOTAL";
                            }
                            else if (Convert.ToInt32(RESULT_3) == 2)
                            {
                                RESULT_3 = "LIMITADO";
                            }
                            else if (Convert.ToInt32(RESULT_3) == 3)
                            {
                                RESULT_3 = "RESTRITO";
                            }
                            else
                            {
                                RESULT_3 = "ERRO..!";
                            }

                            #endregion


                            Word_Appli.Selection.TypeText(RESULT_1.PadRight(12, ' '));
                            Word_Appli.Selection.TypeText(RESULT_2.PadRight(57, ' '));
                            Word_Appli.Selection.TypeText(RESULT_3.PadRight(16, ' '));
                            Word_Appli.Selection.TypeText(RESULT_4.PadRight(10, ' '));
                            QuebraPagina++;

                            if (QuebraPagina == 60)
                            {
                                NumeroDaPagina++;
                                Coluns.WORD_EscreveCabecalho(NomeDoArquivo, Word_Appli);
                                Coluns.WORD_DefineCabeçalho(LojaLogada, Word_Appli, NumeroDaPagina);
                                Coluns.WORD_DefineColunas(NomeDoArquivo);
                                //PEGA NA CLASSE Colunas E JOGA NESSE MÉTODO
                                Word_Appli.Selection.TypeText(Coluns.ColunasDocumento);
                                QuebraPagina = 0;
                            }
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                    }
                    Dr.Close();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabPermi
        public void TabPermi_ImpreWORD_Ali(string NomeDoArquivo, string LojaLogada, ComboBox comSituacaoIMP, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, CheckBox cheImpApelid, ComboBox comPermissaoIMP, RadioButton rabOrdemAlfabeticaApelid, TextBox txtEmpreCodImp, Word.Application Word_Appli, TextBox txtImpUsuarCod)
        {
            ImpreWORD_Colunas Coluns = new ImpreWORD_Colunas();

            if (NomeDoArquivo == "WORD_TabPermi.docx")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                String Select_CMD = String.Format("SELECT SeqUsu_PER,LEFT (Apelid_USU, 34) AS Apelid_USU,LEFT (Descri_USU, 34) as Descri_USU,SeqPgr_PER,LEFT (Descri_PGR,19) AS Descri_PGR,PerINC_PER,PerALT_PER,PerEXC_PER,PerCON_PER,PerABA_PER,PerAb1_PER,PerAb2_PER,PerAb3_PER,PerAb4_PER FROM TabPermi INNER JOIN TabProgr ON TabPermi.SeqPgr_PER = TabProgr.Sequen_PGR INNER JOIN TabUsuar ON TabPermi.SeqUsu_PER = TabUsuar.Sequen_USU WHERE 1=1");


                if (txtEmpreCodImp.Text != string.Empty)
                    Select_CMD += " AND SeqPgr_PER = " + txtEmpreCodImp.Text;
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
                    int QuebraPagina = 0;
                    int QuantidadeDeRegistros = 1;
                    int NumeroDaPagina = 1;


                    while (Dr.Read())
                    {
                        Application.DoEvents();
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        Application.DoEvents();
                        QuantidadeDeRegistros++;


                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";

                            string RESULT_1 = Dr["SeqUsu_PER"].ToString().PadLeft(6, '0');

                            string RESULT_2 = "";
                            if (cheImpApelid.Checked == true)
                            {
                                RESULT_2 = Dr["Apelid_USU"].ToString();
                            }
                            else
                            {
                                RESULT_2 = Dr["Descri_USU"].ToString();
                            }


                            string RESULT_3 = Dr["SeqPgr_PER"].ToString() + " " + Dr["Descri_PGR"].ToString();


                            
                            #region COLOR EM INCLUIR
                            string RESULT_4 = Dr["PerINC_PER"].ToString();
                            if (RESULT_4 == "False")
                            {
                                RESULT_4 = "NÃO";
                            }
                            else
                            {
                                RESULT_4 = "SIM";
                            }
                            #endregion

                            #region COLOR EM ALTERAR
                            string RESULT_5 = Dr["PerALT_PER"].ToString();
                            if (RESULT_5 == "False")
                            {
                                RESULT_5 = "NÃO";
                            }
                            else
                            {
                                RESULT_5 = "SIM";
                            }
                            #endregion

                            #region COLOR EM EXCLUIR
                            string RESULT_6 = Dr["PerEXC_PER"].ToString();
                            if (RESULT_6 == "False")
                            {
                                RESULT_6 = "NÃO";
                            }
                            else
                            {
                                RESULT_6 = "SIM";
                            }
                            #endregion

                            #region COLOR EM ABAS
                            string RESULT_8 = Dr["PerABA_PER"].ToString();
                            if (RESULT_8 == "False")
                            {
                                RESULT_8 = "NÃO";
                            }
                            else
                            {
                                RESULT_8 = "SIM";
                            }
                            #endregion

                         
                            Word_Appli.Selection.TypeText(RESULT_1.PadRight(10, ' '));
                            Word_Appli.Selection.TypeText(RESULT_2.PadRight(37, ' '));
                            Word_Appli.Selection.TypeText(RESULT_3.PadRight(29, ' '));
                            Word_Appli.Selection.TypeText(RESULT_4.PadRight(5, ' '));
                            Word_Appli.Selection.TypeText(RESULT_5.PadRight(5, ' '));
                            Word_Appli.Selection.TypeText(RESULT_6.PadRight(5, ' '));
                            Word_Appli.Selection.TypeText(RESULT_8.PadRight(5, ' '));
                            QuebraPagina++;

                            if (QuebraPagina == 60)
                            {
                                NumeroDaPagina++;
                                Coluns.WORD_EscreveCabecalho(NomeDoArquivo, Word_Appli);
                                Coluns.WORD_DefineCabeçalho(LojaLogada, Word_Appli, NumeroDaPagina);
                                Coluns.WORD_DefineColunas(NomeDoArquivo);
                                //PEGA NA CLASSE Colunas E JOGA NESSE MÉTODO
                                Word_Appli.Selection.TypeText(Coluns.ColunasDocumento);
                                QuebraPagina = 0;
                            }
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                    }
                    Dr.Close();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabCidad
        public void TabCidad_ImpreWORD_Ali(string NomeDoArquivo, string LojaLogada, ComboBox comImpStatus, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, Word.Application Word_Appli)
        {
            ImpreWORD_Colunas Coluns = new ImpreWORD_Colunas();

            if (NomeDoArquivo == "WORD_TabCidad.docx")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                String Select_CMD = String.Format("SELECT Sequen_CID,Descri_CID,UfFede_CID,Descri_PAI,IbgeMu_CID,Status_CID FROM TabCidad INNER JOIN TabPaise ON TabCidad.PaisCi_CID = TabPaise.Codigo_PAI WHERE 1=1");

                if (comImpStatus.SelectedIndex > 0)
                    Select_CMD += " AND Status_CID = " + comImpStatus.SelectedIndex;
                


                if (rabOrdemAlfabetica.Checked == true)
                    Select_CMD += " ORDER BY Descri_CID";


                SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader();
                    int QuebraPagina = 0;
                    int QuantidadeDeRegistros = 1;
                    int NumeroDaPagina = 1;


                    while (Dr.Read())
                    {
                        Application.DoEvents();
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        Application.DoEvents();
                        QuantidadeDeRegistros++;


                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";

                            string RESULT_1 = Dr["Sequen_CID"].ToString().PadLeft(6, '0');

                            string RESULT_2 = Dr["Descri_CID"].ToString();

                            string RESULT_3 = Dr["UfFede_CID"].ToString();

                            string RESULT_4 = Dr["Descri_PAI"].ToString();

                            string RESULT_5 = Dr["IbgeMu_CID"].ToString();

                            string RESULT_6 = Dr["Status_CID"].ToString();


                            //TRATAMENTO PARA Status
                            #region TRATAMENTO String STATUS
                            if (Convert.ToInt32(RESULT_6) == 1)
                            {
                                RESULT_6 = "ATIVO";
                            }
                            else if (Convert.ToInt32(RESULT_6) == 2)
                            {
                                RESULT_6 = "INATIVO";
                            }
                            else if (Convert.ToInt32(RESULT_6) == 3)
                            {
                                RESULT_6 = "EXCLUIDO";
                            }
                            else
                            {
                                RESULT_6 = "ERRO..!";
                            }

                            #endregion


                            Word_Appli.Selection.TypeText(RESULT_1.PadRight(09, ' '));
                            Word_Appli.Selection.TypeText(RESULT_2.PadRight(42, ' '));
                            Word_Appli.Selection.TypeText(RESULT_3.PadRight(05, ' '));
                            Word_Appli.Selection.TypeText(RESULT_4.PadRight(17, ' '));
                            Word_Appli.Selection.TypeText(RESULT_5.PadRight(11, ' '));
                            Word_Appli.Selection.TypeText(RESULT_6.PadRight(10, ' '));
                            QuebraPagina++;

                            if (QuebraPagina == 60)
                            {
                                NumeroDaPagina++;
                                Coluns.WORD_EscreveCabecalho(NomeDoArquivo, Word_Appli);
                                Coluns.WORD_DefineCabeçalho(LojaLogada, Word_Appli, NumeroDaPagina);
                                Coluns.WORD_DefineColunas(NomeDoArquivo);
                                //PEGA NA CLASSE Colunas E JOGA NESSE MÉTODO
                                Word_Appli.Selection.TypeText(Coluns.ColunasDocumento);
                                QuebraPagina = 0;
                            }
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                    }
                    Dr.Close();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabClien
        public void TabClien_ImpreWORD_Ali(string NomeDoArquivo, string LojaLogada, ComboBox comImpSituacao, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, Word.Application Word_Appli, ComboBox comImpCategoria, RadioButton rabOrdemAlfabeticaApelid, TextBox txtImpEmpreCod, TextBox txtImpCidadeCod, CheckBox cheImpApelid)
        {
            ImpreWORD_Colunas Coluns = new ImpreWORD_Colunas();

            if (NomeDoArquivo == "WORD_TabClien.docx")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                String Select_CMD = String.Format("SELECT Sequen_CLI,LEFT (Descri_CLI, 50) AS Descri_CLI,LEFT (Fantas_CLI, 50) AS Fantas_CLI,LEFT (CpfCnp_CLI, 14) AS CpfCnp_CLI,LEFT (InsEst_CLI, 12) AS InsEst_CLI,TelPab_CLI FROM TabClien WHERE 1=1");

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
                    int QuebraPagina = 0;
                    int QuantidadeDeRegistros = 1;
                    int NumeroDaPagina = 1;


                    while (Dr.Read())
                    {
                        Application.DoEvents();
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        Application.DoEvents();
                        QuantidadeDeRegistros++;


                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";

                            Word_Appli.ActiveWindow.Selection.Font.Size = 08;

                            string RESULT_1 = Dr["Sequen_CLI"].ToString().PadLeft(6, '0');

                            string RESULT_2 = "";
                            if (cheImpApelid.Checked == true)
                            {
                                RESULT_2 = Dr["Fantas_CLI"].ToString();
                            }
                            else
                            {
                                RESULT_2 = Dr["Descri_CLI"].ToString();
                            }

                            string RESULT_3 = FormatarCpfCnpj(Dr["CpfCnp_CLI"].ToString());

                            string RESULT_4 = Dr["InsEst_CLI"].ToString();

                            string RESULT_5 = Dr["TelPab_CLI"].ToString();

                            Word_Appli.Selection.TypeText("  ");
                            Word_Appli.Selection.TypeText(RESULT_1.PadRight(11, ' '));
                            Word_Appli.Selection.TypeText(RESULT_2.PadRight(52, ' '));
                            Word_Appli.Selection.TypeText(RESULT_3.PadRight(21, ' '));
                            Word_Appli.Selection.TypeText(RESULT_4.PadRight(19, ' '));
                            Word_Appli.Selection.TypeText(RESULT_5.PadRight(15, ' '));
                            QuebraPagina++;

                            if (QuebraPagina == 75)
                            {
                                NumeroDaPagina++;
                                Word_Appli.ActiveWindow.Selection.Font.Size = 10;
                                Coluns.WORD_EscreveCabecalho(NomeDoArquivo, Word_Appli);
                                Coluns.WORD_DefineCabeçalho(LojaLogada, Word_Appli, NumeroDaPagina);
                                Coluns.WORD_DefineColunas(NomeDoArquivo);
                                //PEGA NA CLASSE Colunas E JOGA NESSE MÉTODO
                                Word_Appli.Selection.TypeText(Coluns.ColunasDocumento);
                                QuebraPagina = 0;
                            }
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                    }
                    Dr.Close();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabMsgNt
        public void TabMsgNt_ImpreWORD_Ali(string NomeDoArquivo, string LojaLogada, ComboBox comSituacaoIMP, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, Word.Application Word_Appli)
        {
            ImpreWORD_Colunas Coluns = new ImpreWORD_Colunas();

            if (NomeDoArquivo == "WORD_TabMsgNt.docx")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                String Select_CMD = String.Format("SELECT Sequen_MSG,LEFT (Descri_MSG, 37) AS Descri_MSG,SeqEmp_MSG,LEFT (Descri_EMP, 32) AS Descri_EMP FROM TabMsgNt INNER JOIN TabEmpre ON TabMsgNt.SeqEmp_MSG = TabEmpre.Sequen_EMP WHERE 1=1");

                if (comSituacaoIMP.SelectedIndex > 0)
                    Select_CMD += " AND SeqEmp_MSG = " + LojaLogada;

                if (rabOrdemAlfabetica.Checked == true)
                    Select_CMD += " ORDER BY Descri_MSG";


                SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader();
                    int QuebraPagina = 0;
                    int QuantidadeDeRegistros = 1;
                    int NumeroDaPagina = 1;


                    while (Dr.Read())
                    {
                        Application.DoEvents();
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        Application.DoEvents();
                        QuantidadeDeRegistros++;


                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";

                            string RESULT_1 = Dr["Sequen_MSG"].ToString().PadLeft(3, '0');
                            string RESULT_2 = Dr["Descri_MSG"].ToString();
                            string RESULT_3 = Dr["SeqEmp_MSG"].ToString().PadLeft(7, '0'); ;
                            string RESULT_4 = Dr["Descri_EMP"].ToString();

                            
                            Word_Appli.Selection.TypeText(RESULT_1.PadRight(09, ' '));
                            Word_Appli.Selection.TypeText(RESULT_2.PadRight(41, ' '));
                            Word_Appli.Selection.TypeText(RESULT_3.PadRight(11, ' '));
                            Word_Appli.Selection.TypeText(RESULT_4.PadRight(35, ' '));
                            QuebraPagina++;

                            if (QuebraPagina == 60)
                            {
                                NumeroDaPagina++;
                                Coluns.WORD_EscreveCabecalho(NomeDoArquivo, Word_Appli);
                                Coluns.WORD_DefineCabeçalho(LojaLogada, Word_Appli, NumeroDaPagina);
                                Coluns.WORD_DefineColunas(NomeDoArquivo);
                                //PEGA NA CLASSE Colunas E JOGA NESSE MÉTODO
                                Word_Appli.Selection.TypeText(Coluns.ColunasDocumento);
                                QuebraPagina = 0;
                            }
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                    }
                    Dr.Close();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabCfope
        public void TabCfope_ImpreWORD_Ali(string NomeDoArquivo, string LojaLogada, ComboBox comSituacaoIMP, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, Word.Application Word_Appli)
        {
            ImpreWORD_Colunas Coluns = new ImpreWORD_Colunas();

            if (NomeDoArquivo == "WORD_TabCfope.docx")
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
                    int QuebraPagina = 0;
                    int QuantidadeDeRegistros = 1;
                    int NumeroDaPagina = 1;


                    while (Dr.Read())
                    {
                        Application.DoEvents();
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        Application.DoEvents();
                        QuantidadeDeRegistros++;


                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";

                            string RESULT_1 = Dr["Sequen_CFO"].ToString().PadLeft(4, '0');
                            string RESULT_2 = Dr["Descri_CFO"].ToString();
                            string RESULT_3 = Dr["EntInd_CFO"].ToString().PadLeft(4, '0');
                            string RESULT_4 = Dr["EntCom_CFO"].ToString().PadLeft(4, '0');


                            Word_Appli.Selection.TypeText(RESULT_1.PadRight(09, ' '));
                            Word_Appli.Selection.TypeText(RESULT_2.PadRight(66, ' '));
                            Word_Appli.Selection.TypeText(RESULT_3.PadRight(12, ' '));
                            Word_Appli.Selection.TypeText(RESULT_4.PadRight(13, ' '));
                            QuebraPagina++;

                            if (QuebraPagina == 60)
                            {
                                NumeroDaPagina++;
                                Coluns.WORD_EscreveCabecalho(NomeDoArquivo, Word_Appli);
                                Coluns.WORD_DefineCabeçalho(LojaLogada, Word_Appli, NumeroDaPagina);
                                Coluns.WORD_DefineColunas(NomeDoArquivo);
                                //PEGA NA CLASSE Colunas E JOGA NESSE MÉTODO
                                Word_Appli.Selection.TypeText(Coluns.ColunasDocumento);
                                QuebraPagina = 0;
                            }
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                    }
                    Dr.Close();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabRotas
        public void TabRotas_ImpreWORD_Ali(string NomeDoArquivo, string LojaLogada, ComboBox comSituacaoIMP, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, Word.Application Word_Appli)
        {
            ImpreWORD_Colunas Coluns = new ImpreWORD_Colunas();

            if (NomeDoArquivo == "WORD_TabRotas.docx")
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
                    int QuebraPagina = 0;
                    int QuantidadeDeRegistros = 1;
                    int NumeroDaPagina = 1;


                    while (Dr.Read())
                    {
                        Application.DoEvents();
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        Application.DoEvents();
                        QuantidadeDeRegistros++;


                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";

                            string RESULT_1 = Dr["Sequen_ROT"].ToString().PadLeft(6, '0');
                            string RESULT_2 = Dr["Descri_ROT"].ToString();
                            string RESULT_3 = Dr["Status_ROT"].ToString();

                            #region TRATAMENTO STATUS
                            if (Convert.ToInt32(RESULT_3) == 1)
                            {
                                RESULT_3 = "ATIVA";
                            }
                            else if (Convert.ToInt32(RESULT_3) == 2)
                            {
                                RESULT_3 = "INATIVA";
                            }
                            else if (Convert.ToInt32(RESULT_3) == 3)
                            {
                                RESULT_3 = "EXCLUIDA";
                            }
                            else
                            {
                                RESULT_3 = "ERRO.";
                            }
                            #endregion


                            Word_Appli.Selection.TypeText(RESULT_1.PadRight(13, ' '));
                            Word_Appli.Selection.TypeText(RESULT_2.PadRight(69, ' '));
                            Word_Appli.Selection.TypeText(RESULT_3.PadRight(17, ' '));
                            QuebraPagina++;

                            if (QuebraPagina == 60)
                            {
                                NumeroDaPagina++;
                                Coluns.WORD_EscreveCabecalho(NomeDoArquivo, Word_Appli);
                                Coluns.WORD_DefineCabeçalho(LojaLogada, Word_Appli, NumeroDaPagina);
                                Coluns.WORD_DefineColunas(NomeDoArquivo);
                                //PEGA NA CLASSE Colunas E JOGA NESSE MÉTODO
                                Word_Appli.Selection.TypeText(Coluns.ColunasDocumento);
                                QuebraPagina = 0;
                            }
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                    }
                    Dr.Close();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabConve
        public void TabConve_ImpreWORD_Ali(string NomeDoArquivo, string LojaLogada, ComboBox comImpTipo, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, Word.Application Word_Appli)
        {
            ImpreWORD_Colunas Coluns = new ImpreWORD_Colunas();

            if (NomeDoArquivo == "WORD_TabConve.docx")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

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
                    int QuebraPagina = 0;
                    int QuantidadeDeRegistros = 1;
                    int NumeroDaPagina = 1;


                    while (Dr.Read())
                    {
                        Application.DoEvents();
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        Application.DoEvents();
                        QuantidadeDeRegistros++;


                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";

                            string RESULT_1 = Dr["Sequen_COV"].ToString().PadLeft(6, '0');
                            string RESULT_2 = Dr["Descri_COV"].ToString();
                            string RESULT_3 = Dr["Status_COV"].ToString();
                            string RESULT_4 = Dr["Taxa01_COV"].ToString();

                            #region TRATAMENTO STATUS
                            if (Convert.ToInt32(RESULT_3) == 1)
                            {
                                RESULT_3 = "ATIVA";
                            }
                            else if (Convert.ToInt32(RESULT_3) == 2)
                            {
                                RESULT_3 = "INATIVA";
                            }
                            else if (Convert.ToInt32(RESULT_3) == 3)
                            {
                                RESULT_3 = "EXCLUIDA";
                            }
                            else
                            {
                                RESULT_3 = "ERRO.";
                            }
                            #endregion


                            Word_Appli.Selection.TypeText(RESULT_1.PadRight(13, ' '));
                            Word_Appli.Selection.TypeText(RESULT_2.PadRight(56, ' '));
                            Word_Appli.Selection.TypeText(RESULT_3.PadRight(15, ' '));
                            Word_Appli.Selection.TypeText(RESULT_4.PadRight(08, ' '));
                            QuebraPagina++;

                            if (QuebraPagina == 60)
                            {
                                NumeroDaPagina++;
                                Coluns.WORD_EscreveCabecalho(NomeDoArquivo, Word_Appli);
                                Coluns.WORD_DefineCabeçalho(LojaLogada, Word_Appli, NumeroDaPagina);
                                Coluns.WORD_DefineColunas(NomeDoArquivo);
                                //PEGA NA CLASSE Colunas E JOGA NESSE MÉTODO
                                Word_Appli.Selection.TypeText(Coluns.ColunasDocumento);
                                QuebraPagina = 0;
                            }
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                    }
                    Dr.Close();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }

        //ALIMENTA PARA TabSetor
        public void TabSetor_ImpreWORD_Ali(string NomeDoArquivo, string LojaLogada, ComboBox comImpTipo,ComboBox comImpStatus, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, Word.Application Word_Appli)
        {
            ImpreWORD_Colunas Coluns = new ImpreWORD_Colunas();

            if (NomeDoArquivo == "WORD_TabSetor.docx")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                String Select_CMD = String.Format("SELECT Sequen_SET, LEFT (Descri_SET, 50) AS Descri_SET, LEFT (Respon_SET, 17) AS Respon_SET FROM TabSetor WHERE 1=1");

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
                    int QuebraPagina = 0;
                    int QuantidadeDeRegistros = 1;
                    int NumeroDaPagina = 1;


                    while (Dr.Read())
                    {
                        Application.DoEvents();
                        txtQtSelectIMP.Text = QuantidadeDeRegistros.ToString("000000");
                        Application.DoEvents();
                        QuantidadeDeRegistros++;


                        if (Dr.HasRows)
                        {
                            EncontrouInformação = "SIM";

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

                            string RESULT_1 = Dr["Sequen_SET"].ToString().PadLeft(7, '0');
                            string RESULT_2 = Dr["Descri_SET"].ToString();
                            string RESULT_3 = Dr["Respon_SET"].ToString();
                            string RESULT_4 = TIPO;

                           


                            Word_Appli.Selection.TypeText(RESULT_1.PadRight(13, ' '));
                            Word_Appli.Selection.TypeText(RESULT_2.PadRight(53, ' '));
                            Word_Appli.Selection.TypeText(RESULT_3.PadRight(21, ' '));
                            Word_Appli.Selection.TypeText(RESULT_4.PadRight(08, ' '));
                            QuebraPagina++;

                            if (QuebraPagina == 60)
                            {
                                NumeroDaPagina++;
                                Coluns.WORD_EscreveCabecalho(NomeDoArquivo, Word_Appli);
                                Coluns.WORD_DefineCabeçalho(LojaLogada, Word_Appli, NumeroDaPagina);
                                Coluns.WORD_DefineColunas(NomeDoArquivo);
                                //PEGA NA CLASSE Colunas E JOGA NESSE MÉTODO
                                Word_Appli.Selection.TypeText(Coluns.ColunasDocumento);
                                QuebraPagina = 0;
                            }
                        }
                        else
                        {
                            EncontrouInformação = "NÃO";
                        }
                    }
                    Dr.Close();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método " + NomeDoArquivo + "_ImpreWORD_Ali()\n\nBLOCO = CLASSE ImpreWORD_AlimentaRelatorio\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }
    }
}