using GerencialClube.Aplicacao.DTO.Response;
using GerencialClube.Aplicacao.Interfaces;
using GerencialClube.Dominio.Mediador.Commands;
using MediatR;

namespace GerencialClube.Dominio.Mediador.Handlers
{
    public class SocioCommandHandler :
        IRequestHandler<CriarSocioCommand, SocioResponse>,
        IRequestHandler<AtualizarSocioCommand, Unit>,
        IRequestHandler<DeletarSocioCommand, Unit>
    {
        private readonly ISocioService _clienteService;

        public SocioCommandHandler(ISocioService clienteService)
        {
            _clienteService = clienteService;
        }

        public async Task<SocioResponse> Handle(CriarSocioCommand request, CancellationToken cancellationToken)
        {
            return await _clienteService.CriarAsync(request.Cliente);
        }

        public async Task<Unit> Handle(AtualizarSocioCommand request, CancellationToken cancellationToken)
        {
            await _clienteService.AtualizarAsync(request.Cliente);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeletarSocioCommand request, CancellationToken cancellationToken)
        {
            await _clienteService.DeletarAsync(request.Id);
            return Unit.Value;
        }
    }
}
