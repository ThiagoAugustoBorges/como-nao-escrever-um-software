using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TabRotas
{
    internal partial class TabRotas : Form
    {
        public TabRotas()
        {
            InitializeComponent();
        }


        public string _Login_LojaID_TabRotas { get; set; }
        public string _Login_UsuarioID_TabRotas { get; set; }
        public int _Tipo { get; set; }

        //LOAD DO FORM
        private void TabRotas_Load(object sender, EventArgs e)
        {
            //DESATIVA OS CAMPOS
            CamposDisable();

            TabRotas_Permi PERMI = new TabRotas_Permi();
            TabRotas_MET MET = new TabRotas_MET();
            TabRotas_FILTROS FILTROS = new TabRotas_FILTROS();

            //PREENCHE O USUÁRIO
            txtUsuario.Text = _Login_UsuarioID_TabRotas.PadLeft(6, '0');

            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panDownAb1, panDownAb2, panDownAb3, lblImpreTitulo, panDownAb6, rtbInforma, lblTitulo };
            //MÉTODO DA CFG
            CfgComun.CfgComun_CLASS CFG = new CfgComun.CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, _Login_LojaID_TabRotas);

            //VERIFICA AS PERMISSÕES DOS BUTTONS
            PERMI.PER_Permiss_Buttons(btnIncluir, btnAlterar, btnExcluir, btnSeta1, btnSeta2, btnSeta3, btnSeta4, txtCodigo, _Login_UsuarioID_TabRotas);

            //VERIFICA SE É PARA GRAVAR OS FILTROS
            FILTROS.cheFILTROSChecked(cheFiltrosIMP, cheFiltrosPES, _Login_LojaID_TabRotas);

            #region CANCELA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged -= new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged -= new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged -= new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged -= new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged -= new EventHandler(nupQtResultados_ValueChanged);
            comPesStatus.SelectedIndexChanged -= new EventHandler(comPesStatus_SelectedIndexChanged);
            #endregion
            //CARREGA OS FILTROS
            FILTROS.CarregaFILTROS(panPrinAb2, panPrinAb3, panPrinAb4);
            #region VOLTA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged += new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged += new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged += new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged += new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged += new EventHandler(nupQtResultados_ValueChanged);
            comPesStatus.SelectedIndexChanged += new EventHandler(comPesStatus_SelectedIndexChanged);
            if (rabTOP.Checked == true) { nupQtResultados.Enabled = true; }
            #endregion

            //CAPTURA O CAMINHO DO ARQUIVO CASO NÃO ESTEJA NO XML DOS FILTROS
            txtCaminhoRel.Text = MET.MET_CapCaminhoSALV(txtCaminhoRel, _Login_LojaID_TabRotas);

            btnIncluir.Select();

            //SELECIONA A 6° ABA PELO ATALHO
            if (_Tipo == 1)
            {
                TabControl.SelectedTab = Tp6;
            }
        }

        //PROPRIEDADES POR ABAS
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
                TabRotas_Lixeira Lix = new TabRotas_Lixeira();
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

                //Popula os campos do histórico
                TabRotas_Histórico HIST = new TabRotas_Histórico();
                HIST.HIS_SelecionaDados(mtbData1His, mtbData2His, txtUsuarioHis, txtUsuario);



                HIST.HIS_PopularHISTORICO(txtUsuarioHis, this, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
                mtbData1His.Select();
                SendKeys.Send("{HOME}");
                SendKeys.Send("+{END}");


                comIDHis.SelectedIndexChanged += new EventHandler(comIDHis_SelectedIndexChanged);
            }
            #endregion
            #region Prop. ABA 6
            if (TabControl.SelectedTab == Tp6)
            {
                txtMESTRE.Text = "RELAÇÃO";
                txtMESTRE.BackColor = Color.YellowGreen;
                txtMESTRE.ForeColor = Color.Aqua;

                ButtonsDisable();
                txtCodigoRel.Text = string.Empty;
                txtDescriRel.Text = string.Empty;

                txtCodigoRel.Select();
            }
            #endregion
        }

        //PERMISSÃO POR ABAS
        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            TabRotas_Permi PERMI = new TabRotas_Permi();
            if (e.TabPageIndex > 0)
            {
                PERMI.PER_VerificaPermi_TbCont(e, e.TabPageIndex, txtCodigo, _Login_UsuarioID_TabRotas);
            }
        }

        //TECLAS DE ATALHO
        private void TabRotas_KeyDown(object sender, KeyEventArgs e)
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
                    btnFecharRelacao.PerformClick();
                    break;
                case Keys.F8:
                    btnVoltarIMP.PerformClick();
                    btnVoltar.PerformClick();
                    btnRelVoltarPadrao.PerformClick();
                    break;
                case Keys.F9:
                    btnZerar.PerformClick();
                    break;
                case Keys.F10:
                    btnGravar.PerformClick();
                    btnGravarRelacao.PerformClick();
                    break;
                case Keys.F11:
                    btnPrint.PerformClick();
                    btnHistorico.PerformClick();
                    break;
            }
        }

        //GRAVA OS FILTROS
        private void TabRotas_FormClosing(object sender, FormClosingEventArgs e)
        {
            TabRotas_FILTROS FILTROS = new TabRotas_FILTROS();
            FILTROS.GravarFILTROS(panPrinAb2, panPrinAb3, panPrinAb4);
        }




        #region FORM PRINCIPAL

        #region ZerarCampos, CamposEnable, CamposDisable
        public void ZerarCampos()
        {
            txtDescri.Text = string.Empty;
            comStatus.SelectedIndex = -1;
        }
        public void CamposEnable()
        {
            txtDescri.Enabled = true;
            comStatus.Enabled = true;
        }
        public void CamposDisable()
        {
            txtDescri.Enabled = false;
            comStatus.Enabled = false;
        }
        #endregion


        #region Buttons INCLUIR, ALTERAR e EXCLUIR
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            //APLICA A APARENCIA DO BUTTON
            TabRotas_AppaButtons Appa = new TabRotas_AppaButtons();
            Appa._ButtonINC(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);
            //SELECIONA O ULTIMO +1
            TabRotas_MET MET = new TabRotas_MET();
            MET.MET_SelecionaUltimoRegistroMaisUm(txtCodigo, btnGravar, _Login_LojaID_TabRotas);
        }
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //APLICA A APARENCIA DO BUTTON
            TabRotas_AppaButtons Appa = new TabRotas_AppaButtons();
            Appa._ButtonALT(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //APLICA A APARENCIA DO BUTTON
            TabRotas_AppaButtons Appa = new TabRotas_AppaButtons();
            Appa._ButtonEXC(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);
        }
        #endregion


        #region Buttons SETAS 1,2,3 e 4
        private void btnSeta1_Click(object sender, EventArgs e)
        {
            //APARENCIA DAS SETAS
            TabRotas_AppaButtons Appa = new TabRotas_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);

            //EXECUTA A SETA
            TabRotas_ExecSETAS Exec = new TabRotas_ExecSETAS();
            Exec.ExecSETAS("1", txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabRotas, comStatus);
        }
        private void btnSeta2_Click(object sender, EventArgs e)
        {
            //APARENCIA DAS SETAS
            TabRotas_AppaButtons Appa = new TabRotas_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            //EXECUTA A SETA
            TabRotas_ExecSETAS Exec = new TabRotas_ExecSETAS();
            Exec.ExecSETAS("2", txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabRotas, comStatus);
        }
        private void btnSeta3_Click(object sender, EventArgs e)
        {
            //APARENCIA DAS SETAS
            TabRotas_AppaButtons Appa = new TabRotas_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            //EXECUTA A SETA
            TabRotas_ExecSETAS Exec = new TabRotas_ExecSETAS();
            Exec.ExecSETAS("3", txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabRotas, comStatus);
        }
        private void btnSeta4_Click(object sender, EventArgs e)
        {
            //APARENCIA DAS SETAS
            TabRotas_AppaButtons Appa = new TabRotas_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            //EXECUTA A SETA
            TabRotas_ExecSETAS Exec = new TabRotas_ExecSETAS();
            Exec.ExecSETAS("4", txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabRotas, comStatus);
        }
        #endregion



        #region Buttons Gravar, Zerar, Fechar e Ajuda
        private void btnGravar_Click(object sender, EventArgs e)
        {
            //DEFINE OS CAMPOS OBRIGATORIOS
            TabRotas_CamposObrig Obrig = new TabRotas_CamposObrig();
            bool Obrigacao = Obrig.CamposObrig(txtMESTRE, txtCodigo, txtDescri, comStatus);
            if (!Obrigacao) { } else { return; }

            //GRAVA OS INFORMAÇÕES
            TabRotas_bntGravar GRAVAR = new TabRotas_bntGravar();
            GRAVAR.GravarINC(txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabRotas, comStatus, txtUsuario);
            GRAVAR.GravarALT(txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabRotas, comStatus, txtUsuario);
            GRAVAR.GravarEXC(txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabRotas, comStatus, txtUsuario, MOTIVO_EXC);
        }
        private void btnZerar_Click(object sender, EventArgs e)
        {
            //ZERA OS CASMPO
            ZerarCampos();
            TabRotas_AppaButtons Appa = new TabRotas_AppaButtons();
            Appa._ButtonZER(txtMESTRE, txtCodigo, btnIncluir, btnGravar, CamposDisable, TabControl, Tp1);
        }
        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnAjuda_Click(object sender, EventArgs e)
        {
            //linkn da ajuda
            TabRotas_FILTROS FILT = new TabRotas_FILTROS();
            FILT.LinkAjuda();
        }
        #endregion


        //SELECIONA NO TAB
        public string MOTIVO_EXC { get; set; }
        private void txtCodigo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //SELECIONA O REGISTRO NO TAB
                TabRotas_MET MET = new TabRotas_MET();
                MET.MET_SelecionaCodigoTAB(txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabRotas, comStatus);

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


        //ZERA OS CAMPOS
        private void txtCodigo_MouseDown(object sender, MouseEventArgs e)
        {
            ZerarCampos();
            CamposDisable();
            txtCodigo.Select();
            btnGravar.Enabled = false;
            txtCodigo.SelectAll();
        }
        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            ZerarCampos();
            CamposDisable();
            btnGravar.Enabled = false;
        }
        private void txtCodigo_Click(object sender, EventArgs e)
        {
            txtCodigo.SelectAll();
        }


        //APENAS NÚMEROS
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabRotas_MET MET = new TabRotas_MET();
            MET.MET_ApenasNúmeros(e);
        }


        //VERIFICA SE JA EXISTE
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
                else
                {
                    TabRotas_MET MET = new TabRotas_MET();
                    MET.MET_VerificaRotaExiste(txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabRotas);
                }
            }
        }

        //ABRE A PESQUISA DE ROTA
        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');

                PesRotas.cs.PesRotas_CALL Call = new PesRotas.cs.PesRotas_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabRotas;
                Call._Login_CryptDesc = _Login_UsuarioID_TabRotas;
                Call._WenCrypt = "PesRotas18Wenemy3156!.350?°";
                Call.PesRotas_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtCodigo.Text = Call._ResultPesquisaCALL;
                }

                txtCodigo.SelectAll();
            }
        }

        #endregion

        #region PESQUISA

        #region Buttons Pesquisar, Voltar e Fechar
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            TabRotas_Pesquisa PESQ = new TabRotas_Pesquisa();
            PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabRotas, comPesStatus, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
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
                TabRotas_AppaButtons Apa = new TabRotas_AppaButtons();
                Apa._ButtonZER(txtMESTRE, txtCodigo, btnIncluir, btnGravar, CamposDisable, TabControl, Tp1);
                txtCodigo.Text = Codigo;
                txtCodigo.Select();
                SendKeys.Send("{TAB}");
            }
        }


        #region PESQUISA PELOS EVENTOS
        private void rabNumerico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabNumerico.Checked == true)
            {
                //EXECUTA A PESQUISA
                TabRotas_Pesquisa PESQ = new TabRotas_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabRotas, comPesStatus, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void rabAlfabetico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabAlfabetico.Checked == true)
            {
                //EXECUTA A PESQUISA
                TabRotas_Pesquisa PESQ = new TabRotas_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabRotas, comPesStatus, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void rabTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTodos.Checked == true)
            {
                //EXECUTA A PESQUISA
                TabRotas_Pesquisa PESQ = new TabRotas_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabRotas, comPesStatus, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void rabTOP_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTOP.Checked == true)
            {
                nupQtResultados.Enabled = true;
                //EXECUTA A PESQUISA
                TabRotas_Pesquisa PESQ = new TabRotas_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabRotas, comPesStatus, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
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
                TabRotas_Pesquisa PESQ = new TabRotas_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabRotas, comPesStatus, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void comPesStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabRotas_Pesquisa PESQ = new TabRotas_Pesquisa();
            PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabRotas, comPesStatus, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
            txtPesDescri.Select(); txtPesDescri.SelectAll();
        }
        private void txtPesDescri_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabRotas_Pesquisa PESQ = new TabRotas_Pesquisa();
            PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabRotas, comPesStatus, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
        }
        #endregion

        #endregion

        #region IMPRESSÃO

        #region Buttons Print, Voltar e Fechar
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
        public string NomeDoArquivo = "TabRotas";

        private void rabRPV_CheckedChanged(object sender, EventArgs e)
        {
            if (rabRPV.Checked == true)
            {
                lblNomedoArquivo.Text = NomeDoArquivo + ".rpv";
            }
            txtQtSelectIMP.Text = string.Empty;
        }
        private void rabWORD_CheckedChanged(object sender, EventArgs e)
        {
            if (rabWORD.Checked == true)
            {
                lblNomedoArquivo.Text = "WORD_" + NomeDoArquivo + ".docx";
            }
            txtQtSelectIMP.Text = string.Empty;
        }
        private void rabEXCEL_CheckedChanged(object sender, EventArgs e)
        {
            if (rabEXCEL.Checked == true)
            {
                lblNomedoArquivo.Text = "EXC_" + NomeDoArquivo + ".xlxs";
            }
            txtQtSelectIMP.Text = string.Empty;
        }
        private void rabTXT_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTXT.Checked == true)
            {
                lblNomedoArquivo.Text = "TXT_" + NomeDoArquivo + ".txt";
            }
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
        private void comImpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQtSelectIMP.Text = string.Empty;
        }
        #endregion


        #endregion

        #region LIXEIRA
        private void Dgv_Lixeira_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                //RESTAURA O ITEM DA LIXEIRA
                TabRotas_Lixeira Lixeira = new TabRotas_Lixeira();
                Lixeira.Lix_RESTAURAR(Dgv_Lixeira, cheVoltarLix, TabControl, Tp1, txtUsuario, btnGravar, txtMESTRE, CamposDisable, txtCodigo, btnIncluir);
            }
        }
        #endregion

        #region HISTÓRICO

        //FAZ A PESQUISA PELO INDEX
        private void comIDHis_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabRotas_Histórico HIST = new TabRotas_Histórico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, comIDHis, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
        }
        //FAZ A PESQUISA PELO BUTTON
        private void btnHistorico_Click(object sender, EventArgs e)
        {
            TabRotas_Histórico HIST = new TabRotas_Histórico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, btnHistorico, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
        }



        //VERIFICA SE A DATA É VÁLIDA
        private void mtbData1His_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //VERIFICA SE A DATA É VALIDA
                TabRotas_Histórico HIS = new TabRotas_Histórico();
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
                TabRotas_Histórico HIS = new TabRotas_Histórico();
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
                        TabRotas_Histórico HIST = new TabRotas_Histórico();
                        HIST.HIS_VerificaUsuarioEXISTE(txtUsuarioHis, mtbData2His, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
                    }
                }
            }
        }
        //APENAS NÚMEROS
        private void txtUsuarioHis_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabRotas_MET MET = new TabRotas_MET();
            MET.MET_ApenasNúmeros(e);
        }
        //ABRE A PESQUISA DO USUÁRIO
        private void txtUsuarioHis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                PesUsuar.cs.PesUsuar_CALL Call = new PesUsuar.cs.PesUsuar_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabRotas;
                Call._Login_CryptDesc = _Login_UsuarioID_TabRotas;
                Call._WenCrypt = "PesUsuar21Wenemy3156!.350?°";
                Call.PesUsuar_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtUsuarioHis.Text = Call._ResultPesquisaCALL;
                }

                txtUsuarioHis.SelectAll();
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
            txtUsuarioHis.SelectAll();
        }
        #endregion


        #endregion

        #region RELAÇÃO CLIENTES x ROTAS

        public void ButtonsEnable()
        {
            btnRelDown.Enabled = true;
            btnRelUp.Enabled = true;
            btnRelVoltarPadrao.Enabled = true;
            btnGravarRelacao.Enabled = true;
        }
        public void ButtonsDisable()
        {
            btnRelDown.Enabled = false;
            btnRelUp.Enabled = false;
            btnRelVoltarPadrao.Enabled = false;
            btnGravarRelacao.Enabled = false;
        }

        //SELECIONA A ROTA NO TAB
        private void txtCodigoRel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                TabRotas_Relac RELAC = new TabRotas_Relac();
                RELAC.Rel_SelectRota(Dgv_Relacao, txtCodigoRel, txtDescriRel, panUpAb6, ButtonsEnable, ButtonsDisable);
            }
        }


        //APENAS NÚMEROS
        private void txtCodigoRel_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabRotas_MET MET = new TabRotas_MET();
            MET.MET_ApenasNúmeros(e);
        }
        //ABRE A PESQUISA DE ROTAS
        private void txtCodigoRel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtCodigoRel.Text = txtCodigoRel.Text.PadLeft(6, '0');

                PesRotas.cs.PesRotas_CALL Call = new PesRotas.cs.PesRotas_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabRotas;
                Call._Login_CryptDesc = _Login_UsuarioID_TabRotas;
                Call._WenCrypt = "TabRotas18Wenemy3156!.350?°";
                Call.PesRotas_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtCodigoRel.Text = Call._ResultPesquisaCALL;
                }

                txtCodigoRel.SelectAll();
            }
        }
        //SELEC ALL
        private void txtCodigoRel_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonsDisable();
            txtDescriRel.Text = string.Empty;
            Dgv_Relacao.Rows.Clear();
            txtCodigoRel.SelectAll();
        }
        //APAGA NO TEXTCHANGE
        private void txtCodigoRel_TextChanged(object sender, EventArgs e)
        {
            ButtonsDisable();
            Dgv_Relacao.Rows.Clear();
            txtDescriRel.Text = string.Empty;
        }


        //REORGANIZA AS LINHAS PELOS BUTTONS UP E DOWNS
        private void btnRelUp_Click(object sender, EventArgs e)
        {
            TabRotas_Relac RELAC = new TabRotas_Relac();
            RELAC.Rel_ReorganizarUP(Dgv_Relacao);
        }
        private void btnRelDown_Click(object sender, EventArgs e)
        {
            TabRotas_Relac RELAC = new TabRotas_Relac();
            RELAC.Rel_ReorganizarDOWN(Dgv_Relacao);
        }


        //VOLTA AS LINHAS PARA O PADRÃO DO BANCO DE DADOS
        private void btnRelVoltarPadrao_Click(object sender, EventArgs e)
        {
            TabRotas_Relac RELAC = new TabRotas_Relac();
            RELAC.Rel_PopulaDataGrid(Dgv_Relacao, txtCodigoRel, txtDescriRel);

            txtCodigoRel.Select();
            txtCodigoRel.SelectAll();
        }



        //GRAVA A NOVA ORDEM DAS ROTAS
        private void btnGravarRelacao_Click(object sender, EventArgs e)
        {
            TabRotas_Relac RELAC = new TabRotas_Relac();
            RELAC.Rel_GravaInformac(Dgv_Relacao, txtCodigoRel, txtDescriRel);

            txtCodigoRel.Select();
            txtCodigoRel.SelectAll();
        }

        //FECHA O FORMULÁRIO
        private void btnFecharRelacao_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion



    }
}
