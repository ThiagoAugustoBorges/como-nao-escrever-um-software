using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace ReportRPV
{
    internal class ReportRPV_AboutCompany : ReportRPV_TechSIS
    {
        static string AboutFileName;
        static string AboutCompanyCode;

        public ReportRPV_AboutCompany(string FileName, string CompanyCode)
            : base(FileName, CompanyCode)
        {
            AboutFileName = FileName;
            AboutCompanyCode = CompanyCode;
        }


        //Popula a empresa
        public void CompanyFields(StreamWriter Writer_Arq)
        {
            //Cria a conexão com o Banco de Dados e Abre!
            StreamReader StringConexão = new StreamReader(@"..\Conexão\StringConexão.xml", true);
            string LerString = StringConexão.ReadLine();
            SqlConnection Conexão = new SqlConnection(LerString);
            Conexão.Open();


            string _SeleçãoDados = "SELECT *,LEFT (Descri_CID, 28) AS Descri_CID,UfFede_CID FROM TabEmpre INNER JOIN TabCidad ON TabEmpre.EndCid_EMP = TabCidad.Sequen_CID WHERE Sequen_EMP = @Sequen_EMP";
            SqlCommand Comando = new SqlCommand(_SeleçãoDados, Conexão);
            Comando.Parameters.Add("@Sequen_EMP", SqlDbType.Int).Value = AboutCompanyCode;

            try
            {
                SqlDataReader Dr = Comando.ExecuteReader(); Dr.Read();
                if (Dr.HasRows)
                {
                    string SEQ_EMPRESA = Dr["Sequen_EMP"].ToString();
                    string RAZAO_EMPRESA = Dr["Descri_EMP"].ToString();
                    string FANTASIA = Dr["Fantas_EMP"].ToString();
                    string ENDERECO = Dr["EndLog_EMP"].ToString();
                    string NUM = Dr["EndNum_EMP"].ToString();
                    string CIDADE = Dr["EndCid_EMP"].ToString();
                    string CIDADE_DESC = Dr["Descri_CID"].ToString();
                    string UF = Dr["UfFede_CID"].ToString();
                    string CEP = Dr["EndCep_EMP"].ToString();
                    string BAIRRO = Dr["EndBai_EMP"].ToString();
                    string COMPLE = Dr["EndCom_EMP"].ToString();
                    string CPFCNPJ = FerrCnpjCpfMask(Dr["CpfCnp_EMP"].ToString());
                    string INSCRICAO_EST = Dr["InsEst_EMP"].ToString();
                    string TELEFONE = Dr["TelLoj_EMP"].ToString();
                    string FAX = Dr["FaxLoj_EMP"].ToString();
                    string EMAIL = Dr["Emai01_EMP"].ToString();
                    string HOME_PAGE = Dr["HomPag_EMP"].ToString();

                    string CIDADE_T = CIDADE.PadLeft(6, '0');

                    //Imagem do relatório
                    Writer_Arq.WriteLine(@"{pic=..\Imagens\LogoEmp.jpg,0420,420,2470,2280} ");
                    Writer_Arq.WriteLine(@"{Box=00400,0400,11050,16000}{Box=00400,0400,02500,02300}{Box=02900,0400,08550,02300} {\n}");
                    Writer_Arq.WriteLine(@"{f=Courier New;s=10;b=n}{F=Courier New}{400}RAZÃO SOCIAL.:{TAB=60}{F=Courier New}" + RAZAO_EMPRESA + @"{b=N}{F=Courier New}{s=10;b=n}{8150}DATA.Hora.:{TAB=60}" + DateTime.Now.ToString("g") + @"{b=N}");
                    Writer_Arq.WriteLine(@"{f=Courier New;s=12;b=y}{3900}{\n;\n} " + ReportRPV_Statics.NomeDoRelatorio + @"{\n;\n}{b=N}");
                    Writer_Arq.WriteLine(@"{F=Courier New;U=n;S=10}{3000}{b=Y}{c=8}EMPRESA..:{c=0}{b=N}{TAB=100}" + FANTASIA + @"{\n}");
                    Writer_Arq.WriteLine(@"{F=Courier New;U=n;S=10}{3000}{b=Y}{c=8}CNPJ/CPF.:{c=0}{b=N}{TAB=100}" + CPFCNPJ + @"{F=Courier New;U=n;S=10}{7700}{b=Y}{c=8}INSCRIÇÃO EST.:{c=0}{b=N}{TAB=100}" + INSCRICAO_EST + @"{\n}");
                    Writer_Arq.WriteLine(@"{F=Courier New;U=n;S=10}{3000}{b=Y}{c=8}ENDEREÇO.:{c=0}{b=N}{TAB=100}" + ENDERECO + @" {F=Courier New;U=n;S=10}{9600}{b=Y}{c=8}NUM.:{c=0}{b=N}{TAB=100}" + NUM + @"{\n}");
                    Writer_Arq.WriteLine(@"{F=Courier New;U=n;S=10}{3000}{b=Y}{c=8}CIDADE...:{c=0}{b=N}{TAB=100}" + CIDADE_T + @"{F=Courier New;U=n;S=10}{TAB=100}" + CIDADE_DESC + @"{9600}{b=Y}{F=Courier New;U=n;S=10}{8600}{b=Y}{c=8}UF.:{c=0}{b=N}{TAB=100}" + UF + @" {F=Courier New;U=n;S=10}{9600}{b=Y}{c=8}CEP.:{c=0}{b=N}{TAB=100}" + CEP + @"{\n}");
                    Writer_Arq.WriteLine(@"{F=Courier New;U=n;S=10}{3000}{b=Y}{c=8}BAIRRO...:{c=0}{b=N}{TAB=100}" + BAIRRO + @" {F=Courier New;U=n;S=10}{7000}{b=Y}{c=8}COMPLE.:{c=0}{b=N}{TAB=100}" + COMPLE + @"{\n}");
                    Writer_Arq.WriteLine(@"{F=Courier New;U=n;S=10}{3000}{b=Y}{c=8}TELEFONE.:{c=0}{b=N}{TAB=60}" + TELEFONE + @"{F=Courier New;U=n;S=10}{6200}{b=Y}{c=8}FAX...:{c=0}{b=N}{TAB=60}" + FAX + @"{\n}");
                    Writer_Arq.WriteLine(@"{F=Courier New;U=n;S=10}{3000}{b=Y}{c=8}EMAIL....:{c=0}{b=N}{TAB=60}{c=9}" + EMAIL + @"{\n}");
                    Writer_Arq.WriteLine(@"{F=Courier New;U=n;S=10}{3000}{b=Y}{c=8}HOME PAGE:{c=0}{b=N}{TAB=60}{c=9}" + HOME_PAGE + @"{c=0}{10000}PÁGINA.:{11000}{c=5}{PAG}{\n}{\n}");
                }
                else
                {
                    MessageBox.Show("ERRO: Os dados da empresa não foram encontrados!\n1° - Verifique a integridade das Fks\n2° - Verifique se a cidade informada na empresa é válida", "TechSIS Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (FileNotFoundException Ex)
            {
                MessageBox.Show("TechSIS Erro.: Ocorreu um erro ao executar o método CompanyFields()\n\nBLOCO = CLASSE ReportRPV_AboutCompany\n" + Ex.Message + "\n" + Ex.GetType().ToString(), "TechSIS Exception Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexão.Close();
            }
        }



        //Máscara para Cpf ou Cnpj
        #region Máscara para Cpf ou Cnpj
        public static string FerrCnpjCpfMask(string strCpfCnpj)
        {
            if (strCpfCnpj.Length <= 11)
            {
                MaskedTextProvider mtpCpf = new MaskedTextProvider(@"000\.000\.000-00");
                mtpCpf.Set(FerrCnpjCpfMaskZeros(strCpfCnpj, 11));
                return mtpCpf.ToString();
            }
            else
            {
                MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");
                mtpCnpj.Set(FerrCnpjCpfMaskZeros(strCpfCnpj, 11));
                return mtpCnpj.ToString();
            }
        }
        public static string FerrCnpjCpfMaskZeros(string strString, int intTamanho)
        {
            string strResult = "";
            for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
            {
                strResult += "0";
            }
            return strResult + strString;
        }
        #endregion

    }
}