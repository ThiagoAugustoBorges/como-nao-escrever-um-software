using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PesProgr.cs
{
    internal partial class PesProgr : Form
    {
        public PesProgr()
        {
            InitializeComponent();
        }

        public string VariavelPesquisa
        { get; set; }
        public string CodigoLoja
        { get; set; }
        public string CodigoUsuario
        { get; set; }


        //EVENTO LOAD DO FORM
        private void PesProgr_Load(object sender, EventArgs e)
        {
            VariavelPesquisa = string.Empty;

            //VERIFICA SE É PARA GRAVAR OS FILTROS
            PesProgr_MET MET = new PesProgr_MET();
            MET.cheFILTROSChecked(cheFiltrosPES, CodigoLoja);

            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panDownPesq, txtQtSelectPES, panDownPesq, panDownPesq, panDownPesq, panDownPesq, panDownPesq };
            //MÉTODO DA CFG
            CfgComun.CfgComun_CLASS CFG = new CfgComun.CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, CodigoLoja);

            #region CANCELA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged -= new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged -= new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged -= new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged -= new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged -= new EventHandler(nupQtResultados_ValueChanged);
            comStatusPES.SelectedIndexChanged -= new EventHandler(comStatusPES_SelectedIndexChanged);
            comModuloPES.SelectedIndexChanged -= new EventHandler(comModuloPES_SelectedIndexChanged);
            #endregion
            //CARREGA OS FILTROS
            MET.CarregaFILTROS(cheFiltrosPES, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comStatusPES, comModuloPES);
            #region VOLTA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged += new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged += new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged += new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged += new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged += new EventHandler(nupQtResultados_ValueChanged);
            comStatusPES.SelectedIndexChanged += new EventHandler(comStatusPES_SelectedIndexChanged);
            comModuloPES.SelectedIndexChanged += new EventHandler(comModuloPES_SelectedIndexChanged);
            if (rabTOP.Checked == true) { nupQtResultados.Enabled = true; }
            #endregion

            //LIMPA O GRID
            Dgv_Pesquisa.Rows.Clear();


            txtDescri.Select();
        }




        //TECLAS DE ATALHO
        private void PesProgr_KeyDown(object sender, KeyEventArgs e)
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




        //Buttons
        #region Buttons Pesquisar, Fechar e Ajuda
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesProgr_MET MET = new PesProgr_MET();
            MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comStatusPES, comModuloPES);

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
            PesProgr_MET MET = new PesProgr_MET();
            MET.LinkAjuda();
        }
        #endregion



        //QUANTIDADE DO SELECT
        private void Dgv_Pesquisa_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }
        private void Dgv_Pesquisa_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }


        //GRAVA OS FILTOS
        private void PesProgr_FormClosing(object sender, FormClosingEventArgs e)
        {
            PesProgr_MET MET = new PesProgr_MET();
            MET.GravarFILTROS(cheFiltrosPES, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comStatusPES, comModuloPES);
        }


        //PREENCHE A VARIAVEL
        private void Dgv_Pesquisa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                VariavelPesquisa = Dgv_Pesquisa.CurrentRow.Cells[0].Value.ToString();
                this.Close();
            }
        }


        //FAZ AS PESQUISAS COM OS EVENTOS DOS FORMULÁRIOS
        #region PESQUISA EVENTOS DOS FORMULÁRIOS

        private void comStatusPES_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesProgr_MET MET = new PesProgr_MET();
            MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comStatusPES, comModuloPES);
        }

        private void comModuloPES_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesProgr_MET MET = new PesProgr_MET();
            MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comStatusPES, comModuloPES);
        }

        private void rabNumerico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabNumerico.Checked == true)
            {
                //EXECUTA A PESQUISA
                PesProgr_MET MET = new PesProgr_MET();
                MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comStatusPES, comModuloPES);
            }
        }

        private void rabAlfabetico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabAlfabetico.Checked == true)
            {
                //EXECUTA A PESQUISA
                PesProgr_MET MET = new PesProgr_MET();
                MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comStatusPES, comModuloPES);
            }
        }

        private void rabTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTodos.Checked == true)
            {
                //EXECUTA A PESQUISA
                PesProgr_MET MET = new PesProgr_MET();
                MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comStatusPES, comModuloPES);
            }
        }

        private void rabTOP_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTOP.Checked == true)
            {
                //EXECUTA A PESQUISA
                PesProgr_MET MET = new PesProgr_MET();
                MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comStatusPES, comModuloPES);
            }

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
        }

        private void txtDescri_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesProgr_MET MET = new PesProgr_MET();
            MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comStatusPES, comModuloPES);
        }


        private void nupQtResultados_ValueChanged(object sender, EventArgs e)
        {
            if (rabTOP.Checked == true)
            {
                //EXECUTA A PESQUISA
                PesProgr_MET MET = new PesProgr_MET();
                MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comStatusPES, comModuloPES);
            }
        }

        #endregion



       

    }
}
