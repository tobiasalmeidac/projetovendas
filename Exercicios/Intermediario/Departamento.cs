namespace ProjetoVendas.Exercicios.Intermediario;

/// <summary>
/// Departamento 1:N Funcionario, com navegação nos dois sentidos.
/// </summary>
public class Departamento
{
    private readonly List<Funcionario> _funcionarios = new();

    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public IReadOnlyCollection<Funcionario> Funcionarios => _funcionarios.AsReadOnly();

    // Mantém as DUAS pontas da relação coerentes:
    // adiciona o funcionário na lista do departamento E define o departamento do funcionário.
    public void AdicionarFuncionario(Funcionario funcionario)
    {
        if (_funcionarios.Contains(funcionario))
            return;

        // Se o funcionário já estava em outro departamento, remove de lá primeiro.
        funcionario.Departamento?._funcionarios.Remove(funcionario);

        _funcionarios.Add(funcionario);
        funcionario.Departamento = this;
    }
}
