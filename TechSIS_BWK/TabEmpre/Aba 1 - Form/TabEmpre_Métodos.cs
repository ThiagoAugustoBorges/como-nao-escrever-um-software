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
    public class TabEmpre_Métodos
    {
        #region CRIPTOGRAFIA
        const string senha = "“3.!156350WeNeMy”";
        public static string Criptografar(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(senha));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }
        public static string Descriptografar(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(senha));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }
        #endregion

        //Seleciona os dados ao dar Tab no txtCodigo
        public void SelecionarCodigoDigitarTAB(TextBox TXT_MESTRE, MaskedTextBox mtbCpfCnpj, TextBox txtCodigo, TextBox txtDescri, ComboBox comTipo, TextBox txtFantasia, TextBox txtResponsavel, TextBox txtEndLogradouro, TextBox txtEndNumero, TextBox txtEndCidade, TextBox txtEndCidDescri, TextBox txtEndCidUF, MaskedTextBox mtbEndCep, TextBox txtEndBairro, TextBox txtEndComple, MaskedTextBox mtbPabx, MaskedTextBox mtbFax, TextBox txtEmail, TextBox txtHomePage, TextBox txtInscricaoEstadual, TextBox txtInscricaoMunicipal, MaskedTextBox mtbVencEst, MaskedTextBox mtbVencMun, ComboBox comRegimeTrib, TextBox txtUTF8, TextBox txtUsuario, ComboBox comAtividade, ComboBox comIcmsRdz, ComboBox comIcmsST, ComboBox comTipoFaturamento, ComboBox comSitEmpresa, ComboBox comModuloSistema, TextBox txtCfop, TextBox txtSerie, TextBox txtModelo, TextBox txtMsg, TextBox txtCaixas, TextBox txtEspecie, TextBox txtAproveitamento, TextBox txtObsLivro, ComboBox comLote, ComboBox comFrete, ComboBox comCondicao, ComboBox comRecebimento, ComboBox comConsulta, ComboBox comTipoDeVenda, ComboBox comEstoque, ComboBox comFinanceiro, ComboBox comJuros, CheckBox cheEstNegativoNot, CheckBox cheEstNegativoPed, CheckBox chePedVend, CheckBox chePedComp, CheckBox cheSenhaLimite, CheckBox cheGeraFinanc, CheckBox cheSenhaDesc, CheckBox cheBloqueiaVendaInsc, CheckBox cheVendedorObrig, CheckBox cheEmiteNota, CheckBox cheAutorizaPedV, CheckBox cheAutorizaPedC, CheckBox cheAutorizaCtsPag, CheckBox cheDataAutomatica, CheckBox cheExcluiOrca, CheckBox cheExcluiHist, CheckBox cheDescAnte, CheckBox cheTabelaPrecoVista, NumericUpDown nupOrcamento, NumericUpDown nupHistorico, NumericUpDown nupAntecipado, NumericUpDown nupTabelaVista, NumericUpDown nupTabelaPrazo, NumericUpDown nupJuros, Panel Painel_Codigo, MethodInvoker ZerarCampos, MethodInvoker CamposINATIV, MethodInvoker CamposATIVOS, Button btnGravar, Button btnAvanca)
        {
            txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');
            #region Tratamento para 0
            if (Convert.ToInt32(txtCodigo.Text) < 1)
            {
                MessageBox.Show("Campo (Código) preenchido incorretamente", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Painel_Codigo.Focus();
                txtCodigo.SelectAll();
                ZerarCampos();
                CamposINATIV();
                btnGravar.Enabled = false;
                btnAvanca.Enabled = false;
                return;
            }
            #endregion


            if (TXT_MESTRE.Text == "ALTERAR" || TXT_MESTRE.Text == "SELECT" || TXT_MESTRE.Text == "CONSULTA")
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                string StringComando = "SELECT Sequen_EMP,Descri_EMP,Fantas_EMP,EndLog_EMP,EndNum_EMP,EndCid_EMP,Descri_CID,UfFede_CID,CepCi1_CID,EndCep_EMP,EndBai_EMP,EndCom_EMP,Tipo01_EMP,CpfCnp_EMP,InsEst_EMP,InsMun_EMP,RegTri_EMP,Respon_EMP,TelLoj_EMP,FaxLoj_EMP,Emai01_EMP,HomPag_EMP,NumCai_EMP,CfoSai_EMP,Series_EMP,Rodape_EMP,UTF8En_EMP,DtVeEs_EMP,DtVeMu_EMP,Ativid_EMP,IcmdRz_EMP,IcmsSt_EMP,TipFat_EMP,SitEmp_EMP,ModSof_EMP,EspeNf_EMP,AprCre_EMP,ObsLiv_EMP,LotePr_EMP,FretTp_EMP,CondTp_EMP,ReceTp_EMP,ConCod_EMP,TpVend_EMP,MovEst_EMP,MovFin_EMP,TpJuro_EMP,VaJuro_EMP,Modelo_EMP,EstNot_EMP,EstPed_EMP,PeVeOb_EMP,PeCoOb_EMP,SenLim_EMP,FinFix_EMP,SenDes_EMP,BloIns_EMP,VenObr_EMP,EmiNot_EMP,AutPev_EMP,AutPec_EMP,AutBai_EMP,PedDat_EMP,ExcOrc_EMP,OrcSem_EMP,ExcHis_EMP,OrcHis_EMP,DesAnt_EMP,ValDes_EMP,TabPre_EMP,TabPUm_EMP,TabPDo_EMP FROM TabEmpre INNER JOIN TabCidad ON TabEmpre.EndCid_EMP = TabCidad.Sequen_CID WHERE Sequen_EMP = @Sequen";
                SqlCommand Comando = new SqlCommand(StringComando, Conexão);
                Comando.Parameters.Add("@Sequen", SqlDbType.Int).Value = txtCodigo.Text;

                try
                {
                    SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                    if (Dr.HasRows)
                    {
                        btnGravar.Enabled = true;
                        btnAvanca.Enabled = true;
                        CamposATIVOS();
                        #region POPULA OS CAMPOS DO COM DATAREADER
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
                        if (TXT_MESTRE.Text == "SELECT" || TXT_MESTRE.Text == "CONSULTA")
                        {
                            Painel_Codigo.Focus(); txtCodigo.SelectAll();
                            CamposINATIV();
                            btnGravar.Enabled = false;
                            btnAvanca.Enabled = false;
                        }
                    }
                    else
                    {
                        Painel_Codigo.Focus(); txtCodigo.SelectAll();
                        ZerarCampos();
                        CamposINATIV();
                        MessageBox.Show("Empresa inexistente no banco de dados", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnGravar.Enabled = false;
                        btnAvanca.Enabled = false;
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

        //Seleciona a cor de fundo do painel Painel_UTF8
        public void SelecionaCorFundo(Panel Painel_Cor1, Panel Painel_Cor2, Panel Painel_Cor3, Panel Painel_Cor4, MaskedTextBox mtbCpfCnpj,string LojaSequen)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT CorFun_CON FROM TabConfi WHERE SeqLoj_CON = @Sequen";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen", SqlDbType.VarChar).Value = LojaSequen;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string Cor = Dr[0].ToString();
                    Painel_Cor1.BackColor = Color.FromName(Cor);
                    Painel_Cor2.BackColor = Color.FromName(Cor);
                    Painel_Cor3.BackColor = Color.FromName(Cor);
                    Painel_Cor4.BackColor = Color.FromName(Cor);
                    mtbCpfCnpj.BackColor = Color.FromName(Cor);
                }
                else
                {
                    Painel_Cor1.BackColor = Color.Silver;
                    Painel_Cor2.BackColor = Color.Silver;
                    Painel_Cor3.BackColor = Color.Silver;
                    Painel_Cor4.BackColor = Color.Silver;
                    mtbCpfCnpj.BackColor = Color.Silver;
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

        //Seleciona texto SecF logado
        public void SelecionaSecFLogado(string LojaLogada, TextBox txtCodigo, TextBox txtDescri, ComboBox comTipo, MaskedTextBox mtbCpfCnpj, TextBox txtUTF8)
        {
            string Arquivo = ("SecF_" + LojaLogada + ".xml");
            XmlDocument LerXML = new XmlDocument();
            if (File.Exists(Arquivo))
            {
                LerXML.Load(Arquivo);
                XmlNode Codigo_EMP = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_01");
                XmlNode Descri_EMP = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_02");
                XmlNode CpfCnp_EMP = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_03");
                XmlNode Chave_UTF8 = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_04");
                XmlNode Data_Arquivo = LerXML.DocumentElement.SelectSingleNode("Tech_SEC_05");

                string _Codigo_EMP = Descriptografar(Codigo_EMP.InnerText.ToString());
                string _Descri_EMP = Descriptografar(Descri_EMP.InnerText.ToString());
                string _CpfCnp_EMP = Descriptografar(CpfCnp_EMP.InnerText.ToString());
                string _Chave_UTF8 = Descriptografar(Chave_UTF8.InnerText.ToString());
                string _Data_Arquivo = Data_Arquivo.InnerText.ToString();

                if (_CpfCnp_EMP.Length == 11)
                {
                    comTipo.SelectedIndex = 0;
                }
                if (_CpfCnp_EMP.Length == 14)
                {
                    comTipo.SelectedIndex = 1;
                }

                txtCodigo.Text = _Codigo_EMP.PadLeft(6, '0');
                txtDescri.Text = _Descri_EMP;
                mtbCpfCnpj.Text = _CpfCnp_EMP;
                txtUTF8.Text = _Chave_UTF8;   
            }
            else
            {
                MessageBox.Show("MENSAGEM Q O FILE NAO EXISTE E FECHA O SISTEMA");
            }

            
        }

        //Define que no campo pode haver apenas números
        #region Apenas Números
        public void ApenasNúmeros(KeyPressEventArgs KeyPress)
        {
            if (!Char.IsDigit(KeyPress.KeyChar) && KeyPress.KeyChar != (char)8)
            {
                KeyPress.Handled = true;
                return;
            }
        }
        #endregion

        //Verifica se data é valida ou não
        public bool VerificaDATA(MaskedTextBox CampoAvaliado, Control Retorno)
        {
            if (CampoAvaliado.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
            {
                return false;
            }
            else
            {
                try
                {
                    Convert.ToDateTime(CampoAvaliado.Text);
                    return false;
                }
                catch (Exception)
                {
                    MessageBox.Show("Data inválida..Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Retorno.Focus();
                    SendKeys.Send("{HOME}");
                    SendKeys.Send("+{END}"); 
                    return true;
                }
            }
        }

        //Busca a cidade no TAB
        public void SelecionaCidade(TextBox txtEndCidade, TextBox txtEndCidDescri, TextBox txtEndCidUF, MaskedTextBox mtbEndCep, Control Retorno)
        {
            #region Tratamento para 0
            if (txtEndCidade.Text == string.Empty)
            {
                txtEndCidDescri.Text = string.Empty; txtEndCidUF.Text = string.Empty;
                txtEndCidade.Text = "000000";
            }
            if (Convert.ToInt32(txtEndCidade.Text) < 1)
            {
                txtEndCidDescri.Text = string.Empty; txtEndCidUF.Text = string.Empty;
                MessageBox.Show("Campo (Código Cidade) preenchido incorretamente", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Retorno.Select(); txtEndCidade.SelectAll();
                return;
            }
            #endregion
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringComando = "SELECT Descri_CID,UfFede_CID,CepCi1_CID,Status_CID FROM TabCidad WHERE Sequen_CID = @Sequen";
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);
            Comando.Parameters.Add("@Sequen", SqlDbType.VarChar).Value = txtEndCidade.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    if (Dr["Status_CID"].ToString() == "3")
                    {
                        txtEndCidDescri.Text = string.Empty; txtEndCidUF.Text = string.Empty;
                        MessageBox.Show("Cidade consta como EXCLUIDA no Software. Verifique!", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Retorno.Select(); txtEndCidade.SelectAll();
                    }
                    else if (Dr["Status_CID"].ToString() == "2")
                    {
                        txtEndCidDescri.Text = string.Empty; txtEndCidUF.Text = string.Empty;
                        MessageBox.Show("Cidade consta como INATIVA no Software. Verifique!", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Retorno.Select(); txtEndCidade.SelectAll();
                    }
                    else
                    {
                        txtEndCidDescri.Text = Dr["Descri_CID"].ToString();
                        txtEndCidUF.Text = Dr["UfFede_CID"].ToString();
                    }
                }
                else
                {
                    txtEndCidDescri.Text = string.Empty; txtEndCidUF.Text = string.Empty;
                    MessageBox.Show("Cidade inexistente no banco de dados", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Retorno.Select(); txtEndCidade.SelectAll();
                }
            }
            //Trata os possiveis erro do método
            #region TRATAMENTO DE ERROS
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
