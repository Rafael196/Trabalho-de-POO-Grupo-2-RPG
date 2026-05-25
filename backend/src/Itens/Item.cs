using CampusQuest.Core;

namespace CampusQuest.Itens;

public abstract class Item : Core.Item
{
    public string Nome { get; protected set; }
    public string Descricao { get; protected set; }

    public abstract void Usar(Aluno aluno);
    public abstract string GetDescricaoEfeito();
}