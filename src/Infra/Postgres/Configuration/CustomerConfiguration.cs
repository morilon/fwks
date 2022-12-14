using Fwks.ExampleService.Core.Domain;
using Fwks.Postgres.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fwks.ExampleService.Infra.Postgres.Configuration;

public sealed class CustomerConfiguration : EntityTypeConfiguration<Customer, int>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);

        builder
            .ToTable("Customers", "App");
    }
}