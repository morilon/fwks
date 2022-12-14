namespace Fwks.ExampleService.Core.Domain.Requests;

public sealed record AddCustomerRequest(string Name, string DateOfBirth, string Email, string PhoneNumber);