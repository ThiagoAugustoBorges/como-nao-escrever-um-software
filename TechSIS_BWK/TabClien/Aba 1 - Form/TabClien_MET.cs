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
    internal class TabClien_MET
    {
        //VALORES INICIAIS AO SE INCLUIR
        public void MET_ValoresIniciaisINC(string LojaLogada, string LojaLogadaDesc, TextBox txtEmpresaCod, TextBox txtEmpresaDesc, ComboBox comStatus, TextBox txtConceito, ComboBox comFrete, ComboBox comRecebimento, ComboBox comCondicao, ComboBox comTipoDeVenda, ComboBox comIcmsRdz, ComboBox comIcmsST, TextBox txtCfop, MaskedTextBox mtbDataCadastro, MaskedTextBox mtbRevisao, ComboBox comContratoEmpresa, TextBox txtCodigoPrin, TextBox txtCodigo, ComboBox comTipoPFPJ, ComboBox comSituacaoCredito, MaskedTextBox mtbDataVenciLimite, TextBox txtValorLimiteCre, ComboBox comPjAtividade, ComboBox comPjRegimeTrib)
        {
            comStatus.SelectedIndex = 1;
            txtEmpresaCod.Text = LojaLogada.PadLeft(6, '0');
            txtEmpresaDesc.Text = LojaLogadaDesc;
            txtConceito.Text = "A";
            mtbDataCadastro.Text = DateTime.Now.ToString("dd/MM/yyyy");
            mtbRevisao.Text = mtbDataCadastro.Text;
            comContratoEmpresa.SelectedIndex = 2;
            txtCodigoPrin.Text = txtCodigo.Text;
            comTipoPFPJ.SelectedIndex = 0;
            comSituacaoCredito.SelectedIndex = 0;
            comPjAtividade.SelectedIndex = 0;
            comPjRegimeTrib.SelectedIndex = 0;

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            //Define a string e o comando
            string StringComandoSELEÇÃO = ("SELECT FretTp_EMP,CondTp_EMP,ReceTp_EMP,TpVend_EMP,IcmdRz_EMP,IcmsSt_EMP,CfoSai_EMP,VrLiCr_EMP,DtLiCr_EMP FROM TabEmpre WHERE Sequen_EMP = @Sequen_EMP");
            SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
            ComandoSELEÇÃO.Parameters.Add("@Sequen_EMP", SqlDbType.Int).Value = LojaLogada;

            try
            {
                SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    comFrete.SelectedIndex = Convert.ToInt32(Dr["FretTp_EMP"]);
                    comCondicao.SelectedIndex = Convert.ToInt32(Dr["CondTp_EMP"]);
                    comRecebimento.SelectedIndex = Convert.ToInt32(Dr["ReceTp_EMP"]);
                    comTipoDeVenda.SelectedIndex = Convert.ToInt32(Dr["TpVend_EMP"]);
                    comIcmsRdz.SelectedIndex = Convert.ToInt32(Dr["IcmdRz_EMP"]);
                    comIcmsST.SelectedIndex = Convert.ToInt32(Dr["IcmsSt_EMP"]);
                    txtCfop.Text = Dr["CfoSai_EMP"].ToString();

                    if (Convert.ToDecimal(Dr["VrLiCr_EMP"]) > 0)
                    {
                        txtValorLimiteCre.Text = Convert.ToDecimal(Dr["VrLiCr_EMP"]).ToString("0000.00");
                        int AddMeses = Convert.ToInt32(Dr["DtLiCr_EMP"]);
                        mtbDataVenciLimite.Text = DateTime.Now.AddMonths(AddMeses).ToString("dd/MM/yyyy");
                    }
                }
                else
                {
                    comFrete.SelectedIndex = 0;
                    comCondicao.SelectedIndex = 0;
                    comRecebimento.SelectedIndex = 0;
                    comTipoDeVenda.SelectedIndex = 0;
                    comIcmsRdz.SelectedIndex = 0;
                    comIcmsST.SelectedIndex = 0;
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_ValoresIniciaisINC()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentOutOfRangeException)
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_ValoresIniciaisINC()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //BUSCA AS INFORMAÇÕES AO DIGITAR TAB NO CAMPO CÓDIGO
        public void MET_SelecionaCodigoTAB(TextBox txtCodigo, TextBox txtMESTRE, Panel panUpAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, MethodInvoker CamposEnable, Button btnGravar, Button btnInfFinaShow, Button btnInfComerShow, Button btnAvancar, Button btnGravarAb2, Button btnVoltar, TextBox txtDescri, TextBox txtCodigoPrin, ComboBox comTipoPFPJ, MaskedTextBox mtbCpfCnpj, ComboBox comStatus, TextBox txtFantasia, MaskedTextBox mtbTelPabx, MaskedTextBox mtbTelCel1, ComboBox comCategoria, TextBox txtObservacao, MaskedTextBox mtbTelFax, MaskedTextBox mtbTelCel2, TextBox txtInscricaoEstadual, MaskedTextBox mtbVencEst, TextBox txtRotaCod, TextBox txtVendedorCod, TextBox txtVendedorDesc, TextBox txtDesconto, TextBox txtBanco, TextBox txtAgencia, TextBox txtInscricaoMunicipal, MaskedTextBox mtbVencMun, TextBox txtReferencialCod, TextBox txtEmpresaCod, TextBox txtEmpresaDesc, TextBox txtRgIdentidade, TextBox txtConceito, TextBox txtEndLogradouroFATU, TextBox txtEndNumeroFATU, TextBox txtEndCidadeFATU, TextBox txtEndCidDescriFATU, TextBox txtEndCidUFFATU, MaskedTextBox mtbEndCepFATU, TextBox txtEndBairroFATU, TextBox txtEndCompleFATU, TextBox txtLabel, TextBox txtEndLogradouroPERSO, TextBox txtEndNumeroPERSO, TextBox txtEndCidadePERSO, TextBox txtEndCidDescriPERSO, TextBox txtEndCidUFPERSO, MaskedTextBox mtbEndCepPERSO, TextBox txtEndBairroPERSO, TextBox txtEndComplePERSO, TextBox txtTransportadoraCod, TextBox txtTransportadoraDesc, TextBox txtConvenioCod, TextBox txtConvenioDesc, TextBox txtValorLimiteCre, MaskedTextBox mtbDataVenciLimite, ComboBox comSituacaoCredito, ComboBox comFrete, ComboBox comRecebimento, ComboBox comCondicao, ComboBox comTipoDeVenda, ComboBox comContratoEmpresa, MaskedTextBox mtbContraInicio, MaskedTextBox mtbContraFim, TextBox txtCodigoAb2, TextBox txtDescriAb2, MaskedTextBox mtbCpfCnpjAb2, TextBox txtEmailContato1, TextBox txtEmailContato2, TextBox txtSkype, TextBox txtMsn, TextBox txtHomePage, TextBox txtFacebook, ComboBox comSexo, TextBox txtPfLocalDeTrabalho, TextBox txtPfNaturalDe, TextBox txtPfCargo, MaskedTextBox mtbPfAdmissao, MaskedTextBox mtbPfAniversario, TextBox txtPfFiliacaoPai, TextBox txtPfFiliacaoMae, TextBox txtPfConjuge, MaskedTextBox mtbPfCpfConjuge, TextBox txtPjContatoProp1, TextBox txtPjContatoProp2, TextBox txtPjEscritorioContabilidade, TextBox txtPjDiaFatu, ComboBox comPjAtividade, ComboBox comPjRegimeTrib, TextBox txtPjCnae, ComboBox comIcmsRdz, ComboBox comIcmsST, TextBox txtCfop, TextBox txtCfopDesc, TextBox txtMsg1, TextBox txtMsg2, MaskedTextBox mtbDataCadastro, MaskedTextBox mtbRevisao, string LojaLogada, string LojaLogadaDesc, TextBox txtRotaSequen)
        {
            #region TRATAMENTO txtCodigo em BRANCO e > QUE 999999
            if (txtCodigo.Text == string.Empty)
            {
                txtCodigo.Text = "000000";
            }
            if (Convert.ToInt32(txtCodigo.Text) == 0)
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');
                panUpAb1.Focus();
                txtCodigo.SelectAll();
                ZerarCampos();
                CamposDisable();
                btnGravar.Enabled = false;
                MessageBox.Show("Campo (Código) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Convert.ToInt32(txtCodigo.Text) == 999999)
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');
                panUpAb1.Focus();
                txtCodigo.SelectAll();
                ZerarCampos();
                CamposDisable();
                btnGravar.Enabled = false;
                MessageBox.Show("LIMITE DE CADASTROS DE CLIENTES ATINGIDO", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');
            }
            #endregion

            #region INCLUIR
            if (txtMESTRE.Text == "INCLUIR")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                //Define a string e o comando
                string StringComandoSELEÇÃO = ("SELECT * FROM TabClien INNER JOIN TabCidad ON TabClien.EndCi1_CLI = TabCidad.Sequen_CID INNER JOIN TabEmpre ON TabClien.EmpSeq_CLI = TabEmpre.Sequen_EMP WHERE Sequen_CLI = @Sequen_CLI");
                SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                ComandoSELEÇÃO.Parameters.Add("@Sequen_CLI", SqlDbType.VarChar).Value = txtCodigo.Text;

                try
                {
                    SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        #region TRATAMENTO CLIENTE EXCLUIDO
                        if (Dr["Status_CLI"].ToString() == "3")
                        {
                            panUpAb1.Focus();
                            txtCodigo.SelectAll();
                            ZerarCampos();
                            CamposDisable();

                            btnInfFinaShow.Enabled = false;
                            btnInfComerShow.Enabled = false;
                            btnAvancar.Enabled = false;
                            btnGravar.Enabled = false;
                            btnGravarAb2.Enabled = false;
                            btnVoltar.Enabled = false;

                            MessageBox.Show("Cliente consta como EXCLUIDO. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        #endregion
                        //SE ELE NÃO FOR EXCLUIDO \/
                        else
                        {
                            ZerarCampos();


                            #region PARAMETROS DR[]
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

                            //SELECIONA AS FKs
                            MET_SelecionaFK(txtVendedorCod, txtVendedorDesc, txtEndCidadePERSO, txtEndCidDescriPERSO, txtEndCidUFPERSO, txtTransportadoraCod, txtTransportadoraDesc, txtConvenioCod, txtConvenioDesc, txtCfop, txtCfopDesc, txtRotaCod, txtRotaSequen, txtCodigo);

                            #endregion

                            #region TRATAMENTO PARA CLIENTE JÁ EXISTENTE
                            panUpAb1.Focus();
                            txtCodigo.SelectAll();
                            CamposDisable();

                            btnInfFinaShow.Enabled = true;
                            btnInfComerShow.Enabled = true;
                            btnAvancar.Enabled = true;
                            btnVoltar.Enabled = true;
                            btnGravar.Enabled = false;
                            btnGravarAb2.Enabled = false;

                            MessageBox.Show("Registro já cadastrado no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            #endregion
                        }
                    }
                    else
                    {
                        ZerarCampos();

                        //DEFINE OS VALORES INICIAS
                        MET_ValoresIniciaisINC(LojaLogada, LojaLogadaDesc, txtEmpresaCod, txtEmpresaDesc, comStatus, txtConceito, comFrete, comRecebimento, comCondicao, comTipoDeVenda, comIcmsRdz, comIcmsST, txtCfop, mtbDataCadastro, mtbRevisao, comContratoEmpresa, txtCodigoPrin, txtCodigo, comTipoPFPJ, comSituacaoCredito, mtbDataVenciLimite, txtValorLimiteCre, comPjAtividade, comPjRegimeTrib);

                        CamposEnable();

                        btnInfFinaShow.Enabled = false;
                        btnInfComerShow.Enabled = false;
                        btnAvancar.Enabled = true;
                        btnGravar.Enabled = true;
                        btnGravarAb2.Enabled = true;
                        btnVoltar.Enabled = true;
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: TabClien_MET = INCLUIR\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: TabClien_MET = INCLUIR\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
            #region OUTROS
            if (txtMESTRE.Text != "INCLUIR")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                //Define a string e o comando
                string StringComandoSELEÇÃO = ("SELECT * FROM TabClien INNER JOIN TabCidad ON TabClien.EndCi1_CLI = TabCidad.Sequen_CID INNER JOIN TabEmpre ON TabClien.EmpSeq_CLI = TabEmpre.Sequen_EMP WHERE Sequen_CLI = @Sequen_CLI");
                SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);
                ComandoSELEÇÃO.Parameters.Add("@Sequen_CLI", SqlDbType.VarChar).Value = txtCodigo.Text;

                try
                {
                    SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        #region TRATAMENTO CLIENTE EXCLUIDO
                        if (Dr["Status_CLI"].ToString() == "3")
                        {
                            panUpAb1.Focus();
                            txtCodigo.SelectAll();
                            ZerarCampos();
                            CamposDisable();

                            btnInfFinaShow.Enabled = false;
                            btnInfComerShow.Enabled = false;
                            btnAvancar.Enabled = false;
                            btnGravar.Enabled = false;
                            btnGravarAb2.Enabled = false;
                            btnVoltar.Enabled = false;

                            MessageBox.Show("Cliente consta como EXCLUIDO. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        #endregion
                        //SE ELE NÃO FOR EXCLUIDO \/
                        else
                        {
                            ZerarCampos();


                            #region PARAMETROS DR[]
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

                            //SELECIONA AS FKs
                            MET_SelecionaFK(txtVendedorCod, txtVendedorDesc, txtEndCidadePERSO, txtEndCidDescriPERSO, txtEndCidUFPERSO, txtTransportadoraCod, txtTransportadoraDesc, txtConvenioCod, txtConvenioDesc, txtCfop, txtCfopDesc, txtRotaCod, txtRotaSequen, txtCodigo);

                            #endregion

                            if (txtMESTRE.Text == "SELECT" || txtMESTRE.Text == "CONSULTA")
                            {
                                CamposDisable();
                                btnInfFinaShow.Enabled = true;
                                btnInfComerShow.Enabled = true;
                                btnAvancar.Enabled = true;
                                btnGravar.Enabled = false;
                                btnGravarAb2.Enabled = false;
                                btnVoltar.Enabled = true;
                                panUpAb1.Focus();
                                txtCodigo.SelectAll();
                            }
                            else
                            {
                                CamposEnable();

                                btnInfFinaShow.Enabled = true;
                                btnInfComerShow.Enabled = true;
                                btnAvancar.Enabled = true;
                                btnGravar.Enabled = true;
                                btnGravarAb2.Enabled = true;
                                btnVoltar.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        ZerarCampos();
                        CamposDisable();

                        btnInfFinaShow.Enabled = false;
                        btnInfComerShow.Enabled = false;
                        btnAvancar.Enabled = false;
                        btnGravar.Enabled = false;
                        btnGravarAb2.Enabled = false;
                        btnVoltar.Enabled = false;

                        panUpAb1.Focus();
                        txtCodigo.SelectAll();

                        MessageBox.Show("Registro inexistente no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: TabClien_MET != INCLUIR\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodigoTAB()\n\nBLOCO.: TabClien_MET != INCLUIR\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
            #endregion
        }

        //SELECIONA O ULTIMO REGISTRO NO BANCO E ADICIONA MAIS 1
        public void MET_SelecionaUltimoRegistroMaisUm(TextBox txtCodigo, Button btnGravar)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT MAX (Sequen_CLI + 1) as Sequen_CLI FROM TabClien";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string Codigo = Dr["Sequen_CLI"].ToString();
                    if (Codigo == string.Empty)
                    {
                        txtCodigo.Text = "000001";
                    }
                    else
                    {
                        txtCodigo.Text = Codigo.PadLeft(6, '0');
                    }

                    txtCodigo.SelectAll();
                    Dr.Close();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUltimoRegistroMaisUm()\n\nBLOCO.: TabClien_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaUltimoRegistroMaisUm()\n\nBLOCO.: TabClien_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGravar.Enabled = false;
            }
            finally
            {
                Conexão.Close();
            }
        }

        //DEFINE QUAL CAMPO PODE RECEBER APENAS NÚMEROS
        public void MET_ApenasNúmeros(KeyPressEventArgs KeyPress)
        {
            if (!Char.IsDigit(KeyPress.KeyChar) && KeyPress.KeyChar != (char)8)
            {
                KeyPress.Handled = true;
                return;
            }
        }

        //DEFINE QUAL CAMPO PODE RECEBER APENAS NÚMEROS DECIMAIS
        public void MET_ApenasNúmerosDec(KeyPressEventArgs KeyPress, Control Texto)
        {
            if (!Char.IsDigit(KeyPress.KeyChar) && KeyPress.KeyChar != (Char)Keys.Back && KeyPress.KeyChar != ',')
            {
                KeyPress.Handled = true;
                return;
            }
            //pega a posição da virgula, caso ela exista:
            int posSeparator = Texto.Text.IndexOf(',');
            //se a tecla digitada for virgula e ela já existir, barra:
            if (KeyPress.KeyChar == ',' && posSeparator > -1)
            {
                KeyPress.Handled = true;
                return;
            }
        }

        //SELECIONA OS ITEMS COMO TRANSPORTADOR, CONVENIO ETC.. NO TAB
        #region SELEÇÃO DOS ITEMS NO TAB
        //SELECIONA ACIDADE NO TAB
        public void MET_SelecionaCidadeTAB(TextBox CodigoCidade, TextBox DescriCidade, TextBox UFCidade, Control Retorno)
        {
            CodigoCidade.Text = CodigoCidade.Text.PadLeft(6, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Descri_CID,UfFede_CID,Status_CID FROM TabCidad WHERE Sequen_CID = @Sequen_CID";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen_CID", SqlDbType.Int).Value = CodigoCidade.Text;


            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    #region TRATAMENTO STATUS
                    if (Convert.ToInt32(Dr["Status_CID"]) == 3)
                    {
                        MessageBox.Show("Cidade consta como EXCLUIDA. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DescriCidade.Text = string.Empty;
                        UFCidade.Text = string.Empty;
                        Retorno.Focus(); CodigoCidade.SelectAll();
                    }
                    else if (Convert.ToInt32(Dr["Status_CID"]) == 2)
                    {
                        DialogResult CONTINUA = MessageBox.Show("Cidade consta como INATIVA. Deseja Continuar?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (CONTINUA == DialogResult.Yes)
                        {
                            DescriCidade.Text = Dr["Descri_CID"].ToString();
                            UFCidade.Text = Dr["UfFede_CID"].ToString();
                        }
                        else
                        {
                            DescriCidade.Text = string.Empty;
                            UFCidade.Text = string.Empty;
                            Retorno.Focus(); CodigoCidade.SelectAll();
                        }
                    }
                    #endregion
                    else
                    {
                        DescriCidade.Text = Dr["Descri_CID"].ToString();
                        UFCidade.Text = Dr["UfFede_CID"].ToString();
                    }
                }
                else
                {
                    DescriCidade.Text = string.Empty;
                    UFCidade.Text = string.Empty;
                    MessageBox.Show("Cidade não encontrada no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Retorno.Focus(); CodigoCidade.SelectAll();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCidadeTab()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCidadeTab()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //SELECIONA A EMPRESA
        public void MET_SelecionaEmpresTAB(TextBox txtEmpresaCod, TextBox txtEmpresaDesc, Control Retorno)
        {
            txtEmpresaCod.Text = txtEmpresaCod.Text.PadLeft(6, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Descri_EMP FROM TabEmpre WHERE Sequen_EMP = @Sequen_EMP";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen_EMP", SqlDbType.Int).Value = txtEmpresaCod.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    txtEmpresaDesc.Text = Dr["Descri_EMP"].ToString();
                }
                else
                {
                    txtEmpresaDesc.Text = string.Empty;
                    MessageBox.Show("Empresa não encontrada no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Retorno.Focus(); txtEmpresaCod.SelectAll();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaEmpresTAB()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaEmpresTAB()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //SELECIONA O CLIENTE
        public void MET_SelecionaClientTAB(TextBox txtCodigo, TextBox CodigoCliente, Control Retorno)
        {
            CodigoCliente.Text = CodigoCliente.Text.PadLeft(6, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Descri_CLI,Status_CLI FROM TabClien WHERE Sequen_CLI = @Sequen_CLI";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen_CLI", SqlDbType.Int).Value = CodigoCliente.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows || CodigoCliente.Text == txtCodigo.Text)
                {
                    #region TRATAMENTO STATUS
                    if (Dr.HasRows)
                    {
                        if (Convert.ToInt32(Dr["Status_CLI"]) == 3)
                        {
                            MessageBox.Show("Cliente consta como EXCLUIDO. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Retorno.Focus(); CodigoCliente.SelectAll();
                        }
                        else if (Convert.ToInt32(Dr["Status_CLI"]) == 2)
                        {
                            DialogResult CONTINUA = MessageBox.Show("Cliente consta como INATIVO. Deseja Continuar?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (CONTINUA == DialogResult.Yes)
                            {

                            }
                            else
                            {
                                Retorno.Focus(); CodigoCliente.SelectAll();
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("Cliente não encontrada no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Retorno.Focus(); CodigoCliente.SelectAll();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaClientTAB()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaClientTAB()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //SELECIONA O VENDEDOR
        public void MET_SelecionaVendedTAB(TextBox txtVendedorCod, TextBox txtVendedorDesc, Control Retorno)
        {
            txtVendedorCod.Text = txtVendedorCod.Text.PadLeft(6, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Descri_VEN, Status_VEN FROM TabVende WHERE Sequen_VEN = @Sequen_VEN";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen_VEN", SqlDbType.Int).Value = txtVendedorCod.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    #region TRATAMENTO STATUS
                    if (Convert.ToInt32(Dr["Status_VEN"]) == 3)
                    {
                        MessageBox.Show("Vendedor consta como EXCLUIDO. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtVendedorDesc.Text = string.Empty;
                        Retorno.Focus(); txtVendedorCod.SelectAll();
                    }
                    else if (Convert.ToInt32(Dr["Status_VEN"]) == 2)
                    {
                        DialogResult CONTINUA = MessageBox.Show("Vendedor consta como INATIVO. Deseja Continuar?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (CONTINUA == DialogResult.Yes)
                        {
                            txtVendedorDesc.Text = Dr["Descri_VEN"].ToString();
                        }
                        else
                        {
                            txtVendedorDesc.Text = string.Empty;
                            Retorno.Focus(); txtVendedorCod.SelectAll();
                        }
                    }
                    #endregion
                    else
                    {
                        txtVendedorDesc.Text = Dr["Descri_VEN"].ToString();
                    }
                }
                else
                {
                    txtVendedorDesc.Text = string.Empty;
                    MessageBox.Show("Vendedor não encontrada no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Retorno.Focus(); txtVendedorCod.SelectAll();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaVendedTAB()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaVendedTAB()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //SELECIONA A TRANSPORTADORA
        public void MET_SelecionaTranspTAB(TextBox txtTransportadoraCod, TextBox txtTransportadoraDesc, Control Retorno)
        {
            txtTransportadoraCod.Text = txtTransportadoraCod.Text.PadLeft(6, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Descri_TRA, Status_TRA FROM TabTrans WHERE Sequen_TRA = @Sequen_TRA";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen_TRA", SqlDbType.Int).Value = txtTransportadoraCod.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    #region TRATAMENTO STATUS
                    if (Convert.ToInt32(Dr["Status_TRA"]) == 3)
                    {
                        MessageBox.Show("Transportadora consta como EXCLUIDA. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTransportadoraDesc.Text = string.Empty;
                        Retorno.Focus(); txtTransportadoraCod.SelectAll();
                    }
                    else if (Convert.ToInt32(Dr["Status_TRA"]) == 2)
                    {
                        DialogResult CONTINUA = MessageBox.Show("Transportadora consta como INATIVA. Deseja Continuar?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (CONTINUA == DialogResult.Yes)
                        {
                            txtTransportadoraDesc.Text = Dr["Descri_TRA"].ToString();
                        }
                        else
                        {
                            txtTransportadoraDesc.Text = string.Empty;
                            Retorno.Focus(); txtTransportadoraCod.SelectAll();
                        }
                    }
                    #endregion
                    else
                    {
                        txtTransportadoraDesc.Text = Dr["Descri_TRA"].ToString();
                    }
                }
                else
                {
                    txtTransportadoraDesc.Text = string.Empty;
                    MessageBox.Show("Transportadora não encontrada no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Retorno.Focus(); txtTransportadoraCod.SelectAll();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaTranspTAB()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaTranspTAB()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //SELECIONA O CONVENIO
        public void MET_SelecionaConvenTAB(TextBox txtConvenioCod, TextBox txtConvenioDesc, Control Retorno, ComboBox comRecebimento, ComboBox comCondicao, TextBox txtMESTRE)
        {
            txtConvenioCod.Text = txtConvenioCod.Text.PadLeft(6, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Descri_COV,Tipo01_COV,Status_COV FROM TabConve WHERE Sequen_COV = @Sequen_COV";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen_COV", SqlDbType.Int).Value = txtConvenioCod.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    #region TRATAMENTO STATUS
                    if (Convert.ToInt32(Dr["Status_COV"]) == 3)
                    {
                        MessageBox.Show("Convênio consta como EXCLUIDO. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtConvenioDesc.Text = string.Empty;
                        if (txtMESTRE.Text == "INCLUIR")
                        {
                            comCondicao.SelectedIndex = 0;
                            comRecebimento.SelectedIndex = 0;
                        }
                        Retorno.Focus(); txtConvenioCod.SelectAll();
                    }
                    else if (Convert.ToInt32(Dr["Status_COV"]) == 2)
                    {
                        DialogResult CONTINUA = MessageBox.Show("Convênio consta como INATIVO. Deseja Continuar?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (CONTINUA == DialogResult.Yes)
                        {
                            txtConvenioDesc.Text = Dr["Descri_COV"].ToString();
                            if (txtMESTRE.Text == "INCLUIR")
                            {
                                comCondicao.SelectedIndex = 5;
                                comRecebimento.SelectedIndex = 7;
                            }
                        }
                        else
                        {
                            txtConvenioDesc.Text = string.Empty;
                            if (txtMESTRE.Text == "INCLUIR")
                            {
                                comCondicao.SelectedIndex = 0;
                                comRecebimento.SelectedIndex = 0;
                            }
                            Retorno.Focus(); txtConvenioCod.SelectAll();
                        }
                    }
                    #endregion
                    else
                    {
                        txtConvenioDesc.Text = Dr["Descri_COV"].ToString();
                        int TIPO = Convert.ToInt32(Dr["Tipo01_COV"]);
                        if (txtMESTRE.Text == "INCLUIR")
                        {
                            #region TRATAMENTO CONVENIO
                            if (TIPO == 2)
                            {
                                comRecebimento.SelectedIndex = 5;
                            }
                            else if (TIPO == 3)
                            {
                                comRecebimento.SelectedIndex = 6;
                            }
                            else
                            {
                                comRecebimento.SelectedIndex = 7;
                            }
                            #endregion
                            comCondicao.SelectedIndex = 5;

                        }
                    }
                }
                else
                {
                    txtConvenioDesc.Text = string.Empty;
                    if (txtMESTRE.Text == "INCLUIR")
                    {
                        comCondicao.SelectedIndex = 0;
                        comRecebimento.SelectedIndex = 0;
                    }
                    MessageBox.Show("Convênio não encontrada no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Retorno.Focus(); txtConvenioCod.SelectAll();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaConvenTAB()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaConvenTAB()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //SELECIONA O CFOP
        public void MET_SelecionaCodFOPTAB(TextBox txtCfop, TextBox txtCfopDesc, Control Retorno)
        {
            txtCfop.Text = txtCfop.Text.PadRight(4, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Descri_CFO FROM TabCfope WHERE Sequen_CFO = @Sequen_CFO";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen_CFO", SqlDbType.Int).Value = txtCfop.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    txtCfopDesc.Text = Dr["Descri_CFO"].ToString();
                }
                else
                {
                    txtCfopDesc.Text = string.Empty;
                    MessageBox.Show("Código Fiscal de Operação não encontrado", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Retorno.Focus(); txtCfop.SelectAll();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodFOPTAB()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaCodFOPTAB()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //SELECIONA A MENSAGEM DA NOTA
        public void MET_SelecionaMensagTAB(TextBox CodigoMensa, Control Retorno)
        {
            CodigoMensa.Text = CodigoMensa.Text.PadLeft(4, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Descri_MSG FROM TabMsgNt WHERE Sequen_MSG = @Sequen_MSG";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen_MSG", SqlDbType.Int).Value = CodigoMensa.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {

                }
                else
                {
                    MessageBox.Show("Mensagem para nota não encontrada no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Retorno.Focus(); CodigoMensa.SelectAll();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaMensagTAB()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaMensagTAB()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //SELECIONA A ROTA E PROCURA O NÚMERO DA ORDEM DA ROTA
        public void MET_SelecionaRotasTAB(TextBox txtRotaCod, Control Retorno, TextBox txtMESTRE, TextBox txtRotaSequen, TextBox txtCodigo)
        {
            txtRotaCod.Text = txtRotaCod.Text.PadLeft(6, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Sequen_ROT, Status_ROT FROM TabRotas WHERE Sequen_ROT = @Sequen_ROT";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen_ROT", SqlDbType.Int).Value = txtRotaCod.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    #region TRATAMENTO STATUS
                    if (Convert.ToInt32(Dr["Status_ROT"]) == 3)
                    {
                        txtRotaSequen.Text = string.Empty;
                        MessageBox.Show("Rota consta como EXCLUIDA. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Retorno.Focus(); txtRotaCod.SelectAll();
                    }
                    else if (Convert.ToInt32(Dr["Status_ROT"]) == 2)
                    {
                        DialogResult CONTINUA = MessageBox.Show("Rota consta como INATIVA. Deseja Continuar?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (CONTINUA == DialogResult.Yes)
                        {
                            MET_SelectUltimaOrdemRota(txtMESTRE, txtRotaSequen, txtRotaCod, txtCodigo);
                        }
                        else
                        {
                            txtRotaSequen.Text = string.Empty;
                            Retorno.Focus(); txtRotaCod.SelectAll();
                        }
                    }
                    else
                    {
                        MET_SelectUltimaOrdemRota(txtMESTRE, txtRotaSequen, txtRotaCod, txtCodigo);
                    }
                    #endregion
                }
                else
                {
                    txtRotaSequen.Text = string.Empty;
                    MessageBox.Show("Rota não encontrada no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Retorno.Focus(); txtRotaCod.SelectAll();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaRotasTAB()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaRotasTAB()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //SELECIONA O CLIENTE NO LEAVE
        public void MET_SelecionaClientLEAVE(TextBox txtCodigo, TextBox CodigoCliente, Control Retorno)
        {
            CodigoCliente.Text = CodigoCliente.Text.PadLeft(6, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Descri_CLI,Status_CLI FROM TabClien WHERE Sequen_CLI = @Sequen_CLI";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen_CLI", SqlDbType.Int).Value = CodigoCliente.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows || CodigoCliente.Text == txtCodigo.Text)
                {

                }
                else
                {
                    CodigoCliente.Text = txtCodigo.Text;
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaClientLEAVE()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaClientLEAVE()\n\nBLOCO.: CLASSE TabClien_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }
        #endregion

        //SELECIONA AS FKS
        public void MET_SelecionaFK(TextBox txtVendedorCod, TextBox txtVendedorDesc, TextBox txtEndCidadePERSO, TextBox txtEndCidDescriPERSO, TextBox txtEndCidUFPERSO, TextBox txtTransportadoraCod, TextBox txtTransportadoraDesc, TextBox txtConvenioCod, TextBox txtConvenioDesc, TextBox txtCfop, TextBox txtCfopDesc, TextBox txtRotaCod, TextBox txtRotaSequen, TextBox txtCodigo)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão1 = new SqlConnection(LerString);
            SqlConnection Conexão2 = new SqlConnection(LerString);
            SqlConnection Conexão3 = new SqlConnection(LerString);
            SqlConnection Conexão4 = new SqlConnection(LerString);
            SqlConnection Conexão5 = new SqlConnection(LerString);
            SqlConnection Conexão6 = new SqlConnection(LerString);

            Conexão1.Open();
            Conexão2.Open();
            Conexão3.Open();
            Conexão4.Open();
            Conexão5.Open();
            Conexão6.Open();


            try
            {

                string SELEC_VENDEDOR = "SELECT Descri_VEN FROM TabVende WHERE Sequen_VEN = @Sequen_VEN";
                SqlCommand Comando_VENDE = new SqlCommand(SELEC_VENDEDOR, Conexão1);
                Comando_VENDE.Parameters.Add("@Sequen_VEN", SqlDbType.Int).Value = txtVendedorCod.Text;

                if (txtVendedorCod.Text != string.Empty)
                {
                    SqlDataReader Dr = Comando_VENDE.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        txtVendedorDesc.Text = Dr["Descri_VEN"].ToString();
                        Dr.Close();
                    }
                    else
                    {
                        txtVendedorCod.Text = string.Empty;
                        txtVendedorDesc.Text = string.Empty;
                    }
                }



                string SELEC_CIDADE = "SELECT Descri_CID,UfFede_CID FROM TabCidad WHERE Sequen_CID = @Sequen_CID";
                SqlCommand Comando_CIDADE = new SqlCommand(SELEC_CIDADE, Conexão2);
                Comando_CIDADE.Parameters.Add("@Sequen_CID", SqlDbType.Int).Value = txtEndCidadePERSO.Text;

                if (txtEndCidadePERSO.Text != string.Empty)
                {
                    SqlDataReader Dr = Comando_CIDADE.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        txtEndCidDescriPERSO.Text = Dr["Descri_CID"].ToString();
                        txtEndCidUFPERSO.Text = Dr["UfFede_CID"].ToString();
                        Dr.Close();
                    }
                    else
                    {
                        txtEndCidadePERSO.Text = string.Empty;
                        txtEndCidDescriPERSO.Text = string.Empty;
                        txtEndCidUFPERSO.Text = string.Empty;
                    }
                }



                string SELEC_TRASPORTADOR = "SELECT Descri_TRA FROM TabTrans WHERE Sequen_TRA = @Sequen_TRA";
                SqlCommand Comando_TRANSPORTADOR = new SqlCommand(SELEC_TRASPORTADOR, Conexão3);
                Comando_TRANSPORTADOR.Parameters.Add("@Sequen_TRA", SqlDbType.Int).Value = txtTransportadoraCod.Text;

                if (txtTransportadoraCod.Text != string.Empty)
                {
                    SqlDataReader Dr = Comando_TRANSPORTADOR.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        txtTransportadoraDesc.Text = Dr["Descri_TRA"].ToString();
                        Dr.Close();
                    }
                    else
                    {
                        txtTransportadoraCod.Text = string.Empty;
                        txtTransportadoraDesc.Text = string.Empty;
                    }
                }




                string SELEC_CONVENIO = "SELECT Descri_COV FROM TabConve WHERE Sequen_COV = @Sequen_COV";
                SqlCommand Comando_CONVENIO = new SqlCommand(SELEC_CONVENIO, Conexão4);
                Comando_CONVENIO.Parameters.Add("@Sequen_COV", SqlDbType.Int).Value = txtConvenioCod.Text;

                if (txtConvenioCod.Text != string.Empty)
                {
                    SqlDataReader Dr = Comando_CONVENIO.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        txtConvenioDesc.Text = Dr["Descri_COV"].ToString();
                        Dr.Close();
                    }
                    else
                    {
                        txtConvenioCod.Text = string.Empty;
                        txtConvenioDesc.Text = string.Empty;
                    }
                }




                string SELEC_CFOP = "SELECT Descri_CFO FROM TabCfope WHERE Sequen_CFO = @Sequen_CFO";
                SqlCommand Comando_CFOP = new SqlCommand(SELEC_CFOP, Conexão5);
                Comando_CFOP.Parameters.Add("@Sequen_CFO", SqlDbType.Int).Value = txtCfop.Text;

                if (txtCfop.Text != string.Empty)
                {
                    SqlDataReader Dr = Comando_CFOP.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        txtCfopDesc.Text = Dr["Descri_CFO"].ToString();
                        Dr.Close();
                    }
                    else
                    {
                        txtCfop.Text = string.Empty;
                        txtCfopDesc.Text = string.Empty;
                    }
                }


                string SELEC_ORDEM = "SELECT Ordems_RO1 FROM TabRot01 WHERE SeqCli_RO1 = @SeqCli_RO1 AND Sequen_RO1 = @Sequen_RO1";
                SqlCommand Comando_ORDEM = new SqlCommand(SELEC_ORDEM, Conexão6);
                Comando_ORDEM.Parameters.Add("@SeqCli_RO1", SqlDbType.Int).Value = txtCodigo.Text;
                Comando_ORDEM.Parameters.Add("@Sequen_RO1", SqlDbType.Int).Value = txtRotaCod.Text;

                if (txtRotaCod.Text != string.Empty)
                {
                    SqlDataReader Dr = Comando_ORDEM.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        txtRotaSequen.Text = Dr["Ordems_RO1"].ToString().PadLeft(4, '0');
                        Dr.Close();
                    }
                    else
                    {
                        txtRotaSequen.Text = string.Empty;
                    }
                }
                else
                {
                    txtRotaSequen.Text = string.Empty;
                }




            }

            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaFK()\n\nBLOCO.: TabClien_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_SelecionaFK()\n\nBLOCO.: TabClien_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão1.Close();
                Conexão2.Close();
                Conexão3.Close();
                Conexão4.Close();
                Conexão5.Close();
                Conexão6.Close();
            }
        }

        //VERIFICA SE A DATA É VALIDA
        public bool MET_VerificaDATA(MaskedTextBox CampoAvaliado, Control Retorno)
        {
            if (CampoAvaliado.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
            {
                return false;
            }
            else
            {
                try
                {
                    string MeuTEXTO = CampoAvaliado.Text;
                    string MeuAno = DateTime.Now.Year.ToString();
                    if (CampoAvaliado.Text.Length <= 6)
                    {
                        CampoAvaliado.Text = MeuTEXTO + MeuAno;
                    }
                    Convert.ToDateTime(CampoAvaliado.Text);
                    return false;
                }
                catch (Exception)
                {
                    MessageBox.Show("DATA INFORMADA É INVÁLIDA..VERIFIQUE!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Retorno.Select();
                    SendKeys.Send("{HOME}");
                    SendKeys.Send("+{END}");
                    return true;
                }
            }
        }

        //VALIDA O CPF CNPJ
        public bool MET_ValidaCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
        public bool MET_ValidaCNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        //VERIFICA SE CLIENTE JÁ É EXISTENTE PELO SEU CPF CNPJ
        public void MET_VerificaClientExistente(MaskedTextBox mtbCpfCnpj, Control Retorno, TextBox txtMESTRE)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);

            Conexão.Open();

            string strComando = "SELECT Sequen_CLI,Descri_CLI FROM TabClien WHERE CpfCnp_CLI = @CpfCnp_CLI";
            SqlCommand Comando = new SqlCommand(strComando, Conexão);
            Comando.Parameters.Add("@CpfCnp_CLI", SqlDbType.VarChar).Value = mtbCpfCnpj.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    if (txtMESTRE.Text == "INCLUIR")
                    {
                        DialogResult CONTINUA = MessageBox.Show("CLIENTE EXISTENTE NO BANCO DE DADOS! CÓDIGO (" + Dr["Sequen_CLI"].ToString().PadLeft(6, '0') + ") \n     DESEJA CONTINUAR COM O CADASTRO DO CLIENTE?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (CONTINUA == DialogResult.No)
                        {
                            Retorno.Focus();
                            SendKeys.Send("{HOME}");
                            SendKeys.Send("+{END}");
                        }
                        else
                        {

                        }
                    }
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_VerificaClientExistente()\n\nBLOCO.: TabClien_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método MET_VerificaClientExistente()\n\nBLOCO.: TabClien_MET\n\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //SELECIONA A COR DE FUNDO DOS FORMULÁRIOS
        public void MET_SelectCorFundo(Control Control_1, Control Control_2, Control Control_3, Control Control_4, Control Control_5, Control Control_6, Control Control_7, string CodigoLoja)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string SelecionaCOR = "SELECT CorFun_CON FROM TabConfi WHERE SeqLoj_CON = " + CodigoLoja;
            SqlCommand Comando = new SqlCommand(SelecionaCOR, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string Cor = Dr[0].ToString();

                    Control_1.BackColor = Color.FromName(Cor);
                    Control_2.BackColor = Color.FromName(Cor);
                    Control_3.BackColor = Color.FromName(Cor);
                    Control_4.BackColor = Color.FromName(Cor);
                    Control_5.BackColor = Color.FromName(Cor);
                    Control_6.BackColor = Color.FromName(Cor);
                    Control_7.BackColor = Color.FromName(Cor);
                }
                else
                {
                    Control_1.BackColor = Color.Silver;
                    Control_2.BackColor = Color.Silver;
                    Control_3.BackColor = Color.Silver;
                    Control_4.BackColor = Color.Silver;
                    Control_5.BackColor = Color.Silver;
                    Control_6.BackColor = Color.Silver;
                    Control_7.BackColor = Color.Silver;
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                Conexão.Close();
            }
        }

        //CAPTURA O CAMINHO DO SALVAMENTO
        public string MET_CapCaminhoSALV(TextBox txtCaminhoRel, string LojaLogada)
        {
            if (txtCaminhoRel.Text == string.Empty)
            {

                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();


                string StringCapCaminhoSALV = "SELECT CamRel_CON FROM TabConfi WHERE SeqLoj_CON = @Sequen";
                SqlCommand Comando = new SqlCommand(StringCapCaminhoSALV, Conexão);

                Comando.Parameters.Add("@Sequen", SqlDbType.Int).Value = LojaLogada;

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        string Caminho = Dr["CamRel_CON"].ToString();

                        return Caminho;
                    }
                    else
                    {
                        MessageBox.Show("Caminho padrão para salvar arquivos de relatório não foi localizado", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_CapCaminhoSALV()\n\nBLOCO = TabClien_MET - CAPTURA CAMINHO DOS RELATÓRIOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_CapCaminhoSALV()\n\nBLOCO = TabClien_MET - CAPTURA CAMINHO DOS RELATÓRIOS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
            return txtCaminhoRel.Text;
        }

        //SELECIONA A ULTIMA ORDEM DA ROTA
        public void MET_SelectUltimaOrdemRota(TextBox txtMESTRE, TextBox txtRotaSequen, TextBox txtRotaCod, TextBox txtCodigo)
        {

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão1 = new SqlConnection(LerString);
            SqlConnection Conexão2 = new SqlConnection(LerString);

            Conexão1.Open();
            Conexão2.Open();

            string strComando = "SELECT Ordems_RO1 FROM TabRot01 WHERE Sequen_RO1 = @Sequen_RO1 AND SeqCli_RO1 = @SeqCli_RO1";
            SqlCommand Comando = new SqlCommand(strComando, Conexão1);
            Comando.Parameters.Add("@Sequen_RO1", SqlDbType.VarChar).Value = txtRotaCod.Text;
            Comando.Parameters.Add("@SeqCli_RO1", SqlDbType.VarChar).Value = txtCodigo.Text;

            string strComandoMais1 = "SELECT MAX (Ordems_RO1 + 1) AS Ordems_RO1 FROM TabRot01 WHERE Sequen_RO1 = @Sequen_RO1";
            SqlCommand ComandoMais1 = new SqlCommand(strComandoMais1, Conexão2);
            ComandoMais1.Parameters.Add("@Sequen_RO1", SqlDbType.VarChar).Value = txtRotaCod.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    txtRotaSequen.Text = Dr["Ordems_RO1"].ToString().PadLeft(4, '0');
                }
                else
                {
                    try
                    {
                        SqlDataReader Dz = ComandoMais1.ExecuteReader(); Dz.Read();
                        if (Dz.HasRows)
                        {
                            if (Dz["Ordems_RO1"].ToString() != string.Empty)
                            {
                                txtRotaSequen.Text = Dz["Ordems_RO1"].ToString().PadLeft(4, '0');
                            }
                            else
                            {
                                txtRotaSequen.Text = "0001";
                            }
                        }
                    }
                    catch (SqlException Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelectUltimaOrdemRota()\n\nBLOCO = SELECIONAR O MAIOR REGISTRO\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelectUltimaOrdemRota()\n\nBLOCO = SELECIONAR O MAIOR REGISTRO\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelectUltimaOrdemRota()\n\nBLOCO = TabClien_MET - CAPTURA ORDEM DA ROTA\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método MET_SelectUltimaOrdemRota()\n\nBLOCO = TabClien_MET - CAPTURA ORDEM DA ROTA\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão1.Close();
                Conexão2.Close();
            }
        }
    }
}
