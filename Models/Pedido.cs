namespace ProjetoVendas.Models;

/// <summary>
/// Pedido de um cliente. Composição com ItemPedido: os itens só fazem sentido dentro do pedido.
/// A relação N:N entre Pedido e Produto é materializada pela classe associativa ItemPedido.
/// </summary>
public class Pedido
{
    private readonly List<ItemPedido> _itens = new();

    public int Id { get; init; }
    public DateTime Data { get; init; } = DateTime.Now;
    public required Cliente Cliente { get; init; }

    // A lista é privada; o código externo só enxerga uma coleção somente leitura.
    public IReadOnlyCollection<ItemPedido> Itens => _itens.AsReadOnly();

    // Propriedade calculada: sempre reflete os itens atuais.
    public decimal Total => _itens.Sum(i => i.Subtotal);

    public void AdicionarItem(Produto produto, int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("Quantidade deve ser maior que zero.");

        ItemPedido item = new ItemPedido
        {
            Pedido = this,
            Produto = produto,
            Quantidade = quantidade,
            // O preço é "congelado" no momento da compra: mudanças futuras no produto não alteram o pedido.
            PrecoUnitario = produto.Preco
        };

        _itens.Add(item);

        // Mantém as duas pontas coerentes (Pedido -> Itens e Produto -> ItensPedido)
        produto.ItensPedido.Add(item);
    }

    public void VincularAoCliente()
    {
        Cliente.AdicionarPedido(this);
    }
}
