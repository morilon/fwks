using Fwks.Core.Domain.Filters;

namespace Fwks.ExampleService.Core.Domain.Filters;

public enum DbType
{
    MongoDb = 0,
    Postgres = 1
}

public sealed record GetCustomerByNamePageFilter(DbType DbType, string Name) : BasePageFilter;