using System.Threading;

namespace Xamarin.Basics.Helpers
{
    public class CancellationTokenHelper
    {
        public static void GenerateTokenSource(ref CancellationTokenSource tokenSource)
        {
            CancelTokenSource(tokenSource);
            tokenSource = new CancellationTokenSource();
        }

        public static void CancelTokenSource(CancellationTokenSource tokenSource)
        {
            if (tokenSource is { Token: { CanBeCanceled: true } })
                tokenSource.Cancel();
        }
    }
}