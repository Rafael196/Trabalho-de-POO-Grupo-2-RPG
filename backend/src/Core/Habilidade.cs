using System;
using CampusQuest.Exame;

namespace CampusQuest.Core;

public class Habilidade
{
    public string Nome { get; }
    public string Descricao { get; }
    public TipoEfeito Efeito { get; }

    public Habilidade(string nome, string descricao, TipoEfeito efeito)
    {
        Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        Descricao = descricao ?? throw new ArgumentNullException(nameof(descricao));
        Efeito = efeito;
    }

    public void Aplicar(Aluno aluno, ContextoExame contexto)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        if (contexto == null)
        {
            throw new ArgumentNullException(nameof(contexto));
        }

        switch (Efeito)
        {
            case TipoEfeito.AumentaDano:
                contexto.MultiplicadorAnulado = false;
                break;
            case TipoEfeito.ReducaoDano:
                contexto.DanoContinuo = false;
                contexto.TurnosComDano = 0;
                break;
            case TipoEfeito.DuploAtaque:
                contexto.DanoContinuo = true;
                contexto.TurnosComDano = Math.Max(contexto.TurnosComDano, 2);
                break;
            default:
                throw new InvalidOperationException("Tipo de efeito desconhecido.");
        }
    }
}

public enum TipoEfeito
{
    AumentaDano,
    ReducaoDano,
    DuploAtaque
}
