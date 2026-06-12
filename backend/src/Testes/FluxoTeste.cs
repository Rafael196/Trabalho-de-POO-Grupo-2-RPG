using CampusQuest.Core;
using CampusQuest.Exame;
using CampusQuest.Materias;
using CampusQuest.Quiz;

namespace CampusQuest.Testes;

public static class FluxoTeste
{
    public static ResultadoExame Executar(int semestreQuiz, Materia chefe)
    {
        Aluno aluno = new Aluno();
        SistemaQuiz quiz = new SistemaQuiz();
        ResultadoQuiz resultadoQuiz = quiz.Executar(aluno, semestreQuiz);

        if (resultadoQuiz.ItemDropado != null)
        {
            resultadoQuiz.ItemDropado.Usar(aluno);
        }

        SistemaExame exame = new SistemaExame();
        return exame.Executar(aluno, chefe);
    }
}