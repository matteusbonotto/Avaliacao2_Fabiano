using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util;

namespace Loja
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.Hide();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ValidaAcessoSistema();
        }

        private void ValidaAcessoSistema()
        {
            frmAcessoSistema acessoSistema = new frmAcessoSistema();
            acessoSistema.ShowDialog();

            if (acessoSistema.DialogResult)
            {
                this.Visible = true;
                this.BringToFront();
            }
            else
            {
                Application.Exit();
            }
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conteudo.CarregarUserControl(new uCliente());
        }
    }
}
