using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Print_WORD
{
    internal class ImpreWORD_MET
    {
        public string LocalSalvamento { get; set; }
        public string LocalSalvamento_Arquivo { get; set; }

        //BUSCA O LOCAL DE SALVAMENTO DO ARQUIVO
        public void Busca_CaminhoSALV(string LojaLogada, TextBox txtCaminhoRel, string NomeDoArquivo)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();

            string SELECT_DIRETORIO = "SELECT CamRel_CON FROM TabConfi WHERE SeqLoj_CON = " + LojaLogada;
            SqlCommand _ComandoDIRE = new SqlCommand(SELECT_DIRETORIO, Conexão);

            try
            {
                SqlDataReader Dr = _ComandoDIRE.ExecuteReader(); Dr.Read();
                //Pega o caminho de salvamento
                //Pego primeiro o texto do TXT de salvamento do Form
                LocalSalvamento = txtCaminhoRel.Text;

                //Se ele está em branco, eu pego do banco de dados
                if (txtCaminhoRel.Text == string.Empty && Dr.HasRows)
                {
                    LocalSalvamento = Dr["CamRel_CON"].ToString();
                }
                //Se também está em branco, eu salvo no caminho Padrão
                if (LocalSalvamento == string.Empty)
                {
                    LocalSalvamento = @"C:\TechSIS BWK\Planilhas\";
                    MessageBox.Show("Atenção.: Falha ao capturar o caminho de salvamento do arquivo " + NomeDoArquivo + ". Para corrigir este aviso, acesse a Configuração Geral Do Sistema (08.04.00) e no campo 'CAMINHO A SER SALVO' selecione a pasta que você deseja que seus relatórios do Word sejam salvos. \n\nQuando não existe um caminho informado, o sistema salva automaticamente no caminho '" + LocalSalvamento + "'.", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                if (!Directory.Exists(LocalSalvamento))
                {
                    Directory.CreateDirectory(LocalSalvamento);
                }


                LocalSalvamento_Arquivo = LocalSalvamento + NomeDoArquivo;
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Busca_CaminhoSALV()\n\nBLOCO = CLASSE ImpreEXCEL_MET\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método Busca_CaminhoSALV()\n\nBLOCO = CLASSE ImpreEXCEL_MET\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }
    }
}
