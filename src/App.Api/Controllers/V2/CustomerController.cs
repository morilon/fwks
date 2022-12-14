using System.Threading.Tasks;
using Fwks.AspNetCore.Attributes;
using Fwks.ExampleService.Core.Abstractions.Services;
using Fwks.ExampleService.Core.Domain.Filters;
using Fwks.ExampleService.Core.Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fwks.ExampleService.App.Api.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("v{v:apiVersion}/[controller]")]
public sealed class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerService _service;

    public CustomerController(
        ILogger<CustomerController> logger,
        ICustomerService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("")]
    [ProducesPagedResponse<CustomerResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(GetCustomerByNamePageFilter filter)
    {
        return Ok(await _service.FindByFilterAsync(filter));
    }
}