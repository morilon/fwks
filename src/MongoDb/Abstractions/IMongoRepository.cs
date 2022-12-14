using System;
using System.Threading.Tasks;
using Fwks.Core.Domain;
using MongoDB.Driver;

namespace Fwks.MongoDb.Abstractions;

public interface IMongoRepository<TEntity> where TEntity : Entity<Guid>
{
    Task<Page<TEntity>> FindPageByAsync(int currentPage, int pageSize, FilterDefinition<TEntity> filter);
}