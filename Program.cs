using System.Globalization;
using System.Text;
using ProjetoVendas.Data;
using ProjetoVendas.Exercicios;
using ProjetoVendas.Models;
using ProjetoVendas.Services;

namespace ProjetoVendas;

internal static class Program
{
    private static BancoEmMemoria _db = null!;
    private static ConsultaService _consultas = null!;
    private static PedidoService _pedidos = null!;

    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        ConfigurarCultura();

        _db = DadosVirtuais.Criar();
        _consultas = new ConsultaService(_db);
        _pedidos = new PedidoService(_db);

        Console.WriteLine("=== Sistema de Vendas em Memória ===");
        Console.WriteLine($"Categorias: {_db.Categorias.Count}");
        Console.WriteLine($"Produtos: {_db.Produtos.Count}");
        Console.WriteLine($"Clientes: {_db.Clientes.Count}");
        Console.WriteLine($"Pedidos: {_db.Pedidos.Count}");

        int opcao;
        do
        {
            ExibirMenu();
            opcao = LerInteiro("Escolha uma opção: ");
            Console.WriteLine();

            switch (opcao)
            {
                case 1: ListarCategorias(); break;
                case 2: ListarProdutos(); break;
                case 3: ConsultarProdutosPorCategoria(); break;
                case 4: ListarClientes(); break;
                case 5: ConsultarPedidosPorCliente(); break;
                case 6: ExibirDetalhesDoPedido(); break;
                case 7: ExibirRankingProdutos(); break;
                case 8: ExibirRankingClientes(); break;
                case 9: CriarPedido(); break;
                case 10: MenuExercicios(); break;
                case 0: Console.WriteLine("Encerrando o sistema. Até logo!"); break;
                default: Console.WriteLine("Opção inválida."); break;
            }
        } while (opcao != 0);
    }

    private static void ExibirMenu()
    {
        Console.WriteLine();
        Console.WriteLine("------------- MENU -------------");
        Console.WriteLine("1  - Listar categorias");
        Console.WriteLine("2  - Listar produtos");
        Console.WriteLine("3  - Consultar produtos por categoria");
        Console.WriteLine("4  - Listar clientes");
        Console.WriteLine("5  - Consultar pedidos por cliente");
        Console.WriteLine("6  - Exibir detalhes de um pedido");
        Console.WriteLine("7  - Ranking de produtos vendidos");
        Console.WriteLine("8  - Ranking de clientes");
        Console.WriteLine("9  - Criar pedido");
        Console.WriteLine("10 - Exercícios e demonstrações do curso");
        Console.WriteLine("0  - Sair");
        Console.WriteLine("--------------------------------");
    }

    private static void ListarCategorias()
    {
        Console.WriteLine("== Categorias ==");
        foreach (Categoria categoria in _db.Categorias)
            Console.WriteLine($"{categoria.Id} - {categoria.Nome} ({categoria.Produtos.Count} produto(s))");
    }

    private static void ListarProdutos()
    {
        Console.WriteLine("== Produtos ==");
        foreach (Produto produto in _db.Produtos.OrderBy(p => p.Id))
            Console.WriteLine($"{produto.Id} - {produto.Nome} | {produto.Preco:C} | Categoria: {produto.Categoria.Nome}");
    }

    private static void ConsultarProdutosPorCategoria()
    {
        ListarCategorias();
        string entrada = LerTexto("Informe o Id ou o nome da categoria: ");

        Categoria? categoria = _consultas.BuscarCategoria(entrada);
        if (categoria is null)
        {
            Console.WriteLine("Categoria não encontrada.");
            return;
        }

        Console.WriteLine($"Categoria: {categoria.Nome}");
        foreach (Produto produto in categoria.Produtos)
            Console.WriteLine($"- {produto}");
    }

    private static void ListarClientes()
    {
        Console.WriteLine("== Clientes ==");
        foreach (Cliente cliente in _db.Clientes)
        {
            string endereco = cliente.EnderecoPrincipal?.ToString() ?? "sem endereço";
            Console.WriteLine($"{cliente.Id} - {cliente.Nome} | {cliente.Email} | {endereco} | {cliente.Pedidos.Count} pedido(s)");
        }
    }

    private static void ConsultarPedidosPorCliente()
    {
        ListarClientes();
        string entrada = LerTexto("Informe o Id ou o nome do cliente: ");

        Cliente? cliente = _consultas.BuscarCliente(entrada);
        if (cliente is null)
        {
            Console.WriteLine("Cliente não encontrado.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Cliente: {cliente.Nome}");

        if (cliente.Pedidos.Count == 0)
        {
            Console.WriteLine("Este cliente ainda não possui pedidos.");
            return;
        }

        foreach (Pedido pedido in cliente.Pedidos)
        {
            Console.WriteLine();
            ExibirPedido(pedido, exibirCliente: false);
        }

        Console.WriteLine();
        Console.WriteLine($"Total comprado pelo cliente: {cliente.Pedidos.Sum(p => p.Total):C}");
    }

    private static void ExibirDetalhesDoPedido()
    {
        Console.WriteLine("Pedidos existentes: " + string.Join(", ", _db.Pedidos.Select(p => p.Id)));
        int id = LerInteiro("Informe o Id do pedido: ");

        Pedido? pedido = _consultas.BuscarPedido(id);
        if (pedido is null)
        {
            Console.WriteLine("Pedido não encontrado.");
            return;
        }

        ExibirPedido(pedido, exibirCliente: true);
    }

    private static void ExibirPedido(Pedido pedido, bool exibirCliente)
    {
        if (exibirCliente)
            Console.WriteLine($"Cliente: {pedido.Cliente.Nome}");

        Console.WriteLine($"Pedido: {pedido.Id}");
        Console.WriteLine($"Data: {pedido.Data:dd/MM/yyyy}");
        Console.WriteLine("Itens:");

        foreach (ItemPedido item in pedido.Itens)
        {
            Console.WriteLine(
                $"  {item.Quantidade} x {item.Produto.Nome}  {item.PrecoUnitario:C} = {item.Subtotal:C}");
        }

        Console.WriteLine("------------------------------------");
        Console.WriteLine($"Total do pedido: {pedido.Total:C}");
    }

    private static void ExibirRankingProdutos()
    {
        Console.WriteLine("== Ranking de produtos por quantidade vendida ==");
        int posicao = 1;
        foreach (RankingProduto item in _consultas.RankingProdutos())
        {
            Console.WriteLine($"{posicao}. {item.Produto}: {item.Quantidade} unidade(s) - {item.Valor:C}");
            posicao++;
        }
    }

    private static void ExibirRankingClientes()
    {
        Console.WriteLine("== Ranking de clientes por valor total comprado ==");
        int posicao = 1;
        foreach (RankingCliente item in _consultas.RankingClientes())
        {
            Console.WriteLine($"{posicao}. {item.Cliente}: {item.TotalComprado:C} ({item.QuantidadePedidos} pedido(s))");
            posicao++;
        }

        Console.WriteLine($"Faturamento geral: {_consultas.Faturamento():C}");
    }

    private static void CriarPedido()
    {
        ListarClientes();
        string entrada = LerTexto("Informe o Id ou o nome do cliente: ");

        Cliente? cliente = _consultas.BuscarCliente(entrada);
        if (cliente is null)
        {
            Console.WriteLine("Cliente não encontrado.");
            return;
        }

        Console.WriteLine();
        ListarProdutos();
        Console.WriteLine();

        List<(Produto Produto, int Quantidade)> itens = new();

        while (true)
        {
            int produtoId = LerInteiro("Id do produto (0 para finalizar): ");
            if (produtoId == 0)
                break;

            Produto? produto = _db.Produtos.FirstOrDefault(p => p.Id == produtoId);
            if (produto is null)
            {
                Console.WriteLine("Produto não encontrado.");
                continue;
            }

            int quantidade = LerInteiro("Quantidade: ");
            if (quantidade <= 0)
            {
                Console.WriteLine("Quantidade deve ser maior que zero.");
                continue;
            }

            itens.Add((produto, quantidade));
            Console.WriteLine($"Adicionado: {quantidade} x {produto.Nome}");
        }

        if (itens.Count == 0)
        {
            Console.WriteLine("Pedido cancelado: é necessário informar pelo menos 1 item.");
            return;
        }

        Pedido pedido = _pedidos.CriarPedido(cliente.Id);
        foreach ((Produto produto, int quantidade) in itens)
            pedido.AdicionarItem(produto, quantidade);

        Console.WriteLine();
        Console.WriteLine("Pedido criado com sucesso!");
        ExibirPedido(pedido, exibirCliente: true);
    }

    private static void MenuExercicios()
    {
        int opcao;
        do
        {
            Console.WriteLine();
            Console.WriteLine("---- Exercícios e demonstrações ----");
            Console.WriteLine("1 - Exercício inicial (Aluno)");
            Console.WriteLine("2 - Nível básico (Livro e Autor)");
            Console.WriteLine("3 - Nível intermediário (Departamento, Escola, Turma)");
            Console.WriteLine("4 - Nível avançado (Curso, Aluno e Matrícula)");
            Console.WriteLine("5 - Demonstração dos Módulos 7 e 8 (navegação e alterações)");
            Console.WriteLine("6 - Executar todos");
            Console.WriteLine("0 - Voltar");
            opcao = LerInteiro("Escolha uma opção: ");
            Console.WriteLine();

            switch (opcao)
            {
                case 1: ExercicioInicial.Executar(); break;
                case 2: ExercicioBasico.Executar(); break;
                case 3: ExercicioIntermediario.Executar(); break;
                case 4: ExercicioAvancado.Executar(); break;
                case 5: DemonstracaoModulos7e8.Executar(); break;
                case 6:
                    ExercicioInicial.Executar();
                    ExercicioBasico.Executar();
                    ExercicioIntermediario.Executar();
                    ExercicioAvancado.Executar();
                    DemonstracaoModulos7e8.Executar();
                    break;
                case 0: break;
                default: Console.WriteLine("Opção inválida."); break;
            }
        } while (opcao != 0);
    }

    private static string LerTexto(string mensagem)
    {
        Console.Write(mensagem);
        string? linha = Console.ReadLine();

        if (linha is null)
            Environment.Exit(0);

        return linha.Trim();
    }

    private static int LerInteiro(string mensagem)
    {
        string texto = LerTexto(mensagem);
        return int.TryParse(texto, out int valor) ? valor : -1;
    }

    private static void ConfigurarCultura()
    {
        try
        {
            CultureInfo cultura = new CultureInfo("pt-BR");
            CultureInfo.CurrentCulture = cultura;
            CultureInfo.CurrentUICulture = cultura;
        }
        catch (CultureNotFoundException)
        {
        }
    }
}
