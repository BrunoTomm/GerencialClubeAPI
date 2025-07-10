using GerencialClube.Aplicacao.DTO.Response;
using MediatR;

namespace GerencialClube.Dominio.Mediador.Queries
{
    public class ListarPlanosQuery : IRequest<List<PlanoResponse>> { }
}
