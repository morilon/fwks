using System.Threading.Tasks;
using Fwks.Core.Domain;
using Fwks.ExampleService.Core.Domain.Filters;
using Fwks.ExampleService.Core.Domain.Requests;
using Fwks.ExampleService.Core.Domain.Responses;

namespace Fwks.ExampleService.Core.Abstractions.Services;

public interface ICustomerService
{
    Task AddAsync(AddCustomerRequest request);
    Task<Page<CustomerResponse>> FindByFilterAsync(GetCustomerByNamePageFilter filter);
}