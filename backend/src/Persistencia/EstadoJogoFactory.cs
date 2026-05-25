using System;
using System.Collections.Generic;
using CampusQuest.Core;
using CampusQuest.Itens;

namespace CampusQuest.Persistencia;

public static class EstadoJogoFactory
{
    public static EstadoJogo Criar(Aluno aluno)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        EstadoJogo estado = new EstadoJogo
        {
            Versao = "1.0",
            DataSalvamento = DateTime.UtcNow,
            NomeAluno = aluno.Nome ?? string.Empty,
            VidaAtual = aluno.Vida,
            Conhecimento = aluno.Conhecimento,
            SemestreAtual = aluno.SemestreAtual
        };

        estado.Aluno = new DadosAluno
        {
            Nome = aluno.Nome ?? string.Empty,
            VidaAtual = aluno.Vida,
            VidaMaxima = aluno.VidaMaxima,
            Conhecimento = aluno.Conhecimento,
            SemestreAtual = aluno.SemestreAtual,
            Habilidades = MapearHabilidades(aluno.Habilidades),
            Aproveitamentos = (int[])(aluno.Aproveitamentos?.Clone() ?? Array.Empty<int>()),
            Inventario = MapearInventario(aluno.Inventario),
            CafesRecebidosSemestre = aluno.CafesRecebidosSemestre,
            CadernosRecebidosSemestre = aluno.CadernosRecebidosSemestre,
            LivrosRecebidosSemestre = aluno.LivrosRecebidosSemestre
        };

        return estado;
    }

    public static void Aplicar(Aluno aluno, EstadoJogo estado)
    {
        if (aluno == null || estado == null)
        {
            return;
        }

        if (estado.Aluno != null)
        {
            AplicarDadosAluno(aluno, estado.Aluno);
            return;
        }

        if (!string.IsNullOrWhiteSpace(estado.NomeAluno))
        {
            aluno.DefinirNome(estado.NomeAluno);
        }

        aluno.AumentarConhecimento(Math.Max(0, estado.Conhecimento));
        AjustarVida(aluno, estado.VidaAtual);
        AjustarSemestre(aluno, estado.SemestreAtual);
    }

    private static void AplicarDadosAluno(Aluno aluno, DadosAluno dados)
    {
        if (!string.IsNullOrWhiteSpace(dados.Nome))
        {
            aluno.DefinirNome(dados.Nome);
        }

        aluno.AumentarConhecimento(Math.Max(0, dados.Conhecimento));
        AjustarVida(aluno, dados.VidaAtual);
        AjustarSemestre(aluno, dados.SemestreAtual);

        if (dados.Aproveitamentos != null)
        {
            for (int i = 0; i < dados.Aproveitamentos.Length; i++)
            {
                aluno.RegistrarAproveitamento(i + 1, dados.Aproveitamentos[i]);
            }
        }

        if (dados.Inventario != null)
        {
            foreach (ItemSalvo item in dados.Inventario)
            {
                Core.Item itemCriado = CriarItem(item);
                if (itemCriado != null)
                {
                    aluno.Inventario.Adicionar(itemCriado);
                }
            }
        }

        aluno.DefinirContagemItensSemestre(
            dados.CafesRecebidosSemestre,
            dados.CadernosRecebidosSemestre,
            dados.LivrosRecebidosSemestre);

        AplicarHabilidadesSalvas(aluno, dados.Habilidades);
    }

    private static void AjustarVida(Aluno aluno, int vidaAlvo)
    {
        int alvoNormalizado = Math.Max(0, vidaAlvo);
        int diferenca = aluno.Vida - alvoNormalizado;
        if (diferenca > 0)
        {
            aluno.ReceberDano(diferenca);
            return;
        }

        if (diferenca < 0)
        {
            aluno.RestaurarVida(-diferenca);
        }
    }

    private static void AjustarSemestre(Aluno aluno, int semestreAlvo)
    {
        int semestreNormalizado = Math.Max(1, Math.Min(3, semestreAlvo));
        while (aluno.SemestreAtual < semestreNormalizado)
        {
            aluno.AvancarSemestre();
        }
    }

    private static List<string> MapearHabilidades(List<Habilidade> habilidades)
    {
        List<string> resultado = new();
        if (habilidades == null)
        {
            return resultado;
        }

        foreach (Habilidade habilidade in habilidades)
        {
            if (!string.IsNullOrWhiteSpace(habilidade?.Nome))
            {
                resultado.Add(habilidade.Nome);
            }
        }

        return resultado;
    }

    private static void AplicarHabilidadesSalvas(Aluno aluno, List<string> habilidadesSalvas)
    {
        if (aluno == null || habilidadesSalvas == null)
        {
            return;
        }

        foreach (string nome in habilidadesSalvas)
        {
            Habilidade habilidade = HabilidadeCatalogo.ObterPorNome(nome);
            if (habilidade != null)
            {
                aluno.AdicionarHabilidade(habilidade);
            }
        }
    }

    private static List<ItemSalvo> MapearInventario(Inventario inventario)
    {
        List<ItemSalvo> resultado = new();
        if (inventario == null)
        {
            return resultado;
        }

        foreach (Core.Item item in inventario.ListarItens())
        {
            ItemSalvo salvo = new ItemSalvo
            {
                Tipo = item?.GetType().Name ?? string.Empty,
                Nome = item is Itens.Item itemDetalhe ? itemDetalhe.Nome : string.Empty,
                Descricao = item is Itens.Item itemDetalhe2 ? itemDetalhe2.Descricao : string.Empty,
                MateriaAlvo = item is LivroTecnico livro ? livro.MateriaAlvo : string.Empty
            };

            resultado.Add(salvo);
        }

        return resultado;
    }

    private static Core.Item CriarItem(ItemSalvo item)
    {
        if (item == null || string.IsNullOrWhiteSpace(item.Tipo))
        {
            return null;
        }

        return item.Tipo switch
        {
            nameof(Cafe) => new Cafe(),
            nameof(Caderno) => new Caderno(),
            nameof(LivroTecnico) => new LivroTecnico(item.MateriaAlvo ?? string.Empty),
            nameof(Cola) => new Cola(),
            _ => null
        };
    }
}
