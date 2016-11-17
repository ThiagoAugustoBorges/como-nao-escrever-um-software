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
    public class ImpreEXCEL
    {
        public void ImpreEXCEL_GO(string NomeDoArquivo, string LojaLogada, TextBox txtCaminhoRel, ComboBox comSituacaoIMP, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, CheckBox cheImpApelid, ComboBox comPermissaoIMP, RadioButton rabOrdemAlfabeticaApelid, TextBox txtEmpreCodImp, TextBox txtImpUsuarCod)
        {
            //Adiciono a extenção
            if (NomeDoArquivo != string.Empty)
                NomeDoArquivo += ".xlsx";

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            ImpreEXCEL_MET MET = new ImpreEXCEL_MET();
            MET.Busca_CaminhoSALV(LojaLogada, txtCaminhoRel, NomeDoArquivo);


            Microsoft.Office.Interop.Excel.Application Appli_EXCEL = new Microsoft.Office.Interop.Excel.Application();
            // Workbook com 1 worksheet padrão.
            Excel.Workbook Workbook_EXCEL = Appli_EXCEL.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);

            // Cria mais 1 worksheet. O objeto "ws" fará inicialmente referência à primeira worksheet.
            Excel.Worksheet Worksheet_EXCEL = (Excel.Worksheet)Workbook_EXCEL.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);

            // Renomeia todas as worksheets (2 no meu caso)
            Worksheet_EXCEL = (Excel.Worksheet)Workbook_EXCEL.Worksheets[1];
            Worksheet_EXCEL.Name = "TechSIS Relatório";


            //DEFINE AS COLUNAS DO RELATORIO
            ImpreEXCEL_Colunas COLUNS = new ImpreEXCEL_Colunas();
            COLUNS.EXCEL_DefineColunas(NomeDoArquivo, Worksheet_EXCEL);

            //DEFINE AS BORDAS COMO CONTINUAS
            Worksheet_EXCEL.Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;


            #region ALIMENTA O RELATÓRIO DO EXCEL
            ImpreEXCEL_AlimentaRelatorio ALIMENTA = new ImpreEXCEL_AlimentaRelatorio();

            //TabProgr
            ALIMENTA.TabProgr_ImpreEXCEL_Ali(NomeDoArquivo, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, Worksheet_EXCEL);

            //TabUsuar
            ALIMENTA.TabUsuar_ImpreEXCEL_Ali(NomeDoArquivo, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, cheImpApelid, comPermissaoIMP, rabOrdemAlfabeticaApelid, txtEmpreCodImp, Worksheet_EXCEL);

            //TabUsuar
            ALIMENTA.TabPermi_ImpreEXCEL_Ali(NomeDoArquivo, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, cheImpApelid, comPermissaoIMP, rabOrdemAlfabeticaApelid, txtEmpreCodImp, Worksheet_EXCEL, txtImpUsuarCod);

            //TabCidad
            ALIMENTA.TabCidad_ImpreEXCEL_Ali(NomeDoArquivo, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, Worksheet_EXCEL);

            //TabClien
            ALIMENTA.TabClien_ImpreEXCEL_Ali(NomeDoArquivo, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, cheImpApelid, comPermissaoIMP, rabOrdemAlfabeticaApelid, txtEmpreCodImp, txtImpUsuarCod, Worksheet_EXCEL);

            //TabMsgNt
            ALIMENTA.TabMsgNt_ImpreEXCEL_Ali(NomeDoArquivo, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, Worksheet_EXCEL, LojaLogada);

            //TabCfope
            ALIMENTA.TabCfope_ImpreEXCEL_Ali(NomeDoArquivo, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, Worksheet_EXCEL, LojaLogada);

            //TabRotas
            ALIMENTA.TabRotas_ImpreEXCEL_Ali(NomeDoArquivo, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, Worksheet_EXCEL, LojaLogada);

            //TabConve
            ALIMENTA.TabConve_ImpreEXCEL_Ali(NomeDoArquivo, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, Worksheet_EXCEL, LojaLogada);

            //TabSetor
            ALIMENTA.TabSetor_ImpreEXCEL_Ali(NomeDoArquivo, comSituacaoIMP, comPermissaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, Worksheet_EXCEL, LojaLogada);
            #endregion


            // Retorna a referência de "ws" para a primeira planilha.
            Worksheet_EXCEL = (Excel.Worksheet)Workbook_EXCEL.Worksheets[1];



            if (ALIMENTA.EncontrouInformação == "SIM")
            {

                try
                {
                    Appli_EXCEL.ActiveWorkbook.SaveCopyAs(MET.LocalSalvamento_Arquivo);
                    MessageBox.Show("RELATÓRIO " + NomeDoArquivo + " SALVO COM SUCESSO\nDIRETÓRIO DO ARQUIVO.:\n(" + MET.LocalSalvamento_Arquivo + ")", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método ImpreEXCEL_GO()\n\nBLOCO = CLASSE ImpreEXCEL\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }

                DialogResult AbrirArquivo = MessageBox.Show("Deseja visualizar o arquivo " + NomeDoArquivo + "?", "TechSIS Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                {
                    if (AbrirArquivo == DialogResult.Yes)
                    {
                        Appli_EXCEL.Visible = true;
                        Appli_EXCEL.ActiveWorkbook.Saved = true;
                    }
                    if (AbrirArquivo == DialogResult.No)
                    {
                        Appli_EXCEL.ActiveWorkbook.Saved = true;
                        Workbook_EXCEL.Close();
                        Appli_EXCEL.Quit();
                        foreach
                        (System.Diagnostics.Process Proc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                        {
                            Proc.Kill();
                        }
                    }
                }
            }


            else
            {
                //CASO NÃO EXISTAM RESULTADOS
                Appli_EXCEL.ActiveWorkbook.Saved = true;
                Workbook_EXCEL.Close();
                Appli_EXCEL.Quit();
                foreach
                (System.Diagnostics.Process Proc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                {
                    Proc.Kill();
                }

                txtQtSelectIMP.Text = string.Empty;
                MessageBox.Show("Nenhuma informação encontrada", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}