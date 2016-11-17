using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Word = Microsoft.Office.Interop.Word;

namespace Print_WORD
{
    internal class ImpreWORD_Colunas
    {
        public string ColunasDocumento { get; set; }


        //DEFINE AS COLUNAS DO CABEÇALHO
        public void WORD_DefineCabeçalho(string LojaLogada, Word.Application Word_Appli, int NumeroDaPagina)
        {
            //BUSCA OS DADOS DA LOJA
            ImpreWORD_BuscaDadosLoja Loja = new ImpreWORD_BuscaDadosLoja();
            Loja.BuscaDadosLoja(LojaLogada);

            //ESCREVE OS DADOS EM VERDE DO CABEÇALHO
            #region DADOS CABEÇALHO EMPRESA
            Word_Appli.ActiveWindow.Selection.Font.Bold = 1;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorGreen;
            string Empresa = "EMPRESA..: ";
            Word_Appli.Selection.TypeText(Empresa);
            Word_Appli.ActiveWindow.Selection.Font.Bold = 0;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorBlack;
            string DadosEmpresa = Loja.FANTASIA.PadRight(84, ' ');
            Word_Appli.Selection.TypeText(DadosEmpresa);

            Word_Appli.ActiveWindow.Selection.Font.Bold = 1;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorGreen;
            string CpfCnpj = "CPF.CNPJ.: ";
            Word_Appli.Selection.TypeText(CpfCnpj);
            Word_Appli.ActiveWindow.Selection.Font.Bold = 0;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorBlack;
            string DadosCpfCnpj = Loja.CPFCNPJ.PadRight(42, ' ');
            Word_Appli.Selection.TypeText(DadosCpfCnpj);

            Word_Appli.ActiveWindow.Selection.Font.Bold = 1;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorGreen;
            string Inscr_ESTADUAL = "INSCRIÇÃO ESTADUAL.: ";
            Word_Appli.Selection.TypeText(Inscr_ESTADUAL);
            Word_Appli.ActiveWindow.Selection.Font.Bold = 0;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorBlack;
            string DadosInscr_ESTADUAL = Loja.INSCRICAO_EST.PadRight(21, ' ');
            Word_Appli.Selection.TypeText(DadosInscr_ESTADUAL);

            Word_Appli.ActiveWindow.Selection.Font.Bold = 1;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorGreen;
            string Endereço = "ENDEREÇO.: ";
            Word_Appli.Selection.TypeText(Endereço);
            Word_Appli.ActiveWindow.Selection.Font.Bold = 0;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorBlack;
            string DadosEndereço = Loja.ENDERECO.PadRight(42, ' ');
            Word_Appli.Selection.TypeText(DadosEndereço);

            Word_Appli.ActiveWindow.Selection.Font.Bold = 1;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorGreen;
            string sCEP = "CEP.: ";
            Word_Appli.Selection.TypeText(sCEP);
            Word_Appli.ActiveWindow.Selection.Font.Bold = 0;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorBlack;
            string sCEPDados = Loja.CEP.PadRight(15, ' ');
            Word_Appli.Selection.TypeText(sCEPDados);

            Word_Appli.ActiveWindow.Selection.Font.Bold = 1;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorGreen;
            string sNUM = "NUM.: ";
            Word_Appli.Selection.TypeText(sNUM);
            Word_Appli.ActiveWindow.Selection.Font.Bold = 0;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorBlack;
            string sNUMDados = Loja.NUM.PadRight(15, ' ');
            Word_Appli.Selection.TypeText(sNUMDados);

            Word_Appli.ActiveWindow.Selection.Font.Bold = 1;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorGreen;
            string sBAI = "BAIRRO...: ";
            Word_Appli.Selection.TypeText(sBAI);
            Word_Appli.ActiveWindow.Selection.Font.Bold = 0;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorBlack;
            string sBAIDados = Loja.BAIRRO.PadRight(42, ' ');
            Word_Appli.Selection.TypeText(sBAIDados);


            Word_Appli.ActiveWindow.Selection.Font.Bold = 1;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorGreen;
            string sCOM = "COMPLEMENTO..: ";
            Word_Appli.Selection.TypeText(sCOM);
            Word_Appli.ActiveWindow.Selection.Font.Bold = 0;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorBlack;
            string sCOMDados = Loja.COMPLE.PadRight(27, ' ');
            Word_Appli.Selection.TypeText(sCOMDados);

            Word_Appli.ActiveWindow.Selection.Font.Bold = 1;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorGreen;
            string sCID = "CIDADE...: ";
            Word_Appli.Selection.TypeText(sCID);
            Word_Appli.ActiveWindow.Selection.Font.Bold = 0;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorBlack;
            string CidadeTRAT = Loja.CIDADE.PadLeft(6, '0');
            string sCIDDados_COD = CidadeTRAT.PadRight(06, ' ');
            string sCIDDados_DES = Loja.CIDADE_DESC.PadRight(56, ' ');
            Word_Appli.Selection.TypeText(sCIDDados_COD + " " + sCIDDados_DES);

            Word_Appli.ActiveWindow.Selection.Font.Bold = 1;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorGreen;
            string sUF = "UF..: ";
            Word_Appli.Selection.TypeText(sUF);
            Word_Appli.ActiveWindow.Selection.Font.Bold = 0;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorBlack;
            string sUFDados = Loja.UF.PadRight(15, ' ');
            Word_Appli.Selection.TypeText(sUFDados);

            Word_Appli.ActiveWindow.Selection.Font.Bold = 1;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorGreen;
            string sTEL = "TELEFONE.: ";
            Word_Appli.Selection.TypeText(sTEL);
            Word_Appli.ActiveWindow.Selection.Font.Bold = 0;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorBlack;
            string sTELDados = Loja.TELEFONE.PadRight(42, ' ');
            Word_Appli.Selection.TypeText(sTELDados);

            Word_Appli.ActiveWindow.Selection.Font.Bold = 1;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorGreen;
            string sFAX = "FAX.....: ";
            Word_Appli.Selection.TypeText(sFAX);
            Word_Appli.ActiveWindow.Selection.Font.Bold = 0;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorBlack;
            string sFAXDados = Loja.FAX.PadRight(32, ' ');
            Word_Appli.Selection.TypeText(sFAXDados);

            Word_Appli.ActiveWindow.Selection.Font.Bold = 1;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorGreen;
            string sEMAIL = "EMAIL....: ";
            Word_Appli.Selection.TypeText(sEMAIL);
            Word_Appli.ActiveWindow.Selection.Font.Bold = 0;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorBlack;
            string sEMAILDados = Loja.EMAIL.PadRight(84, ' ');
            Word_Appli.Selection.TypeText(sEMAILDados);

            Word_Appli.ActiveWindow.Selection.Font.Bold = 1;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorGreen;
            string sHOME = "HOME PAGE: ";
            Word_Appli.Selection.TypeText(sHOME);
            Word_Appli.ActiveWindow.Selection.Font.Bold = 0;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorBlack;
            string sHOMEDados = Loja.HOME_PAGE.PadRight(62, ' ');
            Word_Appli.Selection.TypeText(sHOMEDados);


            Word_Appli.ActiveWindow.Selection.Font.Bold = 1;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorGreen;
            string sPAGINA = "PÁGINA..: " + NumeroDaPagina.ToString("000000") + "      ";
            Word_Appli.Selection.TypeText(sPAGINA);
            Word_Appli.ActiveWindow.Selection.Font.Bold = 0;
            Word_Appli.ActiveWindow.Selection.Font.Color = Word.WdColor.wdColorBlack;

            Word_Appli.Selection.TypeText("-----------------------------------------------------------------------------------------------");
            #endregion
        }

        //DEFINE AS COLUNAS PAR AO DOCUMENTO
        public void WORD_DefineColunas(string NomeDoArquivo)
        {
            string Column1 = "";
            string Column2 = "";
            string Column3 = "";
            string Column4 = "";
            string Column5 = "";
            string Column6 = "";
            string Column7 = "";

            #region WORD_TabProgr.docx
            if (NomeDoArquivo == "WORD_TabProgr.docx")
            {
                Column1 = "*-CÓDIGO-* ";
                Column2 = "*-----------------------(DESCRIÇÃO)-----------------------* ";
                Column3 = "*-STATUS-* ";
                Column4 = "*--MÓDULO--* ";

                ColunasDocumento = Column1 + Column2 + Column3 + Column4;
            }
            #endregion
            #region WORD_TabUsuar.docx
            if (NomeDoArquivo == "WORD_TabUsuar.docx")
            {
                Column1 = "*-CÓDIGO-* ";
                Column2 = "*---------------------(DESCRIÇÃO)----------------------* ";
                Column3 = "*-PERMISSÃO-* ";
                Column4 = "*--EMPRESA--* ";

                ColunasDocumento = Column1 + Column2 + Column3 + Column4;
            }
            #endregion
            #region WORD_TabPermi.docx
            if (NomeDoArquivo == "WORD_TabPermi.docx")
            {
                Column1 = "*CÓDIGO* ";
                Column2 = "*-----------(DESCRIÇÃO)------------* ";
                Column3 = "*-(PROGRAMA)---------------* ";
                Column4 = "*INC*";
                Column5 = "*ALT*";
                Column6 = "*EXC*";
                Column7 = "*CON*";
                Column7 = "*ABA* ";

                ColunasDocumento = Column1 + Column2 + Column3 + Column4 + Column5 + Column6 + Column7;
            }
            #endregion
            #region WORD_TabCidad.docx
            if (NomeDoArquivo == "WORD_TabCidad.docx")
            {
                Column1 = "{CÓDIGO}";
                Column2 = "*---------(DESCRIÇÃO DA CIDADE)---------*";
                Column3 = "*(UF)*";
                Column4 = "*----(PAÍS)----* ";
                Column5 = "*-(IBGE)-* ";
                Column6 = "*-(STATUS)-* ";
                

                ColunasDocumento = Column1 + Column2 + Column3 + Column4 + Column5 + Column6 + Column7;
            }
            #endregion
            #region WORD_TabClien.docx
            if (NomeDoArquivo == "WORD_TabClien.docx")
            {
                Column1 = "(CÓDIGO)";
                Column2 = "*---------(DESCRIÇÃO DO CLIENTE)---------*";
                Column3 = "*--(CPF.CNPJ)--*";
                Column4 = "*-(INSCRIÇÃO)-*";
                Column5 = "*-(TELEFONE)-* ";


                ColunasDocumento = Column1 + Column2 + Column3 + Column4 + Column5 + Column6 + Column7;
            }
            #endregion
            #region WORD_TabMsgNt.docx
            if (NomeDoArquivo == "WORD_TabMsgNt.docx")
            {
                Column1 = "(CÓDIGO)";
                Column2 = "*-------(DESCRIÇÃO DA MENSAGEM)-------*";
                Column3 = "*-(EMPRESA)-*";
                Column4 = "*-----(DESCRIÇÃO DA EMPRESA)-----* ";


                ColunasDocumento = Column1 + Column2 + Column3 + Column4 + Column5 + Column6 + Column7;
            }
            #endregion
            #region WORD_TabCfope.docx
            if (NomeDoArquivo == "WORD_TabCfope.docx")
            {
                Column1 = "(CÓDIGO)";
                Column2 = "*-----------(DESCRIÇÃO DO CÓDIGO FISCAL DE OPERAÇÃO)----------*";
                Column3 = "*-(C. IND)-*";
                Column4 = "*-(C. COM)-* ";


                ColunasDocumento = Column1 + Column2 + Column3 + Column4 + Column5 + Column6 + Column7;
            }
            #endregion
            #region WORD_TabRotas.docx
            if (NomeDoArquivo == "WORD_TabRotas.docx")
            {
                Column1 = "(CÓDIGO)--* ";
                Column2 = "*-----------------------(DESCRIÇÃO DA ROTA)------------------------* ";
                Column3 = "*--(STATUS)--* ";


                ColunasDocumento = Column1 + Column2 + Column3 + Column4 + Column5 + Column6 + Column7;
            }
            #endregion
            #region WORD_TabConve.docx
            if (NomeDoArquivo == "WORD_TabConve.docx")
            {
                Column1 = "(CÓDIGO)--* ";
                Column2 = "*----------(DESCRIÇÃO DO CONVÊNIO E CARTÕES)----------* ";
                Column3 = "*--(STATUS)--* ";
                Column4 = "*-(TAXA %)-* ";


                ColunasDocumento = Column1 + Column2 + Column3 + Column4 + Column5 + Column6 + Column7;
            }
            #endregion
            #region WORD_TabSetor.docx
            if (NomeDoArquivo == "WORD_TabSetor.docx")
            {
                Column1 = "(CÓDIGO)--* ";
                Column2 = "*-----------(DESCRIÇÃO DO SETOR\\SUBSETOR)----------* ";
                Column3 = "*--(RESPONSÁVEL)--* ";
                Column4 = "*-(TIPO)-* ";


                ColunasDocumento = Column1 + Column2 + Column3 + Column4 + Column5 + Column6 + Column7;
            }
            #endregion
        }

        //ESCREVE O TITULO DO RELATÓRIO
        public void WORD_EscreveCabecalho(string NomeDoArquivo, Word.Application Word_Appli)
        {
            //DEFINE O TITULO DO CABEÇALHO (DEVE TER 54 POSIÇÕES)
            string Cabecalho_TITULO = "";
            #region TITULO DO CABEÇALHO (TEXTO)
            switch (NomeDoArquivo)
            {
                #region WORD_TabProgr.docx
                case "WORD_TabProgr.docx":
                    Cabecalho_TITULO = "RELATÓRIO GERENCIAL CADASTROS DE PROGRAMAS.: 09-02-00 ";
                    break;
                #endregion
                #region WORD_TabUsuar.docx
                case "WORD_TabUsuar.docx":
                    Cabecalho_TITULO = "RELATÓRIO GERENCIAL CADASTROS DE USUÁRIOS..: 09-01-00 ";
                    break;
                #endregion
                #region WORD_TabPermi.docx
                case "WORD_TabPermi.docx":
                    Cabecalho_TITULO = "RELATÓRIO GERENCIAL CADASTROS DE PERMISSÕES: 09-01-00 ";
                    break;
                #endregion
                #region WORD_TabCidad.docx
                case "WORD_TabCidad.docx":
                    Cabecalho_TITULO = "RELATÓRIO GERENCIAL CADASTROS DE CIDADES...: 01-02-00 ";
                    break;
                #endregion
                #region WORD_TabClien.docx
                case "WORD_TabClien.docx":
                    Cabecalho_TITULO = "RELATÓRIO GERENCIAL CADASTROS DE CLIENTES..: 02-01-00 ";
                    break;
                #endregion
                #region WORD_TabMsgNt.docx
                case "WORD_TabMsgNt.docx":
                    Cabecalho_TITULO = "RELATÓRIO GERENCIAL CADASTROS DE MENSAGENS.: 01-05-00 ";
                    break;
                #endregion
                #region WORD_TabCfope.docx
                case "WORD_TabCfope.docx":
                    Cabecalho_TITULO = "RELATÓRIO GERENCIAL CADASTROS DE CFOPs.....: 01-03-00 ";
                    break;
                #endregion
                #region WORD_TabRotas.docx
                case "WORD_TabRotas.docx":
                    Cabecalho_TITULO = "RELATÓRIO GERENCIAL CADASTROS DE ROTAS.....: 01-04-00 ";
                    break;
                #endregion
                #region WORD_TabConve.docx
                case "WORD_TabConve.docx":
                    Cabecalho_TITULO = "REL. DE CADASTROS DE CONVÊNIOS E CARTÕES...: 01-06-00 ";
                    break;
                #endregion
                #region WORD_TabSetor.docx
                case "WORD_TabSetor.docx":
                    Cabecalho_TITULO = "RELATÓRIO DE CADASTROS DE SETORES..........: 01-07-00 ";
                    break;
                #endregion
            }
            #endregion

            //ESCREVE O NOME DO RELATORIO NA PARTE DE CIMA DO DOCUMENTO
            #region TITULO DO CABEÇALHO
            Word_Appli.ActiveWindow.Selection.Font.Bold = 1;
            Word_Appli.ActiveWindow.Selection.Font.Name = "Courier New";
            Word_Appli.ActiveWindow.Selection.Font.Size = 14;
            Word_Appli.Selection.TypeText("TechSIS INF - ");
            Word_Appli.ActiveWindow.Selection.Font.Bold = 0;
            Word_Appli.Selection.TypeText(Cabecalho_TITULO);
            Word_Appli.ActiveWindow.Selection.Font.Name = "Courier New";
            Word_Appli.ActiveWindow.Selection.Font.Size = 10;
            Word_Appli.Selection.TypeText("-----------------------------------------------------------------------------------------------");
            #endregion
        }
    }
}
