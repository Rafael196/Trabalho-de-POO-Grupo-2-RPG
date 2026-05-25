using System;
using CampusQuest.Core;
using CampusQuest.Persistencia;
using CampusQuest.UI;

namespace CampusQuest.Estados;

public class EstadoMenu : IEstadoJogo
{
    private readonly IConsoleIO io;

    public EstadoMenu(IConsoleIO? io = null)
    {
        this.io = io ?? new ConsoleIO();
    }

    public void Entrar(JogoContexto contexto)
    {
        io.WriteLine("=== Menu Principal ===");
    }

    public void Executar(JogoContexto contexto)
    {
        io.WriteLine("1) Novo jogo");
        io.WriteLine("2) Carregar jogo");
        io.WriteLine("3) Sair");
        io.Write("Opcao: ");

        string entrada = io.ReadLine();
        switch (entrada)
        {
            case "1":
                contexto.AlunoAtivo = new Aluno();
                SolicitarNomeJogador(contexto.AlunoAtivo);
                contexto.MudarEstado(new EstadoExplorando(io));
                break;
            case "2":
                CarregarJogo(contexto);
                break;
            case "3":
                contexto.MudarEstado(new EstadoSair());
                break;
            default:
                io.WriteLine("Opcao invalida.");
                break;
        }
    }

    public void Sair(JogoContexto contexto) { }

    private void CarregarJogo(JogoContexto contexto)
    {
        if (contexto.Repositorio == null)
        {
            io.WriteLine("Repositorio nao configurado.");
            return;
        }

        if (!contexto.Repositorio.ExisteArquivo())
        {
            io.WriteLine("Nenhum save encontrado.");
            return;
        }

        EstadoJogo estado = contexto.Repositorio.Carregar();
        if (!string.IsNullOrWhiteSpace(estado.NomeAluno))
        {
            io.WriteLine($"Save encontrado: {estado.NomeAluno}");
        }
        else
        {
            io.WriteLine("Save encontrado: Jogador");
        }

        Aluno aluno = new Aluno();

        if (!string.IsNullOrWhiteSpace(estado.NomeAluno))
        {
            aluno.DefinirNome(estado.NomeAluno);
        }

        int conhecimento = Math.Max(0, estado.Conhecimento);
        aluno.AumentarConhecimento(conhecimento);

        int dano = Math.Max(0, aluno.Vida - estado.VidaAtual);
        if (dano > 0)
        {
            aluno.ReceberDano(dano);
        }

        int semestreSalvo = Math.Max(1, Math.Min(3, estado.SemestreAtual));
        while (aluno.SemestreAtual < semestreSalvo)
        {
            aluno.AvancarSemestre();
        }

        contexto.AlunoAtivo = aluno;
        contexto.MudarEstado(new EstadoExplorando(io));
    }

    private void SolicitarNomeJogador(Aluno aluno)
    {
        io.Write("Nome do jogador: ");
        string nome = io.ReadLine();
        aluno.DefinirNome(nome);
    }
}
