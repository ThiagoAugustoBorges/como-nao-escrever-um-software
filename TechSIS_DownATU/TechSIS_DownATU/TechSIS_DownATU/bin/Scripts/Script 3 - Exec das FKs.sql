/*--------------------------------------------------------*/
/*        TechSIS - SQLServer - Criacao Das FKs           */
/*                       SCRIPT 3                         */
/*--------------------------------------------------------*/
/*--------------------------------------------------------*/
USE master
USE TechSIS_INF
/*--------------------------------------------------------*/
/*--------------------------------------------------------*/
--REFERENCIA O USUÁRIO NO HISTÓRICO
ALTER  TABLE  TabHisto  ADD
       CONSTRAINT       FK_TabHisto_TabUsuar FOREIGN KEY (Usuari_HIS)
                        REFERENCES  TabUsuar (Sequen_USU)
                        ON  DELETE  NO ACTION

--REFERENCIA A EMPRESA NO USUÁRIO
ALTER  TABLE  TabUsuar  ADD
       CONSTRAINT       FK_TabUsuar_TabEmpre FOREIGN KEY (CodLoj_USU)
                        REFERENCES  TabEmpre (Sequen_EMP)
                        ON  DELETE  CASCADE

--REFERENCIA A EMPRESA NA CONFIGURAÇÃO GERAL						 
 ALTER  TABLE TabConfi  ADD
       CONSTRAINT       FK_TabConfi_TabEmpre FOREIGN KEY (SeqLoj_CON)
                        REFERENCES  TabEmpre (Sequen_EMP)
                        ON  DELETE  NO ACTION

--DELETA TODA A USU01 CASO O USUÁRIO SEJA EXCLUIDO
ALTER  TABLE  TabUsu01  ADD
       CONSTRAINT       FK_TabUsu01_TabUsuar FOREIGN KEY (SeqUsu_US1)
                        REFERENCES  TabUsuar (Sequen_USU)
                        ON  DELETE  CASCADE
--REFERENCIA A EMPRESA NA TABELA TabUso01					 
ALTER  TABLE TabUsu01  ADD
       CONSTRAINT       FK_TabUsu01_TabEmpre FOREIGN KEY (SeqLoj_US1)
                        REFERENCES  TabEmpre (Sequen_EMP)
                        ON  DELETE  NO ACTION

--DELETA TODA A USU02 CASO O USUÁRIO SEJA EXCLUIDO
ALTER  TABLE  TabUsu02  ADD
       CONSTRAINT       FK_TabUsu02_TabUsuar FOREIGN KEY (SeqUsu_US2)
                        REFERENCES  TabUsuar (Sequen_USU)
                        ON  DELETE  CASCADE

--REFERENCIA A CIDADE NA EMPRESA
ALTER  TABLE  TabEmpre  ADD
       CONSTRAINT       FK_TabEmpre_TabCidad FOREIGN KEY (EndCid_EMP)
                        REFERENCES  TabCidad (Sequen_CID)
                        ON  DELETE  NO ACTION

--REFERENCIA O CÓDIGO DO MUNICIPIO NA CIDADE
ALTER  TABLE  TabCidad  ADD
       CONSTRAINT       FK_TabCidad_TabCidMu FOREIGN KEY (IbgeMu_CID)
                        REFERENCES  TabCidMu (Codigo_MUN)
                        ON  DELETE  NO ACTION

--REFERENCIA O PAIS NA CIDADE
ALTER  TABLE  TabCidad  ADD
       CONSTRAINT       FK_TabCidad_TabPaise FOREIGN KEY (PaisCi_CID)
                        REFERENCES  TabPaise (Codigo_PAI)
                        ON  DELETE  NO ACTION

--REFERENCIA A CIDADE NO CLIENTE
ALTER  TABLE  TabClien  ADD
       CONSTRAINT       FK_TabClien_TabCidad FOREIGN KEY (EndCi1_CLI)
                        REFERENCES  TabCidad (Sequen_CID)
                        ON  DELETE  NO ACTION

--REFERENCIA A EMPRESA NO CLIENTE
ALTER  TABLE  TabClien  ADD
       CONSTRAINT       FK_TabClien_TabEmpre FOREIGN KEY (EmpSeq_CLI)
                        REFERENCES  TabEmpre (Sequen_EMP)
                        ON  DELETE  NO ACTION

--REFERENCIA A EMPRESA NA MENSAGEM DE NOTAS
ALTER  TABLE  TabMsgNt  ADD
       CONSTRAINT       FK_TabMsgNt_TabEmpre FOREIGN KEY (SeqEmp_MSG)
                        REFERENCES  TabEmpre (Sequen_EMP)
                        ON  DELETE  NO ACTION

--DELETA TODAS AS ROT01, CASO A ROTA SEJA DELETADA
ALTER  TABLE  TabRot01  ADD
       CONSTRAINT       FK_TabRot01_TabRotas FOREIGN KEY (Sequen_RO1)
                        REFERENCES  TabRotas (Sequen_ROT)
                        ON  DELETE  CASCADE

--DELETA TODAS AS ROT01, CASO O CLIENTE DA ROTA SEJA DELETADA
ALTER  TABLE  TabRot01  ADD
       CONSTRAINT       FK_TabRot01_TabClien FOREIGN KEY (SeqCli_RO1)
                        REFERENCES  TabClien (Sequen_CLI)
                        ON  DELETE  CASCADE
/*--------------------------------------------------------*/


/*--------------------------------------------------------*/
GO
use master
