namespace MAUI.Basics.Mvvm.ViewModels.Utils
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