namespace Fwks.Core.Domain.Filters;

public abstract record BasePageFilter(int CurrentPage = 1, int PageSize = 10);