namespace ProjetoVendas.Exercicios.Avancado;

public class Matricula
{
    private static int _proximoId = 1;

    public int Id { get; }
    public Curso Curso { get; }
    public Aluno Aluno { get; }
    public DateTime DataMatricula { get; }
    public SituacaoMatricula Situacao { get; private set; }

    internal Matricula(Curso curso, Aluno aluno, DateTime dataMatricula)
    {
        Id = _proximoId++;
        Curso = curso;
        Aluno = aluno;
        DataMatricula = dataMatricula;
        Situacao = SituacaoMatricula.Ativa;
    }

    public void AlterarSituacao(SituacaoMatricula novaSituacao)
    {
        Situacao = novaSituacao;
    }
}
