using GerencialClube.Aplicacao.DTO.Response;
using MediatR;

namespace GerencialClube.Dominio.Mediador.Queries
{
    public class ObterPlanoPorIdQuery : IRequest<PlanoResponse>
    {
        public Guid Id { get; }

        public ObterPlanoPorIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
