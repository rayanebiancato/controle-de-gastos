using ControleGastos.Domain;

namespace ControleGastos.Application;

public sealed record CriarGastoRequest(string Descricao, decimal Valor, DateTime Data, Categoria Categoria);
public sealed record GastoResponse(Guid Id, string Descricao, decimal Valor, DateTime Data, Categoria Categoria, string AlertaOrcamento);
public sealed record ResumoMensalResponse(int Mes, int Ano, decimal LimiteMensal, decimal TotalGasto, decimal SaldoRestante, decimal PercentualUtilizado, StatusOrcamento StatusOrcamento);
