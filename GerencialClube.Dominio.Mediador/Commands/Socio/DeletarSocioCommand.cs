using MediatR;

namespace GerencialClube.Dominio.Mediador.Commands
{
    public class DeletarSocioCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public DeletarSocioCommand(Guid id)
        {
            Id = id;
        }
    }
}
