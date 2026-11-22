namespace ProjetoVendas.Exercicios.Intermediario;

public class Escola
{
    private readonly List<Turma> _turmas = new();

    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public IReadOnlyCollection<Turma> Turmas => _turmas.AsReadOnly();

    public void AdicionarTurma(Turma turma)
    {
        if (_turmas.Contains(turma))
            return;

        _turmas.Add(turma);
        turma.Escola = this;
    }
}
