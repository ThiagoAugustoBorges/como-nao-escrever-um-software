using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TechSIS_DownATU
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DownATU_MET MET = new DownATU_MET();
            if (MET.IsConnected() == true)
            {   
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                bool Exec = MET.MET_VerificaConecBancoRodando();
                if (!Exec) { } else { return; }

                //VERIFICA SE O WinRAR ESTÁ NA PASTA
                bool WinRAR = MET.MET_VerificaWinRAR();
                if (!WinRAR) { } else { return; }

                Application.Run(new TechSIS_DownATU());
            }
            else
            {
                string nomeArquivoConecBanco = "TechSIS_ConecBanco.exe";

                if (System.IO.File.Exists(nomeArquivoConecBanco))
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
}
