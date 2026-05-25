namespace CampusQuest.Estados;

public interface IEstadoJogo
{
    void Entrar(JogoContexto contexto);
    void Executar(JogoContexto contexto);
    void Sair(JogoContexto contexto);
}
