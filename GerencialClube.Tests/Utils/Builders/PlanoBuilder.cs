using Bogus;
using GerencialClube.Dominio.Entidades;
using GerencialClube.Dominio.Enumeradores;

namespace GerencialClube.Tests.Utils.Builders;

public static class PlanoBuilder
{
    public static Plano CriarPlano(List<AreaClube>? areasPermitidas = null)
    {
        var faker = new Faker("pt_BR");

        var nomePlano = faker.Commerce.ProductName();
        areasPermitidas ??= new List<AreaClube> { AreaClube.Piscina };

        var plano = new Plano(nomePlano, areasPermitidas);

        typeof(EntidadeBase)
            .GetProperty("Id")!
            .SetValue(plano, Guid.NewGuid());

        return plano;
    }
}
