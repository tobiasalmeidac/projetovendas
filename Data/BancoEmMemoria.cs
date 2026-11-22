using ProjetoVendas.Models;

namespace ProjetoVendas.Data;

public class BancoEmMemoria
{
    public List<Categoria> Categorias { get; } = new();
    public List<Produto> Produtos { get; } = new();
    public List<Cliente> Clientes { get; } = new();
    public List<Pedido> Pedidos { get; } = new();
}
