using System;
using System.Collections.Generic;
using CampusQuest.Core;

namespace CampusQuest.Itens;

public class LivroTecnico : Item
{
    public string MateriaAlvo { get; }
    public const int BonusConhecimento = 20;

    private static readonly Dictionary<Aluno, Dictionary<string, int>> BonusPorAluno = new();

    public LivroTecnico(string materiaAlvo)
    {
        MateriaAlvo = string.IsNullOrWhiteSpace(materiaAlvo) ? string.Empty : materiaAlvo.Trim();
        Nome = "Livro Técnico";
        Descricao = "Aprofunda uma matéria específica.";
    }

    public override void Usar(Aluno aluno)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        if (!BonusPorAluno.TryGetValue(aluno, out Dictionary<string, int> bonusMateria))
        {
            bonusMateria = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            BonusPorAluno[aluno] = bonusMateria;
        }

        bonusMateria[MateriaAlvo] = BonusConhecimento;
    }

    public static int ObterBonus(Aluno aluno, string materiaAlvo)
    {
        if (aluno == null || string.IsNullOrWhiteSpace(materiaAlvo))
        {
            return 0;
        }

        if (!BonusPorAluno.TryGetValue(aluno, out Dictionary<string, int> bonusMateria))
        {
            return 0;
        }

        return bonusMateria.TryGetValue(materiaAlvo, out int bonus) ? bonus : 0;
    }

    public override string GetDescricaoEfeito()
    {
        if (string.IsNullOrWhiteSpace(MateriaAlvo))
        {
            return $"Concede +{BonusConhecimento} de Conhecimento permanente em uma matéria alvo.";
        }

        return $"Concede +{BonusConhecimento} de Conhecimento permanente em {MateriaAlvo}.";
    }
}