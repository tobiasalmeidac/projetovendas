namespace ProjetoVendas.Exercicios.Avancado;

/// <summary>
/// Classe associativa da relação N:N entre Curso e Aluno.
/// Guarda os dados próprios do vínculo: DataMatricula e Situacao.
/// </summary>
public class Matricula
{
    private static int _proximoId = 1;

    public int Id { get; }
    public Curso Curso { get; }
    public Aluno Aluno { get; }
    public DateTime DataMatricula { get; }
    public SituacaoMatricula Situacao { get; private set; }

    // internal: matrículas só são criadas por Curso.Matricular, que aplica as regras.
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
