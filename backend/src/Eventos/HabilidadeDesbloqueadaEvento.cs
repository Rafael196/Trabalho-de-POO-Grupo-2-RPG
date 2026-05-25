using CampusQuest.Core;

namespace CampusQuest.Eventos;

public class HabilidadeDesbloqueadaEvento : IEventoJogo
{
    public Habilidade Habilidade { get; }

    public HabilidadeDesbloqueadaEvento(Habilidade habilidade)
    {
        Habilidade = habilidade;
    }
}
