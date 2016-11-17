using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TabEmpre
{
    public partial class TabEmpre : Form
    {
        public TabEmpre()
        {
            InitializeComponent();
        }

        public string _Login_UsuarioID_TabEmpre
        { get; set; }
        public string _Login_LojaID_TabEmpre
        { get; set; }

        //Techas de Atalho
        private void TabEmpre_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    btnINC.PerformClick();
                    break;
                case Keys.F2:
                    btnALT.PerformClick();
                    break;
                case Keys.F3:
                    break;
                case Keys.F4:
                    btnAjuda.PerformClick();
                    break;
                case Keys.F5:
                    break;
                case Keys.F6:
                    break;
                case Keys.F7:
                    btnFechar.PerformClick();
                    btnFechar2.PerformClick();
                    break;
                case Keys.F8:
                    break;
                case Keys.F9:
                    btnZerar.PerformClick();
                    btnRetorna.PerformClick();
                    break;
                case Keys.F10:
                    btnGravar.PerformClick();
                    break;
                case Keys.F11:
                    break;
                case Keys.F12:
                    btnAvanca.PerformClick();
                    break;
            }
        }
        
        //Loado do Form
        private void TabEmpre_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            //Deixa os campos inativos
            CamposINATIV();
            //Define se é para gravar os filtros
            TabEmpre_FILTROs FILT = new TabEmpre_FILTROs();
            FILT.cheFILTROSChecked(cheFILTROSImp, cheFILTROSPes, _Login_LojaID_TabEmpre);
            //Popula os filtros
            FILT.CarregaFILTROS(rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comSituacaoPES, rabRPV, rabWORD, rabEXCEL, rabTXT, comSituacaoIMP, rabOrdemNumerica, rabOrdemAlfabeticaRaz, cheFILTROSImp, cheFILTROSPes, comModuloIMP, comTipoDeVendaIMP, rabOrdemAlfabeticaFan, txtCaminhoRel, comAtividadePES, comModuloPES);
            //Verifica permissão dos buttons
            TabEmpre_Permissão Permi = new TabEmpre_Permissão();
            Permi.VerificaPermi_Buttons(btnINC, btnALT, btnSet1, btnSet2, btnSet3, btnSet4, txtCodigo, _Login_UsuarioID_TabEmpre);
            //Seleciona a cor de fundo do formulario DOWN
            TabEmpre_Métodos MET = new TabEmpre_Métodos();
            MET.SelecionaCorFundo(Painel_Cor1, Painel_Cor2, Painel_Cor3, Painel_Cor4, mtbCpfCnpj, _Login_LojaID_TabEmpre);
            //Exibe os dados SecF da Loja Logada
            MET.SelecionaSecFLogado(_Login_LojaID_TabEmpre, txtCodigo, txtDescri, comTipo, mtbCpfCnpj, txtUTF8);
            //Preenche o txtUsuario com o usuário logado
            txtUsuario.Text = _Login_UsuarioID_TabEmpre.PadLeft(6, '0');
            //Popula as informações do formulario dando um SendKeys TAB
            #region Popula as Informações
            txtCodigo.Select();
            SendKeys.Send("{TAB}");
            #endregion
        }
        //Permissão por abas
        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {


        }
        //Aplica as propriedades ao se trocar d ABA
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Aba 1
            if (TabControl.SelectedTab == Tp_1)
            {
                if (TXT_MESTRE.Text == "PESQUISA" || TXT_MESTRE.Text == "IMPRESSÃO")
                {
                    TXT_MESTRE.Text = "SELECT";
                    TXT_MESTRE.BackColor = Color.Silver;
                    TXT_MESTRE.ForeColor = Color.Black;
                    ZerarCampos();
                    CamposINATIV();
                    txtCodigo.Text = string.Empty;
                    btnALT.Select();
                }
                else
                {
                    txtCodigo.Select(); txtCodigo.SelectAll();
                }
            }
                #endregion
            #region Aba 2
            if (TabControl.SelectedTab == Tp_2)
            {
                if (TXT_MESTRE.Text == "PESQUISA" || TXT_MESTRE.Text == "IMPRESSÃO")
                {
                    TabControl.SelectedTab = Tp_1;
                }
                else
                {
                    txtCfop.Select(); txtCfop.SelectAll();
                }
            }
            #endregion
            #region Aba 3
            if (TabControl.SelectedTab == Tp_3)
            {
                ZerarCampos();
                CamposINATIV();
                #region TRATAMENTOS
                if (rabAlfabetico.Checked == false && rabNumerico.Checked == false)
                {
                    rabAlfabetico.Checked = true;
                }
                if (rabTodos.Checked == false && rabTOP.Checked == false)
                {
                    rabTodos.Checked = true;
                }
                if (comSituacaoPES.SelectedIndex < 0)
                {
                    comSituacaoPES.SelectedIndex = 3;
                }
                if (comAtividadePES.SelectedIndex < 0)
                {
                    comAtividadePES.SelectedIndex = 7;
                }
                if (comModuloPES.SelectedIndex < 0)
                {
                    comModuloPES.SelectedIndex = 5;
                }
                if (nupQtResultados.Value == 0 && nupQtResultados.Enabled == true)
                {
                    nupQtResultados.Value = 20;
                }
                #endregion
                comSituacaoPES.SelectedIndexChanged -= new EventHandler(comSituacaoPES_SelectedIndexChanged);
                txtRazaoPES.TextChanged -= new EventHandler(txtRazaoPES_TextChanged);
                comAtividadePES.SelectedIndexChanged -= new EventHandler(comAtividadePES_SelectedIndexChanged);
                comModuloPES.SelectedIndexChanged -= new EventHandler(comModuloPES_SelectedIndexChanged);
                txtFantasiaPES.TextChanged -= new EventHandler(txtFantasiaPES_TextChanged);

                txtRazaoPES.Text = string.Empty; txtRazaoPES.Select();
                txtFantasiaPES.Text = string.Empty;
                Dgv_Pesquisa.Rows.Clear();
                TXT_MESTRE.Text = "PESQUISA";
                TXT_MESTRE.BackColor = Color.Salmon;
                TXT_MESTRE.ForeColor = Color.Green;

                comSituacaoPES.SelectedIndexChanged += new EventHandler(comSituacaoPES_SelectedIndexChanged);
                txtRazaoPES.TextChanged += new EventHandler(txtRazaoPES_TextChanged);
                comAtividadePES.SelectedIndexChanged += new EventHandler(comAtividadePES_SelectedIndexChanged);
                comModuloPES.SelectedIndexChanged += new EventHandler(comModuloPES_SelectedIndexChanged);
                txtFantasiaPES.TextChanged += new EventHandler(txtFantasiaPES_TextChanged);

                btnAvanca.Enabled = false;
                btnGravar.Enabled = false;
            }
            #endregion
            #region Aba 4
            if (TabControl.SelectedTab == Tp_4)
            {
                ZerarCampos();
                CamposINATIV();
                TXT_MESTRE.Text = "IMPRESSÃO";
                TXT_MESTRE.BackColor = Color.Gold;
                TXT_MESTRE.ForeColor = Color.Red;
                btnAvanca.Enabled = false;
                btnGravar.Enabled = false;
            }
            #endregion
        }


        //Verifica se está tudo OK antes de mudar de aba
        private void TabControl_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (TabControl.SelectedTab == Tp_1 && TXT_MESTRE.Text == "ALTERAR")
            {
                e.Cancel = true;
                #region Verifica as Datas e Campos Obrigatórios
                TabEmpre_Métodos MET = new TabEmpre_Métodos();
                bool Verifica1 = MET.VerificaDATA(mtbVencEst, mtbVencEst);
                if (!Verifica1) { } else { return; }
                bool Verifica2 = MET.VerificaDATA(mtbVencMun, mtbVencMun);
                if (!Verifica2) { } else { return; }

                TabEmpre_CamposObrig Obr = new TabEmpre_CamposObrig();
                bool Obrig = Obr.CamposObrig(TXT_MESTRE, btnAvanca, txtCodigo, txtDescri, txtFantasia, txtResponsavel, comTipoFaturamento, txtEndLogradouro, txtEndNumero, txtEndCidade, txtEndCidDescri, txtEndCidUF, txtEndBairro, txtInscricaoEstadual, mtbVencEst);
                if (!Obrig) { } else { return; }
                #endregion
                e.Cancel = false;
            }
            else
            {
            }
        }

        //Grava o XML com os filtros
        private void TabEmpre_FormClosed(object sender, FormClosedEventArgs e)
        {
            TabEmpre_FILTROs FILT = new TabEmpre_FILTROs();
            FILT.GravarFILTROS(rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comSituacaoPES, rabRPV, rabWORD, rabEXCEL, rabTXT, comSituacaoIMP, rabOrdemNumerica, rabOrdemAlfabeticaRaz, cheFILTROSImp, cheFILTROSPes, comModuloIMP, comTipoDeVendaIMP, rabOrdemAlfabeticaFan, txtCaminhoRel, comAtividadePES, comModuloPES);
        }


        #region Apenas Números
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabEmpre_Métodos MET = new TabEmpre_Métodos();
            MET.ApenasNúmeros(e);
        }

        private void txtEndCidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabEmpre_Métodos MET = new TabEmpre_Métodos();
            MET.ApenasNúmeros(e);
        }

        private void txtCfop_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabEmpre_Métodos MET = new TabEmpre_Métodos();
            MET.ApenasNúmeros(e);
        }

        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabEmpre_Métodos MET = new TabEmpre_Métodos();
            MET.ApenasNúmeros(e);
        }

        private void txtCaixas_KeyPress(object sender, KeyPressEventArgs e)
        {
            TabEmpre_Métodos MET = new TabEmpre_Métodos();
            MET.ApenasNúmeros(e);
        }
        #endregion



        #region FORM
        /// <summary> REFERENTE A FORM
        ///
        /// 
        ///</summary> ------------------------------------------------------------------------------

        public void ZerarCampos()
        {
            #region Painel_Codigo
            mtbCpfCnpj.Text = string.Empty;
            txtUTF8.Text = string.Empty;
            foreach (Control ctrl in Painel_Codigo.Controls)
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
            #region Painel_Infor
            foreach (Control ctrl in Painel_Infor.Controls)
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
            #region Painel_Endereco
            foreach (Control ctrl in Painel_Endereco.Controls)
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
            #region Painel_Telefones
            foreach (Control ctrl in Painel_Telefones.Controls)
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
            #region Painel_Inscricao
            foreach (Control ctrl in Painel_Inscricao.Controls)
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
            #region grbInformacoesSistema
            foreach (Control ctrl in grbInformacoesSistema.Controls)
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
            #region Painel_Faturamento1
            foreach (Control ctrl in Painel_Faturamento1.Controls)
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
            #region Painel_Faturamento2
            foreach (Control ctrl in Painel_Faturamento2.Controls)
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
            #region grbInformacoesAutomacao
            foreach (Control ctrl in grbInformacoesAutomacao.Controls)
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
            #region Painel_Cor2
            foreach (Control ctrl in Painel_Cor2.Controls)
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
            btnMarcar.Text = "MARCAR";
        }
        public void CamposINATIV()
        {
            #region Painel_Codigo
            foreach (Control ctrl in Painel_Codigo.Controls)
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
            #region Painel_Infor
            foreach (Control ctrl in Painel_Infor.Controls)
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
            #region Painel_Endereco
            foreach (Control ctrl in Painel_Endereco.Controls)
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
            #region Painel_Telefones
            foreach (Control ctrl in Painel_Telefones.Controls)
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
            #region Painel_Inscricao
            foreach (Control ctrl in Painel_Inscricao.Controls)
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
            #region grbInformacoesSistema
            foreach (Control ctrl in grbInformacoesSistema.Controls)
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
            #region Painel_Faturamento1
            foreach (Control ctrl in Painel_Faturamento1.Controls)
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
            #region Painel_Faturamento2
            foreach (Control ctrl in Painel_Faturamento2.Controls)
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
            #region grbInformacoesAutomacao
            foreach (Control ctrl in grbInformacoesAutomacao.Controls)
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
            }
            #endregion
            #region Painel_Cor2
            foreach (Control ctrl in Painel_Cor2.Controls)
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
            }
            #endregion
        }
        public void CamposATIVOS()
        {
            #region Painel_Codigo
            foreach (Control ctrl in Painel_Codigo.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox) && ctrl.Name != "txtCodigo" && ctrl.Name != "txtDescri")
                {
                    (ctrl as TextBox).Enabled = true;
                }
                if (ctrl.GetType() == typeof(ComboBox) && ctrl.Name != "comTipo")
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
            #region Painel_Infor
            foreach (Control ctrl in Painel_Infor.Controls)
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
            #region Painel_Endereco
            foreach (Control ctrl in Painel_Endereco.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox) && ctrl.Name != "txtEndCidDescri" && ctrl.Name != "txtEndCidUF")
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
            #region Painel_Telefones
            foreach (Control ctrl in Painel_Telefones.Controls)
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
            #region Painel_Inscricao
            foreach (Control ctrl in Painel_Inscricao.Controls)
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
            #region grbInformacoesSistema
            foreach (Control ctrl in grbInformacoesSistema.Controls)
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
            #region Painel_Faturamento1
            foreach (Control ctrl in Painel_Faturamento1.Controls)
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
            #region Painel_Faturamento2
            foreach (Control ctrl in Painel_Faturamento2.Controls)
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
            #region grbInformacoesAutomacao
            foreach (Control ctrl in grbInformacoesAutomacao.Controls)
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
                if (ctrl.GetType() == typeof(NumericUpDown))
                {
                    (ctrl as NumericUpDown).Enabled = true;
                }
                if (ctrl.GetType() == typeof(Button))
                {
                    (ctrl as Button).Enabled = true;
                }
            }
            #endregion
            #region Painel_Cor2
            foreach (Control ctrl in Painel_Cor2.Controls)
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
                if (ctrl.GetType() == typeof(NumericUpDown))
                {
                    (ctrl as NumericUpDown).Enabled = true;
                }
            }
            #endregion
        }

        #region SelectAll no Click
        private void txtCfop_Click(object sender, EventArgs e)
        {
            txtCfop.SelectAll();
        }
        private void txtSerie_Click(object sender, EventArgs e)
        {
            txtSerie.SelectAll();
        }
        private void txtModelo_Click(object sender, EventArgs e)
        {
            txtModelo.SelectAll();
        }
        private void txtMsg_Click(object sender, EventArgs e)
        {
            txtMsg.SelectAll();
        }
        private void txtCaixas_Click(object sender, EventArgs e)
        {
            txtCaixas.SelectAll();
        }
        private void txtEspecie_Click(object sender, EventArgs e)
        {
            txtEspecie.SelectAll();
        }
        private void txtAproveitamento_Click(object sender, EventArgs e)
        {
            txtAproveitamento.SelectAll();
        }
        private void txtCodigo_Click(object sender, EventArgs e)
        {
            txtCodigo.SelectAll();
        }
        private void txtEndCidade_MouseDown(object sender, MouseEventArgs e)
        {
            txtEndCidade.SelectAll();
            txtEndCidDescri.Text = string.Empty;
            txtEndCidUF.Text = string.Empty;
        }
        private void mtbPabx_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbFax_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbVencEst_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        private void mtbVencMun_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }
        #endregion

        #region Buttons INCLUIR e ALTERAR
        private void btnINC_Click(object sender, EventArgs e)
        {
            ZerarCampos();
            CamposINATIV();
            btnAvanca.Enabled = false;
            btnGravar.Enabled = false;
            txtCodigo.Select(); txtCodigo.SelectAll();
            //Chama o executavel de add lojas
            TabEmpre_Gravar Gravar = new TabEmpre_Gravar();
            Gravar.btnINC(TXT_MESTRE,ZerarCampos);
        }
        private void btnALT_Click(object sender, EventArgs e)
        {
            ZerarCampos();
            btnAvanca.Enabled = false;
            //Aplica a aparecia do TXT MESTRE
            TabEmpre_ApareButtons Apa = new TabEmpre_ApareButtons();
            Apa._ButtonALT(TXT_MESTRE, btnGravar, txtCodigo, CamposINATIV, TabControl, Tp_1);
        }
        #endregion

        #region Buttons SETA 1,2,3 e 4
        private void btnSet1_Click(object sender, EventArgs e)
        {
            TabEmpre_ApareButtons Apa = new TabEmpre_ApareButtons();
            Apa._ButtonSETAS(TXT_MESTRE, btnGravar, CamposINATIV, TabControl, Tp_1);
            TabEmpre_ExecSETAs Exec = new TabEmpre_ExecSETAs();
            Exec.ExecSETAs("1", TXT_MESTRE, mtbCpfCnpj, txtCodigo, txtDescri, comTipo, txtFantasia, txtResponsavel, txtEndLogradouro, txtEndNumero, txtEndCidade, txtEndCidDescri, txtEndCidUF, mtbEndCep, txtEndBairro, txtEndComple, mtbPabx, mtbFax, txtEmail, txtHomePage, txtInscricaoEstadual, txtInscricaoMunicipal, mtbVencEst, mtbVencMun, comRegimeTrib, txtUTF8, txtUsuario, comAtividade, comIcmsRdz, comIcmsST, comTipoFaturamento, comSitEmpresa, comModuloSistema, txtCfop, txtSerie, txtModelo, txtMsg, txtCaixas, txtEspecie, txtAproveitamento, txtObsLivro, comLote, comFrete, comCondicao, comRecebimento, comConsulta, comTipoDeVenda, comEstoque, comFinanceiro, comJuros, cheEstNegativoNot, cheEstNegativoPed, chePedVend, chePedComp, cheSenhaLimite, cheGeraFinanc, cheSenhaDesc, cheBloqueiaVendaInsc, cheVendedorObrig, cheEmiteNota, cheAutorizaPedV, cheAutorizaPedC, cheAutorizaCtsPag, cheDataAutomatica, cheExcluiOrca, cheExcluiHist, cheDescAnte, cheTabelaPrecoVista, nupOrcamento, nupHistorico, nupAntecipado, nupTabelaVista, nupTabelaPrazo, nupJuros, Painel_Codigo, ZerarCampos, CamposINATIV, CamposATIVOS, btnGravar, btnAvanca);
            btnSet1.Select();
        }

        private void btnSet2_Click(object sender, EventArgs e)
        {
            TabEmpre_ApareButtons Apa = new TabEmpre_ApareButtons();
            Apa._ButtonSETAS(TXT_MESTRE, btnGravar, CamposINATIV, TabControl, Tp_1);
            TabEmpre_ExecSETAs Exec = new TabEmpre_ExecSETAs();
            Exec.ExecSETAs("2", TXT_MESTRE, mtbCpfCnpj, txtCodigo, txtDescri, comTipo, txtFantasia, txtResponsavel, txtEndLogradouro, txtEndNumero, txtEndCidade, txtEndCidDescri, txtEndCidUF, mtbEndCep, txtEndBairro, txtEndComple, mtbPabx, mtbFax, txtEmail, txtHomePage, txtInscricaoEstadual, txtInscricaoMunicipal, mtbVencEst, mtbVencMun, comRegimeTrib, txtUTF8, txtUsuario, comAtividade, comIcmsRdz, comIcmsST, comTipoFaturamento, comSitEmpresa, comModuloSistema, txtCfop, txtSerie, txtModelo, txtMsg, txtCaixas, txtEspecie, txtAproveitamento, txtObsLivro, comLote, comFrete, comCondicao, comRecebimento, comConsulta, comTipoDeVenda, comEstoque, comFinanceiro, comJuros, cheEstNegativoNot, cheEstNegativoPed, chePedVend, chePedComp, cheSenhaLimite, cheGeraFinanc, cheSenhaDesc, cheBloqueiaVendaInsc, cheVendedorObrig, cheEmiteNota, cheAutorizaPedV, cheAutorizaPedC, cheAutorizaCtsPag, cheDataAutomatica, cheExcluiOrca, cheExcluiHist, cheDescAnte, cheTabelaPrecoVista, nupOrcamento, nupHistorico, nupAntecipado, nupTabelaVista, nupTabelaPrazo, nupJuros, Painel_Codigo, ZerarCampos, CamposINATIV, CamposATIVOS, btnGravar, btnAvanca);
            btnSet2.Select();
        }

        private void btnSet3_Click(object sender, EventArgs e)
        {
            TabEmpre_ApareButtons Apa = new TabEmpre_ApareButtons();
            Apa._ButtonSETAS(TXT_MESTRE, btnGravar, CamposINATIV, TabControl, Tp_1);
            TabEmpre_ExecSETAs Exec = new TabEmpre_ExecSETAs();
            Exec.ExecSETAs("3", TXT_MESTRE, mtbCpfCnpj, txtCodigo, txtDescri, comTipo, txtFantasia, txtResponsavel, txtEndLogradouro, txtEndNumero, txtEndCidade, txtEndCidDescri, txtEndCidUF, mtbEndCep, txtEndBairro, txtEndComple, mtbPabx, mtbFax, txtEmail, txtHomePage, txtInscricaoEstadual, txtInscricaoMunicipal, mtbVencEst, mtbVencMun, comRegimeTrib, txtUTF8, txtUsuario, comAtividade, comIcmsRdz, comIcmsST, comTipoFaturamento, comSitEmpresa, comModuloSistema, txtCfop, txtSerie, txtModelo, txtMsg, txtCaixas, txtEspecie, txtAproveitamento, txtObsLivro, comLote, comFrete, comCondicao, comRecebimento, comConsulta, comTipoDeVenda, comEstoque, comFinanceiro, comJuros, cheEstNegativoNot, cheEstNegativoPed, chePedVend, chePedComp, cheSenhaLimite, cheGeraFinanc, cheSenhaDesc, cheBloqueiaVendaInsc, cheVendedorObrig, cheEmiteNota, cheAutorizaPedV, cheAutorizaPedC, cheAutorizaCtsPag, cheDataAutomatica, cheExcluiOrca, cheExcluiHist, cheDescAnte, cheTabelaPrecoVista, nupOrcamento, nupHistorico, nupAntecipado, nupTabelaVista, nupTabelaPrazo, nupJuros, Painel_Codigo, ZerarCampos, CamposINATIV, CamposATIVOS, btnGravar, btnAvanca);
            btnSet3.Select();
        }

        private void btnSet4_Click(object sender, EventArgs e)
        {
            TabEmpre_ApareButtons Apa = new TabEmpre_ApareButtons();
            Apa._ButtonSETAS(TXT_MESTRE, btnGravar, CamposINATIV, TabControl, Tp_1);
            TabEmpre_ExecSETAs Exec = new TabEmpre_ExecSETAs();
            Exec.ExecSETAs("4", TXT_MESTRE, mtbCpfCnpj, txtCodigo, txtDescri, comTipo, txtFantasia, txtResponsavel, txtEndLogradouro, txtEndNumero, txtEndCidade, txtEndCidDescri, txtEndCidUF, mtbEndCep, txtEndBairro, txtEndComple, mtbPabx, mtbFax, txtEmail, txtHomePage, txtInscricaoEstadual, txtInscricaoMunicipal, mtbVencEst, mtbVencMun, comRegimeTrib, txtUTF8, txtUsuario, comAtividade, comIcmsRdz, comIcmsST, comTipoFaturamento, comSitEmpresa, comModuloSistema, txtCfop, txtSerie, txtModelo, txtMsg, txtCaixas, txtEspecie, txtAproveitamento, txtObsLivro, comLote, comFrete, comCondicao, comRecebimento, comConsulta, comTipoDeVenda, comEstoque, comFinanceiro, comJuros, cheEstNegativoNot, cheEstNegativoPed, chePedVend, chePedComp, cheSenhaLimite, cheGeraFinanc, cheSenhaDesc, cheBloqueiaVendaInsc, cheVendedorObrig, cheEmiteNota, cheAutorizaPedV, cheAutorizaPedC, cheAutorizaCtsPag, cheDataAutomatica, cheExcluiOrca, cheExcluiHist, cheDescAnte, cheTabelaPrecoVista, nupOrcamento, nupHistorico, nupAntecipado, nupTabelaVista, nupTabelaPrazo, nupJuros, Painel_Codigo, ZerarCampos, CamposINATIV, CamposATIVOS, btnGravar, btnAvanca);
            btnSet4.Select();
        }
        #endregion

        #region Abre Pesquisas
        //Abre a pesquisa de Cidades
        private void txtEndCidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                if (txtEndCidade.Text != string.Empty)
                {
                    txtEndCidade.Text = txtEndCidade.Text.PadLeft(6, '0');
                }

                PesCidad.cs.PesCidad_CALL Call = new PesCidad.cs.PesCidad_CALL();
                Call._Login_CryptCode = _Login_LojaID_TabEmpre;
                Call._Login_CryptDesc = _Login_UsuarioID_TabEmpre;
                Call._WenCrypt = "PesCidad3Wenemy3156!.350?°";
                Call.PesCidad_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtEndCidade.Text = Call._ResultPesquisaCALL;
                }

                txtEndCidade.SelectAll();
            }
        }
        //Abre a pesquisa de CFOP
        private void txtCfop_KeyDown(object sender, KeyEventArgs e)
        {

        }
        //Abre a pesquisa de Obs para nota
        private void txtMsg_KeyDown(object sender, KeyEventArgs e)
        {

        }
        //Abre a pesquisa de Empresas
        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {

        }
        #endregion

        //Seleciona a mascara do CPF CNPJ
        private void comTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comTipo.SelectedIndex == 1)
            {
                mtbCpfCnpj.Mask = "000,000,000-00";
            }
            else if (comTipo.SelectedIndex == 0)
            {
                mtbCpfCnpj.Mask = "00,000,000/0000-00";
            }
            else
            {
                mtbCpfCnpj.Mask = "";
            }
        }

        #region Propriedades CheckedChanged e SelectIndex com Propriedades
        private void cheExcluiOrca_CheckedChanged(object sender, EventArgs e)
        {
            if (cheExcluiOrca.Checked == true)
            {
                nupOrcamento.Visible = true;
                lblSemanas.Visible = true;
                cheExcluiOrca.Text = "EXCLUSÃO AUTOMÁTICA DE ORÇAMENTOS NÃO FATURADOS..:";
            }
            else
            {
                nupOrcamento.Visible = false;
                lblSemanas.Visible = false;
                nupOrcamento.Value = 0;
                cheExcluiOrca.Text = "EXCLUSÃO AUTOMÁTICA DE ORÇAMENTOS NÃO FATURADOS";
            }
        }
        private void cheExcluiHist_CheckedChanged(object sender, EventArgs e)
        {
            if (cheExcluiHist.Checked == true)
            {
                cheExcluiHist.Text = "EXCLUSÃO AUTOMÁTICA DO HISTÓRICO DO SOFTWARE.....:";
                nupHistorico.Visible = true;
                lblMeses.Visible = true;
            }
            else
            {
                cheExcluiHist.Text = "EXCLUSÃO AUTOMÁTICA DO HISTÓRICO DO SOFTWARE";
                nupHistorico.Visible = false;
                nupHistorico.Value = 0;
                lblMeses.Visible = false;
            }
        }
        private void cheDescAnte_CheckedChanged(object sender, EventArgs e)
        {
            if (cheDescAnte.Checked == true)
            {
                cheDescAnte.Text = "DESCONTO PARA PAGAMENTO ANTECIPADO...............:";
                nupAntecipado.Visible = true;
                lblPorcentagem.Visible = true;
            }
            else
            {
                cheDescAnte.Text = "DESCONTO PARA PAGAMENTO ANTECIPADO";
                nupAntecipado.Visible = false;
                nupAntecipado.Value = 0;
                lblPorcentagem.Visible = false;
            }
        }
        private void cheTabelaPrecoVista_CheckedChanged(object sender, EventArgs e)
        {
            if (cheTabelaPrecoVista.Checked == true)
            {
                cheTabelaPrecoVista.Text = "TABELA DE PREÇO DE VENDA A VISTA E A PRAZO.......:";
                nupTabelaVista.Visible = true;
                nupTabelaPrazo.Visible = true;
            }
            else
            {
                cheTabelaPrecoVista.Text = "TABELA DE PREÇO DE VENDA A VISTA E A PRAZO";
                nupTabelaVista.Visible = false;
                nupTabelaPrazo.Visible = false;
                nupTabelaVista.Value = 1;
                nupTabelaPrazo.Value = 2;
            }
        }
        private void comJuros_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comJuros.SelectedIndex == 2 || comJuros.SelectedIndex == -1)
            {
                lblJuros.Text = "JUROS";
                nupJuros.Visible = false;
                nupJuros.Value = 0;
            }
            else
            {
                lblJuros.Text = "JUROS........VALOR(%)";
                nupJuros.Visible = true;
            }
        }
        private void comCondicao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comCondicao.SelectedIndex == 0)
            {
                comRecebimento.SelectedIndex = 0;
            }
        }
        #endregion

        #region Buttons Marcar, Gravar, Zerar, Avançar, Retornar e Fechar 1 e 2
        private void btnMarcar_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnMarcar.Text == "MARCAR")
                {
                    foreach (Control Che in grbInformacoesAutomacao.Controls)
                    {
                        if (Che.GetType() == typeof(CheckBox))
                        {
                            (Che as CheckBox).Checked = true;
                        }
                    }
                    btnMarcar.Text = "DESMARCAR";
                    return;
                }
                if (btnMarcar.Text == "DESMARCAR")
                {
                    foreach (Control Che in grbInformacoesAutomacao.Controls)
                    {
                        if (Che.GetType() == typeof(CheckBox))
                        {
                            (Che as CheckBox).Checked = false;
                        }
                    }
                    btnMarcar.Text = "MARCAR";
                    return;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            TabEmpre_Gravar Grava = new TabEmpre_Gravar();
            Grava.btnALT(TXT_MESTRE, mtbCpfCnpj, txtCodigo, txtDescri, comTipo, txtFantasia, txtResponsavel, txtEndLogradouro, txtEndNumero, txtEndCidade, txtEndCidDescri, txtEndCidUF, mtbEndCep, txtEndBairro, txtEndComple, mtbPabx, mtbFax, txtEmail, txtHomePage, txtInscricaoEstadual, txtInscricaoMunicipal, mtbVencEst, mtbVencMun, comRegimeTrib, txtUTF8, txtUsuario, comAtividade, comIcmsRdz, comIcmsST, comTipoFaturamento, comSitEmpresa, comModuloSistema, txtCfop, txtSerie, txtModelo, txtMsg, txtCaixas, txtEspecie, txtAproveitamento, txtObsLivro, comLote, comFrete, comCondicao, comRecebimento, comConsulta, comTipoDeVenda, comEstoque, comFinanceiro, comJuros, cheEstNegativoNot, cheEstNegativoPed, chePedVend, chePedComp, cheSenhaLimite, cheGeraFinanc, cheSenhaDesc, cheBloqueiaVendaInsc, cheVendedorObrig, cheEmiteNota, cheAutorizaPedV, cheAutorizaPedC, cheAutorizaCtsPag, cheDataAutomatica, cheExcluiOrca, cheExcluiHist, cheDescAnte, cheTabelaPrecoVista, nupOrcamento, nupHistorico, nupAntecipado, nupTabelaVista, nupTabelaPrazo, nupJuros, ZerarCampos, CamposINATIV, btnGravar, btnAvanca);
            TabControl.SelectedTab = Tp_1;
        }

        private void btnZerar_Click(object sender, EventArgs e)
        {
            TabEmpre_ApareButtons Apa = new TabEmpre_ApareButtons();
            Apa._ButtonZER(TXT_MESTRE, txtCodigo, btnINC, btnGravar, CamposINATIV, TabControl, Tp_1);
            ZerarCampos();
            CamposINATIV();
        }

        private void btnAvanca_Click(object sender, EventArgs e)
        {
            #region Verifica as Datas e Campos Obrigatórios
            TabEmpre_Métodos MET = new TabEmpre_Métodos();
            bool Verifica1 = MET.VerificaDATA(mtbVencEst, mtbVencEst);
            if (!Verifica1) { } else { return; }
            bool Verifica2 = MET.VerificaDATA(mtbVencMun, mtbVencMun);
            if (!Verifica2) { } else { return; }
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");

            TabEmpre_CamposObrig Obr = new TabEmpre_CamposObrig();
            bool Obrig = Obr.CamposObrig(TXT_MESTRE, btnAvanca, txtCodigo, txtDescri, txtFantasia, txtResponsavel, comTipoFaturamento, txtEndLogradouro, txtEndNumero, txtEndCidade, txtEndCidDescri, txtEndCidUF, txtEndBairro,txtInscricaoEstadual,mtbVencEst);
            if (!Obrig) { } else { return; }
            #endregion

            TabControl.SelectedTab = Tp_2;
        }

        private void btnRetorna_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = Tp_1;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFechar2_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion


        //Seleciona dados no TAB
        private void txtCodigo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                TabEmpre_Métodos MET = new TabEmpre_Métodos();
                MET.SelecionarCodigoDigitarTAB(TXT_MESTRE, mtbCpfCnpj, txtCodigo, txtDescri, comTipo, txtFantasia, txtResponsavel, txtEndLogradouro, txtEndNumero, txtEndCidade, txtEndCidDescri, txtEndCidUF, mtbEndCep, txtEndBairro, txtEndComple, mtbPabx, mtbFax, txtEmail, txtHomePage, txtInscricaoEstadual, txtInscricaoMunicipal, mtbVencEst, mtbVencMun, comRegimeTrib, txtUTF8, txtUsuario, comAtividade, comIcmsRdz, comIcmsST, comTipoFaturamento, comSitEmpresa, comModuloSistema, txtCfop, txtSerie, txtModelo, txtMsg, txtCaixas, txtEspecie, txtAproveitamento, txtObsLivro, comLote, comFrete, comCondicao, comRecebimento, comConsulta, comTipoDeVenda, comEstoque, comFinanceiro, comJuros, cheEstNegativoNot, cheEstNegativoPed, chePedVend, chePedComp, cheSenhaLimite, cheGeraFinanc, cheSenhaDesc, cheBloqueiaVendaInsc, cheVendedorObrig, cheEmiteNota, cheAutorizaPedV, cheAutorizaPedC, cheAutorizaCtsPag, cheDataAutomatica, cheExcluiOrca, cheExcluiHist, cheDescAnte, cheTabelaPrecoVista, nupOrcamento, nupHistorico, nupAntecipado, nupTabelaVista, nupTabelaPrazo, nupJuros, Painel_Codigo, ZerarCampos, CamposINATIV, CamposATIVOS, btnGravar, btnAvanca);
            }
        }




        //Busca os dados da cidade
        private void txtEndCidade_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtEndCidade.Text = txtEndCidade.Text.PadLeft(6, '0');
                TabEmpre_Métodos MET = new TabEmpre_Métodos();
                MET.SelecionaCidade(txtEndCidade, txtEndCidDescri, txtEndCidUF, mtbEndCep, txtEndNumero);
            }
        }
        //Busca o CFOP
        private void txtCfop_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }
        //Busca a msg do rodape
        private void txtMsg_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }
        //Aplica a mascara decimal
        private void txtAproveitamento_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtAproveitamento.Text = Convert.ToDecimal(txtAproveitamento.Text).ToString("0.000");
            }
        }




        #region VALIDA DATAS NO TAB
        private void mtbVencEst_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (mtbVencEst.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {

                }
                else
                {
                    try
                    {
                        string MeuTEXTO = mtbVencEst.Text;
                        string MeuAno = DateTime.Now.Year.ToString();
                        if (mtbVencEst.Text.Length <= 6)
                        {
                            mtbVencEst.Text = MeuTEXTO + MeuAno;
                        }
                        TabEmpre_Métodos MET = new TabEmpre_Métodos();
                        bool Verifica = MET.VerificaDATA(mtbVencEst, txtInscricaoEstadual);
                        if (!Verifica) { }
                        else
                        {
                            SendKeys.Send("{HOME}");
                            SendKeys.Send("+{END}"); 
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
        }
        private void mtbVencMun_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (mtbVencMun.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
                {

                }
                else
                {
                    try
                    {
                        string MeuTEXTO = mtbVencMun.Text;
                        string MeuAno = DateTime.Now.Year.ToString();
                        if (mtbVencMun.Text.Length <= 6)
                        {
                            mtbVencMun.Text = MeuTEXTO + MeuAno;
                        }
                        TabEmpre_Métodos MET = new TabEmpre_Métodos();
                        bool Verifica = MET.VerificaDATA(mtbVencMun, txtInscricaoMunicipal);
                        if (!Verifica) { }
                        else
                        {
                            SendKeys.Send("{HOME}");
                            SendKeys.Send("+{END}");
                            return;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        #endregion


        //Pede senha para mudar o modulo do sistema
        private void comModuloSistema_MouseDown(object sender, MouseEventArgs e)
        {
            SenhaAcesso Acc = new SenhaAcesso();
            Acc.IndexAntigo = comModuloSistema.SelectedIndex;
            Acc.ShowDialog();
            comModuloSistema.SelectedIndex = Acc.NovoIndex;
            btnAvanca.Select(); btnAvanca.Focus();
        }



        //Dá um PerformClick no button ao clicar em TAB
        private void btnAvanca_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                btnAvanca.PerformClick();
                Painel_Faturamento1.Focus();
            }
        }
        private void btnRetorna_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                btnRetorna.PerformClick();
                Painel_Codigo.Focus();
            }
        }


        //Não deixa ser 0
        private void txtCaixas_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtCaixas.Text = txtCaixas.Text.PadLeft(3, '0');
                if (txtCaixas.Text == string.Empty)
                {
                    txtCaixas.Text = "0";
                }
                if (Convert.ToInt32(txtCaixas.Text) < 1)
                {
                    MessageBox.Show("Campo (Caixas) preenchido incorretamente", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMsg.Select(); txtCaixas.SelectAll();
                }
            }
        }


        //Tratamento para Decimal
        private void txtAproveitamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (Char)Keys.Back && e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }
            //pega a posição da virgula, caso ela exista:
            int posSeparator = txtAproveitamento.Text.IndexOf(',');
            //se a tecla digitada for virgula e ela já existir, barra:
            if (e.KeyChar == ',' && posSeparator > -1)
            {
                e.Handled = true;
                return;
            }
        }
        private void txtAproveitamento_TextChanged(object sender, EventArgs e)
        {
            if (txtAproveitamento.Text.Contains(","))
            {
                txtAproveitamento.MaxLength = 5;
            }
            else
            {
                txtAproveitamento.MaxLength = 3;
                if (txtAproveitamento.Text.Length == 2)
                {
                    txtAproveitamento.Text = txtAproveitamento.Text + ",";
                    txtAproveitamento.Select(3, 1);
                }
            }
        }
     
        #endregion


        #region PESQUISA
        /// <summary> REFERENTE A PESQUISA
        ///
        /// 
        ///</summary> ------------------------------------------------------------------------------



        #region Buttons Pesquisa, Voltar e Fechar
        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            txtRazaoPES.Select(); txtRazaoPES.SelectAll();
            TabEmpre_PesguisaGo Pes = new TabEmpre_PesguisaGo();
            Pes.PesquisaGo(Dgv_Pesquisa, rabAlfabetico, rabNumerico, rabTodos, rabTOP, comSituacaoPES, nupQtResultados, txtRazaoPES, txtFantasiaPES, comAtividadePES, comModuloPES);
            if (Dgv_Pesquisa.Rows.Count <= 0)
            {
                MessageBox.Show("Nenhuma informação encontrada. Verifique os filtros!", "TechSIS BWK Pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnVoltarPES_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = Tp_1;
        }
        private void btnFecharPES_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion


        //Aplica as propriedades para rabTodos e rabTOP
        private void rabTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTodos.Checked == true)
            {
                nupQtResultados.Enabled = false;
                nupQtResultados.Value = 0;
            }
        }
        private void rabTOP_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTOP.Checked == true)
            {
                nupQtResultados.Enabled = true;
                if (nupQtResultados.Value == 0)
                {
                    nupQtResultados.Value = 20;
                }
            }
        }

        #region Pesquisa por TEXTCHANGE e SELECTEDINDEX
        private void comSituacaoPES_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dgv_Pesquisa.Rows.Clear();
            btnPesquisa.PerformClick();
        }
        private void comAtividadePES_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dgv_Pesquisa.Rows.Clear();
            btnPesquisa.PerformClick();
        }
        private void comModuloPES_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dgv_Pesquisa.Rows.Clear();
            btnPesquisa.PerformClick();
        }
        private void txtRazaoPES_TextChanged(object sender, EventArgs e)
        {
            Dgv_Pesquisa.Rows.Clear();
            TabEmpre_PesguisaGo Pes = new TabEmpre_PesguisaGo();
            Pes.PesquisaGo(Dgv_Pesquisa, rabAlfabetico, rabNumerico, rabTodos, rabTOP, comSituacaoPES, nupQtResultados, txtRazaoPES, txtFantasiaPES, comAtividadePES, comModuloPES);
        }
        private void txtFantasiaPES_TextChanged(object sender, EventArgs e)
        {
            Dgv_Pesquisa.Rows.Clear();
            TabEmpre_PesguisaGo Pes = new TabEmpre_PesguisaGo();
            Pes.PesquisaGo(Dgv_Pesquisa, rabAlfabetico, rabNumerico, rabTodos, rabTOP, comSituacaoPES, nupQtResultados, txtRazaoPES, txtFantasiaPES, comAtividadePES, comModuloPES);
        }
        #endregion
        
        //Popula de acordo com o CellClick
        private void Dgv_Pesquisa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Codigo = Dgv_Pesquisa.CurrentRow.Cells[0].Value.ToString();
                TabEmpre_ApareButtons Apa = new TabEmpre_ApareButtons();
                Apa._ButtonZER(TXT_MESTRE, txtCodigo, btnINC, btnGravar, CamposINATIV, TabControl, Tp_1);
                btnGravar.Enabled = false; btnAvanca.Enabled = false;
                txtCodigo.Text = Codigo;
                txtCodigo.Select();
                SendKeys.Send("{TAB}");
            }
        }

        //Popula a quantidade de resultados encontrados
        private void Dgv_Pesquisa_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            txtQtSelect.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }
        private void Dgv_Pesquisa_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            txtQtSelect.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }
        #endregion































































    }
}