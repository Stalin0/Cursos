using Npgsql;

namespace BlockchainService.Data;

public static class DatabaseBootstrapper
{
    public static async Task EnsureDatabaseAsync(string connectionString, CancellationToken cancellationToken = default)
    {
        var builder = new NpgsqlConnectionStringBuilder(connectionString);
        var databaseName = builder.Database;

        if (string.IsNullOrWhiteSpace(databaseName))
        {
            throw new InvalidOperationException("Database name is required for blockchain persistence.");
        }

        var adminConnectionString = new NpgsqlConnectionStringBuilder(connectionString)
        {
            Database = "postgres"
        }.ToString();

        await using var connection = new NpgsqlConnection(adminConnectionString);
        await connection.OpenAsync(cancellationToken);

        await using var existsCommand = new NpgsqlCommand(
            "SELECT 1 FROM pg_database WHERE datname = @databaseName",
            connection);
        existsCommand.Parameters.AddWithValue("databaseName", databaseName);

        var exists = await existsCommand.ExecuteScalarAsync(cancellationToken) is not null;
        if (exists)
        {
            return;
        }

        var safeDatabaseName = databaseName.Replace("\"", "\"\"");
        await using var createCommand = new NpgsqlCommand(
            $"CREATE DATABASE \"{safeDatabaseName}\"",
            connection);

        await createCommand.ExecuteNonQueryAsync(cancellationToken);
    }
}
