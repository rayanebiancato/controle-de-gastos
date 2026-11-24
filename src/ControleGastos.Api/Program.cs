using System.Text.Json.Serialization;
using ControleGastos.Api;
using ControleGastos.Application;
using ControleGastos.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ExpenseService>(provider => new ExpenseService(
    provider.GetRequiredService<IGastoRepository>(),
    provider.GetRequiredService<IClock>(),
    builder.Configuration.GetValue<decimal?>("LimiteMensal") ?? 3000m));
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("Gastos") ?? "Data Source=gastos.db");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<GastosDbContext>().Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
