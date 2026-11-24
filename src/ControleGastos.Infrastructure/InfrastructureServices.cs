using ControleGastos.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ControleGastos.Infrastructure;

public sealed class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<GastosDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<IGastoRepository, GastoRepository>();
        services.AddSingleton<IClock, SystemClock>();
        return services;
    }
}
