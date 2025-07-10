using GerencialClube.Aplicacao.DTO.Request;
using GerencialClube.Aplicacao.DTO.Response;

namespace GerencialClube.Aplicacao.Interfaces
{
    public interface IAcessoService
    {
        Task<RegistroAcessoResponse> RegistrarAcessoAsync(RegistroAcessoRequest request);
        Task<List<RegistroAcessoResponse>> ObterAcessosPorSocioAsync(Guid socioId);
    }
}
