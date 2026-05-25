using System;
using System.Collections.Generic;

namespace CampusQuest.Core;

public static class HabilidadeCatalogo
{
    private static readonly Dictionary<string, Habilidade> Catalogo = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Foco IC"] = new Habilidade(
            "Foco IC",
            "Neutraliza dano continuo ao errar.",
            TipoEfeito.ReducaoDano),
        ["Foco AED"] = new Habilidade(
            "Foco AED",
            "Restaura o multiplicador de conhecimento quando anulado.",
            TipoEfeito.AumentaDano),
        ["Foco POO"] = new Habilidade(
            "Foco POO",
            "Ignora anulacao do multiplicador por um turno.",
            TipoEfeito.AumentaDano)
    };

    public static Habilidade ObterPorNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return null;
        }

        return Catalogo.TryGetValue(nome.Trim(), out Habilidade habilidade)
            ? new Habilidade(habilidade.Nome, habilidade.Descricao, habilidade.Efeito)
            : null;
    }

    public static Habilidade ObterPorChefe(string chefeNome)
    {
        if (string.IsNullOrWhiteSpace(chefeNome))
        {
            return null;
        }

        string chave = chefeNome.Trim();
        return chave switch
        {
            "IC" => ObterPorNome("Foco IC"),
            "AED" => ObterPorNome("Foco AED"),
            "POO" => ObterPorNome("Foco POO"),
            _ => null
        };
    }
}
