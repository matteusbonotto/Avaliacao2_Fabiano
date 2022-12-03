using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Data.Models
{
    public class Telefone
    {
        public virtual int Id { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Responsavel { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}
