using System;
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

namespace TabClien
{
    internal class TabClien_FILTROS
    {
        #region CRIPTOGRAFIA
        internal static string WenProtect(string Message)
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
        internal static string WenDisprotect(string Message)
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

        //LANÇA O USUÁRIO PARA A VIDEO AULA
        public void LinkAjuda()
        {
            string Patch = "..\\Debug\\VideoAulas.XML";
            string NomeDoNó = "TabClien";
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
                        string VideoAula = WenDisprotect(VideoAula_XML.InnerText.ToString());

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

        //VERIFICA SE É PARA GRAVAR OS FILTROS
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
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método cheFILTROSChecked()\n\nBLOCO = CLASSE TabClien_FILTROS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método cheFILTROSChecked()\n\nBLOCO = CLASSE TabClien_FILTROS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //GRAVA O XML DOS FILTROS
        public void GravarFILTROS(CheckBox cheFiltrosPES, CheckBox cheOrdemAlfDown, RadioButton rabNumerico, RadioButton rabAlfabetico, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, ComboBox comPesSituacao, ComboBox comPesCategoria, ComboBox comPesQtAoCredito, CheckBox cheFiltrosIMP, ComboBox comImpSituacao, TextBox txtImpEmpreCod, TextBox txtImpEmpreDesc, ComboBox comImpCategoria, CheckBox cheImpApelid, RadioButton rabRPV, RadioButton rabWORD, RadioButton rabEXCEL, RadioButton rabTXT, RadioButton rabOrdemNumerica, RadioButton rabOrdemAlfabetica, RadioButton rabOrdemAlfabeticaApelid, TextBox txtCaminhoRel, CheckBox cheVoltarLix)
        {
            #region CRIA O DIRETÓRIO SE NÃO EXISTIR
            if (!Directory.Exists("..\\Log"))
            {
                Directory.CreateDirectory("..\\Log");
            }
            #endregion

            try
            {
                string Patch = @"..\Log\TabClien.xml";
                XmlTextWriter GerarXML = new XmlTextWriter(Patch, null);
                GerarXML.WriteStartDocument();
                GerarXML.WriteStartElement("Dados_TabClien");


                //PESQUISA
                GerarXML.WriteElementString("cheFiltrosPES", WenProtect(cheFiltrosPES.Checked.ToString()));
                GerarXML.WriteElementString("rabNumerico", WenProtect(rabNumerico.Checked.ToString()));
                GerarXML.WriteElementString("rabAlfabetico", WenProtect(rabAlfabetico.Checked.ToString()));
                GerarXML.WriteElementString("rabTodos", WenProtect(rabTodos.Checked.ToString()));
                GerarXML.WriteElementString("rabTOP", WenProtect(rabTOP.Checked.ToString()));
                GerarXML.WriteElementString("nupQtResultados", WenProtect(nupQtResultados.Value.ToString()));
                GerarXML.WriteElementString("comPesSituacao", WenProtect(comPesSituacao.SelectedIndex.ToString()));
                GerarXML.WriteElementString("comPesCategoria", WenProtect(comPesCategoria.SelectedIndex.ToString()));
                GerarXML.WriteElementString("comPesQtAoCredito", WenProtect(comPesQtAoCredito.SelectedIndex.ToString()));
                GerarXML.WriteElementString("cheOrdemAlfDown", WenProtect(cheOrdemAlfDown.Checked.ToString()));


                //IMPRESSÃO
                GerarXML.WriteElementString("cheFiltrosIMP", WenProtect(cheFiltrosIMP.Checked.ToString()));
                GerarXML.WriteElementString("rabOrdemNumerica", WenProtect(rabOrdemNumerica.Checked.ToString()));
                GerarXML.WriteElementString("rabOrdemAlfabetica", WenProtect(rabOrdemAlfabetica.Checked.ToString()));
                GerarXML.WriteElementString("rabOrdemAlfabeticaApelid", WenProtect(rabOrdemAlfabeticaApelid.Checked.ToString()));
                GerarXML.WriteElementString("rabRPV", WenProtect(rabRPV.Checked.ToString()));
                GerarXML.WriteElementString("rabWORD", WenProtect(rabWORD.Checked.ToString()));
                GerarXML.WriteElementString("rabEXCEL", WenProtect(rabEXCEL.Checked.ToString()));
                GerarXML.WriteElementString("rabTXT", WenProtect(rabTXT.Checked.ToString()));
                GerarXML.WriteElementString("comImpSituacao", WenProtect(comImpSituacao.SelectedIndex.ToString()));
                GerarXML.WriteElementString("comImpCategoria", WenProtect(comImpCategoria.SelectedIndex.ToString()));
                GerarXML.WriteElementString("txtCaminhoRel", WenProtect(txtCaminhoRel.Text));
                GerarXML.WriteElementString("cheImpApelid", WenProtect(cheImpApelid.Checked.ToString()));
                GerarXML.WriteElementString("txtImpEmpreCod", WenProtect(txtImpEmpreCod.Text));
                GerarXML.WriteElementString("txtImpEmpreDesc", WenProtect(txtImpEmpreDesc.Text));

                //LIXEIRA
                GerarXML.WriteElementString("cheVoltarLix", WenProtect(cheVoltarLix.Checked.ToString()));

                GerarXML.WriteEndElement();
                GerarXML.Close();
            }
            catch (XmlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarFILTROS()\n\nBLOCO = CLASSE TabClien_FILTROS\n\n" + Ex.Message, "TechSIS XML Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarFILTROS()\n\nBLOCO = CLASSE TabClien_FILTROS\n\n" + Ex.Message + "\n\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //CARREGA OS FILTROS
        public void CarregaFILTROS(CheckBox cheFiltrosPES, CheckBox cheOrdemAlfDown, RadioButton rabNumerico, RadioButton rabAlfabetico, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, ComboBox comPesSituacao, ComboBox comPesCategoria, ComboBox comPesQtAoCredito, CheckBox cheFiltrosIMP, ComboBox comImpSituacao, TextBox txtImpEmpreCod, TextBox txtImpEmpreDesc, ComboBox comImpCategoria, CheckBox cheImpApelid, RadioButton rabRPV, RadioButton rabWORD, RadioButton rabEXCEL, RadioButton rabTXT, RadioButton rabOrdemNumerica, RadioButton rabOrdemAlfabetica, RadioButton rabOrdemAlfabeticaApelid, TextBox txtCaminhoRel, CheckBox cheVoltarLix)
        {
            string Patch = @"..\Log\TabClien.xml";
            try
            {
                if (File.Exists(Patch))
                {
                    XmlDocument LerXML = new XmlDocument();
                    LerXML.Load(Patch);
                    //PESQUISA
                    XmlNode cheFiltrosPES_xml = LerXML.DocumentElement.SelectSingleNode("cheFiltrosPES");
                    XmlNode rabNumerico_xml = LerXML.DocumentElement.SelectSingleNode("rabNumerico");
                    XmlNode rabAlfabetico_xml = LerXML.DocumentElement.SelectSingleNode("rabAlfabetico");
                    XmlNode rabTodos_xml = LerXML.DocumentElement.SelectSingleNode("rabTodos");
                    XmlNode rabTOP_xml = LerXML.DocumentElement.SelectSingleNode("rabTOP");
                    XmlNode nupQtResultados_xml = LerXML.DocumentElement.SelectSingleNode("nupQtResultados");
                    XmlNode comPesSituacao_xml = LerXML.DocumentElement.SelectSingleNode("comPesSituacao");
                    XmlNode comPesCategoria_xml = LerXML.DocumentElement.SelectSingleNode("comPesCategoria");
                    XmlNode comPesQtAoCredito_xml = LerXML.DocumentElement.SelectSingleNode("comPesQtAoCredito");
                    XmlNode cheOrdemAlfDown_xml = LerXML.DocumentElement.SelectSingleNode("cheOrdemAlfDown");

                    //IMPRESSÃO
                    XmlNode cheFiltrosIMP_xml = LerXML.DocumentElement.SelectSingleNode("cheFiltrosIMP");
                    XmlNode rabRPV_xml = LerXML.DocumentElement.SelectSingleNode("rabRPV");
                    XmlNode rabWORD_xml = LerXML.DocumentElement.SelectSingleNode("rabWORD");
                    XmlNode rabEXCEL_xml = LerXML.DocumentElement.SelectSingleNode("rabEXCEL");
                    XmlNode rabTXT_xml = LerXML.DocumentElement.SelectSingleNode("rabTXT");
                    XmlNode rabOrdemNumerica_xml = LerXML.DocumentElement.SelectSingleNode("rabOrdemNumerica");
                    XmlNode rabOrdemAlfabetica_xml = LerXML.DocumentElement.SelectSingleNode("rabOrdemAlfabetica");
                    XmlNode rabOrdemAlfabeticaApelid_xml = LerXML.DocumentElement.SelectSingleNode("rabOrdemAlfabeticaApelid");
                    XmlNode comImpSituacao_xml = LerXML.DocumentElement.SelectSingleNode("comImpSituacao");
                    XmlNode comImpCategoria_xml = LerXML.DocumentElement.SelectSingleNode("comImpCategoria");
                    XmlNode txtCaminhoRel_xml = LerXML.DocumentElement.SelectSingleNode("txtCaminhoRel");
                    XmlNode cheImpApelid_xml = LerXML.DocumentElement.SelectSingleNode("cheImpApelid");
                    XmlNode txtImpEmpreCod_xml = LerXML.DocumentElement.SelectSingleNode("txtImpEmpreCod");
                    XmlNode txtImpEmpreDesc_xml = LerXML.DocumentElement.SelectSingleNode("txtImpEmpreDesc");


                    //LIXEIRA
                    XmlNode cheVoltarLix_xml = LerXML.DocumentElement.SelectSingleNode("cheVoltarLix");


                    #region STRING PESQUISA
                    string _cheFiltrosPES = WenDisprotect(cheFiltrosPES_xml.InnerText.ToString());
                    string _rabNumerico = WenDisprotect(rabNumerico_xml.InnerText.ToString());
                    string _rabAlfabetico = WenDisprotect(rabAlfabetico_xml.InnerText.ToString());
                    string _rabTodos = WenDisprotect(rabTodos_xml.InnerText.ToString());
                    string _rabTOP = WenDisprotect(rabTOP_xml.InnerText.ToString());
                    string _nupQtResultados = WenDisprotect(nupQtResultados_xml.InnerText.ToString());
                    string _comPesSituacao = WenDisprotect(comPesSituacao_xml.InnerText.ToString());
                    string _comPesCategoria = WenDisprotect(comPesCategoria_xml.InnerText.ToString());
                    string _comPesQtAoCredito = WenDisprotect(comPesQtAoCredito_xml.InnerText.ToString());
                    string _cheOrdemAlfDown = WenDisprotect(cheOrdemAlfDown_xml.InnerText.ToString());
                    #endregion

                    #region STRING IMPRESSÃO
                    string _cheFiltrosIMP = WenDisprotect(cheFiltrosIMP_xml.InnerText.ToString());
                    string _rabRPV = WenDisprotect(rabRPV_xml.InnerText.ToString());
                    string _rabWORD = WenDisprotect(rabWORD_xml.InnerText.ToString());
                    string _rabEXCEL = WenDisprotect(rabEXCEL_xml.InnerText.ToString());
                    string _rabTXT = WenDisprotect(rabTXT_xml.InnerText.ToString());
                    string _rabOrdemNumerica = WenDisprotect(rabOrdemNumerica_xml.InnerText.ToString());
                    string _rabOrdemAlfabetica = WenDisprotect(rabOrdemAlfabetica_xml.InnerText.ToString());
                    string _rabOrdemAlfabeticaApelid = WenDisprotect(rabOrdemAlfabeticaApelid_xml.InnerText.ToString());
                    string _comImpSituacao = WenDisprotect(comImpSituacao_xml.InnerText.ToString());
                    string _comImpCategoria = WenDisprotect(comImpCategoria_xml.InnerText.ToString());
                    string _txtCaminhoRel = WenDisprotect(txtCaminhoRel_xml.InnerText.ToString());
                    string _cheImpApelid = WenDisprotect(cheImpApelid_xml.InnerText.ToString());
                    string _txtImpEmpreCod = WenDisprotect(txtImpEmpreCod_xml.InnerText.ToString());
                    string _txtImpEmpreDesc = WenDisprotect(txtImpEmpreDesc_xml.InnerText.ToString());
                    #endregion

                    #region STRING LIXEIRA
                    string _cheVoltarLix = WenDisprotect(cheVoltarLix_xml.InnerText.ToString());
                    #endregion




                    #region TRATAMENTO PESQUISA
                    if (_cheFiltrosPES == "True")
                    {
                        rabNumerico.Checked = Convert.ToBoolean(_rabNumerico);
                        rabAlfabetico.Checked = Convert.ToBoolean(_rabAlfabetico);
                        rabTodos.Checked = Convert.ToBoolean(_rabTodos);
                        rabTOP.Checked = Convert.ToBoolean(_rabTOP);
                        if (_rabTOP == "True")
                        {
                            nupQtResultados.Value = Convert.ToInt32(_nupQtResultados);
                        }

                        comPesSituacao.SelectedIndex = Convert.ToInt32(_comPesSituacao);
                        comPesCategoria.SelectedIndex = Convert.ToInt32(_comPesCategoria);
                        comPesQtAoCredito.SelectedIndex = Convert.ToInt32(_comPesQtAoCredito);

                        cheOrdemAlfDown.Checked = Convert.ToBoolean(_cheOrdemAlfDown);
                    }
                    else
                    {
                        rabNumerico.Checked = false;
                        rabAlfabetico.Checked = false;
                        rabTodos.Checked = false;
                        rabTOP.Checked = false;
                        nupQtResultados.Value = 0;
                        comPesSituacao.SelectedIndex = -1;
                        comPesCategoria.SelectedIndex = -1;
                        comPesQtAoCredito.SelectedIndex = -1;
                        cheOrdemAlfDown.Checked = false;
                    }
                    #endregion

                    #region TRATAMENTO IMPRESSÃO
                    if (_cheFiltrosIMP == "True")
                    {
                        rabRPV.Checked = Convert.ToBoolean(_rabRPV);
                        rabWORD.Checked = Convert.ToBoolean(_rabWORD);
                        rabEXCEL.Checked = Convert.ToBoolean(_rabEXCEL);
                        rabTXT.Checked = Convert.ToBoolean(_rabTXT);
                        rabOrdemNumerica.Checked = Convert.ToBoolean(_rabOrdemNumerica);
                        rabOrdemAlfabetica.Checked = Convert.ToBoolean(_rabOrdemAlfabetica);
                        rabOrdemAlfabeticaApelid.Checked = Convert.ToBoolean(_rabOrdemAlfabeticaApelid);
                        comImpSituacao.SelectedIndex = Convert.ToInt32(_comImpSituacao);
                        comImpCategoria.SelectedIndex = Convert.ToInt32(_comImpCategoria);
                        txtCaminhoRel.Text = _txtCaminhoRel;
                        txtImpEmpreCod.Text = _txtImpEmpreCod;
                        txtImpEmpreDesc.Text = _txtImpEmpreDesc;
                        cheImpApelid.Checked = Convert.ToBoolean(_cheImpApelid);
                    }
                    else
                    {
                        rabRPV.Checked = false;
                        rabWORD.Checked = false;
                        rabEXCEL.Checked = false;
                        rabTXT.Checked = false;
                        rabOrdemNumerica.Checked = false;
                        rabOrdemAlfabetica.Checked = false;
                        rabOrdemAlfabeticaApelid.Checked = false;
                        txtCaminhoRel.Text = string.Empty;
                        txtImpEmpreCod.Text = string.Empty;
                        txtImpEmpreDesc.Text = string.Empty;
                        comImpSituacao.SelectedIndex = -1;
                        comImpCategoria.SelectedIndex = -1;
                        cheImpApelid.Checked = false;
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
