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
    public class ReportRPV_TechSIS
    {
        internal string FileName;
        internal string CompanyCode;
        internal StreamWriter Writer_Arq = null;


        public ReportRPV_TechSIS(string FileName, string CompanyCode)
        {
            this.FileName = FileName;
            this.CompanyCode = CompanyCode;
        }


        public void ReportRPV_Try(params Control[] ThisControles)
        {
            //CRIA O DIRETÓRIO ..\Print se ele não existir
            #region CRIA O DIRETÓRIO ..\Print
            if (!Directory.Exists(@"..\Print"))
            {
                Directory.CreateDirectory(@"..\Print");
            }
            #endregion
            //EXCLUI O ARQUIVO RPV CASO ELE EXISTA
            #region EXCLUIR O ARQUIVO .RPV CASO ELE JÁ EXISTE
            if (File.Exists(@"..\Print\" + FileName + ".rpv"))
            {
                File.Delete(@"..\Print\" + FileName + ".rpv");
            }
            #endregion


            //instancio o StreamWriter
            StreamWriter Writer_Arq = new StreamWriter(@"..\Print\" + FileName + ".rpv", true, Encoding.Default);

            //AQUI VOU TRATAR 1 POR 1 OS NOMES DO RELATORIO E DO CABECALHO!!
            //CADA NOVO RELATÓRIO QUE FOR CRIADO, ISSO DEVE SER MODIFICADO
            #region TRATAMENTOS PARA NomeDoRelatorio & NomeDoCabecalho
            switch (FileName)
            {
                case "TabProgr":
                    ReportRPV_Statics.NomeDoRelatorio = "RELATÓRIO DE CADASTROS DE PROGRAMAS - {TAB=150}{c=8}09.02.00{c=0}";
                    ReportRPV_Statics.NomeDoCabecalho = "RELATÓRIO DE CADASTROS DE PROGRAMAS";
                    break;
                case "TabUsuar":
                    ReportRPV_Statics.NomeDoRelatorio = "RELATÓRIO DE CADASTROS DE USUÁRIOS - {TAB=150}{c=8}09.01.00{c=0}";
                    ReportRPV_Statics.NomeDoCabecalho = "RELATÓRIO DE CADASTROS DE USUÁRIOS";
                    break;
                case "TabPermi":
                    ReportRPV_Statics.NomeDoRelatorio = "RELATÓRIO DE CADASTROS DE PERMISSÕES - {TAB=150}{c=8}09.03.00{c=0}";
                    ReportRPV_Statics.NomeDoCabecalho = "RELATÓRIO DE CADASTROS DE PERMISSÕES";
                    break;
                case "TabCidad":
                    ReportRPV_Statics.NomeDoRelatorio = "RELATÓRIO DE CADASTROS DE CIDADES - {TAB=150}{c=8}01.02.00{c=0}";
                    ReportRPV_Statics.NomeDoCabecalho = "RELATÓRIO DE CADASTROS DE CIDADES";
                    break;
                case "TabClien":
                    ReportRPV_Statics.NomeDoRelatorio = "RELATÓRIO DE CADASTROS DE CLIENTES - {TAB=150}{c=8}02.01.00{c=0}";
                    ReportRPV_Statics.NomeDoCabecalho = "RELATÓRIO DE CADASTROS DE CLIENTES";
                    break;
                case "TabMsgNt":
                    ReportRPV_Statics.NomeDoRelatorio = "RELATÓRIO DE CADASTROS DE MENSAGENS - {TAB=150}{c=8}01.05.00{c=0}";
                    ReportRPV_Statics.NomeDoCabecalho = "RELATÓRIO DE CADASTROS DE MENSAGENS";
                    break;
                case "TabCfope":
                    ReportRPV_Statics.NomeDoRelatorio = "RELATÓRIO DE CADASTROS DE CFOPs - {TAB=150}{c=8}01.03.00{c=0}";
                    ReportRPV_Statics.NomeDoCabecalho = "RELATÓRIO DE CADASTROS DE CFOPs";
                    break;
                case "TabRotas":
                    ReportRPV_Statics.NomeDoRelatorio = "RELATÓRIO DE CADASTROS DE ROTAS - {TAB=150}{c=8}01.04.00{c=0}";
                    ReportRPV_Statics.NomeDoCabecalho = "RELATÓRIO DE CADASTROS DE ROTAS";
                    break;
                case "TabConve":
                    ReportRPV_Statics.NomeDoRelatorio = "CADASTROS DE CONVÊNIOS E CARTÕES - {TAB=150}{c=8}01.06.00{c=0}";
                    ReportRPV_Statics.NomeDoCabecalho = "CADASTROS DE CONVÊNIOS E CARTÕES";
                    break;
                case "TabSetor":
                    ReportRPV_Statics.NomeDoRelatorio = "RELATÓRIO DE SETORES E SUBSETORES - {TAB=150}{c=8}01.07.00{c=0}";
                    ReportRPV_Statics.NomeDoCabecalho = "RELATÓRIO DE SETORES E SUBSETORES";
                    break;
            }
            #endregion

            //Cabeçalho
            Writer_Arq.WriteLine(@"Orientation=1");
            Writer_Arq.WriteLine(@"Papersize=A4");
            Writer_Arq.WriteLine(@"Report_title=" + ReportRPV_Statics.NomeDoCabecalho);
            Writer_Arq.WriteLine(@"{f=Segoe UI Symbol;s=20;b=y}");
            Writer_Arq.WriteLine(@"[header]");
            Writer_Arq.WriteLine(@"[data]");


            //FAZ O SELECT NO BANCO DE DADOS E TRAZ OS DADOS DA EMPRESA
            ReportRPV_AboutCompany AboutCompany = new ReportRPV_AboutCompany(FileName, CompanyCode);
            AboutCompany.CompanyFields(Writer_Arq);

            //CADA NOVO RELATÓRIO TERÁ QUE SER FEITA UMA NOME VERIFICAÇÃO DE COLUNAS
            ReportRPV_ColunsDesign Coluns = new ReportRPV_ColunsDesign(FileName, CompanyCode);
            Coluns.DefiningColumns(Writer_Arq);
           
            //ALIMENTA PARA TabProgr
            ReportRPV_FeedsReport Feeds = new ReportRPV_FeedsReport(FileName, CompanyCode);
            Feeds.TabProgr_Report(Writer_Arq, ThisControles);

            ////ALIMENTA PARA TabUsuar
            //ALIMENTA.TabUsuar_ImpreRPV_Ali(FileName, rabOrdemAlfabetica, rabOrdemAlfabeticaApelid, comSituacaoIMP, comPermissaoIMP, txtEmpreCodImp, cheImpApelid, Writer_Arq, NomeDoRelatorio, CompanyCode);

            ////ALIMENTA PARA TabPermi
            //ALIMENTA.TabPermi_ImpreRPV_Ali(FileName, rabOrdemAlfabetica, txtEmpreCodImp, txtImpUsuarCod, cheImpApelid, Writer_Arq, NomeDoRelatorio, CompanyCode);

            ////ALIMENTA PARA TabCidad
            //ALIMENTA.TabCidad_ImpreRPV_Ali(FileName, rabOrdemAlfabetica, Writer_Arq, NomeDoRelatorio, CompanyCode, comSituacaoIMP);

            ////ALIMENTA PARA TabUsuar
            //ALIMENTA.TabClien_ImpreRPV_Ali(FileName, rabOrdemAlfabetica, rabOrdemAlfabeticaApelid, comSituacaoIMP, comPermissaoIMP, txtImpUsuarCod, txtEmpreCodImp, cheImpApelid, Writer_Arq, NomeDoRelatorio, CompanyCode);

            ////ALIMENTA PARA TabMsgNt
            //ALIMENTA.TabMsgNt_ImpreRPV_Ali(FileName, rabOrdemAlfabetica, comSituacaoIMP, Writer_Arq, NomeDoRelatorio, CompanyCode);

            ////ALIMENTA PARA TabCfope
            //ALIMENTA.TabCfope_ImpreRPV_Ali(FileName, rabOrdemAlfabetica, comSituacaoIMP, Writer_Arq, NomeDoRelatorio, CompanyCode);

            ////ALIMENTA PARA TabRotas
            //ALIMENTA.TabRotas_ImpreRPV_Ali(FileName, rabOrdemAlfabetica, comSituacaoIMP, Writer_Arq, NomeDoRelatorio, CompanyCode);

            //ALIMENTA PARA TabConve
            //ALIMENTA.TabConve_ImpreRPV_Ali(FileName, rabOrdemAlfabetica, comSituacaoIMP, Writer_Arq, NomeDoRelatorio, CompanyCode);

            ////ALIMENTA PARA TabSetor
            //ALIMENTA.TabSetor_ImpreRPV_Ali(FileName, rabOrdemAlfabetica, comSituacaoIMP, Writer_Arq, NomeDoRelatorio, CompanyCode, comPermissaoIMP);
            //#endregion



            //CASO TENHA INFORMAÇÃO.. CHAMA O PRINTRPV
            //CASO NÃO TENHA, DA A MENSAGEM
            if (ReportRPV_Statics.FeedsEncontrouInformação == "SIM")
            {
                try
                {
                    System.Diagnostics.Process.Start(@"..\Print\Rpv.exe", @"..\Print\" + FileName + ".rpv");
                    ThisControles[0].Text = string.Empty;
                }

                catch (Exception Ex)
                {
                    ThisControles[0].Text = string.Empty;
                    MessageBox.Show("ERRO AO TENTAR ABRIR O RELATÓRIO:\n" + Ex.Message + "\nVERIFIQUE SE O ARQUIVO (..\\Print\\Rpv.exe) É VÁLIDO.", "TechSIS Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                ThisControles[0].Text = string.Empty;
                MessageBox.Show("Nenhuma informação encontrada", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Writer_Arq.Close();
            Writer_Arq.Dispose();

        }
    }
}