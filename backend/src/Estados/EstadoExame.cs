using CampusQuest.Core;
using CampusQuest.Exame;
using CampusQuest.Materias;
using CampusQuest.Persistencia;
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
                SalvarSePossivel(contexto);
                contexto.MudarEstado(new EstadoVitoria(resultado, io));
                return;
            }

            TentarDesbloquearHabilidade(contexto.AlunoAtivo, chefe);
            contexto.AlunoAtivo.AvancarSemestre();
            SalvarSePossivel(contexto);
            contexto.MudarEstado(new EstadoExplorando(io));
            return;
        }

        if (chefe is TCC)
        {
            SalvarSePossivel(contexto);
            contexto.MudarEstado(new EstadoGameOver(io));
            return;
        }

        SalvarSePossivel(contexto);
        contexto.MudarEstado(new EstadoExplorando(io));
    }

    public void Sair(JogoContexto contexto) { }

    private void SalvarSePossivel(JogoContexto contexto)
    {
        if (contexto?.Repositorio == null || contexto.AlunoAtivo == null)
        {
            return;
        }

        try
        {
            EstadoJogo estado = EstadoJogoFactory.Criar(contexto.AlunoAtivo);
            contexto.Repositorio.Salvar(estado);
            io.WriteLine("Jogo salvo.");
        }
        catch
        {
            io.WriteLine("Falha ao salvar o jogo.");
        }
    }

    private void TentarDesbloquearHabilidade(Aluno aluno, Materia materia)
    {
        if (aluno == null || materia == null)
        {
            return;
        }

        Habilidade habilidade = HabilidadeCatalogo.ObterPorChefe(materia.Nome);
        if (habilidade == null)
        {
            return;
        }

        aluno.AdicionarHabilidade(habilidade);
    }
}
