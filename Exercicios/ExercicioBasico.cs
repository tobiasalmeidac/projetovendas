using ProjetoVendas.Exercicios.Basico;

namespace ProjetoVendas.Exercicios;

public static class ExercicioBasico
{
    public static void Executar()
    {
        Console.WriteLine("=== Nível básico: Livro e Autor ===");

        Autor machado = new Autor { Id = 1, Nome = "Machado de Assis" };
        Autor clarice = new Autor { Id = 2, Nome = "Clarice Lispector" };

        Livro livro1 = new Livro { Id = 1, Titulo = "Dom Casmurro", Preco = 39.90m, Autor = machado };
        Livro livro2 = new Livro { Id = 2, Titulo = "A Hora da Estrela", Preco = 54.90m, Autor = clarice };
        Livro livro3 = new Livro { Id = 3, Titulo = "Memórias Póstumas de Brás Cubas", Preco = 69.90m, Autor = machado };

        List<Livro> livros = new() { livro1, livro2, livro3 };

        Console.WriteLine("Todos os livros:");
        foreach (Livro livro in livros)
        {
            Console.WriteLine(
                $"  {livro.Id} - {livro.Titulo} | Autor: {livro.Autor.Nome} | {livro.Preco:C} | Com 10% de desconto: {livro.PrecoComDesconto:C}");
        }

        Console.WriteLine("Livros com preço acima de R$ 50,00:");
        foreach (Livro livro in livros.Where(l => l.Preco > 50m))
        {
            Console.WriteLine($"  {livro.Titulo} - {livro.Preco:C}");
        }

        Console.WriteLine();
    }
}
