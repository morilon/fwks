using System;
using System.Threading;
using System.Threading.Tasks;
using Fwks.Core.Abstractions.Services;
using Microsoft.EntityFrameworkCore;

namespace Fwks.Postgres.Services;

public sealed class TransactionService<TDbContext> : ITransactionService
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;

    public TransactionService(
        TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Commit()
    {
        return _dbContext.SaveChanges() > 0;
    }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public void Dispose()
    {
        _dbContext.Dispose();

        GC.SuppressFinalize(this);
    }
}
