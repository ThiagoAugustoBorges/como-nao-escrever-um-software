using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace TechSIS_ConecBanco
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!Directory.Exists("..\\Scripts"))
            {
                if (File.Exists("TechSIS_DownATU.exe"))
                {
                    System.Diagnostics.Process.Start("TechSIS_DownATU.exe");
                }
                else
                {
                    MessageBox.Show("Falha na inicialização da configuração do software", "TechSIS Conec: Método MAIN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Application.Run(new ConecBanco_FormPrin());
            }
        }
    }
}