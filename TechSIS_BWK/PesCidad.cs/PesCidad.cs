using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PesCidad.cs
{
    internal partial class PesCidad : Form
    {
        public PesCidad()
        {
            InitializeComponent();
        }

        public string _Login_LojaID_PesCidad { get; set; }
        public string _Login_UsuarioID_PesCidad { get; set; }

        public string _ResultPesquisa { get; set; }

        //LOAD DO FORM
        private void PesCidad_Load(object sender, EventArgs e)
        {
            PesCidad_MET MET = new PesCidad_MET();

            //VERIFICA SE É PARA GRAVAR OS FILTROS
            MET.cheFILTROSChecked(cheFiltrosPES, _Login_LojaID_PesCidad);

            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panDownPesq, txtQtSelectPES, panDownPesq, panDownPesq, panDownPesq, panDownPesq, panDownPesq };
            //MÉTODO DA CFG
            CfgComun.CfgComun_CLASS CFG = new CfgComun.CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, _Login_LojaID_PesCidad);

            #region CANCELA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged -= new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged -= new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged -= new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged -= new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged -= new EventHandler(nupQtResultados_ValueChanged);
            comUF.SelectedIndexChanged -= new EventHandler(comUF_SelectedIndexChanged);
            comStatus.SelectedIndexChanged -= new EventHandler(comStatus_SelectedIndexChanged);
            #endregion
            //CARREGA OS FILTROS
            MET.CarregaFILTROS(cheFiltrosPES, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comUF, comStatus);
            #region VOLTA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged += new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged += new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged += new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged += new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged += new EventHandler(nupQtResultados_ValueChanged);
            comUF.SelectedIndexChanged += new EventHandler(comUF_SelectedIndexChanged);
            comStatus.SelectedIndexChanged += new EventHandler(comStatus_SelectedIndexChanged);
            if (rabTOP.Checked == true) { nupQtResultados.Enabled = true; }
            #endregion

            //APAGA A VARIAVEL
            _ResultPesquisa = string.Empty;

            //LIMPA O GRID
            Dgv_Pesquisa.Rows.Clear();

            txtDescri.Select();
        }

        //GRAVA OS FILTROS
        private void PesCidad_FormClosing(object sender, FormClosingEventArgs e)
        {
            PesCidad_MET MET = new PesCidad_MET();
            //GRAVA OS FILTROS DA PESQUISA
            MET.GravarFILTROS(cheFiltrosPES, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comUF, comStatus);
        }

        //TECLAS DE ATALHO
        private void PesCidad_KeyDown(object sender, KeyEventArgs e)
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









        //ATUALIZA A QT DE RESULTADOS ENCONTRADOS NA PESQUISA
        private void Dgv_Pesquisa_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }
        private void Dgv_Pesquisa_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }


        #region Buttons Pesquisar, Fechar e Ajuda
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesCidad_MET MET = new PesCidad_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesCidad, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriUf, comUF, comStatus);
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
            //EXIBE A AJUDA
            PesCidad_MET MET = new PesCidad_MET();
            MET.LinkAjuda();

            txtDescri.Select(); txtDescri.SelectAll();
        }
        #endregion


        #region FAZ A PESQUISA PELOS DIFERENTES EVENTOS
        private void txtDescri_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesCidad_MET MET = new PesCidad_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesCidad, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriUf, comUF, comStatus);
        }
        private void txtDescriUf_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesCidad_MET MET = new PesCidad_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesCidad, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriUf, comUF, comStatus);
        }
        private void comUF_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesCidad_MET MET = new PesCidad_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesCidad, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriUf, comUF, comStatus);
            txtDescri.Select(); txtDescri.SelectAll();
        }
        private void comStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesCidad_MET MET = new PesCidad_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesCidad, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriUf, comUF, comStatus);
            txtDescri.Select(); txtDescri.SelectAll();
        }
        private void rabNumerico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabNumerico.Checked == true)
            {
                //EXECUTA A PESQUISA
                PesCidad_MET MET = new PesCidad_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesCidad, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriUf, comUF, comStatus);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void rabAlfabetico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabAlfabetico.Checked == true)
            {
                //EXECUTA A PESQUISA
                PesCidad_MET MET = new PesCidad_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesCidad, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriUf, comUF, comStatus);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void rabTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTodos.Checked == true)
            {
                //EXECUTA A PESQUISA
                PesCidad_MET MET = new PesCidad_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesCidad, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriUf, comUF, comStatus);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void rabTOP_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTOP.Checked == true)
            {
                nupQtResultados.Enabled = true;

                //EXECUTA A PESQUISA
                PesCidad_MET MET = new PesCidad_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesCidad, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriUf, comUF, comStatus);
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
                PesCidad_MET MET = new PesCidad_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesCidad, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriUf, comUF, comStatus);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        #endregion




        //PREENCHE A VARIAVEL E FECHA O FORMULÁRIO
        private void Dgv_Pesquisa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                _ResultPesquisa = Dgv_Pesquisa.CurrentRow.Cells[0].Value.ToString();
                this.Close();
            }
        }





    }
}
