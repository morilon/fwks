using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fwks.Core.Abstractions.Services;

public interface ITransactionService : IDisposable
{
    Task<bool> CommitAsync(CancellationToken cancellationToken = default);
    bool Commit();
}
