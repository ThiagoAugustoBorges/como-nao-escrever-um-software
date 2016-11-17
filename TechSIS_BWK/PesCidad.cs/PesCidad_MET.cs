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

namespace PesCidad.cs
{
    internal class PesCidad_MET
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

        //VERIFICA SE É PARA GRAVAR OS FILTROS
        public void cheFILTROSChecked(CheckBox cheFILTROSPes, string CodigoLoja)
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
                        cheFILTROSPes.Checked = true;
                    }
                    else
                    {
                        cheFILTROSPes.Checked = false;
                    }
                }
                else
                {
                    cheFILTROSPes.Checked = false;
                }

                Dr.Close();
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método cheFILTROSChecked()\n\nBLOCO = CLASSE Pesquisa\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método cheFILTROSChecked()\n\nBLOCO = CLASSE Pesquisa\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //SELECIONA A COR DE FUNDO DOS FORMULÁRIOS
        public void SelectCorFundo(Control Control_1, Control Control_2, Control Control_3, Control Control_4, Control Control_5, Control Control_6, Control Control_7, string CodigoLoja)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string SelecionaCOR = "SELECT CorFun_CON FROM TabConfi WHERE SeqLoj_CON = " + CodigoLoja;
            SqlCommand Comando = new SqlCommand(SelecionaCOR, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string Cor = Dr[0].ToString();

                    Control_1.BackColor = Color.FromName(Cor);
                    Control_2.BackColor = Color.FromName(Cor);
                    Control_3.BackColor = Color.FromName(Cor);
                    Control_4.BackColor = Color.FromName(Cor);
                    Control_5.BackColor = Color.FromName(Cor);
                    Control_6.BackColor = Color.FromName(Cor);
                    Control_7.BackColor = Color.FromName(Cor);
                }
                else
                {
                    Control_1.BackColor = Color.Silver;
                    Control_2.BackColor = Color.Silver;
                    Control_3.BackColor = Color.Silver;
                    Control_4.BackColor = Color.Silver;
                    Control_5.BackColor = Color.Silver;
                    Control_6.BackColor = Color.Silver;
                    Control_7.BackColor = Color.Silver;
                }
            }
            catch (Exception)
            {

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
            string NomeDoNó = "Pesquisa";
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

        //APLICA OS FILTROS DA PESQUISA
        public void Pesc_FILTROS(string LojaLogada, RadioButton rabAlfabetico, RadioButton rabNumerico, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, DataGridView Dgv_Pesquisa, TextBox txtDescri, ComboBox comUF, ComboBox comStatus)
        {
            string QuantidadeResultadosPadrao = "";

            //Captura a quantidade de resultado padrão na configuração geral do sistema
            #region CAPTURA A QUANTIDADE DE RESULTADOS NA CONFIGURAÇÃO DO SISTEMA
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StringCaptura = "SELECT QtPesq_CON FROM TabConfi WHERE SeqLoj_CON = @Sequen";
            SqlCommand ComandoCaptura = new SqlCommand(StringCaptura, Conexão);

            ComandoCaptura.Parameters.Add("@Sequen", SqlDbType.Int).Value = LojaLogada;

            try
            {
                SqlDataReader Dr = ComandoCaptura.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    QuantidadeResultadosPadrao = Dr["QtPesq_CON"].ToString();
                }
                else
                {
                    QuantidadeResultadosPadrao = "20";
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Pesc_FILTROS()\n\nBLOCO = CAPTURA A QUANTIDADE DE RESULTADOS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Pesc_FILTROS()\n\nBLOCO = CAPTURA A QUANTIDADE DE RESULTADOS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
            #endregion


            if (rabAlfabetico.Checked == false && rabNumerico.Checked == false)
            {
                rabNumerico.Checked = true;
            }

            if (rabTodos.Checked == false && rabTOP.Checked == false)
            {
                rabTOP.Checked = true;
                nupQtResultados.Value = Convert.ToDecimal(QuantidadeResultadosPadrao);
            }

            if (rabTOP.Checked == true && nupQtResultados.Value == 0)
            {
                nupQtResultados.Value = Convert.ToDecimal(QuantidadeResultadosPadrao);
            }

            if (comUF.SelectedIndex < 0)
            {
                comUF.SelectedIndex = 0;
            }

            if (comStatus.SelectedIndex < 0)
            {
                comStatus.SelectedIndex = 0;
            }
        }

        //FAZ A PESQUISA NO FORMULÁRIOS
        public void Pesquisa_EXEC(string LojaLogada, RadioButton rabAlfabetico, RadioButton rabNumerico, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, DataGridView Dgv_Pesquisa, TextBox txtDescri, TextBox txtDescriUf, ComboBox comUF, ComboBox comStatus)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            //CASO O USUÁRIO COMEÇE A PESQUISA E OS FILTROS ESTEJAM EM BRANCO
            //ESSE METODO MARCA ELES AUTOMATICAMENTE
            #region APLICA OS FILTROS CASO ESTEJAM EM BRANCO
            Pesc_FILTROS(LojaLogada, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, txtDescri, comUF, comStatus);
            #endregion

            Dgv_Pesquisa.Rows.Clear();


            string NúmeroResults = "TOP " + nupQtResultados.Value.ToString();
            if (rabTodos.Checked == true)
            {
                NúmeroResults = "";
            }


            String Select_CMD = String.Format("SELECT " + NúmeroResults + " Sequen_CID,Descri_CID,UfFede_CID,Status_CID,IbgeMu_CID FROM TabCidad WHERE 1=1");
            if (!String.IsNullOrEmpty(txtDescri.Text))
                Select_CMD += " AND Descri_CID LIKE '%' + @1 + '%'";
            if (!String.IsNullOrEmpty(txtDescriUf.Text))
                Select_CMD += " AND UfDesc_CID LIKE '%' + @2 + '%'";
            if (comUF.SelectedIndex > 0)
                Select_CMD += " AND UfFede_CID = '" + comUF.Text + "'";

            if (comStatus.SelectedIndex > 0)
                Select_CMD += " AND Status_CID = " + comStatus.SelectedIndex;



            if (rabAlfabetico.Checked == true)
                Select_CMD += " ORDER BY Descri_CID";
            if (rabNumerico.Checked == true)
                Select_CMD += " ORDER BY Sequen_CID";


            SqlCommand ComandoPESQ = new SqlCommand(Select_CMD, Conexão);
            ComandoPESQ.Parameters.Add("@1", SqlDbType.VarChar).Value = txtDescri.Text;
            ComandoPESQ.Parameters.Add("@2", SqlDbType.VarChar).Value = txtDescriUf.Text;

            try
            {
                SqlDataReader Dr = ComandoPESQ.ExecuteReader();
                while (Dr.Read())
                {
                    if (Dr.HasRows)
                    {
                        string Sequen_CID = Dr["Sequen_CID"].ToString().PadLeft(6, '0');
                        string Descri_CID = Dr["Descri_CID"].ToString();
                        string UfFede_CID = Dr["UfFede_CID"].ToString();
                        string Status_CID = Dr["Status_CID"].ToString();
                        string IbgeMu_CID = Dr["IbgeMu_CID"].ToString().PadLeft(6, '0');

                        #region TRATAMENTO STATUS
                        if (Convert.ToInt32(Status_CID) == 1)
                        {
                            Status_CID = "ATIVA";
                        }
                        else if (Convert.ToInt32(Status_CID) == 2)
                        {
                            Status_CID = "INATIVA";
                        }
                        else if (Convert.ToInt32(Status_CID) == 3)
                        {
                            Status_CID = "EXCLUIDA";
                        }
                        else
                        {
                            Status_CID = "ERRO.";
                        }
                        #endregion



                        Dgv_Pesquisa.Rows.Add(Sequen_CID, Descri_CID, UfFede_CID, Status_CID, IbgeMu_CID);
                    }
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Pesc_EXECUTAR()\n\nBLOCO = MÉTODO DA PESQUISA\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Pesc_EXECUTAR()\n\nBLOCO = MÉTODO DA PESQUISA\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //GRAVA O XML DOS FILTROS
        public void GravarFILTROS(CheckBox cheFiltrosPES, RadioButton rabNumerico, RadioButton rabAlfabetica, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, ComboBox comUF, ComboBox comStatus)
        {
            #region CRIA O DIRETÓRIO SE NÃO EXISTIR
            if (!Directory.Exists("..\\Log"))
            {
                Directory.CreateDirectory("..\\Log");
            }
            #endregion

            try
            {
                string Patch = @"..\Log\Pe_PesCidad.xml";
                XmlTextWriter GerarXML = new XmlTextWriter(Patch, null);
                GerarXML.WriteStartDocument();
                GerarXML.WriteStartElement("Dados_PesCidad");

                //PESQUISA
                GerarXML.WriteElementString("cheFiltrosPES", WenProtects(cheFiltrosPES.Checked.ToString()));
                GerarXML.WriteElementString("rabNumerico", WenProtects(rabNumerico.Checked.ToString()));
                GerarXML.WriteElementString("rabAlfabetica", WenProtects(rabAlfabetica.Checked.ToString()));
                GerarXML.WriteElementString("rabTodos", WenProtects(rabTodos.Checked.ToString()));
                GerarXML.WriteElementString("rabTOP", WenProtects(rabTOP.Checked.ToString()));
                GerarXML.WriteElementString("nupQtResultados", WenProtects(nupQtResultados.Value.ToString()));
                GerarXML.WriteElementString("comUF", WenProtects(comUF.SelectedIndex.ToString()));
                GerarXML.WriteElementString("comStatus", WenProtects(comStatus.SelectedIndex.ToString()));

                GerarXML.WriteEndElement();
                GerarXML.Close();
            }
            catch (XmlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarFILTROS()\n\nBLOCO = CLASSE _FILTROS\n\n" + Ex.Message, "TechSIS XML Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarFILTROS()\n\nBLOCO = CLASSE _FILTROS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //GRAVA O XML DOS FILTROS
        public void CarregaFILTROS(CheckBox cheFiltrosPES, RadioButton rabNumerico, RadioButton rabAlfabetica, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, ComboBox comUF, ComboBox comStatus)
        {
            string Patch = @"..\Log\Pe_PesCidad.xml";
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
                    XmlNode comUF_xml = LerXML.DocumentElement.SelectSingleNode("comUF");
                    XmlNode comStatus_xml = LerXML.DocumentElement.SelectSingleNode("comStatus");


                    #region STRING PESQUISA
                    string _cheFiltrosPES = WenDisprotects(cheFiltrosPES_xml.InnerText.ToString());
                    string _rabNumerico = WenDisprotects(rabNumerico_xml.InnerText.ToString());
                    string _rabAlfabetica = WenDisprotects(rabAlfabetica_xml.InnerText.ToString());
                    string _rabTodos = WenDisprotects(rabTodos_xml.InnerText.ToString());
                    string _rabTOP = WenDisprotects(rabTOP_xml.InnerText.ToString());
                    string _nupQtResultados = WenDisprotects(nupQtResultados_xml.InnerText.ToString());
                    string _comUF = WenDisprotects(comUF_xml.InnerText.ToString());
                    string _comStatus = WenDisprotects(comStatus_xml.InnerText.ToString());
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

                        comUF.SelectedIndex = Convert.ToInt32(_comUF);
                        comStatus.SelectedIndex = Convert.ToInt32(_comStatus);
                    }
                    else
                    {
                        rabNumerico.Checked = false;
                        rabAlfabetica.Checked = false;
                        rabTodos.Checked = false;
                        rabTOP.Checked = false;
                        nupQtResultados.Value = 0;
                        comUF.SelectedIndex = -1;
                        comStatus.SelectedIndex = -1;
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
