using ProjetoVendas.Data;
using ProjetoVendas.Models;
using ProjetoVendas.Services;

namespace ProjetoVendas.Exercicios;

public static class DemonstracaoModulos7e8
{
    public static void Executar()
    {
        Console.WriteLine("=== Módulos 7 e 8: navegação, consultas e alterações ===");
        BancoEmMemoria db = DadosVirtuais.Criar();
        ConsultaService consultas = new ConsultaService(db);

        Console.WriteLine("--- Produto -> Categoria ---");
        Produto produto = db.Produtos.First();
        Console.WriteLine($"{produto.Nome} pertence à categoria {produto.Categoria.Nome}");

        Console.WriteLine("--- Categoria -> Produtos ---");
        Categoria categoria = db.Categorias.First(c => c.Nome == "Informática");
        Console.WriteLine($"Categoria: {categoria.Nome}");
        foreach (Produto p in categoria.Produtos)
            Console.WriteLine($"  - {p.Nome}");

        Console.WriteLine("--- Cliente -> Pedidos -> Itens -> Produto ---");
        Cliente cliente = db.Clientes.First(c => c.Nome.StartsWith("Tobias"));
        Console.WriteLine($"Cliente: {cliente.Nome}");
        foreach (Pedido pedido in cliente.Pedidos)
        {
            Console.WriteLine($"  Pedido {pedido.Id} - {pedido.Data:d}");
            foreach (ItemPedido item in pedido.Itens)
                Console.WriteLine($"    {item.Produto.Nome} x {item.Quantidade} = {item.Subtotal:C}");
            Console.WriteLine($"  Total: {pedido.Total:C}");
        }

        Console.WriteLine("--- Produto -> ItensPedido -> Pedidos ---");
        Produto mouse = db.Produtos.First(p => p.Nome.Contains("Mouse"));
        Console.WriteLine($"Produto: {mouse.Nome}");
        foreach (ItemPedido item in mouse.ItensPedido)
            Console.WriteLine($"  Pedido {item.Pedido.Id} - Cliente: {item.Pedido.Cliente.Nome}");

        Console.WriteLine("--- Consultas LINQ ---");
        Console.WriteLine("Produtos acima de R$ 100,00:");
        foreach (Produto p in consultas.ProdutosAcimaDe(100m))
            Console.WriteLine($"  {p}");
        Console.WriteLine($"Faturamento geral: {consultas.Faturamento():C}");

        Console.WriteLine("--- Cadastrando um novo cliente ---");
        Cliente novoCliente = new Cliente
        {
            Id = 6,
            Nome = "Carla Mendes",
            Email = "carla@exemplo.local",
            EnderecoPrincipal = new Endereco
            {
                Logradouro = "Rua B, 50",
                Cidade = "Barra Mansa",
                Uf = "RJ"
            }
        };
        db.Clientes.Add(novoCliente);
        Console.WriteLine($"Clientes cadastrados: {db.Clientes.Count}");

        Console.WriteLine("--- Criando um pedido para o novo cliente ---");
        Produto teclado = db.Produtos.First(p => p.Id == 2);
        Pedido novoPedido = new Pedido { Id = 1007, Data = DateTime.Now, Cliente = novoCliente };
        novoPedido.AdicionarItem(teclado, 2);
        novoPedido.VincularAoCliente();
        db.Pedidos.Add(novoPedido);
        Console.WriteLine($"Pedido {novoPedido.Id} de {novoCliente.Nome}: total {novoPedido.Total:C}");

        Console.WriteLine("--- Localizando e alterando o preço de um produto ---");
        Produto? alvo = db.Produtos.FirstOrDefault(p => p.Id == 1);
        if (alvo is not null)
        {
            decimal precoAntigo = alvo.Preco;
            alvo.AlterarPreco(99.90m);
            Console.WriteLine($"{alvo.Nome}: de {precoAntigo:C} para {alvo.Preco:C}");

            ItemPedido itemAntigo = alvo.ItensPedido.First();
            Console.WriteLine($"Item do pedido {itemAntigo.Pedido.Id} continua com preço unitário de {itemAntigo.PrecoUnitario:C}");
        }

        Console.WriteLine("--- Regra de negócio protegida pelo encapsulamento ---");
        try
        {
            teclado.AlterarPreco(-10m);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Bloqueado: {ex.Message}");
        }

        Console.WriteLine();
    }
}
