using System;
using CampusQuest.Core;
using CampusQuest.Itens;
using CampusQuest.Quiz;

namespace CampusQuest.NPCs;

public class Professor : NPC
{
    private readonly SistemaQuiz sistemaQuiz = new();
    private string dialogoAtual = "Professor pronto para sugerir um quiz.";

    public Professor()
    {
        Nome = "Professor";
    }

    public override void Interagir(Aluno aluno)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        dialogoAtual = "Quer fazer um quiz para reforçar o semestre?";
        if (!SugerirQuiz())
        {
            dialogoAtual = "Quiz recusado. Volte quando quiser estudar.";
            return;
        }

        int semestre = EscolherSemestreQuiz(aluno.SemestreAtual);
        ResultadoQuiz resultado = AplicarQuiz(aluno, semestre);
        dialogoAtual = $"Quiz concluído com aproveitamento {resultado.Aproveitamento}.";
    }

    public bool SugerirQuiz()
    {
        Console.WriteLine("O professor sugere um quiz de reforço. Aceitar? (s/n)");
        string resposta = Console.ReadLine();
        return !string.IsNullOrWhiteSpace(resposta) && resposta.Trim().Equals("s", StringComparison.OrdinalIgnoreCase);
    }

    public int EscolherSemestreQuiz(int semestreAtual)
    {
        int semestreMinimo = Math.Max(1, Math.Min(3, semestreAtual));
        while (true)
        {
            Console.WriteLine($"Escolha o semestre do quiz (1-3). Atual sugerido: {semestreMinimo}");
            string entrada = Console.ReadLine();

            if (int.TryParse(entrada, out int semestre) && semestre >= 1 && semestre <= 3)
            {
                return semestre;
            }

            Console.WriteLine("Entrada inválida.");
        }
    }

    public ResultadoQuiz AplicarQuiz(Aluno aluno, int semestre)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        return sistemaQuiz.Executar(aluno, semestre);
    }

    public override string GetDialogo()
    {
        return dialogoAtual;
    }
}