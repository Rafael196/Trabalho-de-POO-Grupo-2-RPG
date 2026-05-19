using System;
using System.Collections.Generic;
using CampusQuest.Core;

namespace CampusQuest.Itens;

public class Caderno : Item
{
    public const int BonusConhecimento = 15;
    private static readonly Dictionary<Aluno, int> BonusTemporarioPorAluno = new();

    public Caderno()
    {
        Nome = "Caderno";
        Descricao = "Consolida anotações para a próxima avaliação.";
    }

    public override void Usar(Aluno aluno)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        BonusTemporarioPorAluno[aluno] = BonusConhecimento;
    }

    public static int ConsumirBonus(Aluno aluno)
    {
        if (aluno == null)
        {
            return 0;
        }

        if (!BonusTemporarioPorAluno.TryGetValue(aluno, out int bonus))
        {
            return 0;
        }

        BonusTemporarioPorAluno.Remove(aluno);
        return bonus;
    }

    public override string GetDescricaoEfeito()
    {
        return $"Concede +{BonusConhecimento} de Conhecimento temporário no próximo exame.";
    }
}