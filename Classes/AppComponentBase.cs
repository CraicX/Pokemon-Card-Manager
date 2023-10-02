using System;
using System.Threading;
using Microsoft.AspNetCore.Components;

namespace PokeCardManager.Classes;
public abstract class AppComponentBase : ComponentBase, IDisposable
{
    private CancellationTokenSource cancellationTokenSource;

    protected CancellationToken CancellationToken => (cancellationTokenSource ??= new()).Token;

    public virtual void Dispose()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
        }
    }
}