using System;
using System.Collections.Generic;

namespace CampusQuest.Persistencia;

public class EstadoJogo
{
    public string Versao { get; set; }
    public DateTime DataSalvamento { get; set; }
    public DadosAluno Aluno { get; set; }
    public Estatisticas Estatisticas { get; set; }

    public string NomeAluno { get; set; }
    public int VidaAtual { get; set; }
    public int Conhecimento { get; set; }
    public int SemestreAtual { get; set; }
}

public class DadosAluno
{
    public string Nome { get; set; }
    public int VidaAtual { get; set; }
    public int VidaMaxima { get; set; }
    public int Conhecimento { get; set; }
    public int SemestreAtual { get; set; }
    public List<string> Habilidades { get; set; }
    public int[] Aproveitamentos { get; set; }
    public List<ItemSalvo> Inventario { get; set; }
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
    public string Tipo { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string MateriaAlvo { get; set; }
}