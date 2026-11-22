namespace ProjetoVendas.Models;

/// <summary>
/// Categoria de produtos. Relação 1:N com Produto (uma categoria possui vários produtos).
/// </summary>
public class Categoria
{
    public int Id { get; init; }
    public required string Nome { get; init; }

    // Navegação Categoria -> Produtos (lado "1" da relação 1:N)
    public List<Produto> Produtos { get; } = new();

    public override string ToString() => Nome;
}
