namespace ControleGastos.Domain;

public sealed class Gasto
{
    private Gasto() { }

    private Gasto(Guid id, string descricao, decimal valor, DateTime data, Categoria categoria, DateTime criadoEm)
    {
        Id = id;
        Descricao = descricao;
        Valor = valor;
        Data = data;
        Categoria = categoria;
        CriadoEm = criadoEm;
    }

    public Guid Id { get; private set; }
    public string Descricao { get; private set; } = string.Empty;
    public decimal Valor { get; private set; }
    public DateTime Data { get; private set; }
    public Categoria Categoria { get; private set; }
    public DateTime CriadoEm { get; private set; }

    public static Gasto Criar(string descricao, decimal valor, DateTime data, Categoria categoria, DateTime agoraUtc)
    {
        if (valor <= 0) throw new DomainException("O valor deve ser maior que zero.");
        if (string.IsNullOrWhiteSpace(descricao) || descricao.Trim().Length is < 3 or > 100)
            throw new DomainException("A descrição deve ter entre 3 e 100 caracteres.");
        if (data.ToUniversalTime() > agoraUtc.ToUniversalTime())
            throw new DomainException("A data do gasto não pode ser futura.");
        if (!Enum.IsDefined(categoria))
            throw new DomainException("A categoria informada é inválida.");

        return new Gasto(Guid.NewGuid(), descricao.Trim(), valor, data, categoria, agoraUtc.ToUniversalTime());
    }
}
