using System.Collections.Generic;
using CampusQuest.Core;
using CampusQuest.Quiz;

namespace CampusQuest.Materias;

public class TCC : Materia
{
    public TCC(float mediaFinalAluno)
    {
        Nome = "TCC";
        descricaoAtaque = "Sintese Total";

        if (mediaFinalAluno >= 80f)
        {
            vidaMaxima = 200;
        }
        else if (mediaFinalAluno >= 50f)
        {
            vidaMaxima = 150;
        }
        else
        {
            vidaMaxima = 100;
        }

        vidaAtual = vidaMaxima;
    }

    public override void Atacar(Aluno aluno)
    {
        int dano = CalcularDanoAoAluno(aluno, DanoBaseChefe);
        aluno.ReceberDano(dano);
    }

    public override void AtaqueEspecial(Aluno aluno)
    {
        contextoExame.DanoContinuo = true;
        contextoExame.TurnosComDano = 2;
        contextoExame.MultiplicadorAnulado = true;

        double danoBase = DanoBaseChefe * 2.5;
        int dano = CalcularDanoAoAluno(aluno, danoBase);
        aluno.ReceberDano(dano);
    }

    public override List<Pergunta> GetPerguntasExame()
    {
        return new List<Pergunta>
        {
            new Pergunta(
                "Qual formula calcula a media final dos 3 semestres?",
                new[] { "(a1 + a2 + a3) / 3", "(a1 + a2) / 2", "a1 * a2 * a3", "a1 - a2 - a3" },
                0,
                "A media final e a soma dividida por 3."),
            new Pergunta(
                "No exame, dano ao chefe e calculado por:",
                new[] { "DanoBase * (1 + Conhecimento / 100)", "DanoBase * (1 - Conhecimento / 100)", "DanoBase + Conhecimento", "Conhecimento * 100" },
                0,
                "O dano ao chefe cresce com o conhecimento."),
            new Pergunta(
                "No exame, o dano ao aluno tem minimo:",
                new[] { "1", "0", "10", "Conhecimento" },
                0,
                "O dano ao aluno nao pode ser menor que 1."),
            new Pergunta(
                "Qual ataque especial anula o multiplicador de conhecimento?",
                new[] { "NullPointerException", "Loop Infinito", "Stack Overflow", "Sintese Total" },
                0,
                "NullPointerException anula o multiplicador."),
            new Pergunta(
                "Qual estrutura e mais adequada para LIFO?",
                new[] { "Pilha", "Fila", "Array ordenado", "Arvore" },
                0,
                "Pilha representa LIFO."),
            new Pergunta(
                "Qual componente armazena dados temporarios?",
                new[] { "RAM", "SSD", "HD", "ROM" },
                0,
                "RAM e memoria volatil."),
            new Pergunta(
                "Encapsulamento e aplicado quando:",
                new[] { "Dados privados com acesso controlado", "Funcoes globais", "Heranca multipla", "Script solto" },
                0,
                "Encapsulamento controla o acesso a dados."),
            new Pergunta(
                "Busca binaria exige dados:",
                new[] { "Ordenados", "Aleatorios", "Duplicados", "Vazios" },
                0,
                "Busca binaria funciona em dados ordenados."),
        };
    }
}
