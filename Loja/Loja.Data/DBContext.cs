using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using Util;
using System.IO;
using System.Reflection;
using NHibernate.Tool.hbm2ddl;
using Loja.Data.UniqueClass;
using System.Runtime.InteropServices;
using System.Security;
using System;
using System.Text;

namespace Loja.Data
{
    public class DBContext : IDisposable
    {
        public SecureString StringConnection { get; protected set; }
        public DadosAcessoBancoDados DadosAcessoBancoDados { get; set; }
        public DBContext()
        {
            this.DadosAcessoBancoDados = new DadosAcessoBancoDados();
            this.StringConnection = this.StringConnection.Preencher(Encoding.ASCII.GetBytes($"Server={this.DadosAcessoBancoDados.Servidor};Port={this.DadosAcessoBancoDados.Porta};" +
                $"Database={this.DadosAcessoBancoDados.Nome};Uid={this.DadosAcessoBancoDados.Usuario};" +
                $"Pwd={this.DadosAcessoBancoDados.Senha.ObterTexto()};"));
        }
        public bool AbrirConexao()
        {
            try
            {
                if (CacheDados.Sessao == null)
                    CacheDados.Sessao = Fluently.Configure().Database(MySQLConfiguration.Standard.ConnectionString(this.StringConnection.ObterTexto()))
                        .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())).BuildSessionFactory().OpenSession();

                return true;
            }
            catch (HibernateException ex)
            {
                Logs.Gerar(TipoLog.BancoDados, $"{ex.Message} {ex.InnerException}");
                return false;
            }
        }

        public void Dispose()
        {
            if (CacheDados.Sessao != null && (CacheDados.Sessao.IsConnected || CacheDados.Sessao.IsOpen))
            {
                CacheDados.Sessao.Disconnect();
                CacheDados.Sessao.Close();
                CacheDados.Sessao.Dispose();
            }

            if (!DadosAcessoBancoDados.IsNullOrEmpty())
                DadosAcessoBancoDados.Senha.Dispose();

            if (!StringConnection.IsNullOrEmpty())
                StringConnection.Dispose();
        }

        public void CriarBancoDados()
        {
            try
            {
                using (ISessionFactory session = Fluently.Configure().Database(MySQLConfiguration.Standard.ConnectionString(this.StringConnection.ObterTexto()))
                    .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<Models.Email>()
                    .AddFromAssemblyOf<Models.Endereco>()
                    .AddFromAssemblyOf<Models.Telefone>()
                    .AddFromAssemblyOf<Models.Funcionario>()
                    .AddFromAssemblyOf<Models.Pessoa>()
                    ).ExposeConfiguration(e => new SchemaExport(e).Create(true, true)).BuildSessionFactory())
                {
                    session.OpenSession();
                }
            }
            catch (HibernateException ex)
            {
                Logs.Gerar(TipoLog.BancoDados, $"{ex.Message} {ex.InnerException}");
            }
        }

        public void AtualizarEstruturaBancoDados()
        {
            try
            {
                using (ISessionFactory session = Fluently.Configure().Database(MySQLConfiguration.Standard.ConnectionString(this.StringConnection.ObterTexto()))
                    .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<Models.Email>()
                    .AddFromAssemblyOf<Models.Endereco>()
                    .AddFromAssemblyOf<Models.Telefone>()
                    .AddFromAssemblyOf<Models.Funcionario>()
                    .AddFromAssemblyOf<Models.Pessoa>())
                    .ExposeConfiguration(e => new SchemaUpdate(e).Execute(true, true)).BuildSessionFactory())
                {
                    session.OpenSession();
                }
            }
            catch (HibernateException ex)
            {
                Logs.Gerar(TipoLog.BancoDados, $"{ex.Message} {ex.InnerException}");
            }
        }
    }
}
