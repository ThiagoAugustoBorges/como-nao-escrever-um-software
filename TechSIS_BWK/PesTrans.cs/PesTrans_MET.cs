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

namespace PesTrans.cs
{
    internal class PesTrans_MET
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
        #region FORMATAR CPF.CNPJ
        public static string FormatarCpfCnpj(string strCpfCnpj)
        {
            if (strCpfCnpj.Length <= 11)
            {
                MaskedTextProvider mtpCpf = new MaskedTextProvider(@"000\.000\.000-00");
                mtpCpf.Set(ZerosEsquerda(strCpfCnpj, 11));
                return mtpCpf.ToString();
            }
            else
            {
                MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");
                mtpCnpj.Set(ZerosEsquerda(strCpfCnpj, 11));
                return mtpCnpj.ToString();
            }
        }
        public static string ZerosEsquerda(string strString, int intTamanho)
        {
            string strResult = "";
            for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
            {
                strResult += "0";
            }
            return strResult + strString;
        }
        #endregion


        //DEFINE O PACH DO XML
        public string PatchXML = "..\\Log\\Pe_PesTrans.xml";
        public string IdentXML = "PesTrans";

        //DEFINE QUE PODE RECEBER APEANS NÚMEROS
        public void MET_ApenasNúmeros(KeyPressEventArgs KeyPress)
        {
            if (!Char.IsDigit(KeyPress.KeyChar) && KeyPress.KeyChar != (char)8)
            {
                KeyPress.Handled = true;
                return;
            }
        }

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
        public void Pesc_FILTROS(string LojaLogada, RadioButton rabAlfabetico, RadioButton rabNumerico, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, DataGridView Dgv_Pesquisa, ComboBox comStatus, ComboBox comVeiculo, TextBox txtCidadeCod, TextBox txtCidadeDesc)
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


            if (comStatus.SelectedIndex < 0)
            {
                comStatus.SelectedIndex = 0;
            }

            if (comVeiculo.SelectedIndex < 0)
            {
                comVeiculo.SelectedIndex = 0;
            }


            if (txtCidadeDesc.Text == string.Empty)
            {
                txtCidadeCod.Text = string.Empty;
            }
        }

        //FAZ A PESQUISA NO FORMULÁRIOS
        public void Pesquisa_EXEC(string LojaLogada, RadioButton rabAlfabetico, RadioButton rabNumerico, RadioButton rabTodos, RadioButton rabTOP, NumericUpDown nupQtResultados, DataGridView Dgv_Pesquisa, ComboBox comStatus, ComboBox comVeiculo, TextBox txtCidadeCod, TextBox txtCidadeDesc, TextBox txtDescri, TextBox txtFantasia, TextBox txtCpfCnpj, TextBox txtPlaca, TextBox txtEmpresa)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            //CASO O USUÁRIO COMEÇE A PESQUISA E OS FILTROS ESTEJAM EM BRANCO
            //ESSE METODO MARCA ELES AUTOMATICAMENTE
            #region APLICA OS FILTROS CASO ESTEJAM EM BRANCO
            Pesc_FILTROS(LojaLogada, rabAlfabetico, rabNumerico, rabTodos, rabTOP, nupQtResultados, Dgv_Pesquisa, comStatus, comVeiculo, txtCidadeCod, txtCidadeDesc);
            #endregion

            Dgv_Pesquisa.Rows.Clear();


            string NúmeroResults = "TOP " + nupQtResultados.Value.ToString();
            if (rabTodos.Checked == true)
            {
                NúmeroResults = "";
            }


            String Select_CMD = String.Format("SELECT " + NúmeroResults + " Descri_TRA,Fantas_TRA,CpfCnp_TRA,Sequen_TRA,Status_TRA FROM TabTrans WHERE 1=1");
            if (!String.IsNullOrEmpty(txtDescri.Text))
                Select_CMD += " AND Descri_TRA LIKE '%' + @1 + '%'";
            if (!String.IsNullOrEmpty(txtFantasia.Text))
                Select_CMD += " AND Fantas_TRA LIKE '%' + @2 + '%'";

            if (!String.IsNullOrEmpty(txtEmpresa.Text))
                Select_CMD += " AND SeqEmp_TRA = " + txtEmpresa.Text;
            if (!String.IsNullOrEmpty(txtCidadeCod.Text))
                Select_CMD += " AND EndCi1_TRA = " + txtCidadeCod.Text;
            if (!String.IsNullOrEmpty(txtPlaca.Text))
                Select_CMD += " AND Placas_TRA LIKE '%' + @3 + '%'";
            if (!String.IsNullOrEmpty(txtCpfCnpj.Text))
                Select_CMD += " AND CpfCnp_TRA LIKE '%' + @4 + '%'";




            if (comStatus.SelectedIndex > 0)
                Select_CMD += " AND Status_TRA = " + comStatus.SelectedIndex;
            if (comVeiculo.SelectedIndex > 0)
                Select_CMD += " AND Veicul_TRA = " + comVeiculo.SelectedIndex;




            if (rabAlfabetico.Checked == true)
                Select_CMD += " ORDER BY Fantas_TRA";
            if (rabNumerico.Checked == true)
                Select_CMD += " ORDER BY Sequen_TRA";


            SqlCommand ComandoPESQ = new SqlCommand(Select_CMD, Conexão);
            ComandoPESQ.Parameters.Add("@1", SqlDbType.VarChar).Value = txtDescri.Text;
            ComandoPESQ.Parameters.Add("@2", SqlDbType.VarChar).Value = txtFantasia.Text;
            ComandoPESQ.Parameters.Add("@3", SqlDbType.VarChar).Value = txtPlaca.Text;
            ComandoPESQ.Parameters.Add("@4", SqlDbType.VarChar).Value = txtCpfCnpj.Text;

            try
            {
                SqlDataReader Dr = ComandoPESQ.ExecuteReader();
                while (Dr.Read())
                {
                    if (Dr.HasRows)
                    {
                        string Sequen_TRA = Dr["Sequen_TRA"].ToString().PadLeft(6, '0');
                        string Descri_TRA = Dr["Descri_TRA"].ToString();
                        string CpfCnp_TRA = Dr["CpfCnp_TRA"].ToString();
                        string Fantas_TRA = Dr["Fantas_TRA"].ToString();
                        string Status_TRA = Dr["Status_TRA"].ToString();

                        #region TRATAMENTO STATUS
                        if (Convert.ToInt32(Status_TRA) == 1)
                        {
                            Status_TRA = "ATIVO";
                        }
                        else if (Convert.ToInt32(Status_TRA) == 2)
                        {
                            Status_TRA = "INATIVO";
                        }
                        else if (Convert.ToInt32(Status_TRA) == 3)
                        {
                            Status_TRA = "EXCLUIDO";
                        }
                        else
                        {
                            Status_TRA = "ERRO.";
                        }
                        #endregion



                        Dgv_Pesquisa.Rows.Add(Sequen_TRA, Descri_TRA, FormatarCpfCnpj(CpfCnp_TRA), Fantas_TRA, Status_TRA);
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

        //BUSCA A EMPRESA
        public void SelectEmpresa(TextBox txtEmpresa)
        {
            txtEmpresa.Text = txtEmpresa.Text.PadLeft(6, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string strSelect = "SELECT Descri_EMP FROM TabEmpre WHERE Sequen_EMP = @Sequen_EMP";
            SqlCommand Comando = new SqlCommand(strSelect, Conexão);
            Comando.Parameters.Add("@Sequen_EMP", SqlDbType.Int).Value = txtEmpresa.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string Descri = Dr["Descri_EMP"].ToString();
                }
                else
                {
                    txtEmpresa.Text = string.Empty;
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método SelectEmpresa()\n\nBLOCO = PESQUISA\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método SelectEmpresa()\n\nBLOCO = PESQUISA\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //BUSCA A CIDADE
        public void SelectCidade(int ID, TextBox txtCidadeCod, TextBox txtCidadeDesc, Control Retorno)
        {
            //ID 1 = LEAVE
            //ID 2 = TAB

            txtCidadeCod.Text = txtCidadeCod.Text.PadLeft(6, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string strSelect = "SELECT Descri_CID FROM TabCidad WHERE Sequen_CID = @Sequen_CID";
            SqlCommand Comando = new SqlCommand(strSelect, Conexão);
            Comando.Parameters.Add("@Sequen_CID", SqlDbType.Int).Value = txtCidadeCod.Text;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string Descri = Dr["Descri_CID"].ToString();
                    if (ID == 2)
                    {
                        txtCidadeDesc.Text = Descri;
                    }
                }
                else
                {
                    txtCidadeDesc.Text = string.Empty;

                    if (ID == 1)
                    {
                        txtCidadeCod.Text = string.Empty;
                    }
                    if (ID == 2)
                    {
                        MessageBox.Show("Cidade não encontrada no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Retorno.Focus(); txtCidadeCod.SelectAll();
                    }
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método SelectCidade()\n\nBLOCO = PESQUISA\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método SelectCidade()\n\nBLOCO = PESQUISA\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //GRAVA O XML DOS FILTROS
        public void GravarFILTROS(Panel panPesq)
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
                foreach (Control PainelPrincipal in panPesq.Controls)
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

                GerarXML.WriteEndElement();
                GerarXML.Close();
            }
            catch (XmlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarFILTROS()\n\nBLOCO = CLASSE PesTrans_FILTROS\n\n" + Ex.Message, "TechSIS XML Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método GravarFILTROS()\n\nBLOCO = CLASSE PesTrans_FILTROS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //GRAVA O XML DOS FILTROS
        public void CarregaFILTROS(Panel panPesq)
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
                        foreach (Control PainelPrincipal in panPesq.Controls)
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
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
