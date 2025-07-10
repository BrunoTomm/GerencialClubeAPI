using AutoMapper;
using GerencialClube.Aplicacao.Interfaces;
using GerencialClube.Aplicacao.Servicos;
using GerencialClube.Dominio.Repositorios;
using GerencialClube.Infra.Configuracoes;
using GerencialClube.Infra.Contextos;
using GerencialClube.Infra.Interfaces;
using GerencialClube.Infra.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace GerencialClube.Tests.Integracao.Helpers;

public class IntegrationHelper
{
    public ContextoGerencialClube Context { get; }
    public ISocioRepository SocioRepo { get; }
    public IRegistroAcessoRepository RegistroRepo { get; }
    public IPlanoRepository PlanoRepo { get; }
    public IPlanoService PlanoService { get; }
    public IViaCepApiService ViaCepService { get; }
    public ISocioService SocioService { get; }
    public IAcessoService AcessoService { get; }

    public IMapper Mapper { get; }
    public ILogger<SocioService> SocioLogger { get; }
    public ILogger<AcessoService> AcessoLogger { get; }

    public IntegrationHelper()
    {
        var options = new DbContextOptionsBuilder<ContextoGerencialClube>()
            .UseInMemoryDatabase($"Db_{Guid.NewGuid()}")
            .Options;

        Context = new ContextoGerencialClube(options);

        SocioRepo = new SocioRepository(Context);
        RegistroRepo = new RegistroAcessoRepository(Context);
        PlanoRepo = new PlanoRepository(Context);

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(AcessoService).Assembly);
        });
        Mapper = mapperConfig.CreateMapper();

        SocioLogger = new LoggerFactory().CreateLogger<SocioService>();
        AcessoLogger = new LoggerFactory().CreateLogger<AcessoService>();

        var optionsMock = new Mock<IOptions<ApiSettings>>();
        optionsMock.Setup(o => o.Value).Returns(new ApiSettings { ViaCepBaseUrl = "https://viacep.com.br/ws" });

        ViaCepService = new ViaCepApiService(new HttpClient(), optionsMock.Object);
        PlanoService = new PlanoService(PlanoRepo, Mapper);
        SocioService = new SocioService(SocioRepo, PlanoService, ViaCepService, Mapper, SocioLogger);
        AcessoService = new AcessoService(SocioService, RegistroRepo, AcessoLogger, Mapper);
    }

    public async Task PersistirSocioAsync(Socio socio)
    {
        Context.Planos.Add(socio.Plano);
        Context.Socios.Add(socio);
        await Context.SaveChangesAsync();
    }
}
