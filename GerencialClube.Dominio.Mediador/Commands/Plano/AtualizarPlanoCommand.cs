using GerencialClube.Aplicacao.DTO.Request.Update;
using MediatR;

namespace GerencialClube.Dominio.Mediador.Commands
{
    public class AtualizarPlanoCommand : IRequest<Unit>
    {
        public UpdatePlanoRequest Plano { get; }

        public AtualizarPlanoCommand(UpdatePlanoRequest plano)
        {
            Plano = plano;
        }
    }
}
