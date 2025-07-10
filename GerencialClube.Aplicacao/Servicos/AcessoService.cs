using AutoMapper;
using GerencialClube.Aplicacao.DTO.Request;
using GerencialClube.Aplicacao.DTO.Response;
using GerencialClube.Aplicacao.Interfaces;
using GerencialClube.Dominio.Exceptions;
using Microsoft.Extensions.Logging;

namespace GerencialClube.Aplicacao.Servicos;

public class AcessoService : IAcessoService
{
    private readonly ISocioService _socioService;
    private readonly IRegistroAcessoRepository _registroRepository;
    private readonly ILogger<AcessoService> _logger;
    private readonly IMapper _mapper;

    public AcessoService(
        ISocioService socioService,
        IRegistroAcessoRepository registroRepository,
        ILogger<AcessoService> logger,
        IMapper mapper)
    {
        _socioService = socioService;
        _registroRepository = registroRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<RegistroAcessoResponse> RegistrarAcessoAsync(RegistroAcessoRequest request)
    {
        var socio = await _socioService.ObterEntidadePorIdAsync(request.SocioId);

        if (socio == null)
            throw new SocioException("Sócio não encontrado.");

        var autorizado = socio.Plano.AreasPermitidas.Contains(request.Area);

        var registro = new RegistroAcesso(request.SocioId, request.Area, autorizado);
        await _registroRepository.AdicionarAsync(registro);

        _logger.LogInformation("Registro de acesso: Sócio {Id}, Área {Area}, Autorizado: {Autorizado}",
            request.SocioId, request.Area, autorizado);

        return _mapper.Map<RegistroAcessoResponse>(registro);
    }

    public async Task<List<RegistroAcessoResponse>> ObterAcessosPorSocioAsync(Guid socioId)
    {
        var acessos = await _registroRepository.ObterPorSocioAsync(socioId);
        return _mapper.Map<List<RegistroAcessoResponse>>(acessos);
    }
}
