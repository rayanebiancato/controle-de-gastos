using ControleGastos.Application;
using ControleGastos.Domain;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure;

public sealed class GastoRepository(GastosDbContext db) : IGastoRepository
{
    public async Task AddAsync(Gasto gasto, CancellationToken cancellationToken = default)
    {
        await db.Gastos.AddAsync(gasto, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }

    public Task<Gasto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        db.Gastos.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Gasto>> ListByMonthAsync(int month, int year, CancellationToken cancellationToken = default) =>
        await db.Gastos.AsNoTracking().Where(x => x.Data.Month == month && x.Data.Year == year)
            .OrderBy(x => x.Data).ToListAsync(cancellationToken);

    public async Task<decimal> SumByMonthAsync(int month, int year, CancellationToken cancellationToken = default) =>
        await db.Gastos.Where(x => x.Data.Month == month && x.Data.Year == year)
            .SumAsync(x => (decimal?)x.Valor, cancellationToken) ?? 0m;

    public async Task DeleteAsync(Gasto gasto, CancellationToken cancellationToken = default)
    {
        db.Gastos.Remove(gasto);
        await db.SaveChangesAsync(cancellationToken);
    }
}
