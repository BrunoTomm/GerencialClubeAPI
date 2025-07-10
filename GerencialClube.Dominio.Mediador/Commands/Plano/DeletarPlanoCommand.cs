using MediatR;

namespace GerencialClube.Dominio.Mediador.Commands
{
    public class DeletarPlanoCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public DeletarPlanoCommand(Guid id)
        {
            Id = id;
        }
    }
}
