using GerencialClube.Dominio.Repositorios;

public interface IRegistroAcessoRepository : IRepositorioBase<RegistroAcesso>
{
    Task<List<RegistroAcesso>> ObterPorSocioAsync(Guid socioId);
}
