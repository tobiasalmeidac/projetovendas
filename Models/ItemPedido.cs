namespace ProjetoVendas.Models;

public class ItemPedido
{
    public int Id { get; init; }
    public required Pedido Pedido { get; init; }
    public required Produto Produto { get; init; }
    public int Quantidade { get; init; }
    public decimal PrecoUnitario { get; init; }
    public decimal Subtotal => Quantidade * PrecoUnitario;
}
