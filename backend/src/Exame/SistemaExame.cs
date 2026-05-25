using System;
using System.Collections.Generic;
using CampusQuest.Core;
using CampusQuest.Itens;
using CampusQuest.Materias;
using CampusQuest.Quiz;
using CampusQuest.UI;

namespace CampusQuest.Exame;

public class SistemaExame
{
    private const int DanoBaseChefe = 20;
    private const int DanoBaseAluno = 15;
    private readonly IConsoleIO io;

    public SistemaExame(IConsoleIO? io = null)
    {
        this.io = io ?? new ConsoleIO();
    }

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
        int bonusCaderno = Caderno.ConsumirBonus(aluno);
        int bonusLivro = LivroTecnico.ObterBonus(aluno, chefe.Nome ?? string.Empty);
        int conhecimentoEfetivo = Math.Max(0, conhecimentoNoInicio + bonusCaderno + bonusLivro);
        List<Pergunta> perguntas = chefe.GetPerguntasExame() ?? new List<Pergunta>();
        int acertos = 0;
        int erros = 0;
        ContextoExame contexto = new ContextoExame();

        io.WriteLine("=== Exame de Aproveitamento ===");
        io.WriteLine($"Chefe: {chefe.Nome}");
        io.WriteLine($"Conhecimento base: {conhecimentoNoInicio}");
        MostrarBonusItens(bonusCaderno, bonusLivro, conhecimentoEfetivo);

        AplicarAtaqueEspecialInicial(chefe, aluno, contexto, conhecimentoEfetivo);

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
                int danoAoChefe = CalcularDanoAoChefe(conhecimentoEfetivo, contexto);
                chefe.ReceberDano(danoAoChefe);
                acertos++;
                io.WriteLine($"Acerto! Dano ao chefe: {danoAoChefe} (Vida do chefe: {chefe.GetVidaAtual()}/{chefe.GetVidaMaxima()})");
            }
            else
            {
                int danoAoAluno = CalcularDanoAoAluno(conhecimentoEfetivo, contexto, DanoBaseChefe);
                aluno.ReceberDano(danoAoAluno);
                erros++;
                io.WriteLine($"Erro. Dano ao aluno: {danoAoAluno} (Vida do aluno: {aluno.Vida}/{aluno.VidaMaxima})");
            }

            if (contexto.DanoContinuo && contexto.TurnosComDano > 0)
            {
                int danoContinuo = CalcularDanoAoAluno(conhecimentoEfetivo, contexto, DanoBaseChefe);
                aluno.ReceberDano(danoContinuo);
                contexto.TurnosComDano--;

                io.WriteLine($"Dano continuo aplicado: {danoContinuo} (Vida do aluno: {aluno.Vida}/{aluno.VidaMaxima})");

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
                        io.WriteLine($"Habilidade ativada: {habilidade.Nome} - {habilidade.Descricao}");
                        habilidade.Aplicar(aluno, contexto);
                    }
                }
            }

            if (contexto.MultiplicadorAnulado)
            {
                contexto.MultiplicadorAnulado = false;
            }

            io.WriteLine("Pressione Enter para continuar...");
            io.ReadLine();
        }

        bool vitoria = chefe.EstaVencido();
        int aproveitamentoFinal = 0;
        bool bonusCoragemAplicado = false;

        if (vitoria)
        {
            int totalPerguntas = perguntas.Count;
            float fatorConhecimento = 0.5f + (conhecimentoEfetivo / 200.0f);
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

        MostrarResumoFinal(vitoria, acertos, erros, aproveitamentoFinal, bonusCoragemAplicado, aluno, chefe);

        return new ResultadoExame(vitoria, aproveitamentoFinal, bonusCoragemAplicado, acertos, erros);
    }

    private void AplicarAtaqueEspecialInicial(Materia chefe, Aluno aluno, ContextoExame contexto, int conhecimentoEfetivo)
    {
        if (chefe is IC)
        {
            contexto.DanoContinuo = true;
            contexto.TurnosComDano = 2;
            io.WriteLine("Ataque especial: Loop Infinito (dano continuo por 2 turnos).");
            return;
        }

        if (chefe is AED)
        {
            int dano = CalcularDanoAoAluno(conhecimentoEfetivo, contexto, DanoBaseChefe * 2.5);
            aluno.ReceberDano(dano);
            io.WriteLine($"Ataque especial: Stack Overflow (dano imediato {dano}).");
            return;
        }

        if (chefe is POO)
        {
            contexto.MultiplicadorAnulado = true;
            io.WriteLine("Ataque especial: NullPointerException (multiplicador anulado neste turno).");
            return;
        }

        if (chefe is TCC)
        {
            contexto.DanoContinuo = true;
            contexto.TurnosComDano = 2;
            int dano = CalcularDanoAoAluno(conhecimentoEfetivo, contexto, DanoBaseChefe * 2.5);
            aluno.ReceberDano(dano);
            contexto.MultiplicadorAnulado = true;
            io.WriteLine("Ataque especial: Sintese Total (combo de efeitos)." );
            io.WriteLine($"Dano imediato aplicado: {dano}.");
        }
    }

    private int CalcularDanoAoChefe(int conhecimentoEfetivo, ContextoExame contexto)
    {
        double conhecimentoBase = contexto.MultiplicadorAnulado ? 0 : conhecimentoEfetivo;
        double dano = DanoBaseAluno * (1 + conhecimentoBase / 100.0);
        return (int)Math.Round(dano, MidpointRounding.AwayFromZero);
    }

    private int CalcularDanoAoAluno(int conhecimentoEfetivo, ContextoExame contexto, double danoBase)
    {
        double conhecimentoBase = contexto.MultiplicadorAnulado ? 0 : conhecimentoEfetivo;
        double dano = Math.Max(1, danoBase * (1 - conhecimentoBase / 100.0));
        return (int)Math.Round(dano, MidpointRounding.AwayFromZero);
    }

    private void ApresentarPergunta(Pergunta pergunta)
    {
        io.WriteLine(pergunta.Enunciado);

        if (pergunta.Alternativas == null)
        {
            return;
        }

        for (int i = 0; i < pergunta.Alternativas.Length; i++)
        {
            io.WriteLine($"{i + 1}. {pergunta.Alternativas[i]}");
        }

        io.WriteLine("Escolha uma alternativa:");
    }

    private int LerIndiceResposta(int totalAlternativas)
    {
        if (totalAlternativas <= 0)
        {
            return -1;
        }

        while (true)
        {
            string entrada = io.ReadLine();
            if (int.TryParse(entrada, out int valor))
            {
                int indice = valor - 1;
                if (indice >= 0 && indice < totalAlternativas)
                {
                    return indice;
                }
            }

            io.WriteLine("Entrada invalida.");
        }
    }

    private void MostrarBonusItens(int bonusCaderno, int bonusLivro, int conhecimentoEfetivo)
    {
        if (bonusCaderno > 0)
        {
            io.WriteLine($"Bonus do Caderno: +{bonusCaderno} Conhecimento (temporario)." );
        }

        if (bonusLivro > 0)
        {
            io.WriteLine($"Bonus do Livro Tecnico: +{bonusLivro} Conhecimento (materia)." );
        }

        if (bonusCaderno > 0 || bonusLivro > 0)
        {
            io.WriteLine($"Conhecimento efetivo no exame: {conhecimentoEfetivo}." );
        }
    }

    private void MostrarResumoFinal(
        bool vitoria,
        int acertos,
        int erros,
        int aproveitamentoFinal,
        bool bonusCoragemAplicado,
        Aluno aluno,
        Materia chefe)
    {
        io.WriteLine("=== Resumo do Exame ===");
        io.WriteLine(vitoria ? "Resultado: Vitoria" : "Resultado: Derrota");
        io.WriteLine($"Chefe: {chefe.Nome}");
        io.WriteLine($"Acertos: {acertos}");
        io.WriteLine($"Erros: {erros}");
        io.WriteLine($"Vida final do aluno: {aluno.Vida}/{aluno.VidaMaxima}");
        io.WriteLine($"Vida final do chefe: {chefe.GetVidaAtual()}/{chefe.GetVidaMaxima()}");

        if (vitoria)
        {
            io.WriteLine($"Aproveitamento final: {aproveitamentoFinal}");
            if (bonusCoragemAplicado)
            {
                io.WriteLine("Bonus de coragem aplicado.");
            }
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
