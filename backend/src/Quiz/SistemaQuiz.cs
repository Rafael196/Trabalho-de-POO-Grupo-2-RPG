using System;
using System.Collections.Generic;
using System.Linq;
using Aluno = CampusQuest.Core.Aluno;
using GameItem = CampusQuest.Itens.Item;

using CampusQuest.Core;
using CampusQuest.Itens;
using CampusQuest.Persistencia;
using CampusQuest.UI;

namespace CampusQuest.Quiz;

public class SistemaQuiz
{
    private const int GanhoBaseConhecimento = 10;
    private static readonly Random Rng = new();
    private readonly IConsoleIO io;
    private readonly IRepositorioQuestoes repositorioQuestoes;

    public SistemaQuiz(IRepositorioQuestoes repositorioQuestoes, IConsoleIO? io = null)
    {
        this.repositorioQuestoes = repositorioQuestoes ?? throw new ArgumentNullException(nameof(repositorioQuestoes));
        this.io = io ?? new ConsoleIO();
    }

    public ResultadoQuiz Executar(Aluno aluno, int semestre)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        int semestreNormalizado = Math.Max(1, Math.Min(3, semestre));
        Pergunta[] perguntas = ObterPerguntasAleatorias(semestreNormalizado);
        int acertos = 0;

        io.WriteLine($"Quiz do semestre {semestreNormalizado}");

        for (int i = 0; i < perguntas.Length; i++)
        {
            Pergunta pergunta = perguntas[i];
            io.WriteLine(pergunta.Enunciado);

            for (int opcao = 0; opcao < pergunta.Alternativas.Length; opcao++)
            {
                io.WriteLine($"{opcao + 1}. {pergunta.Alternativas[opcao]}");
            }

            io.Write("Resposta: ");
            string entrada = io.ReadLine();
            int indiceEscolhido = int.TryParse(entrada, out int valor) ? valor - 1 : -1;

            if (pergunta.VerificarResposta(indiceEscolhido))
            {
                acertos++;
                io.WriteLine("Correto.");
            }
            else
            {
                io.WriteLine("Errado.");
                io.WriteLine(pergunta.Explicacao);
            }

            io.WriteLine(string.Empty);
        }

        int totalPerguntas = perguntas.Length;
        int aproveitamento = totalPerguntas == 0
            ? 0
            : (int)Math.Round(acertos / (double)totalPerguntas * 100.0, MidpointRounding.AwayFromZero);

        bool revisao = semestreNormalizado < aluno.SemestreAtual;
        int ganhoConhecimento = acertos * GanhoBaseConhecimento * (revisao ? 50 : 100) / 100;

        aluno.AumentarConhecimento(ganhoConhecimento);
        aluno.RegistrarQuizConcluido();

        GameItem itemDropado = CriarItemPorFaixa(aproveitamento, semestreNormalizado);
        if (itemDropado != null)
        {
            if (!aluno.PodeReceberItem(itemDropado))
            {
                io.WriteLine("Limite de itens do semestre atingido. Item nao adicionado.");
                itemDropado = null;
            }
            else if (aluno.Inventario.Adicionar(itemDropado))
            {
                aluno.RegistrarItemRecebido(itemDropado);
            }
            else
            {
                io.WriteLine("Inventario cheio. Item nao adicionado.");
                itemDropado = null;
            }
        }

        return new ResultadoQuiz(aproveitamento, ganhoConhecimento, itemDropado);
    }

    private static GameItem CriarItemPorFaixa(int aproveitamento, int semestre)
    {
        if (aproveitamento >= 80)
        {
            return new LivroTecnico(ObterMateriaAlvo(semestre));
        }

        if (aproveitamento >= 50)
        {
            return new Caderno();
        }

        return new CampusQuest.Itens.Cafe();
    }

    private static string ObterMateriaAlvo(int semestre)
    {
        return semestre switch
        {
            1 => "IC",
            2 => "AED",
            3 => "POO",
            _ => "IC"
        };
    }

    private Pergunta[] ObterPerguntasAleatorias(int semestre)
    {
        var questoes = repositorioQuestoes.ObterPorSemestre(semestre).ToList();
        
        if (questoes.Count == 0)
        {
            return Array.Empty<Pergunta>();
        }

        // Embaralhar questões
        for (int i = questoes.Count - 1; i > 0; i--)
        {
            int j = Rng.Next(i + 1);
            (questoes[i], questoes[j]) = (questoes[j], questoes[i]);
        }

        // Converter QuestaoDto para Pergunta
        return questoes
            .Select(q => new Pergunta(q.Enunciado, q.Alternativas, q.IndiceCorreto, q.Explicacao))
            .ToArray();
    }
}
