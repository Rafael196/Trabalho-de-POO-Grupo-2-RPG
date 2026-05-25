using System;
using CampusQuest.Core;

namespace CampusQuest.Itens;

public class Cola : Item
{
    public Cola()
    {
        Nome = "Cola";
        Descricao = "Garante um acerto na proxima pergunta do exame.";
    }

    public override void Usar(Aluno aluno)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        aluno.AtivarCola();
    }

    public override string GetDescricaoEfeito()
    {
        return "Garante um acerto na proxima pergunta do exame.";
    }
}
