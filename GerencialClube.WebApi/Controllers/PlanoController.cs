using GerencialClube.Aplicacao.DTO.Request.Create;
using GerencialClube.Aplicacao.DTO.Request.Update;
using GerencialClube.Aplicacao.DTO.Response;
using GerencialClube.Dominio.Mediador.Commands;
using GerencialClube.Dominio.Mediador.Commands.Plano;
using GerencialClube.Dominio.Mediador.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerencialClube.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/planos")]
public class PlanoController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlanoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Criar um novo plano
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(PlanoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CriarAsync(
        [FromBody] CreatePlanoRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CriarPlanoCommand(request);
        var resultado = await _mediator.Send(command, cancellationToken);
        return CreatedAtRoute("ObterPlanoPorId", new { id = resultado.Id }, resultado);
    }

    /// <summary>
    /// Obter plano por ID
    /// </summary>
    [HttpGet("{id}", Name = "ObterPlanoPorId")]
    [ProducesResponseType(typeof(PlanoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new ObterPlanoPorIdQuery(id);
        var resultado = await _mediator.Send(query, cancellationToken);

        return resultado is null
            ? NotFound("Plano não encontrado.")
            : Ok(resultado);
    }

    /// <summary>
    /// Listar todos os planos
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<PlanoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarAsync(CancellationToken cancellationToken)
    {
        var query = new ListarPlanosQuery();
        var resultado = await _mediator.Send(query, cancellationToken);
        return Ok(resultado);
    }

    /// <summary>
    /// Atualizar um plano
    /// </summary>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AtualizarAsync(
        [FromBody] UpdatePlanoRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AtualizarPlanoCommand(request);
        await _mediator.Send(command, cancellationToken);
        return Ok(new { mensagem = "Plano atualizado com sucesso!" });
    }

    /// <summary>
    /// Deletar um plano
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletarAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeletarPlanoCommand(id);
        await _mediator.Send(command, cancellationToken);
        return Ok(new { mensagem = "Plano deletado com sucesso!" });
    }
}
