using System.IO;
using CampusQuest.Estados;
using CampusQuest.Eventos;
using CampusQuest.Persistencia;
using CampusQuest.UI;

namespace CampusQuest;

internal static class Program
{
    private static void Main()
    {
        // Inicializar banco de dados
        DatabaseInitializer.Inicializar();

        IConsoleIO io = new ConsoleIO();
        RegistrarObservadores(io);

        // Criar repositórios
        IRepositorioQuestoes repositorioQuestoes = new SqliteRepositorioQuestoes("database/campusquest.db");
        IRepositorio repositorioSave = new SqliteRepositorioSave("database/campusquest.db");

        // Montar contexto com injeções
        JogoContexto contexto = new JogoContexto(io)
        {
            Repositorio = repositorioSave,
            RepositorioQuestoes = repositorioQuestoes
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
