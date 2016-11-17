using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TabClien
{
    internal partial class TabClien : Form
    {
        public TabClien()
        {
            InitializeComponent();
        }

        public string _Login_LojaID_TabClien { get; set; }
        public string _Login_LojaDesc_TabClien { get; set; }
        public string _Login_UsuarioID_TabClien { get; set; }
        public string _Login_UsuarioDesc_TabClien { get; set; }



        //LOAD DO FORM
        private void TabClien_Load(object sender, EventArgs e)
        {
            //DESATIVA OS CAMPOS
            CamposDisable();

            //POPULA O USUÁRIO
            txtUsuario.Text = _Login_UsuarioID_TabClien.PadLeft(6, '0');

            //VERIFICA AS PERMISSÕES DOS BUTTONS
            TabClien_Permi PERMI = new TabClien_Permi();
            PERMI.PER_Permiss_Buttons(btnIncluir, btnAlterar, btnExcluir, btnSeta1, btnSeta2, btnSeta3, btnSeta4, txtCodigo, _Login_UsuarioID_TabClien);


            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panDownAb1, panDownAb2, panDownAb3, panDownAb4, lblImpreTitulo, txtQtSelectIMP, txtQtSelectPES };
            //MÉTODO DA CFG
            CfgComun.CfgComun_CLASS CFG = new CfgComun.CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, _Login_LojaID_TabClien);


            //CARREGA OS FILTROS
            TabClien_FILTROS FILTROS = new TabClien_FILTROS();
            FILTROS.cheFILTROSChecked(cheFiltrosIMP, cheFiltrosPES, _Login_LojaID_TabClien);

            #region CANCELA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged -= new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged -= new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged -= new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged -= new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged -= new EventHandler(nupQtResultados_ValueChanged);
            comPesSituacao.SelectedIndexChanged -= new EventHandler(comPesSituacao_SelectedIndexChanged);
            comPesCategoria.SelectedIndexChanged -= new EventHandler(comPesCategoria_SelectedIndexChanged);
            comPesQtAoCredito.SelectedIndexChanged -= new EventHandler(comPesQtAoCredito_SelectedIndexChanged);
            cheOrdemAlfDown.CheckedChanged -= new EventHandler(cheOrdemAlfDown_CheckedChanged);
            #endregion
            FILTROS.CarregaFILTROS(cheFiltrosPES, cheOrdemAlfDown, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comPesSituacao, comPesCategoria, comPesQtAoCredito, cheFiltrosIMP, comImpSituacao, txtImpEmpreCod, txtImpEmpreDesc, comImpCategoria, cheImpApelid, rabRPV, rabWORD, rabEXCEL, rabTXT, rabOrdemNumerica, rabOrdemAlfabetica, rabOrdemAlfabeticaApelid, txtCaminhoRel, cheVoltarLix);
            #region VOLTA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged += new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged += new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged += new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged += new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged += new EventHandler(nupQtResultados_ValueChanged);
            comPesSituacao.SelectedIndexChanged += new EventHandler(comPesSituacao_SelectedIndexChanged);
            comPesCategoria.SelectedIndexChanged += new EventHandler(comPesCategoria_SelectedIndexChanged);
            comPesQtAoCredito.SelectedIndexChanged += new EventHandler(comPesQtAoCredito_SelectedIndexChanged);
            cheOrdemAlfDown.CheckedChanged += new EventHandler(cheOrdemAlfDown_CheckedChanged);
            if (rabTOP.Checked == true) { nupQtResultados.Enabled = true; }
            #endregion


            //APLICA A COR DE FUNDO
            TabClien_MET MET = new TabClien_MET();
            //CAPTURA O CAMINHO DO ARQUIVO CASO NÃO ESTEJA NO XML DOS FILTROS
            txtCaminhoRel.Text = MET.MET_CapCaminhoSALV(txtCaminhoRel, _Login_LojaID_TabClien);

            btnIncluir.Select();
        }

        //PROPRIEDADES AO SE MUDAR DE ABA
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Prop. ABA 1
            if (TabControl.SelectedTab == Tp1)
            {
                if (txtMESTRE.Text == "PESQUISA" || txtMESTRE.Text == "IMPRESSÃO" || txtMESTRE.Text == "HISTÓRICO" || txtMESTRE.Text == "LIXEIRA")
                {
                    txtMESTRE.Text = "SELECT";
                    txtMESTRE.BackColor = Color.Silver;
                    txtMESTRE.ForeColor = Color.Black;
                    ZerarCampos();
                    CamposDisable();
                    txtCodigo.Text = string.Empty;
                    btnIncluir.Select();
                }
                else
                {
                    txtCodigo.Select(); txtCodigo.SelectAll();
                }
            }
            #endregion
            #region Prop. ABA 2
            if (TabControl.SelectedTab == Tp2)
            {
                if (txtMESTRE.Text == "PESQUISA" || txtMESTRE.Text == "IMPRESSÃO" || txtMESTRE.Text == "HISTÓRICO" || txtMESTRE.Text == "LIXEIRA")
                {
                    TabControl.SelectedTab = Tp1;
                }
                else
                {
                    txtEmailContato1.Select(); txtEmailContato1.SelectAll();

                    txtCodigoAb2.Text = txtCodigo.Text;
                    txtDescriAb2.Text = txtDescri.Text;
                    mtbCpfCnpjAb2.Text = mtbCpfCnpj.Text;
                }
            }
            #endregion
            #region Prop. ABA 3
            if (TabControl.SelectedTab == Tp3)
            {
                txtQtSelectPES.Text = string.Empty;

                txtMESTRE.Text = "PESQUISA";
                txtMESTRE.BackColor = Color.Salmon;
                txtMESTRE.ForeColor = Color.Green;


                //Apaga os campos quando a aba pesquisa é selecionada
                txtPesDescri.Select();

                ZerarCamposPesquisa();

                txtQtSelectPES.Text = string.Empty;
                Dgv_Pesquisa.Rows.Clear();
            }
            #endregion
            #region Prop. ABA 4
            if (TabControl.SelectedTab == Tp4)
            {
                txtQtSelectIMP.Text = string.Empty;

                txtMESTRE.Text = "IMPRESSÃO";
                txtMESTRE.BackColor = Color.Gold;
                txtMESTRE.ForeColor = Color.Red;
                btnPrint.Select();
            }
            #endregion
            #region Prop. ABA 5
            if (TabControl.SelectedTab == Tp5)
            {
                txtMESTRE.Text = "LIXEIRA";
                txtMESTRE.BackColor = Color.Coral;
                txtMESTRE.ForeColor = Color.Yellow;
                //Popula Lixeira
                TabClien_Lixeira Lix = new TabClien_Lixeira();
                Lix.Lix_POPULAR(Dgv_Lixeira, cheVoltarLix, TabControl, Tp1);
            }
            #endregion
            #region Prop. ABA 6
            if (TabControl.SelectedTab == Tp6)
            {
                comIDHis.SelectedIndexChanged -= new EventHandler(comIDHis_SelectedIndexChanged);

                txtMESTRE.Text = "HISTÓRICO";
                txtMESTRE.BackColor = Color.MediumSlateBlue;
                txtMESTRE.ForeColor = Color.White;

                //Popula Histório
                TabClien_Histórico HIST = new TabClien_Histórico();
                HIST.HIS_SelecionaDados(mtbData1His, mtbData2His, txtUsuarioHis, txtUsuario, _Login_UsuarioDesc_TabClien, txtUsuarioDescHis);



                HIST.HIS_PopularHISTORICO(txtUsuarioHis, this, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
                mtbData1His.Select();
                SendKeys.Send("{HOME}");
                SendKeys.Send("+{END}");


                comIDHis.SelectedIndexChanged += new EventHandler(comIDHis_SelectedIndexChanged);
            }
            #endregion
        }

        //VERIFICA AS PROPRIEDADES ANTES DE MUDAR DE ABA
        private void TabControl_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (TabControl.SelectedTab == Tp1 && btnGravar.Enabled == true)
            {
                e.Cancel = true;
                if (txtMESTRE.Text == "ALTERAR" || txtMESTRE.Text == "INCLUIR")
                {
                    //VERIFICA OS CAMPOS OBRIGATÓRIOS
                    TabClien_CamposObrig CamposObrig = new TabClien_CamposObrig();
                    bool Obrig = CamposObrig.CamposObrig(txtMESTRE, txtCodigo, txtDescri, txtCodigoPrin, comTipoPFPJ, mtbCpfCnpj, txtFantasia, txtInscricaoEstadual, mtbVencEst, txtEmpresaCod, txtEmpresaDesc, txtConceito, txtEndCidadeFATU, txtEndCidDescriFATU, txtEndCidUFFATU, comContratoEmpresa, comCategoria, txtVendedorDesc, txtVendedorCod, txtEndCidadePERSO, txtEndCidDescriPERSO, txtTransportadoraCod, txtTransportadoraDesc, txtConvenioCod, txtConvenioDesc, txtCfop, txtCfopDesc, mtbVencMun, txtInscricaoMunicipal, txtValorLimiteCre, mtbDataVenciLimite, mtbContraInicio, mtbContraFim, mtbPfCpfConjuge, txtPfConjuge, comStatus, txtRotaSequen, txtRotaCod);
                    if (!Obrig) { } else { return; }

                    //VALIDAS OS CAMPOS DE DATAS
                    #region VALIDA OS CAMPOS DE DATA
                    TabClien_MET MET = new TabClien_MET();
                    bool Campo1 = MET.MET_VerificaDATA(mtbVencEst, mtbVencEst);
                    if (!Campo1) { } else { return; }

                    bool Campo2 = MET.MET_VerificaDATA(mtbVencMun, mtbVencMun);
                    if (!Campo2) { } else { return; }

                    bool Campo3 = MET.MET_VerificaDATA(mtbDataVenciLimite, mtbDataVenciLimite);
                    if (!Campo3) { } else { return; }

                    bool Campo4 = MET.MET_VerificaDATA(mtbContraInicio, mtbContraInicio);
                    if (!Campo4) { } else { return; }

                    bool Campo5 = MET.MET_VerificaDATA(mtbContraFim, mtbContraFim);
                    if (!Campo5) { } else { return; }
                    #endregion

                    //VALIDA O CPF CNPJ
                    #region VALIDA CPF CNPJ
                    if (comTipoPFPJ.SelectedIndex == 0)
                    {
                        bool VALIDAR = MET.MET_ValidaCPF(mtbCpfCnpj.Text);
                        if (!VALIDAR)
                        {
                            MessageBox.Show("CPF informado é inválido. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mtbCpfCnpj.Select();
                            mtbCpfCnpj.SelectAll();
                            return;
                        }
                    }
                    if (comTipoPFPJ.SelectedIndex == 1)
                    {
                        bool VALIDAR = MET.MET_ValidaCNPJ(mtbCpfCnpj.Text);
                        if (!VALIDAR)
                        {
                            MessageBox.Show("CNPJ informado é inválido. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mtbCpfCnpj.Select();
                            mtbCpfCnpj.SelectAll();
                            return;
                        }
                    }
                    #endregion
                }
                e.Cancel = false;
            }
            if (TabControl.SelectedTab == Tp2 && btnGravarAb2.Enabled == true)
            {
                e.Cancel = true;
                if (txtMESTRE.Text == "ALTERAR" || txtMESTRE.Text == "INCLUIR")
                {
                    TabClien_MET MET = new TabClien_MET();

                    //VALIDAS OS CAMPOS DE DATAS
                    #region VALIDA OS CAMPOS DE DATA
                    bool Campo1 = MET.MET_VerificaDATA(mtbPfAdmissao, mtbPfAdmissao);
                    if (!Campo1) { } else { return; }

                    bool Campo2 = MET.MET_VerificaDATA(mtbPfAniversario, mtbPfAniversario);
                    if (!Campo2) { } else { return; }

                    #endregion

                    //VALIDA O CPF CNPJ
                    #region VALIDAR CPF CONJUGE
                    if (mtbPfCpfConjuge.Text.Count(c => char.IsLetterOrDigit(c)) != 0)
                    {
                        bool VALIDAR = MET.MET_ValidaCPF(mtbPfCpfConjuge.Text);
                        if (!VALIDAR)
                        {
                            MessageBox.Show("CPF informado é inválido. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mtbPfCpfConjuge.Select();
                            mtbPfCpfConjuge.SelectAll();
                            return;
                        }
                    }
                    #endregion
                }
                e.Cancel = false;
            }
        }

        //PERMISSÃO POR ABAS
        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            TabClien_Permi PERMI = new TabClien_Permi();
            if (e.TabPageIndex > 1)
            {
                PERMI.PER_VerificaPermi_TbCont(e, e.TabPageIndex, txtCodigo, _Login_UsuarioID_TabClien);
            }
        }

        //GRAVA OS VALORES DOS FILTROS
        private void TabClien_FormClosing(object sender, FormClosingEventArgs e)
        {
            TabClien_FILTROS FILTROS = new TabClien_FILTROS();
            FILTROS.GravarFILTROS(cheFiltrosPES, cheOrdemAlfDown, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comPesSituacao, comPesCategoria, comPesQtAoCredito, cheFiltrosIMP, comImpSituacao, txtImpEmpreCod, txtImpEmpreDesc, comImpCategoria, cheImpApelid, rabRPV, rabWORD, rabEXCEL, rabTXT, rabOrdemNumerica, rabOrdemAlfabetica, rabOrdemAlfabeticaApelid, txtCaminhoRel, cheVoltarLix);
        }

        //TECLAS DE ATALHOS DO FORMULÁRIO
        private void TabClien_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    btnIncluir.PerformClick();
                    break;
                case Keys.F2:
                    btnAlterar.PerformClick();
                    break;
                case Keys.F3:
                    btnExcluir.PerformClick();
                    break;
                case Keys.F4:
                    btnAjuda.PerformClick();
                    break;
                case Keys.F5:
                    btnPesquisar.PerformClick();
                    break;
                case Keys.F7:
                    btnFechar.PerformClick();
                    btnFecharAb2.PerformClick();
                    btnFechar2.PerformClick();
                    btnFecharIMP.PerformClick();
                    break;
                case Keys.F8:
                    btnVoltar.PerformClick();
                    btnVoltarIMP.PerformClick();
                    break;
                case Keys.F9:
                    btnZerar.PerformClick();
                    break;
                case Keys.F10:
                    btnGravar.PerformClick();
                    btnGravarAb2.PerformClick();
                    break;
                case Keys.F11:
                    btnPrint.PerformClick();
                    btnHistorico.PerformClick();
                    btnVoltar.PerformClick();
                    btnAvancar.PerformClick();
                    break;
            }
        }

        #region FORMULÁRIO PRINCIPAL


        //MÉTODOS DO FORM
        #region ZerarCampos, CamposDisable e CamposEnable
        public void ZerarCampos()
        {
            txtLabel.Text = "ALTERE AQUI";
            #region Aba 1
            foreach (Control panPrin in panPrinAb1.Controls)
            {
                foreach (Control CONTROLs in panPrin.Controls)
                {
                    if (CONTROLs.GetType() == typeof(TextBox) && CONTROLs.Name != "txtCodigo" && CONTROLs.Name != "txtLabel")
                    {
                        (CONTROLs as TextBox).Text = string.Empty;
                    }
                    if (CONTROLs.GetType() == typeof(ComboBox))
                    {
                        (CONTROLs as ComboBox).SelectedIndex = -1;
                    }
                    if (CONTROLs.GetType() == typeof(MaskedTextBox))
                    {
                        (CONTROLs as MaskedTextBox).Text = string.Empty;
                    }
                    if (CONTROLs.GetType() == typeof(CheckBox))
                    {
                        (CONTROLs as CheckBox).Checked = false;
                    }
                }
            }
            #endregion
            #region Aba 2
            foreach (Control panPrin in panPrinAb2.Controls)
            {
                foreach (Control CONTROLs in panPrin.Controls)
                {
                    if (CONTROLs.GetType() == typeof(TextBox))
                    {
                        (CONTROLs as TextBox).Text = string.Empty;
                    }
                    if (CONTROLs.GetType() == typeof(ComboBox))
                    {
                        (CONTROLs as ComboBox).SelectedIndex = -1;
                    }
                    if (CONTROLs.GetType() == typeof(MaskedTextBox))
                    {
                        (CONTROLs as MaskedTextBox).Text = string.Empty;
                    }
                    if (CONTROLs.GetType() == typeof(CheckBox))
                    {
                        (CONTROLs as CheckBox).Checked = false;
                    }
                }
            }
            #endregion

            mtbDataCadastro.Text = string.Empty;
            mtbRevisao.Text = string.Empty;
        }
        public void CamposDisable()
        {
            #region Aba 1
            foreach (Control panPrin in panPrinAb1.Controls)
            {
                foreach (Control CONTROLs in panPrin.Controls)
                {
                    if (CONTROLs.GetType() == typeof(TextBox) && CONTROLs.Name != "txtCodigo")
                    {
                        (CONTROLs as TextBox).Enabled = false;
                    }
                    if (CONTROLs.GetType() == typeof(ComboBox))
                    {
                        (CONTROLs as ComboBox).Enabled = false;
                    }
                    if (CONTROLs.GetType() == typeof(MaskedTextBox))
                    {
                        (CONTROLs as MaskedTextBox).Enabled = false;
                    }
                    if (CONTROLs.GetType() == typeof(CheckBox))
                    {
                        (CONTROLs as CheckBox).Enabled = false;
                    }
                }
            }
            #endregion
            #region Aba 2
            foreach (Control panPrin in panPrinAb2.Controls)
            {
                foreach (Control CONTROLs in panPrin.Controls)
                {
                    if (CONTROLs.GetType() == typeof(TextBox))
                    {
                        (CONTROLs as TextBox).Enabled = false;
                    }
                    if (CONTROLs.GetType() == typeof(ComboBox))
                    {
                        (CONTROLs as ComboBox).Enabled = false;
                    }
                    if (CONTROLs.GetType() == typeof(MaskedTextBox))
                    {
                        (CONTROLs as MaskedTextBox).Enabled = false;
                    }
                    if (CONTROLs.GetType() == typeof(CheckBox))
                    {
                        (CONTROLs as CheckBox).Enabled = false;
                    }
                }
            }
            #endregion
        }
        public void CamposEnable()
        {
            #region Aba 1
            foreach (Control panPrin in panPrinAb1.Controls)
            {
                foreach (Control CONTROLs in panPrin.Controls)
                {
                    if (CONTROLs.GetType() == typeof(TextBox) && CONTROLs.AccessibleName != "1")
                    {
                        (CONTROLs as TextBox).Enabled = true;
                    }
                    if (CONTROLs.GetType() == typeof(ComboBox))
                    {
                        (CONTROLs as ComboBox).Enabled = true;
                    }
                    if (CONTROLs.GetType() == typeof(MaskedTextBox))
                    {
                        (CONTROLs as MaskedTextBox).Enabled = true;
                    }
                    if (CONTROLs.GetType() == typeof(CheckBox))
                    {
                        (CONTROLs as CheckBox).Enabled = true;
                    }
                }
            }
            #endregion
            #region Aba 2
            foreach (Control panPrin in panPrinAb2.Controls)
            {
                foreach (Control CONTROLs in panPrin.Controls)
                {
                    if (CONTROLs.GetType() == typeof(TextBox) && CONTROLs.AccessibleName != "1")
                    {
                        (CONTROLs as TextBox).Enabled = true;
                    }
                    if (CONTROLs.GetType() == typeof(ComboBox))
                    {
                        (CONTROLs as ComboBox).Enabled = true;
                    }
                    if (CONTROLs.GetType() == typeof(MaskedTextBox) && CONTROLs.AccessibleName != "1")
                    {
                        (CONTROLs as MaskedTextBox).Enabled = true;
                    }
                    if (CONTROLs.GetType() == typeof(CheckBox))
                    {
                        (CONTROLs as CheckBox).Enabled = true;
                    }
                }
            }
            #endregion

            if (comTipoPFPJ.SelectedIndex == 0)
            {
                #region HABILITA OS CAMPOS
                foreach (Control CTRL in panPesF.Controls)
                {
                    if (CTRL.GetType() == typeof(TextBox))
                    {
                        (CTRL as TextBox).Enabled = true;
                    }
                    if (CTRL.GetType() == typeof(MaskedTextBox))
                    {
                        (CTRL as MaskedTextBox).Enabled = true;
                    }
                    if (CTRL.GetType() == typeof(ComboBox))
                    {
                        (CTRL as ComboBox).Enabled = true;
                        (CTRL as ComboBox).SelectedIndex = 0;
                    }
                }
                foreach (Control CTRL in panPesJ.Controls)
                {
                    if (CTRL.GetType() == typeof(TextBox))
                    {
                        (CTRL as TextBox).Enabled = false;
                        (CTRL as TextBox).Text = string.Empty;
                    }
                    if (CTRL.GetType() == typeof(MaskedTextBox))
                    {
                        (CTRL as MaskedTextBox).Enabled = false;
                        (CTRL as MaskedTextBox).Text = string.Empty;
                    }
                    if (CTRL.GetType() == typeof(ComboBox))
                    {
                        (CTRL as ComboBox).Enabled = false;
                        (CTRL as ComboBox).SelectedIndex = 0;
                    }
                }
                #endregion
            }
            else
            {
                #region HABILITA OS CAMPOS
                foreach (Control CTRL in panPesF.Controls)
                {
                    if (CTRL.GetType() == typeof(TextBox))
                    {
                        (CTRL as TextBox).Enabled = false;
                        (CTRL as TextBox).Text = string.Empty;
                    }
                    if (CTRL.GetType() == typeof(MaskedTextBox))
                    {
                        (CTRL as MaskedTextBox).Enabled = false;
                        (CTRL as MaskedTextBox).Text = string.Empty;
                    }
                    if (CTRL.GetType() == typeof(ComboBox))
                    {
                        (CTRL as ComboBox).Enabled = false;
                        (CTRL as ComboBox).SelectedIndex = 0;
                    }
                }
                foreach (Control CTRL in panPesJ.Controls)
                {
                    if (CTRL.GetType() == typeof(TextBox))
                    {
                        (CTRL as TextBox).Enabled = true;
                    }
                    if (CTRL.GetType() == typeof(MaskedTextBox))
                    {
                        (CTRL as MaskedTextBox).Enabled = true;
                    }
                    if (CTRL.GetType() == typeof(ComboBox))
                    {
                        (CTRL as ComboBox).Enabled = true;
                    }
                }
                #endregion
            }
        }
        #endregion

        //REGION PARA ARMAZENAR TODOS OS BUTTONS DA PRIMERA ABA
        #region Buttons DO FORMULÁRIO

        #region Buttons Gravar, Zerar, Avançar, Fechar, Ajuda e Voltar


        //CLICK DA PRIMEIRA ABA
        private void btnGravar_Click(object sender, EventArgs e)
        {
            //VERIFICA OS CAMPOS OBRIGATÓRIOS
            TabClien_CamposObrig CamposObrig = new TabClien_CamposObrig();
            bool Obrig = CamposObrig.CamposObrig(txtMESTRE, txtCodigo, txtDescri, txtCodigoPrin, comTipoPFPJ, mtbCpfCnpj, txtFantasia, txtInscricaoEstadual, mtbVencEst, txtEmpresaCod, txtEmpresaDesc, txtConceito, txtEndCidadeFATU, txtEndCidDescriFATU, txtEndCidUFFATU, comContratoEmpresa, comCategoria, txtVendedorDesc, txtVendedorCod, txtEndCidadePERSO, txtEndCidDescriPERSO, txtTransportadoraCod, txtTransportadoraDesc, txtConvenioCod, txtConvenioDesc, txtCfop, txtCfopDesc, mtbVencMun, txtInscricaoMunicipal, txtValorLimiteCre, mtbDataVenciLimite, mtbContraInicio, mtbContraFim, mtbPfCpfConjuge, txtPfConjuge, comStatus, txtRotaSequen, txtRotaCod);
            if (!Obrig) { } else { return; }

            //VALIDAS OS CAMPOS DE DATAS
            #region VALIDA OS CAMPOS DE DATA
            TabClien_MET MET = new TabClien_MET();
            bool Campo1 = MET.MET_VerificaDATA(mtbVencEst, mtbVencEst);
            if (!Campo1) { } else { return; }

            bool Campo2 = MET.MET_VerificaDATA(mtbVencMun, mtbVencMun);
            if (!Campo2) { } else { return; }

            bool Campo3 = MET.MET_VerificaDATA(mtbDataVenciLimite, mtbDataVenciLimite);
            if (!Campo3) { } else { return; }

            bool Campo4 = MET.MET_VerificaDATA(mtbContraInicio, mtbContraInicio);
            if (!Campo4) { } else { return; }

            bool Campo5 = MET.MET_VerificaDATA(mtbContraFim, mtbContraFim);
            if (!Campo5) { } else { return; }
            #endregion

            //VALIDA O CPF CNPJ
            #region VALIDA CPF CNPJ
            if (comTipoPFPJ.SelectedIndex == 0)
            {
                bool VALIDAR = MET.MET_ValidaCPF(mtbCpfCnpj.Text);
                if (!VALIDAR)
                {
                    MessageBox.Show("CPF informado é inválido. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtbCpfCnpj.Select();
                    mtbCpfCnpj.SelectAll();
                    return;
                }
            }
            if (comTipoPFPJ.SelectedIndex == 1)
            {
                bool VALIDAR = MET.MET_ValidaCNPJ(mtbCpfCnpj.Text);
                if (!VALIDAR)
                {
                    MessageBox.Show("CNPJ informado é inválido. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtbCpfCnpj.Select();
                    mtbCpfCnpj.SelectAll();
                    return;
                }
            }
            #endregion

            TabClien_btnGravar GRAVAR = new TabClien_btnGravar();
            GRAVAR.GravarINC(txtCodigo, txtMESTRE, panUpAb1, ZerarCampos, CamposDisable, CamposEnable, btnGravar, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar, txtDescri, txtCodigoPrin, comTipoPFPJ, mtbCpfCnpj, comStatus, txtFantasia, mtbTelPabx, mtbTelCel1, comCategoria, txtObservacao, mtbTelFax, mtbTelCel2, txtInscricaoEstadual, mtbVencEst, txtRotaCod, txtVendedorCod, txtVendedorDesc, txtDesconto, txtBanco, txtAgencia, txtInscricaoMunicipal, mtbVencMun, txtReferencialCod, txtEmpresaCod, txtEmpresaDesc, txtRgIdentidade, txtConceito, txtEndLogradouroFATU, txtEndNumeroFATU, txtEndCidadeFATU, txtEndCidDescriFATU, txtEndCidUFFATU, mtbEndCepFATU, txtEndBairroFATU, txtEndCompleFATU, txtLabel, txtEndLogradouroPERSO, txtEndNumeroPERSO, txtEndCidadePERSO, txtEndCidDescriPERSO, txtEndCidUFPERSO, mtbEndCepPERSO, txtEndBairroPERSO, txtEndComplePERSO, txtTransportadoraCod, txtTransportadoraDesc, txtConvenioCod, txtConvenioDesc, txtValorLimiteCre, mtbDataVenciLimite, comSituacaoCredito, comFrete, comRecebimento, comCondicao, comTipoDeVenda, comContratoEmpresa, mtbContraInicio, mtbContraFim, txtCodigoAb2, txtDescriAb2, mtbCpfCnpjAb2, txtEmailContato1, txtEmailContato2, txtSkype, txtMsn, txtHomePage, txtFacebook, comSexo, txtPfLocalDeTrabalho, txtPfNaturalDe, txtPfCargo, mtbPfAdmissao, mtbPfAniversario, txtPfFiliacaoPai, txtPfFiliacaoMae, txtPfConjuge, mtbPfCpfConjuge, txtPjContatoProp1, txtPjContatoProp2, txtPjEscritorioContabilidade, txtPjDiaFatu, comPjAtividade, comPjRegimeTrib, txtPjCnae, comIcmsRdz, comIcmsST, txtCfop, txtCfopDesc, txtMsg1, txtMsg2, mtbDataCadastro, mtbRevisao, _Login_LojaID_TabClien, _Login_LojaDesc_TabClien, txtUsuario, txtRotaSequen);
            GRAVAR.GravarALT(txtCodigo, txtMESTRE, panUpAb1, ZerarCampos, CamposDisable, CamposEnable, btnGravar, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar, txtDescri, txtCodigoPrin, comTipoPFPJ, mtbCpfCnpj, comStatus, txtFantasia, mtbTelPabx, mtbTelCel1, comCategoria, txtObservacao, mtbTelFax, mtbTelCel2, txtInscricaoEstadual, mtbVencEst, txtRotaCod, txtVendedorCod, txtVendedorDesc, txtDesconto, txtBanco, txtAgencia, txtInscricaoMunicipal, mtbVencMun, txtReferencialCod, txtEmpresaCod, txtEmpresaDesc, txtRgIdentidade, txtConceito, txtEndLogradouroFATU, txtEndNumeroFATU, txtEndCidadeFATU, txtEndCidDescriFATU, txtEndCidUFFATU, mtbEndCepFATU, txtEndBairroFATU, txtEndCompleFATU, txtLabel, txtEndLogradouroPERSO, txtEndNumeroPERSO, txtEndCidadePERSO, txtEndCidDescriPERSO, txtEndCidUFPERSO, mtbEndCepPERSO, txtEndBairroPERSO, txtEndComplePERSO, txtTransportadoraCod, txtTransportadoraDesc, txtConvenioCod, txtConvenioDesc, txtValorLimiteCre, mtbDataVenciLimite, comSituacaoCredito, comFrete, comRecebimento, comCondicao, comTipoDeVenda, comContratoEmpresa, mtbContraInicio, mtbContraFim, txtCodigoAb2, txtDescriAb2, mtbCpfCnpjAb2, txtEmailContato1, txtEmailContato2, txtSkype, txtMsn, txtHomePage, txtFacebook, comSexo, txtPfLocalDeTrabalho, txtPfNaturalDe, txtPfCargo, mtbPfAdmissao, mtbPfAniversario, txtPfFiliacaoPai, txtPfFiliacaoMae, txtPfConjuge, mtbPfCpfConjuge, txtPjContatoProp1, txtPjContatoProp2, txtPjEscritorioContabilidade, txtPjDiaFatu, comPjAtividade, comPjRegimeTrib, txtPjCnae, comIcmsRdz, comIcmsST, txtCfop, txtCfopDesc, txtMsg1, txtMsg2, mtbDataCadastro, mtbRevisao, _Login_LojaID_TabClien, _Login_LojaDesc_TabClien, txtUsuario, txtRotaSequen);
            GRAVAR.GravarEXC(txtCodigo, txtMESTRE, panUpAb1, ZerarCampos, CamposDisable, CamposEnable, btnGravar, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar, txtDescri, txtCodigoPrin, comTipoPFPJ, mtbCpfCnpj, comStatus, txtFantasia, mtbTelPabx, mtbTelCel1, comCategoria, txtObservacao, mtbTelFax, mtbTelCel2, txtInscricaoEstadual, mtbVencEst, txtRotaCod, txtVendedorCod, txtVendedorDesc, txtDesconto, txtBanco, txtAgencia, txtInscricaoMunicipal, mtbVencMun, txtReferencialCod, txtEmpresaCod, txtEmpresaDesc, txtRgIdentidade, txtConceito, txtEndLogradouroFATU, txtEndNumeroFATU, txtEndCidadeFATU, txtEndCidDescriFATU, txtEndCidUFFATU, mtbEndCepFATU, txtEndBairroFATU, txtEndCompleFATU, txtLabel, txtEndLogradouroPERSO, txtEndNumeroPERSO, txtEndCidadePERSO, txtEndCidDescriPERSO, txtEndCidUFPERSO, mtbEndCepPERSO, txtEndBairroPERSO, txtEndComplePERSO, txtTransportadoraCod, txtTransportadoraDesc, txtConvenioCod, txtConvenioDesc, txtValorLimiteCre, mtbDataVenciLimite, comSituacaoCredito, comFrete, comRecebimento, comCondicao, comTipoDeVenda, comContratoEmpresa, mtbContraInicio, mtbContraFim, txtCodigoAb2, txtDescriAb2, mtbCpfCnpjAb2, txtEmailContato1, txtEmailContato2, txtSkype, txtMsn, txtHomePage, txtFacebook, comSexo, txtPfLocalDeTrabalho, txtPfNaturalDe, txtPfCargo, mtbPfAdmissao, mtbPfAniversario, txtPfFiliacaoPai, txtPfFiliacaoMae, txtPfConjuge, mtbPfCpfConjuge, txtPjContatoProp1, txtPjContatoProp2, txtPjEscritorioContabilidade, txtPjDiaFatu, comPjAtividade, comPjRegimeTrib, txtPjCnae, comIcmsRdz, comIcmsST, txtCfop, txtCfopDesc, txtMsg1, txtMsg2, mtbDataCadastro, mtbRevisao, _Login_LojaID_TabClien, _Login_LojaDesc_TabClien, txtUsuario, _MOTIVO_EXCLUSAO);

        }
        //CLICK DA SEGUNDA ABA
        private void btnGravarAb2_Click(object sender, EventArgs e)
        {
            TabClien_MET MET = new TabClien_MET();

            //VALIDAS OS CAMPOS DE DATAS
            #region VALIDA OS CAMPOS DE DATA
            bool Campo1 = MET.MET_VerificaDATA(mtbPfAdmissao, mtbPfAdmissao);
            if (!Campo1) { } else { return; }

            bool Campo2 = MET.MET_VerificaDATA(mtbPfAniversario, mtbPfAniversario);
            if (!Campo2) { } else { return; }

            #endregion

            //VALIDA O CPF CNPJ
            #region VALIDAR CPF CONJUGE
            if (mtbPfCpfConjuge.Text.Count(c => char.IsLetterOrDigit(c)) != 0)
            {
                bool VALIDAR = MET.MET_ValidaCPF(mtbPfCpfConjuge.Text);
                if (!VALIDAR)
                {
                    MessageBox.Show("CPF informado é inválido. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtbPfCpfConjuge.Select();
                    mtbPfCpfConjuge.SelectAll();
                    return;
                }
            }
            #endregion

            TabClien_btnGravar GRAVAR = new TabClien_btnGravar();
            GRAVAR.GravarINC(txtCodigo, txtMESTRE, panUpAb1, ZerarCampos, CamposDisable, CamposEnable, btnGravar, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar, txtDescri, txtCodigoPrin, comTipoPFPJ, mtbCpfCnpj, comStatus, txtFantasia, mtbTelPabx, mtbTelCel1, comCategoria, txtObservacao, mtbTelFax, mtbTelCel2, txtInscricaoEstadual, mtbVencEst, txtRotaCod, txtVendedorCod, txtVendedorDesc, txtDesconto, txtBanco, txtAgencia, txtInscricaoMunicipal, mtbVencMun, txtReferencialCod, txtEmpresaCod, txtEmpresaDesc, txtRgIdentidade, txtConceito, txtEndLogradouroFATU, txtEndNumeroFATU, txtEndCidadeFATU, txtEndCidDescriFATU, txtEndCidUFFATU, mtbEndCepFATU, txtEndBairroFATU, txtEndCompleFATU, txtLabel, txtEndLogradouroPERSO, txtEndNumeroPERSO, txtEndCidadePERSO, txtEndCidDescriPERSO, txtEndCidUFPERSO, mtbEndCepPERSO, txtEndBairroPERSO, txtEndComplePERSO, txtTransportadoraCod, txtTransportadoraDesc, txtConvenioCod, txtConvenioDesc, txtValorLimiteCre, mtbDataVenciLimite, comSituacaoCredito, comFrete, comRecebimento, comCondicao, comTipoDeVenda, comContratoEmpresa, mtbContraInicio, mtbContraFim, txtCodigoAb2, txtDescriAb2, mtbCpfCnpjAb2, txtEmailContato1, txtEmailContato2, txtSkype, txtMsn, txtHomePage, txtFacebook, comSexo, txtPfLocalDeTrabalho, txtPfNaturalDe, txtPfCargo, mtbPfAdmissao, mtbPfAniversario, txtPfFiliacaoPai, txtPfFiliacaoMae, txtPfConjuge, mtbPfCpfConjuge, txtPjContatoProp1, txtPjContatoProp2, txtPjEscritorioContabilidade, txtPjDiaFatu, comPjAtividade, comPjRegimeTrib, txtPjCnae, comIcmsRdz, comIcmsST, txtCfop, txtCfopDesc, txtMsg1, txtMsg2, mtbDataCadastro, mtbRevisao, _Login_LojaID_TabClien, _Login_LojaDesc_TabClien, txtUsuario, txtRotaSequen);
            GRAVAR.GravarALT(txtCodigo, txtMESTRE, panUpAb1, ZerarCampos, CamposDisable, CamposEnable, btnGravar, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar, txtDescri, txtCodigoPrin, comTipoPFPJ, mtbCpfCnpj, comStatus, txtFantasia, mtbTelPabx, mtbTelCel1, comCategoria, txtObservacao, mtbTelFax, mtbTelCel2, txtInscricaoEstadual, mtbVencEst, txtRotaCod, txtVendedorCod, txtVendedorDesc, txtDesconto, txtBanco, txtAgencia, txtInscricaoMunicipal, mtbVencMun, txtReferencialCod, txtEmpresaCod, txtEmpresaDesc, txtRgIdentidade, txtConceito, txtEndLogradouroFATU, txtEndNumeroFATU, txtEndCidadeFATU, txtEndCidDescriFATU, txtEndCidUFFATU, mtbEndCepFATU, txtEndBairroFATU, txtEndCompleFATU, txtLabel, txtEndLogradouroPERSO, txtEndNumeroPERSO, txtEndCidadePERSO, txtEndCidDescriPERSO, txtEndCidUFPERSO, mtbEndCepPERSO, txtEndBairroPERSO, txtEndComplePERSO, txtTransportadoraCod, txtTransportadoraDesc, txtConvenioCod, txtConvenioDesc, txtValorLimiteCre, mtbDataVenciLimite, comSituacaoCredito, comFrete, comRecebimento, comCondicao, comTipoDeVenda, comContratoEmpresa, mtbContraInicio, mtbContraFim, txtCodigoAb2, txtDescriAb2, mtbCpfCnpjAb2, txtEmailContato1, txtEmailContato2, txtSkype, txtMsn, txtHomePage, txtFacebook, comSexo, txtPfLocalDeTrabalho, txtPfNaturalDe, txtPfCargo, mtbPfAdmissao, mtbPfAniversario, txtPfFiliacaoPai, txtPfFiliacaoMae, txtPfConjuge, mtbPfCpfConjuge, txtPjContatoProp1, txtPjContatoProp2, txtPjEscritorioContabilidade, txtPjDiaFatu, comPjAtividade, comPjRegimeTrib, txtPjCnae, comIcmsRdz, comIcmsST, txtCfop, txtCfopDesc, txtMsg1, txtMsg2, mtbDataCadastro, mtbRevisao, _Login_LojaID_TabClien, _Login_LojaDesc_TabClien, txtUsuario, txtRotaSequen);


            TabControl.SelectedTab = Tp1;
        }
        //ZERA OS CAMPOS
        private void btnZerar_Click(object sender, EventArgs e)
        {
            ZerarCampos();
            //DEFINE A APARENCIA DO BUTTON
            TabClien_AppaButtons Appa = new TabClien_AppaButtons();
            Appa._ButtonZER(txtMESTRE, txtCodigo, btnIncluir, btnGravar, CamposDisable, TabControl, Tp1, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar);
        }
        //AVANÇA
        private void btnAvancar_Click(object sender, EventArgs e)
        {
            if (txtMESTRE.Text == "ALTERAR" || txtMESTRE.Text == "INCLUIR")
            {
                //VERIFICA OS CAMPOS OBRIGATÓRIOS
                TabClien_CamposObrig CamposObrig = new TabClien_CamposObrig();
                bool Obrig = CamposObrig.CamposObrig(txtMESTRE, txtCodigo, txtDescri, txtCodigoPrin, comTipoPFPJ, mtbCpfCnpj, txtFantasia, txtInscricaoEstadual, mtbVencEst, txtEmpresaCod, txtEmpresaDesc, txtConceito, txtEndCidadeFATU, txtEndCidDescriFATU, txtEndCidUFFATU, comContratoEmpresa, comCategoria, txtVendedorDesc, txtVendedorCod, txtEndCidadePERSO, txtEndCidDescriPERSO, txtTransportadoraCod, txtTransportadoraDesc, txtConvenioCod, txtConvenioDesc, txtCfop, txtCfopDesc, mtbVencMun, txtInscricaoMunicipal, txtValorLimiteCre, mtbDataVenciLimite, mtbContraInicio, mtbContraFim, mtbPfCpfConjuge, txtPfConjuge, comStatus, txtRotaSequen, txtRotaCod);
                if (!Obrig) { } else { return; }

                //VALIDAS OS CAMPOS DE DATAS
                #region VALIDA OS CAMPOS DE DATA
                TabClien_MET MET = new TabClien_MET();
                bool Campo1 = MET.MET_VerificaDATA(mtbVencEst, mtbVencEst);
                if (!Campo1) { } else { return; }

                bool Campo2 = MET.MET_VerificaDATA(mtbVencMun, mtbVencMun);
                if (!Campo2) { } else { return; }

                bool Campo3 = MET.MET_VerificaDATA(mtbDataVenciLimite, mtbDataVenciLimite);
                if (!Campo3) { } else { return; }

                bool Campo4 = MET.MET_VerificaDATA(mtbContraInicio, mtbContraInicio);
                if (!Campo4) { } else { return; }

                bool Campo5 = MET.MET_VerificaDATA(mtbContraFim, mtbContraFim);
                if (!Campo5) { } else { return; }
                #endregion

                //VALIDA O CPF CNPJ
                #region VALIDA CPF CNPJ
                if (comTipoPFPJ.SelectedIndex == 0)
                {
                    bool VALIDAR = MET.MET_ValidaCPF(mtbCpfCnpj.Text);
                    if (!VALIDAR)
                    {
                        MessageBox.Show("CPF informado é inválido. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mtbCpfCnpj.Select();
                        mtbCpfCnpj.SelectAll();
                        return;
                    }
                }
                if (comTipoPFPJ.SelectedIndex == 1)
                {
                    bool VALIDAR = MET.MET_ValidaCNPJ(mtbCpfCnpj.Text);
                    if (!VALIDAR)
                    {
                        MessageBox.Show("CNPJ informado é inválido. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mtbCpfCnpj.Select();
                        mtbCpfCnpj.SelectAll();
                        return;
                    }
                }
                #endregion
            }

            TabControl.SelectedTab = Tp2;
        }
        //FECHA O FORM
        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        //FECHA O FORM
        private void btnFecharAb2_Click(object sender, EventArgs e)
        {
            Close();
        }
        //EXIBE A AJUDA
        private void btnAjuda_Click(object sender, EventArgs e)
        {
            //EXIBE A AJUDA
            TabClien_FILTROS FILT = new TabClien_FILTROS();
            FILT.LinkAjuda();
        }
        //VOLTA
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            TabClien_MET MET = new TabClien_MET();

            //VALIDAS OS CAMPOS DE DATAS
            #region VALIDA OS CAMPOS DE DATA
            bool Campo1 = MET.MET_VerificaDATA(mtbPfAdmissao, mtbPfAdmissao);
            if (!Campo1) { } else { return; }

            bool Campo2 = MET.MET_VerificaDATA(mtbPfAniversario, mtbPfAniversario);
            if (!Campo2) { } else { return; }

            #endregion

            //VALIDA O CPF
            #region VALIDAR CPF CONJUGE
            if (mtbPfCpfConjuge.Text.Count(c => char.IsLetterOrDigit(c)) != 0)
            {
                bool VALIDAR = MET.MET_ValidaCPF(mtbPfCpfConjuge.Text);
                if (!VALIDAR)
                {
                    MessageBox.Show("CPF informado é inválido. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtbPfCpfConjuge.Select();
                    mtbPfCpfConjuge.SelectAll();
                    return;
                }
            }
            #endregion

            TabControl.SelectedTab = Tp1;
        }


       
        #endregion


        #region Buttons Inf Financeiras e Inf Comerciais
        private void btnInfFinaShow_Click(object sender, EventArgs e)
        {

        }

        private void btnInfComerShow_Click(object sender, EventArgs e)
        {

        }
        #endregion


        #region Buttons INCLUIR, ALTERAR e EXCLUIR
        //BUT INCLUIR
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            //DEFINE A APARENCIA DO BUTTON
            TabClien_AppaButtons Appa = new TabClien_AppaButtons();
            Appa._ButtonINC(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar);

            //SELECIONA O ULTIMO + 1
            TabClien_MET MET = new TabClien_MET();
            MET.MET_SelecionaUltimoRegistroMaisUm(txtCodigo, btnGravar);
        }
        //BUT ALTERAR
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //DEFINE A APARENCIA DO BUTTON
            TabClien_AppaButtons Appa = new TabClien_AppaButtons();
            Appa._ButtonALT(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar);
        }
        //BUT EXCLUIR
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //DEFINE A APARENCIA DO BUTTON
            TabClien_AppaButtons Appa = new TabClien_AppaButtons();
            Appa._ButtonEXC(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar);
        }
        #endregion


        #region Buttons Seta 1,2,3 e 4
        private void btnSeta1_Click(object sender, EventArgs e)
        {
            //APARENCIA AO APERTAR O BUTTON
            TabClien_AppaButtons Appa = new TabClien_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar);
            //EXECUTA A SETA
            TabClien_ExecSETAS EXEC = new TabClien_ExecSETAS();
            EXEC.ExecSETAS("1", txtCodigo, txtMESTRE, panUpAb1, ZerarCampos, CamposDisable, CamposEnable, btnGravar, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar, txtDescri, txtCodigoPrin, comTipoPFPJ, mtbCpfCnpj, comStatus, txtFantasia, mtbTelPabx, mtbTelCel1, comCategoria, txtObservacao, mtbTelFax, mtbTelCel2, txtInscricaoEstadual, mtbVencEst, txtRotaCod, txtVendedorCod, txtVendedorDesc, txtDesconto, txtBanco, txtAgencia, txtInscricaoMunicipal, mtbVencMun, txtReferencialCod, txtEmpresaCod, txtEmpresaDesc, txtRgIdentidade, txtConceito, txtEndLogradouroFATU, txtEndNumeroFATU, txtEndCidadeFATU, txtEndCidDescriFATU, txtEndCidUFFATU, mtbEndCepFATU, txtEndBairroFATU, txtEndCompleFATU, txtLabel, txtEndLogradouroPERSO, txtEndNumeroPERSO, txtEndCidadePERSO, txtEndCidDescriPERSO, txtEndCidUFPERSO, mtbEndCepPERSO, txtEndBairroPERSO, txtEndComplePERSO, txtTransportadoraCod, txtTransportadoraDesc, txtConvenioCod, txtConvenioDesc, txtValorLimiteCre, mtbDataVenciLimite, comSituacaoCredito, comFrete, comRecebimento, comCondicao, comTipoDeVenda, comContratoEmpresa, mtbContraInicio, mtbContraFim, txtCodigoAb2, txtDescriAb2, mtbCpfCnpjAb2, txtEmailContato1, txtEmailContato2, txtSkype, txtMsn, txtHomePage, txtFacebook, comSexo, txtPfLocalDeTrabalho, txtPfNaturalDe, txtPfCargo, mtbPfAdmissao, mtbPfAniversario, txtPfFiliacaoPai, txtPfFiliacaoMae, txtPfConjuge, mtbPfCpfConjuge, txtPjContatoProp1, txtPjContatoProp2, txtPjEscritorioContabilidade, txtPjDiaFatu, comPjAtividade, comPjRegimeTrib, txtPjCnae, comIcmsRdz, comIcmsST, txtCfop, txtCfopDesc, txtMsg1, txtMsg2, mtbDataCadastro, mtbRevisao, _Login_LojaID_TabClien, _Login_LojaDesc_TabClien, txtRotaSequen);
        }
        private void btnSeta2_Click(object sender, EventArgs e)
        {
            //APARENCIA AO APERTAR O BUTTON
            TabClien_AppaButtons Appa = new TabClien_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar);
            //EXECUTA A SETA
            TabClien_ExecSETAS EXEC = new TabClien_ExecSETAS();
            EXEC.ExecSETAS("2", txtCodigo, txtMESTRE, panUpAb1, ZerarCampos, CamposDisable, CamposEnable, btnGravar, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar, txtDescri, txtCodigoPrin, comTipoPFPJ, mtbCpfCnpj, comStatus, txtFantasia, mtbTelPabx, mtbTelCel1, comCategoria, txtObservacao, mtbTelFax, mtbTelCel2, txtInscricaoEstadual, mtbVencEst, txtRotaCod, txtVendedorCod, txtVendedorDesc, txtDesconto, txtBanco, txtAgencia, txtInscricaoMunicipal, mtbVencMun, txtReferencialCod, txtEmpresaCod, txtEmpresaDesc, txtRgIdentidade, txtConceito, txtEndLogradouroFATU, txtEndNumeroFATU, txtEndCidadeFATU, txtEndCidDescriFATU, txtEndCidUFFATU, mtbEndCepFATU, txtEndBairroFATU, txtEndCompleFATU, txtLabel, txtEndLogradouroPERSO, txtEndNumeroPERSO, txtEndCidadePERSO, txtEndCidDescriPERSO, txtEndCidUFPERSO, mtbEndCepPERSO, txtEndBairroPERSO, txtEndComplePERSO, txtTransportadoraCod, txtTransportadoraDesc, txtConvenioCod, txtConvenioDesc, txtValorLimiteCre, mtbDataVenciLimite, comSituacaoCredito, comFrete, comRecebimento, comCondicao, comTipoDeVenda, comContratoEmpresa, mtbContraInicio, mtbContraFim, txtCodigoAb2, txtDescriAb2, mtbCpfCnpjAb2, txtEmailContato1, txtEmailContato2, txtSkype, txtMsn, txtHomePage, txtFacebook, comSexo, txtPfLocalDeTrabalho, txtPfNaturalDe, txtPfCargo, mtbPfAdmissao, mtbPfAniversario, txtPfFiliacaoPai, txtPfFiliacaoMae, txtPfConjuge, mtbPfCpfConjuge, txtPjContatoProp1, txtPjContatoProp2, txtPjEscritorioContabilidade, txtPjDiaFatu, comPjAtividade, comPjRegimeTrib, txtPjCnae, comIcmsRdz, comIcmsST, txtCfop, txtCfopDesc, txtMsg1, txtMsg2, mtbDataCadastro, mtbRevisao, _Login_LojaID_TabClien, _Login_LojaDesc_TabClien, txtRotaSequen);
        }
        private void btnSeta3_Click(object sender, EventArgs e)
        {
            //APARENCIA AO APERTAR O BUTTON
            TabClien_AppaButtons Appa = new TabClien_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar);
            //EXECUTA A SETA
            TabClien_ExecSETAS EXEC = new TabClien_ExecSETAS();
            EXEC.ExecSETAS("3", txtCodigo, txtMESTRE, panUpAb1, ZerarCampos, CamposDisable, CamposEnable, btnGravar, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar, txtDescri, txtCodigoPrin, comTipoPFPJ, mtbCpfCnpj, comStatus, txtFantasia, mtbTelPabx, mtbTelCel1, comCategoria, txtObservacao, mtbTelFax, mtbTelCel2, txtInscricaoEstadual, mtbVencEst, txtRotaCod, txtVendedorCod, txtVendedorDesc, txtDesconto, txtBanco, txtAgencia, txtInscricaoMunicipal, mtbVencMun, txtReferencialCod, txtEmpresaCod, txtEmpresaDesc, txtRgIdentidade, txtConceito, txtEndLogradouroFATU, txtEndNumeroFATU, txtEndCidadeFATU, txtEndCidDescriFATU, txtEndCidUFFATU, mtbEndCepFATU, txtEndBairroFATU, txtEndCompleFATU, txtLabel, txtEndLogradouroPERSO, txtEndNumeroPERSO, txtEndCidadePERSO, txtEndCidDescriPERSO, txtEndCidUFPERSO, mtbEndCepPERSO, txtEndBairroPERSO, txtEndComplePERSO, txtTransportadoraCod, txtTransportadoraDesc, txtConvenioCod, txtConvenioDesc, txtValorLimiteCre, mtbDataVenciLimite, comSituacaoCredito, comFrete, comRecebimento, comCondicao, comTipoDeVenda, comContratoEmpresa, mtbContraInicio, mtbContraFim, txtCodigoAb2, txtDescriAb2, mtbCpfCnpjAb2, txtEmailContato1, txtEmailContato2, txtSkype, txtMsn, txtHomePage, txtFacebook, comSexo, txtPfLocalDeTrabalho, txtPfNaturalDe, txtPfCargo, mtbPfAdmissao, mtbPfAniversario, txtPfFiliacaoPai, txtPfFiliacaoMae, txtPfConjuge, mtbPfCpfConjuge, txtPjContatoProp1, txtPjContatoProp2, txtPjEscritorioContabilidade, txtPjDiaFatu, comPjAtividade, comPjRegimeTrib, txtPjCnae, comIcmsRdz, comIcmsST, txtCfop, txtCfopDesc, txtMsg1, txtMsg2, mtbDataCadastro, mtbRevisao, _Login_LojaID_TabClien, _Login_LojaDesc_TabClien, txtRotaSequen);
        }
        private void btnSeta4_Click(object sender, EventArgs e)
        {
            //APARENCIA AO APERTAR O BUTTON
            TabClien_AppaButtons Appa = new TabClien_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar);
            //EXECUTA A SETA
            TabClien_ExecSETAS EXEC = new TabClien_ExecSETAS();
            EXEC.ExecSETAS("4", txtCodigo, txtMESTRE, panUpAb1, ZerarCampos, CamposDisable, CamposEnable, btnGravar, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar, txtDescri, txtCodigoPrin, comTipoPFPJ, mtbCpfCnpj, comStatus, txtFantasia, mtbTelPabx, mtbTelCel1, comCategoria, txtObservacao, mtbTelFax, mtbTelCel2, txtInscricaoEstadual, mtbVencEst, txtRotaCod, txtVendedorCod, txtVendedorDesc, txtDesconto, txtBanco, txtAgencia, txtInscricaoMunicipal, mtbVencMun, txtReferencialCod, txtEmpresaCod, txtEmpresaDesc, txtRgIdentidade, txtConceito, txtEndLogradouroFATU, txtEndNumeroFATU, txtEndCidadeFATU, txtEndCidDescriFATU, txtEndCidUFFATU, mtbEndCepFATU, txtEndBairroFATU, txtEndCompleFATU, txtLabel, txtEndLogradouroPERSO, txtEndNumeroPERSO, txtEndCidadePERSO, txtEndCidDescriPERSO, txtEndCidUFPERSO, mtbEndCepPERSO, txtEndBairroPERSO, txtEndComplePERSO, txtTransportadoraCod, txtTransportadoraDesc, txtConvenioCod, txtConvenioDesc, txtValorLimiteCre, mtbDataVenciLimite, comSituacaoCredito, comFrete, comRecebimento, comCondicao, comTipoDeVenda, comContratoEmpresa, mtbContraInicio, mtbContraFim, txtCodigoAb2, txtDescriAb2, mtbCpfCnpjAb2, txtEmailContato1, txtEmailContato2, txtSkype, txtMsn, txtHomePage, txtFacebook, comSexo, txtPfLocalDeTrabalho, txtPfNaturalDe, txtPfCargo, mtbPfAdmissao, mtbPfAniversario, txtPfFiliacaoPai, txtPfFiliacaoMae, txtPfConjuge, mtbPfCpfConjuge, txtPjContatoProp1, txtPjContatoProp2, txtPjEscritorioContabilidade, txtPjDiaFatu, comPjAtividade, comPjRegimeTrib, txtPjCnae, comIcmsRdz, comIcmsST, txtCfop, txtCfopDesc, txtMsg1, txtMsg2, mtbDataCadastro, mtbRevisao, _Login_LojaID_TabClien, _Login_LojaDesc_TabClien, txtRotaSequen);
        }
        #endregion

        #endregion




        //EXIBE OS VALORES NO TAB
        public string _MOTIVO_EXCLUSAO { get; set; }
        private void txtCodigo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //SELECIONA O CLIENTE NO TAB
                TabClien_MET MET = new TabClien_MET();
                MET.MET_SelecionaCodigoTAB(txtCodigo, txtMESTRE, panUpAb1, ZerarCampos, CamposDisable, CamposEnable, btnGravar, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar, txtDescri, txtCodigoPrin, comTipoPFPJ, mtbCpfCnpj, comStatus, txtFantasia, mtbTelPabx, mtbTelCel1, comCategoria, txtObservacao, mtbTelFax, mtbTelCel2, txtInscricaoEstadual, mtbVencEst, txtRotaCod, txtVendedorCod, txtVendedorDesc, txtDesconto, txtBanco, txtAgencia, txtInscricaoMunicipal, mtbVencMun, txtReferencialCod, txtEmpresaCod, txtEmpresaDesc, txtRgIdentidade, txtConceito, txtEndLogradouroFATU, txtEndNumeroFATU, txtEndCidadeFATU, txtEndCidDescriFATU, txtEndCidUFFATU, mtbEndCepFATU, txtEndBairroFATU, txtEndCompleFATU, txtLabel, txtEndLogradouroPERSO, txtEndNumeroPERSO, txtEndCidadePERSO, txtEndCidDescriPERSO, txtEndCidUFPERSO, mtbEndCepPERSO, txtEndBairroPERSO, txtEndComplePERSO, txtTransportadoraCod, txtTransportadoraDesc, txtConvenioCod, txtConvenioDesc, txtValorLimiteCre, mtbDataVenciLimite, comSituacaoCredito, comFrete, comRecebimento, comCondicao, comTipoDeVenda, comContratoEmpresa, mtbContraInicio, mtbContraFim, txtCodigoAb2, txtDescriAb2, mtbCpfCnpjAb2, txtEmailContato1, txtEmailContato2, txtSkype, txtMsn, txtHomePage, txtFacebook, comSexo, txtPfLocalDeTrabalho, txtPfNaturalDe, txtPfCargo, mtbPfAdmissao, mtbPfAniversario, txtPfFiliacaoPai, txtPfFiliacaoMae, txtPfConjuge, mtbPfCpfConjuge, txtPjContatoProp1, txtPjContatoProp2, txtPjEscritorioContabilidade, txtPjDiaFatu, comPjAtividade, comPjRegimeTrib, txtPjCnae, comIcmsRdz, comIcmsST, txtCfop, txtCfopDesc, txtMsg1, txtMsg2, mtbDataCadastro, mtbRevisao, _Login_LojaID_TabClien, _Login_LojaDesc_TabClien, txtRotaSequen);
                //Faz o tratamento quando chama o form de exlusão
                #region TRATAMENTO PARA EXCLUSÃO
                if (txtMESTRE.Text == "EXCLUIR" && txtDescri.Text != string.Empty)
                {
                    FromEx Ex = new FromEx();
                    Ex.ShowDialog();
                    _MOTIVO_EXCLUSAO = Ex._MotivoExclusão;
                    if (_MOTIVO_EXCLUSAO != string.Empty)
                    {
                        btnGravar.PerformClick();
                    }

                    btnInfFinaShow.Enabled = false;
                    btnInfComerShow.Enabled = false;
                    btnAvancar.Enabled = false;
                    btnGravar.Enabled = false;
                    btnGravarAb2.Enabled = false;
                    btnVoltar.Enabled = false;

                    ZerarCampos();
                    CamposDisable();
                    panUpAb1.Focus();
                    txtCodigo.SelectAll();
                }
                #endregion
            }
        }








        //FAZ A MASCARA DO CPF CNPJ
        private void comTipoPFPJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PF
            if (comTipoPFPJ.SelectedIndex == 0)
            {
                mtbCpfCnpj.Text = string.Empty;
                mtbCpfCnpjAb2.Text = string.Empty;
                mtbCpfCnpj.Mask = "000,000,000-00";
                mtbCpfCnpjAb2.Mask = "000,000,000-00";
                lblFantasia.Text = "APELIDO...:";
                lblFantasia.AccessibleName = "Apelido";
                #region HABILITA OS CAMPOS
                foreach (Control CTRL in panPesF.Controls)
                {
                    if (CTRL.GetType() == typeof(TextBox))
                    {
                        (CTRL as TextBox).Enabled = true;
                        (CTRL as TextBox).Text = string.Empty;
                    }
                    if (CTRL.GetType() == typeof(MaskedTextBox))
                    {
                        (CTRL as MaskedTextBox).Enabled = true;
                        (CTRL as MaskedTextBox).Text = string.Empty;
                    }
                    if (CTRL.GetType() == typeof(ComboBox))
                    {
                        (CTRL as ComboBox).Enabled = true;
                        (CTRL as ComboBox).SelectedIndex = -1;
                    }
                }
                foreach (Control CTRL in panPesJ.Controls)
                {
                    if (CTRL.GetType() == typeof(TextBox))
                    {
                        (CTRL as TextBox).Enabled = false;
                        (CTRL as TextBox).Text = string.Empty;
                    }
                    if (CTRL.GetType() == typeof(MaskedTextBox))
                    {
                        (CTRL as MaskedTextBox).Enabled = false;
                        (CTRL as MaskedTextBox).Text = string.Empty;
                    }
                    if (CTRL.GetType() == typeof(ComboBox))
                    {
                        (CTRL as ComboBox).Enabled = false;
                        (CTRL as ComboBox).SelectedIndex = -1;
                    }
                }
                #endregion

                if (txtMESTRE.Text == "INCLUIR")
                {
                    comCategoria.SelectedIndex = 0;
                }
            }
            //PJ
            else if (comTipoPFPJ.SelectedIndex == 1)
            {
                mtbCpfCnpj.Text = string.Empty;
                mtbCpfCnpjAb2.Text = string.Empty;
                mtbCpfCnpj.Mask = "00,000,000/0000-00";
                mtbCpfCnpjAb2.Mask = "00,000,000/0000-00";
                lblFantasia.Text = "FANTASIA..:";
                lblFantasia.AccessibleName = "Fantasia";
                #region HABILITA OS CAMPOS
                foreach (Control CTRL in panPesF.Controls)
                {
                    if (CTRL.GetType() == typeof(TextBox))
                    {
                        (CTRL as TextBox).Enabled = false;
                        (CTRL as TextBox).Text = string.Empty;
                    }
                    if (CTRL.GetType() == typeof(MaskedTextBox))
                    {
                        (CTRL as MaskedTextBox).Enabled = false;
                        (CTRL as MaskedTextBox).Text = string.Empty;
                    }
                    if (CTRL.GetType() == typeof(ComboBox))
                    {
                        (CTRL as ComboBox).Enabled = false;
                        (CTRL as ComboBox).SelectedIndex = -1;
                    }
                }
                foreach (Control CTRL in panPesJ.Controls)
                {
                    if (CTRL.GetType() == typeof(TextBox))
                    {
                        (CTRL as TextBox).Enabled = true;
                        (CTRL as TextBox).Text = string.Empty;
                    }
                    if (CTRL.GetType() == typeof(MaskedTextBox))
                    {
                        (CTRL as MaskedTextBox).Enabled = true;
                        (CTRL as MaskedTextBox).Text = string.Empty;
                    }
                    if (CTRL.GetType() == typeof(ComboBox))
                    {
                        (CTRL as ComboBox).Enabled = true;
                        (CTRL as ComboBox).SelectedIndex = -1;
                    }
                }
                #endregion

                if (txtMESTRE.Text == "INCLUIR")
                {
                    comCategoria.SelectedIndex = 1;
                }
            }
            else
            {
                mtbCpfCnpj.Text = string.Empty;
                mtbCpfCnpjAb2.Text = string.Empty;
                mtbCpfCnpj.Mask = "";
                mtbCpfCnpjAb2.Mask = "";
            }
        }
        //TORNA OBRIGATÓRIO
        private void comTipoPFPJ_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (comTipoPFPJ.SelectedIndex < 0)
                {
                    MessageBox.Show("Campos (Tipo) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigoPrin.Focus(); comTipoPFPJ.SelectAll();
                }

                SendKeys.Send("{HOME}");
                SendKeys.Send("+{END}");
            }
        }



        //SELECIONA AS FKs E TORNA OBRIGATÓRIO NO TAB
        #region SELECIONA AS FKs

        //OBRIGAÇÃO NO TAB, E SELECIONA A CIDADE
        private void txtEndCidadeFATU_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtEndCidadeFATU.Text == string.Empty || Convert.ToInt32(txtEndCidadeFATU.Text) == 0)
                {
                    txtEndCidadeFATU.Text = txtEndCidadeFATU.Text.PadLeft(6, '0');
                    txtEndCidDescriFATU.Text = string.Empty; txtEndCidUFFATU.Text = string.Empty;
                    MessageBox.Show("Campo (Cidade) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEndNumeroFATU.Select(); txtEndCidadeFATU.SelectAll();
                    return;
                }
                TabClien_MET MET = new TabClien_MET();
                MET.MET_SelecionaCidadeTAB(txtEndCidadeFATU, txtEndCidDescriFATU, txtEndCidUFFATU, txtEndNumeroFATU);
            }
        }
        //SELECIONA A CIDADE
        private void txtEndCidadePERSO_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtEndCidadePERSO.Text != string.Empty)
            {
                if (Convert.ToInt32(txtEndCidadePERSO.Text) == 0)
                {
                    txtEndCidadePERSO.Text = string.Empty;
                    txtEndCidDescriPERSO.Text = string.Empty;
                    txtEndCidUFPERSO.Text = string.Empty;
                }
                else
                {
                    TabClien_MET MET = new TabClien_MET();
                    MET.MET_SelecionaCidadeTAB(txtEndCidadePERSO, txtEndCidDescriPERSO, txtEndCidUFPERSO, txtEndNumeroPERSO);
                }
            }
            if (e.KeyCode == Keys.Tab && txtEndCidadePERSO.Text == string.Empty)
            {
                txtEndCidadePERSO.Text = string.Empty;
                txtEndCidDescriPERSO.Text = string.Empty;
                txtEndCidUFPERSO.Text = string.Empty;
            }
        }
        //OBRIGAÇÃO NO TAB, E SELECIONA A EMPRESA
        private void txtEmpresaCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtEmpresaCod.Text == string.Empty || Convert.ToInt32(txtEmpresaCod.Text) == 0)
                {
                    txtEmpresaCod.Text = txtEmpresaCod.Text.PadLeft(6, '0');
                    txtEmpresaDesc.Text = string.Empty;
                    MessageBox.Show("Campo (Empresa) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtReferencialCod.Select(); txtEmpresaCod.SelectAll();
                    return;
                }
                TabClien_MET MET = new TabClien_MET();
                MET.MET_SelecionaEmpresTAB(txtEmpresaCod, txtEmpresaDesc, txtReferencialCod);
            }
        }
        //OBRIGAÇÃO NO TAB, E SELECIONA O CLIENTE
        private void txtCodigoPrin_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtCodigoPrin.Text == string.Empty || Convert.ToInt32(txtCodigoPrin.Text) == 0)
                {
                    txtCodigoPrin.Text = txtCodigoPrin.Text.PadLeft(6, '0');
                    MessageBox.Show("Campo (Cliente Principal) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescri.Select(); txtCodigoPrin.SelectAll();
                    return;
                }
                else
                {
                    TabClien_MET MET = new TabClien_MET();
                    MET.MET_SelecionaClientTAB(txtCodigo, txtCodigoPrin, txtDescri);
                }
            }
        }
        //SELECIONA O CLIENTE NO TAB
        private void txtReferencialCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtReferencialCod.Text != string.Empty)
            {
                if (Convert.ToInt32(txtReferencialCod.Text) == 0)
                {
                    txtReferencialCod.Text = string.Empty;
                }
                else
                {
                    TabClien_MET MET = new TabClien_MET();
                    MET.MET_SelecionaClientTAB(txtCodigo, txtReferencialCod, mtbVencMun);
                }
            }
        }
        //SELECIONA O VENDEDOR
        private void txtVendedorCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtVendedorCod.Text != string.Empty)
            {
                if (Convert.ToInt32(txtVendedorCod.Text) == 0)
                {
                    txtVendedorCod.Text = string.Empty;
                    txtVendedorDesc.Text = string.Empty;
                }
                else
                {
                    TabClien_MET MET = new TabClien_MET();
                    MET.MET_SelecionaVendedTAB(txtVendedorCod, txtVendedorDesc, txtRotaCod);
                }
            }
            if (e.KeyCode == Keys.Tab && txtVendedorCod.Text == string.Empty)
            {
                txtVendedorCod.Text = string.Empty;
                txtVendedorDesc.Text = string.Empty;
            }
        }
        //SELECIONA A TRANSPORTADORA
        private void txtTransportadoraCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtTransportadoraCod.Text != string.Empty)
            {
                if (Convert.ToInt32(txtTransportadoraCod.Text) == 0)
                {
                    txtTransportadoraCod.Text = string.Empty;
                    txtTransportadoraDesc.Text = string.Empty;
                }
                else
                {
                    TabClien_MET MET = new TabClien_MET();
                    MET.MET_SelecionaTranspTAB(txtTransportadoraCod, txtTransportadoraDesc, panTransport);
                }
            }
            if (e.KeyCode == Keys.Tab && txtTransportadoraCod.Text == string.Empty)
            {
                txtTransportadoraCod.Text = string.Empty;
                txtTransportadoraDesc.Text = string.Empty;
            }
        }
        //SELECIONA O CONVENIO
        private void txtConvenioCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtConvenioCod.Text != string.Empty)
            {
                if (Convert.ToInt32(txtConvenioCod.Text) == 0)
                {
                    txtConvenioCod.Text = string.Empty;
                    txtConvenioDesc.Text = string.Empty;
                    comCondicao.SelectedIndex = 0;
                    comRecebimento.SelectedIndex = 0;
                }
                else
                {
                    TabClien_MET MET = new TabClien_MET();
                    MET.MET_SelecionaConvenTAB(txtConvenioCod, txtConvenioDesc, txtTransportadoraCod, comRecebimento, comCondicao, txtMESTRE);
                }
            }
            if (e.KeyCode == Keys.Tab && txtConvenioCod.Text == string.Empty)
            {
                txtConvenioCod.Text = string.Empty;
                txtConvenioDesc.Text = string.Empty;
            }
        }
        //SELECIONA O CFOP
        private void txtCfop_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtCfop.Text != string.Empty)
            {
                if (Convert.ToInt32(txtCfop.Text) == 0)
                {
                    txtCfop.Text = string.Empty;
                    txtCfopDesc.Text = string.Empty;
                }
                else
                {
                    TabClien_MET MET = new TabClien_MET();
                    MET.MET_SelecionaCodFOPTAB(txtCfop, txtCfopDesc, comIcmsST);
                }
            }
            if (e.KeyCode == Keys.Tab && txtCfop.Text == string.Empty)
            {
                txtCfop.Text = string.Empty;
                txtCfopDesc.Text = string.Empty;
            }
        }
        //SELECIONA A MESAGEM DA NOTA
        private void txtMsg1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtMsg1.Text != string.Empty)
            {
                if (Convert.ToInt32(txtMsg1.Text) == 0)
                {
                    txtMsg1.Text = string.Empty;
                }
                else
                {
                    TabClien_MET MET = new TabClien_MET();
                    MET.MET_SelecionaMensagTAB(txtMsg1, txtCfop);
                }
            }
        }
        //SELECIONA A MESAGEM DA NOTA
        private void txtMsg2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtMsg2.Text != string.Empty)
            {
                if (Convert.ToInt32(txtMsg2.Text) == 0)
                {
                    txtMsg2.Text = string.Empty;
                }
                else
                {
                    TabClien_MET MET = new TabClien_MET();
                    MET.MET_SelecionaMensagTAB(txtMsg2, txtMsg1);
                }
            }
        }
        //SELECIONA A ROTA
        private void txtRotaCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtRotaCod.Text != string.Empty)
            {
                if (Convert.ToInt32(txtRotaCod.Text) == 0)
                {
                    txtRotaCod.Text = string.Empty;
                    txtRotaSequen.Text = string.Empty;
                }
                else
                {
                    TabClien_MET MET = new TabClien_MET();
                    MET.MET_SelecionaRotasTAB(txtRotaCod, mtbVencEst, txtMESTRE, txtRotaSequen, txtCodigo);
                }
            }
            if (e.KeyCode == Keys.Tab && txtRotaCod.Text == string.Empty)
            {
                txtRotaCod.Text = string.Empty;
                txtRotaSequen.Text = string.Empty;
            }
        }
        //SELECIONA O CLIENTE NO LEAVE
        private void txtCodigoPrin_Leave(object sender, EventArgs e)
        {
            TabClien_MET MET = new TabClien_MET();
            MET.MET_SelecionaClientLEAVE(txtCodigo, txtCodigoPrin, txtDescri);
        }
        #endregion

        




        

        //DEFINE QUAL CONTROLE PODE RECEBER APENAS NÚMEROS
        #region APENAS NÚMEROS
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtCodigoPrin_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtRotaCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtReferencialCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtVendedorCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtEmpresaCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtBanco_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtAgencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtEndCidadeFATU_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtEndCidadePERSO_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtTransportadoraCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtConvenioCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtPjDiaFatu_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtCfop_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtMsg1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtMsg2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //RECEBE NÚMEROS DECIMAL
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmerosDec(e, txtDesconto);
        }
        private void txtValorLimiteCre_KeyPress(object sender, KeyPressEventArgs e)
        {
            //RECEBE NÚMEROS DECIMAL
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmerosDec(e, txtValorLimiteCre);
        }
        private void txtInscricaoEstadual_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtInscricaoMunicipal_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }

        #endregion



        //FAZ O TRATAMENTO DE NÚMEROS DECIMAIS
        #region TRATAMENTO DECIMAL
        private void txtDesconto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oemcomma)
            {
                txtDesconto.MaxLength = 5;
            }
            else
            {
                if (!txtDesconto.Text.Contains(","))
                {
                    txtDesconto.MaxLength = 2;
                }
                else
                {
                    txtDesconto.MaxLength = 5;
                }
            }
        }
        private void txtDesconto_Leave(object sender, EventArgs e)
        {
            if (txtDesconto.Text != string.Empty)
            {
                if (txtDesconto.Text.Contains(","))
                {
                    txtDesconto.Text = Convert.ToDecimal(txtDesconto.Text).ToString("00.00");
                }
                else
                {
                    if (txtDesconto.Text.Length > 2)
                    {
                        decimal Dividido;
                        Dividido = Convert.ToDecimal(txtDesconto.Text);
                        Dividido = Decimal.Parse(txtDesconto.Text);
                        txtDesconto.Text = Convert.ToString(Dividido / 100);
                        txtDesconto.Text = Convert.ToDecimal(txtDesconto.Text).ToString("00.00");
                    }
                    else
                    {
                        txtDesconto.Text = Convert.ToDecimal(txtDesconto.Text).ToString("00.00");
                    }
                }
            }
        }
        private void txtValorLimiteCre_Leave(object sender, EventArgs e)
        {
            if (txtValorLimiteCre.Text != string.Empty)
            {
                if (txtValorLimiteCre.Text.Contains(","))
                {
                    txtValorLimiteCre.Text = Convert.ToDecimal(txtValorLimiteCre.Text).ToString("00.00");
                }
                else
                {
                    if (txtValorLimiteCre.Text.Length > 4)
                    {
                        decimal Dividido;
                        Dividido = Convert.ToDecimal(txtValorLimiteCre.Text);
                        Dividido = Decimal.Parse(txtValorLimiteCre.Text);
                        txtValorLimiteCre.Text = Convert.ToString(Dividido / 100);
                        txtValorLimiteCre.Text = Convert.ToDecimal(txtValorLimiteCre.Text).ToString("00.00");
                    }
                    else
                    {
                        txtValorLimiteCre.Text = Convert.ToDecimal(txtValorLimiteCre.Text).ToString("00.00");
                    }
                }
            }
            else
            {
                txtValorLimiteCre.Text = string.Empty;
                mtbDataVenciLimite.Text = string.Empty;
                comSituacaoCredito.Select();
            }
        }
        private void txtValorLimiteCre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oemcomma)
            {
                txtValorLimiteCre.MaxLength = 7;
            }
            else
            {
                if (!txtValorLimiteCre.Text.Contains(","))
                {
                    txtValorLimiteCre.MaxLength = 4;
                }
                else
                {
                    txtValorLimiteCre.MaxLength = 7;
                }
            }
        }
        #endregion



        #region SELECT ALL NO ENTER DOS MASKED TEXT BOX
        private void mtbDataVenciLimite_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbTelPabx_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbTelCel1_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbTelFax_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbTelCel2_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbVencEst_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbVencMun_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbEndCepFATU_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbEndCepPERSO_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbContraInicio_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbContraFim_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbPfAdmissao_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbPfAniversario_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbPfCpfConjuge_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        #endregion

        #region SELECT ALL NO MOUSE DOWN
        private void mtbTelPabx_MouseDown(object sender, MouseEventArgs e)
        {
            mtbTelPabx.SelectAll();
        }
        private void mtbTelCel1_MouseDown(object sender, MouseEventArgs e)
        {
            mtbTelCel1.SelectAll();
        }
        private void mtbTelFax_MouseDown(object sender, MouseEventArgs e)
        {
            mtbTelFax.SelectAll();
        }
        private void mtbTelCel2_MouseDown(object sender, MouseEventArgs e)
        {
            mtbTelCel2.SelectAll();
        }
        private void mtbVencEst_MouseDown(object sender, MouseEventArgs e)
        {
            mtbVencEst.SelectAll();
        }
        private void mtbVencMun_MouseDown(object sender, MouseEventArgs e)
        {
            mtbVencMun.SelectAll();
        }
        private void mtbCpfCnpj_MouseDown(object sender, MouseEventArgs e)
        {
            mtbCpfCnpj.SelectAll();
        }
        private void mtbEndCepFATU_MouseDown(object sender, MouseEventArgs e)
        {
            mtbEndCepFATU.SelectAll();
        }
        private void mtbEndCepPERSO_MouseDown(object sender, MouseEventArgs e)
        {
            mtbEndCepPERSO.SelectAll();
        }
        private void mtbDataVenciLimite_MouseDown(object sender, MouseEventArgs e)
        {
            mtbDataVenciLimite.SelectAll();
        }
        private void mtbContraInicio_MouseDown(object sender, MouseEventArgs e)
        {
            mtbContraInicio.SelectAll();
        }
        private void mtbContraFim_MouseDown(object sender, MouseEventArgs e)
        {
            mtbContraFim.SelectAll();
        }
        private void mtbPfAdmissao_MouseDown(object sender, MouseEventArgs e)
        {
            mtbPfAdmissao.SelectAll();
        }
        private void mtbPfAniversario_MouseDown(object sender, MouseEventArgs e)
        {
            mtbPfAniversario.SelectAll();
        }
        private void mtbPfCpfConjuge_MouseDown(object sender, MouseEventArgs e)
        {
            mtbPfCpfConjuge.SelectAll();
        }
        private void txtCodigoPrin_MouseDown(object sender, MouseEventArgs e)
        {
            txtCodigoPrin.SelectAll();
        }
        private void txtRotaCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtRotaSequen.Text = string.Empty;
            txtRotaCod.SelectAll();
        }
        private void txtReferencialCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtReferencialCod.SelectAll();
        }
        private void txtVendedorCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtVendedorDesc.Text = string.Empty;
            txtVendedorCod.SelectAll();
        }
        private void txtEmpresaCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtEmpresaDesc.Text = string.Empty;
            txtEmpresaCod.SelectAll();
        }
        private void txtBanco_MouseDown(object sender, MouseEventArgs e)
        {
            txtBanco.SelectAll();
        }
        private void txtAgencia_MouseDown(object sender, MouseEventArgs e)
        {
            txtAgencia.SelectAll();
        }
        private void txtConceito_MouseDown(object sender, MouseEventArgs e)
        {
            txtConceito.SelectAll();
        }
        private void txtEndCidadeFATU_MouseDown(object sender, MouseEventArgs e)
        {
            txtEndCidDescriFATU.Text = string.Empty;
            txtEndCidUFFATU.Text = string.Empty;
            txtEndCidadeFATU.SelectAll();
        }
        private void txtEndCidadePERSO_MouseDown(object sender, MouseEventArgs e)
        {
            txtEndCidDescriPERSO.Text = string.Empty;
            txtEndCidUFPERSO.Text = string.Empty;
            txtEndCidadePERSO.SelectAll();
        }
        private void txtTransportadoraCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtTransportadoraDesc.Text = string.Empty;
            txtTransportadoraCod.SelectAll();
        }
        private void txtConvenioCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtConvenioDesc.Text = string.Empty;
            txtConvenioCod.SelectAll();
        }
        private void txtPjDiaFatu_MouseDown(object sender, MouseEventArgs e)
        {
            txtPjDiaFatu.SelectAll();
        }
        private void txtCfop_MouseDown(object sender, MouseEventArgs e)
        {
            txtCfopDesc.Text = string.Empty;
            txtCfop.SelectAll();
        }
        private void txtMsg1_MouseDown(object sender, MouseEventArgs e)
        {
            txtMsg1.SelectAll();
        }
        private void txtMsg2_MouseDown(object sender, MouseEventArgs e)
        {
            txtMsg2.SelectAll();
        }
        private void txtValorLimiteCre_MouseDown(object sender, MouseEventArgs e)
        {
            txtValorLimiteCre.SelectAll();
        }
        private void txtDesconto_MouseDown(object sender, MouseEventArgs e)
        {
            txtDesconto.SelectAll();
        }
        private void txtCodigo_MouseDown(object sender, MouseEventArgs e)
        {
            ZerarCampos();
            CamposDisable();


            btnInfFinaShow.Enabled = false;
            btnInfComerShow.Enabled = false;
            btnAvancar.Enabled = false;
            btnGravar.Enabled = false;
            btnGravarAb2.Enabled = false;
            btnVoltar.Enabled = false;

            txtCodigo.SelectAll();
        }
        private void txtCodigo_Click(object sender, EventArgs e)
        {
            txtCodigo.SelectAll();
        }
        #endregion

        #region TEXT CHANGE APAGAR OS CAMPOS
        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            btnInfFinaShow.Enabled = false;
            btnInfComerShow.Enabled = false;
            btnAvancar.Enabled = false;
            btnGravar.Enabled = false;
            btnGravarAb2.Enabled = false;
            btnVoltar.Enabled = false;

        }
        private void txtVendedorCod_TextChanged(object sender, EventArgs e)
        {
            txtVendedorDesc.Text = string.Empty;
        }
        private void txtEmpresaCod_TextChanged(object sender, EventArgs e)
        {
            txtEmpresaDesc.Text = string.Empty;
        }
        private void txtEndCidadeFATU_TextChanged(object sender, EventArgs e)
        {
            txtEndCidDescriFATU.Text = string.Empty;
            txtEndCidUFFATU.Text = string.Empty;
        }
        private void txtEndCidadePERSO_TextChanged(object sender, EventArgs e)
        {
            txtEndCidDescriPERSO.Text = string.Empty;
            txtEndCidUFPERSO.Text = string.Empty;
        }
        private void txtTransportadoraCod_TextChanged(object sender, EventArgs e)
        {
            txtTransportadoraDesc.Text = string.Empty;
        }
        private void txtConvenioCod_TextChanged(object sender, EventArgs e)
        {
            txtConvenioDesc.Text = string.Empty;
        }
        private void txtCfop_TextChanged(object sender, EventArgs e)
        {
            txtCfopDesc.Text = string.Empty;
        }
        private void txtRotaCod_TextChanged(object sender, EventArgs e)
        {
            txtRotaSequen.Text = string.Empty;
        }
        #endregion

        #region OBRIGAÇÃO DOS CAMPOS NO TAB E METODOS LEAVE

        //OBRIGA O CAMPO NO TAB
        private void txtDescri_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtDescri.Text == string.Empty)
                {
                    MessageBox.Show("Campo (Descrição) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigo.Focus(); txtDescri.SelectAll();
                    return;
                }
            }
        }
        //SELECIONA STATUS = 1 SE SELECTED INDEX <= 0
        private void comStatus_Leave(object sender, EventArgs e)
        {
            if (comStatus.SelectedIndex <= 0)
            {
                comStatus.SelectedIndex = 1;
            }
        }      
        //SE O CONTRATO FOR "NÃO" APAGA AS DATAS E SELECIONA O BOTÃO AVANÇAR
        private void comContratoEmpresa_Leave(object sender, EventArgs e)
        {
            if (comContratoEmpresa.SelectedIndex == 2)
            {
                mtbContraInicio.Text = string.Empty;
                mtbContraFim.Text = string.Empty;
                btnAvancar.Select();
            }
        }
        //SE O CONTRATO FOR "SIM - SEM VENCI." APAGA A DATA FINAL E SELECIONA O BOTÃO AVANÇAR
        private void mtbContraInicio_Leave(object sender, EventArgs e)
        {
            if (comContratoEmpresa.SelectedIndex == 1)
            {
                mtbContraFim.Text = string.Empty;
                btnAvancar.Select();
            }
        }
        //SE ESTIVER EM BRANCO, ESCREVE "ISENTO" E APAGA A DATA DE VENCIMENTO
        private void txtInscricaoMunicipal_Leave(object sender, EventArgs e)
        {
            if (txtInscricaoMunicipal.Text == string.Empty)
            {
                txtInscricaoMunicipal.Text = "ISENTO";
                mtbVencMun.Text = string.Empty;
                txtReferencialCod.Select();
            }
            else if (txtInscricaoMunicipal.Text == "ISENTO")
            {
                mtbVencMun.Text = string.Empty;
                txtReferencialCod.Select();
            }
        }       
        //SE ESTIVER EM BRANCO, ESCREVE "ISENTO" E APAGA A DATA DE VENCIMENTO
        private void txtInscricaoEstadual_Leave(object sender, EventArgs e)
        {
            if (txtInscricaoEstadual.Text == string.Empty)
            {
                txtInscricaoEstadual.Text = "ISENTO";
                mtbVencEst.Text = string.Empty;
                txtRotaCod.Select();
            }
            else if (txtInscricaoEstadual.Text == "ISENTO")
            {
                mtbVencEst.Text = string.Empty;
                txtRotaCod.Select();
            }
           
        }
        //OBRIGA NO TAB
        private void txtFantasia_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtFantasia.Text == string.Empty)
                {
                    MessageBox.Show("Campo (" + lblFantasia.AccessibleName + ") deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comStatus.Select(); txtFantasia.SelectAll();
                    return;
                }
            }
        }
        //OBRIGA NO TAB E VALIDA O CPF\CNPJ
        private void mtbCpfCnpj_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (mtbCpfCnpj.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {
                    MessageBox.Show("Campo (CPF.CNPJ) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comTipoPFPJ.Focus(); mtbCpfCnpj.SelectAll();
                    return;
                }
                else
                {
                    TabClien_MET MET = new TabClien_MET();
                    //VALIDA O CPF CNPJ
                    #region VALIDA CPF CNPJ
                    if (comTipoPFPJ.SelectedIndex == 0)
                    {
                        bool VALIDAR = MET.MET_ValidaCPF(mtbCpfCnpj.Text);
                        if (!VALIDAR)
                        {
                            MessageBox.Show("CPF informado é inválido. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            comTipoPFPJ.Select();
                            SendKeys.Send("{HOME}");
                            SendKeys.Send("+{END}");
                            return;
                        }
                    }
                    if (comTipoPFPJ.SelectedIndex == 1)
                    {
                        bool VALIDAR = MET.MET_ValidaCNPJ(mtbCpfCnpj.Text);
                        if (!VALIDAR)
                        {
                            MessageBox.Show("CNPJ informado é inválido. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            comTipoPFPJ.Select();
                            SendKeys.Send("{HOME}");
                            SendKeys.Send("+{END}");
                            return;
                        }
                    }
                    #endregion

                    //VERIFICA SE JÁ EXISTE
                    MET.MET_VerificaClientExistente(mtbCpfCnpj, comTipoPFPJ, txtMESTRE);
                }
            }
        }
        //VALIDA O CFP DO CONJUGE
        private void mtbPfCpfConjuge_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (mtbPfCpfConjuge.Text.Count(c => char.IsLetterOrDigit(c)) != 0)
                {
                    TabClien_MET MET = new TabClien_MET();
                    //VALIDA O CPF
                    bool VALIDAR = MET.MET_ValidaCPF(mtbPfCpfConjuge.Text);
                    if (!VALIDAR)
                    {
                        MessageBox.Show("CPF informado é inválido. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPfConjuge.Select();
                        SendKeys.Send("{HOME}");
                        SendKeys.Send("+{END}");
                        return;
                    }
                }
            }
        }
        //MENSAGEM SE INFORMOU PRODUTOR RURAL E NAO INFORMOU INSCRIÇÃO
        private void txtInscricaoEstadual_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (comCategoria.SelectedIndex == 3 && (txtInscricaoEstadual.Text == "ISENTO" || txtInscricaoEstadual.Text == string.Empty))
                {
                    MessageBox.Show("VOCÊ INFORMOU (PRODUTOR RURAL) E NÃO INFORMOU INSCRIÇÃO", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        //NÃO PODE SER MAIOR QUE 31
        private void txtPjDiaFatu_TextChanged(object sender, EventArgs e)
        {
            if (txtPjDiaFatu.Text != string.Empty)
            {
                if (Convert.ToInt32(txtPjDiaFatu.Text) > 31)
                {
                    txtPjDiaFatu.Text = "31";
                    txtPjDiaFatu.SelectAll();
                }
            }
        }



        #region VALIDAÇÃO DAS DATAS NO TAB

        private void mtbVencEst_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //VALIDA A DATA
                TabClien_MET MET = new TabClien_MET();
                MET.MET_VerificaDATA(mtbVencEst, txtInscricaoEstadual);
            }
        }

        private void mtbVencMun_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //VALIDA A DATA
                TabClien_MET MET = new TabClien_MET();
                MET.MET_VerificaDATA(mtbVencMun, txtInscricaoMunicipal);
            }
        }

        private void mtbDataVenciLimite_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //VALIDA A DATA
                TabClien_MET MET = new TabClien_MET();
                MET.MET_VerificaDATA(mtbDataVenciLimite, txtValorLimiteCre);
            }
        }

        private void mtbContraInicio_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //VALIDA A DATA
                TabClien_MET MET = new TabClien_MET();
                MET.MET_VerificaDATA(mtbContraInicio, comContratoEmpresa);
            }
        }

        private void mtbContraFim_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //VALIDA A DATA
                TabClien_MET MET = new TabClien_MET();
                MET.MET_VerificaDATA(mtbContraFim, mtbContraInicio);
            }

        }

        private void mtbPfAdmissao_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //VALIDA A DATA
                TabClien_MET MET = new TabClien_MET();
                MET.MET_VerificaDATA(mtbPfAdmissao, txtPfCargo);
            }
        }

        private void mtbPfAniversario_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //VALIDA A DATA
                TabClien_MET MET = new TabClien_MET();
                MET.MET_VerificaDATA(mtbPfAniversario, mtbPfAdmissao);
            }
        }
        #endregion    
   
        #endregion


        //ABRE AS TELAS DE PESQUISA
        #region CHAMA AS PESQUISAS NO FORM PRINCIPAL
        private void txtCodigoPrin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                PesClie1.cs.PesClie1_CALL Call = new PesClie1.cs.PesClie1_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesClie13Wenemy3156!.350?°";
                Call.PesClie1_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtCodigoPrin.Text = Call._ResultPesquisaCALL;
                }

                txtCodigoPrin.SelectAll();
            }
        }
        private void txtTransportadoraCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtTransportadoraCod.Text = txtTransportadoraCod.Text.PadLeft(6, '0');
                txtTransportadoraDesc.Text = string.Empty;

                PesTrans.cs.PesTrans_CALL Call = new PesTrans.cs.PesTrans_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesTrans20Wenemy3156!.350?°";
                Call.PesTrans_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtTransportadoraCod.Text = Call._ResultPesquisaCALL;
                }

                txtTransportadoraCod.SelectAll();
            }
        }
        private void txtVendedorCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtVendedorCod.Text = txtVendedorCod.Text.PadLeft(6, '0');
                txtVendedorDesc.Text = string.Empty;

                PesVende.cs.PesVende_CALL Call = new PesVende.cs.PesVende_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesVende21Wenemy3156!.350?°";
                Call.PesVende_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtVendedorCod.Text = Call._ResultPesquisaCALL;
                }

                txtVendedorCod.SelectAll();
            }
        }
        private void txtConvenioCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtConvenioCod.Text = txtConvenioCod.Text.PadLeft(6, '0');
                txtConvenioDesc.Text = string.Empty;


                PesConve.cs.PesConve_CALL Call = new PesConve.cs.PesConve_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesConve3Wenemy3156!.350?°";
                Call.PesConve_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtConvenioCod.Text = Call._ResultPesquisaCALL;
                }

                txtConvenioCod.SelectAll();
            }
        }
        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                ZerarCampos();
                CamposDisable();

                txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');

                PesClie1.cs.PesClie1_CALL Call = new PesClie1.cs.PesClie1_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesClie13Wenemy3156!.350?°";
                Call.PesClie1_AUTORIZADO();


                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtCodigo.Text = Call._ResultPesquisaCALL;
                }

                txtCodigo.SelectAll();
            }
        }
        private void txtRotaCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtRotaCod.Text = txtRotaCod.Text.PadLeft(6, '0');

                PesRotas.cs.PesRotas_CALL Call = new PesRotas.cs.PesRotas_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesRotas18Wenemy3156!.350?°";
                Call.PesRotas_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtRotaCod.Text = Call._ResultPesquisaCALL;
                }

                txtRotaCod.SelectAll();
            }
        }
        private void txtReferencialCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtReferencialCod.Text = txtReferencialCod.Text.PadLeft(6, '0');

                PesClie1.cs.PesClie1_CALL Call = new PesClie1.cs.PesClie1_CALL();

                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesClie13Wenemy3156!.350?°";
                Call.PesClie1_AUTORIZADO();


                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtReferencialCod.Text = Call._ResultPesquisaCALL;
                }

                txtReferencialCod.SelectAll();
            }
        }
        private void txtEmpresaCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtEmpresaCod.Text = txtEmpresaCod.Text.PadLeft(6, '0');
                txtEmpresaDesc.Text = string.Empty;

                PesEmpre.cs.PesEmpre_CALL Call = new PesEmpre.cs.PesEmpre_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesEmpre5Wenemy3156!.350?°";
                Call.PesEmpre_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtEmpresaCod.Text = Call._ResultPesquisaCALL;
                }

                txtEmpresaCod.SelectAll();
            }
        }
        private void txtEndCidadeFATU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtEndCidadeFATU.Text = txtEndCidadeFATU.Text.PadLeft(6, '0');
                txtEndCidDescriFATU.Text = string.Empty;
                txtEndCidUFFATU.Text = string.Empty;

                PesCidad.cs.PesCidad_CALL Call = new PesCidad.cs.PesCidad_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesCidad3Wenemy3156!.350?°";
                Call.PesCidad_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtEndCidadeFATU.Text = Call._ResultPesquisaCALL;
                }

                txtEndCidadeFATU.SelectAll();
            }
        }
        private void txtEndCidadePERSO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtEndCidadePERSO.Text = txtEndCidadePERSO.Text.PadLeft(6, '0');
                txtEndCidDescriPERSO.Text = string.Empty;
                txtEndCidUFPERSO.Text = string.Empty;

                PesCidad.cs.PesCidad_CALL Call = new PesCidad.cs.PesCidad_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesCidad3Wenemy3156!.350?°";
                Call.PesCidad_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtEndCidadePERSO.Text = Call._ResultPesquisaCALL;
                }

                txtEndCidadePERSO.SelectAll();
            }
        }
        private void txtCfop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(4, '0');

                PesCfope.cs.PesCfope_CALL Call = new PesCfope.cs.PesCfope_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesCfope3Wenemy3156!.350?°";
                Call.PesCfope_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtCfop.Text = Call._ResultPesquisaCALL;
                }

                txtCfop.SelectAll();
            }
        }
        private void txtMsg1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtMsg1.Text = txtMsg1.Text.PadLeft(3, '0');

                PesMsgNt.cs.PesMsgNt_CALL Call = new PesMsgNt.cs.PesMsgNt_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesMsgNt13Wenemy3156!.350?°";
                Call.PesMsgNt_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtMsg1.Text = Call._ResultPesquisaCALL;
                }

                txtMsg1.SelectAll();
            }
        }
        private void txtMsg2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtMsg2.Text = txtMsg2.Text.PadLeft(3, '0');

                PesMsgNt.cs.PesMsgNt_CALL Call = new PesMsgNt.cs.PesMsgNt_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesMsgNt13Wenemy3156!.350?°";
                Call.PesMsgNt_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtMsg2.Text = Call._ResultPesquisaCALL;
                }

                txtMsg2.SelectAll();
            }
        }
        #endregion


        #endregion


        #region PESQUISA


        #region Buttons Pesquisar, Voltar e Fechar
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabClien_Pesquisa Pesq = new TabClien_Pesquisa();
            Pesq.Pesc_EXECUTAR(_Login_LojaID_TabClien, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, txtPesDescri, txtPesApelido, txtPesEmpCod, txtPesEmpDesc, txtPesVendCod, txtPesVendDesc, mtbPesCpfCnpj, txtPesPrincipal, txtPesRota, txtPesConvenio, txtPesCidadeCod, txtPesCidadeDesc, comPesSituacao, comPesCategoria, comPesQtAoCredito, cheOrdemAlfDown, Dgv_Pesquisa);
            if (txtQtSelectPES.Text == string.Empty) { txtQtSelectPES.Text = "000000"; }
            if (Convert.ToInt32(txtQtSelectPES.Text) == 0)
            {
                MessageBox.Show("Nenhuma informação encontrada. Verifique os filtros!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            txtPesDescri.Select(); txtPesDescri.SelectAll();
        }
        private void btnVoltarPesq_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = Tp1;
        }
        private void btnFechar2_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion


        public void ZerarCamposPesquisa()
        {
            foreach (Control panPrin in panPrinAb3.Controls)
            {
                foreach (Control CONTROLs in panPrin.Controls)
                {
                    foreach (Control CONTROL in CONTROLs.Controls)
                    {
                        if (CONTROL.GetType() == typeof(TextBox))
                        {
                            (CONTROL as TextBox).Text = string.Empty;
                        }
                        if (CONTROL.GetType() == typeof(MaskedTextBox))
                        {
                            (CONTROL as MaskedTextBox).Text = string.Empty;
                        }
                    }
                }
            }
        }

        //PREENCHE A QUANTIDADE DE RESULTADOS ENCONTRADOS
        private void Dgv_Pesquisa_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }
        private void Dgv_Pesquisa_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }



        //FAZ O SELECT DO ITEM CLICANO NO GRID
        private void Dgv_Pesquisa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string VariavelPesquisa = Dgv_Pesquisa.CurrentRow.Cells[0].Value.ToString();
                TabControl.SelectedTab = Tp1;
                TabClien_AppaButtons Apa = new TabClien_AppaButtons();
                Apa._ButtonZER(txtMESTRE, txtCodigo, btnIncluir, btnGravar, CamposDisable, TabControl, Tp1, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar);
                txtCodigo.Text = VariavelPesquisa;
                txtCodigo.Select(); txtCodigo.SelectAll();
                SendKeys.Send("{TAB}");
            }
        }


        //SELECIONA AS FKs SEM TEXTBOX DE DESCRIÇÃO
        private void txtPesPrincipal_Leave(object sender, EventArgs e)
        {
            //SELECIONA A FK NO LEAVE
            TabClien_Pesquisa PESQ = new TabClien_Pesquisa();
            PESQ.PESQ_SelecionaClientLEAVE(txtPesPrincipal);
        }
        private void txtPesRota_Leave(object sender, EventArgs e)
        {
            //SELECIONA A FK NO LEAVE
            TabClien_Pesquisa PESQ = new TabClien_Pesquisa();
            PESQ.PESQ_SelecionaRotasLEAVE(txtPesRota);
        }
        private void txtPesConvenio_Leave(object sender, EventArgs e)
        {
            //SELECIONA A FK NO LEAVE
            TabClien_Pesquisa PESQ = new TabClien_Pesquisa();
            PESQ.PESQ_SelecionaConvenLEAVE(txtPesConvenio);
        }

        //SELECIONA AS FKs NO TAB
        private void txtPesEmpCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtPesEmpCod.Text != string.Empty)
            {
                if (Convert.ToInt32(txtPesEmpCod.Text) == 0)
                {
                    txtPesEmpCod.Text = string.Empty;
                    txtPesEmpDesc.Text = string.Empty;
                }
                else
                {
                    //SELECIONA A FK NO TAB
                    TabClien_MET MET = new TabClien_MET();
                    MET.MET_SelecionaEmpresTAB(txtPesEmpCod, txtPesEmpDesc, txtPesApelido);
                }
            }
            if (e.KeyCode == Keys.Tab && txtPesEmpCod.Text == string.Empty)
            {
                txtPesEmpCod.Text = string.Empty;
                txtPesEmpDesc.Text = string.Empty;
            }
            if (e.KeyCode == Keys.Tab && txtPesEmpDesc.Text != string.Empty)
            {
                //EXECUTA A PESQUISA
                TabClien_Pesquisa Pesq = new TabClien_Pesquisa();
                Pesq.Pesc_EXECUTAR(_Login_LojaID_TabClien, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, txtPesDescri, txtPesApelido, txtPesEmpCod, txtPesEmpDesc, txtPesVendCod, txtPesVendDesc, mtbPesCpfCnpj, txtPesPrincipal, txtPesRota, txtPesConvenio, txtPesCidadeCod, txtPesCidadeDesc, comPesSituacao, comPesCategoria, comPesQtAoCredito, cheOrdemAlfDown, Dgv_Pesquisa);
            }
        }
        private void txtPesVendCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtPesVendCod.Text != string.Empty)
            {
                if (Convert.ToInt32(txtPesVendCod.Text) == 0)
                {
                    txtPesVendCod.Text = string.Empty;
                    txtPesVendDesc.Text = string.Empty;
                }
                else
                {
                    //SELECIONA A FK NO TAB
                    TabClien_MET MET = new TabClien_MET();
                    MET.MET_SelecionaVendedTAB(txtPesVendCod, txtPesVendDesc, txtPesEmpCod);
                }
            }
            if (e.KeyCode == Keys.Tab && txtPesVendCod.Text == string.Empty)
            {
                txtPesVendCod.Text = string.Empty;
                txtPesVendDesc.Text = string.Empty;
            }
            if (e.KeyCode == Keys.Tab && txtPesVendDesc.Text != string.Empty)
            {
                //EXECUTA A PESQUISA
                TabClien_Pesquisa Pesq = new TabClien_Pesquisa();
                Pesq.Pesc_EXECUTAR(_Login_LojaID_TabClien, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, txtPesDescri, txtPesApelido, txtPesEmpCod, txtPesEmpDesc, txtPesVendCod, txtPesVendDesc, mtbPesCpfCnpj, txtPesPrincipal, txtPesRota, txtPesConvenio, txtPesCidadeCod, txtPesCidadeDesc, comPesSituacao, comPesCategoria, comPesQtAoCredito, cheOrdemAlfDown, Dgv_Pesquisa);
            }
        }
        private void txtPesCidadeCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtPesCidadeCod.Text != string.Empty)
            {
                if (Convert.ToInt32(txtPesCidadeCod.Text) == 0)
                {
                    txtPesCidadeCod.Text = string.Empty;
                    txtPesCidadeDesc.Text = string.Empty;
                    txtPesCidadeUF.Text = string.Empty;
                }
                else
                {
                    //SELECIONA A FK NO TAB
                    TabClien_MET MET = new TabClien_MET();
                    MET.MET_SelecionaCidadeTAB(txtPesCidadeCod, txtPesCidadeDesc, txtPesCidadeUF, txtPesConvenio);
                }
            }
            if (e.KeyCode == Keys.Tab && txtPesCidadeCod.Text == string.Empty)
            {
                txtPesCidadeCod.Text = string.Empty;
                txtPesCidadeDesc.Text = string.Empty;
                txtPesCidadeUF.Text = string.Empty;
            }
            if (e.KeyCode == Keys.Tab && txtPesCidadeDesc.Text != string.Empty)
            {
                //EXECUTA A PESQUISA
                TabClien_Pesquisa Pesq = new TabClien_Pesquisa();
                Pesq.Pesc_EXECUTAR(_Login_LojaID_TabClien, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, txtPesDescri, txtPesApelido, txtPesEmpCod, txtPesEmpDesc, txtPesVendCod, txtPesVendDesc, mtbPesCpfCnpj, txtPesPrincipal, txtPesRota, txtPesConvenio, txtPesCidadeCod, txtPesCidadeDesc, comPesSituacao, comPesCategoria, comPesQtAoCredito, cheOrdemAlfDown, Dgv_Pesquisa);
            }
        }


        //APAGA OS CAMPOS NO TEXTCHANGE
        private void txtPesEmpCod_TextChanged(object sender, EventArgs e)
        {
            txtPesEmpDesc.Text = string.Empty;
        }
        private void txtPesVendCod_TextChanged(object sender, EventArgs e)
        {
            txtPesVendDesc.Text = string.Empty;
        }
        private void txtPesCidadeCod_TextChanged(object sender, EventArgs e)
        {
            txtPesCidadeDesc.Text = string.Empty;
            txtPesCidadeUF.Text = string.Empty;
        }


        //SELECT ALL NO CLICK
        private void txtPesEmpCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtPesEmpDesc.Text = string.Empty;
            txtPesEmpCod.SelectAll();
        }
        private void txtPesPrincipal_MouseDown(object sender, MouseEventArgs e)
        {
            txtPesPrincipal.SelectAll();
        }
        private void txtPesRota_MouseDown(object sender, MouseEventArgs e)
        {
            txtPesRota.SelectAll();
        }
        private void txtPesConvenio_MouseDown(object sender, MouseEventArgs e)
        {
            txtPesConvenio.SelectAll();
        }
        private void txtPesVendCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtPesVendDesc.Text = string.Empty;
            txtPesVendCod.SelectAll();
        }
        private void txtPesCidadeCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtPesCidadeDesc.Text = string.Empty;
            txtPesCidadeUF.Text = string.Empty;
            txtPesCidadeCod.SelectAll();
        }
        private void mtbPesCpfCnpj_MouseDown(object sender, MouseEventArgs e)
        {
            mtbPesCpfCnpj.SelectAll();
        }
        private void txtPesDescri_MouseDown(object sender, MouseEventArgs e)
        {
            txtPesDescri.SelectAll();
        }
        private void txtPesApelido_MouseDown(object sender, MouseEventArgs e)
        {
            txtPesApelido.SelectAll();
        }



        //RECEBE APENAS NÚMERO
        #region APENAS NÚMEROS
        private void txtPesEmpCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            //RECEBE APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtPesPrincipal_KeyPress(object sender, KeyPressEventArgs e)
        {
            //RECEBE APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtPesRota_KeyPress(object sender, KeyPressEventArgs e)
        {
            //RECEBE APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtPesConvenio_KeyPress(object sender, KeyPressEventArgs e)
        {
            //RECEBE APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtPesVendCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            //RECEBE APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtPesCidadeCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            //RECEBE APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void mtbPesCpfCnpj_KeyPress(object sender, KeyPressEventArgs e)
        {
            //RECEBE APENAS NÚMEROS
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        #endregion

        //FAZ A PESQUISA PELAS DIVERSAS PROPRIEDADES DOS CONTROLES
        #region FAZ A PESQUISA
        private void rabNumerico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabNumerico.Checked == true)
            {
                //EXECUTA A PESQUISA
                TabClien_Pesquisa Pesq = new TabClien_Pesquisa();
                Pesq.Pesc_EXECUTAR(_Login_LojaID_TabClien, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, txtPesDescri, txtPesApelido, txtPesEmpCod, txtPesEmpDesc, txtPesVendCod, txtPesVendDesc, mtbPesCpfCnpj, txtPesPrincipal, txtPesRota, txtPesConvenio, txtPesCidadeCod, txtPesCidadeDesc, comPesSituacao, comPesCategoria, comPesQtAoCredito, cheOrdemAlfDown, Dgv_Pesquisa);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void rabAlfabetico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabAlfabetico.Checked == true)
            {
                //EXECUTA A PESQUISA
                TabClien_Pesquisa Pesq = new TabClien_Pesquisa();
                Pesq.Pesc_EXECUTAR(_Login_LojaID_TabClien, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, txtPesDescri, txtPesApelido, txtPesEmpCod, txtPesEmpDesc, txtPesVendCod, txtPesVendDesc, mtbPesCpfCnpj, txtPesPrincipal, txtPesRota, txtPesConvenio, txtPesCidadeCod, txtPesCidadeDesc, comPesSituacao, comPesCategoria, comPesQtAoCredito, cheOrdemAlfDown, Dgv_Pesquisa);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void rabTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTodos.Checked == true)
            {
                //EXECUTA A PESQUISA
                TabClien_Pesquisa Pesq = new TabClien_Pesquisa();
                Pesq.Pesc_EXECUTAR(_Login_LojaID_TabClien, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, txtPesDescri, txtPesApelido, txtPesEmpCod, txtPesEmpDesc, txtPesVendCod, txtPesVendDesc, mtbPesCpfCnpj, txtPesPrincipal, txtPesRota, txtPesConvenio, txtPesCidadeCod, txtPesCidadeDesc, comPesSituacao, comPesCategoria, comPesQtAoCredito, cheOrdemAlfDown, Dgv_Pesquisa);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void rabTOP_CheckedChanged(object sender, EventArgs e)
        {
            //Habilita o numeric up down
            if (rabTOP.Checked == true)
            {
                nupQtResultados.Enabled = true;
                //EXECUTA A PESQUISA
                TabClien_Pesquisa Pesq = new TabClien_Pesquisa();
                Pesq.Pesc_EXECUTAR(_Login_LojaID_TabClien, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, txtPesDescri, txtPesApelido, txtPesEmpCod, txtPesEmpDesc, txtPesVendCod, txtPesVendDesc, mtbPesCpfCnpj, txtPesPrincipal, txtPesRota, txtPesConvenio, txtPesCidadeCod, txtPesCidadeDesc, comPesSituacao, comPesCategoria, comPesQtAoCredito, cheOrdemAlfDown, Dgv_Pesquisa);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
            else
            {
                nupQtResultados.Enabled = false;
                nupQtResultados.Value = 0;
            }
        }
        private void nupQtResultados_ValueChanged(object sender, EventArgs e)
        {
            if (rabTOP.Checked == true)
            {
                //EXECUTA A PESQUISA
                TabClien_Pesquisa Pesq = new TabClien_Pesquisa();
                Pesq.Pesc_EXECUTAR(_Login_LojaID_TabClien, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, txtPesDescri, txtPesApelido, txtPesEmpCod, txtPesEmpDesc, txtPesVendCod, txtPesVendDesc, mtbPesCpfCnpj, txtPesPrincipal, txtPesRota, txtPesConvenio, txtPesCidadeCod, txtPesCidadeDesc, comPesSituacao, comPesCategoria, comPesQtAoCredito, cheOrdemAlfDown, Dgv_Pesquisa);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void comPesSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabClien_Pesquisa Pesq = new TabClien_Pesquisa();
            Pesq.Pesc_EXECUTAR(_Login_LojaID_TabClien, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, txtPesDescri, txtPesApelido, txtPesEmpCod, txtPesEmpDesc, txtPesVendCod, txtPesVendDesc, mtbPesCpfCnpj, txtPesPrincipal, txtPesRota, txtPesConvenio, txtPesCidadeCod, txtPesCidadeDesc, comPesSituacao, comPesCategoria, comPesQtAoCredito, cheOrdemAlfDown, Dgv_Pesquisa);
            txtPesDescri.Select(); txtPesDescri.SelectAll();
        }
        private void comPesCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabClien_Pesquisa Pesq = new TabClien_Pesquisa();
            Pesq.Pesc_EXECUTAR(_Login_LojaID_TabClien, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, txtPesDescri, txtPesApelido, txtPesEmpCod, txtPesEmpDesc, txtPesVendCod, txtPesVendDesc, mtbPesCpfCnpj, txtPesPrincipal, txtPesRota, txtPesConvenio, txtPesCidadeCod, txtPesCidadeDesc, comPesSituacao, comPesCategoria, comPesQtAoCredito, cheOrdemAlfDown, Dgv_Pesquisa);
            txtPesDescri.Select(); txtPesDescri.SelectAll();
        }
        private void comPesQtAoCredito_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabClien_Pesquisa Pesq = new TabClien_Pesquisa();
            Pesq.Pesc_EXECUTAR(_Login_LojaID_TabClien, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, txtPesDescri, txtPesApelido, txtPesEmpCod, txtPesEmpDesc, txtPesVendCod, txtPesVendDesc, mtbPesCpfCnpj, txtPesPrincipal, txtPesRota, txtPesConvenio, txtPesCidadeCod, txtPesCidadeDesc, comPesSituacao, comPesCategoria, comPesQtAoCredito, cheOrdemAlfDown, Dgv_Pesquisa);
            txtPesDescri.Select(); txtPesDescri.SelectAll();
        }
        private void cheOrdemAlfDown_CheckedChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabClien_Pesquisa Pesq = new TabClien_Pesquisa();
            Pesq.Pesc_EXECUTAR(_Login_LojaID_TabClien, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, txtPesDescri, txtPesApelido, txtPesEmpCod, txtPesEmpDesc, txtPesVendCod, txtPesVendDesc, mtbPesCpfCnpj, txtPesPrincipal, txtPesRota, txtPesConvenio, txtPesCidadeCod, txtPesCidadeDesc, comPesSituacao, comPesCategoria, comPesQtAoCredito, cheOrdemAlfDown, Dgv_Pesquisa);
            txtPesDescri.Select(); txtPesDescri.SelectAll();
        }
        private void txtPesDescri_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabClien_Pesquisa Pesq = new TabClien_Pesquisa();
            Pesq.Pesc_EXECUTAR(_Login_LojaID_TabClien, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, txtPesDescri, txtPesApelido, txtPesEmpCod, txtPesEmpDesc, txtPesVendCod, txtPesVendDesc, mtbPesCpfCnpj, txtPesPrincipal, txtPesRota, txtPesConvenio, txtPesCidadeCod, txtPesCidadeDesc, comPesSituacao, comPesCategoria, comPesQtAoCredito, cheOrdemAlfDown, Dgv_Pesquisa);
        }
        private void txtPesApelido_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabClien_Pesquisa Pesq = new TabClien_Pesquisa();
            Pesq.Pesc_EXECUTAR(_Login_LojaID_TabClien, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, txtPesDescri, txtPesApelido, txtPesEmpCod, txtPesEmpDesc, txtPesVendCod, txtPesVendDesc, mtbPesCpfCnpj, txtPesPrincipal, txtPesRota, txtPesConvenio, txtPesCidadeCod, txtPesCidadeDesc, comPesSituacao, comPesCategoria, comPesQtAoCredito, cheOrdemAlfDown, Dgv_Pesquisa);
        }
        #endregion

        //ABRE OS FORMULÁRIO DE PESQUISA
        #region ABRE AS PESQUISAS
        private void txtPesEmpCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtPesEmpCod.Text = txtPesEmpCod.Text.PadLeft(6, '0');
                txtPesEmpDesc.Text = string.Empty;


                PesEmpre.cs.PesEmpre_CALL Call = new PesEmpre.cs.PesEmpre_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesEmpre5Wenemy3156!.350?°";
                Call.PesEmpre_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtPesEmpCod.Text = Call._ResultPesquisaCALL;
                }

                txtPesEmpCod.SelectAll();
            }
        }
        private void txtPesVendCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtPesVendCod.Text = txtPesVendCod.Text.PadLeft(6, '0');
                txtPesVendDesc.Text = string.Empty;

                PesVende.cs.PesVende_CALL Call = new PesVende.cs.PesVende_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesVende21Wenemy3156!.350?°";
                Call.PesVende_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtPesVendCod.Text = Call._ResultPesquisaCALL;
                }

                txtPesVendCod.SelectAll();
            }
        }
        private void txtPesPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtPesPrincipal.Text = txtPesPrincipal.Text.PadLeft(6, '0');

                PesClie1.cs.PesClie1_CALL Call = new PesClie1.cs.PesClie1_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesClie13Wenemy3156!.350?°";
                Call.PesClie1_AUTORIZADO();


                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtPesPrincipal.Text = Call._ResultPesquisaCALL;
                }

                txtPesPrincipal.SelectAll();
            }
        }
        private void txtPesRota_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtPesRota.Text = txtPesRota.Text.PadLeft(6, '0');

                PesRotas.cs.PesRotas_CALL Call = new PesRotas.cs.PesRotas_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesRotas18Wenemy3156!.350?°";
                Call.PesRotas_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtPesRota.Text = Call._ResultPesquisaCALL;
                }

                txtPesRota.SelectAll();
            }
        }
        private void txtPesConvenio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtPesConvenio.Text = txtPesConvenio.Text.PadLeft(6, '0');


                PesConve.cs.PesConve_CALL Call = new PesConve.cs.PesConve_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesConve3Wenemy3156!.350?°";
                Call.PesConve_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtPesConvenio.Text = Call._ResultPesquisaCALL;
                }

                txtPesConvenio.SelectAll();
            }
        }
        private void txtPesCidadeCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtPesCidadeCod.Text = txtPesCidadeCod.Text.PadLeft(6, '0');
                txtPesCidadeDesc.Text = string.Empty;
                txtPesCidadeUF.Text = string.Empty;

                PesCidad.cs.PesCidad_CALL Call = new PesCidad.cs.PesCidad_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesCidad3Wenemy3156!.350?°";
                Call.PesCidad_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtPesCidadeCod.Text = Call._ResultPesquisaCALL;
                }

                txtPesCidadeCod.SelectAll();
            }
        }
        #endregion


        #endregion


        #region IMPRESSÃO

        #region Buttons Print, Fechar e Voltar
        //CHAMA OS FORMULÁRIOS DE IMPRESSÃO
        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
        //FECHA O FORMULARIO
        private void btnFecharIMP_Click(object sender, EventArgs e)
        {
            Close();
        }
        //VOLTA PARA A ABA 1
        private void btnVoltarIMP_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = Tp1;
        }
        #endregion







        //SELECIONA A EMPRESA
        private void txtImpEmpreCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtImpEmpreCod.Text != string.Empty)
            {
                if (Convert.ToInt32(txtImpEmpreCod.Text) == 0)
                {
                    txtImpEmpreCod.Text = string.Empty;
                    txtImpEmpreDesc.Text = string.Empty;
                }
                else
                {
                    TabClien_MET MET = new TabClien_MET();
                    MET.MET_SelecionaEmpresTAB(txtImpEmpreCod, txtImpEmpreDesc, grbInformacoesImpre);
                }
            }
            if (e.KeyCode == Keys.Tab && txtImpEmpreCod.Text == string.Empty)
            {
                txtImpEmpreCod.Text = string.Empty;
                txtImpEmpreDesc.Text = string.Empty;
            }

        }
        //SELECIONA A CIDADE
        private void txtImpCidadeCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (e.KeyCode == Keys.Tab && txtImpCidadeCod.Text != string.Empty)
                {
                    if (Convert.ToInt32(txtImpCidadeCod.Text) == 0)
                    {
                        txtImpCidadeCod.Text = string.Empty;
                        txtImpCidadeDesc.Text = string.Empty;
                        txtImpCidadeUF.Text = string.Empty;
                    }
                    else
                    {
                        TabClien_MET MET = new TabClien_MET();
                        MET.MET_SelecionaCidadeTAB(txtImpCidadeCod, txtImpCidadeDesc, txtImpCidadeUF, comImpSituacao);
                    }
                }
                if (e.KeyCode == Keys.Tab && txtImpCidadeCod.Text == string.Empty)
                {
                    txtImpCidadeCod.Text = string.Empty;
                    txtImpCidadeDesc.Text = string.Empty;
                    txtImpCidadeUF.Text = string.Empty;
                }
            }
        }


        //APAGA OS CAMPOS DE DESCRIÇÃO NO TEXTCHANGE
        private void txtImpEmpreCod_TextChanged(object sender, EventArgs e)
        {
            txtImpEmpreDesc.Text = string.Empty;
        }
        private void txtImpCidadeCod_TextChanged(object sender, EventArgs e)
        {
            txtImpCidadeDesc.Text = string.Empty;
            txtImpCidadeUF.Text = string.Empty;
        }

        //SELECT ALL NO CLICK
        private void txtImpEmpreCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtImpEmpreDesc.Text = string.Empty;
            txtImpEmpreCod.SelectAll();
        }
        private void txtImpCidadeCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtImpCidadeDesc.Text = string.Empty;
            txtImpCidadeUF.Text = string.Empty;
            txtImpCidadeCod.SelectAll();
        }


        //APENAS NÚMEROS
        private void txtImpEmpreCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtImpCidadeCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }


        #region APAGA O QT SELECT
        private void rabRPV_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
            if (rabRPV.Checked == true)
            {
                lblImpNomedoArquivo.Text = "TabClien.rpv";
            }
        }
        private void rabWORD_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
            if (rabWORD.Checked == true)
            {
                lblImpNomedoArquivo.Text = "WORD_TabClien";
            }
        }
        private void rabEXCEL_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
            if (rabEXCEL.Checked == true)
            {
                lblImpNomedoArquivo.Text = "EXC_TabClien";
            }
        }
        private void rabOrdemNumerica_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
        }
        private void rabOrdemAlfabetica_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
        }
        private void rabOrdemAlfabeticaApelid_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
        }
        private void comImpSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
        }
        private void comImpCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
        }
        private void cheImpApelid_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
        }
        #endregion

        //ABRE O BROWSER DE DIRETÓRIO
        private void btnAbrirPastaIMP_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Browser = new FolderBrowserDialog();
            txtCaminhoRel.Text = string.Empty;
            Browser.SelectedPath = string.Empty;

            DialogResult FOLDER = Browser.ShowDialog();


            if (Browser.SelectedPath.Length > 3)
            {
                txtCaminhoRel.Text = Browser.SelectedPath + @"\";
            }
            else
            {
                txtCaminhoRel.Text = Browser.SelectedPath;
            }
        }

        //SELECIONA O BUTTON DE ABRIR PASTA AO CLICAR NO TEXTBOX
        private void txtCaminhoRel_MouseDown(object sender, MouseEventArgs e)
        {
            btnAbrirPastaIMP.Select();
        }

        //ABRE AS PESQUISAS
        private void txtImpEmpreCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtImpEmpreCod.Text = txtImpEmpreCod.Text.PadLeft(6, '0');
                txtImpEmpreDesc.Text = string.Empty;


                PesEmpre.cs.PesEmpre_CALL Call = new PesEmpre.cs.PesEmpre_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesEmpre5Wenemy3156!.350?°";
                Call.PesEmpre_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtImpEmpreCod.Text = Call._ResultPesquisaCALL;
                }

                txtImpEmpreCod.SelectAll();
            }
        }
        private void txtImpCidadeCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtImpCidadeCod.Text = txtImpCidadeCod.Text.PadLeft(6, '0');
                txtImpCidadeDesc.Text = string.Empty;
                txtImpCidadeUF.Text = string.Empty;

                PesCidad.cs.PesCidad_CALL Call = new PesCidad.cs.PesCidad_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesCidad3Wenemy3156!.350?°";
                Call.PesCidad_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtImpCidadeCod.Text = Call._ResultPesquisaCALL;
                }

                txtImpCidadeCod.SelectAll();
            }
        }

        #endregion


        #region LIXEIRA
        //RESTAURA
        private void Dgv_Lixeira_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                //RESTAURA O ITEM DA LIXEIRA
                TabClien_Lixeira Lixeira = new TabClien_Lixeira();
                Lixeira.Lix_RESTAURAR(Dgv_Lixeira, cheVoltarLix, TabControl, Tp1, txtUsuario, btnGravar, txtMESTRE, CamposDisable, txtCodigo, btnIncluir, btnInfFinaShow, btnInfComerShow, btnAvancar, btnGravarAb2, btnVoltar);
            }
        }

        #endregion


        #region HISTÓRICO

        //FAZ A PESQUISA PELO SELECTED INDEX
        private void comIDHis_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Popula Histório
            TabClien_Histórico HIST = new TabClien_Histórico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, comIDHis, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb6);
        }
        //FAZ A PESQUISA PELO CLICK DO BOTÃO
        private void btnHistorico_Click(object sender, EventArgs e)
        {
            //Popula Histório
            TabClien_Histórico HIST = new TabClien_Histórico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, comIDHis, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb6);
        }




        //VERIFICA SE A DATA É VÁLIDA
        private void mtbData1His_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //VERIFICA SE A DATA É VALIDA
                TabClien_MET MET = new TabClien_MET();
                bool DATA = MET.MET_VerificaDATA(mtbData1His, panUpAb6);
                if (!DATA) { } else { return; }
            }
        }

        //VERIFICA SE A DATA É VALIDA 
        //VERIFICA SE É MAIOR QUE A PRIMEIRA
        //VERIFICA SE A PRIMEIRA ESTÁ EM BRANCO E ESTA ESTÁ PREENCHIDO
        private void mtbData2His_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //VERIFICA SE A DATA É VALIDA
                TabClien_MET MET = new TabClien_MET();
                TabClien_Histórico HIS = new TabClien_Histórico();
                bool DATA = MET.MET_VerificaDATA(mtbData2His, mtbData1His);
                if (!DATA) { } else { return; }

                //PREENCHER A PRIMEIRA DATA QUANDO A SEGUNDA ESTÁ PREENCHIDA
                bool OBRI = HIS.HIS_VerificaCamposObrig(mtbData1His, mtbData2His, panUpAb6);
                if (!OBRI) { } else { return; }

                //VERIFICA SE A SEGUNDA DATA É MENOR QUE A PRIMEIRA
                bool MENO = HIS.HIS_VerificaDataMenor(mtbData1His, mtbData2His, mtbData1His);
                if (!MENO) { } else { return; }
            }
        }


        //EVENTOS DE SELECT ALL E DE MOUSE DOWN
        #region SELECT ALL
        private void mtbData1His_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }

        private void mtbData2His_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");

        }

        private void mtbData1His_Click(object sender, EventArgs e)
        {
            mtbData1His.SelectAll();
        }

        private void mtbData2His_Click(object sender, EventArgs e)
        {
            mtbData2His.SelectAll();
        }

        private void txtUsuarioHis_MouseDown(object sender, MouseEventArgs e)
        {
            txtUsuarioDescHis.Text = string.Empty;
            txtUsuarioHis.SelectAll();
        }

        private void txtUsuarioHis_TextChanged(object sender, EventArgs e)
        {
            txtUsuarioDescHis.Text = string.Empty;
        }
        #endregion



        //FAZ A PESQUISA DO USUÁRIO
        private void txtUsuarioHis_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtUsuarioHis.Text == string.Empty)
                {

                }
                else
                {
                    if (Convert.ToInt32(txtUsuarioHis.Text) <= 0)
                    {
                        txtUsuarioHis.Text = string.Empty;
                    }
                    else
                    {
                        TabClien_Histórico HIST = new TabClien_Histórico();
                        HIST.HIS_VerificaUsuarioEXISTE(txtUsuarioHis, mtbData2His, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5, txtUsuarioDescHis);
                    }
                }
            }
        }
        //APENAS NÚMEROS
        private void txtUsuarioHis_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabClien_MET MET = new TabClien_MET();
            MET.MET_ApenasNúmeros(e);
        }
        //FICA EM BRANCO SE NÃO ACHAR USUÁRIO
        private void txtUsuarioHis_Leave(object sender, EventArgs e)
        {
            if (txtUsuarioDescHis.Text == string.Empty)
            {
                txtUsuarioHis.Text = string.Empty;
            }
        }


        //ABRE A PESQUISA DE USUÁRIO
        private void txtUsuarioHis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtUsuarioHis.Text = txtUsuarioHis.Text.PadLeft(6, '0');
                txtUsuarioDescHis.Text = string.Empty;

                PesUsuar.cs.PesUsuar_CALL Call = new PesUsuar.cs.PesUsuar_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabClien;
                Call._Login_CryptDesc = _Login_UsuarioID_TabClien;
                Call._WenCrypt = "PesUsuar21Wenemy3156!.350?°";
                Call.PesUsuar_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtUsuarioHis.Text = Call._ResultPesquisaCALL;
                }

                txtUsuarioHis.SelectAll();
            }
        }



        #endregion



















    }
}
