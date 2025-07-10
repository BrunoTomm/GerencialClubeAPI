using GerencialClube.Aplicacao.DTO.Request.Create;
using GerencialClube.Aplicacao.DTO.Request.Update;
using GerencialClube.Aplicacao.DTO.Response;

namespace GerencialClube.Aplicacao.Interfaces
{
    public interface ISocioService
    {
        Task<SocioResponse> CriarAsync(CreateSocioRequest request);
        Task AtualizarAsync(UpdateSocioRequest request);
        Task DeletarAsync(Guid id);
        Task<SocioResponse> ObterPorIdAsync(Guid id);
        Task<Socio> ObterEntidadePorIdAsync(Guid id);
        Task<List<SocioResponse>> ObterTodosAsync();
    }
}
