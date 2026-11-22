namespace ProjetoVendas.Exercicios.Intermediario;

public class Departamento
{
    private readonly List<Funcionario> _funcionarios = new();

    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public IReadOnlyCollection<Funcionario> Funcionarios => _funcionarios.AsReadOnly();

    public void AdicionarFuncionario(Funcionario funcionario)
    {
        if (_funcionarios.Contains(funcionario))
            return;

        funcionario.Departamento?._funcionarios.Remove(funcionario);

        _funcionarios.Add(funcionario);
        funcionario.Departamento = this;
    }
}
