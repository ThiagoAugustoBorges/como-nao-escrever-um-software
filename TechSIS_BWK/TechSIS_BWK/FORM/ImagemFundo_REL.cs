using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TechSIS_BWK
{
    internal partial class ImagemFundo_REL : Form
    {
        public ImagemFundo_REL()
        {
            InitializeComponent();
        }

        public string Login_LojaID_Image { get; set; }

        private void ImagemFundo_REL_Load(object sender, EventArgs e)
        {
            TechSIS_MET MET = new TechSIS_MET();
            MET.MET_SelecionaCorFundo(panUp, panUp, panUp, panUp, panUp, panUp, Login_LojaID_Image);

            try
            {
                picAnt.Image = Image.FromFile("..\\Imagens\\LogoEmp.jpg");
            }
            catch (Exception)
            {

            }
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                picAnt.Image.Dispose();
                File.Copy(OpenFILE.FileName, "..\\Imagens\\LogoEmp.jpg", true);
                picAnt.Image = Image.FromFile("..\\Imagens\\LogoEmp.jpg");
                MessageBox.Show("Imagem alterada com sucesso", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                picAnt.Image.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao trocar a imagem!", "TechSIS Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
            Close();
        }

        private void btnProcurarImagem_Click(object sender, EventArgs e)
        {
            picNova.Image = null;

            DialogResult REs = OpenFILE.ShowDialog();

            if (REs == DialogResult.OK)
            {
                picNova.Image = Image.FromFile(OpenFILE.FileName);
            }
        }
    }
}
