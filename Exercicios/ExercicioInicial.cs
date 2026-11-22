using ProjetoVendas.Exercicios.Inicial;

namespace ProjetoVendas.Exercicios;

public static class ExercicioInicial
{
    public static void Executar()
    {
        Console.WriteLine("=== Exercício inicial: classe Aluno ===");

        // 2. Instanciar dois alunos
        Aluno aluno1 = new Aluno { Id = 1, Nome = "Maria Clara", Nota = 7.5m };
        Aluno aluno2 = new Aluno { Id = 2, Nome = "Carlos Eduardo", Nota = 8.0m };

        // 3. Exibir os dados no Console
        Console.WriteLine("Dados iniciais:");
        Exibir(aluno1);
        Exibir(aluno2);

        // 4. Alterar a nota do primeiro aluno e exibir novamente
        aluno1.Nota = 9.0m;
        Console.WriteLine("Após alterar a nota do primeiro aluno:");
        Exibir(aluno1);
        Exibir(aluno2);
        Console.WriteLine();
    }

    private static void Exibir(Aluno aluno)
        => Console.WriteLine($"  Id: {aluno.Id} | Nome: {aluno.Nome} | Nota: {aluno.Nota:F1}");
}
