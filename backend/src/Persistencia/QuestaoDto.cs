using System;

namespace CampusQuest.Persistencia;

public class QuestaoDto
{
    public int Id { get; set; }
    public string Enunciado { get; set; } = string.Empty;
    public string[] Alternativas { get; set; } = Array.Empty<string>();
    public int IndiceCorreto { get; set; }
    public string Explicacao { get; set; } = string.Empty;
    public int Semestre { get; set; }
    public string Materia { get; set; } = string.Empty;
    public string Tipo { get; set; } = "Quiz";
    public string Dificuldade { get; set; } = "Normal";
    public bool Ativa { get; set; } = true;
}
