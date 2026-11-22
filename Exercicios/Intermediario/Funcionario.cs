namespace ProjetoVendas.Exercicios.Intermediario;

public class Funcionario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    public Departamento? Departamento { get; internal set; }
}
