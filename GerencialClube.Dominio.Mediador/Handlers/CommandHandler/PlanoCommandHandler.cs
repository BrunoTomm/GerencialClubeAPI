using GerencialClube.Aplicacao.DTO.Response;
using GerencialClube.Aplicacao.Interfaces;
using GerencialClube.Dominio.Mediador.Commands;
using GerencialClube.Dominio.Mediador.Commands.Plano;
using MediatR;

namespace GerencialClube.Dominio.Mediador.Handlers
{
    public class PlanoCommandHandler :
        IRequestHandler<CriarPlanoCommand, PlanoResponse>,
        IRequestHandler<AtualizarPlanoCommand, Unit>,
        IRequestHandler<DeletarPlanoCommand, Unit>
    {
        private readonly IPlanoService _planoService;

        public PlanoCommandHandler(IPlanoService planoService)
        {
            _planoService = planoService;
        }

        public async Task<PlanoResponse> Handle(CriarPlanoCommand request, CancellationToken cancellationToken)
        {
            return await _planoService.CriarAsync(request.Plano);
        }

        public async Task<Unit> Handle(AtualizarPlanoCommand request, CancellationToken cancellationToken)
        {
            await _planoService.AtualizarAsync(request.Plano);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeletarPlanoCommand request, CancellationToken cancellationToken)
        {
            await _planoService.DeletarAsync(request.Id);
            return Unit.Value;
        }
    }
}
