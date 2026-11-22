namespace ProjetoVendas.Exercicios.Intermediario;

public class Funcionario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    // internal set: só o Departamento (via AdicionarFuncionario) altera esta referência
    public Departamento? Departamento { get; internal set; }
}
