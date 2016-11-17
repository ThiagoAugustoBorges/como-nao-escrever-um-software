using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PesEmpre.cs
{
    internal partial class PesEmpre : Form
    {
        public PesEmpre()
        {
            InitializeComponent();
        }

        public string VariavelPesquisa
        { get; set; }
        public string CodigoLoja
        { get; set; }
        public string CodigoUsuar
        { get; set; }


        //LOAD DO FORMULÁRIO
        private void PesEmpre_Load(object sender, EventArgs e)
        {
            PesEmpre_MET MET = new PesEmpre_MET();
            //VERIFICA SE É PARA GRAVAR OS FILTROS
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
            comAtividadePES.SelectedIndexChanged -= new EventHandler(comAtividadePES_SelectedIndexChanged);
            comModuloPES.SelectedIndexChanged -= new EventHandler(comModuloPES_SelectedIndexChanged);
            #endregion
            //CARREGA OS FILTROS
            MET.CarregaFILTROS(cheFiltrosPES, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comAtividadePES, comModuloPES);
            #region CANCELA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged += new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged += new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged += new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged += new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged += new EventHandler(nupQtResultados_ValueChanged);
            comAtividadePES.SelectedIndexChanged += new EventHandler(comAtividadePES_SelectedIndexChanged);
            comModuloPES.SelectedIndexChanged += new EventHandler(comModuloPES_SelectedIndexChanged);
            if (rabTOP.Checked == true) { nupQtResultados.Enabled = true; }
            #endregion

            //APAGA A VARIAVEL
            VariavelPesquisa = string.Empty;

            //LIMPA O GRID
            Dgv_Pesquisa.Rows.Clear();

            txtDescri.Select();
        }

        //TECLAS DE ATALHOS
        private void PesEmpre_KeyDown(object sender, KeyEventArgs e)
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



        #region Buttons Pesquisar, Fechar e Ajuda
        //BUTTONS
        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnAjuda_Click(object sender, EventArgs e)
        {
            //ABRE A AJUDA
            PesEmpre_MET MET = new PesEmpre_MET();
            MET.LinkAjuda();
        }
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesEmpre_MET MET = new PesEmpre_MET();
            MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comAtividadePES, comModuloPES);

            txtDescri.Select();
        }

        #endregion



        //QUANTIDADE DE RESULTADOS NO SELECT
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
                VariavelPesquisa = Dgv_Pesquisa.CurrentRow.Cells[0].Value.ToString();
                this.Close();
            }
        }


        //APLICA A PESQUISA PELOS EVENTOS DOS CONTROLES
        #region EXECUTA AS PESQUISAS PELOS EVENTOS
        //APLICA A PROPRIEDADE
        private void rabTOP_CheckedChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesEmpre_MET MET = new PesEmpre_MET();
            MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comAtividadePES, comModuloPES);

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


        private void rabNumerico_CheckedChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesEmpre_MET MET = new PesEmpre_MET();
            MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comAtividadePES, comModuloPES);
        }

        private void rabAlfabetico_CheckedChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesEmpre_MET MET = new PesEmpre_MET();
            MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comAtividadePES, comModuloPES);
        }

        private void rabTodos_CheckedChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesEmpre_MET MET = new PesEmpre_MET();
            MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comAtividadePES, comModuloPES);
        }

        private void comAtividadePES_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesEmpre_MET MET = new PesEmpre_MET();
            MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comAtividadePES, comModuloPES);
        }

        private void comModuloPES_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesEmpre_MET MET = new PesEmpre_MET();
            MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comAtividadePES, comModuloPES);
        }

        private void txtDescri_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesEmpre_MET MET = new PesEmpre_MET();
            MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comAtividadePES, comModuloPES);
        }

        private void nupQtResultados_ValueChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesEmpre_MET MET = new PesEmpre_MET();
            MET.Pesquisa_EXEC(CodigoLoja, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comAtividadePES, comModuloPES);
        }
        #endregion




        //GRAVA OS FILTROS
        private void PesEmpre_FormClosing(object sender, FormClosingEventArgs e)
        {
            PesEmpre_MET MET = new PesEmpre_MET();
            //GRAVA OS FILTROS DA PESQUISA
            MET.GravarFILTROS(cheFiltrosPES, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comAtividadePES, comModuloPES);
        }

        




    }
}
