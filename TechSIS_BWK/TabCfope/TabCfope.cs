using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CfgComun;

namespace TabCfope
{
    internal partial class TabCfope : Form
    {
        public TabCfope()
        {
            InitializeComponent();
        }

        public string _Login_LojaID_TabCfope { get; set; }
        public string _Login_UsuarioID_TabCfope { get; set; }

        //LOAD DO FORMULÁRIO
        private void TabCfope_Load(object sender, EventArgs e)
        {
            //DESATIVA OS CAMPOS
            CamposDisable();

            //PREENCHE O USUÁRIO LOGADO
            txtUsuario.Text = _Login_UsuarioID_TabCfope.PadLeft(6, '0');

            TabCfope_Permi PERMI = new TabCfope_Permi();
            TabCfope_MET MET = new TabCfope_MET();
            TabCfope_FILTROS FILTROS = new TabCfope_FILTROS();


            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panDownAb1, panDownAb2, panDownAb3, txtQtSelectIMP, txtQtSelectPES, lblTitulo, lblTitulo };
            //MÉTODO DA CFG
            CfgComun_CLASS CFG = new CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, _Login_LojaID_TabCfope);



            //VERIFICA AS PERMISSÕES DOS BUTTONS
            PERMI.PER_Permiss_Buttons(btnIncluir, btnAlterar, btnExcluir, btnSeta1, btnSeta2, btnSeta3, btnSeta4, txtCodigo, _Login_UsuarioID_TabCfope);

            //VERIFICA SE É PARA GRAVAR OS FILTROS
            FILTROS.cheFILTROSChecked(cheFiltrosIMP, cheFiltrosPES, _Login_LojaID_TabCfope);

            #region CANCELA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged -= new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged -= new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged -= new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged -= new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged -= new EventHandler(nupQtResultados_ValueChanged);
            #endregion
            //CARREGA OS FILTROS
            FILTROS.CarregaFILTROS(cheFiltrosPES, cheFiltrosIMP, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, rabOrdemNumerica, rabOrdemAlfabetica, rabRPV, rabWORD, rabEXCEL, rabTXT, comImpLocalizacao, txtCaminhoRel);
            #region VOLTA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged += new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged += new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged += new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged += new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged += new EventHandler(nupQtResultados_ValueChanged);
            if (rabTOP.Checked == true) { nupQtResultados.Enabled = true; }
            #endregion


            //CAPTURA O CAMINHO DO ARQUIVO CASO NÃO ESTEJA NO XML DOS FILTROS
            txtCaminhoRel.Text = MET.MET_CapCaminhoSALV(txtCaminhoRel, _Login_LojaID_TabCfope);



            btnIncluir.Select();
        }

        //PROPRIEDADES AO SE TROCAR DE ABA
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
                comIDHis.SelectedIndexChanged -= new EventHandler(comIDHis_SelectedIndexChanged);

                txtMESTRE.Text = "HISTÓRICO";
                txtMESTRE.BackColor = Color.MediumSlateBlue;
                txtMESTRE.ForeColor = Color.White;

                //Popula Lixeira
                TabCfope_Histórico HIST = new TabCfope_Histórico();
                HIST.HIS_SelecionaDados(mtbData1His, mtbData2His, txtUsuarioHis, txtUsuario);



                HIST.HIS_PopularHISTORICO(txtUsuarioHis, this, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb4);
                mtbData1His.Select();
                SendKeys.Send("{HOME}");
                SendKeys.Send("+{END}");


                comIDHis.SelectedIndexChanged += new EventHandler(comIDHis_SelectedIndexChanged);
            }
            #endregion
        }

        //PERMISSÃO POR ABAS
        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            TabCfope_Permi PERMI = new TabCfope_Permi();
            if (e.TabPageIndex > 0)
            {
                PERMI.PER_VerificaPermi_TbCont(e, e.TabPageIndex, txtCodigo, _Login_UsuarioID_TabCfope);
            }
        }

        //TECLAS DE ATALHO DO FORMULÁRIO
        private void TabCfope_KeyDown(object sender, KeyEventArgs e)
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
                    btnVoltarImp.PerformClick();
                    btnVoltarPesq.PerformClick();
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
        private void TabCfope_FormClosing(object sender, FormClosingEventArgs e)
        {
            TabCfope_FILTROS FILTROS = new TabCfope_FILTROS();
            FILTROS.GravarFILTROS(cheFiltrosPES, cheFiltrosIMP, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, rabOrdemNumerica, rabOrdemAlfabetica, rabRPV, rabWORD, rabEXCEL, rabTXT, comImpLocalizacao, txtCaminhoRel);
        }




        #region FORMULÁRIO PRINCIPAL

        //METODOS DE LIMPEZA
        #region ZerarCampos, CamposEnable e CamposDisable
        public void ZerarCampos()
        {
            foreach (Control CTRL in panPrinAb1.Controls)
            {
                foreach (Control CTRL_2 in CTRL.Controls)
                {
                    if (CTRL_2.GetType() == typeof(TextBox) && CTRL_2.Name != "txtCodigo")
                    {
                        (CTRL_2 as TextBox).Text = string.Empty;
                    }
                    if (CTRL_2.GetType() == typeof(ComboBox))
                    {
                        (CTRL_2 as ComboBox).SelectedIndex = -1;
                    }
                    if (CTRL_2.GetType() == typeof(MaskedTextBox))
                    {
                        (CTRL_2 as MaskedTextBox).Text = string.Empty;
                    }
                    if (CTRL_2.GetType() == typeof(CheckBox))
                    {
                        (CTRL_2 as CheckBox).Checked = false;
                    }
                }
            }
        }
        public void CamposEnable()
        {
            foreach (Control CTRL in panPrinAb1.Controls)
            {
                foreach (Control CTRL_2 in CTRL.Controls)
                {
                    if (CTRL_2.GetType() == typeof(TextBox) && CTRL_2.Name != "txtIndDesc" && CTRL_2.Name != "txtComeDesc")
                    {
                        (CTRL_2 as TextBox).Enabled = true;
                    }
                    if (CTRL_2.GetType() == typeof(ComboBox))
                    {
                        (CTRL_2 as ComboBox).Enabled = true;
                    }
                    if (CTRL_2.GetType() == typeof(MaskedTextBox))
                    {
                        (CTRL_2 as MaskedTextBox).Enabled = true;
                    }
                    if (CTRL_2.GetType() == typeof(CheckBox))
                    {
                        (CTRL_2 as CheckBox).Enabled = true;
                    }
                }
            }
        }
        public void CamposDisable()
        {
            foreach (Control CTRL in panPrinAb1.Controls)
            {
                foreach (Control CTRL_2 in CTRL.Controls)
                {
                    if (CTRL_2.GetType() == typeof(TextBox) && CTRL_2.Name != "txtCodigo")
                    {
                        (CTRL_2 as TextBox).Enabled = false;
                    }
                    if (CTRL_2.GetType() == typeof(ComboBox))
                    {
                        (CTRL_2 as ComboBox).Enabled = false;
                    }
                    if (CTRL_2.GetType() == typeof(MaskedTextBox))
                    {
                        (CTRL_2 as MaskedTextBox).Enabled = false;
                    }
                    if (CTRL_2.GetType() == typeof(CheckBox))
                    {
                        (CTRL_2 as CheckBox).Enabled = false;
                    }
                }
            }
        }
        #endregion



        #region Buttons INCLUIR, ALTERAR E EXCLUIR
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            //APARENCIA DO BUTTON
            TabCfope_AppaButtons Appa = new TabCfope_AppaButtons();
            Appa._ButtonINC(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);
            //SELECIONA ULTIMO REGISTRO MAIS 1
            TabCfope_MET MET = new TabCfope_MET();
            MET.MET_SelecionaUltimoRegistroMaisUm(txtCodigo, btnGravar, _Login_LojaID_TabCfope);
        }
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //APARENCIA DO BUTTON
            TabCfope_AppaButtons Appa = new TabCfope_AppaButtons();
            Appa._ButtonALT(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //APARENCIA DO BUTTON
            TabCfope_AppaButtons Appa = new TabCfope_AppaButtons();
            Appa._ButtonEXC(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);
        }
        #endregion

        #region Setas 1,2,3 e 4
        private void btnSeta1_Click(object sender, EventArgs e)
        {
            //APARENCIA DO BUTTON
            TabCfope_AppaButtons Appa = new TabCfope_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            //BUSCA OS RESULTADOS
            TabCfope_ExecSETAS EXEC = new TabCfope_ExecSETAS();
            EXEC.ExecSETAS("1", txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabCfope, txtIndCod, txtIndDesc, txtComeCod, txtComeDesc, comLocalizacao);
        }
        private void btnSeta2_Click(object sender, EventArgs e)
        {
            //APARENCIA DO BUTTON
            TabCfope_AppaButtons Appa = new TabCfope_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            //BUSCA OS RESULTADOS
            TabCfope_ExecSETAS EXEC = new TabCfope_ExecSETAS();
            EXEC.ExecSETAS("2", txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabCfope, txtIndCod, txtIndDesc, txtComeCod, txtComeDesc, comLocalizacao);
        }
        private void btnSeta3_Click(object sender, EventArgs e)
        {
            //APARENCIA DO BUTTON
            TabCfope_AppaButtons Appa = new TabCfope_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            //BUSCA OS RESULTADOS
            TabCfope_ExecSETAS EXEC = new TabCfope_ExecSETAS();
            EXEC.ExecSETAS("3", txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabCfope, txtIndCod, txtIndDesc, txtComeCod, txtComeDesc, comLocalizacao);
        }
        private void btnSeta4_Click(object sender, EventArgs e)
        {
            //APARENCIA DO BUTTON
            TabCfope_AppaButtons Appa = new TabCfope_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            //BUSCA OS RESULTADOS
            TabCfope_ExecSETAS EXEC = new TabCfope_ExecSETAS();
            EXEC.ExecSETAS("4", txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabCfope, txtIndCod, txtIndDesc, txtComeCod, txtComeDesc, comLocalizacao);
        }
        #endregion

        #region Buttons Gravar, Fechar, Zerar, Ajuda

        //ABRE A VIDEO AULA
        private void btnAjuda_Click(object sender, EventArgs e)
        {
            TabCfope_FILTROS FILTROS = new TabCfope_FILTROS();
            FILTROS.LinkAjuda();
        }
        //BTN GRAVAR
        private void btnGravar_Click(object sender, EventArgs e)
        {
            //CAMPOS OBRIGATÓRIOS
            TabCfope_CamposObrig Obrig = new TabCfope_CamposObrig();
            bool Obg = Obrig.CamposObrig(txtMESTRE, txtCodigo, txtDescri, txtIndCod, txtIndDesc, txtComeCod, txtComeDesc, comLocalizacao);
            if (!Obg) { } else { return; }



            TabCfope_btnGravar GRAVAR = new TabCfope_btnGravar();
            GRAVAR.GravarINC(txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabCfope, txtIndCod, txtIndDesc, txtComeCod, txtComeDesc, comLocalizacao, txtUsuario);
            GRAVAR.GravarALT(txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabCfope, txtIndCod, txtIndDesc, txtComeCod, txtComeDesc, comLocalizacao, txtUsuario);
            GRAVAR.GravarEXC(txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, txtUsuario, MOTIVO_EXC, _Login_LojaID_TabCfope);
        }
        //ZERA OS CAMPOS
        private void btnZerar_Click(object sender, EventArgs e)
        {
            ZerarCampos();
            //DEFINE A APARENCIA DO BUTTON
            TabCfope_AppaButtons Appa = new TabCfope_AppaButtons();
            Appa._ButtonZER(txtMESTRE, txtCodigo, btnIncluir, btnGravar, CamposDisable, TabControl, Tp1);
        }
        //FECHA O FOMR
        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        //SELECIONA NO TAB
        public string MOTIVO_EXC { get; set; }
        private void txtCodigo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                TabCfope_MET MET = new TabCfope_MET();
                MET.MET_SelecionaCodigoTAB(txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabCfope, txtIndCod, txtIndDesc, txtComeCod, txtComeDesc, comLocalizacao);


                //TRATAMENTO PARA EXCLUSÃO
                #region TRATAMENTO PARA EXCLUSÃO
                if (txtMESTRE.Text == "EXCLUIR" && txtDescri.Text != string.Empty)
                {
                    FromEx Ex = new FromEx();
                    Ex.ShowDialog();
                    MOTIVO_EXC = Ex._MotivoExclusão;
                    if (MOTIVO_EXC != string.Empty)
                    {
                        btnGravar.PerformClick();
                    }
                    btnGravar.Enabled = false;
                    ZerarCampos();
                    CamposDisable();
                    panUpAb1.Focus();
                    txtCodigo.SelectAll();
                }
                #endregion
            }
        }

        //SELECIONA O CFOP NO TAB
        private void txtIndCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtIndCod.Text != string.Empty)
            {
                if (Convert.ToInt32(txtIndCod.Text) == 0)
                {
                    txtIndCod.Text = string.Empty;
                    txtIndDesc.Text = string.Empty;
                }
                else
                {
                    TabCfope_MET MET = new TabCfope_MET();
                    MET.MET_SelectionaCFOPTab(txtIndCod, txtIndDesc, txtDescri);
                }
            }
            if (e.KeyCode == Keys.Tab && txtIndCod.Text == string.Empty)
            {
                txtIndCod.Text = string.Empty;
                txtIndDesc.Text = string.Empty;
            }
        }
        //SELECIONA O CFOP NO TAB
        private void txtComeCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtComeCod.Text != string.Empty)
            {
                if (Convert.ToInt32(txtComeCod.Text) == 0)
                {
                    txtComeCod.Text = string.Empty;
                    txtComeDesc.Text = string.Empty;
                }
                else
                {
                    TabCfope_MET MET = new TabCfope_MET();
                    MET.MET_SelectionaCFOPTab(txtComeCod, txtComeDesc, txtIndCod);
                }
            }
            if (e.KeyCode == Keys.Tab && txtComeCod.Text == string.Empty)
            {
                txtComeCod.Text = string.Empty;
                txtComeDesc.Text = string.Empty;
            }
        }


        //SELECT ALL NO CLICK
        private void txtCodigo_MouseDown(object sender, MouseEventArgs e)
        {
            txtCodigo.SelectAll();
            ZerarCampos();
            CamposDisable();
            btnGravar.Enabled = false;
        }
        private void txtIndCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtIndDesc.Text = string.Empty;
            txtIndCod.SelectAll();
        }
        private void txtComeCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtComeDesc.Text = string.Empty;
            txtComeCod.SelectAll();
        }
        private void txtCodigo_Click(object sender, EventArgs e)
        {
            txtCodigo.SelectAll();
        }


        //TEXT CHANGE
        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            ZerarCampos();
            CamposDisable();
            btnGravar.Enabled = false;
        }
        private void txtComeCod_TextChanged(object sender, EventArgs e)
        {
            txtComeDesc.Text = string.Empty;
        }
        private void txtIndCod_TextChanged(object sender, EventArgs e)
        {
            txtIndDesc.Text = string.Empty;
        }


        //APENAS NÚMEROS
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabCfope_MET MET = new TabCfope_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtComeCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabCfope_MET MET = new TabCfope_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtIndCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabCfope_MET MET = new TabCfope_MET();
            MET.MET_ApenasNúmeros(e);
        }

        //OBRIGAÇÃO NO TAB
        private void txtDescri_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtDescri.Text == string.Empty)
            {
                MessageBox.Show("Campo (Descrição) deve ser preenchido", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodigo.Focus(); txtDescri.SelectAll();
            }
        }


        //ABRE AS PESQUISAS DE CFOP
        #region ABRE A PESQUISA DE CFOP
        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(4, '0');

                PesCfope.cs.PesCfope_CALL Call = new PesCfope.cs.PesCfope_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabCfope;
                Call._Login_CryptDesc = _Login_UsuarioID_TabCfope;
                Call._WenCrypt = "PesCfope3Wenemy3156!.350?°";
                Call.PesCfope_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtCodigo.Text = Call._ResultPesquisaCALL;
                }

                txtCodigo.SelectAll();
            }
        }
        private void txtComeCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(4, '0');

                PesCfope.cs.PesCfope_CALL Call = new PesCfope.cs.PesCfope_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabCfope;
                Call._Login_CryptDesc = _Login_UsuarioID_TabCfope;
                Call._WenCrypt = "PesCfope3Wenemy3156!.350?°";
                Call.PesCfope_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtComeCod.Text = Call._ResultPesquisaCALL;
                }

                txtComeCod.SelectAll();
            }
        }
        private void txtIndCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(4, '0');

                PesCfope.cs.PesCfope_CALL Call = new PesCfope.cs.PesCfope_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabCfope;
                Call._Login_CryptDesc = _Login_UsuarioID_TabCfope;
                Call._WenCrypt = "PesCfope3Wenemy3156!.350?°";
                Call.PesCfope_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtIndCod.Text = Call._ResultPesquisaCALL;
                }

                txtIndCod.SelectAll();
            }
        }
        #endregion



        #endregion

        #region PESQUISA

        #region Pesquisar, Voltar e Fechar
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabCfope_Pesquisa PESQ = new TabCfope_Pesquisa();
            PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabCfope, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
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


        //ATUALIZA A QT DE LINHAS ENCONTRADAS NA PESQUISA
        private void Dgv_Pesquisa_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }
        private void Dgv_Pesquisa_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }



        //SELECT ALL NO CLICK
        private void txtPesDescri_MouseDown(object sender, MouseEventArgs e)
        {
            txtPesDescri.SelectAll();
        }

        //EXIBE NO FORMULARIO
        private void Dgv_Pesquisa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string VariavelPesquisa = Dgv_Pesquisa.CurrentRow.Cells[0].Value.ToString();
                TabControl.SelectedTab = Tp1;
                TabCfope_AppaButtons Apa = new TabCfope_AppaButtons();
                Apa._ButtonZER(txtMESTRE, txtCodigo, btnIncluir, btnGravar, CamposDisable, TabControl, Tp1);
                txtCodigo.Text = VariavelPesquisa;
                txtCodigo.Select(); txtCodigo.SelectAll();
                SendKeys.Send("{TAB}");
            }
        }

        //EXECUTA A PESQUISA PELAS DIVERSAS PROPRIEDADES
        #region EXECUTA A PESQUISA
        private void rabNumerico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabNumerico.Checked == true)
            {
                //EXECUTA A PESQUISA
                TabCfope_Pesquisa PESQ = new TabCfope_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabCfope, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void rabAlfabetico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabAlfabetico.Checked == true)
            {
                //EXECUTA A PESQUISA
                TabCfope_Pesquisa PESQ = new TabCfope_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabCfope, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void rabTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTodos.Checked == true)
            {
                //EXECUTA A PESQUISA
                TabCfope_Pesquisa PESQ = new TabCfope_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabCfope, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void rabTOP_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTOP.Checked == true)
            {
                nupQtResultados.Enabled = true;
                //EXECUTA A PESQUISA
                TabCfope_Pesquisa PESQ = new TabCfope_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabCfope, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
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
                TabCfope_Pesquisa PESQ = new TabCfope_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabCfope, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void txtPesDescri_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabCfope_Pesquisa PESQ = new TabCfope_Pesquisa();
            PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabCfope, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
        }
        #endregion

        #endregion

        #region IMPRESSÃO

        #region Buttons Print, Fechar e Voltar

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
        private void btnFecharIMP_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnVoltarIMP_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = Tp1;
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

        //ZERA O TXTQT IMP
        #region txtQtSelectIMP.Text = string.Empty;
        private void rabRPV_CheckedChanged(object sender, EventArgs e)
        {
            
            txtQtSelectIMP.Text = string.Empty;
        }
        private void rabWORD_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
        }
        private void rabEXCEL_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
        }
        private void rabTXT_CheckedChanged(object sender, EventArgs e)
        {
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
        private void comImpEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
        }

        #endregion

        #endregion

        #region HISTÓRICO

        //VERIFICA SE A DATA É VÁLIDA
        private void mtbData1His_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //VERIFICA SE A DATA É VALIDA
                TabCfope_Histórico HIS = new TabCfope_Histórico();
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
                TabCfope_Histórico HIS = new TabCfope_Histórico();
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



        //FAZ A PESQUISA PELO SELECTED INDEX
        private void comIDHis_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Popula Histório
            TabCfope_Histórico HIST = new TabCfope_Histórico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, comIDHis, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb4);
        }
        //FAZ A PESQUISA PELO CLICK DO BOTÃO
        private void btnHistorico_Click(object sender, EventArgs e)
        {
            //Popula Histório
            TabCfope_Histórico HIST = new TabCfope_Histórico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, comIDHis, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb4);
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
            txtUsuarioHis.SelectAll();
        }
        #endregion



        //FAZ A PESQUISA DO USUÁRIO
        private void txtUsuarioHis_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                TabCfope_Histórico HIST = new TabCfope_Histórico();
                HIST.HIS_VerificaUsuarioEXISTE(txtUsuarioHis, mtbData2His, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb4);
            }
        }
        //APENAS NÚMEROS
        private void txtUsuarioHis_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabCfope_MET MET = new TabCfope_MET();
            MET.MET_ApenasNúmeros(e);
        }
        //ABRE A PESQUISA DO USUÁRIO
        private void txtUsuarioHis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                PesUsuar.cs.PesUsuar_CALL Call = new PesUsuar.cs.PesUsuar_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabCfope;
                Call._Login_CryptDesc = _Login_UsuarioID_TabCfope;
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
