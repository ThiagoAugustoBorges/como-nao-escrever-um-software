using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace TechSIS_UpdSIS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string MyProc = Process.GetCurrentProcess().ProcessName;
            Process[] Proc = Process.GetProcessesByName(MyProc);
            // Verifica se além desta instância, já existe mais alguma?
            if (Proc != null && Proc.Length > 1)
            {
                MessageBox.Show("Aplicação de atualização do sistema já está em execução", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new TechSIS_UpdSIS());
            }
        }
    }
}