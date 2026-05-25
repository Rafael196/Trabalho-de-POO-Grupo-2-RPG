using CampusQuest.UI;

namespace CampusQuest.Estados;

public class EstadoGameOver : IEstadoJogo
{
    private readonly IConsoleIO io;

    public EstadoGameOver(IConsoleIO? io = null)
    {
        this.io = io ?? new ConsoleIO();
    }

    public void Entrar(JogoContexto contexto)
    {
        io.WriteLine("=== Game Over ===");
        io.WriteLine("O TCC foi forte demais desta vez.");
    }

    public void Executar(JogoContexto contexto)
    {
        io.WriteLine("Retornando ao menu principal...");
        contexto.MudarEstado(new EstadoMenu(io));
    }

    public void Sair(JogoContexto contexto) { }
}
