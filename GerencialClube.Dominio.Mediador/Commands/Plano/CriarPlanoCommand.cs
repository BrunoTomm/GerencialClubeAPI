using GerencialClube.Aplicacao.DTO.Request.Create;
using GerencialClube.Aplicacao.DTO.Response;
using MediatR;

namespace GerencialClube.Dominio.Mediador.Commands.Plano
{
    public class CriarPlanoCommand : IRequest<PlanoResponse>
    {
        public CreatePlanoRequest Plano { get; }

        public CriarPlanoCommand(CreatePlanoRequest plano)
        {
            Plano = plano;
        }
    }
}
