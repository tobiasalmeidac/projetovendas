namespace ProjetoVendas.Models;

/// <summary>
/// Cliente da loja. Possui um endereço principal (1:1) e vários pedidos (1:N).
/// </summary>
public class Cliente
{
    private readonly List<Pedido> _pedidos = new();

    public int Id { get; init; }
    public required string Nome { get; init; }
    public string? Email { get; init; }

    // Relação 1:1 (opcional)
    public Endereco? EnderecoPrincipal { get; init; }

    // Navegação Cliente -> Pedidos, exposta somente para leitura.
    public IReadOnlyCollection<Pedido> Pedidos => _pedidos.AsReadOnly();

    // internal: apenas código do próprio projeto (o Pedido) consegue vincular um pedido ao cliente.
    internal void AdicionarPedido(Pedido pedido)
    {
        if (!_pedidos.Contains(pedido))
            _pedidos.Add(pedido);
    }

    public override string ToString() => Nome;
}
