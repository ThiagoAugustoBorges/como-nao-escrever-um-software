using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TechSIS_BWK
{
    internal partial class BackUp : Form
    {
        public BackUp()
        {
            InitializeComponent();
        }

        public string Servidor { get; set; }
        public string BancoDeDados { get; set; }
        public string CodigoLoja { get; set; }


        //Popula o lvl servidor com a maquina sendo ou não o servidor
        private void BackUp_Load(object sender, EventArgs e)
        {
            TechSIS_MET MET = new TechSIS_MET();
            MET.MET_SelecionaCorFundo(Painel_Down, Painel_Down, Painel_Down, Painel_Down, Painel_Down, Painel_Down, CodigoLoja);
            lblServidorRES.Text = Servidor;
        }


        //Fecha o formulário
        private void btnCancela_Click(object sender, EventArgs e)
        {
            Close();
        }


        //Define que apenas o servidor faz o backup do banco
        private void comTipoBackup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comTipoBackup.SelectedIndex == 1 && lblServidorRES.Text == "NÃO")
            {
                MessageBox.Show("A maquina atual não é o servidor do banco de dados", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comTipoBackup.SelectedIndex = 0;
                return;
            }
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            if (comTipoBackup.SelectedIndex < 0)
            {
                MessageBox.Show("Seleciona um tipo de BackUp primeiramente", "TechSIS BWK Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comTipoBackup.Select(); comTipoBackup.SelectAll();
            }
            if (comTipoBackup.SelectedIndex == 0)
            {
                SaveFILE.Filter = "Arquivo RAR|*.rar";
                SaveFILE.FileName = @"BackUp TechSIS SISTEMA";
                DialogResult QUAL = SaveFILE.ShowDialog();
                if (QUAL == DialogResult.Cancel)
                {

                }
                else
                {
                    TechSIS_BackUp BackUp = new TechSIS_BackUp();
                    BackUp.BackUpSistema(SaveFILE.FileName, proBar);
                }
            }
            if (comTipoBackup.SelectedIndex == 1)
            {
                SaveFILE.Filter = "Arquivo de Banco SQL Server|*.bak";
                SaveFILE.FileName = @"BackUp TechSIS " + DateTime.Today.ToString("D");
                DialogResult QUAL = SaveFILE.ShowDialog();
                if (QUAL == DialogResult.Cancel)
                {

                }
                else
                {
                    TechSIS_BackUp BackUp = new TechSIS_BackUp();
                    BackUp.BackUpBanco(SaveFILE.FileName, BancoDeDados, proBar);
                }
            }
            if (comTipoBackup.SelectedIndex == 2)
            {
                SaveFILE.Filter = "Arquivo RAR|*.rar";
                SaveFILE.FileName = @"BackUp TechSIS FILTROS";
                DialogResult QUAL = SaveFILE.ShowDialog();
                if (QUAL == DialogResult.Cancel)
                {

                }
                else
                {
                    TechSIS_BackUp BackUp = new TechSIS_BackUp();
                    BackUp.BackUpFIL(SaveFILE.FileName, proBar);
                }
            }
        }

        private void btnBackAutoma_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desculpe. BackUp automático apenas na versão Business ou superior", "TechSIS Auto-Manutenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
