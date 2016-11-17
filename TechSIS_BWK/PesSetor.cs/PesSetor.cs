using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PesSetor.cs
{
    internal partial class PesSetor : Form
    {
        public PesSetor()
        {
            InitializeComponent();
        }


        public string _Login_LojaID_PesSetor { get; set; }
        public string _Login_UsuarioID_PesSetor { get; set; }
        public string _TipoChamada { get; set; }
        public string _Setor { get; set; }

        public string _ResultPesquisa { get; set; }
        

        //LOAD DO FORM
        private void PesSetor_Load(object sender, EventArgs e)
        {
            PesSetor_MET MET = new PesSetor_MET();

            //VERIFICA SE É PARA GRAVAR OS FILTROS
            MET.cheFILTROSChecked(cheFiltrosPES, _Login_LojaID_PesSetor);

            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panDownPesq, txtQtSelectPES, panDownPesq, panDownPesq, panDownPesq, panDownPesq, panDownPesq };
            //MÉTODO DA CFG
            CfgComun.CfgComun_CLASS CFG = new CfgComun.CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, _Login_LojaID_PesSetor);

            #region CANCELA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged -= new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged -= new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged -= new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged -= new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged -= new EventHandler(nupQtResultados_ValueChanged);
            cheSubEspe.CheckedChanged -= new EventHandler(cheSubEspe_CheckedChanged);
            comStatus.SelectedIndexChanged -= new EventHandler(comStatus_SelectedIndexChanged);
            #endregion
            //CARREGA OS FILTROS
            MET.CarregaFILTROS(panPrinPesq);

            //DEFINE SE A PESQUISA É DE SETORES OU SUBSETORES
            #region TRATAMENTO SETOR\SUBSETOR
            if (_TipoChamada == "1")
            {
                this.Text = "TechSIS INF - Pesquisa de Setores        PesSetor.dll";
                lblDescri.Text = "DESCRIÇÃO DO SETOR";
                cheSubEspe.Checked = false;
                cheSubEspe.Enabled = false;
            }
            else
            {
                this.Text = "TechSIS INF - Pesquisa de SubSetores     PesSetor.dll";
                lblDescri.Text = "DESCRIÇÃO DO SUBSETOR (SETOR.: " + MET.SelectNomeSetor(_Setor) + ")";
                cheSubEspe.Enabled = true;
            }
            #endregion


            #region VOLTA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged += new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged += new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged += new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged += new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged += new EventHandler(nupQtResultados_ValueChanged);
            cheSubEspe.CheckedChanged += new EventHandler(cheSubEspe_CheckedChanged);
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
        private void PesSetor_FormClosing(object sender, FormClosingEventArgs e)
        {
            PesSetor_MET MET = new PesSetor_MET();
            MET.GravarFILTROS(panPrinPesq);
        }

        //TECLAS DE ATALHO
        private void PesSetor_KeyDown(object sender, KeyEventArgs e)
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



        //PREENCHE A VARIAVEL E FECHA O FORMULÁRIO
        private void Dgv_Pesquisa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                _ResultPesquisa = Dgv_Pesquisa.CurrentRow.Cells[0].Value.ToString();
                this.Close();
            }
        }




        #region Buttons Pesquisar, Fechar e Ajuda
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesSetor_MET MET = new PesSetor_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesSetor, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtLocali, txtRespon, comStatus, _TipoChamada, cheSubEspe, _Setor);
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
            PesSetor_MET MET = new PesSetor_MET();
            MET.LinkAjuda();

            txtDescri.Select(); txtDescri.SelectAll();
        }
        #endregion



        #region FAZ A PESQUISA PELOS EVENTOS
        private void rabNumerico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabNumerico.Checked == true)
            {
                //EXECUTA A PESQUISA
                PesSetor_MET MET = new PesSetor_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesSetor, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtLocali, txtRespon, comStatus, _TipoChamada, cheSubEspe, _Setor);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void rabAlfabetico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabAlfabetico.Checked == true)
            {
                //EXECUTA A PESQUISA
                PesSetor_MET MET = new PesSetor_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesSetor, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtLocali, txtRespon, comStatus, _TipoChamada, cheSubEspe, _Setor);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void rabTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTodos.Checked == true)
            {
                //EXECUTA A PESQUISA
                PesSetor_MET MET = new PesSetor_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesSetor, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtLocali, txtRespon, comStatus, _TipoChamada, cheSubEspe, _Setor);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void rabTOP_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTOP.Checked == true)
            {
                nupQtResultados.Enabled = true;

                //EXECUTA A PESQUISA
                PesSetor_MET MET = new PesSetor_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesSetor, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtLocali, txtRespon, comStatus, _TipoChamada, cheSubEspe, _Setor);
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
                PesSetor_MET MET = new PesSetor_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesSetor, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtLocali, txtRespon, comStatus, _TipoChamada, cheSubEspe, _Setor);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void txtDescri_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesSetor_MET MET = new PesSetor_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesSetor, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtLocali, txtRespon, comStatus, _TipoChamada, cheSubEspe, _Setor);
        }
        private void txtLocali_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesSetor_MET MET = new PesSetor_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesSetor, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtLocali, txtRespon, comStatus, _TipoChamada, cheSubEspe, _Setor);
        }
        private void txtRespon_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesSetor_MET MET = new PesSetor_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesSetor, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtLocali, txtRespon, comStatus, _TipoChamada, cheSubEspe, _Setor);
        }
        private void comStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesSetor_MET MET = new PesSetor_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesSetor, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtLocali, txtRespon, comStatus, _TipoChamada, cheSubEspe, _Setor);
            txtDescri.Select(); txtDescri.SelectAll();
        }
        private void cheSubEspe_CheckedChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesSetor_MET MET = new PesSetor_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesSetor, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, txtLocali, txtRespon, comStatus, _TipoChamada, cheSubEspe, _Setor);
            if (cheSubEspe.Checked == true)
            {
                if (txtQtSelectPES.Text == string.Empty) { txtQtSelectPES.Text = "000000"; }
                if (Convert.ToInt32(txtQtSelectPES.Text) == 0)
                {
                    MessageBox.Show("Não existe subsetor específico para o setor informado!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cheSubEspe.Checked = false;
                }
            }

            txtDescri.Select(); txtDescri.SelectAll();
        }
        #endregion



        //SELECT ALL NO CLICK
        private void txtDescri_MouseDown(object sender, MouseEventArgs e)
        {
            txtDescri.SelectAll();
        }
        private void txtLocali_MouseDown(object sender, MouseEventArgs e)
        {
            txtLocali.SelectAll();
        }
        private void txtRespon_MouseDown(object sender, MouseEventArgs e)
        {
            txtRespon.SelectAll();
        }












    }
}
