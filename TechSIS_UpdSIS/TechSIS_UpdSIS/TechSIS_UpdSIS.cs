using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Resources;

namespace TechSIS_UpdSIS
{
    internal partial class TechSIS_UpdSIS : Form
    {
        public TechSIS_UpdSIS()
        {
            InitializeComponent();
        }

        //Instancio a classe de metodos
        UpdSIS_MET MET = new UpdSIS_MET();

        //Crio o Timer de verificação de atualizações
        System.Timers.Timer VerATU = new System.Timers.Timer();
        

        //PROPRIEDADES DO TRAY
        private void TechSIS_UpdSIS_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Tray.Visible = true;
            this.Visible = false;
            this.ShowInTaskbar = false;
            if (lblInf1.Text == "Não foram encontradas atualizações disponíveis.")
            {
                lblInf1.Text = string.Empty;
            }
        }
        private void Tray_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            Tray.Visible = false;
            this.Visible = true;
            this.Focus();
            foreach (Control Ctrl in panPrin.Controls)
            {
                Ctrl.Refresh();
            }
        }
        private void Tray_BalloonTipClicked(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            Tray.Visible = false;
            this.Visible = true;
            this.Focus();
            foreach (Control Ctrl in panPrin.Controls)
            {
                Ctrl.Refresh();
            }

            btnProcurar.PerformClick();
        }



        //LOAD DO FORMULÁRIO
        private void TechSIS_UpdSIS_Load(object sender, EventArgs e)
        {
            btnFechar.PerformClick();

            #region PASSA OS CONTROLES PARA A CLASSE
            MET.lblInf1 = lblInf1;
            MET.lblInf2 = lblInf2;
            MET.lblInf3 = lblInf3;
            MET.lblInf4 = lblInf4;
            MET.panPrin = panPrin;
            MET.proBar = proBar;
            MET.FormularioPRIN = this;
            MET.btnAtualizar = btnAtualizar;
            MET.btnProcurar = btnProcurar;
            MET.Tray_Form = Tray;
            #endregion

            VerATU.Enabled = true;
            VerATU.Elapsed += new System.Timers.ElapsedEventHandler(VerATU_Elapsed);
            VerATU.Interval = 360000; //PADRÃO = 360000 (6 minutos)
        }
        //EVENTO DO TIMER
        public void VerATU_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int ATU = MET.MET_VerificaTechSTATE();
            if (ATU == 1)
            {
                Tray.BalloonTipTitle = "TechSIS - Update";
                Tray.BalloonTipIcon = ToolTipIcon.Info;
                Tray.BalloonTipText = "Existem atualizações disponíveis no momento.\nClique aqui para atualizar o sistema agora!";
                Tray.ShowBalloonTip(10000);
            }
        }



        #region Buttons do Formulário
        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();  
        }
        private void btnProcurar_Click(object sender, EventArgs e)
        {            
            //VERIFICA SE O WinRAR ESTÁ NA PASTA
            bool WinRAR = MET.MET_VerificaWinRAR();
            if (!WinRAR) { } else { btnFechar.PerformClick(); return; }

            //VERIFICA SE O SISTEMA ESTÁ EM EXECUÇÃO
            bool EXE = MET.MET_VerificaSistemaEmExecucao();
            if (!EXE) { } else { btnFechar.PerformClick(); return; }

            btnProcurar.Enabled = false;
            Thread ThMET = new Thread(new ThreadStart(MET.MET_ProcurarAtualizacoes));
            ThMET.Start();
        }
        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            //VERIFICA SE O WinRAR ESTÁ NA PASTA
            bool WinRAR = MET.MET_VerificaWinRAR();
            if (!WinRAR) { } else { btnFechar.PerformClick(); return; }

            //VERIFICA SE O SISTEMA ESTÁ EM EXECUÇÃO
            bool EXE = MET.MET_VerificaSistemaEmExecucao();
            if (!EXE) { } else { btnFechar.PerformClick(); return; }

            btnAtualizar.Enabled = false;
            Thread ThMET = new Thread(new ThreadStart(MET.MET_BaixoAsAtualizacoes));
            ThMET.Start();

        }
        #endregion


        //TECLAS DE ATALHO
        private void TechSIS_UpdSIS_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    btnProcurar.PerformClick();
                    break;
                case Keys.F3:
                    btnAtualizar.PerformClick();
                    break;
                case Keys.F7:
                    btnFechar.PerformClick();
                    break;
                case Keys.F12:
                    DialogResult Fechar = MessageBox.Show("Deseja realmente sair do sistema de atualizações TechSIS?", "TechSIS Upd", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (Fechar == System.Windows.Forms.DialogResult.Yes)
                    {
                        Application.ExitThread();
                    }
                    break;
            }
        }

       
    }
}
