using GerencialClube.Dominio.Repositorios;
using GerencialClube.Infra.Contextos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GerencialClube.Infra.Repositorios.Base
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        private readonly ContextoGerencialClube _contexto;

        public RepositorioBase(ContextoGerencialClube contexto)
        {
            _contexto = contexto;
        }

        public async Task<T> AdicionarAsync(T entidade)
        {
            await _contexto.Set<T>().AddAsync(entidade);
            await _contexto.SaveChangesAsync();
            return entidade;
        }

        public async Task<T> AtualizarAsync(T entidade)
        {
            _contexto.Entry(entidade).State = EntityState.Modified;

            await _contexto.SaveChangesAsync();
            return entidade;
        }

        public async Task RemoverAsync(T entidade)
        {
            _contexto.Set<T>().Remove(entidade);
            await _contexto.SaveChangesAsync();
        }

        public async Task<T> ObterPorIdAsync(Guid id)
        {
            return await _contexto.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> ObterTodosAsync()
        {
            return await _contexto.Set<T>().ToListAsync();
        }

        public async Task<List<T>> BuscarAsync(Expression<Func<T, bool>> predicado)
        {
            return await _contexto.Set<T>().Where(predicado).ToListAsync();
        }

        public async Task<List<T>> ObterTodosComIncludeAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _contexto.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }
    }
}
