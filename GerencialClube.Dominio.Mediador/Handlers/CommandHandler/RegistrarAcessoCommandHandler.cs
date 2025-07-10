using GerencialClube.Aplicacao.DTO.Response;
using GerencialClube.Aplicacao.Interfaces;
using GerencialClube.Dominio.Mediador.Commands.RegistroAcesso;
using MediatR;

namespace GerencialClube.Dominio.Mediador.Handlers
{
    public class RegistrarAcessoCommandHandler : IRequestHandler<RegistrarAcessoCommand, RegistroAcessoResponse>
    {
        private readonly IAcessoService _acessoService;

        public RegistrarAcessoCommandHandler(IAcessoService acessoService)
        {
            _acessoService = acessoService;
        }

        public async Task<RegistroAcessoResponse> Handle(RegistrarAcessoCommand command, CancellationToken cancellationToken)
        {
            return await _acessoService.RegistrarAcessoAsync(command.Request);
        }
    }
}
