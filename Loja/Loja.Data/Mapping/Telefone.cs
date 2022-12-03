using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Data.Mapping
{
    public class Telefone : ClassMap<Models.Telefone>
    {
        public Telefone()
        {
            Table("TELEFONE");
            Id(x => x.Id, "TELEFONE_ID").GeneratedBy.Increment();
            Map(x => x.Responsavel, "RESPONSAVEL");
            Map(x => x.Numero, "NUMERO");
            References(x => x.Pessoa).Column("PESSOA_ID").ForeignKey("FK_TELEFONE_PESSOA_ID").Cascade.All();
        }
    }
}
