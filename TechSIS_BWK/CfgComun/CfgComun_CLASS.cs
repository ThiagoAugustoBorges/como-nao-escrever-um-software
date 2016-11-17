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

namespace CfgComun
{
    public class CfgComun_CLASS
    {
        //TROCA A COR DO FORMULÁRIO
        public void Cfg_SelecionaCorConfig(Control[] Controles, string _Login_LojaID)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string SelecionaCOR = "SELECT CorFun_CON FROM TabConfi WHERE SeqLoj_CON = @SeqLoj_CON";
            SqlCommand Comando = new SqlCommand(SelecionaCOR, Conexão);
            Comando.Parameters.Add("@SeqLoj_CON", SqlDbType.Int).Value = _Login_LojaID;

            SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();

            //EQUANTO EXISTIR CONTROLES, EU VOU EXECUTANDO
            for (int i = 0; i < Controles.Length; i++)
            {
                try
                {
                    if (Dr.HasRows)
                    {
                        string strNomeCor = Dr[0].ToString();
                        Controles[i].BackColor = Color.FromName(strNomeCor);
                    }
                    else
                    {
                        Controles[i].BackColor = Color.Silver;
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}