using Fwks.ExampleService.Core.Abstractions.Repositories;
using Fwks.ExampleService.Core.Domain;
using Fwks.ExampleService.Infra.Postgres.Contexts;
using Fwks.Postgres.Repositories;

namespace Fwks.ExampleService.Infra.Postgres.Repositories;

public sealed class CustomerRepository : BaseRepository<Customer, int>, ICustomerRepository<Customer, int>
{
    public CustomerRepository(
        AppServiceContext dbContext) : base(dbContext)
    {
    }
}