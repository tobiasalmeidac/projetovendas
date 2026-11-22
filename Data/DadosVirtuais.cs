using ProjetoVendas.Models;

namespace ProjetoVendas.Data;

/// <summary>
/// Carga inicial (seeder) com dados virtuais.
/// Requisitos do laboratório: 3 categorias, 8 produtos, 5 clientes e 6 pedidos (cada um com pelo menos 1 item).
/// Os 5 clientes são os membros do grupo, com o nome completo.
/// </summary>
public static class DadosVirtuais
{
    public static BancoEmMemoria Criar()
    {
        BancoEmMemoria db = new BancoEmMemoria();

        // ---------- Categorias (3) ----------
        Categoria informatica = new Categoria { Id = 1, Nome = "Informática" };
        Categoria escritorio = new Categoria { Id = 2, Nome = "Escritório" };
        Categoria acessorios = new Categoria { Id = 3, Nome = "Acessórios" };

        // ---------- Produtos (8) ----------
        // Usamos o método de fábrica, que já adiciona o produto na coleção da categoria.
        Produto mouse = Produto.CriarProduto(1, "Mouse sem fio", 89.90m, informatica);
        Produto teclado = Produto.CriarProduto(2, "Teclado mecânico", 159.90m, informatica);
        Produto monitor = Produto.CriarProduto(3, "Monitor 24 polegadas", 1199.00m, informatica);

        Produto cadeira = Produto.CriarProduto(4, "Cadeira ergonômica", 899.00m, escritorio);
        Produto luminaria = Produto.CriarProduto(5, "Luminária de mesa", 79.90m, escritorio);
        Produto suporte = Produto.CriarProduto(6, "Suporte para monitor", 129.90m, escritorio);

        Produto mochila = Produto.CriarProduto(7, "Mochila para notebook", 189.90m, acessorios);
        Produto headset = Produto.CriarProduto(8, "Headset com microfone", 249.90m, acessorios);

        // ---------- Clientes (5): um para cada membro do grupo ----------
        Cliente pedro = new Cliente
        {
            Id = 1,
            Nome = "Pedro Arthur Santanna Pimentel",
            Email = "pedro@exemplo.local",
            EnderecoPrincipal = new Endereco
            {
                Logradouro = "Rua das Flores, 100",
                Cidade = "Resende",
                Uf = "RJ",
                Cep = "27500-000"
            }
        };

        Cliente evellyn = new Cliente
        {
            Id = 2,
            Nome = "Evellyn da Silva Rodrigues",
            Email = "evellyn@exemplo.local",
            EnderecoPrincipal = new Endereco
            {
                Logradouro = "Av. Central, 250",
                Cidade = "Volta Redonda",
                Uf = "RJ",
                Cep = "27200-000"
            }
        };

        Cliente tobias = new Cliente
        {
            Id = 3,
            Nome = "Tobias Henrique de Almeida Caetano",
            Email = "tobias@exemplo.local",
            EnderecoPrincipal = new Endereco
            {
                Logradouro = "Rua B, 50",
                Cidade = "Barra Mansa",
                Uf = "RJ",
                Cep = "27300-000"
            }
        };

        Cliente rhayssa = new Cliente
        {
            Id = 4,
            Nome = "Rhayssa Hyllari Lemos Rezende",
            Email = "rhayssa@exemplo.local",
            EnderecoPrincipal = new Endereco
            {
                Logradouro = "Rua do Comércio, 75",
                Cidade = "Itatiaia",
                Uf = "RJ"
            }
        };

        Cliente joao = new Cliente
        {
            Id = 5,
            Nome = "João Marcos de Almeida Albarello",
            Email = "joao@exemplo.local",
            EnderecoPrincipal = new Endereco
            {
                Logradouro = "Av. Brasil, 900",
                Cidade = "Rio de Janeiro",
                Uf = "RJ",
                Cep = "20000-000"
            }
        };

        // ---------- Pedidos (6), cada um com pelo menos 1 item ----------
        Pedido pedido1 = new Pedido { Id = 1001, Data = new DateTime(2026, 11, 1), Cliente = pedro };
        pedido1.AdicionarItem(mouse, 2);
        pedido1.AdicionarItem(teclado, 1);
        pedido1.VincularAoCliente();

        Pedido pedido2 = new Pedido { Id = 1002, Data = new DateTime(2026, 11, 3), Cliente = evellyn };
        pedido2.AdicionarItem(cadeira, 1);
        pedido2.AdicionarItem(luminaria, 2);
        pedido2.VincularAoCliente();

        Pedido pedido3 = new Pedido { Id = 1003, Data = new DateTime(2026, 11, 5), Cliente = tobias };
        pedido3.AdicionarItem(monitor, 1);
        pedido3.AdicionarItem(suporte, 1);
        pedido3.VincularAoCliente();

        Pedido pedido4 = new Pedido { Id = 1004, Data = new DateTime(2026, 11, 8), Cliente = rhayssa };
        pedido4.AdicionarItem(mochila, 1);
        pedido4.AdicionarItem(headset, 1);
        pedido4.VincularAoCliente();

        Pedido pedido5 = new Pedido { Id = 1005, Data = new DateTime(2026, 11, 10), Cliente = joao };
        pedido5.AdicionarItem(teclado, 1);
        pedido5.AdicionarItem(mouse, 1);
        pedido5.AdicionarItem(headset, 1);
        pedido5.VincularAoCliente();

        Pedido pedido6 = new Pedido { Id = 1006, Data = new DateTime(2026, 11, 12), Cliente = tobias };
        pedido6.AdicionarItem(mouse, 3);
        pedido6.VincularAoCliente();

        // ---------- Carga no "banco" ----------
        db.Categorias.AddRange(new[] { informatica, escritorio, acessorios });
        db.Produtos.AddRange(new[] { mouse, teclado, monitor, cadeira, luminaria, suporte, mochila, headset });
        db.Clientes.AddRange(new[] { pedro, evellyn, tobias, rhayssa, joao });
        db.Pedidos.AddRange(new[] { pedido1, pedido2, pedido3, pedido4, pedido5, pedido6 });

        return db;
    }
}
