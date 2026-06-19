using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;

namespace CampusQuest.Persistencia;

public static class DatabaseInitializer
{
    private static readonly string CaminhoDb = "database/campusquest.db";

    public static void Inicializar()
    {
        EnsureDatabaseDirectory();
        CriarSchemaSeNaoExistir();
        SeedQuestoesSeVazio();
    }

    private static void EnsureDatabaseDirectory()
    {
        string diretorio = Path.GetDirectoryName(CaminhoDb);
        if (!string.IsNullOrWhiteSpace(diretorio) && !Directory.Exists(diretorio))
        {
            Directory.CreateDirectory(diretorio);
        }
    }

    private static void CriarSchemaSeNaoExistir()
    {
        string conexaoString = $"Data Source={CaminhoDb}";

        using (SqliteConnection conn = new(conexaoString))
        {
            conn.Open();
            string sql = @"
                CREATE TABLE IF NOT EXISTS Questoes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Enunciado TEXT NOT NULL,
                    Alternativa1 TEXT NOT NULL,
                    Alternativa2 TEXT NOT NULL,
                    Alternativa3 TEXT NOT NULL,
                    Alternativa4 TEXT NOT NULL,
                    IndiceCorreto INTEGER NOT NULL,
                    Explicacao TEXT NOT NULL,
                    Semestre INTEGER NOT NULL,
                    Materia TEXT NOT NULL,
                    Tipo TEXT NOT NULL DEFAULT 'Quiz',
                    Dificuldade TEXT NOT NULL DEFAULT 'Normal',
                    Ativa INTEGER NOT NULL DEFAULT 1,
                    DataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP
                );
            ";

            using (SqliteCommand cmd = new(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }

            GarantirColuna(conn, "Tipo", "TEXT NOT NULL DEFAULT 'Quiz'");
            GarantirColuna(conn, "Dificuldade", "TEXT NOT NULL DEFAULT 'Normal'");
            GarantirColuna(conn, "Ativa", "INTEGER NOT NULL DEFAULT 1");
        }
    }

    private static void GarantirColuna(SqliteConnection conn, string nomeColuna, string definicao)
    {
        string checkSql = "PRAGMA table_info(Questoes)";

        using (SqliteCommand checkCmd = new(checkSql, conn))
        {
            using (SqliteDataReader reader = checkCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (string.Equals(reader.GetString(1), nomeColuna, StringComparison.OrdinalIgnoreCase))
                    {
                        return;
                    }
                }
            }
        }

        string alterSql = $"ALTER TABLE Questoes ADD COLUMN {nomeColuna} {definicao}";
        using (SqliteCommand alterCmd = new(alterSql, conn))
        {
            alterCmd.ExecuteNonQuery();
        }
    }

    private static void SeedQuestoesSeVazio()
    {
        var questoes = ObterQuestoesPadrao();
        var repositorio = new SqliteRepositorioQuestoes(CaminhoDb);

        foreach (var questao in questoes)
        {
            if (!ExisteQuestao(questao))
            {
                repositorio.Inserir(questao);
            }
        }
    }

    private static bool ExisteQuestao(QuestaoDto questao)
    {
        string conexaoString = $"Data Source={CaminhoDb}";

        using (SqliteConnection conn = new(conexaoString))
        {
            conn.Open();
            string sql = "SELECT COUNT(*) FROM Questoes WHERE Enunciado = @enunciado AND Materia = @materia AND Tipo = @tipo";
            using (SqliteCommand cmd = new(sql, conn))
            {
                cmd.Parameters.AddWithValue("@enunciado", questao.Enunciado ?? string.Empty);
                cmd.Parameters.AddWithValue("@materia", questao.Materia ?? string.Empty);
                cmd.Parameters.AddWithValue("@tipo", string.IsNullOrWhiteSpace(questao.Tipo) ? "Quiz" : questao.Tipo);
                return Convert.ToInt32(cmd.ExecuteScalar() ?? 0) > 0;
            }
        }
    }

    private static List<QuestaoDto> ObterQuestoesPadrao()
    {
        List<QuestaoDto> questoes = new()
        {
            // Semestre 1 - IC
            new QuestaoDto
            {
                Enunciado = "O que é hardware?",
                Alternativas = new[] { "Parte física", "Programa", "Rede", "Arquivo" },
                IndiceCorreto = 0,
                Explicacao = "Hardware é a parte física do computador.",
                Semestre = 1,
                Materia = "IC"
            },
            new QuestaoDto
            {
                Enunciado = "O que é software?",
                Alternativas = new[] { "Programa", "Placa", "Teclado", "Memória" },
                IndiceCorreto = 0,
                Explicacao = "Software é o conjunto de programas e aplicações.",
                Semestre = 1,
                Materia = "IC"
            },
            new QuestaoDto
            {
                Enunciado = "Qual sistema gerencia recursos do computador?",
                Alternativas = new[] { "Sistema operacional", "Editor", "Navegador", "Jogos" },
                IndiceCorreto = 0,
                Explicacao = "O sistema operacional gerencia todos os recursos de hardware.",
                Semestre = 1,
                Materia = "IC"
            },
            new QuestaoDto
            {
                Enunciado = "Qual número representa binário?",
                Alternativas = new[] { "0 e 1", "2 e 3", "5 e 6", "8 e 9" },
                IndiceCorreto = 0,
                Explicacao = "Binário usa apenas os dígitos 0 e 1.",
                Semestre = 1,
                Materia = "IC"
            },
            new QuestaoDto
            {
                Enunciado = "Qual dispositivo conecta redes?",
                Alternativas = new[] { "Roteador", "Mouse", "Monitor", "Teclado" },
                IndiceCorreto = 0,
                Explicacao = "Roteador conecta diferentes redes e permite comunicação entre elas.",
                Semestre = 1,
                Materia = "IC"
            },

            // Semestre 2 - AED
            new QuestaoDto
            {
                Enunciado = "Pilha segue qual regra?",
                Alternativas = new[] { "LIFO", "FIFO", "ABC", "XYZ" },
                IndiceCorreto = 0,
                Explicacao = "Pilha é LIFO (Last In, First Out).",
                Semestre = 2,
                Materia = "AED"
            },
            new QuestaoDto
            {
                Enunciado = "Fila segue qual regra?",
                Alternativas = new[] { "FIFO", "LIFO", "DFS", "BFS" },
                IndiceCorreto = 0,
                Explicacao = "Fila é FIFO (First In, First Out).",
                Semestre = 2,
                Materia = "AED"
            },
            new QuestaoDto
            {
                Enunciado = "Busca binária exige dados:",
                Alternativas = new[] { "Ordenados", "Aleatórios", "Vazios", "Repetidos" },
                IndiceCorreto = 0,
                Explicacao = "Busca binária exige que os dados estejam ordenados.",
                Semestre = 2,
                Materia = "AED"
            },
            new QuestaoDto
            {
                Enunciado = "Complexidade O(n) indica crescimento:",
                Alternativas = new[] { "Linear", "Constante", "Quadrático", "Logarítmico" },
                IndiceCorreto = 0,
                Explicacao = "O(n) indica crescimento linear com o tamanho da entrada.",
                Semestre = 2,
                Materia = "AED"
            },
            new QuestaoDto
            {
                Enunciado = "Qual estrutura combina recursão?",
                Alternativas = new[] { "Pilha", "Fila", "Lista", "Matriz" },
                IndiceCorreto = 0,
                Explicacao = "Recursão usa a pilha de execução (call stack) internamente.",
                Semestre = 2,
                Materia = "AED"
            },

            // Semestre 3 - POO
            new QuestaoDto
            {
                Enunciado = "Encapsulamento controla:",
                Alternativas = new[] { "Acesso aos dados", "Rede", "Compilação", "Memória" },
                IndiceCorreto = 0,
                Explicacao = "Encapsulamento controla o acesso aos dados de um objeto.",
                Semestre = 3,
                Materia = "POO"
            },
            new QuestaoDto
            {
                Enunciado = "Herança permite:",
                Alternativas = new[] { "Reuso", "Apagar dados", "Acelerar CPU", "Criar rede" },
                IndiceCorreto = 0,
                Explicacao = "Herança promove reutilização de código entre classes.",
                Semestre = 3,
                Materia = "POO"
            },
            new QuestaoDto
            {
                Enunciado = "Polimorfismo significa:",
                Alternativas = new[] { "Mesmo método com comportamentos diferentes", "Só um construtor", "Sem classes", "Sem objetos" },
                IndiceCorreto = 0,
                Explicacao = "Polimorfismo permite que o mesmo método tenha comportamentos diferentes.",
                Semestre = 3,
                Materia = "POO"
            },
            new QuestaoDto
            {
                Enunciado = "Abstração é:",
                Alternativas = new[] { "Focar no essencial", "Duplicar código", "Evitar classes", "Criar bugs" },
                IndiceCorreto = 0,
                Explicacao = "Abstração foca no essencial, escondendo detalhes complexos.",
                Semestre = 3,
                Materia = "POO"
            },
            new QuestaoDto
            {
                Enunciado = "Objeto é:",
                Alternativas = new[] { "Instância de classe", "Método", "Interface", "Namespace" },
                IndiceCorreto = 0,
                Explicacao = "Objeto é uma instância de uma classe.",
                Semestre = 3,
                Materia = "POO"
            }
        };

        questoes.AddRange(ObterQuestoesExamePadrao());
        return questoes;
    }

    private static List<QuestaoDto> ObterQuestoesExamePadrao()
    {
        return new List<QuestaoDto>
        {
            new QuestaoDto { Enunciado = "Qual componente armazena dados temporariamente e se perde ao desligar o computador?", Alternativas = new[] { "RAM", "SSD", "HD", "ROM" }, IndiceCorreto = 0, Explicacao = "A memoria RAM e volatil e perde dados ao desligar.", Semestre = 1, Materia = "IC", Tipo = "Exame", Dificuldade = "Media" },
            new QuestaoDto { Enunciado = "O que significa a sigla CPU?", Alternativas = new[] { "Central Processing Unit", "Control Program Unit", "Central Peripheral Unit", "Compute Processing Utility" }, IndiceCorreto = 0, Explicacao = "CPU significa Central Processing Unit.", Semestre = 1, Materia = "IC", Tipo = "Exame", Dificuldade = "Media" },
            new QuestaoDto { Enunciado = "Qual base numerica usa apenas os digitos 0 e 1?", Alternativas = new[] { "Binaria", "Decimal", "Octal", "Hexadecimal" }, IndiceCorreto = 0, Explicacao = "A base binaria usa apenas 0 e 1.", Semestre = 1, Materia = "IC", Tipo = "Exame", Dificuldade = "Media" },
            new QuestaoDto { Enunciado = "Qual software gerencia hardware, processos e memoria?", Alternativas = new[] { "Sistema operacional", "Editor de texto", "Compilador", "Navegador" }, IndiceCorreto = 0, Explicacao = "O sistema operacional gerencia recursos do computador.", Semestre = 1, Materia = "IC", Tipo = "Exame", Dificuldade = "Media" },
            new QuestaoDto { Enunciado = "Qual dispositivo encaminha pacotes entre redes diferentes?", Alternativas = new[] { "Roteador", "Switch", "Hub", "Repetidor" }, IndiceCorreto = 0, Explicacao = "O roteador conecta redes distintas.", Semestre = 1, Materia = "IC", Tipo = "Exame", Dificuldade = "Media" },

            new QuestaoDto { Enunciado = "A complexidade O(n) indica tempo proporcional a:", Alternativas = new[] { "Tamanho da entrada", "Quadrado da entrada", "Logaritmo da entrada", "Constante" }, IndiceCorreto = 0, Explicacao = "O(n) cresce linearmente com o tamanho da entrada.", Semestre = 2, Materia = "AED", Tipo = "Exame", Dificuldade = "Media" },
            new QuestaoDto { Enunciado = "Qual estrutura segue o principio LIFO?", Alternativas = new[] { "Pilha", "Fila", "Lista", "Arvore" }, IndiceCorreto = 0, Explicacao = "Pilha e LIFO: ultimo a entrar, primeiro a sair.", Semestre = 2, Materia = "AED", Tipo = "Exame", Dificuldade = "Media" },
            new QuestaoDto { Enunciado = "Qual estrutura segue o principio FIFO?", Alternativas = new[] { "Fila", "Pilha", "Arvore", "Hash" }, IndiceCorreto = 0, Explicacao = "Fila e FIFO: primeiro a entrar, primeiro a sair.", Semestre = 2, Materia = "AED", Tipo = "Exame", Dificuldade = "Media" },
            new QuestaoDto { Enunciado = "Para usar busca binaria, o vetor precisa estar:", Alternativas = new[] { "Ordenado", "Aleatorio", "Reverso", "Circular" }, IndiceCorreto = 0, Explicacao = "Busca binaria exige dados ordenados.", Semestre = 2, Materia = "AED", Tipo = "Exame", Dificuldade = "Media" },
            new QuestaoDto { Enunciado = "Qual algoritmo troca elementos adjacentes repetidamente?", Alternativas = new[] { "Bubble sort", "Merge sort", "Quick sort", "Selection sort" }, IndiceCorreto = 0, Explicacao = "Bubble sort compara e troca vizinhos.", Semestre = 2, Materia = "AED", Tipo = "Exame", Dificuldade = "Media" },

            new QuestaoDto { Enunciado = "Qual pilar oculta detalhes internos e expoe apenas o necessario?", Alternativas = new[] { "Encapsulamento", "Heranca", "Polimorfismo", "Abstracao" }, IndiceCorreto = 0, Explicacao = "Encapsulamento controla acesso a dados internos.", Semestre = 3, Materia = "POO", Tipo = "Exame", Dificuldade = "Media" },
            new QuestaoDto { Enunciado = "Heranca permite:", Alternativas = new[] { "Reutilizar codigo de uma classe base", "Executar codigo em paralelo", "Acessar banco de dados", "Criar interfaces" }, IndiceCorreto = 0, Explicacao = "Heranca reaproveita comportamento da classe base.", Semestre = 3, Materia = "POO", Tipo = "Exame", Dificuldade = "Media" },
            new QuestaoDto { Enunciado = "Polimorfismo permite:", Alternativas = new[] { "Mesmo metodo com comportamentos diferentes", "Somente heranca simples", "Tipos primitivos", "Variaveis globais" }, IndiceCorreto = 0, Explicacao = "Polimorfismo varia comportamento por tipo.", Semestre = 3, Materia = "POO", Tipo = "Exame", Dificuldade = "Media" },
            new QuestaoDto { Enunciado = "Abstracao significa:", Alternativas = new[] { "Modelar apenas aspectos essenciais", "Esconder dados com private", "Repetir codigo", "Evitar heranca" }, IndiceCorreto = 0, Explicacao = "Abstracao foca no essencial do modelo.", Semestre = 3, Materia = "POO", Tipo = "Exame", Dificuldade = "Media" },
            new QuestaoDto { Enunciado = "Construtor em C# serve para:", Alternativas = new[] { "Inicializar o objeto", "Destruir o objeto", "Compilar o projeto", "Executar testes" }, IndiceCorreto = 0, Explicacao = "Construtor inicializa o estado do objeto.", Semestre = 3, Materia = "POO", Tipo = "Exame", Dificuldade = "Media" },

            new QuestaoDto { Enunciado = "Qual formula calcula a media final dos 3 semestres?", Alternativas = new[] { "(a1 + a2 + a3) / 3", "(a1 + a2) / 2", "a1 * a2 * a3", "a1 - a2 - a3" }, IndiceCorreto = 0, Explicacao = "A media final e a soma dividida por 3.", Semestre = 4, Materia = "TCC", Tipo = "Exame", Dificuldade = "Dificil" },
            new QuestaoDto { Enunciado = "No exame, dano ao chefe e calculado por:", Alternativas = new[] { "DanoBase * (1 + Conhecimento / 100)", "DanoBase * (1 - Conhecimento / 100)", "DanoBase + Conhecimento", "Conhecimento * 100" }, IndiceCorreto = 0, Explicacao = "O dano ao chefe cresce com o conhecimento.", Semestre = 4, Materia = "TCC", Tipo = "Exame", Dificuldade = "Dificil" },
            new QuestaoDto { Enunciado = "No exame, o dano ao aluno tem minimo:", Alternativas = new[] { "1", "0", "10", "Conhecimento" }, IndiceCorreto = 0, Explicacao = "O dano ao aluno nao pode ser menor que 1.", Semestre = 4, Materia = "TCC", Tipo = "Exame", Dificuldade = "Dificil" },
            new QuestaoDto { Enunciado = "Qual ataque especial anula o multiplicador de conhecimento?", Alternativas = new[] { "NullPointerException", "Loop Infinito", "Stack Overflow", "Sintese Total" }, IndiceCorreto = 0, Explicacao = "NullPointerException anula o multiplicador.", Semestre = 4, Materia = "TCC", Tipo = "Exame", Dificuldade = "Dificil" },
            new QuestaoDto { Enunciado = "Qual estrutura e mais adequada para LIFO?", Alternativas = new[] { "Pilha", "Fila", "Array ordenado", "Arvore" }, IndiceCorreto = 0, Explicacao = "Pilha representa LIFO.", Semestre = 4, Materia = "TCC", Tipo = "Exame", Dificuldade = "Dificil" }
        };
    }
}
