using AutoMapper;
using GerencialClube.Aplicacao.DTO.Request.Create;
using GerencialClube.Aplicacao.DTO.Request.Update;
using GerencialClube.Aplicacao.DTO.Response;
using GerencialClube.Aplicacao.Interfaces;
using GerencialClube.Dominio.Exceptions;
using GerencialClube.Dominio.Repositorios;

namespace GerencialClube.Aplicacao.Servicos
{
    public class PlanoService : IPlanoService
    {
        private readonly IPlanoRepository _planoRepository;
        private readonly IMapper _mapper;

        public PlanoService(IPlanoRepository planoRepository, IMapper mapper)
        {
            _planoRepository = planoRepository;
            _mapper = mapper;
        }

        public async Task<PlanoResponse> CriarAsync(CreatePlanoRequest request)
        {
            var plano = new Plano(request.Nome, request.AreasPermitidas);
            var criado = await _planoRepository.AdicionarAsync(plano);
            return _mapper.Map<PlanoResponse>(criado);
        }

        public async Task AtualizarAsync(UpdatePlanoRequest request)
        {
            var planoExistente = await _planoRepository.ObterPorIdAsync(request.Id);
            if (planoExistente is null)
                throw new PlanoException("Plano não encontrado.");

            planoExistente.AtualizarAreasPermitidas(request.AreasPermitidas);
            planoExistente.AtualizarNome(request.Nome);
            await _planoRepository.AtualizarAsync(planoExistente);
        }

        public async Task DeletarAsync(Guid id)
        {
            var plano = await _planoRepository.ObterPorIdAsync(id);
            if (plano is null)
                throw new PlanoException("Plano não encontrado para exclusão.");

            await _planoRepository.RemoverAsync(plano);
        }

        public async Task<PlanoResponse> ObterPorIdAsync(Guid id)
        {
            var plano = await _planoRepository.ObterPorIdAsync(id);
            if (plano is null)
                throw new PlanoException("Plano não encontrado.");

            return _mapper.Map<PlanoResponse>(plano);
        }

        public async Task<Plano> ObterEntidadePorIdAsync(Guid id)
        {
            var plano = await _planoRepository.ObterPorIdAsync(id);

            if (plano == null)
                throw new PlanoException("Plano não encontrado.");

            return plano;
        }


        public async Task<List<PlanoResponse>> ObterTodosAsync()
        {
            var planos = await _planoRepository.ObterTodosAsync();
            return _mapper.Map<List<PlanoResponse>>(planos);
        }
    }
}
