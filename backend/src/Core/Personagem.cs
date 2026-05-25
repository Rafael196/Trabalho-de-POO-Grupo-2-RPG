using System;

namespace CampusQuest.Core;

public abstract class Personagem
{
    protected int vida;
    protected int vidaMaxima;
    protected int conhecimento;

    public int Vida
    {
        get => vida;
        protected set => vida = value;
    }

    public int VidaMaxima
    {
        get => vidaMaxima;
        protected set => vidaMaxima = value;
    }

    public int Conhecimento
    {
        get => conhecimento;
        protected set => conhecimento = value;
    }

    public abstract void Atacar(Personagem alvo);

    public void ReceberDano(int dano)
    {
        int danoAplicado = Math.Max(0, dano);
        Vida = Math.Max(0, Vida - danoAplicado);
    }

    public bool EstaVivo()
    {
        return Vida > 0;
    }

    public void RestaurarVida(int quantidade)
    {
        int curaAplicada = Math.Max(0, quantidade);
        Vida = Math.Min(VidaMaxima, Vida + curaAplicada);
    }
}
