using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PesTrans.cs
{
    internal partial class PesTrans : Form
    {
        public PesTrans()
        {
            InitializeComponent();
        }

        public string _Login_LojaID_PesTrans { get; set; }
        public string _Login_UsuarioID_PesTrans { get; set; }

        public string _ResultPesquisa { get; set; }

        //LOAD DO FORMULARIO
        private void PesTrans_Load(object sender, EventArgs e)
        {
            PesTrans_MET MET = new PesTrans_MET();

            //VERIFICA SE É PARA GRAVAR OS FILTROS
            MET.cheFILTROSChecked(cheFiltrosPES, _Login_LojaID_PesTrans);

            //CONTROLES PARA MUDAR A COR
            Control[] Controles = new Control[] { panDownPesq, txtQtSelectPES, panDownPesq, panDownPesq, panDownPesq, panDownPesq, panDownPesq };
            //MÉTODO DA CFG
            CfgComun.CfgComun_CLASS CFG = new CfgComun.CfgComun_CLASS();
            CFG.Cfg_SelecionaCorConfig(Controles, _Login_LojaID_PesTrans);

            #region CANCELA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged -= new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged -= new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged -= new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged -= new EventHandler(rabTOP_CheckedChanged);
            nupQtResultados.ValueChanged -= new EventHandler(nupQtResultados_ValueChanged);
            comStatus.SelectedIndexChanged -= new EventHandler(comStatus_SelectedIndexChanged);
            comVeiculo.SelectedIndexChanged -= new EventHandler(comVeiculo_SelectedIndexChanged);
            #endregion
            //CARREGA OS FILTROS
            MET.CarregaFILTROS(panPrinPesq);
            #region VOLTA OS MÉTODOS PARA A PESQUISA
            rabNumerico.CheckedChanged += new EventHandler(rabNumerico_CheckedChanged);
            rabAlfabetico.CheckedChanged += new EventHandler(rabAlfabetico_CheckedChanged);
            rabTodos.CheckedChanged += new EventHandler(rabTodos_CheckedChanged);
            rabTOP.CheckedChanged += new EventHandler(rabTOP_CheckedChanged);
            comStatus.SelectedIndexChanged += new EventHandler(comStatus_SelectedIndexChanged);
            comVeiculo.SelectedIndexChanged += new EventHandler(comVeiculo_SelectedIndexChanged);
            if (rabTOP.Checked == true) { nupQtResultados.Enabled = true; }
            #endregion

            //APAGA A VARIAVEL
            _ResultPesquisa = string.Empty;

            //LIMPA O GRID
            Dgv_Pesquisa.Rows.Clear();




            //SELECIONA O TEXTBOX
            txtDescri.Select();
        }

        //TECLAS DE ATALHO
        private void PesTrans_KeyDown(object sender, KeyEventArgs e)
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
        private void PesTrans_FormClosing(object sender, FormClosingEventArgs e)
        {
            PesTrans_MET MET = new PesTrans_MET();
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



        //ABRE A PESQUISA DE CIDADE
        private void txtCidadeCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                if (txtCidadeCod.Text != string.Empty)
                {
                    txtCidadeCod.Text = txtCidadeCod.Text.PadLeft(6, '0');
                }

                PesCidad.cs.PesCidad_CALL Call = new PesCidad.cs.PesCidad_CALL();
                Call._Login_CryptCode = _Login_LojaID_PesTrans;
                Call._Login_CryptDesc = _Login_UsuarioID_PesTrans;
                Call._WenCrypt = "PesCidad3Wenemy3156!.350?°";
                Call.PesCidad_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtCidadeCod.Text = Call._ResultPesquisaCALL;
                }

                txtCidadeCod.SelectAll();
            }
        }
        //ABRE A PESQUISA DE EMPRESAS
        private void txtEmpresa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                PesEmpre.cs.PesEmpre_CALL Call = new PesEmpre.cs.PesEmpre_CALL();
                Call._Login_CryptCode = _Login_LojaID_PesTrans;
                Call._Login_CryptDesc = _Login_UsuarioID_PesTrans;
                Call._WenCrypt = "PesEmpre5Wenemy3156!.350?°";
                Call.PesEmpre_AUTORIZADO();

                if (Call._ResultPesquisaCALL != string.Empty)
                {
                    txtEmpresa.Text = Call._ResultPesquisaCALL;
                }

                txtEmpresa.SelectAll();
            }
        }




        //RECEBE APENAS NÚMEROS
        #region Apenas Números
        private void txtCidadeCod_KeyPress(object sender, KeyPressEventArgs e)
        {
            PesTrans_MET MET = new PesTrans_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtCpfCnpj_KeyPress(object sender, KeyPressEventArgs e)
        {
            PesTrans_MET MET = new PesTrans_MET();
            MET.MET_ApenasNúmeros(e);
        }
        private void txtEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            PesTrans_MET MET = new PesTrans_MET();
            MET.MET_ApenasNúmeros(e);
        }
        #endregion


        //SELECT ALL NO MOUSE DOWN
        #region SelectAll
        private void txtDescri_MouseDown(object sender, MouseEventArgs e)
        {
            txtDescri.SelectAll();
        }
        private void txtFantasia_MouseDown(object sender, MouseEventArgs e)
        {
            txtFantasia.SelectAll();
        }
        private void txtCidadeCod_MouseDown(object sender, MouseEventArgs e)
        {
            txtCidadeDesc.Text = string.Empty;
            txtCidadeCod.SelectAll();
        }
        private void txtCpfCnpj_MouseDown(object sender, MouseEventArgs e)
        {
            txtCpfCnpj.SelectAll();
        }
        private void txtPlaca_MouseDown(object sender, MouseEventArgs e)
        {
            txtPlaca.SelectAll();
        }
        private void txtEmpresa_MouseDown(object sender, MouseEventArgs e)
        {
            txtEmpresa.SelectAll();
        }
        #endregion




        #region Buttons Pesquisar, Fecher e Ajuda
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            PesTrans_MET MET = new PesTrans_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesTrans, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comVeiculo, txtCidadeCod, txtCidadeDesc, txtDescri, txtFantasia, txtCpfCnpj, txtPlaca, txtEmpresa);
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
            PesTrans_MET MET = new PesTrans_MET();
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
                PesTrans_MET MET = new PesTrans_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesTrans, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comVeiculo, txtCidadeCod, txtCidadeDesc, txtDescri, txtFantasia, txtCpfCnpj, txtPlaca, txtEmpresa);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void rabAlfabetico_CheckedChanged(object sender, EventArgs e)
        {
            if (rabAlfabetico.Checked == true)
            {
                //EXECUTA A PESQUISA
                PesTrans_MET MET = new PesTrans_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesTrans, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comVeiculo, txtCidadeCod, txtCidadeDesc, txtDescri, txtFantasia, txtCpfCnpj, txtPlaca, txtEmpresa);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void rabTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTodos.Checked == true)
            {
                //EXECUTA A PESQUISA
                PesTrans_MET MET = new PesTrans_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesTrans, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comVeiculo, txtCidadeCod, txtCidadeDesc, txtDescri, txtFantasia, txtCpfCnpj, txtPlaca, txtEmpresa);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void rabTOP_CheckedChanged(object sender, EventArgs e)
        {
            if (rabTOP.Checked == true)
            {
                nupQtResultados.Enabled = true;

                //EXECUTA A PESQUISA
                PesTrans_MET MET = new PesTrans_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesTrans, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comVeiculo, txtCidadeCod, txtCidadeDesc, txtDescri, txtFantasia, txtCpfCnpj, txtPlaca, txtEmpresa);
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
                PesTrans_MET MET = new PesTrans_MET();
                MET.Pesquisa_EXEC(_Login_LojaID_PesTrans, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comVeiculo, txtCidadeCod, txtCidadeDesc, txtDescri, txtFantasia, txtCpfCnpj, txtPlaca, txtEmpresa);
                txtDescri.Select(); txtDescri.SelectAll();
            }
        }
        private void txtDescri_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesTrans_MET MET = new PesTrans_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesTrans, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comVeiculo, txtCidadeCod, txtCidadeDesc, txtDescri, txtFantasia, txtCpfCnpj, txtPlaca, txtEmpresa);
        }
        private void txtFantasia_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesTrans_MET MET = new PesTrans_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesTrans, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comVeiculo, txtCidadeCod, txtCidadeDesc, txtDescri, txtFantasia, txtCpfCnpj, txtPlaca, txtEmpresa);
        }
        private void txtCpfCnpj_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesTrans_MET MET = new PesTrans_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesTrans, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comVeiculo, txtCidadeCod, txtCidadeDesc, txtDescri, txtFantasia, txtCpfCnpj, txtPlaca, txtEmpresa);
        }
        private void txtPlaca_TextChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesTrans_MET MET = new PesTrans_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesTrans, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comVeiculo, txtCidadeCod, txtCidadeDesc, txtDescri, txtFantasia, txtCpfCnpj, txtPlaca, txtEmpresa);
        }
        private void comStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesTrans_MET MET = new PesTrans_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesTrans, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comVeiculo, txtCidadeCod, txtCidadeDesc, txtDescri, txtFantasia, txtCpfCnpj, txtPlaca, txtEmpresa);
            txtDescri.Select(); txtDescri.SelectAll();
        }
        private void comVeiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EXECUTA A PESQUISA
            PesTrans_MET MET = new PesTrans_MET();
            MET.Pesquisa_EXEC(_Login_LojaID_PesTrans, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comVeiculo, txtCidadeCod, txtCidadeDesc, txtDescri, txtFantasia, txtCpfCnpj, txtPlaca, txtEmpresa);
            txtDescri.Select(); txtDescri.SelectAll();
        }
      
        #endregion

        //VERIFICA SE A EMPRESA EXISTE NO LEAVE
        private void txtEmpresa_Leave(object sender, EventArgs e)
        {
            PesTrans_MET MET = new PesTrans_MET();
            MET.SelectEmpresa(txtEmpresa);
        }
        //VERIFICA SE A CIDADE EXISTE NO LEAVE
        private void txtCidadeCod_Leave(object sender, EventArgs e)
        {
            PesTrans_MET MET = new PesTrans_MET();
            MET.SelectCidade(1, txtCidadeCod, txtCidadeDesc, txtFantasia);
        }
        //VERIFICA SE A CIDADE EXISTE NO TAB E FAZ A PESQUISA
        private void txtCidadeCod_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtCidadeCod.Text != string.Empty)
            {
                PesTrans_MET MET = new PesTrans_MET();
                MET.SelectCidade(2, txtCidadeCod, txtCidadeDesc, txtFantasia);

                //EXECUTA A PESQUISA
                MET.Pesquisa_EXEC(_Login_LojaID_PesTrans, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comVeiculo, txtCidadeCod, txtCidadeDesc, txtDescri, txtFantasia, txtCpfCnpj, txtPlaca, txtEmpresa);
            }
        }
        //APAGA A DESCRIÇÃO DA CIDADE
        private void txtCidadeCod_TextChanged(object sender, EventArgs e)
        {
            txtCidadeDesc.Text = string.Empty;
        }
        //EXECUTA A PESQUISA NO TAB DA EMPRESA
        private void txtEmpresa_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && txtEmpresa.Text != string.Empty)
            {
                PesTrans_MET MET = new PesTrans_MET();
                MET.SelectEmpresa(txtEmpresa);

                //EXECUTA A PESQUISA
                MET.Pesquisa_EXEC(_Login_LojaID_PesTrans, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comVeiculo, txtCidadeCod, txtCidadeDesc, txtDescri, txtFantasia, txtCpfCnpj, txtPlaca, txtEmpresa);
            }
        }

    }
}
