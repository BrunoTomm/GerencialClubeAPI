using System.Linq.Expressions;

namespace GerencialClube.Dominio.Repositorios
{
    public interface IRepositorioBase<T> where T : class
    {
        Task<T> ObterPorIdAsync(Guid id);
        Task<List<T>> ObterTodosAsync();
        Task<List<T>> BuscarAsync(Expression<Func<T, bool>> predicado);
        Task<T> AdicionarAsync(T entidade);
        Task<T> AtualizarAsync(T entidade);
        Task RemoverAsync(T entidade);
    }
}
