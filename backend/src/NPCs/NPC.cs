using CampusQuest.Core;

namespace CampusQuest.NPCs;

public abstract class NPC
{
    public string Nome { get; protected set; }

    public abstract void Interagir(Aluno aluno);
    public abstract string GetDialogo();
}