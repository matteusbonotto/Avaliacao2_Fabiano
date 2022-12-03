using Loja.Data.UniqueClass;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using Util;
using Expression = NHibernate.Criterion.Expression;

namespace Loja.Data
{
    public class DAO
    {
        public static int RecuperarUltimoId<T>() where T : class
        {
            try
            {
                ICriteria criteria = CacheDados.Sessao.CreateCriteria<T>().SetProjection(Projections.Max("Id"));
                return criteria.UniqueResult<int>();
            }
            catch (HibernateException ex)
            {
                Logs.Gerar(TipoLog.BancoDados, $"{ex.Message} {ex.InnerException}");
                return -1;
            }
        }

        public static IList<T> RecuperarLista<T>() where T : class
        {
            try
            {
                ICriteria criteria = CacheDados.Sessao.CreateCriteria<T>();
                IList<T> lista = criteria.List<T>();

                if (lista != null)
                    return lista;
            }
            catch (HibernateException ex)
            {
                Logs.Gerar(TipoLog.BancoDados, $"{ex.Message} {ex.InnerException}");
            }
            return null;
        }

        public static T RecuperarUnico<T>(int id) where T : class
        {
            try
            {
                ICriteria criteria = CacheDados.Sessao.CreateCriteria<T>();
                criteria.Add(Expression.IdEq(id));

                return criteria.UniqueResult<T>();
            }
            catch (HibernateException ex)
            {
                Logs.Gerar(TipoLog.BancoDados, $"{ex.Message} {ex.InnerException}");
            }
            return null;
        }

        public static bool Inserir(object obj) => Executar(new DAOComando(obj, TipoComandoSQL.Inserir));
        public static bool Atualizar(object obj) => Executar(new DAOComando(obj, TipoComandoSQL.Atualizar));
        public static bool Excluir(object obj) => Executar(new DAOComando(obj, TipoComandoSQL.Excluir));
        public static bool Executar(DAOComando comando) => Executar(new List<DAOComando>() { comando });
        public static bool Executar(List<DAOComando> comandos)
        {
            if (comandos == null)
                return false;

            ITransaction trans = null;

            try
            {
                using (trans = CacheDados.Sessao.BeginTransaction())
                {
                    foreach (DAOComando comando in comandos)
                    {
                        switch (comando.TipoComandoSQL)
                        {
                            case TipoComandoSQL.Inserir:
                                CacheDados.Sessao.Save(comando.Objeto);
                                break;
                            case TipoComandoSQL.Atualizar:
                                CacheDados.Sessao.Update(comando.Objeto);
                                break;
                            case TipoComandoSQL.Excluir:
                                CacheDados.Sessao.Delete(comando.Objeto);
                                break;
                            case TipoComandoSQL.InserirOuAtualizar:
                                CacheDados.Sessao.SaveOrUpdate(comando.Objeto);
                                break;
                            case TipoComandoSQL.TextoSQL:
                                CacheDados.Sessao.CreateSQLQuery(comando.Objeto as string).ExecuteUpdate();
                                break;
                        }
                    }

                    trans.Commit();
                    CacheDados.Sessao.Flush();
                    return true;
                }
            }
            catch (HibernateException ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                    CacheDados.Sessao.Flush();
                }

                Logs.Gerar(TipoLog.BancoDados, $"{ex.Message} {ex.InnerException}");

                return false;
            }
            finally
            {
                LimparSessao();
            }
        }

        public static void LimparSessao()
        {
            if (CacheDados.Sessao != null && (CacheDados.Sessao.IsOpen || CacheDados.Sessao.IsConnected))
                CacheDados.Sessao.Clear();
        }


    }
}
