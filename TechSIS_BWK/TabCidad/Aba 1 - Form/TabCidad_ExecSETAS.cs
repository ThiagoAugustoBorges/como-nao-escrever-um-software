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
using System.Xml;


namespace TabCidad
{
    internal class TabCidad_ExecSETAS
    {
        public void ExecSETAs(string StringComandoSELEÇÃO, TextBox txtMESTRE, TextBox txtCodigo, Panel panCodigoAb1, MethodInvoker ZerarCampos, MethodInvoker CamposDisable, Button btnGravar, MethodInvoker CamposEnable, TextBox txtDescri, TextBox txtPaisCod, TextBox txtPaisDesc, TextBox txtIbgeUF, ComboBox comUF, TextBox txtUFDesc, TextBox txtIbgeMuCod, TextBox txtIbgeMuDesc, TextBox txtIbgeEstadual, MaskedTextBox mtbCep1, MaskedTextBox mtbCep2, ComboBox comStatus)
        {
            string Mensagem_De_Erro = "";

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string NomeDaOpção = "Cidades";

            #region Comandos
            if (StringComandoSELEÇÃO == "1")
            {
                StringComandoSELEÇÃO = "SELECT TOP 1 Sequen_CID,Descri_CID,UfInde_CID,PaisCi_CID,Descri_PAI,IbgeMu_CID,Descri_MUN,IbgeEs_CID,CepCi1_CID,CepCi2_CID,Status_CID FROM TabCidad INNER JOIN TabCidMu ON TabCidad.IbgeMu_CID = TabCidMu.Codigo_MUN INNER JOIN TabPaise ON TabCidad.PaisCi_CID = TabPaise.Codigo_PAI WHERE Status_CID <> 3 ORDER BY Sequen_CID";
                Mensagem_De_Erro = "Sem dados de " + NomeDaOpção + " para exibir";
            }
            if (StringComandoSELEÇÃO == "2")
            {
                string Num = txtCodigo.Text;
                if (txtCodigo.Text == string.Empty)
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 Sequen_CID,Descri_CID,UfInde_CID,PaisCi_CID,Descri_PAI,IbgeMu_CID,Descri_MUN,IbgeEs_CID,CepCi1_CID,CepCi2_CID,Status_CID FROM TabCidad INNER JOIN TabCidMu ON TabCidad.IbgeMu_CID = TabCidMu.Codigo_MUN INNER JOIN TabPaise ON TabCidad.PaisCi_CID = TabPaise.Codigo_PAI WHERE Status_CID <> 3 ORDER BY Sequen_CID";
                    Mensagem_De_Erro = "Não existe registro anterior no banco";
                }
                else
                {
                    StringComandoSELEÇÃO = "SELECT TOP 1 Sequen_CID,Descri_CID,UfInde_CID,PaisCi_CID,Descri_PAI,IbgeMu_CID,Descri_MUN,IbgeEs_CID,CepCi1_CID,CepCi2_CID,Status_CID FROM TabCidad INNER JOIN TabCidMu ON TabCidad.IbgeMu_CID = TabCidMu.Codigo_MUN INNER JOIN TabPaise ON TabCidad.PaisCi_CID = TabPaise.Codigo_PAI WHERE Sequen_CID < " + Num + " AND Status_CID <> 3 ORDER BY Sequen_CID DESC";
                    Mensagem_De_Erro = "Não existe registro anterior no banco";
                }
            }
            if (StringComandoSELEÇÃO == "3")
            {
                string Num = txtCodigo.Text;
                if (txtCodigo.Text == string.Empty)
                {
                    Num = "0";
                }
                StringComandoSELEÇÃO = "SELECT TOP 1 Sequen_CID,Descri_CID,UfInde_CID,PaisCi_CID,Descri_PAI,IbgeMu_CID,Descri_MUN,IbgeEs_CID,CepCi1_CID,CepCi2_CID,Status_CID FROM TabCidad INNER JOIN TabCidMu ON TabCidad.IbgeMu_CID = TabCidMu.Codigo_MUN INNER JOIN TabPaise ON TabCidad.PaisCi_CID = TabPaise.Codigo_PAI WHERE Sequen_CID > " + Num + " AND Status_CID <> 3";
                Mensagem_De_Erro = "Não existe próximo registro no banco";
            }
            if (StringComandoSELEÇÃO == "4")
            {
                StringComandoSELEÇÃO = "SELECT TOP 1 Sequen_CID,Descri_CID,UfInde_CID,PaisCi_CID,Descri_PAI,IbgeMu_CID,Descri_MUN,IbgeEs_CID,CepCi1_CID,CepCi2_CID,Status_CID FROM TabCidad INNER JOIN TabCidMu ON TabCidad.IbgeMu_CID = TabCidMu.Codigo_MUN INNER JOIN TabPaise ON TabCidad.PaisCi_CID = TabPaise.Codigo_PAI WHERE Status_CID <> 3 AND Sequen_CID >= 1 ORDER BY Sequen_CID DESC";
                Mensagem_De_Erro = "Sem dados de " + NomeDaOpção + " para exibir";
            }
            #endregion

            SqlCommand ComandoSELEÇÃO = new SqlCommand(StringComandoSELEÇÃO, Conexão);

            try
            {
                SqlDataReader Dr = ComandoSELEÇÃO.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    int STATUS = Convert.ToInt32(Dr["Status_CID"]);

                    txtCodigo.Text = Dr["Sequen_CID"].ToString().PadLeft(6, '0');
                    txtDescri.Text = Dr["Descri_CID"].ToString();
                    txtPaisCod.Text = Dr["PaisCi_CID"].ToString().PadLeft(6, '0'); ;
                    txtPaisDesc.Text = Dr["Descri_PAI"].ToString();
                    txtIbgeMuCod.Text = Dr["IbgeMu_CID"].ToString();
                    txtIbgeMuDesc.Text = Dr["Descri_MUN"].ToString();
                    txtIbgeEstadual.Text = Dr["IbgeEs_CID"].ToString();
                    mtbCep1.Text = Dr["CepCi1_CID"].ToString();
                    mtbCep2.Text = Dr["CepCi2_CID"].ToString();

                    comUF.SelectedIndex = Convert.ToInt32(Dr["UfInde_CID"]);


                    if (STATUS > 3 || STATUS <= 0)
                    {
                        STATUS = 2;
                    }
                    comStatus.SelectedIndex = STATUS;

                    #region TRATAMENTOS PARA CAMPOS INT = 0
                    if (Convert.ToInt32(Dr["CepCi1_CID"]) == 0)
                    {
                        mtbCep1.Text = string.Empty;
                    }
                    if (Convert.ToInt32(Dr["CepCi2_CID"]) == 0)
                    {
                        mtbCep2.Text = string.Empty;
                    }
                    if (Convert.ToInt32(Dr["IbgeEs_CID"]) == 0)
                    {
                        txtIbgeEstadual.Text = string.Empty;
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show(Mensagem_De_Erro, "Consulta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método ExecSETAs()\n\nBLOCO = TabCidad_ExecSETAS\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método ExecSETAs()\n\nBLOCO = TabCidad_ExecSETAS\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }
    }
}
