using System.Threading;

namespace Xamarin.Basics.CancellationToken
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
            if (tokenSource != null && tokenSource.Token.CanBeCanceled)
                tokenSource.Cancel();
        }
    }
}