using Fwks.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fwks.Postgres.Configuration;

public abstract class EntityTypeConfiguration<TEntity, TKeyType> : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity<TKeyType>
    where TKeyType : struct
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Ignore(x => x.Notifications);
    }
}
