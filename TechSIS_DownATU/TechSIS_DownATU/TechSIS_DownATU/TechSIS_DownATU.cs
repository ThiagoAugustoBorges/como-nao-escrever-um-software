using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;


namespace TechSIS_DownATU
{
    public partial class TechSIS_DownATU : Form
    {
        public TechSIS_DownATU()
        {
            InitializeComponent();
        }

        //INSTANCIO A CLASSE
        DownATU_MET MET = new DownATU_MET();






        //LOAD DO FORMULÁRIO
        private void TechSIS_DownATU_Load(object sender, EventArgs e)
        {
            #region CONTROLES
            MET.lblInf = lblInf;
            MET.proBar = proBar;
            MET.Formu = this;
            #endregion
        }

        //EXECUTO A THREAD
        private void TechSIS_DownATU_Shown(object sender, EventArgs e)
        {
            lblInf.Text = "PROCURANDO ATUALIZAÇÕES....";
            Thread ThMET = new Thread(new ThreadStart(MET.MET_ProcurarDownloadAtualizacoes));
            ThMET.Start();
        }

        //ABRE CONEC BANCO
        private void TechSIS_DownATU_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists("..\\Debug\\TechSIS_ConecBanco.exe"))
            {
                Process AbrirConec = new Process();
                AbrirConec.StartInfo.FileName = ("..\\Debug\\TechSIS_ConecBanco.exe");
                AbrirConec.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                AbrirConec.Start();
            }
            else
            {
                MessageBox.Show("Executável de instalação não encontrado no diretório.", "TechSIS DownATU", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
