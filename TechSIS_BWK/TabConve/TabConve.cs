using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TabConve
{
    internal partial class TabConve : Form
    {
        public TabConve()
        {
            InitializeComponent();
        }

        public string _Login_LojaID_TabConve { get; set; }
        public string _Login_UsuarioID_TabConve { get; set; }


        //LOAD DO FORMULÁRIO
        private void TabConve_Load(object sender, EventArgs e)
        {
            //DESATIVA OS CAMPOS
            CamposDisable();


            //PREENCHE O USUÁRIO
            txtUsuario.Text = _Login_UsuarioID_TabConve.PadLeft(6, '0');

            TabConve_Permi PERMI = new TabConve_Permi();
            TabConve_MET MET = new TabConve_MET();
            TabConve_FILTROS FILTROS = new TabConve_FILTROS();

            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panDownAb1, panDownAb2, panDownAb3, lblTitulo, panDownAb6, txtQtSelectPES, txtQtSelectIMP };
            //MÉTODO DA CFG
            CfgComun.CfgComun_CLASS CFG = new CfgComun.CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, _Login_LojaID_TabConve);

            //VERIFICA AS PERMISSÕES DOS BUTTONS
            PERMI.PER_Permiss_Buttons(btnIncluir, btnAlterar, btnExcluir, btnSeta1, btnSeta2, btnSeta3, btnSeta4, txtCodigo, _Login_UsuarioID_TabConve);

            //VERIFICA SE É PARA GRAVAR OS FILTROS
            FILTROS.cheFILTROSChecked(cheFiltrosIMP, cheFiltrosPES, _Login_LojaID_TabConve);

            #region CANCELA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged -= new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged -= new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged -= new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged -= new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged -= new EventHandler(nupQtResultados_ValueChanged);
            comPesTipo.SelectedIndexChanged -= new EventHandler(comPesTipo_SelectedIndexChanged);
            #endregion
            //CARREGA OS FILTROS
            FILTROS.CarregaFILTROS(panPrinAb2, panPrinAb3, panPrinAb4);
            #region VOLTA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged += new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged += new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged += new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged += new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged += new EventHandler(nupQtResultados_ValueChanged);
            comPesTipo.SelectedIndexChanged += new EventHandler(comPesTipo_SelectedIndexChanged);
            if (rabTOP.Checked == true) { nupQtResultados.Enabled = true; }
            #endregion

            //CAPTURA O CAMINHO DO ARQUIVO CASO NÃO ESTEJA NO XML DOS FILTROS
            txtCaminhoRel.Text = MET.MET_CapCaminhoSALV(txtCaminhoRel, _Login_LojaID_TabConve);

            btnIncluir.Select();

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
                TabConve_Lixeira Lix = new TabConve_Lixeira();
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
                TabConve_Histórico HIST = new TabConve_Histórico();
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

                txtCodigoAb6.Text = string.Empty;
                txtCodigoAb6.Select();

                TabConve_Relac RELAC = new TabConve_Relac();
                RELAC.Rel_PopulaDataGrid(Dgv_Relacao, txtCodigoAb6);
            }
            #endregion
        }

        //PERMISSÃO POR ABAS
        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            TabConve_Permi PERMI = new TabConve_Permi();
            if (e.TabPageIndex > 0)
            {
                PERMI.PER_VerificaPermi_TbCont(e, e.TabPageIndex, txtCodigo, _Login_UsuarioID_TabConve);
            }
        }

        //TECLAS DE ATALHO
        private void TabControl_KeyDown(object sender, KeyEventArgs e)
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
                    btnFecharCli.PerformClick();
                    break;
                case Keys.F8:
                    btnVoltarPesq.PerformClick();
                    btnVoltarImp.PerformClick();
                    btnVoltarCli.PerformClick();
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
        private void TabConve_FormClosing(object sender, FormClosingEventArgs e)
        {
            TabConve_FILTROS FILTROS = new TabConve_FILTROS();
            FILTROS.GravarFILTROS(panPrinAb2, panPrinAb3, panPrinAb4);
        }

        #region FORM PRINCIPAL

        #region ZerarCampos, CamposEnable, CamposDisable
        public void ZerarCampos()
        {
            comStatus.SelectedIndex = -1;
            foreach (Control panPrin in panPrinAb1.Controls)
            {
                foreach (Control CONTROLs in panPrin.Controls)
                {
                    if (CONTROLs.GetType() == typeof(TextBox) && CONTROLs.Name != "txtCodigo")
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
        }
        public void CamposEnable()
        {
            comStatus.Enabled = true;
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
        }
        public void CamposDisable()
        {
            comStatus.Enabled = false;
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
        }
        #endregion


        #region Buttons INCLUIR, ALTERAR e EXCLUIR
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            //APLICA A APARENCIA DO BUTTON
            TabConve_AppaButtons Appa = new TabConve_AppaButtons();
            Appa._ButtonINC(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);
            //SELECIONA O ULTIMO +1
            TabConve_MET MET = new TabConve_MET();
            MET.MET_SelecionaUltimoRegistroMaisUm(txtCodigo, btnGravar, _Login_LojaID_TabConve);
        }
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //APLICA A APARENCIA DO BUTTON
            TabConve_AppaButtons Appa = new TabConve_AppaButtons();
            Appa._ButtonALT(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //APLICA A APARENCIA DO BUTTON
            TabConve_AppaButtons Appa = new TabConve_AppaButtons();
            Appa._ButtonEXC(txtMESTRE, btnGravar, txtCodigo, CamposDisable, TabControl, Tp1, ZerarCampos);
        }
        #endregion

        #region Buttons SETAS 1,2,3 e 4
        private void btnSeta1_Click(object sender, EventArgs e)
        {
            //APARENCIA DAS SETAS
            TabConve_AppaButtons Appa = new TabConve_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);

            //EXECUTA A SETA
            TabConve_ExecSETAS Exec = new TabConve_ExecSETAS();
            Exec.ExecSETAS("1", txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabConve, comStatus, comTipo, txtTaxa);
        }
        private void btnSeta2_Click(object sender, EventArgs e)
        {
            //APARENCIA DAS SETAS
            TabConve_AppaButtons Appa = new TabConve_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            //EXECUTA A SETA
            TabConve_ExecSETAS Exec = new TabConve_ExecSETAS();
            Exec.ExecSETAS("2", txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabConve, comStatus, comTipo, txtTaxa);
        }
        private void btnSeta3_Click(object sender, EventArgs e)
        {
            //APARENCIA DAS SETAS
            TabConve_AppaButtons Appa = new TabConve_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            //EXECUTA A SETA
            TabConve_ExecSETAS Exec = new TabConve_ExecSETAS();
            Exec.ExecSETAS("3", txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabConve, comStatus, comTipo, txtTaxa);
        }
        private void btnSeta4_Click(object sender, EventArgs e)
        {
            //APARENCIA DAS SETAS
            TabConve_AppaButtons Appa = new TabConve_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            //EXECUTA A SETA
            TabConve_ExecSETAS Exec = new TabConve_ExecSETAS();
            Exec.ExecSETAS("4", txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabConve, comStatus, comTipo, txtTaxa);
        }
        #endregion

        #region Buttons Gravar, Zerar, Fechar e Ajuda
        private void btnGravar_Click(object sender, EventArgs e)
        {
            //DEFINE OS CAMPOS OBRIGATORIOS
            TabConve_CamposObrig Obrig = new TabConve_CamposObrig();
            bool Obrigacao = Obrig.CamposObrig(txtMESTRE, txtCodigo, txtDescri, comStatus, comTipo);
            if (!Obrigacao) { } else { return; }

            //GRAVA OS INFORMAÇÕES
            TabConve_btnGravar GRAVAR = new TabConve_btnGravar();
            GRAVAR.GravarINC(txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabConve, comTipo, txtUsuario, txtTaxa, comStatus);
            GRAVAR.GravarALT(txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabConve, comTipo, txtUsuario, txtTaxa, comStatus);
            GRAVAR.GravarEXC(txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabConve, comTipo, txtUsuario, txtTaxa, comStatus, MOTIVO_EXC);
        }
        private void btnZerar_Click(object sender, EventArgs e)
        {
            //ZERA OS CASMPO
            ZerarCampos();
            TabConve_AppaButtons Appa = new TabConve_AppaButtons();
            Appa._ButtonZER(txtMESTRE, txtCodigo, btnIncluir, btnGravar, CamposDisable, TabControl, Tp1);
        }
        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnAjuda_Click(object sender, EventArgs e)
        {
            //linkn da ajuda
            TabConve_FILTROS FILT = new TabConve_FILTROS();
            FILT.LinkAjuda();
            btnIncluir.Select();
        }
        #endregion

        //SELECIONA NO TAB
        public string MOTIVO_EXC { get; set; }
        private void txtCodigo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //SELECIONA O REGISTRO NO TAB
                TabConve_MET MET = new TabConve_MET();
                MET.MET_SelecionaCodigoTAB(txtMESTRE, txtCodigo, panUpAb1, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabConve, comStatus, comTipo, txtTaxa);

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


        //APENAS NÚMEROS
        private void txtTaxa_KeyPress(object sender, KeyPressEventArgs e)
        {
            //RECEBE NÚMEROS DECIMAL
            TabConve_MET MET = new TabConve_MET();
            MET.MET_ApenasNúmerosDec(e, txtTaxa);
        }
        //APENAS NÚMEROS
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabConve_MET MET = new TabConve_MET();
            MET.MET_ApenasNúmeros(e);
        }
        //ABRE A PESQUISA DE CONVENIO
        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtCodigo.Text = txtCodigo.Text.PadLeft(6, '0');

                PesConve.cs.PesConve_CALL Call = new PesConve.cs.PesConve_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabConve;
                Call._Login_CryptDesc = _Login_UsuarioID_TabConve;
                Call._WenCrypt = "PesConve3Wenemy3156!.350?°";
                Call.PesConve_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtCodigo.Text = Call._ResultPesquisaCALL;
                }

                txtCodigo.SelectAll();
            }
        }


        //TRATAMENTO PARA DECIMAL
        private void txtTaxa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oemcomma)
            {
                txtTaxa.MaxLength = 5;
            }
            else
            {
                if (!txtTaxa.Text.Contains(","))
                {
                    txtTaxa.MaxLength = 2;
                }
                else
                {
                    txtTaxa.MaxLength = 5;
                }
            }
        }
        private void txtTaxa_Leave(object sender, EventArgs e)
        {
            if (txtTaxa.Text != string.Empty)
            {
                if (txtTaxa.Text.Contains(","))
                {
                    txtTaxa.Text = Convert.ToDecimal(txtTaxa.Text).ToString("00.00");
                }
                else
                {
                    if (txtTaxa.Text.Length > 2)
                    {
                        decimal Dividido;
                        Dividido = Convert.ToDecimal(txtTaxa.Text);
                        Dividido = Decimal.Parse(txtTaxa.Text);
                        txtTaxa.Text = Convert.ToString(Dividido / 100);
                        txtTaxa.Text = Convert.ToDecimal(txtTaxa.Text).ToString("00.00");
                    }
                    else
                    {
                        txtTaxa.Text = Convert.ToDecimal(txtTaxa.Text).ToString("00.00");
                    }
                }
            }
        }


        //SELECT ALL NO CLICK
        private void txtTaxa_MouseDown(object sender, MouseEventArgs e)
        {
            txtTaxa.SelectAll();
        }
        private void txtCodigo_MouseDown(object sender, MouseEventArgs e)
        {
            txtCodigo.SelectAll();
            ZerarCampos();
            CamposDisable();
            txtCodigo.SelectAll();
        }
        private void txtDescri_MouseDown(object sender, MouseEventArgs e)
        {
            txtDescri.SelectAll();
        }
        private void txtCodigo_Click(object sender, EventArgs e)
        {
            txtCodigo.SelectAll();
        }




        #endregion

        #region IMPRESSÃO

        #region Buttons Print, Voltar e Fechar
        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
        private void btnVoltarImp_Click(object sender, EventArgs e)
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
        public string NomeDoArquivo = "TabConve";

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

        #region PESQUISA

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
                TabConve_AppaButtons Apa = new TabConve_AppaButtons();
                Apa._ButtonZER(txtMESTRE, txtCodigo, btnIncluir, btnGravar, CamposDisable, TabControl, Tp1);
                txtCodigo.Text = Codigo;
                txtCodigo.Select();
                SendKeys.Send("{TAB}");
            }
        }

        #region Buttons Pesquisar, Voltar e Fechar
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            TabConve_Pesquisa PESQ = new TabConve_Pesquisa();
            PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabConve, comPesTipo, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
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

        #region PESQUISA PELOS EVENTOS
        private void rabNumerico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabNumerico.Checked == true)
            {
                //EXECUTA A PESQUISA
                TabConve_Pesquisa PESQ = new TabConve_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabConve, comPesTipo, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void rabAlfabetico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabAlfabetico.Checked == true)
            {
                //EXECUTA A PESQUISA
                TabConve_Pesquisa PESQ = new TabConve_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabConve, comPesTipo, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void rabTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTodos.Checked == true)
            {
                //EXECUTA A PESQUISA
                TabConve_Pesquisa PESQ = new TabConve_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabConve, comPesTipo, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void rabTOP_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTOP.Checked == true)
            {
                nupQtResultados.Enabled = true;
                //EXECUTA A PESQUISA
                TabConve_Pesquisa PESQ = new TabConve_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabConve, comPesTipo, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
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
                TabConve_Pesquisa PESQ = new TabConve_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabConve, comPesTipo, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void comPesTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabConve_Pesquisa PESQ = new TabConve_Pesquisa();
            PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabConve, comPesTipo, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
            txtPesDescri.Select(); txtPesDescri.SelectAll();
        }
        private void txtPesDescri_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabConve_Pesquisa PESQ = new TabConve_Pesquisa();
            PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabConve, comPesTipo, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
        }
        #endregion

        



        #endregion

        #region LIXEIRA
        private void Dgv_Lixeira_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                //RESTAURA O ITEM DA LIXEIRA
                TabConve_Lixeira Lixeira = new TabConve_Lixeira();
                Lixeira.Lix_RESTAURAR(Dgv_Lixeira, cheVoltarLix, TabControl, Tp1, txtUsuario, btnGravar, txtMESTRE, CamposDisable, txtCodigo, btnIncluir);
            }
        }
        #endregion

        #region HISTÓRICO

        //FAZ A PESQUISA PELO INDEX
        private void comIDHis_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabConve_Histórico HIST = new TabConve_Histórico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, comIDHis, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
        }
        //FAZ A PESQUISA PELO BUTTON
        private void btnHistorico_Click(object sender, EventArgs e)
        {
            TabConve_Histórico HIST = new TabConve_Histórico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, btnHistorico, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
        }



        //VERIFICA SE A DATA É VÁLIDA
        private void mtbData1His_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //VERIFICA SE A DATA É VALIDA
                TabConve_Histórico HIS = new TabConve_Histórico();
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
                TabConve_Histórico HIS = new TabConve_Histórico();
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
                        TabConve_Histórico HIST = new TabConve_Histórico();
                        HIST.HIS_VerificaUsuarioEXISTE(txtUsuarioHis, mtbData2His, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
                    }
                }
            }
        }
        //APENAS NÚMEROS
        private void txtUsuarioHis_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabConve_MET MET = new TabConve_MET();
            MET.MET_ApenasNúmeros(e);
        }
        //ABRE A PESQUISA DO USUÁRIO
        private void txtUsuarioHis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                PesUsuar.cs.PesUsuar_CALL Call = new PesUsuar.cs.PesUsuar_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabConve;
                Call._Login_CryptDesc = _Login_UsuarioID_TabConve;
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

        #region RELAÇÃO

        //APENAS NÚMEROS
        private void txtCodigoAb6_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabConve_MET MET = new TabConve_MET();
            MET.MET_ApenasNúmeros(e);
        }

        //ABRE A PESQUISA
        private void txtCodigoAb6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtCodigoAb6.Text = txtCodigoAb6.Text.PadLeft(6, '0');

                PesConve.cs.PesConve_CALL Call = new PesConve.cs.PesConve_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabConve;
                Call._Login_CryptDesc = _Login_UsuarioID_TabConve;
                Call._WenCrypt = "PesConve3Wenemy3156!.350?°";
                Call.PesConve_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtCodigoAb6.Text = Call._ResultPesquisaCALL;
                }

                txtCodigoAb6.SelectAll();
            }
        }

        //EXECUTA A PESQUISA
        private void txtCodigoAb6_Leave(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab == Tp6)
            {
                TabConve_Relac RELAC = new TabConve_Relac();
                RELAC.Rel_PopulaDataGrid(Dgv_Relacao, txtCodigoAb6);
            }
        }

        //SELECT ALL
        private void txtCodigoAb6_MouseDown(object sender, MouseEventArgs e)
        {
            txtCodigoAb6.SelectAll();
        }

        //BUTTONS
        private void btnVoltarCli_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = Tp1;
        }
        private void btnFecharCli_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion



    }
}
