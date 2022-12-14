using Fwks.ExampleService.Core.Domain;
using Fwks.ExampleService.Infra.Mongo.Abstractions;
using Fwks.MongoDb.Mappers;

namespace Fwks.ExampleService.Infra.Mongo.Mappers;

public sealed class CustomerMap : EntityClassMap<CustomerDocument>, IEntityMap
{
}