using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Loja.Data.Models
{
    public class Pessoa
    {
        public virtual int Id { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
        public virtual string Sexo { get; set; }
        public virtual string RazaoSocialNomeCompleto { get; set; }
        public virtual string NomeFantasiaApelido { get; set; }
        public virtual string CnpjCpf { get; set; }
        public virtual int TipoPessoaNumerica {
            get
            {
                return this.tipoPessoa;
            }
            set
            {
                this.tipoPessoa = value;
            }
        }

        public virtual ISet<Email> Emails { get; set; }
        public virtual ISet<Telefone> Telefones { get; set; }
        public virtual ISet<Endereco> Enderecos { get; set; }


        public virtual TipoPessoa TipoPessoa
        {
            get
            {
                return (TipoPessoa)this.tipoPessoa;
            }
            set
            {
                this.tipoPessoa = (int)value;
            }
        }

        private int tipoPessoa;
        public Pessoa()
        {
            Emails = new HashSet<Email>();
            Telefones = new HashSet<Telefone>();
            Enderecos = new HashSet<Endereco>();
            DataAlteracao = DateTime.Now;
        }
    }
}
