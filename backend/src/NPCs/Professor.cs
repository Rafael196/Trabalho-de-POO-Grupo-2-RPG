using System;
using CampusQuest.Core;
using CampusQuest.Itens;
using CampusQuest.Persistencia;
using CampusQuest.Quiz;
using CampusQuest.UI;

namespace CampusQuest.NPCs;

public class Professor : NPC
{
    private readonly SistemaQuiz sistemaQuiz;
    private readonly IConsoleIO io;
    private string dialogoAtual = "Professor pronto para sugerir um quiz.";

    public Professor(IRepositorioQuestoes repositorioQuestoes, IConsoleIO? io = null)
    {
        if (repositorioQuestoes == null)
            throw new ArgumentNullException(nameof(repositorioQuestoes));

        Nome = "Professor";
        this.io = io ?? new ConsoleIO();
        sistemaQuiz = new SistemaQuiz(repositorioQuestoes, this.io);
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

    public override string GetMensagemAbertura(Aluno aluno)
    {
        string nome = string.IsNullOrWhiteSpace(aluno?.Nome) ? "aluno" : aluno.Nome;
        return $"Professor: Ola, {nome}. Pronto para um quiz?";
    }

    public bool SugerirQuiz()
    {
        io.WriteLine("O professor sugere um quiz de reforco. Aceitar? (s/n)");
        string resposta = io.ReadLine();
        return !string.IsNullOrWhiteSpace(resposta) && resposta.Trim().Equals("s", StringComparison.OrdinalIgnoreCase);
    }

    public int EscolherSemestreQuiz(int semestreAtual)
    {
        int semestreMinimo = Math.Max(1, Math.Min(3, semestreAtual));
        while (true)
        {
            io.WriteLine($"Escolha o semestre do quiz (1-3). Atual sugerido: {semestreMinimo}");
            string entrada = io.ReadLine();

            if (int.TryParse(entrada, out int semestre) && semestre >= 1 && semestre <= 3)
            {
                return semestre;
            }

            io.WriteLine("Entrada invalida.");
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

    public override string ObterDica(Aluno aluno)
    {
        if (aluno == null)
        {
            return "Professor: Procure o veterano no Hall para obter itens uteis.";
        }

        return aluno.SemestreAtual switch
        {
            1 => "Professor: No Hall, o veterano pode te dar Cafe para recuperar vida.",
            2 => "Professor: O veterano pode entregar um Caderno para um bonus temporario.",
            3 => "Professor: O veterano pode entregar um Livro Tecnico para reforcar a materia.",
            _ => "Professor: Procure o veterano no Hall para obter itens uteis."
        };
    }

    public override Core.Item SolicitarItem(Aluno aluno)
    {
        return null;
    }

    public override string MensagemItemIndisponivel(Aluno aluno)
    {
        return "Professor: Eu nao distribuo itens. Fale com o veterano no Hall.";
    }
}