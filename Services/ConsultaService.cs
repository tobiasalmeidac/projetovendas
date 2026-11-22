using System.Globalization;
using System.Text;
using ProjetoVendas.Data;
using ProjetoVendas.Models;

namespace ProjetoVendas.Services;

public record RankingProduto(string Produto, int Quantidade, decimal Valor);

public record RankingCliente(string Cliente, int QuantidadePedidos, decimal TotalComprado);

public class ConsultaService
{
    private readonly BancoEmMemoria _db;

    public ConsultaService(BancoEmMemoria db)
    {
        _db = db;
    }

    public Categoria? BuscarCategoria(string entrada)
    {
        if (int.TryParse(entrada, out int id))
            return _db.Categorias.FirstOrDefault(c => c.Id == id);

        string busca = Normalizar(entrada);
        return _db.Categorias.FirstOrDefault(c => Normalizar(c.Nome).Contains(busca));
    }

    public Cliente? BuscarCliente(string entrada)
    {
        if (int.TryParse(entrada, out int id))
            return _db.Clientes.FirstOrDefault(c => c.Id == id);

        string busca = Normalizar(entrada);
        return _db.Clientes.FirstOrDefault(c => Normalizar(c.Nome).Contains(busca));
    }

    public Pedido? BuscarPedido(int id)
        => _db.Pedidos.FirstOrDefault(p => p.Id == id);

    public List<Produto> ProdutosAcimaDe(decimal valor)
        => _db.Produtos
            .Where(p => p.Preco > valor)
            .OrderBy(p => p.Preco)
            .ToList();

    public decimal Faturamento()
        => _db.Pedidos.Sum(p => p.Total);

    public List<RankingProduto> RankingProdutos()
        => _db.Pedidos
            .SelectMany(p => p.Itens)
            .GroupBy(i => i.Produto.Nome)
            .Select(g => new RankingProduto(
                g.Key,
                g.Sum(i => i.Quantidade),
                g.Sum(i => i.Subtotal)))
            .OrderByDescending(x => x.Quantidade)
            .ThenByDescending(x => x.Valor)
            .ToList();

    public List<RankingCliente> RankingClientes()
        => _db.Clientes
            .Select(c => new RankingCliente(
                c.Nome,
                c.Pedidos.Count,
                c.Pedidos.Sum(p => p.Total)))
            .OrderByDescending(x => x.TotalComprado)
            .ToList();

    private static string Normalizar(string texto)
    {
        string decomposto = texto.Trim().Normalize(NormalizationForm.FormD);
        StringBuilder sb = new StringBuilder();

        foreach (char c in decomposto)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }

        return sb.ToString().ToLowerInvariant();
    }
}
