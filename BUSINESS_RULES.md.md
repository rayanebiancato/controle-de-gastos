# BUSINESS_RULES.md

## Entidade: Gasto (Expense)
1. **Valor obrigatório:** Todo gasto deve ter um valor estritamente maior que zero (`Valor > 0`). Valores negativos ou zero devem ser rejeitados.
2. **Descrição:** A descrição é obrigatória, não pode ser vazia e deve ter entre 3 e 100 caracteres.
3. **Data do gasto:** A data não pode ser futura (deve ser menor ou igual à data/hora atual).
4. **Categorias permitidas:** Todo gasto deve pertencer a uma das seguintes categorias válidas:
   - `Alimentacao`
   - `Transporte`
   - `Moradia`
   - `Lazer`
   - `Saude`
   - `Outros`

## Orçamento Mensal e Alertas
5. **Limite mensal padrão:** Existe um teto mensal configurável (valor padrão de **R$ 3.000,00** caso não seja informado).
6. **Alerta de 80%:** Quando a soma dos gastos aprovados no mês atingir ou ultrapassar **80%** do limite mensal, o sistema deve sinalizar um alerta (`StatusOrcamento = "Alerta"`).
7. **Bloqueio por estouro de orçamento:** Gastos da categoria `Lazer` não podem ser registrados se o orçamento mensal já estiver estourado (soma mensal > 100% do limite). Demais categorias essenciais são permitidas, mas marcam o status como `"Estourado"`.

## Exclusão e Auditoria
8. **Registro de criação:** Todo gasto registra automaticamente a data/hora exata de criação (`CriadoEm`) em UTC.
9. **Exclusão:** Um gasto só pode ser excluído se tiver sido criado há menos de **7 dias**. Gastos mais antigos retornam erro de regra de negócio.