using System;
using CampusQuest.Core;
using CampusQuest.Exame;
using CampusQuest.Materias;

namespace CampusQuest;

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("Campus Quest - Teste Console");

        while (true)
        {
            Console.WriteLine("1) Executar exame");
            Console.WriteLine("2) Sair");
            Console.Write("Opcao: ");
            string opcao = Console.ReadLine();

            if (opcao == "2")
            {
                return;
            }

            if (opcao != "1")
            {
                Console.WriteLine("Opcao invalida.");
                continue;
            }

            Aluno aluno = CriarAluno();
            Materia chefe = CriarChefe();

            SistemaExame sistema = new SistemaExame();
            ResultadoExame resultado = sistema.Executar(aluno, chefe);

            Console.WriteLine();
            Console.WriteLine($"Vitoria: {(resultado.Vitoria ? "Sim" : "Nao")}");
            Console.WriteLine($"Aproveitamento: {resultado.Aproveitamento}");
            Console.WriteLine($"Bonus coragem: {(resultado.BonusCoragemAplicado ? "Sim" : "Nao")}");
            Console.WriteLine($"Acertos: {resultado.AcertosTotal} | Erros: {resultado.ErrosTotal}");
            Console.WriteLine($"Vida do aluno: {aluno.Vida}");
            Console.WriteLine($"Vida do chefe: {chefe.GetVidaAtual()}");
            Console.WriteLine();
        }
    }

    private static Aluno CriarAluno()
    {
        Aluno aluno = new Aluno();
        int conhecimento = LerInt("Conhecimento inicial (0-100): ", 0, 100);
        aluno.AumentarConhecimento(conhecimento);

        Console.Write("Adicionar habilidade DuploAtaque? (s/n): ");
        string resposta = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(resposta) && resposta.Trim().Equals("s", StringComparison.OrdinalIgnoreCase))
        {
            aluno.AdicionarHabilidade(new Habilidade("Duplo Ataque", "Aplica dois ataques extras.", TipoEfeito.DuploAtaque));
        }

        return aluno;
    }

    private static Materia CriarChefe()
    {
        Console.WriteLine("Chefe:");
        Console.WriteLine("1) IC");
        Console.WriteLine("2) AED");
        Console.WriteLine("3) POO");
        Console.WriteLine("4) TCC");

        int opcao = LerInt("Opcao: ", 1, 4);
        switch (opcao)
        {
            case 1:
                return new IC();
            case 2:
                return new AED();
            case 3:
                return new POO();
            case 4:
                float media = LerFloat("Media final do aluno (0-100): ", 0f, 100f);
                return new TCC(media);
            default:
                return new IC();
        }
    }

    private static int LerInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            string entrada = Console.ReadLine();

            if (int.TryParse(entrada, out int valor) && valor >= min && valor <= max)
            {
                return valor;
            }

            Console.WriteLine("Entrada invalida.");
        }
    }

    private static float LerFloat(string prompt, float min, float max)
    {
        while (true)
        {
            Console.Write(prompt);
            string entrada = Console.ReadLine();

            if (float.TryParse(entrada, out float valor) && valor >= min && valor <= max)
            {
                return valor;
            }

            Console.WriteLine("Entrada invalida.");
        }
    }
}
