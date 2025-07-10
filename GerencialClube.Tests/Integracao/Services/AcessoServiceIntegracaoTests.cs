using GerencialClube.Aplicacao.DTO.Request;
using GerencialClube.Dominio.Enumeradores;
using GerencialClube.Tests.Integracao.Helpers;
using GerencialClube.Tests.Utils.Builders;
using Xunit;

namespace GerencialClube.Tests.Integracao.Services;

public class AcessoServiceIntegracaoTests
{
    [Fact]
    public async Task RegistrarAcessoAsync_DevePermitirAcesso_QuandoAreaPermitida()
    {
        // Arrange
        var helper = new IntegrationHelper();
        var areasPermitidas = new List<AreaClube> { AreaClube.Piscina, AreaClube.Restaurante };
        var socio = SocioBuilder.CriarSocio(areasPermitidas);

        await helper.PersistirSocioAsync(socio);

        var areaAcessar = AreaClube.Piscina;

        var request = new RegistroAcessoRequest
        {
            SocioId = socio.Id,
            Area = areaAcessar
        };

        // Act
        var resultado = await helper.AcessoService.RegistrarAcessoAsync(request);

        // Assert
        Assert.NotNull(resultado);
        Assert.True(resultado.Autorizado);
        Assert.Equal(areaAcessar, resultado.Area);
        Assert.Equal(socio.Id, resultado.SocioId);
        Assert.True((DateTime.UtcNow - resultado.DataHora).TotalSeconds < 5);
    }


    [Fact]
    public async Task RegistrarAcessoAsync_NaoDevePermitirAcesso_QuandoAreaNaoPermitida()
    {
        // Arrange
        var helper = new IntegrationHelper();
        var AreasPermitidas = new List<AreaClube> { AreaClube.Piscina, AreaClube.Academia };
        var areaNegada = AreaClube.Restaurante;

        var socio = SocioBuilder.CriarSocio(AreasPermitidas);
        await helper.PersistirSocioAsync(socio);

        var request = new RegistroAcessoRequest
        {
            SocioId = socio.Id,
            Area = areaNegada
        };

        // Act
        var resultado = await helper.AcessoService.RegistrarAcessoAsync(request);

        // Assert
        Assert.NotNull(resultado);
        Assert.False(resultado.Autorizado); 
        Assert.Equal(areaNegada, resultado.Area);
        Assert.Equal(socio.Id, resultado.SocioId);
        Assert.True((DateTime.UtcNow - resultado.DataHora).TotalSeconds < 5);
    }

}
