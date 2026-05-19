using System;
using System.Collections.Generic;
using Aluno = CampusQuest.Core.Aluno;
using GameItem = CampusQuest.Itens.Item;

using CampusQuest.Core;
using CampusQuest.Itens;

namespace CampusQuest.Quiz;

public class SistemaQuiz
{
    private const int GanhoBaseConhecimento = 10;
    private static readonly Dictionary<int, List<Pergunta>> BancoPerguntas = CriarBancoPerguntas();

    public ResultadoQuiz Executar(Aluno aluno, int semestre)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        int semestreNormalizado = Math.Max(1, Math.Min(3, semestre));
        List<Pergunta> perguntas = BancoPerguntas[semestreNormalizado];
        int acertos = 0;

        Console.WriteLine($"Quiz do semestre {semestreNormalizado}");

        for (int i = 0; i < perguntas.Count; i++)
        {
            Pergunta pergunta = perguntas[i];
            Console.WriteLine(pergunta.Enunciado);

            for (int opcao = 0; opcao < pergunta.Alternativas.Length; opcao++)
            {
                Console.WriteLine($"{opcao + 1}. {pergunta.Alternativas[opcao]}");
            }

            Console.Write("Resposta: ");
            string entrada = Console.ReadLine();
            int indiceEscolhido = int.TryParse(entrada, out int valor) ? valor - 1 : -1;

            if (pergunta.VerificarResposta(indiceEscolhido))
            {
                acertos++;
                Console.WriteLine("Correto.");
            }
            else
            {
                Console.WriteLine("Errado.");
                Console.WriteLine(pergunta.Explicacao);
            }

            Console.WriteLine();
        }

        int totalPerguntas = perguntas.Count;
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

    private static Dictionary<int, List<Pergunta>> CriarBancoPerguntas()
    {
        return new Dictionary<int, List<Pergunta>>
        {
            [1] = new List<Pergunta>
            {
                new Pergunta("O que é hardware?", new[] { "Parte física", "Programa", "Rede", "Arquivo" }, 0, "Hardware é a parte física.") ,
                new Pergunta("O que é software?", new[] { "Programa", "Placa", "Teclado", "Memória" }, 0, "Software é o conjunto de programas."),
                new Pergunta("Qual sistema gerencia recursos do computador?", new[] { "Sistema operacional", "Editor", "Navegador", "Jogos" }, 0, "O sistema operacional gerencia recursos."),
                new Pergunta("Qual número representa binário?", new[] { "0 e 1", "2 e 3", "5 e 6", "8 e 9" }, 0, "Binário usa 0 e 1."),
                new Pergunta("Qual dispositivo conecta redes?", new[] { "Roteador", "Mouse", "Monitor", "Teclado" }, 0, "Roteador conecta redes diferentes.")
            },
            [2] = new List<Pergunta>
            {
                new Pergunta("Pilha segue qual regra?", new[] { "LIFO", "FIFO", "ABC", "XYZ" }, 0, "Pilha é LIFO."),
                new Pergunta("Fila segue qual regra?", new[] { "FIFO", "LIFO", "DFS", "BFS" }, 0, "Fila é FIFO."),
                new Pergunta("Busca binária exige dados:", new[] { "Ordenados", "Aleatórios", "Vazios", "Repetidos" }, 0, "Busca binária exige ordenação."),
                new Pergunta("Complexidade O(n) indica crescimento:", new[] { "Linear", "Constante", "Quadrático", "Logarítmico" }, 0, "O(n) é linear."),
                new Pergunta("Qual estrutura combina recursão?", new[] { "Pilha", "Fila", "Lista", "Matriz" }, 0, "Recursão usa pilha de execução.")
            },
            [3] = new List<Pergunta>
            {
                new Pergunta("Encapsulamento controla:", new[] { "Acesso aos dados", "Rede", "Compilação", "Memória" }, 0, "Encapsulamento controla acesso."),
                new Pergunta("Herança permite:", new[] { "Reuso", "Apagar dados", "Acelerar CPU", "Criar rede" }, 0, "Herança promove reuso."),
                new Pergunta("Polimorfismo significa:", new[] { "Mesmo método com comportamentos diferentes", "Só um construtor", "Sem classes", "Sem objetos" }, 0, "Polimorfismo altera comportamento."),
                new Pergunta("Abstração é:", new[] { "Focar no essencial", "Duplicar código", "Evitar classes", "Criar bugs" }, 0, "Abstração foca no essencial."),
                new Pergunta("Objeto é:", new[] { "Instância de classe", "Método", "Interface", "Namespace" }, 0, "Objeto é uma instância.")
            }
        };
    }
}

public class ResultadoQuiz
{
    public int Aproveitamento { get; }
    public int GanhoConhecimento { get; }
    public GameItem ItemDropado { get; }

    public ResultadoQuiz(int aproveitamento, int ganhoConhecimento, GameItem itemDropado)
    {
        Aproveitamento = aproveitamento;
        GanhoConhecimento = ganhoConhecimento;
        ItemDropado = itemDropado;
    }
}
