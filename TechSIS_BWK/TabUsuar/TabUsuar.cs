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

namespace TabUsuar
{
    internal partial class TabUsuar : Form
    {
        public TabUsuar()
        {
            InitializeComponent();
        }


        public string _Login_LojaID_TabUsuar { get; set; }
        public string _Login_UsuarioID_TabUsuar { get; set; }



        //Load do formulário
        private void TabUsuar_Load(object sender, EventArgs e)
        {
            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panDownAb1, panDownAb2, panDownAb3, panDownAb4, lblImpreTitulo, txtQtSelectIMP, txtQtSelectPES };
            //MÉTODO DA CFG
            CfgComun.CfgComun_CLASS CFG = new CfgComun.CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, _Login_LojaID_TabUsuar);

            //SELECIONA A PERMISSÃO DOS BUTTONS
            TabUsuar_Permi PERMI = new TabUsuar_Permi();
            PERMI.Permiss_Buttons(btnIncluir, btnAlterar, btnExcluir, btnSeta1, btnSeta2, btnSeta3, btnSeta4, txtCodigo, _Login_UsuarioID_TabUsuar);


            //VERIFICA SE É PARA GRAVAR OS FILTROS
            TabUsuar_FILTROS FILTROS = new TabUsuar_FILTROS();
            FILTROS.cheFILTROSChecked(cheFiltrosIMP, cheFiltrosPES, _Login_LojaID_TabUsuar);

            #region CANCELA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged -= new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged -= new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged -= new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged -= new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged -= new EventHandler(nupQtResultados_ValueChanged);
            comPesSituacao.SelectedIndexChanged -= new EventHandler(comPesSituacao_SelectedIndexChanged);
            comPesPermissao.SelectedIndexChanged -= new EventHandler(comPesPermissao_SelectedIndexChanged);
            #endregion
            //CARREGA OS FILTROS
            FILTROS.CarregaFILTROS(cheFiltrosPES, cheFiltrosIMP, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comPesSituacao, rabOrdemNumerica, rabOrdemAlfabetica, rabRPV, rabWORD, rabEXCEL, rabTXT, comSituacaoIMP, txtCaminhoRel, cheVoltarLix, comPesPermissao, comPermissaoIMP, cheImpApelid, rabOrdemAlfabeticaApelid);
            #region VOLTA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged += new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged += new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged += new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged += new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged += new EventHandler(nupQtResultados_ValueChanged);
            comPesSituacao.SelectedIndexChanged += new EventHandler(comPesSituacao_SelectedIndexChanged);
            comPesPermissao.SelectedIndexChanged += new EventHandler(comPesPermissao_SelectedIndexChanged);
            if (rabTOP.Checked == true) { nupQtResultados.Enabled = true; }
            #endregion

            //DESATIVA OS CONTROLES
            CamposDisable();

            //PREENCHE COM O USUÁRIO LOGADO
            txtUsuario.Text = _Login_UsuarioID_TabUsuar.PadLeft(6, '0');


            //SELECIONA A COR DE FUNDO DO FORMULARIO
            TabUsuar_MET MET = new TabUsuar_MET();
            //CAPTURA O CAMINHO DO ARQUIVO CASO NÃO ESTEJA NO XML DOS FILTROS
            txtCaminhoRel.Text = MET.MET_CapCaminhoSALV(txtCaminhoRel, _Login_LojaID_TabUsuar);



            btnIncluir.Select();
        }

        //Propriedades ao trocar de aba
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Prop. ABA 1
            if (TabControl.SelectedTab == Tp1)
            {
                txtMESTRE.Text = "SELECT";
                txtMESTRE.BackColor = Color.Silver;
                txtMESTRE.ForeColor = Color.Black;
                ZerarCampos();
                txtCodigo.Text = string.Empty;
                btnIncluir.Select();
            }
            #endregion
            #region Prop. ABA 2
            if (TabControl.SelectedTab == Tp2)
            {
                txtQtSelectPES.Text = string.Empty;

                txtMESTRE.Text = "PESQUISA";
                txtMESTRE.BackColor = Color.Salmon;
                txtMESTRE.ForeColor = Color.Green;


                //Apaga os campos quando a aba pesquisa é selecionada
                txtPesDescri.Select();


                txtPesDescri.Text = string.Empty;
                txtPesApelido.Text = string.Empty;
                txtPesEmpCod.Text = string.Empty;
                txtPesEmpDesc.Text = string.Empty;

                txtQtSelectPES.Text = string.Empty;
                Dgv_Pesquisa.Rows.Clear();
            }
            #endregion
            #region Prop. ABA 3
            if (TabControl.SelectedTab == Tp3)
            {
                txtQtSelectIMP.Text = string.Empty;

                txtMESTRE.Text = "IMPRESSÃO";
                txtMESTRE.BackColor = Color.Gold;
                txtMESTRE.ForeColor = Color.Red;
                btnPrint.Select();
            }
            #endregion
            #region Prop. ABA 4
            if (TabControl.SelectedTab == Tp4)
            {
                txtMESTRE.Text = "LIXEIRA";
                txtMESTRE.BackColor = Color.Coral;
                txtMESTRE.ForeColor = Color.Yellow;
                //Popula Lixeira
                TabUsuar_Lixeira Lix = new TabUsuar_Lixeira();
                Lix.Lix_POPULAR(Dgv_Lixeira, cheVoltarLix, TabControl, Tp1);
            }
            #endregion
            #region Prop. ABA 5
            if (TabControl.SelectedTab == Tp5)
            {
                comIDHis.SelectedIndexChanged -= new EventHandler(comIDHis_SelectedIndexChanged);

                txtMESTRE.Text = "HISTÓRICO";
                txtMESTRE.BackColor = Color.MediumSlateBlue;
                txtMESTRE.ForeColor = Color.White;

                //Popula Histório
                TabUsuar_Histórico HIST = new TabUsuar_Histórico();
                HIST.HIS_SelecionaDados(mtbData1His, mtbData2His, txtUsuarioHis, txtUsuario);



                HIST.HIS_PopularHISTORICO(txtUsuarioHis, this, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
                mtbData1His.Select();
                SendKeys.Send("{HOME}");
                SendKeys.Send("+{END}");


                comIDHis.SelectedIndexChanged += new EventHandler(comIDHis_SelectedIndexChanged);
            }
            #endregion
        }

        //Permissão por abas
        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex != 0)
            {
                TabUsuar_Permi Permi = new TabUsuar_Permi();
                Permi.VerificaPermi_TbCont(e, e.TabPageIndex, txtCodigo, _Login_UsuarioID_TabUsuar);
            }
        }

        //Grava os filtros
        private void TabUsuar_FormClosing(object sender, FormClosingEventArgs e)
        {
            TabUsuar_FILTROS FILTROS = new TabUsuar_FILTROS();
            FILTROS.GravarFILTROS(cheFiltrosPES, cheFiltrosIMP, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comPesSituacao, rabOrdemNumerica, rabOrdemAlfabetica, rabRPV, rabWORD, rabEXCEL, rabTXT, comSituacaoIMP, txtCaminhoRel, cheVoltarLix, comPesPermissao, comPermissaoIMP, cheImpApelid, rabOrdemAlfabeticaApelid);
        }


        //TECLAS DE ATALHOS DO FORMULÁRIO
        private void TabUsuar_KeyDown(object sender, KeyEventArgs e)
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
                    break;
                case Keys.F11:
                    btnPrint.PerformClick();
                    btnHistorico.PerformClick();
                    break;
            }
        }


        #region FORM PRINCIPAL

        #region ZerarCampos, CamposDisable, CamposEnable
        public void ZerarCampos()
        {
            Dgv_Empresas.Rows.Clear();
            #region panCodigoAb1
            foreach (Control ctrl in panCodigoAb1.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox) && ctrl.Name != "txtCodigo")
                {
                    (ctrl as TextBox).Text = string.Empty;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    (ctrl as ComboBox).SelectedIndex = -1;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    (ctrl as MaskedTextBox).Text = string.Empty;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    (ctrl as CheckBox).Checked = false;
                }
            }
            #endregion
            #region grbInformacoes
            foreach (Control ctrl in grbInformacoes.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox))
                {
                    (ctrl as TextBox).Text = string.Empty;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    (ctrl as ComboBox).SelectedIndex = -1;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    (ctrl as MaskedTextBox).Text = string.Empty;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    (ctrl as CheckBox).Checked = false;
                }
            }
            #endregion
            #region grbEmpresa
            foreach (Control ctrl in grbEmpresa.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox))
                {
                    (ctrl as TextBox).Text = string.Empty;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    (ctrl as ComboBox).SelectedIndex = -1;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    (ctrl as MaskedTextBox).Text = string.Empty;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    (ctrl as CheckBox).Checked = false;
                }
            }
            #endregion
            #region grbInformacoesDoUsuario
            foreach (Control ctrl in grbInformacoesDoUsuario.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox))
                {
                    (ctrl as TextBox).Text = string.Empty;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    (ctrl as ComboBox).SelectedIndex = -1;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    (ctrl as MaskedTextBox).Text = string.Empty;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    (ctrl as CheckBox).Checked = false;
                }
            }
            #endregion
            #region grbAtivo
            foreach (Control ctrl in grbAtivo.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox) && ctrl.Name != "txtUsuario")
                {
                    (ctrl as TextBox).Text = string.Empty;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    (ctrl as ComboBox).SelectedIndex = -1;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    (ctrl as MaskedTextBox).Text = string.Empty;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    (ctrl as CheckBox).Checked = false;
                }
            }
            #endregion
        }

        public void CamposDisable()
        {
            #region panCodigoAb1
            foreach (Control ctrl in panCodigoAb1.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox) && ctrl.Name != "txtCodigo")
                {
                    (ctrl as TextBox).Enabled = false;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    (ctrl as ComboBox).Enabled = false;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    (ctrl as MaskedTextBox).Enabled = false;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    (ctrl as CheckBox).Enabled = false;
                }
            }
            #endregion
            #region grbInformacoes
            foreach (Control ctrl in grbInformacoes.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox))
                {
                    (ctrl as TextBox).Enabled = false;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    (ctrl as ComboBox).Enabled = false;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    (ctrl as MaskedTextBox).Enabled = false;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    (ctrl as CheckBox).Enabled = false;
                }
            }
            #endregion
            #region grbEmpresa
            foreach (Control ctrl in grbEmpresa.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox))
                {
                    (ctrl as TextBox).Enabled = false;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    (ctrl as ComboBox).Enabled = false;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    (ctrl as MaskedTextBox).Enabled = false;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    (ctrl as CheckBox).Enabled = false;
                }
            }
            #endregion
            #region grbInformacoesDoUsuario
            foreach (Control ctrl in grbInformacoesDoUsuario.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox))
                {
                    (ctrl as TextBox).Enabled = false;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    (ctrl as ComboBox).Enabled = false;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    (ctrl as MaskedTextBox).Enabled = false;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    (ctrl as CheckBox).Enabled = false;
                }
            }
            #endregion
            #region grbAtivo
            foreach (Control ctrl in grbAtivo.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox) && ctrl.Name != "txtUsuario")
                {
                    (ctrl as TextBox).Enabled = false;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    (ctrl as ComboBox).Enabled = false;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    (ctrl as MaskedTextBox).Enabled = false;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    (ctrl as CheckBox).Enabled = false;
                }
            }
            #endregion
        }

        public void CamposEnable()
        {
            #region panCodigoAb1
            foreach (Control ctrl in panCodigoAb1.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox) && ctrl.Name != "txtDesci")
                {
                    (ctrl as TextBox).Enabled = true;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    (ctrl as ComboBox).Enabled = true;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    (ctrl as MaskedTextBox).Enabled = true;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    (ctrl as CheckBox).Enabled = true;
                }
            }
            #endregion
            #region grbInformacoes
            foreach (Control ctrl in grbInformacoes.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox))
                {
                    (ctrl as TextBox).Enabled = true;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    (ctrl as ComboBox).Enabled = true;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    (ctrl as MaskedTextBox).Enabled = true;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    (ctrl as CheckBox).Enabled = true;
                }
            }
            #endregion
            #region grbEmpresa
            foreach (Control ctrl in grbEmpresa.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox) && ctrl.Name != "txtEmpreDesc")
                {
                    (ctrl as TextBox).Enabled = true;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    (ctrl as ComboBox).Enabled = true;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    (ctrl as MaskedTextBox).Enabled = true;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    (ctrl as CheckBox).Enabled = true;
                }
            }
            #endregion
            #region grbInformacoesDoUsuario
            foreach (Control ctrl in grbInformacoesDoUsuario.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox) && ctrl.Name != "txtEmpreDescDown")
                {
                    (ctrl as TextBox).Enabled = true;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    (ctrl as ComboBox).Enabled = true;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    (ctrl as MaskedTextBox).Enabled = true;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    (ctrl as CheckBox).Enabled = true;
                }
            }
            #endregion
            #region grbAtivo
            foreach (Control ctrl in grbAtivo.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox) && ctrl.Name != "txtUsuario")
                {
                    (ctrl as TextBox).Enabled = true;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    (ctrl as ComboBox).Enabled = true;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    (ctrl as MaskedTextBox).Enabled = true;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    (ctrl as CheckBox).Enabled = true;
                }
            }
            #endregion
        }
        #endregion



        #region Buttons INCLUIR, ALTERAR e EXCLUIR
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            //APLICA A APARENCIA NO CLICK DO BUTTON
            TabUsuar_AppaButtons Appa = new TabUsuar_AppaButtons();
            Appa._ButtonINC(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);

            //SELECIONA O ULTIMO NO BANCO E ADICIONA MAIS 1
            TabUsuar_MET MET = new TabUsuar_MET();
            MET.MET_SelecionaUltimoRegistroMaisUm(txtCodigo, btnGravar);
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //APLICA A APARENCIA NO CLICK DO BUTTON
            TabUsuar_AppaButtons Appa = new TabUsuar_AppaButtons();
            Appa._ButtonALT(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //APLICA A APARENCIA NO CLICK DO BUTTON
            TabUsuar_AppaButtons Appa = new TabUsuar_AppaButtons();
            Appa._ButtonEXC(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);
        }
        #endregion

        #region Buttons SETAS 1 2 3 e 4
        private void btnSeta1_Click(object sender, EventArgs e)
        {
            TabUsuar_AppaButtons Appa = new TabUsuar_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);

            TabUsuar_ExecSETAS Exec = new TabUsuar_ExecSETAS();
            Exec.ExexSETAS("1", txtMESTRE, txtCodigo, ZerarCampos, CamposDisable, panCodigoAb1, txtDescri, comPermissao, txtEmailSuporte, txtMsn, txtEmailContato, txtSkype, txtEmpreCod, txtEmpreDesc, txtApelido, txtEmpresa, txtEmpreDescDown, txtCaixa, Dgv_Empresas, comStatus, btnGravar, CamposEnable);
        }

        private void btnSeta2_Click(object sender, EventArgs e)
        {
            TabUsuar_AppaButtons Appa = new TabUsuar_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);

            TabUsuar_ExecSETAS Exec = new TabUsuar_ExecSETAS();
            Exec.ExexSETAS("2", txtMESTRE, txtCodigo, ZerarCampos, CamposDisable, panCodigoAb1, txtDescri, comPermissao, txtEmailSuporte, txtMsn, txtEmailContato, txtSkype, txtEmpreCod, txtEmpreDesc, txtApelido, txtEmpresa, txtEmpreDescDown, txtCaixa, Dgv_Empresas, comStatus, btnGravar, CamposEnable);
        }

        private void btnSeta3_Click(object sender, EventArgs e)
        {
            TabUsuar_AppaButtons Appa = new TabUsuar_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);

            TabUsuar_ExecSETAS Exec = new TabUsuar_ExecSETAS();
            Exec.ExexSETAS("3", txtMESTRE, txtCodigo, ZerarCampos, CamposDisable, panCodigoAb1, txtDescri, comPermissao, txtEmailSuporte, txtMsn, txtEmailContato, txtSkype, txtEmpreCod, txtEmpreDesc, txtApelido, txtEmpresa, txtEmpreDescDown, txtCaixa, Dgv_Empresas, comStatus, btnGravar, CamposEnable);
        }

        private void btnSeta4_Click(object sender, EventArgs e)
        {
            TabUsuar_AppaButtons Appa = new TabUsuar_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);

            TabUsuar_ExecSETAS Exec = new TabUsuar_ExecSETAS();
            Exec.ExexSETAS("4", txtMESTRE, txtCodigo, ZerarCampos, CamposDisable, panCodigoAb1, txtDescri, comPermissao, txtEmailSuporte, txtMsn, txtEmailContato, txtSkype, txtEmpreCod, txtEmpreDesc, txtApelido, txtEmpresa, txtEmpreDescDown, txtCaixa, Dgv_Empresas, comStatus, btnGravar, CamposEnable);
        }
        #endregion

        #region Buttons Ajuda, Gravar, Fechar e Zerar
        private void btnAjuda_Click(object sender, EventArgs e)
        {
            //VAI PARA AJUDA
            TabUsuar_FILTROS FILTROS = new TabUsuar_FILTROS();
            FILTROS.LinkAjuda();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            //Define os campos obrigatórios
            TabUsuar_CamposObrig CamposObrig = new TabUsuar_CamposObrig();
            bool Preench = CamposObrig.CamposObrig(txtCodigo, txtDescri, comPermissao, txtEmpreCod, txtEmpreDesc, txtApelido, comStatus, Dgv_Empresas, txtCaixa, txtMESTRE);
            if (!Preench) { } else { return; }


            TabUsuar_btnGravar Gravar = new TabUsuar_btnGravar();
            Gravar.GravarINC(txtMESTRE, txtCodigo, ZerarCampos, CamposDisable, panCodigoAb1, txtDescri, comPermissao, txtEmailSuporte, txtMsn, txtEmailContato, txtSkype, txtEmpreCod, txtEmpreDesc, txtApelido, txtEmpresa, txtEmpreDescDown, txtCaixa, Dgv_Empresas, comStatus, btnGravar, CamposEnable, txtUsuario);
            Gravar.GravarALT(txtMESTRE, txtCodigo, ZerarCampos, CamposDisable, panCodigoAb1, txtDescri, comPermissao, txtEmailSuporte, txtMsn, txtEmailContato, txtSkype, txtEmpreCod, txtEmpreDesc, txtApelido, txtEmpresa, txtEmpreDescDown, txtCaixa, Dgv_Empresas, comStatus, btnGravar, CamposEnable, txtUsuario);
            Gravar.GravarEXC(txtMESTRE, txtCodigo, ZerarCampos, CamposDisable, panCodigoAb1, txtDescri, comPermissao, txtEmailSuporte, txtMsn, txtEmailContato, txtSkype, txtEmpreCod, txtEmpreDesc, txtApelido, txtEmpresa, txtEmpreDescDown, txtCaixa, Dgv_Empresas, comStatus, btnGravar, CamposEnable, txtUsuario, MotivoExc);
        }

        private void btnZerar_Click(object sender, EventArgs e)
        {
            ZerarCampos();
            //APLICA A APARENCIA NO CLICK DO BUTTON
            TabUsuar_AppaButtons Appa = new TabUsuar_AppaButtons();
            Appa._ButtonZER(txtMESTRE, txtCodigo, btnIncluir, btnGravar, CamposDisable, TabControl, Tp1);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion



        //DEFINE OS CONTROLES QUE SO PODEM RECEBER NÚMEROS
        #region Apenas Números
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabUsuar_MET MET = new TabUsuar_MET();
            MET.MET_ApenasNúmeros(e);
        }

        private void txtEmpreCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabUsuar_MET MET = new TabUsuar_MET();
            MET.MET_ApenasNúmeros(e);
        }

        private void txtLoja_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabUsuar_MET MET = new TabUsuar_MET();
            MET.MET_ApenasNúmeros(e);
        }

        private void txtCaixa_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabUsuar_MET MET = new TabUsuar_MET();
            MET.MET_ApenasNúmeros(e);
        }
        #endregion



        //FAZ O SELECT NO TAB
        //Tab no campo Código
        public string MotivoExc { get; set; }
        private void txtCodigo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtCodigo.Text == "1000000")
                {
                    MessageBox.Show("Limite de sequência atingido. Verifique!", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    panCodigoAb1.Focus(); txtCodigo.SelectAll();
                    return;
                }

                TabUsuar_MET MET = new TabUsuar_MET();
                MET.MET_SelecionaCodigoTAB(txtMESTRE, txtCodigo, ZerarCampos, CamposDisable, panCodigoAb1, txtDescri, comPermissao, txtEmailSuporte, txtMsn, txtEmailContato, txtSkype, txtEmpreCod, txtEmpreDesc, txtApelido, txtEmpresa, txtEmpreDescDown, txtCaixa, Dgv_Empresas, comStatus, btnGravar, CamposEnable);

                //Faz o tratamento quando chama o form de exlusão
                #region TRATAMENTO PARA EXCLUSÃO
                if (txtMESTRE.Text == "EXCLUIR" && txtDescri.Text != string.Empty)
                {
                    FromEx Ex = new FromEx();
                    Ex.ShowDialog();
                    MotivoExc = Ex._MotivoExclusão;
                    if (MotivoExc != string.Empty)
                    {
                        btnGravar.PerformClick();
                    }
                    btnGravar.Enabled = false;
                    ZerarCampos();
                    CamposDisable();
                    panCodigoAb1.Focus();
                    txtCodigo.SelectAll();
                }
                #endregion
            }
        }



        //POPULA O DATAGRID VIEW
        private void txtCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCaixa.Text = txtCaixa.Text.PadLeft(6, '0');

                if (Convert.ToInt32(txtCaixa.Text) == 0)
                {
                    MessageBox.Show("Campo (Caixa) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    TabUsuar_MET MET = new TabUsuar_MET();
                    MET.MET_PopulaDataGrid(Dgv_Empresas, txtEmpresa, txtCaixa, txtCodigo, btnGravar, txtEmpreDescDown);
                }
            }
        }
        private void txtCaixa_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (Dgv_Empresas.Rows.Count == 0)
                {
                    MessageBox.Show("Adicione um número de caixa e tecle ENTER para continuar", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmpresa.Select(); txtCaixa.SelectAll();
                }
                else
                {
                    txtCaixa.Text = string.Empty;
                    txtEmpreDescDown.Text = string.Empty;
                    txtEmpresa.Text = string.Empty;
                }
            }
        }


        //BUSCA A LOJA NO TAB
        #region BUSCA LOJA
        //BUSCA LOJA
        private void txtEmpreCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                TabUsuar_MET MET = new TabUsuar_MET();
                MET.MET_BuscaDadosLoja(txtEmpreCod, txtEmpreDesc, grbEmpresa);
            }
        }
        //BUSCA LOJA
        private void txtEmpresa_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtEmpresa.Text == string.Empty && Dgv_Empresas.Rows.Count > 0)
                {

                }
                else
                {
                    TabUsuar_MET MET = new TabUsuar_MET();
                    MET.MET_BuscaDadosLoja(txtEmpresa, txtEmpreDescDown, txtApelido);
                }
            }
        }
        #endregion



        //Abre as pesquisas no F6
        #region Abre as Pesquisas
        //EXECUTA O FORM DE PESQUISA
        private void txtEmpreCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtEmpreDesc.Text = string.Empty;


                PesEmpre.cs.PesEmpre_CALL Call = new PesEmpre.cs.PesEmpre_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabUsuar;
                Call._Login_CryptDesc = _Login_UsuarioID_TabUsuar;
                Call._WenCrypt = "PesEmpre5Wenemy3156!.350?°";
                Call.PesEmpre_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtEmpreCod.Text = Call._ResultPesquisaCALL;
                }

                txtEmpreCod.SelectAll();
            }
        }
        //EXECUTA O FORM DE PESQUISA
        private void txtEmpresa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtEmpreDescDown.Text = string.Empty;

                PesEmpre.cs.PesEmpre_CALL Call = new PesEmpre.cs.PesEmpre_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabUsuar;
                Call._Login_CryptDesc = _Login_UsuarioID_TabUsuar;
                Call._WenCrypt = "PesEmpre5Wenemy3156!.350?°";
                Call.PesEmpre_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtEmpresa.Text = Call._ResultPesquisaCALL;
                }

                txtEmpresa.SelectAll();
            }
        }
        //EXECUTA O FORM DE PESQUISA
        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                PesUsuar.cs.PesUsuar_CALL Call = new PesUsuar.cs.PesUsuar_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabUsuar;
                Call._Login_CryptDesc = _Login_UsuarioID_TabUsuar;
                Call._WenCrypt = "PesUsuar21Wenemy3156!.350?°";
                Call.PesUsuar_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtCodigo.Text = Call._ResultPesquisaCALL;
                }

                txtCodigo.SelectAll();
            }
        }
        #endregion


        //Define que o campo é obrigatorio ao passar por ele no TAB
        #region Obrigação no TAB
        private void txtDescri_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtDescri.Text == string.Empty)
                {
                    MessageBox.Show("Campo (Descrição) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigo.Select();
                }
            }
        }
        private void txtApelido_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtApelido.Text == string.Empty)
                {
                    MessageBox.Show("Campo (Apelido) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    grbInformacoesDoUsuario.Focus();
                }
            }
        }
        private void comPermissao_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (comPermissao.SelectedIndex <= 0)
                {
                    MessageBox.Show("Campo (Permissão) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDescri.Select(); comPermissao.SelectAll();
                }
            }
        }
        #endregion



        //Remove a linha e popula os textboxs
        private void Dgv_Empresas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Remove a linha e popula os textboxs
            TabUsuar_MET MET = new TabUsuar_MET();
            MET.MET_RemoveLinhaDGV_PopulaTXT(Dgv_Empresas, btnGravar, e, txtEmpresa, txtEmpreDescDown, txtCaixa);
        }


        //SelectAll no MouseDown
        #region SelectAll no Click
        private void txtCodigo_MouseDown(object sender, MouseEventArgs e)
        {
            txtCodigo.SelectAll();
            btnGravar.Enabled = false;
        }

        private void txtEmpreCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtEmpreCod.SelectAll();
            txtEmpreDesc.Text = string.Empty;
        }

        private void txtEmpresa_MouseDown(object sender, MouseEventArgs e)
        {
            txtEmpresa.SelectAll();
            txtEmpreDescDown.Text = string.Empty;
        }

        private void txtCaixa_MouseDown(object sender, MouseEventArgs e)
        {
            txtCaixa.SelectAll();
        }
        #endregion


        //MUDOU O TEXTO, A FK SOME
        private void txtEmpreCod_TextChanged(object sender, EventArgs e)
        {
            txtEmpreDesc.Text = string.Empty;
        }
        private void txtEmpresa_TextChanged(object sender, EventArgs e)
        {
            txtEmpreDescDown.Text = string.Empty;
        }



        //DESABILITA PARA EVITAR BOBERA
        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            btnGravar.Enabled = false;
        }

        #endregion


        #region PESQUISA

        #region Buttons Pesquisar, Voltar e Fechar
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //faz a pesquisa
            TabUsuar_Pesquisa Pesq = new TabUsuar_Pesquisa();
            Pesq.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comPesSituacao, comPesPermissao, txtPesDescri, txtPesApelido, txtPesEmpCod, txtQtSelectPES, txtPesEmpDesc, _Login_LojaID_TabUsuar);
            if (txtQtSelectPES.Text == string.Empty) { txtQtSelectPES.Text = "000000"; }
            if (Convert.ToInt32(txtQtSelectPES.Text) == 0)
            {
                MessageBox.Show("Nenhuma informação encontrada. Verifique os filtros!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            txtPesDescri.Select(); txtPesDescri.SelectAll();
        }
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = Tp1;
        }
        private void btnFechar2_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion





        //Preenche a quantidade de resultados encontrados
        private void Dgv_Pesquisa_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }
        private void Dgv_Pesquisa_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }



        #region FAZ A PESQUISA PELAS PROPRIEDADES DOS CONTROLES
        private void txtPesDescri_TextChanged(object sender, EventArgs e)
        {
            //faz a pesquisa
            TabUsuar_Pesquisa Pesq = new TabUsuar_Pesquisa();
            Pesq.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comPesSituacao, comPesPermissao, txtPesDescri, txtPesApelido, txtPesEmpCod, txtQtSelectPES, txtPesEmpDesc, _Login_LojaID_TabUsuar);
        }

        private void txtPesApelido_TextChanged(object sender, EventArgs e)
        {
            //faz a pesquisa
            TabUsuar_Pesquisa Pesq = new TabUsuar_Pesquisa();
            Pesq.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comPesSituacao, comPesPermissao, txtPesDescri, txtPesApelido, txtPesEmpCod, txtQtSelectPES, txtPesEmpDesc, _Login_LojaID_TabUsuar);
        }

        private void comPesSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //faz a pesquisa
            TabUsuar_Pesquisa Pesq = new TabUsuar_Pesquisa();
            Pesq.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comPesSituacao, comPesPermissao, txtPesDescri, txtPesApelido, txtPesEmpCod, txtQtSelectPES, txtPesEmpDesc, _Login_LojaID_TabUsuar);
        }

        private void comPesPermissao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //faz a pesquisa
            TabUsuar_Pesquisa Pesq = new TabUsuar_Pesquisa();
            Pesq.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comPesSituacao, comPesPermissao, txtPesDescri, txtPesApelido, txtPesEmpCod, txtQtSelectPES, txtPesEmpDesc, _Login_LojaID_TabUsuar);
        }

        private void rabNumerico_CheckedChanged(object sender, EventArgs e)
        {
            //faz a pesquisa
            TabUsuar_Pesquisa Pesq = new TabUsuar_Pesquisa();
            Pesq.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comPesSituacao, comPesPermissao, txtPesDescri, txtPesApelido, txtPesEmpCod, txtQtSelectPES, txtPesEmpDesc, _Login_LojaID_TabUsuar);
        }

        private void rabAlfabetico_CheckedChanged(object sender, EventArgs e)
        {
            //faz a pesquisa
            TabUsuar_Pesquisa Pesq = new TabUsuar_Pesquisa();
            Pesq.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comPesSituacao, comPesPermissao, txtPesDescri, txtPesApelido, txtPesEmpCod, txtQtSelectPES, txtPesEmpDesc, _Login_LojaID_TabUsuar);
        }

        private void rabTodos_CheckedChanged(object sender, EventArgs e)
        {
            //faz a pesquisa
            TabUsuar_Pesquisa Pesq = new TabUsuar_Pesquisa();
            Pesq.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comPesSituacao, comPesPermissao, txtPesDescri, txtPesApelido, txtPesEmpCod, txtQtSelectPES, txtPesEmpDesc, _Login_LojaID_TabUsuar);
        }

        private void rabTOP_CheckedChanged(object sender, EventArgs e)
        {
            //Habilita o numeric up down
            #region TRATAMENTO PARA HABILITAR O NUMERIC UP DOWN
            if (rabTOP.Checked == true)
            {
                nupQtResultados.Enabled = true;
            }
            else
            {
                nupQtResultados.Enabled = false;
                nupQtResultados.Value = 0;
            }
            #endregion

            //faz a pesquisa
            TabUsuar_Pesquisa Pesq = new TabUsuar_Pesquisa();
            Pesq.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comPesSituacao, comPesPermissao, txtPesDescri, txtPesApelido, txtPesEmpCod, txtQtSelectPES, txtPesEmpDesc, _Login_LojaID_TabUsuar);

        }

        private void nupQtResultados_ValueChanged(object sender, EventArgs e)
        {
            //faz a pesquisa
            TabUsuar_Pesquisa Pesq = new TabUsuar_Pesquisa();
            Pesq.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comPesSituacao, comPesPermissao, txtPesDescri, txtPesApelido, txtPesEmpCod, txtQtSelectPES, txtPesEmpDesc, _Login_LojaID_TabUsuar);
        }

        #endregion


        //SELECIONA O REGISTRO AO CLICAR NO DESEJADO
        private void Dgv_Pesquisa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string VariavelPesquisa = Dgv_Pesquisa.CurrentRow.Cells[0].Value.ToString();
                TabControl.SelectedTab = Tp1;
                TabUsuar_AppaButtons Apa = new TabUsuar_AppaButtons();
                Apa._ButtonZER(txtMESTRE, txtCodigo, btnIncluir, btnGravar, CamposDisable, TabControl, Tp1);
                txtCodigo.Text = VariavelPesquisa;
                txtCodigo.Select(); txtCodigo.SelectAll();
                SendKeys.Send("{TAB}");
            }
        }



        //Busca a loja
        private void txtPesEmpCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtPesEmpCod.Text != string.Empty)
                {
                    txtPesEmpCod.Text = txtPesEmpCod.Text.PadLeft(6, '0');

                    //Busca a loja
                    TabUsuar_MET MET = new TabUsuar_MET();
                    MET.MET_BuscaDadosLoja(txtPesEmpCod, txtPesEmpDesc, txtPesApelido);
                }
                else
                {
                    txtPesEmpDesc.Text = string.Empty;
                }
            }
        }
        //Limpa a loja no mouseDown
        private void txtPesEmpCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtPesEmpCod.SelectAll();
            txtPesEmpDesc.Text = string.Empty;
        }



        //Chama a pesquisa
        private void txtPesEmpCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtPesEmpDesc.Text = string.Empty;

                PesEmpre.cs.PesEmpre_CALL Call = new PesEmpre.cs.PesEmpre_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabUsuar;
                Call._Login_CryptDesc = _Login_UsuarioID_TabUsuar;
                Call._WenCrypt = "PesEmpre5Wenemy3156!.350?°";
                Call.PesEmpre_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtPesEmpCod.Text = Call._ResultPesquisaCALL;
                }

                txtPesEmpCod.SelectAll();
            }
        }


        //APENAS NÚMEROS
        private void txtPesEmpCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabUsuar_MET MET = new TabUsuar_MET();
            MET.MET_ApenasNúmeros(e);
        }



        #endregion


        #region IMPRESSÃO

        //CHAMA OS FORMULÁRIOS DE IMPRESSÃO
        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnVoltarIMP_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = Tp1;
        }

        private void btnFecharIMP_Click(object sender, EventArgs e)
        {
            Close();
        }


        //ABRE O BROWSER DE DIRETÓRIO
        private void btnAbrirPastaIMP_Click(object sender, EventArgs e)
        {
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




        //BUSCA OS DADOS DAS LOJAS
        private void txtEmpreCodImp_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtEmpreCodImp.Text != string.Empty)
                {
                    TabUsuar_MET MET = new TabUsuar_MET();
                    MET.MET_BuscaDadosLoja(txtEmpreCodImp, txtEmpreDescImp, grbInformacoesImpre);
                }
            }
        }
        //APAGA OS DADOS NO CLICK
        private void txtEmpreCodImp_MouseDown(object sender, MouseEventArgs e)
        {
            txtEmpreCodImp.SelectAll();
            txtEmpreDescImp.Text = string.Empty;
        }


        

        

        //SELECIONA O BUTTON DE ABRIR PASTA AO CLICAR NO TEXTBOX
        private void txtCaminhoRel_MouseDown(object sender, MouseEventArgs e)
        {
            btnAbrirPastaIMP.Select();
        }




        //APAGA O VALOR DA QUANTIDADE DE SELECT DADO
        #region APAGA O VALOR DA QUANTIDADE DE SELECT
        private void rabRPV_CheckedChanged(object sender, EventArgs e)
        {
            lblNomedoArquivo.Text = "Rpv_TabUsuar.rpv";
            txtQtSelectIMP.Text = string.Empty;
        }

        private void rabWORD_CheckedChanged(object sender, EventArgs e)
        {
            lblNomedoArquivo.Text = "WORD_TabUsuar.docx";
            txtQtSelectIMP.Text = string.Empty;
        }

        private void rabEXCEL_CheckedChanged(object sender, EventArgs e)
        {
            lblNomedoArquivo.Text = "EXC_TabUsuar.xlsx";
            txtQtSelectIMP.Text = string.Empty;
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

        private void comSituacaoIMP_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
        }

        private void comPermissaoIMP_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
        }

        private void cheImpApelid_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
        }
        #endregion



        //EXECUTA A PESQUISA
        private void txtEmpreCodImp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtEmpreDescImp.Text = string.Empty;

                PesEmpre.cs.PesEmpre_CALL Call = new PesEmpre.cs.PesEmpre_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabUsuar;
                Call._Login_CryptDesc = _Login_UsuarioID_TabUsuar;
                Call._WenCrypt = "PesEmpre5Wenemy3156!.350?°";
                Call.PesEmpre_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtEmpreCodImp.Text = Call._ResultPesquisaCALL;
                }

                txtEmpreCodImp.SelectAll();
            }
        }



        //APENAS NÚMEROS
        private void txtEmpreCodImp_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabUsuar_MET MET = new TabUsuar_MET();
            MET.MET_ApenasNúmeros(e);
        }

        #endregion


        #region LIXEIRA
        //Restaura
        private void Dgv_Lixeira_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                //RESTAURA O ITEM DA LIXEIRA
                TabUsuar_Lixeira Lixeira = new TabUsuar_Lixeira();
                Lixeira.Lix_RESTAURAR(Dgv_Lixeira, cheVoltarLix, TabControl, Tp1, txtUsuario, btnGravar, txtMESTRE, CamposDisable, txtCodigo, btnIncluir);
            }
        }
        #endregion


        #region HISTÓRICO

        //FAZ A PESQUISA PELO SELECTED INDEX
        private void comIDHis_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Popula Histório
            TabUsuar_Histórico HIST = new TabUsuar_Histórico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, comIDHis, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
        }
        //FAZ A PESQUISA PELO CLICK DO BOTÃO
        private void btnHistorico_Click(object sender, EventArgs e)
        {
            //Popula Histório
            TabUsuar_Histórico HIST = new TabUsuar_Histórico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, comIDHis, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
        }




        //ABRE A PESQUISA DO USUÁRIO
        private void txtUsuarioHis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                PesUsuar.cs.PesUsuar_CALL Call = new PesUsuar.cs.PesUsuar_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabUsuar;
                Call._Login_CryptDesc = _Login_UsuarioID_TabUsuar;
                Call._WenCrypt = "PesUsuar21Wenemy3156!.350?°";
                Call.PesUsuar_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtUsuarioHis.Text = Call._ResultPesquisaCALL;
                }

                txtUsuarioHis.SelectAll();
            }
        }


        //VERIFICA SE A DATA É VÁLIDA
        private void mtbData1His_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //VERIFICA SE A DATA É VALIDA
                TabUsuar_Histórico HIS = new TabUsuar_Histórico();
                bool DATA = HIS.HIS_VerificaDATA(mtbData1His, panUpAb5);
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
                TabUsuar_Histórico HIS = new TabUsuar_Histórico();
                bool DATA = HIS.HIS_VerificaDATA(mtbData2His, mtbData1His);
                if (!DATA) { } else { return; }

                //PREENCHER A PRIMEIRA DATA QUANDO A SEGUNDA ESTÁ PREENCHIDA
                bool OBRI = HIS.HIS_VerificaCamposObrig(mtbData1His, mtbData2His, panUpAb5);
                if (!OBRI) { } else { return; }

                //VERIFICA SE A SEGUNDA DATA É MENOR QUE A PRIMEIRA
                bool MENO = HIS.HIS_VerificaDataMenor(mtbData1His, mtbData2His, mtbData1His);
                if (!MENO) { } else { return; }
            }
        }


        //VERIFICA SE O USUÁRIO EXISTE
        private void txtUsuarioHis_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtUsuarioHis.Text != string.Empty)
                {
                    TabUsuar_Histórico HIS = new TabUsuar_Histórico();
                    HIS.HIS_VerificaUsuarioEXISTE(txtUsuarioHis, mtbData2His, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5, txtUsuarioDescHis);
                }
                else
                {
                    txtUsuarioDescHis.Text = string.Empty;
                }
            }
        }



        //SelectAll dependendo do Evento
        #region Eventos de SelectAll
        private void txtUsuarioHis_MouseDown(object sender, MouseEventArgs e)
        {
            txtUsuarioHis.SelectAll();
            txtUsuarioDescHis.Text = string.Empty;
        }

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
        #endregion




        //APENAS NÚMEROS
        private void txtUsuarioHis_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabUsuar_MET MET = new TabUsuar_MET();
            MET.MET_ApenasNúmeros(e);
        }
       


    

       
        
        #endregion



 

       

    }
}