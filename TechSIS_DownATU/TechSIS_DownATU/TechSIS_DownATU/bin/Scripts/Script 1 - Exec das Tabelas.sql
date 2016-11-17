/*--------------------------------------------------------*/
/*    TechSIS - SQLServer - Criacao Banco e Tabelas       */
/*                       SCRIPT 1                         */
/*--------------------------------------------------------*/
USE  MASTER
DROP   DATABASE  TechSIS_INF
CREATE DATABASE  TechSIS_INF
USE TechSIS_INF

/*--------------------------------------------------------*/
/*           TabEmpre - Cadastro De Empresas              */
/*--------------------------------------------------------*/
CREATE TABLE TabEmpre
(
	Sequen_EMP		NUMERIC (006)   NOT NULL, -- Id da empresa
	Descri_EMP		VARCHAR (070)   NOT NULL, -- Descrição da empresa
	Fantas_EMP		VARCHAR	(070)	NOT NULL, -- Fantasia da empresa
	EndLog_EMP		VARCHAR	(060)		NULL, -- Endereço
	EndNum_EMP		VARCHAR (015)		NULL, -- Número
	EndCid_EMP		NUMERIC (006)	    NULL, -- Cidade da empresa
	EndCep_EMP		VARCHAR (015)		NULL, -- Cep da empresa
	EndBai_EMP		VARCHAR (040)		NULL, -- Bairro empresa
	EndCom_EMP		VARCHAR (040)		NULL, -- Complemento do endereço
	Tipo01_EMP		NUMERIC (001)	NOT NULL, -- Tipo 0 = PF 1 = PJ
	CpfCnp_EMP		VARCHAR (020)	NOT NULL, -- CPF ou CNPJ
	InsEst_EMP		VARCHAR (020)		NULL, -- Inscrição Estadual
	InsMun_EMP      VARCHAR (020)		NULL, -- Inscrição Municipal
	RegTri_EMP		NUMERIC (001)	NOT NULL, -- Regime tributario - 0 = Simples, 1 = ExS, 2 = Regime Normal
	Respon_EMP		VARCHAR (050)		NULL, -- Responsavel pela empresa
	TelLoj_EMP		VARCHAR (020)		NULL, -- Telefone da empresa
	FaxLoj_EMP		VARCHAR (020)		NULL, -- Fax da empresa
	Emai01_EMP		VARCHAR (050)		NULL, -- Email da empresa
	HomPag_EMP		VARCHAR (050)		NULL, -- Home page
	NumCai_EMP		NUMERIC (002)	NOT NULL, -- Número de caixas
	CfoSai_EMP		NUMERIC (005)		NULL, -- CFOP Saida
	Series_EMP		VARCHAR (005)		NULL, -- Serie da nota fiscal
	Rodape_EMP		NUMERIC (006)		NULL, -- Msg do rodapé na nota
	DtVeEs_EMP		DATE			NOT NULL, -- Data do vencimento inscrição estadual
	DtVeMu_EMP      DATE			NOT NULL, -- Data do vencimento inscrição municipal
	Ativid_EMP		NUMERIC (006)		NULL, -- Código atividade da empresa
	IcmdRz_EMP		NUMERIC (002)		NULL, -- Tipo de ICMS Reduzido
	IcmsSt_EMP		NUMERIC (002)		NULL, -- Tipo de ICMS ST
	TipFat_EMP		NUMERIC (002)		NULL, -- Tipo de faturamento
	SitEmp_EMP	    NUMERIC (002)		NULL, -- Situação da empresa
	EspeNf_EMP		VARCHAR (010)		NULL, -- Descrição da especie da NF
	AprCre_EMP		DECIMAL (14,4)		NULL, -- Valor em % do aproveitamento de crédito
	ObsLiv_EMP		VARCHAR (050)		NULL, -- Obs para o livro de saida
	LotePr_EMP		NUMERIC (001)		NULL, -- Produtos por lote?
	FretTp_EMP		NUMERIC (002)		NULL, -- Tipo de frete
	CondTp_EMP		NUMERIC (002)       NULL, -- Condição do pagamento
	ReceTp_EMP		NUMERIC (002)		NULL, -- Condição do recebimento
	ConCod_EMP		NUMERIC (002)		NULL, -- Tipo da consultada do produto
	TpVend_EMP		NUMERIC (002)		NULL, -- Tipo de venda solicitada
	MovEst_EMP		NUMERIC (001)		NULL, -- Movimenta estoque?
	MovFin_EMP		NUMERIC (001)		NULL, -- Movimenta financeiro?
	TpJuro_EMP		NUMERIC (001)		NULL, -- Tipo de Juros
	VaJuro_EMP		DECIMAL (14,4)		NULL, -- Valor do Juros em % 45
	Modelo_EMP		VARCHAR (010)		NULL, -- Modelo da Nf
	
	EstNot_EMP		VARCHAR (005)		NULL, -- Aceita estoque negativo em Notas
	EstPed_EMP		VARCHAR (005)		NULL, -- Aceita estoque negativo em Pedidos
	PeVeOb_EMP		VARCHAR (005)		NULL, -- Pedido de venda obrigatorio na nota
	PeCoOb_EMP		VARCHAR (005)		NULL, -- Pedido de compra obrigatorio na nota
	SenLim_EMP		VARCHAR (005)		NULL, -- Senha para limite vencido
	FinFix_EMP		VARCHAR (005)		NULL, -- Vencimento Fixo das Parcelas
	SenDes_EMP		VARCHAR (005)		NULL, -- Senha para desconto maximo
	BloIns_EMP		VARCHAR (005)		NULL, -- Bloqueia venda com insc vencida
	VenObr_EMP		VARCHAR (005)		NULL, -- Vendedor obrigatorio na venda
	EmiNot_EMP		VARCHAR (005)		NULL, -- Emite nota logo apos pedido?
	AutPev_EMP		VARCHAR (005)		NULL, -- Autorização em pedidos de venda
	AutPec_EMP		VARCHAR (005)		NULL, -- Autorização em pedidos de compra?
	AutBai_EMP		VARCHAR (005)		NULL, -- Autorização para efetuar baixa
	PedDat_EMP		VARCHAR (005)		NULL, -- Data do pedido automatica
	ExcOrc_EMP		VARCHAR (005)		NULL, -- Exclui orçamento
	OrcSem_EMP		NUMERIC (002)		NULL, -- Semanas para excluir
	ExcHis_EMP		VARCHAR (005)		NULL, -- Exclui historico
	OrcHis_EMP		NUMERIC (002)		NULL, -- Meses para excluir
	DesAnt_EMP		VARCHAR (005)		NULL, -- Desconto para pagamento adiantado
	ValDes_EMP		DECIMAL (14,4)		NULL, -- Valor do desconto em %
	TabPre_EMP		VARCHAR (005)		NULL, -- Tabelas de preço para a vista e a prazo
	TabPUm_EMP		NUMERIC (002)		NULL, -- Tabela A VISTA
	TabPDo_EMP		NUMERIC (002)		NULL, -- Tabela A PRAZO
	VrLiCr_EMP		DECIMAL (14,4)		NULL, -- Valor padrão do limite de crédito
	DtLiCr_EMP		NUMERIC (002)		NULL, -- Número de meses da latencia do limite
	DatCad_EMP		DATETIME		NOT NULL, -- Data do cadastro da empresa
	CONSTRAINT Sequen_EMP_PK PRIMARY KEY (Sequen_EMP)
)
             
/*--------------------------------------------------------*/
/*           TabProgr - Cadastro De Programas             */
/*--------------------------------------------------------*/
CREATE TABLE TabProgr
(
	Sequen_PGR		VARCHAR (006)	NOT NULL, -- Identificação do programa
	Descri_PGR		VARCHAR	(070)	NOT NULL, -- Descrição do programa
	DtCada_PGR		DATE				NULL, -- Data do cadastramento
	Status_PGR		NUMERIC (002)	NOT NULL, -- 1 - Ativo, 2 - Inativo, 3 - Excluido
	Modulo_PGR		NUMERIC (002)	NOT NULL, -- Modulo que o programa pertence
	Tabela_PGR		VARCHAR (010)	NOT NULL, -- Tabela envolvida no programa
	Prefix_PGR		VARCHAR (003)	NOT NULL  -- Prefixo envolvido na tabela
	CONSTRAINT Sequen_PGR_PK PRIMARY KEY (Sequen_PGR)		
)
/*--------------------------------------------------------*/
/*             TabClien - Tabela de Clientes              */
/*--------------------------------------------------------*/
CREATE TABLE TabClien
(
	Sequen_CLI		NUMERIC (006)   NOT NULL, -- Identificação do programa
	Descri_CLI		VARCHAR (080)   NOT NULL, -- Descrição do Cliente
	SeqPri_CLI		NUMERIC (006)   NOT NULL, -- Cliente principal
	Tipo01_CLI		NUMERIC (001)	NOT NULL, -- Tipo 1 = PF 2 = PJ
	CpfCnp_CLI		VARCHAR (020)	NOT NULL, -- CPF ou CNPJ
	Fantas_CLI		VARCHAR (070)		NULL, -- Fantasia do cliente
	TelPab_CLI		VARCHAR (020)		NULL, -- Telefone pabx do cliente
	TelFax_CLI		VARCHAR (020)		NULL, -- Telefone fax do cliente
	TelCe1_CLI		VARCHAR (020)		NULL, -- Telefone cel 1 do cliente
	TelCe2_CLI		VARCHAR (020)		NULL, -- Telefone cel 2 do cliente
	Catego_CLI		NUMERIC (002)	NOT	NULL, -- Categoria do cliente
	Observ_CLI		VARCHAR (070)		NULL, -- Observação para esse cliente
	EndLo1_CLI		VARCHAR	(060)		NULL, -- Endereço 1
	EndNu1_CLI		VARCHAR (015)		NULL, -- Número
	EndCi1_CLI		NUMERIC (006)	NOT	NULL, -- Cidade da empresa
	EndCe1_CLI		VARCHAR (015)		NULL, -- Cep da empresa
	EndBa1_CLI		VARCHAR (040)		NULL, -- Bairro empresa
	EndCo1_CLI		VARCHAR (040)		NULL, -- Complemento do endereço
    EndTi2_CLI		VARCHAR (030)		NULL, -- Descrição do tipo de endereço VARIAVEL
	EndLo2_CLI		VARCHAR	(060)		NULL, -- Endereço VARIAVEL
	EndNu2_CLI		VARCHAR (015)		NULL, -- Número
	EndCi2_CLI		NUMERIC (006)	NOT	NULL, -- Cidade da empresa
	EndCe2_CLI		VARCHAR (015)		NULL, -- Cep da empresa
	EndBa2_CLI		VARCHAR (040)		NULL, -- Bairro empresa
	EndCo2_CLI		VARCHAR (040)		NULL, -- Complemento do endereço
	TraSeq_CLI		NUMERIC (006)	NOT NULL, -- Sequencia do transportador
    ConSeq_CLI		NUMERIC (006)	NOT NULL, -- Sequencia do convenio
	InsEst_CLI		VARCHAR (020)		NULL, -- Inscrição Estadual
	InsMun_CLI      VARCHAR (020)		NULL, -- Inscrição Municipal
	DtVeEs_CLI		DATE			NOT NULL, -- Data do vencimento inscrição estadual
	DtVeMu_CLI      DATE			NOT NULL, -- Data do vencimento inscrição municipal
	RotSeq_CLI		NUMERIC (006)	NOT	NULL, -- Rota do Cliente
	Refere_CLI		NUMERIC (006)	NOT	NULL, -- Codigo do clientes que foi referencia
	VenSeq_CLI		NUMERIC (006)	NOT	NULL, -- Sequencia do Vendedor
	EmpSeq_CLI		NUMERIC (006)	NOT	NULL, -- Empresa do cadastramento do cliente
	Descon_CLI		DECIMAL (14,4)	NOT	NULL, -- Desconto preferencial para o cliente
	BancCl_CLI		NUMERIC (010)	NOT	NULL, -- Código do banco do cliente
	AgenCl_CLI		NUMERIC (015)	NOT	NULL, -- Agencia do banco
	Identi_CLI		VARCHAR (025)		NULL, -- Identidade do Cliente
	Concet_CLI		VARCHAR (002)		NULL, -- Conceito do Cliente
	ValLim_CLI		DECIMAL (14,4)	NOT	NULL, -- Valor do limite de credito
    VenLim_CLI		DATE			NOT	NULL, -- Vencimento do limite
	SitCre_CLI		NUMERIC (002)	NOT	NULL, -- Situação do crédito 0 = Liberado 1 = Bloqueado
	ComFre_CLI		NUMERIC (002)	NOT	NULL, -- Tipo de frete para o cliente
	ComRec_CLI		NUMERIC (002)	NOT	NULL, -- Tipo de recebimento do cliente
	ComCom_CLI		NUMERIC (002)	NOT	NULL, -- Tipo de condição do cliente
	ComTip_CLI		NUMERIC (002)	NOT	NULL, -- Tipo de Venda do cliente
	Contra_CLI		NUMERIC (002)	NOT	NULL, -- Contrato na empresa? 0=SIM 1=SIM 2=NAO
	DtInic_CLI		DATE			NOT NULL, -- Data do inicio do contrato
	DtFina_CLI      DATE			NOT NULL, -- Data do final do contrato
	Emai01_CLI		VARCHAR (050)		NULL, -- Email 1
	Emai02_CLI		VARCHAR (050)		NULL, -- Email 2
	MsnCon_CLI		VARCHAR (050)		NULL, -- Msn
	Skype1_CLI		VARCHAR (050)		NULL, -- Skype
	HomPag_CLI		VARCHAR (050)		NULL, -- Home page
	Facebo_CLI		VARCHAR (050)		NULL, -- Facebook
	SexoCl_CLI		NUMERIC (001)		NULL, -- Sexo do Cliente 1=M 2=F
	LocaTr_CLI		VARCHAR (050)		NULL, -- Local de trabalho do Cliente
	Natura_CLI		VARCHAR (050)		NULL, -- Natural de
	CargCl_CLI		VARCHAR (050)		NULL, -- Cargo do Cliente
	FilPai_CLI		VARCHAR (060)		NULL, -- Filiação Pai
	FilMae_CLI		VARCHAR (060)		NULL, -- Filiação Mae
	Conjug_CLI		VARCHAR (060)		NULL, -- Conjuge Cliente
    CpfCon_CLI		VARCHAR (020)	NOT NULL, -- CPF Conjuje
    DtAdmi_CLI      DATE			NOT NULL, -- Data de admissão do cliente
    DtAniv_CLI      DATE			NOT NULL, -- Data de aniversario do cliente
    Conta1_CLI		VARCHAR (060)		NULL, -- Contato proprietario 1
    Conta2_CLI		VARCHAR (060)		NULL, -- Contato proprietario 2
    EscCon_CLI		VARCHAR (060)		NULL, -- Escritorio de contabilidade do cliente
    DiaFat_CLI		NUMERIC (002)	NOT	NULL, -- Dia de faturamento
	AtivEm_CLI		NUMERIC (002)	NOT	NULL, -- Atividade do cliente-empresa
	RegiEm_CLI		NUMERIC (002)	NOT	NULL, -- Código do regime tributario do cliente-empresa
	CnaeEm_CLI		VARCHAR (020)		NULL, -- Número do CNAE do cliente-empresa
	IcmsBc_CLI		NUMERIC (002)	NOT	NULL, -- Icms bc reduzida do cliente
	IcmsSu_CLI		NUMERIC (002)	NOT	NULL, -- Icms Subs do cliente
	CfopCl_CLI		NUMERIC (006)	NOT NULL, -- Codigo do CFOP do cliente
	MsgNo1_CLI		NUMERIC (006)	NOT	NULL, -- Mensagem 1 da nota fiscal
	MsgNo2_CLI		NUMERIC (006)	NOT	NULL, -- Mensagem 2 da nota fiscal
    UsuSeq_CLI		NUMERIC (006)	NOT NULL, -- Sequencia do usuario que fez o cadastramento
    DtCada_CLI		DATETIME		NOT	NULL, -- Data de cadastro do cliente
    DtRevi_CLI		DATETIME		NOT	NULL, -- Data de revisão do cadastro
	Status_CLI		NUMERIC (002)   NOT NULL, -- 1 - Ativo, 2 - Inativo, 3 - Excluido, 4 - Bloqueado
	CONSTRAINT Sequen_CLI_PK PRIMARY KEY (Sequen_CLI)		
)
/*--------------------------------------------------------*/
/*           TabUsuar - Cadastro De Usuários              */
/*--------------------------------------------------------*/
CREATE TABLE TabUsuar
(
	Sequen_USU		NUMERIC (006)  NOT NULL, -- ID do usuário
	Descri_USU		VARCHAR (060)  NOT NULL, -- Descrição do usuário
	Apelid_USU		VARCHAR (060)  NOT NULL, -- Nome reduzido para apresentaçao
	CodLoj_USU		NUMERIC (006)	   NULL, -- Código da Loja
	Tipo01_USU      NUMERIC (003)  NOT NULL, -- Tipo do usuario
	Emai01_USU      VARCHAR (070)      NULL, -- Email do usuário
	Mesg01_USU      VARCHAR (070)      NULL, -- MSN do usuário
	Senhas_USU		VARCHAR (100)  NOT NULL, -- Senha do usuário
	Status_USU		NUMERIC (002)  NOT NULL, -- 1 - Ativo, 2 - Inativo, 3 - Excluido
	Emai02_USU      VARCHAR (070)	   NULL, -- Email de contato do usuário
	Skype1_USU		VARCHAR (070)	   NULL, -- Skype do usuário
	DtCada_USU		DATETIME	       NULL  -- Data do cadastramento
	CONSTRAINT Sequen_USU_PK PRIMARY KEY (Sequen_USU)	
)
--CAIXAS POR USUÁRIOS
CREATE TABLE TabUsu01
(
	SeqUsu_US1		NUMERIC (006)  NOT NULL, -- Seq do Usuário
	SeqLoj_US1		NUMERIC (006)  NOT NULL, -- ID da Loja
	SeqCai_US1		NUMERIC (003)  NOT NULL, -- ID do Caixa
	PerCre_US1		VARCHAR (006)      NULL  -- Liberação de crédito?
	CONSTRAINT SeqUsu_Loj_Cai_US1_PK PRIMARY KEY (SeqUsu_US1,SeqLoj_US1,SeqCai_US1)	
)
--ATALHOS POR USUÁRIOS
CREATE TABLE TabUsu02
(
	SeqUsu_US2		NUMERIC (006)  NOT NULL, -- Seq do Usuário
	AtaNu1_US2		VARCHAR (014)      NULL, -- Atalho person. 1
	AtaNu2_US2		VARCHAR (014)      NULL, -- Atalho person. 1
	AtaPed_US2		VARCHAR (014)      NULL, -- Atalho do Pedido
	AtaNot_US2		VARCHAR (014)      NULL, -- Atalho da Nota Fiscal
	AtaPro_US2		VARCHAR (014)	   NULL  -- Atalho do Produto
	CONSTRAINT SeqUsu_US2_PK PRIMARY KEY (SeqUsu_US2)	
)
/*--------------------------------------------------------*/
/*        TabHisto - Histórico de Processamento           */
/*--------------------------------------------------------*/
CREATE TABLE TabHisto
(
	Sequen_HIS		NUMERIC (010)	NOT NULL, -- Identificação do programa
	Prog01_HIS		VARCHAR	(070)	NOT NULL, -- Caminho da opção
	TipLan_HIS		VARCHAR	(030)	    NULL, -- Tipo de lançamento (Inc,Alt,Exc,Rest)
	ObsLa1_HIS		VARCHAR (200)		NULL, -- Obs lançamento 1
	ObsLa2_HIS		VARCHAR (200)		NULL, -- Obs lançamento 2
	Usuari_HIS		NUMERIC (006)	NOT NULL, -- Usuário que efetuou a transação
	DtLanc_HIS		DATETIME	    NOT NULL, -- Data que o Log foi alterado
	CONSTRAINT Sequen_HIS_PK PRIMARY KEY (Sequen_HIS)		
)
/*--------------------------------------------------------*/
/*             TabCidad - Tabela de Cidades               */
/*--------------------------------------------------------*/
CREATE TABLE TabCidad
(
	Sequen_CID		NUMERIC (006)	NOT NULL, -- Identificação da Cidade
	Descri_CID		VARCHAR	(100)	NOT NULL, -- Descrição da Cidade
	UfFede_CID		VARCHAR	(003)	    NULL, -- Sigla da UF da Cidade
	UfInde_CID		NUMERIC (003)		NULL, -- Index da UF (Programação)
	UfDesc_CID		VARCHAR (060)		NULL, -- Descrição da UF
	PaisCi_CID		NUMERIC (010)	    NULL, -- Código do Pais
	IbgeCi_CID		NUMERIC (003)	    NULL, -- Código IBGE da UF
	IbgeMu_CID		NUMERIC (010)	    NULL, -- Código IBGE da municipio
	IbgeEs_CID		NUMERIC (015)	    NULL, -- Código IBGE do estado
	CepCi1_CID		NUMERIC (006)		NULL, -- Intervalo de CEP 1
	CepCi2_CID		NUMERIC (006)		NULL, -- Intervalo de CEP 2
	Status_CID		NUMERIC (002)   NOT NULL, -- 1 - Ativo, 2 - Inativo, 3 - Excluido
	CONSTRAINT Sequen_CID_PK PRIMARY KEY (Sequen_CID)		
)
/*--------------------------------------------------------*/
/*            TabPermi - Tabela de Permissão              */
/*--------------------------------------------------------*/
CREATE TABLE TabPermi
(
	SeqUsu_PER		NUMERIC (006)	NOT NULL,	-- Código do usuário
	SeqPgr_PER		VARCHAR (006)	NOT NULL,	-- Código do programa
	PerINC_PER		VARCHAR (005)		NULL,	-- Permissão para INCLUIR?   (True-False)
	PerALT_PER		VARCHAR (005)		NULL,	-- Permissão para ALTERAR?   (True-False)
	PerEXC_PER		VARCHAR (005)		NULL,	-- Permissão para EXCLUIR?   (True-False)
	PerCON_PER		VARCHAR (005)		NULL,	-- Permissão para CONSULTAR? (True-False)
	PerABA_PER		VARCHAR (005)		NULL, 	-- Bloquear Abas?            (True-False)
	PerAb1_PER		NUMERIC (001)		NULL,	-- Número da Aba a ser bloqueada
    PerAb2_PER		NUMERIC (001)		NULL,	-- Número da Aba a ser bloqueada
	PerAb3_PER		NUMERIC (001)		NULL,	-- Número da Aba a ser bloqueada
	PerAb4_PER		NUMERIC (001)		NULL	-- Número da Aba a ser bloqueada
	CONSTRAINT SeqUsu_SeqPgr_PK	PRIMARY KEY (SeqUsu_PER,SeqPgr_PER)	
)
/*--------------------------------------------------------*/
/*         TabNcmSo - Tabela de NCMs do Software          */
/*--------------------------------------------------------*/
CREATE TABLE TabNcmSo
(
	Sequen_NCM		NUMERIC (008)	NOT NULL,	-- Código da sequencia do NCM
	Codigo_NCM		NUMERIC (010)	NOT NULL,	-- Código do NCM (8 Digitos)
	Descri_NCM		VARCHAR (100)	NOT NULL,	-- Descrição do NCM
	CONSTRAINT Sequen_NCM_PK	PRIMARY KEY (Sequen_NCM)	
)
/*--------------------------------------------------------*/
/*              TabPaise - Tabela de Paises               */
/*--------------------------------------------------------*/
CREATE TABLE TabPaise
(
	Sequen_PAI		NUMERIC (008)	NOT NULL,	-- Código da sequencia do Pais
	Codigo_PAI		NUMERIC (010)	NOT NULL,	-- Código do Pais
	Descri_PAI		VARCHAR (100)	NOT NULL,	-- Descrição do Pais
	CONSTRAINT Sequen_PAI_PK	PRIMARY KEY (Codigo_PAI)	
)
/*--------------------------------------------------------*/
/*        TabSetor - Tabela de Setores na Empresa         */
/*--------------------------------------------------------*/
CREATE TABLE TabSetor
(
	Sequen_SET		NUMERIC (008)	NOT NULL,	-- Código do Setor
	Descri_SET		VARCHAR (070)	NOT NULL,	-- Descrição do Setor
	Respon_SET		VARCHAR (080)		NULL,	-- Responsável pelo Setor
	Locali_SET		VARCHAR (100)		NULL,	-- Localização do Setor
	Status_SET		NUMERIC (002)   NOT NULL    -- 1 - Ativo, 2 - Inativo, 3 - Excluido
	CONSTRAINT Sequen_SET_PK	PRIMARY KEY (Sequen_SET)	
)
/*--------------------------------------------------------*/
/*       TabCidMu - Tabela de Códigos de Municípios       */
/*--------------------------------------------------------*/
CREATE TABLE TabCidMu
(
	Sequen_MUN		NUMERIC (008)	NOT NULL,	-- Código da sequencia do Municipio
	Codigo_MUN		NUMERIC (010)	NOT NULL,	-- Código do Municipio
	Descri_MUN		VARCHAR (100)	NOT NULL,	-- Descrição do Municipio
	CoFede_MUN		NUMERIC (004)	NOT NULL,	-- Codigo da UF do Municipio
	UfFede_MUN		VARCHAR	(003)	NOT NULL,   -- Sigla da UF do Municipio
	CONSTRAINT Sequen_MUN_PK	PRIMARY KEY (Codigo_MUN)	
)
/*--------------------------------------------------------*/
/*          TabTrans - Tabela de Transportadoras          */
/*--------------------------------------------------------*/
CREATE TABLE TabTrans
(
	Sequen_TRA		NUMERIC (006)	NOT NULL,	-- Código da sequencia da Transportador
	Descri_TRA		VARCHAR (100)	NOT NULL,	-- Descrição da Transportadora
	SeqEmp_TRA		NUMERIC (006)	NOT NULL,	-- Empresa da transportadora
	Fantas_TRA		VARCHAR	(070)		NULL,	-- Fantasia da Trans.
	Tipo01_TRA		NUMERIC (001)	NOT NULL,	-- Tipo 1 = PF 2 = PJ
	CpfCnp_TRA		VARCHAR (020)	    NULL,	-- CPF ou CNPJ
	Placas_TRA		VARCHAR (008)		NULL,	-- Placa do transportador
	Veicul_TRA		NUMERIC (001)	NOT NULL,	-- Veiculo da transportadora
	EndLo1_TRA		VARCHAR	(060)		NULL,	-- Endereço 1
	EndNu1_TRA		VARCHAR (015)		NULL,	-- Número 1
	EndCi1_TRA		NUMERIC (006)	NOT	NULL,	-- Cidad 1
	EndCe1_TRA		VARCHAR (015)		NULL,	-- Cep 1
	EndBa1_TRA		VARCHAR (040)		NULL,	-- Bairro 1
	EndCo1_TRA		VARCHAR (040)		NULL,	-- Complemento 1
	Status_TRA		NUMERIC (002)   NOT NULL,   -- 1 - Ativo, 2 - Inativo, 3 - Excluido
	CONSTRAINT Sequen_TRA_PK	PRIMARY KEY (Sequen_TRA)	
)
/*--------------------------------------------------------*/
/*             TabVende - Tabela de Vendedores            */
/*--------------------------------------------------------*/
CREATE TABLE TabVende
(
	Sequen_VEN		NUMERIC (006)	NOT NULL,	-- Código da sequencia do vendedor
	Descri_VEN		VARCHAR (100)	NOT NULL,	-- Descrição do vendedor
	SeqEmp_VEN		NUMERIC (006)	NOT NULL,	-- Empresa do vendedor
	Fantas_VEN		VARCHAR	(070)		NULL,	-- Fantasia do vendedor
	Tipo01_VEN		NUMERIC (001)	NOT NULL,	-- Tipo 1 = PF 2 = PJ
	CpfCnp_VEN		VARCHAR (020)	    NULL,	-- CPF ou CNPJ
	Veicul_VEN		VARCHAR (070)	    NULL,	-- Veiculo do vendedor
	Placas_VEN		VARCHAR (008)		NULL,	-- Placa do vendedor
	EndLo1_VEN		VARCHAR	(060)		NULL,	-- Endereço 1
	EndNu1_VEN		VARCHAR (015)		NULL,	-- Número 1
	EndCi1_VEN		NUMERIC (006)	NOT	NULL,	-- Cidad 1
	EndCe1_VEN		VARCHAR (015)		NULL,	-- Cep 1
	EndBa1_VEN		VARCHAR (040)		NULL,	-- Bairro 1
	EndCo1_VEN		VARCHAR (040)		NULL,	-- Complemento 1
	Contra_VEN		NUMERIC (001)	NOT NULL,	-- Tipo 1 = COM CONTRATO 2 = SEM CONTRATO
	Comiss_VEN		NUMERIC (001)	NOT NULL,	-- Tipo 1 = Faturamento 2 = Liquidação
	Status_VEN		NUMERIC (002)   NOT NULL,   -- 1 - Ativo, 2 - Inativo, 3 - Excluido
	CONSTRAINT Sequen_VEN_PK	PRIMARY KEY (Sequen_VEN)	
)
/*--------------------------------------------------------*/
/*            TabConve - Tabela de Convenios              */
/*--------------------------------------------------------*/
CREATE TABLE TabConve
(
	Sequen_COV		NUMERIC (006)	NOT NULL,	-- Código do Convenio
	Descri_COV		VARCHAR (100)	NOT NULL,	-- Descrição do Convenio
	Tipo01_COV		NUMERIC (001)	NOT NULL,	-- 1 - Convenio, 2 - Cartão Cre, 3 - Cartão Deb
	Taxa01_COV		DECIMAL (14,4)	NOT NULL,	-- Taxa cobrada pelo uso
    Status_COV		NUMERIC (002)   NOT NULL,   -- 1 - Ativo, 2 - Inativo, 3 - Excluido
	CONSTRAINT Sequen_COV_PK	PRIMARY KEY (Sequen_COV)	
)
/*--------------------------------------------------------*/
/*              TabRotas - Tabela de Rotas                */
/*--------------------------------------------------------*/
CREATE TABLE TabRotas
(
	Sequen_ROT		NUMERIC (006)	NOT NULL,	-- Código da Rota
	Descri_ROT		VARCHAR (100)	NOT NULL,	-- Descrição da Rota
	Status_ROT		NUMERIC (002)   NOT NULL,   -- 1 - Ativo, 2 - Inativo, 3 - Excluido
	CONSTRAINT Sequen_ROT_PK	PRIMARY KEY (Sequen_ROT)	
)
/*--------------------------------------------------------*/
/*            TabRot01 - Clientes das Rotas               */
/*--------------------------------------------------------*/
CREATE TABLE TabRot01
(
	Sequen_RO1		NUMERIC (006)	NOT NULL,	-- Código da Rota
	SeqCli_RO1		NUMERIC (006)	NOT NULL,   -- Código do cliente da rota
	Ordems_RO1		NUMERIC (004)   NOT NULL,   -- Númeração da ordem em relação a cidade
	Status_RO1		NUMERIC (002)	NOT NULL,   -- Status da cidade na rota 1 - Ativo, 2 - Inativo
	CONSTRAINT Sequen_Ordems_RO1_PK	PRIMARY KEY (Sequen_RO1,Ordems_RO1)
)
/*--------------------------------------------------------*/
/*              TabCfope - Tabela de CFOPs               */
/*--------------------------------------------------------*/
CREATE TABLE TabCfope
(
	Sequen_CFO		NUMERIC (004)	NOT NULL,	-- Código do cfop
	Descri_CFO		VARCHAR (100)	NOT NULL,	-- Descrição do cfop
	EntCom_CFO		NUMERIC (004)	NOT NULL,   -- Correspondente para entrada em comercio
	EntInd_CFO		NUMERIC (004)	NOT NULL,	-- Correspondente para entrada em indus.
	DenFor_CFO		NUMERIC (001)	NOT NULL,   -- CFOP Dentro ou Fora estado 0 = dentro, 1 = fora
	CONSTRAINT Sequen_CFO_PK	PRIMARY KEY (Sequen_CFO)	
)
/*--------------------------------------------------------*/
/*       TabMsgNt - Tabela de Mensagens para notas        */
/*--------------------------------------------------------*/
CREATE TABLE TabMsgNt
(
	Sequen_MSG		NUMERIC (003)	NOT NULL,	-- Código da mensagem
	Descri_MSG		VARCHAR (100)	NOT NULL,	-- Descrição da mensagem
	SeqEmp_MSG		NUMERIC (006)	NOT NULL,   -- Empresa da mensagem
	CONSTRAINT Sequen_SeqEmp_MSG_PK	PRIMARY KEY (Sequen_MSG,SeqEmp_MSG)	
)
/*--------------------------------------------------------*/
/*       TabConfi - Tabela de Configuração Geral          */
/*--------------------------------------------------------*/
CREATE TABLE TabConfi
(
	SeqLoj_CON		NUMERIC (006)	NOT NULL,	-- Código da Loja a ser configurada
	QtPesq_CON		NUMERIC (002)	NOT	NULL,	-- Quantidade de resultados das pesquisas
	GraXML_CON		VARCHAR	(005)	NOT	NULL,	-- Gravar filtros das pesquisas e impressão?
	VerBco_CON		VARCHAR (010)	NOT	NULL,	-- Define a versão do banco de dados
	CorFun_CON		VARCHAR	(030)   NOT NULL,   -- Define a cor de fundo das janelas ñ-padrão
	CamRel_CON		VARCHAR (080)	NOT NULL,   -- Salvar arquivos EXCEL e WORD onde?
	PlayMu_CON		VARCHAR (010)		NULL,   -- Tocar música ao logar
	LixMas_CON		VARCHAR (010)       NULL,   -- Apenas usuário master pode acessar a lixeira?
	TimeOc_CON		NUMERIC (002)		NULL,   -- Tempo de ocioso (Em minutos)
	LojMas_CON		VARCHAR (010)		NULL,   -- Apenas usuário master acessa Loja?
	AleRec_CON		VARCHAR (010)		NULL,	-- Alerta em Cts a Receber?
	AlePag_CON		VARCHAR (010)		NULL,	-- Alerta em Cts a Pagar?
	DatCom_CON		VARCHAR (010)		NULL,	-- Data comemorativas nos pedidos e notas?
	AleOff_CON		VARCHAR (010)		NULL,	-- Alerta Office não instalado?
  	OcuPan_CON		VARCHAR (010)		NULL,	-- Ocultar painel?
    ExiEme_CON		VARCHAR (010)		NULL,   -- Exibe mensagem no modo de emergencia?
	PerUsu_CON		VARCHAR (010)		NULL,	-- Pergunta antes de trocar o usuário
	NomAt1_CON		VARCHAR (016)	NOT	NULL,	-- Nome do primeiro atalho
	NomAt2_CON		VARCHAR (016)	NOT	NULL,	-- Nome do segundo atalho
	NomDl1_CON		VARCHAR (014)		NULL,	-- Nome da primeira DLL
	NomDl2_CON		VARCHAR (014)		NULL,	-- Nome da segunda DLL
	LacCai_CON		VARCHAR (010)		NULL,	-- Permitir lançamento manual no caixa
    CONSTRAINT SeqLoj_CON_PK	PRIMARY KEY (SeqLoj_CON)
)

/*--------------------------------------------------------*/
                          USE master
                          
                          