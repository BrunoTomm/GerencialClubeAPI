using System.Linq.Expressions;

namespace GerencialClube.Infra.Interface
{
    public interface IRepositorioBase
    {
        Task<T> AdicionarAsync<T>(T entidade) where T : class;
        Task<T> AtualizarAsync<T>(T entidade) where T : class;
        Task ExcluirAsync<T>(T entidade) where T : class;
        Task<T> ObterPorIdAsync<T>(Guid id) where T : class;
        Task<List<T>> ObterTodosAsync<T>() where T : class;
        Task<List<T>> BuscarAsync<T>(Expression<Func<T, bool>> predicado) where T : class;
        Task<List<T>> ObterTodosComIncludeAsync<T>(params Expression<Func<T, object>>[] includes) where T : class;
    }
}
