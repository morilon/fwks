using System;
using System.Linq;
using System.Threading.Tasks;
using Fwks.Core.Abstractions.Contexts;
using Fwks.Core.Abstractions.Services;
using Fwks.Core.Domain;
using Fwks.Core.Extensions;
using Fwks.ExampleService.Core.Abstractions.Repositories;
using Fwks.ExampleService.Core.Abstractions.Services;
using Fwks.ExampleService.Core.Domain;
using Fwks.ExampleService.Core.Domain.Filters;
using Fwks.ExampleService.Core.Domain.Requests;
using Fwks.ExampleService.Core.Domain.Responses;
using Microsoft.Extensions.Logging;

namespace Fwks.ExampleService.Application.Services;

public sealed class CustomerService : ICustomerService
{
    private readonly ILogger<CustomerService> _logger;
    private readonly INotificationContext _notifications;

    private readonly ICustomerRepository<CustomerDocument, Guid> _mongoRepository;

    private readonly ICustomerRepository<Customer, int> _postgresRepository;
    private readonly ITransactionService _transactionService;

    public CustomerService(
        ILogger<CustomerService> logger,
        INotificationContext notifications,
        ICustomerRepository<CustomerDocument, Guid> mongoRepository,
        ICustomerRepository<Customer, int> postgresRepository,
        ITransactionService transactionService)
    {
        _logger = logger;
        _notifications = notifications;
        
        _mongoRepository = mongoRepository;
        
        _postgresRepository = postgresRepository;
        _transactionService = transactionService;
    }

    public async Task AddAsync(AddCustomerRequest request)
    {
        await ExecuteMongoDb();

        await ExecutePostgres();

        async Task ExecutePostgres()
        {
            _ = DateOnly.TryParse(request.DateOfBirth, out var dob);

            await _postgresRepository.AddAsync(new Customer
            {
                Name = request.Name,
                Email = request.Email,
                DateOfBirth = dob,
                PhoneNumber = request.PhoneNumber,
            });

            await _transactionService.CommitAsync();
        }

        async Task ExecuteMongoDb()
        {
            await _mongoRepository.AddAsync(new CustomerDocument
            {
                Name = request.Name,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
            });
        }
    }

    public async Task<Page<CustomerResponse>> FindByFilterAsync(GetCustomerByNamePageFilter filter)
    {
        return filter.DbType switch
        {
            DbType.MongoDb => await ExecuteMongoDb(),
            _ => await ExecutePostgres(),
        };

        async Task<Page<CustomerResponse>> ExecutePostgres()
        {
            var customers = await _postgresRepository
                .FindPageByAsync(
                    filter.CurrentPage,
                    filter.PageSize,
                    x => x.Name.ToLower().Contains(filter.Name.ToLower()));

            _logger.TraceCorrelatedInfo("Finding customers by filter.", new
            {
                Filter = filter,
                ResultCount = customers.Items.Count
            });

            if (!customers.Items.Any())
                _notifications.Add("404", "No Customers were found.");

            return customers.Transform(x =>
                new CustomerResponse(x.Guid, x.Name, x.DateOfBirth.ToString(), x.Email, x.PhoneNumber));
        }

        async Task<Page<CustomerResponse>> ExecuteMongoDb()
        {
            var customers = await _mongoRepository
                .FindPageByAsync(
                    filter.CurrentPage,
                    filter.PageSize,
                    x => x.Name.ToLower().Contains(filter.Name.ToLower()));

            _logger.TraceCorrelatedInfo("Finding customers by filter.", new
            {
                Filter = filter,
                ResultCount = customers.Items.Count
            });

            if (!customers.Items.Any())
                _notifications.Add("404", "No Customers were found.");

            return customers.Transform(x =>
                new CustomerResponse(x.Id, x.Name, x.DateOfBirth, x.Email, x.PhoneNumber));
        }
    }
}