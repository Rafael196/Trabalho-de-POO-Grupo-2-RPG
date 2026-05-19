using System.Collections.Generic;
using CampusQuest.Core;
using CampusQuest.Quiz;

namespace CampusQuest.Materias;

public class IC : Materia
{
    public IC()
    {
        Nome = "IC";
        vidaMaxima = 100;
        vidaAtual = vidaMaxima;
        descricaoAtaque = "Loop Infinito";
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
    }

    public override List<Pergunta> GetPerguntasExame()
    {
        return new List<Pergunta>
        {
            new Pergunta(
                "Qual componente armazena dados temporariamente e se perde ao desligar o computador?",
                new[] { "RAM", "SSD", "HD", "ROM" },
                0,
                "A memoria RAM e volatil e perde dados ao desligar."),
            new Pergunta(
                "O que significa a sigla CPU?",
                new[] { "Central Processing Unit", "Control Program Unit", "Central Peripheral Unit", "Compute Processing Utility" },
                0,
                "CPU significa Central Processing Unit."),
            new Pergunta(
                "Qual base numerica usa apenas os digitos 0 e 1?",
                new[] { "Binaria", "Decimal", "Octal", "Hexadecimal" },
                0,
                "A base binaria usa apenas 0 e 1."),
            new Pergunta(
                "Qual software gerencia hardware, processos e memoria?",
                new[] { "Sistema operacional", "Editor de texto", "Compilador", "Navegador" },
                0,
                "O sistema operacional gerencia recursos do computador."),
            new Pergunta(
                "Qual dispositivo encaminha pacotes entre redes diferentes?",
                new[] { "Roteador", "Switch", "Hub", "Repetidor" },
                0,
                "O roteador conecta redes distintas."),
            new Pergunta(
                "Qual unidade mede a frequencia de processadores?",
                new[] { "GHz", "MB", "dpi", "Volt" },
                0,
                "GHz mede ciclos por segundo do processador."),
            new Pergunta(
                "Qual e um exemplo de software de aplicacao?",
                new[] { "Editor de texto", "BIOS", "Firmware", "Driver de dispositivo" },
                0,
                "Editor de texto e um software voltado ao usuario."),
            new Pergunta(
                "Qual dispositivo oferece armazenamento nao volatil e rapido?",
                new[] { "SSD", "RAM", "Cache L1", "Registrador" },
                0,
                "SSD e nao volatil e mais rapido que HD."),
        };
    }
}
