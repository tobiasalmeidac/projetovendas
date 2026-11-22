namespace ProjetoVendas.Exercicios.Intermediario;

/// <summary>
/// Turma N:1 Escola e Turma 1:N Aluno.
/// </summary>
public class Turma
{
    private readonly List<Aluno> _alunos = new();

    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public Escola? Escola { get; internal set; }
    public IReadOnlyCollection<Aluno> Alunos => _alunos.AsReadOnly();

    public void AdicionarAluno(Aluno aluno)
    {
        if (_alunos.Contains(aluno))
            return;

        _alunos.Add(aluno);
        aluno.Turma = this;
    }
}
