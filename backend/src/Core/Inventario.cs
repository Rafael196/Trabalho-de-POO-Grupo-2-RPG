using System.Collections.Generic;

namespace CampusQuest.Core;

public class Inventario
{
    public const int CapacidadeMaxima = 6;

    private readonly List<Item> itens = new();

    public int Quantidade => itens.Count;

    public bool Adicionar(Item item)
    {
        if (item == null || EstaCheia())
        {
            return false;
        }

        itens.Add(item);
        return true;
    }

    public bool Remover(Item item)
    {
        if (item == null)
        {
            return false;
        }

        return itens.Remove(item);
    }

    public bool EstaCheia()
    {
        return Quantidade >= CapacidadeMaxima;
    }

    public List<Item> ListarItens()
    {
        return new List<Item>(itens);
    }

    public Item BuscarPorTipo<T>() where T : Item
    {
        for (int i = 0; i < itens.Count; i++)
        {
            if (itens[i] is T)
            {
                return itens[i];
            }
        }

        return null;
    }
}
