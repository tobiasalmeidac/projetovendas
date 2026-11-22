namespace ProjetoVendas.Models;

public class Categoria
{
    public int Id { get; init; }
    public required string Nome { get; init; }

    public List<Produto> Produtos { get; } = new();

    public override string ToString() => Nome;
}
