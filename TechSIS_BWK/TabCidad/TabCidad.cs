using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CfgComun;

namespace TabCidad
{
    internal partial class TabCidad : Form
    {
        public TabCidad()
        {
            InitializeComponent();
        }

        public string _Login_LojaID_TabCidad { get; set; }
        public string _Login_UsuarioID_TabCidad { get; set; }


        //LOAD DO FORM
        private void TabCidad_Load(object sender, EventArgs e)
        {
            CamposDisable();
            txtUsuario.Text = _Login_UsuarioID_TabCidad.PadLeft(6, '0');

            //PERMISSÃO POR BUTTONS
            TabCidad_Permi Permi = new TabCidad_Permi();
            Permi.Permiss_Buttons(btnIncluir, btnAlterar, btnExcluir, btnSeta1, btnSeta2, btnSeta3, btnSeta4, txtCodigo, _Login_UsuarioID_TabCidad);


            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panDownAb1, panDownAb2, panDownAb3, txtInformativo, lblImpreTitulo, panDownAb1, panDownAb1 };
            //MÉTODO DA CFG
            CfgComun.CfgComun_CLASS CFG = new CfgComun.CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, _Login_LojaID_TabCidad);


            //VERIFICA SE É PARA GRAVAR OS FILTROS
            TabCidad_FILTROS FILTROS = new TabCidad_FILTROS();
            FILTROS.cheFILTROSChecked(cheFiltrosIMP, cheFiltrosPES, _Login_LojaID_TabCidad);

            #region CANCELA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged -= new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged -= new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged -= new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged -= new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged -= new EventHandler(nupQtResultados_ValueChanged);
            comPesStatus.SelectedIndexChanged -= new EventHandler(comPesStatus_SelectedIndexChanged);
            #endregion
            //CARREGA OS FILTROS
            FILTROS.CarregaFILTROS(cheFiltrosPES, cheFiltrosIMP, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comPesStatus, rabOrdemNumerica, rabOrdemAlfabetica, rabRPV, rabWORD, rabEXCEL, rabTXT, comImpStatus, txtCaminhoRel, cheVoltarLix);
            #region VOLTA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged += new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged += new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged += new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged += new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged += new EventHandler(nupQtResultados_ValueChanged);
            comPesStatus.SelectedIndexChanged += new EventHandler(comPesStatus_SelectedIndexChanged);
            if (rabTOP.Checked == true) { nupQtResultados.Enabled = true; }
            #endregion



            TabCidad_MET MET = new TabCidad_MET();
            //Seleciona o caminho de salvar os relatorios
            txtCaminhoRel.Text = MET.MET_CapCaminhoSALV(txtCaminhoRel, _Login_LojaID_TabCidad);

            //Seleciona o button
            btnIncluir.Select();
        }

        //Define as propriedades ao trocar de Aba
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
                TabCidad_Lixeira Lix = new TabCidad_Lixeira();
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

                //Popula Lixeira
                TabCidad_Historico HIST = new TabCidad_Historico();
                HIST.HIS_SelecionaDados(mtbData1His, mtbData2His, txtUsuarioHis, txtUsuario);



                HIST.HIS_PopularHISTORICO(txtUsuarioHis, this, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
                mtbData1His.Select();
                SendKeys.Send("{HOME}");
                SendKeys.Send("+{END}");


                comIDHis.SelectedIndexChanged += new EventHandler(comIDHis_SelectedIndexChanged);
            }
            #endregion
        }

        //TECLAS DE ATALHO
        private void TabCidad_KeyDown(object sender, KeyEventArgs e)
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

        //PERMISSÃO POR ABAS
        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex != 0)
            {
                TabCidad_Permi Permi = new TabCidad_Permi();
                Permi.VerificaPermi_TbCont(e, e.TabPageIndex, txtCodigo, _Login_UsuarioID_TabCidad);
            }
        }

        //GRAVA OS FILTROS
        private void TabCidad_FormClosing(object sender, FormClosingEventArgs e)
        {
            TabCidad_FILTROS FILTROS = new TabCidad_FILTROS();
            FILTROS.GravarFILTROS(cheFiltrosPES, cheFiltrosIMP, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comPesStatus, rabOrdemNumerica, rabOrdemAlfabetica, rabRPV, rabWORD, rabEXCEL, rabTXT, comImpStatus, txtCaminhoRel, cheVoltarLix);
        }



        #region FORM PRINCIPAL


        //Metodos Zerar, Enable e Disable
        #region ZerarCampos, Enable e Disable
        public void ZerarCampos()
        {
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
            #region panUpAb1
            foreach (Control ctrl in panUpAb1.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox) && ctrl.Name != "txtInformativo")
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
        public void CamposEnable()
        {
            #region panCodigoAb1
            foreach (Control ctrl in panCodigoAb1.Controls)
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
            #region panUpAb1
            foreach (Control ctrl in panUpAb1.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox) && ctrl.Name != "txtPaisDesc" && ctrl.Name != "txtUFDesc" && ctrl.Name != "txtIbgeUF" && ctrl.Name != "txtIbgeMuDesc")
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
            #region panUpAb1
            foreach (Control ctrl in panUpAb1.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox) && ctrl.Name != "txtInformativo")
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
        }
        #endregion



        //BUTTONS
        #region Buttons INCLUIR, ALTERAR e EXCLUIR
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            //APLICA A APARENCIA AO CLICAR NO BUTTONS
            TabCidad_AppaButtons Appa = new TabCidad_AppaButtons();
            Appa._ButtonINC(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);
            //SELECIONA ULTIMO REGISTRO +1
            TabCidad_MET MET = new TabCidad_MET();
            MET.MET_SelecionaUltimoRegistroMaisUm(txtCodigo, btnGravar);
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //APLICA A APARENCIA AO CLICAR NO BUTTONS
            TabCidad_AppaButtons Appa = new TabCidad_AppaButtons();
            Appa._ButtonALT(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //APLICA A APARENCIA AO CLICAR NO BUTTONS
            TabCidad_AppaButtons Appa = new TabCidad_AppaButtons();
            Appa._ButtonEXC(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);
        }
        #endregion

        #region Buttons SETAS 1,2,3 e 4
        private void btnSeta1_Click(object sender, EventArgs e)
        {
            TabCidad_AppaButtons Apa = new TabCidad_AppaButtons();
            Apa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            //METODO DE SETAS
            TabCidad_ExecSETAS Exec = new TabCidad_ExecSETAS();
            Exec.ExecSETAs("1", txtMESTRE, txtCodigo, panCodigoAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, txtPaisCod, txtPaisDesc, txtIbgeUF, comUF, txtUFDesc, txtIbgeMuCod, txtIbgeMuDesc, txtIbgeEstadual, mtbCep1, mtbCep2, comStatus);
        }

        private void btnSeta2_Click(object sender, EventArgs e)
        {
            TabCidad_AppaButtons Apa = new TabCidad_AppaButtons();
            Apa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            //METODO DE SETAS
            TabCidad_ExecSETAS Exec = new TabCidad_ExecSETAS();
            Exec.ExecSETAs("2", txtMESTRE, txtCodigo, panCodigoAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, txtPaisCod, txtPaisDesc, txtIbgeUF, comUF, txtUFDesc, txtIbgeMuCod, txtIbgeMuDesc, txtIbgeEstadual, mtbCep1, mtbCep2, comStatus);
        }

        private void btnSeta3_Click(object sender, EventArgs e)
        {
            TabCidad_AppaButtons Apa = new TabCidad_AppaButtons();
            Apa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            //METODO DE SETAS
            TabCidad_ExecSETAS Exec = new TabCidad_ExecSETAS();
            Exec.ExecSETAs("3", txtMESTRE, txtCodigo, panCodigoAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, txtPaisCod, txtPaisDesc, txtIbgeUF, comUF, txtUFDesc, txtIbgeMuCod, txtIbgeMuDesc, txtIbgeEstadual, mtbCep1, mtbCep2, comStatus);
        }

        private void btnSeta4_Click(object sender, EventArgs e)
        {
            TabCidad_AppaButtons Apa = new TabCidad_AppaButtons();
            Apa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            //METODO DE SETAS
            TabCidad_ExecSETAS Exec = new TabCidad_ExecSETAS();
            Exec.ExecSETAs("4", txtMESTRE, txtCodigo, panCodigoAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, txtPaisCod, txtPaisDesc, txtIbgeUF, comUF, txtUFDesc, txtIbgeMuCod, txtIbgeMuDesc, txtIbgeEstadual, mtbCep1, mtbCep2, comStatus);
        }
        #endregion

        #region Buttons Ajuda, Gravar, Zerar e Fechar
        private void btnAjuda_Click(object sender, EventArgs e)
        {
            CamposEnable();
        }

        public string Motivo { get; set; }
        private void btnGravar_Click(object sender, EventArgs e)
        {
            //CAMPOS OBRIGATÓRIOS
            TabCidad_CamposObrig CamposObr = new TabCidad_CamposObrig();
            bool Preenc = CamposObr.CamposObrig(txtMESTRE, txtCodigo, panCodigoAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, txtPaisCod, txtPaisDesc, txtIbgeUF, comUF, txtUFDesc, txtIbgeMuCod, txtIbgeMuDesc, txtIbgeEstadual, mtbCep1, mtbCep2, comStatus);
            if (!Preenc) { } else { return; }

            TabCidad_btnGravar Gravar = new TabCidad_btnGravar();
            Gravar.GravarINC(txtMESTRE, txtCodigo, panCodigoAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, txtPaisCod, txtPaisDesc, txtIbgeUF, comUF, txtUFDesc, txtIbgeMuCod, txtIbgeMuDesc, txtIbgeEstadual, mtbCep1, mtbCep2, comStatus, txtUsuario);
            Gravar.GravarALT(txtMESTRE, txtCodigo, panCodigoAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, txtPaisCod, txtPaisDesc, txtIbgeUF, comUF, txtUFDesc, txtIbgeMuCod, txtIbgeMuDesc, txtIbgeEstadual, mtbCep1, mtbCep2, comStatus, txtUsuario);
            Gravar.GravarEXC(txtMESTRE, txtCodigo, Motivo, txtUsuario);
        }

        private void btnZerar_Click(object sender, EventArgs e)
        {
            ZerarCampos();
            TabCidad_AppaButtons Appa = new TabCidad_AppaButtons();
            Appa._ButtonZER(txtMESTRE, txtCodigo, btnIncluir, btnGravar, CamposDisable, TabControl, Tp1);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

    

        //SELECIONA OS CAMPOS NO TAB
        private void txtCodigo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //SELECIONA OS VALORES
                TabCidad_MET MET = new TabCidad_MET();
                MET.MET_SelecionarCodigoDigitarTAB(txtMESTRE, txtCodigo, panCodigoAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, txtPaisCod, txtPaisDesc, txtIbgeUF, comUF, txtUFDesc, txtIbgeMuCod, txtIbgeMuDesc, txtIbgeEstadual, mtbCep1, mtbCep2, comStatus);

                #region TRATAMENTO PARA EXCLUSÃO
                if (txtMESTRE.Text == "EXCLUIR" && txtDescri.Text != string.Empty)
                {
                    FromEx Form = new FromEx();
                    Form.ShowDialog();
                    Motivo = Form._MotivoExclusão;

                    if (Motivo != string.Empty)
                    {
                        btnGravar.PerformClick();
                    }
                    panCodigoAb1.Focus();
                    btnGravar.Enabled = false;
                    ZerarCampos(); CamposDisable();
                }
                #endregion
            }
        }



        //Adiciona o nome do estado e o código IBGE da UF
        private void comUF_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comUF.SelectedIndex)
            {
                case -1:
                    txtUFDesc.Text = string.Empty;
                    txtIbgeUF.Text = string.Empty;
                    break;
                case 0:
                    txtUFDesc.Text = "ACRE";
                    txtIbgeUF.Text = "12";
                    break;
                case 1:
                    txtUFDesc.Text = "ALAGOAS";
                    txtIbgeUF.Text = "27";
                    break;
                case 2:
                    txtUFDesc.Text = "AMAZONAS";
                    txtIbgeUF.Text = "13";
                    break;
                case 3:
                    txtUFDesc.Text = "AMAPÁ";
                    txtIbgeUF.Text = "16";
                    break;
                case 4:
                    txtUFDesc.Text = "BAHIA";
                    txtIbgeUF.Text = "29";
                    break;
                case 5:
                    txtUFDesc.Text = "CEARÁ";
                    txtIbgeUF.Text = "23";
                    break;
                case 6:
                    txtUFDesc.Text = "DISTRITO FEDERAL";
                    txtIbgeUF.Text = "53";
                    break;
                case 7:
                    txtUFDesc.Text = "ESPÍRITO SANTO";
                    txtIbgeUF.Text = "32";
                    break;
                case 8:
                    txtUFDesc.Text = "GOIÁS";
                    txtIbgeUF.Text = "52";
                    break;
                case 9:
                    txtUFDesc.Text = "MARANHÃO";
                    txtIbgeUF.Text = "21";
                    break;
                case 10:
                    txtUFDesc.Text = "MINAS GERAIS";
                    txtIbgeUF.Text = "31";
                    break;
                case 11:
                    txtUFDesc.Text = "MATO GROSSO DO SUL";
                    txtIbgeUF.Text = "50";
                    break;
                case 12:
                    txtUFDesc.Text = "MATO GROSSO";
                    txtIbgeUF.Text = "51";
                    break;
                case 13:
                    txtUFDesc.Text = "PARÁ";
                    txtIbgeUF.Text = "15";
                    break;
                case 14:
                    txtUFDesc.Text = "PARAÍBA";
                    txtIbgeUF.Text = "25";
                    break;
                case 15:
                    txtUFDesc.Text = "PERNAMBUCO";
                    txtIbgeUF.Text = "26";
                    break;
                case 16:
                    txtUFDesc.Text = "PIAUÍ";
                    txtIbgeUF.Text = "22";
                    break;
                case 17:
                    txtUFDesc.Text = "PARANÁ";
                    txtIbgeUF.Text = "41";
                    break;
                case 18:
                    txtUFDesc.Text = "RIO DE JANEIRO";
                    txtIbgeUF.Text = "33";
                    break;
                case 19:
                    txtUFDesc.Text = "RIO GRANDE DO NORTE";
                    txtIbgeUF.Text = "24";
                    break;
                case 20:
                    txtUFDesc.Text = "RONDÔNIA";
                    txtIbgeUF.Text = "11";
                    break;
                case 21:
                    txtUFDesc.Text = "RORAIMA";
                    txtIbgeUF.Text = "14";
                    break;
                case 22:
                    txtUFDesc.Text = "RIO GRANDE DO SUL";
                    txtIbgeUF.Text = "43";
                    break;
                case 23:
                    txtUFDesc.Text = "SANTA CATARINA";
                    txtIbgeUF.Text = "42";
                    break;
                case 24:
                    txtUFDesc.Text = "SERGIPE";
                    txtIbgeUF.Text = "28";
                    break;
                case 25:
                    txtUFDesc.Text = "SÃO PAULO";
                    txtIbgeUF.Text = "35";
                    break;
                case 26:
                    txtUFDesc.Text = "TOCANTINS";
                    txtIbgeUF.Text = "17";
                    break;
                case 27:
                    txtUFDesc.Text = "EXTERIOR";
                    txtIbgeUF.Text = "00";
                    break;
            }

        
        }


        //BUSCA O PAIS
        private void txtPaisCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                TabCidad_MET MET = new TabCidad_MET();
                MET.MET_SelecionaPAIS(txtPaisCod, txtPaisDesc, txtDescri, comUF, comStatus, txtIbgeMuCod, txtIbgeMuDesc, txtIbgeEstadual);
            }
        }
        //BUSCA O MUNICIPIO
        private void txtIbgeMuCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                TabCidad_MET MET = new TabCidad_MET();
                MET.MET_SelecionaMUNI(txtIbgeMuCod, txtIbgeMuDesc, txtPaisCod, comUF);
            }
        }
        //BUSCA A CIDADE PELA DESCRIÇÃO
        private void txtDescri_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtDescri.Text == string.Empty)
            {
                MessageBox.Show("Campo (Descrição) preenchido incorretamente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodigo.Select();
                return;
            }
            if (e.KeyCode == Keys.Tab && txtDescri.Text != string.Empty)
            {
                TabCidad_MET MET = new TabCidad_MET();
                bool JaEXISTE = MET.MET_VerificaCidadeEXISTE(txtDescri, txtCodigo, txtMESTRE);
                if (!JaEXISTE) { } else { return; }

                MET.MET_PopulaCamposCidade(txtDescri, txtIbgeMuCod, comUF, txtPaisCod, txtPaisDesc, txtIbgeMuDesc, txtIbgeEstadual, mtbCep1, txtMESTRE);
            }
        }

        //Apenas Números
        #region Apenas Números
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabCidad_MET MET = new TabCidad_MET();
            MET.ApenasNúmeros(e);
        }

        private void txtPaisCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabCidad_MET MET = new TabCidad_MET();
            MET.ApenasNúmeros(e);
        }

        private void txtIbgeMuCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabCidad_MET MET = new TabCidad_MET();
            MET.ApenasNúmeros(e);
        }

        private void txtIbgeEstadual_KeyPress(object sender, KeyPressEventArgs e)
        {
            //APENAS NÚMEROS
            TabCidad_MET MET = new TabCidad_MET();
            MET.ApenasNúmeros(e);
        }
        #endregion

        //Apaga os campos no TextChanged
        #region Apaga os campos no TextChanged
        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            btnGravar.Enabled = false;
        }

        private void txtPaisCod_TextChanged(object sender, EventArgs e)
        {
            txtPaisDesc.Text = string.Empty;
        }

        private void txtIbgeMuCod_TextChanged(object sender, EventArgs e)
        {
            txtIbgeMuDesc.Text = string.Empty;
        }
        #endregion

        //Abre os formulários de pesquisa
        #region Abre as Pesquisas
        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                PesCidad.cs.PesCidad_CALL Call = new PesCidad.cs.PesCidad_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabCidad;
                Call._Login_CryptDesc = _Login_UsuarioID_TabCidad;
                Call._WenCrypt = "PesCidad3Wenemy3156!.350?°";
                Call.PesCidad_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtCodigo.Text = Call._ResultPesquisaCALL;
                }

                txtCodigo.SelectAll();
            }
        }
        private void txtPaisCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                PesPaise.cs.PesPaise_CALL Call = new PesPaise.cs.PesPaise_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabCidad;
                Call._Login_CryptDesc = _Login_UsuarioID_TabCidad;
                Call._WenCrypt = "PesPaise16Wenemy3156!.350?°";
                Call.PesPaise_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtPaisCod.Text = Call._ResultPesquisaCALL;
                }

                txtPaisCod.SelectAll();
            }
        }
        private void txtIbgeMuCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                if (comUF.Text != string.Empty)
                {
                    PesMunic.cs.PesMunic_CALL Call = new PesMunic.cs.PesMunic_CALL();
                    Call._Login_CryptCode = _Login_LojaID_TabCidad;
                    Call._Login_CryptDesc = _Login_UsuarioID_TabCidad;
                    Call._UF_Call = comUF.Text;
                    Call._WenCrypt = "PesMunic13Wenemy3156!.350?°";
                    Call.PesMunic_AUTORIZADO();

                    if (Call._ResultPesquisaCALL != string.Empty)
                    {
                        txtIbgeMuCod.Text = Call._ResultPesquisaCALL;
                    }

                    txtIbgeMuCod.SelectAll();
                }
                else
                {
                    MessageBox.Show("Informe uma (UF) para começar a pesquisa", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIbgeMuCod.SelectAll();
                }
            }
        }
        #endregion

        //Apaga e da o SelectAll
        #region MouseDown nos Controles
        private void txtCodigo_MouseDown(object sender, MouseEventArgs e)
        {
            btnGravar.Enabled = false;
            txtCodigo.SelectAll();
        }

        private void txtPaisCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtPaisDesc.Text = string.Empty;
            txtPaisCod.SelectAll();
        }

        private void txtIbgeMuCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtIbgeMuDesc.Text = string.Empty;
            txtIbgeMuCod.SelectAll();
        }

        private void mtbCep2_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }

        private void mtbCep1_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }

        private void mtbCep2_MouseDown(object sender, MouseEventArgs e)
        {
            mtbCep2.SelectAll();
        }

        private void mtbCep1_MouseDown(object sender, MouseEventArgs e)
        {
            mtbCep1.SelectAll();
        }
        #endregion

        //Se não estiver nada escrito no CEP 1 joga o foco para o BT Gravar
        private void mtbCep1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (mtbCep1.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {
                    panButtonsDownAb1.Select();
                }
            }
        }


        #endregion

        #region PESQUISA

        #region Buttons Pesquisar, Voltar e Fechar
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA PELO BUTTON
            TabCidad_Pesquisa Pesquisa = new TabCidad_Pesquisa();
            Pesquisa.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, nupQtResultados, txtPesDescri, comPesStatus, rabAlfabetico, rabNumerico, rabTOP, _Login_LojaID_TabCidad);
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


        //EXECUTA A PESQUISA PELAS DIFERENTES PROPRIEDADES
        #region Pesquisa PELAS PROPRIEDADES
        private void rabNumerico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabNumerico.Checked == true)
            {
                //EXECUTA A PESQUISA PELO BUTTON
                TabCidad_Pesquisa Pesquisa = new TabCidad_Pesquisa();
                Pesquisa.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, nupQtResultados, txtPesDescri, comPesStatus, rabAlfabetico, rabNumerico, rabTOP, _Login_LojaID_TabCidad);
            }
        }

        private void rabAlfabetico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabAlfabetico.Checked == true)
            {
                //EXECUTA A PESQUISA PELO BUTTON
                TabCidad_Pesquisa Pesquisa = new TabCidad_Pesquisa();
                Pesquisa.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, nupQtResultados, txtPesDescri, comPesStatus, rabAlfabetico, rabNumerico, rabTOP, _Login_LojaID_TabCidad);
            }
        }

        private void rabTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTodos.Checked == true)
            {
                //EXECUTA A PESQUISA PELO BUTTON
                TabCidad_Pesquisa Pesquisa = new TabCidad_Pesquisa();
                Pesquisa.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, nupQtResultados, txtPesDescri, comPesStatus, rabAlfabetico, rabNumerico, rabTOP, _Login_LojaID_TabCidad);
            }
        }

        private void rabTOP_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTOP.Checked == true)
            {
                nupQtResultados.Enabled = true;
                //EXECUTA A PESQUISA PELO BUTTON
                TabCidad_Pesquisa Pesquisa = new TabCidad_Pesquisa();
                Pesquisa.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, nupQtResultados, txtPesDescri, comPesStatus, rabAlfabetico, rabNumerico, rabTOP, _Login_LojaID_TabCidad);
            }
            else
            {
                nupQtResultados.Value = 0;
                nupQtResultados.Enabled = false;
            }
        }

        private void nupQtResultados_ValueChanged(object sender, EventArgs e)
        {
            if (rabTOP.Checked == true)
            {
                //EXECUTA A PESQUISA PELO BUTTON
                TabCidad_Pesquisa Pesquisa = new TabCidad_Pesquisa();
                Pesquisa.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, nupQtResultados, txtPesDescri, comPesStatus, rabAlfabetico, rabNumerico, rabTOP, _Login_LojaID_TabCidad);
            }
        }

        private void txtPesDescri_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA PELO BUTTON
            TabCidad_Pesquisa Pesquisa = new TabCidad_Pesquisa();
            Pesquisa.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, nupQtResultados, txtPesDescri, comPesStatus, rabAlfabetico, rabNumerico, rabTOP, _Login_LojaID_TabCidad);
        }

        private void comPesStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA PELO BUTTON
            TabCidad_Pesquisa Pesquisa = new TabCidad_Pesquisa();
            Pesquisa.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, nupQtResultados, txtPesDescri, comPesStatus, rabAlfabetico, rabNumerico, rabTOP, _Login_LojaID_TabCidad);
        }
        #endregion



        //QUANTIDADE DE LINHAS ENCONTRADAS
        private void Dgv_Pesquisa_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }
        private void Dgv_Pesquisa_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }


        //SELECIONA O ITEM DA PESQUISA
        private void Dgv_Pesquisa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Codigo = Dgv_Pesquisa.CurrentRow.Cells[0].Value.ToString();
                TabCidad_AppaButtons Apa = new TabCidad_AppaButtons();
                Apa._ButtonZER(txtMESTRE, txtCodigo, btnIncluir, btnGravar, CamposDisable, TabControl, Tp1);
                txtCodigo.Text = Codigo;
                txtCodigo.Select();
                SendKeys.Send("{TAB}");
            }
        }

        #endregion

        #region IMPRESSÃO

        #region Buttons Print, Fechar e Voltar
        //DA O PRINT
        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
        //VOLTA PARA A TP1
        private void btnVoltarIMP_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = Tp1;
        }
        //FECHA O FORMULÁRIO
        private void btnFecharIMP_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion


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


        //DA O FOCO AO BOTÃO BROWSER AO SE CLICAR NO TXT DO CAMINHO DE SALVAMENTO
        private void txtCaminhoRel_MouseDown(object sender, MouseEventArgs e)
        {
            btnAbrirPastaIMP.Select();
        }


        //APLICA AS PROPRIEDADES NO CHECKED CHANGE
        private void rabRPV_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;

            if (rabRPV.Checked == true)
            {
                lblNomedoArquivo.Text = "Rpv_TabProgr.rpv";
            }
        }
        private void rabWORD_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;

            if (rabWORD.Checked == true)
            {
                lblNomedoArquivo.Text = "WORD_TabProgr.docx";
            }
        }
        private void rabEXCEL_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;

            if (rabEXCEL.Checked == true)
            {
                lblNomedoArquivo.Text = "EXC_TabProgr.xlsx";
            }
        }


        //APAGA O TXT DE QUANTIDADE DE RESULTADOS
        private void rabOrdemNumerica_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
        }
        private void rabOrdemAlfabetica_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
        }
        private void comImpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
        }

        #endregion

        #region LIXEIRA

        //RECUPERA O ITEM DA LIXEIRA
        private void Dgv_Lixeira_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                TabCidad_Lixeira Lix = new TabCidad_Lixeira();
                Lix.Lix_RESTAURAR(Dgv_Lixeira, cheVoltarLix, TabControl, Tp1, txtUsuario, btnGravar, txtMESTRE, CamposDisable, txtCodigo, btnIncluir);
            }
        }

        #endregion

        #region HISTÓRICO

        //VERIFICA SE A DATA É VÁLIDA
        private void mtbData1His_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //VERIFICA SE A DATA É VALIDA
                TabCidad_Historico HIS = new TabCidad_Historico();
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
                TabCidad_Historico HIS = new TabCidad_Historico();
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
                    TabCidad_Historico HIS = new TabCidad_Historico();
                    HIS.HIS_VerificaUsuarioEXISTE(txtUsuarioHis, mtbData2His, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
                }
            }
        }


        //EVENTOS PARA SELECT ALL
        #region SelectAll Eventos

        private void mtbData1His_Enter(object sender, EventArgs e)
        {
            mtbData1His.SelectAll();
        }

        private void mtbData2His_Enter(object sender, EventArgs e)
        {
            mtbData2His.SelectAll();
        }

        private void mtbData1His_Click(object sender, EventArgs e)
        {
            mtbData1His.SelectAll();
        }

        private void mtbData2His_Click(object sender, EventArgs e)
        {
            mtbData2His.SelectAll();
        }

        private void txtUsuarioHis_Click(object sender, EventArgs e)
        {
            txtUsuarioHis.SelectAll();
        }
        #endregion



        //FAZ A PESQUISA PELO SELECTED INDEX
        private void comIDHis_SelectedIndexChanged(object sender, EventArgs e)
        {
            //POPULA O HISTÓRICO A PARTIR DOS FILTROS
            TabCidad_Historico HIST = new TabCidad_Historico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, this, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
        }
        //FAZ A PESQUISA PELO CLICK DO BOTÃO
        private void btnHistorico_Click(object sender, EventArgs e)
        {
            //POPULA O HISTÓRICO A PARTIR DOS FILTROS
            TabCidad_Historico HIST = new TabCidad_Historico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, this, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
        }



        //Apenas Números
        private void txtUsuarioHis_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabCidad_MET MET = new TabCidad_MET();
            MET.MET_ApenasNúmeros(e);
        }

        //Abre a Pesquisa
        private void txtUsuarioHis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                PesUsuar.cs.PesUsuar_CALL Call = new PesUsuar.cs.PesUsuar_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabCidad;
                Call._Login_CryptDesc = _Login_UsuarioID_TabCidad;
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
