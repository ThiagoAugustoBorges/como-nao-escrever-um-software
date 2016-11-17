using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TechSIS_AddEmpre
{
    internal partial class TechSIS_AddEmpre : Form
    {
        public TechSIS_AddEmpre()
        {
            InitializeComponent();
        }

        internal static string _TipoInicialização { get; set; }
        internal static string _CorDeFundo { get; set; }

        public string CaminhoDoArquivo { get; set; }

        void RetornarAoInicio()
        {
            txtEmpresa.Text = string.Empty;
            txtRazao.Text = string.Empty;
            mtbCpfCnpj.Text = string.Empty;
            mtbCpfCnpj.Mask = string.Empty;
            txtFantasia.Text = string.Empty;
            comModulo.SelectedIndex = -1;
            comModuloProvi.SelectedIndex = -1;
            comSistema.SelectedIndex = -1;
            btnInserir.Enabled = false;
        }

        //INSTANCIO A CLASSE
        AddEmpre_MET MET = new AddEmpre_MET();

        //DEFINE O TIPO DE INICIALIZAÇÃO
        private void TechSIS_AddEmpre_Load(object sender, EventArgs e)
        {
            if (_TipoInicialização == "1")
            {
                this.ShowInTaskbar = false;
                panPrin.BackColor = Color.FromName(_CorDeFundo);
                this.Focus();
                this.Deactivate += new EventHandler(TechSIS_AddEmpre_Deactivate);
            }
            else
            {
                this.ShowInTaskbar = true;
            }
        }

        //FECHA QUANDO PERDE O FOCO
        private void TechSIS_AddEmpre_Deactivate(object sender, EventArgs e)
        {
            Application.ExitThread();
        }




        //SAI DA APLICAÇÃO
        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
        //SELECIONA O ARQUIVO SecF
        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            this.Deactivate -= new EventHandler(TechSIS_AddEmpre_Deactivate);
            RetornarAoInicio();
            OpenFileDialog OpenFILE = new OpenFileDialog();
            OpenFILE.DefaultExt = "xml";
            OpenFILE.Filter = "Arquivos XML (*.xml)|*.xml";
            OpenFILE.Title = "TechSIS - LOCALIZAÇÃO DE ARQUIVOS SecF";

            DialogResult Arquivo = OpenFILE.ShowDialog();
            if (Arquivo == System.Windows.Forms.DialogResult.OK)
            {
                CaminhoDoArquivo = OpenFILE.FileName;

                //VERIFICO SE O ARQUIVO É VALIDO
                bool VALIDOS = MET.MET_ArquivoValido(CaminhoDoArquivo);
                if (!VALIDOS) { } else { return; }

                //POPULO STRING COM INFORMAÇÕES DO XML
                MET.MET_PopularStrings(CaminhoDoArquivo);

                MET.txtEmpresa = txtEmpresa;
                MET.txtFantasia = txtFantasia;
                MET.txtRazao = txtRazao;
                MET.mtbCpfCnpj = mtbCpfCnpj;
                MET.comModulo = comModulo;
                MET.comModuloProvi = comModuloProvi;
                MET.comSistema = comSistema;
                MET.btnIncluir = btnInserir;

                //POPULO OS CAMPOS
                MET.MET_PopularControles();
            }

            this.Deactivate += new EventHandler(TechSIS_AddEmpre_Deactivate);
        }
        //INSERE OS VALORES NO BANCO DE DADOS
        private void btnInserir_Click(object sender, EventArgs e)
        {
            this.Deactivate -= new EventHandler(TechSIS_AddEmpre_Deactivate);
            //INSERE A EMRPESA
            bool InsertEmpresa = MET.Conec_InsertEmpresa(CaminhoDoArquivo);
            if (!InsertEmpresa) { } else { Dispose(); return; }

            //REMOVE OS NULOS EMPRESA
            bool NuloEmpresa = MET.Conec_RemoverNuloEmpresa();
            if (!InsertEmpresa) { } else { Dispose(); return; }

            //INSERE A CONFIG
            bool InsertConfig = MET.Conec_InsertConfig(CaminhoDoArquivo);
            if (!InsertConfig) { } else { Dispose(); return; }

            //REMOVE NULS CONFIG
            bool NuloConfig = MET.Conec_RemoverNuloConfig();
            if (!NuloConfig) { } else { Dispose(); return; }

            MessageBox.Show("Empresa adicionada com sucesso no banco de dados", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSair.PerformClick();
        }
    }
}
