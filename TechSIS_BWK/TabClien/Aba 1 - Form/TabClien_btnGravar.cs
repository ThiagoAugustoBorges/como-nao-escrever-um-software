using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Security;
using System.Security.Cryptography;

namespace TabClien
{
    internal class TabClien_btnGravar
    {
        public void GravarINC(TextBox txtCodigo, TextBox txtMESTRE, Panel panUpAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, MethodInvoker CamposEnable, Button btnGravar, Button btnInfFinaShow, Button btnInfComerShow, Button btnAvancar, Button btnGravarAb2, Button btnVoltar, TextBox txtDescri, TextBox txtCodigoPrin, ComboBox comTipoPFPJ, MaskedTextBox mtbCpfCnpj, ComboBox comStatus, TextBox txtFantasia, MaskedTextBox mtbTelPabx, MaskedTextBox mtbTelCel1, ComboBox comCategoria, TextBox txtObservacao, MaskedTextBox mtbTelFax, MaskedTextBox mtbTelCel2, TextBox txtInscricaoEstadual, MaskedTextBox mtbVencEst, TextBox txtRotaCod, TextBox txtVendedorCod, TextBox txtVendedorDesc, TextBox txtDesconto, TextBox txtBanco, TextBox txtAgencia, TextBox txtInscricaoMunicipal, MaskedTextBox mtbVencMun, TextBox txtReferencialCod, TextBox txtEmpresaCod, TextBox txtEmpresaDesc, TextBox txtRgIdentidade, TextBox txtConceito, TextBox txtEndLogradouroFATU, TextBox txtEndNumeroFATU, TextBox txtEndCidadeFATU, TextBox txtEndCidDescriFATU, TextBox txtEndCidUFFATU, MaskedTextBox mtbEndCepFATU, TextBox txtEndBairroFATU, TextBox txtEndCompleFATU, TextBox txtLabel, TextBox txtEndLogradouroPERSO, TextBox txtEndNumeroPERSO, TextBox txtEndCidadePERSO, TextBox txtEndCidDescriPERSO, TextBox txtEndCidUFPERSO, MaskedTextBox mtbEndCepPERSO, TextBox txtEndBairroPERSO, TextBox txtEndComplePERSO, TextBox txtTransportadoraCod, TextBox txtTransportadoraDesc, TextBox txtConvenioCod, TextBox txtConvenioDesc, TextBox txtValorLimiteCre, MaskedTextBox mtbDataVenciLimite, ComboBox comSituacaoCredito, ComboBox comFrete, ComboBox comRecebimento, ComboBox comCondicao, ComboBox comTipoDeVenda, ComboBox comContratoEmpresa, MaskedTextBox mtbContraInicio, MaskedTextBox mtbContraFim, TextBox txtCodigoAb2, TextBox txtDescriAb2, MaskedTextBox mtbCpfCnpjAb2, TextBox txtEmailContato1, TextBox txtEmailContato2, TextBox txtSkype, TextBox txtMsn, TextBox txtHomePage, TextBox txtFacebook, ComboBox comSexo, TextBox txtPfLocalDeTrabalho, TextBox txtPfNaturalDe, TextBox txtPfCargo, MaskedTextBox mtbPfAdmissao, MaskedTextBox mtbPfAniversario, TextBox txtPfFiliacaoPai, TextBox txtPfFiliacaoMae, TextBox txtPfConjuge, MaskedTextBox mtbPfCpfConjuge, TextBox txtPjContatoProp1, TextBox txtPjContatoProp2, TextBox txtPjEscritorioContabilidade, TextBox txtPjDiaFatu, ComboBox comPjAtividade, ComboBox comPjRegimeTrib, TextBox txtPjCnae, ComboBox comIcmsRdz, ComboBox comIcmsST, TextBox txtCfop, TextBox txtCfopDesc, TextBox txtMsg1, TextBox txtMsg2, MaskedTextBox mtbDataCadastro, MaskedTextBox mtbRevisao, string LojaLogada, string LojaLogadaDesc, TextBox txtUsuario, TextBox txtRotaSequen)
        {
            if (txtMESTRE.Text == "INCLUIR")
            {
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

                //Pega o Ultimo registro da Tabela de clientes
                string PegarULTIMORegCLI = "SELECT MAX (Sequen_CLI + 1) FROM TabClien";
                SqlCommand PegarCLI = new SqlCommand(PegarULTIMORegCLI, Conexão);
                SqlDataReader DrCLI = PegarCLI.ExecuteReader(); DrCLI.Read();
                string _SequenCLI = DrCLI[0].ToString();
                if (String.IsNullOrEmpty(_SequenCLI))
                {
                    _SequenCLI = "1";
                }
                DrCLI.Close();

                //Define a string e o comando
                string StringComandoINCLUIR_TABE = "INSERT INTO TabClien VALUES (@Sequen_CLI,@Descri_CLI,@SeqPri_CLI,@Tipo01_CLI,@CpfCnp_CLI,@Fantas_CLI,@TelPab_CLI,@TelFax_CLI,@TelCe1_CLI,@TelCe2_CLI,@Catego_CLI,@Observ_CLI,@EndLo1_CLI,@EndNu1_CLI,@EndCi1_CLI,@EndCe1_CLI,@EndBa1_CLI,@EndCo1_CLI,@EndTi2_CLI,@EndLo2_CLI,@EndNu2_CLI,@EndCi2_CLI,@EndCe2_CLI,@EndBa2_CLI,@EndCo2_CLI,@TraSeq_CLI,@ConSeq_CLI,@InsEst_CLI,@InsMun_CLI,@DtVeEs_CLI,@DtVeMu_CLI,@RotSeq_CLI,@Refere_CLI,@VenSeq_CLI,@EmpSeq_CLI,@Descon_CLI,@BancCl_CLI,@AgenCl_CLI,@Identi_CLI,@Concet_CLI,@ValLim_CLI,@VenLim_CLI,@SitCre_CLI,@ComFre_CLI,@ComRec_CLI,@ComCom_CLI,@ComTip_CLI,@Contra_CLI,@DtInic_CLI,@DtFina_CLI,@Emai01_CLI,@Emai02_CLI,@MsnCon_CLI,@Skype1_CLI,@HomPag_CLI,@Facebo_CLI,@SexoCl_CLI,@LocaTr_CLI,@Natura_CLI,@CargCl_CLI,@FilPai_CLI,@FilMae_CLI,@Conjug_CLI,@CpfCon_CLI,@DtAdmi_CLI,@DtAniv_CLI,@Conta1_CLI,@Conta2_CLI,@EscCon_CLI,@DiaFat_CLI,@AtivEm_CLI,@RegiEm_CLI,@CnaeEm_CLI,@IcmsBc_CLI,@IcmsSu_CLI,@CfopCl_CLI,@MsgNo1_CLI,@MsgNo2_CLI,@UsuSeq_CLI,@DtCada_CLI,@DtRevi_CLI,@Status_CLI)";
                SqlCommand ComandoINCLUIR_TABE = new SqlCommand(StringComandoINCLUIR_TABE, Conexão);
                string StringComandoINCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'020100','INCLUSÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                SqlCommand ComandoINCLUIR_HIST = new SqlCommand(StringComandoINCLUIR_HIST, Conexão);
                string StringComandoROTA_TABE = "INSERT INTO TabRot01 (Sequen_RO1,SeqCli_RO1,Ordems_RO1,Status_RO1) VALUES (@Sequen_RO1,@SeqCli_RO1,@Ordems_RO1,@Status_RO1)";
                SqlCommand ComandoROTA_TABE = new SqlCommand(StringComandoROTA_TABE, Conexão);

                #region PARAMETERS TABELA
                ComandoINCLUIR_TABE.Parameters.Add("@Sequen_CLI", SqlDbType.Int).Value = _SequenCLI;
                ComandoINCLUIR_TABE.Parameters.Add("@Descri_CLI", SqlDbType.VarChar).Value = txtDescri.Text;
                #region ComandoINCLUIR_TABE.Parameters.Add("@SeqPri_CLI", SqlDbType.Int).Value = txtCodigoPrin.Text;
                if (Convert.ToInt32(txtCodigo.Text) == Convert.ToInt32(txtCodigoPrin.Text))
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@SeqPri_CLI", SqlDbType.Int).Value = _SequenCLI;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@SeqPri_CLI", SqlDbType.Int).Value = txtCodigoPrin.Text;
                }
                #endregion
                ComandoINCLUIR_TABE.Parameters.Add("@Tipo01_CLI", SqlDbType.Int).Value = comTipoPFPJ.SelectedIndex;
                ComandoINCLUIR_TABE.Parameters.Add("@CpfCnp_CLI", SqlDbType.VarChar).Value = mtbCpfCnpj.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@Fantas_CLI", SqlDbType.VarChar).Value = txtFantasia.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@TelPab_CLI", SqlDbType.VarChar).Value = mtbTelPabx.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@TelFax_CLI", SqlDbType.VarChar).Value = mtbTelFax.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@TelCe1_CLI", SqlDbType.VarChar).Value = mtbTelCel1.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@TelCe2_CLI", SqlDbType.VarChar).Value = mtbTelCel2.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@Catego_CLI", SqlDbType.Int).Value = comCategoria.SelectedIndex;
                ComandoINCLUIR_TABE.Parameters.Add("@Observ_CLI", SqlDbType.VarChar).Value = txtObservacao.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@EndLo1_CLI", SqlDbType.VarChar).Value = txtEndLogradouroFATU.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@EndNu1_CLI", SqlDbType.VarChar).Value = txtEndNumeroFATU.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@EndCi1_CLI", SqlDbType.Int).Value = txtEndCidadeFATU.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@EndCe1_CLI", SqlDbType.VarChar).Value = mtbEndCepFATU.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@EndBa1_CLI", SqlDbType.VarChar).Value = txtEndBairroFATU.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@EndCo1_CLI", SqlDbType.VarChar).Value = txtEndCompleFATU.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@EndTi2_CLI", SqlDbType.VarChar).Value = txtLabel.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@EndLo2_CLI", SqlDbType.VarChar).Value = txtEndLogradouroPERSO.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@EndNu2_CLI", SqlDbType.VarChar).Value = txtEndNumeroPERSO.Text;
                #region ComandoINCLUIR_TABE.Parameters.Add("@EndCi2_CLI", SqlDbType.Int).Value = txtEndCidadePERSO.Text;
                if (txtEndCidadePERSO.Text == string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@EndCi2_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@EndCi2_CLI", SqlDbType.Int).Value = txtEndCidadePERSO.Text;
                }
                #endregion
                ComandoINCLUIR_TABE.Parameters.Add("@EndCe2_CLI", SqlDbType.VarChar).Value = mtbEndCepPERSO.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@EndBa2_CLI", SqlDbType.VarChar).Value = txtEndBairroPERSO.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@EndCo2_CLI", SqlDbType.VarChar).Value = txtEndComplePERSO.Text;
                #region ComandoINCLUIR_TABE.Parameters.Add("@TraSeq_CLI", SqlDbType.Int).Value = txtTransportadoraCod.Text;
                if (txtTransportadoraCod.Text == string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@TraSeq_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@TraSeq_CLI", SqlDbType.Int).Value = txtTransportadoraCod.Text;
                }
                #endregion
                #region ComandoINCLUIR_TABE.Parameters.Add("@ConSeq_CLI", SqlDbType.Int).Value = txtConvenioCod.Text;
                if (txtConvenioCod.Text == string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@ConSeq_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@ConSeq_CLI", SqlDbType.Int).Value = txtConvenioCod.Text;
                }
                #endregion
                ComandoINCLUIR_TABE.Parameters.Add("@InsEst_CLI", SqlDbType.VarChar).Value = txtInscricaoEstadual.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@InsMun_CLI", SqlDbType.VarChar).Value = txtInscricaoMunicipal.Text;
                #region ComandoINCLUIR_TABE.Parameters.Add("@DtVeEs_CLI", SqlDbType.Date).Value = mtbVencEst.Text;
                if (mtbVencEst.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@DtVeEs_CLI", SqlDbType.Date).Value = DateTime.MinValue;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@DtVeEs_CLI", SqlDbType.Date).Value = mtbVencEst.Text;
                }
                #endregion
                #region ComandoINCLUIR_TABE.Parameters.Add("@DtVeMu_CLI", SqlDbType.Date).Value = mtbVencMun.Text;
                if (mtbVencMun.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@DtVeMu_CLI", SqlDbType.Date).Value = DateTime.MinValue;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@DtVeMu_CLI", SqlDbType.Date).Value = mtbVencMun.Text;
                }
                #endregion
                #region ComandoINCLUIR_TABE.Parameters.Add("@RotSeq_CLI", SqlDbType.Int).Value = txtRotaCod.Text;
                if (txtRotaCod.Text == string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@RotSeq_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@RotSeq_CLI", SqlDbType.Int).Value = txtRotaCod.Text;
                }
                #endregion
                #region ComandoINCLUIR_TABE.Parameters.Add("@Refere_CLI", SqlDbType.Int).Value = txtReferencialCod.Text;
                if (txtReferencialCod.Text == string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@Refere_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@Refere_CLI", SqlDbType.Int).Value = txtReferencialCod.Text;
                }
                #endregion
                #region ComandoINCLUIR_TABE.Parameters.Add("@VenSeq_CLI", SqlDbType.Int).Value = txtVendedorCod.Text;
                if (txtVendedorCod.Text == string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@VenSeq_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@VenSeq_CLI", SqlDbType.Int).Value = txtVendedorCod.Text;
                }
                #endregion
                ComandoINCLUIR_TABE.Parameters.Add("@EmpSeq_CLI", SqlDbType.Int).Value = txtEmpresaCod.Text;
                #region ComandoINCLUIR_TABE.Parameters.Add("@Descon_CLI", SqlDbType.Decimal).Value = txtDesconto.Text;
                if (txtDesconto.Text == string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@Descon_CLI", SqlDbType.Decimal).Value = 0;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@Descon_CLI", SqlDbType.Decimal).Value = txtDesconto.Text;
                }
                #endregion
                #region ComandoINCLUIR_TABE.Parameters.Add("@BancCl_CLI", SqlDbType.Int).Value = txtBanco.Text;
                if (txtBanco.Text == string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@BancCl_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@BancCl_CLI", SqlDbType.Int).Value = txtBanco.Text;
                }
                #endregion
                #region ComandoINCLUIR_TABE.Parameters.Add("@AgenCl_CLI", SqlDbType.Int).Value = txtAgencia.Text;
                if (txtAgencia.Text == string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@AgenCl_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@AgenCl_CLI", SqlDbType.Int).Value = txtAgencia.Text;
                }
                #endregion
                ComandoINCLUIR_TABE.Parameters.Add("@Identi_CLI", SqlDbType.VarChar).Value = txtRgIdentidade.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@Concet_CLI", SqlDbType.VarChar).Value = txtConceito.Text;
                #region ComandoINCLUIR_TABE.Parameters.Add("@ValLim_CLI", SqlDbType.Decimal).Value = txtValorLimiteCre.Text;
                if (txtValorLimiteCre.Text == string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@ValLim_CLI", SqlDbType.Decimal).Value = 0;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@ValLim_CLI", SqlDbType.Decimal).Value = txtValorLimiteCre.Text;
                }
                #endregion
                #region ComandoINCLUIR_TABE.Parameters.Add("@VenLim_CLI", SqlDbType.Date).Value = mtbDataVenciLimite.Text;
                if (mtbDataVenciLimite.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@VenLim_CLI", SqlDbType.Date).Value = DateTime.MinValue;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@VenLim_CLI", SqlDbType.Date).Value = mtbDataVenciLimite.Text;
                }
                #endregion
                ComandoINCLUIR_TABE.Parameters.Add("@SitCre_CLI", SqlDbType.Int).Value = comSituacaoCredito.SelectedIndex;
                ComandoINCLUIR_TABE.Parameters.Add("@ComFre_CLI", SqlDbType.Int).Value = comFrete.SelectedIndex;
                ComandoINCLUIR_TABE.Parameters.Add("@ComRec_CLI", SqlDbType.Int).Value = comRecebimento.SelectedIndex;
                ComandoINCLUIR_TABE.Parameters.Add("@ComCom_CLI", SqlDbType.Int).Value = comCondicao.SelectedIndex;
                ComandoINCLUIR_TABE.Parameters.Add("@ComTip_CLI", SqlDbType.Int).Value = comTipoDeVenda.SelectedIndex;
                ComandoINCLUIR_TABE.Parameters.Add("@Contra_CLI", SqlDbType.Int).Value = comContratoEmpresa.SelectedIndex;
                #region ComandoINCLUIR_TABE.Parameters.Add("@DtInic_CLI", SqlDbType.Date).Value = mtbContraInicio.Text;
                if (mtbContraInicio.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@DtInic_CLI", SqlDbType.Date).Value = DateTime.MinValue;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@DtInic_CLI", SqlDbType.Date).Value = mtbContraInicio.Text;
                }
                #endregion
                #region ComandoINCLUIR_TABE.Parameters.Add("@DtFina_CLI", SqlDbType.Date).Value = mtbContraFim.Text;
                if (mtbContraFim.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@DtFina_CLI", SqlDbType.Date).Value = DateTime.MinValue;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@DtFina_CLI", SqlDbType.Date).Value = mtbContraFim.Text;
                }
                #endregion
                ComandoINCLUIR_TABE.Parameters.Add("@Emai01_CLI", SqlDbType.VarChar).Value = txtEmailContato1.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@Emai02_CLI", SqlDbType.VarChar).Value = txtEmailContato2.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@MsnCon_CLI", SqlDbType.VarChar).Value = txtMsn.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@Skype1_CLI", SqlDbType.VarChar).Value = txtSkype.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@HomPag_CLI", SqlDbType.VarChar).Value = txtHomePage.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@Facebo_CLI", SqlDbType.VarChar).Value = txtFacebook.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@SexoCl_CLI", SqlDbType.Int).Value = comSexo.SelectedIndex;
                ComandoINCLUIR_TABE.Parameters.Add("@LocaTr_CLI", SqlDbType.VarChar).Value = txtPfLocalDeTrabalho.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@Natura_CLI", SqlDbType.VarChar).Value = txtPfNaturalDe.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@CargCl_CLI", SqlDbType.VarChar).Value = txtPfCargo.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@FilPai_CLI", SqlDbType.VarChar).Value = txtPfFiliacaoPai.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@FilMae_CLI", SqlDbType.VarChar).Value = txtPfFiliacaoMae.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@Conjug_CLI", SqlDbType.VarChar).Value = txtPfConjuge.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@CpfCon_CLI", SqlDbType.VarChar).Value = mtbPfCpfConjuge.Text;
                #region ComandoINCLUIR_TABE.Parameters.Add("@DtAdmi_CLI", SqlDbType.Date).Value = mtbPfAdmissao.Text;
                if (mtbPfAdmissao.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@DtAdmi_CLI", SqlDbType.Date).Value = DateTime.MinValue;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@DtAdmi_CLI", SqlDbType.Date).Value = mtbPfAdmissao.Text;
                }
                #endregion
                #region ComandoINCLUIR_TABE.Parameters.Add("@DtAniv_CLI", SqlDbType.Date).Value = mtbPfAniversario.Text;
                if (mtbPfAniversario.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@DtAniv_CLI", SqlDbType.Date).Value = DateTime.MinValue;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@DtAniv_CLI", SqlDbType.Date).Value = mtbPfAniversario.Text;
                }
                #endregion
                ComandoINCLUIR_TABE.Parameters.Add("@Conta1_CLI", SqlDbType.VarChar).Value = txtPjContatoProp1.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@Conta2_CLI", SqlDbType.VarChar).Value = txtPjContatoProp2.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@EscCon_CLI", SqlDbType.VarChar).Value = txtPjEscritorioContabilidade.Text;
                #region ComandoINCLUIR_TABE.Parameters.Add("@DiaFat_CLI", SqlDbType.Int).Value = txtPjDiaFatu.Text;
                if (txtPjDiaFatu.Text == string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@DiaFat_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@DiaFat_CLI", SqlDbType.Int).Value = txtPjDiaFatu.Text;
                }
                #endregion
                ComandoINCLUIR_TABE.Parameters.Add("@AtivEm_CLI", SqlDbType.Int).Value = comPjAtividade.SelectedIndex;
                ComandoINCLUIR_TABE.Parameters.Add("@RegiEm_CLI", SqlDbType.Int).Value = comPjRegimeTrib.SelectedIndex;
                ComandoINCLUIR_TABE.Parameters.Add("@CnaeEm_CLI", SqlDbType.VarChar).Value = txtPjCnae.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@IcmsBc_CLI", SqlDbType.Int).Value = comIcmsRdz.SelectedIndex;
                ComandoINCLUIR_TABE.Parameters.Add("@IcmsSu_CLI", SqlDbType.Int).Value = comIcmsST.SelectedIndex;
                #region ComandoINCLUIR_TABE.Parameters.Add("@CfopCl_CLI", SqlDbType.Int).Value = txtCfop.Text;
                if (txtCfop.Text == string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@CfopCl_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@CfopCl_CLI", SqlDbType.Int).Value = txtCfop.Text;
                }
                #endregion
                #region ComandoINCLUIR_TABE.Parameters.Add("@MsgNo1_CLI", SqlDbType.Int).Value = txtMsg1.Text;
                if (txtMsg1.Text == string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@MsgNo1_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@MsgNo1_CLI", SqlDbType.Int).Value = txtMsg1.Text;
                }
                #endregion
                #region ComandoINCLUIR_TABE.Parameters.Add("@MsgNo2_CLI", SqlDbType.Int).Value = txtMsg2.Text;
                if (txtMsg2.Text == string.Empty)
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@MsgNo2_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoINCLUIR_TABE.Parameters.Add("@MsgNo2_CLI", SqlDbType.Int).Value = txtMsg2.Text;
                }
                #endregion
                ComandoINCLUIR_TABE.Parameters.Add("@UsuSeq_CLI", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@DtCada_CLI", SqlDbType.DateTime).Value = mtbDataCadastro.Text;
                ComandoINCLUIR_TABE.Parameters.Add("@DtRevi_CLI", SqlDbType.DateTime).Value = mtbRevisao.Text;
                #region ComandoINCLUIR_TABE.Parameters.Add("@Status_CLI", SqlDbType.Int).Value = STATUS;
                int STATUS = comStatus.SelectedIndex;
                if (STATUS == 3)
                {
                    STATUS = 4;
                }
                ComandoINCLUIR_TABE.Parameters.Add("@Status_CLI", SqlDbType.Int).Value = STATUS;
                #endregion
                #endregion
                #region PARAMETERS HISTÓRICO
                //Parametros do Insert no Historico
                ComandoINCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "INCLUSÃO DO CLIENTE....: " + _SequenCLI.ToString().PadLeft(6, '0');
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = txtDescri.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();
                #endregion
                #region PARAMETERS ROTA
                #region ComandoROTA_TABE.Parameters.Add("@Sequen_RO1", SqlDbType.Int).Value = txtRotaCod.Text;
                if (txtRotaCod.Text == string.Empty)
                {
                    ComandoROTA_TABE.Parameters.Add("@Sequen_RO1", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoROTA_TABE.Parameters.Add("@Sequen_RO1", SqlDbType.Int).Value = txtRotaCod.Text;
                }
                #endregion
                #region ComandoROTA_TABE.Parameters.Add("@SeqCli_RO1", SqlDbType.Int).Value = _SequenCLI;
                if (Convert.ToInt32(_SequenCLI) <= 0)
                {
                    ComandoROTA_TABE.Parameters.Add("@SeqCli_RO1", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoROTA_TABE.Parameters.Add("@SeqCli_RO1", SqlDbType.Int).Value = _SequenCLI;
                }
                #endregion
                #region ComandoROTA_TABE.Parameters.Add("@Ordems_RO1", SqlDbType.Int).Value = txtRotaSequen.Text;
                if (txtRotaSequen.Text == string.Empty)
                {
                    ComandoROTA_TABE.Parameters.Add("@Ordems_RO1", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoROTA_TABE.Parameters.Add("@Ordems_RO1", SqlDbType.Int).Value = txtRotaSequen.Text;
                }
                #endregion
                ComandoROTA_TABE.Parameters.Add("@Status_RO1", SqlDbType.Int).Value = 1;
                #endregion

                try
                {
                    
                    ComandoINCLUIR_TABE.ExecuteNonQuery();
                    ComandoINCLUIR_HIST.ExecuteNonQuery();
                    //INSERE A ROTA CASO SEJA INFORMADA
                    if (txtRotaCod.Text != string.Empty)
                    {
                        ComandoROTA_TABE.ExecuteNonQuery();
                    }

                    MessageBox.Show("Dados inseridos no banco de dados com sucesso!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Seleciona ultimo registro +1
                    TabClien_MET MET = new TabClien_MET();
                    MET.MET_SelecionaUltimoRegistroMaisUm(txtCodigo, btnGravar);


                    ZerarCampos();
                    txtCodigo.Select(); txtCodigo.SelectAll();
                    btnInfFinaShow.Enabled = false;
                    btnInfComerShow.Enabled = false;
                    btnAvancar.Enabled = false;
                    btnGravar.Enabled = false;
                    btnGravarAb2.Enabled = false;
                    btnVoltar.Enabled = false;
                    CamposDisable();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarINC()\n\nBLOCO = INCLUSÃO DE DADOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarINC()\n\nBLOCO = INCLUSÃO DE DADOS\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }
        public void GravarALT(TextBox txtCodigo, TextBox txtMESTRE, Panel panUpAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, MethodInvoker CamposEnable, Button btnGravar, Button btnInfFinaShow, Button btnInfComerShow, Button btnAvancar, Button btnGravarAb2, Button btnVoltar, TextBox txtDescri, TextBox txtCodigoPrin, ComboBox comTipoPFPJ, MaskedTextBox mtbCpfCnpj, ComboBox comStatus, TextBox txtFantasia, MaskedTextBox mtbTelPabx, MaskedTextBox mtbTelCel1, ComboBox comCategoria, TextBox txtObservacao, MaskedTextBox mtbTelFax, MaskedTextBox mtbTelCel2, TextBox txtInscricaoEstadual, MaskedTextBox mtbVencEst, TextBox txtRotaCod, TextBox txtVendedorCod, TextBox txtVendedorDesc, TextBox txtDesconto, TextBox txtBanco, TextBox txtAgencia, TextBox txtInscricaoMunicipal, MaskedTextBox mtbVencMun, TextBox txtReferencialCod, TextBox txtEmpresaCod, TextBox txtEmpresaDesc, TextBox txtRgIdentidade, TextBox txtConceito, TextBox txtEndLogradouroFATU, TextBox txtEndNumeroFATU, TextBox txtEndCidadeFATU, TextBox txtEndCidDescriFATU, TextBox txtEndCidUFFATU, MaskedTextBox mtbEndCepFATU, TextBox txtEndBairroFATU, TextBox txtEndCompleFATU, TextBox txtLabel, TextBox txtEndLogradouroPERSO, TextBox txtEndNumeroPERSO, TextBox txtEndCidadePERSO, TextBox txtEndCidDescriPERSO, TextBox txtEndCidUFPERSO, MaskedTextBox mtbEndCepPERSO, TextBox txtEndBairroPERSO, TextBox txtEndComplePERSO, TextBox txtTransportadoraCod, TextBox txtTransportadoraDesc, TextBox txtConvenioCod, TextBox txtConvenioDesc, TextBox txtValorLimiteCre, MaskedTextBox mtbDataVenciLimite, ComboBox comSituacaoCredito, ComboBox comFrete, ComboBox comRecebimento, ComboBox comCondicao, ComboBox comTipoDeVenda, ComboBox comContratoEmpresa, MaskedTextBox mtbContraInicio, MaskedTextBox mtbContraFim, TextBox txtCodigoAb2, TextBox txtDescriAb2, MaskedTextBox mtbCpfCnpjAb2, TextBox txtEmailContato1, TextBox txtEmailContato2, TextBox txtSkype, TextBox txtMsn, TextBox txtHomePage, TextBox txtFacebook, ComboBox comSexo, TextBox txtPfLocalDeTrabalho, TextBox txtPfNaturalDe, TextBox txtPfCargo, MaskedTextBox mtbPfAdmissao, MaskedTextBox mtbPfAniversario, TextBox txtPfFiliacaoPai, TextBox txtPfFiliacaoMae, TextBox txtPfConjuge, MaskedTextBox mtbPfCpfConjuge, TextBox txtPjContatoProp1, TextBox txtPjContatoProp2, TextBox txtPjEscritorioContabilidade, TextBox txtPjDiaFatu, ComboBox comPjAtividade, ComboBox comPjRegimeTrib, TextBox txtPjCnae, ComboBox comIcmsRdz, ComboBox comIcmsST, TextBox txtCfop, TextBox txtCfopDesc, TextBox txtMsg1, TextBox txtMsg2, MaskedTextBox mtbDataCadastro, MaskedTextBox mtbRevisao, string LojaLogada, string LojaLogadaDesc, TextBox txtUsuario, TextBox txtRotaSequen)
        {
            if (txtMESTRE.Text == "ALTERAR")
            {
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


                //Define a string e o comando
                string StringComandoALTERAR_TABE = "UPDATE TabClien SET Descri_CLI = @Descri_CLI,SeqPri_CLI = @SeqPri_CLI, Tipo01_CLI = @Tipo01_CLI,CpfCnp_CLI = @CpfCnp_CLI, Fantas_CLI = @Fantas_CLI,TelPab_CLI = @TelPab_CLI,TelFax_CLI = @TelFax_CLI,TelCe1_CLI = @TelCe1_CLI,TelCe2_CLI = @TelCe2_CLI,Catego_CLI = @Catego_CLI,Observ_CLI = @Observ_CLI,EndLo1_CLI = @EndLo1_CLI,EndNu1_CLI = @EndNu1_CLI,EndCi1_CLI = @EndCi1_CLI,EndCe1_CLI = @EndCe1_CLI,EndBa1_CLI = @EndBa1_CLI,EndCo1_CLI = @EndCo1_CLI,EndTi2_CLI = @EndTi2_CLI,EndLo2_CLI = @EndLo2_CLI,EndNu2_CLI = @EndNu2_CLI,EndCi2_CLI = @EndCi2_CLI,EndCe2_CLI = @EndCe2_CLI,EndBa2_CLI = @EndBa2_CLI,EndCo2_CLI = @EndCo2_CLI,TraSeq_CLI = @TraSeq_CLI,ConSeq_CLI = @ConSeq_CLI,InsEst_CLI = @InsEst_CLI,InsMun_CLI = @InsMun_CLI,DtVeEs_CLI = @DtVeEs_CLI,DtVeMu_CLI = @DtVeMu_CLI,RotSeq_CLI = @RotSeq_CLI,Refere_CLI = @Refere_CLI,VenSeq_CLI = @VenSeq_CLI,EmpSeq_CLI = @EmpSeq_CLI,Descon_CLI = @Descon_CLI,BancCl_CLI = @BancCl_CLI,AgenCl_CLI = @AgenCl_CLI,Identi_CLI = @Identi_CLI,Concet_CLI = @Concet_CLI,ValLim_CLI = @ValLim_CLI,VenLim_CLI = @VenLim_CLI,SitCre_CLI = @SitCre_CLI,ComFre_CLI = @ComFre_CLI,ComRec_CLI = @ComRec_CLI,ComCom_CLI = @ComCom_CLI,ComTip_CLI = @ComTip_CLI,Contra_CLI = @Contra_CLI,DtInic_CLI = @DtInic_CLI,DtFina_CLI = @DtFina_CLI,Emai01_CLI = @Emai01_CLI,Emai02_CLI = @Emai02_CLI,MsnCon_CLI = @MsnCon_CLI,Skype1_CLI = @Skype1_CLI,HomPag_CLI = @HomPag_CLI,Facebo_CLI = @Facebo_CLI,SexoCl_CLI = @SexoCl_CLI,LocaTr_CLI = @LocaTr_CLI,Natura_CLI = @Natura_CLI,CargCl_CLI = @CargCl_CLI,FilPai_CLI = @FilPai_CLI,FilMae_CLI = @FilMae_CLI,Conjug_CLI = @Conjug_CLI,CpfCon_CLI = @CpfCon_CLI,DtAdmi_CLI = @DtAdmi_CLI,DtAniv_CLI = @DtAniv_CLI,Conta1_CLI = @Conta1_CLI,Conta2_CLI = @Conta2_CLI,EscCon_CLI = @EscCon_CLI,DiaFat_CLI = @DiaFat_CLI,AtivEm_CLI = @AtivEm_CLI,RegiEm_CLI = @RegiEm_CLI,CnaeEm_CLI = @CnaeEm_CLI,IcmsBc_CLI = @IcmsBc_CLI,IcmsSu_CLI = @IcmsSu_CLI,CfopCl_CLI = @CfopCl_CLI,MsgNo1_CLI = @MsgNo1_CLI,MsgNo2_CLI = @MsgNo2_CLI,DtRevi_CLI = @DtRevi_CLI,Status_CLI = @Status_CLI WHERE Sequen_CLI = @Sequen_CLI";
                SqlCommand ComandoALTERAR_TABE = new SqlCommand(StringComandoALTERAR_TABE, Conexão);
                string StringComandoINCLUIR_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'020100','ALTERAÇÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                SqlCommand ComandoINCLUIR_HIST = new SqlCommand(StringComandoINCLUIR_HIST, Conexão);


                //ROTAS
                #region COMANDOS PARA ROTAS

                string StringComandoROTA_TABE_UP = "UPDATE TabRot01 SET Sequen_RO1 = @Sequen_RO1,SeqCli_RO1 = @SeqCli_RO1,Ordems_RO1 = @Ordems_RO1 WHERE SeqCli_RO1 = @SeqCli_RO1";
                SqlCommand ComandoROTA_TABE_UP = new SqlCommand(StringComandoROTA_TABE_UP, Conexão);
                string StringComandoROTA_TABE_IN = "INSERT INTO TabRot01 (Sequen_RO1,SeqCli_RO1,Ordems_RO1,Status_RO1) VALUES (@Sequen_RO1,@SeqCli_RO1,@Ordems_RO1,@Status_RO1)";
                SqlCommand ComandoROTA_TABE_IN = new SqlCommand(StringComandoROTA_TABE_IN, Conexão);
                string StringComandoROTA_TABE_EX = "DELETE FROM TabRot01 WHERE SeqCli_RO1 = @SeqCli_RO1";
                SqlCommand ComandoROTA_TABE_EX = new SqlCommand(StringComandoROTA_TABE_EX, Conexão);

                //PARAMETERS DE TODOS OS COMANDOS
                #region PARAMETERS ROTA
                #region ComandoROTA_TABE_UP.Parameters.Add("@Sequen_RO1", SqlDbType.Int).Value = txtRotaCod.Text;
                if (txtRotaCod.Text == string.Empty)
                {
                    ComandoROTA_TABE_UP.Parameters.Add("@Sequen_RO1", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoROTA_TABE_UP.Parameters.Add("@Sequen_RO1", SqlDbType.Int).Value = txtRotaCod.Text;
                }
                #endregion
                #region ComandoROTA_TABE_UP.Parameters.Add("@SeqCli_RO1", SqlDbType.Int).Value = txtCodigo.Text;
                if (Convert.ToInt32(txtCodigo.Text) <= 0)
                {
                    ComandoROTA_TABE_UP.Parameters.Add("@SeqCli_RO1", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoROTA_TABE_UP.Parameters.Add("@SeqCli_RO1", SqlDbType.Int).Value = txtCodigo.Text;
                }
                #endregion
                #region ComandoROTA_TABE_UP.Parameters.Add("@Ordems_RO1", SqlDbType.Int).Value = txtRotaSequen.Text;
                if (txtRotaSequen.Text == string.Empty)
                {
                    ComandoROTA_TABE_UP.Parameters.Add("@Ordems_RO1", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoROTA_TABE_UP.Parameters.Add("@Ordems_RO1", SqlDbType.Int).Value = txtRotaSequen.Text;
                }
                #endregion

                #region ComandoROTA_TABE_IN.Parameters.Add("@Sequen_RO1", SqlDbType.Int).Value = txtRotaCod.Text;
                if (txtRotaCod.Text == string.Empty)
                {
                    ComandoROTA_TABE_IN.Parameters.Add("@Sequen_RO1", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoROTA_TABE_IN.Parameters.Add("@Sequen_RO1", SqlDbType.Int).Value = txtRotaCod.Text;
                }
                #endregion
                #region ComandoROTA_TABE_IN.Parameters.Add("@SeqCli_RO1", SqlDbType.Int).Value = txtCodigo.Text;
                if (Convert.ToInt32(txtCodigo.Text) <= 0)
                {
                    ComandoROTA_TABE_IN.Parameters.Add("@SeqCli_RO1", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoROTA_TABE_IN.Parameters.Add("@SeqCli_RO1", SqlDbType.Int).Value = txtCodigo.Text;
                }
                #endregion
                #region ComandoROTA_TABE_IN.Parameters.Add("@Ordems_RO1", SqlDbType.Int).Value = txtRotaSequen.Text;
                if (txtRotaSequen.Text == string.Empty)
                {
                    ComandoROTA_TABE_IN.Parameters.Add("@Ordems_RO1", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoROTA_TABE_IN.Parameters.Add("@Ordems_RO1", SqlDbType.Int).Value = txtRotaSequen.Text;
                }
                #endregion
                ComandoROTA_TABE_IN.Parameters.Add("@Status_RO1", SqlDbType.Int).Value = 1;

                ComandoROTA_TABE_EX.Parameters.Add("@SeqCli_RO1", SqlDbType.Int).Value = txtCodigo.Text;
                #endregion
                #endregion



                #region PARAMETERS TABELA
                ComandoALTERAR_TABE.Parameters.Add("@Sequen_CLI", SqlDbType.Int).Value = txtCodigo.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Descri_CLI", SqlDbType.VarChar).Value = txtDescri.Text;
                ComandoALTERAR_TABE.Parameters.Add("@SeqPri_CLI", SqlDbType.Int).Value = txtCodigoPrin.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Tipo01_CLI", SqlDbType.Int).Value = comTipoPFPJ.SelectedIndex;
                ComandoALTERAR_TABE.Parameters.Add("@CpfCnp_CLI", SqlDbType.VarChar).Value = mtbCpfCnpj.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Fantas_CLI", SqlDbType.VarChar).Value = txtFantasia.Text;
                ComandoALTERAR_TABE.Parameters.Add("@TelPab_CLI", SqlDbType.VarChar).Value = mtbTelPabx.Text;
                ComandoALTERAR_TABE.Parameters.Add("@TelFax_CLI", SqlDbType.VarChar).Value = mtbTelFax.Text;
                ComandoALTERAR_TABE.Parameters.Add("@TelCe1_CLI", SqlDbType.VarChar).Value = mtbTelCel1.Text;
                ComandoALTERAR_TABE.Parameters.Add("@TelCe2_CLI", SqlDbType.VarChar).Value = mtbTelCel2.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Catego_CLI", SqlDbType.Int).Value = comCategoria.SelectedIndex;
                ComandoALTERAR_TABE.Parameters.Add("@Observ_CLI", SqlDbType.VarChar).Value = txtObservacao.Text;
                ComandoALTERAR_TABE.Parameters.Add("@EndLo1_CLI", SqlDbType.VarChar).Value = txtEndLogradouroFATU.Text;
                ComandoALTERAR_TABE.Parameters.Add("@EndNu1_CLI", SqlDbType.VarChar).Value = txtEndNumeroFATU.Text;
                ComandoALTERAR_TABE.Parameters.Add("@EndCi1_CLI", SqlDbType.Int).Value = txtEndCidadeFATU.Text;
                ComandoALTERAR_TABE.Parameters.Add("@EndCe1_CLI", SqlDbType.VarChar).Value = mtbEndCepFATU.Text;
                ComandoALTERAR_TABE.Parameters.Add("@EndBa1_CLI", SqlDbType.VarChar).Value = txtEndBairroFATU.Text;
                ComandoALTERAR_TABE.Parameters.Add("@EndCo1_CLI", SqlDbType.VarChar).Value = txtEndCompleFATU.Text;
                ComandoALTERAR_TABE.Parameters.Add("@EndTi2_CLI", SqlDbType.VarChar).Value = txtLabel.Text;
                ComandoALTERAR_TABE.Parameters.Add("@EndLo2_CLI", SqlDbType.VarChar).Value = txtEndLogradouroPERSO.Text;
                ComandoALTERAR_TABE.Parameters.Add("@EndNu2_CLI", SqlDbType.VarChar).Value = txtEndNumeroPERSO.Text;
                #region ComandoALTERAR_TABE.Parameters.Add("@EndCi2_CLI", SqlDbType.Int).Value = txtEndCidadePERSO.Text;
                if (txtEndCidadePERSO.Text == string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@EndCi2_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@EndCi2_CLI", SqlDbType.Int).Value = txtEndCidadePERSO.Text;
                }
                #endregion
                ComandoALTERAR_TABE.Parameters.Add("@EndCe2_CLI", SqlDbType.VarChar).Value = mtbEndCepPERSO.Text;
                ComandoALTERAR_TABE.Parameters.Add("@EndBa2_CLI", SqlDbType.VarChar).Value = txtEndBairroPERSO.Text;
                ComandoALTERAR_TABE.Parameters.Add("@EndCo2_CLI", SqlDbType.VarChar).Value = txtEndComplePERSO.Text;
                #region ComandoALTERAR_TABE.Parameters.Add("@TraSeq_CLI", SqlDbType.Int).Value = txtTransportadoraCod.Text;
                if (txtTransportadoraCod.Text == string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@TraSeq_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@TraSeq_CLI", SqlDbType.Int).Value = txtTransportadoraCod.Text;
                }
                #endregion
                #region ComandoALTERAR_TABE.Parameters.Add("@ConSeq_CLI", SqlDbType.Int).Value = txtConvenioCod.Text;
                if (txtConvenioCod.Text == string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@ConSeq_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@ConSeq_CLI", SqlDbType.Int).Value = txtConvenioCod.Text;
                }
                #endregion
                ComandoALTERAR_TABE.Parameters.Add("@InsEst_CLI", SqlDbType.VarChar).Value = txtInscricaoEstadual.Text;
                ComandoALTERAR_TABE.Parameters.Add("@InsMun_CLI", SqlDbType.VarChar).Value = txtInscricaoMunicipal.Text;
                #region ComandoALTERAR_TABE.Parameters.Add("@DtVeEs_CLI", SqlDbType.Date).Value = mtbVencEst.Text;
                if (mtbVencEst.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@DtVeEs_CLI", SqlDbType.Date).Value = DateTime.MinValue;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@DtVeEs_CLI", SqlDbType.Date).Value = mtbVencEst.Text;
                }
                #endregion
                #region ComandoALTERAR_TABE.Parameters.Add("@DtVeMu_CLI", SqlDbType.Date).Value = mtbVencMun.Text;
                if (mtbVencMun.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@DtVeMu_CLI", SqlDbType.Date).Value = DateTime.MinValue;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@DtVeMu_CLI", SqlDbType.Date).Value = mtbVencMun.Text;
                }
                #endregion
                #region ComandoALTERAR_TABE.Parameters.Add("@RotSeq_CLI", SqlDbType.Int).Value = txtRotaCod.Text;
                if (txtRotaCod.Text == string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@RotSeq_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@RotSeq_CLI", SqlDbType.Int).Value = txtRotaCod.Text;
                }
                #endregion
                #region ComandoALTERAR_TABE.Parameters.Add("@Refere_CLI", SqlDbType.Int).Value = txtReferencialCod.Text;
                if (txtReferencialCod.Text == string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@Refere_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@Refere_CLI", SqlDbType.Int).Value = txtReferencialCod.Text;
                }
                #endregion
                #region ComandoALTERAR_TABE.Parameters.Add("@VenSeq_CLI", SqlDbType.Int).Value = txtVendedorCod.Text;
                if (txtVendedorCod.Text == string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@VenSeq_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@VenSeq_CLI", SqlDbType.Int).Value = txtVendedorCod.Text;
                }
                #endregion
                ComandoALTERAR_TABE.Parameters.Add("@EmpSeq_CLI", SqlDbType.Int).Value = txtEmpresaCod.Text;
                #region ComandoALTERAR_TABE.Parameters.Add("@Descon_CLI", SqlDbType.Decimal).Value = txtDesconto.Text;
                if (txtDesconto.Text == string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@Descon_CLI", SqlDbType.Decimal).Value = 0;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@Descon_CLI", SqlDbType.Decimal).Value = txtDesconto.Text;
                }
                #endregion
                #region ComandoALTERAR_TABE.Parameters.Add("@BancCl_CLI", SqlDbType.Int).Value = txtBanco.Text;
                if (txtBanco.Text == string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@BancCl_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@BancCl_CLI", SqlDbType.Int).Value = txtBanco.Text;
                }
                #endregion
                #region ComandoALTERAR_TABE.Parameters.Add("@AgenCl_CLI", SqlDbType.Int).Value = txtAgencia.Text;
                if (txtAgencia.Text == string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@AgenCl_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@AgenCl_CLI", SqlDbType.Int).Value = txtAgencia.Text;
                }
                #endregion
                ComandoALTERAR_TABE.Parameters.Add("@Identi_CLI", SqlDbType.VarChar).Value = txtRgIdentidade.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Concet_CLI", SqlDbType.VarChar).Value = txtConceito.Text;
                #region ComandoALTERAR_TABE.Parameters.Add("@ValLim_CLI", SqlDbType.Decimal).Value = txtValorLimiteCre.Text;
                if (txtValorLimiteCre.Text == string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@ValLim_CLI", SqlDbType.Decimal).Value = 0;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@ValLim_CLI", SqlDbType.Decimal).Value = txtValorLimiteCre.Text;
                }
                #endregion
                #region ComandoALTERAR_TABE.Parameters.Add("@VenLim_CLI", SqlDbType.Date).Value = mtbDataVenciLimite.Text;
                if (mtbDataVenciLimite.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@VenLim_CLI", SqlDbType.Date).Value = DateTime.MinValue;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@VenLim_CLI", SqlDbType.Date).Value = mtbDataVenciLimite.Text;
                }
                #endregion
                ComandoALTERAR_TABE.Parameters.Add("@SitCre_CLI", SqlDbType.Int).Value = comSituacaoCredito.SelectedIndex;
                ComandoALTERAR_TABE.Parameters.Add("@ComFre_CLI", SqlDbType.Int).Value = comFrete.SelectedIndex;
                ComandoALTERAR_TABE.Parameters.Add("@ComRec_CLI", SqlDbType.Int).Value = comRecebimento.SelectedIndex;
                ComandoALTERAR_TABE.Parameters.Add("@ComCom_CLI", SqlDbType.Int).Value = comCondicao.SelectedIndex;
                ComandoALTERAR_TABE.Parameters.Add("@ComTip_CLI", SqlDbType.Int).Value = comTipoDeVenda.SelectedIndex;
                ComandoALTERAR_TABE.Parameters.Add("@Contra_CLI", SqlDbType.Int).Value = comContratoEmpresa.SelectedIndex;
                #region ComandoALTERAR_TABE.Parameters.Add("@DtInic_CLI", SqlDbType.Date).Value = mtbContraInicio.Text;
                if (mtbContraInicio.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@DtInic_CLI", SqlDbType.Date).Value = DateTime.MinValue;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@DtInic_CLI", SqlDbType.Date).Value = mtbContraInicio.Text;
                }
                #endregion
                #region ComandoALTERAR_TABE.Parameters.Add("@DtFina_CLI", SqlDbType.Date).Value = mtbContraFim.Text;
                if (mtbContraFim.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@DtFina_CLI", SqlDbType.Date).Value = DateTime.MinValue;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@DtFina_CLI", SqlDbType.Date).Value = mtbContraFim.Text;
                }
                #endregion
                ComandoALTERAR_TABE.Parameters.Add("@Emai01_CLI", SqlDbType.VarChar).Value = txtEmailContato1.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Emai02_CLI", SqlDbType.VarChar).Value = txtEmailContato2.Text;
                ComandoALTERAR_TABE.Parameters.Add("@MsnCon_CLI", SqlDbType.VarChar).Value = txtMsn.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Skype1_CLI", SqlDbType.VarChar).Value = txtSkype.Text;
                ComandoALTERAR_TABE.Parameters.Add("@HomPag_CLI", SqlDbType.VarChar).Value = txtHomePage.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Facebo_CLI", SqlDbType.VarChar).Value = txtFacebook.Text;
                ComandoALTERAR_TABE.Parameters.Add("@SexoCl_CLI", SqlDbType.Int).Value = comSexo.SelectedIndex;
                ComandoALTERAR_TABE.Parameters.Add("@LocaTr_CLI", SqlDbType.VarChar).Value = txtPfLocalDeTrabalho.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Natura_CLI", SqlDbType.VarChar).Value = txtPfNaturalDe.Text;
                ComandoALTERAR_TABE.Parameters.Add("@CargCl_CLI", SqlDbType.VarChar).Value = txtPfCargo.Text;
                ComandoALTERAR_TABE.Parameters.Add("@FilPai_CLI", SqlDbType.VarChar).Value = txtPfFiliacaoPai.Text;
                ComandoALTERAR_TABE.Parameters.Add("@FilMae_CLI", SqlDbType.VarChar).Value = txtPfFiliacaoMae.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Conjug_CLI", SqlDbType.VarChar).Value = txtPfConjuge.Text;
                ComandoALTERAR_TABE.Parameters.Add("@CpfCon_CLI", SqlDbType.VarChar).Value = mtbPfCpfConjuge.Text;
                #region ComandoALTERAR_TABE.Parameters.Add("@DtAdmi_CLI", SqlDbType.Date).Value = mtbPfAdmissao.Text;
                if (mtbPfAdmissao.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@DtAdmi_CLI", SqlDbType.Date).Value = DateTime.MinValue;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@DtAdmi_CLI", SqlDbType.Date).Value = mtbPfAdmissao.Text;
                }
                #endregion
                #region ComandoALTERAR_TABE.Parameters.Add("@DtAniv_CLI", SqlDbType.Date).Value = mtbPfAniversario.Text;
                if (mtbPfAniversario.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@DtAniv_CLI", SqlDbType.Date).Value = DateTime.MinValue;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@DtAniv_CLI", SqlDbType.Date).Value = mtbPfAniversario.Text;
                }
                #endregion
                ComandoALTERAR_TABE.Parameters.Add("@Conta1_CLI", SqlDbType.VarChar).Value = txtPjContatoProp1.Text;
                ComandoALTERAR_TABE.Parameters.Add("@Conta2_CLI", SqlDbType.VarChar).Value = txtPjContatoProp2.Text;
                ComandoALTERAR_TABE.Parameters.Add("@EscCon_CLI", SqlDbType.VarChar).Value = txtPjEscritorioContabilidade.Text;
                #region ComandoALTERAR_TABE.Parameters.Add("@DiaFat_CLI", SqlDbType.Int).Value = txtPjDiaFatu.Text;
                if (txtPjDiaFatu.Text == string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@DiaFat_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@DiaFat_CLI", SqlDbType.Int).Value = txtPjDiaFatu.Text;
                }
                #endregion
                ComandoALTERAR_TABE.Parameters.Add("@AtivEm_CLI", SqlDbType.Int).Value = comPjAtividade.SelectedIndex;
                ComandoALTERAR_TABE.Parameters.Add("@RegiEm_CLI", SqlDbType.Int).Value = comPjRegimeTrib.SelectedIndex;
                ComandoALTERAR_TABE.Parameters.Add("@CnaeEm_CLI", SqlDbType.VarChar).Value = txtPjCnae.Text;
                ComandoALTERAR_TABE.Parameters.Add("@IcmsBc_CLI", SqlDbType.Int).Value = comIcmsRdz.SelectedIndex;
                ComandoALTERAR_TABE.Parameters.Add("@IcmsSu_CLI", SqlDbType.Int).Value = comIcmsST.SelectedIndex;
                #region ComandoALTERAR_TABE.Parameters.Add("@CfopCl_CLI", SqlDbType.Int).Value = txtCfop.Text;
                if (txtCfop.Text == string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@CfopCl_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@CfopCl_CLI", SqlDbType.Int).Value = txtCfop.Text;
                }
                #endregion
                #region ComandoALTERAR_TABE.Parameters.Add("@MsgNo1_CLI", SqlDbType.Int).Value = txtMsg1.Text;
                if (txtMsg1.Text == string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@MsgNo1_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@MsgNo1_CLI", SqlDbType.Int).Value = txtMsg1.Text;
                }
                #endregion
                #region ComandoALTERAR_TABE.Parameters.Add("@MsgNo2_CLI", SqlDbType.Int).Value = txtMsg2.Text;
                if (txtMsg2.Text == string.Empty)
                {
                    ComandoALTERAR_TABE.Parameters.Add("@MsgNo2_CLI", SqlDbType.Int).Value = 0;
                }
                else
                {
                    ComandoALTERAR_TABE.Parameters.Add("@MsgNo2_CLI", SqlDbType.Int).Value = txtMsg2.Text;
                }
                #endregion
                ComandoALTERAR_TABE.Parameters.Add("@DtRevi_CLI", SqlDbType.DateTime).Value = DateTime.Now;
                #region ComandoALTERAR_TABE.Parameters.Add("@Status_CLI", SqlDbType.Int).Value = STATUS;
                int STATUS = comStatus.SelectedIndex;
                if (STATUS == 3)
                {
                    STATUS = 4;
                }
                ComandoALTERAR_TABE.Parameters.Add("@Status_CLI", SqlDbType.Int).Value = STATUS;
                #endregion
                #endregion
                #region PARAMETERS HISTÓRICO
                //Parametros do Insert no Historico
                ComandoINCLUIR_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "ALTERAÇÃO DO CLIENTE...: " + txtCodigo.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = txtDescri.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoINCLUIR_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();
                #endregion

                
                try
                {
                    ComandoALTERAR_TABE.ExecuteNonQuery();
                    ComandoINCLUIR_HIST.ExecuteNonQuery();

                    #region TRATAMENTO PARA ROTAS
                    if (txtRotaCod.Text != string.Empty)
                    {
                        //VERIFICA SE EXISTE LINHA NA TABELA DE ROTAS
                        int COMANDO = ComandoROTA_TABE_UP.ExecuteNonQuery();
                        //SE NÃO EXISTIR, EU INCLUO UMA
                        if (COMANDO == 0)
                        {
                            ComandoROTA_TABE_IN.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        //SE EU APAGAR O VALOR DO TXT DA ROTA, EU APAGO TAMBÉM NA TABELA
                        ComandoROTA_TABE_EX.ExecuteNonQuery();
                    }
                    #endregion

                    MessageBox.Show("Dados alterados no banco de dados com sucesso!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ZerarCampos();
                    txtCodigo.Select(); txtCodigo.SelectAll();
                    btnInfFinaShow.Enabled = false;
                    btnInfComerShow.Enabled = false;
                    btnAvancar.Enabled = false;
                    btnGravar.Enabled = false;
                    btnGravarAb2.Enabled = false;
                    btnVoltar.Enabled = false;
                    CamposDisable();
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarALT()\n\nBLOCO = ALTERAÇÃO DE DADOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarALT()\n\nBLOCO = ALTERAÇÃO DE DADOS\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }
        public void GravarEXC(TextBox txtCodigo, TextBox txtMESTRE, Panel panUpAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, MethodInvoker CamposEnable, Button btnGravar, Button btnInfFinaShow, Button btnInfComerShow, Button btnAvancar, Button btnGravarAb2, Button btnVoltar, TextBox txtDescri, TextBox txtCodigoPrin, ComboBox comTipoPFPJ, MaskedTextBox mtbCpfCnpj, ComboBox comStatus, TextBox txtFantasia, MaskedTextBox mtbTelPabx, MaskedTextBox mtbTelCel1, ComboBox comCategoria, TextBox txtObservacao, MaskedTextBox mtbTelFax, MaskedTextBox mtbTelCel2, TextBox txtInscricaoEstadual, MaskedTextBox mtbVencEst, TextBox txtRotaCod, TextBox txtVendedorCod, TextBox txtVendedorDesc, TextBox txtDesconto, TextBox txtBanco, TextBox txtAgencia, TextBox txtInscricaoMunicipal, MaskedTextBox mtbVencMun, TextBox txtReferencialCod, TextBox txtEmpresaCod, TextBox txtEmpresaDesc, TextBox txtRgIdentidade, TextBox txtConceito, TextBox txtEndLogradouroFATU, TextBox txtEndNumeroFATU, TextBox txtEndCidadeFATU, TextBox txtEndCidDescriFATU, TextBox txtEndCidUFFATU, MaskedTextBox mtbEndCepFATU, TextBox txtEndBairroFATU, TextBox txtEndCompleFATU, TextBox txtLabel, TextBox txtEndLogradouroPERSO, TextBox txtEndNumeroPERSO, TextBox txtEndCidadePERSO, TextBox txtEndCidDescriPERSO, TextBox txtEndCidUFPERSO, MaskedTextBox mtbEndCepPERSO, TextBox txtEndBairroPERSO, TextBox txtEndComplePERSO, TextBox txtTransportadoraCod, TextBox txtTransportadoraDesc, TextBox txtConvenioCod, TextBox txtConvenioDesc, TextBox txtValorLimiteCre, MaskedTextBox mtbDataVenciLimite, ComboBox comSituacaoCredito, ComboBox comFrete, ComboBox comRecebimento, ComboBox comCondicao, ComboBox comTipoDeVenda, ComboBox comContratoEmpresa, MaskedTextBox mtbContraInicio, MaskedTextBox mtbContraFim, TextBox txtCodigoAb2, TextBox txtDescriAb2, MaskedTextBox mtbCpfCnpjAb2, TextBox txtEmailContato1, TextBox txtEmailContato2, TextBox txtSkype, TextBox txtMsn, TextBox txtHomePage, TextBox txtFacebook, ComboBox comSexo, TextBox txtPfLocalDeTrabalho, TextBox txtPfNaturalDe, TextBox txtPfCargo, MaskedTextBox mtbPfAdmissao, MaskedTextBox mtbPfAniversario, TextBox txtPfFiliacaoPai, TextBox txtPfFiliacaoMae, TextBox txtPfConjuge, MaskedTextBox mtbPfCpfConjuge, TextBox txtPjContatoProp1, TextBox txtPjContatoProp2, TextBox txtPjEscritorioContabilidade, TextBox txtPjDiaFatu, ComboBox comPjAtividade, ComboBox comPjRegimeTrib, TextBox txtPjCnae, ComboBox comIcmsRdz, ComboBox comIcmsST, TextBox txtCfop, TextBox txtCfopDesc, TextBox txtMsg1, TextBox txtMsg2, MaskedTextBox mtbDataCadastro, MaskedTextBox mtbRevisao, string LojaLogada, string LojaLogadaDesc, TextBox txtUsuario, string MOTIVO_EXC)
        {
            if (txtMESTRE.Text == "EXCLUIR")
            {
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
                string StringComandoEXCLUI_TABE = "UPDATE TabClien SET Status_CLI = 3 WHERE Sequen_CLI = @Sequen_CLI";
                string StringComandoEXCLUI_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'020100','EXCLUSÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";
                SqlCommand ComandoEXCLUI_HIST = new SqlCommand(StringComandoEXCLUI_HIST, Conexão);
                SqlCommand ComandoEXCLUI_TABE = new SqlCommand(StringComandoEXCLUI_TABE, Conexão);



                ComandoEXCLUI_TABE.Parameters.Add("@Sequen_CLI", SqlDbType.VarChar).Value = txtCodigo.Text;



                //Parametros do Exc no banco
                ComandoEXCLUI_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoEXCLUI_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "EXCLUSÃO DO CLIENTE...: " + txtCodigo.Text;
                ComandoEXCLUI_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = "Motivo.: " + MOTIVO_EXC;
                ComandoEXCLUI_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoEXCLUI_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now;


                DialogResult Exc = MessageBox.Show("Deseja mover o registro para a LIXEIRA?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Exc == DialogResult.Yes)
                {
                    try
                    {
                        ComandoEXCLUI_TABE.ExecuteNonQuery();
                        ComandoEXCLUI_HIST.ExecuteNonQuery();
                        MessageBox.Show("Registro enviado para a LIXEIRA!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ZerarCampos();
                        CamposDisable();
                        btnInfFinaShow.Enabled = false;
                        btnInfComerShow.Enabled = false;
                        btnAvancar.Enabled = false;
                        btnGravar.Enabled = false;
                        btnGravarAb2.Enabled = false;
                        btnVoltar.Enabled = false;
                    }
                    catch (SqlException Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarEXC()\n\nBLOCO = EXCLUSÃO DE DADOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarEXC()\n\nBLOCO = EXCLUSÃO DE DADOS\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Conexão.Close();
                    }
                }
                if (Exc == DialogResult.No)
                {
                    ZerarCampos();
                    CamposDisable();
                    panUpAb1.Focus();
                    btnInfFinaShow.Enabled = false;
                    btnInfComerShow.Enabled = false;
                    btnAvancar.Enabled = false;
                    btnGravar.Enabled = false;
                    btnGravarAb2.Enabled = false;
                    btnVoltar.Enabled = false;
                }
            }
        }
    }
}
