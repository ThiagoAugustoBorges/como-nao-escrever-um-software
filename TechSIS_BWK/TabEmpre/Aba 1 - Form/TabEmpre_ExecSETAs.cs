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

namespace TabEmpre
{
    class TabEmpre_ExecSETAs
    {
        public void ExecSETAs(string StringComandoSELEÇÃO, TextBox TXT_MESTRE, MaskedTextBox mtbCpfCnpj, TextBox txtCodigo, TextBox txtDescri, ComboBox comTipo, TextBox txtFantasia, TextBox txtResponsavel, TextBox txtEndLogradouro, TextBox txtEndNumero, TextBox txtEndCidade, TextBox txtEndCidDescri, TextBox txtEndCidUF, MaskedTextBox mtbEndCep, TextBox txtEndBairro, TextBox txtEndComple, MaskedTextBox mtbPabx, MaskedTextBox mtbFax, TextBox txtEmail, TextBox txtHomePage, TextBox txtInscricaoEstadual, TextBox txtInscricaoMunicipal, MaskedTextBox mtbVencEst, MaskedTextBox mtbVencMun, ComboBox comRegimeTrib, TextBox txtUTF8, TextBox txtUsuario, ComboBox comAtividade, ComboBox comIcmsRdz, ComboBox comIcmsST, ComboBox comTipoFaturamento, ComboBox comSitEmpresa, ComboBox comModuloSistema, TextBox txtCfop, TextBox txtSerie, TextBox txtModelo, TextBox txtMsg, TextBox txtCaixas, TextBox txtEspecie, TextBox txtAproveitamento, TextBox txtObsLivro, ComboBox comLote, ComboBox comFrete, ComboBox comCondicao, ComboBox comRecebimento, ComboBox comConsulta, ComboBox comTipoDeVenda, ComboBox comEstoque, ComboBox comFinanceiro, ComboBox comJuros, CheckBox cheEstNegativoNot, CheckBox cheEstNegativoPed, CheckBox chePedVend, CheckBox chePedComp, CheckBox cheSenhaLimite, CheckBox cheGeraFinanc, CheckBox cheSenhaDesc, CheckBox cheBloqueiaVendaInsc, CheckBox cheVendedorObrig, CheckBox cheEmiteNota, CheckBox cheAutorizaPedV, CheckBox cheAutorizaPedC, CheckBox cheAutorizaCtsPag, CheckBox cheDataAutomatica, CheckBox cheExcluiOrca, CheckBox cheExcluiHist, CheckBox cheDescAnte, CheckBox cheTabelaPrecoVista, NumericUpDown nupOrcamento, NumericUpDown nupHistorico, NumericUpDown nupAntecipado, NumericUpDown nupTabelaVista, NumericUpDown nupTabelaPrazo, NumericUpDown nupJuros, Panel Painel_Codigo, MethodInvoker ZerarCampos, MethodInvoker CamposINATIV, MethodInvoker CamposATIVOS, Button btnGravar, Button btnAvanca)
        {
            string Mensagem_De_Erro = "";

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string NomeDaOpção = "Empresas";

            #region Comandos
            if (StringComandoSELEÇÃO == "1")
            {
                StringComandoSELEÇÃO = "SELECT TOP 1 *,Descri_CID,UfFede_CID FROM TabEmpre INNER JOIN TabCidad ON TabEmpre.EndCid_EMP = TabCidad.Sequen_CID ORDER BY Sequen_EMP";
                Mensagem_De_Erro = "Sem dados de " + NomeDaOpção + " para exibir";
            }
            if (StringComandoSELEÇÃO == "2")
            {
                string Num = txtCodigo.Text;
                if (txtCodigo.Text == string.Empty)
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 *,Descri_CID,UfFede_CID FROM TabEmpre INNER JOIN TabCidad ON TabEmpre.EndCid_EMP = TabCidad.Sequen_CID ORDER BY Sequen_EMP";
                    Mensagem_De_Erro = "Não existe registro anterior no banco";
                }
                else
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 *,Descri_CID,UfFede_CID FROM TabEmpre INNER JOIN TabCidad ON TabEmpre.EndCid_EMP = TabCidad.Sequen_CID WHERE Sequen_EMP < " + Num + " ORDER BY Sequen_EMP DESC";
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
                StringComandoSELEÇÃO = "SELECT TOP 1 *,Descri_CID,UfFede_CID FROM TabEmpre INNER JOIN TabCidad ON TabEmpre.EndCid_EMP = TabCidad.Sequen_CID WHERE Sequen_EMP > " + Num ;
                Mensagem_De_Erro = "Não existe próximo registro no banco";
            }
            if (StringComandoSELEÇÃO == "4")
            {
                StringComandoSELEÇÃO = "SELECT TOP 1 *,Descri_CID,UfFede_CID FROM TabEmpre INNER JOIN TabCidad ON TabEmpre.EndCid_EMP = TabCidad.Sequen_CID ORDER BY Sequen_EMP DESC";
                Mensagem_De_Erro = "Sem dados de " + NomeDaOpção + " para exibir";
            }
            #endregion

            SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);

            try
            {
                SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    btnGravar.Enabled = false;
                    btnAvanca.Enabled = false;
                    #region POPULA OS CAMPOS DO COM DATAREADER
                    txtCodigo.Text = Dr["Sequen_EMP"].ToString().PadLeft(6, '0');
                    txtDescri.Text = Dr["Descri_EMP"].ToString();
                    txtFantasia.Text = Dr["Fantas_EMP"].ToString();
                    txtEndLogradouro.Text = Dr["EndLog_EMP"].ToString();
                    txtEndNumero.Text = Dr["EndNum_EMP"].ToString();
                    txtEndCidade.Text = Dr["EndCid_EMP"].ToString().PadLeft(6, '0');
                    txtEndCidDescri.Text = Dr["Descri_CID"].ToString();
                    txtEndCidUF.Text = Dr["UfFede_CID"].ToString();
                    mtbEndCep.Text = Dr["EndCep_EMP"].ToString();
                    txtEndBairro.Text = Dr["EndBai_EMP"].ToString();
                    txtEndComple.Text = Dr["EndCom_EMP"].ToString();
                    comTipo.SelectedIndex = Convert.ToInt32(Dr["Tipo01_EMP"]);
                    mtbCpfCnpj.Text = Dr["CpfCnp_EMP"].ToString();
                    txtInscricaoEstadual.Text = Dr["InsEst_EMP"].ToString();
                    txtInscricaoMunicipal.Text = Dr["InsMun_EMP"].ToString();
                    comRegimeTrib.SelectedIndex = Convert.ToInt32(Dr["RegTri_EMP"]);
                    txtResponsavel.Text = Dr["Respon_EMP"].ToString();
                    mtbFax.Text = Dr["FaxLoj_EMP"].ToString();
                    mtbPabx.Text = Dr["TelLoj_EMP"].ToString();
                    txtEmail.Text = Dr["Emai01_EMP"].ToString();
                    txtHomePage.Text = Dr["HomPag_EMP"].ToString();
                    #region txtCaixas.Text = Dr["NumCai_EMP"].ToString();
                    if (Convert.ToInt32(Dr["NumCai_EMP"]) < 1)
                    {
                        txtCaixas.Text = string.Empty;
                    }
                    else
                    {
                        txtCaixas.Text = Dr["NumCai_EMP"].ToString().PadLeft(3, '0');
                    }
                    #endregion
                    #region txtCfop.Text = Dr["txtCfop"].ToString();
                    if (Convert.ToInt32(Dr["CfoSai_EMP"]) < 1)
                    {
                        txtCfop.Text = string.Empty;
                    }
                    else
                    {
                        txtCfop.Text = Dr["CfoSai_EMP"].ToString();
                    }
                    #endregion
                    #region txtSerie.Text = Dr["Series_EMP"].ToString();
                    if (Convert.ToInt32(Dr["Series_EMP"]) < 1)
                    {
                        txtSerie.Text = string.Empty;
                    }
                    else
                    {
                        txtSerie.Text = Dr["Series_EMP"].ToString().PadLeft(3, '0');
                    }
                    #endregion
                    #region txtMsg.Text = Dr["Rodape_EMP"].ToString();
                    if (Convert.ToInt32(Dr["Rodape_EMP"]) < 1)
                    {
                        txtMsg.Text = string.Empty;
                    }
                    else
                    {
                        txtMsg.Text = Dr["Rodape_EMP"].ToString().PadLeft(3, '0');
                    }
                    #endregion
                    txtUTF8.Text = Dr["UTF8En_EMP"].ToString();
                    #region mtbVencEst.Text = Dr["DtVeEs_EMP"].ToString();
                    if (Convert.ToDateTime(Dr["DtVeEs_EMP"]) == DateTime.MinValue)
                    {
                        mtbVencEst.Text = string.Empty;
                    }
                    else
                    {
                        mtbVencEst.Text = Dr["DtVeEs_EMP"].ToString();
                    }
                    #endregion
                    #region mtbVencMun.Text = Dr["DtVeMu_EMP"].ToString();
                    if (Convert.ToDateTime(Dr["DtVeMu_EMP"]) == DateTime.MinValue)
                    {
                        mtbVencMun.Text = string.Empty;
                    }
                    else
                    {
                        mtbVencMun.Text = Dr["DtVeMu_EMP"].ToString();
                    }
                    #endregion
                    comAtividade.SelectedIndex = Convert.ToInt32(Dr["Ativid_EMP"]);
                    comIcmsRdz.SelectedIndex = Convert.ToInt32(Dr["IcmdRz_EMP"]);
                    comIcmsST.SelectedIndex = Convert.ToInt32(Dr["IcmsSt_EMP"]);
                    comTipoFaturamento.SelectedIndex = Convert.ToInt32(Dr["TipFat_EMP"]);
                    comSitEmpresa.SelectedIndex = Convert.ToInt32(Dr["SitEmp_EMP"]);
                    comModuloSistema.SelectedIndex = Convert.ToInt32(Dr["ModSof_EMP"]);
                    txtEspecie.Text = Dr["EspeNf_EMP"].ToString();
                    txtAproveitamento.Text = Dr["AprCre_EMP"].ToString();
                    txtObsLivro.Text = Dr["ObsLiv_EMP"].ToString();
                    comLote.SelectedIndex = Convert.ToInt32(Dr["LotePr_EMP"]);
                    comFrete.SelectedIndex = Convert.ToInt32(Dr["FretTp_EMP"]);
                    comCondicao.SelectedIndex = Convert.ToInt32(Dr["CondTp_EMP"]);
                    comRecebimento.SelectedIndex = Convert.ToInt32(Dr["ReceTp_EMP"]);
                    comConsulta.SelectedIndex = Convert.ToInt32(Dr["ConCod_EMP"]);
                    comTipoDeVenda.SelectedIndex = Convert.ToInt32(Dr["TpVend_EMP"]);
                    comEstoque.SelectedIndex = Convert.ToInt32(Dr["MovEst_EMP"]);
                    comFinanceiro.SelectedIndex = Convert.ToInt32(Dr["MovFin_EMP"]);
                    comJuros.SelectedIndex = Convert.ToInt32(Dr["TpJuro_EMP"]);
                    nupJuros.Value = Convert.ToDecimal(Dr["VaJuro_EMP"]);
                    txtModelo.Text = Dr["Modelo_EMP"].ToString();
                    //CheckBoxs
                    cheEstNegativoNot.Checked = Convert.ToBoolean(Dr["EstNot_EMP"]);
                    cheEstNegativoPed.Checked = Convert.ToBoolean(Dr["EstPed_EMP"]);
                    chePedVend.Checked = Convert.ToBoolean(Dr["PeVeOb_EMP"]);
                    chePedComp.Checked = Convert.ToBoolean(Dr["PeCoOb_EMP"]);
                    cheSenhaLimite.Checked = Convert.ToBoolean(Dr["SenLim_EMP"]);
                    cheGeraFinanc.Checked = Convert.ToBoolean(Dr["FinFix_EMP"]);
                    cheSenhaDesc.Checked = Convert.ToBoolean(Dr["SenDes_EMP"]);
                    cheBloqueiaVendaInsc.Checked = Convert.ToBoolean(Dr["BloIns_EMP"]);
                    cheVendedorObrig.Checked = Convert.ToBoolean(Dr["VenObr_EMP"]);
                    cheEmiteNota.Checked = Convert.ToBoolean(Dr["EmiNot_EMP"]);
                    cheAutorizaPedV.Checked = Convert.ToBoolean(Dr["AutPev_EMP"]);
                    cheAutorizaPedC.Checked = Convert.ToBoolean(Dr["AutPec_EMP"]);
                    cheAutorizaCtsPag.Checked = Convert.ToBoolean(Dr["AutBai_EMP"]);
                    cheDataAutomatica.Checked = Convert.ToBoolean(Dr["PedDat_EMP"]);
                    cheExcluiOrca.Checked = Convert.ToBoolean(Dr["ExcOrc_EMP"]);
                    nupOrcamento.Value = Convert.ToDecimal(Dr["OrcSem_EMP"]);
                    cheExcluiHist.Checked = Convert.ToBoolean(Dr["ExcHis_EMP"]);
                    nupHistorico.Value = Convert.ToDecimal(Dr["OrcHis_EMP"]);
                    cheDescAnte.Checked = Convert.ToBoolean(Dr["DesAnt_EMP"]);
                    nupAntecipado.Value = Convert.ToDecimal(Dr["ValDes_EMP"]);
                    cheTabelaPrecoVista.Checked = Convert.ToBoolean(Dr["TabPre_EMP"]);
                    nupTabelaVista.Value = Convert.ToDecimal(Dr["TabPUm_EMP"]);
                    nupTabelaPrazo.Value = Convert.ToDecimal(Dr["TabPDo_EMP"]);
                    #endregion
                }
                else
                {
                    MessageBox.Show(Mensagem_De_Erro, "Consulta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //Trata os possiveis erro do método
            #region TRATAMENTO DE ERROS
            catch (FormatException)
            {
                MessageBox.Show("Foi detectado que algum campo do formulário foi alterado manualmente no banco de dados. Para evitar que isso aconteça certifique-se que o UPDATE/INSERT foi escrito corretamente.\nEsta situação pode ocorrer principalmente nos campos de marcar, onde no banco de dados deve constar True ou False.\n\nPara corrigir este problema, apenas regrave as informações da Empresa.", "TechSIS Auto-Manutenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException Ex)
            {
                DialogResult Erro = MessageBox.Show("ERRO SQL.: FOI DETECTADO UM ERRO NA CONSULTA\nTecle CANCELAR para ver detalhes do erro e contate a Programação responsável pelo sistema", "TechSIS BWK Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (Erro == DialogResult.Cancel)
                {
                    MessageBox.Show("DETALHE DO ERRO OCORRIDO.:\n" + Ex.Message, "TechSIS Auto-Manutenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Ex)
            {
                DialogResult Erro = MessageBox.Show("ERRO SQL.: FOI DETECTADO UM ERRO NA CONSULTA\nTecle CANCELAR para ver detalhes do erro e contate a Programação responsável pelo sistema", "TechSIS BWK Erro.: " + Ex.GetType().ToString(), MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (Erro == DialogResult.Cancel)
                {
                    MessageBox.Show("DETALHE DO ERRO OCORRIDO.:\n" + Ex.Message, "TechSIS Auto-Manutenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                Conexão.Close();
            }
            #endregion
        }
    }
}
