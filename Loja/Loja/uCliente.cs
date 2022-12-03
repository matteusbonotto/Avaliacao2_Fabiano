using Loja.Data.Mapping;
using Loja.Data.UniqueClass;
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
    public partial class uCliente : UserControl
    {
        private Data.Models.Pessoa PessoaAtual { get; set; }
        private int RowIndexEndereco { get; set; }
        private int RowIndexTelefone { get; set; }
        private int RowIndexEmail { get; set; }

        private bool Alterar { 
            get 
            {
               return !btnNovo.Enabled;
            }
            set 
            {
                btnNovo.Enabled = !value;
                btnSalvar.Enabled = value;
                btnCancelar.Enabled = value;
                btnExcluir.Enabled = value;
            } 
        }

        private bool Novo
        {
            get
            {
                return btnNovo.Enabled;
            }
            set
            {
                btnNovo.Enabled = !value;
                btnExcluir.Enabled = value;
                btnSalvar.Enabled = value;
                btnCancelar.Enabled = value;
            }
        }
        public uCliente()
        {
            InitializeComponent();
            PessoaAtual = new Data.Models.Pessoa();
        }

        private void ultimoRegistro()
        {
            int id = Business.Pessoa.UltimoRegistro();
            lblUltimoId.Text = $"Último cód.: {id}";
        }

        private void limparDadosTela()
        {
            RowIndexEndereco = -1;
            RowIndexEmail = -1;
            RowIndexTelefone = -1;
            Alterar = false;
            Novo = false;
            this.LimparTextoComponentes();
            this.AtivarDesativarComponentes(false);
            ultimoRegistro();
        }

        private void uCliente_Load(object sender, EventArgs e)
        {
            rbPessoaFisica.Checked = true;
            limparDadosTela();

            List<Data.UniqueClass.UniqueGenerico> sexos = new List<Data.UniqueClass.UniqueGenerico>() 
            {
                new Data.UniqueClass.UniqueGenerico() { Identificador = "N", Descricao = "Nenhum" },
                new Data.UniqueClass.UniqueGenerico() { Identificador = "M", Descricao = "Masculino" },
                new Data.UniqueClass.UniqueGenerico() { Identificador = "F", Descricao = "Feminino" }
            };

            cbbSexo.DataSource = sexos;
            cbbSexo.ValueMember = "Identificador";
            cbbSexo.DisplayMember = "Descricao";
        }

        private void rbPessoaFisica_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPessoaFisica.Checked.Equals(true))
            {
                lblNomeRazaoSocial.Text = "Nome completo";
                lblApelidoNomeFantasia.Text = "Apelido";
                lblCpfCnpj.Text = "CPF";

                cbbSexo.Visible = true;
                if (cbbSexo.Items.Count > 0)
                    cbbSexo.SelectedIndex = 0;
                lblSexo.Visible = true;

                txtCpf.Mask = "000,000,000-00";
            }
        }

        private void rbPessoaJuridica_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPessoaJuridica.Checked.Equals(true))
            {
                lblNomeRazaoSocial.Text = "Razão social";
                lblApelidoNomeFantasia.Text = "Nome fantasia";
                lblCpfCnpj.Text = "CNPJ";

                cbbSexo.Visible = false;
                cbbSexo.SelectedIndex = 0;
                lblSexo.Visible = false;
                txtCpf.Mask = "00,000,000/0000-00";
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            this.AtivarDesativarComponentes(true);
            Novo = true;
            btnExcluir.Enabled = false;
        }

        private void btnAdicionarEndereco_Click(object sender, EventArgs e)
        {
            if(rbCobranca.Checked.Equals(false) && rbEntrega.Checked.Equals(false))
            {
                MessageBox.Show("Selecione um tipo de endereço","Endereço", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mensagemErro = null;

            if (txtRuaLogradouro.Text.IsNullOrEmpty())
            {
                mensagemErro = "Digite a rua/logradouro do endereço";
            }
            else if (txtNumero.Text.IsNullOrEmpty())
            {
                mensagemErro = "Digite o número";
            }
            else if (txtCep.Text.IsNullOrEmpty())
            {
                mensagemErro = "Digite um CEP válido";
            }
            else if (txtBairroDistrito.Text.IsNullOrEmpty())
            {
                mensagemErro = "Digite um bairro/distrito";
            }
            else if (txtCidade.Text.IsNullOrEmpty())
            {
                mensagemErro = "Digite uma cidade";
            }
            else if (txtUF.Text.IsNullOrEmpty())
            {
                mensagemErro = "Digite UF do estado";
            }

            if (mensagemErro.IsNullOrEmpty())
            {
                if(RowIndexEndereco >= 0)
                {
                    dataGridViewEndereco.Rows[RowIndexEndereco].Cells[0].Value = rbCobranca.Checked ? TipoEndereco.Cobranca : TipoEndereco.Entrega;
                    dataGridViewEndereco.Rows[RowIndexEndereco].Cells[1].Value = txtRuaLogradouro.Text;
                    dataGridViewEndereco.Rows[RowIndexEndereco].Cells[2].Value = txtNumero.Text;
                    dataGridViewEndereco.Rows[RowIndexEndereco].Cells[3].Value = txtCep.Text;
                    dataGridViewEndereco.Rows[RowIndexEndereco].Cells[4].Value = txtBairroDistrito.Text;
                    dataGridViewEndereco.Rows[RowIndexEndereco].Cells[5].Value = txtCidade.Text;
                    dataGridViewEndereco.Rows[RowIndexEndereco].Cells[6].Value = txtUF.Text;
                }
                else
                {
                    dataGridViewEndereco.Rows.Add(
                    rbCobranca.Checked ? TipoEndereco.Cobranca : TipoEndereco.Entrega,
                    txtRuaLogradouro.Text, txtNumero.Text, txtCep.Text, txtBairroDistrito.Text, txtCidade.Text, txtUF.Text, 0
                    );
                }
                RowIndexEndereco = -1;
                btnAdicionarEndereco.Text = "Adicionar";
                rbCobranca.Checked = false;
                rbEntrega.Checked = false;
                txtRuaLogradouro.Clear();
                txtNumero.Clear();
                txtCep.Clear();
                txtBairroDistrito.Clear();
                txtCidade.Clear();
                txtUF.Clear();
                dataGridViewEndereco.ClearSelection();
            }
            else
            {
                MessageBox.Show(mensagemErro, "Endereço", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridViewEndereco_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                rbCobranca.Checked = (TipoEndereco)dataGridViewEndereco.Rows[e.RowIndex].Cells[0].Value == TipoEndereco.Cobranca;
                rbEntrega.Checked = (TipoEndereco)dataGridViewEndereco.Rows[e.RowIndex].Cells[0].Value == TipoEndereco.Entrega;
                txtRuaLogradouro.Text = dataGridViewEndereco.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtNumero.Text = dataGridViewEndereco.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtCep.Text = dataGridViewEndereco.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtBairroDistrito.Text = dataGridViewEndereco.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtCidade.Text = dataGridViewEndereco.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtUF.Text = dataGridViewEndereco.Rows[e.RowIndex].Cells[6].Value.ToString();
                RowIndexEndereco = e.RowIndex;
                btnAdicionarEndereco.Text = "Alterar";
            }
        }

        private void btnAdicionarTelefone_Click(object sender, EventArgs e)
        {
            if (txtNumeroTelefone.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Digite um número de telefone válido", "Telefone", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(RowIndexTelefone >= 0)
            {
                dataGridViewTelefone.Rows[RowIndexTelefone].Cells[0].Value = txtNumeroTelefone.Text;
                dataGridViewTelefone.Rows[RowIndexTelefone].Cells[1].Value = txtResponsavel.Text;
            }
            else
            {
                dataGridViewTelefone.Rows.Add(txtNumeroTelefone.Text, txtResponsavel.Text,0);
            }

            btnAdicionarTelefone.Text = "Adicionar";
            RowIndexTelefone = -1;
            txtNumeroTelefone.Clear();
            txtResponsavel.Clear();
            dataGridViewTelefone.ClearSelection();
        }

        private void dataGridViewTelefone_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                txtNumeroTelefone.Text = dataGridViewTelefone.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtResponsavel.Text = dataGridViewTelefone.Rows[e.RowIndex].Cells[1].Value.ToString();
                RowIndexTelefone = e.RowIndex;
                btnAdicionarTelefone.Text = "Alterar";
            }
        }

        private void txtAdicionarEmail_Click(object sender, EventArgs e)
        {
            if (txtEnderecoEmail.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Digite um e-mail válido", "E-mail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (RowIndexEmail >= 0)
            {
                dataGridViewEmail.Rows[RowIndexEmail].Cells[0].Value = txtEnderecoEmail.Text;
            }
            else
            {
                dataGridViewEmail.Rows.Add(txtEnderecoEmail.Text,0);
            }


            dataGridViewEmail.ClearSelection();
            btnAdicionarEmail.Text = "Adicionar";
            RowIndexEmail = -1;
            txtEnderecoEmail.Clear();
        }

        private void dataGridViewEmail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtEnderecoEmail.Text = dataGridViewEmail.Rows[e.RowIndex].Cells[0].Value.ToString();

                RowIndexEmail = e.RowIndex;
                btnAdicionarEmail.Text = "Alterar";
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparDadosTela();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string mensagemErro = null;

            if(rbPessoaFisica.Checked.Equals(false) && rbPessoaJuridica.Checked.Equals(false))
            {
                mensagemErro = "Selecione um tipo de pessoa (física ou jurídica)";
            }
            else if (txtNome.Text.IsNullOrEmpty())
            {
                mensagemErro = $"Digite {(rbPessoaFisica.Checked ? "um nome" : "uma razão social")}";
            }
            else if (txtApelido.Text.IsNullOrEmpty())
            {
                mensagemErro = $"Digite {(rbPessoaFisica.Checked ? "um apelido" : "um nome fantasia")}";
            }
            else if (txtCpf.Text.IsNullOrEmpty())
            {
                mensagemErro = $"Digite {(rbPessoaFisica.Checked ? "um CPF" : "um CNPJ")}";
            }
            else if (cbbSexo.SelectedIndex == 0 && rbPessoaFisica.Checked)
            {
                mensagemErro = $"Selecione um sexo para continuar";
            }


            if (mensagemErro.IsNullOrEmpty())
            {
                if (PessoaAtual.IsNullOrEmpty() || PessoaAtual.Id > 0)
                {
                    int id = 0;
                    if(!PessoaAtual.IsNullOrEmpty() && PessoaAtual.Id > 0)
                        id = PessoaAtual.Id;
                    Data.DAO.LimparSessao();
                    PessoaAtual = new Data.Models.Pessoa() { Id = id};
                }
                    

                PessoaAtual.TipoPessoa = rbPessoaFisica.Checked ? TipoPessoa.Fisica : TipoPessoa.Juridica;
                PessoaAtual.RazaoSocialNomeCompleto = txtNome.Text;
                PessoaAtual.NomeFantasiaApelido = txtApelido.Text;
                PessoaAtual.CnpjCpf = txtCpf.Text;

                if (rbPessoaFisica.Checked)
                {
                    var item = (UniqueGenerico)cbbSexo.SelectedItem;
                    PessoaAtual.Sexo = item.Identificador.ToString();
                }

                if(dataGridViewEndereco.Rows.Count > 0)
                {
                    if (PessoaAtual.Enderecos.IsNullOrEmpty() || PessoaAtual.Enderecos.Count >0)
                        PessoaAtual.Enderecos = new HashSet<Data.Models.Endereco>();

                    foreach (DataGridViewRow row in dataGridViewEndereco.Rows)
                    {
                        PessoaAtual.Enderecos.Add(new Data.Models.Endereco()
                        {
                            TipoEndereco = (TipoEndereco)row.Cells[0].Value,
                            Logradouro = row.Cells[1].Value.ToString(),
                            Numero = row.Cells[2].Value.ToString(),
                            CEP = row.Cells[3].Value.ToString(),
                            Distrito = row.Cells[4].Value.ToString(),
                            Localicidade = row.Cells[5].Value.ToString(),
                            UF = row.Cells[6].Value.ToString(),
                            Id = (int)row.Cells[7].Value
                        }) ;
                    }
                }

                if (dataGridViewTelefone.Rows.Count > 0)
                {
                    if (PessoaAtual.Telefones.IsNullOrEmpty() || PessoaAtual.Telefones.Count > 0)
                        PessoaAtual.Telefones = new HashSet<Data.Models.Telefone>();

                    foreach (DataGridViewRow row in dataGridViewTelefone.Rows)
                    {
                        PessoaAtual.Telefones.Add(new Data.Models.Telefone() 
                        { 
                            Numero = row.Cells[0].Value.ToString(),
                            Responsavel = row.Cells[1].Value.ToString(),
                            Id = (int)row.Cells[2].Value
                        });
                    }
                }

                if (dataGridViewEmail.Rows.Count > 0)
                {
                    if (PessoaAtual.Emails.IsNullOrEmpty() || PessoaAtual.Emails.Count > 0)
                        PessoaAtual.Emails = new HashSet<Data.Models.Email>();

                    foreach (DataGridViewRow row in dataGridViewEmail.Rows)
                    {
                        PessoaAtual.Emails.Add(new Data.Models.Email()
                        {
                            Endereco = row.Cells[0].Value.ToString(),
                            Id = (int)row.Cells[1].Value
                        });
                    }
                }

                if (Business.Pessoa.Salvar(PessoaAtual, out mensagemErro))
                {
                    PessoaAtual = new Data.Models.Pessoa();
                    limparDadosTela();
                    ultimoRegistro();
                    MessageBox.Show("Sucesso ao salvar cliente", "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(mensagemErro, "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(mensagemErro, "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            frmPesquisar pesquisar = new frmPesquisar();
            pesquisar.EventoClienteSelecionado += Pesquisar_EventoClienteSelecionado;
            pesquisar.ShowDialog();
        }

        private void Pesquisar_EventoClienteSelecionado(frmPesquisar form)
        {
            PessoaAtual = form.PessoaSelecionada;
            form.Hide();

            limparDadosTela();
            Alterar = true;

            if (PessoaAtual.TipoPessoa == TipoPessoa.Fisica)
            {
                rbPessoaFisica.Checked = true;
                cbbSexo.SelectedValue = PessoaAtual.Sexo;
            }
            else
            {
                rbPessoaJuridica.Checked = true;
                cbbSexo.SelectedIndex = 0;
            }

            txtNome.Text = PessoaAtual.RazaoSocialNomeCompleto;
            txtApelido.Text = PessoaAtual.NomeFantasiaApelido;
            txtCpf.Text = PessoaAtual.CnpjCpf;

            if (!PessoaAtual.Enderecos.IsNullOrEmpty())
                foreach (var item in PessoaAtual.Enderecos)
                {
                    dataGridViewEndereco.Rows.Add(item.TipoEndereco, item.Logradouro, item.Numero, item.CEP, item.Distrito, item.Localicidade, item.UF, item.Id);
                }

            if (!PessoaAtual.Telefones.IsNullOrEmpty())
                foreach (var item in PessoaAtual.Telefones)
                {
                    dataGridViewTelefone.Rows.Add(item.Numero, item.Responsavel, item.Id);
                }

            if (!PessoaAtual.Emails.IsNullOrEmpty())
                foreach (var item in PessoaAtual.Emails)
                {
                    dataGridViewEmail.Rows.Add(item.Endereco, item.Id);
                }

            dataGridViewEndereco.ClearSelection();
            dataGridViewTelefone.ClearSelection();
            dataGridViewEmail.ClearSelection();
            this.AtivarDesativarComponentes(true);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (Business.Pessoa.Excluir(PessoaAtual, out string mensagemErro))
            {
                PessoaAtual = new Data.Models.Pessoa();
                limparDadosTela();
                ultimoRegistro();
                MessageBox.Show("Excluído com sucesso", "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(mensagemErro, "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
