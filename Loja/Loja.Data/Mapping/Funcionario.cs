using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Data.Mapping
{
    public class Funcionario : ClassMap<Models.Funcionario>
    {
        public Funcionario()
        {
            Table("FUNCIONARIO");
            Id(x => x.Id, "FUNCIONARIO_ID").GeneratedBy.Increment();
            Map(x => x.Usuario, "USUARIO");
            Map(x => x.Funcao, "FUNCAO");
            Map(x => x.SenhaHash, "SENHA");
            References(x => x.Pessoa).Column("PESSOA_ID").ForeignKey("FK_FUNCIONARIO_PESSOA_ID").Cascade.All();
        }
    }
}
