﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace TabConfg
{
    public class TabConfg_CALL
    {
        //RECEBE O CÓDIGO DA EMPRESA
        public string _Login_CryptCode { get; set; }
        //RECEBE O CÓDIGO DO USUÁRIO
        public string _Login_CryptDesc { get; set; }



        //RECEBE A DESCRIÇÃO DA LOJA
        public string _Login_Crypt01 { get; set; }
        //RECEBE A DESCRIÇÃO DO USUÁRIO
        public string _Login_Crypt02 { get; set; }


        public MenuStrip MenuStrip_FORM { get; set; }
        public Panel Panel_Opcoes { get; set; }
        public Panel Panel_Buttons { get; set; }
        public Panel Painel_Informações { get; set; }
        public Panel Painel_Erro { get; set; }

        public Button ATALHO1 { get; set; }
        public Button ATALHO2 { get; set; }

        public Color Cor_BackUp { get; set; }




        //RECEBE O FORMULÁRIO PAI
        public System.Windows.Forms.Form _FORM_PAI { get; set; }

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


                if (!System.IO.File.Exists(CaminhoSecF) || !System.IO.File.Exists(CaminhoWenFILE) || !System.IO.File.Exists(CaminhoStringConexão) || !System.IO.File.Exists(CaminhoDLLCfg))
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
        public void TabConfg_AUTORIZADO()
        {
            string MENSAGEM = "PROGRAMADOR SEM AUTORIZAÇÃO DE USO DA DLL\nERRO.: ";


            if (!VerFILES())
            {
                try
                {
                    if (_WenCrypt == "TabConfg3Wenemy3156!.350?°")
                    {
                        if (String.IsNullOrEmpty(_Login_CryptCode) || (String.IsNullOrEmpty(_Login_CryptDesc)))
                        {
                            System.Windows.Forms.MessageBox.Show(MENSAGEM + "1", "TechSIS ERRO FATAL", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                        else if (Convert.ToInt32(_Login_CryptCode) <= 0 || Convert.ToInt32(_Login_CryptDesc) <= 0)
                        {
                            System.Windows.Forms.MessageBox.Show(MENSAGEM + "2", "TechSIS ERRO FATAL", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                        else
                        {
                            TabConfg Call = new TabConfg();
                            Call._Login_LojaID_Confg = _Login_CryptCode;
                            Call._Login_UsuarioID_Confg = _Login_CryptDesc;
                            Call._Login_UsuarioDesc_Confg = _Login_Crypt02;
                            Call.MenuStrip_FORM = MenuStrip_FORM;
                            Call.Panel_Opcoes = Panel_Opcoes;
                            Call.Panel_Buttons = Panel_Buttons;
                            Call.Painel_Informações = Painel_Informações;
                            Call.Painel_Erro = Painel_Erro;
                            Call.Cor_BackUp = Painel_Erro.BackColor;
                            Call.ATALHO1 = ATALHO1;
                            Call.ATALHO2 = ATALHO2;
                            Call.Owner = _FORM_PAI;
                            Call.ShowDialog();
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show(MENSAGEM + "3", "TechSIS ERRO FATAL", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    System.Windows.Forms.MessageBox.Show(MENSAGEM + "4", "TechSIS ERRO FATAL", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("ARQUIVO(S) DE INICIALIZAÇÃO DE ASSEMBLY NÃO ENCONTRADO(S)", "TechSIS ERRO FATAL", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}