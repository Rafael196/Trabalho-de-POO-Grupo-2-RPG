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

    public override string GetMensagemAbertura(Aluno aluno)
    {
        string nome = string.IsNullOrWhiteSpace(aluno?.Nome) ? "aluno" : aluno.Nome;
        return $"Coordenador: Boa tarde, {nome}. Em que posso ajudar?";
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
            EstadoJogo estado = EstadoJogoFactory.Criar(aluno);
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

    public override string ObterDica(Aluno aluno)
    {
        return "Coordenador: Se precisar de ajuda, faca um quiz com o professor e pegue itens com o veterano.";
    }

    public override Core.Item SolicitarItem(Aluno aluno)
    {
        return null;
    }

    public override string MensagemItemIndisponivel(Aluno aluno)
    {
        return "Coordenador: Itens sao com o veterano no Hall.";
    }
}