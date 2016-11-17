using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Data.SqlClient;

namespace TabUsuar
{
    internal class TabUsuar_Histórico
    {
        //VERIFICA SE A DATA É VALIDA
        public bool HIS_VerificaDATA(MaskedTextBox CampoAvaliado, Control Retorno)
        {
            if (CampoAvaliado.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
            {
                return false;
            }
            else
            {
                try
                {
                    string MeuTEXTO = CampoAvaliado.Text;
                    string MeuAno = DateTime.Now.Year.ToString();
                    if (CampoAvaliado.Text.Length <= 6)
                    {
                        CampoAvaliado.Text = MeuTEXTO + MeuAno;
                    }
                    Convert.ToDateTime(CampoAvaliado.Text);
                    return false;
                }
                catch (Exception)
                {
                    MessageBox.Show("DATA INFORMADA É INVÁLIDA..VERIFIQUE!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Retorno.Select();
                    SendKeys.Send("{HOME}");
                    SendKeys.Send("+{END}");
                    return true;
                }
            }
        }

        //QUANDO SEGUNDA DATA ESTÁ PREENCHIDA, SEGUNDA É OBRIGATÓRIA
        public bool HIS_VerificaCamposObrig(MaskedTextBox mtbData1His, MaskedTextBox mtbData2His, Control Retorno)
        {
            if (char.IsLetterOrDigit(mtbData2His.Text[0]) && !char.IsLetterOrDigit(mtbData1His.Text[0]))
            {
                MessageBox.Show("Quando segunda data está preenchida, primeira é obrigatória", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Retorno.Select();
                SendKeys.Send("{HOME}");
                SendKeys.Send("+{END}");
                return true;
            }
            return false;
        }

        //VERIFICA SE SEGUNDA DATA É MENOR QUE A PRIMEIRA
        public bool HIS_VerificaDataMenor(MaskedTextBox mtbData1His, MaskedTextBox mtbData2His, Control Retorno)
        {
            try
            {
                if (Convert.ToDateTime(mtbData1His.Text) > Convert.ToDateTime(mtbData2His.Text))
                {
                    MessageBox.Show("Segunda data não pode ser menor que primeira.", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Retorno.Select();
                    SendKeys.Send("{HOME}");
                    SendKeys.Send("+{END}");
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }



        //CAMPOS INICIAIS QUANDO A ABA É SELECIONADA
        public void HIS_SelecionaDados(MaskedTextBox mtbData1His, MaskedTextBox mtbData2His, TextBox txtUsuarioHist, TextBox txtUsuario)
        {
            string Hoje = DateTime.Today.ToString("dd/MM/yyyy");
            string PrimeiroDiaDoMes = DateTime.Today.ToString("01/MM/yyyy");
            mtbData1His.Text = PrimeiroDiaDoMes;
            mtbData2His.Text = Hoje;
            txtUsuarioHist.Text = txtUsuario.Text.PadLeft(6, '0');
        }





        //VERIFICA SE O USUÁRIO INFORMADO EXISTE
        public void HIS_VerificaUsuarioEXISTE(TextBox txtUsuarioHis, Control Retorno, DataGridView Dgv_Histórico, MaskedTextBox mtbData1His, MaskedTextBox mtbData2His, ComboBox comIDHis, Panel panUpAb5, TextBox txtUsuarioDescHis)
        {
            txtUsuarioHis.Text = txtUsuarioHis.Text.PadLeft(6, '0');

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            string StringComando = "SELECT Descri_USU FROM TabUsuar WHERE Sequen_USU = " + txtUsuarioHis.Text;
            SqlCommand Comando = new SqlCommand(StringComando, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string Descri = Dr["Descri_USU"].ToString();
                    txtUsuarioDescHis.Text = Descri;
                }
                else
                {
                    mtbData2His.Focus();
                    txtUsuarioHis.SelectAll();
                    MessageBox.Show("Usuário inexistente no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dgv_Histórico.Rows.Clear();
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método VerificaUsuarioEXISTE()\n\nBLOCO = VERIFICAÇÃO DE USUÁRIO\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método VerificaUsuarioEXISTE()\n\nBLOCO = VERIFICAÇÃO DE USUÁRIO\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }



        //POPULA O HISTÓRICO
        public void HIS_PopularHISTORICO(TextBox txtUsuarioHis, Control Retorno, DataGridView Dgv_Histórico, MaskedTextBox mtbData1His, MaskedTextBox mtbData2His, ComboBox comIDHis, Panel panUpAb5)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            Dgv_Histórico.Rows.Clear();

            //VERIFICA DATAS VALIDAS
            #region Verifica DATA
            bool Val_1 = HIS_VerificaDATA(mtbData1His, mtbData1His);
            if (!Val_1)
            { }
            else
            { Dgv_Histórico.Rows.Clear(); return; }
            bool Val_2 = HIS_VerificaDATA(mtbData2His, mtbData2His);
            if (!Val_2)
            { }
            else
            { Dgv_Histórico.Rows.Clear(); return; }
            #endregion

            //QUANDO SEGUNDA DATA ESTÁ PREENCHIDA, SEGUNDA É OBRIGATÓRIA
            #region Verifica Campos Obrig
            bool Obrig = HIS_VerificaCamposObrig(mtbData1His, mtbData2His, mtbData1His);
            if (!Obrig)
            { }
            else
            { Dgv_Histórico.Rows.Clear(); return; }
            #endregion

            //VERIFICA DATA MENOR
            #region Verifica Campos Obrig
            bool Mnor = HIS_VerificaDataMenor(mtbData1His, mtbData2His, mtbData2His);
            if (!Mnor)
            { }
            else
            { Dgv_Histórico.Rows.Clear(); return; }
            #endregion


            String Select_CMD = String.Format("SELECT TipLan_HIS,Usuari_HIS,Apelid_USU,ObsLa1_HIS,ObsLa2_HIS,DtLanc_HIS FROM TabHisto INNER JOIN TabUsuar ON TabHisto.Usuari_HIS = TabUsuar.Sequen_USU WHERE Prog01_HIS = '090100'");
            if (!String.IsNullOrEmpty(txtUsuarioHis.Text))
                Select_CMD += " AND Usuari_HIS = @USUAR";
            if (comIDHis.SelectedIndex > 0)
                Select_CMD += " AND TipLan_HIS = @TIPO";
            if (!String.IsNullOrEmpty(mtbData1His.Text) && !String.IsNullOrEmpty(mtbData2His.Text))
                Select_CMD += " AND DtLanc_HIS >= @DATA_1 AND DtLanc_HIS <= @DATA_2";



            SqlCommand Comando = new SqlCommand(Select_CMD, Conexão);
            Comando.Parameters.Add("@USUAR", SqlDbType.VarChar).Value = txtUsuarioHis.Text;
            Comando.Parameters.Add("@TIPO", SqlDbType.VarChar).Value = comIDHis.Text;
            #region Parameters DATA
            if (mtbData1His.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
            {
                Comando.Parameters.Add("@DATA_1", SqlDbType.DateTime).Value = "01/01/1800";
            }
            else
            {
                Comando.Parameters.Add("@DATA_1", SqlDbType.DateTime).Value = mtbData1His.Text;
            }
            if (mtbData2His.Text.Count(c => char.IsLetterOrDigit(c)) == 0)
            {
                Comando.Parameters.Add("@DATA_2", SqlDbType.DateTime).Value = "01/01/2900";
            }
            else
            {
                Comando.Parameters.Add("@DATA_2", SqlDbType.DateTime).Value = mtbData2His.Text + " 23:59:59.000";
            }
            #endregion

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader();

                while (Dr.Read())
                {
                    if (Dr.HasRows)
                    {
                        string TipLan_HIS = Dr["TipLan_HIS"].ToString();
                        string Usuari_HIS = Dr["Usuari_HIS"].ToString().PadLeft(6, '0');
                        string ObsLa1_HIS = Dr["ObsLa1_HIS"].ToString();
                        string ObsLa2_HIS = Dr["ObsLa2_HIS"].ToString();
                        string DtLanc_HIS = Dr["DtLanc_HIS"].ToString();
                        //Inner Join
                        string Apelid_USU = Dr["Apelid_USU"].ToString();

                        Dgv_Histórico.Rows.Add(TipLan_HIS, Usuari_HIS, Apelid_USU, ObsLa1_HIS, ObsLa2_HIS, DtLanc_HIS);
                    }
                }
                if (Dgv_Histórico.Rows.Count == 0)
                {
                    MessageBox.Show("Nenhum resultado encontrado. Verifique!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Dr.Close();
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método HIS_PopularHISTORICO()\n\nBLOCO = POPULAR O HISTÓRICO\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método HIS_PopularHISTORICO()\n\nBLOCO = POPULAR O HISTÓRICO\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }
    }
}
