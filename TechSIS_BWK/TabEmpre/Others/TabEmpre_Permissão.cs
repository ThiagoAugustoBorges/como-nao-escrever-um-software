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

namespace TabEmpre
{
    public class TabEmpre_Permissão
    {
        public void VerificaPermi_Buttons(Button btnINC, Button btnALT, Button btnSet1, Button btnSet2, Button btnSet3, Button btnSet4, TextBox TXT_Codigo, string CodigoUSU)
        {
            string CaminhoDoPrograma = "'010100'";
            string NomeDoPrograma = "Tabela de Empresas";


            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();



            string Select_Permi = "SELECT PerINC_PER,PerALT_PER,PerEXC_PER,PerCON_PER FROM TabPermi WHERE SeqPgr_PER = " + CaminhoDoPrograma + " AND SeqUsu_PER = " + CodigoUSU;

            SqlCommand _Go = new SqlCommand(Select_Permi, Conexão);

            try
            {
                SqlDataReader Dr = _Go.ExecuteReader(); Dr.Read();
                string Inc = Convert.ToString(Dr[0]);
                string Alt = Convert.ToString(Dr[1]);
                string Exc = Convert.ToString(Dr[2]);
                string Set = Convert.ToString(Dr[3]);




                //INCLUIR
                if (Inc == "False" && Convert.ToInt32(CodigoUSU) != 1)
                {
                    btnINC.Enabled = false;
                }
                else
                {
                    btnINC.Enabled = true;
                }

                //ALTERAR
                if (Alt == "False" && Convert.ToInt32(CodigoUSU) != 1)
                {
                    btnALT.Enabled = false;
                }
                else
                {
                    btnALT.Enabled = true;
                }

                ////EXCLUIR
                //if (Exc == "False" && Convert.ToInt32(CodigoUSU) != 1)
                //{
                //    BT_Excluir.Enabled = false;
                //}
                //else
                //{
                //    BT_Excluir.Enabled = true;
                //}

                //SETAS
                if (Set == "False" && Convert.ToInt32(CodigoUSU) != 1)
                {
                    btnSet1.Enabled = false;
                    btnSet2.Enabled = false;
                    btnSet3.Enabled = false;
                    btnSet4.Enabled = false;
                }
                else
                {
                    btnSet1.Enabled = true;
                    btnSet2.Enabled = true;
                    btnSet3.Enabled = true;
                    btnSet4.Enabled = true;
                }


                if (Inc == "False" && Alt == "False" && Exc == "False" && Set == "False")
                {
                    MessageBox.Show("Usuário sem nenhuma permissão para esta opção. Código bloqueado", "TechSIS BWK Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TXT_Codigo.Enabled = false;
                }

            }
            catch (Exception)
            {
                if (Convert.ToInt32(CodigoUSU) != 1)
                {
                    MessageBox.Show("Usuário " + CodigoUSU + " sem permissão cadastrada.: " + NomeDoPrograma, "TechSIS BWK Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally 
            { 
                Conexão.Close();
            }

        } 
    }
}
