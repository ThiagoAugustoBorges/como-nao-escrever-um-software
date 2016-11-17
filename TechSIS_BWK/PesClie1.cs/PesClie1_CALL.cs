using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace PesClie1.cs
{
    public class PesClie1_CALL
    {
        //RECEBE O CÓDIGO DA EMPRESA
        public string _Login_CryptCode { get; set; }
        //RECEBE O CÓDIGO DO USUÁRIO
        public string _Login_CryptDesc { get; set; }


        //RECEBE A STRING DO RESULTADO DA PESQUISA
        public string _ResultPesquisaCALL { get; set; }


        //RECEBE A SENHA DE AUTORIZAÇÃO
        public string _WenCrypt { get; set; }


        //VERIFICO SE OS ARQUIVOS DE TODO O SISTEMA EXISTE
        private bool VerFILES()
        {
            try
            {
                int LojaLogadaFormatada = Convert.ToInt32(_Login_CryptCode);

                string CaminhoSecF = "..\\Debug\\SecF_" + LojaLogadaFormatada.ToString("00") + ".xml";
                string CaminhoWenFILE = "..\\Debug\\TechSIS_" + LojaLogadaFormatada.ToString("00") + "_WenFILE.ini";
                string CaminhoStringConexão = "..\\Conexão\\StringConexão.xml";
                string CaminhoDLLCfg = "..\\Debug\\CfgComun.dll";


                if (!File.Exists(CaminhoSecF) || !File.Exists(CaminhoWenFILE) || !File.Exists(CaminhoStringConexão) || !File.Exists(CaminhoDLLCfg))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }


        //EXECUTA O MÉTODO QUE CHAMA O FORMULÁRIO
        public void PesClie1_AUTORIZADO()
        {
            string MENSAGEM = "PROGRAMADOR SEM AUTORIZAÇÃO DE USO DA DLL\nERRO.: ";


            if (!VerFILES())
            {
                try
                {
                    if (_WenCrypt == "PesClie13Wenemy3156!.350?°")
                    {
                        if (String.IsNullOrEmpty(_Login_CryptCode) || (String.IsNullOrEmpty(_Login_CryptDesc)))
                        {
                            MessageBox.Show(MENSAGEM + "1", "TechSIS ERRO FATAL", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                        else if (Convert.ToInt32(_Login_CryptCode) <= 0 || Convert.ToInt32(_Login_CryptDesc) <= 0)
                        {
                            MessageBox.Show(MENSAGEM + "2", "TechSIS ERRO FATAL", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                        else
                        {
                            PesClie1 Call = new PesClie1();
                            Call._Login_LojaID_PesClie1 = _Login_CryptCode;
                            Call._Login_UsuarioID_PesClie1 = _Login_CryptDesc;
                            Call.ShowDialog();
                            _ResultPesquisaCALL = Call._ResultPesquisa;
                        }
                    }
                    else
                    {
                        MessageBox.Show(MENSAGEM + "3", "TechSIS ERRO FATAL", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(MENSAGEM + "4", "TechSIS ERRO FATAL", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("ARQUIVO(S) DE INICIALIZAÇÃO DE ASSEMBLY NÃO ENCONTRADO(S)", "TechSIS ERRO FATAL", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

    }
}
