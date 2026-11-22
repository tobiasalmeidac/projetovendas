namespace ProjetoVendas.Models;

/// <summary>
/// Produto à venda. Pertence a uma Categoria (N:1) e aparece em vários ItemPedido (1:N).
/// </summary>
public class Produto
{
    public int Id { get; init; }
    public required string Nome { get; init; }

    // Encapsulamento: o preço só pode ser alterado pelo método AlterarPreco (que valida a regra).
    public decimal Preco { get; private set; }

    // Navegação Produto -> Categoria
    public required Categoria Categoria { get; init; }

    // Navegação Produto -> ItensPedido (permite descobrir em quais pedidos o produto apareceu)
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

    /// <summary>
    /// Método de fábrica: cria o produto E já o adiciona na coleção da categoria,
    /// mantendo as duas pontas da navegação bidirecional coerentes.
    /// </summary>
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
