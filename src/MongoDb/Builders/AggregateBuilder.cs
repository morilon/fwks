using MongoDB.Driver;

namespace Fwks.MongoDb.Builders;

public static class AggregateBuilder
{
    public const string CountFacetId = "COUNT_FACET";
    public const string ItemsFacetId = "ITEMS_FACET";

    public static AggregateFacet<TInput, TOutput> Create<TInput, TOutput>(string name, params IPipelineStageDefinition[] stages)
    {
        return AggregateFacet.Create(name, PipelineDefinition<TInput, TOutput>.Create(stages));
    }

    public static AggregateFacet<TEntity, AggregateCountResult> Count<TEntity>()
    {
        return AggregateFacet.Create(CountFacetId, PipelineDefinition<TEntity, AggregateCountResult>.Create(new[] { PipelineStageDefinitionBuilder.Count<TEntity>() }));
    }

    public static AggregateFacet<TEntity, TEntity> Items<TEntity>(int currentPage, int pageSize)
    {
        return AggregateFacet.Create(ItemsFacetId, PipelineDefinition<TEntity, TEntity>.Create(new[]
        {
        PipelineStageDefinitionBuilder.Skip<TEntity>((currentPage - 1) * pageSize),
        PipelineStageDefinitionBuilder.Limit<TEntity>(pageSize)
    }));
    }
}