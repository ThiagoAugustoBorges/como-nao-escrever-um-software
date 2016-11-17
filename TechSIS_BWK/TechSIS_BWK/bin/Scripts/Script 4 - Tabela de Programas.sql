/*--------------------------------------------------------*/
/*      TechSIS - SQLServer - Criacao Dos Programas       */
/*                       SCRIPT 4                         */
/*--------------------------------------------------------*/
/*--------------------------------------------------------*/
USE master
USE TechSIS_INF
/*--------------------------------------------------------*/
/*--------------------------------------------------------*/
DELETE FROM TabProgr
INSERT INTO TabProgr VALUES ('010100','TABELA DE EMPRESAS',GETDATE(),'1','1','TabEmpre','EMP')
INSERT INTO TabProgr VALUES ('010200','TABELA DE CIDADES',GETDATE(),'1','1','TabCidad','CID')
INSERT INTO TabProgr VALUES ('010300','TABELA DE CÓDIGO FISCAL DE OPERAÇÃO',GETDATE(),'1','1','TabCfope','CFO')
INSERT INTO TabProgr VALUES ('010400','TABELA DE ROTAS',GETDATE(),'1','1','TabRotas','ROT')
INSERT INTO TabProgr VALUES ('010500','TABELA DE MENSAGEM PARA NT',GETDATE(),'1','1','TabMsgNt','MSG')
INSERT INTO TabProgr VALUES ('010600','TABELA DE CONVENIOS E CARTÕES',GETDATE(),'1','1','TabConve','COV')
INSERT INTO TabProgr VALUES ('010700','TABELA DE SETORES E SUBSETORES',GETDATE(),'1','1','TabSetor','SET')

INSERT INTO TabProgr VALUES ('020100','TABELA DE CLIENTES',GETDATE(),'1','1','TabClien','CLI')
INSERT INTO TabProgr VALUES ('020200','TABELA DE FORNECEDORES',GETDATE(),'1','1','TabForne','FOR')
INSERT INTO TabProgr VALUES ('020300','TABELA DE TRANSPORTADORES',GETDATE(),'1','1','TabTrans','TRA')

INSERT INTO TabProgr VALUES ('020401','TABELA DE PRODUTOS PARA REVENDA',GETDATE(),'1','1','TabPro01','PR1')
INSERT INTO TabProgr VALUES ('020402','TABELA DE PRODUTOS DE USO E CONSUMO',GETDATE(),'1','3','TabPro02','PR2')
INSERT INTO TabProgr VALUES ('020403','TABELA DE PRODUTOS DE MATERIA PRIMA',GETDATE(),'1','4','TabPro03','PR3')

INSERT INTO TabProgr VALUES ('020501','TABELA DE SERVIÇOS - SEUS SERVIÇOS',GETDATE(),'1','1','TabSer01','SE1')
INSERT INTO TabProgr VALUES ('020502','TABELA DE SERVIÇOS - SERVIÇOS DE TERCEIROS',GETDATE(),'1','1','TabSer01','SE2')

INSERT INTO TabProgr VALUES ('020601','TABELA DE MOTORISTAS',GETDATE(),'1','1','TabMotor','MOT')
INSERT INTO TabProgr VALUES ('020602','TABELA DE OPERADORES',GETDATE(),'1','1','TabOpera','OPE')
INSERT INTO TabProgr VALUES ('020603','CADASTRO DE MAQUINAS',GETDATE(),'1','1','TabMaqui','MAQ')
INSERT INTO TabProgr VALUES ('020604','CADASTRO DE VEICULOS',GETDATE(),'1','1','TabVeicu','VEI')

INSERT INTO TabProgr VALUES ('090100','TABELA DE USUÁRIOS',GETDATE(),'1','1','TabUsuar','USU')
INSERT INTO TabProgr VALUES ('090200','TABELA DE PROGRAMAS',GETDATE(),'1','1','TabProgr','PGR')
INSERT INTO TabProgr VALUES ('090300','TABELA DE CONFI. DE PERMISSÃO',GETDATE(),'1','1','TabPermi','PER')
INSERT INTO TabProgr VALUES ('090400','CONFIGURAÇÃO GERAL DO SOFTWARE',GETDATE(),'1','1','TabConfi','CON')
/*--------------------------------------------------------*/
USE master

/*--------------------------------------------------------*/