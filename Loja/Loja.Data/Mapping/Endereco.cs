using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Data.Mapping
{
    public class Endereco : ClassMap<Models.Endereco>
    {
        public Endereco()
        {
            Table("ENDERECO");
            Id(x => x.Id, "ENDERECO_ID").GeneratedBy.Increment();
            Map(x => x.Logradouro, "LOGRADOURO");
            Map(x => x.Distrito, "DISTRITO");
            Map(x => x.Localicidade, "LOCALIDADE");
            Map(x => x.UF, "UF");
            Map(x => x.CEP, "CEP");
            Map(x => x.Numero, "NUMERO");
            Map(x => x.TipoEnderecoNumerico, "TIPO");
            References(x => x.Pessoa).Column("PESSOA_ID").ForeignKey("FK_ENDERECO_PESSOA_ID").Cascade.All();
        }
    }
}
