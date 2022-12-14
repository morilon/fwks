using System.IO;
using System.Linq;
using Fwks.ExampleService.Core.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Fwks.ExampleService.Infra.Postgres.Contexts;

public sealed class MigrationContext : IDesignTimeDbContextFactory<AppServiceContext>
{
    public AppServiceContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AppServiceContext>()
            .UseNpgsql(BuildConnectionString(args), x => x.MigrationsHistoryTable("Migrations", "History"));

        return new AppServiceContext(builder.Options);
    }

    private static string BuildConnectionString(string[] args)
    {
        var index = args.ToList().IndexOf("--environment");

        var environment = index == -1 ? "Development" : args[index + 1];

        var path = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.FullName, "App.Api");

        return new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environment}.json", true)
            .Build()
            .Get<AppSettings>()
            .Storage
            .Postgres
            .BuildConnectionString();
    }
}
