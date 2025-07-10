using GerencialClube.Aplicacao.DTO.Response;
using MediatR;

namespace GerencialClube.Dominio.Mediador.Queries
{
    public class ObterSocioPorIdQuery : IRequest<SocioResponse>
    {
        public Guid Id { get; }

        public ObterSocioPorIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
