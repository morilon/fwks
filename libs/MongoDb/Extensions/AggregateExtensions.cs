using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fwks.Core.Domain;
using Fwks.MongoDb.Builders;
using MongoDB.Driver;

namespace Fwks.MongoDb.Extensions;

public static class AggregateExtensions
{
    public static async Task<Page<TEntity>> GetPageAsync<TEntity>(this IMongoCollection<TEntity> collection, int currentPage, int pageSize, FilterDefinition<TEntity> filter)
        where TEntity : class
    {
        var aggregate = await collection
            .Aggregate().Match(filter)
            .Facet(
                AggregateBuilder.Count<TEntity>(),
                AggregateBuilder.Items<TEntity>(currentPage, pageSize))
            .ToListAsync();

        return GetPage<TEntity>(aggregate, currentPage, pageSize);
    }

    public static async Task<Page<TEntity>> GetPageAsync<TEntity>(this IMongoCollection<TEntity> collection, int currentPage, int pageSize, Expression<Func<TEntity, bool>> predicate = default)
        where TEntity : class
    {
        var aggregate = await collection
            .Aggregate().Match(predicate ?? FilterDefinition<TEntity>.Empty)
            .Facet(
                AggregateBuilder.Count<TEntity>(),
                AggregateBuilder.Items<TEntity>(currentPage, pageSize))
            .ToListAsync();

        return GetPage<TEntity>(aggregate, currentPage, pageSize);
    }

    private static Page<TEntity> GetPage<TEntity>(List<AggregateFacetResults> aggregate, int currentPage, int pageSize) where TEntity : class
    {
        if (!aggregate.Any())
            return Page<TEntity>.Empty(currentPage, pageSize);

        return new Page<TEntity>
        {
            CurrentPage = currentPage,
            PageSize = pageSize,
            Items = aggregate[0].Facets.First(x => x.Name.Equals(AggregateBuilder.ItemsFacetId)).Output<TEntity>(),
            TotalItems = (int)aggregate[0].Facets.First(x => x.Name.Equals(AggregateBuilder.CountFacetId)).Output<AggregateCountResult>()[0].Count
        };
    }
}