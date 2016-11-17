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

namespace TabRotas
{
    internal class TabRotas_Lixeira
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


            string LixeiraPOP = "SELECT * FROM TabRotas WHERE Status_ROT = 3";
            SqlCommand _LixeiraPOP = new SqlCommand(LixeiraPOP, Conexão);

            try
            {
                SqlDataReader Dr = _LixeiraPOP.ExecuteReader();
                if (Dr.HasRows)
                {
                    while (Dr.Read())
                    {
                        string Sequen_ROT = Dr["Sequen_ROT"].ToString().PadLeft(6, '0');
                        string Descri_ROT = Dr["Descri_ROT"].ToString();


                        Dgv_Lixeira.Rows.Add(Sequen_ROT, Descri_ROT);
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
                string _ComandoRESTAU = "UPDATE TabRotas SET Status_ROT = 1 WHERE Sequen_ROT = @Sequen_ROT";
                string StringComandoRESTAU_HIST = "INSERT INTO TabHisto (Sequen_HIS,Prog01_HIS,TipLan_HIS,ObsLa1_HIS,ObsLa2_HIS,Usuari_HIS,DtLanc_HIS) VALUES (@SequenHIS,'010400','RESTAURAÇÃO',@ObsLa1,@ObsLa2,@Usuari,@DtCada)";


                SqlCommand CodigoAoResta = new SqlCommand(_ComandoRESTAU, Conexão);
                SqlCommand ComandoRESTAU_HIST = new SqlCommand(StringComandoRESTAU_HIST, Conexão);

                //Parametros do Insert no historico
                ComandoRESTAU_HIST.Parameters.Add("@SequenHIS", SqlDbType.Int).Value = _SequenHIS;
                ComandoRESTAU_HIST.Parameters.Add("@ObsLa1", SqlDbType.VarChar).Value = "RESTAURAÇÃO DA LIXEIRA.: " + _CodigoAoResta;
                ComandoRESTAU_HIST.Parameters.Add("@ObsLa2", SqlDbType.VarChar).Value = Dgv_Lixeira.CurrentRow.Cells["dgvcDescriLix"].Value.ToString();
                ComandoRESTAU_HIST.Parameters.Add("@Usuari", SqlDbType.Int).Value = txtUsuario.Text;
                ComandoRESTAU_HIST.Parameters.Add("@DtCada", SqlDbType.DateTime).Value = DateTime.Now.ToString();

                CodigoAoResta.Parameters.Add("@Sequen_ROT", SqlDbType.VarChar).Value = _CodigoAoResta;

                try
                {
                    ComandoRESTAU_HIST.ExecuteNonQuery();
                    CodigoAoResta.ExecuteNonQuery();
                    MessageBox.Show("Registro foi restaurado com sucesso!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dgv_Lixeira.Rows.Remove(Dgv_Lixeira.CurrentRow);

                    if (cheVoltarLix.Checked == true)
                    {
                        TabControl.SelectedTab = Tp1;
                        TabRotas_AppaButtons Apa = new TabRotas_AppaButtons();
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
