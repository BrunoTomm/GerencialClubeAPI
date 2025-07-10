using GerencialClube.Aplicacao.DTO.Response;
using GerencialClube.Aplicacao.Interfaces;
using GerencialClube.Dominio.Mediador.Queries;
using MediatR;

namespace GerencialClube.Dominio.Mediador.Handlers
{
    public class PlanoQueryHandler :
        IRequestHandler<ObterPlanoPorIdQuery, PlanoResponse>,
        IRequestHandler<ListarPlanosQuery, List<PlanoResponse>>
    {
        private readonly IPlanoService _planoService;

        public PlanoQueryHandler(IPlanoService planoService)
        {
            _planoService = planoService;
        }

        public async Task<PlanoResponse> Handle(ObterPlanoPorIdQuery request, CancellationToken cancellationToken)
        {
            return await _planoService.ObterPorIdAsync(request.Id);
        }

        public async Task<List<PlanoResponse>> Handle(ListarPlanosQuery request, CancellationToken cancellationToken)
        {
            return await _planoService.ObterTodosAsync();
        }
    }
}
