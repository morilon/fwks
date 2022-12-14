using Fwks.Core.Abstractions.Repositories;
using Fwks.Core.Domain;

namespace Fwks.ExampleService.Core.Abstractions.Repositories;

public interface ICustomerRepository<TEntity, TKeyType> : IRepository<TEntity, TKeyType>
    where TEntity : Entity<TKeyType>
    where TKeyType : struct
{
}