using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TabConfg
{
    internal partial class TabConfg : Form
    {
        public TabConfg()
        {
            InitializeComponent();
        }

        public string _Login_LojaID_Confg { get; set; }
        public string _Login_UsuarioID_Confg { get; set; }
        public string _Login_UsuarioDesc_Confg { get; set; }


        public MenuStrip MenuStrip_FORM { get; set; }
        public Panel Panel_Opcoes { get; set; }
        public Panel Panel_Buttons { get; set;}
        public Panel Painel_Informações { get; set; }
        public Panel Painel_Erro { get; set; }

        public Button ATALHO1 { get; set; }
        public Button ATALHO2 { get; set; }

        public Color Cor_BackUp { get; set; }


        //LOAD DO FORMULÁRIO
        private void TabConfg_Load(object sender, EventArgs e)
        {
            Text = "EMPRESA.: " + _Login_LojaID_Confg.PadLeft(6, '0') + " - CONFIGURAÇÃO GERAL DO SISTEMA - " + _Login_UsuarioID_Confg.PadLeft(6, '0') + "  " + _Login_UsuarioDesc_Confg;

            CamposDisable();

            //PEGA OS RESULTADOS
            TabConfg_MET MET = new TabConfg_MET();
            MET.MET_SelectResult(_Login_LojaID_Confg, cheGravaFiltros, nupQtResultados, txtCaminhoREL, nupOcioso, cheTocarBoasVindas, cheMasterLixeira, cheMasterLoja, cheAlertaReceber, cheAlertaPagar, cheDatasComemorativas, cheAlertaOffice, cheTrocarUsuario, cheOcultarPainel, cheMsgEmergencia, txtAtalho1, txtAtalho2, txtAtalhoDll1, txtAtalhoDll2, panMeio, cheLancCaixa);

            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panMeio, panMeio, panMeio, panMeio, panMeio, panMeio };
            //MÉTODO DA CFG
            CfgComun.CfgComun_CLASS CFG = new CfgComun.CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, _Login_LojaID_Confg);

            txtSenha.Select();
        }

        //TECLAS DE ATALHO
        private void TabConfg_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    btnSenha.PerformClick();
                    break;
                case Keys.F2:
                    try
                    {
                        comCores.SelectedIndex++;
                    }
                    catch (Exception)
                    {
                        comCores.SelectedIndex = 0;
                    }
                    break;
                case Keys.F3:
                    try
                    {
                        comCores.SelectedIndex--;
                    }
                    catch (Exception)
                    {
                        comCores.SelectedIndex = 0;
                    }
                    break;
                case Keys.F10:
                    btnGravar.PerformClick();
                    break;
                case Keys.F7:
                    btnFechar.PerformClick();
                    break;
            }
        }

        ////VOLTA A COR DE BACKUP E MUDA O NOME DOS LABELS DE ATALHO
        private void TabConfg_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != System.Windows.Forms.DialogResult.OK)
            {
                panMeio.BackColor = Cor_BackUp;
                MenuStrip_FORM.BackColor = Cor_BackUp;
                Panel_Opcoes.BackColor = Cor_BackUp;
                Panel_Buttons.BackColor = Cor_BackUp;
                Painel_Informações.BackColor = Cor_BackUp;
                Painel_Erro.BackColor = Cor_BackUp;
            }
            else
            {
                ATALHO1.Text = txtAtalho1.Text;
                ATALHO2.Text = txtAtalho2.Text;
            }
        }

        public void CamposEnable()
        {
            #region grbRelatorios
            foreach (Control ctrl in grbRelatorios.Controls)
            {
                if (ctrl.GetType() == typeof(Label))
                {
                    (ctrl as Label).Enabled = true;
                }
                if (ctrl.GetType() == typeof(NumericUpDown))
                {
                    (ctrl as NumericUpDown).Enabled = true;
                }
                if (ctrl.GetType() == typeof(Button))
                {
                    (ctrl as Button).Enabled = true;
                }
                if (ctrl.GetType() == typeof(TextBox) && ctrl.Name != "txtCaminhoREL")
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
            #region grbSistema
            foreach (Control ctrl in grbSistema.Controls)
            {
                if (ctrl.GetType() == typeof(Label))
                {
                    (ctrl as Label).Enabled = true;
                }
                if (ctrl.GetType() == typeof(NumericUpDown))
                {
                    (ctrl as NumericUpDown).Enabled = true;
                }
                if (ctrl.GetType() == typeof(Button))
                {
                    (ctrl as Button).Enabled = true;
                }
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
            #region grbAtalhos
            foreach (Control ctrl in grbAtalhos.Controls)
            {
                if (ctrl.GetType() == typeof(Label))
                {
                    (ctrl as Label).Enabled = true;
                }
                if (ctrl.GetType() == typeof(NumericUpDown))
                {
                    (ctrl as NumericUpDown).Enabled = true;
                }
                if (ctrl.GetType() == typeof(Button))
                {
                    (ctrl as Button).Enabled = true;
                }
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
            #region grbCores
            foreach (Control ctrl in grbCores.Controls)
            {
                if (ctrl.GetType() == typeof(Label))
                {
                    (ctrl as Label).Visible = true;
                }
                if (ctrl.GetType() == typeof(NumericUpDown))
                {
                    (ctrl as NumericUpDown).Visible = true;
                }
                if (ctrl.GetType() == typeof(Button))
                {
                    (ctrl as Button).Visible = true;
                }
                if (ctrl.GetType() == typeof(TextBox))
                {
                    (ctrl as TextBox).Visible = true;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    (ctrl as ComboBox).Visible = true;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    (ctrl as MaskedTextBox).Visible = true;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    (ctrl as CheckBox).Visible = true;
                }
            }
            #endregion
        }
        public void CamposDisable()
        {
            #region grbRelatorios
            foreach (Control ctrl in grbRelatorios.Controls)
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
                if (ctrl.GetType() == typeof(Label))
                {
                    (ctrl as Label).Enabled = false;
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
            #region grbSistema
            foreach (Control ctrl in grbSistema.Controls)
            {
                if (ctrl.GetType() == typeof(Label))
                {
                    (ctrl as Label).Enabled = false;
                }
                if (ctrl.GetType() == typeof(NumericUpDown))
                {
                    (ctrl as NumericUpDown).Enabled = false;
                }
                if (ctrl.GetType() == typeof(Button))
                {
                    (ctrl as Button).Enabled = false;
                }
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
            #region grbAtalhos
            foreach (Control ctrl in grbAtalhos.Controls)
            {
                if (ctrl.GetType() == typeof(Label))
                {
                    (ctrl as Label).Enabled = false;
                }
                if (ctrl.GetType() == typeof(NumericUpDown))
                {
                    (ctrl as NumericUpDown).Enabled = false;
                }
                if (ctrl.GetType() == typeof(Button))
                {
                    (ctrl as Button).Enabled = false;
                }
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
            #region grbCores
            foreach (Control ctrl in grbCores.Controls)
            {
                if (ctrl.GetType() == typeof(Label))
                {
                    (ctrl as Label).Visible = false;
                }
                if (ctrl.GetType() == typeof(NumericUpDown))
                {
                    (ctrl as NumericUpDown).Visible = false;
                }
                if (ctrl.GetType() == typeof(Button))
                {
                    (ctrl as Button).Visible = false;
                }
                if (ctrl.GetType() == typeof(TextBox))
                {
                    (ctrl as TextBox).Visible = false;
                }
                if (ctrl.GetType() == typeof(ComboBox))
                {
                    (ctrl as ComboBox).Visible = false;
                }
                if (ctrl.GetType() == typeof(MaskedTextBox))
                {
                    (ctrl as MaskedTextBox).Visible = false;
                }
                if (ctrl.GetType() == typeof(CheckBox))
                {
                    (ctrl as CheckBox).Visible = false;
                }
            }
            #endregion
        }


        //TROCAM AS CORES DO FORMULÁRIO
        #region TROCA AS CORES DO FORMULÁRIO
        private void btnCor1_Click(object sender, EventArgs e)
        {
            panMeio.BackColor = Color.CornflowerBlue;
            MenuStrip_FORM.BackColor = Color.CornflowerBlue;
            Panel_Opcoes.BackColor = Color.CornflowerBlue;
            Panel_Buttons.BackColor = Color.CornflowerBlue;
            Painel_Informações.BackColor = Color.CornflowerBlue;
            Painel_Erro.BackColor = Color.CornflowerBlue;
        }
        private void btnCor2_Click(object sender, EventArgs e)
        {
            panMeio.BackColor = Color.Pink;
            MenuStrip_FORM.BackColor = Color.Pink;
            Panel_Opcoes.BackColor = Color.Pink;
            Panel_Buttons.BackColor = Color.Pink;
            Painel_Informações.BackColor = Color.Pink;
            Painel_Erro.BackColor = Color.Pink;
        }
        private void btnCor3_Click(object sender, EventArgs e)
        {
            panMeio.BackColor = Color.SeaGreen;
            MenuStrip_FORM.BackColor = Color.SeaGreen;
            Panel_Opcoes.BackColor = Color.SeaGreen;
            Panel_Buttons.BackColor = Color.SeaGreen;
            Painel_Informações.BackColor = Color.SeaGreen;
            Painel_Erro.BackColor = Color.SeaGreen;
        }
        private void btnCor4_Click(object sender, EventArgs e)
        {
            panMeio.BackColor = Color.White;
            MenuStrip_FORM.BackColor = Color.White;
            Panel_Opcoes.BackColor = Color.White;
            Panel_Buttons.BackColor = Color.White;
            Painel_Informações.BackColor = Color.White;
            Painel_Erro.BackColor = Color.White;
        }
        private void btnCor5_Click(object sender, EventArgs e)
        {
            panMeio.BackColor = Color.Blue;
            MenuStrip_FORM.BackColor = Color.Blue;
            Panel_Opcoes.BackColor = Color.Blue;
            Panel_Buttons.BackColor = Color.Blue;
            Painel_Informações.BackColor = Color.Blue;
            Painel_Erro.BackColor = Color.Blue;
        }
        private void btnCor6_Click(object sender, EventArgs e)
        {
            panMeio.BackColor = Color.Red;
            MenuStrip_FORM.BackColor = Color.Red;
            Panel_Opcoes.BackColor = Color.Red;
            Panel_Buttons.BackColor = Color.Red;
            Painel_Informações.BackColor = Color.Red;
            Painel_Erro.BackColor = Color.Red;
        }
        private void btnCor7_Click(object sender, EventArgs e)
        {
            panMeio.BackColor = Color.Green;
            MenuStrip_FORM.BackColor = Color.Green;
            Panel_Opcoes.BackColor = Color.Green;
            Panel_Buttons.BackColor = Color.Green;
            Painel_Informações.BackColor = Color.Green;
            Painel_Erro.BackColor = Color.Green;
        }
        private void btnCor8_Click(object sender, EventArgs e)
        {
            panMeio.BackColor = Color.Orange;
            MenuStrip_FORM.BackColor = Color.Orange;
            Panel_Opcoes.BackColor = Color.Orange;
            Panel_Buttons.BackColor = Color.Orange;
            Painel_Informações.BackColor = Color.Orange;
            Painel_Erro.BackColor = Color.Orange;
        }
        private void btnCor9_Click(object sender, EventArgs e)
        {
            panMeio.BackColor = Color.DeepSkyBlue;
            MenuStrip_FORM.BackColor = Color.DeepSkyBlue;
            Panel_Opcoes.BackColor = Color.DeepSkyBlue;
            Panel_Buttons.BackColor = Color.DeepSkyBlue;
            Painel_Informações.BackColor = Color.DeepSkyBlue;
            Painel_Erro.BackColor = Color.DeepSkyBlue;
        }
        private void btnCor10_Click(object sender, EventArgs e)
        {
            panMeio.BackColor = Color.BlueViolet;
            MenuStrip_FORM.BackColor = Color.BlueViolet;
            Panel_Opcoes.BackColor = Color.BlueViolet;
            Panel_Buttons.BackColor = Color.BlueViolet;
            Painel_Informações.BackColor = Color.BlueViolet;
            Painel_Erro.BackColor = Color.BlueViolet;
        }
        private void btnCor11_Click(object sender, EventArgs e)
        {
            panMeio.BackColor = Color.DeepPink;
            MenuStrip_FORM.BackColor = Color.DeepPink;
            Panel_Opcoes.BackColor = Color.DeepPink;
            Panel_Buttons.BackColor = Color.DeepPink;
            Painel_Informações.BackColor = Color.DeepPink;
            Painel_Erro.BackColor = Color.DeepPink;
        }

        private void comCores_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comCores.SelectedIndex)
            {
                case 0:
                    panMeio.BackColor = Color.SkyBlue;
                    MenuStrip_FORM.BackColor = Color.SkyBlue;
                    Panel_Opcoes.BackColor = Color.SkyBlue;
                    Panel_Buttons.BackColor = Color.SkyBlue;
                    Painel_Informações.BackColor = Color.SkyBlue;
                    Painel_Erro.BackColor = Color.SkyBlue;
                    break;
                case 1:
                    panMeio.BackColor = Color.Maroon;
                    MenuStrip_FORM.BackColor = Color.Maroon;
                    Panel_Opcoes.BackColor = Color.Maroon;
                    Panel_Buttons.BackColor = Color.Maroon;
                    Painel_Informações.BackColor = Color.Maroon;
                    Painel_Erro.BackColor = Color.Maroon;
                    break;
                case 2:
                    panMeio.BackColor = Color.Gold;
                    MenuStrip_FORM.BackColor = Color.Gold;
                    Panel_Opcoes.BackColor = Color.Gold;
                    Panel_Buttons.BackColor = Color.Gold;
                    Painel_Informações.BackColor = Color.Gold;
                    Painel_Erro.BackColor = Color.Gold;
                    break;
                case 3:
                    panMeio.BackColor = Color.DarkOrange;
                    MenuStrip_FORM.BackColor = Color.DarkOrange;
                    Panel_Opcoes.BackColor = Color.DarkOrange;
                    Panel_Buttons.BackColor = Color.DarkOrange;
                    Painel_Informações.BackColor = Color.DarkOrange;
                    Painel_Erro.BackColor = Color.DarkOrange;
                    break;
                case 4:
                    panMeio.BackColor = Color.DarkSlateBlue;
                    MenuStrip_FORM.BackColor = Color.DarkSlateBlue;
                    Panel_Opcoes.BackColor = Color.DarkSlateBlue;
                    Panel_Buttons.BackColor = Color.DarkSlateBlue;
                    Painel_Informações.BackColor = Color.DarkSlateBlue;
                    Painel_Erro.BackColor = Color.DarkSlateBlue;
                    break;
                case 5:
                    panMeio.BackColor = Color.Yellow;
                    MenuStrip_FORM.BackColor = Color.Yellow;
                    Panel_Opcoes.BackColor = Color.Yellow;
                    Panel_Buttons.BackColor = Color.Yellow;
                    Painel_Informações.BackColor = Color.Yellow;
                    Painel_Erro.BackColor = Color.Yellow;
                    break;
                case 6:
                    panMeio.BackColor = Color.Goldenrod;
                    MenuStrip_FORM.BackColor = Color.Goldenrod;
                    Panel_Opcoes.BackColor = Color.Goldenrod;
                    Panel_Buttons.BackColor = Color.Goldenrod;
                    Painel_Informações.BackColor = Color.Goldenrod;
                    Painel_Erro.BackColor = Color.Goldenrod;
                    break;
                case 7:
                    panMeio.BackColor = Color.Lime;
                    MenuStrip_FORM.BackColor = Color.Lime;
                    Panel_Opcoes.BackColor = Color.Lime;
                    Panel_Buttons.BackColor = Color.Lime;
                    Painel_Informações.BackColor = Color.Lime;
                    Painel_Erro.BackColor = Color.Lime;
                    break;
                case 8:
                    panMeio.BackColor = Color.Aqua;
                    MenuStrip_FORM.BackColor = Color.Aqua;
                    Panel_Opcoes.BackColor = Color.Aqua;
                    Panel_Buttons.BackColor = Color.Aqua;
                    Painel_Informações.BackColor = Color.Aqua;
                    Painel_Erro.BackColor = Color.Aqua;
                    break;
                case 9:
                    panMeio.BackColor = Color.Magenta;
                    MenuStrip_FORM.BackColor = Color.Magenta;
                    Panel_Opcoes.BackColor = Color.Magenta;
                    Panel_Buttons.BackColor = Color.Magenta;
                    Painel_Informações.BackColor = Color.Magenta;
                    Painel_Erro.BackColor = Color.Magenta;
                    break;
                case 10:
                    panMeio.BackColor = Color.OliveDrab;
                    MenuStrip_FORM.BackColor = Color.OliveDrab;
                    Panel_Opcoes.BackColor = Color.OliveDrab;
                    Panel_Buttons.BackColor = Color.OliveDrab;
                    Painel_Informações.BackColor = Color.OliveDrab;
                    Painel_Erro.BackColor = Color.OliveDrab;
                    break;
                case 11:
                    panMeio.BackColor = Color.DimGray;
                    MenuStrip_FORM.BackColor = Color.DimGray;
                    Panel_Opcoes.BackColor = Color.DimGray;
                    Panel_Buttons.BackColor = Color.DimGray;
                    Painel_Informações.BackColor = Color.DimGray;
                    Painel_Erro.BackColor = Color.DimGray;
                    break;
                case 12:
                    panMeio.BackColor = Color.LightGray;
                    MenuStrip_FORM.BackColor = Color.LightGray;
                    Panel_Opcoes.BackColor = Color.LightGray;
                    Panel_Buttons.BackColor = Color.LightGray;
                    Painel_Informações.BackColor = Color.LightGray;
                    Painel_Erro.BackColor = Color.LightGray;
                    break;
                case 13:
                    panMeio.BackColor = Color.Peru;
                    MenuStrip_FORM.BackColor = Color.Peru;
                    Panel_Opcoes.BackColor = Color.Peru;
                    Panel_Buttons.BackColor = Color.Peru;
                    Painel_Informações.BackColor = Color.Peru;
                    Painel_Erro.BackColor = Color.Peru;
                    break;
                case 14:
                    panMeio.BackColor = Color.Coral;
                    MenuStrip_FORM.BackColor = Color.Coral;
                    Panel_Opcoes.BackColor = Color.Coral;
                    Panel_Buttons.BackColor = Color.Coral;
                    Painel_Informações.BackColor = Color.Coral;
                    Painel_Erro.BackColor = Color.Coral;
                    break;
                case 15:
                    panMeio.BackColor = Color.Chocolate;
                    MenuStrip_FORM.BackColor = Color.Chocolate;
                    Panel_Opcoes.BackColor = Color.Chocolate;
                    Panel_Buttons.BackColor = Color.Chocolate;
                    Painel_Informações.BackColor = Color.Chocolate;
                    Painel_Erro.BackColor = Color.Chocolate;
                    break;
                case 16:
                    panMeio.BackColor = Color.YellowGreen;
                    MenuStrip_FORM.BackColor = Color.YellowGreen;
                    Panel_Opcoes.BackColor = Color.YellowGreen;
                    Panel_Buttons.BackColor = Color.YellowGreen;
                    Painel_Informações.BackColor = Color.YellowGreen;
                    Painel_Erro.BackColor = Color.YellowGreen;
                    break;
            }
        }


        
        #endregion

        //BUTTONS DO FORMULÁRIO
        #region Buttons Fechar, Senha e Gravar
        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnSenha_Click(object sender, EventArgs e)
        {
            TabConfg_MET MET = new TabConfg_MET();
            //VERIFICA A SENHA
            bool Sen = MET.MET_SelectSenha(txtSenha, btnSenha, CamposEnable, btnGravar, cheGravaFiltros);
            if (!Sen) { } else { return; }
        }
        private void btnGravar_Click(object sender, EventArgs e)
        {
            //GRAVA OS NOVOS DADOS
            TabConfg_MET MET = new TabConfg_MET();
            MET.MET_GravarResult(_Login_LojaID_Confg, _Login_UsuarioID_Confg, _Login_UsuarioDesc_Confg, cheGravaFiltros, nupQtResultados, txtCaminhoREL, nupOcioso, cheTocarBoasVindas, cheMasterLixeira, cheMasterLoja, cheAlertaReceber, cheAlertaPagar, cheDatasComemorativas, cheAlertaOffice, cheTrocarUsuario, cheOcultarPainel, cheMsgEmergencia, txtAtalho1, txtAtalho2, txtAtalhoDll1, txtAtalhoDll2, panMeio, cheLancCaixa);
        }
        #endregion


        //PROCURA NOVA PASTA
        private void btnProcurarPasta_Click(object sender, EventArgs e)
        {
            txtCaminhoREL.Text = string.Empty;

            FolderBrowserDialog Browner = new FolderBrowserDialog();

            Browner.ShowDialog();

            txtCaminhoREL.Text = Browner.SelectedPath;
            if (txtCaminhoREL.Text.Length > 3)
            {
                txtCaminhoREL.Text = Browner.SelectedPath + @"\";
            }

            btnGravar.Select();
        }

        //FAZ O TRATAMENTO DE .DLL NO KEY DOWN APAGANDO O PONTO FINAL
        private void txtAtalhoDll1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemPeriod)
            {
                MessageBox.Show("O próprio sistema já faz o tratamento de .DLL", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SendKeys.Send("{BS}");
            }
        }
        private void txtAtalhoDll2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemPeriod)
            {
                MessageBox.Show("O próprio sistema já faz o tratamento de .DLL", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SendKeys.Send("{BS}");
            }
        }

       







    }
}
