using ProjetoVendas.Exercicios.Intermediario;

namespace ProjetoVendas.Exercicios;

public static class ExercicioIntermediario
{
    public static void Executar()
    {
        Console.WriteLine("=== Nível intermediário ===");
        ExecutarDepartamento();
        ExecutarEscola();
    }

    // Exercícios 1 e 2: Departamento 1:N Funcionario
    private static void ExecutarDepartamento()
    {
        Console.WriteLine("--- Departamento 1:N Funcionario ---");

        Departamento ti = new Departamento { Id = 1, Nome = "Tecnologia" };
        Departamento rh = new Departamento { Id = 2, Nome = "Recursos Humanos" };

        Funcionario f1 = new Funcionario { Id = 1, Nome = "Fernanda Lima" };
        Funcionario f2 = new Funcionario { Id = 2, Nome = "Ricardo Alves" };
        Funcionario f3 = new Funcionario { Id = 3, Nome = "Patrícia Gomes" };

        // AdicionarFuncionario atualiza as duas pontas da relação
        ti.AdicionarFuncionario(f1);
        ti.AdicionarFuncionario(f2);
        rh.AdicionarFuncionario(f3);

        foreach (Departamento depto in new[] { ti, rh })
        {
            Console.WriteLine($"Departamento {depto.Nome}:");
            foreach (Funcionario f in depto.Funcionarios)
                Console.WriteLine($"  - {f.Nome} (departamento do funcionário: {f.Departamento?.Nome})");
        }

        // Transferência: o método remove do departamento antigo e adiciona no novo
        rh.AdicionarFuncionario(f2);
        Console.WriteLine($"Após transferir {f2.Nome} para {rh.Nome}:");
        Console.WriteLine($"  {ti.Nome}: {ti.Funcionarios.Count} funcionário(s)");
        Console.WriteLine($"  {rh.Nome}: {rh.Funcionarios.Count} funcionário(s)");
        Console.WriteLine();
    }

    // Exercícios 3 a 6: Escola 1:N Turma e Turma 1:N Aluno
    private static void ExecutarEscola()
    {
        Console.WriteLine("--- Escola 1:N Turma e Turma 1:N Aluno ---");

        // Exercício 4: 3 turmas e 20 alunos com dados virtuais
        Escola escola = new Escola { Id = 1, Nome = "Escola Municipal Aprender" };

        Turma turmaA = new Turma { Id = 1, Nome = "1º Ano A" };
        Turma turmaB = new Turma { Id = 2, Nome = "1º Ano B" };
        Turma turmaC = new Turma { Id = 3, Nome = "2º Ano A" };

        escola.AdicionarTurma(turmaA);
        escola.AdicionarTurma(turmaB);
        escola.AdicionarTurma(turmaC);

        string[] nomes =
        {
            "Ana Beatriz", "Bruno Henrique", "Camila Santos", "Daniel Moreira", "Eduarda Costa",
            "Felipe Andrade", "Gabriela Nunes", "Heitor Barbosa", "Isabela Rocha", "Joaquim Teixeira",
            "Karina Duarte", "Lucas Pereira", "Marina Castro", "Nicolas Ribeiro", "Olívia Martins",
            "Pietro Carvalho", "Quésia Araújo", "Rafael Mendes", "Sofia Cardoso", "Thiago Freitas"
        };

        Turma[] turmas = { turmaA, turmaB, turmaC };
        for (int i = 0; i < nomes.Length; i++)
        {
            Aluno aluno = new Aluno { Id = i + 1, Nome = nomes[i] };
            turmas[i % 3].AdicionarAluno(aluno);
        }

        // Exercício 5: listar todas as turmas da escola e todos os alunos de uma turma
        Console.WriteLine($"Turmas da escola {escola.Nome}:");
        foreach (Turma turma in escola.Turmas)
            Console.WriteLine($"  - {turma.Nome}");

        Console.WriteLine($"Alunos da turma {turmaA.Nome}:");
        foreach (Aluno aluno in turmaA.Alunos)
            Console.WriteLine($"  - {aluno.Nome}");

        // Exercício 6: quantidade de alunos por turma usando LINQ
        Console.WriteLine("Quantidade de alunos por turma (LINQ):");
        var quantidadePorTurma = escola.Turmas
            .Select(t => new { Turma = t.Nome, Quantidade = t.Alunos.Count });

        foreach (var item in quantidadePorTurma)
            Console.WriteLine($"  {item.Turma}: {item.Quantidade} aluno(s)");

        Console.WriteLine($"Total de alunos na escola: {escola.Turmas.Sum(t => t.Alunos.Count)}");
        Console.WriteLine();
    }
}
