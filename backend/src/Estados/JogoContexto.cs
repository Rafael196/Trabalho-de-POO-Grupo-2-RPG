using CampusQuest.Core;
using CampusQuest.Persistencia;
using CampusQuest.UI;

namespace CampusQuest.Estados;

public class JogoContexto
{
    public Aluno AlunoAtivo { get; set; }
    public IRepositorio Repositorio { get; set; }
    public IRepositorioQuestoes RepositorioQuestoes { get; set; }
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

    public bool SalvarProgresso(bool exibirMensagem = false)
    {
        if (Repositorio == null || AlunoAtivo == null)
        {
            return false;
        }

        try
        {
            EstadoJogo estado = EstadoJogoFactory.Criar(AlunoAtivo);
            Repositorio.Salvar(estado);

            if (exibirMensagem)
            {
                io.WriteLine("Jogo salvo.");
            }

            return true;
        }
        catch
        {
            if (exibirMensagem)
            {
                io.WriteLine("Falha ao salvar o jogo.");
            }

            return false;
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
