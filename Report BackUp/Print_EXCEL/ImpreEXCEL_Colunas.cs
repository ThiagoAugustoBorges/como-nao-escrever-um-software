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
    internal class ImpreEXCEL_Colunas
    {
        public void EXCEL_DefineColunas(string NomeDoArquivo, Excel.Worksheet Worksheet_EXCEL)
        {
            switch (NomeDoArquivo)
            {
                //ESPECIFICA AS COLUNAS PARA EXC_TabProgr
                #region EXC_TabProgr.xlsx
                case "EXC_TabProgr.xlsx":
                    Worksheet_EXCEL.Range[Worksheet_EXCEL.Cells[1, 1], Worksheet_EXCEL.Cells[1, 4]].Merge();
                    Worksheet_EXCEL.Cells[1, 1] = "TechSIS INF - ONDE A IMAGINAÇÃO VIRA CÓDIGO!";

                    Worksheet_EXCEL.Cells[1, 1].Font.Color = Excel.XlRgbColor.rgbBlack;

                    Worksheet_EXCEL.Cells[2, 1] = "CÓDIGO";
                    Worksheet_EXCEL.Cells[2, 2] = "DESCRIÇÃO DO PROGRAMA";
                    Worksheet_EXCEL.Cells[2, 3] = "MÓDULO";
                    Worksheet_EXCEL.Cells[2, 4] = "STATUS";
                    Worksheet_EXCEL.Cells.Interior.Color = Excel.XlRgbColor.rgbWhite;
                    Worksheet_EXCEL.Cells.Font.Name = "Courrier New";
                    Worksheet_EXCEL.Cells[2, 1].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 2].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 3].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 4].Font.Size = "10";

                    Worksheet_EXCEL.Cells[2, 1].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 2].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 3].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 4].Font.Bold = true;



                    Worksheet_EXCEL.Cells[1].ColumnWidth = 15.0F;
                    Worksheet_EXCEL.Cells[2].ColumnWidth = 50.0F;
                    Worksheet_EXCEL.Cells[3].ColumnWidth = 5.0F;
                    Worksheet_EXCEL.Cells[3].ColumnWidth = 10.0F;

                    Worksheet_EXCEL.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    Worksheet_EXCEL.Cells[1, 1].Interior.Color = Excel.XlRgbColor.rgbGold;

                    Worksheet_EXCEL.Cells[2, 1].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 2].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 3].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 4].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    break;
                #endregion
                //ESPECIFICA AS COLUNAS PARA EXC_TabUsuar
                #region EXC_TabUsuar.xlsx
                case "EXC_TabUsuar.xlsx":
                    Worksheet_EXCEL.Range[Worksheet_EXCEL.Cells[1, 1], Worksheet_EXCEL.Cells[1, 4]].Merge();
                    Worksheet_EXCEL.Cells[1, 1] = "TechSIS INF - ONDE A IMAGINAÇÃO VIRA CÓDIGO!";

                    Worksheet_EXCEL.Cells[1, 1].Font.Color = Excel.XlRgbColor.rgbBlack;

                    Worksheet_EXCEL.Cells[2, 1] = "Cód.";
                    Worksheet_EXCEL.Cells[2, 2] = "DESCRIÇÃO DO USUÁRIO";
                    Worksheet_EXCEL.Cells[2, 3] = "PERMISSÃO";
                    Worksheet_EXCEL.Cells[2, 4] = "EMPRESA";
                    Worksheet_EXCEL.Cells.Interior.Color = Excel.XlRgbColor.rgbWhite;
                    Worksheet_EXCEL.Cells.Font.Name = "Courrier New";
                    Worksheet_EXCEL.Cells[2, 1].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 2].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 3].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 4].Font.Size = "10";

                    Worksheet_EXCEL.Cells[2, 1].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 2].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 3].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 4].Font.Bold = true;



                    Worksheet_EXCEL.Cells[1].ColumnWidth = 11.0F;
                    Worksheet_EXCEL.Cells[2].ColumnWidth = 50.0F;
                    Worksheet_EXCEL.Cells[3].ColumnWidth = 15.0F;
                    Worksheet_EXCEL.Cells[4].ColumnWidth = 13.0F;

                    Worksheet_EXCEL.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    Worksheet_EXCEL.Cells[1, 1].Interior.Color = Excel.XlRgbColor.rgbGold;

                    Worksheet_EXCEL.Cells[2, 1].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 2].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 3].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 4].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    break;
                #endregion
                //ESPECIFICA AS COLUNAS PARA EXC_TabPermi
                #region EXC_TabPermi.xlsx
                case "EXC_TabPermi.xlsx":
                    Worksheet_EXCEL.Range[Worksheet_EXCEL.Cells[1, 1], Worksheet_EXCEL.Cells[1, 7]].Merge();
                    Worksheet_EXCEL.Cells[1, 1] = "TechSIS INF - ONDE A IMAGINAÇÃO VIRA CÓDIGO!";

                    Worksheet_EXCEL.Cells[1, 1].Font.Color = Excel.XlRgbColor.rgbBlack;

                    Worksheet_EXCEL.Cells[2, 1] = "Cód.Usu";
                    Worksheet_EXCEL.Cells[2, 2] = "Cód.Pgr";
                    Worksheet_EXCEL.Cells[2, 3] = "INC";
                    Worksheet_EXCEL.Cells[2, 4] = "ALT";
                    Worksheet_EXCEL.Cells[2, 5] = "EXC";
                    Worksheet_EXCEL.Cells[2, 6] = "ABA";
                    Worksheet_EXCEL.Cells[2, 7] = "CON";

                    Worksheet_EXCEL.Cells.Interior.Color = Excel.XlRgbColor.rgbWhite;
                    Worksheet_EXCEL.Cells.Font.Name = "Courrier New";
                    Worksheet_EXCEL.Cells[2, 1].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 2].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 3].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 4].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 5].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 6].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 7].Font.Size = "10";

                    Worksheet_EXCEL.Cells[2, 1].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 2].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 3].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 4].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 5].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 6].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 7].Font.Bold = true;



                    Worksheet_EXCEL.Cells[1].ColumnWidth = 12.0F;
                    Worksheet_EXCEL.Cells[2].ColumnWidth = 12.0F;
                    Worksheet_EXCEL.Cells[3].ColumnWidth = 11.0F;
                    Worksheet_EXCEL.Cells[4].ColumnWidth = 11.0F;
                    Worksheet_EXCEL.Cells[5].ColumnWidth = 11.0F;
                    Worksheet_EXCEL.Cells[6].ColumnWidth = 11.0F;
                    Worksheet_EXCEL.Cells[7].ColumnWidth = 11.0F;

                    Worksheet_EXCEL.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    Worksheet_EXCEL.Cells[1, 1].Interior.Color = Excel.XlRgbColor.rgbGold;

                    Worksheet_EXCEL.Cells[2, 1].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 2].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 3].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 4].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 5].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 6].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 7].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    break;
                #endregion
                //ESPECIFICA AS COLUNAS PARA EXC_TabUsuar
                #region EXC_TabCidad.xlsx
                case "EXC_TabCidad.xlsx":
                    Worksheet_EXCEL.Range[Worksheet_EXCEL.Cells[1, 1], Worksheet_EXCEL.Cells[1, 4]].Merge();
                    Worksheet_EXCEL.Cells[1, 1] = "TechSIS INF - ONDE A IMAGINAÇÃO VIRA CÓDIGO!";

                    Worksheet_EXCEL.Cells[1, 1].Font.Color = Excel.XlRgbColor.rgbBlack;

                    Worksheet_EXCEL.Cells[2, 1] = "Cód.";
                    Worksheet_EXCEL.Cells[2, 2] = "DESCRIÇÃO DA CIDADE";
                    Worksheet_EXCEL.Cells[2, 3] = "IBGE";
                    Worksheet_EXCEL.Cells[2, 4] = "STATUS";
                    Worksheet_EXCEL.Cells.Interior.Color = Excel.XlRgbColor.rgbWhite;
                    Worksheet_EXCEL.Cells.Font.Name = "Courrier New";
                    Worksheet_EXCEL.Cells[2, 1].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 2].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 3].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 4].Font.Size = "10";

                    Worksheet_EXCEL.Cells[2, 1].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 2].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 3].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 4].Font.Bold = true;



                    Worksheet_EXCEL.Cells[1].ColumnWidth = 11.0F;
                    Worksheet_EXCEL.Cells[2].ColumnWidth = 50.0F;
                    Worksheet_EXCEL.Cells[3].ColumnWidth = 15.0F;
                    Worksheet_EXCEL.Cells[4].ColumnWidth = 13.0F;

                    Worksheet_EXCEL.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    Worksheet_EXCEL.Cells[1, 1].Interior.Color = Excel.XlRgbColor.rgbGold;

                    Worksheet_EXCEL.Cells[2, 1].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 2].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 3].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 4].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    break;
                #endregion
                //ESPECIFICA AS COLUNAS PARA EXC_TabClien
                #region EXC_TabClien.xlsx
                case "EXC_TabClien.xlsx":
                    Worksheet_EXCEL.Range[Worksheet_EXCEL.Cells[1, 1], Worksheet_EXCEL.Cells[1, 3]].Merge();
                    Worksheet_EXCEL.Cells[1, 1] = "TechSIS INF - ONDE A IMAGINAÇÃO VIRA CÓDIGO!";

                    Worksheet_EXCEL.Cells[1, 1].Font.Color = Excel.XlRgbColor.rgbBlack;

                    Worksheet_EXCEL.Cells[2, 1] = "CÓDIGO";
                    Worksheet_EXCEL.Cells[2, 2] = "  DESCRIÇÃO DO CLIENTE  ";
                    Worksheet_EXCEL.Cells[2, 3] = "  CPF.CNPJ  ";
                    Worksheet_EXCEL.Cells.Interior.Color = Excel.XlRgbColor.rgbWhite;
                    Worksheet_EXCEL.Cells.Font.Name = "Courrier New";
                    Worksheet_EXCEL.Cells[2, 1].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 2].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 3].Font.Size = "10";

                    Worksheet_EXCEL.Cells[2, 1].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 2].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 3].Font.Bold = true;



                    Worksheet_EXCEL.Cells[1].ColumnWidth = 11.0F;
                    Worksheet_EXCEL.Cells[2].ColumnWidth = 56.0F;
                    Worksheet_EXCEL.Cells[3].ColumnWidth = 20.0F;

                    Worksheet_EXCEL.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    Worksheet_EXCEL.Cells[1, 1].Interior.Color = Excel.XlRgbColor.rgbGold;

                    Worksheet_EXCEL.Cells[2, 1].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 2].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 3].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    break;
                #endregion
                //ESPECIFICA AS COLUNAS PARA EXC_TabMsgNt
                #region EXC_TabMsgNt.xlsx
                case "EXC_TabMsgNt.xlsx":
                    Worksheet_EXCEL.Range[Worksheet_EXCEL.Cells[1, 1], Worksheet_EXCEL.Cells[1, 3]].Merge();
                    Worksheet_EXCEL.Cells[1, 1] = "TechSIS INF - ONDE A IMAGINAÇÃO VIRA CÓDIGO!";

                    Worksheet_EXCEL.Cells[1, 1].Font.Color = Excel.XlRgbColor.rgbBlack;


                    Worksheet_EXCEL.Cells[2, 1] = "CÓDIGO";
                    Worksheet_EXCEL.Cells[2, 2] = "  DESCRIÇÃO DA MENSAGEM  ";
                    Worksheet_EXCEL.Cells[2, 3] = "  EMPRESA  ";
                    Worksheet_EXCEL.Cells.Interior.Color = Excel.XlRgbColor.rgbWhite;
                    Worksheet_EXCEL.Cells.Font.Name = "Courrier New";
                    Worksheet_EXCEL.Cells[2, 1].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 2].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 3].Font.Size = "10";

                    Worksheet_EXCEL.Cells[2, 1].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 2].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 3].Font.Bold = true;



                    Worksheet_EXCEL.Cells[1].ColumnWidth = 11.0F;
                    Worksheet_EXCEL.Cells[2].ColumnWidth = 56.0F;
                    Worksheet_EXCEL.Cells[3].ColumnWidth = 20.0F;

                    Worksheet_EXCEL.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    Worksheet_EXCEL.Cells[1, 1].Interior.Color = Excel.XlRgbColor.rgbGold;

                    Worksheet_EXCEL.Cells[2, 1].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 2].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 3].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    break;
                #endregion
                //ESPECIFICA AS COLUNAS PARA EXC_TabCfope
                #region EXC_TabCfope.xlsx
                case "EXC_TabCfope.xlsx":
                    Worksheet_EXCEL.Range[Worksheet_EXCEL.Cells[1, 1], Worksheet_EXCEL.Cells[1, 4]].Merge();
                    Worksheet_EXCEL.Cells[1, 1] = "TechSIS INF - ONDE A IMAGINAÇÃO VIRA CÓDIGO!";

                    Worksheet_EXCEL.Cells[1, 1].Font.Color = Excel.XlRgbColor.rgbBlack;

                    Worksheet_EXCEL.Cells[2, 1] = "CFOP";
                    Worksheet_EXCEL.Cells[2, 2] = "  DESCRIÇÃO DO CFOP  ";
                    Worksheet_EXCEL.Cells[2, 3] = "C COM";
                    Worksheet_EXCEL.Cells[2, 4] = "C IND";
                    Worksheet_EXCEL.Cells.Interior.Color = Excel.XlRgbColor.rgbWhite;
                    Worksheet_EXCEL.Cells.Font.Name = "Courrier New";
                    Worksheet_EXCEL.Cells[2, 1].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 2].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 3].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 4].Font.Size = "10";

                    Worksheet_EXCEL.Cells[2, 1].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 2].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 3].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 4].Font.Bold = true;



                    Worksheet_EXCEL.Cells[1].ColumnWidth = 12.0F;
                    Worksheet_EXCEL.Cells[2].ColumnWidth = 56.0F;
                    Worksheet_EXCEL.Cells[3].ColumnWidth = 10.0F;
                    Worksheet_EXCEL.Cells[4].ColumnWidth = 10.0F;

                    Worksheet_EXCEL.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    Worksheet_EXCEL.Cells[1, 1].Interior.Color = Excel.XlRgbColor.rgbGold;

                    Worksheet_EXCEL.Cells[2, 1].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 2].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 3].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 4].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    break;
                #endregion
                //ESPECIFICA AS COLUNAS PARA EXC_TabRotas
                #region EXC_TabRotas.xlsx
                case "EXC_TabRotas.xlsx":
                    Worksheet_EXCEL.Range[Worksheet_EXCEL.Cells[1, 1], Worksheet_EXCEL.Cells[1, 3]].Merge();
                    Worksheet_EXCEL.Cells[1, 1] = "TechSIS INF - ONDE A IMAGINAÇÃO VIRA CÓDIGO!";

                    Worksheet_EXCEL.Cells[1, 1].Font.Color = Excel.XlRgbColor.rgbBlack;


                    Worksheet_EXCEL.Cells[2, 1] = " CÓDIGO ";
                    Worksheet_EXCEL.Cells[2, 2] = "    DESCRIÇÃO DA ROTA    ";
                    Worksheet_EXCEL.Cells[2, 3] = " STATUS ";

                    Worksheet_EXCEL.Cells.Interior.Color = Excel.XlRgbColor.rgbWhite;
                    Worksheet_EXCEL.Cells.Font.Name = "Courrier New";
                    Worksheet_EXCEL.Cells[2, 1].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 2].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 3].Font.Size = "10";

                    Worksheet_EXCEL.Cells[2, 1].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 2].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 3].Font.Bold = true;



                    Worksheet_EXCEL.Cells[1].ColumnWidth = 12.0F;
                    Worksheet_EXCEL.Cells[2].ColumnWidth = 60.0F;
                    Worksheet_EXCEL.Cells[3].ColumnWidth = 14.0F;

                    Worksheet_EXCEL.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    Worksheet_EXCEL.Cells[1, 1].Interior.Color = Excel.XlRgbColor.rgbGold;

                    Worksheet_EXCEL.Cells[2, 1].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 2].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 3].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    break;
                #endregion
                //ESPECIFICA AS COLUNAS PARA EXC_TabConve
                #region EXC_TabConve.xlsx
                case "EXC_TabConve.xlsx":

                    Worksheet_EXCEL.Range[Worksheet_EXCEL.Cells[1, 1], Worksheet_EXCEL.Cells[1, 4]].Merge();
                    Worksheet_EXCEL.Cells[1, 1] = "TechSIS INF - ONDE A IMAGINAÇÃO VIRA CÓDIGO!";

                    Worksheet_EXCEL.Cells[1, 1].Font.Color = Excel.XlRgbColor.rgbBlack;


                    Worksheet_EXCEL.Cells[2, 1] = " CÓDIGO ";
                    Worksheet_EXCEL.Cells[2, 2] = "    CONVÊNIO OU CARTÃO    ";
                    Worksheet_EXCEL.Cells[2, 3] = " STATUS ";
                    Worksheet_EXCEL.Cells[2, 4] = " TIPO ";
                    Worksheet_EXCEL.Cells.Interior.Color = Excel.XlRgbColor.rgbWhite;


                    Worksheet_EXCEL.Cells.Font.Name = "Courrier New";
                    Worksheet_EXCEL.Cells[2, 1].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 2].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 3].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 4].Font.Size = "10";

                    Worksheet_EXCEL.Cells[2, 1].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 2].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 3].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 4].Font.Bold = true;



                    Worksheet_EXCEL.Cells[1].ColumnWidth = 12.0F;
                    Worksheet_EXCEL.Cells[2].ColumnWidth = 52.43F;
                    Worksheet_EXCEL.Cells[3].ColumnWidth = 14.0F;
                    Worksheet_EXCEL.Cells[4].ColumnWidth = 11.0F;


                    Worksheet_EXCEL.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    Worksheet_EXCEL.Cells[1, 1].Interior.Color = Excel.XlRgbColor.rgbGold;

                    Worksheet_EXCEL.Cells[2, 1].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 2].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 3].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 4].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    break;
                #endregion
                //ESPECIFICA AS COLUNAS PARA EXC_TabSetor
                #region EXC_TabSetor.xlsx
                case "EXC_TabSetor.xlsx":

                    Worksheet_EXCEL.Range[Worksheet_EXCEL.Cells[1, 1], Worksheet_EXCEL.Cells[1, 4]].Merge();
                    Worksheet_EXCEL.Cells[1, 1] = "TechSIS INF - ONDE A IMAGINAÇÃO VIRA CÓDIGO!";

                    Worksheet_EXCEL.Cells[1, 1].Font.Color = Excel.XlRgbColor.rgbBlack;


                    Worksheet_EXCEL.Cells[2, 1] = " CÓDIGO ";
                    Worksheet_EXCEL.Cells[2, 2] = "    DESCRIÇÃO DO SETOR\\SUBSETOR    ";
                    Worksheet_EXCEL.Cells[2, 3] = " LOCALIZAÇÃO ";
                    Worksheet_EXCEL.Cells[2, 4] = " TIPO ";
                    Worksheet_EXCEL.Cells.Interior.Color = Excel.XlRgbColor.rgbWhite;


                    Worksheet_EXCEL.Cells.Font.Name = "Courrier New";
                    Worksheet_EXCEL.Cells[2, 1].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 2].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 3].Font.Size = "10";
                    Worksheet_EXCEL.Cells[2, 4].Font.Size = "10";

                    Worksheet_EXCEL.Cells[2, 1].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 2].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 3].Font.Bold = true;
                    Worksheet_EXCEL.Cells[2, 4].Font.Bold = true;



                    Worksheet_EXCEL.Cells[1].ColumnWidth = 12.0F;
                    Worksheet_EXCEL.Cells[2].ColumnWidth = 52.43F;
                    Worksheet_EXCEL.Cells[3].ColumnWidth = 19.14F;
                    Worksheet_EXCEL.Cells[4].ColumnWidth = 5.86F;


                    Worksheet_EXCEL.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    Worksheet_EXCEL.Cells[1, 1].Interior.Color = Excel.XlRgbColor.rgbGold;

                    Worksheet_EXCEL.Cells[2, 1].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 2].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 3].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    Worksheet_EXCEL.Cells[2, 4].Interior.Color = Excel.XlRgbColor.rgbGrey;
                    break;
                #endregion
            }
        }
    }
}
