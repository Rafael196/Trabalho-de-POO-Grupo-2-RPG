using System;
using System.Collections.Generic;

namespace CampusQuest.Persistencia;

public class EstadoJogo
{
    public string Versao { get; set; } = "1.0";
    public DateTime DataSalvamento { get; set; }
    public DadosAluno Aluno { get; set; } = new();
    public Estatisticas Estatisticas { get; set; } = new();

    public string NomeAluno { get; set; } = string.Empty;
    public int VidaAtual { get; set; }
    public int Conhecimento { get; set; }
    public int SemestreAtual { get; set; }
}

public class DadosAluno
{
    public string Nome { get; set; } = string.Empty;
    public int VidaAtual { get; set; }
    public int VidaMaxima { get; set; }
    public int Conhecimento { get; set; }
    public int SemestreAtual { get; set; }
    public List<string> Habilidades { get; set; } = new();
    public int[] Aproveitamentos { get; set; } = Array.Empty<int>();
    public List<ItemSalvo> Inventario { get; set; } = new();
    public int CafesRecebidosSemestre { get; set; }
    public int CadernosRecebidosSemestre { get; set; }
    public int LivrosRecebidosSemestre { get; set; }
}

public class Estatisticas
{
    public int SemestresRepetidos { get; set; }
    public int QuizzesConcluidos { get; set; }
    public int ItensUsados { get; set; }
    public bool BonusCoragemAplicado { get; set; }
}

public class ItemSalvo
{
    public string Tipo { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string MateriaAlvo { get; set; } = string.Empty;
}
