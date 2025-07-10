using AutoMapper;
using GerencialClube.Aplicacao.DTO.Request.Create;
using GerencialClube.Aplicacao.DTO.Request.Update;
using GerencialClube.Aplicacao.DTO.Response;
using GerencialClube.Aplicacao.Interfaces;
using GerencialClube.Dominio.Entidades;
using GerencialClube.Dominio.Exceptions;
using GerencialClube.Dominio.Repositorios;
using Microsoft.Extensions.Logging;

namespace GerencialClube.Aplicacao.Servicos;

public class SocioService : ISocioService
{
    private readonly ISocioRepository _socioRepository;
    private readonly IPlanoService _planoService;
    private readonly IViaCepApiService _viaCepApiService;
    private readonly IMapper _mapper;
    private readonly ILogger<SocioService> _logger;

    public SocioService(
        ISocioRepository socioRepository,
        IPlanoService planoService,
        IViaCepApiService viaCepApiService,
        IMapper mapper,
        ILogger<SocioService> logger)
    {
        _socioRepository = socioRepository;
        _planoService = planoService;
        _viaCepApiService = viaCepApiService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<SocioResponse> CriarAsync(CreateSocioRequest request)
    {
        _logger.LogInformation("Iniciando criação de sócio.");

        await ValidarDuplicidadeSocioAsync(request);
        var plano = await ValidarPlanoExistenteAsync(request.PlanoId);

        var socio = new Socio(request.Nome, plano.Id);

        if (request.Contatos != null && request.Contatos.Any())
        {
            var contatos = request.Contatos
                .Select(c => _mapper.Map<Contato>(c))
                .ToList();

            socio.AtualizarContatos(contatos);
        }

        var endereco = await PreencherEnderecoViaCepAsync(request.Endereco);
        if (endereco != null)
            socio.AtualizarEndereco(endereco);

        await _socioRepository.AdicionarAsync(socio);

        _logger.LogInformation("Sócio criado com sucesso. ID: {Id}", socio.Id);

        return _mapper.Map<SocioResponse>(socio);
    }

    public async Task AtualizarAsync(UpdateSocioRequest request)
    {
        _logger.LogInformation("Iniciando atualização do sócio {Id}", request.Id);

        var socio = await _socioRepository.ObterSocioPorIdAsync(request.Id)
            ?? throw new SocioException("Sócio não encontrado.");

        socio.AtualizarNome(request.Nome);

        if (request.PlanoId != Guid.Empty)
        {
            var plano = await ValidarPlanoExistenteAsync(request.PlanoId);
            socio.AtualizarPlano(plano);
        }

        if (request.Endereco != null)
            socio.AtualizarEndereco(await PreencherEnderecoViaCepAsync(request.Endereco));

        var contatosAtualizados = request.Contatos?
            .Select(c => new Contato(c.Id, c.Tipo, c.Texto))
            .ToList() ?? new List<Contato>();

        ValidarContatos(contatosAtualizados, socio);
        socio.AtualizarContatos(contatosAtualizados);

        await _socioRepository.AtualizarAsync(socio);

        _logger.LogInformation("Sócio {Id} atualizado com sucesso.", request.Id);
    }

    public async Task DeletarAsync(Guid id)
    {
        _logger.LogInformation("Iniciando deleção do sócio {Id}", id);

        var socio = await _socioRepository.ObterPorIdAsync(id)
            ?? throw new SocioException("Sócio não encontrado.");

        await _socioRepository.RemoverAsync(socio);

        _logger.LogInformation("Sócio {Id} deletado com sucesso.", id);
    }

    public async Task<SocioResponse> ObterPorIdAsync(Guid id)
    {
        _logger.LogInformation("Buscando sócio por ID: {Id}", id);

        var socio = await _socioRepository.ObterSocioPorIdAsync(id);
        return socio == null ? null : _mapper.Map<SocioResponse>(socio);
    }
    public async Task<Socio> ObterEntidadePorIdAsync(Guid id)
    {
        _logger.LogInformation("Obtendo entidade Sócio por ID: {Id}", id);

        var socio = await _socioRepository.ObterSocioPorIdAsync(id);
        if (socio == null)
            throw new SocioException("Sócio não encontrado.");

        return socio;
    }

    public async Task<List<SocioResponse>> ObterTodosAsync()
    {
        _logger.LogInformation("Buscando todos os sócios.");
        var socios = await _socioRepository.ObterTodosSociosAsync();
        return _mapper.Map<List<SocioResponse>>(socios);
    }

    #region Metodos Privados
    private async Task ValidarDuplicidadeSocioAsync(CreateSocioRequest request)
    {
        var existente = await _socioRepository.ObterSocioPorNomeEPlanoAsync(request.Nome, request.PlanoId);
        if (existente != null)
        {
            _logger.LogWarning("Sócio duplicado. Nome: {Nome}, PlanoId: {PlanoId}", request.Nome, request.PlanoId);
            throw new SocioException($"Já existe um sócio com o nome '{request.Nome}' nesse plano.");
        }
    }

    private async Task<Plano> ValidarPlanoExistenteAsync(Guid planoId)
    {
        var plano = await _planoService.ObterEntidadePorIdAsync(planoId);

        if (plano == null)
        {
            _logger.LogWarning("Plano não encontrado. PlanoId: {PlanoId}", planoId);
            throw new SocioException("O plano informado não foi encontrado.");
        }

        return plano;
    }
    private void ValidarContatos(List<Contato> novos, Socio socio)
    {
        var invalidos = novos.Where(c => c.Id != Guid.Empty && !socio.Contatos.Any(e => e.Id == c.Id)).ToList();

        if (invalidos.Any())
        {
            var ids = string.Join(", ", invalidos.Select(c => c.Id));
            _logger.LogWarning("Contatos inválidos: {Ids}", ids);
            throw new SocioException($"Os seguintes IDs de contato não pertencem ao sócio: {ids}");
        }
    }

    private async Task<Endereco> PreencherEnderecoViaCepAsync(CreateEnderecoRequest request)
        => await PreencherEnderecoViaCepAsync(
            request,
            r => r.Cep,
            r => r.Numero,
            r => r.Complemento
        );

    private async Task<Endereco> PreencherEnderecoViaCepAsync(UpdateEnderecoRequest request)
        => await PreencherEnderecoViaCepAsync(
            request,
            r => r.Cep,
            r => r.Numero,
            r => r.Complemento
        );

    private async Task<Endereco> PreencherEnderecoViaCepAsync<T>(
        T request,
        Func<T, string> getCep,
        Func<T, string> getNumero,
        Func<T, string> getComplemento)
    {
        if (request == null || string.IsNullOrWhiteSpace(getCep(request)))
            return null;

        var cep = getCep(request);

        _logger.LogInformation("Consultando ViaCEP para o CEP: {Cep}", cep);

        var enderecoViaCep = await _viaCepApiService.ObterEnderecoPorCepAsync(cep);
        if (!enderecoViaCep.Sucesso)
        {
            _logger.LogWarning("Erro ViaCEP: {Erro}", enderecoViaCep.Erro);
            throw new SocioException(enderecoViaCep.Erro);
        }

        return new Endereco(
            cep: cep,
            logradouro: enderecoViaCep.Logradouro,
            cidade: enderecoViaCep.Localidade,
            numero: getNumero(request),
            complemento: getComplemento(request)
        );
    }
    #endregion
}
