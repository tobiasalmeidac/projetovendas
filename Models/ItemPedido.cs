namespace ProjetoVendas.Models;

/// <summary>
/// Classe associativa que resolve a relação N:N entre Pedido e Produto.
/// Possui atributos próprios do vínculo: Quantidade e PrecoUnitario.
/// </summary>
public class ItemPedido
{
    public int Id { get; init; }
    public required Pedido Pedido { get; init; }
    public required Produto Produto { get; init; }
    public int Quantidade { get; init; }
    public decimal PrecoUnitario { get; init; }
    public decimal Subtotal => Quantidade * PrecoUnitario;
}
