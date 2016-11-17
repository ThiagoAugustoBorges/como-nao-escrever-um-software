using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PesMsgNt.cs
{
    internal partial class PesMsgNt : Form
    {
        public PesMsgNt()
        {
            InitializeComponent();
        }


        public string _Login_LojaID_PesMsgNt { get; set; }
        public string _Login_UsuarioID_PesMsgNt { get; set; }

        public string _ResultPesquisa { get; set; }


        //LOAD DO FORM
        private void PesMsgNt_Load(object sender, EventArgs e)
        {
            PesMsgNt_MET MET = new PesMsgNt_MET();

            //VERIFICA SE É PARA GRAVAR OS FILTROS
            MET.cheFILTROSChecked(cheFiltrosPES, _Login_LojaID_PesMsgNt);

            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panDownPesq, txtQtSelectPES, panPrinPesq, panDownPesq, panDownPesq, panDownPesq, panDownPesq };
            //MÉTODO DA CFG
            CfgComun.CfgComun_CLASS CFG = new CfgComun.CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, _Login_LojaID_PesMsgNt);

            //CARREGA OS FILTROS
            MET.CarregaFILTROS(cheFiltrosPES, rabNumerico, rabAlfabetico);

            //APAGA A VARIAVEL
            _ResultPesquisa = string.Empty;

            //LIMPA O GRID
            Dgv_Pesquisa.Rows.Clear();

            //EXECUTA A PESQUISA
            MET.Pesquisa_EXEC(_Login_LojaID_PesMsgNt, rabAlfabetico, rabNumerico, Dgv_Pesquisa);

            //DA O SELECT NO DGV
            Dgv_Pesquisa.Select();
        }

        //TECLAS DE ATALHO
        private void PesMsgNt_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    btnAjuda.PerformClick();
                    break;
                case Keys.F3:
                    rabNumerico.Checked = true;
                    break;
                case Keys.F4:
                    rabAlfabetico.Checked = true;
                    break;
                case Keys.F7:
                    btnFechar.PerformClick();
                    break;
            }
        }

        //GRAVA OS FILTROS
        private void PesMsgNt_FormClosing(object sender, FormClosingEventArgs e)
        {
            PesMsgNt_MET MET = new PesMsgNt_MET();
            MET.GravarFILTROS(cheFiltrosPES, rabNumerico, rabAlfabetico);
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


        #region Buttons Fechar e Ajuda
        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAjuda_Click(object sender, EventArgs e)
        {
            //EXIBE A AJUDA
            PesMsgNt_MET MET = new PesMsgNt_MET();
            MET.LinkAjuda();
        }
        #endregion


        #region POPULA POR OUTROS EVENTOS
        private void rabNumerico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabNumerico.Checked == true)
            {
                PesMsgNt_MET MET = new PesMsgNt_MET();
                //EXECUTA A PESQUISA
                MET.Pesquisa_EXEC(_Login_LojaID_PesMsgNt, rabAlfabetico, rabNumerico, Dgv_Pesquisa);
                Dgv_Pesquisa.Select();
            }
        }
        private void rabAlfabetico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabAlfabetico.Checked == true)
            {
                PesMsgNt_MET MET = new PesMsgNt_MET();
                //EXECUTA A PESQUISA
                MET.Pesquisa_EXEC(_Login_LojaID_PesMsgNt, rabAlfabetico, rabNumerico, Dgv_Pesquisa);
                Dgv_Pesquisa.Select();
            }
        }
        #endregion
    }
}
