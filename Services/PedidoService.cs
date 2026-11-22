using ProjetoVendas.Data;
using ProjetoVendas.Models;

namespace ProjetoVendas.Services;

/// <summary>
/// Serviço que coordena várias entidades (Cliente, Pedido, Produto) e o banco em memória.
/// Regras que envolvem mais de uma entidade ficam aqui, e não dentro das entidades.
/// </summary>
public class PedidoService
{
    private readonly BancoEmMemoria _db;

    public PedidoService(BancoEmMemoria db)
    {
        _db = db;
    }

    public int ProximoIdPedido()
        => _db.Pedidos.Count == 0 ? 1001 : _db.Pedidos.Max(p => p.Id) + 1;

    public Pedido CriarPedido(int clienteId, int novoId)
    {
        if (_db.Pedidos.Any(p => p.Id == novoId))
            throw new InvalidOperationException($"Já existe um pedido com Id {novoId}.");

        Cliente cliente = _db.Clientes
            .First(c => c.Id == clienteId);

        Pedido pedido = new Pedido
        {
            Id = novoId,
            Cliente = cliente,
            Data = DateTime.Now
        };

        pedido.VincularAoCliente();
        _db.Pedidos.Add(pedido);
        return pedido;
    }

    public Pedido CriarPedido(int clienteId)
        => CriarPedido(clienteId, ProximoIdPedido());

    public void AdicionarItem(Pedido pedido, int produtoId, int quantidade)
    {
        Produto produto = _db.Produtos.FirstOrDefault(p => p.Id == produtoId)
            ?? throw new ArgumentException($"Produto {produtoId} não encontrado.");

        pedido.AdicionarItem(produto, quantidade);
    }
}
