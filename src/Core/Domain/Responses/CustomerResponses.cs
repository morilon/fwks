using System;

namespace Fwks.ExampleService.Core.Domain.Responses;

public sealed record CustomerResponse(Guid Guid, string Name, string DateOfBirth, string Email, string PhoneNumber);
