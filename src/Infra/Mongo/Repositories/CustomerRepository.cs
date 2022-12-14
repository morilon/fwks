using System;
using Fwks.ExampleService.Core.Abstractions.Repositories;
using Fwks.ExampleService.Core.Domain;
using Fwks.MongoDb.Repositories;
using MongoDB.Driver;

namespace Fwks.ExampleService.Infra.Mongo.Repositories;

public sealed class CustomerRepository : BaseRepository<CustomerDocument>, ICustomerRepository<CustomerDocument, Guid>
{
    public CustomerRepository(
        IMongoDatabase database)
        : base(database, "customers")
    {
    }
}