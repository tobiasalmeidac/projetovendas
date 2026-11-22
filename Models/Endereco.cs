namespace ProjetoVendas.Models;

/// <summary>
/// Endereço principal do cliente (relação 1:1 com Cliente).
/// </summary>
public class Endereco
{
    public required string Logradouro { get; init; }
    public required string Cidade { get; init; }
    public required string Uf { get; init; }
    public string? Cep { get; init; }

    public override string ToString()
        => $"{Logradouro} - {Cidade}/{Uf}";
}
