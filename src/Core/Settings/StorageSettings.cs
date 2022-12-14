using Fwks.Core.Abstractions.Builders;

namespace Fwks.ExampleService.Core.Settings;

public sealed class StorageSettings
{
    public MongoDbSettings MongoDb { get; set; }
    public PostgresSettings Postgres { get; set; }
}

public sealed record MongoDbSettings(
    string ConnectionString, string Database) : IConnectionStringBuilder
{
    public string BuildConnectionString() => ConnectionString;
}

public sealed record PostgresSettings(
    string Host, int Port, string Database, string UserId, string Password) : IConnectionStringBuilder
{
    public string BuildConnectionString() => $"Host={Host};Port={Port};Database={Database};UserId={UserId};Password={Password}";
}