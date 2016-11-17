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

namespace TabEmpre
{
    public class TabEmpre_FILTROs
    {
        #region CRIPTOGRAFIA
        const string senha = "“3.!156350WeNeMy”";
        public static string Criptografar(string Message)
        {
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
        public static string Descriptografar(string Message)
        {
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

        //Grava o xml dos filtros informados
        public void GravarFILTROS(RadioButton rabNumerico, RadioButton rabAlfabetico, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, ComboBox comSituaçãoPES, RadioButton rabRPV, RadioButton rabWORD, RadioButton rabEXCEL, RadioButton rabTXT, ComboBox comSituacaoIMP, RadioButton rabOrdemNumerica, RadioButton rabOrdemAlfabetica, CheckBox cheFILTROSImp, CheckBox cheFILTROSPes, ComboBox comModuloIMP, ComboBox comTipoDeVendaIMP, RadioButton rabOrdemAlfabeticaFan, TextBox txtCaminhoRel, ComboBox comAtividadePES, ComboBox comModuloPES)
        {
            try
            {
                string Patch = @"..\Log\TabEmpre.xml";
                XmlTextWriter GerarXML = new XmlTextWriter(Patch, null);
                GerarXML.WriteStartDocument();
                GerarXML.WriteStartElement("Dados_TabEmpre");
                //PESQUISA
                GerarXML.WriteElementString("cheFILTROSPes", Criptografar(cheFILTROSPes.Checked.ToString()));

                GerarXML.WriteElementString("rabNumerico", Criptografar(rabNumerico.Checked.ToString()));
                GerarXML.WriteElementString("rabAlfabetico", Criptografar(rabAlfabetico.Checked.ToString()));
                GerarXML.WriteElementString("rabTodos", Criptografar(rabTodos.Checked.ToString()));
                GerarXML.WriteElementString("rabTOP", Criptografar(rabTOP.Checked.ToString()));
                GerarXML.WriteElementString("nupQtResultados", Criptografar(nupQtResultados.Value.ToString()));
                GerarXML.WriteElementString("comSituaçãoPES", Criptografar(comSituaçãoPES.SelectedIndex.ToString()));
                GerarXML.WriteElementString("comAtividadePES", Criptografar(comAtividadePES.SelectedIndex.ToString()));
                GerarXML.WriteElementString("comModuloPES", Criptografar(comModuloPES.SelectedIndex.ToString()));

                //IMPRESSÃO
                GerarXML.WriteElementString("cheFILTROSImp", Criptografar(cheFILTROSImp.Checked.ToString()));

                GerarXML.WriteElementString("rabRPV", Criptografar(rabRPV.Checked.ToString()));
                GerarXML.WriteElementString("rabWORD", Criptografar(rabWORD.Checked.ToString()));
                GerarXML.WriteElementString("rabEXCEL", Criptografar(rabEXCEL.Checked.ToString()));
                GerarXML.WriteElementString("rabTXT", Criptografar(rabTXT.Checked.ToString()));
                GerarXML.WriteElementString("comSituacaoIMP", Criptografar(comSituacaoIMP.SelectedIndex.ToString()));
                GerarXML.WriteElementString("comModuloIMP", Criptografar(comModuloIMP.SelectedIndex.ToString()));
                GerarXML.WriteElementString("comTipoDeVendaIMP", Criptografar(comTipoDeVendaIMP.SelectedIndex.ToString()));
                GerarXML.WriteElementString("rabOrdemNumerica", Criptografar(rabOrdemNumerica.Checked.ToString()));
                GerarXML.WriteElementString("rabOrdemAlfabetica", Criptografar(rabOrdemAlfabetica.Checked.ToString()));
                GerarXML.WriteElementString("rabOrdemAlfabeticaFan", Criptografar(rabOrdemAlfabeticaFan.Checked.ToString()));
                GerarXML.WriteElementString("txtCaminhoRel", Criptografar(txtCaminhoRel.Text));

                GerarXML.WriteEndElement();
                GerarXML.Close();
            }
            catch (Exception Ex)
            {
                DialogResult Erro = MessageBox.Show("ERRO Exception. Erro ao tentar gravar o XML (Exception Erro)\nAperte em CANCELAR para ver detalhes do erro.", "Verifique erro ocorrido!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (Erro == DialogResult.Cancel)
                {
                    MessageBox.Show("DETALHAMENTO DO ERRO Exception\n\n" + Ex.Message, "TechSIS Auto-Manutenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Carrega o XML dos filtros
        public void CarregaFILTROS(RadioButton rabNumerico, RadioButton rabAlfabetico, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, ComboBox comSituaçãoPES, RadioButton rabRPV, RadioButton rabWORD, RadioButton rabEXCEL, RadioButton rabTXT, ComboBox comSituacaoIMP, RadioButton rabOrdemNumerica, RadioButton rabOrdemAlfabetica, CheckBox cheFILTROSImp, CheckBox cheFILTROSPes, ComboBox comModuloIMP, ComboBox comTipoDeVendaIMP, RadioButton rabOrdemAlfabeticaFan, TextBox txtCaminhoRel, ComboBox comAtividadePES, ComboBox comModuloPES)
        {
            string Patch = @"..\Log\TabEmpre.xml";
            try
            {
                if (File.Exists(Patch))
                {
                    XmlDocument LerXML = new XmlDocument();
                    LerXML.Load(Patch);
                    //PESQUISA
                    XmlNode cheFILTROSPes_xml = LerXML.DocumentElement.SelectSingleNode("cheFILTROSPes");
                    XmlNode rabNumerico_xml = LerXML.DocumentElement.SelectSingleNode("rabNumerico");
                    XmlNode rabAlfabetico_xml = LerXML.DocumentElement.SelectSingleNode("rabAlfabetico");
                    XmlNode rabTodos_xml = LerXML.DocumentElement.SelectSingleNode("rabTodos");
                    XmlNode rabTOP_xml = LerXML.DocumentElement.SelectSingleNode("rabTOP");
                    XmlNode nupQtResultados_xml = LerXML.DocumentElement.SelectSingleNode("nupQtResultados");
                    XmlNode comSituaçãoPES_xml = LerXML.DocumentElement.SelectSingleNode("comSituaçãoPES");
                    XmlNode comAtividadePES_xml = LerXML.DocumentElement.SelectSingleNode("comAtividadePES");
                    XmlNode comModuloPES_xml = LerXML.DocumentElement.SelectSingleNode("comModuloPES");
                    //IMPRESSÃO
                    XmlNode cheFILTROSImp_xml = LerXML.DocumentElement.SelectSingleNode("cheFILTROSImp");
                    XmlNode rabRPV_xml = LerXML.DocumentElement.SelectSingleNode("rabRPV");
                    XmlNode rabWORD_xml = LerXML.DocumentElement.SelectSingleNode("rabWORD");
                    XmlNode rabEXCEL_xml = LerXML.DocumentElement.SelectSingleNode("rabEXCEL");
                    XmlNode rabTXT_xml = LerXML.DocumentElement.SelectSingleNode("rabTXT");
                    XmlNode rabOrdemNumerica_xml = LerXML.DocumentElement.SelectSingleNode("rabOrdemNumerica");
                    XmlNode rabOrdemAlfabetica_xml = LerXML.DocumentElement.SelectSingleNode("rabOrdemAlfabetica");
                    XmlNode txtCaminhoRel_xml = LerXML.DocumentElement.SelectSingleNode("txtCaminhoRel");
                    XmlNode comSituacaoIMP_xml = LerXML.DocumentElement.SelectSingleNode("comSituacaoIMP");
                    XmlNode comModuloIMP_xml = LerXML.DocumentElement.SelectSingleNode("comModuloIMP");
                    XmlNode comTipoDeVendaIMP_xml = LerXML.DocumentElement.SelectSingleNode("comTipoDeVendaIMP");

                    #region STRING PESQUISA
                    string _cheFILTROSPes = Descriptografar(cheFILTROSPes_xml.InnerText.ToString());
                    string _rabNumerico = Descriptografar(rabNumerico_xml.InnerText.ToString());
                    string _rabAlfabetico = Descriptografar(rabAlfabetico_xml.InnerText.ToString());
                    string _rabTodos = Descriptografar(rabTodos_xml.InnerText.ToString());
                    string _rabTOP = Descriptografar(rabTOP_xml.InnerText.ToString());
                    string _nupQtResultados = Descriptografar(nupQtResultados_xml.InnerText.ToString());
                    string _comSituaçãoPES = Descriptografar(comSituaçãoPES_xml.InnerText.ToString());
                    string _comAtividadePES = Descriptografar(comAtividadePES_xml.InnerText.ToString());
                    string _comModuloPES = Descriptografar(comModuloPES_xml.InnerText.ToString());
                    #endregion

                    #region STRING IMPRESSÃO
                    string _cheFILTROSImp = Descriptografar(cheFILTROSImp_xml.InnerText.ToString());
                    string _rabRPV = Descriptografar(rabRPV_xml.InnerText.ToString());
                    string _rabWORD = Descriptografar(rabWORD_xml.InnerText.ToString());
                    string _rabEXCEL = Descriptografar(rabEXCEL_xml.InnerText.ToString());
                    string _rabTXT = Descriptografar(rabTXT_xml.InnerText.ToString());
                    string _comSituacaoIMP = Descriptografar(comSituacaoIMP_xml.InnerText.ToString());
                    string _comModuloIMP = Descriptografar(comModuloIMP_xml.InnerText.ToString());
                    string _comTipoDeVendaIMP = Descriptografar(comTipoDeVendaIMP_xml.InnerText.ToString());
                    string _rabOrdemNumerica = Descriptografar(rabOrdemNumerica_xml.InnerText.ToString());
                    string _rabOrdemAlfabetica = Descriptografar(rabOrdemAlfabetica_xml.InnerText.ToString());
                    string _txtCaminhoRel = Descriptografar(txtCaminhoRel_xml.InnerText.ToString());
                    #endregion



                    #region TRATAMENTO PESQUISA
                    if (_cheFILTROSPes == "True")
                    {
                        rabNumerico.Checked = Convert.ToBoolean(_rabNumerico);
                        rabAlfabetico.Checked = Convert.ToBoolean(_rabAlfabetico);
                        rabTodos.Checked = Convert.ToBoolean(_rabTodos);
                        rabTOP.Checked = Convert.ToBoolean(_rabTOP);
                        if (_rabTOP == "True")
                        {
                            nupQtResultados.Value = Convert.ToInt32(_nupQtResultados);
                        }
                        comSituaçãoPES.SelectedIndex = Convert.ToInt32(_comSituaçãoPES);
                        comAtividadePES.SelectedIndex = Convert.ToInt32(_comAtividadePES);
                        comModuloPES.SelectedIndex = Convert.ToInt32(_comModuloPES);
                    }
                    else
                    {
                        rabNumerico.Checked = false;
                        rabAlfabetico.Checked = false;
                        rabTodos.Checked = false;
                        rabTOP.Checked = false;
                        nupQtResultados.Value = 0;
                        comSituaçãoPES.SelectedIndex = -1;
                        comModuloPES.SelectedIndex = -1;
                        comAtividadePES.SelectedIndex = -1;
                    }
                    #endregion

                    #region TRATAMENTO IMPRESSÃO
                    if (_cheFILTROSImp == "True")
                    {
                        rabRPV.Checked = Convert.ToBoolean(_rabRPV);
                        rabWORD.Checked = Convert.ToBoolean(_rabWORD);
                        rabEXCEL.Checked = Convert.ToBoolean(_rabEXCEL);
                        rabTXT.Checked = Convert.ToBoolean(_rabTXT);
                        comSituacaoIMP.SelectedIndex = Convert.ToInt32(_comSituacaoIMP);
                        comModuloIMP.SelectedIndex = Convert.ToInt32(_comModuloIMP);
                        comTipoDeVendaIMP.SelectedIndex = Convert.ToInt32(_comTipoDeVendaIMP);
                        rabOrdemNumerica.Checked = Convert.ToBoolean(_rabOrdemNumerica);
                        rabOrdemAlfabetica.Checked = Convert.ToBoolean(_rabOrdemAlfabetica);
                        txtCaminhoRel.Text = _txtCaminhoRel;
                    }
                    else
                    {
                        rabRPV.Checked = false;
                        rabWORD.Checked = false;
                        rabEXCEL.Checked = false;
                        rabTXT.Checked = false;
                        comSituacaoIMP.SelectedIndex = -1;
                        comModuloIMP.SelectedIndex = -1;
                        comTipoDeVendaIMP.SelectedIndex = -1;
                        rabOrdemNumerica.Checked = false;
                        rabOrdemAlfabetica.Checked = false;
                        txtCaminhoRel.Text = string.Empty;
                    }
                    #endregion
                }
            }
            catch (FormatException Ex)
            {
                MessageBox.Show("Arquivo XML dos filtros foi editado ou está corrompido.\n\nPara evitar esta mensagem novamente, tente não modificar o conteúdo da pasta Log. Todo arquivo da pasta é Criptografado pela segurança do Software, qualquer tentativa de edição ou alteração será barrada pelo Software.\n\nCaso esta mensagem apareça constantemente contate suporte técnico.\nFiltros serão zerado para serem reconfigurados pelo usuário.", "TechSIS BWK Erro.: Criptografia Violada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(Ex.Message, Ex.GetBaseException().ToString());
            }
            catch (XmlException Ex)
            {
                MessageBox.Show("Arquivo XML dos filtros foi editado ou está corrompido.\n\nPara evitar esta mensagem novamente, tente não modificar o conteúdo da pasta Log. Todo arquivo da pasta é Criptografado pela segurança do Software, qualquer tentativa de edição ou alteração será barrada pelo Software.\n\nCaso esta mensagem apareça constantemente contate suporte técnico.\nFiltros serão zerado para serem reconfigurados pelo usuário.", "TechSIS BWK Erro.: Criptografia Violada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(Ex.Message);
            }
            catch (Exception Ex)
            {
                DialogResult Erro = MessageBox.Show("ERRO Exception. Erro ao tentar carregar o XML (Exception Erro)\nAperte em CANCELAR para ver detalhes do erro.\n" + Ex.GetType().ToString(), "Verifique erro ocorrido!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (Erro == DialogResult.Cancel)
                {
                    MessageBox.Show("DETALHAMENTO DO ERRO Exception\n\n" + Ex.Message, "TechSIS Auto-Manutenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

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
                    if (Dr[0].ToString() == "True")
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
                DialogResult Erro = MessageBox.Show("ERRO SQL. Erro ao tentar capturar cfg geral (SQLErro.: " + Ex.Number + ")\nAperte em CANCELAR para ver detalhes do erro.", "Verifique erro ocorrido.: cheFILTROSChecked()", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (Erro == DialogResult.Cancel)
                {
                    MessageBox.Show("DETALHAMENTO DO ERRO SQL CÓDIGO.: " + Ex.Number + "\n\n" + Ex.Message, "TechSIS Auto-Manutenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Ex)
            {
                DialogResult Erro = MessageBox.Show("ERRO Exception. Erro ao tentar capturar cfg geral (Exception Erro)\nAperte em CANCELAR para ver detalhes do erro.", "Verifique erro ocorrido.: cheFILTROSChecked()", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (Erro == DialogResult.Cancel)
                {
                    MessageBox.Show("DETALHAMENTO DO ERRO Exception\n\n" + Ex.Message, "TechSIS Auto-Manutenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
