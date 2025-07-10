using Bogus;
using GerencialClube.Dominio.Entidades;
using GerencialClube.Dominio.Enumeradores;

namespace GerencialClube.Tests.Utils.Builders;

public static class SocioBuilder
{
    public static Socio CriarSocio(List<AreaClube>? areasPermitidas = null)
    {
        var faker = new Faker("pt_BR");

        areasPermitidas ??= new List<AreaClube> { AreaClube.Piscina };

        var plano = PlanoBuilder.CriarPlano(areasPermitidas);

        var socio = new Socio(faker.Person.FullName, plano.Id);
        typeof(EntidadeBase).GetProperty("Id")!.SetValue(socio, Guid.NewGuid());

        socio.AtualizarPlano(plano);

        var endereco = EnderecoBuilder.CriarEndereco();
        socio.AtualizarEndereco(endereco);

        var contatos = new List<Contato>();
        contatos.AddRange(ContatoBuilder.CriarContato(2));
        socio.AtualizarContatos(contatos);

        return socio;
    }
}
