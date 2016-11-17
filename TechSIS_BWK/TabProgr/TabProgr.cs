using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TabProgr
{
    internal partial class TabProgr : Form
    {
        public TabProgr()
        {
            InitializeComponent();
        }


        public string _Login_UsuarioID_TabProgr { get; set; }
        public string _Login_LojaID_TabProgr { get; set; }



        //EVENTO LOAD DO FORM
        private void TabProgr_Load(object sender, EventArgs e)
        {
            //Popula o txtUsuario com o código do usuário Logado
            txtUsuario.Text = _Login_UsuarioID_TabProgr.PadLeft(6, '0');

            //Verifica o checkbox dos filtros
            TabProgr_FILTROS FILTROS = new TabProgr_FILTROS();
            FILTROS.cheFILTROSChecked(cheFiltrosPES, cheFiltrosIMP, _Login_LojaID_TabProgr);

            #region CANCELA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged -= new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged -= new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged -= new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged -= new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged -= new EventHandler(nupQtResultados_ValueChanged);
            comStatusPES.SelectedIndexChanged -= new EventHandler(comStatusPES_SelectedIndexChanged);
            #endregion
            //Carrega os filtros
            //FILTROS.CarregaFILTROS(cheFiltrosPES, cheFiltrosIMP, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comStatusPES, rabOrdemNumerica, rabOrdemAlfabetica, rabReport, rabWORD, rabExcel, rabTXT, comSituacaoIMP, txtCaminhoRel, cheVoltarLix);
            #region VOLTA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged += new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged += new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged += new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged += new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged += new EventHandler(nupQtResultados_ValueChanged);
            comStatusPES.SelectedIndexChanged += new EventHandler(comStatusPES_SelectedIndexChanged);
            if (rabTOP.Checked == true) { nupQtResultados.Enabled = true; }
            #endregion

            //Verifica a permissão do usuário nos Buttons
            TabProgr_Permi Permi = new TabProgr_Permi();
            Permi.Permiss_Buttons(btnIncluir, btnAlterar, btnExcluir, btnSeta1, btnSeta2, btnSeta3, btnSeta4, txtCodigo, _Login_UsuarioID_TabProgr);

            //Seleciona a quantidade de programas cadastrados em TabProgr
            TabProgr_MET MET = new TabProgr_MET();
            MET.MET_NúmeroDeProgramas(txtTotalProgramas);

            //Seleciona o caminho de salvar os relatorios
            //txtCaminhoRel.Text = MET.MET_CapCaminhoSALV(txtCaminhoRel, _Login_LojaID_TabProgr);

            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panDownAb1, panDownAb2, panDownAb3, panDownAb4, panDownAb5, txtQtSelectIMP, txtQtSelectPES };
            //MÉTODO DA CFG
            CfgComun.CfgComun_CLASS CFG = new CfgComun.CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, _Login_LojaID_TabProgr);

            btnIncluir.Select();
        }


        //GRAVA OS FILTROS
        private void TabProgr_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Grava os filtros
            TabProgr_FILTROS FILTROS = new TabProgr_FILTROS();
            //FILTROS.GravarFILTROS(cheFiltrosPES, cheFiltrosIMP, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comStatusPES, rabOrdemNumerica, rabOrdemAlfabetica, rabReport, rabWORD, rabExcel, rabTXT, comSituacaoIMP, txtCaminhoRel, cheVoltarLix);
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
                txtDescriPES.Select();
                txtDescriPES.Text = string.Empty;
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
                //BT_AbaImpressão_IMPRESSÃO.Select();
            }
            #endregion
            #region Prop. ABA 4
            if (TabControl.SelectedTab == Tp4)
            {
                txtMESTRE.Text = "LIXEIRA";
                txtMESTRE.BackColor = Color.Coral;
                txtMESTRE.ForeColor = Color.Yellow;
                //Popula Lixeira
                TabProgr_Lixeira Lix = new TabProgr_Lixeira();
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
                TabProgr_Histórico HIST = new TabProgr_Histórico();
                HIST.HIS_SelecionaDados(mtbData1His, mtbData2His, txtUsuarioHis, txtUsuario);

                

                HIST.HIS_PopularHISTORICO(txtUsuarioHis, this, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
                mtbData1His.Select();
                SendKeys.Send("{HOME}");
                SendKeys.Send("+{END}");


                comIDHis.SelectedIndexChanged += new EventHandler(comIDHis_SelectedIndexChanged);
            }
            #endregion
        }


        //TECLAS DE ATALHOS DO FORMULÁRIO
        private void TabProgr_KeyDown(object sender, KeyEventArgs e)
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
                TabProgr_Permi Permi = new TabProgr_Permi();
                Permi.VerificaPermi_TbCont(e, e.TabPageIndex, txtCodigo, _Login_UsuarioID_TabProgr);
            }
        }




        #region FORM PRINCIPAL
        #region ZerarCampos, CamposEnable, CamposDisable
        public void ZerarCampos()
        {
            #region grbControls
            foreach (Control ctrl in grbControls.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox) && ctrl.Name != "txtCodigo" && ctrl.Name != "txtUsuario" && ctrl.Name != "txtTotalProgramas")
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
            #region grbControls
            foreach (Control ctrl in grbControls.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox) && ctrl.Name != "txtUsuario" && ctrl.Name != "txtTotalProgramas" && ctrl.Name != "txtDataCadastro")
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
            #region grbControls
            foreach (Control ctrl in grbControls.Controls)
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
        }
        #endregion

        #region Buttons INC, ALT e EXC
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            //Aplica as propriedades ao clicar no button
            TabProgr_AppaButtons Buttons = new TabProgr_AppaButtons();
            Buttons._ButtonINC(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //Aplica as propriedades ao clicar no button
            TabProgr_AppaButtons Buttons = new TabProgr_AppaButtons();
            Buttons._ButtonALT(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //Aplica as propriedades ao clicar no button
            TabProgr_AppaButtons Buttons = new TabProgr_AppaButtons();
            Buttons._ButtonEXC(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);
        }
        #endregion
        #region Buttons SETAS
        private void btnSeta1_Click(object sender, EventArgs e)
        {
            //Aplica as propriedades ao clicar no button
            TabProgr_AppaButtons Buttons = new TabProgr_AppaButtons();
            Buttons._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);

            //Popula as informações no clicar das setas
            TabProgr_ExecSETA SET = new TabProgr_ExecSETA();
            SET.ExexSETAS("1", txtCodigo, txtDescricao, txtDataCadastro, comStatus, comModulo, btnGravar, CamposDisable);
        }

        private void btnSeta2_Click(object sender, EventArgs e)
        {
            //Aplica as propriedades ao clicar no button
            TabProgr_AppaButtons Buttons = new TabProgr_AppaButtons();
            Buttons._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);

            //Popula as informações no clicar das setas
            TabProgr_ExecSETA SET = new TabProgr_ExecSETA();
            SET.ExexSETAS("2", txtCodigo, txtDescricao, txtDataCadastro, comStatus, comModulo, btnGravar, CamposDisable);
        }

        private void btnSeta3_Click(object sender, EventArgs e)
        {
            //Aplica as propriedades ao clicar no button
            TabProgr_AppaButtons Buttons = new TabProgr_AppaButtons();
            Buttons._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);

            //Popula as informações no clicar das setas
            TabProgr_ExecSETA SET = new TabProgr_ExecSETA();
            SET.ExexSETAS("3", txtCodigo, txtDescricao, txtDataCadastro, comStatus, comModulo, btnGravar, CamposDisable);
        }

        private void btnSeta4_Click(object sender, EventArgs e)
        {
            //Aplica as propriedades ao clicar no button
            TabProgr_AppaButtons Buttons = new TabProgr_AppaButtons();
            Buttons._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);

            //Popula as informações no clicar das setas
            TabProgr_ExecSETA SET = new TabProgr_ExecSETA();
            SET.ExexSETAS("4", txtCodigo, txtDescricao, txtDataCadastro, comStatus, comModulo, btnGravar, CamposDisable);
        }
        #endregion
        #region Buttons Ajuda, Gravar, Zerar e Fechar
        private void btnAjuda_Click(object sender, EventArgs e)
        {
            //Abre a video aula de ajuda
            TabProgr_FILTROS FILTROS = new TabProgr_FILTROS();
            FILTROS.LinkAjuda();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            //Verifica os campos obrigatórios antes de gravar
            TabProgr_CamposObrig Obrig = new TabProgr_CamposObrig();
            bool Complete = Obrig.CamposObrig(txtMESTRE, txtCodigo, txtDescricao, comStatus, comModulo, txtUsuario);
            if (!Complete) { } else { return; }

            //Grava as informações dependendo do TXT_MESTRE
            TabProgr_btnGRAVAR Gravar = new TabProgr_btnGRAVAR();
            Gravar.GravarINC(txtMESTRE, txtCodigo, txtDescricao, comStatus, comModulo, txtUsuario, txtTotalProgramas, ZerarCampos, btnGravar, CamposDisable);
            Gravar.GravarALT(txtMESTRE, txtCodigo, txtDescricao, comStatus, comModulo, txtUsuario, txtTotalProgramas, ZerarCampos, btnGravar, CamposDisable);
            Gravar.GravarEXC(txtMESTRE, txtCodigo, txtDescricao, comStatus, comModulo, txtUsuario, txtTotalProgramas, ZerarCampos, btnGravar, CamposDisable, MotivoExc, grbControls);
        }

        private void btnZerar_Click(object sender, EventArgs e)
        {
            //Zera as informações do FORM
            ZerarCampos();
            TabProgr_AppaButtons Appa = new TabProgr_AppaButtons();
            Appa._ButtonZER(txtMESTRE, txtCodigo, btnIncluir, btnGravar, CamposDisable, TabControl, Tp1);
            txtCodigo.Text = string.Empty; btnIncluir.Select();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion


        //Apenas Números
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Define que so pode receber NÚMEROS
            TabProgr_MET MET = new TabProgr_MET();
            MET.MET_ApenasNúmeros(e);
        }

        //DESABILITA PARA EVITAR BOBERRA
        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            btnGravar.Enabled = false;
        }

        //Tab no campo Código
        public string MotivoExc { get; set; }
        private void txtCodigo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                TabProgr_MET MET = new TabProgr_MET();

                //Verifica se o código do programa é legal
                bool Valido = MET.MET_CodigoLoyou(txtCodigo, grbControls);
                if (!Valido) { } else { return; }

                //Executa o metodo de popular os controles com TAB
                MET.MET_SelecionaDigitarTAB(txtMESTRE, txtCodigo, txtDescricao, comStatus, comModulo, txtDataCadastro, btnGravar, ZerarCampos, CamposEnable, CamposDisable, grbControls);

                //Faz o tratamento quando chama o form de exlusão
                #region TRATAMENTO PARA EXCLUSÃO
                if (txtMESTRE.Text == "EXCLUIR" && txtDescricao.Text != string.Empty)
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
                    grbControls.Focus();
                    txtCodigo.SelectAll();
                }
                #endregion
            }
        }


        //Zera o formulário para o usuário não fazer cagada
        private void txtCodigo_MouseDown(object sender, MouseEventArgs e)
        {
            btnGravar.Enabled = false;
            ZerarCampos(); CamposDisable();

            txtCodigo.SelectAll();
        }


        //CHAMA A PESQUISA
        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                PesProgr.cs.PesProgr_CALL Call = new PesProgr.cs.PesProgr_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabProgr;
                Call._Login_CryptDesc = _Login_UsuarioID_TabProgr;
                Call._WenCrypt = "PesProgr16Wenemy3156!.350?°";
                Call.PesProgr_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtCodigo.Text = Call._ResultPesquisaCALL;
                }

                txtCodigo.SelectAll();
            }
        }
        #endregion


        #region PESQUISA
        //SELECIONA A QUANTIDADE DE REGISTROS ENCONTRADOS NO SELECT
        private void Dgv_Pesquisa_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }
        private void Dgv_Pesquisa_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }


        //EXECUTA A PESQUISA DE TODAS AS MANEIRAS

        //EXECUTA NO CLICK DO BOTÃO PESQUISAR
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabProgr_Pesquisa Pesqu = new TabProgr_Pesquisa();
            bool BoolPesqu = Pesqu.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comStatusPES, txtDescriPES, txtQtSelectPES, _Login_LojaID_TabProgr);
            if (!BoolPesqu) { } else { return; }
            if (txtQtSelectPES.Text == string.Empty) { txtQtSelectPES.Text = "000000"; }
            if (Convert.ToInt32(txtQtSelectPES.Text) == 0)
            {
                MessageBox.Show("Nenhuma informação encontrada. Verifique os filtros!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            txtDescriPES.Select(); txtDescriPES.SelectAll();
        }
        //EXECUTA NO TEXTCHANGED DO TXT DE DESCRIÇÃO
        private void txtDescriPES_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabProgr_Pesquisa Pesqu = new TabProgr_Pesquisa();
            bool BoolPesqu = Pesqu.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comStatusPES, txtDescriPES, txtQtSelectPES, _Login_LojaID_TabProgr);
            if (!BoolPesqu) { } else { return; }
        }
        //EXECUTA NA TROCA DE STATUS
        private void comStatusPES_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabProgr_Pesquisa Pesqu = new TabProgr_Pesquisa();
            bool BoolPesqu = Pesqu.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comStatusPES, txtDescriPES, txtQtSelectPES, _Login_LojaID_TabProgr);
            if (!BoolPesqu) { } else { return; }
            txtDescriPES.Select(); txtDescriPES.SelectAll();
        }
        //EXECUTA SELECIONANDO NUMERICO
        private void rabNumerico_CheckedChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabProgr_Pesquisa Pesqu = new TabProgr_Pesquisa();
            bool BoolPesqu = Pesqu.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comStatusPES, txtDescriPES, txtQtSelectPES, _Login_LojaID_TabProgr);
            if (!BoolPesqu) { } else { return; }
            txtDescriPES.Select(); txtDescriPES.SelectAll();
        }
        //EXECUTA SELECIONANDO ALFABETICO
        private void rabAlfabetico_CheckedChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabProgr_Pesquisa Pesqu = new TabProgr_Pesquisa();
            bool BoolPesqu = Pesqu.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comStatusPES, txtDescriPES, txtQtSelectPES, _Login_LojaID_TabProgr);
            if (!BoolPesqu) { } else { return; }
            txtDescriPES.Select(); txtDescriPES.SelectAll();
        }
        //EXECUTA ACHANDO TODOS OS RESULTADOS
        private void rabTodos_CheckedChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabProgr_Pesquisa Pesqu = new TabProgr_Pesquisa();
            bool BoolPesqu = Pesqu.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comStatusPES, txtDescriPES, txtQtSelectPES, _Login_LojaID_TabProgr);
            if (!BoolPesqu) { } else { return; }
            txtDescriPES.Select(); txtDescriPES.SelectAll();
        }
        //EXECUTA ACHANDO RESULTADOS POR TOP
        private void rabTOP_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTOP.Checked == true)
            {
                nupQtResultados.Enabled = true;
            }

            //EXECUTA A PESQUISA
            TabProgr_Pesquisa Pesqu = new TabProgr_Pesquisa();
            bool BoolPesqu = Pesqu.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comStatusPES, txtDescriPES, txtQtSelectPES, _Login_LojaID_TabProgr);
            if (!BoolPesqu) { } else { return; }
            txtDescriPES.Select(); txtDescriPES.SelectAll();
        }
        //EXECUTA AO TROCAR A QUANTIDADE DE TOP
        private void nupQtResultados_ValueChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabProgr_Pesquisa Pesqu = new TabProgr_Pesquisa();
            bool BoolPesqu = Pesqu.Pesc_EXECUTAR(Dgv_Pesquisa, rabTodos, rabTOP, nupQtResultados, rabAlfabetico, rabNumerico, comStatusPES, txtDescriPES, txtQtSelectPES, _Login_LojaID_TabProgr);
            if (!BoolPesqu) { } else { return; }
            txtDescriPES.Select(); txtDescriPES.SelectAll();
        }



        //SELECIONA O ITEM DESEJADO DA PESQUISA NO CLICK DA CELULA
        private void Dgv_Pesquisa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string VariavelPesquisa = Dgv_Pesquisa.CurrentRow.Cells[0].Value.ToString();
                TabControl.SelectedTab = Tp1;
                TabProgr_AppaButtons Apa = new TabProgr_AppaButtons();
                Apa._ButtonZER(txtMESTRE, txtCodigo, btnIncluir, btnGravar, CamposDisable, TabControl, Tp1);
                txtCodigo.Text = VariavelPesquisa;
                txtCodigo.Select(); txtCodigo.SelectAll();
                SendKeys.Send("{TAB}");
            }
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


        #region LIXEIRA

        //RESTAURA O ITEM DA LIXEIRA
        private void Dgv_Lixeira_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                //RESTAURA O ITEM DA LIXEIRA
                TabProgr_Lixeira Lixeira = new TabProgr_Lixeira();
                Lixeira.Lix_RESTAURAR(Dgv_Lixeira, cheVoltarLix, TabControl, Tp1, txtUsuario, btnGravar, txtMESTRE, CamposDisable, txtCodigo, btnIncluir);
            }
        }

        #endregion


        #region HISTÓRICO

        //FAZ A PESQUISA PELO SELECTED INDEX
        private void comIDHis_SelectedIndexChanged(object sender, EventArgs e)
        {
            //POPULA O HISTÓRICO A PARTIR DOS FILTROS
            TabProgr_Histórico HIST = new TabProgr_Histórico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, this, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
        }
        //FAZ A PESQUISA PELO CLICK DO BOTÃO
        private void btnHistorico_Click(object sender, EventArgs e)
        {
            //POPULA O HISTÓRICO A PARTIR DOS FILTROS
            TabProgr_Histórico HIST = new TabProgr_Histórico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, this, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
        }



        //ABRE A PESQUISA DO USUÁRIO
        private void txtUsuarioHis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                PesUsuar.cs.PesUsuar_CALL Call = new PesUsuar.cs.PesUsuar_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabProgr;
                Call._Login_CryptDesc = _Login_UsuarioID_TabProgr;
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
                TabProgr_Histórico HIS = new TabProgr_Histórico();
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
                TabProgr_Histórico HIS = new TabProgr_Histórico();
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
                    TabProgr_Histórico HIS = new TabProgr_Histórico();
                    HIS.HIS_VerificaUsuarioEXISTE(txtUsuarioHis, mtbData2His, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
                }
            }
        }


        //Todos os eventos que dão SelectAll em um controle
        #region EVENTOS SelecALL
        private void txtUsuarioHis_Click(object sender, EventArgs e)
        {
            txtUsuarioHis.SelectAll();
        }
        private void mtbData2His_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbData1His_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbData2His_Click(object sender, EventArgs e)
        {
            mtbData2His.SelectAll();
        }

        private void mtbData1His_Click(object sender, EventArgs e)
        {
            mtbData1His.SelectAll();
        }
        #endregion


        //APENAS NÚMEROS
        private void txtUsuarioHis_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabProgr_MET MET = new TabProgr_MET();
            MET.MET_ApenasNúmeros(e);
        }



        #endregion


        #region IMPRESSÃO

        #region Buttons PRINT, VOLTAR e FECHAR
        //CHAMA OS FORMULÁRIOS DE IMPRESSÃO
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
            //txtCaminhoRel.Text = string.Empty;
            //Browser.SelectedPath = string.Empty;

            //DialogResult FOLDER = Browser.ShowDialog();


            //if (Browser.SelectedPath.Length > 3)
            //{
            //    txtCaminhoRel.Text = Browser.SelectedPath + @"\";
            //}
            //else
            //{
            //    txtCaminhoRel.Text = Browser.SelectedPath;
            //}
        }

        //DA O FOCO AO BOTÃO BROWSER AO SE CLICAR NO TXT DO CAMINHO DE SALVAMENTO
        private void txtCaminhoRel_MouseDown(object sender, MouseEventArgs e)
        {
            //btnAbrirPastaIMP.Select();
        }



        //APLICA AS PROPRIEDADES NO CHECKED CHANGE
        private void rabRPV_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;

            //if (rabReport.Checked == true)
            //{
            //    lblNomedoArquivo.Text = "Rpv_TabProgr.rpv";
            //    lblMsg1.Text = "CERTIFIQUE-SE DE";
            //    lblMsg2.Text = "QUE AS .OCX";
            //    lblMsg3.Text = "ESTÃO REGISTRADAS";
            //    lblMsg4.Text = "E QUE A IMAGEM";
            //    lblMsg5.Text = "ESTÁ NA PASTA";
            //}
        }
        private void rabWORD_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;

            //if (rabWORD.Checked == true)
            //{
            //    lblNomedoArquivo.Text = "WORD_TabProgr.docx";
            //    lblMsg1.Text = "CERTIFIQUE-SE DE";
            //    lblMsg2.Text = "QUE O MICROSOFT";
            //    lblMsg3.Text = "OFFICE WORD ESTÁ";
            //    lblMsg4.Text = "INSTALADO NO PC.";
            //    lblMsg5.Text = "PREF.: OFFICE 2010";
            //}
        }
        private void rabEXCEL_CheckedChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;

            //if (rabExcel.Checked == true)
            //{
            //    lblNomedoArquivo.Text = "EXC_TabProgr.xlsx";
            //    lblMsg1.Text = "CERTIFIQUE-SE DE";
            //    lblMsg2.Text = "QUE O MICROSOFT";
            //    lblMsg3.Text = "OFFICE EXCEL ESTÁ";
            //    lblMsg4.Text = "INSTALADO NO PC.";
            //    lblMsg5.Text = "PREF.: OFFICE 2010";
            //}
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
        private void comSituacaoIMP_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
        }
        #endregion








        


        



        

        
    }
}
