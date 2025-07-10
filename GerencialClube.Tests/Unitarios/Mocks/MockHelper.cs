using AutoMapper;
using GerencialClube.Aplicacao.DTO.Response;
using GerencialClube.Aplicacao.Interfaces;
using GerencialClube.Aplicacao.Servicos;
using GerencialClube.Dominio.Entidades;
using GerencialClube.Dominio.Repositorios;
using Microsoft.Extensions.Logging;
using Moq;

namespace GerencialClube.Tests.Unitarios.Mocks;

public class MockHelper
{
    public Mock<ISocioService> SocioServiceMock { get; private set; }
    public Mock<IRegistroAcessoRepository> RegistroRepoMock { get; private set; }
    public Mock<IMapper> MapperMock { get; private set; }
    public Mock<ILogger<AcessoService>> LoggerMock { get; private set; }

    public ISocioService SocioService => SocioServiceMock.Object;
    public IRegistroAcessoRepository RegistroRepo => RegistroRepoMock.Object;
    public IMapper Mapper => MapperMock.Object;
    public ILogger<AcessoService> Logger => LoggerMock.Object;

    public MockHelper(Socio socio)
    {
        CriarSocioServiceMock(socio);
        CriarRegistroRepoMock();
        CriarMapperMock();
        CriarLoggerMock();
    }

    public MockHelper()
    {
        CriarSocioServiceMockNulo();
        CriarRegistroRepoMock();
        CriarMapperMock();
        CriarLoggerMock();
    }

    private void CriarSocioServiceMock(Socio socio)
    {
        SocioServiceMock = new Mock<ISocioService>();
        SocioServiceMock.Setup(s => s.ObterEntidadePorIdAsync(socio.Id))
                        .ReturnsAsync(socio);
    }

    private void CriarSocioServiceMockNulo()
    {
        SocioServiceMock = new Mock<ISocioService>();
        SocioServiceMock.Setup(s => s.ObterEntidadePorIdAsync(It.IsAny<Guid>()))
                        .ReturnsAsync((Socio?)null);
    }

    private void CriarRegistroRepoMock()
    {
        RegistroRepoMock = new Mock<IRegistroAcessoRepository>();
        RegistroRepoMock.Setup(r => r.AdicionarAsync(It.IsAny<RegistroAcesso>()))
                        .ReturnsAsync((RegistroAcesso r) => r);
    }

    private void CriarMapperMock()
    {
        MapperMock = new Mock<IMapper>();
        MapperMock.Setup(m => m.Map<RegistroAcessoResponse>(It.IsAny<RegistroAcesso>()))
                  .Returns((RegistroAcesso r) => new RegistroAcessoResponse
                  {
                      SocioId = r.SocioId,
                      Area = r.Area,
                      DataHora = r.DataHora,
                      Autorizado = r.Autorizado
                  });
    }

    private void CriarLoggerMock()
    {
        LoggerMock = new Mock<ILogger<AcessoService>>();
    }
}
