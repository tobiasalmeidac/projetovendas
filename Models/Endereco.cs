namespace ProjetoVendas.Models;

public class Endereco
{
    public required string Logradouro { get; init; }
    public required string Cidade { get; init; }
    public required string Uf { get; init; }
    public string? Cep { get; init; }

    public override string ToString()
        => $"{Logradouro} - {Cidade}/{Uf}";
}
