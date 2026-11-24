# ARCHITECTURE.md

## Visão Geral
O sistema de Controle de Gastos é uma Web API desenvolvida em C# / .NET seguindo os princípios da **Clean Architecture** (Arquitetura Limpa), garantindo separação clara de responsabilidades e facilidade para testes automatizados.

## Estrutura de Pastas e Projetos
```text
ControleGastos/
├── docs/                           # Apresentação do produto e materiais de apoio
├── src/
│   ├── ControleGastos.Domain/      # Entidades, enums, interfaces e exceções de domínio
│   ├── ControleGastos.Application/ # Casos de uso (Services), DTOs e validações
│   ├── ControleGastos.Infrastructure/ # Persistência (EF Core / SQLite) e repositórios
│   └── ControleGastos.Api/         # Controllers/Endpoints, middlewares e configuração DI
└── tests/
    └── ControleGastos.Tests/       # Testes unitários de regras de negócio e serviços
```

## Regras de Dependência
1. `ControleGastos.Api` depende de `Application` e `Infrastructure` (apenas para injeção de dependência).
2. `ControleGastos.Infrastructure` depende de `Domain` (e opcionalmente `Application`) para implementar as interfaces de repositório.
3. `ControleGastos.Application` depende exclusivamente de `Domain`.
4. `ControleGastos.Domain` **não possui dependências** de nenhum outro projeto ou framework externo (sem referências a Entity Framework ou ASP.NET).