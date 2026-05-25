using System;

namespace CampusQuest.Persistencia;

public class StubRepositorio : IRepositorio
{
    private EstadoJogo estadoSalvo;

    public void Salvar(EstadoJogo estado) => estadoSalvo = estado;

    public EstadoJogo Carregar() => estadoSalvo
        ?? throw new InvalidOperationException("Nenhum save encontrado.");

    public bool ExisteArquivo() => estadoSalvo != null;

    public void Deletar() => estadoSalvo = null;
}