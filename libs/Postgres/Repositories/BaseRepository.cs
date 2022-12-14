using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fwks.Core.Abstractions.Repositories;
using Fwks.Core.Domain;
using Fwks.Postgres.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Fwks.Postgres.Repositories;

public abstract class BaseRepository<TEntity, TKeyType> : IRepository<TEntity, TKeyType>, IDisposable
    where TEntity : Entity<TKeyType>
    where TKeyType : struct
{
    protected readonly DbContext DbContext;
    protected readonly DbSet<TEntity> DbSet;

    public BaseRepository(
        DbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public void Dispose()
    {
        DbContext.Dispose();

        GC.SuppressFinalize(this);
    }

    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return DbSet.AnyAsync(predicate);
    }

    public Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public Task<TEntity> FindOneByAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return DbSet.AsNoTracking().SingleOrDefaultAsync(predicate);
    }

    public Task<Page<TEntity>> FindPageByAsync(int currentPage, int pageSize, Expression<Func<TEntity, bool>> predicate)
    {
        return DbSet.GetPageAsync<TEntity, TKeyType>(currentPage, pageSize, predicate);
    }

    public Task<List<TEntity>> GetAllAsync()
    {
        return DbSet.AsNoTracking().ToListAsync();
    }

    public Task<TEntity> GetByIdAsync(TKeyType id)
    {
        return DbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public Task RemoveAsync(TEntity entity)
    {
        DbSet.Remove(entity); 
        
        return Task.CompletedTask;
    }

    public async Task RemoveByIdAsync(TKeyType id)
    {
        var entity = await DbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (entity == null)
            return;

        await RemoveAsync(entity);
    }

    [Obsolete("EF uses tracking to update. Use SaveChanges methods.", true)]
    public Task UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException("Use tracking along with SaveChanges to Add, Remove or Update entities.");
    }
}
