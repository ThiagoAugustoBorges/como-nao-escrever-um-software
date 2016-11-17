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
    internal partial class UsuárioInativo : Form
    {
        public UsuárioInativo()
        {
            InitializeComponent();
        }

        public string CodigoLoja { get; set; }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Opacity = 0;
            Close();
        }

        private void UsuárioInativo_Load(object sender, EventArgs e)
        {
            TechSIS_MET MET = new TechSIS_MET();
            MET.MET_SelecionaCorFundo(txt, txt, txt, txt, txt, txt, CodigoLoja);

            btnOk.Select();
        }

    }
}