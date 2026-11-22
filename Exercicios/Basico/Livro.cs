namespace ProjetoVendas.Exercicios.Basico;

public class Livro
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public decimal Preco { get; set; }

    // Exercício 2: relação Livro -> Autor (navegação unidirecional)
    public Autor Autor { get; set; } = null!;

    // Exercício 5: propriedade calculada, preço com 10% de desconto
    public decimal PrecoComDesconto => Preco * 0.90m;
}
