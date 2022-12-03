using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Data
{
    public class DAOFuncionario
    {
        public static Models.Funcionario RecuperarPorUsuario(string usuario)
        {
            return CacheDados.Sessao.QueryOver<Models.Funcionario>().Where(x => x.Usuario == usuario).Take(1).SingleOrDefault();
        }
    }
}
