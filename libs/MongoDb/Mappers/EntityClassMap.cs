using System;
using Fwks.Core.Domain;
using MongoDB.Bson.Serialization;

namespace Fwks.MongoDb.Mappers;

public abstract class EntityClassMap<TEntity> : BsonClassMap<TEntity> where TEntity : Entity<Guid>
{
    public EntityClassMap()
    {
        if (IsClassMapRegistered(typeof(TEntity)))
            RegisterClassMap<TEntity>(Map);
    }

    public virtual void Map(BsonClassMap<TEntity> mapper)
    {
        mapper.AutoMap();

        mapper.UnmapProperty(x => x.Notifications);
    }
}