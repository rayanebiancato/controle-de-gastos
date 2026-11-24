# Controle de Gastos

Web API para registrar gastos pessoais, consultar gastos por mês, acompanhar o orçamento mensal e excluir registros recentes.

## Funcionalidades

- Cadastro de gastos com validação de valor, descrição, data e categoria.
- Limite mensal configurável, com alerta a partir de 80%.
- Bloqueio de novos gastos de `Lazer` quando o mês já está estourado.
- Resumo mensal com total, saldo, percentual e status.
- Exclusão permitida somente nos primeiros sete dias.
- Persistência local em SQLite (`gastos.db`).
- Swagger disponível na execução da API.

## Execução

```bash
dotnet restore
dotnet build
dotnet test
dotnet run --project src/ControleGastos.Api
```

A documentação interativa ficará disponível em `/swagger`.

## Organização

A solução segue Clean Architecture:

- `Domain`: entidades, enumerações e regras invariantes.
- `Application`: contratos, DTOs e casos de uso.
- `Infrastructure`: EF Core, SQLite e repositórios.
- `Api`: endpoints HTTP, composição e tratamento de erros.
- `tests`: testes unitários das regras e serviços.
