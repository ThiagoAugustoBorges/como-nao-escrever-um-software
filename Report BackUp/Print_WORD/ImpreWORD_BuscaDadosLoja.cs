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
    internal class ImpreWORD_BuscaDadosLoja
    {
        public void BuscaDadosLoja(string LojaLogada)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            string _SeleçãoDados = "SELECT *,LEFT (Descri_CID, 28) AS Descri_CID,UfFede_CID FROM TabEmpre INNER JOIN TabCidad ON TabEmpre.EndCid_EMP = TabCidad.Sequen_CID WHERE Sequen_EMP = " + LojaLogada;
            SqlCommand Comando = new SqlCommand(_SeleçãoDados, Conexão);

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    SEQ_EMPRESA = Dr["Sequen_EMP"].ToString();
                    RAZAO_EMPRESA = Dr["Descri_EMP"].ToString();
                    FANTASIA = Dr["Fantas_EMP"].ToString();
                    ENDERECO = Dr["EndLog_EMP"].ToString();
                    NUM = Dr["EndNum_EMP"].ToString();
                    CIDADE = Dr["EndCid_EMP"].ToString();
                    CIDADE_DESC = Dr["Descri_CID"].ToString();
                    UF = Dr["UfFede_CID"].ToString();
                    CEP = Dr["EndCep_EMP"].ToString();
                    BAIRRO = Dr["EndBai_EMP"].ToString();
                    COMPLE = Dr["EndCom_EMP"].ToString();
                    CPFCNPJ = Dr["CpfCnp_EMP"].ToString();
                    CPFCNPJ = FormatarCpfCnpj(CPFCNPJ.ToString());
                    INSCRICAO_EST = Dr["InsEst_EMP"].ToString();
                    TELEFONE = Dr["TelLoj_EMP"].ToString();
                    FAX = Dr["FaxLoj_EMP"].ToString();
                    EMAIL = Dr["Emai01_EMP"].ToString();
                    HOME_PAGE = Dr["HomPag_EMP"].ToString();
                }
                else
                {
                    #region TRATAMENTO CASO NÃO EXISTA LOJA (MUITO IMPOSSÍVEL DE ACONTECER)
                    MessageBox.Show("ERRO AO CAPTURAR DADOS DA EMPRESA! VERIFIQUE AS FKs", "TechSIS Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SEQ_EMPRESA = "ERRO";
                    RAZAO_EMPRESA = "ERRO";
                    FANTASIA = "ERRO";
                    ENDERECO = "ERRO";
                    NUM = "ERRO";
                    CIDADE = "ERRO";
                    CIDADE_DESC = "ERRO";
                    CEP = "ERRO";
                    BAIRRO = "ERRO";
                    COMPLE = "ERRO";
                    CPFCNPJ = "ERRO";
                    CPFCNPJ = "ERRO";
                    INSCRICAO_EST = "ERRO";
                    UF = "ERRO";
                    TELEFONE = "ERRO";
                    FAX = "ERRO";
                    EMAIL = "ERRO";
                    HOME_PAGE = "ERRO";
                    #endregion
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método BuscaDadosLoja()\n\nBLOCO = CLASSE ImpreRPV_BuscaDadosLoja\n\n" + Ex.Message, "TechSIS SQL Erro.: " + Ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método BuscaDadosLoja()\n\nBLOCO = CLASSE ImpreRPV_BuscaDadosLoja\n\n" + Ex.Message, "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }




        public string SEQ_EMPRESA
        { get; set; }
        public string RAZAO_EMPRESA
        { get; set; }
        public string FANTASIA
        { get; set; }
        public string CPFCNPJ
        { get; set; }
        public string INSCRICAO_EST
        { get; set; }
        public string ENDERECO
        { get; set; }
        public string NUM
        { get; set; }
        public string CIDADE
        { get; set; }
        public string CIDADE_DESC
        { get; set; }
        public string CEP
        { get; set; }
        public string UF
        { get; set; }
        public string BAIRRO
        { get; set; }
        public string COMPLE
        { get; set; }
        public string TELEFONE
        { get; set; }
        public string FAX
        { get; set; }
        public string EMAIL
        { get; set; }
        public string HOME_PAGE
        { get; set; }


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
    }
}
