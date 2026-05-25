using CampusQuest.Exame;
using CampusQuest.UI;

namespace CampusQuest.Estados;

public class EstadoVitoria : IEstadoJogo
{
    private readonly ResultadoExame resultado;
    private readonly IConsoleIO io;

    public EstadoVitoria(ResultadoExame resultado, IConsoleIO? io = null)
    {
        this.resultado = resultado;
        this.io = io ?? new ConsoleIO();
    }

    public void Entrar(JogoContexto contexto)
    {
        io.WriteLine("=== Vitoria ===");
        io.WriteLine("Parabens! Voce concluiu o TCC.");
        io.WriteLine($"Aproveitamento final: {resultado.Aproveitamento}");
        io.WriteLine($"Acertos: {resultado.AcertosTotal} | Erros: {resultado.ErrosTotal}");
    }

    public void Executar(JogoContexto contexto)
    {
        io.WriteLine("Voltando ao menu principal...");
        contexto.MudarEstado(new EstadoMenu(io));
    }

    public void Sair(JogoContexto contexto) { }
}
