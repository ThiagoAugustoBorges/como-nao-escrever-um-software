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
    public class ImpreWORD
    {
        public void ImpreWORD_GO(string NomeDoArquivo, string LojaLogada, TextBox txtCaminhoRel, ComboBox comSituacaoIMP, RadioButton rabOrdemAlfabetica, TextBox txtQtSelectIMP, CheckBox cheImpApelid, ComboBox comPermissaoIMP, TextBox txtEmpreCodImp, RadioButton rabOrdemAlfabeticaApelid, TextBox txtImpUsuarCod)
        {
            //Adiciono a extenção
            if (NomeDoArquivo != string.Empty)
                NomeDoArquivo += ".docx";

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            Word.Application Word_Appli = new Word.Application();
            Word.Document Word_Document = new Word.Document();


            //BUSCA O LOCAL DE SALVAMENTO
            ImpreWORD_MET MET = new ImpreWORD_MET();
            MET.Busca_CaminhoSALV(LojaLogada, txtCaminhoRel, NomeDoArquivo);


            //FAZ AS PRIMEIRAS FORMATAÇÕES
            Word_Appli.Documents.Add();
            Word_Appli.ActiveDocument.PageSetup.TopMargin = (float)10;
            Word_Appli.ActiveDocument.PageSetup.LeftMargin = (float)15;
            Word_Appli.ActiveDocument.PageSetup.RightMargin = (float)10;
            Word_Appli.ActiveDocument.PageSetup.BottomMargin = (float)10;
            Word_Appli.ActiveWindow.Selection.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
            Word_Appli.ActiveWindow.Selection.ParagraphFormat.SpaceAfter = 0.0F;



       

            ImpreWORD_Colunas Coluns = new ImpreWORD_Colunas();

            //ESCREVE O CABEÇALHO
            Coluns.WORD_EscreveCabecalho(NomeDoArquivo, Word_Appli);
            //DEFINE O PRIMEIRO CABEÇALHO
            Coluns.WORD_DefineCabeçalho(LojaLogada, Word_Appli, 0001);
            //DEINE AS COLUNAS
            Coluns.WORD_DefineColunas(NomeDoArquivo);


            //PEGA NA CLASSE Colunas E JOGA NESSE MÉTODO
            Word_Appli.Selection.TypeText(Coluns.ColunasDocumento);




            ImpreWORD_AlimentaRelatorio ALIMENTA = new ImpreWORD_AlimentaRelatorio();
            #region TabProgr
            ALIMENTA.TabProgr_ImpreWORD_Ali(NomeDoArquivo, LojaLogada, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, Word_Appli);
            #endregion
            #region TabUsuar
            ALIMENTA.TabUsuar_ImpreWORD_Ali(NomeDoArquivo, LojaLogada, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, cheImpApelid, comPermissaoIMP, rabOrdemAlfabeticaApelid, txtEmpreCodImp, Word_Appli);
            #endregion
            #region TabPermi
            ALIMENTA.TabPermi_ImpreWORD_Ali(NomeDoArquivo, LojaLogada, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, cheImpApelid, comPermissaoIMP, rabOrdemAlfabeticaApelid, txtEmpreCodImp, Word_Appli, txtImpUsuarCod);
            #endregion
            #region TabCidad
            ALIMENTA.TabCidad_ImpreWORD_Ali(NomeDoArquivo, LojaLogada, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, Word_Appli);
            #endregion
            #region TabClien
            ALIMENTA.TabClien_ImpreWORD_Ali(NomeDoArquivo, LojaLogada, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, Word_Appli, comPermissaoIMP, rabOrdemAlfabeticaApelid, txtEmpreCodImp, txtImpUsuarCod, cheImpApelid);
            #endregion
            #region TabMsgNt
            ALIMENTA.TabMsgNt_ImpreWORD_Ali(NomeDoArquivo, LojaLogada, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, Word_Appli);
            #endregion
            #region TabCfope
            ALIMENTA.TabCfope_ImpreWORD_Ali(NomeDoArquivo, LojaLogada, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, Word_Appli);
            #endregion
            #region TabRotas
            ALIMENTA.TabRotas_ImpreWORD_Ali(NomeDoArquivo, LojaLogada, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, Word_Appli);
            #endregion
            #region TabConve
            ALIMENTA.TabConve_ImpreWORD_Ali(NomeDoArquivo, LojaLogada, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, Word_Appli);
            #endregion
            #region TabSetor
            ALIMENTA.TabSetor_ImpreWORD_Ali(NomeDoArquivo, LojaLogada, comPermissaoIMP, comSituacaoIMP, rabOrdemAlfabetica, txtQtSelectIMP, Word_Appli);
            #endregion



            if (ALIMENTA.EncontrouInformação == "SIM")
            {
                try
                {
                    Word_Appli.ActiveDocument.SaveAs(MET.LocalSalvamento_Arquivo);
                    MessageBox.Show("RELATÓRIO " + NomeDoArquivo + " SALVO COM SUCESSO\nDIRETÓRIO DO ARQUIVO.:\n(" + MET.LocalSalvamento_Arquivo + ")", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    DialogResult AbrirArquivo = MessageBox.Show("Deseja visualizar o arquivo " + NomeDoArquivo + "?", "TechSIS Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    {
                        if (AbrirArquivo == DialogResult.Yes)
                        {
                            Word_Appli.Visible = true;
                            Word_Appli.ActiveDocument.Saved = true;
                            Word_Appli.Activate();
                            Word_Appli.WindowState = Word.WdWindowState.wdWindowStateMaximize;
                        }
                        if (AbrirArquivo == DialogResult.No)
                        {
                            Word_Appli.ActiveDocument.Saved = true;
                            Word_Appli.ActiveWindow.Close();
                        }
                    }
                }
                catch (System.Runtime.InteropServices.COMException)
                {
                    MessageBox.Show("O ARQUIVO ESTÁ EM USO. FECHE O WORD E TENTE NOVAMENTE!", "TechSIS COMException Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método ImpreWORD_GO()\n\nBLOCO = CLASSE ImpreWORD\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
            else
            {
                Word_Appli.ActiveDocument.Saved = true;
                Word_Appli.ActiveWindow.Close();
                txtQtSelectIMP.Text = string.Empty;
                MessageBox.Show("Nenhuma informação encontrada", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
