using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Loja.Data.Models
{
    public class Endereco
    {
        public virtual int Id { get; set; }
        public virtual string Logradouro { get; set; }
        public virtual string Distrito { get; set; }
        public virtual string Localicidade { get; set; }
        public virtual string UF { get; set; }
        public virtual string CEP { get; set; }
        public virtual string Numero { get; set; }
        public virtual int TipoEnderecoNumerico 
        {
            get
            {
                return (int)tipoEndereco;
            }
            set
            {
                tipoEndereco = value;
            }
        }

        public virtual Pessoa Pessoa { get; set; }

        public virtual TipoEndereco TipoEndereco
        {
            get
            {
                return (TipoEndereco)tipoEndereco;
            }
            set
            {
                tipoEndereco = (int)value;
            }
        }

        private int tipoEndereco;
    }
}
