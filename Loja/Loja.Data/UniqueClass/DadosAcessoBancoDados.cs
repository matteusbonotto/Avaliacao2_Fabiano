using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using Util;

namespace Loja.Data.UniqueClass
{
    public class DadosAcessoBancoDados
    {
        public string Servidor { get; private set; }
        public string Porta { get; private set; }
        public string Nome { get; private set; }
        public string Usuario { get; private set; }
        public SecureString Senha { get; private set; }

        public DadosAcessoBancoDados()
        {
            this.Servidor = "localhost";
            this.Porta = "3306";
            this.Nome = "loja";
            this.Usuario = "projetos";
            this.Senha = this.Senha.Preencher(new byte[] { 0x53, 0x75, 0x70, 0x72, 0x69, 0x2A, 0x2A, 0x32, 0x30, 0x31, 0x36 });
        }
    }
}
