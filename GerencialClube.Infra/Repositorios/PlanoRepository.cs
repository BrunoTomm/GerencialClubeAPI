using GerencialClube.Dominio.Repositorios;
using GerencialClube.Infra.Contextos;
using GerencialClube.Infra.Repositorios.Base;
using Microsoft.EntityFrameworkCore;

namespace GerencialClube.Infra.Repositorios
{
    public class PlanoRepository : RepositorioBase<Plano>, IPlanoRepository
    {
        private readonly ContextoGerencialClube _contexto;

        public PlanoRepository(ContextoGerencialClube contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<Socio> ObterPorIdComEnderecoEContatosAsync(Guid id)
        {
            return await _contexto.Socios
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Socio>> ObterTodosClientesAsync()
        {
            return await _contexto.Socios
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .ToListAsync();
        }

        public async Task<Socio> ObterClientePorIdAsync(Guid id)
        {
            return await _contexto.Socios
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Socio> ObterPorNomeECepAsync(string nome, string cep)
        {
            return await _contexto.Socios
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c =>
                    c.Nome.ToLower() == nome.ToLower() &&
                    c.Endereco != null &&
                    c.Endereco.Cep == cep);
        }
    }
}
