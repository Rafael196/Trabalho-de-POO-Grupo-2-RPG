using System;
using System.Collections.Generic;
using CampusQuest.Core;
using CampusQuest.Materias;
using CampusQuest.Quiz;

namespace CampusQuest.Exame;

public class SistemaExame
{
    private const int DanoBaseChefe = 20;
    private const int DanoBaseAluno = 15;

    public ResultadoExame Executar(Aluno aluno, Materia chefe)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        if (chefe == null)
        {
            throw new ArgumentNullException(nameof(chefe));
        }

        int conhecimentoNoInicio = aluno.Conhecimento;
        List<Pergunta> perguntas = chefe.GetPerguntasExame() ?? new List<Pergunta>();
        int acertos = 0;
        int erros = 0;
        ContextoExame contexto = new ContextoExame();

        AplicarAtaqueEspecialInicial(chefe, aluno, contexto);

        foreach (Pergunta pergunta in perguntas)
        {
            if (!aluno.EstaVivo() || chefe.EstaVencido())
            {
                break;
            }

            if (pergunta == null)
            {
                continue;
            }

            ApresentarPergunta(pergunta);
            int indiceResposta = LerIndiceResposta(pergunta.Alternativas?.Length ?? 0);

            if (indiceResposta == pergunta.IndiceCorreto)
            {
                int danoAoChefe = CalcularDanoAoChefe(aluno, contexto);
                chefe.ReceberDano(danoAoChefe);
                acertos++;
            }
            else
            {
                int danoAoAluno = CalcularDanoAoAluno(aluno, contexto, DanoBaseChefe);
                aluno.ReceberDano(danoAoAluno);
                erros++;
            }

            if (contexto.DanoContinuo && contexto.TurnosComDano > 0)
            {
                int danoContinuo = CalcularDanoAoAluno(aluno, contexto, DanoBaseChefe);
                aluno.ReceberDano(danoContinuo);
                contexto.TurnosComDano--;

                if (contexto.TurnosComDano <= 0)
                {
                    contexto.DanoContinuo = false;
                }
            }

            if (aluno.Habilidades != null)
            {
                foreach (Habilidade habilidade in aluno.Habilidades)
                {
                    if (habilidade != null)
                    {
                        habilidade.Aplicar(aluno, contexto, chefe);
                    }
                }
            }

            if (contexto.MultiplicadorAnulado)
            {
                contexto.MultiplicadorAnulado = false;
            }
        }

        bool vitoria = chefe.EstaVencido();
        int aproveitamentoFinal = 0;
        bool bonusCoragemAplicado = false;

        if (vitoria)
        {
            int totalPerguntas = perguntas.Count;
            float fatorConhecimento = 0.5f + (aluno.Conhecimento / 200.0f);
            float aproveitamento = totalPerguntas == 0
                ? 0f
                : (acertos / (float)totalPerguntas * 100f) * fatorConhecimento;

            if (conhecimentoNoInicio < 40)
            {
                aproveitamento = Math.Min(65f, aproveitamento + 10f);
                bonusCoragemAplicado = true;
            }

            int valor = (int)Math.Round(aproveitamento, MidpointRounding.AwayFromZero);
            valor = Math.Max(0, Math.Min(100, valor));
            aproveitamentoFinal = valor;
            aluno.RegistrarAproveitamento(aluno.SemestreAtual, valor);
        }

        return new ResultadoExame(vitoria, aproveitamentoFinal, bonusCoragemAplicado, acertos, erros);
    }

    private void AplicarAtaqueEspecialInicial(Materia chefe, Aluno aluno, ContextoExame contexto)
    {
        if (chefe is IC)
        {
            contexto.DanoContinuo = true;
            contexto.TurnosComDano = 2;
            return;
        }

        if (chefe is AED)
        {
            int dano = CalcularDanoAoAluno(aluno, contexto, DanoBaseChefe * 2.5);
            aluno.ReceberDano(dano);
            return;
        }

        if (chefe is POO)
        {
            contexto.MultiplicadorAnulado = true;
            return;
        }

        if (chefe is TCC)
        {
            contexto.DanoContinuo = true;
            contexto.TurnosComDano = 2;
            int dano = CalcularDanoAoAluno(aluno, contexto, DanoBaseChefe * 2.5);
            aluno.ReceberDano(dano);
            contexto.MultiplicadorAnulado = true;
        }
    }

    private int CalcularDanoAoChefe(Aluno aluno, ContextoExame contexto)
    {
        double conhecimentoEfetivo = contexto.MultiplicadorAnulado ? 0 : aluno.Conhecimento;
        double dano = DanoBaseAluno * (1 + conhecimentoEfetivo / 100.0);
        return (int)Math.Round(dano, MidpointRounding.AwayFromZero);
    }

    private int CalcularDanoAoAluno(Aluno aluno, ContextoExame contexto, double danoBase)
    {
        double conhecimentoEfetivo = contexto.MultiplicadorAnulado ? 0 : aluno.Conhecimento;
        double dano = Math.Max(1, danoBase * (1 - conhecimentoEfetivo / 100.0));
        return (int)Math.Round(dano, MidpointRounding.AwayFromZero);
    }

    private void ApresentarPergunta(Pergunta pergunta)
    {
        Console.WriteLine(pergunta.Enunciado);

        if (pergunta.Alternativas == null)
        {
            return;
        }

        for (int i = 0; i < pergunta.Alternativas.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {pergunta.Alternativas[i]}");
        }

        Console.WriteLine("Escolha uma alternativa:");
    }

    private int LerIndiceResposta(int totalAlternativas)
    {
        if (totalAlternativas <= 0)
        {
            return -1;
        }

        while (true)
        {
            string entrada = Console.ReadLine();
            if (int.TryParse(entrada, out int valor))
            {
                int indice = valor - 1;
                if (indice >= 0 && indice < totalAlternativas)
                {
                    return indice;
                }
            }

            Console.WriteLine("Entrada invalida.");
        }
    }
}

public class ResultadoExame
{
    public bool Vitoria { get; }
    public int Aproveitamento { get; }
    public bool BonusCoragemAplicado { get; }
    public int AcertosTotal { get; }
    public int ErrosTotal { get; }

    public ResultadoExame(bool vitoria, int aproveitamento, bool bonusCoragemAplicado, int acertosTotal, int errosTotal)
    {
        Vitoria = vitoria;
        Aproveitamento = aproveitamento;
        BonusCoragemAplicado = bonusCoragemAplicado;
        AcertosTotal = acertosTotal;
        ErrosTotal = errosTotal;
    }
}
