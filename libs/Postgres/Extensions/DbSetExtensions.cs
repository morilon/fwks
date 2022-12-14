using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fwks.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Fwks.Postgres.Extensions;

public static class DbSetExtensions
{
    public static async Task<Page<TEntity>> GetPageAsync<TEntity, TKeyType>(this DbSet<TEntity> dbSet, int currentPage, int pageSize, Expression<Func<TEntity, bool>> predicate)
        where TEntity : Entity<TKeyType>
        where TKeyType : struct
    {
        var query = dbSet.AsNoTracking().Where(predicate);

        return new Page<TEntity>
        {
            CurrentPage = currentPage,
            PageSize = pageSize,
            Items = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync(),
            TotalItems = await query.CountAsync()
        };
    }
}