using Bogus;
using GerencialClube.Dominio.Entidades;

namespace GerencialClube.Tests.Utils.Builders;

public static class EnderecoBuilder
{
    public static Endereco CriarEndereco()
    {
        var faker = new Faker("pt_BR");

        return new Endereco(
            cep: faker.Address.ZipCode(),
            logradouro: faker.Address.StreetName(),
            cidade: faker.Address.City(),
            numero: faker.Address.BuildingNumber(),
            complemento: faker.Address.SecondaryAddress()
        );
    }
}
