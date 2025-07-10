using GerencialClube.Aplicacao.DTO.Request;
using GerencialClube.Aplicacao.DTO.Response;
using GerencialClube.Dominio.Mediador.Commands.RegistroAcesso;
using GerencialClube.Dominio.Mediador.Queries.RegistroAcesso;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerencialClube.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/acessos")]
public class AcessoController : ControllerBase
{
    private readonly IMediator _mediator;

    public AcessoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Registrar tentativa de acesso de um sócio a uma área do clube 
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(RegistroAcessoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegistrarAcessoAsync(
        [FromBody] RegistroAcessoRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegistrarAcessoCommand(request);
        var resultado = await _mediator.Send(command, cancellationToken);
        return Ok(resultado);
    }

    /// <summary>
    /// Obter tentativas de acesso de um sócio
    /// </summary>
    [HttpGet("socio/{socioId}")]
    [ProducesResponseType(typeof(List<RegistroAcessoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterAcessosPorSocioAsync(
        [FromRoute] Guid socioId,
        CancellationToken cancellationToken)
    {
        var query = new ObterAcessosPorSocioQuery(socioId);
        var resultado = await _mediator.Send(query, cancellationToken);

        if (resultado == null || !resultado.Any())
            return NotFound(new { mensagem = "Nenhum registro de acesso encontrado para este sócio." });

        return Ok(resultado);
    }
}
