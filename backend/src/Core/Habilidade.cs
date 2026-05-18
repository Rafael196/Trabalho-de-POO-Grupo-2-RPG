using System;
using CampusQuest.Exame;
using CampusQuest.Materias;

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

    public void Aplicar(Aluno aluno, ContextoExame contexto, Materia chefe = null)
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
                break;
            case TipoEfeito.ReducaoDano:
                break;
            case TipoEfeito.DuploAtaque:
                if (chefe == null)
                {
                    throw new ArgumentNullException(nameof(chefe));
                }

                double conhecimentoEfetivo = contexto.MultiplicadorAnulado ? 0 : aluno.Conhecimento;
                double danoCalculado = 15 * (1 + conhecimentoEfetivo / 100.0);
                int dano = (int)Math.Round(danoCalculado, MidpointRounding.AwayFromZero);
                chefe.ReceberDano(dano);
                chefe.ReceberDano(dano);
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
