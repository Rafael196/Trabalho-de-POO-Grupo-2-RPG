using System.Collections.Generic;

namespace CampusQuest.Persistencia;

public interface IRepositorioQuestoes
{
    IEnumerable<QuestaoDto> ObterPorSemestre(int semestre);
    IEnumerable<QuestaoDto> ObterQuizPorSemestre(int semestre);
    IEnumerable<QuestaoDto> ObterExamePorMateria(string materia);
    IEnumerable<QuestaoDto> ObterTodas();
    QuestaoDto ObterPorId(int id);
    void Inserir(QuestaoDto questao);
    void Atualizar(QuestaoDto questao);
    void Deletar(int id);
}
