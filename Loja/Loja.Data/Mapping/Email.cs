using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Data.Mapping
{
    public class Email : ClassMap<Models.Email>
    {
        public Email()
        {
            Table("EMAIL");
            Id(x => x.Id, "EMAIL_ID").GeneratedBy.Increment();
            Map(x => x.Endereco, "ENDERECO");
            LazyLoad();
            References(x => x.Pessoa).Column("PESSOA_ID").ForeignKey("FK_EMAIL_PESSOA_ID").Cascade.All();
        }
    }
}
