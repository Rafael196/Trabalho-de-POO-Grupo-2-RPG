using System;
using CampusQuest.Core;
using CampusQuest.Persistencia;

namespace CampusQuest.NPCs;

public class Coordenador : NPC
{
    private readonly IRepositorio repositorio;
    private string dialogoAtual = "Coordenador pronto para trancar o semestre.";

    public Coordenador()
    {
        Nome = "Coordenador";
    }

    public Coordenador(IRepositorio repositorio)
        : this()
    {
        this.repositorio = repositorio;
    }

    public override void Interagir(Aluno aluno)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        dialogoAtual = "Informe o repositório para salvar o semestre.";
    }

    public bool TrancarSemestre(Aluno aluno, IRepositorio repo)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno));
        }

        IRepositorio repositorioEfetivo = repo ?? repositorio;
        if (repositorioEfetivo == null)
        {
            dialogoAtual = "Nenhum repositório disponível para salvar.";
            return false;
        }

        try
        {
            EstadoJogo estado = new EstadoJogo
            {
                NomeAluno = string.Empty,
                VidaAtual = aluno.Vida,
                Conhecimento = aluno.Conhecimento,
                SemestreAtual = aluno.SemestreAtual
            };

            repositorioEfetivo.Salvar(estado);
            dialogoAtual = "Semestre trancado com sucesso.";
            return true;
        }
        catch
        {
            dialogoAtual = "Falha ao salvar o semestre.";
            return false;
        }
    }

    public override string GetDialogo()
    {
        return dialogoAtual;
    }
}