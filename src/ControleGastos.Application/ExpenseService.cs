using ControleGastos.Domain;

namespace ControleGastos.Application;

public sealed class ExpenseService
{
    private const decimal LimiteMensalPadrao = 3000m;
    private readonly IGastoRepository _repository;
    private readonly IClock _clock;
    private readonly decimal _limiteMensal;

    public ExpenseService(IGastoRepository repository, IClock clock, decimal limiteMensal = LimiteMensalPadrao)
    {
        if (limiteMensal <= 0) throw new ArgumentOutOfRangeException(nameof(limiteMensal));
        _repository = repository;
        _clock = clock;
        _limiteMensal = limiteMensal;
    }

    public async Task<GastoResponse> CriarAsync(CriarGastoRequest request, CancellationToken cancellationToken = default)
    {
        var agora = _clock.UtcNow;
        var totalAtual = await _repository.SumByMonthAsync(request.Data.Month, request.Data.Year, cancellationToken);
        if (request.Categoria == Categoria.Lazer && totalAtual > _limiteMensal)
            throw new BudgetExceededException("Gastos de Lazer estão bloqueados porque o orçamento mensal foi estourado.");

        var gasto = Gasto.Criar(request.Descricao, request.Valor, request.Data, request.Categoria, agora);
        await _repository.AddAsync(gasto, cancellationToken);
        return ToResponse(gasto, GetStatus(totalAtual + gasto.Valor));
    }

    public Task<IReadOnlyList<Gasto>> ListarAsync(int mes, int ano, CancellationToken cancellationToken = default)
    {
        ValidatePeriod(mes, ano);
        return _repository.ListByMonthAsync(mes, ano, cancellationToken);
    }

    public async Task<ResumoMensalResponse> ResumoAsync(int mes, int ano, CancellationToken cancellationToken = default)
    {
        ValidatePeriod(mes, ano);
        var total = await _repository.SumByMonthAsync(mes, ano, cancellationToken);
        var percentual = total / _limiteMensal * 100m;
        return new ResumoMensalResponse(mes, ano, _limiteMensal, total, _limiteMensal - total, percentual, GetStatus(total));
    }

    public async Task ExcluirAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var gasto = await _repository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Gasto não encontrado.");
        if (gasto.CriadoEm <= _clock.UtcNow.AddDays(-7))
            throw new DomainException("O gasto só pode ser excluído nos primeiros 7 dias após sua criação.");
        await _repository.DeleteAsync(gasto, cancellationToken);
    }

    private StatusOrcamento GetStatus(decimal total) => total > _limiteMensal
        ? StatusOrcamento.Estourado
        : total >= _limiteMensal * 0.8m ? StatusOrcamento.Alerta : StatusOrcamento.Normal;

    private static GastoResponse ToResponse(Gasto gasto, StatusOrcamento status) =>
        new(gasto.Id, gasto.Descricao, gasto.Valor, gasto.Data, gasto.Categoria, status.ToString());

    private static void ValidatePeriod(int mes, int ano)
    {
        if (mes is < 1 or > 12 || ano is < 1 or > 9999)
            throw new ArgumentException("Mês ou ano inválido.");
    }
}
