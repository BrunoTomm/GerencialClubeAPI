using GerencialClube.Aplicacao.DTO.Request.Create;
using GerencialClube.Aplicacao.DTO.Response;
using MediatR;

namespace GerencialClube.Dominio.Mediador.Commands
{
    public class CriarSocioCommand : IRequest<SocioResponse>
    {
        public CreateSocioRequest Cliente { get; }

        public CriarSocioCommand(CreateSocioRequest cliente)
        {
            Cliente = cliente;
        }
    }
}
