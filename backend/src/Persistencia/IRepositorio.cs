namespace CampusQuest.Persistencia;

public interface IRepositorio
{
    void Salvar(EstadoJogo estado);
    EstadoJogo Carregar();
    bool ExisteArquivo();
    void Deletar();
}