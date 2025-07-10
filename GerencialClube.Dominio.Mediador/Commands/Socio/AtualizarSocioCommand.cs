using GerencialClube.Aplicacao.DTO.Request.Update;
using MediatR;

namespace GerencialClube.Dominio.Mediador.Commands;

public class AtualizarSocioCommand : IRequest<Unit>
{
    public UpdateSocioRequest Cliente { get; }

    public AtualizarSocioCommand(UpdateSocioRequest cliente)
    {
        Cliente = cliente;
    }
}
