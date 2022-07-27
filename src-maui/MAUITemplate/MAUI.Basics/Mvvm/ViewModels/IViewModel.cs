using MAUI.Basics.Mvvm.Navigations.Interfaces;

namespace MAUI.Basics.Mvvm.ViewModels
{
    public interface IViewModel : IViewModel<object>
    {
    }

    public interface IViewModel<in TParams> : ILoadable
    {
        public Task InitializeAsync(TParams @params);
    }
}