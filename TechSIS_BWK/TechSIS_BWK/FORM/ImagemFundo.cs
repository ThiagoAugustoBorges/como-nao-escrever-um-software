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
    internal partial class ImagemFundo : Form
    {
        public ImagemFundo()
        {
            InitializeComponent();
        }

        public string LojaLogada { get; set; }

        public Form FORMULARIO { get; set; }

        private void ImagemFundo_Load(object sender, EventArgs e)
        {
            TechSIS_MET MET = new TechSIS_MET();
            MET.MET_SelecionaCorFundo(Painel_Up, Painel_Up, Painel_Up, Painel_Up, Painel_Up, Painel_Up, LojaLogada);
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            txtCaminho.Text = string.Empty;

            DialogResult REs = OpenFILE.ShowDialog();

            if (REs == DialogResult.OK)
            {
                txtCaminho.Text = OpenFILE.FileName;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCaminho.Text != string.Empty)
                {
                    FORMULARIO.BackgroundImage.Dispose();
                    try
                    {
                        File.Copy(txtCaminho.Text, "..\\Imagens\\TechSIS Unic Soft.png", true);
                        FORMULARIO.BackgroundImage = Image.FromFile("..\\Imagens\\TechSIS Unic Soft.png");
                    }
                    catch (Exception)
                    {

                    }

                    Close();
                }
                else
                {
                    MessageBox.Show("Selecione uma imagem .PNG primeiramente!", "TechSIS Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao trocar a imagem!", "TechSIS Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
