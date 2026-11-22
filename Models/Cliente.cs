namespace ProjetoVendas.Models;

public class Cliente
{
    private readonly List<Pedido> _pedidos = new();

    public int Id { get; init; }
    public required string Nome { get; init; }
    public string? Email { get; init; }

    public Endereco? EnderecoPrincipal { get; init; }

    public IReadOnlyCollection<Pedido> Pedidos => _pedidos.AsReadOnly();

    internal void AdicionarPedido(Pedido pedido)
    {
        if (!_pedidos.Contains(pedido))
            _pedidos.Add(pedido);
    }

    public override string ToString() => Nome;
}
