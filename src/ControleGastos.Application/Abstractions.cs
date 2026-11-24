using ControleGastos.Domain;

namespace ControleGastos.Application;

public interface IClock
{
    DateTime UtcNow { get; }
}

public interface IGastoRepository
{
    Task AddAsync(Gasto gasto, CancellationToken cancellationToken = default);
    Task<Gasto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Gasto>> ListByMonthAsync(int month, int year, CancellationToken cancellationToken = default);
    Task<decimal> SumByMonthAsync(int month, int year, CancellationToken cancellationToken = default);
    Task DeleteAsync(Gasto gasto, CancellationToken cancellationToken = default);
}

public class ApplicationException : Exception
{
    public ApplicationException(string message) : base(message) { }
}

public sealed class NotFoundException : ApplicationException
{
    public NotFoundException(string message) : base(message) { }
}

public sealed class BudgetExceededException : ApplicationException
{
    public BudgetExceededException(string message) : base(message) { }
}
