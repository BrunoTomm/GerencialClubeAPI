using GerencialClube.Dominio.Entidades;
using GerencialClube.Dominio.Repositorios;
using GerencialClube.Infra.Contextos;
using GerencialClube.Infra.Repositorios.Base;
using Microsoft.EntityFrameworkCore;

namespace GerencialClube.Infra.Repositorios
{
    public class RegistroAcessoRepository : RepositorioBase<RegistroAcesso>, IRegistroAcessoRepository
    {
        private readonly ContextoGerencialClube _contexto;

        public RegistroAcessoRepository(ContextoGerencialClube contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<RegistroAcesso>> ObterPorSocioAsync(Guid socioId)
        {
            return await _contexto.RegistrosAcesso
                .Where(r => r.SocioId == socioId)
                .OrderByDescending(r => r.DataHora)
                .ToListAsync();
        }
    }

}
