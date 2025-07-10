using GerencialClube.Aplicacao.DTO.Request.Create;
using GerencialClube.Aplicacao.DTO.Request.Update;
using GerencialClube.Aplicacao.DTO.Response;

namespace GerencialClube.Aplicacao.Interfaces
{
    public interface IPlanoService
    {
        Task<PlanoResponse> CriarAsync(CreatePlanoRequest request);
        Task AtualizarAsync(UpdatePlanoRequest request);
        Task DeletarAsync(Guid id);
        Task<PlanoResponse> ObterPorIdAsync(Guid id);
        Task<Plano> ObterEntidadePorIdAsync(Guid id);
        Task<List<PlanoResponse>> ObterTodosAsync();
    }
}
