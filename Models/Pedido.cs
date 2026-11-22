namespace ProjetoVendas.Models;

public class Pedido
{
    private readonly List<ItemPedido> _itens = new();

    public int Id { get; init; }
    public DateTime Data { get; init; } = DateTime.Now;
    public required Cliente Cliente { get; init; }

    public IReadOnlyCollection<ItemPedido> Itens => _itens.AsReadOnly();

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
            PrecoUnitario = produto.Preco
        };

        _itens.Add(item);

        produto.ItensPedido.Add(item);
    }

    public void VincularAoCliente()
    {
        Cliente.AdicionarPedido(this);
    }
}
