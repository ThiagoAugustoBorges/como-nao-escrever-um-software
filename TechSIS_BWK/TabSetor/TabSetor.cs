using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TabSetor
{
    internal partial class TabSetor : Form
    {
        public TabSetor()
        {
            InitializeComponent();
        }

        public string _Login_LojaID_TabSetor { get; set; }
        public string _Login_UsuarioID_TabSetor { get; set; }

        //LOAD DO FORM
        private void TabSetor_Load(object sender, EventArgs e)
        {
            //DESATIVA OS CAMPOS
            CamposDisable();

            TabSetor_Permi PERMI = new TabSetor_Permi();
            TabSetor_MET MET = new TabSetor_MET();
            TabSetor_FILTROS FILTROS = new TabSetor_FILTROS();

            //PREENCHE O USUÁRIO
            txtUsuario.Text = _Login_UsuarioID_TabSetor.PadLeft(6, '0');

            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panDownAb1, panDownAb2, panDownAb3, lblImpreTitulo, lblTitulo, txtQtSelectPES, txtQtSelectIMP };
            //MÉTODO DA CFG
            CfgComun.CfgComun_CLASS CFG = new CfgComun.CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, _Login_LojaID_TabSetor);

            //VERIFICA AS PERMISSÕES DOS BUTTONS
            PERMI.PER_Permiss_Buttons(btnIncluir, btnAlterar, btnExcluir, btnSeta1, btnSeta2, btnSeta3, btnSeta4, txtSetCod, _Login_UsuarioID_TabSetor);

            //VERIFICA SE É PARA GRAVAR OS FILTROS
            FILTROS.cheFILTROSChecked(cheFiltrosIMP, cheFiltrosPES, _Login_LojaID_TabSetor);

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
            txtCaminhoRel.Text = MET.MET_CapCaminhoSALV(txtCaminhoRel, _Login_LojaID_TabSetor);

            btnIncluir.Select();
        }

        //TECLAS DE ATALHO
        private void TabSetor_KeyDown(object sender, KeyEventArgs e)
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
                    btnVoltarIMP.PerformClick();
                    btnVoltar.PerformClick();
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
        private void TabSetor_FormClosing(object sender, FormClosingEventArgs e)
        {
            TabSetor_FILTROS FILT = new TabSetor_FILTROS();
            FILT.GravarFILTROS(panPrinAb2, panPrinAb3, panPrinAb4);
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
                txtSetCod.Text = string.Empty;
                txtSubCod.Text = string.Empty;
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
                TabSetor_Lixeira Lix = new TabSetor_Lixeira();
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
                TabSetor_Histórico HIST = new TabSetor_Histórico();
                HIST.HIS_SelecionaDados(mtbData1His, mtbData2His, txtUsuarioHis, txtUsuario);



                HIST.HIS_PopularHISTORICO(txtUsuarioHis, this, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
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
            TabSetor_Permi PERMI = new TabSetor_Permi();
            if (e.TabPageIndex > 0)
            {
                PERMI.PER_VerificaPermi_TbCont(e, e.TabPageIndex, txtSetCod, _Login_UsuarioID_TabSetor);
            }
        }




        #region FORMULÁRIO

        #region Zerar, Enable e Disable Campos
        public void ZerarCampos()
        {
            comStatus.SelectedIndex = -1;
            lblDescricao.Text = "DESCRIÇÃO DO SETOR OU SUBSETOR";
            foreach (Control panPrin in panPrinAb1.Controls)
            {
                foreach (Control CONTROLs in panPrin.Controls)
                {
                    if (CONTROLs.GetType() == typeof(TextBox) && CONTROLs.Name != "txtSetCod" && CONTROLs.Name != "txtSubCod")
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
                    if (CONTROLs.GetType() == typeof(TextBox) && CONTROLs.Name != "txtSetCod" && CONTROLs.Name != "txtSubCod")
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
            TabSetor_AppaButtons Appa = new TabSetor_AppaButtons();
            Appa._ButtonINC(txtMESTRE, btnGravar, txtSetCod, CamposDisable, TabControl, Tp1, ZerarCampos);

            //DESABILITA O TXT SUB
            txtSubCod.Enabled = false;
            txtSubCod.Text = string.Empty;

            //SELECIONA O ULTIMO +1
            TabSetor_MET MET = new TabSetor_MET();
            MET.MET_SelecionaUltimoRegistroMaisUmSETOR(txtSetCod, btnGravar, _Login_LojaID_TabSetor);
        }
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            //APLICA A APARENCIA DO BUTTON
            TabSetor_AppaButtons Appa = new TabSetor_AppaButtons();
            Appa._ButtonALT(txtMESTRE, btnGravar, txtSetCod, CamposDisable, TabControl, Tp1, ZerarCampos);

            //DESABILITA O TXT SUB
            txtSubCod.Enabled = false;
            txtSubCod.Text = string.Empty;
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //APLICA A APARENCIA DO BUTTON
            TabSetor_AppaButtons Appa = new TabSetor_AppaButtons();
            Appa._ButtonEXC(txtMESTRE, btnGravar, txtSetCod, CamposDisable, TabControl, Tp1, ZerarCampos);

            //DESABILITA O TXT SUB
            txtSubCod.Enabled = false;
            txtSubCod.Text = string.Empty;
        }
        #endregion

        #region Buttons Gravar, Zerar, Fechar e Ajuda
        private void btnGravar_Click(object sender, EventArgs e)
        {
            //DEFINE OS CAMPOS OBRIGATORIOS
            TabSetor_CamposObrig Obrig = new TabSetor_CamposObrig();
            bool Obrigacao = Obrig.CamposObrig(txtMESTRE, txtSetCod, txtSubCod, txtDescri, comStatus);
            if (!Obrigacao) { } else { return; }

            //GRAVA OS INFORMAÇÕES
            TabSetor_bntGravar GRAVAR = new TabSetor_bntGravar();
            GRAVAR.GravarINC(txtMESTRE, txtSetCod, txtSubCod, txtSetCod, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabSetor, comStatus, txtUsuario, txtRespon, txtLocali);
            GRAVAR.GravarALT(txtMESTRE, txtSetCod, txtSubCod, txtSetCod, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabSetor, comStatus, txtUsuario, txtRespon, txtLocali);
            GRAVAR.GravarEXC(txtMESTRE, txtSetCod, txtSubCod, txtSetCod, ZerarCampos, CamposDisable, btnGravar, CamposEnable, txtDescri, _Login_LojaID_TabSetor, comStatus, txtUsuario, txtRespon, txtLocali, MOTIVO_EXC);
        }
        private void btnZerar_Click(object sender, EventArgs e)
        {
            //ZERA OS CASMPO
            ZerarCampos();
            TabSetor_AppaButtons Appa = new TabSetor_AppaButtons();
            Appa._ButtonZER(txtMESTRE, txtSetCod, btnIncluir, btnGravar, CamposDisable, TabControl, Tp1);
        }
        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnAjuda_Click(object sender, EventArgs e)
        {
            //linkn da ajuda
            TabSetor_FILTROS FILT = new TabSetor_FILTROS();
            FILT.LinkAjuda();
        }
        #endregion

        #region Buttons SETAS 1,2,3 e 4
        private void btnSeta1_Click(object sender, EventArgs e)
        {
            TabSetor_AppaButtons Appa = new TabSetor_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            TabSetor_ExecSETAS SETAS = new TabSetor_ExecSETAS();
            SETAS.ExecSETAS_SET("1", txtMESTRE, txtSetCod, txtSubCod, CamposDisable, btnGravar, txtDescri, _Login_LojaID_TabSetor, comStatus, txtRespon, txtLocali);
        }
        private void btnSeta2_Click(object sender, EventArgs e)
        {
            TabSetor_AppaButtons Appa = new TabSetor_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            TabSetor_ExecSETAS SETAS = new TabSetor_ExecSETAS();
            SETAS.ExecSETAS_SUB("2", txtMESTRE, txtSetCod, txtSubCod, CamposDisable, btnGravar, txtDescri, _Login_LojaID_TabSetor, comStatus, txtRespon, txtLocali, btnSeta1, btnSeta4);
        }
        private void btnSeta3_Click(object sender, EventArgs e)
        {
            TabSetor_AppaButtons Appa = new TabSetor_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            TabSetor_ExecSETAS SETAS = new TabSetor_ExecSETAS();
            SETAS.ExecSETAS_SUB("3", txtMESTRE, txtSetCod, txtSubCod, CamposDisable, btnGravar, txtDescri, _Login_LojaID_TabSetor, comStatus, txtRespon, txtLocali, btnSeta1, btnSeta4);
        }
        private void btnSeta4_Click(object sender, EventArgs e)
        {
            TabSetor_AppaButtons Appa = new TabSetor_AppaButtons();
            Appa._ButtonSETAS(txtMESTRE, btnGravar, CamposDisable, TabControl, Tp1);
            TabSetor_ExecSETAS SETAS = new TabSetor_ExecSETAS();
            SETAS.ExecSETAS_SET("4", txtMESTRE, txtSetCod, txtSubCod, CamposDisable, btnGravar, txtDescri, _Login_LojaID_TabSetor, comStatus, txtRespon, txtLocali);
        }
        #endregion


        //SELECIONA SETOR NO TAB
        private void txtSetCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtSetCod.Text = txtSetCod.Text.PadLeft(3, '0');
                TabSetor_MET MET = new TabSetor_MET();
                MET.MET_SelecionaCodigoTAB_SET(txtMESTRE, txtSetCod, txtSubCod, panUpAb1, ZerarCampos, CamposEnable, CamposDisable, btnGravar, _Login_LojaID_TabSetor, lblDescricao, comStatus);
            }
        }
        //SELECIONA SUBSETOR NO TAB
        public string MOTIVO_EXC { get; set; }
        private void txtSubCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtSubCod.Text = txtSubCod.Text.PadLeft(4, '0');
                TabSetor_MET MET = new TabSetor_MET();
                MET.MET_SelecionaCodigoTAB_SUB(txtMESTRE, txtSetCod, txtSubCod, txtSetCod, ZerarCampos, CamposEnable, CamposDisable, btnGravar, _Login_LojaID_TabSetor, lblDescricao, txtDescri, txtRespon, txtLocali, comStatus);

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
                    txtSetCod.Focus();
                    txtSubCod.SelectAll();
                }
                #endregion
            }
        }

        //SELECT ALL NO CLICK
        private void txtSetCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtSetCod.SelectAll();
            ZerarCampos();
            CamposDisable();
            btnGravar.Enabled = false;

            //DESABILITA O TXT SUB
            txtSubCod.Enabled = false;
            txtSubCod.Text = string.Empty;
        }
        private void txtSetCod_TextChanged(object sender, EventArgs e)
        {
            ZerarCampos();
            CamposDisable();
            btnGravar.Enabled = false;

            //DESABILITA O TXT SUB
            txtSubCod.Enabled = false;
            txtSubCod.Text = string.Empty;
        }
        private void txtSetCod_Click(object sender, EventArgs e)
        {
            txtSetCod.SelectAll();
            btnGravar.Enabled = false;

            //DESABILITA O TXT SUB
            txtSubCod.Enabled = false;
            txtSubCod.Text = string.Empty;
        }

        //SELECT ALL NO CLICK
        private void txtSubCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtSubCod.SelectAll();
            ZerarCampos();
            CamposDisable();
            btnGravar.Enabled = false;
        }
        private void txtSubCod_TextChanged(object sender, EventArgs e)
        {
            ZerarCampos();
            CamposDisable();
            btnGravar.Enabled = false;
        }
        private void txtSubCod_Click(object sender, EventArgs e)
        {
            txtSubCod.SelectAll();
            btnGravar.Enabled = false;
        }


        //VERIFICA SE SETOR.SUBSETOR EXISTE
        private void txtDescri_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                TabSetor_MET MET = new TabSetor_MET();
                MET.MET_VerificaSetorExiste(txtMESTRE, txtDescri, panUpAb1, txtSetCod,txtSubCod, btnGravar, ZerarCampos, CamposDisable);
            }
        }



        //APENAS NÚMERO E ABRE A PESQUISA
        private void txtSetCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabSetor_MET MET = new TabSetor_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtSetCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtSetCod.Text = txtSetCod.Text.PadLeft(3, '0');

                PesSetor.cs.PesSetor_CALL Call = new PesSetor.cs.PesSetor_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabSetor;
                Call._Login_CryptDesc = _Login_UsuarioID_TabSetor;
                Call._TipoChamada_CALL = "1";
                Call._Setor_CALL = "";
                Call._WenCrypt = "PesSetor19Wenemy3156!.350?°";
                Call.PesSetor_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtSetCod.Text = Call._ResultPesquisaCALL.Substring(0, 3);
                }

                txtSetCod.SelectAll();
            }
        }
        //APENAS NÚMERO E ABRE A PESQUISA
        private void txtSubCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabSetor_MET MET = new TabSetor_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtSubCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                if (Convert.ToInt32(txtSetCod.Text) > 0)
                {
                    txtSubCod.Text = txtSubCod.Text.PadLeft(4, '0');

                    PesSetor.cs.PesSetor_CALL Call = new PesSetor.cs.PesSetor_CALL();
                    Call._Login_CryptCode = _Login_LojaID_TabSetor;
                    Call._Login_CryptDesc = _Login_UsuarioID_TabSetor;
                    Call._TipoChamada_CALL = "2";
                    Call._Setor_CALL = txtSetCod.Text;
                    Call._WenCrypt = "PesSetor19Wenemy3156!.350?°";
                    Call.PesSetor_AUTORIZADO();

                    if (Call._ResultPesquisaCALL != string.Empty)
                    {
                        txtSubCod.Text = Call._ResultPesquisaCALL.Substring(3, 4);
                    }

                    txtSubCod.SelectAll();
                }
                else
                {
                    MessageBox.Show("Informe um setor para iniciar a pesquisa!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSetCod.Select(); txtSetCod.SelectAll();
                }
            }
        }

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

        #region Buttons Pesquisar, Voltar e Fechar
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            TabSetor_Pesquisa PESQ = new TabSetor_Pesquisa();
            PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabSetor, comPesStatus, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
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

        #region PESQUISA PELOS EVENTOS
        private void rabNumerico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabNumerico.Checked == true)
            {
                //EXECUTA A PESQUISA
                TabSetor_Pesquisa PESQ = new TabSetor_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabSetor, comPesStatus, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void rabAlfabetico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabAlfabetico.Checked == true)
            {
                //EXECUTA A PESQUISA
                TabSetor_Pesquisa PESQ = new TabSetor_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabSetor, comPesStatus, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void rabTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTodos.Checked == true)
            {
                //EXECUTA A PESQUISA
                TabSetor_Pesquisa PESQ = new TabSetor_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabSetor, comPesStatus, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void rabTOP_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTOP.Checked == true)
            {
                nupQtResultados.Enabled = true;
                //EXECUTA A PESQUISA
                TabSetor_Pesquisa PESQ = new TabSetor_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabSetor, comPesStatus, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
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
                TabSetor_Pesquisa PESQ = new TabSetor_Pesquisa();
                PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabSetor, comPesStatus, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
                txtPesDescri.Select(); txtPesDescri.SelectAll();
            }
        }
        private void comPesStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabSetor_Pesquisa PESQ = new TabSetor_Pesquisa();
            PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabSetor, comPesStatus, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
            txtPesDescri.Select(); txtPesDescri.SelectAll();
        }
        private void txtPesDescri_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            TabSetor_Pesquisa PESQ = new TabSetor_Pesquisa();
            PESQ.Pesc_EXECUTAR(Dgv_Pesquisa, _Login_LojaID_TabSetor, comPesStatus, txtPesDescri, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados);
        }
        #endregion

        //SELECIONA O ITEM DA PESQUISA
        private void Dgv_Pesquisa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string SET = Dgv_Pesquisa.CurrentRow.Cells[0].Value.ToString().Substring(0, 3);
                string SUB = Dgv_Pesquisa.CurrentRow.Cells[0].Value.ToString().Substring(3, 4);
                TabSetor_AppaButtons Appa = new TabSetor_AppaButtons();
                Appa._ButtonZER(txtMESTRE, txtSetCod, btnIncluir, btnGravar, CamposDisable, TabControl, Tp1);
                txtSetCod.Text = SET;
                txtSetCod.Select();
                SendKeys.Send("{TAB}");

                txtSubCod.Text = SUB;
                SendKeys.Send("{TAB}");
            }
        }

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
        public string NomeDoArquivo = "TabSetor";

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
        private void comImpTipo_SelectedIndexChanged(object sender, EventArgs e)
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
                TabSetor_Lixeira Lixeira = new TabSetor_Lixeira();
                Lixeira.Lix_RESTAURAR(Dgv_Lixeira, cheVoltarLix, TabControl, Tp1, txtUsuario, btnGravar, txtMESTRE, CamposDisable, txtSubCod,txtSetCod, btnIncluir);
            }
        }
        #endregion

        #region HISTÓRICO
        //FAZ A PESQUISA PELO INDEX
        private void comIDHis_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabSetor_Histórico HIST = new TabSetor_Histórico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, comIDHis, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
        }
        //FAZ A PESQUISA PELO BUTTON
        private void btnHistorico_Click(object sender, EventArgs e)
        {
            TabSetor_Histórico HIST = new TabSetor_Histórico();
            HIST.HIS_PopularHISTORICO(txtUsuarioHis, btnHistorico, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
        }



        //VERIFICA SE A DATA É VÁLIDA
        private void mtbData1His_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                //VERIFICA SE A DATA É VALIDA
                TabSetor_Histórico HIS = new TabSetor_Histórico();
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
                TabSetor_Histórico HIS = new TabSetor_Histórico();
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
                        TabSetor_Histórico HIST = new TabSetor_Histórico();
                        HIST.HIS_VerificaUsuarioEXISTE(txtUsuarioHis, mtbData2His, Dgv_Histórico, mtbData1His, mtbData2His, comIDHis, panUpAb5);
                    }
                }
            }
        }
        //APENAS NÚMEROS
        private void txtUsuarioHis_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabSetor_MET MET = new TabSetor_MET();
            MET.MET_ApenasNúmeros(e);
        }
        //ABRE A PESQUISA DO USUÁRIO
        private void txtUsuarioHis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                PesUsuar.cs.PesUsuar_CALL Call = new PesUsuar.cs.PesUsuar_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabSetor;
                Call._Login_CryptDesc = _Login_UsuarioID_TabSetor;
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












    }
}
