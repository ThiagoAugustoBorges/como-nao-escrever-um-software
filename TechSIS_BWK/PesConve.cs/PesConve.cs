using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PesConve.cs
{
    internal partial class PesConve : Form
    {
        public PesConve()
        {
            InitializeComponent();
        }

        public string _Login_LojaID_PesConve { get; set; }
        public string _Login_UsuarioID_PesConve { get; set; }

        public string _ResultPesquisa { get; set; }

        //LOAD DO FORMULARIO
        private void PesConve_Load(object sender, EventArgs e)
        {
            PesConve_MET MET = new PesConve_MET();

            //VERIFICA SE É PARA GRAVAR OS FILTROS
            MET.cheFILTROSChecked(cheFiltrosPES, _Login_LojaID_PesConve);

            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panDownPesq, txtQtSelectPES, panDownPesq, panDownPesq, panDownPesq, panDownPesq, panDownPesq };
            //MÉTODO DA CFG
            CfgComun.CfgComun_CLASS CFG = new CfgComun.CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, _Login_LojaID_PesConve);

            #region CANCELA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged -= new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged -= new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged -= new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged -= new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged -= new EventHandler(nupQtResultados_ValueChanged);
            comStatus.SelectedIndexChanged -= new EventHandler(comStatus_SelectedIndexChanged);
            comTipo.SelectedIndexChanged -= new EventHandler(comTipo_SelectedIndexChanged);
            #endregion
            //CARREGA OS FILTROS
            MET.CarregaFILTROS(panPrinPesq);
            #region VOLTA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged += new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged += new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged += new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged += new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged += new EventHandler(nupQtResultados_ValueChanged);
            comStatus.SelectedIndexChanged += new EventHandler(comStatus_SelectedIndexChanged);
            comTipo.SelectedIndexChanged += new EventHandler(comTipo_SelectedIndexChanged);
            #endregion

            //APAGA A VARIAVEL
            _ResultPesquisa = string.Empty;

            //LIMPA O GRID
            Dgv_Pesquisa.Rows.Clear();




            //SELECIONA O TEXTBOX
            txtDescri.Select();
        }

        //TECLAS DE ATALHO
        private void PesConve_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    btnPesquisar.PerformClick();
                    break;
                case Keys.F2:
                    btnAjuda.PerformClick();
                    break;
                case Keys.F7:
                    btnFechar.PerformClick();
                    break;
            }
        }

        //GRAVA OS FILTROS
        private void PesConve_FormClosing(object sender, FormClosingEventArgs e)
        {
            PesConve_MET MET = new PesConve_MET();
            MET.GravarFILTROS(panPrinPesq);
        }



        //ATUALIZA A QT DE RESULTADOS ENCONTRADOS NA PESQUISA
        private void Dgv_Pesquisa_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }
        private void Dgv_Pesquisa_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }

        //PREENCHE A VARIAVEL E FECHA O FORMULÁRIO
        private void Dgv_Pesquisa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                _ResultPesquisa = Dgv_Pesquisa.CurrentRow.Cells[0].Value.ToString();
                this.Close();
            }
        }

        #region Buttons Pesquisar, Fecher e Ajuda
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            PesConve_MET MET = new PesConve_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesConve, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comTipo, txtDescri);
            if (txtQtSelectPES.Text == string.Empty) { txtQtSelectPES.Text = "000000"; }
            if (Convert.ToInt32(txtQtSelectPES.Text) == 0)
            {
                MessageBox.Show("Nenhuma informação encontrada. Verifique os filtros!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            txtDescri.Select(); txtDescri.SelectAll();
        }
        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnAjuda_Click(object sender, EventArgs e)
        {
            PesConve_MET MET = new PesConve_MET();
            MET.LinkAjuda();
            btnPesquisar.Select();
        }
        #endregion



        #region FAZ A PESQUISA PELOS EVENTOS
        private void rabNumerico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabNumerico.Checked == true)
            {
                //EXECUTA A PESQUISA
                PesConve_MET MET = new PesConve_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesConve, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comTipo, txtDescri);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void rabAlfabetico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabAlfabetico.Checked == true)
            {
                //EXECUTA A PESQUISA
                PesConve_MET MET = new PesConve_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesConve, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comTipo, txtDescri);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void rabTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTodos.Checked == true)
            {
                //EXECUTA A PESQUISA
                PesConve_MET MET = new PesConve_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesConve, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comTipo, txtDescri);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void rabTOP_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTOP.Checked == true)
            {
                nupQtResultados.Enabled = true;

                //EXECUTA A PESQUISA
                PesConve_MET MET = new PesConve_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesConve, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comTipo, txtDescri);
                txtDescri.Select(); txtDescri.SelectAll();
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
                PesConve_MET MET = new PesConve_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesConve, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comTipo, txtDescri);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void txtDescri_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesConve_MET MET = new PesConve_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesConve, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comTipo, txtDescri);
        }
        private void comStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesConve_MET MET = new PesConve_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesConve, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comTipo, txtDescri);
            txtDescri.Select(); txtDescri.SelectAll();
        }
        private void comTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesConve_MET MET = new PesConve_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesConve, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comTipo, txtDescri);
            txtDescri.Select(); txtDescri.SelectAll();
        }
        #endregion



    }
}
