using Loja.Data;
using Loja.Data.Models;
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
    public partial class frmAcessoSistema : Form
    {
        public new bool DialogResult { get; set; }
        public frmAcessoSistema()
        {
            InitializeComponent();

            /*Pessoa pessoa = new Pessoa();
            Funcionario funcionario = new Funcionario();

            pessoa.NomeFantasiaApelido = "Eliezer Ferreira";
            pessoa.RazaoSocialNomeCompleto = "Eliezer Aparecido Ferreira da Silva";
            pessoa.DataCadastro = DateTime.Now;
            pessoa.Sexo = "M";


            funcionario.Pessoa = pessoa;
            funcionario.Usuario = "eliezer";
            funcionario.Senha = funcionario.Senha.Preencher(new byte[] { 0xf5, 0x24, 0x0F});
            funcionario.Funcao = "Administrador";

            DAO.Inserir(funcionario);*/
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            ValidarAcesso();
        }

        private void ValidarAcesso()
        {

            if (Business.Autenticacao.ValidarAcessoSistema(txtUsuario.Text, txtSenha.Text, out string mensagemErro))
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(mensagemErro, "Autenticação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                txtSenha.Focus();
            }
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                ValidarAcesso();
            }
        }
    }
}
