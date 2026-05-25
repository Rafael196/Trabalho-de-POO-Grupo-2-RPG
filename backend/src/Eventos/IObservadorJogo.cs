namespace CampusQuest.Eventos;

public interface IObservadorJogo<in TEvento> where TEvento : IEventoJogo
{
    void AoReceberEvento(TEvento evento);
}
