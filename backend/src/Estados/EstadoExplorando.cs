using System;
using CampusQuest.Core;
using CampusQuest.Materias;
using CampusQuest.NPCs;
using CampusQuest.UI;

namespace CampusQuest.Estados;

public class EstadoExplorando : IEstadoJogo
{
    private readonly IConsoleIO io;

    public EstadoExplorando(IConsoleIO? io = null)
    {
        this.io = io ?? new ConsoleIO();
    }

    public void Entrar(JogoContexto contexto)
    {
        io.WriteLine("=== Hall ===");
    }

    public void Executar(JogoContexto contexto)
    {
        if (contexto.AlunoAtivo == null)
        {
            contexto.MudarEstado(new EstadoMenu(io));
            return;
        }

        io.WriteLine("1) Falar com veterano (dicas/itens)");
        io.WriteLine("2) Ir para sala do professor (quiz)");
        io.WriteLine("3) Ir para sala de exame (batalha)");
        io.WriteLine("4) Ir para sala da coordenacao (trancar semestre)");
        io.WriteLine("5) Ver status e inventario");
        io.WriteLine("6) Voltar ao menu");
        io.Write("Opcao: ");

        string entrada = io.ReadLine();
        switch (entrada)
        {
            case "1":
                InteragirVeterano(contexto.AlunoAtivo);
                break;
            case "2":
            {
                InteragirProfessor(contexto.AlunoAtivo);
                break;
            }
            case "3":
            {
                Materia chefe = CriarChefe(contexto.AlunoAtivo);
                contexto.MudarEstado(new EstadoExame(chefe, io));
                break;
            }
            case "4":
            {
                InteragirCoordenador(contexto.AlunoAtivo, contexto.Repositorio);

                break;
            }
            case "5":
                MostrarStatus(contexto.AlunoAtivo);
                break;
            case "6":
                contexto.MudarEstado(new EstadoMenu(io));
                break;
            default:
                io.WriteLine("Opcao invalida.");
                break;
        }
    }

    public void Sair(JogoContexto contexto) { }

    private void InteragirVeterano(Aluno aluno)
    {
        if (aluno == null)
        {
            return;
        }

        Veterano veterano = new Veterano();
        io.WriteLine(veterano.GetMensagemAbertura(aluno));
        veterano.Interagir(aluno);

        io.WriteLine("1) Pedir item para a prova");
        io.WriteLine("2) Pedir dica de estudo");
        io.WriteLine("3) Encerrar conversa");
        io.Write("Opcao: ");

        string resposta = io.ReadLine();
        switch (resposta)
        {
            case "1":
            {
                Core.Item item = veterano.SolicitarItem(aluno);
                if (aluno.Inventario.Adicionar(item))
                {
                    io.WriteLine($"Item recebido: {ObterNomeItem(item)}");
                }
                else
                {
                    io.WriteLine("Inventario cheio. Item nao adicionado.");
                }

                break;
            }
            case "2":
                io.WriteLine(veterano.ObterDica(aluno));
                break;
            default:
                io.WriteLine("Conversa encerrada.");
                break;
        }
    }

    private void InteragirProfessor(Aluno aluno)
    {
        if (aluno == null)
        {
            return;
        }

        Professor professor = new Professor(io);
        io.WriteLine(professor.GetMensagemAbertura(aluno));
        io.WriteLine("1) Pedir item para a prova");
        io.WriteLine("2) Pedir dica de estudo");
        io.WriteLine("3) Fazer quiz");
        io.WriteLine("4) Encerrar conversa");
        io.Write("Opcao: ");

        string resposta = io.ReadLine();
        switch (resposta)
        {
            case "1":
                io.WriteLine(professor.MensagemItemIndisponivel(aluno));
                break;
            case "2":
                io.WriteLine(professor.ObterDica(aluno));
                break;
            case "3":
                professor.Interagir(aluno);
                io.WriteLine(professor.GetDialogo());
                break;
            default:
                io.WriteLine("Conversa encerrada.");
                break;
        }
    }

    private void InteragirCoordenador(Aluno aluno, Persistencia.IRepositorio repositorio)
    {
        if (aluno == null)
        {
            return;
        }

        Coordenador coordenador = new Coordenador(repositorio);
        io.WriteLine(coordenador.GetMensagemAbertura(aluno));
        io.WriteLine("1) Pedir item para a prova");
        io.WriteLine("2) Pedir dica");
        io.WriteLine("3) Trancar semestre");
        io.WriteLine("4) Encerrar conversa");
        io.Write("Opcao: ");

        string resposta = io.ReadLine();
        switch (resposta)
        {
            case "1":
                io.WriteLine(coordenador.MensagemItemIndisponivel(aluno));
                break;
            case "2":
                io.WriteLine(coordenador.ObterDica(aluno));
                break;
            case "3":
            {
                bool trancado = coordenador.TrancarSemestre(aluno, repositorio);
                io.WriteLine(coordenador.GetDialogo());
                if (!trancado)
                {
                    io.WriteLine("Tente novamente mais tarde.");
                }

                break;
            }
            default:
                io.WriteLine("Conversa encerrada.");
                break;
        }
    }

    private void MostrarStatus(Aluno aluno)
    {
        if (aluno == null)
        {
            return;
        }

        io.WriteLine("=== Status do Aluno ===");
        io.WriteLine($"Vida: {aluno.Vida}/{aluno.VidaMaxima}");
        io.WriteLine($"Conhecimento: {aluno.Conhecimento}");
        io.WriteLine($"Semestre: {aluno.SemestreAtual}");
        io.WriteLine($"Inventario: {aluno.Inventario.Quantidade}/{Inventario.CapacidadeMaxima}");

        var itens = aluno.Inventario.ListarItens();
        if (itens.Count == 0)
        {
            io.WriteLine("Nenhum item no inventario.");
            return;
        }

        for (int i = 0; i < itens.Count; i++)
        {
            io.WriteLine($"- {ObterNomeItem(itens[i])}");
        }
    }

    private static string ObterNomeItem(Core.Item item)
    {
        if (item is CampusQuest.Itens.Item itemDetalhe && !string.IsNullOrWhiteSpace(itemDetalhe.Nome))
        {
            return itemDetalhe.Nome;
        }

        return item?.GetType().Name ?? "Item";
    }

    private static Materia CriarChefe(Aluno aluno)
    {
        if (aluno.SemestresCompletos())
        {
            return new TCC(aluno.GetMediaFinal());
        }

        return aluno.SemestreAtual switch
        {
            1 => new IC(),
            2 => new AED(),
            3 => new POO(),
            _ => new IC()
        };
    }
}
