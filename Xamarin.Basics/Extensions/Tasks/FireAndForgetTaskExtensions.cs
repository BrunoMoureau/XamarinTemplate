using System;
using System.Threading.Tasks;

namespace Xamarin.Basics.Extensions.Tasks
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }

    public static class FireAndForgetTaskExtensions
    {
#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
        public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null)
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
        {
            try
            {
                await task;
            }
            catch (Exception exception)
            {
                handler?.HandleError(exception);
            }
        }

#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
        public static async void FireAndForgetSafeAsync(this Task task, Action<Exception> errorHandler)
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
        {
            try
            {
                await task;
            }
            catch (Exception exception)
            {
                errorHandler(exception);
            }
        }
    }
}