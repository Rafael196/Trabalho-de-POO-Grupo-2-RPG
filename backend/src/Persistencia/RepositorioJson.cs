using System;
using System.IO;
using System.Text.Json;

namespace CampusQuest.Persistencia;

public class RepositorioJson : IRepositorio
{
    private readonly string caminhoArquivo;
    private readonly JsonSerializerOptions jsonOptions;

    public RepositorioJson(string caminhoArquivo)
    {
        this.caminhoArquivo = string.IsNullOrWhiteSpace(caminhoArquivo)
            ? "saves/slot1.json"
            : caminhoArquivo;
        jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };
    }

    public void Salvar(EstadoJogo estado)
    {
        if (estado == null)
        {
            throw new ArgumentNullException(nameof(estado));
        }

        string caminhoCompleto = ObterCaminhoCompleto();
        string diretorio = Path.GetDirectoryName(caminhoCompleto) ?? string.Empty;
        if (!string.IsNullOrWhiteSpace(diretorio))
        {
            Directory.CreateDirectory(diretorio);
        }

        string json = JsonSerializer.Serialize(estado, jsonOptions);
        File.WriteAllText(caminhoCompleto, json);
    }

    public EstadoJogo Carregar()
    {
        string caminhoCompleto = ObterCaminhoCompleto();
        if (!File.Exists(caminhoCompleto))
        {
            throw new InvalidOperationException("Nenhum save encontrado.");
        }

        string json = File.ReadAllText(caminhoCompleto);
        EstadoJogo estado = JsonSerializer.Deserialize<EstadoJogo>(json, jsonOptions);
        if (estado == null)
        {
            throw new InvalidOperationException("Save invalido.");
        }

        return estado;
    }

    public bool ExisteArquivo()
    {
        return File.Exists(ObterCaminhoCompleto());
    }

    public void Deletar()
    {
        string caminhoCompleto = ObterCaminhoCompleto();
        if (File.Exists(caminhoCompleto))
        {
            File.Delete(caminhoCompleto);
        }
    }

    private string ObterCaminhoCompleto()
    {
        return Path.GetFullPath(caminhoArquivo);
    }
}
