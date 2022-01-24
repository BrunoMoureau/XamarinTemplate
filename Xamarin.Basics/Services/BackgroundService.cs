using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Basics.Helpers;

namespace Xamarin.Basics.Services
{
    public class BackgroundService
    {
        private CancellationTokenSource _cancellationTokenSource;

        public Task<TResult> CallAsync<TResult>(Func<CancellationToken, Task<TResult>> func)
        {
            CancellationTokenHelper.GenerateTokenSource(ref _cancellationTokenSource);
            return Task.Run(() => func(_cancellationTokenSource.Token), _cancellationTokenSource.Token);
        }

        public void Cancel()
        {
            CancellationTokenHelper.CancelTokenSource(_cancellationTokenSource);
        }
    }
}