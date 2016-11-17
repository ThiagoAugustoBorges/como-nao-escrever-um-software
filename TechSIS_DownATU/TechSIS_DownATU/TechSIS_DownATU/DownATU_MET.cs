using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Windows.Forms;


namespace TechSIS_DownATU
{
    internal class DownATU_MET
    {
        internal const string UsuarFTP = "techsis";
        internal const string SenhaFTP = "3156350";
        internal const string FPT_HOST = "ftp://ftp.xpg.com.br/Tech_INST/";

        //Método da API
        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);
        //VERIFICA SE EXISTE CONEXÃO COM A INTERNET
        public Boolean IsConnected()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }



        #region DELEGATES
        //Troca o texto de uma label
        delegate void TrocarLabelTextDelegate(Label NomeLabel, string newText);
        private void TrocarLabelText(Label NomeLabel, string newText)
        {
            if (NomeLabel.InvokeRequired)
            {
                TrocarLabelTextDelegate lblDelegate = new TrocarLabelTextDelegate(TrocarLabelText);
                NomeLabel.Invoke(lblDelegate, new object[] { NomeLabel, newText });
            }
            else
            {
                NomeLabel.Text = newText;
            }
        }

        //Fechar o formulário
        delegate void FecharFormDelegate(Form NomeForm);
        private void FecharForm(Form NomeForm)
        {
            if (NomeForm.InvokeRequired)
            {
                FecharFormDelegate lblDelegate = new FecharFormDelegate(FecharForm);
                NomeForm.Invoke(lblDelegate, new object[] { NomeForm });
            }
            else
            {
                NomeForm.Close();
            }
        }

        //Valor da progressBar
        delegate void ValueProgressBarDelegate(ProgressBar proBar, int newValue);
        private void ProgressBarValue(ProgressBar proBar, int newValue)
        {
            if (proBar.InvokeRequired)
            {
                ValueProgressBarDelegate lblDelegate = new ValueProgressBarDelegate(ProgressBarValue);
                proBar.Invoke(lblDelegate, new object[] { proBar,newValue });
            }
            else
            {
                proBar.Value = newValue;
            }
        }
        #endregion
        #region CONTROLES
        public Label lblInf { get; set; }
        public ProgressBar proBar { get; set; }
        public Form Formu { get; set; }
        #endregion


        //ICONES DEVEM TER NOMES MANUAIS NO EXECUTÁVEL
        /*
         * LISTA DE ICONES A SEREM BAIXADOS 
         * 
         * TechSIS_ConecBanco
         * Scripts do Sistema
         * Arquivos .tech
         */


        internal List<string> listPastas = new List<string> { "TechARQUIVOs", "TechSCRIPTs" };
        

        //PROCURO AS ATUALIZAÇÕES E FAÇO O DOWNLOAD
        public void MET_ProcurarDownloadAtualizacoes()
        {
            try
            {
                //CRIO  O WebClient
                WebClient FTPWebClient = new WebClient();
                FTPWebClient.Credentials = new NetworkCredential(UsuarFTP, SenhaFTP);
                FTPWebClient.Proxy = null;

                byte[] ArquivoBIT;

                for (int z = 0; z < listPastas.Count; z++)
                {
                    //CRIO A REQUISIÇÃO
                    FtpWebRequest Request = (FtpWebRequest)WebRequest.Create(FPT_HOST + listPastas[z] + "/");
                    Request.Method = WebRequestMethods.Ftp.ListDirectory;

                    //CRIO AS CREDENCIAIS
                    Request.Credentials = new NetworkCredential(UsuarFTP, SenhaFTP);
                    FtpWebResponse Resposta = (FtpWebResponse)Request.GetResponse();

                    StreamReader StreamRead = new StreamReader(Request.GetResponse().GetResponseStream());

                    //PEGO TODOS OS ARQUIVOS EXISTENTES NA PASTA
                    string[] NomeArquivo = StreamRead.ReadToEnd().Trim().Split('\n');

                    for (int NumArq = 0; NumArq < NomeArquivo.Length; NumArq++)
                    {
                        ArquivoBIT = null;
 

                        string NomeDoArquivo = NomeArquivo[NumArq].Trim();
                        string Pasta = listPastas[z];

                        #region CAMINHO DAS PASTAS
                        string CaminhoPASTA = "";
                        if (Pasta == "TechARQUIVOs")
                        {
                            CaminhoPASTA = "..\\Tech FILEs\\";
                            if (!Directory.Exists(CaminhoPASTA))
                            {
                                Directory.CreateDirectory(CaminhoPASTA);
                            }
                        }
                        if (Pasta == "TechSCRIPTs")
                        {
                            CaminhoPASTA = "..\\Scripts\\";
                            if (!Directory.Exists(CaminhoPASTA))
                            {
                                Directory.CreateDirectory(CaminhoPASTA);
                            }
                        }
                        #endregion


                        string FTP_FULL = FPT_HOST + Pasta + "/" + NomeDoArquivo;


                        TrocarLabelText(lblInf, "DOWNLOAD.: " + NomeDoArquivo);

                        ProgressBarValue(proBar, 2);
                        ArquivoBIT = FTPWebClient.DownloadData(FTP_FULL);
                        ProgressBarValue(proBar, 3);

                        FileStream FILE = File.Create(CaminhoPASTA + NomeDoArquivo);
                        FILE.Write(ArquivoBIT, 0, ArquivoBIT.Length);
                        FILE.Close();
                    }

                    StreamRead.Close();
                    Resposta.Close();
                }



                //BAIXO O EXECUTÁVEL
                MET_DownloadConecBanco();

                //EXTRAIO OS ARQUIVOS
                MET_ExtrairArquivosRAR();

                //APAGO OS .RAR E JOGO O EXECUTAVEL NA TEMP
                MET_CopiarArquivosExtraidos();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "TechSIS: ERRO AO REALIZAR DOWNLOAD.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //DELEGATE PARA FECHAR O FORMULÁRIO
                FecharForm(Formu);

                TrocarLabelText(lblInf, string.Empty);

                ////DELETA O .RAR
                File.Delete("..\\Debug\\RAR_DownATU.bat");
            }
        }
        //BAIXO O EXECUTÁVEL ConecBanco
        public void MET_DownloadConecBanco()
        {
            try
            {
                string DiretorioSalvamento = "..\\Temp";

                if (!Directory.Exists(DiretorioSalvamento))
                {
                    Directory.CreateDirectory(DiretorioSalvamento);
                }


                //CRIO  O WebClient
                WebClient FTPWebClient = new WebClient();
                FTPWebClient.Credentials = new NetworkCredential(UsuarFTP, SenhaFTP);
                FTPWebClient.Proxy = null;

                TrocarLabelText(lblInf, "BAIXANDO O EXECUTÁVEL DE INSTALAÇÃO...");

                byte[] ArquivoBIT;
                ArquivoBIT = FTPWebClient.DownloadData("ftp://ftp.xpg.com.br/Tech_UPDATE/COMs/TechSIS_ConecBanco.rar");

                FileStream FILE = File.Create(DiretorioSalvamento + "\\TechSIS_ConecBanco.rar");
                FILE.Write(ArquivoBIT, 0, ArquivoBIT.Length);
                FILE.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + "\n MET_DownloadConecBanco", "TechSIS DownATU ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //EXRAIO OS ARQUIVOS
        public void MET_ExtrairArquivosRAR()
        {
            try
            {
                string NomeDoArquivo = "RAR_DownATU.bat";

                using (StreamWriter Wr = new StreamWriter(NomeDoArquivo, true, Encoding.Default))
                {
                    DirectoryInfo Temp = new DirectoryInfo("..\\Temp");
                    DirectoryInfo Script = new DirectoryInfo("..\\Scripts");
                    DirectoryInfo TechFILES = new DirectoryInfo("..\\Tech FILEs");
                    string DirectoryTemp = Temp.FullName;
                    string DirectoryScript = Script.FullName;
                    string DirectoryTechFILES = TechFILES.FullName;

                    Wr.WriteLine("cd\\");
                    Wr.WriteLine("cd " + Environment.CurrentDirectory);
                    Wr.WriteLine("winrar e -o+ " + "\"" + DirectoryTemp + "\\TechSIS_ConecBanco.rar" + "" + "\"" + " " + "\"" + DirectoryTemp + "\"");
                    Wr.WriteLine("winrar e -o+ " + "\"" + DirectoryTechFILES + "\\TechFILEs.rar" + "" + "\"" + " " + "\"" + DirectoryTechFILES + "\"");
                    Wr.WriteLine("winrar e -o+ " + "\"" + DirectoryScript + "\\Scripts TechSIS.rar" + "" + "\"" + " " + "\"" + DirectoryScript + "\"");
                }


                Process Rodar_BAT = new Process();
                Rodar_BAT.StartInfo.FileName = NomeDoArquivo;
                Rodar_BAT.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Rodar_BAT.StartInfo.Verb = "runas";
                Rodar_BAT.Start();
                Rodar_BAT.WaitForExit();
                Rodar_BAT.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + "\n MET_ExtrairArquivosRAR", "TechSIS DownATU ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //COPIA OS ARQUIVOS
        public void MET_CopiarArquivosExtraidos()
        {
            try
            {
                if (File.Exists("..\\Scripts\\Scripts TechSIS.rar"))
                    File.Delete("..\\Scripts\\Scripts TechSIS.rar");
                if (File.Exists("..\\Tech FILEs\\TechFILEs.rar"))
                    File.Delete("..\\Tech FILEs\\TechFILEs.rar");
                if (File.Exists("..\\Temp\\TechSIS_ConecBanco.rar"))
                    File.Delete("..\\Temp\\TechSIS_ConecBanco.rar");

                if (File.Exists("..\\Temp\\TechSIS_ConecBanco.exe"))
                    File.Copy("..\\Temp\\TechSIS_ConecBanco.exe", "..\\Debug\\TechSIS_ConecBanco.exe", true);


                File.Delete("..\\Temp\\TechSIS_ConecBanco.exe");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + "\n MET_CopiarArquivosExtraidos", "TechSIS DownATU ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //VERIFICA SE ConecBanco ESTÁ RODANDO
        public bool MET_VerificaConecBancoRodando()
        {
            System.Diagnostics.Process[] Proc = System.Diagnostics.Process.GetProcessesByName("TechSIS_ConecBanco");
            if (Proc.Length > 0)
            {
                DialogResult Continua = MessageBox.Show("O menu de instalação TechSIS está em execução no momento. Para evitar problemas durante a atualização dos componentes e bibliotecas o sistema terá que ser encerrado.\n\nDeseja executar o encerramento do sistema agora e continuar?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Continua == DialogResult.Yes)
                {
                    Proc[0].Kill();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        //VERIFICA SE WINRAR ESTÁ INSTALADO
        public bool MET_VerificaWinRAR()
        {
            if (File.Exists((Environment.CurrentDirectory) + "\\WinRAR.exe"))
            {
                return false;
            }
            else
            {
                MessageBox.Show("WinRAR.exe não foi encontrado no diretório atual.", "TechSIS DownATU", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
        }
    }
}