using GameItem = CampusQuest.Itens.Item;

namespace CampusQuest.Quiz;

public class ResultadoQuiz
{
    public int Aproveitamento { get; }
    public int GanhoConhecimento { get; }
    public GameItem ItemDropado { get; }

    public ResultadoQuiz(int aproveitamento, int ganhoConhecimento, GameItem itemDropado)
    {
        Aproveitamento = aproveitamento;
        GanhoConhecimento = ganhoConhecimento;
        ItemDropado = itemDropado;
    }
}
