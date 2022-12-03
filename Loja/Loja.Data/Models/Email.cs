using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Data.Models
{
    public class Email
    {
        public virtual int Id { get; set; } 
        public virtual string Endereco { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}
