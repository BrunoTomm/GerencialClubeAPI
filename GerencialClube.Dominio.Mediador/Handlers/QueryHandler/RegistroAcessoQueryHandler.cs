using GerencialClube.Aplicacao.DTO.Response;
using GerencialClube.Aplicacao.Interfaces;
using GerencialClube.Dominio.Mediador.Queries.RegistroAcesso;
using MediatR;

public class RegistroAcessoQueryHandler : IRequestHandler<ObterAcessosPorSocioQuery, List<RegistroAcessoResponse>>
{
    private readonly IAcessoService _acessoService;

    public RegistroAcessoQueryHandler(IAcessoService acessoService)
    {
        _acessoService = acessoService;
    }

    public async Task<List<RegistroAcessoResponse>> Handle(ObterAcessosPorSocioQuery request, CancellationToken cancellationToken)
    {
        return await _acessoService.ObterAcessosPorSocioAsync(request.SocioId);
    }
}
