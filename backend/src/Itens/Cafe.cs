using System;
using CampusQuest.Core;

namespace CampusQuest.Itens;

public class Cafe : Item
{
    public const int RestauracaoVida = 25;

    public Cafe()
    {
        Nome = "Café";
        Descricao = "Restaura vida rapidamente.";
    }

    public override void Usar(Aluno aluno)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        aluno.RestaurarVida(RestauracaoVida);
    }

    public override string GetDescricaoEfeito()
    {
        return $"Restaura {RestauracaoVida} de vida.";
    }
}