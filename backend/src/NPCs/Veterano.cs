using System;
using CampusQuest.Core;
using GameItem = CampusQuest.Itens.Item;

namespace CampusQuest.NPCs;

public class Veterano : NPC
{
    private int ultimoSemestre;
    private string dialogoAtual = "Veterano do Hall pronto para ajudar.";

    public Veterano()
    {
        Nome = "Veterano";
        ultimoSemestre = 1;
    }

    public override void Interagir(Aluno aluno)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        ultimoSemestre = Math.Max(1, Math.Min(3, aluno.SemestreAtual));
        dialogoAtual = DarDica(ultimoSemestre);
    }

    public string DarDica(int semestreAtual)
    {
        ultimoSemestre = Math.Max(1, Math.Min(3, semestreAtual));

        return ultimoSemestre switch
        {
            1 => "IC: foque em hardware, software e sistemas operacionais.",
            2 => "AED: revise pilhas, filas, busca binária e complexidade.",
            3 => "POO: revise encapsulamento, herança, polimorfismo e abstração.",
            _ => "Mantenha o ritmo de estudos e prepare-se para o próximo desafio."
        };
    }

    public GameItem OfereceItem()
    {
        return ultimoSemestre switch
        {
            1 => new CampusQuest.Itens.Cafe(),
            2 => new CampusQuest.Itens.Caderno(),
            3 => new CampusQuest.Itens.LivroTecnico("POO"),
            _ => new CampusQuest.Itens.Cafe()
        };
    }

    public override string GetDialogo()
    {
        return dialogoAtual;
    }
}