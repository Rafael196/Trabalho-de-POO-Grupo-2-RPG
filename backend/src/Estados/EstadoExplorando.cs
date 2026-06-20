using System;
using CampusQuest.Core;
using CampusQuest.Materias;
using CampusQuest.NPCs;
using CampusQuest.Persistencia;
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
        io.WriteLine("6) Usar item do inventario");
        io.WriteLine("7) Voltar ao menu");
        io.Write("Opcao: ");

        string entrada = io.ReadLine();
        switch (entrada)
        {
            case "1":
                InteragirVeterano(contexto.AlunoAtivo);
                break;
            case "2":
                {
                    InteragirProfessor(contexto.AlunoAtivo, contexto.RepositorioQuestoes);
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
                UsarItemInventario(contexto.AlunoAtivo);
                break;
            case "7":
                contexto.MudarEstado(new EstadoMenu(io));
                break;
            default:
                io.WriteLine("Opcao invalida.");
                break;
        }

        contexto.SalvarProgresso();
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
                    if (item == null)
                    {
                        io.WriteLine("Nenhum item disponivel no momento.");
                        break;
                    }

                    if (!aluno.PodeReceberItem(item))
                    {
                        io.WriteLine("Limite de itens do semestre atingido. Item nao adicionado.");
                    }
                    else if (aluno.Inventario.Adicionar(item))
                    {
                        aluno.RegistrarItemRecebido(item);
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

    private void InteragirProfessor(Aluno aluno, IRepositorioQuestoes repositorioQuestoes)
    {
        if (aluno == null)
        {
            return;
        }

        if (repositorioQuestoes == null)
        {
            io.WriteLine("Erro: Repositorio de questoes nao configurado.");
            return;
        }

        Professor professor = new Professor(repositorioQuestoes, io);
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

    private void UsarItemInventario(Aluno aluno)
    {
        if (aluno?.Inventario == null)
        {
            return;
        }

        var itens = aluno.Inventario.ListarItens();
        if (itens.Count == 0)
        {
            io.WriteLine("Inventario vazio.");
            return;
        }

        io.WriteLine("Escolha um item para usar (0 para cancelar):");
        for (int i = 0; i < itens.Count; i++)
        {
            io.WriteLine($"{i + 1}. {ObterNomeItem(itens[i])}");
        }

        string entrada = io.ReadLine();
        if (!int.TryParse(entrada, out int escolha))
        {
            io.WriteLine("Entrada invalida.");
            return;
        }

        if (escolha == 0)
        {
            io.WriteLine("Uso de item cancelado.");
            return;
        }

        int indice = escolha - 1;
        if (indice < 0 || indice >= itens.Count)
        {
            io.WriteLine("Opcao invalida.");
            return;
        }

        Core.Item item = itens[indice];
        if (item is not CampusQuest.Itens.Item itemDetalhe)
        {
            io.WriteLine("Item invalido.");
            return;
        }

        if (itemDetalhe is CampusQuest.Itens.Cola)
        {
            io.WriteLine("A cola so pode ser usada durante o exame.");
            return;
        }

        itemDetalhe.Usar(aluno);
        aluno.Inventario.Remover(item);
        aluno.RegistrarItemUsado();
        io.WriteLine(itemDetalhe.GetDescricaoEfeito());
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
