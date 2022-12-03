using Google.Protobuf.WellKnownTypes;
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Loja.Data.Models
{
    public class Funcionario
    {
        public virtual int Id { get; set; }
        public virtual string Usuario { get; set; }
        public virtual string Funcao { get; set; }
        public virtual byte[] SenhaHash 
        { 
            get 
            {
                if(senha.IsNullOrEmpty())
                    return null;

                return Encoding.ASCII.GetBytes(senha.ObterTexto());
            }
            set
            {
                if (!value.IsNullOrEmpty())
                    senha = senha.Preencher(value);
            }
        }

        public virtual Pessoa Pessoa { get; set; }
     
        public virtual SecureString Senha
        {
            get
            {
                return senha;
            }
            set
            {
                if (!value.IsNullOrEmpty())
                {
                    SenhaHash = Encoding.ASCII.GetBytes(value.ObterTexto());
                }

                senha = value;
            }
        }

        private SecureString senha;
    }
}
