using Loja.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Loja.Business
{
    public class Autenticacao
    {
        public static bool ValidarAcessoSistema(string usuario, string senha, out string mensagemErro)
        {
            mensagemErro = string.Empty;

            if (usuario.IsNullOrEmpty())
            {
                mensagemErro = "Digite um usuário válido";
                return false;
            }

            if (senha.IsNullOrEmpty())
            {
                mensagemErro = "Digite uma senha válida";
                return false;
            }

            var func = DAOFuncionario.RecuperarPorUsuario(usuario);


            if (func.IsNullOrEmpty() || !Utils.CompararHashSha3(senha, Encoding.ASCII.GetString(func.SenhaHash)))
            {
                mensagemErro = "Usuário e ou senha estão incorretos";
                return false;
            }

            return true;
        }
    }
}
