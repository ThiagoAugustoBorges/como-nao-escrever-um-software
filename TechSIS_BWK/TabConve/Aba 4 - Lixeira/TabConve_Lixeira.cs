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

namespace TabConve
{
    internal class TabConve_Lixeira
    {
        //POPULA A LIXEIRA
        public void Lix_POPULAR(DataGridView Dgv_Lixeira, CheckBox cheVoltarLix, TabControl TabControl, TabPage Tp1)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            Dgv_Lixeira.Rows.Clear();


            string LixeiraPOP = "SELECT * FROM TabConve WHERE Status_COV = 3";
            SqlCommand _LixeiraPOP = new SqlCommand(LixeiraPOP, Conexão);

            try
            {
                SqlDataReader Dr = _LixeiraPOP.ExecuteReader();
                if (Dr.HasRows)
                {
                    while (Dr.Read())
                    {
                        string Sequen_COV = Dr["Sequen_COV"].ToString().PadLeft(6, '0');
                        string Descri_COV = Dr["Descri_COV"].ToString();
                        string Tipo01_COV = Dr["Tipo01_COV"].ToString();

                        #region TRATAMENTO TIPO
                        if (Convert.ToInt32(Tipo01_COV) == 1)
                        {
                            Tipo01_COV = "CONVÊNIO";
                        }
                        else if (Convert.ToInt32(Tipo01_COV) == 2)
                        {
                            Tipo01_COV = "CARTÃO C";
                        }
                        else if (Convert.ToInt32(Tipo01_COV) == 3)
                        {
                            Tipo01_COV = "CARTÃO D";
                        }
                        else
                        {
                            Tipo01_COV = "ERRO.";
                        }
                        #endregion

                        Dgv_Lixeira.Rows.Add(Sequen_COV, Descri_COV, Tipo01_COV);
                    }
                }
                else
                {
                    MessageBox.Show("Nenhum registro de exclusão foi encontrado", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TabControl.SelectedTab = Tp1;
                }

            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Lix_POPULAR()\n\nBLOCO = POPULA LIXEIRA\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Lix_POPULAR()\n\nBLOCO = POPULA LIXEIRA\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //RESTAURA O ITEM DA LIXEIRA
        public void Lix_RESTAURAR(DataGridView Dgv_Lixeira, CheckBox cheVoltarLix, TabControl TabControl, TabPage Tp1, TextBox txtUsuario, Button btnGravar, TextBox txtMESTRE, MethodInvoker CamposDisable, TextBox txtCodigo, Button btnIncluir)
        {
            DialogResult Restau = MessageBox.Show("DESEJA RESTAURAR O ITEM CLICADO DA LIXEIRA?", "TechSIS Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Restau == DialogResult.Yes)
            {
                //Cria a conexão com o Banco de Dados e Abre!
                StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
                string LerString = StringConexão.ReadLine();
                SqlConnection Conexão = new SqlConnection(LerString);
                Conexão.Open();

                //Pega o Ultimo registro da Tab de Historico!
                string PegarULTIMORegHIST = "SELECT MAX (Sequen_HIS + 1) FROM TabHisto";
                SqlCommand PegarREG = new SqlCommand(PegarULTIMORegHIST, Conexão);
                SqlDataReader Dr = PegarREG.ExecuteReader(); Dr.Read();
                int _SequenHIS = Convert.ToInt32(Dr[0].ToString());
                Dr.Close();


                string _CodigoAoResta = Dgv_Lixeira.CurrentRow.Cells["dgvcSequenLix"].Value.ToString();
                string _ComandoRESTAU = "UPDATE TabConve SET Status_COV = 1 WHERE Sequen_COV = @Sequen_COV";
                string StringComandoRESTAU_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'010600','RESTAURAÇÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";


                SqlCommand CodigoAoResta = new SqlCommand(_ComandoRESTAU, Conexão);
                SqlCommand ComandoRESTAU_HIST = new SqlCommand(StringComandoRESTAU_HIST, Conexão);

                //Parametros do Insert no historico
                ComandoRESTAU_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoRESTAU_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "RESTAURAÇÃO DA LIXEIRA.: " + _CodigoAoResta;
                ComandoRESTAU_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = Dgv_Lixeira.CurrentRow.Cells["dgvcDescriLix"].Value.ToString();
                ComandoRESTAU_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoRESTAU_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();

                CodigoAoResta.Parameters.Add("@Sequen_COV", SqlDbType.VarChar).Value = _CodigoAoResta;

                try
                {
                    CodigoAoResta.ExecuteNonQuery();
                    ComandoRESTAU_HIST.ExecuteNonQuery();
                    MessageBox.Show("Registro foi restaurado com sucesso!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dgv_Lixeira.Rows.Remove(Dgv_Lixeira.CurrentRow);

                    if (cheVoltarLix.Checked == true)
                    {
                        TabControl.SelectedTab = Tp1;
                        TabConve_AppaButtons Apa = new TabConve_AppaButtons();
                        Apa._ButtonZER(txtMESTRE, txtCodigo, btnIncluir, btnGravar, CamposDisable, TabControl, Tp1);
                        txtCodigo.Select(); txtCodigo.SelectAll();
                        txtCodigo.Text = _CodigoAoResta;
                        SendKeys.Send("{TAB}");
                    }
                }
                catch (SqlException Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Lix_RESTAURAR()\n\nBLOCO = RESTAURAR DA LIXEIRA\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Lix_RESTAURAR()\n\nBLOCO = RESTAURAR DA LIXEIRA\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Conexão.Close();
                }
            }
        }
    }
}
