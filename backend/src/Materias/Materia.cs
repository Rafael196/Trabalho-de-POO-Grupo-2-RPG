using System;
using System.Collections.Generic;
using CampusQuest.Core;
using CampusQuest.Exame;
using CampusQuest.Quiz;

namespace CampusQuest.Materias;

public abstract class Materia
{
    protected const int DanoBaseChefe = 20;
    protected int vidaAtual;
    protected int vidaMaxima;
    protected string descricaoAtaque;
    protected ContextoExame contextoExame;

    public string Nome { get; protected set; }

    protected Materia()
    {
        Nome = string.Empty;
        descricaoAtaque = string.Empty;
        contextoExame = new ContextoExame();
    }

    public abstract void Atacar(Aluno aluno);
    public abstract void AtaqueEspecial(Aluno aluno);
    public abstract List<Pergunta> GetPerguntasExame();

    public void ReceberDano(int dano)
    {
        int danoAplicado = Math.Max(0, dano);
        vidaAtual = Math.Max(0, vidaAtual - danoAplicado);
    }

    public bool EstaVencido()
    {
        return vidaAtual <= 0;
    }

    public int GetVidaAtual()
    {
        return vidaAtual;
    }

    public int GetVidaMaxima()
    {
        return vidaMaxima;
    }

    public string GetDescricaoAtaque()
    {
        return descricaoAtaque;
    }

    protected int CalcularDanoAoAluno(Aluno aluno, double danoBase)
    {
        double dano = Math.Max(1, danoBase * (1 - aluno.Conhecimento / 100.0));
        int danoFinal = (int)Math.Round(dano, MidpointRounding.AwayFromZero);
        return Math.Max(1, danoFinal);
    }
}
