using System.Threading.Tasks;
using Fwks.AspNetCore.Attributes;
using Fwks.ExampleService.Core.Abstractions.Services;
using Fwks.ExampleService.Core.Domain.Filters;
using Fwks.ExampleService.Core.Domain.Requests;
using Fwks.ExampleService.Core.Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fwks.ExampleService.App.Api.Controllers.V1;

//[Authorize]
[ApiController]
[ApiVersion("1.0")]
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

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> PostAsync([FromBody] AddCustomerRequest request)
    {
        await _service.AddAsync(request);

        return Ok();
    }

    [HttpGet("")]
    [ProducesPagedResponse<CustomerResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromQuery] GetCustomerByNamePageFilter filter)
    {
        return Ok(await _service.FindByFilterAsync(filter));
    }
}