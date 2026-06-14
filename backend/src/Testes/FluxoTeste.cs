using CampusQuest.Core;
using CampusQuest.Exame;
using CampusQuest.Materias;
using CampusQuest.Persistencia;
using CampusQuest.Quiz;

namespace CampusQuest.Testes;

public static class FluxoTeste
{
    public static ResultadoExame Executar(int semestreQuiz, Materia chefe)
    {
        DatabaseInitializer.Inicializar();

        Aluno aluno = new Aluno();
        IRepositorioQuestoes repositorioQuestoes = new SqliteRepositorioQuestoes();
        SistemaQuiz quiz = new SistemaQuiz(repositorioQuestoes);
        ResultadoQuiz resultadoQuiz = quiz.Executar(aluno, semestreQuiz);

        if (resultadoQuiz.ItemDropado != null)
        {
            resultadoQuiz.ItemDropado.Usar(aluno);
        }

        SistemaExame exame = new SistemaExame(repositorioQuestoes: repositorioQuestoes);
        return exame.Executar(aluno, chefe);
    }
}