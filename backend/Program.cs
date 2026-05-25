using CampusQuest.Estados;
using CampusQuest.Eventos;
using CampusQuest.Persistencia;
using CampusQuest.UI;

namespace CampusQuest;

internal static class Program
{
    private static void Main()
    {
        IConsoleIO io = new ConsoleIO();
        RegistrarObservadores(io);
        JogoContexto contexto = new JogoContexto(io)
        {
            Repositorio = new StubRepositorio()
        };

        contexto.Iniciar();
    }

    private static void RegistrarObservadores(IConsoleIO io)
    {
        EventoBus.Inscrever<ItemAdquiridoEvento>(evento =>
        {
            if (evento?.Item != null)
            {
                io.WriteLine($"[Evento] Item adquirido: {evento.Item.GetType().Name}");
            }
        });

        EventoBus.Inscrever<HabilidadeDesbloqueadaEvento>(evento =>
        {
            if (evento?.Habilidade != null)
            {
                io.WriteLine($"[Evento] Habilidade desbloqueada: {evento.Habilidade.Nome}");
            }
        });
    }
}
