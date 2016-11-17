using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Security;
using System.Security.Cryptography;

namespace TechSIS_BWK
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



            try
            {
                //SUBSTITUI O ARQUIVO DE ATUALIZAÇÃO E VERIFICA SE JÁ ESTÁ EM EXECUÇÃO
                TechSIS_MET MET = new TechSIS_MET();
                MET.MET_SubsTechATU();

                //VERIFICA SE A PASTA CONEXÃO EXISTE
                //VERIFICA SE O ARQUIVO STRINGCONEXÃO EXISTE
                //VERIFICA SE O ARQUIVO DADOS SERVIDOR
                bool VERI = MET.MET_VerificaArquivosDiretorios();
                if (!VERI) { } else { return; }

                //FAZ A CONEXÃO COM O SQL SERVER
                bool Conec = MET.MET_ConecTESTEBanco();
                if (!Conec) { } else { return; }

                //FAZ A CONEXÃO COM O SQL SERVER PELO ARQUIVO DE DADOS
                bool ConecDads = MET.MET_VerificaArquivosValidos();
                if (!ConecDads) { } else { return; }


                Application.Run(new TechSIS_BWK());

            }
            //catch (SqlException Ex)
            //{
            //    MessageBox.Show("OCORREU UM ERRO SEM TRATAMENTO NO SISTEMA. (SqlException)\n\n" + Ex.Message, "TechSIS MAIN Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Application.ExitThread();
            //}
            catch (FileLoadException Ex)
            {
                MessageBox.Show("OCORREU UM ERRO SEM TRATAMENTO NO SISTEMA. (FileLoadException)\n\n" + Ex.Message, "TechSIS MAIN ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
            }
            catch (FileNotFoundException Ex)
            {
                MessageBox.Show("OCORREU UM ERRO SEM TRATAMENTO NO SISTEMA. (FileNotFoundException)\n\n" + Ex.Message, "TechSIS MAIN ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
            }
            //catch (Exception Ex)
            //{
            //    MessageBox.Show("OCORREU UM ERRO SEM TRATAMENTO NO SISTEMA. (Exception)\n\n" + Ex.Message, "TechSIS MAIN " + Ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Application.ExitThread();
            //}
        }
    }
}