﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Security;
using System.Security.Cryptography;
using System.Xml;

namespace TabProgr
{
    internal class TabProgr_FILTROS
    {
        #region CRIPTOGRAFIA
        public static string WenProtects(string Message)
        {
            string senha = "“3.!156350WeNeMy”";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(senha));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }
        public static string WenDisprotects(string Message)
        {
            string senha = "“3.!156350WeNeMy”";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(senha));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }
        #endregion

        //Verifica se é para gravar os filtros
        public void cheFILTROSChecked(CheckBox cheFILTROSImp, CheckBox cheFILTROSPes, string CodigoLoja)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            string GravaXML = "SELECT GraXML_CON FROM TabConfi WHERE SeqLoj_CON = " + CodigoLoja;
            SqlCommand Comando = new SqlCommand(GravaXML, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    if (Dr["GraXML_CON"].ToString() == "True")
                    {
                        cheFILTROSImp.Checked = true;
                        cheFILTROSPes.Checked = true;
                    }
                    else
                    {
                        cheFILTROSImp.Checked = false;
                        cheFILTROSPes.Checked = false;
                    }
                }
                else
                {
                    cheFILTROSImp.Checked = false;
                    cheFILTROSPes.Checked = false;
                }

                Dr.Close();
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método cheFILTROSChecked()\n\nBLOCO = CLASSE TabProgr_FILTROS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método cheFILTROSChecked()\n\nBLOCO = CLASSE TabProgr_FILTROS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //LANÇA O USUÁRIO PARA A VIDEO AULA
        public void LinkAjuda()
        {
            string Patch = "..\\Debug\\VideoAulas.XML";
            string NomeDoNó = "TabProgr";
            try
            {

                if (File.Exists(Patch))
                {
                    DialogResult Deseja = MessageBox.Show("Deseja abrir o conteúdo da Video Aula?", "TechSIS Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (Deseja == DialogResult.Yes)
                    {

                        XmlDocument LerXML = new XmlDocument();
                        LerXML.Load(Patch);

                        XmlNode VideoAula_XML = LerXML.DocumentElement.SelectSingleNode(NomeDoNó);
                        string VideoAula = WenDisprotects(VideoAula_XML.InnerText.ToString());

                        System.Diagnostics.Process.Start(VideoAula);
                    }
                }
                else
                {
                    MessageBox.Show("Arquivo de Video Aulas não foi encontrado.\nFaça a atualização automática do sistema para solucionar o problema.\n\n\b ..\\Debug\\VideoAulas.XML", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (XmlException)
            {
                MessageBox.Show("Arquivo de Video Aulas está corrompido ou é inexistente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show("Faça a atualização automática do sistema para solucionar o problema.", "TechSIS Aviso.: " + NomeDoNó, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Arquivo de Video Aulas está corrompido ou é inexistente", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show("Faça a atualização automática do sistema para solucionar o problema.", "TechSIS Aviso.: " + NomeDoNó, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        //Grava o xml dos filtros informados
        public void GravarFILTROS(CheckBox cheFiltrosPES, CheckBox cheFiltrosIMP, RadioButton rabNumerico, RadioButton rabAlfabetica, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, ComboBox comStatusPES, RadioButton rabOrdemNumerica, RadioButton rabOrdemAlfabetica, RadioButton rabRPV, RadioButton rabWORD, RadioButton rabEXCEL, RadioButton rabTXT, ComboBox comSituacaoIMP, TextBox txtCaminhoRel, CheckBox cheVoltarLix)
        {
            #region CRIA O DIRETÓRIO SE NÃO EXISTIR
            if (!Directory.Exists("..\\Log"))
            {
                Directory.CreateDirectory("..\\Log");
            }
            #endregion

            try
            {
                string Patch = @"..\Log\TabProgr.xml";
                XmlTextWriter GerarXML = new XmlTextWriter(Patch, null);
                GerarXML.WriteStartDocument();
                GerarXML.WriteStartElement("Dados_TabProgr");
                //PESQUISA
                GerarXML.WriteElementString("cheFiltrosPES", WenProtects(cheFiltrosPES.Checked.ToString()));
                GerarXML.WriteElementString("rabNumerico", WenProtects(rabNumerico.Checked.ToString()));
                GerarXML.WriteElementString("rabAlfabetica", WenProtects(rabAlfabetica.Checked.ToString()));
                GerarXML.WriteElementString("rabTodos", WenProtects(rabTodos.Checked.ToString()));
                GerarXML.WriteElementString("rabTOP", WenProtects(rabTOP.Checked.ToString()));
                GerarXML.WriteElementString("nupQtResultados", WenProtects(nupQtResultados.Value.ToString()));
                GerarXML.WriteElementString("comStatusPES", WenProtects(comStatusPES.SelectedIndex.ToString()));
                //IMPRESSÃO
                GerarXML.WriteElementString("cheFiltrosIMP", WenProtects(cheFiltrosIMP.Checked.ToString()));
                GerarXML.WriteElementString("rabOrdemNumerica", WenProtects(rabOrdemNumerica.Checked.ToString()));
                GerarXML.WriteElementString("rabOrdemAlfabetica", WenProtects(rabOrdemAlfabetica.Checked.ToString()));
                GerarXML.WriteElementString("rabRPV", WenProtects(rabRPV.Checked.ToString()));
                GerarXML.WriteElementString("rabWORD", WenProtects(rabWORD.Checked.ToString()));
                GerarXML.WriteElementString("rabEXCEL", WenProtects(rabEXCEL.Checked.ToString()));
                GerarXML.WriteElementString("comSituacaoIMP", WenProtects(comSituacaoIMP.SelectedIndex.ToString()));
                GerarXML.WriteElementString("txtCaminhoRel", WenProtects(txtCaminhoRel.Text));
                //LIXEIRA
                GerarXML.WriteElementString("cheVoltarLix", WenProtects(cheVoltarLix.Checked.ToString()));

                GerarXML.WriteEndElement();
                GerarXML.Close();
            }
            catch (XmlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarFILTROS()\n\nBLOCO = CLASSE TabProgr_FILTROS\n\n" + Ex.Message, "TechSIS XML Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarFILTROS()\n\nBLOCO = CLASSE TabProgr_FILTROS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Carrega o XML dos filtros
        public void CarregaFILTROS(CheckBox cheFiltrosPES, CheckBox cheFiltrosIMP, RadioButton rabNumerico, RadioButton rabAlfabetica, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, ComboBox comStatusPES, RadioButton rabOrdemNumerica, RadioButton rabOrdemAlfabetica, RadioButton rabRPV, RadioButton rabWORD, RadioButton rabEXCEL, RadioButton rabTXT, ComboBox comSituacaoIMP, TextBox txtCaminhoRel, CheckBox cheVoltarLix)
        {
            string Patch = @"..\Log\TabProgr.xml";
            try
            {
                if (File.Exists(Patch))
                {
                    XmlDocument LerXML = new XmlDocument();
                    LerXML.Load(Patch);
                    //PESQUISA
                    XmlNode cheFiltrosPES_xml = LerXML.DocumentElement.SelectSingleNode("cheFiltrosPES");
                    XmlNode rabNumerico_xml = LerXML.DocumentElement.SelectSingleNode("rabNumerico");
                    XmlNode rabAlfabetica_xml = LerXML.DocumentElement.SelectSingleNode("rabAlfabetica");
                    XmlNode rabTodos_xml = LerXML.DocumentElement.SelectSingleNode("rabTodos");
                    XmlNode rabTOP_xml = LerXML.DocumentElement.SelectSingleNode("rabTOP");
                    XmlNode nupQtResultados_xml = LerXML.DocumentElement.SelectSingleNode("nupQtResultados");
                    XmlNode comStatusPES_xml = LerXML.DocumentElement.SelectSingleNode("comStatusPES");

                    //IMPRESSÃO
                    XmlNode cheFiltrosIMP_xml = LerXML.DocumentElement.SelectSingleNode("cheFiltrosIMP");
                    XmlNode rabRPV_xml = LerXML.DocumentElement.SelectSingleNode("rabRPV");
                    XmlNode rabWORD_xml = LerXML.DocumentElement.SelectSingleNode("rabWORD");
                    XmlNode rabEXCEL_xml = LerXML.DocumentElement.SelectSingleNode("rabEXCEL");
                    XmlNode rabOrdemNumerica_xml = LerXML.DocumentElement.SelectSingleNode("rabOrdemNumerica");
                    XmlNode rabOrdemAlfabetica_xml = LerXML.DocumentElement.SelectSingleNode("rabOrdemAlfabetica");
                    XmlNode comSituacaoIMP_xml = LerXML.DocumentElement.SelectSingleNode("comSituacaoIMP");
                    XmlNode txtCaminhoRel_xml = LerXML.DocumentElement.SelectSingleNode("txtCaminhoRel");

                    //LIXEIRA
                    XmlNode cheVoltarLix_xml = LerXML.DocumentElement.SelectSingleNode("cheVoltarLix");


                    #region STRING PESQUISA
                    string _cheFiltrosPES = WenDisprotects(cheFiltrosPES_xml.InnerText.ToString());
                    string _rabNumerico = WenDisprotects(rabNumerico_xml.InnerText.ToString());
                    string _rabAlfabetica = WenDisprotects(rabAlfabetica_xml.InnerText.ToString());
                    string _rabTodos = WenDisprotects(rabTodos_xml.InnerText.ToString());
                    string _rabTOP = WenDisprotects(rabTOP_xml.InnerText.ToString());
                    string _nupQtResultados = WenDisprotects(nupQtResultados_xml.InnerText.ToString());
                    string _comStatusPES = WenDisprotects(comStatusPES_xml.InnerText.ToString());
                    #endregion

                    #region STRING IMPRESSÃO
                    string _cheFiltrosIMP = WenDisprotects(cheFiltrosIMP_xml.InnerText.ToString());
                    string _rabRPV = WenDisprotects(rabRPV_xml.InnerText.ToString());
                    string _rabWORD = WenDisprotects(rabWORD_xml.InnerText.ToString());
                    string _rabEXCEL = WenDisprotects(rabEXCEL_xml.InnerText.ToString());
                    string _rabOrdemNumerica = WenDisprotects(rabOrdemNumerica_xml.InnerText.ToString());
                    string _rabOrdemAlfabetica = WenDisprotects(rabOrdemAlfabetica_xml.InnerText.ToString());
                    string _comSituacaoIMP = WenDisprotects(comSituacaoIMP_xml.InnerText.ToString());
                    string _txtCaminhoRel = WenDisprotects(txtCaminhoRel_xml.InnerText.ToString());
                    #endregion

                    #region STRING LIXEIRA
                    string _cheVoltarLix = WenDisprotects(cheVoltarLix_xml.InnerText.ToString());
                    #endregion




                    #region TRATAMENTO PESQUISA
                    if (_cheFiltrosPES == "True")
                    {
                        rabNumerico.Checked = Convert.ToBoolean(_rabNumerico);
                        rabAlfabetica.Checked = Convert.ToBoolean(_rabAlfabetica);
                        rabTodos.Checked = Convert.ToBoolean(_rabTodos);
                        rabTOP.Checked = Convert.ToBoolean(_rabTOP);
                        if (_rabTOP == "True")
                        {
                            nupQtResultados.Value = Convert.ToInt32(_nupQtResultados);
                        }

                        comStatusPES.SelectedIndex = Convert.ToInt32(_comStatusPES);
                    }
                    else
                    {
                        rabNumerico.Checked = false;
                        rabAlfabetica.Checked = false;
                        rabTodos.Checked = false;
                        rabTOP.Checked = false;
                        nupQtResultados.Value = 0;
                        comStatusPES.SelectedIndex = -1;
                    }
                    #endregion

                    #region TRATAMENTO IMPRESSÃO
                    if (_cheFiltrosIMP == "True")
                    {
                        rabRPV.Checked = Convert.ToBoolean(_rabRPV);
                        rabWORD.Checked = Convert.ToBoolean(_rabWORD);
                        rabEXCEL.Checked = Convert.ToBoolean(_rabEXCEL);
                        rabOrdemNumerica.Checked = Convert.ToBoolean(_rabOrdemNumerica);
                        rabOrdemAlfabetica.Checked = Convert.ToBoolean(_rabOrdemAlfabetica);
                        comSituacaoIMP.SelectedIndex = Convert.ToInt32(_comSituacaoIMP);
                        txtCaminhoRel.Text = _txtCaminhoRel;
                    }
                    else
                    {
                        rabRPV.Checked = false;
                        rabWORD.Checked = false;
                        rabEXCEL.Checked = false;
                        rabOrdemNumerica.Checked = false;
                        rabOrdemAlfabetica.Checked = false;
                        txtCaminhoRel.Text = string.Empty;
                        comSituacaoIMP.SelectedIndex = -1;
                    }
                    #endregion

                    #region TRATAMENTO LIXEIRA
                    if (_cheVoltarLix == "True")
                    {
                        cheVoltarLix.Checked = true;
                    }
                    #endregion
                }
            }

            catch (Exception)
            {

            }
        }
    }
}