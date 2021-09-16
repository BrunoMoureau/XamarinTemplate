using System.Threading.Tasks;
using Xamarin.Basics.Mvvm.Contracts.ViewModels;

namespace Xamarin.Basics.Mvvm.Utils
{
    public static class ViewModelUtils
    {
        public static Task InitializeAsync<TViewModelParams>(IViewModel<TViewModelParams> viewModel, TViewModelParams parameters)
        {
            return viewModel != null
                ? viewModel.InitializeAsync(parameters)
                : Task.CompletedTask;
        }
    }
}