using Bogus;
using GerencialClube.Dominio.Entidades;
using GerencialClube.Dominio.Enumeradores;

namespace GerencialClube.Tests.Utils.Builders;

public static class ContatoBuilder
{
    public static List<Contato> CriarContato(int quantidade = 1)
    {
        var faker = new Faker("pt_BR");

        return Enumerable.Range(0, quantidade)
            .Select(_ => new Contato(TipoContato.Email, faker.Internet.Email()))
            .ToList();
    }
}

