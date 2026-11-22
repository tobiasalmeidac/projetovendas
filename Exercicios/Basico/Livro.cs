namespace ProjetoVendas.Exercicios.Basico;

public class Livro
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public decimal Preco { get; set; }

    public Autor Autor { get; set; } = null!;

    public decimal PrecoComDesconto => Preco * 0.90m;
}
