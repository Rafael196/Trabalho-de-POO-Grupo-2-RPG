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
    private readonly IRepositorioQuestoes repositorioQuestoes;

    public EstadoExame(Materia chefe, IConsoleIO? io = null, IRepositorioQuestoes repositorioQuestoes = null)
    {
        this.chefe = chefe;
        this.io = io ?? new ConsoleIO();
        this.repositorioQuestoes = repositorioQuestoes;
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

        SistemaExame sistema = new SistemaExame(io, repositorioQuestoes);
        ResultadoExame resultado = sistema.Executar(contexto.AlunoAtivo, chefe);

        if (resultado.Vitoria)
        {
            if (resultado.BonusCoragemAplicado)
            {
                contexto.AlunoAtivo.RegistrarBonusCoragemAplicado();
            }

            if (chefe is TCC)
            {
                contexto.SalvarProgresso(exibirMensagem: true);
                contexto.MudarEstado(new EstadoVitoria(resultado, io));
                return;
            }

            TentarDesbloquearHabilidade(contexto.AlunoAtivo, chefe);
            contexto.AlunoAtivo.AvancarSemestre();
            contexto.SalvarProgresso(exibirMensagem: true);
            contexto.MudarEstado(new EstadoExplorando(io));
            return;
        }

        if (chefe is TCC)
        {
            contexto.SalvarProgresso(exibirMensagem: true);
            contexto.MudarEstado(new EstadoGameOver(io));
            return;
        }

        contexto.AlunoAtivo.RegistrarSemestreRepetido();
        contexto.SalvarProgresso(exibirMensagem: true);
        contexto.MudarEstado(new EstadoExplorando(io));
    }

    public void Sair(JogoContexto contexto) { }

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
