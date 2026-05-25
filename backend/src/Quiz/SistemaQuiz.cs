using System;
using System.Collections.Generic;
using Aluno = CampusQuest.Core.Aluno;
using GameItem = CampusQuest.Itens.Item;

using CampusQuest.Core;
using CampusQuest.Itens;
using CampusQuest.UI;

namespace CampusQuest.Quiz;

public class SistemaQuiz
{
    private const int GanhoBaseConhecimento = 10;
    private static readonly Dictionary<int, Pergunta[]> BancoPerguntas = CriarBancoPerguntas();
    private static readonly Random Rng = new();
    private readonly IConsoleIO io;

    public SistemaQuiz(IConsoleIO? io = null)
    {
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

        GameItem itemDropado = CriarItemPorFaixa(aproveitamento, semestreNormalizado);
        if (itemDropado != null)
        {
            aluno.Inventario.Adicionar(itemDropado);
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

    private static Dictionary<int, Pergunta[]> CriarBancoPerguntas()
    {
        return new Dictionary<int, Pergunta[]>
        {
            [1] = new[]
            {
                new Pergunta("O que é hardware?", new[] { "Parte física", "Programa", "Rede", "Arquivo" }, 0, "Hardware é a parte física.") ,
                new Pergunta("O que é software?", new[] { "Programa", "Placa", "Teclado", "Memória" }, 0, "Software é o conjunto de programas."),
                new Pergunta("Qual sistema gerencia recursos do computador?", new[] { "Sistema operacional", "Editor", "Navegador", "Jogos" }, 0, "O sistema operacional gerencia recursos."),
                new Pergunta("Qual número representa binário?", new[] { "0 e 1", "2 e 3", "5 e 6", "8 e 9" }, 0, "Binário usa 0 e 1."),
                new Pergunta("Qual dispositivo conecta redes?", new[] { "Roteador", "Mouse", "Monitor", "Teclado" }, 0, "Roteador conecta redes diferentes.")
            },
            [2] = new[]
            {
                new Pergunta("Pilha segue qual regra?", new[] { "LIFO", "FIFO", "ABC", "XYZ" }, 0, "Pilha é LIFO."),
                new Pergunta("Fila segue qual regra?", new[] { "FIFO", "LIFO", "DFS", "BFS" }, 0, "Fila é FIFO."),
                new Pergunta("Busca binária exige dados:", new[] { "Ordenados", "Aleatórios", "Vazios", "Repetidos" }, 0, "Busca binária exige ordenação."),
                new Pergunta("Complexidade O(n) indica crescimento:", new[] { "Linear", "Constante", "Quadrático", "Logarítmico" }, 0, "O(n) é linear."),
                new Pergunta("Qual estrutura combina recursão?", new[] { "Pilha", "Fila", "Lista", "Matriz" }, 0, "Recursão usa pilha de execução.")
            },
            [3] = new[]
            {
                new Pergunta("Encapsulamento controla:", new[] { "Acesso aos dados", "Rede", "Compilação", "Memória" }, 0, "Encapsulamento controla acesso."),
                new Pergunta("Herança permite:", new[] { "Reuso", "Apagar dados", "Acelerar CPU", "Criar rede" }, 0, "Herança promove reuso."),
                new Pergunta("Polimorfismo significa:", new[] { "Mesmo método com comportamentos diferentes", "Só um construtor", "Sem classes", "Sem objetos" }, 0, "Polimorfismo altera comportamento."),
                new Pergunta("Abstração é:", new[] { "Focar no essencial", "Duplicar código", "Evitar classes", "Criar bugs" }, 0, "Abstração foca no essencial."),
                new Pergunta("Objeto é:", new[] { "Instância de classe", "Método", "Interface", "Namespace" }, 0, "Objeto é uma instância.")
            }
        };
    }

    private static Pergunta[] ObterPerguntasAleatorias(int semestre)
    {
        if (!BancoPerguntas.TryGetValue(semestre, out Pergunta[] perguntasBase))
        {
            return Array.Empty<Pergunta>();
        }

        Pergunta[] copia = (Pergunta[])perguntasBase.Clone();
        for (int i = copia.Length - 1; i > 0; i--)
        {
            int j = Rng.Next(i + 1);
            (copia[i], copia[j]) = (copia[j], copia[i]);
        }

        return copia;
    }
}
