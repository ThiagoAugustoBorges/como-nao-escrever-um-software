using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TabPermi
{
    internal partial class TabPermi : Form
    {
        public TabPermi()
        {
            InitializeComponent();
        }


        public string _Login_LojaID_TabPermi { get; set; }
        public string _Login_UsuarioID_TabPermi { get; set; }


        //EVENTO LOAD DO FORM
        private void TabPermi_Load(object sender, EventArgs e)
        {
            //Preenche com o usário ativo
            txtUsuarCodigo.Text = _Login_UsuarioID_TabPermi.PadLeft(6, '0');
            txtUsuario.Text = _Login_UsuarioID_TabPermi.PadLeft(6, '0');

            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panDownAb1, panDownAb2, panDownAb3, txtQtSelectPES, lblImpreTitulo, panRadionButtonsAb1, txtQtSelectIMP };
            //MÉTODO DA CFG
            CfgComun.CfgComun_CLASS CFG = new CfgComun.CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, _Login_LojaID_TabPermi);

            TabPermi_Permissão PERMI = new TabPermi_Permissão();
            PERMI.Permiss_Buttons(btnIncluir, btnAlterar, btnExcluir, btnExcluir, btnExcluir, btnExcluir, btnExcluir, txtUsuarCodigo, _Login_UsuarioID_TabPermi);

            TabPermi_FILTROS FILTROS = new TabPermi_FILTROS();
            //VERIFICA SE É PARA GRAVAR OS FILTROS
            FILTROS.cheFILTROSChecked(cheFiltrosIMP, cheFiltrosPES, _Login_LojaID_TabPermi);

            #region CANCELA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged -= new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged -= new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged -= new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged -= new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged -= new EventHandler(nupQtResultados_ValueChanged);
            #endregion
            //PREENCHE OS FILTROS
            FILTROS.CarregaFILTROS(cheFiltrosPES, cheFiltrosIMP, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, rabOrdemNumerica, rabOrdemAlfabetica, rabRPV, rabWORD, rabEXCEL, rabTXT, txtCaminhoRel, cheImpApelid);
            #region VOLTA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged += new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged += new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged += new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged += new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged += new EventHandler(nupQtResultados_ValueChanged);
            if (rabTOP.Checked == true) { nupQtResultados.Enabled = true; }
            #endregion


            TabPermi_MET MET = new TabPermi_MET();
            //CAPTURA CAMINHO SALVAR ARQUIVO
            txtCaminhoRel.Text = FILTROS.MET_CapCaminhoSALV(txtCaminhoRel, _Login_LojaID_TabPermi);
            


            btnIncluir.Select();
        }


        //EVENTO AO TROCAR DE ABAS
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Prop. ABA 1
            if (TabControl.SelectedTab == Tp1)
            {
                txtMESTRE.Text = "SELECT";
                txtMESTRE.BackColor = Color.Silver;
                txtMESTRE.ForeColor = Color.Black;
                ZerarCampos_grb1(); ZerarCampos_grb2();
                CamposDisable_grb1(); CamposDisable_grb2();
                txtUsuarCodigo.Text = string.Empty;
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
                txtPesUsuarCod.Select();


                txtPesUsuarCod.Text = string.Empty;
                txtPesProgrCod.Text = string.Empty;

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

                txtImpUsuarCod.Text = string.Empty;
                txtImpProgrCod.Text = string.Empty;

                txtImpUsuarCod.Select();
            }
            #endregion
            #region Prop. ABA 4
            if (TabControl.SelectedTab == Tp4)
            {
                comIDHis.SelectedIndexChanged -= new EventHandler(comIDHis_SelectedIndexChanged);

                txtMESTRE.Text = "HISTÓRICO";
                txtMESTRE.BackColor = Color.MediumSlateBlue;
                txtMESTRE.ForeColor = Color.White;

                //Popula Histório
                TabPermi_Histórico HIST = new TabPermi_Histórico();
                HIST.HIS_SelecionaDados(mtbData1His, mtbData2His, txtUsuarioHis, txtUsuario);



                HIST.HIS_PopularHISTORICO(txtUsuarioHis, this, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis);
                mtbData1His.Select();
                SendKeys.Send("{HOME}");
                SendKeys.Send("+{END}");


                comIDHis.SelectedIndexChanged += new EventHandler(comIDHis_SelectedIndexChanged);
            }
            #endregion
        }

        //TECLAS DE ATALHO
        private void TabPermi_KeyDown(object sender, KeyEventArgs e)
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


        //GRAVA OS FILTROS
        private void TabPermi_FormClosing(object sender, FormClosingEventArgs e)
        {
            TabPermi_FILTROS FILTROS = new TabPermi_FILTROS();
            FILTROS.GravarFILTROS(cheFiltrosPES, cheFiltrosIMP, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, rabOrdemNumerica, rabOrdemAlfabetica, rabRPV, rabWORD, rabEXCEL, rabTXT, txtCaminhoRel, cheImpApelid);
        }


        //PERMISSÃO POR ABAS
        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex != 0)
            {
                TabPermi_Permissão PERMI = new TabPermi_Permissão();
                PERMI.VerificaPermi_TbCont(e, e.TabPageIndex, txtUsuarCodigo, _Login_UsuarioID_TabPermi);
            }
        }


        #region FORM PRINCIPAL



        #region ZerarCampos, CamposEnable e CamposDisable
        public void CamposEnable_grb1()
        {
            #region grb1
            foreach (Control ctrl in grb1.Controls)
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
                if (ctrl.GetType() == typeof(Button))
                {
                    (ctrl as Button).Enabled = true;
                }
                if (ctrl.GetType() == typeof(Label))
                {
                    (ctrl as Label).Enabled = true;
                }
            }
            #endregion
        }
        public void CamposDisable_grb1()
        {
            #region grb1
            foreach (Control ctrl in grb1.Controls)
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
                if (ctrl.GetType() == typeof(NumericUpDown))
                {
                    (ctrl as NumericUpDown).Enabled = false;
                }
                if (ctrl.GetType() == typeof(Button))
                {
                    (ctrl as Button).Enabled = false;
                }
                if (ctrl.GetType() == typeof(Label))
                {
                    (ctrl as Label).Enabled = false;
                }
            }
            #endregion
        }
        public void CamposEnable_grb2()
        {
            #region grb2
            foreach (Control ctrl in grb2.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox) && ctrl.Name != "txtDescicaoBl2")
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
                if (ctrl.GetType() == typeof(Button))
                {
                    (ctrl as Button).Enabled = true;
                }
                if (ctrl.GetType() == typeof(Label))
                {
                    (ctrl as Label).Enabled = true;
                }
            }
            #endregion
        }
        public void CamposDisable_grb2()
        {
            #region grb2
            foreach (Control ctrl in grb2.Controls)
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
                if (ctrl.GetType() == typeof(NumericUpDown))
                {
                    (ctrl as NumericUpDown).Enabled = false;
                }
                if (ctrl.GetType() == typeof(Button))
                {
                    (ctrl as Button).Enabled = false;
                }
                if (ctrl.GetType() == typeof(Label))
                {
                    (ctrl as Label).Enabled = false;
                }
            }
            #endregion
        }
        public void ZerarCampos_grb1()
        {
            #region grb1
            foreach (Control ctrl in grb1.Controls)
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
                if (ctrl.GetType() == typeof(NumericUpDown))
                {
                    (ctrl as NumericUpDown).Value = 0;
                }

            }
            #endregion
        }
        public void ZerarCampos_grb2()
        {
            #region grb2
            foreach (Control ctrl in grb2.Controls)
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
                if (ctrl.GetType() == typeof(NumericUpDown))
                {
                    (ctrl as NumericUpDown).Value = 0;
                }

            }
            #endregion
        }
        #endregion

        #region Buttonsn INCLUIR, ALTERAR E EXCLUIR
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            TabPermi_AppaButtons Appa = new TabPermi_AppaButtons();
            Appa._ButtonINC(txtMESTRE, btnGravar, txtUsuarCodigo, TabControl, Tp1, CamposEnable_grb1, CamposDisable_grb1, CamposEnable_grb1, CamposDisable_grb1, ZerarCampos_grb1, ZerarCampos_grb2, txtUsuarDescri);
            panRadionButtonsAb1.Enabled = true;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            TabPermi_AppaButtons Appa = new TabPermi_AppaButtons();
            Appa._ButtonALT(txtMESTRE, btnGravar, txtUsuarCodigo, TabControl, Tp1, CamposEnable_grb1, CamposDisable_grb1, CamposEnable_grb1, CamposDisable_grb1, ZerarCampos_grb1, ZerarCampos_grb2, txtUsuarDescri);
            panRadionButtonsAb1.Enabled = true;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            TabPermi_AppaButtons Appa = new TabPermi_AppaButtons();
            Appa._ButtonEXC(txtMESTRE, btnGravar, txtUsuarCodigo, TabControl, Tp1, CamposEnable_grb1, CamposDisable_grb1, CamposEnable_grb1, CamposDisable_grb1, ZerarCampos_grb1, ZerarCampos_grb2, txtUsuarDescri);
            panRadionButtonsAb1.Enabled = true;
        }
        #endregion
        #region Buttons SETAS 1, 2, 3 e 4
        private void btnSeta1_Click(object sender, EventArgs e)
        {

        }

        private void btnSeta2_Click(object sender, EventArgs e)
        {

        }

        private void btnSeta3_Click(object sender, EventArgs e)
        {

        }

        private void btnSeta4_Click(object sender, EventArgs e)
        {

        }
        #endregion



        #region Buttons Gravar, Zerar, Fechar e Ajuda
        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (rabAb1Blocos.Checked == true && comBlocoBl1.SelectedIndex < 0)
            {
                comBlocoBl1.SelectedIndex = 0;
            }

            TabPermi_btnGRAVAR Gravar = new TabPermi_btnGRAVAR();
            Gravar._ButtonINC_BLOC(txtUsuarCodigo, btnGravar, panUsuarioAb1, CamposEnable_grb1, CamposDisable_grb1, CamposEnable_grb2, CamposDisable_grb2, ZerarCampos_grb1, ZerarCampos_grb2, txtUsuarDescri, rabAb1Blocos, rabAb1Unitario, txtCodigoBl2, txtDescicaoBl2, grb2, txtMESTRE, chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2, chePerIncBl1, chePerAltBl1, chePerExcBl1, cheConAltBl1, cheBloqAbasBl1, nupDown1Bl1, nupDown2Bl1, nupDown3Bl1, nupDown4Bl1, btnMarcarTodasBl1, btnPassAntBl1, btnPassProxBl1, txtUsuario, comBlocoBl1);
            Gravar._ButtonALT_BLOC(txtUsuarCodigo, btnGravar, panUsuarioAb1, CamposEnable_grb1, CamposDisable_grb1, CamposEnable_grb2, CamposDisable_grb2, ZerarCampos_grb1, ZerarCampos_grb2, txtUsuarDescri, rabAb1Blocos, rabAb1Unitario, txtCodigoBl2, txtDescicaoBl2, grb2, txtMESTRE, chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2, chePerIncBl1, chePerAltBl1, chePerExcBl1, cheConAltBl1, cheBloqAbasBl1, nupDown1Bl1, nupDown2Bl1, nupDown3Bl1, nupDown4Bl1, btnMarcarTodasBl1, btnPassAntBl1, btnPassProxBl1, txtUsuario, comBlocoBl1);
            Gravar._ButtonEXC_BLOC(txtUsuarCodigo, btnGravar, panUsuarioAb1, CamposEnable_grb1, CamposDisable_grb1, CamposEnable_grb2, CamposDisable_grb2, ZerarCampos_grb1, ZerarCampos_grb2, txtUsuarDescri, rabAb1Blocos, rabAb1Unitario, txtCodigoBl2, txtDescicaoBl2, grb2, txtMESTRE, chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2, chePerIncBl1, chePerAltBl1, chePerExcBl1, cheConAltBl1, cheBloqAbasBl1, nupDown1Bl1, nupDown2Bl1, nupDown3Bl1, nupDown4Bl1, btnMarcarTodasBl1, btnPassAntBl1, btnPassProxBl1, txtUsuario, comBlocoBl1, MOTIVO_BLOC, MotivoExc, INDEX);

        }

        private void btnZerar_Click(object sender, EventArgs e)
        {
            TabPermi_AppaButtons Appa = new TabPermi_AppaButtons();
            Appa._ButtonZER(txtMESTRE, btnGravar, txtUsuarCodigo, TabControl, Tp1, CamposEnable_grb1, CamposDisable_grb1, CamposEnable_grb2, CamposDisable_grb2, ZerarCampos_grb1, ZerarCampos_grb2, btnIncluir, txtUsuarDescri);
            rabAb1Blocos.Checked = false; rabAb1Unitario.Checked = false;
            panRadionButtonsAb1.Enabled = false;
            panUsuarioAb1.Enabled = false;

            btnIncluir.Select();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAjuda_Click(object sender, EventArgs e)
        {
            TabPermi_FILTROS FILTROS = new TabPermi_FILTROS();
            FILTROS.LinkAjuda();
        }
        #endregion



        //PASSA UM POR UM
        #region Buttons PASSAR
        private void btnPassAntBl1_Click(object sender, EventArgs e)
        {
            if (comBlocoBl1.SelectedIndex == -1 || comBlocoBl1.SelectedIndex == 0)
            {
                comBlocoBl1.SelectedIndex = 5;
            }
            else
            {
                comBlocoBl1.SelectedIndex--;
            }
        }
        private void btnPassProxBl1_Click(object sender, EventArgs e)
        {
            try
            {
                comBlocoBl1.SelectedIndex++;
            }
            catch (Exception)
            {
                comBlocoBl1.SelectedIndex = 0;
            }
        }


        private void btnPassAntBl2_Click(object sender, EventArgs e)
        {
            btnGravar.Enabled = false;

            txtCodigoBl2.TextChanged -= new System.EventHandler(this.txtCodigoBl2_TextChanged);
            TabPermi_MET MET = new TabPermi_MET();
            MET.MET_SelecionaPrograma_SETAS("<", txtCodigoBl2, txtDescicaoBl2, "ORDER BY Sequen_PGR DESC");
            MET.DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnMarcarTodasBl2, btnMarcarTodasBl2);
            MET.ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
            txtCodigoBl2.TextChanged += new System.EventHandler(this.txtCodigoBl2_TextChanged);


        }

        private void btnPassProxBl2_Click(object sender, EventArgs e)
        {
            btnGravar.Enabled = false;

            txtCodigoBl2.TextChanged -= new System.EventHandler(this.txtCodigoBl2_TextChanged);
            TabPermi_MET MET = new TabPermi_MET();
            MET.MET_SelecionaPrograma_SETAS(">", txtCodigoBl2, txtDescicaoBl2, "");
            MET.DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnMarcarTodasBl2, btnMarcarTodasBl2);
            MET.ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
            txtCodigoBl2.TextChanged += new System.EventHandler(this.txtCodigoBl2_TextChanged);
        }
        #endregion


        //MARCA OS CHECKBOX
        #region Buttons MARCAR TODAS
        private void btnMarcarTodasBl1_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in grb1.Controls)
            {
                if (ctrl.GetType() == typeof(CheckBox) && ctrl.Name != "cheBloqAbasBl1")
                {
                    (ctrl as CheckBox).Checked = true;
                }
            }
        }
        private void btnMarcarTodasBl2_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in grb2.Controls)
            {
                if (ctrl.GetType() == typeof(CheckBox) && ctrl.Name != "cheBloqAbasBl2")
                {
                    (ctrl as CheckBox).Checked = true;
                }
            }
        }
        #endregion





        //HABILITA OS FORMULARIOS
        private void rabAb1Blocos_CheckedChanged(object sender, EventArgs e)
        {
            panUsuarioAb1.Enabled = true;

            txtUsuarDescri.Text = string.Empty;
            btnGravar.Enabled = false;

            if (rabAb1Blocos.Checked == true)
            {
                ZerarCampos_grb1();
                ZerarCampos_grb2();
                CamposEnable_grb1();
            }
            else
            {
                ZerarCampos_grb1();
                ZerarCampos_grb2();
                CamposDisable_grb1();
            }

            txtUsuarCodigo.Select();
            txtUsuarCodigo.SelectAll();
        }
        private void rabAb1Unitario_CheckedChanged(object sender, EventArgs e)
        {
            panUsuarioAb1.Enabled = true;

            txtUsuarDescri.Text = string.Empty;
            btnGravar.Enabled = false;

            if (rabAb1Unitario.Checked == true)
            {
                ZerarCampos_grb1();
                ZerarCampos_grb2();
                CamposEnable_grb2();

                TabPermi_MET MET = new TabPermi_MET();
                MET.DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);
            }
            else
            {
                ZerarCampos_grb1();
                ZerarCampos_grb2();
                CamposDisable_grb2();
            }

            txtUsuarCodigo.Select();
            txtUsuarCodigo.SelectAll();
        }



        //ZERA OS NÚMERICS UP DOWN
        private void cheBloqAbasBl1_CheckedChanged(object sender, EventArgs e)
        {
            if (cheBloqAbasBl1.Checked == true && chePerIncBl1.Enabled == true)
            {
                nupDown1Bl1.Enabled = true;
                nupDown2Bl1.Enabled = true;
                nupDown3Bl1.Enabled = true;
                nupDown4Bl1.Enabled = true;
            }
            if (cheBloqAbasBl1.Checked == false && chePerIncBl1.Enabled == true)
            {
                nupDown1Bl1.Value = 0;
                nupDown2Bl1.Value = 0;
                nupDown3Bl1.Value = 0;
                nupDown4Bl1.Value = 0;

                nupDown1Bl1.Enabled = false;
                nupDown2Bl1.Enabled = false;
                nupDown3Bl1.Enabled = false;
                nupDown4Bl1.Enabled = false;
            }
        }
        private void cheBloqAbasBl2_CheckedChanged(object sender, EventArgs e)
        {
            if (cheBloqAbasBl2.Checked == true && chePerIncBl2.Enabled == true)
            {
                nupDown1Bl2.Enabled = true;
                nupDown2Bl2.Enabled = true;
                nupDown3Bl2.Enabled = true;
                nupDown4Bl2.Enabled = true;
            }
            if (cheBloqAbasBl2.Checked == false && chePerIncBl2.Enabled == true)
            {
                nupDown1Bl2.Value = 0;
                nupDown2Bl2.Value = 0;
                nupDown3Bl2.Value = 0;
                nupDown4Bl2.Value = 0;

                nupDown1Bl2.Enabled = false;
                nupDown2Bl2.Enabled = false;
                nupDown3Bl2.Enabled = false;
                nupDown4Bl2.Enabled = false;
            }
        }





        //SELECIONA O USUÁRIO
        public string MOTIVO_BLOC { get; set; }
        public int INDEX { get; set; }
        private void txtUsuarCodigo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //SELECIONA O USUÁRIO LOGADO
                TabPermi_MET MET = new TabPermi_MET();
                MET.MET_SelecionaUsuario(txtUsuarCodigo, btnGravar, panUsuarioAb1, CamposEnable_grb1, CamposDisable_grb1, CamposEnable_grb2, CamposDisable_grb2, ZerarCampos_grb1, ZerarCampos_grb2, txtUsuarDescri, rabAb1Blocos, rabAb1Unitario, chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2, txtDescicaoBl2);

                #region TRATAMENTO EXCLUSÃO
                if (txtMESTRE.Text == "EXCLUIR" && rabAb1Blocos.Checked == true)
                {
                    FromEx_BLOC Ex = new FromEx_BLOC();
                    Ex.ShowDialog();
                    MOTIVO_BLOC = Ex.MOTIVO;
                    INDEX = Ex.SELECTEDIndex;
                    if (MOTIVO_BLOC != string.Empty && INDEX > -1)
                    {
                        btnGravar.PerformClick();
                    }
                    btnGravar.Enabled = false;
                    MET.ZeraCamposChecked(chePerIncBl1, chePerAltBl1, chePerExcBl1, cheConAltBl1, cheBloqAbasBl1, nupDown1Bl1, nupDown2Bl1, nupDown3Bl1, nupDown4Bl1, null);
                    MET.DesabilitarCamposChecked(chePerIncBl1, chePerAltBl1, chePerExcBl1, cheConAltBl1, cheBloqAbasBl1, nupDown1Bl1, nupDown2Bl1, nupDown3Bl1, nupDown4Bl1, btnMarcarTodasBl1, btnPassAntBl1, btnPassProxBl1);
                    comBlocoBl1.Enabled = false;
                    txtUsuarDescri.Text = string.Empty;
                    panUsuarioAb1.Focus();
                    txtUsuarCodigo.SelectAll();
                }
                #endregion
            }
        }

        //SELECIONA O PROGRAMA
        public string MotivoExc { get; set; }
        private void txtCodigoBl2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            TabPermi_MET MET = new TabPermi_MET();

            if (e.KeyCode == Keys.Tab)
            {
                if (txtUsuarCodigo.Text != string.Empty && txtUsuarDescri.Text != string.Empty)
                {
                    //SELECIONA O PROGRAMA
                    MET.MET_SelecionaPrograma(txtUsuarCodigo, btnGravar, panUsuarioAb1, CamposEnable_grb1, CamposDisable_grb1, CamposEnable_grb1, CamposDisable_grb2, ZerarCampos_grb1, ZerarCampos_grb2, txtUsuarDescri, rabAb1Blocos, rabAb1Unitario, txtCodigoBl2, txtDescicaoBl2, grb2, txtMESTRE, chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);

                    #region TRATAMENTO EXCLUSÃO
                    if (txtMESTRE.Text == "EXCLUIR" && txtDescicaoBl2.Text != string.Empty)
                    {
                        FromEx Ex = new FromEx();
                        Ex.ShowDialog();
                        MotivoExc = Ex._MotivoExclusão;
                        if (MotivoExc != string.Empty)
                        {
                            btnGravar.PerformClick();
                        }
                        btnGravar.Enabled = false;
                        MET.ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
                        MET.DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);
                        txtDescicaoBl2.Text = string.Empty;
                        grb2.Focus();
                        txtCodigoBl2.SelectAll();
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("INFORME UM USUÁRIO VÁLIDO PARA CONFIGURAR UMA PERMISSÃO", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MET.ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
                    MET.DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);
                    panUsuarioAb1.Focus();
                    txtUsuarDescri.Text = string.Empty;
                    txtCodigoBl2.Text = string.Empty; txtDescicaoBl2.Text = string.Empty;
                    txtUsuarCodigo.SelectAll();
                }
            }
        }



        //ZERA AO CLICAR NO CAMPO DO PROGRAMA
        private void txtCodigoBl2_MouseDown(object sender, MouseEventArgs e)
        {
            txtDescicaoBl2.Text = string.Empty;
            btnGravar.Enabled = false;
            TabPermi_MET MET = new TabPermi_MET();
            MET.ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
            MET.DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);

            txtCodigoBl2.SelectAll();
        }
        private void txtCodigoBl2_Click(object sender, EventArgs e)
        {
            txtDescicaoBl2.Text = string.Empty;
            btnGravar.Enabled = false;
            TabPermi_MET MET = new TabPermi_MET();
            MET.ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
            MET.DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);

            txtCodigoBl2.SelectAll();
        }
        private void txtCodigoBl2_TextChanged(object sender, EventArgs e)
        {
            txtDescicaoBl2.Text = string.Empty;
            btnGravar.Enabled = false;
            TabPermi_MET MET = new TabPermi_MET();
            MET.ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
            MET.DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);
        }


        //ZERA AO CLICAR NO CAMPO
        private void txtUsuarCodigo_MouseDown(object sender, MouseEventArgs e)
        {
            txtUsuarCodigo.SelectAll();
            txtUsuarDescri.Text = string.Empty;

            txtDescicaoBl2.Text = string.Empty;
            txtCodigoBl2.Text = string.Empty;

            txtDescicaoBl2.Enabled = false;
            txtCodigoBl2.Enabled = false;

            btnGravar.Enabled = false;
            TabPermi_MET MET = new TabPermi_MET();
            MET.ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
            MET.DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);

        }
        private void txtUsuarCodigo_Click(object sender, EventArgs e)
        {
            txtUsuarCodigo.SelectAll();
            txtUsuarDescri.Text = string.Empty;

            txtDescicaoBl2.Text = string.Empty;
            txtCodigoBl2.Text = string.Empty;

            txtDescicaoBl2.Enabled = false;
            txtCodigoBl2.Enabled = false;

            btnGravar.Enabled = false;
            TabPermi_MET MET = new TabPermi_MET();
            MET.ZeraCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, txtDescicaoBl2);
            MET.DesabilitarCamposChecked(chePerIncBl2, chePerAltBl2, chePerExcBl2, cheConAltBl2, cheBloqAbasBl2, nupDown1Bl2, nupDown2Bl2, nupDown3Bl2, nupDown4Bl2, btnMarcarTodasBl2, btnPassAntBl2, btnPassProxBl2);

        }


        private void comBlocoBl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chePerIncBl1.Checked = true;
            chePerAltBl1.Checked = true;
            chePerExcBl1.Checked = true;
            cheConAltBl1.Checked = true;
        }


        //Abre as pesquisas
        private void txtUsuarCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                PesUsuar.cs.PesUsuar_CALL Call = new PesUsuar.cs.PesUsuar_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabPermi;
                Call._Login_CryptDesc = _Login_UsuarioID_TabPermi;
                Call._WenCrypt = "PesUsuar21Wenemy3156!.350?°";
                Call.PesUsuar_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtUsuarCodigo.Text = Call._ResultPesquisaCALL;
                }

                txtUsuarCodigo.SelectAll();
            }
        }
        private void txtCodigoBl2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtDescicaoBl2.Text = string.Empty;

                PesProgr.cs.PesProgr_CALL Call = new PesProgr.cs.PesProgr_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabPermi;
                Call._Login_CryptDesc = _Login_UsuarioID_TabPermi;
                Call._WenCrypt = "PesProgr16Wenemy3156!.350?°";
                Call.PesProgr_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtCodigoBl2.Text = Call._ResultPesquisaCALL;
                }

                txtCodigoBl2.SelectAll();
            }
        }




        //APENAS NÚMEROS
        private void txtUsuarCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabPermi_MET MET = new TabPermi_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtCodigoBl2_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabPermi_MET MET = new TabPermi_MET();
            MET.MET_ApenasNúmeros(e);
        }




        #endregion


        #region PESQUISA

        //FAZ A PESQUISA E EXIBE A MSG CASO NÃO ENCONTRE RESULTADOS
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            txtQtSelectPES.Text = "000000";

            TabPermi_Pesquisa Pesq = new TabPermi_Pesquisa();
            //OBRIGA TER UM USUÁRIO OU UM PROGRAMA INFORMADO
            bool ApenasUm = Pesq.Pesc_ObrigUsuarOuProgr(txtPesUsuarCod, txtPesUsuarDesc, txtPesProgrCod, txtPesProgrDesc, TabControl);
            if (!ApenasUm) { } else { return; }

            //EXECUTA A PESQUISA
            Pesq.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, txtQtSelectPES, txtPesUsuarCod, txtPesUsuarDesc, txtPesProgrCod, txtPesProgrDesc, _Login_LojaID_TabPermi, btnPesquisar);
            if (txtQtSelectPES.Text == string.Empty) { txtQtSelectPES.Text = "000000"; }
            if (Convert.ToInt32(txtQtSelectPES.Text) == 0)
            {
                MessageBox.Show("Nenhuma informação encontrada. Verifique os filtros!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            txtPesUsuarCod.Select(); txtPesUsuarCod.SelectAll();
        }
        //VOLTA
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = Tp1;
        }
        //FECHA
        private void btnFechar2_Click(object sender, EventArgs e)
        {
            Close();
        }



        //Procura o usuário no TAB
        private void txtPesUsuarCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            
            {
                TabPermi_Pesquisa Pesq = new TabPermi_Pesquisa();
                Pesq.Pesc_SearchUSUAR(txtPesUsuarCod, txtPesUsuarDesc, grbFiltros);
            }
        }
        //Procura o programa no TAB
        private void txtPesProgrCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                TabPermi_Pesquisa Pesq = new TabPermi_Pesquisa();
                Pesq.Pesc_SearchPROGR(txtPesUsuarCod, txtPesUsuarDesc, grbFiltros, txtPesProgrCod, txtPesProgrDesc);
            }
        }


        //Apaga os campos de pesquisa no TextChanged
        private void txtPesUsuarCod_TextChanged(object sender, EventArgs e)
        {
            txtPesUsuarDesc.Text = string.Empty;
        }
        private void txtPesProgrCod_TextChanged(object sender, EventArgs e)
        {
            txtPesProgrDesc.Text = string.Empty;
        }


        //EXECUTA AS PESQUISAS PELOS EVENTOS DO FORMULARIO
        #region EXECUTA AS PESQUISAS PELAS PROPRIEDADES

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

            if (rabTOP.Checked == true)
            {
                TabPermi_Pesquisa Pesq = new TabPermi_Pesquisa();

                //OBRIGA TER UM USUÁRIO OU UM PROGRAMA INFORMADO
                bool ApenasUm = Pesq.Pesc_ObrigUsuarOuProgr(txtPesUsuarCod, txtPesUsuarDesc, txtPesProgrCod, txtPesProgrDesc, TabControl);
                if (!ApenasUm) { } else { return; }

                //EXECUTA A PESQUISA
                Pesq.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, txtQtSelectPES, txtPesUsuarCod, txtPesUsuarDesc, txtPesProgrCod, txtPesProgrDesc, _Login_LojaID_TabPermi, btnPesquisar);
            }
        }

        private void rabTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTodos.Checked == true)
            {
                TabPermi_Pesquisa Pesq = new TabPermi_Pesquisa();
                //OBRIGA TER UM USUÁRIO OU UM PROGRAMA INFORMADO
                bool ApenasUm = Pesq.Pesc_ObrigUsuarOuProgr(txtPesUsuarCod, txtPesUsuarDesc, txtPesProgrCod, txtPesProgrDesc, TabControl);
                if (!ApenasUm) { } else { return; }

                //EXECUTA A PESQUISA
                Pesq.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, txtQtSelectPES, txtPesUsuarCod, txtPesUsuarDesc, txtPesProgrCod, txtPesProgrDesc, _Login_LojaID_TabPermi, btnPesquisar);
            }
        }

        private void rabNumerico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabNumerico.Checked == true)
            {
                TabPermi_Pesquisa Pesq = new TabPermi_Pesquisa();
                //OBRIGA TER UM USUÁRIO OU UM PROGRAMA INFORMADO
                bool ApenasUm = Pesq.Pesc_ObrigUsuarOuProgr(txtPesUsuarCod, txtPesUsuarDesc, txtPesProgrCod, txtPesProgrDesc, TabControl);
                if (!ApenasUm) { } else { return; }

                //EXECUTA A PESQUISA
                Pesq.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, txtQtSelectPES, txtPesUsuarCod, txtPesUsuarDesc, txtPesProgrCod, txtPesProgrDesc, _Login_LojaID_TabPermi, btnPesquisar);
            }
        }

        private void rabAlfabetico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabAlfabetico.Checked == true)
            {
                TabPermi_Pesquisa Pesq = new TabPermi_Pesquisa();
                //OBRIGA TER UM USUÁRIO OU UM PROGRAMA INFORMADO
                bool ApenasUm = Pesq.Pesc_ObrigUsuarOuProgr(txtPesUsuarCod, txtPesUsuarDesc, txtPesProgrCod, txtPesProgrDesc, TabControl);
                if (!ApenasUm) { } else { return; }

                //EXECUTA A PESQUISA
                Pesq.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, txtQtSelectPES, txtPesUsuarCod, txtPesUsuarDesc, txtPesProgrCod, txtPesProgrDesc, _Login_LojaID_TabPermi, btnPesquisar);
            }
        }

        private void nupQtResultados_ValueChanged(object sender, EventArgs e)
        {
            if (rabTOP.Checked == true)
            {
                TabPermi_Pesquisa Pesq = new TabPermi_Pesquisa();
                //OBRIGA TER UM USUÁRIO OU UM PROGRAMA INFORMADO
                bool ApenasUm = Pesq.Pesc_ObrigUsuarOuProgr(txtPesUsuarCod, txtPesUsuarDesc, txtPesProgrCod, txtPesProgrDesc, TabControl);
                if (!ApenasUm) { } else { return; }

                //EXECUTA A PESQUISA
                Pesq.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, txtQtSelectPES, txtPesUsuarCod, txtPesUsuarDesc, txtPesProgrCod, txtPesProgrDesc, _Login_LojaID_TabPermi, btnPesquisar);
            }
        }

        #endregion


        //POPULA A QUANTIDADE DE RESULTADOS ENCONTRADOS
        private void Dgv_Pesquisa_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }
        private void Dgv_Pesquisa_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }


        //APAGA OS CAMPOS NO MOUSE DOWN
        private void txtPesUsuarCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtPesUsuarDesc.Text = string.Empty;
            txtPesUsuarCod.SelectAll();
        }
        private void txtPesProgrCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtPesProgrDesc.Text = string.Empty;
            txtPesProgrCod.SelectAll();
        }




        //APENAS NÚMEROS
        private void txtPesUsuarCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabPermi_MET MET = new TabPermi_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtPesProgrCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabPermi_MET MET = new TabPermi_MET();
            MET.MET_ApenasNúmeros(e);
        }


        //ABRE AS PESQUISAS
        private void txtPesUsuarCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtPesUsuarDesc.Text = string.Empty;

                PesUsuar.cs.PesUsuar_CALL Call = new PesUsuar.cs.PesUsuar_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabPermi;
                Call._Login_CryptDesc = _Login_UsuarioID_TabPermi;
                Call._WenCrypt = "PesUsuar21Wenemy3156!.350?°";
                Call.PesUsuar_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtPesUsuarCod.Text = Call._ResultPesquisaCALL;
                }

                txtPesUsuarCod.SelectAll();
            }
        }
        private void txtPesProgrCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtPesProgrDesc.Text = string.Empty;

                PesProgr.cs.PesProgr_CALL Call = new PesProgr.cs.PesProgr_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabPermi;
                Call._Login_CryptDesc = _Login_UsuarioID_TabPermi;
                Call._WenCrypt = "PesProgr16Wenemy3156!.350?°";
                Call.PesProgr_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtPesProgrCod.Text = Call._ResultPesquisaCALL;
                }

                txtPesProgrCod.SelectAll();
            }
        }

        #endregion


        #region HISTÓRICO

        //POPULA O DATAGRID
        private void btnHistorico_Click(object sender, EventArgs e)
        {
            TabPermi_Histórico HIST = new TabPermi_Histórico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, mtbData1His, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis);
        }
        //POPULA O DATAGRID
        private void comIDHis_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPermi_Histórico HIST = new TabPermi_Histórico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, comIDHis, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis);
        }



        //VERIFICA SE A DATA É VÁLIDA
        private void mtbData1His_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //VERIFICA SE A DATA É VALIDA
                TabPermi_Histórico HIS = new TabPermi_Histórico();
                bool DATA = HIS.HIS_VerificaDATA(mtbData1His, panUpAb4);
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
                TabPermi_Histórico HIS = new TabPermi_Histórico();
                bool DATA = HIS.HIS_VerificaDATA(mtbData2His, mtbData1His);
                if (!DATA) { } else { return; }

                //PREENCHER A PRIMEIRA DATA QUANDO A SEGUNDA ESTÁ PREENCHIDA
                bool OBRI = HIS.HIS_VerificaCamposObrig(mtbData1His, mtbData2His, panUpAb4);
                if (!OBRI) { } else { return; }

                //VERIFICA SE A SEGUNDA DATA É MENOR QUE A PRIMEIRA
                bool MENO = HIS.HIS_VerificaDataMenor(mtbData1His, mtbData2His, mtbData1His);
                if (!MENO) { } else { return; }
            }
        }



        //CHAMA A PESQUISA DE USUÁRIO
        private void txtUsuarioHis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                PesUsuar.cs.PesUsuar_CALL Call = new PesUsuar.cs.PesUsuar_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabPermi;
                Call._Login_CryptDesc = _Login_UsuarioID_TabPermi;
                Call._WenCrypt = "PesUsuar21Wenemy3156!.350?°";
                Call.PesUsuar_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtUsuarioHis.Text = Call._ResultPesquisaCALL;
                }

                txtUsuarioHis.SelectAll();
            }
        }


        //VERIFICA SE O USUÁRIO EXISTE NO TAB
        private void txtUsuarioHis_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (txtUsuarioHis.Text != string.Empty)
                {
                    TabPermi_Histórico HIST = new TabPermi_Histórico();
                    HIST.HIS_VerificaUsuarioEXISTE(txtUsuarioHis, mtbData2His, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis);
                }
                else
                {
 
                }
            }
        }

        //APENAS NÚMEROS
        private void txtUsuarioHis_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabPermi_MET MET = new TabPermi_MET();
            MET.MET_ApenasNúmeros(e);
        }

        //SelectAll dependendo do Evento
        #region Eventos de SelectAll
        private void txtUsuarioHis_MouseDown(object sender, MouseEventArgs e)
        {
            txtUsuarioHis.SelectAll();
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

        #endregion


        #region IMPRESSÃO

        //IMPRESSÃO GO
        private void btnPrint_Click(object sender, EventArgs e)
        {
            
        }


        //VOLTAR
        private void btnVoltarIMP_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = Tp1;
        }
        //FECHAR
        private void btnFecharIMP_Click(object sender, EventArgs e)
        {
            Close();
        }
        //ABRE O FOLDER
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


        //ABRE A PESQUISA
        private void txtImpProgrCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtImpProgrDesc.Text = string.Empty;

                PesProgr.cs.PesProgr_CALL Call = new PesProgr.cs.PesProgr_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabPermi;
                Call._Login_CryptDesc = _Login_UsuarioID_TabPermi;
                Call._WenCrypt = "PesProgr16Wenemy3156!.350?°";
                Call.PesProgr_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtImpProgrCod.Text = Call._ResultPesquisaCALL;
                }

                txtImpProgrCod.SelectAll();
            }
        }
        //ABRE A PESQUISA
        private void txtImpUsuarCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtImpUsuarDesc.Text = string.Empty;

                PesUsuar.cs.PesUsuar_CALL Call = new PesUsuar.cs.PesUsuar_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabPermi;
                Call._Login_CryptDesc = _Login_UsuarioID_TabPermi;
                Call._WenCrypt = "PesUsuar21Wenemy3156!.350?°";
                Call.PesUsuar_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtImpUsuarCod.Text = Call._ResultPesquisaCALL;
                }

                txtImpUsuarCod.SelectAll();
            }
        }



        //APENAS NÚMEROS
        private void txtImpUsuarCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabPermi_MET MET = new TabPermi_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtImpProgrCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabPermi_MET MET = new TabPermi_MET();
            MET.MET_ApenasNúmeros(e);
        }



        //PROCURA O USUÁIRO E O PROGRAMA NO TAB
        private void txtImpUsuarCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                TabPermi_Impressão Impre = new TabPermi_Impressão();
                Impre.Imp_SearchUSUAR(txtImpUsuarCod, txtImpUsuarDesc, grbInformacoesImpre);
            }
        }
        private void txtImpProgrCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                TabPermi_Impressão Impre = new TabPermi_Impressão();
                Impre.Imp_SearchPROGR(txtImpUsuarCod, txtImpUsuarDesc, grbInformacoesImpre, txtImpProgrCod, txtImpProgrDesc);
            }
        }


        //SELECT ALL
        private void txtImpUsuarCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtImpUsuarDesc.Text = string.Empty;
            txtImpUsuarCod.SelectAll();
        }
        private void txtImpProgrCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtImpProgrDesc.Text = string.Empty;
            txtImpProgrCod.SelectAll();
        }


        //APAGA NO TEXTCHANGE
        private void txtImpUsuarCod_TextChanged(object sender, EventArgs e)
        {
            txtImpUsuarDesc.Text = string.Empty;
        }
        private void txtImpProgrCod_TextChanged(object sender, EventArgs e)
        {
            txtImpProgrDesc.Text = string.Empty;
        }


        //Seleciona o botão da pasta
        private void txtCaminhoRel_MouseDown(object sender, MouseEventArgs e)
        {
            btnAbrirPastaIMP.Select();
        }


        #endregion












        








       


    }
}
