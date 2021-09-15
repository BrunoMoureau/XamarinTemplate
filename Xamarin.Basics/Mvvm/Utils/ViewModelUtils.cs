using System;
using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Models;

namespace Xamarin.Basics.Mvvm.Utils
{
    public static class ViewModelUtils
    {
        public static Task InitializeAsync<TViewModelParams>(object bindingContext, TViewModelParams parameters)
            where TViewModelParams : ViewModelParams
        {
            var viewModel = GetViewModel<TViewModelParams>(bindingContext);
            return viewModel != null
                ? viewModel.InitializeAsync(parameters)
                : Task.CompletedTask;
        }

        private static ViewModel<TViewModelParams> GetViewModel<TViewModelParams>(object bindingContext)
            where TViewModelParams : ViewModelParams
            => bindingContext as ViewModel<TViewModelParams>;

        public static void DisposeViewModel(object bindingContext)
        {
            if (bindingContext is IDisposable disposable)
                disposable.Dispose();
        }

        public static void DisposeViewModels(params object[] bindingContexts)
        {
            foreach (var bindingContext in bindingContexts)
            {
                DisposeViewModel(bindingContext);
            }
        }
    }
}