using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Loja.Data.UniqueClass
{
    public class DAOComando
    {
        public object Objeto { get; set; }
        public TipoComandoSQL TipoComandoSQL { get; set; }
        public DAOComando() { }
        public DAOComando(object objeto, TipoComandoSQL tipoComandoSQL)
        {
            Objeto = objeto;
            TipoComandoSQL = tipoComandoSQL;
        }
    }
}
