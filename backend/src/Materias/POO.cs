using System.Collections.Generic;
using CampusQuest.Core;
using CampusQuest.Quiz;

namespace CampusQuest.Materias;

public class POO : Materia
{
    public POO()
    {
        Nome = "POO";
        vidaMaxima = 100;
        vidaAtual = vidaMaxima;
        descricaoAtaque = "NullPointerException";
    }

    public override void Atacar(Aluno aluno)
    {
        int dano = CalcularDanoAoAluno(aluno, DanoBaseChefe);
        aluno.ReceberDano(dano);
    }

    public override void AtaqueEspecial(Aluno aluno)
    {
        contextoExame.MultiplicadorAnulado = true;
    }

    public override List<Pergunta> GetPerguntasExame()
    {
        return new List<Pergunta>
        {
            new Pergunta(
                "Qual pilar oculta detalhes internos e expoe apenas o necessario?",
                new[] { "Encapsulamento", "Heranca", "Polimorfismo", "Abstracao" },
                0,
                "Encapsulamento controla acesso a dados internos."),
            new Pergunta(
                "Heranca permite:",
                new[] { "Reutilizar codigo de uma classe base", "Executar codigo em paralelo", "Acessar banco de dados", "Criar interfaces" },
                0,
                "Heranca reaproveita comportamento da classe base."),
            new Pergunta(
                "Polimorfismo permite:",
                new[] { "Mesmo metodo com comportamentos diferentes", "Somente heranca simples", "Tipos primitivos", "Variaveis globais" },
                0,
                "Polimorfismo varia comportamento por tipo."),
            new Pergunta(
                "Abstracao significa:",
                new[] { "Modelar apenas aspectos essenciais", "Esconder dados com private", "Repetir codigo", "Evitar heranca" },
                0,
                "Abstracao foca no essencial do modelo."),
            new Pergunta(
                "Construtor em C# serve para:",
                new[] { "Inicializar o objeto", "Destruir o objeto", "Compilar o projeto", "Executar testes" },
                0,
                "Construtor inicializa o estado do objeto."),
            new Pergunta(
                "Objeto e:",
                new[] { "Instancia de uma classe", "Definicao de metodo", "Variavel global", "Biblioteca" },
                0,
                "Objeto e uma instancia concreta."),
            new Pergunta(
                "Override e usado para:",
                new[] { "Reescrever metodo virtual da classe base", "Criar interface", "Declarar campo", "Importar namespace" },
                0,
                "Override substitui comportamento da classe base."),
            new Pergunta(
                "Composicao e preferida quando:",
                new[] { "A classe depende de outra como parte", "A classe deve herdar tudo", "Nao ha reutilizacao", "So existe heranca multipla" },
                0,
                "Composicao modela relacao de parte e todo."),
        };
    }
}
