using CampusQuest.Core;
using CampusQuest.Exame;
using CampusQuest.Materias;
using CampusQuest.UI;

namespace CampusQuest.Estados;

public class EstadoExame : IEstadoJogo
{
    private readonly Materia chefe;
    private readonly IConsoleIO io;

    public EstadoExame(Materia chefe, IConsoleIO? io = null)
    {
        this.chefe = chefe;
        this.io = io ?? new ConsoleIO();
    }

    public void Entrar(JogoContexto contexto)
    {
        io.WriteLine($"=== Exame: {chefe.Nome} ===");
    }

    public void Executar(JogoContexto contexto)
    {
        if (contexto.AlunoAtivo == null)
        {
            contexto.MudarEstado(new EstadoMenu(io));
            return;
        }

        SistemaExame sistema = new SistemaExame(io);
        ResultadoExame resultado = sistema.Executar(contexto.AlunoAtivo, chefe);

        if (resultado.Vitoria)
        {
            if (chefe is TCC)
            {
                contexto.MudarEstado(new EstadoVitoria(resultado, io));
                return;
            }

            contexto.AlunoAtivo.AvancarSemestre();
            contexto.MudarEstado(new EstadoExplorando(io));
            return;
        }

        if (chefe is TCC)
        {
            contexto.MudarEstado(new EstadoGameOver(io));
            return;
        }

        contexto.MudarEstado(new EstadoExplorando(io));
    }

    public void Sair(JogoContexto contexto) { }
}
