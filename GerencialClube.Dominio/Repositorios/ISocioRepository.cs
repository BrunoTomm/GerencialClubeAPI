using GerencialClube.Dominio.Entidades;

namespace GerencialClube.Dominio.Repositorios
{
    public interface ISocioRepository : IRepositorioBase<Socio>
    {
        Task<Socio> ObterPorIdComEnderecoEContatosAsync(Guid id);
        Task<Socio> ObterSocioPorIdAsync(Guid id);
        Task<Socio> ObterPorNomeECepAsync(string nome, string cep);
        Task<Socio> ObterSocioPorNomeEPlanoAsync(string nome, Guid planoId);
        Task<List<Socio>> ObterTodosSociosAsync();
    }
}
