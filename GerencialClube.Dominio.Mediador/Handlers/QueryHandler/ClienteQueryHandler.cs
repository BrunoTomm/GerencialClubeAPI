using GerencialClube.Aplicacao.DTO.Response;
using GerencialClube.Aplicacao.Interfaces;
using GerencialClube.Dominio.Mediador.Queries;
using GerencialClube.Dominio.Mediador.Querys.Socio;
using MediatR;

namespace GerencialClube.Dominio.Mediador.Handlers
{
    public class SocioQueryHandler :
        IRequestHandler<ObterSocioPorIdQuery, SocioResponse>,
        IRequestHandler<ListarSociosQuery, List<SocioResponse>>
    {
        private readonly ISocioService _socioService;

        public SocioQueryHandler(ISocioService socioService)
        {
            _socioService = socioService;
        }

        public async Task<SocioResponse> Handle(ObterSocioPorIdQuery request, CancellationToken cancellationToken)
        {
            var socio = await _socioService.ObterPorIdAsync(request.Id);

            return socio; 
        }

        public async Task<List<SocioResponse>> Handle(ListarSociosQuery request, CancellationToken cancellationToken)
        {
            return await _socioService.ObterTodosAsync();
        }
    }
}
