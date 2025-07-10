using GerencialClube.Dominio.Entidades;
using GerencialClube.Dominio.Repositorios;
using GerencialClube.Infra.Contextos;
using GerencialClube.Infra.Repositorios.Base;
using Microsoft.EntityFrameworkCore;

namespace GerencialClube.Infra.Repositorios
{
    public class SocioRepository : RepositorioBase<Socio>, ISocioRepository
    {
        private readonly ContextoGerencialClube _contexto;

        public SocioRepository(ContextoGerencialClube contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<Socio> ObterPorIdComEnderecoEContatosAsync(Guid id)
        {
            return await _contexto.Socios
                .Include(s => s.Endereco)
                .Include(s => s.Contatos)
                .Include(s => s.Plano)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Socio> ObterSocioPorIdAsync(Guid id)
        {
            return await _contexto.Socios
                .Include(s => s.Endereco)
                .Include(s => s.Contatos)
                .Include(s => s.Plano)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Socio> ObterPorNomeECepAsync(string nome, string cep)
        {
            return await _contexto.Socios
                .Include(s => s.Endereco)
                .FirstOrDefaultAsync(s =>
                    s.Nome.ToLower() == nome.ToLower() &&
                    s.Endereco != null &&
                    s.Endereco.Cep == cep);
        }

        public async Task<Socio> ObterSocioPorNomeEPlanoAsync(string nome, Guid planoId)
        {
            return await _contexto.Socios
                .FirstOrDefaultAsync(s =>
                    s.Nome.ToLower() == nome.ToLower() &&
                    s.PlanoId == planoId);
        }

        public async Task<List<Socio>> ObterTodosSociosAsync()
        {
            return await _contexto.Socios
                .Include(s => s.Endereco)
                .Include(s => s.Contatos)
                .Include(s => s.Plano)
                .ToListAsync();
        }
    }
}
