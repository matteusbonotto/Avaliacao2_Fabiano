using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Data.Mapping
{
    public class Pessoa : ClassMap<Models.Pessoa>
    {
        public Pessoa()
        {
            Table("PESSOA");
            Id(x => x.Id, "PESSOA_ID").GeneratedBy.Increment();
            Map(x => x.DataCadastro, "DATA_CADASTRO");
            Map(x => x.DataAlteracao, "DATA_ALTERACAO");
            Map(x => x.Sexo, "SEXO");
            Map(x => x.NomeFantasiaApelido, "NOME_FANTASIA_OU_APELIDO");
            Map(x => x.RazaoSocialNomeCompleto, "RAZAO_SOCIAL_OU_NOME_COMPLETO");
            Map(x => x.CnpjCpf, "CNPJ_CPF");
            Map(x => x.TipoPessoaNumerica, "TIPO_PESSOA");

            HasMany(x => x.Emails).KeyColumn("PESSOA_ID").KeyUpdate().Cascade.All().Not.LazyLoad().AsSet();
            HasMany(x => x.Telefones).KeyColumn("PESSOA_ID").KeyUpdate().Cascade.All().Not.LazyLoad().AsSet();
            HasMany(x => x.Enderecos).KeyColumn("PESSOA_ID").KeyUpdate().Cascade.All().Not.LazyLoad().AsSet();
        }
    }
}
