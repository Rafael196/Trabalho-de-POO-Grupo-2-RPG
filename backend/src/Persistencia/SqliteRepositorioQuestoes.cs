using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace CampusQuest.Persistencia;

public class SqliteRepositorioQuestoes : IRepositorioQuestoes
{
    private readonly string caminhoDb;
    private const string ColunasQuestoes = "Id, Enunciado, Alternativa1, Alternativa2, Alternativa3, Alternativa4, " +
        "IndiceCorreto, Explicacao, Semestre, Materia, Tipo, Dificuldade, Ativa";

    public SqliteRepositorioQuestoes(string caminhoDb = "database/campusquest.db")
    {
        this.caminhoDb = caminhoDb ?? "database/campusquest.db";
    }

    public IEnumerable<QuestaoDto> ObterPorSemestre(int semestre)
    {
        return ObterQuizPorSemestre(semestre);
    }

    public IEnumerable<QuestaoDto> ObterQuizPorSemestre(int semestre)
    {
        List<QuestaoDto> questoes = new();
        string conexaoString = $"Data Source={caminhoDb}";

        using (SqliteConnection conn = new(conexaoString))
        {
            conn.Open();
            string sql = $"SELECT {ColunasQuestoes} FROM Questoes " +
                         "WHERE Semestre = @semestre AND Tipo = @tipo AND Ativa = 1";

            using (SqliteCommand cmd = new(sql, conn))
            {
                cmd.Parameters.AddWithValue("@semestre", semestre);
                cmd.Parameters.AddWithValue("@tipo", "Quiz");
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        questoes.Add(LerQuestao(reader));
                    }
                }
            }
        }

        return questoes;
    }

    public IEnumerable<QuestaoDto> ObterExamePorMateria(string materia)
    {
        List<QuestaoDto> questoes = new();
        string conexaoString = $"Data Source={caminhoDb}";

        using (SqliteConnection conn = new(conexaoString))
        {
            conn.Open();
            string sql = $"SELECT {ColunasQuestoes} FROM Questoes " +
                         "WHERE Materia = @materia AND Tipo = @tipo AND Ativa = 1";

            using (SqliteCommand cmd = new(sql, conn))
            {
                cmd.Parameters.AddWithValue("@materia", materia ?? string.Empty);
                cmd.Parameters.AddWithValue("@tipo", "Exame");
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        questoes.Add(LerQuestao(reader));
                    }
                }
            }
        }

        return questoes;
    }

    public IEnumerable<QuestaoDto> ObterTodas()
    {
        List<QuestaoDto> questoes = new();
        string conexaoString = $"Data Source={caminhoDb}";

        using (SqliteConnection conn = new(conexaoString))
        {
            conn.Open();
            string sql = $"SELECT {ColunasQuestoes} FROM Questoes";

            using (SqliteCommand cmd = new(sql, conn))
            {
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        questoes.Add(LerQuestao(reader));
                    }
                }
            }
        }

        return questoes;
    }

    public QuestaoDto ObterPorId(int id)
    {
        string conexaoString = $"Data Source={caminhoDb}";

        using (SqliteConnection conn = new(conexaoString))
        {
            conn.Open();
            string sql = $"SELECT {ColunasQuestoes} FROM Questoes WHERE Id = @id";

            using (SqliteCommand cmd = new(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return LerQuestao(reader);
                    }
                }
            }
        }

        return null;
    }

    public void Inserir(QuestaoDto questao)
    {
        if (questao == null)
            throw new ArgumentNullException(nameof(questao));

        string conexaoString = $"Data Source={caminhoDb}";

        using (SqliteConnection conn = new(conexaoString))
        {
            conn.Open();
            string sql = "INSERT INTO Questoes (Enunciado, Alternativa1, Alternativa2, Alternativa3, Alternativa4, " +
                         "IndiceCorreto, Explicacao, Semestre, Materia, Tipo, Dificuldade, Ativa) " +
                         "VALUES (@enunciado, @alt1, @alt2, @alt3, @alt4, @indice, @explicacao, @semestre, @materia, @tipo, @dificuldade, @ativa)";

            using (SqliteCommand cmd = new(sql, conn))
            {
                AdicionarParametros(cmd, questao);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Atualizar(QuestaoDto questao)
    {
        if (questao == null)
            throw new ArgumentNullException(nameof(questao));

        string conexaoString = $"Data Source={caminhoDb}";

        using (SqliteConnection conn = new(conexaoString))
        {
            conn.Open();
            string sql = "UPDATE Questoes SET Enunciado = @enunciado, Alternativa1 = @alt1, Alternativa2 = @alt2, " +
                         "Alternativa3 = @alt3, Alternativa4 = @alt4, IndiceCorreto = @indice, Explicacao = @explicacao, " +
                         "Semestre = @semestre, Materia = @materia, Tipo = @tipo, Dificuldade = @dificuldade, Ativa = @ativa WHERE Id = @id";

            using (SqliteCommand cmd = new(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", questao.Id);
                AdicionarParametros(cmd, questao);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Deletar(int id)
    {
        string conexaoString = $"Data Source={caminhoDb}";

        using (SqliteConnection conn = new(conexaoString))
        {
            conn.Open();
            string sql = "DELETE FROM Questoes WHERE Id = @id";

            using (SqliteCommand cmd = new(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }

    private QuestaoDto LerQuestao(SqliteDataReader reader)
    {
        return new QuestaoDto
        {
            Id = reader.GetInt32(0),
            Enunciado = reader.GetString(1),
            Alternativas = new[]
            {
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(5)
            },
            IndiceCorreto = reader.GetInt32(6),
            Explicacao = reader.GetString(7),
            Semestre = reader.GetInt32(8),
            Materia = reader.GetString(9),
            Tipo = reader.GetString(10),
            Dificuldade = reader.GetString(11),
            Ativa = reader.GetInt32(12) == 1
        };
    }

    private void AdicionarParametros(SqliteCommand cmd, QuestaoDto questao)
    {
        cmd.Parameters.AddWithValue("@enunciado", questao.Enunciado ?? string.Empty);
        cmd.Parameters.AddWithValue("@alt1", questao.Alternativas.Length > 0 ? questao.Alternativas[0] : string.Empty);
        cmd.Parameters.AddWithValue("@alt2", questao.Alternativas.Length > 1 ? questao.Alternativas[1] : string.Empty);
        cmd.Parameters.AddWithValue("@alt3", questao.Alternativas.Length > 2 ? questao.Alternativas[2] : string.Empty);
        cmd.Parameters.AddWithValue("@alt4", questao.Alternativas.Length > 3 ? questao.Alternativas[3] : string.Empty);
        cmd.Parameters.AddWithValue("@indice", questao.IndiceCorreto);
        cmd.Parameters.AddWithValue("@explicacao", questao.Explicacao ?? string.Empty);
        cmd.Parameters.AddWithValue("@semestre", questao.Semestre);
        cmd.Parameters.AddWithValue("@materia", questao.Materia ?? string.Empty);
        cmd.Parameters.AddWithValue("@tipo", string.IsNullOrWhiteSpace(questao.Tipo) ? "Quiz" : questao.Tipo);
        cmd.Parameters.AddWithValue("@dificuldade", string.IsNullOrWhiteSpace(questao.Dificuldade) ? "Normal" : questao.Dificuldade);
        cmd.Parameters.AddWithValue("@ativa", questao.Ativa ? 1 : 0);
    }
}
