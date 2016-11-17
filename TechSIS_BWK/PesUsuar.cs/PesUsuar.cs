using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PesUsuar.cs
{
    internal partial class PesUsuar : Form
    {
        public PesUsuar()
        {
            InitializeComponent();
        }

        public string _Login_LojaID_PesUsuar { get; set; }
        public string _Login_UsuarioID_PesUsuar { get; set; }

        public string _ResultPesquisa { get; set; }

       
        //LOAD DO FORM
        private void PesUsuar_Load(object sender, EventArgs e)
        {
            PesUsuar_MET MET = new PesUsuar_MET();

            //VERIFICA SE É PARA GRAVAR OS FILTROS
            MET.cheFILTROSChecked(cheFiltrosPES, _Login_LojaID_PesUsuar);

            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panDownPesq, txtQtSelectPES, panDownPesq, panDownPesq, panDownPesq, panDownPesq, panDownPesq };
            //MÉTODO DA CFG
            CfgComun.CfgComun_CLASS CFG = new CfgComun.CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, _Login_LojaID_PesUsuar);

            #region CANCELA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged -= new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged -= new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged -= new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged -= new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged -= new EventHandler(nupQtResultados_ValueChanged);
            comStatus.SelectedIndexChanged -= new EventHandler(comStatus_SelectedIndexChanged);
            #endregion
            //CARREGA OS FILTROS
            MET.CarregaFILTROS(cheFiltrosPES, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comStatus);
            #region VOLTA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged += new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged += new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged += new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged += new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged += new EventHandler(nupQtResultados_ValueChanged);
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
        private void PesUsuar_FormClosing(object sender, FormClosingEventArgs e)
        {
            PesUsuar_MET MET = new PesUsuar_MET();
            //GRAVA OS FILTROS DA PESQUISA
            MET.GravarFILTROS(cheFiltrosPES, rabNumerico, rabAlfabetico, rabTodos, rabTOP, nupQtResultados, comStatus);
        }

        //TECLAS DE ATALHO
        private void PesUsuar_KeyDown(object sender, KeyEventArgs e)
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
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            PesUsuar_MET MET = new PesUsuar_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesUsuar, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriApelid, txtEmpresa, comStatus);
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
            PesUsuar_MET MET = new PesUsuar_MET();
            MET.LinkAjuda();
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



        //ATUALIZA A QT DE RESULTADOS ENCONTRADOS NA PESQUISA
        private void Dgv_Pesquisa_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }
        private void Dgv_Pesquisa_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            txtQtSelectPES.Text = Dgv_Pesquisa.Rows.Count.ToString("000000");
        }


        #region PESQUISA PELOS DIFERENTES EVENTOS
        private void txtDescri_TextChanged(object sender, EventArgs e)
        {
            //FAZ A PESQUISA
            PesUsuar_MET MET = new PesUsuar_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesUsuar, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriApelid, txtEmpresa, comStatus);
        }
        private void txtDescriApelid_TextChanged(object sender, EventArgs e)
        {
            //FAZ A PESQUISA
            PesUsuar_MET MET = new PesUsuar_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesUsuar, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriApelid, txtEmpresa, comStatus);
        }
        private void comStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FAZ A PESQUISA
            PesUsuar_MET MET = new PesUsuar_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesUsuar, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriApelid, txtEmpresa, comStatus);
            txtDescri.Select(); txtDescri.SelectAll();
        }
        private void rabNumerico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabNumerico.Checked == true)
            {
                //FAZ A PESQUISA
                PesUsuar_MET MET = new PesUsuar_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesUsuar, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriApelid, txtEmpresa, comStatus);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void rabAlfabetico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabAlfabetico.Checked == true)
            {
                //FAZ A PESQUISA
                PesUsuar_MET MET = new PesUsuar_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesUsuar, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriApelid, txtEmpresa, comStatus);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void rabTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTodos.Checked == true)
            {
                //FAZ A PESQUISA
                PesUsuar_MET MET = new PesUsuar_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesUsuar, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriApelid, txtEmpresa, comStatus);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void rabTOP_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTOP.Checked == true)
            {
                nupQtResultados.Enabled = true;

                //FAZ A PESQUISA
                PesUsuar_MET MET = new PesUsuar_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesUsuar, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriApelid, txtEmpresa, comStatus);
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
                //FAZ A PESQUISA
                PesUsuar_MET MET = new PesUsuar_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesUsuar, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtDescriApelid, txtEmpresa, comStatus);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        #endregion


        //VERIFICA SE A EMPRESA EXISTE
        private void txtEmpresa_Leave(object sender, EventArgs e)
        {
            if (txtEmpresa.Text != string.Empty)
            {
                PesUsuar_MET MET = new PesUsuar_MET();
                MET.SelectEmpresa(txtEmpresa);
            }
        }
        private void txtEmpresa_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtEmpresa.Text != string.Empty)
            {
                txtEmpresa.Text = txtEmpresa.Text.PadLeft(6, '0');
                PesUsuar_MET MET = new PesUsuar_MET();
                MET.SelectEmpresa(txtEmpresa);
            }
        }

        //SELECT ALL
        private void txtEmpresa_MouseDown(object sender, MouseEventArgs e)
        {
            txtEmpresa.SelectAll();
        }


        //APENAS NÚMEROS
        private void txtEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            PesUsuar_MET MET = new PesUsuar_MET();
            MET.MET_ApenasNúmeros(e);
        }

        //ABRE A PESQUISA DE EMPRESAS
        private void txtEmpresa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                txtEmpresa.Text = string.Empty;

                PesEmpre.cs.PesEmpre_CALL Call = new PesEmpre.cs.PesEmpre_CALL();
                Call._Login_CryptCode = _Login_LojaID_PesUsuar;
                Call._Login_CryptDesc = _Login_UsuarioID_PesUsuar;
                Call._WenCrypt = "PesEmpre5Wenemy3156!.350?°";
                Call.PesEmpre_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtEmpresa.Text = Call._ResultPesquisaCALL;
                }

                txtEmpresa.SelectAll();
            }
        }


        
    }
}
