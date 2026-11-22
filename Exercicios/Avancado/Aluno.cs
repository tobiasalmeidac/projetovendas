namespace ProjetoVendas.Exercicios.Avancado;

public class Aluno
{
    private readonly List<Matricula> _matriculas = new();

    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    public IReadOnlyCollection<Matricula> Matriculas => _matriculas.AsReadOnly();

    public IEnumerable<Curso> Cursos => _matriculas.Select(m => m.Curso);

    internal void AdicionarMatricula(Matricula matricula)
    {
        if (!_matriculas.Contains(matricula))
            _matriculas.Add(matricula);
    }
}
