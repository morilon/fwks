using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fwks.Core.Domain;

namespace Fwks.Core.Abstractions.Repositories;

public interface IRepository<TEntity, TKeyType>
    where TEntity : Entity<TKeyType>
    where TKeyType : struct
{
    Task AddAsync(TEntity entity);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(TKeyType id);
    Task<Page<TEntity>> FindPageByAsync(int currentPage, int pageSize, Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> FindOneByAsync(Expression<Func<TEntity, bool>> predicate);
    Task RemoveAsync(TEntity entity);
    Task RemoveByIdAsync(TKeyType id);
    Task UpdateAsync(TEntity entity);
}