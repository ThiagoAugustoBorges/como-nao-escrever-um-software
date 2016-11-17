using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TechSIS_BWK
{
    internal partial class MsgMódulo : Form
    {
        public MsgMódulo()
        {
            InitializeComponent();
        }

        public string _MODULO { get; set; }

        private void rabFree_CheckedChanged(object sender, EventArgs e)
        {
            if (rabFree.Checked == true)
            {
                Texto.Text = "TechSIS FREE - Developed by Thiago 'Wenemy' Borges\n\n\n\bCADASTROS DE CIDADE AUTOMATIZADO\n\bATUALIZAÇÕES AUTOMÁTICAS\n\bHISTÓRICO SOBRE ATIVIDADES DE USUÁRIOS\n\bINSTALAÇÃO DE VÁRIAS MAQUINAS EM UM MESMO BANCO DE DADOS (LIMITE 3)\n\bREPORTE SOBRE ERROS,UPGRADES E SUGESTÕES\n\bCADASTRO DE EMPRESA ÚNICA SEM DIREITO A INFORMAR SEU CNPJ OU CPF\n\bCADASTRO DE FORNECEDORES\n\bCADASTRO DE TRANSPORTADORAS\n\bCADASTRO DE PRODUTOS PARA REVENDA\\MATÉRIA PRIMA E USO E CONSUMO\n\bCADASTRO DE SERVIÇOS PRÓPRIOS E DE TERCEIROS\n\bCADASTRO DE MAQUINÁRIO\\OPERADOR E VEÍCULOS\\MOTORISTAS\n\bCADASTRO DE VENDEDORES\n\bCADASTRO DE FUNCIONÁRIOS";
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdqLicenca_Click(object sender, EventArgs e)
        {
            MessageBox.Show("OPÇÃO ESTARÁ DISPONÍVEL EM BREVE....\nMANTENHA O PROGRAMA SEMPRE ATUALIZADO!!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
