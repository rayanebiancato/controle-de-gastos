using ControleGastos.Application;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

[ApiController]
[Route("api/gastos")]
public sealed class GastosController(ExpenseService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(GastoResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Criar(CriarGastoRequest request, CancellationToken cancellationToken)
    {
        var response = await service.CriarAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Listar), new { mes = response.Data.Month, ano = response.Data.Year }, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ControleGastos.Domain.Gasto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar([FromQuery] int mes, [FromQuery] int ano, CancellationToken cancellationToken)
    {
        return Ok(await service.ListarAsync(mes, ano, cancellationToken));
    }

    [HttpGet("resumo")]
    [ProducesResponseType(typeof(ResumoMensalResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Resumo([FromQuery] int mes, [FromQuery] int ano, CancellationToken cancellationToken)
    {
        return Ok(await service.ResumoAsync(mes, ano, cancellationToken));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Excluir(Guid id, CancellationToken cancellationToken)
    {
        await service.ExcluirAsync(id, cancellationToken);
        return NoContent();
    }
}
