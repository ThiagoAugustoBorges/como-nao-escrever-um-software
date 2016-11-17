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
using System.Security;
using System.Security.Cryptography;
using System.Xml;

namespace TabEmpre
{
    public class TabEmpre_Gravar
    {
        public void btnALT(TextBox TXT_MESTRE, MaskedTextBox mtbCpfCnpj, TextBox txtCodigo, TextBox txtDescri, ComboBox comTipo, TextBox txtFantasia, TextBox txtResponsavel, TextBox txtEndLogradouro, TextBox txtEndNumero, TextBox txtEndCidade, TextBox txtEndCidDescri, TextBox txtEndCidUF, MaskedTextBox mtbEndCep, TextBox txtEndBairro, TextBox txtEndComple, MaskedTextBox mtbPabx, MaskedTextBox mtbFax, TextBox txtEmail, TextBox txtHomePage, TextBox txtInscricaoEstadual, TextBox txtInscricaoMunicipal, MaskedTextBox mtbVencEst, MaskedTextBox mtbVencMun, ComboBox comRegimeTrib, TextBox txtUTF8, TextBox txtUsuario, ComboBox comAtividade, ComboBox comIcmsRdz, ComboBox comIcmsST, ComboBox comTipoFaturamento, ComboBox comSitEmpresa, ComboBox comModuloSistema, TextBox txtCfop, TextBox txtSerie, TextBox txtModelo, TextBox txtMsg, TextBox txtCaixas, TextBox txtEspecie, TextBox txtAproveitamento, TextBox txtObsLivro, ComboBox comLote, ComboBox comFrete, ComboBox comCondicao, ComboBox comRecebimento, ComboBox comConsulta, ComboBox comTipoDeVenda, ComboBox comEstoque, ComboBox comFinanceiro, ComboBox comJuros, CheckBox cheEstNegativoNot, CheckBox cheEstNegativoPed, CheckBox chePedVend, CheckBox chePedComp, CheckBox cheSenhaLimite, CheckBox cheGeraFinanc, CheckBox cheSenhaDesc, CheckBox cheBloqueiaVendaInsc, CheckBox cheVendedorObrig, CheckBox cheEmiteNota, CheckBox cheAutorizaPedV, CheckBox cheAutorizaPedC, CheckBox cheAutorizaCtsPag, CheckBox cheDataAutomatica, CheckBox cheExcluiOrca, CheckBox cheExcluiHist, CheckBox cheDescAnte, CheckBox cheTabelaPrecoVista, NumericUpDown nupOrcamento, NumericUpDown nupHistorico, NumericUpDown nupAntecipado, NumericUpDown nupTabelaVista, NumericUpDown nupTabelaPrazo, NumericUpDown nupJuros, MethodInvoker ZerarCampos, MethodInvoker CamposINATIV, Button btnGravar, Button btnAvancar)
        {
            if (TXT_MESTRE.Text == "ALTERAR")
            {
                string OpçãoParaErro = "TabEmpre";
                string FazendoQue = "ALTERAÇÃO";
                string Indece = "-UPD";

                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string StringComando = "UPDATE TabEmpre SET Fantas_EMP = @Fantas,EndLog_EMP = @EndLog,EndNum_EMP = @EndNum,EndCid_EMP = @EndCid,EndCep_EMP = @EndCep,EndBai_EMP = @EndBai,EndCom_EMP = @EndCom,InsEst_EMP = @InsEst,InsMun_EMP = @InsMun,RegTri_EMP = @RegTri,Respon_EMP = @Respon,TelLoj_EMP = @TelLoj,FaxLoj_EMP = @FaxLoj,Emai01_EMP = @Emai01,HomPag_EMP = @HomPag,NumCai_EMP = @NumCai,CfoSai_EMP = @CfoSai,Series_EMP = @Series,Rodape_EMP = @Rodape,DtVeEs_EMP = @DtVeEs,DtVeMu_EMP = @DtVeMu,Ativid_EMP = @Ativid,IcmdRz_EMP = @IcmdRz,IcmsSt_EMP = @IcmsSt,TipFat_EMP = @TipFat,SitEmp_EMP = @SitEmp,ModSof_EMP = @ModSof,EspeNf_EMP = @EspeNf,AprCre_EMP = @AprCre,ObsLiv_EMP = @ObsLiv,LotePr_EMP = @LotePr,FretTp_EMP = @FretTp,CondTp_EMP = @CondTp,ReceTp_EMP = @ReceTp,ConCod_EMP = @ConCod,TpVend_EMP = @TpVend,MovEst_EMP = @MovEst,MovFin_EMP = @MovFin,TpJuro_EMP = @TpJuro,VaJuro_EMP = @VaJuro,Modelo_EMP = @Modelo,EstNot_EMP = @EstNot,EstPed_EMP = @EstPed,PeVeOb_EMP = @PeVeOb,PeCoOb_EMP = @PeCoOb,SenLim_EMP = @SenLim,FinFix_EMP = @FinFix,SenDes_EMP = @SenDes,BloIns_EMP = @BloIns,VenObr_EMP = @VenObr,EmiNot_EMP = @EmiNot,AutPev_EMP = @AutPev,AutPec_EMP = @AutPec,AutBai_EMP = @AutBai,PedDat_EMP = @PedDat,ExcOrc_EMP = @ExcOrc,OrcSem_EMP = @OrcSem,ExcHis_EMP = @ExcHis,OrcHis_EMP = @OrcHis,DesAnt_EMP = @DesAnt,ValDes_EMP = @ValDes,TabPre_EMP = @TabPre,TabPUm_EMP = @TabPUm, TabPDo_EMP = @TabPDo WHERE Sequen_EMP = @Sequen";
                SqlCommand Comando = new SqlCommand(StringComando, Conexão);
                Comando.Parameters.Add("@Sequen",SqlDbType.Int).Value = txtCodigo.Text;

                #region Parameters
                Comando.Parameters.Add("@Fantas", SqlDbType.VarChar).Value = txtFantasia.Text;
                Comando.Parameters.Add("@EndLog", SqlDbType.VarChar).Value = txtEndLogradouro.Text;
                Comando.Parameters.Add("@EndNum", SqlDbType.VarChar).Value = txtEndNumero.Text;
                Comando.Parameters.Add("@EndCid", SqlDbType.Int).Value = txtEndCidade.Text;
                Comando.Parameters.Add("@EndCep", SqlDbType.VarChar).Value = mtbEndCep.Text;
                Comando.Parameters.Add("@EndBai", SqlDbType.VarChar).Value = txtEndBairro.Text;
                Comando.Parameters.Add("@EndCom", SqlDbType.VarChar).Value = txtEndComple.Text;
                Comando.Parameters.Add("@InsEst", SqlDbType.VarChar).Value = txtInscricaoEstadual.Text;
                Comando.Parameters.Add("@InsMun", SqlDbType.VarChar).Value = txtInscricaoMunicipal.Text;
                Comando.Parameters.Add("@RegTri", SqlDbType.Int).Value = comRegimeTrib.SelectedIndex;
                Comando.Parameters.Add("@Respon", SqlDbType.VarChar).Value = txtResponsavel.Text;
                Comando.Parameters.Add("@TelLoj", SqlDbType.VarChar).Value = mtbPabx.Text;
                Comando.Parameters.Add("@FaxLoj", SqlDbType.VarChar).Value = mtbFax.Text;
                Comando.Parameters.Add("@Emai01", SqlDbType.VarChar).Value = txtEmail.Text;
                Comando.Parameters.Add("@HomPag", SqlDbType.VarChar).Value = txtHomePage.Text;
                #region Comando.Parameters.Add("@NumCai", SqlDbType.Int).Value = txtCaixas.Text;
                if (txtCaixas.Text != string.Empty)
                {
                    Comando.Parameters.Add("@NumCai", SqlDbType.Int).Value = txtCaixas.Text;
                }
                else
                {
                    Comando.Parameters.Add("@NumCai", SqlDbType.Int).Value = 0;
                }
                #endregion
                #region Comando.Parameters.Add("@CfoSai", SqlDbType.Int).Value = txtCfop.Text;
                if (txtCfop.Text != string.Empty)
                {
                    Comando.Parameters.Add("@CfoSai", SqlDbType.Int).Value = txtCfop.Text;
                }
                else
                {
                    Comando.Parameters.Add("@CfoSai", SqlDbType.Int).Value = 0;
                }
                #endregion
                #region Comando.Parameters.Add("@Series", SqlDbType.Int).Value = txtSerie.Text;
                if (txtSerie.Text != string.Empty)
                {
                    Comando.Parameters.Add("@Series", SqlDbType.Int).Value = txtSerie.Text;
                }
                else
                {
                    Comando.Parameters.Add("@Series", SqlDbType.Int).Value = 0;
                }
                #endregion
                #region Comando.Parameters.Add("@Rodape", SqlDbType.VarChar).Value = txtMsg.Text;
                if (txtMsg.Text != string.Empty)
                {
                    Comando.Parameters.Add("@Rodape", SqlDbType.Int).Value = txtMsg.Text;
                }
                else
                {
                    Comando.Parameters.Add("@Rodape", SqlDbType.Int).Value = 0;
                }
                #endregion
                #region Comando.Parameters.Add("@DtVeEs", SqlDbType.Date).Value = mtbVencEst.Text;
                if (char.IsLetterOrDigit(mtbVencEst.Text[0]))
                {
                    Comando.Parameters.Add("@DtVeEs", SqlDbType.Date).Value = mtbVencEst.Text;
                }
                else
                {
                    Comando.Parameters.Add("@DtVeEs", SqlDbType.Date).Value = DateTime.MinValue.ToString("dd/MM/yyyy");
                }
                #endregion
                #region Comando.Parameters.Add("@DtVeMu", SqlDbType.Date).Value = mtbVencMun.Text;
                if (char.IsLetterOrDigit(mtbVencMun.Text[0]))
                {
                    Comando.Parameters.Add("@DtVeMu", SqlDbType.Date).Value = mtbVencMun.Text;
                }
                else
                {
                    Comando.Parameters.Add("@DtVeMu", SqlDbType.Date).Value = DateTime.MinValue.ToString("dd/MM/yyyy");
                }
                #endregion
                Comando.Parameters.Add("@Ativid", SqlDbType.Int).Value = comAtividade.SelectedIndex;
                Comando.Parameters.Add("@IcmsSt", SqlDbType.Int).Value = comIcmsST.SelectedIndex;
                Comando.Parameters.Add("@IcmdRz", SqlDbType.Int).Value = comIcmsRdz.SelectedIndex;
                Comando.Parameters.Add("@TipFat", SqlDbType.Int).Value = comTipoFaturamento.SelectedIndex;
                Comando.Parameters.Add("@SitEmp", SqlDbType.Int).Value = comSitEmpresa.SelectedIndex;
                Comando.Parameters.Add("@ModSof", SqlDbType.Int).Value = comModuloSistema.SelectedIndex;
                Comando.Parameters.Add("@EspeNf", SqlDbType.VarChar).Value = txtEspecie.Text;
                Comando.Parameters.Add("@AprCre", SqlDbType.Decimal).Value = txtAproveitamento.Text;
                Comando.Parameters.Add("@ObsLiv", SqlDbType.VarChar).Value = txtObsLivro.Text;
                Comando.Parameters.Add("@LotePr", SqlDbType.Int).Value = comLote.SelectedIndex;
                Comando.Parameters.Add("@FretTp", SqlDbType.Int).Value = comFrete.SelectedIndex;
                Comando.Parameters.Add("@CondTp", SqlDbType.Int).Value = comCondicao.SelectedIndex;
                Comando.Parameters.Add("@ReceTp", SqlDbType.Int).Value = comRecebimento.SelectedIndex;
                Comando.Parameters.Add("@ConCod", SqlDbType.Int).Value = comConsulta.SelectedIndex;
                Comando.Parameters.Add("@TpVend", SqlDbType.Int).Value = comTipoDeVenda.SelectedIndex;
                Comando.Parameters.Add("@MovEst", SqlDbType.Int).Value = comEstoque.SelectedIndex;
                Comando.Parameters.Add("@MovFin", SqlDbType.Int).Value = comFinanceiro.SelectedIndex;
                Comando.Parameters.Add("@TpJuro", SqlDbType.Int).Value = comJuros.SelectedIndex;
                Comando.Parameters.Add("@VaJuro", SqlDbType.Decimal).Value = nupJuros.Value;
                Comando.Parameters.Add("@Modelo", SqlDbType.VarChar).Value = txtModelo.Text;
                //Comaça os CheckBoxs

                Comando.Parameters.Add("@EstNot", SqlDbType.VarChar).Value = cheEstNegativoNot.Checked;
                Comando.Parameters.Add("@EstPed", SqlDbType.VarChar).Value = cheEstNegativoPed.Checked;
                Comando.Parameters.Add("@PeVeOb", SqlDbType.VarChar).Value = chePedVend.Checked;
                Comando.Parameters.Add("@PeCoOb", SqlDbType.VarChar).Value = chePedComp.Checked;
                Comando.Parameters.Add("@SenLim", SqlDbType.VarChar).Value = cheSenhaLimite.Checked;
                Comando.Parameters.Add("@FinFix", SqlDbType.VarChar).Value = cheGeraFinanc.Checked;
                Comando.Parameters.Add("@SenDes", SqlDbType.VarChar).Value = cheSenhaDesc.Checked;
                Comando.Parameters.Add("@BloIns", SqlDbType.VarChar).Value = cheBloqueiaVendaInsc.Checked;
                Comando.Parameters.Add("@VenObr", SqlDbType.VarChar).Value = cheVendedorObrig.Checked;
                Comando.Parameters.Add("@EmiNot", SqlDbType.VarChar).Value = cheEmiteNota.Checked;
                Comando.Parameters.Add("@AutPev", SqlDbType.VarChar).Value = cheAutorizaPedV.Checked;
                Comando.Parameters.Add("@AutPec", SqlDbType.VarChar).Value = cheAutorizaPedC.Checked;
                Comando.Parameters.Add("@AutBai", SqlDbType.VarChar).Value = cheAutorizaCtsPag.Checked;
                Comando.Parameters.Add("@PedDat", SqlDbType.VarChar).Value = cheDataAutomatica.Checked;
                Comando.Parameters.Add("@ExcOrc", SqlDbType.VarChar).Value = cheExcluiOrca.Checked;
                Comando.Parameters.Add("@ExcHis", SqlDbType.VarChar).Value = cheExcluiHist.Checked;
                Comando.Parameters.Add("@DesAnt", SqlDbType.VarChar).Value = cheDescAnte.Checked;
                Comando.Parameters.Add("@TabPre", SqlDbType.VarChar).Value = cheTabelaPrecoVista.Checked;

                Comando.Parameters.Add("@OrcSem", SqlDbType.Int).Value = nupOrcamento.Value;
                Comando.Parameters.Add("@OrcHis", SqlDbType.Int).Value = nupHistorico.Value;
                Comando.Parameters.Add("@ValDes", SqlDbType.Decimal).Value = nupAntecipado.Value;
                Comando.Parameters.Add("@TabPUm", SqlDbType.Int).Value = nupTabelaVista.Value;
                Comando.Parameters.Add("@TabPDo", SqlDbType.Int).Value = nupTabelaPrazo.Value;
                #endregion

                try
                {
                    Comando.ExecuteNonQuery();
                    MessageBox.Show("Dados alterados com sucesso", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ZerarCampos();
                    CamposINATIV();
                    btnAvancar.Enabled = false; btnGravar.Enabled = false;
                    txtCodigo.Select(); txtCodigo.SelectAll();
                }


                catch (SqlException Ex)
                {
                    #region ErrosSQL
                    //Forening Key
                    if (Ex.Number == 547)
                    {
                        MessageBox.Show("Erro.: Verifique se Código da Cidade está informado corretamente.\n\nO Sistema está tentando inserir registros que não existe no banco de dados (Erro FOREIGN KEY).\nCaso erro continue contate suporte técnico.", "TechSIS BWK Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    #endregion
                    else
                    {
                        DialogResult Erro = MessageBox.Show("Erro na " + FazendoQue + " de Dados (" + OpçãoParaErro + Indece + ")", "TechSIS BWK Erro.: " + Ex.Number, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        if (Erro == DialogResult.Cancel)
                        {
                            MessageBox.Show("DETALHAMENTO DO ERRO.: " + OpçãoParaErro + " " + FazendoQue + "\n\n" + Ex.Message, "TechSIS BWK Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    DialogResult Erro = MessageBox.Show("Erro na " + FazendoQue + " de Dados (" + OpçãoParaErro + Indece +")", "TechSIS BWK Erro.: Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (Erro == DialogResult.Cancel)
                    {
                        MessageBox.Show("DETALHAMENTO DO ERRO.: " + OpçãoParaErro + " " + FazendoQue + "\n\n" + Ex.Message, "TechSIS BWK Erro.: Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }
        public void btnINC(TextBox TXT_MESTRE,MethodInvoker ZerarCampos)
        {
            try
            {
                if (File.Exists("TechSIS_AddLoja.exe"))
                {
                    System.Diagnostics.Process.Start("TechSIS_AddLoja.exe");
                    ZerarCampos();
                }
                else
                {
                    MessageBox.Show("Executável (TechSIS_AddLoja.exe) não foi encontrado na pasta Debug", "TechSIS BWK Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
    }
}