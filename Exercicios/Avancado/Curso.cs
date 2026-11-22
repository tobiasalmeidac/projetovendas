namespace ProjetoVendas.Exercicios.Avancado;

public class Curso
{
    private readonly List<Matricula> _matriculas = new();

    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    // Navegação Curso -> Matriculas
    public IReadOnlyCollection<Matricula> Matriculas => _matriculas.AsReadOnly();

    // Atalho: Curso -> Matriculas -> Aluno
    public IEnumerable<Aluno> Alunos => _matriculas.Select(m => m.Aluno);

    /// <summary>
    /// Matricula o aluno no curso. Regra: a mesma pessoa não pode se matricular duas vezes no mesmo curso.
    /// Atualiza as duas pontas da navegação (Curso e Aluno).
    /// </summary>
    public Matricula Matricular(Aluno aluno, DateTime dataMatricula)
    {
        if (_matriculas.Any(m => m.Aluno == aluno))
            throw new InvalidOperationException(
                $"O aluno {aluno.Nome} já está matriculado no curso {Nome}.");

        Matricula matricula = new Matricula(this, aluno, dataMatricula);
        _matriculas.Add(matricula);
        aluno.AdicionarMatricula(matricula);
        return matricula;
    }
}
