using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Data
{
    public static class CacheDados
    {
        public static ISession Sessao { get; set; }
    }
}
