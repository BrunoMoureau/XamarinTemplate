using MAUI.Basics.Helpers;

namespace MAUI.Basics.Services
{
    public class BackgroundTask
    {
        private CancellationTokenSource _cancellationTokenSource;

        public Task<TResult> RunAsync<TResult>(Func<CancellationToken, Task<TResult>> func)
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