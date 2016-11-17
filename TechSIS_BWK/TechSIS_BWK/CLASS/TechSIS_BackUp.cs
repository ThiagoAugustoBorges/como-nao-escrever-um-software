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
using System.IO.Compression;
using Ionic.Utils.Zip;



namespace TechSIS_BWK
{
    internal class TechSIS_BackUp
    {
        public void BackUpBanco(string Caminho, string BancoDeDados, ProgressBar Pbar)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string StrComando = @"BACKUP DATABASE [" + BancoDeDados + "] TO DISK = '" + Caminho + "'";
            SqlCommand Comando = new SqlCommand(StrComando, Conexão);

            try
            {
                Comando.ExecuteNonQuery();
                Pbar.Maximum = Comando.CommandTimeout;
                Pbar.Value = Comando.CommandTimeout;
                MessageBox.Show("BackUp do banco de dados realizado com sucesso!\nCaminho do BackUp.: " + Caminho, "TechSIS Auto-Manutenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Pbar.Value = 0;
            }
            catch (SqlException Ex)
            {
                if (Ex.Number == 3201)
                {
                    MessageBox.Show(@"Você não tem permissão de Administrador para efetuar o BackUp no caminho desejado. Faça o BackUp em um diretório diferente do escolhido. Exemplo.:(D:\ ou E:\)." + "\nEvite escolher a unidade C: e qualquer outro diretório dentro da pasta WINDOWs", "TechSIS Auto-Manutenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Falha ao tentar realizar o BackUp do banco de dados!\nErro.: " + Ex.Message, "TechSIS Auto-Manutenção: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Falha ao tentar realizar o BackUp do banco de dados!\nErro.: " + Ex.Message, "TechSIS Auto-Manutenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void BackUpSistema(string Caminho, ProgressBar Pbar)
        {
            using (ZipFile Zip = new ZipFile("BackUp"))
            {
                Zip.AddDirectory(@"..\", "TechSIS_BWK");
                Pbar.Value = 0;
                Zip.Save(Caminho);
                Pbar.Value = Pbar.Maximum;
                MessageBox.Show("BackUp do SISTEMA realizado com sucesso!\nDiretório.: " + Caminho, "TechSIS Auto-Manutenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Pbar.Value = 0;
            }
        }
        public void BackUpFIL(string Caminho, ProgressBar Pbar)
        {
            using (ZipFile Zip = new ZipFile("BackUp"))
            {
                // add this map file into the "images" directory in the zip archive
                Zip.AddDirectory(@"E:\TechSIS Projeto\TechSIS_BWK\TechSIS_BWK\TechSIS_BWK\bin\Log", "Log");
                Pbar.Value = 0;
                Zip.Save(Caminho);
                Pbar.Value = Pbar.Maximum;
                MessageBox.Show("BackUp dos FILTROS realizado com sucesso!\nDiretório.: " + Caminho, "TechSIS Auto-Manutenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Pbar.Value = 0;
            }
        }
    }
}
