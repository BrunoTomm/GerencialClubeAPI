using GerencialClube.Aplicacao.DTO.Request.Create;
using GerencialClube.Aplicacao.DTO.Request.Update;
using GerencialClube.Aplicacao.DTO.Response;
using GerencialClube.Dominio.Mediador.Commands;
using GerencialClube.Dominio.Mediador.Queries;
using GerencialClube.Dominio.Mediador.Querys.Socio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerencialClube.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/socios")]
public class SocioController : ControllerBase
{
    private readonly IMediator _mediator;

    public SocioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cadastrar um sócio
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(SocioResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CriarAsync(
        [FromBody] CreateSocioRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CriarSocioCommand(request);
        var resultado = await _mediator.Send(command, cancellationToken);
        return CreatedAtRoute("ObterSocioPorId", new { id = resultado.Id }, resultado);
    }

    /// <summary>
    /// Obter sócio por ID
    /// </summary>
    [HttpGet("{id}", Name = "ObterSocioPorId")]
    [ProducesResponseType(typeof(SocioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new ObterSocioPorIdQuery(id);
        var resultado = await _mediator.Send(query, cancellationToken);

        return resultado is null
            ? NotFound(new { mensagem = "Sócio não encontrado." })
            : Ok(resultado);
    }

    /// <summary>
    /// Listar todos os sócios
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<SocioResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarTodosAsync(CancellationToken cancellationToken)
    {
        var query = new ListarSociosQuery();
        var resultado = await _mediator.Send(query, cancellationToken);
        return Ok(resultado);
    }

    /// <summary>
    /// Atualizar um sócio
    /// </summary>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AtualizarAsync(
        [FromBody] UpdateSocioRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AtualizarSocioCommand(request);
        await _mediator.Send(command, cancellationToken);
        return Ok(new { mensagem = "Sócio atualizado com sucesso!" });
    }

    /// <summary>
    /// Deletar um sócio
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeletarAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeletarSocioCommand(id);
        await _mediator.Send(command, cancellationToken);
        return Ok(new { mensagem = "Sócio deletado com sucesso!" });
    }
}
