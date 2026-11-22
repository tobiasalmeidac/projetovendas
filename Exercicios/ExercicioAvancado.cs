using ProjetoVendas.Exercicios.Avancado;

namespace ProjetoVendas.Exercicios;

public static class ExercicioAvancado
{
    public static void Executar()
    {
        Console.WriteLine("=== Nível avançado: Curso N:N Aluno através de Matricula ===");

        List<Curso> cursos = new()
        {
            new Curso { Id = 1, Nome = "Lógica de Programação" },
            new Curso { Id = 2, Nome = "Banco de Dados" },
            new Curso { Id = 3, Nome = "Estruturas de Dados" },
            new Curso { Id = 4, Nome = "Desenvolvimento Web" },
            new Curso { Id = 5, Nome = "Engenharia de Software" }
        };

        string[] nomes =
        {
            "Alice Ferreira", "Bernardo Silva", "Carolina Dias", "Diego Santos", "Elisa Monteiro",
            "Fábio Cardoso", "Giovana Lopes", "Hugo Batista", "Ingrid Vieira", "Jorge Pinto",
            "Kelly Ramos", "Leandro Souza", "Mariana Azevedo", "Nelson Correia", "Priscila Moura"
        };

        List<Aluno> alunos = new();
        for (int i = 0; i < nomes.Length; i++)
            alunos.Add(new Aluno { Id = i + 1, Nome = nomes[i] });

        DateTime dataBase = new DateTime(2026, 2, 2);
        for (int i = 0; i < alunos.Count; i++)
        {
            DateTime data = dataBase.AddDays(i);
            cursos[i % 5].Matricular(alunos[i], data);
            cursos[(i + 2) % 5].Matricular(alunos[i], data);

            if (i % 3 == 0)
                cursos[(i + 4) % 5].Matricular(alunos[i], data);
        }

        alunos[1].Matriculas.First().AlterarSituacao(SituacaoMatricula.Concluida);
        alunos[2].Matriculas.First().AlterarSituacao(SituacaoMatricula.Trancada);

        Aluno aluno = alunos[0];
        Console.WriteLine($"Cursos do aluno {aluno.Nome}:");
        foreach (Matricula m in aluno.Matriculas)
            Console.WriteLine($"  - {m.Curso.Nome} | Matrícula em {m.DataMatricula:dd/MM/yyyy} | Situação: {m.Situacao}");

        Curso curso = cursos[0];
        Console.WriteLine($"Alunos do curso {curso.Nome}:");
        foreach (Matricula m in curso.Matriculas)
            Console.WriteLine($"  - {m.Aluno.Nome} ({m.Situacao})");

        Console.WriteLine("Quantidade de matrículas por curso:");
        foreach (Curso c in cursos)
            Console.WriteLine($"  {c.Nome}: {c.Matriculas.Count} matrícula(s)");

        Console.WriteLine($"Total de matrículas: {cursos.Sum(c => c.Matriculas.Count)}");

        Console.WriteLine("Testando a regra de matrícula duplicada:");
        try
        {
            cursos[0].Matricular(alunos[0], DateTime.Now);
            Console.WriteLine("  (não deveria chegar aqui)");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"  Bloqueado: {ex.Message}");
        }

        Console.WriteLine();
    }
}
