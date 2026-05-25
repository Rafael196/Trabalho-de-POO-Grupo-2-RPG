namespace CampusQuest.Exame;

public static class CalculadoraMedia
{
    public static float Calcular(int[] aproveitamentos)
    {
        if (aproveitamentos == null || aproveitamentos.Length == 0)
        {
            return 0f;
        }

        int soma = 0;
        for (int i = 0; i < aproveitamentos.Length; i++)
        {
            soma += aproveitamentos[i];
        }

        return soma / (float)aproveitamentos.Length;
    }

    public static FaixaMedia ClassificarFaixa(float media)
    {
        if (media >= 80f)
        {
            return FaixaMedia.Alta;
        }

        if (media >= 50f)
        {
            return FaixaMedia.Media;
        }

        return FaixaMedia.Baixa;
    }
}

public enum FaixaMedia
{
    Alta,
    Media,
    Baixa
}
