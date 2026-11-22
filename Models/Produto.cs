namespace ProjetoVendas.Models;

public class Produto
{
    public int Id { get; init; }
    public required string Nome { get; init; }

    public decimal Preco { get; private set; }

    public required Categoria Categoria { get; init; }

    public List<ItemPedido> ItensPedido { get; } = new();

    public Produto(decimal preco)
    {
        AlterarPreco(preco);
    }

    public void AlterarPreco(decimal novoPreco)
    {
        if (novoPreco <= 0)
            throw new ArgumentException("O preço deve ser maior que zero.");

        Preco = novoPreco;
    }

    public decimal CalcularPrecoComDesconto(decimal percentual)
    {
        return Preco - (Preco * percentual / 100m);
    }

    public static Produto CriarProduto(int id, string nome, decimal preco, Categoria categoria)
    {
        Produto produto = new Produto(preco)
        {
            Id = id,
            Nome = nome,
            Categoria = categoria
        };

        categoria.Produtos.Add(produto);
        return produto;
    }

    public override string ToString() => $"{Nome} - {Preco:C}";
}
