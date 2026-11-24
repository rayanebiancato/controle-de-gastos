# STACK.md

## Runtime e Linguagem
- **Linguagem:** C# 13
- **Framework:** .NET 9.0 (ASP.NET Core Web API)
- **IDE:** Visual Studio 2026 com GitHub Copilot Chat

## Banco de Dados e Persistência
- **ORM:** Entity Framework Core 9.0
- **Banco de Dados:** SQLite (arquivo local `gastos.db` para facilitar execução e avaliação sem configuração externa)

## Qualidade e Testes
- **Framework de Testes:** xUnit
- **Asserções:** FluentAssertions
- **Documentação da API:** Swagger / OpenAPI (Swashbuckle ou Scalar)

## Comandos Principais
```bash
# Restaurar pacotes e compilar a solução
dotnet build

# Executar todos os testes unitários
dotnet test

# Rodar a API localmente
dotnet run --project src/ControleGastos.Api
```