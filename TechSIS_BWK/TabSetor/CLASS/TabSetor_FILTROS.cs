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

namespace TabSetor
{
    internal class TabSetor_FILTROS
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

        //DEFINE O PACH DO XML
        public string PatchXML = "..\\Log\\TabSetor.xml";
        public string IdentXML = "TabSetor";

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
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método cheFILTROSChecked()\n\nBLOCO = CLASSE TabUsuar_FILTROS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método cheFILTROSChecked()\n\nBLOCO = CLASSE TabUsuar_FILTROS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string NomeDoNó = "Tabelas_Gerais";
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

        //GRAVA O XML COM OS FILTROS
        public void GravarFILTROS(Panel panPrinAb2, Panel panPrinAb3, Panel panPrinAb4)
        {
            #region CRIA O DIRETÓRIO SE NÃO EXISTIR
            if (!Directory.Exists("..\\Log"))
            {
                Directory.CreateDirectory("..\\Log");
            }
            #endregion


            try
            {
                XmlTextWriter GerarXML = new XmlTextWriter(PatchXML, Encoding.Default);
                GerarXML.WriteStartDocument();
                GerarXML.WriteStartElement("Dados_" + IdentXML);

                #region TRATAMENTO PARA PESQUISA
                foreach (Control PainelPrincipal in panPrinAb2.Controls)
                {
                    foreach (Control PainelSecundario in PainelPrincipal.Controls)
                    {
                        foreach (Control CONTROLES in PainelSecundario.Controls)
                        {
                            #region TRATAMENTO DO QUE CAPTURAR
                            if (CONTROLES.GetType() == typeof(RadioButton) && CONTROLES.AccessibleName == "5")
                            {
                                string Checked = (CONTROLES as RadioButton).Checked.ToString();
                                GerarXML.WriteElementString(CONTROLES.Name, WenProtects(Checked));
                            }
                            if (CONTROLES.GetType() == typeof(NumericUpDown) && CONTROLES.AccessibleName == "5")
                            {
                                string Value = (CONTROLES as NumericUpDown).Value.ToString();
                                GerarXML.WriteElementString(CONTROLES.Name, WenProtects(Value));
                            }
                            if (CONTROLES.GetType() == typeof(ComboBox) && CONTROLES.AccessibleName == "5")
                            {
                                string SelectedIndex = (CONTROLES as ComboBox).SelectedIndex.ToString();
                                GerarXML.WriteElementString(CONTROLES.Name, WenProtects(SelectedIndex));
                            }
                            if (CONTROLES.GetType() == typeof(CheckBox) && CONTROLES.AccessibleName == "5")
                            {
                                string Checked = (CONTROLES as CheckBox).Checked.ToString();
                                GerarXML.WriteElementString(CONTROLES.Name, WenProtects(Checked));
                            }
                            if (CONTROLES.GetType() == typeof(TextBox) && CONTROLES.AccessibleName == "5")
                            {
                                string Text = (CONTROLES as TextBox).Text.ToString();
                                GerarXML.WriteElementString(CONTROLES.Name, WenProtects(Text));
                            }
                            #endregion
                        }
                    }
                }
                #endregion
                #region TRATAMENTO PARA IMPRESSÃO
                foreach (Control PainelPrincipal in panPrinAb3.Controls)
                {
                    foreach (Control PainelSecundario in PainelPrincipal.Controls)
                    {

                        #region TRATAMENTO DO QUE CAPTURAR
                        if (PainelSecundario.GetType() == typeof(RadioButton) && PainelSecundario.AccessibleName == "5")
                        {
                            string Checked = (PainelSecundario as RadioButton).Checked.ToString();
                            GerarXML.WriteElementString(PainelSecundario.Name, WenProtects(Checked));
                        }
                        if (PainelSecundario.GetType() == typeof(NumericUpDown) && PainelSecundario.AccessibleName == "5")
                        {
                            string Value = (PainelSecundario as NumericUpDown).Value.ToString();
                            GerarXML.WriteElementString(PainelSecundario.Name, WenProtects(Value));
                        }
                        if (PainelSecundario.GetType() == typeof(ComboBox) && PainelSecundario.AccessibleName == "5")
                        {
                            string SelectedIndex = (PainelSecundario as ComboBox).SelectedIndex.ToString();
                            GerarXML.WriteElementString(PainelSecundario.Name, WenProtects(SelectedIndex));
                        }
                        if (PainelSecundario.GetType() == typeof(CheckBox) && PainelSecundario.AccessibleName == "5")
                        {
                            string Checked = (PainelSecundario as CheckBox).Checked.ToString();
                            GerarXML.WriteElementString(PainelSecundario.Name, WenProtects(Checked));
                        }
                        if (PainelSecundario.GetType() == typeof(TextBox) && PainelSecundario.AccessibleName == "5")
                        {
                            string Text = (PainelSecundario as TextBox).Text.ToString();
                            GerarXML.WriteElementString(PainelSecundario.Name, WenProtects(Text));
                        }
                        #endregion


                        foreach (Control CONTROLES in PainelSecundario.Controls)
                        {
                            #region TRATAMENTO DO QUE CAPTURAR
                            if (CONTROLES.GetType() == typeof(RadioButton) && CONTROLES.AccessibleName == "5")
                            {
                                string Checked = (CONTROLES as RadioButton).Checked.ToString();
                                GerarXML.WriteElementString(CONTROLES.Name, WenProtects(Checked));
                            }
                            if (CONTROLES.GetType() == typeof(NumericUpDown) && CONTROLES.AccessibleName == "5")
                            {
                                string Value = (CONTROLES as NumericUpDown).Value.ToString();
                                GerarXML.WriteElementString(CONTROLES.Name, WenProtects(Value));
                            }
                            if (CONTROLES.GetType() == typeof(ComboBox) && CONTROLES.AccessibleName == "5")
                            {
                                string SelectedIndex = (CONTROLES as ComboBox).SelectedIndex.ToString();
                                GerarXML.WriteElementString(CONTROLES.Name, WenProtects(SelectedIndex));
                            }
                            if (CONTROLES.GetType() == typeof(CheckBox) && CONTROLES.AccessibleName == "5")
                            {
                                string Checked = (CONTROLES as CheckBox).Checked.ToString();
                                GerarXML.WriteElementString(CONTROLES.Name, WenProtects(Checked));
                            }
                            if (CONTROLES.GetType() == typeof(TextBox) && CONTROLES.AccessibleName == "5")
                            {
                                string Text = (CONTROLES as TextBox).Text.ToString();
                                GerarXML.WriteElementString(CONTROLES.Name, WenProtects(Text));
                            }
                            #endregion
                        }
                    }
                }
                #endregion
                #region TRATAMENTO PARA LIXEIRA
                foreach (Control PainelPrincipal in panPrinAb4.Controls)
                {
                    foreach (Control PainelSecundario in PainelPrincipal.Controls)
                    {
                        #region TRATAMENTO DO QUE CAPTURAR
                        if (PainelSecundario.GetType() == typeof(RadioButton) && PainelSecundario.AccessibleName == "5")
                        {
                            string Checked = (PainelSecundario as RadioButton).Checked.ToString();
                            GerarXML.WriteElementString(PainelSecundario.Name, WenProtects(Checked));
                        }
                        if (PainelSecundario.GetType() == typeof(NumericUpDown) && PainelSecundario.AccessibleName == "5")
                        {
                            string Value = (PainelSecundario as NumericUpDown).Value.ToString();
                            GerarXML.WriteElementString(PainelSecundario.Name, WenProtects(Value));
                        }
                        if (PainelSecundario.GetType() == typeof(ComboBox) && PainelSecundario.AccessibleName == "5")
                        {
                            string SelectedIndex = (PainelSecundario as ComboBox).SelectedIndex.ToString();
                            GerarXML.WriteElementString(PainelSecundario.Name, WenProtects(SelectedIndex));
                        }
                        if (PainelSecundario.GetType() == typeof(CheckBox) && PainelSecundario.AccessibleName == "5")
                        {
                            string Checked = (PainelSecundario as CheckBox).Checked.ToString();
                            GerarXML.WriteElementString(PainelSecundario.Name, WenProtects(Checked));
                        }
                        if (PainelSecundario.GetType() == typeof(TextBox) && PainelSecundario.AccessibleName == "5")
                        {
                            string Text = (PainelSecundario as TextBox).Text.ToString();
                            GerarXML.WriteElementString(PainelSecundario.Name, WenProtects(Text));
                        }
                        #endregion

                    }
                }
                #endregion


                GerarXML.WriteEndElement();
                GerarXML.Close();
            }
            catch (XmlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarFILTROS()\n\nBLOCO = CLASSE TabSetor_FILTROS\n\n" + Ex.Message, "TechSIS XML Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarFILTROS()\n\nBLOCO = CLASSE TabSetor_FILTROS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //CARREGA OS FILTROS A PARTIR DO XML
        public void CarregaFILTROS(Panel panPrinAb2, Panel panPrinAb3, Panel panPrinAb4)
        {
            try
            {
                if (File.Exists(PatchXML))
                {
                    XmlDocument LerXML = new XmlDocument();
                    LerXML.Load(PatchXML);

                    XmlNode LEMBRAR_PES = LerXML.DocumentElement.SelectSingleNode("cheFiltrosPES");
                    string STR_LEMBRAR_PES = WenDisprotects(LEMBRAR_PES.InnerText.ToString());
                    if (STR_LEMBRAR_PES == "True")
                    {
                        #region TRATAMENTO PARA PESQUISA
                        foreach (Control PainelPrincipal in panPrinAb2.Controls)
                        {
                            foreach (Control PainelSecundario in PainelPrincipal.Controls)
                            {
                                foreach (Control CONTROLES in PainelSecundario.Controls)
                                {
                                    #region TRATAMENTO DO QUE CAPTURAR
                                    if (CONTROLES.GetType() == typeof(RadioButton) && CONTROLES.AccessibleName == "5")
                                    {
                                        XmlNode XML_RadionButton = LerXML.DocumentElement.SelectSingleNode(CONTROLES.Name);
                                        string STR_RadionButton = WenDisprotects(XML_RadionButton.InnerText.ToString());
                                        (CONTROLES as RadioButton).Checked = Convert.ToBoolean(STR_RadionButton);
                                    }
                                    if (CONTROLES.GetType() == typeof(NumericUpDown) && CONTROLES.AccessibleName == "5")
                                    {
                                        XmlNode XML_NumerciUpDown = LerXML.DocumentElement.SelectSingleNode(CONTROLES.Name);
                                        string STR_NumerciUpDown = WenDisprotects(XML_NumerciUpDown.InnerText.ToString());
                                        (CONTROLES as NumericUpDown).Value = Convert.ToDecimal(STR_NumerciUpDown);
                                    }
                                    if (CONTROLES.GetType() == typeof(ComboBox) && CONTROLES.AccessibleName == "5")
                                    {
                                        XmlNode XML_ComboBox = LerXML.DocumentElement.SelectSingleNode(CONTROLES.Name);
                                        string STR_ComboBox = WenDisprotects(XML_ComboBox.InnerText.ToString());
                                        (CONTROLES as ComboBox).SelectedIndex = Convert.ToInt32(STR_ComboBox);
                                    }
                                    if (CONTROLES.GetType() == typeof(TextBox) && CONTROLES.AccessibleName == "5")
                                    {
                                        XmlNode XML_TextBox = LerXML.DocumentElement.SelectSingleNode(CONTROLES.Name);
                                        string STR_TextBox = WenDisprotects(XML_TextBox.InnerText.ToString());
                                        (CONTROLES as TextBox).Text = Convert.ToString(STR_TextBox);
                                    }
                                    if (CONTROLES.GetType() == typeof(CheckBox) && CONTROLES.AccessibleName == "5" && CONTROLES.Name != "cheFiltrosPES")
                                    {
                                        XmlNode XML_CheckBox = LerXML.DocumentElement.SelectSingleNode(CONTROLES.Name);
                                        string STR_CheckBox = WenDisprotects(XML_CheckBox.InnerText.ToString());
                                        (CONTROLES as CheckBox).Checked = Convert.ToBoolean(STR_CheckBox);
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion
                    }

                    XmlNode LEMBRAR_IMP = LerXML.DocumentElement.SelectSingleNode("cheFiltrosIMP");
                    string STR_LEMBRAR_IMP = WenDisprotects(LEMBRAR_IMP.InnerText.ToString());
                    if (STR_LEMBRAR_IMP == "True")
                    {
                        #region TRATAMENTO PARA IMPRESSÃO
                        foreach (Control PainelPrincipal in panPrinAb3.Controls)
                        {
                            foreach (Control PainelSecundario in PainelPrincipal.Controls)
                            {
                                #region TRATAMENTO DO QUE CAPTURAR
                                if (PainelSecundario.GetType() == typeof(RadioButton) && PainelSecundario.AccessibleName == "5")
                                {
                                    XmlNode XML_RadionButton = LerXML.DocumentElement.SelectSingleNode(PainelSecundario.Name);
                                    string STR_RadionButton = WenDisprotects(XML_RadionButton.InnerText.ToString());
                                    (PainelSecundario as RadioButton).Checked = Convert.ToBoolean(STR_RadionButton);
                                }
                                if (PainelSecundario.GetType() == typeof(NumericUpDown) && PainelSecundario.AccessibleName == "5")
                                {
                                    XmlNode XML_NumerciUpDown = LerXML.DocumentElement.SelectSingleNode(PainelSecundario.Name);
                                    string STR_NumerciUpDown = WenDisprotects(XML_NumerciUpDown.InnerText.ToString());
                                    (PainelSecundario as NumericUpDown).Value = Convert.ToDecimal(STR_NumerciUpDown);
                                }
                                if (PainelSecundario.GetType() == typeof(ComboBox) && PainelSecundario.AccessibleName == "5")
                                {
                                    XmlNode XML_ComboBox = LerXML.DocumentElement.SelectSingleNode(PainelSecundario.Name);
                                    string STR_ComboBox = WenDisprotects(XML_ComboBox.InnerText.ToString());
                                    (PainelSecundario as ComboBox).SelectedIndex = Convert.ToInt32(STR_ComboBox);
                                }
                                if (PainelSecundario.GetType() == typeof(TextBox) && PainelSecundario.AccessibleName == "5")
                                {
                                    XmlNode XML_TextBox = LerXML.DocumentElement.SelectSingleNode(PainelSecundario.Name);
                                    string STR_TextBox = WenDisprotects(XML_TextBox.InnerText.ToString());
                                    (PainelSecundario as TextBox).Text = Convert.ToString(STR_TextBox);
                                }
                                if (PainelSecundario.GetType() == typeof(CheckBox) && PainelSecundario.AccessibleName == "5" && PainelSecundario.Name != "cheFiltrosIMP")
                                {
                                    XmlNode XML_CheckBox = LerXML.DocumentElement.SelectSingleNode(PainelSecundario.Name);
                                    string STR_CheckBox = WenDisprotects(XML_CheckBox.InnerText.ToString());
                                    (PainelSecundario as CheckBox).Checked = Convert.ToBoolean(STR_CheckBox);
                                }
                                #endregion

                                foreach (Control CONTROLES in PainelSecundario.Controls)
                                {
                                    #region TRATAMENTO DO QUE CAPTURAR
                                    if (CONTROLES.GetType() == typeof(RadioButton) && CONTROLES.AccessibleName == "5")
                                    {
                                        XmlNode XML_RadionButton = LerXML.DocumentElement.SelectSingleNode(CONTROLES.Name);
                                        string STR_RadionButton = WenDisprotects(XML_RadionButton.InnerText.ToString());
                                        (CONTROLES as RadioButton).Checked = Convert.ToBoolean(STR_RadionButton);
                                    }
                                    if (CONTROLES.GetType() == typeof(NumericUpDown) && CONTROLES.AccessibleName == "5")
                                    {
                                        XmlNode XML_NumerciUpDown = LerXML.DocumentElement.SelectSingleNode(CONTROLES.Name);
                                        string STR_NumerciUpDown = WenDisprotects(XML_NumerciUpDown.InnerText.ToString());
                                        (CONTROLES as NumericUpDown).Value = Convert.ToDecimal(STR_NumerciUpDown);
                                    }
                                    if (CONTROLES.GetType() == typeof(ComboBox) && CONTROLES.AccessibleName == "5")
                                    {
                                        XmlNode XML_ComboBox = LerXML.DocumentElement.SelectSingleNode(CONTROLES.Name);
                                        string STR_ComboBox = WenDisprotects(XML_ComboBox.InnerText.ToString());
                                        (CONTROLES as ComboBox).SelectedIndex = Convert.ToInt32(STR_ComboBox);
                                    }
                                    if (CONTROLES.GetType() == typeof(TextBox) && CONTROLES.AccessibleName == "5")
                                    {
                                        XmlNode XML_TextBox = LerXML.DocumentElement.SelectSingleNode(CONTROLES.Name);
                                        string STR_TextBox = WenDisprotects(XML_TextBox.InnerText.ToString());
                                        (CONTROLES as TextBox).Text = Convert.ToString(STR_TextBox);
                                    }
                                    if (CONTROLES.GetType() == typeof(CheckBox) && CONTROLES.AccessibleName == "5" && CONTROLES.Name != "cheFiltrosIMP")
                                    {
                                        XmlNode XML_CheckBox = LerXML.DocumentElement.SelectSingleNode(CONTROLES.Name);
                                        string STR_CheckBox = WenDisprotects(XML_CheckBox.InnerText.ToString());
                                        (CONTROLES as CheckBox).Checked = Convert.ToBoolean(STR_CheckBox);
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion
                    }

                    foreach (Control PainelPrincipal in panPrinAb4.Controls)
                    {
                        foreach (Control PainelSecundario in PainelPrincipal.Controls)
                        {
                            #region TRATAMENTO PARA LIXEIRA
                            if (PainelSecundario.GetType() == typeof(RadioButton) && PainelSecundario.AccessibleName == "5")
                            {
                                XmlNode XML_RadionButton = LerXML.DocumentElement.SelectSingleNode(PainelSecundario.Name);
                                string STR_RadionButton = WenDisprotects(XML_RadionButton.InnerText.ToString());
                                (PainelSecundario as RadioButton).Checked = Convert.ToBoolean(STR_RadionButton);
                            }
                            if (PainelSecundario.GetType() == typeof(NumericUpDown) && PainelSecundario.AccessibleName == "5")
                            {
                                XmlNode XML_NumerciUpDown = LerXML.DocumentElement.SelectSingleNode(PainelSecundario.Name);
                                string STR_NumerciUpDown = WenDisprotects(XML_NumerciUpDown.InnerText.ToString());
                                (PainelSecundario as NumericUpDown).Value = Convert.ToDecimal(STR_NumerciUpDown);
                            }
                            if (PainelSecundario.GetType() == typeof(ComboBox) && PainelSecundario.AccessibleName == "5")
                            {
                                XmlNode XML_ComboBox = LerXML.DocumentElement.SelectSingleNode(PainelSecundario.Name);
                                string STR_ComboBox = WenDisprotects(XML_ComboBox.InnerText.ToString());
                                (PainelSecundario as ComboBox).SelectedIndex = Convert.ToInt32(STR_ComboBox);
                            }
                            if (PainelSecundario.GetType() == typeof(TextBox) && PainelSecundario.AccessibleName == "5")
                            {
                                XmlNode XML_TextBox = LerXML.DocumentElement.SelectSingleNode(PainelSecundario.Name);
                                string STR_TextBox = WenDisprotects(XML_TextBox.InnerText.ToString());
                                (PainelSecundario as TextBox).Text = Convert.ToString(STR_TextBox);
                            }
                            if (PainelSecundario.GetType() == typeof(CheckBox) && PainelSecundario.AccessibleName == "5" && PainelSecundario.Name != "cheFiltrosIMP")
                            {
                                XmlNode XML_CheckBox = LerXML.DocumentElement.SelectSingleNode(PainelSecundario.Name);
                                string STR_CheckBox = WenDisprotects(XML_CheckBox.InnerText.ToString());
                                (PainelSecundario as CheckBox).Checked = Convert.ToBoolean(STR_CheckBox);
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
