using System;
using System.Collections.Generic;
using CampusQuest.Eventos;

namespace CampusQuest.Core;

public class Aluno : Personagem
{
    public string Nome { get; private set; }
    public Inventario Inventario { get; private set; }
    public List<Habilidade> Habilidades { get; private set; }
    public int SemestreAtual { get; private set; }
    public int[] Aproveitamentos { get; private set; }
    public bool BonusCoragemAplicado { get; private set; }
    public bool ColaAtiva { get; private set; }
    public int CafesRecebidosSemestre { get; private set; }
    public int CadernosRecebidosSemestre { get; private set; }
    public int LivrosRecebidosSemestre { get; private set; }

    public Aluno()
    {
        Nome = string.Empty;
        Inventario = new Inventario();
        Habilidades = new List<Habilidade>();
        SemestreAtual = 1;
        Aproveitamentos = new int[3];
        BonusCoragemAplicado = false;
        ColaAtiva = false;
        CafesRecebidosSemestre = 0;
        CadernosRecebidosSemestre = 0;
        LivrosRecebidosSemestre = 0;
        vidaMaxima = 100;
        vida = vidaMaxima;
        conhecimento = 0;
    }

    public void DefinirNome(string nome)
    {
        Nome = string.IsNullOrWhiteSpace(nome) ? "Jogador" : nome.Trim();
    }

    public override void Atacar(Personagem alvo)
    {
        if (alvo == null)
        {
            throw new ArgumentNullException(nameof(alvo));
        }

        double danoCalculado = 15 * (1 + Conhecimento / 100.0);
        int dano = (int)Math.Round(danoCalculado, MidpointRounding.AwayFromZero);
        alvo.ReceberDano(dano);
    }

    public void AdicionarHabilidade(Habilidade h)
    {
        if (h == null)
        {
            return;
        }

        if (PossuiHabilidade(h.Nome))
        {
            return;
        }

        Habilidades.Add(h);
        EventoBus.Publicar(new HabilidadeDesbloqueadaEvento(h));
    }

    public bool PossuiHabilidade(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome) || Habilidades == null)
        {
            return false;
        }

        for (int i = 0; i < Habilidades.Count; i++)
        {
            if (string.Equals(Habilidades[i]?.Nome, nome, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    public void RegistrarAproveitamento(int semestre, int valor)
    {
        if (Aproveitamentos == null || Aproveitamentos.Length < 3)
        {
            Aproveitamentos = new int[3];
        }

        if (semestre < 1 || semestre > 3)
        {
            return;
        }

        int valorNormalizado = Math.Max(0, Math.Min(100, valor));
        Aproveitamentos[semestre - 1] = valorNormalizado;
    }

    public float GetMediaFinal()
    {
        if (Aproveitamentos == null || Aproveitamentos.Length == 0)
        {
            return 0f;
        }

        int soma = 0;
        for (int i = 0; i < Aproveitamentos.Length; i++)
        {
            soma += Aproveitamentos[i];
        }

        return soma / (float)Aproveitamentos.Length;
    }

    public void AumentarConhecimento(int quantidade)
    {
        int novoValor = Conhecimento + quantidade;
        Conhecimento = Math.Max(0, novoValor);
    }

    public void AvancarSemestre()
    {
        SemestreAtual++;
        ResetarLimitesSemestre();
    }

    public bool SemestresCompletos()
    {
        return SemestreAtual > 3;
    }

    public void AtivarCola()
    {
        ColaAtiva = true;
    }

    public void DesativarCola()
    {
        ColaAtiva = false;
    }

    public bool PodeReceberItem(Item item)
    {
        if (item == null)
        {
            return false;
        }

        return item switch
        {
            CampusQuest.Itens.Cafe => CafesRecebidosSemestre < 3,
            CampusQuest.Itens.Caderno => CadernosRecebidosSemestre < 2,
            CampusQuest.Itens.LivroTecnico => LivrosRecebidosSemestre < 1,
            CampusQuest.Itens.Cola => Inventario?.BuscarPorTipo<CampusQuest.Itens.Cola>() == null,
            _ => true
        };
    }

    public void RegistrarItemRecebido(Item item)
    {
        if (item == null)
        {
            return;
        }

        if (item is CampusQuest.Itens.Cafe)
        {
            CafesRecebidosSemestre++;
            return;
        }

        if (item is CampusQuest.Itens.Caderno)
        {
            CadernosRecebidosSemestre++;
            return;
        }

        if (item is CampusQuest.Itens.LivroTecnico)
        {
            LivrosRecebidosSemestre++;
        }
    }

    public void DefinirContagemItensSemestre(int cafes, int cadernos, int livros)
    {
        CafesRecebidosSemestre = Math.Max(0, cafes);
        CadernosRecebidosSemestre = Math.Max(0, cadernos);
        LivrosRecebidosSemestre = Math.Max(0, livros);
    }

    private void ResetarLimitesSemestre()
    {
        CafesRecebidosSemestre = 0;
        CadernosRecebidosSemestre = 0;
        LivrosRecebidosSemestre = 0;
    }
}
