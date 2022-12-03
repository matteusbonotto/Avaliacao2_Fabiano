using Loja.Data;
using Loja.Data.UniqueClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Loja.Business
{
    public class Pessoa
    {
        public static bool Excluir(Data.Models.Pessoa pessoaAtual, out string mensagemErro)
        {
            mensagemErro = null;
            try
            {
                return DAO.Excluir(pessoaAtual);
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
            }

            return false;

        }

        public static List<Data.Models.Pessoa> RecuperarTodosRegistros()
        {
            var resultado = DAO.RecuperarLista<Data.Models.Pessoa>();

            if(resultado.IsNullOrEmpty() && resultado.Count == 0)
            {
                return null;
            }
            else
            {
                
                return resultado.ToList();
            }

        }

        public static bool Salvar(Data.Models.Pessoa pessoaAtual, out string mensagemErro)
        {
            mensagemErro = null;
            try
            {
                DAOComando dAOComando = new DAOComando(pessoaAtual, Util.TipoComandoSQL.InserirOuAtualizar);
                return DAO.Executar(dAOComando);
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
            }

            return false;
        }

        public static int UltimoRegistro()
        {
            return DAO.RecuperarUltimoId<Data.Models.Pessoa>();
        }
    }
}
