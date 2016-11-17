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

namespace TabCfope
{
    internal class TabCfope_Permi
    {
        //VERIFICA A PERMISSÃO NOS BUTTONS
        public void PER_Permiss_Buttons(Button btnIncluir, Button btnAlterar, Button btnExcluir, Button btnSeta1, Button btnSeta2, Button btnSeta3, Button btnSeta4, TextBox txtCodigo, string CodigoUSU)
        {
            string CaminhoDoPrograma = "'010300'";
            string NomeDoPrograma = "Tabela de CFOPs";

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string strComando = "SELECT PerINC_PER,PerALT_PER,PerEXC_PER,PerCON_PER FROM TabPermi WHERE SeqPgr_PER = " + CaminhoDoPrograma + " AND SeqUsu_PER = " + CodigoUSU;

            SqlCommand SQLComando = new SqlCommand(strComando, Conexão);

            try
            {
                SqlDataReader Dr = SQLComando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string Inc = Convert.ToString(Dr[0]);
                    string Alt = Convert.ToString(Dr[1]);
                    string Exc = Convert.ToString(Dr[2]);
                    string Set = Convert.ToString(Dr[3]);



                    #region INCLUIR
                    //INCLUIR
                    if (Inc == "False" && Convert.ToInt32(CodigoUSU) != 1)
                    {
                        btnIncluir.Enabled = false;
                    }
                    else
                    {
                        btnIncluir.Enabled = true;
                    }
                    #endregion
                    #region ALTERAR
                    //ALTERAR
                    if (Alt == "False" && Convert.ToInt32(CodigoUSU) != 1)
                    {
                        btnAlterar.Enabled = false;
                    }
                    else
                    {
                        btnAlterar.Enabled = true;
                    }
                    #endregion
                    #region EXCLUIR
                    //EXCLUIR
                    if (Exc == "False" && Convert.ToInt32(CodigoUSU) != 1)
                    {
                        btnExcluir.Enabled = false;
                    }
                    else
                    {
                        btnExcluir.Enabled = true;
                    }
                    #endregion
                    #region SETAS
                    //SETAS
                    if (Set == "False" && Convert.ToInt32(CodigoUSU) != 1)
                    {
                        btnSeta1.Enabled = false;
                        btnSeta2.Enabled = false;
                        btnSeta3.Enabled = false;
                        btnSeta4.Enabled = false;
                    }
                    else
                    {
                        btnSeta1.Enabled = true;
                        btnSeta2.Enabled = true;
                        btnSeta3.Enabled = true;
                        btnSeta4.Enabled = true;
                    }
                    #endregion

                    if (Inc == "False" && Alt == "False" && Exc == "False" && Set == "False")
                    {
                        MessageBox.Show("Usuário sem nenhuma permissão para esta opção. Código bloqueado", "TechSIS Aviso..: " + CodigoUSU.PadLeft(6, '0'), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCodigo.Enabled = false;
                    }
                }
                else
                {
                    if (Convert.ToInt32(CodigoUSU) != 1)
                    {
                        MessageBox.Show("Usuário " + CodigoUSU + " sem permissão cadastrada.: " + NomeDoPrograma, "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro no método Permiss_Buttons()\n\nException.: " + Ex.Message, "TechSIS Erro Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }

        //VERIFICA A PERMISSÃO POR ABAS
        public void PER_VerificaPermi_TbCont(TabControlCancelEventArgs e, int IndexDaAba, TextBox txtCodigo, string CodigoUSU)
        {
            string CaminhoDoPrograma = "'010300'";

            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            string Select_Permi = "SELECT PerABA_PER,PerAb1_PER,PerAb2_PER,PerAb3_PER,PerAb4_PER FROM TabPermi WHERE SeqPgr_PER = " + CaminhoDoPrograma + " AND SeqUsu_PER = " + CodigoUSU;

            SqlCommand _Go = new SqlCommand(Select_Permi, Conexão);

            try
            {
                SqlDataReader Dr = _Go.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string Bloqueia_Abas = Convert.ToString(Dr[0]);
                    string Aba1 = Convert.ToString(Dr[1]);
                    string Aba2 = Convert.ToString(Dr[2]);
                    string Aba3 = Convert.ToString(Dr[3]);
                    string Aba4 = Convert.ToString(Dr[4]);

                    if (Bloqueia_Abas == "True" && Convert.ToInt32(CodigoUSU) != 1)
                    {
                        if (Aba1 == IndexDaAba.ToString() || Aba2 == IndexDaAba.ToString() || Aba3 == IndexDaAba.ToString() || Aba4 == IndexDaAba.ToString())
                        {
                            MessageBox.Show("Usuário sem permissão de acesso a Aba selecionada.", "TechSIS Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            e.Cancel = true;
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Erro na Permissão por Abas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }
    }
}
