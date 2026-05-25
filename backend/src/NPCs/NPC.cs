using CampusQuest.Core;

namespace CampusQuest.NPCs;

public abstract class NPC
{
    public string Nome { get; protected set; }

    public abstract void Interagir(Aluno aluno);
    public abstract string GetDialogo();

    public virtual string GetMensagemAbertura(Aluno aluno)
    {
        string nome = string.IsNullOrWhiteSpace(aluno?.Nome) ? "Jogador" : aluno.Nome;
        return $"Ola, {nome}.";
    }

    public virtual string ObterDica(Aluno aluno)
    {
        return "Procure o veterano no Hall para dicas e itens uteis.";
    }

    public virtual Core.Item SolicitarItem(Aluno aluno)
    {
        return null;
    }

    public virtual string MensagemItemIndisponivel(Aluno aluno)
    {
        return "Nao tenho itens para entregar agora.";
    }
}