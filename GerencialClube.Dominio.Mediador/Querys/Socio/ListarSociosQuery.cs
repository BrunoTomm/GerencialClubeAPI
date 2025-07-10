using GerencialClube.Aplicacao.DTO.Response;
using MediatR;

namespace GerencialClube.Dominio.Mediador.Querys.Socio
{
    public class ListarSociosQuery : IRequest<List<SocioResponse>>
    {
        public ListarSociosQuery() { }
    }
}
