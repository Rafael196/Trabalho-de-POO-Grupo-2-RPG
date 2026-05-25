using System.Collections.Generic;
using CampusQuest.Core;
using CampusQuest.Quiz;

namespace CampusQuest.Materias;

public class AED : Materia
{
    public AED()
    {
        Nome = "AED";
        vidaMaxima = 100;
        vidaAtual = vidaMaxima;
        descricaoAtaque = "Stack Overflow";
    }

    public override void Atacar(Aluno aluno)
    {
        int dano = CalcularDanoAoAluno(aluno, DanoBaseChefe);
        aluno.ReceberDano(dano);
    }

    public override void AtaqueEspecial(Aluno aluno)
    {
        double danoBase = DanoBaseChefe * 2.5;
        int dano = CalcularDanoAoAluno(aluno, danoBase);
        aluno.ReceberDano(dano);
    }

    public override List<Pergunta> GetPerguntasExame()
    {
        return new List<Pergunta>
        {
            new Pergunta(
                "A complexidade O(n) indica tempo proporcional a:",
                new[] { "Tamanho da entrada", "Quadrado da entrada", "Logaritmo da entrada", "Constante" },
                0,
                "O(n) cresce linearmente com o tamanho da entrada."),
            new Pergunta(
                "Qual estrutura segue o principio LIFO?",
                new[] { "Pilha", "Fila", "Lista", "Arvore" },
                0,
                "Pilha e LIFO: ultimo a entrar, primeiro a sair."),
            new Pergunta(
                "Qual estrutura segue o principio FIFO?",
                new[] { "Fila", "Pilha", "Arvore", "Hash" },
                0,
                "Fila e FIFO: primeiro a entrar, primeiro a sair."),
            new Pergunta(
                "Para usar busca binaria, o vetor precisa estar:",
                new[] { "Ordenado", "Aleatorio", "Reverso", "Circular" },
                0,
                "Busca binaria exige dados ordenados."),
            new Pergunta(
                "Qual algoritmo troca elementos adjacentes repetidamente?",
                new[] { "Bubble sort", "Merge sort", "Quick sort", "Selection sort" },
                0,
                "Bubble sort compara e troca vizinhos."),
            new Pergunta(
                "Qual estrutura suporta chamadas recursivas?",
                new[] { "Pilha", "Fila", "Hash", "Grafo" },
                0,
                "Chamadas recursivas usam a pilha de execucao."),
            new Pergunta(
                "A busca linear em pior caso tem complexidade:",
                new[] { "O(n)", "O(log n)", "O(1)", "O(n log n)" },
                0,
                "Busca linear pode visitar todos os elementos."),
            new Pergunta(
                "Em uma arvore binaria de busca, valores menores ficam:",
                new[] { "Na subarvore esquerda", "Na subarvore direita", "Em qualquer lado", "Apenas na raiz" },
                0,
                "Na BST, menores ficam a esquerda."),
        };
    }
}
