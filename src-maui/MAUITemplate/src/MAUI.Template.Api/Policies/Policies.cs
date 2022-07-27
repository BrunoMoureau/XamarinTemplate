using System.Net;
using MAUI.Template.Api.Filters;
using Polly;
using Polly.Retry;
using Refit;

namespace MAUI.Template.Api.Policies
{
    public static class Policies
    {
        private static readonly HttpStatusCode[] BasicRetryStatusCodes =
        {
            HttpStatusCode.RequestTimeout, // 408
            HttpStatusCode.InternalServerError, // 500
            HttpStatusCode.BadGateway, // 502
            HttpStatusCode.ServiceUnavailable, // 503
            HttpStatusCode.GatewayTimeout // 504
        };

        private static readonly TimeSpan[] ThreeAttemptsSpacedByOneSecondEach =
        {
            TimeSpan.FromSeconds(1),
            TimeSpan.FromSeconds(1)
        };

        public static AsyncRetryPolicy Retry =>
            Policy
                .Handle<ApiException>(e => BasicRetryStatusCodes.Any(c => c == e.StatusCode))
                .Or<Exception>(HttpExceptionFilter.NoConnection)
                .Or<Exception>(HttpExceptionFilter.LostConnection)
                .WaitAndRetryAsync(ThreeAttemptsSpacedByOneSecondEach);
    }
}