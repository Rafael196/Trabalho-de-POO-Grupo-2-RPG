using System;

namespace CampusQuest.Quiz;

public class Pergunta
{
    public string Enunciado { get; }
    public string[] Alternativas { get; }
    public int IndiceCorreto { get; }
    public string Explicacao { get; }

    public Pergunta(string enunciado, string[] alternativas, int indiceCorreto, string explicacao)
    {
        Enunciado = enunciado ?? string.Empty;
        Alternativas = alternativas ?? Array.Empty<string>();
        IndiceCorreto = indiceCorreto;
        Explicacao = explicacao ?? string.Empty;
    }

    public bool VerificarResposta(int indiceEscolhido)
    {
        return indiceEscolhido == IndiceCorreto;
    }
}