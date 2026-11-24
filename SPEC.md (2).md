# SPEC.md

## Feature: Gestão de Gastos e Resumo Mensal
**Objetivo:** Permitir o cadastro, listagem, exclusão e consulta do resumo mensal de gastos pessoais aplicando as regras de limite orçamentário.

## Entidade Principal (`Gasto`)
- `Id` (Guid)
- `Descricao` (string)
- `Valor` (decimal)
- `Data` (DateTime)
- `Categoria` (Enum: `Alimentacao`, `Transporte`, `Moradia`, `Lazer`, `Saude`, `Outros`)
- `CriadoEm` (DateTime)

## Endpoints da API

### 1. Registrar Novo Gasto
`POST /api/gastos`

**Request (JSON):**
```json
{
  "descricao": "Almoço executivo",
  "valor": 45.90,
  "data": "2026-11-24T12:30:00Z",
  "categoria": "Alimentacao"
}
```

**Response (`201 Created`):**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "descricao": "Almoço executivo",
  "valor": 45.90,
  "data": "2026-11-24T12:30:00Z",
  "categoria": "Alimentacao",
  "alertaOrcamento": "Normal"
}
```

**Erros Esperados:**
- `400 Bad Request`: Valor `<= 0`, descrição inválida, data futura ou categoria inexistente.
- `422 Unprocessable Entity`: Tentativa de cadastrar gasto na categoria `Lazer` com orçamento mensal já estourado.

---

### 2. Listar Gastos por Mês/Ano
`GET /api/gastos?mes=11&ano=2026`

**Response (`200 OK`):** Lista de gastos cadastrados no período informado.

---

### 3. Consultar Resumo Mensal
`GET /api/gastos/resumo?mes=11&ano=2026`

**Response (`200 OK`):**
```json
{
  "mes": 11,
  "ano": 2026,
  "limiteMensal": 3000.00,
  "totalGasto": 2500.00,
  "saldoRestante": 500.00,
  "percentualUtilizado": 83.33,
  "statusOrcamento": "Alerta"
}
```

---

### 4. Excluir Gasto
`DELETE /api/gastos/{id}`

**Response:** `204 No Content`  
**Erros:** `404 Not Found` (não encontrado) ou `422 Unprocessable Entity` (gasto com mais de 7 dias).

## Checklist de Entrega
- [ ] Criar solução e projetos seguindo `ARCHITECTURE.md` e `STACK.md`
- [ ] Implementar entidades, enums e exceções no `Domain`
- [ ] Implementar casos de uso e validações conforme `BUSINESS_RULES.md`
- [ ] Configurar EF Core com SQLite na camada `Infrastructure`
- [ ] Criar endpoints na `Api` e testar via Swagger
- [ ] Criar testes unitários no `ControleGastos.Tests` cobrindo as regras de negócio
- [ ] Salvar apresentação do produto na pasta `/docs`