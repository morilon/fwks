using Microsoft.EntityFrameworkCore;

namespace Fwks.ExampleService.Infra.Postgres.Contexts;

public sealed class AppServiceContext : DbContext
{
    public AppServiceContext() { }

    public AppServiceContext(DbContextOptions<AppServiceContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppServiceContext).Assembly);
    }
}
