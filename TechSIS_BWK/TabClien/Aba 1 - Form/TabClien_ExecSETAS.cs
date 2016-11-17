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
using System.Xml;

namespace TabClien
{
    internal class TabClien_ExecSETAS
    {
        public void ExecSETAS(string StringComandoSELEÇÃO, TextBox txtCodigo, TextBox txtMESTRE, Panel panUpAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, MethodInvoker CamposEnable, Button btnGravar, Button btnInfFinaShow, Button btnInfComerShow, Button btnAvancar, Button btnGravarAb2, Button btnVoltar, TextBox txtDescri, TextBox txtCodigoPrin, ComboBox comTipoPFPJ, MaskedTextBox mtbCpfCnpj, ComboBox comStatus, TextBox txtFantasia, MaskedTextBox mtbTelPabx, MaskedTextBox mtbTelCel1, ComboBox comCategoria, TextBox txtObservacao, MaskedTextBox mtbTelFax, MaskedTextBox mtbTelCel2, TextBox txtInscricaoEstadual, MaskedTextBox mtbVencEst, TextBox txtRotaCod, TextBox txtVendedorCod, TextBox txtVendedorDesc, TextBox txtDesconto, TextBox txtBanco, TextBox txtAgencia, TextBox txtInscricaoMunicipal, MaskedTextBox mtbVencMun, TextBox txtReferencialCod, TextBox txtEmpresaCod, TextBox txtEmpresaDesc, TextBox txtRgIdentidade, TextBox txtConceito, TextBox txtEndLogradouroFATU, TextBox txtEndNumeroFATU, TextBox txtEndCidadeFATU, TextBox txtEndCidDescriFATU, TextBox txtEndCidUFFATU, MaskedTextBox mtbEndCepFATU, TextBox txtEndBairroFATU, TextBox txtEndCompleFATU, TextBox txtLabel, TextBox txtEndLogradouroPERSO, TextBox txtEndNumeroPERSO, TextBox txtEndCidadePERSO, TextBox txtEndCidDescriPERSO, TextBox txtEndCidUFPERSO, MaskedTextBox mtbEndCepPERSO, TextBox txtEndBairroPERSO, TextBox txtEndComplePERSO, TextBox txtTransportadoraCod, TextBox txtTransportadoraDesc, TextBox txtConvenioCod, TextBox txtConvenioDesc, TextBox txtValorLimiteCre, MaskedTextBox mtbDataVenciLimite, ComboBox comSituacaoCredito, ComboBox comFrete, ComboBox comRecebimento, ComboBox comCondicao, ComboBox comTipoDeVenda, ComboBox comContratoEmpresa, MaskedTextBox mtbContraInicio, MaskedTextBox mtbContraFim, TextBox txtCodigoAb2, TextBox txtDescriAb2, MaskedTextBox mtbCpfCnpjAb2, TextBox txtEmailContato1, TextBox txtEmailContato2, TextBox txtSkype, TextBox txtMsn, TextBox txtHomePage, TextBox txtFacebook, ComboBox comSexo, TextBox txtPfLocalDeTrabalho, TextBox txtPfNaturalDe, TextBox txtPfCargo, MaskedTextBox mtbPfAdmissao, MaskedTextBox mtbPfAniversario, TextBox txtPfFiliacaoPai, TextBox txtPfFiliacaoMae, TextBox txtPfConjuge, MaskedTextBox mtbPfCpfConjuge, TextBox txtPjContatoProp1, TextBox txtPjContatoProp2, TextBox txtPjEscritorioContabilidade, TextBox txtPjDiaFatu, ComboBox comPjAtividade, ComboBox comPjRegimeTrib, TextBox txtPjCnae, ComboBox comIcmsRdz, ComboBox comIcmsST, TextBox txtCfop, TextBox txtCfopDesc, TextBox txtMsg1, TextBox txtMsg2, MaskedTextBox mtbDataCadastro, MaskedTextBox mtbRevisao, string LojaLogada, string LojaLogadaDesc, TextBox txtRotaSequen)
        {
            string Mensagem_De_Erro = "";

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string NomeDaOpção = "Clientes";

            #region Comandos
            if (StringComandoSELEÇÃO == "1")
            {
                StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabClien INNER JOIN TabCidad ON TabClien.EndCi1_CLI = TabCidad.Sequen_CID INNER JOIN TabEmpre ON TabClien.EmpSeq_CLI = TabEmpre.Sequen_EMP WHERE Status_CLI <> 3 AND Sequen_CLI >= 1 ORDER BY Sequen_CLI";
                Mensagem_De_Erro = "Sem dados de " + NomeDaOpção + " para exibir";
            }
            if (StringComandoSELEÇÃO == "2")
            {
                string Num = txtCodigo.Text;
                if (txtCodigo.Text == string.Empty)
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabClien INNER JOIN TabCidad ON TabClien.EndCi1_CLI = TabCidad.Sequen_CID INNER JOIN TabEmpre ON TabClien.EmpSeq_CLI = TabEmpre.Sequen_EMP WHERE Status_CLI <> 3 AND Sequen_CLI >= 1 ORDER BY Sequen_CLI";
                    Mensagem_De_Erro = "Não existe registro anterior no banco";
                }
                else
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabClien INNER JOIN TabCidad ON TabClien.EndCi1_CLI = TabCidad.Sequen_CID INNER JOIN TabEmpre ON TabClien.EmpSeq_CLI = TabEmpre.Sequen_EMP WHERE Sequen_CLI < " + Num + " AND Status_CLI <> 3 ORDER BY Sequen_CLI DESC";
                    Mensagem_De_Erro = "Não existe registro anterior no banco";
                }
            }
            if (StringComandoSELEÇÃO == "3")
            {
                string Num = txtCodigo.Text;
                if (txtCodigo.Text == string.Empty)
                {
                    Num = "0";
                }
                StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabClien INNER JOIN TabCidad ON TabClien.EndCi1_CLI = TabCidad.Sequen_CID INNER JOIN TabEmpre ON TabClien.EmpSeq_CLI = TabEmpre.Sequen_EMP WHERE Sequen_CLI > " + Num + " AND Status_CLI <> 3";
                Mensagem_De_Erro = "Não existe próximo registro no banco";
            }
            if (StringComandoSELEÇÃO == "4")
            {
                StringComandoSELEÇÃO = "SELECT TOP 1 * FROM TabClien INNER JOIN TabCidad ON TabClien.EndCi1_CLI = TabCidad.Sequen_CID INNER JOIN TabEmpre ON TabClien.EmpSeq_CLI = TabEmpre.Sequen_EMP WHERE Status_CLI <> 3 AND Sequen_CLI >= 1 ORDER BY Sequen_CLI DESC";
                Mensagem_De_Erro = "Sem dados de " + NomeDaOpção + " para exibir";
            }
            #endregion

            SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);

            try
            {
                SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    #region PARAMETROS DR[]
                    txtCodigo.Text = Dr["Sequen_CLI"].ToString().PadLeft(6, '0');
                    txtDescri.Text = Dr["Descri_CLI"].ToString();
                    txtCodigoPrin.Text = Dr["SeqPri_CLI"].ToString().PadLeft(6, '0');
                    #region comTipoPFPJ.SelectedIndex = Convert.ToInt32(Dr["Tipo01_CLI"]);
                    try
                    {
                        comTipoPFPJ.SelectedIndex = Convert.ToInt32(Dr["Tipo01_CLI"]);
                    }
                    catch (Exception)
                    {
                        comTipoPFPJ.SelectedIndex = 1;
                    }
                    #endregion
                    mtbCpfCnpj.Text = Dr["CpfCnp_CLI"].ToString();
                    #region comStatus.SelectedIndex = Convert.ToInt32(Dr["Status_CLI"]);
                    int STATUS = Convert.ToInt32(Dr["Status_CLI"]);
                    if (STATUS == 4)
                    {
                        STATUS = 3;
                    }
                    if (STATUS <= 0)
                    {
                        STATUS = 1;
                    }
                    try
                    {
                        comStatus.SelectedIndex = STATUS;
                    }
                    catch (Exception)
                    {
                        comStatus.SelectedIndex = 1;
                    }
                    #endregion
                    #region comCategoria.SelectedIndex = Convert.ToInt32(Dr["Catego_CLI"]);
                    try
                    {
                        comCategoria.SelectedIndex = Convert.ToInt32(Dr["Catego_CLI"]);
                    }
                    catch (Exception)
                    {
                        if (comTipoPFPJ.SelectedIndex == 1)
                        {
                            comCategoria.SelectedIndex = 0;
                        }
                        else
                        {
                            comCategoria.SelectedIndex = 1;
                        }
                    }
                    #endregion
                    txtFantasia.Text = Dr["Fantas_CLI"].ToString();
                    txtObservacao.Text = Dr["Observ_CLI"].ToString();
                    mtbTelPabx.Text = Dr["TelPab_CLI"].ToString();
                    mtbTelFax.Text = Dr["TelFax_CLI"].ToString();
                    mtbTelCel1.Text = Dr["TelCe1_CLI"].ToString();
                    mtbTelCel2.Text = Dr["TelCe2_CLI"].ToString();

                    txtInscricaoEstadual.Text = Dr["InsEst_CLI"].ToString();
                    txtInscricaoMunicipal.Text = Dr["InsMun_CLI"].ToString();
                    #region mtbVencEst.Text = Dr["DtVeEs_CLI"].ToString();
                    if (Convert.ToDateTime(Dr["DtVeEs_CLI"]) == DateTime.MinValue)
                    {
                        mtbVencEst.Text = string.Empty;
                    }
                    else
                    {
                        mtbVencEst.Text = Dr["DtVeEs_CLI"].ToString();
                    }
                    #endregion
                    #region mtbVencEst.Text = Dr["DtVeEs_CLI"].ToString();
                    if (Convert.ToDateTime(Dr["DtVeMu_CLI"]) == DateTime.MinValue)
                    {
                        mtbVencMun.Text = string.Empty;
                    }
                    else
                    {
                        mtbVencMun.Text = Dr["DtVeMu_CLI"].ToString();
                    }
                    #endregion
                    #region txtRotaCod.Text = Dr["RotSeq_CLI"].ToString().PadLeft(6, '0');
                    if (Convert.ToInt32(Dr["RotSeq_CLI"]) <= 0)
                    {
                        txtRotaCod.Text = string.Empty;
                    }
                    else
                    {
                        txtRotaCod.Text = Dr["RotSeq_CLI"].ToString().PadLeft(6, '0');
                    }
                    #endregion
                    #region txtReferencialCod.Text = Dr["Refere_CLI"].ToString().PadLeft(6, '0');
                    if (Convert.ToInt32(Dr["Refere_CLI"]) <= 0)
                    {
                        txtReferencialCod.Text = string.Empty;
                    }
                    else
                    {
                        txtReferencialCod.Text = Dr["Refere_CLI"].ToString().PadLeft(6, '0');
                    }
                    #endregion
                    #region txtVendedorCod.Text = Dr["VenSeq_CLI"].ToString().PadLeft(6, '0');
                    if (Convert.ToInt32(Dr["VenSeq_CLI"]) <= 0)
                    {
                        txtVendedorCod.Text = string.Empty;
                    }
                    else
                    {
                        txtVendedorCod.Text = Dr["VenSeq_CLI"].ToString().PadLeft(6, '0');
                    }
                    #endregion
                    txtEmpresaCod.Text = Dr["EmpSeq_CLI"].ToString().PadLeft(6, '0');
                    txtEmpresaDesc.Text = Dr["Descri_EMP"].ToString();
                    #region txtDesconto.Text = Convert.ToDecimal(Dr["Descon_CLI"]).ToString("00.00");
                    if (Convert.ToDecimal(Dr["Descon_CLI"]) <= 0)
                    {
                        txtDesconto.Text = string.Empty;
                    }
                    else
                    {
                        txtDesconto.Text = Convert.ToDecimal(Dr["Descon_CLI"]).ToString("00.00");
                    }
                    #endregion
                    #region txtBanco.Text = Dr["BancCl_CLI"].ToString().PadLeft(3, '0');
                    if (Convert.ToInt32(Dr["BancCl_CLI"]) <= 0)
                    {
                        txtBanco.Text = string.Empty;
                    }
                    else
                    {
                        txtBanco.Text = Dr["BancCl_CLI"].ToString().PadLeft(3, '0');
                    }
                    #endregion
                    #region txtAgencia.Text = Dr["AgenCl_CLI"].ToString().PadLeft(5, '0');
                    if (Convert.ToInt32(Dr["AgenCl_CLI"]) <= 0)
                    {
                        txtAgencia.Text = string.Empty;
                    }
                    else
                    {
                        txtAgencia.Text = Dr["AgenCl_CLI"].ToString().PadLeft(6, '0');
                    }
                    #endregion
                    txtRgIdentidade.Text = Dr["Identi_CLI"].ToString();
                    txtConceito.Text = Dr["Concet_CLI"].ToString();

                    txtEndLogradouroFATU.Text = Dr["EndLo1_CLI"].ToString();
                    txtEndNumeroFATU.Text = Dr["EndNu1_CLI"].ToString();
                    txtEndCidadeFATU.Text = Dr["EndCi1_CLI"].ToString().PadLeft(6, '0');
                    txtEndCidDescriFATU.Text = Dr["Descri_CID"].ToString();
                    txtEndCidUFFATU.Text = Dr["UfFede_CID"].ToString();
                    mtbEndCepFATU.Text = Dr["EndCe1_CLI"].ToString();
                    txtEndBairroFATU.Text = Dr["EndBa1_CLI"].ToString();
                    txtEndCompleFATU.Text = Dr["EndCo1_CLI"].ToString();

                    txtLabel.Text = Dr["EndTi2_CLI"].ToString();
                    txtEndLogradouroPERSO.Text = Dr["EndLo2_CLI"].ToString();
                    txtEndNumeroPERSO.Text = Dr["EndNu2_CLI"].ToString();
                    #region txtEndCidadePERSO.Text = Dr["EndCi2_CLI"].ToString().PadLeft(6, '0');
                    if (Convert.ToInt32(Dr["EndCi2_CLI"]) <= 0)
                    {
                        txtEndCidadePERSO.Text = string.Empty;
                    }
                    else
                    {
                        txtEndCidadePERSO.Text = Dr["EndCi2_CLI"].ToString().PadLeft(6, '0');
                    }
                    #endregion
                    mtbEndCepPERSO.Text = Dr["EndCe2_CLI"].ToString();
                    txtEndBairroPERSO.Text = Dr["EndBa2_CLI"].ToString();
                    txtEndComplePERSO.Text = Dr["EndCo2_CLI"].ToString();

                    #region txtTransportadoraCod.Text = Dr["TraSeq_CLI"].ToString().PadLeft(6, '0');
                    if (Convert.ToInt32(Dr["TraSeq_CLI"]) <= 0)
                    {
                        txtTransportadoraCod.Text = string.Empty;
                    }
                    else
                    {
                        txtTransportadoraCod.Text = Dr["TraSeq_CLI"].ToString().PadLeft(6, '0');
                    }
                    #endregion
                    #region txtConvenioCod.Text = Dr["ConSeq_CLI"].ToString().PadLeft(6, '0');
                    if (Convert.ToInt32(Dr["ConSeq_CLI"]) <= 0)
                    {
                        txtConvenioCod.Text = string.Empty;
                    }
                    else
                    {
                        txtConvenioCod.Text = Dr["ConSeq_CLI"].ToString().PadLeft(6, '0');
                    }
                    #endregion

                    #region txtValorLimiteCre.Text = Convert.ToDecimal(Dr["ValLim_CLI"]).ToString("00.00");
                    if (Convert.ToDecimal(Dr["ValLim_CLI"]) <= 0)
                    {
                        txtValorLimiteCre.Text = string.Empty;
                    }
                    else
                    {
                        txtValorLimiteCre.Text = Convert.ToDecimal(Dr["ValLim_CLI"]).ToString("00.00");
                    }
                    #endregion
                    #region mtbDataVenciLimite.Text = Dr["VenLim_CLI"].ToString();
                    if (Convert.ToDateTime(Dr["VenLim_CLI"]) == DateTime.MinValue)
                    {
                        mtbDataVenciLimite.Text = string.Empty;
                    }
                    else
                    {
                        mtbDataVenciLimite.Text = Dr["VenLim_CLI"].ToString();
                    }
                    #endregion
                    #region comSituacaoCredito.SelectedIndex = Convert.ToInt32(Dr["SitCre_CLI"]);
                    try
                    {
                        comSituacaoCredito.SelectedIndex = Convert.ToInt32(Dr["SitCre_CLI"]);
                    }
                    catch (Exception)
                    {
                        comSituacaoCredito.SelectedIndex = 1;
                    }
                    #endregion

                    #region comFrete.SelectedIndex = Convert.ToInt32(Dr["ComFre_CLI"]);
                    try
                    {
                        comFrete.SelectedIndex = Convert.ToInt32(Dr["ComFre_CLI"]);
                    }
                    catch (Exception)
                    {
                        comFrete.SelectedIndex = 0;
                    }
                    #endregion
                    #region comRecebimento.SelectedIndex = Convert.ToInt32(Dr["ComRec_CLI"]);
                    try
                    {
                        comRecebimento.SelectedIndex = Convert.ToInt32(Dr["ComRec_CLI"]);
                    }
                    catch (Exception)
                    {
                        comRecebimento.SelectedIndex = 0;
                    }
                    #endregion
                    #region comCondicao.SelectedIndex = Convert.ToInt32(Dr["ComCom_CLI"]);
                    try
                    {
                        comCondicao.SelectedIndex = Convert.ToInt32(Dr["ComCom_CLI"]);
                    }
                    catch (Exception)
                    {
                        comCondicao.SelectedIndex = 0;
                    }
                    #endregion
                    #region comTipoDeVenda.SelectedIndex = Convert.ToInt32(Dr["ComTip_CLI"]);
                    try
                    {
                        comTipoDeVenda.SelectedIndex = Convert.ToInt32(Dr["ComTip_CLI"]);
                    }
                    catch (Exception)
                    {
                        comTipoDeVenda.SelectedIndex = 0;
                    }
                    #endregion

                    #region comContratoEmpresa.SelectedIndex = Convert.ToInt32(Dr["Contra_CLI"]);
                    try
                    {
                        comContratoEmpresa.SelectedIndex = Convert.ToInt32(Dr["Contra_CLI"]);
                    }
                    catch (Exception)
                    {
                        comContratoEmpresa.SelectedIndex = 2;
                        mtbContraInicio.Text = string.Empty;
                        mtbContraFim.Text = string.Empty;
                    }
                    #endregion
                    #region mtbContraInicio.Text = Dr["DtInic_CLI"].ToString();
                    if (Convert.ToDateTime(Dr["DtInic_CLI"]) == DateTime.MinValue)
                    {
                        mtbContraInicio.Text = string.Empty;
                    }
                    else
                    {
                        if (comContratoEmpresa.SelectedIndex < 2)
                        {
                            mtbContraInicio.Text = Dr["DtInic_CLI"].ToString();
                        }
                    }
                    #endregion
                    #region mtbContraFim.Text = Dr["DtFina_CLI"].ToString();
                    if (Convert.ToDateTime(Dr["DtFina_CLI"]) == DateTime.MinValue)
                    {
                        mtbContraFim.Text = string.Empty;
                    }
                    else
                    {
                        if (comContratoEmpresa.SelectedIndex < 2)
                        {
                            mtbContraFim.Text = Dr["DtFina_CLI"].ToString();
                        }
                    }
                    #endregion

                    txtCodigoAb2.Text = txtCodigo.Text;
                    txtDescriAb2.Text = txtDescri.Text;
                    mtbCpfCnpjAb2.Text = mtbCpfCnpj.Text;

                    txtEmailContato1.Text = Dr["Emai01_CLI"].ToString();
                    txtEmailContato2.Text = Dr["Emai02_CLI"].ToString();
                    txtSkype.Text = Dr["Skype1_CLI"].ToString();
                    txtMsn.Text = Dr["MsnCon_CLI"].ToString();
                    txtHomePage.Text = Dr["HomPag_CLI"].ToString();
                    txtFacebook.Text = Dr["Facebo_CLI"].ToString();

                    if (comTipoPFPJ.SelectedIndex == 0)
                    {
                        #region comSexo.SelectedIndex = Convert.ToInt32(Dr["SexoCl_CLI"]);
                        try
                        {
                            comSexo.SelectedIndex = Convert.ToInt32(Dr["SexoCl_CLI"]);
                        }
                        catch (Exception)
                        {
                            comSexo.SelectedIndex = 0;
                        }
                        #endregion
                        txtPfLocalDeTrabalho.Text = Dr["LocaTr_CLI"].ToString();
                        txtPfNaturalDe.Text = Dr["Natura_CLI"].ToString();
                        txtPfCargo.Text = Dr["CargCl_CLI"].ToString();
                        txtPfFiliacaoPai.Text = Dr["FilPai_CLI"].ToString();
                        txtPfFiliacaoMae.Text = Dr["FilMae_CLI"].ToString();
                        txtPfConjuge.Text = Dr["Conjug_CLI"].ToString();
                        mtbPfCpfConjuge.Text = Dr["CpfCon_CLI"].ToString();
                        #region mtbPfAdmissao.Text = Dr["DtAdmi_CLI"].ToString();
                        if (Convert.ToDateTime(Dr["DtAdmi_CLI"]) == DateTime.MinValue)
                        {
                            mtbPfAdmissao.Text = string.Empty;
                        }
                        else
                        {
                            mtbPfAdmissao.Text = Dr["DtAdmi_CLI"].ToString();
                        }
                        #endregion
                        #region mtbPfAniversario.Text = Dr["DtAniv_CLI"].ToString();
                        if (Convert.ToDateTime(Dr["DtAniv_CLI"]) == DateTime.MinValue)
                        {
                            mtbPfAniversario.Text = string.Empty;
                        }
                        else
                        {
                            mtbPfAniversario.Text = Dr["DtAniv_CLI"].ToString();
                        }
                        #endregion
                    }
                    else if (comTipoPFPJ.SelectedIndex == 1)
                    {
                        txtPjContatoProp1.Text = Dr["Conta1_CLI"].ToString();
                        txtPjContatoProp2.Text = Dr["Conta2_CLI"].ToString();
                        txtPjEscritorioContabilidade.Text = Dr["EscCon_CLI"].ToString();
                        #region txtPjDiaFatu.Text = Dr["DiaFat_CLI"].ToString().PadLeft(6, '0');
                        if (Convert.ToInt32(Dr["DiaFat_CLI"]) <= 0)
                        {
                            txtPjDiaFatu.Text = string.Empty;
                        }
                        else
                        {
                            txtPjDiaFatu.Text = Dr["DiaFat_CLI"].ToString().PadLeft(2, '0');
                            if (Convert.ToInt32(txtPjDiaFatu.Text) > 31)
                            {
                                txtPjDiaFatu.Text = "31";
                            }
                        }
                        #endregion
                        #region comPjAtividade.SelectedIndex = Convert.ToInt32(Dr["AtivEm_CLI"]);
                        try
                        {
                            comPjAtividade.SelectedIndex = Convert.ToInt32(Dr["AtivEm_CLI"]);
                        }
                        catch (Exception)
                        {
                            comPjAtividade.SelectedIndex = 7;
                        }
                        #endregion
                        #region comPjRegimeTrib.SelectedIndex = Convert.ToInt32(Dr["RegiEm_CLI"]);
                        try
                        {
                            comPjRegimeTrib.SelectedIndex = Convert.ToInt32(Dr["RegiEm_CLI"]);
                        }
                        catch (Exception)
                        {
                            comPjRegimeTrib.SelectedIndex = 0;
                        }
                        #endregion
                        txtPjCnae.Text = Dr["CnaeEm_CLI"].ToString();
                    }

                    #region comIcmsRdz.SelectedIndex = Convert.ToInt32(Dr["IcmsBc_CLI"]);
                    try
                    {
                        comIcmsRdz.SelectedIndex = Convert.ToInt32(Dr["IcmsBc_CLI"]);
                    }
                    catch (Exception)
                    {
                        comIcmsRdz.SelectedIndex = 1;
                    }
                    #endregion
                    #region comIcmsST.SelectedIndex = Convert.ToInt32(Dr["IcmsSu_CLI"]);
                    try
                    {
                        comIcmsST.SelectedIndex = Convert.ToInt32(Dr["IcmsSu_CLI"]);
                    }
                    catch (Exception)
                    {
                        comIcmsST.SelectedIndex = 1;
                    }
                    #endregion
                    #region txtCfop.Text = Dr["CfopCl_CLI"].ToString().PadLeft(4, '0');
                    if (Convert.ToInt32(Dr["CfopCl_CLI"]) <= 0)
                    {
                        txtCfop.Text = string.Empty;
                    }
                    else
                    {
                        txtCfop.Text = Dr["CfopCl_CLI"].ToString().PadLeft(4, '0');
                    }
                    #endregion
                    #region txtMsg1.Text = Dr["MsgNo1_CLI"].ToString().PadLeft(4, '0');
                    if (Convert.ToInt32(Dr["MsgNo1_CLI"]) <= 0)
                    {
                        txtMsg1.Text = string.Empty;
                    }
                    else
                    {
                        txtMsg1.Text = Dr["MsgNo1_CLI"].ToString().PadLeft(4, '0');
                    }
                    #endregion
                    #region txtMsg2.Text = Dr["MsgNo2_CLI"].ToString().PadLeft(4, '0');
                    if (Convert.ToInt32(Dr["MsgNo2_CLI"]) <= 0)
                    {
                        txtMsg2.Text = string.Empty;
                    }
                    else
                    {
                        txtMsg2.Text = Dr["MsgNo2_CLI"].ToString().PadLeft(4, '0');
                    }
                    #endregion
                    mtbDataCadastro.Text = Dr["DtCada_CLI"].ToString();
                    #region mtbRevisao.Text = Dr["DtFina_CLI"].ToString();
                    if (Convert.ToDateTime(Dr["DtRevi_CLI"]) == (Convert.ToDateTime(Dr["DtCada_CLI"])))
                    {
                        mtbRevisao.Text = string.Empty;
                    }
                    else
                    {
                        mtbRevisao.Text = Dr["DtRevi_CLI"].ToString();
                    }
                    #endregion

                    
                    #endregion

                    //SELECIONA AS FKs
                    TabClien_MET MET = new TabClien_MET();
                    MET.MET_SelecionaFK(txtVendedorCod, txtVendedorDesc, txtEndCidadePERSO, txtEndCidDescriPERSO, txtEndCidUFPERSO, txtTransportadoraCod, txtTransportadoraDesc, txtConvenioCod, txtConvenioDesc, txtCfop, txtCfopDesc, txtRotaCod, txtRotaSequen, txtCodigo);

                    btnGravar.Enabled = false;
                    btnInfFinaShow.Enabled = true;
                    btnInfComerShow.Enabled = true;
                    btnAvancar.Enabled = true;
                    btnGravarAb2.Enabled = false;
                    btnVoltar.Enabled = true;
                }
                else
                {
                    btnGravar.Enabled = false;
                    btnInfFinaShow.Enabled = true;
                    btnInfComerShow.Enabled = true;
                    btnAvancar.Enabled = true;
                    btnGravarAb2.Enabled = false;
                    btnVoltar.Enabled = true;
                    CamposDisable();

                    MessageBox.Show(Mensagem_De_Erro, "Consulta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (txtDescri.Text == string.Empty)
                    {
                        btnGravar.Enabled = false;
                        btnInfFinaShow.Enabled = false;
                        btnInfComerShow.Enabled = false;
                        btnAvancar.Enabled = false;
                        btnGravarAb2.Enabled = false;
                        btnVoltar.Enabled = false;
                    }
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método ExecSETAS()\n\nBLOCO = CONSULTA DE DADOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método ExecSETAS()\n\nBLOCO = CONSULTA DE DADOS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }
    }
}
