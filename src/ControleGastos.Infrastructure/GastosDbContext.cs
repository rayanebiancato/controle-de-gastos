using ControleGastos.Domain;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure;

public sealed class GastosDbContext(DbContextOptions<GastosDbContext> options) : DbContext(options)
{
    public DbSet<Gasto> Gastos => Set<Gasto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var gasto = modelBuilder.Entity<Gasto>();
        gasto.HasKey(x => x.Id);
        gasto.Property(x => x.Descricao).HasMaxLength(100).IsRequired();
        gasto.Property(x => x.Valor).HasPrecision(18, 2).IsRequired();
        gasto.Property(x => x.Categoria).HasConversion<string>().IsRequired();
        gasto.Property(x => x.Data).IsRequired();
        gasto.Property(x => x.CriadoEm).IsRequired();
    }
}
