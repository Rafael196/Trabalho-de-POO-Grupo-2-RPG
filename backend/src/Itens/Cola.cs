using System;
using CampusQuest.Core;

namespace CampusQuest.Itens;

public class Cola : Item
{
    public Cola()
    {
        Nome = "Cola";
        Descricao = "Elimina duas alternativas no exame — uso único.";
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
        return "Elimina duas alternativas no exame — uso único.";
    }
}
