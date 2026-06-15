using System;
using System.IO;
using System.Text.Json;
using Microsoft.Data.Sqlite;

namespace CampusQuest.Persistencia;

public class SqliteRepositorioSave : IRepositorio
{
    private readonly string caminhoDb;
    private readonly JsonSerializerOptions jsonOptions;
    private const int SlotPadrao = 1;

    public SqliteRepositorioSave(string caminhoDb = "database/campusquest.db")
    {
        this.caminhoDb = caminhoDb ?? "database/campusquest.db";
        jsonOptions = new JsonSerializerOptions { WriteIndented = true };
    }

    public void Salvar(EstadoJogo estado)
    {
        if (estado == null)
            throw new ArgumentNullException(nameof(estado));

        string conexaoString = $"Data Source={caminhoDb}";

        using (SqliteConnection conn = new(conexaoString))
        {
            conn.Open();
            CriarTabelaSaveSeNaoExistir(conn);

            // Verificar se já existe save no slot
            string checkSql = "SELECT COUNT(*) FROM SaveJogo WHERE Slot = @slot";
            int existeSave = 0;
            using (SqliteCommand checkCmd = new(checkSql, conn))
            {
                checkCmd.Parameters.AddWithValue("@slot", SlotPadrao);
                existeSave = Convert.ToInt32(checkCmd.ExecuteScalar() ?? 0);
            }

            string json = JsonSerializer.Serialize(estado, jsonOptions);

            if (existeSave > 0)
            {
                // Atualizar
                string updateSql = @"
                    UPDATE SaveJogo 
                    SET NomeAluno = @nome, DadosJson = @dados, DataSalvamento = @data, Versao = @versao
                    WHERE Slot = @slot
                ";
                using (SqliteCommand updateCmd = new(updateSql, conn))
                {
                    AdicionarParametros(updateCmd, estado, json);
                    updateCmd.Parameters.AddWithValue("@slot", SlotPadrao);
                    updateCmd.ExecuteNonQuery();
                }
            }
            else
            {
                // Inserir
                string insertSql = @"
                    INSERT INTO SaveJogo (Slot, NomeAluno, DadosJson, DataSalvamento, Versao)
                    VALUES (@slot, @nome, @dados, @data, @versao)
                ";
                using (SqliteCommand insertCmd = new(insertSql, conn))
                {
                    insertCmd.Parameters.AddWithValue("@slot", SlotPadrao);
                    AdicionarParametros(insertCmd, estado, json);
                    insertCmd.ExecuteNonQuery();
                }
            }
        }
    }

    public EstadoJogo Carregar()
    {
        string conexaoString = $"Data Source={caminhoDb}";

        using (SqliteConnection conn = new(conexaoString))
        {
            conn.Open();
            CriarTabelaSaveSeNaoExistir(conn);

            string sql = "SELECT DadosJson FROM SaveJogo WHERE Slot = @slot";
            using (SqliteCommand cmd = new(sql, conn))
            {
                cmd.Parameters.AddWithValue("@slot", SlotPadrao);
                var resultado = cmd.ExecuteScalar();

                if (resultado == null || resultado == DBNull.Value)
                    throw new InvalidOperationException("Nenhum save encontrado.");

                string json = resultado.ToString();
                EstadoJogo estado = JsonSerializer.Deserialize<EstadoJogo>(json, jsonOptions);

                if (estado == null)
                    throw new InvalidOperationException("Save inválido.");

                return estado;
            }
        }
    }

    public bool ExisteArquivo()
    {
        string conexaoString = $"Data Source={caminhoDb}";

        using (SqliteConnection conn = new(conexaoString))
        {
            conn.Open();
            CriarTabelaSaveSeNaoExistir(conn);

            string sql = "SELECT COUNT(*) FROM SaveJogo WHERE Slot = @slot";
            using (SqliteCommand cmd = new(sql, conn))
            {
                cmd.Parameters.AddWithValue("@slot", SlotPadrao);
                int count = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                return count > 0;
            }
        }
    }

    public void Deletar()
    {
        string conexaoString = $"Data Source={caminhoDb}";

        using (SqliteConnection conn = new(conexaoString))
        {
            conn.Open();
            CriarTabelaSaveSeNaoExistir(conn);

            string sql = "DELETE FROM SaveJogo WHERE Slot = @slot";
            using (SqliteCommand cmd = new(sql, conn))
            {
                cmd.Parameters.AddWithValue("@slot", SlotPadrao);
                cmd.ExecuteNonQuery();
            }
        }
    }

    private void CriarTabelaSaveSeNaoExistir(SqliteConnection conn)
    {
        string sql = @"
            CREATE TABLE IF NOT EXISTS SaveJogo (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Slot INTEGER NOT NULL UNIQUE,
                NomeAluno TEXT NOT NULL,
                DadosJson TEXT NOT NULL,
                DataSalvamento TEXT NOT NULL,
                Versao TEXT NOT NULL
            );
        ";

        using (SqliteCommand cmd = new(sql, conn))
        {
            cmd.ExecuteNonQuery();
        }
    }

    private void AdicionarParametros(SqliteCommand cmd, EstadoJogo estado, string json)
    {
        cmd.Parameters.AddWithValue("@nome", estado.NomeAluno ?? string.Empty);
        cmd.Parameters.AddWithValue("@dados", json);
        cmd.Parameters.AddWithValue("@data", DateTime.UtcNow.ToString("O"));
        cmd.Parameters.AddWithValue("@versao", estado.Versao ?? "1.0");
    }
}
