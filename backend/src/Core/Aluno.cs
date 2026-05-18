using System;
using System.Collections.Generic;

namespace CampusQuest.Core;

public class Aluno : Personagem
{
    public Inventario Inventario { get; private set; }
    public List<Habilidade> Habilidades { get; private set; }
    public int SemestreAtual { get; private set; }
    public int[] Aproveitamentos { get; private set; }
    public bool BonusCoragemAplicado { get; private set; }

    public Aluno()
    {
        Inventario = new Inventario();
        Habilidades = new List<Habilidade>();
        SemestreAtual = 1;
        Aproveitamentos = new int[3];
        BonusCoragemAplicado = false;
        vidaMaxima = 100;
        vida = vidaMaxima;
        conhecimento = 0;
    }

    public override void Atacar(Personagem alvo)
    {
        if (alvo == null)
        {
            throw new ArgumentNullException(nameof(alvo));
        }

        double danoCalculado = 15 * (1 + Conhecimento / 100.0);
        int dano = (int)Math.Round(danoCalculado, MidpointRounding.AwayFromZero);
        alvo.ReceberDano(dano);
    }

    public void AdicionarHabilidade(Habilidade h)
    {
        if (h == null)
        {
            return;
        }

        Habilidades.Add(h);
    }

    public void RegistrarAproveitamento(int semestre, int valor)
    {
        if (Aproveitamentos == null || Aproveitamentos.Length < 3)
        {
            Aproveitamentos = new int[3];
        }

        if (semestre < 1 || semestre > 3)
        {
            return;
        }

        int valorNormalizado = Math.Max(0, Math.Min(100, valor));
        Aproveitamentos[semestre - 1] = valorNormalizado;
    }

    public float GetMediaFinal()
    {
        if (Aproveitamentos == null || Aproveitamentos.Length == 0)
        {
            return 0f;
        }

        int soma = 0;
        for (int i = 0; i < Aproveitamentos.Length; i++)
        {
            soma += Aproveitamentos[i];
        }

        return soma / (float)Aproveitamentos.Length;
    }

    public void AumentarConhecimento(int quantidade)
    {
        int novoValor = Conhecimento + quantidade;
        Conhecimento = Math.Max(0, novoValor);
    }

    public void AvancarSemestre()
    {
        SemestreAtual++;
    }

    public bool SemestresCompletos()
    {
        return SemestreAtual > 3;
    }
}
