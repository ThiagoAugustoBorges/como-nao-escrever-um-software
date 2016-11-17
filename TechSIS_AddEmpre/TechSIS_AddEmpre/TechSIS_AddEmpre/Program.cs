using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TechSIS_AddEmpre
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Argumento)
        {
            AddEmpre_MET MET = new AddEmpre_MET();

            bool EXISTE = MET.MET_ArquivoNecessarios();
            if (!EXISTE) { } else { return; }

            if (Argumento.Length > 0)
            {
                TechSIS_AddEmpre._TipoInicialização = Argumento[0].Substring(0, 1);
                TechSIS_AddEmpre._CorDeFundo = Argumento[0].Substring(1);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TechSIS_AddEmpre());
        }
    }
}
