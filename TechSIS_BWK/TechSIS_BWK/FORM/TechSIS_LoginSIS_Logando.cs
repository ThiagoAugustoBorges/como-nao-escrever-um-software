using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TechSIS_BWK
{
    internal partial class TechSIS_LoginSIS_Logando : Form
    {
        public TechSIS_LoginSIS_Logando()
        {
            InitializeComponent();
        }


        public string Login_LojaID { get; set; }

        //APLICA OS EFEITOS VISUAIS
        private void TechSIS_LoginSIS_Logando_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            proBar.Value = 35;
            Application.DoEvents();
            System.Threading.Thread.Sleep(300);
            Application.DoEvents();
            proBar.Value = 45;
            Application.DoEvents();
            System.Threading.Thread.Sleep(300);
            Application.DoEvents();
            proBar.Value = 85;
            Application.DoEvents();
            System.Threading.Thread.Sleep(300);
            Application.DoEvents();
            proBar.Value = 98;
            Application.DoEvents();
            System.Threading.Thread.Sleep(300);
            Application.DoEvents();
            proBar.Value = 100;

            this.Close();
        }


        //1 - FAZ INSERT GENERICO NA HISTORICO E CONFIG GERAL CASO NÃO EXISTA
        //2 - VERIFICA SE A MÚSICA É PARA SER TOCADA
        private void TechSIS_LoginSIS_Logando_FormClosed(object sender, FormClosedEventArgs e)
        {
            TechSIS_MET MET = new TechSIS_MET();
            //VERIFICA SE TEM HISTÓRICO E CONFIGURAÇÃO
            //SE NÃO EXISTIR, FAZ UM INSERT GENERICO
            MET.MET_VerHISTVerCONF(1, Login_LojaID);
            MET.MET_VerHISTVerCONF(2, Login_LojaID);

            //VERIFICA SE É PARA TOCAR A MÚSICA
            TechSIS_LoginMET LogMET = new TechSIS_LoginMET();
            LogMET.LOG_VerificaMusica(Login_LojaID);
        }
    }
}