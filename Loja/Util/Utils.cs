using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Util
{
    public static class Utils
    {
        public static bool IsTrimNullOrEmpty(this string texto) => texto.Trim().IsNullOrEmpty();
        public static bool IsNullOrEmpty(this string texto) => string.IsNullOrEmpty(texto);
        public static bool IsNullOrEmpty(this object obj) => obj == null;
        public static bool IsNullOrEmpty(this List<object> list) => list == null || list.Count == 0;
        public static bool IsNullOrEmpty(this IList<object> list) => list == null || list.Count == 0;
    }

    public enum Situacao
    {
        Desativado = 0,
        Ativo = 1
    }
    public enum TipoComandoSQL
    {
        Atualizar,
        Inserir,
        Excluir,
        InserirOuAtualizar,
        TextoSQL
    }
    public enum TipoLog
    {
        Geral,
        BancoDados,
        Erro
    }
}
