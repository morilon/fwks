using System;
using System.Collections.Generic;
using System.Linq;

namespace Fwks.Core.Domain;

public class Page<TOutput> where TOutput : class
{
    public required int CurrentPage { get; set; }
    public required int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize);
    public required int TotalItems { get; set; }
    public required IReadOnlyList<TOutput> Items { get; set; }

    public static Page<TOutput> Empty(int currentPage, int pageSize)
    {
        return new()
        {
            CurrentPage = currentPage,
            PageSize = pageSize,
            TotalItems = 0,
            Items = new List<TOutput>()
        };
    }

    public Page<TTarget> Transform<TTarget>(Func<TOutput, TTarget> transformFunc) where TTarget : class
    {
        return new()
        {
            CurrentPage = CurrentPage,
            PageSize = PageSize,
            TotalItems = TotalItems,
            Items = Items.Select(transformFunc).ToList()
        };
    }
}