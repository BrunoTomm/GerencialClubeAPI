using GerencialClube.Aplicacao.DTO.Response;
using MediatR;

namespace GerencialClube.Dominio.Mediador.Queries.RegistroAcesso;

public record ObterAcessosPorSocioQuery(Guid SocioId) : IRequest<List<RegistroAcessoResponse>>;
