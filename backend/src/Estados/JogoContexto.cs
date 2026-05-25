using CampusQuest.Core;
using CampusQuest.Persistencia;
using CampusQuest.UI;

namespace CampusQuest.Estados;

public class JogoContexto
{
    public Aluno AlunoAtivo { get; set; }
    public IRepositorio Repositorio { get; set; }
    public IEstadoJogo EstadoAtual { get; private set; }

    private readonly IConsoleIO io;

    public JogoContexto(IConsoleIO? io = null)
    {
        this.io = io ?? new ConsoleIO();
    }

    public void MudarEstado(IEstadoJogo novoEstado)
    {
        EstadoAtual?.Sair(this);
        EstadoAtual = novoEstado;
        EstadoAtual?.Entrar(this);
    }

    public void Iniciar()
    {
        if (EstadoAtual == null)
        {
            MudarEstado(new EstadoMenu(io));
        }

        while (EstadoAtual != null)
        {
            if (EstadoAtual is EstadoSair)
            {
                break;
            }

            EstadoAtual.Executar(this);
        }
    }

    internal IConsoleIO IO => io;
}

internal sealed class EstadoSair : IEstadoJogo
{
    public void Entrar(JogoContexto contexto) { }
    public void Executar(JogoContexto contexto) { }
    public void Sair(JogoContexto contexto) { }
}
