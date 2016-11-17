using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Resources;

namespace TechSIS_UpdSIS
{
    internal class UpdSIS_MET
    {
        #region CONTROLES
        public Panel panPrin { get; set; }
        public Label lblInf1 { get; set; }
        public Label lblInf2 { get; set; }
        public Label lblInf3 { get; set; }
        public Label lblInf4 { get; set; }
        public ProgressBar proBar { get; set; }
        public Form FormularioPRIN { get; set; }
        public Button btnProcurar { get; set; }
        public Button btnAtualizar { get; set; }
        public NotifyIcon Tray_Form { get; set; }
        #endregion
        #region DELEGATES DA THREAD
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

        //Troca o value de uma progressbar
        delegate void TrocarProgressBarValueDelegate(ProgressBar NomeProgress, int newMaximum, int newValue, bool newVisible);
        private void TrocarProgressBarValue(ProgressBar NomeProgress, int newMaximum, int newValue, bool newVisible)
        {
            if (NomeProgress.InvokeRequired)
            {
                TrocarProgressBarValueDelegate lblDelegate = new TrocarProgressBarValueDelegate(TrocarProgressBarValue);
                NomeProgress.Invoke(lblDelegate, new object[] { NomeProgress, newMaximum, newValue, newVisible });
            }
            else
            {
                NomeProgress.Maximum = newMaximum;
                NomeProgress.Value = newValue;
                NomeProgress.Visible = newVisible;
            }
        }

        //Ativa ou desativa um button
        delegate void TrocarButtonEnableDelegat(Button NomeButton, bool newEnable);
        private void TrocarButtonEnable(Button NomeButton, bool newEnable)
        {
            if (NomeButton.InvokeRequired)
            {
                TrocarButtonEnableDelegat lblDelegate = new TrocarButtonEnableDelegat(TrocarButtonEnable);
                NomeButton.Invoke(lblDelegate, new object[] { NomeButton, newEnable });
            }
            else
            {
                NomeButton.Enabled = newEnable;
                if (newEnable == true)
                {
                    NomeButton.Select();
                    NomeButton.Focus();
                }
            }
        }
        #endregion

        static double ConvertBytesToKilobytes(long bytes)
        {
            return (bytes / 1024f);
        }

        internal const string UsuarFTP = "techsis";
        internal const string SenhaFTP = "3156350";
        internal const string FPT_HOST = "ftp://ftp.xpg.com.br/";
        internal const string CaminhoTechSTATE = FPT_HOST + "Tech_UPDATE/TechSTATE.tech";

        //ARMAZENA O NOME DOS ARQUIVOS DESATUALIZADOS
        internal List<string> ListNomeArquivosDesatualizados = new List<string> { };
        internal List<string> ListTipoArquivosDesatualizados = new List<string> { };
        internal List<string> ListPastas = new List<string> { "DLLs", "COMs" };

        //ARMAZENO OS ARQUIVOS .RAR PARA SEREM DESCOMPACTADOS
        internal List<string> ListArquivosRAR = new List<string> { };


        //VERIFICA SE WINRAR ESTÁ INSTALADO
        public bool MET_VerificaWinRAR()
        {
            if (File.Exists((Environment.CurrentDirectory) + "\\WinRAR.exe"))
            {
                return false;
            }
            else
            {
                MessageBox.Show("WinRAR.exe não foi encontrado no diretório atual.", "TechSIS Upd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
        }

        //VERIFICA SE O SISTEMA TechSIS BWK ESTÁ EM EXECUÇÃO
        public bool MET_VerificaSistemaEmExecucao()
        {
            System.Diagnostics.Process[] Proc = System.Diagnostics.Process.GetProcessesByName("TechSIS_BWK");
            if (Proc.Length > 0)
            {
                DialogResult Continua = MessageBox.Show("O menu de aplicação TechSIS está em execução no momento. Para evitar problemas durante a atualização dos componentes e bibliotecas o sistema terá que ser encerrado.\n\nDeseja executar o encerramento do sistema agora e continuar?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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



        //PROCURO AS ATUALIZAÇÕES E JOGO OS NOMES E O TIPO NA LIST
        public void MET_ProcurarAtualizacoes()
        {
            try
            {
                ListArquivosRAR.Clear();
                ListNomeArquivosDesatualizados.Clear();
                ListTipoArquivosDesatualizados.Clear();

                for (int z = 0; z < ListPastas.Count; z++)
                {
                    //CRIO A REQUISIÇÃO
                    FtpWebRequest Request = (FtpWebRequest)WebRequest.Create(FPT_HOST + "Tech_UPDATE/" + ListPastas[z] + "/");
                    Request.Method = WebRequestMethods.Ftp.ListDirectory;

                    //CRIO AS CREDENCIAIS
                    Request.Credentials = new NetworkCredential(UsuarFTP, SenhaFTP);
                    FtpWebResponse Resposta = (FtpWebResponse)Request.GetResponse();

                    StreamReader StreamRead = new StreamReader(Request.GetResponse().GetResponseStream());

                    //PEGO TODOS OS ARQUIVOS EXISTENTES NA PASTA
                    string[] NomeArquivo = StreamRead.ReadToEnd().Trim().Split('\n');

                    int NumeroDeAtualizacoes = 0;

                    for (int NumArq = 0; NumArq < NomeArquivo.Length; NumArq++)
                    {
                        //PREENCHO O TEXTO DOS LABELS
                        TrocarLabelText(lblInf1, "ATUALIZAÇÃO.: " + NomeArquivo[NumArq]);
                        TrocarLabelText(lblInf2, "EXTENSÃO....: " + ListPastas[z].Substring(0, 3));


                        //CRIO UMA NOVA REQUISIÇÃO PARA COMPARAR AS DATAS
                        WebRequest Requisicao = (FtpWebRequest)WebRequest.Create(FPT_HOST + "Tech_UPDATE/" + ListPastas[z] + "/" + NomeArquivo[NumArq]);
                        Requisicao.Credentials = new NetworkCredential(UsuarFTP, SenhaFTP);
                        Requisicao.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                        Resposta = (FtpWebResponse)Requisicao.GetResponse();

                        #region CAMINHO DAS PASTAS
                        string CaminhoPASTA = "";
                        if (ListPastas[z] == "COMs")
                        {
                            CaminhoPASTA = "..\\Temp\\";
                            if (!Directory.Exists(CaminhoPASTA))
                            {
                                Directory.CreateDirectory(CaminhoPASTA);
                            }
                        }
                        if (ListPastas[z] == "IMGs")
                        {
                            CaminhoPASTA = "..\\Imagens\\";
                            if (!Directory.Exists(CaminhoPASTA))
                            {
                                Directory.CreateDirectory(CaminhoPASTA);
                            }
                        }
                        #endregion

                        //COMPARO A DATA DO FTP, COM A DATA LOCAL NO COMPUTADOR
                        string DATA_FPT = (Resposta.LastModified.ToString("dd/MM/yyyy HH:mm:ss"));
                        string DATA_LOC = File.GetLastWriteTime(CaminhoPASTA + NomeArquivo[NumArq]).ToString("dd/MM/yyyy HH:mm:ss");

                        if (Convert.ToDateTime(DATA_FPT) > Convert.ToDateTime(DATA_LOC))
                        {
                            ListNomeArquivosDesatualizados.Add(NomeArquivo[NumArq]);
                            ListTipoArquivosDesatualizados.Add(ListPastas[z]);
                            NumeroDeAtualizacoes++;
                            TrocarLabelText(lblInf4, "STATUS.....: DESATUALIZADA");
                        }
                        else
                        {
                            TrocarLabelText(lblInf4, "STATUS.....: ATUALIZADA");
                        }
                    }

                    StreamRead.Close();
                    Resposta.Close();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Detectamos um erro ao executar o método de busca.\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Upd ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                TrocarLabelText(lblInf1, string.Empty);
                TrocarLabelText(lblInf2, string.Empty);
                TrocarLabelText(lblInf3, string.Empty);
                TrocarLabelText(lblInf4, string.Empty);

                MET_VerificoAsAtualizacoes();
            }
        }

        //VERIFICO SE EXISTE OU NÃO ATUALIZAÇÕES
        public void MET_VerificoAsAtualizacoes()
        {
            if (ListNomeArquivosDesatualizados.Count > 0)
            {
                TrocarButtonEnable(btnAtualizar, true);
                TrocarButtonEnable(btnProcurar, false);
            }
            else
            {
                TrocarButtonEnable(btnAtualizar, false);
                TrocarButtonEnable(btnProcurar, true);
                TrocarLabelText(lblInf1, "Não foram encontradas atualizações disponíveis.");
            }
        }

        //BAIXO AS ATUALIZAÇÕES
        public void MET_BaixoAsAtualizacoes()
        {
            try
            {
                //CRIO  O WebClient
                WebClient FTPWebClient = new WebClient();
                FTPWebClient.Credentials = new NetworkCredential(UsuarFTP, SenhaFTP);
                FTPWebClient.Proxy = null;

                byte[] ArquivoBIT;

                int NumeroATUAL = 0;

                for (int i = 0; i < ListNomeArquivosDesatualizados.Count; i++)
                {
                    ArquivoBIT = null;

                    TrocarProgressBarValue(proBar, ListNomeArquivosDesatualizados.Count, NumeroATUAL, true);

                    string NomeDoArquivo = ListNomeArquivosDesatualizados[i].Trim();
                    string Pasta = ListTipoArquivosDesatualizados[i].Trim();

                    #region CAMINHO DAS PASTAS
                    string CaminhoPASTA = "";
                    if (Pasta == "COMs")
                    {
                        CaminhoPASTA = "..\\Temp\\";
                        ListArquivosRAR.Add(NomeDoArquivo);
                        if (!Directory.Exists(CaminhoPASTA))
                        {
                            Directory.CreateDirectory(CaminhoPASTA);
                        }
                    }
                    else if (Pasta == "IMGs")
                    {
                        CaminhoPASTA = "..\\Imagens\\";
                        if (!Directory.Exists(CaminhoPASTA))
                        {
                            Directory.CreateDirectory(CaminhoPASTA);
                        }
                    }
                    #endregion


                    string FTP_FULL = FPT_HOST + "Tech_UPDATE/" + Pasta + "/" + NomeDoArquivo;


                    TrocarLabelText(lblInf1, "ATUALIZAÇÃO.: " + NomeDoArquivo);
                    TrocarLabelText(lblInf2, "PASTA FTP...: " + Pasta);
                    TrocarLabelText(lblInf4, "STATUS.....: EFETUANDO DOWNLOAD");


                    ArquivoBIT = FTPWebClient.DownloadData(FTP_FULL);
                    TrocarLabelText(lblInf3, "TAMANHO EM KB.: " + ConvertBytesToKilobytes(ArquivoBIT.Length));

                    FileStream FILE = File.Create(CaminhoPASTA + NomeDoArquivo);
                    FILE.Write(ArquivoBIT, 0, ArquivoBIT.Length);
                    FILE.Close();


                    NumeroATUAL++;
                }


                //DELETO OS ANTIGOS exe PARA NÃO PARA O .RAR
                MET_DeletarArquivosExtraidos();
                //EXTRAIO OS ARQUIVOS
                MET_ExtrairArquivoRAR();
                //COPIO PARA A PASTA DEBUG
                MET_CopiarParaPastaDebug();
                //BAIXO O ULTIMO ARQUIVO TechSTATE
                MET_BaixoTechSTATE();
            }
            catch (WebException Ex)
            {
                MessageBox.Show("Foi detectado um erro ao realizar o download do arquivo.\nVerifique as atualizações e faça o download novamente!\n\nDETALHES DO ERRO \n" + Ex.GetType().ToString() + "\n" + Ex.Message, "TechSIS Upd ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                TrocarLabelText(lblInf1, string.Empty);
                TrocarLabelText(lblInf2, string.Empty);
                TrocarLabelText(lblInf3, string.Empty);
                TrocarLabelText(lblInf4, string.Empty);
                TrocarProgressBarValue(proBar, 0, 0, false);

                TrocarButtonEnable(btnAtualizar, false);
                TrocarButtonEnable(btnProcurar, true);
            }
        }





        //BAIXO O ULTIMO ARQUIVO TechSTATE
        public void MET_BaixoTechSTATE()
        {
            try
            {
                //CRIO  O WebClient
                WebClient FTPWebClient = new WebClient();
                FTPWebClient.Credentials = new NetworkCredential(UsuarFTP, SenhaFTP);
                FTPWebClient.Proxy = null;

                byte[] ArquivoBIT = FTPWebClient.DownloadData(CaminhoTechSTATE);

                FileStream FILE = File.Create("TechSTATE.tech");
                FILE.Write(ArquivoBIT, 0, ArquivoBIT.Length);
                FILE.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Detectamos um erro ao efetuar o download TechSTATE.", "TechSIS Upd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //VERIFICO A DATA DO ARQUIVO TechSTATE
        public int MET_VerificaTechSTATE()
        {
            try
            {
                //CRIO UMA NOVA REQUISIÇÃO PARA COMPARAR AS DATAS
                WebRequest Requisicao = (FtpWebRequest)WebRequest.Create(CaminhoTechSTATE);
                Requisicao.Credentials = new NetworkCredential(UsuarFTP, SenhaFTP);
                Requisicao.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                FtpWebResponse Resposta = (FtpWebResponse)Requisicao.GetResponse();

                //COMPARO A DATA DO FTP, COM A DATA LOCAL NO COMPUTADOR
                string DATA_FPT = (Resposta.LastModified.ToString("dd/MM/yyyy HH:mm:ss"));
                string DATA_LOC = File.GetLastWriteTime("TechSTATE.tech").ToString("dd/MM/yyyy HH:mm:ss");

                if (Convert.ToDateTime(DATA_FPT) > Convert.ToDateTime(DATA_LOC))
                {
                    // 1 = TEM ATUALIZAÇÃO
                    return 1;
                }
                else
                {
                    // 0 = NÃO TEM ATUALIZAÇÃO
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }






        //EXTRAIO OS ARQUIVOS
        public void MET_ExtrairArquivoRAR()
        {
            string NomeDoArquivo = "..\\Temp\\RAR_TechSIS.bat";


            using (StreamWriter Wr = new StreamWriter(NomeDoArquivo, false, Encoding.Default))
            {
                DirectoryInfo Temp = new DirectoryInfo("..\\Temp");
                string DirectoryTemp = Temp.FullName;

                Wr.WriteLine("cd\\");
                Wr.WriteLine("cd " + Environment.CurrentDirectory);
                for (int i = 0; i < ListArquivosRAR.Count; i++)
                {
                    //NÃO CRIO PARA A PARTE 02 DO EXECUTAVEL
                    if (!ListArquivosRAR[i].Contains(".part2.rar"))
                    {
                        Wr.WriteLine("winrar e -o+ " + "\"" + DirectoryTemp + "\\" + ListArquivosRAR[i] + "" + "\"" + " " + "\"" + DirectoryTemp + "\"");
                    }
                }
            }

            Process Rodar_BAT = new Process();
            Rodar_BAT.StartInfo.FileName = NomeDoArquivo;
            Rodar_BAT.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Rodar_BAT.StartInfo.ErrorDialog = false;
            Rodar_BAT.StartInfo.Verb = "runas";
            Rodar_BAT.StartInfo.WorkingDirectory = Path.GetDirectoryName(NomeDoArquivo);
            Rodar_BAT.Start();
            Rodar_BAT.WaitForExit();

           
            File.Delete(NomeDoArquivo);
        }

        //COPIO PARA A PASTA DEBUG
        public void MET_CopiarParaPastaDebug()
        {
            DirectoryInfo ArquivosDIR_TEMP = new DirectoryInfo("..\\Temp");
            DirectoryInfo ArquivosDIR_DEBUG = new DirectoryInfo("..\\Debug");
            FileInfo[] FilesTEMP = ArquivosDIR_TEMP.GetFiles("*.exe");
            FileInfo[] FilesDEBUG = ArquivosDIR_DEBUG.GetFiles("*.exe");

            //PERCORRO TODOS OS ARQUIVOS DA PASTA TEMP
            for (int i = 0; i < FilesTEMP.Length; i++)
            {
                //SE O ARQUIVO DA PASTA TEMP NÃO EXISTE NA DEBUG EU COPIO
                if (!File.Exists("..\\Debug\\" + FilesTEMP[i].Name))
                {
                    File.Copy(FilesTEMP[i].FullName, "..\\Debug\\" + FilesTEMP[i].Name, true);
                }
                //CASO EXISTA
                else
                {
                    //EU VERIFICO TODOS NA PASTA DEBUG
                    foreach (FileInfo ArquivoDEBUG in FilesDEBUG)
                    {
                        //PROCURO O MESMO ARQUIVO EM COMUM NA PASTA TEMP E PASTA DEBUG
                        if (ArquivoDEBUG.Name == FilesTEMP[i].Name)
                        {
                            //SE FOREM ARQUIVOS EM COMUM, EU COMPARO AS DASTAS
                            DateTime DATA_DEBUG = File.GetLastWriteTime(ArquivoDEBUG.FullName);
                            DateTime DATA_TEMP = File.GetLastWriteTime(FilesTEMP[i].FullName);

                            //SE DATA FOR MAIOR NA DEBUG, E NÃO FOR O EXE DE ATUALIZAÇÃO, EU COPIO
                            if (DATA_TEMP > DATA_DEBUG && FilesTEMP[i].Name != "TechSIS_UpdSIS.exe")
                            {
                                File.Copy(FilesTEMP[i].FullName, "..\\Debug\\" + FilesTEMP[i].Name, true);
                            }
                        }
                    }
                }
            }

            //DELETO OS EXECUTAVEIS EXTRAIDOS
            for (int k = 0; k < FilesTEMP.Length; k++)
            {
                File.Delete(FilesTEMP[k].FullName);
            }

        }


        //DELETO OS EXECUTÁVEIS ANTES DE EXTRAIR
        public void MET_DeletarArquivosExtraidos()
        {
            DirectoryInfo ArquivosDIR_TEMP = new DirectoryInfo("..\\Temp");
            FileInfo[] FilesDEBUG = ArquivosDIR_TEMP.GetFiles("*.exe");

            for (int i = 0; i < FilesDEBUG.Length; i++)
            {
                FilesDEBUG[i].Delete();
            }
        }


    }
}