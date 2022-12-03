using Antlr.Runtime.Tree;
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
    public partial class frmPesquisar : Form
    {
        public Data.Models.Pessoa PessoaSelecionada { get; set; }
        public List<Data.Models.Pessoa> Pessoas { get; set; }
        public delegate void DelegateEventoClienteSelecionado(frmPesquisar form);
        public event DelegateEventoClienteSelecionado EventoClienteSelecionado;
        public frmPesquisar()
        {
            InitializeComponent();
        }

        private void frmPesquisar_Load(object sender, EventArgs e)
        {
            List<Data.Models.Pessoa> pessoas = Business.Pessoa.RecuperarTodosRegistros();

            if (pessoas.IsNullOrEmpty())
            {
                MessageBox.Show("Não existe pessoas cadastradas","Pesquisar",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }

            foreach (var pessoa in pessoas)
            {
                dataGridViewCliente.Rows.Add(pessoa.Id, pessoa.RazaoSocialNomeCompleto, pessoa.CnpjCpf);
            }

            Pessoas = pessoas;
        }

        private void dataGridViewCliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                int id = (int)dataGridViewCliente.Rows[e.RowIndex].Cells[0].Value;
                PessoaSelecionada = Pessoas.Where(x => x.Id == id).Take(1).First();
                EventoClienteSelecionado(this);
            }
            else
            {
                MessageBox.Show("Não foi possível validar cliente selecionado", "Pesquisar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
