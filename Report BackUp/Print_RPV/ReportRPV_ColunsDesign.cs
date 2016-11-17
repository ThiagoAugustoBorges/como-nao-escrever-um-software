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
    internal class ReportRPV_ColunsDesign : ReportRPV_TechSIS
    {
        static string ColunsFileName = string.Empty;
        static string ColunsCompanyCode = string.Empty;

        public ReportRPV_ColunsDesign(string FileName, string CompanyCode)
            : base (FileName,CompanyCode)
        {
            ColunsFileName = FileName;
            ColunsCompanyCode = CompanyCode;
        }


        //Definição das colunas de cada relatório
        public void DefiningColumns(StreamWriter Writer_Arq)
        {
            switch (ColunsFileName)
            {
                //ESPECIFICA AS COLUNAS PARA TabProgr
                #region TabProgr
                case "TabProgr":
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=10;c=0}{480} *-CÓDIGO-*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=10;c=0}{1730} *-----------------------DESCRIÇÃO DO PROGRAMA----------------------*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=10;c=0}{9920} *--STATUS--* {\n}");
                    break;
                #endregion
                #region TabUsuar
                case "TabUsuar":
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=10;c=0}{480} *-CÓDIGO-*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=10;c=0}{1730} *-----------------DESCRIÇÃO DO USUÁRIO----------------*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=10;c=0}{8350} *-PERMISSÃO-*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=10;c=0}{9950} *-EMPRESA-* {\n}");
                    break;
                #endregion
                #region TabPermi
                case "TabPermi":
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{480} *-CÓDIGO-*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{1540} *---------DESCRIÇÃO DO USUÁRIO--------*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{5690} *-PROGRAMA-----------------*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{8700} *INC*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{9230} *ALT*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{9770} *EXC*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{10300} *CON*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{10840} *ABA* {\n}");
                    break;
                #endregion
                #region TabCidad
                case "TabCidad":
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{480} *-CÓDIGO-*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{1540} *-------------DESCRIÇÃO DA CIDADE------------*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{6390} *-UF-*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{7030} *------PAÍS------*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{8950} *--IBGE--*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{10050} *--STATUS--*{\n}");
                    break;
                #endregion
                #region TabClien
                case "TabClien":
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{500} *-----------------DESCRIÇÃO DO CLIENTE----------------*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{6270} *----CPF.CNPJ----*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{8150} *---INSCRIÇÃO---*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{9910} *--TELEFONE--*{\n}");
                    break;
                #endregion
                #region TabMsgNt
                case "TabMsgNt":
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{500} *-CÓDIGO-*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{1490} *-----------DESCRIÇÃO DA MENSAGEM-----------*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{6180} *-EMPRESA-*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{7310} *--------DESCRIÇÃO DA EMPRESA---------*{\n}");
                    break;
                #endregion
                #region TabCfope
                case "TabCfope":
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{500} *-CÓDIGO-*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{1530} *-----------------DESCRIÇÃO DO CÓDIGO FISCAL DE OPERAÇÃO-----------------*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{9300} *-C. IND-*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{10310} *-C. COM-*{\n}");
                    break;
                #endregion
                #region TabRotas
                case "TabRotas":
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=10;c=0}{500} *-CÓDIGO-*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=10;c=0}{1700} *----------------------DESCRIÇÃO DA ROTA----------------------*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=10;c=0}{9600} *--STATUS--*{\n}");
                    break;
                #endregion
                #region TabConve
                case "TabConve":
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=10;c=0}{500} *-CÓDIGO-*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=10;c=0}{1700} *-----------DESCRIÇÃO DO CONVÊNIO OU CARTÃO-----------*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=10;c=0}{8300} *--STATUS--*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=10;c=0}{10000} *-TAXA (%)-*{\n}");
                    break;
                #endregion
                #region TabSetor
                case "TabSetor":
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{500} *-CÓDIGO-*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{1520} *----------DESCRIÇÃO DO SETOR OU SUBSETOR----------*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{6950} *-----------LOCALIZAÇÃO-----------*");
                    Writer_Arq.WriteLine(@"{F=Courier New;b=Y;S=08;c=0}{10570} *-TIPO-*{\n}");
                    break;
                #endregion
            }
        }


    }
}
