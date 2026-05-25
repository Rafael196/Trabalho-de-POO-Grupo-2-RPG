using CampusQuest.Core;

namespace CampusQuest.Eventos;

public class ItemAdquiridoEvento : IEventoJogo
{
    public Item Item { get; }

    public ItemAdquiridoEvento(Item item)
    {
        Item = item;
    }
}
