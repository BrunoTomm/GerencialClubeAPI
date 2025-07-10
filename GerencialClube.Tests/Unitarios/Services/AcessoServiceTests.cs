using GerencialClube.Aplicacao.DTO.Request;
using GerencialClube.Aplicacao.Servicos;
using GerencialClube.Dominio.Enumeradores;
using GerencialClube.Dominio.Exceptions;
using GerencialClube.Tests.Unitarios.Mocks;
using GerencialClube.Tests.Utils.Builders;
using Moq;

namespace GerencialClube.Tests.Unitarios.Services;

public class AcessoServiceTests
{
    [Fact]
    public async Task RegistrarAcessoAsync_DevePermitirAcesso_QuandoAreaEstaNoPlano()
    {
        // Arrange
        var areasPermitidas = new List<AreaClube> { AreaClube.Piscina };
        var socio = SocioBuilder.CriarSocio(areasPermitidas);
        var helper = new MockHelper(socio);

        var service = new AcessoService(
            helper.SocioService,
            helper.RegistroRepo,
            helper.Logger,
            helper.Mapper
        );

        var areaAcessar = AreaClube.Piscina;

        var request = new RegistroAcessoRequest
        {
            SocioId = socio.Id,
            Area = areaAcessar
        };

        // Act
        var resultado = await service.RegistrarAcessoAsync(request);

        // Assert
        Assert.NotNull(resultado);
        Assert.True(resultado.Autorizado);
        Assert.Equal(areaAcessar, resultado.Area);
        Assert.Equal(socio.Id, resultado.SocioId);
        Assert.True((DateTime.UtcNow - resultado.DataHora).TotalSeconds < 5);

        helper.SocioServiceMock.Verify(s => s.ObterEntidadePorIdAsync(socio.Id), Times.Once);
        helper.RegistroRepoMock.Verify(r => r.AdicionarAsync(It.IsAny<RegistroAcesso>()), Times.Once);
    }
    [Fact]
    public async Task RegistrarAcessoAsync_DeveNegarAcesso_QuandoAreaNaoEstaNoPlano()
    {
        // Arrange
        var areasPermitidas = new List<AreaClube> { AreaClube.Piscina, AreaClube.Restaurante };
        var socio = SocioBuilder.CriarSocio(areasPermitidas);
        var helper = new MockHelper(socio);

        var service = new AcessoService(
            helper.SocioService,
            helper.RegistroRepo,
            helper.Logger,
            helper.Mapper
        );

        var areaAcessar = AreaClube.Academia;

        var request = new RegistroAcessoRequest
        {
            SocioId = socio.Id,
            Area = areaAcessar
        };

        // Act
        var resultado = await service.RegistrarAcessoAsync(request);

        // Assert
        Assert.NotNull(resultado);
        Assert.False(resultado.Autorizado);
        Assert.Equal(areaAcessar, resultado.Area);
        Assert.Equal(socio.Id, resultado.SocioId);
        Assert.True((DateTime.UtcNow - resultado.DataHora).TotalSeconds < 5);

        helper.SocioServiceMock.Verify(s => s.ObterEntidadePorIdAsync(socio.Id), Times.Once);
        helper.RegistroRepoMock.Verify(r => r.AdicionarAsync(It.IsAny<RegistroAcesso>()), Times.Once);
    }
    [Fact]
    public async Task RegistrarAcessoAsync_DeveLancarExcecao_QuandoSocioNaoForEncontrado()
    {
        // Arrange
        var helper = new MockHelper();
        var service = new AcessoService(
            helper.SocioService,
            helper.RegistroRepo,
            helper.Logger,
            helper.Mapper
        );

        var request = new RegistroAcessoRequest
        {
            SocioId = Guid.NewGuid(),
            Area = AreaClube.Piscina
        };

        // Act & Assert
        var ex = await Assert.ThrowsAsync<SocioException>(() =>
            service.RegistrarAcessoAsync(request));

        Assert.Equal("Sócio não encontrado.", ex.Message);
        helper.SocioServiceMock.Verify(s => s.ObterEntidadePorIdAsync(request.SocioId), Times.Once);
        helper.RegistroRepoMock.Verify(r => r.AdicionarAsync(It.IsAny<RegistroAcesso>()), Times.Never);
    }
}
